#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Bounded background queue wrapping a sink. Drops <see cref="KryptonLogLevel.Trace"/> and
/// <see cref="KryptonLogLevel.Debug"/> when full; blocks for <see cref="KryptonLogLevel.Error"/>
/// and <see cref="KryptonLogLevel.Fatal"/>.
/// </summary>
internal sealed class AsyncLogSink : IKryptonLogSink
{
    private readonly IKryptonLogSink _inner;
    private readonly int _capacity;
    private readonly ConcurrentQueue<KryptonLogEvent> _queue = new();
    private readonly ManualResetEventSlim _signal = new(false);
    private readonly ManualResetEventSlim _space = new(true);
    private readonly CancellationTokenSource _cts = new();
    private readonly Thread _thread;
    private int _count;
    private int _disposed;

    public AsyncLogSink(IKryptonLogSink inner, int capacity)
    {
        _inner = inner ?? NullLogSink.Instance;
        _capacity = Math.Max(32, capacity);
        _thread = new Thread(Pump)
        {
            Name = "KryptonLog",
            IsBackground = true
        };
        _thread.Start();
    }

    public bool IsEnabled(KryptonLogLevel level) => _inner.IsEnabled(level);

    public void Emit(KryptonLogEvent logEvent)
    {
        if (Volatile.Read(ref _disposed) != 0)
        {
            return;
        }

        while (true)
        {
            var count = Volatile.Read(ref _count);
            if (count < _capacity)
            {
                _queue.Enqueue(logEvent);
                Interlocked.Increment(ref _count);
                _signal.Set();
                return;
            }

            if (logEvent.Level < KryptonLogLevel.Error)
            {
                return;
            }

            _space.Reset();
            _space.Wait(50);
        }
    }

    public void Dispose()
    {
        if (Interlocked.Exchange(ref _disposed, 1) != 0)
        {
            return;
        }

        _cts.Cancel();
        _signal.Set();
        _space.Set();
        if (!_thread.Join(TimeSpan.FromSeconds(5)))
        {
            // Background thread will exit when the process does.
        }

        Drain();
        _inner.Dispose();
        _signal.Dispose();
        _space.Dispose();
        _cts.Dispose();
    }

    private void Pump()
    {
        var token = _cts.Token;
        while (!token.IsCancellationRequested)
        {
            try
            {
                _signal.Wait(token);
            }
            catch (OperationCanceledException)
            {
                break;
            }

            _signal.Reset();
            Drain();
        }

        Drain();
    }

    private void Drain()
    {
        while (_queue.TryDequeue(out var item))
        {
            Interlocked.Decrement(ref _count);
            _space.Set();
            try
            {
                _inner.Emit(item);
            }
            catch
            {
            }
        }
    }
}

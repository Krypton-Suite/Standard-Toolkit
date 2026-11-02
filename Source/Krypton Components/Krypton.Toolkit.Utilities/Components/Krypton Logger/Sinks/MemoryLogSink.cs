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
/// Fixed-capacity ring buffer of recent events for the log viewer and bug-report excerpts.
/// </summary>
public sealed class MemoryLogSink : IKryptonLogSink
{
    private readonly object _sync = new();
    private readonly KryptonLogEvent[] _buffer;
    private int _next;
    private int _count;

    /// <summary>
    /// Initializes a new instance of the <see cref="MemoryLogSink"/> class.
    /// </summary>
    /// <param name="capacity">Maximum events retained. Values below 16 are raised to 16.</param>
    public MemoryLogSink(int capacity)
    {
        Capacity = Math.Max(16, capacity);
        _buffer = new KryptonLogEvent[Capacity];
    }

    /// <summary>Gets the ring-buffer capacity.</summary>
    public int Capacity { get; }

    /// <summary>Gets the number of events currently stored.</summary>
    public int Count
    {
        get
        {
            lock (_sync)
            {
                return _count;
            }
        }
    }

    /// <summary>
    /// Raised after an event is stored. The viewer subscribes only while it is open.
    /// Handlers must not block; marshal to the UI thread if needed.
    /// </summary>
    public event EventHandler? EventsChanged;

    /// <inheritdoc />
    public bool IsEnabled(KryptonLogLevel level) => true;

    /// <inheritdoc />
    public void Emit(KryptonLogEvent logEvent)
    {
        lock (_sync)
        {
            _buffer[_next] = logEvent;
            _next = (_next + 1) % Capacity;
            if (_count < Capacity)
            {
                _count++;
            }
        }

        try
        {
            EventsChanged?.Invoke(this, EventArgs.Empty);
        }
        catch
        {
        }
    }

    /// <summary>
    /// Returns a snapshot of stored events in chronological order (oldest first).
    /// </summary>
    public KryptonLogEvent[] Snapshot()
    {
        lock (_sync)
        {
            if (_count == 0)
            {
                return Array.Empty<KryptonLogEvent>();
            }

            var result = new KryptonLogEvent[_count];
            var start = _count < Capacity ? 0 : _next;
            for (var i = 0; i < _count; i++)
            {
                result[i] = _buffer[(start + i) % Capacity];
            }

            return result;
        }
    }

    /// <summary>
    /// Renders the newest <paramref name="count"/> events using <paramref name="layout"/>.
    /// </summary>
    public string FormatRecent(int count, KryptonLogLayout? layout = null)
    {
        layout ??= KryptonLogLayout.Default;
        var events = Snapshot();
        if (events.Length == 0)
        {
            return string.Empty;
        }

        var take = Math.Min(Math.Max(1, count), events.Length);
        var start = events.Length - take;
        var sb = new StringBuilder();
        for (var i = start; i < events.Length; i++)
        {
            sb.Append(layout.Render(events[i]));
            if (!EndsWithNewLine(sb))
            {
                sb.AppendLine();
            }
        }

        return sb.ToString();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        EventsChanged = null;
        lock (_sync)
        {
            Array.Clear(_buffer, 0, _buffer.Length);
            _next = 0;
            _count = 0;
        }
    }

    private static bool EndsWithNewLine(StringBuilder sb)
    {
        var nl = Environment.NewLine;
        if (sb.Length < nl.Length)
        {
            return false;
        }

        for (var i = 0; i < nl.Length; i++)
        {
            if (sb[sb.Length - nl.Length + i] != nl[i])
            {
                return false;
            }
        }

        return true;
    }
}

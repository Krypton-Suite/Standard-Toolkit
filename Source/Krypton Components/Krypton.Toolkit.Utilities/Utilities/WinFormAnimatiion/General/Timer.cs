#region Original MIT License

/* The MIT License (MIT)
 *
 * Copyright (c) 2016 - 2020 Soroush Falahati
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

#endregion

#region Modified License

/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */

#endregion

namespace WinFormAnimation;

/// <summary>
///     The timer class, will execute your code in specific time frames
/// </summary>
internal class Timer
{
    private static Thread? _timerThread;

    private static readonly object LockHandle = new();

    private static readonly long StartTimeAsMs = DateTime.Now.Ticks;

    private static readonly List<Timer> Subscribers = [];

    private readonly Action<ulong> _callback;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Timer" /> class.
    /// </summary>
    /// <param name="callback">
    ///     The callback to be executed at each tick
    /// </param>
    /// <param name="fpsKnownLimit">
    ///     The max ticks per second
    /// </param>
    public Timer(Action<ulong> callback, FPSLimiterKnownValues fpsKnownLimit = FPSLimiterKnownValues.LimitThirty)
        : this(callback, (int) fpsKnownLimit)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Timer" /> class.
    /// </summary>
    /// <param name="callback">
    ///     The callback to be executed at each tick
    /// </param>
    /// <param name="fpsLimit">
    ///     The max ticks per second
    /// </param>
    public Timer(Action<ulong> callback, int fpsLimit)
    {
        _callback = callback ?? throw new ArgumentNullException(nameof(callback));
        FrameLimiter = fpsLimit;
        lock (LockHandle)
        {
            if (_timerThread == null)
            {
                (_timerThread = new Thread(ThreadCycle) {IsBackground = true}).Start();
            }
        }
    }

    /// <summary>
    ///     Gets the time of the last frame/tick related to the global-timer start reference
    /// </summary>
    public long LastTick { get; private set; }

    /// <summary>
    ///     Gets or sets the maximum frames/ticks per second
    /// </summary>
    public int FrameLimiter { get; set; }

    /// <summary>
    ///     Gets the time of the first frame/tick related to the global-timer start reference
    /// </summary>
    public long FirstTick { get; private set; }


    private void Tick()
    {
        if (1000 / FrameLimiter < GetTimeDifferenceAsMs() - LastTick)
        {
            LastTick = GetTimeDifferenceAsMs();
            _callback((ulong) (LastTick - FirstTick));
        }
    }

    private static long GetTimeDifferenceAsMs()
    {
        return (DateTime.Now.Ticks - StartTimeAsMs) / 10000;
    }

    private static void ThreadCycle()
    {
        while (true)
            try
            {
                bool hibernate;
                lock (Subscribers)
                {
                    hibernate = Subscribers.Count == 0;
                    if (!hibernate)
                    {
                        foreach (var t in Subscribers.ToList())
                            t.Tick();
                    }
                }

                Thread.Sleep(hibernate ? 50 : 1);
            }
            catch
            {
                // ignored
            }

        // ReSharper disable once FunctionNeverReturns
    }

    /// <summary>
    ///     The method to reset the time of the starting frame/tick
    /// </summary>
    public void ResetClock()
    {
        FirstTick = GetTimeDifferenceAsMs();
    }

    /// <summary>
    ///     The method to resume the timer after stopping it
    /// </summary>
    public void Resume()
    {
        lock (Subscribers)
        {
            if (!Subscribers.Contains(this))
            {
                FirstTick += GetTimeDifferenceAsMs() - LastTick;
                Subscribers.Add(this);
            }
        }
    }

    /// <summary>
    ///     The method to start the timer from the beginning
    /// </summary>
    public void Start()
    {
        lock (Subscribers)
        {
            if (!Subscribers.Contains(this))
            {
                FirstTick = GetTimeDifferenceAsMs();
                Subscribers.Add(this);
            }
        }
    }

    /// <summary>
    ///     The method to stop the timer from generating any new ticks/frames
    /// </summary>
    public void Stop()
    {
        lock (Subscribers)
        {
            if (Subscribers.Contains(this))
            {
                Subscribers.Remove(this);
            }
        }
    }
}

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Shows a non-blocking, themed splash window on a dedicated STA thread while the owner application
/// continues startup work on the calling thread.
/// </summary>
/// <remarks>
/// Use <see cref="Show(KryptonSplashScreenManagerData)"/> plus <see cref="SetStatus(string)"/> /
/// <see cref="SetProgress(int, int?)"/>, then <see cref="Close()"/> before <c>Application.Run</c> of the
/// main form. <see cref="Run(KryptonSplashScreenManagerData, IList{KryptonSplashStep})"/> executes a
/// sequence of caller-thread steps, updates progress from the step count, and reports exceptions.
/// Distinct from the modal <see cref="KryptonSplashScreen"/> in <c>Krypton.Toolkit</c>.
/// </remarks>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public sealed class KryptonSplashScreenManager : IDisposable
{
    #region Static Fields

    private static readonly TimeSpan ReadyTimeout = TimeSpan.FromSeconds(15);
    private static readonly TimeSpan CloseJoinTimeout = TimeSpan.FromSeconds(20);

    #endregion

    #region Instance Fields

    private readonly KryptonSplashScreenManagerData _data;
    private readonly PaletteMode _paletteMode;
    private readonly Thread _thread;
    private readonly ManualResetEventSlim _ready;
    private readonly ManualResetEventSlim _closed;
    private VisualSplashScreenManagerForm? _form;
    private Exception? _startException;
    private int _completedSteps;
    private int _closeRequested;
    private int _disposed;
    private int _shownTick;

    #endregion

    #region Identity

    private KryptonSplashScreenManager(KryptonSplashScreenManagerData data)
    {
        _data = data;
        _paletteMode = data.PaletteMode ?? PaletteMode.Global;
        _ready = new ManualResetEventSlim(false);
        _closed = new ManualResetEventSlim(false);
        _thread = new Thread(SplashThreadMain)
        {
            Name = "KryptonSplashScreen",
            IsBackground = true
        };
        _thread.SetApartmentState(ApartmentState.STA);
    }

    #endregion

    #region Public

    /// <summary>
    /// Shows a splash window on a dedicated STA thread and returns when the window handle exists.
    /// The calling thread is not blocked after this method returns.
    /// </summary>
    /// <param name="data">Splash content and behaviour. Cannot be null.</param>
    /// <returns>A manager the caller uses to update status/progress and to close the splash.</returns>
    public static KryptonSplashScreenManager Show(KryptonSplashScreenManagerData data)
    {
        ThrowHelper.ThrowIfNull(data);

        var manager = new KryptonSplashScreenManager(data);
        try
        {
            manager.Start();
            return manager;
        }
        catch
        {
            manager.Dispose();
            throw;
        }
    }

    /// <summary>
    /// Shows the splash, runs each step on the calling thread, then closes the splash.
    /// Exceptions from a step close the splash and optionally show <see cref="KryptonExceptionDialog"/>.
    /// </summary>
    /// <param name="data">Splash content and behaviour. Cannot be null.</param>
    /// <param name="steps">Startup steps. Cannot be null.</param>
    public static void Run(KryptonSplashScreenManagerData data, IList<KryptonSplashStep> steps)
    {
        ThrowHelper.ThrowIfNull(data);
        ThrowHelper.ThrowIfNull(steps);

        if (!data.ExpectedStepCount.HasValue)
        {
            data.ExpectedStepCount = steps.Count;
        }

        using var splash = Show(data);
        try
        {
            foreach (KryptonSplashStep? step in steps)
            {
                if (step == null)
                {
                    continue;
                }

                splash.SetStatus(step.Status ?? string.Empty);
                step.Action?.Invoke();
            }
        }
        catch (Exception exception)
        {
            splash.ReportException(exception);
            return;
        }

        splash.Close();
    }

    /// <summary>
    /// Shows the splash, runs each step on the calling thread, then closes the splash.
    /// </summary>
    /// <param name="data">Splash content and behaviour. Cannot be null.</param>
    /// <param name="steps">Startup steps.</param>
    public static void Run(KryptonSplashScreenManagerData data, params KryptonSplashStep[] steps) =>
        Run(data, (IList<KryptonSplashStep>)steps);

    /// <summary>
    /// Updates the splash status text. When <see cref="KryptonSplashScreenManagerData.ExpectedStepCount"/>
    /// is set, each call advances the progress bar by <c>100 / N</c>.
    /// </summary>
    /// <param name="status">Status text to display.</param>
    public void SetStatus(string status)
    {
        int progressValue = -1;
        int expected = _data.ExpectedStepCount ?? 0;
        if (expected > 0)
        {
            int completed = Interlocked.Increment(ref _completedSteps);
            progressValue = (int)Math.Min(100, Math.Round(completed * 100.0 / expected));
        }

        Log(status);
        InvokeOnSplash(form => form.ApplyStatus(status, progressValue));
    }

    /// <summary>Sets a determinate progress value on the splash progress bar.</summary>
    /// <param name="value">Progress value.</param>
    /// <param name="maximum">Progress maximum. When null, 100 is used.</param>
    public void SetProgress(int value, int? maximum = null)
    {
        int max = maximum ?? 100;
        InvokeOnSplash(form => form.ApplyProgress(value, max));
    }

    /// <summary>
    /// Closes the splash (after the minimum display time and optional fade-out) and waits for the splash thread to exit.
    /// </summary>
    public void Close()
    {
        if (Interlocked.Exchange(ref _closeRequested, 1) != 0)
        {
            WaitForThread();
            return;
        }

        WaitForMinimumDisplay();
        VisualSplashScreenManagerForm? form = _form;
        if (form != null && !form.IsDisposed && form.IsHandleCreated)
        {
            try
            {
                form.Invoke(new Action(form.RequestClose));
            }
            catch (ObjectDisposedException)
            {
            }
            catch (InvalidOperationException)
            {
            }
        }

        WaitForThread();
    }

    /// <summary>
    /// Logs the exception, closes the splash, and optionally shows <see cref="KryptonExceptionDialog"/>
    /// on the calling thread.
    /// </summary>
    /// <param name="exception">The exception thrown by owner-app startup work. Cannot be null.</param>
    public void ReportException(Exception exception)
    {
        ThrowHelper.ThrowIfNull(exception);

        Log($"Splash exception: {exception}");
        Close();
        if (_data.ShowExceptionDialog)
        {
            KryptonExceptionDialog.Show(exception);
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (Interlocked.Exchange(ref _disposed, 1) != 0)
        {
            return;
        }

        Close();
        _ready.Dispose();
        _closed.Dispose();
        GC.SuppressFinalize(this);
    }

    #endregion

    #region Implementation

    private void Start()
    {
        // Bind KryptonManager / SystemEvents to the owner STA thread before the splash thread
        // creates controls. A blocked STA wait without pumping is a known SystemEvents stack-overflow.
        _ = KryptonManager.CurrentGlobalPalette;

        _thread.Start();
        WaitWhilePumping(_ready, ReadyTimeout, "Timed out waiting for the splash screen to create its window handle.");

        if (_startException != null)
        {
            throw new InvalidOperationException("Failed to start the splash screen.", _startException);
        }

        _shownTick = Environment.TickCount;
    }

    private void SplashThreadMain()
    {
        try
        {
            SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
            var form = new VisualSplashScreenManagerForm(_data, _paletteMode);
            _form = form;
            _ = form.Handle;
            _ready.Set();
            Application.Run(form);
        }
        catch (Exception exception)
        {
            _startException = exception;
            _ready.Set();
        }
        finally
        {
            VisualSplashScreenManagerForm? form = _form;
            _form = null;
            if (form != null && !form.IsDisposed)
            {
                try
                {
                    form.Dispose();
                }
                catch (ObjectDisposedException)
                {
                }
            }

            _closed.Set();
        }
    }

    private void InvokeOnSplash(Action<VisualSplashScreenManagerForm> action)
    {
        VisualSplashScreenManagerForm? form = _form;
        if (form == null || form.IsDisposed || !form.IsHandleCreated)
        {
            return;
        }

        try
        {
            if (form.InvokeRequired)
            {
                form.BeginInvoke(new Action(() =>
                {
                    if (!form.IsDisposed)
                    {
                        action(form);
                    }
                }));
                return;
            }

            action(form);
        }
        catch (ObjectDisposedException)
        {
        }
        catch (InvalidOperationException)
        {
        }
    }

    private void WaitForMinimumDisplay()
    {
        int minimum = _data.MinimumDisplayMilliseconds;
        if (minimum <= 0 || _shownTick == 0)
        {
            return;
        }

        int elapsed = Environment.TickCount - _shownTick;
        int remaining = minimum - elapsed;
        if (remaining > 0)
        {
            Thread.Sleep(remaining);
        }
    }

    private void WaitForThread()
    {
        if (!_thread.IsAlive || Thread.CurrentThread == _thread)
        {
            return;
        }

        WaitWhilePumping(_closed, CloseJoinTimeout, null);
        if (_thread.IsAlive)
        {
            _thread.Join(CloseJoinTimeout);
        }
    }

    private static void WaitWhilePumping(ManualResetEventSlim handle, TimeSpan timeout, string? timeoutMessage)
    {
        var stopwatch = Stopwatch.StartNew();
        while (!handle.IsSet)
        {
            if (stopwatch.Elapsed > timeout)
            {
                if (!string.IsNullOrEmpty(timeoutMessage))
                {
                    ThrowHelper.ThrowInvalidOperationException(timeoutMessage);
                }

                return;
            }

            Application.DoEvents();
            handle.Wait(50);
        }
    }

    private void Log(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return;
        }

        var logger = _data.Logger;
        if (logger == null && _data.UseKryptonLog)
        {
            logger = KryptonLog.AsKryptonLogger();
        }

        logger?.Write(message);
        _data.LogCallback?.Invoke(message);
    }

    #endregion
}

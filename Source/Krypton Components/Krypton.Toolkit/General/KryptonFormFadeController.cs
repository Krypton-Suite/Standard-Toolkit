#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2023 - 2026. All rights reserved.
 *
 */
#endregion

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit;

/// <summary>
/// Fades <see cref="VisualForm"/> instances in and out. Disabled by default; enable explicitly.
/// Opacity steps use a WinForms <see cref="Timer"/> so the UI message pump stays alive
/// (including during nested modal loops and <c>ShowDialogAsync</c> on .NET 9+).
/// Original inspiration: https://gist.github.com/nathan-fiscaletti/3c0514862fe88b5664b10444e1098778
/// </summary>
internal class KryptonFormFadeController
{
    #region Instance Fields

    private bool _shouldClose;

    private float _fadeSpeed;

    private FormFadeDirection _fadeDirection;

    private FadeCompleted? _fadeCompleted;

    private Timer? _fadeTimer;

    private readonly VisualForm? _parentForm;

    private readonly VisualForm? _owner;

    #endregion

    #region Delegate

    public delegate void FadeCompleted();

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonFormFadeController" /> class.</summary>
    public KryptonFormFadeController(VisualForm owner, VisualForm? childForm) : this(owner) => _parentForm = childForm ?? null;

    public KryptonFormFadeController(VisualForm owner)
    {
        _owner = owner;
        _shouldClose = true;
    }

    #endregion

    #region Implementation

    private void EnsureFadeTimer()
    {
        if (_fadeTimer != null)
        {
            return;
        }

        _fadeTimer = new Timer
        {
            Interval = 10
        };
        _fadeTimer.Tick += (_, _) => UpdateOpacity();
    }

    private void StartFadeTimer()
    {
        EnsureFadeTimer();
        _fadeTimer!.Start();
    }

    private void StopFadeTimer()
    {
        if (_fadeTimer == null)
        {
            return;
        }

        _fadeTimer.Stop();
        _fadeTimer.Dispose();
        _fadeTimer = null;
    }

    private void CompleteFade()
    {
        StopFadeTimer();
        _fadeCompleted?.Invoke();
    }

    /// <summary>
    /// Update the opacity of the owner on each timer tick.
    /// </summary>
    private void UpdateOpacity()
    {
        if (_owner == null || _owner.IsDisposed)
        {
            CompleteFade();
            return;
        }

        switch (_fadeDirection)
        {
            case FormFadeDirection.In:
                if (_owner.Opacity < 1.0)
                {
                    _owner.Opacity += _fadeSpeed / 1000.0;
                }
                else
                {
                    _owner.Opacity = 1.0;
                    CompleteFade();
                }

                break;

            case FormFadeDirection.Out:
                if (_owner.Opacity > 0.1)
                {
                    _owner.Opacity -= _fadeSpeed / 1000.0;
                }
                else
                {
                    if (!_shouldClose)
                    {
                        _owner.Hide();
                    }
                    else
                    {
                        _owner.Close();
                    }

                    CompleteFade();
                }

                break;
        }
    }

    private float ResolveFadeSpeed(FadeSpeedChoice fadeSpeedChoice, float? fadeSpeed) =>
        fadeSpeedChoice switch
        {
            FadeSpeedChoice.Slowest => KryptonFormFadeSpeed.DEFAULT_SLOWEST,
            FadeSpeedChoice.Slower => KryptonFormFadeSpeed.DEFAULT_SLOWER,
            FadeSpeedChoice.Slow => KryptonFormFadeSpeed.DEFAULT_SLOW,
            FadeSpeedChoice.Normal => KryptonFormFadeSpeed.DEFAULT_NORMAL,
            FadeSpeedChoice.Fast => KryptonFormFadeSpeed.DEFAULT_FAST,
            FadeSpeedChoice.Faster => KryptonFormFadeSpeed.DEFAULT_FASTER,
            FadeSpeedChoice.Fastest => KryptonFormFadeSpeed.DEFAULT_FASTEST,
            FadeSpeedChoice.Custom => fadeSpeed ?? 0.5f,
            _ => fadeSpeed ?? _fadeSpeed
        };

    /// <summary>
    /// Fade the owner in as a modal dialog owned by <paramref name="finished"/>'s parent.
    /// Uses sync <see cref="Form.ShowDialog(IWin32Window)"/> so timer ticks run inside the nested modal pump.
    /// </summary>
    private Task<DialogResult> ShowDialog(float fadeSpeed, FadeCompleted? finished)
    {
        _fadeCompleted = finished;
        _owner!.Opacity = 0;
        _fadeSpeed = fadeSpeed;
        _fadeDirection = FormFadeDirection.In;
        StartFadeTimer();

        DialogResult result = _owner.ShowDialog(_parentForm!);
        StopFadeTimer();
        return Task.FromResult(result);
    }

    /// <summary>
    /// Fade the owner in while awaiting modal show (async on .NET 9+, sync ShowDialog fallback earlier).
    /// </summary>
    private async Task<DialogResult> ShowDialogAsyncCore(float fadeSpeed, FadeCompleted? finished)
    {
        _fadeCompleted = finished;
        _owner!.Opacity = 0;
        _fadeSpeed = fadeSpeed;
        _fadeDirection = FormFadeDirection.In;
        StartFadeTimer();

        try
        {
            // Await required so finally can stop the fade timer after the dialog completes.
            return await KryptonFormAsync.ShowDialogAsync(_owner, _parentForm!).ConfigureAwait(false);
        }
        finally
        {
            StopFadeTimer();
        }
    }

    private void FadeIn(float fadeSpeed, FadeCompleted? finished)
    {
        _owner!.Opacity = 0;
        _owner.Show();
        _fadeCompleted = finished;
        _fadeSpeed = fadeSpeed;
        _fadeDirection = FormFadeDirection.In;
        StartFadeTimer();
    }

    private void FadeIn(FadeSpeedChoice fadeSpeedChoice, FadeCompleted? finished, float? fadeSpeed)
    {
        _owner!.Opacity = 0;
        _owner.Show();
        _fadeCompleted = finished;
        _fadeSpeed = ResolveFadeSpeed(fadeSpeedChoice, fadeSpeed);
        _fadeDirection = FormFadeDirection.In;
        StartFadeTimer();
    }

    private void FadeOut(float? fadeSpeed, FadeCompleted? finished)
    {
        if (_owner!.Opacity < 0.1)
        {
            finished?.Invoke();
            return;
        }

        _fadeCompleted = finished;
        // Opacity is 0..1; previously incorrectly assigned 100.
        if (_owner.Opacity > 1.0)
        {
            _owner.Opacity = 1.0;
        }

        _fadeSpeed = fadeSpeed ?? 0.5f;
        _fadeDirection = FormFadeDirection.Out;
        StartFadeTimer();
    }

    private void FadeOut(FadeSpeedChoice fadeSpeedChoice, FadeCompleted? finished, float? fadeSpeed)
    {
        if (_owner!.Opacity < 0.1)
        {
            finished?.Invoke();
            return;
        }

        _fadeCompleted = finished;
        if (_owner.Opacity > 1.0)
        {
            _owner.Opacity = 1.0;
        }

        _fadeSpeed = ResolveFadeSpeed(fadeSpeedChoice, fadeSpeed);
        _fadeDirection = FormFadeDirection.Out;
        StartFadeTimer();
    }

    /// <summary>
    /// Fades a dialog in using parent owner and defined fade speed.
    /// </summary>
    public static Task<DialogResult> ShowDialog(VisualForm owner, VisualForm parent, float fadeSpeed)
    {
        var fader = new KryptonFormFadeController(owner, parent);
        return fader.ShowDialog(fadeSpeed, null);
    }

    /// <summary>
    /// Fades a dialog in using parent owner and defined fade speed and call the finished delegate.
    /// </summary>
    public static Task<DialogResult> ShowDialog(VisualForm owner, VisualForm parent, float fadeSpeed, FadeCompleted finished)
    {
        var fader = new KryptonFormFadeController(owner, parent);
        return fader.ShowDialog(fadeSpeed, finished);
    }

    /// <summary>
    /// Fades a dialog in asynchronously so the UI thread can remain responsive on .NET 9+ (sync ShowDialog fallback earlier).
    /// </summary>
    public static Task<DialogResult> ShowDialogAsync(VisualForm owner, VisualForm parent, float fadeSpeed)
    {
        var fader = new KryptonFormFadeController(owner, parent);
        return fader.ShowDialogAsyncCore(fadeSpeed, null);
    }

    /// <summary>
    /// Fades a dialog in asynchronously and invokes <paramref name="finished"/> when fade-in completes.
    /// </summary>
    public static Task<DialogResult> ShowDialogAsync(VisualForm owner, VisualForm parent, float fadeSpeed, FadeCompleted finished)
    {
        var fader = new KryptonFormFadeController(owner, parent);
        return fader.ShowDialogAsyncCore(fadeSpeed, finished);
    }

    public static void FadeIn(VisualForm owner, float fadeSpeed, FadeCompleted finished)
    {
        var fader = new KryptonFormFadeController(owner);
        fader.FadeIn(fadeSpeed, finished);
    }

    public static void FadeIn(VisualForm owner, FadeSpeedChoice fadeSpeedChoice, float? fadeSpeed, FadeCompleted? finished)
    {
        var fader = new KryptonFormFadeController(owner);
        fader.FadeIn(fadeSpeedChoice, finished, fadeSpeed);
    }

    /// <summary>
    /// Fade a owner out at the defined speed.
    /// </summary>
    public static void FadeOut(VisualForm owner, float? fadeSpeed, FadeCompleted? finished)
    {
        var fader = new KryptonFormFadeController(owner);
        fader.FadeOut(fadeSpeed, finished);
    }

    public static void FadeOut(VisualForm owner, FadeSpeedChoice fadeSpeedChoice, float? fadeSpeed, FadeCompleted? finished)
    {
        var fader = new KryptonFormFadeController(owner);
        fader.FadeOut(fadeSpeedChoice, finished, fadeSpeed);
    }

    /// <summary>
    /// Fade a owner in at the defined speed.
    /// </summary>
    public static void FadeIn(VisualForm owner, float fadeSpeed)
    {
        var fader = new KryptonFormFadeController(owner);
        fader.FadeIn(fadeSpeed, null);
    }

    /// <summary>
    /// Fade a owner out at the defined speed.
    /// </summary>
    public static void FadeOut(VisualForm owner, float fadeSpeed)
    {
        var fader = new KryptonFormFadeController(owner);
        fader.FadeOut(fadeSpeed, null);
    }

    /// <summary>
    /// Fade a owner out at the defined speed and close it when the fade has completed.
    /// </summary>
    public static void FadeOutAndClose(VisualForm owner, float fadeSpeed)
    {
        var fader = new KryptonFormFadeController(owner)
        {
            _shouldClose = true
        };
        fader.FadeOut(fadeSpeed, null);
    }

    /// <summary>
    /// Fade a owner out at the defined speed and close it when the fade has completed.
    /// After the owner has closed, call the FadeComplete delegate.
    /// </summary>
    public static void FadeOutAndClose(VisualForm owner, float fadeSpeed, FadeCompleted finished)
    {
        var fader = new KryptonFormFadeController(owner)
        {
            _shouldClose = true
        };
        fader.FadeOut(fadeSpeed, finished);
    }

    public static void FadeOutAndClose(VisualForm owner, FadeSpeedChoice fadeSpeedChoice, float? fadeSpeed, FadeCompleted? finished)
    {
        var fader = new KryptonFormFadeController(owner)
        {
            _shouldClose = true
        };
        fader.FadeOut(fadeSpeedChoice, finished, fadeSpeed);
    }

    /// <summary>Fades the owner in using the shared timer engine.</summary>
    public static void ModernFadeFormIn(VisualForm owner) => FadeIn(owner, KryptonFormFadeSpeed.DEFAULT_NORMAL);

    /// <summary>Fades the owner in using the shared timer engine.</summary>
    /// <param name="owner">The owner.</param>
    /// <param name="fadeSpeed">Opacity step scale (same units as other fade APIs).</param>
    public static void ModernFadeFormIn(VisualForm owner, int? fadeSpeed) =>
        FadeIn(owner, fadeSpeed.HasValue ? fadeSpeed.Value : KryptonFormFadeSpeed.DEFAULT_NORMAL);

    /// <summary>Fades the owner out using the shared timer engine.</summary>
    public static void ModernFadeFormOut(VisualForm owner, int? fadeSpeed) =>
        FadeOut(owner, fadeSpeed.HasValue ? fadeSpeed.Value : KryptonFormFadeSpeed.DEFAULT_NORMAL);

    /// <summary>Fades the owner out, then shows <paramref name="nextForm"/> when complete.</summary>
    public static void ModernFadeFormOut(VisualForm owner, VisualForm? nextForm) =>
        FadeOut(owner, (float)KryptonFormFadeSpeed.DEFAULT_NORMAL, () => nextForm?.Show());

    #endregion
}

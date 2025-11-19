#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>This deals with the fading in and out of <see cref="VisualForm"/>. The developer must explicitly enable this, as it is turned off by default. Original library: (https://gist.github.com/nathan-fiscaletti/3c0514862fe88b5664b10444e1098778).</summary>
internal class KryptonFormFadeController
{
    #region Instance Fields

    // Disabled unused field
    //private bool _fadingEnabled;

    private bool _shouldClose;

    private float _fadeIn;

    private float _fadeOut;

    private float _fadeSpeed;

    private int _fadeDuration;

    private FormFadeDirection _fadeDirection;

    private FadeCompleted _fadeCompleted;

    private readonly TaskCompletionSource<DialogResult> _showDialogResult;

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

        // Disabled unused field
        //_fadingEnabled = false;

        _shouldClose = true;

        _fadeIn = 0.0f;

        _fadeOut = 0.0f;

        _showDialogResult = new TaskCompletionSource<DialogResult>();
    }

    #endregion

    #region Implementation

    /// <summary>
    /// Begin fading the _owner.
    /// </summary>
    private void BeginFade()
    {
        UpdateOpacity();
            
        _fadeCompleted?.Invoke();
    }

    /// <summary>
    /// Update the opacity of the _owner using the timer.
    /// </summary>
    private void UpdateOpacity()
    {
        if (_owner!.IsDisposed)
        {
            return;
        }

        switch (_fadeDirection)
        {
            // Fade in
            case FormFadeDirection.In:
                if (_owner.Opacity < 1.0)
                {
                    _owner.Opacity += (_fadeSpeed / 1000.0);
                }
                else
                {
                    return;
                }

                break;

            // Fade out
            case FormFadeDirection.Out:
                if (_owner.Opacity > 0.1)
                {
                    _owner.Opacity -= (_fadeSpeed / 1000.0);
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

                    return;
                }
                break;
        }

        // Have to use a thread sleep, rather than an await, otherwise on close would have completed as disposed on the first await Task.Delay()
        Thread.Sleep(10);
        UpdateOpacity();
    }

    /// <summary>
    /// Fade the _owner in at the defined speed as a dialog
    /// based on parent _owner.
    /// </summary>
    private async Task<DialogResult> ShowDialog(float fadeSpeed, FadeCompleted? finished)
    {
        _parentForm!.BeginInvoke(() => _showDialogResult.SetResult(_owner!.ShowDialog(_parentForm)));

        _fadeCompleted = finished!;

        _owner!.Opacity = 0;
            
        _fadeSpeed = fadeSpeed;
            
        _fadeDirection = FormFadeDirection.In;

        BeginFade();

        return await _showDialogResult.Task;
    }

    /// <summary>
    /// Fade the _owner in at the defined speed.
    /// </summary>
    private void FadeIn(float fadeSpeed, FadeCompleted? finished)
    {
        _owner!.Opacity = 0;
        _owner.Show();

        _fadeCompleted = finished!;

        _fadeSpeed = fadeSpeed;

        _fadeDirection = FormFadeDirection.In;

        BeginFade();
    }

    private void FadeIn(FadeSpeedChoice fadeSpeedChoice, FadeCompleted? finished, float? fadeSpeed)
    {
        _owner!.Opacity = 0;

        _owner.Show();

        _fadeCompleted = finished!;

        switch (fadeSpeedChoice)
        {
            case FadeSpeedChoice.Slowest:
                _fadeSpeed = KryptonFormFadeSpeed.DEFAULT_SLOWEST;
                break;
            case FadeSpeedChoice.Slower:
                _fadeSpeed = KryptonFormFadeSpeed.DEFAULT_SLOWER;
                break;
            case FadeSpeedChoice.Slow:
                _fadeSpeed = KryptonFormFadeSpeed.DEFAULT_SLOW;
                break;
            case FadeSpeedChoice.Normal:
                _fadeSpeed = KryptonFormFadeSpeed.DEFAULT_NORMAL;
                break;
            case FadeSpeedChoice.Fast:
                _fadeSpeed = KryptonFormFadeSpeed.DEFAULT_FAST;
                break;
            case FadeSpeedChoice.Faster:
                _fadeSpeed = KryptonFormFadeSpeed.DEFAULT_FASTER;
                break;
            case FadeSpeedChoice.Fastest:
                _fadeSpeed = KryptonFormFadeSpeed.DEFAULT_FASTEST;
                break;
            case FadeSpeedChoice.Custom:
                _fadeSpeed = fadeSpeed ?? 0.5f;
                break;
        }

        _fadeDirection = FormFadeDirection.In;

        BeginFade();
    }

    /// <summary>
    /// Fade the _owner out at the defined speed.
    /// </summary>
    private void FadeOut(float? fadeSpeed, FadeCompleted? finished)
    {
        if (_owner!.Opacity < 0.1)
        {
            finished?.Invoke();
            return;
        }

        _fadeCompleted = finished!;
        _owner.Opacity = 100;
        _fadeSpeed = fadeSpeed ?? 0.5f;

        _fadeDirection = FormFadeDirection.Out;

        BeginFade();
    }

    private void FadeOut(FadeSpeedChoice fadeSpeedChoice, FadeCompleted? finished, float? fadeSpeed)
    {
        if (_owner!.Opacity < 0.1)
        {
            finished?.Invoke();

            return;
        }
        _fadeCompleted = finished!;

        _owner.Opacity = 100;

        switch (fadeSpeedChoice)
        {
            case FadeSpeedChoice.Slowest:
                _fadeSpeed = KryptonFormFadeSpeed.DEFAULT_SLOWEST;
                break;
            case FadeSpeedChoice.Slower:
                _fadeSpeed = KryptonFormFadeSpeed.DEFAULT_SLOWER;
                break;
            case FadeSpeedChoice.Slow:
                _fadeSpeed = KryptonFormFadeSpeed.DEFAULT_SLOW;
                break;
            case FadeSpeedChoice.Normal:
                _fadeSpeed = KryptonFormFadeSpeed.DEFAULT_NORMAL;
                break;
            case FadeSpeedChoice.Fast:
                _fadeSpeed = KryptonFormFadeSpeed.DEFAULT_FAST;
                break;
            case FadeSpeedChoice.Faster:
                _fadeSpeed = KryptonFormFadeSpeed.DEFAULT_FASTER;
                break;
            case FadeSpeedChoice.Fastest:
                _fadeSpeed = KryptonFormFadeSpeed.DEFAULT_FASTEST;
                break;
            case FadeSpeedChoice.Custom:
                _fadeSpeed = fadeSpeed ?? 0.5f;
                break;
        }

        _fadeDirection = FormFadeDirection.Out;

        BeginFade();
    }

    /// <summary>
    /// Fades a dialog in using parent _owner and defined fade speed.
    /// </summary>
    public static async Task<DialogResult> ShowDialog(VisualForm owner, VisualForm parent, float fadeSpeed)
    {
        KryptonFormFadeController fader = new KryptonFormFadeController(owner, parent);
        return await fader.ShowDialog(fadeSpeed, null);
    }

    /// <summary>
    /// Fades a dialog in using parent _owner and defined fade speed
    /// and call the finished delegate.)
    /// </summary>
    public static async Task<DialogResult> ShowDialog(VisualForm owner, VisualForm parent, float fadeSpeed, FadeCompleted finished)
    {
        KryptonFormFadeController fader = new KryptonFormFadeController(owner, parent);
        return await fader.ShowDialog(fadeSpeed, finished);
    }

    public static void FadeIn(VisualForm owner, float fadeSpeed, FadeCompleted finished)
    {
        KryptonFormFadeController fader = new KryptonFormFadeController(owner);
        fader.FadeIn(fadeSpeed, finished);
    }

    public static void FadeIn(VisualForm owner, FadeSpeedChoice fadeSpeedChoice, float? fadeSpeed, FadeCompleted? finished)
    {
        KryptonFormFadeController fader = new KryptonFormFadeController(owner);

        fader.FadeIn(fadeSpeedChoice, finished, fadeSpeed);
    }

    /// <summary>
    /// Fade a _owner out at the defined speed.
    /// </summary>
    public static void FadeOut(VisualForm owner, float? fadeSpeed, FadeCompleted? finished)
    {
        KryptonFormFadeController fader = new KryptonFormFadeController(owner);
        fader.FadeOut(fadeSpeed, finished);
    }

    public static void FadeOut(VisualForm owner, FadeSpeedChoice fadeSpeedChoice, float? fadeSpeed, FadeCompleted? finished)
    {
        KryptonFormFadeController fader = new KryptonFormFadeController(owner);

        fader.FadeOut(fadeSpeedChoice, finished, fadeSpeed);
    }

    /// <summary>
    /// Fade a _owner in at the defined speed.
    /// </summary>
    public static void FadeIn(VisualForm owner, float fadeSpeed)
    {
        KryptonFormFadeController fader = new KryptonFormFadeController(owner);
        fader.FadeIn(fadeSpeed, null);
    }

    /// <summary>
    /// Fade a _owner out at the defined speed.
    /// </summary>
    public static void FadeOut(VisualForm owner, float fadeSpeed)
    {
        KryptonFormFadeController fader = new KryptonFormFadeController(owner);
        fader.FadeOut(fadeSpeed, null);
    }

    /// <summary>
    /// Fade a _owner out at the defined speed and
    /// close it when the fade has completed.
    /// </summary>
    public static void FadeOutAndClose(VisualForm owner, float fadeSpeed)
    {
        KryptonFormFadeController fader = new KryptonFormFadeController(owner)
        {
            _shouldClose = true
        };
        fader.FadeOut(fadeSpeed, null);
    }

    /// <summary>
    /// Fade a _owner out at the defined speed and
    /// close it when the fade has completed.
    /// After the _owner has closed, call the FadeComplete delegate.
    /// </summary>
    public static void FadeOutAndClose(VisualForm owner, float fadeSpeed, FadeCompleted finished)
    {
        KryptonFormFadeController fader = new KryptonFormFadeController(owner)
        {
            _shouldClose = true
        };
        fader.FadeOut(fadeSpeed, finished);
    }

    public static void FadeOutAndClose(VisualForm owner, FadeSpeedChoice fadeSpeedChoice, float? fadeSpeed, FadeCompleted? finished)
    {
        KryptonFormFadeController fader = new KryptonFormFadeController(owner)
        {
            _shouldClose = true
        };

        fader.FadeOut(fadeSpeedChoice, finished, fadeSpeed);
    }

    /// <summary>Fades the _owner in.</summary>
    /// <param name="owner">The owner.</param>
    public static async void ModernFadeFormIn(VisualForm owner)
    {
        KryptonFormFadeController controller = new KryptonFormFadeController(owner);

        while (owner.Opacity <= 1.1)
        {
            await Task.Delay(controller._fadeDuration);

            owner.Opacity += 0.5;
        }

        owner.Opacity = 1;
    }

    /// <summary>Fades the _owner in.</summary>
    /// <param name="owner">The owner.</param>
    /// <param name="fadeSpeed">The fade speed.</param>
    public static void ModernFadeFormIn(VisualForm owner, int? fadeSpeed)
    {
        KryptonFormFadeController controller = new KryptonFormFadeController(owner);

        int speed = fadeSpeed ?? 50;

        for (controller._fadeIn = 0.0f; controller._fadeIn <= 1.1f; controller._fadeIn += 0.1f)
        {
            owner.Opacity = controller._fadeIn;

            owner.Refresh();

            Thread.Sleep(speed);
        }
    }

    /// <summary>Fades the _owner out.</summary>
    /// <param name="owner">The owner.</param>
    /// <param name="fadeSpeed">The fade speed.</param>
    public static void ModernFadeFormOut(VisualForm owner, int? fadeSpeed)
    {
        KryptonFormFadeController controller = new KryptonFormFadeController(owner);

        int speed = fadeSpeed ?? 50;

        for (controller._fadeOut = 90; controller._fadeOut >= 10; controller._fadeOut += 10)
        {
            owner.Opacity = controller._fadeOut / 100;

            owner.Refresh();

            Thread.Sleep(speed);
        }
    }

    /// <summary>Fades the _owner out.</summary>
    /// <param name="owner">The owner.</param>
    /// <param name="nextForm">The next _owner.</param>
    public async void ModernFadeFormOut(VisualForm owner, VisualForm? nextForm)
    {
        KryptonFormFadeController controller = new KryptonFormFadeController(owner, nextForm);

        while (owner.Opacity > 0.0)
        {
            await Task.Delay(controller._fadeDuration);

            owner.Opacity -= 0.05;
        }

        owner.Opacity = 0;

        nextForm?.Show();
    }

    #endregion
}
#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit;

/// <summary>
/// Manages cue hint text shimmer animation for input controls.
/// </summary>
internal sealed class CueHintAnimationController : IDisposable
{
    #region Instance Fields

    private const int BaseTimerInterval = 50;
    private const float BasePhaseStep = 0.04f;

    private readonly Func<bool> _shouldAnimate;
    private readonly Func<float> _getAnimationSpeed;
    private readonly NeedPaintHandler? _needPaint;
    private readonly Action? _invalidateCueSurface;
    private Timer? _timer;
    private float _animationPhase;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the CueHintAnimationController class.
    /// </summary>
    /// <param name="shouldAnimate">Delegate that indicates whether animation should currently run.</param>
    /// <param name="getAnimationSpeed">Delegate that returns the animation speed multiplier.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    /// <param name="invalidateCueSurface">Delegate that invalidates the surface where cue hint text is drawn.</param>
    public CueHintAnimationController(Func<bool> shouldAnimate,
        Func<float> getAnimationSpeed,
        NeedPaintHandler? needPaint,
        Action? invalidateCueSurface)
    {
        _shouldAnimate = shouldAnimate;
        _getAnimationSpeed = getAnimationSpeed;
        _needPaint = needPaint;
        _invalidateCueSurface = invalidateCueSurface;
    }

    #endregion

    #region AnimationPhase

    /// <summary>
    /// Gets the current animation phase.
    /// </summary>
    public float AnimationPhase => _animationPhase;

    #endregion

    #region Public

    /// <summary>
    /// Updates the animation timer based on the current cue hint state.
    /// </summary>
    public void UpdateAnimationState()
    {
        if (!_shouldAnimate())
        {
            StopAnimation();
            return;
        }

        if (_timer == null)
        {
            _timer = new Timer
            {
                Interval = BaseTimerInterval
            };
            _timer.Tick += OnAnimationTick;
        }

        if (!_timer.Enabled)
        {
            _timer.Start();
        }
    }

    /// <summary>
    /// Release resources used by the controller.
    /// </summary>
    public void Dispose()
    {
        StopAnimation();
        _timer?.Dispose();
        _timer = null;
    }

    #endregion

    #region Implementation

    private void StopAnimation()
    {
        _timer?.Stop();
        _animationPhase = 0f;
    }

    private void OnAnimationTick(object? sender, EventArgs e)
    {
        if (!_shouldAnimate())
        {
            StopAnimation();
            return;
        }

        _animationPhase += BasePhaseStep * _getAnimationSpeed();
        if (_animationPhase > 1f)
        {
            _animationPhase -= 1f;
        }

        _needPaint?.Invoke(this, new NeedLayoutEventArgs(false));
        _invalidateCueSurface?.Invoke();
    }

    #endregion
}

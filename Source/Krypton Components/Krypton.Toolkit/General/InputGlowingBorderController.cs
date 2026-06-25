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
/// Manages glowing border animation for input controls.
/// </summary>
internal sealed class InputGlowingBorderController : IDisposable
{
    #region Instance Fields

    private readonly InputGlowingBorderValues _values;
    private readonly NeedPaintHandler? _needPaint;
    private readonly Func<bool> _shouldDraw;
    private Timer? _timer;
    private float _animationPhase;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the InputGlowingBorderController class.
    /// </summary>
    /// <param name="values">Glowing border values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    /// <param name="shouldDraw">Delegate that indicates whether the glow should currently draw.</param>
    public InputGlowingBorderController(InputGlowingBorderValues values,
        NeedPaintHandler? needPaint,
        Func<bool> shouldDraw)
    {
        _values = values;
        _needPaint = needPaint;
        _shouldDraw = shouldDraw;
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
    /// Updates the animation timer based on the current glowing border state.
    /// </summary>
    public void UpdateAnimationState()
    {
        if (!_shouldDraw() || !_values.Animate)
        {
            StopAnimation();
            return;
        }

        if (_timer == null)
        {
            _timer = new Timer
            {
                Interval = 50
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
        if (!_shouldDraw() || !_values.Animate)
        {
            StopAnimation();
            return;
        }

        _animationPhase += 0.04f;
        if (_animationPhase > 1f)
        {
            _animationPhase -= 1f;
        }

        _needPaint?.Invoke(this, new NeedLayoutEventArgs(false));
    }

    #endregion
}

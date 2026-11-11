#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Hosts pulsing border values and animation for a control.
/// </summary>
internal sealed class InputPulsingBorderHost : IInputPulsingBorderProvider, IDisposable
{
    #region Instance Fields

    private readonly Control _control;
    private readonly NeedPaintHandler _needPaint;
    private readonly Func<bool> _isActive;
    private readonly Func<IPaletteTriple?> _getTripleState;
    private readonly Func<PaletteState> _getBorderState;
    private readonly InputPulsingBorderController _controller;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="InputPulsingBorderHost"/> class.
    /// </summary>
    /// <param name="control">Owning control.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    /// <param name="isActive">Delegate that returns whether the control is active.</param>
    /// <param name="getTripleState">Delegate that returns the current palette triple state.</param>
    /// <param name="getBorderState">Delegate that returns the current border palette state.</param>
    /// <param name="category">Manager group this control inherits from.</param>
    public InputPulsingBorderHost(Control control,
        NeedPaintHandler needPaint,
        Func<bool> isActive,
        Func<IPaletteTriple?> getTripleState,
        Func<PaletteState> getBorderState,
        InputPulsingBorderCategory category)
    {
        _control = control;
        _needPaint = needPaint;
        _isActive = isActive;
        _getTripleState = getTripleState;
        _getBorderState = getBorderState;

        NeedPaintHandler needPaintHandler = (sender, e) =>
        {
            needPaint(sender, e);
            UpdateAnimationState();
        };

        Values = new InputPulsingBorderValues(needPaintHandler, category);
        _controller = new InputPulsingBorderController(Values, OnAnimationNeedPaint, () => ShouldDrawGlowingBorder());
        KryptonManager.GlobalPulsingBorderChanged += OnGlobalPulsingBorderChanged;
        UpdateAnimationState();
    }

    #endregion

    #region IInputPulsingBorderProvider

    /// <inheritdoc />
    public InputPulsingBorderValues Values { get; }

    /// <inheritdoc />
    public float AnimationPhase => _controller.AnimationPhase;

    /// <inheritdoc />
    public bool ShouldDrawGlowingBorder() =>
        Values.Enable && _control.Enabled && ShouldShowGlowingBorder();

    /// <inheritdoc />
    public IPaletteTriple? GetGlowingBorderTripleState() => _getTripleState();

    /// <inheritdoc />
    public PaletteState GetGlowingBorderState() => _getBorderState();

    #endregion

    #region Public

    /// <summary>
    /// Updates the animation timer based on the current pulsing border state.
    /// </summary>
    public void UpdateAnimationState() => _controller.UpdateAnimationState();

    /// <summary>
    /// Release resources used by the host.
    /// </summary>
    public void Dispose()
    {
        KryptonManager.GlobalPulsingBorderChanged -= OnGlobalPulsingBorderChanged;
        _controller.Dispose();
    }

    #endregion

    #region Implementation

    private void OnAnimationNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        if (_control.IsDisposed || !_control.IsHandleCreated)
        {
            return;
        }

        // Do not invalidate child HWNDs (combo drop glyph, up-down buttons, etc.).
        _control.Invalidate(false);
    }

    private void OnGlobalPulsingBorderChanged(object? sender, EventArgs e)
    {
        if (_control.IsDisposed)
        {
            return;
        }

        UpdateAnimationState();
        _needPaint(this, new NeedLayoutEventArgs(true));
    }

    private bool ShouldShowGlowingBorder() => Values.ShowWhen switch
    {
        InputPulsingBorderShowWhen.Always => true,
        InputPulsingBorderShowWhen.Active => _isActive(),
        _ => _control.ContainsFocus
    };

    #endregion
}

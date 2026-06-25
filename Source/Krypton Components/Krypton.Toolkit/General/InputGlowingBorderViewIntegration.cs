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
/// Wires optional glowing border decoration into a control view hierarchy.
/// </summary>
internal sealed class InputGlowingBorderViewIntegration : IDisposable
{
    #region Instance Fields

    private readonly InputGlowingBorderHost _host;
    private readonly ViewDecoratorInputGlow _viewRoot;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the InputGlowingBorderViewIntegration class for an input control border view.
    /// </summary>
    /// <param name="control">Owning control.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    /// <param name="isActive">Delegate that returns whether the control is active for ShowWhen.Active.</param>
    /// <param name="getTripleState">Delegate that returns the current palette triple state.</param>
    /// <param name="borderView">Border view to decorate.</param>
    public InputGlowingBorderViewIntegration(Control control,
        NeedPaintHandler needPaint,
        Func<bool> isActive,
        Func<IPaletteTriple> getTripleState,
        ViewDrawDocker borderView)
        : this(control, needPaint, isActive, getTripleState, borderView, () => borderView.State)
    {
    }

    /// <summary>
    /// Initialize a new instance of the InputGlowingBorderViewIntegration class.
    /// </summary>
    /// <param name="control">Owning control.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    /// <param name="isActive">Delegate that returns whether the control is active for ShowWhen.Active.</param>
    /// <param name="getTripleState">Delegate that returns the current palette triple state.</param>
    /// <param name="borderView">Border view to decorate.</param>
    /// <param name="getBorderState">Delegate that returns the current border palette state.</param>
    public InputGlowingBorderViewIntegration(Control control,
        NeedPaintHandler needPaint,
        Func<bool> isActive,
        Func<IPaletteTriple> getTripleState,
        ViewBase borderView,
        Func<PaletteState> getBorderState)
    {
        _host = new InputGlowingBorderHost(control, needPaint, isActive, getTripleState, getBorderState);
        _viewRoot = new ViewDecoratorInputGlow(_host, borderView);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the decorated view root for the control view manager.
    /// </summary>
    public ViewBase ViewRoot => _viewRoot;

    /// <summary>
    /// Gets the glowing border values.
    /// </summary>
    public InputGlowingBorderValues Values => _host.Values;

    /// <summary>
    /// Updates the animation timer based on the current glowing border state.
    /// </summary>
    public void UpdateAnimationState() => _host.UpdateAnimationState();

    /// <summary>
    /// Release resources used by the integration.
    /// </summary>
    public void Dispose() => _host.Dispose();

    #endregion
}

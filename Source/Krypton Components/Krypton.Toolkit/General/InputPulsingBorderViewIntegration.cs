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
/// Wires optional pulsing border decoration into a control view hierarchy.
/// </summary>
internal sealed class InputPulsingBorderViewIntegration : IDisposable
{
    #region Instance Fields

    private readonly InputPulsingBorderHost _host;
    private readonly ViewDecoratorInputGlow _viewRoot;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the InputPulsingBorderViewIntegration class for an input control border view.
    /// </summary>
    public InputPulsingBorderViewIntegration(Control control,
        NeedPaintHandler needPaint,
        Func<bool> isActive,
        Func<IPaletteTriple?> getTripleState,
        ViewDrawDocker borderView,
        InputPulsingBorderCategory category = InputPulsingBorderCategory.Inputs)
        : this(control, needPaint, isActive, getTripleState, borderView, () => borderView.State, category)
    {
    }

    /// <summary>
    /// Initialize a new instance of the InputPulsingBorderViewIntegration class.
    /// </summary>
    public InputPulsingBorderViewIntegration(Control control,
        NeedPaintHandler needPaint,
        Func<bool> isActive,
        Func<IPaletteTriple?> getTripleState,
        ViewBase borderView,
        Func<PaletteState> getBorderState,
        InputPulsingBorderCategory category = InputPulsingBorderCategory.Inputs)
    {
        _host = new InputPulsingBorderHost(control, needPaint, isActive, getTripleState, getBorderState, category);
        _viewRoot = new ViewDecoratorInputGlow(_host, borderView);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the decorated view root for the control view manager.
    /// </summary>
    public ViewBase ViewRoot => _viewRoot;

    /// <summary>
    /// Gets the pulsing border values.
    /// </summary>
    public InputPulsingBorderValues Values => _host.Values;

    /// <summary>
    /// Updates the animation timer based on the current pulsing border state.
    /// </summary>
    public void UpdateAnimationState() => _host.UpdateAnimationState();

    /// <summary>
    /// Release resources used by the integration.
    /// </summary>
    public void Dispose() => _host.Dispose();

    #endregion
}

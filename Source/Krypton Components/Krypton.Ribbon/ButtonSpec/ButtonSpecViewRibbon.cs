#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Create and manage the view for a ribbon specific ButtonSpec definition.
/// </summary>
public class ButtonSpecViewRibbon : ButtonSpecView
{
    #region Instance Fields
    private ButtonSpecRibbonController? _controller;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonSpecViewRibbon class.
    /// </summary>
    /// <param name="redirector">Palette redirector.</param>
    /// <param name="paletteMetric">Source for metric values.</param>
    /// <param name="metricPadding">Padding metric for border padding.</param>
    /// <param name="manager">Reference to owning manager.</param>
    /// <param name="buttonSpec">Access</param>
    public ButtonSpecViewRibbon(PaletteRedirect? redirector,
        IPaletteMetric paletteMetric,
        PaletteMetricPadding metricPadding,
        ButtonSpecManagerBase? manager,
        ButtonSpec buttonSpec)
        : base(redirector!, paletteMetric, metricPadding, manager!, buttonSpec)
    {
    }
    #endregion

    #region Protected
    /// <summary>
    /// Create a button controller for the view.
    /// </summary>
    /// <param name="viewButton">View to be controlled.</param>
    /// <param name="needPaint">Paint delegate.</param>
    /// <param name="clickHandler">Reference to click handler.</param>
    /// <returns>Controller instance.</returns>
    public override ButtonSpecViewControllers CreateController(ViewDrawButton viewButton,
        NeedPaintHandler needPaint,
        MouseEventHandler? clickHandler)
    {
        // Create a ribbon specific button controller
        _controller = new ButtonSpecRibbonController(viewButton, needPaint)
        {
            BecomesFixed = true
        };
        _controller.Click += clickHandler;

        // If associated with a tooltip manager then pass mouse messages onto tooltip manager
        IMouseController? mouseController = _controller;
        if (Manager.ToolTipManager != null)
        {
            mouseController = new ToolTipController(Manager.ToolTipManager, viewButton, _controller);
        }

        // Return a collection of controllers
        return new ButtonSpecViewControllers(mouseController, _controller, _controller);
    }

    /// <summary>
    /// Processes the finish of the button being pressed.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnFinishDelegate(object? sender, EventArgs? e) =>
        // Ask the button to remove the fixed pressed appearance
        _controller?.RemoveFixed();
    #endregion
}
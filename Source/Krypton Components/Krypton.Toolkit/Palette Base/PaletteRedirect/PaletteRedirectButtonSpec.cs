#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Redirect button spec requests to provided target.
/// </summary>
public class PaletteRedirectButtonSpec : PaletteRedirect
{
    #region Instance Fields
    private readonly IPaletteButtonSpec _inherit;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRedirectButtonSpec class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    /// <param name="inherit">Redirection button spec requests.</param>
    public PaletteRedirectButtonSpec(PaletteBase target, IPaletteButtonSpec inherit)
        : base(target) =>
        _inherit = inherit;

    #endregion

    #region ButtonSpec
    /// <summary>
    /// Gets the image to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <param name="state">State for which image is required.</param>
    /// <returns>Image value.</returns>
    public override Image? GetButtonSpecImage(PaletteButtonSpecStyle style,
        PaletteState state) =>
        _inherit.GetButtonSpecImage(style, state);

    /// <summary>
    /// Gets the short text to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>String value.</returns>
    public override string GetButtonSpecShortText(PaletteButtonSpecStyle style) => _inherit.GetButtonSpecShortText(style);

    /// <summary>
    /// Gets the long text to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>String value.</returns>
    public override string GetButtonSpecLongText(PaletteButtonSpecStyle style) => _inherit.GetButtonSpecLongText(style);

    /// <summary>
    /// Gets the color to remap from the image to the container foreground.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>Color value.</returns>
    public override Color GetButtonSpecColorMap(PaletteButtonSpecStyle style) => _inherit.GetButtonSpecColorMap(style);

    /// <summary>
    /// Gets the button style used for drawing the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>PaletteButtonStyle value.</returns>
    public override PaletteButtonStyle GetButtonSpecStyle(PaletteButtonSpecStyle style) => _inherit.GetButtonSpecStyle(style);

    /// <summary>
    /// Get the location for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>HeaderLocation value.</returns>
    public override HeaderLocation GetButtonSpecLocation(PaletteButtonSpecStyle style) => _inherit.GetButtonSpecLocation(style);

    /// <summary>
    /// Gets the edge to position the button against.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>PaletteRelativeEdgeAlign value.</returns>
    public override PaletteRelativeEdgeAlign GetButtonSpecEdge(PaletteButtonSpecStyle style) => _inherit.GetButtonSpecEdge(style);

    /// <summary>
    /// Gets the button orientation.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>PaletteButtonOrientation value.</returns>
    public override PaletteButtonOrientation GetButtonSpecOrientation(PaletteButtonSpecStyle style) => _inherit.GetButtonSpecOrientation(style);

    #endregion
}
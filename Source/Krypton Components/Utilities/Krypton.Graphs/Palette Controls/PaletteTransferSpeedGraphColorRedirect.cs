#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Graphs;

/// <summary>
/// Inherit one graph colour from a transfer graph <see cref="PaletteElement"/>.
/// </summary>
internal sealed class PaletteTransferSpeedGraphColorInheritRedirect : PaletteElementColorInherit
{
    private readonly TransferSpeedGraphPaletteColor _color;
    private readonly PaletteElement _element;
    private PaletteRedirect _redirect;

    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteTransferSpeedGraphColorInheritRedirect"/> class.
    /// </summary>
    /// <param name="redirect">Source for inheriting values.</param>
    /// <param name="color">Graph colour this instance supplies as Color1.</param>
    /// <param name="element">Palette element that owns the five graph colours.</param>
    public PaletteTransferSpeedGraphColorInheritRedirect(PaletteRedirect redirect, TransferSpeedGraphPaletteColor color, PaletteElement element)
    {
        _redirect = redirect;
        _color = color;
        _element = element;
    }

    /// <summary>
    /// Update the redirector with a new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public void SetRedirector(PaletteRedirect redirect) => _redirect = redirect;

    /// <inheritdoc />
    public override Color GetElementColor1(PaletteState state) =>
        _color switch
        {
            TransferSpeedGraphPaletteColor.Plot => _redirect.GetElementColor1(_element, state),
            TransferSpeedGraphPaletteColor.Series => _redirect.GetElementColor2(_element, state),
            TransferSpeedGraphPaletteColor.Grid => _redirect.GetElementColor3(_element, state),
            TransferSpeedGraphPaletteColor.Border => _redirect.GetElementColor4(_element, state),
            _ => _redirect.GetElementColor5(_element, state)
        };

    /// <inheritdoc />
    public override Color GetElementColor2(PaletteState state) => GetElementColor1(state);

    /// <inheritdoc />
    public override Color GetElementColor3(PaletteState state) => GetElementColor1(state);

    /// <inheritdoc />
    public override Color GetElementColor4(PaletteState state) => GetElementColor1(state);

    /// <inheritdoc />
    public override Color GetElementColor5(PaletteState state) => GetElementColor1(state);
}

/// <summary>
/// Storage for one graph colour that defaults to the palette and can be overridden.
/// </summary>
public class PaletteTransferSpeedGraphColorRedirect : PaletteElementColor
{
    private readonly PaletteTransferSpeedGraphColorInheritRedirect _inherit;

    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteTransferSpeedGraphColorRedirect"/> class.
    /// </summary>
    /// <param name="redirect">Source for inheriting values.</param>
    /// <param name="color">Graph colour this storage supplies.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    /// <param name="element">Palette element that owns the five graph colours.</param>
    internal PaletteTransferSpeedGraphColorRedirect(PaletteRedirect redirect, TransferSpeedGraphPaletteColor color, NeedPaintHandler? needPaint, PaletteElement element = PaletteElement.TransferSpeedGraph)
        : base(null, needPaint)
    {
        _inherit = new PaletteTransferSpeedGraphColorInheritRedirect(redirect, color, element);
        SetInherit(_inherit);
    }

    /// <summary>
    /// Update the redirector with a new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    internal void SetRedirector(PaletteRedirect redirect) => _inherit.SetRedirector(redirect);
}

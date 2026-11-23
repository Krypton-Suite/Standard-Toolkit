#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Which transfer-speed graph colour is inherited from <see cref="PaletteElement.TransferSpeedGraph"/>.
/// </summary>
internal enum TransferSpeedGraphPaletteColor
{
    /// <summary>Plot background (<see cref="IPaletteElementColor.GetElementColor1"/> of the palette element).</summary>
    Plot = 1,

    /// <summary>Series fill (palette element Color2).</summary>
    Series = 2,

    /// <summary>Grid lines (palette element Color3).</summary>
    Grid = 3,

    /// <summary>Chart frame border (palette element Color4).</summary>
    Border = 4,

    /// <summary>Speed-label text (palette element Color5).</summary>
    Label = 5
}

/// <summary>
/// Inherit one graph colour from <see cref="PaletteElement.TransferSpeedGraph"/>.
/// </summary>
internal sealed class PaletteTransferSpeedGraphColorInheritRedirect : PaletteElementColorInherit
{
    private readonly TransferSpeedGraphPaletteColor _color;
    private PaletteRedirect _redirect;

    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteTransferSpeedGraphColorInheritRedirect"/> class.
    /// </summary>
    /// <param name="redirect">Source for inheriting values.</param>
    /// <param name="color">Graph colour this instance supplies as Color1.</param>
    public PaletteTransferSpeedGraphColorInheritRedirect(PaletteRedirect redirect, TransferSpeedGraphPaletteColor color)
    {
        _redirect = redirect;
        _color = color;
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
            TransferSpeedGraphPaletteColor.Plot => _redirect.GetElementColor1(PaletteElement.TransferSpeedGraph, state),
            TransferSpeedGraphPaletteColor.Series => _redirect.GetElementColor2(PaletteElement.TransferSpeedGraph, state),
            TransferSpeedGraphPaletteColor.Grid => _redirect.GetElementColor3(PaletteElement.TransferSpeedGraph, state),
            TransferSpeedGraphPaletteColor.Border => _redirect.GetElementColor4(PaletteElement.TransferSpeedGraph, state),
            _ => _redirect.GetElementColor5(PaletteElement.TransferSpeedGraph, state)
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
    internal PaletteTransferSpeedGraphColorRedirect(PaletteRedirect redirect, TransferSpeedGraphPaletteColor color, NeedPaintHandler? needPaint)
        : base(null, needPaint)
    {
        _inherit = new PaletteTransferSpeedGraphColorInheritRedirect(redirect, color);
        SetInherit(_inherit);
    }

    /// <summary>
    /// Update the redirector with a new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    internal void SetRedirector(PaletteRedirect redirect) => _inherit.SetRedirector(redirect);
}

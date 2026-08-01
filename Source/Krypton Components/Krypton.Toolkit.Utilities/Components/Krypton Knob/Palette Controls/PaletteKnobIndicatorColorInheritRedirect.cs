#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Inherit knob indicator colours from the palette <see cref="PaletteBackStyle.PanelAlternate"/> back.
/// </summary>
internal class PaletteKnobIndicatorColorInheritRedirect : PaletteElementColorInherit
{
    #region Instance Fields
    private PaletteRedirect _redirect;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteKnobIndicatorColorInheritRedirect"/> class.
    /// </summary>
    /// <param name="redirect">Source for inheriting values.</param>
    public PaletteKnobIndicatorColorInheritRedirect(PaletteRedirect redirect) => _redirect = redirect;
    #endregion

    #region Public
    /// <summary>
    /// Update the redirector with a new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public void SetRedirector(PaletteRedirect redirect) => _redirect = redirect;
    #endregion

    #region IPaletteElementColor
    /// <inheritdoc />
    public override Color GetElementColor1(PaletteState state) =>
        _redirect.GetBackColor1(PaletteBackStyle.PanelAlternate, state);

    /// <inheritdoc />
    public override Color GetElementColor2(PaletteState state) =>
        _redirect.GetBackColor2(PaletteBackStyle.PanelAlternate, state);

    /// <inheritdoc />
    public override Color GetElementColor3(PaletteState state) =>
        _redirect.GetBorderColor1(PaletteBorderStyle.ControlClient, state);

    /// <inheritdoc />
    public override Color GetElementColor4(PaletteState state) => GetElementColor1(state);

    /// <inheritdoc />
    public override Color GetElementColor5(PaletteState state) => GetElementColor1(state);
    #endregion
}

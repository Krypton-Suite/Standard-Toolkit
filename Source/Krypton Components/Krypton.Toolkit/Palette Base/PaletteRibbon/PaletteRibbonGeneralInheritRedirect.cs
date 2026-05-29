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
/// Provide inheritance of palette ribbon general properties from source redirector.
/// </summary>
public class PaletteRibbonGeneralInheritRedirect : PaletteRibbonGeneralInherit
{
    #region Instance Fields
    private PaletteRedirect _redirect;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRibbonGeneralInheritRedirect class.
    /// </summary>
    /// <param name="redirect">Source for inherit requests.</param>
    public PaletteRibbonGeneralInheritRedirect([DisallowNull] PaletteRedirect redirect)
    {
        Debug.Assert(redirect != null);
        _redirect = redirect!;
    }
    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public void SetRedirector(PaletteRedirect redirect) => _redirect = redirect;
    #endregion

    #region IPaletteRibbon
    /// <summary>
    /// Gets access to ribbon shape.
    /// </summary>
    public override PaletteRibbonShape GetRibbonShape() => _redirect.GetRibbonShape();

    /// <summary>
    /// Gets the text alignment for the ribbon context text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override PaletteRelativeAlign GetRibbonContextTextAlign(PaletteState state) => _redirect.GetRibbonContextTextAlign(state);

    /// <summary>
    /// Gets the font for the ribbon context text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font GetRibbonContextTextFont(PaletteState state) => _redirect.GetRibbonContextTextFont(state);

    /// <summary>
    /// Gets the color for the ribbon context text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Color GetRibbonContextTextColor(PaletteState state) => _redirect.GetRibbonContextTextColor(state);

    /// <summary>
    /// Gets the dark disabled color used for ribbon glyphs.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonDisabledDark(PaletteState state) => _redirect.GetRibbonDisabledDark(state);

    /// <summary>
    /// Gets the light disabled color used for ribbon glyphs.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonDisabledLight(PaletteState state) => _redirect.GetRibbonDisabledLight(state);

    /// <summary>
    /// Gets the color for the drop arrow light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonDropArrowLight(PaletteState state) => _redirect.GetRibbonDropArrowLight(state);

    /// <summary>
    /// Gets the color for the drop arrow dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonDropArrowDark(PaletteState state) => _redirect.GetRibbonDropArrowDark(state);

    /// <summary>
    /// Gets the color for the dialog launcher dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonGroupDialogDark(PaletteState state) => _redirect.GetRibbonGroupDialogDark(state);

    /// <summary>
    /// Gets the color for the dialog launcher light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonGroupDialogLight(PaletteState state) => _redirect.GetRibbonGroupDialogLight(state);

    /// <summary>
    /// Gets the color for the group separator dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonGroupSeparatorDark(PaletteState state) => _redirect.GetRibbonGroupSeparatorDark(state);

    /// <summary>
    /// Gets the color for the group separator light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonGroupSeparatorLight(PaletteState state) => _redirect.GetRibbonGroupSeparatorLight(state);

    /// <summary>
    /// Gets the color for the minimize bar dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonMinimizeBarDark(PaletteState state) => _redirect.GetRibbonMinimizeBarDark(state);

    /// <summary>
    /// Gets the color for the minimize bar light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonMinimizeBarLight(PaletteState state) => _redirect.GetRibbonMinimizeBarLight(state);

    /// <summary>
    /// Gets the gradient dark rafting color for the tab background.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state) => _redirect.GetRibbonTabRowBackgroundGradientRaftingDark(state);

    /// <summary>
    /// Gets the gradient light rafting color for the tab background.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state) => _redirect.GetRibbonTabRowBackgroundGradientRaftingLight(state);

    /// <summary>
    /// Gets the solid color for the tab background.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) => _redirect.GetRibbonTabRowBackgroundSolidColor(state);

    /// <summary>
    /// Gets the color for the tab separator.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonTabSeparatorColor(PaletteState state) => _redirect.GetRibbonTabSeparatorColor(state);

    /// <summary>
    /// Gets the color for the tab context separators.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonTabSeparatorContextColor(PaletteState state) => _redirect.GetRibbonTabSeparatorContextColor(state);

    /// <summary>
    /// Gets the font for the ribbon text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font GetRibbonTextFont(PaletteState state) => _redirect.GetRibbonTextFont(state);

    /// <summary>
    /// Gets the rendering hint for the ribbon font.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public override PaletteTextHint GetRibbonTextHint(PaletteState state) => _redirect.GetRibbonTextHint(state);

    /// <summary>
    /// Gets the color for the extra QAT button dark content color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonQATButtonDark(PaletteState state) => _redirect.GetRibbonQATButtonDark(state);

    /// <summary>
    /// Gets the color for the extra QAT button light content color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonQATButtonLight(PaletteState state) => _redirect.GetRibbonQATButtonLight(state);
		
    /// <inheritdoc />
    public override Color GetRibbonTabRowGradientColor1(PaletteState state) =>
        _redirect.GetRibbonTabRowGradientColor1(state);

    /// <summary>Gets the ribbon tab row gradient rafting angle.</summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>The gradient rafting angle.</returns>
    public override float GetRibbonTabRowGradientRaftingAngle(PaletteState state) =>
        _redirect.GetRibbonTabRowGradientRaftingAngle(state);

    #endregion
}
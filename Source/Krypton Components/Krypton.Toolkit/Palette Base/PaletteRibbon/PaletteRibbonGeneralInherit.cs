#region BSD License
/*
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provide inheritance of palette ribbon general properties.
/// </summary>
public abstract class PaletteRibbonGeneralInherit : GlobalId,
    IPaletteRibbonGeneral
{
    #region IPaletteRibbon
    /// <summary>
    /// Gets the ribbon shape that should be used.
    /// </summary>
    /// <returns>Ribbon shape value.</returns>
    public abstract PaletteRibbonShape GetRibbonShape();

    /// <summary>
    /// Gets the text alignment for the ribbon context text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public abstract PaletteRelativeAlign GetRibbonContextTextAlign(PaletteState state);

    /// <summary>
    /// Gets the font for the ribbon context text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public abstract Font GetRibbonContextTextFont(PaletteState state);

    /// <summary>
    /// Gets the color for the ribbon context text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public abstract Color GetRibbonContextTextColor(PaletteState state);

    /// <summary>
    /// Gets the dark disabled color used for ribbon glyphs.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonDisabledDark(PaletteState state);

    /// <summary>
    /// Gets the light disabled color used for ribbon glyphs.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonDisabledLight(PaletteState state);

    /// <summary>
    /// Gets the color for the dialog launcher dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonGroupDialogDark(PaletteState state);

    /// <summary>
    /// Gets the color for the dialog launcher light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonGroupDialogLight(PaletteState state);

    /// <summary>
    /// Gets the color for the drop arrow dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonDropArrowDark(PaletteState state);

    /// <summary>
    /// Gets the color for the drop arrow light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonDropArrowLight(PaletteState state);

    /// <summary>
    /// Gets the color for the group separator dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonGroupSeparatorDark(PaletteState state);

    /// <summary>
    /// Gets the color for the group separator light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonGroupSeparatorLight(PaletteState state);

    /// <summary>
    /// Gets the color for the minimize bar dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonMinimizeBarDark(PaletteState state);

    /// <summary>
    /// Gets the color for the minimize bar light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonMinimizeBarLight(PaletteState state);

    /// <summary>
    /// Gets the color for the tab separator.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonTabSeparatorColor(PaletteState state);

    /// <summary>
    /// Gets the color for the tab context separators.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonTabSeparatorContextColor(PaletteState state);

    /// <summary>
    /// Gets the font for the ribbon text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public abstract Font GetRibbonTextFont(PaletteState state);

    /// <summary>
    /// Gets the rendering hint for the ribbon font.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public abstract PaletteTextHint GetRibbonTextHint(PaletteState state);

    /// <summary>
    /// Gets the color for the extra QAT button dark content color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonQATButtonDark(PaletteState state);

    /// <summary>
    /// Gets the color for the extra QAT button light content color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonQATButtonLight(PaletteState state);

    /// <summary>
    /// Gets the gradient dark rafting color for the tab background.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state);

    /// <summary>
    /// Gets the gradient light rafting color for the tab background.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state);

    /// <summary>
    /// Gets the solid color for the tab background.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonTabRowBackgroundSolidColor(PaletteState state);

    /// <inheritdoc />
    public abstract Color GetRibbonTabRowGradientColor1(PaletteState state);

    /// <summary>Gets the ribbon tab row gradient rafting angle.</summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>The gradient rafting angle.</returns>
    public abstract float GetRibbonTabRowGradientRaftingAngle(PaletteState state);

    #endregion
}
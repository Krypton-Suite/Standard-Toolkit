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

internal class ContextToContent : RibbonToContent
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ContextToContent class.
    /// </summary>
    /// <param name="ribbonGeneral">Source for general ribbon settings.</param>
    public ContextToContent(PaletteRibbonGeneral ribbonGeneral)
        : base(ribbonGeneral)
    {
        OverrideTextColor = Color.Empty;
        OverrideTextHint = PaletteTextHint.Inherit;
    }
    #endregion

    #region OverrideTextColor
    /// <summary>
    /// Gets and sets the text color override.
    /// </summary>
    public Color OverrideTextColor { get; set; }

    #endregion

    #region OverrideTextHint
    /// <summary>
    /// Gets and sets the text hint.
    /// </summary>
    public PaletteTextHint OverrideTextHint { get; set; }

    #endregion

    #region IPaletteContent
    /// <summary>
    /// Gets the text trimming to use for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public override PaletteTextTrim GetContentShortTextTrim(PaletteState state) => PaletteTextTrim.Character;

    /// <summary>
    /// Gets the horizontal relative alignment of the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextH(PaletteState state) => RibbonGeneral.GetRibbonContextTextAlign(state);

    /// <summary>
    /// Gets the font for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font GetContentShortTextFont(PaletteState state) => RibbonGeneral.GetRibbonContextTextFont(state);

    /// <summary>
    /// Gets the rendering hint for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public override PaletteTextHint GetContentShortTextHint(PaletteState state) =>
        OverrideTextHint != PaletteTextHint.Inherit ? OverrideTextHint : RibbonGeneral.GetRibbonTextHint(state);

    /// <summary>
    /// Gets the first back color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor1(PaletteState state) => OverrideTextColor != Color.Empty
        ? OverrideTextColor
        : RibbonGeneral.GetRibbonContextTextColor(state);

    /// <summary>
    /// Gets the second back color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor2(PaletteState state) => OverrideTextColor != Color.Empty
        ? OverrideTextColor
        : RibbonGeneral.GetRibbonContextTextColor(state);

    /// <summary>
    /// Gets the text trimming to use for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public override PaletteTextTrim GetContentLongTextTrim(PaletteState state) => PaletteTextTrim.Character;

    /// <summary>
    /// Gets the font for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font GetContentLongTextFont(PaletteState state) => RibbonGeneral.GetRibbonContextTextFont(state);

    /// <summary>
    /// Gets the rendering hint for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public override PaletteTextHint GetContentLongTextHint(PaletteState state) =>
        OverrideTextHint != PaletteTextHint.Inherit ? OverrideTextHint : RibbonGeneral.GetRibbonTextHint(state);

    /// <summary>
    /// Gets the first back color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor1(PaletteState state) => OverrideTextColor != Color.Empty
        ? OverrideTextColor
        : RibbonGeneral.GetRibbonContextTextColor(state);

    /// <summary>
    /// Gets the second back color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor2(PaletteState state) => OverrideTextColor != Color.Empty
        ? OverrideTextColor
        : RibbonGeneral.GetRibbonContextTextColor(state);

    #endregion
}
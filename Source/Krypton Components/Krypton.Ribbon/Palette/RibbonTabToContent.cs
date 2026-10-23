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

internal class RibbonTabToContent : RibbonToContent
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the RibbonTabToContent class.
    /// </summary>
    /// <param name="ribbonGeneral">Source for general ribbon settings.</param>
    /// <param name="ribbonTabText">Source for ribbon tab settings.</param>
    public RibbonTabToContent([DisallowNull] PaletteRibbonGeneral ribbonGeneral,
        [DisallowNull] IPaletteRibbonText ribbonTabText)
        : base(ribbonGeneral)
    {
        Debug.Assert(ribbonTabText is not null);

        PaletteRibbonText = ribbonTabText ?? throw new ArgumentNullException(nameof(ribbonTabText));
    }
    #endregion
        
    #region PaletteRibbonText
    /// <summary>
    /// Gets and sets the ribbon tab palette to use.
    /// </summary>
    public IPaletteRibbonText PaletteRibbonText { get; set; }

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
    public override PaletteRelativeAlign GetContentShortTextH(PaletteState state) => PaletteRelativeAlign.Center;

    /// <summary>
    /// Gets the first back color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor1(PaletteState state) => PaletteRibbonText.GetRibbonTextColor(state);

    /// <summary>
    /// Gets the second back color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor2(PaletteState state) => PaletteRibbonText.GetRibbonTextColor(state);

    /// <summary>
    /// Gets the text trimming to use for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public override PaletteTextTrim GetContentLongTextTrim(PaletteState state) => PaletteTextTrim.Character;

    /// <summary>
    /// Gets the first back color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor1(PaletteState state) => PaletteRibbonText.GetRibbonTextColor(state);

    /// <summary>
    /// Gets the second back color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor2(PaletteState state) => PaletteRibbonText.GetRibbonTextColor(state);

    #endregion
}
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

internal class RibbonRecentDocsTitleToContent : RibbonToContent
{
    #region Static Fields
    private static readonly Padding _titlePadding = new Padding(5, 3, 5, 1);
    #endregion

    #region Instance Fields
    private readonly IPaletteRibbonText _ribbonRecentTitleText;
    private Font? _shortTextFont;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the RibbonRecentDocsToContent class.
    /// </summary>
    /// <param name="ribbonGeneral">Source for general ribbon settings.</param>
    /// <param name="ribbonRecentTitleText">Source for ribbon recent document title settings.</param>
    public RibbonRecentDocsTitleToContent([DisallowNull] PaletteRibbonGeneral ribbonGeneral,
        [DisallowNull] IPaletteRibbonText ribbonRecentTitleText)
        : base(ribbonGeneral)
    {
        Debug.Assert(ribbonRecentTitleText is not null);

        _ribbonRecentTitleText = ribbonRecentTitleText ?? throw new ArgumentNullException(nameof(ribbonRecentTitleText));
    }

    /// <summary>
    /// Remove any cached resources.
    /// </summary>
    public void Dispose() => _shortTextFont?.Dispose();
    #endregion

    #region IPaletteContent
    /// <summary>
    /// Gets the horizontal relative alignment of the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextH(PaletteState state) => PaletteRelativeAlign.Near;

    /// <summary>
    /// Gets the first back color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor1(PaletteState state) => _ribbonRecentTitleText.GetRibbonTextColor(state);

    /// <summary>
    /// Gets the second back color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor2(PaletteState state) => _ribbonRecentTitleText.GetRibbonTextColor(state);

    /// <summary>
    /// Gets the first back color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor1(PaletteState state) => _ribbonRecentTitleText.GetRibbonTextColor(state);

    /// <summary>
    /// Gets the second back color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor2(PaletteState state) => _ribbonRecentTitleText.GetRibbonTextColor(state);

    /// <summary>
    /// Gets the padding between the border and content drawing.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Padding value.</returns>
    public override Padding GetBorderContentPadding(KryptonForm? owningForm, PaletteState state) => _titlePadding;

    /// <summary>
    /// Gets the font for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font GetContentShortTextFont(PaletteState state)
    {
        _shortTextFont?.Dispose();

        _shortTextFont = new Font(RibbonGeneral.GetRibbonTextFont(state), FontStyle.Bold);
        return _shortTextFont;
    }
    #endregion
}
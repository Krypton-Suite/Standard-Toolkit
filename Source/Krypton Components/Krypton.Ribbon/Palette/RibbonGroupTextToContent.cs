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

internal class RibbonGroupTextToContent : RibbonToContent
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the RibbonGroupTextToContent class.
    /// </summary>
    /// <param name="ribbonGeneral">Source for general ribbon settings.</param>
    /// <param name="ribbonGroupText">Source for ribbon group settings.</param>
    public RibbonGroupTextToContent([DisallowNull] PaletteRibbonGeneral ribbonGeneral,
        [DisallowNull] IPaletteRibbonText ribbonGroupText)
        : base(ribbonGeneral)
    {
        Debug.Assert(ribbonGroupText is not null);

        PaletteRibbonGroup = ribbonGroupText ?? throw new ArgumentNullException(nameof(ribbonGroupText));
    }
    #endregion

    #region PaletteRibbonGroup
    /// <summary>
    /// Gets and sets the ribbon group palette to use.
    /// </summary>
    public IPaletteRibbonText PaletteRibbonGroup { get; set; }

    #endregion

    #region IPaletteContent
    /// <summary>
    /// Gets the first back color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor1(PaletteState state) => PaletteRibbonGroup.GetRibbonTextColor(state);

    /// <summary>
    /// Gets the second back color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor2(PaletteState state) => PaletteRibbonGroup.GetRibbonTextColor(state);

    /// <summary>
    /// Gets the first back color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor1(PaletteState state) => PaletteRibbonGroup.GetRibbonTextColor(state);

    /// <summary>
    /// Gets the second back color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor2(PaletteState state) => PaletteRibbonGroup.GetRibbonTextColor(state);

    #endregion
}
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
/// Custom type converter so that PaletteButtonStyle values appear as neat text at design time.
/// </summary>
internal class PaletteButtonStyleConverter : StringLookupConverter<PaletteButtonStyle>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<PaletteButtonStyle, string> _pairs = new BiDictionary<PaletteButtonStyle, string>(
        new Dictionary<PaletteButtonStyle, string>
        {
            {PaletteButtonStyle.Inherit, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_STYLE_INHERIT},
            {PaletteButtonStyle.Standalone, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_STYLE_STANDALONE},
            {PaletteButtonStyle.Alternate, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_STYLE_ALTERNATE},
            {PaletteButtonStyle.LowProfile, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_STYLE_LOW_PROFILE},
            {PaletteButtonStyle.BreadCrumb, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_STYLE_BREAD_CRUMB},
            {PaletteButtonStyle.Cluster, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_STYLE_CLUSTER},
            {PaletteButtonStyle.NavigatorStack, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_STYLE_NAVIGATOR_STACK},
            {PaletteButtonStyle.NavigatorOverflow, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_STYLE_NAVIGATOR_OVERFLOW},
            {PaletteButtonStyle.NavigatorMini, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_STYLE_NAVIGATOR_MINI},
            {PaletteButtonStyle.InputControl, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_STYLE_INPUT_CONTROL},
            {PaletteButtonStyle.ListItem, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_STYLE_LIST_ITEM},
            {PaletteButtonStyle.Form, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_STYLE_FORM},
            {PaletteButtonStyle.FormClose, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_STYLE_FORM_CLOSE},
            {PaletteButtonStyle.ButtonSpec, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_STYLE_BUTTON_SPEC},
            {PaletteButtonStyle.Command, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_STYLE_COMMAND},
            {PaletteButtonStyle.Custom1, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_STYLE_CUSTOM1},
            {PaletteButtonStyle.Custom2, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_STYLE_CUSTOM2},
            {PaletteButtonStyle.Custom3, DesignTimeUtilities.DEFAULT_PALETTE_BUTTON_STYLE_CUSTOM3}
        });

    #endregion

    #region Protected
    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<PaletteButtonStyle /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    protected override IReadOnlyDictionary<string /*Display*/, PaletteButtonStyle /*Enum*/ > PairsStringToEnum => _pairs.SecondToFirst;

    #endregion
}
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
/// Custom type converter so that TabBorderStyle values appear as neat text at design time.
/// </summary>
internal class TabBorderStyleConverter : StringLookupConverter<TabBorderStyle>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<TabBorderStyle, string> _pairs = new BiDictionary<TabBorderStyle, string>(
        new Dictionary<TabBorderStyle, string>
        {
            {TabBorderStyle.OneNote, DesignTimeUtilities.DEFAULT_TAB_BORDER_STYLE_ONE_NOTE},
            {TabBorderStyle.SquareEqualSmall, DesignTimeUtilities.DEFAULT_TAB_BORDER_STYLE_SQUARE_EQUAL_SMALL},
            {TabBorderStyle.SquareEqualMedium, DesignTimeUtilities.DEFAULT_TAB_BORDER_STYLE_SQUARE_EQUAL_MEDIUM},
            {TabBorderStyle.SquareEqualLarge, DesignTimeUtilities.DEFAULT_TAB_BORDER_STYLE_SQUARE_EQUAL_LARGE},
            {TabBorderStyle.SquareOutsizeSmall, DesignTimeUtilities.DEFAULT_TAB_BORDER_STYLE_SQUARE_OUTSIZE_SMALL},
            {TabBorderStyle.SquareOutsizeMedium, DesignTimeUtilities.DEFAULT_TAB_BORDER_STYLE_SQUARE_OUTSIZE_MEDIUM},
            {TabBorderStyle.SquareOutsizeLarge, DesignTimeUtilities.DEFAULT_TAB_BORDER_STYLE_SQUARE_OUTSIZE_LARGE},
            {TabBorderStyle.RoundedEqualSmall, DesignTimeUtilities.DEFAULT_TAB_BORDER_STYLE_ROUNDED_EQUAL_SMALL},
            {TabBorderStyle.RoundedEqualMedium, DesignTimeUtilities.DEFAULT_TAB_BORDER_STYLE_ROUNDED_EQUAL_MEDIUM},
            {TabBorderStyle.RoundedEqualLarge, DesignTimeUtilities.DEFAULT_TAB_BORDER_STYLE_ROUNDED_EQUAL_LARGE},
            {TabBorderStyle.RoundedOutsizeSmall, DesignTimeUtilities.DEFAULT_TAB_BORDER_STYLE_ROUNDED_OUTSIZE_SMALL},
            {TabBorderStyle.RoundedOutsizeMedium, DesignTimeUtilities.DEFAULT_TAB_BORDER_STYLE_ROUNDED_OUTSIZE_MEDIUM},
            {TabBorderStyle.RoundedOutsizeLarge, DesignTimeUtilities.DEFAULT_TAB_BORDER_STYLE_ROUNDED_OUTSIZE_LARGE},
            {TabBorderStyle.SlantEqualNear, DesignTimeUtilities.DEFAULT_TAB_BORDER_STYLE_SLANT_EQUAL_NEAR},
            {TabBorderStyle.SlantEqualFar, DesignTimeUtilities.DEFAULT_TAB_BORDER_STYLE_SLANT_EQUAL_FAR},
            {TabBorderStyle.SlantEqualBoth, DesignTimeUtilities.DEFAULT_TAB_BORDER_STYLE_SLANT_EQUAL_BOTH},
            {TabBorderStyle.SlantOutsizeNear, DesignTimeUtilities.DEFAULT_TAB_BORDER_STYLE_SLANT_OUTSIZE_NEAR},
            {TabBorderStyle.SlantOutsizeFar, DesignTimeUtilities.DEFAULT_TAB_BORDER_STYLE_SLANT_OUTSIZE_FAR},
            {TabBorderStyle.SlantOutsizeBoth, DesignTimeUtilities.DEFAULT_TAB_BORDER_STYLE_SLANT_OUTSIZE_BOTH},
            {TabBorderStyle.SmoothEqual, DesignTimeUtilities.DEFAULT_TAB_BORDER_STYLE_SMOOTH_EQUAL},
            {TabBorderStyle.SmoothOutsize, DesignTimeUtilities.DEFAULT_TAB_BORDER_STYLE_SMOOTH_OUTSIZE},
            {TabBorderStyle.DockEqual, DesignTimeUtilities.DEFAULT_TAB_BORDER_STYLE_DOCK_EQUAL},
            {TabBorderStyle.DockOutsize, DesignTimeUtilities.DEFAULT_TAB_BORDER_STYLE_DOCK_OUTSIZE}
        });

    #endregion  

    #region Protected

    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<TabBorderStyle /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    protected override IReadOnlyDictionary<string /*Display*/, TabBorderStyle /*Enum*/ > PairsStringToEnum => _pairs.SecondToFirst;

    #endregion
}
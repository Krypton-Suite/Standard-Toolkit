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
/// Storage for color button string properties.
/// </summary>
public class PaletteColorButtonStrings : Storage
{
    #region Static Fields

    private const string DEFAULT_MORE_COLORS = "&More Colors...";
    private const string DEFAULT_NO_COLOR = "&No Color";
    private const string DEFAULT_RECENT_COLORS = "Recent Colors";
    private const string DEFAULT_STANDARD_COLORS = "Standard Colors";
    private const string DEFAULT_THEME_COLORS = "Theme Colors";

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteColorButtonStrings class.
    /// </summary>
    public PaletteColorButtonStrings()
    {
        // Default values
        MoreColors = DEFAULT_MORE_COLORS;
        NoColor = DEFAULT_NO_COLOR;
        RecentColors = DEFAULT_RECENT_COLORS;
        StandardColors = DEFAULT_STANDARD_COLORS;
        ThemeColors = DEFAULT_THEME_COLORS;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => MoreColors.Equals(DEFAULT_MORE_COLORS) &&
                                      NoColor.Equals(DEFAULT_NO_COLOR) &&
                                      RecentColors.Equals(DEFAULT_RECENT_COLORS) &&
                                      StandardColors.Equals(DEFAULT_STANDARD_COLORS) &&
                                      ThemeColors.Equals(DEFAULT_THEME_COLORS);

    #endregion

    #region MoreColors
    /// <summary>
    /// Gets and sets the menu string for a 'more colors' entry.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Menu string for a 'more colors' entry.")]
    [DefaultValue("&More Colors...")]
    [RefreshProperties(RefreshProperties.All)]
    public string MoreColors { get; set; }

    #endregion

    #region NoColor
    /// <summary>
    /// Gets and sets the menu string for a 'no color' entry.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Menu string for a 'no color' entry.")]
    [DefaultValue("&No Color")]
    [RefreshProperties(RefreshProperties.All)]
    public string NoColor { get; set; }

    #endregion

    #region RecentColors
    /// <summary>
    /// Gets and sets the title for the recent colors section of the color button menu.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Title for recent colors section of the color button menu.")]
    [DefaultValue("Recent Colors")]
    [RefreshProperties(RefreshProperties.All)]
    public string RecentColors { get; set; }

    #endregion

    #region StandardColors
    /// <summary>
    /// Gets and sets the title for the standard colors section of the application menu.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Title for standard colors section of the color button menu.")]
    [DefaultValue("Standard Colors")]
    [RefreshProperties(RefreshProperties.All)]
    public string StandardColors { get; set; }

    #endregion

    #region ThemeColors
    /// <summary>
    /// Gets and sets the title for the theme colors section of the application menu.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Title for theme colors section of the color button menu.")]
    [DefaultValue("Theme Colors")]
    [RefreshProperties(RefreshProperties.All)]
    public string ThemeColors { get; set; }

    #endregion
}
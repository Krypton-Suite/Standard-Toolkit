#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonMiscellaneousThemeStrings : GlobalId
{
    #region Static Strings

    private const string DEFAULT_THEME_BROWSER_WINDOW_TITLE = @"Select a Theme";
    private const string DEFAULT_THEME_BROWSER_DESCRIPTION = @"Select a theme from the list below:";
    private const string DEFAULT_IMPORT_THEME_TEXT = @"I&mport...";
    private const string DEFAULT_SILENT_TEXT = @"&Silent";
    private const string DEFAULT_UPGRADE_TEXT = @"Up&grade";

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonMiscellaneousThemeStrings" /> class.</summary>
    public KryptonMiscellaneousThemeStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    /// <summary>Gets or sets the theme browser window title.</summary>
    /// <value>The theme browser window title.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The theme browser window title.")]
    [DefaultValue(DEFAULT_THEME_BROWSER_WINDOW_TITLE)]
    public string ThemeBrowserWindowTitle { get; set; }

    /// <summary>Gets or sets the theme browser description.</summary>
    /// <value>The theme browser description.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The theme browser description text.")]
    [DefaultValue(DEFAULT_THEME_BROWSER_DESCRIPTION)]
    public string ThemeBrowserDescription { get; set; }

    /// <summary>Gets or sets the import theme text.</summary>
    /// <value>The import theme text.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The import theme text.")]
    [DefaultValue(DEFAULT_IMPORT_THEME_TEXT)]
    public string Import { get; set; }

    /// <summary>Gets or sets the silent text.</summary>
    /// <value>The silent text.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The silent text.")]
    [DefaultValue(DEFAULT_SILENT_TEXT)]
    public string Silent { get; set; }

    /// <summary>Gets or sets the upgrade text.</summary>
    /// <value>The upgrade text.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The upgrade text.")]
    [DefaultValue(DEFAULT_UPGRADE_TEXT)]
    public string Upgrade { get; set; }

    #endregion

    #region Implementation

    [Browsable(false)]
    public bool IsDefault => ThemeBrowserDescription.Equals(DEFAULT_THEME_BROWSER_DESCRIPTION) &&
                             ThemeBrowserWindowTitle.Equals(DEFAULT_THEME_BROWSER_WINDOW_TITLE) &&
                             Import.Equals(DEFAULT_IMPORT_THEME_TEXT) &&
                             Silent.Equals(DEFAULT_SILENT_TEXT) &&
                             Upgrade.Equals(DEFAULT_UPGRADE_TEXT);

    public void Reset()
    {
        ThemeBrowserDescription = DEFAULT_THEME_BROWSER_DESCRIPTION;

        ThemeBrowserWindowTitle = DEFAULT_THEME_BROWSER_WINDOW_TITLE;

        Import = DEFAULT_IMPORT_THEME_TEXT;

        Silent = DEFAULT_SILENT_TEXT;

        Upgrade = DEFAULT_UPGRADE_TEXT;
    }

    #endregion
}
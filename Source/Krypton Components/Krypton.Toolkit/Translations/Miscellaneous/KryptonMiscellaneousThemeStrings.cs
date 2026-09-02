#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2023 - 2026. All rights reserved.
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
    private const string DEFAULT_THEME_FALLBACK_WARNING_TITLE = @"Theme Fallback Warning";
    private const string DEFAULT_THEME_FALLBACK_WARNING_MESSAGE = @"The requested theme '{0}' ('{1}') requires the 'Krypton.Themes' assembly ('Krypton.Themes.dll'), which is not loaded or could not be found in the application directory.\nThe theme has reverted to '{2}' ('{3}').\nPlease install the 'Krypton.Standard.Toolkit' package from NuGet to continue using this theme.";
    private const string DEFAULT_LEGACY_XML_UPGRADE_TITLE = @"Legacy XML palette";
    private const string DEFAULT_LEGACY_XML_UPGRADE_MESSAGE =
        @"'{0}' uses the legacy .xml palette format. Prefer .kpalx, which is the same XML document with the dedicated palette extension. Support for .xml palette files may be removed in a future release.

{1}: Upgrade to .kpalx and apply the theme. The original .xml file is left in place.
{2}: Apply this .xml file without upgrading.
{3}: Do not apply the theme.";

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

    /// <summary>Gets or sets the theme fallback warning dialog title.</summary>
    /// <value>The theme fallback warning dialog title.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The theme fallback warning dialog title.")]
    [DefaultValue(DEFAULT_THEME_FALLBACK_WARNING_TITLE)]
    public string ThemeFallbackWarningTitle { get; set; }

    /// <summary>Gets or sets the theme fallback warning message format template.</summary>
    /// <value>The theme fallback warning message format template.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The theme fallback warning message format template.")]
    [DefaultValue(DEFAULT_THEME_FALLBACK_WARNING_MESSAGE)]
    public string ThemeFallbackWarningMessage { get; set; }

    /// <summary>Gets or sets the title for the legacy <c>.xml</c> palette upgrade warning.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Title for the warning shown when loading a legacy .xml palette file.")]
    [DefaultValue(DEFAULT_LEGACY_XML_UPGRADE_TITLE)]
    public string LegacyXmlUpgradeTitle { get; set; }

    /// <summary>
    /// Gets or sets the warning shown when loading a legacy <c>.xml</c> palette.
    /// Format items: <c>{0}</c> file name, <c>{1}</c> Yes, <c>{2}</c> No, <c>{3}</c> Cancel
    /// (from <see cref="GeneralToolkitStrings"/>).
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Warning when loading a legacy .xml palette. {0}=file name, {1}=Yes, {2}=No, {3}=Cancel.")]
    [DefaultValue(DEFAULT_LEGACY_XML_UPGRADE_MESSAGE)]
    public string LegacyXmlUpgradeMessage { get; set; }

    #endregion

    #region Implementation

    [Browsable(false)]
    public bool IsDefault => ThemeBrowserDescription.Equals(DEFAULT_THEME_BROWSER_DESCRIPTION) &&
                             ThemeBrowserWindowTitle.Equals(DEFAULT_THEME_BROWSER_WINDOW_TITLE) &&
                             Import.Equals(DEFAULT_IMPORT_THEME_TEXT) &&
                             Silent.Equals(DEFAULT_SILENT_TEXT) &&
                             Upgrade.Equals(DEFAULT_UPGRADE_TEXT) &&
                             ThemeFallbackWarningTitle.Equals(DEFAULT_THEME_FALLBACK_WARNING_TITLE) &&
                             ThemeFallbackWarningMessage.Equals(DEFAULT_THEME_FALLBACK_WARNING_MESSAGE) &&
                             LegacyXmlUpgradeTitle.Equals(DEFAULT_LEGACY_XML_UPGRADE_TITLE) &&
                             LegacyXmlUpgradeMessage.Equals(DEFAULT_LEGACY_XML_UPGRADE_MESSAGE);

    public void Reset()
    {
        ThemeBrowserDescription = DEFAULT_THEME_BROWSER_DESCRIPTION;

        ThemeBrowserWindowTitle = DEFAULT_THEME_BROWSER_WINDOW_TITLE;

        Import = DEFAULT_IMPORT_THEME_TEXT;

        Silent = DEFAULT_SILENT_TEXT;

        Upgrade = DEFAULT_UPGRADE_TEXT;

        ThemeFallbackWarningTitle = DEFAULT_THEME_FALLBACK_WARNING_TITLE;

        ThemeFallbackWarningMessage = DEFAULT_THEME_FALLBACK_WARNING_MESSAGE;

        LegacyXmlUpgradeTitle = DEFAULT_LEGACY_XML_UPGRADE_TITLE;

        LegacyXmlUpgradeMessage = DEFAULT_LEGACY_XML_UPGRADE_MESSAGE;
    }

    #endregion
}
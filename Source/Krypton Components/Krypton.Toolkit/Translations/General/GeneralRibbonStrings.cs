#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Exposes a general set of strings that are used within the Krypton Ribbon, and are localisable.</summary>
/// <seealso cref="GlobalId" />
[TypeConverter(typeof(ExpandableObjectConverter))]
public class GeneralRibbonStrings : GlobalId
{
    #region Static Values

    private const string DEFAULT_APPLICATION_BUTTON_TEXT = @"File";
    private const string DEFAULT_APPLICATION_BUTTON_KEY_TIP = @"F";
    private const string DEFAULT_CUSTOMIZE_QUICK_ACCESS_TOOLBAR = @"Customize Quick Access Toolbar";
    private const string DEFAULT_MINIMIZE = @"Mi&nimize the Ribbon";
    private const string DEFAULT_MORE_COLORS = @"&More Colors...";
    private const string DEFAULT_NO_COLOR = @"&No Color";
    private const string DEFAULT_RECENT_DOCUMENTS = @"Recent Documents";
    private const string DEFAULT_RECENT_COLORS = @"Recent Colors";
    private const string DEFAULT_SHOW_QAT_ABOVE_RIBBON = @"&Show Quick Access Toolbar Above the Ribbon";
    private const string DEFAULT_SHOW_QAT_BELOW_RIBBON = @"&Show Quick Access Toolbar Below the Ribbon";
    private const string DEFAULT_SHOW_ABOVE_RIBBON = @"&Show Above the Ribbon";
    private const string DEFAULT_SHOW_BELOW_RIBBON = @"&Show Below the Ribbon";
    private const string DEFAULT_STANDARD_COLORS = @"Standard Colors";
    private const string DEFAULT_THEME_COLORS = @"Theme Colors";

    #endregion

    #region Instance Fields

    private string _appButtonKeyTip;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="GeneralRibbonStrings" /> class.</summary>
    public GeneralRibbonStrings()
    {
        Reset();
    }

    /// <summary>Converts to string.</summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsDefault => AppButtonKeyTip.Equals(DEFAULT_APPLICATION_BUTTON_KEY_TIP) &&
                             CustomizeQuickAccessToolbar.Equals(DEFAULT_CUSTOMIZE_QUICK_ACCESS_TOOLBAR) &&
                             Minimize.Equals(DEFAULT_MINIMIZE) &&
                             MoreColors.Equals(DEFAULT_MORE_COLORS) &&
                             NoColor.Equals(DEFAULT_NO_COLOR) &&
                             RecentDocuments.Equals(DEFAULT_RECENT_DOCUMENTS) &&
                             RecentColors.Equals(DEFAULT_RECENT_COLORS) &&
                             ShowAboveRibbon.Equals(DEFAULT_SHOW_ABOVE_RIBBON) &&
                             ShowBelowRibbon.Equals(DEFAULT_SHOW_BELOW_RIBBON) &&
                             ShowQATAboveRibbon.Equals(DEFAULT_SHOW_QAT_ABOVE_RIBBON) &&
                             ShowQATBelowRibbon.Equals(DEFAULT_SHOW_QAT_BELOW_RIBBON) &&
                             StandardColors.Equals(DEFAULT_STANDARD_COLORS) &&
                             ThemeColors.Equals(DEFAULT_THEME_COLORS) &&
                             AppButtonText.Equals(DEFAULT_APPLICATION_BUTTON_TEXT);

    #endregion

    #region Public

    /// <summary>
    /// Gets and sets the application button key tip string.
    /// </summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"Application button key tip string.")]
    [DefaultValue(DEFAULT_APPLICATION_BUTTON_KEY_TIP)]
    [RefreshProperties(RefreshProperties.All)]
    public string AppButtonKeyTip
    {
        get => _appButtonKeyTip;

        set
        {
            // We only allow uppercase strings of minimum 1 character length
            if (!string.IsNullOrEmpty(value))
            {
                _appButtonKeyTip = value.ToUpper();
            }
        }
    }

    /// <summary>
    /// Gets and sets the heading for the quick access toolbar menu.
    /// </summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"Heading for quick access toolbar menu.")]
    [DefaultValue(DEFAULT_CUSTOMIZE_QUICK_ACCESS_TOOLBAR)]
    [RefreshProperties(RefreshProperties.All)]
    public string CustomizeQuickAccessToolbar { get; set; }

    /// <summary>
    /// Gets and sets the menu string for minimizing the ribbon option.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Menu string for minimizing the ribbon option.")]
    [DefaultValue(DEFAULT_MINIMIZE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Minimize { get; set; }

    /// <summary>
    /// Gets and sets the menu string for a 'more colors' entry.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Menu string for a 'more colors' entry.")]
    [DefaultValue(DEFAULT_MORE_COLORS)]
    [RefreshProperties(RefreshProperties.All)]
    public string MoreColors { get; set; }

    /// <summary>
    /// Gets and sets the menu string for a 'no color' entry.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Menu string for a 'no color' entry.")]
    [DefaultValue(DEFAULT_NO_COLOR)]
    [RefreshProperties(RefreshProperties.All)]
    public string NoColor { get; set; }

    /// <summary>
    /// Gets and sets the title for the recent documents section of the application menu.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Title for recent documents section of the application menu.")]
    [DefaultValue(DEFAULT_RECENT_DOCUMENTS)]
    [RefreshProperties(RefreshProperties.All)]
    public string RecentDocuments { get; set; }

    /// <summary>
    /// Gets and sets the title for the recent colors section of the color button menu.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Title for recent colors section of the color button menu.")]
    [DefaultValue(DEFAULT_RECENT_COLORS)]
    [RefreshProperties(RefreshProperties.All)]
    public string RecentColors { get; set; }

    /// <summary>
    /// Gets and sets the menu string for showing above the ribbon.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Menu string for showing above the ribbon.")]
    [DefaultValue(DEFAULT_SHOW_ABOVE_RIBBON)]
    [RefreshProperties(RefreshProperties.All)]
    public string ShowAboveRibbon { get; set; }

    /// <summary>
    /// Gets and sets the menu string for showing below the ribbon.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Menu string for showing below the ribbon.")]
    [DefaultValue(DEFAULT_SHOW_BELOW_RIBBON)]
    [RefreshProperties(RefreshProperties.All)]
    public string ShowBelowRibbon { get; set; }

    /// <summary>
    /// Gets and sets the menu string for showing QAT above the ribbon.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Menu string for showing QAT above the ribbon.")]
    [DefaultValue(DEFAULT_SHOW_QAT_ABOVE_RIBBON)]
    [RefreshProperties(RefreshProperties.All)]
    public string ShowQATAboveRibbon { get; set; }

    /// <summary>
    /// Gets and sets the menu string for showing QAT below the ribbon.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Menu string for showing QAT below the ribbon.")]
    [DefaultValue(DEFAULT_SHOW_QAT_BELOW_RIBBON)]
    [RefreshProperties(RefreshProperties.All)]
    public string ShowQATBelowRibbon { get; set; }

    /// <summary>
    /// Gets and sets the title for the standard colors section of the application menu.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Title for standard colors section of the color button menu.")]
    [DefaultValue(DEFAULT_STANDARD_COLORS)]
    [RefreshProperties(RefreshProperties.All)]
    public string StandardColors { get; set; }

    /// <summary>
    /// Gets and sets the title for the theme colors section of the application menu.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Title for theme colors section of the color button menu.")]
    [DefaultValue(DEFAULT_THEME_COLORS)]
    [RefreshProperties(RefreshProperties.All)]
    public string ThemeColors { get; set; }

    /// <summary>
    /// Gets and sets the button text for the app button.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Button text for the AppButton.")]
    [DefaultValue(DEFAULT_APPLICATION_BUTTON_TEXT)]
    [RefreshProperties(RefreshProperties.All)]
    public string AppButtonText { get; set; }

    #endregion

    #region Implementation

    /// <summary>Resets this instance.</summary>
    public void Reset()
    {
        AppButtonKeyTip = DEFAULT_APPLICATION_BUTTON_KEY_TIP;

        AppButtonText = DEFAULT_APPLICATION_BUTTON_TEXT;

        CustomizeQuickAccessToolbar = DEFAULT_CUSTOMIZE_QUICK_ACCESS_TOOLBAR;

        Minimize = DEFAULT_MINIMIZE;

        MoreColors = DEFAULT_MORE_COLORS;

        NoColor = DEFAULT_NO_COLOR;

        RecentColors = DEFAULT_RECENT_COLORS;

        RecentDocuments = DEFAULT_RECENT_DOCUMENTS;

        ShowAboveRibbon = DEFAULT_SHOW_ABOVE_RIBBON;

        ShowBelowRibbon = DEFAULT_SHOW_BELOW_RIBBON;

        ShowQATAboveRibbon = DEFAULT_SHOW_QAT_ABOVE_RIBBON;

        ShowQATBelowRibbon = DEFAULT_SHOW_QAT_BELOW_RIBBON;

        StandardColors = DEFAULT_STANDARD_COLORS;

        ThemeColors = DEFAULT_THEME_COLORS;
    }

    #endregion
}
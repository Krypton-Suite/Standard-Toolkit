#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Ribbon
{
    /// <summary>
    /// Storage for string related properties.
    /// </summary>
    public class RibbonStrings : Storage
    {
        #region Static Fields

        private const string DEFAULT_APP_BUTTON_KEY_TIP = "F";
        private const string DEFAULT_CUSTOMIZE_QUICK_ACCESS_TOOLBAR = "Customize Quick Access Toolbar";
        private const string DEFAULT_MINIMIZE = "Mi&nimize the Ribbon";
        private const string DEFAULT_MORE_COLORS = "&More Colors...";
        private const string DEFAULT_NO_COLOR = "&No Color";
        private const string DEFAULT_RECENT_DOCUMENTS = "Recent Documents";
        private const string DEFAULT_RECENT_COLORS = "Recent Colors";
        private const string DEFAULT_SHOW_QAT_ABOVE_RIBBON = "&Show Quick Access Toolbar Above the Ribbon";
        private const string DEFAULT_SHOW_QAT_BELOW_RIBBON = "&Show Quick Access Toolbar Below the Ribbon";
        private const string DEFAULT_SHOW_ABOVE_RIBBON = "&Show Above the Ribbon";
        private const string DEFAULT_SHOW_BELOW_RIBBON = "&Show Below the Ribbon";
        private const string DEFAULT_STANDARD_COLORS = "Standard Colors";
        private const string DEFAULT_THEME_COLORS = "Theme Colors";

        #endregion

        #region Instance Fields
        private string _appButtonKeyTip;

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the RibbonStrings class.
        /// </summary>
        public RibbonStrings()
        {
            // Default values
            _appButtonKeyTip = DEFAULT_APP_BUTTON_KEY_TIP;
            CustomizeQuickAccessToolbar = DEFAULT_CUSTOMIZE_QUICK_ACCESS_TOOLBAR;
            Minimize = DEFAULT_MINIMIZE;
            MoreColors = DEFAULT_MORE_COLORS;
            NoColor = DEFAULT_NO_COLOR;
            RecentDocuments = DEFAULT_RECENT_DOCUMENTS;
            RecentColors = DEFAULT_RECENT_COLORS;
            ShowAboveRibbon = DEFAULT_SHOW_ABOVE_RIBBON;
            ShowBelowRibbon = DEFAULT_SHOW_BELOW_RIBBON;
            ShowQATAboveRibbon = DEFAULT_SHOW_QAT_ABOVE_RIBBON;
            ShowQATBelowRibbon = DEFAULT_SHOW_QAT_BELOW_RIBBON;
            StandardColors = DEFAULT_STANDARD_COLORS;
            ThemeColors = DEFAULT_THEME_COLORS;
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => AppButtonKeyTip.Equals(DEFAULT_APP_BUTTON_KEY_TIP) &&
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
                                          ThemeColors.Equals(DEFAULT_THEME_COLORS);

        #endregion

        #region AppButtonKeyTip
        /// <summary>
        /// Gets and sets the application button key tip string.
        /// </summary>
        [Localizable(true)]
        [Category("Values")]
        [Description("Application button key tip string.")]
        [DefaultValue("F")]
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
        #endregion

        #region CustomizeQuickAccessToolbar
        /// <summary>
        /// Gets and sets the heading for the quick access toolbar menu.
        /// </summary>
        [Localizable(true)]
        [Category("Values")]
        [Description("Heading for quick access toolbar menu.")]
        [DefaultValue("Customize Quick Access Toolbar")]
        [RefreshProperties(RefreshProperties.All)]
        public string CustomizeQuickAccessToolbar { get; set; }

        #endregion

        #region Minimize
        /// <summary>
        /// Gets and sets the menu string for minimizing the ribbon option.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Menu string for minimizing the ribbon option.")]
        [DefaultValue("Mi&nimize the Ribbon")]
        [RefreshProperties(RefreshProperties.All)]
        public string Minimize { get; set; }

        #endregion

        #region MoreColors
        /// <summary>
        /// Gets and sets the menu string for a 'more colors' entry.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Menu string for a 'more colors' entry.")]
        [DefaultValue("&More Colors...")]
        [RefreshProperties(RefreshProperties.All)]
        public string MoreColors { get; set; }

        #endregion

        #region NoColor
        /// <summary>
        /// Gets and sets the menu string for a 'no color' entry.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Menu string for a 'no color' entry.")]
        [DefaultValue("&No Color")]
        [RefreshProperties(RefreshProperties.All)]
        public string NoColor { get; set; }

        #endregion

        #region RecentDocuments     
        /// <summary>
        /// Gets and sets the title for the recent documents section of the application menu.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Title for recent documents section of the application menu.")]
        [DefaultValue("Recent Documents")]
        [RefreshProperties(RefreshProperties.All)]
        public string RecentDocuments { get; set; }

        #endregion

        #region RecentColors
        /// <summary>
        /// Gets and sets the title for the recent colors section of the color button menu.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Title for recent colors section of the color button menu.")]
        [DefaultValue("Recent Colors")]
        [RefreshProperties(RefreshProperties.All)]
        public string RecentColors { get; set; }

        #endregion

        #region ShowAboveRibbon
        /// <summary>
        /// Gets and sets the menu string for showing above the ribbon.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Menu string for showing above the ribbon.")]
        [DefaultValue("&Show Above the Ribbon")]
        [RefreshProperties(RefreshProperties.All)]
        public string ShowAboveRibbon { get; set; }

        #endregion

        #region ShowBelowRibbon
        /// <summary>
        /// Gets and sets the menu string for showing below the ribbon.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Menu string for showing below the ribbon.")]
        [DefaultValue("&Show Below the Ribbon")]
        [RefreshProperties(RefreshProperties.All)]
        public string ShowBelowRibbon { get; set; }

        #endregion

        #region ShowQATAboveRibbon
        /// <summary>
        /// Gets and sets the menu string for showing QAT above the ribbon.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Menu string for showing QAT above the ribbon.")]
        [DefaultValue("&Show Quick Access Toolbar Above the Ribbon")]
        [RefreshProperties(RefreshProperties.All)]
        public string ShowQATAboveRibbon { get; set; }

        #endregion

        #region ShowQATBelowRibbon
        /// <summary>
        /// Gets and sets the menu string for showing QAT below the ribbon.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Menu string for showing QAT below the ribbon.")]
        [DefaultValue("&Show Quick Access Toolbar Below the Ribbon")]
        [RefreshProperties(RefreshProperties.All)]
        public string ShowQATBelowRibbon { get; set; }

        #endregion

        #region StandardColors
        /// <summary>
        /// Gets and sets the title for the standard colors section of the application menu.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Title for standard colors section of the color button menu.")]
        [DefaultValue("Standard Colors")]
        [RefreshProperties(RefreshProperties.All)]
        public string StandardColors { get; set; }

        #endregion

        #region ThemeColors
        /// <summary>
        /// Gets and sets the title for the theme colors section of the application menu.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Title for theme colors section of the color button menu.")]
        [DefaultValue("Theme Colors")]
        [RefreshProperties(RefreshProperties.All)]
        public string ThemeColors { get; set; }

        #endregion
    }
}

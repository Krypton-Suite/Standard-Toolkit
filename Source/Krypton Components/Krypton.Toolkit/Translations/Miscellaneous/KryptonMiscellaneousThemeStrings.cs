#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class KryptonMiscellaneousThemeStrings : GlobalId
    {
        #region Static Strings

        private const string DEFAULT_THEME_BROWSER_WINDOW_TITLE = @"Select a Theme";
        private const string DEFAULT_THEME_BROWSER_DESCRIPTION = @"Select a theme from the list below:";
        private const string DEFAULT_IMPORT_THEME_TEXT = @"I&mport...";
        private const string DEFAULT_SILENT_TEXT = @"&Silent";
        private const string DEFAULT_UPGRADE_THEME = @"&Upgrade Theme";
        private const string DEFAULT_UPGRADE_THEME_SILENTLY = @"&Silently Upgrade Theme";

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonMiscellaneousThemeStrings" /> class.</summary>
        public KryptonMiscellaneousThemeStrings()
        {
            Reset();
        }

        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
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

        /// <summary>Gets or sets the upgrade theme text.</summary>
        /// <value>The upgrade theme text.</value>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The upgrade theme text.")]
        [DefaultValue(DEFAULT_UPGRADE_THEME)]
        public string UpgradeTheme { get; set; }

        /// <summary>Gets or sets the upgrade theme silently text.</summary>
        /// <value>The upgrade theme silently text.</value>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The upgrade theme silently text.")]
        [DefaultValue(DEFAULT_UPGRADE_THEME_SILENTLY)]
        public string SilentlyUpgradeTheme { get; set; }

        #endregion

        #region Implementation

        /// <summary>Gets a value indicating whether this instance is default.</summary>
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        [Browsable(false)]
        public bool IsDefault => ThemeBrowserDescription.Equals(DEFAULT_THEME_BROWSER_DESCRIPTION) &&
                                 ThemeBrowserWindowTitle.Equals(DEFAULT_THEME_BROWSER_WINDOW_TITLE) &&
                                 Import.Equals(DEFAULT_IMPORT_THEME_TEXT) &&
                                 Silent.Equals(DEFAULT_SILENT_TEXT) &&
                                 UpgradeTheme.Equals(DEFAULT_UPGRADE_THEME) &&
                                 SilentlyUpgradeTheme.Equals(DEFAULT_UPGRADE_THEME_SILENTLY);

        /// <summary>Resets the strings back to default.</summary>
        public void Reset()
        {
            ThemeBrowserDescription = DEFAULT_THEME_BROWSER_DESCRIPTION;

            ThemeBrowserWindowTitle = DEFAULT_THEME_BROWSER_WINDOW_TITLE;

            Import = DEFAULT_IMPORT_THEME_TEXT;

            Silent = DEFAULT_SILENT_TEXT;

            UpgradeTheme = DEFAULT_UPGRADE_THEME;

            SilentlyUpgradeTheme = DEFAULT_UPGRADE_THEME_SILENTLY;
        }

        #endregion
    }
}
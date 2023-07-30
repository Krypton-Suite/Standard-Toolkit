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
    /// <summary>Exposes a custom set of theme strings that are used within the Krypton Toolkit, and are localisable.</summary>
    /// <seealso cref="GlobalId"/>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class CustomThemeStrings : GlobalId
    {
        #region Static Strings

        private const string DEFAULT_THEME_BROWSER_WINDOW_TEXT = @"Select a Theme";

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="CustomThemeStrings" /> class.</summary>
        public CustomThemeStrings()
        {
            ResetValues();
        }

        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => !IsDefault ? "Modified" : string.Empty;

        #endregion

        #region Public

        /// <summary>Gets a value indicating whether this instance is default.</summary>
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        [Browsable(false)]
        public bool IsDefault => ThemeBrowserWindowTitleText.Equals(DEFAULT_THEME_BROWSER_WINDOW_TEXT);

        /// <summary>Resets the values.</summary>
        public void ResetValues()
        {
            ThemeBrowserWindowTitleText = DEFAULT_THEME_BROWSER_WINDOW_TEXT;
        }

        /// <summary>Gets or sets the theme browser window title text.</summary>
        /// <value>The theme browser window title text.</value>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Gets or sets the theme browser window title text.")]
        [DefaultValue(DEFAULT_THEME_BROWSER_WINDOW_TEXT)]
        public string ThemeBrowserWindowTitleText { get; set; }

        #endregion
    }
}
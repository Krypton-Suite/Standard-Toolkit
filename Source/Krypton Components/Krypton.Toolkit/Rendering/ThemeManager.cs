#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Allows the developer to easily access the entire array of supported themes for custom controls.
    /// </summary>
    public class ThemeManager
    {
        #region Theme Array        
        /// <summary>
        /// The supported themes
        /// </summary>
        [Localizable(true)]
        // TODO: This should use the list from Z:\GitHub\Krypton-Suite\Standard-Toolkit\Source\Krypton Components\Krypton.Toolkit\Converters\PaletteModeConverter.cs
        private static readonly BiDictionary<string, PaletteMode> _supportedThemes =
            new BiDictionary<string, PaletteMode>(new Dictionary<string, PaletteMode>
            {
                { KryptonLanguageManager.ModeStrings.Professional, PaletteMode.ProfessionalSystem },
                { KryptonLanguageManager.ModeStrings.Professional2003, PaletteMode.ProfessionalOffice2003 },
                { KryptonLanguageManager.ModeStrings.Office2007Blue, PaletteMode.Office2007Blue },
                { KryptonLanguageManager.ModeStrings.Office2007BlueDarkMode, PaletteMode.Office2007BlueDarkMode },
                { KryptonLanguageManager.ModeStrings.Office2007BlueLightMode, PaletteMode.Office2007BlueLightMode },
                { KryptonLanguageManager.ModeStrings.Office2007Silver, PaletteMode.Office2007Silver },
                { KryptonLanguageManager.ModeStrings.Office2007SilverDarkMode, PaletteMode.Office2007SilverDarkMode },
                { KryptonLanguageManager.ModeStrings.Office2007SilverLightMode, PaletteMode.Office2007SilverLightMode },
                { KryptonLanguageManager.ModeStrings.Office2007White, PaletteMode.Office2007White },
                { KryptonLanguageManager.ModeStrings.Office2007Black, PaletteMode.Office2007Black },
                { KryptonLanguageManager.ModeStrings.Office2007BlackDarkMode, PaletteMode.Office2007BlackDarkMode },
                { KryptonLanguageManager.ModeStrings.Office2007DarkGray, PaletteMode.Office2007DarkGray },
                { KryptonLanguageManager.ModeStrings.Office2010Blue, PaletteMode.Office2010Blue },
                { KryptonLanguageManager.ModeStrings.Office2010BlueDarkMode, PaletteMode.Office2010BlueDarkMode },
                { KryptonLanguageManager.ModeStrings.Office2010BlueLightMode, PaletteMode.Office2010BlueLightMode },
                { KryptonLanguageManager.ModeStrings.Office2010Silver, PaletteMode.Office2010Silver },
                { KryptonLanguageManager.ModeStrings.Office2010SilverDarkMode, PaletteMode.Office2010SilverDarkMode },
                { KryptonLanguageManager.ModeStrings.Office2010SilverLightMode, PaletteMode.Office2010SilverLightMode },
                { KryptonLanguageManager.ModeStrings.Office2010White, PaletteMode.Office2010White },
                { KryptonLanguageManager.ModeStrings.Office2010Black, PaletteMode.Office2010Black },
                { KryptonLanguageManager.ModeStrings.Office2010BlackDarkMode, PaletteMode.Office2010BlackDarkMode },
                { KryptonLanguageManager.ModeStrings.Office2010DarkGray, PaletteMode.Office2010DarkGray },
                { KryptonLanguageManager.ModeStrings.Office2013DarkGray, PaletteMode.Office2013DarkGray },
                //{ @"Office 2013", PaletteMode.Office2013 },
                { KryptonLanguageManager.ModeStrings.Office2013White, PaletteMode.Office2013White },
                { KryptonLanguageManager.ModeStrings.SparkleBlue, PaletteMode.SparkleBlue },
                { KryptonLanguageManager.ModeStrings.SparkleBlueDarkMode, PaletteMode.SparkleBlueDarkMode },
                { KryptonLanguageManager.ModeStrings.SparkleBlueLightMode, PaletteMode.SparkleBlueLightMode },
                { KryptonLanguageManager.ModeStrings.SparkleOrange, PaletteMode.SparkleOrange },
                { KryptonLanguageManager.ModeStrings.SparkleOrangeDarkMode, PaletteMode.SparkleOrangeDarkMode },
                { KryptonLanguageManager.ModeStrings.SparkleOrangeLightMode, PaletteMode.SparkleOrangeLightMode },
                { KryptonLanguageManager.ModeStrings.SparklePurple, PaletteMode.SparklePurple },
                { KryptonLanguageManager.ModeStrings.SparklePurpleDarkMode, PaletteMode.SparklePurpleDarkMode },
                { KryptonLanguageManager.ModeStrings.SparklePurpleLightMode, PaletteMode.SparklePurpleLightMode },
                { KryptonLanguageManager.ModeStrings.Microsoft365Blue, PaletteMode.Microsoft365Blue },
                { KryptonLanguageManager.ModeStrings.Microsoft365BlueDarkMode, PaletteMode.Microsoft365BlueDarkMode },
                { KryptonLanguageManager.ModeStrings.Microsoft365BlueLightMode, PaletteMode.Microsoft365BlueLightMode },
                { KryptonLanguageManager.ModeStrings.Microsoft365Silver, PaletteMode.Microsoft365Silver },
                {
                    KryptonLanguageManager.ModeStrings.Microsoft365SilverDarkMode,
                    PaletteMode.Microsoft365SilverDarkMode
                },
                {
                    KryptonLanguageManager.ModeStrings.Microsoft365SilverLightMode,
                    PaletteMode.Microsoft365SilverLightMode
                },
                { KryptonLanguageManager.ModeStrings.Microsoft365White, PaletteMode.Microsoft365White },
                { KryptonLanguageManager.ModeStrings.Microsoft365Black, PaletteMode.Microsoft365Black },
                { KryptonLanguageManager.ModeStrings.Microsoft365BlackDarkMode, PaletteMode.Microsoft365BlackDarkMode },
                { KryptonLanguageManager.ModeStrings.Microsoft365DarkGray, PaletteMode.Microsoft365DarkGray },
                { KryptonLanguageManager.ModeStrings.Custom, PaletteMode.Custom }
            });

        #endregion

        #region Properties        
        /// <summary>
        /// Gets the supported theme array.
        /// </summary>
        /// <value>
        /// The supported theme array.
        /// </value>
        public static ICollection<string> SupportedInternalThemeNames => _supportedThemes.GetAllFirsts();

        #endregion

        #region Implementation
        /// <summary>
        /// Applies the theme.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="manager">The manager.</param>
        private static void ApplyTheme(PaletteMode mode, KryptonManager manager) => manager.GlobalPaletteMode = mode;

        /// <summary>Gets the palette mode.</summary>
        /// <param name="manager">The manager.</param>
        /// <returns>The current <see cref="PaletteMode"/>.</returns>
        public static PaletteMode GetPaletteMode(KryptonManager manager) => ReturnPaletteMode(manager.GlobalPaletteMode);

        /// <summary>Returns the palette mode.</summary>
        /// <param name="themeName">Name of the theme.</param>
        /// <returns>The <see cref="PaletteMode"/> based on the name.</returns>
        private static PaletteMode ReturnPaletteMode(string themeName) =>
            (PaletteMode)Enum.Parse(typeof(PaletteMode), themeName);

        /// <summary>Returns the palette mode.</summary>
        /// <param name="paletteMode">The palette mode manager.</param>
        /// <returns>The selected <see cref="PaletteMode"/>.</returns>
        private static PaletteMode ReturnPaletteMode(PaletteMode paletteMode) => paletteMode;

        /// <summary>
        /// Applies the theme.
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        /// <param name="manager">The manager.</param>
        public static void ApplyTheme(string themeName, KryptonManager manager) => ApplyTheme(_supportedThemes.GetByFirst(themeName), manager);

        /// <summary>
        /// Sets the theme.
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        /// <param name="manager">The manager.</param>
        public static void SetTheme(string themeName, KryptonManager manager)
        {
            try
            {
                ApplyTheme(themeName, manager);

                ApplyGlobalTheme(manager, GetPaletteMode(manager));
            }
            catch (Exception exc)
            {
                ExceptionHandler.CaptureException(exc);
            }
        }

        /// <summary>
        /// Returns the palette mode manager as string.
        /// </summary>
        /// <param name="paletteMode">The palette mode manager.</param>
        /// <param name="manager">The manager.</param>
        /// <returns>The chosen theme as a string.</returns>
        public static string ReturnPaletteModeAsString(PaletteMode paletteMode, KryptonManager? manager)
        {
            var mode = manager?.GlobalPaletteMode ?? paletteMode;

            return _supportedThemes.GetBySecond(mode);
        }

        /// <summary>
        /// Loads the custom theme.
        /// </summary>
        /// <param name="palette">The palette.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="themeFile">A custom theme file.</param>
        /// <param name="silent">if set to <c>true</c> [silent].</param>
        public static void LoadCustomTheme(KryptonCustomPaletteBase palette, KryptonManager manager, string themeFile = "", bool silent = false)
        {
            try
            {
                //throw new ApplicationException(@"Currently not implemented correctly");

                // Declare new instances
                palette = new KryptonCustomPaletteBase();

                manager = new KryptonManager();

                // Prompt user for palette definition

                // TODO: Add silent option
                if (silent)
                {
                    if (themeFile is not ("" and ""))
                    {
                        palette.Import(themeFile, silent);
                    }
                }
                else
                {
                    palette.Import();
                }

                // Set manager
                manager.GlobalPalette = palette;

                ApplyTheme(PaletteMode.Custom, manager);
            }
            catch (Exception exc)
            {
                ExceptionHandler.CaptureException(exc);
            }
        }

        /// <summary>
        /// Returns the palette mode as string.
        /// </summary>
        /// <param name="paletteMode">The palette mode.</param>
        /// <returns></returns>
        public static string? ReturnPaletteModeAsString(PaletteMode paletteMode)
        {
            var modeConverter = new PaletteModeConverter();

            return modeConverter.ConvertToString(paletteMode);
        }

        /// <summary>
        /// Applies the global theme.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="paletteMode">The palette mode manager.</param>
        public static void ApplyGlobalTheme(KryptonManager manager, PaletteMode paletteMode)
        {
            try
            {
                manager.GlobalPaletteMode = paletteMode;
            }
            catch (Exception exc)
            {
                ExceptionHandler.CaptureException(exc);
            }
        }

        private static void AddToCollection(IList target, string[] excludes)
        {
            try
            {
                foreach (var theme in SupportedInternalThemeNames)
                {
                    if (!excludes.Any(t => theme.IndexOf(t, StringComparison.InvariantCultureIgnoreCase) > -1))
                    {
                        target.Add(theme);
                    }
                }
            }
            catch (Exception exc)
            {
                ExceptionHandler.CaptureException(exc);
            }
        }

        /// <summary>
        /// Propagates the theme selector.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="excludePartials">do not include any string containing</param>
        public static void PropagateThemeSelector(KryptonComboBox target, params string[] excludePartials) => AddToCollection(target.Items, excludePartials);

        /// <summary>
        /// Propagates the theme selector.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="excludePartials">do not include any string containing</param>
        public static void PropagateThemeSelector(KryptonListBox target, params string[] excludePartials) => AddToCollection(target.Items, excludePartials);

        /// <summary>
        /// Propagates the theme selector.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="excludePartials">do not include any string containing</param>
        public static void PropagateThemeSelector(KryptonDomainUpDown target, params string[] excludePartials) => AddToCollection(target.Items, excludePartials);

        /// <summary>
        /// Propagates the theme selector.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="excludePartials">do not include any string containing</param>
        public static void PropagateThemeSelector(ComboBox target, params string[] excludePartials) => AddToCollection(target.Items, excludePartials);

        /// <summary>
        /// Propagates the theme selector.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="excludePartials">do not include any string containing</param>
        public static void PropagateThemeSelector(DomainUpDown target, params string[] excludePartials) => AddToCollection(target.Items, excludePartials);

        /// <summary>
        /// Propagates the theme selector.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="excludePartials">do not include any string containing</param>
        public static void PropagateThemeSelector(ToolStripComboBox target, params string[] excludePartials) => AddToCollection(target.Items, excludePartials);

        /// <summary>
        /// Applies the theme manager mode.
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        /// <returns>The <see cref="PaletteMode"/> equivalent.</returns>
        public static PaletteMode GetThemeManagerMode(string themeName) => _supportedThemes.GetByFirst(themeName);
        #endregion
    }
}
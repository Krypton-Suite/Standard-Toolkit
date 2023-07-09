#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Ribbon
{
    /// <summary>
    /// Allows the developer to easily access the entire array of supported themes for ribbon controls.
    /// </summary>
    public class RibbonThemeManager
    {
        #region Theme Array        
        /// <summary>
        /// The supported themes
        /// </summary>
        // TODO: This should use the list from Z:\GitHub\Krypton-Suite\Standard-Toolkit\Source\Krypton Components\Krypton.Toolkit\Converters\PaletteModeConverter.cs
        private static readonly BiDictionary<string, PaletteMode> _supportedThemes =
            new BiDictionary<string, PaletteMode>(new Dictionary<string, PaletteMode>
            {
                { @"Professional - System", PaletteMode.ProfessionalSystem },
                { @"Professional - Office 2003", PaletteMode.ProfessionalOffice2003 },
                { @"Office 2007 - Blue", PaletteMode.Office2007Blue },
                { @"Office 2007 - Blue (Dark Mode)", PaletteMode.Office2007BlueDarkMode },
                { @"Office 2007 - Blue (Light Mode)", PaletteMode.Office2007BlueLightMode },
                { @"Office 2007 - Silver", PaletteMode.Office2007Silver },
                { @"Office 2007 - Silver (Dark Mode)", PaletteMode.Office2007SilverDarkMode },
                { @"Office 2007 - Silver (Light Mode)", PaletteMode.Office2007SilverLightMode },
                { @"Office 2007 - White", PaletteMode.Office2007White },
                { @"Office 2007 - Black", PaletteMode.Office2007Black },
                { @"Office 2007 - Black (Dark Mode)", PaletteMode.Office2007BlackDarkMode },
                { @"Office 2007 - Dark Gray", PaletteMode.Office2007DarkGray },
                { @"Office 2010 - Blue", PaletteMode.Office2010Blue },
                { @"Office 2010 - Blue (Dark Mode)", PaletteMode.Office2010BlueDarkMode },
                { @"Office 2010 - Blue (Light Mode)", PaletteMode.Office2010BlueLightMode },
                { @"Office 2010 - Silver", PaletteMode.Office2010Silver },
                { @"Office 2010 - Silver (Dark Mode)", PaletteMode.Office2010SilverDarkMode },
                { @"Office 2010 - Silver (Light Mode)", PaletteMode.Office2010SilverLightMode },
                { @"Office 2010 - White", PaletteMode.Office2010White },
                { @"Office 2010 - Black", PaletteMode.Office2010Black },
                { @"Office 2010 - Black (Dark Mode)", PaletteMode.Office2010BlackDarkMode },
                { @"Office 2010 - Dark Gray", PaletteMode.Office2010DarkGray },
                { @"Office 2013 - Dark Gray", PaletteMode.Office2013DarkGray },
                //{ @"Office 2013", PaletteMode.Office2013 },
                { @"Office 2013 - White", PaletteMode.Office2013White },
                { @"Sparkle - Blue", PaletteMode.SparkleBlue },
                { @"Sparkle - Blue (Dark Mode)", PaletteMode.SparkleBlueDarkMode },
                { @"Sparkle - Blue (Light Mode)", PaletteMode.SparkleBlueLightMode },
                { @"Sparkle - Orange", PaletteMode.SparkleOrange },
                { @"Sparkle - Orange (Dark Mode)", PaletteMode.SparkleOrangeDarkMode },
                { @"Sparkle - Orange (Light Mode)", PaletteMode.SparkleOrangeLightMode },
                { @"Sparkle - Purple", PaletteMode.SparklePurple },
                { @"Sparkle - Purple (Dark Mode)", PaletteMode.SparklePurpleDarkMode },
                { @"Sparkle - Purple (Light Mode)", PaletteMode.SparklePurpleLightMode },
                { @"Microsoft 365 - Blue", PaletteMode.Microsoft365Blue },
                { @"Microsoft 365 - Blue (Dark Mode)", PaletteMode.Microsoft365BlueDarkMode },
                { @"Microsoft 365 - Blue (Light Mode)", PaletteMode.Microsoft365BlueLightMode },
                { @"Microsoft 365 - Silver", PaletteMode.Microsoft365Silver },
                { @"Microsoft 365 - Silver (Dark Mode)", PaletteMode.Microsoft365SilverDarkMode },
                { @"Microsoft 365 - Silver (Light Mode)", PaletteMode.Microsoft365SilverLightMode },
                { @"Microsoft 365 - White", PaletteMode.Microsoft365White },
                { @"Microsoft 365 - Black", PaletteMode.Microsoft365Black },
                { @"Microsoft 365 - Black (Dark Mode)", PaletteMode.Microsoft365BlackDarkMode },
                { @"Microsoft 365 - Dark Gray", PaletteMode.Microsoft365DarkGray },
                { @"Custom", PaletteMode.Custom }
            });

        #endregion

        #region Public

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

        private static PaletteMode ReturnPaletteMode(string themeName) =>
            // Note: Needs to be filled out
            PaletteMode.Custom;

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
        public static string ReturnPaletteModeAsString(PaletteMode paletteMode, KryptonManager manager = null)
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
        /// Applies the theme manager mode.
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        /// <returns>The <see cref="PaletteMode"/> equivalent.</returns>
        public static PaletteMode GetThemeManagerMode(string themeName) => _supportedThemes.GetByFirst(themeName);

        /// <summary>
        /// Propagates the theme selector.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="excludePartials">do not include any string containing</param>
        public static void PropagateThemeSelector(KryptonRibbonGroupThemeComboBox target, params string[] excludePartials) => AddToCollection(target.Items, excludePartials);

        #endregion
    }
}
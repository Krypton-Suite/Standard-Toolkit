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

using Krypton.Toolkit.Utilities;

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
        /// TODO: this should use the list from Z:\GitHub\Krypton-Suite\Standard-Toolkit\Source\Krypton Components\Krypton.Toolkit\Converters\PaletteModeConverter.cs
        private static readonly BiDictionary<string, PaletteModeManager> _supportedThemes = new(new Dictionary<string, PaletteModeManager>
            {
                { @"Professional - System", PaletteModeManager.ProfessionalSystem },
                { @"Professional - Office 2003", PaletteModeManager.ProfessionalOffice2003 },
                { @"Office 2007 - Blue", PaletteModeManager.Office2007Blue },
                { @"Office 2007 - Blue (Dark Mode)", PaletteModeManager.Office2007BlueDarkMode },
                { @"Office 2007 - Blue (Light Mode)", PaletteModeManager.Office2007BlueLightMode },
                { @"Office 2007 - Silver", PaletteModeManager.Office2007Silver },
                { @"Office 2007 - Silver (Dark Mode)", PaletteModeManager.Office2007SilverDarkMode },
                { @"Office 2007 - Silver (Light Mode)", PaletteModeManager.Office2007SilverLightMode },
                { @"Office 2007 - White", PaletteModeManager.Office2007White },
                { @"Office 2007 - Black", PaletteModeManager.Office2007Black },
                { @"Office 2007 - Black (Dark Mode)", PaletteModeManager.Office2007BlackDarkMode },
                { @"Office 2007 - Dark Gray", PaletteModeManager.Office2007DarkGray },
                { @"Office 2010 - Blue", PaletteModeManager.Office2010Blue },
                { @"Office 2010 - Blue (Dark Mode)", PaletteModeManager.Office2010BlueDarkMode },
                { @"Office 2010 - Blue (Light Mode)", PaletteModeManager.Office2010BlueLightMode },
                { @"Office 2010 - Silver", PaletteModeManager.Office2010Silver },
                { @"Office 2010 - Silver (Dark Mode)", PaletteModeManager.Office2010SilverDarkMode },
                { @"Office 2010 - Silver (Light Mode)", PaletteModeManager.Office2010SilverLightMode },
                { @"Office 2010 - White", PaletteModeManager.Office2010White },
                { @"Office 2010 - Black", PaletteModeManager.Office2010Black },
                { @"Office 2010 - Black (Dark Mode)", PaletteModeManager.Office2010BlackDarkMode },
                { @"Office 2010 - Dark Gray", PaletteModeManager.Office2010DarkGray },
                { @"Office 2013 - Dark Gray", PaletteModeManager.Office2013DarkGray },
                //{ @"Office 2013", PaletteModeManager.Office2013 },
                { @"Office 2013 - White", PaletteModeManager.Office2013White },
                { @"Sparkle - Blue", PaletteModeManager.SparkleBlue },
                { @"Sparkle - Blue (Dark Mode)", PaletteModeManager.SparkleBlueDarkMode },
                { @"Sparkle - Blue (Light Mode)", PaletteModeManager.SparkleBlueLightMode },
                { @"Sparkle - Orange", PaletteModeManager.SparkleOrange },
                { @"Sparkle - Orange (Dark Mode)", PaletteModeManager.SparkleOrangeDarkMode },
                { @"Sparkle - Orange (Light Mode)", PaletteModeManager.SparkleOrangeLightMode },
                { @"Sparkle - Purple", PaletteModeManager.SparklePurple },
                { @"Sparkle - Purple (Dark Mode)", PaletteModeManager.SparklePurpleDarkMode },
                { @"Sparkle - Purple (Light Mode)", PaletteModeManager.SparklePurpleLightMode },
                { @"Office 365 - Blue", PaletteModeManager.Microsoft365Blue },
                { @"Office 365 - Blue (Dark Mode)", PaletteModeManager.Microsoft365BlueDarkMode },
                { @"Office 365 - Blue (Light Mode)", PaletteModeManager.Microsoft365BlueLightMode },
                { @"Office 365 - Silver", PaletteModeManager.Microsoft365Silver },
                { @"Office 365 - Silver (Dark Mode)", PaletteModeManager.Microsoft365SilverDarkMode },
                { @"Office 365 - Silver (Light Mode)", PaletteModeManager.Microsoft365SilverLightMode },
                { @"Office 365 - White", PaletteModeManager.Microsoft365White },
                { @"Office 365 - Black", PaletteModeManager.Microsoft365Black },
                { @"Office 365 - Black (Dark Mode)", PaletteModeManager.Microsoft365BlackDarkMode },
                { @"Office 365 - Dark Gray", PaletteModeManager.Microsoft365DarkGray },
                { @"Custom", PaletteModeManager.Custom }
            });

        #endregion

        #region Instance Fields

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

        #region Methods
        /// <summary>
        /// Applies the theme.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="manager">The manager.</param>
        private static void ApplyTheme(PaletteModeManager mode, KryptonManager manager) => manager.GlobalPaletteMode = mode;

        /// <summary>
        /// Gets the palette mode manager.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <returns>The current <see cref="PaletteModeManager"/> mode.</returns>
        public static PaletteModeManager GetPaletteModeManager(KryptonManager manager) => manager.GlobalPaletteMode;

        /// <summary>Gets the palette mode.</summary>
        /// <param name="manager">The manager.</param>
        /// <returns>The current <see cref="PaletteMode"/>.</returns>
        public static PaletteMode GetPaletteMode(KryptonManager manager) => ReturnPaletteMode(manager.GlobalPaletteMode);

        private static PaletteMode ReturnPaletteMode(string themeName)
        {
            // Note: Needs to be filled out
            return PaletteMode.Custom;
        }

        /// <summary>Returns the palette mode.</summary>
        /// <param name="paletteModeManager">The palette mode manager.</param>
        /// <returns>The selected <see cref="PaletteMode"/>.</returns>
        private static PaletteMode ReturnPaletteMode(PaletteModeManager paletteModeManager)
        {
            switch (paletteModeManager)
            {
                case PaletteModeManager.Custom:
                    return PaletteMode.Custom;
                case PaletteModeManager.Office2007Black:
                    return PaletteMode.Office2007Black;
                case PaletteModeManager.Office2007White:
                    return PaletteMode.Office2007White;
                case PaletteModeManager.Office2007BlackDarkMode:
                    return PaletteMode.Office2007BlackDarkMode;
                case PaletteModeManager.Office2007Blue:
                    return PaletteMode.Office2007Blue;
                case PaletteModeManager.Office2007BlueDarkMode:
                    return PaletteMode.Office2007BlueDarkMode;
                case PaletteModeManager.Office2007BlueLightMode:
                    return PaletteMode.Office2007BlueLightMode;
                case PaletteModeManager.Office2007DarkGray:
                    return PaletteMode.Office2007DarkGray;
                case PaletteModeManager.Office2007Silver:
                    return PaletteMode.Office2007Silver;
                case PaletteModeManager.Office2007SilverDarkMode:
                    return PaletteMode.Office2007SilverDarkMode;
                case PaletteModeManager.Office2007SilverLightMode:
                    return PaletteMode.Office2007SilverLightMode;
                case PaletteModeManager.Office2010Black:
                    return PaletteMode.Office2010Black;
                case PaletteModeManager.Office2010BlackDarkMode:
                    return PaletteMode.Office2010BlackDarkMode;
                case PaletteModeManager.Office2010Blue:
                    return PaletteMode.Office2010Blue;
                case PaletteModeManager.Office2010BlueDarkMode:
                    return PaletteMode.Office2010BlueDarkMode;
                case PaletteModeManager.Office2010BlueLightMode:
                    return PaletteMode.Office2010BlueLightMode;
                case PaletteModeManager.Office2010DarkGray:
                    return PaletteMode.Office2010DarkGray;
                case PaletteModeManager.Office2010Silver:
                    return PaletteMode.Office2010Silver;
                case PaletteModeManager.Office2010SilverDarkMode:
                    return PaletteMode.Office2010SilverDarkMode;
                case PaletteModeManager.Office2010SilverLightMode:
                    return PaletteMode.Office2010SilverLightMode;
                case PaletteModeManager.Office2013DarkGray:
                    return PaletteMode.Office2013DarkGray;
                case PaletteModeManager.Office2010White:
                    return PaletteMode.Office2010White;
                case PaletteModeManager.Office2013White:
                    return PaletteMode.Office2013White;
                case PaletteModeManager.Microsoft365Black:
                    return PaletteMode.Microsoft365Black;
                case PaletteModeManager.Microsoft365BlackDarkMode:
                    return PaletteMode.Microsoft365BlackDarkMode;
                case PaletteModeManager.Microsoft365Blue:
                    return PaletteMode.Microsoft365Blue;
                case PaletteModeManager.Microsoft365BlueDarkMode:
                    return PaletteMode.Microsoft365BlueDarkMode;
                case PaletteModeManager.Microsoft365BlueLightMode:
                    return PaletteMode.Microsoft365BlueLightMode;
                case PaletteModeManager.Microsoft365DarkGray:
                    return PaletteMode.Microsoft365DarkGray;
                case PaletteModeManager.Microsoft365Silver:
                    return PaletteMode.Microsoft365Silver;
                case PaletteModeManager.Microsoft365SilverDarkMode:
                    return PaletteMode.Microsoft365SilverDarkMode;
                case PaletteModeManager.Microsoft365SilverLightMode:
                    return PaletteMode.Microsoft365SilverLightMode;
                case PaletteModeManager.Microsoft365White:
                    return PaletteMode.Microsoft365White;
                case PaletteModeManager.ProfessionalOffice2003:
                    return PaletteMode.ProfessionalOffice2003;
                case PaletteModeManager.ProfessionalSystem:
                    return PaletteMode.ProfessionalSystem;
                case PaletteModeManager.SparkleBlue:
                    return PaletteMode.SparkleBlue;
                case PaletteModeManager.SparkleBlueDarkMode:
                    return PaletteMode.SparkleBlueDarkMode;
                case PaletteModeManager.SparkleBlueLightMode:
                    return PaletteMode.SparkleBlueLightMode;
                case PaletteModeManager.SparkleOrange:
                    return PaletteMode.SparkleOrange;
                case PaletteModeManager.SparkleOrangeDarkMode:
                    return PaletteMode.SparkleOrangeDarkMode;
                case PaletteModeManager.SparkleOrangeLightMode:
                    return PaletteMode.SparkleOrangeLightMode;
                case PaletteModeManager.SparklePurple:
                    return PaletteMode.SparklePurple;
                case PaletteModeManager.SparklePurpleDarkMode:
                    return PaletteMode.SparklePurpleDarkMode;
                case PaletteModeManager.SparklePurpleLightMode:
                    return PaletteMode.SparklePurpleLightMode;
                default:
                    return PaletteMode.Microsoft365Blue;
            }
        }

        /// <summary>
        /// Applies the theme.
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        /// <param name="manager">The manager.</param>
        public static void ApplyTheme(string themeName, KryptonManager manager)
        {
            ApplyTheme(_supportedThemes.GetByFirst(themeName), manager);
        }

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

                ApplyGlobalTheme(manager, GetPaletteModeManager(manager));
            }
            catch (Exception exc)
            {
                ExceptionHandler.CaptureException(exc);
            }
        }

        /// <summary>
        /// Returns the palette mode manager as string.
        /// </summary>
        /// <param name="paletteModeManager">The palette mode manager.</param>
        /// <param name="manager">The manager.</param>
        /// <returns>The chosen theme as a string.</returns>
        public static string ReturnPaletteModeManagerAsString(PaletteModeManager paletteModeManager, KryptonManager manager = null)
        {
            var paletteMode = manager?.GlobalPaletteMode ?? paletteModeManager;

            return _supportedThemes.GetBySecond(paletteMode);
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

                ApplyTheme(PaletteModeManager.Custom, manager);
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
        public static string ReturnPaletteModeAsString(PaletteMode paletteMode)
        {
            PaletteModeConverter modeConverter = new();

            return modeConverter.ConvertToString(paletteMode);
        }

        /// <summary>
        /// Applies the global theme.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="paletteModeManager">The palette mode manager.</param>
        public static void ApplyGlobalTheme(KryptonManager manager, PaletteModeManager paletteModeManager)
        {
            try
            {
                manager.GlobalPaletteMode = paletteModeManager;
            }
            catch (Exception exc)
            {
                ExceptionHandler.CaptureException(exc);
            }
        }

        /// <summary>
        /// Applies the global theme.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="mode">The theme mode.</param>
        public static void ApplyGlobalTheme(KryptonManager manager, PaletteMode mode)
        {
            try
            {
                manager.GlobalPaletteMode = (PaletteModeManager)mode;
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
        public static void PropagateThemeSelector(KryptonComboBox target, params string[] excludePartials)
        {
            AddToCollection(target.Items, excludePartials);
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
        /// Applies the theme mode.
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        /// <returns>The <see cref="PaletteMode"/> equivalent.</returns>
        public static PaletteMode ApplyThemeMode(string themeName) => (PaletteMode)Enum.Parse(typeof(PaletteMode), themeName);

        /// <summary>
        /// Applies the theme manager mode.
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        /// <returns>The <see cref="PaletteModeManager"/> equivalent.</returns>
        public static PaletteModeManager ApplyThemeManagerMode(string themeName) => (PaletteModeManager)Enum.Parse(typeof(PaletteModeManager), themeName);
        #endregion
    }
}
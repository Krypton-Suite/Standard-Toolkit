#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
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
        private static readonly string[] _supportedThemes = new string[]
        {
             "Professional - System",

            "Professional - Office 2003",

            "Office 2007 - Black",

            "Office 2007 - Blue",

            "Office 2007 - Silver",

            "Office 2007 - White",

            "Office 2010 - Black",

            "Office 2010 - Blue",

            "Office 2010 - Silver",

            "Office 2010 - White",

            "Office 2013",

            "Office 2013 - White",

            "Office 365 - Black",

            "Office 365 - Blue",

            "Office 365 - Silver",

            "Office 365 - White",

            "Sparkle - Blue",

            "Sparkle - Orange",

            "Sparkle - Purple",

            "Custom"
        };
        #endregion

        #region Properties        
        /// <summary>
        /// Gets the supported theme array.
        /// </summary>
        /// <value>
        /// The supported theme array.
        /// </value>
        public static string[] SupportedThemeArray => _supportedThemes;

        #endregion

        #region Methods
        /// <summary>
        /// Applies the theme.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="manager">The manager.</param>
        private static void ApplyTheme(PaletteModeManager mode, KryptonManager manager)
        {
            switch (mode)
            {
                case PaletteModeManager.ProfessionalSystem:
                    manager.GlobalPaletteMode = PaletteModeManager.ProfessionalSystem;
                    break;
                case PaletteModeManager.ProfessionalOffice2003:
                    manager.GlobalPaletteMode = PaletteModeManager.ProfessionalOffice2003;
                    break;
                case PaletteModeManager.Office2007Blue:
                    manager.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
                    break;
                case PaletteModeManager.Office2007Silver:
                    manager.GlobalPaletteMode = PaletteModeManager.Office2007Silver;
                    break;
                case PaletteModeManager.Office2007White:
                    manager.GlobalPaletteMode = PaletteModeManager.Office2007White;
                    break;
                case PaletteModeManager.Office2007Black:
                    manager.GlobalPaletteMode = PaletteModeManager.Office2007Black;
                    break;
                case PaletteModeManager.Office2010Blue:
                    manager.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
                    break;
                case PaletteModeManager.Office2010Silver:
                    manager.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
                    break;
                case PaletteModeManager.Office2010White:
                    manager.GlobalPaletteMode = PaletteModeManager.Office2010White;
                    break;
                case PaletteModeManager.Office2010Black:
                    manager.GlobalPaletteMode = PaletteModeManager.Office2010Black;
                    break;
                case PaletteModeManager.Office2013:
                    manager.GlobalPaletteMode = PaletteModeManager.Office2013;
                    break;
                case PaletteModeManager.Office2013White:
                    manager.GlobalPaletteMode = PaletteModeManager.Office2013White;
                    break;
                case PaletteModeManager.Office365Black:
                    manager.GlobalPaletteMode = PaletteModeManager.Office365Black;
                    break;
                case PaletteModeManager.Office365Blue:
                    manager.GlobalPaletteMode = PaletteModeManager.Office365Blue;
                    break;
                case PaletteModeManager.Office365Silver:
                    manager.GlobalPaletteMode = PaletteModeManager.Office365Silver;
                    break;
                case PaletteModeManager.Office365White:
                    manager.GlobalPaletteMode = PaletteModeManager.Office365White;
                    break;
                case PaletteModeManager.SparkleBlue:
                    manager.GlobalPaletteMode = PaletteModeManager.SparkleBlue;
                    break;
                case PaletteModeManager.SparkleOrange:
                    manager.GlobalPaletteMode = PaletteModeManager.SparkleOrange;
                    break;
                case PaletteModeManager.SparklePurple:
                    manager.GlobalPaletteMode = PaletteModeManager.SparklePurple;
                    break;
                case PaletteModeManager.Custom:
                    manager.GlobalPaletteMode = PaletteModeManager.Custom;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Gets the palette mode.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <returns>The current <see cref="PaletteModeManager"/> mode.</returns>
        public static PaletteModeManager GetPaletteMode(KryptonManager manager)
        {
            return manager.GlobalPaletteMode;
        }

        /// <summary>
        /// Applies the theme.
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        /// <param name="manager">The manager.</param>
        private static void ApplyTheme(string themeName, KryptonManager manager)
        {
            switch (themeName)
            {
                case "Custom":
                    ApplyTheme(PaletteModeManager.Custom, manager);
                    break;
                case "Professional - System":
                    ApplyTheme(PaletteModeManager.ProfessionalSystem, manager);
                    break;
                case "Professional - Office 2003":
                    ApplyTheme(PaletteModeManager.ProfessionalOffice2003, manager);
                    break;
                case "Office 2007 - Blue":
                    ApplyTheme(PaletteModeManager.Office2007Blue, manager);
                    break;
                case "Office 2007 - Silver":
                    ApplyTheme(PaletteModeManager.Office2007Silver, manager);
                    break;
                case "Office 2007 - White":
                    ApplyTheme(PaletteModeManager.Office2007White, manager);
                    break;
                case "Office 2007 - Black":
                    ApplyTheme(PaletteModeManager.Office2007Black, manager);
                    break;
                case "Office 2010 - Blue":
                    ApplyTheme(PaletteModeManager.Office2010Blue, manager);
                    break;
                case "Office 2010 - Silver":
                    ApplyTheme(PaletteModeManager.Office2010Silver, manager);
                    break;
                case "Office 2010 - White":
                    ApplyTheme(PaletteModeManager.Office2010White, manager);
                    break;
                case "Office 2010 - Black":
                    ApplyTheme(PaletteModeManager.Office2010Black, manager);
                    break;
                case "Office 2013":
                    ApplyTheme(PaletteModeManager.Office2013, manager);
                    break;
                case "Office 2013 - White":
                    ApplyTheme(PaletteModeManager.Office2013White, manager);
                    break;
                case "Sparkle - Blue":
                    ApplyTheme(PaletteModeManager.SparkleBlue, manager);
                    break;
                case "Sparkle - Orange":
                    ApplyTheme(PaletteModeManager.SparkleOrange, manager);
                    break;
                case "Sparkle - Purple":
                    ApplyTheme(PaletteModeManager.SparklePurple, manager);
                    break;
                case "Office 365 - Black":
                    ApplyTheme(PaletteModeManager.Office365Black, manager);
                    break;
                case "Office 365 - Blue":
                    ApplyTheme(PaletteModeManager.Office365Blue, manager);
                    break;
                case "Office 365 - Silver":
                    ApplyTheme(PaletteModeManager.Office365Silver, manager);
                    break;
                case "Office 365 - White":
                    ApplyTheme(PaletteModeManager.Office365White, manager);
                    break;
            }

            if (string.IsNullOrEmpty(themeName))
            {
                throw new ArgumentNullException();
            }
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
        /// <param name="paletteModeManager">The palette mode manager.</param>
        /// <param name="manager">The manager.</param>
        /// <returns>The chosen theme as a string.</returns>
        public static string ReturnPaletteModeManagerAsString(PaletteModeManager paletteModeManager, KryptonManager manager = null)
        {
            string result = null;

            if (manager != null)
            {
                result = manager.GlobalPaletteMode switch
                {
                    PaletteModeManager.Custom => "Custom",
                    PaletteModeManager.ProfessionalSystem => "Professional - System",
                    PaletteModeManager.ProfessionalOffice2003 => "Professional - Office 2003",
                    PaletteModeManager.Office2007Blue => "Office 2007 - Blue",
                    PaletteModeManager.Office2007Silver => "Office 2007 - Silver",
                    PaletteModeManager.Office2007White => "Office 2007 - White",
                    PaletteModeManager.Office2007Black => "Office 2007 - Black",
                    PaletteModeManager.Office2010Blue => "Office 2010 - Blue",
                    PaletteModeManager.Office2010Silver => "Office 2010 - Silver",
                    PaletteModeManager.Office2010White => "Office 2010 - White",
                    PaletteModeManager.Office2010Black => "Office 2010 - Black",
                    PaletteModeManager.Office2013 => "Office 2013",
                    PaletteModeManager.Office2013White => "Office 2013 - White",
                    PaletteModeManager.SparkleBlue => "Sparkle - Blue",
                    PaletteModeManager.SparkleOrange => "Sparkle - Orange",
                    PaletteModeManager.SparklePurple => "Sparkle - Purple",
                    PaletteModeManager.Office365Blue => "Office 365 - Blue",
                    PaletteModeManager.Office365Silver => "Office 365 - Silver",
                    PaletteModeManager.Office365White => "Office 365 - White",
                    PaletteModeManager.Office365Black => "Office 365 - Black",
                    _ => result
                };
            }
            else
            {
                result = paletteModeManager switch
                {
                    PaletteModeManager.Custom => "Custom",
                    PaletteModeManager.ProfessionalSystem => "Professional - System",
                    PaletteModeManager.ProfessionalOffice2003 => "Professional - Office 2003",
                    PaletteModeManager.Office2007Blue => "Office 2007 - Blue",
                    PaletteModeManager.Office2007Silver => "Office 2007 - Silver",
                    PaletteModeManager.Office2007White => "Office 2007 - White",
                    PaletteModeManager.Office2007Black => "Office 2007 - Black",
                    PaletteModeManager.Office2010Blue => "Office 2010 - Blue",
                    PaletteModeManager.Office2010Silver => "Office 2010 - Silver",
                    PaletteModeManager.Office2010White => "Office 2010 - White",
                    PaletteModeManager.Office2010Black => "Office 2010 - Black",
                    PaletteModeManager.Office2013 => "Office 2013",
                    PaletteModeManager.Office2013White => "Office 2013 - White",
                    PaletteModeManager.SparkleBlue => "Sparkle - Blue",
                    PaletteModeManager.SparkleOrange => "Sparkle - Orange",
                    PaletteModeManager.SparklePurple => "Sparkle - Purple",
                    PaletteModeManager.Office365Blue => "Office 365 - Blue",
                    PaletteModeManager.Office365Silver => "Office 365 - Silver",
                    PaletteModeManager.Office365White => "Office 365 - White",
                    PaletteModeManager.Office365Black => "Office 365 - Black",
                    _ => result
                };
            }

            return result;
        }

        /// <summary>
        /// Loads the custom theme.
        /// </summary>
        /// <param name="palette">The palette.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="silent">if set to <c>true</c> [silent].</param>
        public static void LoadCustomTheme(KryptonPalette palette, KryptonManager manager, bool silent = false)
        {
            try
            {
                // Declare new instances
                palette = new KryptonPalette();

                manager = new KryptonManager();

                // Prompt user for palette definition

                // TODO: Add silent option
                //if (silent)
                //{

                //}
                //else
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
            #region Old Code
            //string result = null;

            //if (paletteMode == PaletteMode.Custom)
            //{
            //    result = "Custom";
            //}

            //if (paletteMode == PaletteMode.Global)
            //{
            //    result = "Global";
            //}

            //if (paletteMode == PaletteMode.ProfessionalSystem)
            //{
            //    result = "Professional - System";
            //}

            //if (paletteMode == PaletteMode.ProfessionalOffice2003)
            //{
            //    result = "Professional - Office 2003";
            //}

            //if (paletteMode == PaletteMode.Office2007Blue)
            //{
            //    result = "Office 2007 - Blue";
            //}

            //if (paletteMode == PaletteMode.Office2007Silver)
            //{
            //    result = "Office 2007 - Silver";
            //}

            //if (paletteMode == PaletteMode.Office2007White)
            //{
            //    result = "Office 2007 - White";
            //}

            //if (paletteMode == PaletteMode.Office2007Black)
            //{
            //    result = "Office 2007 - Black";
            //}

            //if (paletteMode == PaletteMode.Office2010Blue)
            //{
            //    result = "Office 2010 - Blue";
            //}

            //if (paletteMode == PaletteMode.Office2010Silver)
            //{
            //    result = "Office 2010 - Silver";
            //}

            //if (paletteMode == PaletteMode.Office2010White)
            //{
            //    result = "Office 2010 - White";
            //}

            //if (paletteMode == PaletteMode.Office2010Black)
            //{
            //    result = "Office 2010 - Black";
            //}

            //if (paletteMode == PaletteMode.Office2013)
            //{
            //    result = "Office 2013";
            //}

            //if (paletteMode == PaletteMode.Office2013White)
            //{
            //    result = "Office 2013 - White";
            //}

            //if (paletteMode == PaletteMode.SparkleBlue)
            //{
            //    result = "Sparkle Blue";
            //}

            //if (paletteMode == PaletteMode.SparkleOrange)
            //{
            //    result = "Sparkle Orange";
            //}

            //if (paletteMode == PaletteMode.SparklePurple)
            //{
            //    result = "Sparkle Purple";
            //}

            //if (paletteMode == PaletteMode.Office365Blue)
            //{
            //    result = "Office 365 - Blue";
            //}

            //if (paletteMode == PaletteMode.Office365Silver)
            //{
            //    result = "Office 365 - Silver";
            //}

            //if (paletteMode == PaletteMode.Office365White)
            //{
            //    result = "Office 365 - White";
            //}

            //if (paletteMode == PaletteMode.Office365Black)
            //{
            //    result = "Office 365 - Black";
            //}

            //return result;
            #endregion

            string result;

            PaletteModeConverter modeConverter = new();

            result = modeConverter.ConvertToString(paletteMode);

            return result;
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
        public static void PropagateThemeSelector(KryptonComboBox target)
        {
            try
            {
                foreach (string theme in SupportedThemeArray)
                {
                    target.Items.Add(theme);
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
        public static void PropagateThemeSelector(KryptonListBox target)
        {
            try
            {
                foreach (string theme in SupportedThemeArray)
                {
                    target.Items.Add(theme);
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
        public static void PropagateThemeSelector(KryptonDomainUpDown target)
        {
            try
            {
                foreach (string theme in SupportedThemeArray)
                {
                    target.Items.Add(theme);
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
        public static void PropagateThemeSelector(ComboBox target)
        {
            try
            {
                foreach (string theme in SupportedThemeArray)
                {
                    target.Items.Add(theme);
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
        public static void PropagateThemeSelector(DomainUpDown target)
        {
            try
            {
                foreach (string theme in SupportedThemeArray)
                {
                    target.Items.Add(theme);
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
        public static void PropagateThemeSelector(ToolStripComboBox target)
        {
            try
            {
                foreach (string theme in SupportedThemeArray)
                {
                    target.Items.Add(theme);
                }
            }
            catch (Exception exc)
            {
                ExceptionHandler.CaptureException(exc);
            }
        }

        /// <summary>
        /// Applies the theme mode.
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        /// <returns>The <see cref="PaletteMode"/> equivalent.</returns>
        public static PaletteMode ApplyThemeMode(string themeName)
        {
            PaletteMode mode = (PaletteMode)Enum.Parse(typeof(PaletteMode), themeName);

            return mode;
        }

        /// <summary>
        /// Applies the theme manager mode.
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        /// <returns>The <see cref="PaletteModeManager"/> equivalent.</returns>
        public static PaletteModeManager ApplyThemeManagerMode(string themeName)
        {
            PaletteModeManager modeManager = (PaletteModeManager)Enum.Parse(typeof(PaletteModeManager), themeName);

            return modeManager;
        }
        #endregion
    }
}
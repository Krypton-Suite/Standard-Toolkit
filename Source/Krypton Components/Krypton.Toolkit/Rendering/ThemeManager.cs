#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
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
        #region Properties        

        /// <summary>Gets the supported theme array.</summary>
        /// <value>The supported theme array.</value>
        public static ICollection<string> SupportedInternalThemeNames => PaletteModeStrings.SupportedInternalThemeNames;

        /// <summary>Returns the Default Global Palette.</summary>
        public static PaletteMode DefaultGlobalPalette => GlobalStaticValues.GLOBAL_DEFAULT_PALETTE_MODE;

        #endregion

        #region Implementation

        /// <summary>Returns the palette mode from the Krypton Manager instance.</summary>
        /// <param name="manager">The manager instance.</param>
        /// <returns>The current <see cref="PaletteMode"/>.</returns>
        public static PaletteMode GetPaletteMode(KryptonManager manager) => manager.GlobalPaletteMode;

        /// <summary>
        /// Applies the theme using PaletteMode enumeration.
        /// </summary>
        /// <param name="mode">The palette mode.</param>
        /// <param name="manager">The manager.</param>
        public static void ApplyTheme(PaletteMode mode, KryptonManager manager) => ApplyGlobalTheme(manager, mode);

        /// <summary>
        /// Applies the theme using the themes name.<br/>
        /// </summary>
        /// <param name="themeName">Valid name of the theme.</param>
        /// <param name="manager">The manager.</param>
        public static void ApplyTheme(string themeName, KryptonManager manager) => ApplyGlobalTheme(manager, GetThemeManagerMode(themeName));

        /// <summary>
        /// Sets the theme.
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        /// <param name="manager">The manager.</param>
        [Obsolete("Deprecated and will be removed in V100. Replace this with calls to ApplyTheme( . . . )")]
        public static void SetTheme(string themeName, KryptonManager manager) =>
            //TODO V100 Remove SetTheme method
            ApplyGlobalTheme(manager, GetThemeManagerMode(themeName));

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
                ExceptionHandler.CaptureException(exc, showStackTrace: GlobalStaticValues.DEFAULT_USE_STACK_TRACE);
            }
        }

        /// <summary>
        /// Returns the respective theme name for the given KryptonManager instance.<br/>
        /// </summary>
        /// <param name="manager">A valid reference to a KryptonManager instance.</param>
        /// <returns>The theme name.</returns>
        public static string ReturnPaletteModeAsString(KryptonManager manager) => ReturnPaletteModeAsString(manager.GlobalPaletteMode);

        /// <summary>
        /// Returns the palette mode as string.
        /// </summary>
        /// <param name="paletteMode">The palette mode.</param>
        /// <returns>The theme name</returns>
        public static string ReturnPaletteModeAsString(PaletteMode paletteMode) => new PaletteModeConverter().ConvertToString(paletteMode)!;
        
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
                manager.GlobalCustomPalette = palette;

                ApplyTheme(PaletteMode.Custom, manager);
            }
            catch (Exception exc)
            {
                ExceptionHandler.CaptureException(exc, showStackTrace: GlobalStaticValues.DEFAULT_USE_STACK_TRACE);
            }
        }

        /// <summary>
        /// Returns the themes PaletteMode from the theme's name.
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        /// <returns>The respective PaletteMode if the theme name is valid. Otherwise PaletteMode.Global.</returns>
        public static PaletteMode GetThemeManagerMode(string themeName)
        {
            return PaletteModeStrings.SupportedThemesMap.TryGetValue(themeName, out PaletteMode paletteMode)
                ? paletteMode
                : PaletteMode.Global;
        }

        #endregion
    }
}
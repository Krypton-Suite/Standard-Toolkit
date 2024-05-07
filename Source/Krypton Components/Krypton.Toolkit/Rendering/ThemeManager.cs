#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2024. All rights reserved. 
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

        /// <summary>Gets or sets the index of the theme.</summary>
        /// <value>The index of the theme.</value>
        public static int ThemeIndex { get; set; } = 33;

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
        /// <param name="paletteMode">The palette mode manager.</param>
        /// <returns>The selected <see cref="PaletteMode"/>.</returns>
        private static PaletteMode ReturnPaletteMode(PaletteMode paletteMode) => paletteMode;

        /// <summary>
        /// Applies the theme.
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        /// <param name="manager">The manager.</param>
        public static void ApplyTheme(string themeName, KryptonManager manager) => ApplyTheme(PaletteModeStrings.SupportedThemesMap[themeName], manager);

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
            var cnvtr = new PaletteModeConverter();
            return cnvtr.ConvertToString(mode)!;
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
                manager.GlobalCustomPalette = palette;

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
        public static string ReturnPaletteModeAsString(PaletteMode paletteMode)
        {
            var modeConverter = new PaletteModeConverter();

            return modeConverter.ConvertToString(paletteMode)!;
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
        public static PaletteMode GetThemeManagerMode(string themeName)
        {
            var modeConverter = new PaletteModeConverter();

            return (PaletteMode)modeConverter.ConvertFrom(themeName)!;
        }

        /// <summary>Sets the index of the theme.</summary>
        /// <param name="value">The value.</param>
        public static void SetThemeIndex(int? value) => ThemeIndex = value ?? 33;

        /// <summary>Gets the index of the theme.</summary>
        /// <returns></returns>
        public static int GetThemeIndex() => ThemeIndex;

        #endregion
    }
}
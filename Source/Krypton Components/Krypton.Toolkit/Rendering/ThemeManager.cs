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

namespace Krypton.Toolkit;

/// <summary>
/// Allows the developer to easily access the entire array of supported themes for custom controls.
/// </summary>
public class ThemeManager
{
    #region Private static fields
    private const string _msgBoxCaption = "ThemeManager";
    #endregion

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
    /// Applies the theme using the theme's name.
    /// </summary>
    /// <param name="themeName">Valid name of the theme.</param>
    /// <param name="manager">The manager.</param>
    public static void ApplyTheme(string themeName, KryptonManager manager) => ApplyGlobalTheme(manager, GetThemeManagerMode(themeName));

    /// <summary>
    /// Applies the provided custom palette object.
    /// </summary>
    /// <param name="palette">Reference to a KryptonCustomPaletteBase object</param>
    /// <param name="manager">The manager.</param>
    public static void ApplyTheme(KryptonCustomPaletteBase palette, KryptonManager manager)
    {
        manager.GlobalCustomPalette = palette;
        manager.GlobalPaletteMode = PaletteMode.Custom;
    }

    /// <summary>
    /// Loads a custom theme from the given file.
    /// </summary>
    /// <param name="themeFile">Valid path including filename to the theme file. The file must exist an be compatible, otherwise the import will fail.</param>
    /// <param name="silent">True if the operation should suppress messages from the palette import process, otherwise false.</param>
    /// <param name="manager">The manager.</param>
    public static void ApplyTheme(string themeFile, bool silent, KryptonManager manager)
    {
        if (themeFile.Length > 0 && File.Exists(themeFile))
        {
            try
            {
                KryptonCustomPaletteBase palette = new();
                palette.Import(themeFile, silent);

                ApplyTheme(palette, manager);
            }
            catch (Exception exc)
            {
                KryptonExceptionHandler.CaptureException(exc, showStackTrace: GlobalStaticValues.DEFAULT_USE_STACK_TRACE);
            }
        }
        else
        {
            KryptonMessageBox.Show(
                $"The parameter 'themeFile' points to a file that does not exist.\n" + 
                $"Filename: {themeFile}\n\n" +
                $"ApplyTheme aborted.",
                _msgBoxCaption,
                buttons: KryptonMessageBoxButtons.OK,
                icon: KryptonMessageBoxIcon.Exclamation);
        }
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
            // Set the global palette mode
            manager.GlobalPaletteMode = paletteMode;
        }
        catch (Exception exc)
        {
            KryptonExceptionHandler.CaptureException(exc, showStackTrace: GlobalStaticValues.DEFAULT_USE_STACK_TRACE);
        }
    }

    /// <summary>
    /// Returns the respective theme name for the given KryptonManager instance.
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
    [Obsolete("Deprecated and will be removed in V110. Set a global custom palette through 'ThemeManager.ApplyTheme(...)'.")]
    public static void LoadCustomTheme(KryptonCustomPaletteBase palette, KryptonManager manager, string themeFile = "", bool silent = false)
    {
        // Until removal pass the call to the new ApplyTheme method.
        ApplyTheme(themeFile, silent, manager ?? new KryptonManager());
            
        //try
        //{
        //    // Declare new instances (no need for locking if these are local to the method)
        //    palette = new KryptonCustomPaletteBase();
        //    manager = new KryptonManager();

        //    // TODO: Add silent option
        //    if (silent)
        //    {
        //        if (themeFile is not ("" and ""))
        //        {
        //            palette.Import(themeFile, silent);
        //        }
        //    }
        //    else
        //    {
        //        palette.Import();
        //    }

        //    // Set manager
        //    manager.GlobalCustomPalette = palette;

        //    ApplyTheme(PaletteMode.Custom, manager);
        //}
        //catch (Exception exc)
        //{
        //    KryptonExceptionHandler.CaptureException(exc,
        //        showStackTrace: GlobalStaticValues.DEFAULT_USE_STACK_TRACE);
        //}
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
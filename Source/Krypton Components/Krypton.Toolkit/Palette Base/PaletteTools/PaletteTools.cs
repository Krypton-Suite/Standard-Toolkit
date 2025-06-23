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

//public class PaletteTools
//{
//    #region Properties
//    /// <summary>Gets the theme list.</summary>
//    /// <value>The theme list.</value>
//    public static List<string?> ThemeList1 => ThemeManager.SupportedInternalThemeNames.ToList();

//    #endregion

//    #region Methods
//    /// <summary>Links the type of the palette to the correct theme style.</summary>
//    /// <param name="themeName">Name of the theme.</param>
//    /// <returns></returns>
//    public PaletteMode LinkPaletteType1(string themeName)
//    {
//        var cnvtr = new PaletteModeConverter();
//        return (PaletteMode)cnvtr.ConvertFromString(themeName);
//    }

//    /// <summary>Applies the theme.</summary>
//    /// <param name="manager">The manager.</param>
//    /// <param name="paletteMode">The palette mode.</param>
//    /// <param name="customThemePath">The custom theme path.</param>
//    public static void ApplyTheme(KryptonManager manager, PaletteMode paletteMode = PaletteMode.Microsoft365Blue, string customThemePath = "")
//    {
//        manager.GlobalPaletteMode = paletteMode;

//        if (!string.IsNullOrWhiteSpace(customThemePath))
//        {
//            var palette = new KryptonCustomPaletteBase();

//            palette.Import(customThemePath);

//            manager.GlobalPalette = palette;

//            manager.GlobalPaletteMode = PaletteMode.Custom;
//        }
//    }
//    #endregion
//}
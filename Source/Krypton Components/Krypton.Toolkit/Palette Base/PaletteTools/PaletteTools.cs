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
    public class PaletteTools
    {
        #region Properties
        /// <summary>Gets the theme list.</summary>
        /// <value>The theme list.</value>
        public static List<string> ThemeList1 => ThemeManager.SupportedInternalThemeNames.ToList();

        #endregion

        #region Methods
        /// <summary>Links the type of the palette to the correct theme style.</summary>
        /// <param name="themeName">Name of the theme.</param>
        /// <returns></returns>
        public PaletteMode LinkPaletteType1(string themeName)
        {
            PaletteMode paletteMode = new();

            switch (themeName)
            {
                case "Custom":
                    paletteMode = PaletteMode.Custom;
                    break;
                case "Professional - System":
                    paletteMode = PaletteMode.ProfessionalSystem;
                    break;
                case "Professional - Office 2003":
                    paletteMode = PaletteMode.ProfessionalOffice2003;
                    break;
                case "Office 2007 - Black":
                    paletteMode = PaletteMode.Office2007Black;
                    break;
                case "Office 2007 - Blue":
                    paletteMode = PaletteMode.Office2007Blue;
                    break;
                case "Office 2007 - Silver":
                    paletteMode = PaletteMode.Office2007Silver;
                    break;
                case "Office 2007 - White":
                    paletteMode = PaletteMode.Office2007White;
                    break;
                case "Office 2010 - Black":
                    paletteMode = PaletteMode.Office2010Black;
                    break;
                case "Office 2010 - Blue":
                    paletteMode = PaletteMode.Office2010Blue;
                    break;
                case "Office 2010 - Silver":
                    paletteMode = PaletteMode.Office2010Silver;
                    break;
                case "Office 2010 - White":
                    paletteMode = PaletteMode.Office2010White;
                    break;
                /*else if (themeName.Equals("Office 2013"))
            {
                paletteMode = PaletteMode.Office2013;
            }*/
                case "Office 2013 - White":
                    paletteMode = PaletteMode.Office2013White;
                    break;
                case "Microsoft 365 - Black":
                    paletteMode = PaletteMode.Microsoft365Black;
                    break;
                case "Microsoft 365 - Blue":
                    paletteMode = PaletteMode.Microsoft365Blue;
                    break;
                case "Microsoft 365 - Silver":
                    paletteMode = PaletteMode.Microsoft365Silver;
                    break;
                case "Microsoft 365 - White":
                    paletteMode = PaletteMode.Microsoft365White;
                    break;
                case "Sparkle - Blue":
                    paletteMode = PaletteMode.SparkleBlue;
                    break;
                case "Sparkle - Orange":
                    paletteMode = PaletteMode.SparkleOrange;
                    break;
                case "Sparkle - Purple":
                    paletteMode = PaletteMode.SparklePurple;
                    break;
            }

            return paletteMode;
        }

        /// <summary>Applies the theme.</summary>
        /// <param name="manager">The manager.</param>
        /// <param name="paletteMode">The palette mode.</param>
        /// <param name="customThemePath">The custom theme path.</param>
        public static void ApplyTheme(KryptonManager manager, PaletteMode paletteMode = PaletteMode.Microsoft365Blue, string customThemePath = "")
        {
            manager.GlobalPaletteMode = paletteMode;

            if (!string.IsNullOrWhiteSpace(customThemePath))
            {
                KryptonCustomPaletteBase palette = new();

                palette.Import(customThemePath);

                manager.GlobalPalette = palette;

                manager.GlobalPaletteMode = PaletteMode.Custom;
            }
        }
        #endregion
    }
}
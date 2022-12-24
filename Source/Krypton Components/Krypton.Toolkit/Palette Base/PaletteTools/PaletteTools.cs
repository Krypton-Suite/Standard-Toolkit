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

            if (themeName.Equals("Custom"))
            {
                paletteMode = PaletteMode.Custom;
            }
            else if (themeName.Equals("Professional - System"))
            {
                paletteMode = PaletteMode.ProfessionalSystem;
            }
            else if (themeName.Equals("Professional - Office 2003"))
            {
                paletteMode = PaletteMode.ProfessionalOffice2003;
            }
            else if (themeName.Equals("Office 2007 - Black"))
            {
                paletteMode = PaletteMode.Office2007Black;
            }
            else if (themeName.Equals("Office 2007 - Blue"))
            {
                paletteMode = PaletteMode.Office2007Blue;
            }
            else if (themeName.Equals("Office 2007 - Silver"))
            {
                paletteMode = PaletteMode.Office2007Silver;
            }
            else if (themeName.Equals("Office 2007 - White"))
            {
                paletteMode = PaletteMode.Office2007White;
            }
            else if (themeName.Equals("Office 2010 - Black"))
            {
                paletteMode = PaletteMode.Office2010Black;
            }
            else if (themeName.Equals("Office 2010 - Blue"))
            {
                paletteMode = PaletteMode.Office2010Blue;
            }
            else if (themeName.Equals("Office 2010 - Silver"))
            {
                paletteMode = PaletteMode.Office2010Silver;
            }
            else if (themeName.Equals("Office 2010 - White"))
            {
                paletteMode = PaletteMode.Office2010White;
            }
            /*else if (themeName.Equals("Office 2013"))
            {
                paletteMode = PaletteMode.Office2013;
            }*/
            else if (themeName.Equals("Office 2013 - White"))
            {
                paletteMode = PaletteMode.Office2013White;
            }
            else if (themeName.Equals("Microsoft 365 - Black"))
            {
                paletteMode = PaletteMode.Microsoft365Black;
            }
            else if (themeName.Equals("Microsoft 365 - Blue"))
            {
                paletteMode = PaletteMode.Microsoft365Blue;
            }
            else if (themeName.Equals("Microsoft 365 - Silver"))
            {
                paletteMode = PaletteMode.Microsoft365Silver;
            }
            else if (themeName.Equals("Microsoft 365 - White"))
            {
                paletteMode = PaletteMode.Microsoft365White;
            }
            else if (themeName.Equals("Sparkle - Blue"))
            {
                paletteMode = PaletteMode.SparkleBlue;
            }
            else if (themeName.Equals("Sparkle - Orange"))
            {
                paletteMode = PaletteMode.SparkleOrange;
            }
            else if (themeName.Equals("Sparkle - Purple"))
            {
                paletteMode = PaletteMode.SparklePurple;
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
#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    public class PaletteTools
    {
        #region Variables
        private static string[] _themeList = new string[] { "Custom",
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
                                                            "Sparkle - Purple" };
        #endregion

        #region Properties
        public static string[] ThemeList { get => _themeList; }
        #endregion

        #region Constructor
        public PaletteTools()
        {

        }
        #endregion

        #region Methods
        public PaletteModeManager LinkPaletteType(string themeName)
        {
            PaletteModeManager paletteMode = new PaletteModeManager();

            if (themeName.Equals("Custom"))
            {
                paletteMode = PaletteModeManager.Custom;
            }
            else if (themeName.Equals("Professional - System"))
            {
                paletteMode = PaletteModeManager.ProfessionalSystem;
            }
            else if (themeName.Equals("Professional - Office 2003"))
            {
                paletteMode = PaletteModeManager.ProfessionalOffice2003;
            }
            else if (themeName.Equals("Office 2007 - Black"))
            {
                paletteMode = PaletteModeManager.Office2007Black;
            }
            else if (themeName.Equals("Office 2007 - Blue"))
            {
                paletteMode = PaletteModeManager.Office2007Blue;
            }
            else if (themeName.Equals("Office 2007 - Silver"))
            {
                paletteMode = PaletteModeManager.Office2007Silver;
            }
            else if (themeName.Equals("Office 2007 - White"))
            {
                paletteMode = PaletteModeManager.Office2007White;
            }
            else if (themeName.Equals("Office 2010 - Black"))
            {
                paletteMode = PaletteModeManager.Office2010Black;
            }
            else if (themeName.Equals("Office 2010 - Blue"))
            {
                paletteMode = PaletteModeManager.Office2010Blue;
            }
            else if (themeName.Equals("Office 2010 - Silver"))
            {
                paletteMode = PaletteModeManager.Office2010Silver;
            }
            else if (themeName.Equals("Office 2010 - White"))
            {
                paletteMode = PaletteModeManager.Office2010White;
            }
            else if (themeName.Equals("Office 2013"))
            {
                paletteMode = PaletteModeManager.Office2013;
            }
            else if (themeName.Equals("Office 2013 - White"))
            {
                paletteMode = PaletteModeManager.Office2013White;
            }
            else if (themeName.Equals("Office 365 - Black"))
            {
                paletteMode = PaletteModeManager.Office365Black;
            }
            else if (themeName.Equals("Office 365 - Blue"))
            {
                paletteMode = PaletteModeManager.Office365Blue;
            }
            else if (themeName.Equals("Office 365 - Silver"))
            {
                paletteMode = PaletteModeManager.Office365Silver;
            }
            else if (themeName.Equals("Office 365 - White"))
            {
                paletteMode = PaletteModeManager.Office365White;
            }
            else if (themeName.Equals("Sparkle - Blue"))
            {
                paletteMode = PaletteModeManager.SparkleBlue;
            }
            else if (themeName.Equals("Sparkle - Orange"))
            {
                paletteMode = PaletteModeManager.SparkleOrange;
            }
            else if (themeName.Equals("Sparkle - Purple"))
            {
                paletteMode = PaletteModeManager.SparklePurple;
            }

            return paletteMode;
        }

        public static void ApplyTheme(KryptonManager manager, PaletteModeManager paletteMode = PaletteModeManager.Office365Blue, string customThemePath = "")
        {
            manager.GlobalPaletteMode = paletteMode;

            if (!MissingFrameWorkAPIs.IsNullOrWhiteSpace(customThemePath))
            {
                KryptonPalette palette = new KryptonPalette();

                palette.Import(customThemePath);

                manager.GlobalPalette = palette;

                manager.GlobalPaletteMode = PaletteModeManager.Custom;
            }
        }
        #endregion
    }
}
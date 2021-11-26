#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that PaletteMode values appear as neat text at design time.
    /// </summary>
    internal class PaletteModeConverter : StringLookupConverter
    {
        #region Static Fields        
        /// <summary>Converts the <see cref="PaletteMode"/> values into a human readable format.</summary>
        private readonly Pair[] _pairs = new Pair[] { new(PaletteMode.ProfessionalSystem,    "Professional - System"),
                                             new(PaletteMode.ProfessionalOffice2003,"Professional - Office 2003"),
                                             new(PaletteMode.Office2007Blue,        "Office 2007 - Blue"),
                                             new(PaletteMode.Office2007BlueDarkMode,     "Office 2007 - Blue (Dark Mode)"),
                                             new(PaletteMode.Office2007BlueLightMode,     "Office 2007 - Blue (Light Mode)"),
                                             new(PaletteMode.Office2007Silver,      "Office 2007 - Silver"),
                                             new(PaletteMode.Office2007SilverDarkMode, "Office 2007 - Silver (Dark Mode)"),
                                             new(PaletteMode.Office2007SilverLightMode, "Office 2007 - Silver (Light Mode)"),
                                             new(PaletteMode.Office2007White,       "Office 2007 - White"),
                                             new(PaletteMode.Office2007Black,       "Office 2007 - Black"),
                                             new(PaletteMode.Office2007BlackDarkMode,    "Office 2007 - Black (Dark Mode)"),
                                             new(PaletteMode.Office2010Blue,        "Office 2010 - Blue"),
                                             new(PaletteMode.Office2010BlueDarkMode,     "Office 2010 - Blue (Dark Mode)"),
                                             new(PaletteMode.Office2010BlueLightMode,     "Office 2010 - Blue (Light Mode)"),
                                             new(PaletteMode.Office2010Silver,      "Office 2010 - Silver"),
                                             new(PaletteMode.Office2010SilverDarkMode, "Office 2010 - Silver (Dark Mode)"),
                                             new(PaletteMode.Office2010SilverLightMode, "Office 2010 - Silver (Light Mode)"),
                                             new(PaletteMode.Office2010White,       "Office 2010 - White"),
                                             new(PaletteMode.Office2010Black,       "Office 2010 - Black"),
                                             new(PaletteMode.Office2010BlackDarkMode,    "Office 2010 - Black (Dark Mode)"),
                                             new(PaletteMode.Office2013White,       "Office 2013 - White"),
                                             new(PaletteMode.Office365Black,        "Office 365 - Black"),
                                             new(PaletteMode.Office365BlackDarkMode,     "Office 365 - Black (Dark Mode)"),
                                             new(PaletteMode.Office365Blue,         "Office 365 - Blue"),
                                             new(PaletteMode.Office365BlueDarkMode,      "Office 365 - Blue (Dark Mode)"),
                                             new(PaletteMode.Office365BlueLightMode,      "Office 365 - Blue (Light Mode)"),
                                             new(PaletteMode.Office365Silver,       "Office 365 - Silver"),
                                             new(PaletteMode.Office365SilverDarkMode, "Office 365 - Silver (Dark Mode)"),
                                             new(PaletteMode.Office365SilverLightMode, "Office 365 - Silver (Light Mode)"),
                                             new(PaletteMode.Office365White,        "Office 365 - White"),
                                             new(PaletteMode.SparkleBlue,           "Sparkle - Blue"),
                                             new(PaletteMode.SparkleBlueDarkMode, "Sparkle - Blue (Dark Mode)"),
                                             new(PaletteMode.SparkleBlueLightMode, "Sparkle - Blue (Light Mode)"),
                                             new(PaletteMode.SparkleOrange,         "Sparkle - Orange"),
                                             new(PaletteMode.SparkleOrangeDarkMode, "Sparkle - Orange (Dark Mode)"),
                                             new(PaletteMode.SparkleOrangeLightMode, "Sparkle - Orange (Light Mode)"),
                                             new(PaletteMode.SparklePurple,         "Sparkle - Purple"),
                                             new(PaletteMode.SparklePurpleDarkMode, "Sparkle - Purple (Dark Mode)"),
                                             new(PaletteMode.SparklePurpleLightMode, "Sparkle - Purple (Light Mode)")
                                             //new(PaletteMode.VisualStudioDark,      "Visual Studio (Dark Mode)"),
                                             //new(PaletteMode.VisualStudioLight,     "Visual Studio (Light Mode)")
                                             };
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteModeConverter class.
        /// </summary>
        public PaletteModeConverter()
            : base(typeof(PaletteMode))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs => _pairs;

        #endregion
    }
}

﻿namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that PaletteModeManager values appear as neat text at design time.
    /// </summary>
    internal class PaletteModeManagerConverter : StringLookupConverter
    {
        #region Static Fields

        private readonly Pair[] _pairs =
        {
            new(PaletteModeManager.ProfessionalSystem, "Professional - System"),
            new(PaletteModeManager.ProfessionalOffice2003, "Professional - Office 2003"),
            new(PaletteModeManager.Office2007Black, "Office 2007 - Black"),
            new(PaletteModeManager.Office2007BlackDarkMode, "Office 2007 - Black (Dark Mode)"),
            new(PaletteModeManager.Office2007Blue, "Office 2007 - Blue"),
            new(PaletteModeManager.Office2007BlueDarkMode, "Office 2007 - Blue (Dark Mode)"),
            new(PaletteModeManager.Office2007BlueLightMode, "Office 2007 - Blue (Light Mode)"),
            // Note: Re-enable when the gray themes are completed
            new(PaletteModeManager.Office2007DarkGray, "Office 2007 - Dark Gray"),
            //new(PaletteModeManager.Office2007LightGray, "Office 2007 - Light Gray"),
            new(PaletteModeManager.Office2007Silver, "Office 2007 - Silver"),
            new(PaletteModeManager.Office2007SilverDarkMode, "Office 2007 - Silver (Dark Mode)"),
            new(PaletteModeManager.Office2007SilverLightMode, "Office 2007 - Silver (Light Mode)"),
            new(PaletteModeManager.Office2007White, "Office 2007 - White"),
            new(PaletteModeManager.Office2010Black, "Office 2010 - Black"),
            new(PaletteModeManager.Office2010BlackDarkMode, "Office 2010 - Black (Dark Mode)"),
            new(PaletteModeManager.Office2010Blue, "Office 2010 - Blue"),
            new(PaletteModeManager.Office2010BlueDarkMode, "Office 2010 - Blue (Dark Mode)"),
            new(PaletteModeManager.Office2010BlueLightMode, "Office 2010 - Blue (Light Mode)"),
            // Note: Re-enable when the gray themes are completed
            new(PaletteModeManager.Office2010DarkGray, "Office 2010 - Dark Gray"),
            //new(PaletteModeManager.Office2010LightGray, "Office 2010 - Light Gray"),
            new(PaletteModeManager.Office2010Silver, "Office 2010 - Silver"),
            new(PaletteModeManager.Office2010SilverDarkMode, "Office 2010 - Silver (Dark Mode)"),
            new(PaletteModeManager.Office2010SilverLightMode, "Office 2010 - Silver (Light Mode)"),
            new(PaletteModeManager.Office2010White, "Office 2010 - White"),
            // Note: Re-enable when the gray themes are completed
            new(PaletteModeManager.Office2013DarkGray, "Office 2013 - Dark Gray"),
            //new(PaletteModeManager.Office2013LightGray, "Office 2013 - Light Gray"),
            new(PaletteModeManager.Office2013White, "Office 2013 - White"),
            new(PaletteModeManager.Office365Black, "Office 365 - Black"),
            new(PaletteModeManager.Office365BlackDarkMode, "Office 365 - Black (Dark Mode)"),
            new(PaletteModeManager.Office365Blue, "Office 365 - Blue"),
            new(PaletteModeManager.Office365BlueDarkMode, "Office 365 - Blue (Dark Mode)"),
            new(PaletteModeManager.Office365BlueLightMode, "Office 365 - Blue (Light Mode)"),
            // Note: Re-enable when the gray themes are completed
            new(PaletteModeManager.Office365DarkGray, "Office 365 - Dark Gray"),
            //new(PaletteModeManager.Office365LightGray, "Office 365 - Light Gray"),
            new(PaletteModeManager.Office365Silver, "Office 365 - Silver"),
            new(PaletteModeManager.Office365SilverDarkMode, "Office 365 - Silver (Dark Mode)"),
            new(PaletteModeManager.Office365SilverLightMode, "Office 365 - Silver (Light Mode)"),
            new(PaletteModeManager.Office365White, "Office 365 - White"),
            new(PaletteModeManager.SparkleBlue, "Sparkle - Blue"),
            new(PaletteModeManager.SparkleBlueDarkMode, "Sparkle - Blue (Dark Mode)"),
            new(PaletteModeManager.SparkleBlueLightMode, "Sparkle - Blue (Light Mode)"),
            new(PaletteModeManager.SparkleOrange, "Sparkle - Orange"),
            new(PaletteModeManager.SparkleOrangeDarkMode, "Sparkle - Orange (Dark Mode)"),
            new(PaletteModeManager.SparkleOrangeLightMode, "Sparkle - Orange (Light Mode)"),
            new(PaletteModeManager.SparklePurple, "Sparkle - Purple"),
            new(PaletteModeManager.SparklePurpleDarkMode, "Sparkle - Purple (Dark Mode)"),
            new(PaletteModeManager.SparklePurpleLightMode, "Sparkle - Purple (Light Mode)")
            //new(PaletteModeManager.VisualStudioDark,      "Visual Studio (Dark Mode)"),
            //new(PaletteModeManager.VisualStudioLight,     "Visual Studio (Light Mode)")
        };
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteModeManagerConverter class.
        /// </summary>
        public PaletteModeManagerConverter()
            : base(typeof(PaletteModeManager))
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

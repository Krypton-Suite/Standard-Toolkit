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
    /// <summary>
    /// Custom type converter so that PaletteMode values appear as neat text at design time.
    /// </summary>
    public class PaletteModeConverter : StringLookupConverter
    {
        #region Static Fields

        #region Old Array

        /*private readonly Pair[] _pairs =
        {
            new(PaletteMode.ProfessionalSystem, "Professional - System"),
            new(PaletteMode.ProfessionalOffice2003, "Professional - Office 2003"),
            new(PaletteMode.Office2007Black, "Office 2007 - Black"),
            new(PaletteMode.Office2007BlackDarkMode, "Office 2007 - Black (Dark Mode)"),
            new(PaletteMode.Office2007Blue, "Office 2007 - Blue"),
            new(PaletteMode.Office2007BlueDarkMode, "Office 2007 - Blue (Dark Mode)"),
            new(PaletteMode.Office2007BlueLightMode, "Office 2007 - Blue (Light Mode)"),
            // Note: Re-enable when the gray themes are completed
            new(PaletteMode.Office2007DarkGray, "Office 2007 - Dark Gray"),
            //new(PaletteMode.Office2007LightGray, "Office 2007 - Light Gray"),
            new(PaletteMode.Office2007Silver, "Office 2007 - Silver"),
            new(PaletteMode.Office2007SilverDarkMode, "Office 2007 - Silver (Dark Mode)"),
            new(PaletteMode.Office2007SilverLightMode, "Office 2007 - Silver (Light Mode)"),
            new(PaletteMode.Office2007White, "Office 2007 - White"),
            new(PaletteMode.Office2010Black, "Office 2010 - Black"),
            new(PaletteMode.Office2010BlackDarkMode, "Office 2010 - Black (Dark Mode)"),
            new(PaletteMode.Office2010Blue, "Office 2010 - Blue"),
            new(PaletteMode.Office2010BlueDarkMode, "Office 2010 - Blue (Dark Mode)"),
            new(PaletteMode.Office2010BlueLightMode, "Office 2010 - Blue (Light Mode)"),
            // Note: Re-enable when the gray themes are completed
            new(PaletteMode.Office2010DarkGray, "Office 2010 - Dark Gray"),
            //new(PaletteMode.Office2010LightGray, "Office 2010 - Light Gray"),
            new(PaletteMode.Office2010Silver, "Office 2010 - Silver"),
            new(PaletteMode.Office2010SilverDarkMode, "Office 2010 - Silver (Dark Mode)"),
            new(PaletteMode.Office2010SilverLightMode, "Office 2010 - Silver (Light Mode)"),
            new(PaletteMode.Office2010White, "Office 2010 - White"),
            // Note: Re-enable when the gray themes are completed
            new(PaletteMode.Office2013DarkGray, "Office 2013 - Dark Gray"),
            //new(PaletteMode.Office2013LightGray, "Office 2013 - Light Gray"),
            new(PaletteMode.Office2013White, "Office 2013 - White"),
            new(PaletteMode.Microsoft365Black, "Microsoft 365 - Black"),
            new(PaletteMode.Microsoft365BlackDarkMode, "Microsoft 365 - Black (Dark Mode)"),
            new(PaletteMode.Microsoft365Blue, "Microsoft 365 - Blue"),
            new(PaletteMode.Microsoft365BlueDarkMode, "Microsoft 365 - Blue (Dark Mode)"),
            new(PaletteMode.Microsoft365BlueLightMode, "Microsoft 365 - Blue (Light Mode)"),
            // Note: Re-enable when the gray themes are completed
            new(PaletteMode.Microsoft365DarkGray, "Microsoft 365 - Dark Gray"),
            //new(PaletteMode.Microsoft365LightGray, "Microsoft 365 - Light Gray"),
            new(PaletteMode.Microsoft365Silver, "Microsoft 365 - Silver"),
            new(PaletteMode.Microsoft365SilverDarkMode, "Microsoft 365 - Silver (Dark Mode)"),
            new(PaletteMode.Microsoft365SilverLightMode, "Microsoft 365 - Silver (Light Mode)"),
            new(PaletteMode.Microsoft365White, "Microsoft 365 - White"),
            new(PaletteMode.SparkleBlue, "Sparkle - Blue"),
            new(PaletteMode.SparkleBlueDarkMode, "Sparkle - Blue (Dark Mode)"),
            new(PaletteMode.SparkleBlueLightMode, "Sparkle - Blue (Light Mode)"),
            new(PaletteMode.SparkleOrange, "Sparkle - Orange"),
            new(PaletteMode.SparkleOrangeDarkMode, "Sparkle - Orange (Dark Mode)"),
            new(PaletteMode.SparkleOrangeLightMode, "Sparkle - Orange (Light Mode)"),
            new(PaletteMode.SparklePurple, "Sparkle - Purple"),
            new(PaletteMode.SparklePurpleDarkMode, "Sparkle - Purple (Dark Mode)"),
            new(PaletteMode.SparklePurpleLightMode, "Sparkle - Purple (Light Mode)")
            //new(PaletteMode.VisualStudioDark,      "Visual Studio (Dark Mode)"),
            //new(PaletteMode.VisualStudioLight,     "Visual Studio (Light Mode)")
        };*/

        #endregion

        private readonly Pair[] _pairs =
       {
           new Pair(PaletteMode.ProfessionalSystem, KryptonLanguageManager.ModeStrings.Professional),
           new Pair(PaletteMode.ProfessionalOffice2003, KryptonLanguageManager.ModeStrings.Professional2003),
           new Pair(PaletteMode.Office2007Black, KryptonLanguageManager.ModeStrings.Office2007Black),
           new Pair(PaletteMode.Office2007BlackDarkMode, KryptonLanguageManager.ModeStrings.Office2007BlackDarkMode),
           new Pair(PaletteMode.Office2007Blue, KryptonLanguageManager.ModeStrings.Office2007Blue),
           new Pair(PaletteMode.Office2007BlueDarkMode, KryptonLanguageManager.ModeStrings.Office2007BlueDarkMode),
           new Pair(PaletteMode.Office2007BlueLightMode, KryptonLanguageManager.ModeStrings.Office2007BlueLightMode),
           // Note: Re-enable when the gray themes are completed
           new Pair(PaletteMode.Office2007DarkGray, KryptonLanguageManager.ModeStrings.Office2007DarkGray),
           //new(PaletteMode.Office2007LightGray, KryptonLanguageManager.ModeStrings.Office2007LightGray),
           new Pair(PaletteMode.Office2007Silver, KryptonLanguageManager.ModeStrings.Office2007Silver),
           new Pair(PaletteMode.Office2007SilverDarkMode, KryptonLanguageManager.ModeStrings.Office2007SilverDarkMode),
           new Pair(PaletteMode.Office2007SilverLightMode, KryptonLanguageManager.ModeStrings.Office2007SilverLightMode),
           new Pair(PaletteMode.Office2007White, KryptonLanguageManager.ModeStrings.Office2007White),
           new Pair(PaletteMode.Office2010Black, KryptonLanguageManager.ModeStrings.Office2010Black),
           new Pair(PaletteMode.Office2010BlackDarkMode, KryptonLanguageManager.ModeStrings.Office2010BlackDarkMode),
           new Pair(PaletteMode.Office2010Blue, KryptonLanguageManager.ModeStrings.Office2010Blue),
           new Pair(PaletteMode.Office2010BlueDarkMode, KryptonLanguageManager.ModeStrings.Office2010BlueDarkMode),
           new Pair(PaletteMode.Office2010BlueLightMode, KryptonLanguageManager.ModeStrings.Office2010BlueLightMode),
           // Note: Re-enable when the gray themes are completed
           new Pair(PaletteMode.Office2010DarkGray, KryptonLanguageManager.ModeStrings.Office2010DarkGray),
           //new(PaletteMode.Office2010LightGray, KryptonLanguageManager.ModeStrings.Office2010LightGray),
           new Pair(PaletteMode.Office2010Silver, KryptonLanguageManager.ModeStrings.Office2010Silver),
           new Pair(PaletteMode.Office2010SilverDarkMode, KryptonLanguageManager.ModeStrings.Office2010SilverDarkMode),
           new Pair(PaletteMode.Office2010SilverLightMode, KryptonLanguageManager.ModeStrings.Office2010SilverLightMode),
           new Pair(PaletteMode.Office2010White, KryptonLanguageManager.ModeStrings.Office2010White),
           // Note: Re-enable when the gray themes are completed
           new Pair(PaletteMode.Office2013DarkGray, KryptonLanguageManager.ModeStrings.Office2013DarkGray),
           //new(PaletteMode.Office2013LightGray, KryptonLanguageManager.ModeStrings.Office2013LightGray),
           new Pair(PaletteMode.Office2013White, KryptonLanguageManager.ModeStrings.Office2013White),
           new Pair(PaletteMode.Microsoft365Black, KryptonLanguageManager.ModeStrings.Microsoft365Black),
           new Pair(PaletteMode.Microsoft365BlackDarkMode, KryptonLanguageManager.ModeStrings.Microsoft365BlackDarkMode),
           new Pair(PaletteMode.Microsoft365Blue, KryptonLanguageManager.ModeStrings.Microsoft365Blue),
           new Pair(PaletteMode.Microsoft365BlueDarkMode, KryptonLanguageManager.ModeStrings.Microsoft365BlueDarkMode),
           new Pair(PaletteMode.Microsoft365BlueLightMode, KryptonLanguageManager.ModeStrings.Microsoft365BlueLightMode),
           // Note: Re-enable when the gray themes are completed
           new Pair(PaletteMode.Microsoft365DarkGray, KryptonLanguageManager.ModeStrings.Microsoft365DarkGray),
           //new(PaletteMode.Microsoft365LightGray, KryptonLanguageManager.ModeStrings.Microsoft365LightGray),
           new Pair(PaletteMode.Microsoft365Silver, KryptonLanguageManager.ModeStrings.Microsoft365Silver),
           new Pair(PaletteMode.Microsoft365SilverDarkMode,
               KryptonLanguageManager.ModeStrings.Microsoft365SilverDarkMode),
           new Pair(PaletteMode.Microsoft365SilverLightMode,
               KryptonLanguageManager.ModeStrings.Microsoft365SilverLightMode),
           new Pair(PaletteMode.Microsoft365White, KryptonLanguageManager.ModeStrings.Microsoft365White),
           new Pair(PaletteMode.SparkleBlue, KryptonLanguageManager.ModeStrings.SparkleBlue),
           new Pair(PaletteMode.SparkleBlueDarkMode, KryptonLanguageManager.ModeStrings.SparkleBlueDarkMode),
           new Pair(PaletteMode.SparkleBlueLightMode, KryptonLanguageManager.ModeStrings.SparkleBlueLightMode),
           new Pair(PaletteMode.SparkleOrange, KryptonLanguageManager.ModeStrings.SparkleOrange),
           new Pair(PaletteMode.SparkleOrangeDarkMode, KryptonLanguageManager.ModeStrings.SparkleOrangeDarkMode),
           new Pair(PaletteMode.SparkleOrangeLightMode, KryptonLanguageManager.ModeStrings.SparkleOrangeLightMode),
           new Pair(PaletteMode.SparklePurple, KryptonLanguageManager.ModeStrings.SparklePurple),
           new Pair(PaletteMode.SparklePurpleDarkMode, KryptonLanguageManager.ModeStrings.SparklePurpleDarkMode),
           new Pair(PaletteMode.SparklePurpleLightMode, KryptonLanguageManager.ModeStrings.SparklePurpleLightMode)
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

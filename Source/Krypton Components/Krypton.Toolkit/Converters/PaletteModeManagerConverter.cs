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
    /// Custom type converter so that PaletteModeManager values appear as neat text at design time.
    /// </summary>
    internal class PaletteModeManagerConverter : StringLookupConverter
    {
        #region Static Fields

        private readonly Pair[] _pairs =
        {
            new(PaletteModeManager.ProfessionalSystem, "Professional - System"),
            new(PaletteModeManager.ProfessionalOffice2003, "Professional - Office 2003"),
            new(PaletteModeManager.Office2007DarkGray, "Office 2007 - Dark Grey"),
            new(PaletteModeManager.Office2007LightGray, "Office 2007 - Light Grey"),
            new(PaletteModeManager.Office2007Access, "Office 2007 - Access"),
            new(PaletteModeManager.Office2007Excel, "Office 2007 - Excel"),
            new(PaletteModeManager.Office2007Groove, "Office 2007 - Groove"),
            new(PaletteModeManager.Office2007InfoPath, "Office 2007 - InfoPath"),
            new(PaletteModeManager.Office2007OneNote, "Office 2007 - OneNote"),
            new(PaletteModeManager.Office2007Outlook, "Office 2007 - Outlook"),
            new(PaletteModeManager.Office2007PowerPoint, "Office 2007 - PowerPoint"),
            new(PaletteModeManager.Office2007Project, "Office 2007 - Project"),
            new(PaletteModeManager.Office2007Publisher, "Office 2007 - Publisher"),
            new(PaletteModeManager.Office2007Visio, "Office 2007 - Visio"),
            new(PaletteModeManager.Office2007Word, "Office 2007 - Word"),
            new(PaletteModeManager.Office2007Blue, "Office 2007 - Blue"),
            new(PaletteModeManager.Office2007BlueDarkMode, "Office 2007 - Blue (Dark Mode)"),
            new(PaletteModeManager.Office2007BlueLightMode, "Office 2007 - Blue (Light Mode)"),
            new(PaletteModeManager.Office2007Silver, "Office 2007 - Silver"),
            new(PaletteModeManager.Office2007SilverDarkMode, "Office 2007 - Silver (Dark Mode)"),
            new(PaletteModeManager.Office2007SilverLightMode, "Office 2007 - Silver (Light Mode)"),
            new(PaletteModeManager.Office2007White, "Office 2007 - White"),
            new(PaletteModeManager.Office2007Black, "Office 2007 - Black"),
            new(PaletteModeManager.Office2007BlackDarkMode, "Office 2007 - Black (Dark Mode)"),
            new(PaletteModeManager.Office2010DarkGray, "Office 2010 - Dark Grey"),
            new(PaletteModeManager.Office2010LightGray, "Office 2010 - Light Grey"),
            new(PaletteModeManager.Office2010Access, "Office 2010 - Access"),
            new(PaletteModeManager.Office2010Excel, "Office 2010 - Excel"),
            new(PaletteModeManager.Office2010InfoPath, "Office 2010 - InfoPath"),
            new(PaletteModeManager.Office2010OneNote, "Office 2010 - OneNote"),
            new(PaletteModeManager.Office2010Outlook, "Office 2010 - Outlook"),
            new(PaletteModeManager.Office2010PowerPoint, "Office 2010 - PowerPoint"),
            new(PaletteModeManager.Office2010Project, "Office 2010 - Project"),
            new(PaletteModeManager.Office2010Publisher, "Office 2010 - Publisher"),
            new(PaletteModeManager.Office2010SharePoint, "Office 2010 - SharePoint"),
            new(PaletteModeManager.Office2010Visio, "Office 2010 - Visio"),
            new(PaletteModeManager.Office2010Word, "Office 2010 - Word"),
            new(PaletteModeManager.Office2010Blue, "Office 2010 - Blue"),
            new(PaletteModeManager.Office2010BlueDarkMode, "Office 2010 - Blue (Dark Mode)"),
            new(PaletteModeManager.Office2010BlueLightMode, "Office 2010 - Blue (Light Mode)"),
            new(PaletteModeManager.Office2010Silver, "Office 2010 - Silver"),
            new(PaletteModeManager.Office2010SilverDarkMode, "Office 2010 - Silver (Dark Mode)"),
            new(PaletteModeManager.Office2010SilverLightMode, "Office 2010 - Silver (Light Mode)"),
            new(PaletteModeManager.Office2010White, "Office 2010 - White"),
            new(PaletteModeManager.Office2010Black, "Office 2010 - Black"),
            new(PaletteModeManager.Office2010BlackDarkMode, "Office 2010 - Black (Dark Mode)"),
            new(PaletteModeManager.Office2013DarkGray, "Office 2013 - Dark Grey"),
            new(PaletteModeManager.Office2013LightGray, "Office 2013 - Light Grey"),
            new(PaletteModeManager.Office2013Access, "Office 2013 - Access"),
            new(PaletteModeManager.Office2013AccessLegacy, "Office 2013 - Access (Pre-2013 Colours)"),
            new(PaletteModeManager.Office2013Excel, "Office 2013 - Excel"),
            new(PaletteModeManager.Office2013InfoPath, "Office 2013 - InfoPath"),
            new(PaletteModeManager.Office2013Lync, "Office 2013 - Lync"),
            new(PaletteModeManager.Office2013OneNote, "Office 2013 - OneNote"),
            new(PaletteModeManager.Office2013Outlook, "Office 2013 - Outlook"),
            new(PaletteModeManager.Office2013PowerPoint, "Office 2013 - PowerPoint"),
            new(PaletteModeManager.Office2013Project, "Office 2013 - Project"),
            new(PaletteModeManager.Office2013Publisher, "Office 2013 - Publisher"),
            new(PaletteModeManager.Office2013SharePoint, "Office 2013 - SharePoint"),
            new(PaletteModeManager.Office2013Visio, "Office 2013 - Visio"),
            new(PaletteModeManager.Office2013Word, "Office 2013 - Word"),
            new(PaletteModeManager.Office2013White, "Office 2013 - White"),
            new(PaletteModeManager.Office365DarkGray, "Office 365 - Dark Grey"),
            new(PaletteModeManager.Office365LightGray, "Office 365 - Light Grey"),
            new(PaletteModeManager.Office365Access, "Office 365 - Access"),
            new(PaletteModeManager.Office2013AccessLegacy, "Office 365 - Access (Pre-2013 Colours)"),
            new(PaletteModeManager.Office365Excel, "Office 365 - Excel"),
            new(PaletteModeManager.Office365InfoPath, "Office 365 - InfoPath"),
            new(PaletteModeManager.Office365Lync, "Office 365 - Lync"),
            new(PaletteModeManager.Office365OneNote, "Office 365 - OneNote"),
            new(PaletteModeManager.Office365Outlook, "Office 365 - Outlook"),
            new(PaletteModeManager.Office365PowerPoint, "Office 365 - PowerPoint"),
            new(PaletteModeManager.Office365Project, "Office 365 - Project"),
            new(PaletteModeManager.Office365Publisher, "Office 365 - Publisher"),
            new(PaletteModeManager.Office365SharePoint, "Office 365 - SharePoint"),
            new(PaletteModeManager.Office365Visio, "Office 365 - Visio"),
            new(PaletteModeManager.Office365Word, "Office 365 - Word"),
            new(PaletteModeManager.Office365Black, "Office 365 - Black"),
            new(PaletteModeManager.Office365BlackDarkMode, "Office 365 - Black (Dark Mode)"),
            new(PaletteModeManager.Office365Blue, "Office 365 - Blue"),
            new(PaletteModeManager.Office365BlueDarkMode, "Office 365 - Blue (Dark Mode)"),
            new(PaletteModeManager.Office365BlueLightMode, "Office 365 - Blue (Light Mode)"),
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

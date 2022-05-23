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
        private readonly Pair[] _pairs =
        {
            new(PaletteMode.ProfessionalSystem, "Professional - System"),
            new(PaletteMode.ProfessionalOffice2003, "Professional - Office 2003"),
            new(PaletteMode.Office2007DarkGray, "Office 2007 - Dark Grey"),
            new(PaletteMode.Office2007LightGray, "Office 2007 - Light Grey"),
            new(PaletteMode.Office2007Access, "Office 2007 - Access"),
            new(PaletteMode.Office2007Excel, "Office 2007 - Excel"),
            new(PaletteMode.Office2007Groove, "Office 2007 - Groove"),
            new(PaletteMode.Office2007InfoPath, "Office 2007 - InfoPath"),
            new(PaletteMode.Office2007OneNote, "Office 2007 - OneNote"),
            new(PaletteMode.Office2007Outlook, "Office 2007 - Outlook"),
            new(PaletteMode.Office2007PowerPoint, "Office 2007 - PowerPoint"),
            new(PaletteMode.Office2007Project, "Office 2007 - Project"),
            new(PaletteMode.Office2007Publisher, "Office 2007 - Publisher"),
            new(PaletteMode.Office2007Visio, "Office 2007 - Visio"),
            new(PaletteMode.Office2007Word, "Office 2007 - Word"),
            new(PaletteMode.Office2007Blue, "Office 2007 - Blue"),
            new(PaletteMode.Office2007BlueDarkMode, "Office 2007 - Blue (Dark Mode)"),
            new(PaletteMode.Office2007BlueLightMode, "Office 2007 - Blue (Light Mode)"),
            new(PaletteMode.Office2007Silver, "Office 2007 - Silver"),
            new(PaletteMode.Office2007SilverDarkMode, "Office 2007 - Silver (Dark Mode)"),
            new(PaletteMode.Office2007SilverLightMode, "Office 2007 - Silver (Light Mode)"),
            new(PaletteMode.Office2007White, "Office 2007 - White"),
            new(PaletteMode.Office2007Black, "Office 2007 - Black"),
            new(PaletteMode.Office2007BlackDarkMode, "Office 2007 - Black (Dark Mode)"),
            new(PaletteMode.Office2010DarkGray, "Office 2010 - Dark Grey"),
            new(PaletteMode.Office2010LightGray, "Office 2010 - Light Grey"),
            new(PaletteMode.Office2010Access, "Office 2010 - Access"),
            new(PaletteMode.Office2010Excel, "Office 2010 - Excel"),
            new(PaletteMode.Office2010InfoPath, "Office 2010 - InfoPath"),
            new(PaletteMode.Office2010OneNote, "Office 2010 - OneNote"),
            new(PaletteMode.Office2010Outlook, "Office 2010 - Outlook"),
            new(PaletteMode.Office2010PowerPoint, "Office 2010 - PowerPoint"),
            new(PaletteMode.Office2010Project, "Office 2010 - Project"),
            new(PaletteMode.Office2010Publisher, "Office 2010 - Publisher"),
            new(PaletteMode.Office2010SharePoint, "Office 2010 - SharePoint"),
            new(PaletteMode.Office2010Visio, "Office 2010 - Visio"),
            new(PaletteMode.Office2010Word, "Office 2010 - Word"),
            new(PaletteMode.Office2010Blue, "Office 2010 - Blue"),
            new(PaletteMode.Office2010BlueDarkMode, "Office 2010 - Blue (Dark Mode)"),
            new(PaletteMode.Office2010BlueLightMode, "Office 2010 - Blue (Light Mode)"),
            new(PaletteMode.Office2010Silver, "Office 2010 - Silver"),
            new(PaletteMode.Office2010SilverDarkMode, "Office 2010 - Silver (Dark Mode)"),
            new(PaletteMode.Office2010SilverLightMode, "Office 2010 - Silver (Light Mode)"),
            new(PaletteMode.Office2010White, "Office 2010 - White"),
            new(PaletteMode.Office2010Black, "Office 2010 - Black"),
            new(PaletteMode.Office2010BlackDarkMode, "Office 2010 - Black (Dark Mode)"),
            new(PaletteMode.Office2013DarkGray, "Office 2013 - Dark Grey"),
            new(PaletteMode.Office2013LightGray, "Office 2013 - Light Grey"),
            new(PaletteMode.Office2013Access, "Office 2013 - Access"),
            new(PaletteMode.Office2013AccessLegacy, "Office 2013 - Access (Pre-2013 colours)"),
            new(PaletteMode.Office2013Excel, "Office 2013 - Excel"),
            new(PaletteMode.Office2013InfoPath, "Office 2013 - InfoPath"),
            new(PaletteMode.Office2013Lync, "Office 2013 - Lync"),
            new(PaletteMode.Office2013OneNote, "Office 2013 - OneNote"),
            new(PaletteMode.Office2013Outlook, "Office 2013 - Outlook"),
            new(PaletteMode.Office2013PowerPoint, "Office 2013 - PowerPoint"),
            new(PaletteMode.Office2013Project, "Office 2013 - Project"),
            new(PaletteMode.Office2013Publisher, "Office 2013 - Publisher"),
            new(PaletteMode.Office2013SharePoint, "Office 2013 - SharePoint"),
            new(PaletteMode.Office2013Visio, "Office 2013 - Visio"),
            new(PaletteMode.Office2013Word, "Office 2013 - Word"),
            new(PaletteMode.Office2013White, "Office 2013 - White"),
            new(PaletteMode.Office365DarkGray, "Office 365 - Dark Grey"),
            new(PaletteMode.Office365LightGray, "Office 365 - Light Grey"),
            new(PaletteMode.Office365Access, "Office 365 - Access"),
            new(PaletteMode.Office2013AccessLegacy, "Office 365 - Access (Pre-2013 colours)"),
            new(PaletteMode.Office365Excel, "Office 365 - Excel"),
            new(PaletteMode.Office365InfoPath, "Office 365 - InfoPath"),
            new(PaletteMode.Office365Lync, "Office 365 - Lync"),
            new(PaletteMode.Office365OneNote, "Office 365 - OneNote"),
            new(PaletteMode.Office365Outlook, "Office 365 - Outlook"),
            new(PaletteMode.Office365PowerPoint, "Office 365 - PowerPoint"),
            new(PaletteMode.Office365Project, "Office 365 - Project"),
            new(PaletteMode.Office365Publisher, "Office 365 - Publisher"),
            new(PaletteMode.Office365SharePoint, "Office 365 - SharePoint"),
            new(PaletteMode.Office365Visio, "Office 365 - Visio"),
            new(PaletteMode.Office365Word, "Office 365 - Word"),
            new(PaletteMode.Office365Black, "Office 365 - Black"),
            new(PaletteMode.Office365BlackDarkMode, "Office 365 - Black (Dark Mode)"),
            new(PaletteMode.Office365Blue, "Office 365 - Blue"),
            new(PaletteMode.Office365BlueDarkMode, "Office 365 - Blue (Dark Mode)"),
            new(PaletteMode.Office365BlueLightMode, "Office 365 - Blue (Light Mode)"),
            new(PaletteMode.Office365Silver, "Office 365 - Silver"),
            new(PaletteMode.Office365SilverDarkMode, "Office 365 - Silver (Dark Mode)"),
            new(PaletteMode.Office365SilverLightMode, "Office 365 - Silver (Light Mode)"),
            new(PaletteMode.Office365White, "Office 365 - White"),
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

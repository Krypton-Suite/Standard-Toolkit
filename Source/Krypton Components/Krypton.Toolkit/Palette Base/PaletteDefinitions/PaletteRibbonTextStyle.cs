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
    /// Specifies the style of ribbon text.
    /// </summary>
    public enum PaletteRibbonTextStyle
    {
        /// <summary>
        /// Specifies a text style appropriate for a normal group title.
        /// </summary>
        RibbonGroupNormalTitle,

        /// <summary>
        /// Specifies a text style appropriate for a collapsed group text.
        /// </summary>
        RibbonGroupCollapsedText,

        /// <summary>
        /// Specifies a text style appropriate for a group button text.
        /// </summary>
        RibbonGroupButtonText,

        /// <summary>
        /// Specifies a text style appropriate for a group label text.
        /// </summary>
        RibbonGroupLabelText,

        /// <summary>
        /// Specifies a text style appropriate for a group check box button text.
        /// </summary>
        RibbonGroupCheckBoxText,

        /// <summary>
        /// Specifies a text style appropriate for a group radio button text.
        /// </summary>
        RibbonGroupRadioButtonText,

        /// <summary>
        /// Specifies a text style appropriate for a ribbon tab.
        /// </summary>
        RibbonTab,

        /// <summary>
        /// Specifies a text style appropriate for a app menu recent documents title.
        /// </summary>
        RibbonAppMenuDocsTitle,

        /// <summary>
        /// Specifies a text style appropriate for a app menu recent documents entry.
        /// </summary>
        RibbonAppMenuDocsEntry
    }
}
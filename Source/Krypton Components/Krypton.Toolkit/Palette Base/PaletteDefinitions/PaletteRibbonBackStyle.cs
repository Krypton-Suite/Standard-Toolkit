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
    /// Specifies the style of ribbon background.
    /// </summary>
    public enum PaletteRibbonBackStyle
    {
        /// <summary>
        /// Specifies a background style appropriate for an application button.
        /// </summary>
        RibbonAppButton,

        /// <summary>
        /// Specifies a background style appropriate for an application menu inner area.
        /// </summary>
        RibbonAppMenuInner,

        /// <summary>
        /// Specifies a background style appropriate for an application menu outer area.
        /// </summary>
        RibbonAppMenuOuter,

        /// <summary>
        /// Specifies a background style appropriate for an application menu recent documents area.
        /// </summary>
        RibbonAppMenuDocs,

        /// <summary>
        /// Specifies a background style appropriate for a group area.
        /// </summary>
        RibbonGroupArea,

        /// <summary>
        /// Specifies a background style appropriate for a normal group border.
        /// </summary>
        RibbonGroupNormalBorder,

        /// <summary>
        /// Specifies a background style appropriate for a normal group title.
        /// </summary>
        RibbonGroupNormalTitle,

        /// <summary>
        /// Specifies a background style appropriate for a collapsed group border.
        /// </summary>
        RibbonGroupCollapsedBack,

        /// <summary>
        /// Specifies a border style appropriate for a collapsed group border.
        /// </summary>
        RibbonGroupCollapsedBorder,

        /// <summary>
        /// Specifies a background style appropriate for a collapsed group frame border.
        /// </summary>
        RibbonGroupCollapsedFrameBack,

        /// <summary>
        /// Specifies a border style appropriate for a collapsed group frame border.
        /// </summary>
        RibbonGroupCollapsedFrameBorder,

        /// <summary>
        /// Specifies a background style appropriate for a ribbon tab.
        /// </summary>
        RibbonTab,

        /// <summary>
        /// Specifies a background style appropriate for a ribbon quick access toolbar in full mode.
        /// </summary>
        RibbonQATFullbar,

        /// <summary>
        /// Specifies a background style appropriate for a ribbon quick access toolbar in mini mode.
        /// </summary>
        RibbonQATMinibar,

        /// <summary>
        /// Specifies a background style appropriate for a ribbon quick access toolbar in overflow.
        /// </summary>
        RibbonQATOverflow,

        /// <summary>
        /// Specifies a background style appropriate for a gallery.
        /// </summary>
        RibbonGalleryBack,

        /// <summary>
        /// Specifies a border style appropriate for a gallery.
        /// </summary>
        RibbonGalleryBorder
    }
}
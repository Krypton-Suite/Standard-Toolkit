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
    /// Specifies the color drawing style for ribbon elements.
    /// </summary>
    public enum PaletteRibbonColorStyle
    {
        /// <summary>
        /// Specifies color style should be inherited.
        /// </summary>
        Inherit,

        /// <summary>
        /// Specifies that no drawing take place.
        /// </summary>
        Empty,

        /// <summary>
        /// Specifies solid drawing using the first color.
        /// </summary>
        Solid,

        /// <summary>
        /// Specifies linear gradient from first to second color.
        /// </summary>
        Linear,

        /// <summary>
        /// Specifies linear gradient border from first to second color.
        /// </summary>
        LinearBorder,

        /// <summary>
        /// Specifies using colors to draw a application menu inner area.
        /// </summary>
        RibbonAppMenuInner,

        /// <summary>
        /// Specifies using colors to draw a application menu inner area.
        /// </summary>
        RibbonAppMenuOuter,

        /// <summary>
        /// Specifies using colors to draw a tracking ribbon tab appropriate for Office 2007.
        /// </summary>
        RibbonTabTracking2007,

        /// <summary>
        /// Specifies using colors to draw a focused ribbon tab appropriate for Office 2010.
        /// </summary>
        RibbonTabFocus2010,

        /// <summary>
        /// Specifies using colors to draw a tracking ribbon tab appropriate for Office 2010.
        /// </summary>
        RibbonTabTracking2010,

        /// <summary>
        /// Specifies alternate drawing of the RibbonTabTracking2010 enumeration.
        /// </summary>
        RibbonTabTracking2010Alt,

        /// <summary>
        /// Specifies using colors to draw a glowing ribbon tab.
        /// </summary>
        RibbonTabGlowing,

        /// <summary>
        /// Specifies using colors to draw a selected ribbon tab appropriate for Office 2007.
        /// </summary>
        RibbonTabSelected2007,

        /// <summary>
        /// Specifies using colors to draw a selected ribbon tab appropriate for Office 2010.
        /// </summary>
        RibbonTabSelected2010,

        /// <summary>
        /// Specifies alternate drawing of the RibbonTabSelected2010 enumeration.
        /// </summary>
        RibbonTabSelected2010Alt,

        /// <summary>
        /// Specifies using colors to draw a selected and tracking ribbon tab.
        /// </summary>
        RibbonTabHighlight,

        /// <summary>
        /// Specifies using colors for an alternative way of drawing a selected and tracking ribbon tab.
        /// </summary>
        RibbonTabHighlight2,

        /// <summary>
        /// Specifies using colors to draw a context selected ribbon tab for Office 2007.
        /// </summary>
        RibbonTabContextSelected,

        /// <summary>
        /// Specifies using colors to draw a groups area border.
        /// </summary>
        RibbonGroupAreaBorder,

        /// <summary>
        /// Specifies using colors to draw a groups area border, variation 2.
        /// </summary>
        RibbonGroupAreaBorder2,

        /// <summary>
        /// Specifies using colors to draw a groups area border, variation 3.
        /// </summary>
        RibbonGroupAreaBorder3,

        /// <summary>
        /// Specifies using colors to draw a groups area border, variation 4.
        /// </summary>
        RibbonGroupAreaBorder4,

        /// <summary>
        /// Specifies using colors to draw a groups area border for a context selected tab.
        /// </summary>
        RibbonGroupAreaBorderContext,

        /// <summary>
        /// Specifies using colors to draw a group normal border.
        /// </summary>
        RibbonGroupNormalBorder,

        /// <summary>
        /// Specifies using colors to draw a group normal area.
        /// </summary>
        RibbonGroupNormal,

        /// <summary>
        /// Specifies using colors to draw a group pressed area, variation based on light background.
        /// </summary>
        RibbonGroupNormalPressedLight,

        /// <summary>
        /// Specifies using colors to draw a group pressed area, variation based on dark background.
        /// </summary>
        RibbonGroupNormalPressedDark,

        /// <summary>
        /// Specifies using colors to draw a group tracking area, variation based on light background.
        /// </summary>
        RibbonGroupNormalTrackingLight,

        /// <summary>
        /// Specifies using colors to draw a group tracking area, variation based on dark background.
        /// </summary>
        RibbonGroupNormalTrackingDark,

        /// <summary>
        /// Specifies using colors to draw a group normal border as a vertical separator.
        /// </summary>
        RibbonGroupNormalBorderSep,

        /// <summary>
        /// Specifies using colors to draw a group pressed border as a vertical separator, variation based on light background.
        /// </summary>
        RibbonGroupNormalBorderSepPressedLight,

        /// <summary>
        /// Specifies using colors to draw a group pressed border as a vertical separator, variation based on dark background.
        /// </summary>
        RibbonGroupNormalBorderSepPressedDark,

        /// <summary>
        /// Specifies using colors to draw a group tracking border as a vertical separator, variation based on light background.
        /// </summary>
        RibbonGroupNormalBorderSepTrackingLight,

        /// <summary>
        /// Specifies using colors to draw a group tracking border as a vertical separator, variation based on dark background.
        /// </summary>
        RibbonGroupNormalBorderSepTrackingDark,

        /// <summary>
        /// Specifies using colors to draw a tracking group normal border.
        /// </summary>
        RibbonGroupNormalBorderTracking,

        /// <summary>
        /// Specifies using colors to draw a tracking group normal border with light inside edge.
        /// </summary>
        RibbonGroupNormalBorderTrackingLight,

        /// <summary>
        /// Specifies using colors to draw a group normal title.
        /// </summary>
        RibbonGroupNormalTitle,

        /// <summary>
        /// Specifies using colors to draw a group collapsed border.
        /// </summary>
        RibbonGroupCollapsedBorder,

        /// <summary>
        /// Specifies using colors to draw a group collapsed frame border.
        /// </summary>
        RibbonGroupCollapsedFrameBorder,

        /// <summary>
        /// Specifies using colors to draw a group collapsed frame back.
        /// </summary>
        RibbonGroupCollapsedFrameBack,

        /// <summary>
        /// Specifies using colors to draw a one tone gradient in the groups area.
        /// </summary>
        RibbonGroupGradientOne,

        /// <summary>
        /// Specifies using colors to draw a two tone gradient in the groups area.
        /// </summary>
        RibbonGroupGradientTwo,

        /// <summary>
        /// Specifies using colors to draw a rounded quick access toolbar mini area with single rounded end.
        /// </summary>
        RibbonQATMinibarSingle,

        /// <summary>
        /// Specifies using colors to draw a rounded quick access toolbar mini area with double rounded end.
        /// </summary>
        RibbonQATMinibarDouble,

        /// <summary>
        /// Specifies using colors to draw a rounded quick access toolbar full area.
        /// </summary>
        RibbonQATFullbarRound,

        /// <summary>
        /// Specifies using colors to draw a square quick access toolbar full area.
        /// </summary>
        RibbonQATFullbarSquare,

        /// <summary>
        /// Specifies using colors to draw a rounded quick access toolbar overflow.
        /// </summary>
        RibbonQATOverflow
    }
}
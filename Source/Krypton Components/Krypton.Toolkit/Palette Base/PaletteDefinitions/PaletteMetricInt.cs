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
    /// Specifies a integer type metric.
    /// </summary>
    public enum PaletteMetricInt
    {
        /// <summary>
        /// Specifies that no integer metric is required.
        /// </summary>
        None,

        /// <summary>
        /// Specifies how far to inset a button on a primary header.
        /// </summary>
        HeaderButtonEdgeInsetPrimary,

        /// <summary>
        /// Specifies how far to inset a button on a secondary header.
        /// </summary>
        HeaderButtonEdgeInsetSecondary,

        /// <summary>
        /// Specifies how far to inset a button on an inactive dock header.
        /// </summary>
        HeaderButtonEdgeInsetDockInactive,

        /// <summary>
        /// Specifies how far to inset a button on an active dock header.
        /// </summary>
        HeaderButtonEdgeInsetDockActive,

        /// <summary>
        /// Specifies how far to inset a button on a main form header.
        /// </summary>
        HeaderButtonEdgeInsetForm,

        /// <summary>
        /// Specifies how far to inset a button on a calendar header.
        /// </summary>
        HeaderButtonEdgeInsetCalendar,

        /// <summary>
        /// Specifies how far to inset a button on a input control.
        /// </summary>
        HeaderButtonEdgeInsetInputControl,

        /// <summary>
        /// Specifies how far to inset a button on a custom1 header.
        /// </summary>
        HeaderButtonEdgeInsetCustom1,
        HeaderButtonEdgeInsetCustom2,
        HeaderButtonEdgeInsetCustom3,

        /// <summary>
        /// Specifies the padding from buttons to the outside control edge.
        /// </summary>
        BarButtonEdgeOutside,

        /// <summary>
        /// Specifies the padding for buttons to the bar.
        /// </summary>
        BarButtonEdgeInside,

        /// <summary>
        /// Specifies the padding from buttons to the page header content.
        /// </summary>
        PageButtonInset,

        /// <summary>
        /// Specifies the spacing gap been each check button.
        /// </summary>
        CheckButtonGap,

        /// <summary>
        /// Specifies the spacing gap been each ribbon tab.
        /// </summary>
        RibbonTabGap
    }
}
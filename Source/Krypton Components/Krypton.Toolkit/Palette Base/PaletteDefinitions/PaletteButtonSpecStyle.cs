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
    /// Specifies the style of button spec.
    /// </summary>
    [TypeConverter(typeof(PaletteButtonSpecStyleConverter))]
    public enum PaletteButtonSpecStyle
    {
        /// <summary>
        /// Specifies a general purpose button specification.
        /// </summary>
        Generic,

        /// <summary>
        /// Specifies a close button specification.
        /// </summary>
        Close,

        /// <summary>
        /// Specifies a context button specification.
        /// </summary>
        Context,

        /// <summary>
        /// Specifies a next button specification.
        /// </summary>
        Next,

        /// <summary>
        /// Specifies a previous button specification.
        /// </summary>
        Previous,

        /// <summary>
        /// Specifies a left pointing arrow button specification.
        /// </summary>
        ArrowLeft,

        /// <summary>
        /// Specifies a right pointing arrow button specification.
        /// </summary>
        ArrowRight,

        /// <summary>
        /// Specifies an upwards pointing arrow button specification.
        /// </summary>
        ArrowUp,

        /// <summary>
        /// Specifies a downwards pointing arrow button specification.
        /// </summary>
        ArrowDown,

        /// <summary>
        /// Specifies a drop down button specification.
        /// </summary>
        DropDown,

        /// <summary>
        /// Specifies a vertical pin specification.
        /// </summary>
        PinVertical,

        /// <summary>
        /// Specifies a horizontal pin specification.
        /// </summary>
        PinHorizontal,

        /// <summary>
        /// Specifies a form "Close" button specification.
        /// </summary>
        FormClose,

        /// <summary>
        /// Specifies a form "Minimize" button specification.
        /// </summary>
        FormMin,

        /// <summary>
        /// Specifies a form "Maximize" button specification.
        /// </summary>
        FormMax,

        /// <summary>
        /// Specifies a form "Restore" button specification.
        /// </summary>
        FormRestore,

        /// <summary>
        /// Specifies a form "Help" button specification.
        /// </summary>
        FormHelp,

        /// <summary>
        /// Specifies a pendant close button specification.
        /// </summary>
        PendantClose,

        /// <summary>
        /// Specifies a pendant minimize button specification.
        /// </summary>
        PendantMin,

        /// <summary>
        /// Specifies a pendant restore button specification.
        /// </summary>
        PendantRestore,

        /// <summary>
        /// Specifies a workspace maximize button specification.
        /// </summary>
        WorkspaceMaximize,

        /// <summary>
        /// Specifies a workspace maximize button specification.
        /// </summary>
        WorkspaceRestore,

        /// <summary>
        /// Specifies a ribbon minimize button specification.
        /// </summary>
        RibbonMinimize,

        /// <summary>
        /// Specifies a ribbon expand button specification.
        /// </summary>
        RibbonExpand,

        /// <summary>Specifies a new document button specification.</summary>
        New,

        /// <summary>Specifies the open button specification.</summary>
        Open,

        /// <summary>Specifies the save button specification.</summary>
        Save,

        /// <summary>Specifies the save as button specification.</summary>
        SaveAs,

        /// <summary>Specifies the save all button specification.</summary>
        SaveAll,

        /// <summary>Specifies the cut button specification.</summary>
        Cut,

        /// <summary>Specifies the copy button specification.</summary>
        Copy,

        /// <summary>Specifies the paste button specification.</summary>
        Paste,

        /// <summary>Specifies the undo button specification.</summary>
        Undo,

        /// <summary>Specifies the redo button specification.</summary>
        Redo,

        /// <summary>Specifies the page setup button specification.</summary>
        PageSetup,

        /// <summary>Specifies the print preview button specification.</summary>
        PrintPreview,

        /// <summary>Specifies the print button specification.</summary>
        Print,

        /// <summary>Specifies the quick print button specification.</summary>
        QuickPrint
    }
}
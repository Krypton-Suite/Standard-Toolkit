#region BSD License
// TODO: Put in the correct license info
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// The scrollbar arrow button states.
    /// </summary>
    internal enum ScrollBarArrowButtonState
    {
        /// <summary>
        /// Indicates the up arrow is in normal state.
        /// </summary>
        UPNORMAL,

        /// <summary>
        /// Indicates the up arrow is in hot state.
        /// </summary>
        UPHOT,

        /// <summary>
        /// Indicates the up arrow is in active state.
        /// </summary>
        UPACTIVE,

        /// <summary>
        /// Indicates the up arrow is in pressed state.
        /// </summary>
        UPPRESSED,

        /// <summary>
        /// Indicates the up arrow is in disabled state.
        /// </summary>
        UPDISABLED,

        /// <summary>
        /// Indicates the down arrow is in normal state.
        /// </summary>
        DOWNNORMAL,

        /// <summary>
        /// Indicates the down arrow is in hot state.
        /// </summary>
        DOWNHOT,

        /// <summary>
        /// Indicates the down arrow is in active state.
        /// </summary>
        DOWNACTIVE,

        /// <summary>
        /// Indicates the down arrow is in pressed state.
        /// </summary>
        DOWNPRESSED,

        /// <summary>
        /// Indicates the down arrow is in disabled state.
        /// </summary>
        DOWNDISABLED
    }

    /// <summary>
    /// Enum for the scrollbar orientation.
    /// </summary>
    public enum ScrollBarOrientation
    {
        /// <summary>
        /// Indicates a horizontal scrollbar.
        /// </summary>
        HORIZONTAL,

        /// <summary>
        /// Indicates a vertical scrollbar.
        /// </summary>
        VERTICAL
    }

    /// <summary>
    /// The scrollbar states.
    /// </summary>
    internal enum ScrollBarState
    {
        /// <summary>
        /// Indicates a normal scrollbar state.
        /// </summary>
        NORMAL,

        /// <summary>
        /// Indicates a hot scrollbar state.
        /// </summary>
        HOT,

        /// <summary>
        /// Indicates an active scrollbar state.
        /// </summary>
        ACTIVE,

        /// <summary>
        /// Indicates a pressed scrollbar state.
        /// </summary>
        PRESSED,

        /// <summary>
        /// Indicates a disabled scrollbar state.
        /// </summary>
        DISABLED
    }
}
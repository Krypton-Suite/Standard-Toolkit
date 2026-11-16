#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Shared;

/// <summary>
/// Provides definitions for interacting with Win32 scroll bars.
/// </summary>
public class WIN32ScrollBars
{
    /// <summary>
    /// Contains the range, page size, position, and tracking position of a standard scroll bar.
    /// </summary>
    /// <remarks>Used with the Win32 <c>GetScrollInfo</c> and <c>SetScrollInfo</c> functions. Initialize
    /// <c>cbSize</c> to the size of this structure and set <c>fMask</c> to specify which values to retrieve or
    /// update.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct ScrollInfo
    {
        /// <summary>
        /// Specifies the size, in bytes, of this structure.
        /// </summary>
        public int cbSize;
        /// <summary>
        /// Specifies which scroll bar information members to set or retrieve.
        /// </summary>
        /// <remarks>Combine the SIF_* flags to select the members used by the scroll bar information
        /// function.</remarks>
        public int fMask;
        /// <summary>
        /// Specifies the minimum scroll-bar position.
        /// </summary>
        public int nMin;
        /// <summary>
        /// The maximum position in the scroll range.
        /// </summary>
        public int nMax;
        /// <summary>
        /// The page size of the scroll bar.
        /// </summary>
        public int nPage;
        /// <summary>
        /// Specifies the current position of the scroll box.
        /// </summary>
        public int nPos;
        /// <summary>
        /// Specifies the scroll box position while it is being dragged.
        /// </summary>
        public int nTrackPos;
    }
}
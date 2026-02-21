#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

#if WEBVIEW2_AVAILABLE
namespace Krypton.Utilities;

/// <summary>
/// Internal helper class for Windows message constants and utilities used by KryptonWebView2.
/// </summary>
internal static class WebView2MessageHelper
{
    /// <summary>
    /// Windows message constants used by WebView2 control.
    /// </summary>
    internal static class WM_
    {
        /// <summary>
        /// The WM_CONTEXTMENU message notifies a window that the user clicked the right mouse button (right-clicked) in the window.
        /// </summary>
        internal const int CONTEXTMENU = 0x007B;

        /// <summary>
        /// The WM_PARENTNOTIFY message is sent to the parent of a child window when the child window is created or destroyed,
        /// or when the user clicks a mouse button while the cursor is over the child window.
        /// </summary>
        internal const int PARENTNOTIFY = 0x0210;

        /// <summary>
        /// The WM_RBUTTONDOWN message is posted when the user presses the right mouse button while the cursor is in the client area of a window.
        /// </summary>
        internal const int RBUTTONDOWN = 0x0204;
    }

    /// <summary>
    /// Extracts the low-order word from the given value.
    /// </summary>
    /// <param name="value">The value to extract the low-order word from.</param>
    /// <returns>The low-order word.</returns>
    internal static int LOWORD(IntPtr value)
    {
        var int32 = (int)value.ToInt64() & 0xFFFF;
        return (int32 > 32767) ? int32 - 65536 : int32;
    }

    /// <summary>
    /// Extracts the high-order word from the given value.
    /// </summary>
    /// <param name="value">The value to extract the high-order word from.</param>
    /// <returns>The high-order word.</returns>
    internal static int HIWORD(IntPtr value)
    {
        var int32 = ((int)value.ToInt64() >> 0x10) & 0xFFFF;
        return (int32 > 32767) ? int32 - 65536 : int32;
    }
}
#endif


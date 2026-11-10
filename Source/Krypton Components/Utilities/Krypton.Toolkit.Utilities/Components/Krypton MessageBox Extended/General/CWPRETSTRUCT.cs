#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Contains information about a window message that has been sent to a window procedure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct CWPRETSTRUCT
{
    /// <summary>
    /// The result of the message processing. The meaning of this value depends on the message that was processed.
    /// </summary>
    public IntPtr lResult;
    /// <summary>
    /// The additional message-specific information. The meaning of this value depends on the message that was processed.
    /// </summary>
    public IntPtr lParam;
    /// <summary>
    /// The first message parameter. The meaning of this value depends on the message that was processed.
    /// </summary>
    public IntPtr wParam;
    /// <summary>
    /// The message identifier.
    /// </summary>
    public uint message;
    /// <summary>
    /// A handle to the window that received the message.
    /// </summary>
    public IntPtr hwnd;
};
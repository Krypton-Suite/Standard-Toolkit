#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

[StructLayout(LayoutKind.Sequential)]
public struct CWPRETSTRUCT
{
    public IntPtr lResult;
    public IntPtr lParam;
    public IntPtr wParam;
    public uint message;
    public IntPtr hwnd;
};
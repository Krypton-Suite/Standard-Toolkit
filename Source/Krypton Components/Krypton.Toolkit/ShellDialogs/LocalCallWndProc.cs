#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

using MsdnMag;
// ReSharper disable NotAccessedField.Global

// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming

namespace Krypton.Toolkit;

internal struct CWPRETSTRUCT
{
    public IntPtr retValue;
    public IntPtr lParam;
    public IntPtr wParam;
    public int message;
    public IntPtr hWnd;
}

internal class LocalCallWndProc : LocalWindowsHook
{
    public LocalCallWndProc()
        : base(HookType.WH_CALLWNDPROCRET) =>
        m_filterFunc = CwpHookProc;

    public delegate void CwpEventHandler(object sender, CWPRETSTRUCT e, out bool actioned);

    public event CwpEventHandler? WindowMessage;

    internal IntPtr TargetWnd { get; set; }

    private int CwpHookProc(int code, IntPtr wParam, IntPtr lParam)
    {
        if (code < 0)
        {
            return CallNextHookEx(m_hHook, code, wParam, lParam);
        }

        var actioned = false;
        var msg = (CWPRETSTRUCT)Marshal.PtrToStructure(lParam, typeof(CWPRETSTRUCT))!;
        if (msg.hWnd == TargetWnd)
        {
            WindowMessage?.Invoke(this, msg, out actioned);
        }

        return actioned 
            ? 0 
            : CallNextHookEx(m_hHook, code, wParam, lParam);
    }
}
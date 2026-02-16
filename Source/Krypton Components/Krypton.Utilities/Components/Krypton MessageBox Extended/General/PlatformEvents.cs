#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

internal class PlatformEvents
{
    [DllImport(Libraries.User32)]
    internal static extern bool GetWindowRect(IntPtr hWnd, ref Rectangle lpRect);

    [DllImport(Libraries.User32)]
    internal static extern int MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    [DllImport(Libraries.User32)]
    public static extern UIntPtr SetTimer(IntPtr hWnd, UIntPtr nIDEvent, uint uElapse, PI.TimerProc lpTimerFunc);

    [DllImport(Libraries.User32)]
    public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

    [DllImport(Libraries.User32)]
    public static extern IntPtr SetWindowsHookEx(int idHook, PI.HookProc lpfn, IntPtr hInstance, int threadId);

    [DllImport(Libraries.User32)]
    public static extern int UnhookWindowsHookEx(IntPtr idHook);

    [DllImport(Libraries.User32)]
    public static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport(Libraries.User32)]
    public static extern int GetWindowTextLength(IntPtr hWnd);

    [DllImport(Libraries.User32)]
    public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int maxLength);

    [DllImport(Libraries.User32)]
    public static extern int EndDialog(IntPtr hDlg, IntPtr nResult);

    [DllImport(Libraries.User32, SetLastError = true)]
    internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
}
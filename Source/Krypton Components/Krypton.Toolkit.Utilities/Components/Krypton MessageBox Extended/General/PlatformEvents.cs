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
/// Message-box helpers over <see cref="PI"/>. Keeps call-site shapes used by extended message boxes.
/// </summary>
internal static class PlatformEvents
{
    internal static bool GetWindowRect(IntPtr hWnd, ref Rectangle lpRect)
    {
        PI.RECT native = new PI.RECT();
        if (!PI.GetWindowRect(hWnd, ref native))
        {
            return false;
        }

        // Historical PlatformEvents layout treated Rectangle as LTRB coordinates (Width/Height == right/bottom).
        lpRect = new Rectangle(native.left, native.top, native.right, native.bottom);
        return true;
    }

    internal static int MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint) =>
        PI.MoveWindow(hWnd, X, Y, nWidth, nHeight, bRepaint) ? 1 : 0;

    public static UIntPtr SetTimer(IntPtr hWnd, UIntPtr nIDEvent, uint uElapse, PI.TimerProc lpTimerFunc) =>
        PI.SetTimer(hWnd, nIDEvent, uElapse, lpTimerFunc);

    public static IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam) =>
        new IntPtr(unchecked((int)PI.SendMessage(hWnd, Msg, wParam, lParam)));

    public static IntPtr SetWindowsHookEx(int idHook, PI.HookProc lpfn, IntPtr hInstance, int threadId) =>
        PI.SetWindowsHookEx((PI.WH_)idHook, lpfn, hInstance, threadId);

    public static int UnhookWindowsHookEx(IntPtr idHook) => PI.UnhookWindowsHookEx(idHook);

    public static IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam) =>
        PI.CallNextHookEx(idHook, nCode, wParam, lParam);

    public static int GetWindowTextLength(IntPtr hWnd) => PI.GetWindowTextLength(hWnd);

    public static int GetWindowText(IntPtr hWnd, StringBuilder text, int maxLength) =>
        PI.GetWindowText(hWnd, text, maxLength);

    public static int EndDialog(IntPtr hDlg, IntPtr nResult) => (int)PI.EndDialog(hDlg, nResult);

    public static IntPtr FindWindow(string lpClassName, string lpWindowName) =>
        PI.FindWindow(lpClassName, lpWindowName);
}

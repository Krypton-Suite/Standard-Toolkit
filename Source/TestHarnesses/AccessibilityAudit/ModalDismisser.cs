#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace AccessibilityAudit;

/// <summary>
/// Closes modal dialogs owned by this process so a headless audit cannot block.
/// </summary>
internal sealed class ModalDismisser : IDisposable
{
    private const int WM_CLOSE = 0x0010;

    private readonly Thread _thread;
    private volatile bool _stop;

    public ModalDismisser()
    {
        _thread = new Thread(Watch)
        {
            IsBackground = true,
            Name = "AccessibilityAuditModalDismisser"
        };
        _thread.Start();
    }

    public void Dispose()
    {
        _stop = true;
    }

    private void Watch()
    {
        while (!_stop)
        {
            try
            {
                EnumWindows(CloseDialog, IntPtr.Zero);
            }
            catch (Exception)
            {
                // Keep watching. A failed enumeration should not stop the audit.
            }

            Thread.Sleep(50);
        }
    }

    private bool CloseDialog(IntPtr hWnd, IntPtr lParam)
    {
        GetWindowThreadProcessId(hWnd, out uint processId);
        if (processId != (uint)Environment.ProcessId)
        {
            return true;
        }

        var className = new StringBuilder(32);
        GetClassName(hWnd, className, className.Capacity);
        if (string.Equals(className.ToString(), "#32770", StringComparison.Ordinal))
        {
            var title = new StringBuilder(256);
            GetWindowText(hWnd, title, title.Capacity);
            AuditLog.Write("[INFO] Closing dialog: " + title);
            PostMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        return true;
    }

    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll")]
    private static extern bool PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
}

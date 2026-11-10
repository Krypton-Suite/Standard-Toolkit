#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Icons for the applications that currently have a taskbar button.
/// </summary>
internal static class KryptonSnapLayoutTaskbar
{
    private const int IconSize = 48;
    private const int OtherApplicationLimit = 8;
    private const uint SendTimeoutMs = 50;
    private const uint SmtoAbortIfHung = 0x0002;
    private const int WmGetIcon = 0x007F;
    private const int IconBig = 1;
    private const int IconSmall = 0;
    private const int IconSmall2 = 2;
    private const int GclHicon = -14;
    private const int GclHiconSm = -34;
    private const int DiNormal = 0x0003;
    private const uint WsExToolWindow = 0x00000080;
    private const uint WsExAppWindow = 0x00040000;

    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    /// <summary>
    /// Collects taskbar icons. Index 0 is <paramref name="ownerImage"/> for <paramref name="owner"/>.
    /// Later entries are the other taskbar applications, front to back. The caller owns every bitmap except <paramref name="ownerImage"/>.
    /// </summary>
    /// <param name="owner">Form that owns the snap menu. Omitted from the other-application list.</param>
    /// <param name="ownerImage">Icon already drawn for <paramref name="owner"/>. Not copied.</param>
    internal static Bitmap?[] Collect(IntPtr owner, Bitmap? ownerImage)
    {
        var images = new List<Bitmap?> { ownerImage };
        var windows = new List<IntPtr>();
        EnumWindowsProc callback = (hwnd, lParam) =>
        {
            if (IsTaskbarWindow(hwnd, owner))
            {
                windows.Add(hwnd);
            }

            return true;
        };

        EnumWindows(callback, IntPtr.Zero);
        GC.KeepAlive(callback);

        for (int i = 0; i < windows.Count && images.Count <= OtherApplicationLimit; i++)
        {
            Bitmap? image = IconFromWindow(windows[i]);
            if (image != null)
            {
                images.Add(image);
            }
        }

        return images.ToArray();
    }

    /// <summary>
    /// A top-level visible window is on the taskbar when it is not a tool window,
    /// unless it sets <c>WS_EX_APPWINDOW</c>, and it has no owner unless it sets that style.
    /// </summary>
    private static bool IsTaskbarWindow(IntPtr hwnd, IntPtr owner)
    {
        if (hwnd == IntPtr.Zero || hwnd == owner || !PI.IsWindowVisible(hwnd))
        {
            return false;
        }

        int cloaked = 0;
        if (PI.Dwm.DwmGetWindowAttribute(hwnd, PI.Dwm.DWMWINDOWATTRIBUTE.Cloaked, ref cloaked, sizeof(int)) == 0 && cloaked != 0)
        {
            return false;
        }

        uint exStyle = PI.GetWindowLong(hwnd, PI.GWL_.EXSTYLE);
        bool toolWindow = (exStyle & WsExToolWindow) != 0;
        bool appWindow = (exStyle & WsExAppWindow) != 0;
        if (toolWindow && !appWindow)
        {
            return false;
        }

        IntPtr ownedBy = PI.GetWindow(hwnd, PI.GetWindowType.GW_OWNER);
        if (ownedBy != IntPtr.Zero && !appWindow)
        {
            return false;
        }

        if (PI.GetWindowTextLength(hwnd) == 0)
        {
            return false;
        }

        string className = PI.GetClassNameString(hwnd);
        return className != "Progman"
               && className != "WorkerW"
               && className != "Shell_TrayWnd"
               && className != "Shell_SecondaryTrayWnd"
               && className != "NotifyIconOverflowWindow"
               && className != "TopLevelWindowForOverflowXamlIsland"
               && className != "DummyDWMListenerWindow";
    }

    private static Bitmap? IconFromWindow(IntPtr hwnd)
    {
        IntPtr icon = QueryIcon(hwnd, IconBig);
        if (icon == IntPtr.Zero)
        {
            icon = QueryIcon(hwnd, IconSmall2);
        }

        if (icon == IntPtr.Zero)
        {
            icon = QueryIcon(hwnd, IconSmall);
        }

        if (icon == IntPtr.Zero)
        {
            icon = PI.GetClassLongPtr(hwnd, GclHicon);
        }

        if (icon == IntPtr.Zero)
        {
            icon = PI.GetClassLongPtr(hwnd, GclHiconSm);
        }

        if (icon == IntPtr.Zero)
        {
            return null;
        }

        IntPtr copy = PI.CopyIcon(icon);
        if (copy == IntPtr.Zero)
        {
            return null;
        }

        try
        {
            var bitmap = new Bitmap(IconSize, IconSize, PixelFormat.Format32bppArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                IntPtr dc = graphics.GetHdc();
                try
                {
                    if (!DrawIconEx(dc, 0, 0, copy, IconSize, IconSize, 0, IntPtr.Zero, DiNormal))
                    {
                        bitmap.Dispose();
                        return null;
                    }
                }
                finally
                {
                    graphics.ReleaseHdc(dc);
                }
            }

            return bitmap;
        }
        finally
        {
            PI.DestroyIcon(copy);
        }
    }

    private static IntPtr QueryIcon(IntPtr hwnd, int iconType)
    {
        IntPtr result;
        SendMessageTimeout(hwnd, WmGetIcon, new IntPtr(iconType), IntPtr.Zero, SmtoAbortIfHung, SendTimeoutMs, out result);
        return result;
    }

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr SendMessageTimeout(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, uint flags, uint timeout, out IntPtr result);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyHeight, int istepIfAniCur, IntPtr hbrFlickerFreeDraw, int diFlags);
}

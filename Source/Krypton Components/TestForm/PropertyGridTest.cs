#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace TestForm;

public partial class PropertyGridTest : KryptonForm
{
    public PropertyGridTest()
    {
        InitializeComponent();
    }

    private void kbtnStressTest_Click(object sender, EventArgs e)
    {
        const int Iterations = 200;
        int paintFailures = 0;
        try
        {
            int gdiStart = GetGuiResources(Process.GetCurrentProcess().Handle, 0); // 0 == GR_GDIOBJECTS

            // Ensure bitmap large enough
            using var bmp = new Bitmap(kpgExample.Width, kpgExample.Height, PixelFormat.Format32bppArgb);
            for (int i = 0; i < Iterations; i++)
            {
                kpgExample.Invalidate();
                kpgExample.Update();

                kpgExample.DrawToBitmap(bmp, new Rectangle(Point.Empty, bmp.Size));

                if (IsBitmapSolid(bmp))
                {
                    // Record the failure but continue the stress run
                    paintFailures++;
                }
            }

            // Flush GDI to ensure any cached HDC/HBRUSH objects are released
            GdiFlush();

            // Measure GDI handles after test (post-GC)
            int gdiEnd = GetGuiResources(Process.GetCurrentProcess().Handle, 0);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            gdiEnd = GetGuiResources(Process.GetCurrentProcess().Handle, 0);

            // Release default bitmap associated with a temp memory DC to balance cached objects
            IntPtr tempDc = CreateCompatibleDC(IntPtr.Zero);
            IntPtr defBmp = GetCurrentObject(tempDc, OBJ_BITMAP);
            if (defBmp != IntPtr.Zero)
            {
                DeleteObject(defBmp);
            }
            DeleteDC(tempDc);

            // Show summary
            int successes = Iterations - paintFailures;
            string title = paintFailures == 0
                ? "Stress Test Passed"
                : "Stress Test Completed";
            string message = $"Correct drawing routine tested {Iterations} times.\n\n" +
                             $"Successful paints : {successes}\n" +
                             $"Solid-image failures : {paintFailures}";

            KryptonMessageBox.Show(message, title);
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show(ex.Message, "KryptonPropertyGrid Test Failed");
        }
    }

    private void kbtnStressTestBeforeFix_Click(object sender, EventArgs e)
    {
        const int Iterations = 200;
        int reproductionFailures = 0;
        int gdiStart = 0;

        // Re-use a single solid red brush to avoid leaks
        Color testColor = Color.Red;
        IntPtr redBrush = CreateSolidBrush(ColorTranslator.ToWin32(testColor));

        try
        {
            gdiStart = GetGuiResources(Process.GetCurrentProcess().Handle, 0);

            for (int i = 0; i < Iterations; i++)
            {
                IntPtr screenDc = GetDC(IntPtr.Zero);
                IntPtr memDc = CreateCompatibleDC(screenDc);
                ReleaseDC(IntPtr.Zero, screenDc);

                try
                {
                    IntPtr hBitmap = CreateCompatibleBitmap(memDc, 10, 10);
                    if (hBitmap == IntPtr.Zero)
                    {
                        throw new InvalidOperationException($"Failed to create bitmap at iteration {i}.");
                    }

                    SelectObject(memDc, hBitmap);
                    DeleteObject(hBitmap); // Intentional BUG action

                    var rect = new RECT { left = 0, top = 0, right = 10, bottom = 10 };
                    FillRect(memDc, ref rect, redBrush);

                    using (var verifyBmp = new Bitmap(10, 10))
                    {
                        using (Graphics g = Graphics.FromImage(verifyBmp))
                        {
                            IntPtr hdc = g.GetHdc();
                            BitBlt(hdc, 0, 0, 10, 10, memDc, 0, 0, 0x00CC0020);
                            g.ReleaseHdc(hdc);
                        }

                        if (verifyBmp.GetPixel(5, 5).ToArgb() == testColor.ToArgb())
                        {
                            reproductionFailures++; // corruption did NOT occur this iteration
                        }
                    }
                }
                finally
                {
                    if (memDc != IntPtr.Zero)
                    {
                        IntPtr defBmp2 = GetCurrentObject(memDc, OBJ_BITMAP);
                        if (defBmp2 != IntPtr.Zero)
                        {
                            DeleteObject(defBmp2);
                        }
                        DeleteDC(memDc);
                    }
                }
            }
        }
        finally
        {
            if (redBrush != IntPtr.Zero)
            {
                DeleteObject(redBrush);
            }
        }

        // After cleanup, force GC and measure handles again
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        int gdiEnd = GetGuiResources(Process.GetCurrentProcess().Handle, 0);

        int successes = Iterations - reproductionFailures;
        string title = reproductionFailures == 0 ? "Stress Test Passed" : "Stress Test Completed";
        string message = $"The flawed GDI routine was tested {Iterations} times.\n\n" +
                         $"Bug Reproduced (Success): {successes}\n" +
                         $"Bug Not Reproduced (Fail): {reproductionFailures}";

        KryptonMessageBox.Show(message, title);
    }

    [DllImport("user32.dll")]
    private static extern int GetGuiResources(IntPtr hProcess, int uiFlags);

    [DllImport("gdi32.dll", SetLastError = true)]
    private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

    [DllImport("gdi32.dll", SetLastError = true)]
    private static extern bool DeleteDC(IntPtr hdc);

    [DllImport("user32.dll")]
    private static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
    private static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

    [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
    public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

    [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DeleteObject(IntPtr hObject);

    // Flush GDI batching to force release of cached handles
    [DllImport("gdi32.dll")]
    private static extern bool GdiFlush();

    [DllImport("gdi32.dll", EntryPoint = "BitBlt", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateSolidBrush(int crColor);

    [DllImport("user32.dll")]
    private static extern int FillRect(IntPtr hDC, [In] ref RECT lprc, IntPtr hbr);

    [DllImport("gdi32.dll")]
    private static extern IntPtr GetCurrentObject(IntPtr hdc, int objectType); // objectType 7 = OBJ_BITMAP

    private const int OBJ_BITMAP = 7;

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    private static bool IsBitmapSolid(Bitmap bmp)
    {
        if (bmp.Width < 2 || bmp.Height < 2)
        {
            return true;
        }
        Color first = bmp.GetPixel(0, 0);
        for (int y = 0; y < bmp.Height; y++)
        {
            for (int x = 0; x < bmp.Width; x++)
            {
                if (bmp.GetPixel(x, y) != first)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
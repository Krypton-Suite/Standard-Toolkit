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
            int gdiStart = PI.GetGuiResources(Process.GetCurrentProcess().Handle, 0); // 0 == GR_GDIOBJECTS

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
            PI.GdiFlush();

            // Measure GDI handles after test (post-GC)
            int gdiEnd = PI.GetGuiResources(Process.GetCurrentProcess().Handle, 0);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            gdiEnd = PI.GetGuiResources(Process.GetCurrentProcess().Handle, 0);

            // Release default bitmap associated with a temp memory DC to balance cached objects
            IntPtr tempDc = PI.CreateCompatibleDC(IntPtr.Zero);
            IntPtr defBmp = PI.GetCurrentObject(tempDc, PI.OBJ_BITMAP);
            if (defBmp != IntPtr.Zero)
            {
                PI.DeleteObject(defBmp);
            }
            PI.DeleteDC(tempDc);

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
        IntPtr redBrush = PI.CreateSolidBrush(ColorTranslator.ToWin32(testColor));

        try
        {
            gdiStart = PI.GetGuiResources(Process.GetCurrentProcess().Handle, 0);

            for (int i = 0; i < Iterations; i++)
            {
                IntPtr screenDc = PI.GetDC(IntPtr.Zero);
                IntPtr memDc = PI.CreateCompatibleDC(screenDc);
                PI.ReleaseDC(IntPtr.Zero, screenDc);

                try
                {
                    IntPtr hBitmap = PI.CreateCompatibleBitmap(memDc, 10, 10);
                    if (hBitmap == IntPtr.Zero)
                    {
                        throw new InvalidOperationException($"Failed to create bitmap at iteration {i}.");
                    }

                    PI.SelectObject(memDc, hBitmap);
                    PI.DeleteObject(hBitmap); // Intentional BUG action

                    var rect = new PI.RECT { left = 0, top = 0, right = 10, bottom = 10 };
                    PI.FillRect(memDc, ref rect, redBrush);

                    using (var verifyBmp = new Bitmap(10, 10))
                    {
                        using (Graphics g = Graphics.FromImage(verifyBmp))
                        {
                            IntPtr hdc = g.GetHdc();
                            PI.BitBlt(hdc, 0, 0, 10, 10, memDc, 0, 0, 0x00CC0020);
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
                        IntPtr defBmp2 = PI.GetCurrentObject(memDc, PI.OBJ_BITMAP);
                        if (defBmp2 != IntPtr.Zero)
                        {
                            PI.DeleteObject(defBmp2);
                        }
                        PI.DeleteDC(memDc);
                    }
                }
            }
        }
        finally
        {
            if (redBrush != IntPtr.Zero)
            {
                PI.DeleteObject(redBrush);
            }
        }

        // After cleanup, force GC and measure handles again
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        int gdiEnd = PI.GetGuiResources(Process.GetCurrentProcess().Handle, 0);

        int successes = Iterations - reproductionFailures;
        string title = reproductionFailures == 0 ? "Stress Test Passed" : "Stress Test Completed";
        string message = $"The flawed GDI routine was tested {Iterations} times.\n\n" +
                         $"Bug Reproduced (Success): {successes}\n" +
                         $"Bug Not Reproduced (Fail): {reproductionFailures}";

        KryptonMessageBox.Show(message, title);
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
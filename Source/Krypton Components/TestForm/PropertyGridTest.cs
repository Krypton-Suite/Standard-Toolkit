#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
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
        const int Iterations = 500;
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
                    throw new InvalidOperationException($"Solid image detected at iteration {i}. Possible paint failure.");
                }
            }

            int gdiEnd = GetGuiResources(Process.GetCurrentProcess().Handle, 0);

            // Give the GC a chance to collect any unreleased bitmaps created during DrawToBitmap
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            gdiEnd = GetGuiResources(Process.GetCurrentProcess().Handle, 0);

            const int AllowedLeak = 3; // small fluctuations are normal (theme handles, etc.)
            if (gdiEnd - gdiStart > 1)
            {
                if (gdiEnd - gdiStart > AllowedLeak)
                {
                    throw new InvalidOperationException($"Potential GDI leak detected. Handles before: {gdiStart}, after: {gdiEnd}.");
                }
            }

            KryptonMessageBox.Show("Stress test completed successfully.", "KryptonPropertyGrid Test");
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show(ex.Message, "KryptonPropertyGrid Test Failed");
        }
    }

    [DllImport("user32.dll")]
    private static extern int GetGuiResources(IntPtr hProcess, int uiFlags);

    private static bool IsBitmapSolid(Bitmap bmp)
    {
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
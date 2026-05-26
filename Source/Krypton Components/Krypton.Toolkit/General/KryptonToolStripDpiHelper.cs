#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Applies per-monitor DPI metrics to Krypton tool strip controls.
/// </summary>
internal static class KryptonToolStripDpiHelper
{
    internal static void SyncFonts(Control root)
    {
        if (root == null)
        {
            return;
        }

        if (root is KryptonMenuStrip menuStrip)
        {
            ApplyDpiMetrics(menuStrip, isStatusStrip: false);
        }
        else if (root is KryptonToolStrip toolStrip)
        {
            ApplyDpiMetrics(toolStrip, isStatusStrip: false);
        }
        else if (root is KryptonStatusStrip statusStrip)
        {
            ApplyDpiMetrics(statusStrip, isStatusStrip: true);
        }

        foreach (Control child in root.Controls)
        {
            SyncFonts(child);
        }
    }

    private static void ApplyDpiMetrics(ToolStrip strip, bool isStatusStrip)
    {
        if (strip.IsDisposed)
        {
            return;
        }

        float dpiFactor = CommonHelper.GetControlDpiFactor(strip);
        if (dpiFactor <= 0f)
        {
            dpiFactor = 1f;
        }

        Font systemFont = isStatusStrip ? SystemFonts.StatusFont! : SystemFonts.MenuFont!;
        float targetPoints = systemFont.SizeInPoints;

        PaletteBase? palette = KryptonManager.CurrentGlobalPalette;
        if (palette?.ColorTable != null)
        {
            Font paletteFont = isStatusStrip
                ? palette.ColorTable.StatusStripFont
                : palette.ColorTable.ToolStripFont;
            if (paletteFont != null)
            {
                targetPoints = paletteFont.SizeInPoints * dpiFactor;
            }
        }

        Font scaledFont = new Font(systemFont.FontFamily, targetPoints, systemFont.Style, systemFont.Unit);
        if (strip.Font == null || Math.Abs(strip.Font.SizeInPoints - scaledFont.SizeInPoints) > 0.01f)
        {
            strip.Font = scaledFont;
        }

        int imageSize = Math.Max(16, (int)Math.Round(16 * dpiFactor));
        strip.ImageScalingSize = new Size(imageSize, imageSize);
        strip.PerformLayout();
        strip.Invalidate(true);
    }
}

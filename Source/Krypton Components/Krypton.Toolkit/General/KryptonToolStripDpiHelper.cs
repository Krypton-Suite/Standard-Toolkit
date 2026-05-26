#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Applies per-monitor DPI and touchscreen metrics to menu, tool, and status strips.
/// </summary>
public static class KryptonToolStripDpiHelper
{
    /// <summary>
    /// Syncs strip fonts, item fonts, and image sizes for the control tree rooted at <paramref name="root"/>.
    /// </summary>
    public static void SyncFonts(Control? root)
    {
        if (root == null)
        {
            return;
        }

        if (root is ToolStrip strip)
        {
            ApplyDpiMetrics(strip, strip is StatusStrip, null);
        }

        foreach (Control child in root.Controls)
        {
            SyncFonts(child);
        }
    }

    /// <summary>
    /// Syncs fonts and image metrics for a single strip.
    /// </summary>
    /// <param name="strip">The strip to update.</param>
    /// <param name="palette">Optional palette; uses <see cref="KryptonManager.CurrentGlobalPalette"/> when null.</param>
    public static void SyncStrip(ToolStrip strip, PaletteBase? palette = null)
    {
        if (strip != null)
        {
            ApplyDpiMetrics(strip, strip is StatusStrip, palette);
        }
    }

    private static void ApplyDpiMetrics(ToolStrip strip, bool isStatusStrip, PaletteBase? palette)
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

        float controlScale = KryptonManager.UseTouchscreenSupport ? KryptonManager.TouchscreenScaleFactor : 1f;
        float fontScale = KryptonManager.TouchscreenFontScaleFactor;

        palette ??= KryptonManager.CurrentGlobalPalette;

        Font systemFont = isStatusStrip ? SystemFonts.StatusFont! : SystemFonts.MenuFont!;
        FontFamily fontFamily = systemFont.FontFamily;
        FontStyle fontStyle = systemFont.Style;
        float targetPoints = systemFont.SizeInPoints * dpiFactor * fontScale;

        if (palette?.ColorTable != null)
        {
            Font? paletteFont = isStatusStrip
                ? palette.ColorTable.StatusStripFont
                : palette.ColorTable.ToolStripFont;
            if (paletteFont != null)
            {
                fontFamily = paletteFont.FontFamily;
                fontStyle = paletteFont.Style;
                targetPoints = paletteFont.SizeInPoints * dpiFactor * fontScale;
            }
        }

        Font scaledFont = new Font(fontFamily, targetPoints, fontStyle, systemFont.Unit);
        if (strip.Font == null || Math.Abs(strip.Font.SizeInPoints - scaledFont.SizeInPoints) > 0.01f
            || strip.Font.FontFamily != scaledFont.FontFamily)
        {
            strip.Font = scaledFont;
        }

        ApplyFontToItems(strip.Items, scaledFont);

        int imageSize = Math.Max(16, (int)Math.Round(16 * dpiFactor * controlScale));
        if (strip.ImageScalingSize.Width != imageSize || strip.ImageScalingSize.Height != imageSize)
        {
            strip.ImageScalingSize = new Size(imageSize, imageSize);
        }

        strip.PerformLayout();
        strip.Invalidate(true);
    }

    private static void ApplyFontToItems(ToolStripItemCollection items, Font font)
    {
        foreach (ToolStripItem item in items)
        {
            if (item is ToolStripControlHost)
            {
                continue;
            }

            if (item.Font == null
                || Math.Abs(item.Font.SizeInPoints - font.SizeInPoints) > 0.01f
                || item.Font.FontFamily != font.FontFamily)
            {
                item.Font = font;
            }

            if (item is ToolStripDropDownItem dropDownItem)
            {
                ApplyFontToItems(dropDownItem.DropDownItems, font);

                if (dropDownItem.DropDown is ToolStripDropDown dropDown)
                {
                    if (dropDown.Font == null
                        || Math.Abs(dropDown.Font.SizeInPoints - font.SizeInPoints) > 0.01f
                        || dropDown.Font.FontFamily != font.FontFamily)
                    {
                        dropDown.Font = font;
                    }
                }
            }
        }
    }
}

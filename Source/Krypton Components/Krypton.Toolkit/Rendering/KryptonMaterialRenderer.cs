#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Minimal Material renderer for ToolStrip/ContextMenu: flat dark/light backgrounds, subtle item highlight, thin borders.
/// Does not alter ColorTable classes; reads fonts and text colors from provided ColorTable.
/// </summary>
public sealed class KryptonMaterialRenderer : KryptonProfessionalRenderer
{
    public KryptonMaterialRenderer([DisallowNull] KryptonColorTable kct)
        : base(kct)
    {
    }

    protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
    {
        if (e.ToolStrip is ContextMenuStrip or ToolStripDropDownMenu)
        {
            if (e.ToolStrip.Font != KCT.MenuStripFont)
            {
                e.ToolStrip.Font = KCT.MenuStripFont;
            }

            using var borderPath = CreateBorderPath(e.AffectedBounds, 0f);
            using var clipPath = CreateClipBorderPath(e.AffectedBounds, 0f);
            using var clipping = new Clipping(e.Graphics, clipPath);
            // Popup background per current palette (Material overrides ContextMenuOuter/Inner)
            var popupBack = KCT.Palette.GetBackColor1(PaletteBackStyle.ContextMenuOuter, PaletteState.Normal);
            using var backBrush = new SolidBrush(popupBack);
            e.Graphics.FillPath(backBrush, borderPath);
            return;
        }
        else if (e.ToolStrip is StatusStrip ss)
        {
            // Material: flat fill; honor per-control overrides on KryptonStatusStrip so animations work
            Color back;
            if (ss is KryptonStatusStrip kss)
            {
                var style = kss.StateCommon.GetBackColorStyle(PaletteState.Normal);
                var c1 = kss.StateCommon.GetBackColor1(PaletteState.Normal);
                if (style == PaletteColorStyle.Solid && c1 != GlobalStaticValues.EMPTY_COLOR && !c1.IsEmpty)
                {
                    back = c1;
                }
                else
                {
                    back = (KCT.StatusStripGradientEnd != GlobalStaticValues.EMPTY_COLOR && !KCT.StatusStripGradientEnd.IsEmpty)
                        ? KCT.StatusStripGradientEnd
                        : ((KCT.StatusStripGradientBegin != GlobalStaticValues.EMPTY_COLOR && !KCT.StatusStripGradientBegin.IsEmpty)
                            ? KCT.StatusStripGradientBegin
                            : ss.BackColor);
                }
            }
            else
            {
                back = (KCT.StatusStripGradientEnd != GlobalStaticValues.EMPTY_COLOR && !KCT.StatusStripGradientEnd.IsEmpty)
                    ? KCT.StatusStripGradientEnd
                    : ((KCT.StatusStripGradientBegin != GlobalStaticValues.EMPTY_COLOR && !KCT.StatusStripGradientBegin.IsEmpty)
                        ? KCT.StatusStripGradientBegin
                        : ss.BackColor);
            }

            using var b = new SolidBrush(back);
            e.Graphics.FillRectangle(b, e.AffectedBounds);
            return;
        }

        base.OnRenderToolStripBackground(e);
    }

    protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
    {
        if (e.ToolStrip is ContextMenuStrip or ToolStripDropDownMenu)
        {
            var borderColor = KCT.Palette.GetBorderColor1(PaletteBorderStyle.ContextMenuOuter, PaletteState.Normal);
            using var borderPen = new Pen(borderColor);
            var rect = e.AffectedBounds;
            rect.Width -= 1;
            rect.Height -= 1;

            // If connected to a parent (drop-down attached), avoid drawing over the connection
            if (!e.ConnectedArea.IsEmpty && e.ConnectedArea.Top <= rect.Top + 1 && e.ConnectedArea.Bottom >= rect.Top)
            {
                // Left, right and bottom borders as normal
                e.Graphics.DrawLine(borderPen, rect.Left, rect.Top, rect.Left, rect.Bottom);
                e.Graphics.DrawLine(borderPen, rect.Right, rect.Top, rect.Right, rect.Bottom);
                e.Graphics.DrawLine(borderPen, rect.Left, rect.Bottom, rect.Right, rect.Bottom);

                // Top border split around the connected area
                int gapLeft = Math.Max(rect.Left, e.ConnectedArea.Left);
                int gapRight = Math.Min(rect.Right, e.ConnectedArea.Right);
                if (gapLeft > rect.Left)
                {
                    e.Graphics.DrawLine(borderPen, rect.Left, rect.Top, gapLeft, rect.Top);
                }
                if (gapRight < rect.Right)
                {
                    e.Graphics.DrawLine(borderPen, gapRight, rect.Top, rect.Right, rect.Top);
                }
            }
            else
            {
                e.Graphics.DrawRectangle(borderPen, rect);
            }
            return;
        }

        base.OnRenderToolStripBorder(e);
    }

    protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
    {
        if (e.ToolStrip is ContextMenuStrip or ToolStripDropDownMenu)
        {
            if (e.Item.Selected)
            {
                var rect = new Rectangle(2, 0, e.Item.Bounds.Width - 4, e.Item.Bounds.Height);
                var hi = KCT.Palette.GetBackColor1(PaletteBackStyle.ContextMenuItemHighlight, PaletteState.Tracking);
                using var brush = new SolidBrush(hi);
                e.Graphics.FillRectangle(brush, rect);
            }
            return;
        }
        else if (e.ToolStrip is MenuStrip)
        {
            // Flat highlight for top-level menu items
            var state = e.Item.Selected || e.Item.Pressed ? PaletteState.Tracking : PaletteState.Normal;
            if (state == PaletteState.Tracking)
            {
                var rect = new Rectangle(2, 0, e.Item.Bounds.Width - 4, e.Item.Bounds.Height);
                var hi = KCT.Palette.GetBackColor1(PaletteBackStyle.ContextMenuItemHighlight, PaletteState.Tracking);
                using var brush = new SolidBrush(hi);
                e.Graphics.FillRectangle(brush, rect);
                return;
            }
        }

        base.OnRenderMenuItemBackground(e);
    }

    protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
    {
        if (e.ToolStrip is ContextMenuStrip or ToolStripDropDownMenu)
        {
            // Flat margin matching palette image column color
            var rect = e.AffectedBounds;
            rect.Inflate(-2, -2);
            var marginBack = KCT.Palette.GetBackColor1(PaletteBackStyle.ContextMenuItemImageColumn, PaletteState.Normal);
            using var b = new SolidBrush(marginBack);
            e.Graphics.FillRectangle(b, rect);
            return;
        }

        base.OnRenderImageMargin(e);
    }

    protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
    {
        if (e.ToolStrip is ContextMenuStrip or ToolStripDropDownMenu)
        {
            var state = !e.Item.Enabled ? PaletteState.Disabled : (e.Item.Selected ? PaletteState.Tracking : PaletteState.Normal);
            var textColor = KCT.Palette.GetContentShortTextColor1(PaletteContentStyle.ContextMenuItemTextStandard, state);
            if (textColor == GlobalStaticValues.EMPTY_COLOR || textColor.IsEmpty)
            {
                // Deterministic fallback based on state
                textColor = KCT.MenuItemText;
            }
            e.TextColor = textColor;
        }
        else if (e.ToolStrip is MenuStrip)
        {
            var state = e.Item.Selected || e.Item.Pressed ? PaletteState.Tracking : PaletteState.Normal;
            // Prefer on-surface label color for top-level menu items under Material
            var c = KCT.Palette.GetContentShortTextColor1(PaletteContentStyle.LabelNormalPanel, state);
            if (c == GlobalStaticValues.EMPTY_COLOR || c.IsEmpty)
            {
                c = KCT.MenuStripText;
            }
            e.TextColor = c;
        }
        else if (e.ToolStrip is StatusStrip)
        {
            e.TextColor = KCT.StatusStripText;
        }
        else if (e.Item is ToolStripMenuItem)
        {
            // Menu items hosted on a ToolStrip (not MenuStrip/ContextMenu): use on-surface label color
            var state = !e.Item.Enabled ? PaletteState.Disabled : (e.Item.Selected || e.Item.Pressed ? PaletteState.Tracking : PaletteState.Normal);
            var c = KCT.Palette.GetContentShortTextColor1(PaletteContentStyle.LabelNormalPanel, state);
            if (c == GlobalStaticValues.EMPTY_COLOR || c.IsEmpty)
            {
                c = KCT.MenuItemText;
            }
            e.TextColor = c;
        }
        else
        {
            // Generic ToolStrip items: use on-surface label color from palette for Material dark
            var state = !e.Item.Enabled ? PaletteState.Disabled : (e.Item.Selected ? PaletteState.Tracking : PaletteState.Normal);
            var c = KCT.Palette.GetContentShortTextColor1(PaletteContentStyle.LabelNormalPanel, state);
            if (c == GlobalStaticValues.EMPTY_COLOR || c.IsEmpty)
            {
                c = KCT.ToolStripText;
            }
            e.TextColor = c;
        }

        using var clearType = new GraphicsTextHint(e.Graphics, TextRenderingHint.ClearTypeGridFit);
        base.OnRenderItemText(e);
    }

    // Local helpers for simple rectangular paths (square corners per Material)
    internal static GraphicsPath CreateBorderPath(Rectangle rect, float cut)
    {
        // Ensure positive rect and draw border on pixel grid
        Rectangle r = new Rectangle(rect.X, rect.Y, Math.Max(0, rect.Width - 1), Math.Max(0, rect.Height - 1));
        var path = new GraphicsPath();
        path.AddRectangle(r);
        return path;
    }

    internal static GraphicsPath CreateClipBorderPath(Rectangle rect, float cut)
    {
        // Slightly inset to keep fills inside drawn border
        Rectangle r = new Rectangle(rect.X + 1, rect.Y + 1, Math.Max(0, rect.Width - 2), Math.Max(0, rect.Height - 2));
        var path = new GraphicsPath();
        if (r.Width > 0 && r.Height > 0)
        {
            path.AddRectangle(r);
        }
        else
        {
            path.AddRectangle(Rectangle.Empty);
        }
        return path;
    }
}
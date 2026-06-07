#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// macOS-styled ToolStrip renderer: flat fills with softly rounded popup menus.
/// </summary>
public class KryptonMacOSRenderer : KryptonMaterialRenderer
{
    private const float PopupCornerRadius = 8f;

    public KryptonMacOSRenderer([DisallowNull] KryptonColorTable kct)
        : base(kct)
    {
    }

    protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
    {
        if (e.ToolStrip is ContextMenuStrip or ToolStripDropDownMenu)
        {
            if (!Equals(e.ToolStrip.Font, KCT.MenuStripFont))
            {
                e.ToolStrip.Font = KCT.MenuStripFont;
            }

            using var borderPath = CreateRoundedBorderPath(e.AffectedBounds, PopupCornerRadius);
            using var clipPath = CreateRoundedClipPath(e.AffectedBounds, PopupCornerRadius);
            using var clipping = new Clipping(e.Graphics, clipPath);
            var popupBack = KCT.Palette.GetBackColor1(PaletteBackStyle.ContextMenuOuter, PaletteState.Normal);
            using var backBrush = new SolidBrush(popupBack);
            e.Graphics.FillPath(backBrush, borderPath);
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
            using var borderPath = CreateRoundedBorderPath(e.AffectedBounds, PopupCornerRadius);
            e.Graphics.DrawPath(borderPen, borderPath);
            return;
        }

        base.OnRenderToolStripBorder(e);
    }

    protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
    {
        if (e.ToolStrip is ContextMenuStrip or ToolStripDropDownMenu && e.Item.Selected)
        {
            var rect = new Rectangle(4, 1, e.Item.Bounds.Width - 8, e.Item.Bounds.Height - 2);
            var hi = KCT.Palette.GetBackColor1(PaletteBackStyle.ContextMenuItemHighlight, PaletteState.Tracking);
            using var path = CreateRoundedBorderPath(rect, 4f);
            using var brush = new SolidBrush(hi);
            e.Graphics.FillPath(brush, path);
            return;
        }

        base.OnRenderMenuItemBackground(e);
    }

    private static GraphicsPath CreateRoundedBorderPath(Rectangle rect, float radius)
    {
        Rectangle r = new Rectangle(rect.X, rect.Y, Math.Max(0, rect.Width - 1), Math.Max(0, rect.Height - 1));
        var path = new GraphicsPath();
        if (r.Width <= 0 || r.Height <= 0)
        {
            return path;
        }

        float d = radius * 2f;
        if (d > r.Width)
        {
            d = r.Width;
        }

        if (d > r.Height)
        {
            d = r.Height;
        }

        path.AddArc(r.X, r.Y, d, d, 180, 90);
        path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
        path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
        path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
        path.CloseFigure();
        return path;
    }

    private static GraphicsPath CreateRoundedClipPath(Rectangle rect, float radius)
    {
        Rectangle r = new Rectangle(rect.X + 1, rect.Y + 1, Math.Max(0, rect.Width - 2), Math.Max(0, rect.Height - 2));
        return CreateRoundedBorderPath(r, Math.Max(0f, radius - 1f));
    }
}
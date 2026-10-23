#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Flat ToolStrip/ContextMenu rendering for the Retro palette.
/// </summary>
public sealed class KryptonRetroRenderer : KryptonProfessionalRenderer
{
    public KryptonRetroRenderer([DisallowNull] KryptonColorTable kct)
        : base(kct)
    {
    }

    protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
    {
        if (e.Item is ToolStripMenuItem && e.Item.Selected)
        {
            var g = e.Graphics;
            var oldSmoothing = g.SmoothingMode;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            var highlight = KCT.Palette.GetBackColor1(PaletteBackStyle.ButtonListItem, PaletteState.Tracking);
            using var brush = new SolidBrush(highlight);
            g.FillRectangle(brush, new Rectangle(Point.Empty, e.Item.Size));
            g.SmoothingMode = oldSmoothing;
            return;
        }

        base.OnRenderMenuItemBackground(e);
    }
}

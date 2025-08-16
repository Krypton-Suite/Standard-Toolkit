#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

internal sealed class FileMenuBorderTestRenderer : Krypton.Toolkit.KryptonProfessionalRenderer
{
    private readonly System.Collections.Generic.Dictionary<string, System.Drawing.Color> _nameToColor;

    public FileMenuBorderTestRenderer(Krypton.Toolkit.KryptonColorTable kct)
        : base(kct)
    {
        _nameToColor = new System.Collections.Generic.Dictionary<string, System.Drawing.Color>(System.StringComparer.Ordinal)
        {
            ["newToolStripMenuItem"] = System.Drawing.Color.Red,
            ["openToolStripMenuItem"] = System.Drawing.Color.ForestGreen,
            ["saveToolStripMenuItem"] = System.Drawing.Color.RoyalBlue,
            ["saveAsToolStripMenuItem"] = System.Drawing.Color.Orange,
            ["printToolStripMenuItem"] = System.Drawing.Color.MediumVioletRed,
            ["printPreviewToolStripMenuItem"] = System.Drawing.Color.SaddleBrown,
            ["exitToolStripMenuItem"] = System.Drawing.Color.Crimson
        };
    }

    protected override void OnRenderMenuItemBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
    {
        // First, honor per-item overrides (KryptonToolStripMenuItem State* colors)
        if (TryRenderMenuItemOverride(e))
        {
            return;
        }

        base.OnRenderMenuItemBackground(e);

        var ownerItem = e.Item.OwnerItem as System.Windows.Forms.ToolStripMenuItem;
        if (ownerItem != null && ownerItem.Name == "fileToolStripMenuItem")
        {
            System.Drawing.Color? overrideColor = null;
            if (e.Item.Tag is System.Drawing.Color c)
            {
                overrideColor = c;
            }
            else if (e.Item.Tag is string s && !string.IsNullOrWhiteSpace(s))
            {
                try { overrideColor = System.Drawing.ColorTranslator.FromHtml(s); } catch { /* ignore */ }
            }

            var hasMap = _nameToColor.TryGetValue(e.Item.Name ?? string.Empty, out var mappedColor);
            var borderColor = overrideColor ?? (hasMap ? mappedColor : (System.Drawing.Color?)null);

            if (borderColor.HasValue)
            {
                var rect = e.Item.ContentRectangle;
                rect.Inflate(-1, -1);
                using (var pen = new System.Drawing.Pen(borderColor.Value))
                {
                    e.Graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
                }
            }
        }
    }
}

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Dialogs;

/// <summary>
/// Shared layout for the Windows-style icon + main-instruction banner on file-operation dialogs.
/// Uses <see cref="KryptonWrapLabel"/> because <see cref="KryptonLabel"/> does not word-wrap.
/// </summary>
internal static class FileOperationBannerLayout
{
    /// <summary>Design-time / fallback banner height including padding and border.</summary>
    public const int DefaultHeight = 100;

    private const int PaddingTop = 24;
    private const int PaddingBottom = 24;
    private const int PaddingLeft = 16;
    private const int PaddingRight = 16;
    private const int IconTextGap = 12;
    private const int MinLabelHeight = 48;
    private const int IconSize = 32;

    /// <summary>
    /// Applies the bold main-instruction look used by file-operation banners.
    /// </summary>
    /// <param name="label">Wrap label that shows the main instruction.</param>
    public static void Configure(KryptonWrapLabel label)
    {
        if (label.StateCommon.Font == null)
        {
            label.StateCommon.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
        }

        label.AutoSize = true;
    }

    /// <summary>
    /// Wires the banner table (icon | instruction) inside the Dock-Top panel.
    /// </summary>
    /// <param name="table">Two-column table hosted in the banner panel.</param>
    /// <param name="icon">Banner icon.</param>
    /// <param name="label">Main-instruction wrap label.</param>
    public static void ConfigureTable(KryptonTableLayoutPanel table, PictureBox icon, KryptonWrapLabel label)
    {
        Configure(label);

        table.ColumnCount = 2;
        table.RowCount = 1;
        table.Dock = DockStyle.Fill;
        table.Padding = new Padding(PaddingLeft, PaddingTop, PaddingRight, PaddingBottom);
        table.Margin = Padding.Empty;
        table.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
        table.ColumnStyles.Clear();
        table.RowStyles.Clear();
        table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

        icon.Size = new Size(IconSize, IconSize);
        icon.SizeMode = PictureBoxSizeMode.CenterImage;
        icon.BackColor = Color.Transparent;
        icon.Margin = new Padding(0, 0, IconTextGap, 0);
        icon.Anchor = AnchorStyles.None;
        icon.TabStop = false;

        label.Dock = DockStyle.Fill;
        label.Margin = Padding.Empty;
        label.AutoSize = true;

        table.Controls.Add(icon, 0, 0);
        table.Controls.Add(label, 1, 0);
    }

    /// <summary>
    /// Sizes the banner panel so the instruction wraps with comfortable vertical padding.
    /// </summary>
    /// <param name="panel">Dock-Top banner panel.</param>
    /// <param name="table">Banner table hosting the icon and label.</param>
    /// <param name="label">Main-instruction wrap label.</param>
    /// <param name="icon">Banner icon.</param>
    /// <returns>The resulting panel height.</returns>
    public static int Fit(KryptonPanel panel, KryptonTableLayoutPanel table, KryptonWrapLabel label, PictureBox icon)
    {
        if (panel == null || !panel.Visible)
        {
            return panel == null ? 0 : panel.Height;
        }

        Configure(label);

        int labelWidth = Math.Max(40, panel.ClientSize.Width - table.Padding.Horizontal - IconSize - IconTextGap);
        label.MaximumSize = new Size(labelWidth, 0);
        Size preferred = label.GetPreferredSize(new Size(labelWidth, 0));
        int labelHeight = Math.Max(MinLabelHeight, preferred.Height);
        int contentHeight = Math.Max(IconSize, labelHeight);

        int borderHeight = 1;
        foreach (Control child in panel.Controls)
        {
            if (child is KryptonBorderEdge edge && edge.Dock == DockStyle.Bottom)
            {
                borderHeight = Math.Max(1, edge.Height);
                break;
            }
        }

        int height = table.Padding.Vertical + contentHeight + borderHeight;
        if (height < DefaultHeight)
        {
            height = DefaultHeight;
        }

        panel.Height = height;
        return height;
    }
}

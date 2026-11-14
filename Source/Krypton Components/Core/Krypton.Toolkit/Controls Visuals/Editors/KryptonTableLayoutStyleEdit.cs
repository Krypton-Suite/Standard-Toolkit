#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Inserts, deletes, and copies <see cref="TableLayoutPanel"/> column and row styles.
/// </summary>
internal static class KryptonTableLayoutStyleEdit
{
    /// <summary>Absolute size given to a style created by Add or Insert.</summary>
    internal const float NewStyleSize = 20f;

    internal readonly struct StyleValue
    {
        internal StyleValue(SizeType sizeType, float size)
        {
            SizeType = sizeType;
            Size = size;
        }

        internal SizeType SizeType { get; }

        internal float Size { get; }
    }

    internal readonly struct CellValue
    {
        internal CellValue(Control control, int column, int row, int columnSpan, int rowSpan)
        {
            Control = control;
            Column = column;
            Row = row;
            ColumnSpan = columnSpan;
            RowSpan = rowSpan;
        }

        internal Control Control { get; }

        internal int Column { get; }

        internal int Row { get; }

        internal int ColumnSpan { get; }

        internal int RowSpan { get; }
    }

    internal static List<StyleValue> Read(TableLayoutPanel panel, bool columns)
    {
        int count = columns ? panel.ColumnCount : panel.RowCount;
        var styles = new List<StyleValue>(count);
        for (var index = 0; index < count; index++)
        {
            if (columns && index < panel.ColumnStyles.Count)
            {
                ColumnStyle style = panel.ColumnStyles[index];
                styles.Add(new StyleValue(style.SizeType, style.Width));
            }
            else if (!columns && index < panel.RowStyles.Count)
            {
                RowStyle style = panel.RowStyles[index];
                styles.Add(new StyleValue(style.SizeType, style.Height));
            }
            else
            {
                styles.Add(new StyleValue(SizeType.AutoSize, 0f));
            }
        }

        return styles;
    }

    internal static CellValue[] ReadCells(TableLayoutPanel panel)
    {
        var cells = new CellValue[panel.Controls.Count];
        for (var index = 0; index < panel.Controls.Count; index++)
        {
            Control control = panel.Controls[index];
            cells[index] = new CellValue(
                control,
                panel.GetColumn(control),
                panel.GetRow(control),
                panel.GetColumnSpan(control),
                panel.GetRowSpan(control));
        }

        return cells;
    }

    internal static void Write(TableLayoutPanel panel, bool columns, IReadOnlyList<StyleValue> styles)
    {
        // ColumnCount can be ahead of the style collection until the panel lays out.
        if (columns)
        {
            EnsureColumnStyles(panel, styles.Count);
            panel.ColumnCount = styles.Count;
            EnsureColumnStyles(panel, styles.Count);
            for (var index = 0; index < styles.Count; index++)
            {
                panel.ColumnStyles[index].SizeType = styles[index].SizeType;
                panel.ColumnStyles[index].Width = styles[index].Size;
            }
        }
        else
        {
            EnsureRowStyles(panel, styles.Count);
            panel.RowCount = styles.Count;
            EnsureRowStyles(panel, styles.Count);
            for (var index = 0; index < styles.Count; index++)
            {
                panel.RowStyles[index].SizeType = styles[index].SizeType;
                panel.RowStyles[index].Height = styles[index].Size;
            }
        }
    }

    internal static void Restore(TableLayoutPanel panel, IReadOnlyList<StyleValue> columns, IReadOnlyList<StyleValue> rows, CellValue[] cells)
    {
        panel.SuspendLayout();
        try
        {
            Write(panel, columns: true, columns);
            Write(panel, columns: false, rows);
            foreach (CellValue cell in cells)
            {
                if (cell.Control.IsDisposed)
                {
                    continue;
                }

                panel.SetCellPosition(cell.Control, new TableLayoutPanelCellPosition(cell.Column, cell.Row));
                panel.SetColumnSpan(cell.Control, Math.Max(1, cell.ColumnSpan));
                panel.SetRowSpan(cell.Control, Math.Max(1, cell.RowSpan));
            }
        }
        finally
        {
            panel.ResumeLayout(true);
        }
    }

    internal static void Insert(TableLayoutPanel panel, bool columns, int index)
    {
        List<StyleValue> styles = Read(panel, columns);
        if (index < 0 || index > styles.Count)
        {
            index = styles.Count;
        }

        styles.Insert(index, new StyleValue(SizeType.Absolute, NewStyleSize));
        panel.SuspendLayout();
        try
        {
            // Grow first so a shifted cell index is still inside the grid.
            Write(panel, columns, styles);
            Shift(panel, columns, index, delta: 1);
        }
        finally
        {
            panel.ResumeLayout(true);
        }
    }

    internal static void RemoveAt(TableLayoutPanel panel, bool columns, int index)
    {
        List<StyleValue> styles = Read(panel, columns);
        if (index < 0 || index >= styles.Count)
        {
            return;
        }

        styles.RemoveAt(index);
        panel.SuspendLayout();
        try
        {
            // Shift while the old index is still valid, then drop the extra style.
            Shift(panel, columns, index, delta: -1);
            Write(panel, columns, styles);
            ClampCells(panel);
        }
        finally
        {
            panel.ResumeLayout(true);
        }
    }

    internal static void Update(TableLayoutPanel panel, bool columns, int index, SizeType sizeType, float size)
    {
        if (columns)
        {
            if (index < 0 || index >= panel.ColumnCount)
            {
                return;
            }

            EnsureColumnStyles(panel, panel.ColumnCount);
            panel.ColumnStyles[index].SizeType = sizeType;
            panel.ColumnStyles[index].Width = size;
        }
        else
        {
            if (index < 0 || index >= panel.RowCount)
            {
                return;
            }

            EnsureRowStyles(panel, panel.RowCount);
            panel.RowStyles[index].SizeType = sizeType;
            panel.RowStyles[index].Height = size;
        }
    }

    private static void EnsureColumnStyles(TableLayoutPanel panel, int count)
    {
        while (panel.ColumnStyles.Count < count)
        {
            panel.ColumnStyles.Add(new ColumnStyle());
        }

        while (panel.ColumnStyles.Count > count)
        {
            panel.ColumnStyles.RemoveAt(panel.ColumnStyles.Count - 1);
        }
    }

    private static void EnsureRowStyles(TableLayoutPanel panel, int count)
    {
        while (panel.RowStyles.Count < count)
        {
            panel.RowStyles.Add(new RowStyle());
        }

        while (panel.RowStyles.Count > count)
        {
            panel.RowStyles.RemoveAt(panel.RowStyles.Count - 1);
        }
    }

    private static void Shift(TableLayoutPanel panel, bool columns, int index, int delta)
    {
        Control[] controls = new Control[panel.Controls.Count];
        panel.Controls.CopyTo(controls, 0);
        foreach (Control control in controls)
        {
            int start = columns ? panel.GetColumn(control) : panel.GetRow(control);
            int span = columns ? panel.GetColumnSpan(control) : panel.GetRowSpan(control);
            if (delta > 0)
            {
                if (start >= index)
                {
                    start++;
                }
                else if (start + span > index)
                {
                    // The new strip sits inside this span, so the span grows with it.
                    span++;
                }
            }
            else if (start > index)
            {
                start--;
            }
            else if (span > 1 && start <= index && start + span - 1 >= index)
            {
                span--;
            }

            if (columns)
            {
                panel.SetColumn(control, start);
                panel.SetColumnSpan(control, Math.Max(1, span));
            }
            else
            {
                panel.SetRow(control, start);
                panel.SetRowSpan(control, Math.Max(1, span));
            }
        }
    }

    private static void ClampCells(TableLayoutPanel panel)
    {
        Control[] controls = new Control[panel.Controls.Count];
        panel.Controls.CopyTo(controls, 0);
        foreach (Control control in controls)
        {
            ClampAxis(panel.ColumnCount, panel.GetColumn(control), panel.GetColumnSpan(control), out int column, out int columnSpan);
            ClampAxis(panel.RowCount, panel.GetRow(control), panel.GetRowSpan(control), out int row, out int rowSpan);
            panel.SetCellPosition(control, new TableLayoutPanelCellPosition(column, row));
            panel.SetColumnSpan(control, columnSpan);
            panel.SetRowSpan(control, rowSpan);
        }
    }

    private static void ClampAxis(int count, int start, int span, out int clampedStart, out int clampedSpan)
    {
        if (count <= 0)
        {
            clampedStart = 0;
            clampedSpan = 1;
            return;
        }

        clampedStart = start >= count ? count - 1 : Math.Max(0, start);
        clampedSpan = Math.Max(1, span);
        if (clampedStart + clampedSpan > count)
        {
            clampedSpan = count - clampedStart;
        }
    }
}

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

using System.Text;
using System.Xml;

namespace Classes;

internal static class Exporter
{
    public static string ToCsv(DataGridView grid)
    {
        var sb = new StringBuilder();
        sb.Append("\"Theme\"");
        for (int r = 0; r < grid.Rows.Count; r++)
        {
            sb.Append(',').Append('"').Append(grid.Rows[r].Cells[0].Value?.ToString() ?? r.ToString()).Append('"');
        }
        sb.AppendLine();

        for (int c = 3; c < grid.Columns.Count; c++)
        {
            var theme = GetPaletteName(grid.Columns[c]);
            sb.Append('"').Append(theme.Replace("\"", "\"\"")).Append('"');
            for (int r = 0; r < grid.Rows.Count; r++)
            {
                sb.Append(',');
                var val = grid.Rows[r].Cells[c].Value?.ToString() ?? string.Empty;
                sb.Append('"').Append(val.Replace("\"", "\"\"")).Append('"');
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }

    public static void ToXml(DataGridView grid, string fileName)
    {
        using (var writer = XmlWriter.Create(fileName, new XmlWriterSettings { Indent = true }))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("PaletteViewerExport");

            for (int c = 3; c < grid.Columns.Count; c++)
            {
                var theme = GetPaletteName(grid.Columns[c]);
                var safeTheme = XmlConvert.EncodeName(theme);
                writer.WriteStartElement(safeTheme);

                for (int r = 0; r < grid.Rows.Count; r++)
                {
                    var idx = grid.Rows[r].Cells[0].Value?.ToString() ?? r.ToString();
                    var val = grid.Rows[r].Cells[c].Value?.ToString() ?? string.Empty;

                    writer.WriteStartElement("Color");
                    writer.WriteAttributeString("index", idx);
                    writer.WriteString(val);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
        }
    }

    private static string GetPaletteName(DataGridViewColumn col)
    {
        var full = col.Name ?? string.Empty;
        int idx = full.LastIndexOf('.');
        return idx >= 0 ? full.Substring(idx + 1) : full;
    }

    private static string CleanHeader(DataGridViewColumn col, int columnIndex)
    {
        string header = col.HeaderText;

        // For palette columns (index >= 2) we need full theme name, but without any extra lines like "(baseline)" or warnings.
        if (columnIndex >= 2)
        {
            // Split by line breaks inserted by BreakHeader / extra status lines
            var parts = header.Split('\n');
            var significant = new System.Collections.Generic.List<string>();
            foreach (var p in parts)
            {
                var part = p.Trim();
                if (string.IsNullOrEmpty(part))
                    continue;

                // Skip status annotations
                if (part.StartsWith("(") || part.StartsWith("⚠"))
                    break;

                significant.Add(part);
            }

            header = string.Join(" ", significant);
        }
        else
        {
            // For non-palette columns keep original behaviour (single-line, replace newlines with space)
            header = header.Replace("\n", " ").Trim();
        }

        return header;
    }
}
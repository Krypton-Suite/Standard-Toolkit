#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Repro for issue #4413: disabled caption glyphs on Office 2010, Office 2013, and Microsoft 365.
/// </summary>
public sealed class Bug4413DisabledCaptionGlyphDemo : KryptonForm
{
    public Bug4413DisabledCaptionGlyphDemo()
    {
        Text = @"Bug #4413 - Disabled caption glyph";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(920, 520);
        MinimumSize = new Size(760, 440);

        var info = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 78,
            Text =
                @"Issue #4413: disabled min, max, and close glyphs use a theme colour. The close-button fill stays empty." + Environment.NewLine +
                @"The disabled Close text should match the swatch. Official form captions still draw the disabled bitmap; these buttons show the vector glyph."
        };

        var table = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 4,
            RowCount = 7,
            Padding = new Padding(12),
            AutoScroll = true
        };
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 210));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44));
        for (var row = 0; row < 7; row++)
        {
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, row == 0 ? 28 : 52));
        }

        AddHeader(table);
        var modes = new (string Caption, PaletteMode Mode)[]
        {
            (@"Office 2010 Blue", PaletteMode.Office2010Blue),
            (@"Office 2010 Black", PaletteMode.Office2010Black),
            (@"Office 2013 White", PaletteMode.Office2013White),
            (@"Office 2013 Dark Gray", PaletteMode.Office2013DarkGray),
            (@"Microsoft 365 Blue", PaletteMode.Microsoft365Blue),
            (@"Microsoft 365 Black", PaletteMode.Microsoft365Black)
        };
        for (var i = 0; i < modes.Length; i++)
        {
            AddRow(table, i + 1, modes[i].Caption, modes[i].Mode);
        }

        Controls.Add(table);
        Controls.Add(info);
    }

    private static void AddHeader(TableLayoutPanel table)
    {
        table.Controls.Add(Header(@"Theme"), 0, 0);
        table.Controls.Add(Header(@"Enabled"), 1, 0);
        table.Controls.Add(Header(@"Disabled"), 2, 0);
        table.Controls.Add(Header(@"Disabled glyph / fill"), 3, 0);
    }

    private static KryptonLabel Header(string text) => new KryptonLabel
    {
        Text = text,
        LabelStyle = LabelStyle.BoldControl,
        Dock = DockStyle.Fill
    };

    private static void AddRow(TableLayoutPanel table, int row, string caption, PaletteMode mode)
    {
        table.Controls.Add(new KryptonLabel
        {
            Text = caption,
            Dock = DockStyle.Fill,
            LabelStyle = LabelStyle.NormalControl
        }, 0, row);

        table.Controls.Add(CreateCaptionButton(mode, enabled: true), 1, row);
        table.Controls.Add(CreateCaptionButton(mode, enabled: false), 2, row);

        var palette = KryptonManager.GetPaletteForMode(mode);
        var glyph = palette.GetContentShortTextColor1(PaletteContentStyle.ButtonFormClose, PaletteState.Disabled);
        var fill = palette.GetBackColor1(PaletteBackStyle.ButtonFormClose, PaletteState.Disabled);
        var swatch = new Panel
        {
            BackColor = glyph,
            Size = new Size(28, 28),
            Margin = new Padding(8, 10, 8, 10)
        };
        var readout = new KryptonLabel
        {
            Text = $"Glyph {FormatColor(glyph)}   Fill {(fill.IsEmpty ? "empty" : FormatColor(fill))}",
            Dock = DockStyle.Fill,
            LabelStyle = LabelStyle.NormalControl
        };
        var host = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            Margin = new Padding(0)
        };
        host.Controls.Add(swatch);
        host.Controls.Add(readout);
        table.Controls.Add(host, 3, row);
    }

    private static KryptonButton CreateCaptionButton(PaletteMode mode, bool enabled) => new()
    {
        Text = @"Close",
        Enabled = enabled,
        ButtonStyle = ButtonStyle.FormClose,
        PaletteMode = mode,
        Dock = DockStyle.Fill,
        Margin = new Padding(8, 6, 8, 6)
    };

    private static string FormatColor(Color color) => $"#{color.R:X2}{color.G:X2}{color.B:X2}";
}

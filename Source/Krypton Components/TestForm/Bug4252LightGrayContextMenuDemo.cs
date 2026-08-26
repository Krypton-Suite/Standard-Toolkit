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
/// Repro for issue #4252: Light Gray Office 2007 / 2010 / Microsoft 365 palettes threw
/// <see cref="NotImplementedException"/> from <c>GetContextMenuSubMenuImage</c>.
/// </summary>
public sealed class Bug4252LightGrayContextMenuDemo : KryptonForm
{
    public Bug4252LightGrayContextMenuDemo()
    {
        Text = @"Bug #4252 — Light Gray GetContextMenuSubMenuImage";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(720, 420);
        MinimumSize = new Size(560, 320);

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 88,
            Padding = new Padding(12),
            Text =
                "Issue #4252: Office 2007, Office 2010, and Microsoft 365 Light Gray palettes used to throw " +
                "NotImplementedException from GetContextMenuSubMenuImage when a nested context menu was drawn.\r\n" +
                "Each row below constructs that palette and calls the method. PASS means a chevron image was returned."
        };

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            ColumnCount = 3,
            RowCount = 4
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 34F));

        layout.Controls.Add(new KryptonLabel { Text = @"Palette", AutoSize = true }, 0, 0);
        layout.Controls.Add(new KryptonLabel { Text = @"Image", AutoSize = true }, 1, 0);
        layout.Controls.Add(new KryptonLabel { Text = @"Result", AutoSize = true }, 2, 0);

        AddProbeRow(layout, 1, @"Office2007LightGray", CreateOffice2007LightGray());
        AddProbeRow(layout, 2, @"Office2010LightGray", CreateOffice2010LightGray());
        AddProbeRow(layout, 3, @"Microsoft365LightGray", CreateMicrosoft365LightGray());

        Controls.Add(layout);
        Controls.Add(instructions);
    }

    private static void AddProbeRow(TableLayoutPanel layout, int row, string name, PaletteBase palette)
    {
        layout.Controls.Add(new KryptonLabel { Text = name, AutoSize = true, Dock = DockStyle.Fill }, 0, row);

        var picture = new PictureBox
        {
            Dock = DockStyle.Fill,
            SizeMode = PictureBoxSizeMode.CenterImage,
            BorderStyle = BorderStyle.FixedSingle
        };
        var result = new KryptonLabel { AutoSize = true, Dock = DockStyle.Fill };

        try
        {
            Image? image = palette.GetContextMenuSubMenuImage();
            picture.Image = image;
            result.Text = image is null
                ? @"FAIL: returned null"
                : $@"PASS: {image.Width}×{image.Height}";
        }
        catch (Exception ex)
        {
            result.Text = $@"FAIL: {ex.GetType().Name}: {ex.Message}";
        }

        layout.Controls.Add(picture, 1, row);
        layout.Controls.Add(result, 2, row);
    }

    private static PaletteOffice2007LightGray CreateOffice2007LightGray()
    {
        var scheme = new EmptySchemeBase();
        return new PaletteOffice2007LightGray(
            nameof(PaletteOffice2007LightGray),
            scheme.ToArray(),
            new ImageList(),
            new ImageList(),
            new Image?[8],
            scheme.ToTrackBarArray());
    }

    private static PaletteOffice2010LightGray CreateOffice2010LightGray()
    {
        var scheme = new EmptySchemeBase();
        return new PaletteOffice2010LightGray(
            scheme.ToArray(),
            new ImageList(),
            new ImageList(),
            new Image?[8],
            scheme.ToTrackBarArray());
    }

    private static PaletteMicrosoft365LightGray CreateMicrosoft365LightGray()
    {
        var scheme = new EmptySchemeBase();
        return new PaletteMicrosoft365LightGray(
            scheme.ToArray(),
            new ImageList(),
            new ImageList(),
            new Image?[8],
            scheme.ToTrackBarArray());
    }
}

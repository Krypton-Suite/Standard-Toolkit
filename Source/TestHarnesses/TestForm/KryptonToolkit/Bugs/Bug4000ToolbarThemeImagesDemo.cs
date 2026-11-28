#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Manual validation for Issue #4000: <see cref="KryptonManager.Images"/>.ToolbarImages
/// must refresh from the active theme when the global palette changes.
/// </summary>
public class Bug4000ToolbarThemeImagesDemo : KryptonForm
{
    private readonly KryptonThemeComboBox _themeCombo;
    private readonly KryptonLabel _statusLabel;
    private readonly FlowLayoutPanel _imageHost;
    private readonly List<(string Name, PictureBox Box)> _slots = new();

    public Bug4000ToolbarThemeImagesDemo()
    {
        Text = "Bug 4000 — Toolbar images from theme";
        Width = 920;
        Height = 420;
        StartPosition = FormStartPosition.CenterScreen;

        var root = new KryptonPanel { Dock = DockStyle.Fill };
        Controls.Add(root);

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 4,
            Padding = new Padding(12)
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        root.Controls.Add(layout);

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Fill,
            AutoSize = false,
            Height = 72,
            TextAlign = ContentAlignment.TopLeft,
            Text =
                "Issue #4000: KryptonManager.Images.ToolbarImages must replace its image pack when the theme changes.\r\n" +
                "Change the theme below (or from the TestForm menu). The icons should update to the matching Office / System / Microsoft 365 pack.\r\n" +
                "Before the fix, repeated theme changes kept showing the first pack because images were appended instead of replaced."
        };
        layout.Controls.Add(instructions, 0, 0);

        var themeRow = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            AutoSize = true,
            WrapContents = false
        };
        themeRow.Controls.Add(new KryptonLabel { Values = { Text = "Theme:" }, Padding = new Padding(0, 6, 8, 0) });
        _themeCombo = new KryptonThemeComboBox { Width = 280 };
        themeRow.Controls.Add(_themeCombo);
        layout.Controls.Add(themeRow, 0, 1);

        _statusLabel = new KryptonLabel
        {
            Dock = DockStyle.Fill,
            Values = { Text = "Status" }
        };
        layout.Controls.Add(_statusLabel, 0, 2);

        _imageHost = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
            WrapContents = true,
            Padding = new Padding(0, 8, 0, 0)
        };
        layout.Controls.Add(_imageHost, 0, 3);

        CreateSlots();
        RefreshImages();

        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
        FormClosed += (_, _) => KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
    }

    private void CreateSlots()
    {
        string[] names =
        [
            "New", "Open", "Save", "SaveAs", "SaveAll",
            "Cut", "Copy", "Paste", "Undo", "Redo",
            "PageSetup", "PrintPreview", "Print", "QuickPrint"
        ];

        foreach (string name in names)
        {
            var cell = new Panel
            {
                Width = 88,
                Height = 72,
                Margin = new Padding(4)
            };

            var caption = new Label
            {
                Text = name,
                Dock = DockStyle.Bottom,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 20,
                AutoEllipsis = true
            };

            var box = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.CenterImage,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            cell.Controls.Add(box);
            cell.Controls.Add(caption);
            _imageHost.Controls.Add(cell);
            _slots.Add((name, box));
        }
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e) => RefreshImages();

    private void RefreshImages()
    {
        var images = KryptonManager.Images.ToolbarImages;
        Image[] values =
        [
            images.New, images.Open, images.Save, images.SaveAs, images.SaveAll,
            images.Cut, images.Copy, images.Paste, images.Undo, images.Redo,
            images.PageSetup, images.PrintPreview, images.Print, images.QuickPrint
        ];

        for (int i = 0; i < _slots.Count; i++)
        {
            _slots[i].Box.Image = values[i];
        }

        _statusLabel.Values.Text =
            $"Palette: {KryptonManager.CurrentGlobalPaletteMode}  |  " +
            $"ToolbarImages.IsDefault: {images.IsDefault}  |  " +
            $"New size: {images.New.Width}x{images.New.Height}";
    }
}

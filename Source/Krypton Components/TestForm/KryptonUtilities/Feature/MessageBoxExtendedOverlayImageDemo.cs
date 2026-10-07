#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Demo for Issue #4162: optional macOS-style overlay (badge) image on
/// <see cref="KryptonMessageBoxExtended"/> main icons via <see cref="KryptonMessageBoxExtendedData"/>
/// and key <c>Show</c> overloads.
/// </summary>
public sealed class MessageBoxExtendedOverlayImageDemo : KryptonForm
{
    private readonly KryptonComboBox _cmbPosition;
    private readonly KryptonCheckBox _chkRtl;
    private readonly PictureBox _picPreview;
    private Image? _badgeImage;
    private Image? _customMainImage;

    public MessageBoxExtendedOverlayImageDemo()
    {
        Text = @"4162 — MessageBox Extended Overlay Image";
        Size = new Size(760, 520);
        StartPosition = FormStartPosition.CenterScreen;

        _badgeImage = CreateBadgeImage();
        _customMainImage = SystemIcons.Application.ToBitmap();

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 88,
            Padding = new Padding(12),
            Text =
                "Issue #4162: optional overlay (badge) on the KryptonMessageBoxExtended icon — similar to macOS app badges.\r\n" +
                "Compare no-overlay vs overlay for stock, application, and custom main icons. Use the position combo and RTL checkbox, then open a dialog."
        };

        var options = new KryptonPanel { Dock = DockStyle.Top, Height = 48, Padding = new Padding(12, 8, 12, 8) };
        var optionsLayout = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false };

        optionsLayout.Controls.Add(new KryptonLabel { Text = @"Overlay position:", AutoSize = true, Padding = new Padding(0, 6, 0, 0) });
        _cmbPosition = new KryptonComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 140 };
        _cmbPosition.Items.AddRange(new object[]
        {
            OverlayImagePosition.BottomRight,
            OverlayImagePosition.BottomLeft,
            OverlayImagePosition.TopRight,
            OverlayImagePosition.TopLeft
        });
        _cmbPosition.SelectedItem = OverlayImagePosition.BottomRight;
        _cmbPosition.SelectedIndexChanged += (_, _) => UpdatePreview();
        optionsLayout.Controls.Add(_cmbPosition);

        _chkRtl = new KryptonCheckBox { Text = @"RTL (mirror Left/Right)", Padding = new Padding(16, 4, 0, 0) };
        _chkRtl.CheckedChanged += (_, _) => UpdatePreview();
        optionsLayout.Controls.Add(_chkRtl);
        options.Controls.Add(optionsLayout);

        var previewPanel = new KryptonGroupBox
        {
            Dock = DockStyle.Top,
            Height = 120,
            Values = { Heading = @"Live composite preview (ComposeOverlayImage)" }
        };
        _picPreview = new PictureBox
        {
            Dock = DockStyle.Fill,
            SizeMode = PictureBoxSizeMode.CenterImage,
            BackColor = Color.Transparent
        };
        previewPanel.Panel.Controls.Add(_picPreview);

        var buttons = new KryptonPanel { Dock = DockStyle.Fill, Padding = new Padding(12) };
        var grid = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 4
        };
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        for (var i = 0; i < 4; i++)
        {
            grid.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        }

        grid.Controls.Add(CreateActionButton("Stock icon — no overlay", (_, _) => ShowStock(useOverlay: false)), 0, 0);
        grid.Controls.Add(CreateActionButton("Stock icon — with overlay", (_, _) => ShowStock(useOverlay: true)), 1, 0);
        grid.Controls.Add(CreateActionButton("Application icon — no overlay", (_, _) => ShowApplication(useOverlay: false)), 0, 1);
        grid.Controls.Add(CreateActionButton("Application icon — with overlay", (_, _) => ShowApplication(useOverlay: true)), 1, 1);
        grid.Controls.Add(CreateActionButton("Custom icon — no overlay", (_, _) => ShowCustom(useOverlay: false)), 0, 2);
        grid.Controls.Add(CreateActionButton("Custom icon — with overlay (Show overload)", (_, _) => ShowCustom(useOverlay: true)), 1, 2);
        grid.Controls.Add(CreateActionButton("Core KryptonMessageBox + overlay", (_, _) => ShowCoreMessageBox()), 0, 3);
        grid.Controls.Add(CreateActionButton("Data struct — full overlay options", (_, _) => ShowViaData()), 1, 3);
        buttons.Controls.Add(grid);

        Controls.Add(buttons);
        Controls.Add(previewPanel);
        Controls.Add(options);
        Controls.Add(instructions);

        UpdatePreview();
    }

    private OverlayImagePosition SelectedPosition =>
        _cmbPosition.SelectedItem is OverlayImagePosition position
            ? position
            : OverlayImagePosition.BottomRight;

    private void ShowStock(bool useOverlay)
    {
        var data = new KryptonMessageBoxExtendedData
        {
            Owner = this,
            Caption = useOverlay ? @"Stock + overlay" : @"Stock (no overlay)",
            MessageText = useOverlay
                ? "Information icon with an optional badge overlay (data path)."
                : "Information icon with no overlay.",
            Buttons = ExtendedMessageBoxButtons.OK,
            Icon = ExtendedKryptonMessageBoxIcon.Information,
            Options = _chkRtl.Checked ? MessageBoxOptions.RtlReading : 0,
            OverlayImage = useOverlay
                ? new KryptonOverlayImage(_badgeImage, SelectedPosition)
                : default
        };

        KryptonMessageBoxExtended.Show(data);
    }

    private void ShowApplication(bool useOverlay)
    {
        var data = new KryptonMessageBoxExtendedData
        {
            Owner = this,
            Caption = useOverlay ? @"Application + overlay" : @"Application (no overlay)",
            MessageText = "Uses ExtendedKryptonMessageBoxIcon.Application with the current process path.",
            Buttons = ExtendedMessageBoxButtons.OK,
            Icon = ExtendedKryptonMessageBoxIcon.Application,
            ApplicationPath = Application.ExecutablePath,
            Options = _chkRtl.Checked ? MessageBoxOptions.RtlReading : 0,
            OverlayImage = useOverlay
                ? new KryptonOverlayImage(_badgeImage, SelectedPosition)
                : default
        };

        KryptonMessageBoxExtended.Show(data);
    }

    private void ShowCustom(bool useOverlay)
    {
        var options = _chkRtl.Checked ? MessageBoxOptions.RtlReading : 0;

        if (!useOverlay)
        {
            // Positional false after options selects the displayHelpButton overload (not helpFilePath).
            KryptonMessageBoxExtended.Show(this,
                "Custom main image with no overlay.",
                "Custom (no overlay)",
                ExtendedMessageBoxButtons.OK,
                ExtendedKryptonMessageBoxIcon.Custom,
                KryptonMessageBoxDefaultButton.Button1,
                options,
                false,
                customImageIcon: _customMainImage);
            return;
        }

        KryptonMessageBoxExtended.Show(this,
            "Custom main image with overlay via Show(..., overlayImage, overlayImagePosition).",
            "Custom + overlay",
            ExtendedMessageBoxButtons.OK,
            ExtendedKryptonMessageBoxIcon.Custom,
            KryptonMessageBoxDefaultButton.Button1,
            options,
            false,
            customImageIcon: _customMainImage,
            overlayImage: _badgeImage,
            overlayImagePosition: SelectedPosition);
    }

    private void ShowViaData()
    {
        var data = new KryptonMessageBoxExtendedData
        {
            Owner = this,
            Caption = @"Data struct overlay",
            MessageText = "Full KryptonOverlayImage options: BottomRight-style badge with Percentage scale.",
            Buttons = ExtendedMessageBoxButtons.OKCancel,
            Icon = ExtendedKryptonMessageBoxIcon.Warning,
            Options = _chkRtl.Checked ? MessageBoxOptions.RtlReading : 0,
            OverlayImage = new KryptonOverlayImage(
                _badgeImage,
                SelectedPosition,
                OverlayImageScaleMode.Percentage,
                KryptonOverlayImage.DefaultScaleFactor,
                KryptonOverlayImage.DefaultFixedSize,
                Color.Empty)
        };

        KryptonMessageBoxExtended.Show(data);
    }

    private void ShowCoreMessageBox()
    {
        KryptonMessageBox.Show(this,
            "Core KryptonMessageBox with optional overlayImage / overlayImagePosition.",
            "Core MessageBox overlay",
            KryptonMessageBoxButtons.OK,
            displayHelpButton: false,
            icon: KryptonMessageBoxIcon.Information,
            defaultButton: KryptonMessageBoxDefaultButton.Button1,
            options: _chkRtl.Checked ? MessageBoxOptions.RtlReading : 0,
            overlayImage: _badgeImage,
            overlayImagePosition: SelectedPosition);
    }

    private void UpdatePreview()
    {
        if (_customMainImage == null || _badgeImage == null)
        {
            return;
        }

        Image? previous = _picPreview.Image;
        Bitmap? composed = GraphicsExtensions.TryComposeOverlay(
            _customMainImage,
            new KryptonOverlayImage(_badgeImage, SelectedPosition),
            _chkRtl.Checked);

        _picPreview.Image = composed;
        previous?.Dispose();
    }

    private static Image CreateBadgeImage()
    {
        var bitmap = new Bitmap(32, 32);
        using (var g = Graphics.FromImage(bitmap))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.Transparent);
            using (var brush = new SolidBrush(Color.FromArgb(220, 220, 20, 60)))
            {
                g.FillEllipse(brush, 2, 2, 28, 28);
            }

            using (var font = new Font("Segoe UI", 12f, FontStyle.Bold))
            using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                g.DrawString("!", font, Brushes.White, new RectangleF(0, 0, 32, 32), sf);
            }
        }

        return bitmap;
    }

    private KryptonButton CreateActionButton(string text, EventHandler onClick)
    {
        var button = new KryptonButton
        {
            Dock = DockStyle.Fill,
            Margin = new Padding(4),
            Values = { Text = text }
        };
        button.Click += onClick;
        return button;
    }

    /// <inheritdoc />
    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _picPreview.Image?.Dispose();
        _picPreview.Image = null;
        _badgeImage?.Dispose();
        _badgeImage = null;
        _customMainImage?.Dispose();
        _customMainImage = null;
        base.OnFormClosed(e);
    }
}

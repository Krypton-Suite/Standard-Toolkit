using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Krypton.Toolkit.Utilities;

namespace TestForm;

public partial class QRCodeDemo : KryptonForm
{
    /// <summary>Maximum UTF-8 payload (bytes) for QR version 40 by ECC level (byte mode).</summary>
    private static readonly int[] MaxPayloadBytes =
    {
        2953, 2331, 1663, 1273
    };

    public QRCodeDemo()
    {
        InitializeComponent();
        SetupDemo();
    }

    private void SetupDemo()
    {
        _cmbEcc.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbEcc.Items.AddRange(new object[]
        {
            "L — max capacity (~7% recovery)",
            "M — default (~15% recovery)",
            "Q — durable (~25% recovery)",
            "H — max durability (~30% recovery)"
        });
        _cmbEcc.SelectedIndex = 1;

        _numModule.Minimum = 1;
        _numModule.Maximum = 20;
        _numModule.Value = 4;

        PaletteState paletteState = PaletteState.Normal;
        PaletteBase palette = _kryptonQRCode.GetResolvedPalette();
        _kcbtnDarkColor.SelectedColor = palette.GetContentShortTextColor1(PaletteContentStyle.LabelNormalPanel, paletteState);
        _kcbtnLightColor.SelectedColor = _kryptonQRCode.StateNormal.GetBackColor1(paletteState);
        _kcbtnDarkColor.Values.Text = "Dark modules";
        _kcbtnLightColor.Values.Text = "Light modules";

        _txtContent.Text = "https://github.com/Krypton-Suite/Standard-Toolkit";

        _txtContent.TextChanged += (_, _) => SyncQrFromUi();
        _cmbEcc.SelectedIndexChanged += (_, _) => SyncQrFromUi();
        _numModule.ValueChanged += (_, _) => SyncQrFromUi();
        _chkQuietZone.CheckedChanged += (_, _) => SyncQrFromUi();
        _kcbtnDarkColor.SelectedColorChanged += OnDarkModuleColorChanged;
        _kcbtnLightColor.SelectedColorChanged += OnLightModuleColorChanged;

        _kbtnSampleUrl.Click += (_, _) => SetSample(_txtSampleUrl);
        _kbtnSampleVCard.Click += (_, _) => SetSample(_sampleVCard);
        _kbtnSampleUnicode.Click += (_, _) => SetSample("Hello 世界 🎉");
        _kbtnClear.Click += (_, _) => { _txtContent.Clear(); SyncQrFromUi(); };

        _kbtnSavePng.Click += OnSavePngClick;
        _kbtnCopyImage.Click += OnCopyImageClick;
        _kbtnStaticApi.Click += OnStaticApiClick;

        _kryptonQRCode.ContentChanged += (_, _) => UpdateStatus();

        SyncQrFromUi();
    }

    private const string _txtSampleUrl = "https://github.com/Krypton-Suite/Standard-Toolkit";

    private const string _sampleVCard = """
        BEGIN:VCARD
        VERSION:3.0
        FN:Krypton Demo
        ORG:Krypton Suite
        TEL:+1-555-0100
        EMAIL:demo@example.com
        URL:https://github.com/Krypton-Suite/Standard-Toolkit
        END:VCARD
        """;

    private void SetSample(string text)
    {
        _txtContent.Text = text;
        SyncQrFromUi();
    }

    private void SyncQrFromUi()
    {
        _kryptonQRCode.Content = _txtContent.Text;
        _kryptonQRCode.ErrorCorrectionLevel = (QRErrorCorrectionLevel)_cmbEcc.SelectedIndex;
        _kryptonQRCode.ModuleSize = (int)_numModule.Value;
        _kryptonQRCode.ShowBorder = _chkQuietZone.Checked;
        UpdateStatus();
    }

    private void OnDarkModuleColorChanged(object? sender, EventArgs e)
    {
        _kryptonQRCode.DarkColor = _kcbtnDarkColor.SelectedColor;
        UpdateStatus();
    }

    private void OnLightModuleColorChanged(object? sender, EventArgs e)
    {
        _kryptonQRCode.LightColor = _kcbtnLightColor.SelectedColor;
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        int bytes = string.IsNullOrEmpty(_txtContent.Text)
            ? 0
            : Encoding.UTF8.GetByteCount(_txtContent.Text);

        int eccIndex = _cmbEcc.SelectedIndex;
        if (eccIndex < 0) eccIndex = 0;
        else if (eccIndex > 3) eccIndex = 3;
        int maxBytes = MaxPayloadBytes[eccIndex];
        string eccName = ((QRErrorCorrectionLevel)eccIndex).ToString();

        if (bytes == 0)
        {
            _lblStatus.Values.Text = "Enter content to generate a QR code (UTF-8 byte mode, versions 1–40).";
            _lblStatus.StateCommon.ShortText.Color1 = KryptonManager.CurrentGlobalPalette.GetContentShortTextColor1(
                PaletteContentStyle.LabelNormalPanel, PaletteState.Normal);
        }
        else if (bytes > maxBytes)
        {
            _lblStatus.Values.Text = $"Payload too large: {bytes} UTF-8 bytes (max ~{maxBytes} at ECC {eccName}, version 40).";
            _lblStatus.StateCommon.ShortText.Color1 = Color.Firebrick;
        }
        else
        {
            _lblStatus.Values.Text = $"{bytes} UTF-8 bytes — within capacity for ECC {eccName} (max ~{maxBytes} at v40). Scan with a phone to verify.";
            _lblStatus.StateCommon.ShortText.Color1 = KryptonManager.CurrentGlobalPalette.GetContentShortTextColor1(
                PaletteContentStyle.LabelNormalPanel, PaletteState.Normal);
        }
    }

    private void OnSavePngClick(object? sender, EventArgs e)
    {
        using var dlg = new SaveFileDialog
        {
            Filter = @"PNG images (*.png)|*.png|All files (*.*)|*.*",
            FileName = "krypton-qrcode.png",
            Title = @"Save QR code"
        };

        if (dlg.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            _kryptonQRCode.SaveToFile(dlg.FileName, ImageFormat.Png);
            KryptonMessageBox.Show(this, $"Saved to:\n{dlg.FileName}", @"Save QR code", KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show(this, ex.Message, @"Save failed", KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Error);
        }
    }

    private void OnCopyImageClick(object? sender, EventArgs e)
    {
        try
        {
            using Bitmap? bmp = _kryptonQRCode.GetBitmap();
            if (bmp == null)
            {
                KryptonMessageBox.Show(this, @"Nothing to copy — enter content first.", @"Clipboard",
                    KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
                return;
            }

            Clipboard.SetImage(bmp);
            KryptonMessageBox.Show(this, @"QR image copied to the clipboard.", @"Clipboard",
                KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show(this, ex.Message, @"Clipboard failed", KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Error);
        }
    }

    private void OnStaticApiClick(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_txtContent.Text))
        {
            KryptonMessageBox.Show(this, @"Enter content before using the static API.", @"Static API",
                KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
            return;
        }

        try
        {
            using Bitmap bmp = KryptonQRCode.GenerateBitmap(
                _txtContent.Text,
                (int)_numModule.Value,
                (QRErrorCorrectionLevel)_cmbEcc.SelectedIndex,
                _kcbtnDarkColor.SelectedColor,
                _kcbtnLightColor.SelectedColor,
                centerImage: _kryptonQRCode.CenterImage,
                centerImageRelativeSize: _kryptonQRCode.CenterImageRelativeSize,
                centerImagePaddingModules: _kryptonQRCode.CenterImagePaddingModules,
                centerImagePalette: _kryptonQRCode.GetCenterImagePalette());

            Clipboard.SetImage(bmp);
            KryptonMessageBox.Show(this,
                "KryptonQRCode.GenerateBitmap(...) produced a bitmap with the same settings as the preview and copied it to the clipboard.\n\n"
                + "Use this API when you do not need a control on the form.",
                @"Static API", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
        }
        catch (ArgumentException ex)
        {
            KryptonMessageBox.Show(this, ex.Message, @"Static API", KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show(this, ex.Message, @"Static API", KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Error);
        }
    }
}
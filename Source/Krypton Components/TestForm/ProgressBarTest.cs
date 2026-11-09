#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class ProgressBarTest : KryptonForm
{
    private KryptonLabel? _klblSchemeReadout;
    private KryptonColorButton? _kcbtnSchemeValue;
    private KryptonColorButton? _kcbtnSchemeLow;
    private KryptonColorButton? _kcbtnSchemeMedium;
    private KryptonColorButton? _kcbtnSchemeHigh;
    private KryptonButton? _kbtnSchemeContrast;
    private KryptonButton? _kbtnSchemeReset;
    private KryptonThemeComboBox? _kcmbTheme;
    private PaletteBase? _subscribedPalette;

    public ProgressBarTest()
    {
        InitializeComponent();
        ktrkProgressValues.Value = 75;
        AppendSchemeSlotUi();
        FormClosed += OnFormClosed;
    }

    private void ProgressBarTest_Load(object sender, EventArgs e)
    {
        foreach (var value in Enum.GetValues(typeof(ProgressBarStyle)))
        {
            kcmbProgressBarStyle.Items.Add(value);
        }

        kcmbProgressBarStyle.SelectedIndex = 1;

        foreach (var value in Enum.GetValues(typeof(PaletteColorStyle)))
        {
            kcmbColorStyle.Items.Add(value);
        }

        kcmbColorStyle.SelectedItem = PaletteColorStyle.GlassNormalFull;
        WirePaletteSchemeColorChanged();
        RefreshSchemeReadout();
    }

    private void OnFormClosed(object? sender, FormClosedEventArgs e) => UnwirePaletteSchemeColorChanged();

    private void AppendSchemeSlotUi()
    {
        ClientSize = new Size(Math.Max(ClientSize.Width, 569), Math.Max(ClientSize.Height, 400));
        MinimumSize = new Size(500, 420);

        var klblHeader = new KryptonLabel
        {
            Location = new Point(13, 220),
            Size = new Size(450, 20),
            Name = "klblSchemeHeader"
        };
        klblHeader.Values.Text = "Issue #4684 SchemeBaseColors (ProgressBarValue / Low / Medium / High)";

        _klblSchemeReadout = new KryptonLabel
        {
            Location = new Point(13, 244),
            Size = new Size(450, 36),
            Name = "klblSchemeReadout"
        };
        _klblSchemeReadout.Values.Text = "Slots: …";

        _kcmbTheme = new KryptonThemeComboBox
        {
            Location = new Point(13, 286),
            Size = new Size(220, 22),
            Name = "kcmbTheme"
        };
        _kcmbTheme.SelectedIndexChanged += (_, _) =>
        {
            WirePaletteSchemeColorChanged();
            RefreshSchemeReadout();
        };

        _kcbtnSchemeValue = CreateSchemeColorButton("Value", 240, 286);
        _kcbtnSchemeLow = CreateSchemeColorButton("Low", 320, 286);
        _kcbtnSchemeMedium = CreateSchemeColorButton("Med", 400, 286);
        _kcbtnSchemeHigh = CreateSchemeColorButton("High", 480, 286);

        _kcbtnSchemeValue.SelectedColorChanged += (_, e) => ApplySchemeColor(SchemeBaseColors.ProgressBarValue, e.Color);
        _kcbtnSchemeLow.SelectedColorChanged += (_, e) => ApplySchemeColor(SchemeBaseColors.ProgressBarLow, e.Color);
        _kcbtnSchemeMedium.SelectedColorChanged += (_, e) => ApplySchemeColor(SchemeBaseColors.ProgressBarMedium, e.Color);
        _kcbtnSchemeHigh.SelectedColorChanged += (_, e) => ApplySchemeColor(SchemeBaseColors.ProgressBarHigh, e.Color);

        _kbtnSchemeContrast = new KryptonButton
        {
            Location = new Point(13, 320),
            Size = new Size(140, 28),
            Name = "kbtnSchemeContrast"
        };
        _kbtnSchemeContrast.Values.Text = "Contrast slots";
        _kbtnSchemeContrast.Click += (_, _) =>
        {
            ApplySchemeColor(SchemeBaseColors.ProgressBarValue, Color.MediumBlue);
            ApplySchemeColor(SchemeBaseColors.ProgressBarLow, Color.Firebrick);
            ApplySchemeColor(SchemeBaseColors.ProgressBarMedium, Color.DarkOrange);
            ApplySchemeColor(SchemeBaseColors.ProgressBarHigh, Color.SeaGreen);
            ClearLocalOverrides();
        };

        _kbtnSchemeReset = new KryptonButton
        {
            Location = new Point(160, 320),
            Size = new Size(140, 28),
            Name = "kbtnSchemeReset"
        };
        _kbtnSchemeReset.Values.Text = "Reset slots";
        _kbtnSchemeReset.Click += (_, _) =>
        {
            // Restore historic Green / Red / Orange / Green factory look
            ApplySchemeColor(SchemeBaseColors.ProgressBarValue, Color.Green);
            ApplySchemeColor(SchemeBaseColors.ProgressBarLow, Color.Red);
            ApplySchemeColor(SchemeBaseColors.ProgressBarMedium, Color.Orange);
            ApplySchemeColor(SchemeBaseColors.ProgressBarHigh, Color.Green);
            ClearLocalOverrides();
            RefreshSchemeReadout();
        };

        kryptonPanel1.Controls.Add(klblHeader);
        kryptonPanel1.Controls.Add(_klblSchemeReadout);
        kryptonPanel1.Controls.Add(_kcmbTheme);
        kryptonPanel1.Controls.Add(_kcbtnSchemeValue);
        kryptonPanel1.Controls.Add(_kcbtnSchemeLow);
        kryptonPanel1.Controls.Add(_kcbtnSchemeMedium);
        kryptonPanel1.Controls.Add(_kcbtnSchemeHigh);
        kryptonPanel1.Controls.Add(_kbtnSchemeContrast);
        kryptonPanel1.Controls.Add(_kbtnSchemeReset);
    }

    private static KryptonColorButton CreateSchemeColorButton(string text, int x, int y)
    {
        var button = new KryptonColorButton
        {
            Location = new Point(x, y),
            Size = new Size(72, 24),
            Name = "kcbtnScheme" + text
        };
        button.Values.Text = text;
        return button;
    }

    private void ClearLocalOverrides()
    {
        ResetBackColor1(kryptonProgressBar1.StateCommon.Back);
        ResetBackColor1(kryptonProgressBar2.StateCommon.Back);
        ResetBackColor1(kryptonProgressBarVert1.StateCommon.Back);
        ResetBackColor1(kryptonProgressBarVert2.StateCommon.Back);
    }

    private static void ResetBackColor1(PaletteBack back)
    {
        PropertyDescriptor? property = TypeDescriptor.GetProperties(back)[nameof(PaletteBack.Color1)];
        property?.ResetValue(back);
    }

    private void WirePaletteSchemeColorChanged()
    {
        UnwirePaletteSchemeColorChanged();
        _subscribedPalette = KryptonManager.CurrentGlobalPalette;
        if (_subscribedPalette != null)
        {
            _subscribedPalette.SchemeColorChanged += OnSchemeColorChanged;
        }
    }

    private void UnwirePaletteSchemeColorChanged()
    {
        if (_subscribedPalette != null)
        {
            _subscribedPalette.SchemeColorChanged -= OnSchemeColorChanged;
            _subscribedPalette = null;
        }
    }

    private void OnSchemeColorChanged(object? sender, SchemeColorChangedEventArgs e) => RefreshSchemeReadout();

    private void RefreshSchemeReadout()
    {
        if (_klblSchemeReadout == null)
        {
            return;
        }

        // Empty Value/High slots inherit TrackBarFillTrack so theme switches change the fill.
        _klblSchemeReadout.Values.Text =
            $"Slots: Value={FormatSchemeColor(SchemeBaseColors.ProgressBarValue)} " +
            $"(fill={FormatSchemeColor(SchemeBaseColors.ProgressBarValue, SchemeBaseColors.TrackBarFillTrack)})  |  " +
            $"Low={FormatSchemeColor(SchemeBaseColors.ProgressBarLow)}  |  " +
            $"Medium={FormatSchemeColor(SchemeBaseColors.ProgressBarMedium)}  |  " +
            $"High={FormatSchemeColor(SchemeBaseColors.ProgressBarHigh, SchemeBaseColors.TrackBarFillTrack)}";
    }

    private static string FormatSchemeColor(SchemeBaseColors role, SchemeBaseColors? emptyFallback = null)
    {
        var palette = KryptonManager.CurrentGlobalPalette;
        if (palette == null)
        {
            return "(no palette)";
        }

        try
        {
            Color color = palette.GetSchemeColor(role);
            if ((color.IsEmpty || color.A == 0) && emptyFallback.HasValue)
            {
                color = palette.GetSchemeColor(emptyFallback.Value);
            }

            return color.IsEmpty ? "Empty" : $"{color.R},{color.G},{color.B}";
        }
        catch
        {
            return "(error)";
        }
    }

    private static void ApplySchemeColor(SchemeBaseColors role, Color color)
    {
        KryptonManager.CurrentGlobalPalette?.SetSchemeColor(role, color);
    }

    private void ktrkProgressValues_ValueChanged(object sender, EventArgs e)
    {
        kryptonProgressBar1.Value = ktrkProgressValues.Value;

        kryptonProgressBar2.Value = ktrkProgressValues.Value;

        kryptonProgressBarVert1.Value = ktrkProgressValues.Value;

        kryptonProgressBarVert2.Value = ktrkProgressValues.Value;
    }

    private void kchkUseProgressValueAsText_CheckedChanged(object sender, EventArgs e)
    {
        kryptonProgressBar1.UseValueAsText = kchkUseProgressValueAsText.Checked;

        kryptonProgressBar2.UseValueAsText = kchkUseProgressValueAsText.Checked;
    }

    private void kcmbProgressBarStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
        kryptonProgressBar1.Style =
            (ProgressBarStyle)Enum.Parse(typeof(ProgressBarStyle), kcmbProgressBarStyle.Text);

        kryptonProgressBar2.Style =
            (ProgressBarStyle)Enum.Parse(typeof(ProgressBarStyle), kcmbProgressBarStyle.Text);

        kryptonProgressBarVert1.Style =
            (ProgressBarStyle)Enum.Parse(typeof(ProgressBarStyle), kcmbProgressBarStyle.Text);

        kryptonProgressBarVert2.Style =
            (ProgressBarStyle)Enum.Parse(typeof(ProgressBarStyle), kcmbProgressBarStyle.Text);
    }

    private void kcbtnProgressBarColour_SelectedColorChanged(object sender, ColorEventArgs e)
    {
        kryptonProgressBar1.StateCommon.Back.Color1 = e.Color;

        kryptonProgressBar2.StateCommon.Back.Color1 = e.Color;

        kryptonProgressBarVert1.StateCommon.Back.Color1 = e.Color;

        kryptonProgressBarVert2.StateCommon.Back.Color1 = e.Color;
    }

    private void kcmbColorStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
        var style = (PaletteColorStyle)Enum.Parse(typeof(PaletteColorStyle), kcmbColorStyle.Text);
        kryptonProgressBar1.ValueBackColorStyle = style;

        kryptonProgressBar2.ValueBackColorStyle = style;

        kryptonProgressBarVert1.ValueBackColorStyle = style;

        kryptonProgressBarVert2.ValueBackColorStyle = style;
    }

    private void kchkShowTextBackdrop_CheckedChanged(object sender, EventArgs e)
    {
        kryptonProgressBar1.ShowTextBackdrop = kchkShowTextBackdrop.Checked;

        kryptonProgressBar2.ShowTextBackdrop = kchkShowTextBackdrop.Checked;

        kryptonProgressBarVert1.ShowTextBackdrop = kchkShowTextBackdrop.Checked;

        kryptonProgressBarVert2.ShowTextBackdrop = kchkShowTextBackdrop.Checked;
    }

    private void kchkShowTextShadow_CheckedChanged(object sender, EventArgs e)
    {
        kryptonProgressBar1.ShowTextShadow = kchkShowTextShadow.Checked;

        kryptonProgressBar2.ShowTextShadow = kchkShowTextShadow.Checked;

        kryptonProgressBarVert1.ShowTextShadow = kchkShowTextShadow.Checked;

        kryptonProgressBarVert2.ShowTextShadow = kchkShowTextShadow.Checked;
    }

    private void kcbtnBackdropColor_SelectedColorChanged(object sender, ColorEventArgs e)
    {
        kryptonProgressBar1.TextBackdropColor = e.Color;

        kryptonProgressBar2.TextBackdropColor = e.Color;

        kryptonProgressBarVert1.TextBackdropColor = e.Color;

        kryptonProgressBarVert2.TextBackdropColor = e.Color;
    }
}

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Manual demo for GitHub #3381: vertical text alignment on <see cref="KryptonButton"/> with large corner rounding
/// relative to control height (wide pill bar, large font, centered text).
/// </summary>
public partial class Bug3381KryptonButtonRoundedTextCenteringDemo : KryptonForm
{
    public Bug3381KryptonButtonRoundedTextCenteringDemo()
    {
        InitializeComponent();
    }

    private static Control NewSectionHeader(string text) =>
        new KryptonLabel
        {
            Text = text,
            Margin = new Padding(0, 12, 0, 4),
            AutoSize = true
        };

    private static Panel NewFillPanel(int height, Control inner)
    {
        var p = new Panel { Height = height, Dock = DockStyle.Fill, Margin = new Padding(0) };
        p.Controls.Add(inner);
        inner.Dock = DockStyle.Fill;
        return p;
    }

    private void OnTrackRoundingValueChanged(object? sender, EventArgs e)
    {
        float v = _trackRounding.Value;
        _lblRoundingValue.Text = string.Format(CultureInfo.InvariantCulture, @"Rounding: {0:0}", v);
        ApplyRounding(_btnLive, v);
        RequestLiveRelayout();
    }

    private void OnCmbTextVSelectedIndexChanged(object? sender, EventArgs e)
    {
        PaletteRelativeAlign align = _cmbTextV.SelectedIndex switch
        {
            1 => PaletteRelativeAlign.Near,
            2 => PaletteRelativeAlign.Far,
            _ => PaletteRelativeAlign.Center
        };
        _btnLive.StateCommon.Content.ShortText.TextV = align;
        RequestLiveRelayout();
    }

    private void OnNudFontSizeValueChanged(object? sender, EventArgs e)
    {
        float pt = (float)_nudFontSize.Value;
        string familyName = _btnLive.StateCommon.Content.ShortText.Font?.FontFamily?.Name ?? FontFamily.GenericSansSerif.Name;
        _btnLive.StateCommon.Content.ShortText.Font = new Font(familyName, pt, FontStyle.Regular, GraphicsUnit.Point);
        RequestLiveRelayout();
    }

    private void OnNudLiveHeightValueChanged(object? sender, EventArgs e)
    {
        int h = (int)_nudLiveHeight.Value;
        _pLiveButtonHost.Height = Math.Max(48, h);
        RequestLiveRelayout();
    }

    private void RequestLiveRelayout()
    {
        _btnLive.Invalidate(true);
        _root.PerformLayout();
    }

    private static KryptonButton CreatePillButton(string text, float fontSizePt, float rounding)
    {
        var b = new KryptonButton
        {
            Values = { Text = text },
            AutoSize = false
        };
        StylePillSurface(b);
        b.StateCommon.Content.ShortText.Font = new Font(@"Segoe UI", fontSizePt, FontStyle.Regular, GraphicsUnit.Point);
        b.StateCommon.Content.ShortText.TextH = PaletteRelativeAlign.Center;
        b.StateCommon.Content.ShortText.TextV = PaletteRelativeAlign.Center;
        b.StateCommon.Content.ShortText.Hint = PaletteTextHint.AntiAlias;
        ApplyRounding(b, rounding);
        return b;
    }

    private static void StylePillSurface(KryptonButton b)
    {
        Color fill = Color.YellowGreen;
        Color edge = Color.YellowGreen;
        b.StateCommon.Back.Color1 = fill;
        b.StateCommon.Back.Color2 = fill;
        b.StateCommon.Border.Color1 = edge;
        b.StateCommon.Border.DrawBorders = PaletteDrawBorders.All;
        b.StateCommon.Border.Width = 2;
        b.StateCommon.Content.ShortText.Color1 = Color.White;
        b.OverrideDefault.Back.Color1 = fill;
        b.OverrideDefault.Back.Color2 = fill;
        b.OverrideDefault.Border.Color1 = edge;
        b.OverrideDefault.Border.DrawBorders = PaletteDrawBorders.All;
    }

    private static void ApplyRounding(KryptonButton b, float rounding)
    {
        b.StateCommon.Border.Rounding = rounding;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        OnNudLiveHeightValueChanged(null, EventArgs.Empty);
    }
}

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion


namespace TestForm;

/// <summary>
/// Issue #4685: <see cref="KryptonManager.UseKryptonColorsOnForm"/>, <see cref="KryptonManager.PanelStyle"/>,
/// and <see cref="KryptonForm.PanelStyle"/> sync form / internal panel <c>BackColor</c> to the effective
/// palette panel style (or <see cref="SystemColors.Control"/> when the manager flag is off).
/// </summary>
public partial class FormClientBackColorDemo : KryptonForm
{
    private readonly bool _originalUseKryptonColorsOnForm;
    private readonly PaletteBackStyle _originalManagerPanelStyle;
    private readonly PaletteMode _originalPaletteMode;
    private readonly KryptonManager _manager = new();
    private bool _suppressThemeApply;
    private bool _suppressManagerPanelStyleApply;
    private bool _suppressFormPanelStyleApply;

    public FormClientBackColorDemo()
    {
        _originalUseKryptonColorsOnForm = KryptonManager.UseKryptonColorsOnForm;
        _originalManagerPanelStyle = KryptonManager.PanelStyle;
        _originalPaletteMode = KryptonManager.CurrentGlobalPaletteMode;
        InitializeComponent();
        kchkUseKryptonColors.Checked = KryptonManager.UseKryptonColorsOnForm;

        _suppressThemeApply = true;
        kcmbTheme.SelectedIndex = IndexForMode(KryptonManager.CurrentGlobalPaletteMode);
        _suppressThemeApply = false;

        _suppressManagerPanelStyleApply = true;
        kcmbManagerPanelStyle.SelectedIndex = IndexForPanelStyle(KryptonManager.PanelStyle);
        _suppressManagerPanelStyleApply = false;

        _suppressFormPanelStyleApply = true;
        kcmbPanelStyle.SelectedIndex = 0; // Inherit
        InheritFormPanelStyle();
        _suppressFormPanelStyleApply = false;

        UpdateStatus();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        KryptonManager.UseKryptonColorsOnForm = _originalUseKryptonColorsOnForm;
        KryptonManager.PanelStyle = _originalManagerPanelStyle;
        _manager.GlobalPaletteMode = _originalPaletteMode;
        base.OnFormClosed(e);
    }

    private void kchkUseKryptonColors_CheckedChanged(object? sender, EventArgs e)
    {
        KryptonManager.UseKryptonColorsOnForm = kchkUseKryptonColors.Checked;
        UpdateStatus();
    }

    private void kcmbTheme_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_suppressThemeApply || kcmbTheme.SelectedIndex < 0)
        {
            return;
        }

        _manager.GlobalPaletteMode = ModeForIndex(kcmbTheme.SelectedIndex);
        UpdateStatus();
    }

    private void kcmbManagerPanelStyle_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_suppressManagerPanelStyleApply || kcmbManagerPanelStyle.SelectedIndex < 0)
        {
            return;
        }

        KryptonManager.PanelStyle = PanelStyleForIndex(kcmbManagerPanelStyle.SelectedIndex);
        UpdateStatus();
    }

    private void kcmbPanelStyle_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_suppressFormPanelStyleApply || kcmbPanelStyle.SelectedIndex < 0)
        {
            return;
        }

        if (kcmbPanelStyle.SelectedIndex == 0)
        {
            InheritFormPanelStyle();
        }
        else
        {
            PanelStyle = PanelStyleForIndex(kcmbPanelStyle.SelectedIndex - 1);
        }

        UpdateStatus();
    }

    private void InheritFormPanelStyle() => ResetPanelStyle();

    private bool HasLocalPanelStyleOverride() => kcmbPanelStyle.SelectedIndex > 0;

    private void UpdateStatus()
    {
        // Keep demo panels aligned with the form style. KryptonPanel paints from PanelBackStyle,
        // not BackColor — leaving the default PanelClient made Alternate look "wrong" while
        // status still reported the inherited BackColor hex.
        SyncDemoSurfaces();

        Color formBack = BackColor;
        Color panelBack = panelNative.BackColor;
        Color kryptonPanelBack = kryptonPanelSample.BackColor;
        string local = HasLocalPanelStyleOverride() ? PanelStyle.ToString() : "Inherit";
        klblStatus.Values.Text =
            $"UseKryptonColorsOnForm={KryptonManager.UseKryptonColorsOnForm}\r\n" +
            $"Manager.PanelStyle={KryptonManager.PanelStyle}\r\n" +
            $"Form.PanelStyle={local} (effective {PanelStyle})\r\n" +
            $"Form.BackColor={ColorName(formBack)}\r\n" +
            $"Native Panel.BackColor={ColorName(panelBack)}\r\n" +
            $"KryptonPanel.PanelBackStyle={kryptonPanelSample.PanelBackStyle}\r\n" +
            $"KryptonPanel.BackColor={ColorName(kryptonPanelBack)}";
    }

    private void SyncDemoSurfaces()
    {
        PaletteBackStyle style = PanelStyle;
        bool useKrypton = KryptonManager.UseKryptonColorsOnForm;

        kryptonPanel1.PanelBackStyle = style;
        kryptonPanelSample.PanelBackStyle = style;

        if (useKrypton)
        {
            kryptonPanel1.StateCommon.Color1 = Color.Empty;
            kryptonPanel1.StateCommon.ColorStyle = PaletteColorStyle.Inherit;
            kryptonPanelSample.StateCommon.Color1 = Color.Empty;
            kryptonPanelSample.StateCommon.ColorStyle = PaletteColorStyle.Inherit;
        }
        else
        {
            Color control = SystemColors.Control;
            kryptonPanel1.StateCommon.Color1 = control;
            kryptonPanel1.StateCommon.ColorStyle = PaletteColorStyle.Solid;
            kryptonPanelSample.StateCommon.Color1 = control;
            kryptonPanelSample.StateCommon.ColorStyle = PaletteColorStyle.Solid;
        }

        // GroupBox visual styles often ignore ambient BackColor; stamp explicitly so the
        // native column matches Form.BackColor (and keep label readable on dark clients).
        Color back = BackColor;
        Color fore = GetContrastingForeColor(back);
        grpNative.BackColor = back;
        grpNative.ForeColor = fore;
        panelNative.BackColor = back;
        lblNative.ForeColor = fore;
        grpKrypton.BackColor = back;
        grpKrypton.ForeColor = fore;
    }

    private static Color GetContrastingForeColor(Color back)
    {
        // Relative luminance (sRGB) — dark clients need light text for the native label.
        double luminance = ((0.2126 * back.R) + (0.7152 * back.G) + (0.0722 * back.B)) / 255.0;
        return luminance < 0.45 ? Color.White : SystemColors.ControlText;
    }

    private static PaletteMode ModeForIndex(int index) =>
        index switch
        {
            0 => PaletteMode.Microsoft365Blue,
            1 => PaletteMode.Microsoft365BlackDarkMode,
            2 => PaletteMode.Office2010Blue,
            3 => PaletteMode.Office2010Black,
            4 => PaletteMode.SparkleBlue,
            _ => PaletteMode.Microsoft365Blue
        };

    private static int IndexForMode(PaletteMode mode) =>
        mode switch
        {
            PaletteMode.Microsoft365Blue => 0,
            PaletteMode.Microsoft365BlackDarkMode => 1,
            PaletteMode.Office2010Blue => 2,
            PaletteMode.Office2010Black => 3,
            PaletteMode.SparkleBlue => 4,
            _ => 0
        };

    private static PaletteBackStyle PanelStyleForIndex(int index) =>
        index switch
        {
            0 => PaletteBackStyle.PanelClient,
            1 => PaletteBackStyle.PanelAlternate,
            2 => PaletteBackStyle.PanelCustom1,
            _ => PaletteBackStyle.PanelClient
        };

    private static int IndexForPanelStyle(PaletteBackStyle style) =>
        style switch
        {
            PaletteBackStyle.PanelClient => 0,
            PaletteBackStyle.PanelAlternate => 1,
            PaletteBackStyle.PanelCustom1 => 2,
            _ => 0
        };

    private static string ColorName(Color c) =>
        c.IsNamedColor ? c.Name : $"#{c.R:X2}{c.G:X2}{c.B:X2}";
}
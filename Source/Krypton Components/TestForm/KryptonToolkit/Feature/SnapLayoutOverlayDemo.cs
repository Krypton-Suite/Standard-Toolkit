#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Exercises <see cref="KryptonForm.SnapLayouts"/> (issue #4579).
/// Hover this form's maximize button. The form inherits KryptonManager.SnapLayoutValues.Mode.
/// System keeps the Windows menu. Toolkit draws the palette menu.
/// </summary>
public partial class SnapLayoutOverlayDemo : KryptonForm
{
    private static readonly PaletteMode[] Themes =
    {
        PaletteMode.Microsoft365Blue,
        PaletteMode.Office2007Blue,
        PaletteMode.Office2007Black,
        PaletteMode.Office2010Blue,
        PaletteMode.SparkleBlue,
        PaletteMode.ProfessionalSystem
    };

    public SnapLayoutOverlayDemo()
    {
        InitializeComponent();
        kcmbMode.SelectedIndexChanged += OnModeChanged;
        kcmbGlobal.SelectedIndexChanged += OnGlobalChanged;
        kchkShowIcon.CheckedChanged += OnShowIconChanged;
        kchkShowPreview.CheckedChanged += OnShowPreviewChanged;
        knudPreviewOpacity.ValueChanged += OnPreviewOpacityChanged;
        kcmbTheme.SelectedIndexChanged += OnThemeChanged;
        kchkIncludeBuiltin.CheckedChanged += OnIncludeBuiltinChanged;
        kbtnAddCustom.Click += OnAddCustomClick;
        kbtnClearCustom.Click += OnClearCustomClick;
        kchkShowShadow.CheckedChanged += OnShowShadowChanged;
        knudShadowOffset.ValueChanged += OnShadowOffsetChanged;
        knudShadowBlur.ValueChanged += OnShadowBlurChanged;
        knudShadowOpacity.ValueChanged += OnShadowOpacityChanged;
    }

    private void SnapLayoutOverlayDemo_Load(object? sender, EventArgs e)
    {
        kcmbMode.Items.Clear();
        kcmbMode.Items.Add(KryptonSnapLayoutMode.Inherit);
        kcmbMode.Items.Add(KryptonSnapLayoutMode.Toolkit);
        kcmbMode.Items.Add(KryptonSnapLayoutMode.System);
        kcmbMode.Items.Add(KryptonSnapLayoutMode.None);
        kcmbMode.SelectedItem = SnapLayouts;

        kcmbGlobal.Items.Clear();
        kcmbGlobal.Items.Add(KryptonSnapLayoutMode.System);
        kcmbGlobal.Items.Add(KryptonSnapLayoutMode.Toolkit);
        kcmbGlobal.Items.Add(KryptonSnapLayoutMode.None);
        SnapLayoutValues snap = KryptonManager.SnapLayoutValues;
        kcmbGlobal.SelectedItem = snap.Mode;
        kchkShowIcon.Checked = snap.ShowIcon;
        kchkShowPreview.Checked = snap.ShowPreview;
        kchkIncludeBuiltin.Checked = snap.IncludeBuiltinLayouts;
        decimal previewPercent = (decimal)Math.Round(snap.PreviewOpacity * 100f);
        if (previewPercent < knudPreviewOpacity.Minimum)
        {
            previewPercent = knudPreviewOpacity.Minimum;
        }
        else if (previewPercent > knudPreviewOpacity.Maximum)
        {
            previewPercent = knudPreviewOpacity.Maximum;
        }

        knudPreviewOpacity.Value = previewPercent;
        kchkShowShadow.Checked = snap.ShowShadow;
        knudShadowOffset.Value = snap.ShadowOffset;
        knudShadowBlur.Value = snap.ShadowBlur;
        decimal shadowPercent = (decimal)Math.Round(snap.ShadowOpacity * 100f);
        if (shadowPercent < knudShadowOpacity.Minimum)
        {
            shadowPercent = knudShadowOpacity.Minimum;
        }
        else if (shadowPercent > knudShadowOpacity.Maximum)
        {
            shadowPercent = knudShadowOpacity.Maximum;
        }

        knudShadowOpacity.Value = shadowPercent;
        kcbtnShadow.SelectedColor = snap.ShadowColor.IsEmpty
            ? Color.Black
            : snap.ShadowColor;
        kcbtnShadow.SelectedColorChanged += OnShadowColorChanged;

        kcmbTheme.Items.Clear();
        foreach (PaletteMode theme in Themes)
        {
            kcmbTheme.Items.Add(theme);
        }

        kcmbTheme.SelectedItem = PaletteMode;
        UpdateBoundsReadout();
    }

    private void OnModeChanged(object? sender, EventArgs e)
    {
        if (kcmbMode.SelectedItem is KryptonSnapLayoutMode mode)
        {
            SnapLayouts = mode;
            UpdateBoundsReadout();
        }
    }

    private void OnGlobalChanged(object? sender, EventArgs e)
    {
        if (kcmbGlobal.SelectedItem is KryptonSnapLayoutMode mode)
        {
            KryptonManager.SnapLayoutValues.Mode = mode;
            UpdateBoundsReadout();
        }
    }

    private void OnShowIconChanged(object? sender, EventArgs e)
    {
        KryptonManager.SnapLayoutValues.ShowIcon = kchkShowIcon.Checked;
        UpdateBoundsReadout();
    }

    private void OnShowPreviewChanged(object? sender, EventArgs e)
    {
        KryptonManager.SnapLayoutValues.ShowPreview = kchkShowPreview.Checked;
        UpdateBoundsReadout();
    }

    private void OnPreviewOpacityChanged(object? sender, EventArgs e)
    {
        KryptonManager.SnapLayoutValues.PreviewOpacity = (float)knudPreviewOpacity.Value / 100f;
        UpdateBoundsReadout();
    }

    private void OnThemeChanged(object? sender, EventArgs e)
    {
        if (kcmbTheme.SelectedItem is PaletteMode theme)
        {
            PaletteMode = theme;
        }
    }

    private void OnShowShadowChanged(object? sender, EventArgs e)
    {
        KryptonManager.SnapLayoutValues.ShowShadow = kchkShowShadow.Checked;
        UpdateBoundsReadout();
    }

    private void OnShadowOffsetChanged(object? sender, EventArgs e)
    {
        KryptonManager.SnapLayoutValues.ShadowOffset = (int)knudShadowOffset.Value;
        UpdateBoundsReadout();
    }

    private void OnShadowBlurChanged(object? sender, EventArgs e)
    {
        KryptonManager.SnapLayoutValues.ShadowBlur = (int)knudShadowBlur.Value;
        UpdateBoundsReadout();
    }

    private void OnShadowOpacityChanged(object? sender, EventArgs e)
    {
        KryptonManager.SnapLayoutValues.ShadowOpacity = (float)knudShadowOpacity.Value / 100f;
        UpdateBoundsReadout();
    }

    private void OnShadowColorChanged(object? sender, ColorEventArgs e)
    {
        KryptonManager.SnapLayoutValues.ShadowColor = e.Color == Color.Black ? Color.Empty : e.Color;
        UpdateBoundsReadout();
    }

    private void OnIncludeBuiltinChanged(object? sender, EventArgs e)
    {
        KryptonManager.SnapLayoutValues.IncludeBuiltinLayouts = kchkIncludeBuiltin.Checked;
        UpdateBoundsReadout();
    }

    private void OnAddCustomClick(object? sender, EventArgs e)
    {
        KryptonManager.SnapLayoutValues.CustomLayouts.Add(new KryptonSnapLayoutCard(
            new KryptonSnapLayoutZone(0f, 0f, 0.7f, 1f),
            new KryptonSnapLayoutZone(0.7f, 0f, 0.3f, 1f)));
        UpdateBoundsReadout();
    }

    private void OnClearCustomClick(object? sender, EventArgs e)
    {
        KryptonManager.SnapLayoutValues.CustomLayouts.Clear();
        UpdateBoundsReadout();
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        UpdateBoundsReadout();
    }

    protected override void OnMove(EventArgs e)
    {
        base.OnMove(e);
        UpdateBoundsReadout();
    }

    private void UpdateBoundsReadout()
    {
        if (klblBounds == null || klblBounds.IsDisposed)
        {
            return;
        }

        SnapLayoutValues snap = KryptonManager.SnapLayoutValues;
        string builtin = snap.IncludeBuiltinLayouts ? "included" : "hidden";
        klblBounds.Values.Text =
            $"This form = {SnapLayouts}. All forms = {snap.Mode}. Icon {(snap.ShowIcon ? "shown" : "hidden")}. Preview {(snap.ShowPreview ? "on" : "off")} at {(int)Math.Round(snap.PreviewOpacity * 100f)}%. Shadow {(snap.ShowShadow ? "on" : "off")} at {(int)Math.Round(snap.ShadowOpacity * 100f)}%, offset {snap.ShadowOffset}, blur {snap.ShadowBlur}. Builtin layouts {builtin}. Custom cards {snap.CustomLayouts.Count}. Bounds {Bounds.X}, {Bounds.Y}  {Bounds.Width} x {Bounds.Height}. State {WindowState}.";
    }
}

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
/// Hover this form's maximize button. The form inherits KryptonManager.GlobalSnapLayouts.
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
        kcmbGlobal.SelectedItem = KryptonManager.SnapLayouts;
        kchkShowIcon.Checked = KryptonManager.SnapLayoutShowIcon;
        kchkShowPreview.Checked = KryptonManager.SnapLayoutShowPreview;
        decimal previewPercent = (decimal)Math.Round(KryptonManager.SnapLayoutPreviewOpacity * 100f);
        if (previewPercent < knudPreviewOpacity.Minimum)
        {
            previewPercent = knudPreviewOpacity.Minimum;
        }
        else if (previewPercent > knudPreviewOpacity.Maximum)
        {
            previewPercent = knudPreviewOpacity.Maximum;
        }

        knudPreviewOpacity.Value = previewPercent;

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
            KryptonManager.SnapLayouts = mode;
            UpdateBoundsReadout();
        }
    }

    private void OnShowIconChanged(object? sender, EventArgs e)
    {
        KryptonManager.SnapLayoutShowIcon = kchkShowIcon.Checked;
        UpdateBoundsReadout();
    }

    private void OnShowPreviewChanged(object? sender, EventArgs e)
    {
        KryptonManager.SnapLayoutShowPreview = kchkShowPreview.Checked;
        UpdateBoundsReadout();
    }

    private void OnPreviewOpacityChanged(object? sender, EventArgs e)
    {
        KryptonManager.SnapLayoutPreviewOpacity = (float)knudPreviewOpacity.Value / 100f;
        UpdateBoundsReadout();
    }

    private void OnThemeChanged(object? sender, EventArgs e)
    {
        if (kcmbTheme.SelectedItem is PaletteMode theme)
        {
            PaletteMode = theme;
        }
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

        klblBounds.Values.Text =
            $"This form = {SnapLayouts}. All forms = {KryptonManager.SnapLayouts}. Icon {(KryptonManager.SnapLayoutShowIcon ? "shown" : "hidden")}. Preview {(KryptonManager.SnapLayoutShowPreview ? "on" : "off")} at {(int)Math.Round(KryptonManager.SnapLayoutPreviewOpacity * 100f)}%. Bounds {Bounds.X}, {Bounds.Y}  {Bounds.Width} x {Bounds.Height}. State {WindowState}.";
    }
}

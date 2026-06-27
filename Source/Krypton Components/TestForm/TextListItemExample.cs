#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Comprehensive demonstration of <see cref="SchemeBaseColors.TextListItem"/> (Issue #880).
/// Tree views, list boxes, checked list boxes, and list views use TextListItem; labels use TextLabelControl;
/// standalone buttons use TextButtonNormal — each customizable independently via the active palette.
/// </summary>
public partial class TextListItemExample : KryptonForm
{
    private PaletteBase? _subscribedPalette;

    public TextListItemExample()
    {
        InitializeComponent();
        Load += OnLoad;
        FormClosed += OnFormClosed;
    }

    private void OnLoad(object? sender, EventArgs e)
    {
        kryptonManager1.GlobalPaletteMode = PaletteMode.Microsoft365Blue;
        PopulateListControls();
        WirePaletteSchemeColorChanged();
        RefreshSchemeReadout();
        UpdateDescription();
    }

    private void OnFormClosed(object? sender, FormClosedEventArgs e) => UnwirePaletteSchemeColorChanged();

    private void PopulateListControls()
    {
        kryptonTreeView1.Nodes.Clear();
        var root = kryptonTreeView1.Nodes.Add("Projects");
        root.Nodes.Add("Design");
        root.Nodes.Add("Implementation");
        var archive = root.Nodes.Add("Archive");
        archive.Nodes.Add("2024");
        archive.Nodes.Add("2025");
        kryptonTreeView1.ExpandAll();
        kryptonTreeView1.SelectedNode = root.Nodes[0];

        kryptonListBox1.Items.Clear();
        kryptonListBox1.Items.AddRange(["Inbox", "Drafts", "Sent", "Archive"]);
        kryptonListBox1.SelectedIndex = 0;

        kryptonCheckedListBox1.Items.Clear();
        kryptonCheckedListBox1.Items.Add("Notifications");
        kryptonCheckedListBox1.SetItemChecked(0, true);
        kryptonCheckedListBox1.Items.Add("Weekly summary");
        kryptonCheckedListBox1.Items.Add("Beta features");
        kryptonCheckedListBox1.SetItemChecked(2, true);

        kryptonListView1.Items.Clear();
        kryptonListView1.View = View.List;
        kryptonListView1.FullRowSelect = true;
        kryptonListView1.Items.Add("ListView row alpha");
        kryptonListView1.Items.Add("ListView row beta");
        kryptonListView1.Items.Add("ListView row gamma");
        kryptonListView1.Items[1].Selected = true;
        kryptonListView1.Select();

        kryptonListBoxDisabled.Items.Clear();
        kryptonListBoxDisabled.Items.AddRange(["Disabled list (control off)", "Still uses theme disabled text"]);
    }

    private void kryptonThemeComboBox1_SelectedIndexChanged(object? sender, EventArgs e)
    {
        WirePaletteSchemeColorChanged();
        RefreshSchemeReadout();
        UpdateDescription();
    }

    private void UpdateDescription()
    {
        var theme = kryptonThemeComboBox1.SelectedItem?.ToString() ?? "Unknown";
        klblDescription.Values.Text =
            $"Theme: {theme}. Issue #880 adds SchemeBaseColors.TextListItem so list and tree item text " +
            "no longer has to share TextLabelControl with KryptonLabel. Use the pickers below or " +
            "\"Contrast demo\" to verify each scheme slot updates only its target controls. " +
            "Professional themes use system colors and ignore scheme text slots.";
    }

    private void WirePaletteSchemeColorChanged()
    {
        UnwirePaletteSchemeColorChanged();
        _subscribedPalette = KryptonManager.CurrentGlobalPalette;
        if (_subscribedPalette is not null)
        {
            _subscribedPalette.SchemeColorChanged += OnSchemeColorChanged;
        }
    }

    private void UnwirePaletteSchemeColorChanged()
    {
        if (_subscribedPalette is not null)
        {
            _subscribedPalette.SchemeColorChanged -= OnSchemeColorChanged;
            _subscribedPalette = null;
        }
    }

    private void OnSchemeColorChanged(object? sender, SchemeColorChangedEventArgs e) => RefreshSchemeReadout();

    private void RefreshSchemeReadout()
    {
        klblSchemeReadout.Values.Text =
            $"TextLabelControl = {FormatSchemeColor(SchemeBaseColors.TextLabelControl)}  |  " +
            $"TextListItem = {FormatSchemeColor(SchemeBaseColors.TextListItem)}  |  " +
            $"TextButtonNormal = {FormatSchemeColor(SchemeBaseColors.TextButtonNormal)}";
    }

    private static string FormatSchemeColor(SchemeBaseColors role)
    {
        var palette = KryptonManager.CurrentGlobalPalette;
        if (palette is null)
        {
            return "(no palette)";
        }

        var color = palette.GetSchemeColor(role);
        if (color.IsEmpty || color == GlobalStaticVariables.EMPTY_COLOR)
        {
            return "(theme default)";
        }

        return $"{color.Name} (#{color.R:X2}{color.G:X2}{color.B:X2})";
    }

    private void kcbtnLabelColor_SelectedColorChanged(object? sender, ColorEventArgs e) =>
        ApplySchemeColor(SchemeBaseColors.TextLabelControl, e.Color);

    private void kcbtnListItemColor_SelectedColorChanged(object? sender, ColorEventArgs e) =>
        ApplySchemeColor(SchemeBaseColors.TextListItem, e.Color);

    private void kcbtnButtonColor_SelectedColorChanged(object? sender, ColorEventArgs e) =>
        ApplySchemeColor(SchemeBaseColors.TextButtonNormal, e.Color);

    private void kbtnApplyLabelColor_Click(object? sender, EventArgs e) =>
        ApplySchemeColor(SchemeBaseColors.TextLabelControl, kcbtnLabelColor.SelectedColor);

    private void kbtnApplyListItemColor_Click(object? sender, EventArgs e) =>
        ApplySchemeColor(SchemeBaseColors.TextListItem, kcbtnListItemColor.SelectedColor);

    private void kbtnApplyButtonColor_Click(object? sender, EventArgs e) =>
        ApplySchemeColor(SchemeBaseColors.TextButtonNormal, kcbtnButtonColor.SelectedColor);

    private void kbtnContrastDemo_Click(object? sender, EventArgs e)
    {
        ApplySchemeColor(SchemeBaseColors.TextLabelControl, Color.Firebrick);
        ApplySchemeColor(SchemeBaseColors.TextListItem, Color.MediumBlue);
        ApplySchemeColor(SchemeBaseColors.TextButtonNormal, Color.DarkGreen);
        kcbtnLabelColor.SelectedColor = Color.Firebrick;
        kcbtnListItemColor.SelectedColor = Color.MediumBlue;
        kcbtnButtonColor.SelectedColor = Color.DarkGreen;
        klblStatus.Values.Text =
            "Contrast demo applied: labels = firebrick, list/tree = medium blue, button = dark green.";
    }

    private void kbtnResetAll_Click(object? sender, EventArgs e)
    {
        kcbtnLabelColor.SelectedColor = Color.Empty;
        kcbtnListItemColor.SelectedColor = Color.Empty;
        kcbtnButtonColor.SelectedColor = Color.Empty;
        ApplySchemeColor(SchemeBaseColors.TextLabelControl, Color.Empty);
        ApplySchemeColor(SchemeBaseColors.TextListItem, Color.Empty);
        ApplySchemeColor(SchemeBaseColors.TextButtonNormal, Color.Empty);
        klblStatus.Values.Text = "Scheme overrides cleared. Re-select the theme to restore built-in defaults.";
        RefreshSchemeReadout();
    }

    private void ApplySchemeColor(SchemeBaseColors role, Color color)
    {
        var palette = KryptonManager.CurrentGlobalPalette;
        if (palette is null)
        {
            return;
        }

        if (color.IsEmpty || color.A == 0)
        {
            palette.SetSchemeColor(role, GlobalStaticVariables.EMPTY_COLOR);
            klblStatus.Values.Text = $"{role} reset. Re-select the theme for the built-in default.";
        }
        else
        {
            palette.SetSchemeColor(role, color);
            klblStatus.Values.Text = $"{role} set to {color.Name} (#{color.R:X2}{color.G:X2}{color.B:X2}).";
        }

        RefreshSchemeReadout();
    }
}

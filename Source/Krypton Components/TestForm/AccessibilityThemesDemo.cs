#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Demo for Issue #4168: High Contrast, Deuteranopia, and Protanopia PaletteMode themes.
/// </summary>
public sealed class AccessibilityThemesDemo : KryptonForm
{
    private readonly KryptonManager _manager = new KryptonManager();
    private readonly KryptonComboBox _cmbTheme;
    private readonly KryptonLabel _lblStatus;
    private readonly KryptonLabel _lblAccentGuide;
    private PaletteMode _previousMode;

    public AccessibilityThemesDemo()
    {
        Text = @"4168 — Accessibility Themes";
        Size = new Size(820, 620);
        StartPosition = FormStartPosition.CenterScreen;

        _previousMode = KryptonManager.CurrentGlobalPaletteMode;

        _lblStatus = new KryptonLabel { Text = @"Ready", AutoSize = true, Padding = new Padding(12, 6, 0, 0) };
        _lblAccentGuide = new KryptonLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 48,
            Padding = new Padding(4)
        };

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 100,
            Padding = new Padding(12),
            Text =
                "Issue #4168: full PaletteMode themes for High Contrast, Deuteranopia, and Protanopia.\r\n" +
                "Unprefixed names use Microsoft 365 chrome; also try Office 2007 / 2010 / 2013 and Sparkle variants (same colours, family-specific renderer).\r\n" +
                "Look for: Primary header, checked button (orange / brown / green), link accent, AcceptButton default fill.\r\n" +
                "Dialog button colours remain independent — use the MessageBox button to compare #4165 presets."
        };

        var toolbar = new KryptonPanel { Dock = DockStyle.Top, Height = 48, Padding = new Padding(12, 8, 12, 8) };
        var toolbarFlow = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false };

        toolbarFlow.Controls.Add(new KryptonLabel { Text = @"Theme:", AutoSize = true, Padding = new Padding(0, 6, 0, 0) });
        _cmbTheme = new KryptonComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 180 };
        _cmbTheme.Items.AddRange(new object[]
        {
            PaletteMode.HighContrast,
            PaletteMode.Deuteranopia,
            PaletteMode.Protanopia,
            PaletteMode.Office2007HighContrast,
            PaletteMode.Office2007Deuteranopia,
            PaletteMode.Office2007Protanopia,
            PaletteMode.Office2010HighContrast,
            PaletteMode.Office2010Deuteranopia,
            PaletteMode.Office2010Protanopia,
            PaletteMode.Office2013HighContrast,
            PaletteMode.Office2013Deuteranopia,
            PaletteMode.Office2013Protanopia,
            PaletteMode.SparkleHighContrast,
            PaletteMode.SparkleDeuteranopia,
            PaletteMode.SparkleProtanopia
        });
        _cmbTheme.SelectedIndexChanged += (_, _) => ApplySelectedTheme();
        toolbarFlow.Controls.Add(_cmbTheme);

        var btnRestore = new KryptonButton { Text = @"Restore previous theme", AutoSize = true, Padding = new Padding(8, 0, 0, 0) };
        btnRestore.Click += (_, _) =>
        {
            ThemeManager.ApplyTheme(_previousMode, _manager);
            _lblStatus.Text = $@"Restored {_previousMode}";
            UpdateAccentGuide(_previousMode);
        };
        toolbarFlow.Controls.Add(btnRestore);

        toolbar.Controls.Add(toolbarFlow);

        var content = new KryptonPanel { Dock = DockStyle.Fill, Padding = new Padding(12) };
        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 5
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));

        var primaryHeader = new KryptonHeader
        {
            Dock = DockStyle.Fill,
            HeaderStyle = HeaderStyle.Primary,
            Values = { Heading = @"Primary header (theme primary)" }
        };
        layout.Controls.Add(primaryHeader, 0, 0);
        layout.SetColumnSpan(primaryHeader, 2);

        var secondaryHeader = new KryptonHeader
        {
            Dock = DockStyle.Fill,
            HeaderStyle = HeaderStyle.Secondary,
            Values = { Heading = @"Secondary header (theme secondary / warning)" }
        };
        layout.Controls.Add(secondaryHeader, 0, 1);
        layout.SetColumnSpan(secondaryHeader, 2);

        var buttonRow = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = true };
        buttonRow.Controls.Add(new KryptonButton { Text = @"Normal button", AutoSize = true });
        var defaultBtn = new KryptonButton { Text = @"Default / Accept", AutoSize = true };
        buttonRow.Controls.Add(defaultBtn);
        AcceptButton = defaultBtn;
        buttonRow.Controls.Add(new KryptonCheckButton { Text = @"Checked (secondary)", AutoSize = true, Checked = true });
        buttonRow.Controls.Add(new KryptonCheckBox { Text = @"Check box", Checked = true });
        buttonRow.Controls.Add(new KryptonRadioButton { Text = @"Radio A", Checked = true });
        buttonRow.Controls.Add(new KryptonRadioButton { Text = @"Radio B" });
        layout.Controls.Add(buttonRow, 0, 2);
        layout.SetColumnSpan(buttonRow, 2);

        var left = new KryptonPanel { Dock = DockStyle.Fill, Padding = new Padding(4) };
        left.Controls.Add(new KryptonTextBox { Dock = DockStyle.Top, Text = @"Input sample text" });
        left.Controls.Add(new KryptonLinkLabel
        {
            Dock = DockStyle.Top,
            Text = @"Sample link (accent colour)",
            Padding = new Padding(0, 8, 0, 0)
        });
        left.Controls.Add(new KryptonLabel
        {
            Dock = DockStyle.Top,
            Text = @"Body text on themed panel",
            Padding = new Padding(0, 8, 0, 0)
        });
        layout.Controls.Add(left, 0, 3);

        var right = new KryptonGroupBox { Dock = DockStyle.Fill, Values = { Heading = @"What to verify" } };
        right.Panel.Controls.Add(_lblAccentGuide);
        right.Panel.Controls.Add(new KryptonWrapLabel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(4),
            Text = @"Primary header / Accept use the primary accent. Checked button uses secondary. Link uses the third accent. " +
                   @"Surfaces should no longer look like stock Microsoft 365 Blue."
        });
        layout.Controls.Add(right, 1, 3);

        var bottom = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = true };
        var btnMsg = new KryptonButton { Text = @"MessageBox with matching dialog button scheme", AutoSize = true };
        btnMsg.Click += (_, _) => ShowMatchingMessageBox();
        bottom.Controls.Add(btnMsg);
        bottom.Controls.Add(_lblStatus);
        layout.Controls.Add(bottom, 0, 4);
        layout.SetColumnSpan(bottom, 2);

        content.Controls.Add(layout);

        Controls.Add(content);
        Controls.Add(toolbar);
        Controls.Add(instructions);

        FormClosed += (_, _) => ThemeManager.ApplyTheme(_previousMode, _manager);

        _cmbTheme.SelectedItem = PaletteMode.HighContrast;
    }

    private void ApplySelectedTheme()
    {
        if (!(_cmbTheme.SelectedItem is PaletteMode mode))
        {
            return;
        }

        ThemeManager.ApplyTheme(mode, _manager);
        _lblStatus.Text = $@"Applied {mode} (KryptonManager.CurrentGlobalPaletteMode = {KryptonManager.CurrentGlobalPaletteMode})";
        UpdateAccentGuide(mode);
    }

    private void UpdateAccentGuide(PaletteMode mode)
    {
        _lblAccentGuide.Text = mode switch
        {
            PaletteMode.HighContrast or PaletteMode.Office2007HighContrast or PaletteMode.Office2010HighContrast or PaletteMode.Office2013HighContrast or PaletteMode.SparkleHighContrast =>
                @"High Contrast: black/white surfaces · primary neon green · secondary yellow · accent cyan",
            PaletteMode.Deuteranopia or PaletteMode.Office2007Deuteranopia or PaletteMode.Office2010Deuteranopia or PaletteMode.Office2013Deuteranopia or PaletteMode.SparkleDeuteranopia =>
                @"Deuteranopia: cool neutrals · primary blue · secondary orange (checked) · accent purple (link)",
            PaletteMode.Protanopia or PaletteMode.Office2007Protanopia or PaletteMode.Office2010Protanopia or PaletteMode.Office2013Protanopia or PaletteMode.SparkleProtanopia =>
                @"Protanopia: warm neutrals · primary blue · secondary brown (checked) · accent magenta (link)",
            _ => @"Select an accessibility theme."
        };
    }

    private void ShowMatchingMessageBox()
    {
        if (!(_cmbTheme.SelectedItem is PaletteMode mode))
        {
            return;
        }

        var scheme = mode switch
        {
            PaletteMode.HighContrast or PaletteMode.Office2007HighContrast or PaletteMode.Office2010HighContrast or PaletteMode.Office2013HighContrast or PaletteMode.SparkleHighContrast =>
                KryptonDialogButtonColorScheme.HighContrast,
            PaletteMode.Deuteranopia or PaletteMode.Office2007Deuteranopia or PaletteMode.Office2010Deuteranopia or PaletteMode.Office2013Deuteranopia or PaletteMode.SparkleDeuteranopia =>
                KryptonDialogButtonColorScheme.Deuteranopia,
            PaletteMode.Protanopia or PaletteMode.Office2007Protanopia or PaletteMode.Office2010Protanopia or PaletteMode.Office2013Protanopia or PaletteMode.SparkleProtanopia =>
                KryptonDialogButtonColorScheme.Protanopia,
            _ => KryptonDialogButtonColorScheme.None
        };

        var options = new KryptonDialogButtonColorOptions { Scheme = scheme };
        KryptonMessageBox.Show(this,
            @"Theme chrome and dialog button colours are independent. This MessageBox applies the matching #4165 scheme for comparison only.",
            @"4168 Accessibility Themes",
            KryptonMessageBoxButtons.YesNoCancel,
            KryptonMessageBoxIcon.Question,
            options);
    }
}

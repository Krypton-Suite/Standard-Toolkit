#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Ribbon;

namespace TestForm;

/// <summary>
/// Demo for Issue #942: grey chrome palettes across Office 2007/2010/2013, Microsoft 365, and Material.
/// Grey variants use grey window chrome with a light document surface.
/// </summary>
public sealed class Office2013GrayThemesDemo : KryptonForm
{
    private readonly KryptonManager _manager = new KryptonManager();
    private readonly KryptonComboBox _cmbTheme;
    private readonly KryptonLabel _lblStatus;
    private readonly KryptonRibbon _ribbon;
    private PaletteMode _previousMode;

    public Office2013GrayThemesDemo()
    {
        Text = @"942 — Grey Themes (2007 / 2010 / 2013 / 365 / Material)";
        Size = new Size(900, 640);
        StartPosition = FormStartPosition.CenterScreen;

        _previousMode = KryptonManager.CurrentGlobalPaletteMode;

        _ribbon = new KryptonRibbon { Dock = DockStyle.Top };
        ((ISupportInitialize)_ribbon).BeginInit();
        var home = new KryptonRibbonTab { Text = @"Home" };
        var insert = new KryptonRibbonTab { Text = @"Insert" };
        var view = new KryptonRibbonTab { Text = @"View" };
        var group = new KryptonRibbonGroup { TextLine1 = @"Clipboard" };
        var triple = new KryptonRibbonGroupTriple();
        triple.Items!.Add(new KryptonRibbonGroupButton { TextLine1 = @"Paste" });
        triple.Items.Add(new KryptonRibbonGroupButton { TextLine1 = @"Cut" });
        triple.Items.Add(new KryptonRibbonGroupButton { TextLine1 = @"Copy" });
        group.Items.Add(triple);
        home.Groups.Add(group);
        _ribbon.RibbonTabs.Add(home);
        _ribbon.RibbonTabs.Add(insert);
        _ribbon.RibbonTabs.Add(view);
        _ribbon.SelectedTab = home;
        ((ISupportInitialize)_ribbon).EndInit();

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 88,
            Padding = new Padding(12),
            Text =
                "Issue #942: Office 2013 Light Grey and Dark Grey chrome (White is the same family for comparison).\r\n" +
                "Grey themes change the title bar, ribbon tab row, and status strip. The client/document area stays light.\r\n" +
                "Dark Grey is not a full dark mode: caption text is white on dark chrome; buttons and text boxes stay light."
        };

        var toolbar = new KryptonPanel { Dock = DockStyle.Top, Height = 48, Padding = new Padding(12, 8, 12, 8) };
        var toolbarFlow = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false };
        toolbarFlow.Controls.Add(new KryptonLabel { Text = @"Theme:", AutoSize = true, Padding = new Padding(0, 6, 0, 0) });
        _cmbTheme = new KryptonComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 280 };
        _cmbTheme.Items.AddRange(new object[]
        {
            PaletteMode.Office2013White,
            PaletteMode.Office2013LightGray,
            PaletteMode.Office2013DarkGray,
            PaletteMode.Office2007LightGray,
            PaletteMode.Office2007DarkGray,
            PaletteMode.Office2010LightGray,
            PaletteMode.Office2010DarkGray,
            PaletteMode.Microsoft365LightGray,
            PaletteMode.Microsoft365DarkGray,
            PaletteMode.MaterialLightGray,
            PaletteMode.MaterialDarkGray,
            PaletteMode.MaterialLightGrayRipple,
            PaletteMode.MaterialDarkGrayRipple
        });
        _cmbTheme.SelectedIndexChanged += (_, _) => ApplySelectedTheme();
        toolbarFlow.Controls.Add(_cmbTheme);

        _lblStatus = new KryptonLabel { Text = @"Ready", AutoSize = true, Dock = DockStyle.Fill, Padding = new Padding(4, 8, 0, 0) };

        var btnRestore = new KryptonButton { Text = @"Restore previous theme", AutoSize = true, Padding = new Padding(8, 0, 0, 0) };
        btnRestore.Click += (_, _) =>
        {
            ThemeManager.ApplyTheme(_previousMode, _manager);
            _lblStatus.Text = $@"Restored {_previousMode}";
        };
        toolbarFlow.Controls.Add(btnRestore);
        toolbar.Controls.Add(toolbarFlow);

        var content = new KryptonPanel { Dock = DockStyle.Fill, Padding = new Padding(12) };
        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 4
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));

        var primaryHeader = new KryptonHeader
        {
            Dock = DockStyle.Fill,
            HeaderStyle = HeaderStyle.Primary,
            Values = { Heading = @"Primary header (follows chrome)" }
        };
        layout.Controls.Add(primaryHeader, 0, 0);
        layout.SetColumnSpan(primaryHeader, 2);

        var secondaryHeader = new KryptonHeader
        {
            Dock = DockStyle.Fill,
            HeaderStyle = HeaderStyle.Secondary,
            Values = { Heading = @"Secondary header" }
        };
        layout.Controls.Add(secondaryHeader, 0, 1);
        layout.SetColumnSpan(secondaryHeader, 2);

        var left = new KryptonPanel { Dock = DockStyle.Fill, Padding = new Padding(4) };
        var leftFlow = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false };
        leftFlow.Controls.Add(new KryptonLabel { Text = @"Document / client area (stays light)", AutoSize = true });
        leftFlow.Controls.Add(new KryptonTextBox { Width = 280, Text = @"Sample input" });
        leftFlow.Controls.Add(new KryptonButton { Text = @"Normal button", AutoSize = true });
        leftFlow.Controls.Add(new KryptonCheckBox { Text = @"Check box", Checked = true });
        leftFlow.Controls.Add(new KryptonLinkLabel { Text = @"Sample link" });
        left.Controls.Add(leftFlow);
        layout.Controls.Add(left, 0, 2);

        var right = new KryptonGroupBox { Dock = DockStyle.Fill, Values = { Heading = @"What to verify" } };
        right.Panel.Controls.Add(new KryptonWrapLabel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(8),
            Text =
                "White: white chrome, dark caption text, blue status (family default).\r\n\r\n" +
                "Light Grey: medium-grey title bar and ribbon tab row, dark caption text, light client.\r\n\r\n" +
                "Dark Grey: dark title bar and tab row, white caption text, grey ribbon groups, light client. Control-box glyphs should remain visible."
        });
        layout.Controls.Add(right, 1, 2);

        layout.Controls.Add(_lblStatus, 0, 3);
        layout.SetColumnSpan(_lblStatus, 2);

        content.Controls.Add(layout);

        var status = new StatusStrip();
        status.Items.Add(new ToolStripStatusLabel { Text = @"Status strip follows chrome (grey themes are not blue like White)." });

        Controls.Add(content);
        Controls.Add(toolbar);
        Controls.Add(instructions);
        Controls.Add(_ribbon);
        Controls.Add(status);

        FormClosed += (_, _) => ThemeManager.ApplyTheme(_previousMode, _manager);

        _cmbTheme.SelectedItem = PaletteMode.Office2013LightGray;
    }

    private void ApplySelectedTheme()
    {
        if (!(_cmbTheme.SelectedItem is PaletteMode mode))
        {
            return;
        }

        ThemeManager.ApplyTheme(mode, _manager);
        _lblStatus.Text = $@"Applied {mode}";
    }
}

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Ribbon;

namespace TestForm;

/// <summary>
/// Issue #4496: Office 2024 underline tabs, flat groups, and a text File tab.
/// Microsoft 365 Blue is included so the previous folder-tab chrome can be compared.
/// </summary>
public partial class Office2024RibbonDemo : KryptonForm
{
    private readonly KryptonManager _manager = new();
    private readonly KryptonRibbon _ribbon;
    private readonly KryptonWrapLabel _status;

    public Office2024RibbonDemo()
    {
        InitializeComponent();

        _ribbon = new KryptonRibbon
        {
            Dock = DockStyle.Top,
            Name = @"office2024Ribbon"
        };
        _ribbon.RibbonFileAppTab.FileAppTabText = @"File";

        var home = new KryptonRibbonTab { Text = @"Home", KeyTip = @"H" };
        var insert = new KryptonRibbonTab { Text = @"Insert", KeyTip = @"N" };
        var table = new KryptonRibbonTab
        {
            Text = @"Table Design",
            KeyTip = @"JT",
            ContextName = @"Table"
        };

        home.Groups.Add(CreateClipboardGroup());
        home.Groups.Add(CreateFontGroup());
        insert.Groups.Add(CreateInsertGroup());
        table.Groups.Add(CreateTableGroup());

        _ribbon.RibbonTabs.Add(home);
        _ribbon.RibbonTabs.Add(insert);
        _ribbon.RibbonTabs.Add(table);
        _ribbon.RibbonContexts.Add(new KryptonRibbonContext
        {
            ContextName = @"Table",
            ContextTitle = @"Table Tools",
            ContextColor = Color.FromArgb(15, 108, 189)
        });
        _ribbon.SelectedContext = @"Table";

        _ribbon.QATButtons.Add(new KryptonRibbonQATButton { Text = @"Undo", ToolTipTitle = @"Undo" });
        _ribbon.RibbonFileAppButton.AppButtonMenuItems.Add(new KryptonContextMenuItem(@"New"));
        _ribbon.RibbonFileAppButton.AppButtonMenuItems.Add(new KryptonContextMenuItem(@"Open"));

        Controls.Add(_ribbon);

        var panel = new KryptonPanel { Dock = DockStyle.Fill };
        Controls.Add(panel);
        panel.BringToFront();

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            Height = 88,
            Text =
                @"Issue #4496: Office 2024 ribbon. The selected tab is an underline, groups have no boxes, and File is text." +
                Environment.NewLine +
                @"Switch Blue, Silver, White, grays, and Black, including dark and light modes. Microsoft 365 Blue keeps the folder-tab chrome." +
                Environment.NewLine +
                @"Home has two groups and a dialog launcher. Table Design is a contextual tab. File opens the application menu."
        };

        var buttons = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 132,
            Padding = new Padding(8, 8, 8, 0)
        };
        buttons.Controls.Add(CreateThemeButton(@"Office 2024 Blue", PaletteMode.Office2024Blue));
        buttons.Controls.Add(CreateThemeButton(@"Blue Dark", PaletteMode.Office2024BlueDarkMode));
        buttons.Controls.Add(CreateThemeButton(@"Blue Light", PaletteMode.Office2024BlueLightMode));
        buttons.Controls.Add(CreateThemeButton(@"Office 2024 Silver", PaletteMode.Office2024Silver));
        buttons.Controls.Add(CreateThemeButton(@"Silver Dark", PaletteMode.Office2024SilverDarkMode));
        buttons.Controls.Add(CreateThemeButton(@"Silver Light", PaletteMode.Office2024SilverLightMode));
        buttons.Controls.Add(CreateThemeButton(@"Office 2024 White", PaletteMode.Office2024White));
        buttons.Controls.Add(CreateThemeButton(@"Office 2024 Light Gray", PaletteMode.Office2024LightGray));
        buttons.Controls.Add(CreateThemeButton(@"Office 2024 Dark Gray", PaletteMode.Office2024DarkGray));
        buttons.Controls.Add(CreateThemeButton(@"Office 2024 Black", PaletteMode.Office2024Black));
        buttons.Controls.Add(CreateThemeButton(@"Black Dark", PaletteMode.Office2024BlackDarkMode));
        buttons.Controls.Add(CreateThemeButton(@"Black Dark Alternate", PaletteMode.Office2024BlackDarkModeAlternate));
        buttons.Controls.Add(CreateThemeButton(@"Microsoft 365 Blue", PaletteMode.Microsoft365Blue));

        _status = new KryptonWrapLabel
        {
            Dock = DockStyle.Fill,
            Text = @"Select a theme."
        };

        panel.Controls.Add(_status);
        panel.Controls.Add(buttons);
        panel.Controls.Add(instructions);

        ApplyTheme(PaletteMode.Office2024Black);
    }

    /// <summary>
    /// Applies a builtin palette to the form and ribbon and reports the ribbon shape.
    /// </summary>
    /// <param name="mode">Palette mode to apply.</param>
    public void ApplyTheme(PaletteMode mode)
    {
        _manager.GlobalPaletteMode = mode;
        PaletteMode = mode;
        _ribbon.PaletteMode = mode;
        _status.Text = $@"Palette: {mode}. Ribbon shape: {_ribbon.StateCommon.RibbonGeneral.GetRibbonShape()}.";
    }

    private KryptonButton CreateThemeButton(string text, PaletteMode mode)
    {
        var button = new KryptonButton
        {
            Text = text,
            AutoSize = true,
            MinimumSize = new Size(140, 28),
            Tag = mode
        };
        button.Click += OnThemeClick;
        return button;
    }

    private void OnThemeClick(object? sender, EventArgs e)
    {
        if (sender is KryptonButton button && button.Tag is PaletteMode mode)
        {
            ApplyTheme(mode);
        }
    }

    private static KryptonRibbonGroup CreateClipboardGroup()
    {
        var paste = new KryptonRibbonGroupButton
        {
            TextLine1 = @"Paste",
            KeyTip = @"V",
            ItemSizeMaximum = GroupItemSize.Large,
            ItemSizeMinimum = GroupItemSize.Large
        };
        var cut = new KryptonRibbonGroupButton { TextLine1 = @"Cut", KeyTip = @"X" };
        var copy = new KryptonRibbonGroupButton { TextLine1 = @"Copy", KeyTip = @"C" };

        var lines = new KryptonRibbonGroupTriple();
        lines.Items!.Add(paste);
        lines.Items.Add(cut);
        lines.Items.Add(copy);

        var group = new KryptonRibbonGroup
        {
            TextLine1 = @"Clipboard",
            DialogBoxLauncher = true
        };
        group.Items.Add(lines);
        return group;
    }

    private static KryptonRibbonGroup CreateFontGroup()
    {
        var bold = new KryptonRibbonGroupButton { TextLine1 = @"Bold", KeyTip = @"1" };
        var italic = new KryptonRibbonGroupButton { TextLine1 = @"Italic", KeyTip = @"2" };
        var underline = new KryptonRibbonGroupButton { TextLine1 = @"Underline", KeyTip = @"3" };
        var lines = new KryptonRibbonGroupTriple();
        lines.Items!.Add(bold);
        lines.Items.Add(italic);
        lines.Items.Add(underline);

        var group = new KryptonRibbonGroup { TextLine1 = @"Font" };
        group.Items.Add(lines);
        return group;
    }

    private static KryptonRibbonGroup CreateInsertGroup()
    {
        var table = new KryptonRibbonGroupButton
        {
            TextLine1 = @"Table",
            KeyTip = @"T",
            ItemSizeMaximum = GroupItemSize.Large,
            ItemSizeMinimum = GroupItemSize.Large
        };
        var lines = new KryptonRibbonGroupTriple();
        lines.Items!.Add(table);
        var group = new KryptonRibbonGroup { TextLine1 = @"Tables" };
        group.Items.Add(lines);
        return group;
    }

    private static KryptonRibbonGroup CreateTableGroup()
    {
        var shading = new KryptonRibbonGroupButton { TextLine1 = @"Shading", KeyTip = @"SH" };
        var lines = new KryptonRibbonGroupTriple();
        lines.Items!.Add(shading);
        var group = new KryptonRibbonGroup { TextLine1 = @"Table Styles" };
        group.Items.Add(lines);
        return group;
    }
}

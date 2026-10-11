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
    private readonly KryptonCheckBox _bevel;
    private readonly KryptonCheckBox _contextTitles;
    private readonly KryptonCheckBox _pills;
    private readonly KryptonCheckBox _glow;
    private readonly KryptonCheckBox _compact;
    private readonly KryptonComboBox _arrowLocation;
    private readonly KryptonNumericUpDown _bevelSize;
    private readonly KryptonNumericUpDown _gap;
    private bool _syncingBevel;
    private bool _syncingContextTitles;
    private bool _syncingPills;
    private bool _syncingGlow;
    private bool _syncingCompact;
    private bool _syncingArrowLocation;
    private bool _syncingBevelSize;
    private bool _syncingGap;

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
        _ribbon.ShowCompactButton = true;
        CreateOffice2024Backstage();

        Controls.Add(_ribbon);

        var panel = new KryptonPanel { Dock = DockStyle.Fill };
        Controls.Add(panel);
        panel.BringToFront();

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            Height = 188,
            Text =
                @"Issue #4496: Office 2024 ribbon. The selected tab is an underline, groups have no boxes, and File is text." +
                Environment.NewLine +
                @"Switch Blue, Silver, White, grays, and Black, including dark and light modes. Microsoft 365 Blue keeps the folder-tab chrome." +
                Environment.NewLine +
                @"Home has two groups and a dialog launcher. Table Design is a contextual tab. File opens the backstage." +
                Environment.NewLine +
                @"Compact ribbon (#4533) lays those same groups on one row for every ribbon shape: icon and text, then icons only as the form narrows. Group captions and group boxes hide. Each shape keeps its own tabs. Arrow place chooses under the groups, the end of the groups, or the tab row. Switching eases the groups area. The arrow is optional." +
                Environment.NewLine +
                @"Beveled edges and gaps between groups are optional. Bevel size and group gap are pixels at 96 DPI. A gap of zero keeps one card." +
                Environment.NewLine +
                @"Context titles in the title bar are optional for Office 2024 and stay off until you turn them on. Microsoft 365 still shows them." +
                Environment.NewLine +
                @"Tab marks can be a straight line or a pill-shaped line under the label. An optional glow can be turned on."
        };

        var buttons = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 408,
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
        buttons.Controls.Add(CreateThemeButton(@"Office 2007 Blue", PaletteMode.Office2007Blue));
        buttons.Controls.Add(CreateThemeButton(@"Office 2010 Blue", PaletteMode.Office2010Blue));
        buttons.Controls.Add(CreateThemeButton(@"Office 2013 White", PaletteMode.Office2013White));
        buttons.Controls.Add(CreateThemeButton(@"Sparkle Blue", PaletteMode.SparkleBlue));
        buttons.Controls.Add(CreateThemeButton(@"Visual Studio 2010", PaletteMode.VisualStudio2010Render2010));
        _bevel = new KryptonCheckBox
        {
            Text = @"Beveled group edges",
            AutoSize = true,
            Margin = new Padding(8, 6, 8, 0)
        };
        _bevel.CheckedChanged += (_, _) =>
        {
            if (!_syncingBevel)
            {
                SetGroupAreaBevel(_bevel.Checked);
            }
        };
        buttons.Controls.Add(_bevel);
        _contextTitles = new KryptonCheckBox
        {
            Text = @"Context titles in title bar",
            AutoSize = true,
            Margin = new Padding(8, 6, 8, 0)
        };
        _contextTitles.CheckedChanged += (_, _) =>
        {
            if (!_syncingContextTitles)
            {
                SetShowContextTitles(_contextTitles.Checked);
            }
        };
        buttons.Controls.Add(_contextTitles);
        _pills = new KryptonCheckBox
        {
            Text = @"Pill shaped lines",
            AutoSize = true,
            Margin = new Padding(8, 6, 8, 0)
        };
        _pills.CheckedChanged += (_, _) =>
        {
            if (!_syncingPills)
            {
                SetTabMarker(_pills.Checked ? PaletteRibbonTabMarker.Pill : PaletteRibbonTabMarker.Line);
            }
        };
        buttons.Controls.Add(_pills);
        _glow = new KryptonCheckBox
        {
            Text = @"Glow tab lines",
            AutoSize = true,
            Margin = new Padding(8, 6, 8, 0)
        };
        _glow.CheckedChanged += (_, _) =>
        {
            if (!_syncingGlow)
            {
                SetTabMarkerGlow(_glow.Checked);
            }
        };
        buttons.Controls.Add(_glow);
        _compact = new KryptonCheckBox
        {
            Text = @"Compact ribbon",
            AutoSize = true,
            Margin = new Padding(8, 6, 8, 0)
        };
        _compact.CheckedChanged += (_, _) =>
        {
            if (!_syncingCompact)
            {
                SetCompactMode(_compact.Checked);
            }
        };
        buttons.Controls.Add(_compact);
        var arrowLabel = new KryptonLabel
        {
            Text = @"Arrow place",
            AutoSize = true,
            Margin = new Padding(8, 8, 0, 0)
        };
        _arrowLocation = new KryptonComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            DropDownWidth = 140,
            Width = 140,
            Margin = new Padding(4, 4, 8, 0)
        };
        _arrowLocation.Items.AddRange(new object[] { @"Below groups", @"End of groups", @"Tab row" });
        _arrowLocation.SelectedIndex = 0;
        _arrowLocation.SelectedIndexChanged += (_, _) =>
        {
            if (!_syncingArrowLocation && _arrowLocation.SelectedIndex >= 0)
            {
                SetCompactButtonLocation(LocationFromIndex(_arrowLocation.SelectedIndex));
            }
        };
        buttons.Controls.Add(arrowLabel);
        buttons.Controls.Add(_arrowLocation);
        var bevelSizeLabel = new KryptonLabel
        {
            Text = @"Bevel size",
            AutoSize = true,
            Margin = new Padding(8, 8, 0, 0)
        };
        _bevelSize = new KryptonNumericUpDown
        {
            Minimum = 1,
            Maximum = 16,
            Value = PaletteRibbonGeneral.GroupAreaBevelSizeDefault,
            Width = 64,
            Margin = new Padding(4, 4, 8, 0)
        };
        _bevelSize.ValueChanged += (_, _) =>
        {
            if (!_syncingBevelSize)
            {
                SetGroupAreaBevelSize((int)_bevelSize.Value);
            }
        };
        buttons.Controls.Add(bevelSizeLabel);
        buttons.Controls.Add(_bevelSize);
        var gapLabel = new KryptonLabel
        {
            Text = @"Group gap",
            AutoSize = true,
            Margin = new Padding(8, 8, 0, 0)
        };
        _gap = new KryptonNumericUpDown
        {
            Minimum = 0,
            Maximum = 48,
            Value = 0,
            Width = 64,
            Margin = new Padding(4, 4, 8, 0)
        };
        _gap.ValueChanged += (_, _) =>
        {
            if (!_syncingGap)
            {
                SetGroupAreaGap((int)_gap.Value);
            }
        };
        buttons.Controls.Add(gapLabel);
        buttons.Controls.Add(_gap);

        _status = new KryptonWrapLabel
        {
            Dock = DockStyle.Fill,
            Text = @"Select a theme."
        };

        panel.Controls.Add(_status);
        panel.Controls.Add(buttons);
        panel.Controls.Add(instructions);

        _ribbon.CompactModeChanged += (_, _) =>
        {
            if (_compact.Checked == _ribbon.CompactMode)
            {
                _status.Text = StatusText();
                return;
            }

            _syncingCompact = true;
            _compact.Checked = _ribbon.CompactMode;
            _syncingCompact = false;
            _status.Text = StatusText();
        };

        ApplyTheme(PaletteMode.Office2024Black);
    }

    /// <summary>
    /// Hosts an Office 2024 backstage on the File tab. Page content is a label so the rail can be checked.
    /// </summary>
    private void CreateOffice2024Backstage()
    {
        var backstage = new KryptonBackstageView
        {
            OverlayMode = BackstageOverlayMode.FullClient
        };
        backstage.Pages.Add(CreateBackstagePage(@"Home", 0, false, false));
        backstage.Pages.Add(CreateBackstagePage(@"New", 1, false, false));
        backstage.Pages.Add(CreateBackstagePage(@"Open", 2, false, false));
        backstage.Pages.Add(CreateBackstagePage(@"Info", 3, false, true));
        backstage.Pages.Add(CreateBackstagePage(@"Print", 4, false, false));
        backstage.Pages.Add(CreateBackstagePage(@"Options", 10, true, false));

        var close = new KryptonBackstageCommand(@"Close")
        {
            NavigationOrder = 5
        };
        close.Click += (_, _) => _ribbon.CloseBackstageView();
        backstage.Commands.Add(close);

        _ribbon.RibbonFileAppTab.UseBackstageView = true;
        _ribbon.RibbonFileAppTab.BackstageView = backstage;
    }

    private static KryptonBackstagePage CreateBackstagePage(string text, int order, bool placeAtBottom, bool separatorBefore)
    {
        var page = new KryptonBackstagePage
        {
            Text = text,
            NavigationOrder = order,
            PlaceAtBottom = placeAtBottom,
            SeparatorBefore = separatorBefore
        };
        page.Controls.Add(new KryptonLabel
        {
            Text = text,
            Dock = DockStyle.Top,
            Padding = new Padding(24, 20, 24, 0)
        });
        return page;
    }

    /// <summary>
    /// Opens the File backstage the same way the File tab does.
    /// </summary>
    public void ShowFileBackstage()
    {
        System.Reflection.MethodInfo? method = typeof(KryptonRibbon).GetMethod(
            @"TryToggleBackstageView",
            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        method?.Invoke(_ribbon, null);
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
        _status.Text = StatusText();
    }

    /// <summary>
    /// Turns the Office 2024 group-area bevel on or off.
    /// </summary>
    /// <param name="enabled">True to draw the bevel.</param>
    public void SetGroupAreaBevel(bool enabled)
    {
        _ribbon.StateCommon.RibbonGeneral.GroupAreaBevelEdges = enabled;
        if (_bevel.Checked != enabled)
        {
            _syncingBevel = true;
            _bevel.Checked = enabled;
            _syncingBevel = false;
        }

        _status.Text = StatusText();
    }

    /// <summary>
    /// Shows or hides Office 2024 contextual tab titles in the title bar.
    /// </summary>
    /// <param name="visible">True to draw the context title in the caption.</param>
    public void SetShowContextTitles(bool visible)
    {
        _ribbon.StateCommon.RibbonGeneral.ShowContextTitles = visible;
        if (_contextTitles.Checked != visible)
        {
            _syncingContextTitles = true;
            _contextTitles.Checked = visible;
            _syncingContextTitles = false;
        }

        _status.Text = StatusText();
    }

    /// <summary>
    /// Lays the ribbon groups out as one command row, or restores the classic three-line band.
    /// </summary>
    /// <param name="compact">True for the single-row compact ribbon.</param>
    public void SetCompactMode(bool compact)
    {
        _ribbon.CompactMode = compact;
        if (_compact.Checked != compact)
        {
            _syncingCompact = true;
            _compact.Checked = compact;
            _syncingCompact = false;
        }

        _status.Text = StatusText();
    }

    /// <summary>
    /// Moves the compact-mode arrow under the groups, to the end of the groups, or onto the tab row.
    /// </summary>
    /// <param name="location">Placement of the arrow.</param>
    public void SetCompactButtonLocation(CompactButtonLocation location)
    {
        _ribbon.CompactButtonLocation = location;
        int index = location == CompactButtonLocation.GroupsTrailing
            ? 1
            : location == CompactButtonLocation.Tabs
                ? 2
                : 0;
        if (_arrowLocation.SelectedIndex != index)
        {
            _syncingArrowLocation = true;
            _arrowLocation.SelectedIndex = index;
            _syncingArrowLocation = false;
        }

        _status.Text = StatusText();
    }

    private static CompactButtonLocation LocationFromIndex(int index)
    {
        if (index == 1)
        {
            return CompactButtonLocation.GroupsTrailing;
        }

        if (index == 2)
        {
            return CompactButtonLocation.Tabs;
        }

        return CompactButtonLocation.BelowGroups;
    }

    /// <summary>
    /// Turns the compact-mode height animation on or off.
    /// </summary>
    /// <param name="animate">True to ease the groups area when compact mode changes.</param>
    public void SetAnimateCompactMode(bool animate) => _ribbon.AnimateCompactMode = animate;

    /// <summary>
    /// Selects the Home tab so the clipboard and font groups can be checked.
    /// </summary>
    public void SelectHomeTab()
    {
        if (_ribbon.RibbonTabs.Count > 0)
        {
            _ribbon.SelectedTab = _ribbon.RibbonTabs[0];
            _status.Text = StatusText();
        }
    }

    /// <summary>
    /// Sets the size at which the ribbon hides, so a narrow compact row can be checked.
    /// </summary>
    /// <param name="width">Minimum width before the ribbon hides.</param>
    /// <param name="height">Minimum height before the ribbon hides.</param>
    public void SetHideRibbonSize(int width, int height) => _ribbon.HideRibbonSize = new Size(width, height);

    /// <summary>
    /// Sets Office 2024 selected and hover tabs to a straight line or a pill-shaped line.
    /// </summary>
    /// <param name="marker">Straight line, or a pill-shaped line, under the label.</param>
    public void SetTabMarker(PaletteRibbonTabMarker marker)
    {
        _ribbon.StateCommon.RibbonGeneral.TabMarker = marker;
        var pills = marker == PaletteRibbonTabMarker.Pill;
        if (_pills.Checked != pills)
        {
            _syncingPills = true;
            _pills.Checked = pills;
            _syncingPills = false;
        }

        _status.Text = StatusText();
    }

    /// <summary>
    /// Turns the soft halo behind Office 2024 tab lines on or off.
    /// </summary>
    /// <param name="enabled">True to draw the halo.</param>
    public void SetTabMarkerGlow(bool enabled)
    {
        _ribbon.StateCommon.RibbonGeneral.TabMarkerGlow = enabled;
        if (_glow.Checked != enabled)
        {
            _syncingGlow = true;
            _glow.Checked = enabled;
            _syncingGlow = false;
        }

        _status.Text = StatusText();
    }

    /// <summary>
    /// Selects the first contextual tab so its coloured label and underline can be seen.
    /// </summary>
    public void SelectContextualTab()
    {
        foreach (KryptonRibbonTab tab in _ribbon.RibbonTabs)
        {
            if (!string.IsNullOrEmpty(tab.ContextName))
            {
                _ribbon.SelectedTab = tab;
                return;
            }
        }
    }

    /// <summary>
    /// Sets the visible width of the Office 2024 group-area bevel.
    /// </summary>
    /// <param name="size">Width in pixels at 96 DPI.</param>
    public void SetGroupAreaBevelSize(int size)
    {
        _ribbon.StateCommon.RibbonGeneral.GroupAreaBevelSize = size;
        if ((int)_bevelSize.Value != _ribbon.StateCommon.RibbonGeneral.GroupAreaBevelSize)
        {
            _syncingBevelSize = true;
            _bevelSize.Value = _ribbon.StateCommon.RibbonGeneral.GroupAreaBevelSize;
            _syncingBevelSize = false;
        }

        _status.Text = StatusText();
    }

    /// <summary>
    /// Sets the Office 2024 gap between group cards. Zero keeps one continuous card.
    /// </summary>
    /// <param name="gap">Gap in pixels at 96 DPI.</param>
    public void SetGroupAreaGap(int gap)
    {
        _ribbon.StateCommon.RibbonGeneral.GroupAreaGap = gap;
        if ((int)_gap.Value != gap)
        {
            _syncingGap = true;
            _gap.Value = gap;
            _syncingGap = false;
        }

        _status.Text = StatusText();
    }

    private string StatusText() =>
        $@"Palette: {_ribbon.PaletteMode}. Ribbon shape: {_ribbon.StateCommon.RibbonGeneral.GetRibbonShape()}. Compact: {_ribbon.CompactMode}. Bevel: {_ribbon.StateCommon.RibbonGeneral.GroupAreaBevelEdges}. Bevel size: {_ribbon.StateCommon.RibbonGeneral.GroupAreaBevelSize}. Gap: {_ribbon.StateCommon.RibbonGeneral.GroupAreaGap}. Context titles: {_ribbon.StateCommon.RibbonGeneral.ShowContextTitles}. Tab marker: {_ribbon.StateCommon.RibbonGeneral.TabMarker}. Glow: {_ribbon.StateCommon.RibbonGeneral.TabMarkerGlow}.";

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

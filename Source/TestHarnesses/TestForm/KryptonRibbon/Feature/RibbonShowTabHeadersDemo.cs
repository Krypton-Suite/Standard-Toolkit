#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Ribbon;
using Krypton.Toolkit;

namespace TestForm;

/// <summary>
/// Demonstrates ShowTabHeaders and KryptonRibbonToolbar (Issue #331).
/// </summary>
public partial class RibbonShowTabHeadersDemo : KryptonForm
{
    private KryptonRibbon? _standardRibbon;
    private KryptonRibbonToolbar? _toolbarRibbon;
    private KryptonCheckBox? _chkShowTabHeaders;
    private KryptonCheckBox? _chkAppButton;
    private KryptonLabel? _lblStatus;

    public RibbonShowTabHeadersDemo()
    {
        InitializeComponent();
        BuildUi();
        UpdateStatus();
    }

    private void BuildUi()
    {
        // Ribbons must be parented for layout; keep them as direct Dock.Top children of the form
        // (same pattern as RibbonDetachableTest). Nested SplitContainer hosting hid both ribbons.
        _standardRibbon = CreateStandardRibbon();
        _toolbarRibbon = CreateToolbarRibbon();

        var instructions = CreateLabel(
            "Issue #331 — Compare a normal KryptonRibbon (toggle ShowTabHeaders) with KryptonRibbonToolbar " +
            "(headers off by default). Selected tab groups stay visible when headers are hidden. " +
            "Standard ribbon includes a Tools context: context titles must show only with headers, and must not linger after headers are hidden.",
            LabelStyle.NormalControl,
            56);

        var optionsPanel = new KryptonPanel
        {
            Dock = DockStyle.Top,
            Height = 40,
            PanelBackStyle = PaletteBackStyle.PanelClient
        };

        _chkShowTabHeaders = new KryptonCheckBox
        {
            Text = "Standard ribbon: ShowTabHeaders",
            Checked = true,
            Location = new Point(12, 10),
            AutoSize = true
        };
        _chkShowTabHeaders.CheckedChanged += OnShowTabHeadersCheckedChanged;

        _chkAppButton = new KryptonCheckBox
        {
            Text = "App button visible (both)",
            Checked = true,
            Location = new Point(280, 10),
            AutoSize = true
        };
        _chkAppButton.CheckedChanged += OnAppButtonCheckedChanged;

        optionsPanel.Controls.Add(_chkShowTabHeaders);
        optionsPanel.Controls.Add(_chkAppButton);

        var standardHeading = CreateLabel("KryptonRibbon (toggle headers below)", LabelStyle.TitlePanel, 24);
        var standardNote = CreateLabel(
            "Use the checkbox above to hide/show tab headers. Groups of the selected tab remain. " +
            "Mouse-wheel tab switching is disabled while headers are hidden.",
            LabelStyle.NormalControl,
            40);

        var toolbarHeading = CreateLabel("KryptonRibbonToolbar (headers off by default)", LabelStyle.TitlePanel, 24);
        var toolbarNote = CreateLabel(
            "Drop-in toolbox control with ShowTabHeaders=false. Same ribbon API; designer DefaultValue is false.",
            LabelStyle.NormalControl,
            40);

        _lblStatus = CreateLabel(string.Empty, LabelStyle.NormalControl, 28);
        _lblStatus.Dock = DockStyle.Bottom;

        var filler = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            PanelBackStyle = PaletteBackStyle.PanelClient
        };

        // Dock order: add Fill/Bottom first, then Top controls from bottom-most upward so the
        // last Top control becomes the uppermost band.
        Controls.Add(filler);
        Controls.Add(_lblStatus);
        Controls.Add(toolbarNote);
        Controls.Add(_toolbarRibbon);
        Controls.Add(toolbarHeading);
        Controls.Add(standardNote);
        Controls.Add(_standardRibbon);
        Controls.Add(standardHeading);
        Controls.Add(optionsPanel);
        Controls.Add(instructions);
    }

    private static KryptonLabel CreateLabel(string text, LabelStyle style, int height)
    {
        return new KryptonLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = height,
            LabelStyle = style,
            Values = { Text = text }
        };
    }

    private KryptonRibbon CreateStandardRibbon()
    {
        var ribbon = new KryptonRibbon
        {
            Dock = DockStyle.Top,
            ShowTabHeaders = true
        };
        ribbon.RibbonFileAppButton.AppButtonVisible = true;
        PopulateRibbon(ribbon, includeSecondTab: true);
        return ribbon;
    }

    private KryptonRibbonToolbar CreateToolbarRibbon()
    {
        var ribbon = new KryptonRibbonToolbar
        {
            Dock = DockStyle.Top
        };
        ribbon.RibbonFileAppButton.AppButtonVisible = true;
        PopulateRibbon(ribbon, includeSecondTab: false);
        return ribbon;
    }

    private static void PopulateRibbon(KryptonRibbon ribbon, bool includeSecondTab)
    {
        var homeTab = new KryptonRibbonTab { Text = @"Home" };
        var clipboardGroup = new KryptonRibbonGroup { TextLine1 = @"Clipboard" };
        var triple = new KryptonRibbonGroupTriple();
        triple.Items?.Add(new KryptonRibbonGroupButton { TextLine1 = @"Paste" });
        triple.Items?.Add(new KryptonRibbonGroupButton { TextLine1 = @"Cut" });
        triple.Items?.Add(new KryptonRibbonGroupButton { TextLine1 = @"Copy" });
        clipboardGroup.Items.Add(triple);
        homeTab.Groups.Add(clipboardGroup);
        ribbon.RibbonTabs.Add(homeTab);

        if (includeSecondTab)
        {
            var viewTab = new KryptonRibbonTab { Text = @"View" };
            var viewGroup = new KryptonRibbonGroup { TextLine1 = @"Show" };
            var viewTriple = new KryptonRibbonGroupTriple();
            viewTriple.Items?.Add(new KryptonRibbonGroupButton { TextLine1 = @"Zoom" });
            viewGroup.Items.Add(viewTriple);
            viewTab.Groups.Add(viewGroup);
            ribbon.RibbonTabs.Add(viewTab);

            // Contextual tab so ShowTabHeaders toggles can be checked against context titles.
            var toolsContext = new KryptonRibbonContext
            {
                ContextName = @"Tools",
                ContextTitle = @"Tools",
                ContextColor = Color.Orange
            };
            ribbon.RibbonContexts.Add(toolsContext);

            var toolsTab = new KryptonRibbonTab
            {
                Text = @"Drawing",
                ContextName = @"Tools"
            };
            var toolsGroup = new KryptonRibbonGroup { TextLine1 = @"Draw" };
            var toolsTriple = new KryptonRibbonGroupTriple();
            toolsTriple.Items?.Add(new KryptonRibbonGroupButton { TextLine1 = @"Pen" });
            toolsGroup.Items.Add(toolsTriple);
            toolsTab.Groups.Add(toolsGroup);
            ribbon.RibbonTabs.Add(toolsTab);
            ribbon.SelectedContext = @"Tools";
        }

        ribbon.SelectedTab = homeTab;
    }

    private void OnShowTabHeadersCheckedChanged(object? sender, EventArgs e)
    {
        if (_standardRibbon is null || _chkShowTabHeaders is null)
        {
            return;
        }

        _standardRibbon.ShowTabHeaders = _chkShowTabHeaders.Checked;
        UpdateStatus();
    }

    private void OnAppButtonCheckedChanged(object? sender, EventArgs e)
    {
        if (_chkAppButton is null)
        {
            return;
        }

        var visible = _chkAppButton.Checked;
        if (_standardRibbon is not null)
        {
            _standardRibbon.RibbonFileAppButton.AppButtonVisible = visible;
        }

        if (_toolbarRibbon is not null)
        {
            _toolbarRibbon.RibbonFileAppButton.AppButtonVisible = visible;
        }

        UpdateStatus();
    }

    private void UpdateStatus()
    {
        if (_lblStatus is null || _standardRibbon is null || _toolbarRibbon is null)
        {
            return;
        }

        _lblStatus.Values.Text =
            $"Standard ShowTabHeaders={_standardRibbon.ShowTabHeaders}; " +
            $"SelectedTab={_standardRibbon.SelectedTab?.Text ?? "(null)"}; " +
            $"Toolbar ShowTabHeaders={_toolbarRibbon.ShowTabHeaders}; " +
            $"Toolbar SelectedTab={_toolbarRibbon.SelectedTab?.Text ?? "(null)"}";
    }
}

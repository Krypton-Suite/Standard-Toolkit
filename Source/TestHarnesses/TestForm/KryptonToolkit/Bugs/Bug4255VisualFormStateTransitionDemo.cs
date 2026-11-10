#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Docking;
using Krypton.Navigator;

namespace TestForm;

/// <summary>
/// Demo for issue #4255: custom-chrome maximize/minimize/restore must not flash a black frame,
/// and nested docking content must not be laid out at a 0-pixel client on minimize.
/// </summary>
public sealed class Bug4255VisualFormStateTransitionDemo : KryptonForm
{
    private const string DemoTitle = @"Bug #4255 - Form max/min black frame";
    private const int MaxSizeLogEntries = 16;

    private readonly KryptonListBox _lstSizeLog;
    private readonly KryptonLabel _lblProbeSize;
    private readonly KryptonDockingManager _outerDocking = new();
    private readonly KryptonDockingManager _innerDocking = new();
    private Control? _sizeProbe;
    private int _pageCounter;

    public Bug4255VisualFormStateTransitionDemo()
    {
        Text = DemoTitle;
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(1100, 720);
        MinimumSize = new Size(900, 560);
        MinimizeBox = true;
        MaximizeBox = true;

        var lblInfo = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 132,
            Padding = new Padding(12, 10, 12, 4),
            Text =
                @"How to test issue #4255:" + Environment.NewLine +
                @"1) Maximize, restore, and minimize from the caption buttons and from the taskbar. The frame and client must not flash black." + Environment.NewLine +
                @"2) Repeat while dragging the border to resize — chrome must keep painting live during the drag (not frozen)." + Environment.NewLine +
                @"3) Nested docking (outer workspace + inner docking manager in ""Inner Docking"") should not log a 0×0 probe size on minimize." + Environment.NewLine +
                @"4) Switch themes and retry. Programmatic Maximize/Restore/Minimize below does not raise WM_SYSCOMMAND; caption/taskbar does."
        };

        var bottom = new KryptonPanel
        {
            Dock = DockStyle.Bottom,
            Height = 168,
            Padding = new Padding(12, 8, 12, 8)
        };

        var themeRow = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 36,
            WrapContents = false,
            AutoSize = false
        };
        var lblTheme = new KryptonLabel
        {
            Text = @"Theme:",
            AutoSize = true,
            Padding = new Padding(0, 8, 8, 0)
        };
        var cmbTheme = new KryptonThemeComboBox { Width = 260 };
        var btnMaximize = new KryptonButton { Text = @"Maximize", Width = 100, Height = 28 };
        var btnRestore = new KryptonButton { Text = @"Restore", Width = 100, Height = 28 };
        var btnMinimize = new KryptonButton { Text = @"Minimize", Width = 100, Height = 28 };
        btnMaximize.Click += (_, _) => WindowState = FormWindowState.Maximized;
        btnRestore.Click += (_, _) => WindowState = FormWindowState.Normal;
        btnMinimize.Click += (_, _) => WindowState = FormWindowState.Minimized;
        themeRow.Controls.Add(lblTheme);
        themeRow.Controls.Add(cmbTheme);
        themeRow.Controls.Add(btnMaximize);
        themeRow.Controls.Add(btnRestore);
        themeRow.Controls.Add(btnMinimize);

        _lblProbeSize = new KryptonLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 24,
            Text = @"Inner probe size: (pending)"
        };

        _lstSizeLog = new KryptonListBox
        {
            Dock = DockStyle.Fill
        };

        bottom.Controls.Add(_lstSizeLog);
        bottom.Controls.Add(_lblProbeSize);
        bottom.Controls.Add(themeRow);

        var hostPanel = new KryptonPanel { Dock = DockStyle.Fill };
        var outerWorkspace = new KryptonDockableWorkspace { Dock = DockStyle.Fill };
        hostPanel.Controls.Add(outerWorkspace);

        Controls.Add(hostPanel);
        Controls.Add(bottom);
        Controls.Add(lblInfo);

        _outerDocking.ManageControl("Control", hostPanel);
        _outerDocking.ManageWorkspace("Workspace", outerWorkspace);
        _outerDocking.ManageFloating("Floating", this);

        var leftPage = CreateColoredPage("Toolbox", Color.Lavender);
        _outerDocking.AddDockspace("Control", DockingEdge.Left, new[] { leftPage });

        var docA = CreateColoredPage("Document A", Color.LightBlue);
        var nestedPage = CreateNestedDockingPage();
        _outerDocking.AddToWorkspace("Workspace", new[] { nestedPage, docA });

        SizeChanged += OnFormSizeChanged;
        LogSize(@"created");
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _innerDocking.Dispose();
            _outerDocking.Dispose();
        }

        base.Dispose(disposing);
    }

    private KryptonPage CreateNestedDockingPage()
    {
        var page = CreatePageShell("Inner Docking");
        var innerHost = new KryptonPanel { Dock = DockStyle.Fill };
        var innerWorkspace = new KryptonDockableWorkspace { Dock = DockStyle.Fill };
        innerHost.Controls.Add(innerWorkspace);
        page.Controls.Add(innerHost);

        _innerDocking.ManageControl("Control", innerHost);
        _innerDocking.ManageWorkspace("Workspace", innerWorkspace);

        var nestedLeft = CreateColoredPage("Nested Left", Color.MistyRose);
        _innerDocking.AddDockspace("Control", DockingEdge.Left, new[] { nestedLeft });

        var nestedDoc = CreateColoredPage("Nested Document", Color.Honeydew);
        var splitterPage = CreateNestedSplitterPage();
        _innerDocking.AddToWorkspace("Workspace", new[] { splitterPage, nestedDoc });

        return page;
    }

    private KryptonPage CreateNestedSplitterPage()
    {
        var page = CreatePageShell("Nested Splitters");
        var outerSplit = new KryptonSplitContainer
        {
            Dock = DockStyle.Fill,
            Orientation = Orientation.Vertical
        };
        var innerSplit = new KryptonSplitContainer
        {
            Dock = DockStyle.Fill,
            Orientation = Orientation.Horizontal
        };

        var probe = new KryptonPanel { Dock = DockStyle.Fill };
        probe.StateCommon.Color1 = Color.LightGoldenrodYellow;
        var probeLabel = new KryptonLabel
        {
            Dock = DockStyle.Fill,
            LabelStyle = LabelStyle.NormalPanel,
            Text = @"Size probe" + Environment.NewLine + @"This panel should not be laid out at 0×0 when the form is minimized from the caption or taskbar."
        };
        probeLabel.StateCommon.ShortText.TextH = PaletteRelativeAlign.Center;
        probeLabel.StateCommon.ShortText.TextV = PaletteRelativeAlign.Center;
        probe.Controls.Add(probeLabel);
        probe.SizeChanged += OnProbeSizeChanged;

        innerSplit.Panel1.Controls.Add(CreateFillPanel(Color.Thistle, "Nested top"));
        innerSplit.Panel2.Controls.Add(probe);
        outerSplit.Panel1.Controls.Add(CreateFillPanel(Color.PowderBlue, "Nested left"));
        outerSplit.Panel2.Controls.Add(innerSplit);
        page.Controls.Add(outerSplit);

        _sizeProbe = probe;
        return page;
    }

    private KryptonPage CreateColoredPage(string name, Color backColor)
    {
        var page = CreatePageShell(name);
        page.Controls.Add(CreateFillPanel(backColor, name));
        return page;
    }

    private KryptonPage CreatePageShell(string name)
    {
        var page = new KryptonPage
        {
            Name = $"Page_{_pageCounter++}",
            Text = name,
            TextTitle = name,
            TextDescription = name,
            UniqueName = $"Bug4255_{name}_{Guid.NewGuid():N}",
            MinimumSize = new Size(120, 120)
        };

        page.SetFlags(KryptonPageFlags.AllowConfigSave | KryptonPageFlags.DockingAllowDocked |
                      KryptonPageFlags.DockingAllowFloating | KryptonPageFlags.DockingAllowAutoHidden |
                      KryptonPageFlags.DockingAllowWorkspace);

        return page;
    }

    private static KryptonPanel CreateFillPanel(Color backColor, string caption)
    {
        var panel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            BackColor = backColor
        };
        panel.StateCommon.Color1 = backColor;

        var label = new KryptonLabel
        {
            Text = caption,
            Dock = DockStyle.Fill,
            LabelStyle = LabelStyle.NormalPanel
        };
        label.StateCommon.ShortText.TextH = PaletteRelativeAlign.Center;
        label.StateCommon.ShortText.TextV = PaletteRelativeAlign.Center;
        panel.Controls.Add(label);
        return panel;
    }

    private void OnFormSizeChanged(object? sender, EventArgs e) => LogSize(@"form");

    private void OnProbeSizeChanged(object? sender, EventArgs e)
    {
        if (sender is Control control)
        {
            _lblProbeSize.Text = $@"Inner probe size: {control.Width}×{control.Height}  (0×0 on minimize is the pre-fix layout cascade)";
        }

        LogSize(@"probe");
    }

    private void LogSize(string source)
    {
        var probe = _sizeProbe == null
            ? @"n/a"
            : $"{_sizeProbe.Width}×{_sizeProbe.Height}";
        var line =
            $"{DateTime.Now:HH:mm:ss.fff}  {source,-6}  WindowState={WindowState}  Client={ClientSize.Width}×{ClientSize.Height}  Probe={probe}";

        _lstSizeLog.Items.Insert(0, line);
        while (_lstSizeLog.Items.Count > MaxSizeLogEntries)
        {
            _lstSizeLog.Items.RemoveAt(_lstSizeLog.Items.Count - 1);
        }
    }
}

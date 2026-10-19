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
/// Shows <see cref="KryptonRibbonGroupStack"/> beside a triple (issue #4560).
/// </summary>
public partial class RibbonGroupStackDemo : KryptonForm
{
    private KryptonRibbon? _ribbon;
    private KryptonRibbonGroupStack? _adaptive;
    private KryptonLabel? _status;

    public RibbonGroupStackDemo()
    {
        InitializeComponent();
        BuildUi();
    }

    private void BuildUi()
    {
        _ribbon = new KryptonRibbon
        {
            Name = "kryptonRibbon",
            Dock = DockStyle.Top
        };

        var tab = new KryptonRibbonTab { Text = "Home" };

        var tripleGroup = new KryptonRibbonGroup { TextLine1 = "Triple" };
        var triple = new KryptonRibbonGroupTriple();
        triple.Items!.Add(CreateButton("Cut"));
        triple.Items.Add(CreateButton("Copy"));
        triple.Items.Add(CreateButton("Paste"));
        tripleGroup.Items.Add(triple);

        var columnsGroup = new KryptonRibbonGroup { TextLine1 = "Columns" };
        var columns = new KryptonRibbonGroupStack
        {
            StackLayout = RibbonGroupStackLayout.Columns
        };
        for (var i = 1; i <= 6; i++)
        {
            columns.Items!.Add(CreateButton("B" + i.ToString()));
        }

        columns.Items!.Add(new KryptonRibbonGroupTextBox { Text = "Note" });

        var cluster = new KryptonRibbonGroupCluster();
        cluster.Items!.Add(new KryptonRibbonGroupClusterButton { TextLine = "A" });
        cluster.Items.Add(new KryptonRibbonGroupClusterButton { TextLine = "B" });
        columns.Items.Add(cluster);
        columnsGroup.Items.Add(columns);

        var adaptiveGroup = new KryptonRibbonGroup { TextLine1 = "Adaptive" };
        _adaptive = new KryptonRibbonGroupStack
        {
            StackLayout = RibbonGroupStackLayout.Adaptive
        };
        _adaptive.Items!.Add(CreateButton("Wide 1"));
        _adaptive.Items.Add(CreateButton("Wide 2"));
        _adaptive.Items.Add(CreateButton("Wide 3"));
        _adaptive.Items.Add(CreateButton("Wide 4"));
        _adaptive.PropertyChanged += OnAdaptivePropertyChanged;
        adaptiveGroup.Items.Add(_adaptive);

        tab.Groups.Add(tripleGroup);
        tab.Groups.Add(columnsGroup);
        tab.Groups.Add(adaptiveGroup);
        _ribbon.RibbonTabs.Add(tab);
        _ribbon.SelectedTab = tab;

        var instructions = new KryptonLabel
        {
            Dock = DockStyle.Top,
            LabelStyle = LabelStyle.NormalControl,
            AutoSize = false,
            Height = 72,
            Values = { Text = "Issue #4560. Triple stays at three buttons. Columns is a stack of six buttons, a text box, and a cluster, filled down columns of three. Adaptive is four large buttons in one row; narrow the window (or use Narrow) and they wrap into columns of three. The theme list restyles the ribbon." }
        };

        var options = new KryptonPanel
        {
            Dock = DockStyle.Top,
            Height = 40,
            PanelBackStyle = PaletteBackStyle.PanelClient
        };

        var theme = new KryptonThemeComboBox
        {
            Width = 260,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Location = new Point(12, 8)
        };

        var wide = new KryptonButton
        {
            Text = "Wide",
            Location = new Point(284, 6),
            AutoSize = true
        };
        wide.Click += (_, _) => Width = 1100;

        var narrow = new KryptonButton
        {
            Text = "Narrow",
            Location = new Point(360, 6),
            AutoSize = true
        };
        narrow.Click += (_, _) => Width = 640;

        _status = new KryptonLabel
        {
            Location = new Point(450, 10),
            AutoSize = true,
            LabelStyle = LabelStyle.NormalControl,
            Values = { Text = "Adaptive size: Large" }
        };

        options.Controls.Add(theme);
        options.Controls.Add(wide);
        options.Controls.Add(narrow);
        options.Controls.Add(_status);

        Controls.Add(options);
        Controls.Add(instructions);
        Controls.Add(_ribbon);
        Controls.SetChildIndex(_ribbon, 0);
    }

    private static KryptonRibbonGroupButton CreateButton(string text) =>
        new KryptonRibbonGroupButton { TextLine1 = text };

    private void OnAdaptivePropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if ((e.PropertyName == "ItemSizeCurrent") && (_status != null) && (_adaptive != null))
        {
            _status.Values.Text = "Adaptive size: " + _adaptive.ItemSizeCurrent.ToString();
        }
    }
}

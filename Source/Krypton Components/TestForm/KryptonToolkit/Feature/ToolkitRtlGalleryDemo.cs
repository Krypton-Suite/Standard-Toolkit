#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Navigator;

namespace TestForm;

/// <summary>
/// Comprehensive Issue #2379 gallery of visual Toolkit controls under the two-flag RTL contract.
/// </summary>
public partial class ToolkitRtlGalleryDemo : KryptonForm
{
    private KryptonContextMenu? _dropMenu;
    private KryptonContextMenu? _itemMenu;
    private BindingSource? _bindingSource;
    private PropertyGrid? _nativePropertyGrid;
    private KryptonPropertyGrid? _kryptonPropertyGrid;

    public ToolkitRtlGalleryDemo()
    {
        InitializeComponent();
        BuildPages();
        kchkDualRtl.CheckedChanged += (_, _) => SetDualRtl(kchkDualRtl.Checked);
        kchkDualRtl.Checked = true;
    }

    /// <summary>
    /// Sets both RTL flags on this form and descendant Krypton controls.
    /// </summary>
    /// <param name="enabled">True for RightToLeft.Yes plus RightToLeftLayout; false for LTR.</param>
    public void SetDualRtl(bool enabled)
    {
        RightToLeft = enabled ? RightToLeft.Yes : RightToLeft.No;
        RightToLeftLayout = enabled;
        ApplyDualRtl(this, enabled);
        _nativePropertyGrid?.Refresh();
        _kryptonPropertyGrid?.Refresh();
        PerformLayout();
        Refresh();
    }

    private static void ApplyDualRtl(Control parent, bool enabled)
    {
        foreach (Control child in parent.Controls)
        {
            child.RightToLeft = enabled ? RightToLeft.Yes : RightToLeft.No;
            switch (child)
            {
                case VisualControlBase visual:
                    visual.RightToLeftLayout = enabled;
                    break;
                case VisualPanel panel:
                    panel.RightToLeftLayout = enabled;
                    break;
                case VisualContainerControlBase container:
                    container.RightToLeftLayout = enabled;
                    break;
            }

            if (child is TableLayoutPanel tlp)
            {
                tlp.RightToLeft = enabled ? RightToLeft.Yes : RightToLeft.No;
            }

            ApplyDualRtl(child, enabled);
        }
    }

    private void BuildPages()
    {
        AddPage("Buttons", CreateButtonsPage());
        AddPage("Inputs", CreateInputsPage());
        AddPage("Choice", CreateChoicePage());
        AddPage("Lists", CreateListsPage());
        AddPage("PropertyGrid", CreatePropertyGridPage());
        AddPage("Chrome", CreateChromePage());
        AddPage("Calendar", CreateCalendarPage());
        AddPage("Strips", CreateStripsPage());
        AddPage("Dialogs", CreateDialogsPage());
        AddPage("Utilities", CreateUtilitiesPage());
    }

    private void AddPage(string title, Control content)
    {
        var page = new KryptonPage();
        ((ISupportInitialize)page).BeginInit();
        page.Text = title;
        page.TextTitle = title;
        page.TextDescription = title;
        content.Dock = DockStyle.Fill;
        page.Controls.Add(content);
        ((ISupportInitialize)page).EndInit();
        kryptonNavigator.Pages.Add(page);
    }

    private static FlowLayoutPanel CreateFlow() => new FlowLayoutPanel
    {
        Dock = DockStyle.Fill,
        AutoScroll = true,
        FlowDirection = FlowDirection.TopDown,
        WrapContents = false,
        Padding = new Padding(12)
    };

    private static Control CreateButtonsPage()
    {
        var flow = CreateFlow();
        flow.Controls.Add(new KryptonButton { Text = "KryptonButton", Width = 160 });
        flow.Controls.Add(new KryptonCheckButton { Text = "KryptonCheckButton", Width = 180, Checked = true });
        var colorButton = new KryptonColorButton { Width = 160 };
        colorButton.Values.Text = "KryptonColorButton";
        flow.Controls.Add(colorButton);
        var dropMenu = new KryptonContextMenu();
        var dropItems = new KryptonContextMenuItems();
        dropItems.Items.Add(new KryptonContextMenuItem("Drop item"));
        dropMenu.Items.Add(dropItems);
        var dropButton = new KryptonDropButton { Text = "KryptonDropButton", Width = 180 };
        dropButton.KryptonContextMenu = dropMenu;
        flow.Controls.Add(dropButton);
        var link = new KryptonLinkLabel();
        link.Values.Text = "KryptonLinkLabel";
        flow.Controls.Add(link);
        flow.Controls.Add(new KryptonLinkWrapLabel { Text = "KryptonLinkWrapLabel wraps in RTL.", Width = 240, AutoSize = false, Height = 36 });
        flow.Controls.Add(new KryptonPoweredByButton { Width = 180 });
        return flow;
    }

    private static Control CreateInputsPage()
    {
        var flow = CreateFlow();
        flow.Controls.Add(new KryptonLabel { Values = { Text = "TextBox / Masked / Rich / Combo / spin" } });
        flow.Controls.Add(new KryptonTextBox { Text = "KryptonTextBox", Width = 220 });
        flow.Controls.Add(new KryptonMaskedTextBox { Mask = "00/00/0000", Width = 140 });
        flow.Controls.Add(new KryptonRichTextBox { Text = "KryptonRichTextBox sample.", Width = 280, Height = 64 });
        var combo = new KryptonComboBox { Width = 200, Text = "KryptonComboBox" };
        combo.Items.Add("One");
        combo.Items.Add("Two");
        flow.Controls.Add(combo);
        flow.Controls.Add(new KryptonNumericUpDown { Value = 7, Width = 120 });
        var domain = new KryptonDomainUpDown { Width = 140 };
        domain.Items.Add("Alpha");
        domain.Items.Add("Beta");
        domain.SelectedIndex = 0;
        flow.Controls.Add(domain);
        flow.Controls.Add(new KryptonCalcInput { Value = 12.5m, Width = 140 });
        flow.Controls.Add(new KryptonDateTimePicker { Width = 200 });
        flow.Controls.Add(new KryptonWrapLabel { Text = "KryptonWrapLabel: reading order follows RightToLeft.", Width = 280, AutoSize = false, Height = 36 });
        return flow;
    }

    private static Control CreateChoicePage()
    {
        var flow = CreateFlow();
        flow.Controls.Add(new KryptonCheckBox { Text = "KryptonCheckBox", AutoSize = true });
        flow.Controls.Add(new KryptonRadioButton { Text = "KryptonRadioButton A", AutoSize = true, Checked = true });
        flow.Controls.Add(new KryptonRadioButton { Text = "KryptonRadioButton B", AutoSize = true });
        flow.Controls.Add(new KryptonToggleSwitch { Width = 72, Height = 28 });
        flow.Controls.Add(new KryptonRating { Width = 160, Height = 28 });
        flow.Controls.Add(new KryptonTrackBar { Width = 220 });
        var progress = new KryptonProgressBar { Width = 220, Height = 22, Maximum = 100, Value = 45 };
        flow.Controls.Add(progress);
        return flow;
    }

    private Control CreateListsPage()
    {
        var flow = CreateFlow();
        var list = new KryptonListBox { Width = 200, Height = 80 };
        list.Items.Add("List one");
        list.Items.Add("List two");
        flow.Controls.Add(list);
        var checkedList = new KryptonCheckedListBox { Width = 200, Height = 80 };
        checkedList.Items.Add("Checked one");
        checkedList.Items.Add("Checked two");
        flow.Controls.Add(checkedList);
        var listView = new KryptonListView { Width = 240, Height = 90, View = View.Details };
        listView.Columns.Add("Name", 160);
        listView.Items.Add("ListView one");
        listView.Items.Add("ListView two");
        flow.Controls.Add(listView);
        var tree = new KryptonTreeView { Width = 220, Height = 90 };
        var root = tree.Nodes.Add("Root");
        root.Nodes.Add("Child");
        root.Expand();
        flow.Controls.Add(tree);
        var grid = new KryptonDataGridView
        {
            Width = 320,
            Height = 100,
            AutoGenerateColumns = true,
            AllowUserToAddRows = false
        };
        grid.DataSource = new[]
        {
            new GalleryRow { Name = "One", Amount = 10 },
            new GalleryRow { Name = "Two", Amount = 20 }
        };
        flow.Controls.Add(grid);
        return flow;
    }

    private Control CreatePropertyGridPage()
    {
        var host = new KryptonPanel { Dock = DockStyle.Fill };
        var instructions = new KryptonLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 48,
            Values =
            {
                Text = "Native WinForms PropertyGrid vs KryptonPropertyGrid. Dual RTL on this form. " +
                       "Both receive RightToLeft (help pane / toolbar). Label/value columns stay native LTR — " +
                       "WinForms PropertyGrid does not mirror them. Selected object is this form."
            }
        };

        var split = new KryptonSplitContainer
        {
            Dock = DockStyle.Fill,
            SplitterDistance = 420
        };

        var nativeLabel = new Label
        {
            Dock = DockStyle.Top,
            Height = 22,
            Text = "Native PropertyGrid",
            TextAlign = ContentAlignment.MiddleLeft
        };
        _nativePropertyGrid = new PropertyGrid
        {
            Dock = DockStyle.Fill,
            SelectedObject = this,
            HelpVisible = true,
            ToolbarVisible = true
        };
        split.Panel1.Controls.Add(_nativePropertyGrid);
        split.Panel1.Controls.Add(nativeLabel);

        var kryptonLabel = new KryptonLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 22,
            Values = { Text = "KryptonPropertyGrid" }
        };
        _kryptonPropertyGrid = new KryptonPropertyGrid
        {
            Dock = DockStyle.Fill,
            SelectedObject = this
        };
        split.Panel2.Controls.Add(_kryptonPropertyGrid);
        split.Panel2.Controls.Add(kryptonLabel);

        host.Controls.Add(split);
        host.Controls.Add(instructions);
        return host;
    }

    private static Control CreateChromePage()
    {
        var flow = CreateFlow();
        var header = new KryptonHeader { Width = 320, Height = 36 };
        header.Values.Heading = "KryptonHeader";
        flow.Controls.Add(header);
        var headerGroup = new KryptonHeaderGroup { Width = 360, Height = 80 };
        headerGroup.ValuesPrimary.Heading = "KryptonHeaderGroup";
        headerGroup.Panel.Controls.Add(new KryptonLabel
        {
            Values = { Text = "Header group body" },
            Location = new Point(8, 8)
        });
        flow.Controls.Add(headerGroup);
        var groupBox = new KryptonGroupBox { Width = 280, Height = 72 };
        groupBox.Values.Heading = "KryptonGroupBox";
        groupBox.Panel.Controls.Add(new KryptonLabel { Values = { Text = "GroupBox panel" }, Location = new Point(8, 8) });
        flow.Controls.Add(groupBox);
        var group = new KryptonGroup { Width = 220, Height = 56 };
        group.Panel.Controls.Add(new KryptonLabel { Values = { Text = "KryptonGroup" }, Location = new Point(8, 8) });
        flow.Controls.Add(group);
        var panel = new KryptonPanel { Width = 220, Height = 40 };
        panel.Controls.Add(new KryptonLabel { Values = { Text = "KryptonPanel" }, Location = new Point(8, 8) });
        flow.Controls.Add(panel);
        var split = new KryptonSplitContainer { Width = 360, Height = 80, SplitterDistance = 160 };
        split.Panel1.Controls.Add(new KryptonLabel { Values = { Text = "Panel1" }, Location = new Point(8, 8) });
        split.Panel2.Controls.Add(new KryptonLabel { Values = { Text = "Panel2" }, Location = new Point(8, 8) });
        flow.Controls.Add(split);
        flow.Controls.Add(new KryptonSeparator { Width = 240, Height = 8 });
        flow.Controls.Add(new KryptonBorderEdge { Width = 240, Height = 4 });
        var bread = new KryptonBreadCrumb { Width = 320 };
        bread.RootItem.ShortText = "Root";
        var child = new KryptonBreadCrumbItem { ShortText = "Child" };
        bread.RootItem.Items.Add(child);
        bread.SelectedItem = child;
        flow.Controls.Add(bread);
        var flowPanel = new KryptonFlowLayoutPanel { Width = 240, Height = 40 };
        flowPanel.Controls.Add(new KryptonLabel { Values = { Text = "FlowLayoutPanel" } });
        flow.Controls.Add(flowPanel);
        flow.Controls.Add(new KryptonPictureBox
        {
            Width = 32,
            Height = 32,
            SizeMode = PictureBoxSizeMode.CenterImage,
            Image = SystemIcons.Information.ToBitmap()
        });
        flow.Controls.Add(new KryptonThemeComboBox { Width = 240 });
        return flow;
    }

    private static Control CreateCalendarPage()
    {
        var flow = CreateFlow();
        flow.Controls.Add(new KryptonLabel
        {
            Values = { Text = "Both flags pack weekday columns from the start edge (first day on the right)." }
        });
        flow.Controls.Add(new KryptonMonthCalendar());
        var features = new KryptonMonthCalendar { ShowWeekNumbers = true };
        flow.Controls.Add(features);
        return flow;
    }

    private Control CreateStripsPage()
    {
        var host = new KryptonPanel { Dock = DockStyle.Fill };
        var menuStrip = new KryptonMenuStrip { Dock = DockStyle.Top };
        var fileItem = new ToolStripMenuItem("File");
        fileItem.DropDownItems.Add("Open");
        menuStrip.Items.Add(fileItem);
        menuStrip.Items.Add(new ToolStripMenuItem("Edit"));
        host.Controls.Add(menuStrip);

        var menuBar = new KryptonMenuBar { Dock = DockStyle.Top, Height = 28 };
        var fileBar = new KryptonContextMenuItem("File");
        fileBar.Items.Add(new KryptonContextMenuItem("Open"));
        menuBar.Items.Add(fileBar);
        menuBar.Items.Add(new KryptonContextMenuItem("Edit"));
        host.Controls.Add(menuBar);

        var toolStrip = new KryptonToolStrip { Dock = DockStyle.Top };
        toolStrip.Items.Add(new ToolStripButton("Cut"));
        toolStrip.Items.Add(new ToolStripButton("Copy"));
        host.Controls.Add(toolStrip);

        _bindingSource = new BindingSource
        {
            DataSource = new List<GalleryRow>
            {
                new GalleryRow { Name = "Alpha", Amount = 1 },
                new GalleryRow { Name = "Beta", Amount = 2 }
            }
        };
        var bindingNav = new KryptonBindingNavigator { Dock = DockStyle.Top, BindingSource = _bindingSource };
        host.Controls.Add(bindingNav);

        var status = new KryptonStatusStrip { Dock = DockStyle.Bottom };
        status.Items.Add(new ToolStripStatusLabel("KryptonStatusStrip"));
        host.Controls.Add(status);

        var body = new KryptonLabel
        {
            Dock = DockStyle.Fill,
            Values = { Text = "MenuStrip, MenuBar, ToolStrip, BindingNavigator, StatusStrip. Toggle Dual RTL." }
        };
        host.Controls.Add(body);
        body.SendToBack();
        return host;
    }

    private Control CreateDialogsPage()
    {
        var flow = CreateFlow();
        _itemMenu = new KryptonContextMenu();
        var items = new KryptonContextMenuItems();
        items.Items.Add(new KryptonContextMenuItem("Item one"));
        var sub = new KryptonContextMenuItem("Submenu");
        sub.Items.Add(new KryptonContextMenuItem("Child item"));
        items.Items.Add(sub);
        _itemMenu.Items.Add(items);
        var menuButton = new KryptonButton { Text = "Context menu", Width = 160 };
        menuButton.Click += (_, _) => _itemMenu!.Show(menuButton);
        flow.Controls.Add(menuButton);

        var taskButton = new KryptonButton { Text = "TaskDialog", Width = 160 };
        taskButton.Click += (_, _) =>
        {
            using var dialog = new KryptonTaskDialog();
            dialog.Heading.Text = "RTL TaskDialog";
            dialog.Heading.IconType = KryptonTaskDialogIconType.ShieldInformation;
            dialog.Heading.Visible = true;
            dialog.Content.Text = "Owner Dual RTL copies onto the dialog; table columns reverse when both flags are set.";
            dialog.Content.Visible = true;
            dialog.FooterBar.CommonButtons.Buttons = KryptonTaskDialogCommonButtonTypes.OK;
            dialog.FooterBar.Visible = true;
            dialog.ShowDialog(this);
        };
        flow.Controls.Add(taskButton);

        var msgButton = new KryptonButton { Text = "MessageBox", Width = 160 };
        msgButton.Click += (_, _) =>
            KryptonMessageBox.Show(this, "MessageBox follows the owner RTL flags.", "RTL MessageBox",
                KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
        flow.Controls.Add(msgButton);

        _dropMenu = new KryptonContextMenu();
        var dropItems = new KryptonContextMenuItems();
        dropItems.Items.Add(new KryptonContextMenuItem("Palette item"));
        _dropMenu.Items.Add(dropItems);
        flow.Controls.Add(new KryptonLabel
        {
            Values = { Text = "OpenFile / SaveFile / FolderBrowser remain native dialogs; use Dual RTL on this form then open them from other demos." }
        });
        return flow;
    }

    private static Control CreateUtilitiesPage()
    {
        var flow = CreateFlow();
        flow.Controls.Add(new KryptonLabel
        {
            Values = { Text = "Krypton.Toolkit.Utilities samples that participate in the same two-flag contract." }
        });
        var commandLink = new Krypton.Toolkit.Utilities.KryptonCommandLinkButton
        {
            Width = 280,
            Height = 56
        };
        commandLink.CommandLinkTextValues.Heading = "CommandLink";
        commandLink.CommandLinkTextValues.Description = "Arrow mirrors with both flags.";
        flow.Controls.Add(commandLink);
        return flow;
    }

    private sealed class GalleryRow
    {
        public string Name { get; set; } = string.Empty;
        public int Amount { get; set; }
    }
}

#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

internal partial class VisualBreadCrumbItemsForm
{
    #region Windows Form Designer generated code

    private IContainer components = null!;

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new Container();
        treeView1 = new KryptonTreeView();
        buttonMoveUp = new KryptonButton();
        buttonMoveDown = new KryptonButton();
        buttonAddItem = new KryptonButton();
        buttonDelete = new KryptonButton();
        propertyGrid1 = new KryptonPropertyGrid();
        groupBoxItems = new KryptonGroupBox();
        groupBoxProperties = new KryptonGroupBox();
        buttonAddChild = new KryptonButton();
        kpnlContent = new KryptonPanel();
        kpnlButtonBar = new InternalDesignerEditorButtonBarPanel();
        ((ISupportInitialize)groupBoxItems).BeginInit();
        ((ISupportInitialize)groupBoxItems.Panel).BeginInit();
        groupBoxItems.Panel.SuspendLayout();
        groupBoxItems.SuspendLayout();
        ((ISupportInitialize)groupBoxProperties).BeginInit();
        ((ISupportInitialize)groupBoxProperties.Panel).BeginInit();
        groupBoxProperties.Panel.SuspendLayout();
        groupBoxProperties.SuspendLayout();
        ((ISupportInitialize)kpnlContent).BeginInit();
        kpnlContent.SuspendLayout();
        SuspendLayout();

        // treeView1
        treeView1.Dock = DockStyle.Fill;
        treeView1.Name = nameof(treeView1);
        treeView1.TabIndex = 1;
        treeView1.HideSelection = false;
        treeView1.AfterSelect += treeView1_AfterSelect;

        // propertyGrid1
        propertyGrid1.Dock = DockStyle.Fill;
        propertyGrid1.HelpVisible = false;
        propertyGrid1.Name = nameof(propertyGrid1);
        propertyGrid1.TabIndex = 7;
        propertyGrid1.ToolbarVisible = false;

        // groupBoxItems
        groupBoxItems.Dock = DockStyle.Fill;
        groupBoxItems.Name = nameof(groupBoxItems);
        groupBoxItems.Values.Heading = @"BreadCrumbItems Collection";
        groupBoxItems.Panel.Controls.Add(treeView1);

        // groupBoxProperties
        groupBoxProperties.Dock = DockStyle.Fill;
        groupBoxProperties.Name = nameof(groupBoxProperties);
        groupBoxProperties.Values.Heading = @"Item Properties";
        groupBoxProperties.Panel.Controls.Add(propertyGrid1);

        // Navigation buttons
        ConfigureToolbarButton(buttonMoveUp, BlueArrowResources.arrow_up_blue, @"Move Up", buttonMoveUp_Click);
        buttonMoveUp.Name = nameof(buttonMoveUp);
        buttonMoveUp.TabIndex = 2;

        ConfigureToolbarButton(buttonMoveDown, BlueArrowResources.arrow_down_blue, @"Move Down", buttonMoveDown_Click);
        buttonMoveDown.Name = nameof(buttonMoveDown);
        buttonMoveDown.TabIndex = 3;

        ConfigureToolbarButton(buttonAddItem, GenericImageResources.add, @"Add Sibling", buttonAddSibling_Click);
        buttonAddItem.Name = nameof(buttonAddItem);
        buttonAddItem.TabIndex = 4;

        ConfigureToolbarButton(buttonAddChild, GenericImageResources.add, @"Add Child", buttonAddChild_Click);
        buttonAddChild.Name = nameof(buttonAddChild);
        buttonAddChild.TabIndex = 5;

        ConfigureToolbarButton(buttonDelete, GenericImageResources.delete, @"Delete Item", buttonDelete_Click);
        buttonDelete.Name = nameof(buttonDelete);
        buttonDelete.TabIndex = 6;

        var navPanel = new FlowLayoutPanel
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Dock = DockStyle.Top,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            Padding = new Padding(0),
            Margin = new Padding(0),
            BackColor = Color.Transparent
        };
        navPanel.Controls.Add(buttonMoveUp);
        navPanel.Controls.Add(buttonMoveDown);
        navPanel.Controls.Add(buttonAddItem);
        navPanel.Controls.Add(buttonAddChild);
        navPanel.Controls.Add(buttonDelete);

        var navHost = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(8, 18, 8, 8),
            StateCommon = { Color1 = Color.Transparent }
        };
        navHost.Controls.Add(navPanel);

        _layout = new TableLayoutPanel
        {
            ColumnCount = 3,
            Dock = DockStyle.Fill,
            RowCount = 1,
            Padding = new Padding(6),
            BackColor = Color.Transparent
        };
        _layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
        _layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
        _layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _layout.Controls.Add(groupBoxItems, 0, 0);
        _layout.Controls.Add(navHost, 1, 0);
        _layout.Controls.Add(groupBoxProperties, 2, 0);

        groupBoxItems.Margin = new Padding(0, 0, 6, 0);
        navHost.Margin = new Padding(0, 0, 6, 0);
        groupBoxProperties.Margin = new Padding(0);

        kpnlContent.Controls.Add(_layout);
        kpnlContent.Dock = DockStyle.Fill;
        kpnlContent.Name = "kpnlContent";
        kpnlContent.Padding = new Padding(9);
        kpnlContent.PanelBackStyle = PaletteBackStyle.PanelClient;

        kpnlButtonBar.Dock = DockStyle.Bottom;
        kpnlButtonBar.Name = "kpnlButtonBar";

        AutoScaleMode = AutoScaleMode.None;
        ClientSize = new Size(720, 510);
        Controls.Add(kpnlContent);
        Controls.Add(kpnlButtonBar);
        MinimumSize = new Size(640, 470);
        Name = "KryptonBreadCrumbCollectionForm";
        Text = @"BreadCrumbItems Collection Editor";

        ((ISupportInitialize)groupBoxItems.Panel).EndInit();
        groupBoxItems.Panel.ResumeLayout(false);
        ((ISupportInitialize)groupBoxItems).EndInit();
        groupBoxItems.ResumeLayout(false);
        ((ISupportInitialize)groupBoxProperties.Panel).EndInit();
        groupBoxProperties.Panel.ResumeLayout(false);
        ((ISupportInitialize)groupBoxProperties).EndInit();
        groupBoxProperties.ResumeLayout(false);
        ((ISupportInitialize)kpnlContent).EndInit();
        kpnlContent.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private DictItemBase _beforeItems = null!;
    private List<KryptonBreadCrumbItem> _sessionStartRootOrder = null!;
    private Dictionary<KryptonBreadCrumbItem, List<KryptonBreadCrumbItem>> _sessionStartChildOrder = null!;
    private KryptonTreeView treeView1;
    private KryptonButton buttonMoveUp;
    private KryptonButton buttonMoveDown;
    private KryptonButton buttonAddItem;
    private KryptonButton buttonDelete;
    private KryptonPropertyGrid propertyGrid1;
    private KryptonGroupBox groupBoxItems;
    private KryptonGroupBox groupBoxProperties;
    private KryptonButton buttonAddChild;
    private TableLayoutPanel _layout;
    private KryptonPanel kpnlContent;
    private InternalDesignerEditorButtonBarPanel kpnlButtonBar;
}

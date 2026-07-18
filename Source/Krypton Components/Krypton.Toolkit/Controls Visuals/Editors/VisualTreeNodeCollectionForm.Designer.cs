#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal partial class VisualTreeNodeCollectionForm
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
        _membersLabel = new KryptonLabel();
        _treeView = new KryptonTreeView();
        _membersPanel = new KryptonPanel();
        _propertiesLabel = new KryptonLabel();
        _propertyGrid = new KryptonPropertyGrid();
        _propertiesPanel = new KryptonPanel();
        _buttonAddRoot = new KryptonButton();
        _buttonAddChild = new KryptonButton();
        _buttonDelete = new KryptonButton();
        _buttonMoveUp = new KryptonButton();
        _buttonMoveDown = new KryptonButton();
        _navPanel = new TableLayoutPanel();
        _navHost = new KryptonPanel();
        _layout = new TableLayoutPanel();
        kpnlContent = new KryptonPanel();
        kpnlButtonBar = new InternalDesignerEditorButtonBarPanel();
        _membersPanel.SuspendLayout();
        _propertiesPanel.SuspendLayout();
        _navPanel.SuspendLayout();
        _navHost.SuspendLayout();
        _layout.SuspendLayout();
        ((ISupportInitialize)kpnlContent).BeginInit();
        kpnlContent.SuspendLayout();
        SuspendLayout();
        //
        // _membersLabel
        //
        _membersLabel.AutoSize = true;
        _membersLabel.Dock = DockStyle.Top;
        _membersLabel.Name = "_membersLabel";
        _membersLabel.Values.Text = @"Members:";
        //
        // _treeView
        //
        _treeView.Dock = DockStyle.Fill;
        _treeView.Name = "_treeView";
        //
        // _membersPanel
        //
        _membersPanel.Controls.Add(_treeView);
        _membersPanel.Controls.Add(_membersLabel);
        _membersPanel.Dock = DockStyle.Fill;
        _membersPanel.Margin = new Padding(0, 0, 8, 0);
        _membersPanel.Name = "_membersPanel";
        //
        // _propertiesLabel
        //
        _propertiesLabel.AutoSize = true;
        _propertiesLabel.Dock = DockStyle.Top;
        _propertiesLabel.Name = "_propertiesLabel";
        _propertiesLabel.Values.Text = @"Properties:";
        //
        // _propertyGrid
        //
        _propertyGrid.Dock = DockStyle.Fill;
        _propertyGrid.Name = "_propertyGrid";
        //
        // _propertiesPanel
        //
        _propertiesPanel.Controls.Add(_propertyGrid);
        _propertiesPanel.Controls.Add(_propertiesLabel);
        _propertiesPanel.Dock = DockStyle.Fill;
        _propertiesPanel.Margin = new Padding(0);
        _propertiesPanel.Name = "_propertiesPanel";
        //
        // _buttonAddRoot
        //
        _buttonAddRoot.Name = "_buttonAddRoot";
        _buttonAddRoot.Values.Text = @"Add Root";
        _buttonAddRoot.Click += (_, _) => AddNode(null);
        //
        // _buttonAddChild
        //
        _buttonAddChild.Name = "_buttonAddChild";
        _buttonAddChild.Values.Text = @"Add Child";
        _buttonAddChild.Click += (_, _) => AddNode(_selectedNode);
        //
        // _buttonDelete
        //
        _buttonDelete.Name = "_buttonDelete";
        _buttonDelete.Values.Text = @"Delete";
        _buttonDelete.Click += (_, _) => DeleteSelectedNode();
        //
        // _buttonMoveUp
        //
        _buttonMoveUp.Name = "_buttonMoveUp";
        _buttonMoveUp.Values.Text = @"Move Up";
        _buttonMoveUp.Click += (_, _) => MoveSelectedNode(true);
        //
        // _buttonMoveDown
        //
        _buttonMoveDown.Name = "_buttonMoveDown";
        _buttonMoveDown.Values.Text = @"Move Down";
        _buttonMoveDown.Click += (_, _) => MoveSelectedNode(false);
        //
        // _navPanel
        //
        _navPanel.AutoSize = true;
        _navPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _navPanel.BackColor = Color.Transparent;
        _navPanel.ColumnCount = 5;
        _navPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
        _navPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
        _navPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
        _navPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
        _navPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
        _navPanel.Controls.Add(_buttonAddRoot, 0, 0);
        _navPanel.Controls.Add(_buttonAddChild, 1, 0);
        _navPanel.Controls.Add(_buttonDelete, 2, 0);
        _navPanel.Controls.Add(_buttonMoveUp, 3, 0);
        _navPanel.Controls.Add(_buttonMoveDown, 4, 0);
        _navPanel.Dock = DockStyle.Bottom;
        _navPanel.Margin = new Padding(0, 6, 0, 0);
        _navPanel.Name = "_navPanel";
        _navPanel.RowCount = 1;
        //
        // _navHost
        //
        _navHost.Controls.Add(_navPanel);
        _navHost.Dock = DockStyle.Bottom;
        _navHost.Name = "_navHost";
        //
        // _layout
        //
        _layout.BackColor = Color.Transparent;
        _layout.ColumnCount = 2;
        _layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
        _layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
        _layout.Controls.Add(_membersPanel, 0, 0);
        _layout.Controls.Add(_propertiesPanel, 1, 0);
        _layout.Dock = DockStyle.Fill;
        _layout.Padding = new Padding(6);
        _layout.RowCount = 1;
        _layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _layout.Name = "_layout";
        //
        // kpnlContent
        //
        kpnlContent.Controls.Add(_layout);
        kpnlContent.Controls.Add(_navHost);
        kpnlContent.Dock = DockStyle.Fill;
        kpnlContent.Name = "kpnlContent";
        kpnlContent.Padding = new Padding(9);
        kpnlContent.PanelBackStyle = PaletteBackStyle.PanelClient;
        //
        // kpnlButtonBar
        //
        kpnlButtonBar.Dock = DockStyle.Bottom;
        kpnlButtonBar.Name = "kpnlButtonBar";
        //
        // VisualTreeNodeCollectionForm
        //
        Controls.Add(kpnlContent);
        Controls.Add(kpnlButtonBar);
        Name = nameof(VisualTreeNodeCollectionForm);
        _membersPanel.ResumeLayout(false);
        _membersPanel.PerformLayout();
        _propertiesPanel.ResumeLayout(false);
        _propertiesPanel.PerformLayout();
        _navPanel.ResumeLayout(false);
        _navHost.ResumeLayout(false);
        _layout.ResumeLayout(false);
        _layout.PerformLayout();
        ((ISupportInitialize)kpnlContent).EndInit();
        kpnlContent.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private KryptonLabel _membersLabel;
    private KryptonTreeView _treeView;
    private KryptonPanel _membersPanel;
    private KryptonLabel _propertiesLabel;
    private KryptonPropertyGrid _propertyGrid;
    private KryptonPanel _propertiesPanel;
    private KryptonButton _buttonAddRoot;
    private KryptonButton _buttonAddChild;
    private KryptonButton _buttonDelete;
    private KryptonButton _buttonMoveUp;
    private KryptonButton _buttonMoveDown;
    private TableLayoutPanel _navPanel;
    private KryptonPanel _navHost;
    private TableLayoutPanel _layout;
    private KryptonPanel kpnlContent;
    private InternalDesignerEditorButtonBarPanel kpnlButtonBar;
}

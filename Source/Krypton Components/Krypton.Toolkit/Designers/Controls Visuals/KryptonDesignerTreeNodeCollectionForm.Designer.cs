#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal partial class KryptonDesignerTreeNodeCollectionForm
{
    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
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
        _navPanel = new FlowLayoutPanel();
        _navHost = new KryptonPanel();
        _layout = new TableLayoutPanel();
        _buttonOk = new KryptonButton();
        _buttonCancel = new KryptonButton();
        _membersPanel.SuspendLayout();
        _propertiesPanel.SuspendLayout();
        _navPanel.SuspendLayout();
        _navHost.SuspendLayout();
        _layout.SuspendLayout();
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
        _treeView.AllowDrop = true;
        _treeView.Dock = DockStyle.Fill;
        _treeView.HideSelection = false;
        _treeView.Name = "_treeView";
        //
        // _membersPanel
        //
        _membersPanel.Controls.Add(_treeView);
        _membersPanel.Controls.Add(_membersLabel);
        _membersPanel.Dock = DockStyle.Fill;
        _membersPanel.Margin = new Padding(0, 0, 6, 0);
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
        _buttonMoveUp.Click += (_, _) => MoveSelectedNode(up: true);
        //
        // _buttonMoveDown
        //
        _buttonMoveDown.Name = "_buttonMoveDown";
        _buttonMoveDown.Values.Text = @"Move Down";
        _buttonMoveDown.Click += (_, _) => MoveSelectedNode(up: false);
        //
        // _navPanel
        //
        _navPanel.AutoSize = true;
        _navPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _navPanel.BackColor = Color.Transparent;
        _navPanel.Controls.Add(_buttonAddRoot);
        _navPanel.Controls.Add(_buttonAddChild);
        _navPanel.Controls.Add(_buttonDelete);
        _navPanel.Controls.Add(_buttonMoveUp);
        _navPanel.Controls.Add(_buttonMoveDown);
        _navPanel.Dock = DockStyle.Top;
        _navPanel.FlowDirection = FlowDirection.TopDown;
        _navPanel.Margin = new Padding(0);
        _navPanel.Padding = new Padding(0);
        _navPanel.WrapContents = false;
        _navPanel.Name = "_navPanel";
        //
        // _navHost
        //
        _navHost.Controls.Add(_navPanel);
        _navHost.Dock = DockStyle.Fill;
        _navHost.Margin = new Padding(0, 0, 6, 0);
        _navHost.Padding = new Padding(8, 18, 8, 8);
        _navHost.StateCommon.Color1 = Color.Transparent;
        _navHost.Name = "_navHost";
        //
        // _layout
        //
        _layout.BackColor = Color.Transparent;
        _layout.ColumnCount = 3;
        _layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
        _layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
        _layout.Controls.Add(_membersPanel, 0, 0);
        _layout.Controls.Add(_navHost, 1, 0);
        _layout.Controls.Add(_propertiesPanel, 2, 0);
        _layout.Dock = DockStyle.Fill;
        _layout.Padding = new Padding(6);
        _layout.RowCount = 1;
        _layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _layout.Name = "_layout";
        //
        // _buttonOk
        //
        _buttonOk.DialogResult = DialogResult.OK;
        _buttonOk.Name = "_buttonOk";
        _buttonOk.Values.Text = KryptonManager.Strings.GeneralStrings.OK;
        _buttonOk.Click += OnOkClick;
        //
        // _buttonCancel
        //
        _buttonCancel.DialogResult = DialogResult.Cancel;
        _buttonCancel.Name = "_buttonCancel";
        _buttonCancel.Values.Text = KryptonManager.Strings.GeneralStrings.Cancel;
        //
        // KryptonDesignerTreeNodeCollectionForm
        //
        Controls.Add(KryptonDesignerEditorContentPanel.Create(this, _layout));
        Controls.Add(KryptonDesignerEditorButtonBar.Create(this, _buttonOk, _buttonCancel));
        Name = nameof(KryptonDesignerTreeNodeCollectionForm);
        _membersPanel.ResumeLayout(false);
        _membersPanel.PerformLayout();
        _propertiesPanel.ResumeLayout(false);
        _propertiesPanel.PerformLayout();
        _navPanel.ResumeLayout(false);
        _navHost.ResumeLayout(false);
        _layout.ResumeLayout(false);
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
    private FlowLayoutPanel _navPanel;
    private KryptonPanel _navHost;
    private TableLayoutPanel _layout;
    private KryptonButton _buttonOk;
    private KryptonButton _buttonCancel;
}

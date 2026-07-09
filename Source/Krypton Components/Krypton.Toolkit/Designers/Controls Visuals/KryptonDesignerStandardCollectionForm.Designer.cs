#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal partial class KryptonDesignerStandardCollectionForm
{
    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        _membersLabel = new KryptonLabel();
        _propertiesLabel = new KryptonLabel();
        _listBox = new KryptonListBox();
        _propertyGrid = new KryptonPropertyGrid();
        _buttonAdd = new KryptonButton();
        _buttonRemove = new KryptonButton();
        _buttonOk = new KryptonButton();
        _buttonCancel = new KryptonButton();
        _membersPanel = new KryptonPanel();
        _propertiesPanel = new KryptonPanel();
        _buttonPanel = new TableLayoutPanel();
        _content = new TableLayoutPanel();
        _membersPanel.SuspendLayout();
        _propertiesPanel.SuspendLayout();
        _buttonPanel.SuspendLayout();
        _content.SuspendLayout();
        SuspendLayout();
        //
        // _membersLabel
        //
        _membersLabel.AutoSize = true;
        _membersLabel.Dock = DockStyle.Top;
        _membersLabel.Name = "_membersLabel";
        _membersLabel.Values.Text = @"Members:";
        //
        // _listBox
        //
        _listBox.Dock = DockStyle.Fill;
        _listBox.Name = "_listBox";
        //
        // _membersPanel
        //
        _membersPanel.Controls.Add(_listBox);
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
        // _buttonAdd
        //
        _buttonAdd.Name = "_buttonAdd";
        _buttonAdd.Values.Text = @"Add";
        _buttonAdd.Click += OnAddClick;
        //
        // _buttonRemove
        //
        _buttonRemove.Name = "_buttonRemove";
        _buttonRemove.Values.Text = @"Remove";
        _buttonRemove.Click += OnRemoveClick;
        //
        // _buttonPanel
        //
        _buttonPanel.AutoSize = true;
        _buttonPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _buttonPanel.BackColor = Color.Transparent;
        _buttonPanel.ColumnCount = 2;
        _buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        _buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        _buttonPanel.Controls.Add(_buttonAdd, 0, 0);
        _buttonPanel.Controls.Add(_buttonRemove, 1, 0);
        _buttonPanel.Dock = DockStyle.Bottom;
        _buttonPanel.Margin = new Padding(0, 6, 0, 0);
        _buttonPanel.Name = "_buttonPanel";
        _buttonPanel.RowCount = 1;
        //
        // _content
        //
        _content.BackColor = Color.Transparent;
        _content.ColumnCount = 2;
        _content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
        _content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
        _content.Controls.Add(_membersPanel, 0, 0);
        _content.Controls.Add(_propertiesPanel, 1, 0);
        _content.Controls.Add(_buttonPanel, 0, 1);
        _content.Dock = DockStyle.Fill;
        _content.Padding = new Padding(6);
        _content.RowCount = 2;
        _content.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _content.RowStyles.Add(new RowStyle());
        _content.Name = "_content";
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
        // KryptonDesignerStandardCollectionForm
        //
        Controls.Add(KryptonDesignerEditorContentPanel.Create(this, _content));
        Controls.Add(KryptonDesignerEditorButtonBar.Create(this, _buttonOk, _buttonCancel));
        Name = nameof(KryptonDesignerStandardCollectionForm);
        _membersPanel.ResumeLayout(false);
        _membersPanel.PerformLayout();
        _propertiesPanel.ResumeLayout(false);
        _propertiesPanel.PerformLayout();
        _buttonPanel.ResumeLayout(false);
        _content.ResumeLayout(false);
        _content.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private KryptonLabel _membersLabel;
    private KryptonLabel _propertiesLabel;
    private KryptonListBox _listBox;
    private KryptonPropertyGrid _propertyGrid;
    private KryptonButton _buttonAdd;
    private KryptonButton _buttonRemove;
    private KryptonButton _buttonOk;
    private KryptonButton _buttonCancel;
    private KryptonPanel _membersPanel;
    private KryptonPanel _propertiesPanel;
    private TableLayoutPanel _buttonPanel;
    private TableLayoutPanel _content;
}

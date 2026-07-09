#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

public partial class KryptonContextMenuCollectionEditor
{
    protected partial class KryptonContextMenuCollectionForm
    {
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new Container();
            _buttonOk = new KryptonButton();
            _buttonCancel = new KryptonButton();
            _treeView = new KryptonTreeView();
            _imageList = new ImageList(components);
            _label1 = new KryptonLabel();
            _buttonDelete = new KryptonButton();
            _buttonMoveUp = new KryptonButton();
            _buttonMoveDown = new KryptonButton();
            _buttonAddCheckBox = new KryptonButton();
            _buttonAddCheckButton = new KryptonButton();
            _buttonAddRadioButton = new KryptonButton();
            _buttonAddLinkLabel = new KryptonButton();
            _buttonAddSeparator = new KryptonButton();
            _buttonAddItem = new KryptonButton();
            _buttonAddItems = new KryptonButton();
            _buttonAddHeading = new KryptonButton();
            _buttonAddMonthCalendar = new KryptonButton();
            _propertyGrid1 = new KryptonPropertyGrid();
            _label2 = new KryptonLabel();
            _buttonAddColorColumns = new KryptonButton();
            _buttonAddImageSelect = new KryptonButton();
            _buttonAddComboBox = new KryptonButton();
            _buttonAddProgressBar = new KryptonButton();
            _tableLayoutPanel1 = new TableLayoutPanel();
            _panel1 = new KryptonPanel();
            _tableLayoutPanel1.SuspendLayout();
            _panel1.SuspendLayout();
            SuspendLayout();
            // 
            // buttonOK
            _buttonOk.DialogResult = DialogResult.OK;
            _buttonOk.Name = nameof(_buttonOk);
            _buttonOk.TabIndex = 16;
            _buttonOk.Values.Text = @"OK";
            _buttonOk.Click += buttonOK_Click;
            // 
            // buttonCancel
            // 
            _buttonCancel.DialogResult = DialogResult.Cancel;
            _buttonCancel.Name = nameof(_buttonCancel);
            _buttonCancel.TabIndex = 17;
            _buttonCancel.Values.Text = @"Cancel";
            _buttonCancel.Click += buttonCancel_Click;
            // 
            // treeView
            // 
            _treeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _treeView.HideSelection = false;
            _treeView.ImageIndex = 0;
            _treeView.ImageList = _imageList;
            _treeView.Location = new Point(16, 33);
            _treeView.Name = nameof(_treeView);
            _treeView.SelectedImageIndex = 0;
            _treeView.Size = new Size(320, 615);
            _treeView.TabIndex = 0;
            _treeView.AfterSelect += SelectionChanged;
            // 
            // imageList
            // 
            _imageList.TransparentColor = Color.Magenta;
            _imageList.Images.AddRange([
                GenericKryptonImageResources.KryptonContextMenuColorColumns,
                GenericKryptonImageResources.KryptonContextMenuHeading,
                GenericKryptonImageResources.KryptonContextMenuItem,
                GenericKryptonImageResources.KryptonContextMenuItems,
                GenericKryptonImageResources.KryptonContextMenuSeparator,
                GenericKryptonImageResources.KryptonRadioButton,
                GenericKryptonImageResources.KryptonCheckBox,
                GenericKryptonImageResources.KryptonCheckButton,
                GenericKryptonImageResources.KryptonLinkLabel,
                GenericImageResources.delete,
                BlueArrowResources.arrow_up_blue,
                BlueArrowResources.arrow_down_blue,
                GenericKryptonImageResources.KryptonContextMenuImageSelect,
                GenericKryptonImageResources.KryptonMonthCalendar,
                GenericKryptonImageResources.KryptonComboBox,
                GenericKryptonImageResources.KryptonTextBox,
                GenericKryptonImageResources.KryptonNumericUpDown
            ]);

            // TODO: Do these need updating?
            _imageList.Images.SetKeyName(0, "KryptonContextMenuColorColumns.bmp");
            _imageList.Images.SetKeyName(1, "KryptonContextMenuHeading.bmp");
            _imageList.Images.SetKeyName(2, "KryptonContextMenuItem.bmp");
            _imageList.Images.SetKeyName(3, "KryptonContextMenuItems.bmp");
            _imageList.Images.SetKeyName(4, "KryptonContextMenuSeparator.bmp");
            _imageList.Images.SetKeyName(5, "KryptonRadioButton.bmp");
            _imageList.Images.SetKeyName(6, "KryptonCheckBox.bmp");
            _imageList.Images.SetKeyName(7, "KryptonCheckButton.bmp");
            _imageList.Images.SetKeyName(8, "KryptonLinkLabel.bmp");
            _imageList.Images.SetKeyName(9, "delete.png");
            _imageList.Images.SetKeyName(10, "arrow_up_blue.png");
            _imageList.Images.SetKeyName(11, "arrow_down_blue.png");
            _imageList.Images.SetKeyName(12, "KryptonContextMenuImageSelect.bmp");
            _imageList.Images.SetKeyName(13, "KryptonContextMenuMonthCalendar.bmp");
            _imageList.Images.SetKeyName(14, "KryptonComboBox.bmp");
            _imageList.Images.SetKeyName(15, "KryptonTextBox.bmp");
            _imageList.Images.SetKeyName(16, "KryptonNumericUpDown.bmp");
            // 
            // label1
            // 
            _label1.AutoSize = true;
            _label1.Location = new Point(3, 0);
            _label1.Name = nameof(_label1);
            _label1.Size = new Size(120, 21);
            _label1.TabIndex = 7;
            _label1.Text = @"Item Hierarchy";
            // 
            // buttonDelete
            // 
            ConfigureImageListButton(_buttonDelete, _imageList, 9, @"Delete", buttonDelete_Click);
            _buttonDelete.Name = nameof(_buttonDelete);
            _buttonDelete.TabIndex = 14;
            // 
            // buttonMoveUp
            // 
            ConfigureImageListButton(_buttonMoveUp, _imageList, 10, @"Move Up", buttonMoveUp_Click);
            _buttonMoveUp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonMoveUp.Location = new Point(21, 29);
            _buttonMoveUp.Name = nameof(_buttonMoveUp);
            _buttonMoveUp.Size = new Size(184, 32);
            _buttonMoveUp.TabIndex = 1;
            // 
            // buttonMoveDown
            // 
            ConfigureImageListButton(_buttonMoveDown, _imageList, 11, @"Move Down", buttonMoveDown_Click);
            _buttonMoveDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonMoveDown.Location = new Point(21, 70);
            _buttonMoveDown.Name = nameof(_buttonMoveDown);
            _buttonMoveDown.Size = new Size(184, 32);
            _buttonMoveDown.TabIndex = 2;
            // 
            // buttonAddCheckBox
            // 
            ConfigureImageListButton(_buttonAddCheckBox, _imageList, 6, @"Add CheckBox", buttonAddCheckBox_Click);
            _buttonAddCheckBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddCheckBox.Location = new Point(21, 275);
            _buttonAddCheckBox.Name = nameof(_buttonAddCheckBox);
            _buttonAddCheckBox.Size = new Size(184, 32);
            _buttonAddCheckBox.TabIndex = 7;
            // 
            // buttonAddCheckButton
            // 
            ConfigureImageListButton(_buttonAddCheckButton, _imageList, 7, @"Add CheckButton", buttonAddCheckButton_Click);
            _buttonAddCheckButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddCheckButton.Location = new Point(21, 316);
            _buttonAddCheckButton.Name = nameof(_buttonAddCheckButton);
            _buttonAddCheckButton.Size = new Size(184, 32);
            _buttonAddCheckButton.TabIndex = 8;
            // 
            // buttonAddRadioButton
            // 
            ConfigureImageListButton(_buttonAddRadioButton, _imageList, 5, @"Add RadioButton", buttonAddRadioButton_Click);
            _buttonAddRadioButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddRadioButton.Location = new Point(21, 357);
            _buttonAddRadioButton.Name = nameof(_buttonAddRadioButton);
            _buttonAddRadioButton.Size = new Size(184, 32);
            _buttonAddRadioButton.TabIndex = 9;
            // 
            // buttonAddLinkLabel
            // 
            ConfigureImageListButton(_buttonAddLinkLabel, _imageList, 8, @"Add LinkLabel", buttonAddLinkLabel_Click);
            _buttonAddLinkLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddLinkLabel.Location = new Point(21, 398);
            _buttonAddLinkLabel.Name = nameof(_buttonAddLinkLabel);
            _buttonAddLinkLabel.Size = new Size(184, 32);
            _buttonAddLinkLabel.TabIndex = 10;
            // 
            // buttonAddSeparator
            // 
            ConfigureImageListButton(_buttonAddSeparator, _imageList, 4, @"Add Separator", buttonAddSeparator_Click);
            _buttonAddSeparator.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddSeparator.Location = new Point(21, 234);
            _buttonAddSeparator.Name = nameof(_buttonAddSeparator);
            _buttonAddSeparator.Size = new Size(184, 32);
            _buttonAddSeparator.TabIndex = 6;
            // 
            // buttonAddItem
            // 
            ConfigureImageListButton(_buttonAddItem, _imageList, 2, @"Add Item", buttonAddItem_Click);
            _buttonAddItem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddItem.Location = new Point(21, 111);
            _buttonAddItem.Name = nameof(_buttonAddItem);
            _buttonAddItem.Size = new Size(184, 32);
            _buttonAddItem.TabIndex = 3;
            // 
            // buttonAddItems
            // 
            ConfigureImageListButton(_buttonAddItems, _imageList, 3, @"Add Items", buttonAddItems_Click);
            _buttonAddItems.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddItems.Location = new Point(21, 152);
            _buttonAddItems.Name = nameof(_buttonAddItems);
            _buttonAddItems.Size = new Size(184, 32);
            _buttonAddItems.TabIndex = 4;
            // 
            // buttonAddHeading
            // 
            ConfigureImageListButton(_buttonAddHeading, _imageList, 1, @"Add Heading", buttonAddHeading_Click);
            _buttonAddHeading.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddHeading.Location = new Point(21, 193);
            _buttonAddHeading.Name = nameof(_buttonAddHeading);
            _buttonAddHeading.Size = new Size(184, 32);
            _buttonAddHeading.TabIndex = 5;
            // 
            // buttonAddMonthCalendar
            // 
            ConfigureImageListButton(_buttonAddMonthCalendar, _imageList, 13, @"Add Month Calendar", buttonAddMonthCalendar_Click);
            _buttonAddMonthCalendar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddMonthCalendar.Location = new Point(21, 521);
            _buttonAddMonthCalendar.Name = nameof(_buttonAddMonthCalendar);
            _buttonAddMonthCalendar.Size = new Size(184, 32);
            _buttonAddMonthCalendar.TabIndex = 13;
            // 
            // propertyGrid1
            // 
            _propertyGrid1.Dock = DockStyle.Fill;
            _propertyGrid1.HelpVisible = true;
            _propertyGrid1.Location = new Point(524, 24);
            _propertyGrid1.Name = "_propertyGrid1";
            _propertyGrid1.Size = new Size(289, 658);
            _propertyGrid1.TabIndex = 15;
            _propertyGrid1.ToolbarVisible = true;
            // 
            // label2
            // 
            _label2.AutoSize = true;
            _label2.Location = new Point(524, 0);
            _label2.Name = nameof(_label2);
            _label2.Size = new Size(125, 21);
            _label2.TabIndex = 16;
            _label2.Text = @"Item Properties";
            // 
            // buttonAddColorColumns
            // 
            ConfigureImageListButton(_buttonAddColorColumns, _imageList, 0, @"Add ColorColumns", buttonAddColorColumns_Click);
            _buttonAddColorColumns.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddColorColumns.Location = new Point(21, 439);
            _buttonAddColorColumns.Name = nameof(_buttonAddColorColumns);
            _buttonAddColorColumns.Size = new Size(184, 32);
            _buttonAddColorColumns.TabIndex = 11;
            // 
            // buttonAddImageSelect
            // 
            ConfigureImageListButton(_buttonAddImageSelect, _imageList, 12, @"Add ImageSelect", buttonAddImageSelect_Click);
            _buttonAddImageSelect.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddImageSelect.Location = new Point(21, 480);
            _buttonAddImageSelect.Name = nameof(_buttonAddImageSelect);
            _buttonAddImageSelect.Size = new Size(184, 32);
            _buttonAddImageSelect.TabIndex = 12;
            // 
            // buttonAddComboBox
            // 
            ConfigureImageListButton(_buttonAddComboBox, _imageList, 14, @"Add ComboBox", buttonAddComboBox_Click);
            _buttonAddComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddComboBox.Location = new Point(21, 562);
            _buttonAddComboBox.Name = nameof(_buttonAddComboBox);
            _buttonAddComboBox.Size = new Size(184, 32);
            _buttonAddComboBox.TabIndex = 14;
            // 
            // buttonAddProgressBar
            // 
            ConfigureImageListButton(_buttonAddProgressBar, _imageList, 16, @"Add ProgressBar", buttonAddProgressBar_Click);
            _buttonAddProgressBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _buttonAddProgressBar.Location = new Point(21, 603);
            _buttonAddProgressBar.Name = nameof(_buttonAddProgressBar);
            _buttonAddProgressBar.Size = new Size(184, 32);
            _buttonAddProgressBar.TabIndex = 18;
            // 
            // tableLayoutPanel1
            // 
            _tableLayoutPanel1.ColumnCount = 3;
            _tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 226F));
            _tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _tableLayoutPanel1.Controls.Add(_label1, 0, 0);
            _tableLayoutPanel1.Controls.Add(_label2, 2, 0);
            _tableLayoutPanel1.Controls.Add(_treeView, 0, 1);
            _tableLayoutPanel1.Controls.Add(_propertyGrid1, 2, 1);
            _tableLayoutPanel1.Controls.Add(_panel1, 1, 1);
            _tableLayoutPanel1.Dock = DockStyle.Fill;
            _tableLayoutPanel1.Location = new Point(0, 0);
            _tableLayoutPanel1.Name = "_tableLayoutPanel1";
            _tableLayoutPanel1.BackColor = Color.Transparent;
            _tableLayoutPanel1.RowCount = 2;
            _tableLayoutPanel1.RowStyles.Add(new RowStyle());
            _tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _tableLayoutPanel1.Size = new Size(816, 674);
            _tableLayoutPanel1.TabIndex = 17;
            // 
            // panel1
            // 
            _panel1.Controls.Add(_buttonMoveUp);
            _panel1.Controls.Add(_buttonAddProgressBar);
            _panel1.Controls.Add(_buttonAddComboBox);
            _panel1.Controls.Add(_buttonAddMonthCalendar);
            _panel1.Controls.Add(_buttonAddImageSelect);
            _panel1.Controls.Add(_buttonAddColorColumns);
            _panel1.Controls.Add(_buttonMoveDown);
            _panel1.Controls.Add(_buttonAddItem);
            _panel1.Controls.Add(_buttonAddHeading);
            _panel1.Controls.Add(_buttonAddLinkLabel);
            _panel1.Controls.Add(_buttonAddSeparator);
            _panel1.Controls.Add(_buttonAddRadioButton);
            _panel1.Controls.Add(_buttonAddItems);
            _panel1.Controls.Add(_buttonAddCheckButton);
            _panel1.Controls.Add(_buttonAddCheckBox);
            _panel1.Dock = DockStyle.Fill;
            _panel1.Location = new Point(298, 24);
            _panel1.Name = "_panel1";
            _panel1.Size = new Size(220, 658);
            _panel1.StateCommon.Color1 = Color.Transparent;
            _panel1.TabIndex = 17;
            // 
            // KryptonContextMenuCollectionForm
            // 
            AcceptButton = _buttonOk;
            CancelButton = _buttonCancel;
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(816, 724);
            Controls.Add(KryptonDesignerEditorContentPanel.Create(this, _tableLayoutPanel1));
            Controls.Add(KryptonDesignerEditorButtonBar.Create(this, _buttonOk, _buttonCancel, _buttonDelete));
            Font = new Font(@"Tahoma", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MinimumSize = new Size(733, 593);
            Name = nameof(KryptonContextMenuCollectionForm);
            Text = @"KryptonContextMenu Items Editor";
            _tableLayoutPanel1.ResumeLayout(false);
            _tableLayoutPanel1.PerformLayout();
            _panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ImageList _imageList;
        private KryptonButton _buttonOk;
        private KryptonButton _buttonCancel;
        private KryptonTreeView _treeView;
        private KryptonLabel _label1;
        private KryptonLabel _label2;
        private KryptonButton _buttonDelete;
        private KryptonButton _buttonMoveUp;
        private KryptonButton _buttonMoveDown;
        private KryptonButton _buttonAddCheckBox;
        private KryptonButton _buttonAddCheckButton;
        private KryptonButton _buttonAddRadioButton;
        private KryptonButton _buttonAddLinkLabel;
        private KryptonButton _buttonAddSeparator;
        private KryptonButton _buttonAddItem;
        private KryptonButton _buttonAddItems;
        private KryptonButton _buttonAddHeading;
        private KryptonButton _buttonAddMonthCalendar;
        private KryptonButton _buttonAddColorColumns;
        private KryptonButton _buttonAddImageSelect;
        private KryptonButton _buttonAddComboBox;
        private KryptonButton _buttonAddProgressBar;
        private KryptonPropertyGrid _propertyGrid1;
        private IContainer components;
        private TableLayoutPanel _tableLayoutPanel1;
        private KryptonPanel _panel1;
    }
}

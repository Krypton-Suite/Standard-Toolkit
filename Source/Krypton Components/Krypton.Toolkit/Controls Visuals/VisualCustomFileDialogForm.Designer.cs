namespace Krypton.Toolkit
{
    partial class VisualCustomFileDialogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._rootPanel = new Krypton.Toolkit.KryptonPanel();
            this._chromeLayout = new System.Windows.Forms.TableLayoutPanel();
            this._navigationLayout = new System.Windows.Forms.TableLayoutPanel();
            this._backButton = new Krypton.Toolkit.KryptonButton();
            this._forwardButton = new Krypton.Toolkit.KryptonButton();
            this._upButton = new Krypton.Toolkit.KryptonButton();
            this._refreshButton = new Krypton.Toolkit.KryptonButton();
            this._addressHost = new System.Windows.Forms.Panel();
            this._addressBar = new Krypton.Toolkit.KryptonBreadCrumb();
            this._addressEditBox = new Krypton.Toolkit.KryptonTextBox();
            this._viewButton = new Krypton.Toolkit.KryptonButton();
            this._searchLabel = new Krypton.Toolkit.KryptonLabel();
            this._searchTextBox = new Krypton.Toolkit.KryptonTextBox();
            this._splitContainer = new Krypton.Toolkit.KryptonSplitContainer();
            this._navigationTree = new Krypton.Toolkit.KryptonTreeView();
            this._fileList = new Krypton.Toolkit.KryptonListView();
            this._columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._columnType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._columnModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._columnSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._bottomLayout = new System.Windows.Forms.TableLayoutPanel();
            this._fileNameLabel = new Krypton.Toolkit.KryptonLabel();
            this._fileNameTextBox = new Krypton.Toolkit.KryptonTextBox();
            this._filterLabel = new Krypton.Toolkit.KryptonLabel();
            this._filterComboBox = new Krypton.Toolkit.KryptonComboBox();
            this._acceptButton = new Krypton.Toolkit.KryptonButton();
            this._cancelButton = new Krypton.Toolkit.KryptonButton();
            this._statusLabel = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this._rootPanel)).BeginInit();
            this._rootPanel.SuspendLayout();
            this._chromeLayout.SuspendLayout();
            this._navigationLayout.SuspendLayout();
            this._addressHost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._addressBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer.Panel1)).BeginInit();
            this._splitContainer.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer.Panel2)).BeginInit();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this._bottomLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._filterComboBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _rootPanel
            // 
            this._rootPanel.Controls.Add(this._chromeLayout);
            this._rootPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rootPanel.Location = new System.Drawing.Point(0, 0);
            this._rootPanel.Margin = new System.Windows.Forms.Padding(0);
            this._rootPanel.Name = "_rootPanel";
            this._rootPanel.Padding = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this._rootPanel.Size = new System.Drawing.Size(735, 552);
            this._rootPanel.TabIndex = 0;
            // 
            // _chromeLayout
            // 
            this._chromeLayout.BackColor = System.Drawing.Color.Transparent;
            this._chromeLayout.ColumnCount = 1;
            this._chromeLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._chromeLayout.Controls.Add(this._navigationLayout, 0, 0);
            this._chromeLayout.Controls.Add(this._splitContainer, 0, 1);
            this._chromeLayout.Controls.Add(this._bottomLayout, 0, 2);
            this._chromeLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._chromeLayout.Location = new System.Drawing.Point(9, 10);
            this._chromeLayout.Margin = new System.Windows.Forms.Padding(0);
            this._chromeLayout.Name = "_chromeLayout";
            this._chromeLayout.RowCount = 3;
            this._chromeLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._chromeLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._chromeLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._chromeLayout.Size = new System.Drawing.Size(717, 532);
            this._chromeLayout.TabIndex = 0;
            // 
            // _navigationLayout
            // 
            this._navigationLayout.AutoSize = true;
            this._navigationLayout.ColumnCount = 8;
            this._navigationLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._navigationLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._navigationLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._navigationLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._navigationLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._navigationLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._navigationLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._navigationLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 165F));
            this._navigationLayout.Controls.Add(this._backButton, 0, 0);
            this._navigationLayout.Controls.Add(this._forwardButton, 1, 0);
            this._navigationLayout.Controls.Add(this._upButton, 2, 0);
            this._navigationLayout.Controls.Add(this._refreshButton, 3, 0);
            this._navigationLayout.Controls.Add(this._addressHost, 4, 0);
            this._navigationLayout.Controls.Add(this._viewButton, 5, 0);
            this._navigationLayout.Controls.Add(this._searchLabel, 6, 0);
            this._navigationLayout.Controls.Add(this._searchTextBox, 7, 0);
            this._navigationLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._navigationLayout.Location = new System.Drawing.Point(0, 0);
            this._navigationLayout.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this._navigationLayout.Name = "_navigationLayout";
            this._navigationLayout.RowCount = 1;
            this._navigationLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._navigationLayout.Size = new System.Drawing.Size(717, 27);
            this._navigationLayout.TabIndex = 0;
            // 
            // _backButton
            // 
            this._backButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._backButton.AutoSize = true;
            this._backButton.Enabled = false;
            this._backButton.Location = new System.Drawing.Point(2, 11);
            this._backButton.Margin = new System.Windows.Forms.Padding(2);
            this._backButton.Name = "_backButton";
            this._backButton.Size = new System.Drawing.Size(90, 25);
            this._backButton.TabIndex = 0;
            this._backButton.ToolTipValues.EnableToolTips = true;
            this._backButton.ToolTipValues.Heading = "Back";
            this._backButton.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._backButton.Values.Text = "";
            this._backButton.Click += new System.EventHandler(this.OnBackButtonClick);
            // 
            // _forwardButton
            // 
            this._forwardButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._forwardButton.AutoSize = true;
            this._forwardButton.Enabled = false;
            this._forwardButton.Location = new System.Drawing.Point(10, 11);
            this._forwardButton.Margin = new System.Windows.Forms.Padding(2);
            this._forwardButton.Name = "_forwardButton";
            this._forwardButton.Size = new System.Drawing.Size(90, 25);
            this._forwardButton.TabIndex = 1;
            this._forwardButton.ToolTipValues.EnableToolTips = true;
            this._forwardButton.ToolTipValues.Heading = "Forward";
            this._forwardButton.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._forwardButton.Values.Text = "";
            this._forwardButton.Click += new System.EventHandler(this.OnForwardButtonClick);
            // 
            // _upButton
            // 
            this._upButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._upButton.AutoSize = true;
            this._upButton.Location = new System.Drawing.Point(18, 11);
            this._upButton.Margin = new System.Windows.Forms.Padding(2);
            this._upButton.Name = "_upButton";
            this._upButton.Size = new System.Drawing.Size(90, 25);
            this._upButton.TabIndex = 2;
            this._upButton.ToolTipValues.EnableToolTips = true;
            this._upButton.ToolTipValues.Heading = "Up";
            this._upButton.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._upButton.Values.Text = "";
            this._upButton.Click += new System.EventHandler(this.OnUpButtonClick);
            // 
            // _refreshButton
            // 
            this._refreshButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._refreshButton.AutoSize = true;
            this._refreshButton.Location = new System.Drawing.Point(26, 2);
            this._refreshButton.Margin = new System.Windows.Forms.Padding(2);
            this._refreshButton.Name = "_refreshButton";
            this._refreshButton.Size = new System.Drawing.Size(90, 25);
            this._refreshButton.TabIndex = 3;
            this._refreshButton.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._refreshButton.Values.Text = "Refresh";
            this._refreshButton.Click += new System.EventHandler(this.OnRefreshButtonClick);
            // 
            // _addressHost
            // 
            this._addressHost.Controls.Add(this._addressEditBox);
            this._addressHost.Controls.Add(this._addressBar);
            this._addressHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this._addressHost.Location = new System.Drawing.Point(77, 0);
            this._addressHost.Margin = new System.Windows.Forms.Padding(0);
            this._addressHost.MinimumSize = new System.Drawing.Size(120, 27);
            this._addressHost.Name = "_addressHost";
            this._addressHost.Size = new System.Drawing.Size(300, 27);
            this._addressHost.TabIndex = 4;
            // 
            // _addressBar
            // 
            this._addressBar.AutoSize = false;
            this._addressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this._addressBar.DropDownNavigation = false;
            this._addressBar.Location = new System.Drawing.Point(0, 0);
            this._addressBar.Margin = new System.Windows.Forms.Padding(0);
            this._addressBar.Name = "_addressBar";
            this._addressBar.RootItem.ShortText = "Root";
            this._addressBar.SelectedItem = this._addressBar.RootItem;
            this._addressBar.Size = new System.Drawing.Size(300, 27);
            this._addressBar.TabIndex = 0;
            this._addressBar.ToolTipValues.Description = "Click empty space, press Ctrl+L, or F4 to type a path.";
            this._addressBar.ToolTipValues.EnableToolTips = true;
            this._addressBar.ToolTipValues.Heading = "Address";
            this._addressBar.SelectedItemChanged += new System.EventHandler(this.OnAddressBarSelectedItemChanged);
            this._addressBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnAddressBarMouseUp);
            // 
            // _addressEditBox
            // 
            this._addressEditBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._addressEditBox.Location = new System.Drawing.Point(0, 0);
            this._addressEditBox.Margin = new System.Windows.Forms.Padding(0);
            this._addressEditBox.Name = "_addressEditBox";
            this._addressEditBox.Size = new System.Drawing.Size(300, 23);
            this._addressEditBox.TabIndex = 1;
            this._addressEditBox.Visible = false;
            this._addressEditBox.TextChanged += new System.EventHandler(this.OnAddressEditBoxTextChanged);
            this._addressEditBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnAddressEditBoxKeyDown);
            this._addressEditBox.LostFocus += new System.EventHandler(this.OnAddressEditBoxLostFocus);
            // 
            // _viewButton
            // 
            this._viewButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._viewButton.AutoSize = true;
            this._viewButton.Location = new System.Drawing.Point(383, 1);
            this._viewButton.Margin = new System.Windows.Forms.Padding(4, 1, 2, 1);
            this._viewButton.MinimumSize = new System.Drawing.Size(110, 25);
            this._viewButton.Name = "_viewButton";
            this._viewButton.ShowSplitOption = true;
            this._viewButton.Size = new System.Drawing.Size(120, 25);
            this._viewButton.TabIndex = 5;
            this._viewButton.ToolTipValues.EnableToolTips = true;
            this._viewButton.ToolTipValues.Heading = "Change view";
            this._viewButton.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._viewButton.Values.ShowSplitOption = true;
            this._viewButton.Values.Text = "Details";
            this._viewButton.Click += new System.EventHandler(this.OnViewButtonClick);
            // 
            // _searchLabel
            // 
            this._searchLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._searchLabel.Location = new System.Drawing.Point(511, 3);
            this._searchLabel.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this._searchLabel.Name = "_searchLabel";
            this._searchLabel.Size = new System.Drawing.Size(49, 20);
            this._searchLabel.TabIndex = 6;
            this._searchLabel.Values.Text = "Search:";
            // 
            // _searchTextBox
            // 
            this._searchTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._searchTextBox.Location = new System.Drawing.Point(554, 2);
            this._searchTextBox.Margin = new System.Windows.Forms.Padding(2);
            this._searchTextBox.Name = "_searchTextBox";
            this._searchTextBox.Size = new System.Drawing.Size(161, 23);
            this._searchTextBox.TabIndex = 7;
            this._searchTextBox.TextChanged += new System.EventHandler(this.OnSearchTextChanged);
            this._searchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnSearchKeyDown);
            // 
            // _splitContainer
            // 
            this._splitContainer.Cursor = System.Windows.Forms.Cursors.Default;
            this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this._splitContainer.Location = new System.Drawing.Point(0, 33);
            this._splitContainer.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.Controls.Add(this._navigationTree);
            // 
            // _splitContainer.Panel2
            // 
            this._splitContainer.Panel2.Controls.Add(this._fileList);
            this._splitContainer.Size = new System.Drawing.Size(717, 401);
            this._splitContainer.SplitterDistance = 240;
            this._splitContainer.TabIndex = 2;
            // 
            // _navigationTree
            // 
            this._navigationTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this._navigationTree.FullRowSelect = true;
            this._navigationTree.HideSelection = false;
            this._navigationTree.Location = new System.Drawing.Point(0, 0);
            this._navigationTree.Margin = new System.Windows.Forms.Padding(2);
            this._navigationTree.Name = "_navigationTree";
            this._navigationTree.Size = new System.Drawing.Size(240, 401);
            this._navigationTree.TabIndex = 0;
            this._navigationTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.OnNavigationBeforeExpand);
            this._navigationTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnNavigationAfterSelect);
            // 
            // _fileList
            // 
            this._fileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._columnName,
            this._columnType,
            this._columnModified,
            this._columnSize});
            this._fileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fileList.FullRowSelect = true;
            this._fileList.HideSelection = false;
            this._fileList.Location = new System.Drawing.Point(0, 0);
            this._fileList.Margin = new System.Windows.Forms.Padding(2);
            this._fileList.MultiSelect = false;
            this._fileList.Name = "_fileList";
            this._fileList.Size = new System.Drawing.Size(472, 401);
            this._fileList.TabIndex = 0;
            this._fileList.View = System.Windows.Forms.View.Details;
            this._fileList.ItemActivate += new System.EventHandler(this.OnFileListItemActivate);
            this._fileList.SelectedIndexChanged += new System.EventHandler(this.OnFileListSelectedIndexChanged);
            // 
            // _columnName
            // 
            this._columnName.Text = "Name";
            this._columnName.Width = 280;
            // 
            // _columnType
            // 
            this._columnType.Text = "Type";
            this._columnType.Width = 120;
            // 
            // _columnModified
            // 
            this._columnModified.Text = "Modified";
            this._columnModified.Width = 180;
            // 
            // _columnSize
            // 
            this._columnSize.Text = "Size";
            this._columnSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._columnSize.Width = 120;
            // 
            // _bottomLayout
            // 
            this._bottomLayout.AutoSize = true;
            this._bottomLayout.ColumnCount = 4;
            this._bottomLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._bottomLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._bottomLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._bottomLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._bottomLayout.Controls.Add(this._fileNameLabel, 0, 0);
            this._bottomLayout.Controls.Add(this._fileNameTextBox, 1, 0);
            this._bottomLayout.Controls.Add(this._filterLabel, 0, 1);
            this._bottomLayout.Controls.Add(this._filterComboBox, 1, 1);
            this._bottomLayout.Controls.Add(this._acceptButton, 2, 1);
            this._bottomLayout.Controls.Add(this._cancelButton, 3, 1);
            this._bottomLayout.Controls.Add(this._statusLabel, 0, 2);
            this._bottomLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._bottomLayout.Location = new System.Drawing.Point(0, 473);
            this._bottomLayout.Margin = new System.Windows.Forms.Padding(0);
            this._bottomLayout.Name = "_bottomLayout";
            this._bottomLayout.RowCount = 3;
            this._bottomLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._bottomLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._bottomLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._bottomLayout.Size = new System.Drawing.Size(717, 59);
            this._bottomLayout.TabIndex = 3;
            // 
            // _fileNameLabel
            // 
            this._fileNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._fileNameLabel.Location = new System.Drawing.Point(2, 3);
            this._fileNameLabel.Margin = new System.Windows.Forms.Padding(2);
            this._fileNameLabel.Name = "_fileNameLabel";
            this._fileNameLabel.Size = new System.Drawing.Size(65, 20);
            this._fileNameLabel.TabIndex = 0;
            this._fileNameLabel.Values.Text = "File name:";
            // 
            // _fileNameTextBox
            // 
            this._bottomLayout.SetColumnSpan(this._fileNameTextBox, 3);
            this._fileNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fileNameTextBox.Location = new System.Drawing.Point(71, 2);
            this._fileNameTextBox.Margin = new System.Windows.Forms.Padding(2);
            this._fileNameTextBox.Name = "_fileNameTextBox";
            this._fileNameTextBox.Size = new System.Drawing.Size(644, 23);
            this._fileNameTextBox.TabIndex = 1;
            this._fileNameTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnFileNameTextBoxKeyDown);
            // 
            // _filterLabel
            // 
            this._filterLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._filterLabel.Location = new System.Drawing.Point(2, 30);
            this._filterLabel.Margin = new System.Windows.Forms.Padding(2);
            this._filterLabel.Name = "_filterLabel";
            this._filterLabel.Size = new System.Drawing.Size(40, 20);
            this._filterLabel.TabIndex = 2;
            this._filterLabel.Values.Text = "Filter:";
            // 
            // _filterComboBox
            // 
            this._filterComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._filterComboBox.DropDownWidth = 121;
            this._filterComboBox.IntegralHeight = false;
            this._filterComboBox.Location = new System.Drawing.Point(71, 29);
            this._filterComboBox.Margin = new System.Windows.Forms.Padding(2);
            this._filterComboBox.Name = "_filterComboBox";
            this._filterComboBox.Size = new System.Drawing.Size(552, 22);
            this._filterComboBox.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this._filterComboBox.TabIndex = 3;
            this._filterComboBox.SelectedIndexChanged += new System.EventHandler(this.OnFilterSelectedIndexChanged);
            // 
            // _acceptButton
            // 
            this._acceptButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._acceptButton.AutoSize = true;
            this._acceptButton.Location = new System.Drawing.Point(627, 29);
            this._acceptButton.Margin = new System.Windows.Forms.Padding(2);
            this._acceptButton.Name = "_acceptButton";
            this._acceptButton.Size = new System.Drawing.Size(90, 25);
            this._acceptButton.TabIndex = 4;
            this._acceptButton.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._acceptButton.Values.Text = "Open";
            this._acceptButton.Click += new System.EventHandler(this.OnAcceptButtonClick);
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._cancelButton.AutoSize = true;
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(670, 29);
            this._cancelButton.Margin = new System.Windows.Forms.Padding(2);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(90, 25);
            this._cancelButton.TabIndex = 5;
            this._cancelButton.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._cancelButton.Values.Text = "Cancel";
            this._cancelButton.Click += new System.EventHandler(this.OnCancelButtonClick);
            // 
            // _statusLabel
            // 
            this._bottomLayout.SetColumnSpan(this._statusLabel, 4);
            this._statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._statusLabel.Location = new System.Drawing.Point(2, 55);
            this._statusLabel.Margin = new System.Windows.Forms.Padding(2);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(713, 2);
            this._statusLabel.TabIndex = 6;
            this._statusLabel.Values.Text = "";
            // 
            // VisualCustomFileDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 533);
            this.Controls.Add(this._rootPanel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(679, 495);
            this.Name = "VisualCustomFileDialogForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.KeyPreview = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnFormKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this._rootPanel)).EndInit();
            this._rootPanel.ResumeLayout(false);
            this._chromeLayout.ResumeLayout(false);
            this._chromeLayout.PerformLayout();
            this._navigationLayout.ResumeLayout(false);
            this._navigationLayout.PerformLayout();
            this._addressHost.ResumeLayout(false);
            this._addressHost.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._addressBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer.Panel1)).EndInit();
            this._splitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer.Panel2)).EndInit();
            this._splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
            this._splitContainer.ResumeLayout(false);
            this._bottomLayout.ResumeLayout(false);
            this._bottomLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._filterComboBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel _rootPanel;
        private TableLayoutPanel _chromeLayout;
        private TableLayoutPanel _navigationLayout;
        private KryptonButton _backButton;
        private KryptonButton _forwardButton;
        private KryptonButton _upButton;
        private KryptonButton _refreshButton;
        private Panel _addressHost;
        private KryptonBreadCrumb _addressBar;
        private KryptonTextBox _addressEditBox;
        private KryptonButton _viewButton;
        private KryptonLabel _searchLabel;
        private KryptonTextBox _searchTextBox;
        private KryptonSplitContainer _splitContainer;
        private KryptonTreeView _navigationTree;
        private KryptonListView _fileList;
        private ColumnHeader _columnName;
        private ColumnHeader _columnType;
        private ColumnHeader _columnModified;
        private ColumnHeader _columnSize;
        private TableLayoutPanel _bottomLayout;
        private KryptonLabel _fileNameLabel;
        private KryptonTextBox _fileNameTextBox;
        private KryptonLabel _filterLabel;
        private KryptonComboBox _filterComboBox;
        private KryptonButton _acceptButton;
        private KryptonButton _cancelButton;
        private KryptonLabel _statusLabel;
    }
}

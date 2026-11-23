#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class BindingNavigatorDemo
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
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonBindingNavigator1 = new Krypton.Toolkit.KryptonBindingNavigator();
            this.pnlDetails = new Krypton.Toolkit.KryptonPanel();
            this.klblCurrentItem = new Krypton.Toolkit.KryptonLabel();
            this.knudAge = new Krypton.Toolkit.KryptonNumericUpDown();
            this.klblAge = new Krypton.Toolkit.KryptonLabel();
            this.ktxtEmail = new Krypton.Toolkit.KryptonTextBox();
            this.klblEmail = new Krypton.Toolkit.KryptonLabel();
            this.ktxtLastName = new Krypton.Toolkit.KryptonTextBox();
            this.klblLastName = new Krypton.Toolkit.KryptonLabel();
            this.ktxtFirstName = new Krypton.Toolkit.KryptonTextBox();
            this.klblFirstName = new Krypton.Toolkit.KryptonLabel();
            this.pnlOptions = new Krypton.Toolkit.KryptonPanel();
            this.kbtnTestManyItems = new Krypton.Toolkit.KryptonButton();
            this.kbtnTestSingleItem = new Krypton.Toolkit.KryptonButton();
            this.kbtnTestEmptyList = new Krypton.Toolkit.KryptonButton();
            this.kbtnRefreshItems = new Krypton.Toolkit.KryptonButton();
            this.kbtnLoadSampleData = new Krypton.Toolkit.KryptonButton();
            this.kbtnClearAll = new Krypton.Toolkit.KryptonButton();
            this.kbtnDeleteCurrent = new Krypton.Toolkit.KryptonButton();
            this.kbtnAddNew = new Krypton.Toolkit.KryptonButton();
            this.kchkDeleteEnabled = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkAddNewEnabled = new Krypton.Toolkit.KryptonCheckBox();
            this.kdgvMain = new Krypton.Toolkit.KryptonDataGridView();
            this.colId = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.colFirstName = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.colLastName = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.colEmail = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.colAge = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlDetails)).BeginInit();
            this.pnlDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlOptions)).BeginInit();
            this.pnlOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kdgvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonBindingNavigator1);
            this.kryptonPanel1.Controls.Add(this.pnlDetails);
            this.kryptonPanel1.Controls.Add(this.pnlOptions);
            this.kryptonPanel1.Controls.Add(this.kdgvMain);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(1000, 650);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonBindingNavigator1
            // 
            this.kryptonBindingNavigator1.Location = new System.Drawing.Point(12, 12);
            this.kryptonBindingNavigator1.Name = "kryptonBindingNavigator1";
            this.kryptonBindingNavigator1.Size = new System.Drawing.Size(976, 33);
            this.kryptonBindingNavigator1.TabIndex = 0;
            this.kryptonBindingNavigator1.RefreshItems += new System.EventHandler(this.kryptonBindingNavigator1_RefreshItems);
            // 
            // pnlDetails
            // 
            this.pnlDetails.Controls.Add(this.klblCurrentItem);
            this.pnlDetails.Controls.Add(this.knudAge);
            this.pnlDetails.Controls.Add(this.klblAge);
            this.pnlDetails.Controls.Add(this.ktxtEmail);
            this.pnlDetails.Controls.Add(this.klblEmail);
            this.pnlDetails.Controls.Add(this.ktxtLastName);
            this.pnlDetails.Controls.Add(this.klblLastName);
            this.pnlDetails.Controls.Add(this.ktxtFirstName);
            this.pnlDetails.Controls.Add(this.klblFirstName);
            this.pnlDetails.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDetails.Location = new System.Drawing.Point(0, 450);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Padding = new System.Windows.Forms.Padding(12);
            this.pnlDetails.Size = new System.Drawing.Size(1000, 120);
            this.pnlDetails.TabIndex = 2;
            // 
            // klblCurrentItem
            // 
            this.klblCurrentItem.Location = new System.Drawing.Point(12, 12);
            this.klblCurrentItem.Name = "klblCurrentItem";
            this.klblCurrentItem.Size = new System.Drawing.Size(200, 20);
            this.klblCurrentItem.TabIndex = 8;
            this.klblCurrentItem.Values.Text = "Current: None";
            // 
            // knudAge
            // 
            this.knudAge.Location = new System.Drawing.Point(700, 50);
            this.knudAge.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.knudAge.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.knudAge.Name = "knudAge";
            this.knudAge.Size = new System.Drawing.Size(120, 22);
            this.knudAge.TabIndex = 7;
            // 
            // klblAge
            // 
            this.klblAge.Location = new System.Drawing.Point(650, 52);
            this.klblAge.Name = "klblAge";
            this.klblAge.Size = new System.Drawing.Size(35, 20);
            this.klblAge.TabIndex = 6;
            this.klblAge.Values.Text = "Age:";
            // 
            // ktxtEmail
            // 
            this.ktxtEmail.Location = new System.Drawing.Point(350, 50);
            this.ktxtEmail.Name = "ktxtEmail";
            this.ktxtEmail.Size = new System.Drawing.Size(280, 23);
            this.ktxtEmail.TabIndex = 5;
            // 
            // klblEmail
            // 
            this.klblEmail.Location = new System.Drawing.Point(300, 52);
            this.klblEmail.Name = "klblEmail";
            this.klblEmail.Size = new System.Drawing.Size(44, 20);
            this.klblEmail.TabIndex = 4;
            this.klblEmail.Values.Text = "Email:";
            // 
            // ktxtLastName
            // 
            this.ktxtLastName.Location = new System.Drawing.Point(100, 50);
            this.ktxtLastName.Name = "ktxtLastName";
            this.ktxtLastName.Size = new System.Drawing.Size(180, 23);
            this.ktxtLastName.TabIndex = 3;
            // 
            // klblLastName
            // 
            this.klblLastName.Location = new System.Drawing.Point(12, 52);
            this.klblLastName.Name = "klblLastName";
            this.klblLastName.Size = new System.Drawing.Size(70, 20);
            this.klblLastName.TabIndex = 2;
            this.klblLastName.Values.Text = "Last Name:";
            // 
            // ktxtFirstName
            // 
            this.ktxtFirstName.Location = new System.Drawing.Point(100, 80);
            this.ktxtFirstName.Name = "ktxtFirstName";
            this.ktxtFirstName.Size = new System.Drawing.Size(180, 23);
            this.ktxtFirstName.TabIndex = 1;
            // 
            // klblFirstName
            // 
            this.klblFirstName.Location = new System.Drawing.Point(12, 82);
            this.klblFirstName.Name = "klblFirstName";
            this.klblFirstName.Size = new System.Drawing.Size(71, 20);
            this.klblFirstName.TabIndex = 0;
            this.klblFirstName.Values.Text = "First Name:";
            // 
            // pnlOptions
            // 
            this.pnlOptions.Controls.Add(this.kbtnTestManyItems);
            this.pnlOptions.Controls.Add(this.kbtnTestSingleItem);
            this.pnlOptions.Controls.Add(this.kbtnTestEmptyList);
            this.pnlOptions.Controls.Add(this.kbtnRefreshItems);
            this.pnlOptions.Controls.Add(this.kbtnLoadSampleData);
            this.pnlOptions.Controls.Add(this.kbtnClearAll);
            this.pnlOptions.Controls.Add(this.kbtnDeleteCurrent);
            this.pnlOptions.Controls.Add(this.kbtnAddNew);
            this.pnlOptions.Controls.Add(this.kchkDeleteEnabled);
            this.pnlOptions.Controls.Add(this.kchkAddNewEnabled);
            this.pnlOptions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOptions.Location = new System.Drawing.Point(0, 570);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Padding = new System.Windows.Forms.Padding(12);
            this.pnlOptions.Size = new System.Drawing.Size(1000, 80);
            this.pnlOptions.TabIndex = 1;
            // 
            // kbtnTestManyItems
            // 
            this.kbtnTestManyItems.Location = new System.Drawing.Point(900, 45);
            this.kbtnTestManyItems.Name = "kbtnTestManyItems";
            this.kbtnTestManyItems.Size = new System.Drawing.Size(88, 25);
            this.kbtnTestManyItems.TabIndex = 9;
            this.kbtnTestManyItems.Values.Text = "Test 50 Items";
            this.kbtnTestManyItems.Click += new System.EventHandler(this.kbtnTestManyItems_Click);
            // 
            // kbtnTestSingleItem
            // 
            this.kbtnTestSingleItem.Location = new System.Drawing.Point(800, 45);
            this.kbtnTestSingleItem.Name = "kbtnTestSingleItem";
            this.kbtnTestSingleItem.Size = new System.Drawing.Size(88, 25);
            this.kbtnTestSingleItem.TabIndex = 8;
            this.kbtnTestSingleItem.Values.Text = "Test 1 Item";
            this.kbtnTestSingleItem.Click += new System.EventHandler(this.kbtnTestSingleItem_Click);
            // 
            // kbtnTestEmptyList
            // 
            this.kbtnTestEmptyList.Location = new System.Drawing.Point(700, 45);
            this.kbtnTestEmptyList.Name = "kbtnTestEmptyList";
            this.kbtnTestEmptyList.Size = new System.Drawing.Size(88, 25);
            this.kbtnTestEmptyList.TabIndex = 7;
            this.kbtnTestEmptyList.Values.Text = "Test Empty";
            this.kbtnTestEmptyList.Click += new System.EventHandler(this.kbtnTestEmptyList_Click);
            // 
            // kbtnRefreshItems
            // 
            this.kbtnRefreshItems.Location = new System.Drawing.Point(600, 45);
            this.kbtnRefreshItems.Name = "kbtnRefreshItems";
            this.kbtnRefreshItems.Size = new System.Drawing.Size(88, 25);
            this.kbtnRefreshItems.TabIndex = 6;
            this.kbtnRefreshItems.Values.Text = "Refresh Items";
            this.kbtnRefreshItems.Click += new System.EventHandler(this.kbtnRefreshItems_Click);
            // 
            // kbtnLoadSampleData
            // 
            this.kbtnLoadSampleData.Location = new System.Drawing.Point(500, 45);
            this.kbtnLoadSampleData.Name = "kbtnLoadSampleData";
            this.kbtnLoadSampleData.Size = new System.Drawing.Size(88, 25);
            this.kbtnLoadSampleData.TabIndex = 5;
            this.kbtnLoadSampleData.Values.Text = "Load Sample";
            this.kbtnLoadSampleData.Click += new System.EventHandler(this.kbtnLoadSampleData_Click);
            // 
            // kbtnClearAll
            // 
            this.kbtnClearAll.Location = new System.Drawing.Point(400, 45);
            this.kbtnClearAll.Name = "kbtnClearAll";
            this.kbtnClearAll.Size = new System.Drawing.Size(88, 25);
            this.kbtnClearAll.TabIndex = 4;
            this.kbtnClearAll.Values.Text = "Clear All";
            this.kbtnClearAll.Click += new System.EventHandler(this.kbtnClearAll_Click);
            // 
            // kbtnDeleteCurrent
            // 
            this.kbtnDeleteCurrent.Location = new System.Drawing.Point(300, 45);
            this.kbtnDeleteCurrent.Name = "kbtnDeleteCurrent";
            this.kbtnDeleteCurrent.Size = new System.Drawing.Size(88, 25);
            this.kbtnDeleteCurrent.TabIndex = 3;
            this.kbtnDeleteCurrent.Values.Text = "Delete Current";
            this.kbtnDeleteCurrent.Click += new System.EventHandler(this.kbtnDeleteCurrent_Click);
            // 
            // kbtnAddNew
            // 
            this.kbtnAddNew.Location = new System.Drawing.Point(200, 45);
            this.kbtnAddNew.Name = "kbtnAddNew";
            this.kbtnAddNew.Size = new System.Drawing.Size(88, 25);
            this.kbtnAddNew.TabIndex = 2;
            this.kbtnAddNew.Values.Text = "Add New";
            this.kbtnAddNew.Click += new System.EventHandler(this.kbtnAddNew_Click);
            // 
            // kchkDeleteEnabled
            // 
            this.kchkDeleteEnabled.Checked = true;
            this.kchkDeleteEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkDeleteEnabled.Location = new System.Drawing.Point(12, 45);
            this.kchkDeleteEnabled.Name = "kchkDeleteEnabled";
            this.kchkDeleteEnabled.Size = new System.Drawing.Size(120, 20);
            this.kchkDeleteEnabled.TabIndex = 1;
            this.kchkDeleteEnabled.Values.Text = "Delete Enabled";
            this.kchkDeleteEnabled.CheckedChanged += new System.EventHandler(this.kchkDeleteEnabled_CheckedChanged);
            // 
            // kchkAddNewEnabled
            // 
            this.kchkAddNewEnabled.Checked = true;
            this.kchkAddNewEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkAddNewEnabled.Location = new System.Drawing.Point(12, 15);
            this.kchkAddNewEnabled.Name = "kchkAddNewEnabled";
            this.kchkAddNewEnabled.Size = new System.Drawing.Size(120, 20);
            this.kchkAddNewEnabled.TabIndex = 0;
            this.kchkAddNewEnabled.Values.Text = "Add New Enabled";
            this.kchkAddNewEnabled.CheckedChanged += new System.EventHandler(this.kchkAddNewEnabled_CheckedChanged);
            // 
            // kdgvMain
            // 
            this.kdgvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kdgvMain.AutoGenerateColumns = false;
            this.kdgvMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.kdgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colFirstName,
            this.colLastName,
            this.colEmail,
            this.colAge});
            this.kdgvMain.Location = new System.Drawing.Point(12, 55);
            this.kdgvMain.Name = "kdgvMain";
            this.kdgvMain.Size = new System.Drawing.Size(976, 380);
            this.kdgvMain.TabIndex = 3;
            // 
            // colId
            // 
            this.colId.DataPropertyName = "Id";
            this.colId.HeaderText = "ID";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            // 
            // colFirstName
            // 
            this.colFirstName.DataPropertyName = "FirstName";
            this.colFirstName.HeaderText = "First Name";
            this.colFirstName.Name = "colFirstName";
            // 
            // colLastName
            // 
            this.colLastName.DataPropertyName = "LastName";
            this.colLastName.HeaderText = "Last Name";
            this.colLastName.Name = "colLastName";
            // 
            // colEmail
            // 
            this.colEmail.DataPropertyName = "Email";
            this.colEmail.HeaderText = "Email";
            this.colEmail.Name = "colEmail";
            // 
            // colAge
            // 
            this.colAge.DataPropertyName = "Age";
            this.colAge.HeaderText = "Age";
            this.colAge.Name = "colAge";
            // 
            // BindingNavigatorDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "BindingNavigatorDemo";
            this.Text = "KryptonBindingNavigator Demo";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlDetails)).EndInit();
            this.pnlDetails.ResumeLayout(false);
            this.pnlDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlOptions)).EndInit();
            this.pnlOptions.ResumeLayout(false);
            this.pnlOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kdgvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonBindingNavigator kryptonBindingNavigator1;
        private Krypton.Toolkit.KryptonDataGridView kdgvMain;
        private Krypton.Toolkit.KryptonPanel pnlDetails;
        private Krypton.Toolkit.KryptonTextBox ktxtFirstName;
        private Krypton.Toolkit.KryptonLabel klblFirstName;
        private Krypton.Toolkit.KryptonTextBox ktxtLastName;
        private Krypton.Toolkit.KryptonLabel klblLastName;
        private Krypton.Toolkit.KryptonTextBox ktxtEmail;
        private Krypton.Toolkit.KryptonLabel klblEmail;
        private Krypton.Toolkit.KryptonNumericUpDown knudAge;
        private Krypton.Toolkit.KryptonLabel klblAge;
        private Krypton.Toolkit.KryptonLabel klblCurrentItem;
        private Krypton.Toolkit.KryptonPanel pnlOptions;
        private Krypton.Toolkit.KryptonCheckBox kchkAddNewEnabled;
        private Krypton.Toolkit.KryptonCheckBox kchkDeleteEnabled;
        private Krypton.Toolkit.KryptonButton kbtnAddNew;
        private Krypton.Toolkit.KryptonButton kbtnDeleteCurrent;
        private Krypton.Toolkit.KryptonButton kbtnClearAll;
        private Krypton.Toolkit.KryptonButton kbtnLoadSampleData;
        private Krypton.Toolkit.KryptonButton kbtnRefreshItems;
        private Krypton.Toolkit.KryptonButton kbtnTestEmptyList;
        private Krypton.Toolkit.KryptonButton kbtnTestSingleItem;
        private Krypton.Toolkit.KryptonButton kbtnTestManyItems;
        private Krypton.Toolkit.KryptonDataGridViewTextBoxColumn colId;
        private Krypton.Toolkit.KryptonDataGridViewTextBoxColumn colFirstName;
        private Krypton.Toolkit.KryptonDataGridViewTextBoxColumn colLastName;
        private Krypton.Toolkit.KryptonDataGridViewTextBoxColumn colEmail;
        private Krypton.Toolkit.KryptonDataGridViewTextBoxColumn colAge;
    }
}


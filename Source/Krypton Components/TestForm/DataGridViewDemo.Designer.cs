#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class DataGridViewDemo
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
            Krypton.Toolkit.KryptonDataGridViewIconColumn.ColumnButtonSpec columnButtonSpec1 = new Krypton.Toolkit.KryptonDataGridViewIconColumn.ColumnButtonSpec();
            Krypton.Toolkit.KryptonDataGridViewIconColumn.ColumnButtonSpec columnButtonSpec2 = new Krypton.Toolkit.KryptonDataGridViewIconColumn.ColumnButtonSpec();
            Krypton.Toolkit.KryptonDataGridViewIconColumn.ColumnButtonSpec columnButtonSpec3 = new Krypton.Toolkit.KryptonDataGridViewIconColumn.ColumnButtonSpec();
            Krypton.Toolkit.KryptonDataGridViewIconColumn.ColumnButtonSpec columnButtonSpec4 = new Krypton.Toolkit.KryptonDataGridViewIconColumn.ColumnButtonSpec();
            Krypton.Toolkit.KryptonDataGridViewIconColumn.ColumnButtonSpec columnButtonSpec5 = new Krypton.Toolkit.KryptonDataGridViewIconColumn.ColumnButtonSpec();
            Krypton.Toolkit.KryptonDataGridViewIconColumn.ColumnButtonSpec columnButtonSpec6 = new Krypton.Toolkit.KryptonDataGridViewIconColumn.ColumnButtonSpec();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataGridViewDemo));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kcbGridRtl = new Krypton.Toolkit.KryptonCheckBox();
            this.kdgvMain = new Krypton.Toolkit.KryptonDataGridView();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.pnlOptions = new Krypton.Toolkit.KryptonPanel();
            this.klblColumnHeadersHeight = new Krypton.Toolkit.KryptonLabel();
            this.knudColumnHeadersHeight = new Krypton.Toolkit.KryptonNumericUpDown();
            this.klblRowHeadersWidth = new Krypton.Toolkit.KryptonLabel();
            this.knudRowHeadersWidth = new Krypton.Toolkit.KryptonNumericUpDown();
            this.klblAutoSizeRowsMode = new Krypton.Toolkit.KryptonLabel();
            this.kcmbAutoSizeRowsMode = new Krypton.Toolkit.KryptonComboBox();
            this.klblScrollBars = new Krypton.Toolkit.KryptonLabel();
            this.kcmbScrollBars = new Krypton.Toolkit.KryptonComboBox();
            this.klblEditMode = new Krypton.Toolkit.KryptonLabel();
            this.kcmbEditMode = new Krypton.Toolkit.KryptonComboBox();
            this.klblSelectionMode = new Krypton.Toolkit.KryptonLabel();
            this.kcmbSelectionMode = new Krypton.Toolkit.KryptonComboBox();
            this.klblAutoSizeColumnsMode = new Krypton.Toolkit.KryptonLabel();
            this.kcmbAutoSizeColumnsMode = new Krypton.Toolkit.KryptonComboBox();
            this.kchkEnableHeadersVisualStyles = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkColumnHeadersVisible = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkRowHeadersVisible = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkReadOnly = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkShowGridLines = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkMultiSelect = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkAllowUserToResizeRows = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkAllowUserToResizeColumns = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkAllowUserToDeleteRows = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkAllowUserToAddRows = new Krypton.Toolkit.KryptonCheckBox();
            this.colId = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.colName = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.colDate = new Krypton.Toolkit.KryptonDataGridViewDateTimePickerColumn();
            this.colCombo = new Krypton.Toolkit.KryptonDataGridViewComboBoxColumn();
            this.colQuantity = new Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn();
            this.colDomain = new Krypton.Toolkit.KryptonDataGridViewDomainUpDownColumn();
            this.colActive = new Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn();
            this.colMasked = new Krypton.Toolkit.KryptonDataGridViewMaskedTextBoxColumn();
            this.colProgress = new Krypton.Toolkit.KryptonDataGridViewProgressColumn();
            this.colRating = new Krypton.Toolkit.KryptonDataGridViewRatingColumn();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kdgvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlOptions)).BeginInit();
            this.pnlOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbAutoSizeRowsMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbScrollBars)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbEditMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbSelectionMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbAutoSizeColumnsMode)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kcbGridRtl);
            this.kryptonPanel1.Controls.Add(this.kdgvMain);
            this.kryptonPanel1.Controls.Add(this.kryptonThemeComboBox1);
            this.kryptonPanel1.Controls.Add(this.pnlOptions);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(1048, 562);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kcbGridRtl
            // 
            this.kcbGridRtl.Location = new System.Drawing.Point(538, 10);
            this.kcbGridRtl.Name = "kcbGridRtl";
            this.kcbGridRtl.Size = new System.Drawing.Size(123, 20);
            this.kcbGridRtl.TabIndex = 6;
            this.kcbGridRtl.Values.Text = "Grid Right-to-left?";
            this.kcbGridRtl.CheckedChanged += new System.EventHandler(this.kcbGridRtl_CheckedChanged);
            // 
            // kdgvMain
            // 
            this.kdgvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kdgvMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.kdgvMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.kdgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colName,
            this.colDate,
            this.colCombo,
            this.colQuantity,
            this.colDomain,
            this.colActive,
            this.colMasked,
            this.colProgress,
            this.colRating});
            this.kdgvMain.Location = new System.Drawing.Point(12, 40);
            this.kdgvMain.Name = "kdgvMain";
            this.kdgvMain.Size = new System.Drawing.Size(1024, 318);
            this.kdgvMain.TabIndex = 1;
            this.kdgvMain.EditingControlButtonSpecClick += new System.EventHandler<Krypton.Toolkit.DataGridViewButtonSpecClickEventArgs>(this.kdgvMain_EditingControlButtonSpecClick);
            this.kdgvMain.ColumnHeadersHeightChanged += new System.EventHandler(this.kdgvMain_ColumnHeadersHeightChanged);
            this.kdgvMain.RowHeadersWidthChanged += new System.EventHandler(this.kdgvMain_RowHeadersWidthChanged);
            // 
            // kryptonThemeComboBox1
            // 
            this.kryptonThemeComboBox1.DropDownWidth = 500;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(12, 8);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(500, 22);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 0;
            // 
            // pnlOptions
            // 
            this.pnlOptions.Controls.Add(this.klblColumnHeadersHeight);
            this.pnlOptions.Controls.Add(this.knudColumnHeadersHeight);
            this.pnlOptions.Controls.Add(this.klblRowHeadersWidth);
            this.pnlOptions.Controls.Add(this.knudRowHeadersWidth);
            this.pnlOptions.Controls.Add(this.klblAutoSizeRowsMode);
            this.pnlOptions.Controls.Add(this.kcmbAutoSizeRowsMode);
            this.pnlOptions.Controls.Add(this.klblScrollBars);
            this.pnlOptions.Controls.Add(this.kcmbScrollBars);
            this.pnlOptions.Controls.Add(this.klblEditMode);
            this.pnlOptions.Controls.Add(this.kcmbEditMode);
            this.pnlOptions.Controls.Add(this.klblSelectionMode);
            this.pnlOptions.Controls.Add(this.kcmbSelectionMode);
            this.pnlOptions.Controls.Add(this.klblAutoSizeColumnsMode);
            this.pnlOptions.Controls.Add(this.kcmbAutoSizeColumnsMode);
            this.pnlOptions.Controls.Add(this.kchkEnableHeadersVisualStyles);
            this.pnlOptions.Controls.Add(this.kchkColumnHeadersVisible);
            this.pnlOptions.Controls.Add(this.kchkRowHeadersVisible);
            this.pnlOptions.Controls.Add(this.kchkReadOnly);
            this.pnlOptions.Controls.Add(this.kchkShowGridLines);
            this.pnlOptions.Controls.Add(this.kchkMultiSelect);
            this.pnlOptions.Controls.Add(this.kchkAllowUserToResizeRows);
            this.pnlOptions.Controls.Add(this.kchkAllowUserToResizeColumns);
            this.pnlOptions.Controls.Add(this.kchkAllowUserToDeleteRows);
            this.pnlOptions.Controls.Add(this.kchkAllowUserToAddRows);
            this.pnlOptions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOptions.Location = new System.Drawing.Point(0, 382);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Size = new System.Drawing.Size(1048, 180);
            this.pnlOptions.TabIndex = 2;
            // 
            // klblColumnHeadersHeight
            // 
            this.klblColumnHeadersHeight.Location = new System.Drawing.Point(228, 138);
            this.klblColumnHeadersHeight.Name = "klblColumnHeadersHeight";
            this.klblColumnHeadersHeight.Size = new System.Drawing.Size(137, 20);
            this.klblColumnHeadersHeight.TabIndex = 12;
            this.klblColumnHeadersHeight.Values.Text = "ColumnHeadersHeight:";
            // 
            // knudColumnHeadersHeight
            // 
            this.knudColumnHeadersHeight.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.knudColumnHeadersHeight.Location = new System.Drawing.Point(374, 136);
            this.knudColumnHeadersHeight.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.knudColumnHeadersHeight.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.knudColumnHeadersHeight.Name = "knudColumnHeadersHeight";
            this.knudColumnHeadersHeight.Size = new System.Drawing.Size(70, 22);
            this.knudColumnHeadersHeight.TabIndex = 13;
            this.knudColumnHeadersHeight.Value = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.knudColumnHeadersHeight.ValueChanged += new System.EventHandler(this.knudColumnHeadersHeight_ValueChanged);
            // 
            // klblRowHeadersWidth
            // 
            this.klblRowHeadersWidth.Location = new System.Drawing.Point(228, 112);
            this.klblRowHeadersWidth.Name = "klblRowHeadersWidth";
            this.klblRowHeadersWidth.Size = new System.Drawing.Size(115, 20);
            this.klblRowHeadersWidth.TabIndex = 10;
            this.klblRowHeadersWidth.Values.Text = "RowHeadersWidth:";
            // 
            // knudRowHeadersWidth
            // 
            this.knudRowHeadersWidth.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.knudRowHeadersWidth.Location = new System.Drawing.Point(374, 108);
            this.knudRowHeadersWidth.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.knudRowHeadersWidth.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.knudRowHeadersWidth.Name = "knudRowHeadersWidth";
            this.knudRowHeadersWidth.Size = new System.Drawing.Size(70, 22);
            this.knudRowHeadersWidth.TabIndex = 11;
            this.knudRowHeadersWidth.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.knudRowHeadersWidth.ValueChanged += new System.EventHandler(this.knudRowHeadersWidth_ValueChanged);
            // 
            // klblAutoSizeRowsMode
            // 
            this.klblAutoSizeRowsMode.Location = new System.Drawing.Point(450, 120);
            this.klblAutoSizeRowsMode.Name = "klblAutoSizeRowsMode";
            this.klblAutoSizeRowsMode.Size = new System.Drawing.Size(122, 20);
            this.klblAutoSizeRowsMode.TabIndex = 22;
            this.klblAutoSizeRowsMode.Values.Text = "AutoSizeRowsMode:";
            // 
            // kcmbAutoSizeRowsMode
            // 
            this.kcmbAutoSizeRowsMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbAutoSizeRowsMode.DropDownWidth = 250;
            this.kcmbAutoSizeRowsMode.IntegralHeight = false;
            this.kcmbAutoSizeRowsMode.Items.AddRange(new object[] {
            "None",
            "AllHeaders",
            "AllCellsExceptHeaders",
            "AllCells",
            "DisplayedHeaders",
            "DisplayedCellsExceptHeaders",
            "DisplayedCells"});
            this.kcmbAutoSizeRowsMode.Location = new System.Drawing.Point(650, 120);
            this.kcmbAutoSizeRowsMode.Name = "kcmbAutoSizeRowsMode";
            this.kcmbAutoSizeRowsMode.Size = new System.Drawing.Size(250, 22);
            this.kcmbAutoSizeRowsMode.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbAutoSizeRowsMode.TabIndex = 23;
            this.kcmbAutoSizeRowsMode.Text = "None";
            this.kcmbAutoSizeRowsMode.SelectedIndexChanged += new System.EventHandler(this.kcmbAutoSizeRowsMode_SelectedIndexChanged);
            // 
            // klblScrollBars
            // 
            this.klblScrollBars.Location = new System.Drawing.Point(450, 92);
            this.klblScrollBars.Name = "klblScrollBars";
            this.klblScrollBars.Size = new System.Drawing.Size(66, 20);
            this.klblScrollBars.TabIndex = 20;
            this.klblScrollBars.Values.Text = "ScrollBars:";
            // 
            // kcmbScrollBars
            // 
            this.kcmbScrollBars.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbScrollBars.DropDownWidth = 250;
            this.kcmbScrollBars.IntegralHeight = false;
            this.kcmbScrollBars.Items.AddRange(new object[] {
            "None",
            "Horizontal",
            "Vertical",
            "Both"});
            this.kcmbScrollBars.Location = new System.Drawing.Point(650, 92);
            this.kcmbScrollBars.Name = "kcmbScrollBars";
            this.kcmbScrollBars.Size = new System.Drawing.Size(250, 22);
            this.kcmbScrollBars.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbScrollBars.TabIndex = 21;
            this.kcmbScrollBars.Text = "Both";
            this.kcmbScrollBars.SelectedIndexChanged += new System.EventHandler(this.kcmbScrollBars_SelectedIndexChanged);
            // 
            // klblEditMode
            // 
            this.klblEditMode.Location = new System.Drawing.Point(450, 64);
            this.klblEditMode.Name = "klblEditMode";
            this.klblEditMode.Size = new System.Drawing.Size(66, 20);
            this.klblEditMode.TabIndex = 18;
            this.klblEditMode.Values.Text = "EditMode:";
            // 
            // kcmbEditMode
            // 
            this.kcmbEditMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbEditMode.DropDownWidth = 250;
            this.kcmbEditMode.IntegralHeight = false;
            this.kcmbEditMode.Items.AddRange(new object[] {
            "EditOnEnter",
            "EditOnKeystroke",
            "EditOnKeystrokeOrF2",
            "EditOnF2",
            "EditProgrammatically"});
            this.kcmbEditMode.Location = new System.Drawing.Point(650, 64);
            this.kcmbEditMode.Name = "kcmbEditMode";
            this.kcmbEditMode.Size = new System.Drawing.Size(250, 22);
            this.kcmbEditMode.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbEditMode.TabIndex = 19;
            this.kcmbEditMode.Text = "EditOnKeystrokeOrF2";
            this.kcmbEditMode.SelectedIndexChanged += new System.EventHandler(this.kcmbEditMode_SelectedIndexChanged);
            // 
            // klblSelectionMode
            // 
            this.klblSelectionMode.Location = new System.Drawing.Point(450, 36);
            this.klblSelectionMode.Name = "klblSelectionMode";
            this.klblSelectionMode.Size = new System.Drawing.Size(95, 20);
            this.klblSelectionMode.TabIndex = 16;
            this.klblSelectionMode.Values.Text = "SelectionMode:";
            // 
            // kcmbSelectionMode
            // 
            this.kcmbSelectionMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbSelectionMode.DropDownWidth = 250;
            this.kcmbSelectionMode.IntegralHeight = false;
            this.kcmbSelectionMode.Items.AddRange(new object[] {
            "CellSelect",
            "FullRowSelect",
            "FullColumnSelect",
            "RowHeaderSelect",
            "ColumnHeaderSelect"});
            this.kcmbSelectionMode.Location = new System.Drawing.Point(650, 36);
            this.kcmbSelectionMode.Name = "kcmbSelectionMode";
            this.kcmbSelectionMode.Size = new System.Drawing.Size(250, 22);
            this.kcmbSelectionMode.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbSelectionMode.TabIndex = 17;
            this.kcmbSelectionMode.Text = "RowHeaderSelect";
            this.kcmbSelectionMode.SelectedIndexChanged += new System.EventHandler(this.kcmbSelectionMode_SelectedIndexChanged);
            // 
            // klblAutoSizeColumnsMode
            // 
            this.klblAutoSizeColumnsMode.Location = new System.Drawing.Point(450, 8);
            this.klblAutoSizeColumnsMode.Name = "klblAutoSizeColumnsMode";
            this.klblAutoSizeColumnsMode.Size = new System.Drawing.Size(141, 20);
            this.klblAutoSizeColumnsMode.TabIndex = 14;
            this.klblAutoSizeColumnsMode.Values.Text = "AutoSizeColumnsMode:";
            // 
            // kcmbAutoSizeColumnsMode
            // 
            this.kcmbAutoSizeColumnsMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbAutoSizeColumnsMode.DropDownWidth = 250;
            this.kcmbAutoSizeColumnsMode.IntegralHeight = false;
            this.kcmbAutoSizeColumnsMode.Items.AddRange(new object[] {
            "None",
            "ColumnHeader",
            "AllCellsExceptHeader",
            "AllCells",
            "DisplayedCellsExceptHeader",
            "DisplayedCells",
            "Fill"});
            this.kcmbAutoSizeColumnsMode.Location = new System.Drawing.Point(650, 8);
            this.kcmbAutoSizeColumnsMode.Name = "kcmbAutoSizeColumnsMode";
            this.kcmbAutoSizeColumnsMode.Size = new System.Drawing.Size(250, 22);
            this.kcmbAutoSizeColumnsMode.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbAutoSizeColumnsMode.TabIndex = 15;
            this.kcmbAutoSizeColumnsMode.Text = "None";
            this.kcmbAutoSizeColumnsMode.SelectedIndexChanged += new System.EventHandler(this.kcmbAutoSizeColumnsMode_SelectedIndexChanged);
            // 
            // kchkEnableHeadersVisualStyles
            // 
            this.kchkEnableHeadersVisualStyles.Checked = true;
            this.kchkEnableHeadersVisualStyles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkEnableHeadersVisualStyles.Location = new System.Drawing.Point(228, 60);
            this.kchkEnableHeadersVisualStyles.Name = "kchkEnableHeadersVisualStyles";
            this.kchkEnableHeadersVisualStyles.Size = new System.Drawing.Size(168, 20);
            this.kchkEnableHeadersVisualStyles.TabIndex = 8;
            this.kchkEnableHeadersVisualStyles.Values.Text = "EnableHeadersVisualStyles";
            this.kchkEnableHeadersVisualStyles.CheckedChanged += new System.EventHandler(this.kchkEnableHeadersVisualStyles_CheckedChanged);
            // 
            // kchkColumnHeadersVisible
            // 
            this.kchkColumnHeadersVisible.Checked = true;
            this.kchkColumnHeadersVisible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkColumnHeadersVisible.Location = new System.Drawing.Point(228, 34);
            this.kchkColumnHeadersVisible.Name = "kchkColumnHeadersVisible";
            this.kchkColumnHeadersVisible.Size = new System.Drawing.Size(146, 20);
            this.kchkColumnHeadersVisible.TabIndex = 7;
            this.kchkColumnHeadersVisible.Values.Text = "ColumnHeadersVisible";
            this.kchkColumnHeadersVisible.CheckedChanged += new System.EventHandler(this.kchkColumnHeadersVisible_CheckedChanged);
            // 
            // kchkRowHeadersVisible
            // 
            this.kchkRowHeadersVisible.Checked = true;
            this.kchkRowHeadersVisible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkRowHeadersVisible.Location = new System.Drawing.Point(228, 8);
            this.kchkRowHeadersVisible.Name = "kchkRowHeadersVisible";
            this.kchkRowHeadersVisible.Size = new System.Drawing.Size(128, 20);
            this.kchkRowHeadersVisible.TabIndex = 6;
            this.kchkRowHeadersVisible.Values.Text = "RowHeadersVisible";
            this.kchkRowHeadersVisible.CheckedChanged += new System.EventHandler(this.kchkRowHeadersVisible_CheckedChanged);
            // 
            // kchkReadOnly
            // 
            this.kchkReadOnly.Location = new System.Drawing.Point(12, 138);
            this.kchkReadOnly.Name = "kchkReadOnly";
            this.kchkReadOnly.Size = new System.Drawing.Size(76, 20);
            this.kchkReadOnly.TabIndex = 5;
            this.kchkReadOnly.Values.Text = "ReadOnly";
            this.kchkReadOnly.CheckedChanged += new System.EventHandler(this.kchkReadOnly_CheckedChanged);
            // 
            // kchkShowGridLines
            // 
            this.kchkShowGridLines.Checked = true;
            this.kchkShowGridLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkShowGridLines.Location = new System.Drawing.Point(228, 86);
            this.kchkShowGridLines.Name = "kchkShowGridLines";
            this.kchkShowGridLines.Size = new System.Drawing.Size(111, 20);
            this.kchkShowGridLines.TabIndex = 9;
            this.kchkShowGridLines.Values.Text = "Show Grid Lines";
            this.kchkShowGridLines.CheckedChanged += new System.EventHandler(this.kchkShowGridLines_CheckedChanged);
            // 
            // kchkMultiSelect
            // 
            this.kchkMultiSelect.Checked = true;
            this.kchkMultiSelect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkMultiSelect.Location = new System.Drawing.Point(12, 112);
            this.kchkMultiSelect.Name = "kchkMultiSelect";
            this.kchkMultiSelect.Size = new System.Drawing.Size(84, 20);
            this.kchkMultiSelect.TabIndex = 4;
            this.kchkMultiSelect.Values.Text = "MultiSelect";
            this.kchkMultiSelect.CheckedChanged += new System.EventHandler(this.kchkMultiSelect_CheckedChanged);
            // 
            // kchkAllowUserToResizeRows
            // 
            this.kchkAllowUserToResizeRows.Checked = true;
            this.kchkAllowUserToResizeRows.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkAllowUserToResizeRows.Location = new System.Drawing.Point(12, 86);
            this.kchkAllowUserToResizeRows.Name = "kchkAllowUserToResizeRows";
            this.kchkAllowUserToResizeRows.Size = new System.Drawing.Size(155, 20);
            this.kchkAllowUserToResizeRows.TabIndex = 3;
            this.kchkAllowUserToResizeRows.Values.Text = "AllowUserToResizeRows";
            this.kchkAllowUserToResizeRows.CheckedChanged += new System.EventHandler(this.kchkAllowUserToResizeRows_CheckedChanged);
            // 
            // kchkAllowUserToResizeColumns
            // 
            this.kchkAllowUserToResizeColumns.Checked = true;
            this.kchkAllowUserToResizeColumns.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkAllowUserToResizeColumns.Location = new System.Drawing.Point(12, 60);
            this.kchkAllowUserToResizeColumns.Name = "kchkAllowUserToResizeColumns";
            this.kchkAllowUserToResizeColumns.Size = new System.Drawing.Size(174, 20);
            this.kchkAllowUserToResizeColumns.TabIndex = 2;
            this.kchkAllowUserToResizeColumns.Values.Text = "AllowUserToResizeColumns";
            this.kchkAllowUserToResizeColumns.CheckedChanged += new System.EventHandler(this.kchkAllowUserToResizeColumns_CheckedChanged);
            // 
            // kchkAllowUserToDeleteRows
            // 
            this.kchkAllowUserToDeleteRows.Checked = true;
            this.kchkAllowUserToDeleteRows.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkAllowUserToDeleteRows.Location = new System.Drawing.Point(12, 34);
            this.kchkAllowUserToDeleteRows.Name = "kchkAllowUserToDeleteRows";
            this.kchkAllowUserToDeleteRows.Size = new System.Drawing.Size(156, 20);
            this.kchkAllowUserToDeleteRows.TabIndex = 1;
            this.kchkAllowUserToDeleteRows.Values.Text = "AllowUserToDeleteRows";
            this.kchkAllowUserToDeleteRows.CheckedChanged += new System.EventHandler(this.kchkAllowUserToDeleteRows_CheckedChanged);
            // 
            // kchkAllowUserToAddRows
            // 
            this.kchkAllowUserToAddRows.Checked = true;
            this.kchkAllowUserToAddRows.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkAllowUserToAddRows.Location = new System.Drawing.Point(12, 8);
            this.kchkAllowUserToAddRows.Name = "kchkAllowUserToAddRows";
            this.kchkAllowUserToAddRows.Size = new System.Drawing.Size(143, 20);
            this.kchkAllowUserToAddRows.TabIndex = 0;
            this.kchkAllowUserToAddRows.Values.Text = "AllowUserToAddRows";
            this.kchkAllowUserToAddRows.CheckedChanged += new System.EventHandler(this.kchkAllowUserToAddRows_CheckedChanged);
            // 
            // colId
            // 
            this.colId.HeaderText = "Id";
            this.colId.Name = "colId";
            this.colId.Width = 46;
            // 
            // colName
            // 
            columnButtonSpec1.Alignment = Krypton.Toolkit.IconSpec.IconAlignment.Right;
            columnButtonSpec1.ExtraText = null;
            columnButtonSpec1.Icon = ((System.Drawing.Image)(resources.GetObject("columnButtonSpec1.Icon")));
            columnButtonSpec1.ImageTransparentColor = System.Drawing.Color.Empty;
            columnButtonSpec1.Text = null;
            columnButtonSpec1.Type = Krypton.Toolkit.PaletteButtonSpecStyle.FormClose;
            this.colName.ButtonSpecs.Add(columnButtonSpec1);
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.Width = 68;
            // 
            // colDate
            // 
            this.colDate.Checked = false;
            this.colDate.CustomFormat = "dd.MM.yyyy";
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 0, 17, 0);
            this.colDate.DefaultCellStyle = dataGridViewCellStyle1;
            this.colDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            columnButtonSpec2.Alignment = Krypton.Toolkit.IconSpec.IconAlignment.Right;
            columnButtonSpec2.ExtraText = null;
            columnButtonSpec2.Icon = null;
            columnButtonSpec2.ImageTransparentColor = System.Drawing.Color.Empty;
            columnButtonSpec2.Text = null;
            columnButtonSpec2.Type = Krypton.Toolkit.PaletteButtonSpecStyle.FormClose;
            this.colDate.ButtonSpecs.Add(columnButtonSpec2);
            this.colDate.HeaderText = "Date";
            this.colDate.MinimumWidth = 19;
            this.colDate.Name = "colDate";
            this.colDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colDate.Width = 60;
            // 
            // colCombo
            // 
            this.colCombo.DropDownWidth = 121;
            columnButtonSpec3.Alignment = Krypton.Toolkit.IconSpec.IconAlignment.Right;
            columnButtonSpec3.ExtraText = null;
            columnButtonSpec3.Icon = null;
            columnButtonSpec3.ImageTransparentColor = System.Drawing.Color.Empty;
            columnButtonSpec3.Text = null;
            columnButtonSpec3.Type = Krypton.Toolkit.PaletteButtonSpecStyle.FormClose;
            this.colCombo.ButtonSpecs.Add(columnButtonSpec3);
            this.colCombo.HeaderText = "Combo";
            this.colCombo.Name = "colCombo";
            this.colCombo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colCombo.Width = 76;
            // 
            // colQuantity
            // 
            this.colQuantity.AllowDecimals = false;
            columnButtonSpec4.Alignment = Krypton.Toolkit.IconSpec.IconAlignment.Right;
            columnButtonSpec4.ExtraText = null;
            columnButtonSpec4.Icon = null;
            columnButtonSpec4.ImageTransparentColor = System.Drawing.Color.Empty;
            columnButtonSpec4.Text = null;
            columnButtonSpec4.Type = Krypton.Toolkit.PaletteButtonSpecStyle.FormClose;
            this.colQuantity.ButtonSpecs.Add(columnButtonSpec4);
            this.colQuantity.HeaderText = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colQuantity.Width = 82;
            // 
            // colDomain
            // 
            columnButtonSpec5.Alignment = Krypton.Toolkit.IconSpec.IconAlignment.Right;
            columnButtonSpec5.ExtraText = null;
            columnButtonSpec5.Icon = null;
            columnButtonSpec5.ImageTransparentColor = System.Drawing.Color.Empty;
            columnButtonSpec5.Text = null;
            columnButtonSpec5.Type = Krypton.Toolkit.PaletteButtonSpecStyle.FormClose;
            this.colDomain.ButtonSpecs.Add(columnButtonSpec5);
            this.colDomain.HeaderText = "Domain";
            this.colDomain.Name = "colDomain";
            this.colDomain.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colDomain.Width = 78;
            // 
            // colActive
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = false;
            this.colActive.DefaultCellStyle = dataGridViewCellStyle2;
            this.colActive.FalseValue = null;
            this.colActive.HeaderText = "Active";
            this.colActive.IndeterminateValue = null;
            this.colActive.Name = "colActive";
            this.colActive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colActive.TrueValue = null;
            this.colActive.Width = 69;
            // 
            // colMasked
            // 
            columnButtonSpec6.Alignment = Krypton.Toolkit.IconSpec.IconAlignment.Right;
            columnButtonSpec6.ExtraText = null;
            columnButtonSpec6.Icon = null;
            columnButtonSpec6.ImageTransparentColor = System.Drawing.Color.Empty;
            columnButtonSpec6.Text = null;
            columnButtonSpec6.Type = Krypton.Toolkit.PaletteButtonSpecStyle.FormClose;
            this.colMasked.ButtonSpecs.Add(columnButtonSpec6);
            this.colMasked.HeaderText = "Masked";
            this.colMasked.Name = "colMasked";
            this.colMasked.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colMasked.Width = 77;
            // 
            // colProgress
            // 
            this.colProgress.HeaderText = "Progress";
            this.colProgress.Name = "colProgress";
            this.colProgress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colProgress.Width = 81;
            // 
            // colRating
            // 
            this.colRating.HeaderText = "Rating";
            this.colRating.Name = "colRating";
            this.colRating.RatingMaximum = ((byte)(0));
            this.colRating.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colRating.Width = 70;
            // 
            // DataGridViewDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 562);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "DataGridViewDemo";
            this.Text = "KryptonDataGridView Demo";
            this.Load += new System.EventHandler(this.DataGridViewDemo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kdgvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlOptions)).EndInit();
            this.pnlOptions.ResumeLayout(false);
            this.pnlOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbAutoSizeRowsMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbScrollBars)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbEditMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbSelectionMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbAutoSizeColumnsMode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
        private Krypton.Toolkit.KryptonDataGridView kdgvMain;
        private Krypton.Toolkit.KryptonPanel pnlOptions;
        private Krypton.Toolkit.KryptonCheckBox kchkAllowUserToAddRows;
        private Krypton.Toolkit.KryptonCheckBox kchkAllowUserToDeleteRows;
        private Krypton.Toolkit.KryptonCheckBox kchkAllowUserToResizeColumns;
        private Krypton.Toolkit.KryptonCheckBox kchkAllowUserToResizeRows;
        private Krypton.Toolkit.KryptonCheckBox kchkMultiSelect;
        private Krypton.Toolkit.KryptonCheckBox kchkReadOnly;
        private Krypton.Toolkit.KryptonCheckBox kchkRowHeadersVisible;
        private Krypton.Toolkit.KryptonCheckBox kchkColumnHeadersVisible;
        private Krypton.Toolkit.KryptonCheckBox kchkEnableHeadersVisualStyles;
        private Krypton.Toolkit.KryptonLabel klblAutoSizeColumnsMode;
        private Krypton.Toolkit.KryptonComboBox kcmbAutoSizeColumnsMode;
        private Krypton.Toolkit.KryptonLabel klblSelectionMode;
        private Krypton.Toolkit.KryptonComboBox kcmbSelectionMode;
        private Krypton.Toolkit.KryptonLabel klblEditMode;
        private Krypton.Toolkit.KryptonComboBox kcmbEditMode;
        private Krypton.Toolkit.KryptonLabel klblScrollBars;
        private Krypton.Toolkit.KryptonComboBox kcmbScrollBars;
        private Krypton.Toolkit.KryptonLabel klblAutoSizeRowsMode;
        private Krypton.Toolkit.KryptonComboBox kcmbAutoSizeRowsMode;
        private Krypton.Toolkit.KryptonLabel klblRowHeadersWidth;
        private Krypton.Toolkit.KryptonNumericUpDown knudRowHeadersWidth;
        private Krypton.Toolkit.KryptonLabel klblColumnHeadersHeight;
        private Krypton.Toolkit.KryptonNumericUpDown knudColumnHeadersHeight;
        private Krypton.Toolkit.KryptonCheckBox kchkShowGridLines;
        private KryptonCheckBox kcbGridRtl;
        private KryptonDataGridViewTextBoxColumn colId;
        private KryptonDataGridViewTextBoxColumn colName;
        private KryptonDataGridViewDateTimePickerColumn colDate;
        private KryptonDataGridViewComboBoxColumn colCombo;
        private KryptonDataGridViewNumericUpDownColumn colQuantity;
        private KryptonDataGridViewDomainUpDownColumn colDomain;
        private KryptonDataGridViewCheckBoxColumn colActive;
        private KryptonDataGridViewMaskedTextBoxColumn colMasked;
        private KryptonDataGridViewProgressColumn colProgress;
        private KryptonDataGridViewRatingColumn colRating;
    }
}

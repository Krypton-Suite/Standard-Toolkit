using System.Windows.Forms;

namespace TestForm
{
    partial class PaletteViewerForm
    {
        private Krypton.Toolkit.KryptonDataGridView dataGridViewPalette;
        private Krypton.Toolkit.KryptonButton buttonAddPalette;
        private Krypton.Toolkit.KryptonButton buttonRemovePalette;
        private Krypton.Toolkit.KryptonComboBox comboTheme;
        private Krypton.Toolkit.KryptonPanel panelTop;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private Krypton.Toolkit.KryptonButton buttonAddAll;
        private Krypton.Toolkit.KryptonButton buttonCancel;
        private Krypton.Toolkit.KryptonButton buttonClear;
        private Krypton.Toolkit.KryptonComboBox comboSaveFormat;
        private Krypton.Toolkit.KryptonButton buttonSave;
        private Krypton.Toolkit.KryptonLabel labelSourceTitle;
        private Krypton.Toolkit.KryptonTextBox textSourcePath;
        private Krypton.Toolkit.KryptonButton buttonBrowseSource;
        private Krypton.Toolkit.KryptonLabel labelSwitchTheme;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox;

        private void InitializeComponent()
        {
            this.dataGridViewPalette = new Krypton.Toolkit.KryptonDataGridView();
            this.buttonAddPalette = new Krypton.Toolkit.KryptonButton();
            this.buttonRemovePalette = new Krypton.Toolkit.KryptonButton();
            this.comboTheme = new Krypton.Toolkit.KryptonComboBox();
            this.panelTop = new Krypton.Toolkit.KryptonPanel();
            this.buttonAddAll = new Krypton.Toolkit.KryptonButton();
            this.buttonClear = new Krypton.Toolkit.KryptonButton();
            this.comboSaveFormat = new Krypton.Toolkit.KryptonComboBox();
            this.buttonSave = new Krypton.Toolkit.KryptonButton();
            this.buttonCancel = new Krypton.Toolkit.KryptonButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelSourceTitle = new Krypton.Toolkit.KryptonLabel();
            this.textSourcePath = new Krypton.Toolkit.KryptonTextBox();
            this.buttonBrowseSource = new Krypton.Toolkit.KryptonButton();
            this.labelSwitchTheme = new Krypton.Toolkit.KryptonLabel();
            this.kryptonThemeComboBox = new Krypton.Toolkit.KryptonThemeComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPalette)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboTheme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).BeginInit();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboSaveFormat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            //
            // dataGridViewPalette
            //
            this.dataGridViewPalette.AllowUserToAddRows = false;
            this.dataGridViewPalette.AllowUserToDeleteRows = false;
            this.dataGridViewPalette.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewPalette.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPalette.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPalette.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewPalette.Location = new System.Drawing.Point(0, 40);
            this.dataGridViewPalette.MultiSelect = false;
            this.dataGridViewPalette.Name = "dataGridViewPalette";
            this.dataGridViewPalette.RowTemplate.Height = 30;
            this.dataGridViewPalette.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewPalette.Size = new System.Drawing.Size(1157, 564);
            this.dataGridViewPalette.TabIndex = 0;
            this.dataGridViewPalette.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DataGridViewPalette_CellPainting);
            this.dataGridViewPalette.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.DataGridViewPalette_ColumnAdded);
            this.dataGridViewPalette.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridViewPalette_ColumnHeaderMouseClick);
            this.dataGridViewPalette.CurrentCellChanged += new System.EventHandler(this.DataGridViewPalette_CurrentCellChanged);
            this.dataGridViewPalette.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DataGridViewPalette_EditingControlShowing);
            this.dataGridViewPalette.Scroll += new System.Windows.Forms.ScrollEventHandler(this.DataGridViewPalette_Scroll);
            this.dataGridViewPalette.SelectionChanged += new System.EventHandler(this.DataGridViewPalette_SelectionChanged);
            this.dataGridViewPalette.Paint += new System.Windows.Forms.PaintEventHandler(this.DataGridViewPalette_Paint);
            //
            // buttonAddPalette
            //
            this.buttonAddPalette.AutoSize = true;
            this.buttonAddPalette.Location = new System.Drawing.Point(383, 8);
            this.buttonAddPalette.Name = "buttonAddPalette";
            this.buttonAddPalette.Size = new System.Drawing.Size(90, 26);
            this.buttonAddPalette.TabIndex = 1;
            this.buttonAddPalette.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonAddPalette.Values.Text = "Add Palette";
            this.buttonAddPalette.Click += new System.EventHandler(this.BtnAddPalette_Click);
            //
            // buttonRemovePalette
            //
            this.buttonRemovePalette.AutoSize = true;
            this.buttonRemovePalette.Location = new System.Drawing.Point(479, 8);
            this.buttonRemovePalette.Name = "buttonRemovePalette";
            this.buttonRemovePalette.Size = new System.Drawing.Size(116, 26);
            this.buttonRemovePalette.TabIndex = 2;
            this.buttonRemovePalette.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonRemovePalette.Values.Text = "Remove Palette";
            this.buttonRemovePalette.Click += new System.EventHandler(this.BtnRemovePalette_Click);
            //
            // comboTheme
            //
            this.comboTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTheme.DropDownWidth = 400;
            this.comboTheme.Location = new System.Drawing.Point(6, 8);
            this.comboTheme.Name = "comboTheme";
            this.comboTheme.Size = new System.Drawing.Size(369, 22);
            this.comboTheme.TabIndex = 0;
            this.comboTheme.SelectedIndexChanged += new System.EventHandler(this.ComboTheme_SelectedIndexChanged);
            //
            // panelTop
            //
            this.panelTop.Controls.Add(this.comboTheme);
            this.panelTop.Controls.Add(this.labelSourceTitle);
            this.panelTop.Controls.Add(this.textSourcePath);
            this.panelTop.Controls.Add(this.buttonBrowseSource);
            this.panelTop.Controls.Add(this.buttonAddPalette);
            this.panelTop.Controls.Add(this.buttonRemovePalette);
            this.panelTop.Controls.Add(this.buttonAddAll);
            this.panelTop.Controls.Add(this.buttonClear);
            this.panelTop.Controls.Add(this.comboSaveFormat);
            this.panelTop.Controls.Add(this.buttonSave);
            this.panelTop.Controls.Add(this.buttonCancel);
            this.panelTop.Controls.Add(this.labelSwitchTheme);
            this.panelTop.Controls.Add(this.kryptonThemeComboBox);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(8);
            this.panelTop.Size = new System.Drawing.Size(1157, 70);
            this.panelTop.TabIndex = 1;
            //
            // buttonAddAll
            //
            this.buttonAddAll.AutoSize = true;
            this.buttonAddAll.Location = new System.Drawing.Point(615, 8);
            this.buttonAddAll.Name = "buttonAddAll";
            this.buttonAddAll.Size = new System.Drawing.Size(66, 26);
            this.buttonAddAll.TabIndex = 3;
            this.buttonAddAll.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonAddAll.Values.Text = "Add ALL";
            this.buttonAddAll.Click += new System.EventHandler(this.BtnAddAll_Click);
            //
            // buttonClear
            //
            this.buttonClear.AutoSize = true;
            this.buttonClear.Location = new System.Drawing.Point(700, 8);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(16, 2, 2, 2);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(58, 26);
            this.buttonClear.TabIndex = 4;
            this.buttonClear.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonClear.Values.Text = "Clear";
            this.buttonClear.Click += new System.EventHandler(this.BtnClear_Click);
            //
            // comboSaveFormat
            //
            this.comboSaveFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboSaveFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSaveFormat.DropDownWidth = 100;
            this.comboSaveFormat.Items.AddRange(new object[] {
            "CSV",
            "XML"});
            this.comboSaveFormat.Location = new System.Drawing.Point(935, 8);
            this.comboSaveFormat.Margin = new System.Windows.Forms.Padding(16, 2, 2, 2);
            this.comboSaveFormat.Name = "comboSaveFormat";
            this.comboSaveFormat.Size = new System.Drawing.Size(63, 22);
            this.comboSaveFormat.TabIndex = 5;
            this.comboSaveFormat.Text = "CSV";
            //
            // buttonSave
            //
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.AutoSize = true;
            this.buttonSave.Enabled = false;
            this.buttonSave.Location = new System.Drawing.Point(1003, 8);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(60, 26);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonSave.Values.Text = "Save";
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            //
            // buttonCancel
            //
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.AutoSize = true;
            this.buttonCancel.Location = new System.Drawing.Point(1082, 8);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(63, 26);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonCancel.Values.Text = "Cancel";
            this.buttonCancel.Visible = false;
            this.buttonCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            //
            // statusStrip
            //
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 604);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1157, 22);
            this.statusStrip.TabIndex = 2;
            //
            // statusLabel
            //
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(39, 17);
            this.statusLabel.Text = "Ready";
            //
            // labelSourceTitle
            //
            this.labelSourceTitle.AutoSize = true;
            this.labelSourceTitle.Location = new System.Drawing.Point(7, 36);
            this.labelSourceTitle.Name = "labelSourceTitle";
            this.labelSourceTitle.Size = new System.Drawing.Size(90, 18);
            this.labelSourceTitle.TabIndex = 8;
            this.labelSourceTitle.Values.Text = "Source path:";
            //
            // textSourcePath
            //
            this.textSourcePath.Location = new System.Drawing.Point(110, 36);
            this.textSourcePath.Name = "textSourcePath";
            this.textSourcePath.Size = new System.Drawing.Size(400, 24);
            this.textSourcePath.TabIndex = 9;
            this.textSourcePath.ReadOnly = true;
            //
            // buttonBrowseSource
            //
            this.buttonBrowseSource.AutoSize = true;
            this.buttonBrowseSource.Location = new System.Drawing.Point(520, 36);
            this.buttonBrowseSource.Name = "buttonBrowseSource";
            this.buttonBrowseSource.Size = new System.Drawing.Size(116, 26);
            this.buttonBrowseSource.TabIndex = 10;
            this.buttonBrowseSource.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonBrowseSource.Values.Text = "Browse Source";
            this.buttonBrowseSource.Click += new System.EventHandler(this.BtnBrowseSource_Click);
            //
            // labelSwitchTheme
            //
            this.labelSwitchTheme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSwitchTheme.AutoSize = true;
            this.labelSwitchTheme.Location = new System.Drawing.Point(834, 36);
            this.labelSwitchTheme.Name = "labelSwitchTheme";
            this.labelSwitchTheme.Size = new System.Drawing.Size(95, 18);
            this.labelSwitchTheme.TabIndex = 11;
            this.labelSwitchTheme.Values.Text = "Switch Theme:";
            //
            // kryptonThemeComboBox
            //
            this.kryptonThemeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonThemeComboBox.DropDownWidth = 200;
            this.kryptonThemeComboBox.IntegralHeight = false;
            this.kryptonThemeComboBox.Location = new System.Drawing.Point(935, 36);
            this.kryptonThemeComboBox.Name = "kryptonThemeComboBox";
            this.kryptonThemeComboBox.Size = new System.Drawing.Size(200, 22);
            this.kryptonThemeComboBox.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox.TabIndex = 12;
            //
            // MainForm
            //
            this.ClientSize = new System.Drawing.Size(1157, 626);
            this.Controls.Add(this.dataGridViewPalette);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.statusStrip);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PaletteViewer";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPalette)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboTheme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboSaveFormat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
using System.Windows.Forms;

namespace TestForm
{
    partial class PaletteViewerForm
    {
        private System.Windows.Forms.DataGridView dataGridViewPalette;
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
        private Krypton.Toolkit.KryptonButton buttonHelp;
        private Krypton.Toolkit.KryptonLabel labelSourceTitle;
        private Krypton.Toolkit.KryptonTextBox textSourcePath;
        private Krypton.Toolkit.KryptonButton buttonBrowseSource;
        private Krypton.Toolkit.KryptonButton buttonClearSource;
        private Krypton.Toolkit.KryptonLabel labelSourceRequired;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox;

        private void InitializeComponent()
        {
            this.dataGridViewPalette = new System.Windows.Forms.DataGridView();
            this.buttonAddPalette = new Krypton.Toolkit.KryptonButton();
            this.buttonRemovePalette = new Krypton.Toolkit.KryptonButton();
            this.comboTheme = new Krypton.Toolkit.KryptonComboBox();
            this.panelTop = new Krypton.Toolkit.KryptonPanel();
            this.labelSourceTitle = new Krypton.Toolkit.KryptonLabel();
            this.textSourcePath = new Krypton.Toolkit.KryptonTextBox();
            this.buttonBrowseSource = new Krypton.Toolkit.KryptonButton();
            this.buttonClearSource = new Krypton.Toolkit.KryptonButton();
            this.labelSourceRequired = new Krypton.Toolkit.KryptonLabel();
            this.buttonAddAll = new Krypton.Toolkit.KryptonButton();
            this.buttonClear = new Krypton.Toolkit.KryptonButton();
            this.comboSaveFormat = new Krypton.Toolkit.KryptonComboBox();
            this.buttonSave = new Krypton.Toolkit.KryptonButton();
            this.buttonHelp = new Krypton.Toolkit.KryptonButton();
            this.buttonCancel = new Krypton.Toolkit.KryptonButton();
            this.kryptonThemeComboBox = new Krypton.Toolkit.KryptonThemeComboBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.dataGridViewPalette.Location = new System.Drawing.Point(0, 70);
            this.dataGridViewPalette.MultiSelect = false;
            this.dataGridViewPalette.Name = "dataGridViewPalette";
            this.dataGridViewPalette.RowTemplate.Height = 30;
            this.dataGridViewPalette.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewPalette.Size = new System.Drawing.Size(1289, 522);
            this.dataGridViewPalette.TabIndex = 0;
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
            this.panelTop.Controls.Add(this.buttonClearSource);
            this.panelTop.Controls.Add(this.labelSourceRequired);
            this.panelTop.Controls.Add(this.buttonAddPalette);
            this.panelTop.Controls.Add(this.buttonRemovePalette);
            this.panelTop.Controls.Add(this.buttonAddAll);
            this.panelTop.Controls.Add(this.buttonClear);
            this.panelTop.Controls.Add(this.comboSaveFormat);
            this.panelTop.Controls.Add(this.buttonSave);
            this.panelTop.Controls.Add(this.buttonHelp);
            this.panelTop.Controls.Add(this.buttonCancel);
            this.panelTop.Controls.Add(this.kryptonThemeComboBox);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(8);
            this.panelTop.Size = new System.Drawing.Size(1289, 70);
            this.panelTop.TabIndex = 1;
            //
            // labelSourceTitle
            //
            this.labelSourceTitle.AutoSize = false;
            this.labelSourceTitle.Location = new System.Drawing.Point(5, 43);
            this.labelSourceTitle.Name = "labelSourceTitle";
            this.labelSourceTitle.Size = new System.Drawing.Size(100, 20);
            this.labelSourceTitle.TabIndex = 8;
            this.labelSourceTitle.Values.Text = "Source path:";
            //
            // textSourcePath
            //
            this.textSourcePath.Location = new System.Drawing.Point(110, 40);
            this.textSourcePath.Name = "textSourcePath";
            this.textSourcePath.ReadOnly = true;
            this.textSourcePath.Size = new System.Drawing.Size(400, 23);
            this.textSourcePath.TabIndex = 9;
            //
            // buttonBrowseSource
            //
            this.buttonBrowseSource.Location = new System.Drawing.Point(547, 40);
            this.buttonBrowseSource.Name = "buttonBrowseSource";
            this.buttonBrowseSource.Size = new System.Drawing.Size(116, 24);
            this.buttonBrowseSource.TabIndex = 12;
            this.buttonBrowseSource.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonBrowseSource.Values.Text = "Browse Source";
            this.buttonBrowseSource.Click += new System.EventHandler(this.BtnBrowseSource_Click);
            //
            // buttonClearSource
            //
            this.buttonClearSource.Location = new System.Drawing.Point(515, 40);
            this.buttonClearSource.Name = "buttonClearSource";
            this.buttonClearSource.Size = new System.Drawing.Size(26, 24);
            this.buttonClearSource.TabIndex = 10;
            this.buttonClearSource.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonClearSource.Values.Text = "X";
            this.buttonClearSource.Click += new System.EventHandler(this.BtnClearSource_Click);
            //
            // labelSourceRequired
            //
            this.labelSourceRequired.AutoSize = false;
            this.labelSourceRequired.Location = new System.Drawing.Point(672, 43);
            this.labelSourceRequired.Name = "labelSourceRequired";
            this.labelSourceRequired.Size = new System.Drawing.Size(90, 20);
            this.labelSourceRequired.StateCommon.ShortText.Color1 = System.Drawing.Color.Red;
            this.labelSourceRequired.TabIndex = 13;
            this.labelSourceRequired.Values.Text = "Required";
            //
            // buttonAddAll
            //
            this.buttonAddAll.AutoSize = true;
            this.buttonAddAll.Location = new System.Drawing.Point(600, 8);
            this.buttonAddAll.Name = "buttonAddAll";
            this.buttonAddAll.Size = new System.Drawing.Size(66, 26);
            this.buttonAddAll.TabIndex = 3;
            this.buttonAddAll.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonAddAll.Values.Text = " Add ALL ";
            this.buttonAddAll.Click += new System.EventHandler(this.BtnAddAll_Click);
            //
            // buttonClear
            //
            this.buttonClear.AutoSize = true;
            this.buttonClear.Location = new System.Drawing.Point(672, 8);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(16, 2, 2, 2);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(58, 26);
            this.buttonClear.TabIndex = 4;
            this.buttonClear.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonClear.Values.Text = " Clear ";
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
            this.comboSaveFormat.Location = new System.Drawing.Point(917, 8);
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
            this.buttonSave.Location = new System.Drawing.Point(985, 8);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(60, 26);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonSave.Values.Text = "Save";
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);

            //
            // buttonHelp
            //
            this.buttonHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHelp.AutoSize = true;
            this.buttonHelp.Location = new System.Drawing.Point(1051, 8);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(26, 26);
            this.buttonHelp.TabIndex = 16;
            this.buttonHelp.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonHelp.Values.Text = "?";
            this.buttonHelp.ToolTipValues.EnableToolTips = true;
            this.buttonHelp.ToolTipValues.Heading = "Hotkeys";
            this.buttonHelp.ToolTipValues.Description = "F6 – Edit cell colour\nCtrl+Z – Undo last colour change\nCtrl+F – Search colour\nCtrl+Shift+C – Filter by colour\nCtrl+Shift+F – Filter by name\nCtrl+Shift+R – Clear filters";
            this.buttonHelp.ToolTipValues.ToolTipPosition.PlacementMode = Krypton.Toolkit.PlacementMode.Bottom;
            //
            // buttonCancel
            //
            this.buttonCancel.AutoSize = true;
            this.buttonCancel.Location = new System.Drawing.Point(735, 8);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(63, 26);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonCancel.Values.Text = " Cancel ";
            this.buttonCancel.Visible = false;
            this.buttonCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            //
            // kryptonThemeComboBox
            //
            this.kryptonThemeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonThemeComboBox.DefaultPalette = Krypton.Toolkit.PaletteMode.Microsoft365Blue;
            this.kryptonThemeComboBox.IntegralHeight = false;
            this.kryptonThemeComboBox.Location = new System.Drawing.Point(917, 40);
            this.kryptonThemeComboBox.Name = "kryptonThemeComboBox";
            this.kryptonThemeComboBox.Size = new System.Drawing.Size(350, 22);
            this.kryptonThemeComboBox.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox.TabIndex = 15;
            //
            // statusStrip
            //
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 592);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1289, 22);
            this.statusStrip.TabIndex = 2;
            //
            // statusLabel
            //
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(39, 17);
            this.statusLabel.Text = "Ready";
            //
            // PaletteViewerForm
            //
            this.ClientSize = new System.Drawing.Size(1289, 614);
            this.Controls.Add(this.dataGridViewPalette);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.statusStrip);
            this.MinimumSize = new System.Drawing.Size(1150, 200);
            this.Name = "PaletteViewerForm";
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
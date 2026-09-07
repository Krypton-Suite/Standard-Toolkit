namespace TestForm;

partial class KryptonTagInputDemo
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
        this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
        this.kwlblInfo = new Krypton.Toolkit.KryptonWrapLabel();
        this.flowToolbar = new System.Windows.Forms.FlowLayoutPanel();
        this.klblTheme = new Krypton.Toolkit.KryptonLabel();
        this.kcmbTheme = new Krypton.Toolkit.KryptonThemeComboBox();
        this.ktiTags = new Krypton.Toolkit.Utilities.KryptonTagInputControl();
        this.flowOptions = new System.Windows.Forms.FlowLayoutPanel();
        this.chkAllowDuplicates = new Krypton.Toolkit.KryptonCheckBox();
        this.chkReadOnly = new Krypton.Toolkit.KryptonCheckBox();
        this.chkCommitOnComma = new Krypton.Toolkit.KryptonCheckBox();
        this.chkShowRemove = new Krypton.Toolkit.KryptonCheckBox();
        this.chkAllowCustom = new Krypton.Toolkit.KryptonCheckBox();
        this.klblMaxTags = new Krypton.Toolkit.KryptonLabel();
        this.nudMaxTags = new Krypton.Toolkit.KryptonNumericUpDown();
        this.kbtnAddUrgent = new Krypton.Toolkit.KryptonButton();
        this.kbtnClear = new Krypton.Toolkit.KryptonButton();
        this.klblCurrent = new Krypton.Toolkit.KryptonLabel();
        this.klbTags = new Krypton.Toolkit.KryptonListBox();
        this.kwlblStatus = new Krypton.Toolkit.KryptonWrapLabel();
        this.krtbLog = new Krypton.Toolkit.KryptonRichTextBox();
        ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
        this.kryptonPanel1.SuspendLayout();
        this.tableLayout.SuspendLayout();
        this.flowToolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ktiTags)).BeginInit();
        this.flowOptions.SuspendLayout();
        this.SuspendLayout();
        // 
        // kryptonPanel1
        // 
        this.kryptonPanel1.Controls.Add(this.tableLayout);
        this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
        this.kryptonPanel1.Name = "kryptonPanel1";
        this.kryptonPanel1.Padding = new System.Windows.Forms.Padding(12);
        this.kryptonPanel1.Size = new System.Drawing.Size(760, 520);
        this.kryptonPanel1.TabIndex = 0;
        // 
        // tableLayout
        // 
        this.tableLayout.ColumnCount = 2;
        this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
        this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
        this.tableLayout.Controls.Add(this.kwlblInfo, 0, 0);
        this.tableLayout.Controls.Add(this.flowToolbar, 0, 1);
        this.tableLayout.Controls.Add(this.ktiTags, 0, 2);
        this.tableLayout.Controls.Add(this.flowOptions, 0, 3);
        this.tableLayout.Controls.Add(this.kwlblStatus, 0, 4);
        this.tableLayout.Controls.Add(this.krtbLog, 0, 5);
        this.tableLayout.Controls.Add(this.klblCurrent, 1, 1);
        this.tableLayout.Controls.Add(this.klbTags, 1, 2);
        this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tableLayout.Location = new System.Drawing.Point(12, 12);
        this.tableLayout.Name = "tableLayout";
        this.tableLayout.RowCount = 6;
        this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
        this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
        this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
        this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
        this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
        this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.tableLayout.Size = new System.Drawing.Size(736, 496);
        this.tableLayout.TabIndex = 0;
        this.tableLayout.SetColumnSpan(this.kwlblInfo, 2);
        this.tableLayout.SetRowSpan(this.klbTags, 4);
        // 
        // kwlblInfo
        // 
        this.kwlblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kwlblInfo.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
        this.kwlblInfo.Location = new System.Drawing.Point(3, 3);
        this.kwlblInfo.Name = "kwlblInfo";
        this.kwlblInfo.Size = new System.Drawing.Size(730, 50);
        this.kwlblInfo.Text = "KryptonTagInputControl (Utilities). Type a tag and press Enter or comma to commit. Backspace removes the last chip when the input is empty. Tab moves focus. Suggestions include Bug/Feature/Security; category colours apply to matching names. Typing 'reject' is cancelled by TagAdding.";
        // 
        // flowToolbar
        // 
        this.flowToolbar.AutoSize = true;
        this.flowToolbar.Controls.Add(this.klblTheme);
        this.flowToolbar.Controls.Add(this.kcmbTheme);
        this.flowToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
        this.flowToolbar.Location = new System.Drawing.Point(0, 56);
        this.flowToolbar.Margin = new System.Windows.Forms.Padding(0);
        this.flowToolbar.Name = "flowToolbar";
        this.flowToolbar.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
        this.flowToolbar.Size = new System.Drawing.Size(515, 36);
        this.flowToolbar.TabIndex = 1;
        this.flowToolbar.WrapContents = false;
        // 
        // klblTheme
        // 
        this.klblTheme.Location = new System.Drawing.Point(3, 7);
        this.klblTheme.Name = "klblTheme";
        this.klblTheme.Size = new System.Drawing.Size(48, 20);
        this.klblTheme.TabIndex = 0;
        this.klblTheme.Values.Text = "Theme:";
        // 
        // kcmbTheme
        // 
        this.kcmbTheme.DefaultPalette = Krypton.Toolkit.PaletteMode.Global;
        this.kcmbTheme.DropDownWidth = 240;
        this.kcmbTheme.IntegralHeight = false;
        this.kcmbTheme.Location = new System.Drawing.Point(57, 7);
        this.kcmbTheme.Name = "kcmbTheme";
        this.kcmbTheme.Size = new System.Drawing.Size(240, 22);
        this.kcmbTheme.TabIndex = 1;
        // 
        // ktiTags
        // 
        this.ktiTags.Dock = System.Windows.Forms.DockStyle.Fill;
        this.ktiTags.Location = new System.Drawing.Point(3, 95);
        this.ktiTags.Name = "ktiTags";
        this.ktiTags.Size = new System.Drawing.Size(509, 84);
        this.ktiTags.TabIndex = 2;
        this.ktiTags.TagAdding += new System.EventHandler<Krypton.Toolkit.Utilities.KryptonTagCancelEventArgs>(this.ktiTags_TagAdding);
        this.ktiTags.TagAdded += new System.EventHandler<Krypton.Toolkit.Utilities.KryptonTagEventArgs>(this.ktiTags_TagAdded);
        this.ktiTags.TagRemoved += new System.EventHandler<Krypton.Toolkit.Utilities.KryptonTagEventArgs>(this.ktiTags_TagRemoved);
        // 
        // flowOptions
        // 
        this.flowOptions.AutoSize = true;
        this.flowOptions.Controls.Add(this.chkAllowDuplicates);
        this.flowOptions.Controls.Add(this.chkReadOnly);
        this.flowOptions.Controls.Add(this.chkCommitOnComma);
        this.flowOptions.Controls.Add(this.chkShowRemove);
        this.flowOptions.Controls.Add(this.chkAllowCustom);
        this.flowOptions.Controls.Add(this.klblMaxTags);
        this.flowOptions.Controls.Add(this.nudMaxTags);
        this.flowOptions.Controls.Add(this.kbtnAddUrgent);
        this.flowOptions.Controls.Add(this.kbtnClear);
        this.flowOptions.Dock = System.Windows.Forms.DockStyle.Fill;
        this.flowOptions.Location = new System.Drawing.Point(0, 182);
        this.flowOptions.Margin = new System.Windows.Forms.Padding(0);
        this.flowOptions.Name = "flowOptions";
        this.flowOptions.Size = new System.Drawing.Size(736, 72);
        this.flowOptions.TabIndex = 3;
        // 
        // chkAllowDuplicates
        // 
        this.chkAllowDuplicates.Location = new System.Drawing.Point(3, 3);
        this.chkAllowDuplicates.Name = "chkAllowDuplicates";
        this.chkAllowDuplicates.Size = new System.Drawing.Size(118, 20);
        this.chkAllowDuplicates.TabIndex = 0;
        this.chkAllowDuplicates.Values.Text = "Allow duplicates";
        this.chkAllowDuplicates.CheckedChanged += new System.EventHandler(this.chkAllowDuplicates_CheckedChanged);
        // 
        // chkReadOnly
        // 
        this.chkReadOnly.Location = new System.Drawing.Point(127, 3);
        this.chkReadOnly.Name = "chkReadOnly";
        this.chkReadOnly.Size = new System.Drawing.Size(78, 20);
        this.chkReadOnly.TabIndex = 1;
        this.chkReadOnly.Values.Text = "Read only";
        this.chkReadOnly.CheckedChanged += new System.EventHandler(this.chkReadOnly_CheckedChanged);
        // 
        // chkCommitOnComma
        // 
        this.chkCommitOnComma.Checked = true;
        this.chkCommitOnComma.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkCommitOnComma.Location = new System.Drawing.Point(211, 3);
        this.chkCommitOnComma.Name = "chkCommitOnComma";
        this.chkCommitOnComma.Size = new System.Drawing.Size(118, 20);
        this.chkCommitOnComma.TabIndex = 2;
        this.chkCommitOnComma.Values.Text = "Commit on comma";
        this.chkCommitOnComma.CheckedChanged += new System.EventHandler(this.chkCommitOnComma_CheckedChanged);
        // 
        // chkShowRemove
        // 
        this.chkShowRemove.Checked = true;
        this.chkShowRemove.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkShowRemove.Location = new System.Drawing.Point(335, 3);
        this.chkShowRemove.Name = "chkShowRemove";
        this.chkShowRemove.Size = new System.Drawing.Size(108, 20);
        this.chkShowRemove.TabIndex = 3;
        this.chkShowRemove.Values.Text = "Show remove";
        this.chkShowRemove.CheckedChanged += new System.EventHandler(this.chkShowRemove_CheckedChanged);
        // 
        // chkAllowCustom
        // 
        this.chkAllowCustom.Checked = true;
        this.chkAllowCustom.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkAllowCustom.Location = new System.Drawing.Point(449, 3);
        this.chkAllowCustom.Name = "chkAllowCustom";
        this.chkAllowCustom.Size = new System.Drawing.Size(118, 20);
        this.chkAllowCustom.TabIndex = 4;
        this.chkAllowCustom.Values.Text = "Allow custom tags";
        this.chkAllowCustom.CheckedChanged += new System.EventHandler(this.chkAllowCustom_CheckedChanged);
        // 
        // klblMaxTags
        // 
        this.klblMaxTags.Location = new System.Drawing.Point(3, 29);
        this.klblMaxTags.Name = "klblMaxTags";
        this.klblMaxTags.Size = new System.Drawing.Size(62, 20);
        this.klblMaxTags.TabIndex = 5;
        this.klblMaxTags.Values.Text = "Max tags:";
        // 
        // nudMaxTags
        // 
        this.nudMaxTags.Location = new System.Drawing.Point(71, 29);
        this.nudMaxTags.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
        this.nudMaxTags.Name = "nudMaxTags";
        this.nudMaxTags.Size = new System.Drawing.Size(56, 22);
        this.nudMaxTags.TabIndex = 6;
        this.nudMaxTags.ValueChanged += new System.EventHandler(this.nudMaxTags_ValueChanged);
        // 
        // kbtnAddUrgent
        // 
        this.kbtnAddUrgent.Location = new System.Drawing.Point(133, 29);
        this.kbtnAddUrgent.Name = "kbtnAddUrgent";
        this.kbtnAddUrgent.Size = new System.Drawing.Size(100, 24);
        this.kbtnAddUrgent.TabIndex = 7;
        this.kbtnAddUrgent.Values.Text = "Add Urgent";
        this.kbtnAddUrgent.Click += new System.EventHandler(this.kbtnAddUrgent_Click);
        // 
        // kbtnClear
        // 
        this.kbtnClear.Location = new System.Drawing.Point(239, 29);
        this.kbtnClear.Name = "kbtnClear";
        this.kbtnClear.Size = new System.Drawing.Size(75, 24);
        this.kbtnClear.TabIndex = 8;
        this.kbtnClear.Values.Text = "Clear";
        this.kbtnClear.Click += new System.EventHandler(this.kbtnClear_Click);
        // 
        // klblCurrent
        // 
        this.klblCurrent.Dock = System.Windows.Forms.DockStyle.Fill;
        this.klblCurrent.Location = new System.Drawing.Point(518, 59);
        this.klblCurrent.Name = "klblCurrent";
        this.klblCurrent.Size = new System.Drawing.Size(215, 30);
        this.klblCurrent.TabIndex = 4;
        this.klblCurrent.Values.Text = "Current tags";
        // 
        // klbTags
        // 
        this.klbTags.Dock = System.Windows.Forms.DockStyle.Fill;
        this.klbTags.Location = new System.Drawing.Point(518, 95);
        this.klbTags.Name = "klbTags";
        this.klbTags.Size = new System.Drawing.Size(215, 398);
        this.klbTags.TabIndex = 5;
        // 
        // kwlblStatus
        // 
        this.kwlblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kwlblStatus.Location = new System.Drawing.Point(3, 257);
        this.kwlblStatus.Name = "kwlblStatus";
        this.kwlblStatus.Size = new System.Drawing.Size(730, 22);
        this.kwlblStatus.Text = "No tags.";
        // 
        // krtbLog
        // 
        this.krtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
        this.krtbLog.Location = new System.Drawing.Point(3, 285);
        this.krtbLog.Name = "krtbLog";
        this.krtbLog.ReadOnly = true;
        this.krtbLog.Size = new System.Drawing.Size(509, 208);
        this.krtbLog.TabIndex = 6;
        // 
        // KryptonTagInputDemo
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(760, 520);
        this.Controls.Add(this.kryptonPanel1);
        this.Name = "KryptonTagInputDemo";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "KryptonTagInputControl Demo";
        this.Load += new System.EventHandler(this.KryptonTagInputDemo_Load);
        ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
        this.kryptonPanel1.ResumeLayout(false);
        this.tableLayout.ResumeLayout(false);
        this.tableLayout.PerformLayout();
        this.flowToolbar.ResumeLayout(false);
        this.flowToolbar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ktiTags)).EndInit();
        this.flowOptions.ResumeLayout(false);
        this.flowOptions.PerformLayout();
        this.ResumeLayout(false);
    }

    #endregion

    private Krypton.Toolkit.KryptonPanel kryptonPanel1;
    private System.Windows.Forms.TableLayoutPanel tableLayout;
    private Krypton.Toolkit.KryptonWrapLabel kwlblInfo;
    private System.Windows.Forms.FlowLayoutPanel flowToolbar;
    private Krypton.Toolkit.KryptonLabel klblTheme;
    private Krypton.Toolkit.KryptonThemeComboBox kcmbTheme;
    private Krypton.Toolkit.Utilities.KryptonTagInputControl ktiTags;
    private System.Windows.Forms.FlowLayoutPanel flowOptions;
    private Krypton.Toolkit.KryptonCheckBox chkAllowDuplicates;
    private Krypton.Toolkit.KryptonCheckBox chkReadOnly;
    private Krypton.Toolkit.KryptonCheckBox chkCommitOnComma;
    private Krypton.Toolkit.KryptonCheckBox chkShowRemove;
    private Krypton.Toolkit.KryptonCheckBox chkAllowCustom;
    private Krypton.Toolkit.KryptonLabel klblMaxTags;
    private Krypton.Toolkit.KryptonNumericUpDown nudMaxTags;
    private Krypton.Toolkit.KryptonButton kbtnAddUrgent;
    private Krypton.Toolkit.KryptonButton kbtnClear;
    private Krypton.Toolkit.KryptonLabel klblCurrent;
    private Krypton.Toolkit.KryptonListBox klbTags;
    private Krypton.Toolkit.KryptonWrapLabel kwlblStatus;
    private Krypton.Toolkit.KryptonRichTextBox krtbLog;
}

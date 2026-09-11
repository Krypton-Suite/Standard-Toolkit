#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

partial class VisualStudioThemesDemo
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

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.kryptonManager1 = new Krypton.Toolkit.KryptonManager(this.components);
        this.kryptonPanelMain = new Krypton.Toolkit.KryptonPanel();
        this.kryptonPanelSamples = new Krypton.Toolkit.KryptonPanel();
        this.lstSample = new Krypton.Toolkit.KryptonListBox();
        this.txtSample = new Krypton.Toolkit.KryptonTextBox();
        this.chkSample = new Krypton.Toolkit.KryptonCheckBox();
        this.btnSample = new Krypton.Toolkit.KryptonButton();
        this.lblInstructions = new Krypton.Toolkit.KryptonWrapLabel();
        this.kryptonPanelToolbar = new Krypton.Toolkit.KryptonPanel();
        this.btnReset = new Krypton.Toolkit.KryptonButton();
        this.lblStatus = new Krypton.Toolkit.KryptonLabel();
        this.cmbVariant = new Krypton.Toolkit.KryptonComboBox();
        this.cmbYear = new Krypton.Toolkit.KryptonComboBox();
        this.lblVariant = new Krypton.Toolkit.KryptonLabel();
        this.lblYear = new Krypton.Toolkit.KryptonLabel();
        ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).BeginInit();
        this.kryptonPanelMain.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelSamples)).BeginInit();
        this.kryptonPanelSamples.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelToolbar)).BeginInit();
        this.kryptonPanelToolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.cmbVariant)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.cmbYear)).BeginInit();
        this.SuspendLayout();
        // 
        // kryptonPanelMain
        // 
        this.kryptonPanelMain.Controls.Add(this.kryptonPanelSamples);
        this.kryptonPanelMain.Controls.Add(this.kryptonPanelToolbar);
        this.kryptonPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kryptonPanelMain.Location = new System.Drawing.Point(0, 0);
        this.kryptonPanelMain.Name = "kryptonPanelMain";
        this.kryptonPanelMain.Size = new System.Drawing.Size(820, 480);
        this.kryptonPanelMain.TabIndex = 0;
        // 
        // kryptonPanelSamples
        // 
        this.kryptonPanelSamples.Controls.Add(this.lstSample);
        this.kryptonPanelSamples.Controls.Add(this.txtSample);
        this.kryptonPanelSamples.Controls.Add(this.chkSample);
        this.kryptonPanelSamples.Controls.Add(this.btnSample);
        this.kryptonPanelSamples.Controls.Add(this.lblInstructions);
        this.kryptonPanelSamples.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kryptonPanelSamples.Location = new System.Drawing.Point(0, 88);
        this.kryptonPanelSamples.Name = "kryptonPanelSamples";
        this.kryptonPanelSamples.Padding = new System.Windows.Forms.Padding(12);
        this.kryptonPanelSamples.Size = new System.Drawing.Size(820, 392);
        this.kryptonPanelSamples.TabIndex = 1;
        // 
        // lblInstructions
        // 
        this.lblInstructions.Dock = System.Windows.Forms.DockStyle.Top;
        this.lblInstructions.Location = new System.Drawing.Point(12, 12);
        this.lblInstructions.Name = "lblInstructions";
        this.lblInstructions.Size = new System.Drawing.Size(796, 56);
        this.lblInstructions.Text = "Issue #1083: pick a Visual Studio year and variant. 2012–2022: Dark/Light/Blue. 2026: Fluent Dark/Light (theme color tokens). Or pick a VS2010 Office renderer variation. Closing restores the previous global theme.";
        this.lblInstructions.AutoSize = false;
        // 
        // btnSample
        // 
        this.btnSample.Location = new System.Drawing.Point(15, 80);
        this.btnSample.Name = "btnSample";
        this.btnSample.Size = new System.Drawing.Size(140, 28);
        this.btnSample.TabIndex = 0;
        this.btnSample.Values.Text = "Sample button";
        // 
        // chkSample
        // 
        this.chkSample.Location = new System.Drawing.Point(170, 84);
        this.chkSample.Name = "chkSample";
        this.chkSample.Size = new System.Drawing.Size(140, 20);
        this.chkSample.TabIndex = 1;
        this.chkSample.Values.Text = "Sample checkbox";
        // 
        // txtSample
        // 
        this.txtSample.Location = new System.Drawing.Point(15, 120);
        this.txtSample.Name = "txtSample";
        this.txtSample.Size = new System.Drawing.Size(360, 23);
        this.txtSample.TabIndex = 2;
        this.txtSample.Text = "Sample text box";
        // 
        // lstSample
        // 
        this.lstSample.Location = new System.Drawing.Point(15, 160);
        this.lstSample.Name = "lstSample";
        this.lstSample.Size = new System.Drawing.Size(360, 160);
        this.lstSample.TabIndex = 3;
        this.lstSample.Items.AddRange(new object[] {
            "List item one",
            "List item two",
            "List item three",
            "List item four"});
        // 
        // kryptonPanelToolbar
        // 
        this.kryptonPanelToolbar.Controls.Add(this.btnReset);
        this.kryptonPanelToolbar.Controls.Add(this.lblStatus);
        this.kryptonPanelToolbar.Controls.Add(this.cmbVariant);
        this.kryptonPanelToolbar.Controls.Add(this.cmbYear);
        this.kryptonPanelToolbar.Controls.Add(this.lblVariant);
        this.kryptonPanelToolbar.Controls.Add(this.lblYear);
        this.kryptonPanelToolbar.Dock = System.Windows.Forms.DockStyle.Top;
        this.kryptonPanelToolbar.Location = new System.Drawing.Point(0, 0);
        this.kryptonPanelToolbar.Name = "kryptonPanelToolbar";
        this.kryptonPanelToolbar.Size = new System.Drawing.Size(820, 88);
        this.kryptonPanelToolbar.TabIndex = 0;
        // 
        // lblYear
        // 
        this.lblYear.Location = new System.Drawing.Point(12, 14);
        this.lblYear.Name = "lblYear";
        this.lblYear.Size = new System.Drawing.Size(36, 20);
        this.lblYear.TabIndex = 0;
        this.lblYear.Values.Text = "Year";
        // 
        // cmbYear
        // 
        this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbYear.DropDownWidth = 160;
        this.cmbYear.Location = new System.Drawing.Point(54, 12);
        this.cmbYear.Name = "cmbYear";
        this.cmbYear.Size = new System.Drawing.Size(160, 21);
        this.cmbYear.TabIndex = 1;
        this.cmbYear.SelectedIndexChanged += new System.EventHandler(this.cmbYear_SelectedIndexChanged);
        // 
        // lblVariant
        // 
        this.lblVariant.Location = new System.Drawing.Point(230, 14);
        this.lblVariant.Name = "lblVariant";
        this.lblVariant.Size = new System.Drawing.Size(48, 20);
        this.lblVariant.TabIndex = 2;
        this.lblVariant.Values.Text = "Variant";
        // 
        // cmbVariant
        // 
        this.cmbVariant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbVariant.DropDownWidth = 220;
        this.cmbVariant.Items.AddRange(new object[] {
            "Dark",
            "Light",
            "Blue"});
        this.cmbVariant.Location = new System.Drawing.Point(284, 12);
        this.cmbVariant.Name = "cmbVariant";
        this.cmbVariant.Size = new System.Drawing.Size(220, 21);
        this.cmbVariant.TabIndex = 3;
        this.cmbVariant.SelectedIndexChanged += new System.EventHandler(this.cmbVariant_SelectedIndexChanged);
        // 
        // btnReset
        // 
        this.btnReset.Location = new System.Drawing.Point(520, 10);
        this.btnReset.Name = "btnReset";
        this.btnReset.Size = new System.Drawing.Size(140, 25);
        this.btnReset.TabIndex = 4;
        this.btnReset.Values.Text = "Reset previous theme";
        this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
        // 
        // lblStatus
        // 
        this.lblStatus.Location = new System.Drawing.Point(12, 48);
        this.lblStatus.Name = "lblStatus";
        this.lblStatus.Size = new System.Drawing.Size(50, 20);
        this.lblStatus.TabIndex = 5;
        this.lblStatus.Values.Text = "Status";
        // 
        // VisualStudioThemesDemo
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(820, 480);
        this.Controls.Add(this.kryptonPanelMain);
        this.Name = "VisualStudioThemesDemo";
        this.Text = "Visual Studio Themes (#1083)";
        this.Load += new System.EventHandler(this.VisualStudioThemesDemo_Load);
        this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.VisualStudioThemesDemo_FormClosed);
        ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).EndInit();
        this.kryptonPanelMain.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelSamples)).EndInit();
        this.kryptonPanelSamples.ResumeLayout(false);
        this.kryptonPanelSamples.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelToolbar)).EndInit();
        this.kryptonPanelToolbar.ResumeLayout(false);
        this.kryptonPanelToolbar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.cmbVariant)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.cmbYear)).EndInit();
        this.ResumeLayout(false);
    }

    private Krypton.Toolkit.KryptonManager kryptonManager1;
    private Krypton.Toolkit.KryptonPanel kryptonPanelMain;
    private Krypton.Toolkit.KryptonPanel kryptonPanelSamples;
    private Krypton.Toolkit.KryptonPanel kryptonPanelToolbar;
    private Krypton.Toolkit.KryptonLabel lblYear;
    private Krypton.Toolkit.KryptonComboBox cmbYear;
    private Krypton.Toolkit.KryptonLabel lblVariant;
    private Krypton.Toolkit.KryptonComboBox cmbVariant;
    private Krypton.Toolkit.KryptonButton btnReset;
    private Krypton.Toolkit.KryptonLabel lblStatus;
    private Krypton.Toolkit.KryptonWrapLabel lblInstructions;
    private Krypton.Toolkit.KryptonButton btnSample;
    private Krypton.Toolkit.KryptonCheckBox chkSample;
    private Krypton.Toolkit.KryptonTextBox txtSample;
    private Krypton.Toolkit.KryptonListBox lstSample;
}

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

partial class LoadingCircleDemo
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
        this.chkActive = new Krypton.Toolkit.KryptonCheckBox();
        this.klblPreset = new Krypton.Toolkit.KryptonLabel();
        this.cmbPreset = new Krypton.Toolkit.KryptonComboBox();
        this.kbtnSetOverride = new Krypton.Toolkit.KryptonButton();
        this.kbtnResetOverride = new Krypton.Toolkit.KryptonButton();
        this.flowCircles = new System.Windows.Forms.FlowLayoutPanel();
        this.pnlPalette = new System.Windows.Forms.Panel();
        this.loadingPalette = new Krypton.Toolkit.Utilities.KryptonLoadingCircle();
        this.klblPalette = new Krypton.Toolkit.KryptonLabel();
        this.pnlOverride = new System.Windows.Forms.Panel();
        this.loadingOverride = new Krypton.Toolkit.Utilities.KryptonLoadingCircle();
        this.klblOverride = new Krypton.Toolkit.KryptonLabel();
        this.pnlDisabled = new System.Windows.Forms.Panel();
        this.loadingDisabled = new Krypton.Toolkit.Utilities.KryptonLoadingCircle();
        this.klblDisabled = new Krypton.Toolkit.KryptonLabel();
        this.kwlblStatus = new Krypton.Toolkit.KryptonWrapLabel();
        ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
        this.kryptonPanel1.SuspendLayout();
        this.tableLayout.SuspendLayout();
        this.flowToolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.cmbPreset)).BeginInit();
        this.flowCircles.SuspendLayout();
        this.pnlPalette.SuspendLayout();
        this.pnlOverride.SuspendLayout();
        this.pnlDisabled.SuspendLayout();
        this.SuspendLayout();
        // 
        // kryptonPanel1
        // 
        this.kryptonPanel1.Controls.Add(this.tableLayout);
        this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
        this.kryptonPanel1.Name = "kryptonPanel1";
        this.kryptonPanel1.Padding = new System.Windows.Forms.Padding(12);
        this.kryptonPanel1.Size = new System.Drawing.Size(720, 420);
        this.kryptonPanel1.TabIndex = 0;
        // 
        // tableLayout
        // 
        this.tableLayout.ColumnCount = 1;
        this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.tableLayout.Controls.Add(this.kwlblInfo, 0, 0);
        this.tableLayout.Controls.Add(this.flowToolbar, 0, 1);
        this.tableLayout.Controls.Add(this.flowCircles, 0, 2);
        this.tableLayout.Controls.Add(this.kwlblStatus, 0, 3);
        this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tableLayout.Location = new System.Drawing.Point(12, 12);
        this.tableLayout.Name = "tableLayout";
        this.tableLayout.RowCount = 4;
        this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
        this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
        this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
        this.tableLayout.Size = new System.Drawing.Size(696, 396);
        this.tableLayout.TabIndex = 0;
        // 
        // kwlblInfo
        // 
        this.kwlblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kwlblInfo.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
        this.kwlblInfo.Location = new System.Drawing.Point(3, 3);
        this.kwlblInfo.Name = "kwlblInfo";
        this.kwlblInfo.Size = new System.Drawing.Size(690, 66);
        this.kwlblInfo.Text = "Left: Color=Empty (inherits palette short-text). Middle: explicit override. Right: Disabled with Empty colour. Switch themes — the left and right spinners should track the theme; the middle stays SteelBlue / OrangeRed until Reset.";
        // 
        // flowToolbar
        // 
        this.flowToolbar.Controls.Add(this.klblTheme);
        this.flowToolbar.Controls.Add(this.kcmbTheme);
        this.flowToolbar.Controls.Add(this.chkActive);
        this.flowToolbar.Controls.Add(this.klblPreset);
        this.flowToolbar.Controls.Add(this.cmbPreset);
        this.flowToolbar.Controls.Add(this.kbtnSetOverride);
        this.flowToolbar.Controls.Add(this.kbtnResetOverride);
        this.flowToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
        this.flowToolbar.Location = new System.Drawing.Point(3, 75);
        this.flowToolbar.Name = "flowToolbar";
        this.flowToolbar.Size = new System.Drawing.Size(690, 38);
        this.flowToolbar.TabIndex = 1;
        // 
        // klblTheme
        // 
        this.klblTheme.Location = new System.Drawing.Point(3, 8);
        this.klblTheme.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
        this.klblTheme.Name = "klblTheme";
        this.klblTheme.Size = new System.Drawing.Size(48, 20);
        this.klblTheme.Values.Text = "Theme:";
        // 
        // kcmbTheme
        // 
        this.kcmbTheme.DropDownWidth = 220;
        this.kcmbTheme.Location = new System.Drawing.Point(57, 5);
        this.kcmbTheme.Margin = new System.Windows.Forms.Padding(3, 5, 12, 3);
        this.kcmbTheme.Name = "kcmbTheme";
        this.kcmbTheme.Size = new System.Drawing.Size(200, 21);
        this.kcmbTheme.SelectedIndexChanged += new System.EventHandler(this.kcmbTheme_SelectedIndexChanged);
        // 
        // chkActive
        // 
        this.chkActive.Checked = true;
        this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkActive.Location = new System.Drawing.Point(272, 6);
        this.chkActive.Margin = new System.Windows.Forms.Padding(3, 6, 12, 3);
        this.chkActive.Name = "chkActive";
        this.chkActive.Size = new System.Drawing.Size(62, 20);
        this.chkActive.Values.Text = "Active";
        this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged);
        // 
        // klblPreset
        // 
        this.klblPreset.Location = new System.Drawing.Point(349, 8);
        this.klblPreset.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
        this.klblPreset.Name = "klblPreset";
        this.klblPreset.Size = new System.Drawing.Size(46, 20);
        this.klblPreset.Values.Text = "Preset:";
        // 
        // cmbPreset
        // 
        this.cmbPreset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbPreset.DropDownWidth = 120;
        this.cmbPreset.Location = new System.Drawing.Point(401, 5);
        this.cmbPreset.Margin = new System.Windows.Forms.Padding(3, 5, 12, 3);
        this.cmbPreset.Name = "cmbPreset";
        this.cmbPreset.Size = new System.Drawing.Size(110, 21);
        this.cmbPreset.SelectedIndexChanged += new System.EventHandler(this.cmbPreset_SelectedIndexChanged);
        // 
        // kbtnSetOverride
        // 
        this.kbtnSetOverride.Location = new System.Drawing.Point(526, 3);
        this.kbtnSetOverride.Name = "kbtnSetOverride";
        this.kbtnSetOverride.Size = new System.Drawing.Size(88, 25);
        this.kbtnSetOverride.Values.Text = "Set Override";
        this.kbtnSetOverride.Click += new System.EventHandler(this.kbtnSetOverride_Click);
        // 
        // kbtnResetOverride
        // 
        this.kbtnResetOverride.Location = new System.Drawing.Point(620, 3);
        this.kbtnResetOverride.Name = "kbtnResetOverride";
        this.kbtnResetOverride.Size = new System.Drawing.Size(55, 25);
        this.kbtnResetOverride.Values.Text = "Reset";
        this.kbtnResetOverride.Click += new System.EventHandler(this.kbtnResetOverride_Click);
        // 
        // flowCircles
        // 
        this.flowCircles.Controls.Add(this.pnlPalette);
        this.flowCircles.Controls.Add(this.pnlOverride);
        this.flowCircles.Controls.Add(this.pnlDisabled);
        this.flowCircles.Dock = System.Windows.Forms.DockStyle.Fill;
        this.flowCircles.Location = new System.Drawing.Point(3, 119);
        this.flowCircles.Name = "flowCircles";
        this.flowCircles.Size = new System.Drawing.Size(690, 234);
        this.flowCircles.TabIndex = 2;
        // 
        // pnlPalette
        // 
        this.pnlPalette.Controls.Add(this.loadingPalette);
        this.pnlPalette.Controls.Add(this.klblPalette);
        this.pnlPalette.Location = new System.Drawing.Point(3, 3);
        this.pnlPalette.Name = "pnlPalette";
        this.pnlPalette.Size = new System.Drawing.Size(160, 140);
        this.pnlPalette.TabIndex = 0;
        // 
        // loadingPalette
        // 
        this.loadingPalette.Location = new System.Drawing.Point(48, 40);
        this.loadingPalette.Name = "loadingPalette";
        this.loadingPalette.Size = new System.Drawing.Size(64, 64);
        this.loadingPalette.TabIndex = 0;
        // 
        // klblPalette
        // 
        this.klblPalette.Dock = System.Windows.Forms.DockStyle.Top;
        this.klblPalette.Location = new System.Drawing.Point(0, 0);
        this.klblPalette.Name = "klblPalette";
        this.klblPalette.Size = new System.Drawing.Size(160, 20);
        this.klblPalette.Values.Text = "Palette (Empty)";
        // 
        // pnlOverride
        // 
        this.pnlOverride.Controls.Add(this.loadingOverride);
        this.pnlOverride.Controls.Add(this.klblOverride);
        this.pnlOverride.Location = new System.Drawing.Point(169, 3);
        this.pnlOverride.Name = "pnlOverride";
        this.pnlOverride.Size = new System.Drawing.Size(160, 140);
        this.pnlOverride.TabIndex = 1;
        // 
        // loadingOverride
        // 
        this.loadingOverride.Location = new System.Drawing.Point(48, 40);
        this.loadingOverride.Name = "loadingOverride";
        this.loadingOverride.Size = new System.Drawing.Size(64, 64);
        this.loadingOverride.TabIndex = 0;
        // 
        // klblOverride
        // 
        this.klblOverride.Dock = System.Windows.Forms.DockStyle.Top;
        this.klblOverride.Location = new System.Drawing.Point(0, 0);
        this.klblOverride.Name = "klblOverride";
        this.klblOverride.Size = new System.Drawing.Size(160, 20);
        this.klblOverride.Values.Text = "Explicit override";
        // 
        // pnlDisabled
        // 
        this.pnlDisabled.Controls.Add(this.loadingDisabled);
        this.pnlDisabled.Controls.Add(this.klblDisabled);
        this.pnlDisabled.Location = new System.Drawing.Point(335, 3);
        this.pnlDisabled.Name = "pnlDisabled";
        this.pnlDisabled.Size = new System.Drawing.Size(160, 140);
        this.pnlDisabled.TabIndex = 2;
        // 
        // loadingDisabled
        // 
        this.loadingDisabled.Location = new System.Drawing.Point(48, 40);
        this.loadingDisabled.Name = "loadingDisabled";
        this.loadingDisabled.Size = new System.Drawing.Size(64, 64);
        this.loadingDisabled.TabIndex = 0;
        // 
        // klblDisabled
        // 
        this.klblDisabled.Dock = System.Windows.Forms.DockStyle.Top;
        this.klblDisabled.Location = new System.Drawing.Point(0, 0);
        this.klblDisabled.Name = "klblDisabled";
        this.klblDisabled.Size = new System.Drawing.Size(160, 20);
        this.klblDisabled.Values.Text = "Disabled (Empty)";
        // 
        // kwlblStatus
        // 
        this.kwlblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kwlblStatus.Location = new System.Drawing.Point(3, 359);
        this.kwlblStatus.Name = "kwlblStatus";
        this.kwlblStatus.Size = new System.Drawing.Size(690, 34);
        this.kwlblStatus.Text = "Status";
        // 
        // LoadingCircleDemo
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(720, 420);
        this.Controls.Add(this.kryptonPanel1);
        this.Name = "LoadingCircleDemo";
        this.Text = "LoadingCircle Palette Demo";
        this.Load += new System.EventHandler(this.LoadingCircleDemo_Load);
        ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
        this.kryptonPanel1.ResumeLayout(false);
        this.tableLayout.ResumeLayout(false);
        this.tableLayout.PerformLayout();
        this.flowToolbar.ResumeLayout(false);
        this.flowToolbar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.cmbPreset)).EndInit();
        this.flowCircles.ResumeLayout(false);
        this.pnlPalette.ResumeLayout(false);
        this.pnlPalette.PerformLayout();
        this.pnlOverride.ResumeLayout(false);
        this.pnlOverride.PerformLayout();
        this.pnlDisabled.ResumeLayout(false);
        this.pnlDisabled.PerformLayout();
        this.ResumeLayout(false);
    }

    #endregion

    private Krypton.Toolkit.KryptonPanel kryptonPanel1;
    private System.Windows.Forms.TableLayoutPanel tableLayout;
    private Krypton.Toolkit.KryptonWrapLabel kwlblInfo;
    private System.Windows.Forms.FlowLayoutPanel flowToolbar;
    private Krypton.Toolkit.KryptonLabel klblTheme;
    private Krypton.Toolkit.KryptonThemeComboBox kcmbTheme;
    private Krypton.Toolkit.KryptonCheckBox chkActive;
    private Krypton.Toolkit.KryptonLabel klblPreset;
    private Krypton.Toolkit.KryptonComboBox cmbPreset;
    private Krypton.Toolkit.KryptonButton kbtnSetOverride;
    private Krypton.Toolkit.KryptonButton kbtnResetOverride;
    private System.Windows.Forms.FlowLayoutPanel flowCircles;
    private System.Windows.Forms.Panel pnlPalette;
    private Krypton.Toolkit.Utilities.KryptonLoadingCircle loadingPalette;
    private Krypton.Toolkit.KryptonLabel klblPalette;
    private System.Windows.Forms.Panel pnlOverride;
    private Krypton.Toolkit.Utilities.KryptonLoadingCircle loadingOverride;
    private Krypton.Toolkit.KryptonLabel klblOverride;
    private System.Windows.Forms.Panel pnlDisabled;
    private Krypton.Toolkit.Utilities.KryptonLoadingCircle loadingDisabled;
    private Krypton.Toolkit.KryptonLabel klblDisabled;
    private Krypton.Toolkit.KryptonWrapLabel kwlblStatus;
}

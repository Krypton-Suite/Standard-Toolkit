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
        if (disposing)
        {
            components?.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
        this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
        this.kwlblInfo = new Krypton.Toolkit.KryptonWrapLabel();
        this.flowToolbar = new System.Windows.Forms.FlowLayoutPanel();
        this.klblTheme = new Krypton.Toolkit.KryptonLabel();
        this.kcmbTheme = new Krypton.Toolkit.KryptonThemeComboBox();
        this.grpSpinner = new Krypton.Toolkit.KryptonGroupBox();
        this.spinner = new Krypton.Toolkit.Utilities.KryptonLoadingCircle();
        this.flowOptions = new System.Windows.Forms.FlowLayoutPanel();
        this.chkActive = new Krypton.Toolkit.KryptonCheckBox();
        this.chkEnabled = new Krypton.Toolkit.KryptonCheckBox();
        this.chkUsePalette = new Krypton.Toolkit.KryptonCheckBox();
        this.klblPreset = new Krypton.Toolkit.KryptonLabel();
        this.cmbPreset = new Krypton.Toolkit.KryptonComboBox();
        this.btnOverrideRed = new Krypton.Toolkit.KryptonButton();
        this.btnResetColor = new Krypton.Toolkit.KryptonButton();
        this.kwlblStatus = new Krypton.Toolkit.KryptonWrapLabel();
        ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
        this.kpnlMain.SuspendLayout();
        this.tableLayout.SuspendLayout();
        this.flowToolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.grpSpinner)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.grpSpinner.Panel)).BeginInit();
        this.grpSpinner.Panel.SuspendLayout();
        this.grpSpinner.SuspendLayout();
        this.flowOptions.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.cmbPreset)).BeginInit();
        this.SuspendLayout();
        //
        // kpnlMain
        //
        this.kpnlMain.Controls.Add(this.tableLayout);
        this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kpnlMain.Location = new System.Drawing.Point(0, 0);
        this.kpnlMain.Name = "kpnlMain";
        this.kpnlMain.Padding = new System.Windows.Forms.Padding(12);
        this.kpnlMain.Size = new System.Drawing.Size(640, 360);
        this.kpnlMain.TabIndex = 0;
        //
        // tableLayout
        //
        this.tableLayout.ColumnCount = 1;
        this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.tableLayout.Controls.Add(this.kwlblInfo, 0, 0);
        this.tableLayout.Controls.Add(this.flowToolbar, 0, 1);
        this.tableLayout.Controls.Add(this.grpSpinner, 0, 2);
        this.tableLayout.Controls.Add(this.flowOptions, 0, 3);
        this.tableLayout.Controls.Add(this.kwlblStatus, 0, 4);
        this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tableLayout.Location = new System.Drawing.Point(12, 12);
        this.tableLayout.Name = "tableLayout";
        this.tableLayout.RowCount = 5;
        this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
        this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
        this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
        this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
        this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.tableLayout.Size = new System.Drawing.Size(616, 336);
        this.tableLayout.TabIndex = 0;
        //
        // kwlblInfo
        //
        this.kwlblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kwlblInfo.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
        this.kwlblInfo.Location = new System.Drawing.Point(3, 3);
        this.kwlblInfo.Name = "kwlblInfo";
        this.kwlblInfo.Size = new System.Drawing.Size(610, 58);
        this.kwlblInfo.Text = "KryptonLoadingCircle (Utilities): toolbox spinner for forms. With CircleValues.Color = Empty (default), spokes use the active palette LabelNormalControl colour and update on theme change. Uncheck Use palette colour or click Override red for an explicit colour. Toggle Active / Enabled and try MacOSX / Firefox / IE7 presets.";
        //
        // flowToolbar
        //
        this.flowToolbar.AutoSize = true;
        this.flowToolbar.Controls.Add(this.klblTheme);
        this.flowToolbar.Controls.Add(this.kcmbTheme);
        this.flowToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
        this.flowToolbar.Location = new System.Drawing.Point(0, 64);
        this.flowToolbar.Margin = new System.Windows.Forms.Padding(0);
        this.flowToolbar.Name = "flowToolbar";
        this.flowToolbar.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
        this.flowToolbar.Size = new System.Drawing.Size(616, 40);
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
        this.kcmbTheme.DropDownWidth = 280;
        this.kcmbTheme.IntegralHeight = false;
        this.kcmbTheme.Location = new System.Drawing.Point(57, 7);
        this.kcmbTheme.Name = "kcmbTheme";
        this.kcmbTheme.Size = new System.Drawing.Size(280, 22);
        this.kcmbTheme.TabIndex = 1;
        this.kcmbTheme.SelectedIndexChanged += new System.EventHandler(this.kcmbTheme_SelectedIndexChanged);
        //
        // grpSpinner
        //
        this.grpSpinner.Dock = System.Windows.Forms.DockStyle.Fill;
        this.grpSpinner.Location = new System.Drawing.Point(3, 107);
        this.grpSpinner.Name = "grpSpinner";
        this.grpSpinner.Size = new System.Drawing.Size(610, 134);
        this.grpSpinner.TabIndex = 2;
        this.grpSpinner.Values.Heading = "KryptonLoadingCircle";
        //
        // grpSpinner.Panel
        //
        this.grpSpinner.Panel.Controls.Add(this.spinner);
        //
        // spinner
        //
        this.spinner.Location = new System.Drawing.Point(24, 24);
        this.spinner.Name = "spinner";
        this.spinner.Size = new System.Drawing.Size(72, 72);
        this.spinner.TabIndex = 0;
        //
        // flowOptions
        //
        this.flowOptions.AutoSize = true;
        this.flowOptions.Controls.Add(this.chkActive);
        this.flowOptions.Controls.Add(this.chkEnabled);
        this.flowOptions.Controls.Add(this.chkUsePalette);
        this.flowOptions.Controls.Add(this.klblPreset);
        this.flowOptions.Controls.Add(this.cmbPreset);
        this.flowOptions.Controls.Add(this.btnOverrideRed);
        this.flowOptions.Controls.Add(this.btnResetColor);
        this.flowOptions.Dock = System.Windows.Forms.DockStyle.Fill;
        this.flowOptions.Location = new System.Drawing.Point(0, 244);
        this.flowOptions.Margin = new System.Windows.Forms.Padding(0);
        this.flowOptions.Name = "flowOptions";
        this.flowOptions.Size = new System.Drawing.Size(616, 72);
        this.flowOptions.TabIndex = 3;
        //
        // chkActive
        //
        this.chkActive.Location = new System.Drawing.Point(3, 3);
        this.chkActive.Name = "chkActive";
        this.chkActive.Size = new System.Drawing.Size(62, 20);
        this.chkActive.TabIndex = 0;
        this.chkActive.Values.Text = "Active";
        this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged);
        //
        // chkEnabled
        //
        this.chkEnabled.Checked = true;
        this.chkEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkEnabled.Location = new System.Drawing.Point(71, 3);
        this.chkEnabled.Name = "chkEnabled";
        this.chkEnabled.Size = new System.Drawing.Size(70, 20);
        this.chkEnabled.TabIndex = 1;
        this.chkEnabled.Values.Text = "Enabled";
        this.chkEnabled.CheckedChanged += new System.EventHandler(this.chkEnabled_CheckedChanged);
        //
        // chkUsePalette
        //
        this.chkUsePalette.Checked = true;
        this.chkUsePalette.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkUsePalette.Location = new System.Drawing.Point(147, 3);
        this.chkUsePalette.Name = "chkUsePalette";
        this.chkUsePalette.Size = new System.Drawing.Size(136, 20);
        this.chkUsePalette.TabIndex = 2;
        this.chkUsePalette.Values.Text = "Use palette colour";
        this.chkUsePalette.CheckedChanged += new System.EventHandler(this.chkUsePalette_CheckedChanged);
        //
        // klblPreset
        //
        this.klblPreset.Location = new System.Drawing.Point(289, 3);
        this.klblPreset.Name = "klblPreset";
        this.klblPreset.Size = new System.Drawing.Size(46, 20);
        this.klblPreset.TabIndex = 3;
        this.klblPreset.Values.Text = "Preset:";
        //
        // cmbPreset
        //
        this.cmbPreset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbPreset.DropDownWidth = 120;
        this.cmbPreset.IntegralHeight = false;
        this.cmbPreset.Location = new System.Drawing.Point(341, 3);
        this.cmbPreset.Name = "cmbPreset";
        this.cmbPreset.Size = new System.Drawing.Size(120, 21);
        this.cmbPreset.TabIndex = 4;
        this.cmbPreset.SelectedIndexChanged += new System.EventHandler(this.cmbPreset_SelectedIndexChanged);
        //
        // btnOverrideRed
        //
        this.btnOverrideRed.Location = new System.Drawing.Point(467, 3);
        this.btnOverrideRed.Name = "btnOverrideRed";
        this.btnOverrideRed.Size = new System.Drawing.Size(100, 25);
        this.btnOverrideRed.TabIndex = 5;
        this.btnOverrideRed.Values.Text = "Override red";
        this.btnOverrideRed.Click += new System.EventHandler(this.btnOverrideRed_Click);
        //
        // btnResetColor
        //
        this.btnResetColor.Location = new System.Drawing.Point(3, 34);
        this.btnResetColor.Name = "btnResetColor";
        this.btnResetColor.Size = new System.Drawing.Size(120, 25);
        this.btnResetColor.TabIndex = 6;
        this.btnResetColor.Values.Text = "Reset to palette";
        this.btnResetColor.Click += new System.EventHandler(this.btnResetColor_Click);
        //
        // kwlblStatus
        //
        this.kwlblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kwlblStatus.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
        this.kwlblStatus.Location = new System.Drawing.Point(3, 319);
        this.kwlblStatus.Name = "kwlblStatus";
        this.kwlblStatus.Size = new System.Drawing.Size(610, 14);
        this.kwlblStatus.Text = "Ready.";
        //
        // LoadingCircleDemo
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(640, 360);
        this.Controls.Add(this.kpnlMain);
        this.Name = "LoadingCircleDemo";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Loading Circle Spinner (Utilities)";
        this.Load += new System.EventHandler(this.LoadingCircleDemo_Load);
        ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
        this.kpnlMain.ResumeLayout(false);
        this.tableLayout.ResumeLayout(false);
        this.tableLayout.PerformLayout();
        this.flowToolbar.ResumeLayout(false);
        this.flowToolbar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.grpSpinner.Panel)).EndInit();
        this.grpSpinner.Panel.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.grpSpinner)).EndInit();
        this.grpSpinner.ResumeLayout(false);
        this.flowOptions.ResumeLayout(false);
        this.flowOptions.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.cmbPreset)).EndInit();
        this.ResumeLayout(false);
    }

    #endregion

    private Krypton.Toolkit.KryptonPanel kpnlMain;
    private System.Windows.Forms.TableLayoutPanel tableLayout;
    private Krypton.Toolkit.KryptonWrapLabel kwlblInfo;
    private System.Windows.Forms.FlowLayoutPanel flowToolbar;
    private Krypton.Toolkit.KryptonLabel klblTheme;
    private Krypton.Toolkit.KryptonThemeComboBox kcmbTheme;
    private Krypton.Toolkit.KryptonGroupBox grpSpinner;
    private Krypton.Toolkit.Utilities.KryptonLoadingCircle spinner;
    private System.Windows.Forms.FlowLayoutPanel flowOptions;
    private Krypton.Toolkit.KryptonCheckBox chkActive;
    private Krypton.Toolkit.KryptonCheckBox chkEnabled;
    private Krypton.Toolkit.KryptonCheckBox chkUsePalette;
    private Krypton.Toolkit.KryptonLabel klblPreset;
    private Krypton.Toolkit.KryptonComboBox cmbPreset;
    private Krypton.Toolkit.KryptonButton btnOverrideRed;
    private Krypton.Toolkit.KryptonButton btnResetColor;
    private Krypton.Toolkit.KryptonWrapLabel kwlblStatus;
}

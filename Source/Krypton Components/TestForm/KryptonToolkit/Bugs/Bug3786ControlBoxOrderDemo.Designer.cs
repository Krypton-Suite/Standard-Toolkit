#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class Bug3786ControlBoxOrderDemo
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
            this.kryptonPanelMain = new Krypton.Toolkit.KryptonPanel();
            this.grpDiagnostics = new Krypton.Toolkit.KryptonGroupBox();
            this.lblOpenSamples = new Krypton.Toolkit.KryptonLabel();
            this.lblResult = new Krypton.Toolkit.KryptonLabel();
            this.lblMeasured = new Krypton.Toolkit.KryptonLabel();
            this.lblExpected = new Krypton.Toolkit.KryptonLabel();
            this.lblHostTrafficEdge = new Krypton.Toolkit.KryptonLabel();
            this.lblHostRtl = new Krypton.Toolkit.KryptonLabel();
            this.lblHostPalette = new Krypton.Toolkit.KryptonLabel();
            this.grpScenarios = new Krypton.Toolkit.KryptonGroupBox();
            this.btnCloseSamples = new Krypton.Toolkit.KryptonButton();
            this.btnApplyToOpenSamples = new Krypton.Toolkit.KryptonButton();
            this.btnOpenAllScenarios = new Krypton.Toolkit.KryptonButton();
            this.btnOpenAquaTrafficLights = new Krypton.Toolkit.KryptonButton();
            this.btnOpenMacWindowsLayout = new Krypton.Toolkit.KryptonButton();
            this.btnOpenMacTrafficLights = new Krypton.Toolkit.KryptonButton();
            this.btnOpenStandardRtl = new Krypton.Toolkit.KryptonButton();
            this.btnOpenStandardLtr = new Krypton.Toolkit.KryptonButton();
            this.grpHostSettings = new Krypton.Toolkit.KryptonGroupBox();
            this.cmbTrafficLightEdge = new Krypton.Toolkit.KryptonComboBox();
            this.lblTrafficLightEdge = new Krypton.Toolkit.KryptonLabel();
            this.chkMaximizeBox = new Krypton.Toolkit.KryptonCheckBox();
            this.chkMinimizeBox = new Krypton.Toolkit.KryptonCheckBox();
            this.chkRtl = new Krypton.Toolkit.KryptonCheckBox();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.lblTheme = new Krypton.Toolkit.KryptonLabel();
            this.lblInstruction = new Krypton.Toolkit.KryptonWrapLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).BeginInit();
            this.kryptonPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpDiagnostics)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDiagnostics.Panel)).BeginInit();
            this.grpDiagnostics.Panel.SuspendLayout();
            this.grpDiagnostics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpScenarios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpScenarios.Panel)).BeginInit();
            this.grpScenarios.Panel.SuspendLayout();
            this.grpScenarios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpHostSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpHostSettings.Panel)).BeginInit();
            this.grpHostSettings.Panel.SuspendLayout();
            this.grpHostSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTrafficLightEdge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.SuspendLayout();
            //
            // kryptonPanelMain
            //
            this.kryptonPanelMain.Controls.Add(this.grpDiagnostics);
            this.kryptonPanelMain.Controls.Add(this.grpScenarios);
            this.kryptonPanelMain.Controls.Add(this.grpHostSettings);
            this.kryptonPanelMain.Controls.Add(this.lblInstruction);
            this.kryptonPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanelMain.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanelMain.Name = "kryptonPanelMain";
            this.kryptonPanelMain.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanelMain.Size = new System.Drawing.Size(934, 711);
            this.kryptonPanelMain.TabIndex = 0;
            //
            // lblInstruction
            //
            this.lblInstruction.AutoSize = false;
            this.lblInstruction.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInstruction.Location = new System.Drawing.Point(12, 12);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblInstruction.Size = new System.Drawing.Size(910, 88);
            this.lblInstruction.Text = "Issue #3786: verify KryptonForm control box order and RTL placement. Use the host settings on this window, open sample forms, or run all scenarios. Diagnostics probe the title bar using HitTest and report PASS/FAIL.";
            //
            // grpHostSettings
            //
            this.grpHostSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpHostSettings.Location = new System.Drawing.Point(12, 100);
            this.grpHostSettings.Name = "grpHostSettings";
            this.grpHostSettings.Size = new System.Drawing.Size(910, 150);
            this.grpHostSettings.TabIndex = 1;
            this.grpHostSettings.Values.Heading = "Host form settings (this window)";
            //
            // grpHostSettings.Panel
            //
            this.grpHostSettings.Panel.Controls.Add(this.cmbTrafficLightEdge);
            this.grpHostSettings.Panel.Controls.Add(this.lblTrafficLightEdge);
            this.grpHostSettings.Panel.Controls.Add(this.chkMaximizeBox);
            this.grpHostSettings.Panel.Controls.Add(this.chkMinimizeBox);
            this.grpHostSettings.Panel.Controls.Add(this.chkRtl);
            this.grpHostSettings.Panel.Controls.Add(this.kryptonThemeComboBox1);
            this.grpHostSettings.Panel.Controls.Add(this.lblTheme);
            //
            // lblTheme
            //
            this.lblTheme.Location = new System.Drawing.Point(14, 12);
            this.lblTheme.Name = "lblTheme";
            this.lblTheme.Size = new System.Drawing.Size(48, 20);
            this.lblTheme.Values.Text = "Theme";
            //
            // kryptonThemeComboBox1
            //
            this.kryptonThemeComboBox1.DefaultPalette = Krypton.Toolkit.PaletteMode.Global;
            this.kryptonThemeComboBox1.DropDownWidth = 260;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(68, 10);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(260, 22);
            this.kryptonThemeComboBox1.TabIndex = 0;
            //
            // chkRtl
            //
            this.chkRtl.Location = new System.Drawing.Point(350, 12);
            this.chkRtl.Name = "chkRtl";
            this.chkRtl.Size = new System.Drawing.Size(220, 20);
            this.chkRtl.Values.Text = "RTL (RightToLeftLayout + Yes)";
            //
            // chkMinimizeBox
            //
            this.chkMinimizeBox.Checked = true;
            this.chkMinimizeBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMinimizeBox.Location = new System.Drawing.Point(14, 44);
            this.chkMinimizeBox.Name = "chkMinimizeBox";
            this.chkMinimizeBox.Size = new System.Drawing.Size(104, 20);
            this.chkMinimizeBox.Values.Text = "MinimizeBox";
            //
            // chkMaximizeBox
            //
            this.chkMaximizeBox.Checked = true;
            this.chkMaximizeBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMaximizeBox.Location = new System.Drawing.Point(140, 44);
            this.chkMaximizeBox.Name = "chkMaximizeBox";
            this.chkMaximizeBox.Size = new System.Drawing.Size(104, 20);
            this.chkMaximizeBox.Values.Text = "MaximizeBox";
            //
            // lblTrafficLightEdge
            //
            this.lblTrafficLightEdge.Location = new System.Drawing.Point(14, 76);
            this.lblTrafficLightEdge.Name = "lblTrafficLightEdge";
            this.lblTrafficLightEdge.Size = new System.Drawing.Size(130, 20);
            this.lblTrafficLightEdge.Values.Text = "FormTrafficLightEdge";
            //
            // cmbTrafficLightEdge
            //
            this.cmbTrafficLightEdge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrafficLightEdge.DropDownWidth = 180;
            this.cmbTrafficLightEdge.Items.AddRange(new object[] {
            "Inherit",
            "Near",
            "Far"});
            this.cmbTrafficLightEdge.Location = new System.Drawing.Point(150, 74);
            this.cmbTrafficLightEdge.Name = "cmbTrafficLightEdge";
            this.cmbTrafficLightEdge.Size = new System.Drawing.Size(180, 22);
            this.cmbTrafficLightEdge.TabIndex = 1;
            //
            // grpScenarios
            //
            this.grpScenarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpScenarios.Location = new System.Drawing.Point(12, 250);
            this.grpScenarios.Name = "grpScenarios";
            this.grpScenarios.Size = new System.Drawing.Size(910, 190);
            this.grpScenarios.TabIndex = 2;
            this.grpScenarios.Values.Heading = "Sample windows";
            //
            // grpScenarios.Panel
            //
            this.grpScenarios.Panel.Controls.Add(this.btnCloseSamples);
            this.grpScenarios.Panel.Controls.Add(this.btnApplyToOpenSamples);
            this.grpScenarios.Panel.Controls.Add(this.btnOpenAllScenarios);
            this.grpScenarios.Panel.Controls.Add(this.btnOpenAquaTrafficLights);
            this.grpScenarios.Panel.Controls.Add(this.btnOpenMacWindowsLayout);
            this.grpScenarios.Panel.Controls.Add(this.btnOpenMacTrafficLights);
            this.grpScenarios.Panel.Controls.Add(this.btnOpenStandardRtl);
            this.grpScenarios.Panel.Controls.Add(this.btnOpenStandardLtr);
            //
            // btnOpenStandardLtr
            //
            this.btnOpenStandardLtr.Location = new System.Drawing.Point(14, 12);
            this.btnOpenStandardLtr.Name = "btnOpenStandardLtr";
            this.btnOpenStandardLtr.Size = new System.Drawing.Size(200, 28);
            this.btnOpenStandardLtr.TabIndex = 0;
            this.btnOpenStandardLtr.Values.Text = "Standard LTR";
            //
            // btnOpenStandardRtl
            //
            this.btnOpenStandardRtl.Location = new System.Drawing.Point(224, 12);
            this.btnOpenStandardRtl.Name = "btnOpenStandardRtl";
            this.btnOpenStandardRtl.Size = new System.Drawing.Size(200, 28);
            this.btnOpenStandardRtl.TabIndex = 1;
            this.btnOpenStandardRtl.Values.Text = "Standard RTL";
            //
            // btnOpenMacTrafficLights
            //
            this.btnOpenMacTrafficLights.Location = new System.Drawing.Point(434, 12);
            this.btnOpenMacTrafficLights.Name = "btnOpenMacTrafficLights";
            this.btnOpenMacTrafficLights.Size = new System.Drawing.Size(200, 28);
            this.btnOpenMacTrafficLights.TabIndex = 2;
            this.btnOpenMacTrafficLights.Values.Text = "macOS traffic lights";
            //
            // btnOpenMacWindowsLayout
            //
            this.btnOpenMacWindowsLayout.Location = new System.Drawing.Point(644, 12);
            this.btnOpenMacWindowsLayout.Name = "btnOpenMacWindowsLayout";
            this.btnOpenMacWindowsLayout.Size = new System.Drawing.Size(240, 28);
            this.btnOpenMacWindowsLayout.TabIndex = 3;
            this.btnOpenMacWindowsLayout.Values.Text = "macOS + Windows layout";
            //
            // btnOpenAquaTrafficLights
            //
            this.btnOpenAquaTrafficLights.Location = new System.Drawing.Point(14, 50);
            this.btnOpenAquaTrafficLights.Name = "btnOpenAquaTrafficLights";
            this.btnOpenAquaTrafficLights.Size = new System.Drawing.Size(200, 28);
            this.btnOpenAquaTrafficLights.TabIndex = 4;
            this.btnOpenAquaTrafficLights.Values.Text = "OS X Aqua traffic lights";
            //
            // btnOpenAllScenarios
            //
            this.btnOpenAllScenarios.Location = new System.Drawing.Point(224, 50);
            this.btnOpenAllScenarios.Name = "btnOpenAllScenarios";
            this.btnOpenAllScenarios.Size = new System.Drawing.Size(200, 28);
            this.btnOpenAllScenarios.TabIndex = 5;
            this.btnOpenAllScenarios.Values.Text = "Open all (2×2 grid)";
            //
            // btnApplyToOpenSamples
            //
            this.btnApplyToOpenSamples.Location = new System.Drawing.Point(434, 50);
            this.btnApplyToOpenSamples.Name = "btnApplyToOpenSamples";
            this.btnApplyToOpenSamples.Size = new System.Drawing.Size(240, 28);
            this.btnApplyToOpenSamples.TabIndex = 6;
            this.btnApplyToOpenSamples.Values.Text = "Apply host settings to open samples";
            //
            // btnCloseSamples
            //
            this.btnCloseSamples.Location = new System.Drawing.Point(684, 50);
            this.btnCloseSamples.Name = "btnCloseSamples";
            this.btnCloseSamples.Size = new System.Drawing.Size(200, 28);
            this.btnCloseSamples.TabIndex = 7;
            this.btnCloseSamples.Values.Text = "Close all sample windows";
            //
            // grpDiagnostics
            //
            this.grpDiagnostics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDiagnostics.Location = new System.Drawing.Point(12, 440);
            this.grpDiagnostics.Name = "grpDiagnostics";
            this.grpDiagnostics.Size = new System.Drawing.Size(910, 259);
            this.grpDiagnostics.TabIndex = 3;
            this.grpDiagnostics.Values.Heading = "Live diagnostics (host form)";
            //
            // grpDiagnostics.Panel
            //
            this.grpDiagnostics.Panel.Controls.Add(this.lblOpenSamples);
            this.grpDiagnostics.Panel.Controls.Add(this.lblResult);
            this.grpDiagnostics.Panel.Controls.Add(this.lblMeasured);
            this.grpDiagnostics.Panel.Controls.Add(this.lblExpected);
            this.grpDiagnostics.Panel.Controls.Add(this.lblHostTrafficEdge);
            this.grpDiagnostics.Panel.Controls.Add(this.lblHostRtl);
            this.grpDiagnostics.Panel.Controls.Add(this.lblHostPalette);
            //
            // lblHostPalette
            //
            this.lblHostPalette.Location = new System.Drawing.Point(14, 12);
            this.lblHostPalette.Name = "lblHostPalette";
            this.lblHostPalette.Size = new System.Drawing.Size(860, 20);
            this.lblHostPalette.Values.Text = "Global palette:";
            //
            // lblHostRtl
            //
            this.lblHostRtl.Location = new System.Drawing.Point(14, 36);
            this.lblHostRtl.Name = "lblHostRtl";
            this.lblHostRtl.Size = new System.Drawing.Size(860, 20);
            this.lblHostRtl.Values.Text = "RTL:";
            //
            // lblHostTrafficEdge
            //
            this.lblHostTrafficEdge.Location = new System.Drawing.Point(14, 60);
            this.lblHostTrafficEdge.Name = "lblHostTrafficEdge";
            this.lblHostTrafficEdge.Size = new System.Drawing.Size(860, 20);
            this.lblHostTrafficEdge.Values.Text = "FormTrafficLightEdge:";
            //
            // lblExpected
            //
            this.lblExpected.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.lblExpected.Location = new System.Drawing.Point(14, 92);
            this.lblExpected.Name = "lblExpected";
            this.lblExpected.Size = new System.Drawing.Size(860, 24);
            this.lblExpected.Values.Text = "Expected:";
            //
            // lblMeasured
            //
            this.lblMeasured.Location = new System.Drawing.Point(14, 120);
            this.lblMeasured.Name = "lblMeasured";
            this.lblMeasured.Size = new System.Drawing.Size(860, 24);
            this.lblMeasured.Values.Text = "Measured:";
            //
            // lblResult
            //
            this.lblResult.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this.lblResult.Location = new System.Drawing.Point(14, 152);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(860, 28);
            this.lblResult.Values.Text = "Result:";
            //
            // lblOpenSamples
            //
            this.lblOpenSamples.Location = new System.Drawing.Point(14, 188);
            this.lblOpenSamples.Name = "lblOpenSamples";
            this.lblOpenSamples.Size = new System.Drawing.Size(860, 20);
            this.lblOpenSamples.Values.Text = "Open sample windows: 0";
            //
            // Bug3786ControlBoxOrderDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 711);
            this.Controls.Add(this.kryptonPanelMain);
            this.MinimizeBox = true;
            this.Name = "Bug3786ControlBoxOrderDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bug 3786 — Control Box Order Demo";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).EndInit();
            this.kryptonPanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpDiagnostics.Panel)).EndInit();
            this.grpDiagnostics.Panel.ResumeLayout(false);
            this.grpDiagnostics.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpDiagnostics)).EndInit();
            this.grpDiagnostics.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpScenarios.Panel)).EndInit();
            this.grpScenarios.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpScenarios)).EndInit();
            this.grpScenarios.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpHostSettings.Panel)).EndInit();
            this.grpHostSettings.Panel.ResumeLayout(false);
            this.grpHostSettings.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpHostSettings)).EndInit();
            this.grpHostSettings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbTrafficLightEdge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanelMain;
        private Krypton.Toolkit.KryptonWrapLabel lblInstruction;
        private Krypton.Toolkit.KryptonGroupBox grpHostSettings;
        private Krypton.Toolkit.KryptonLabel lblTheme;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
        private Krypton.Toolkit.KryptonCheckBox chkRtl;
        private Krypton.Toolkit.KryptonCheckBox chkMinimizeBox;
        private Krypton.Toolkit.KryptonCheckBox chkMaximizeBox;
        private Krypton.Toolkit.KryptonLabel lblTrafficLightEdge;
        private Krypton.Toolkit.KryptonComboBox cmbTrafficLightEdge;
        private Krypton.Toolkit.KryptonGroupBox grpScenarios;
        private Krypton.Toolkit.KryptonButton btnOpenStandardLtr;
        private Krypton.Toolkit.KryptonButton btnOpenStandardRtl;
        private Krypton.Toolkit.KryptonButton btnOpenMacTrafficLights;
        private Krypton.Toolkit.KryptonButton btnOpenMacWindowsLayout;
        private Krypton.Toolkit.KryptonButton btnOpenAquaTrafficLights;
        private Krypton.Toolkit.KryptonButton btnOpenAllScenarios;
        private Krypton.Toolkit.KryptonButton btnApplyToOpenSamples;
        private Krypton.Toolkit.KryptonButton btnCloseSamples;
        private Krypton.Toolkit.KryptonGroupBox grpDiagnostics;
        private Krypton.Toolkit.KryptonLabel lblHostPalette;
        private Krypton.Toolkit.KryptonLabel lblHostRtl;
        private Krypton.Toolkit.KryptonLabel lblHostTrafficEdge;
        private Krypton.Toolkit.KryptonLabel lblExpected;
        private Krypton.Toolkit.KryptonLabel lblMeasured;
        private Krypton.Toolkit.KryptonLabel lblResult;
        private Krypton.Toolkit.KryptonLabel lblOpenSamples;
    }
}

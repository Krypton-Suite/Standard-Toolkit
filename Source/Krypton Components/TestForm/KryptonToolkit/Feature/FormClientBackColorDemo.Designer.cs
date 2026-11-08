#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

partial class FormClientBackColorDemo
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
        this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
        this.klblInstructions = new Krypton.Toolkit.KryptonLabel();
        this.kchkUseKryptonColors = new Krypton.Toolkit.KryptonCheckBox();
        this.klblTheme = new Krypton.Toolkit.KryptonLabel();
        this.kcmbTheme = new Krypton.Toolkit.KryptonComboBox();
        this.klblManagerPanelStyle = new Krypton.Toolkit.KryptonLabel();
        this.kcmbManagerPanelStyle = new Krypton.Toolkit.KryptonComboBox();
        this.klblPanelStyle = new Krypton.Toolkit.KryptonLabel();
        this.kcmbPanelStyle = new Krypton.Toolkit.KryptonComboBox();
        this.klblStatus = new Krypton.Toolkit.KryptonLabel();
        this.grpNative = new System.Windows.Forms.GroupBox();
        this.panelNative = new System.Windows.Forms.Panel();
        this.lblNative = new System.Windows.Forms.Label();
        this.btnNative = new System.Windows.Forms.Button();
        this.grpKrypton = new System.Windows.Forms.GroupBox();
        this.kryptonPanelSample = new Krypton.Toolkit.KryptonPanel();
        this.klblKrypton = new Krypton.Toolkit.KryptonLabel();
        this.kbtnKrypton = new Krypton.Toolkit.KryptonButton();
        ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
        this.kryptonPanel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbManagerPanelStyle)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbPanelStyle)).BeginInit();
        this.grpNative.SuspendLayout();
        this.panelNative.SuspendLayout();
        this.grpKrypton.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelSample)).BeginInit();
        this.kryptonPanelSample.SuspendLayout();
        this.SuspendLayout();
        // 
        // kryptonPanel1
        // 
        this.kryptonPanel1.Controls.Add(this.grpKrypton);
        this.kryptonPanel1.Controls.Add(this.grpNative);
        this.kryptonPanel1.Controls.Add(this.klblStatus);
        this.kryptonPanel1.Controls.Add(this.kcmbPanelStyle);
        this.kryptonPanel1.Controls.Add(this.klblPanelStyle);
        this.kryptonPanel1.Controls.Add(this.kcmbManagerPanelStyle);
        this.kryptonPanel1.Controls.Add(this.klblManagerPanelStyle);
        this.kryptonPanel1.Controls.Add(this.kcmbTheme);
        this.kryptonPanel1.Controls.Add(this.klblTheme);
        this.kryptonPanel1.Controls.Add(this.kchkUseKryptonColors);
        this.kryptonPanel1.Controls.Add(this.klblInstructions);
        this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
        this.kryptonPanel1.Name = "kryptonPanel1";
        this.kryptonPanel1.Padding = new System.Windows.Forms.Padding(12);
        this.kryptonPanel1.Size = new System.Drawing.Size(780, 560);
        this.kryptonPanel1.TabIndex = 0;
        // 
        // klblInstructions
        // 
        this.klblInstructions.Location = new System.Drawing.Point(16, 16);
        this.klblInstructions.Name = "klblInstructions";
        this.klblInstructions.Size = new System.Drawing.Size(740, 64);
        this.klblInstructions.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
        this.klblInstructions.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
        this.klblInstructions.TabIndex = 0;
        this.klblInstructions.Values.Text = "Issue #4685. Manager PanelStyle is the default for forms. Form PanelStyle Inherit keeps that default; choosing a style sets a local override. UseKryptonColorsOnForm off uses SystemColors.Control. Demo KryptonPanels follow the effective PanelBackStyle so paint matches Form.BackColor.";
        // 
        // kchkUseKryptonColors
        // 
        this.kchkUseKryptonColors.Location = new System.Drawing.Point(16, 92);
        this.kchkUseKryptonColors.Name = "kchkUseKryptonColors";
        this.kchkUseKryptonColors.Size = new System.Drawing.Size(360, 20);
        this.kchkUseKryptonColors.TabIndex = 1;
        this.kchkUseKryptonColors.Values.Text = "KryptonManager.UseKryptonColorsOnForm";
        this.kchkUseKryptonColors.CheckedChanged += new System.EventHandler(this.kchkUseKryptonColors_CheckedChanged);
        // 
        // klblTheme
        // 
        this.klblTheme.Location = new System.Drawing.Point(16, 124);
        this.klblTheme.Name = "klblTheme";
        this.klblTheme.Size = new System.Drawing.Size(60, 20);
        this.klblTheme.TabIndex = 2;
        this.klblTheme.Values.Text = "Theme";
        // 
        // kcmbTheme
        // 
        this.kcmbTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbTheme.DropDownWidth = 260;
        this.kcmbTheme.Items.AddRange(new object[] {
            "Microsoft 365 Blue",
            "Microsoft 365 Black Dark Mode",
            "Office 2010 Blue",
            "Office 2010 Black",
            "Sparkle Blue"});
        this.kcmbTheme.Location = new System.Drawing.Point(120, 120);
        this.kcmbTheme.Name = "kcmbTheme";
        this.kcmbTheme.Size = new System.Drawing.Size(260, 22);
        this.kcmbTheme.TabIndex = 3;
        this.kcmbTheme.SelectedIndexChanged += new System.EventHandler(this.kcmbTheme_SelectedIndexChanged);
        // 
        // klblManagerPanelStyle
        // 
        this.klblManagerPanelStyle.Location = new System.Drawing.Point(16, 160);
        this.klblManagerPanelStyle.Name = "klblManagerPanelStyle";
        this.klblManagerPanelStyle.Size = new System.Drawing.Size(100, 20);
        this.klblManagerPanelStyle.TabIndex = 4;
        this.klblManagerPanelStyle.Values.Text = "Manager style";
        // 
        // kcmbManagerPanelStyle
        // 
        this.kcmbManagerPanelStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbManagerPanelStyle.DropDownWidth = 260;
        this.kcmbManagerPanelStyle.Items.AddRange(new object[] {
            "PanelClient",
            "PanelAlternate",
            "PanelCustom1"});
        this.kcmbManagerPanelStyle.Location = new System.Drawing.Point(120, 156);
        this.kcmbManagerPanelStyle.Name = "kcmbManagerPanelStyle";
        this.kcmbManagerPanelStyle.Size = new System.Drawing.Size(260, 22);
        this.kcmbManagerPanelStyle.TabIndex = 5;
        this.kcmbManagerPanelStyle.SelectedIndexChanged += new System.EventHandler(this.kcmbManagerPanelStyle_SelectedIndexChanged);
        // 
        // klblPanelStyle
        // 
        this.klblPanelStyle.Location = new System.Drawing.Point(16, 196);
        this.klblPanelStyle.Name = "klblPanelStyle";
        this.klblPanelStyle.Size = new System.Drawing.Size(100, 20);
        this.klblPanelStyle.TabIndex = 6;
        this.klblPanelStyle.Values.Text = "Form style";
        // 
        // kcmbPanelStyle
        // 
        this.kcmbPanelStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbPanelStyle.DropDownWidth = 260;
        this.kcmbPanelStyle.Items.AddRange(new object[] {
            "Inherit",
            "PanelClient",
            "PanelAlternate",
            "PanelCustom1"});
        this.kcmbPanelStyle.Location = new System.Drawing.Point(120, 192);
        this.kcmbPanelStyle.Name = "kcmbPanelStyle";
        this.kcmbPanelStyle.Size = new System.Drawing.Size(260, 22);
        this.kcmbPanelStyle.TabIndex = 7;
        this.kcmbPanelStyle.SelectedIndexChanged += new System.EventHandler(this.kcmbPanelStyle_SelectedIndexChanged);
        // 
        // klblStatus
        // 
        this.klblStatus.Location = new System.Drawing.Point(400, 92);
        this.klblStatus.Name = "klblStatus";
        this.klblStatus.Size = new System.Drawing.Size(360, 120);
        this.klblStatus.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
        this.klblStatus.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
        this.klblStatus.TabIndex = 8;
        this.klblStatus.Values.Text = "Status";
        // 
        // grpNative
        // 
        this.grpNative.Controls.Add(this.panelNative);
        this.grpNative.Location = new System.Drawing.Point(16, 240);
        this.grpNative.Name = "grpNative";
        this.grpNative.Size = new System.Drawing.Size(360, 280);
        this.grpNative.TabIndex = 9;
        this.grpNative.TabStop = false;
        this.grpNative.Text = "Native WinForms (inherits BackColor)";
        // 
        // panelNative
        // 
        this.panelNative.Controls.Add(this.btnNative);
        this.panelNative.Controls.Add(this.lblNative);
        this.panelNative.Dock = System.Windows.Forms.DockStyle.Fill;
        this.panelNative.Location = new System.Drawing.Point(3, 16);
        this.panelNative.Name = "panelNative";
        this.panelNative.Padding = new System.Windows.Forms.Padding(12);
        this.panelNative.Size = new System.Drawing.Size(354, 261);
        this.panelNative.TabIndex = 0;
        // 
        // lblNative
        // 
        this.lblNative.AutoSize = true;
        this.lblNative.Location = new System.Drawing.Point(15, 20);
        this.lblNative.Name = "lblNative";
        this.lblNative.Size = new System.Drawing.Size(200, 13);
        this.lblNative.TabIndex = 0;
        this.lblNative.Text = "Native Label / Panel use parent BackColor";
        // 
        // btnNative
        // 
        this.btnNative.Location = new System.Drawing.Point(15, 50);
        this.btnNative.Name = "btnNative";
        this.btnNative.Size = new System.Drawing.Size(120, 28);
        this.btnNative.TabIndex = 1;
        this.btnNative.Text = "Native Button";
        this.btnNative.UseVisualStyleBackColor = true;
        // 
        // grpKrypton
        // 
        this.grpKrypton.Controls.Add(this.kryptonPanelSample);
        this.grpKrypton.Location = new System.Drawing.Point(400, 240);
        this.grpKrypton.Name = "grpKrypton";
        this.grpKrypton.Size = new System.Drawing.Size(360, 280);
        this.grpKrypton.TabIndex = 10;
        this.grpKrypton.TabStop = false;
        this.grpKrypton.Text = "Krypton controls";
        // 
        // kryptonPanelSample
        // 
        this.kryptonPanelSample.Controls.Add(this.kbtnKrypton);
        this.kryptonPanelSample.Controls.Add(this.klblKrypton);
        this.kryptonPanelSample.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kryptonPanelSample.Location = new System.Drawing.Point(3, 16);
        this.kryptonPanelSample.Name = "kryptonPanelSample";
        this.kryptonPanelSample.Padding = new System.Windows.Forms.Padding(12);
        this.kryptonPanelSample.Size = new System.Drawing.Size(354, 261);
        this.kryptonPanelSample.TabIndex = 0;
        // 
        // klblKrypton
        // 
        this.klblKrypton.Location = new System.Drawing.Point(15, 20);
        this.klblKrypton.Name = "klblKrypton";
        this.klblKrypton.Size = new System.Drawing.Size(300, 20);
        this.klblKrypton.TabIndex = 0;
        this.klblKrypton.Values.Text = "KryptonLabel on KryptonPanel";
        // 
        // kbtnKrypton
        // 
        this.kbtnKrypton.Location = new System.Drawing.Point(15, 50);
        this.kbtnKrypton.Name = "kbtnKrypton";
        this.kbtnKrypton.Size = new System.Drawing.Size(140, 28);
        this.kbtnKrypton.TabIndex = 1;
        this.kbtnKrypton.Values.Text = "Krypton Button";
        // 
        // FormClientBackColorDemo
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(780, 560);
        this.Controls.Add(this.kryptonPanel1);
        this.MinimumSize = new System.Drawing.Size(700, 500);
        this.Name = "FormClientBackColorDemo";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Form Client BackColor (#4685)";
        ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
        this.kryptonPanel1.ResumeLayout(false);
        this.kryptonPanel1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbManagerPanelStyle)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbPanelStyle)).EndInit();
        this.grpNative.ResumeLayout(false);
        this.panelNative.ResumeLayout(false);
        this.panelNative.PerformLayout();
        this.grpKrypton.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelSample)).EndInit();
        this.kryptonPanelSample.ResumeLayout(false);
        this.kryptonPanelSample.PerformLayout();
        this.ResumeLayout(false);
    }

    private Krypton.Toolkit.KryptonPanel kryptonPanel1;
    private Krypton.Toolkit.KryptonLabel klblInstructions;
    private Krypton.Toolkit.KryptonCheckBox kchkUseKryptonColors;
    private Krypton.Toolkit.KryptonLabel klblTheme;
    private Krypton.Toolkit.KryptonComboBox kcmbTheme;
    private Krypton.Toolkit.KryptonLabel klblManagerPanelStyle;
    private Krypton.Toolkit.KryptonComboBox kcmbManagerPanelStyle;
    private Krypton.Toolkit.KryptonLabel klblPanelStyle;
    private Krypton.Toolkit.KryptonComboBox kcmbPanelStyle;
    private Krypton.Toolkit.KryptonLabel klblStatus;
    private System.Windows.Forms.GroupBox grpNative;
    private System.Windows.Forms.Panel panelNative;
    private System.Windows.Forms.Label lblNative;
    private System.Windows.Forms.Button btnNative;
    private System.Windows.Forms.GroupBox grpKrypton;
    private Krypton.Toolkit.KryptonPanel kryptonPanelSample;
    private Krypton.Toolkit.KryptonLabel klblKrypton;
    private Krypton.Toolkit.KryptonButton kbtnKrypton;
}

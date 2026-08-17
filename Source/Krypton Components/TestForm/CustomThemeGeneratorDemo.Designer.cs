#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class CustomThemeGeneratorDemo
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                _dropperGlyph?.Dispose();
                _dropperGlyph = null;
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            kryptonManager1 = new KryptonManager(components);
            kwlblInfo = new KryptonWrapLabel();
            kpnlMain = new KryptonPanel();
            tlpMain = new TableLayoutPanel();
            klblName = new KryptonLabel();
            ktxtName = new KryptonTextBox();
            klblPrimary = new KryptonLabel();
            kbtnPrimary = new KryptonColorButton();
            ktxtPrimaryHex = new KryptonTextBox();
            kbtnPickPrimary = new KryptonButton();
            klblRgb = new KryptonLabel();
            ktxtPrimaryRgb = new KryptonTextBox();
            kbtnUseRgb = new KryptonButton();
            kchkSecondary = new KryptonCheckBox();
            kbtnSecondary = new KryptonColorButton();
            kbtnPickSecondary = new KryptonButton();
            kchkSurface = new KryptonCheckBox();
            kbtnSurface = new KryptonColorButton();
            kbtnPickSurface = new KryptonButton();
            klblDonor = new KryptonLabel();
            kcmbDonor = new KryptonComboBox();
            klblTheme = new KryptonLabel();
            kcmbTheme = new KryptonThemeComboBox();
            flpActions = new FlowLayoutPanel();
            kbtnApply = new KryptonButton();
            kbtnRegister = new KryptonButton();
            kbtnExport = new KryptonButton();
            kbtnBuilder = new KryptonButton();
            kbtnReset = new KryptonButton();
            khgPreview = new KryptonHeaderGroup();
            tlpPreview = new TableLayoutPanel();
            kbtnPreview = new KryptonButton();
            kchkPreview = new KryptonCheckButton();
            ktxtPreview = new KryptonTextBox();
            klblStatus = new KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)kpnlMain).BeginInit();
            kpnlMain.SuspendLayout();
            tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)kcmbDonor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kcmbTheme).BeginInit();
            flpActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)khgPreview).BeginInit();
            ((System.ComponentModel.ISupportInitialize)khgPreview.Panel).BeginInit();
            khgPreview.Panel.SuspendLayout();
            khgPreview.SuspendLayout();
            tlpPreview.SuspendLayout();
            SuspendLayout();
            //
            // kwlblInfo
            //
            kwlblInfo.Dock = DockStyle.Top;
            kwlblInfo.LabelStyle = LabelStyle.NormalControl;
            kwlblInfo.Location = new Point(0, 0);
            kwlblInfo.Name = "kwlblInfo";
            kwlblInfo.Padding = new Padding(12, 12, 12, 8);
            kwlblInfo.Size = new Size(820, 108);
            kwlblInfo.Text = "Issue #4234: generate a custom theme from a few colours. Enter hex (#0078D4) or RGB (0, 120, 212), or use the dropper to pick from the screen (PowerToys-style magnifier). Pick a donor family (light or dark), then Apply. Register adds the theme to the combo below. Export writes Krypton palette XML. Open Builder for the Utilities dialog.";
            //
            // kpnlMain
            //
            kpnlMain.Controls.Add(tlpMain);
            kpnlMain.Dock = DockStyle.Fill;
            kpnlMain.Location = new Point(0, 108);
            kpnlMain.Name = "kpnlMain";
            kpnlMain.Padding = new Padding(8);
            kpnlMain.Size = new Size(820, 432);
            kpnlMain.TabIndex = 0;
            //
            // tlpMain
            //
            tlpMain.ColumnCount = 4;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            tlpMain.Controls.Add(klblName, 0, 0);
            tlpMain.Controls.Add(ktxtName, 1, 0);
            tlpMain.Controls.Add(klblPrimary, 0, 1);
            tlpMain.Controls.Add(kbtnPrimary, 1, 1);
            tlpMain.Controls.Add(ktxtPrimaryHex, 2, 1);
            tlpMain.Controls.Add(kbtnPickPrimary, 3, 1);
            tlpMain.Controls.Add(klblRgb, 0, 2);
            tlpMain.Controls.Add(ktxtPrimaryRgb, 1, 2);
            tlpMain.Controls.Add(kbtnUseRgb, 2, 2);
            tlpMain.Controls.Add(kchkSecondary, 0, 3);
            tlpMain.Controls.Add(kbtnSecondary, 1, 3);
            tlpMain.Controls.Add(kbtnPickSecondary, 3, 3);
            tlpMain.Controls.Add(kchkSurface, 0, 4);
            tlpMain.Controls.Add(kbtnSurface, 1, 4);
            tlpMain.Controls.Add(kbtnPickSurface, 3, 4);
            tlpMain.Controls.Add(klblDonor, 0, 5);
            tlpMain.Controls.Add(kcmbDonor, 1, 5);
            tlpMain.Controls.Add(klblTheme, 0, 6);
            tlpMain.Controls.Add(kcmbTheme, 1, 6);
            tlpMain.Controls.Add(flpActions, 0, 7);
            tlpMain.Controls.Add(khgPreview, 0, 8);
            tlpMain.Controls.Add(klblStatus, 0, 9);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(8, 8);
            tlpMain.Name = "tlpMain";
            tlpMain.RowCount = 10;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tlpMain.Size = new Size(804, 416);
            tlpMain.TabIndex = 0;
            tlpMain.SetColumnSpan(ktxtName, 3);
            tlpMain.SetColumnSpan(kbtnUseRgb, 2);
            tlpMain.SetColumnSpan(kcmbDonor, 3);
            tlpMain.SetColumnSpan(kcmbTheme, 3);
            tlpMain.SetColumnSpan(flpActions, 4);
            tlpMain.SetColumnSpan(khgPreview, 4);
            tlpMain.SetColumnSpan(klblStatus, 4);
            //
            // klblName
            //
            klblName.Dock = DockStyle.Fill;
            klblName.Location = new Point(3, 3);
            klblName.Name = "klblName";
            klblName.Values.Text = "Name";
            //
            // ktxtName
            //
            ktxtName.Dock = DockStyle.Fill;
            ktxtName.Location = new Point(143, 3);
            ktxtName.Name = "ktxtName";
            ktxtName.TabIndex = 0;
            //
            // klblPrimary
            //
            klblPrimary.Dock = DockStyle.Fill;
            klblPrimary.Location = new Point(3, 35);
            klblPrimary.Name = "klblPrimary";
            klblPrimary.Values.Text = "Primary (hex)";
            //
            // kbtnPrimary
            //
            kbtnPrimary.Dock = DockStyle.Fill;
            kbtnPrimary.Location = new Point(143, 35);
            kbtnPrimary.Name = "kbtnPrimary";
            kbtnPrimary.TabIndex = 1;
            kbtnPrimary.Values.Text = "Pick";
            kbtnPrimary.SelectedColorChanged += OnColorChanged;
            //
            // ktxtPrimaryHex
            //
            ktxtPrimaryHex.Dock = DockStyle.Fill;
            ktxtPrimaryHex.Location = new Point(446, 35);
            ktxtPrimaryHex.Name = "ktxtPrimaryHex";
            ktxtPrimaryHex.TabIndex = 2;
            //
            // kbtnPickPrimary
            //
            kbtnPickPrimary.Dock = DockStyle.Fill;
            kbtnPickPrimary.Location = new Point(764, 35);
            kbtnPickPrimary.Name = "kbtnPickPrimary";
            kbtnPickPrimary.TabIndex = 3;
            kbtnPickPrimary.Values.Text = "";
            kbtnPickPrimary.Click += kbtnPickPrimary_Click;
            //
            // klblRgb
            //
            klblRgb.Dock = DockStyle.Fill;
            klblRgb.Location = new Point(3, 71);
            klblRgb.Name = "klblRgb";
            klblRgb.Values.Text = "Primary (RGB)";
            //
            // ktxtPrimaryRgb
            //
            ktxtPrimaryRgb.Dock = DockStyle.Fill;
            ktxtPrimaryRgb.Location = new Point(143, 71);
            ktxtPrimaryRgb.Name = "ktxtPrimaryRgb";
            ktxtPrimaryRgb.TabIndex = 3;
            //
            // kbtnUseRgb
            //
            kbtnUseRgb.Dock = DockStyle.Fill;
            kbtnUseRgb.Location = new Point(446, 71);
            kbtnUseRgb.Name = "kbtnUseRgb";
            kbtnUseRgb.TabIndex = 4;
            kbtnUseRgb.Values.Text = "Use RGB";
            kbtnUseRgb.Click += kbtnUseRgb_Click;
            //
            // kchkSecondary
            //
            kchkSecondary.Dock = DockStyle.Fill;
            kchkSecondary.Location = new Point(3, 107);
            kchkSecondary.Name = "kchkSecondary";
            kchkSecondary.TabIndex = 5;
            kchkSecondary.Values.Text = "Secondary";
            kchkSecondary.CheckedChanged += OnSeedChanged;
            //
            // kbtnSecondary
            //
            kbtnSecondary.Dock = DockStyle.Fill;
            kbtnSecondary.Enabled = false;
            kbtnSecondary.Location = new Point(143, 107);
            kbtnSecondary.Name = "kbtnSecondary";
            kbtnSecondary.TabIndex = 6;
            kbtnSecondary.Values.Text = "Pick";
            //
            // kbtnPickSecondary
            //
            kbtnPickSecondary.Dock = DockStyle.Fill;
            kbtnPickSecondary.Location = new Point(764, 107);
            kbtnPickSecondary.Name = "kbtnPickSecondary";
            kbtnPickSecondary.TabIndex = 7;
            kbtnPickSecondary.Values.Text = "";
            kbtnPickSecondary.Click += kbtnPickSecondary_Click;
            //
            // kchkSurface
            //
            kchkSurface.Dock = DockStyle.Fill;
            kchkSurface.Location = new Point(3, 143);
            kchkSurface.Name = "kchkSurface";
            kchkSurface.TabIndex = 7;
            kchkSurface.Values.Text = "Surface";
            kchkSurface.CheckedChanged += OnSeedChanged;
            //
            // kbtnSurface
            //
            kbtnSurface.Dock = DockStyle.Fill;
            kbtnSurface.Enabled = false;
            kbtnSurface.Location = new Point(143, 143);
            kbtnSurface.Name = "kbtnSurface";
            kbtnSurface.TabIndex = 8;
            kbtnSurface.Values.Text = "Pick";
            //
            // kbtnPickSurface
            //
            kbtnPickSurface.Dock = DockStyle.Fill;
            kbtnPickSurface.Location = new Point(764, 143);
            kbtnPickSurface.Name = "kbtnPickSurface";
            kbtnPickSurface.TabIndex = 9;
            kbtnPickSurface.Values.Text = "";
            kbtnPickSurface.Click += kbtnPickSurface_Click;
            //
            // klblDonor
            //
            klblDonor.Dock = DockStyle.Fill;
            klblDonor.Location = new Point(3, 179);
            klblDonor.Name = "klblDonor";
            klblDonor.Values.Text = "Donor family";
            //
            // kcmbDonor
            //
            kcmbDonor.Dock = DockStyle.Fill;
            kcmbDonor.DropDownStyle = ComboBoxStyle.DropDownList;
            kcmbDonor.Location = new Point(143, 179);
            kcmbDonor.Name = "kcmbDonor";
            kcmbDonor.TabIndex = 9;
            //
            // klblTheme
            //
            klblTheme.Dock = DockStyle.Fill;
            klblTheme.Location = new Point(3, 215);
            klblTheme.Name = "klblTheme";
            klblTheme.Values.Text = "Theme selector";
            //
            // kcmbTheme
            //
            kcmbTheme.Dock = DockStyle.Fill;
            kcmbTheme.Location = new Point(143, 215);
            kcmbTheme.Name = "kcmbTheme";
            kcmbTheme.TabIndex = 10;
            //
            // flpActions
            //
            flpActions.Controls.Add(kbtnApply);
            flpActions.Controls.Add(kbtnRegister);
            flpActions.Controls.Add(kbtnExport);
            flpActions.Controls.Add(kbtnBuilder);
            flpActions.Controls.Add(kbtnReset);
            flpActions.Dock = DockStyle.Fill;
            flpActions.Location = new Point(3, 251);
            flpActions.Name = "flpActions";
            flpActions.Size = new Size(798, 34);
            flpActions.TabIndex = 11;
            //
            // kbtnApply
            //
            kbtnApply.Location = new Point(3, 3);
            kbtnApply.Name = "kbtnApply";
            kbtnApply.Size = new Size(100, 28);
            kbtnApply.TabIndex = 0;
            kbtnApply.Values.Text = "Apply";
            kbtnApply.Click += kbtnApply_Click;
            //
            // kbtnRegister
            //
            kbtnRegister.Location = new Point(109, 3);
            kbtnRegister.Name = "kbtnRegister";
            kbtnRegister.Size = new Size(110, 28);
            kbtnRegister.TabIndex = 1;
            kbtnRegister.Values.Text = "Register";
            kbtnRegister.Click += kbtnRegister_Click;
            //
            // kbtnExport
            //
            kbtnExport.Location = new Point(225, 3);
            kbtnExport.Name = "kbtnExport";
            kbtnExport.Size = new Size(110, 28);
            kbtnExport.TabIndex = 2;
            kbtnExport.Values.Text = "Export XML";
            kbtnExport.Click += kbtnExport_Click;
            //
            // kbtnBuilder
            //
            kbtnBuilder.Location = new Point(341, 3);
            kbtnBuilder.Name = "kbtnBuilder";
            kbtnBuilder.Size = new Size(120, 28);
            kbtnBuilder.TabIndex = 3;
            kbtnBuilder.Values.Text = "Open Builder";
            kbtnBuilder.Click += kbtnBuilder_Click;
            //
            // kbtnReset
            //
            kbtnReset.Location = new Point(467, 3);
            kbtnReset.Name = "kbtnReset";
            kbtnReset.Size = new Size(100, 28);
            kbtnReset.TabIndex = 4;
            kbtnReset.Values.Text = "Reset";
            kbtnReset.Click += kbtnReset_Click;
            //
            // khgPreview
            //
            khgPreview.Dock = DockStyle.Fill;
            khgPreview.Location = new Point(3, 291);
            khgPreview.Name = "khgPreview";
            khgPreview.Size = new Size(798, 94);
            khgPreview.TabIndex = 12;
            khgPreview.ValuesPrimary.Heading = "Preview";
            khgPreview.ValuesPrimary.Description = "Buttons and input follow the generated theme";
            khgPreview.Panel.Controls.Add(tlpPreview);
            //
            // tlpPreview
            //
            tlpPreview.ColumnCount = 3;
            tlpPreview.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tlpPreview.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tlpPreview.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            tlpPreview.Controls.Add(kbtnPreview, 0, 0);
            tlpPreview.Controls.Add(kchkPreview, 1, 0);
            tlpPreview.Controls.Add(ktxtPreview, 2, 0);
            tlpPreview.Dock = DockStyle.Fill;
            tlpPreview.Location = new Point(0, 0);
            tlpPreview.Name = "tlpPreview";
            tlpPreview.Padding = new Padding(8);
            tlpPreview.RowCount = 1;
            tlpPreview.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPreview.Size = new Size(796, 50);
            tlpPreview.TabIndex = 0;
            //
            // kbtnPreview
            //
            kbtnPreview.Dock = DockStyle.Fill;
            kbtnPreview.Location = new Point(11, 11);
            kbtnPreview.Name = "kbtnPreview";
            kbtnPreview.Values.Text = "Sample button";
            //
            // kchkPreview
            //
            kchkPreview.Dock = DockStyle.Fill;
            kchkPreview.Location = new Point(273, 11);
            kchkPreview.Name = "kchkPreview";
            kchkPreview.Values.Text = "Check button";
            //
            // ktxtPreview
            //
            ktxtPreview.Dock = DockStyle.Fill;
            ktxtPreview.Location = new Point(535, 11);
            ktxtPreview.Name = "ktxtPreview";
            ktxtPreview.Text = "Sample input";
            //
            // klblStatus
            //
            klblStatus.Dock = DockStyle.Fill;
            klblStatus.Location = new Point(3, 391);
            klblStatus.Name = "klblStatus";
            klblStatus.Values.Text = "Ready";
            //
            // CustomThemeGeneratorDemo
            //
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(820, 540);
            Controls.Add(kpnlMain);
            Controls.Add(kwlblInfo);
            MinimumSize = new Size(760, 480);
            Name = "CustomThemeGeneratorDemo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Custom Theme Generator (#4234)";
            FormClosed += CustomThemeGeneratorDemo_FormClosed;
            Load += CustomThemeGeneratorDemo_Load;
            ((System.ComponentModel.ISupportInitialize)kpnlMain).EndInit();
            kpnlMain.ResumeLayout(false);
            tlpMain.ResumeLayout(false);
            tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)kcmbDonor).EndInit();
            ((System.ComponentModel.ISupportInitialize)kcmbTheme).EndInit();
            flpActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)khgPreview.Panel).EndInit();
            khgPreview.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)khgPreview).EndInit();
            khgPreview.ResumeLayout(false);
            tlpPreview.ResumeLayout(false);
            tlpPreview.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private KryptonManager kryptonManager1;
        private KryptonWrapLabel kwlblInfo;
        private KryptonPanel kpnlMain;
        private TableLayoutPanel tlpMain;
        private KryptonLabel klblName;
        private KryptonTextBox ktxtName;
        private KryptonLabel klblPrimary;
        private KryptonColorButton kbtnPrimary;
        private KryptonTextBox ktxtPrimaryHex;
        private KryptonButton kbtnPickPrimary;
        private KryptonLabel klblRgb;
        private KryptonTextBox ktxtPrimaryRgb;
        private KryptonButton kbtnUseRgb;
        private KryptonCheckBox kchkSecondary;
        private KryptonColorButton kbtnSecondary;
        private KryptonButton kbtnPickSecondary;
        private KryptonCheckBox kchkSurface;
        private KryptonColorButton kbtnSurface;
        private KryptonButton kbtnPickSurface;
        private KryptonLabel klblDonor;
        private KryptonComboBox kcmbDonor;
        private KryptonLabel klblTheme;
        private KryptonThemeComboBox kcmbTheme;
        private FlowLayoutPanel flpActions;
        private KryptonButton kbtnApply;
        private KryptonButton kbtnRegister;
        private KryptonButton kbtnExport;
        private KryptonButton kbtnBuilder;
        private KryptonButton kbtnReset;
        private KryptonHeaderGroup khgPreview;
        private TableLayoutPanel tlpPreview;
        private KryptonButton kbtnPreview;
        private KryptonCheckButton kchkPreview;
        private KryptonTextBox ktxtPreview;
        private KryptonLabel klblStatus;
    }
}

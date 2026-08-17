#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities
{
    partial class VisualCustomThemeBuilderForm
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
            kpnlMain = new KryptonPanel();
            tlpMain = new TableLayoutPanel();
            kwlblInfo = new KryptonWrapLabel();
            tlpInputs = new TableLayoutPanel();
            klblName = new KryptonLabel();
            ktxtName = new KryptonTextBox();
            klblPrimary = new KryptonLabel();
            kbtnPrimary = new KryptonColorButton();
            ktxtPrimaryHex = new KryptonTextBox();
            kbtnPickPrimary = new KryptonButton();
            kchkSecondary = new KryptonCheckBox();
            kbtnSecondary = new KryptonColorButton();
            ktxtSecondaryHex = new KryptonTextBox();
            kbtnPickSecondary = new KryptonButton();
            kchkSurface = new KryptonCheckBox();
            kbtnSurface = new KryptonColorButton();
            ktxtSurfaceHex = new KryptonTextBox();
            kbtnPickSurface = new KryptonButton();
            klblDonor = new KryptonLabel();
            kcmbDonor = new KryptonComboBox();
            klblStatus = new KryptonLabel();
            khgPreview = new KryptonHeaderGroup();
            tlpPreview = new TableLayoutPanel();
            kbtnPreview = new KryptonButton();
            kchkPreview = new KryptonCheckButton();
            ktxtPreview = new KryptonTextBox();
            klblPreview = new KryptonLabel();
            kpnlButtons = new KryptonPanel();
            flpButtons = new FlowLayoutPanel();
            kbtnApply = new KryptonButton();
            kbtnRegister = new KryptonButton();
            kbtnExport = new KryptonButton();
            kbtnReset = new KryptonButton();
            kbtnClose = new KryptonButton();
            ((System.ComponentModel.ISupportInitialize)kpnlMain).BeginInit();
            kpnlMain.SuspendLayout();
            tlpMain.SuspendLayout();
            tlpInputs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)kcmbDonor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)khgPreview).BeginInit();
            ((System.ComponentModel.ISupportInitialize)khgPreview.Panel).BeginInit();
            khgPreview.Panel.SuspendLayout();
            khgPreview.SuspendLayout();
            tlpPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)kpnlButtons).BeginInit();
            kpnlButtons.SuspendLayout();
            flpButtons.SuspendLayout();
            SuspendLayout();
            //
            // kpnlMain
            //
            kpnlMain.Controls.Add(tlpMain);
            kpnlMain.Dock = DockStyle.Fill;
            kpnlMain.Location = new Point(0, 0);
            kpnlMain.Name = "kpnlMain";
            kpnlMain.Padding = new Padding(8);
            kpnlMain.Size = new Size(760, 430);
            kpnlMain.TabIndex = 0;
            //
            // tlpMain
            //
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tlpMain.Controls.Add(kwlblInfo, 0, 0);
            tlpMain.Controls.Add(tlpInputs, 0, 1);
            tlpMain.Controls.Add(khgPreview, 1, 1);
            tlpMain.Controls.Add(klblStatus, 0, 2);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(8, 8);
            tlpMain.Name = "tlpMain";
            tlpMain.RowCount = 3;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 96F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tlpMain.Size = new Size(744, 414);
            tlpMain.TabIndex = 0;
            tlpMain.SetColumnSpan(kwlblInfo, 2);
            tlpMain.SetColumnSpan(klblStatus, 2);
            //
            // kwlblInfo
            //
            kwlblInfo.Dock = DockStyle.Fill;
            kwlblInfo.LabelStyle = LabelStyle.NormalControl;
            kwlblInfo.Location = new Point(3, 3);
            kwlblInfo.Name = "kwlblInfo";
            kwlblInfo.Text = "Provide a name and a few colours (hex such as #0078D4, or RGB such as 0,120,212), or use the dropper to pick from the screen (PowerToys-style magnifier). The generator remaps an Office 2010 or Microsoft 365 donor. Preview updates on this dialog only until you Apply.";
            //
            // tlpInputs
            //
            tlpInputs.ColumnCount = 4;
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            tlpInputs.Controls.Add(klblName, 0, 0);
            tlpInputs.Controls.Add(ktxtName, 1, 0);
            tlpInputs.Controls.Add(klblPrimary, 0, 1);
            tlpInputs.Controls.Add(kbtnPrimary, 1, 1);
            tlpInputs.Controls.Add(ktxtPrimaryHex, 2, 1);
            tlpInputs.Controls.Add(kbtnPickPrimary, 3, 1);
            tlpInputs.Controls.Add(kchkSecondary, 0, 2);
            tlpInputs.Controls.Add(kbtnSecondary, 1, 2);
            tlpInputs.Controls.Add(ktxtSecondaryHex, 2, 2);
            tlpInputs.Controls.Add(kbtnPickSecondary, 3, 2);
            tlpInputs.Controls.Add(kchkSurface, 0, 3);
            tlpInputs.Controls.Add(kbtnSurface, 1, 3);
            tlpInputs.Controls.Add(ktxtSurfaceHex, 2, 3);
            tlpInputs.Controls.Add(kbtnPickSurface, 3, 3);
            tlpInputs.Controls.Add(klblDonor, 0, 4);
            tlpInputs.Controls.Add(kcmbDonor, 1, 4);
            tlpInputs.Dock = DockStyle.Fill;
            tlpInputs.Location = new Point(3, 75);
            tlpInputs.Name = "tlpInputs";
            tlpInputs.RowCount = 6;
            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpInputs.Size = new Size(403, 308);
            tlpInputs.TabIndex = 1;
            tlpInputs.SetColumnSpan(ktxtName, 3);
            tlpInputs.SetColumnSpan(kcmbDonor, 3);
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
            ktxtName.TextChanged += OnSeedChanged;
            //
            // klblPrimary
            //
            klblPrimary.Dock = DockStyle.Fill;
            klblPrimary.Location = new Point(3, 35);
            klblPrimary.Name = "klblPrimary";
            klblPrimary.Values.Text = "Primary";
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
            ktxtPrimaryHex.Location = new Point(283, 35);
            ktxtPrimaryHex.Name = "ktxtPrimaryHex";
            ktxtPrimaryHex.TabIndex = 2;
            ktxtPrimaryHex.Leave += OnHexLeave;
            //
            // kbtnPickPrimary
            //
            kbtnPickPrimary.Dock = DockStyle.Fill;
            kbtnPickPrimary.Location = new Point(363, 35);
            kbtnPickPrimary.Name = "kbtnPickPrimary";
            kbtnPickPrimary.TabIndex = 3;
            kbtnPickPrimary.Values.Text = "";
            kbtnPickPrimary.Click += kbtnPickPrimary_Click;
            //
            // kchkSecondary
            //
            kchkSecondary.Dock = DockStyle.Fill;
            kchkSecondary.Location = new Point(3, 71);
            kchkSecondary.Name = "kchkSecondary";
            kchkSecondary.TabIndex = 3;
            kchkSecondary.Values.Text = "Secondary";
            kchkSecondary.CheckedChanged += OnSeedChanged;
            //
            // kbtnSecondary
            //
            kbtnSecondary.Dock = DockStyle.Fill;
            kbtnSecondary.Location = new Point(143, 71);
            kbtnSecondary.Name = "kbtnSecondary";
            kbtnSecondary.TabIndex = 4;
            kbtnSecondary.Values.Text = "Pick";
            kbtnSecondary.SelectedColorChanged += OnColorChanged;
            //
            // ktxtSecondaryHex
            //
            ktxtSecondaryHex.Dock = DockStyle.Fill;
            ktxtSecondaryHex.Location = new Point(283, 71);
            ktxtSecondaryHex.Name = "ktxtSecondaryHex";
            ktxtSecondaryHex.TabIndex = 6;
            ktxtSecondaryHex.Leave += OnHexLeave;
            //
            // kbtnPickSecondary
            //
            kbtnPickSecondary.Dock = DockStyle.Fill;
            kbtnPickSecondary.Location = new Point(363, 71);
            kbtnPickSecondary.Name = "kbtnPickSecondary";
            kbtnPickSecondary.TabIndex = 7;
            kbtnPickSecondary.Values.Text = "";
            kbtnPickSecondary.Click += kbtnPickSecondary_Click;
            //
            // kchkSurface
            //
            kchkSurface.Dock = DockStyle.Fill;
            kchkSurface.Location = new Point(3, 107);
            kchkSurface.Name = "kchkSurface";
            kchkSurface.TabIndex = 6;
            kchkSurface.Values.Text = "Surface";
            kchkSurface.CheckedChanged += OnSeedChanged;
            //
            // kbtnSurface
            //
            kbtnSurface.Dock = DockStyle.Fill;
            kbtnSurface.Location = new Point(143, 107);
            kbtnSurface.Name = "kbtnSurface";
            kbtnSurface.TabIndex = 7;
            kbtnSurface.Values.Text = "Pick";
            kbtnSurface.SelectedColorChanged += OnColorChanged;
            //
            // ktxtSurfaceHex
            //
            ktxtSurfaceHex.Dock = DockStyle.Fill;
            ktxtSurfaceHex.Location = new Point(283, 107);
            ktxtSurfaceHex.Name = "ktxtSurfaceHex";
            ktxtSurfaceHex.TabIndex = 10;
            ktxtSurfaceHex.Leave += OnHexLeave;
            //
            // kbtnPickSurface
            //
            kbtnPickSurface.Dock = DockStyle.Fill;
            kbtnPickSurface.Location = new Point(363, 107);
            kbtnPickSurface.Name = "kbtnPickSurface";
            kbtnPickSurface.TabIndex = 11;
            kbtnPickSurface.Values.Text = "";
            kbtnPickSurface.Click += kbtnPickSurface_Click;
            //
            // klblDonor
            //
            klblDonor.Dock = DockStyle.Fill;
            klblDonor.Location = new Point(3, 143);
            klblDonor.Name = "klblDonor";
            klblDonor.Values.Text = "Donor family";
            //
            // kcmbDonor
            //
            kcmbDonor.Dock = DockStyle.Fill;
            kcmbDonor.DropDownStyle = ComboBoxStyle.DropDownList;
            kcmbDonor.Location = new Point(143, 143);
            kcmbDonor.Name = "kcmbDonor";
            kcmbDonor.TabIndex = 9;
            kcmbDonor.SelectedIndexChanged += OnSeedChanged;
            //
            // klblStatus
            //
            klblStatus.Dock = DockStyle.Fill;
            klblStatus.Location = new Point(3, 389);
            klblStatus.Name = "klblStatus";
            klblStatus.Values.Text = "Ready";
            //
            // khgPreview
            //
            khgPreview.Dock = DockStyle.Fill;
            khgPreview.Location = new Point(412, 75);
            khgPreview.Name = "khgPreview";
            khgPreview.Size = new Size(329, 308);
            khgPreview.TabIndex = 2;
            khgPreview.ValuesPrimary.Heading = "Preview";
            khgPreview.ValuesPrimary.Description = "Button, check, input";
            khgPreview.Panel.Controls.Add(tlpPreview);
            //
            // tlpPreview
            //
            tlpPreview.ColumnCount = 1;
            tlpPreview.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPreview.Controls.Add(klblPreview, 0, 0);
            tlpPreview.Controls.Add(kbtnPreview, 0, 1);
            tlpPreview.Controls.Add(kchkPreview, 0, 2);
            tlpPreview.Controls.Add(ktxtPreview, 0, 3);
            tlpPreview.Dock = DockStyle.Fill;
            tlpPreview.Location = new Point(0, 0);
            tlpPreview.Name = "tlpPreview";
            tlpPreview.Padding = new Padding(8);
            tlpPreview.RowCount = 4;
            tlpPreview.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tlpPreview.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpPreview.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpPreview.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPreview.Size = new Size(327, 250);
            tlpPreview.TabIndex = 0;
            //
            // klblPreview
            //
            klblPreview.Dock = DockStyle.Fill;
            klblPreview.Location = new Point(11, 11);
            klblPreview.Name = "klblPreview";
            klblPreview.Values.Text = "Sample label";
            //
            // kbtnPreview
            //
            kbtnPreview.Dock = DockStyle.Fill;
            kbtnPreview.Location = new Point(11, 39);
            kbtnPreview.Name = "kbtnPreview";
            kbtnPreview.Values.Text = "Sample button";
            //
            // kchkPreview
            //
            kchkPreview.Dock = DockStyle.Fill;
            kchkPreview.Location = new Point(11, 75);
            kchkPreview.Name = "kchkPreview";
            kchkPreview.Values.Text = "Sample check button";
            //
            // ktxtPreview
            //
            ktxtPreview.Dock = DockStyle.Top;
            ktxtPreview.Location = new Point(11, 111);
            ktxtPreview.Name = "ktxtPreview";
            ktxtPreview.Text = "Sample input";
            //
            // kpnlButtons
            //
            kpnlButtons.Controls.Add(flpButtons);
            kpnlButtons.Dock = DockStyle.Bottom;
            kpnlButtons.Location = new Point(0, 430);
            kpnlButtons.Name = "kpnlButtons";
            kpnlButtons.PanelBackStyle = PaletteBackStyle.PanelAlternate;
            kpnlButtons.Size = new Size(760, 48);
            kpnlButtons.TabIndex = 1;
            //
            // flpButtons
            //
            flpButtons.Controls.Add(kbtnApply);
            flpButtons.Controls.Add(kbtnRegister);
            flpButtons.Controls.Add(kbtnExport);
            flpButtons.Controls.Add(kbtnReset);
            flpButtons.Controls.Add(kbtnClose);
            flpButtons.Dock = DockStyle.Fill;
            flpButtons.FlowDirection = FlowDirection.RightToLeft;
            flpButtons.Location = new Point(0, 0);
            flpButtons.Name = "flpButtons";
            flpButtons.Padding = new Padding(8, 8, 8, 8);
            flpButtons.Size = new Size(760, 48);
            flpButtons.TabIndex = 0;
            //
            // kbtnClose
            //
            kbtnClose.Location = new Point(8, 8);
            kbtnClose.Name = "kbtnClose";
            kbtnClose.Size = new Size(90, 28);
            kbtnClose.TabIndex = 4;
            kbtnClose.Values.Text = "Close";
            kbtnClose.Click += kbtnClose_Click;
            //
            // kbtnReset
            //
            kbtnReset.Location = new Point(104, 8);
            kbtnReset.Name = "kbtnReset";
            kbtnReset.Size = new Size(90, 28);
            kbtnReset.TabIndex = 3;
            kbtnReset.Values.Text = "Reset";
            kbtnReset.Click += kbtnReset_Click;
            //
            // kbtnExport
            //
            kbtnExport.Location = new Point(200, 8);
            kbtnExport.Name = "kbtnExport";
            kbtnExport.Size = new Size(90, 28);
            kbtnExport.TabIndex = 2;
            kbtnExport.Values.Text = "Export XML";
            kbtnExport.Click += kbtnExport_Click;
            //
            // kbtnRegister
            //
            kbtnRegister.Location = new Point(296, 8);
            kbtnRegister.Name = "kbtnRegister";
            kbtnRegister.Size = new Size(90, 28);
            kbtnRegister.TabIndex = 1;
            kbtnRegister.Values.Text = "Register";
            kbtnRegister.Click += kbtnRegister_Click;
            //
            // kbtnApply
            //
            kbtnApply.Location = new Point(392, 8);
            kbtnApply.Name = "kbtnApply";
            kbtnApply.Size = new Size(90, 28);
            kbtnApply.TabIndex = 0;
            kbtnApply.Values.Text = "Apply";
            kbtnApply.Click += kbtnApply_Click;
            //
            // VisualCustomThemeBuilderForm
            //
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(760, 502);
            Controls.Add(kpnlMain);
            Controls.Add(kpnlButtons);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(720, 504);
            Name = "VisualCustomThemeBuilderForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Custom Theme Builder";
            Load += VisualCustomThemeBuilderForm_Load;
            ((System.ComponentModel.ISupportInitialize)kpnlMain).EndInit();
            kpnlMain.ResumeLayout(false);
            tlpMain.ResumeLayout(false);
            tlpInputs.ResumeLayout(false);
            tlpInputs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)kcmbDonor).EndInit();
            ((System.ComponentModel.ISupportInitialize)khgPreview.Panel).EndInit();
            khgPreview.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)khgPreview).EndInit();
            khgPreview.ResumeLayout(false);
            tlpPreview.ResumeLayout(false);
            tlpPreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)kpnlButtons).EndInit();
            kpnlButtons.ResumeLayout(false);
            flpButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private KryptonPanel kpnlMain;
        private TableLayoutPanel tlpMain;
        private KryptonWrapLabel kwlblInfo;
        private TableLayoutPanel tlpInputs;
        private KryptonLabel klblName;
        private KryptonTextBox ktxtName;
        private KryptonLabel klblPrimary;
        private KryptonColorButton kbtnPrimary;
        private KryptonTextBox ktxtPrimaryHex;
        private KryptonButton kbtnPickPrimary;
        private KryptonCheckBox kchkSecondary;
        private KryptonColorButton kbtnSecondary;
        private KryptonTextBox ktxtSecondaryHex;
        private KryptonButton kbtnPickSecondary;
        private KryptonCheckBox kchkSurface;
        private KryptonColorButton kbtnSurface;
        private KryptonTextBox ktxtSurfaceHex;
        private KryptonButton kbtnPickSurface;
        private KryptonLabel klblDonor;
        private KryptonComboBox kcmbDonor;
        private KryptonLabel klblStatus;
        private KryptonHeaderGroup khgPreview;
        private TableLayoutPanel tlpPreview;
        private KryptonButton kbtnPreview;
        private KryptonCheckButton kchkPreview;
        private KryptonTextBox ktxtPreview;
        private KryptonLabel klblPreview;
        private KryptonPanel kpnlButtons;
        private FlowLayoutPanel flpButtons;
        private KryptonButton kbtnApply;
        private KryptonButton kbtnRegister;
        private KryptonButton kbtnExport;
        private KryptonButton kbtnReset;
        private KryptonButton kbtnClose;
    }
}

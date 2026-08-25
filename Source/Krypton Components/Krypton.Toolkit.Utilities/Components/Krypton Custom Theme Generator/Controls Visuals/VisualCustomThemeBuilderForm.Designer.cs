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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisualCustomThemeBuilderForm));
            this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.kwlblInfo = new Krypton.Toolkit.KryptonWrapLabel();
            this.tlpInputs = new System.Windows.Forms.TableLayoutPanel();
            this.klblName = new Krypton.Toolkit.KryptonLabel();
            this.ktxtName = new Krypton.Toolkit.KryptonTextBox();
            this.klblPrimary = new Krypton.Toolkit.KryptonLabel();
            this.kbtnPrimary = new Krypton.Toolkit.KryptonColorButton();
            this.ktxtPrimaryHex = new Krypton.Toolkit.KryptonTextBox();
            this.kbtnPickPrimary = new Krypton.Toolkit.KryptonButton();
            this.kchkSecondary = new Krypton.Toolkit.KryptonCheckBox();
            this.kbtnSecondary = new Krypton.Toolkit.KryptonColorButton();
            this.ktxtSecondaryHex = new Krypton.Toolkit.KryptonTextBox();
            this.kbtnPickSecondary = new Krypton.Toolkit.KryptonButton();
            this.kchkSurface = new Krypton.Toolkit.KryptonCheckBox();
            this.kbtnSurface = new Krypton.Toolkit.KryptonColorButton();
            this.ktxtSurfaceHex = new Krypton.Toolkit.KryptonTextBox();
            this.kbtnPickSurface = new Krypton.Toolkit.KryptonButton();
            this.klblDonor = new Krypton.Toolkit.KryptonLabel();
            this.kcmbDonor = new Krypton.Toolkit.KryptonComboBox();
            this.klblFlyout = new Krypton.Toolkit.KryptonLabel();
            this.kcmbFlyout = new Krypton.Toolkit.KryptonComboBox();
            this.klblMagnifierSize = new Krypton.Toolkit.KryptonLabel();
            this.knudMagnifierSize = new Krypton.Toolkit.KryptonNumericUpDown();
            this.klblFormats = new Krypton.Toolkit.KryptonLabel();
            this.kclbColorFormats = new Krypton.Toolkit.KryptonCheckedListBox();
            this.khgPreview = new Krypton.Toolkit.KryptonHeaderGroup();
            this.tlpPreview = new System.Windows.Forms.TableLayoutPanel();
            this.klblPreview = new Krypton.Toolkit.KryptonLabel();
            this.kbtnPreview = new Krypton.Toolkit.KryptonButton();
            this.kchkPreview = new Krypton.Toolkit.KryptonCheckButton();
            this.ktxtPreview = new Krypton.Toolkit.KryptonTextBox();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            this.kpnlButtons = new Krypton.Toolkit.KryptonPanel();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.flpButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.kbtnApply = new Krypton.Toolkit.KryptonButton();
            this.kbtnRegister = new Krypton.Toolkit.KryptonButton();
            this.kbtnExport = new Krypton.Toolkit.KryptonButton();
            this.kbtnReset = new Krypton.Toolkit.KryptonButton();
            this.kbtnRandom = new Krypton.Toolkit.KryptonButton();
            this.kbtnClose = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
            this.kpnlMain.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.tlpInputs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbDonor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbFlyout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.khgPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.khgPreview.Panel)).BeginInit();
            this.khgPreview.Panel.SuspendLayout();
            this.khgPreview.SuspendLayout();
            this.tlpPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).BeginInit();
            this.kpnlButtons.SuspendLayout();
            this.flpButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // kpnlMain
            // 
            this.kpnlMain.Controls.Add(this.tlpMain);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 0);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.Padding = new System.Windows.Forms.Padding(8);
            this.kpnlMain.Size = new System.Drawing.Size(760, 490);
            this.kpnlMain.TabIndex = 0;
            // 
            // tlpMain
            // 
            this.tlpMain.BackColor = System.Drawing.Color.Transparent;
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tlpMain.Controls.Add(this.kwlblInfo, 0, 0);
            this.tlpMain.Controls.Add(this.tlpInputs, 0, 1);
            this.tlpMain.Controls.Add(this.khgPreview, 1, 1);
            this.tlpMain.Controls.Add(this.klblStatus, 0, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(8, 8);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpMain.Size = new System.Drawing.Size(744, 474);
            this.tlpMain.TabIndex = 0;
            // 
            // kwlblInfo
            // 
            this.tlpMain.SetColumnSpan(this.kwlblInfo, 2);
            this.kwlblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblInfo.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblInfo.Location = new System.Drawing.Point(3, 0);
            this.kwlblInfo.Name = "kwlblInfo";
            this.kwlblInfo.Size = new System.Drawing.Size(738, 96);
            this.kwlblInfo.Text = resources.GetString("kwlblInfo.Text");
            // 
            // tlpInputs
            // 
            this.tlpInputs.ColumnCount = 4;
            this.tlpInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tlpInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpInputs.Controls.Add(this.klblName, 0, 0);
            this.tlpInputs.Controls.Add(this.ktxtName, 1, 0);
            this.tlpInputs.Controls.Add(this.klblPrimary, 0, 1);
            this.tlpInputs.Controls.Add(this.kbtnPrimary, 1, 1);
            this.tlpInputs.Controls.Add(this.ktxtPrimaryHex, 2, 1);
            this.tlpInputs.Controls.Add(this.kbtnPickPrimary, 3, 1);
            this.tlpInputs.Controls.Add(this.kchkSecondary, 0, 2);
            this.tlpInputs.Controls.Add(this.kbtnSecondary, 1, 2);
            this.tlpInputs.Controls.Add(this.ktxtSecondaryHex, 2, 2);
            this.tlpInputs.Controls.Add(this.kbtnPickSecondary, 3, 2);
            this.tlpInputs.Controls.Add(this.kchkSurface, 0, 3);
            this.tlpInputs.Controls.Add(this.kbtnSurface, 1, 3);
            this.tlpInputs.Controls.Add(this.ktxtSurfaceHex, 2, 3);
            this.tlpInputs.Controls.Add(this.kbtnPickSurface, 3, 3);
            this.tlpInputs.Controls.Add(this.klblDonor, 0, 4);
            this.tlpInputs.Controls.Add(this.kcmbDonor, 1, 4);
            this.tlpInputs.Controls.Add(this.klblFlyout, 0, 5);
            this.tlpInputs.Controls.Add(this.kcmbFlyout, 1, 5);
            this.tlpInputs.Controls.Add(this.klblMagnifierSize, 0, 6);
            this.tlpInputs.Controls.Add(this.knudMagnifierSize, 1, 6);
            this.tlpInputs.Controls.Add(this.klblFormats, 0, 7);
            this.tlpInputs.Controls.Add(this.kclbColorFormats, 1, 7);
            this.tlpInputs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpInputs.Location = new System.Drawing.Point(3, 99);
            this.tlpInputs.Name = "tlpInputs";
            this.tlpInputs.RowCount = 8;
            this.tlpInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInputs.Size = new System.Drawing.Size(403, 344);
            this.tlpInputs.TabIndex = 1;
            // 
            // klblName
            // 
            this.klblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblName.Location = new System.Drawing.Point(3, 3);
            this.klblName.Name = "klblName";
            this.klblName.Size = new System.Drawing.Size(134, 26);
            this.klblName.TabIndex = 0;
            this.klblName.Values.Text = "Name";
            // 
            // ktxtName
            // 
            this.tlpInputs.SetColumnSpan(this.ktxtName, 3);
            this.ktxtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtName.Location = new System.Drawing.Point(143, 3);
            this.ktxtName.Name = "ktxtName";
            this.ktxtName.Size = new System.Drawing.Size(257, 23);
            this.ktxtName.TabIndex = 0;
            // 
            // klblPrimary
            // 
            this.klblPrimary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblPrimary.Location = new System.Drawing.Point(3, 35);
            this.klblPrimary.Name = "klblPrimary";
            this.klblPrimary.Size = new System.Drawing.Size(134, 30);
            this.klblPrimary.TabIndex = 1;
            this.klblPrimary.Values.Text = "Primary";
            // 
            // kbtnPrimary
            // 
            this.kbtnPrimary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnPrimary.Location = new System.Drawing.Point(143, 35);
            this.kbtnPrimary.Name = "kbtnPrimary";
            this.kbtnPrimary.Size = new System.Drawing.Size(114, 30);
            this.kbtnPrimary.TabIndex = 1;
            this.kbtnPrimary.Values.Text = "Pick";
            // 
            // ktxtPrimaryHex
            // 
            this.ktxtPrimaryHex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtPrimaryHex.Location = new System.Drawing.Point(263, 35);
            this.ktxtPrimaryHex.Name = "ktxtPrimaryHex";
            this.ktxtPrimaryHex.Size = new System.Drawing.Size(97, 23);
            this.ktxtPrimaryHex.TabIndex = 2;
            // 
            // kbtnPickPrimary
            // 
            this.kbtnPickPrimary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnPickPrimary.Location = new System.Drawing.Point(366, 35);
            this.kbtnPickPrimary.Name = "kbtnPickPrimary";
            this.kbtnPickPrimary.Size = new System.Drawing.Size(34, 30);
            this.kbtnPickPrimary.TabIndex = 3;
            this.kbtnPickPrimary.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnPickPrimary.Values.Text = "";
            // 
            // kchkSecondary
            // 
            this.kchkSecondary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kchkSecondary.Location = new System.Drawing.Point(3, 71);
            this.kchkSecondary.Name = "kchkSecondary";
            this.kchkSecondary.Size = new System.Drawing.Size(134, 30);
            this.kchkSecondary.TabIndex = 3;
            this.kchkSecondary.Values.Text = "Secondary";
            // 
            // kbtnSecondary
            // 
            this.kbtnSecondary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnSecondary.Location = new System.Drawing.Point(143, 71);
            this.kbtnSecondary.Name = "kbtnSecondary";
            this.kbtnSecondary.Size = new System.Drawing.Size(114, 30);
            this.kbtnSecondary.TabIndex = 4;
            this.kbtnSecondary.Values.Text = "Pick";
            // 
            // ktxtSecondaryHex
            // 
            this.ktxtSecondaryHex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtSecondaryHex.Location = new System.Drawing.Point(263, 71);
            this.ktxtSecondaryHex.Name = "ktxtSecondaryHex";
            this.ktxtSecondaryHex.Size = new System.Drawing.Size(97, 23);
            this.ktxtSecondaryHex.TabIndex = 6;
            // 
            // kbtnPickSecondary
            // 
            this.kbtnPickSecondary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnPickSecondary.Location = new System.Drawing.Point(366, 71);
            this.kbtnPickSecondary.Name = "kbtnPickSecondary";
            this.kbtnPickSecondary.Size = new System.Drawing.Size(34, 30);
            this.kbtnPickSecondary.TabIndex = 7;
            this.kbtnPickSecondary.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnPickSecondary.Values.Text = "";
            // 
            // kchkSurface
            // 
            this.kchkSurface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kchkSurface.Location = new System.Drawing.Point(3, 107);
            this.kchkSurface.Name = "kchkSurface";
            this.kchkSurface.Size = new System.Drawing.Size(134, 30);
            this.kchkSurface.TabIndex = 6;
            this.kchkSurface.Values.Text = "Surface";
            // 
            // kbtnSurface
            // 
            this.kbtnSurface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnSurface.Location = new System.Drawing.Point(143, 107);
            this.kbtnSurface.Name = "kbtnSurface";
            this.kbtnSurface.Size = new System.Drawing.Size(114, 30);
            this.kbtnSurface.TabIndex = 7;
            this.kbtnSurface.Values.Text = "Pick";
            // 
            // ktxtSurfaceHex
            // 
            this.ktxtSurfaceHex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtSurfaceHex.Location = new System.Drawing.Point(263, 107);
            this.ktxtSurfaceHex.Name = "ktxtSurfaceHex";
            this.ktxtSurfaceHex.Size = new System.Drawing.Size(97, 23);
            this.ktxtSurfaceHex.TabIndex = 10;
            // 
            // kbtnPickSurface
            // 
            this.kbtnPickSurface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnPickSurface.Location = new System.Drawing.Point(366, 107);
            this.kbtnPickSurface.Name = "kbtnPickSurface";
            this.kbtnPickSurface.Size = new System.Drawing.Size(34, 30);
            this.kbtnPickSurface.TabIndex = 11;
            this.kbtnPickSurface.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnPickSurface.Values.Text = "";
            // 
            // klblDonor
            // 
            this.klblDonor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblDonor.Location = new System.Drawing.Point(3, 143);
            this.klblDonor.Name = "klblDonor";
            this.klblDonor.Size = new System.Drawing.Size(134, 30);
            this.klblDonor.TabIndex = 12;
            this.klblDonor.Values.Text = "Donor family";
            // 
            // kcmbDonor
            // 
            this.tlpInputs.SetColumnSpan(this.kcmbDonor, 3);
            this.kcmbDonor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kcmbDonor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbDonor.Location = new System.Drawing.Point(143, 143);
            this.kcmbDonor.Name = "kcmbDonor";
            this.kcmbDonor.Size = new System.Drawing.Size(257, 30);
            this.kcmbDonor.TabIndex = 9;
            // 
            // klblFlyout
            // 
            this.klblFlyout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblFlyout.Location = new System.Drawing.Point(3, 179);
            this.klblFlyout.Name = "klblFlyout";
            this.klblFlyout.Size = new System.Drawing.Size(134, 30);
            this.klblFlyout.TabIndex = 13;
            this.klblFlyout.Values.Text = "Picker flyout";
            // 
            // kcmbFlyout
            // 
            this.tlpInputs.SetColumnSpan(this.kcmbFlyout, 3);
            this.kcmbFlyout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kcmbFlyout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbFlyout.Location = new System.Drawing.Point(143, 179);
            this.kcmbFlyout.Name = "kcmbFlyout";
            this.kcmbFlyout.Size = new System.Drawing.Size(257, 30);
            this.kcmbFlyout.TabIndex = 10;
            // 
            // klblMagnifierSize
            // 
            this.klblMagnifierSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblMagnifierSize.Location = new System.Drawing.Point(3, 215);
            this.klblMagnifierSize.Name = "klblMagnifierSize";
            this.klblMagnifierSize.Size = new System.Drawing.Size(134, 30);
            this.klblMagnifierSize.TabIndex = 14;
            this.klblMagnifierSize.Values.Text = "Magnifier size";
            // 
            // knudMagnifierSize
            // 
            this.knudMagnifierSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.knudMagnifierSize.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.knudMagnifierSize.Location = new System.Drawing.Point(143, 215);
            this.knudMagnifierSize.Maximum = new decimal(new int[] {
            21,
            0,
            0,
            0});
            this.knudMagnifierSize.Minimum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.knudMagnifierSize.Name = "knudMagnifierSize";
            this.knudMagnifierSize.Size = new System.Drawing.Size(114, 30);
            this.knudMagnifierSize.TabIndex = 11;
            this.knudMagnifierSize.Value = new decimal(new int[] {
            11,
            0,
            0,
            0});
            // 
            // klblFormats
            // 
            this.klblFormats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblFormats.Location = new System.Drawing.Point(3, 251);
            this.klblFormats.Name = "klblFormats";
            this.klblFormats.Size = new System.Drawing.Size(134, 90);
            this.klblFormats.TabIndex = 15;
            this.klblFormats.Values.Text = "Colour formats";
            // 
            // kclbColorFormats
            // 
            this.kclbColorFormats.CheckOnClick = true;
            this.tlpInputs.SetColumnSpan(this.kclbColorFormats, 3);
            this.kclbColorFormats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kclbColorFormats.Location = new System.Drawing.Point(143, 251);
            this.kclbColorFormats.Name = "kclbColorFormats";
            this.kclbColorFormats.Size = new System.Drawing.Size(257, 90);
            this.kclbColorFormats.TabIndex = 12;
            // 
            // khgPreview
            // 
            this.khgPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.khgPreview.Location = new System.Drawing.Point(412, 99);
            // 
            // khgPreview.Panel
            // 
            this.khgPreview.Panel.Controls.Add(this.tlpPreview);
            this.khgPreview.Size = new System.Drawing.Size(329, 344);
            this.khgPreview.TabIndex = 2;
            this.khgPreview.ValuesPrimary.Description = "Button, check, input";
            this.khgPreview.ValuesPrimary.Heading = "Preview";
            // 
            // tlpPreview
            // 
            this.tlpPreview.BackColor = System.Drawing.Color.Transparent;
            this.tlpPreview.ColumnCount = 1;
            this.tlpPreview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPreview.Controls.Add(this.klblPreview, 0, 0);
            this.tlpPreview.Controls.Add(this.kbtnPreview, 0, 1);
            this.tlpPreview.Controls.Add(this.kchkPreview, 0, 2);
            this.tlpPreview.Controls.Add(this.ktxtPreview, 0, 3);
            this.tlpPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPreview.Location = new System.Drawing.Point(0, 0);
            this.tlpPreview.Name = "tlpPreview";
            this.tlpPreview.Padding = new System.Windows.Forms.Padding(8);
            this.tlpPreview.RowCount = 4;
            this.tlpPreview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpPreview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpPreview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpPreview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPreview.Size = new System.Drawing.Size(327, 286);
            this.tlpPreview.TabIndex = 0;
            // 
            // klblPreview
            // 
            this.klblPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblPreview.Location = new System.Drawing.Point(11, 11);
            this.klblPreview.Name = "klblPreview";
            this.klblPreview.Size = new System.Drawing.Size(305, 22);
            this.klblPreview.TabIndex = 0;
            this.klblPreview.Values.Text = "Sample label";
            // 
            // kbtnPreview
            // 
            this.kbtnPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnPreview.Location = new System.Drawing.Point(11, 39);
            this.kbtnPreview.Name = "kbtnPreview";
            this.kbtnPreview.Size = new System.Drawing.Size(305, 30);
            this.kbtnPreview.TabIndex = 1;
            this.kbtnPreview.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnPreview.Values.Text = "Sample button";
            // 
            // kchkPreview
            // 
            this.kchkPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kchkPreview.Location = new System.Drawing.Point(11, 75);
            this.kchkPreview.Name = "kchkPreview";
            this.kchkPreview.Size = new System.Drawing.Size(305, 30);
            this.kchkPreview.TabIndex = 2;
            this.kchkPreview.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kchkPreview.Values.Text = "Sample check button";
            // 
            // ktxtPreview
            // 
            this.ktxtPreview.Dock = System.Windows.Forms.DockStyle.Top;
            this.ktxtPreview.Location = new System.Drawing.Point(11, 111);
            this.ktxtPreview.Name = "ktxtPreview";
            this.ktxtPreview.Size = new System.Drawing.Size(305, 23);
            this.ktxtPreview.TabIndex = 3;
            this.ktxtPreview.Text = "Sample input";
            // 
            // klblStatus
            // 
            this.tlpMain.SetColumnSpan(this.klblStatus, 2);
            this.klblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblStatus.Location = new System.Drawing.Point(3, 449);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(738, 22);
            this.klblStatus.TabIndex = 3;
            this.klblStatus.Values.Text = "Ready";
            // 
            // kpnlButtons
            // 
            this.kpnlButtons.Controls.Add(this.kryptonBorderEdge1);
            this.kpnlButtons.Controls.Add(this.flpButtons);
            this.kpnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kpnlButtons.Location = new System.Drawing.Point(0, 490);
            this.kpnlButtons.Name = "kpnlButtons";
            this.kpnlButtons.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kpnlButtons.Size = new System.Drawing.Size(760, 48);
            this.kpnlButtons.TabIndex = 1;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(760, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // flpButtons
            // 
            this.flpButtons.BackColor = System.Drawing.Color.Transparent;
            this.flpButtons.Controls.Add(this.kbtnApply);
            this.flpButtons.Controls.Add(this.kbtnRegister);
            this.flpButtons.Controls.Add(this.kbtnExport);
            this.flpButtons.Controls.Add(this.kbtnReset);
            this.flpButtons.Controls.Add(this.kbtnRandom);
            this.flpButtons.Controls.Add(this.kbtnClose);
            this.flpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpButtons.Location = new System.Drawing.Point(0, 0);
            this.flpButtons.Name = "flpButtons";
            this.flpButtons.Padding = new System.Windows.Forms.Padding(8);
            this.flpButtons.Size = new System.Drawing.Size(760, 48);
            this.flpButtons.TabIndex = 0;
            // 
            // kbtnApply
            // 
            this.kbtnApply.Location = new System.Drawing.Point(651, 11);
            this.kbtnApply.Name = "kbtnApply";
            this.kbtnApply.Size = new System.Drawing.Size(90, 28);
            this.kbtnApply.TabIndex = 0;
            this.kbtnApply.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnApply.Values.Text = "Apply";
            // 
            // kbtnRegister
            // 
            this.kbtnRegister.Location = new System.Drawing.Point(555, 11);
            this.kbtnRegister.Name = "kbtnRegister";
            this.kbtnRegister.Size = new System.Drawing.Size(90, 28);
            this.kbtnRegister.TabIndex = 1;
            this.kbtnRegister.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnRegister.Values.Text = "Register";
            // 
            // kbtnExport
            // 
            this.kbtnExport.Location = new System.Drawing.Point(459, 11);
            this.kbtnExport.Name = "kbtnExport";
            this.kbtnExport.Size = new System.Drawing.Size(90, 28);
            this.kbtnExport.TabIndex = 2;
            this.kbtnExport.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnExport.Values.Text = "Export XML";
            // 
            // kbtnReset
            // 
            this.kbtnReset.Location = new System.Drawing.Point(363, 11);
            this.kbtnReset.Name = "kbtnReset";
            this.kbtnReset.Size = new System.Drawing.Size(90, 28);
            this.kbtnReset.TabIndex = 3;
            this.kbtnReset.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnReset.Values.Text = "Reset";
            // 
            // kbtnRandom
            // 
            this.kbtnRandom.Location = new System.Drawing.Point(267, 11);
            this.kbtnRandom.Name = "kbtnRandom";
            this.kbtnRandom.Size = new System.Drawing.Size(90, 28);
            this.kbtnRandom.TabIndex = 4;
            this.kbtnRandom.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnRandom.Values.Text = "Randomize";
            // 
            // kbtnClose
            // 
            this.kbtnClose.Location = new System.Drawing.Point(171, 11);
            this.kbtnClose.Name = "kbtnClose";
            this.kbtnClose.Size = new System.Drawing.Size(90, 28);
            this.kbtnClose.TabIndex = 4;
            this.kbtnClose.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnClose.Values.Text = "Close";
            // 
            // VisualCustomThemeBuilderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 538);
            this.Controls.Add(this.kpnlMain);
            this.Controls.Add(this.kpnlButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(720, 540);
            this.Name = "VisualCustomThemeBuilderForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Custom Theme Builder";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
            this.kpnlMain.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tlpInputs.ResumeLayout(false);
            this.tlpInputs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbDonor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbFlyout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.khgPreview.Panel)).EndInit();
            this.khgPreview.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.khgPreview)).EndInit();
            this.khgPreview.ResumeLayout(false);
            this.tlpPreview.ResumeLayout(false);
            this.tlpPreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).EndInit();
            this.kpnlButtons.ResumeLayout(false);
            this.kpnlButtons.PerformLayout();
            this.flpButtons.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private KryptonLabel klblFlyout;
        private KryptonComboBox kcmbFlyout;
        private KryptonLabel klblMagnifierSize;
        private KryptonNumericUpDown knudMagnifierSize;
        private KryptonLabel klblFormats;
        private KryptonCheckedListBox kclbColorFormats;
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
        private KryptonButton kbtnRandom;
        private KryptonButton kbtnClose;
        private KryptonBorderEdge kryptonBorderEdge1;
    }
}

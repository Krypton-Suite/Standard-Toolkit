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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomThemeGeneratorDemo));
            this.kryptonManager1 = new Krypton.Toolkit.KryptonManager(this.components);
            this.kwlblInfo = new Krypton.Toolkit.KryptonWrapLabel();
            this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.klblName = new Krypton.Toolkit.KryptonLabel();
            this.ktxtName = new Krypton.Toolkit.KryptonTextBox();
            this.klblPrimary = new Krypton.Toolkit.KryptonLabel();
            this.kbtnPrimary = new Krypton.Toolkit.KryptonColorButton();
            this.ktxtPrimaryHex = new Krypton.Toolkit.KryptonTextBox();
            this.kbtnPickPrimary = new Krypton.Toolkit.KryptonButton();
            this.klblRgb = new Krypton.Toolkit.KryptonLabel();
            this.ktxtPrimaryRgb = new Krypton.Toolkit.KryptonTextBox();
            this.kbtnUseRgb = new Krypton.Toolkit.KryptonButton();
            this.kchkSecondary = new Krypton.Toolkit.KryptonCheckBox();
            this.kbtnSecondary = new Krypton.Toolkit.KryptonColorButton();
            this.kbtnPickSecondary = new Krypton.Toolkit.KryptonButton();
            this.kchkSurface = new Krypton.Toolkit.KryptonCheckBox();
            this.kbtnSurface = new Krypton.Toolkit.KryptonColorButton();
            this.kbtnPickSurface = new Krypton.Toolkit.KryptonButton();
            this.klblDonor = new Krypton.Toolkit.KryptonLabel();
            this.kcmbDonor = new Krypton.Toolkit.KryptonComboBox();
            this.klblTheme = new Krypton.Toolkit.KryptonLabel();
            this.kcmbTheme = new Krypton.Toolkit.KryptonThemeComboBox();
            this.klblFlyout = new Krypton.Toolkit.KryptonLabel();
            this.kcmbFlyout = new Krypton.Toolkit.KryptonComboBox();
            this.klblMagnifierSize = new Krypton.Toolkit.KryptonLabel();
            this.knudMagnifierSize = new Krypton.Toolkit.KryptonNumericUpDown();
            this.klblFormats = new Krypton.Toolkit.KryptonLabel();
            this.kclbColorFormats = new Krypton.Toolkit.KryptonCheckedListBox();
            this.flpActions = new System.Windows.Forms.FlowLayoutPanel();
            this.kbtnApply = new Krypton.Toolkit.KryptonButton();
            this.kbtnRegister = new Krypton.Toolkit.KryptonButton();
            this.kbtnExport = new Krypton.Toolkit.KryptonButton();
            this.kbtnBuilder = new Krypton.Toolkit.KryptonButton();
            this.kbtnReset = new Krypton.Toolkit.KryptonButton();
            this.kbtnRandom = new Krypton.Toolkit.KryptonButton();
            this.khgPreview = new Krypton.Toolkit.KryptonHeaderGroup();
            this.tlpPreview = new System.Windows.Forms.TableLayoutPanel();
            this.kbtnPreview = new Krypton.Toolkit.KryptonButton();
            this.kchkPreview = new Krypton.Toolkit.KryptonCheckButton();
            this.ktxtPreview = new Krypton.Toolkit.KryptonTextBox();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
            this.kpnlMain.SuspendLayout();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbDonor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbFlyout)).BeginInit();
            this.flpActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.khgPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.khgPreview.Panel)).BeginInit();
            this.khgPreview.Panel.SuspendLayout();
            this.khgPreview.SuspendLayout();
            this.tlpPreview.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.ToolkitStrings.MessageBoxStrings.LessDetails = "L&ess Details...";
            this.kryptonManager1.ToolkitStrings.MessageBoxStrings.MoreDetails = "&More Details...";
            // 
            // kwlblInfo
            // 
            this.kwlblInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.kwlblInfo.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblInfo.Location = new System.Drawing.Point(0, 0);
            this.kwlblInfo.Name = "kwlblInfo";
            this.kwlblInfo.Padding = new System.Windows.Forms.Padding(12, 12, 12, 8);
            this.kwlblInfo.Size = new System.Drawing.Size(1221, 35);
            this.kwlblInfo.Text = resources.GetString("kwlblInfo.Text");
            // 
            // kpnlMain
            // 
            this.kpnlMain.Controls.Add(this.tlpMain);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 35);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.Padding = new System.Windows.Forms.Padding(8);
            this.kpnlMain.Size = new System.Drawing.Size(820, 645);
            this.kpnlMain.TabIndex = 0;
            // 
            // tlpMain
            // 
            this.tlpMain.BackColor = System.Drawing.Color.Transparent;
            this.tlpMain.ColumnCount = 4;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tlpMain.Controls.Add(this.klblName, 0, 0);
            this.tlpMain.Controls.Add(this.ktxtName, 1, 0);
            this.tlpMain.Controls.Add(this.klblPrimary, 0, 1);
            this.tlpMain.Controls.Add(this.kbtnPrimary, 1, 1);
            this.tlpMain.Controls.Add(this.ktxtPrimaryHex, 2, 1);
            this.tlpMain.Controls.Add(this.kbtnPickPrimary, 3, 1);
            this.tlpMain.Controls.Add(this.klblRgb, 0, 2);
            this.tlpMain.Controls.Add(this.ktxtPrimaryRgb, 1, 2);
            this.tlpMain.Controls.Add(this.kbtnUseRgb, 2, 2);
            this.tlpMain.Controls.Add(this.kchkSecondary, 0, 3);
            this.tlpMain.Controls.Add(this.kbtnSecondary, 1, 3);
            this.tlpMain.Controls.Add(this.kbtnPickSecondary, 3, 3);
            this.tlpMain.Controls.Add(this.kchkSurface, 0, 4);
            this.tlpMain.Controls.Add(this.kbtnSurface, 1, 4);
            this.tlpMain.Controls.Add(this.kbtnPickSurface, 3, 4);
            this.tlpMain.Controls.Add(this.klblDonor, 0, 5);
            this.tlpMain.Controls.Add(this.kcmbDonor, 1, 5);
            this.tlpMain.Controls.Add(this.klblTheme, 0, 6);
            this.tlpMain.Controls.Add(this.kcmbTheme, 1, 6);
            this.tlpMain.Controls.Add(this.klblFlyout, 0, 7);
            this.tlpMain.Controls.Add(this.kcmbFlyout, 1, 7);
            this.tlpMain.Controls.Add(this.klblMagnifierSize, 0, 8);
            this.tlpMain.Controls.Add(this.knudMagnifierSize, 1, 8);
            this.tlpMain.Controls.Add(this.klblFormats, 0, 9);
            this.tlpMain.Controls.Add(this.kclbColorFormats, 1, 9);
            this.tlpMain.Controls.Add(this.flpActions, 0, 10);
            this.tlpMain.Controls.Add(this.khgPreview, 0, 11);
            this.tlpMain.Controls.Add(this.klblStatus, 0, 12);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(8, 8);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 13;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpMain.Size = new System.Drawing.Size(804, 629);
            this.tlpMain.TabIndex = 0;
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
            this.tlpMain.SetColumnSpan(this.ktxtName, 3);
            this.ktxtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtName.Location = new System.Drawing.Point(143, 3);
            this.ktxtName.Name = "ktxtName";
            this.ktxtName.Size = new System.Drawing.Size(658, 23);
            this.ktxtName.TabIndex = 0;
            // 
            // klblPrimary
            // 
            this.klblPrimary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblPrimary.Location = new System.Drawing.Point(3, 35);
            this.klblPrimary.Name = "klblPrimary";
            this.klblPrimary.Size = new System.Drawing.Size(134, 30);
            this.klblPrimary.TabIndex = 1;
            this.klblPrimary.Values.Text = "Primary (hex)";
            // 
            // kbtnPrimary
            // 
            this.kbtnPrimary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnPrimary.Location = new System.Drawing.Point(143, 35);
            this.kbtnPrimary.Name = "kbtnPrimary";
            this.kbtnPrimary.Size = new System.Drawing.Size(336, 30);
            this.kbtnPrimary.TabIndex = 1;
            this.kbtnPrimary.Values.Text = "Pick";
            // 
            // ktxtPrimaryHex
            // 
            this.ktxtPrimaryHex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtPrimaryHex.Location = new System.Drawing.Point(485, 35);
            this.ktxtPrimaryHex.Name = "ktxtPrimaryHex";
            this.ktxtPrimaryHex.Size = new System.Drawing.Size(274, 23);
            this.ktxtPrimaryHex.TabIndex = 2;
            // 
            // kbtnPickPrimary
            // 
            this.kbtnPickPrimary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnPickPrimary.Location = new System.Drawing.Point(765, 35);
            this.kbtnPickPrimary.Name = "kbtnPickPrimary";
            this.kbtnPickPrimary.Size = new System.Drawing.Size(36, 30);
            this.kbtnPickPrimary.TabIndex = 3;
            this.kbtnPickPrimary.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnPickPrimary.Values.Text = "";
            // 
            // klblRgb
            // 
            this.klblRgb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblRgb.Location = new System.Drawing.Point(3, 71);
            this.klblRgb.Name = "klblRgb";
            this.klblRgb.Size = new System.Drawing.Size(134, 30);
            this.klblRgb.TabIndex = 4;
            this.klblRgb.Values.Text = "Primary (RGB)";
            // 
            // ktxtPrimaryRgb
            // 
            this.ktxtPrimaryRgb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtPrimaryRgb.Location = new System.Drawing.Point(143, 71);
            this.ktxtPrimaryRgb.Name = "ktxtPrimaryRgb";
            this.ktxtPrimaryRgb.Size = new System.Drawing.Size(336, 23);
            this.ktxtPrimaryRgb.TabIndex = 3;
            // 
            // kbtnUseRgb
            // 
            this.tlpMain.SetColumnSpan(this.kbtnUseRgb, 2);
            this.kbtnUseRgb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnUseRgb.Location = new System.Drawing.Point(485, 71);
            this.kbtnUseRgb.Name = "kbtnUseRgb";
            this.kbtnUseRgb.Size = new System.Drawing.Size(316, 30);
            this.kbtnUseRgb.TabIndex = 4;
            this.kbtnUseRgb.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnUseRgb.Values.Text = "Use RGB";
            // 
            // kchkSecondary
            // 
            this.kchkSecondary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kchkSecondary.Location = new System.Drawing.Point(3, 107);
            this.kchkSecondary.Name = "kchkSecondary";
            this.kchkSecondary.Size = new System.Drawing.Size(134, 30);
            this.kchkSecondary.TabIndex = 5;
            this.kchkSecondary.Values.Text = "Secondary";
            // 
            // kbtnSecondary
            // 
            this.kbtnSecondary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnSecondary.Enabled = false;
            this.kbtnSecondary.Location = new System.Drawing.Point(143, 107);
            this.kbtnSecondary.Name = "kbtnSecondary";
            this.kbtnSecondary.Size = new System.Drawing.Size(336, 30);
            this.kbtnSecondary.TabIndex = 6;
            this.kbtnSecondary.Values.Text = "Pick";
            // 
            // kbtnPickSecondary
            // 
            this.kbtnPickSecondary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnPickSecondary.Location = new System.Drawing.Point(765, 107);
            this.kbtnPickSecondary.Name = "kbtnPickSecondary";
            this.kbtnPickSecondary.Size = new System.Drawing.Size(36, 30);
            this.kbtnPickSecondary.TabIndex = 7;
            this.kbtnPickSecondary.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnPickSecondary.Values.Text = "";
            // 
            // kchkSurface
            // 
            this.kchkSurface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kchkSurface.Location = new System.Drawing.Point(3, 143);
            this.kchkSurface.Name = "kchkSurface";
            this.kchkSurface.Size = new System.Drawing.Size(134, 30);
            this.kchkSurface.TabIndex = 7;
            this.kchkSurface.Values.Text = "Surface";
            // 
            // kbtnSurface
            // 
            this.kbtnSurface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnSurface.Enabled = false;
            this.kbtnSurface.Location = new System.Drawing.Point(143, 143);
            this.kbtnSurface.Name = "kbtnSurface";
            this.kbtnSurface.Size = new System.Drawing.Size(336, 30);
            this.kbtnSurface.TabIndex = 8;
            this.kbtnSurface.Values.Text = "Pick";
            // 
            // kbtnPickSurface
            // 
            this.kbtnPickSurface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnPickSurface.Location = new System.Drawing.Point(765, 143);
            this.kbtnPickSurface.Name = "kbtnPickSurface";
            this.kbtnPickSurface.Size = new System.Drawing.Size(36, 30);
            this.kbtnPickSurface.TabIndex = 9;
            this.kbtnPickSurface.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnPickSurface.Values.Text = "";
            // 
            // klblDonor
            // 
            this.klblDonor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblDonor.Location = new System.Drawing.Point(3, 179);
            this.klblDonor.Name = "klblDonor";
            this.klblDonor.Size = new System.Drawing.Size(134, 30);
            this.klblDonor.TabIndex = 10;
            this.klblDonor.Values.Text = "Donor family";
            // 
            // kcmbDonor
            // 
            this.tlpMain.SetColumnSpan(this.kcmbDonor, 3);
            this.kcmbDonor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kcmbDonor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbDonor.Location = new System.Drawing.Point(143, 179);
            this.kcmbDonor.Name = "kcmbDonor";
            this.kcmbDonor.Size = new System.Drawing.Size(658, 30);
            this.kcmbDonor.TabIndex = 9;
            // 
            // klblTheme
            // 
            this.klblTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblTheme.Location = new System.Drawing.Point(3, 215);
            this.klblTheme.Name = "klblTheme";
            this.klblTheme.Size = new System.Drawing.Size(134, 30);
            this.klblTheme.TabIndex = 11;
            this.klblTheme.Values.Text = "Theme selector";
            // 
            // kcmbTheme
            // 
            this.tlpMain.SetColumnSpan(this.kcmbTheme, 3);
            this.kcmbTheme.DefaultPalette = Krypton.Toolkit.PaletteMode.Microsoft365Blue;
            this.kcmbTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kcmbTheme.Location = new System.Drawing.Point(143, 215);
            this.kcmbTheme.Name = "kcmbTheme";
            this.kcmbTheme.Size = new System.Drawing.Size(658, 30);
            this.kcmbTheme.TabIndex = 10;
            // 
            // klblFlyout
            // 
            this.klblFlyout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblFlyout.Location = new System.Drawing.Point(3, 251);
            this.klblFlyout.Name = "klblFlyout";
            this.klblFlyout.Size = new System.Drawing.Size(134, 30);
            this.klblFlyout.TabIndex = 12;
            this.klblFlyout.Values.Text = "Picker flyout";
            // 
            // kcmbFlyout
            // 
            this.tlpMain.SetColumnSpan(this.kcmbFlyout, 3);
            this.kcmbFlyout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kcmbFlyout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbFlyout.Location = new System.Drawing.Point(143, 251);
            this.kcmbFlyout.Name = "kcmbFlyout";
            this.kcmbFlyout.Size = new System.Drawing.Size(658, 30);
            this.kcmbFlyout.TabIndex = 11;
            // 
            // klblMagnifierSize
            // 
            this.klblMagnifierSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblMagnifierSize.Location = new System.Drawing.Point(3, 287);
            this.klblMagnifierSize.Name = "klblMagnifierSize";
            this.klblMagnifierSize.Size = new System.Drawing.Size(134, 30);
            this.klblMagnifierSize.TabIndex = 13;
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
            this.knudMagnifierSize.Location = new System.Drawing.Point(143, 287);
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
            this.knudMagnifierSize.Size = new System.Drawing.Size(336, 30);
            this.knudMagnifierSize.TabIndex = 12;
            this.knudMagnifierSize.Value = new decimal(new int[] {
            11,
            0,
            0,
            0});
            // 
            // klblFormats
            // 
            this.klblFormats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblFormats.Location = new System.Drawing.Point(3, 323);
            this.klblFormats.Name = "klblFormats";
            this.klblFormats.Size = new System.Drawing.Size(134, 114);
            this.klblFormats.TabIndex = 14;
            this.klblFormats.Values.Text = "Colour formats";
            // 
            // kclbColorFormats
            // 
            this.kclbColorFormats.CheckOnClick = true;
            this.tlpMain.SetColumnSpan(this.kclbColorFormats, 3);
            this.kclbColorFormats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kclbColorFormats.Location = new System.Drawing.Point(143, 323);
            this.kclbColorFormats.Name = "kclbColorFormats";
            this.kclbColorFormats.Size = new System.Drawing.Size(658, 114);
            this.kclbColorFormats.TabIndex = 13;
            // 
            // flpActions
            // 
            this.tlpMain.SetColumnSpan(this.flpActions, 4);
            this.flpActions.Controls.Add(this.kbtnApply);
            this.flpActions.Controls.Add(this.kbtnRegister);
            this.flpActions.Controls.Add(this.kbtnExport);
            this.flpActions.Controls.Add(this.kbtnBuilder);
            this.flpActions.Controls.Add(this.kbtnReset);
            this.flpActions.Controls.Add(this.kbtnRandom);
            this.flpActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpActions.Location = new System.Drawing.Point(3, 443);
            this.flpActions.Name = "flpActions";
            this.flpActions.Size = new System.Drawing.Size(798, 34);
            this.flpActions.TabIndex = 11;
            // 
            // kbtnApply
            // 
            this.kbtnApply.Location = new System.Drawing.Point(3, 3);
            this.kbtnApply.Name = "kbtnApply";
            this.kbtnApply.Size = new System.Drawing.Size(100, 28);
            this.kbtnApply.TabIndex = 0;
            this.kbtnApply.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnApply.Values.Text = "Apply";
            // 
            // kbtnRegister
            // 
            this.kbtnRegister.Location = new System.Drawing.Point(109, 3);
            this.kbtnRegister.Name = "kbtnRegister";
            this.kbtnRegister.Size = new System.Drawing.Size(110, 28);
            this.kbtnRegister.TabIndex = 1;
            this.kbtnRegister.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnRegister.Values.Text = "Register";
            // 
            // kbtnExport
            // 
            this.kbtnExport.Location = new System.Drawing.Point(225, 3);
            this.kbtnExport.Name = "kbtnExport";
            this.kbtnExport.Size = new System.Drawing.Size(110, 28);
            this.kbtnExport.TabIndex = 2;
            this.kbtnExport.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnExport.Values.Text = "Export XML";
            // 
            // kbtnBuilder
            // 
            this.kbtnBuilder.Location = new System.Drawing.Point(341, 3);
            this.kbtnBuilder.Name = "kbtnBuilder";
            this.kbtnBuilder.Size = new System.Drawing.Size(120, 28);
            this.kbtnBuilder.TabIndex = 3;
            this.kbtnBuilder.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnBuilder.Values.Text = "Open Builder";
            // 
            // kbtnReset
            // 
            this.kbtnReset.Location = new System.Drawing.Point(467, 3);
            this.kbtnReset.Name = "kbtnReset";
            this.kbtnReset.Size = new System.Drawing.Size(100, 28);
            this.kbtnReset.TabIndex = 4;
            this.kbtnReset.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnReset.Values.Text = "Reset";
            // 
            // kbtnRandom
            // 
            this.kbtnRandom.Location = new System.Drawing.Point(573, 3);
            this.kbtnRandom.Name = "kbtnRandom";
            this.kbtnRandom.Size = new System.Drawing.Size(100, 28);
            this.kbtnRandom.TabIndex = 5;
            this.kbtnRandom.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnRandom.Values.Text = "Randomize";
            this.kbtnRandom.Click += new System.EventHandler(this.kbtnRandom_Click);
            // 
            // khgPreview
            // 
            this.tlpMain.SetColumnSpan(this.khgPreview, 4);
            this.khgPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.khgPreview.Location = new System.Drawing.Point(3, 483);
            // 
            // khgPreview.Panel
            // 
            this.khgPreview.Panel.Controls.Add(this.tlpPreview);
            this.khgPreview.Size = new System.Drawing.Size(798, 115);
            this.khgPreview.TabIndex = 12;
            this.khgPreview.ValuesPrimary.Description = "Buttons and input follow the generated theme";
            this.khgPreview.ValuesPrimary.Heading = "Preview";
            // 
            // tlpPreview
            // 
            this.tlpPreview.ColumnCount = 3;
            this.tlpPreview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tlpPreview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tlpPreview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tlpPreview.Controls.Add(this.kbtnPreview, 0, 0);
            this.tlpPreview.Controls.Add(this.kchkPreview, 1, 0);
            this.tlpPreview.Controls.Add(this.ktxtPreview, 2, 0);
            this.tlpPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPreview.Location = new System.Drawing.Point(0, 0);
            this.tlpPreview.Name = "tlpPreview";
            this.tlpPreview.Padding = new System.Windows.Forms.Padding(8);
            this.tlpPreview.RowCount = 1;
            this.tlpPreview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPreview.Size = new System.Drawing.Size(796, 57);
            this.tlpPreview.TabIndex = 0;
            // 
            // kbtnPreview
            // 
            this.kbtnPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnPreview.Location = new System.Drawing.Point(11, 11);
            this.kbtnPreview.Name = "kbtnPreview";
            this.kbtnPreview.Size = new System.Drawing.Size(251, 35);
            this.kbtnPreview.TabIndex = 0;
            this.kbtnPreview.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnPreview.Values.Text = "Sample button";
            // 
            // kchkPreview
            // 
            this.kchkPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kchkPreview.Location = new System.Drawing.Point(268, 11);
            this.kchkPreview.Name = "kchkPreview";
            this.kchkPreview.Size = new System.Drawing.Size(251, 35);
            this.kchkPreview.TabIndex = 1;
            this.kchkPreview.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kchkPreview.Values.Text = "Check button";
            // 
            // ktxtPreview
            // 
            this.ktxtPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtPreview.Location = new System.Drawing.Point(525, 11);
            this.ktxtPreview.Name = "ktxtPreview";
            this.ktxtPreview.Size = new System.Drawing.Size(260, 23);
            this.ktxtPreview.TabIndex = 2;
            this.ktxtPreview.Text = "Sample input";
            // 
            // klblStatus
            // 
            this.tlpMain.SetColumnSpan(this.klblStatus, 4);
            this.klblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblStatus.Location = new System.Drawing.Point(3, 604);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(798, 22);
            this.klblStatus.TabIndex = 15;
            this.klblStatus.Values.Text = "Ready";
            // 
            // CustomThemeGeneratorDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 680);
            this.Controls.Add(this.kpnlMain);
            this.Controls.Add(this.kwlblInfo);
            this.MinimumSize = new System.Drawing.Size(760, 620);
            this.Name = "CustomThemeGeneratorDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Custom Theme Generator (#4234)";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
            this.kpnlMain.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbDonor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbFlyout)).EndInit();
            this.flpActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.khgPreview.Panel)).EndInit();
            this.khgPreview.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.khgPreview)).EndInit();
            this.khgPreview.ResumeLayout(false);
            this.tlpPreview.ResumeLayout(false);
            this.tlpPreview.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private KryptonLabel klblFlyout;
        private KryptonComboBox kcmbFlyout;
        private KryptonLabel klblMagnifierSize;
        private KryptonNumericUpDown knudMagnifierSize;
        private KryptonLabel klblFormats;
        private KryptonCheckedListBox kclbColorFormats;
        private FlowLayoutPanel flpActions;
        private KryptonButton kbtnApply;
        private KryptonButton kbtnRegister;
        private KryptonButton kbtnExport;
        private KryptonButton kbtnBuilder;
        private KryptonButton kbtnReset;
        private KryptonButton kbtnRandom;
        private KryptonHeaderGroup khgPreview;
        private TableLayoutPanel tlpPreview;
        private KryptonButton kbtnPreview;
        private KryptonCheckButton kchkPreview;
        private KryptonTextBox ktxtPreview;
        private KryptonLabel klblStatus;
    }
}

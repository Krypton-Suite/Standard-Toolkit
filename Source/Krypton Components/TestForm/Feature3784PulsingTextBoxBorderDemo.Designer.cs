#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class Feature3784PulsingTextBoxBorderDemo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Feature3784PulsingTextBoxBorderDemo));
            this.kwlblInfo = new Krypton.Toolkit.KryptonWrapLabel();
            this.kryptonPanelMain = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanelRoot = new System.Windows.Forms.TableLayoutPanel();
            this.panelSamplesScroll = new System.Windows.Forms.Panel();
            this.tableLayoutPanelSamples = new System.Windows.Forms.TableLayoutPanel();
            this.klblHeroHeader = new Krypton.Toolkit.KryptonLabel();
            this.ktxtAnimatedGlow = new Krypton.Toolkit.KryptonTextBox();
            this.klblStaticHeader = new Krypton.Toolkit.KryptonLabel();
            this.ktxtStaticGlow = new Krypton.Toolkit.KryptonTextBox();
            this.klblMaskedHeader = new Krypton.Toolkit.KryptonLabel();
            this.kmtxtPhone = new Krypton.Toolkit.KryptonMaskedTextBox();
            this.klblComboHeader = new Krypton.Toolkit.KryptonLabel();
            this.kcmbGlow = new Krypton.Toolkit.KryptonComboBox();
            this.klblRichTextHeader = new Krypton.Toolkit.KryptonLabel();
            this.krtbGlow = new Krypton.Toolkit.KryptonRichTextBox();
            this.klblNumericHeader = new Krypton.Toolkit.KryptonLabel();
            this.knudQuantity = new Krypton.Toolkit.KryptonNumericUpDown();
            this.klblDomainHeader = new Krypton.Toolkit.KryptonLabel();
            this.kdudPriority = new Krypton.Toolkit.KryptonDomainUpDown();
            this.klblDateTimeHeader = new Krypton.Toolkit.KryptonLabel();
            this.kdtpDue = new Krypton.Toolkit.KryptonDateTimePicker();
            this.klblCalcHeader = new Krypton.Toolkit.KryptonLabel();
            this.kcalcBudget = new Krypton.Toolkit.KryptonCalcInput();
            this.klblButtonHeader = new Krypton.Toolkit.KryptonLabel();
            this.kbtnGlow = new Krypton.Toolkit.KryptonButton();
            this.kryptonHeaderGroupSettings = new Krypton.Toolkit.KryptonHeaderGroup();
            this.tableLayoutPanelSettings = new System.Windows.Forms.TableLayoutPanel();
            this.klblTarget = new Krypton.Toolkit.KryptonLabel();
            this.kcmbTarget = new Krypton.Toolkit.KryptonComboBox();
            this.kchkEnable = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkAnimate = new Krypton.Toolkit.KryptonCheckBox();
            this.klblShowWhen = new Krypton.Toolkit.KryptonLabel();
            this.kcmbShowWhen = new Krypton.Toolkit.KryptonComboBox();
            this.klblStyle = new Krypton.Toolkit.KryptonLabel();
            this.kcmbStyle = new Krypton.Toolkit.KryptonComboBox();
            this.klblAnimSpeed = new Krypton.Toolkit.KryptonLabel();
            this.knudAnimationSpeed = new Krypton.Toolkit.KryptonNumericUpDown();
            this.kchkCueAnimate = new Krypton.Toolkit.KryptonCheckBox();
            this.klblCueSpeed = new Krypton.Toolkit.KryptonLabel();
            this.knudCueSpeed = new Krypton.Toolkit.KryptonNumericUpDown();
            this.flowLayoutPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.kbtnApply = new Krypton.Toolkit.KryptonButton();
            this.kbtnReset = new Krypton.Toolkit.KryptonButton();
            this.kbtnOpenFormGlow = new Krypton.Toolkit.KryptonButton();
            this.kwlblStatus = new Krypton.Toolkit.KryptonWrapLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).BeginInit();
            this.kryptonPanelMain.SuspendLayout();
            this.tableLayoutPanelRoot.SuspendLayout();
            this.panelSamplesScroll.SuspendLayout();
            this.tableLayoutPanelSamples.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbGlow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroupSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroupSettings.Panel)).BeginInit();
            this.kryptonHeaderGroupSettings.Panel.SuspendLayout();
            this.kryptonHeaderGroupSettings.SuspendLayout();
            this.tableLayoutPanelSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTarget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbShowWhen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbStyle)).BeginInit();
            this.flowLayoutPanelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // kwlblInfo
            // 
            this.kwlblInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.kwlblInfo.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kwlblInfo.Location = new System.Drawing.Point(0, 0);
            this.kwlblInfo.Name = "kwlblInfo";
            this.kwlblInfo.Padding = new System.Windows.Forms.Padding(12, 12, 12, 8);
            this.kwlblInfo.Size = new System.Drawing.Size(1424, 35);
            this.kwlblInfo.Text = resources.GetString("kwlblInfo.Text");
            // 
            // kryptonPanelMain
            // 
            this.kryptonPanelMain.Controls.Add(this.tableLayoutPanelRoot);
            this.kryptonPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanelMain.Location = new System.Drawing.Point(0, 35);
            this.kryptonPanelMain.Name = "kryptonPanelMain";
            this.kryptonPanelMain.Padding = new System.Windows.Forms.Padding(12, 0, 12, 8);
            this.kryptonPanelMain.Size = new System.Drawing.Size(980, 656);
            this.kryptonPanelMain.TabIndex = 0;
            // 
            // tableLayoutPanelRoot
            // 
            this.tableLayoutPanelRoot.ColumnCount = 2;
            this.tableLayoutPanelRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62F));
            this.tableLayoutPanelRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38F));
            this.tableLayoutPanelRoot.Controls.Add(this.panelSamplesScroll, 0, 0);
            this.tableLayoutPanelRoot.Controls.Add(this.kryptonHeaderGroupSettings, 1, 0);
            this.tableLayoutPanelRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelRoot.Location = new System.Drawing.Point(12, 0);
            this.tableLayoutPanelRoot.Name = "tableLayoutPanelRoot";
            this.tableLayoutPanelRoot.RowCount = 1;
            this.tableLayoutPanelRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelRoot.Size = new System.Drawing.Size(956, 648);
            this.tableLayoutPanelRoot.TabIndex = 0;
            // 
            // panelSamplesScroll
            // 
            this.panelSamplesScroll.AutoScroll = true;
            this.panelSamplesScroll.Controls.Add(this.tableLayoutPanelSamples);
            this.panelSamplesScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSamplesScroll.Location = new System.Drawing.Point(3, 3);
            this.panelSamplesScroll.Name = "panelSamplesScroll";
            this.panelSamplesScroll.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.panelSamplesScroll.Size = new System.Drawing.Size(586, 642);
            this.panelSamplesScroll.TabIndex = 0;
            // 
            // tableLayoutPanelSamples
            // 
            this.tableLayoutPanelSamples.AutoSize = true;
            this.tableLayoutPanelSamples.ColumnCount = 1;
            this.tableLayoutPanelSamples.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSamples.Controls.Add(this.klblHeroHeader, 0, 0);
            this.tableLayoutPanelSamples.Controls.Add(this.ktxtAnimatedGlow, 0, 1);
            this.tableLayoutPanelSamples.Controls.Add(this.klblStaticHeader, 0, 2);
            this.tableLayoutPanelSamples.Controls.Add(this.ktxtStaticGlow, 0, 3);
            this.tableLayoutPanelSamples.Controls.Add(this.klblMaskedHeader, 0, 4);
            this.tableLayoutPanelSamples.Controls.Add(this.kmtxtPhone, 0, 5);
            this.tableLayoutPanelSamples.Controls.Add(this.klblComboHeader, 0, 6);
            this.tableLayoutPanelSamples.Controls.Add(this.kcmbGlow, 0, 7);
            this.tableLayoutPanelSamples.Controls.Add(this.klblRichTextHeader, 0, 8);
            this.tableLayoutPanelSamples.Controls.Add(this.krtbGlow, 0, 9);
            this.tableLayoutPanelSamples.Controls.Add(this.klblNumericHeader, 0, 10);
            this.tableLayoutPanelSamples.Controls.Add(this.knudQuantity, 0, 11);
            this.tableLayoutPanelSamples.Controls.Add(this.klblDomainHeader, 0, 12);
            this.tableLayoutPanelSamples.Controls.Add(this.kdudPriority, 0, 13);
            this.tableLayoutPanelSamples.Controls.Add(this.klblDateTimeHeader, 0, 14);
            this.tableLayoutPanelSamples.Controls.Add(this.kdtpDue, 0, 15);
            this.tableLayoutPanelSamples.Controls.Add(this.klblCalcHeader, 0, 16);
            this.tableLayoutPanelSamples.Controls.Add(this.kcalcBudget, 0, 17);
            this.tableLayoutPanelSamples.Controls.Add(this.klblButtonHeader, 0, 18);
            this.tableLayoutPanelSamples.Controls.Add(this.kbtnGlow, 0, 19);
            this.tableLayoutPanelSamples.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelSamples.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelSamples.Name = "tableLayoutPanelSamples";
            this.tableLayoutPanelSamples.RowCount = 20;
            this.tableLayoutPanelSamples.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelSamples.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanelSamples.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelSamples.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanelSamples.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelSamples.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanelSamples.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelSamples.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanelSamples.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelSamples.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanelSamples.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelSamples.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanelSamples.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelSamples.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanelSamples.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelSamples.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanelSamples.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelSamples.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanelSamples.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelSamples.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanelSamples.Size = new System.Drawing.Size(565, 688);
            this.tableLayoutPanelSamples.TabIndex = 0;
            // 
            // klblHeroHeader
            // 
            this.klblHeroHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblHeroHeader.Location = new System.Drawing.Point(3, 0);
            this.klblHeroHeader.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.klblHeroHeader.Name = "klblHeroHeader";
            this.klblHeroHeader.Size = new System.Drawing.Size(559, 20);
            this.klblHeroHeader.TabIndex = 0;
            this.klblHeroHeader.Values.Text = "KryptonTextBox — animated full border + cue shimmer";
            // 
            // ktxtAnimatedGlow
            // 
            this.ktxtAnimatedGlow.AlwaysActive = false;
            this.ktxtAnimatedGlow.CueHint.Animate = true;
            this.ktxtAnimatedGlow.CueHint.AnimationSpeed = 0.75F;
            this.ktxtAnimatedGlow.CueHint.CueHintText = "Describe the app or website or idea that you want to build";
            this.ktxtAnimatedGlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtAnimatedGlow.Location = new System.Drawing.Point(3, 25);
            this.ktxtAnimatedGlow.Name = "ktxtAnimatedGlow";
            this.ktxtAnimatedGlow.PulsingBorderValues.AnimationSpeed = 1.5F;
            this.ktxtAnimatedGlow.PulsingBorderValues.Enable = true;
            this.ktxtAnimatedGlow.PulsingBorderValues.Style = Krypton.Toolkit.InputPulsingBorderStyle.All;
            this.ktxtAnimatedGlow.Size = new System.Drawing.Size(559, 23);
            this.ktxtAnimatedGlow.TabIndex = 1;
            // 
            // klblStaticHeader
            // 
            this.klblStaticHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblStaticHeader.Location = new System.Drawing.Point(3, 66);
            this.klblStaticHeader.Margin = new System.Windows.Forms.Padding(3, 8, 3, 2);
            this.klblStaticHeader.Name = "klblStaticHeader";
            this.klblStaticHeader.Size = new System.Drawing.Size(559, 20);
            this.klblStaticHeader.TabIndex = 2;
            this.klblStaticHeader.Values.Text = "KryptonTextBox — static bottom-edge glow";
            // 
            // ktxtStaticGlow
            // 
            this.ktxtStaticGlow.AlwaysActive = false;
            this.ktxtStaticGlow.CueHint.CueHintText = "Static bottom glow while focused";
            this.ktxtStaticGlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtStaticGlow.Location = new System.Drawing.Point(3, 91);
            this.ktxtStaticGlow.Name = "ktxtStaticGlow";
            this.ktxtStaticGlow.PulsingBorderValues.Animate = false;
            this.ktxtStaticGlow.PulsingBorderValues.Enable = true;
            this.ktxtStaticGlow.Size = new System.Drawing.Size(559, 23);
            this.ktxtStaticGlow.TabIndex = 3;
            // 
            // klblMaskedHeader
            // 
            this.klblMaskedHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblMaskedHeader.Location = new System.Drawing.Point(3, 132);
            this.klblMaskedHeader.Margin = new System.Windows.Forms.Padding(3, 8, 3, 2);
            this.klblMaskedHeader.Name = "klblMaskedHeader";
            this.klblMaskedHeader.Size = new System.Drawing.Size(559, 20);
            this.klblMaskedHeader.TabIndex = 4;
            this.klblMaskedHeader.Values.Text = "KryptonMaskedTextBox — animated glow";
            // 
            // kmtxtPhone
            // 
            this.kmtxtPhone.AlwaysActive = false;
            this.kmtxtPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kmtxtPhone.Location = new System.Drawing.Point(3, 157);
            this.kmtxtPhone.Mask = "(000) 000-0000";
            this.kmtxtPhone.Name = "kmtxtPhone";
            this.kmtxtPhone.PulsingBorderValues.AnimationSpeed = 1.2F;
            this.kmtxtPhone.PulsingBorderValues.Enable = true;
            this.kmtxtPhone.Size = new System.Drawing.Size(559, 23);
            this.kmtxtPhone.TabIndex = 5;
            this.kmtxtPhone.Text = "(   )    -";
            // 
            // klblComboHeader
            // 
            this.klblComboHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblComboHeader.Location = new System.Drawing.Point(3, 198);
            this.klblComboHeader.Margin = new System.Windows.Forms.Padding(3, 8, 3, 2);
            this.klblComboHeader.Name = "klblComboHeader";
            this.klblComboHeader.Size = new System.Drawing.Size(559, 20);
            this.klblComboHeader.TabIndex = 6;
            this.klblComboHeader.Values.Text = "KryptonComboBox — animated glow + cue hint";
            // 
            // kcmbGlow
            // 
            this.kcmbGlow.AlwaysActive = false;
            this.kcmbGlow.CueHint.CueHintText = "Choose an option…";
            this.kcmbGlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kcmbGlow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbGlow.Items.AddRange(new object[] {
            "Option Alpha",
            "Option Beta",
            "Option Gamma"});
            this.kcmbGlow.Location = new System.Drawing.Point(3, 223);
            this.kcmbGlow.Name = "kcmbGlow";
            this.kcmbGlow.PulsingBorderValues.AnimationSpeed = 0.5F;
            this.kcmbGlow.PulsingBorderValues.Enable = true;
            this.kcmbGlow.Size = new System.Drawing.Size(559, 30);
            this.kcmbGlow.TabIndex = 7;
            // 
            // klblRichTextHeader
            // 
            this.klblRichTextHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblRichTextHeader.Location = new System.Drawing.Point(3, 264);
            this.klblRichTextHeader.Margin = new System.Windows.Forms.Padding(3, 8, 3, 2);
            this.klblRichTextHeader.Name = "klblRichTextHeader";
            this.klblRichTextHeader.Size = new System.Drawing.Size(559, 20);
            this.klblRichTextHeader.TabIndex = 8;
            this.klblRichTextHeader.Values.Text = "KryptonRichTextBox — animated glow";
            // 
            // krtbGlow
            // 
            this.krtbGlow.AlwaysActive = false;
            this.krtbGlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.krtbGlow.Location = new System.Drawing.Point(3, 289);
            this.krtbGlow.Name = "krtbGlow";
            this.krtbGlow.PulsingBorderValues.Enable = true;
            this.krtbGlow.Size = new System.Drawing.Size(559, 66);
            this.krtbGlow.TabIndex = 9;
            this.krtbGlow.Text = "Rich text with glowing border";
            // 
            // klblNumericHeader
            // 
            this.klblNumericHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblNumericHeader.Location = new System.Drawing.Point(3, 366);
            this.klblNumericHeader.Margin = new System.Windows.Forms.Padding(3, 8, 3, 2);
            this.klblNumericHeader.Name = "klblNumericHeader";
            this.klblNumericHeader.Size = new System.Drawing.Size(559, 20);
            this.klblNumericHeader.TabIndex = 10;
            this.klblNumericHeader.Values.Text = "KryptonNumericUpDown — animated glow";
            // 
            // knudQuantity
            // 
            this.knudQuantity.AlwaysActive = false;
            this.knudQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.knudQuantity.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.knudQuantity.Location = new System.Drawing.Point(3, 391);
            this.knudQuantity.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.knudQuantity.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.knudQuantity.Name = "knudQuantity";
            this.knudQuantity.PulsingBorderValues.Enable = true;
            this.knudQuantity.Size = new System.Drawing.Size(559, 30);
            this.knudQuantity.TabIndex = 11;
            this.knudQuantity.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // klblDomainHeader
            // 
            this.klblDomainHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblDomainHeader.Location = new System.Drawing.Point(3, 432);
            this.klblDomainHeader.Margin = new System.Windows.Forms.Padding(3, 8, 3, 2);
            this.klblDomainHeader.Name = "klblDomainHeader";
            this.klblDomainHeader.Size = new System.Drawing.Size(559, 20);
            this.klblDomainHeader.TabIndex = 12;
            this.klblDomainHeader.Values.Text = "KryptonDomainUpDown — animated glow";
            // 
            // kdudPriority
            // 
            this.kdudPriority.AlwaysActive = false;
            this.kdudPriority.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kdudPriority.Items.Add("Low");
            this.kdudPriority.Items.Add("Normal");
            this.kdudPriority.Items.Add("High");
            this.kdudPriority.Location = new System.Drawing.Point(3, 457);
            this.kdudPriority.Name = "kdudPriority";
            this.kdudPriority.PulsingBorderValues.Enable = true;
            this.kdudPriority.PulsingBorderValues.Style = Krypton.Toolkit.InputPulsingBorderStyle.All;
            this.kdudPriority.Size = new System.Drawing.Size(559, 30);
            this.kdudPriority.TabIndex = 13;
            // 
            // klblDateTimeHeader
            // 
            this.klblDateTimeHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblDateTimeHeader.Location = new System.Drawing.Point(3, 498);
            this.klblDateTimeHeader.Margin = new System.Windows.Forms.Padding(3, 8, 3, 2);
            this.klblDateTimeHeader.Name = "klblDateTimeHeader";
            this.klblDateTimeHeader.Size = new System.Drawing.Size(559, 20);
            this.klblDateTimeHeader.TabIndex = 14;
            this.klblDateTimeHeader.Values.Text = "KryptonDateTimePicker — animated glow";
            // 
            // kdtpDue
            // 
            this.kdtpDue.AlwaysActive = false;
            this.kdtpDue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kdtpDue.Location = new System.Drawing.Point(3, 523);
            this.kdtpDue.Name = "kdtpDue";
            this.kdtpDue.PulsingBorderValues.AnimationSpeed = 0.8F;
            this.kdtpDue.PulsingBorderValues.Enable = true;
            this.kdtpDue.Size = new System.Drawing.Size(559, 30);
            this.kdtpDue.TabIndex = 15;
            // 
            // klblCalcHeader
            // 
            this.klblCalcHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblCalcHeader.Location = new System.Drawing.Point(3, 564);
            this.klblCalcHeader.Margin = new System.Windows.Forms.Padding(3, 8, 3, 2);
            this.klblCalcHeader.Name = "klblCalcHeader";
            this.klblCalcHeader.Size = new System.Drawing.Size(559, 20);
            this.klblCalcHeader.TabIndex = 16;
            this.klblCalcHeader.Values.Text = "KryptonCalcInput — animated glow";
            // 
            // kcalcBudget
            // 
            this.kcalcBudget.AlwaysActive = false;
            this.kcalcBudget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kcalcBudget.Location = new System.Drawing.Point(3, 589);
            this.kcalcBudget.Name = "kcalcBudget";
            this.kcalcBudget.PulsingBorderValues.Enable = true;
            this.kcalcBudget.Size = new System.Drawing.Size(559, 30);
            this.kcalcBudget.TabIndex = 17;
            this.kcalcBudget.Value = new decimal(new int[] {
            1251,
            0,
            0,
            0});
            // 
            // klblButtonHeader
            // 
            this.klblButtonHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblButtonHeader.Location = new System.Drawing.Point(3, 630);
            this.klblButtonHeader.Margin = new System.Windows.Forms.Padding(3, 8, 3, 2);
            this.klblButtonHeader.Name = "klblButtonHeader";
            this.klblButtonHeader.Size = new System.Drawing.Size(559, 20);
            this.klblButtonHeader.TabIndex = 18;
            this.klblButtonHeader.Values.Text = "KryptonButton — animated glow on hover/focus";
            // 
            // kbtnGlow
            // 
            this.kbtnGlow.Dock = System.Windows.Forms.DockStyle.Left;
            this.kbtnGlow.Location = new System.Drawing.Point(3, 655);
            this.kbtnGlow.Name = "kbtnGlow";
            this.kbtnGlow.PulsingBorderValues.Enable = true;
            this.kbtnGlow.PulsingBorderValues.ShowWhen = Krypton.Toolkit.InputPulsingBorderShowWhen.Active;
            this.kbtnGlow.PulsingBorderValues.Style = Krypton.Toolkit.InputPulsingBorderStyle.All;
            this.kbtnGlow.Size = new System.Drawing.Size(180, 30);
            this.kbtnGlow.TabIndex = 19;
            this.kbtnGlow.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnGlow.Values.Text = "Generate";
            // 
            // kryptonHeaderGroupSettings
            // 
            this.kryptonHeaderGroupSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonHeaderGroupSettings.HeaderStylePrimary = Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonHeaderGroupSettings.Location = new System.Drawing.Point(595, 3);
            // 
            // kryptonHeaderGroupSettings.Panel
            // 
            this.kryptonHeaderGroupSettings.Panel.Controls.Add(this.tableLayoutPanelSettings);
            this.kryptonHeaderGroupSettings.Size = new System.Drawing.Size(358, 642);
            this.kryptonHeaderGroupSettings.TabIndex = 1;
            this.kryptonHeaderGroupSettings.ValuesPrimary.Description = "Choose a target control (or All), adjust values, then Apply.";
            this.kryptonHeaderGroupSettings.ValuesPrimary.Heading = "Glow settings";
            // 
            // tableLayoutPanelSettings
            // 
            this.tableLayoutPanelSettings.ColumnCount = 2;
            this.tableLayoutPanelSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanelSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSettings.Controls.Add(this.klblTarget, 0, 0);
            this.tableLayoutPanelSettings.Controls.Add(this.kcmbTarget, 1, 0);
            this.tableLayoutPanelSettings.Controls.Add(this.kchkEnable, 1, 1);
            this.tableLayoutPanelSettings.Controls.Add(this.kchkAnimate, 1, 2);
            this.tableLayoutPanelSettings.Controls.Add(this.klblShowWhen, 0, 3);
            this.tableLayoutPanelSettings.Controls.Add(this.kcmbShowWhen, 1, 3);
            this.tableLayoutPanelSettings.Controls.Add(this.klblStyle, 0, 4);
            this.tableLayoutPanelSettings.Controls.Add(this.kcmbStyle, 1, 4);
            this.tableLayoutPanelSettings.Controls.Add(this.klblAnimSpeed, 0, 5);
            this.tableLayoutPanelSettings.Controls.Add(this.knudAnimationSpeed, 1, 5);
            this.tableLayoutPanelSettings.Controls.Add(this.kchkCueAnimate, 1, 6);
            this.tableLayoutPanelSettings.Controls.Add(this.klblCueSpeed, 0, 7);
            this.tableLayoutPanelSettings.Controls.Add(this.knudCueSpeed, 1, 7);
            this.tableLayoutPanelSettings.Controls.Add(this.flowLayoutPanelButtons, 1, 8);
            this.tableLayoutPanelSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelSettings.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelSettings.Name = "tableLayoutPanelSettings";
            this.tableLayoutPanelSettings.Padding = new System.Windows.Forms.Padding(8);
            this.tableLayoutPanelSettings.RowCount = 9;
            this.tableLayoutPanelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSettings.Size = new System.Drawing.Size(356, 584);
            this.tableLayoutPanelSettings.TabIndex = 0;
            // 
            // klblTarget
            // 
            this.klblTarget.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.klblTarget.Location = new System.Drawing.Point(11, 16);
            this.klblTarget.Name = "klblTarget";
            this.klblTarget.Size = new System.Drawing.Size(48, 20);
            this.klblTarget.TabIndex = 0;
            this.klblTarget.Values.Text = "Target:";
            // 
            // kcmbTarget
            // 
            this.kcmbTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kcmbTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbTarget.Items.AddRange(new object[] {
            "All controls",
            "Hero TextBox",
            "Static TextBox",
            "MaskedTextBox",
            "ComboBox",
            "RichTextBox",
            "NumericUpDown",
            "DomainUpDown",
            "DateTimePicker",
            "CalcInput",
            "Button"});
            this.kcmbTarget.Location = new System.Drawing.Point(141, 11);
            this.kcmbTarget.Name = "kcmbTarget";
            this.kcmbTarget.Size = new System.Drawing.Size(204, 30);
            this.kcmbTarget.TabIndex = 1;
            this.kcmbTarget.Text = "All controls";
            // 
            // kchkEnable
            // 
            this.kchkEnable.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kchkEnable.Checked = true;
            this.kchkEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkEnable.Location = new System.Drawing.Point(141, 48);
            this.kchkEnable.Name = "kchkEnable";
            this.kchkEnable.Size = new System.Drawing.Size(90, 20);
            this.kchkEnable.TabIndex = 2;
            this.kchkEnable.Values.Text = "Enable glow";
            // 
            // kchkAnimate
            // 
            this.kchkAnimate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kchkAnimate.Checked = true;
            this.kchkAnimate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkAnimate.Location = new System.Drawing.Point(141, 76);
            this.kchkAnimate.Name = "kchkAnimate";
            this.kchkAnimate.Size = new System.Drawing.Size(69, 20);
            this.kchkAnimate.TabIndex = 3;
            this.kchkAnimate.Values.Text = "Animate";
            // 
            // klblShowWhen
            // 
            this.klblShowWhen.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.klblShowWhen.Location = new System.Drawing.Point(11, 108);
            this.klblShowWhen.Name = "klblShowWhen";
            this.klblShowWhen.Size = new System.Drawing.Size(76, 20);
            this.klblShowWhen.TabIndex = 4;
            this.klblShowWhen.Values.Text = "Show when:";
            // 
            // kcmbShowWhen
            // 
            this.kcmbShowWhen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kcmbShowWhen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbShowWhen.Items.AddRange(new object[] {
            "Focused",
            "Active",
            "Always"});
            this.kcmbShowWhen.Location = new System.Drawing.Point(141, 103);
            this.kcmbShowWhen.Name = "kcmbShowWhen";
            this.kcmbShowWhen.Size = new System.Drawing.Size(204, 30);
            this.kcmbShowWhen.TabIndex = 5;
            this.kcmbShowWhen.Text = "Focused";
            // 
            // klblStyle
            // 
            this.klblStyle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.klblStyle.Location = new System.Drawing.Point(11, 144);
            this.klblStyle.Name = "klblStyle";
            this.klblStyle.Size = new System.Drawing.Size(39, 20);
            this.klblStyle.TabIndex = 6;
            this.klblStyle.Values.Text = "Style:";
            // 
            // kcmbStyle
            // 
            this.kcmbStyle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kcmbStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbStyle.Items.AddRange(new object[] {
            "Bottom",
            "All"});
            this.kcmbStyle.Location = new System.Drawing.Point(141, 139);
            this.kcmbStyle.Name = "kcmbStyle";
            this.kcmbStyle.Size = new System.Drawing.Size(204, 30);
            this.kcmbStyle.TabIndex = 7;
            this.kcmbStyle.Text = "Bottom";
            // 
            // klblAnimSpeed
            // 
            this.klblAnimSpeed.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.klblAnimSpeed.Location = new System.Drawing.Point(11, 180);
            this.klblAnimSpeed.Name = "klblAnimSpeed";
            this.klblAnimSpeed.Size = new System.Drawing.Size(81, 20);
            this.klblAnimSpeed.TabIndex = 8;
            this.klblAnimSpeed.Values.Text = "Anim. speed:";
            // 
            // knudAnimationSpeed
            // 
            this.knudAnimationSpeed.AllowDecimals = true;
            this.knudAnimationSpeed.DecimalPlaces = 1;
            this.knudAnimationSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.knudAnimationSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.knudAnimationSpeed.Location = new System.Drawing.Point(141, 175);
            this.knudAnimationSpeed.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.knudAnimationSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.knudAnimationSpeed.Name = "knudAnimationSpeed";
            this.knudAnimationSpeed.Size = new System.Drawing.Size(204, 30);
            this.knudAnimationSpeed.TabIndex = 9;
            this.knudAnimationSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // kchkCueAnimate
            // 
            this.kchkCueAnimate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kchkCueAnimate.Location = new System.Drawing.Point(141, 212);
            this.kchkCueAnimate.Name = "kchkCueAnimate";
            this.kchkCueAnimate.Size = new System.Drawing.Size(120, 20);
            this.kchkCueAnimate.TabIndex = 10;
            this.kchkCueAnimate.Values.Text = "Cue hint shimmer";
            // 
            // klblCueSpeed
            // 
            this.klblCueSpeed.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.klblCueSpeed.Location = new System.Drawing.Point(11, 244);
            this.klblCueSpeed.Name = "klblCueSpeed";
            this.klblCueSpeed.Size = new System.Drawing.Size(70, 20);
            this.klblCueSpeed.TabIndex = 11;
            this.klblCueSpeed.Values.Text = "Cue speed:";
            // 
            // knudCueSpeed
            // 
            this.knudCueSpeed.AllowDecimals = true;
            this.knudCueSpeed.DecimalPlaces = 1;
            this.knudCueSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.knudCueSpeed.Enabled = false;
            this.knudCueSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.knudCueSpeed.Location = new System.Drawing.Point(141, 239);
            this.knudCueSpeed.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.knudCueSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.knudCueSpeed.Name = "knudCueSpeed";
            this.knudCueSpeed.Size = new System.Drawing.Size(204, 30);
            this.knudCueSpeed.TabIndex = 12;
            this.knudCueSpeed.Value = new decimal(new int[] {
            75,
            0,
            0,
            65536});
            // 
            // flowLayoutPanelButtons
            // 
            this.flowLayoutPanelButtons.Controls.Add(this.kbtnApply);
            this.flowLayoutPanelButtons.Controls.Add(this.kbtnReset);
            this.flowLayoutPanelButtons.Controls.Add(this.kbtnOpenFormGlow);
            this.flowLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelButtons.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelButtons.Location = new System.Drawing.Point(141, 275);
            this.flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
            this.flowLayoutPanelButtons.Size = new System.Drawing.Size(204, 298);
            this.flowLayoutPanelButtons.TabIndex = 13;
            this.flowLayoutPanelButtons.WrapContents = false;
            // 
            // kbtnApply
            // 
            this.kbtnApply.Location = new System.Drawing.Point(3, 3);
            this.kbtnApply.Name = "kbtnApply";
            this.kbtnApply.Size = new System.Drawing.Size(120, 28);
            this.kbtnApply.TabIndex = 0;
            this.kbtnApply.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnApply.Values.Text = "Apply";
            // 
            // kbtnReset
            // 
            this.kbtnReset.Location = new System.Drawing.Point(3, 37);
            this.kbtnReset.Name = "kbtnReset";
            this.kbtnReset.Size = new System.Drawing.Size(120, 28);
            this.kbtnReset.TabIndex = 1;
            this.kbtnReset.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnReset.Values.Text = "Reset defaults";
            // 
            // kbtnOpenFormGlow
            // 
            this.kbtnOpenFormGlow.Location = new System.Drawing.Point(3, 71);
            this.kbtnOpenFormGlow.Name = "kbtnOpenFormGlow";
            this.kbtnOpenFormGlow.Size = new System.Drawing.Size(190, 28);
            this.kbtnOpenFormGlow.TabIndex = 2;
            this.kbtnOpenFormGlow.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnOpenFormGlow.Values.Text = "Open KryptonForm glow demo…";
            // 
            // kwlblStatus
            // 
            this.kwlblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kwlblStatus.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kwlblStatus.Location = new System.Drawing.Point(0, 691);
            this.kwlblStatus.Name = "kwlblStatus";
            this.kwlblStatus.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.kwlblStatus.Size = new System.Drawing.Size(65, 29);
            this.kwlblStatus.Text = "Ready.";
            // 
            // Feature3784PulsingTextBoxBorderDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 720);
            this.Controls.Add(this.kryptonPanelMain);
            this.Controls.Add(this.kwlblStatus);
            this.Controls.Add(this.kwlblInfo);
            this.MinimumSize = new System.Drawing.Size(820, 600);
            this.Name = "Feature3784PulsingTextBoxBorderDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Feature 3784 - Glowing Borders (Comprehensive)";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).EndInit();
            this.kryptonPanelMain.ResumeLayout(false);
            this.tableLayoutPanelRoot.ResumeLayout(false);
            this.panelSamplesScroll.ResumeLayout(false);
            this.panelSamplesScroll.PerformLayout();
            this.tableLayoutPanelSamples.ResumeLayout(false);
            this.tableLayoutPanelSamples.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbGlow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroupSettings.Panel)).EndInit();
            this.kryptonHeaderGroupSettings.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroupSettings)).EndInit();
            this.kryptonHeaderGroupSettings.ResumeLayout(false);
            this.tableLayoutPanelSettings.ResumeLayout(false);
            this.tableLayoutPanelSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTarget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbShowWhen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbStyle)).EndInit();
            this.flowLayoutPanelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonWrapLabel kwlblInfo;
        private Krypton.Toolkit.KryptonPanel kryptonPanelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRoot;
        private System.Windows.Forms.Panel panelSamplesScroll;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSamples;
        private Krypton.Toolkit.KryptonLabel klblHeroHeader;
        private Krypton.Toolkit.KryptonTextBox ktxtAnimatedGlow;
        private Krypton.Toolkit.KryptonLabel klblStaticHeader;
        private Krypton.Toolkit.KryptonTextBox ktxtStaticGlow;
        private Krypton.Toolkit.KryptonLabel klblMaskedHeader;
        private Krypton.Toolkit.KryptonMaskedTextBox kmtxtPhone;
        private Krypton.Toolkit.KryptonLabel klblComboHeader;
        private Krypton.Toolkit.KryptonComboBox kcmbGlow;
        private Krypton.Toolkit.KryptonLabel klblRichTextHeader;
        private Krypton.Toolkit.KryptonRichTextBox krtbGlow;
        private Krypton.Toolkit.KryptonLabel klblNumericHeader;
        private Krypton.Toolkit.KryptonNumericUpDown knudQuantity;
        private Krypton.Toolkit.KryptonLabel klblDomainHeader;
        private Krypton.Toolkit.KryptonDomainUpDown kdudPriority;
        private Krypton.Toolkit.KryptonLabel klblDateTimeHeader;
        private Krypton.Toolkit.KryptonDateTimePicker kdtpDue;
        private Krypton.Toolkit.KryptonLabel klblCalcHeader;
        private Krypton.Toolkit.KryptonCalcInput kcalcBudget;
        private Krypton.Toolkit.KryptonLabel klblButtonHeader;
        private Krypton.Toolkit.KryptonButton kbtnGlow;
        private Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroupSettings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSettings;
        private Krypton.Toolkit.KryptonLabel klblTarget;
        private Krypton.Toolkit.KryptonComboBox kcmbTarget;
        private Krypton.Toolkit.KryptonCheckBox kchkEnable;
        private Krypton.Toolkit.KryptonCheckBox kchkAnimate;
        private Krypton.Toolkit.KryptonLabel klblShowWhen;
        private Krypton.Toolkit.KryptonComboBox kcmbShowWhen;
        private Krypton.Toolkit.KryptonLabel klblStyle;
        private Krypton.Toolkit.KryptonComboBox kcmbStyle;
        private Krypton.Toolkit.KryptonLabel klblAnimSpeed;
        private Krypton.Toolkit.KryptonNumericUpDown knudAnimationSpeed;
        private Krypton.Toolkit.KryptonCheckBox kchkCueAnimate;
        private Krypton.Toolkit.KryptonLabel klblCueSpeed;
        private Krypton.Toolkit.KryptonNumericUpDown knudCueSpeed;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelButtons;
        private Krypton.Toolkit.KryptonButton kbtnApply;
        private Krypton.Toolkit.KryptonButton kbtnReset;
        private Krypton.Toolkit.KryptonButton kbtnOpenFormGlow;
        private Krypton.Toolkit.KryptonWrapLabel kwlblStatus;
    }
}

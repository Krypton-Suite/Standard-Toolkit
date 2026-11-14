#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

partial class EnhancedContextMenuDemo
{
    private System.ComponentModel.IContainer components = null;

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
        this.tableMain = new System.Windows.Forms.TableLayoutPanel();
        this.kwlblInfo = new Krypton.Toolkit.KryptonWrapLabel();
        this.kpnlOptions = new Krypton.Toolkit.KryptonPanel();
        this.kcmbTheme = new Krypton.Toolkit.KryptonThemeComboBox();
        this.kbtnClose = new Krypton.Toolkit.KryptonButton();
        this.tableBody = new System.Windows.Forms.TableLayoutPanel();
        this.tableEditors = new System.Windows.Forms.TableLayoutPanel();
        this.klblKrypton = new Krypton.Toolkit.KryptonLabel();
        this.klblNative = new Krypton.Toolkit.KryptonLabel();
        this.krtbKrypton = new Krypton.Toolkit.KryptonRichTextBox();
        this.rtbNative = new System.Windows.Forms.RichTextBox();
        this.kpnlConfig = new Krypton.Toolkit.KryptonPanel();
        this.kwlblConfig = new Krypton.Toolkit.KryptonWrapLabel();
        this.klblPosition = new Krypton.Toolkit.KryptonLabel();
        this.kcmbPosition = new Krypton.Toolkit.KryptonComboBox();
        this.klblIdleOpacity = new Krypton.Toolkit.KryptonLabel();
        this.knudIdleOpacity = new Krypton.Toolkit.KryptonNumericUpDown();
        this.klblApproach = new Krypton.Toolkit.KryptonLabel();
        this.knudApproach = new Krypton.Toolkit.KryptonNumericUpDown();
        this.klblGap = new Krypton.Toolkit.KryptonLabel();
        this.knudGap = new Krypton.Toolkit.KryptonNumericUpDown();
        this.kchkShowShadow = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkSelectionFade = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkKeepToolbar = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkShowMiniToolbar = new Krypton.Toolkit.KryptonCheckBox();
        this.kwlblItems = new Krypton.Toolkit.KryptonWrapLabel();
        this.kchkItemBold = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkItemItalic = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkItemUnderline = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkItemFont = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkItemSize = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkItemColor = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkItemGallery = new Krypton.Toolkit.KryptonCheckBox();
        this.kpgSettings = new Krypton.Toolkit.KryptonPropertyGrid();
        this.kwlblStatus = new Krypton.Toolkit.KryptonWrapLabel();
        ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
        this.kpnlMain.SuspendLayout();
        this.tableMain.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kpnlOptions)).BeginInit();
        this.kpnlOptions.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).BeginInit();
        this.tableBody.SuspendLayout();
        this.tableEditors.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kpnlConfig)).BeginInit();
        this.kpnlConfig.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbPosition)).BeginInit();
        this.SuspendLayout();
        //
        // kpnlMain
        //
        this.kpnlMain.Controls.Add(this.tableMain);
        this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kpnlMain.Location = new System.Drawing.Point(0, 0);
        this.kpnlMain.Name = "kpnlMain";
        this.kpnlMain.Padding = new System.Windows.Forms.Padding(12);
        this.kpnlMain.Size = new System.Drawing.Size(1284, 718);
        this.kpnlMain.TabIndex = 0;
        //
        // tableMain
        //
        this.tableMain.ColumnCount = 1;
        this.tableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.tableMain.Controls.Add(this.kwlblInfo, 0, 0);
        this.tableMain.Controls.Add(this.kpnlOptions, 0, 1);
        this.tableMain.Controls.Add(this.tableBody, 0, 2);
        this.tableMain.Controls.Add(this.kwlblStatus, 0, 3);
        this.tableMain.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tableMain.Location = new System.Drawing.Point(12, 12);
        this.tableMain.Name = "tableMain";
        this.tableMain.RowCount = 4;
        this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 78F));
        this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
        this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
        this.tableMain.Size = new System.Drawing.Size(1260, 694);
        this.tableMain.TabIndex = 0;
        //
        // kwlblInfo
        //
        this.kwlblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kwlblInfo.Location = new System.Drawing.Point(3, 3);
        this.kwlblInfo.Name = "kwlblInfo";
        this.kwlblInfo.Size = new System.Drawing.Size(1254, 72);
        this.kwlblInfo.Text = "Issue #3862 — Office-style enhanced context menu (configurable).\r\nRight-click either editor for Mini Toolbar + menu. Select text for the fade-in Mini Toolbar. Use the Configuration panel to show or hide commands, change position, gap, opacity, and selection fade. The property grid is the same surface the Visual Studio designer exposes.";
        //
        // kpnlOptions
        //
        this.kpnlOptions.Controls.Add(this.kcmbTheme);
        this.kpnlOptions.Controls.Add(this.kbtnClose);
        this.kpnlOptions.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kpnlOptions.Location = new System.Drawing.Point(3, 81);
        this.kpnlOptions.Name = "kpnlOptions";
        this.kpnlOptions.Size = new System.Drawing.Size(1254, 30);
        this.kpnlOptions.TabIndex = 1;
        //
        // kcmbTheme
        //
        this.kcmbTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbTheme.DropDownWidth = 280;
        this.kcmbTheme.IntegralHeight = false;
        this.kcmbTheme.Location = new System.Drawing.Point(0, 4);
        this.kcmbTheme.Name = "kcmbTheme";
        this.kcmbTheme.Size = new System.Drawing.Size(280, 21);
        this.kcmbTheme.TabIndex = 0;
        //
        // kbtnClose
        //
        this.kbtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.kbtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.kbtnClose.Location = new System.Drawing.Point(1150, 2);
        this.kbtnClose.Name = "kbtnClose";
        this.kbtnClose.Size = new System.Drawing.Size(100, 25);
        this.kbtnClose.TabIndex = 1;
        this.kbtnClose.Values.Text = "Close";
        this.kbtnClose.Click += new System.EventHandler(this.kbtnClose_Click);
        //
        // tableBody
        //
        this.tableBody.ColumnCount = 2;
        this.tableBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.tableBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
        this.tableBody.Controls.Add(this.tableEditors, 0, 0);
        this.tableBody.Controls.Add(this.kpnlConfig, 1, 0);
        this.tableBody.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tableBody.Location = new System.Drawing.Point(3, 117);
        this.tableBody.Name = "tableBody";
        this.tableBody.RowCount = 1;
        this.tableBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.tableBody.Size = new System.Drawing.Size(1254, 546);
        this.tableBody.TabIndex = 2;
        //
        // tableEditors
        //
        this.tableEditors.ColumnCount = 2;
        this.tableEditors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        this.tableEditors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        this.tableEditors.Controls.Add(this.klblKrypton, 0, 0);
        this.tableEditors.Controls.Add(this.klblNative, 1, 0);
        this.tableEditors.Controls.Add(this.krtbKrypton, 0, 1);
        this.tableEditors.Controls.Add(this.rtbNative, 1, 1);
        this.tableEditors.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tableEditors.Location = new System.Drawing.Point(0, 0);
        this.tableEditors.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
        this.tableEditors.Name = "tableEditors";
        this.tableEditors.RowCount = 2;
        this.tableEditors.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
        this.tableEditors.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.tableEditors.Size = new System.Drawing.Size(946, 546);
        this.tableEditors.TabIndex = 0;
        //
        // klblKrypton
        //
        this.klblKrypton.Dock = System.Windows.Forms.DockStyle.Fill;
        this.klblKrypton.Location = new System.Drawing.Point(3, 3);
        this.klblKrypton.Name = "klblKrypton";
        this.klblKrypton.Size = new System.Drawing.Size(467, 18);
        this.klblKrypton.TabIndex = 0;
        this.klblKrypton.Values.Text = "KryptonRichTextBox";
        //
        // klblNative
        //
        this.klblNative.Dock = System.Windows.Forms.DockStyle.Fill;
        this.klblNative.Location = new System.Drawing.Point(476, 3);
        this.klblNative.Name = "klblNative";
        this.klblNative.Size = new System.Drawing.Size(467, 18);
        this.klblNative.TabIndex = 1;
        this.klblNative.Values.Text = "Native RichTextBox";
        //
        // krtbKrypton
        //
        this.krtbKrypton.Dock = System.Windows.Forms.DockStyle.Fill;
        this.krtbKrypton.Location = new System.Drawing.Point(3, 27);
        this.krtbKrypton.Name = "krtbKrypton";
        this.krtbKrypton.Size = new System.Drawing.Size(467, 516);
        this.krtbKrypton.TabIndex = 2;
        this.krtbKrypton.Text = "";
        //
        // rtbNative
        //
        this.rtbNative.DetectUrls = false;
        this.rtbNative.Dock = System.Windows.Forms.DockStyle.Fill;
        this.rtbNative.HideSelection = false;
        this.rtbNative.Location = new System.Drawing.Point(476, 27);
        this.rtbNative.Name = "rtbNative";
        this.rtbNative.Size = new System.Drawing.Size(467, 516);
        this.rtbNative.TabIndex = 3;
        this.rtbNative.Text = "";
        //
        // kpnlConfig
        //
        this.kpnlConfig.Controls.Add(this.kpgSettings);
        this.kpnlConfig.Controls.Add(this.kchkItemGallery);
        this.kpnlConfig.Controls.Add(this.kchkItemColor);
        this.kpnlConfig.Controls.Add(this.kchkItemSize);
        this.kpnlConfig.Controls.Add(this.kchkItemFont);
        this.kpnlConfig.Controls.Add(this.kchkItemUnderline);
        this.kpnlConfig.Controls.Add(this.kchkItemItalic);
        this.kpnlConfig.Controls.Add(this.kchkItemBold);
        this.kpnlConfig.Controls.Add(this.kwlblItems);
        this.kpnlConfig.Controls.Add(this.kchkShowMiniToolbar);
        this.kpnlConfig.Controls.Add(this.kchkKeepToolbar);
        this.kpnlConfig.Controls.Add(this.kchkSelectionFade);
        this.kpnlConfig.Controls.Add(this.kchkShowShadow);
        this.kpnlConfig.Controls.Add(this.knudGap);
        this.kpnlConfig.Controls.Add(this.klblGap);
        this.kpnlConfig.Controls.Add(this.knudApproach);
        this.kpnlConfig.Controls.Add(this.klblApproach);
        this.kpnlConfig.Controls.Add(this.knudIdleOpacity);
        this.kpnlConfig.Controls.Add(this.klblIdleOpacity);
        this.kpnlConfig.Controls.Add(this.kcmbPosition);
        this.kpnlConfig.Controls.Add(this.klblPosition);
        this.kpnlConfig.Controls.Add(this.kwlblConfig);
        this.kpnlConfig.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kpnlConfig.Location = new System.Drawing.Point(954, 0);
        this.kpnlConfig.Margin = new System.Windows.Forms.Padding(0);
        this.kpnlConfig.Name = "kpnlConfig";
        this.kpnlConfig.Padding = new System.Windows.Forms.Padding(8);
        this.kpnlConfig.Size = new System.Drawing.Size(300, 546);
        this.kpnlConfig.TabIndex = 1;
        //
        // kwlblConfig
        //
        this.kwlblConfig.Location = new System.Drawing.Point(11, 8);
        this.kwlblConfig.Name = "kwlblConfig";
        this.kwlblConfig.Size = new System.Drawing.Size(90, 15);
        this.kwlblConfig.Text = "Configuration";
        //
        // klblPosition
        //
        this.klblPosition.Location = new System.Drawing.Point(11, 30);
        this.klblPosition.Name = "klblPosition";
        this.klblPosition.Size = new System.Drawing.Size(120, 20);
        this.klblPosition.TabIndex = 0;
        this.klblPosition.Values.Text = "Mini Toolbar position";
        //
        // kcmbPosition
        //
        this.kcmbPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbPosition.DropDownWidth = 277;
        this.kcmbPosition.IntegralHeight = false;
        this.kcmbPosition.Location = new System.Drawing.Point(11, 52);
        this.kcmbPosition.Name = "kcmbPosition";
        this.kcmbPosition.Size = new System.Drawing.Size(277, 21);
        this.kcmbPosition.TabIndex = 1;
        this.kcmbPosition.SelectedIndexChanged += new System.EventHandler(this.kcmbPosition_SelectedIndexChanged);
        //
        // klblIdleOpacity
        //
        this.klblIdleOpacity.Location = new System.Drawing.Point(11, 82);
        this.klblIdleOpacity.Name = "klblIdleOpacity";
        this.klblIdleOpacity.Size = new System.Drawing.Size(140, 20);
        this.klblIdleOpacity.TabIndex = 2;
        this.klblIdleOpacity.Values.Text = "Selection idle opacity";
        //
        // knudIdleOpacity
        //
        this.knudIdleOpacity.Location = new System.Drawing.Point(168, 80);
        this.knudIdleOpacity.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
        this.knudIdleOpacity.Name = "knudIdleOpacity";
        this.knudIdleOpacity.Size = new System.Drawing.Size(120, 22);
        this.knudIdleOpacity.TabIndex = 3;
        this.knudIdleOpacity.Value = new decimal(new int[] { 40, 0, 0, 0 });
        this.knudIdleOpacity.ValueChanged += new System.EventHandler(this.knudIdleOpacity_ValueChanged);
        //
        // klblApproach
        //
        this.klblApproach.Location = new System.Drawing.Point(11, 110);
        this.klblApproach.Name = "klblApproach";
        this.klblApproach.Size = new System.Drawing.Size(140, 20);
        this.klblApproach.TabIndex = 4;
        this.klblApproach.Values.Text = "Approach distance";
        //
        // knudApproach
        //
        this.knudApproach.Location = new System.Drawing.Point(168, 108);
        this.knudApproach.Maximum = new decimal(new int[] { 400, 0, 0, 0 });
        this.knudApproach.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        this.knudApproach.Name = "knudApproach";
        this.knudApproach.Size = new System.Drawing.Size(120, 22);
        this.knudApproach.TabIndex = 5;
        this.knudApproach.Value = new decimal(new int[] { 80, 0, 0, 0 });
        this.knudApproach.ValueChanged += new System.EventHandler(this.knudApproach_ValueChanged);
        //
        // klblGap
        //
        this.klblGap.Location = new System.Drawing.Point(11, 138);
        this.klblGap.Name = "klblGap";
        this.klblGap.Size = new System.Drawing.Size(140, 20);
        this.klblGap.TabIndex = 6;
        this.klblGap.Values.Text = "Toolbar / menu gap";
        //
        // knudGap
        //
        this.knudGap.Location = new System.Drawing.Point(168, 136);
        this.knudGap.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
        this.knudGap.Name = "knudGap";
        this.knudGap.Size = new System.Drawing.Size(120, 22);
        this.knudGap.TabIndex = 7;
        this.knudGap.Value = new decimal(new int[] { 2, 0, 0, 0 });
        this.knudGap.ValueChanged += new System.EventHandler(this.knudGap_ValueChanged);
        //
        // kchkShowShadow
        //
        this.kchkShowShadow.Checked = true;
        this.kchkShowShadow.CheckState = System.Windows.Forms.CheckState.Checked;
        this.kchkShowShadow.Location = new System.Drawing.Point(11, 166);
        this.kchkShowShadow.Name = "kchkShowShadow";
        this.kchkShowShadow.Size = new System.Drawing.Size(140, 20);
        this.kchkShowShadow.TabIndex = 8;
        this.kchkShowShadow.Values.Text = "Show shadow";
        this.kchkShowShadow.CheckedChanged += new System.EventHandler(this.kchkShowShadow_CheckedChanged);
        //
        // kchkSelectionFade
        //
        this.kchkSelectionFade.Checked = true;
        this.kchkSelectionFade.CheckState = System.Windows.Forms.CheckState.Checked;
        this.kchkSelectionFade.Location = new System.Drawing.Point(11, 188);
        this.kchkSelectionFade.Name = "kchkSelectionFade";
        this.kchkSelectionFade.Size = new System.Drawing.Size(250, 20);
        this.kchkSelectionFade.TabIndex = 9;
        this.kchkSelectionFade.Values.Text = "Selection Mini Toolbar (fade-in)";
        this.kchkSelectionFade.CheckedChanged += new System.EventHandler(this.kchkSelectionFade_CheckedChanged);
        //
        // kchkKeepToolbar
        //
        this.kchkKeepToolbar.Checked = true;
        this.kchkKeepToolbar.CheckState = System.Windows.Forms.CheckState.Checked;
        this.kchkKeepToolbar.Location = new System.Drawing.Point(11, 210);
        this.kchkKeepToolbar.Name = "kchkKeepToolbar";
        this.kchkKeepToolbar.Size = new System.Drawing.Size(250, 20);
        this.kchkKeepToolbar.TabIndex = 10;
        this.kchkKeepToolbar.Values.Text = "Keep Mini Toolbar after command";
        this.kchkKeepToolbar.CheckedChanged += new System.EventHandler(this.kchkKeepToolbar_CheckedChanged);
        //
        // kchkShowMiniToolbar
        //
        this.kchkShowMiniToolbar.Checked = true;
        this.kchkShowMiniToolbar.CheckState = System.Windows.Forms.CheckState.Checked;
        this.kchkShowMiniToolbar.Location = new System.Drawing.Point(11, 232);
        this.kchkShowMiniToolbar.Name = "kchkShowMiniToolbar";
        this.kchkShowMiniToolbar.Size = new System.Drawing.Size(200, 20);
        this.kchkShowMiniToolbar.TabIndex = 11;
        this.kchkShowMiniToolbar.Values.Text = "Show Mini Toolbar on menu";
        this.kchkShowMiniToolbar.CheckedChanged += new System.EventHandler(this.kchkShowMiniToolbar_CheckedChanged);
        //
        // kwlblItems
        //
        this.kwlblItems.Location = new System.Drawing.Point(11, 258);
        this.kwlblItems.Name = "kwlblItems";
        this.kwlblItems.Size = new System.Drawing.Size(160, 15);
        this.kwlblItems.Text = "Mini Toolbar commands";
        //
        // kchkItemBold
        //
        this.kchkItemBold.Checked = true;
        this.kchkItemBold.CheckState = System.Windows.Forms.CheckState.Checked;
        this.kchkItemBold.Location = new System.Drawing.Point(11, 278);
        this.kchkItemBold.Name = "kchkItemBold";
        this.kchkItemBold.Size = new System.Drawing.Size(90, 20);
        this.kchkItemBold.TabIndex = 12;
        this.kchkItemBold.Values.Text = "Bold";
        this.kchkItemBold.CheckedChanged += new System.EventHandler(this.OnItemVisibilityChanged);
        //
        // kchkItemItalic
        //
        this.kchkItemItalic.Checked = true;
        this.kchkItemItalic.CheckState = System.Windows.Forms.CheckState.Checked;
        this.kchkItemItalic.Location = new System.Drawing.Point(107, 278);
        this.kchkItemItalic.Name = "kchkItemItalic";
        this.kchkItemItalic.Size = new System.Drawing.Size(90, 20);
        this.kchkItemItalic.TabIndex = 13;
        this.kchkItemItalic.Values.Text = "Italic";
        this.kchkItemItalic.CheckedChanged += new System.EventHandler(this.OnItemVisibilityChanged);
        //
        // kchkItemUnderline
        //
        this.kchkItemUnderline.Checked = true;
        this.kchkItemUnderline.CheckState = System.Windows.Forms.CheckState.Checked;
        this.kchkItemUnderline.Location = new System.Drawing.Point(203, 278);
        this.kchkItemUnderline.Name = "kchkItemUnderline";
        this.kchkItemUnderline.Size = new System.Drawing.Size(90, 20);
        this.kchkItemUnderline.TabIndex = 14;
        this.kchkItemUnderline.Values.Text = "Underline";
        this.kchkItemUnderline.CheckedChanged += new System.EventHandler(this.OnItemVisibilityChanged);
        //
        // kchkItemFont
        //
        this.kchkItemFont.Checked = true;
        this.kchkItemFont.CheckState = System.Windows.Forms.CheckState.Checked;
        this.kchkItemFont.Location = new System.Drawing.Point(11, 300);
        this.kchkItemFont.Name = "kchkItemFont";
        this.kchkItemFont.Size = new System.Drawing.Size(90, 20);
        this.kchkItemFont.TabIndex = 15;
        this.kchkItemFont.Values.Text = "Font";
        this.kchkItemFont.CheckedChanged += new System.EventHandler(this.OnItemVisibilityChanged);
        //
        // kchkItemSize
        //
        this.kchkItemSize.Checked = true;
        this.kchkItemSize.CheckState = System.Windows.Forms.CheckState.Checked;
        this.kchkItemSize.Location = new System.Drawing.Point(107, 300);
        this.kchkItemSize.Name = "kchkItemSize";
        this.kchkItemSize.Size = new System.Drawing.Size(90, 20);
        this.kchkItemSize.TabIndex = 16;
        this.kchkItemSize.Values.Text = "Size";
        this.kchkItemSize.CheckedChanged += new System.EventHandler(this.OnItemVisibilityChanged);
        //
        // kchkItemColor
        //
        this.kchkItemColor.Checked = true;
        this.kchkItemColor.CheckState = System.Windows.Forms.CheckState.Checked;
        this.kchkItemColor.Location = new System.Drawing.Point(203, 300);
        this.kchkItemColor.Name = "kchkItemColor";
        this.kchkItemColor.Size = new System.Drawing.Size(90, 20);
        this.kchkItemColor.TabIndex = 17;
        this.kchkItemColor.Values.Text = "Colour";
        this.kchkItemColor.CheckedChanged += new System.EventHandler(this.OnItemVisibilityChanged);
        //
        // kchkItemGallery
        //
        this.kchkItemGallery.Checked = false;
        this.kchkItemGallery.CheckState = System.Windows.Forms.CheckState.Unchecked;
        this.kchkItemGallery.Location = new System.Drawing.Point(11, 322);
        this.kchkItemGallery.Name = "kchkItemGallery";
        this.kchkItemGallery.Size = new System.Drawing.Size(140, 20);
        this.kchkItemGallery.TabIndex = 18;
        this.kchkItemGallery.Values.Text = "Style gallery";
        this.kchkItemGallery.CheckedChanged += new System.EventHandler(this.OnItemVisibilityChanged);
        //
        // kpgSettings
        //
        this.kpgSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
        this.kpgSettings.Location = new System.Drawing.Point(11, 348);
        this.kpgSettings.Name = "kpgSettings";
        this.kpgSettings.Size = new System.Drawing.Size(277, 186);
        this.kpgSettings.TabIndex = 19;
        //
        // kwlblStatus
        //
        this.kwlblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kwlblStatus.Location = new System.Drawing.Point(3, 669);
        this.kwlblStatus.Name = "kwlblStatus";
        this.kwlblStatus.Size = new System.Drawing.Size(1254, 22);
        this.kwlblStatus.Text = "Ready.";
        //
        // EnhancedContextMenuDemo
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.CancelButton = this.kbtnClose;
        this.ClientSize = new System.Drawing.Size(1284, 718);
        this.Controls.Add(this.kpnlMain);
        this.MinimumSize = new System.Drawing.Size(960, 560);
        this.Name = "EnhancedContextMenuDemo";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Enhanced Context Menu (#3862)";
        ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
        this.kpnlMain.ResumeLayout(false);
        this.tableMain.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.kpnlOptions)).EndInit();
        this.kpnlOptions.ResumeLayout(false);
        this.kpnlOptions.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).EndInit();
        this.tableBody.ResumeLayout(false);
        this.tableEditors.ResumeLayout(false);
        this.tableEditors.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kpnlConfig)).EndInit();
        this.kpnlConfig.ResumeLayout(false);
        this.kpnlConfig.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbPosition)).EndInit();
        this.ResumeLayout(false);
    }

    #endregion

    private Krypton.Toolkit.KryptonPanel kpnlMain;
    private System.Windows.Forms.TableLayoutPanel tableMain;
    private Krypton.Toolkit.KryptonWrapLabel kwlblInfo;
    private Krypton.Toolkit.KryptonPanel kpnlOptions;
    private Krypton.Toolkit.KryptonThemeComboBox kcmbTheme;
    private Krypton.Toolkit.KryptonButton kbtnClose;
    private System.Windows.Forms.TableLayoutPanel tableBody;
    private System.Windows.Forms.TableLayoutPanel tableEditors;
    private Krypton.Toolkit.KryptonLabel klblKrypton;
    private Krypton.Toolkit.KryptonLabel klblNative;
    private Krypton.Toolkit.KryptonRichTextBox krtbKrypton;
    private System.Windows.Forms.RichTextBox rtbNative;
    private Krypton.Toolkit.KryptonPanel kpnlConfig;
    private Krypton.Toolkit.KryptonWrapLabel kwlblConfig;
    private Krypton.Toolkit.KryptonLabel klblPosition;
    private Krypton.Toolkit.KryptonComboBox kcmbPosition;
    private Krypton.Toolkit.KryptonLabel klblIdleOpacity;
    private Krypton.Toolkit.KryptonNumericUpDown knudIdleOpacity;
    private Krypton.Toolkit.KryptonLabel klblApproach;
    private Krypton.Toolkit.KryptonNumericUpDown knudApproach;
    private Krypton.Toolkit.KryptonLabel klblGap;
    private Krypton.Toolkit.KryptonNumericUpDown knudGap;
    private Krypton.Toolkit.KryptonCheckBox kchkShowShadow;
    private Krypton.Toolkit.KryptonCheckBox kchkSelectionFade;
    private Krypton.Toolkit.KryptonCheckBox kchkKeepToolbar;
    private Krypton.Toolkit.KryptonCheckBox kchkShowMiniToolbar;
    private Krypton.Toolkit.KryptonWrapLabel kwlblItems;
    private Krypton.Toolkit.KryptonCheckBox kchkItemBold;
    private Krypton.Toolkit.KryptonCheckBox kchkItemItalic;
    private Krypton.Toolkit.KryptonCheckBox kchkItemUnderline;
    private Krypton.Toolkit.KryptonCheckBox kchkItemFont;
    private Krypton.Toolkit.KryptonCheckBox kchkItemSize;
    private Krypton.Toolkit.KryptonCheckBox kchkItemColor;
    private Krypton.Toolkit.KryptonCheckBox kchkItemGallery;
    private Krypton.Toolkit.KryptonPropertyGrid kpgSettings;
    private Krypton.Toolkit.KryptonWrapLabel kwlblStatus;
}

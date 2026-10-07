#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

partial class RadialMenuDemo
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_hostedControl.IsFloating)
            {
                _hostedControl.DockBack();
            }

            components?.Dispose();
            _radialMenu.Dispose();
            _importedMenu.Dispose();
            _hostedControl.Dispose();
            _sourceContextMenu.Dispose();
            _cutCommand.Dispose();
            _copyCommand.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RadialMenuDemo));
            this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
            this.kpnlContent = new Krypton.Toolkit.KryptonPanel();
            this.kpnlSurface = new Krypton.Toolkit.KryptonPanel();
            this.kwlblHint = new Krypton.Toolkit.KryptonWrapLabel();
            this.kpnlHosted = new Krypton.Toolkit.KryptonPanel();
            this.kbtnDockHosted = new Krypton.Toolkit.KryptonButton();
            this.kwlblHosted = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktxtLog = new Krypton.Toolkit.KryptonTextBox();
            this.kpnlToolbar = new Krypton.Toolkit.KryptonPanel();
            this.kbtnShowAtCursor = new Krypton.Toolkit.KryptonButton();
            this.kchkPreferRadial = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkUseHub = new Krypton.Toolkit.KryptonCheckBox();
            this.kwlblHubText = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktxtHubText = new Krypton.Toolkit.KryptonTextBox();
            this.kchkHubImage = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkShowCheckedGlyph = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkShowShadow = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkShowOuterRingOnLeaves = new Krypton.Toolkit.KryptonCheckBox();
            this.kwlblShadowBlur = new Krypton.Toolkit.KryptonWrapLabel();
            this.knudShadowBlur = new Krypton.Toolkit.KryptonNumericUpDown();
            this.kwlblShadowOffset = new Krypton.Toolkit.KryptonWrapLabel();
            this.knudShadowOffset = new Krypton.Toolkit.KryptonNumericUpDown();
            this.kcmbImageSize = new Krypton.Toolkit.KryptonComboBox();
            this.kwlblImageSize = new Krypton.Toolkit.KryptonWrapLabel();
            this.knudScale = new Krypton.Toolkit.KryptonNumericUpDown();
            this.kwlblScale = new Krypton.Toolkit.KryptonWrapLabel();
            this.knudOuterRing = new Krypton.Toolkit.KryptonNumericUpDown();
            this.kwlblOuterRing = new Krypton.Toolkit.KryptonWrapLabel();
            this.kcmbDisplayStyle = new Krypton.Toolkit.KryptonComboBox();
            this.kwlblDisplayStyle = new Krypton.Toolkit.KryptonWrapLabel();
            this.kcmbAnimation = new Krypton.Toolkit.KryptonComboBox();
            this.kwlblAnimation = new Krypton.Toolkit.KryptonWrapLabel();
            this.kchkAllowMove = new Krypton.Toolkit.KryptonCheckBox();
            this.krdoImported = new Krypton.Toolkit.KryptonRadioButton();
            this.krdoNative = new Krypton.Toolkit.KryptonRadioButton();
            this.kwlblMode = new Krypton.Toolkit.KryptonWrapLabel();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonPropertyGrid1 = new Krypton.Toolkit.KryptonPropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
            this.kpnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlContent)).BeginInit();
            this.kpnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlSurface)).BeginInit();
            this.kpnlSurface.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlHosted)).BeginInit();
            this.kpnlHosted.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlToolbar)).BeginInit();
            this.kpnlToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbImageSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbDisplayStyle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbAnimation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kpnlMain
            // 
            this.kpnlMain.Controls.Add(this.kpnlContent);
            this.kpnlMain.Controls.Add(this.ktxtLog);
            this.kpnlMain.Controls.Add(this.kpnlToolbar);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 0);
            this.kpnlMain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.Padding = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.kpnlMain.Size = new System.Drawing.Size(1040, 642);
            this.kpnlMain.TabIndex = 0;
            // 
            // kpnlContent
            // 
            this.kpnlContent.Controls.Add(this.kpnlSurface);
            this.kpnlContent.Controls.Add(this.kpnlHosted);
            this.kpnlContent.Controls.Add(this.kryptonPanel1);
            this.kpnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlContent.Location = new System.Drawing.Point(9, 108);
            this.kpnlContent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.kpnlContent.Name = "kpnlContent";
            this.kpnlContent.Size = new System.Drawing.Size(1022, 394);
            this.kpnlContent.TabIndex = 1;
            // 
            // kpnlSurface
            // 
            this.kpnlSurface.Controls.Add(this.kwlblHint);
            this.kpnlSurface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlSurface.Location = new System.Drawing.Point(0, 0);
            this.kpnlSurface.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.kpnlSurface.Name = "kpnlSurface";
            this.kpnlSurface.Padding = new System.Windows.Forms.Padding(12, 13, 12, 13);
            this.kpnlSurface.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kpnlSurface.Size = new System.Drawing.Size(343, 394);
            this.kpnlSurface.TabIndex = 0;
            this.kpnlSurface.MouseUp += new System.Windows.Forms.MouseEventHandler(this.kpnlSurface_MouseUp);
            // 
            // kwlblHint
            // 
            this.kwlblHint.Dock = System.Windows.Forms.DockStyle.Top;
            this.kwlblHint.Location = new System.Drawing.Point(12, 13);
            this.kwlblHint.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.kwlblHint.Name = "kwlblHint";
            this.kwlblHint.Size = new System.Drawing.Size(428, 90);
            this.kwlblHint.Text = resources.GetString("kwlblHint.Text");
            this.kwlblHint.MouseUp += new System.Windows.Forms.MouseEventHandler(this.kpnlSurface_MouseUp);
            // 
            // kpnlHosted
            // 
            this.kpnlHosted.Controls.Add(this.kbtnDockHosted);
            this.kpnlHosted.Controls.Add(this.kwlblHosted);
            this.kpnlHosted.Dock = System.Windows.Forms.DockStyle.Right;
            this.kpnlHosted.Location = new System.Drawing.Point(343, 0);
            this.kpnlHosted.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.kpnlHosted.Name = "kpnlHosted";
            this.kpnlHosted.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.kpnlHosted.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kpnlHosted.Size = new System.Drawing.Size(328, 394);
            this.kpnlHosted.TabIndex = 1;
            this.kpnlHosted.Resize += new System.EventHandler(this.kpnlHosted_Resize);
            // 
            // kbtnDockHosted
            // 
            this.kbtnDockHosted.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kbtnDockHosted.Enabled = false;
            this.kbtnDockHosted.Location = new System.Drawing.Point(6, 362);
            this.kbtnDockHosted.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.kbtnDockHosted.Name = "kbtnDockHosted";
            this.kbtnDockHosted.Size = new System.Drawing.Size(316, 26);
            this.kbtnDockHosted.TabIndex = 1;
            this.kbtnDockHosted.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnDockHosted.Values.Text = "Dock hosted control back";
            this.kbtnDockHosted.Click += new System.EventHandler(this.kbtnDockHosted_Click);
            // 
            // kwlblHosted
            // 
            this.kwlblHosted.Dock = System.Windows.Forms.DockStyle.Top;
            this.kwlblHosted.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.kwlblHosted.Location = new System.Drawing.Point(6, 6);
            this.kwlblHosted.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.kwlblHosted.Name = "kwlblHosted";
            this.kwlblHosted.Size = new System.Drawing.Size(215, 30);
            this.kwlblHosted.Text = "Hosted control\r\nAllow move: drag hub/centre past the form edge to float";
            // 
            // ktxtLog
            // 
            this.ktxtLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ktxtLog.Location = new System.Drawing.Point(9, 502);
            this.ktxtLog.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ktxtLog.Multiline = true;
            this.ktxtLog.Name = "ktxtLog";
            this.ktxtLog.ReadOnly = true;
            this.ktxtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ktxtLog.Size = new System.Drawing.Size(1022, 130);
            this.ktxtLog.TabIndex = 2;
            // 
            // kpnlToolbar
            // 
            this.kpnlToolbar.Controls.Add(this.kbtnShowAtCursor);
            this.kpnlToolbar.Controls.Add(this.kchkPreferRadial);
            this.kpnlToolbar.Controls.Add(this.kchkUseHub);
            this.kpnlToolbar.Controls.Add(this.kwlblHubText);
            this.kpnlToolbar.Controls.Add(this.ktxtHubText);
            this.kpnlToolbar.Controls.Add(this.kchkHubImage);
            this.kpnlToolbar.Controls.Add(this.kchkShowCheckedGlyph);
            this.kpnlToolbar.Controls.Add(this.kchkShowShadow);
            this.kpnlToolbar.Controls.Add(this.kchkShowOuterRingOnLeaves);
            this.kpnlToolbar.Controls.Add(this.kwlblShadowBlur);
            this.kpnlToolbar.Controls.Add(this.knudShadowBlur);
            this.kpnlToolbar.Controls.Add(this.kwlblShadowOffset);
            this.kpnlToolbar.Controls.Add(this.knudShadowOffset);
            this.kpnlToolbar.Controls.Add(this.kcmbImageSize);
            this.kpnlToolbar.Controls.Add(this.kwlblImageSize);
            this.kpnlToolbar.Controls.Add(this.knudScale);
            this.kpnlToolbar.Controls.Add(this.kwlblScale);
            this.kpnlToolbar.Controls.Add(this.knudOuterRing);
            this.kpnlToolbar.Controls.Add(this.kwlblOuterRing);
            this.kpnlToolbar.Controls.Add(this.kcmbDisplayStyle);
            this.kpnlToolbar.Controls.Add(this.kwlblDisplayStyle);
            this.kpnlToolbar.Controls.Add(this.kcmbAnimation);
            this.kpnlToolbar.Controls.Add(this.kwlblAnimation);
            this.kpnlToolbar.Controls.Add(this.kchkAllowMove);
            this.kpnlToolbar.Controls.Add(this.krdoImported);
            this.kpnlToolbar.Controls.Add(this.krdoNative);
            this.kpnlToolbar.Controls.Add(this.kwlblMode);
            this.kpnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.kpnlToolbar.Location = new System.Drawing.Point(9, 10);
            this.kpnlToolbar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.kpnlToolbar.Name = "kpnlToolbar";
            this.kpnlToolbar.Size = new System.Drawing.Size(1022, 98);
            this.kpnlToolbar.TabIndex = 0;
            // 
            // kbtnShowAtCursor
            // 
            this.kbtnShowAtCursor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnShowAtCursor.Location = new System.Drawing.Point(914, 6);
            this.kbtnShowAtCursor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.kbtnShowAtCursor.Name = "kbtnShowAtCursor";
            this.kbtnShowAtCursor.Size = new System.Drawing.Size(99, 23);
            this.kbtnShowAtCursor.TabIndex = 4;
            this.kbtnShowAtCursor.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnShowAtCursor.Values.Text = "Show popup";
            this.kbtnShowAtCursor.Click += new System.EventHandler(this.kbtnShowAtCursor_Click);
            // 
            // kchkPreferRadial
            // 
            this.kchkPreferRadial.Location = new System.Drawing.Point(6, 57);
            this.kchkPreferRadial.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.kchkPreferRadial.Name = "kchkPreferRadial";
            this.kchkPreferRadial.Size = new System.Drawing.Size(296, 20);
            this.kchkPreferRadial.TabIndex = 10;
            this.kchkPreferRadial.Values.Text = "PreferRadialContextMenus (imported + Presenter)";
            this.kchkPreferRadial.CheckedChanged += new System.EventHandler(this.kchkPreferRadial_CheckedChanged);
            // 
            // kchkUseHub
            // 
            this.kchkUseHub.Checked = true;
            this.kchkUseHub.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkUseHub.Location = new System.Drawing.Point(267, 57);
            this.kchkUseHub.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.kchkUseHub.Name = "kchkUseHub";
            this.kchkUseHub.Size = new System.Drawing.Size(109, 20);
            this.kchkUseHub.TabIndex = 11;
            this.kchkUseHub.Values.Text = "Hosted UseHub";
            this.kchkUseHub.CheckedChanged += new System.EventHandler(this.kchkUseHub_CheckedChanged);
            // 
            // kwlblHubText
            // 
            this.kwlblHubText.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kwlblHubText.Location = new System.Drawing.Point(366, 60);
            this.kwlblHubText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.kwlblHubText.Name = "kwlblHubText";
            this.kwlblHubText.Size = new System.Drawing.Size(55, 15);
            this.kwlblHubText.Text = "Hub text:";
            // 
            // ktxtHubText
            // 
            this.ktxtHubText.Location = new System.Drawing.Point(414, 57);
            this.ktxtHubText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ktxtHubText.Name = "ktxtHubText";
            this.ktxtHubText.Size = new System.Drawing.Size(60, 23);
            this.ktxtHubText.TabIndex = 12;
            this.ktxtHubText.Text = "+";
            this.ktxtHubText.TextChanged += new System.EventHandler(this.ktxtHubText_TextChanged);
            // 
            // kchkHubImage
            // 
            this.kchkHubImage.Location = new System.Drawing.Point(483, 57);
            this.kchkHubImage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.kchkHubImage.Name = "kchkHubImage";
            this.kchkHubImage.Size = new System.Drawing.Size(84, 20);
            this.kchkHubImage.TabIndex = 13;
            this.kchkHubImage.Values.Text = "Hub image";
            this.kchkHubImage.CheckedChanged += new System.EventHandler(this.kchkHubImage_CheckedChanged);
            // 
            // kchkShowCheckedGlyph
            // 
            this.kchkShowCheckedGlyph.Location = new System.Drawing.Point(660, 57);
            this.kchkShowCheckedGlyph.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.kchkShowCheckedGlyph.Name = "kchkShowCheckedGlyph";
            this.kchkShowCheckedGlyph.Size = new System.Drawing.Size(104, 20);
            this.kchkShowCheckedGlyph.TabIndex = 14;
            this.kchkShowCheckedGlyph.Values.Text = "Checked glyph";
            this.kchkShowCheckedGlyph.CheckedChanged += new System.EventHandler(this.kchkShowCheckedGlyph_CheckedChanged);
            // 
            // kchkShowShadow
            // 
            this.kchkShowShadow.Location = new System.Drawing.Point(460, 32);
            this.kchkShowShadow.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.kchkShowShadow.Name = "kchkShowShadow";
            this.kchkShowShadow.Size = new System.Drawing.Size(99, 20);
            this.kchkShowShadow.TabIndex = 9;
            this.kchkShowShadow.Values.Text = "Show shadow";
            this.kchkShowShadow.CheckedChanged += new System.EventHandler(this.kchkShowShadow_CheckedChanged);
            // 
            // kchkShowOuterRingOnLeaves
            // 
            this.kchkShowOuterRingOnLeaves.Location = new System.Drawing.Point(770, 57);
            this.kchkShowOuterRingOnLeaves.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.kchkShowOuterRingOnLeaves.Name = "kchkShowOuterRingOnLeaves";
            this.kchkShowOuterRingOnLeaves.Size = new System.Drawing.Size(118, 20);
            this.kchkShowOuterRingOnLeaves.TabIndex = 16;
            this.kchkShowOuterRingOnLeaves.Values.Text = "Ring on leaves";
            this.kchkShowOuterRingOnLeaves.CheckedChanged += new System.EventHandler(this.kchkShowOuterRingOnLeaves_CheckedChanged);
            // 
            // kwlblShadowBlur
            // 
            this.kwlblShadowBlur.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kwlblShadowBlur.Location = new System.Drawing.Point(540, 36);
            this.kwlblShadowBlur.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.kwlblShadowBlur.Name = "kwlblShadowBlur";
            this.kwlblShadowBlur.Size = new System.Drawing.Size(31, 15);
            this.kwlblShadowBlur.Text = "Blur:";
            // 
            // knudShadowBlur
            // 
            this.knudShadowBlur.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.knudShadowBlur.Location = new System.Drawing.Point(567, 32);
            this.knudShadowBlur.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.knudShadowBlur.Maximum = new decimal(new int[] {
            48,
            0,
            0,
            0});
            this.knudShadowBlur.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.knudShadowBlur.Name = "knudShadowBlur";
            this.knudShadowBlur.Size = new System.Drawing.Size(36, 22);
            this.knudShadowBlur.TabIndex = 10;
            this.knudShadowBlur.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.knudShadowBlur.ValueChanged += new System.EventHandler(this.knudShadowBlur_ValueChanged);
            // 
            // kwlblShadowOffset
            // 
            this.kwlblShadowOffset.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kwlblShadowOffset.Location = new System.Drawing.Point(609, 36);
            this.kwlblShadowOffset.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.kwlblShadowOffset.Name = "kwlblShadowOffset";
            this.kwlblShadowOffset.Size = new System.Drawing.Size(36, 15);
            this.kwlblShadowOffset.Text = "Drop:";
            // 
            // knudShadowOffset
            // 
            this.knudShadowOffset.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.knudShadowOffset.Location = new System.Drawing.Point(642, 32);
            this.knudShadowOffset.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.knudShadowOffset.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.knudShadowOffset.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.knudShadowOffset.Name = "knudShadowOffset";
            this.knudShadowOffset.Size = new System.Drawing.Size(36, 22);
            this.knudShadowOffset.TabIndex = 11;
            this.knudShadowOffset.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.knudShadowOffset.ValueChanged += new System.EventHandler(this.knudShadowOffset_ValueChanged);
            // 
            // kcmbImageSize
            // 
            this.kcmbImageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbImageSize.Location = new System.Drawing.Point(327, 32);
            this.kcmbImageSize.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.kcmbImageSize.Name = "kcmbImageSize";
            this.kcmbImageSize.Size = new System.Drawing.Size(48, 22);
            this.kcmbImageSize.TabIndex = 7;
            this.kcmbImageSize.SelectedIndexChanged += new System.EventHandler(this.kcmbImageSize_SelectedIndexChanged);
            // 
            // kwlblImageSize
            // 
            this.kwlblImageSize.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kwlblImageSize.Location = new System.Drawing.Point(297, 36);
            this.kwlblImageSize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.kwlblImageSize.Name = "kwlblImageSize";
            this.kwlblImageSize.Size = new System.Drawing.Size(30, 15);
            this.kwlblImageSize.Text = "Size:";
            // 
            // knudScale
            // 
            this.knudScale.AllowDecimals = true;
            this.knudScale.DecimalPlaces = 2;
            this.knudScale.Increment = new decimal(new int[] {
            25,
            0,
            0,
            65536});
            this.knudScale.Location = new System.Drawing.Point(610, 57);
            this.knudScale.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.knudScale.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.knudScale.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.knudScale.Name = "knudScale";
            this.knudScale.Size = new System.Drawing.Size(42, 22);
            this.knudScale.TabIndex = 15;
            this.knudScale.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.knudScale.ValueChanged += new System.EventHandler(this.knudScale_ValueChanged);
            // 
            // kwlblScale
            // 
            this.kwlblScale.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kwlblScale.Location = new System.Drawing.Point(578, 60);
            this.kwlblScale.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.kwlblScale.Name = "kwlblScale";
            this.kwlblScale.Size = new System.Drawing.Size(37, 15);
            this.kwlblScale.Text = "Scale:";
            // 
            // knudOuterRing
            // 
            this.knudOuterRing.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.knudOuterRing.Location = new System.Drawing.Point(412, 32);
            this.knudOuterRing.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.knudOuterRing.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.knudOuterRing.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.knudOuterRing.Name = "knudOuterRing";
            this.knudOuterRing.Size = new System.Drawing.Size(39, 22);
            this.knudOuterRing.TabIndex = 8;
            this.knudOuterRing.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.knudOuterRing.ValueChanged += new System.EventHandler(this.knudOuterRing_ValueChanged);
            // 
            // kwlblOuterRing
            // 
            this.kwlblOuterRing.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kwlblOuterRing.Location = new System.Drawing.Point(382, 36);
            this.kwlblOuterRing.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.kwlblOuterRing.Name = "kwlblOuterRing";
            this.kwlblOuterRing.Size = new System.Drawing.Size(34, 15);
            this.kwlblOuterRing.Text = "Ring:";
            // 
            // kcmbDisplayStyle
            // 
            this.kcmbDisplayStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbDisplayStyle.Location = new System.Drawing.Point(183, 32);
            this.kcmbDisplayStyle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.kcmbDisplayStyle.Name = "kcmbDisplayStyle";
            this.kcmbDisplayStyle.Size = new System.Drawing.Size(105, 22);
            this.kcmbDisplayStyle.TabIndex = 6;
            this.kcmbDisplayStyle.SelectedIndexChanged += new System.EventHandler(this.kcmbDisplayStyle_SelectedIndexChanged);
            // 
            // kwlblDisplayStyle
            // 
            this.kwlblDisplayStyle.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kwlblDisplayStyle.Location = new System.Drawing.Point(150, 36);
            this.kwlblDisplayStyle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.kwlblDisplayStyle.Name = "kwlblDisplayStyle";
            this.kwlblDisplayStyle.Size = new System.Drawing.Size(35, 15);
            this.kwlblDisplayStyle.Text = "Style:";
            // 
            // kcmbAnimation
            // 
            this.kcmbAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbAnimation.Location = new System.Drawing.Point(58, 32);
            this.kcmbAnimation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.kcmbAnimation.Name = "kcmbAnimation";
            this.kcmbAnimation.Size = new System.Drawing.Size(82, 22);
            this.kcmbAnimation.TabIndex = 5;
            this.kcmbAnimation.SelectedIndexChanged += new System.EventHandler(this.kcmbAnimation_SelectedIndexChanged);
            // 
            // kwlblAnimation
            // 
            this.kwlblAnimation.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kwlblAnimation.Location = new System.Drawing.Point(6, 36);
            this.kwlblAnimation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.kwlblAnimation.Name = "kwlblAnimation";
            this.kwlblAnimation.Size = new System.Drawing.Size(66, 15);
            this.kwlblAnimation.Text = "Animation:";
            // 
            // kchkAllowMove
            // 
            this.kchkAllowMove.Location = new System.Drawing.Point(327, 8);
            this.kchkAllowMove.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.kchkAllowMove.Name = "kchkAllowMove";
            this.kchkAllowMove.Size = new System.Drawing.Size(87, 20);
            this.kchkAllowMove.TabIndex = 3;
            this.kchkAllowMove.Values.Text = "Allow move";
            this.kchkAllowMove.CheckedChanged += new System.EventHandler(this.kchkAllowMove_CheckedChanged);
            // 
            // krdoImported
            // 
            this.krdoImported.Location = new System.Drawing.Point(160, 8);
            this.krdoImported.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.krdoImported.Name = "krdoImported";
            this.krdoImported.Size = new System.Drawing.Size(180, 20);
            this.krdoImported.TabIndex = 2;
            this.krdoImported.Values.Text = "Imported from ContextMenu";
            // 
            // krdoNative
            // 
            this.krdoNative.Checked = true;
            this.krdoNative.Location = new System.Drawing.Point(42, 8);
            this.krdoNative.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.krdoNative.Name = "krdoNative";
            this.krdoNative.Size = new System.Drawing.Size(124, 20);
            this.krdoNative.TabIndex = 1;
            this.krdoNative.Values.Text = "Native radial items";
            // 
            // kwlblMode
            // 
            this.kwlblMode.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kwlblMode.Location = new System.Drawing.Point(6, 10);
            this.kwlblMode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.kwlblMode.Name = "kwlblMode";
            this.kwlblMode.Size = new System.Drawing.Size(41, 15);
            this.kwlblMode.Text = "Mode:";
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonPropertyGrid1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.kryptonPanel1.Location = new System.Drawing.Point(671, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(351, 394);
            this.kryptonPanel1.TabIndex = 2;
            // 
            // kryptonPropertyGrid1
            // 
            this.kryptonPropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPropertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPropertyGrid1.Name = "kryptonPropertyGrid1";
            this.kryptonPropertyGrid1.Padding = new System.Windows.Forms.Padding(1);
            this.kryptonPropertyGrid1.Size = new System.Drawing.Size(351, 394);
            this.kryptonPropertyGrid1.TabIndex = 0;
            this.kryptonPropertyGrid1.Text = "kryptonPropertyGrid1";
            // 
            // RadialMenuDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 642);
            this.Controls.Add(this.kpnlMain);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "RadialMenuDemo";
            this.Text = "Radial Menu Demo (#4172)";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
            this.kpnlMain.ResumeLayout(false);
            this.kpnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlContent)).EndInit();
            this.kpnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlSurface)).EndInit();
            this.kpnlSurface.ResumeLayout(false);
            this.kpnlSurface.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlHosted)).EndInit();
            this.kpnlHosted.ResumeLayout(false);
            this.kpnlHosted.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlToolbar)).EndInit();
            this.kpnlToolbar.ResumeLayout(false);
            this.kpnlToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbImageSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbDisplayStyle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbAnimation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion

    private Krypton.Toolkit.KryptonPanel kpnlMain;
    private Krypton.Toolkit.KryptonTextBox ktxtLog;
    private Krypton.Toolkit.KryptonPanel kpnlContent;
    private Krypton.Toolkit.KryptonPanel kpnlSurface;
    private Krypton.Toolkit.KryptonWrapLabel kwlblHint;
    private Krypton.Toolkit.KryptonPanel kpnlHosted;
    private Krypton.Toolkit.KryptonWrapLabel kwlblHosted;
    private Krypton.Toolkit.KryptonButton kbtnDockHosted;
    private Krypton.Toolkit.KryptonPanel kpnlToolbar;
    private Krypton.Toolkit.KryptonButton kbtnShowAtCursor;
    private Krypton.Toolkit.KryptonCheckBox kchkPreferRadial;
    private Krypton.Toolkit.KryptonCheckBox kchkUseHub;
    private Krypton.Toolkit.KryptonWrapLabel kwlblHubText;
    private Krypton.Toolkit.KryptonTextBox ktxtHubText;
    private Krypton.Toolkit.KryptonCheckBox kchkHubImage;
    private Krypton.Toolkit.KryptonCheckBox kchkShowCheckedGlyph;
    private Krypton.Toolkit.KryptonCheckBox kchkShowShadow;
    private Krypton.Toolkit.KryptonCheckBox kchkShowOuterRingOnLeaves;
    private Krypton.Toolkit.KryptonWrapLabel kwlblShadowBlur;
    private Krypton.Toolkit.KryptonNumericUpDown knudShadowBlur;
    private Krypton.Toolkit.KryptonWrapLabel kwlblShadowOffset;
    private Krypton.Toolkit.KryptonNumericUpDown knudShadowOffset;
    private Krypton.Toolkit.KryptonComboBox kcmbImageSize;
    private Krypton.Toolkit.KryptonWrapLabel kwlblImageSize;
    private Krypton.Toolkit.KryptonNumericUpDown knudOuterRing;
    private Krypton.Toolkit.KryptonWrapLabel kwlblOuterRing;
    private Krypton.Toolkit.KryptonNumericUpDown knudScale;
    private Krypton.Toolkit.KryptonWrapLabel kwlblScale;
    private Krypton.Toolkit.KryptonComboBox kcmbDisplayStyle;
    private Krypton.Toolkit.KryptonWrapLabel kwlblDisplayStyle;
    private Krypton.Toolkit.KryptonCheckBox kchkAllowMove;
    private Krypton.Toolkit.KryptonComboBox kcmbAnimation;
    private Krypton.Toolkit.KryptonWrapLabel kwlblAnimation;
    private Krypton.Toolkit.KryptonRadioButton krdoImported;
    private Krypton.Toolkit.KryptonRadioButton krdoNative;
    private Krypton.Toolkit.KryptonWrapLabel kwlblMode;
    private KryptonPanel kryptonPanel1;
    private KryptonPropertyGrid kryptonPropertyGrid1;
}

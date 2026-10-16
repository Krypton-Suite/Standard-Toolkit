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
    partial class KryptonRatingDemo
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
            this.kwlHeader = new Krypton.Toolkit.KryptonWrapLabel();
            this.kscMain = new Krypton.Toolkit.KryptonSplitContainer();
            this.kpnlLeft = new Krypton.Toolkit.KryptonPanel();
            this.tlpSettings = new System.Windows.Forms.TableLayoutPanel();
            this.klblPrecision = new Krypton.Toolkit.KryptonLabel();
            this.kcmbPrecision = new Krypton.Toolkit.KryptonComboBox();
            this.klblMaximum = new Krypton.Toolkit.KryptonLabel();
            this.knudMaximum = new Krypton.Toolkit.KryptonNumericUpDown();
            this.klblGlyph = new Krypton.Toolkit.KryptonLabel();
            this.kcmbGlyph = new Krypton.Toolkit.KryptonComboBox();
            this.klblOrientation = new Krypton.Toolkit.KryptonLabel();
            this.kcmbOrientation = new Krypton.Toolkit.KryptonComboBox();
            this.klblItemSize = new Krypton.Toolkit.KryptonLabel();
            this.knudItemSize = new Krypton.Toolkit.KryptonNumericUpDown();
            this.klblTheme = new Krypton.Toolkit.KryptonLabel();
            this.kcmbTheme = new Krypton.Toolkit.KryptonThemeComboBox();
            this.flpFlags = new System.Windows.Forms.FlowLayoutPanel();
            this.kchkReadOnly = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkAllowClear = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkEnabled = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkRtl = new Krypton.Toolkit.KryptonCheckBox();
            this.kbtnFillGold = new Krypton.Toolkit.KryptonButton();
            this.kbtnFillBlue = new Krypton.Toolkit.KryptonButton();
            this.klblValue = new Krypton.Toolkit.KryptonLabel();
            this.klblHover = new Krypton.Toolkit.KryptonLabel();
            this.kratingMain = new Krypton.Toolkit.KryptonRating();
            this.klblMainCaption = new Krypton.Toolkit.KryptonLabel();
            this.tlpCompare = new System.Windows.Forms.TableLayoutPanel();
            this.klblDisabled = new Krypton.Toolkit.KryptonLabel();
            this.kratingDisabled = new Krypton.Toolkit.KryptonRating();
            this.klblImage = new Krypton.Toolkit.KryptonLabel();
            this.kratingImage = new Krypton.Toolkit.KryptonRating();
            this.klblHeart = new Krypton.Toolkit.KryptonLabel();
            this.kratingHeart = new Krypton.Toolkit.KryptonRating();
            this.klblVertical = new Krypton.Toolkit.KryptonLabel();
            this.kratingVertical = new Krypton.Toolkit.KryptonRating();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            this.kpnlRight = new Krypton.Toolkit.KryptonPanel();
            this.kpgMain = new Krypton.Toolkit.KryptonPropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.kscMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kscMain.Panel1)).BeginInit();
            this.kscMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kscMain.Panel2)).BeginInit();
            this.kscMain.Panel2.SuspendLayout();
            this.kscMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlLeft)).BeginInit();
            this.kpnlLeft.SuspendLayout();
            this.tlpSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbPrecision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbGlyph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbOrientation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).BeginInit();
            this.flpFlags.SuspendLayout();
            this.tlpCompare.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlRight)).BeginInit();
            this.kpnlRight.SuspendLayout();
            this.SuspendLayout();
            //
            // kwlHeader
            //
            this.kwlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.kwlHeader.Location = new System.Drawing.Point(0, 0);
            this.kwlHeader.Name = "kwlHeader";
            this.kwlHeader.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.kwlHeader.Size = new System.Drawing.Size(1080, 72);
            this.kwlHeader.TabIndex = 0;
            this.kwlHeader.Text = "Issue #3928: KryptonRating. Hover a glyph for a live preview, click to commit, and" +
    " switch Precision between Full, Half, and Exact. Compare disabled, Image (stock s" +
    "tars), Heart, and Vertical samples. Theme changes recolour empty/outline glyphs.";
            //
            // kscMain
            //
            this.kscMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kscMain.Location = new System.Drawing.Point(0, 72);
            this.kscMain.Name = "kscMain";
            this.kscMain.SeparatorStyle = Krypton.Toolkit.SeparatorStyle.HighProfile;
            this.kscMain.Size = new System.Drawing.Size(1080, 568);
            this.kscMain.SplitterDistance = 760;
            this.kscMain.TabIndex = 1;
            //
            // kscMain.Panel1
            //
            this.kscMain.Panel1.Controls.Add(this.kpnlLeft);
            //
            // kscMain.Panel2
            //
            this.kscMain.Panel2.Controls.Add(this.kpnlRight);
            //
            // kpnlLeft
            //
            this.kpnlLeft.Controls.Add(this.tlpCompare);
            this.kpnlLeft.Controls.Add(this.klblStatus);
            this.kpnlLeft.Controls.Add(this.klblHover);
            this.kpnlLeft.Controls.Add(this.klblValue);
            this.kpnlLeft.Controls.Add(this.kratingMain);
            this.kpnlLeft.Controls.Add(this.klblMainCaption);
            this.kpnlLeft.Controls.Add(this.flpFlags);
            this.kpnlLeft.Controls.Add(this.tlpSettings);
            this.kpnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlLeft.Location = new System.Drawing.Point(0, 0);
            this.kpnlLeft.Name = "kpnlLeft";
            this.kpnlLeft.Padding = new System.Windows.Forms.Padding(12);
            this.kpnlLeft.Size = new System.Drawing.Size(760, 568);
            this.kpnlLeft.TabIndex = 0;
            //
            // tlpSettings
            //
            this.tlpSettings.AutoSize = true;
            this.tlpSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpSettings.ColumnCount = 4;
            this.tlpSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSettings.Controls.Add(this.klblPrecision, 0, 0);
            this.tlpSettings.Controls.Add(this.kcmbPrecision, 1, 0);
            this.tlpSettings.Controls.Add(this.klblMaximum, 2, 0);
            this.tlpSettings.Controls.Add(this.knudMaximum, 3, 0);
            this.tlpSettings.Controls.Add(this.klblGlyph, 0, 1);
            this.tlpSettings.Controls.Add(this.kcmbGlyph, 1, 1);
            this.tlpSettings.Controls.Add(this.klblOrientation, 2, 1);
            this.tlpSettings.Controls.Add(this.kcmbOrientation, 3, 1);
            this.tlpSettings.Controls.Add(this.klblItemSize, 0, 2);
            this.tlpSettings.Controls.Add(this.knudItemSize, 1, 2);
            this.tlpSettings.Controls.Add(this.klblTheme, 2, 2);
            this.tlpSettings.Controls.Add(this.kcmbTheme, 3, 2);
            this.tlpSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpSettings.Location = new System.Drawing.Point(12, 12);
            this.tlpSettings.Name = "tlpSettings";
            this.tlpSettings.RowCount = 3;
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSettings.Size = new System.Drawing.Size(736, 90);
            this.tlpSettings.TabIndex = 0;
            //
            // klblPrecision
            //
            this.klblPrecision.Location = new System.Drawing.Point(3, 3);
            this.klblPrecision.Name = "klblPrecision";
            this.klblPrecision.Size = new System.Drawing.Size(66, 20);
            this.klblPrecision.TabIndex = 0;
            this.klblPrecision.Values.Text = "Precision";
            //
            // kcmbPrecision
            //
            this.kcmbPrecision.AccessibleName = "Precision";
            this.kcmbPrecision.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kcmbPrecision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbPrecision.Location = new System.Drawing.Point(85, 3);
            this.kcmbPrecision.Name = "kcmbPrecision";
            this.kcmbPrecision.Size = new System.Drawing.Size(250, 21);
            this.kcmbPrecision.TabIndex = 1;
            this.kcmbPrecision.SelectedIndexChanged += new System.EventHandler(this.kcmbPrecision_SelectedIndexChanged);
            //
            // klblMaximum
            //
            this.klblMaximum.Location = new System.Drawing.Point(341, 3);
            this.klblMaximum.Name = "klblMaximum";
            this.klblMaximum.Size = new System.Drawing.Size(63, 20);
            this.klblMaximum.TabIndex = 2;
            this.klblMaximum.Values.Text = "Maximum";
            //
            // knudMaximum
            //
            this.knudMaximum.Location = new System.Drawing.Point(410, 3);
            this.knudMaximum.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
            this.knudMaximum.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.knudMaximum.Name = "knudMaximum";
            this.knudMaximum.Size = new System.Drawing.Size(80, 22);
            this.knudMaximum.TabIndex = 3;
            this.knudMaximum.Value = new decimal(new int[] { 5, 0, 0, 0 });
            this.knudMaximum.ValueChanged += new System.EventHandler(this.knudMaximum_ValueChanged);
            //
            // klblGlyph
            //
            this.klblGlyph.Location = new System.Drawing.Point(3, 33);
            this.klblGlyph.Name = "klblGlyph";
            this.klblGlyph.Size = new System.Drawing.Size(43, 20);
            this.klblGlyph.TabIndex = 4;
            this.klblGlyph.Values.Text = "Glyph";
            //
            // kcmbGlyph
            //
            this.kcmbGlyph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kcmbGlyph.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbGlyph.Location = new System.Drawing.Point(85, 33);
            this.kcmbGlyph.Name = "kcmbGlyph";
            this.kcmbGlyph.Size = new System.Drawing.Size(250, 21);
            this.kcmbGlyph.TabIndex = 5;
            this.kcmbGlyph.SelectedIndexChanged += new System.EventHandler(this.kcmbGlyph_SelectedIndexChanged);
            //
            // klblOrientation
            //
            this.klblOrientation.Location = new System.Drawing.Point(341, 33);
            this.klblOrientation.Name = "klblOrientation";
            this.klblOrientation.Size = new System.Drawing.Size(73, 20);
            this.klblOrientation.TabIndex = 6;
            this.klblOrientation.Values.Text = "Orientation";
            //
            // kcmbOrientation
            //
            this.kcmbOrientation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kcmbOrientation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbOrientation.Location = new System.Drawing.Point(410, 33);
            this.kcmbOrientation.Name = "kcmbOrientation";
            this.kcmbOrientation.Size = new System.Drawing.Size(250, 21);
            this.kcmbOrientation.TabIndex = 7;
            this.kcmbOrientation.SelectedIndexChanged += new System.EventHandler(this.kcmbOrientation_SelectedIndexChanged);
            //
            // klblItemSize
            //
            this.klblItemSize.Location = new System.Drawing.Point(3, 63);
            this.klblItemSize.Name = "klblItemSize";
            this.klblItemSize.Size = new System.Drawing.Size(61, 20);
            this.klblItemSize.TabIndex = 8;
            this.klblItemSize.Values.Text = "Item size";
            //
            // knudItemSize
            //
            this.knudItemSize.Location = new System.Drawing.Point(85, 63);
            this.knudItemSize.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            this.knudItemSize.Minimum = new decimal(new int[] { 8, 0, 0, 0 });
            this.knudItemSize.Name = "knudItemSize";
            this.knudItemSize.Size = new System.Drawing.Size(80, 22);
            this.knudItemSize.TabIndex = 9;
            this.knudItemSize.Value = new decimal(new int[] { 20, 0, 0, 0 });
            this.knudItemSize.ValueChanged += new System.EventHandler(this.knudItemSize_ValueChanged);
            //
            // klblTheme
            //
            this.klblTheme.Location = new System.Drawing.Point(341, 63);
            this.klblTheme.Name = "klblTheme";
            this.klblTheme.Size = new System.Drawing.Size(47, 20);
            this.klblTheme.TabIndex = 10;
            this.klblTheme.Values.Text = "Theme";
            //
            // kcmbTheme
            //
            this.kcmbTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kcmbTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbTheme.Location = new System.Drawing.Point(410, 63);
            this.kcmbTheme.Name = "kcmbTheme";
            this.kcmbTheme.Size = new System.Drawing.Size(250, 21);
            this.kcmbTheme.TabIndex = 11;
            //
            // flpFlags
            //
            this.flpFlags.AutoSize = true;
            this.flpFlags.Controls.Add(this.kchkReadOnly);
            this.flpFlags.Controls.Add(this.kchkAllowClear);
            this.flpFlags.Controls.Add(this.kchkEnabled);
            this.flpFlags.Controls.Add(this.kchkRtl);
            this.flpFlags.Controls.Add(this.kbtnFillGold);
            this.flpFlags.Controls.Add(this.kbtnFillBlue);
            this.flpFlags.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpFlags.Location = new System.Drawing.Point(12, 102);
            this.flpFlags.Name = "flpFlags";
            this.flpFlags.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.flpFlags.Size = new System.Drawing.Size(736, 46);
            this.flpFlags.TabIndex = 1;
            //
            // kchkReadOnly
            //
            this.kchkReadOnly.Location = new System.Drawing.Point(3, 11);
            this.kchkReadOnly.Name = "kchkReadOnly";
            this.kchkReadOnly.Size = new System.Drawing.Size(82, 20);
            this.kchkReadOnly.TabIndex = 0;
            this.kchkReadOnly.Values.Text = "ReadOnly";
            this.kchkReadOnly.CheckedChanged += new System.EventHandler(this.kchkReadOnly_CheckedChanged);
            //
            // kchkAllowClear
            //
            this.kchkAllowClear.Checked = true;
            this.kchkAllowClear.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkAllowClear.Location = new System.Drawing.Point(91, 11);
            this.kchkAllowClear.Name = "kchkAllowClear";
            this.kchkAllowClear.Size = new System.Drawing.Size(87, 20);
            this.kchkAllowClear.TabIndex = 1;
            this.kchkAllowClear.Values.Text = "AllowClear";
            this.kchkAllowClear.CheckedChanged += new System.EventHandler(this.kchkAllowClear_CheckedChanged);
            //
            // kchkEnabled
            //
            this.kchkEnabled.Checked = true;
            this.kchkEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkEnabled.Location = new System.Drawing.Point(184, 11);
            this.kchkEnabled.Name = "kchkEnabled";
            this.kchkEnabled.Size = new System.Drawing.Size(69, 20);
            this.kchkEnabled.TabIndex = 2;
            this.kchkEnabled.Values.Text = "Enabled";
            this.kchkEnabled.CheckedChanged += new System.EventHandler(this.kchkEnabled_CheckedChanged);
            //
            // kchkRtl
            //
            this.kchkRtl.Location = new System.Drawing.Point(259, 11);
            this.kchkRtl.Name = "kchkRtl";
            this.kchkRtl.Size = new System.Drawing.Size(46, 20);
            this.kchkRtl.TabIndex = 3;
            this.kchkRtl.Values.Text = "RTL";
            this.kchkRtl.CheckedChanged += new System.EventHandler(this.kchkRtl_CheckedChanged);
            //
            // kbtnFillGold
            //
            this.kbtnFillGold.Location = new System.Drawing.Point(311, 11);
            this.kbtnFillGold.Name = "kbtnFillGold";
            this.kbtnFillGold.Size = new System.Drawing.Size(110, 25);
            this.kbtnFillGold.TabIndex = 4;
            this.kbtnFillGold.Values.Text = "Reset colours";
            this.kbtnFillGold.Click += new System.EventHandler(this.kbtnFillGold_Click);
            //
            // kbtnFillBlue
            //
            this.kbtnFillBlue.Location = new System.Drawing.Point(427, 11);
            this.kbtnFillBlue.Name = "kbtnFillBlue";
            this.kbtnFillBlue.Size = new System.Drawing.Size(110, 25);
            this.kbtnFillBlue.TabIndex = 5;
            this.kbtnFillBlue.Values.Text = "Blue override";
            this.kbtnFillBlue.Click += new System.EventHandler(this.kbtnFillBlue_Click);
            //
            // klblMainCaption
            //
            this.klblMainCaption.Location = new System.Drawing.Point(15, 156);
            this.klblMainCaption.Name = "klblMainCaption";
            this.klblMainCaption.Size = new System.Drawing.Size(90, 20);
            this.klblMainCaption.TabIndex = 2;
            this.klblMainCaption.Values.Text = "Interactive";
            //
            // kratingMain
            //
            this.kratingMain.AccessibleName = "Main rating";
            this.kratingMain.Location = new System.Drawing.Point(15, 182);
            this.kratingMain.Name = "kratingMain";
            this.kratingMain.Size = new System.Drawing.Size(124, 24);
            this.kratingMain.TabIndex = 3;
            this.kratingMain.Value = new decimal(new int[] { 3, 0, 0, 0 });
            this.kratingMain.ValueChanged += new System.EventHandler(this.kratingMain_ValueChanged);
            this.kratingMain.MouseLeave += new System.EventHandler(this.kratingMain_MouseLeave);
            this.kratingMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.kratingMain_MouseMove);
            //
            // klblValue
            //
            this.klblValue.Location = new System.Drawing.Point(15, 220);
            this.klblValue.Name = "klblValue";
            this.klblValue.Size = new System.Drawing.Size(90, 20);
            this.klblValue.TabIndex = 4;
            this.klblValue.Values.Text = "Value: 3 / 5";
            //
            // klblHover
            //
            this.klblHover.Location = new System.Drawing.Point(160, 220);
            this.klblHover.Name = "klblHover";
            this.klblHover.Size = new System.Drawing.Size(90, 20);
            this.klblHover.TabIndex = 5;
            this.klblHover.Values.Text = "Hover: (none)";
            //
            // klblStatus
            //
            this.klblStatus.Location = new System.Drawing.Point(15, 244);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(700, 20);
            this.klblStatus.TabIndex = 6;
            this.klblStatus.Values.Text = "Status";
            //
            // tlpCompare
            //
            this.tlpCompare.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpCompare.ColumnCount = 4;
            this.tlpCompare.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpCompare.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpCompare.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpCompare.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpCompare.Controls.Add(this.klblDisabled, 0, 0);
            this.tlpCompare.Controls.Add(this.klblImage, 1, 0);
            this.tlpCompare.Controls.Add(this.klblHeart, 2, 0);
            this.tlpCompare.Controls.Add(this.klblVertical, 3, 0);
            this.tlpCompare.Controls.Add(this.kratingDisabled, 0, 1);
            this.tlpCompare.Controls.Add(this.kratingImage, 1, 1);
            this.tlpCompare.Controls.Add(this.kratingHeart, 2, 1);
            this.tlpCompare.Controls.Add(this.kratingVertical, 3, 1);
            this.tlpCompare.Location = new System.Drawing.Point(15, 280);
            this.tlpCompare.Name = "tlpCompare";
            this.tlpCompare.RowCount = 2;
            this.tlpCompare.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpCompare.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCompare.Size = new System.Drawing.Size(720, 260);
            this.tlpCompare.TabIndex = 7;
            //
            // klblDisabled
            //
            this.klblDisabled.Location = new System.Drawing.Point(3, 3);
            this.klblDisabled.Name = "klblDisabled";
            this.klblDisabled.Size = new System.Drawing.Size(60, 20);
            this.klblDisabled.TabIndex = 0;
            this.klblDisabled.Values.Text = "Disabled";
            //
            // kratingDisabled
            //
            this.kratingDisabled.Enabled = false;
            this.kratingDisabled.Location = new System.Drawing.Point(3, 29);
            this.kratingDisabled.Name = "kratingDisabled";
            this.kratingDisabled.Size = new System.Drawing.Size(124, 24);
            this.kratingDisabled.TabIndex = 1;
            this.kratingDisabled.Value = new decimal(new int[] { 3, 0, 0, 0 });
            //
            // klblImage
            //
            this.klblImage.Location = new System.Drawing.Point(183, 3);
            this.klblImage.Name = "klblImage";
            this.klblImage.Size = new System.Drawing.Size(90, 20);
            this.klblImage.TabIndex = 2;
            this.klblImage.Values.Text = "Image (stock)";
            //
            // kratingImage
            //
            this.kratingImage.Location = new System.Drawing.Point(183, 29);
            this.kratingImage.Name = "kratingImage";
            this.kratingImage.Size = new System.Drawing.Size(124, 24);
            this.kratingImage.TabIndex = 3;
            this.kratingImage.Value = new decimal(new int[] { 4, 0, 0, 0 });
            //
            // klblHeart
            //
            this.klblHeart.Location = new System.Drawing.Point(363, 3);
            this.klblHeart.Name = "klblHeart";
            this.klblHeart.Size = new System.Drawing.Size(90, 20);
            this.klblHeart.TabIndex = 4;
            this.klblHeart.Values.Text = "Heart (half)";
            //
            // kratingHeart
            //
            this.kratingHeart.Location = new System.Drawing.Point(363, 29);
            this.kratingHeart.Name = "kratingHeart";
            this.kratingHeart.Size = new System.Drawing.Size(124, 24);
            this.kratingHeart.TabIndex = 5;
            this.kratingHeart.Value = new decimal(new int[] { 25, 0, 0, 65536 });
            //
            // klblVertical
            //
            this.klblVertical.Location = new System.Drawing.Point(543, 3);
            this.klblVertical.Name = "klblVertical";
            this.klblVertical.Size = new System.Drawing.Size(56, 20);
            this.klblVertical.TabIndex = 6;
            this.klblVertical.Values.Text = "Vertical";
            //
            // kratingVertical
            //
            this.kratingVertical.Location = new System.Drawing.Point(543, 29);
            this.kratingVertical.Name = "kratingVertical";
            this.kratingVertical.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.kratingVertical.Size = new System.Drawing.Size(24, 124);
            this.kratingVertical.TabIndex = 7;
            this.kratingVertical.Value = new decimal(new int[] { 3, 0, 0, 0 });
            //
            // kpnlRight
            //
            this.kpnlRight.Controls.Add(this.kpgMain);
            this.kpnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlRight.Location = new System.Drawing.Point(0, 0);
            this.kpnlRight.Name = "kpnlRight";
            this.kpnlRight.Padding = new System.Windows.Forms.Padding(8);
            this.kpnlRight.Size = new System.Drawing.Size(315, 568);
            this.kpnlRight.TabIndex = 0;
            //
            // kpgMain
            //
            this.kpgMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpgMain.Location = new System.Drawing.Point(8, 8);
            this.kpgMain.Name = "kpgMain";
            this.kpgMain.Size = new System.Drawing.Size(299, 552);
            this.kpgMain.TabIndex = 0;
            //
            // KryptonRatingDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 640);
            this.Controls.Add(this.kscMain);
            this.Controls.Add(this.kwlHeader);
            this.Name = "KryptonRatingDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KryptonRating (#3928)";
            this.Load += new System.EventHandler(this.KryptonRatingDemo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kscMain.Panel1)).EndInit();
            this.kscMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kscMain.Panel2)).EndInit();
            this.kscMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kscMain)).EndInit();
            this.kscMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlLeft)).EndInit();
            this.kpnlLeft.ResumeLayout(false);
            this.kpnlLeft.PerformLayout();
            this.tlpSettings.ResumeLayout(false);
            this.tlpSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbPrecision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbGlyph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbOrientation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).EndInit();
            this.flpFlags.ResumeLayout(false);
            this.flpFlags.PerformLayout();
            this.tlpCompare.ResumeLayout(false);
            this.tlpCompare.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlRight)).EndInit();
            this.kpnlRight.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonWrapLabel kwlHeader;
        private Krypton.Toolkit.KryptonSplitContainer kscMain;
        private Krypton.Toolkit.KryptonPanel kpnlLeft;
        private System.Windows.Forms.TableLayoutPanel tlpSettings;
        private Krypton.Toolkit.KryptonLabel klblPrecision;
        private Krypton.Toolkit.KryptonComboBox kcmbPrecision;
        private Krypton.Toolkit.KryptonLabel klblMaximum;
        private Krypton.Toolkit.KryptonNumericUpDown knudMaximum;
        private Krypton.Toolkit.KryptonLabel klblGlyph;
        private Krypton.Toolkit.KryptonComboBox kcmbGlyph;
        private Krypton.Toolkit.KryptonLabel klblOrientation;
        private Krypton.Toolkit.KryptonComboBox kcmbOrientation;
        private Krypton.Toolkit.KryptonLabel klblItemSize;
        private Krypton.Toolkit.KryptonNumericUpDown knudItemSize;
        private Krypton.Toolkit.KryptonLabel klblTheme;
        private Krypton.Toolkit.KryptonThemeComboBox kcmbTheme;
        private System.Windows.Forms.FlowLayoutPanel flpFlags;
        private Krypton.Toolkit.KryptonCheckBox kchkReadOnly;
        private Krypton.Toolkit.KryptonCheckBox kchkAllowClear;
        private Krypton.Toolkit.KryptonCheckBox kchkEnabled;
        private Krypton.Toolkit.KryptonCheckBox kchkRtl;
        private Krypton.Toolkit.KryptonButton kbtnFillGold;
        private Krypton.Toolkit.KryptonButton kbtnFillBlue;
        private Krypton.Toolkit.KryptonLabel klblMainCaption;
        private Krypton.Toolkit.KryptonRating kratingMain;
        private Krypton.Toolkit.KryptonLabel klblValue;
        private Krypton.Toolkit.KryptonLabel klblHover;
        private Krypton.Toolkit.KryptonLabel klblStatus;
        private System.Windows.Forms.TableLayoutPanel tlpCompare;
        private Krypton.Toolkit.KryptonLabel klblDisabled;
        private Krypton.Toolkit.KryptonRating kratingDisabled;
        private Krypton.Toolkit.KryptonLabel klblImage;
        private Krypton.Toolkit.KryptonRating kratingImage;
        private Krypton.Toolkit.KryptonLabel klblHeart;
        private Krypton.Toolkit.KryptonRating kratingHeart;
        private Krypton.Toolkit.KryptonLabel klblVertical;
        private Krypton.Toolkit.KryptonRating kratingVertical;
        private Krypton.Toolkit.KryptonPanel kpnlRight;
        private Krypton.Toolkit.KryptonPropertyGrid kpgMain;
    }
}

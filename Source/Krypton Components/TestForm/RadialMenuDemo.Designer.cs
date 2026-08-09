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
            components?.Dispose();
            _radialMenu.Dispose();
            _importedMenu.Dispose();
            _sourceContextMenu.Dispose();
            _cutCommand.Dispose();
            _copyCommand.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
        this.ktxtLog = new Krypton.Toolkit.KryptonTextBox();
        this.kpnlSurface = new Krypton.Toolkit.KryptonPanel();
        this.kwlblHint = new Krypton.Toolkit.KryptonWrapLabel();
        this.kpnlToolbar = new Krypton.Toolkit.KryptonPanel();
        this.kbtnShowAtCursor = new Krypton.Toolkit.KryptonButton();
        this.kchkPreferRadial = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkShowCheckedGlyph = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkShowShadow = new Krypton.Toolkit.KryptonCheckBox();
        this.kcmbImageSize = new Krypton.Toolkit.KryptonComboBox();
        this.kwlblImageSize = new Krypton.Toolkit.KryptonWrapLabel();
        this.kcmbDisplayStyle = new Krypton.Toolkit.KryptonComboBox();
        this.kwlblDisplayStyle = new Krypton.Toolkit.KryptonWrapLabel();
        this.kchkAllowMove = new Krypton.Toolkit.KryptonCheckBox();
        this.kcmbAnimation = new Krypton.Toolkit.KryptonComboBox();
        this.kwlblAnimation = new Krypton.Toolkit.KryptonWrapLabel();
        this.krdoImported = new Krypton.Toolkit.KryptonRadioButton();
        this.krdoNative = new Krypton.Toolkit.KryptonRadioButton();
        this.kwlblMode = new Krypton.Toolkit.KryptonWrapLabel();
        ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
        this.kpnlMain.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kpnlSurface)).BeginInit();
        this.kpnlSurface.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kpnlToolbar)).BeginInit();
        this.kpnlToolbar.SuspendLayout();
        this.SuspendLayout();
        //
        // kpnlMain
        //
        this.kpnlMain.Controls.Add(this.ktxtLog);
        this.kpnlMain.Controls.Add(this.kpnlSurface);
        this.kpnlMain.Controls.Add(this.kpnlToolbar);
        this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kpnlMain.Location = new System.Drawing.Point(0, 0);
        this.kpnlMain.Name = "kpnlMain";
        this.kpnlMain.Padding = new System.Windows.Forms.Padding(12);
        this.kpnlMain.Size = new System.Drawing.Size(920, 560);
        this.kpnlMain.TabIndex = 0;
        //
        // kpnlToolbar
        //
        this.kpnlToolbar.Controls.Add(this.kbtnShowAtCursor);
        this.kpnlToolbar.Controls.Add(this.kchkPreferRadial);
        this.kpnlToolbar.Controls.Add(this.kchkShowCheckedGlyph);
        this.kpnlToolbar.Controls.Add(this.kchkShowShadow);
        this.kpnlToolbar.Controls.Add(this.kcmbImageSize);
        this.kpnlToolbar.Controls.Add(this.kwlblImageSize);
        this.kpnlToolbar.Controls.Add(this.kcmbDisplayStyle);
        this.kpnlToolbar.Controls.Add(this.kwlblDisplayStyle);
        this.kpnlToolbar.Controls.Add(this.kcmbAnimation);
        this.kpnlToolbar.Controls.Add(this.kwlblAnimation);
        this.kpnlToolbar.Controls.Add(this.kchkAllowMove);
        this.kpnlToolbar.Controls.Add(this.krdoImported);
        this.kpnlToolbar.Controls.Add(this.krdoNative);
        this.kpnlToolbar.Controls.Add(this.kwlblMode);
        this.kpnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
        this.kpnlToolbar.Location = new System.Drawing.Point(12, 12);
        this.kpnlToolbar.Name = "kpnlToolbar";
        this.kpnlToolbar.Size = new System.Drawing.Size(896, 108);
        this.kpnlToolbar.TabIndex = 0;
        //
        // kwlblMode
        //
        this.kwlblMode.Location = new System.Drawing.Point(8, 14);
        this.kwlblMode.Name = "kwlblMode";
        this.kwlblMode.Size = new System.Drawing.Size(40, 20);
        this.kwlblMode.Text = "Mode:";
        //
        // krdoNative
        //
        this.krdoNative.Checked = true;
        this.krdoNative.Location = new System.Drawing.Point(60, 12);
        this.krdoNative.Name = "krdoNative";
        this.krdoNative.Size = new System.Drawing.Size(160, 24);
        this.krdoNative.TabIndex = 1;
        this.krdoNative.Values.Text = "Native radial items";
        //
        // krdoImported
        //
        this.krdoImported.Location = new System.Drawing.Point(230, 12);
        this.krdoImported.Name = "krdoImported";
        this.krdoImported.Size = new System.Drawing.Size(220, 24);
        this.krdoImported.TabIndex = 2;
        this.krdoImported.Values.Text = "Imported from KryptonContextMenu";
        //
        // kchkAllowMove
        //
        this.kchkAllowMove.Location = new System.Drawing.Point(460, 12);
        this.kchkAllowMove.Name = "kchkAllowMove";
        this.kchkAllowMove.Size = new System.Drawing.Size(140, 24);
        this.kchkAllowMove.TabIndex = 3;
        this.kchkAllowMove.Values.Text = "Allow move (drag centre)";
        this.kchkAllowMove.CheckedChanged += new System.EventHandler(this.kchkAllowMove_CheckedChanged);
        //
        // kwlblAnimation
        //
        this.kwlblAnimation.Location = new System.Drawing.Point(8, 48);
        this.kwlblAnimation.Name = "kwlblAnimation";
        this.kwlblAnimation.Size = new System.Drawing.Size(64, 20);
        this.kwlblAnimation.Text = "Animation:";
        //
        // kcmbAnimation
        //
        this.kcmbAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbAnimation.Location = new System.Drawing.Point(78, 44);
        this.kcmbAnimation.Name = "kcmbAnimation";
        this.kcmbAnimation.Size = new System.Drawing.Size(120, 25);
        this.kcmbAnimation.TabIndex = 5;
        this.kcmbAnimation.SelectedIndexChanged += new System.EventHandler(this.kcmbAnimation_SelectedIndexChanged);
        //
        // kwlblDisplayStyle
        //
        this.kwlblDisplayStyle.Location = new System.Drawing.Point(210, 48);
        this.kwlblDisplayStyle.Name = "kwlblDisplayStyle";
        this.kwlblDisplayStyle.Size = new System.Drawing.Size(40, 20);
        this.kwlblDisplayStyle.Text = "Style:";
        //
        // kcmbDisplayStyle
        //
        this.kcmbDisplayStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbDisplayStyle.Location = new System.Drawing.Point(254, 44);
        this.kcmbDisplayStyle.Name = "kcmbDisplayStyle";
        this.kcmbDisplayStyle.Size = new System.Drawing.Size(140, 25);
        this.kcmbDisplayStyle.TabIndex = 6;
        this.kcmbDisplayStyle.SelectedIndexChanged += new System.EventHandler(this.kcmbDisplayStyle_SelectedIndexChanged);
        //
        // kwlblImageSize
        //
        this.kwlblImageSize.Location = new System.Drawing.Point(408, 48);
        this.kwlblImageSize.Name = "kwlblImageSize";
        this.kwlblImageSize.Size = new System.Drawing.Size(36, 20);
        this.kwlblImageSize.Text = "Size:";
        //
        // kcmbImageSize
        //
        this.kcmbImageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbImageSize.Location = new System.Drawing.Point(448, 44);
        this.kcmbImageSize.Name = "kcmbImageSize";
        this.kcmbImageSize.Size = new System.Drawing.Size(64, 25);
        this.kcmbImageSize.TabIndex = 7;
        this.kcmbImageSize.SelectedIndexChanged += new System.EventHandler(this.kcmbImageSize_SelectedIndexChanged);
        //
        // kchkShowShadow
        //
        this.kchkShowShadow.Location = new System.Drawing.Point(528, 44);
        this.kchkShowShadow.Name = "kchkShowShadow";
        this.kchkShowShadow.Size = new System.Drawing.Size(100, 24);
        this.kchkShowShadow.TabIndex = 8;
        this.kchkShowShadow.Values.Text = "Show shadow";
        this.kchkShowShadow.CheckedChanged += new System.EventHandler(this.kchkShowShadow_CheckedChanged);
        //
        // kchkShowCheckedGlyph
        //
        this.kchkShowCheckedGlyph.Location = new System.Drawing.Point(640, 44);
        this.kchkShowCheckedGlyph.Name = "kchkShowCheckedGlyph";
        this.kchkShowCheckedGlyph.Size = new System.Drawing.Size(120, 24);
        this.kchkShowCheckedGlyph.TabIndex = 9;
        this.kchkShowCheckedGlyph.Values.Text = "Checked glyph";
        this.kchkShowCheckedGlyph.CheckedChanged += new System.EventHandler(this.kchkShowCheckedGlyph_CheckedChanged);
        //
        // kchkPreferRadial
        //
        this.kchkPreferRadial.Location = new System.Drawing.Point(8, 78);
        this.kchkPreferRadial.Name = "kchkPreferRadial";
        this.kchkPreferRadial.Size = new System.Drawing.Size(360, 24);
        this.kchkPreferRadial.TabIndex = 10;
        this.kchkPreferRadial.Values.Text = "PreferRadialContextMenus (imported mode via Presenter)";
        this.kchkPreferRadial.CheckedChanged += new System.EventHandler(this.kchkPreferRadial_CheckedChanged);
        //
        // kbtnShowAtCursor
        //
        this.kbtnShowAtCursor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.kbtnShowAtCursor.Location = new System.Drawing.Point(756, 10);
        this.kbtnShowAtCursor.Name = "kbtnShowAtCursor";
        this.kbtnShowAtCursor.Size = new System.Drawing.Size(128, 28);
        this.kbtnShowAtCursor.TabIndex = 4;
        this.kbtnShowAtCursor.Values.Text = "Show at cursor";
        this.kbtnShowAtCursor.Click += new System.EventHandler(this.kbtnShowAtCursor_Click);
        //
        // kpnlSurface
        //
        this.kpnlSurface.Controls.Add(this.kwlblHint);
        this.kpnlSurface.Dock = System.Windows.Forms.DockStyle.Top;
        this.kpnlSurface.Location = new System.Drawing.Point(12, 120);
        this.kpnlSurface.Name = "kpnlSurface";
        this.kpnlSurface.Padding = new System.Windows.Forms.Padding(16);
        this.kpnlSurface.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.ControlClient;
        this.kpnlSurface.Size = new System.Drawing.Size(896, 180);
        this.kpnlSurface.TabIndex = 1;
        this.kpnlSurface.MouseUp += new System.Windows.Forms.MouseEventHandler(this.kpnlSurface_MouseUp);
        //
        // kwlblHint
        //
        this.kwlblHint.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kwlblHint.LabelStyle = Krypton.Toolkit.LabelStyle.NormalPanel;
        this.kwlblHint.Location = new System.Drawing.Point(16, 16);
        this.kwlblHint.Name = "kwlblHint";
        this.kwlblHint.Size = new System.Drawing.Size(864, 148);
        this.kwlblHint.Text = "Issue #4172 — KryptonRadialMenu\r\n\r\nRight-click this surface (or use Show at cursor).\r\n• Native / imported modes, animation, DisplayStyle, ItemImageSize, shadow, checked glyph.\r\n• Keys: arrows focus sectors, Enter/Space activate, Esc/Back backs out, Esc at root dismisses.\r\n• Imported mode + PreferRadial uses KryptonRadialMenuPresenter.";
        //
        // ktxtLog
        //
        this.ktxtLog.Dock = System.Windows.Forms.DockStyle.Fill;
        this.ktxtLog.Location = new System.Drawing.Point(12, 300);
        this.ktxtLog.Multiline = true;
        this.ktxtLog.Name = "ktxtLog";
        this.ktxtLog.ReadOnly = true;
        this.ktxtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.ktxtLog.Size = new System.Drawing.Size(896, 248);
        this.ktxtLog.TabIndex = 2;
        //
        // RadialMenuDemo
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(920, 560);
        this.Controls.Add(this.kpnlMain);
        this.Name = "RadialMenuDemo";
        this.Text = "Radial Menu Demo (#4172)";
        ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
        this.kpnlMain.ResumeLayout(false);
        this.kpnlMain.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kpnlSurface)).EndInit();
        this.kpnlSurface.ResumeLayout(false);
        this.kpnlSurface.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kpnlToolbar)).EndInit();
        this.kpnlToolbar.ResumeLayout(false);
        this.kpnlToolbar.PerformLayout();
        this.ResumeLayout(false);
    }

    #endregion

    private Krypton.Toolkit.KryptonPanel kpnlMain;
    private Krypton.Toolkit.KryptonTextBox ktxtLog;
    private Krypton.Toolkit.KryptonPanel kpnlSurface;
    private Krypton.Toolkit.KryptonWrapLabel kwlblHint;
    private Krypton.Toolkit.KryptonPanel kpnlToolbar;
    private Krypton.Toolkit.KryptonButton kbtnShowAtCursor;
    private Krypton.Toolkit.KryptonCheckBox kchkPreferRadial;
    private Krypton.Toolkit.KryptonCheckBox kchkShowCheckedGlyph;
    private Krypton.Toolkit.KryptonCheckBox kchkShowShadow;
    private Krypton.Toolkit.KryptonComboBox kcmbImageSize;
    private Krypton.Toolkit.KryptonWrapLabel kwlblImageSize;
    private Krypton.Toolkit.KryptonComboBox kcmbDisplayStyle;
    private Krypton.Toolkit.KryptonWrapLabel kwlblDisplayStyle;
    private Krypton.Toolkit.KryptonCheckBox kchkAllowMove;
    private Krypton.Toolkit.KryptonComboBox kcmbAnimation;
    private Krypton.Toolkit.KryptonWrapLabel kwlblAnimation;
    private Krypton.Toolkit.KryptonRadioButton krdoImported;
    private Krypton.Toolkit.KryptonRadioButton krdoNative;
    private Krypton.Toolkit.KryptonWrapLabel kwlblMode;
}

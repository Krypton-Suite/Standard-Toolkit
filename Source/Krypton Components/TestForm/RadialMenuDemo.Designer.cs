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
        this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
        this.ktxtLog = new Krypton.Toolkit.KryptonTextBox();
        this.kpnlContent = new Krypton.Toolkit.KryptonPanel();
        this.kpnlSurface = new Krypton.Toolkit.KryptonPanel();
        this.kwlblHint = new Krypton.Toolkit.KryptonWrapLabel();
        this.kpnlHosted = new Krypton.Toolkit.KryptonPanel();
        this.kbtnDockHosted = new Krypton.Toolkit.KryptonButton();
        this.kwlblHosted = new Krypton.Toolkit.KryptonWrapLabel();
        this.kpnlToolbar = new Krypton.Toolkit.KryptonPanel();
        this.kbtnShowAtCursor = new Krypton.Toolkit.KryptonButton();
        this.kchkPreferRadial = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkUseHub = new Krypton.Toolkit.KryptonCheckBox();
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
        ((System.ComponentModel.ISupportInitialize)(this.kpnlContent)).BeginInit();
        this.kpnlContent.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kpnlSurface)).BeginInit();
        this.kpnlSurface.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kpnlHosted)).BeginInit();
        this.kpnlHosted.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kpnlToolbar)).BeginInit();
        this.kpnlToolbar.SuspendLayout();
        this.SuspendLayout();
        //
        // kpnlMain
        //
        this.kpnlMain.Controls.Add(this.kpnlContent);
        this.kpnlMain.Controls.Add(this.ktxtLog);
        this.kpnlMain.Controls.Add(this.kpnlToolbar);
        this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kpnlMain.Location = new System.Drawing.Point(0, 0);
        this.kpnlMain.Name = "kpnlMain";
        this.kpnlMain.Padding = new System.Windows.Forms.Padding(12);
        this.kpnlMain.Size = new System.Drawing.Size(1000, 680);
        this.kpnlMain.TabIndex = 0;
        //
        // kpnlToolbar
        //
        this.kpnlToolbar.Controls.Add(this.kbtnShowAtCursor);
        this.kpnlToolbar.Controls.Add(this.kchkPreferRadial);
        this.kpnlToolbar.Controls.Add(this.kchkUseHub);
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
        this.kpnlToolbar.Size = new System.Drawing.Size(976, 96);
        this.kpnlToolbar.TabIndex = 0;
        //
        // kwlblMode
        //
        this.kwlblMode.Location = new System.Drawing.Point(8, 12);
        this.kwlblMode.Name = "kwlblMode";
        this.kwlblMode.Size = new System.Drawing.Size(40, 20);
        this.kwlblMode.Text = "Mode:";
        //
        // krdoNative
        //
        this.krdoNative.Checked = true;
        this.krdoNative.Location = new System.Drawing.Point(56, 10);
        this.krdoNative.Name = "krdoNative";
        this.krdoNative.Size = new System.Drawing.Size(150, 24);
        this.krdoNative.TabIndex = 1;
        this.krdoNative.Values.Text = "Native radial items";
        //
        // krdoImported
        //
        this.krdoImported.Location = new System.Drawing.Point(214, 10);
        this.krdoImported.Name = "krdoImported";
        this.krdoImported.Size = new System.Drawing.Size(210, 24);
        this.krdoImported.TabIndex = 2;
        this.krdoImported.Values.Text = "Imported from ContextMenu";
        //
        // kchkAllowMove
        //
        this.kchkAllowMove.Location = new System.Drawing.Point(436, 10);
        this.kchkAllowMove.Name = "kchkAllowMove";
        this.kchkAllowMove.Size = new System.Drawing.Size(100, 24);
        this.kchkAllowMove.TabIndex = 3;
        this.kchkAllowMove.Values.Text = "Allow move";
        this.kchkAllowMove.CheckedChanged += new System.EventHandler(this.kchkAllowMove_CheckedChanged);
        //
        // kbtnShowAtCursor
        //
        this.kbtnShowAtCursor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.kbtnShowAtCursor.Location = new System.Drawing.Point(832, 8);
        this.kbtnShowAtCursor.Name = "kbtnShowAtCursor";
        this.kbtnShowAtCursor.Size = new System.Drawing.Size(132, 28);
        this.kbtnShowAtCursor.TabIndex = 4;
        this.kbtnShowAtCursor.Values.Text = "Show popup";
        this.kbtnShowAtCursor.Click += new System.EventHandler(this.kbtnShowAtCursor_Click);
        //
        // kwlblAnimation
        //
        this.kwlblAnimation.Location = new System.Drawing.Point(8, 44);
        this.kwlblAnimation.Name = "kwlblAnimation";
        this.kwlblAnimation.Size = new System.Drawing.Size(64, 20);
        this.kwlblAnimation.Text = "Animation:";
        //
        // kcmbAnimation
        //
        this.kcmbAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbAnimation.Location = new System.Drawing.Point(78, 40);
        this.kcmbAnimation.Name = "kcmbAnimation";
        this.kcmbAnimation.Size = new System.Drawing.Size(110, 25);
        this.kcmbAnimation.TabIndex = 5;
        this.kcmbAnimation.SelectedIndexChanged += new System.EventHandler(this.kcmbAnimation_SelectedIndexChanged);
        //
        // kwlblDisplayStyle
        //
        this.kwlblDisplayStyle.Location = new System.Drawing.Point(200, 44);
        this.kwlblDisplayStyle.Name = "kwlblDisplayStyle";
        this.kwlblDisplayStyle.Size = new System.Drawing.Size(40, 20);
        this.kwlblDisplayStyle.Text = "Style:";
        //
        // kcmbDisplayStyle
        //
        this.kcmbDisplayStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbDisplayStyle.Location = new System.Drawing.Point(244, 40);
        this.kcmbDisplayStyle.Name = "kcmbDisplayStyle";
        this.kcmbDisplayStyle.Size = new System.Drawing.Size(140, 25);
        this.kcmbDisplayStyle.TabIndex = 6;
        this.kcmbDisplayStyle.SelectedIndexChanged += new System.EventHandler(this.kcmbDisplayStyle_SelectedIndexChanged);
        //
        // kwlblImageSize
        //
        this.kwlblImageSize.Location = new System.Drawing.Point(396, 44);
        this.kwlblImageSize.Name = "kwlblImageSize";
        this.kwlblImageSize.Size = new System.Drawing.Size(36, 20);
        this.kwlblImageSize.Text = "Size:";
        //
        // kcmbImageSize
        //
        this.kcmbImageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbImageSize.Location = new System.Drawing.Point(436, 40);
        this.kcmbImageSize.Name = "kcmbImageSize";
        this.kcmbImageSize.Size = new System.Drawing.Size(64, 25);
        this.kcmbImageSize.TabIndex = 7;
        this.kcmbImageSize.SelectedIndexChanged += new System.EventHandler(this.kcmbImageSize_SelectedIndexChanged);
        //
        // kchkShowShadow
        //
        this.kchkShowShadow.Location = new System.Drawing.Point(516, 40);
        this.kchkShowShadow.Name = "kchkShowShadow";
        this.kchkShowShadow.Size = new System.Drawing.Size(110, 24);
        this.kchkShowShadow.TabIndex = 8;
        this.kchkShowShadow.Values.Text = "Show shadow";
        this.kchkShowShadow.CheckedChanged += new System.EventHandler(this.kchkShowShadow_CheckedChanged);
        //
        // kchkShowCheckedGlyph
        //
        this.kchkShowCheckedGlyph.Location = new System.Drawing.Point(636, 40);
        this.kchkShowCheckedGlyph.Name = "kchkShowCheckedGlyph";
        this.kchkShowCheckedGlyph.Size = new System.Drawing.Size(120, 24);
        this.kchkShowCheckedGlyph.TabIndex = 9;
        this.kchkShowCheckedGlyph.Values.Text = "Checked glyph";
        this.kchkShowCheckedGlyph.CheckedChanged += new System.EventHandler(this.kchkShowCheckedGlyph_CheckedChanged);
        //
        // kchkPreferRadial
        //
        this.kchkPreferRadial.Location = new System.Drawing.Point(8, 70);
        this.kchkPreferRadial.Name = "kchkPreferRadial";
        this.kchkPreferRadial.Size = new System.Drawing.Size(340, 24);
        this.kchkPreferRadial.TabIndex = 10;
        this.kchkPreferRadial.Values.Text = "PreferRadialContextMenus (imported + Presenter)";
        this.kchkPreferRadial.CheckedChanged += new System.EventHandler(this.kchkPreferRadial_CheckedChanged);
        //
        // kchkUseHub
        //
        this.kchkUseHub.Checked = true;
        this.kchkUseHub.Location = new System.Drawing.Point(356, 70);
        this.kchkUseHub.Name = "kchkUseHub";
        this.kchkUseHub.Size = new System.Drawing.Size(160, 24);
        this.kchkUseHub.TabIndex = 11;
        this.kchkUseHub.Values.Text = "Hosted UseHub";
        this.kchkUseHub.CheckedChanged += new System.EventHandler(this.kchkUseHub_CheckedChanged);
        //
        // ktxtLog
        //
        this.ktxtLog.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.ktxtLog.Location = new System.Drawing.Point(12, 508);
        this.ktxtLog.Multiline = true;
        this.ktxtLog.Name = "ktxtLog";
        this.ktxtLog.ReadOnly = true;
        this.ktxtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.ktxtLog.Size = new System.Drawing.Size(976, 160);
        this.ktxtLog.TabIndex = 2;
        //
        // kpnlContent
        //
        this.kpnlContent.Controls.Add(this.kpnlSurface);
        this.kpnlContent.Controls.Add(this.kpnlHosted);
        this.kpnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kpnlContent.Location = new System.Drawing.Point(12, 108);
        this.kpnlContent.Name = "kpnlContent";
        this.kpnlContent.Size = new System.Drawing.Size(976, 400);
        this.kpnlContent.TabIndex = 1;
        //
        // kpnlHosted
        //
        this.kpnlHosted.Controls.Add(this.kbtnDockHosted);
        this.kpnlHosted.Controls.Add(this.kwlblHosted);
        this.kpnlHosted.Dock = System.Windows.Forms.DockStyle.Right;
        this.kpnlHosted.Location = new System.Drawing.Point(676, 0);
        this.kpnlHosted.Name = "kpnlHosted";
        this.kpnlHosted.Padding = new System.Windows.Forms.Padding(8);
        this.kpnlHosted.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
        this.kpnlHosted.Size = new System.Drawing.Size(300, 400);
        this.kpnlHosted.TabIndex = 1;
        this.kpnlHosted.Resize += new System.EventHandler(this.kpnlHosted_Resize);
        //
        // kwlblHosted
        //
        this.kwlblHosted.Dock = System.Windows.Forms.DockStyle.Top;
        this.kwlblHosted.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
        this.kwlblHosted.Location = new System.Drawing.Point(8, 8);
        this.kwlblHosted.Name = "kwlblHosted";
        this.kwlblHosted.Size = new System.Drawing.Size(284, 40);
        this.kwlblHosted.Text = "Hosted control\r\nPress hub · Allow move to drag / float";
        //
        // kbtnDockHosted
        //
        this.kbtnDockHosted.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.kbtnDockHosted.Enabled = false;
        this.kbtnDockHosted.Location = new System.Drawing.Point(8, 360);
        this.kbtnDockHosted.Name = "kbtnDockHosted";
        this.kbtnDockHosted.Size = new System.Drawing.Size(284, 32);
        this.kbtnDockHosted.TabIndex = 1;
        this.kbtnDockHosted.Values.Text = "Dock hosted control back";
        this.kbtnDockHosted.Click += new System.EventHandler(this.kbtnDockHosted_Click);
        //
        // kpnlSurface
        //
        this.kpnlSurface.Controls.Add(this.kwlblHint);
        this.kpnlSurface.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kpnlSurface.Location = new System.Drawing.Point(0, 0);
        this.kpnlSurface.Name = "kpnlSurface";
        this.kpnlSurface.Padding = new System.Windows.Forms.Padding(16);
        this.kpnlSurface.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.ControlClient;
        this.kpnlSurface.Size = new System.Drawing.Size(676, 400);
        this.kpnlSurface.TabIndex = 0;
        this.kpnlSurface.MouseUp += new System.Windows.Forms.MouseEventHandler(this.kpnlSurface_MouseUp);
        //
        // kwlblHint
        //
        this.kwlblHint.Dock = System.Windows.Forms.DockStyle.Top;
        this.kwlblHint.LabelStyle = Krypton.Toolkit.LabelStyle.NormalPanel;
        this.kwlblHint.Location = new System.Drawing.Point(16, 16);
        this.kwlblHint.Name = "kwlblHint";
        this.kwlblHint.Size = new System.Drawing.Size(644, 120);
        this.kwlblHint.Text = "Issue #4172 — popup Component\r\n\r\nRight-click the empty area below (or Show popup) to open KryptonRadialMenu.\r\n• Outer ring opens children / editors; leaf body click activates.\r\n• MaxVisibleItems = 6 pages overflow. Keys: arrows + Enter.\r\n• Imported mode + PreferRadial uses Presenter / AlternativeShow.";
        this.kwlblHint.MouseUp += new System.Windows.Forms.MouseEventHandler(this.kpnlSurface_MouseUp);
        //
        // RadialMenuDemo
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1000, 680);
        this.Controls.Add(this.kpnlMain);
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

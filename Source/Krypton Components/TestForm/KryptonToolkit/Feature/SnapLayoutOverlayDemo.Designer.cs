#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

partial class SnapLayoutOverlayDemo
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

    private void InitializeComponent()
    {
        this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
        this.klblInstructions = new Krypton.Toolkit.KryptonLabel();
        this.klblMode = new Krypton.Toolkit.KryptonLabel();
        this.kcmbMode = new Krypton.Toolkit.KryptonComboBox();
        this.klblGlobal = new Krypton.Toolkit.KryptonLabel();
        this.kcmbGlobal = new Krypton.Toolkit.KryptonComboBox();
        this.kchkShowIcon = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkShowPreview = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkShowPreviewGhost = new Krypton.Toolkit.KryptonCheckBox();
        this.klblPreviewOpacity = new Krypton.Toolkit.KryptonLabel();
        this.knudPreviewOpacity = new Krypton.Toolkit.KryptonNumericUpDown();
        this.klblTheme = new Krypton.Toolkit.KryptonLabel();
        this.kcmbTheme = new Krypton.Toolkit.KryptonComboBox();
        this.kchkIncludeBuiltin = new Krypton.Toolkit.KryptonCheckBox();
        this.kbtnAddCustom = new Krypton.Toolkit.KryptonButton();
        this.kbtnClearCustom = new Krypton.Toolkit.KryptonButton();
        this.kbtnSaveCustom = new Krypton.Toolkit.KryptonButton();
        this.kbtnLoadCustom = new Krypton.Toolkit.KryptonButton();
        this.kchkShowShadow = new Krypton.Toolkit.KryptonCheckBox();
        this.klblShadowOffset = new Krypton.Toolkit.KryptonLabel();
        this.knudShadowOffset = new Krypton.Toolkit.KryptonNumericUpDown();
        this.klblShadowBlur = new Krypton.Toolkit.KryptonLabel();
        this.knudShadowBlur = new Krypton.Toolkit.KryptonNumericUpDown();
        this.klblShadowOpacity = new Krypton.Toolkit.KryptonLabel();
        this.knudShadowOpacity = new Krypton.Toolkit.KryptonNumericUpDown();
        this.kcbtnShadow = new Krypton.Toolkit.KryptonColorButton();
        this.kchkRememberGroups = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkShowAssist = new Krypton.Toolkit.KryptonCheckBox();
        this.kbtnOpenPeer = new Krypton.Toolkit.KryptonButton();
        this.klblBounds = new Krypton.Toolkit.KryptonLabel();
        ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
        this.kryptonPanel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbMode)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbGlobal)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).BeginInit();
        this.SuspendLayout();
        // 
        // kryptonPanel1
        // 
        this.kryptonPanel1.Controls.Add(this.klblBounds);
        this.kryptonPanel1.Controls.Add(this.kbtnOpenPeer);
        this.kryptonPanel1.Controls.Add(this.kchkShowAssist);
        this.kryptonPanel1.Controls.Add(this.kchkRememberGroups);
        this.kryptonPanel1.Controls.Add(this.kcbtnShadow);
        this.kryptonPanel1.Controls.Add(this.knudShadowOpacity);
        this.kryptonPanel1.Controls.Add(this.klblShadowOpacity);
        this.kryptonPanel1.Controls.Add(this.knudShadowBlur);
        this.kryptonPanel1.Controls.Add(this.klblShadowBlur);
        this.kryptonPanel1.Controls.Add(this.knudShadowOffset);
        this.kryptonPanel1.Controls.Add(this.klblShadowOffset);
        this.kryptonPanel1.Controls.Add(this.kchkShowShadow);
        this.kryptonPanel1.Controls.Add(this.kbtnLoadCustom);
        this.kryptonPanel1.Controls.Add(this.kbtnSaveCustom);
        this.kryptonPanel1.Controls.Add(this.kbtnClearCustom);
        this.kryptonPanel1.Controls.Add(this.kbtnAddCustom);
        this.kryptonPanel1.Controls.Add(this.kchkIncludeBuiltin);
        this.kryptonPanel1.Controls.Add(this.kcmbTheme);
        this.kryptonPanel1.Controls.Add(this.klblTheme);
        this.kryptonPanel1.Controls.Add(this.knudPreviewOpacity);
        this.kryptonPanel1.Controls.Add(this.klblPreviewOpacity);
        this.kryptonPanel1.Controls.Add(this.kchkShowPreviewGhost);
        this.kryptonPanel1.Controls.Add(this.kchkShowPreview);
        this.kryptonPanel1.Controls.Add(this.kchkShowIcon);
        this.kryptonPanel1.Controls.Add(this.kcmbGlobal);
        this.kryptonPanel1.Controls.Add(this.klblGlobal);
        this.kryptonPanel1.Controls.Add(this.kcmbMode);
        this.kryptonPanel1.Controls.Add(this.klblMode);
        this.kryptonPanel1.Controls.Add(this.klblInstructions);
        this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
        this.kryptonPanel1.Name = "kryptonPanel1";
        this.kryptonPanel1.Padding = new System.Windows.Forms.Padding(12);
        this.kryptonPanel1.Size = new System.Drawing.Size(720, 860);
        this.kryptonPanel1.TabIndex = 0;
        // 
        // klblInstructions
        // 
        this.klblInstructions.AutoSize = false;
        this.klblInstructions.LabelStyle = Krypton.Toolkit.LabelStyle.NormalPanel;
        this.klblInstructions.Location = new System.Drawing.Point(16, 16);
        this.klblInstructions.Name = "klblInstructions";
        this.klblInstructions.Size = new System.Drawing.Size(680, 268);
        this.klblInstructions.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
        this.klblInstructions.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
        this.klblInstructions.StateCommon.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Near;
        this.klblInstructions.TabIndex = 0;
        this.klblInstructions.Values.Text = "Issue #4579. Rest the pointer on this form's maximize button.\r\n\r\nAll forms follow KryptonManager.SnapLayoutValues.Mode. A KryptonManager edits the same values under Snap layout. System (the default) keeps the Windows 11 menu. Toolkit draws the palette menu and snaps this window. None is maximize and restore only.\r\nThis form inherits that choice unless you pick a mode of its own.\r\nShow application icon puts this form in the zone it will snap to and the other taskbar applications in the remaining zones.\r\nShow snap preview draws that rectangle in the palette highlight colours. Preview % is its opacity. Show preview ghost stretches a capture of this window into the zone instead of the colour fill.\r\nShow shadow draws the drop shadow. Offset is the drop in pixels, blur is the halo, and shadow % is the strength. The colour button tints it; black is the default.\r\nAdd 70/30 layout appends a custom card (70% then 30%) after the builtin cards. That card is labelled Custom. Clear custom layouts removes those cards. Save / Load custom layouts write and restore those cards (and Include builtin layouts) as versioned XML. Include builtin layouts keeps the standard cards; clear it to show only custom cards.\r\nOpen peer form, set All forms to Toolkit, snap this window into one half, then pick the peer from Assist for the other half. Hover the same card again for Group | … and click a group zone to restore both. Remember groups / Show Assist turn those off.\r\nThe restore glyph returns the previous size. Drag the caption to unsnap. Win+Z stays the shell menu.";
        // 
        // klblMode
        // 
        this.klblMode.Location = new System.Drawing.Point(16, 300);
        this.klblMode.Name = "klblMode";
        this.klblMode.Size = new System.Drawing.Size(120, 24);
        this.klblMode.TabIndex = 1;
        this.klblMode.Values.Text = "This form";
        // 
        // kcmbMode
        // 
        this.kcmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbMode.DropDownWidth = 220;
        this.kcmbMode.Location = new System.Drawing.Point(150, 296);
        this.kcmbMode.Name = "kcmbMode";
        this.kcmbMode.Size = new System.Drawing.Size(220, 22);
        this.kcmbMode.TabIndex = 2;
        // 
        // klblGlobal
        // 
        this.klblGlobal.Location = new System.Drawing.Point(16, 340);
        this.klblGlobal.Name = "klblGlobal";
        this.klblGlobal.Size = new System.Drawing.Size(120, 24);
        this.klblGlobal.TabIndex = 3;
        this.klblGlobal.Values.Text = "All forms";
        // 
        // kcmbGlobal
        // 
        this.kcmbGlobal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbGlobal.DropDownWidth = 220;
        this.kcmbGlobal.Location = new System.Drawing.Point(150, 336);
        this.kcmbGlobal.Name = "kcmbGlobal";
        this.kcmbGlobal.Size = new System.Drawing.Size(220, 22);
        this.kcmbGlobal.TabIndex = 4;
        // 
        // kchkShowIcon
        // 
        this.kchkShowIcon.Checked = true;
        this.kchkShowIcon.Location = new System.Drawing.Point(150, 376);
        this.kchkShowIcon.Name = "kchkShowIcon";
        this.kchkShowIcon.Size = new System.Drawing.Size(220, 24);
        this.kchkShowIcon.TabIndex = 5;
        this.kchkShowIcon.Values.Text = "Show application icon";
        // 
        // kchkShowPreview
        // 
        this.kchkShowPreview.Checked = true;
        this.kchkShowPreview.Location = new System.Drawing.Point(390, 376);
        this.kchkShowPreview.Name = "kchkShowPreview";
        this.kchkShowPreview.Size = new System.Drawing.Size(220, 24);
        this.kchkShowPreview.TabIndex = 6;
        this.kchkShowPreview.Values.Text = "Show snap preview";
        // 
        // kchkShowPreviewGhost
        // 
        this.kchkShowPreviewGhost.Location = new System.Drawing.Point(390, 412);
        this.kchkShowPreviewGhost.Name = "kchkShowPreviewGhost";
        this.kchkShowPreviewGhost.Size = new System.Drawing.Size(220, 24);
        this.kchkShowPreviewGhost.TabIndex = 7;
        this.kchkShowPreviewGhost.Values.Text = "Show preview ghost";
        // 
        // klblPreviewOpacity
        // 
        this.klblPreviewOpacity.Location = new System.Drawing.Point(16, 416);
        this.klblPreviewOpacity.Name = "klblPreviewOpacity";
        this.klblPreviewOpacity.Size = new System.Drawing.Size(120, 24);
        this.klblPreviewOpacity.TabIndex = 8;
        this.klblPreviewOpacity.Values.Text = "Preview %";
        // 
        // knudPreviewOpacity
        // 
        this.knudPreviewOpacity.Location = new System.Drawing.Point(150, 412);
        this.knudPreviewOpacity.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
        this.knudPreviewOpacity.Name = "knudPreviewOpacity";
        this.knudPreviewOpacity.Size = new System.Drawing.Size(80, 22);
        this.knudPreviewOpacity.TabIndex = 9;
        this.knudPreviewOpacity.Value = new decimal(new int[] { 45, 0, 0, 0 });
        // 
        // klblTheme
        // 
        this.klblTheme.Location = new System.Drawing.Point(16, 456);
        this.klblTheme.Name = "klblTheme";
        this.klblTheme.Size = new System.Drawing.Size(120, 24);
        this.klblTheme.TabIndex = 9;
        this.klblTheme.Values.Text = "Palette";
        // 
        // kcmbTheme
        // 
        this.kcmbTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbTheme.DropDownWidth = 220;
        this.kcmbTheme.Location = new System.Drawing.Point(150, 452);
        this.kcmbTheme.Name = "kcmbTheme";
        this.kcmbTheme.Size = new System.Drawing.Size(220, 22);
        this.kcmbTheme.TabIndex = 10;
        // 
        // kchkIncludeBuiltin
        // 
        this.kchkIncludeBuiltin.Checked = true;
        this.kchkIncludeBuiltin.Location = new System.Drawing.Point(150, 492);
        this.kchkIncludeBuiltin.Name = "kchkIncludeBuiltin";
        this.kchkIncludeBuiltin.Size = new System.Drawing.Size(220, 24);
        this.kchkIncludeBuiltin.TabIndex = 11;
        this.kchkIncludeBuiltin.Values.Text = "Include builtin layouts";
        // 
        // kbtnAddCustom
        // 
        this.kbtnAddCustom.Location = new System.Drawing.Point(150, 528);
        this.kbtnAddCustom.Name = "kbtnAddCustom";
        this.kbtnAddCustom.Size = new System.Drawing.Size(180, 28);
        this.kbtnAddCustom.TabIndex = 12;
        this.kbtnAddCustom.Values.Text = "Add 70/30 layout";
        // 
        // kbtnClearCustom
        // 
        this.kbtnClearCustom.Location = new System.Drawing.Point(340, 528);
        this.kbtnClearCustom.Name = "kbtnClearCustom";
        this.kbtnClearCustom.Size = new System.Drawing.Size(180, 28);
        this.kbtnClearCustom.TabIndex = 13;
        this.kbtnClearCustom.Values.Text = "Clear custom layouts";
        // 
        // kbtnSaveCustom
        // 
        this.kbtnSaveCustom.Location = new System.Drawing.Point(150, 564);
        this.kbtnSaveCustom.Name = "kbtnSaveCustom";
        this.kbtnSaveCustom.Size = new System.Drawing.Size(180, 28);
        this.kbtnSaveCustom.TabIndex = 14;
        this.kbtnSaveCustom.Values.Text = "Save custom layouts…";
        // 
        // kbtnLoadCustom
        // 
        this.kbtnLoadCustom.Location = new System.Drawing.Point(340, 564);
        this.kbtnLoadCustom.Name = "kbtnLoadCustom";
        this.kbtnLoadCustom.Size = new System.Drawing.Size(180, 28);
        this.kbtnLoadCustom.TabIndex = 15;
        this.kbtnLoadCustom.Values.Text = "Load custom layouts…";
        // 
        // kchkShowShadow
        // 
        this.kchkShowShadow.Checked = true;
        this.kchkShowShadow.Location = new System.Drawing.Point(150, 604);
        this.kchkShowShadow.Name = "kchkShowShadow";
        this.kchkShowShadow.Size = new System.Drawing.Size(140, 24);
        this.kchkShowShadow.TabIndex = 14;
        this.kchkShowShadow.Values.Text = "Show shadow";
        // 
        // klblShadowOffset
        // 
        this.klblShadowOffset.Location = new System.Drawing.Point(310, 604);
        this.klblShadowOffset.Name = "klblShadowOffset";
        this.klblShadowOffset.Size = new System.Drawing.Size(70, 24);
        this.klblShadowOffset.TabIndex = 15;
        this.klblShadowOffset.Values.Text = "Offset";
        // 
        // knudShadowOffset
        // 
        this.knudShadowOffset.Location = new System.Drawing.Point(390, 600);
        this.knudShadowOffset.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
        this.knudShadowOffset.Name = "knudShadowOffset";
        this.knudShadowOffset.Size = new System.Drawing.Size(70, 22);
        this.knudShadowOffset.TabIndex = 16;
        this.knudShadowOffset.Value = new decimal(new int[] { 3, 0, 0, 0 });
        // 
        // klblShadowBlur
        // 
        this.klblShadowBlur.Location = new System.Drawing.Point(16, 644);
        this.klblShadowBlur.Name = "klblShadowBlur";
        this.klblShadowBlur.Size = new System.Drawing.Size(70, 24);
        this.klblShadowBlur.TabIndex = 17;
        this.klblShadowBlur.Values.Text = "Blur";
        // 
        // knudShadowBlur
        // 
        this.knudShadowBlur.Location = new System.Drawing.Point(150, 640);
        this.knudShadowBlur.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
        this.knudShadowBlur.Name = "knudShadowBlur";
        this.knudShadowBlur.Size = new System.Drawing.Size(70, 22);
        this.knudShadowBlur.TabIndex = 18;
        // 
        // klblShadowOpacity
        // 
        this.klblShadowOpacity.Location = new System.Drawing.Point(240, 644);
        this.klblShadowOpacity.Name = "klblShadowOpacity";
        this.klblShadowOpacity.Size = new System.Drawing.Size(90, 24);
        this.klblShadowOpacity.TabIndex = 19;
        this.klblShadowOpacity.Values.Text = "Shadow %";
        // 
        // knudShadowOpacity
        // 
        this.knudShadowOpacity.Location = new System.Drawing.Point(340, 640);
        this.knudShadowOpacity.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
        this.knudShadowOpacity.Name = "knudShadowOpacity";
        this.knudShadowOpacity.Size = new System.Drawing.Size(70, 22);
        this.knudShadowOpacity.TabIndex = 20;
        this.knudShadowOpacity.Value = new decimal(new int[] { 18, 0, 0, 0 });
        // 
        // kcbtnShadow
        // 
        this.kcbtnShadow.Location = new System.Drawing.Point(430, 636);
        this.kcbtnShadow.Name = "kcbtnShadow";
        this.kcbtnShadow.Size = new System.Drawing.Size(160, 28);
        this.kcbtnShadow.TabIndex = 21;
        this.kcbtnShadow.Values.Text = "Shadow colour";
        // 
        // kchkRememberGroups
        // 
        this.kchkRememberGroups.Checked = true;
        this.kchkRememberGroups.Location = new System.Drawing.Point(150, 680);
        this.kchkRememberGroups.Name = "kchkRememberGroups";
        this.kchkRememberGroups.Size = new System.Drawing.Size(180, 24);
        this.kchkRememberGroups.TabIndex = 22;
        this.kchkRememberGroups.Values.Text = "Remember groups";
        // 
        // kchkShowAssist
        // 
        this.kchkShowAssist.Checked = true;
        this.kchkShowAssist.Location = new System.Drawing.Point(340, 680);
        this.kchkShowAssist.Name = "kchkShowAssist";
        this.kchkShowAssist.Size = new System.Drawing.Size(160, 24);
        this.kchkShowAssist.TabIndex = 23;
        this.kchkShowAssist.Values.Text = "Show Assist";
        // 
        // kbtnOpenPeer
        // 
        this.kbtnOpenPeer.Location = new System.Drawing.Point(520, 676);
        this.kbtnOpenPeer.Name = "kbtnOpenPeer";
        this.kbtnOpenPeer.Size = new System.Drawing.Size(160, 28);
        this.kbtnOpenPeer.TabIndex = 24;
        this.kbtnOpenPeer.Values.Text = "Open peer form";
        // 
        // klblBounds
        // 
        this.klblBounds.AutoSize = false;
        this.klblBounds.Location = new System.Drawing.Point(16, 720);
        this.klblBounds.Name = "klblBounds";
        this.klblBounds.Size = new System.Drawing.Size(680, 64);
        this.klblBounds.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
        this.klblBounds.TabIndex = 27;
        this.klblBounds.Values.Text = "Bounds";
        // 
        // SnapLayoutOverlayDemo
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(720, 860);
        this.Controls.Add(this.kryptonPanel1);
        this.MinimumSize = new System.Drawing.Size(480, 820);
        this.Name = "SnapLayoutOverlayDemo";
        this.Text = "4579 Snap Layout Overlay";
        this.Load += new System.EventHandler(this.SnapLayoutOverlayDemo_Load);
        ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
        this.kryptonPanel1.ResumeLayout(false);
        this.kryptonPanel1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbMode)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbGlobal)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).EndInit();
        this.ResumeLayout(false);
    }

    private Krypton.Toolkit.KryptonPanel kryptonPanel1;
    private Krypton.Toolkit.KryptonLabel klblInstructions;
    private Krypton.Toolkit.KryptonLabel klblMode;
    private Krypton.Toolkit.KryptonComboBox kcmbMode;
    private Krypton.Toolkit.KryptonLabel klblGlobal;
    private Krypton.Toolkit.KryptonComboBox kcmbGlobal;
    private Krypton.Toolkit.KryptonCheckBox kchkShowIcon;
    private Krypton.Toolkit.KryptonCheckBox kchkShowPreview;
    private Krypton.Toolkit.KryptonCheckBox kchkShowPreviewGhost;
    private Krypton.Toolkit.KryptonLabel klblPreviewOpacity;
    private Krypton.Toolkit.KryptonNumericUpDown knudPreviewOpacity;
    private Krypton.Toolkit.KryptonLabel klblTheme;
    private Krypton.Toolkit.KryptonComboBox kcmbTheme;
    private Krypton.Toolkit.KryptonCheckBox kchkIncludeBuiltin;
    private Krypton.Toolkit.KryptonButton kbtnAddCustom;
    private Krypton.Toolkit.KryptonButton kbtnClearCustom;
    private Krypton.Toolkit.KryptonButton kbtnSaveCustom;
    private Krypton.Toolkit.KryptonButton kbtnLoadCustom;
    private Krypton.Toolkit.KryptonCheckBox kchkShowShadow;
    private Krypton.Toolkit.KryptonLabel klblShadowOffset;
    private Krypton.Toolkit.KryptonNumericUpDown knudShadowOffset;
    private Krypton.Toolkit.KryptonLabel klblShadowBlur;
    private Krypton.Toolkit.KryptonNumericUpDown knudShadowBlur;
    private Krypton.Toolkit.KryptonLabel klblShadowOpacity;
    private Krypton.Toolkit.KryptonNumericUpDown knudShadowOpacity;
    private Krypton.Toolkit.KryptonColorButton kcbtnShadow;
    private Krypton.Toolkit.KryptonCheckBox kchkRememberGroups;
    private Krypton.Toolkit.KryptonCheckBox kchkShowAssist;
    private Krypton.Toolkit.KryptonButton kbtnOpenPeer;
    private Krypton.Toolkit.KryptonLabel klblBounds;
}

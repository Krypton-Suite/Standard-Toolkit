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
        this.klblPreviewOpacity = new Krypton.Toolkit.KryptonLabel();
        this.knudPreviewOpacity = new Krypton.Toolkit.KryptonNumericUpDown();
        this.klblTheme = new Krypton.Toolkit.KryptonLabel();
        this.kcmbTheme = new Krypton.Toolkit.KryptonComboBox();
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
        this.kryptonPanel1.Controls.Add(this.kcmbTheme);
        this.kryptonPanel1.Controls.Add(this.klblTheme);
        this.kryptonPanel1.Controls.Add(this.knudPreviewOpacity);
        this.kryptonPanel1.Controls.Add(this.klblPreviewOpacity);
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
        this.kryptonPanel1.Size = new System.Drawing.Size(720, 500);
        this.kryptonPanel1.TabIndex = 0;
        // 
        // klblInstructions
        // 
        this.klblInstructions.AutoSize = false;
        this.klblInstructions.LabelStyle = Krypton.Toolkit.LabelStyle.NormalPanel;
        this.klblInstructions.Location = new System.Drawing.Point(16, 16);
        this.klblInstructions.Name = "klblInstructions";
        this.klblInstructions.Size = new System.Drawing.Size(680, 148);
        this.klblInstructions.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
        this.klblInstructions.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
        this.klblInstructions.StateCommon.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Near;
        this.klblInstructions.TabIndex = 0;
        this.klblInstructions.Values.Text = "Issue #4579. Rest the pointer on this form's maximize button.\r\n\r\nAll forms follow KryptonManager.GlobalSnapLayouts. System (the default) keeps the Windows 11 menu. Toolkit draws the palette menu and snaps this window. None is maximize and restore only.\r\nThis form inherits that choice unless you pick a mode of its own.\r\nShow application icon puts this form in the zone it will snap to and the other taskbar applications in the remaining zones.\r\nShow snap preview draws that rectangle in the palette highlight colours. Preview % is its opacity.\r\nThe restore glyph returns the previous size. Drag the caption to unsnap. Win+Z stays the shell menu.";
        // 
        // klblMode
        // 
        this.klblMode.Location = new System.Drawing.Point(16, 180);
        this.klblMode.Name = "klblMode";
        this.klblMode.Size = new System.Drawing.Size(120, 24);
        this.klblMode.TabIndex = 1;
        this.klblMode.Values.Text = "This form";
        // 
        // kcmbMode
        // 
        this.kcmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbMode.DropDownWidth = 220;
        this.kcmbMode.Location = new System.Drawing.Point(150, 176);
        this.kcmbMode.Name = "kcmbMode";
        this.kcmbMode.Size = new System.Drawing.Size(220, 22);
        this.kcmbMode.TabIndex = 2;
        // 
        // klblGlobal
        // 
        this.klblGlobal.Location = new System.Drawing.Point(16, 220);
        this.klblGlobal.Name = "klblGlobal";
        this.klblGlobal.Size = new System.Drawing.Size(120, 24);
        this.klblGlobal.TabIndex = 3;
        this.klblGlobal.Values.Text = "All forms";
        // 
        // kcmbGlobal
        // 
        this.kcmbGlobal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbGlobal.DropDownWidth = 220;
        this.kcmbGlobal.Location = new System.Drawing.Point(150, 216);
        this.kcmbGlobal.Name = "kcmbGlobal";
        this.kcmbGlobal.Size = new System.Drawing.Size(220, 22);
        this.kcmbGlobal.TabIndex = 4;
        // 
        // kchkShowIcon
        // 
        this.kchkShowIcon.Checked = true;
        this.kchkShowIcon.Location = new System.Drawing.Point(150, 256);
        this.kchkShowIcon.Name = "kchkShowIcon";
        this.kchkShowIcon.Size = new System.Drawing.Size(220, 24);
        this.kchkShowIcon.TabIndex = 5;
        this.kchkShowIcon.Values.Text = "Show application icon";
        // 
        // kchkShowPreview
        // 
        this.kchkShowPreview.Checked = true;
        this.kchkShowPreview.Location = new System.Drawing.Point(390, 256);
        this.kchkShowPreview.Name = "kchkShowPreview";
        this.kchkShowPreview.Size = new System.Drawing.Size(220, 24);
        this.kchkShowPreview.TabIndex = 6;
        this.kchkShowPreview.Values.Text = "Show snap preview";
        // 
        // klblPreviewOpacity
        // 
        this.klblPreviewOpacity.Location = new System.Drawing.Point(16, 296);
        this.klblPreviewOpacity.Name = "klblPreviewOpacity";
        this.klblPreviewOpacity.Size = new System.Drawing.Size(120, 24);
        this.klblPreviewOpacity.TabIndex = 7;
        this.klblPreviewOpacity.Values.Text = "Preview %";
        // 
        // knudPreviewOpacity
        // 
        this.knudPreviewOpacity.Location = new System.Drawing.Point(150, 292);
        this.knudPreviewOpacity.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
        this.knudPreviewOpacity.Name = "knudPreviewOpacity";
        this.knudPreviewOpacity.Size = new System.Drawing.Size(80, 22);
        this.knudPreviewOpacity.TabIndex = 8;
        this.knudPreviewOpacity.Value = new decimal(new int[] { 45, 0, 0, 0 });
        // 
        // klblTheme
        // 
        this.klblTheme.Location = new System.Drawing.Point(16, 336);
        this.klblTheme.Name = "klblTheme";
        this.klblTheme.Size = new System.Drawing.Size(120, 24);
        this.klblTheme.TabIndex = 9;
        this.klblTheme.Values.Text = "Palette";
        // 
        // kcmbTheme
        // 
        this.kcmbTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbTheme.DropDownWidth = 220;
        this.kcmbTheme.Location = new System.Drawing.Point(150, 332);
        this.kcmbTheme.Name = "kcmbTheme";
        this.kcmbTheme.Size = new System.Drawing.Size(220, 22);
        this.kcmbTheme.TabIndex = 10;
        // 
        // klblBounds
        // 
        this.klblBounds.AutoSize = false;
        this.klblBounds.Location = new System.Drawing.Point(16, 384);
        this.klblBounds.Name = "klblBounds";
        this.klblBounds.Size = new System.Drawing.Size(680, 48);
        this.klblBounds.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
        this.klblBounds.TabIndex = 11;
        this.klblBounds.Values.Text = "Bounds";
        // 
        // SnapLayoutOverlayDemo
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(720, 500);
        this.Controls.Add(this.kryptonPanel1);
        this.MinimumSize = new System.Drawing.Size(480, 400);
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
    private Krypton.Toolkit.KryptonLabel klblPreviewOpacity;
    private Krypton.Toolkit.KryptonNumericUpDown knudPreviewOpacity;
    private Krypton.Toolkit.KryptonLabel klblTheme;
    private Krypton.Toolkit.KryptonComboBox kcmbTheme;
    private Krypton.Toolkit.KryptonLabel klblBounds;
}

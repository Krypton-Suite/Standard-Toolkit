#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

partial class BorderlessMdiHostDemo
{
    private System.ComponentModel.IContainer components;

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
        this.panelTop = new Krypton.Toolkit.KryptonPanel();
        this.klblInstructions = new Krypton.Toolkit.KryptonLabel();
        this.panelButtons = new Krypton.Toolkit.KryptonPanel();
        this.btnOpenChild = new Krypton.Toolkit.KryptonButton();
        this.klblManagerMdiPanelStyle = new Krypton.Toolkit.KryptonLabel();
        this.kcmbManagerMdiPanelStyle = new Krypton.Toolkit.KryptonComboBox();
        this.klblFormMdiPanelStyle = new Krypton.Toolkit.KryptonLabel();
        this.kcmbFormMdiPanelStyle = new Krypton.Toolkit.KryptonComboBox();
        this.klblStatus = new Krypton.Toolkit.KryptonLabel();
        ((System.ComponentModel.ISupportInitialize)(this.panelTop)).BeginInit();
        this.panelTop.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.panelButtons)).BeginInit();
        this.panelButtons.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbManagerMdiPanelStyle)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbFormMdiPanelStyle)).BeginInit();
        this.SuspendLayout();
        //
        // panelTop
        //
        this.panelTop.Controls.Add(this.klblInstructions);
        this.panelTop.Controls.Add(this.panelButtons);
        this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
        this.panelTop.Location = new System.Drawing.Point(0, 0);
        this.panelTop.Name = "panelTop";
        this.panelTop.Size = new System.Drawing.Size(900, 150);
        this.panelTop.TabIndex = 0;
        //
        // klblInstructions
        //
        this.klblInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
        this.klblInstructions.Location = new System.Drawing.Point(0, 70);
        this.klblInstructions.Name = "klblInstructions";
        this.klblInstructions.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
        this.klblInstructions.Size = new System.Drawing.Size(900, 80);
        this.klblInstructions.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
        this.klblInstructions.TabIndex = 1;
        this.klblInstructions.Values.Text =
            "Issue #2922 — MDI + Dock.Fill. The child must appear already borderless (no Windows title bar or gray MDI frame). " +
            "MdiChildActivate must increment in the status line. " +
            "Issue #4685 — Manager/Form MdiPanelStyle colour the MDI client (default PanelAlternate; independent of PanelStyle).";
        //
        // panelButtons
        //
        this.panelButtons.Controls.Add(this.btnOpenChild);
        this.panelButtons.Controls.Add(this.klblManagerMdiPanelStyle);
        this.panelButtons.Controls.Add(this.kcmbManagerMdiPanelStyle);
        this.panelButtons.Controls.Add(this.klblFormMdiPanelStyle);
        this.panelButtons.Controls.Add(this.kcmbFormMdiPanelStyle);
        this.panelButtons.Controls.Add(this.klblStatus);
        this.panelButtons.Dock = System.Windows.Forms.DockStyle.Top;
        this.panelButtons.Location = new System.Drawing.Point(0, 0);
        this.panelButtons.Name = "panelButtons";
        this.panelButtons.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
        this.panelButtons.Size = new System.Drawing.Size(900, 70);
        this.panelButtons.TabIndex = 0;
        //
        // btnOpenChild
        //
        this.btnOpenChild.Location = new System.Drawing.Point(8, 7);
        this.btnOpenChild.Name = "btnOpenChild";
        this.btnOpenChild.Size = new System.Drawing.Size(180, 25);
        this.btnOpenChild.TabIndex = 0;
        this.btnOpenChild.Values.Text = "Open dock-filled child";
        this.btnOpenChild.Click += this.BtnOpenChild_Click;
        //
        // klblManagerMdiPanelStyle
        //
        this.klblManagerMdiPanelStyle.Location = new System.Drawing.Point(200, 10);
        this.klblManagerMdiPanelStyle.Name = "klblManagerMdiPanelStyle";
        this.klblManagerMdiPanelStyle.Size = new System.Drawing.Size(90, 20);
        this.klblManagerMdiPanelStyle.TabIndex = 1;
        this.klblManagerMdiPanelStyle.Values.Text = "Manager MDI";
        //
        // kcmbManagerMdiPanelStyle
        //
        this.kcmbManagerMdiPanelStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbManagerMdiPanelStyle.DropDownWidth = 140;
        this.kcmbManagerMdiPanelStyle.Items.AddRange(new object[] {
            "PanelClient",
            "PanelAlternate",
            "PanelCustom1"});
        this.kcmbManagerMdiPanelStyle.Location = new System.Drawing.Point(294, 7);
        this.kcmbManagerMdiPanelStyle.Name = "kcmbManagerMdiPanelStyle";
        this.kcmbManagerMdiPanelStyle.Size = new System.Drawing.Size(140, 22);
        this.kcmbManagerMdiPanelStyle.TabIndex = 2;
        this.kcmbManagerMdiPanelStyle.SelectedIndexChanged += this.kcmbManagerMdiPanelStyle_SelectedIndexChanged;
        //
        // klblFormMdiPanelStyle
        //
        this.klblFormMdiPanelStyle.Location = new System.Drawing.Point(450, 10);
        this.klblFormMdiPanelStyle.Name = "klblFormMdiPanelStyle";
        this.klblFormMdiPanelStyle.Size = new System.Drawing.Size(70, 20);
        this.klblFormMdiPanelStyle.TabIndex = 3;
        this.klblFormMdiPanelStyle.Values.Text = "Form MDI";
        //
        // kcmbFormMdiPanelStyle
        //
        this.kcmbFormMdiPanelStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbFormMdiPanelStyle.DropDownWidth = 140;
        this.kcmbFormMdiPanelStyle.Items.AddRange(new object[] {
            "Inherit",
            "PanelClient",
            "PanelAlternate",
            "PanelCustom1"});
        this.kcmbFormMdiPanelStyle.Location = new System.Drawing.Point(524, 7);
        this.kcmbFormMdiPanelStyle.Name = "kcmbFormMdiPanelStyle";
        this.kcmbFormMdiPanelStyle.Size = new System.Drawing.Size(140, 22);
        this.kcmbFormMdiPanelStyle.TabIndex = 4;
        this.kcmbFormMdiPanelStyle.SelectedIndexChanged += this.kcmbFormMdiPanelStyle_SelectedIndexChanged;
        //
        // klblStatus
        //
        this.klblStatus.Location = new System.Drawing.Point(8, 40);
        this.klblStatus.Name = "klblStatus";
        this.klblStatus.Size = new System.Drawing.Size(880, 20);
        this.klblStatus.TabIndex = 5;
        this.klblStatus.Values.Text = "MdiChildActivate count: 0    Visible MDI children: 0";
        //
        // BorderlessMdiHostDemo
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(900, 520);
        this.Controls.Add(this.panelTop);
        this.IsMdiContainer = true;
        this.Name = "BorderlessMdiHostDemo";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Borderless MDI Host (#2922 / #4685 MdiPanelStyle)";
        ((System.ComponentModel.ISupportInitialize)(this.panelTop)).EndInit();
        this.panelTop.ResumeLayout(false);
        this.panelTop.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.panelButtons)).EndInit();
        this.panelButtons.ResumeLayout(false);
        this.panelButtons.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbManagerMdiPanelStyle)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbFormMdiPanelStyle)).EndInit();
        this.ResumeLayout(false);
    }

    #endregion

    private Krypton.Toolkit.KryptonPanel panelTop;
    private Krypton.Toolkit.KryptonLabel klblInstructions;
    private Krypton.Toolkit.KryptonPanel panelButtons;
    private Krypton.Toolkit.KryptonButton btnOpenChild;
    private Krypton.Toolkit.KryptonLabel klblManagerMdiPanelStyle;
    private Krypton.Toolkit.KryptonComboBox kcmbManagerMdiPanelStyle;
    private Krypton.Toolkit.KryptonLabel klblFormMdiPanelStyle;
    private Krypton.Toolkit.KryptonComboBox kcmbFormMdiPanelStyle;
    private Krypton.Toolkit.KryptonLabel klblStatus;
}

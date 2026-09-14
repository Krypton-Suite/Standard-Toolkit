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
        this.klblStatus = new Krypton.Toolkit.KryptonLabel();
        ((System.ComponentModel.ISupportInitialize)(this.panelTop)).BeginInit();
        this.panelTop.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.panelButtons)).BeginInit();
        this.panelButtons.SuspendLayout();
        this.SuspendLayout();
        //
        // panelTop
        //
        this.panelTop.Controls.Add(this.klblInstructions);
        this.panelTop.Controls.Add(this.panelButtons);
        this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
        this.panelTop.Location = new System.Drawing.Point(0, 0);
        this.panelTop.Name = "panelTop";
        this.panelTop.Size = new System.Drawing.Size(900, 120);
        this.panelTop.TabIndex = 0;
        //
        // klblInstructions
        //
        this.klblInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
        this.klblInstructions.Location = new System.Drawing.Point(0, 40);
        this.klblInstructions.Name = "klblInstructions";
        this.klblInstructions.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
        this.klblInstructions.Size = new System.Drawing.Size(900, 80);
        this.klblInstructions.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
        this.klblInstructions.TabIndex = 1;
        this.klblInstructions.Values.Text =
            "Issue #2922 — MDI + Dock.Fill. The child must appear already borderless (no Windows title bar or gray MDI frame). " +
            "MdiChildActivate must increment in the status line.";
        //
        // panelButtons
        //
        this.panelButtons.Controls.Add(this.btnOpenChild);
        this.panelButtons.Controls.Add(this.klblStatus);
        this.panelButtons.Dock = System.Windows.Forms.DockStyle.Top;
        this.panelButtons.Location = new System.Drawing.Point(0, 0);
        this.panelButtons.Name = "panelButtons";
        this.panelButtons.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
        this.panelButtons.Size = new System.Drawing.Size(900, 40);
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
        // klblStatus
        //
        this.klblStatus.Location = new System.Drawing.Point(200, 10);
        this.klblStatus.Name = "klblStatus";
        this.klblStatus.Size = new System.Drawing.Size(680, 20);
        this.klblStatus.TabIndex = 1;
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
        this.Text = "Borderless MDI Host (Issue #2922)";
        ((System.ComponentModel.ISupportInitialize)(this.panelTop)).EndInit();
        this.panelTop.ResumeLayout(false);
        this.panelTop.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.panelButtons)).EndInit();
        this.panelButtons.ResumeLayout(false);
        this.panelButtons.PerformLayout();
        this.ResumeLayout(false);
    }

    #endregion

    private Krypton.Toolkit.KryptonPanel panelTop;
    private Krypton.Toolkit.KryptonLabel klblInstructions;
    private Krypton.Toolkit.KryptonPanel panelButtons;
    private Krypton.Toolkit.KryptonButton btnOpenChild;
    private Krypton.Toolkit.KryptonLabel klblStatus;
}

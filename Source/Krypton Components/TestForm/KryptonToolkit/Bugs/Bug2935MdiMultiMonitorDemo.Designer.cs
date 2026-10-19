namespace TestForm;

partial class Bug2935MdiMultiMonitorDemo
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
        this.kryptonGroupBoxInstructions = new Krypton.Toolkit.KryptonGroupBox();
        this.lblInstructions = new Krypton.Toolkit.KryptonLabel();
        this.panelButtons = new Krypton.Toolkit.KryptonPanel();
        this.btnNewMdiChild = new Krypton.Toolkit.KryptonButton();
        this.btnMoveToSecondary = new Krypton.Toolkit.KryptonButton();
        this.btnTileHorizontal = new Krypton.Toolkit.KryptonButton();
        this.btnTileVertical = new Krypton.Toolkit.KryptonButton();
        this.btnCascade = new Krypton.Toolkit.KryptonButton();
        this.btnArrangeIcons = new Krypton.Toolkit.KryptonButton();
        ((System.ComponentModel.ISupportInitialize)(this.panelTop)).BeginInit();
        this.panelTop.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxInstructions)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxInstructions.Panel)).BeginInit();
        this.kryptonGroupBoxInstructions.Panel.SuspendLayout();
        this.kryptonGroupBoxInstructions.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.panelButtons)).BeginInit();
        this.panelButtons.SuspendLayout();
        this.SuspendLayout();
        //
        // panelTop
        //
        this.panelTop.Controls.Add(this.kryptonGroupBoxInstructions);
        this.panelTop.Controls.Add(this.panelButtons);
        this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
        this.panelTop.Location = new System.Drawing.Point(0, 0);
        this.panelTop.Name = "panelTop";
        this.panelTop.Size = new System.Drawing.Size(884, 140);
        this.panelTop.TabIndex = 0;
        //
        // kryptonGroupBoxInstructions
        //
        this.kryptonGroupBoxInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kryptonGroupBoxInstructions.Location = new System.Drawing.Point(0, 40);
        this.kryptonGroupBoxInstructions.Name = "kryptonGroupBoxInstructions";
        this.kryptonGroupBoxInstructions.Size = new System.Drawing.Size(884, 100);
        this.kryptonGroupBoxInstructions.TabIndex = 1;
        this.kryptonGroupBoxInstructions.Values.Heading = "Instructions (Issue #2935)";
        //
        // kryptonGroupBoxInstructions.Panel
        //
        this.kryptonGroupBoxInstructions.Panel.Controls.Add(this.lblInstructions);
        //
        // lblInstructions
        //
        this.lblInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
        this.lblInstructions.Location = new System.Drawing.Point(0, 0);
        this.lblInstructions.Name = "lblInstructions";
        this.lblInstructions.Size = new System.Drawing.Size(880, 73);
        this.lblInstructions.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
        this.lblInstructions.StateCommon.ShortText.MultiLineH = Krypton.Toolkit.PaletteRelativeAlign.Near;
        this.lblInstructions.TabIndex = 0;
        this.lblInstructions.Values.Text = "Instructions...";
        //
        // panelButtons
        //
        this.panelButtons.Controls.Add(this.btnNewMdiChild);
        this.panelButtons.Controls.Add(this.btnMoveToSecondary);
        this.panelButtons.Controls.Add(this.btnTileHorizontal);
        this.panelButtons.Controls.Add(this.btnTileVertical);
        this.panelButtons.Controls.Add(this.btnCascade);
        this.panelButtons.Controls.Add(this.btnArrangeIcons);
        this.panelButtons.Dock = System.Windows.Forms.DockStyle.Top;
        this.panelButtons.Location = new System.Drawing.Point(0, 0);
        this.panelButtons.Name = "panelButtons";
        this.panelButtons.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
        this.panelButtons.Size = new System.Drawing.Size(884, 40);
        this.panelButtons.TabIndex = 0;
        //
        // btnNewMdiChild
        //
        this.btnNewMdiChild.Location = new System.Drawing.Point(8, 7);
        this.btnNewMdiChild.Name = "btnNewMdiChild";
        this.btnNewMdiChild.Size = new System.Drawing.Size(120, 25);
        this.btnNewMdiChild.TabIndex = 0;
        this.btnNewMdiChild.Values.Text = "New MDI Child";
        this.btnNewMdiChild.Click += this.BtnNewMdiChild_Click;
        //
        // btnMoveToSecondary
        //
        this.btnMoveToSecondary.Location = new System.Drawing.Point(134, 7);
        this.btnMoveToSecondary.Name = "btnMoveToSecondary";
        this.btnMoveToSecondary.Size = new System.Drawing.Size(160, 25);
        this.btnMoveToSecondary.TabIndex = 1;
        this.btnMoveToSecondary.Values.Text = "Move to secondary monitor";
        this.btnMoveToSecondary.Click += this.BtnMoveToSecondary_Click;
        //
        // btnTileHorizontal
        //
        this.btnTileHorizontal.Location = new System.Drawing.Point(300, 7);
        this.btnTileHorizontal.Name = "btnTileHorizontal";
        this.btnTileHorizontal.Size = new System.Drawing.Size(100, 25);
        this.btnTileHorizontal.TabIndex = 2;
        this.btnTileHorizontal.Values.Text = "Tile horizontal";
        this.btnTileHorizontal.Click += this.BtnTileHorizontal_Click;
        //
        // btnTileVertical
        //
        this.btnTileVertical.Location = new System.Drawing.Point(406, 7);
        this.btnTileVertical.Name = "btnTileVertical";
        this.btnTileVertical.Size = new System.Drawing.Size(90, 25);
        this.btnTileVertical.TabIndex = 3;
        this.btnTileVertical.Values.Text = "Tile vertical";
        this.btnTileVertical.Click += this.BtnTileVertical_Click;
        //
        // btnCascade
        //
        this.btnCascade.Location = new System.Drawing.Point(502, 7);
        this.btnCascade.Name = "btnCascade";
        this.btnCascade.Size = new System.Drawing.Size(75, 25);
        this.btnCascade.TabIndex = 4;
        this.btnCascade.Values.Text = "Cascade";
        this.btnCascade.Click += this.BtnCascade_Click;
        //
        // btnArrangeIcons
        //
        this.btnArrangeIcons.Location = new System.Drawing.Point(583, 7);
        this.btnArrangeIcons.Name = "btnArrangeIcons";
        this.btnArrangeIcons.Size = new System.Drawing.Size(100, 25);
        this.btnArrangeIcons.TabIndex = 5;
        this.btnArrangeIcons.Values.Text = "Arrange icons";
        this.btnArrangeIcons.Click += this.BtnArrangeIcons_Click;
        //
        // Bug2935MdiMultiMonitorDemo
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(884, 461);
        this.Controls.Add(this.panelTop);
        this.IsMdiContainer = true;
        this.Name = "Bug2935MdiMultiMonitorDemo";
        this.Text = "Bug 2935 – MDI multi-monitor border demo";
        ((System.ComponentModel.ISupportInitialize)(this.panelTop)).EndInit();
        this.panelTop.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxInstructions.Panel)).EndInit();
        this.kryptonGroupBoxInstructions.Panel.ResumeLayout(false);
        this.kryptonGroupBoxInstructions.Panel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxInstructions)).EndInit();
        this.kryptonGroupBoxInstructions.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.panelButtons)).EndInit();
        this.panelButtons.ResumeLayout(false);
        this.ResumeLayout(false);
    }

    #endregion

    private Krypton.Toolkit.KryptonPanel panelTop;
    private Krypton.Toolkit.KryptonGroupBox kryptonGroupBoxInstructions;
    private Krypton.Toolkit.KryptonLabel lblInstructions;
    private Krypton.Toolkit.KryptonPanel panelButtons;
    private Krypton.Toolkit.KryptonButton btnNewMdiChild;
    private Krypton.Toolkit.KryptonButton btnMoveToSecondary;
    private Krypton.Toolkit.KryptonButton btnTileHorizontal;
    private Krypton.Toolkit.KryptonButton btnTileVertical;
    private Krypton.Toolkit.KryptonButton btnCascade;
    private Krypton.Toolkit.KryptonButton btnArrangeIcons;
}

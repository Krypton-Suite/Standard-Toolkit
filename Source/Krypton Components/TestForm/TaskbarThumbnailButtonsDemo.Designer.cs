namespace TestForm;

partial class TaskbarThumbnailButtonsDemo
{
    private System.ComponentModel.IContainer components = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _playIcon?.Dispose();
            _pauseIcon?.Dispose();
            _nextIcon?.Dispose();
            _stopIcon?.Dispose();
            components?.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.grpDemo = new Krypton.Toolkit.KryptonGroupBox();
        this.lblInstructions = new Krypton.Toolkit.KryptonWrapLabel();
        this.lblLastClicked = new Krypton.Toolkit.KryptonLabel();
        this.btnAddButtons = new Krypton.Toolkit.KryptonButton();
        this.btnClearButtons = new Krypton.Toolkit.KryptonButton();
        ((System.ComponentModel.ISupportInitialize)(this.grpDemo)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.grpDemo.Panel)).BeginInit();
        this.grpDemo.Panel.SuspendLayout();
        this.grpDemo.SuspendLayout();
        this.SuspendLayout();
        //
        // grpDemo
        //
        this.grpDemo.Location = new System.Drawing.Point(12, 12);
        this.grpDemo.Name = "grpDemo";
        this.grpDemo.Size = new System.Drawing.Size(460, 180);
        this.grpDemo.TabIndex = 0;
        this.grpDemo.Values.Heading = "Taskbar Thumbnail Buttons";
        //
        // grpDemo.Panel
        //
        this.grpDemo.Panel.Controls.Add(this.lblInstructions);
        this.grpDemo.Panel.Controls.Add(this.lblLastClicked);
        this.grpDemo.Panel.Controls.Add(this.btnAddButtons);
        this.grpDemo.Panel.Controls.Add(this.btnClearButtons);
        //
        // lblInstructions
        //
        this.lblInstructions.AutoSize = false;
        this.lblInstructions.Location = new System.Drawing.Point(15, 15);
        this.lblInstructions.Name = "lblInstructions";
        this.lblInstructions.Size = new System.Drawing.Size(430, 50);
        this.lblInstructions.Text = "Hover over this application's taskbar button to show the thumbnail preview. " +
            "You will see Play, Pause, Next, and Stop buttons. Click them to trigger actions.";
        //
        // lblLastClicked
        //
        this.lblLastClicked.Location = new System.Drawing.Point(15, 75);
        this.lblLastClicked.Name = "lblLastClicked";
        this.lblLastClicked.Size = new System.Drawing.Size(430, 20);
        this.lblLastClicked.TabIndex = 1;
        this.lblLastClicked.Values.Text = "Hover the taskbar button, then click a thumbnail toolbar button.";
        //
        // btnAddButtons
        //
        this.btnAddButtons.Location = new System.Drawing.Point(15, 110);
        this.btnAddButtons.Name = "btnAddButtons";
        this.btnAddButtons.Size = new System.Drawing.Size(130, 35);
        this.btnAddButtons.TabIndex = 2;
        this.btnAddButtons.Values.Text = "Add Buttons";
        this.btnAddButtons.Click += BtnAddButtons_Click;
        //
        // btnClearButtons
        //
        this.btnClearButtons.Location = new System.Drawing.Point(155, 110);
        this.btnClearButtons.Name = "btnClearButtons";
        this.btnClearButtons.Size = new System.Drawing.Size(130, 35);
        this.btnClearButtons.TabIndex = 3;
        this.btnClearButtons.Values.Text = "Clear Buttons";
        this.btnClearButtons.Click += BtnClearButtons_Click;
        //
        // TaskbarThumbnailButtonsDemo
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(484, 211);
        this.Controls.Add(this.grpDemo);
        this.Name = "TaskbarThumbnailButtonsDemo";
        this.ShowInTaskbar = true;
        this.Text = "Taskbar Thumbnail Buttons Demo";
        this.Load += TaskbarThumbnailButtonsDemo_Load;
        ((System.ComponentModel.ISupportInitialize)(this.grpDemo.Panel)).EndInit();
        this.grpDemo.Panel.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.grpDemo)).EndInit();
        this.grpDemo.ResumeLayout(false);
        this.ResumeLayout(false);
    }

    #endregion

    private Krypton.Toolkit.KryptonGroupBox grpDemo;
    private Krypton.Toolkit.KryptonWrapLabel lblInstructions;
    private Krypton.Toolkit.KryptonLabel lblLastClicked;
    private Krypton.Toolkit.KryptonButton btnAddButtons;
    private Krypton.Toolkit.KryptonButton btnClearButtons;
}

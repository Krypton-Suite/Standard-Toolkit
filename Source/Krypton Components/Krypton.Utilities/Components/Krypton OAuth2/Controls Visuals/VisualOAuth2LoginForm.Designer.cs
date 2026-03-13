namespace Krypton.Utilities
{
#if WEBVIEW2_AVAILABLE

internal sealed partial class VisualOAuth2LoginForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
        this.kryptonWebView21 = new Krypton.Utilities.KryptonWebView2();
        this.kryptonStatusLabel = new Krypton.Toolkit.KryptonLabel();
        ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
        this.kryptonPanel1.SuspendLayout();
        this.SuspendLayout();
        //
        // kryptonPanel1
        //
        this.kryptonPanel1.Controls.Add(this.kryptonWebView21);
        this.kryptonPanel1.Controls.Add(this.kryptonStatusLabel);
        this.kryptonPanel1.Dock = DockStyle.Fill;
        this.kryptonPanel1.Location = new Point(0, 0);
        this.kryptonPanel1.Name = "kryptonPanel1";
        this.kryptonPanel1.PanelBackStyle = PaletteBackStyle.ControlClient;
        this.kryptonPanel1.Size = new Size(600, 700);
        this.kryptonPanel1.TabIndex = 0;
        //
        // kryptonWebView21
        //
        this.kryptonWebView21.Dock = DockStyle.Fill;
        this.kryptonWebView21.Location = new Point(0, 28);
        this.kryptonWebView21.Name = "kryptonWebView21";
        this.kryptonWebView21.Size = new Size(600, 672);
        this.kryptonWebView21.TabIndex = 1;
        //
        // kryptonStatusLabel
        //
        this.kryptonStatusLabel.AutoSize = false;
        this.kryptonStatusLabel.Dock = DockStyle.Top;
        this.kryptonStatusLabel.Height = 28;
        this.kryptonStatusLabel.LabelStyle = LabelStyle.NormalControl;
        this.kryptonStatusLabel.Location = new Point(0, 0);
        this.kryptonStatusLabel.Name = "kryptonStatusLabel";
        this.kryptonStatusLabel.Padding = new Padding(8, 8, 8, 4);
        this.kryptonStatusLabel.Size = new Size(600, 28);
        this.kryptonStatusLabel.TabIndex = 0;
        this.kryptonStatusLabel.Values.Text = "Loading sign-in page...";
        //
        // VisualOAuth2LoginForm
        //
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(600, 700);
        this.Controls.Add(this.kryptonPanel1);
        this.FormBorderStyle = FormBorderStyle.Sizable;
        this.MinimumSize = new Size(400, 400);
        this.Name = "VisualOAuth2LoginForm";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "Sign in";
        ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
        this.kryptonPanel1.ResumeLayout(false);
        this.kryptonPanel1.PerformLayout();
        this.ResumeLayout(false);
    }

    #endregion

    private Krypton.Toolkit.KryptonPanel kryptonPanel1;
    private Krypton.Utilities.KryptonWebView2 kryptonWebView21;
    private Krypton.Toolkit.KryptonLabel kryptonStatusLabel;
}

#else

    internal sealed partial class VisualOAuth2LoginForm
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 700);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "VisualOAuth2LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sign in";
            this.ResumeLayout(false);
        }
    }

#endif
}
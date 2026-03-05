namespace TestForm
{
    partial class OAuth2Demo
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
            this.components = new System.ComponentModel.Container();
            this.SuspendLayout();
            //
            // OAuth2Demo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "OAuth2Demo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OAuth2 PKCE Demo";
            this.Load += new System.EventHandler(this.OAuth2Demo_Load);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
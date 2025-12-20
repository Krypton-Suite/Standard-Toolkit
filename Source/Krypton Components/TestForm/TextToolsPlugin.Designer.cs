namespace TestForm
{
    partial class TextToolsPlugin
    {
        private System.ComponentModel.IContainer components = null;
        private Krypton.Toolkit.KryptonLabel lblPluginStatus;

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
            this.SuspendLayout();
            // 
            // TextToolsPlugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "TextToolsPlugin";
            this.Size = new System.Drawing.Size(800, 400);
            this.ResumeLayout(false);
        }
    }
}


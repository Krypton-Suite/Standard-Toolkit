namespace TestForm
{
    partial class Form6
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonToolkitPoweredByControl1 = new Krypton.Toolkit.KryptonToolkitPoweredByControl();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonToolkitPoweredByControl1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(800, 450);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonToolkitPoweredByControl1
            // 
            this.kryptonToolkitPoweredByControl1.BackColor = System.Drawing.Color.Transparent;
            this.kryptonToolkitPoweredByControl1.Location = new System.Drawing.Point(84, 12);
            this.kryptonToolkitPoweredByControl1.Name = "kryptonToolkitPoweredByControl1";
            this.kryptonToolkitPoweredByControl1.ShowThemeOption = false;
            this.kryptonToolkitPoweredByControl1.ShowVersions = false;
            this.kryptonToolkitPoweredByControl1.Size = new System.Drawing.Size(659, 249);
            this.kryptonToolkitPoweredByControl1.TabIndex = 1;
            this.kryptonToolkitPoweredByControl1.ToolkitType = Krypton.Toolkit.ToolkitType.Stable;
            // 
            // Form6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "Form6";
            this.Text = "Form6";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonToolkitPoweredByControl kryptonToolkitPoweredByControl1;
    }
}
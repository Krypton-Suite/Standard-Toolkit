namespace TestForm.RTLTests
{
    partial class RTLTestForm
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
            this.ktsRTL = new Krypton.Toolkit.KryptonToggleSwitch();
            this.buttonSpecAny1 = new Krypton.Toolkit.ButtonSpecAny();
            this.buttonSpecAny2 = new Krypton.Toolkit.ButtonSpecAny();
            this.kryptonPropertyGrid1 = new Krypton.Toolkit.KryptonPropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonPropertyGrid1);
            this.kryptonPanel1.Controls.Add(this.ktsRTL);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(804, 438);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // ktsRTL
            // 
            this.ktsRTL.Location = new System.Drawing.Point(345, 152);
            this.ktsRTL.Name = "ktsRTL";
            this.ktsRTL.Size = new System.Drawing.Size(90, 28);
            this.ktsRTL.TabIndex = 0;
            this.ktsRTL.Text = "Toggle RTL States";
            this.ktsRTL.CheckedChanged += new System.EventHandler(this.ktsRTL_CheckedChanged);
            // 
            // buttonSpecAny1
            // 
            this.buttonSpecAny1.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Previous;
            this.buttonSpecAny1.UniqueName = "4c02382cf86f4197ac6cc70654c2834e";
            // 
            // buttonSpecAny2
            // 
            this.buttonSpecAny2.Type = Krypton.Toolkit.PaletteButtonSpecStyle.ArrowLeft;
            this.buttonSpecAny2.UniqueName = "6707f9f6597349d1a4832a107cc5522b";
            // 
            // kryptonPropertyGrid1
            // 
            this.kryptonPropertyGrid1.Location = new System.Drawing.Point(13, 13);
            this.kryptonPropertyGrid1.Name = "kryptonPropertyGrid1";
            this.kryptonPropertyGrid1.Padding = new System.Windows.Forms.Padding(1);
            this.kryptonPropertyGrid1.SelectedObject = this;
            this.kryptonPropertyGrid1.Size = new System.Drawing.Size(326, 413);
            this.kryptonPropertyGrid1.TabIndex = 1;
            this.kryptonPropertyGrid1.Text = "kryptonPropertyGrid1";
            // 
            // RTLTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ButtonSpecs.Add(this.buttonSpecAny1);
            this.ButtonSpecs.Add(this.buttonSpecAny2);
            this.ClientSize = new System.Drawing.Size(804, 438);
            this.Controls.Add(this.kryptonPanel1);
            this.HelpButton = true;
            this.Name = "RTLTestForm";
            this.Text = "RTLTestForm";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonToggleSwitch ktsRTL;
        private ButtonSpecAny buttonSpecAny1;
        private ButtonSpecAny buttonSpecAny2;
        private KryptonPropertyGrid kryptonPropertyGrid1;
    }
}
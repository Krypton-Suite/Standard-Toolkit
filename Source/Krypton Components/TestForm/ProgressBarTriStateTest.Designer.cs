namespace TestForm
{
    partial class ProgressBarTriStateTest
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
            this.kryptonProgressBar1 = new Krypton.Toolkit.KryptonProgressBar();
            this.kryptonPropertyGrid1 = new Krypton.Toolkit.KryptonPropertyGrid();
            this.kryptonTrackBar1 = new Krypton.Toolkit.KryptonTrackBar();
            this.SuspendLayout();
            // 
            // kryptonProgressBar1
            // 
            this.kryptonProgressBar1.Location = new System.Drawing.Point(131, 120);
            this.kryptonProgressBar1.Maximum = 200;
            this.kryptonProgressBar1.Name = "kryptonProgressBar1";
            this.kryptonProgressBar1.Size = new System.Drawing.Size(353, 26);
            this.kryptonProgressBar1.StateCommon.Back.Color1 = System.Drawing.Color.Green;
            this.kryptonProgressBar1.StateDisabled.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kryptonProgressBar1.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kryptonProgressBar1.TabIndex = 0;
            this.kryptonProgressBar1.Text = "kryptonProgressBar1";
            this.kryptonProgressBar1.TextBackdropColor = System.Drawing.Color.Empty;
            this.kryptonProgressBar1.TextShadowColor = System.Drawing.Color.Empty;
            this.kryptonProgressBar1.Values.Text = "kryptonProgressBar1";
            // 
            // kryptonPropertyGrid1
            // 
            this.kryptonPropertyGrid1.Location = new System.Drawing.Point(612, 53);
            this.kryptonPropertyGrid1.Name = "kryptonPropertyGrid1";
            this.kryptonPropertyGrid1.Padding = new System.Windows.Forms.Padding(1);
            this.kryptonPropertyGrid1.SelectedObject = this.kryptonProgressBar1;
            this.kryptonPropertyGrid1.Size = new System.Drawing.Size(460, 500);
            this.kryptonPropertyGrid1.TabIndex = 1;
            this.kryptonPropertyGrid1.Text = "kryptonPropertyGrid1";
            // 
            // kryptonTrackBar1
            // 
            this.kryptonTrackBar1.Location = new System.Drawing.Point(131, 168);
            this.kryptonTrackBar1.Maximum = 200;
            this.kryptonTrackBar1.Name = "kryptonTrackBar1";
            this.kryptonTrackBar1.Size = new System.Drawing.Size(353, 27);
            this.kryptonTrackBar1.TabIndex = 2;
            this.kryptonTrackBar1.ValueChanged += new System.EventHandler(this.kryptonTrackBar1_ValueChanged);
            // 
            // ProgressBarTriStateTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 653);
            this.Controls.Add(this.kryptonTrackBar1);
            this.Controls.Add(this.kryptonPropertyGrid1);
            this.Controls.Add(this.kryptonProgressBar1);
            this.Name = "ProgressBarTriStateTest";
            this.Text = "ProgressBarTriStateTest";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KryptonProgressBar kryptonProgressBar1;
        private KryptonPropertyGrid kryptonPropertyGrid1;
        private KryptonTrackBar kryptonTrackBar1;
    }
}
namespace TestForm
{
    partial class Bug3013TestForm
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
            this.kwlblFormResizeData = new Krypton.Toolkit.KryptonWrapLabel();
            this.SuspendLayout();
            // 
            // kwlblFormResizeData
            // 
            this.kwlblFormResizeData.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kwlblFormResizeData.Location = new System.Drawing.Point(12, 9);
            this.kwlblFormResizeData.Name = "kwlblFormResizeData";
            this.kwlblFormResizeData.Size = new System.Drawing.Size(110, 15);
            this.kwlblFormResizeData.Text = "kryptonWrapLabel1";
            // 
            // Bug3013TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.kwlblFormResizeData);
            this.Name = "Bug3013TestForm";
            this.Text = "Bug3013TestForm";
            this.Resize += new System.EventHandler(this.Bug3013TestForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KryptonWrapLabel kwlblFormResizeData;
    }
}
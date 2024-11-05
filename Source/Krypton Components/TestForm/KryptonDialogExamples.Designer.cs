namespace TestForm
{
    partial class KryptonDialogExamples
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
            this.kbtnColorDialog = new Krypton.Toolkit.KryptonButton();
            this.kbtnPrintDialog = new Krypton.Toolkit.KryptonButton();
            this.kbtnFontDialog = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnFontDialog);
            this.kryptonPanel1.Controls.Add(this.kbtnPrintDialog);
            this.kryptonPanel1.Controls.Add(this.kbtnColorDialog);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(230, 114);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnColorDialog
            // 
            this.kbtnColorDialog.Location = new System.Drawing.Point(13, 13);
            this.kbtnColorDialog.Name = "kbtnColorDialog";
            this.kbtnColorDialog.Size = new System.Drawing.Size(201, 25);
            this.kbtnColorDialog.TabIndex = 0;
            this.kbtnColorDialog.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnColorDialog.Values.Text = "Color Dialog";
            this.kbtnColorDialog.Click += new System.EventHandler(this.kbtnColorDialog_Click);
            // 
            // kbtnPrintDialog
            // 
            this.kbtnPrintDialog.Location = new System.Drawing.Point(13, 75);
            this.kbtnPrintDialog.Name = "kbtnPrintDialog";
            this.kbtnPrintDialog.Size = new System.Drawing.Size(201, 25);
            this.kbtnPrintDialog.TabIndex = 1;
            this.kbtnPrintDialog.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnPrintDialog.Values.Text = "Print Dialog";
            this.kbtnPrintDialog.Click += new System.EventHandler(this.kbtnPrintDialog_Click);
            // 
            // kbtnFontDialog
            // 
            this.kbtnFontDialog.Location = new System.Drawing.Point(13, 44);
            this.kbtnFontDialog.Name = "kbtnFontDialog";
            this.kbtnFontDialog.Size = new System.Drawing.Size(201, 25);
            this.kbtnFontDialog.TabIndex = 2;
            this.kbtnFontDialog.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnFontDialog.Values.Text = "Font Dialog";
            this.kbtnFontDialog.Click += new System.EventHandler(this.kbtnFontDialog_Click);
            // 
            // KryptonDialogExamples
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 114);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "KryptonDialogExamples";
            this.Text = "KryptonDialogExamples";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonButton kbtnColorDialog;
        private KryptonButton kbtnFontDialog;
        private KryptonButton kbtnPrintDialog;
    }
}
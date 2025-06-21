namespace TestForm
{
    partial class PoweredByButtonForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PoweredByButtonForm));
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonPropertyGrid1 = new Krypton.Toolkit.KryptonPropertyGrid();
            this.kryptonPoweredByButton1 = new Krypton.Toolkit.KryptonPoweredByButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonButton1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 364);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(590, 50);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.kryptonButton1.Location = new System.Drawing.Point(482, 17);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(90, 25);
            this.kryptonButton1.TabIndex = 0;
            this.kryptonButton1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton1.Values.Text = "O&K";
            this.kryptonButton1.Values.UseAsADialogButton = true;
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.kryptonPropertyGrid1);
            this.kryptonPanel2.Controls.Add(this.kryptonPoweredByButton1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(590, 364);
            this.kryptonPanel2.TabIndex = 1;
            // 
            // kryptonPropertyGrid1
            // 
            this.kryptonPropertyGrid1.Location = new System.Drawing.Point(265, 13);
            this.kryptonPropertyGrid1.Name = "kryptonPropertyGrid1";
            this.kryptonPropertyGrid1.Padding = new System.Windows.Forms.Padding(1);
            this.kryptonPropertyGrid1.SelectedObject = this.kryptonPoweredByButton1;
            this.kryptonPropertyGrid1.Size = new System.Drawing.Size(296, 341);
            this.kryptonPropertyGrid1.TabIndex = 1;
            this.kryptonPropertyGrid1.Text = "kryptonPropertyGrid1";
            // 
            // kryptonPoweredByButton1
            // 
            this.kryptonPoweredByButton1.Location = new System.Drawing.Point(13, 90);
            this.kryptonPoweredByButton1.Name = "kryptonPoweredByButton1";
            this.kryptonPoweredByButton1.Size = new System.Drawing.Size(153, 25);
            this.kryptonPoweredByButton1.TabIndex = 0;
            this.kryptonPoweredByButton1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonPoweredByButton1.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonPoweredByButton1.Values.Image")));
            this.kryptonPoweredByButton1.Values.Text = "&Powered By Krypton";
            // 
            // PoweredByButtonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 414);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "PoweredByButtonForm";
            this.Text = "PoweredByButtonForm";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonButton kryptonButton1;
        private KryptonPanel kryptonPanel2;
        private KryptonPropertyGrid kryptonPropertyGrid1;
        private KryptonPoweredByButton kryptonPoweredByButton1;
    }
}
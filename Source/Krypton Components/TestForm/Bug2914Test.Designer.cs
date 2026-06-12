namespace TestForm
{
    partial class Bug2914Test
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
			this.components = new System.ComponentModel.Container();
			this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
			this.kryptonButton2 = new Krypton.Toolkit.KryptonButton();
			this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
			this.kryptonButton3 = new Krypton.Toolkit.KryptonButton();
			this.kryptonManager1 = new Krypton.Toolkit.KryptonManager(this.components);
			((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
			this.kryptonPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// kryptonButton1
			// 
			this.kryptonButton1.Location = new System.Drawing.Point(43, 39);
			this.kryptonButton1.Name = "kryptonButton1";
			this.kryptonButton1.Size = new System.Drawing.Size(353, 77);
			this.kryptonButton1.TabIndex = 1;
			this.kryptonButton1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
			this.kryptonButton1.Values.Text = "Open same Form without ControlBox in a Modal";
			this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
			// 
			// kryptonButton2
			// 
			this.kryptonButton2.Location = new System.Drawing.Point(561, 367);
			this.kryptonButton2.Name = "kryptonButton2";
			this.kryptonButton2.Size = new System.Drawing.Size(243, 52);
			this.kryptonButton2.TabIndex = 1;
			this.kryptonButton2.Values.DropDownArrowColor = System.Drawing.Color.Empty;
			this.kryptonButton2.Values.Text = "Close Form";
			this.kryptonButton2.Click += new System.EventHandler(this.kryptonButton2_Click);
			// 
			// kryptonPanel1
			// 
			this.kryptonPanel1.Controls.Add(this.kryptonButton3);
			this.kryptonPanel1.Controls.Add(this.kryptonButton1);
			this.kryptonPanel1.Controls.Add(this.kryptonButton2);
			this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
			this.kryptonPanel1.Name = "kryptonPanel1";
			this.kryptonPanel1.Size = new System.Drawing.Size(934, 450);
			this.kryptonPanel1.TabIndex = 3;
			// 
			// kryptonButton3
			// 
			this.kryptonButton3.Location = new System.Drawing.Point(463, 39);
			this.kryptonButton3.Name = "kryptonButton3";
			this.kryptonButton3.Size = new System.Drawing.Size(353, 77);
			this.kryptonButton3.TabIndex = 1;
			this.kryptonButton3.Values.DropDownArrowColor = System.Drawing.Color.Empty;
			this.kryptonButton3.Values.Text = "Open same Form in a Modal";
			this.kryptonButton3.Click += new System.EventHandler(this.kryptonButton3_Click);
			// 
			// kryptonManager1
			// 
			this.kryptonManager1.ShowAdministratorSuffix = false;
			this.kryptonManager1.ToolkitStrings.MessageBoxStrings.LessDetails = "L&ess Details...";
			this.kryptonManager1.ToolkitStrings.MessageBoxStrings.MoreDetails = "&More Details...";
			this.kryptonManager1.ToolkitStrings.PrintPreviewDialogStrings.PaginationPartOneText = "of";
			this.kryptonManager1.ToolkitStrings.PrintPreviewDialogStrings.PaginationPartTwoText = null;
			this.kryptonManager1.ToolkitStrings.PrintPreviewDialogStrings.ZoomInButtonText = "Zoom &Out";
			this.kryptonManager1.ToolkitStrings.PrintPreviewDialogStrings.ZoomOutButtonText = null;
			// 
			// Bug2914Test
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(934, 450);
			this.Controls.Add(this.kryptonPanel1);
			this.Name = "Bug2914Test";
			this.Text = "Bug 2914 Test";
			this.Load += new System.EventHandler(this.Bug2914Test_Load);
			((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
			this.kryptonPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

		#endregion

		private KryptonButton kryptonButton1;
		private KryptonButton kryptonButton2;
		private KryptonPanel kryptonPanel1;
		private KryptonManager kryptonManager1;
		private KryptonButton kryptonButton3;
	}
}
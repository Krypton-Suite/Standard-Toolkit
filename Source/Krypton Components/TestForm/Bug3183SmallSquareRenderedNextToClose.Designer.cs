namespace TestForm
{
	partial class Bug3183SmallSquareRenderedNextToClose
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
			this.kryptonManager1 = new Krypton.Toolkit.KryptonManager(this.components);
			this.SuspendLayout();
			// 
			// kryptonButton1
			// 
			this.kryptonButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.kryptonButton1.Location = new System.Drawing.Point(465, 45);
			this.kryptonButton1.Name = "kryptonButton1";
			this.kryptonButton1.Size = new System.Drawing.Size(290, 90);
			this.kryptonButton1.TabIndex = 2;
			this.kryptonButton1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
			this.kryptonButton1.Values.Text = "Apply Pink Theme";
			this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
			// 
			// kryptonManager1
			// 
			this.kryptonManager1.ToolkitStrings.MessageBoxStrings.LessDetails = "L&ess Details...";
			this.kryptonManager1.ToolkitStrings.MessageBoxStrings.MoreDetails = "&More Details...";
			this.kryptonManager1.ToolkitStrings.PrintPreviewDialogStrings.PaginationPartOneText = "of";
			this.kryptonManager1.ToolkitStrings.PrintPreviewDialogStrings.PaginationPartTwoText = null;
			this.kryptonManager1.ToolkitStrings.PrintPreviewDialogStrings.ZoomInButtonText = "Zoom &Out";
			this.kryptonManager1.ToolkitStrings.PrintPreviewDialogStrings.ZoomOutButtonText = null;
			// 
			// Bug3183SmallSquareRenderedNextToClose
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.kryptonButton1);
			this.Name = "Bug3183SmallSquareRenderedNextToClose";
			this.Text = "Small square rendered next to Close button on KryptonForm when using a custom the" +
    "me (Issue #3183";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.Bug3183SmallSquareRenderedNextToClose_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private KryptonButton kryptonButton1;
		private KryptonManager kryptonManager1;
	}
}
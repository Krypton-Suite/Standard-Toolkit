namespace TestForm
{
	partial class Bug3203QATLocationHiddenFormTest
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
			this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
			this.kryptonRibbon1 = new Krypton.Ribbon.KryptonRibbon();
			this.kryptonRibbonQATButton1 = new Krypton.Ribbon.KryptonRibbonQATButton();
			this.kryptonRibbonTab1 = new Krypton.Ribbon.KryptonRibbonTab();
			this.kryptonRibbonGroup1 = new Krypton.Ribbon.KryptonRibbonGroup();
			this.kryptonRibbonGroupTriple1 = new Krypton.Ribbon.KryptonRibbonGroupTriple();
			this.kryptonRibbonGroupButton1 = new Krypton.Ribbon.KryptonRibbonGroupButton();
			this.kryptonRibbonGroupButton2 = new Krypton.Ribbon.KryptonRibbonGroupButton();
			this.kryptonRibbonGroupButton3 = new Krypton.Ribbon.KryptonRibbonGroupButton();
			this.btnHidden = new Krypton.Toolkit.KryptonButton();
			this.btnBelow = new Krypton.Toolkit.KryptonButton();
			this.btnAbove = new Krypton.Toolkit.KryptonButton();
			this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
			((System.ComponentModel.ISupportInitialize)(this.kryptonRibbon1)).BeginInit();
			this.SuspendLayout();
			// 
			// kryptonLabel1
			// 
			this.kryptonLabel1.Location = new System.Drawing.Point(270, 216);
			this.kryptonLabel1.Name = "kryptonLabel1";
			this.kryptonLabel1.Size = new System.Drawing.Size(88, 20);
			this.kryptonLabel1.TabIndex = 2;
			this.kryptonLabel1.Values.Text = "kryptonLabel1";
			// 
			// kryptonRibbon1
			// 
			this.kryptonRibbon1.Name = "kryptonRibbon1";
			this.kryptonRibbon1.NotificationBar.ActionButtonTexts = new string[] {
        "Update now"};
			this.kryptonRibbon1.QATButtons.AddRange(new System.ComponentModel.Component[] {
            this.kryptonRibbonQATButton1});
			this.kryptonRibbon1.QATLocation = Krypton.Ribbon.QATLocation.Hidden;
			this.kryptonRibbon1.RibbonFileAppButton.AppButtonToolTipStyle = Krypton.Toolkit.LabelStyle.SuperTip;
			this.kryptonRibbon1.RibbonFileAppButton.AppButtonVisible = false;
			this.kryptonRibbon1.RibbonTabs.AddRange(new Krypton.Ribbon.KryptonRibbonTab[] {
            this.kryptonRibbonTab1});
			this.kryptonRibbon1.SelectedTab = this.kryptonRibbonTab1;
			this.kryptonRibbon1.Size = new System.Drawing.Size(800, 115);
			this.kryptonRibbon1.TabIndex = 0;
			// 
			// kryptonRibbonQATButton1
			// 
			this.kryptonRibbonQATButton1.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
			// 
			// kryptonRibbonTab1
			// 
			this.kryptonRibbonTab1.Groups.AddRange(new Krypton.Ribbon.KryptonRibbonGroup[] {
            this.kryptonRibbonGroup1});
			// 
			// kryptonRibbonGroup1
			// 
			this.kryptonRibbonGroup1.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple1});
			// 
			// kryptonRibbonGroupTriple1
			// 
			this.kryptonRibbonGroupTriple1.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupButton1,
            this.kryptonRibbonGroupButton2,
            this.kryptonRibbonGroupButton3});
			// 
			// btnHidden
			// 
			this.btnHidden.Location = new System.Drawing.Point(127, 242);
			this.btnHidden.Name = "btnHidden";
			this.btnHidden.Size = new System.Drawing.Size(112, 23);
			this.btnHidden.TabIndex = 4;
			this.btnHidden.Values.DropDownArrowColor = System.Drawing.Color.Empty;
			this.btnHidden.Values.Text = "Hidden";
			this.btnHidden.Click += new System.EventHandler(this.btnHidden_Click);
			// 
			// btnBelow
			// 
			this.btnBelow.Location = new System.Drawing.Point(127, 213);
			this.btnBelow.Name = "btnBelow";
			this.btnBelow.Size = new System.Drawing.Size(112, 23);
			this.btnBelow.TabIndex = 4;
			this.btnBelow.Values.DropDownArrowColor = System.Drawing.Color.Empty;
			this.btnBelow.Values.Text = "Below";
			this.btnBelow.Click += new System.EventHandler(this.btnBelow_Click);
			// 
			// btnAbove
			// 
			this.btnAbove.Location = new System.Drawing.Point(127, 184);
			this.btnAbove.Name = "btnAbove";
			this.btnAbove.Size = new System.Drawing.Size(112, 23);
			this.btnAbove.TabIndex = 4;
			this.btnAbove.Values.DropDownArrowColor = System.Drawing.Color.Empty;
			this.btnAbove.Values.Text = "Above";
			this.btnAbove.Click += new System.EventHandler(this.btnAbove_Click);
			// 
			// kryptonLabel2
			// 
			this.kryptonLabel2.Location = new System.Drawing.Point(32, 158);
			this.kryptonLabel2.Name = "kryptonLabel2";
			this.kryptonLabel2.Size = new System.Drawing.Size(171, 20);
			this.kryptonLabel2.TabIndex = 6;
			this.kryptonLabel2.Values.Text = "kryptonRibbon1.QATLocation";
			// 
			// Bug3203QATLocationHiddenFormTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.CloseBox = false;
			this.ControlBox = false;
			this.Controls.Add(this.kryptonLabel2);
			this.Controls.Add(this.btnAbove);
			this.Controls.Add(this.btnBelow);
			this.Controls.Add(this.btnHidden);
			this.Controls.Add(this.kryptonLabel1);
			this.Controls.Add(this.kryptonRibbon1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Bug3203QATLocationHiddenFormTest";
			this.StateCommon.Border.Draw = Krypton.Toolkit.InheritBool.False;
			this.Load += new System.EventHandler(this.Bug3203QATLocationHiddenFormTest_Load);
			((System.ComponentModel.ISupportInitialize)(this.kryptonRibbon1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Krypton.Ribbon.KryptonRibbon kryptonRibbon1;
		private Krypton.Ribbon.KryptonRibbonTab kryptonRibbonTab1;
		private Krypton.Ribbon.KryptonRibbonGroup kryptonRibbonGroup1;
		private Krypton.Ribbon.KryptonRibbonGroupTriple kryptonRibbonGroupTriple1;
		private Krypton.Ribbon.KryptonRibbonGroupButton kryptonRibbonGroupButton1;
		private Krypton.Ribbon.KryptonRibbonGroupButton kryptonRibbonGroupButton2;
		private Krypton.Ribbon.KryptonRibbonGroupButton kryptonRibbonGroupButton3;
		private KryptonLabel kryptonLabel1;
		private Krypton.Ribbon.KryptonRibbonQATButton kryptonRibbonQATButton1;
		private KryptonButton btnHidden;
		private KryptonButton btnBelow;
		private KryptonButton btnAbove;
		private KryptonLabel kryptonLabel2;
	}
}
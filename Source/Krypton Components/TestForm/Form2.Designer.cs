namespace TestForm
{
    partial class Form2
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
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.kryptonBreadCrumb1 = new Krypton.Toolkit.KryptonBreadCrumb();
            this.kryptonBreadCrumbItem1 = new Krypton.Toolkit.KryptonBreadCrumbItem();
            this.kryptonBreadCrumbItem2 = new Krypton.Toolkit.KryptonBreadCrumbItem();
            this.kryptonBreadCrumbItem3 = new Krypton.Toolkit.KryptonBreadCrumbItem();
            this.kryptonBreadCrumbItem4 = new Krypton.Toolkit.KryptonBreadCrumbItem();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonBreadCrumb1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonButton1);
            this.kryptonPanel1.Controls.Add(this.kryptonBreadCrumb1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(1096, 624);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(220, 13);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(90, 25);
            this.kryptonButton1.TabIndex = 1;
            this.kryptonButton1.Values.Text = "kryptonButton1";
            // 
            // kryptonBreadCrumb1
            // 
            this.kryptonBreadCrumb1.AutoSize = false;
            this.kryptonBreadCrumb1.Location = new System.Drawing.Point(13, 13);
            this.kryptonBreadCrumb1.Name = "kryptonBreadCrumb1";
            // 
            // 
            // 
            this.kryptonBreadCrumb1.RootItem.ShortText = "Root";
            this.kryptonBreadCrumb1.SelectedItem = this.kryptonBreadCrumb1.RootItem;
            this.kryptonBreadCrumb1.Size = new System.Drawing.Size(200, 28);
            this.kryptonBreadCrumb1.TabIndex = 0;
            // 
            // kryptonBreadCrumbItem1
            // 
            this.kryptonBreadCrumbItem1.Items.AddRange(new Krypton.Toolkit.KryptonBreadCrumbItem[] {
            this.kryptonBreadCrumbItem2});
            this.kryptonBreadCrumbItem1.ShortText = "ListItem";
            // 
            // kryptonBreadCrumbItem2
            // 
            this.kryptonBreadCrumbItem2.Items.AddRange(new Krypton.Toolkit.KryptonBreadCrumbItem[] {
            this.kryptonBreadCrumbItem3});
            this.kryptonBreadCrumbItem2.ShortText = "ListItem";
            // 
            // kryptonBreadCrumbItem3
            // 
            this.kryptonBreadCrumbItem3.Items.AddRange(new Krypton.Toolkit.KryptonBreadCrumbItem[] {
            this.kryptonBreadCrumbItem4});
            this.kryptonBreadCrumbItem3.ShortText = "ListItem";
            // 
            // kryptonBreadCrumbItem4
            // 
            this.kryptonBreadCrumbItem4.ShortText = "ListItem";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 624);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "Form2";
            this.ShowIntegratedToolBar = false;
            this.Text = "Form2";
            this.Controls.SetChildIndex(this.kryptonPanel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonBreadCrumb1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonBreadCrumbItem kryptonBreadCrumbItem1;
        private Krypton.Toolkit.KryptonBreadCrumbItem kryptonBreadCrumbItem2;
        private Krypton.Toolkit.KryptonBreadCrumbItem kryptonBreadCrumbItem3;
        private Krypton.Toolkit.KryptonBreadCrumbItem kryptonBreadCrumbItem4;
        private Krypton.Toolkit.KryptonBreadCrumb kryptonBreadCrumb1;
        private Krypton.Toolkit.KryptonButton kryptonButton1;
    }
}
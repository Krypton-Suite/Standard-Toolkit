namespace Krypton.Toolkit
{
    partial class KryptonOutlookGridFilterItemGroup
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.MainLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.GroupFilter = new System.Windows.Forms.Label();
            this.FilterItems = new System.Windows.Forms.FlowLayoutPanel();
            this.FlowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.FilterGroupMenu = new KryptonOutlookGridFilterItemGroupMenuButton();
            this.GroupBox1.SuspendLayout();
            this.MainLayoutPanel.SuspendLayout();
            this.FlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.GroupBox1.AutoSize = true;
            this.GroupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GroupBox1.Controls.Add(this.MainLayoutPanel);
            this.GroupBox1.ForeColor = System.Drawing.Color.Blue;
            this.GroupBox1.Location = new System.Drawing.Point(3, 3);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(188, 38);
            this.GroupBox1.TabIndex = 0;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "GroupBox1";
            // 
            // MainLayoutPanel
            // 
            this.MainLayoutPanel.AutoSize = true;
            this.MainLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MainLayoutPanel.Controls.Add(this.GroupFilter);
            this.MainLayoutPanel.Controls.Add(this.FilterItems);
            this.MainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.MainLayoutPanel.ForeColor = System.Drawing.Color.Black;
            this.MainLayoutPanel.Location = new System.Drawing.Point(3, 16);
            this.MainLayoutPanel.MaximumSize = new System.Drawing.Size(1000, 0);
            this.MainLayoutPanel.Name = "MainLayoutPanel";
            this.MainLayoutPanel.Size = new System.Drawing.Size(182, 19);
            this.MainLayoutPanel.TabIndex = 0;
            this.MainLayoutPanel.WrapContents = false;
            // 
            // GroupFilter
            // 
            this.GroupFilter.AutoSize = true;
            this.MainLayoutPanel.SetFlowBreak(this.GroupFilter, true);
            this.GroupFilter.Location = new System.Drawing.Point(3, 0);
            this.GroupFilter.MaximumSize = new System.Drawing.Size(1000, 0);
            this.GroupFilter.Name = "GroupFilter";
            this.GroupFilter.Size = new System.Drawing.Size(176, 13);
            this.GroupFilter.TabIndex = 1;
            this.GroupFilter.Text = "This is where the Group Filter will go";
            // 
            // FilterItems
            // 
            this.FilterItems.AutoSize = true;
            this.FilterItems.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FilterItems.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.FilterItems.ForeColor = System.Drawing.Color.Black;
            this.FilterItems.Location = new System.Drawing.Point(3, 16);
            this.FilterItems.Name = "FilterItems";
            this.FilterItems.Size = new System.Drawing.Size(0, 0);
            this.FilterItems.TabIndex = 2;
            this.FilterItems.WrapContents = false;
            // 
            // FlowLayoutPanel1
            // 
            this.FlowLayoutPanel1.AutoSize = true;
            this.FlowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FlowLayoutPanel1.Controls.Add(this.GroupBox1);
            this.FlowLayoutPanel1.Controls.Add(this.FilterGroupMenu);
            this.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.FlowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.FlowLayoutPanel1.Name = "FlowLayoutPanel1";
            this.FlowLayoutPanel1.Size = new System.Drawing.Size(194, 72);
            this.FlowLayoutPanel1.TabIndex = 2;
            // 
            // FilterGroupMenu
            // 
            this.FilterGroupMenu.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.FilterGroupMenu.AutoSize = true;
            this.FilterGroupMenu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FilterGroupMenu.Location = new System.Drawing.Point(68, 47);
            this.FilterGroupMenu.Name = "FilterGroupMenu";
            this.FilterGroupMenu.Size = new System.Drawing.Size(57, 22);
            this.FilterGroupMenu.Splitter = false;
            this.FilterGroupMenu.TabIndex = 2;
            // 
            // FilterItemGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.FlowLayoutPanel1);
            this.Name = "FilterItemGroup";
            this.Size = new System.Drawing.Size(194, 72);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.MainLayoutPanel.ResumeLayout(false);
            this.MainLayoutPanel.PerformLayout();
            this.FlowLayoutPanel1.ResumeLayout(false);
            this.FlowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal System.Windows.Forms.GroupBox GroupBox1;

        internal System.Windows.Forms.FlowLayoutPanel MainLayoutPanel;

        internal System.Windows.Forms.Label GroupFilter;

        internal System.Windows.Forms.FlowLayoutPanel FilterItems;

        internal System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel1;

        internal KryptonOutlookGridFilterItemGroupMenuButton FilterGroupMenu;


        #endregion
    }
}

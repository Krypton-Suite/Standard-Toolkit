#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace TestForm
{
    partial class BreadCrumbTest
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
            this.kryptonNavigator1 = new Krypton.Navigator.KryptonNavigator();
            this.kryptonPage1 = new Krypton.Navigator.KryptonPage();
            this.kryptonPage2 = new Krypton.Navigator.KryptonPage();
            this.kryptonSplitContainer1 = new Krypton.Toolkit.KryptonSplitContainer();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.kryptonBreadCrumb1 = new Krypton.Toolkit.KryptonBreadCrumb();
            this.kryptonBreadCrumbItem1 = new Krypton.Toolkit.KryptonBreadCrumbItem();
            this.kryptonBreadCrumbItem2 = new Krypton.Toolkit.KryptonBreadCrumbItem();
            this.kryptonBreadCrumbItem3 = new Krypton.Toolkit.KryptonBreadCrumbItem();
            this.kryptonBreadCrumbItem4 = new Krypton.Toolkit.KryptonBreadCrumbItem();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).BeginInit();
            this.kryptonNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel2)).BeginInit();
            this.kryptonSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonBreadCrumb1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonNavigator1);
            this.kryptonPanel1.Controls.Add(this.kryptonSplitContainer1);
            this.kryptonPanel1.Controls.Add(this.kryptonButton1);
            this.kryptonPanel1.Controls.Add(this.kryptonBreadCrumb1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(1104, 624);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonNavigator1
            // 
            this.kryptonNavigator1.ControlKryptonFormFeatures = false;
            this.kryptonNavigator1.Location = new System.Drawing.Point(426, 13);
            this.kryptonNavigator1.NavigatorMode = Krypton.Navigator.NavigatorMode.BarTabGroup;
            this.kryptonNavigator1.Owner = null;
            this.kryptonNavigator1.PageBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kryptonNavigator1.Pages.AddRange(new Krypton.Navigator.KryptonPage[] {
            this.kryptonPage1,
            this.kryptonPage2});
            this.kryptonNavigator1.SelectedIndex = 0;
            this.kryptonNavigator1.Size = new System.Drawing.Size(658, 265);
            this.kryptonNavigator1.TabIndex = 3;
            this.kryptonNavigator1.Text = "kryptonNavigator1";
            // 
            // kryptonPage1
            // 
            this.kryptonPage1.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage1.Flags = 65534;
            this.kryptonPage1.LastVisibleSet = true;
            this.kryptonPage1.MinimumSize = new System.Drawing.Size(150, 50);
            this.kryptonPage1.Name = "kryptonPage1";
            this.kryptonPage1.Size = new System.Drawing.Size(656, 238);
            this.kryptonPage1.Text = "kryptonPage1";
            this.kryptonPage1.ToolTipTitle = "Page ToolTip";
            this.kryptonPage1.UniqueName = "e1a692ea0e18480aa2ed26f6e2ee01ff";
            // 
            // kryptonPage2
            // 
            this.kryptonPage2.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage2.Flags = 65534;
            this.kryptonPage2.LastVisibleSet = true;
            this.kryptonPage2.MinimumSize = new System.Drawing.Size(150, 50);
            this.kryptonPage2.Name = "kryptonPage2";
            this.kryptonPage2.Size = new System.Drawing.Size(150, 100);
            this.kryptonPage2.Text = "kryptonPage2";
            this.kryptonPage2.ToolTipTitle = "Page ToolTip";
            this.kryptonPage2.UniqueName = "c71fb1fbd12b4734b8303a9182540eca";
            // 
            // kryptonSplitContainer1
            // 
            this.kryptonSplitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.kryptonSplitContainer1.Location = new System.Drawing.Point(13, 58);
            this.kryptonSplitContainer1.Size = new System.Drawing.Size(359, 150);
            this.kryptonSplitContainer1.SplitterDistance = 119;
            this.kryptonSplitContainer1.TabIndex = 2;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(220, 13);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(90, 25);
            this.kryptonButton1.TabIndex = 1;
            this.kryptonButton1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
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
            // BreadCrumbTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 624);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "BreadCrumbTest";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).EndInit();
            this.kryptonNavigator1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).EndInit();
            this.kryptonSplitContainer1.ResumeLayout(false);
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
        private Krypton.Toolkit.KryptonSplitContainer kryptonSplitContainer1;
        private Krypton.Navigator.KryptonNavigator kryptonNavigator1;
        private Krypton.Navigator.KryptonPage kryptonPage1;
        private Krypton.Navigator.KryptonPage kryptonPage2;
    }
}
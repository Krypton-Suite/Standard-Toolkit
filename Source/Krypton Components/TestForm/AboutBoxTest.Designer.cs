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
    partial class AboutBoxTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBoxTest));
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnShow = new Krypton.Toolkit.KryptonButton();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonTextBox4 = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonTextBox3 = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonTextBox2 = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonTextBox1 = new Krypton.Toolkit.KryptonTextBox();
            this.kchkUseRtlLayout = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkUseFullBuiltOnDate = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkShowToolkitInformation = new Krypton.Toolkit.KryptonCheckBox();
            this.kryptonLabel4 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonManager1 = new Krypton.Toolkit.KryptonManager(this.components);
            this.bsaAssemblyBrowse = new Krypton.Toolkit.ButtonSpecAny();
            this.bsaBrowseHeaderImage = new Krypton.Toolkit.ButtonSpecAny();
            this.bsaBrowseMainImage = new Krypton.Toolkit.ButtonSpecAny();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnShow);
            this.kryptonPanel1.Controls.Add(this.kryptonButton1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 212);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(800, 50);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnShow
            // 
            this.kbtnShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnShow.Location = new System.Drawing.Point(602, 13);
            this.kbtnShow.Name = "kbtnShow";
            this.kbtnShow.Size = new System.Drawing.Size(90, 25);
            this.kbtnShow.TabIndex = 1;
            this.kbtnShow.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnShow.Values.Text = "Show";
            this.kbtnShow.Click += new System.EventHandler(this.kbtnShow_Click);
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kryptonButton1.Location = new System.Drawing.Point(698, 13);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(90, 25);
            this.kryptonButton1.TabIndex = 0;
            this.kryptonButton1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton1.Values.Text = "Cance&l";
            this.kryptonButton1.Values.UseAsADialogButton = true;
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 211);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(800, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.kryptonTextBox4);
            this.kryptonPanel2.Controls.Add(this.kryptonTextBox3);
            this.kryptonPanel2.Controls.Add(this.kryptonTextBox2);
            this.kryptonPanel2.Controls.Add(this.kryptonTextBox1);
            this.kryptonPanel2.Controls.Add(this.kchkUseRtlLayout);
            this.kryptonPanel2.Controls.Add(this.kchkUseFullBuiltOnDate);
            this.kryptonPanel2.Controls.Add(this.kchkShowToolkitInformation);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(800, 211);
            this.kryptonPanel2.TabIndex = 2;
            // 
            // kryptonTextBox4
            // 
            this.kryptonTextBox4.ButtonSpecs.Add(this.bsaBrowseMainImage);
            this.kryptonTextBox4.Location = new System.Drawing.Point(139, 135);
            this.kryptonTextBox4.Name = "kryptonTextBox4";
            this.kryptonTextBox4.Size = new System.Drawing.Size(649, 26);
            this.kryptonTextBox4.TabIndex = 10;
            this.kryptonTextBox4.Text = "kryptonTextBox4";
            // 
            // kryptonTextBox3
            // 
            this.kryptonTextBox3.ButtonSpecs.Add(this.bsaBrowseHeaderImage);
            this.kryptonTextBox3.Location = new System.Drawing.Point(139, 98);
            this.kryptonTextBox3.Name = "kryptonTextBox3";
            this.kryptonTextBox3.Size = new System.Drawing.Size(649, 26);
            this.kryptonTextBox3.TabIndex = 9;
            this.kryptonTextBox3.Text = "kryptonTextBox3";
            // 
            // kryptonTextBox2
            // 
            this.kryptonTextBox2.Location = new System.Drawing.Point(139, 55);
            this.kryptonTextBox2.Name = "kryptonTextBox2";
            this.kryptonTextBox2.Size = new System.Drawing.Size(649, 23);
            this.kryptonTextBox2.TabIndex = 8;
            this.kryptonTextBox2.Text = "kryptonTextBox2";
            // 
            // kryptonTextBox1
            // 
            this.kryptonTextBox1.ButtonSpecs.Add(this.bsaAssemblyBrowse);
            this.kryptonTextBox1.Location = new System.Drawing.Point(139, 13);
            this.kryptonTextBox1.Name = "kryptonTextBox1";
            this.kryptonTextBox1.Size = new System.Drawing.Size(649, 26);
            this.kryptonTextBox1.TabIndex = 7;
            this.kryptonTextBox1.Text = "kryptonTextBox1";
            // 
            // kchkUseRtlLayout
            // 
            this.kchkUseRtlLayout.Location = new System.Drawing.Point(328, 174);
            this.kchkUseRtlLayout.Name = "kchkUseRtlLayout";
            this.kchkUseRtlLayout.Size = new System.Drawing.Size(102, 22);
            this.kchkUseRtlLayout.TabIndex = 6;
            this.kchkUseRtlLayout.Values.Text = "Use Rtl Layout";
            // 
            // kchkUseFullBuiltOnDate
            // 
            this.kchkUseFullBuiltOnDate.Location = new System.Drawing.Point(181, 174);
            this.kchkUseFullBuiltOnDate.Name = "kchkUseFullBuiltOnDate";
            this.kchkUseFullBuiltOnDate.Size = new System.Drawing.Size(140, 22);
            this.kchkUseFullBuiltOnDate.TabIndex = 5;
            this.kchkUseFullBuiltOnDate.Values.Text = "Use Full Built on Date";
            // 
            // kchkShowToolkitInformation
            // 
            this.kchkShowToolkitInformation.Location = new System.Drawing.Point(13, 174);
            this.kchkShowToolkitInformation.Name = "kchkShowToolkitInformation";
            this.kchkShowToolkitInformation.Size = new System.Drawing.Size(161, 22);
            this.kchkShowToolkitInformation.TabIndex = 4;
            this.kchkShowToolkitInformation.Values.Text = "Show Toolkit Information";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.kryptonLabel4.Location = new System.Drawing.Point(13, 98);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(92, 22);
            this.kryptonLabel4.TabIndex = 3;
            this.kryptonLabel4.Values.Text = "Header Image";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.kryptonLabel3.Location = new System.Drawing.Point(13, 135);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(83, 22);
            this.kryptonLabel3.TabIndex = 2;
            this.kryptonLabel3.Values.Text = "Main Image:";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.kryptonLabel2.Location = new System.Drawing.Point(13, 55);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(119, 22);
            this.kryptonLabel2.TabIndex = 1;
            this.kryptonLabel2.Values.Text = "Application Name:";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.kryptonLabel1.Location = new System.Drawing.Point(13, 13);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(69, 22);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Assembly:";
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.GlobalPaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            // 
            // bsaAssemblyBrowse
            // 
            this.bsaAssemblyBrowse.Text = ".&..";
            this.bsaAssemblyBrowse.UniqueName = "8b78c231307c42499bad24c53a26eeb2";
            this.bsaAssemblyBrowse.Click += new System.EventHandler(this.bsaAssemblyBrowse_Click);
            // 
            // bsaBrowseHeaderImage
            // 
            this.bsaBrowseHeaderImage.Text = ".&..";
            this.bsaBrowseHeaderImage.UniqueName = "ba7f76ebadf64157adeab2b8fc7658f3";
            this.bsaBrowseHeaderImage.Click += new System.EventHandler(this.bsaBrowseHeaderImage_Click);
            // 
            // bsaBrowseMainImage
            // 
            this.bsaBrowseMainImage.Text = ".&..";
            this.bsaBrowseMainImage.UniqueName = "0773a389882641feb79629815f46d5e5";
            this.bsaBrowseMainImage.Click += new System.EventHandler(this.bsaBrowseMainImage_Click);
            // 
            // AboutBoxTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 262);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonBorderEdge1);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "AboutBoxTest";
            this.Text = "AboutBoxTest";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonButton kryptonButton1;
        private KryptonButton kbtnShow;
        private KryptonPanel kryptonPanel2;
        private KryptonManager kryptonManager1;
        private KryptonLabel kryptonLabel1;
        private KryptonLabel kryptonLabel2;
        private KryptonLabel kryptonLabel4;
        private KryptonLabel kryptonLabel3;
        private KryptonCheckBox kchkShowToolkitInformation;
        private KryptonCheckBox kchkUseFullBuiltOnDate;
        private KryptonCheckBox kchkUseRtlLayout;
        private KryptonTextBox kryptonTextBox4;
        private KryptonTextBox kryptonTextBox3;
        private KryptonTextBox kryptonTextBox2;
        private KryptonTextBox kryptonTextBox1;
        private ButtonSpecAny bsaAssemblyBrowse;
        private ButtonSpecAny bsaBrowseHeaderImage;
        private ButtonSpecAny bsaBrowseMainImage;
    }
}
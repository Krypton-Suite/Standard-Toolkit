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
    partial class TreeViewExample
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeViewExample));
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnClose = new Krypton.Toolkit.KryptonButton();
            this.buttonAppend = new Krypton.Toolkit.KryptonButton();
            this.buttonInsert = new Krypton.Toolkit.KryptonButton();
            this.buttonClear = new Krypton.Toolkit.KryptonButton();
            this.buttonRemove = new Krypton.Toolkit.KryptonButton();
            this.kbtnToggleNodeCheckBox = new Krypton.Toolkit.KryptonButton();
            this.kryptonPropertyGrid1 = new Krypton.Toolkit.KryptonPropertyGrid();
            this.ktvTest = new Krypton.Toolkit.KryptonTreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.kryptonCustomPaletteBase1 = new Krypton.Toolkit.KryptonCustomPaletteBase(this.components);
            this.kryptonManager1 = new Krypton.Toolkit.KryptonManager(this.components);
            this.kbtnTreeViewTest = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnTreeViewTest);
            this.kryptonPanel1.Controls.Add(this.kbtnClose);
            this.kryptonPanel1.Controls.Add(this.buttonAppend);
            this.kryptonPanel1.Controls.Add(this.buttonInsert);
            this.kryptonPanel1.Controls.Add(this.buttonClear);
            this.kryptonPanel1.Controls.Add(this.buttonRemove);
            this.kryptonPanel1.Controls.Add(this.kbtnToggleNodeCheckBox);
            this.kryptonPanel1.Controls.Add(this.kryptonPropertyGrid1);
            this.kryptonPanel1.Controls.Add(this.kryptonThemeComboBox1);
            this.kryptonPanel1.Controls.Add(this.ktvTest);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(742, 629);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnClose
            // 
            this.kbtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnClose.Location = new System.Drawing.Point(596, 595);
            this.kbtnClose.Name = "kbtnClose";
            this.kbtnClose.Size = new System.Drawing.Size(138, 25);
            this.kbtnClose.TabIndex = 8;
            this.kbtnClose.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnClose.Values.Text = "Close";
            this.kbtnClose.Click += new System.EventHandler(this.kbtnClose_Click);
            // 
            // buttonAppend
            // 
            this.buttonAppend.AutoSize = true;
            this.buttonAppend.Location = new System.Drawing.Point(13, 492);
            this.buttonAppend.Name = "buttonAppend";
            this.buttonAppend.Size = new System.Drawing.Size(314, 28);
            this.buttonAppend.TabIndex = 4;
            this.buttonAppend.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonAppend.Values.Text = "Append";
            this.buttonAppend.Click += new System.EventHandler(this.buttonAppend_Click);
            // 
            // buttonInsert
            // 
            this.buttonInsert.AutoSize = true;
            this.buttonInsert.Location = new System.Drawing.Point(13, 524);
            this.buttonInsert.Name = "buttonInsert";
            this.buttonInsert.Size = new System.Drawing.Size(314, 28);
            this.buttonInsert.TabIndex = 5;
            this.buttonInsert.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonInsert.Values.Text = "Insert";
            this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.AutoSize = true;
            this.buttonClear.Location = new System.Drawing.Point(13, 588);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(314, 28);
            this.buttonClear.TabIndex = 7;
            this.buttonClear.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonClear.Values.Text = "Clear";
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.AutoSize = true;
            this.buttonRemove.Location = new System.Drawing.Point(13, 556);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(314, 28);
            this.buttonRemove.TabIndex = 6;
            this.buttonRemove.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonRemove.Values.Text = "Remove";
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // kbtnToggleNodeCheckBox
            // 
            this.kbtnToggleNodeCheckBox.Location = new System.Drawing.Point(13, 461);
            this.kbtnToggleNodeCheckBox.Name = "kbtnToggleNodeCheckBox";
            this.kbtnToggleNodeCheckBox.Size = new System.Drawing.Size(314, 25);
            this.kbtnToggleNodeCheckBox.TabIndex = 3;
            this.kbtnToggleNodeCheckBox.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnToggleNodeCheckBox.Values.Text = "Toggle Node CheckBox";
            this.kbtnToggleNodeCheckBox.Click += new System.EventHandler(this.kbtnToggleNodeCheckBox_Click);
            // 
            // kryptonPropertyGrid1
            // 
            this.kryptonPropertyGrid1.Location = new System.Drawing.Point(334, 41);
            this.kryptonPropertyGrid1.Name = "kryptonPropertyGrid1";
            this.kryptonPropertyGrid1.Padding = new System.Windows.Forms.Padding(1);
            this.kryptonPropertyGrid1.SelectedObject = this.ktvTest;
            this.kryptonPropertyGrid1.Size = new System.Drawing.Size(377, 413);
            this.kryptonPropertyGrid1.TabIndex = 2;
            // 
            // ktvTest
            // 
            this.ktvTest.CheckBoxes = true;
            this.ktvTest.ImageIndex = 0;
            this.ktvTest.ImageList = this.imageList;
            this.ktvTest.Location = new System.Drawing.Point(13, 13);
            this.ktvTest.MultiSelect = true;
            this.ktvTest.Name = "ktvTest";
            this.ktvTest.SelectedImageIndex = 0;
            this.ktvTest.Size = new System.Drawing.Size(314, 441);
            this.ktvTest.TabIndex = 0;
            this.ktvTest.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.ktvTest_BeforeCheck);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "flag_bahamas.png");
            this.imageList.Images.SetKeyName(1, "flag_china.png");
            this.imageList.Images.SetKeyName(2, "flag_ecuador.png");
            this.imageList.Images.SetKeyName(3, "flag_england.png");
            this.imageList.Images.SetKeyName(4, "flag_france.png");
            this.imageList.Images.SetKeyName(5, "flag_greece.png");
            this.imageList.Images.SetKeyName(6, "flag_netherlands.png");
            this.imageList.Images.SetKeyName(7, "flag_poland.png");
            // 
            // kryptonThemeComboBox1
            // 
            this.kryptonThemeComboBox1.DefaultPalette = Krypton.Toolkit.PaletteMode.Microsoft365Blue;
            this.kryptonThemeComboBox1.DropDownWidth = 503;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(334, 13);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(377, 22);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 1;
            // 
            // kryptonCustomPaletteBase1
            // 
            this.kryptonCustomPaletteBase1.BaseFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonCustomPaletteBase1.UseThemeFormChromeBorderWidth = Krypton.Toolkit.InheritBool.True;
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.BaseFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonManager1.GlobalCustomPalette = this.kryptonCustomPaletteBase1;
            this.kryptonManager1.GlobalPaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonManager1.ToolkitStrings.MessageBoxStrings.LessDetails = "L&ess Details...";
            this.kryptonManager1.ToolkitStrings.MessageBoxStrings.MoreDetails = "&More Details...";
            // 
            // kbtnTreeViewTest
            // 
            this.kbtnTreeViewTest.Location = new System.Drawing.Point(334, 461);
            this.kbtnTreeViewTest.Name = "kbtnTreeViewTest";
            this.kbtnTreeViewTest.Size = new System.Drawing.Size(377, 25);
            this.kbtnTreeViewTest.TabIndex = 9;
            this.kbtnTreeViewTest.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnTreeViewTest.Values.Text = "TreeView Test";
            this.kbtnTreeViewTest.Click += new System.EventHandler(this.kbtnTreeViewTest_Click);
            // 
            // TreeViewExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.kbtnClose;
            this.ClientSize = new System.Drawing.Size(742, 629);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "TreeViewExample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TreeView Example";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonTreeView ktvTest;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
        private Krypton.Toolkit.KryptonPropertyGrid kryptonPropertyGrid1;
        private System.Windows.Forms.ImageList imageList;
        private Krypton.Toolkit.KryptonButton kbtnToggleNodeCheckBox;
        private Krypton.Toolkit.KryptonButton buttonAppend;
        private Krypton.Toolkit.KryptonButton buttonInsert;
        private Krypton.Toolkit.KryptonButton buttonClear;
        private Krypton.Toolkit.KryptonButton buttonRemove;
        private Krypton.Toolkit.KryptonButton kbtnClose;
        private Krypton.Toolkit.KryptonCustomPaletteBase kryptonCustomPaletteBase1;
        private Krypton.Toolkit.KryptonManager kryptonManager1;
        private KryptonButton kbtnTreeViewTest;
    }
}
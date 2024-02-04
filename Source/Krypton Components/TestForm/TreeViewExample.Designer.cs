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
            this.ktvTest = new Krypton.Toolkit.KryptonTreeView();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.kryptonPropertyGrid1 = new Krypton.Toolkit.KryptonPropertyGrid();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.kbtnToggleNodeCheckBox = new Krypton.Toolkit.KryptonButton();
            this.buttonAppend = new Krypton.Toolkit.KryptonButton();
            this.buttonInsert = new Krypton.Toolkit.KryptonButton();
            this.buttonClear = new Krypton.Toolkit.KryptonButton();
            this.buttonRemove = new Krypton.Toolkit.KryptonButton();
            this.kbtnClose = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
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
            this.kryptonPanel1.Size = new System.Drawing.Size(721, 627);
            this.kryptonPanel1.TabIndex = 0;
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
            // kryptonThemeComboBox1
            // 
            this.kryptonThemeComboBox1.DropDownWidth = 121;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(334, 13);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.ReportSelectedThemeIndex = false;
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(377, 21);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 1;
            // 
            // kryptonPropertyGrid1
            // 
            this.kryptonPropertyGrid1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));
            this.kryptonPropertyGrid1.CategoryForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.kryptonPropertyGrid1.CommandsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));
            this.kryptonPropertyGrid1.CommandsForeColor = System.Drawing.Color.White;
            this.kryptonPropertyGrid1.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.kryptonPropertyGrid1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kryptonPropertyGrid1.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));
            this.kryptonPropertyGrid1.HelpForeColor = System.Drawing.Color.White;
            this.kryptonPropertyGrid1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(108)))), ((int)(((byte)(135)))));
            this.kryptonPropertyGrid1.Location = new System.Drawing.Point(334, 41);
            this.kryptonPropertyGrid1.Name = "kryptonPropertyGrid1";
            this.kryptonPropertyGrid1.SelectedObject = this.ktvTest;
            this.kryptonPropertyGrid1.Size = new System.Drawing.Size(377, 413);
            this.kryptonPropertyGrid1.TabIndex = 2;
            this.kryptonPropertyGrid1.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));
            this.kryptonPropertyGrid1.ViewForeColor = System.Drawing.Color.White;
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
            // buttonAppend
            // 
            this.buttonAppend.AutoSize = true;
            this.buttonAppend.Location = new System.Drawing.Point(13, 492);
            this.buttonAppend.Name = "buttonAppend";
            this.buttonAppend.Size = new System.Drawing.Size(314, 28);
            this.buttonAppend.TabIndex = 4;
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
            this.buttonRemove.Values.Text = "Remove";
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // kbtnClose
            // 
            this.kbtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnClose.Location = new System.Drawing.Point(575, 593);
            this.kbtnClose.Name = "kbtnClose";
            this.kbtnClose.Size = new System.Drawing.Size(138, 25);
            this.kbtnClose.TabIndex = 8;
            this.kbtnClose.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnClose.Values.Text = "Close";
            // 
            // TreeViewExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.kbtnClose;
            this.ClientSize = new System.Drawing.Size(721, 627);
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
    }
}
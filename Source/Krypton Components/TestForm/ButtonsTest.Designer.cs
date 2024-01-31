namespace TestForm
{
    partial class ButtonsTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ButtonsTest));
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kcbtnDropDown = new Krypton.Toolkit.KryptonColorButton();
            this.kryptonButton5 = new Krypton.Toolkit.KryptonButton();
            this.kryptonButton6 = new Krypton.Toolkit.KryptonButton();
            this.kryptonButton7 = new Krypton.Toolkit.KryptonButton();
            this.kryptonButton8 = new Krypton.Toolkit.KryptonButton();
            this.kryptonButton4 = new Krypton.Toolkit.KryptonButton();
            this.kryptonButton2 = new Krypton.Toolkit.KryptonButton();
            this.kryptonButton3 = new Krypton.Toolkit.KryptonButton();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.kryptonContextMenu1 = new Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuItems1 = new Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuItem1 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem2 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem3 = new Krypton.Toolkit.KryptonContextMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kcbtnDropDown);
            this.kryptonPanel1.Controls.Add(this.kryptonButton5);
            this.kryptonPanel1.Controls.Add(this.kryptonButton6);
            this.kryptonPanel1.Controls.Add(this.kryptonButton7);
            this.kryptonPanel1.Controls.Add(this.kryptonButton8);
            this.kryptonPanel1.Controls.Add(this.kryptonButton4);
            this.kryptonPanel1.Controls.Add(this.kryptonButton2);
            this.kryptonPanel1.Controls.Add(this.kryptonButton3);
            this.kryptonPanel1.Controls.Add(this.kryptonButton1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(522, 180);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kcbtnDropDown
            // 
            this.kcbtnDropDown.CustomColorPreviewShape = Krypton.Toolkit.KryptonColorButtonCustomColorPreviewShape.Circle;
            this.kcbtnDropDown.Location = new System.Drawing.Point(13, 137);
            this.kcbtnDropDown.Name = "kcbtnDropDown";
            this.kcbtnDropDown.Size = new System.Drawing.Size(243, 25);
            this.kcbtnDropDown.TabIndex = 7;
            this.kcbtnDropDown.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonColorButton1.Values.Image")));
            this.kcbtnDropDown.Values.RoundedCorners = 8;
            this.kcbtnDropDown.Values.Text = "Drop Down Color";
            this.kcbtnDropDown.SelectedColorChanged += new System.EventHandler<Krypton.Toolkit.ColorEventArgs>(this.kcbtnDropDown_SelectedColorChanged);
            // 
            // kryptonButton5
            // 
            this.kryptonButton5.Enabled = false;
            this.kryptonButton5.KryptonContextMenu = this.kryptonContextMenu1;
            this.kryptonButton5.Location = new System.Drawing.Point(262, 106);
            this.kryptonButton5.Name = "kryptonButton5";
            this.kryptonButton5.Size = new System.Drawing.Size(243, 25);
            this.kryptonButton5.TabIndex = 6;
            this.kryptonButton5.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonButton5.Values.Image")));
            this.kryptonButton5.Values.ShowSplitOption = true;
            this.kryptonButton5.Values.Text = "Disabled UAC Drop Down";
            this.kryptonButton5.Values.UseAsUACElevationButton = true;
            // 
            // kryptonButton6
            // 
            this.kryptonButton6.Enabled = false;
            this.kryptonButton6.Location = new System.Drawing.Point(262, 75);
            this.kryptonButton6.Name = "kryptonButton6";
            this.kryptonButton6.Size = new System.Drawing.Size(243, 25);
            this.kryptonButton6.TabIndex = 4;
            this.kryptonButton6.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonButton6.Values.Image")));
            this.kryptonButton6.Values.Text = "Disabled UAC Button";
            this.kryptonButton6.Values.UseAsUACElevationButton = true;
            // 
            // kryptonButton7
            // 
            this.kryptonButton7.KryptonContextMenu = this.kryptonContextMenu1;
            this.kryptonButton7.Location = new System.Drawing.Point(13, 106);
            this.kryptonButton7.Name = "kryptonButton7";
            this.kryptonButton7.Size = new System.Drawing.Size(243, 25);
            this.kryptonButton7.TabIndex = 5;
            this.kryptonButton7.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonButton7.Values.Image")));
            this.kryptonButton7.Values.ShowSplitOption = true;
            this.kryptonButton7.Values.Text = "Normal UAC Drop Down";
            this.kryptonButton7.Values.UseAsUACElevationButton = true;
            // 
            // kryptonButton8
            // 
            this.kryptonButton8.Location = new System.Drawing.Point(13, 75);
            this.kryptonButton8.Name = "kryptonButton8";
            this.kryptonButton8.Size = new System.Drawing.Size(243, 25);
            this.kryptonButton8.TabIndex = 3;
            this.kryptonButton8.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonButton8.Values.Image")));
            this.kryptonButton8.Values.Text = "Normal UAC Button";
            this.kryptonButton8.Values.UseAsUACElevationButton = true;
            // 
            // kryptonButton4
            // 
            this.kryptonButton4.Enabled = false;
            this.kryptonButton4.KryptonContextMenu = this.kryptonContextMenu1;
            this.kryptonButton4.Location = new System.Drawing.Point(262, 44);
            this.kryptonButton4.Name = "kryptonButton4";
            this.kryptonButton4.Size = new System.Drawing.Size(243, 25);
            this.kryptonButton4.TabIndex = 2;
            this.kryptonButton4.Values.ShowSplitOption = true;
            this.kryptonButton4.Values.Text = "Disabled Drop Down";
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.Enabled = false;
            this.kryptonButton2.Location = new System.Drawing.Point(262, 13);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.Size = new System.Drawing.Size(243, 25);
            this.kryptonButton2.TabIndex = 1;
            this.kryptonButton2.Values.Text = "Disabled Button";
            // 
            // kryptonButton3
            // 
            this.kryptonButton3.KryptonContextMenu = this.kryptonContextMenu1;
            this.kryptonButton3.Location = new System.Drawing.Point(13, 44);
            this.kryptonButton3.Name = "kryptonButton3";
            this.kryptonButton3.Size = new System.Drawing.Size(243, 25);
            this.kryptonButton3.TabIndex = 1;
            this.kryptonButton3.Values.ShowSplitOption = true;
            this.kryptonButton3.Values.Text = "Normal Drop Down";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(13, 13);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(243, 25);
            this.kryptonButton1.TabIndex = 0;
            this.kryptonButton1.Values.Text = "Normal Button";
            // 
            // kryptonContextMenu1
            // 
            this.kryptonContextMenu1.Items.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItems1});
            // 
            // kryptonContextMenuItems1
            // 
            this.kryptonContextMenuItems1.Items.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItem1,
            this.kryptonContextMenuItem2,
            this.kryptonContextMenuItem3});
            // 
            // kryptonContextMenuItem1
            // 
            this.kryptonContextMenuItem1.Text = "Choice 1";
            // 
            // kryptonContextMenuItem2
            // 
            this.kryptonContextMenuItem2.Text = "Choice 2";
            // 
            // kryptonContextMenuItem3
            // 
            this.kryptonContextMenuItem3.Text = "Choice 3";
            // 
            // ButtonsTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 180);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "ButtonsTest";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Buttons Test";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonButton kryptonButton1;
        private Krypton.Toolkit.KryptonColorButton kcbtnDropDown;
        private Krypton.Toolkit.KryptonButton kryptonButton5;
        private Krypton.Toolkit.KryptonButton kryptonButton6;
        private Krypton.Toolkit.KryptonButton kryptonButton7;
        private Krypton.Toolkit.KryptonButton kryptonButton8;
        private Krypton.Toolkit.KryptonButton kryptonButton4;
        private Krypton.Toolkit.KryptonButton kryptonButton2;
        private Krypton.Toolkit.KryptonButton kryptonButton3;
        private Krypton.Toolkit.KryptonContextMenu kryptonContextMenu1;
        private Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems1;
        private Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem1;
        private Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem2;
        private Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem3;
    }
}
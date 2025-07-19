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
            this.kbtnButtonStyles = new Krypton.Toolkit.KryptonButton();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.kryptonColorButton1 = new Krypton.Toolkit.KryptonColorButton();
            this.kcbtnDropDown = new Krypton.Toolkit.KryptonColorButton();
            this.kryptonButton5 = new Krypton.Toolkit.KryptonButton();
            this.kryptonContextMenu1 = new Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuItems1 = new Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuItem1 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonCommand1 = new Krypton.Toolkit.KryptonCommand();
            this.kryptonContextMenuItem2 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonCommand2 = new Krypton.Toolkit.KryptonCommand();
            this.kryptonContextMenuItem3 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonButton6 = new Krypton.Toolkit.KryptonButton();
            this.kryptonButton7 = new Krypton.Toolkit.KryptonButton();
            this.kryptonButton8 = new Krypton.Toolkit.KryptonButton();
            this.kryptonButton4 = new Krypton.Toolkit.KryptonButton();
            this.kryptonButton2 = new Krypton.Toolkit.KryptonButton();
            this.kryptonButton3 = new Krypton.Toolkit.KryptonButton();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.buttonSpecAny1 = new Krypton.Toolkit.ButtonSpecAny();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnButtonStyles);
            this.kryptonPanel1.Controls.Add(this.kryptonThemeComboBox1);
            this.kryptonPanel1.Controls.Add(this.kryptonColorButton1);
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
            this.kryptonPanel1.Size = new System.Drawing.Size(542, 233);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnButtonStyles
            // 
            this.kbtnButtonStyles.Location = new System.Drawing.Point(140, 196);
            this.kbtnButtonStyles.Name = "kbtnButtonStyles";
            this.kbtnButtonStyles.Size = new System.Drawing.Size(243, 25);
            this.kbtnButtonStyles.TabIndex = 10;
            this.kbtnButtonStyles.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnButtonStyles.Values.Text = "Button Styles";
            this.kbtnButtonStyles.Click += new System.EventHandler(this.kbtnButtonStyles_Click);
            // 
            // kryptonThemeComboBox1
            // 
            this.kryptonThemeComboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonThemeComboBox1.DefaultPalette = Krypton.Toolkit.PaletteMode.Microsoft365Blue;
            this.kryptonThemeComboBox1.DropDownWidth = 492;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(18, 13);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(504, 22);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 9;
            // 
            // kryptonColorButton1
            // 
            this.kryptonColorButton1.CustomColorPreviewShape = Krypton.Toolkit.KryptonColorButtonCustomColorPreviewShape.Circle;
            this.kryptonColorButton1.Enabled = false;
            this.kryptonColorButton1.Location = new System.Drawing.Point(267, 165);
            this.kryptonColorButton1.Name = "kryptonColorButton1";
            this.kryptonColorButton1.Size = new System.Drawing.Size(243, 25);
            this.kryptonColorButton1.TabIndex = 8;
            this.kryptonColorButton1.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonColorButton1.Values.Image")));
            this.kryptonColorButton1.Values.RoundedCorners = 8;
            this.kryptonColorButton1.Values.Text = "Disabled Drop Down Color";
            // 
            // kcbtnDropDown
            // 
            this.kcbtnDropDown.CustomColorPreviewShape = Krypton.Toolkit.KryptonColorButtonCustomColorPreviewShape.Circle;
            this.kcbtnDropDown.Location = new System.Drawing.Point(18, 165);
            this.kcbtnDropDown.Name = "kcbtnDropDown";
            this.kcbtnDropDown.Size = new System.Drawing.Size(243, 25);
            this.kcbtnDropDown.TabIndex = 7;
            this.kcbtnDropDown.Values.Image = ((System.Drawing.Image)(resources.GetObject("kcbtnDropDown.Values.Image")));
            this.kcbtnDropDown.Values.RoundedCorners = 8;
            this.kcbtnDropDown.Values.Text = "Drop Down Color";
            this.kcbtnDropDown.SelectedColorChanged += new System.EventHandler<Krypton.Toolkit.ColorEventArgs>(this.kcbtnDropDown_SelectedColorChanged);
            // 
            // kryptonButton5
            // 
            this.kryptonButton5.Enabled = false;
            this.kryptonButton5.KryptonContextMenu = this.kryptonContextMenu1;
            this.kryptonButton5.Location = new System.Drawing.Point(267, 134);
            this.kryptonButton5.Name = "kryptonButton5";
            this.kryptonButton5.ShowSplitOption = true;
            this.kryptonButton5.Size = new System.Drawing.Size(243, 25);
            this.kryptonButton5.TabIndex = 6;
            this.kryptonButton5.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton5.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonButton5.Values.Image")));
            this.kryptonButton5.Values.ShowSplitOption = true;
            this.kryptonButton5.Values.Text = "Disabled UAC Drop Down";
            this.kryptonButton5.Values.UseAsUACElevationButton = true;
            // 
            // kryptonContextMenu1
            // 
            this.kryptonContextMenu1.Items.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] {
            ((Krypton.Toolkit.KryptonContextMenuItemBase)(this.kryptonContextMenuItems1))});
            // 
            // kryptonContextMenuItems1
            // 
            this.kryptonContextMenuItems1.Items.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] {
            ((Krypton.Toolkit.KryptonContextMenuItemBase)(this.kryptonContextMenuItem1)),
            ((Krypton.Toolkit.KryptonContextMenuItemBase)(this.kryptonContextMenuItem2)),
            ((Krypton.Toolkit.KryptonContextMenuItemBase)(this.kryptonContextMenuItem3))});
            // 
            // kryptonContextMenuItem1
            // 
            this.kryptonContextMenuItem1.CommandParameter = "DoThing1";
            this.kryptonContextMenuItem1.KryptonCommand = this.kryptonCommand1;
            this.kryptonContextMenuItem1.Text = "Choice 1 (Command 1)";
            // 
            // kryptonCommand1
            // 
            this.kryptonCommand1.Execute += new System.EventHandler(this.kryptonCommand1_Execute);
            // 
            // kryptonContextMenuItem2
            // 
            this.kryptonContextMenuItem2.CommandParameter = "DoThing2";
            this.kryptonContextMenuItem2.KryptonCommand = this.kryptonCommand1;
            this.kryptonContextMenuItem2.Text = "Choice 2 (Command 1)";
            // 
            // kryptonCommand2
            // 
            this.kryptonCommand2.Execute += new System.EventHandler(this.kryptonCommand1_Execute);
            // 
            // kryptonContextMenuItem3
            // 
            this.kryptonContextMenuItem3.CommandParameter = "ThisIsCmd2";
            this.kryptonContextMenuItem3.KryptonCommand = this.kryptonCommand2;
            this.kryptonContextMenuItem3.Text = "Choice 3 (Command 2)";
            // 
            // kryptonButton6
            // 
            this.kryptonButton6.Enabled = false;
            this.kryptonButton6.Location = new System.Drawing.Point(267, 103);
            this.kryptonButton6.Name = "kryptonButton6";
            this.kryptonButton6.Size = new System.Drawing.Size(243, 25);
            this.kryptonButton6.TabIndex = 4;
            this.kryptonButton6.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton6.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonButton6.Values.Image")));
            this.kryptonButton6.Values.Text = "Disabled UAC Button";
            this.kryptonButton6.Values.UseAsUACElevationButton = true;
            // 
            // kryptonButton7
            // 
            this.kryptonButton7.KryptonContextMenu = this.kryptonContextMenu1;
            this.kryptonButton7.Location = new System.Drawing.Point(18, 134);
            this.kryptonButton7.Name = "kryptonButton7";
            this.kryptonButton7.ShowSplitOption = true;
            this.kryptonButton7.Size = new System.Drawing.Size(243, 25);
            this.kryptonButton7.TabIndex = 5;
            this.kryptonButton7.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton7.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonButton7.Values.Image")));
            this.kryptonButton7.Values.ShowSplitOption = true;
            this.kryptonButton7.Values.Text = "Normal UAC Drop Down";
            this.kryptonButton7.Values.UseAsUACElevationButton = true;
            // 
            // kryptonButton8
            // 
            this.kryptonButton8.Location = new System.Drawing.Point(18, 103);
            this.kryptonButton8.Name = "kryptonButton8";
            this.kryptonButton8.Size = new System.Drawing.Size(243, 25);
            this.kryptonButton8.TabIndex = 3;
            this.kryptonButton8.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton8.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonButton8.Values.Image")));
            this.kryptonButton8.Values.Text = "Normal UAC Button";
            this.kryptonButton8.Values.UseAsUACElevationButton = true;
            // 
            // kryptonButton4
            // 
            this.kryptonButton4.Enabled = false;
            this.kryptonButton4.KryptonContextMenu = this.kryptonContextMenu1;
            this.kryptonButton4.Location = new System.Drawing.Point(267, 72);
            this.kryptonButton4.Name = "kryptonButton4";
            this.kryptonButton4.Size = new System.Drawing.Size(243, 25);
            this.kryptonButton4.TabIndex = 2;
            this.kryptonButton4.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton4.Values.ShowSplitOption = true;
            this.kryptonButton4.Values.Text = "Disabled Drop Down";
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.Enabled = false;
            this.kryptonButton2.Location = new System.Drawing.Point(267, 41);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.Size = new System.Drawing.Size(243, 25);
            this.kryptonButton2.TabIndex = 1;
            this.kryptonButton2.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton2.Values.Text = "Disabled Button";
            // 
            // kryptonButton3
            // 
            this.kryptonButton3.KryptonContextMenu = this.kryptonContextMenu1;
            this.kryptonButton3.Location = new System.Drawing.Point(18, 72);
            this.kryptonButton3.Name = "kryptonButton3";
            this.kryptonButton3.Size = new System.Drawing.Size(243, 25);
            this.kryptonButton3.TabIndex = 1;
            this.kryptonButton3.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton3.Values.ShowSplitOption = true;
            this.kryptonButton3.Values.Text = "Normal Drop Down";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(18, 41);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(243, 25);
            this.kryptonButton1.TabIndex = 0;
            this.kryptonButton1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton1.Values.Text = "Normal Button";
            // 
            // buttonSpecAny1
            // 
            this.buttonSpecAny1.Text = "Test Text";
            this.buttonSpecAny1.UniqueName = "bad5983b9e7f4d82b15e55a1a19807bb";
            // 
            // ButtonsTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ButtonSpecs.Add(this.buttonSpecAny1);
            this.ClientSize = new System.Drawing.Size(542, 233);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(544, 280);
            this.Name = "ButtonsTest";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Buttons Test";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
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
        private KryptonColorButton kryptonColorButton1;
        private ButtonSpecAny buttonSpecAny1;
        private KryptonThemeComboBox kryptonThemeComboBox1;
        private KryptonButton kbtnButtonStyles;
        private KryptonCommand kryptonCommand1;
        private KryptonCommand kryptonCommand2;
    }
}
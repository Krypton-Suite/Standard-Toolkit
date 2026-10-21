#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class PoweredByButtonExample
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PoweredByButtonExample));
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kchkShowChangelogButton = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkShowReadmeButton = new Krypton.Toolkit.KryptonCheckBox();
            this.kryptonPoweredByButton1 = new Krypton.Toolkit.KryptonPoweredByButton();
            this.kryptonComboBox1 = new Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kchkShowChangelogButton);
            this.kryptonPanel1.Controls.Add(this.kchkShowReadmeButton);
            this.kryptonPanel1.Controls.Add(this.kryptonPoweredByButton1);
            this.kryptonPanel1.Controls.Add(this.kryptonComboBox1);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(503, 121);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kchkShowChangelogButton
            // 
            this.kchkShowChangelogButton.Location = new System.Drawing.Point(249, 42);
            this.kchkShowChangelogButton.Name = "kchkShowChangelogButton";
            this.kchkShowChangelogButton.Size = new System.Drawing.Size(154, 20);
            this.kchkShowChangelogButton.TabIndex = 4;
            this.kchkShowChangelogButton.Values.Text = "Show changelog button";
            this.kchkShowChangelogButton.CheckedChanged += new System.EventHandler(this.kchkShowChangelogButton_CheckedChanged);
            // 
            // kchkShowReadmeButton
            // 
            this.kchkShowReadmeButton.Location = new System.Drawing.Point(104, 42);
            this.kchkShowReadmeButton.Name = "kchkShowReadmeButton";
            this.kchkShowReadmeButton.Size = new System.Drawing.Size(138, 20);
            this.kchkShowReadmeButton.TabIndex = 3;
            this.kchkShowReadmeButton.Values.Text = "Show readme button";
            this.kchkShowReadmeButton.CheckedChanged += new System.EventHandler(this.kchkShowReadmeButton_CheckedChanged);
            // 
            // kryptonPoweredByButton1
            // 
            this.kryptonPoweredByButton1.Location = new System.Drawing.Point(104, 71);
            this.kryptonPoweredByButton1.Name = "kryptonPoweredByButton1";
            this.kryptonPoweredByButton1.Size = new System.Drawing.Size(373, 25);
            this.kryptonPoweredByButton1.TabIndex = 2;
            this.kryptonPoweredByButton1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonPoweredByButton1.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonPoweredByButton1.Values.Image")));
            this.kryptonPoweredByButton1.Values.Text = "&Powered By Krypton";
            // 
            // kryptonComboBox1
            // 
            this.kryptonComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kryptonComboBox1.DropDownWidth = 373;
            this.kryptonComboBox1.Location = new System.Drawing.Point(104, 13);
            this.kryptonComboBox1.Name = "kryptonComboBox1";
            this.kryptonComboBox1.Size = new System.Drawing.Size(373, 22);
            this.kryptonComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonComboBox1.TabIndex = 1;
            this.kryptonComboBox1.SelectedIndexChanged += new System.EventHandler(this.kryptonComboBox1_SelectedIndexChanged);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.kryptonLabel1.Location = new System.Drawing.Point(13, 13);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(84, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Toolkit type:";
            // 
            // PoweredByButtonExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 125);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "PoweredByButtonExample";
            this.Text = "PoweredByButtonExample";
            this.Load += new System.EventHandler(this.PoweredByButtonExample_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonLabel kryptonLabel1;
        private KryptonComboBox kryptonComboBox1;
        private KryptonPoweredByButton kryptonPoweredByButton1;
        private KryptonCheckBox kchkShowReadmeButton;
        private KryptonCheckBox kchkShowChangelogButton;
    }
}
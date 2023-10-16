namespace TestForm
{
    partial class Form6
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
            this.kryptonToolkitPoweredByControl1 = new Krypton.Toolkit.KryptonToolkitPoweredByControl();
            this.kcbThemeOptions = new Krypton.Toolkit.KryptonCheckBox();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.kcmbToolkitType = new Krypton.Toolkit.KryptonComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbToolkitType)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kcmbToolkitType);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.kcbThemeOptions);
            this.kryptonPanel1.Controls.Add(this.kryptonToolkitPoweredByControl1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(800, 450);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonToolkitPoweredByControl1
            // 
            this.kryptonToolkitPoweredByControl1.Location = new System.Drawing.Point(55, 12);
            this.kryptonToolkitPoweredByControl1.Name = "kryptonToolkitPoweredByControl1";
            this.kryptonToolkitPoweredByControl1.ShowThemeOption = false;
            this.kryptonToolkitPoweredByControl1.Size = new System.Drawing.Size(659, 249);
            this.kryptonToolkitPoweredByControl1.TabIndex = 1;
            this.kryptonToolkitPoweredByControl1.ToolkitType = Krypton.Toolkit.ToolkitType.Stable;
            // 
            // kcbThemeOptions
            // 
            this.kcbThemeOptions.Location = new System.Drawing.Point(55, 268);
            this.kcbThemeOptions.Name = "kcbThemeOptions";
            this.kcbThemeOptions.Size = new System.Drawing.Size(140, 20);
            this.kcbThemeOptions.TabIndex = 2;
            this.kcbThemeOptions.Values.Text = "Show &Theme Options";
            this.kcbThemeOptions.CheckedChanged += new System.EventHandler(this.kcbThemeOptions_CheckedChanged);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.kryptonLabel1.Location = new System.Drawing.Point(202, 268);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(87, 20);
            this.kryptonLabel1.TabIndex = 3;
            this.kryptonLabel1.Values.Text = "Toolkit Type:";
            // 
            // kcmbToolkitType
            // 
            this.kcmbToolkitType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbToolkitType.DropDownWidth = 121;
            this.kcmbToolkitType.IntegralHeight = false;
            this.kcmbToolkitType.Location = new System.Drawing.Point(296, 268);
            this.kcmbToolkitType.Name = "kcmbToolkitType";
            this.kcmbToolkitType.Size = new System.Drawing.Size(121, 21);
            this.kcmbToolkitType.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbToolkitType.TabIndex = 4;
            this.kcmbToolkitType.SelectedIndexChanged += new System.EventHandler(this.kcmbToolkitType_SelectedIndexChanged);
            // 
            // Form6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "Form6";
            this.Text = "Form6";
            this.Load += new System.EventHandler(this.Form6_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbToolkitType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonToolkitPoweredByControl kryptonToolkitPoweredByControl1;
        private Krypton.Toolkit.KryptonCheckBox kcbThemeOptions;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonComboBox kcmbToolkitType;
    }
}
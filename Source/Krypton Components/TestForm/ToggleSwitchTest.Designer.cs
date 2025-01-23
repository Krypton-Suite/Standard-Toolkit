namespace TestForm
{
    partial class ToggleSwitchTest
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
            this.kryptonToggleSwitch1 = new Krypton.Toolkit.KryptonToggleSwitch();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonToggleSwitch1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(808, 426);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonToggleSwitch1
            // 
            this.kryptonToggleSwitch1.Checked = false;
            this.kryptonToggleSwitch1.EnableKnobGradient = true;
            this.kryptonToggleSwitch1.GradientEndIntensity = 0.5F;
            this.kryptonToggleSwitch1.GradientStartIntensity = 0.8F;
            this.kryptonToggleSwitch1.Location = new System.Drawing.Point(61, 51);
            this.kryptonToggleSwitch1.Name = "kryptonToggleSwitch1";
            this.kryptonToggleSwitch1.Size = new System.Drawing.Size(670, 319);
            this.kryptonToggleSwitch1.TabIndex = 0;
            // 
            // ToggleSwitchTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 426);
            this.Controls.Add(this.kryptonPanel1);
            this.FormTitleAlign = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.FormMain;
            this.GroupBorderStyle = Krypton.Toolkit.PaletteBorderStyle.FormMain;
            this.HeaderStyle = Krypton.Toolkit.HeaderStyle.Form;
            this.ImageStyle = Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.Name = "ToggleSwitchTest";
            this.Text = "ToggleSwitchTest";
            this.TitleStyle = Krypton.Toolkit.KryptonFormTitleStyle.Inherit;
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonToggleSwitch kryptonToggleSwitch1;
    }
}
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
            this.components = new System.ComponentModel.Container();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonToggleSwitch1 = new Krypton.Toolkit.KryptonToggleSwitch();
            this.kryptonManager1 = new Krypton.Toolkit.KryptonManager(this.components);
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
            this.kryptonPanel1.Size = new System.Drawing.Size(862, 264);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonToggleSwitch1
            // 
            this.kryptonToggleSwitch1.BackColor = System.Drawing.Color.Transparent;
            this.kryptonToggleSwitch1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.kryptonToggleSwitch1.Location = new System.Drawing.Point(205, 25);
            this.kryptonToggleSwitch1.Name = "kryptonToggleSwitch1";
            this.kryptonToggleSwitch1.Size = new System.Drawing.Size(442, 174);
            this.kryptonToggleSwitch1.TabIndex = 0;
            this.kryptonToggleSwitch1.Text = "kryptonToggleSwitch1";
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.ToolkitStrings.MessageBoxStrings.LessDetails = "L&ess Details...";
            this.kryptonManager1.ToolkitStrings.MessageBoxStrings.MoreDetails = "&More Details...";
            // 
            // ToggleSwitchTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 264);
            this.Controls.Add(this.kryptonPanel1);
            this.FormTitleAlign = Krypton.Toolkit.PaletteRelativeAlign.Inherit;
            this.Name = "ToggleSwitchTest";
            this.Text = "ToggleSwitchTest";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonToggleSwitch kryptonToggleSwitch1;
        private KryptonManager kryptonManager1;
    }
}
namespace TestForm
{
    partial class ControlStyles
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
            this.kbtnPanelStyles = new Krypton.Toolkit.KryptonButton();
            this.kbtnRadioButtonStyles = new Krypton.Toolkit.KryptonButton();
            this.kbtnCheckBoxStyles = new Krypton.Toolkit.KryptonButton();
            this.kbtnButtonStyles = new Krypton.Toolkit.KryptonButton();
            this.kbtnLabelStyles = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnLabelStyles);
            this.kryptonPanel1.Controls.Add(this.kbtnPanelStyles);
            this.kryptonPanel1.Controls.Add(this.kbtnRadioButtonStyles);
            this.kryptonPanel1.Controls.Add(this.kbtnCheckBoxStyles);
            this.kryptonPanel1.Controls.Add(this.kbtnButtonStyles);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(232, 336);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnPanelStyles
            // 
            this.kbtnPanelStyles.Location = new System.Drawing.Point(13, 107);
            this.kbtnPanelStyles.Name = "kbtnPanelStyles";
            this.kbtnPanelStyles.Size = new System.Drawing.Size(205, 25);
            this.kbtnPanelStyles.TabIndex = 3;
            this.kbtnPanelStyles.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnPanelStyles.Values.Text = "Panel Styles";
            this.kbtnPanelStyles.Click += new System.EventHandler(this.kbtnPanelStyles_Click);
            // 
            // kbtnRadioButtonStyles
            // 
            this.kbtnRadioButtonStyles.Location = new System.Drawing.Point(13, 138);
            this.kbtnRadioButtonStyles.Name = "kbtnRadioButtonStyles";
            this.kbtnRadioButtonStyles.Size = new System.Drawing.Size(205, 25);
            this.kbtnRadioButtonStyles.TabIndex = 2;
            this.kbtnRadioButtonStyles.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnRadioButtonStyles.Values.Text = "Radio Button Styles";
            this.kbtnRadioButtonStyles.Click += new System.EventHandler(this.kbtnRadioButtonStyles_Click);
            // 
            // kbtnCheckBoxStyles
            // 
            this.kbtnCheckBoxStyles.Location = new System.Drawing.Point(13, 45);
            this.kbtnCheckBoxStyles.Name = "kbtnCheckBoxStyles";
            this.kbtnCheckBoxStyles.Size = new System.Drawing.Size(205, 25);
            this.kbtnCheckBoxStyles.TabIndex = 1;
            this.kbtnCheckBoxStyles.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnCheckBoxStyles.Values.Text = "CheckBox Styles";
            this.kbtnCheckBoxStyles.Click += new System.EventHandler(this.kbtnCheckBoxStyles_Click);
            // 
            // kbtnButtonStyles
            // 
            this.kbtnButtonStyles.Location = new System.Drawing.Point(13, 13);
            this.kbtnButtonStyles.Name = "kbtnButtonStyles";
            this.kbtnButtonStyles.Size = new System.Drawing.Size(205, 25);
            this.kbtnButtonStyles.TabIndex = 0;
            this.kbtnButtonStyles.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnButtonStyles.Values.Text = "Button Styles";
            this.kbtnButtonStyles.Click += new System.EventHandler(this.kbtnButtonStyles_Click);
            // 
            // kbtnLabelStyles
            // 
            this.kbtnLabelStyles.Location = new System.Drawing.Point(13, 76);
            this.kbtnLabelStyles.Name = "kbtnLabelStyles";
            this.kbtnLabelStyles.Size = new System.Drawing.Size(205, 25);
            this.kbtnLabelStyles.TabIndex = 4;
            this.kbtnLabelStyles.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnLabelStyles.Values.Text = "Label Styles";
            this.kbtnLabelStyles.Click += new System.EventHandler(this.kbtnLabelStyles_Click);
            // 
            // ControlStyles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 336);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "ControlStyles";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Control  Styles";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonButton kbtnButtonStyles;
        private KryptonButton kbtnCheckBoxStyles;
        private KryptonButton kbtnRadioButtonStyles;
        private KryptonButton kbtnPanelStyles;
        private KryptonButton kbtnLabelStyles;
    }
}
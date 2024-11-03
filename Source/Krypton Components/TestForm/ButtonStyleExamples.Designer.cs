namespace TestForm
{
    partial class ButtonStyleExamples
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ButtonStyleExamples));
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.buttonLive = new Krypton.Toolkit.KryptonCheckButton();
            this.buttonDefaultFocus = new Krypton.Toolkit.KryptonCheckButton();
            this.buttonTracking = new Krypton.Toolkit.KryptonCheckButton();
            this.buttonNormal = new Krypton.Toolkit.KryptonCheckButton();
            this.buttonPressed = new Krypton.Toolkit.KryptonCheckButton();
            this.buttonDisabled = new Krypton.Toolkit.KryptonCheckButton();
            this.buttonCheckedNormal = new Krypton.Toolkit.KryptonCheckButton();
            this.buttonCheckedTracking = new Krypton.Toolkit.KryptonCheckButton();
            this.buttonCheckedPressed = new Krypton.Toolkit.KryptonCheckButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonThemeComboBox1);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.buttonLive);
            this.kryptonPanel1.Controls.Add(this.buttonDefaultFocus);
            this.kryptonPanel1.Controls.Add(this.buttonTracking);
            this.kryptonPanel1.Controls.Add(this.buttonNormal);
            this.kryptonPanel1.Controls.Add(this.buttonPressed);
            this.kryptonPanel1.Controls.Add(this.buttonDisabled);
            this.kryptonPanel1.Controls.Add(this.buttonCheckedNormal);
            this.kryptonPanel1.Controls.Add(this.buttonCheckedTracking);
            this.kryptonPanel1.Controls.Add(this.buttonCheckedPressed);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(664, 261);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonThemeComboBox1
            // 
            this.kryptonThemeComboBox1.DefaultPalette = Krypton.Toolkit.PaletteMode.Microsoft365Blue;
            this.kryptonThemeComboBox1.DropDownWidth = 282;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(205, 228);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(282, 22);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 27;
            this.kryptonThemeComboBox1.SelectedIndexChanged += new System.EventHandler(this.kryptonThemeComboBox1_SelectedIndexChanged);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this.kryptonLabel1.Location = new System.Drawing.Point(12, 13);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(138, 29);
            this.kryptonLabel1.TabIndex = 26;
            this.kryptonLabel1.Values.Text = "kryptonLabel1";
            // 
            // buttonLive
            // 
            this.buttonLive.AutoSize = true;
            this.buttonLive.Location = new System.Drawing.Point(285, 187);
            this.buttonLive.Name = "buttonLive";
            this.buttonLive.Size = new System.Drawing.Size(90, 28);
            this.buttonLive.TabIndex = 17;
            this.buttonLive.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonLive.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonLive.Values.Image")));
            this.buttonLive.Values.Text = "Live";
            this.buttonLive.Click += new System.EventHandler(this.buttonLive_Click);
            // 
            // buttonDefaultFocus
            // 
            this.buttonDefaultFocus.AutoSize = true;
            this.buttonDefaultFocus.Enabled = false;
            this.buttonDefaultFocus.Location = new System.Drawing.Point(340, 51);
            this.buttonDefaultFocus.Name = "buttonDefaultFocus";
            this.buttonDefaultFocus.Size = new System.Drawing.Size(147, 28);
            this.buttonDefaultFocus.TabIndex = 25;
            this.buttonDefaultFocus.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonDefaultFocus.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonDefaultFocus.Values.Image")));
            this.buttonDefaultFocus.Values.Text = "Default + Focus";
            this.buttonDefaultFocus.Click += new System.EventHandler(this.buttonDefaultFocus_Click);
            // 
            // buttonTracking
            // 
            this.buttonTracking.AutoSize = true;
            this.buttonTracking.Enabled = false;
            this.buttonTracking.Location = new System.Drawing.Point(204, 119);
            this.buttonTracking.Name = "buttonTracking";
            this.buttonTracking.Size = new System.Drawing.Size(119, 28);
            this.buttonTracking.TabIndex = 24;
            this.buttonTracking.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonTracking.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonTracking.Values.Image")));
            this.buttonTracking.Values.Text = "Tracking";
            this.buttonTracking.Click += new System.EventHandler(this.buttonTracking_Click);
            // 
            // buttonNormal
            // 
            this.buttonNormal.AutoSize = true;
            this.buttonNormal.Enabled = false;
            this.buttonNormal.Location = new System.Drawing.Point(204, 85);
            this.buttonNormal.Name = "buttonNormal";
            this.buttonNormal.Size = new System.Drawing.Size(119, 28);
            this.buttonNormal.TabIndex = 23;
            this.buttonNormal.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonNormal.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonNormal.Values.Image")));
            this.buttonNormal.Values.Text = "Normal";
            this.buttonNormal.Click += new System.EventHandler(this.buttonNormal_Click);
            // 
            // buttonPressed
            // 
            this.buttonPressed.AutoSize = true;
            this.buttonPressed.Enabled = false;
            this.buttonPressed.Location = new System.Drawing.Point(204, 153);
            this.buttonPressed.Name = "buttonPressed";
            this.buttonPressed.Size = new System.Drawing.Size(119, 28);
            this.buttonPressed.TabIndex = 22;
            this.buttonPressed.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonPressed.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonPressed.Values.Image")));
            this.buttonPressed.Values.Text = "Pressed";
            this.buttonPressed.Click += new System.EventHandler(this.buttonPressed_Click);
            // 
            // buttonDisabled
            // 
            this.buttonDisabled.AutoSize = true;
            this.buttonDisabled.Enabled = false;
            this.buttonDisabled.Location = new System.Drawing.Point(204, 51);
            this.buttonDisabled.Name = "buttonDisabled";
            this.buttonDisabled.Size = new System.Drawing.Size(119, 28);
            this.buttonDisabled.TabIndex = 21;
            this.buttonDisabled.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonDisabled.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonDisabled.Values.Image")));
            this.buttonDisabled.Values.Text = "Disabled";
            this.buttonDisabled.Click += new System.EventHandler(this.buttonDisabled_Click);
            // 
            // buttonCheckedNormal
            // 
            this.buttonCheckedNormal.AutoSize = true;
            this.buttonCheckedNormal.Enabled = false;
            this.buttonCheckedNormal.Location = new System.Drawing.Point(340, 85);
            this.buttonCheckedNormal.Name = "buttonCheckedNormal";
            this.buttonCheckedNormal.Size = new System.Drawing.Size(147, 28);
            this.buttonCheckedNormal.TabIndex = 20;
            this.buttonCheckedNormal.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonCheckedNormal.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonCheckedNormal.Values.Image")));
            this.buttonCheckedNormal.Values.Text = "Checked Normal";
            this.buttonCheckedNormal.Click += new System.EventHandler(this.buttonCheckedNormal_Click);
            // 
            // buttonCheckedTracking
            // 
            this.buttonCheckedTracking.AutoSize = true;
            this.buttonCheckedTracking.Enabled = false;
            this.buttonCheckedTracking.Location = new System.Drawing.Point(340, 119);
            this.buttonCheckedTracking.Name = "buttonCheckedTracking";
            this.buttonCheckedTracking.Size = new System.Drawing.Size(147, 28);
            this.buttonCheckedTracking.TabIndex = 19;
            this.buttonCheckedTracking.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonCheckedTracking.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonCheckedTracking.Values.Image")));
            this.buttonCheckedTracking.Values.Text = "Checked Tracking";
            this.buttonCheckedTracking.Click += new System.EventHandler(this.buttonCheckedTracking_Click);
            // 
            // buttonCheckedPressed
            // 
            this.buttonCheckedPressed.AutoSize = true;
            this.buttonCheckedPressed.Enabled = false;
            this.buttonCheckedPressed.Location = new System.Drawing.Point(340, 153);
            this.buttonCheckedPressed.Name = "buttonCheckedPressed";
            this.buttonCheckedPressed.Size = new System.Drawing.Size(147, 28);
            this.buttonCheckedPressed.TabIndex = 18;
            this.buttonCheckedPressed.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonCheckedPressed.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonCheckedPressed.Values.Image")));
            this.buttonCheckedPressed.Values.Text = "Checked Pressed";
            this.buttonCheckedPressed.Click += new System.EventHandler(this.buttonCheckedPressed_Click);
            // 
            // ButtonStyleExamples
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 261);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "ButtonStyleExamples";
            this.Text = "ButtonStyleExamples";
            this.Load += new System.EventHandler(this.ButtonStyleExamples_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonCheckButton buttonLive;
        private KryptonCheckButton buttonCheckedPressed;
        private KryptonCheckButton buttonCheckedTracking;
        private KryptonCheckButton buttonCheckedNormal;
        private KryptonCheckButton buttonDisabled;
        private KryptonCheckButton buttonPressed;
        private KryptonCheckButton buttonNormal;
        private KryptonCheckButton buttonTracking;
        private KryptonCheckButton buttonDefaultFocus;
        private KryptonLabel kryptonLabel1;
        private KryptonThemeComboBox kryptonThemeComboBox1;
    }
}
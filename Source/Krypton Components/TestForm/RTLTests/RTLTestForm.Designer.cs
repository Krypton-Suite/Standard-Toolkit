namespace TestForm.RTLTests
{
    partial class RTLTestForm
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
            this.kryptonCheckButton1 = new Krypton.Toolkit.KryptonCheckButton();
            this.kryptonCheckBox1 = new Krypton.Toolkit.KryptonCheckBox();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.kryptonPropertyGrid1 = new Krypton.Toolkit.KryptonPropertyGrid();
            this.ktsRTL = new Krypton.Toolkit.KryptonToggleSwitch();
            this.buttonSpecAny1 = new Krypton.Toolkit.ButtonSpecAny();
            this.buttonSpecAny2 = new Krypton.Toolkit.ButtonSpecAny();
            this.klblRTL = new Krypton.Toolkit.KryptonLabel();
            this.klblRTLLayout = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.klblRTLLayout);
            this.kryptonPanel1.Controls.Add(this.klblRTL);
            this.kryptonPanel1.Controls.Add(this.kryptonCheckButton1);
            this.kryptonPanel1.Controls.Add(this.kryptonCheckBox1);
            this.kryptonPanel1.Controls.Add(this.kryptonButton1);
            this.kryptonPanel1.Controls.Add(this.kryptonPropertyGrid1);
            this.kryptonPanel1.Controls.Add(this.ktsRTL);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(814, 452);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonCheckButton1
            // 
            this.kryptonCheckButton1.Location = new System.Drawing.Point(565, 57);
            this.kryptonCheckButton1.Name = "kryptonCheckButton1";
            this.kryptonCheckButton1.Size = new System.Drawing.Size(206, 25);
            this.kryptonCheckButton1.TabIndex = 4;
            this.kryptonCheckButton1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonCheckButton1.Values.Text = "kryptonCheckButton1";
            // 
            // kryptonCheckBox1
            // 
            this.kryptonCheckBox1.Location = new System.Drawing.Point(565, 108);
            this.kryptonCheckBox1.Name = "kryptonCheckBox1";
            this.kryptonCheckBox1.Size = new System.Drawing.Size(125, 20);
            this.kryptonCheckBox1.TabIndex = 3;
            this.kryptonCheckBox1.Values.Text = "kryptonCheckBox1";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(565, 25);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(206, 25);
            this.kryptonButton1.TabIndex = 2;
            this.kryptonButton1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton1.Values.Text = "kryptonButton1";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // kryptonPropertyGrid1
            // 
            this.kryptonPropertyGrid1.Location = new System.Drawing.Point(13, 13);
            this.kryptonPropertyGrid1.Name = "kryptonPropertyGrid1";
            this.kryptonPropertyGrid1.Padding = new System.Windows.Forms.Padding(1);
            this.kryptonPropertyGrid1.SelectedObject = this;
            this.kryptonPropertyGrid1.Size = new System.Drawing.Size(326, 413);
            this.kryptonPropertyGrid1.TabIndex = 1;
            this.kryptonPropertyGrid1.Text = "kryptonPropertyGrid1";
            this.kryptonPropertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.kryptonPropertyGrid1_PropertyValueChanged);
            // 
            // ktsRTL
            // 
            this.ktsRTL.Location = new System.Drawing.Point(345, 152);
            this.ktsRTL.Name = "ktsRTL";
            this.ktsRTL.Size = new System.Drawing.Size(90, 28);
            this.ktsRTL.TabIndex = 0;
            this.ktsRTL.CheckedChanged += new System.EventHandler(this.ktsRTL_CheckedChanged);
            // 
            // buttonSpecAny1
            // 
            this.buttonSpecAny1.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Previous;
            this.buttonSpecAny1.UniqueName = "4c02382cf86f4197ac6cc70654c2834e";
            // 
            // buttonSpecAny2
            // 
            this.buttonSpecAny2.Type = Krypton.Toolkit.PaletteButtonSpecStyle.ArrowLeft;
            this.buttonSpecAny2.UniqueName = "6707f9f6597349d1a4832a107cc5522b";
            // 
            // klblRTL
            // 
            this.klblRTL.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.klblRTL.Location = new System.Drawing.Point(565, 135);
            this.klblRTL.Name = "klblRTL";
            this.klblRTL.Size = new System.Drawing.Size(107, 20);
            this.klblRTL.TabIndex = 5;
            this.klblRTL.Values.Text = "Right to Left: {0}";
            // 
            // klblRTLLayout
            // 
            this.klblRTLLayout.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.klblRTLLayout.Location = new System.Drawing.Point(565, 161);
            this.klblRTLLayout.Name = "klblRTLLayout";
            this.klblRTLLayout.Size = new System.Drawing.Size(150, 20);
            this.klblRTLLayout.TabIndex = 6;
            this.klblRTLLayout.Values.Text = "Right to Left Layout: {0}";
            // 
            // RTLTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ButtonSpecs.Add(this.buttonSpecAny1);
            this.ButtonSpecs.Add(this.buttonSpecAny2);
            this.ClientSize = new System.Drawing.Size(814, 452);
            this.Controls.Add(this.kryptonPanel1);
            this.HelpButton = true;
            this.Name = "RTLTestForm";
            this.Text = "RTLTestForm";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonToggleSwitch ktsRTL;
        private ButtonSpecAny buttonSpecAny1;
        private ButtonSpecAny buttonSpecAny2;
        private KryptonPropertyGrid kryptonPropertyGrid1;
        private KryptonButton kryptonButton1;
        private KryptonCheckBox kryptonCheckBox1;
        private KryptonCheckButton kryptonCheckButton1;
        private KryptonLabel klblRTL;
        private KryptonLabel klblRTLLayout;
    }
}
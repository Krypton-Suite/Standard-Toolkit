namespace TestForm.RTLTests
{
    partial class RTLMirroringTest
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
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.kryptonCheckBox1 = new Krypton.Toolkit.KryptonCheckBox();
            this.kryptonToggleSwitch1 = new Krypton.Toolkit.KryptonToggleSwitch();
            this.kryptonToggleSwitch2 = new Krypton.Toolkit.KryptonToggleSwitch();
            this.kryptonCheckBox2 = new Krypton.Toolkit.KryptonCheckBox();
            this.kryptonButton2 = new Krypton.Toolkit.KryptonButton();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.ktsToggleRTL = new Krypton.Toolkit.KryptonToggleSwitch();
            this.kbtnToggleRTL = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnToggleRTL);
            this.kryptonPanel1.Controls.Add(this.ktsToggleRTL);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.kryptonToggleSwitch2);
            this.kryptonPanel1.Controls.Add(this.kryptonCheckBox2);
            this.kryptonPanel1.Controls.Add(this.kryptonButton2);
            this.kryptonPanel1.Controls.Add(this.kryptonToggleSwitch1);
            this.kryptonPanel1.Controls.Add(this.kryptonCheckBox1);
            this.kryptonPanel1.Controls.Add(this.kryptonButton1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(804, 438);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(13, 13);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(169, 25);
            this.kryptonButton1.TabIndex = 0;
            this.kryptonButton1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton1.Values.Text = "Left Button";
            // 
            // kryptonCheckBox1
            // 
            this.kryptonCheckBox1.Location = new System.Drawing.Point(13, 45);
            this.kryptonCheckBox1.Name = "kryptonCheckBox1";
            this.kryptonCheckBox1.Size = new System.Drawing.Size(100, 20);
            this.kryptonCheckBox1.TabIndex = 1;
            this.kryptonCheckBox1.Values.Text = "Left CheckBox";
            // 
            // kryptonToggleSwitch1
            // 
            this.kryptonToggleSwitch1.Location = new System.Drawing.Point(13, 72);
            this.kryptonToggleSwitch1.Name = "kryptonToggleSwitch1";
            this.kryptonToggleSwitch1.Size = new System.Drawing.Size(169, 28);
            this.kryptonToggleSwitch1.TabIndex = 2;
            // 
            // kryptonToggleSwitch2
            // 
            this.kryptonToggleSwitch2.Location = new System.Drawing.Point(623, 72);
            this.kryptonToggleSwitch2.Name = "kryptonToggleSwitch2";
            this.kryptonToggleSwitch2.Size = new System.Drawing.Size(169, 28);
            this.kryptonToggleSwitch2.TabIndex = 5;
            // 
            // kryptonCheckBox2
            // 
            this.kryptonCheckBox2.Location = new System.Drawing.Point(623, 45);
            this.kryptonCheckBox2.Name = "kryptonCheckBox2";
            this.kryptonCheckBox2.Size = new System.Drawing.Size(109, 20);
            this.kryptonCheckBox2.TabIndex = 4;
            this.kryptonCheckBox2.Values.Text = "Right CheckBox";
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.Location = new System.Drawing.Point(623, 13);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.Size = new System.Drawing.Size(169, 25);
            this.kryptonButton2.TabIndex = 3;
            this.kryptonButton2.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton2.Values.Text = "Right Button";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(285, 169);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(248, 20);
            this.kryptonLabel1.TabIndex = 6;
            this.kryptonLabel1.Values.Text = "Controls should mirror when RTL is enabled";
            // 
            // ktsToggleRTL
            // 
            this.ktsToggleRTL.Location = new System.Drawing.Point(372, 205);
            this.ktsToggleRTL.Name = "ktsToggleRTL";
            this.ktsToggleRTL.Size = new System.Drawing.Size(90, 28);
            this.ktsToggleRTL.TabIndex = 7;
            this.ktsToggleRTL.CheckedChanged += new System.EventHandler(this.ktsToggleRTL_CheckedChanged);
            // 
            // kbtnToggleRTL
            // 
            this.kbtnToggleRTL.Location = new System.Drawing.Point(372, 240);
            this.kbtnToggleRTL.Name = "kbtnToggleRTL";
            this.kbtnToggleRTL.Size = new System.Drawing.Size(90, 25);
            this.kbtnToggleRTL.TabIndex = 8;
            this.kbtnToggleRTL.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnToggleRTL.Values.Text = "kryptonButton3";
            this.kbtnToggleRTL.Click += new System.EventHandler(this.kbtnToggleRTL_Click);
            // 
            // RTLMirroringTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 438);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "RTLMirroringTest";
            this.Text = "RTL Mirroring Test - LTR Mode";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonButton kryptonButton1;
        private KryptonCheckBox kryptonCheckBox1;
        private KryptonToggleSwitch kryptonToggleSwitch1;
        private KryptonLabel kryptonLabel1;
        private KryptonToggleSwitch kryptonToggleSwitch2;
        private KryptonCheckBox kryptonCheckBox2;
        private KryptonButton kryptonButton2;
        private KryptonToggleSwitch ktsToggleRTL;
        private KryptonButton kbtnToggleRTL;
    }
}
namespace TestForm
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
            this.kbtnCenter = new Krypton.Toolkit.KryptonButton();
            this.kbtnRight = new Krypton.Toolkit.KryptonButton();
            this.kbtnLeft = new Krypton.Toolkit.KryptonButton();
            this.kbtnToggleRTL = new Krypton.Toolkit.KryptonButton();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnCenter);
            this.kryptonPanel1.Controls.Add(this.kbtnRight);
            this.kryptonPanel1.Controls.Add(this.kbtnLeft);
            this.kryptonPanel1.Controls.Add(this.kbtnToggleRTL);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(806, 432);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnCenter
            // 
            this.kbtnCenter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnCenter.Location = new System.Drawing.Point(120, 120);
            this.kbtnCenter.Name = "kbtnCenter";
            this.kbtnCenter.Size = new System.Drawing.Size(566, 194);
            this.kbtnCenter.TabIndex = 4;
            this.kbtnCenter.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnCenter.Values.Text = "Center Button (Anchored)";
            // 
            // kbtnRight
            // 
            this.kbtnRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.kbtnRight.Location = new System.Drawing.Point(706, 20);
            this.kbtnRight.Name = "kbtnRight";
            this.kbtnRight.Size = new System.Drawing.Size(100, 392);
            this.kbtnRight.TabIndex = 3;
            this.kbtnRight.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnRight.Values.Text = "Right Button";
            // 
            // kbtnLeft
            // 
            this.kbtnLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.kbtnLeft.Location = new System.Drawing.Point(0, 20);
            this.kbtnLeft.Name = "kbtnLeft";
            this.kbtnLeft.Size = new System.Drawing.Size(100, 392);
            this.kbtnLeft.TabIndex = 2;
            this.kbtnLeft.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnLeft.Values.Text = "Left Button";
            // 
            // kbtnToggleRTL
            // 
            this.kbtnToggleRTL.Location = new System.Drawing.Point(365, 90);
            this.kbtnToggleRTL.Name = "kbtnToggleRTL";
            this.kbtnToggleRTL.Size = new System.Drawing.Size(90, 25);
            this.kbtnToggleRTL.TabIndex = 1;
            this.kbtnToggleRTL.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnToggleRTL.Values.Text = "Toggle RTL";
            this.kbtnToggleRTL.Click += new System.EventHandler(this.kbtnToggleRTL_Click);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonLabel2.Location = new System.Drawing.Point(0, 412);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(806, 20);
            this.kryptonLabel2.TabIndex = 5;
            this.kryptonLabel2.Values.Text = "Bottom Label - Test RTL mirroring of docked controls";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonLabel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(806, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "This is a test form to demonstrate RTL support in KryptonForm.";
            // 
            // RTLTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 432);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "RTLTestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RTL Test Form";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonLabel kryptonLabel1;
        private KryptonButton kbtnToggleRTL;
        private KryptonButton kbtnLeft;
        private KryptonButton kbtnRight;
        private KryptonButton kbtnCenter;
        private KryptonLabel kryptonLabel2;
    }
}
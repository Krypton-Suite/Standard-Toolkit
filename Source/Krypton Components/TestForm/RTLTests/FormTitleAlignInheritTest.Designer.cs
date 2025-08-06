namespace TestForm.RTLTests
{
    partial class FormTitleAlignInheritTest
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
            this.klblTest = new Krypton.Toolkit.KryptonLabel();
            this.kbtnTest = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnTest);
            this.kryptonPanel1.Controls.Add(this.klblTest);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(802, 444);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // klblTest
            // 
            this.klblTest.Location = new System.Drawing.Point(13, 13);
            this.klblTest.Name = "klblTest";
            this.klblTest.Size = new System.Drawing.Size(223, 36);
            this.klblTest.TabIndex = 0;
            this.klblTest.Values.Text = "Testing FormTitleAlign = Inherit\r\nThis should not cause assertion failures";
            // 
            // kbtnTest
            // 
            this.kbtnTest.Location = new System.Drawing.Point(13, 56);
            this.kbtnTest.Name = "kbtnTest";
            this.kbtnTest.Size = new System.Drawing.Size(223, 25);
            this.kbtnTest.TabIndex = 1;
            this.kbtnTest.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnTest.Values.Text = "Test RTL Toggle";
            this.kbtnTest.Click += new System.EventHandler(this.kbtnTest_Click);
            // 
            // FormTitleAlignInheritTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 444);
            this.Controls.Add(this.kryptonPanel1);
            this.FormTitleAlign = Krypton.Toolkit.PaletteRelativeAlign.Inherit;
            this.Name = "FormTitleAlignInheritTest";
            this.Text = "FormTitleAlign Inherit Test";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonLabel klblTest;
        private KryptonButton kbtnTest;
    }
}
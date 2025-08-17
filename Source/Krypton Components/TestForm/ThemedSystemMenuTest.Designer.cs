namespace TestForm
{
    partial class ThemedSystemMenuTest
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
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonCheckBox1 = new Krypton.Toolkit.KryptonCheckBox();
            this.kryptonCheckBox2 = new Krypton.Toolkit.KryptonCheckBox();
            this.kryptonCheckBox3 = new Krypton.Toolkit.KryptonCheckBox();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.kryptonButton2 = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonButton2);
            this.kryptonPanel1.Controls.Add(this.kryptonButton1);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.kryptonCheckBox3);
            this.kryptonPanel1.Controls.Add(this.kryptonCheckBox2);
            this.kryptonPanel1.Controls.Add(this.kryptonCheckBox1);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(600, 400);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(20, 20);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(200, 23);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Themed System Menu Test";
            // 
            // kryptonCheckBox1
            // 
            this.kryptonCheckBox1.Location = new System.Drawing.Point(20, 60);
            this.kryptonCheckBox1.Name = "kryptonCheckBox1";
            this.kryptonCheckBox1.Size = new System.Drawing.Size(200, 20);
            this.kryptonCheckBox1.TabIndex = 1;
            this.kryptonCheckBox1.Values.Text = "Left-click on title bar";
            this.kryptonCheckBox1.CheckedChanged += new System.EventHandler(this.kryptonCheckBox1_CheckedChanged);
            // 
            // kryptonCheckBox2
            // 
            this.kryptonCheckBox2.Location = new System.Drawing.Point(20, 90);
            this.kryptonCheckBox2.Name = "kryptonCheckBox2";
            this.kryptonCheckBox2.Size = new System.Drawing.Size(200, 20);
            this.kryptonCheckBox2.TabIndex = 2;
            this.kryptonCheckBox2.Values.Text = "Right-click on title bar";
            this.kryptonCheckBox2.CheckedChanged += new System.EventHandler(this.kryptonCheckBox2_CheckedChanged);
            // 
            // kryptonCheckBox3
            // 
            this.kryptonCheckBox3.Location = new System.Drawing.Point(20, 120);
            this.kryptonCheckBox3.Name = "kryptonCheckBox3";
            this.kryptonCheckBox3.Size = new System.Drawing.Size(200, 20);
            this.kryptonCheckBox3.TabIndex = 3;
            this.kryptonCheckBox3.Values.Text = "Alt+Space keyboard shortcut";
            this.kryptonCheckBox3.CheckedChanged += new System.EventHandler(this.kryptonCheckBox3_CheckedChanged);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(20, 160);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(400, 60);
            this.kryptonLabel2.TabIndex = 4;
            this.kryptonLabel2.Values.Text = "Try clicking on the title bar (left or right click) or press Alt+Space to see the themed system menu in action!";

            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(20, 240);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(150, 25);
            this.kryptonButton1.TabIndex = 5;
            this.kryptonButton1.Values.Text = "Clear Custom Items";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);

            // kryptonButton2
            // 
            this.kryptonButton2.Location = new System.Drawing.Point(190, 240);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.Size = new System.Drawing.Size(150, 25);
            this.kryptonButton2.TabIndex = 6;
            this.kryptonButton2.Values.Text = "Show Menu Info";
            this.kryptonButton2.Click += new System.EventHandler(this.kryptonButton2_Click);
            // 
            // ThemedSystemMenuTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "ThemedSystemMenuTest";
            this.Text = "Themed System Menu Test";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonCheckBox kryptonCheckBox1;
        private Krypton.Toolkit.KryptonCheckBox kryptonCheckBox2;
        private Krypton.Toolkit.KryptonCheckBox kryptonCheckBox3;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonButton kryptonButton1;
        private Krypton.Toolkit.KryptonButton kryptonButton2;
    }
}

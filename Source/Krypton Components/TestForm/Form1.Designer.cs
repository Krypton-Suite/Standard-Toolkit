namespace TestForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonListBox1 = new Krypton.Toolkit.KryptonListBox();
            this.kryptonTextBox1 = new Krypton.Toolkit.KryptonTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonListBox1);
            this.kryptonPanel1.Controls.Add(this.kryptonTextBox1);
            this.kryptonPanel1.Controls.Add(this.textBox1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(800, 450);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonListBox1
            // 
            this.kryptonListBox1.Location = new System.Drawing.Point(270, 12);
            this.kryptonListBox1.Name = "kryptonListBox1";
            this.kryptonListBox1.Size = new System.Drawing.Size(518, 426);
            this.kryptonListBox1.TabIndex = 2;
            // 
            // kryptonTextBox1
            // 
            this.kryptonTextBox1.Location = new System.Drawing.Point(77, 128);
            this.kryptonTextBox1.Name = "kryptonTextBox1";
            this.kryptonTextBox1.Size = new System.Drawing.Size(100, 27);
            this.kryptonTextBox1.TabIndex = 1;
            this.kryptonTextBox1.Text = "kryptonTextBox1";
            this.kryptonTextBox1.Click += new System.EventHandler(this.kryptonTextBox1_Click);
            this.kryptonTextBox1.DoubleClick += new System.EventHandler(this.kryptonTextBox1_DoubleClick);
            this.kryptonTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.kryptonTextBox1_KeyDown);
            this.kryptonTextBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.kryptonTextBox1_KeyPress);
            this.kryptonTextBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.kryptonTextBox1_KeyUp);
            this.kryptonTextBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.kryptonTextBox1_MouseClick);
            this.kryptonTextBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.kryptonTextBox1_MouseDoubleClick);
            this.kryptonTextBox1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.kryptonTextBox1_PreviewKeyDown);
            this.kryptonTextBox1.Validating += new System.ComponentModel.CancelEventHandler(this.kryptonTextBox1_Validating);
            this.kryptonTextBox1.Validated += new System.EventHandler(this.kryptonTextBox1_Validated);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(73, 60);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 0;
            this.textBox1.Click += new System.EventHandler(this.textBox1_Click);
            this.textBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseClick);
            this.textBox1.DoubleClick += new System.EventHandler(this.textBox1_DoubleClick);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            this.textBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseDoubleClick);
            this.textBox1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textBox1_PreviewKeyDown);
            this.textBox1.Validating += new System.ComponentModel.CancelEventHandler(this.textBox1_Validating);
            this.textBox1.Validated += new System.EventHandler(this.textBox1_Validated);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.kryptonPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlackDarkMode;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonTextBox kryptonTextBox1;
        private System.Windows.Forms.TextBox textBox1;
        private Krypton.Toolkit.KryptonListBox kryptonListBox1;
    }
}
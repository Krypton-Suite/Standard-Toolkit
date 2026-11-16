namespace TestForm
{
    partial class KryptonTextBoxValidatingTest
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
            this.kbtnTestInstructions = new Krypton.Toolkit.KryptonButton();
            this.kbtnResetCounts = new Krypton.Toolkit.KryptonButton();
            this.klblTextBox2Count = new Krypton.Toolkit.KryptonLabel();
            this.klblTextBox1Count = new Krypton.Toolkit.KryptonLabel();
            this.klblTextBox2 = new Krypton.Toolkit.KryptonLabel();
            this.ktxtTextBox2 = new Krypton.Toolkit.KryptonTextBox();
            this.klblTextBox1 = new Krypton.Toolkit.KryptonLabel();
            this.ktxtTextBox1 = new Krypton.Toolkit.KryptonTextBox();
            this.klblInstructions = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnTestInstructions);
            this.kryptonPanel1.Controls.Add(this.kbtnResetCounts);
            this.kryptonPanel1.Controls.Add(this.klblTextBox2Count);
            this.kryptonPanel1.Controls.Add(this.klblTextBox1Count);
            this.kryptonPanel1.Controls.Add(this.klblTextBox2);
            this.kryptonPanel1.Controls.Add(this.ktxtTextBox2);
            this.kryptonPanel1.Controls.Add(this.klblTextBox1);
            this.kryptonPanel1.Controls.Add(this.ktxtTextBox1);
            this.kryptonPanel1.Controls.Add(this.klblInstructions);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(500, 300);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnTestInstructions
            // 
            this.kbtnTestInstructions.Location = new System.Drawing.Point(260, 230);
            this.kbtnTestInstructions.Name = "kbtnTestInstructions";
            this.kbtnTestInstructions.Size = new System.Drawing.Size(220, 35);
            this.kbtnTestInstructions.TabIndex = 8;
            this.kbtnTestInstructions.Values.Text = "Show Test Instructions";
            this.kbtnTestInstructions.Click += new System.EventHandler(this.kbtnTestInstructions_Click);
            // 
            // kbtnResetCounts
            // 
            this.kbtnResetCounts.Location = new System.Drawing.Point(20, 230);
            this.kbtnResetCounts.Name = "kbtnResetCounts";
            this.kbtnResetCounts.Size = new System.Drawing.Size(220, 35);
            this.kbtnResetCounts.TabIndex = 7;
            this.kbtnResetCounts.Values.Text = "Reset Event Counts";
            this.kbtnResetCounts.Click += new System.EventHandler(this.kbtnResetCounts_Click);
            // 
            // klblTextBox2Count
            // 
            this.klblTextBox2Count.Location = new System.Drawing.Point(260, 170);
            this.klblTextBox2Count.Name = "klblTextBox2Count";
            this.klblTextBox2Count.Size = new System.Drawing.Size(220, 24);
            this.klblTextBox2Count.TabIndex = 6;
            this.klblTextBox2Count.Values.Text = "Validating events: 0";
            // 
            // klblTextBox1Count
            // 
            this.klblTextBox1Count.Location = new System.Drawing.Point(20, 170);
            this.klblTextBox1Count.Name = "klblTextBox1Count";
            this.klblTextBox1Count.Size = new System.Drawing.Size(220, 24);
            this.klblTextBox1Count.TabIndex = 5;
            this.klblTextBox1Count.Values.Text = "Validating events: 0";
            // 
            // klblTextBox2
            // 
            this.klblTextBox2.Location = new System.Drawing.Point(260, 95);
            this.klblTextBox2.Name = "klblTextBox2";
            this.klblTextBox2.Size = new System.Drawing.Size(220, 24);
            this.klblTextBox2.TabIndex = 4;
            this.klblTextBox2.Values.Text = "TextBox 2:";
            // 
            // ktxtTextBox2
            // 
            this.ktxtTextBox2.Location = new System.Drawing.Point(260, 125);
            this.ktxtTextBox2.Name = "ktxtTextBox2";
            this.ktxtTextBox2.Size = new System.Drawing.Size(220, 23);
            this.ktxtTextBox2.TabIndex = 2;
            this.ktxtTextBox2.Validated += new System.EventHandler(this.ktxtTextBox2_Validated);
            this.ktxtTextBox2.Validating += new System.ComponentModel.CancelEventHandler(this.ktxtTextBox2_Validating);
            // 
            // klblTextBox1
            // 
            this.klblTextBox1.Location = new System.Drawing.Point(20, 95);
            this.klblTextBox1.Name = "klblTextBox1";
            this.klblTextBox1.Size = new System.Drawing.Size(220, 24);
            this.klblTextBox1.TabIndex = 2;
            this.klblTextBox1.Values.Text = "TextBox 1:";
            // 
            // ktxtTextBox1
            // 
            this.ktxtTextBox1.Location = new System.Drawing.Point(20, 125);
            this.ktxtTextBox1.Name = "ktxtTextBox1";
            this.ktxtTextBox1.Size = new System.Drawing.Size(220, 23);
            this.ktxtTextBox1.TabIndex = 1;
            this.ktxtTextBox1.Validated += new System.EventHandler(this.ktxtTextBox1_Validated);
            this.ktxtTextBox1.Validating += new System.ComponentModel.CancelEventHandler(this.ktxtTextBox1_Validating);
            // 
            // klblInstructions
            // 
            this.klblInstructions.Location = new System.Drawing.Point(20, 20);
            this.klblInstructions.Name = "klblInstructions";
            this.klblInstructions.Size = new System.Drawing.Size(460, 60);
            this.klblInstructions.TabIndex = 0;
            this.klblInstructions.Values.Text = "KryptonTextBox Validating Event Test (Bug #2801 Fix)\r\n\r\nMove focus between the two textboxes using Tab or mouse click.\r\nEach textbox should raise Validating event ONCE per focus change.";
            // 
            // KryptonTextBoxValidatingTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 300);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KryptonTextBoxValidatingTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KryptonTextBox Validating Event Test - Bug #2801";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonLabel klblInstructions;
        private Krypton.Toolkit.KryptonTextBox ktxtTextBox1;
        private Krypton.Toolkit.KryptonLabel klblTextBox1;
        private Krypton.Toolkit.KryptonLabel klblTextBox2;
        private Krypton.Toolkit.KryptonTextBox ktxtTextBox2;
        private Krypton.Toolkit.KryptonLabel klblTextBox1Count;
        private Krypton.Toolkit.KryptonLabel klblTextBox2Count;
        private Krypton.Toolkit.KryptonButton kbtnResetCounts;
        private Krypton.Toolkit.KryptonButton kbtnTestInstructions;
    }
}
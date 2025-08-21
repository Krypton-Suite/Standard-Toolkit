namespace TestForm
{
    partial class PropertyGridTest
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
            this.kryptonGroupBox1 = new Krypton.Toolkit.KryptonGroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kpgExample = new Krypton.Toolkit.KryptonPropertyGrid();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.kbtnStressTest = new Krypton.Toolkit.KryptonButton();
            this.kbtnStressTestBeforeFix = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.kryptonPanel1.Size = new System.Drawing.Size(425, 634);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroupBox1.Location = new System.Drawing.Point(5, 5);
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.tableLayoutPanel1);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(415, 624);
            this.kryptonGroupBox1.TabIndex = 0;
            this.kryptonGroupBox1.Values.Heading = "Property Grid";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.kpgExample, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.kryptonThemeComboBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.kbtnStressTest, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.kbtnStressTestBeforeFix, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(411, 600);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // kpgExample
            // 
            this.kpgExample.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpgExample.Location = new System.Drawing.Point(3, 3);
            this.kpgExample.Name = "kpgExample";
            this.kpgExample.Padding = new System.Windows.Forms.Padding(1);
            this.kpgExample.SelectedObject = this;
            this.kpgExample.Size = new System.Drawing.Size(405, 504);
            this.kpgExample.TabIndex = 0;
            // 
            // kryptonThemeComboBox1
            // 
            this.kryptonThemeComboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonThemeComboBox1.DefaultPalette = Krypton.Toolkit.PaletteMode.Microsoft365Blue;
            this.kryptonThemeComboBox1.DropDownWidth = 276;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(3, 513);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(405, 22);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 1;
            // 
            // kbtnStressTest
            // 
            this.kbtnStressTest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnStressTest.Location = new System.Drawing.Point(3, 541);
            this.kbtnStressTest.Name = "kbtnStressTest";
            this.kbtnStressTest.Size = new System.Drawing.Size(405, 25);
            this.kbtnStressTest.TabIndex = 2;
            this.kbtnStressTest.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnStressTest.Values.Text = "Run Drawing Stress Test (After Fix)";
            this.kbtnStressTest.Click += new System.EventHandler(this.kbtnStressTest_Click);
            // 
            // kbtnStressTestBeforeFix
            // 
            this.kbtnStressTestBeforeFix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnStressTestBeforeFix.Location = new System.Drawing.Point(3, 572);
            this.kbtnStressTestBeforeFix.Name = "kbtnStressTestBeforeFix";
            this.kbtnStressTestBeforeFix.Size = new System.Drawing.Size(405, 25);
            this.kbtnStressTestBeforeFix.TabIndex = 3;
            this.kbtnStressTestBeforeFix.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnStressTestBeforeFix.Values.Text = "Run Drawing Stress Test (Before Fix)";
            this.kbtnStressTestBeforeFix.Click += new System.EventHandler(this.kbtnStressTestBeforeFix_Click);
            // 
            // PropertyGridTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 634);
            this.Controls.Add(this.kryptonPanel1);
            this.MinimumSize = new System.Drawing.Size(310, 350);
            this.Name = "PropertyGridTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PropertyGridTest";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Krypton.Toolkit.KryptonPropertyGrid kpgExample;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
        private Krypton.Toolkit.KryptonButton kbtnStressTest;
        private Krypton.Toolkit.KryptonButton kbtnStressTestBeforeFix;
    }
}
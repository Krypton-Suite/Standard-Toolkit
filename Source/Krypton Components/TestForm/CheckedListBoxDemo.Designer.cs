namespace TestForm
{
    partial class CheckedListBoxDemo
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
            this.kbtnGetChecked = new Krypton.Toolkit.KryptonButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.klblNotification = new Krypton.Toolkit.KryptonLabel();
            this.kryptonCheckedListBox1 = new Krypton.Toolkit.KryptonCheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnGetChecked);
            this.kryptonPanel1.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 497);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(812, 50);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnGetChecked
            // 
            this.kbtnGetChecked.Location = new System.Drawing.Point(637, 13);
            this.kbtnGetChecked.Name = "kbtnGetChecked";
            this.kbtnGetChecked.Size = new System.Drawing.Size(155, 25);
            this.kbtnGetChecked.TabIndex = 1;
            this.kbtnGetChecked.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnGetChecked.Values.Text = "Show Checked IDs";
            this.kbtnGetChecked.Click += new System.EventHandler(this.kbtnGetChecked_Click);
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(812, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(812, 497);
            this.kryptonPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.klblNotification, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.kryptonCheckedListBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(812, 497);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // klblNotification
            // 
            this.klblNotification.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblNotification.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.klblNotification.Location = new System.Drawing.Point(3, 492);
            this.klblNotification.Name = "klblNotification";
            this.klblNotification.Size = new System.Drawing.Size(806, 2);
            this.klblNotification.TabIndex = 0;
            this.klblNotification.Values.Text = "";
            // 
            // kryptonCheckedListBox1
            // 
            this.kryptonCheckedListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonCheckedListBox1.Location = new System.Drawing.Point(3, 3);
            this.kryptonCheckedListBox1.Name = "kryptonCheckedListBox1";
            this.kryptonCheckedListBox1.Size = new System.Drawing.Size(806, 483);
            this.kryptonCheckedListBox1.TabIndex = 1;
            // 
            // CheckedListBoxDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 547);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "CheckedListBoxDemo";
            this.Text = "CheckedListBoxDemo";
            this.Load += new System.EventHandler(this.CheckedListBoxDemo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonPanel kryptonPanel2;
        private TableLayoutPanel tableLayoutPanel1;
        private KryptonLabel klblNotification;
        private KryptonCheckedListBox kryptonCheckedListBox1;
        private KryptonButton kbtnGetChecked;
        private KryptonBorderEdge kryptonBorderEdge1;
    }
}
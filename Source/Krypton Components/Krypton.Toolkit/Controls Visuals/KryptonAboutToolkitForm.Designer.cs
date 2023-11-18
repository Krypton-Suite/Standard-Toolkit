namespace Krypton.Toolkit
{
    partial class KryptonAboutToolkitForm
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
            kryptonPanel1 = new KryptonPanel();
            kryptonBorderEdge1 = new KryptonBorderEdge();
            tableLayoutPanel1 = new TableLayoutPanel();
            kryptonPanel2 = new KryptonPanel();
            kryptonButton1 = new KryptonButton();
            kryptonButton2 = new KryptonButton();
            kryptonHeaderGroup1 = new KryptonHeaderGroup();
            BottomToolStripPanel = new ToolStripPanel();
            TopToolStripPanel = new ToolStripPanel();
            RightToolStripPanel = new ToolStripPanel();
            LeftToolStripPanel = new ToolStripPanel();
            ContentPanel = new ToolStripContentPanel();
            ((ISupportInitialize)kryptonPanel1).BeginInit();
            kryptonPanel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((ISupportInitialize)kryptonPanel2).BeginInit();
            kryptonPanel2.SuspendLayout();
            ((ISupportInitialize)kryptonHeaderGroup1).BeginInit();
            ((ISupportInitialize)kryptonHeaderGroup1.Panel).BeginInit();
            SuspendLayout();
            // 
            // kryptonPanel1
            // 
            kryptonPanel1.Controls.Add(tableLayoutPanel1);
            kryptonPanel1.Controls.Add(kryptonBorderEdge1);
            kryptonPanel1.Dock = DockStyle.Bottom;
            kryptonPanel1.Location = new Point(0, 400);
            kryptonPanel1.Name = "kryptonPanel1";
            kryptonPanel1.PanelBackStyle = PaletteBackStyle.PanelAlternate;
            kryptonPanel1.Size = new Size(800, 50);
            kryptonPanel1.TabIndex = 0;
            // 
            // kryptonBorderEdge1
            // 
            kryptonBorderEdge1.BorderStyle = PaletteBorderStyle.HeaderSecondary;
            kryptonBorderEdge1.Dock = DockStyle.Top;
            kryptonBorderEdge1.Location = new Point(0, 0);
            kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            kryptonBorderEdge1.Size = new Size(800, 1);
            kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.Transparent;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(kryptonButton1, 1, 0);
            tableLayoutPanel1.Controls.Add(kryptonButton2, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 1);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(800, 49);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // kryptonPanel2
            // 
            kryptonPanel2.Controls.Add(kryptonHeaderGroup1);
            kryptonPanel2.Dock = DockStyle.Fill;
            kryptonPanel2.Location = new Point(0, 0);
            kryptonPanel2.Name = "kryptonPanel2";
            kryptonPanel2.Size = new Size(800, 400);
            kryptonPanel2.TabIndex = 1;
            // 
            // kryptonButton1
            // 
            kryptonButton1.Anchor = AnchorStyles.Right;
            kryptonButton1.Location = new Point(700, 12);
            kryptonButton1.Margin = new Padding(10);
            kryptonButton1.Name = "kryptonButton1";
            kryptonButton1.Size = new Size(90, 25);
            kryptonButton1.TabIndex = 0;
            kryptonButton1.Values.Text = "kryptonButton1";
            // 
            // kryptonButton2
            // 
            kryptonButton2.Anchor = AnchorStyles.Right;
            kryptonButton2.Location = new Point(590, 12);
            kryptonButton2.Margin = new Padding(10);
            kryptonButton2.Name = "kryptonButton2";
            kryptonButton2.Size = new Size(90, 25);
            kryptonButton2.TabIndex = 1;
            kryptonButton2.Values.Text = "kryptonButton2";
            // 
            // kryptonHeaderGroup1
            // 
            kryptonHeaderGroup1.HeaderVisibleSecondary = false;
            kryptonHeaderGroup1.Location = new Point(12, 8);
            kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            kryptonHeaderGroup1.Size = new Size(778, 386);
            kryptonHeaderGroup1.TabIndex = 0;
            kryptonHeaderGroup1.ValuesPrimary.Image = null;
            // 
            // BottomToolStripPanel
            // 
            BottomToolStripPanel.Location = new Point(0, 0);
            BottomToolStripPanel.Name = "BottomToolStripPanel";
            BottomToolStripPanel.Orientation = Orientation.Horizontal;
            BottomToolStripPanel.RowMargin = new Padding(3, 0, 0, 0);
            BottomToolStripPanel.Size = new Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            TopToolStripPanel.Location = new Point(0, 0);
            TopToolStripPanel.Name = "TopToolStripPanel";
            TopToolStripPanel.Orientation = Orientation.Horizontal;
            TopToolStripPanel.RowMargin = new Padding(3, 0, 0, 0);
            TopToolStripPanel.Size = new Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            RightToolStripPanel.Location = new Point(0, 0);
            RightToolStripPanel.Name = "RightToolStripPanel";
            RightToolStripPanel.Orientation = Orientation.Horizontal;
            RightToolStripPanel.RowMargin = new Padding(3, 0, 0, 0);
            RightToolStripPanel.Size = new Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            LeftToolStripPanel.Location = new Point(0, 0);
            LeftToolStripPanel.Name = "LeftToolStripPanel";
            LeftToolStripPanel.Orientation = Orientation.Horizontal;
            LeftToolStripPanel.RowMargin = new Padding(3, 0, 0, 0);
            LeftToolStripPanel.Size = new Size(0, 0);
            // 
            // ContentPanel
            // 
            ContentPanel.Size = new Size(150, 150);
            // 
            // KryptonAboutToolkitForm
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(kryptonPanel2);
            Controls.Add(kryptonPanel1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "KryptonAboutToolkitForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            ((ISupportInitialize)kryptonPanel1).EndInit();
            kryptonPanel1.ResumeLayout(false);
            kryptonPanel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ((ISupportInitialize)kryptonPanel2).EndInit();
            kryptonPanel2.ResumeLayout(false);
            ((ISupportInitialize)kryptonHeaderGroup1.Panel).EndInit();
            ((ISupportInitialize)kryptonHeaderGroup1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonBorderEdge kryptonBorderEdge1;
        private TableLayoutPanel tableLayoutPanel1;
        private KryptonPanel kryptonPanel2;
        private KryptonButton kryptonButton1;
        private KryptonButton kryptonButton2;
        private KryptonHeaderGroup kryptonHeaderGroup1;
        private ToolStripPanel BottomToolStripPanel;
        private ToolStripPanel TopToolStripPanel;
        private ToolStripPanel RightToolStripPanel;
        private ToolStripPanel LeftToolStripPanel;
        private ToolStripContentPanel ContentPanel;
    }
}
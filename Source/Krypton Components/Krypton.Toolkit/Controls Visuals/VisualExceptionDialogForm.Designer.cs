namespace Krypton.Toolkit
{
    partial class VisualExceptionDialogForm
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
            this.kbtnCopy = new Krypton.Toolkit.KryptonButton();
            this.kbtnOk = new Krypton.Toolkit.KryptonButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kwlblExceptionOutline = new Krypton.Toolkit.KryptonWrapLabel();
            this.kwlblExceptionDetails = new Krypton.Toolkit.KryptonWrapLabel();
            this.kryptonSeparator1 = new Krypton.Toolkit.KryptonSeparator();
            this.rtbExceptionDetails = new System.Windows.Forms.RichTextBox();
            this.isbSearchArea = new Krypton.Toolkit.InternalSearchableExceptionWinFormsTreeView();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSeparator1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnCopy);
            this.kryptonPanel1.Controls.Add(this.kbtnOk);
            this.kryptonPanel1.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 491);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(1137, 50);
            this.kryptonPanel1.TabIndex = 1;
            // 
            // kbtnCopy
            // 
            this.kbtnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnCopy.Enabled = false;
            this.kbtnCopy.Location = new System.Drawing.Point(939, 13);
            this.kbtnCopy.Name = "kbtnCopy";
            this.kbtnCopy.Size = new System.Drawing.Size(90, 25);
            this.kbtnCopy.TabIndex = 2;
            this.kbtnCopy.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnCopy.Values.Text = "kryptonButton1";
            this.kbtnCopy.Click += new System.EventHandler(this.kbtnCopy_Click);
            // 
            // kbtnOk
            // 
            this.kbtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnOk.Location = new System.Drawing.Point(1035, 13);
            this.kbtnOk.Name = "kbtnOk";
            this.kbtnOk.Size = new System.Drawing.Size(90, 25);
            this.kbtnOk.TabIndex = 1;
            this.kbtnOk.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnOk.Values.Text = "kryptonButton1";
            this.kbtnOk.Click += new System.EventHandler(this.kbtnOk_Click);
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(1137, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(1137, 491);
            this.kryptonPanel2.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.kwlblExceptionOutline, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.kwlblExceptionDetails, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.kryptonSeparator1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.rtbExceptionDetails, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.isbSearchArea, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1137, 491);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // kwlblExceptionOutline
            // 
            this.kwlblExceptionOutline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblExceptionOutline.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.kwlblExceptionOutline.Location = new System.Drawing.Point(5, 5);
            this.kwlblExceptionOutline.Margin = new System.Windows.Forms.Padding(5);
            this.kwlblExceptionOutline.Name = "kwlblExceptionOutline";
            this.kwlblExceptionOutline.Size = new System.Drawing.Size(231, 25);
            this.kwlblExceptionOutline.Text = "kryptonWrapLabel1";
            this.kwlblExceptionOutline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // kwlblExceptionDetails
            // 
            this.kwlblExceptionDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblExceptionDetails.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.kwlblExceptionDetails.Location = new System.Drawing.Point(261, 5);
            this.kwlblExceptionDetails.Margin = new System.Windows.Forms.Padding(5);
            this.kwlblExceptionDetails.Name = "kwlblExceptionDetails";
            this.kwlblExceptionDetails.Size = new System.Drawing.Size(871, 25);
            this.kwlblExceptionDetails.Text = "kryptonWrapLabel2";
            this.kwlblExceptionDetails.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // kryptonSeparator1
            // 
            this.kryptonSeparator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonSeparator1.Location = new System.Drawing.Point(246, 40);
            this.kryptonSeparator1.Margin = new System.Windows.Forms.Padding(5);
            this.kryptonSeparator1.Name = "kryptonSeparator1";
            this.kryptonSeparator1.Size = new System.Drawing.Size(5, 446);
            this.kryptonSeparator1.TabIndex = 2;
            // 
            // rtbExceptionDetails
            // 
            this.rtbExceptionDetails.BackColor = System.Drawing.SystemColors.Control;
            this.rtbExceptionDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbExceptionDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbExceptionDetails.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbExceptionDetails.Location = new System.Drawing.Point(259, 38);
            this.rtbExceptionDetails.Name = "rtbExceptionDetails";
            this.rtbExceptionDetails.ReadOnly = true;
            this.rtbExceptionDetails.Size = new System.Drawing.Size(875, 450);
            this.rtbExceptionDetails.TabIndex = 4;
            this.rtbExceptionDetails.Text = "";
            this.rtbExceptionDetails.TextChanged += new System.EventHandler(this.rtbExceptionDetails_TextChanged);
            // 
            // isbSearchArea
            // 
            this.isbSearchArea.BackColor = System.Drawing.Color.Transparent;
            this.isbSearchArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.isbSearchArea.Location = new System.Drawing.Point(3, 38);
            this.isbSearchArea.Name = "isbSearchArea";
            this.isbSearchArea.Size = new System.Drawing.Size(235, 450);
            this.isbSearchArea.TabIndex = 5;
            this.isbSearchArea.NodeSelected += new System.Windows.Forms.TreeViewEventHandler(this.isbSearchArea_NodeSelected);
            // 
            // VisualExceptionDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 541);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualExceptionDialogForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VisualExceptionDialogForm";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSeparator1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonButton kbtnCopy;
        private KryptonButton kbtnOk;
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonPanel kryptonPanel2;
        private TableLayoutPanel tableLayoutPanel1;
        private KryptonWrapLabel kwlblExceptionOutline;
        private KryptonWrapLabel kwlblExceptionDetails;
        private KryptonSeparator kryptonSeparator1;
        private RichTextBox rtbExceptionDetails;
        private InternalSearchableExceptionWinFormsTreeView isbSearchArea;
    }
}
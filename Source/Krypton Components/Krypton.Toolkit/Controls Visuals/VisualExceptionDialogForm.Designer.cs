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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.kryptonTextBox1 = new Krypton.Toolkit.KryptonTextBox();
            this.etvExceptionOutline = new Krypton.Toolkit.InternalExceptionWinFormsTreeView();
            this.bsaClear = new Krypton.Toolkit.ButtonSpecAny();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSeparator1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnCopy);
            this.kryptonPanel1.Controls.Add(this.kbtnOk);
            this.kryptonPanel1.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 582);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(1106, 50);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnCopy
            // 
            this.kbtnCopy.Enabled = false;
            this.kbtnCopy.Location = new System.Drawing.Point(896, 13);
            this.kbtnCopy.Name = "kbtnCopy";
            this.kbtnCopy.Size = new System.Drawing.Size(90, 25);
            this.kbtnCopy.TabIndex = 2;
            this.kbtnCopy.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnCopy.Values.Text = "kryptonButton1";
            this.kbtnCopy.Click += new System.EventHandler(this.kbtnCopy_Click);
            // 
            // kbtnOk
            // 
            this.kbtnOk.Location = new System.Drawing.Point(992, 13);
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
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(1106, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(1106, 582);
            this.kryptonPanel2.TabIndex = 1;
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
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1106, 582);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // kwlblExceptionOutline
            // 
            this.kwlblExceptionOutline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblExceptionOutline.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.kwlblExceptionOutline.Location = new System.Drawing.Point(5, 5);
            this.kwlblExceptionOutline.Margin = new System.Windows.Forms.Padding(5);
            this.kwlblExceptionOutline.Name = "kwlblExceptionOutline";
            this.kwlblExceptionOutline.Size = new System.Drawing.Size(196, 25);
            this.kwlblExceptionOutline.Text = "kryptonWrapLabel1";
            this.kwlblExceptionOutline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // kwlblExceptionDetails
            // 
            this.kwlblExceptionDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblExceptionDetails.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.kwlblExceptionDetails.Location = new System.Drawing.Point(226, 5);
            this.kwlblExceptionDetails.Margin = new System.Windows.Forms.Padding(5);
            this.kwlblExceptionDetails.Name = "kwlblExceptionDetails";
            this.kwlblExceptionDetails.Size = new System.Drawing.Size(875, 25);
            this.kwlblExceptionDetails.Text = "kryptonWrapLabel2";
            this.kwlblExceptionDetails.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // kryptonSeparator1
            // 
            this.kryptonSeparator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonSeparator1.Location = new System.Drawing.Point(211, 40);
            this.kryptonSeparator1.Margin = new System.Windows.Forms.Padding(5);
            this.kryptonSeparator1.Name = "kryptonSeparator1";
            this.kryptonSeparator1.Size = new System.Drawing.Size(5, 537);
            this.kryptonSeparator1.TabIndex = 2;
            // 
            // rtbExceptionDetails
            // 
            this.rtbExceptionDetails.BackColor = System.Drawing.SystemColors.Control;
            this.rtbExceptionDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbExceptionDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbExceptionDetails.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbExceptionDetails.Location = new System.Drawing.Point(224, 38);
            this.rtbExceptionDetails.Name = "rtbExceptionDetails";
            this.rtbExceptionDetails.ReadOnly = true;
            this.rtbExceptionDetails.Size = new System.Drawing.Size(879, 541);
            this.rtbExceptionDetails.TabIndex = 4;
            this.rtbExceptionDetails.Text = "";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.etvExceptionOutline, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.kryptonTextBox1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 38);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(200, 541);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // kryptonTextBox1
            // 
            this.kryptonTextBox1.ButtonSpecs.Add(this.bsaClear);
            this.kryptonTextBox1.CueHint.CueHintText = "Search...";
            this.kryptonTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonTextBox1.Location = new System.Drawing.Point(5, 5);
            this.kryptonTextBox1.Margin = new System.Windows.Forms.Padding(5);
            this.kryptonTextBox1.Name = "kryptonTextBox1";
            this.kryptonTextBox1.Size = new System.Drawing.Size(190, 24);
            this.kryptonTextBox1.TabIndex = 0;
            // 
            // etvExceptionOutline
            // 
            this.etvExceptionOutline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.etvExceptionOutline.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.etvExceptionOutline.Location = new System.Drawing.Point(3, 36);
            this.etvExceptionOutline.Name = "etvExceptionOutline";
            this.etvExceptionOutline.Size = new System.Drawing.Size(194, 502);
            this.etvExceptionOutline.TabIndex = 4;
            // 
            // bsaClear
            // 
            this.bsaClear.Text = "X";
            this.bsaClear.UniqueName = "f861f1fa9be044aea62c2dfe261de741";
            // 
            // VisualExceptionDialogForm
            // 
            this.AcceptButton = this.kbtnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 632);
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
            this.Load += new System.EventHandler(this.VisualExceptionDialogForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSeparator1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonPanel kryptonPanel2;
        private TableLayoutPanel tableLayoutPanel1;
        private KryptonWrapLabel kwlblExceptionOutline;
        private KryptonWrapLabel kwlblExceptionDetails;
        private KryptonSeparator kryptonSeparator1;
        private RichTextBox rtbExceptionDetails;
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonButton kbtnOk;
        private KryptonButton kbtnCopy;
        private TableLayoutPanel tableLayoutPanel2;
        private KryptonTextBox kryptonTextBox1;
        private InternalExceptionWinFormsTreeView etvExceptionOutline;
        private ButtonSpecAny bsaClear;
    }
}
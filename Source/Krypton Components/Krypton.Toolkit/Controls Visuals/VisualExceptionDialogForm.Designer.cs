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
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kbtnOk = new Krypton.Toolkit.KryptonButton();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kwlblExceptionOutline = new Krypton.Toolkit.KryptonWrapLabel();
            this.kwlblExceptionDetails = new Krypton.Toolkit.KryptonWrapLabel();
            this.kbtnCopy = new Krypton.Toolkit.KryptonButton();
            this.ketvExceptionOutline = new Krypton.Toolkit.InternalExceptionTreeView();
            this.krtbExceptionDetails = new Krypton.Toolkit.KryptonRichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnOk);
            this.kryptonPanel1.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 554);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(1056, 50);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(1056, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kbtnOk
            // 
            this.kbtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnOk.AutoSize = true;
            this.kbtnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.kbtnOk.Location = new System.Drawing.Point(954, 13);
            this.kbtnOk.Name = "kbtnOk";
            this.kbtnOk.Size = new System.Drawing.Size(90, 25);
            this.kbtnOk.TabIndex = 1;
            this.kbtnOk.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnOk.Values.Text = "O&K";
            this.kbtnOk.Values.UseAsADialogButton = true;
            this.kbtnOk.Click += new System.EventHandler(this.kbtnOk_Click);
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(1056, 554);
            this.kryptonPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.kwlblExceptionOutline, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.kwlblExceptionDetails, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.kbtnCopy, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.ketvExceptionOutline, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.krtbExceptionDetails, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1056, 554);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // kwlblExceptionOutline
            // 
            this.kwlblExceptionOutline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblExceptionOutline.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.kwlblExceptionOutline.Location = new System.Drawing.Point(5, 5);
            this.kwlblExceptionOutline.Margin = new System.Windows.Forms.Padding(5);
            this.kwlblExceptionOutline.Name = "kwlblExceptionOutline";
            this.kwlblExceptionOutline.Size = new System.Drawing.Size(266, 25);
            this.kwlblExceptionOutline.Text = "Exception Outline";
            this.kwlblExceptionOutline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // kwlblExceptionDetails
            // 
            this.kwlblExceptionDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblExceptionDetails.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.kwlblExceptionDetails.Location = new System.Drawing.Point(281, 5);
            this.kwlblExceptionDetails.Margin = new System.Windows.Forms.Padding(5);
            this.kwlblExceptionDetails.Name = "kwlblExceptionDetails";
            this.kwlblExceptionDetails.Size = new System.Drawing.Size(770, 25);
            this.kwlblExceptionDetails.Text = "Exception Details";
            this.kwlblExceptionDetails.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // kbtnCopy
            // 
            this.kbtnCopy.AutoSize = true;
            this.kbtnCopy.Location = new System.Drawing.Point(281, 527);
            this.kbtnCopy.Margin = new System.Windows.Forms.Padding(5);
            this.kbtnCopy.Name = "kbtnCopy";
            this.kbtnCopy.Size = new System.Drawing.Size(94, 22);
            this.kbtnCopy.TabIndex = 2;
            this.kbtnCopy.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnCopy.Values.Text = "kryptonButton1";
            this.kbtnCopy.Click += new System.EventHandler(this.kbtnCopy_Click);
            // 
            // ketvExceptionOutline
            // 
            this.ketvExceptionOutline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ketvExceptionOutline.Location = new System.Drawing.Point(5, 40);
            this.ketvExceptionOutline.Margin = new System.Windows.Forms.Padding(5);
            this.ketvExceptionOutline.Name = "ketvExceptionOutline";
            this.ketvExceptionOutline.Size = new System.Drawing.Size(266, 477);
            this.ketvExceptionOutline.TabIndex = 3;
            this.ketvExceptionOutline.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ketvExceptionOutline_AfterSelect);
            // 
            // krtbExceptionDetails
            // 
            this.krtbExceptionDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.krtbExceptionDetails.InputControlStyle = Krypton.Toolkit.InputControlStyle.PanelClient;
            this.krtbExceptionDetails.Location = new System.Drawing.Point(281, 40);
            this.krtbExceptionDetails.Margin = new System.Windows.Forms.Padding(5);
            this.krtbExceptionDetails.Name = "krtbExceptionDetails";
            this.krtbExceptionDetails.ReadOnly = true;
            this.krtbExceptionDetails.Size = new System.Drawing.Size(770, 477);
            this.krtbExceptionDetails.TabIndex = 4;
            this.krtbExceptionDetails.Text = "kryptonRichTextBox1";
            // 
            // VisualExceptionDialogForm
            // 
            this.AcceptButton = this.kbtnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 604);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualExceptionDialogForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "VisualExceptionDialogForm";
            this.Load += new System.EventHandler(this.VisualExceptionDialogForm_Load);
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
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonButton kbtnOk;
        private KryptonPanel kryptonPanel2;
        private TableLayoutPanel tableLayoutPanel1;
        private KryptonWrapLabel kwlblExceptionOutline;
        private KryptonWrapLabel kwlblExceptionDetails;
        private KryptonButton kbtnCopy;
        private InternalExceptionTreeView ketvExceptionOutline;
        private KryptonRichTextBox krtbExceptionDetails;
    }
}
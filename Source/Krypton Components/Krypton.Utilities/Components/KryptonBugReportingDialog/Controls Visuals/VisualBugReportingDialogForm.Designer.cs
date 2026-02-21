namespace Krypton.Utilities
{
    partial class VisualBugReportingDialogForm
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.kbtnAddFile = new Krypton.Toolkit.KryptonButton();
            this.kbtnSend = new Krypton.Toolkit.KryptonButton();
            this.kbtnCancel = new Krypton.Toolkit.KryptonButton();
            this.kbtnRemove = new Krypton.Toolkit.KryptonButton();
            this.kbtnAddScreenshot = new Krypton.Toolkit.KryptonButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kwlblEmailAddress = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbEmailAddress = new Krypton.Toolkit.KryptonTextBox();
            this.kwlblBugDescription = new Krypton.Toolkit.KryptonWrapLabel();
            this.krtbBugDescription = new Krypton.Toolkit.KryptonRichTextBox();
            this.kwlblStepsToReproduce = new Krypton.Toolkit.KryptonWrapLabel();
            this.krtbStepsToReproduce = new Krypton.Toolkit.KryptonRichTextBox();
            this.kwlblAttachments = new Krypton.Toolkit.KryptonWrapLabel();
            this.kryptonPanel3 = new Krypton.Toolkit.KryptonPanel();
            this.flpAttachments = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).BeginInit();
            this.kryptonPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.tableLayoutPanel2);
            this.kryptonPanel1.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 526);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(1000, 50);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.kbtnAddScreenshot, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.kbtnAddFile, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.kbtnRemove, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.kbtnSend, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.kbtnCancel, 4, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1000, 49);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // kbtnAddFile
            // 
            this.kbtnAddFile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kbtnAddFile.AutoSize = true;
            this.kbtnAddFile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.kbtnAddFile.Location = new System.Drawing.Point(125, 13);
            this.kbtnAddFile.Margin = new System.Windows.Forms.Padding(10);
            this.kbtnAddFile.Name = "kbtnAddFile";
            this.kbtnAddFile.Size = new System.Drawing.Size(53, 22);
            this.kbtnAddFile.TabIndex = 2;
            this.kbtnAddFile.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnAddFile.Values.Text = "Add File";
            this.kbtnAddFile.Click += new System.EventHandler(this.kbtnAddFile_Click);
            // 
            // kbtnSend
            // 
            this.kbtnSend.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.kbtnSend.AutoSize = true;
            this.kbtnSend.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.kbtnSend.Location = new System.Drawing.Point(849, 13);
            this.kbtnSend.Margin = new System.Windows.Forms.Padding(10);
            this.kbtnSend.Name = "kbtnSend";
            this.kbtnSend.Size = new System.Drawing.Size(76, 22);
            this.kbtnSend.TabIndex = 0;
            this.kbtnSend.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnSend.Values.Text = "Send Report";
            this.kbtnSend.Click += new System.EventHandler(this.kbtnSend_Click);
            // 
            // kbtnCancel
            // 
            this.kbtnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.kbtnCancel.AutoSize = true;
            this.kbtnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.kbtnCancel.Location = new System.Drawing.Point(945, 13);
            this.kbtnCancel.Margin = new System.Windows.Forms.Padding(10);
            this.kbtnCancel.Name = "kbtnCancel";
            this.kbtnCancel.Size = new System.Drawing.Size(45, 22);
            this.kbtnCancel.TabIndex = 1;
            this.kbtnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnCancel.Values.Text = "Cancel";
            this.kbtnCancel.Click += new System.EventHandler(this.kbtnCancel_Click);
            // 
            // kbtnRemove
            // 
            this.kbtnRemove.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kbtnRemove.AutoSize = true;
            this.kbtnRemove.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.kbtnRemove.Enabled = false;
            this.kbtnRemove.Location = new System.Drawing.Point(198, 13);
            this.kbtnRemove.Margin = new System.Windows.Forms.Padding(10);
            this.kbtnRemove.Name = "kbtnRemove";
            this.kbtnRemove.Size = new System.Drawing.Size(53, 22);
            this.kbtnRemove.TabIndex = 3;
            this.kbtnRemove.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnRemove.Values.Text = "Remove";
            this.kbtnRemove.Click += new System.EventHandler(this.kbtnRemove_Click);
            // 
            // kbtnAddScreenshot
            // 
            this.kbtnAddScreenshot.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kbtnAddScreenshot.AutoSize = true;
            this.kbtnAddScreenshot.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.kbtnAddScreenshot.Location = new System.Drawing.Point(10, 13);
            this.kbtnAddScreenshot.Margin = new System.Windows.Forms.Padding(10);
            this.kbtnAddScreenshot.Name = "kbtnAddScreenshot";
            this.kbtnAddScreenshot.Size = new System.Drawing.Size(95, 22);
            this.kbtnAddScreenshot.TabIndex = 1;
            this.kbtnAddScreenshot.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnAddScreenshot.Values.Text = "Add Screenshot";
            this.kbtnAddScreenshot.Click += new System.EventHandler(this.kbtnAddScreenshot_Click);
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(1000, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(1000, 526);
            this.kryptonPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.kwlblEmailAddress, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ktbEmailAddress, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.kwlblBugDescription, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.krtbBugDescription, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.kwlblStepsToReproduce, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.krtbStepsToReproduce, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.kwlblAttachments, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.kryptonPanel3, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1000, 526);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // kwlblEmailAddress
            // 
            this.kwlblEmailAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblEmailAddress.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kwlblEmailAddress.Location = new System.Drawing.Point(13, 10);
            this.kwlblEmailAddress.Name = "kwlblEmailAddress";
            this.kwlblEmailAddress.Padding = new System.Windows.Forms.Padding(5);
            this.kwlblEmailAddress.Size = new System.Drawing.Size(144, 35);
            this.kwlblEmailAddress.Text = "Email Address:";
            this.kwlblEmailAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ktbEmailAddress
            // 
            this.ktbEmailAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktbEmailAddress.Location = new System.Drawing.Point(163, 13);
            this.ktbEmailAddress.Name = "ktbEmailAddress";
            this.ktbEmailAddress.Size = new System.Drawing.Size(824, 23);
            this.ktbEmailAddress.TabIndex = 1;
            this.ktbEmailAddress.TextChanged += new System.EventHandler(this.ktbEmailAddress_TextChanged);
            // 
            // kwlblBugDescription
            // 
            this.kwlblBugDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblBugDescription.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kwlblBugDescription.Location = new System.Drawing.Point(13, 45);
            this.kwlblBugDescription.Name = "kwlblBugDescription";
            this.kwlblBugDescription.Padding = new System.Windows.Forms.Padding(5);
            this.kwlblBugDescription.Size = new System.Drawing.Size(144, 188);
            this.kwlblBugDescription.Text = "Bug Description:";
            // 
            // krtbBugDescription
            // 
            this.krtbBugDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.krtbBugDescription.Location = new System.Drawing.Point(163, 48);
            this.krtbBugDescription.Name = "krtbBugDescription";
            this.krtbBugDescription.Size = new System.Drawing.Size(824, 182);
            this.krtbBugDescription.TabIndex = 2;
            this.krtbBugDescription.Text = "";
            this.krtbBugDescription.TextChanged += new System.EventHandler(this.krtbBugDescription_TextChanged);
            // 
            // kwlblStepsToReproduce
            // 
            this.kwlblStepsToReproduce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblStepsToReproduce.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kwlblStepsToReproduce.Location = new System.Drawing.Point(13, 233);
            this.kwlblStepsToReproduce.Name = "kwlblStepsToReproduce";
            this.kwlblStepsToReproduce.Padding = new System.Windows.Forms.Padding(5);
            this.kwlblStepsToReproduce.Size = new System.Drawing.Size(144, 188);
            this.kwlblStepsToReproduce.Text = "Steps to Reproduce:";
            // 
            // krtbStepsToReproduce
            // 
            this.krtbStepsToReproduce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.krtbStepsToReproduce.Location = new System.Drawing.Point(163, 236);
            this.krtbStepsToReproduce.Name = "krtbStepsToReproduce";
            this.krtbStepsToReproduce.Size = new System.Drawing.Size(824, 182);
            this.krtbStepsToReproduce.TabIndex = 3;
            this.krtbStepsToReproduce.Text = "";
            // 
            // kwlblAttachments
            // 
            this.kwlblAttachments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblAttachments.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kwlblAttachments.Location = new System.Drawing.Point(13, 421);
            this.kwlblAttachments.Name = "kwlblAttachments";
            this.kwlblAttachments.Padding = new System.Windows.Forms.Padding(5);
            this.kwlblAttachments.Size = new System.Drawing.Size(144, 95);
            this.kwlblAttachments.Text = "Attachments:";
            // 
            // kryptonPanel3
            // 
            this.kryptonPanel3.Controls.Add(this.flpAttachments);
            this.kryptonPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel3.Location = new System.Drawing.Point(163, 424);
            this.kryptonPanel3.Name = "kryptonPanel3";
            this.kryptonPanel3.Size = new System.Drawing.Size(824, 89);
            this.kryptonPanel3.TabIndex = 4;
            // 
            // flpAttachments
            // 
            this.flpAttachments.AutoScroll = true;
            this.flpAttachments.BackColor = System.Drawing.Color.Transparent;
            this.flpAttachments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpAttachments.Location = new System.Drawing.Point(0, 0);
            this.flpAttachments.Name = "flpAttachments";
            this.flpAttachments.Size = new System.Drawing.Size(824, 89);
            this.flpAttachments.TabIndex = 0;
            this.flpAttachments.WrapContents = false;
            // 
            // VisualBugReportingDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 576);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualBugReportingDialogForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Bug";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).EndInit();
            this.kryptonPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonButton kbtnCancel;
        private KryptonButton kbtnSend;
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonPanel kryptonPanel2;
        private TableLayoutPanel tableLayoutPanel1;
        private KryptonWrapLabel kwlblEmailAddress;
        private KryptonTextBox ktbEmailAddress;
        private KryptonWrapLabel kwlblBugDescription;
        private KryptonRichTextBox krtbBugDescription;
        private KryptonWrapLabel kwlblStepsToReproduce;
        private KryptonRichTextBox krtbStepsToReproduce;
        private KryptonWrapLabel kwlblAttachments;
        private KryptonPanel kryptonPanel3;
        private FlowLayoutPanel flpAttachments;
        private KryptonButton kbtnRemove;
        private KryptonButton kbtnAddFile;
        private KryptonButton kbtnAddScreenshot;
        private TableLayoutPanel tableLayoutPanel2;
    }
}

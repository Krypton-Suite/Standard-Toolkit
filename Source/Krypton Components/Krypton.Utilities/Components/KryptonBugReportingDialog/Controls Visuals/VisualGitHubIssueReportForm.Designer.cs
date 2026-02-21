namespace Krypton.Utilities
{
    partial class VisualGitHubIssueReportForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlMain = new Krypton.Toolkit.KryptonPanel();
            this.pnlScroll = new System.Windows.Forms.Panel();
            this.tlpContent = new System.Windows.Forms.TableLayoutPanel();
            this.kwlblSummary = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbSummary = new Krypton.Toolkit.KryptonTextBox();
            this.kwlblDescription = new Krypton.Toolkit.KryptonWrapLabel();
            this.krtbDescription = new Krypton.Toolkit.KryptonRichTextBox();
            this.pnlButtons = new Krypton.Toolkit.KryptonPanel();
            this.kbtnCreate = new Krypton.Toolkit.KryptonButton();
            this.kbtnCancel = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlScroll.SuspendLayout();
            this.tlpContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButtons)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlScroll);
            this.pnlMain.Controls.Add(this.pnlButtons);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(484, 261);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlScroll
            // 
            this.pnlScroll.AutoScroll = true;
            this.pnlScroll.BackColor = System.Drawing.Color.Transparent;
            this.pnlScroll.Controls.Add(this.tlpContent);
            this.pnlScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScroll.Location = new System.Drawing.Point(0, 0);
            this.pnlScroll.Name = "pnlScroll";
            this.pnlScroll.Padding = new System.Windows.Forms.Padding(12);
            this.pnlScroll.Size = new System.Drawing.Size(484, 206);
            this.pnlScroll.TabIndex = 0;
            // 
            // tlpContent
            // 
            this.tlpContent.ColumnCount = 2;
            this.tlpContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContent.Controls.Add(this.kwlblSummary, 0, 0);
            this.tlpContent.Controls.Add(this.ktbSummary, 1, 0);
            this.tlpContent.Controls.Add(this.kwlblDescription, 0, 1);
            this.tlpContent.Controls.Add(this.krtbDescription, 1, 1);
            this.tlpContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpContent.Location = new System.Drawing.Point(12, 12);
            this.tlpContent.Name = "tlpContent";
            this.tlpContent.RowCount = 2;
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContent.Size = new System.Drawing.Size(460, 182);
            this.tlpContent.TabIndex = 0;
            // 
            // kwlblSummary
            // 
            this.kwlblSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblSummary.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kwlblSummary.Location = new System.Drawing.Point(0, 0);
            this.kwlblSummary.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.kwlblSummary.Name = "kwlblSummary";
            this.kwlblSummary.Size = new System.Drawing.Size(92, 28);
            this.kwlblSummary.Text = "Title:";
            this.kwlblSummary.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ktbSummary
            // 
            this.ktbSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ktbSummary.Location = new System.Drawing.Point(100, 2);
            this.ktbSummary.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.ktbSummary.Name = "ktbSummary";
            this.ktbSummary.Size = new System.Drawing.Size(360, 23);
            this.ktbSummary.TabIndex = 0;
            // 
            // kwlblDescription
            // 
            this.kwlblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblDescription.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kwlblDescription.Location = new System.Drawing.Point(0, 30);
            this.kwlblDescription.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this.kwlblDescription.Name = "kwlblDescription";
            this.kwlblDescription.Size = new System.Drawing.Size(92, 152);
            this.kwlblDescription.Text = "Description:";
            this.kwlblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // krtbDescription
            // 
            this.krtbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.krtbDescription.Location = new System.Drawing.Point(100, 30);
            this.krtbDescription.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.krtbDescription.Name = "krtbDescription";
            this.krtbDescription.Size = new System.Drawing.Size(360, 150);
            this.krtbDescription.TabIndex = 1;
            this.krtbDescription.Text = "";
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.kbtnCreate);
            this.pnlButtons.Controls.Add(this.kbtnCancel);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 206);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.pnlButtons.Size = new System.Drawing.Size(484, 55);
            this.pnlButtons.TabIndex = 1;
            // 
            // kbtnCreate
            // 
            this.kbtnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnCreate.Location = new System.Drawing.Point(233, 15);
            this.kbtnCreate.Name = "kbtnCreate";
            this.kbtnCreate.Size = new System.Drawing.Size(120, 28);
            this.kbtnCreate.TabIndex = 0;
            this.kbtnCreate.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnCreate.Values.Text = "Create on GitHub";
            this.kbtnCreate.Click += new System.EventHandler(this.kbtnCreate_Click);
            // 
            // kbtnCancel
            // 
            this.kbtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnCancel.Location = new System.Drawing.Point(359, 15);
            this.kbtnCancel.Name = "kbtnCancel";
            this.kbtnCancel.Size = new System.Drawing.Size(113, 28);
            this.kbtnCancel.TabIndex = 1;
            this.kbtnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnCancel.Values.Text = "Cancel";
            this.kbtnCancel.Click += new System.EventHandler(this.kbtnCancel_Click);
            // 
            // VisualGitHubIssueReportForm
            // 
            this.AcceptButton = this.kbtnCreate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.kbtnCancel;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 250);
            this.Name = "VisualGitHubIssueReportForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Issue on GitHub";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlScroll.ResumeLayout(false);
            this.tlpContent.ResumeLayout(false);
            this.tlpContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButtons)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel pnlMain;
        private System.Windows.Forms.Panel pnlScroll;
        private System.Windows.Forms.TableLayoutPanel tlpContent;
        private Krypton.Toolkit.KryptonWrapLabel kwlblSummary;
        private Krypton.Toolkit.KryptonTextBox ktbSummary;
        private Krypton.Toolkit.KryptonWrapLabel kwlblDescription;
        private Krypton.Toolkit.KryptonRichTextBox krtbDescription;
        private Krypton.Toolkit.KryptonPanel pnlButtons;
        private Krypton.Toolkit.KryptonButton kbtnCreate;
        private Krypton.Toolkit.KryptonButton kbtnCancel;
    }
}
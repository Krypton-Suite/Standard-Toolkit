namespace Krypton.Toolkit
{
    partial class VisualToastNotificationBasicRtlAwareForm
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
            this.kpnlButtons = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.kbtnDismiss = new Krypton.Toolkit.KryptonButton();
            this.klblToastLocation = new Krypton.Toolkit.KryptonLabel();
            this.kchkDoNotShowAgain = new Krypton.Toolkit.KryptonCheckBox();
            this.itbDismiss = new Krypton.Toolkit.InternalToastButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pbxImage = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.kwlToastHeader = new Krypton.Toolkit.KryptonWrapLabel();
            this.kwlToastContent = new Krypton.Toolkit.KryptonWrapLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).BeginInit();
            this.kpnlButtons.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImage)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // kpnlButtons
            // 
            this.kpnlButtons.Controls.Add(this.tableLayoutPanel3);
            this.kpnlButtons.Controls.Add(this.kryptonBorderEdge1);
            this.kpnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kpnlButtons.Location = new System.Drawing.Point(0, 219);
            this.kpnlButtons.Name = "kpnlButtons";
            this.kpnlButtons.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kpnlButtons.Size = new System.Drawing.Size(577, 50);
            this.kpnlButtons.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.kbtnDismiss, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.klblToastLocation, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.kchkDoNotShowAgain, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.itbDismiss, 3, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 1);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(577, 49);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // kbtnDismiss
            // 
            this.kbtnDismiss.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.kbtnDismiss.AutoSize = true;
            this.kbtnDismiss.Location = new System.Drawing.Point(78, 13);
            this.kbtnDismiss.Margin = new System.Windows.Forms.Padding(10);
            this.kbtnDismiss.Name = "kbtnDismiss";
            this.kbtnDismiss.Size = new System.Drawing.Size(48, 22);
            this.kbtnDismiss.TabIndex = 0;
            this.kbtnDismiss.Values.Text = "{0} ({1})";
            this.kbtnDismiss.Visible = false;
            this.kbtnDismiss.Click += new System.EventHandler(this.kbtnDismiss_Click);
            // 
            // klblToastLocation
            // 
            this.klblToastLocation.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.klblToastLocation.Location = new System.Drawing.Point(496, 23);
            this.klblToastLocation.Margin = new System.Windows.Forms.Padding(10);
            this.klblToastLocation.Name = "klblToastLocation";
            this.klblToastLocation.Size = new System.Drawing.Size(6, 2);
            this.klblToastLocation.TabIndex = 1;
            this.klblToastLocation.Values.Text = "";
            // 
            // kchkDoNotShowAgain
            // 
            this.kchkDoNotShowAgain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kchkDoNotShowAgain.Location = new System.Drawing.Point(522, 10);
            this.kchkDoNotShowAgain.Margin = new System.Windows.Forms.Padding(10);
            this.kchkDoNotShowAgain.Name = "kchkDoNotShowAgain";
            this.kchkDoNotShowAgain.Size = new System.Drawing.Size(45, 29);
            this.kchkDoNotShowAgain.TabIndex = 5;
            this.kchkDoNotShowAgain.Values.Text = "CB1";
            this.kchkDoNotShowAgain.Visible = false;
            // 
            // itbDismiss
            // 
            this.itbDismiss.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.itbDismiss.AutoSize = true;
            this.itbDismiss.BaseToastForm = this;
            this.itbDismiss.IsActionButton = false;
            this.itbDismiss.IsDismissButton = false;
            this.itbDismiss.Location = new System.Drawing.Point(10, 13);
            this.itbDismiss.Margin = new System.Windows.Forms.Padding(10);
            this.itbDismiss.Name = "itbDismiss";
            this.itbDismiss.ProcessPath = null;
            this.itbDismiss.Size = new System.Drawing.Size(48, 22);
            this.itbDismiss.TabIndex = 6;
            this.itbDismiss.Values.Text = "{0} ({1})";
            this.itbDismiss.Click += new System.EventHandler(this.itbDismiss_Click);
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(577, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(577, 219);
            this.kryptonPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pbxImage, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(577, 219);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pbxImage
            // 
            this.pbxImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxImage.Location = new System.Drawing.Point(444, 5);
            this.pbxImage.Margin = new System.Windows.Forms.Padding(5);
            this.pbxImage.Name = "pbxImage";
            this.pbxImage.Size = new System.Drawing.Size(128, 209);
            this.pbxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxImage.TabIndex = 0;
            this.pbxImage.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.kwlToastHeader, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.kwlToastContent, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(433, 213);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // kwlToastHeader
            // 
            this.kwlToastHeader.AutoSize = false;
            this.kwlToastHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlToastHeader.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.kwlToastHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kwlToastHeader.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.kwlToastHeader.Location = new System.Drawing.Point(3, 0);
            this.kwlToastHeader.Name = "kwlToastHeader";
            this.kwlToastHeader.Size = new System.Drawing.Size(427, 50);
            this.kwlToastHeader.Text = "kryptonWrapLabel1";
            this.kwlToastHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // kwlToastContent
            // 
            this.kwlToastContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlToastContent.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kwlToastContent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kwlToastContent.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kwlToastContent.Location = new System.Drawing.Point(3, 50);
            this.kwlToastContent.Name = "kwlToastContent";
            this.kwlToastContent.Size = new System.Drawing.Size(427, 163);
            this.kwlToastContent.Text = "kryptonWrapLabel2";
            this.kwlToastContent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // VisualToastNotificationBasicRtlAwareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 269);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.kpnlButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualToastNotificationBasicRtlAwareForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "";
            this.Load += new System.EventHandler(this.VisualToastNotificationBasicRtlAwareForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).EndInit();
            this.kpnlButtons.ResumeLayout(false);
            this.kpnlButtons.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxImage)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kpnlButtons;
        private KryptonPanel kryptonPanel1;
        private KryptonBorderEdge kryptonBorderEdge1;
        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pbxImage;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private KryptonButton kbtnDismiss;
        private KryptonLabel klblToastLocation;
        private KryptonWrapLabel kwlToastHeader;
        private KryptonWrapLabel kwlToastContent;
        private KryptonCheckBox kchkDoNotShowAgain;
        private InternalToastButton itbDismiss;
    }
}
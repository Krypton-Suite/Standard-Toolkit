namespace TestForm
{
    partial class KryptonSystemInformationDemo
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
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kwlblInfo = new Krypton.Toolkit.KryptonWrapLabel();
            this.kchkModal = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkRtl = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkWindowsMsinfo = new Krypton.Toolkit.KryptonCheckBox();
            this.kbtnShow = new Krypton.Toolkit.KryptonButton();
            this.kbtnClose = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kwlblInfo);
            this.kryptonPanel1.Controls.Add(this.kchkModal);
            this.kryptonPanel1.Controls.Add(this.kchkRtl);
            this.kryptonPanel1.Controls.Add(this.kchkWindowsMsinfo);
            this.kryptonPanel1.Controls.Add(this.kbtnShow);
            this.kryptonPanel1.Controls.Add(this.kbtnClose);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Padding = new System.Windows.Forms.Padding(20);
            this.kryptonPanel1.Size = new System.Drawing.Size(640, 320);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kwlblInfo
            // 
            this.kwlblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kwlblInfo.AutoSize = false;
            this.kwlblInfo.Location = new System.Drawing.Point(23, 23);
            this.kwlblInfo.Name = "kwlblInfo";
            this.kwlblInfo.Size = new System.Drawing.Size(594, 88);
            this.kwlblInfo.Text = "Issue #3176: Krypton System Information is an msinfo32-style viewer. Open the dialog, select categories (WMI is queried lazily), use Find/Find next (F3), Copy, Print, Save, Refresh, optional All processes on Loaded Modules, and switch themes. The About Box System Information button opens this UI instead of MSInfo32.exe.";
            // 
            // kchkModal
            // 
            this.kchkModal.Location = new System.Drawing.Point(23, 120);
            this.kchkModal.Name = "kchkModal";
            this.kchkModal.Size = new System.Drawing.Size(400, 20);
            this.kchkModal.TabIndex = 1;
            this.kchkModal.Values.Text = "Show as modal dialog";
            // 
            // kchkRtl
            // 
            this.kchkRtl.Location = new System.Drawing.Point(23, 146);
            this.kchkRtl.Name = "kchkRtl";
            this.kchkRtl.Size = new System.Drawing.Size(400, 20);
            this.kchkRtl.TabIndex = 2;
            this.kchkRtl.Values.Text = "Right-to-left layout";
            // 
            // kchkWindowsMsinfo
            // 
            this.kchkWindowsMsinfo.Checked = true;
            this.kchkWindowsMsinfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkWindowsMsinfo.Location = new System.Drawing.Point(23, 172);
            this.kchkWindowsMsinfo.Name = "kchkWindowsMsinfo";
            this.kchkWindowsMsinfo.Size = new System.Drawing.Size(400, 20);
            this.kchkWindowsMsinfo.TabIndex = 3;
            this.kchkWindowsMsinfo.Values.Text = "Show Windows System Information button";
            // 
            // kbtnShow
            // 
            this.kbtnShow.Location = new System.Drawing.Point(23, 214);
            this.kbtnShow.Name = "kbtnShow";
            this.kbtnShow.Size = new System.Drawing.Size(180, 25);
            this.kbtnShow.TabIndex = 4;
            this.kbtnShow.Values.Text = "Show System Information";
            this.kbtnShow.Click += new System.EventHandler(this.kbtnShow_Click);
            // 
            // kbtnClose
            // 
            this.kbtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnClose.Location = new System.Drawing.Point(525, 273);
            this.kbtnClose.Name = "kbtnClose";
            this.kbtnClose.Size = new System.Drawing.Size(92, 25);
            this.kbtnClose.TabIndex = 5;
            this.kbtnClose.Values.Text = "Close";
            this.kbtnClose.Click += new System.EventHandler(this.kbtnClose_Click);
            // 
            // KryptonSystemInformationDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 320);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "KryptonSystemInformationDemo";
            this.Text = "Krypton System Information (#3176)";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonWrapLabel kwlblInfo;
        private Krypton.Toolkit.KryptonCheckBox kchkModal;
        private Krypton.Toolkit.KryptonCheckBox kchkRtl;
        private Krypton.Toolkit.KryptonCheckBox kchkWindowsMsinfo;
        private Krypton.Toolkit.KryptonButton kbtnShow;
        private Krypton.Toolkit.KryptonButton kbtnClose;
    }
}

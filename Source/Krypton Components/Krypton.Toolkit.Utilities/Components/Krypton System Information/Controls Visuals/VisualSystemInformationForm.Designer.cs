namespace Krypton.Toolkit.Utilities
{
    partial class VisualSystemInformationForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CancelPendingCollect();
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.kpnlBottom = new Krypton.Toolkit.KryptonPanel();
            this.kbeBottom = new Krypton.Toolkit.KryptonBorderEdge();
            this.kbtnClose = new Krypton.Toolkit.KryptonButton();
            this.kbtnWindowsMsinfo = new Krypton.Toolkit.KryptonButton();
            this.kpnlToolbar = new Krypton.Toolkit.KryptonPanel();
            this.ksbFind = new Krypton.Toolkit.Utilities.KryptonSearchBox();
            this.kbtnCopy = new Krypton.Toolkit.KryptonButton();
            this.kbtnSave = new Krypton.Toolkit.KryptonButton();
            this.kbtnRefresh = new Krypton.Toolkit.KryptonButton();
            this.kbtnPrint = new Krypton.Toolkit.KryptonButton();
            this.kbtnFindNext = new Krypton.Toolkit.KryptonButton();
            this.kchkAllModules = new Krypton.Toolkit.KryptonCheckBox();
            this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
            this.kscMain = new Krypton.Toolkit.KryptonSplitContainer();
            this.ktvCategories = new Krypton.Toolkit.KryptonTreeView();
            this.kdgvDetails = new Krypton.Toolkit.KryptonDataGridView();
            this.kssStatus = new Krypton.Toolkit.KryptonStatusStrip();
            this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlBottom)).BeginInit();
            this.kpnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlToolbar)).BeginInit();
            this.kpnlToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
            this.kpnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kscMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kscMain.Panel1)).BeginInit();
            this.kscMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kscMain.Panel2)).BeginInit();
            this.kscMain.Panel2.SuspendLayout();
            this.kscMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kdgvDetails)).BeginInit();
            this.kssStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // kpnlBottom
            // 
            this.kpnlBottom.Controls.Add(this.kbtnWindowsMsinfo);
            this.kpnlBottom.Controls.Add(this.kbtnClose);
            this.kpnlBottom.Controls.Add(this.kbeBottom);
            this.kpnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kpnlBottom.Location = new System.Drawing.Point(0, 611);
            this.kpnlBottom.Name = "kpnlBottom";
            this.kpnlBottom.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kpnlBottom.Size = new System.Drawing.Size(1000, 50);
            this.kpnlBottom.TabIndex = 2;
            // 
            // kbeBottom
            // 
            this.kbeBottom.Dock = System.Windows.Forms.DockStyle.Top;
            this.kbeBottom.Location = new System.Drawing.Point(0, 0);
            this.kbeBottom.Name = "kbeBottom";
            this.kbeBottom.Size = new System.Drawing.Size(1000, 1);
            this.kbeBottom.Text = "";
            // 
            // kbtnClose
            // 
            this.kbtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.kbtnClose.Location = new System.Drawing.Point(896, 12);
            this.kbtnClose.Name = "kbtnClose";
            this.kbtnClose.Size = new System.Drawing.Size(92, 25);
            this.kbtnClose.TabIndex = 1;
            this.kbtnClose.Values.Text = "Close";
            this.kbtnClose.Click += new System.EventHandler(this.kbtnClose_Click);
            // 
            // kbtnWindowsMsinfo
            // 
            this.kbtnWindowsMsinfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.kbtnWindowsMsinfo.AutoSize = true;
            this.kbtnWindowsMsinfo.Location = new System.Drawing.Point(12, 12);
            this.kbtnWindowsMsinfo.Name = "kbtnWindowsMsinfo";
            this.kbtnWindowsMsinfo.Size = new System.Drawing.Size(200, 25);
            this.kbtnWindowsMsinfo.TabIndex = 0;
            this.kbtnWindowsMsinfo.Values.Text = "Windows System Information...";
            this.kbtnWindowsMsinfo.Click += new System.EventHandler(this.kbtnWindowsMsinfo_Click);
            // 
            // kpnlToolbar
            // 
            this.kpnlToolbar.Controls.Add(this.kchkAllModules);
            this.kpnlToolbar.Controls.Add(this.kbtnRefresh);
            this.kpnlToolbar.Controls.Add(this.kbtnPrint);
            this.kpnlToolbar.Controls.Add(this.kbtnSave);
            this.kpnlToolbar.Controls.Add(this.kbtnCopy);
            this.kpnlToolbar.Controls.Add(this.kbtnFindNext);
            this.kpnlToolbar.Controls.Add(this.ksbFind);
            this.kpnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.kpnlToolbar.Location = new System.Drawing.Point(0, 0);
            this.kpnlToolbar.Name = "kpnlToolbar";
            this.kpnlToolbar.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kpnlToolbar.Size = new System.Drawing.Size(1000, 40);
            this.kpnlToolbar.TabIndex = 0;
            // 
            // ksbFind
            // 
            this.ksbFind.Location = new System.Drawing.Point(12, 8);
            this.ksbFind.Name = "ksbFind";
            this.ksbFind.Size = new System.Drawing.Size(220, 23);
            this.ksbFind.TabIndex = 0;
            this.ksbFind.Search += new System.EventHandler<Krypton.Toolkit.Utilities.SearchEventArgs>(this.ksbFind_Search);
            this.ksbFind.SearchCleared += new System.EventHandler(this.ksbFind_SearchCleared);
            this.ksbFind.TextChanged += new System.EventHandler(this.ksbFind_TextChanged);
            // 
            // kbtnFindNext
            // 
            this.kbtnFindNext.Location = new System.Drawing.Point(238, 7);
            this.kbtnFindNext.Name = "kbtnFindNext";
            this.kbtnFindNext.Size = new System.Drawing.Size(90, 25);
            this.kbtnFindNext.TabIndex = 2;
            this.kbtnFindNext.Values.Text = "Find next";
            this.kbtnFindNext.Click += new System.EventHandler(this.kbtnFindNext_Click);
            // 
            // kbtnCopy
            // 
            this.kbtnCopy.Location = new System.Drawing.Point(318, 7);
            this.kbtnCopy.Name = "kbtnCopy";
            this.kbtnCopy.Size = new System.Drawing.Size(90, 25);
            this.kbtnCopy.TabIndex = 3;
            this.kbtnCopy.Values.Text = "Copy";
            this.kbtnCopy.Click += new System.EventHandler(this.kbtnCopy_Click);
            // 
            // kbtnSave
            // 
            this.kbtnSave.Location = new System.Drawing.Point(414, 7);
            this.kbtnSave.Name = "kbtnSave";
            this.kbtnSave.Size = new System.Drawing.Size(90, 25);
            this.kbtnSave.TabIndex = 4;
            this.kbtnSave.Values.Text = "Save";
            this.kbtnSave.Click += new System.EventHandler(this.kbtnSave_Click);
            // 
            // kbtnPrint
            // 
            this.kbtnPrint.Location = new System.Drawing.Point(510, 7);
            this.kbtnPrint.Name = "kbtnPrint";
            this.kbtnPrint.Size = new System.Drawing.Size(90, 25);
            this.kbtnPrint.TabIndex = 5;
            this.kbtnPrint.Values.Text = "Print";
            this.kbtnPrint.Click += new System.EventHandler(this.kbtnPrint_Click);
            // 
            // kbtnRefresh
            // 
            this.kbtnRefresh.Location = new System.Drawing.Point(606, 7);
            this.kbtnRefresh.Name = "kbtnRefresh";
            this.kbtnRefresh.Size = new System.Drawing.Size(90, 25);
            this.kbtnRefresh.TabIndex = 6;
            this.kbtnRefresh.Values.Text = "Refresh";
            this.kbtnRefresh.Click += new System.EventHandler(this.kbtnRefresh_Click);
            // 
            // kchkAllModules
            // 
            this.kchkAllModules.Location = new System.Drawing.Point(702, 10);
            this.kchkAllModules.Name = "kchkAllModules";
            this.kchkAllModules.Size = new System.Drawing.Size(200, 20);
            this.kchkAllModules.TabIndex = 7;
            this.kchkAllModules.Values.Text = "All processes";
            this.kchkAllModules.Visible = false;
            this.kchkAllModules.CheckedChanged += new System.EventHandler(this.kchkAllModules_CheckedChanged);
            // 
            // kpnlMain
            // 
            this.kpnlMain.Controls.Add(this.kscMain);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 40);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.Size = new System.Drawing.Size(1000, 571);
            this.kpnlMain.TabIndex = 1;
            // 
            // kscMain
            // 
            this.kscMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.kscMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kscMain.Location = new System.Drawing.Point(0, 0);
            this.kscMain.Name = "kscMain";
            this.kscMain.SeparatorStyle = Krypton.Toolkit.SeparatorStyle.HighProfile;
            this.kscMain.Size = new System.Drawing.Size(1000, 571);
            this.kscMain.SplitterDistance = 280;
            this.kscMain.TabIndex = 0;
            // 
            // kscMain.Panel1
            // 
            this.kscMain.Panel1.Controls.Add(this.ktvCategories);
            // 
            // kscMain.Panel2
            // 
            this.kscMain.Panel2.Controls.Add(this.kdgvDetails);
            // 
            // ktvCategories
            // 
            this.ktvCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktvCategories.HideSelection = false;
            this.ktvCategories.Location = new System.Drawing.Point(0, 0);
            this.ktvCategories.Name = "ktvCategories";
            this.ktvCategories.Size = new System.Drawing.Size(280, 571);
            this.ktvCategories.TabIndex = 0;
            this.ktvCategories.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ktvCategories_AfterSelect);
            // 
            // kdgvDetails
            // 
            this.kdgvDetails.AllowUserToAddRows = false;
            this.kdgvDetails.AllowUserToDeleteRows = false;
            this.kdgvDetails.AllowUserToOrderColumns = true;
            this.kdgvDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.kdgvDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kdgvDetails.Location = new System.Drawing.Point(0, 0);
            this.kdgvDetails.Name = "kdgvDetails";
            this.kdgvDetails.ReadOnly = true;
            this.kdgvDetails.RowHeadersVisible = false;
            this.kdgvDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.kdgvDetails.Size = new System.Drawing.Size(715, 571);
            this.kdgvDetails.TabIndex = 0;
            this.kdgvDetails.VirtualMode = true;
            this.kdgvDetails.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.kdgvDetails_CellValueNeeded);
            // 
            // kssStatus
            // 
            this.kssStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslStatus});
            this.kssStatus.Location = new System.Drawing.Point(0, 661);
            this.kssStatus.Name = "kssStatus";
            this.kssStatus.Size = new System.Drawing.Size(1000, 22);
            this.kssStatus.TabIndex = 3;
            // 
            // tslStatus
            // 
            this.tslStatus.Name = "tslStatus";
            this.tslStatus.Size = new System.Drawing.Size(39, 17);
            this.tslStatus.Text = "Ready";
            // 
            // VisualSystemInformationForm
            // 
            this.AcceptButton = this.kbtnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.kbtnClose;
            this.ClientSize = new System.Drawing.Size(1000, 683);
            this.Controls.Add(this.kpnlMain);
            this.Controls.Add(this.kpnlToolbar);
            this.Controls.Add(this.kpnlBottom);
            this.Controls.Add(this.kssStatus);
            this.MinimumSize = new System.Drawing.Size(720, 480);
            this.Name = "VisualSystemInformationForm";
            this.ShowInTaskbar = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "System Information";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlBottom)).EndInit();
            this.kpnlBottom.ResumeLayout(false);
            this.kpnlBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlToolbar)).EndInit();
            this.kpnlToolbar.ResumeLayout(false);
            this.kpnlToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
            this.kpnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kscMain.Panel1)).EndInit();
            this.kscMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kscMain.Panel2)).EndInit();
            this.kscMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kscMain)).EndInit();
            this.kscMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kdgvDetails)).EndInit();
            this.kssStatus.ResumeLayout(false);
            this.kssStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kpnlBottom;
        private Krypton.Toolkit.KryptonBorderEdge kbeBottom;
        private Krypton.Toolkit.KryptonButton kbtnClose;
        private Krypton.Toolkit.KryptonButton kbtnWindowsMsinfo;
        private Krypton.Toolkit.KryptonPanel kpnlToolbar;
        private Krypton.Toolkit.Utilities.KryptonSearchBox ksbFind;
        private Krypton.Toolkit.KryptonButton kbtnCopy;
        private Krypton.Toolkit.KryptonButton kbtnSave;
        private Krypton.Toolkit.KryptonButton kbtnPrint;
        private Krypton.Toolkit.KryptonButton kbtnFindNext;
        private Krypton.Toolkit.KryptonCheckBox kchkAllModules;
        private Krypton.Toolkit.KryptonButton kbtnRefresh;
        private Krypton.Toolkit.KryptonPanel kpnlMain;
        private Krypton.Toolkit.KryptonSplitContainer kscMain;
        private Krypton.Toolkit.KryptonTreeView ktvCategories;
        private Krypton.Toolkit.KryptonDataGridView kdgvDetails;
        private Krypton.Toolkit.KryptonStatusStrip kssStatus;
        private System.Windows.Forms.ToolStripStatusLabel tslStatus;
    }
}

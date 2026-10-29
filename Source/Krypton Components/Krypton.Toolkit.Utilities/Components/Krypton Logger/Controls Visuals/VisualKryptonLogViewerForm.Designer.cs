#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities
{
    partial class VisualKryptonLogViewerForm
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
            this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
            this.tlpRoot = new System.Windows.Forms.TableLayoutPanel();
            this.tlpFilters = new System.Windows.Forms.TableLayoutPanel();
            this.kwlblLevel = new Krypton.Toolkit.KryptonWrapLabel();
            this.kcmbLevel = new Krypton.Toolkit.KryptonComboBox();
            this.kwlblCategory = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktxtCategory = new Krypton.Toolkit.KryptonTextBox();
            this.kwlblSearch = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktxtSearch = new Krypton.Toolkit.KryptonTextBox();
            this.kchkLiveTail = new Krypton.Toolkit.KryptonCheckBox();
            this.lvEvents = new System.Windows.Forms.ListView();
            this.colTime = new System.Windows.Forms.ColumnHeader();
            this.colLevel = new System.Windows.Forms.ColumnHeader();
            this.colCategory = new System.Windows.Forms.ColumnHeader();
            this.colMessage = new System.Windows.Forms.ColumnHeader();
            this.kpnlButtons = new Krypton.Toolkit.KryptonPanel();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.kwlblStatus = new Krypton.Toolkit.KryptonWrapLabel();
            this.kbtnExport = new Krypton.Toolkit.KryptonButton();
            this.kbtnClose = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
            this.kpnlMain.SuspendLayout();
            this.tlpRoot.SuspendLayout();
            this.tlpFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).BeginInit();
            this.kpnlButtons.SuspendLayout();
            this.tlpButtons.SuspendLayout();
            this.SuspendLayout();
            //
            // kpnlMain
            //
            this.kpnlMain.Controls.Add(this.tlpRoot);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 0);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.Padding = new System.Windows.Forms.Padding(8);
            this.kpnlMain.Size = new System.Drawing.Size(900, 520);
            this.kpnlMain.TabIndex = 0;
            //
            // tlpRoot
            //
            this.tlpRoot.BackColor = System.Drawing.Color.Transparent;
            this.tlpRoot.ColumnCount = 1;
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRoot.Controls.Add(this.tlpFilters, 0, 0);
            this.tlpRoot.Controls.Add(this.lvEvents, 0, 1);
            this.tlpRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRoot.Location = new System.Drawing.Point(8, 8);
            this.tlpRoot.Name = "tlpRoot";
            this.tlpRoot.RowCount = 2;
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRoot.Size = new System.Drawing.Size(884, 462);
            this.tlpRoot.TabIndex = 0;
            //
            // tlpFilters
            //
            this.tlpFilters.AutoSize = true;
            this.tlpFilters.BackColor = System.Drawing.Color.Transparent;
            this.tlpFilters.ColumnCount = 8;
            this.tlpFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFilters.Controls.Add(this.kwlblLevel, 0, 0);
            this.tlpFilters.Controls.Add(this.kcmbLevel, 1, 0);
            this.tlpFilters.Controls.Add(this.kwlblCategory, 2, 0);
            this.tlpFilters.Controls.Add(this.ktxtCategory, 3, 0);
            this.tlpFilters.Controls.Add(this.kwlblSearch, 4, 0);
            this.tlpFilters.Controls.Add(this.ktxtSearch, 5, 0);
            this.tlpFilters.Controls.Add(this.kchkLiveTail, 6, 0);
            this.tlpFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFilters.Location = new System.Drawing.Point(0, 0);
            this.tlpFilters.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.tlpFilters.Name = "tlpFilters";
            this.tlpFilters.RowCount = 1;
            this.tlpFilters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFilters.Size = new System.Drawing.Size(884, 32);
            this.tlpFilters.TabIndex = 0;
            //
            // kwlblLevel
            //
            this.kwlblLevel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kwlblLevel.AutoSize = true;
            this.kwlblLevel.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblLevel.Location = new System.Drawing.Point(3, 7);
            this.kwlblLevel.Name = "kwlblLevel";
            this.kwlblLevel.Text = "Level:";
            //
            // kcmbLevel
            //
            this.kcmbLevel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kcmbLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbLevel.Location = new System.Drawing.Point(50, 3);
            this.kcmbLevel.Name = "kcmbLevel";
            this.kcmbLevel.Size = new System.Drawing.Size(110, 21);
            this.kcmbLevel.TabIndex = 1;
            this.kcmbLevel.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            //
            // kwlblCategory
            //
            this.kwlblCategory.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kwlblCategory.AutoSize = true;
            this.kwlblCategory.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblCategory.Location = new System.Drawing.Point(166, 7);
            this.kwlblCategory.Name = "kwlblCategory";
            this.kwlblCategory.Text = "Category:";
            //
            // ktxtCategory
            //
            this.ktxtCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtCategory.Location = new System.Drawing.Point(230, 3);
            this.ktxtCategory.Name = "ktxtCategory";
            this.ktxtCategory.TabIndex = 3;
            this.ktxtCategory.TextChanged += new System.EventHandler(this.FilterChanged);
            //
            // kwlblSearch
            //
            this.kwlblSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kwlblSearch.AutoSize = true;
            this.kwlblSearch.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblSearch.Location = new System.Drawing.Point(400, 7);
            this.kwlblSearch.Name = "kwlblSearch";
            this.kwlblSearch.Text = "Search:";
            //
            // ktxtSearch
            //
            this.ktxtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtSearch.Location = new System.Drawing.Point(460, 3);
            this.ktxtSearch.Name = "ktxtSearch";
            this.ktxtSearch.TabIndex = 5;
            this.ktxtSearch.TextChanged += new System.EventHandler(this.FilterChanged);
            //
            // kchkLiveTail
            //
            this.kchkLiveTail.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kchkLiveTail.Checked = true;
            this.kchkLiveTail.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkLiveTail.Location = new System.Drawing.Point(720, 4);
            this.kchkLiveTail.Name = "kchkLiveTail";
            this.kchkLiveTail.TabIndex = 6;
            this.kchkLiveTail.Values.Text = "Live tail";
            //
            // lvEvents
            //
            this.lvEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.colTime,
                this.colLevel,
                this.colCategory,
                this.colMessage});
            this.lvEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvEvents.FullRowSelect = true;
            this.lvEvents.HideSelection = false;
            this.lvEvents.Location = new System.Drawing.Point(3, 43);
            this.lvEvents.Name = "lvEvents";
            this.lvEvents.Size = new System.Drawing.Size(878, 416);
            this.lvEvents.TabIndex = 1;
            this.lvEvents.UseCompatibleStateImageBehavior = false;
            this.lvEvents.View = System.Windows.Forms.View.Details;
            this.lvEvents.VirtualMode = true;
            this.lvEvents.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lvEvents_RetrieveVirtualItem);
            //
            // colTime
            //
            this.colTime.Text = "Time";
            this.colTime.Width = 90;
            //
            // colLevel
            //
            this.colLevel.Text = "Level";
            this.colLevel.Width = 90;
            //
            // colCategory
            //
            this.colCategory.Text = "Category";
            this.colCategory.Width = 160;
            //
            // colMessage
            //
            this.colMessage.Text = "Message";
            this.colMessage.Width = 500;
            //
            // kpnlButtons
            //
            this.kpnlButtons.Controls.Add(this.tlpButtons);
            this.kpnlButtons.Controls.Add(this.kryptonBorderEdge1);
            this.kpnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kpnlButtons.Location = new System.Drawing.Point(0, 470);
            this.kpnlButtons.Name = "kpnlButtons";
            this.kpnlButtons.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kpnlButtons.Size = new System.Drawing.Size(900, 50);
            this.kpnlButtons.TabIndex = 1;
            //
            // kryptonBorderEdge1
            //
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(900, 1);
            //
            // tlpButtons
            //
            this.tlpButtons.BackColor = System.Drawing.Color.Transparent;
            this.tlpButtons.ColumnCount = 3;
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpButtons.Controls.Add(this.kwlblStatus, 0, 0);
            this.tlpButtons.Controls.Add(this.kbtnExport, 1, 0);
            this.tlpButtons.Controls.Add(this.kbtnClose, 2, 0);
            this.tlpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButtons.Location = new System.Drawing.Point(0, 1);
            this.tlpButtons.Name = "tlpButtons";
            this.tlpButtons.RowCount = 1;
            this.tlpButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtons.Size = new System.Drawing.Size(900, 49);
            this.tlpButtons.TabIndex = 0;
            //
            // kwlblStatus
            //
            this.kwlblStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kwlblStatus.AutoSize = true;
            this.kwlblStatus.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblStatus.Location = new System.Drawing.Point(10, 16);
            this.kwlblStatus.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.kwlblStatus.Name = "kwlblStatus";
            this.kwlblStatus.Text = "";
            //
            // kbtnExport
            //
            this.kbtnExport.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.kbtnExport.AutoSize = true;
            this.kbtnExport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.kbtnExport.Location = new System.Drawing.Point(720, 12);
            this.kbtnExport.Margin = new System.Windows.Forms.Padding(10);
            this.kbtnExport.Name = "kbtnExport";
            this.kbtnExport.Size = new System.Drawing.Size(70, 25);
            this.kbtnExport.TabIndex = 1;
            this.kbtnExport.Values.Text = "Export";
            this.kbtnExport.Click += new System.EventHandler(this.kbtnExport_Click);
            //
            // kbtnClose
            //
            this.kbtnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.kbtnClose.AutoSize = true;
            this.kbtnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.kbtnClose.Location = new System.Drawing.Point(810, 12);
            this.kbtnClose.Margin = new System.Windows.Forms.Padding(10);
            this.kbtnClose.Name = "kbtnClose";
            this.kbtnClose.Size = new System.Drawing.Size(70, 25);
            this.kbtnClose.TabIndex = 2;
            this.kbtnClose.Values.Text = "Close";
            this.kbtnClose.Click += new System.EventHandler(this.kbtnClose_Click);
            //
            // VisualKryptonLogViewerForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 520);
            this.Controls.Add(this.kpnlMain);
            this.Controls.Add(this.kpnlButtons);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 360);
            this.Name = "VisualKryptonLogViewerForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Application Log";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
            this.kpnlMain.ResumeLayout(false);
            this.tlpRoot.ResumeLayout(false);
            this.tlpRoot.PerformLayout();
            this.tlpFilters.ResumeLayout(false);
            this.tlpFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).EndInit();
            this.kpnlButtons.ResumeLayout(false);
            this.kpnlButtons.PerformLayout();
            this.tlpButtons.ResumeLayout(false);
            this.tlpButtons.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private KryptonPanel kpnlMain;
        private TableLayoutPanel tlpRoot;
        private TableLayoutPanel tlpFilters;
        private KryptonWrapLabel kwlblLevel;
        private KryptonComboBox kcmbLevel;
        private KryptonWrapLabel kwlblCategory;
        private KryptonTextBox ktxtCategory;
        private KryptonWrapLabel kwlblSearch;
        private KryptonTextBox ktxtSearch;
        private KryptonCheckBox kchkLiveTail;
        private ListView lvEvents;
        private ColumnHeader colTime;
        private ColumnHeader colLevel;
        private ColumnHeader colCategory;
        private ColumnHeader colMessage;
        private KryptonPanel kpnlButtons;
        private KryptonBorderEdge kryptonBorderEdge1;
        private TableLayoutPanel tlpButtons;
        private KryptonWrapLabel kwlblStatus;
        private KryptonButton kbtnExport;
        private KryptonButton kbtnClose;
    }
}

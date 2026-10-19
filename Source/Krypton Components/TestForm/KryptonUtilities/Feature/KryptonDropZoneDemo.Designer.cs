#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class KryptonDropZoneDemo
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
            this.kwlHeader = new Krypton.Toolkit.KryptonWrapLabel();
            this.kscWorkspace = new Krypton.Toolkit.KryptonSplitContainer();
            this.kdzDropZone = new Krypton.Toolkit.Utilities.KryptonDropZone();
            this.kpnlSettings = new Krypton.Toolkit.KryptonPanel();
            this.kpnlLog = new Krypton.Toolkit.KryptonPanel();
            this.ktbxEventLog = new Krypton.Toolkit.KryptonTextBox();
            this.kpnlLogHeader = new Krypton.Toolkit.KryptonPanel();
            this.klblEventLogTitle = new Krypton.Toolkit.KryptonLabel();
            this.kbtnClearLog = new Krypton.Toolkit.KryptonButton();
            this.klblSummary = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kscWorkspace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kscWorkspace.Panel1)).BeginInit();
            this.kscWorkspace.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kscWorkspace.Panel2)).BeginInit();
            this.kscWorkspace.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlLog)).BeginInit();
            this.kpnlLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlLogHeader)).BeginInit();
            this.kpnlLogHeader.SuspendLayout();
            this.SuspendLayout();
            //
            // kwlHeader
            //
            this.kwlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.kwlHeader.Location = new System.Drawing.Point(0, 0);
            this.kwlHeader.Name = "kwlHeader";
            this.kwlHeader.Padding = new System.Windows.Forms.Padding(12, 8, 12, 4);
            this.kwlHeader.Size = new System.Drawing.Size(1120, 68);
            this.kwlHeader.TabIndex = 0;
            this.kwlHeader.Text = "Card layout (default): drag image files onto the zone or click it to browse. Watch" +
    " the animated stripes (grey = valid, red = rejected). Use Submit/Cancel, or righ" +
    "t-click the preview list for open/remove/sort/undo. Try Delete, Ctrl+Z, Ctrl+C," +
    " and Enter on the list. Switch to Classic layout or apply a preset on the right.";
            //
            // kscWorkspace
            //
            this.kscWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kscWorkspace.Location = new System.Drawing.Point(0, 68);
            this.kscWorkspace.Name = "kscWorkspace";
            this.kscWorkspace.Orientation = System.Windows.Forms.Orientation.Vertical;
            //
            // kscWorkspace.Panel1
            //
            this.kscWorkspace.Panel1.Controls.Add(this.kdzDropZone);
            this.kscWorkspace.Panel1.Padding = new System.Windows.Forms.Padding(8);
            //
            // kscWorkspace.Panel2
            //
            this.kscWorkspace.Panel2.Controls.Add(this.kpnlSettings);
            this.kscWorkspace.Panel2.Padding = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this.kscWorkspace.Size = new System.Drawing.Size(1120, 508);
            this.kscWorkspace.SplitterDistance = 640;
            this.kscWorkspace.TabIndex = 1;
            //
            // kdzDropZone
            //
            this.kdzDropZone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kdzDropZone.Location = new System.Drawing.Point(8, 8);
            this.kdzDropZone.Name = "kdzDropZone";
            this.kdzDropZone.Padding = new System.Windows.Forms.Padding(8);
            this.kdzDropZone.Size = new System.Drawing.Size(616, 492);
            this.kdzDropZone.TabIndex = 0;
            //
            // kpnlSettings
            //
            this.kpnlSettings.AutoScroll = true;
            this.kpnlSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlSettings.Location = new System.Drawing.Point(8, 0);
            this.kpnlSettings.Name = "kpnlSettings";
            this.kpnlSettings.Size = new System.Drawing.Size(452, 500);
            this.kpnlSettings.TabIndex = 0;
            //
            // kpnlLog
            //
            this.kpnlLog.Controls.Add(this.ktbxEventLog);
            this.kpnlLog.Controls.Add(this.kpnlLogHeader);
            this.kpnlLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kpnlLog.Location = new System.Drawing.Point(0, 576);
            this.kpnlLog.Name = "kpnlLog";
            this.kpnlLog.Padding = new System.Windows.Forms.Padding(8);
            this.kpnlLog.Size = new System.Drawing.Size(1120, 160);
            this.kpnlLog.TabIndex = 2;
            //
            // ktbxEventLog
            //
            this.ktbxEventLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktbxEventLog.Font = new System.Drawing.Font("Courier New", 9F);
            this.ktbxEventLog.Location = new System.Drawing.Point(8, 40);
            this.ktbxEventLog.Multiline = true;
            this.ktbxEventLog.Name = "ktbxEventLog";
            this.ktbxEventLog.ReadOnly = true;
            this.ktbxEventLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ktbxEventLog.Size = new System.Drawing.Size(1104, 112);
            this.ktbxEventLog.TabIndex = 1;
            //
            // kpnlLogHeader
            //
            this.kpnlLogHeader.Controls.Add(this.klblEventLogTitle);
            this.kpnlLogHeader.Controls.Add(this.kbtnClearLog);
            this.kpnlLogHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.kpnlLogHeader.Location = new System.Drawing.Point(8, 8);
            this.kpnlLogHeader.Name = "kpnlLogHeader";
            this.kpnlLogHeader.Padding = new System.Windows.Forms.Padding(8, 6, 8, 0);
            this.kpnlLogHeader.Size = new System.Drawing.Size(1104, 32);
            this.kpnlLogHeader.TabIndex = 0;
            //
            // klblEventLogTitle
            //
            this.klblEventLogTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.klblEventLogTitle.Location = new System.Drawing.Point(8, 6);
            this.klblEventLogTitle.Name = "klblEventLogTitle";
            this.klblEventLogTitle.Size = new System.Drawing.Size(120, 26);
            this.klblEventLogTitle.TabIndex = 0;
            this.klblEventLogTitle.Values.Text = "Event log";
            //
            // kbtnClearLog
            //
            this.kbtnClearLog.Dock = System.Windows.Forms.DockStyle.Right;
            this.kbtnClearLog.Location = new System.Drawing.Point(1006, 6);
            this.kbtnClearLog.Name = "kbtnClearLog";
            this.kbtnClearLog.Size = new System.Drawing.Size(90, 26);
            this.kbtnClearLog.TabIndex = 1;
            this.kbtnClearLog.Values.Text = "Clear log";
            this.kbtnClearLog.Click += new System.EventHandler(this.kbtnClearLog_Click);
            //
            // klblSummary
            //
            this.klblSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.klblSummary.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.klblSummary.Location = new System.Drawing.Point(0, 736);
            this.klblSummary.Name = "klblSummary";
            this.klblSummary.Padding = new System.Windows.Forms.Padding(12, 0, 12, 4);
            this.klblSummary.Size = new System.Drawing.Size(1120, 24);
            this.klblSummary.TabIndex = 3;
            this.klblSummary.Values.Text = "Ready.";
            //
            // KryptonDropZoneDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 760);
            this.Controls.Add(this.kscWorkspace);
            this.Controls.Add(this.kpnlLog);
            this.Controls.Add(this.klblSummary);
            this.Controls.Add(this.kwlHeader);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "KryptonDropZoneDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KryptonDropZone Demo";
            ((System.ComponentModel.ISupportInitialize)(this.kscWorkspace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kscWorkspace.Panel1)).EndInit();
            this.kscWorkspace.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kscWorkspace.Panel2)).EndInit();
            this.kscWorkspace.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlLog)).EndInit();
            this.kpnlLog.ResumeLayout(false);
            this.kpnlLog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlLogHeader)).EndInit();
            this.kpnlLogHeader.ResumeLayout(false);
            this.kpnlLogHeader.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonWrapLabel kwlHeader;
        private Krypton.Toolkit.KryptonSplitContainer kscWorkspace;
        private Krypton.Toolkit.Utilities.KryptonDropZone kdzDropZone;
        private Krypton.Toolkit.KryptonPanel kpnlSettings;
        private Krypton.Toolkit.KryptonPanel kpnlLog;
        private Krypton.Toolkit.KryptonTextBox ktbxEventLog;
        private Krypton.Toolkit.KryptonPanel kpnlLogHeader;
        private Krypton.Toolkit.KryptonLabel klblEventLogTitle;
        private Krypton.Toolkit.KryptonButton kbtnClearLog;
        private Krypton.Toolkit.KryptonLabel klblSummary;
    }
}

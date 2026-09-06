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
    partial class WorkspaceRtlDemo
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.kwlblInfo = new Krypton.Toolkit.KryptonWrapLabel();
            this.kpnlOptions = new Krypton.Toolkit.KryptonPanel();
            this.chkRtl = new Krypton.Toolkit.KryptonCheckBox();
            this.kbtnSave = new Krypton.Toolkit.KryptonButton();
            this.kbtnLoad = new Krypton.Toolkit.KryptonButton();
            this.kbtnMaximize = new Krypton.Toolkit.KryptonButton();
            this.kwlblStatus = new Krypton.Toolkit.KryptonWrapLabel();
            this.kryptonWorkspace = new Krypton.Workspace.KryptonWorkspace();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlOptions)).BeginInit();
            this.kpnlOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspace)).BeginInit();
            this.SuspendLayout();
            //
            // kwlblInfo
            //
            this.kwlblInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.kwlblInfo.Location = new System.Drawing.Point(0, 0);
            this.kwlblInfo.Name = "kwlblInfo";
            this.kwlblInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.kwlblInfo.Size = new System.Drawing.Size(980, 88);
            this.kwlblInfo.Text = "Issue #2383 — Toggle RightToLeft + RightToLeftLayout. Horizontal cells pack from the right (A moves to the right of C). Nested Top/Bottom stay top-to-bottom. Children XML order does not change. Drag a page to the physical left/right edge; the new cell appears on that edge. Splitters still resize the panels they sit between.";
            //
            // kpnlOptions
            //
            this.kpnlOptions.Controls.Add(this.kbtnMaximize);
            this.kpnlOptions.Controls.Add(this.kbtnLoad);
            this.kpnlOptions.Controls.Add(this.kbtnSave);
            this.kpnlOptions.Controls.Add(this.chkRtl);
            this.kpnlOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.kpnlOptions.Location = new System.Drawing.Point(0, 88);
            this.kpnlOptions.Name = "kpnlOptions";
            this.kpnlOptions.Size = new System.Drawing.Size(980, 40);
            this.kpnlOptions.TabIndex = 1;
            //
            // chkRtl
            //
            this.chkRtl.Location = new System.Drawing.Point(12, 8);
            this.chkRtl.Name = "chkRtl";
            this.chkRtl.Size = new System.Drawing.Size(240, 24);
            this.chkRtl.TabIndex = 0;
            this.chkRtl.Values.Text = "RightToLeft + RightToLeftLayout";
            this.chkRtl.CheckedChanged += new System.EventHandler(this.OnRtlCheckedChanged);
            //
            // kbtnSave
            //
            this.kbtnSave.Location = new System.Drawing.Point(270, 6);
            this.kbtnSave.Name = "kbtnSave";
            this.kbtnSave.Size = new System.Drawing.Size(110, 28);
            this.kbtnSave.TabIndex = 1;
            this.kbtnSave.Values.Text = "Save layout";
            this.kbtnSave.Click += new System.EventHandler(this.OnSaveLayout);
            //
            // kbtnLoad
            //
            this.kbtnLoad.Location = new System.Drawing.Point(386, 6);
            this.kbtnLoad.Name = "kbtnLoad";
            this.kbtnLoad.Size = new System.Drawing.Size(110, 28);
            this.kbtnLoad.TabIndex = 2;
            this.kbtnLoad.Values.Text = "Load layout";
            this.kbtnLoad.Click += new System.EventHandler(this.OnLoadLayout);
            //
            // kbtnMaximize
            //
            this.kbtnMaximize.Location = new System.Drawing.Point(502, 6);
            this.kbtnMaximize.Name = "kbtnMaximize";
            this.kbtnMaximize.Size = new System.Drawing.Size(140, 28);
            this.kbtnMaximize.TabIndex = 3;
            this.kbtnMaximize.Values.Text = "Maximize / restore";
            this.kbtnMaximize.Click += new System.EventHandler(this.OnMaximizeToggle);
            //
            // kwlblStatus
            //
            this.kwlblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kwlblStatus.Location = new System.Drawing.Point(0, 520);
            this.kwlblStatus.Name = "kwlblStatus";
            this.kwlblStatus.Size = new System.Drawing.Size(980, 64);
            this.kwlblStatus.Text = "Status";
            //
            // kryptonWorkspace
            //
            this.kryptonWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonWorkspace.Location = new System.Drawing.Point(0, 128);
            this.kryptonWorkspace.Name = "kryptonWorkspace";
            this.kryptonWorkspace.Size = new System.Drawing.Size(980, 392);
            this.kryptonWorkspace.TabIndex = 2;
            //
            // WorkspaceRtlDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 584);
            this.Controls.Add(this.kryptonWorkspace);
            this.Controls.Add(this.kwlblStatus);
            this.Controls.Add(this.kpnlOptions);
            this.Controls.Add(this.kwlblInfo);
            this.MinimumSize = new System.Drawing.Size(720, 480);
            this.Name = "WorkspaceRtlDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Workspace RTL (#2383)";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlOptions)).EndInit();
            this.kpnlOptions.ResumeLayout(false);
            this.kpnlOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspace)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonWrapLabel kwlblInfo;
        private Krypton.Toolkit.KryptonPanel kpnlOptions;
        internal Krypton.Toolkit.KryptonCheckBox chkRtl;
        private Krypton.Toolkit.KryptonButton kbtnSave;
        private Krypton.Toolkit.KryptonButton kbtnLoad;
        private Krypton.Toolkit.KryptonButton kbtnMaximize;
        private Krypton.Toolkit.KryptonWrapLabel kwlblStatus;
        internal Krypton.Workspace.KryptonWorkspace kryptonWorkspace;
    }
}

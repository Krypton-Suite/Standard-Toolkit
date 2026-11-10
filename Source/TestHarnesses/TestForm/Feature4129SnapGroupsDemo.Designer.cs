#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class Feature4129SnapGroupsDemo
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
            this.components = new System.ComponentModel.Container();
            this.kryptonPanelMain = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kwlblInstructions = new Krypton.Toolkit.KryptonWrapLabel();
            this.flowToolbar = new System.Windows.Forms.FlowLayoutPanel();
            this.kbtnOpenPeer = new Krypton.Toolkit.KryptonButton();
            this.kchkFloatsInTaskbar = new Krypton.Toolkit.KryptonCheckBox();
            this.kbtnAddAndFloat = new Krypton.Toolkit.KryptonButton();
            this.kryptonPanelContent = new Krypton.Toolkit.KryptonPanel();
            this.kryptonDockableWorkspace1 = new Krypton.Docking.KryptonDockableWorkspace();
            this.kryptonDockingManager1 = new Krypton.Docking.KryptonDockingManager();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).BeginInit();
            this.kryptonPanelMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelContent)).BeginInit();
            this.kryptonPanelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableWorkspace1)).BeginInit();
            this.SuspendLayout();
            //
            // kryptonPanelMain
            //
            this.kryptonPanelMain.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanelMain.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanelMain.Name = "kryptonPanelMain";
            this.kryptonPanelMain.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanelMain.Size = new System.Drawing.Size(900, 560);
            this.kryptonPanelMain.TabIndex = 0;
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.kwlblInstructions, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowToolbar, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.kryptonPanelContent, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.klblStatus, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(876, 536);
            this.tableLayoutPanel1.TabIndex = 0;
            //
            // kwlblInstructions
            //
            this.kwlblInstructions.AutoSize = false;
            this.kwlblInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblInstructions.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kwlblInstructions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kwlblInstructions.LabelStyle = Krypton.Toolkit.LabelStyle.NormalPanel;
            this.kwlblInstructions.Location = new System.Drawing.Point(3, 3);
            this.kwlblInstructions.Name = "kwlblInstructions";
            this.kwlblInstructions.Size = new System.Drawing.Size(870, 72);
            this.kwlblInstructions.Text = "Issue #4129 — Windows 11 Snap Groups.\r\n" +
                "1) Open Peer window, snap both with Win+Left/Right.\r\n" +
                "2) Hover either taskbar button (Settings → Multitasking → show snapped windows).\r\n" +
                "3) Optionally enable Floats in taskbar, then Add + float so the float gets a taskbar button.";
            //
            // flowToolbar
            //
            this.flowToolbar.AutoSize = true;
            this.flowToolbar.Controls.Add(this.kbtnOpenPeer);
            this.flowToolbar.Controls.Add(this.kchkFloatsInTaskbar);
            this.flowToolbar.Controls.Add(this.kbtnAddAndFloat);
            this.flowToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowToolbar.Location = new System.Drawing.Point(3, 81);
            this.flowToolbar.Name = "flowToolbar";
            this.flowToolbar.Size = new System.Drawing.Size(870, 40);
            this.flowToolbar.TabIndex = 1;
            //
            // kbtnOpenPeer
            //
            this.kbtnOpenPeer.Location = new System.Drawing.Point(3, 3);
            this.kbtnOpenPeer.Name = "kbtnOpenPeer";
            this.kbtnOpenPeer.Size = new System.Drawing.Size(160, 32);
            this.kbtnOpenPeer.TabIndex = 0;
            this.kbtnOpenPeer.Values.Text = "Open peer window";
            this.kbtnOpenPeer.Click += new System.EventHandler(this.kbtnOpenPeer_Click);
            //
            // kchkFloatsInTaskbar
            //
            this.kchkFloatsInTaskbar.Location = new System.Drawing.Point(169, 8);
            this.kchkFloatsInTaskbar.Name = "kchkFloatsInTaskbar";
            this.kchkFloatsInTaskbar.Size = new System.Drawing.Size(180, 20);
            this.kchkFloatsInTaskbar.TabIndex = 1;
            this.kchkFloatsInTaskbar.Values.Text = "Floats in taskbar (opt-in)";
            this.kchkFloatsInTaskbar.CheckedChanged += new System.EventHandler(this.kchkFloatsInTaskbar_CheckedChanged);
            //
            // kbtnAddAndFloat
            //
            this.kbtnAddAndFloat.Location = new System.Drawing.Point(355, 3);
            this.kbtnAddAndFloat.Name = "kbtnAddAndFloat";
            this.kbtnAddAndFloat.Size = new System.Drawing.Size(140, 32);
            this.kbtnAddAndFloat.TabIndex = 2;
            this.kbtnAddAndFloat.Values.Text = "Add + float";
            this.kbtnAddAndFloat.Click += new System.EventHandler(this.kbtnAddAndFloat_Click);
            //
            // kryptonPanelContent
            //
            this.kryptonPanelContent.Controls.Add(this.kryptonDockableWorkspace1);
            this.kryptonPanelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanelContent.Location = new System.Drawing.Point(3, 127);
            this.kryptonPanelContent.Name = "kryptonPanelContent";
            this.kryptonPanelContent.Size = new System.Drawing.Size(870, 370);
            this.kryptonPanelContent.TabIndex = 2;
            //
            // kryptonDockableWorkspace1
            //
            this.kryptonDockableWorkspace1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonDockableWorkspace1.Location = new System.Drawing.Point(0, 0);
            this.kryptonDockableWorkspace1.Name = "kryptonDockableWorkspace1";
            this.kryptonDockableWorkspace1.Size = new System.Drawing.Size(870, 370);
            this.kryptonDockableWorkspace1.TabIndex = 0;
            //
            // klblStatus
            //
            this.klblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblStatus.Location = new System.Drawing.Point(3, 503);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(870, 30);
            this.klblStatus.TabIndex = 3;
            this.klblStatus.Values.Text = "Status";
            //
            // Feature4129SnapGroupsDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 560);
            this.Controls.Add(this.kryptonPanelMain);
            this.Name = "Feature4129SnapGroupsDemo";
            this.ShowInTaskbar = true;
            this.Text = "Feature 4129 Snap Groups";
            this.Load += new System.EventHandler(this.Feature4129SnapGroupsDemo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).EndInit();
            this.kryptonPanelMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowToolbar.ResumeLayout(false);
            this.flowToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelContent)).EndInit();
            this.kryptonPanelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableWorkspace1)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Krypton.Toolkit.KryptonWrapLabel kwlblInstructions;
        private System.Windows.Forms.FlowLayoutPanel flowToolbar;
        private Krypton.Toolkit.KryptonButton kbtnOpenPeer;
        private Krypton.Toolkit.KryptonCheckBox kchkFloatsInTaskbar;
        private Krypton.Toolkit.KryptonButton kbtnAddAndFloat;
        private Krypton.Toolkit.KryptonPanel kryptonPanelContent;
        private Krypton.Docking.KryptonDockableWorkspace kryptonDockableWorkspace1;
        private Krypton.Docking.KryptonDockingManager kryptonDockingManager1;
        private Krypton.Toolkit.KryptonLabel klblStatus;
    }
}

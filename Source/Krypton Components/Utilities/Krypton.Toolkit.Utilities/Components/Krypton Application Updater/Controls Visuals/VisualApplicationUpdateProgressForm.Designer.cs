#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

internal partial class VisualApplicationUpdateProgressForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            this.kryptonPanel = new Krypton.Toolkit.KryptonPanel();
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.lblStatus = new Krypton.Toolkit.KryptonLabel();
            this.progressHost = new System.Windows.Forms.Panel();
            this.transferProgressPanel = new Krypton.Graphs.KryptonTransferProgressPanel();
            this.progressDownload = new Krypton.Toolkit.KryptonProgressBar();
            this.btnDetails = new Krypton.Toolkit.KryptonButton();
            this.btnResume = new Krypton.Toolkit.KryptonButton();
            this.btnRestart = new Krypton.Toolkit.KryptonButton();
            this.btnClose = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.detailsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblFile = new Krypton.Toolkit.KryptonLabel();
            this.lblDownloaded = new Krypton.Toolkit.KryptonLabel();
            this.lblSpeed = new Krypton.Toolkit.KryptonLabel();
            this.lblRemaining = new Krypton.Toolkit.KryptonLabel();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.table.SuspendLayout();
            this.progressHost.SuspendLayout();
            this.detailsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.table);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanel.Size = new System.Drawing.Size(640, 150);
            this.kryptonPanel.TabIndex = 0;
            // 
            // table
            // 
            this.table.BackColor = System.Drawing.Color.Transparent;
            this.table.ColumnCount = 1;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.Controls.Add(this.lblStatus, 0, 0);
            this.table.Controls.Add(this.progressHost, 0, 1);
            this.table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table.Location = new System.Drawing.Point(12, 12);
            this.table.Name = "table";
            this.table.RowCount = 2;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.Size = new System.Drawing.Size(616, 126);
            this.table.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.lblStatus.Location = new System.Drawing.Point(3, 3);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(610, 20);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Values.Text = "Downloading update...";
            // 
            // progressHost
            // 
            this.progressHost.BackColor = System.Drawing.Color.Transparent;
            this.progressHost.Controls.Add(this.transferProgressPanel);
            this.progressHost.Controls.Add(this.progressDownload);
            this.progressHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressHost.Location = new System.Drawing.Point(3, 29);
            this.progressHost.Name = "progressHost";
            this.progressHost.Size = new System.Drawing.Size(610, 94);
            this.progressHost.TabIndex = 1;
            // 
            // transferProgressPanel
            // 
            this.transferProgressPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transferProgressPanel.Location = new System.Drawing.Point(0, 0);
            this.transferProgressPanel.Name = "transferProgressPanel";
            this.transferProgressPanel.Size = new System.Drawing.Size(610, 94);
            this.transferProgressPanel.TabIndex = 1;
            this.transferProgressPanel.TabStop = false;
            this.transferProgressPanel.Visible = false;
            // 
            // progressDownload
            // 
            this.progressDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressDownload.Location = new System.Drawing.Point(0, 0);
            this.progressDownload.Name = "progressDownload";
            this.progressDownload.Size = new System.Drawing.Size(610, 94);
            this.progressDownload.TabIndex = 0;
            // 
            // btnDetails
            // 
            this.btnDetails.Location = new System.Drawing.Point(20, 6);
            this.btnDetails.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(140, 32);
            this.btnDetails.TabIndex = 0;
            this.btnDetails.Values.Text = "Details";
            // 
            // btnResume
            // 
            this.btnResume.Location = new System.Drawing.Point(172, 6);
            this.btnResume.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(110, 32);
            this.btnResume.TabIndex = 0;
            this.btnResume.Values.Text = "Resume";
            this.btnResume.Visible = false;
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(294, 6);
            this.btnRestart.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(110, 32);
            this.btnRestart.TabIndex = 1;
            this.btnRestart.Values.Text = "Restart";
            this.btnRestart.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(416, 6);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 32);
            this.btnClose.TabIndex = 2;
            this.btnClose.Values.Text = "Close";
            this.btnClose.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(528, 6);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 32);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Values.Text = "Cancel";
            // 
            // detailsPanel
            // 
            this.detailsPanel.BackColor = System.Drawing.Color.Transparent;
            this.detailsPanel.ColumnCount = 1;
            this.detailsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.detailsPanel.Controls.Add(this.lblFile, 0, 0);
            this.detailsPanel.Controls.Add(this.lblDownloaded, 0, 1);
            this.detailsPanel.Controls.Add(this.lblSpeed, 0, 2);
            this.detailsPanel.Controls.Add(this.lblRemaining, 0, 3);
            this.detailsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.detailsPanel.Location = new System.Drawing.Point(0, 50);
            this.detailsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.detailsPanel.Name = "detailsPanel";
            this.detailsPanel.Padding = new System.Windows.Forms.Padding(12, 0, 12, 8);
            this.detailsPanel.RowCount = 4;
            this.detailsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.detailsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.detailsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.detailsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.detailsPanel.Size = new System.Drawing.Size(640, 0);
            this.detailsPanel.TabIndex = 4;
            this.detailsPanel.Visible = false;
            // 
            // lblFile
            // 
            this.lblFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFile.Location = new System.Drawing.Point(15, 3);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(610, 14);
            this.lblFile.TabIndex = 0;
            this.lblFile.Values.Text = "File name:";
            // 
            // lblDownloaded
            // 
            this.lblDownloaded.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDownloaded.Location = new System.Drawing.Point(15, 20);
            this.lblDownloaded.Name = "lblDownloaded";
            this.lblDownloaded.Size = new System.Drawing.Size(610, 14);
            this.lblDownloaded.TabIndex = 1;
            this.lblDownloaded.Values.Text = "Downloaded:";
            // 
            // lblSpeed
            // 
            this.lblSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSpeed.Location = new System.Drawing.Point(15, 37);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(610, 14);
            this.lblSpeed.TabIndex = 2;
            this.lblSpeed.Values.Text = "Speed:";
            // 
            // lblRemaining
            // 
            this.lblRemaining.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRemaining.Location = new System.Drawing.Point(15, 54);
            this.lblRemaining.Name = "lblRemaining";
            this.lblRemaining.Size = new System.Drawing.Size(610, 14);
            this.lblRemaining.TabIndex = 3;
            this.lblRemaining.Values.Text = "Time remaining:";
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btnDetails);
            this.kryptonPanel1.Controls.Add(this.detailsPanel);
            this.kryptonPanel1.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonPanel1.Controls.Add(this.btnResume);
            this.kryptonPanel1.Controls.Add(this.btnCancel);
            this.kryptonPanel1.Controls.Add(this.btnRestart);
            this.kryptonPanel1.Controls.Add(this.btnClose);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 150);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(640, 50);
            this.kryptonPanel1.TabIndex = 2;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(640, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // VisualApplicationUpdateProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 200);
            this.Controls.Add(this.kryptonPanel);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualApplicationUpdateProgressForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update available";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.table.ResumeLayout(false);
            this.table.PerformLayout();
            this.progressHost.ResumeLayout(false);
            this.detailsPanel.ResumeLayout(false);
            this.detailsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

    }

    private Krypton.Toolkit.KryptonPanel kryptonPanel;
    private System.Windows.Forms.TableLayoutPanel table;
    private Krypton.Toolkit.KryptonLabel lblStatus;
    private System.Windows.Forms.Panel progressHost;
    private Krypton.Graphs.KryptonTransferProgressPanel transferProgressPanel;
    private Krypton.Toolkit.KryptonProgressBar progressDownload;
    private Krypton.Toolkit.KryptonButton btnDetails;
    private System.Windows.Forms.TableLayoutPanel detailsPanel;
    private Krypton.Toolkit.KryptonLabel lblFile;
    private Krypton.Toolkit.KryptonLabel lblDownloaded;
    private Krypton.Toolkit.KryptonLabel lblSpeed;
    private Krypton.Toolkit.KryptonLabel lblRemaining;
    private Krypton.Toolkit.KryptonButton btnResume;
    private Krypton.Toolkit.KryptonButton btnRestart;
    private Krypton.Toolkit.KryptonButton btnClose;
    private Krypton.Toolkit.KryptonButton btnCancel;
    private KryptonPanel kryptonPanel1;
    private KryptonBorderEdge kryptonBorderEdge1;
}

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
            this.progressDownload = new Krypton.Toolkit.KryptonProgressBar();
            this.buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDetails = new Krypton.Toolkit.KryptonButton();
            this.detailsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblFile = new Krypton.Toolkit.KryptonLabel();
            this.lblDownloaded = new Krypton.Toolkit.KryptonLabel();
            this.lblSpeed = new Krypton.Toolkit.KryptonLabel();
            this.lblRemaining = new Krypton.Toolkit.KryptonLabel();
            this.btnResume = new Krypton.Toolkit.KryptonButton();
            this.btnRestart = new Krypton.Toolkit.KryptonButton();
            this.btnClose = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.table.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.detailsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.table);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanel.Size = new System.Drawing.Size(640, 200);
            this.kryptonPanel.TabIndex = 0;
            // 
            // table
            // 
            this.table.BackColor = System.Drawing.Color.Transparent;
            this.table.ColumnCount = 1;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.Controls.Add(this.lblStatus, 0, 0);
            this.table.Controls.Add(this.progressDownload, 0, 1);
            this.table.Controls.Add(this.buttonPanel, 0, 2);
            this.table.Controls.Add(this.detailsPanel, 0, 3);
            this.table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table.Location = new System.Drawing.Point(12, 12);
            this.table.Name = "table";
            this.table.RowCount = 4;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.table.Size = new System.Drawing.Size(616, 176);
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
            // progressDownload
            // 
            this.progressDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressDownload.Location = new System.Drawing.Point(3, 29);
            this.progressDownload.Name = "progressDownload";
            this.progressDownload.Size = new System.Drawing.Size(610, 92);
            this.progressDownload.TabIndex = 1;
            // 
            // buttonPanel
            // 
            this.buttonPanel.AutoSize = false;
            this.buttonPanel.Controls.Add(this.btnDetails);
            this.buttonPanel.Controls.Add(this.btnResume);
            this.buttonPanel.Controls.Add(this.btnRestart);
            this.buttonPanel.Controls.Add(this.btnClose);
            this.buttonPanel.Controls.Add(this.btnCancel);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.buttonPanel.Location = new System.Drawing.Point(0, 124);
            this.buttonPanel.Margin = new System.Windows.Forms.Padding(0);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Padding = new System.Windows.Forms.Padding(0);
            this.buttonPanel.Size = new System.Drawing.Size(616, 52);
            this.buttonPanel.TabIndex = 2;
            this.buttonPanel.WrapContents = false;
            // 
            // btnDetails
            // 
            this.btnDetails.AutoSize = false;
            this.btnDetails.Location = new System.Drawing.Point(0, 8);
            this.btnDetails.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(140, 32);
            this.btnDetails.TabIndex = 0;
            this.btnDetails.Values.Text = "Details";
            // 
            // detailsPanel
            // 
            this.detailsPanel.ColumnCount = 1;
            this.detailsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.detailsPanel.Controls.Add(this.lblFile, 0, 0);
            this.detailsPanel.Controls.Add(this.lblDownloaded, 0, 1);
            this.detailsPanel.Controls.Add(this.lblSpeed, 0, 2);
            this.detailsPanel.Controls.Add(this.lblRemaining, 0, 3);
            this.detailsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailsPanel.Location = new System.Drawing.Point(0, 176);
            this.detailsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.detailsPanel.Name = "detailsPanel";
            this.detailsPanel.RowCount = 4;
            this.detailsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.detailsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.detailsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.detailsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.detailsPanel.Size = new System.Drawing.Size(616, 0);
            this.detailsPanel.TabIndex = 3;
            this.detailsPanel.Visible = false;
            // 
            // lblFile
            // 
            this.lblFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFile.Name = "lblFile";
            this.lblFile.Values.Text = "File name:";
            // 
            // lblDownloaded
            // 
            this.lblDownloaded.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDownloaded.Name = "lblDownloaded";
            this.lblDownloaded.Values.Text = "Downloaded:";
            // 
            // lblSpeed
            // 
            this.lblSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Values.Text = "Speed:";
            // 
            // lblRemaining
            // 
            this.lblRemaining.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRemaining.Name = "lblRemaining";
            this.lblRemaining.Values.Text = "Time remaining:";
            // 
            // btnResume
            // 
            this.btnResume.AutoSize = false;
            this.btnResume.Location = new System.Drawing.Point(0, 8);
            this.btnResume.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(110, 32);
            this.btnResume.TabIndex = 0;
            this.btnResume.Values.Text = "Resume";
            this.btnResume.Visible = false;
            // 
            // btnRestart
            // 
            this.btnRestart.AutoSize = false;
            this.btnRestart.Location = new System.Drawing.Point(122, 8);
            this.btnRestart.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(110, 32);
            this.btnRestart.TabIndex = 1;
            this.btnRestart.Values.Text = "Restart";
            this.btnRestart.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = false;
            this.btnClose.Location = new System.Drawing.Point(244, 8);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 32);
            this.btnClose.TabIndex = 2;
            this.btnClose.Values.Text = "Close";
            this.btnClose.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = false;
            this.btnCancel.Location = new System.Drawing.Point(356, 8);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 32);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Values.Text = "Cancel";
            // 
            // VisualApplicationUpdateProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 200);
            this.Controls.Add(this.kryptonPanel);
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
            this.buttonPanel.ResumeLayout(false);
            this.detailsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    private Krypton.Toolkit.KryptonPanel kryptonPanel;
    private System.Windows.Forms.TableLayoutPanel table;
    private Krypton.Toolkit.KryptonLabel lblStatus;
    private Krypton.Toolkit.KryptonProgressBar progressDownload;
    private System.Windows.Forms.FlowLayoutPanel buttonPanel;
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
}

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Dialogs;

internal partial class VisualFileOperationProgressForm
{
    /// <summary>Builds the progress layout.</summary>
    private void InitializeComponent()
    {
        _pnlBanner = new KryptonPanel();
        _tlpBanner = new KryptonTableLayoutPanel();
        _picBanner = new PictureBox();
        _lblBanner = new KryptonWrapLabel();
        _borderBanner = new KryptonBorderEdge();
        _tlpContent = new KryptonTableLayoutPanel();
        _pnlProgressHost = new KryptonPanel();
        _pnlButtons = new KryptonPanel();
        _borderButtons = new KryptonBorderEdge();
        _lblItem = new KryptonLabel();
        _lblFrom = new KryptonLabel();
        _lblTo = new KryptonLabel();
        _progress = new KryptonProgressBar();
        _lblDetails = new KryptonLabel();
        _lblError = new KryptonLabel();
        _btnMoreDetails = new KryptonButton();
        _speedGraph = new KryptonTransferSpeedGraph();
        _btnPause = new KryptonButton();
        _btnCancel = new KryptonButton();
        ((System.ComponentModel.ISupportInitialize)_pnlBanner).BeginInit();
        _pnlBanner.SuspendLayout();
        _tlpBanner.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_picBanner).BeginInit();
        _tlpContent.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_pnlProgressHost).BeginInit();
        _pnlProgressHost.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_pnlButtons).BeginInit();
        _pnlButtons.SuspendLayout();
        SuspendLayout();

        _picBanner.Name = "picBanner";
        _picBanner.TabIndex = 0;
        _lblBanner.Name = "lblBanner";
        _lblBanner.TabIndex = 1;
        _lblBanner.Text = "Calculating...";
        _lblBanner.StateCommon.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
        FileOperationBannerLayout.ConfigureTable(_tlpBanner, _picBanner, _lblBanner);
        _tlpBanner.Name = "tlpBanner";
        _tlpBanner.TabIndex = 0;

        _borderBanner.BorderStyle = PaletteBorderStyle.HeaderPrimary;
        _borderBanner.Dock = DockStyle.Bottom;
        _borderBanner.Name = "borderBanner";
        _borderBanner.Size = new Size(560, 1);
        _borderBanner.Text = "";

        _pnlBanner.Controls.Add(_tlpBanner);
        _pnlBanner.Controls.Add(_borderBanner);
        _pnlBanner.Dock = DockStyle.Top;
        _pnlBanner.Location = new Point(0, 0);
        _pnlBanner.Name = "pnlBanner";
        _pnlBanner.Size = new Size(560, FileOperationBannerLayout.DefaultHeight);
        _pnlBanner.TabIndex = 0;

        _lblItem.AutoSize = false;
        _lblItem.Dock = DockStyle.Fill;
        _lblItem.Margin = new Padding(0, 0, 0, 4);
        _lblItem.Name = "lblItem";
        _lblItem.TabIndex = 0;
        _lblItem.StateCommon.ShortText.Trim = PaletteTextTrim.EllipsisPath;

        _lblFrom.AutoSize = false;
        _lblFrom.Dock = DockStyle.Fill;
        _lblFrom.Margin = new Padding(0, 0, 0, 2);
        _lblFrom.Name = "lblFrom";
        _lblFrom.TabIndex = 1;
        _lblFrom.StateCommon.ShortText.Trim = PaletteTextTrim.EllipsisPath;

        _lblTo.AutoSize = false;
        _lblTo.Dock = DockStyle.Fill;
        _lblTo.Margin = new Padding(0, 0, 0, 8);
        _lblTo.Name = "lblTo";
        _lblTo.TabIndex = 2;
        _lblTo.StateCommon.ShortText.Trim = PaletteTextTrim.EllipsisPath;

        _progress.Dock = DockStyle.Fill;
        _progress.Name = "progress";
        _progress.TabIndex = 0;

        _speedGraph.Dock = DockStyle.Fill;
        _speedGraph.Name = "speedGraph";
        _speedGraph.TabIndex = 1;
        _speedGraph.Visible = false;

        _pnlProgressHost.Controls.Add(_speedGraph);
        _pnlProgressHost.Controls.Add(_progress);
        _pnlProgressHost.Dock = DockStyle.Fill;
        _pnlProgressHost.Margin = new Padding(0, 0, 0, 8);
        _pnlProgressHost.Name = "pnlProgressHost";
        _pnlProgressHost.TabIndex = 3;

        _lblDetails.AutoSize = false;
        _lblDetails.Dock = DockStyle.Fill;
        _lblDetails.Margin = new Padding(0, 0, 0, 4);
        _lblDetails.Name = "lblDetails";
        _lblDetails.TabIndex = 4;

        _lblError.AutoSize = false;
        _lblError.Dock = DockStyle.Fill;
        _lblError.Margin = Padding.Empty;
        _lblError.Name = "lblError";
        _lblError.TabIndex = 5;
        _lblError.Visible = false;

        _tlpContent.ColumnCount = 1;
        _tlpContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
        _tlpContent.Controls.Add(_lblItem, 0, 0);
        _tlpContent.Controls.Add(_lblFrom, 0, 1);
        _tlpContent.Controls.Add(_lblTo, 0, 2);
        _tlpContent.Controls.Add(_pnlProgressHost, 0, 3);
        _tlpContent.Controls.Add(_lblDetails, 0, 4);
        _tlpContent.Controls.Add(_lblError, 0, 5);
        _tlpContent.Dock = DockStyle.Fill;
        _tlpContent.Location = new Point(0, FileOperationBannerLayout.DefaultHeight);
        _tlpContent.Name = "tlpContent";
        _tlpContent.Padding = new Padding(16);
        _tlpContent.RowCount = 6;
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 24f));
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 24f));
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 32f));
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 28f));
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 40f));
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 24f));
        _tlpContent.Size = new Size(560, 194);
        _tlpContent.TabIndex = 1;

        _btnMoreDetails.Location = new Point(12, 11);
        _btnMoreDetails.Name = "btnMoreDetails";
        _btnMoreDetails.Size = new Size(120, 28);
        _btnMoreDetails.TabIndex = 0;
        _btnMoreDetails.Text = "More details";
        _btnMoreDetails.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

        _btnPause.Location = new Point(352, 11);
        _btnPause.Name = "btnPause";
        _btnPause.Size = new Size(90, 28);
        _btnPause.TabIndex = 1;
        _btnPause.Text = "Pause";
        _btnPause.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

        _btnCancel.Location = new Point(458, 11);
        _btnCancel.Name = "btnCancel";
        _btnCancel.Size = new Size(90, 28);
        _btnCancel.TabIndex = 2;
        _btnCancel.Text = "Cancel";
        _btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

        _borderButtons.BorderStyle = PaletteBorderStyle.HeaderPrimary;
        _borderButtons.Dock = DockStyle.Top;
        _borderButtons.Name = "borderButtons";
        _borderButtons.Size = new Size(560, 1);
        _borderButtons.Text = "";

        _pnlButtons.Controls.Add(_btnCancel);
        _pnlButtons.Controls.Add(_btnPause);
        _pnlButtons.Controls.Add(_btnMoreDetails);
        _pnlButtons.Controls.Add(_borderButtons);
        _pnlButtons.Dock = DockStyle.Bottom;
        _pnlButtons.Location = new Point(0, 278);
        _pnlButtons.Name = "pnlButtons";
        _pnlButtons.PanelBackStyle = PaletteBackStyle.PanelAlternate;
        _pnlButtons.Size = new Size(560, 50);
        _pnlButtons.TabIndex = 2;

        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(560, 348);
        Controls.Add(_tlpContent);
        Controls.Add(_pnlButtons);
        Controls.Add(_pnlBanner);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = true;
        Name = "VisualFileOperationProgressForm";
        ShowInTaskbar = true;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Calculating...";
        CancelButton = _btnCancel;
        ((System.ComponentModel.ISupportInitialize)_picBanner).EndInit();
        _tlpBanner.ResumeLayout(false);
        _tlpBanner.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)_pnlBanner).EndInit();
        _pnlBanner.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_pnlProgressHost).EndInit();
        _pnlProgressHost.ResumeLayout(false);
        _tlpContent.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_pnlButtons).EndInit();
        _pnlButtons.ResumeLayout(false);
        _pnlButtons.PerformLayout();
        ResumeLayout(false);
    }

    private KryptonPanel _pnlBanner;
    private KryptonTableLayoutPanel _tlpBanner;
    private PictureBox _picBanner;
    private KryptonWrapLabel _lblBanner;
    private KryptonBorderEdge _borderBanner;
    private KryptonTableLayoutPanel _tlpContent;
    private KryptonPanel _pnlProgressHost;
    private KryptonPanel _pnlButtons;
    private KryptonBorderEdge _borderButtons;
    private KryptonLabel _lblItem;
    private KryptonLabel _lblFrom;
    private KryptonLabel _lblTo;
    private KryptonProgressBar _progress;
    private KryptonLabel _lblDetails;
    private KryptonLabel _lblError;
    private KryptonButton _btnMoreDetails;
    private KryptonTransferSpeedGraph _speedGraph;
    private KryptonButton _btnPause;
    private KryptonButton _btnCancel;
}

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
        _picBanner = new PictureBox();
        _lblBanner = new KryptonLabel();
        _borderBanner = new KryptonBorderEdge();
        _pnlContent = new KryptonPanel();
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
        ((System.ComponentModel.ISupportInitialize)_picBanner).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_pnlContent).BeginInit();
        _pnlContent.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_pnlButtons).BeginInit();
        _pnlButtons.SuspendLayout();
        SuspendLayout();

        _picBanner.BackColor = Color.Transparent;
        _picBanner.Location = new Point(12, 12);
        _picBanner.Name = "picBanner";
        _picBanner.Size = new Size(32, 32);
        _picBanner.SizeMode = PictureBoxSizeMode.CenterImage;
        _picBanner.TabIndex = 0;
        _picBanner.TabStop = false;

        _lblBanner.AutoSize = false;
        _lblBanner.Location = new Point(56, 12);
        _lblBanner.Name = "lblBanner";
        _lblBanner.Size = new Size(492, 32);
        _lblBanner.TabIndex = 1;
        _lblBanner.Text = "Calculating...";
        _lblBanner.StateCommon.ShortText.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
        _lblBanner.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        _borderBanner.BorderStyle = PaletteBorderStyle.HeaderPrimary;
        _borderBanner.Dock = DockStyle.Bottom;
        _borderBanner.Location = new Point(0, 55);
        _borderBanner.Name = "borderBanner";
        _borderBanner.Size = new Size(560, 1);
        _borderBanner.Text = "";

        _pnlBanner.Controls.Add(_lblBanner);
        _pnlBanner.Controls.Add(_picBanner);
        _pnlBanner.Controls.Add(_borderBanner);
        _pnlBanner.Dock = DockStyle.Top;
        _pnlBanner.Location = new Point(0, 0);
        _pnlBanner.Name = "pnlBanner";
        _pnlBanner.Size = new Size(560, 56);
        _pnlBanner.TabIndex = 0;

        _lblItem.AutoSize = false;
        _lblItem.Location = new Point(12, 12);
        _lblItem.Name = "lblItem";
        _lblItem.Size = new Size(536, 20);
        _lblItem.TabIndex = 0;
        _lblItem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        _lblFrom.AutoSize = false;
        _lblFrom.Location = new Point(12, 36);
        _lblFrom.Name = "lblFrom";
        _lblFrom.Size = new Size(536, 20);
        _lblFrom.TabIndex = 1;
        _lblFrom.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        _lblTo.AutoSize = false;
        _lblTo.Location = new Point(12, 60);
        _lblTo.Name = "lblTo";
        _lblTo.Size = new Size(536, 20);
        _lblTo.TabIndex = 2;
        _lblTo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        _progress.Location = new Point(12, 88);
        _progress.Name = "progress";
        _progress.Size = new Size(536, 23);
        _progress.TabIndex = 3;
        _progress.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        _lblDetails.AutoSize = false;
        _lblDetails.Location = new Point(12, 120);
        _lblDetails.Name = "lblDetails";
        _lblDetails.Size = new Size(536, 36);
        _lblDetails.TabIndex = 4;
        _lblDetails.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        _lblError.AutoSize = false;
        _lblError.Location = new Point(12, 164);
        _lblError.Name = "lblError";
        _lblError.Size = new Size(536, 20);
        _lblError.TabIndex = 5;
        _lblError.Visible = false;
        _lblError.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        // Same slot as the progress bar; shown only when More details is expanded.
        _speedGraph.Location = new Point(12, 88);
        _speedGraph.Name = "speedGraph";
        _speedGraph.Size = new Size(536, 110);
        _speedGraph.TabIndex = 6;
        _speedGraph.Visible = false;
        _speedGraph.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        _pnlContent.Controls.Add(_speedGraph);
        _pnlContent.Controls.Add(_lblError);
        _pnlContent.Controls.Add(_lblDetails);
        _pnlContent.Controls.Add(_progress);
        _pnlContent.Controls.Add(_lblTo);
        _pnlContent.Controls.Add(_lblFrom);
        _pnlContent.Controls.Add(_lblItem);
        _pnlContent.Dock = DockStyle.Fill;
        _pnlContent.Location = new Point(0, 56);
        _pnlContent.Name = "pnlContent";
        _pnlContent.Size = new Size(560, 194);
        _pnlContent.TabIndex = 1;

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
        _borderButtons.Location = new Point(0, 0);
        _borderButtons.Name = "borderButtons";
        _borderButtons.Size = new Size(560, 1);
        _borderButtons.Text = "";

        _pnlButtons.Controls.Add(_btnCancel);
        _pnlButtons.Controls.Add(_btnPause);
        _pnlButtons.Controls.Add(_btnMoreDetails);
        _pnlButtons.Controls.Add(_borderButtons);
        _pnlButtons.Dock = DockStyle.Bottom;
        _pnlButtons.Location = new Point(0, 250);
        _pnlButtons.Name = "pnlButtons";
        _pnlButtons.PanelBackStyle = PaletteBackStyle.PanelAlternate;
        _pnlButtons.Size = new Size(560, 50);
        _pnlButtons.TabIndex = 2;

        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(560, 300);
        Controls.Add(_pnlContent);
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
        ((System.ComponentModel.ISupportInitialize)_pnlBanner).EndInit();
        _pnlBanner.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_pnlContent).EndInit();
        _pnlContent.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_pnlButtons).EndInit();
        _pnlButtons.ResumeLayout(false);
        _pnlButtons.PerformLayout();
        ResumeLayout(false);
    }

    private KryptonPanel _pnlBanner;
    private PictureBox _picBanner;
    private KryptonLabel _lblBanner;
    private KryptonBorderEdge _borderBanner;
    private KryptonPanel _pnlContent;
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

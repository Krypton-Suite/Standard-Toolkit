#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Dialogs;

internal partial class VisualFileConflictForm
{
    /// <summary>Builds the conflict layout.</summary>
    private void InitializeComponent()
    {
        _pnlBanner = new KryptonPanel();
        _tlpBanner = new KryptonTableLayoutPanel();
        _picBanner = new PictureBox();
        _lblBanner = new KryptonWrapLabel();
        _borderBanner = new KryptonBorderEdge();
        _tlpContent = new KryptonTableLayoutPanel();
        _pnlButtons = new KryptonPanel();
        _borderButtons = new KryptonBorderEdge();
        _lblIntro = new KryptonWrapLabel();
        _lblSource = new KryptonLabel();
        _lblDestination = new KryptonLabel();
        _chkApply = new KryptonCheckBox();
        _btnReplace = new KryptonButton();
        _btnSkip = new KryptonButton();
        _btnKeepBoth = new KryptonButton();
        _btnCancel = new KryptonButton();
        ((System.ComponentModel.ISupportInitialize)_pnlBanner).BeginInit();
        _pnlBanner.SuspendLayout();
        _tlpBanner.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_picBanner).BeginInit();
        _tlpContent.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_pnlButtons).BeginInit();
        _pnlButtons.SuspendLayout();
        SuspendLayout();

        _picBanner.Name = "picBanner";
        _picBanner.TabIndex = 0;
        _lblBanner.Name = "lblBanner";
        _lblBanner.TabIndex = 1;
        _lblBanner.StateCommon.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
        FileOperationBannerLayout.ConfigureTable(_tlpBanner, _picBanner, _lblBanner);
        _tlpBanner.Name = "tlpBanner";
        _tlpBanner.TabIndex = 0;

        _borderBanner.BorderStyle = PaletteBorderStyle.HeaderPrimary;
        _borderBanner.Dock = DockStyle.Bottom;
        _borderBanner.Name = "borderBanner";
        _borderBanner.Size = new Size(520, 1);
        _borderBanner.Text = "";

        _pnlBanner.Controls.Add(_tlpBanner);
        _pnlBanner.Controls.Add(_borderBanner);
        _pnlBanner.Dock = DockStyle.Top;
        _pnlBanner.Location = new Point(0, 0);
        _pnlBanner.Name = "pnlBanner";
        _pnlBanner.Size = new Size(520, FileOperationBannerLayout.DefaultHeight);
        _pnlBanner.TabIndex = 0;

        _lblIntro.AutoSize = true;
        _lblIntro.Dock = DockStyle.Fill;
        _lblIntro.Margin = new Padding(0, 0, 0, 8);
        _lblIntro.MaximumSize = new Size(0, 0);
        _lblIntro.Name = "lblIntro";
        _lblIntro.TabIndex = 0;
        _lblIntro.Visible = false;

        _lblSource.AutoSize = false;
        _lblSource.Dock = DockStyle.Fill;
        _lblSource.Margin = new Padding(0, 0, 0, 4);
        _lblSource.Name = "lblSource";
        _lblSource.Size = new Size(488, 28);
        _lblSource.TabIndex = 1;
        _lblSource.StateCommon.ShortText.Trim = PaletteTextTrim.EllipsisPath;

        _lblDestination.AutoSize = false;
        _lblDestination.Dock = DockStyle.Fill;
        _lblDestination.Margin = new Padding(0, 0, 0, 8);
        _lblDestination.Name = "lblDestination";
        _lblDestination.Size = new Size(488, 28);
        _lblDestination.TabIndex = 2;
        _lblDestination.StateCommon.ShortText.Trim = PaletteTextTrim.EllipsisPath;

        _chkApply.Dock = DockStyle.Fill;
        _chkApply.Margin = Padding.Empty;
        _chkApply.Name = "chkApply";
        _chkApply.TabIndex = 3;

        _tlpContent.ColumnCount = 1;
        _tlpContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
        _tlpContent.Controls.Add(_lblIntro, 0, 0);
        _tlpContent.Controls.Add(_lblSource, 0, 1);
        _tlpContent.Controls.Add(_lblDestination, 0, 2);
        _tlpContent.Controls.Add(_chkApply, 0, 3);
        _tlpContent.Dock = DockStyle.Fill;
        _tlpContent.Location = new Point(0, FileOperationBannerLayout.DefaultHeight);
        _tlpContent.Name = "tlpContent";
        _tlpContent.Padding = new Padding(16);
        _tlpContent.RowCount = 5;
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 0f));
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 32f));
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 36f));
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
        _tlpContent.Size = new Size(520, 134);
        _tlpContent.TabIndex = 1;

        _btnReplace.Location = new Point(12, 11);
        _btnReplace.Name = "btnReplace";
        _btnReplace.Size = new Size(110, 28);
        _btnReplace.TabIndex = 0;
        _btnReplace.Text = "Replace";
        _btnReplace.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

        _btnSkip.Location = new Point(128, 11);
        _btnSkip.Name = "btnSkip";
        _btnSkip.Size = new Size(110, 28);
        _btnSkip.TabIndex = 1;
        _btnSkip.Text = "Skip";
        _btnSkip.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

        _btnKeepBoth.Location = new Point(244, 11);
        _btnKeepBoth.Name = "btnKeepBoth";
        _btnKeepBoth.Size = new Size(110, 28);
        _btnKeepBoth.TabIndex = 2;
        _btnKeepBoth.Text = "Keep both";
        _btnKeepBoth.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

        _btnCancel.Location = new Point(398, 11);
        _btnCancel.Name = "btnCancel";
        _btnCancel.Size = new Size(110, 28);
        _btnCancel.TabIndex = 3;
        _btnCancel.Text = "Cancel";
        _btnCancel.DialogResult = DialogResult.Cancel;
        _btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

        _borderButtons.BorderStyle = PaletteBorderStyle.HeaderPrimary;
        _borderButtons.Dock = DockStyle.Top;
        _borderButtons.Name = "borderButtons";
        _borderButtons.Size = new Size(520, 1);
        _borderButtons.Text = "";

        _pnlButtons.Controls.Add(_btnCancel);
        _pnlButtons.Controls.Add(_btnKeepBoth);
        _pnlButtons.Controls.Add(_btnSkip);
        _pnlButtons.Controls.Add(_btnReplace);
        _pnlButtons.Controls.Add(_borderButtons);
        _pnlButtons.Dock = DockStyle.Bottom;
        _pnlButtons.Location = new Point(0, 218);
        _pnlButtons.Name = "pnlButtons";
        _pnlButtons.PanelBackStyle = PaletteBackStyle.PanelAlternate;
        _pnlButtons.Size = new Size(520, 50);
        _pnlButtons.TabIndex = 2;

        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(520, 284);
        Controls.Add(_tlpContent);
        Controls.Add(_pnlButtons);
        Controls.Add(_pnlBanner);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "VisualFileConflictForm";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Replace or skip files";
        CancelButton = _btnCancel;
        ((System.ComponentModel.ISupportInitialize)_picBanner).EndInit();
        _tlpBanner.ResumeLayout(false);
        _tlpBanner.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)_pnlBanner).EndInit();
        _pnlBanner.ResumeLayout(false);
        _tlpContent.ResumeLayout(false);
        _tlpContent.PerformLayout();
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
    private KryptonPanel _pnlButtons;
    private KryptonBorderEdge _borderButtons;
    private KryptonWrapLabel _lblIntro;
    private KryptonLabel _lblSource;
    private KryptonLabel _lblDestination;
    private KryptonCheckBox _chkApply;
    private KryptonButton _btnReplace;
    private KryptonButton _btnSkip;
    private KryptonButton _btnKeepBoth;
    private KryptonButton _btnCancel;
}

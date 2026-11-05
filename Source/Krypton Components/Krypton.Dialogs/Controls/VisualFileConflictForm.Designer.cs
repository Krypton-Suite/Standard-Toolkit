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
        _picBanner = new PictureBox();
        _lblBanner = new KryptonLabel();
        _borderBanner = new KryptonBorderEdge();
        _pnlContent = new KryptonPanel();
        _pnlButtons = new KryptonPanel();
        _borderButtons = new KryptonBorderEdge();
        _lblIntro = new KryptonLabel();
        _lblSource = new KryptonLabel();
        _lblDestination = new KryptonLabel();
        _chkApply = new KryptonCheckBox();
        _btnReplace = new KryptonButton();
        _btnSkip = new KryptonButton();
        _btnKeepBoth = new KryptonButton();
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
        _lblBanner.Size = new Size(452, 32);
        _lblBanner.TabIndex = 1;
        _lblBanner.StateCommon.ShortText.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
        _lblBanner.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        _borderBanner.BorderStyle = PaletteBorderStyle.HeaderPrimary;
        _borderBanner.Dock = DockStyle.Bottom;
        _borderBanner.Location = new Point(0, 55);
        _borderBanner.Name = "borderBanner";
        _borderBanner.Size = new Size(520, 1);
        _borderBanner.Text = "";

        _pnlBanner.Controls.Add(_lblBanner);
        _pnlBanner.Controls.Add(_picBanner);
        _pnlBanner.Controls.Add(_borderBanner);
        _pnlBanner.Dock = DockStyle.Top;
        _pnlBanner.Location = new Point(0, 0);
        _pnlBanner.Name = "pnlBanner";
        _pnlBanner.Size = new Size(520, 56);
        _pnlBanner.TabIndex = 0;

        _lblIntro.AutoSize = false;
        _lblIntro.Location = new Point(12, 12);
        _lblIntro.Name = "lblIntro";
        _lblIntro.Size = new Size(496, 40);
        _lblIntro.TabIndex = 0;
        _lblIntro.Visible = false;
        _lblIntro.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        _lblSource.AutoSize = false;
        _lblSource.Location = new Point(12, 12);
        _lblSource.Name = "lblSource";
        _lblSource.Size = new Size(496, 36);
        _lblSource.TabIndex = 1;
        _lblSource.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        _lblDestination.AutoSize = false;
        _lblDestination.Location = new Point(12, 52);
        _lblDestination.Name = "lblDestination";
        _lblDestination.Size = new Size(496, 36);
        _lblDestination.TabIndex = 2;
        _lblDestination.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        _chkApply.Location = new Point(12, 96);
        _chkApply.Name = "chkApply";
        _chkApply.Size = new Size(496, 24);
        _chkApply.TabIndex = 3;
        _chkApply.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        _pnlContent.Controls.Add(_chkApply);
        _pnlContent.Controls.Add(_lblDestination);
        _pnlContent.Controls.Add(_lblSource);
        _pnlContent.Controls.Add(_lblIntro);
        _pnlContent.Dock = DockStyle.Fill;
        _pnlContent.Location = new Point(0, 56);
        _pnlContent.Name = "pnlContent";
        _pnlContent.Size = new Size(520, 134);
        _pnlContent.TabIndex = 1;

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
        _borderButtons.Location = new Point(0, 0);
        _borderButtons.Name = "borderButtons";
        _borderButtons.Size = new Size(520, 1);
        _borderButtons.Text = "";

        _pnlButtons.Controls.Add(_btnCancel);
        _pnlButtons.Controls.Add(_btnKeepBoth);
        _pnlButtons.Controls.Add(_btnSkip);
        _pnlButtons.Controls.Add(_btnReplace);
        _pnlButtons.Controls.Add(_borderButtons);
        _pnlButtons.Dock = DockStyle.Bottom;
        _pnlButtons.Location = new Point(0, 190);
        _pnlButtons.Name = "pnlButtons";
        _pnlButtons.PanelBackStyle = PaletteBackStyle.PanelAlternate;
        _pnlButtons.Size = new Size(520, 50);
        _pnlButtons.TabIndex = 2;

        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(520, 240);
        Controls.Add(_pnlContent);
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
    private KryptonLabel _lblIntro;
    private KryptonLabel _lblSource;
    private KryptonLabel _lblDestination;
    private KryptonCheckBox _chkApply;
    private KryptonButton _btnReplace;
    private KryptonButton _btnSkip;
    private KryptonButton _btnKeepBoth;
    private KryptonButton _btnCancel;
}

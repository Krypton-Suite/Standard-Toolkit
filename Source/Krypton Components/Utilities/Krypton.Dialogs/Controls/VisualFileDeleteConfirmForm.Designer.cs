#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Dialogs;

internal partial class VisualFileDeleteConfirmForm
{
    /// <summary>Builds the confirmation layout.</summary>
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
        _lblMessage = new KryptonWrapLabel();
        _txtPaths = new KryptonTextBox();
        _btnYes = new KryptonButton();
        _btnNo = new KryptonButton();
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
        _borderBanner.Size = new Size(480, 1);
        _borderBanner.Text = "";

        _pnlBanner.Controls.Add(_tlpBanner);
        _pnlBanner.Controls.Add(_borderBanner);
        _pnlBanner.Dock = DockStyle.Top;
        _pnlBanner.Location = new Point(0, 0);
        _pnlBanner.Name = "pnlBanner";
        _pnlBanner.Size = new Size(480, FileOperationBannerLayout.DefaultHeight);
        _pnlBanner.TabIndex = 0;

        _lblMessage.AutoSize = true;
        _lblMessage.Dock = DockStyle.Fill;
        _lblMessage.Margin = new Padding(0, 0, 0, 8);
        _lblMessage.Name = "lblMessage";
        _lblMessage.TabIndex = 0;
        _lblMessage.Visible = false;

        _txtPaths.Dock = DockStyle.Fill;
        _txtPaths.Margin = Padding.Empty;
        _txtPaths.Name = "txtPaths";
        _txtPaths.TabIndex = 1;
        _txtPaths.Multiline = true;
        _txtPaths.ReadOnly = true;
        _txtPaths.ScrollBars = ScrollBars.Vertical;

        _tlpContent.ColumnCount = 1;
        _tlpContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
        _tlpContent.Controls.Add(_lblMessage, 0, 0);
        _tlpContent.Controls.Add(_txtPaths, 0, 1);
        _tlpContent.Dock = DockStyle.Fill;
        _tlpContent.Location = new Point(0, FileOperationBannerLayout.DefaultHeight);
        _tlpContent.Name = "tlpContent";
        _tlpContent.Padding = new Padding(16);
        _tlpContent.RowCount = 2;
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 0f));
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
        _tlpContent.Size = new Size(480, 186);
        _tlpContent.TabIndex = 1;

        _btnYes.Location = new Point(272, 11);
        _btnYes.Name = "btnYes";
        _btnYes.Size = new Size(90, 28);
        _btnYes.TabIndex = 0;
        _btnYes.Text = "Yes";
        _btnYes.DialogResult = DialogResult.Yes;
        _btnYes.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

        _btnNo.Location = new Point(378, 11);
        _btnNo.Name = "btnNo";
        _btnNo.Size = new Size(90, 28);
        _btnNo.TabIndex = 1;
        _btnNo.Text = "No";
        _btnNo.DialogResult = DialogResult.No;
        _btnNo.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

        _borderButtons.BorderStyle = PaletteBorderStyle.HeaderPrimary;
        _borderButtons.Dock = DockStyle.Top;
        _borderButtons.Name = "borderButtons";
        _borderButtons.Size = new Size(480, 1);
        _borderButtons.Text = "";

        _pnlButtons.Controls.Add(_btnNo);
        _pnlButtons.Controls.Add(_btnYes);
        _pnlButtons.Controls.Add(_borderButtons);
        _pnlButtons.Dock = DockStyle.Bottom;
        _pnlButtons.Location = new Point(0, 270);
        _pnlButtons.Name = "pnlButtons";
        _pnlButtons.PanelBackStyle = PaletteBackStyle.PanelAlternate;
        _pnlButtons.Size = new Size(480, 50);
        _pnlButtons.TabIndex = 2;

        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(480, 336);
        Controls.Add(_tlpContent);
        Controls.Add(_pnlButtons);
        Controls.Add(_pnlBanner);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "VisualFileDeleteConfirmForm";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        AcceptButton = _btnYes;
        CancelButton = _btnNo;
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
        PerformLayout();
    }

    private KryptonPanel _pnlBanner;
    private KryptonTableLayoutPanel _tlpBanner;
    private PictureBox _picBanner;
    private KryptonWrapLabel _lblBanner;
    private KryptonBorderEdge _borderBanner;
    private KryptonTableLayoutPanel _tlpContent;
    private KryptonPanel _pnlButtons;
    private KryptonBorderEdge _borderButtons;
    private KryptonWrapLabel _lblMessage;
    private KryptonTextBox _txtPaths;
    private KryptonButton _btnYes;
    private KryptonButton _btnNo;
}

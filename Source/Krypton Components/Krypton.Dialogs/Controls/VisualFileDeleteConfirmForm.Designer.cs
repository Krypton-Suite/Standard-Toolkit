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
        _picBanner = new PictureBox();
        _lblBanner = new KryptonLabel();
        _borderBanner = new KryptonBorderEdge();
        _pnlContent = new KryptonPanel();
        _pnlButtons = new KryptonPanel();
        _borderButtons = new KryptonBorderEdge();
        _lblMessage = new KryptonLabel();
        _txtPaths = new KryptonTextBox();
        _btnYes = new KryptonButton();
        _btnNo = new KryptonButton();
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
        _lblBanner.Location = new Point(56, 8);
        _lblBanner.Name = "lblBanner";
        _lblBanner.Size = new Size(412, 40);
        _lblBanner.TabIndex = 1;
        _lblBanner.StateCommon.ShortText.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
        _lblBanner.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        _borderBanner.BorderStyle = PaletteBorderStyle.HeaderPrimary;
        _borderBanner.Dock = DockStyle.Bottom;
        _borderBanner.Location = new Point(0, 55);
        _borderBanner.Name = "borderBanner";
        _borderBanner.Size = new Size(480, 1);
        _borderBanner.Text = "";

        _pnlBanner.Controls.Add(_lblBanner);
        _pnlBanner.Controls.Add(_picBanner);
        _pnlBanner.Controls.Add(_borderBanner);
        _pnlBanner.Dock = DockStyle.Top;
        _pnlBanner.Location = new Point(0, 0);
        _pnlBanner.Name = "pnlBanner";
        _pnlBanner.Size = new Size(480, 56);
        _pnlBanner.TabIndex = 0;

        _lblMessage.AutoSize = false;
        _lblMessage.Location = new Point(12, 12);
        _lblMessage.Name = "lblMessage";
        _lblMessage.Size = new Size(456, 48);
        _lblMessage.TabIndex = 0;
        _lblMessage.Visible = false;
        _lblMessage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        _txtPaths.Location = new Point(12, 12);
        _txtPaths.Name = "txtPaths";
        _txtPaths.Size = new Size(456, 162);
        _txtPaths.TabIndex = 1;
        _txtPaths.Multiline = true;
        _txtPaths.ReadOnly = true;
        _txtPaths.ScrollBars = ScrollBars.Vertical;
        _txtPaths.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

        _pnlContent.Controls.Add(_txtPaths);
        _pnlContent.Controls.Add(_lblMessage);
        _pnlContent.Dock = DockStyle.Fill;
        _pnlContent.Location = new Point(0, 56);
        _pnlContent.Name = "pnlContent";
        _pnlContent.Size = new Size(480, 186);
        _pnlContent.TabIndex = 1;

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
        _borderButtons.Location = new Point(0, 0);
        _borderButtons.Name = "borderButtons";
        _borderButtons.Size = new Size(480, 1);
        _borderButtons.Text = "";

        _pnlButtons.Controls.Add(_btnNo);
        _pnlButtons.Controls.Add(_btnYes);
        _pnlButtons.Controls.Add(_borderButtons);
        _pnlButtons.Dock = DockStyle.Bottom;
        _pnlButtons.Location = new Point(0, 242);
        _pnlButtons.Name = "pnlButtons";
        _pnlButtons.PanelBackStyle = PaletteBackStyle.PanelAlternate;
        _pnlButtons.Size = new Size(480, 50);
        _pnlButtons.TabIndex = 2;

        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(480, 292);
        Controls.Add(_pnlContent);
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
        ((System.ComponentModel.ISupportInitialize)_pnlBanner).EndInit();
        _pnlBanner.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_pnlContent).EndInit();
        _pnlContent.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_pnlButtons).EndInit();
        _pnlButtons.ResumeLayout(false);
        _pnlButtons.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private KryptonPanel _pnlBanner;
    private PictureBox _picBanner;
    private KryptonLabel _lblBanner;
    private KryptonBorderEdge _borderBanner;
    private KryptonPanel _pnlContent;
    private KryptonPanel _pnlButtons;
    private KryptonBorderEdge _borderButtons;
    private KryptonLabel _lblMessage;
    private KryptonTextBox _txtPaths;
    private KryptonButton _btnYes;
    private KryptonButton _btnNo;
}

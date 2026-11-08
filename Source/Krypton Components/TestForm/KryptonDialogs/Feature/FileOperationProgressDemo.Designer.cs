#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

partial class FileOperationProgressDemo
{
    private void InitializeComponent()
    {
        _lblInstructions = new KryptonWrapLabel();
        _lblSandbox = new KryptonLabel();
        _theme = new KryptonThemeComboBox();
        _btnReset = new KryptonButton();
        _btnCopy = new KryptonButton();
        _btnMove = new KryptonButton();
        _btnRename = new KryptonButton();
        _btnRecycle = new KryptonButton();
        _btnPermanent = new KryptonButton();
        _btnConflict = new KryptonButton();
        _btnLarge = new KryptonButton();
        _log = new KryptonTextBox();
        ((System.ComponentModel.ISupportInitialize)_theme).BeginInit();
        SuspendLayout();

        _lblInstructions.AutoSize = true;
        _lblInstructions.MaximumSize = new Size(736, 0);
        _lblInstructions.Location = new Point(12, 12);
        _lblInstructions.Name = "lblInstructions";
        _lblInstructions.TabIndex = 0;
        _lblInstructions.Text = "Krypton file-operation dialogs (#4672). Reset the sandbox, then copy, move, rename, recycle, or permanently delete. Name conflict asks Replace, Skip, or Keep both. Large copy keeps the progress dialog open so you can Cancel. Use More details to replace the progress bar with the theme-aware speed graph. Pause appears when the shell allows it. These calls do not open Explorer's copy window.";
        _lblInstructions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        _lblSandbox.AutoSize = false;
        _lblSandbox.Location = new Point(12, 116);
        _lblSandbox.Name = "lblSandbox";
        _lblSandbox.Size = new Size(520, 24);
        _lblSandbox.TabIndex = 1;
        _lblSandbox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        _theme.DropDownStyle = ComboBoxStyle.DropDownList;
        _theme.DropDownWidth = 220;
        _theme.Location = new Point(538, 114);
        _theme.Name = "theme";
        _theme.Size = new Size(210, 26);
        _theme.TabIndex = 2;
        _theme.Anchor = AnchorStyles.Top | AnchorStyles.Right;

        _btnReset.Location = new Point(12, 148);
        _btnReset.Name = "btnReset";
        _btnReset.Size = new Size(120, 28);
        _btnReset.TabIndex = 3;
        _btnReset.Text = "Reset sandbox";

        _btnCopy.Location = new Point(138, 148);
        _btnCopy.Name = "btnCopy";
        _btnCopy.Size = new Size(110, 28);
        _btnCopy.TabIndex = 4;
        _btnCopy.Text = "Copy";

        _btnMove.Location = new Point(254, 148);
        _btnMove.Name = "btnMove";
        _btnMove.Size = new Size(110, 28);
        _btnMove.TabIndex = 5;
        _btnMove.Text = "Move";

        _btnRename.Location = new Point(370, 148);
        _btnRename.Name = "btnRename";
        _btnRename.Size = new Size(110, 28);
        _btnRename.TabIndex = 6;
        _btnRename.Text = "Rename";

        _btnRecycle.Location = new Point(12, 182);
        _btnRecycle.Name = "btnRecycle";
        _btnRecycle.Size = new Size(140, 28);
        _btnRecycle.TabIndex = 7;
        _btnRecycle.Text = "Recycle";

        _btnPermanent.Location = new Point(158, 182);
        _btnPermanent.Name = "btnPermanent";
        _btnPermanent.Size = new Size(150, 28);
        _btnPermanent.TabIndex = 8;
        _btnPermanent.Text = "Permanent delete";

        _btnConflict.Location = new Point(314, 182);
        _btnConflict.Name = "btnConflict";
        _btnConflict.Size = new Size(140, 28);
        _btnConflict.TabIndex = 9;
        _btnConflict.Text = "Name conflict";

        _btnLarge.Location = new Point(460, 182);
        _btnLarge.Name = "btnLarge";
        _btnLarge.Size = new Size(140, 28);
        _btnLarge.TabIndex = 10;
        _btnLarge.Text = "Large copy";

        _log.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _log.Location = new Point(12, 222);
        _log.Multiline = true;
        _log.Name = "log";
        _log.ReadOnly = true;
        _log.ScrollBars = ScrollBars.Vertical;
        _log.Size = new Size(736, 280);
        _log.TabIndex = 11;

        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(760, 520);
        Controls.Add(_log);
        Controls.Add(_btnLarge);
        Controls.Add(_btnConflict);
        Controls.Add(_btnPermanent);
        Controls.Add(_btnRecycle);
        Controls.Add(_btnRename);
        Controls.Add(_btnMove);
        Controls.Add(_btnCopy);
        Controls.Add(_btnReset);
        Controls.Add(_theme);
        Controls.Add(_lblSandbox);
        Controls.Add(_lblInstructions);
        Name = "FileOperationProgressDemo";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "File operation progress dialogs (#4672)";
        ((System.ComponentModel.ISupportInitialize)_theme).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private KryptonWrapLabel _lblInstructions;
    private KryptonLabel _lblSandbox;
    private KryptonThemeComboBox _theme;
    private KryptonButton _btnReset;
    private KryptonButton _btnCopy;
    private KryptonButton _btnMove;
    private KryptonButton _btnRename;
    private KryptonButton _btnRecycle;
    private KryptonButton _btnPermanent;
    private KryptonButton _btnConflict;
    private KryptonButton _btnLarge;
    private KryptonTextBox _log;
}

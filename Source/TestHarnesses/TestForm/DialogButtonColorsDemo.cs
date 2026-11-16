#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Demo for Issue #4165: optional semantic (accept / cancel) dialog button colours
/// with named accessibility presets and per-role overrides.
/// </summary>
public sealed class DialogButtonColorsDemo : KryptonForm
{
    private readonly KryptonComboBox _cmbScheme;
    private readonly KryptonCheckBox _chkUseManagerDefault;
    private readonly KryptonCheckBox _chkCustomOverrides;
    private readonly KryptonColorButton _btnAcceptColor;
    private readonly KryptonColorButton _btnCancelColor;

    public DialogButtonColorsDemo()
    {
        Text = @"4165 — Dialog Button Semantic Colours";
        Size = new Size(720, 480);
        StartPosition = FormStartPosition.CenterScreen;

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 96,
            Padding = new Padding(12),
            Text =
                "Issue #4165: optional red/green (and colour-blind-safe) accept/cancel colours on dialog buttons.\r\n" +
                "Pick a scheme, optionally override Accept/Cancel fills, then open MessageBox, TaskDialog, Extended, or Foldable.\r\n" +
                "Default remains themed chrome when scheme is None and manager default is unset. Text labels stay the primary cue."
        };

        var options = new KryptonPanel { Dock = DockStyle.Top, Height = 88, Padding = new Padding(12, 8, 12, 8) };
        var optionsLayout = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = true };

        optionsLayout.Controls.Add(new KryptonLabel { Text = @"Scheme:", AutoSize = true, Padding = new Padding(0, 6, 0, 0) });
        _cmbScheme = new KryptonComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 140 };
        _cmbScheme.Items.AddRange(new object[]
        {
            KryptonDialogButtonColorScheme.None,
            KryptonDialogButtonColorScheme.Standard,
            KryptonDialogButtonColorScheme.Deuteranopia,
            KryptonDialogButtonColorScheme.Protanopia,
            KryptonDialogButtonColorScheme.HighContrast,
            KryptonDialogButtonColorScheme.Custom
        });
        _cmbScheme.SelectedItem = KryptonDialogButtonColorScheme.Standard;
        optionsLayout.Controls.Add(_cmbScheme);

        _chkUseManagerDefault = new KryptonCheckBox
        {
            Text = @"Also set KryptonManager.DialogButtonColors",
            Padding = new Padding(16, 4, 0, 0)
        };
        optionsLayout.Controls.Add(_chkUseManagerDefault);

        _chkCustomOverrides = new KryptonCheckBox
        {
            Text = @"Apply custom Accept/Cancel fills",
            Padding = new Padding(16, 4, 0, 0)
        };
        optionsLayout.Controls.Add(_chkCustomOverrides);

        optionsLayout.Controls.Add(new KryptonLabel { Text = @"Accept:", AutoSize = true, Padding = new Padding(8, 6, 0, 0) });
        _btnAcceptColor = new KryptonColorButton { SelectedColor = Color.FromArgb(52, 199, 89), Width = 100 };
        optionsLayout.Controls.Add(_btnAcceptColor);

        optionsLayout.Controls.Add(new KryptonLabel { Text = @"Cancel:", AutoSize = true, Padding = new Padding(8, 6, 0, 0) });
        _btnCancelColor = new KryptonColorButton { SelectedColor = Color.FromArgb(255, 59, 48), Width = 100 };
        optionsLayout.Controls.Add(_btnCancelColor);

        options.Controls.Add(optionsLayout);

        var buttons = new KryptonPanel { Dock = DockStyle.Fill, Padding = new Padding(12) };
        var grid = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 3
        };
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        for (var i = 0; i < 3; i++)
        {
            grid.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
        }

        grid.Controls.Add(CreateActionButton("KryptonMessageBox (Yes/No/Cancel + Help)", (_, _) => ShowMessageBox()), 0, 0);
        grid.Controls.Add(CreateActionButton("KryptonTaskDialog (OK/Cancel)", (_, _) => ShowTaskDialog()), 1, 0);
        grid.Controls.Add(CreateActionButton("KryptonMessageBoxExtended", (_, _) => ShowExtended()), 0, 1);
        grid.Controls.Add(CreateActionButton("KryptonFoldableDialog", (_, _) => ShowFoldable()), 1, 1);
        grid.Controls.Add(CreateActionButton("Clear manager default", (_, _) =>
        {
            KryptonManager.DialogButtonColors = null;
            KryptonMessageBox.Show(this, @"KryptonManager.DialogButtonColors cleared.", @"4165",
                KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
        }), 0, 2);
        grid.Controls.Add(CreateActionButton("MessageBox with no call-site options", (_, _) =>
        {
            SyncManagerDefault();
            KryptonMessageBox.Show(this,
                @"Uses only KryptonManager.DialogButtonColors (if set).",
                @"4165 Manager fallback",
                KryptonMessageBoxButtons.OKCancel,
                KryptonMessageBoxIcon.Question);
        }), 1, 2);
        buttons.Controls.Add(grid);

        Controls.Add(buttons);
        Controls.Add(options);
        Controls.Add(instructions);
    }

    private KryptonDialogButtonColorOptions? BuildOptions()
    {
        var scheme = _cmbScheme.SelectedItem is KryptonDialogButtonColorScheme selected
            ? selected
            : KryptonDialogButtonColorScheme.None;

        if (scheme == KryptonDialogButtonColorScheme.None && !_chkCustomOverrides.Checked)
        {
            return null;
        }

        var options = new KryptonDialogButtonColorOptions { Scheme = scheme };
        if (_chkCustomOverrides.Checked)
        {
            options.AcceptBackColor = _btnAcceptColor.SelectedColor;
            options.AcceptBorderColor = _btnAcceptColor.SelectedColor;
            options.AcceptTextColor = Color.White;
            options.CancelBackColor = _btnCancelColor.SelectedColor;
            options.CancelBorderColor = _btnCancelColor.SelectedColor;
            options.CancelTextColor = Color.White;
            if (scheme == KryptonDialogButtonColorScheme.None)
            {
                options.Scheme = KryptonDialogButtonColorScheme.Custom;
            }
        }

        return options;
    }

    private void SyncManagerDefault()
    {
        KryptonManager.DialogButtonColors = _chkUseManagerDefault.Checked ? BuildOptions() : null;
    }

    private void ShowMessageBox()
    {
        SyncManagerDefault();
        var options = BuildOptions();
        KryptonMessageBox.Show(this,
            @"Confirm this action? Accept should be green-family; Cancel/No red-family; Help blue-family.",
            @"4165 MessageBox",
            KryptonMessageBoxButtons.YesNoCancel,
            KryptonMessageBoxIcon.Question,
            options,
            displayHelpButton: true);
    }

    private void ShowExtended()
    {
        SyncManagerDefault();
        var data = new KryptonMessageBoxExtendedData
        {
            Owner = this,
            Caption = @"4165 Extended",
            MessageText = @"Extended message box with semantic button colours (including Help).",
            Buttons = ExtendedMessageBoxButtons.YesNoCancel,
            Icon = ExtendedKryptonMessageBoxIcon.Question,
            ShowHelpButton = true,
            ButtonColors = BuildOptions()
        };
        KryptonMessageBoxExtended.Show(data);
    }

    private void ShowTaskDialog()
    {
        SyncManagerDefault();
        using var taskDialog = new KryptonTaskDialog(520);
        taskDialog.Dialog.Form.Text = @"4165 TaskDialog";
        taskDialog.Heading.Text = @"Save changes before closing?";
        taskDialog.Heading.Visible = true;
        taskDialog.Heading.IconType = KryptonTaskDialogIconType.ShieldHelp;
        taskDialog.FooterBar.CommonButtons.Buttons =
            KryptonTaskDialogCommonButtonTypes.OK | KryptonTaskDialogCommonButtonTypes.Cancel;
        taskDialog.FooterBar.CommonButtons.AcceptButton = KryptonTaskDialogCommonButtonTypes.OK;
        taskDialog.FooterBar.CommonButtons.CancelButton = KryptonTaskDialogCommonButtonTypes.Cancel;
        taskDialog.FooterBar.CommonButtons.ButtonColors = BuildOptions();
        taskDialog.ShowDialog(this);
    }

    private void ShowFoldable()
    {
        SyncManagerDefault();
        var data = new KryptonFoldableDialogData
        {
            Owner = this,
            Caption = @"4165 Foldable",
            Heading = @"Semantic button colours",
            Text = @"Foldable dialog action buttons use the same colour options.",
            DetailsText = @"Accept / Cancel roles are mapped from DialogResult; Help uses KryptonDialogButtonRole.Help.",
            Buttons = KryptonMessageBoxButtons.OKCancel,
            Icon = ExtendedKryptonMessageBoxIcon.Information,
            ButtonColors = BuildOptions()
        };
        KryptonFoldableDialog.Show(data);
    }

    private static KryptonButton CreateActionButton(string text, EventHandler onClick)
    {
        var button = new KryptonButton
        {
            Dock = DockStyle.Fill,
            Margin = new Padding(6),
            Values = { Text = text }
        };
        button.Click += onClick;
        return button;
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        // Do not leave a demo-only manager default for other TestForm scenarios.
        KryptonManager.DialogButtonColors = null;
        base.OnFormClosed(e);
    }
}
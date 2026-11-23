#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace TestForm;

/// <summary>
/// Comprehensive demo for the optional 'Copy' button on <see cref="KryptonMessageBox"/> (Issue #3836).
/// Lets the user configure buttons, icon, default button, RTL and the various opt-in flags, then shows
/// the message box and previews what the Copy button (or Ctrl+C) placed on the clipboard.
/// </summary>
public partial class MessageBoxCopyButtonDemo : KryptonForm
{
    private const string SAMPLE_MESSAGE = "The operation completed with additional information.\n\n" +
                                          "Tip: click the 'Copy' button (or press Ctrl+C) to copy this\n" +
                                          "message, its caption and the button captions to the clipboard.";

    public MessageBoxCopyButtonDemo()
    {
        InitializeComponent();

        PopulateCombos();

        ktxtCaption.Text = @"Copy button demo";
        ktxtMessage.Text = SAMPLE_MESSAGE;

        kbtnShow.Click += kbtnShow_Click;
        kbtnPresetError.Click += kbtnPresetError_Click;
        kbtnPresetConfirm.Click += kbtnPresetConfirm_Click;
        kbtnPresetRtl.Click += kbtnPresetRtl_Click;
        kbtnRefreshClipboard.Click += kbtnRefreshClipboard_Click;
    }

    private void PopulateCombos()
    {
        foreach (KryptonMessageBoxButtons value in Enum.GetValues(typeof(KryptonMessageBoxButtons)))
        {
            kcmbButtons.Items.Add(value);
        }

        kcmbButtons.SelectedItem = KryptonMessageBoxButtons.OKCancel;

        kcmbIcon.Items.AddRange(new object[]
        {
            KryptonMessageBoxIcon.None,
            KryptonMessageBoxIcon.Information,
            KryptonMessageBoxIcon.Question,
            KryptonMessageBoxIcon.Warning,
            KryptonMessageBoxIcon.Error,
            KryptonMessageBoxIcon.Exclamation,
            KryptonMessageBoxIcon.Asterisk,
            KryptonMessageBoxIcon.Hand,
            KryptonMessageBoxIcon.Stop,
            KryptonMessageBoxIcon.Shield,
            KryptonMessageBoxIcon.WindowsLogo,
            KryptonMessageBoxIcon.Application
        });

        kcmbIcon.SelectedItem = KryptonMessageBoxIcon.Error;

        kcmbDefaultButton.Items.AddRange(new object[]
        {
            KryptonMessageBoxDefaultButton.Button1,
            KryptonMessageBoxDefaultButton.Button2,
            KryptonMessageBoxDefaultButton.Button3,
            KryptonMessageBoxDefaultButton.Button4
        });

        kcmbDefaultButton.SelectedItem = KryptonMessageBoxDefaultButton.Button1;
    }

    private void kbtnShow_Click(object? sender, EventArgs e)
    {
        KryptonMessageBoxButtons buttons = kcmbButtons.SelectedItem is KryptonMessageBoxButtons selectedButtons
            ? selectedButtons
            : KryptonMessageBoxButtons.OK;

        KryptonMessageBoxIcon icon = kcmbIcon.SelectedItem is KryptonMessageBoxIcon selectedIcon
            ? selectedIcon
            : KryptonMessageBoxIcon.None;

        KryptonMessageBoxDefaultButton defaultButton = kcmbDefaultButton.SelectedItem is KryptonMessageBoxDefaultButton selectedDefault
            ? selectedDefault
            : KryptonMessageBoxDefaultButton.Button1;

        MessageBoxOptions options = kchkRightToLeft.Checked ? MessageBoxOptions.RtlReading : (MessageBoxOptions)0;

        DialogResult result = KryptonMessageBox.Show(this,
            ktxtMessage.Text,
            ktxtCaption.Text,
            buttons,
            kchkShowHelpButton.Checked,
            icon,
            defaultButton,
            options,
            showCtrlCopy: kchkShowCtrlCopy.Checked,
            showCloseButton: kchkShowCloseButton.Checked,
            showCopyButton: kchkShowCopyButton.Checked);

        klblResult.Text = $@"Last result: {result}";

        RefreshClipboardPreview();
    }

    private void kbtnPresetError_Click(object? sender, EventArgs e)
    {
        KryptonMessageBox.Show(this,
            "An unexpected error occurred while saving the file.\n\n" +
            "Error code: 0x80070005 (Access is denied)\n" +
            "Path: C:\\Reports\\2026\\Q3-summary.xlsx",
            @"Save failed",
            KryptonMessageBoxButtons.OK,
            KryptonMessageBoxIcon.Error,
            showCopyButton: true);

        klblResult.Text = @"Last result: OK (Error + Copy preset)";

        RefreshClipboardPreview();
    }

    private void kbtnPresetConfirm_Click(object? sender, EventArgs e)
    {
        DialogResult result = KryptonMessageBox.Show(this,
            "You have unsaved changes.\n\nWould you like to save them before closing?",
            @"Confirm",
            KryptonMessageBoxButtons.YesNoCancel,
            true,
            KryptonMessageBoxIcon.Question,
            KryptonMessageBoxDefaultButton.Button1,
            (MessageBoxOptions)0,
            showCopyButton: true);

        klblResult.Text = $@"Last result: {result} (Yes/No/Cancel + Copy + Help preset)";

        RefreshClipboardPreview();
    }

    private void kbtnPresetRtl_Click(object? sender, EventArgs e)
    {
        DialogResult result = KryptonMessageBox.Show(this,
            "This message box is displayed using right-to-left reading order.\n\n" +
            "The Copy button works here too.",
            @"RTL demo",
            KryptonMessageBoxButtons.OKCancel,
            KryptonMessageBoxIcon.Information,
            KryptonMessageBoxDefaultButton.Button1,
            MessageBoxOptions.RtlReading,
            showCopyButton: true);

        klblResult.Text = $@"Last result: {result} (RTL + Copy preset)";

        RefreshClipboardPreview();
    }

    private void kbtnRefreshClipboard_Click(object? sender, EventArgs e) => RefreshClipboardPreview();

    private void RefreshClipboardPreview()
    {
        try
        {
            krtbClipboardPreview.Text = Clipboard.ContainsText()
                ? Clipboard.GetText()
                : @"(the clipboard does not currently contain text)";
        }
        catch (System.Runtime.InteropServices.ExternalException)
        {
            krtbClipboardPreview.Text = @"(unable to read the clipboard)";
        }
    }
}

#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved. 
 *  
 */
#endregion

using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Comprehensive demo for the optional 'Copy' button on <see cref="KryptonMessageBoxExtended"/> (Issue #3836 parity).
/// Lets the user configure buttons, icon, default button, the message container type (Normal / RichTextBox /
/// HyperLink), RTL and the various opt-in flags, then shows the extended message box and previews what the Copy
/// button (or Ctrl+C) placed on the clipboard. Also demonstrates the <see cref="KryptonMessageBoxExtendedData"/>
/// struct-based path via <see cref="VisualMessageBoxExtendedForm"/>.
/// </summary>
public partial class MessageBoxExtendedCopyButtonDemo : KryptonForm
{
    private const string SAMPLE_MESSAGE = "The extended operation completed with additional information.\n\n" +
                                          "Tip: click the 'Copy' button (or press Ctrl+C) to copy this\n" +
                                          "message, its caption and the button captions to the clipboard.";

    private const string LINK_URL = @"https://github.com/Krypton-Suite/Standard-Toolkit/issues/3836";

    public MessageBoxExtendedCopyButtonDemo()
    {
        InitializeComponent();

        PopulateCombos();

        ktxtCaption.Text = @"Extended Copy button demo";
        ktxtMessage.Text = SAMPLE_MESSAGE;

        kbtnShow.Click += kbtnShow_Click;
        kbtnPresetError.Click += kbtnPresetError_Click;
        kbtnPresetRichText.Click += kbtnPresetRichText_Click;
        kbtnPresetHyperlink.Click += kbtnPresetHyperlink_Click;
        kbtnPresetStruct.Click += kbtnPresetStruct_Click;
        kbtnRefreshClipboard.Click += kbtnRefreshClipboard_Click;
    }

    private void PopulateCombos()
    {
        kcmbButtons.Items.AddRange(new object[]
        {
            ExtendedMessageBoxButtons.OK,
            ExtendedMessageBoxButtons.OKCancel,
            ExtendedMessageBoxButtons.YesNo,
            ExtendedMessageBoxButtons.YesNoCancel,
            ExtendedMessageBoxButtons.RetryCancel,
            ExtendedMessageBoxButtons.AbortRetryIgnore
        });

        kcmbButtons.SelectedItem = ExtendedMessageBoxButtons.OKCancel;

        kcmbIcon.Items.AddRange(new object[]
        {
            ExtendedKryptonMessageBoxIcon.None,
            ExtendedKryptonMessageBoxIcon.Information,
            ExtendedKryptonMessageBoxIcon.Question,
            ExtendedKryptonMessageBoxIcon.Warning,
            ExtendedKryptonMessageBoxIcon.Error,
            ExtendedKryptonMessageBoxIcon.Exclamation,
            ExtendedKryptonMessageBoxIcon.Asterisk,
            ExtendedKryptonMessageBoxIcon.Hand,
            ExtendedKryptonMessageBoxIcon.Stop,
            ExtendedKryptonMessageBoxIcon.Shield,
            ExtendedKryptonMessageBoxIcon.WindowsLogo
        });

        kcmbIcon.SelectedItem = ExtendedKryptonMessageBoxIcon.Error;

        kcmbDefaultButton.Items.AddRange(new object[]
        {
            KryptonMessageBoxDefaultButton.Button1,
            KryptonMessageBoxDefaultButton.Button2,
            KryptonMessageBoxDefaultButton.Button3,
            KryptonMessageBoxDefaultButton.Button4
        });

        kcmbDefaultButton.SelectedItem = KryptonMessageBoxDefaultButton.Button1;

        kcmbContainerType.Items.AddRange(new object[]
        {
            ExtendedKryptonMessageBoxMessageContainerType.Normal,
            ExtendedKryptonMessageBoxMessageContainerType.RichTextBox,
            ExtendedKryptonMessageBoxMessageContainerType.HyperLink
        });

        kcmbContainerType.SelectedItem = ExtendedKryptonMessageBoxMessageContainerType.Normal;
    }

    private void kbtnShow_Click(object? sender, EventArgs e)
    {
        ExtendedMessageBoxButtons buttons = kcmbButtons.SelectedItem is ExtendedMessageBoxButtons selectedButtons
            ? selectedButtons
            : ExtendedMessageBoxButtons.OK;

        ExtendedKryptonMessageBoxIcon icon = kcmbIcon.SelectedItem is ExtendedKryptonMessageBoxIcon selectedIcon
            ? selectedIcon
            : ExtendedKryptonMessageBoxIcon.None;

        KryptonMessageBoxDefaultButton defaultButton = kcmbDefaultButton.SelectedItem is KryptonMessageBoxDefaultButton selectedDefault
            ? selectedDefault
            : KryptonMessageBoxDefaultButton.Button1;

        ExtendedKryptonMessageBoxMessageContainerType containerType = kcmbContainerType.SelectedItem is ExtendedKryptonMessageBoxMessageContainerType selectedContainer
            ? selectedContainer
            : ExtendedKryptonMessageBoxMessageContainerType.Normal;

        MessageBoxOptions options = kchkRightToLeft.Checked ? MessageBoxOptions.RtlReading : (MessageBoxOptions)0;

        // A hyperlink container needs a link area (and optionally something to launch when clicked).
        LinkArea? contentLinkArea = containerType == ExtendedKryptonMessageBoxMessageContainerType.HyperLink
            ? new LinkArea(0, ktxtMessage.Text.Length)
            : (LinkArea?)null;

        System.Diagnostics.ProcessStartInfo? linkLaunchArgument = containerType == ExtendedKryptonMessageBoxMessageContainerType.HyperLink
            ? new System.Diagnostics.ProcessStartInfo(LINK_URL) { UseShellExecute = true }
            : null;

        DialogResult result = KryptonMessageBoxExtended.Show(
            messageText: ktxtMessage.Text,
            caption: ktxtCaption.Text,
            buttons: buttons,
            icon: icon,
            defaultButton: defaultButton,
            options: options,
            displayHelpButton: kchkShowHelpButton.Checked,
            showCtrlCopy: kchkShowCtrlCopy.Checked,
            messageContainerType: containerType,
            contentLinkArea: contentLinkArea,
            linkLaunchArgument: linkLaunchArgument,
            showCloseButton: kchkShowCloseButton.Checked,
            showCopyButton: kchkShowCopyButton.Checked);

        klblResult.Text = $@"Last result: {result}";

        RefreshClipboardPreview();
    }

    private void kbtnPresetError_Click(object? sender, EventArgs e)
    {
        KryptonMessageBoxExtended.Show(
            messageText: "An unexpected error occurred while saving the file.\n\n" +
                         "Error code: 0x80070005 (Access is denied)\n" +
                         "Path: C:\\Reports\\2026\\Q3-summary.xlsx",
            caption: @"Save failed",
            buttons: ExtendedMessageBoxButtons.OK,
            icon: ExtendedKryptonMessageBoxIcon.Error,
            defaultButton: KryptonMessageBoxDefaultButton.Button1,
            options: (MessageBoxOptions)0,
            displayHelpButton: false,
            showCtrlCopy: true,
            showCloseButton: true,
            showCopyButton: true);

        klblResult.Text = @"Last result: OK (Error + Copy preset)";

        RefreshClipboardPreview();
    }

    private void kbtnPresetRichText_Click(object? sender, EventArgs e)
    {
        DialogResult result = KryptonMessageBoxExtended.Show(
            messageText: "Release notes\n\n" +
                         "- Added an optional Copy button.\n" +
                         "- Ctrl+C now copies RichTextBox content.\n" +
                         "- Works in right-to-left layouts.",
            caption: @"RichTextBox content",
            buttons: ExtendedMessageBoxButtons.OKCancel,
            icon: ExtendedKryptonMessageBoxIcon.Information,
            defaultButton: KryptonMessageBoxDefaultButton.Button1,
            options: (MessageBoxOptions)0,
            displayHelpButton: false,
            showCtrlCopy: true,
            messageContainerType: ExtendedKryptonMessageBoxMessageContainerType.RichTextBox,
            showCloseButton: true,
            showCopyButton: true);

        klblResult.Text = $@"Last result: {result} (RichTextBox + Copy preset)";

        RefreshClipboardPreview();
    }

    private void kbtnPresetHyperlink_Click(object? sender, EventArgs e)
    {
        const string message = "Open GitHub issue #3836 for more details.";

        DialogResult result = KryptonMessageBoxExtended.Show(
            messageText: message,
            caption: @"Hyperlink content",
            buttons: ExtendedMessageBoxButtons.OKCancel,
            icon: ExtendedKryptonMessageBoxIcon.Information,
            defaultButton: KryptonMessageBoxDefaultButton.Button1,
            options: (MessageBoxOptions)0,
            displayHelpButton: false,
            showCtrlCopy: true,
            messageContainerType: ExtendedKryptonMessageBoxMessageContainerType.HyperLink,
            contentLinkArea: new LinkArea(0, message.Length),
            linkLaunchArgument: new System.Diagnostics.ProcessStartInfo(LINK_URL) { UseShellExecute = true },
            showCloseButton: true,
            showCopyButton: true);

        klblResult.Text = $@"Last result: {result} (HyperLink + Copy preset)";

        RefreshClipboardPreview();
    }

    private void kbtnPresetStruct_Click(object? sender, EventArgs e)
    {
        // Demonstrates the KryptonMessageBoxExtendedData struct-based path, including the new ShowCopyButton flag.
        KryptonMessageBoxExtendedData data = new KryptonMessageBoxExtendedData
        {
            Owner = this,
            Caption = @"Struct-based path",
            MessageText = "This message box was created from a KryptonMessageBoxExtendedData struct.\n\n" +
                          "The ShowCopyButton flag on the struct enables the Copy button here too.",
            Buttons = ExtendedMessageBoxButtons.OKCancel,
            Icon = ExtendedKryptonMessageBoxIcon.Information,
            DefaultButton = KryptonMessageBoxDefaultButton.Button1,
            MessageContentAreaType = ExtendedKryptonMessageBoxMessageContainerType.Normal,
            ShowCtrlCopy = true,
            ShowCopyButton = true
        };

        using (VisualMessageBoxExtendedForm form = new VisualMessageBoxExtendedForm(data, showCloseButton: true))
        {
            DialogResult result = form.ShowDialog(this);

            klblResult.Text = $@"Last result: {result} (Struct-based path + Copy)";
        }

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

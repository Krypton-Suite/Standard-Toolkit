#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Demonstrates the optional expandable ("foldable") footer of <see cref="KryptonMessageBoxExtended"/>: a
/// collapsible details region (Text, CheckBox, or RichTextBox) with a Show/Hide details toggle that mirrors
/// the <see cref="KryptonFoldableDialog"/> expander.
/// </summary>
public partial class MessageBoxExtendedFoldableDemo : KryptonForm
{
    public MessageBoxExtendedFoldableDemo()
    {
        InitializeComponent();

        PopulateCombos();

        LoadSampleContent();
    }

    private void PopulateCombos()
    {
        kcmbIcon.Items.Clear();
        kcmbIcon.Items.AddRange(Enum.GetNames(typeof(ExtendedKryptonMessageBoxIcon)));
        kcmbIcon.SelectedItem = nameof(ExtendedKryptonMessageBoxIcon.Information);

        kcmbButtons.Items.Clear();
        kcmbButtons.Items.AddRange(Enum.GetNames(typeof(ExtendedMessageBoxButtons)));
        kcmbButtons.SelectedItem = nameof(ExtendedMessageBoxButtons.OKCancel);

        kcmbContentType.Items.Clear();
        kcmbContentType.Items.AddRange(Enum.GetNames(typeof(ExtendedKryptonMessageBoxFooterContentType)));
        kcmbContentType.SelectedItem = nameof(ExtendedKryptonMessageBoxFooterContentType.RichTextBox);
    }

    private void LoadSampleContent()
    {
        ktxtCaption.Text = @"Krypton Message Box Extended";
        ktxtMessage.Text = @"An unhandled exception has occurred in your application. If you click Continue, the application will ignore this error and attempt to continue.";
        ktxtFooter.Text = SampleDetails();
    }

    private static string SampleDetails() =>
        "System.InvalidOperationException: Sequence contains no elements\r\n" +
        "   at System.Linq.Enumerable.First[TSource](IEnumerable`1 source)\r\n" +
        "   at MyApp.Services.OrderService.GetLatest()\r\n" +
        "   at MyApp.UI.MainForm.OnLoad(EventArgs e)\r\n" +
        "   at System.Windows.Forms.Form.OnLoad(EventArgs e)";

    private static T ParseEnum<T>(object? value, T fallback) where T : struct =>
        Enum.TryParse(value?.ToString(), out T parsed) ? parsed : fallback;

    private void kbtnShow_Click(object sender, EventArgs e)
    {
        ExtendedKryptonMessageBoxFooterContentType contentType =
            ParseEnum(kcmbContentType.SelectedItem, ExtendedKryptonMessageBoxFooterContentType.Text);

        // The RichTextBox height only applies to the RichTextBox content type.
        int? footerRichTextBoxHeight = contentType == ExtendedKryptonMessageBoxFooterContentType.RichTextBox
            ? (int)knudRtbHeight.Value
            : null;

        DialogResult result = KryptonMessageBoxExtended.Show(
            this,
            ktxtMessage.Text,
            ktxtCaption.Text,
            ParseEnum(kcmbButtons.SelectedItem, ExtendedMessageBoxButtons.OKCancel),
            ParseEnum(kcmbIcon.SelectedItem, ExtendedKryptonMessageBoxIcon.Information),
            showCloseButton: true,
            footerText: ktxtFooter.Text,
            footerExpanded: kchkExpanded.Checked,
            footerContentType: contentType,
            footerRichTextBoxHeight: footerRichTextBoxHeight);

        klblResult.Text = $@"Last result: {result}";
    }

    private void kbtnJitPreset_Click(object sender, EventArgs e)
    {
        // Exercises the data-driven path: the "more details" footer is configured from
        // KryptonMessageBoxExtendedData, with a custom MoreDetailsButtonText toggle caption.
        var data = new KryptonMessageBoxExtendedData
        {
            Owner = this,
            Caption = @"Visual Studio Just-In-Time Debugger",
            MessageText = @"An exception 'System.InvalidOperationException' has occurred in MyApp.exe. Expand the details below to inspect the full stack trace.",
            Buttons = ExtendedMessageBoxButtons.YesNo,
            Icon = ExtendedKryptonMessageBoxIcon.Error,
            ShowMoreDetailsOption = true,
            MoreDetailsExpanded = kchkExpanded.Checked,
            MoreDetailsButtonText = @"Stack trace",
            MoreDetailsMessageText = SampleDetails()
        };

        DialogResult result = KryptonMessageBoxExtended.Show(data);

        klblResult.Text = $@"Last result: {result}";
    }
}

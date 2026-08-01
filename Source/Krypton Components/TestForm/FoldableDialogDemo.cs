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
/// Demonstrates <see cref="KryptonFoldableDialog"/> (Issue #3840): a message-box style dialog with a
/// collapsible ("foldable") details region, modelled on the Visual Studio Just-In-Time debugger dialog.
/// </summary>
public partial class FoldableDialogDemo : KryptonForm
{
    public FoldableDialogDemo()
    {
        InitializeComponent();

        PopulateCombos();

        LoadSampleContent();
    }

    private void PopulateCombos()
    {
        kcmbIcon.Items.Clear();
        kcmbIcon.Items.AddRange(Enum.GetNames(typeof(ExtendedKryptonMessageBoxIcon)));
        kcmbIcon.SelectedItem = nameof(ExtendedKryptonMessageBoxIcon.Error);

        kcmbButtons.Items.Clear();
        kcmbButtons.Items.AddRange(Enum.GetNames(typeof(KryptonMessageBoxButtons)));
        kcmbButtons.SelectedItem = nameof(KryptonMessageBoxButtons.OKCancel);

        kcmbDefault.Items.Clear();
        kcmbDefault.Items.AddRange(Enum.GetNames(typeof(KryptonMessageBoxDefaultButton)));
        kcmbDefault.SelectedItem = nameof(KryptonMessageBoxDefaultButton.Button1);

        kcmbStartPosition.Items.Clear();
        kcmbStartPosition.Items.AddRange(Enum.GetNames(typeof(FormStartPosition)));
        kcmbStartPosition.SelectedItem = nameof(FormStartPosition.CenterScreen);
    }

    private void LoadSampleContent()
    {
        ktxtCaption.Text = @"Krypton Foldable Dialog";
        ktxtHeading.Text = @"An unhandled exception has occurred in your application.";
        ktxtMessage.Text = @"If you click Continue, the application will ignore this error and attempt to continue. If you click Quit, the application will close immediately.";
        ktxtDetails.Text = SampleDetails();
    }

    private static string SampleDetails() =>
        "System.InvalidOperationException: Sequence contains no elements\r\n" +
        "   at System.Linq.Enumerable.First[TSource](IEnumerable`1 source)\r\n" +
        "   at MyApp.Services.OrderService.GetLatest()\r\n" +
        "   at MyApp.UI.MainForm.OnLoad(EventArgs e)\r\n" +
        "   at System.Windows.Forms.Form.OnLoad(EventArgs e)";

    private static T ParseEnum<T>(object? value, T fallback) where T : struct =>
        Enum.TryParse(value?.ToString(), out T parsed) ? parsed : fallback;

    private KryptonFoldableDialogData BuildData() => new KryptonFoldableDialogData
    {
        Owner = this,
        Caption = ktxtCaption.Text,
        Heading = ktxtHeading.Text,
        Text = ktxtMessage.Text,
        DetailsText = ktxtDetails.Text,
        Icon = ParseEnum(kcmbIcon.SelectedItem, ExtendedKryptonMessageBoxIcon.Error),
        Buttons = ParseEnum(kcmbButtons.SelectedItem, KryptonMessageBoxButtons.OKCancel),
        DefaultButton = ParseEnum(kcmbDefault.SelectedItem, KryptonMessageBoxDefaultButton.Button1),
        StartPosition = ParseEnum(kcmbStartPosition.SelectedItem, FormStartPosition.CenterScreen),
        Expanded = kchkExpanded.Checked
    };

    private void kbtnShow_Click(object sender, EventArgs e)
    {
        DialogResult result = KryptonFoldableDialog.Show(BuildData());

        klblResult.Text = $@"Last result: {result}";
    }

    private void kbtnJitPreset_Click(object sender, EventArgs e)
    {
        var data = new KryptonFoldableDialogData
        {
            Owner = this,
            Caption = @"Visual Studio Just-In-Time Debugger",
            Heading = @"An exception 'System.InvalidOperationException' has occurred in MyApp.exe.",
            Text = @"Do you want to debug using the selected debugger? Expand the details below to inspect the full stack trace.",
            DetailsText = SampleDetails(),
            Icon = ExtendedKryptonMessageBoxIcon.Error,
            Buttons = KryptonMessageBoxButtons.YesNo,
            DefaultButton = KryptonMessageBoxDefaultButton.Button2,
            Expanded = true
        };

        DialogResult result = KryptonFoldableDialog.Show(data);

        klblResult.Text = $@"Last result: {result}";
    }
}

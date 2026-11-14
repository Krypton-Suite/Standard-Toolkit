#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Comprehensive demonstration of <see cref="KryptonFolderBrowserDialog"/> properties, presets, and usage patterns.
/// </summary>
public partial class KryptonFolderBrowserDialogDemo : KryptonForm
{
    public KryptonFolderBrowserDialogDemo()
    {
        InitializeComponent();
        InitializeDemo();
    }

    private void InitializeDemo()
    {
        kcmbRootFolder.DataSource = Enum.GetValues(typeof(Environment.SpecialFolder));
        kcmbRootFolder.SelectedItem = Environment.SpecialFolder.Desktop;

#if !NET8_0_OR_GREATER
        klblInitialDirectory.Visible = false;
        ktxtInitialDirectory.Visible = false;
        klblInitialDirectoryNote.Visible = false;
#endif

        ApplyPresetDefault();
    }

    private void ApplySettingsToDialog(KryptonFolderBrowserDialog dialog)
    {
        dialog.Title = ktxtTitle.Text;
        dialog.SelectedPath = ktxtSelectedPath.Text;
        dialog.RootFolder = (Environment.SpecialFolder)kcmbRootFolder.SelectedItem!;
#if NET8_0_OR_GREATER
        dialog.InitialDirectory = ktxtInitialDirectory.Text;
#endif
        dialog.Icon = kcbUseCustomIcon.Checked ? Icon : null;
    }

    private DialogResult ShowConfiguredDialog(bool useFormComponent)
    {
        if (useFormComponent)
        {
            ApplySettingsToDialog(kryptonFolderBrowserDialog1);
            return kcbShowWithOwner.Checked
                ? kryptonFolderBrowserDialog1.ShowDialog(this)
                : kryptonFolderBrowserDialog1.ShowDialog();
        }

        using var dialog = new KryptonFolderBrowserDialog();
        ApplySettingsToDialog(dialog);
        var result = kcbShowWithOwner.Checked ? dialog.ShowDialog(this) : dialog.ShowDialog();
        if (result == DialogResult.OK)
        {
            ktxtSelectedPath.Text = dialog.SelectedPath;
        }

        return result;
    }

    private void UpdateResultDisplay(DialogResult result, string? selectedPath, string dialogDescription)
    {
        ktxtDialogResult.Text = result.ToString();
        ktxtResultSelectedPath.Text = selectedPath ?? string.Empty;
        ktxtResultSummary.Text = result == DialogResult.OK
            ? $"{dialogDescription}: selected '{selectedPath}'."
            : $"{dialogDescription}: cancelled.";
    }

    private void ApplyPresetDefault()
    {
        ktxtTitle.Text = @"Select a folder";
        ktxtSelectedPath.Text = string.Empty;
        ktxtInitialDirectory.Text = string.Empty;
        kcmbRootFolder.SelectedItem = Environment.SpecialFolder.Desktop;
        kcbUseCustomIcon.Checked = false;
        kcbUseFormComponent.Checked = false;
        kcbShowWithOwner.Checked = true;
        ClearResultDisplay();
    }

    private void ClearResultDisplay()
    {
        ktxtDialogResult.Clear();
        ktxtResultSelectedPath.Clear();
        ktxtResultSummary.Text = @"Run a scenario to see the dialog result here.";
    }

    private void kbtnShowKrypton_Click(object sender, EventArgs e)
    {
        var result = ShowConfiguredDialog(kcbUseFormComponent.Checked);
        var selectedPath = result == DialogResult.OK
            ? (kcbUseFormComponent.Checked ? kryptonFolderBrowserDialog1.SelectedPath : ktxtSelectedPath.Text)
            : null;
        UpdateResultDisplay(result, selectedPath, @"Krypton folder browser");
    }

    private void kbtnShowStandard_Click(object sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = ktxtTitle.Text,
            SelectedPath = string.IsNullOrWhiteSpace(ktxtSelectedPath.Text) ? ktxtInitialDirectory.Text : ktxtSelectedPath.Text,
            RootFolder = (Environment.SpecialFolder)kcmbRootFolder.SelectedItem!
        };

#if NET8_0_OR_GREATER
        dialog.InitialDirectory = ktxtInitialDirectory.Text;
#endif

        var result = kcbShowWithOwner.Checked ? dialog.ShowDialog(this) : dialog.ShowDialog();
        if (result == DialogResult.OK)
        {
            ktxtSelectedPath.Text = dialog.SelectedPath;
        }

        UpdateResultDisplay(result, result == DialogResult.OK ? dialog.SelectedPath : null, @"Standard folder browser");
    }

    private void kbtnReset_Click(object sender, EventArgs e)
    {
        kryptonFolderBrowserDialog1.Reset();
        ApplyPresetDefault();
    }

    private void kbtnPresetDefault_Click(object sender, EventArgs e) => ApplyPresetDefault();

    private void kbtnPresetMyDocuments_Click(object sender, EventArgs e)
    {
        ktxtTitle.Text = @"Choose a documents folder";
        ktxtSelectedPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        ktxtInitialDirectory.Text = ktxtSelectedPath.Text;
        kcmbRootFolder.SelectedItem = Environment.SpecialFolder.MyDocuments;
        kcbUseCustomIcon.Checked = false;
        ClearResultDisplay();
    }

    private void kbtnPresetDesktop_Click(object sender, EventArgs e)
    {
        ktxtTitle.Text = @"Browse from Desktop";
        ktxtSelectedPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        ktxtInitialDirectory.Text = ktxtSelectedPath.Text;
        kcmbRootFolder.SelectedItem = Environment.SpecialFolder.Desktop;
        kcbUseCustomIcon.Checked = false;
        ClearResultDisplay();
    }

    private void kbtnPresetTemp_Click(object sender, EventArgs e)
    {
        ktxtTitle.Text = @"Pick a working folder";
        ktxtSelectedPath.Text = System.IO.Path.GetTempPath();
        ktxtInitialDirectory.Text = ktxtSelectedPath.Text;
        kcmbRootFolder.SelectedItem = Environment.SpecialFolder.MyComputer;
        kcbUseCustomIcon.Checked = false;
        ClearResultDisplay();
    }

    private void kbtnPresetBranded_Click(object sender, EventArgs e)
    {
        ktxtTitle.Text = @"Krypton Toolkit - Folder Selection";
        ktxtSelectedPath.Text = string.Empty;
        ktxtInitialDirectory.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        kcmbRootFolder.SelectedItem = Environment.SpecialFolder.Desktop;
        kcbUseCustomIcon.Checked = true;
        ClearResultDisplay();
    }

    private void kbtnClose_Click(object sender, EventArgs e) => Close();
}

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Exercises the native and custom dialog providers side-by-side for open, save, and folder selection.
/// </summary>
public class KryptonFileDialogProviderDemo : KryptonForm
{
    private readonly KryptonTextBox _titleTextBox;
    private readonly KryptonTextBox _initialDirectoryTextBox;
    private readonly KryptonTextBox _fileNameTextBox;
    private readonly KryptonTextBox _filterTextBox;
    private readonly KryptonCheckBox _ownerCheckBox;
    private readonly KryptonTextBox _resultTextBox;

    public KryptonFileDialogProviderDemo()
    {
        Text = @"Krypton File Dialog Provider Demo";
        Size = new Size(980, 620);
        StartPosition = FormStartPosition.CenterScreen;

        var rootPanel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12)
        };
        Controls.Add(rootPanel);

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 4
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        rootPanel.Controls.Add(layout);

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            MaximumSize = new Size(920, 0),
            Text = @"Issue #1231 demo. Compare the existing native wrapper with the new custom provider for open, save, and folder selection. Try changing the initial directory, filter, and owner options, then verify that the custom dialogs stay responsive and return the expected paths."
        };
        layout.Controls.Add(instructions, 0, 0);

        _titleTextBox = new KryptonTextBox
        {
            Dock = DockStyle.Fill,
            Text = @"Krypton Provider Demo"
        };
        _initialDirectoryTextBox = new KryptonTextBox
        {
            Dock = DockStyle.Fill,
            Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        };
        _fileNameTextBox = new KryptonTextBox
        {
            Dock = DockStyle.Fill,
            Text = @"sample.txt"
        };
        _filterTextBox = new KryptonTextBox
        {
            Dock = DockStyle.Fill,
            Text = @"Text Files (*.txt)|*.txt|C# Files (*.cs)|*.cs|All Files (*.*)|*.*"
        };
        _ownerCheckBox = new KryptonCheckBox
        {
            Text = @"Show with owner",
            Checked = true,
            AutoSize = true
        };

        var settingsLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            AutoSize = true
        };
        settingsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        settingsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        AddLabeledRow(settingsLayout, 0, @"Title:", _titleTextBox);
        AddLabeledRow(settingsLayout, 1, @"Initial directory:", _initialDirectoryTextBox);
        AddLabeledRow(settingsLayout, 2, @"Initial file name:", _fileNameTextBox);
        AddLabeledRow(settingsLayout, 3, @"Filter:", _filterTextBox);
        settingsLayout.Controls.Add(_ownerCheckBox, 1, 4);
        layout.Controls.Add(settingsLayout, 0, 1);

        var buttonsLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 3,
            RowCount = 2,
            AutoSize = true
        };
        buttonsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        buttonsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        buttonsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        buttonsLayout.Controls.Add(CreateButton(@"Native Open", (_, _) => ShowOpenDialog(KryptonDialogProviderMode.Native)), 0, 0);
        buttonsLayout.Controls.Add(CreateButton(@"Custom Open", (_, _) => ShowOpenDialog(KryptonDialogProviderMode.Custom)), 1, 0);
        buttonsLayout.Controls.Add(CreateButton(@"Native Save", (_, _) => ShowSaveDialog(KryptonDialogProviderMode.Native)), 0, 1);
        buttonsLayout.Controls.Add(CreateButton(@"Custom Save", (_, _) => ShowSaveDialog(KryptonDialogProviderMode.Custom)), 1, 1);
        buttonsLayout.Controls.Add(CreateButton(@"Native Folder", (_, _) => ShowFolderDialog(KryptonDialogProviderMode.Native)), 2, 0);
        buttonsLayout.Controls.Add(CreateButton(@"Custom Folder", (_, _) => ShowFolderDialog(KryptonDialogProviderMode.Custom)), 2, 1);
        layout.Controls.Add(buttonsLayout, 0, 2);

        _resultTextBox = new KryptonTextBox
        {
            Dock = DockStyle.Fill,
            Multiline = true,
            ReadOnly = true,
            ScrollBars = ScrollBars.Both
        };
        layout.Controls.Add(_resultTextBox, 0, 3);
    }

    private static void AddLabeledRow(TableLayoutPanel layout, int rowIndex, string labelText, Control control)
    {
        while (layout.RowStyles.Count <= rowIndex)
        {
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        }

        layout.Controls.Add(new KryptonLabel
        {
            Text = labelText,
            Anchor = AnchorStyles.Left,
            AutoSize = true
        }, 0, rowIndex);
        layout.Controls.Add(control, 1, rowIndex);
    }

    private static KryptonButton CreateButton(string text, EventHandler onClick)
    {
        var button = new KryptonButton
        {
            Text = text,
            Dock = DockStyle.Fill
        };
        button.Click += onClick;
        return button;
    }

    private void ShowOpenDialog(KryptonDialogProviderMode providerMode)
    {
        using var dialog = new KryptonOpenFileDialog
        {
            ProviderMode = providerMode,
            Title = _titleTextBox.Text,
            InitialDirectory = _initialDirectoryTextBox.Text,
            FileName = _fileNameTextBox.Text,
            Filter = _filterTextBox.Text,
            FilterIndex = 1,
            CheckFileExists = true,
            CheckPathExists = true
        };

        var result = ShowDialog(dialog);
        WriteResult($@"{providerMode} open", result, dialog.FileName);
    }

    private void ShowSaveDialog(KryptonDialogProviderMode providerMode)
    {
        using var dialog = new KryptonSaveFileDialog
        {
            ProviderMode = providerMode,
            Title = _titleTextBox.Text,
            InitialDirectory = _initialDirectoryTextBox.Text,
            FileName = _fileNameTextBox.Text,
            Filter = _filterTextBox.Text,
            FilterIndex = 1,
            CheckPathExists = true,
            OverwritePrompt = true,
            AddExtension = true,
            DefaultExt = @"txt"
        };

        var result = ShowDialog(dialog);
        WriteResult($@"{providerMode} save", result, dialog.FileName);
    }

    private void ShowFolderDialog(KryptonDialogProviderMode providerMode)
    {
        using var dialog = new KryptonFolderBrowserDialog
        {
            ProviderMode = providerMode,
            Title = _titleTextBox.Text,
            SelectedPath = _initialDirectoryTextBox.Text,
            RootFolder = Environment.SpecialFolder.Desktop
        };

#if NET8_0_OR_GREATER
        dialog.InitialDirectory = _initialDirectoryTextBox.Text;
#endif

        var result = ShowDialog(dialog);
        WriteResult($@"{providerMode} folder", result, dialog.SelectedPath);
    }

    private DialogResult ShowDialog(ShellDialogWrapper dialog) => _ownerCheckBox.Checked
        ? dialog.ShowDialog(this)
        : dialog.ShowDialog();

    private void WriteResult(string scenario, DialogResult result, string selectedValue)
    {
        _resultTextBox.Text = string.Join(Environment.NewLine, new[]
        {
            $@"Scenario: {scenario}",
            $@"Result: {result}",
            $@"Value: {selectedValue}"
        });
    }
}

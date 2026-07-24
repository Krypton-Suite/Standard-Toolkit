#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Demonstrates the ToolStrip-hosted controls in <c>Krypton.Toolkit.Utilities</c>: <see cref="KryptonToolStripTextBox"/>,
/// <see cref="KryptonToolStripBrowseBox"/> (open/save/folder-picker/reset), <see cref="KryptonDateTimePickerToolStripItem"/>,
/// <see cref="KryptonNumericUpDownToolStripItem"/>, <see cref="KryptonColorButtonToolStripMenuItem"/>,
/// <see cref="KryptonTrackBarToolStripMenuItem"/>, and <see cref="KryptonThemeToolStripComboBox"/>, all hosted on a
/// single <see cref="KryptonBasicToolStrip"/> with a live-value readout below.
/// </summary>
public class ToolStripHostedItemsDemo : KryptonForm
{
    private readonly KryptonToolStripTextBox _textBoxItem;
    private readonly KryptonToolStripBrowseBox _browseBoxItem;
    private readonly KryptonDateTimePickerToolStripItem _dateTimeItem;
    private readonly KryptonNumericUpDownToolStripItem _numericItem;
    private readonly KryptonColorButtonToolStripMenuItem _colourItem;
    private readonly KryptonTrackBarToolStripMenuItem _trackBarItem;
    private readonly KryptonThemeToolStripComboBox _themeCombo;

    private readonly KryptonCheckBox _chkUseSaveDialog;
    private readonly KryptonCheckBox _chkIsFolderPicker;
    private readonly KryptonCheckBox _chkShowResetButton;
    private readonly KryptonButton _btnRepopulateThemes;
    private readonly KryptonLabel _lblStatus;

    public ToolStripHostedItemsDemo()
    {
        Text = @"ToolStrip Hosted Items Demo";
        Size = new Size(900, 600);
        StartPosition = FormStartPosition.CenterParent;

        var strip = new KryptonBasicToolStrip { Dock = DockStyle.Top };

        _textBoxItem = new KryptonToolStripTextBox { ToolTipText = @"KryptonToolStripTextBox" };
        _textBoxItem.KryptonTextBox!.CueHint.CueHintText = @"Type here...";

        _browseBoxItem = new KryptonToolStripBrowseBox { AutoSize = false, Size = new Size(180, 24) };
        _browseBoxItem.KryptonBrowseBox!.FileDialogFilter = @"Text files (*.txt)|*.txt|All files (*.*)|*.*";
        _browseBoxItem.KryptonBrowseBox!.ShowResetButton = true;

        _dateTimeItem = new KryptonDateTimePickerToolStripItem { Size = new Size(160, 24) };
        _dateTimeItem.Value = DateTime.Today;

        _numericItem = new KryptonNumericUpDownToolStripItem();
        _numericItem.KryptonNumericUpDownControl!.Minimum = 0;
        _numericItem.KryptonNumericUpDownControl!.Maximum = 1000;
        _numericItem.Value = 42;

        _colourItem = new KryptonColorButtonToolStripMenuItem { AutoSize = false, Size = new Size(60, 24) };
        _colourItem.SelectedColor = Color.SteelBlue;

        _trackBarItem = new KryptonTrackBarToolStripMenuItem { Size = new Size(150, 24) };
        _trackBarItem.Minimum = 0;
        _trackBarItem.Maximum = 100;
        _trackBarItem.Value = 25;
        _trackBarItem.Orientation = Orientation.Horizontal;

        _themeCombo = new KryptonThemeToolStripComboBox();

        strip.Items.Add(new ToolStripLabel(@"Text:"));
        strip.Items.Add(_textBoxItem);
        strip.Items.Add(new ToolStripSeparator());
        strip.Items.Add(new ToolStripLabel(@"Browse:"));
        strip.Items.Add(_browseBoxItem);
        strip.Items.Add(new ToolStripSeparator());
        strip.Items.Add(new ToolStripLabel(@"Date:"));
        strip.Items.Add(_dateTimeItem);
        strip.Items.Add(new ToolStripSeparator());
        strip.Items.Add(new ToolStripLabel(@"Number:"));
        strip.Items.Add(_numericItem);
        strip.Items.Add(new ToolStripSeparator());
        strip.Items.Add(new ToolStripLabel(@"Colour:"));
        strip.Items.Add(_colourItem);
        strip.Items.Add(new ToolStripSeparator());
        strip.Items.Add(new ToolStripLabel(@"Volume:"));
        strip.Items.Add(_trackBarItem);
        strip.Items.Add(new ToolStripSeparator());
        strip.Items.Add(_themeCombo);

        Controls.Add(strip);

        // ----- Main content: instructions + browse-box options + live readout -----
        var mainPanel = new KryptonPanel { Dock = DockStyle.Fill, Padding = new Padding(12) };

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 64,
            Text = @"Every item above is hosted on a single KryptonBasicToolStrip. Type in the text box, browse " +
                   @"or reset via the '...' / reset glyphs (toggle Save/Folder mode below), pick a date, change the " +
                   @"number, colour and slider, and switch themes. The status line reflects every change live."
        };
        mainPanel.Controls.Add(instructions);

        var options = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Top = 70 };
        _chkUseSaveDialog = new KryptonCheckBox { Values = { Text = @"BrowseBox.UseSaveDialog" } };
        _chkIsFolderPicker = new KryptonCheckBox { Values = { Text = @"BrowseBox.IsFolderPicker" } };
        _chkShowResetButton = new KryptonCheckBox { Checked = true, Values = { Text = @"BrowseBox.ShowResetButton" } };
        _btnRepopulateThemes = new KryptonButton { Values = { Text = @"ThemeCombo.PopulateThemes()" } };
        options.Controls.Add(_chkUseSaveDialog);
        options.Controls.Add(_chkIsFolderPicker);
        options.Controls.Add(_chkShowResetButton);
        options.Controls.Add(_btnRepopulateThemes);
        mainPanel.Controls.Add(options);

        _lblStatus = new KryptonLabel { Dock = DockStyle.Bottom, Values = { Text = @"Ready." } };
        mainPanel.Controls.Add(_lblStatus);

        Controls.Add(mainPanel);

        WireEvents();
    }

    private void WireEvents()
    {
        _textBoxItem.TextChanged += (_, _) => UpdateStatus($@"TextBox.Text = ""{_textBoxItem.Text}""");

        _browseBoxItem.KryptonBrowseBox!.TextChanged += (_, _) => UpdateStatus($@"BrowseBox.Text = ""{_browseBoxItem.KryptonBrowseBox.Text}""");

        _dateTimeItem.ValueChanged += (_, _) => UpdateStatus($@"DateTimePicker.Value = {_dateTimeItem.Value:yyyy-MM-dd}");

        _numericItem.KryptonNumericUpDownControl!.ValueChanged += (_, _) => UpdateStatus($@"NumericUpDown.Value = {_numericItem.Value}");

        _colourItem.SelectedColorChanged += (_, _) => UpdateStatus($@"ColourButton.SelectedColor = {_colourItem.SelectedColor}");

        _trackBarItem.ValueChanged += (_, _) => UpdateStatus($@"TrackBar.Value = {_trackBarItem.Value}");

        _themeCombo.SelectedIndexChanged += (_, _) => UpdateStatus($@"Theme = {_themeCombo.Text}");

        _chkUseSaveDialog.CheckedChanged += (_, _) =>
        {
            _browseBoxItem.KryptonBrowseBox!.UseSaveDialog = _chkUseSaveDialog.Checked;
            UpdateStatus($@"BrowseBox.UseSaveDialog = {_chkUseSaveDialog.Checked}");
        };

        _chkIsFolderPicker.CheckedChanged += (_, _) =>
        {
            _browseBoxItem.KryptonBrowseBox!.IsFolderPicker = _chkIsFolderPicker.Checked;
            UpdateStatus($@"BrowseBox.IsFolderPicker = {_chkIsFolderPicker.Checked}");
        };

        _chkShowResetButton.CheckedChanged += (_, _) =>
        {
            _browseBoxItem.KryptonBrowseBox!.ShowResetButton = _chkShowResetButton.Checked;
            UpdateStatus($@"BrowseBox.ShowResetButton = {_chkShowResetButton.Checked}");
        };

        _btnRepopulateThemes.Click += (_, _) =>
        {
            _themeCombo.PopulateThemes();
            UpdateStatus(@"ThemeCombo.PopulateThemes() called.");
        };
    }

    private void UpdateStatus(string message) => _lblStatus.Values.Text = message;
}

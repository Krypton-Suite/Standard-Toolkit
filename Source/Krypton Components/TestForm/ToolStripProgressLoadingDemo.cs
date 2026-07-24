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
/// Demonstrates the progress and loading-indicator ToolStrip items in <c>Krypton.Toolkit.Utilities</c>:
/// <see cref="KryptonEnhancedToolStripProgressBar"/> (<see cref="KryptonEnhancedToolStripProgressBar.UseKryptonRender"/>),
/// <see cref="KryptonToolStripProgressBarWithValueText"/> (<see cref="KryptonToolStripProgressBarWithValueText.DisplayValue"/>),
/// a standalone <see cref="KryptonLoadingCircle"/> with selectable <see cref="KryptonLoadingCircle.StylePreset"/>,
/// <see cref="KryptonLoadingCircleToolStripMenuItem"/>, and <see cref="KryptonProgressStatusStrip"/> used as a progress bar.
/// A single Animate action drives every progress indicator from 0 to 100 together.
/// </summary>
public class ToolStripProgressLoadingDemo : KryptonForm
{
    private readonly KryptonEnhancedToolStripProgressBar _enhancedProgressBar;
    private readonly KryptonToolStripProgressBarWithValueText _valueTextProgressBar;
    private readonly KryptonProgressStatusStrip _progressStatusStrip;
    private readonly KryptonLoadingCircle _standaloneLoadingCircle;
    private readonly KryptonLoadingCircleToolStripMenuItem _menuLoadingCircleItem;

    private readonly KryptonCheckBox _chkUseKryptonRender;
    private readonly KryptonCheckBox _chkDisplayValue;
    private readonly KryptonCheckBox _chkUseAsProgressBar;
    private readonly KryptonCheckBox _chkLoadingActive;
    private readonly KryptonComboBox _cmbStylePreset;
    private readonly KryptonButton _btnAnimate;
    private readonly KryptonButton _btnReset;
    private readonly KryptonLabel _lblStatus;

    private readonly System.Windows.Forms.Timer _animationTimer;

    public ToolStripProgressLoadingDemo()
    {
        Text = @"ToolStrip Progress & Loading Demo";
        Size = new Size(900, 600);
        StartPosition = FormStartPosition.CenterParent;

        _animationTimer = new System.Windows.Forms.Timer { Interval = 40 };
        _animationTimer.Tick += OnAnimationTick;

        // ----- Top: native ToolStrip hosting the two enhanced progress bars -----
        var strip = new KryptonBasicToolStrip { Dock = DockStyle.Top };

        _enhancedProgressBar = new KryptonEnhancedToolStripProgressBar
        {
            Minimum = 0,
            Maximum = 100,
            Value = 0,
            UseKryptonRender = true
        };

        _valueTextProgressBar = new KryptonToolStripProgressBarWithValueText
        {
            Minimum = 0,
            Maximum = 100,
            Value = 0,
            DisplayValue = true
        };

        _menuLoadingCircleItem = new KryptonLoadingCircleToolStripMenuItem();
        _menuLoadingCircleItem.LoadingCircleControl!.StylePreset = KryptonLoadingCircle.StylePresets.Firefox;

        strip.Items.Add(new ToolStripLabel(@"EnhancedProgressBar:"));
        strip.Items.Add(_enhancedProgressBar);
        strip.Items.Add(new ToolStripSeparator());
        strip.Items.Add(new ToolStripLabel(@"ProgressBarWithValueText:"));
        strip.Items.Add(_valueTextProgressBar);
        strip.Items.Add(new ToolStripSeparator());
        strip.Items.Add(new ToolStripLabel(@"LoadingCircleToolStripMenuItem:"));
        strip.Items.Add(_menuLoadingCircleItem);

        Controls.Add(strip);

        // ----- Bottom: KryptonProgressStatusStrip used as a progress bar -----
        _progressStatusStrip = new KryptonProgressStatusStrip
        {
            Dock = DockStyle.Bottom,
            MinimumValue = 0f,
            MaximumValue = 100f,
            CurrentValue = 0f,
            UseAsProgressBar = true,
            BarColour = Color.SteelBlue,
            BarShade = Color.LightSteelBlue
        };
        _progressStatusStrip.Items.Add(new ToolStripStatusLabel(@"KryptonProgressStatusStrip (UseAsProgressBar):"));
        Controls.Add(_progressStatusStrip);

        // ----- Main content -----
        var mainPanel = new KryptonPanel { Dock = DockStyle.Fill, Padding = new Padding(12) };

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 72,
            Text = @"Click Animate to drive every progress indicator from 0 to 100 together: the two ToolStrip " +
                   @"progress bars above, the KryptonProgressStatusStrip painted bar below, and the standalone " +
                   @"KryptonLoadingCircle spinner (which simply spins while Active, independent of the percentage)."
        };
        mainPanel.Controls.Add(instructions);

        var options = new TableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Top = 76, ColumnCount = 4 };
        options.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190F));
        options.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        options.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190F));
        options.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

        _chkUseKryptonRender = new KryptonCheckBox { Checked = true, Values = { Text = @"EnhancedProgressBar.UseKryptonRender" } };
        _chkDisplayValue = new KryptonCheckBox { Checked = true, Values = { Text = @"ProgressBarWithValueText.DisplayValue" } };
        _chkUseAsProgressBar = new KryptonCheckBox { Checked = true, Values = { Text = @"ProgressStatusStrip.UseAsProgressBar" } };
        _chkLoadingActive = new KryptonCheckBox { Values = { Text = @"LoadingCircle(s).Active" } };

        _cmbStylePreset = new KryptonComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        _cmbStylePreset.Items.AddRange(new object[] { @"MacOSX", @"Firefox", @"IE7", @"Custom" });
        _cmbStylePreset.SelectedIndex = 0;

        int row = 0;
        AddRow(options, ref row, @"UseKryptonRender:", _chkUseKryptonRender, @"DisplayValue:", _chkDisplayValue);
        AddRow(options, ref row, @"UseAsProgressBar:", _chkUseAsProgressBar, @"LoadingCircle Active:", _chkLoadingActive);
        AddRow(options, ref row, @"LoadingCircle.StylePreset:", _cmbStylePreset, string.Empty, null);
        mainPanel.Controls.Add(options);

        var loadingCircleHost = new KryptonGroupBox
        {
            Dock = DockStyle.Top,
            Top = 220,
            Height = 110,
            Values = { Heading = @"Standalone KryptonLoadingCircle" }
        };
        _standaloneLoadingCircle = new KryptonLoadingCircle
        {
            Location = new Point(20, 20),
            Size = new Size(64, 64),
            StylePreset = KryptonLoadingCircle.StylePresets.MacOSX,
            Color = Color.SteelBlue
        };
        loadingCircleHost.Panel.Controls.Add(_standaloneLoadingCircle);
        mainPanel.Controls.Add(loadingCircleHost);

        var buttonRow = new FlowLayoutPanel { Dock = DockStyle.Top, Top = 336, AutoSize = true };
        _btnAnimate = new KryptonButton { Values = { Text = @"Animate 0 -> 100" } };
        _btnReset = new KryptonButton { Values = { Text = @"Reset" } };
        buttonRow.Controls.Add(_btnAnimate);
        buttonRow.Controls.Add(_btnReset);
        mainPanel.Controls.Add(buttonRow);

        _lblStatus = new KryptonLabel { Dock = DockStyle.Bottom, Values = { Text = @"Ready." } };
        mainPanel.Controls.Add(_lblStatus);

        Controls.Add(mainPanel);

        WireEvents();
    }

    private static void AddRow(TableLayoutPanel table, ref int row, string label1, Control? control1, string label2, Control? control2)
    {
        table.RowCount = row + 1;
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));

        table.Controls.Add(new KryptonLabel { Values = { Text = label1 }, Dock = DockStyle.Fill }, 0, row);

        if (control1 != null)
        {
            control1.Dock = DockStyle.Fill;
            table.Controls.Add(control1, 1, row);
        }

        if (!string.IsNullOrEmpty(label2))
        {
            table.Controls.Add(new KryptonLabel { Values = { Text = label2 }, Dock = DockStyle.Fill }, 2, row);
        }

        if (control2 != null)
        {
            control2.Dock = DockStyle.Fill;
            table.Controls.Add(control2, 3, row);
        }

        row++;
    }

    private void WireEvents()
    {
        _chkUseKryptonRender.CheckedChanged += (_, _) =>
        {
            _enhancedProgressBar.UseKryptonRender = _chkUseKryptonRender.Checked;
            UpdateStatus($@"EnhancedProgressBar.UseKryptonRender = {_chkUseKryptonRender.Checked}");
        };

        _chkDisplayValue.CheckedChanged += (_, _) =>
        {
            _valueTextProgressBar.DisplayValue = _chkDisplayValue.Checked;
            UpdateStatus($@"ProgressBarWithValueText.DisplayValue = {_chkDisplayValue.Checked}");
        };

        _chkUseAsProgressBar.CheckedChanged += (_, _) =>
        {
            _progressStatusStrip.UseAsProgressBar = _chkUseAsProgressBar.Checked;
            UpdateStatus($@"ProgressStatusStrip.UseAsProgressBar = {_chkUseAsProgressBar.Checked}");
        };

        _chkLoadingActive.CheckedChanged += (_, _) =>
        {
            _standaloneLoadingCircle.Active = _chkLoadingActive.Checked;
            _menuLoadingCircleItem.LoadingCircleControl!.Active = _chkLoadingActive.Checked;
            UpdateStatus($@"LoadingCircle(s).Active = {_chkLoadingActive.Checked}");
        };

        _cmbStylePreset.SelectedIndexChanged += (_, _) =>
        {
            if (Enum.TryParse(_cmbStylePreset.Text, out KryptonLoadingCircle.StylePresets preset))
            {
                _standaloneLoadingCircle.StylePreset = preset;
                _menuLoadingCircleItem.LoadingCircleControl!.StylePreset = preset;
                UpdateStatus($@"LoadingCircle.StylePreset = {preset}");
            }
        };

        _btnAnimate.Click += (_, _) =>
        {
            _enhancedProgressBar.Value = _enhancedProgressBar.Minimum;
            _valueTextProgressBar.Value = _valueTextProgressBar.Minimum;
            _progressStatusStrip.CurrentValue = _progressStatusStrip.MinimumValue;
            _animationTimer.Start();
            UpdateStatus(@"Animating all progress indicators toward 100.");
        };

        _btnReset.Click += (_, _) =>
        {
            _animationTimer.Stop();
            _enhancedProgressBar.Value = _enhancedProgressBar.Minimum;
            _valueTextProgressBar.Value = _valueTextProgressBar.Minimum;
            _progressStatusStrip.CurrentValue = _progressStatusStrip.MinimumValue;
            UpdateStatus(@"Reset.");
        };
    }

    private void OnAnimationTick(object? sender, EventArgs e)
    {
        int next = _enhancedProgressBar.Value + 1;

        if (next >= _enhancedProgressBar.Maximum)
        {
            _enhancedProgressBar.Value = _enhancedProgressBar.Maximum;
            _valueTextProgressBar.Value = _valueTextProgressBar.Maximum;
            _progressStatusStrip.CurrentValue = _progressStatusStrip.MaximumValue;
            _animationTimer.Stop();
            UpdateStatus(@"Animation complete.");
            return;
        }

        _enhancedProgressBar.Value = next;
        _valueTextProgressBar.Value = next;
        _progressStatusStrip.CurrentValue = next;
    }

    private void UpdateStatus(string message) => _lblStatus.Values.Text = message;
}

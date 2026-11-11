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
/// Demonstrates <see cref="KryptonSlider"/> hosted on a <see cref="KryptonBasicToolStrip"/>, alongside a standalone
/// <see cref="KryptonToolbarSlider"/> placed directly on a panel. Both share the same underlying slider control, so
/// <see cref="KryptonToolbarSlider.Value"/>, <see cref="KryptonToolbarSlider.Range"/>, <see cref="KryptonToolbarSlider.Steps"/>,
/// <see cref="KryptonToolbarSlider.FireInterval"/>, and <see cref="KryptonToolbarSlider.SingleClick"/> are exercised once and
/// reflected on both. The small +/- chrome either side of the slider track is drawn by the internal
/// <see cref="KryptonSliderButton"/> control.
/// </summary>
public class ToolStripSliderDemo : KryptonForm
{
    private readonly KryptonSlider _toolStripSliderItem;
    private readonly KryptonToolbarSlider _standaloneSlider;

    private readonly KryptonNumericUpDown _numRange;
    private readonly KryptonNumericUpDown _numSteps;
    private readonly KryptonNumericUpDown _numFireInterval;
    private readonly KryptonCheckBox _chkSingleClick;
    private readonly KryptonLabel _lblToolStripSliderValue;
    private readonly KryptonLabel _lblStandaloneSliderValue;
    private readonly KryptonLabel _lblStatus;

    private bool _syncing;

    public ToolStripSliderDemo()
    {
        Text = @"ToolStrip Slider Demo";
        Size = new Size(900, 600);
        StartPosition = FormStartPosition.CenterParent;

        // ----- Top: KryptonSlider hosted on a KryptonBasicToolStrip -----
        var strip = new KryptonBasicToolStrip { Dock = DockStyle.Top };
        strip.Items.Add(new ToolStripLabel(@"KryptonSlider (ToolStrip-hosted):"));

        _toolStripSliderItem = new KryptonSlider
        {
            Range = 100,
            Steps = 5,
            Value = 0,
            SingleClick = false,
            FireInterval = 200
        };
        strip.Items.Add(_toolStripSliderItem);

        Controls.Add(strip);

        // ----- Main content -----
        var mainPanel = new KryptonPanel { Dock = DockStyle.Fill, Padding = new Padding(12) };

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 88,
            Text = @"Drag the slider knob or click the +/- glyphs either side of the track (drawn by the internal " +
                   @"KryptonSliderButton chrome). The toolbar slider above and the standalone KryptonToolbarSlider " +
                   @"below share the same Range/Steps/FireInterval/SingleClick settings applied from the controls here."
        };
        mainPanel.Controls.Add(instructions);

        var options = new TableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Top = 92, ColumnCount = 4 };
        options.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
        options.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        options.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
        options.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

        _numRange = new KryptonNumericUpDown { Minimum = 10, Maximum = 500, Value = 100 };
        _numSteps = new KryptonNumericUpDown { Minimum = 1, Maximum = 50, Value = 5 };
        _numFireInterval = new KryptonNumericUpDown { Minimum = 20, Maximum = 2000, Value = 200 };
        _chkSingleClick = new KryptonCheckBox { Values = { Text = @"SingleClick" } };

        int row = 0;
        AddRow(options, ref row, @"Range:", _numRange, @"Steps:", _numSteps);
        AddRow(options, ref row, @"FireInterval (ms):", _numFireInterval, @"SingleClick:", _chkSingleClick);
        mainPanel.Controls.Add(options);

        var sliderHost = new KryptonGroupBox
        {
            Dock = DockStyle.Top,
            Top = 160,
            Height = 90,
            Values = { Heading = @"Standalone KryptonToolbarSlider" }
        };
        _standaloneSlider = new KryptonToolbarSlider
        {
            Location = new Point(16, 16),
            Size = new Size(300, 16),
            Range = 100,
            Steps = 5,
            Value = 0
        };
        sliderHost.Panel.Controls.Add(_standaloneSlider);
        mainPanel.Controls.Add(sliderHost);

        var readoutRow = new FlowLayoutPanel { Dock = DockStyle.Top, Top = 254, AutoSize = true };
        _lblToolStripSliderValue = new KryptonLabel { Values = { Text = @"ToolStrip slider Value = 0" } };
        _lblStandaloneSliderValue = new KryptonLabel { Values = { Text = @"Standalone slider Value = 0" }, Margin = new Padding(24, 0, 0, 0) };
        readoutRow.Controls.Add(_lblToolStripSliderValue);
        readoutRow.Controls.Add(_lblStandaloneSliderValue);
        mainPanel.Controls.Add(readoutRow);

        _lblStatus = new KryptonLabel { Dock = DockStyle.Bottom, Values = { Text = @"Ready." } };
        mainPanel.Controls.Add(_lblStatus);

        Controls.Add(mainPanel);

        WireEvents();
    }

    private static void AddRow(TableLayoutPanel table, ref int row, string label1, Control control1, string label2, Control control2)
    {
        table.RowCount = row + 1;
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));

        table.Controls.Add(new KryptonLabel { Values = { Text = label1 }, Dock = DockStyle.Fill }, 0, row);
        control1.Dock = DockStyle.Fill;
        table.Controls.Add(control1, 1, row);

        table.Controls.Add(new KryptonLabel { Values = { Text = label2 }, Dock = DockStyle.Fill }, 2, row);
        control2.Dock = DockStyle.Fill;
        table.Controls.Add(control2, 3, row);

        row++;
    }

    private void WireEvents()
    {
        _toolStripSliderItem.ValueChanged += (sender, e) =>
        {
            _lblToolStripSliderValue.Values.Text = $@"ToolStrip slider Value = {e.NewValue}";
            UpdateStatus($@"KryptonSlider.ValueChanged: {e.OldValue} -> {e.NewValue}");
        };

        _standaloneSlider.ValueChanged += (sender, e) =>
        {
            _lblStandaloneSliderValue.Values.Text = $@"Standalone slider Value = {e.NewValue}";
            UpdateStatus($@"KryptonToolbarSlider.ValueChanged: {e.OldValue} -> {e.NewValue}");
        };

        _numRange.ValueChanged += (_, _) => ApplyOptions();
        _numSteps.ValueChanged += (_, _) => ApplyOptions();
        _numFireInterval.ValueChanged += (_, _) => ApplyOptions();
        _chkSingleClick.CheckedChanged += (_, _) => ApplyOptions();
    }

    private void ApplyOptions()
    {
        if (_syncing)
        {
            return;
        }

        _syncing = true;

        int range = (int)_numRange.Value;
        int steps = (int)_numSteps.Value;
        int fireInterval = (int)_numFireInterval.Value;
        bool singleClick = _chkSingleClick.Checked;

        _toolStripSliderItem.Range = range;
        _toolStripSliderItem.Steps = steps;
        _toolStripSliderItem.FireInterval = fireInterval;
        _toolStripSliderItem.SingleClick = singleClick;

        _standaloneSlider.Range = range;
        _standaloneSlider.Steps = steps;
        _standaloneSlider.FireInterval = fireInterval;
        _standaloneSlider.SingleClick = singleClick;

        UpdateStatus($@"Applied Range={range}, Steps={steps}, FireInterval={fireInterval}, SingleClick={singleClick}");

        _syncing = false;
    }

    private void UpdateStatus(string message) => _lblStatus.Values.Text = message;
}

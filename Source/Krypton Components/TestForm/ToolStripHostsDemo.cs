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
/// Demonstrates the Krypton ToolStrip/StatusStrip host replacements in <c>Krypton.Toolkit.Utilities</c>:
/// <see cref="KryptonBasicToolStrip"/>, <see cref="KryptonEnhancedToolStrip"/> (<see cref="KryptonEnhancedToolStrip.ClickThrough"/>),
/// and <see cref="KryptonProgressStatusStrip"/> (<see cref="KryptonProgressStatusStrip.UseAsProgressBar"/> and value/colour properties),
/// each shown alongside a standard WinForms <see cref="ToolStrip"/> / <see cref="StatusStrip"/> for comparison.
/// </summary>
public class ToolStripHostsDemo : KryptonForm
{
    private readonly KryptonProgressStatusStrip _kryptonProgressStrip;
    private readonly ToolStripStatusLabel _kryptonProgressReadout;
    private readonly StatusStrip _nativeStatusStrip;
    private readonly ToolStripProgressBar _nativeProgressBar;
    private readonly KryptonEnhancedToolStrip _enhancedToolStrip;
    private readonly ToolStripLabel _enhancedClickThroughLabel;

    private readonly KryptonCheckBox _chkClickThrough;
    private readonly KryptonCheckBox _chkUseAsProgressBar;
    private readonly KryptonColorButton _btnBarColour;
    private readonly KryptonColorButton _btnBarShade;
    private readonly KryptonNumericUpDown _numCurrent;
    private readonly KryptonNumericUpDown _numMinimum;
    private readonly KryptonNumericUpDown _numMaximum;
    private readonly KryptonButton _btnAnimate;
    private readonly KryptonButton _btnReset;
    private readonly KryptonLabel _lblStatus;

    private readonly System.Windows.Forms.Timer _animationTimer;

    public ToolStripHostsDemo()
    {
        Text = @"ToolStrip Hosts Demo";
        Size = new Size(900, 600);
        StartPosition = FormStartPosition.CenterParent;

        _animationTimer = new System.Windows.Forms.Timer { Interval = 40 };
        _animationTimer.Tick += OnAnimationTick;

        // ----- Top: Krypton vs native ToolStrip, then KryptonEnhancedToolStrip -----
        var kryptonToolStrip = new KryptonBasicToolStrip { Dock = DockStyle.Top };
        kryptonToolStrip.Items.Add(new ToolStripLabel(@"KryptonBasicToolStrip (ManagerRenderMode):"));
        kryptonToolStrip.Items.Add(new ToolStripButton(@"Open"));
        kryptonToolStrip.Items.Add(new ToolStripButton(@"Save"));
        kryptonToolStrip.Items.Add(new ToolStripSeparator());
        kryptonToolStrip.Items.Add(new ToolStripButton(@"Print"));

        var nativeToolStrip = new ToolStrip { Dock = DockStyle.Top };
        nativeToolStrip.Items.Add(new ToolStripLabel(@"Native ToolStrip (comparison):"));
        nativeToolStrip.Items.Add(new ToolStripButton(@"Open"));
        nativeToolStrip.Items.Add(new ToolStripButton(@"Save"));
        nativeToolStrip.Items.Add(new ToolStripSeparator());
        nativeToolStrip.Items.Add(new ToolStripButton(@"Print"));

        _enhancedToolStrip = new KryptonEnhancedToolStrip { Dock = DockStyle.Top };
        _enhancedClickThroughLabel = new ToolStripLabel(@"KryptonEnhancedToolStrip - ClickThrough: False");
        _enhancedToolStrip.Items.Add(_enhancedClickThroughLabel);
        _enhancedToolStrip.Items.Add(new ToolStripButton(@"Action 1"));
        _enhancedToolStrip.Items.Add(new ToolStripButton(@"Action 2"));

        Controls.Add(_enhancedToolStrip);
        Controls.Add(nativeToolStrip);
        Controls.Add(kryptonToolStrip);

        // ----- Bottom: Krypton vs native progress/status strip -----
        _nativeProgressBar = new ToolStripProgressBar { Minimum = 0, Maximum = 100, Value = 0 };
        _nativeStatusStrip = new StatusStrip { Dock = DockStyle.Bottom };
        _nativeStatusStrip.Items.Add(new ToolStripStatusLabel(@"Native StatusStrip + ToolStripProgressBar:"));
        _nativeStatusStrip.Items.Add(_nativeProgressBar);

        _kryptonProgressReadout = new ToolStripStatusLabel(@"0 / 100") { Spring = true, TextAlign = ContentAlignment.MiddleRight };
        _kryptonProgressStrip = new KryptonProgressStatusStrip
        {
            Dock = DockStyle.Bottom,
            MinimumValue = 0f,
            MaximumValue = 100f,
            CurrentValue = 0f,
            BarColour = Color.ForestGreen,
            BarShade = Color.LightGreen
        };
        _kryptonProgressStrip.Items.Add(new ToolStripStatusLabel(@"KryptonProgressStatusStrip (UseAsProgressBar):"));
        _kryptonProgressStrip.Items.Add(_kryptonProgressReadout);

        Controls.Add(_nativeStatusStrip);
        Controls.Add(_kryptonProgressStrip);

        // ----- Middle: instructions + live controls -----
        var mainPanel = new KryptonPanel { Dock = DockStyle.Fill, Padding = new Padding(12) };

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 64,
            Text = @"Compare Krypton ToolStrip hosts with standard WinForms equivalents. Toggle " +
                   @"ClickThrough on the enhanced toolstrip (lets item clicks register on an inactive form " +
                   @"without first activating it). Toggle UseAsProgressBar and drive CurrentValue/Minimum/Maximum " +
                   @"on KryptonProgressStatusStrip, and compare with the native ToolStripProgressBar below."
        };
        mainPanel.Controls.Add(instructions);

        var options = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            ColumnCount = 4,
            Top = 70
        };
        options.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
        options.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        options.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
        options.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

        _chkClickThrough = new KryptonCheckBox { Values = { Text = @"ClickThrough" } };
        _chkUseAsProgressBar = new KryptonCheckBox { Values = { Text = @"UseAsProgressBar" } };

        _btnBarColour = new KryptonColorButton { SelectedColor = Color.ForestGreen, Values = { Text = @"BarColour" } };
        _btnBarShade = new KryptonColorButton { SelectedColor = Color.LightGreen, Values = { Text = @"BarShade" } };

        _numMinimum = new KryptonNumericUpDown { Minimum = -1000, Maximum = 1000, Value = 0 };
        _numMaximum = new KryptonNumericUpDown { Minimum = -1000, Maximum = 1000, Value = 100 };
        _numCurrent = new KryptonNumericUpDown { Minimum = -1000, Maximum = 1000, Value = 0 };

        _btnAnimate = new KryptonButton { Values = { Text = @"Animate 0 -> Max" } };
        _btnReset = new KryptonButton { Values = { Text = @"Reset" } };

        int row = 0;
        AddRow(options, ref row, @"ClickThrough:", _chkClickThrough, @"UseAsProgressBar:", _chkUseAsProgressBar);
        AddRow(options, ref row, @"BarColour:", _btnBarColour, @"BarShade:", _btnBarShade);
        AddRow(options, ref row, @"MinimumValue:", _numMinimum, @"MaximumValue:", _numMaximum);
        AddRow(options, ref row, @"CurrentValue:", _numCurrent, string.Empty, null);

        var buttonRow = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Top = 220 };
        buttonRow.Controls.Add(_btnAnimate);
        buttonRow.Controls.Add(_btnReset);

        _lblStatus = new KryptonLabel { Dock = DockStyle.Bottom, Values = { Text = @"Ready." } };

        mainPanel.Controls.Add(buttonRow);
        mainPanel.Controls.Add(options);
        mainPanel.Controls.Add(_lblStatus);

        Controls.Add(mainPanel);

        WireEvents();
        SyncReadout();
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
        _chkClickThrough.CheckedChanged += (_, _) =>
        {
            _enhancedToolStrip.ClickThrough = _chkClickThrough.Checked;
            _enhancedClickThroughLabel.Text = $@"KryptonEnhancedToolStrip - ClickThrough: {_enhancedToolStrip.ClickThrough}";
            UpdateStatus($@"ClickThrough = {_enhancedToolStrip.ClickThrough}");
        };

        _chkUseAsProgressBar.CheckedChanged += (_, _) =>
        {
            _kryptonProgressStrip.UseAsProgressBar = _chkUseAsProgressBar.Checked;
            UpdateStatus($@"UseAsProgressBar = {_kryptonProgressStrip.UseAsProgressBar}");
        };

        _btnBarColour.SelectedColorChanged += (_, _) => _kryptonProgressStrip.BarColour = _btnBarColour.SelectedColor;
        _btnBarShade.SelectedColorChanged += (_, _) => _kryptonProgressStrip.BarShade = _btnBarShade.SelectedColor;

        _numMinimum.ValueChanged += (_, _) =>
        {
            _kryptonProgressStrip.MinimumValue = (float)_numMinimum.Value;
            SyncReadout();
        };

        _numMaximum.ValueChanged += (_, _) =>
        {
            _kryptonProgressStrip.MaximumValue = (float)_numMaximum.Value;
            _nativeProgressBar.Maximum = (int)_numMaximum.Value;
            SyncReadout();
        };

        _numCurrent.ValueChanged += (_, _) =>
        {
            _kryptonProgressStrip.CurrentValue = (float)_numCurrent.Value;
            SyncReadout();
        };

        _btnAnimate.Click += (_, _) =>
        {
            _numCurrent.Value = _numMinimum.Value;
            _animationTimer.Start();
            UpdateStatus(@"Animating CurrentValue toward MaximumValue.");
        };

        _btnReset.Click += (_, _) =>
        {
            _animationTimer.Stop();
            _numCurrent.Value = _numMinimum.Value;
            UpdateStatus(@"Reset.");
        };
    }

    private void OnAnimationTick(object? sender, EventArgs e)
    {
        decimal next = _numCurrent.Value + 1;

        if (next >= _numMaximum.Value)
        {
            _numCurrent.Value = _numMaximum.Value;
            _animationTimer.Stop();
            UpdateStatus(@"Animation complete.");
            return;
        }

        _numCurrent.Value = next;
    }

    private void SyncReadout()
    {
        int clampedValue = Clamp((int)_numCurrent.Value, (int)_numMinimum.Value, (int)_numMaximum.Value);
        _nativeProgressBar.Minimum = (int)_numMinimum.Value;
        _nativeProgressBar.Maximum = (int)_numMaximum.Value;
        _nativeProgressBar.Value = clampedValue;
        _kryptonProgressReadout.Text = $@"{_numCurrent.Value} / {_numMaximum.Value}";
    }

    private static int Clamp(int value, int min, int max) => value < min ? min : value > max ? max : value;

    private void UpdateStatus(string message) => _lblStatus.Values.Text = message;
}

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
/// Demonstrates the safe subset of <see cref="KryptonToolStripLabelExtended"/> properties: gradient background
/// (<see cref="KryptonToolStripLabelExtended.GradientColorOne"/> / <see cref="KryptonToolStripLabelExtended.GradientColourTwo"/> /
/// <see cref="KryptonToolStripLabelExtended.GradientMode"/>), the alert colour set used while blinking, and
/// <see cref="KryptonToolStripLabelExtended.EnableBlinking"/>, shown alongside a plain <see cref="ToolStripStatusLabel"/>.
/// </summary>
/// <remarks>
/// <see cref="KryptonToolStripLabelExtended.EnableFadeAnimation"/> is intentionally never enabled here: its
/// <c>Tick</c> handler is an unfinished stub that throws <see cref="NotImplementedException"/>. For real blinking
/// status text, prefer the Timer-driven <see cref="KryptonBlinkingToolStripStatusLabel"/> (see the
/// "Blinking Status Strip Label" demo) instead of this draft label's paint-driven blink.
/// </remarks>
public class ToolStripLabelsDemo : KryptonForm
{
    private readonly KryptonToolStripLabelExtended _extendedLabel;
    private readonly ToolStripStatusLabel _nativeLabel;

    private readonly KryptonColorButton _btnGradientOne;
    private readonly KryptonColorButton _btnGradientTwo;
    private readonly KryptonColorButton _btnAlertOne;
    private readonly KryptonColorButton _btnAlertTwo;
    private readonly KryptonColorButton _btnAlertText;
    private readonly KryptonComboBox _cmbGradientMode;
    private readonly KryptonNumericUpDown _numAlertBlinkInterval;
    private readonly KryptonNumericUpDown _numBlinkDuration;
    private readonly KryptonCheckBox _chkEnableBlinking;
    private readonly KryptonCheckBox _chkBkClr;
    private readonly KryptonButton _btnApplyGradient;
    private readonly KryptonButton _btnResetBackground;
    private readonly KryptonButton _btnTriggerBlink;
    private readonly KryptonLabel _lblStatus;

    public ToolStripLabelsDemo()
    {
        Text = @"ToolStrip Labels Demo";
        Size = new Size(900, 600);
        StartPosition = FormStartPosition.CenterParent;

        // ----- Bottom: KryptonToolStripLabelExtended side-by-side with a native ToolStripStatusLabel -----
        var statusStrip = new StatusStrip { Dock = DockStyle.Bottom };

        _extendedLabel = new KryptonToolStripLabelExtended
        {
            Text = @"KryptonToolStripLabelExtended",
            Spring = true,
            TextAlign = ContentAlignment.MiddleLeft,
            // The draft control's default constructor enables blinking; start disabled so the demo is quiet
            // until the checkbox below is ticked (see class remarks re: EnableFadeAnimation).
            EnableBlinking = false
        };

        _nativeLabel = new ToolStripStatusLabel(@"Native ToolStripStatusLabel")
        {
            Spring = true,
            TextAlign = ContentAlignment.MiddleLeft
        };

        statusStrip.Items.Add(_extendedLabel);
        statusStrip.Items.Add(_nativeLabel);
        Controls.Add(statusStrip);

        // ----- Main content -----
        var mainPanel = new KryptonPanel { Dock = DockStyle.Fill, Padding = new Padding(12) };

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 96,
            Text = @"KryptonToolStripLabelExtended (left, status strip below) is an unfinished draft label: only the " +
                   @"gradient background, alert colours, and paint-driven blink are exercised here. EnableFadeAnimation " +
                   @"is never enabled - its Tick handler throws NotImplementedException. For production blinking status " +
                   @"text, prefer KryptonBlinkingToolStripStatusLabel (see the separate Blinking Status Strip Label demo)."
        };
        mainPanel.Controls.Add(instructions);

        var options = new TableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Top = 100, ColumnCount = 4 };
        options.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
        options.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        options.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
        options.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

        _btnGradientOne = new KryptonColorButton { SelectedColor = Color.White, Values = { Text = @"Gradient 1" } };
        _btnGradientTwo = new KryptonColorButton { SelectedColor = Color.SteelBlue, Values = { Text = @"Gradient 2" } };
        _cmbGradientMode = new KryptonComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        foreach (System.Drawing.Drawing2D.LinearGradientMode mode in Enum.GetValues(typeof(System.Drawing.Drawing2D.LinearGradientMode)))
        {
            _cmbGradientMode.Items.Add(mode);
        }
        _cmbGradientMode.SelectedItem = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;

        _btnAlertOne = new KryptonColorButton { SelectedColor = Color.White, Values = { Text = @"AlertColourOne" } };
        _btnAlertTwo = new KryptonColorButton { SelectedColor = Color.OrangeRed, Values = { Text = @"AlertColourTwo" } };
        _btnAlertText = new KryptonColorButton { SelectedColor = Color.DarkRed, Values = { Text = @"AlertTextColour" } };

        _numAlertBlinkInterval = new KryptonNumericUpDown { Minimum = 20, Maximum = 5000, Value = 256 };
        _numBlinkDuration = new KryptonNumericUpDown { Minimum = 0, Maximum = 60000, Value = 10 };

        _chkEnableBlinking = new KryptonCheckBox { Values = { Text = @"EnableBlinking" } };
        _chkBkClr = new KryptonCheckBox { Values = { Text = @"BkClr (blink drives BackColor)" } };

        int row = 0;
        AddRow(options, ref row, @"GradientColourOne:", _btnGradientOne, @"GradientColourTwo:", _btnGradientTwo);
        AddRow(options, ref row, @"GradientMode:", _cmbGradientMode, string.Empty, null);
        AddRow(options, ref row, @"AlertColourOne:", _btnAlertOne, @"AlertColourTwo:", _btnAlertTwo);
        AddRow(options, ref row, @"AlertTextColour:", _btnAlertText, @"AlertBlinkInterval (ms):", _numAlertBlinkInterval);
        AddRow(options, ref row, @"BlinkDuration (ms):", _numBlinkDuration, string.Empty, null);
        AddRow(options, ref row, @"EnableBlinking:", _chkEnableBlinking, string.Empty, _chkBkClr);
        mainPanel.Controls.Add(options);

        var buttonRow = new FlowLayoutPanel { Dock = DockStyle.Top, Top = 300, AutoSize = true };
        _btnApplyGradient = new KryptonButton { Values = { Text = @"Apply Gradient Background" } };
        _btnResetBackground = new KryptonButton { Values = { Text = @"Reset to Solid Background" } };
        _btnTriggerBlink = new KryptonButton { Values = { Text = @"Trigger Blink Cycle (Invalidate)" } };
        buttonRow.Controls.Add(_btnApplyGradient);
        buttonRow.Controls.Add(_btnResetBackground);
        buttonRow.Controls.Add(_btnTriggerBlink);
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
        _btnApplyGradient.Click += (_, _) =>
        {
            // The gradient paint path only runs when BackColor is Color.Empty (see OnPaint override).
            _extendedLabel.BackColor = Color.Empty;
            _extendedLabel.GradientColorOne = _btnGradientOne.SelectedColor;
            _extendedLabel.GradientColorTwo = _btnGradientTwo.SelectedColor;

            if (_cmbGradientMode.SelectedItem is System.Drawing.Drawing2D.LinearGradientMode mode)
            {
                _extendedLabel.GradientMode = mode;
            }

            _extendedLabel.Invalidate();
            UpdateStatus($@"Gradient applied: {_btnGradientOne.SelectedColor} -> {_btnGradientTwo.SelectedColor}, Mode = {_extendedLabel.GradientMode}");
        };

        _btnResetBackground.Click += (_, _) =>
        {
            _extendedLabel.BackColor = SystemColors.Control;
            _extendedLabel.Invalidate();
            UpdateStatus(@"Background reset to a solid colour (gradient path disabled while BackColor != Color.Empty).");
        };

        _chkEnableBlinking.CheckedChanged += (_, _) =>
        {
            _extendedLabel.EnableBlinking = _chkEnableBlinking.Checked;
            UpdateStatus($@"EnableBlinking = {_extendedLabel.EnableBlinking}");
        };

        _chkBkClr.CheckedChanged += (_, _) =>
        {
            _extendedLabel.BkClr = _chkBkClr.Checked;
            UpdateStatus($@"BkClr = {_extendedLabel.BkClr}");
        };

        _numAlertBlinkInterval.ValueChanged += (_, _) => _extendedLabel.AlertBlinkInterval = (int)_numAlertBlinkInterval.Value;
        _numBlinkDuration.ValueChanged += (_, _) => _extendedLabel.BlinkDuration = (long)_numBlinkDuration.Value;

        _btnAlertOne.SelectedColorChanged += (_, _) => _extendedLabel.AlertColorOne = _btnAlertOne.SelectedColor;
        _btnAlertTwo.SelectedColorChanged += (_, _) => _extendedLabel.AlertColorTwo = _btnAlertTwo.SelectedColor;
        _btnAlertText.SelectedColorChanged += (_, _) => _extendedLabel.AlertTextColor = _btnAlertText.SelectedColor;

        _btnTriggerBlink.Click += (_, _) =>
        {
            if (!_extendedLabel.EnableBlinking)
            {
                UpdateStatus(@"Tick EnableBlinking first - Invalidate() only starts a blink cycle when EnableBlinking is true.");
                return;
            }

            _extendedLabel.Invalidate();
            UpdateStatus(@"Invalidate() called - one blink cycle (AlertColourOne <-> AlertColourTwo) runs on repaint.");
        };
    }

    private void UpdateStatus(string message) => _lblStatus.Values.Text = message;
}

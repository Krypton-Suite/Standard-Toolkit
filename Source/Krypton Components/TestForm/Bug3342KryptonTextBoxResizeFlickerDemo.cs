#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public class Bug3342KryptonTextBoxResizeFlickerDemo : KryptonForm
{
    private readonly KryptonWrapLabel _lblInfo;
    private readonly KryptonButton _btnToggleStressResize;
    private readonly KryptonPanel _hostPanel;
    private readonly TableLayoutPanel _comparisonLayout;
    private readonly Label _lblNativeHeader;
    private readonly Label _lblKryptonHeader;
    private readonly TextBox _nativeTextBox;
    private readonly KryptonTextBox _kryptonTextBox;
    private readonly Timer _resizeTimer;
    private bool _growWidth;
    private const int MinimumDemoWidth = 760;
    private const int MaximumDemoWidth = 1120;

    public Bug3342KryptonTextBoxResizeFlickerDemo()
    {
        Text = @"Bug #3342 - Multiline KryptonTextBox Resize Flicker Demo";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(920, 620);
        MinimumSize = new Size(760, 500);

        _lblInfo = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            Height = 96,
            Text =
                @"How to test issue #3342:" + Environment.NewLine +
                @"1) Focus either textbox and select some text in both." + Environment.NewLine +
                @"2) Resize the form manually, or press Start Stress Resize." + Environment.NewLine +
                @"3) Compare Native TextBox (left) and KryptonTextBox (right) rendering stability."
        };

        _btnToggleStressResize = new KryptonButton
        {
            Dock = DockStyle.Top,
            Height = 36,
            Text = @"Start Stress Resize"
        };
        _btnToggleStressResize.Click += OnToggleStressResizeClick;

        _hostPanel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12)
        };

        _comparisonLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 2
        };
        _comparisonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
        _comparisonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
        _comparisonLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _comparisonLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

        _lblNativeHeader = new Label
        {
            Dock = DockStyle.Fill,
            AutoSize = true,
            Padding = new Padding(0, 0, 0, 8),
            Text = @"Native TextBox (baseline)"
        };

        _lblKryptonHeader = new Label
        {
            Dock = DockStyle.Fill,
            AutoSize = true,
            Padding = new Padding(0, 0, 0, 8),
            Text = @"KryptonTextBox (issue #3342)"
        };

        _nativeTextBox = new TextBox
        {
            Dock = DockStyle.Fill,
            Multiline = true,
            AcceptsReturn = true,
            WordWrap = true,
            ScrollBars = ScrollBars.Vertical,
            Text = @"This is the native WinForms TextBox for side-by-side comparison." + Environment.NewLine +
                   @"Resize the form and compare redraw behavior with KryptonTextBox." + Environment.NewLine +
                   @"Use Start Stress Resize to force rapid repaint."
        };

        _kryptonTextBox = new KryptonTextBox
        {
            Dock = DockStyle.Fill,
            Multiline = true,
            AcceptsReturn = true,
            WordWrap = true,
            ScrollBars = ScrollBars.Vertical,
            Text = @"This is a multiline KryptonTextBox used to validate fix #3342." + Environment.NewLine +
                   @"Resize the form while this control contains text to verify there is no flicker." + Environment.NewLine +
                   @"You can also click 'Start Stress Resize' for rapid width changes."
        };

        _comparisonLayout.Controls.Add(_lblNativeHeader, 0, 0);
        _comparisonLayout.Controls.Add(_lblKryptonHeader, 1, 0);
        _comparisonLayout.Controls.Add(_nativeTextBox, 0, 1);
        _comparisonLayout.Controls.Add(_kryptonTextBox, 1, 1);

        _hostPanel.Controls.Add(_comparisonLayout);
        Controls.Add(_hostPanel);
        Controls.Add(_btnToggleStressResize);
        Controls.Add(_lblInfo);

        _resizeTimer = new Timer
        {
            Interval = 40
        };
        _resizeTimer.Tick += OnResizeTimerTick;
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _resizeTimer.Stop();
        _resizeTimer.Tick -= OnResizeTimerTick;
        base.OnFormClosed(e);
    }

    private void OnToggleStressResizeClick(object? sender, EventArgs e)
    {
        if (_resizeTimer.Enabled)
        {
            _resizeTimer.Stop();
            _btnToggleStressResize.Text = @"Start Stress Resize";
        }
        else
        {
            _growWidth = true;
            _resizeTimer.Start();
            _btnToggleStressResize.Text = @"Stop Stress Resize";
        }
    }

    private void OnResizeTimerTick(object? sender, EventArgs e)
    {
        const int step = 8;

        if (_growWidth)
        {
            Width += step;
            if (Width >= MaximumDemoWidth)
            {
                _growWidth = false;
            }
        }
        else
        {
            Width -= step;
            if (Width <= MinimumDemoWidth)
            {
                _growWidth = true;
            }
        }
    }
}

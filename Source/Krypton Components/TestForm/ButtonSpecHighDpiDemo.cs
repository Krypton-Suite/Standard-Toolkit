#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit;

namespace TestForm;

/// <summary>
/// Demonstrates high-DPI ButtonSpec image resolution (Issue #978).
/// </summary>
public class ButtonSpecHighDpiDemo : KryptonForm
{
    private readonly KryptonHeader _header;
    private readonly KryptonHeaderGroup _headerGroup;
    private readonly KryptonTextBox _textBox;
    private readonly KryptonComboBox _comboBox;
    private readonly KryptonLabel _lblDpi;
    private readonly KryptonLabel _lblInfo;
    private readonly Timer _dpiTimer;

    public ButtonSpecHighDpiDemo()
    {
        Text = @"ButtonSpec High DPI Demo (#978)";
        Size = new Size(760, 520);
        StartPosition = FormStartPosition.CenterScreen;

        _lblDpi = new KryptonLabel
        {
            Location = new Point(12, 12),
            AutoSize = true,
            Text = @"DPI: ..."
        };

        _lblInfo = new KryptonLabel
        {
            Location = new Point(12, 36),
            Size = new Size(720, 88),
            Text = @"Built-in ButtonSpec images resolve through ButtonSpecImageResolver using per-monitor DPI. "
                   + @"All inherited palette glyphs receive lazy @2x/@3x sources (placeholders until dedicated art ships). "
                   + @"Move between monitors or change display scaling, then click Refresh DPI."
        };

        _header = new KryptonHeader
        {
            Location = new Point(12, 130),
            Size = new Size(720, 36),
            Values =
            {
                Heading = @"KryptonHeader",
                Description = @"Close + Context ButtonSpecs"
            }
        };
        _header.ButtonSpecs.Add(new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Context,
            Edge = PaletteRelativeEdgeAlign.Far
        });
        _header.ButtonSpecs.Add(new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Close,
            Edge = PaletteRelativeEdgeAlign.Far
        });

        _headerGroup = new KryptonHeaderGroup
        {
            Location = new Point(12, 180),
            Size = new Size(720, 110)
        };
        _headerGroup.ValuesPrimary.Heading = @"KryptonHeaderGroup";
        _headerGroup.ValuesPrimary.Description = @"Arrow, Pin, DropDown ButtonSpecs";
        _headerGroup.ButtonSpecs.Add(new ButtonSpecHeaderGroup { Type = PaletteButtonSpecStyle.ArrowLeft });
        _headerGroup.ButtonSpecs.Add(new ButtonSpecHeaderGroup { Type = PaletteButtonSpecStyle.ArrowRight });
        _headerGroup.ButtonSpecs.Add(new ButtonSpecHeaderGroup { Type = PaletteButtonSpecStyle.PinVertical });
        _headerGroup.ButtonSpecs.Add(new ButtonSpecHeaderGroup
        {
            Type = PaletteButtonSpecStyle.DropDown,
            Edge = PaletteRelativeEdgeAlign.Far
        });

        _textBox = new KryptonTextBox
        {
            Location = new Point(12, 310),
            Size = new Size(340, 27),
            Text = @"KryptonTextBox (InputControl Close)"
        };
        _textBox.ButtonSpecs.Add(new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Close,
            Style = PaletteButtonStyle.InputControl
        });

        _comboBox = new KryptonComboBox
        {
            Location = new Point(12, 350),
            Size = new Size(340, 27)
        };
        _comboBox.Items.AddRange(new object[] { "One", "Two", "Three" });
        _comboBox.SelectedIndex = 0;

        var btnRefresh = new KryptonButton
        {
            Location = new Point(12, 400),
            Size = new Size(140, 32),
            Text = @"Refresh DPI"
        };
        btnRefresh.Click += (_, _) => RefreshDpi();

        var themeCombo = new KryptonThemeComboBox
        {
            Location = new Point(12, 450),
            Size = new Size(320, 27)
        };

        Controls.Add(_lblDpi);
        Controls.Add(_lblInfo);
        Controls.Add(_header);
        Controls.Add(_headerGroup);
        Controls.Add(_textBox);
        Controls.Add(_comboBox);
        Controls.Add(btnRefresh);
        Controls.Add(themeCombo);

        _dpiTimer = new Timer { Interval = 500 };
        _dpiTimer.Tick += (_, _) => UpdateDpiLabel();
        _dpiTimer.Start();

        Move += (_, _) => RefreshDpi();
        DpiChanged += (_, _) => RefreshDpi();

        UpdateDpiLabel();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dpiTimer.Stop();
            _dpiTimer.Dispose();
        }

        base.Dispose(disposing);
    }

    private void RefreshDpi()
    {
        KryptonManager.InvalidateDpiCache();
        UpdateDpiLabel();
        _header.PerformLayout();
        _headerGroup.PerformLayout();
        _textBox.PerformLayout();
        _comboBox.PerformLayout();
        Invalidate(true);
    }

    private void UpdateDpiLabel()
    {
        float factor = DeviceDpi / 96f;
        string tier = factor >= 2.5f ? "@3x path" : factor >= 1.5f ? "@2x path" : "baseline";
        _lblDpi.Text = $@"DeviceDpi: {DeviceDpi}  |  Factor: {factor:0.##}  |  Resolver tier: {tier}";
    }
}

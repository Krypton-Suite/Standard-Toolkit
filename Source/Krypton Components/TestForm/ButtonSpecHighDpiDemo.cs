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
    private readonly KryptonHeader _earlyScaleHeader;
    private readonly KryptonHeaderGroup _headerGroup;
    private readonly KryptonTextBox _textBox;
    private readonly KryptonTextBox _cmdTextBox;
    private readonly KryptonComboBox _comboBox;
    private readonly KryptonLabel _lblDpi;
    private readonly KryptonLabel _lblInfo;
    private readonly KryptonLabel _lblEarlyScale;
    private readonly KryptonLabel _lblCommandPath;
    private readonly KryptonLabel _lblScroll;
    private readonly KryptonPanel _scrollHost;
    private readonly Timer _dpiTimer;

    /// <summary>
    /// Standard demo constructor.
    /// </summary>
    public ButtonSpecHighDpiDemo()
        : this(null, false)
    {
    }

    /// <summary>
    /// Optional constructor used when <see cref="Program"/> applies
    /// <see cref="PaletteImageScaler.ScalePalette"/> before showing this form.
    /// </summary>
    /// <param name="earlyScaledPalette">Pre-scaled custom palette for the early-scale header.</param>
    /// <param name="ranEarlyScaleBeforeShow">True when ScalePalette ran before this form was created.</param>
    public ButtonSpecHighDpiDemo(KryptonCustomPaletteBase? earlyScaledPalette, bool ranEarlyScaleBeforeShow)
    {
        Text = @"ButtonSpec High DPI Demo (#978)";
        Size = new Size(780, 720);
        StartPosition = FormStartPosition.CenterScreen;
        AutoScroll = false;

        _lblDpi = new KryptonLabel
        {
            Location = new Point(12, 12),
            AutoSize = true,
            Text = @"DPI: ..."
        };

        _lblInfo = new KryptonLabel
        {
            Location = new Point(12, 36),
            Size = new Size(740, 72),
            Text = @"Built-in ButtonSpec images resolve through ButtonSpecImageResolver using per-monitor DPI. "
                   + @"Use the sections below to compare global palette specs, early ScalePalette on a custom palette, "
                   + @"and command-backed InputControl specs. Move between monitors or click Refresh DPI."
        };

        _lblEarlyScale = new KryptonLabel
        {
            Location = new Point(12, 112),
            Size = new Size(740, 36),
            Text = @"Early ScalePalette: not run yet. Click the button to scale a local custom palette via PaletteImageScaler."
        };

        _earlyScaleHeader = new KryptonHeader
        {
            Location = new Point(12, 152),
            Size = new Size(360, 36),
            Values =
            {
                Heading = @"Early ScalePalette header",
                Description = @"Close spec on custom palette"
            }
        };
        _earlyScaleHeader.ButtonSpecs.Add(new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Close,
            Edge = PaletteRelativeEdgeAlign.Far
        });

        var btnEarlyScale = new KryptonButton
        {
            Location = new Point(390, 152),
            Size = new Size(180, 32),
            Text = @"Apply early ScalePalette"
        };
        btnEarlyScale.Click += (_, _) => ApplyEarlyScalePalette();

        _lblCommandPath = new KryptonLabel
        {
            Location = new Point(12, 196),
            Size = new Size(740, 20),
            Text = @"Command-backed path: KryptonCommand HelpCommand mapping (palette @2x/@3x, not upscaled 1x)."
        };

        _cmdTextBox = new KryptonTextBox
        {
            Location = new Point(12, 220),
            Size = new Size(340, 27),
            Text = @"KryptonTextBox (KryptonCommand Help)"
        };
        var helpCommand = new KryptonCommand
        {
            CommandType = KryptonCommandType.HelpCommand
        };
        _cmdTextBox.ButtonSpecs.Add(new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.FormHelp,
            Style = PaletteButtonStyle.InputControl,
            KryptonCommand = helpCommand
        });

        _header = new KryptonHeader
        {
            Location = new Point(12, 260),
            Size = new Size(720, 36),
            Values =
            {
                Heading = @"Global palette header",
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
            Location = new Point(12, 310),
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
            Location = new Point(12, 440),
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
            Location = new Point(12, 480),
            Size = new Size(340, 27)
        };
        _comboBox.Items.AddRange(new object[] { "One", "Two", "Three" });
        _comboBox.SelectedIndex = 0;

        var btnRefresh = new KryptonButton
        {
            Location = new Point(12, 530),
            Size = new Size(140, 32),
            Text = @"Refresh DPI"
        };
        btnRefresh.Click += (_, _) => RefreshDpi();

        var themeCombo = new KryptonThemeComboBox
        {
            Location = new Point(12, 580),
            Size = new Size(320, 27)
        };

        _lblScroll = new KryptonLabel
        {
            Location = new Point(400, 520),
            Size = new Size(360, 36),
            Text = @"Krypton scrollbars (not system AutoScroll): verify track/thumb fill at this DPI."
        };

        _scrollHost = new KryptonPanel
        {
            Location = new Point(400, 556),
            Size = new Size(360, 80)
        };
        var scrollFill = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.FromArgb(240, 248, 255)
        };
        var vScroll = new KryptonVScrollBar
        {
            Dock = DockStyle.Right,
            Maximum = 100,
            LargeChange = 20,
            Value = 35
        };
        var hScroll = new KryptonHScrollBar
        {
            Dock = DockStyle.Bottom,
            Maximum = 100,
            LargeChange = 20,
            Value = 35
        };
        _scrollHost.Controls.Add(scrollFill);
        _scrollHost.Controls.Add(vScroll);
        _scrollHost.Controls.Add(hScroll);

        Controls.Add(_lblDpi);
        Controls.Add(_lblInfo);
        Controls.Add(_lblEarlyScale);
        Controls.Add(_earlyScaleHeader);
        Controls.Add(btnEarlyScale);
        Controls.Add(_lblCommandPath);
        Controls.Add(_cmdTextBox);
        Controls.Add(_header);
        Controls.Add(_headerGroup);
        Controls.Add(_textBox);
        Controls.Add(_comboBox);
        Controls.Add(btnRefresh);
        Controls.Add(themeCombo);
        Controls.Add(_lblScroll);
        Controls.Add(_scrollHost);

        _dpiTimer = new Timer { Interval = 500 };
        _dpiTimer.Tick += (_, _) => UpdateDpiLabel();
        _dpiTimer.Start();

        Move += (_, _) => RefreshDpi();
        DpiChanged += (_, _) => RefreshDpi();

        UpdateDpiLabel();
        UpdateCommandPathLabel();

        if (earlyScaledPalette != null)
        {
            BindEarlyScalePalette(earlyScaledPalette, ranEarlyScaleBeforeShow);
        }
    }

    /// <summary>
    /// Creates a custom palette, runs <see cref="PaletteImageScaler.ScalePalette"/>, and binds it to the early-scale header.
    /// Exercises registry initialization on the ScalePalette path (not only via <see cref="KryptonManager"/>).
    /// </summary>
    private void ApplyEarlyScalePalette()
    {
        float factorX = DeviceDpi / 96f;
        float factorY = factorX;
        if (factorX < 1.25f)
        {
            factorX = 2f;
            factorY = 2f;
        }

        var palette = new KryptonCustomPaletteBase
        {
            BasePalette = KryptonManager.GetPaletteForMode(PaletteMode.Microsoft365Blue)
        };
        palette.ButtonSpecs.Close.PopulateFromBase(PaletteButtonSpecStyle.Close);

        Image? baseline = palette.GetButtonSpecImage(PaletteButtonSpecStyle.Close, PaletteState.Normal);
        PaletteImageScaler.ScalePalette(factorX, factorY, palette);

        BindEarlyScalePalette(palette, ranBeforeShow: false, baseline);
    }

    private void BindEarlyScalePalette(KryptonCustomPaletteBase palette, bool ranBeforeShow, Image? baseline = null)
    {
        baseline ??= palette.GetButtonSpecImage(PaletteButtonSpecStyle.Close, PaletteState.Normal);
        bool dedicated = ButtonSpecImageResolver.HasDedicatedScale2xSource(baseline);
        Image? scale2x = palette.GetButtonSpecImageScale2(PaletteButtonSpecStyle.Close, PaletteState.Normal);

        _earlyScaleHeader.LocalCustomPalette = palette;
        _earlyScaleHeader.PerformLayout();
        _earlyScaleHeader.Invalidate(true);

        string when = ranBeforeShow ? "before this form was shown (Program --button-spec-early-scale)"
            : "on button click via PaletteImageScaler";
        _lblEarlyScale.Text =
            $@"Early ScalePalette ({when}): dedicated @2x registered={dedicated}; "
            + $@"scale2x source={(scale2x == null ? "n/a" : $"{scale2x.Width}x{scale2x.Height}")}; "
            + $@"factor applied={DeviceDpi / 96f:0.##}. Compare Close glyph sharpness with the global header.";
    }

    private void UpdateCommandPathLabel()
    {
        Image? baseline = KryptonManager.CurrentGlobalPalette.GetButtonSpecImage(PaletteButtonSpecStyle.FormHelp,
            PaletteState.Normal);
        bool dedicated = ButtonSpecImageResolver.HasDedicatedScale2xSource(baseline);
        _lblCommandPath.Text =
            $@"Command-backed path: KryptonCommand HelpCommand — dedicated @2x registered={dedicated}; "
            + @"Help InputControl spec should match global FormHelp sharpness at 200%+ DPI.";
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
        UpdateCommandPathLabel();
        _header.PerformLayout();
        _earlyScaleHeader.PerformLayout();
        _headerGroup.PerformLayout();
        _textBox.PerformLayout();
        _cmdTextBox.PerformLayout();
        _comboBox.PerformLayout();
        _scrollHost.PerformLayout();
        Invalidate(true);
    }

    private void UpdateDpiLabel()
    {
        float factor = DeviceDpi / 96f;
        string tier = factor >= 2.5f ? "@3x path" : factor >= 1.5f ? "@2x path" : "baseline";
        _lblDpi.Text = $@"DeviceDpi: {DeviceDpi}  |  Factor: {factor:0.##}  |  Resolver tier: {tier}";
    }
}

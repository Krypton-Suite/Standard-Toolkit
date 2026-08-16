#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Internal splash window hosted on a dedicated STA thread by <see cref="KryptonSplashScreenManager"/>.
/// Hosts themed Krypton controls on a borderless <see cref="Form"/> rather than <see cref="KryptonForm"/>,
/// because KryptonForm custom chrome is not safe on a secondary splash thread.
/// </summary>
internal partial class VisualSplashScreenManagerForm : Form
{
    #region Static Fields

    // Matches KryptonFormFadeSpeed units applied as units/1000 on a 10 ms timer.
    private const float FadeSlowest = 1f;
    private const float FadeSlower = 10f;
    private const float FadeSlow = 25f;
    private const float FadeNormal = 50f;
    private const float FadeFast = 60f;
    private const float FadeFaster = 75f;
    private const float FadeFastest = 100f;

    #endregion

    #region Instance Fields

    private readonly KryptonSplashScreenManagerData _data;
    private readonly double _targetOpacity;
    private readonly float _fadeUnits;
    private readonly int _borderThickness;
    private readonly float _borderSpeed;
    private bool _allowClose;
    private bool _fadeIncreasing;
    private bool _closeAfterFadeOut;
    private float _borderPhase;
    private Timer? _fadeTimer;
    private Timer? _borderTimer;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="VisualSplashScreenManagerForm"/> class.</summary>
    /// <param name="data">Splash content and behaviour. Cannot be null.</param>
    /// <param name="paletteMode">Palette captured on the caller thread before this form is created.</param>
    public VisualSplashScreenManagerForm(KryptonSplashScreenManagerData data, PaletteMode paletteMode)
    {
        ThrowHelper.ThrowIfNull(data);

        SetStyle(ControlStyles.AllPaintingInWmPaint
                 | ControlStyles.OptimizedDoubleBuffer
                 | ControlStyles.UserPaint
                 | ControlStyles.ResizeRedraw, true);
        DoubleBuffered = true;

        InitializeComponent();

        _data = data;
        _targetOpacity = ClampOpacity(data.Opacity);
        _fadeUnits = ToFadeUnits(data.FadeSpeed);
        _borderThickness = data.BorderAnimationThickness < 1 ? 1 : data.BorderAnimationThickness;
        _borderSpeed = data.BorderAnimationSpeed <= 0f ? 1f : data.BorderAnimationSpeed;
        ApplyWindowChrome();
        ApplyPalette(paletteMode);
        ApplyFadeAndOpacity();
        ApplyLayout();
        ApplyIdentity();
        ApplyAnimatedBorder();
    }

    #endregion

    #region Internal

    /// <summary>
    /// Allows the manager to close the splash (after optional fade-out). User Alt+F4 / chrome close stays blocked.
    /// </summary>
    internal void RequestClose()
    {
        if (IsDisposed || Disposing)
        {
            return;
        }

        _allowClose = true;
        if (_data.FadeOut)
        {
            StartFade(false, true);
            return;
        }

        Close();
    }

    /// <summary>Updates the status label and optionally the determinate progress value.</summary>
    /// <param name="status">Status text to display.</param>
    /// <param name="progressValue">Progress 0–100, or a negative value to leave the bar unchanged.</param>
    internal void ApplyStatus(string? status, int progressValue)
    {
        if (IsDisposed)
        {
            return;
        }

        kwlblStatus.Text = string.IsNullOrWhiteSpace(status) ? string.Empty : status;
        if (progressValue >= 0)
        {
            ApplyProgress(progressValue, 100);
        }
    }

    /// <summary>Sets a determinate progress value and switches off marquee style.</summary>
    /// <param name="value">Progress value.</param>
    /// <param name="maximum">Progress maximum. Values below 1 are treated as 1.</param>
    internal void ApplyProgress(int value, int maximum)
    {
        if (IsDisposed || !kpbProgress.Visible)
        {
            return;
        }

        int max = maximum < 1 ? 1 : maximum;
        int clamped = Math.Max(0, Math.Min(value, max));
        kpbProgress.Style = ProgressBarStyle.Continuous;
        kpbProgress.Maximum = max;
        kpbProgress.Value = clamped;
        kpbProgress.Values.Text = max == 100 ? $@"{clamped}%" : string.Empty;
    }

    #endregion

    #region Protected Overrides

    /// <inheritdoc />
    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        if (_data.FadeIn)
        {
            StartFade(true, false);
        }
    }

    /// <inheritdoc />
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (!_allowClose && !IsForcedCloseReason(e.CloseReason))
        {
            e.Cancel = true;
            return;
        }

        base.OnFormClosing(e);
    }

    /// <inheritdoc />
    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        StopFadeTimer();
        StopBorderTimer();
        base.OnFormClosed(e);
    }

    /// <inheritdoc />
    protected override void OnPaintBackground(PaintEventArgs e)
    {
        Color back = ResolvePanelBackColor();
        using var brush = new SolidBrush(back);
        e.Graphics.FillRectangle(brush, ClientRectangle);
    }

    /// <inheritdoc />
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (_data.BorderAnimation != KryptonSplashBorderAnimation.None)
        {
            DrawAnimatedBorder(e.Graphics);
        }
    }

    #endregion

    #region Implementation

    private void ApplyWindowChrome()
    {
        FormBorderStyle = FormBorderStyle.None;
        ControlBox = false;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowIcon = false;
        ShowInTaskbar = false;
        TopMost = _data.TopMost;
        StartPosition = _data.StartPosition;
        Size clientSize = _data.Size.Width > 0 && _data.Size.Height > 0
            ? _data.Size
            : new Size(520, 320);
        ClientSize = clientSize;
    }

    private void ApplyPalette(PaletteMode paletteMode)
    {
        if (paletteMode == PaletteMode.Global)
        {
            return;
        }

        kpnlMain.PaletteMode = paletteMode;
        kwlblTitle.PaletteMode = paletteMode;
        kwlblStatus.PaletteMode = paletteMode;
        kwlblVersion.PaletteMode = paletteMode;
        kwlblCopyright.PaletteMode = paletteMode;
    }

    private void ApplyFadeAndOpacity()
    {
        if (_targetOpacity < 1.0)
        {
            AllowTransparency = true;
        }

        Opacity = _data.FadeIn ? 0 : _targetOpacity;
    }

    private void ApplyLayout()
    {
        if (_data.BackgroundImage != null)
        {
            kpnlMain.StateCommon.Image = _data.BackgroundImage;
            kpnlMain.StateCommon.ImageStyle = ToPaletteImageStyle(_data.BackgroundImageLayout);
        }

        pbxLogo.Image = _data.Logo;
        pbxLogo.Visible = _data.Logo != null;

        kpbProgress.Visible = _data.ShowProgressBar;
        if (_data.ShowProgressBar && !_data.ExpectedStepCount.HasValue)
        {
            kpbProgress.Style = ProgressBarStyle.Marquee;
        }
    }

    private void ApplyAnimatedBorder()
    {
        if (_data.BorderAnimation == KryptonSplashBorderAnimation.None)
        {
            return;
        }

        int pad = _borderThickness + 4;
        Padding = new Padding(pad);
        BackColor = ResolvePanelBackColor();
        _borderTimer = new Timer
        {
            Interval = 30
        };
        _borderTimer.Tick += OnBorderTick;
        _borderTimer.Start();
    }

    private void ApplyIdentity()
    {
        string title = !string.IsNullOrWhiteSpace(_data.Title)
            ? _data.Title!
            : Application.ProductName;
        kwlblTitle.Text = title;
        kwlblTitle.Visible = _data.ShowApplicationName || !string.IsNullOrWhiteSpace(_data.Title);
        Text = title;

        kwlblStatus.Text = string.IsNullOrWhiteSpace(_data.Status)
            ? string.Empty
            : _data.Status;

        FileVersionInfo? fvi = TryGetVersionInfo(_data.Assembly);
        string copyright = fvi?.LegalCopyright ?? string.Empty;
        kwlblCopyright.Visible = _data.ShowCopyright;
        kwlblCopyright.Text = string.IsNullOrEmpty(copyright)
            ? $@"{KryptonManager.Strings.SplashScreenStrings.Copyright}:"
            : $@"{KryptonManager.Strings.SplashScreenStrings.Copyright}: {copyright}";

        string fileVersion = fvi?.FileVersion
            ?? _data.Assembly?.GetName().Version?.ToString()
            ?? string.Empty;
        kwlblVersion.Visible = _data.ShowVersion;
        kwlblVersion.Text = string.IsNullOrEmpty(fileVersion)
            ? $@"{KryptonManager.Strings.SplashScreenStrings.Version}:"
            : $@"{KryptonManager.Strings.SplashScreenStrings.Version}: {fileVersion}";
    }

    private void StartFade(bool fadeIn, bool closeAfterFadeOut)
    {
        StopFadeTimer();
        _fadeIncreasing = fadeIn;
        _closeAfterFadeOut = closeAfterFadeOut;
        _fadeTimer = new Timer
        {
            Interval = 10
        };
        _fadeTimer.Tick += OnFadeTick;
        _fadeTimer.Start();
    }

    private void OnFadeTick(object? sender, EventArgs e)
    {
        if (IsDisposed)
        {
            StopFadeTimer();
            return;
        }

        double step = _fadeUnits / 1000.0;
        if (_fadeIncreasing)
        {
            if (Opacity < _targetOpacity)
            {
                Opacity = Math.Min(_targetOpacity, Opacity + step);
                return;
            }

            Opacity = _targetOpacity;
            StopFadeTimer();
            return;
        }

        if (Opacity > 0.05)
        {
            Opacity = Math.Max(0, Opacity - step);
            return;
        }

        Opacity = 0;
        StopFadeTimer();
        if (_closeAfterFadeOut)
        {
            _allowClose = true;
            Close();
        }
    }

    private void StopFadeTimer()
    {
        if (_fadeTimer == null)
        {
            return;
        }

        _fadeTimer.Stop();
        _fadeTimer.Tick -= OnFadeTick;
        _fadeTimer.Dispose();
        _fadeTimer = null;
    }

    private void OnBorderTick(object? sender, EventArgs e)
    {
        if (IsDisposed)
        {
            StopBorderTimer();
            return;
        }

        _borderPhase += 0.028f * _borderSpeed;
        if (_borderPhase >= 1f)
        {
            _borderPhase -= 1f;
        }

        Invalidate();
    }

    private void StopBorderTimer()
    {
        if (_borderTimer == null)
        {
            return;
        }

        _borderTimer.Stop();
        _borderTimer.Tick -= OnBorderTick;
        _borderTimer.Dispose();
        _borderTimer = null;
    }

    private void DrawAnimatedBorder(Graphics graphics)
    {
        int inset = Math.Max(1, _borderThickness / 2) + 1;
        var bounds = new Rectangle(
            inset,
            inset,
            Math.Max(1, ClientSize.Width - (inset * 2) - 1),
            Math.Max(1, ClientSize.Height - (inset * 2) - 1));
        using GraphicsPath path = CreateRoundRectangle(bounds, 6);
        graphics.SmoothingMode = SmoothingMode.AntiAlias;

        Color color = ResolveBorderColor();
        if (_data.BorderAnimation == KryptonSplashBorderAnimation.Pulse)
        {
            double wave = 0.35 + (0.65 * (0.5 + (0.5 * Math.Sin(_borderPhase * Math.PI * 2.0))));
            int alpha = Math.Max(40, Math.Min(255, (int)Math.Round(color.A * wave)));
            using var pen = new Pen(Color.FromArgb(alpha, color), _borderThickness)
            {
                LineJoin = LineJoin.Round
            };
            graphics.DrawPath(pen, path);
            return;
        }

        using (var dim = new Pen(Color.FromArgb(90, color), Math.Max(1, _borderThickness - 1))
        {
            LineJoin = LineJoin.Round
        })
        {
            graphics.DrawPath(dim, path);
        }

        using var sweep = new Pen(color, _borderThickness)
        {
            LineJoin = LineJoin.Round,
            StartCap = LineCap.Round,
            EndCap = LineCap.Round,
            DashStyle = DashStyle.Custom,
            DashPattern = [12f, 72f],
            DashOffset = -_borderPhase * 84f
        };
        graphics.DrawPath(sweep, path);
    }

    private Color ResolveBorderColor()
    {
        if (_data.BorderAnimationColor.HasValue && !_data.BorderAnimationColor.Value.IsEmpty)
        {
            return _data.BorderAnimationColor.Value;
        }

        Color color = KryptonManager.CurrentGlobalPalette.GetBorderColor1(
            PaletteBorderStyle.ButtonStandalone, PaletteState.Tracking);
        return color.IsEmpty
            ? KryptonManager.CurrentGlobalPalette.GetBorderColor1(PaletteBorderStyle.ControlClient, PaletteState.Normal)
            : color;
    }

    private Color ResolvePanelBackColor()
    {
        Color color = KryptonManager.CurrentGlobalPalette.GetBackColor1(
            PaletteBackStyle.PanelClient, PaletteState.Normal);
        return color.IsEmpty ? SystemColors.Control : color;
    }

    private static GraphicsPath CreateRoundRectangle(Rectangle bounds, int radius)
    {
        var path = new GraphicsPath();
        if (radius <= 0 || bounds.Width < radius * 2 || bounds.Height < radius * 2)
        {
            path.AddRectangle(bounds);
            return path;
        }

        int diameter = radius * 2;
        path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
        path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
        path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();
        return path;
    }

    private static FileVersionInfo? TryGetVersionInfo(Assembly? assembly)
    {
        if (assembly == null)
        {
            return null;
        }

        string location = assembly.Location;
        if (string.IsNullOrEmpty(location))
        {
            location = Assembly.GetEntryAssembly()?.Location ?? string.Empty;
        }

        if (string.IsNullOrEmpty(location))
        {
            location = Assembly.GetExecutingAssembly().Location;
        }

        if (string.IsNullOrEmpty(location))
        {
            return null;
        }

        try
        {
            return FileVersionInfo.GetVersionInfo(location);
        }
        catch (ArgumentException)
        {
            return null;
        }
        catch (FileNotFoundException)
        {
            return null;
        }
    }

    private static PaletteImageStyle ToPaletteImageStyle(ImageLayout layout) =>
        layout switch
        {
            ImageLayout.Tile => PaletteImageStyle.Tile,
            ImageLayout.Center => PaletteImageStyle.CenterMiddle,
            ImageLayout.Stretch => PaletteImageStyle.Stretch,
            ImageLayout.Zoom => PaletteImageStyle.Stretch,
            _ => PaletteImageStyle.TopLeft
        };

    private static bool IsForcedCloseReason(CloseReason closeReason) =>
        closeReason == CloseReason.WindowsShutDown
        || closeReason == CloseReason.TaskManagerClosing
        || closeReason == CloseReason.ApplicationExitCall;

    private static double ClampOpacity(double opacity)
    {
        if (opacity < 0.05)
        {
            return 0.05;
        }

        return opacity > 1.0 ? 1.0 : opacity;
    }

    private static float ToFadeUnits(FadeSpeedChoice speed) =>
        speed switch
        {
            FadeSpeedChoice.Slowest => FadeSlowest,
            FadeSpeedChoice.Slower => FadeSlower,
            FadeSpeedChoice.Slow => FadeSlow,
            FadeSpeedChoice.Fast => FadeFast,
            FadeSpeedChoice.Faster => FadeFaster,
            FadeSpeedChoice.Fastest => FadeFastest,
            _ => FadeNormal
        };

    #endregion
}

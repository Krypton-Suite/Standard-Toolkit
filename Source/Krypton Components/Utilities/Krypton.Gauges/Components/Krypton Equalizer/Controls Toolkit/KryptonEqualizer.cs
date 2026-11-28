#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Gauges;

/// <summary>
/// Multi-band spectrum visualizer with interactive graphic-EQ gain thumbs.
/// Band levels are set manually or read from the default recording device via an FFT.
/// </summary>
/// <remarks>
/// Capture updates spectrum levels only. Gain thumbs stay under application or user control.
/// The control does not apply DSP to system audio.
/// Capture does not start in the designer, and it stops when the handle is destroyed.
/// </remarks>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonEqualizer), "ToolboxBitmaps.KryptonEqualizer.bmp")]
[DefaultEvent(nameof(GainChanged))]
[DefaultProperty(nameof(Values))]
[DesignerCategory(@"code")]
[Designer("Krypton.Gauges.KryptonEqualizerDesigner, " + KryptonWinFormsDesignerSdk.AssemblyName)]
[Description(@"Multi-band equalizer spectrum display with interactive gain thumbs.")]
public partial class KryptonEqualizer : VisualSimpleBase
{
    #region Instance Fields

    private const int BallisticsInterval = 30;

    private readonly ViewDrawEqualizer _drawEqualizer;
    private readonly System.Windows.Forms.Timer _ballisticsTimer;
    private readonly List<double> _displayed = new List<double>();
    private readonly List<double> _peaks = new List<double>();
    private readonly List<int> _peakHoldRemaining = new List<int>();
    private EqualizerCapture? _capture;
    private int _captureGeneration;
    private int _focusBand;
    private int _dragBand = -1;
    private bool _disposed;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when one or more spectrum band levels change.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when spectrum band levels change.")]
    public event EventHandler<EqualizerLevelsChangedEventArgs>? LevelsChanged;

    /// <summary>
    /// Occurs when an interactive or assigned band gain changes.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when a band gain changes.")]
    public event EventHandler<EqualizerGainChangedEventArgs>? GainChanged;

    /// <summary>
    /// Occurs when the default recording device cannot be opened.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when capture cannot open the default recording device.")]
    public event EventHandler<EqualizerCaptureFailedEventArgs>? CaptureFailed;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonEqualizer"/> class.
    /// </summary>
    public KryptonEqualizer()
    {
        InitializeComponent();
        SetStyle(ControlStyles.Selectable, true);
        TabStop = true;

        StateCommon = new PaletteEqualizerRedirect(Redirector, NeedPaintDelegate);
        StateDisabled = new PaletteEqualizerStates(StateCommon, NeedPaintDelegate);
        StateNormal = new PaletteEqualizerStates(StateCommon, NeedPaintDelegate);
        Values = new EqualizerValues(this);
        SyncDisplayBuffers();
        _drawEqualizer = new ViewDrawEqualizer(this, StateCommon.Back)
        {
            Enabled = Enabled
        };
        ViewManager = new ViewManager(this, _drawEqualizer);

        _ballisticsTimer = new System.Windows.Forms.Timer
        {
            Interval = BallisticsInterval
        };
        _ballisticsTimer.Tick += OnBallisticsTick;
        ApplyInteractiveChrome();
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the background style drawn behind the equalizer face.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Background style drawn behind the equalizer face.")]
    [DefaultValue(PaletteBackStyle.PanelClient)]
    public PaletteBackStyle PanelBackStyle
    {
        get => StateCommon.BackStyle;
        set
        {
            if (StateCommon.BackStyle != value)
            {
                StateCommon.BackStyle = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializePanelBackStyle() => PanelBackStyle != PaletteBackStyle.PanelClient;

    private void ResetPanelBackStyle() => PanelBackStyle = PaletteBackStyle.PanelClient;

    /// <summary>
    /// Gets the common equalizer appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Common equalizer colours. Other states inherit these.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteEqualizerRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets the equalizer appearance used while the control is disabled.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Equalizer colours used while the control is disabled.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteEqualizerStates StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets the equalizer appearance used while the control is enabled.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Equalizer colours used while the control is enabled.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteEqualizerStates StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets the band and input settings.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Bands, input mode, peak hold, and interaction.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public EqualizerValues Values { get; }

    private bool ShouldSerializeValues() => !Values.IsDefault;

    private void ResetValues() => Values.Reset();

    /// <summary>
    /// Sets every band level from <paramref name="levels"/> (0…1). Ignored while capturing.
    /// </summary>
    /// <param name="levels">Normalized levels; excess entries are ignored.</param>
    public void SetLevels(double[] levels)
    {
        if (levels == null || Values.InputMode == EqualizerInputMode.Capture)
        {
            return;
        }

        int count = Math.Min(levels.Length, Values.Bands.Count);
        for (int i = 0; i < count; i++)
        {
            Values.Bands[i].Level = levels[i];
        }
    }

    /// <summary>
    /// Sets every band level from <paramref name="levels"/> (0…1). Ignored while capturing.
    /// </summary>
    /// <param name="levels">Normalized levels; excess entries are ignored.</param>
    public void SetLevels(float[] levels)
    {
        if (levels == null)
        {
            return;
        }

        var copy = new double[levels.Length];
        for (int i = 0; i < levels.Length; i++)
        {
            copy[i] = levels[i];
        }

        SetLevels(copy);
    }

    /// <summary>
    /// Copies the current target band levels into a new array.
    /// </summary>
    public double[] GetLevels()
    {
        var levels = new double[Values.Bands.Count];
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i] = Values.Bands[i].Level;
        }

        return levels;
    }

    /// <summary>
    /// Copies the current band gains into a new array.
    /// </summary>
    public double[] GetGains()
    {
        var gains = new double[Values.Bands.Count];
        for (int i = 0; i < gains.Length; i++)
        {
            gains[i] = Values.Bands[i].Gain;
        }

        return gains;
    }

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override Size DefaultSize => new Size(360, 180);

    /// <inheritdoc />
    protected override AccessibleObject CreateAccessibilityInstance() => new EqualizerAccessibleObject(this);

    /// <inheritdoc />
    protected override void OnEnabledChanged(EventArgs e)
    {
        _drawEqualizer.Enabled = Enabled;
        PerformNeedPaint(true);
        base.OnEnabledChanged(e);
    }

    /// <inheritdoc />
    protected override void OnFontChanged(EventArgs e)
    {
        PerformNeedPaint(true);
        base.OnFontChanged(e);
    }

    /// <inheritdoc />
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        if (Values.InputMode == EqualizerInputMode.Capture && !IsDesignHost)
        {
            StartCapture();
        }
    }

    /// <inheritdoc />
    protected override void OnHandleDestroyed(EventArgs e)
    {
        StopCapture();
        base.OnHandleDestroyed(e);
    }

    /// <inheritdoc />
    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        if (!Values.Interactive || !Enabled || e.Button != MouseButtons.Left)
        {
            return;
        }

        int band = HitTestBand(e.Location);
        if (band < 0)
        {
            return;
        }

        _focusBand = band;
        _dragBand = band;
        Capture = true;
        Focus();
        ApplyGainFromPoint(band, e.Location);
    }

    /// <inheritdoc />
    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        if (_dragBand >= 0 && (e.Button & MouseButtons.Left) == MouseButtons.Left)
        {
            ApplyGainFromPoint(_dragBand, e.Location);
        }
    }

    /// <inheritdoc />
    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);
        if (e.Button == MouseButtons.Left)
        {
            _dragBand = -1;
            Capture = false;
        }
    }

    /// <inheritdoc />
    protected override void OnMouseWheel(MouseEventArgs e)
    {
        base.OnMouseWheel(e);
        if (!Values.Interactive || !Enabled || Values.Bands.Count == 0)
        {
            return;
        }

        double step = (Values.MaximumGainDb - Values.MinimumGainDb) / 24d;
        if (e.Delta < 0)
        {
            step = -step;
        }

        NudgeGain(_focusBand, step);
    }

    /// <inheritdoc />
    protected override bool IsInputKey(Keys keyData)
    {
        switch (keyData)
        {
            case Keys.Up:
            case Keys.Down:
            case Keys.Left:
            case Keys.Right:
            case Keys.PageUp:
            case Keys.PageDown:
            case Keys.Home:
            case Keys.End:
                return Values.Interactive;
            default:
                return base.IsInputKey(keyData);
        }
    }

    /// <inheritdoc />
    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        if (!Values.Interactive || !Enabled || Values.Bands.Count == 0)
        {
            return;
        }

        double span = Values.MaximumGainDb - Values.MinimumGainDb;
        switch (e.KeyCode)
        {
            case Keys.Left:
                _focusBand = Math.Max(0, _focusBand - 1);
                PerformNeedPaint(false);
                e.Handled = true;
                break;
            case Keys.Right:
                _focusBand = Math.Min(Values.Bands.Count - 1, _focusBand + 1);
                PerformNeedPaint(false);
                e.Handled = true;
                break;
            case Keys.Up:
                NudgeGain(_focusBand, span / 24d);
                e.Handled = true;
                break;
            case Keys.Down:
                NudgeGain(_focusBand, -span / 24d);
                e.Handled = true;
                break;
            case Keys.PageUp:
                NudgeGain(_focusBand, span / 6d);
                e.Handled = true;
                break;
            case Keys.PageDown:
                NudgeGain(_focusBand, -span / 6d);
                e.Handled = true;
                break;
            case Keys.Home:
                Values.Bands[_focusBand].Gain = Values.MinimumGainDb;
                e.Handled = true;
                break;
            case Keys.End:
                Values.Bands[_focusBand].Gain = Values.MaximumGainDb;
                e.Handled = true;
                break;
        }
    }

    /// <summary>
    /// Raises <see cref="LevelsChanged"/>.
    /// </summary>
    /// <param name="e">Event data.</param>
    protected virtual void OnLevelsChanged(EqualizerLevelsChangedEventArgs e) => LevelsChanged?.Invoke(this, e);

    /// <summary>
    /// Raises <see cref="GainChanged"/>.
    /// </summary>
    /// <param name="e">Event data.</param>
    protected virtual void OnGainChanged(EqualizerGainChangedEventArgs e) => GainChanged?.Invoke(this, e);

    /// <summary>
    /// Raises <see cref="CaptureFailed"/>.
    /// </summary>
    /// <param name="e">Event data.</param>
    protected virtual void OnCaptureFailed(EqualizerCaptureFailedEventArgs e) => CaptureFailed?.Invoke(this, e);

    #endregion

    #region Internal

    internal bool IsApplyingCapture { get; private set; }

    internal double GetDisplayedLevel(int index) =>
        index >= 0 && index < _displayed.Count ? _displayed[index] : 0d;

    internal double GetDisplayedPeak(int index) =>
        index >= 0 && index < _peaks.Count ? _peaks[index] : 0d;

    internal void OnBandLevelChanged(EqualizerBand band)
    {
        if (_disposed)
        {
            return;
        }

        if (IsDesignHost)
        {
            int index = IndexOf(band);
            if (index >= 0)
            {
                _displayed[index] = band.Level;
                _peaks[index] = band.Level;
            }

            PerformNeedPaint(false);
        }
        else
        {
            EnsureBallistics();
        }

        OnLevelsChanged(EqualizerLevelsChangedEventArgs.Empty);
    }

    internal void OnLevelsCaptured()
    {
        if (_disposed)
        {
            return;
        }

        EnsureBallistics();
        OnLevelsChanged(EqualizerLevelsChangedEventArgs.Empty);
    }

    internal void OnBandGainChanged(EqualizerBand band, double previous)
    {
        if (_disposed)
        {
            return;
        }

        PerformNeedPaint(false);
        int index = IndexOf(band);
        if (index >= 0)
        {
            OnGainChanged(new EqualizerGainChangedEventArgs(index, previous, band.Gain));
        }
    }

    internal void OnInputModeChanged()
    {
        if (Values.InputMode == EqualizerInputMode.Capture)
        {
            if (IsHandleCreated && !IsDesignHost)
            {
                StartCapture();
            }
        }
        else
        {
            StopCapture();
        }
    }

    internal void OnPeakHoldChanged()
    {
        if (!Values.PeakHold)
        {
            for (int i = 0; i < _peaks.Count; i++)
            {
                _peaks[i] = _displayed[i];
                _peakHoldRemaining[i] = 0;
            }
        }

        PerformNeedPaint(false);
    }

    internal void OnInteractiveChanged()
    {
        ApplyInteractiveChrome();
        PerformNeedPaint(false);
    }

    internal void OnBandCountChanged()
    {
        SyncDisplayBuffers();
        _focusBand = Math.Min(_focusBand, Math.Max(0, Values.Bands.Count - 1));
        PerformNeedPaint(true);
    }

    internal void StopCaptureForDispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _ballisticsTimer.Stop();
        StopCapture();
        _ballisticsTimer.Dispose();
    }

    #endregion

    #region Implementation

    private bool IsDesignHost =>
        DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime;

    private void ApplyInteractiveChrome()
    {
        SetStyle(ControlStyles.Selectable, Values.Interactive);
        TabStop = Values.Interactive;
    }

    private void SyncDisplayBuffers()
    {
        int count = Values.Bands.Count;
        while (_displayed.Count < count)
        {
            _displayed.Add(0d);
            _peaks.Add(0d);
            _peakHoldRemaining.Add(0);
        }

        while (_displayed.Count > count)
        {
            int last = _displayed.Count - 1;
            _displayed.RemoveAt(last);
            _peaks.RemoveAt(last);
            _peakHoldRemaining.RemoveAt(last);
        }

        for (int i = 0; i < count; i++)
        {
            _displayed[i] = Values.Bands[i].Level;
            _peaks[i] = Values.Bands[i].Level;
            _peakHoldRemaining[i] = 0;
        }
    }

    private int IndexOf(EqualizerBand band)
    {
        for (int i = 0; i < Values.Bands.Count; i++)
        {
            if (ReferenceEquals(Values.Bands[i], band))
            {
                return i;
            }
        }

        return -1;
    }

    private void EnsureBallistics()
    {
        if (!_disposed && !_ballisticsTimer.Enabled)
        {
            _ballisticsTimer.Start();
        }
    }

    private void OnBallisticsTick(object? sender, EventArgs e)
    {
        if (_disposed)
        {
            _ballisticsTimer.Stop();
            return;
        }

        bool settled = true;
        int count = Math.Min(Values.Bands.Count, _displayed.Count);
        for (int i = 0; i < count; i++)
        {
            double target = Values.Bands[i].Level;
            int tau = _displayed[i] < target ? Values.AttackMilliseconds : Values.ReleaseMilliseconds;
            _displayed[i] = Ease(_displayed[i], target, tau);
            if (Values.PeakHold)
            {
                if (_displayed[i] >= _peaks[i] - 0.001d)
                {
                    _peaks[i] = _displayed[i];
                    _peakHoldRemaining[i] = Values.PeakHoldMilliseconds;
                }
                else
                {
                    _peakHoldRemaining[i] -= BallisticsInterval;
                    if (_peakHoldRemaining[i] <= 0)
                    {
                        _peaks[i] = Ease(_peaks[i], _displayed[i], Values.PeakDecayMilliseconds);
                    }
                }
            }
            else
            {
                _peaks[i] = _displayed[i];
            }

            if (Math.Abs(_displayed[i] - target) >= 0.01d || Math.Abs(_peaks[i] - _displayed[i]) >= 0.01d)
            {
                settled = false;
            }
        }

        PerformNeedPaint(false);
        if (settled && _capture == null)
        {
            _ballisticsTimer.Stop();
        }
    }

    private static double Ease(double current, double target, int milliseconds)
    {
        if (milliseconds <= 0)
        {
            return target;
        }

        double alpha = 1d - Math.Exp(-BallisticsInterval / (double)milliseconds);
        double next = current + ((target - current) * alpha);
        return Math.Abs(target - next) < 0.01d ? target : next;
    }

    private void StartCapture()
    {
        StopCapture();
        if (_disposed || IsDesignHost)
        {
            return;
        }

        _capture = new EqualizerCapture(this, ReadCaptureSample, OnCapturedLevels, OnCapturedError);
        EnsureBallistics();
    }

    private void StopCapture()
    {
        _captureGeneration++;
        EqualizerCapture? capture = _capture;
        _capture = null;
        capture?.Dispose();
    }

    private EqualizerCaptureSample ReadCaptureSample()
    {
        if (_disposed)
        {
            return new EqualizerCaptureSample(-1, VuMeterChannel.Mix, EqualizerScale.DefaultCalibrationOffsetDb, Array.Empty<double>());
        }

        return new EqualizerCaptureSample(_captureGeneration, Values.Channel, Values.CalibrationOffsetDb, Values.CopyFrequencies());
    }

    private void OnCapturedLevels(int generation, double[] levels)
    {
        if (_disposed || IsDisposed || generation != _captureGeneration || Values.InputMode != EqualizerInputMode.Capture)
        {
            return;
        }

        IsApplyingCapture = true;
        try
        {
            Values.ApplyCapturedLevels(levels);
        }
        finally
        {
            IsApplyingCapture = false;
        }
    }

    private void OnCapturedError(Exception error)
    {
        if (_disposed || IsDisposed)
        {
            return;
        }

        if (Values.InputMode == EqualizerInputMode.Capture)
        {
            Values.InputMode = EqualizerInputMode.Manual;
        }
        else
        {
            StopCapture();
        }

        OnCaptureFailed(new EqualizerCaptureFailedEventArgs(error.Message, error));
    }

    private void NudgeGain(int band, double delta)
    {
        if (band < 0 || band >= Values.Bands.Count)
        {
            return;
        }

        Values.Bands[band].Gain = Values.Bands[band].Gain + delta;
    }

    private void ApplyGainFromPoint(int band, Point location)
    {
        if (band < 0 || band >= Values.Bands.Count)
        {
            return;
        }

        Rectangle plot = GetPlotBounds();
        if (plot.Width < 2 || plot.Height < 2)
        {
            return;
        }

        VisualOrientation orientation = Values.Orientation;
        double position;
        switch (orientation)
        {
            case VisualOrientation.Bottom:
                position = 1d - ((location.Y - plot.Top) / (double)plot.Height);
                break;
            case VisualOrientation.Left:
                position = (location.X - plot.Left) / (double)plot.Width;
                break;
            case VisualOrientation.Right:
                position = 1d - ((location.X - plot.Left) / (double)plot.Width);
                break;
            default:
                position = 1d - ((location.Y - plot.Top) / (double)plot.Height);
                break;
        }

        if (position < 0d)
        {
            position = 0d;
        }
        else if (position > 1d)
        {
            position = 1d;
        }

        double gain = Values.MinimumGainDb + (position * (Values.MaximumGainDb - Values.MinimumGainDb));
        Values.Bands[band].Gain = gain;
    }

    private int HitTestBand(Point location)
    {
        Rectangle plot = GetPlotBounds();
        if (!plot.Contains(location) || Values.Bands.Count == 0)
        {
            return -1;
        }

        bool vertical = Values.Orientation == VisualOrientation.Top || Values.Orientation == VisualOrientation.Bottom;
        if (vertical)
        {
            float slot = plot.Width / (float)Values.Bands.Count;
            int index = (int)((location.X - plot.Left) / slot);
            return index < 0 ? -1 : Math.Min(index, Values.Bands.Count - 1);
        }

        float row = plot.Height / (float)Values.Bands.Count;
        int band = (int)((location.Y - plot.Top) / row);
        return band < 0 ? -1 : Math.Min(band, Values.Bands.Count - 1);
    }

    private Rectangle GetPlotBounds()
    {
        Rectangle bounds = ClientRectangle;
        bounds.Inflate(-4, -4);
        int labelSpace = Values.ShowBandLabels ? 16 : 0;
        bool vertical = Values.Orientation == VisualOrientation.Top || Values.Orientation == VisualOrientation.Bottom;
        if (vertical)
        {
            bounds.Height -= labelSpace;
            if (Values.Orientation == VisualOrientation.Bottom)
            {
                bounds.Y += labelSpace;
            }
        }
        else
        {
            bounds.Width -= labelSpace;
            if (Values.Orientation == VisualOrientation.Right)
            {
                bounds.X += labelSpace;
            }
        }

        return bounds;
    }

    #endregion

    #region Accessibility

    private sealed class EqualizerAccessibleObject : ControlAccessibleObject
    {
        private readonly KryptonEqualizer _owner;

        public EqualizerAccessibleObject(KryptonEqualizer owner)
            : base(owner) =>
            _owner = owner;

        public override string? Name
        {
            get
            {
                if (!string.IsNullOrEmpty(_owner.AccessibleName))
                {
                    return _owner.AccessibleName;
                }

                return base.Name ?? @"Equalizer";
            }
        }

        public override string? Value
        {
            get
            {
                if (_owner.Values.Bands.Count == 0)
                {
                    return null;
                }

                int index = Math.Max(0, Math.Min(_owner._focusBand, _owner.Values.Bands.Count - 1));
                EqualizerBand band = _owner.Values.Bands[index];
                return band.DisplayText + " " + band.Gain.ToString("0.0", CultureInfo.CurrentCulture) + " dB";
            }
        }

        public override AccessibleRole Role
        {
            get
            {
                if (_owner.AccessibleRole != AccessibleRole.Default)
                {
                    return _owner.AccessibleRole;
                }

                return AccessibleRole.Slider;
            }
        }
    }

    #endregion
}

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
/// Displays a VU level as a classic needle meter or a segmented bar.
/// The level is set on <see cref="Values"/> or read from the default recording device.
/// </summary>
/// <remarks>
/// One control is one meter. Place two controls side by side for a stereo pair and point each
/// <see cref="VuMeterValues.Channel"/> at a different channel while capturing.
/// Capture does not start in the designer, and it stops when the handle is destroyed.
/// </remarks>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonVuMeter), "ToolboxBitmaps.KryptonVuMeter.bmp")]
[DefaultEvent(nameof(ValueChanged))]
[DefaultProperty(nameof(Values))]
[DefaultBindingProperty(nameof(Value))]
[DesignerCategory(@"code")]
[Designer("Krypton.Gauges.KryptonVuMeterDesigner, " + KryptonWinFormsDesignerSdk.AssemblyName)]
[Description(@"Displays a VU meter as an analog needle or a segmented bar.")]
public partial class KryptonVuMeter : VisualSimpleBase
{
    #region Instance Fields

    private const int BallisticsInterval = 30;

    private readonly ViewDrawVuMeter _drawMeter;
    private readonly System.Windows.Forms.Timer _ballisticsTimer;
    private VuMeterCapture? _capture;
    private int _captureGeneration;
    private int _peakHoldRemaining;
    private double _displayed = -20d;
    private double _peak = -20d;
    private bool _disposed;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the target <see cref="Value"/> changes.
    /// The painted indicator eases toward the new value and does not raise this event on each frame.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the target VU level changes.")]
    public event EventHandler<VuMeterValueChangedEventArgs>? ValueChanged;

    /// <summary>
    /// Occurs when the default recording device cannot be opened. The meter keeps its last level.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when capture cannot open the default recording device.")]
    public event EventHandler<VuMeterCaptureFailedEventArgs>? CaptureFailed;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonVuMeter"/> class.
    /// </summary>
    public KryptonVuMeter()
    {
        InitializeComponent();
        SetStyle(ControlStyles.Selectable, false);
        TabStop = false;

        StateCommon = new PaletteVuMeterRedirect(Redirector, NeedPaintDelegate);
        StateDisabled = new PaletteVuMeterStates(StateCommon, NeedPaintDelegate);
        StateNormal = new PaletteVuMeterStates(StateCommon, NeedPaintDelegate);
        Values = new VuMeterValues(this);
        _drawMeter = new ViewDrawVuMeter(this, StateCommon.Back)
        {
            Enabled = Enabled
        };
        ViewManager = new ViewManager(this, _drawMeter);

        _ballisticsTimer = new System.Windows.Forms.Timer
        {
            Interval = BallisticsInterval
        };
        _ballisticsTimer.Tick += OnBallisticsTick;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the background style drawn behind the meter face.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Background style drawn behind the meter face.")]
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
    /// Gets the common meter appearance. Other states inherit these colours, which themselves inherit the palette.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Common meter colours. Other states inherit these, and unset colours inherit the palette.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteVuMeterRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets the meter appearance used while the control is disabled.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Meter colours used while the control is disabled.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteVuMeterStates StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets the meter appearance used while the control is enabled.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Meter colours used while the control is enabled.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteVuMeterStates StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets the level and input settings.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Level, input mode, and peak hold.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public VuMeterValues Values { get; }

    private bool ShouldSerializeValues() => !Values.IsDefault;

    private void ResetValues() => Values.Reset();

    /// <summary>
    /// Gets or sets the target level in VU. This is the bindable surface for <see cref="VuMeterValues.Value"/>.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double Value
    {
        get => Values.Value;
        set => Values.Value = value;
    }

    /// <summary>
    /// Gets the level currently painted. This eases toward <see cref="Value"/>.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double DisplayedValue => _displayed;

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override Size DefaultSize => new Size(240, 140);

    /// <inheritdoc />
    protected override AccessibleObject CreateAccessibilityInstance() => new VuMeterAccessibleObject(this);

    /// <inheritdoc />
    protected override void OnEnabledChanged(EventArgs e)
    {
        _drawMeter.Enabled = Enabled;
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
        if (Values.InputMode == VuMeterInputMode.Capture && !IsDesignHost)
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

    /// <summary>
    /// Raises <see cref="ValueChanged"/>.
    /// </summary>
    /// <param name="e">Event data containing the new target level.</param>
    protected virtual void OnValueChanged(VuMeterValueChangedEventArgs e) => ValueChanged?.Invoke(this, e);

    /// <summary>
    /// Raises <see cref="CaptureFailed"/>.
    /// </summary>
    /// <param name="e">Event data describing why capture stopped.</param>
    protected virtual void OnCaptureFailed(VuMeterCaptureFailedEventArgs e) => CaptureFailed?.Invoke(this, e);

    #endregion

    #region Internal

    internal bool IsApplyingCapture { get; private set; }

    internal double DisplayedLevel => _displayed;

    internal double PeakLevel => _peak;

    internal void OnTargetChanged()
    {
        if (_disposed)
        {
            return;
        }

        if (IsDesignHost)
        {
            _displayed = Values.Value;
            _peak = Values.Value;
            PerformNeedPaint(false);
        }
        else
        {
            EnsureBallistics();
        }

        OnValueChanged(new VuMeterValueChangedEventArgs(Values.Value));
    }

    internal void OnInputModeChanged()
    {
        if (Values.InputMode == VuMeterInputMode.Capture)
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
            _peak = _displayed;
            _peakHoldRemaining = 0;
        }

        PerformNeedPaint(false);
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

        double target = Values.Value;
        int tau = _displayed < target ? Values.AttackMilliseconds : Values.ReleaseMilliseconds;
        _displayed = Ease(_displayed, target, tau);
        if (Values.PeakHold)
        {
            if (_displayed >= _peak - 0.001d)
            {
                _peak = _displayed;
                _peakHoldRemaining = Values.PeakHoldMilliseconds;
            }
            else
            {
                _peakHoldRemaining -= BallisticsInterval;
                if (_peakHoldRemaining <= 0)
                {
                    _peak = Ease(_peak, _displayed, Values.PeakDecayMilliseconds);
                }
            }
        }
        else
        {
            _peak = _displayed;
        }

        PerformNeedPaint(false);
        bool settled = Math.Abs(_displayed - target) < 0.02d && Math.Abs(_peak - _displayed) < 0.02d;
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
        return Math.Abs(target - next) < 0.02d ? target : next;
    }

    private void StartCapture()
    {
        StopCapture();
        if (_disposed || IsDesignHost)
        {
            return;
        }

        _capture = new VuMeterCapture(this, ReadCaptureSample, OnCapturedLevel, OnCapturedError);
        EnsureBallistics();
    }

    private void StopCapture()
    {
        _captureGeneration++;
        VuMeterCapture? capture = _capture;
        _capture = null;
        capture?.Dispose();
    }

    private VuMeterCaptureSample ReadCaptureSample()
    {
        if (_disposed)
        {
            return new VuMeterCaptureSample(-1, VuMeterChannel.Left, VuMeterScale.DefaultCalibrationOffsetDb);
        }

        return new VuMeterCaptureSample(_captureGeneration, Values.Channel, Values.CalibrationOffsetDb);
    }

    private void OnCapturedLevel(int generation, double vu)
    {
        if (_disposed || IsDisposed || generation != _captureGeneration || Values.InputMode != VuMeterInputMode.Capture)
        {
            return;
        }

        IsApplyingCapture = true;
        try
        {
            Values.SetCapturedValue(vu);
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

        if (Values.InputMode == VuMeterInputMode.Capture)
        {
            Values.InputMode = VuMeterInputMode.Manual;
        }
        else
        {
            StopCapture();
        }

        OnCaptureFailed(new VuMeterCaptureFailedEventArgs(error.Message, error));
    }

    #endregion

    #region Accessibility

    private sealed class VuMeterAccessibleObject : ControlAccessibleObject
    {
        private readonly KryptonVuMeter _owner;

        public VuMeterAccessibleObject(KryptonVuMeter owner)
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

                return base.Name ?? @"VU meter";
            }
        }

        public override string? Value => _owner.DisplayedValue.ToString("0.0", CultureInfo.CurrentCulture) + " VU";

        public override AccessibleRole Role
        {
            get
            {
                if (_owner.AccessibleRole != AccessibleRole.Default)
                {
                    return _owner.AccessibleRole;
                }

                return AccessibleRole.Graphic;
            }
        }
    }

    #endregion
}

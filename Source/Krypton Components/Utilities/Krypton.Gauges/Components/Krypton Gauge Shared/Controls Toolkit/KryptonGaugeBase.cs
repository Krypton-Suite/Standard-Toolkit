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
/// Non-toolbox base for analogue gauges that share value, palette, animation, and interactive input behaviour.
/// </summary>
[ToolboxItem(false)]
[DefaultEvent(nameof(ValueChanged))]
[DefaultProperty(nameof(Values))]
[DefaultBindingProperty(nameof(Value))]
[DesignerCategory(@"code")]
public abstract class KryptonGaugeBase : VisualSimpleBase
{
    #region Instance Fields

    private const int AnimationInterval = 30;

    private readonly System.Windows.Forms.Timer _animationTimer;
    private double _displayed;
    private double _peak;
    private bool _dragging;
    private bool _disposed;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the target <see cref="Value"/> changes.
    /// The painted indicator may ease toward the new value and does not raise this event on each animation frame.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the target gauge value changes.")]
    public event EventHandler<GaugeValueChangedEventArgs>? ValueChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonGaugeBase"/> class.
    /// </summary>
    protected KryptonGaugeBase()
    {
        SetStyle(ControlStyles.Selectable, false);
        TabStop = false;

        StateCommon = new PaletteGaugeRedirect(Redirector, NeedPaintDelegate);
        StateDisabled = new PaletteGaugeStates(StateCommon, NeedPaintDelegate);
        StateNormal = new PaletteGaugeStates(StateCommon, NeedPaintDelegate);

        _animationTimer = new System.Windows.Forms.Timer
        {
            Interval = AnimationInterval
        };
        _animationTimer.Tick += OnAnimationTick;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the background style drawn behind the gauge face.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Background style drawn behind the gauge face.")]
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
    /// Gets the common gauge appearance. Other states inherit these colours, which themselves inherit the palette.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Common gauge colours. Other states inherit these, and unset colours inherit the palette.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteGaugeRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets the gauge appearance used while the control is disabled.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Gauge colours used while the control is disabled.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteGaugeStates StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets the gauge appearance used while the control is enabled.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Gauge colours used while the control is enabled.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteGaugeStates StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets the shared value, range, and input settings.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Value, ranges, animation, and input mode.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public abstract GaugeValues Values { get; }

    /// <summary>
    /// Gets or sets the target value. This is the bindable surface for <see cref="GaugeValues.Value"/>.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double Value
    {
        get => Values.Value;
        set => Values.Value = value;
    }

    /// <summary>
    /// Gets the value currently painted. This eases toward <see cref="Value"/> when animation is enabled.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double DisplayedValue => _displayed;

    /// <summary>
    /// Gets the recent peak value used by faces that draw a peak marker.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double PeakValue => _peak;

    #endregion

    #region Protected

    /// <summary>
    /// Gets the palette colours for the current enabled state.
    /// </summary>
    protected PaletteElementColor ActiveFace => Enabled ? StateNormal.Face : StateDisabled.Face;

    /// <summary>
    /// Gets the active tick colour storage.
    /// </summary>
    protected PaletteElementColor ActiveTick => Enabled ? StateNormal.Tick : StateDisabled.Tick;

    /// <summary>
    /// Gets the active needle colour storage.
    /// </summary>
    protected PaletteElementColor ActiveNeedle => Enabled ? StateNormal.Needle : StateDisabled.Needle;

    /// <summary>
    /// Gets the active fill colour storage.
    /// </summary>
    protected PaletteElementColor ActiveFill => Enabled ? StateNormal.Fill : StateDisabled.Fill;

    /// <summary>
    /// Gets the active warning-range colour storage.
    /// </summary>
    protected PaletteElementColor ActiveRangeWarning => Enabled ? StateNormal.RangeWarning : StateDisabled.RangeWarning;

    /// <summary>
    /// Gets the active error-range colour storage.
    /// </summary>
    protected PaletteElementColor ActiveRangeError => Enabled ? StateNormal.RangeError : StateDisabled.RangeError;

    /// <summary>
    /// Gets the active cap colour storage.
    /// </summary>
    protected PaletteElementColor ActiveCap => Enabled ? StateNormal.Cap : StateDisabled.Cap;

    /// <summary>
    /// Gets the active peak colour storage.
    /// </summary>
    protected PaletteElementColor ActivePeak => Enabled ? StateNormal.Peak : StateDisabled.Peak;

    /// <summary>
    /// Gets the active unlit-segment colour storage.
    /// </summary>
    protected PaletteElementColor ActiveSegmentOff => Enabled ? StateNormal.SegmentOff : StateDisabled.SegmentOff;

    /// <summary>
    /// Gets the palette state used when resolving colours.
    /// </summary>
    protected PaletteState ActivePaletteState => Enabled ? PaletteState.Normal : PaletteState.Disabled;

    /// <inheritdoc />
    protected override void OnEnabledChanged(EventArgs e)
    {
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
    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        if (!CanInteract || e.Button != MouseButtons.Left)
        {
            return;
        }

        if (TryMapPointToValue(e.Location, out double value))
        {
            _dragging = true;
            Capture = true;
            Focus();
            Values.Value = value;
        }
    }

    /// <inheritdoc />
    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        if (_dragging && CanInteract && TryMapPointToValue(e.Location, out double value))
        {
            Values.Value = value;
        }
    }

    /// <inheritdoc />
    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);
        if (_dragging)
        {
            _dragging = false;
            Capture = false;
        }
    }

    /// <inheritdoc />
    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        if (!CanInteract)
        {
            return;
        }

        double next = Values.Value;
        switch (e.KeyCode)
        {
            case Keys.Left:
            case Keys.Down:
                next -= Values.SmallChange;
                break;
            case Keys.Right:
            case Keys.Up:
                next += Values.SmallChange;
                break;
            case Keys.PageDown:
                next -= Values.LargeChange;
                break;
            case Keys.PageUp:
                next += Values.LargeChange;
                break;
            case Keys.Home:
                next = Values.Minimum;
                break;
            case Keys.End:
                next = Values.Maximum;
                break;
            default:
                return;
        }

        Values.Value = next;
        e.Handled = true;
    }

    /// <inheritdoc />
    protected override bool IsInputKey(Keys keyData)
    {
        if (CanInteract)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                case Keys.PageUp:
                case Keys.PageDown:
                case Keys.Home:
                case Keys.End:
                    return true;
            }
        }

        return base.IsInputKey(keyData);
    }

    /// <summary>
    /// Raises <see cref="ValueChanged"/>.
    /// </summary>
    /// <param name="e">Event data.</param>
    protected virtual void OnValueChanged(GaugeValueChangedEventArgs e) => ValueChanged?.Invoke(this, e);

    /// <summary>
    /// Maps a client point to a gauge value when interactive input is active.
    /// </summary>
    /// <param name="point">Client coordinates.</param>
    /// <param name="value">Mapped value.</param>
    /// <returns>true when the point maps to a value.</returns>
    protected abstract bool TryMapPointToValue(Point point, out double value);

    /// <summary>
    /// Called after the view manager has been created so derived types can finish construction.
    /// </summary>
    /// <param name="initialValue">Initial displayed value.</param>
    protected void InitializeDisplayed(double initialValue)
    {
        _displayed = initialValue;
        _peak = initialValue;
    }

    #endregion

    #region Internal

    internal bool IsDesignHost =>
        DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime;

    internal Color ResolveFaceFill() => ActiveFace.GetElementColor1(ActivePaletteState);

    internal Color ResolveFaceBorder() => ActiveFace.GetElementColor2(ActivePaletteState);

    internal Color ResolveTick() => ActiveTick.GetElementColor1(ActivePaletteState);

    internal Color ResolveNeedle() => ActiveNeedle.GetElementColor1(ActivePaletteState);

    internal Color ResolveFill() => ActiveFill.GetElementColor1(ActivePaletteState);

    internal Color ResolveCap() => ActiveCap.GetElementColor1(ActivePaletteState);

    internal Color ResolvePeak() => ActivePeak.GetElementColor1(ActivePaletteState);

    internal Color ResolveSegmentOff() => ActiveSegmentOff.GetElementColor1(ActivePaletteState);

    internal Color ResolveRangeColor(GaugeRange range)
    {
        if (!range.Color.IsEmpty)
        {
            return range.Color;
        }

        switch (range.Severity)
        {
            case GaugeRangeSeverity.Warning:
                return ActiveRangeWarning.GetElementColor1(ActivePaletteState);
            case GaugeRangeSeverity.Error:
                return ActiveRangeError.GetElementColor1(ActivePaletteState);
            default:
                return ResolveFill();
        }
    }

    internal void OnTargetChanged(double oldValue, double newValue)
    {
        if (_disposed)
        {
            return;
        }

        if (newValue >= _peak)
        {
            _peak = newValue;
        }

        if (!Values.Animate || IsDesignHost || Values.AnimationMilliseconds <= 0)
        {
            _displayed = newValue;
            PerformNeedPaint(false);
        }
        else
        {
            EnsureAnimation();
        }

        OnValueChanged(new GaugeValueChangedEventArgs(oldValue, newValue));
    }

    internal void OnInputModeChanged()
    {
        bool interactive = Values.InputMode == GaugeInputMode.Interactive;
        SetStyle(ControlStyles.Selectable, interactive);
        TabStop = interactive;
        if (!interactive && _dragging)
        {
            _dragging = false;
            Capture = false;
        }

        PerformNeedPaint(false);
    }

    internal void OnAnimateChanged()
    {
        if (!Values.Animate)
        {
            _animationTimer.Stop();
            _displayed = Values.Value;
            PerformNeedPaint(false);
        }
        else
        {
            EnsureAnimation();
        }
    }

    internal void StopAnimationForDispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _animationTimer.Stop();
        _animationTimer.Dispose();
    }

    #endregion

    #region Implementation

    private bool CanInteract => Enabled && !IsDesignHost && Values.InputMode == GaugeInputMode.Interactive;

    private void EnsureAnimation()
    {
        if (!_disposed && !_animationTimer.Enabled)
        {
            _animationTimer.Start();
        }
    }

    private void OnAnimationTick(object? sender, EventArgs e)
    {
        if (_disposed)
        {
            _animationTimer.Stop();
            return;
        }

        double target = Values.Value;
        int tau = Values.AnimationMilliseconds;
        _displayed = Ease(_displayed, target, tau);
        if (target >= _peak - 0.0001d)
        {
            _peak = _displayed;
        }
        else if (_peak > _displayed)
        {
            _peak = Ease(_peak, _displayed, Math.Max(tau, 400));
        }

        PerformNeedPaint(false);
        if (Math.Abs(_displayed - target) < 0.02d && Math.Abs(_peak - Math.Max(_displayed, target)) < 0.02d)
        {
            _displayed = target;
            _animationTimer.Stop();
        }
    }

    private static double Ease(double current, double target, int milliseconds)
    {
        if (milliseconds <= 0)
        {
            return target;
        }

        double alpha = 1d - Math.Exp(-AnimationInterval / (double)milliseconds);
        double next = current + ((target - current) * alpha);
        return Math.Abs(target - next) < 0.02d ? target : next;
    }

    #endregion
}

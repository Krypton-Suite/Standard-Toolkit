#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Allow the user to select a value by rotating a knob.
/// </summary>
[ToolboxItem(true)]
[DefaultEvent(nameof(KnobValueChanged))]
[DefaultProperty(nameof(Value))]
[DesignerCategory(@"code")]
[Description(@"Allow the user to select a value by rotating a knob.")]
public partial class KryptonKnob : VisualSimpleBase
{
    #region Instance Fields
    private readonly ViewDrawKnob _drawKnob;
    private readonly KnobAppearanceValues _appearance;
    private readonly KnobBehaviorValues _behavior;
    private readonly PaletteKnobStatesOverride _overrideNormal;
    private readonly PaletteKnobFaceStatesOverride _overrideTracking;
    private readonly PaletteKnobFaceStatesOverride _overridePressed;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the value of the Value property changes.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the value of the Value property changes.")]
    public event KnobValueChangedEventHandler? KnobValueChanged;

    /// <summary>
    /// Occurs when the value changes because of a user action.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the value changes because of a user action.")]
    public event EventHandler? Scroll;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonKnob class.
    /// </summary>
    public KryptonKnob()
    {
        InitializeComponent();

        StateCommon = new PaletteKnobRedirect(Redirector, NeedPaintDelegate);
        OverrideFocus = new PaletteKnobRedirect(Redirector, NeedPaintDelegate);
        StateDisabled = new PaletteKnobStates(StateCommon, NeedPaintDelegate);
        StateNormal = new PaletteKnobStates(StateCommon, NeedPaintDelegate);
        StateTracking = new PaletteKnobFaceStates(StateCommon, NeedPaintDelegate);
        StatePressed = new PaletteKnobFaceStates(StateCommon, NeedPaintDelegate);

        _overrideNormal = new PaletteKnobStatesOverride(OverrideFocus, StateNormal, PaletteState.FocusOverride);
        _overrideTracking = new PaletteKnobFaceStatesOverride(OverrideFocus, StateTracking, PaletteState.FocusOverride);
        _overridePressed = new PaletteKnobFaceStatesOverride(OverrideFocus, StatePressed, PaletteState.FocusOverride);

        _drawKnob = new ViewDrawKnob(_overrideNormal, StateDisabled, _overrideTracking, _overridePressed, NeedPaintDelegate)
        {
            IgnoreRender = false,
            // Keep the view element in step with the control so the disabled palette is used
            // even when Enabled was set before this view existed.
            Enabled = Enabled
        };
        _drawKnob.ValueChanged += OnDrawValueChanged;
        _drawKnob.Scroll += OnDrawScroll;

        _appearance = new KnobAppearanceValues(this);
        _behavior = new KnobBehaviorValues(this);

        ViewManager = new ViewManager(this, _drawKnob);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets access to the common knob appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common knob appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteKnobRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the knob appearance when it has focus.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining knob appearance when it has focus.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteKnobRedirect OverrideFocus { get; }

    private bool ShouldSerializeOverrideFocus() => !OverrideFocus.IsDefault;

    /// <summary>
    /// Gets access to the disabled knob appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled knob appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteKnobStates StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal knob appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal knob appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteKnobStates StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the tracking knob appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tracking knob face and indicator appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteKnobFaceStates StateTracking { get; }

    private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

    /// <summary>
    /// Gets access to the pressed knob appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed knob face and indicator appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteKnobFaceStates StatePressed { get; }

    private bool ShouldSerializeStatePressed() => !StatePressed.IsDefault;

    /// <summary>
    /// Gets access to the knob appearance settings.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Knob appearance settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KnobAppearanceValues Appearance => _appearance;

    private bool ShouldSerializeAppearance() => !Appearance.IsDefault;

    private void ResetAppearance() => Appearance.Reset();

    /// <summary>
    /// Gets access to the knob behavior settings.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Knob behavior settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KnobBehaviorValues Behavior => _behavior;

    private bool ShouldSerializeBehavior() => !Behavior.IsDefault;

    private void ResetBehavior() => Behavior.Reset();

    /// <summary>
    /// Gets or sets the background style.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PaletteBackStyle BackStyle
    {
        get => Appearance.BackStyle;
        set => Appearance.BackStyle = value;
    }

    /// <summary>
    /// Shows small scale marking.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowSmallScale
    {
        get => Appearance.ShowSmallScale;
        set => Appearance.ShowSmallScale = value;
    }

    /// <summary>
    /// Shows large scale marking.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowLargeScale
    {
        get => Appearance.ShowLargeScale;
        set => Appearance.ShowLargeScale = value;
    }

    /// <summary>
    /// Gets or sets the size of the large scale marker.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SizeLargeScaleMarker
    {
        get => Appearance.SizeLargeScaleMarker;
        set => Appearance.SizeLargeScaleMarker = value;
    }

    /// <summary>
    /// Gets or sets the size of the small scale marker.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SizeSmallScaleMarker
    {
        get => Appearance.SizeSmallScaleMarker;
        set => Appearance.SizeSmallScaleMarker = value;
    }

    /// <summary>
    /// Gets or sets the shape of the value indicator.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KnobIndicatorShape IndicatorShape
    {
        get => Appearance.IndicatorShape;
        set => Appearance.IndicatorShape = value;
    }

    /// <summary>
    /// Gets or sets the size of the value indicator in pixels.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int IndicatorSize
    {
        get => Appearance.IndicatorSize;
        set => Appearance.IndicatorSize = value;
    }

    /// <summary>
    /// Gets or sets normalized custom indicator points used when <see cref="IndicatorShape"/> is <see cref="KnobIndicatorShape.Custom"/>.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PointF[]? IndicatorCustomPoints
    {
        get => Appearance.IndicatorCustomPoints;
        set => Appearance.IndicatorCustomPoints = value;
    }

    /// <summary>
    /// Gets or sets a custom indicator path used when <see cref="IndicatorShape"/> is <see cref="KnobIndicatorShape.Custom"/>.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GraphicsPath? IndicatorCustomPath
    {
        get => Appearance.IndicatorCustomPath;
        set => Appearance.IndicatorCustomPath = value;
    }

    /// <summary>
    /// Gets or sets how the knob face is rendered.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KnobStyle KnobStyle
    {
        get => Appearance.KnobStyle;
        set => Appearance.KnobStyle = value;
    }

    /// <summary>
    /// Gets or sets the minimum value.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Minimum
    {
        get => Behavior.Minimum;
        set => Behavior.Minimum = value;
    }

    /// <summary>
    /// Gets or sets the maximum value.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Maximum
    {
        get => Behavior.Maximum;
        set => Behavior.Maximum = value;
    }

    /// <summary>
    /// Gets or sets the large change value.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int LargeChange
    {
        get => Behavior.LargeChange;
        set => Behavior.LargeChange = value;
    }

    /// <summary>
    /// Gets or sets the small change value.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SmallChange
    {
        get => Behavior.SmallChange;
        set => Behavior.SmallChange = value;
    }

    /// <summary>
    /// Gets or sets the current value.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Value
    {
        get => Behavior.Value;
        set => Behavior.Value = value;
    }

    /// <summary>
    /// Sets the minimum and maximum values for the knob.
    /// </summary>
    /// <param name="minValue">The lower limit.</param>
    /// <param name="maxValue">The upper limit.</param>
    public void SetRange(int minValue, int maxValue)
    {
        if (Minimum != minValue || Maximum != maxValue)
        {
            _drawKnob.SetRange(minValue, maxValue);
            PerformNeedPaint(true);
        }
    }

    /// <summary>
    /// Fix the control to a particular palette state.
    /// </summary>
    /// <param name="state">Palette state to fix.</param>
    public virtual void SetFixedState(PaletteState state) => _drawKnob.SetFixedState(state);

    /// <summary>
    /// Gets or sets if the control should draw the background.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool DrawBackground
    {
        get => Appearance.DrawBackground;
        set => Appearance.DrawBackground = value;
    }

    #endregion

    #region Protected
    /// <inheritdoc />
    protected override Size DefaultSize => new Size(100, 100);

    /// <inheritdoc />
    protected override bool IsInputKey(Keys key) => key switch
    {
        Keys.Up or Keys.Down or Keys.Right or Keys.Left => true,
        _ => base.IsInputKey(key)
    };

    /// <inheritdoc />
    protected override void OnGotFocus(EventArgs e)
    {
        if (!_drawKnob.IsFixed)
        {
            _overrideNormal.Apply = true;
            _overrideTracking.Apply = true;
            _overridePressed.Apply = true;
            PerformNeedPaint(true);
        }

        base.OnGotFocus(e);
    }

    /// <inheritdoc />
    protected override void OnLostFocus(EventArgs e)
    {
        if (!_drawKnob.IsFixed)
        {
            _overrideNormal.Apply = false;
            _overrideTracking.Apply = false;
            _overridePressed.Apply = false;
            PerformNeedPaint(false);
        }

        base.OnLostFocus(e);
    }

    /// <inheritdoc />
    protected override void OnEnabledChanged(EventArgs e)
    {
        _drawKnob.Enabled = Enabled;
        PerformNeedPaint(true);
        base.OnEnabledChanged(e);
    }

    /// <inheritdoc />
    protected override void OnMouseDown(MouseEventArgs e)
    {
        if (CanFocus)
        {
            Focus();
        }

        base.OnMouseDown(e);
    }

    /// <inheritdoc />
    protected virtual void OnKnobValueChanged(KnobValueChangedEventArgs e) =>
        KnobValueChanged?.Invoke(this, e);

    /// <inheritdoc />
    protected virtual void OnScroll(EventArgs e) => Scroll?.Invoke(this, e);

    #endregion

    #region Implementation
    internal ViewDrawKnob ViewDrawKnob => _drawKnob;

    private void OnDrawValueChanged(object? sender, EventArgs e)
    {
        PerformNeedPaint(true);
        OnKnobValueChanged(new KnobValueChangedEventArgs(_drawKnob.Value));
    }

    private void OnDrawScroll(object? sender, EventArgs e) => OnScroll(e);

    #endregion
}

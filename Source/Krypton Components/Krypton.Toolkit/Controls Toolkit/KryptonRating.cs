#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Allows the user to select a rating by clicking or keyboarding a strip of glyphs.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonRating), "ToolboxBitmaps.KryptonRating.bmp")]
[DefaultEvent(nameof(ValueChanged))]
[DefaultProperty(nameof(Value))]
[DefaultBindingProperty(nameof(Value))]
[Designer(typeof(KryptonRatingDesigner))]
[DesignerCategory(@"code")]
[Description(@"Allows the user to select a rating.")]
public class KryptonRating : VisualSimpleBase
{
    #region Instance Fields

    private readonly ViewDrawRating _drawRating;
    private readonly RatingValues _ratingValues;
    private bool _autoSize;
    private int _requestedDim;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the value of the Value property changes.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the value of the Value property changes.")]
    public event EventHandler? ValueChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonRating"/> class.
    /// </summary>
    public KryptonRating()
    {
        _autoSize = true;
        _requestedDim = 0;
        TabStop = true;

        _ratingValues = new RatingValues(NeedPaintDelegate);
        StateCommon = new PaletteRatingStates(null, NeedPaintDelegate);
        StateDisabled = new PaletteRatingStates(StateCommon, NeedPaintDelegate);
        StateNormal = new PaletteRatingStates(StateCommon, NeedPaintDelegate);
        StateTracking = new PaletteRatingStates(StateNormal, NeedPaintDelegate);
        _drawRating = new ViewDrawRating(this, NeedPaintDelegate)
        {
            RightToLeft = RightToLeft,
            Enabled = Enabled
        };
        _drawRating.ValueChanged += OnDrawValueChanged;
        ViewManager = new ViewManager(this, _drawRating);

        SetStyle(ControlStyles.FixedHeight, true);
        SetStyle(ControlStyles.FixedWidth, false);
        SetStyle(ControlStyles.Selectable, true);
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        BackColor = Color.Transparent;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the text associated with this control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public override string Text
    {
        get => base.Text;
        set => base.Text = value;
    }

    /// <summary>
    /// Determines the IME status of the object when selected.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ImeMode ImeMode
    {
        get => base.ImeMode;
        set => base.ImeMode = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control sizes to its glyphs.
    /// </summary>
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
    public override bool AutoSize
    {
        get => _autoSize;
        set
        {
            if (value != _autoSize)
            {
                _autoSize = value;
                ApplyFixedStyle();
                AdjustSize();
                OnAutoSizeChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Gets and sets the auto size mode.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override AutoSizeMode AutoSizeMode
    {
        get => base.AutoSizeMode;
        set => base.AutoSizeMode = value;
    }

    /// <summary>
    /// Gets or sets the current rating between 0 and <see cref="Maximum"/>.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Current rating between 0 and Maximum.")]
    [RefreshProperties(RefreshProperties.All)]
    public decimal Value
    {
        get => _drawRating.Value;
        set => _drawRating.Value = value;
    }

    private bool ShouldSerializeValue() => Value != 0m;

    private void ResetValue() => Value = 0m;

    /// <summary>
    /// Gets the hover preview rating. Equals <see cref="Value"/> when the mouse is not over the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public decimal HoverValue => _drawRating.HoverValue;

    /// <summary>
    /// Gets a value indicating whether the mouse is tracking a preview rating.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsHovering => _drawRating.IsHovering;

    /// <summary>
    /// Gets or sets the number of glyphs.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Number of rating glyphs. Default is 5.")]
    [DefaultValue(ViewDrawRating.DefaultMaximum)]
    [RefreshProperties(RefreshProperties.All)]
    public int Maximum
    {
        get => _drawRating.Maximum;
        set
        {
            if (_drawRating.Maximum != value)
            {
                _drawRating.Maximum = value;
                AdjustSize();
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets how values snap and fill.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"How the rating snaps: whole glyphs, half glyphs, or exact click position.")]
    [DefaultValue(KryptonRatingPrecision.Full)]
    public KryptonRatingPrecision Precision
    {
        get => _drawRating.Precision;
        set => _drawRating.Precision = value;
    }

    /// <summary>
    /// Gets or sets whether the user can change the rating.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"When true, hover preview is shown but clicks and keys do not change Value.")]
    [DefaultValue(false)]
    public bool ReadOnly
    {
        get => _drawRating.ReadOnly;
        set => _drawRating.ReadOnly = value;
    }

    /// <summary>
    /// Gets or sets whether clicking the current rating (or before the first glyph) clears to zero.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"When true, clicking the current rating again, or before the first glyph, clears to zero.")]
    [DefaultValue(true)]
    public bool AllowClear
    {
        get => _drawRating.AllowClear;
        set => _drawRating.AllowClear = value;
    }

    /// <summary>
    /// Gets or sets a value indicating the horizontal or vertical orientation of the glyphs.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Horizontal or vertical layout of the rating glyphs.")]
    [DefaultValue(Orientation.Horizontal)]
    public Orientation Orientation
    {
        get => _drawRating.Orientation;
        set
        {
            if (value != _drawRating.Orientation)
            {
                _drawRating.Orientation = value;
                ApplyFixedStyle();

                if (Orientation == Orientation.Horizontal)
                {
                    Width = Height;
                }
                else
                {
                    Height = Width;
                }

                if (IsHandleCreated)
                {
                    AdjustSize();
                }

                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets access to glyph size, spacing, shape, and images.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Glyph size, spacing, shape, and images.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RatingValues RatingValues => _ratingValues;

    private bool ShouldSerializeRatingValues() => !RatingValues.IsDefault;

    /// <summary>
    /// Resets the RatingValues property to its default value.
    /// </summary>
    public void ResetRatingValues() => RatingValues.Reset();

    /// <summary>
    /// Gets access to the common rating appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common rating appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRatingStates StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Resets the StateCommon property to its default value.
    /// </summary>
    public void ResetStateCommon() => StateCommon.Reset();

    /// <summary>
    /// Gets access to the disabled rating appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled rating appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRatingStates StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Resets the StateDisabled property to its default value.
    /// </summary>
    public void ResetStateDisabled() => StateDisabled.Reset();

    /// <summary>
    /// Gets access to the normal rating appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal rating appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRatingStates StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Resets the StateNormal property to its default value.
    /// </summary>
    public void ResetStateNormal() => StateNormal.Reset();

    /// <summary>
    /// Gets access to the tracking (hover) rating appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tracking (hover) rating appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRatingStates StateTracking { get; }

    private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

    /// <summary>
    /// Resets the StateTracking property to its default value.
    /// </summary>
    public void ResetStateTracking() => StateTracking.Reset();

    /// <summary>
    /// Gets a value indicating whether the control should draw a focus cue.
    /// </summary>
    internal bool DrawFocusCues => Focused && ShowFocusCues;

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override Size DefaultSize => new Size(124, 24);

    /// <inheritdoc />
    protected override AccessibleObject CreateAccessibilityInstance() => new KryptonRatingAccessibleObject(this);

    /// <inheritdoc />
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        AdjustSize();
    }

    /// <inheritdoc />
    protected override void OnEnabledChanged(EventArgs e)
    {
        _drawRating.Enabled = Enabled;
        base.OnEnabledChanged(e);
        PerformNeedPaint(false);
    }

    /// <inheritdoc />
    protected override void OnRightToLeftChanged(EventArgs e)
    {
        _drawRating.RightToLeft = RightToLeft;
        base.OnRightToLeftChanged(e);
        PerformNeedPaint(true);
    }

    /// <inheritdoc />
    protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
    {
        _requestedDim = Orientation == Orientation.Horizontal ? height : width;

        if (_autoSize)
        {
            Size preferred = GetPreferredSize(Size.Empty);
            if (Orientation == Orientation.Horizontal)
            {
                if ((specified & BoundsSpecified.Height) != BoundsSpecified.None)
                {
                    height = preferred.Height;
                }

                if ((specified & BoundsSpecified.Width) != BoundsSpecified.None)
                {
                    width = preferred.Width;
                }
            }
            else
            {
                if ((specified & BoundsSpecified.Width) != BoundsSpecified.None)
                {
                    width = preferred.Width;
                }

                if ((specified & BoundsSpecified.Height) != BoundsSpecified.None)
                {
                    height = preferred.Height;
                }
            }
        }

        base.SetBoundsCore(x, y, width, height, specified);
    }

    /// <inheritdoc />
    protected override bool IsInputKey(Keys keyData) => (keyData & ~Keys.Shift) switch
    {
        Keys.Left or Keys.Right or Keys.Up or Keys.Down or Keys.Home or Keys.End or Keys.PageDown or Keys.PageUp => true,
        _ => base.IsInputKey(keyData)
    };

    /// <inheritdoc />
    protected override void OnMouseWheel(MouseEventArgs e)
    {
        _drawRating.OnMouseWheel(e.Delta);
        base.OnMouseWheel(e);
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
    protected override void OnNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        if (e.NeedLayout)
        {
            AdjustSize();
        }

        base.OnNeedPaint(sender, e);
    }

    /// <inheritdoc />
    protected override bool EvalTransparentPaint() => true;

    /// <summary>
    /// Raises the <see cref="ValueChanged"/> event.
    /// </summary>
    /// <param name="e">Event arguments.</param>
    protected virtual void OnValueChanged(EventArgs e) => ValueChanged?.Invoke(this, e);

    #endregion

    #region Implementation

    private void ApplyFixedStyle()
    {
        if (Orientation == Orientation.Horizontal)
        {
            SetStyle(ControlStyles.FixedHeight, _autoSize);
            SetStyle(ControlStyles.FixedWidth, false);
        }
        else
        {
            SetStyle(ControlStyles.FixedWidth, _autoSize);
            SetStyle(ControlStyles.FixedHeight, false);
        }
    }

    private void AdjustSize()
    {
        if (IsHandleCreated)
        {
            int requestedDim = _requestedDim;
            try
            {
                Size preferred = GetPreferredSize(Size.Empty);
                if (Orientation == Orientation.Horizontal)
                {
                    if (_autoSize)
                    {
                        Size = preferred;
                    }
                    else
                    {
                        Height = requestedDim > 0 ? requestedDim : preferred.Height;
                    }
                }
                else if (_autoSize)
                {
                    Size = preferred;
                }
                else
                {
                    Width = requestedDim > 0 ? requestedDim : preferred.Width;
                }
            }
            finally
            {
                _requestedDim = requestedDim;
            }
        }
    }

    private void OnDrawValueChanged(object? sender, EventArgs e)
    {
        OnValueChanged(e);
        if (IsHandleCreated)
        {
            AccessibilityNotifyClients(AccessibleEvents.ValueChange, -1);
        }
    }

    #endregion
}

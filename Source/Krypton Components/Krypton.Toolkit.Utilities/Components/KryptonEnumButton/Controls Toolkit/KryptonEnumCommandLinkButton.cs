#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// A command-link styled button (bold heading plus descriptive sub-text) that represents a single
/// value of an enumeration and cycles through the values of the assigned <see cref="EnumType"/> each
/// time it is clicked. This is the <see cref="KryptonCommandLinkButton"/> equivalent of
/// <see cref="KryptonEnumButton"/> (issue #3838).
/// </summary>
/// <remarks>
/// For each enum member the command-link <c>Heading</c> is set to the field name (optionally
/// humanised via <see cref="HumanizeNames"/>, or supplied by <see cref="HeadingProvider"/>) and the
/// <c>Description</c> is set to the member's <see cref="DescriptionAttribute"/> text (or
/// <see cref="DescriptionProvider"/>). A single enum decorated with <see cref="DescriptionAttribute"/>
/// therefore populates both lines automatically. Cycling, ordering, and event behaviour are shared
/// with <see cref="KryptonEnumButton"/> via a common engine.
/// </remarks>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonCommandLinkButton), @"ToolboxBitmaps.KryptonCommandLinkButton.bmp")]
[DefaultEvent(nameof(SelectedValueChanged))]
[DefaultProperty(nameof(EnumType))]
[DesignerCategory(@"code")]
[Designer(typeof(KryptonEnumCommandLinkButtonDesigner))]
[DisplayName(@"Krypton Enum Command Link")]
[Description(@"A command-link button that displays an enum value and cycles through the values when clicked.")]
public class KryptonEnumCommandLinkButton : KryptonCommandLinkButton
{
    #region Instance Fields

    private readonly EnumButtonValueCycler _cycler;
    private bool _useDescriptionAttribute;
    private bool _humanizeNames;
    private bool _reverseOnRightClick;
    private bool _allowKeyboardCycling;
    private bool _allowMouseWheelCycling;
    private Func<object, string>? _headingProvider;
    private Func<object, string>? _descriptionProvider;
    private Func<object, Image?>? _imageProvider;

    #endregion

    #region Events

    /// <summary>Occurs before the selected enum value changes; set <see cref="CancelEventArgs.Cancel"/> to veto the change.</summary>
    [Category(@"Behavior")]
    [Description(@"Occurs before the selected enum value changes. Set Cancel to true to veto the change.")]
    public event EventHandler<KryptonEnumButtonValueChangingEventArgs>? SelectedValueChanging;

    /// <summary>Occurs when the selected enum value changes (by clicking, cycling, or programmatically).</summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the selected enum value changes.")]
    public event EventHandler? SelectedValueChanged;

    /// <summary>Occurs when the selected enum value changes, carrying the new value and its heading text.</summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the selected enum value changes, carrying the new value and its heading text.")]
    public event EventHandler<KryptonEnumButtonValueChangedEventArgs>? EnumValueChanged;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonEnumCommandLinkButton" /> class.</summary>
    public KryptonEnumCommandLinkButton()
    {
        _cycler = new EnumButtonValueCycler();
        _useDescriptionAttribute = true;
        _humanizeNames = false;
        _reverseOnRightClick = false;
        _allowKeyboardCycling = true;
        _allowMouseWheelCycling = true;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the enumeration type whose values the button cycles through. Setting a new type
    /// resets the selection to the first value; assigning <see langword="null"/> clears the values.
    /// </summary>
    /// <exception cref="ArgumentException">The supplied type is not an enumeration type.</exception>
    [Category(@"Behavior")]
    [Description(@"The enumeration type whose values are cycled through when the button is clicked.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    [Editor(typeof(EnumTypeEditor), typeof(UITypeEditor))]
    public Type? EnumType
    {
        get => _cycler.EnumType;
        set
        {
            if (_cycler.SetEnumType(value))
            {
                UpdatePresentation();
            }
        }
    }

    /// <summary>Gets or sets the zero-based index of the currently selected enum value.</summary>
    [Category(@"Behavior")]
    [Description(@"The zero-based index of the currently selected enum value.")]
    [DefaultValue(0)]
    [RefreshProperties(RefreshProperties.Repaint)]
    public int SelectedIndex
    {
        get => _cycler.SelectedIndex;
        set => TryChangeTo(value);
    }

    /// <summary>
    /// Gets or sets the currently selected enum value. Setting a value that is not part of
    /// <see cref="EnumType"/> is ignored. Returns <see langword="null"/> when no enum type is assigned.
    /// </summary>
    [Browsable(false)]
    [Bindable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object? SelectedValue
    {
        get => _cycler.SelectedValue;
        set
        {
            var index = _cycler.IndexOfValue(value);
            if (index >= 0)
            {
                TryChangeTo(index);
            }
        }
    }

    /// <summary>Gets the heading text currently shown for the selected value.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SelectedHeadingText
    {
        get
        {
            ComputeText(_cycler.SelectedIndex, out var heading, out _);
            return heading;
        }
    }

    /// <summary>Gets the description text currently shown for the selected value.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SelectedDescriptionText
    {
        get
        {
            ComputeText(_cycler.SelectedIndex, out _, out var description);
            return description;
        }
    }

    /// <summary>Gets or sets a value indicating whether cycling wraps around from the last value to the first (and vice versa).</summary>
    [Category(@"Behavior")]
    [Description(@"When true, cycling past the last value returns to the first (and before the first returns to the last).")]
    [DefaultValue(true)]
    public bool WrapAround
    {
        get => _cycler.WrapAround;
        set => _cycler.WrapAround = value;
    }

    /// <summary>Gets or sets a value indicating whether the <see cref="DescriptionAttribute"/> text populates the command-link description line.</summary>
    [Category(@"Behavior")]
    [Description(@"When true, each value's DescriptionAttribute text is shown as the command-link description; when false, the description is left blank.")]
    [DefaultValue(true)]
    [RefreshProperties(RefreshProperties.Repaint)]
    public bool UseDescriptionAttribute
    {
        get => _useDescriptionAttribute;
        set
        {
            if (_useDescriptionAttribute != value)
            {
                _useDescriptionAttribute = value;
                UpdatePresentation();
            }
        }
    }

    /// <summary>Gets or sets a value indicating whether PascalCase field names are humanised (e.g. <c>ExtraLarge</c> becomes <c>Extra Large</c>) for the heading.</summary>
    [Category(@"Behavior")]
    [Description(@"When true, PascalCase / snake_case field names are shown with spaces in the heading when no HeadingProvider is used.")]
    [DefaultValue(false)]
    [RefreshProperties(RefreshProperties.Repaint)]
    public bool HumanizeNames
    {
        get => _humanizeNames;
        set
        {
            if (_humanizeNames != value)
            {
                _humanizeNames = value;
                UpdatePresentation();
            }
        }
    }

    /// <summary>Gets or sets the order in which the enum values are cycled.</summary>
    [Category(@"Behavior")]
    [Description(@"The order in which the enum values are cycled.")]
    [DefaultValue(typeof(EnumButtonSortOrder), "Declaration")]
    public EnumButtonSortOrder SortOrder
    {
        get => _cycler.SortOrder;
        set
        {
            _cycler.SortOrder = value;
            UpdatePresentation();
        }
    }

    /// <summary>Gets or sets values excluded from the cycle. Assign in code; not serialized by the designer.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IReadOnlyList<object>? ExcludedValues
    {
        get => _cycler.ExcludedValues;
        set
        {
            _cycler.ExcludedValues = value;
            UpdatePresentation();
        }
    }

    /// <summary>Gets or sets a value indicating whether right-clicking cycles to the previous value.</summary>
    [Category(@"Behavior")]
    [Description(@"When true, a right mouse click cycles backwards to the previous value.")]
    [DefaultValue(false)]
    public bool ReverseOnRightClick
    {
        get => _reverseOnRightClick;
        set => _reverseOnRightClick = value;
    }

    /// <summary>Gets or sets a value indicating whether the arrow keys cycle the value while the button has focus.</summary>
    [Category(@"Behavior")]
    [Description(@"When true, Left/Up cycle to the previous value and Right/Down cycle to the next value while the button has focus.")]
    [DefaultValue(true)]
    public bool AllowKeyboardCycling
    {
        get => _allowKeyboardCycling;
        set => _allowKeyboardCycling = value;
    }

    /// <summary>Gets or sets a value indicating whether the mouse wheel cycles the value.</summary>
    [Category(@"Behavior")]
    [Description(@"When true, scrolling the mouse wheel cycles the value (wheel up = previous, wheel down = next).")]
    [DefaultValue(true)]
    public bool AllowMouseWheelCycling
    {
        get => _allowMouseWheelCycling;
        set => _allowMouseWheelCycling = value;
    }

    /// <summary>
    /// Gets or sets an optional callback that supplies the heading text for a value. When
    /// <see langword="null"/> (the default) the enum field name is used (optionally humanised).
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Func<object, string>? HeadingProvider
    {
        get => _headingProvider;
        set
        {
            _headingProvider = value;
            UpdatePresentation();
        }
    }

    /// <summary>
    /// Gets or sets an optional callback that supplies the description text for a value. When
    /// <see langword="null"/> (the default) the value's <see cref="DescriptionAttribute"/> is used
    /// (subject to <see cref="UseDescriptionAttribute"/>).
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Func<object, string>? DescriptionProvider
    {
        get => _descriptionProvider;
        set
        {
            _descriptionProvider = value;
            UpdatePresentation();
        }
    }

    /// <summary>
    /// Gets or sets an optional callback that supplies the icon shown for each value. When
    /// <see langword="null"/> (the default) the command-link default image is used.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Func<object, Image?>? ImageProvider
    {
        get => _imageProvider;
        set
        {
            _imageProvider = value;
            UpdatePresentation();
        }
    }

    /// <summary>Sets the <see cref="EnumType"/> from a strongly-typed enum type parameter.</summary>
    /// <typeparam name="TEnum">The enumeration type.</typeparam>
    public void SetEnumType<TEnum>() where TEnum : struct, Enum => EnumType = typeof(TEnum);

    /// <summary>Assigns the <see cref="EnumType"/> (if required) and selects the supplied value.</summary>
    /// <typeparam name="TEnum">The enumeration type.</typeparam>
    /// <param name="value">The value to select.</param>
    public void SetSelectedValue<TEnum>(TEnum value) where TEnum : struct, Enum
    {
        EnumType = typeof(TEnum);
        SelectedValue = value;
    }

    /// <summary>Gets the currently selected value cast to the supplied enum type.</summary>
    /// <typeparam name="TEnum">The enumeration type.</typeparam>
    /// <returns>The selected value, or <c>default</c> when no value of that type is selected.</returns>
    public TEnum GetSelectedValue<TEnum>() where TEnum : struct, Enum => SelectedValue is TEnum value ? value : default;

    /// <summary>Cycles to the next enum value (wrapping or clamping according to <see cref="WrapAround"/>).</summary>
    public void CycleNext() => TryChangeTo(_cycler.GetCycleTargetIndex(1));

    /// <summary>Cycles to the previous enum value (wrapping or clamping according to <see cref="WrapAround"/>).</summary>
    public void CyclePrevious() => TryChangeTo(_cycler.GetCycleTargetIndex(-1));

    #endregion

    #region Protected Overrides

    /// <summary>Creates the accessibility object for the control.</summary>
    /// <returns>A <see cref="KryptonEnumCommandLinkButtonAccessibleObject"/> instance.</returns>
    protected override AccessibleObject CreateAccessibilityInstance() => new KryptonEnumCommandLinkButtonAccessibleObject(this);

    /// <summary>Raises the <see cref="Control.Click" /> event, cycling to the next value first.</summary>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
    protected override void OnClick(EventArgs e)
    {
        // Cycle before raising Click so handlers observe the newly selected value.
        if (!DesignMode)
        {
            CycleNext();
        }

        base.OnClick(e);
    }

    /// <summary>Raises the <see cref="Control.MouseUp" /> event, cycling backwards on right-click when enabled.</summary>
    /// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data.</param>
    protected override void OnMouseUp(MouseEventArgs e)
    {
        if (_reverseOnRightClick
            && e.Button == MouseButtons.Right
            && !DesignMode
            && _cycler.Count > 0
            && ClientRectangle.Contains(e.Location))
        {
            CyclePrevious();
        }

        base.OnMouseUp(e);
    }

    /// <summary>Raises the <see cref="Control.KeyDown" /> event, cycling on the arrow keys when enabled.</summary>
    /// <param name="e">A <see cref="KeyEventArgs"/> that contains the event data.</param>
    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (_allowKeyboardCycling && _cycler.Count > 0 && !DesignMode)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                case Keys.Down:
                    CycleNext();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Left:
                case Keys.Up:
                    CyclePrevious();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
            }
        }

        base.OnKeyDown(e);
    }

    /// <summary>Raises the <see cref="Control.MouseWheel" /> event, cycling the value when enabled.</summary>
    /// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data.</param>
    protected override void OnMouseWheel(MouseEventArgs e)
    {
        if (_allowMouseWheelCycling && _cycler.Count > 0 && !DesignMode && e.Delta != 0)
        {
            if (e.Delta < 0)
            {
                CycleNext();
            }
            else
            {
                CyclePrevious();
            }

            if (e is HandledMouseEventArgs handled)
            {
                handled.Handled = true;
            }
        }

        base.OnMouseWheel(e);
    }

    #endregion

    #region Implementation

    /// <summary>Raises the <see cref="SelectedValueChanging" /> event.</summary>
    /// <param name="e">A <see cref="KryptonEnumButtonValueChangingEventArgs"/> that contains the event data.</param>
    protected virtual void OnSelectedValueChanging(KryptonEnumButtonValueChangingEventArgs e) => SelectedValueChanging?.Invoke(this, e);

    /// <summary>Raises the <see cref="SelectedValueChanged" /> event.</summary>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
    protected virtual void OnSelectedValueChanged(EventArgs e) => SelectedValueChanged?.Invoke(this, e);

    /// <summary>Raises the <see cref="EnumValueChanged" /> event.</summary>
    /// <param name="e">A <see cref="KryptonEnumButtonValueChangedEventArgs"/> that contains the event data.</param>
    protected virtual void OnEnumValueChanged(KryptonEnumButtonValueChangedEventArgs e) => EnumValueChanged?.Invoke(this, e);

    /// <summary>Attempts to change the selection to the supplied index, honouring the cancelable changing event.</summary>
    /// <param name="targetIndex">The requested index.</param>
    private void TryChangeTo(int targetIndex)
    {
        if (_cycler.Count == 0)
        {
            return;
        }

        var clamped = _cycler.Clamp(targetIndex);
        if (clamped == _cycler.SelectedIndex)
        {
            return;
        }

        ComputeText(clamped, out var proposedHeading, out _);
        var changing = new KryptonEnumButtonValueChangingEventArgs(_cycler.SelectedValue, _cycler.ValueAt(clamped), proposedHeading);
        OnSelectedValueChanging(changing);
        if (changing.Cancel)
        {
            return;
        }

        _cycler.CommitIndex(clamped);
        UpdatePresentation();

        OnSelectedValueChanged(EventArgs.Empty);
        OnEnumValueChanged(new KryptonEnumButtonValueChangedEventArgs(_cycler.SelectedValue, SelectedHeadingText));
    }

    /// <summary>Computes the heading and description text for a value index.</summary>
    /// <param name="index">The value index.</param>
    /// <param name="heading">The resolved heading text.</param>
    /// <param name="description">The resolved description text.</param>
    private void ComputeText(int index, out string heading, out string description)
    {
        var field = _cycler.FieldAt(index);
        if (field is null)
        {
            heading = string.Empty;
            description = string.Empty;
            return;
        }

        var value = _cycler.ValueAt(index)!;

        heading = _headingProvider is not null
            ? _headingProvider(value) ?? string.Empty
            : (_humanizeNames ? EnumButtonTextHelper.Humanize(field.Name) : field.Name);

        if (_descriptionProvider is not null)
        {
            description = _descriptionProvider(value) ?? string.Empty;
        }
        else if (_useDescriptionAttribute)
        {
            description = EnumButtonTextHelper.GetDescription(field);
        }
        else
        {
            description = string.Empty;
        }
    }

    /// <summary>Updates the command-link heading, description and optional image to match the current selection.</summary>
    private void UpdatePresentation()
    {
        if (_cycler.SelectedField is null)
        {
            CommandLinkTextValues.Heading = string.Empty;
            CommandLinkTextValues.Description = string.Empty;
            CommandLinkTextValues.UseDefaultImage = true;
            return;
        }

        ComputeText(_cycler.SelectedIndex, out var heading, out var description);

        CommandLinkTextValues.Heading = heading;
        CommandLinkTextValues.Description = description;

        if (_imageProvider is not null)
        {
            CommandLinkTextValues.UseDefaultImage = false;
            CommandLinkTextValues.Image = _imageProvider(_cycler.SelectedValue!);
        }
        else
        {
            CommandLinkTextValues.UseDefaultImage = true;
        }
    }

    #endregion
}

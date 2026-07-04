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
/// A button that displays the text of a single enum value and cycles through the values of the
/// assigned <see cref="EnumType"/> each time it is clicked. Use this instead of a long list of
/// radio buttons when the user only needs to toggle between the members of an enumeration.
/// </summary>
/// <remarks>
/// The button text is derived from the current enum member: the <see cref="DescriptionAttribute"/>
/// text when present (and <see cref="UseDescriptionAttribute"/> is <see langword="true"/>), otherwise
/// the enum field name (optionally humanised via <see cref="HumanizeNames"/>). The standard
/// <see cref="Control.Click"/> event still fires on every click, and the dedicated
/// <see cref="SelectedValueChanging"/> / <see cref="SelectedValueChanged"/> / <see cref="EnumValueChanged"/>
/// events report the change.
/// </remarks>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonButton), "ToolboxBitmaps.KryptonButton.bmp")]
[DefaultEvent(nameof(SelectedValueChanged))]
[DefaultProperty(nameof(EnumType))]
[DesignerCategory(@"code")]
[Designer(typeof(KryptonEnumButtonDesigner))]
[Description(@"A button that displays an enum value and cycles through the values when clicked.")]
public class KryptonEnumButton : KryptonButton
{
    #region Instance Fields

    private readonly EnumButtonValueCycler _cycler;
    private bool _useDescriptionAttribute;
    private bool _humanizeNames;
    private bool _reverseOnRightClick;
    private bool _allowKeyboardCycling;
    private bool _allowMouseWheelCycling;
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

    /// <summary>Occurs when the selected enum value changes, carrying the new value and its display text.</summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the selected enum value changes, carrying the new value and its display text.")]
    public event EventHandler<KryptonEnumButtonValueChangedEventArgs>? EnumValueChanged;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonEnumButton" /> class.</summary>
    public KryptonEnumButton()
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

    /// <summary>Gets the display text currently shown on the button for the selected value.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SelectedDisplayText => GetTextForIndex(_cycler.SelectedIndex);

    /// <summary>Gets or sets a value indicating whether cycling wraps around from the last value to the first (and vice versa).</summary>
    [Category(@"Behavior")]
    [Description(@"When true, cycling past the last value returns to the first (and before the first returns to the last).")]
    [DefaultValue(true)]
    public bool WrapAround
    {
        get => _cycler.WrapAround;
        set => _cycler.WrapAround = value;
    }

    /// <summary>Gets or sets a value indicating whether the <see cref="DescriptionAttribute"/> text is used for display, falling back to the field name.</summary>
    [Category(@"Behavior")]
    [Description(@"When true, the DescriptionAttribute text is used for each value (falling back to the field name); when false, the field name is used.")]
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

    /// <summary>Gets or sets a value indicating whether PascalCase field names are humanised (e.g. <c>ExtraLarge</c> becomes <c>Extra Large</c>) when no description is used.</summary>
    [Category(@"Behavior")]
    [Description(@"When true, PascalCase / snake_case field names are shown with spaces when no DescriptionAttribute text is used.")]
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
    /// Gets or sets an optional callback that supplies the image shown on the button for each value.
    /// When <see langword="null"/> (the default) the button image is left unchanged.
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

    /// <summary>
    /// Gets or sets the button text. The text is derived from the selected enum value and cannot be
    /// set meaningfully; the setter is retained for base-class compatibility.
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
    /// <returns>A <see cref="KryptonEnumButtonAccessibleObject"/> instance.</returns>
    protected override AccessibleObject CreateAccessibilityInstance() => new KryptonEnumButtonAccessibleObject(this);

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

        var changing = new KryptonEnumButtonValueChangingEventArgs(_cycler.SelectedValue, _cycler.ValueAt(clamped), GetTextForIndex(clamped));
        OnSelectedValueChanging(changing);
        if (changing.Cancel)
        {
            return;
        }

        _cycler.CommitIndex(clamped);
        UpdatePresentation();

        OnSelectedValueChanged(EventArgs.Empty);
        OnEnumValueChanged(new KryptonEnumButtonValueChangedEventArgs(_cycler.SelectedValue, SelectedDisplayText));
    }

    /// <summary>Resolves the display text for a value index.</summary>
    /// <param name="index">The value index.</param>
    /// <returns>The display text, or an empty string when the index has no field.</returns>
    private string GetTextForIndex(int index)
    {
        var field = _cycler.FieldAt(index);
        return field is null
            ? string.Empty
            : EnumButtonTextHelper.ResolveText(field, _useDescriptionAttribute, _humanizeNames);
    }

    /// <summary>Updates the button text (and optional image) to match the current selection.</summary>
    private void UpdatePresentation()
    {
        var field = _cycler.SelectedField;
        if (field is null)
        {
            if (DesignMode && string.IsNullOrEmpty(base.Text))
            {
                base.Text = @"(KryptonEnumButton)";
            }

            return;
        }

        base.Text = EnumButtonTextHelper.ResolveText(field, _useDescriptionAttribute, _humanizeNames);

        if (_imageProvider is not null)
        {
            Values.Image = _imageProvider(_cycler.SelectedValue!);
        }
    }

    #endregion
}

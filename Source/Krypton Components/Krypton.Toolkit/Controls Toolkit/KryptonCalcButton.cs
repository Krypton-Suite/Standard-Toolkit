#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Combines drop-down button functionality with the styling features of the Krypton Toolkit.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonCalcButton), "ToolboxBitmaps.KryptonCalcButton.bmp")]
[DefaultEvent(nameof(Click))]
[DefaultProperty(nameof(Text))]
[Designer(typeof(KryptonCalcButtonDesigner))]
[DesignerCategory(@"code")]
[Description(@"Raises an event when the user clicks it.")]
public class KryptonCalcButton : VisualSimpleBase, IButtonControl, IContentValues
{
    #region Instance Fields
    protected internal readonly ViewDrawButton _drawButton;
    private ButtonStyle _style;
    protected internal readonly ButtonController _buttonController;
    private readonly PaletteTripleOverride _overrideFocus;
    private readonly PaletteTripleOverride _overrideNormal;
    private readonly PaletteTripleOverride _overrideTracking;
    private readonly PaletteTripleOverride _overridePressed;
    private IKryptonCommand? _command;
    private bool _isDefault;
    private bool _useMnemonic;
    private bool _wasEnabled;
    private decimal _value;
    private VisualPopupCalculator? _popupCalculator;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the drop-down portion of the button is pressed.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the drop-down portion of the button is pressed.")]
    public event EventHandler<ContextPositionMenuArgs>? DropDown;

    /// <summary>
    /// Occurs when the Value property has changed value.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Event raised when the value of the Value property is changed on KryptonCalcButton.")]
    public event EventHandler? ValueChanged;

    /// <summary>
    /// Occurs when the value of the KryptonCommand property changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the value of the KryptonCommand property changes.")]
    public event EventHandler? KryptonCommandChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonCalcButton class.
    /// </summary>
    public KryptonCalcButton()
    {
        // We generate click events manually, suppress default
        // production of them by the base Control class
        SetStyle(ControlStyles.StandardClick |
                 ControlStyles.StandardDoubleClick, false);
        SetStyle(ControlStyles.UseTextForAccessibility, true);

        // Set default button properties
        _style = ButtonStyle.Standalone;
        DialogResult = DialogResult.None;
        _useMnemonic = true;

        // Create content storage
        Values = CreateButtonValues(NeedPaintDelegate);
        Values.TextChanged += OnButtonTextChanged;
        // Default calculator glyph
        Values.Image = ResourceFiles.Generic.GenericKryptonImageResources.KryptonCalcButton;

        // Create the palette storage
        StateCommon = new PaletteTripleRedirect(Redirector, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, NeedPaintDelegate);
        StateDisabled = new PaletteTriple(StateCommon, NeedPaintDelegate);
        StateNormal = new PaletteTriple(StateCommon, NeedPaintDelegate);
        StateTracking = new PaletteTriple(StateCommon, NeedPaintDelegate);
        StatePressed = new PaletteTriple(StateCommon, NeedPaintDelegate);
        OverrideDefault = new PaletteTripleRedirect(Redirector, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, NeedPaintDelegate);
        OverrideFocus = new PaletteTripleRedirect(Redirector, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, NeedPaintDelegate);

        // Create the override handling classes
        _overrideFocus = new PaletteTripleOverride(OverrideFocus, StateNormal, PaletteState.FocusOverride);
        _overrideNormal = new PaletteTripleOverride(OverrideDefault, _overrideFocus, PaletteState.NormalDefaultOverride);
        _overrideTracking = new PaletteTripleOverride(OverrideFocus, StateTracking, PaletteState.FocusOverride);
        _overridePressed = new PaletteTripleOverride(OverrideFocus, StatePressed, PaletteState.FocusOverride);

        // Create the view button instance
        _drawButton = new ViewDrawButton(StateDisabled,
            _overrideNormal,
            _overrideTracking,
            _overridePressed,
            new PaletteMetricRedirect(Redirector),
            this,
            VisualOrientation.Top,
            UseMnemonic)
        {
            // Set default button state
            DropDown = true,
            Splitter = true,
            TestForFocusCues = true
        };

        // Replace default drop glyph with calculator icon
        try
        {
            var calcBmp = ResourceFiles.Generic.GenericKryptonImageResources.KryptonCalcButton;
            var ddField = typeof(ViewDrawButton).GetField("_drawDropDownButton", BindingFlags.NonPublic | BindingFlags.Instance);
            if (ddField != null)
            {
                var ddButton = ddField.GetValue(_drawButton);
                var prop = ddButton?.GetType().GetProperty("CustomGlyph", BindingFlags.Public | BindingFlags.Instance);
                prop?.SetValue(ddButton, calcBmp, null);
            }
        }
        catch
        {
            // ignore if internal shape changes in future
        }

        // Create a button controller to handle button style behaviour
        _buttonController = new ButtonController(_drawButton, NeedPaintDelegate)
        {
            BecomesFixed = true
        };

        // Assign the controller to the view element to treat as a button
        _drawButton.MouseController = _buttonController;
        _drawButton.KeyController = _buttonController;
        _drawButton.SourceController = _buttonController;

        // Need to know when user clicks the button view or mouse selects it
        _buttonController.Click += OnButtonClick;
        _buttonController.MouseSelect += OnButtonSelect;

        // Create the view manager instance
        ViewManager = new ViewManager(this, _drawButton);

        // Defaults for calculator value
        _value = 0m;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the automatic resize of the control to fit contents.
    /// </summary>
    [Browsable(true)]
    [Localizable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [RefreshProperties(RefreshProperties.All)]
    public override bool AutoSize
    {
        get => base.AutoSize;
        set => base.AutoSize = value;
    }

    /// <summary>
    /// Gets and sets the internal padding space.
    /// </summary>
    [Browsable(false)]
    [Localizable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Padding Padding
    {
        get => base.Padding;
        set => base.Padding = value;
    }

    /// <summary>
    /// Gets or sets the text associated with this control.
    /// </summary>
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [AllowNull]
    public override string Text
    {
        get => Values.Text;
        set => Values.Text = value;
    }

    private bool ShouldSerializeText() =>
        // Never serialize, let the button values serialize instead
        false;

    /// <summary>
    /// Resets the Text property to its default value.
    /// </summary>
    public override void ResetText() =>
        // Map onto the button property from the values
        Values.ResetText();

    /// <summary>
    /// Gets and sets the visual orientation of the control.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Visual orientation of the control.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(VisualOrientation.Top)]
    public VisualOrientation ButtonOrientation
    {
        get => _drawButton.Orientation;

        set
        {
            if (_drawButton.Orientation != value)
            {
                _drawButton.Orientation = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets and sets the position of the drop arrow within the button.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Position of the drop arrow within the button.")]
    [DefaultValue(VisualOrientation.Right)]
    public VisualOrientation DropDownPosition
    {
        get => _drawButton.DropDownPosition;

        set
        {
            if (_drawButton.DropDownPosition != value)
            {
                _drawButton.DropDownPosition = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets and sets the orientation of the drop arrow within the button.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Orientation of the drop arrow within the button.")]
    [DefaultValue(VisualOrientation.Bottom)]
    public VisualOrientation DropDownOrientation
    {
        get
        {
            return _drawButton.DropDownOrientation switch
            {
                VisualOrientation.Bottom => VisualOrientation.Top,
                VisualOrientation.Left => VisualOrientation.Right,
                VisualOrientation.Right => VisualOrientation.Left,
                _ => VisualOrientation.Bottom
            };
        }

        set
        {
            VisualOrientation converted = value switch
            {
                VisualOrientation.Top => VisualOrientation.Bottom,
                VisualOrientation.Right => VisualOrientation.Left,
                VisualOrientation.Left => VisualOrientation.Right,
                _ => VisualOrientation.Top
            };
            if (_drawButton.DropDownOrientation != converted)
            {
                _drawButton.DropDownOrientation = converted;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets and sets if the button works as a splitter or as a drop-down.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Determine if button acts as a splitter or just a drop-down.")]
    [DefaultValue(true)]
    public bool Splitter
    {
        get => _drawButton.Splitter;

        set
        {
            if (_drawButton.Splitter != value)
            {
                _drawButton.Splitter = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets and sets the button style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Button style.")]
    public ButtonStyle ButtonStyle
    {
        get => _style;

        set
        {
            if (_style != value)
            {
                _style = value;
                SetStyles(_style);
                PerformNeedPaint(true);
            }
        }
    }

    private void ResetButtonStyle() => ButtonStyle = ButtonStyle.Standalone;

    private bool ShouldSerializeButtonStyle() => ButtonStyle != ButtonStyle.Standalone;

    /// <summary>
    /// Gets access to the button content.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Button values")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ButtonValues Values { get; }

    private bool ShouldSerializeValues() => !Values.IsDefault;

    /// <summary>
    /// Gets access to the common button appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common button appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled button appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled button appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal button appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal button appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the hot tracking button appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining hot tracking button appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateTracking { get; }

    private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

    /// <summary>
    /// Gets access to the pressed button appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed button appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StatePressed { get; }

    private bool ShouldSerializeStatePressed() => !StatePressed.IsDefault;

    /// <summary>
    /// Gets access to the normal button appearance when default.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal button appearance when default.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect OverrideDefault { get; }

    private bool ShouldSerializeOverrideDefault() => !OverrideDefault.IsDefault;

    /// <summary>
    /// Gets access to the button appearance when it has focus.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining button appearance when it has focus.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect OverrideFocus { get; }

    private bool ShouldSerializeOverrideFocus() => !OverrideFocus.IsDefault;

    /// <summary>
    /// Gets or sets the value returned to the parent form when the button is clicked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The dialog-box result produced in a modal form by clicking the button.")]
    [DefaultValue(DialogResult.None)]
    public DialogResult DialogResult { get; set; }

    /// <summary>
    /// Gets and sets the associated KryptonCommand.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Command associated with the drop button.")]
    [DefaultValue(null)]
    public virtual IKryptonCommand? KryptonCommand
    {
        get => _command;

        set
        {
            if (_command != value)
            {
                if (_command != null)
                {
                    _command.PropertyChanged -= OnCommandPropertyChanged;
                }
                else
                {
                    _wasEnabled = Enabled;
                }

                _command = value;
                OnKryptonCommandChanged(EventArgs.Empty);

                if (_command != null)
                {
                    _command.PropertyChanged += OnCommandPropertyChanged;
                }
                else
                {
                    Enabled = _wasEnabled;
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets the numeric value represented by the control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Numeric value shown by the control and calculator.")]
    [Bindable(true)]
    public decimal Value
    {
        get => _value;

        set
        {
            if (_value != value)
            {
                _value = value;
                Values.Text = _value.ToString(System.Globalization.CultureInfo.CurrentCulture);
                OnValueChanged(EventArgs.Empty);
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Notifies a control that it is the default button so that its appearance and behavior is adjusted accordingly. 
    /// </summary>
    /// <param name="value">true if the control should behave as a default button; otherwise false.</param>
    public void NotifyDefault(bool value)
    {
        if (!ViewDrawButton.IsFixed && (_isDefault != value))
        {
            // Remember new default status
            _isDefault = value;

            // Decide if the default overrides should be applied
            _overrideNormal.Apply = value;

            // Change in default state requires a layout and repaint
            PerformNeedPaint(true);
        }
    }

    /// <summary>
    /// Generates a Click event for the control.
    /// </summary>
    public void PerformClick()
    {
        if (CanSelect)
        {
            OnClick(EventArgs.Empty);
        }
    }

    /// <summary>
    /// Generates a DropDown event for the control.
    /// </summary>
    public void PerformDropDown()
    {
        if (CanSelect)
        {
            ShowDropDown();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether an ampersand is included in the text of the control. 
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"When true the first character after an ampersand will be used as a mnemonic.")]
    [DefaultValue(true)]
    public bool UseMnemonic
    {
        get => _useMnemonic;

        set
        {
            if (_useMnemonic != value)
            {
                _useMnemonic = value;
                _drawButton.UseMnemonic = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Fix the control to a particular palette state.
    /// </summary>
    /// <param name="state">Palette state to fix.</param>
    public void SetFixedState(PaletteState state)
    {
        if (state == PaletteState.NormalDefaultOverride)
        {
            // Setup the overrides correctly to match state
            _overrideFocus.Apply = true;
            _overrideNormal.Apply = true;

            // Must pass a proper drawing state to the view
            state = PaletteState.Normal;
        }

        // Request fixed state from the view
        _drawButton.FixedState = state;
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
    #endregion

    #region IContentValues
    /// <summary>
    /// Gets the content short text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetShortText() => KryptonCommand?.Text ?? Values.GetShortText();

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetLongText() => KryptonCommand?.ExtraText ?? Values.GetLongText();

    /// <summary>
    /// Gets the content image.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Image value.</returns>
    public Image? GetImage(PaletteState state) => KryptonCommand?.ImageSmall ?? Values.GetImage(state);

    /// <summary>
    /// Gets the image color that should be transparent.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Color value.</returns>
    public Color GetImageTransparentColor(PaletteState state) =>
        KryptonCommand?.ImageTransparentColor ?? Values.GetImageTransparentColor(state);
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Gets the default size of the control.
    /// </summary>
    protected override Size DefaultSize => new Size(90, 25);

    /// <summary>
    /// Gets the default Input Method Editor (IME) mode supported by this control.
    /// </summary>
    protected override ImeMode DefaultImeMode => ImeMode.Disable;

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        // Must inform the button view which itself tells the embedded elements
        _drawButton.Enabled = Enabled;

        // Change in enabled state requires a layout and repaint
        PerformNeedPaint(true);

        // Let base class fire standard event
        base.OnEnabledChanged(e);
    }

    /// <summary>
    /// Raises the GotFocus event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnGotFocus(EventArgs e)
    {
        if (!ViewDrawButton.IsFixed)
        {
            // Apply the focus overrides
            _overrideFocus.Apply = true;
            _overrideTracking.Apply = true;
            _overridePressed.Apply = true;

            // Change in focus requires a repaint
            PerformNeedPaint(false);
        }

        // Let base class fire standard event
        base.OnGotFocus(e);
    }

    /// <summary>
    /// Raises the LostFocus event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnLostFocus(EventArgs e)
    {
        if (!ViewDrawButton.IsFixed)
        {
            // Apply the focus overrides
            _overrideFocus.Apply = false;
            _overrideTracking.Apply = false;
            _overridePressed.Apply = false;

            // Change in focus requires a repaint
            PerformNeedPaint(false);
        }

        // Let base class fire standard event
        base.OnLostFocus(e);
    }

    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnClick(EventArgs e)
    {
        // Find the form this button is on
        Form? owner = FindForm();

        // If we find a valid owner
        if (owner is not null)
        {
            // Update owner with our dialog result setting
            if (IsDialogResultValidForForm(DialogResult))
            {
                owner.DialogResult = DialogResult;
            }
            else if (owner is VisualMessageBoxForm || owner is VisualMessageBoxRtlAwareForm)
            {
                FieldInfo? fi = typeof(Form).GetField("dialogResult", BindingFlags.NonPublic | BindingFlags.Instance);
                if (fi != null)
                {
                    fi.SetValue(owner, DialogResult);
                    owner.Close();
                }
            }
            else
            {
                throw new InvalidEnumArgumentException(nameof(DialogResult), (int)DialogResult, typeof(DialogResult));
            }
        }

        // Let base class fire standard event
        base.OnClick(e);

        // If we have an attached command then execute it
        KryptonCommand?.PerformExecute();
    }

    /// <summary>
    /// Processes a mnemonic character.
    /// </summary>
    /// <param name="charCode">The mnemonic character entered.</param>
    /// <returns>true if the mnemonic was processed; otherwise, false.</returns>
    protected override bool ProcessMnemonic(char charCode)
    {
        // Are we allowed to process mnemonics?
        if (UseMnemonic && CanProcessMnemonic())
        {
            // Does the button primary text contain the mnemonic?
            if (IsMnemonic(charCode, Values.Text))
            {
                if (Splitter)
                {
                    PerformDropDown();
                }
                else
                {
                    PerformClick();
                }

                return true;
            }
        }

        // No match found, let base class do standard processing
        return base.ProcessMnemonic(charCode);
    }

    /// <summary>
    /// Called when a context menu has just been closed.
    /// </summary>
    protected override void ContextMenuClosed() => _buttonController.RemoveFixed();

    /// <summary>
    /// Process Windows-based messages.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    protected override void WndProc(ref Message m)
    {
        // Prevent base class from showing a context menu when right clicking it
        if (m.Msg != PI.WM_.CONTEXTMENU)
        {
            base.WndProc(ref m);
        }
    }
    #endregion

    #region Protected Virtual
    /// <summary>
    /// Raises the DropDown event.
    /// </summary>
    /// <param name="e">An ContextPositionMenuArgs containing the event data.</param>
    protected virtual void OnDropDown(ContextPositionMenuArgs e) => DropDown?.Invoke(this, e);

    /// <summary>
    /// Raises the KryptonCommandChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnKryptonCommandChanged(EventArgs e)
    {
        KryptonCommandChanged?.Invoke(this, e);

        // Use the values from the new command
        if (KryptonCommand != null)
        {
            Enabled = KryptonCommand.Enabled;
        }

        // Redraw to update the text/extratext/image properties
        PerformNeedPaint(true);
    }

    /// <summary>
    /// Raises the ValueChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnValueChanged(EventArgs e) => ValueChanged?.Invoke(this, e);

    /// <summary>
    /// Handles a change in the property of an attached command.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A PropertyChangedEventArgs that contains the event data.</param>
    protected virtual void OnCommandPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(Enabled):
                Enabled = KryptonCommand!.Enabled;
                break;
            case nameof(Text):
            case @"ExtraText":
            case @"ImageSmall":
            case @"ImageTransparentColor":
                PerformNeedPaint(true);
                break;
        }
    }

    /// <summary>
    /// Update the state objects to reflect the new button style.
    /// </summary>
    /// <param name="buttonStyle">New button style.</param>
    protected virtual void SetStyles(ButtonStyle buttonStyle)
    {
        StateCommon.SetStyles(buttonStyle);
        OverrideDefault.SetStyles(buttonStyle);
        OverrideFocus.SetStyles(buttonStyle);
    }

    /// <summary>
    /// Creates a values storage object appropriate for control.
    /// </summary>
    /// <returns>Set of button values.</returns>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    protected virtual ButtonValues CreateButtonValues(NeedPaintHandler needPaint) => new ButtonValues(needPaint);

    /// <summary>
    /// Gets access to the view element for the button.
    /// </summary>
    protected virtual ViewDrawButton ViewDrawButton => _drawButton;
    #endregion

    #region Implementation

    private static bool IsDialogResultValidForForm(DialogResult value)
    {
        return Enum.IsDefined(typeof(DialogResult), value);
    }

    private void OnButtonTextChanged(object? sender, EventArgs e) => OnTextChanged(EventArgs.Empty);

    private void OnButtonClick(object? sender, MouseEventArgs e)
    {
        var showingContextMenu = false;

        // Do we need to show a drop-down menu?
        if ((!Splitter)
            || (Splitter && _drawButton.SplitRectangle.Contains(e.Location))
           )
        {
            showingContextMenu = ShowDropDown();
        }
        else
        {
            // Raise the standard click event
            OnClick(EventArgs.Empty);

            // Raise event to indicate it was a mouse activated click
            OnMouseClick(e);
        }

        // If not showing a context menu then perform cleanup straight away
        if (!showingContextMenu)
        {
            ContextMenuClosed();
        }
    }

    private bool ShowDropDown()
    {
        // Show the calculator popup below the control
        Rectangle screenRect = RectangleToScreen(ClientRectangle);

        _popupCalculator = new VisualPopupCalculator(Value)
        {
            DismissedDelegate = OnCalculatorDismissed
        };

        var popupLocation = new Point(screenRect.X, screenRect.Bottom + 1);
        // Layout will size itself; give a sensible default
        _popupCalculator.Size = new Size(280, 360);
        _popupCalculator.Show(new Rectangle(popupLocation, _popupCalculator.Size));

        return true;
    }

    private KryptonContextMenuPositionH GetPositionH() => DropDownOrientation switch
    {
        VisualOrientation.Left => KryptonContextMenuPositionH.Before,
        VisualOrientation.Right => KryptonContextMenuPositionH.After,
        _ => KryptonContextMenuPositionH.Left
    };

    private KryptonContextMenuPositionV GetPositionV() => DropDownOrientation switch
    {
        VisualOrientation.Top => KryptonContextMenuPositionV.Above,
        VisualOrientation.Left or VisualOrientation.Right => KryptonContextMenuPositionV.Top,
        _ => KryptonContextMenuPositionV.Below
    };

    private void OnContextMenuClosed(object? sender, EventArgs e) => ContextMenuClosed();

    private void OnKryptonContextMenuClosed(object? sender, EventArgs e)
    {
        var kcm = sender as KryptonContextMenu ?? throw new ArgumentNullException(nameof(sender));
        kcm.Closed -= OnKryptonContextMenuClosed;
        ContextMenuClosed();
    }

    private void OnButtonSelect(object? sender, MouseEventArgs e)
    {
        // Take the focus if allowed
        if (CanFocus)
        {
            Focus();
        }
    }
    #endregion

    #region Calculator Popup
    private void OnCalculatorDismissed(object? sender, EventArgs e)
    {
        var calc = _popupCalculator;
        _popupCalculator = null;
        if (calc != null)
        {
            if (calc.Result.HasValue)
            {
                Value = calc.Result.Value;
            }
            calc.Dispose();
        }
        ContextMenuClosed();
    }

    private sealed class VisualPopupCalculator : VisualPopup
    {
        private readonly KryptonTextBox _display;
        private readonly System.Windows.Forms.TableLayoutPanel _layout;
        private readonly KryptonPanel _panel;
        private readonly KryptonButton[] _buttons;

        public decimal? Result { get; private set; }

        public VisualPopupCalculator(decimal initial)
            : base(true)
        {
            // Disable view-managed layout/painting; we host real WinForms/Krypton controls instead
            ViewManager = null;

            _panel = new KryptonPanel
            {
                Dock = DockStyle.Fill
            };

            _display = new KryptonTextBox
            {
                Dock = DockStyle.Top,
                Text = initial.ToString(System.Globalization.CultureInfo.CurrentCulture),
                StateCommon = { Content = { Padding = new Padding(4, 6, 4, 6) } }
            };

            _layout = new System.Windows.Forms.TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 5,
                Padding = new Padding(6),
                BackColor = Color.Transparent
            };

            _buttons = new KryptonButton[20];

            string[] captions =
            [
                "7","8","9","/",
                "4","5","6","*",
                "1","2","3","-",
                "0",".","C","+",
                "<-","(",")","="
            ];

            for (int i = 0; i < captions.Length; i++)
            {
                var btn = new KryptonButton
                {
                    Dock = DockStyle.Fill,
                    ButtonStyle = ButtonStyle.InputControl,
                    Values = { Text = captions[i] }
                };
                btn.Click += OnButtonClick;
                _buttons[i] = btn;
                _layout.Controls.Add(btn, i % 4, i / 4);
            }

            var container = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
            container.Controls.Add(_layout);
            container.Controls.Add(_display);
            _panel.Controls.Add(container);
            Controls.Add(_panel);
        }

        private void OnButtonClick(object? sender, EventArgs e)
        {
            if (sender is not KryptonButton kb)
            {
                return;
            }

            var t = kb.Values.Text;
            switch (t)
            {
                case "C":
                    _display.Text = string.Empty;
                    break;
                case "<-":
                    if (_display.Text.Length > 0)
                    {
                        _display.Text = _display.Text.Substring(0, _display.Text.Length - 1);
                    }
                    break;
                case "=":
                    TryComputeAndClose();
                    break;
                default:
                    _display.Text += t;
                    _display.SelectionStart = _display.Text.Length;
                    break;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                TryComputeAndClose();
                return true;
            }
            if (keyData == Keys.Escape)
            {
                Dispose();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void TryComputeAndClose()
        {
            var expr = _display.Text;
            if (string.IsNullOrWhiteSpace(expr))
            {
                Result = null;
                Dispose();
                return;
            }

            try
            {
                var dt = new System.Data.DataTable();
                object? v = dt.Compute(expr, null);
                if (v != null)
                {
                    decimal dec = Convert.ToDecimal(v, System.Globalization.CultureInfo.CurrentCulture);
                    Result = dec;
                }
            }
            catch
            {
                Result = null;
            }
            finally
            {
                Dispose();
            }
        }
    }
    #endregion
}
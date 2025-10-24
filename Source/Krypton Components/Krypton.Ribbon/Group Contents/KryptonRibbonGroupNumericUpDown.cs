#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Represents a ribbon group numeric up-down.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonRibbonGroupNumericUpDown), "ToolboxBitmaps.KryptonRibbonGroupNumericUpDown.bmp")]
[Designer(typeof(KryptonRibbonGroupNumericUpDownDesigner))]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultEvent(nameof(ValueChanged))]
[DefaultProperty(nameof(Value))]
public class KryptonRibbonGroupNumericUpDown : KryptonRibbonGroupItem
{
    #region Instance Fields
    private bool _visible;
    private bool _enabled;
    private string _keyTip;
    private GroupItemSize _itemSizeCurrent;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the value of the Value property changes.
    /// </summary>
    [Description(@"Occurs when the value of the Value property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? ValueChanged;

    /// <summary>
    /// Occurs when the control receives focus.
    /// </summary>
    [Browsable(false)]
    public event EventHandler? GotFocus;

    /// <summary>
    /// Occurs when the control loses focus.
    /// </summary>
    [Browsable(false)]
    public event EventHandler? LostFocus;

    /// <summary>
    /// Occurs when a key is pressed while the control has focus. 
    /// </summary>
    [Description(@"Occurs when a key is pressed while the control has focus.")]
    [Category(@"Key")]
    public event KeyPressEventHandler? KeyPress;

    /// <summary>
    /// Occurs when a key is released while the control has focus. 
    /// </summary>
    [Description(@"Occurs when a key is released while the control has focus.")]
    [Category(@"Key")]
    public event KeyEventHandler? KeyUp;

    /// <summary>
    /// Occurs when a key is pressed while the control has focus.
    /// </summary>
    [Description(@"Occurs when a key is pressed while the control has focus.")]
    [Category(@"Key")]
    public event KeyEventHandler? KeyDown;

    /// <summary>
    /// Occurs before the KeyDown event when a key is pressed while focus is on this control.
    /// </summary>
    [Description(@"Occurs before the KeyDown event when a key is pressed while focus is on this control.")]
    [Category(@"Key")]
    public event PreviewKeyDownEventHandler? PreviewKeyDown;

    /// <summary>
    /// Occurs after the value of a property has changed.
    /// </summary>
    [Category(@"Ribbon")]
    [Description(@"Occurs after the value of a property has changed.")]
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Occurs when the design time context menu is requested.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event MouseEventHandler? DesignTimeContextMenu;

    internal event EventHandler? MouseEnterControl;
    internal event EventHandler? MouseLeaveControl;
    #endregion

    #region Identity
    /// <summary>
    /// Initialise a new instance of the KryptonRibbonGroupNumericUpDown class.
    /// </summary>
    public KryptonRibbonGroupNumericUpDown()
    {
        // Default fields
        _visible = true;
        _enabled = true;
        _itemSizeCurrent = GroupItemSize.Medium;
        ShortcutKeys = Keys.None;
        _keyTip = "X";

        // Create the actual numeric up-down control and set initial settings
        NumericUpDown = new KryptonNumericUpDown
        {
            InputControlStyle = InputControlStyle.Ribbon,
            AlwaysActive = false,
            MinimumSize = new Size(121, 0),
            MaximumSize = new Size(121, 0),
            TabStop = false
        };

        // Hook into events to expose via this container
        NumericUpDown!.ValueChanged += OnNumericUpDownValueChanged;
        NumericUpDown.GotFocus += OnNumericUpDownGotFocus;
        NumericUpDown.LostFocus += OnNumericUpDownLostFocus;
        NumericUpDown.KeyDown += OnNumericUpDownKeyDown;
        NumericUpDown.KeyUp += OnNumericUpDownKeyUp;
        NumericUpDown.KeyPress += OnNumericUpDownKeyPress;
        NumericUpDown.PreviewKeyDown += OnNumericUpDownPreviewKeyDown;

        // Ensure we can track mouse events on the numeric up-down
        MonitorControl(NumericUpDown);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (NumericUpDown != null)
            {
                UnmonitorControl(NumericUpDown);
                NumericUpDown.Dispose();
                NumericUpDown = null;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public

    /// <summary>
    /// Gets access to the Wrapped Controls Tooltips.
    /// </summary>
    public override ToolTipValues ToolTipValues => NumericUpDown!.ToolTipValues;

    /// <summary>
    /// Gets access to the owning ribbon control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override KryptonRibbon? Ribbon
    {
        set
        {
            base.Ribbon = value;

            if (Ribbon is not null)
            {
                // Use the same palette in the numeric up-down as the ribbon, plus we need
                // to know when the ribbon palette changes so we can reflect that change
                NumericUpDown!.PaletteMode = Ribbon.PaletteMode;
                NumericUpDown.LocalCustomPalette = Ribbon.LocalCustomPalette;
                Ribbon.PaletteChanged += OnRibbonPaletteChanged;
            }
        }
    }

    /// <summary>
    /// Gets and sets the shortcut key combination.
    /// </summary>
    [Localizable(true)]
    [Category(@"Behavior")]
    [Description(@"Shortcut key combination to set focus to the numeric up-down.")]
    public Keys ShortcutKeys { get; set; }

    private bool ShouldSerializeShortcutKeys() => ShortcutKeys != Keys.None;

    /// <summary>
    /// Resets the ShortcutKeys property to its default value.
    /// </summary>
    public void ResetShortcutKeys() => ShortcutKeys = Keys.None;

    /// <summary>
    /// Access to the actual embedded KryptonNumericUpDown instance.
    /// </summary>
    [Description(@"Access to the actual embedded KryptonNumericUpDown instance.")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonNumericUpDown? NumericUpDown { get; private set; }

    /// <summary>
    /// Gets and sets the key tip for the ribbon group numeric up-down.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Ribbon group numeric up-down key tip.")]
    [DefaultValue("X")]
    public string KeyTip
    {
        get => _keyTip;

        set
        {
            if (string.IsNullOrEmpty(value))
            {
                value = @"X";
            }

            _keyTip = value.ToUpper();
        }
    }

    /// <summary>
    /// Gets or sets the number of decimal places to display.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Indicates the number of decimal places to display.")]
    [DefaultValue(0)]
    public int DecimalPlaces
    {
        get => NumericUpDown!.DecimalPlaces;
        set => NumericUpDown!.DecimalPlaces = value;
    }

    /// <summary>
    /// Gets or sets the amount to increment or decrement one each button click.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Indicates the amount to increment or decrement one each button click.")]
    [DefaultValue(typeof(decimal), "1")]
    public decimal Increment
    {
        get => NumericUpDown!.Increment;
        set => NumericUpDown!.Increment = value;
    }

    /// <summary>
    /// Gets or sets the maximum value for the numeric up-down control.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Indicates the maximum value for the numeric up-down control.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(typeof(decimal), "100")]
    public decimal Maximum
    {
        get => NumericUpDown!.Maximum;
        set => NumericUpDown!.Maximum = value;
    }

    /// <summary>
    /// Gets or sets the minimum value for the numeric up-down control.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Indicates the minimum value for the numeric up-down control.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(typeof(decimal), "0")]
    public decimal Minimum
    {
        get => NumericUpDown!.Minimum;
        set => NumericUpDown!.Minimum = value;
    }

    /// <summary>
    /// Gets or sets whether the thousands separator wil be inserted between each three decimal digits.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Indicates whether the thousands separator wil be inserted between each three decimal digits.")]
    [DefaultValue(false)]
    [Localizable(true)]
    public bool ThousandsSeparator
    {
        get => NumericUpDown!.ThousandsSeparator;
        set => NumericUpDown!.ThousandsSeparator = value;
    }

    /// <summary>
    /// Gets or sets the current value of the numeric up-down control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The current value of the numeric up-down control.")]
    [DefaultValue(typeof(decimal), "0")]
    [Bindable(true)]
    public decimal Value
    {
        get => NumericUpDown!.Value;
        set => NumericUpDown!.Value = value;
    }

    /// <summary>
    /// Gets or sets how the text should be aligned for edit controls.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates how the text should be aligned for edit controls.")]
    [DefaultValue(typeof(HorizontalAlignment), "Left")]
    [Localizable(true)]
    public HorizontalAlignment TextAlign
    {
        get => NumericUpDown!.TextAlign;
        set => NumericUpDown!.TextAlign = value;
    }

    /// <summary>
    /// Gets or sets wheather the numeric up-down should display its value in hexadecimal.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates wheather the numeric up-down should display its value in hexadecimal.")]
    [DefaultValue(false)]
    public bool Hexadecimal
    {
        get => NumericUpDown!.Hexadecimal;
        set => NumericUpDown!.Hexadecimal = value;
    }

    /// <summary>
    /// Gets or sets how the up-down control will position the up down buttons relative to its text box.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates how the up-down control will position the up down buttons relative to its text box.")]
    [DefaultValue(typeof(LeftRightAlignment), "Right")]
    [Localizable(true)]
    public LeftRightAlignment UpDownAlign
    {
        get => NumericUpDown!.UpDownAlign;
        set => NumericUpDown!.UpDownAlign = value;
    }

    /// <summary>
    /// Gets or sets whether the up-down control will increment and decrement the value when the UP ARROW and DOWN ARROW are used.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the up-down control will increment and decrement the value when the UP ARROW and DOWN ARROW are used.")]
    [DefaultValue(true)]
    public bool InterceptArrowKeys
    {
        get => NumericUpDown!.InterceptArrowKeys;
        set => NumericUpDown!.InterceptArrowKeys = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the text in the edit control can be changed or not.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Controls whether the text in the edit control can be changed or not.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(false)]
    public bool ReadOnly
    {
        get => NumericUpDown!.ReadOnly;
        set => NumericUpDown!.ReadOnly = value;
    }

    /// <summary>
    /// Gets the collection of button specifications.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Collection of button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonNumericUpDown.NumericUpDownButtonSpecCollection ButtonSpecs => NumericUpDown!.ButtonSpecs;

    /// <summary>
    /// Gets and sets the visible state of the numeric up-down.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the numeric up-down is visible or hidden.")]
    [DefaultValue(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override bool Visible
    {
        get => _visible;

        set
        {
            if (value != _visible)
            {
                _visible = value;
                OnPropertyChanged(nameof(Visible));
            }
        }
    }

    /// <summary>
    /// Make the ribbon group numeric up-down visible.
    /// </summary>
    public void Show() => Visible = true;

    /// <summary>
    /// Make the ribbon group numeric up-down hidden.
    /// </summary>
    public void Hide() => Visible = false;

    /// <summary>
    /// Gets and sets the enabled state of the group numeric up-down.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the group numeric up-down is enabled.")]
    [DefaultValue(true)]
    public bool Enabled
    {
        get => _enabled;

        set
        {
            if (_enabled != value)
            {
                _enabled = value;
                OnPropertyChanged(nameof(Enabled));
            }
        }
    }

    /// <summary>
    /// Gets or sets the minimum size of the control.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Specifies the minimum size of the control.")]
    [DefaultValue(typeof(Size), "121, 0")]
    public Size MinimumSize
    {
        get => NumericUpDown!.MinimumSize;
        set => NumericUpDown!.MinimumSize = value;
    }

    /// <summary>
    /// Gets or sets the maximum size of the control.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Specifies the maximum size of the control.")]
    [DefaultValue(typeof(Size), "121, 0")]
    public Size MaximumSize
    {
        get => NumericUpDown!.MaximumSize;
        set => NumericUpDown!.MaximumSize = value;
    }

    /// <summary>
    /// Gets and sets the associated context menu strip.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The shortcut to display when the user right-clicks the control.")]
    [DefaultValue(null)]
    public ContextMenuStrip? ContextMenuStrip
    {
        get => NumericUpDown!.ContextMenuStrip;
        set => NumericUpDown!.ContextMenuStrip = value;
    }

    /// <summary>
    /// Gets and sets the KryptonContextMenu for showing when the numeric up down is right clicked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"KryptonContextMenu to be shown when the numeric up down is right clicked.")]
    [DefaultValue(null)]
    public KryptonContextMenu? KryptonContextMenu
    {
        get => NumericUpDown!.KryptonContextMenu;
        set => NumericUpDown!.KryptonContextMenu = value;
    }

    /// <summary>
    /// Gets and sets a value indicating if tooltips should be Displayed for button specs.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should tooltips be Displayed for button specs.")]
    [DefaultValue(false)]
    public bool AllowButtonSpecToolTips
    {
        get => NumericUpDown!.AllowButtonSpecToolTips;
        set => NumericUpDown!.AllowButtonSpecToolTips = value;
    }

    /// <summary>
    /// Gets and sets a value indicating if button spec tooltips should remove the parent tooltip.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should button spec tooltips should remove the parent tooltip")]
    [DefaultValue(false)]
    public bool AllowButtonSpecToolTipPriority
    {
        get => NumericUpDown!.AllowButtonSpecToolTipPriority;
        set => NumericUpDown!.AllowButtonSpecToolTipPriority = value;
    }

    /// <summary>
    /// Selects a range of text in the control.
    /// </summary>
    /// <param name="start">The position of the first character in the current text selection within the text box.</param>
    /// <param name="length">The number of characters to select.</param>
    public void Select(int start, int length) => NumericUpDown!.Select(start, length);

    /// <summary>
    /// Gets and sets the maximum allowed size of the item.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override GroupItemSize ItemSizeMaximum
    {
        get => GroupItemSize.Large;
        set { }
    }

    /// <summary>
    /// Gets and sets the minimum allowed size of the item.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override GroupItemSize ItemSizeMinimum
    {
        get => GroupItemSize.Small;
        set { }
    }

    /// <summary>
    /// Gets and sets the current item size.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override GroupItemSize ItemSizeCurrent
    {
        get => _itemSizeCurrent;

        set
        {
            if (_itemSizeCurrent != value)
            {
                _itemSizeCurrent = value;
                OnPropertyChanged(nameof(ItemSizeCurrent));
            }
        }
    }

    /// <summary>
    /// Creates an appropriate view element for this item.
    /// </summary>
    /// <param name="ribbon">Reference to the owning ribbon control.</param>
    /// <param name="needPaint">Delegate for notifying changes in display.</param>
    /// <returns>ViewBase derived instance.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override ViewBase CreateView(KryptonRibbon ribbon,
        NeedPaintHandler needPaint) =>
        new ViewDrawRibbonGroupNumericUpDown(ribbon, this, needPaint);

    /// <summary>
    /// Gets and sets the associated designer.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public IKryptonDesignObject? NumericUpDownDesigner { get; set; }

    /// <summary>
    /// Internal design time properties.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public ViewBase? NumericUpDownView { get; set; }

    #endregion

    #region Protected Virtual
    /// <summary>
    /// Raises the GotFocus event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnGotFocus(EventArgs e) => GotFocus?.Invoke(this, e);

    /// <summary>
    /// Raises the LostFocus event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnLostFocus(EventArgs e) => LostFocus?.Invoke(this, e);

    /// <summary>
    /// Raises the KeyDown event.
    /// </summary>
    /// <param name="e">An KeyEventArgs containing the event data.</param>
    protected virtual void OnKeyDown(KeyEventArgs e) => KeyDown?.Invoke(this, e);

    /// <summary>
    /// Raises the KeyUp event.
    /// </summary>
    /// <param name="e">An KeyEventArgs containing the event data.</param>
    protected virtual void OnKeyUp(KeyEventArgs e) => KeyUp?.Invoke(this, e);

    /// <summary>
    /// Raises the KeyPress event.
    /// </summary>
    /// <param name="e">An KeyPressEventArgs containing the event data.</param>
    protected virtual void OnKeyPress(KeyPressEventArgs e) => KeyPress?.Invoke(this, e);

    /// <summary>
    /// Raises the PreviewKeyDown event.
    /// </summary>
    /// <param name="e">An PreviewKeyDownEventArgs containing the event data.</param>
    protected virtual void OnPreviewKeyDown(PreviewKeyDownEventArgs e) => PreviewKeyDown?.Invoke(this, e);

    /// <summary>
    /// Raises the ValueChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnValueChanged(EventArgs e) => ValueChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">Name of property that has changed.</param>
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion

    #region Internal
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal Control? LastParentControl { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal KryptonNumericUpDown? LastNumericUpDown { get; set; }

    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    internal NeedPaintHandler? ViewPaintDelegate { get; set; }

    internal void OnDesignTimeContextMenu(MouseEventArgs e) => DesignTimeContextMenu?.Invoke(this, e);

    internal override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        // Only interested in key processing if this control definition 
        // is enabled and itself and all parents are also visible
        if (Enabled && ChainVisible)
        {
            // Do we have a shortcut definition for ourself?
            if (ShortcutKeys != Keys.None)
            {
                // Does it match the incoming key combination?
                if (ShortcutKeys == keyData)
                {
                    // Can the numeric up-down take the focus
                    if (LastNumericUpDown is { CanFocus: true })
                    {
                        LastNumericUpDown.NumericUpDown!.Focus();
                    }

                    return true;
                }
            }
        }

        return false;
    }

    #endregion

    #region Implementation
    private void MonitorControl(KryptonNumericUpDown c)
    {
        c.MouseEnter += OnControlEnter;
        c.MouseLeave += OnControlLeave;
        c.TrackMouseEnter += OnControlEnter;
        c.TrackMouseLeave += OnControlLeave;
    }

    private void UnmonitorControl(KryptonNumericUpDown c)
    {
        c.MouseEnter -= OnControlEnter;
        c.MouseLeave -= OnControlLeave;
        c.TrackMouseEnter -= OnControlEnter;
        c.TrackMouseLeave -= OnControlLeave;
    }

    private void OnControlEnter(object? sender, EventArgs e) => MouseEnterControl?.Invoke(this, e);

    private void OnControlLeave(object? sender, EventArgs e) => MouseLeaveControl?.Invoke(this, e);

    private void OnPaletteNeedPaint(object sender, NeedLayoutEventArgs e) =>
        // Pass request onto the view provided paint delegate
        ViewPaintDelegate?.Invoke(this, e);

    private void OnNumericUpDownValueChanged(object? sender, EventArgs e) => OnValueChanged(e);

    private void OnNumericUpDownGotFocus(object? sender, EventArgs e) => OnGotFocus(e);

    private void OnNumericUpDownLostFocus(object? sender, EventArgs e) => OnLostFocus(e);

    private void OnNumericUpDownKeyPress(object? sender, KeyPressEventArgs e) => OnKeyPress(e);

    private void OnNumericUpDownKeyUp(object? sender, KeyEventArgs e) => OnKeyUp(e);

    private void OnNumericUpDownKeyDown(object? sender, KeyEventArgs e) => OnKeyDown(e);

    private void OnNumericUpDownPreviewKeyDown(object? sender, PreviewKeyDownEventArgs e) => OnPreviewKeyDown(e);

    private void OnRibbonPaletteChanged(object? sender, EventArgs e)
    {
        NumericUpDown!.PaletteMode = Ribbon!.PaletteMode;
        NumericUpDown.LocalCustomPalette = Ribbon.LocalCustomPalette;
    }

    #endregion
}
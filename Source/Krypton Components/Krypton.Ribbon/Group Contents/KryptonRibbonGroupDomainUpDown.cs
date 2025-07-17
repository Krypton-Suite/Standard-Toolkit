#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Represents a ribbon group domain up-down.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonRibbonGroupDomainUpDown), "ToolboxBitmaps.KryptonRibbonGroupDomainUpDown.bmp")]
[Designer(typeof(KryptonRibbonGroupDomainUpDownDesigner))]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultEvent(nameof(SelectedItemChanged))]
[DefaultProperty(nameof(Items))]
public class KryptonRibbonGroupDomainUpDown : KryptonRibbonGroupItem
{
    #region Instance Fields
    private bool _visible;
    private bool _enabled;
    private string _keyTip;
    private GroupItemSize _itemSizeCurrent;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the value of the SelectedItem property changes.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the value of the SelectedItem property changes.")]
    public event EventHandler? SelectedItemChanged;

    /// <summary>
    /// Occurs when the user scrolls the scroll box.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the user scrolls the scroll box.")]
    public event ScrollEventHandler? Scroll;

    /// <summary>
    /// Occurs when the value of the Text property changes.
    /// </summary>
    [Description(@"Occurs when the value of the Text property changes.")]
    [Category(@"Property Changed")]
    public event EventHandler? TextChanged;

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
    /// Initialise a new instance of the KryptonRibbonGroupDomainUpDown class.
    /// </summary>
    public KryptonRibbonGroupDomainUpDown()
    {
        // Default fields
        _visible = true;
        _enabled = true;
        _itemSizeCurrent = GroupItemSize.Medium;
        ShortcutKeys = Keys.None;
        _keyTip = "X";

        // Create the actual domain up-down control and set initial settings
        DomainUpDown = new KryptonDomainUpDown
        {
            InputControlStyle = InputControlStyle.Ribbon,
            AlwaysActive = false,
            MinimumSize = new Size(121, 0),
            MaximumSize = new Size(121, 0),
            TabStop = false
        };

        // Hook into events to expose via this container
        DomainUpDown.Scroll += OnDomainUpDownScroll;
        DomainUpDown.SelectedItemChanged += OnDomainUpDownSelectedItemChanged;
        DomainUpDown.GotFocus += OnDomainUpDownGotFocus;
        DomainUpDown.LostFocus += OnDomainUpDownLostFocus;
        DomainUpDown.KeyDown += OnDomainUpDownKeyDown;
        DomainUpDown.KeyUp += OnDomainUpDownKeyUp;
        DomainUpDown.KeyPress += OnDomainUpDownKeyPress;
        DomainUpDown.PreviewKeyDown += OnDomainUpDownPreviewKeyDown;
        DomainUpDown.TextChanged += OnDomainUpDownTextChanged;

        // Ensure we can track mouse events on the domain up-down
        MonitorControl(DomainUpDown);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (DomainUpDown != null)
            {
                UnmonitorControl(DomainUpDown);
                DomainUpDown.Dispose();
                DomainUpDown = null;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
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

            if (value != null)
            {
                // Use the same palette in the domain up-down as the ribbon, plus we need
                // to know when the ribbon palette changes so we can reflect that change
                DomainUpDown!.PaletteMode = Ribbon!.PaletteMode;
                DomainUpDown.LocalCustomPalette = Ribbon.LocalCustomPalette;
                Ribbon.PaletteChanged += OnRibbonPaletteChanged;
            }
        }
    }

    /// <summary>
    /// Gets or sets the index value of the selected item. 
    /// </summary>
    [Browsable(false)]
    [DefaultValue(-1)]
    public int SelectedIndex
    {
        get => DomainUpDown!.SelectedIndex;
        set => DomainUpDown!.SelectedIndex = value;
    }

    /// <summary>
    /// Gets or sets the selected item based on the index value of the selected item in the collection.  
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object? SelectedItem
    {
        get => DomainUpDown!.SelectedItem;
        set => DomainUpDown!.SelectedItem = value;
    }

    /// <summary>
    /// Gets and sets the text associated with the control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Text associated with the control.")]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string Text
    {
        get => DomainUpDown!.Text;
        set => DomainUpDown!.Text = value;
    }

    /// <summary>
    /// Gets and sets the shortcut key combination.
    /// </summary>
    [Localizable(true)]
    [Category(@"Behavior")]
    [Description(@"Shortcut key combination to set focus to the domain up-down.")]
    public Keys ShortcutKeys { get; set; }

    private bool ShouldSerializeShortcutKeys() => ShortcutKeys != Keys.None;

    /// <summary>
    /// Resets the ShortcutKeys property to its default value.
    /// </summary>
    public void ResetShortcutKeys() => ShortcutKeys = Keys.None;

    /// <summary>
    /// Gets or the collection of allowable items of the domain up down.
    /// </summary>
    [Category(@"Data")]
    [Description(@"The allowable items of the domain up down.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(@"System.Windows.Forms.Design.StringCollectionEditor", typeof(UITypeEditor))]
    [Localizable(true)]
    public DomainUpDown.DomainUpDownItemCollection Items => DomainUpDown!.Items;

    /// <summary>
    /// Access to the actual embedded KryptonDomainUpDown instance.
    /// </summary>
    [Description(@"Access to the actual embedded KryptonDomainUpDown instance.")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonDomainUpDown? DomainUpDown { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether the item collection is sorted.   
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Controls whether items in the domain list are sorted.")]
    [DefaultValue(false)]
    public bool Sorted
    {
        get => DomainUpDown!.Sorted;
        set => DomainUpDown!.Sorted = value;
    }

    /// <summary>
    /// Gets and sets the key tip for the ribbon group domain up-down.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Ribbon group domain up-down key tip.")]
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
    /// Gets or sets how the text should be aligned for edit controls.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates how the text should be aligned for edit controls.")]
    [DefaultValue(typeof(HorizontalAlignment), "Left")]
    [Localizable(true)]
    public HorizontalAlignment TextAlign
    {
        get => DomainUpDown!.TextAlign;
        set => DomainUpDown!.TextAlign = value;
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
        get => DomainUpDown!.UpDownAlign;
        set => DomainUpDown!.UpDownAlign = value;
    }

    /// <summary>
    /// Gets or sets whether the up-down control will increment and decrement the value when the UP ARROW and DOWN ARROW are used.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the up-down control will increment and decrement the value when the UP ARROW and DOWN ARROW are used.")]
    [DefaultValue(true)]
    public bool InterceptArrowKeys
    {
        get => DomainUpDown!.InterceptArrowKeys;
        set => DomainUpDown!.InterceptArrowKeys = value;
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
        get => DomainUpDown!.ReadOnly;
        set => DomainUpDown!.ReadOnly = value;
    }

    /// <summary>
    /// Gets the collection of button specifications.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Collection of button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonDomainUpDown.DomainUpDownButtonSpecCollection ButtonSpecs => DomainUpDown!.ButtonSpecs;

    /// <summary>
    /// Gets and sets the visible state of the domain up-down.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the domain up-down is visible or hidden.")]
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
    /// Make the ribbon group domain up-down visible.
    /// </summary>
    public void Show() => Visible = true;

    /// <summary>
    /// Make the ribbon group domain up-down hidden.
    /// </summary>
    public void Hide() => Visible = false;

    /// <summary>
    /// Gets and sets the enabled state of the group domain up-down.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the group domain up-down is enabled.")]
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
        get => DomainUpDown!.MinimumSize;
        set => DomainUpDown!.MinimumSize = value;
    }

    /// <summary>
    /// Gets or sets the maximum size of the control.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Specifies the maximum size of the control.")]
    [DefaultValue(typeof(Size), "121, 0")]
    public Size MaximumSize
    {
        get => DomainUpDown!.MaximumSize;
        set => DomainUpDown!.MaximumSize = value;
    }

    /// <summary>
    /// Gets and sets the associated context menu strip.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The shortcut to display when the user right-clicks the control.")]
    [DefaultValue(null)]
    public ContextMenuStrip? ContextMenuStrip
    {
        get => DomainUpDown!.ContextMenuStrip;
        set => DomainUpDown!.ContextMenuStrip = value;
    }

    /// <summary>
    /// Gets and sets the KryptonContextMenu for showing when the domain up down is right clicked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"KryptonContextMenu to be shown when the domain up down is right clicked.")]
    [DefaultValue(null)]
    public KryptonContextMenu? KryptonContextMenu
    {
        get => DomainUpDown!.KryptonContextMenu;
        set => DomainUpDown!.KryptonContextMenu = value;
    }

    /// <summary>
    /// Gets access to the Wrapped Controls Tooltips.
    /// </summary>
    public override ToolTipValues ToolTipValues => DomainUpDown!.ToolTipValues;


    /// <summary>
    /// Gets and sets a value indicating if tooltips should be Displayed for button specs.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should tooltips be Displayed for button specs.")]
    [DefaultValue(false)]
    public bool AllowButtonSpecToolTips
    {
        get => DomainUpDown!.AllowButtonSpecToolTips;
        set => DomainUpDown!.AllowButtonSpecToolTips = value;
    }

    /// <summary>
    /// Gets and sets a value indicating if button spec tooltips should remove the parent tooltip.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should button spec tooltips should remove the parent tooltip")]
    [DefaultValue(false)]
    public bool AllowButtonSpecToolTipPriority
    {
        get => DomainUpDown!.AllowButtonSpecToolTipPriority;
        set => DomainUpDown!.AllowButtonSpecToolTipPriority = value;
    }

    /// <summary>
    /// Selects a range of text in the control.
    /// </summary>
    /// <param name="start">The position of the first character in the current text selection within the text box.</param>
    /// <param name="length">The number of characters to select.</param>
    public void Select(int start, int length) => DomainUpDown!.Select(start, length);

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
        new ViewDrawRibbonGroupDomainUpDown(ribbon, this, needPaint);

    /// <summary>
    /// Gets and sets the associated designer.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public IKryptonDesignObject DomainUpDownDesigner { get; set; }

    /// <summary>
    /// Internal design time properties.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public ViewBase? DomainUpDownView { get; set; }

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
    /// Raises the SelectedItemChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnSelectedItemChanged(EventArgs e) => SelectedItemChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the TextChanged event.
    /// </summary>
    /// <param name="e">A EventArgs that contains the event data.</param>
    protected virtual void OnTextChanged(EventArgs e) => TextChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the SelectedItemChanged event.
    /// </summary>
    /// <param name="e">A ScrollEventArgs that contains the event data.</param>
    protected virtual void OnScroll(ScrollEventArgs e) => Scroll?.Invoke(this, e);

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">Name of property that has changed.</param>
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion

    #region Internal
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal Control LastParentControl { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal KryptonDomainUpDown? LastDomainUpDown { get; set; }

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
                    // Can the domain up-down take the focus
                    if (LastDomainUpDown is { CanFocus: true })
                    {
                        LastDomainUpDown.DomainUpDown.Focus();
                    }

                    return true;
                }
            }
        }

        return false;
    }
    #endregion

    #region Implementation
    private void MonitorControl(KryptonDomainUpDown c)
    {
        c.MouseEnter += OnControlEnter;
        c.MouseLeave += OnControlLeave;
        c.TrackMouseEnter += OnControlEnter;
        c.TrackMouseLeave += OnControlLeave;
    }

    private void UnmonitorControl(KryptonDomainUpDown c)
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

    private void OnDomainUpDownScroll(object? sender, ScrollEventArgs e) => OnScroll(e);

    private void OnDomainUpDownSelectedItemChanged(object? sender, EventArgs e) => OnSelectedItemChanged(e);

    private void OnDomainUpDownTextChanged(object? sender, EventArgs e) => OnTextChanged(e);

    private void OnDomainUpDownGotFocus(object? sender, EventArgs e) => OnGotFocus(e);

    private void OnDomainUpDownLostFocus(object? sender, EventArgs e) => OnLostFocus(e);

    private void OnDomainUpDownKeyPress(object? sender, KeyPressEventArgs e) => OnKeyPress(e);

    private void OnDomainUpDownKeyUp(object? sender, KeyEventArgs e) => OnKeyUp(e);

    private void OnDomainUpDownKeyDown(object? sender, KeyEventArgs e) => OnKeyDown(e);

    private void OnDomainUpDownPreviewKeyDown(object? sender, PreviewKeyDownEventArgs e) => OnPreviewKeyDown(e);

    private void OnRibbonPaletteChanged(object? sender, EventArgs e)
    {
        DomainUpDown!.PaletteMode = Ribbon!.PaletteMode;
        DomainUpDown.LocalCustomPalette = Ribbon.LocalCustomPalette;
    }

    #endregion
}
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
/// Represents a ribbon group track bar.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonRibbonGroupTrackBar), "ToolboxBitmaps.KryptonRibbonGroupTrackBar.bmp")]
[Designer(typeof(KryptonRibbonGroupTrackBarDesigner))]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultEvent(nameof(ValueChanged))]
[DefaultProperty(nameof(Value))]
public class KryptonRibbonGroupTrackBar : KryptonRibbonGroupItem
{
    #region Instance Fields
    private bool _visible;
    private bool _enabled;
    private string _keyTip;
    private int _minimumLength;
    private int _maximumLength;
    private GroupItemSize _itemSizeCurrent;

    #endregion

    #region Events
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
    /// Occurs when the value of the Value property changes.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the value of the Value property changes.")]
    public event EventHandler? ValueChanged;

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
    /// Initialise a new instance of the KryptonRibbonGroupTrackBar class.
    /// </summary>
    public KryptonRibbonGroupTrackBar()
    {
        // Default fields
        _visible = true;
        _enabled = true;
        _itemSizeCurrent = GroupItemSize.Medium;
        _keyTip = "T";
        _minimumLength = 55;
        _maximumLength = 55;

        // Create the actual track barcontrol and set initial settings
        TrackBar = new KryptonTrackBar
        {
            DrawBackground = false,
            TickStyle = TickStyle.None,
            MinimumSize = new Size(_minimumLength, 0),
            MaximumSize = new Size(_maximumLength, 0),
            TabStop = false
        };

        // Hook into events to expose via this container
        TrackBar.GotFocus += OnTrackBarGotFocus;
        TrackBar.LostFocus += OnTrackBarLostFocus;
        TrackBar.ValueChanged += OnTrackBarValueChanged;

        // Ensure we can track mouse events on the track bar
        MonitorControl(TrackBar);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (TrackBar != null)
            {
                UnmonitorControl(TrackBar);
                TrackBar.Dispose();
                TrackBar = null;
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

            if (Ribbon != null)
            {
                // Use the same palette in the track bar as the ribbon, plus we need
                // to know when the ribbon palette changes so we can reflect that change
                TrackBar!.PaletteMode = Ribbon!.PaletteMode;
                TrackBar.LocalCustomPalette = Ribbon!.LocalCustomPalette;
                Ribbon.PaletteChanged += OnRibbonPaletteChanged;
            }
        }
    }

    /// <summary>
    /// Access to the actual embedded KryptonTrackBar instance.
    /// </summary>
    [Description(@"Access to the actual embedded KryptonTrackBar instance.")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonTrackBar? TrackBar { get; private set; }

    /// <summary>
    /// Gets access to the Wrapped Controls Tooltips.
    /// </summary>
    public override ToolTipValues ToolTipValues => TrackBar?.ToolTipValues!;

    /// <summary>
    /// Gets and sets the key tip for the ribbon group track bar.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Ribbon group track bar key tip.")]
    [DefaultValue("T")]
    public string KeyTip
    {
        get => _keyTip;

        set
        {
            if (string.IsNullOrEmpty(value))
            {
                value = @"T";
            }

            _keyTip = value.ToUpper();
        }
    }

    /// <summary>
    /// Gets and sets the visible state of the track bar.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the track bar is visible or hidden.")]
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
    /// Make the ribbon group track bar visible.
    /// </summary>
    public void Show() => Visible = true;

    /// <summary>
    /// Make the ribbon group track bar hidden.
    /// </summary>
    public void Hide() => Visible = false;

    /// <summary>
    /// Gets and sets the enabled state of the group track bar.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the group track bar is enabled.")]
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
    /// Gets or sets the minimum length of the control.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Specifies the minimum length of the control.")]
    [DefaultValue("55")]
    public int MinimumLength
    {
        get => _minimumLength;

        set
        {
            _minimumLength = value;
            TrackBar!.MinimumSize = Orientation == Orientation.Horizontal
                ? new Size(_minimumLength, 0)
                : new Size(0, _minimumLength);
        }
    }

    /// <summary>
    /// Gets or sets the maximum length of the control.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Specifies the maximum length of the control.")]
    [DefaultValue("50")]
    public int MaximumLength
    {
        get => _maximumLength;

        set
        {
            _maximumLength = value;
            TrackBar!.MaximumSize = Orientation == Orientation.Horizontal
                ? new Size(_maximumLength, 0)
                : new Size(0, _maximumLength);
        }
    }

    /// <summary>
    /// Gets and sets the associated context menu strip.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The shortcut to display when the user right-clicks the control.")]
    [DefaultValue(null)]
    public ContextMenuStrip? ContextMenuStrip
    {
        get => TrackBar?.ContextMenuStrip;
        set => TrackBar!.ContextMenuStrip = value;
    }

    /// <summary>
    /// Gets and sets the KryptonContextMenu for showing when the text box is right clicked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"KryptonContextMenu to be shown when the text box is right clicked.")]
    [DefaultValue(null)]
    public KryptonContextMenu? KryptonContextMenu
    {
        get => TrackBar?.KryptonContextMenu;
        set => TrackBar!.KryptonContextMenu = value;
    }

    /// <summary>
    /// Gets and sets the size of the track bar elements.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Determines size of the track bar elements.")]
    [DefaultValue(typeof(PaletteTrackBarSize), "Medium")]
    public PaletteTrackBarSize TrackBarSize
    {
        get => TrackBar!.TrackBarSize;
        set => TrackBar!.TrackBarSize = value;
    }

    /// <summary>
    /// Gets or sets a value indicating how to display the tick marks on the track bar.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Determines where tick marks are Displayed.")]
    [DefaultValue(typeof(TickStyle), "None")]
    [RefreshProperties(RefreshProperties.All)]
    public TickStyle TickStyle
    {
        get => TrackBar!.TickStyle;
        set => TrackBar!.TickStyle = value;
    }

    /// <summary>
    /// Gets or sets a value that specifies the delta between ticks drawn on the control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Determines the frequency of tick marks.")]
    [DefaultValue(1)]
    public int TickFrequency
    {
        get => TrackBar!.TickFrequency;
        set => TrackBar!.TickFrequency = value;
    }

    /// <summary>
    /// Gets and sets if the control displays like a volume control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Determines if the control display like a volume control.")]
    [DefaultValue(false)]
    public bool VolumeControl
    {
        get => TrackBar!.VolumeControl;
        set => TrackBar!.VolumeControl = value;
    }

    /// <summary>
    /// Gets or sets a value indicating the horizontal or vertical orientation of the track bar.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Background style.")]
    [DefaultValue(typeof(Orientation), "Horizontal")]
    [RefreshProperties(RefreshProperties.All)]
    public Orientation Orientation
    {
        get => TrackBar!.Orientation;

        set
        {
            if (value != TrackBar!.Orientation)
            {
                TrackBar.Orientation = value;

                if (Orientation == Orientation.Horizontal)
                {
                    TrackBar.MinimumSize = new Size(_minimumLength, 0);
                    TrackBar.MaximumSize = new Size(_maximumLength, 0);
                }
                else
                {
                    TrackBar.MinimumSize = new Size(0, _minimumLength);
                    TrackBar.MaximumSize = new Size(0, _maximumLength);
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets the upper limit of the range this trackbar is working with.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Upper limit of the trackbar range.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(10)]
    public int Maximum
    {
        get => TrackBar!.Maximum;
        set => TrackBar!.Maximum = value;
    }

    /// <summary>
    /// Gets or sets the lower limit of the range this trackbar is working with.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Lower limit of the trackbar range.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(0)]
    public int Minimum
    {
        get => TrackBar!.Minimum;
        set => TrackBar!.Minimum = value;
    }

    /// <summary>
    /// Gets or sets a numeric value that represents the current position of the scroll box on the track bar.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Current position of the indicator within the trackbar.")]
    [DefaultValue(0)]
    public int Value
    {
        get => TrackBar!.Value;
        set => TrackBar!.Value = value;
    }

    /// <summary>
    /// Gets or sets the value added to or subtracted from the Value property when the scroll box is moved a small distance.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Change to apply when a small change occurs.")]
    [DefaultValue(1)]
    [DisallowNull]
    public int SmallChange
    {
        get => TrackBar!.SmallChange;
        set => TrackBar!.SmallChange = value;
    }

    /// <summary>
    /// Gets or sets a value to be added to or subtracted from the Value property when the scroll box is moved a large distance.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Change to apply when a large change occurs.")]
    [DefaultValue(5)]
    [DisallowNull]
    public int LargeChange
    {
        get => TrackBar!.LargeChange;
        set => TrackBar!.LargeChange = value;
    }

    /// <summary>
    /// Sets the minimum and maximum values for a TrackBar.
    /// </summary>
    /// <param name="minValue">The lower limit of the range of the track bar.</param>
    /// <param name="maxValue">The upper limit of the range of the track bar.</param>
    public void SetRange(int minValue, int maxValue) => TrackBar?.SetRange(minValue, maxValue);

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
        new ViewDrawRibbonGroupTrackBar(ribbon, this, needPaint);

    /// <summary>
    /// Gets and sets the associated designer.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public IKryptonDesignObject TrackBarDesigner { get; set; }

    /// <summary>
    /// Internal design time properties.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public ViewBase? TrackBarView { get; set; }

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
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">Name of property that has changed.</param>
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion

    #region Internal
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal Control? LastParentControl { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal KryptonTrackBar? LastTrackBar { get; set; }

    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    internal NeedPaintHandler? ViewPaintDelegate { get; set; }

    internal void OnDesignTimeContextMenu(MouseEventArgs e) => DesignTimeContextMenu?.Invoke(this, e);

    internal override bool ProcessCmdKey(ref Message msg, Keys keyData) => false;

    #endregion

    #region Implementation
    private void MonitorControl(KryptonTrackBar? c)
    {
        c!.MouseEnter += OnControlEnter;
        c.MouseLeave += OnControlLeave;
    }

    private void UnmonitorControl(KryptonTrackBar? c)
    {
        c!.MouseEnter -= OnControlEnter;
        c.MouseLeave -= OnControlLeave;
    }

    private void OnControlEnter(object? sender, EventArgs e) => MouseEnterControl?.Invoke(this, e);

    private void OnControlLeave(object? sender, EventArgs e) => MouseLeaveControl?.Invoke(this, e);

    private void OnPaletteNeedPaint(object sender, NeedLayoutEventArgs e) =>
        // Pass request onto the view provided paint delegate
        ViewPaintDelegate?.Invoke(this, e);

    private void OnTrackBarGotFocus(object? sender, EventArgs e) => OnGotFocus(e);

    private void OnTrackBarLostFocus(object? sender, EventArgs e) => OnLostFocus(e);

    private void OnTrackBarValueChanged(object? sender, EventArgs e) => ValueChanged?.Invoke(this, e);

    private void OnRibbonPaletteChanged(object? sender, EventArgs e)
    {
        TrackBar!.PaletteMode = Ribbon!.PaletteMode;
        TrackBar.LocalCustomPalette = Ribbon!.LocalCustomPalette;
    }

    #endregion
}
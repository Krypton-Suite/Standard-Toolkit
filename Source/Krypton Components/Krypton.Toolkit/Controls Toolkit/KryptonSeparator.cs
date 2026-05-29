#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Display a separator with generated events to operation.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonSeparator), "ToolboxBitmaps.KryptonSeparator.bmp")]
[DefaultEvent(nameof(SplitterMoved))]
[DefaultProperty(nameof(Orientation))]
[DesignerCategory(@"code")]
[Designer(typeof(KryptonSeparatorDesigner))]
[Description(@"Display a separator generated events to operation.")]
public class KryptonSeparator : VisualControl,
    ISeparatorSource
{
    #region Instance Fields
    private SeparatorStyle _style;
    private readonly ViewDrawDocker _drawDocker;
    private readonly ViewDrawSeparator _drawSeparator;
    private readonly SeparatorController _separatorController;
    private Orientation _orientation;
    private System.Windows.Forms.Timer? _redrawTimer;
    private Point _designLastPt;
    private int _splitterWidth;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the value of the AutoSize property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new event EventHandler? AutoSizeChanged;

    /// <summary>
    /// Occurs when the value of the BackgroundImage property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new event EventHandler? BackgroundImageChanged;

    /// <summary>
    /// Occurs when the value of the BackgroundImageLayout property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new event EventHandler? BackgroundImageLayoutChanged;

    /// <summary>
    /// Occurs when the value of the ControlAdded property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new event ControlEventHandler? ControlAdded;

    /// <summary>
    /// Occurs when the value of the ControlRemoved property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new event ControlEventHandler? ControlRemoved;

    /// <summary>
    /// Occurs when the separator is about to be moved and requests the rectangle of allowed movement.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the separator is about to be moved and requests the rectangle of allowed movement.")]
    public event EventHandler<SplitterMoveRectMenuArgs>? SplitterMoveRect;

    /// <summary>
    /// Occurs when the separator move finishes and a move has occurred.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the separator move finishes and a move has occurred.")]
    public event SplitterEventHandler? SplitterMoved;

    /// <summary>
    /// Occurs when the separator move finishes and a move has not occurred.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the separator move finishes and a move has not occurred.")]
    public event EventHandler? SplitterNotMoved;

    /// <summary>
    /// Occurs when the separator is currently in the process of moving.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the separator is currently in the process of moving.")]
    public event SplitterCancelEventHandler? SplitterMoving;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonSeparator class.
    /// </summary>
    public KryptonSeparator()
    {
        // The label cannot take the focus
        SetStyle(ControlStyles.Selectable, false);

        // Create the palette storage
        StateCommon = new PaletteSplitContainerRedirect(Redirector, PaletteBackStyle.PanelClient,
            PaletteBorderStyle.ControlClient, PaletteBackStyle.SeparatorHighProfile,
            PaletteBorderStyle.SeparatorHighProfile, NeedPaintDelegate)
        {
            BorderRedirect = { OverrideBorderToFalse = true }
        };

        // Never draw the border around the background

        StateDisabled = new PaletteSplitContainer(StateCommon, StateCommon.Separator, StateCommon.Separator, NeedPaintDelegate);
        StateNormal = new PaletteSplitContainer(StateCommon, StateCommon.Separator, StateCommon.Separator, NeedPaintDelegate);
        StateTracking = new PaletteSeparatorPadding(StateCommon.Separator, StateCommon.Separator, NeedPaintDelegate);
        StatePressed = new PaletteSeparatorPadding(StateCommon.Separator, StateCommon.Separator, NeedPaintDelegate);

        // Our view contains just a simple canvas that covers entire client area and a separator view
        _drawSeparator = new ViewDrawSeparator(StateDisabled.Separator!, StateNormal.Separator!, StateTracking, StatePressed,
            StateDisabled.Separator!, StateNormal.Separator!, StateTracking, StatePressed,
            PaletteMetricPadding.SeparatorPaddingLowProfile, Orientation.Vertical);

        // Get the separator to fill the entire client area
        _drawDocker = new ViewDrawDocker(StateNormal.Back, StateNormal.Border)
        {
            IgnoreAllBorderAndPadding = true
        };
        _drawDocker.Add(_drawSeparator, ViewDockStyle.Fill);

        // Create a separator controller to handle separator style behaviour
        _separatorController = new SeparatorController(this, _drawSeparator, true, true, NeedPaintDelegate);

        // Assign the controller to the view element to treat as a separator
        _drawSeparator.MouseController = _separatorController;
        _drawSeparator.KeyController = _separatorController;
        _drawSeparator.SourceController = _separatorController;

        // Create the view manager instance
        ViewManager = new ViewManager(this, _drawDocker);

        // Use timer to redraw after windows messages are processed
        _redrawTimer = new System.Windows.Forms.Timer
        {
            Interval = 1
        };
        _redrawTimer.Tick += OnRedrawTick;

        // Set other internal starting values
        _style = SeparatorStyle.HighProfile;
        _orientation = Orientation.Vertical;
        AllowMove = true;
        SplitterIncrements = 1;
        _splitterWidth = 5;
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_redrawTimer != null)
            {
                _redrawTimer.Stop();
                _redrawTimer.Dispose();
                _redrawTimer = null;
            }

            // Must remember to dispose of the separator, as it can create a 
            // message filter that would prevent it from being garbage collected
            _separatorController.Dispose();
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets or sets the text associated with this control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [AllowNull]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public override string Text
    {
        get => base.Text;
        set => base.Text = value;
    }

    /// <summary>
    /// Gets or sets the background color for the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public override Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
    }

    /// <summary>
    /// Gets or sets the font of the text Displayed by the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [AmbientValue(null)]
    [AllowNull]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public override Font Font
    {
        get => base.Font;
        set => base.Font = value!;
    }

    /// <summary>
    /// Gets or sets the foreground color for the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public override Color ForeColor
    {
        get => base.ForeColor;
        set => base.ForeColor = value;
    }

    /// <summary>
    /// Gets and sets the separator background style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Separator background style.")]
    public PaletteBackStyle ContainerBackStyle
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

    private bool ShouldSerializeContainerBackStyle() => ContainerBackStyle != PaletteBackStyle.PanelClient;

    private void ResetContainerBackStyle() => ContainerBackStyle = PaletteBackStyle.PanelClient;

    /// <summary>
    /// Gets and sets the separator style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Separator style.")]
    public SeparatorStyle SeparatorStyle
    {
        get => _style;

        set
        {
            if (_style != value)
            {
                _style = value;
                StateCommon?.Separator.SetStyles(_style);
                _drawSeparator.MetricPadding = CommonHelper.SeparatorStyleToMetricPadding(_style);
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeSeparatorStyle() => SeparatorStyle != SeparatorStyle.HighProfile;

    private void ResetSeparatorStyle() => SeparatorStyle = SeparatorStyle.HighProfile;

    /// <summary>
    /// Gets access to the common separator appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common separator appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteSplitContainerRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled separator appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled separator appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteSplitContainer StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal separator appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal separator appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteSplitContainer StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the hot tracking separator appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining hot tracking separator appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteSeparatorPadding? StateTracking { get; }

    private bool ShouldSerializeStateTracking() => !StateTracking!.IsDefault;

    /// <summary>
    /// Gets access to the pressed separator appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed separator appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteSeparatorPadding? StatePressed { get; }

    private bool ShouldSerializeStatePressed() => !StatePressed!.IsDefault;

    /// <summary>
    /// Gets and sets the thickness of the splitter.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Determines the thickness of the splitter.")]
    [Localizable(true)]
    [DefaultValue(5)]
    public int SplitterWidth
    {
        get => _splitterWidth;

        set
        {
            // Only interested in changes of value
            if (_splitterWidth != value)
            {
                // Cannot assign a value of less than zero
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(SplitterWidth), @"Value cannot be less than zero");
                }

                // Use new width of the splitter area
                _splitterWidth = value;

                UpdateSize();
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets and sets the increment used for moving.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Determines the increment used for moving.")]
    [DefaultValue(1)]
    public int SplitterIncrements { get; set; }

    /// <summary>
    /// Gets or sets a value indicating the horizontal or vertical orientation of the separator.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Determines if the separator is vertical or horizontal.")]
    [Localizable(true)]
    [DefaultValue(Orientation.Vertical)]
    public Orientation Orientation
    {
        get => _orientation;

        set
        {
            // Only interested in changes of value
            if (_orientation != value)
            {
                // Use the new orientation
                _orientation = value;

                // Must update the visual drawing with new orientation as well
                _drawSeparator.Orientation = _orientation;

                UpdateSize();
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating if the separator is allowed to notify a move.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Determines if the separator is allowed to notify a move.")]
    [DefaultValue(true)]
    public bool AllowMove { get; set; }

    /// <summary>
    /// Gets and sets the drawing of the movement indicator.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Determines if the move indicator is drawn when moving the separator.")]
    [DefaultValue(true)]
    public bool DrawMoveIndicator
    {
        get => _separatorController.DrawMoveIndicator;
        set => _separatorController.DrawMoveIndicator = value;
    }
    #endregion

    #region Public ISeparatorSource
    /// <summary>
    /// Gets the top level control of the source.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Control SeparatorControl => this;

    /// <summary>
    /// Gets the orientation of the separator.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Orientation SeparatorOrientation => Orientation;

    /// <summary>
    /// Can the separator be moved by the user.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool SeparatorCanMove => AllowMove;

    /// <summary>
    /// Gets the amount the splitter can be incremented.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SeparatorIncrements => SplitterIncrements;

    /// <summary>
    /// Gets the box representing the minimum and maximum allowed splitter movement.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Rectangle SeparatorMoveBox
    {
        get
        {
            // Fire event to recover the rectangle of allowed separator movement
            var args = new SplitterMoveRectMenuArgs(Rectangle.Empty);
            OnSplitterMoveRect(args);

            return Orientation == Orientation.Horizontal
                ? args.MoveRect with { X = 0, Width = 0 }
                : args.MoveRect with { Y = 0, Height = 0 };
        }
    }

    /// <summary>
    /// Indicates the separator is moving.
    /// </summary>
    /// <param name="mouse">Current mouse position in client area.</param>
    /// <param name="splitter">Current position of the splitter.</param>
    /// <returns>True if movement should be cancelled; otherwise false.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool SeparatorMoving(Point mouse, Point splitter)
    {
        // Fire the event that indicates the splitter is being moved
        var e = new SplitterCancelEventArgs(mouse.X, mouse.Y, splitter.X, splitter.Y);
        OnSplitterMoving(e);

        // Tell caller if the movement should be cancelled or not
        return e.Cancel;
    }

    /// <summary>
    /// Indicates the separator has finished and been moved.
    /// </summary>
    /// <param name="mouse">Current mouse position in client area.</param>
    /// <param name="splitter">Current position of the splitter.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SeparatorMoved(Point mouse, Point splitter)
    {
        // Fire the event that indicates the splitter has finished being moved
        var e = new SplitterEventArgs(mouse.X, mouse.Y, splitter.X, splitter.Y);
        OnSplitterMoved(e);

        _redrawTimer?.Start();
    }

    /// <summary>
    /// Indicates the separator has not been moved.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SeparatorNotMoved()
    {
        // Fire the event that indicates the splitter has finished but not been moved
        OnSplitterNotMoved(EventArgs.Empty);

        _redrawTimer?.Start();
    }
    #endregion

    #region Public Overrides
    /// <summary>
    /// Gets or sets padding within the control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Padding Padding
    {
        get => base.Padding;
        set => base.Padding = value;
    }
    #endregion

    #region Protected
    /// <summary>
    /// Raises the SplitterMoveRect event.
    /// </summary>
    /// <param name="e">A SplitterMoveRectMenuArgs containing the event data.</param>
    protected virtual void OnSplitterMoveRect(SplitterMoveRectMenuArgs e) => SplitterMoveRect?.Invoke(this, e);

    /// <summary>
    /// Raises the SplitterMoved event.
    /// </summary>
    /// <param name="e">A SplitterEventArgs containing the event data.</param>
    protected virtual void OnSplitterMoved(SplitterEventArgs e) => SplitterMoved?.Invoke(this, e);

    /// <summary>
    /// Raises the SplitterNotMoved event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnSplitterNotMoved(EventArgs e) => SplitterNotMoved?.Invoke(this, e);

    /// <summary>
    /// Raises the SplitterMoving event.
    /// </summary>
    /// <param name="e">A SplitterEventArgs containing the event data.</param>
    protected virtual void OnSplitterMoving(SplitterCancelEventArgs e) => SplitterMoving?.Invoke(this, e);

    #endregion

    #region Protected Overrides
    /// <summary>
    /// Gets the default size of the control.
    /// </summary>
    protected override Size DefaultSize => new Size(5, 5);

    /// <summary>
    /// Raises the Initialized event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
        PerformNeedPaint(true);
    }

    /// <summary>
    /// Raises the DockChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnDockChanged(EventArgs e)
    {
        UpdateSize();
        base.OnDockChanged(e);
    }

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        // Push correct palettes into the view
        if (Enabled)
        {
            _drawDocker.SetPalettes(StateNormal.Back, StateNormal.Border);
        }
        else
        {
            _drawDocker.SetPalettes(StateDisabled.Back, StateDisabled.Border);
        }

        _drawDocker.Enabled = Enabled;
        _drawSeparator.Enabled = Enabled;

        // Change in enabled state requires a layout and repaint
        PerformNeedPaint(true);

        // Let base class fire standard event
        base.OnEnabledChanged(e);
    }
    #endregion

    #region Protected Overrides (Events)
    /// <summary>
    /// Raises the AutoSizeChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnAutoSizeChanged(EventArgs e) => AutoSizeChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the BackgroundImageChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnBackgroundImageChanged(EventArgs e) => BackgroundImageChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the BackgroundImageLayoutChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnBackgroundImageLayoutChanged(EventArgs e) => BackgroundImageLayoutChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the ControlAdded event.
    /// </summary>
    /// <param name="e">An ControlEventArgs containing the event data.</param>
    protected override void OnControlAdded(ControlEventArgs e) => ControlAdded?.Invoke(this, e);

    /// <summary>
    /// Raises the ControlRemoved event.
    /// </summary>
    /// <param name="e">An ControlEventArgs containing the event data.</param>
    protected override void OnControlRemoved(ControlEventArgs e) => ControlRemoved?.Invoke(this, e);

    #endregion

    #region Internal (Design Time Support)
    internal Cursor? DesignGetHitTest(Point pt)
    {
        // Is the cursor inside the splitter area or if currently moving the splitter
        if (_drawSeparator.ClientRectangle.Contains(pt) || _separatorController.IsMoving)
        {
            // Cursor depends on orientation direction
            return Orientation == Orientation.Vertical ? Cursors.VSplit : Cursors.HSplit;
        }

        return null;
    }

    internal void DesignMouseEnter() =>
        // Pass message directly onto the separator controller
        _separatorController.MouseEnter(this);

    internal bool DesignMouseDown(Point pt, MouseButtons button)
    {
        // Remember last point encountered
        _designLastPt = pt;

        // Pass message directly onto the separator controller
        return _separatorController.MouseDown(this, pt, button);
    }

    internal void DesignMouseMove(Point pt)
    {
        // Remember last point encountered
        _designLastPt = pt;

        // Pass message directly onto the separator controller
        _separatorController.MouseMove(this, pt);
    }

    internal void DesignMouseUp(MouseButtons button) =>
        // Pass message directly onto the separator controller
        _separatorController.MouseUp(this, _designLastPt, button);

    internal void DesignMouseLeave() =>
        // Pass message directly onto the separator controller
        _separatorController.MouseLeave(this, null);

    internal void DesignAbortMoving() =>
        // Pass message directly onto the separator controller
        _separatorController.AbortMoving();

    #endregion

    #region Private
    private void UpdateSize()
    {
        if ((Dock != DockStyle.None) && (Dock != DockStyle.Fill))
        {
            Size = Orientation == Orientation.Vertical
                ? new Size(Width, _splitterWidth)
                : new Size(_splitterWidth, Height);
        }
    }

    private void OnRedrawTick(object? sender, EventArgs e)
    {
        _redrawTimer?.Stop();

        Refresh();
    }
    #endregion
}
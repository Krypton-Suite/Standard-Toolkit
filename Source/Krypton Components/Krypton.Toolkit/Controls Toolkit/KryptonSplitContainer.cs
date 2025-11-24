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
/// Control the sizing of two panels.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonSplitContainer), "ToolboxBitmaps.KryptonSplitContainer.bmp")]
[DefaultEvent(nameof(SplitterMoved))]
[DefaultProperty(nameof(Orientation))]
[DesignerCategory(@"code")]
[Designer(typeof(KryptonSplitContainerDesigner))]
[Description(@"Divide the container inside two resizable panels.")]
[Docking(DockingBehavior.AutoDock)]
public class KryptonSplitContainer : VisualControlContainment,
    ISeparatorSource
{
    #region Instance Fields
    private SeparatorStyle _style;
    private readonly ViewDrawPanel _drawPanel;
    private readonly ViewDrawSeparator _drawSeparator;
    private readonly SeparatorController _separatorController;
    private Orientation _orientation;
    private FixedPanel _fixedPanel;
    private Point _designLastPt;
    private double _splitterPercent;
    private int _splitterDistance;
    private int _splitterIncrement;
    private int _splitterWidth;
    private int _panel1MinSize;
    private int _panel2MinSize;
    private int _fixedDistance;
    private bool _forcedLayout;
    private bool _resizing;

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
    /// Occurs when the splitter control is moved.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the splitter is done being moved.")]
    public event SplitterEventHandler? SplitterMoved;

    /// <summary>
    /// Occurs when the splitter control is in the process of moving.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the splitter is being moved.")]
    public event SplitterCancelEventHandler? SplitterMoving;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonSplitContainer class.
    /// </summary>
    public KryptonSplitContainer()
    {
        // Create the palette storage
        StateCommon = new PaletteSplitContainerRedirect(Redirector, PaletteBackStyle.PanelClient,
            PaletteBorderStyle.ControlClient, PaletteBackStyle.SeparatorLowProfile,
            PaletteBorderStyle.SeparatorLowProfile, NeedPaintDelegate);

        StateDisabled = new PaletteSplitContainer(StateCommon, StateCommon.Separator, StateCommon.Separator, NeedPaintDelegate);
        StateNormal = new PaletteSplitContainer(StateCommon, StateCommon.Separator, StateCommon.Separator, NeedPaintDelegate);
        StateTracking = new PaletteSeparatorPadding(StateCommon.Separator, StateCommon.Separator, NeedPaintDelegate);
        StatePressed = new PaletteSeparatorPadding(StateCommon.Separator, StateCommon.Separator, NeedPaintDelegate);

        // Our view contains just a simple canvas that covers entire client area and a separator view
        _drawSeparator = new ViewDrawSeparator(StateDisabled.Separator!, StateNormal.Separator!, StateTracking, StatePressed,
            StateDisabled.Separator!, StateNormal.Separator!, StateTracking, StatePressed,
            PaletteMetricPadding.SeparatorPaddingLowProfile, Orientation.Vertical);

        _drawPanel = new ViewDrawPanel(StateNormal.Back)
        {
            _drawSeparator
        };

        // Create a separator controller to handle separator style behaviour
        _separatorController = new SeparatorController(this, _drawSeparator, true, true, NeedPaintDelegate);

        // Assign the controller to the view element to treat as a separator
        _drawSeparator.MouseController = _separatorController;
        _drawSeparator.KeyController = _separatorController;
        _drawSeparator.SourceController = _separatorController;

        // Create the view manager instance
        ViewManager = new ViewManager(this, _drawPanel);

        // Set other internal starting values
        _splitterDistance = 50;
        _splitterPercent = 1f / 3f;
        _splitterIncrement = 1;
        _panel1MinSize = 25;
        _panel2MinSize = 25;
        _splitterWidth = 5;
        _fixedDistance = 50;
        _fixedPanel = FixedPanel.None;
        _orientation = Orientation.Vertical;

        // Create the two fixed child panels
        Panel1 = new KryptonSplitterPanel(this);
        Panel2 = new KryptonSplitterPanel(this);

        // Add both panels to the controls collection
        ((KryptonReadOnlyControls)Controls).AddInternal(Panel1);
        ((KryptonReadOnlyControls)Controls).AddInternal(Panel2);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Must remember to dispose of the separator, as it can create a 
            // message filter that would prevent it from being garbage collected
            _separatorController.Dispose();
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the name of the control.
    /// </summary>
    [Browsable(false)]
    [AllowNull]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new string Name
    {
        get => base.Name;

        set
        {
            base.Name = value;
            Panel1.Name = $"{value}.Panel1";
            Panel2.Name = $"{value}.Panel2";
        }
    }

    /// <summary>
    /// Gets and sets the container background style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Container background style.")]
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
                SetStyles(_style);
                _drawSeparator.MetricPadding = CommonHelper.SeparatorStyleToMetricPadding(_style);
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeSeparatorStyle() => SeparatorStyle != SeparatorStyle.LowProfile;

    private void ResetSeparatorStyle() => SeparatorStyle = SeparatorStyle.LowProfile;

    /// <summary>
    /// Gets access to the common split container appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common split container appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteSplitContainerRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled split container appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled split container appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteSplitContainer StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal split container appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal split container appearance.")]
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
    /// Gets access to the first krypton splitter panel.
    /// </summary>
    [Localizable(false)]
    [Category(@"Appearance")]
    [Description(@"The Left or Top panel in the KryptonSplitContainer.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonSplitterPanel Panel1 { get; }

    /// <summary>
    /// Gets and sets the minium size of panel1.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Determines the minimum distance of pixels of the splitter from the left or top edge of Panel1.")]
    [Localizable(true)]
    [DefaultValue(25)]
    public int Panel1MinSize
    {
        get => _panel1MinSize;

        set
        {
            // Only interested in changes of value
            if (_panel1MinSize != value)
            {
                // Cannot assign a value of less than zero
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Panel1MinSize), @"Value cannot be less than zero");
                }

                // Use the new minimum size
                _panel1MinSize = value;

                if (IsInitialized)
                {
                    // Ask the layout to be updated to reflect new distance
                    PerformLayout();
                    Invalidate();
                }
                else
                {
                    // We must have a layout calculation
                    ForceControlLayout();
                }
            }
        }
    }

    /// <summary>
    /// Gets and sets collapsed state of Panel1.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Determines if Panel1 is collapsed.")]
    [DefaultValue(false)]
    public bool Panel1Collapsed
    {
        get => Panel1.Collapsed;

        set
        {
            // Only interested in changes of value
            if (Panel1.Collapsed != value)
            {
                // If making Panel1 collapsed then make sure Panel2 is not
                if (value && Panel2.Collapsed)
                {
                    Panel2.Collapsed = false;
                    Panel2.Visible = true;
                }

                // Update collapsed state of the panel
                Panel1.Collapsed = value;
                Panel1.Visible = !value;

                // If leaving the collapsed state
                if (!Panel1Collapsed && !Panel2Collapsed)
                {
                    // Convert distance using the percentage in opposite direction
                    if (_orientation == Orientation.Vertical)
                    {
                        _splitterDistance = (int)(Width * _splitterPercent);
                    }
                    else
                    {
                        _splitterDistance = (int)(Height * _splitterPercent);
                    }
                }

                if (IsInitialized)
                {
                    // Ask the layout to be updated to reflect new visibility
                    PerformLayout();
                    Invalidate();
                }
                else
                {
                    // We must have a layout calculation
                    ForceControlLayout();
                }
            }
        }
    }

    /// <summary>
    /// Gets access to the second krypton splitter panel.
    /// </summary>
    [Localizable(false)]
    [Category(@"Appearance")]
    [Description(@"The Right or Bottom panel in the KryptonSplitContainer.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonSplitterPanel Panel2 { get; }

    /// <summary>
    /// Gets and sets the minium size of panel2.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Determines the minimum distance of pixels of the splitter from the right or bottom edge of Panel2.")]
    [Localizable(true)]
    [DefaultValue(25)]
    public int Panel2MinSize
    {
        get => _panel2MinSize;

        set
        {
            // Only interested in changes of value
            if (_panel2MinSize != value)
            {
                // Cannot assign a value of less than zero
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Panel2MinSize), @"Value cannot be less than zero");
                }

                // Use the new minimum size
                _panel2MinSize = value;

                if (IsInitialized)
                {
                    // Ask the layout to be updated to reflect new distance
                    PerformLayout();
                    Invalidate();
                }
                else
                {
                    // We must have a layout calculation
                    ForceControlLayout();
                }
            }
        }
    }

    /// <summary>
    /// Gets and sets collapsed state of Panel2.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Determines if Panel2 is collapsed.")]
    [DefaultValue(false)]
    public bool Panel2Collapsed
    {
        get => Panel2.Collapsed;

        set
        {
            // Only interested in changes of value
            if (Panel2.Collapsed != value)
            {
                // If making Panel1 collapsed then make sure Panel1 is not
                if (value && Panel1.Collapsed)
                {
                    Panel1.Collapsed = false;
                    Panel1.Visible = true;
                }

                // Update collapsed state of the panel
                Panel2.Collapsed = value;
                Panel2.Visible = !value;

                // If leaving the collapsed state
                if (!Panel1Collapsed && !Panel2Collapsed)
                {
                    // Convert distance using the percentage in opposite direction
                    if (_orientation == Orientation.Vertical)
                    {
                        _splitterDistance = (int)(Width * _splitterPercent);
                    }
                    else
                    {
                        _splitterDistance = (int)(Height * _splitterPercent);
                    }
                }

                if (IsInitialized)
                {
                    // Ask the layout to be updated to reflect new visibility
                    PerformLayout();
                    Invalidate();
                }
                else
                {
                    // We must have a layout calculation
                    ForceControlLayout();
                }
            }
        }
    }

    /// <summary>
    /// Gets and sets a value indicating if the splitter can be moved.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Determines if the splitter is fixed.")]
    [Localizable(true)]
    [DefaultValue(false)]
    public bool IsSplitterFixed { get; set; }

    /// <summary>
    /// Gets and sets the panel to keep the same size when resizing.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Indicates the panel to keep the same size when resizing.")]
    [DefaultValue(FixedPanel.None)]
    [Localizable(true)]
    public FixedPanel FixedPanel
    {
        get => _fixedPanel;

        set
        {
            // Only interested in changes of value
            if (_fixedPanel != value)
            {
                // Use new value
                _fixedPanel = value;

                // Orientation determines the width/height to use
                if (Orientation == Orientation.Vertical)
                {
                    switch (_fixedPanel)
                    {
                        case FixedPanel.Panel1:
                            _fixedDistance = Panel1.Width;
                            break;
                        case FixedPanel.Panel2:
                            _fixedDistance = Panel2.Width;
                            break;
                    }
                }
                else
                {
                    switch (_fixedPanel)
                    {
                        case FixedPanel.Panel1:
                            _fixedDistance = Panel1.Height;
                            break;
                        case FixedPanel.Panel2:
                            _fixedDistance = Panel2.Height;
                            break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Gets and sets the distance of the splitter.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Determines pixel distance of the splitter from the left or top edge.")]
    [Localizable(true)]
    [SettingsBindable(true)]
    [DefaultValue(50)]
    public int SplitterDistance
    {
        get => _splitterDistance;

        set
        {
            // Only interested in changes of value
            if (_splitterDistance != value)
            {
                // Cannot assign a value of less than zero
                if (value < 0)
                {
                    value = 0;
                }

                // Enforce the minimum size of the first panel
                if (value < Panel1MinSize)
                {
                    value = Panel1MinSize;
                }

                // Limit check against the orientation direction
                if (Orientation == Orientation.Vertical)
                {
                    // Enforce the minimum size of the second second
                    if ((value + SplitterWidth) > (Width - Panel2MinSize))
                    {
                        value = Width - Panel2MinSize - SplitterWidth;
                    }

                    // Cannot assign a value of less than zero
                    if (value < 0)
                    {
                        value = 0;
                    }
                }
                else
                {
                    // Enforce the minimum size of the second second
                    if ((value + SplitterWidth) > (Height - Panel2MinSize))
                    {
                        value = Height - Panel2MinSize - SplitterWidth;
                    }

                    // Cannot assign a value of less than zero
                    if (value < 0)
                    {
                        value = 0;
                    }
                }

                // Use new pixel distance
                _splitterDistance = value;

                // Measure fixed distance from rigth edge if using the second panel as the fixed one
                if (FixedPanel == FixedPanel.Panel2)
                {
                    if (Orientation == Orientation.Vertical)
                    {
                        _fixedDistance = Width - value - SplitterWidth;
                    }
                    else
                    {
                        _fixedDistance = Height - value - SplitterWidth;
                    }
                }
                else
                {
                    _fixedDistance = value;
                }

                if (IsInitialized)
                {
                    // Ask the layout to be updated to reflect new distance
                    PerformLayout();
                    Invalidate();
                }
                else
                {
                    // We must have a layout calculation
                    ForceControlLayout();
                }
            }
        }
    }

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

                if (IsInitialized)
                {
                    // Ask the layout to be updated to reflect new length
                    PerformLayout();
                    Invalidate();
                }
                else
                {
                    // We must have a layout calculation
                    ForceControlLayout();
                }
            }
        }
    }

    /// <summary>
    /// Gets and sets the number of pixels the splitter moves in increments.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Determines the number of pixels the splitter moves in increments.")]
    [Localizable(true)]
    [DefaultValue(1)]
    public int SplitterIncrement
    {
        get => _splitterIncrement;

        set
        {
            // Only interested in changes of value
            if (_splitterIncrement != value)
            {
                // Cannot assign a value of less than zero
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(SplitterIncrement), @"Value cannot be less than one");
                }

                // Remember new value for use when moving the splitter
                _splitterIncrement = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating the horizontal or vertical orientation of the KryptonSplitContainer panels.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Determines if the splitter is vertical or horizontal.")]
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
                if (!Collapsed)
                {
                    // Convert distance using the percentage in opposite direction
                    if (_orientation == Orientation.Vertical)
                    {
                        _splitterDistance = (int)(Width * _splitterPercent);
                    }
                    else
                    {
                        _splitterDistance = (int)(Height * _splitterPercent);
                    }
                }

                // Use the new orientation
                _orientation = value;

                // Must update the visual drawing with new orientation as well
                _drawSeparator.Orientation = _orientation;

                if (IsInitialized)
                {
                    // Ask the layout to be updated to reflect new percentage
                    PerformLayout();
                    Invalidate();
                }
                else
                {
                    // We must have a layout calculation
                    ForceControlLayout();
                }
            }
        }
    }

    /// <summary>
    /// Fix the control to a particular palette state.
    /// </summary>
    /// <param name="stateSplit">Palette state to fix against the split container.</param>
    /// <param name="stateSeparator">Palette state to fix against the separator.</param>
    public virtual void SetFixedState(PaletteState stateSplit,
        PaletteState stateSeparator)
    {
        // Request fixed state from the view
        _drawPanel.FixedState = stateSplit;
        _drawSeparator.FixedState = stateSeparator;
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
    public bool SeparatorCanMove => !IsSplitterFixed && !Collapsed;

    /// <summary>
    /// Gets the amount the splitter can be incremented.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SeparatorIncrements => SplitterIncrement;

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
            // Start with the total client area
            Rectangle rect = ClientRectangle;

            // Calculation depends on the use of right to left layout setting
            int before;
            int after;
            if (CommonHelper.GetRightToLeftLayout(this) && (RightToLeft == RightToLeft.Yes))
            {
                before = Panel2MinSize;
                after = Panel1MinSize;
            }
            else
            {
                before = Panel1MinSize;
                after = Panel2MinSize;
            }

            if (Orientation == Orientation.Vertical)
            {
                var start = Math.Min(before, rect.Width);
                var end = Math.Max(rect.Right - after, 0);
                end = Math.Max(start, end);
                rect.X = start;
                rect.Width = end - start;
            }
            else
            {
                var start = Math.Min(before, rect.Height);
                var end = Math.Max(rect.Bottom - after, 0);
                end = Math.Max(start, end);
                rect.Y = start;
                rect.Height = end - start;
            }

            return rect;
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
        // Ignore the delta applied to the splitter position
        if (Orientation == Orientation.Vertical)
        {
            splitter.Y = 0;
        }
        else
        {
            splitter.X = 0;
        }

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
        // Calculation depends on use of the right to left layout setting
        if (CommonHelper.GetRightToLeftLayout(this) && (RightToLeft == RightToLeft.Yes))
        {
            SplitterDistance = Orientation == Orientation.Vertical ? Width - splitter.X : Height - splitter.Y;
        }
        else
        {
            SplitterDistance = Orientation == Orientation.Vertical ? splitter.X : splitter.Y;
        }

        // Fire the event that indicates the splitter has finished being moved
        var e = new SplitterEventArgs(mouse.X, mouse.Y, splitter.X, splitter.Y);
        OnSplitterMoved(e);
    }

    /// <summary>
    /// Indicates the separator has not been moved.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SeparatorNotMoved()
    {
        // Do nothing, we do not care
    }
    #endregion

    #region Public Overrides
    /// <summary>
    /// Gets the collection of controls contained within the control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ControlCollection Controls => base.Controls;

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

    #region Public (Design Time Support)
    /// <summary>
    /// Internal design time usage only.
    /// </summary>
    /// <param name="pt">Mouse location.</param>
    /// <returns>Cursor to show.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Cursor? DesignGetHitTest(Point pt)
    {
        // Is the cursor inside the splitter area or if currently moving the splitter
        if (_drawSeparator.ClientRectangle.Contains(pt) || _separatorController.IsMoving)
        {
            // Is the splitter allowed to be moved?
            if (!Collapsed)
            {
                // Cursor depends on orientation direction
                return Orientation == Orientation.Vertical ? Cursors.VSplit : Cursors.HSplit;
            }
        }

        return null;
    }

    /// <summary>
    /// Internal design time method.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void DesignMouseEnter() =>
        // Pass message directly onto the separator controller
        _separatorController.MouseEnter(this);

    /// <summary>
    /// Internal design time method.
    /// </summary>
    /// <param name="pt">Mouse location.</param>
    /// <param name="button">Mouse button.</param>
    /// <returns>Process mouse down.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool DesignMouseDown(Point pt, MouseButtons button)
    {
        // Remember last point encountered
        _designLastPt = pt;

        // Pass message directly onto the separator controller
        return _separatorController.MouseDown(this, pt, button);
    }

    /// <summary>
    /// Internal design time method.
    /// </summary>
    /// <param name="pt">Mouse location.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void DesignMouseMove(Point pt)
    {
        // Remember last point encountered
        _designLastPt = pt;

        // Pass message directly onto the separator controller
        _separatorController.MouseMove(this, pt);
    }

    /// <summary>
    /// Internal design time method.
    /// </summary>
    /// <param name="button">Mouse button.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void DesignMouseUp(MouseButtons button) =>
        // Pass message directly onto the separator controller
        _separatorController.MouseUp(this, _designLastPt, button);

    /// <summary>
    /// Internal design time method.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void DesignMouseLeave() =>
        // Pass message directly onto the separator controller
        _separatorController.MouseLeave(this, null);

    /// <summary>
    /// Internal design time method.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void DesignAbortMoving() =>
        // Pass message directly onto the separator controller
        _separatorController.AbortMoving();

    #endregion

    #region Protected
    /// <summary>
    /// Raises the SplitterMoved event.
    /// </summary>
    /// <param name="e">A SplitterEventArgs containing the event data.</param>
    protected virtual void OnSplitterMoved(SplitterEventArgs e) => SplitterMoved?.Invoke(this, e);

    /// <summary>
    /// Raises the SplitterMoving event.
    /// </summary>
    /// <param name="e">A SplitterEventArgs containing the event data.</param>
    protected virtual void OnSplitterMoving(SplitterCancelEventArgs e) => SplitterMoving?.Invoke(this, e);

    /// <summary>
    /// Force the layout logic to size and position the panels.
    /// </summary>
    protected void ForceControlLayout()
    {
        // Usually the layout will not occur if currently initializing but
        // we need to force the layout processing because overwise the size
        // of the panel controls will not have been calculated when controls
        // are added to the panels. That would then cause problems with
        // anchor controls as they would then resize incorrectly.
        if (!IsInitialized)
        {
            _forcedLayout = true;
            OnLayout(new LayoutEventArgs(null, null));
            _forcedLayout = true;
        }
    }
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Gets the default size of the control.
    /// </summary>
    protected override Size DefaultSize => new Size(150, 150);

    /// <summary>
    /// Raises the Initialized event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnInitialized(EventArgs e)
    {
        // Let base class raise events
        base.OnInitialized(e);

        // Force a layout now that initialization is complete
        OnLayout(new LayoutEventArgs(null, null));
    }

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        // Push correct palettes into the view
        _drawPanel.SetPalettes(Enabled ? StateNormal.Back : StateDisabled.Back);

        _drawPanel.Enabled = Enabled;
        _drawSeparator.Enabled = Enabled;

        // Change in enabled state requires a layout and repaint
        PerformNeedPaint(true);

        // Let base class fire standard event
        base.OnEnabledChanged(e);
    }

    /// <summary>
    /// Raises the Resize event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnResize(EventArgs e)
    {
        // Do not alter the distance whilst size is changing because of initialization
        if (!IsInitializing)
        {
            // Caluclate new percentage based on the new size
            if (Orientation == Orientation.Vertical)
            {
                _splitterDistance = (int)(Width * _splitterPercent);
            }
            else
            {
                _splitterDistance = (int)(Height * _splitterPercent);
            }
        }

        // Let base class raise events
        _resizing = true;
        base.OnResize(e);
        _resizing = false;

        // We must have a layout calculation
        ForceControlLayout();
    }

    /// <summary>
    /// Raises the Paint event.
    /// </summary>
    /// <param name="e">A PaintEventArgs that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs? e)
    {
        // Do we have a manager to use for painting?
        if (ViewManager != null)
        {
            // If the control has not been initialized as yet
            if (!IsInitialized)
            {
                // In design mode it does not call BeginInit/EndInit
                BeginInit();
                EndInit();

                // Must perform a layout of panels for painting
                PerformLayout();
            }
        }

        base.OnPaint(e);
    }

    /// <summary>
    /// Raises the Layout event.
    /// </summary>
    /// <param name="levent">A LayoutEventArgs that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
        var separatorRect = Rectangle.Empty;

        // Only use layout logic if control is fully initialized or if being forced
        // to allow a relayout or if in design mode.
        if (IsInitialized || _forcedLayout || (DesignMode && (_drawSeparator != null)))
        {
            // Do we need to perform right to left layout of the control?
            var rtl = CommonHelper.GetRightToLeftLayout(this) && (RightToLeft == RightToLeft.Yes);

            // If we are zero sized then reflect that in the child panels
            if (Width == 0)
            {
                Panel1.Size = new Size(0, Height);
                Panel2.Size = new Size(0, Height);
            }
            else if (Height == 0)
            {
                Panel1.Size = new Size(Width, 0);
                Panel2.Size = new Size(Width, 0);
            }
            else
            {
                // If neither panel is collapsed
                if (!Collapsed)
                {
                    // Positioning of the panels depends on the orientation
                    if (Orientation == Orientation.Vertical)
                    {
                        // Do we used a fixed size for the first panel?
                        switch (FixedPanel)
                        {
                            case FixedPanel.Panel1:
                                // Set the fixed size of first panel, and fill remaining space with 
                                // second but applying the second panel minimum size specification
                                Panel1.Size = new Size(_fixedDistance, Height);
                                Panel2.Size = new Size(Math.Max(Width - SplitterWidth - _fixedDistance, Panel2MinSize), Height);

                                // Positioning depends on right-to-left layout setting
                                if (rtl)
                                {
                                    Panel1.Location = new Point(Panel2.Width + SplitterWidth, 0);
                                    Panel2.Location = Point.Empty;
                                }
                                else
                                {
                                    Panel1.Location = Point.Empty;
                                    Panel2.Location = new Point(Panel1.Width + SplitterWidth, 0);
                                }

                                // Update the splitter distance and percentage to reflect new positions
                                _splitterDistance = Panel1.Width;
                                _splitterPercent = Panel1.Width / (double)Width;
                                break;
                            case FixedPanel.Panel2:
                                // Set the fixed size of second panel, and fill remaining space with 
                                // first but applying the first panel minimum size specification
                                Panel2.Size = new Size(_fixedDistance, Height);
                                Panel1.Size = new Size(Math.Max(Width - SplitterWidth - _fixedDistance, Panel1MinSize), Height);

                                // Positioning depends on right-to-left layout setting
                                if (rtl)
                                {
                                    Panel1.Location = new Point(Panel2.Width + SplitterWidth, 0);
                                    Panel2.Location = Point.Empty;
                                }
                                else
                                {
                                    Panel1.Location = Point.Empty;
                                    Panel2.Location = new Point(Panel1.Width + SplitterWidth, 0);
                                }

                                // Update the splitter distance and percentage to reflect new positions
                                _splitterDistance = Panel1.Width;
                                _splitterPercent = Panel1.Width / (double)Width;
                                break;
                            default:
                                // Find the maximum allowed panel width
                                var panelMax = Width - SplitterWidth;

                                // Find actual pixel width of first panel but limited to maximum allowed
                                var panel1Width = Math.Min(SplitterDistance, panelMax);

                                // Enforce the minimum panel1 width
                                panel1Width = Math.Max(Panel1MinSize, panel1Width);

                                // Size the panels
                                Panel1.Size = new Size(panel1Width, Height);
                                Panel2.Size = new Size(Width - panel1Width - SplitterWidth, Height);

                                // Positioning depends on right-to-left layout setting
                                if (rtl)
                                {
                                    Panel1.Location = new Point(Width - panel1Width, 0);
                                    Panel2.Location = Point.Empty;
                                }
                                else
                                {
                                    Panel1.Location = Point.Empty;
                                    Panel2.Location = new Point(panel1Width + SplitterWidth, 0);
                                }

                                // Update the percentage but not if this occurs because of a resize operation
                                if (!_resizing)
                                {
                                    _splitterPercent = panel1Width / (double)Width;
                                }
                                break;
                        }

                        // Separator rect depends on right-to-left layout setting
                        separatorRect = rtl
                            ? new Rectangle(Panel2.Right, 0, SplitterWidth, Height)
                            : new Rectangle(Panel1.Right, 0, SplitterWidth, Height);
                    }
                    else
                    {
                        // Do we used a fixed size for the first panel?
                        switch (FixedPanel)
                        {
                            case FixedPanel.Panel1:
                                // Set the fixed size of first panel, and fill remaining space with 
                                // second but applying the second panel minimum size specification
                                Panel1.Size = new Size(Width, _fixedDistance);
                                Panel2.Size = new Size(Width, Math.Max(Height - SplitterWidth - _fixedDistance, Panel2MinSize));

                                Panel1.Location = Point.Empty;
                                Panel2.Location = new Point(0, Panel1.Height + SplitterWidth);

                                // Update the splitter distance and percentage to reflect new positions
                                _splitterDistance = Panel1.Height;
                                _splitterPercent = Panel1.Height / (double)Height;
                                break;
                            case FixedPanel.Panel2:
                                // Set the fixed size of second panel, and fill remaining space with 
                                // first but applying the first panel minimum size specification
                                Panel2.Size = new Size(Width, _fixedDistance);
                                Panel1.Size = new Size(Width, Math.Max(Height - SplitterWidth - _fixedDistance, Panel1MinSize));

                                Panel1.Location = Point.Empty;
                                Panel2.Location = new Point(0, Panel1.Height + SplitterWidth);

                                // Update the splitter distance and percentage to reflect new positions
                                _splitterDistance = Panel1.Height;
                                _splitterPercent = Panel1.Height / (double)Height;
                                break;
                            default:
                                // Find the maximum allowed panel1 height
                                var panel1Max = Height - SplitterWidth;

                                // Find actual pixel height of first panel but limited to maximum allowed
                                var panel1Height = Math.Min(SplitterDistance, panel1Max);

                                // Enforce the minimum panel1 height
                                panel1Height = Math.Max(Panel1MinSize, panel1Height);

                                // Size the panels
                                Panel1.Size = new Size(Width, panel1Height);
                                Panel2.Size = new Size(Width, Height - panel1Height - SplitterWidth);

                                Panel1.Location = Point.Empty;
                                Panel2.Location = new Point(0, panel1Height + SplitterWidth);

                                // Update the percentage but not if this occurs because of a resize operation
                                if (!_resizing)
                                {
                                    _splitterPercent = panel1Height / (double)Height;
                                }
                                break;
                        }

                        separatorRect = new Rectangle(0, Panel1.Bottom, Width, SplitterWidth);
                    }
                }
                else if (Panel1Collapsed)
                {
                    Panel2.Size = Size;
                    Panel2.Location = Point.Empty;
                }
                else if (Panel2Collapsed)
                {
                    Panel1.Size = Size;
                    Panel1.Location = Point.Empty;
                }
            }
        }

        // Let base class layout child controls
        base.OnLayout(levent);

        // Update the separator element manually
        if (_drawSeparator != null)
        {
            _drawSeparator.ClientRectangle = separatorRect;
        }
    }

    /// <summary>
    /// Creates a new instance of the control collection for the KryptonSplitContainer.
    /// </summary>
    /// <returns>A new instance of Control.ControlCollection assigned to the control.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected override ControlCollection CreateControlsInstance() => new KryptonReadOnlyControls(this);

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

    #region Implementation
    private bool Collapsed => Panel1.Collapsed || Panel2.Collapsed;

    private void SetStyles(SeparatorStyle separatorStyle) => StateCommon?.Separator.SetStyles(separatorStyle);

    #endregion
}
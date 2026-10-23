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

namespace Krypton.Navigator;

internal class ViewDrawNavRibbonTab : ViewComposite,
    IContentValues,
    INavCheckItem
{
    #region Static Fields
    private static readonly Padding _preferredBorder = new Padding(5, 5, 5, 2);
    private static readonly Padding _layoutBorderTop = new Padding(4, 4, 4, 1);
    private static readonly Padding _layoutBorderLeft = new Padding(5, 4, 3, 1);
    private static readonly Padding _layoutBorderRight = new Padding(4, 5, 4, 0);
    private static readonly Padding _layoutBorderBottom = new Padding(4, 2, 4, 3);
    private static readonly Padding _drawBorder = new Padding(1, 0, 1, 0);
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the drag rectangle for the button is required.
    /// </summary>
    public event EventHandler<ButtonDragRectangleEventArgs>? ButtonDragRectangle;

    /// <summary>
    /// Occurs when the drag button offset changes.
    /// </summary>
    public event EventHandler<ButtonDragOffsetEventArgs>? ButtonDragOffset;
    #endregion

    #region Instance Fields

    private readonly PageButtonController? _buttonController;
    private readonly IPaletteRibbonGeneral _paletteGeneral;
    private readonly PaletteRibbonTabContentInheritOverride _overrideStateNormal;
    private readonly PaletteRibbonTabContentInheritOverride _overrideStateTracking;
    private readonly PaletteRibbonTabContentInheritOverride _overrideStatePressed;
    private readonly PaletteRibbonTabContentInheritOverride _overrideStateSelected;
    private IPaletteRibbonText _currentText;
    private IPaletteRibbonBack _currentBack;
    private IPaletteContent _currentContent;
    private readonly RibbonTabToContent _contentProvider;
    private VisualOrientation _borderBackOrient;
    private NeedPaintHandler? _needPaint;
    private readonly ViewDrawContent _viewContent;
    private readonly ViewLayoutDocker _layoutDocker;
    private PaletteRibbonShape _lastRibbonShape;
    private IDisposable?[] _mementos;
    private DateTime _lastClick;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawNavRibbonTab class.
    /// </summary>
    /// <param name="navigator">Owning navigator instance.</param>
    /// <param name="page">Page this ribbon tab represents.</param>
    public ViewDrawNavRibbonTab([DisallowNull] KryptonNavigator navigator,
        [DisallowNull] KryptonPage page)
    {
        Debug.Assert(navigator is not null);
        Debug.Assert(page is not null);

        Navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));
        Page = page ?? throw new ArgumentNullException(nameof(page));
        _lastClick = DateTime.Now.AddDays(-1);

        // Associate the page component with this view element
        Component = page;

        // Create a controller for managing button behavior
        _buttonController = new PageButtonController(this, OnNeedPaint)
        {
            ClickOnDown = true
        };
        _buttonController.Click += OnClick;
        _buttonController.RightClick += OnRightClick;

        // Allow the page to be dragged and hook into drag events
        _buttonController.AllowDragging = true;
        _buttonController.DragStart += OnDragStart;
        _buttonController.DragMove += OnDragMove;
        _buttonController.DragEnd += OnDragEnd;
        _buttonController.DragQuit += OnDragQuit;
        _buttonController.ButtonDragRectangle += OnButtonDragRectangle;
        _buttonController.ButtonDragOffset += OnButtonDragOffset;

        // A tab is selected on being pressed and not on the mouse up
        _buttonController.ClickOnDown = true;

        // We need to be notified of got/lost focus and keyboard events
        SourceController = _buttonController;
        KeyController = _buttonController;

        // Create a decorator to interface with the tooltip manager
        var toolTipController = new ToolTipController(Navigator.ToolTipManager!, this, _buttonController);
        var hoverController = new ToolTipController(Navigator.HoverManager!, this, toolTipController);

        // Assign controller for handing mouse input
        MouseController = hoverController;

        // Create overrides for handling a focus state
        _paletteGeneral = Navigator.StateCommon!.RibbonGeneral;
        _overrideStateNormal = new PaletteRibbonTabContentInheritOverride(Page!.OverrideFocus.RibbonTab.TabDraw, Page!.OverrideFocus.RibbonTab.TabDraw, Page.OverrideFocus.RibbonTab.Content, Page.StateNormal.RibbonTab.TabDraw, Page.StateNormal.RibbonTab.TabDraw, Page.StateNormal.RibbonTab.Content, PaletteState.FocusOverride);
        _overrideStateTracking = new PaletteRibbonTabContentInheritOverride(Page.OverrideFocus.RibbonTab.TabDraw, Page.OverrideFocus.RibbonTab.TabDraw, Page.OverrideFocus.RibbonTab.Content, Page.StateTracking.RibbonTab.TabDraw, Page.StateTracking.RibbonTab.TabDraw, Page.StateTracking.RibbonTab.Content, PaletteState.FocusOverride);
        _overrideStatePressed = new PaletteRibbonTabContentInheritOverride(Page.OverrideFocus.RibbonTab.TabDraw, Page.OverrideFocus.RibbonTab.TabDraw, Page.OverrideFocus.RibbonTab.Content, Page.StatePressed.RibbonTab.TabDraw, Page.StatePressed.RibbonTab.TabDraw, Page.StatePressed.RibbonTab.Content, PaletteState.FocusOverride);
        _overrideStateSelected = new PaletteRibbonTabContentInheritOverride(Page.OverrideFocus.RibbonTab.TabDraw, Page.OverrideFocus.RibbonTab.TabDraw, Page.OverrideFocus.RibbonTab.Content, Page.StateSelected.RibbonTab.TabDraw, Page.StateSelected.RibbonTab.TabDraw, Page.StateSelected.RibbonTab.Content, PaletteState.FocusOverride);

        // Use a class to convert from ribbon tab to content interface
        _contentProvider = new RibbonTabToContent(_paletteGeneral, _overrideStateNormal, _overrideStateNormal);

        // Create the content view element and use the content provider as a way to
        // convert from the ribbon palette entries to the content palette entries
        _viewContent = new ViewDrawContent(_contentProvider, this, VisualOrientation.Top);

        // Add content to the view
        _layoutDocker = new ViewLayoutDocker
        {
            { _viewContent, ViewDockStyle.Fill }
        };
        Add(_layoutDocker);

        // Create button specification collection manager
        ButtonSpecManager = new ButtonSpecNavManagerLayoutBar(Navigator, Navigator.InternalRedirector, Page.ButtonSpecs, null,
            new[] { _layoutDocker },
            new IPaletteMetric[] { Navigator.StateCommon },
            new[] { PaletteMetricInt.PageButtonInset },
            new[] { PaletteMetricInt.PageButtonInset },
            new[] { PaletteMetricPadding.PageButtonPadding },
            Navigator.CreateToolStripRenderer,
            OnNeedPaint)
        {

            // Hook up the tooltip manager so that tooltips can be generated
            ToolTipManager = Navigator.ToolTipManager,
            RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.ButtonStandalone
        };

        // Ensure current button specs are created
        ButtonSpecManager.RecreateButtons();

        // Create the state specific memento array
        _mementos = new IDisposable[Enum.GetValues(typeof(PaletteState)).Length];

        // Cache the last shape encountered
        _lastRibbonShape = PaletteRibbonShape.Office2010;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawNavRibbonTab:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_mementos != null)
            {
                // Dispose of all the mementos in the array
                foreach (IDisposable? memento in _mementos)
                {
                    // https://github.com/Krypton-Suite/Standard-Toolkit/issues/1146
                    memento?.Dispose();
                }

                _mementos = null!;
            }

            if (ButtonSpecManager != null)
            {
                ButtonSpecManager.Destruct();
                ButtonSpecManager = null!;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the view associated with the ribbon tab.
    /// </summary>
    public ViewBase View => this;

    /// <summary>
    /// Gets the page this ribbon tab represents.
    /// </summary>
    public KryptonPage? Page { get; }

    /// <summary>
    /// Gets the navigator this check item is inside.
    /// </summary>
    public KryptonNavigator Navigator { get; }

    /// <summary>
    /// Gets and sets the checked state of the ribbon tab.
    /// </summary>
    public bool Checked { get; set; }

    /// <summary>
    /// Gets and sets if the ribbon tab has the focus.
    /// </summary>
    public bool HasFocus
    {
        get => _overrideStateNormal.Apply;

        set
        {
            if (_overrideStateNormal.Apply != value)
            {
                _overrideStateNormal.Apply = value;
                _overrideStateTracking.Apply = value;
                _overrideStatePressed.Apply = value;
                _overrideStateSelected.Apply = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the need paint delegate for notifying paint requests.
    /// </summary>
    public NeedPaintHandler? NeedPaint
    {
        get => _needPaint;

        set
        {
            // Warn if multiple sources want to hook their single delegate
            Debug.Assert(((_needPaint == null) && (value != null)) ||
                         ((_needPaint != null) && (value == null)));

            _needPaint = value;
        }
    }

    /// <summary>
    /// Gets the ButtonSpec associated with the provided item.
    /// </summary>
    /// <param name="element">Element to search against.</param>
    /// <returns>Reference to ButtonSpec; otherwise null.</returns>
    public ButtonSpec? ButtonSpecFromView(ViewBase element) => ButtonSpecManager.ButtonSpecFromView(element);

    /// <summary>
    /// Gets access to the button spec manager used for this button.
    /// </summary>
    public ButtonSpecNavManagerLayoutBar ButtonSpecManager { get; private set; }

    /// <summary>
    /// Raises the Click event for the button.
    /// </summary>
    public void PerformClick() => OnClick(this, EventArgs.Empty);

    /// <summary>
    /// Set the orientation of the background/border and content.
    /// </summary>
    /// <param name="borderBackOrient">Orientation of the button border and background..</param>
    /// <param name="contentOrient">Orientation of the button contents.</param>
    public void SetOrientation(VisualOrientation borderBackOrient,
        VisualOrientation contentOrient)
    {
        _borderBackOrient = borderBackOrient;
        _viewContent.Orientation = contentOrient;
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        // Work out the size needed for the child items
        Size childSize = base.GetPreferredSize(context);

        // Add the preferred border based on our orientation
        return CommonHelper.ApplyPadding(_borderBackOrient, childSize, _preferredBorder);
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout(ViewLayoutContext context)
    {
        // Ensure we are using the correct palette
        CheckPaletteState(context);

        // Cache the ribbon shape
        _lastRibbonShape = Navigator.GetResolvedPalette()?.GetRibbonShape() ?? PaletteRibbonShape.Office2007;

        // We take on all the provided size
        ClientRectangle = context.DisplayRectangle;

        var layoutPadding = Padding.Empty;

        switch (_borderBackOrient)
        {
            case VisualOrientation.Top:
                layoutPadding = _layoutBorderTop;
                break;
            case VisualOrientation.Left:
                layoutPadding = _layoutBorderLeft;
                break;
            case VisualOrientation.Right:
                layoutPadding = _layoutBorderRight;
                break;
            case VisualOrientation.Bottom:
                layoutPadding = _layoutBorderBottom;
                break;
        }

        // Reduce the display size by our border spacing
        context.DisplayRectangle = CommonHelper.ApplyPadding(_borderBackOrient, context.DisplayRectangle, layoutPadding);

        // Layout the content using the reduced size
        base.Layout(context);

        // Put back the original size before returning
        context.DisplayRectangle = ClientRectangle;
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform a render of the elements.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void Render([DisallowNull] RenderContext context)
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // Ensure we are using the correct palette
        CheckPaletteState(context);

        // Use renderer to draw the tab background
        var mementoIndex = StateIndex(State);
        _mementos[mementoIndex] = context.Renderer.RenderRibbon.DrawRibbonBack(_lastRibbonShape,
            context,
            CommonHelper.ApplyPadding(_borderBackOrient, ClientRectangle, _drawBorder),
            State,
            _currentBack,
            _borderBackOrient,
            _mementos[mementoIndex]);

        // Let base class draw the child items
        base.Render(context);
    }
    #endregion

    #region IContentValues
    /// <summary>
    /// Gets the content short text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetShortText() => Page!.GetTextMapping(Navigator.Bar.BarMapText);

    /// <summary>
    /// Gets the content image.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Image value.</returns>
    public Image? GetImage(PaletteState state) => Page!.GetImageMapping(Navigator.Bar.BarMapImage);

    /// <summary>
    /// Gets the image color that should be transparent.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Color value.</returns>
    public Color GetImageTransparentColor(PaletteState state) => Color.Empty;

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetLongText() => Page!.GetTextMapping(Navigator.Bar.BarMapExtraText);

    #endregion

    #region Protected
    /// <summary>
    /// Processes the RightClick event from the button. 
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnRightClick(object? sender, MouseEventArgs e)
    {
        // Can only select the page if not already selected and allowed to select a tab
        if ((Navigator.SelectedPage != Page) && Navigator.AllowTabSelect)
        {
            Navigator.SelectedPage = Page;
        }

        // Generate event so user can decide what, if any, context menu to show
        var scma = new ShowContextMenuArgs(Page, Navigator.Pages.IndexOf(Page!));
        Navigator.OnShowContextMenu(scma);

        // Do we need to show a context menu
        if (!scma.Cancel)
        {
            if (CommonHelper.ValidKryptonContextMenu(scma.KryptonContextMenu))
            {
                scma.KryptonContextMenu!.Show(Navigator, Navigator.PointToScreen(new Point(e.X, e.Y)));
            }
            else if (scma.ContextMenuStrip != null)
            {
                if (CommonHelper.ValidContextMenuStrip(scma.ContextMenuStrip))
                {
                    scma.ContextMenuStrip.Show(Navigator.PointToScreen(new Point(e.X, e.Y)));
                }
            }
        }
    }

    /// <summary>
    /// Raises the NeedPaint event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    protected virtual void OnNeedPaint(object? sender, NeedLayoutEventArgs e) => _needPaint?.Invoke(this, e);
    #endregion

    #region Implementation
    private int StateIndex(PaletteState state)
    {
        Array stateValues = Enum.GetValues(typeof(PaletteState));

        PaletteState? ps;

        for (var i = 0; i < stateValues.Length; i++)
        {
            ps = stateValues.GetValue(i) as PaletteState?;

            if (ps is not null && ps == state)
            {
                return i;
            }
        }

        return 0;
    }

    private void CheckPaletteState(ViewContext context)
    {
        // Default to using this element calculated state
        PaletteState buttonState = State;

        // If the actual control is not enabled, force to disabled state
        if (!IsFixed && !context.Control!.Enabled)
        {
            buttonState = PaletteState.Disabled;
        }
        else if (buttonState == PaletteState.Disabled)
        {
            buttonState = PaletteState.Normal;
        }

        if (!IsFixed)
        {
            if (Checked)
            {
                switch (buttonState)
                {
                    case PaletteState.Normal:
                    case PaletteState.CheckedNormal:
                        buttonState = PaletteState.CheckedNormal;
                        break;
                    case PaletteState.Tracking:
                    case PaletteState.CheckedTracking:
                        buttonState = PaletteState.CheckedTracking;
                        break;
                    case PaletteState.Pressed:
                    case PaletteState.CheckedPressed:
                        buttonState = PaletteState.CheckedPressed;
                        break;
                }
            }
            else
            {
                switch (buttonState)
                {
                    case PaletteState.Normal:
                    case PaletteState.CheckedNormal:
                        buttonState = PaletteState.Normal;
                        break;
                    case PaletteState.Tracking:
                    case PaletteState.CheckedTracking:
                        buttonState = PaletteState.Tracking;
                        break;
                    case PaletteState.Pressed:
                    case PaletteState.CheckedPressed:
                        buttonState = PaletteState.Pressed;
                        break;
                }
            }
        }

        // Set the correct palette based on state
        switch (buttonState)
        {
            case PaletteState.Disabled:
                _currentText = Navigator.StateDisabled.RibbonTab.TabDraw;
                _currentBack = Navigator.StateDisabled.RibbonTab.TabDraw;
                _currentContent = Navigator.StateDisabled.RibbonTab.Content;
                break;

            case PaletteState.Normal:
                _currentText = _overrideStateNormal;
                _currentBack = _overrideStateNormal;
                _currentContent = _overrideStateNormal;
                break;

            case PaletteState.Tracking:
                _currentText = _overrideStateTracking;
                _currentBack = _overrideStateTracking;
                _currentContent = _overrideStateTracking;
                break;

            case PaletteState.Pressed:
                _currentText = _overrideStatePressed;
                _currentBack = _overrideStatePressed;
                _currentContent = _overrideStatePressed;
                break;

            case PaletteState.CheckedNormal:
            case PaletteState.CheckedTracking:
            case PaletteState.CheckedPressed:
                _currentText = _overrideStateSelected;
                _currentBack = _overrideStateSelected;
                _currentContent = _overrideStateSelected;
                break;

            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(buttonState.ToString());
                break;
        }

        // Switch the child elements over to correct state
        ElementState = buttonState;
        this[0]![0]!.ElementState = buttonState;

        // Update content palette with the current ribbon text palette
        _contentProvider.PaletteRibbonText = _currentText;
        _contentProvider.PaletteContent = _currentContent;
    }

    private void OnClick(object? sender, EventArgs e)
    {
        // Generate click event for the page header
        Navigator.OnTabClicked(new KryptonPageEventArgs(Page, Navigator.Pages.IndexOf(Page!)));

        // If this click is within the double click time of the last one, generate the double click event.
        DateTime now = DateTime.Now;
        if ((now - _lastClick).TotalMilliseconds < SystemInformation.DoubleClickTime)
        {
            // Tell button controller to abort any drag attempt
            _buttonController!.ClearDragRect();

            // Generate click event for the page header
            Navigator.OnTabDoubleClicked(new KryptonPageEventArgs(Page, Navigator.Pages.IndexOf(Page!)));

            // Prevent a third click causing another double click by resetting the now time backwards
            now = now.AddDays(-1);
        }

        _lastClick = now;

        // Can only select the page if not already selected and allowed a selected tab
        if ((Navigator.SelectedPage != Page) && Navigator.AllowTabSelect)
        {
            Navigator.SelectedPage = Page;
        }

        // If the page is actually now selected
        if (Navigator.SelectedPage == Page)
        {
            // If in a tabs only mode then show the popup for the page
            if (Navigator.NavigatorMode == NavigatorMode.BarRibbonTabOnly)
            {
                Navigator.ShowPopupPage(Page!, this, null);
            }
        }
    }

    private void OnDragStart(object? sender, DragStartEventCancelArgs e) => Navigator.InternalDragStart(e, Page);

    private void OnDragMove(object? sender, PointEventArgs e) => Navigator.InternalDragMove(e);

    private void OnDragEnd(object? sender, PointEventArgs e) => Navigator.InternalDragEnd(e);

    private void OnDragQuit(object? sender, EventArgs e) => Navigator.InternalDragQuit();

    private void OnButtonDragRectangle(object? sender, ButtonDragRectangleEventArgs e) => ButtonDragRectangle?.Invoke(this, e);

    private void OnButtonDragOffset(object? sender, ButtonDragOffsetEventArgs e) => ButtonDragOffset?.Invoke(this, e);
    #endregion
}
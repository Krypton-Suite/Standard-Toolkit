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

/// <summary>
/// Navigator view base element for drawing a check button for a krypton page.
/// </summary>
internal abstract class ViewDrawNavCheckButtonBase : ViewDrawButton,
    IContentValues
{
    #region Instance Fields

    private KryptonPage? _page;
    private NeedPaintHandler? _needPaint;
    private PageButtonController? _buttonController;
    private DateTime _lastClick;

    /// <summary>Override for accessing the disable state.</summary>
    protected PaletteTripleOverride _overrideDisabled;
    /// <summary>Override for accessing the normal state.</summary>
    protected PaletteTripleOverride _overrideNormal;
    /// <summary>Override for accessing the tracking state.</summary>
    protected PaletteTripleOverride _overrideTracking;
    /// <summary>Override for accessing the pressed state.</summary>
    protected PaletteTripleOverride _overridePressed;
    /// <summary>Override for accessing the selected state.</summary>
    protected PaletteTripleOverride _overrideSelected;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the drag rectangle for the button is required.
    /// </summary>
    public event EventHandler<ButtonDragRectangleEventArgs>? ButtonDragRectangle;

    /// <summary>
    /// Occurs when the drag offset for the button is changed.
    /// </summary>
    public event EventHandler<ButtonDragOffsetEventArgs>? ButtonDragOffset;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawNavCheckButtonBase class.
    /// </summary>
    /// <param name="navigator">Owning navigator instance.</param>
    /// <param name="page">Page this check button represents.</param>
    /// <param name="orientation">Orientation for the check button.</param>
    /// <param name="overflow">Button is used on the overflow bar.</param>
    public ViewDrawNavCheckButtonBase(KryptonNavigator navigator,
        KryptonPage? page,
        VisualOrientation orientation,
        bool overflow)
        : this(navigator, page, orientation,
            page?.StateDisabled?.OverflowButton!,
            page?.StateNormal?.OverflowButton!,
            page?.StateTracking.OverflowButton!,
            page?.StatePressed.OverflowButton!,
            page?.StateSelected.OverflowButton!,
            page?.OverrideFocus?.OverflowButton!)
    {
    }

    /// <summary>
    /// Initialize a new instance of the ViewDrawNavCheckButtonBase class.
    /// </summary>
    /// <param name="navigator">Owning navigator instance.</param>
    /// <param name="page">Page this check button represents.</param>
    /// <param name="orientation">Orientation for the check button.</param>
    public ViewDrawNavCheckButtonBase(KryptonNavigator navigator,
        KryptonPage? page,
        VisualOrientation orientation)
        : this(navigator, page, orientation,
            page?.StateDisabled?.CheckButton!,
            page?.StateNormal?.CheckButton!,
            page?.StateTracking.CheckButton!,
            page?.StatePressed.CheckButton!,
            page?.StateSelected.CheckButton!,
            page?.OverrideFocus.CheckButton!)
    {
    }

    /// <summary>
    /// Initialize a new instance of the ViewDrawNavCheckButtonBase class.
    /// </summary>
    /// <param name="navigator">Owning navigator instance.</param>
    /// <param name="page">Page this check button represents.</param>
    /// <param name="orientation">Orientation for the check button.</param>
    /// <param name="stateDisabled">Source for disabled state values.</param>
    /// <param name="stateNormal">Source for normal state values.</param>
    /// <param name="stateTracking">Source for tracking state values.</param>
    /// <param name="statePressed">Source for pressed state values.</param>
    /// <param name="stateSelected">Source for selected state values.</param>
    /// <param name="stateFocused">Source for focused state values.</param>
    public ViewDrawNavCheckButtonBase([DisallowNull] KryptonNavigator navigator,
        KryptonPage? page,
        VisualOrientation orientation,
        IPaletteTriple stateDisabled,
        IPaletteTriple stateNormal,
        IPaletteTriple stateTracking,
        IPaletteTriple statePressed,
        IPaletteTriple stateSelected,
        IPaletteTriple stateFocused)
        : base(stateDisabled, stateNormal, stateTracking,
            statePressed, null, null, orientation, true)
    {
        Debug.Assert(navigator is not null);

        Navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));
        _page = page;
        _lastClick = DateTime.Now.AddDays(-1);

        // Associate the page component with this view element
        Component = page;

        // Prevent user from un-checking the selected check button
        AllowUncheck = false;

        // Set the source for values to ourself
        ButtonValues = this;

        // Create a controller for managing button behavior
        IMouseController controller = CreateMouseController();
        MouseController = controller;

        // Create overrides for getting the focus values
        _overrideDisabled = new PaletteTripleOverride(stateFocused, stateDisabled, PaletteState.FocusOverride);
        _overrideNormal = new PaletteTripleOverride(stateFocused, stateNormal, PaletteState.FocusOverride);
        _overrideTracking = new PaletteTripleOverride(stateFocused, stateTracking, PaletteState.FocusOverride);
        _overridePressed = new PaletteTripleOverride(stateFocused, statePressed, PaletteState.FocusOverride);
        _overrideSelected = new PaletteTripleOverride(stateFocused, stateSelected, PaletteState.FocusOverride);

        // Push values into the base class
        SetPalettes(_overrideDisabled, _overrideNormal, _overrideTracking, _overridePressed);
        SetCheckedPalettes(_overrideSelected, _overrideSelected, _overrideSelected);

        // Are we allowed to add button specs to the button?
        if (AllowButtonSpecs)
        {
            // Create button specification collection manager
            ButtonSpecManager = new ButtonSpecNavManagerLayoutBar(Navigator, Navigator.InternalRedirector, Page?.ButtonSpecs, null,
                new[] { LayoutDocker },
                new IPaletteMetric[] { Navigator.StateCommon! },
                new[] { PaletteMetricInt.PageButtonInset },
                new[] { PaletteMetricInt.PageButtonInset },
                new[] { PaletteMetricPadding.PageButtonPadding },
                Navigator.CreateToolStripRenderer,
                null)
            {

                // Hook up the tooltip manager so that tooltips can be generated
                ToolTipManager = Navigator.ToolTipManager
            };

            // Allow derived classes to update the remapping with different values
            UpdateButtonSpecMapping();
        }
    }

    /// <summary>
    /// Release unmanaged and optionally managed resources.
    /// </summary>
    /// <param name="disposing">Called from Dispose method.</param>
    protected override void Dispose(bool disposing)
    {
        if (ButtonSpecManager != null)
        {
            ButtonSpecManager.Destruct();
            ButtonSpecManager = null;
        }

        // Must call base class to finish disposing
        base.Dispose(disposing);
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawNavCheckButtonBase:{Id}";

    #endregion

    #region NeedPaint
    /// <summary>
    /// Gets and sets the need paint delegate for notifying paint requests.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public NeedPaintHandler? NeedPaint
    {
        get => _needPaint;

        set
        {
            // Warn if multiple sources want to hook their single delegate
            Debug.Assert(((_needPaint == null) && (value != null)) ||
                         ((_needPaint != null) && (value == null)));

            _needPaint = value;

            if (ButtonSpecManager != null)
            {
                ButtonSpecManager.NeedPaint = _needPaint;
            }
        }
    }
    #endregion

    #region HasFocus
    /// <summary>
    /// Gets and sets if the check button has the focus indication.
    /// </summary>
    public bool HasFocus
    {
        get => _overrideDisabled.Apply;

        set
        {
            if (_overrideDisabled.Apply != value)
            {
                _overrideDisabled.Apply = value;
                _overrideNormal.Apply = value;
                _overrideTracking.Apply = value;
                _overridePressed.Apply = value;
                _overrideSelected.Apply = value;
            }
        }
    }
    #endregion

    #region Page
    /// <summary>
    /// Gets the page this view represents.
    /// </summary>
    public virtual KryptonPage? Page
    {
        get => _page;

        set
        {
            if (_page != value)
            {
                _page = value;

                if (_page != null)
                {
                    _overrideDisabled.SetPalettes(_page.OverrideFocus.CheckButton, _page.StateDisabled.CheckButton);
                    _overrideNormal.SetPalettes(_page.OverrideFocus.CheckButton, _page.StateNormal.CheckButton);
                    _overrideTracking.SetPalettes(_page.OverrideFocus.CheckButton, _page.StateTracking.CheckButton);
                    _overridePressed.SetPalettes(_page.OverrideFocus.CheckButton, _page.StatePressed.CheckButton);
                    _overrideSelected.SetPalettes(_page.OverrideFocus.CheckButton, _page.StateSelected.CheckButton);
                }
                else
                {
                    _overrideDisabled.SetPalettes(Navigator.OverrideFocus.CheckButton, Navigator.StateDisabled.CheckButton);
                    _overrideNormal.SetPalettes(Navigator.OverrideFocus.CheckButton, Navigator.StateNormal.CheckButton);
                    _overrideTracking.SetPalettes(Navigator.OverrideFocus.CheckButton, Navigator.StateTracking.CheckButton);
                    _overridePressed.SetPalettes(Navigator.OverrideFocus.CheckButton, Navigator.StatePressed.CheckButton);
                    _overrideSelected.SetPalettes(Navigator.OverrideFocus.CheckButton, Navigator.StateSelected.CheckButton);
                }
            }
        }
    }
    #endregion

    #region Navigator
    /// <summary>
    /// Gets the navigator that owns this view.
    /// </summary>
    public KryptonNavigator Navigator { get; }

    #endregion

    #region ButtonSpecFromView
    /// <summary>
    /// Gets the ButtonSpec associated with the provided item.
    /// </summary>
    /// <param name="element">Element to search against.</param>
    /// <returns>Reference to ButtonSpec; otherwise null.</returns>
    public ButtonSpec? ButtonSpecFromView(ViewBase element) => ButtonSpecManager?.ButtonSpecFromView(element);

    #endregion

    #region AllowButtonSpecs
    /// <summary>
    /// Gets a value indicating if button specs are allowed on the button.
    /// </summary>
    public virtual bool AllowButtonSpecs => true;

    #endregion

    #region PerformClick
    /// <summary>
    /// Raises the Click event for the button.
    /// </summary>
    public void PerformClick() => OnClick(this, EventArgs.Empty);
    #endregion

    #region IContentValues
    /// <summary>
    /// Gets the content image.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Image value.</returns>
    public abstract Image? GetImage(PaletteState state);

    /// <summary>
    /// Gets the image color that should be transparent.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Color value.</returns>
    public Color GetImageTransparentColor(PaletteState state) => Color.Empty;

    /// <summary>
    /// Gets the content short text.
    /// </summary>
    /// <returns>String value.</returns>
    public abstract string GetShortText();

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    /// <returns>String value.</returns>
    public abstract string GetLongText();
    #endregion

    #region ButtonClickOnDown
    /// <summary>
    /// Should the item be selected on the mouse down.
    /// </summary>
    protected virtual bool ButtonClickOnDown => false;

    #endregion

    #region ButtonSpecManager
    /// <summary>
    /// Gets access to the button spec manager used for this button.
    /// </summary>
    public ButtonSpecNavManagerLayoutBar? ButtonSpecManager { get; private set; }

    #endregion

    #region UpdateButtonSpecMapping
    /// <summary>
    /// Update the button spec manager mapping to reflect current settings.
    /// </summary>
    public virtual void UpdateButtonSpecMapping()
    {
        // Define a default mapping for text color and recreate to use that new setting
        if (ButtonSpecManager != null)
        {
            ButtonSpecManager.SetRemapTarget(Navigator.Bar.CheckButtonStyle);
            ButtonSpecManager.RecreateButtons();
        }
    }
    #endregion

    #region CreateMouseController
    /// <summary>
    /// Create a mouse controller appropriate for operating this button.
    /// </summary>
    /// <returns>Reference to IMouseController interface.</returns>
    protected virtual IMouseController CreateMouseController()
    {
        // Create the button controller
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

        // Should the item be selected on the mouse down or the mouse up?
        _buttonController.ClickOnDown = ButtonClickOnDown;

        // We need to be notified of got/lost focus and keyboard events
        SourceController = _buttonController;
        KeyController = _buttonController;

        // Create two decorators in order to support tooltips and hover events
        var toolTipController = new ToolTipController(Navigator.ToolTipManager!, this, _buttonController);
        var hoverController = new ToolTipController(Navigator.HoverManager!, this, toolTipController);

        return hoverController;
    }
    #endregion

    #region OnNeedPaint
    /// <summary>
    /// Raises the NeedPaint event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    protected virtual void OnNeedPaint(object? sender, NeedLayoutEventArgs e) => _needPaint?.Invoke(this, e);
    #endregion

    #region OnClick
    /// <summary>
    /// Processes the Click event from the button. 
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnClick(object? sender, EventArgs e)
    {
        // Generate click event for the page header
        Navigator.OnTabClicked(new KryptonPageEventArgs(_page, Navigator.Pages.IndexOf(_page!)));

        // If this click is within the double click time of the last one, generate the double click event.
        DateTime now = DateTime.Now;
        if ((now - _lastClick).TotalMilliseconds < SystemInformation.DoubleClickTime)
        {
            // Tell button controller to abort any drag attempt
            _buttonController!.ClearDragRect();

            // Generate click event for the page header
            Navigator.OnTabDoubleClicked(new KryptonPageEventArgs(_page, Navigator.Pages.IndexOf(_page!)));

            // Prevent a third click causing another double click by resetting the now time backwards
            now = now.AddDays(-1);
        }

        _lastClick = now;

        // Can only select the page if not already selected and allowed to have a selected tab
        if ((Navigator.SelectedPage != _page) && Navigator.AllowTabSelect)
        {
            // This event might have caused the page to be removed or hidden and so check the page is still present before selecting it
            if (Navigator.ChildPanel?.Controls.Contains(_page) == true && _page!.LastVisibleSet)
            {
                Navigator.SelectedPage = _page;
            }
        }
    }

    /// <summary>
    /// Processes the RightClick event from the button. 
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnRightClick(object? sender, MouseEventArgs e)
    {
        // Can only select the page if not already selected and allowed to select a tab
        if ((Navigator.SelectedPage != _page) && Navigator.AllowTabSelect)
        {
            Navigator.SelectedPage = _page;
        }

        // Generate event so user can decide what, if any, context menu to show
        var scma = new ShowContextMenuArgs(_page, Navigator.Pages.IndexOf(_page!));
        Navigator.OnShowContextMenu(scma);

        // Do we need to show a context menu
        if (!scma.Cancel)
        {
            if (CommonHelper.ValidKryptonContextMenu(scma.KryptonContextMenu))
            {
                scma.KryptonContextMenu!.Show(Navigator, Navigator.PointToScreen(new Point(e.X, e.Y)));
            }
            else
            {
                if (CommonHelper.ValidContextMenuStrip(scma.ContextMenuStrip))
                {
                    scma.ContextMenuStrip!.Show(Navigator.PointToScreen(new Point(e.X, e.Y)));
                }
            }
        }
    }
    #endregion

    #region Implementation
    private void OnDragStart(object? sender, DragStartEventCancelArgs e) => Navigator.InternalDragStart(e, _page);

    private void OnDragMove(object? sender, PointEventArgs e) => Navigator.InternalDragMove(e);

    private void OnDragEnd(object? sender, PointEventArgs e) => Navigator.InternalDragEnd(e);

    private void OnDragQuit(object? sender, EventArgs e) => Navigator.InternalDragQuit();

    private void OnButtonDragRectangle(object? sender, ButtonDragRectangleEventArgs e) => ButtonDragRectangle?.Invoke(this, e);

    private void OnButtonDragOffset(object? sender, ButtonDragOffsetEventArgs e) => ButtonDragOffset?.Invoke(this, e);
    #endregion
}
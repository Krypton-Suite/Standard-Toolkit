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
/// Implements the NavigatorMode.HeaderGroupTab view.
/// </summary>
internal class ViewBuilderHeaderBarTabGroup : ViewBuilderBarTabBase
{
    #region Instance Fields
    private ViewLayoutInsetOverlap _layoutOverlap;
    private ViewDrawDocker _viewHeadingPrimary;
    private ViewDrawContent _viewContentPrimary;
    private ViewDrawDocker _viewHeadingSecondary;
    private ViewDrawContent _viewContentSecondary;
    private ViewLayoutDocker _topGroup;
    #endregion

    #region Public
    /// <summary>
    /// Construct the view appropriate for this builder.
    /// </summary>
    /// <param name="navigator">Reference to navigator instance.</param>
    /// <param name="manager">Reference to current manager.</param>
    /// <param name="redirector">Palette redirector.</param>
    public override void Construct(KryptonNavigator navigator,
        ViewManager manager,
        PaletteRedirect redirector)
    {
        // Let base class perform common operations
        base.Construct(navigator, manager, redirector);

        // Setup the drag and drop handler
        CreateDragDrop();
    }

    /// <summary>
    /// Gets a value indicating if the mode is a tab strip style mode.
    /// </summary>
    public override bool IsTabStripMode => false;

    /// <summary>
    /// Gets the ButtonSpec associated with the provided view element.
    /// </summary>
    /// <param name="element">Element to search against.</param>
    /// <returns>Reference to ButtonSpec; otherwise null.</returns>
    public override ButtonSpec? ButtonSpecFromView(ViewBase element) =>
        // Ask the button manager for the button spec for this element
        _buttonManager!.ButtonSpecFromView(element);

    /// <summary>
    /// Process a change in the selected page
    /// </summary>
    public override void SelectedPageChanged()
    {
        UpdateStatePalettes();
        UpdateButtons();
        base.SelectedPageChanged();
    }

    /// <summary>
    /// Change has occurred to the collection of pages.
    /// </summary>
    public override void PageCollectionChanged()
    {
        UpdateStatePalettes();
        UpdateButtons();
        base.PageCollectionChanged();
    }

    /// <summary>
    /// Process a change in the visible state for a page.
    /// </summary>
    /// <param name="page">Page that has changed visible state.</param>
    public override void PageVisibleStateChanged(KryptonPage? page)
    {
        UpdateButtons();
        base.PageVisibleStateChanged(page);
    }

    /// <summary>
    /// Process a change in the enabled state for a page.
    /// </summary>
    /// <param name="page">Page that has changed enabled state.</param>
    public override void PageEnabledStateChanged(KryptonPage? page)
    {
        // If the page we are showing has changed
        if (page == Navigator.SelectedPage)
        {
            // Update to use the correct enabled/disabled palette
            UpdateStatePalettes();
            UpdateButtons();

            // Need to repaint to show the change
            Navigator.PerformNeedPaint(true);
        }

        // Let base class do standard work
        base.PageEnabledStateChanged(page);
    }

    /// <summary>
    /// Gets the screen coordinates for showing a context action menu.
    /// </summary>
    /// <returns>Point in screen coordinates.</returns>
    public override Point GetContextShowPoint()
    {
        // Get the display rectangle of the context button
        Rectangle rect = _buttonManager!.GetButtonRectangle(Navigator.Button.ContextButton);

        // We want the context menu to show just below the button
        var pt = new Point(rect.Left, rect.Bottom + 3);

        // Convert from control coordinates to screen coordinates
        return Navigator.PointToScreen(pt);
    }

    /// <summary>
    /// Calculate the enabled state of the next button based on the required action.
    /// </summary>
    /// <param name="action">Requested action.</param>
    /// <returns>Enabled state of the button.</returns>
    public override ButtonEnabled NextActionEnabled(DirectionButtonAction action)
    {
        // Our mode appropriate action is always to select a page
        if (action == DirectionButtonAction.ModeAppropriateAction)
        {
            action = DirectionButtonAction.SelectPage;
        }

        // Let base class perform basic action calculations
        return base.NextActionEnabled(action);
    }

    /// <summary>
    /// Perform the next button action requested.
    /// </summary>
    /// <param name="action">Requested action.</param>
    /// <param name="page">Selected page at time of action request.</param>
    public override void PerformNextAction(DirectionButtonAction action, KryptonPage? page)
    {
        // Our mode appropriate action is always to select a page
        if (action == DirectionButtonAction.ModeAppropriateAction)
        {
            action = DirectionButtonAction.SelectPage;
        }

        // Let base class perform basic actions
        base.PerformNextAction(action, page);
    }

    /// <summary>
    /// Calculate the enabled state of the previous button based on the required action.
    /// </summary>
    /// <param name="action">Requested action.</param>
    /// <returns>Enabled state of the button.</returns>
    public override ButtonEnabled PreviousActionEnabled(DirectionButtonAction action)
    {
        // Our mode appropriate action is always to select a page
        if (action == DirectionButtonAction.ModeAppropriateAction)
        {
            action = DirectionButtonAction.SelectPage;
        }

        // Let base class perform basic action calculations
        return base.PreviousActionEnabled(action);
    }

    /// <summary>
    /// Perform the previous button action requested.
    /// </summary>
    /// <param name="action">Requested action.</param>
    /// <param name="page">Selected page at time of action request.</param>
    public override void PerformPreviousAction(DirectionButtonAction action, KryptonPage? page)
    {
        // Our mode appropriate action is always to select a page
        if (action == DirectionButtonAction.ModeAppropriateAction)
        {
            action = DirectionButtonAction.SelectPage;
        }

        // Let base class perform basic actions
        base.PerformPreviousAction(action, page);
    }

    /// <summary>
    /// Recreate the buttons to reflect a change in selected page.
    /// </summary>
    public void UpdateButtons() =>
        // Ensure buttons are recreated to reflect different page
        _buttonManager!.RecreateButtons();
    #endregion

    #region Protected
    /// <summary>
    /// Perform post create tasks.
    /// </summary>
    protected override void PostCreate()
    {
        // Let base class perform standard actions
        base.PostCreate();

        UpdateStatePalettes();
        UpdateHeaders();
        UpdateButtons();
    }

    /// <summary>
    /// Create the view hierarchy for this view mode.
    /// </summary>
    protected override void CreateCheckItemView()
    {
        // Create the two headers and header content
        _viewContentPrimary = new ViewDrawContent(Navigator.StateNormal.HeaderGroup.HeaderPrimary.Content,
            Navigator.Header.HeaderValuesPrimary,
            VisualOrientation.Top);

        _viewHeadingPrimary = new ViewDrawDocker(Navigator.StateNormal.HeaderGroup.HeaderPrimary.Back,
            Navigator.StateNormal.HeaderGroup.HeaderPrimary.Border,
            Navigator.StateNormal.HeaderGroup.HeaderPrimary,
            PaletteMetricBool.None,
            PaletteMetricPadding.HeaderGroupPaddingPrimary,
            VisualOrientation.Top);

        _viewContentSecondary = new ViewDrawContent(Navigator.StateNormal.HeaderGroup.HeaderSecondary.Content,
            Navigator.Header.HeaderValuesSecondary,
            VisualOrientation.Top);

        _viewHeadingSecondary = new ViewDrawDocker(Navigator.StateNormal.HeaderGroup.HeaderSecondary.Back,
            Navigator.StateNormal.HeaderGroup.HeaderSecondary.Border,
            Navigator.StateNormal.HeaderGroup.HeaderSecondary,
            PaletteMetricBool.None,
            PaletteMetricPadding.HeaderGroupPaddingSecondary,
            VisualOrientation.Top);

        // Place the the content as fillers in the headers
        _viewHeadingPrimary.Add(_viewContentPrimary, ViewDockStyle.Fill);
        _viewHeadingSecondary.Add(_viewContentSecondary, ViewDockStyle.Fill);

        // Create a canvas for containing the selected page and put old root inside it
        _drawGroup = new ViewDrawCanvas(Navigator.StateNormal.HeaderGroup.Back,
            Navigator.StateNormal.HeaderGroup.Border,
            VisualOrientation.Top)
        {
            ApplyIncludeBorderEdge = true
        };
        _drawGroup.Add(_oldRoot);

        // Create the view element that lays out the check/tab buttons
        var layoutBar = new ViewLayoutBarForTabs(Navigator.Bar.ItemSizing,
            Navigator.Bar.ItemAlignment, Navigator.Bar.BarMultiline, Navigator.Bar.ItemMinimumSize,
            Navigator.Bar.ItemMaximumSize, Navigator.Bar.BarMinimumHeight, Navigator.Bar.TabBorderStyle, true);
        _layoutBar = layoutBar;

        // Create the scroll spacer that restricts display
        _layoutBarViewport = new ViewLayoutViewport(Navigator.StateCommon!.Bar,
            PaletteMetricPadding.BarPaddingTabs,
            PaletteMetricInt.CheckButtonGap,
            Navigator.Bar.BarOrientation,
            Navigator.Bar.ItemAlignment,
            Navigator.Bar.BarAnimation)
        {
            _layoutBar
        };

        // Create the button bar area docker
        _layoutBarDocker = new ViewLayoutDocker
        {
            { _layoutBarViewport, ViewDockStyle.Fill }
        };

        // Add a separators for insetting items
        _layoutBarSeparatorFirst = new ViewLayoutSeparator(0);
        _layoutBarSeparatorLast = new ViewLayoutSeparator(0);
        _layoutBarDocker.Add(_layoutBarSeparatorFirst, ViewDockStyle.Left);
        _layoutBarDocker.Add(_layoutBarSeparatorLast, ViewDockStyle.Right);

        // Create the layout that insets the contents to allow for rounding of the group border
        _layoutOverlap = new ViewLayoutInsetOverlap(_drawGroup)
        {
            _layoutBarDocker
        };

        // Create the docker used to layout contents of main panel and fill with group
        _layoutPanelDocker = new ViewLayoutDockerOverlap(_drawGroup, _layoutOverlap, layoutBar)
        {
            { _layoutOverlap, ViewDockStyle.Top },
            { _drawGroup, ViewDockStyle.Fill }
        };

        // Place the headers and page holding area into the group
        _topGroup = new ViewLayoutDocker
        {
            { _viewHeadingSecondary, ViewDockStyle.Bottom },
            { _viewHeadingPrimary, ViewDockStyle.Top },
            { _layoutPanelDocker, ViewDockStyle.Fill }
        };

        // Prevent adjacent headers from having two borders
        _topGroup.RemoveChildBorders = true;

        // Create the top level panel and put a layout docker inside it
        _drawPanel = new ViewDrawPanel(Navigator.StateNormal.Back)
        {
            _topGroup
        };
        _newRoot = _drawPanel;

        // Set initial visible state of headers
        _viewHeadingPrimary.Visible = Navigator.Header.HeaderVisiblePrimary;
        _viewHeadingSecondary.Visible = Navigator.Header.HeaderVisibleSecondary;

        // Set the correct tab style
        UpdateTabStyle();

        // Must call the base class to perform common actions
        base.CreateCheckItemView();
    }

    /// <summary>
    /// Ensure the correct state palettes are being used.
    /// </summary>
    public override void UpdateStatePalettes()
    {
        // Let base class perform common actions
        base.UpdateStatePalettes();

        // If whole navigator is disabled then all of view is disabled
        var enabled = Navigator.Enabled;

        // If there is no selected page
        if (Navigator.SelectedPage == null)
        {
            // Then use the states defined in the navigator itself
            SetPalettes((Navigator.Enabled
                ? Navigator.StateNormal.HeaderGroup
                : Navigator.StateDisabled.HeaderGroup));
        }
        else
        {
            // Use states defined in the selected page
            if (Navigator.SelectedPage.Enabled)
            {
                SetPalettes(Navigator.SelectedPage!.StateNormal.HeaderGroup);
            }
            else
            {
                SetPalettes(Navigator.SelectedPage!.StateDisabled.HeaderGroup);

                // If page is disabled then all of view should look disabled
                enabled = false;
            }
        }

        // Update enabled appearance of view
        SetEnabled(enabled);

        // Base class sets metrics reference, but we want to override
        // this to be null so that the tab border gap is used instead
        _layoutBar.SetMetrics(null);
    }

    /// <summary>
    /// Update the bar orientation.
    /// </summary>
    protected override void UpdateOrientation()
    {
        // Let base class update other view elements
        base.UpdateOrientation();

        // The view group should always include the bar orientation edge when drawing
        _drawGroup!.IncludeBorderEdge = Navigator.Bar.BarOrientation;

        switch (Navigator.Bar.BarOrientation)
        {
            case VisualOrientation.Top:
                _layoutPanelDocker.SetDock(_layoutOverlap, ViewDockStyle.Top);
                _layoutBarViewport.CounterAlignment = RelativePositionAlign.Far;
                break;
            case VisualOrientation.Bottom:
                _layoutPanelDocker.SetDock(_layoutOverlap, ViewDockStyle.Bottom);
                _layoutBarViewport.CounterAlignment = RelativePositionAlign.Near;
                break;
            case VisualOrientation.Left:
                _layoutPanelDocker.SetDock(_layoutOverlap, ViewDockStyle.Left);
                _layoutBarViewport.CounterAlignment = RelativePositionAlign.Far;
                break;
            case VisualOrientation.Right:
                _layoutPanelDocker.SetDock(_layoutOverlap, ViewDockStyle.Right);
                _layoutBarViewport.CounterAlignment = RelativePositionAlign.Near;
                break;
        }

        // Keep the overlap layout in sync with the bar orientation
        _layoutOverlap.Orientation = Navigator.Bar.BarOrientation;
    }

    /// <summary>
    /// Process the change in a property that might effect the view builder.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Property changed details.</param>
    protected override void OnViewBuilderPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case @"HeaderStylePrimary":
                SetHeaderStyle(_viewHeadingPrimary, Navigator.StateCommon!.HeaderGroup.HeaderPrimary, Navigator.Header.HeaderStylePrimary);
                UpdateStatePalettes();
                Navigator.PerformNeedPaint(true);
                break;
            case @"HeaderStyleSecondary":
                SetHeaderStyle(_viewHeadingSecondary, Navigator.StateCommon!.HeaderGroup.HeaderSecondary, Navigator.Header.HeaderStyleSecondary);
                UpdateStatePalettes();
                Navigator.PerformNeedPaint(true);
                break;
            case @"HeaderPositionPrimary":
                SetHeaderPosition(_viewHeadingPrimary, _viewContentPrimary, Navigator.Header.HeaderPositionPrimary);
                _buttonManager?.RecreateButtons();
                Navigator.PerformNeedPaint(true);
                break;
            case @"HeaderPositionSecondary":
                SetHeaderPosition(_viewHeadingSecondary, _viewContentSecondary, Navigator.Header.HeaderPositionSecondary);
                _buttonManager?.RecreateButtons();
                Navigator.PerformNeedPaint(true);
                break;
            case @"HeaderVisiblePrimary":
                _viewHeadingPrimary.Visible = Navigator.Header.HeaderVisiblePrimary;
                Navigator.PerformNeedPaint(true);
                break;
            case @"HeaderVisibleSecondary":
                _viewHeadingSecondary.Visible = Navigator.Header.HeaderVisibleSecondary;
                Navigator.PerformNeedPaint(true);
                break;
            case @"PreviousButtonDisplay":
            case @"PreviousButtonAction":
            case @"NextButtonDisplay":
            case @"NextButtonAction":
            case @"ContextButtonDisplay":
            case @"CloseButtonDisplay":
            case nameof(ButtonDisplayLogic):
                _buttonManager?.RecreateButtons();
                break;
        }

        base.OnViewBuilderPropertyChanged(sender, e);
    }
    #endregion

    #region Implementation
    private void CreateDragDrop()
    {
        // Create and attach the drag controller to the header view
        var controller = new DragViewController(_viewHeadingPrimary);
        _viewHeadingPrimary.MouseController = controller;
        _viewHeadingPrimary.KeyController = controller;
        _viewHeadingPrimary.SourceController = controller;

        // Hook into the dragging events for forwarding to the navigator
        controller.DragStart += OnDragStart;
        controller.DragMove += OnDragMove;
        controller.DragEnd += OnDragEnd;
        controller.DragQuit += OnDragQuit;
        controller.LeftMouseDown += OnLeftMouseDown;
        controller.RightMouseDown += OnRightMouseDown;
        controller.LeftDoubleClick += OnLeftDoubleClick;
    }

    protected override void CreateButtonSpecManager() =>
        // Create button specification collection manager
        _buttonManager = new ButtonSpecManagerDraw(Navigator, Redirector, Navigator.Button.ButtonSpecs, Navigator.FixedSpecs,
            new[] { _viewHeadingPrimary, _viewHeadingSecondary },
            new IPaletteMetric[] { Navigator.StateCommon!.HeaderGroup.HeaderPrimary, Navigator.StateCommon.HeaderGroup.HeaderSecondary },
            new[] { PaletteMetricInt.HeaderButtonEdgeInsetPrimary, PaletteMetricInt.HeaderButtonEdgeInsetSecondary },
            new[] { PaletteMetricPadding.HeaderButtonPaddingPrimary, PaletteMetricPadding.HeaderButtonPaddingSecondary },
            Navigator.CreateToolStripRenderer,
            NeedPaintDelegate)
        {

            // Hook up the tooltip manager so that tooltips can be generated
            ToolTipManager = Navigator.ToolTipManager
        };

    private void UpdateHeaders()
    {
        SetHeaderStyle(_viewHeadingPrimary, Navigator.StateCommon!.HeaderGroup.HeaderPrimary, Navigator.Header.HeaderStylePrimary);
        SetHeaderStyle(_viewHeadingSecondary, Navigator.StateCommon.HeaderGroup.HeaderSecondary, Navigator.Header.HeaderStyleSecondary);
        SetHeaderPosition(_viewHeadingPrimary, _viewContentPrimary, Navigator.Header.HeaderPositionPrimary);
        SetHeaderPosition(_viewHeadingSecondary, _viewContentSecondary, Navigator.Header.HeaderPositionSecondary);
    }

    private void SetHeaderStyle(ViewDrawDocker drawDocker,
        PaletteTripleMetricRedirect palette,
        HeaderStyle style)
    {
        palette.SetStyles(style);

        switch (style)
        {
            case HeaderStyle.Primary:
                _buttonManager?.SetDockerMetrics(drawDocker, palette,
                    PaletteMetricInt.HeaderButtonEdgeInsetPrimary,
                    PaletteMetricPadding.HeaderButtonPaddingPrimary);
                break;

            case HeaderStyle.Secondary:
                _buttonManager?.SetDockerMetrics(drawDocker, palette,
                    PaletteMetricInt.HeaderButtonEdgeInsetSecondary,
                    PaletteMetricPadding.HeaderButtonPaddingSecondary);
                break;

            case HeaderStyle.DockActive:
                _buttonManager?.SetDockerMetrics(drawDocker, palette,
                    PaletteMetricInt.HeaderButtonEdgeInsetDockActive,
                    PaletteMetricPadding.HeaderButtonPaddingDockActive);
                break;

            case HeaderStyle.DockInactive:
                _buttonManager?.SetDockerMetrics(drawDocker, palette,
                    PaletteMetricInt.HeaderButtonEdgeInsetDockInactive,
                    PaletteMetricPadding.HeaderButtonPaddingDockInactive);
                break;

            case HeaderStyle.Form:
                _buttonManager?.SetDockerMetrics(drawDocker, palette,
                    PaletteMetricInt.HeaderButtonEdgeInsetForm,
                    PaletteMetricPadding.HeaderButtonPaddingForm);
                break;

            case HeaderStyle.Calendar:
                _buttonManager?.SetDockerMetrics(drawDocker, palette, PaletteMetricInt.HeaderButtonEdgeInsetCalendar, PaletteMetricPadding.HeaderButtonPaddingCalendar);
                break;

            case HeaderStyle.Custom1:
                _buttonManager?.SetDockerMetrics(drawDocker, palette, PaletteMetricInt.HeaderButtonEdgeInsetCustom1, PaletteMetricPadding.HeaderButtonPaddingCustom1);
                break;

            case HeaderStyle.Custom2:
                _buttonManager?.SetDockerMetrics(drawDocker, palette, PaletteMetricInt.HeaderButtonEdgeInsetCustom2, PaletteMetricPadding.HeaderButtonPaddingCustom2);
                break;

            case HeaderStyle.Custom3:
                _buttonManager?.SetDockerMetrics(drawDocker, palette, PaletteMetricInt.HeaderButtonEdgeInsetCustom3, PaletteMetricPadding.HeaderButtonPaddingCustom3);
                break;

            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(style.ToString());
                break;
        }
    }

    private void SetHeaderPosition(ViewDrawCanvas canvas,
        ViewDrawContent content,
        VisualOrientation position)
    {
        switch (position)
        {
            case VisualOrientation.Top:
                _topGroup.SetDock(canvas, ViewDockStyle.Top);
                canvas.Orientation = VisualOrientation.Top;
                content.Orientation = VisualOrientation.Top;
                break;
            case VisualOrientation.Bottom:
                _topGroup.SetDock(canvas, ViewDockStyle.Bottom);
                canvas.Orientation = VisualOrientation.Top;
                content.Orientation = VisualOrientation.Top;
                break;
            case VisualOrientation.Left:
                _topGroup.SetDock(canvas, ViewDockStyle.Left);
                canvas.Orientation = VisualOrientation.Left;
                content.Orientation = VisualOrientation.Left;
                break;
            case VisualOrientation.Right:
                _topGroup.SetDock(canvas, ViewDockStyle.Right);
                canvas.Orientation = VisualOrientation.Right;
                content.Orientation = VisualOrientation.Right;
                break;
        }
    }

    private void SetPalettes([DisallowNull] PaletteHeaderGroup palette)
    {
        _viewHeadingPrimary.SetPalettes(palette.HeaderPrimary.Back, palette.HeaderPrimary.Border, palette.HeaderPrimary);
        _viewHeadingSecondary.SetPalettes(palette.HeaderSecondary.Back, palette.HeaderSecondary.Border, palette.HeaderSecondary);

        _buttonManager?.SetDockerMetrics(_viewHeadingPrimary, palette.HeaderPrimary);
        _buttonManager?.SetDockerMetrics(_viewHeadingSecondary, palette.HeaderSecondary);

        _viewContentPrimary.SetPalette(palette.HeaderPrimary.Content);
        _viewContentSecondary.SetPalette(palette.HeaderSecondary.Content);
    }

    private void SetEnabled(bool enabled)
    {
        _topGroup.Enabled = enabled;
        _viewHeadingPrimary.Enabled = enabled;
        _viewHeadingSecondary.Enabled = enabled;
        _viewContentPrimary.Enabled = enabled;
        _viewContentSecondary.Enabled = enabled;
        _buttonManager?.RecreateButtons();
    }

    private void OnDragStart(object? sender, DragStartEventCancelArgs e) => Navigator.InternalDragStart(e, null);

    private void OnDragMove(object? sender, PointEventArgs e) => Navigator.InternalDragMove(e);

    private void OnDragEnd(object? sender, PointEventArgs e) => Navigator.InternalDragEnd(e);

    private void OnDragQuit(object? sender, EventArgs e) => Navigator.InternalDragQuit();

    private void OnLeftMouseDown(object? sender, EventArgs e) => Navigator.OnPrimaryHeaderLeftClicked(e);

    private void OnRightMouseDown(object? sender, EventArgs e) => Navigator.OnPrimaryHeaderRightClicked(e);

    private void OnLeftDoubleClick(object? sender, EventArgs e) => Navigator.OnPrimaryHeaderDoubleClicked(e);
    #endregion
}
#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Docking;

/// <summary>
/// Docking element that hosts auto-hidden page groups along one edge of a control, including the slide-out panel used to display selected pages.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory("code")]
[DesignTimeVisible(false)]
public class KryptonDockingEdgeAutoHidden : DockingElementClosedCollection
{
    #region Static Fields

    private const int CLIENT_MINIMUM = 22;

    #endregion

    #region Instance Fields

    private readonly KryptonAutoHiddenPanel _panel;
    private readonly KryptonAutoHiddenSlidePanel _slidePanel;
    private bool _panelEventFired;
    #endregion

    #region Identity
    /// <summary>
    /// Creates the auto-hidden hosting panel and slide-out panel on <paramref name="edge"/> of <paramref name="control"/>.
    /// </summary>
    /// <param name="name">Initial name of the element.</param>
    /// <param name="control">Control whose edge receives auto-hidden groups and the slide-out panel.</param>
    /// <param name="edge">Edge of <paramref name="control"/> where auto-hidden groups are placed.</param>
    /// <exception cref="ArgumentNullException"><paramref name="control"/> is <see langword="null"/>.</exception>
    public KryptonDockingEdgeAutoHidden(string name, Control control, DockingEdge edge)
        : base(name)
    {
        Control = control ?? throw new ArgumentNullException(nameof(control));
        Edge = edge;
        _panelEventFired = false;

        // Create and add the panel used to host auto hidden groups
        _panel = new KryptonAutoHiddenPanel(edge)
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Dock = DockingHelper.DockStyleFromDockEdge(edge, false)
        };
        _panel.Disposed += OnPanelDisposed;

        // Create the panel that slides into/out of view to show selected auto host entry
        _slidePanel = new KryptonAutoHiddenSlidePanel(control, edge, _panel);
        _slidePanel.SplitterMoveRect += OnSlidePanelSeparatorMoveRect;
        _slidePanel.SplitterMoved += OnSlidePanelSeparatorMoved;
        _slidePanel.SplitterMoving += OnSlidePanelSeparatorMoving;
        _slidePanel.PageCloseClicked += OnSlidePanelPageCloseClicked;
        _slidePanel.PageAutoHiddenClicked += OnSlidePanelPageAutoHiddenClicked;
        _slidePanel.PageDropDownClicked += OnSlidePanelPageDropDownClicked;
        _slidePanel.AutoHiddenShowingStateChanged += OnSlidePanelAutoHiddenShowingStateChanged;
        _slidePanel.Disposed += OnSlidePanelDisposed;

        Control.Controls.Add(_panel);
    }
    #endregion

    #region Public
    /// <summary>
    /// Control whose edge hosts the auto-hidden panel and slide-out panel owned by this element.
    /// </summary>
    public Control Control { get; }

    /// <summary>
    /// Edge of <see cref="Control"/> where auto-hidden groups and the slide-out panel are placed.
    /// </summary>
    public DockingEdge Edge { get; }

    /// <summary>
    /// Creates an auto-hidden group with a generated unique name at the outer end of this edge collection.
    /// </summary>
    /// <returns>The new <see cref="KryptonDockingAutoHiddenGroup"/> element, already added to this collection and hosting panel.</returns>
    public KryptonDockingAutoHiddenGroup AppendAutoHiddenGroup() =>
        // Generate a unique string by creating a GUID
        AppendAutoHiddenGroup(CommonHelper.UniqueString);

    /// <summary>
    /// Creates an auto-hidden group with the supplied name at the outer end of this edge collection.
    /// </summary>
    /// <param name="name">Initial name of the group element.</param>
    /// <returns>The new <see cref="KryptonDockingAutoHiddenGroup"/> element, already added to this collection and hosting panel.</returns>
    public KryptonDockingAutoHiddenGroup AppendAutoHiddenGroup(string name) => CreateAndInsertAutoHiddenGroup(Count, name);

    /// <summary>
    /// Creates an auto-hidden group with a generated unique name at the specified index.
    /// </summary>
    /// <param name="index">Zero-based insertion index within this edge collection.</param>
    /// <returns>The new <see cref="KryptonDockingAutoHiddenGroup"/> element, already added to this collection and hosting panel.</returns>
    public KryptonDockingAutoHiddenGroup InsertAutoHiddenGroup(int index) =>
        // Generate a unique string by creating a GUID
        CreateAndInsertAutoHiddenGroup(index, CommonHelper.UniqueString);

    /// <summary>
    /// Creates an auto-hidden group with the supplied name at the specified index.
    /// </summary>
    /// <param name="index">Zero-based insertion index within this edge collection.</param>
    /// <param name="name">Initial name of the group element.</param>
    /// <returns>The new <see cref="KryptonDockingAutoHiddenGroup"/> element, already added to this collection and hosting panel.</returns>
    public KryptonDockingAutoHiddenGroup InsertAutoHiddenGroup(int index, string name) => CreateAndInsertAutoHiddenGroup(index, name);

    /// <summary>
    /// Forwards <paramref name="action"/> to child elements after updating the slide-out panel for page removal, bulk removal, or string changes.
    /// </summary>
    /// <param name="action">Docking operation to forward.</param>
    /// <param name="uniqueNames">Page unique names targeted by the action; <see langword="null"/> for actions that apply to all pages.</param>
    public override void PropogateAction(DockingPropogateAction action, string[]? uniqueNames)
    {
        switch (action)
        {
            case DockingPropogateAction.HidePages:
            case DockingPropogateAction.RemovePages:
            case DockingPropogateAction.RemoveAndDisposePages:
            case DockingPropogateAction.StorePages:
                // Ask the sliding panel to remove its display if an incoming name matches
                if (uniqueNames != null)
                {
                    foreach (var uniqueName in uniqueNames)
                    {
                        _slidePanel.HideUniqueName(uniqueName);
                    }
                }

                break;
            case DockingPropogateAction.Loading:
            case DockingPropogateAction.HideAllPages:
            case DockingPropogateAction.RemoveAllPages:
            case DockingPropogateAction.RemoveAndDisposeAllPages:
            case DockingPropogateAction.StoreAllPages:
                // Remove any slide out page
                _slidePanel.HideUniqueName();
                break;
            case DockingPropogateAction.StringChanged:
            {
                // Pushed changed strings to the tooltips
                KryptonDockingManager? dockingManager = DockingManager;
                if (dockingManager?.Strings != null)
                {
                    _slidePanel.DockspaceControl.PinTooltip = dockingManager.Strings.TextDock;
                    _slidePanel.DockspaceControl.CloseTooltip = dockingManager.Strings.TextClose;
                    _slidePanel.DockspaceControl.DropDownTooltip = dockingManager.Strings.TextWindowLocation;
                }
            }
                break;
        }

        // Let base class perform standard processing
        base.PropogateAction(action, uniqueNames);
    }

    /// <summary>
    /// Forwards <paramref name="action"/> to child elements after hiding the slide-out panel when restoring named pages.
    /// </summary>
    /// <param name="action">Docking operation to forward.</param>
    /// <param name="pages">Pages targeted by the action.</param>
    public override void PropogateAction(DockingPropogateAction action, KryptonPage[] pages)
    {
        switch (action)
        {
            case DockingPropogateAction.RestorePages:
                // Ask the sliding panel to remove its display if an incoming name matches
                foreach (KryptonPage page in pages)
                {
                    _slidePanel.HideUniqueName(page.UniqueName);
                }

                break;
        }

        // Let base class perform standard processing
        base.PropogateAction(action, pages);
    }

    /// <summary>
    /// Short-circuits hierarchy search by returning this element as the auto-hidden edge host.
    /// </summary>
    /// <param name="uniqueName">Unique name used when locating an auto-hidden edge host; not evaluated by this override.</param>
    /// <returns>This instance.</returns>
    public override KryptonDockingEdgeAutoHidden? FindDockingEdgeAutoHidden(string uniqueName) => this;

    /// <summary>
    /// Slides the slide-out panel into view for the page identified by <paramref name="page"/>.
    /// </summary>
    /// <param name="page">Page to display in the slide-out panel.</param>
    /// <param name="select">When <see langword="true"/>, selects the page after sliding out; otherwise shows without changing selection.</param>
    public void SlidePageOut(KryptonPage page, bool select) => SlidePageOut(page.UniqueName, select);

    /// <summary>
    /// Slides the slide-out panel into view for the page with the supplied unique name.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page to display in the slide-out panel.</param>
    /// <param name="select">When <see langword="true"/>, selects the page after sliding out; otherwise shows without changing selection.</param>
    public void SlidePageOut(string uniqueName, bool select)
    {
        // Search each of our AutoHiddenGroup entries
        for (var i = 0; i < Count; i++)
        {
            if (this[i] is KryptonDockingAutoHiddenGroup ahg)
            {
                // If the target page is inside this group
                if (ahg.AutoHiddenGroupControl.Pages[uniqueName] is KryptonAutoHiddenProxyPage proxyPage)
                {
                    // Request the sliding panel slide itself into view with the provided page
                    _slidePanel.SlideOut(proxyPage.Page, ahg.AutoHiddenGroupControl, select);
                    break;
                }
            }
        }
    }
    #endregion

    #region Protected
    /// <summary>
    /// Gets the xml element name to use when saving.
    /// </summary>
    protected override string XmlElementName => @"DEAH";

    /// <summary>
    /// Perform docking element specific actions for loading a child xml.
    /// </summary>
    /// <param name="xmlReader">Xml reader object.</param>
    /// <param name="pages">Collection of available pages.</param>
    /// <param name="child">Optional reference to existing child docking element.</param>
    protected override void LoadChildDockingElement(XmlReader xmlReader,
        KryptonPageCollection pages,
        IDockingElement? child)
    {
        if (child != null)
        {
            child.LoadElementFromXml(xmlReader, pages);
        }
        else
        {
            // Create a new auto hidden group and then reload it
            KryptonDockingAutoHiddenGroup autoHiddenGroup = AppendAutoHiddenGroup(xmlReader.GetAttribute(@"N")!);
            autoHiddenGroup.LoadElementFromXml(xmlReader, pages);
        }
    }
    #endregion

    #region Implementation
    private KryptonDockingAutoHiddenGroup CreateAndInsertAutoHiddenGroup(int index, string name)
    {
        // Create the new auto hidden group instance and add into our collection
        var groupElement = new KryptonDockingAutoHiddenGroup(name, Edge);
        groupElement.PageClicked += OnDockingAutoHiddenGroupClicked;
        groupElement.PageHoverStart += OnDockingAutoHiddenGroupHoverStart;
        groupElement.PageHoverEnd += OnDockingAutoHiddenGroupHoverEnd;
        groupElement.Disposed += OnDockingAutoHiddenGroupDisposed;
        InternalInsert(index, groupElement);

        // Events are generated from the parent docking manager
        KryptonDockingManager? dockingManager = DockingManager;
        if (dockingManager != null)
        {
            // The hosting panel/sliding panel dockspace/separator are not shown until the first group is added, so only 
            // generate the events for allowing customization of the when there is a chance they will become displayed.
            if (!_panelEventFired)
            {
                var panelArgs = new AutoHiddenGroupPanelEventArgs(_panel, this);
                var dockspaceArgs = new DockspaceEventArgs(_slidePanel.DockspaceControl, null);
                var separatorArgs = new DockspaceSeparatorEventArgs(_slidePanel.SeparatorControl, null);
                dockingManager.RaiseAutoHiddenGroupPanelAdding(panelArgs);
                dockingManager.RaiseDockspaceAdding(dockspaceArgs);
                dockingManager.RaiseDockspaceSeparatorAdding(separatorArgs);
                _panelEventFired = true;
            }

            // Allow the auto hidden group to be customized by event handlers
            var groupArgs = new AutoHiddenGroupEventArgs(groupElement.AutoHiddenGroupControl, groupElement);
            dockingManager.RaiseAutoHiddenGroupAdding(groupArgs);
        }

        // Append to the child collection of groups
        _panel.Controls.Add(groupElement.AutoHiddenGroupControl);
        _panel.Controls.SetChildIndex(groupElement.AutoHiddenGroupControl, (_panel.Controls.Count - 1) - index);

        // When adding the first group the panel will not be visible and so we need to 
        // force the calculation of a new size so it makes itself visible.
        _panel.PerformLayout();

        return groupElement;
    }

    private void OnDockingAutoHiddenGroupDisposed(object? sender, EventArgs e)
    {
        // Cast to correct type and unhook event handlers so garbage collection can occur
        var groupElement = sender as KryptonDockingAutoHiddenGroup ?? throw new ArgumentNullException(nameof(sender));
        groupElement.PageClicked -= OnDockingAutoHiddenGroupClicked;
        groupElement.PageHoverStart -= OnDockingAutoHiddenGroupHoverStart;
        groupElement.PageHoverEnd -= OnDockingAutoHiddenGroupHoverEnd;
        groupElement.Disposed -= OnDockingAutoHiddenGroupDisposed;

        // Remove the element from our child collection as it is no longer valid
        InternalRemove(groupElement);
    }

    private void OnPanelDisposed(object? sender, EventArgs e)
    {
        // Unhook from events so the control can be garbage collected
        _panel.Disposed -= OnPanelDisposed;

        // Events are generated from the parent docking manager
        KryptonDockingManager? dockingManager = DockingManager;
        if (dockingManager != null)
        {
            // Only generate the removed event if we have already fired the adding event.
            if (_panelEventFired)
            {
                var panelArgs = new AutoHiddenGroupPanelEventArgs(_panel, this);
                dockingManager.RaiseAutoHiddenGroupPanelRemoved(panelArgs);
            }
        }

        // Make sure the sliding panel is also disposed
        if (!_slidePanel.IsDisposed)
        {
            _slidePanel.Dispose();
        }
    }

    private void OnSlidePanelDisposed(object? sender, EventArgs e)
    {
        // Unhook from events so the control can be garbage collected
        _slidePanel.SplitterMoveRect -= OnSlidePanelSeparatorMoveRect;
        _slidePanel.SplitterMoved -= OnSlidePanelSeparatorMoved;
        _slidePanel.SplitterMoving -= OnSlidePanelSeparatorMoving;
        _slidePanel.PageCloseClicked -= OnSlidePanelPageCloseClicked;
        _slidePanel.PageAutoHiddenClicked -= OnSlidePanelPageAutoHiddenClicked;
        _slidePanel.PageDropDownClicked -= OnSlidePanelPageDropDownClicked;
        _slidePanel.Disposed -= OnPanelDisposed;

        // Events are generated from the parent docking manager
        KryptonDockingManager? dockingManager = DockingManager;
        if (dockingManager != null)
        {
            // Only generate the removed event if we have already fired the adding event.
            if (_panelEventFired)
            {
                var dockspaceArgs = new DockspaceEventArgs(_slidePanel.DockspaceControl, null);
                var separatorArgs = new DockspaceSeparatorEventArgs(_slidePanel.SeparatorControl, null);
                dockingManager.RaiseDockspaceRemoved(dockspaceArgs);
                dockingManager.RaiseDockspaceSeparatorRemoved(separatorArgs);
            }
        }

        // Make sure the groups panel is also disposed
        if (!_panel.IsDisposed)
        {
            _panel.Dispose();
        }
    }

    private void OnDockingAutoHiddenGroupClicked(object? sender, KryptonPageEventArgs e)
    {
        // Request the sliding panel slide itself into view with the provided page
        var dockingGroup = sender as KryptonDockingAutoHiddenGroup ?? throw new ArgumentNullException(nameof(sender));
        _slidePanel.SlideOut(e.Item, dockingGroup.AutoHiddenGroupControl, true);
    }

    private void OnDockingAutoHiddenGroupHoverStart(object? sender, KryptonPageEventArgs e)
    {
        // Request the sliding panel slide itself into view with the provided page
        var dockingGroup = sender as KryptonDockingAutoHiddenGroup ?? throw new ArgumentNullException(nameof(sender));
        _slidePanel.SlideOut(e.Item, dockingGroup.AutoHiddenGroupControl, false);
    }

    private void OnDockingAutoHiddenGroupHoverEnd(object? sender, EventArgs e) =>
        // Request the sliding panel slide itself out of view when appropriate
        // (will not retract whilst the mouse is over the slide out dockspace)
        // (will not retract whilst slide out dockspace has the focus)
        _slidePanel.SlideIn();

    private void OnSlidePanelSeparatorMoved(object? sender, SplitterEventArgs e) => _slidePanel.UpdateSize(e.SplitX, e.SplitY);

    private void OnSlidePanelSeparatorMoving(object? sender, SplitterCancelEventArgs e)
    {
    }

    private void OnSlidePanelSeparatorMoveRect(object? sender, SplitterMoveRectMenuArgs e)
    {
        // Cast to correct type and grab associated dockspace control
        var separatorControl = sender as KryptonDockspaceSeparator ?? throw new ArgumentNullException(nameof(sender));
        KryptonDockspace dockspaceControl = _slidePanel.DockspaceControl;
        KryptonPage? page = _slidePanel.Page;

        // Events are generated from the parent docking manager
        KryptonDockingManager? dockingManager = DockingManager;
        if (dockingManager != null)
        {
            // Allow the movement rectangle to be modified by event handlers
            var autoHiddenSeparatorResizeRectArgs = new AutoHiddenSeparatorResizeEventArgs(separatorControl, dockspaceControl, page,
                FindMovementRect(e.MoveRect));
            dockingManager.RaiseAutoHiddenSeparatorResize(autoHiddenSeparatorResizeRectArgs);
            e.MoveRect = autoHiddenSeparatorResizeRectArgs.ResizeRect;
        }
    }

    private void OnSlidePanelPageCloseClicked(object? sender, UniqueNameEventArgs e)
    {
        // Generate event so that the close action is handled for the named page
        KryptonDockingManager? dockingManager = DockingManager;
        dockingManager?.CloseRequest(new[] { e.UniqueName });
    }

    private void OnSlidePanelPageAutoHiddenClicked(object? sender, UniqueNameEventArgs e)
    {
        // Generate event so that the auto hidden is switched to docked is handled for the group that contains the named page
        KryptonDockingManager? dockingManager = DockingManager;
        dockingManager?.SwitchAutoHiddenGroupToDockedCellRequest(e.UniqueName);
    }

    private void OnSlidePanelPageDropDownClicked(object? sender, CancelDropDownEventArgs e)
    {
        // Generate event so that the appropriate context menu options are presented and actioned
        KryptonDockingManager? dockingManager = DockingManager;
        if (dockingManager != null
            && e is { Page: not null, KryptonContextMenu: not null }
           )
        {
            e.Cancel = !dockingManager.ShowPageContextMenuRequest(e.Page, e.KryptonContextMenu);
        }
    }

    private void OnSlidePanelAutoHiddenShowingStateChanged(object? sender, AutoHiddenShowingStateEventArgs e)
    {
        // Generate event so that the appropriate context menu options are presented and actioned
        KryptonDockingManager? dockingManager = DockingManager;
        dockingManager?.RaiseAutoHiddenShowingStateChanged(e);
    }

    private Rectangle FindMovementRect(Rectangle moveRect)
    {
        // We never allow the entire client area to covered, so reduce by a fixed size
        Size innerSize = Control.ClientRectangle.Size;
        innerSize.Width -= CLIENT_MINIMUM;
        innerSize.Height -= CLIENT_MINIMUM;

        // Adjust for any showing auto hidden panels at the edges
        foreach (Control child in Control.Controls.Cast<Control>()
                     .Where(static child => child.Visible
                                            && child is KryptonAutoHiddenPanel)
                )
        {
            switch (child.Dock)
            {
                case DockStyle.Left:
                case DockStyle.Right:
                    innerSize.Width -= child.Width;
                    break;
                case DockStyle.Top:
                case DockStyle.Bottom:
                    innerSize.Height -= child.Height;
                    break;
            }
        }

        // How much can we reduce the width/height of the dockspace to reach the minimum
        Size dockspaceMinimum = _slidePanel.DockspaceControl.MinimumSize;
        var reduceWidth = Math.Max(_slidePanel.DockspaceControl.Width - dockspaceMinimum.Width, 0);
        var reduceHeight = Math.Max(_slidePanel.DockspaceControl.Height - dockspaceMinimum.Height, 0);

        // How much can we expand the width/height of the dockspace to reach the inner minimum
        var expandWidth = Math.Max(innerSize.Width - _slidePanel.Width, 0);
        var expandHeight = Math.Max(innerSize.Height - _slidePanel.Height, 0);

        // Create movement rectangle based on the initial rectangle and the allowed range
        var retRect = Edge switch
        {
            DockingEdge.Left => new Rectangle(moveRect.X - reduceWidth, moveRect.Y,
                moveRect.Width + reduceWidth + expandWidth, moveRect.Height),
            DockingEdge.Right => new Rectangle(moveRect.X - expandWidth, moveRect.Y,
                moveRect.Width + reduceWidth + expandWidth, moveRect.Height),
            DockingEdge.Top => new Rectangle(moveRect.X, moveRect.Y - reduceHeight, moveRect.Width,
                moveRect.Height + reduceHeight + expandHeight),
            DockingEdge.Bottom => new Rectangle(moveRect.X, moveRect.Y - expandHeight, moveRect.Width,
                moveRect.Height + reduceHeight + expandHeight),
            _ => Rectangle.Empty
        };

        // We do not allow negative width/height
        retRect.Width = Math.Max(retRect.Width, 0);
        retRect.Height = Math.Max(retRect.Height, 0);

        return retRect;
    }
    #endregion
}
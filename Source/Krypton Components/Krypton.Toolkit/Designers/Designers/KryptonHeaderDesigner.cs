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

internal class KryptonHeaderDesigner : ControlDesigner
{
    #region Instance Fields
    private bool _lastHitTest;
    private KryptonHeader? _header;
    private IDesignerHost? _designerHost;
    private IComponentChangeService? _changeService;
    private ISelectionService? _selectionService;
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Releases all resources used by the component. 
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        // Unhook from events
        var vm = _header?.GetViewManager();
        if (vm != null)
        {
            vm.MouseUpProcessed -= OnHeaderMouseUp;
            vm.DoubleClickProcessed -= OnHeaderDoubleClick;
        }

        if (_changeService != null)
        {
            _changeService.ComponentRemoving -= OnComponentRemoving;
        }

        // Must let base class do standard stuff
        base.Dispose(disposing);
    }
    #endregion

    #region Public Overrides
    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    /// <param name="component">The IComponent to associate the designer with.</param>
    public override void Initialize([DisallowNull] IComponent component)
    {
        // Let base class do standard stuff
        base.Initialize(component);

        Debug.Assert(component != null);

        // The resizing handles around the control need to change depending on the
        // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
        // do not get the resizing handles, otherwise you do.
        AutoResizeHandles = true;

        // Cast to correct type
        _header = component as KryptonHeader;

        if (_header != null)
        {
            // Hook into header event
            _header.GetViewManager()!.MouseUpProcessed += OnHeaderMouseUp;
            _header.GetViewManager()!.DoubleClickProcessed += OnHeaderDoubleClick;
        }

        // Get access to the design services
        _designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
        _changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
        _selectionService = GetService(typeof(ISelectionService)) as ISelectionService;

        // We need to know when we are being removed
        _changeService!.ComponentRemoving += OnComponentRemoving;
    }

    /// <summary>
    /// Gets the collection of components associated with the component managed by the designer.
    /// </summary>
    public override ICollection AssociatedComponents =>
        _header?.ButtonSpecs ?? base.AssociatedComponents;

    /// <summary>
    ///  Gets the design-time action lists supported by the component associated with the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            // Create a collection of action lists
            var actionLists = new DesignerActionListCollection
            {
                // Add the header specific list
                new KryptonHeaderActionList(this)
            };

            return actionLists;
        }
    }
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Indicates whether a mouse click at the specified point should be handled by the control.
    /// </summary>
    /// <param name="point">A Point indicating the position at which the mouse was clicked, in screen coordinates.</param>
    /// <returns>true if a click at the specified point is to be handled by the control; otherwise, false.</returns>
    protected override bool GetHitTest(Point point)
    {
        if (_header != null)
        {
            // Ask the control if it wants to process the point
            var ret = _header.DesignerGetHitTest(_header.PointToClient(point));

            // If the navigator does not want the mouse point then make sure the 
            // tracking element is informed that the mouse has left the control
            if (!ret && _lastHitTest)
            {
                _header.DesignerMouseLeave();
            }

            // Cache the last answer recovered
            _lastHitTest = ret;

            return ret;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Receives a call when the mouse leaves the control. 
    /// </summary>
    protected override void OnMouseLeave()
    {
        _header?.DesignerMouseLeave();

        base.OnMouseLeave();
    }
    #endregion

    #region Implementation
    private void OnHeaderMouseUp(object? sender, MouseEventArgs e)
    {
        if ((_header != null) && (e.Button == MouseButtons.Left))
        {
            // Get any component associated with the current mouse position
            var component = _header.DesignerComponentFromPoint(new Point(e.X, e.Y));

            if (component != null)
            {
                // Force the layout to be update for any change in selection
                _header.PerformLayout();

                // Select the component
                var selectionList = new ArrayList
                {
                    component
                };
                _selectionService?.SetSelectedComponents(selectionList, SelectionTypes.Auto);
            }
        }
    }

    private void OnHeaderDoubleClick(object sender, Point pt)
    {
        // Get any component associated with the current mouse position
        var component = _header?.DesignerComponentFromPoint(pt);

        if (component != null)
        {
            // Get the designer for the component
            var designer = _designerHost?.GetDesigner(component);

            // Request code for the default event be generated
            designer?.DoDefaultAction();
        }
    }

    private void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        // If our control is being removed
        if ((_header != null) && (Equals(e.Component, _header)))
        {
            // Need access to host in order to delete a component
            var host = GetService(typeof(IDesignerHost)) as IDesignerHost;

            // We need to remove all the button spec instances
            for (var i = _header.ButtonSpecs.Count - 1; i >= 0; i--)
            {
                // Get access to the indexed button spec
                ButtonSpec spec = _header.ButtonSpecs[i];

                // Must wrap button spec removal in change notifications
                _changeService?.OnComponentChanging(_header, null);

                // Perform actual removal of button spec from header
                _header.ButtonSpecs.Remove(spec);

                // Get host to remove it from design time
                host?.DestroyComponent(spec);

                // Must wrap button spec removal in change notifications
                _changeService?.OnComponentChanged(_header, null, null, null);
            }
        }
    }
    #endregion
}
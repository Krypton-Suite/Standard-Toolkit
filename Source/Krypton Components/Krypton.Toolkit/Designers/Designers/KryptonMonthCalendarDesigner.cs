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

internal class KryptonMonthCalendarDesigner : ControlDesigner
{
    #region Instance Fields
    private bool _lastHitTest;
    private KryptonMonthCalendar? _monthCalendar;
    private IDesignerHost? _designerHost;
    private IComponentChangeService? _changeService;
    private ISelectionService? _selectionService;
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
        _monthCalendar = component as KryptonMonthCalendar;

        if (_monthCalendar != null)
        {
            // Hook into header event
            _monthCalendar.GetViewManager()!.MouseUpProcessed += OnCalendarMouseUp;
            _monthCalendar.GetViewManager()!.DoubleClickProcessed += OnCalendarDoubleClick;
        }

        // Acquire service interfaces
        _designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
        _changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
        _selectionService = GetService(typeof(ISelectionService)) as ISelectionService;

        // We need to know when we are being removed
        _changeService!.ComponentRemoving += OnComponentRemoving;

    }

    /// <summary>
    /// Gets the collection of components associated with the component managed by the designer.
    /// </summary>
    public override ICollection AssociatedComponents
    {
        get
        {
            // Get the set of components from the base class
            var baseComponents = base.AssociatedComponents;

            // If no button specs then nothing more to do
            if ((_monthCalendar == null) || (_monthCalendar.ButtonSpecs.Count == 0))
            {
                return baseComponents;
            }
            else
            {
                // Create a new collection for both values
                var compound = new ArrayList(baseComponents);

                // Add all the button specs to the end
                compound.AddRange(_monthCalendar.ButtonSpecs);

                return compound;
            }
        }
    }

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
                // Add the bread crumb specific list
                new KryptonMonthCalendarActionList(this)
            };

            return actionLists;
        }
    }

    /// <summary>
    /// Defines identifiers that are used to indicate selection rules for a component.
    /// </summary>
    public override SelectionRules SelectionRules
    {
        get
        {
            var rules = base.SelectionRules;
            return rules & ~(SelectionRules.LeftSizeable | SelectionRules.TopSizeable);
        }
    }
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Releases all resources used by the component. 
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (_monthCalendar != null)
        {
            // Unhook from events
            _monthCalendar.GetViewManager()!.MouseUpProcessed -= OnCalendarMouseUp;
            _monthCalendar.GetViewManager()!.DoubleClickProcessed -= OnCalendarDoubleClick;
        }

        _changeService!.ComponentRemoving -= OnComponentRemoving;

        // Must let base class do standard stuff
        base.Dispose(disposing);
    }

    /// <summary>
    /// Indicates whether a mouse click at the specified point should be handled by the control.
    /// </summary>
    /// <param name="point">A Point indicating the position at which the mouse was clicked, in screen coordinates.</param>
    /// <returns>true if a click at the specified point is to be handled by the control; otherwise, false.</returns>
    protected override bool GetHitTest(Point point)
    {
        if (_monthCalendar != null)
        {
            // Ask the control if it wants to process the point
            var ret = _monthCalendar.DesignerGetHitTest(_monthCalendar.PointToClient(point));

            // If the navigator does not want the mouse point then make sure the 
            // tracking element is informed that the mouse has left the control
            if (!ret && _lastHitTest)
            {
                _monthCalendar.DesignerMouseLeave();
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
        _monthCalendar?.DesignerMouseLeave();

        base.OnMouseLeave();
    }
    #endregion

    #region Implementation
    private void OnCalendarMouseUp(object? sender, MouseEventArgs e)
    {
        if ((_monthCalendar != null) && (e.Button == MouseButtons.Left))
        {
            // Get any component associated with the current mouse position
            var component = _monthCalendar.DesignerComponentFromPoint(new Point(e.X, e.Y));

            if (component != null)
            {
                // Force the layout to be updated for any change in selection
                _monthCalendar.PerformLayout();

                // Select the component
                var selectionList = new ArrayList
                {
                    component
                };
                _selectionService?.SetSelectedComponents(selectionList, SelectionTypes.Auto);
            }
        }
    }

    private void OnCalendarDoubleClick(object sender, Point pt)
    {
        // Get any component associated with the current mouse position
        var component = _monthCalendar?.DesignerComponentFromPoint(pt);

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
        if (Equals(e.Component, _monthCalendar))
        {
            // Need access to host in order to delete a component
            var host = GetService(typeof(IDesignerHost)) as IDesignerHost;

            // We need to remove all the button spec instances
            for (var i = _monthCalendar!.ButtonSpecs.Count - 1; i >= 0; i--)
            {
                // Get access to the indexed button spec
                ButtonSpec spec = _monthCalendar.ButtonSpecs[i];

                // Must wrap button spec removal in change notifications
                _changeService?.OnComponentChanging(_monthCalendar, null);

                // Perform actual removal of button spec from calendar
                _monthCalendar.ButtonSpecs.Remove(spec);

                // Get host to remove it from design time
                host?.DestroyComponent(spec);

                // Must wrap button spec removal in change notifications
                _changeService?.OnComponentChanged(_monthCalendar, null, null, null);
            }
        }
    }
    #endregion
}
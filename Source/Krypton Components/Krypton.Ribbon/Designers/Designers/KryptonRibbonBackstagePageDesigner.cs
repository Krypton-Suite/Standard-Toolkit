#region BSD License
/*
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Designer for the KryptonRibbonBackstagePage class.
/// </summary>
internal class KryptonRibbonBackstagePageDesigner : ComponentDesigner
{
    #region Instance Fields
    private IDesignerHost? _designerHost;
    private IComponentChangeService? _changeService;
    private KryptonRibbonBackstagePage? _backstagePage;
    #endregion

    #region Public Overrides
    /// <summary>
    /// Initialize the designer with the specified component.
    /// </summary>
    /// <param name="component">The IComponent to associate the designer with.</param>
    public override void Initialize(IComponent component)
    {
        // Let base class do standard stuff
        base.Initialize(component);

        Debug.Assert(component != null);

        // Cast to correct type
        _backstagePage = component as KryptonRibbonBackstagePage;

        if (_backstagePage != null)
        {
            // Get access to the services
            _designerHost = (IDesignerHost?)GetService(typeof(IDesignerHost));
            _changeService = (IComponentChangeService?)GetService(typeof(IComponentChangeService));

            // We need to know when we are being removed/changed
            _changeService?.ComponentChanged += OnComponentChanged;
        }
    }

    /// <summary>
    /// Gets the design-time action lists supported by the component associated with the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            // Create a collection of action lists
            var actionLists = new DesignerActionListCollection
            {
                // Add the backstage page specific list
                new KryptonRibbonBackstagePageActionList(this)
            };

            return actionLists;
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
        try
        {
            if (disposing)
            {
                // Unhook from events
                if (_changeService != null)
                {
                    _changeService.ComponentChanged -= OnComponentChanged;
                }
            }
        }
        finally
        {
            // Must let base class do standard stuff
            base.Dispose(disposing);
        }
    }
    #endregion

    #region Implementation
    private void OnComponentChanged(object? sender, ComponentChangedEventArgs e)
    {
        // If our backstage page is being changed then update immediately
        if (e.Component == _backstagePage)
        {
            // Update the display
        }
    }
    #endregion
}

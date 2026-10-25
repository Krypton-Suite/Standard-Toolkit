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

internal class KryptonGalleryDesigner : ParentControlDesigner
{
    #region Instance Fields
    private KryptonGallery? _gallery;
    private IComponentChangeService _changeService;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonGalleryDesigner class.
    /// </summary>
    public KryptonGalleryDesigner() =>
        // The resizing handles around the control need to change depending on the
        // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
        // do not get the resizing handles, otherwise you do.
        AutoResizeHandles = true;

    #endregion

    #region Public
    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    /// <param name="component">The IComponent to associate the designer with.</param>
    public override void Initialize([DisallowNull] IComponent component)
    {
        // Let base class do standard stuff
        base.Initialize(component);

        Debug.Assert(component != null);
        // Cast to correct type
        _gallery = component as KryptonGallery;

        // We need to know when we are being removed
        _changeService = (IComponentChangeService?)GetService(typeof(IComponentChangeService)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(_changeService)));
        _changeService.ComponentRemoving += OnComponentRemoving;
    }

    /// <summary>
    /// Gets the collection of components associated with the component managed by the designer.
    /// </summary>
    public override ICollection AssociatedComponents
    {
        get
        {
            // Create a new collection for both values
            var compound = new ArrayList(base.AssociatedComponents);

            // Add all the display ranges
            foreach (KryptonGalleryRange dropRange in _gallery?.DropButtonRanges!)
            {
                compound.Add(dropRange);
            }

            return compound;
        }
    }

    /// <summary>
    /// Indicates whether the specified control can be a child of the control managed by a designer.
    /// </summary>
    /// <param name="control">The Control to test.</param>
    /// <returns>true if the specified control can be a child of the control managed by this designer; otherwise, false.</returns>
    public override bool CanParent(Control control) =>
        // We never allow anything to be added to the ribbon
        false;

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
                // Add the gallery specific list
                new KryptonGalleryActionList(this)
            };

            return actionLists;
        }
    }
    #endregion

    #region Protected
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
                _changeService.ComponentRemoving -= OnComponentRemoving;
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
    private void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        // If our control is being removed
        if (e.Component == _gallery)
        {
            // Need access to host in order to delete a component
            var host = (IDesignerHost?)GetService(typeof(IDesignerHost)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("host"));

            // We need to remove all the range instances
            for (var i = _gallery!.DropButtonRanges.Count - 1; i >= 0; i--)
            {
                KryptonGalleryRange dropRange = _gallery.DropButtonRanges[i];
                _gallery.DropButtonRanges.Remove(dropRange);
                host.DestroyComponent(dropRange);
            }
        }
    }
    #endregion
}
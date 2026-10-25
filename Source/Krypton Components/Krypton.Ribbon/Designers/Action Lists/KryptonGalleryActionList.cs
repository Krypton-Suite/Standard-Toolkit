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

internal class KryptonGalleryActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonGallery _gallery;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonGalleryActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonGalleryActionList(KryptonGalleryDesigner owner)
        : base(owner.Component)
    {
        // Remember the gallery instance
        _gallery = (owner.Component as KryptonGallery)!;

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _gallery.PaletteMode;

        set
        {
            if (_gallery.PaletteMode != value)
            {
                _service?.OnComponentChanged(_gallery, null, _gallery.PaletteMode, value);
                _gallery.PaletteMode = value;
            }
        }
    }
    #endregion

    #region Public Override
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        // Create a new collection for holding the single item we want to create
        var actions = new DesignerActionItemCollection();

        // This can be null when deleting a control instance at design time
        if (_gallery != null)
        {
            // Add the list of button specific actions
            actions.Add(new DesignerActionHeaderItem("Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), "Palette", "Visuals", "Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}
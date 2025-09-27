#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Docking;

/// <summary>
/// Action list for KryptonDockingManager using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonDockingManagerExtensibilityActionList : KryptonDockingExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonDockingManager _dockingManager;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDockingManagerExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list.</param>
    public KryptonDockingManagerExtensibilityActionList(KryptonDockingManagerExtensibilityDesigner owner)
        : base(owner)
    {
        _dockingManager = (owner.Component as KryptonDockingManager)!;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the strings for the docking manager.
    /// </summary>
    [Category("Appearance")]
    [Description("Strings for the docking manager.")]
    [DefaultValue(null)]
    public DockingManagerStrings? Strings
    {
        get => _dockingManager.Strings;
        set => SetPropertyValue(nameof(Strings), value ?? new DockingManagerStrings(_dockingManager));
    }

    #endregion

    #region Public Overrides
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var items = new DesignerActionItemCollection();

        // Add the action items
        items.Add(new DesignerActionHeaderItem("Appearance"));
        items.Add(new DesignerActionPropertyItem(nameof(Strings), "Strings", "Appearance", "Strings for the docking manager."));

        return items;
    }
    #endregion
}

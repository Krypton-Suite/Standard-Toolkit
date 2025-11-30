#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Action list for the KryptonContextMenu control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonContextMenuExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonContextMenu? _contextMenu;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonContextMenuExtensibilityActionList(ComponentDesigner owner)
        : base(owner)
    {
        _contextMenu = (KryptonContextMenu?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets whether the context menu is enabled.
    /// </summary>
    public bool Enabled
    {
        get => _contextMenu?.Enabled ?? true;
        set => SetPropertyValue(_contextMenu!, nameof(Enabled), Enabled, value, v => _contextMenu!.Enabled = v);
    }
    #endregion

    #region Public Override
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();

        // This can be null when deleting a control instance at design time
        if (_contextMenu != null)
        {
            // Add the list of ContextMenu specific actions
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(Enabled), @"Enabled", @"Behavior", @"Context menu enabled"));
        }

        return actions;
    }
    #endregion
}

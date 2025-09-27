#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Workspace;

/// <summary>
/// Action list for KryptonWorkspace using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonWorkspaceExtensibilityActionList : KryptonWorkspaceExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonWorkspace _workspace;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonWorkspaceExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list.</param>
    public KryptonWorkspaceExtensibilityActionList(KryptonWorkspaceExtensibilityDesigner owner)
        : base(owner)
    {
        _workspace = (owner.Component as KryptonWorkspace)!;
    }
    #endregion

    #region Public Properties
    #endregion

    #region Public Overrides
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var items = new DesignerActionItemCollection();

        // No properties to expose for KryptonWorkspace

        return items;
    }
    #endregion
}

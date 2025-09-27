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
/// Designer for KryptonWorkspace using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonWorkspaceExtensibilityDesigner : KryptonWorkspaceExtensibilityDesignerBase
{
    #region Public Overrides
    /// <summary>
    /// Gets the action lists for the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            var actionLists = new DesignerActionListCollection
            {
                new KryptonWorkspaceExtensibilityActionList(this)
            };
            return actionLists;
        }
    }
    #endregion
}

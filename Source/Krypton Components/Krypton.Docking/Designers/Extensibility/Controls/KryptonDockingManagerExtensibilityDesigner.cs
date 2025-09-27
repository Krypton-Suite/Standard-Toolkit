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
/// Designer for KryptonDockingManager using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonDockingManagerExtensibilityDesigner : KryptonDockingExtensibilityDesignerBase
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
                new KryptonDockingManagerExtensibilityActionList(this)
            };
            return actionLists;
        }
    }
    #endregion
}

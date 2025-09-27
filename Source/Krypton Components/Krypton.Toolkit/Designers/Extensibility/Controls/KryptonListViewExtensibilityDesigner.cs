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
/// Designer for the KryptonListView control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonListViewExtensibilityDesigner : KryptonExtensibilityDesignerBase
{
    #region Protected Overrides

    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    /// <param name="component">The IComponent to associate with the designer.</param>
    protected override void InitializeDesigner(IComponent component)
    {
        Debug.Assert(component != null);
        // Perform any specific initialization for KryptonListView
    }

    /// <summary>
    /// Gets the design-time action lists supported by the component associated with the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            var actionLists = new DesignerActionListCollection
            {
                new KryptonListViewExtensibilityActionList(this)
            };
            return actionLists;
        }
    }

    #endregion
}

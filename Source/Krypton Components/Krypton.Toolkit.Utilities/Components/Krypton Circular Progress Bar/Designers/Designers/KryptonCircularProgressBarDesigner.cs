#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.   
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Designer for <see cref="KryptonCircularProgressBar"/>.
/// </summary>
internal class KryptonCircularProgressBarDesigner : ControlDesigner
{
    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonCircularProgressBarDesigner"/> class.
    /// </summary>
    public KryptonCircularProgressBarDesigner() => AutoResizeHandles = true;

    #endregion

    #region Public Overrides

    /// <summary>
    /// Gets the design-time action lists supported by the component associated with the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            DesignerActionListCollection actions = new DesignerActionListCollection();
            actions.Add(new KryptonCircularProgressBarActionList(this));
            return actions;
        }
    }

    #endregion
}

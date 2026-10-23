#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

internal class KryptonCheckBoxExtendedDesigner : ControlDesigner
{
    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonCheckBoxExtendedDesigner"/> class.
    /// </summary>
    public KryptonCheckBoxExtendedDesigner() => AutoResizeHandles = true;

    #endregion

    #region Public Overrides

    /// <inheritdoc />
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            var actionLists = new DesignerActionListCollection
            {
                new KryptonCheckBoxExtendedActionList(this)
            };

            return actionLists;
        }
    }

    #endregion
}

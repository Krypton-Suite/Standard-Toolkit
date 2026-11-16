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
/// Designer for <see cref="KryptonTagInputControl"/>. Children are owned by the control and are not dropped at design time.
/// </summary>
internal class KryptonTagInputControlDesigner : ControlDesigner
{
    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonTagInputControlDesigner"/> class.
    /// </summary>
    public KryptonTagInputControlDesigner() => AutoResizeHandles = true;

    #endregion

    #region Public Overrides

    /// <inheritdoc />
    public override SelectionRules SelectionRules =>
        SelectionRules.Visible | SelectionRules.Moveable | SelectionRules.AllSizeable;

    /// <inheritdoc />
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            var actions = new DesignerActionListCollection
            {
                new KryptonTagInputControlActionList(this)
            };
            return actions;
        }
    }

    #endregion
}

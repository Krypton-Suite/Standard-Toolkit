#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Designer for <see cref="KryptonEnumButton"/>. Provides the enum-cycling smart-tag action list.
/// </summary>
internal class KryptonEnumButtonDesigner : ControlDesigner
{
    #region Identity
    /// <summary>Initialize a new instance of the <see cref="KryptonEnumButtonDesigner"/> class.</summary>
    public KryptonEnumButtonDesigner()
    {
        // Without AutoSize the control shows the resizing handles.
        AutoResizeHandles = true;
    }
    #endregion

    #region Public Overrides
    /// <summary>Gets the design-time action lists supported by the component associated with the designer.</summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            DesignerActionListCollection actionList = new DesignerActionListCollection();

            actionList.Add(new KryptonEnumButtonActionList(this));

            return actionList;
        }
    }
    #endregion
}

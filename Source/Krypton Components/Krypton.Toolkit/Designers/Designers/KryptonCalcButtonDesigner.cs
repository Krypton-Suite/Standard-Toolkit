#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonCalcInputDesigner : ControlDesigner
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonCalcInputDesigner class.
    /// </summary>
    public KryptonCalcInputDesigner() =>
        // The resizing handles around the control need to change depending on the
        // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
        // do not get the resizing handles, otherwise you do.
        AutoResizeHandles = true;

    #endregion

    #region Public Overrides
    /// <summary>
    ///  Gets the design-time action lists supported by the component associated with the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            // Create a collection of action lists
            var actionLists = new DesignerActionListCollection
            {
                // Add the calculator button specific list
                new KryptonCalcInputActionList(this)
            };

            return actionLists;
        }
    }
    #endregion
}

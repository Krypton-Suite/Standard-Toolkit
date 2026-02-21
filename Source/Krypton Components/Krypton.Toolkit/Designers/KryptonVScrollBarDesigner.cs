#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonVScrollBarDesigner : ControlDesigner
{
    #region Identity
    /// <summary>Initializes a new instance of the <see cref="KryptonVScrollBarDesigner" /> class.</summary>
    public KryptonVScrollBarDesigner() =>
        // The resizing handles around the control need to change depending on the
        // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
        // do not get the resizing handles, otherwise you do.
        AutoResizeHandles = true;

    #endregion

    #region Public Overrides

    public override DesignerActionListCollection ActionLists
    {
        get
        {
            var actionList = new DesignerActionListCollection
            {
                new KryptonVScrollBarActionList(this)
            };

            return actionList;
        }
    }

    #endregion
}
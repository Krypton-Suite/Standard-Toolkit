#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonCommandLinkButtonDesigner : ControlDesigner
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonButtonDesigner class.
    /// </summary>
    public KryptonCommandLinkButtonDesigner()
    {
        // The resizing handles around the control need to change depending on the
        // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
        // do not get the resizing handles, otherwise you do.
        AutoResizeHandles = true;
    }
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

            DesignerActionListCollection actionList = new DesignerActionListCollection();

            actionList.Add(new KryptonCommandLinkButtonActionList(this));

            return actionList;

/*#if NET8_0_OR_GREATER
                DesignerActionListCollection actionList = new DesignerActionListCollection();

                actionList.Add(new KryptonCommandLinkButtonActionList(this));

                return actionList;
#else
                DesignerActionListCollection actionLists = [new KryptonCommandLinkButtonActionList(this)];

                return actionLists;
#endif*/
        }
    }
    #endregion
}
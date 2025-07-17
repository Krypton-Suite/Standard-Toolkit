#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonPropertyGridDesigner : ControlDesigner
{
    #region Identity
    /// <summary>Initializes a new instance of the <see cref="KryptonPropertyGridDesigner" /> class.</summary>
    public KryptonPropertyGridDesigner() => AutoResizeHandles = true;

    #endregion

    #region Public Override
    /// <summary>Gets the design-time action lists supported by the component associated with the designer.</summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            var actionList = new DesignerActionListCollection
            {
                new KryptonPropertyGridActionList(this)
            };

            return actionList;
        }
    }
    #endregion
}
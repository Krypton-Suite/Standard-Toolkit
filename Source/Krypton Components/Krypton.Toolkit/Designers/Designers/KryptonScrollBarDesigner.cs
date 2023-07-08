﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    internal class KryptonScrollBarDesigner : ControlDesigner
    {
        #region Identity
        /// <summary>Initializes a new instance of the <see cref="KryptonScrollBarDesigner" /> class.</summary>
        public KryptonScrollBarDesigner() =>
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
                    new KryptonScrollBarActionList(this)
                };

                return actionList;
            }
        }

        #endregion
    }
}
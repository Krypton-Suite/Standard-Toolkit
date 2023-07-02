﻿#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    internal class KryptonLanguageManagerDesigner : ControlDesigner
    {
        #region Public Overrides

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                DesignerActionListCollection actionList = new DesignerActionListCollection
                {
                    new KryptonLanguageManagerActionList(this)
                };

                return actionList;
            }
        }

        #endregion
    }
}
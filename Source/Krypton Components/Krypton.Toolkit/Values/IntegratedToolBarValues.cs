#region BSD License
/*
 *  
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    [Category(@"code")]
    [ToolboxItem(false)]
    public class IntegratedToolBarValues : Storage
    {
        #region Instance Fields



        #endregion

        #region Public



        #endregion

        #region Identity

        public IntegratedToolBarValues()
        {
            Reset();
        }

        #endregion

        #region Implementation

        public override bool IsDefault { get; }

        public void Reset()
        {

        }

        #endregion
    }
}
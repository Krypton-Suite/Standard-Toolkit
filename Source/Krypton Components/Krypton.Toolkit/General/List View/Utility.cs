#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *
 */
#endregion

namespace Krypton.Toolkit
{
    internal class Utility
    {
        #region Constructor
        public Utility()
        {

        }
        #endregion

        #region Methods
        public static bool IsSevenOrHigher()
        {
            Version osVersion = Environment.OSVersion.Version;

            if (osVersion.Major >= 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
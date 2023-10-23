#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    internal class AboutToolkitManager
    {
        #region Instance Fields

        private AboutToolkitValues _values;

        private KryptonAboutToolkitControl _aboutToolkitControl;

        #endregion

        #region Identity

        public AboutToolkitManager(KryptonAboutToolkitControl aboutToolkitControl, AboutToolkitValues values)
        {
            _aboutToolkitControl = aboutToolkitControl;

            _values = values;
        }

        #endregion
    }
}
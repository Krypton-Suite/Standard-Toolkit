#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    public class InformationBoxTitleIcon
    {
        #region Instance Fields

        private readonly Icon _icon;

        #endregion

        #region Properties

        internal Icon Icon => _icon;

        #endregion

        #region Identity

        public InformationBoxTitleIcon(string fileName)
        {
            _icon = new Icon(fileName);
        }

        public InformationBoxTitleIcon(Icon icon)
        {
            _icon = icon;
        }

        #endregion
    }
}
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
    public class UACShieldValues : ButtonValues
    {
        #region Instance Fields

        private bool _useAsUACShieldButton;

        private bool _useOSStyleImage;

        private UACShieldIconSize _iconSize;

        private Size _customImageSize;

        #endregion

        public override bool IsDefault =>
                                             (UseAsUACShieldButton == false) &&
                                             (UseOSStyleImage == false) &&
                                             (ShieldIconSize == UACShieldIconSize.ExtraSmall) &&
                                             (CustomImageSize == null);

        #region Identity

        public UACShieldValues(NeedPaintHandler needPaint) : base(needPaint)
        {
        }

        #endregion

        #region Public

        public bool UseAsUACShieldButton { get => _useAsUACShieldButton; set => _useAsUACShieldButton = value; }

        public bool UseOSStyleImage { get => _useOSStyleImage; set => _useOSStyleImage = value; }

        public UACShieldIconSize ShieldIconSize { get => _iconSize; set => _iconSize = value; }

        public Size CustomImageSize { get => _customImageSize; set => _customImageSize = value; }

        #endregion
    }
}
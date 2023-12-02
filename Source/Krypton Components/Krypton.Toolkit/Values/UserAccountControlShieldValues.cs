#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved. 
 */
#endregion

namespace Krypton.Toolkit
{
    public class UserAccountControlShieldValues : Storage, IContentValues
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
                                             (CustomImageSize == Size.Empty);

        #region Identity

        public UserAccountControlShieldValues(NeedPaintHandler needPaint)
        {
            NeedPaint = needPaint;

            _useAsUACShieldButton = false;

            _useOSStyleImage = true;

            _iconSize = UACShieldIconSize.Small;
        }

        #endregion

        #region Public

        public bool UseAsUACShieldButton { get => _useAsUACShieldButton; set => _useAsUACShieldButton = value; }

        public bool UseOSStyleImage { get => _useOSStyleImage; set => _useOSStyleImage = value; }

        public UACShieldIconSize ShieldIconSize { get => _iconSize; set => _iconSize = value; }

        public Size CustomImageSize { get => _customImageSize; set => _customImageSize = value; }

        #endregion

        public Image? GetImage(PaletteState state)
        {
            throw new NotImplementedException();
        }

        public Color GetImageTransparentColor(PaletteState state)
        {
            throw new NotImplementedException();
        }

        public string GetShortText()
        {
            throw new NotImplementedException();
        }

        public string GetLongText()
        {
            throw new NotImplementedException();
        }
    }
}
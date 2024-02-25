#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2024. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    public class PaletteMicrosoft365LightGray : PaletteMicrosoft365Base
    {
        #region Instance Fields

        private readonly Color _tabRowBackgroundColor = GlobalStaticValues.EMPTY_COLOR;

        #region Ribbon Specific Colors

        private static readonly Color _ribbonAppButtonDarkColor = GlobalStaticValues.EMPTY_COLOR;

        private static readonly Color _ribbonAppButtonLightColor = GlobalStaticValues.EMPTY_COLOR;

        private static readonly Color _ribbonAppButtonTextColor = GlobalStaticValues.EMPTY_COLOR;

        #endregion

        #endregion

        public PaletteMicrosoft365LightGray(Color[] schemeColours, ImageList checkBoxList, ImageList galleryButtonList, Image?[] radioButtonArray, Color[] trackBarColours) : base(schemeColours, checkBoxList, galleryButtonList, radioButtonArray, trackBarColours)
        {
        }

        public override Image? GetContextMenuSubMenuImage() => throw new NotImplementedException();

        #region Tab Row Background

        /// <inheritdoc />
        public override Color GetRibbonTabRowGradientColor1(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

        /// <inheritdoc />
        public override Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state) =>
            GlobalStaticValues.EMPTY_COLOR;

        /// <inheritdoc />
        public override Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state) =>
            GlobalStaticValues.EMPTY_COLOR;

        /// <inheritdoc />
        public override Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) => _tabRowBackgroundColor;

        /// <inheritdoc />
        public override float GetRibbonTabRowGradientRaftingAngle(PaletteState state) => -1;

        #endregion

        #region AppButton Colors

        /// <inheritdoc />
        public override Color GetRibbonAppButtonDarkColor(PaletteState state) => _ribbonAppButtonDarkColor;

        /// <inheritdoc />
        public override Color GetRibbonAppButtonLightColor(PaletteState state) => _ribbonAppButtonLightColor;

        /// <inheritdoc />
        public override Color GetRibbonAppButtonTextColor(PaletteState state) => _ribbonAppButtonTextColor;

        #endregion
    }
}

#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public class PaletteOffice2010LightGray : PaletteOffice2010Base
{
    #region Static Fields

    #region Colors

    private readonly Color _tabRowBackgroundGradientRaftingDarkColor = Color.FromArgb(41, 57, 85);

    private readonly Color _tabRowBackgroundGradientRaftingLightColor = Color.FromArgb(188, 199, 216);

    #endregion

    #region Ribbon Specific Colors

    private static readonly Color _ribbonAppButtonDarkColor = GlobalStaticValues.EMPTY_COLOR;

    private static readonly Color _ribbonAppButtonLightColor = GlobalStaticValues.EMPTY_COLOR;

    private static readonly Color _ribbonAppButtonTextColor = GlobalStaticValues.EMPTY_COLOR;

    #endregion

    #region Rafting

    private readonly float _gradientRafting = GlobalStaticValues.DEFAULT_RAFTING_RIBBON_TAB_BACKGROUND_GRADIENT;

    #endregion

    #endregion

    public PaletteOffice2010LightGray(Color[] schemeColors, ImageList checkBoxList, ImageList galleryButtonList, Image?[] radioButtonArray, Color[] trackBarColors) : base(
        new EmptySchemeBase(),
        checkBoxList,
        galleryButtonList,
        radioButtonArray)
    {
    }

    public override Image? GetContextMenuSubMenuImage() => throw new NotImplementedException();

    #region Tab Row Background

    /// <inheritdoc />
    public override Color GetRibbonTabRowGradientColor1(PaletteState state) =>
        GlobalStaticValues.TAB_ROW_GRADIENT_FIRST_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state) =>
        _tabRowBackgroundGradientRaftingDarkColor;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state) =>
        _tabRowBackgroundGradientRaftingLightColor;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

    /// <inheritdoc />
    public override float GetRibbonTabRowGradientRaftingAngle(PaletteState state) => _gradientRafting;

    #endregion

    #region AppButton Colors

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabBottomColor(PaletteState state) => _ribbonAppButtonDarkColor;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTopColor(PaletteState state) => _ribbonAppButtonLightColor;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTextColor(PaletteState state) => _ribbonAppButtonTextColor;

    #endregion
}
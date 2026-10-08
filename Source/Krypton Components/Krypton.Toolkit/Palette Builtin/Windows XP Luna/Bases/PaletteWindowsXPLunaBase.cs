#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Base for Windows XP Luna visual style palettes (rounded chrome, classic window background).
/// </summary>
public abstract class PaletteWindowsXPLunaBase : PaletteOffice2007Base
{
    private readonly Image? _contextMenuSubMenu;
    private readonly Image? _sizeGripImage;
    private readonly Func<PaletteButtonSpecStyle, PaletteState, Image?> _formButtonImageProvider;

    protected PaletteWindowsXPLunaBase(
        string themeName,
        KryptonColorSchemeBase scheme,
        ImageList checkBoxList,
        ImageList galleryButtonList,
        Image?[] radioButtonArray,
        Image? contextMenuSubMenu,
        Image? sizeGripImage,
        Func<PaletteButtonSpecStyle, PaletteState, Image?> formButtonImageProvider)
        : base(themeName, scheme, checkBoxList, galleryButtonList, radioButtonArray)
    {
        _contextMenuSubMenu = contextMenuSubMenu;
        _sizeGripImage = sizeGripImage;
        _formButtonImageProvider = formButtonImageProvider;
    }

    /// <inheritdoc />
    public override IRenderer GetRenderer() => KryptonManager.RenderWindowsXPLuna;

    /// <inheritdoc />
    public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            return PaletteColorStyle.Inherit;
        }

        if (WindowsXPLunaRenderHelper.UsesLunaPushButtonChrome(style))
        {
            return PaletteColorStyle.Rounding2;
        }

        return style switch
        {
            PaletteBackStyle.HeaderForm => PaletteColorStyle.Rounding4,
            PaletteBackStyle.TabHighProfile or PaletteBackStyle.TabStandardProfile => PaletteColorStyle.Rounding3,
            _ => base.GetBackColorStyle(style, state)
        };
    }

    /// <inheritdoc />
    public override Color GetBackColor1(PaletteBackStyle style, PaletteState state)
    {
        if (WindowsXPLunaRenderHelper.UsesLunaTabChrome(style))
        {
            return GetLunaTabBackColor1(state);
        }

        return base.GetBackColor1(style, state);
    }

    /// <inheritdoc />
    public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
    {
        if (WindowsXPLunaRenderHelper.UsesLunaTabChrome(style))
        {
            return GetLunaTabBackColor2(state);
        }

        return base.GetBackColor2(style, state);
    }

    private Color GetLunaTabBackColor1(PaletteState state) => state switch
    {
        PaletteState.Disabled => SystemColors.Control,
        PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed =>
            WindowsXPLunaRenderHelper.TabSelectedFace,
        _ => WindowsXPLunaRenderHelper.TabUnselectedFace1
    };

    private Color GetLunaTabBackColor2(PaletteState state) => state switch
    {
        PaletteState.Disabled => SystemColors.Control,
        PaletteState.CheckedNormal or PaletteState.CheckedTracking or PaletteState.CheckedPressed =>
            WindowsXPLunaRenderHelper.TabSelectedFace,
        _ => WindowsXPLunaRenderHelper.TabUnselectedFace2
    };

    /// <inheritdoc />
    public override Image? GetContextMenuSubMenuImage() => _contextMenuSubMenu;

    /// <inheritdoc />
    public override Image? GetSizeGripImage(RightToLeft isRtl) => _sizeGripImage;

    /// <inheritdoc />
    public override Image? GetButtonSpecImage(PaletteButtonSpecStyle style, PaletteState state) =>
        _formButtonImageProvider(style, state) ?? base.GetButtonSpecImage(style, state);

    /// <inheritdoc />
    public override Color GetRibbonTabRowGradientColor1(PaletteState state) => GlobalStaticVariables.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state) =>
        GlobalStaticVariables.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state) =>
        GlobalStaticVariables.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) => GlobalStaticVariables.EMPTY_COLOR;

    /// <inheritdoc />
    public override float GetRibbonTabRowGradientRaftingAngle(PaletteState state) => -1;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabBottomColor(PaletteState state) => GlobalStaticVariables.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTopColor(PaletteState state) => GlobalStaticVariables.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTextColor(PaletteState state) => GlobalStaticVariables.EMPTY_COLOR;
}

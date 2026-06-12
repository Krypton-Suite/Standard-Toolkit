#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Norton Commander style retro palette (bright blue workspace, cyan highlights).
/// </summary>
public class PaletteRetroBlue : PaletteRetroBase
{
    private static readonly Color _tabRowBackgroundColor = Color.FromArgb(0, 0, 170);

    private static readonly Color _ribbonAppButtonDarkColor = Color.FromArgb(0, 0, 128);

    private static readonly Color _ribbonAppButtonLightColor = Color.FromArgb(0, 0, 170);

    private static readonly Color _ribbonAppButtonTextColor = Color.White;

    private static readonly Color _gridDataCellBackColor = Color.FromArgb(0, 0, 128);

    private static readonly Color _groupBoxBorderColor = Color.FromArgb(192, 192, 192);

    private static readonly Color _panelAlternateBackColor = Color.FromArgb(0, 0, 204);

    public PaletteRetroBlue()
        : base(new PaletteRetroBlue_BaseScheme(), RetroPaletteSharedAssets.CheckBoxList,
            RetroPaletteSharedAssets.GalleryButtonList, RetroPaletteSharedAssets.RadioButtonArray)
    {
        ThemeName = nameof(PaletteRetroBlue);
        RetroRenderHelper.ResetButtonShadowSize();
    }

    public override Image? GetContextMenuCheckedImage() => RetroPaletteSharedAssets.ContextMenuChecked;

    public override Image? GetContextMenuIndeterminateImage() => RetroPaletteSharedAssets.ContextMenuIndeterminate;

    public override Image? GetContextMenuSubMenuImage() => RetroPaletteSharedAssets.ContextMenuSubMenu;

    public override Image? GetButtonSpecImage(PaletteButtonSpecStyle style, PaletteState state) => style switch
    {
        PaletteButtonSpecStyle.FormClose => state switch
        {
            PaletteState.Tracking => RetroPaletteSharedAssets.FormCloseActive,
            PaletteState.Normal => RetroPaletteSharedAssets.FormCloseNormal,
            PaletteState.Pressed => RetroPaletteSharedAssets.FormClosePressed,
            _ => RetroPaletteSharedAssets.FormCloseDisabled
        },
        PaletteButtonSpecStyle.FormMin => state switch
        {
            PaletteState.Normal => RetroPaletteSharedAssets.FormMinimiseNormal,
            PaletteState.Tracking => RetroPaletteSharedAssets.FormMinimiseActive,
            PaletteState.Pressed => RetroPaletteSharedAssets.FormMinimisePressed,
            _ => RetroPaletteSharedAssets.FormMinimiseDisabled
        },
        PaletteButtonSpecStyle.FormMax => state switch
        {
            PaletteState.Normal => RetroPaletteSharedAssets.FormMaximiseNormal,
            PaletteState.Tracking => RetroPaletteSharedAssets.FormMaximiseActive,
            PaletteState.Pressed => RetroPaletteSharedAssets.FormMaximisePressed,
            _ => RetroPaletteSharedAssets.FormMaximiseDisabled
        },
        PaletteButtonSpecStyle.FormRestore => state switch
        {
            PaletteState.Normal => RetroPaletteSharedAssets.FormRestoreNormal,
            PaletteState.Tracking => RetroPaletteSharedAssets.FormRestoreActive,
            PaletteState.Pressed => RetroPaletteSharedAssets.FormRestorePressed,
            _ => RetroPaletteSharedAssets.FormRestoreDisabled
        },
        PaletteButtonSpecStyle.FormHelp => state switch
        {
            PaletteState.Tracking => RetroPaletteSharedAssets.FormHelpActive,
            PaletteState.Pressed => RetroPaletteSharedAssets.FormHelpPressed,
            PaletteState.Normal => RetroPaletteSharedAssets.FormHelpNormal,
            _ => RetroPaletteSharedAssets.FormHelpDisabled
        },
        _ => base.GetButtonSpecImage(style, state)
    };

    public override Color GetRibbonTabRowGradientColor1(PaletteState state) => GlobalStaticVariables.EMPTY_COLOR;

    public override Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state) =>
        GlobalStaticVariables.EMPTY_COLOR;

    public override Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state) =>
        GlobalStaticVariables.EMPTY_COLOR;

    public override Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) => _tabRowBackgroundColor;

    public override float GetRibbonTabRowGradientRaftingAngle(PaletteState state) => -1;

    public override Color GetRibbonFileAppTabBottomColor(PaletteState state) => _ribbonAppButtonDarkColor;

    public override Color GetRibbonFileAppTabTopColor(PaletteState state) => _ribbonAppButtonLightColor;

    public override Color GetRibbonFileAppTabTextColor(PaletteState state) => _ribbonAppButtonTextColor;

    public override Color GetRibbonTextColor(PaletteRibbonTextStyle style, PaletteState state) =>
        style == PaletteRibbonTextStyle.RibbonTab && state is PaletteState.Tracking or PaletteState.Pressed
            ? Color.Black
            : base.GetRibbonTextColor(style, state);

    protected override Color GetRetroListItemNormalBackColor() => ChromeBackgroundColor;

    protected override Color GetRetroListItemNormalTextColor() => Color.White;

    protected override Color GetRetroCommandButtonTextColor() => Color.White;

    protected override Color GetRetroGridDataCellNormalBackColor() => _gridDataCellBackColor;

    protected override Color GetRetroGridDataCellNormalTextColor() => Color.White;

    protected override Color WorkspaceTextColor => Color.White;

    protected override Color GroupBoxBorderColor => _groupBoxBorderColor;

    protected override Color PanelAlternateBackColor => _panelAlternateBackColor;

    protected override Color InputControlBackColor => _gridDataCellBackColor;

    protected override Color InputControlTextColor => Color.White;

    protected override Color FormHeaderTextColor => Color.Black;

    internal override Color RetroButtonFrameColor => _groupBoxBorderColor;

}

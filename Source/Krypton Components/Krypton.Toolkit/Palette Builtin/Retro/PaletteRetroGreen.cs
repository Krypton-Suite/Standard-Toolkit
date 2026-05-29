#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// DOS-style retro palette (teal workspace, green push buttons, silver chrome).
/// </summary>
public class PaletteRetroGreen : PaletteRetroBase
{
    private static readonly Color _tabRowBackgroundColor = Color.FromArgb(0, 160, 160);

    private static readonly Color _ribbonAppButtonDarkColor = Color.FromArgb(0, 128, 128);

    private static readonly Color _ribbonAppButtonLightColor = Color.FromArgb(0, 160, 160);

    private static readonly Color _ribbonAppButtonTextColor = Color.Black;

    private static readonly Color _gridDataCellBackColor = Color.White;

    private static readonly Color _gridDataCellTextColor = Color.Black;

    private static readonly Color _workspaceTextColor = Color.Black;

    public PaletteRetroGreen()
        : base(new PaletteRetroGreen_BaseScheme(), RetroPaletteSharedAssets.CheckBoxList,
            RetroPaletteSharedAssets.GalleryButtonList, RetroPaletteSharedAssets.RadioButtonArray)
    {
        ThemeName = nameof(PaletteRetroGreen);
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

    protected override Color GetRetroListItemNormalBackColor() => _gridDataCellBackColor;

    protected override Color GetRetroListItemNormalTextColor() => _gridDataCellTextColor;

    protected override Color GetRetroGridDataCellNormalBackColor() => _gridDataCellBackColor;

    protected override Color GetRetroGridDataCellNormalTextColor() => _gridDataCellTextColor;

    protected override Color WorkspaceTextColor => _workspaceTextColor;
}

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2023 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Abstract base for Visual Studio 2022 dark palettes.
/// </summary>
public abstract class PaletteVisualStudio2022DarkBase : PaletteVisualStudioBase
{
    protected PaletteVisualStudio2022DarkBase(Color[] schemeColors, ImageList checkBoxList, ImageList galleryButtonList, Image?[] radioButtonArray, Color[] trackBarColors)
        : base(schemeColors, checkBoxList, galleryButtonList, radioButtonArray, trackBarColors)
    {
        ThemeName = nameof(PaletteVisualStudio2022DarkBase);
    }

    protected PaletteVisualStudio2022DarkBase(
        [DisallowNull] KryptonColorSchemeBase scheme,
        [DisallowNull] ImageList checkBoxList,
        [DisallowNull] ImageList galleryButtonList,
        [DisallowNull] Image?[] radioButtonArray)
        : base(scheme, checkBoxList, galleryButtonList, radioButtonArray)
    {
        ThemeName = nameof(PaletteVisualStudio2022DarkBase);
    }

    public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state)
    {
        if ((style == PaletteBackStyle.ControlClient || style == PaletteBackStyle.ButtonListItem)
            && !CommonHelper.IsOverrideState(state))
        {
            return PaletteColorStyle.Solid;
        }

        return base.GetBackColorStyle(style, state);
    }

    public override Color GetBackColor1(PaletteBackStyle style, PaletteState state)
    {
        if (style == PaletteBackStyle.ControlClient)
        {
            return base.GetBackColor1(PaletteBackStyle.PanelClient, state);
        }

        if (style == PaletteBackStyle.ButtonListItem)
        {
            return GetDropDownItemBackColor(state);
        }

        return base.GetBackColor1(style, state);
    }

    public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
    {
        if (style == PaletteBackStyle.ControlClient)
        {
            return base.GetBackColor2(PaletteBackStyle.PanelClient, state);
        }

        if (style == PaletteBackStyle.ButtonListItem)
        {
            return GetDropDownItemBackColor(state);
        }

        return base.GetBackColor2(style, state);
    }

    public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
    {
        if (style == PaletteContentStyle.ButtonListItem)
        {
            return GetDropDownItemTextColor(state);
        }

        return base.GetContentShortTextColor1(style, state);
    }

    public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
    {
        if (style == PaletteContentStyle.ButtonListItem)
        {
            return GetDropDownItemTextColor(state);
        }

        return base.GetContentShortTextColor2(style, state);
    }

    private Color GetDropDownItemBackColor(PaletteState state)
    {
        return state switch
        {
            PaletteState.Tracking or PaletteState.Pressed or PaletteState.CheckedNormal or PaletteState.CheckedTracking
                or PaletteState.CheckedPressed => base.GetBackColor1(PaletteBackStyle.PanelAlternate,
                    PaletteState.Normal),
            _ => base.GetBackColor1(PaletteBackStyle.PanelClient, PaletteState.Normal)
        };
    }

    private Color GetDropDownItemTextColor(PaletteState state)
    {
        if (state == PaletteState.Disabled)
        {
            return Color.FromArgb(100, 100, 100);
        }

        var color = GetSchemeColor(SchemeBaseColors.TextListItem);
        return color.IsEmpty ? Color.White : color;
    }
}

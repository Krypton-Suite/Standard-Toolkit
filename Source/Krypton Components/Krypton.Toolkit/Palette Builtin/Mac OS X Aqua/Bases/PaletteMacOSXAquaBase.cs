#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Base for Mac OS X Aqua palettes: Office 2010 metrics with gel buttons and pinstripe chrome via <see cref="RenderMacOSXAqua"/>.
/// </summary>
public abstract class PaletteMacOSXAquaBase : PaletteOffice2010Base
{
    private const float ControlCornerRadius = 5f;
    private const float WindowCornerRadius = 8f;

    static PaletteMacOSXAquaBase()
    {
        // Gel hover/press tints (replace Office 2010 gold ExpertTracking colours).
        RegisterColor<PaletteMacOSXAquaBase, ButtonBackColor>(ButtonBackColor.Color3, Color.FromArgb(255, 255, 255));
        RegisterColor<PaletteMacOSXAquaBase, ButtonBackColor>(ButtonBackColor.Color4, Color.FromArgb(176, 200, 228));
        RegisterColor<PaletteMacOSXAquaBase, ButtonBackColor>(ButtonBackColor.Color5, Color.FromArgb(198, 214, 234));
        RegisterColor<PaletteMacOSXAquaBase, ButtonBackColor>(ButtonBackColor.Color6, Color.FromArgb(140, 164, 190));
        RegisterColor<PaletteMacOSXAquaBase, ButtonBackColor>(ButtonBackColor.Color7, Color.FromArgb(220, 235, 255));
        RegisterColor<PaletteMacOSXAquaBase, ButtonBackColor>(ButtonBackColor.Color8, Color.FromArgb(176, 200, 228));
        RegisterColor<PaletteMacOSXAquaBase, ButtonBackColor>(ButtonBackColor.Color9, Color.FromArgb(200, 220, 245));
        RegisterColor<PaletteMacOSXAquaBase, ButtonBackColor>(ButtonBackColor.Color10, Color.FromArgb(160, 188, 220));

        RegisterColor<PaletteMacOSXAquaBase, ButtonBorderColor>(ButtonBorderColor.Color2, Color.FromArgb(117, 144, 175));
        RegisterColor<PaletteMacOSXAquaBase, ButtonBorderColor>(ButtonBorderColor.Color3, Color.FromArgb(143, 165, 191));
        RegisterColor<PaletteMacOSXAquaBase, ButtonBorderColor>(ButtonBorderColor.Color4, Color.FromArgb(96, 124, 158));
        RegisterColor<PaletteMacOSXAquaBase, ButtonBorderColor>(ButtonBorderColor.Color5, Color.FromArgb(117, 144, 175));
        RegisterColor<PaletteMacOSXAquaBase, ButtonBorderColor>(ButtonBorderColor.Color6, Color.FromArgb(74, 120, 175));
        RegisterColor<PaletteMacOSXAquaBase, ButtonBorderColor>(ButtonBorderColor.Color7, Color.FromArgb(117, 144, 175));
    }

    protected PaletteMacOSXAquaBase(
        KryptonColorSchemeBase scheme,
        ImageList checkBoxList,
        ImageList galleryButtonList,
        Image?[] radioButtonArray)
        : base(scheme, checkBoxList, galleryButtonList, radioButtonArray)
    {
    }

    /// <inheritdoc />
    public override IRenderer GetRenderer() => KryptonManager.RenderMacOSXAqua;

    /// <inheritdoc />
    public override PaletteRibbonShape GetRibbonShape() => PaletteRibbonShape.OSXAqua;

    /// <inheritdoc />
    public override Image? GetButtonSpecImage(PaletteButtonSpecStyle style, PaletteState state) =>
        MacOSPaletteSharedAssets.GetFormButtonImage(style, state, isDarkSurface: false)
        ?? base.GetButtonSpecImage(style, state);

    /// <inheritdoc />
    public override PaletteRelativeEdgeAlign GetButtonSpecEdge(PaletteButtonSpecStyle style)
    {
        switch (style)
        {
            case PaletteButtonSpecStyle.FormClose:
            case PaletteButtonSpecStyle.FormMin:
            case PaletteButtonSpecStyle.FormMax:
            case PaletteButtonSpecStyle.FormRestore:
            case PaletteButtonSpecStyle.FormHelp:
                return PaletteRelativeEdgeAlign.Near;
            default:
                return base.GetButtonSpecEdge(style);
        }
    }

    /// <inheritdoc />
    public override int GetMetricInt(KryptonForm? owningForm, PaletteState state, PaletteMetricInt metric)
    {
        if (!CommonHelper.IsOverrideState(state))
        {
            switch (metric)
            {
                case PaletteMetricInt.HeaderButtonEdgeInsetForm:
                case PaletteMetricInt.HeaderButtonEdgeInsetFormRight:
                    return 12;
                case PaletteMetricInt.RibbonTabGap:
                    return 3;
            }
        }

        return base.GetMetricInt(owningForm, state, metric);
    }

    /// <inheritdoc />
    public override float GetBorderRounding(PaletteBorderStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            return GlobalStaticConstants.DEFAULT_PRIMARY_CORNER_ROUNDING_VALUE;
        }

        if (style is PaletteBorderStyle.FormMain or PaletteBorderStyle.HeaderForm)
        {
            return WindowCornerRadius;
        }

        if (IsAquaButtonBorderStyle(style) || IsAquaInputBorderStyle(style))
        {
            return ControlCornerRadius;
        }

        return base.GetBorderRounding(style, state);
    }

    /// <inheritdoc />
    public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state)
    {
        if (style is PaletteBackStyle.HeaderForm or PaletteBackStyle.HeaderPrimary
            or PaletteBackStyle.HeaderSecondary)
        {
            return PaletteColorStyle.Linear;
        }

        if (IsAquaGelButton(style, state))
        {
            return PaletteColorStyle.Linear;
        }

        return base.GetBackColorStyle(style, state);
    }

    private static bool IsAquaGelButton(PaletteBackStyle style, PaletteState state)
    {
        if (state == PaletteState.Disabled)
        {
            return false;
        }

        return style switch
        {
            PaletteBackStyle.ButtonStandalone or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonAlternate
                or PaletteBackStyle.ButtonLowProfile or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonListItem
                or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ButtonGallery or PaletteBackStyle.ButtonCluster
                or PaletteBackStyle.ButtonCustom1 or PaletteBackStyle.ButtonCustom2 or PaletteBackStyle.ButtonCustom3
                or PaletteBackStyle.ButtonInputControl or PaletteBackStyle.ContextMenuItemHighlight => true,
            _ => false
        };
    }

    private static bool IsAquaButtonBorderStyle(PaletteBorderStyle style)
    {
        switch (style)
        {
            case PaletteBorderStyle.ButtonStandalone:
            case PaletteBorderStyle.ButtonGallery:
            case PaletteBorderStyle.ButtonAlternate:
            case PaletteBorderStyle.ButtonLowProfile:
            case PaletteBorderStyle.ButtonBreadCrumb:
            case PaletteBorderStyle.ButtonListItem:
            case PaletteBorderStyle.ButtonCommand:
            case PaletteBorderStyle.ButtonButtonSpec:
            case PaletteBorderStyle.ButtonCluster:
            case PaletteBorderStyle.ButtonCalendarDay:
            case PaletteBorderStyle.ButtonNavigatorStack:
            case PaletteBorderStyle.ButtonNavigatorOverflow:
            case PaletteBorderStyle.ButtonNavigatorMini:
            case PaletteBorderStyle.ButtonInputControl:
                return true;
            default:
                return false;
        }
    }

    private static bool IsAquaInputBorderStyle(PaletteBorderStyle style)
    {
        switch (style)
        {
            case PaletteBorderStyle.InputControlStandalone:
            case PaletteBorderStyle.InputControlRibbon:
            case PaletteBorderStyle.InputControlCustom1:
            case PaletteBorderStyle.InputControlCustom2:
            case PaletteBorderStyle.InputControlCustom3:
                return true;
            default:
                return false;
        }
    }
}
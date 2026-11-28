#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Gauges;

/// <summary>
/// Which equalizer colour is inherited from the palette.
/// </summary>
internal enum EqualizerPaletteColor
{
    /// <summary>Panel fill behind the bars.</summary>
    Face,

    /// <summary>Band frequency labels.</summary>
    Label,

    /// <summary>Lit bar / segment colour for the lower third of the scale.</summary>
    Low,

    /// <summary>Lit bar / segment colour for the middle third of the scale.</summary>
    Mid,

    /// <summary>Lit bar / segment colour for the upper third of the scale.</summary>
    High,

    /// <summary>Unlit segment colour.</summary>
    Off,

    /// <summary>Peak-hold mark.</summary>
    Peak,

    /// <summary>Interactive gain thumb.</summary>
    Thumb
}

/// <summary>
/// Inherit one equalizer colour from the current palette.
/// </summary>
internal sealed class PaletteEqualizerColorInheritRedirect : PaletteElementColorInherit
{
    private readonly EqualizerPaletteColor _color;
    private readonly PaletteDoubleRedirect? _chrome;
    private PaletteRedirect _redirect;

    public PaletteEqualizerColorInheritRedirect(PaletteRedirect redirect, EqualizerPaletteColor color, PaletteDoubleRedirect? chrome)
    {
        _redirect = redirect;
        _color = color;
        _chrome = chrome;
    }

    public void SetRedirector(PaletteRedirect redirect) => _redirect = redirect;

    /// <inheritdoc />
    public override Color GetElementColor1(PaletteState state)
    {
        // Disabled lit / label / peak / thumb colours stay muted even when a scheme slot is set.
        if (state == PaletteState.Disabled &&
            _color != EqualizerPaletteColor.Face &&
            _color != EqualizerPaletteColor.Off)
        {
            return _redirect.GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, state);
        }

        Color scheme = _redirect.GetSchemeColor(GetSchemeSlot());
        if (!SchemeBaseColorsExtensions.IsEmptySchemeColor(scheme))
        {
            return scheme;
        }

        switch (_color)
        {
            case EqualizerPaletteColor.Face:
                return _redirect.GetBackColor1(_chrome?.BackStyle ?? PaletteBackStyle.PanelClient, state);
            case EqualizerPaletteColor.Label:
            case EqualizerPaletteColor.Low:
            case EqualizerPaletteColor.Mid:
                return _redirect.GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, state);
            case EqualizerPaletteColor.High:
                return _redirect.GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, PaletteState.LinkPressedOverride);
            case EqualizerPaletteColor.Peak:
                return _redirect.GetBackColor1(PaletteBackStyle.HeaderDockActive, state);
            case EqualizerPaletteColor.Thumb:
                return _redirect.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Tracking);
            default:
                return _redirect.GetBackColor1(PaletteBackStyle.ButtonStandalone, state);
        }
    }

    /// <inheritdoc />
    public override Color GetElementColor2(PaletteState state) =>
        _color == EqualizerPaletteColor.Face
            ? _redirect.GetBorderColor1(_chrome?.BorderStyle ?? PaletteBorderStyle.ControlClient, state)
            : GetElementColor1(state);

    /// <inheritdoc />
    public override Color GetElementColor3(PaletteState state) => GetElementColor1(state);

    /// <inheritdoc />
    public override Color GetElementColor4(PaletteState state) => GetElementColor1(state);

    /// <inheritdoc />
    public override Color GetElementColor5(PaletteState state) => GetElementColor1(state);

    private SchemeBaseColors GetSchemeSlot()
    {
        switch (_color)
        {
            case EqualizerPaletteColor.Face:
                return SchemeBaseColors.EqualizerFace;
            case EqualizerPaletteColor.Label:
                return SchemeBaseColors.EqualizerLabel;
            case EqualizerPaletteColor.Low:
                return SchemeBaseColors.EqualizerLow;
            case EqualizerPaletteColor.Mid:
                return SchemeBaseColors.EqualizerMid;
            case EqualizerPaletteColor.High:
                return SchemeBaseColors.EqualizerHigh;
            case EqualizerPaletteColor.Off:
                return SchemeBaseColors.EqualizerOff;
            case EqualizerPaletteColor.Peak:
                return SchemeBaseColors.EqualizerPeak;
            case EqualizerPaletteColor.Thumb:
                return SchemeBaseColors.EqualizerThumb;
            default:
                return SchemeBaseColors.EqualizerOff;
        }
    }
}

/// <summary>
/// Storage for one equalizer colour that defaults to the palette and can be overridden.
/// </summary>
public class PaletteEqualizerColorRedirect : PaletteElementColor
{
    private readonly PaletteEqualizerColorInheritRedirect _inherit;

    internal PaletteEqualizerColorRedirect(PaletteRedirect redirect, EqualizerPaletteColor color, NeedPaintHandler? needPaint, PaletteDoubleRedirect? chrome)
        : base(null, needPaint)
    {
        _inherit = new PaletteEqualizerColorInheritRedirect(redirect, color, chrome);
        SetInherit(_inherit);
    }

    internal void SetRedirector(PaletteRedirect redirect) => _inherit.SetRedirector(redirect);
}

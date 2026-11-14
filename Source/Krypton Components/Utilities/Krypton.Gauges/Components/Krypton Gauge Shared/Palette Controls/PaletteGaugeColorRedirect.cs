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
/// Which gauge colour is inherited from the palette.
/// </summary>
internal enum GaugePaletteColor
{
    /// <summary>Face fill. <see cref="PaletteElementColor.Color2"/> is the border.</summary>
    Face,

    /// <summary>Scale ticks and labels.</summary>
    Tick,

    /// <summary>Needle or primary indicator.</summary>
    Needle,

    /// <summary>Value fill on linear and rectangular faces.</summary>
    Fill,

    /// <summary>Warning range band.</summary>
    RangeWarning,

    /// <summary>Error range band.</summary>
    RangeError,

    /// <summary>Circular center cap.</summary>
    Cap,

    /// <summary>Peak marker.</summary>
    Peak,

    /// <summary>Unlit rectangular segments.</summary>
    SegmentOff
}

/// <summary>
/// Inherit one gauge colour from the current palette.
/// </summary>
internal sealed class PaletteGaugeColorInheritRedirect : PaletteElementColorInherit
{
    private readonly GaugePaletteColor _color;
    private readonly PaletteDoubleRedirect? _chrome;
    private PaletteRedirect _redirect;

    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteGaugeColorInheritRedirect"/> class.
    /// </summary>
    /// <param name="redirect">Source for inheriting values.</param>
    /// <param name="color">Gauge colour this instance supplies.</param>
    /// <param name="chrome">Panel style used for <see cref="GaugePaletteColor.Face"/>.</param>
    public PaletteGaugeColorInheritRedirect(PaletteRedirect redirect, GaugePaletteColor color, PaletteDoubleRedirect? chrome)
    {
        _redirect = redirect;
        _color = color;
        _chrome = chrome;
    }

    /// <summary>
    /// Update the redirector with a new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public void SetRedirector(PaletteRedirect redirect) => _redirect = redirect;

    /// <inheritdoc />
    public override Color GetElementColor1(PaletteState state)
    {
        if (state == PaletteState.Disabled &&
            _color != GaugePaletteColor.Face &&
            _color != GaugePaletteColor.SegmentOff)
        {
            return _redirect.GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, state);
        }

        switch (_color)
        {
            case GaugePaletteColor.Face:
                return _redirect.GetBackColor1(_chrome?.BackStyle ?? PaletteBackStyle.PanelClient, state);
            case GaugePaletteColor.Tick:
            case GaugePaletteColor.Needle:
            case GaugePaletteColor.Cap:
                return _redirect.GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, state);
            case GaugePaletteColor.Fill:
                return _redirect.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Tracking);
            case GaugePaletteColor.RangeWarning:
                return _redirect.GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, PaletteState.LinkNotVisitedOverride);
            case GaugePaletteColor.RangeError:
                return _redirect.GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, PaletteState.LinkPressedOverride);
            case GaugePaletteColor.Peak:
                return _redirect.GetBackColor1(PaletteBackStyle.HeaderDockActive, state);
            default:
                return _redirect.GetBackColor1(PaletteBackStyle.ButtonStandalone, state);
        }
    }

    /// <inheritdoc />
    public override Color GetElementColor2(PaletteState state) =>
        _color == GaugePaletteColor.Face
            ? _redirect.GetBorderColor1(_chrome?.BorderStyle ?? PaletteBorderStyle.ControlClient, state)
            : GetElementColor1(state);

    /// <inheritdoc />
    public override Color GetElementColor3(PaletteState state) => GetElementColor1(state);

    /// <inheritdoc />
    public override Color GetElementColor4(PaletteState state) => GetElementColor1(state);

    /// <inheritdoc />
    public override Color GetElementColor5(PaletteState state) => GetElementColor1(state);
}

/// <summary>
/// Storage for one gauge colour that defaults to the palette and can be overridden.
/// </summary>
public class PaletteGaugeColorRedirect : PaletteElementColor
{
    private readonly PaletteGaugeColorInheritRedirect _inherit;

    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteGaugeColorRedirect"/> class.
    /// </summary>
    /// <param name="redirect">Source for inheriting values.</param>
    /// <param name="color">Gauge colour this storage supplies.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    /// <param name="chrome">Panel style used when <paramref name="color"/> is the face.</param>
    internal PaletteGaugeColorRedirect(PaletteRedirect redirect, GaugePaletteColor color, NeedPaintHandler? needPaint, PaletteDoubleRedirect? chrome)
        : base(null, needPaint)
    {
        _inherit = new PaletteGaugeColorInheritRedirect(redirect, color, chrome);
        SetInherit(_inherit);
    }

    /// <summary>
    /// Update the redirector with a new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    internal void SetRedirector(PaletteRedirect redirect) => _inherit.SetRedirector(redirect);
}

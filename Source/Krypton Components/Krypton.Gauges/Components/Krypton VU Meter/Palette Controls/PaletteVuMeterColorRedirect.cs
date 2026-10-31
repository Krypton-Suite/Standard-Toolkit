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
/// Which meter colour is inherited from the palette.
/// </summary>
internal enum VuMeterPaletteColor
{
    /// <summary>Dial fill. <see cref="PaletteElementColor.Color2"/> is the dial border.</summary>
    Face,

    /// <summary>Scale ticks and labels.</summary>
    Tick,

    /// <summary>Live needle.</summary>
    Needle,

    /// <summary>Held peak.</summary>
    Peak,

    /// <summary>Lit segments below 0 VU.</summary>
    SegmentLow,

    /// <summary>Overload arc and lit segments at 0 VU and above.</summary>
    SegmentHigh,

    /// <summary>Segments that are not lit.</summary>
    SegmentOff
}

/// <summary>
/// Inherit one meter colour from the current palette.
/// </summary>
internal sealed class PaletteVuMeterColorInheritRedirect : PaletteElementColorInherit
{
    private readonly VuMeterPaletteColor _color;
    private PaletteRedirect _redirect;

    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteVuMeterColorInheritRedirect"/> class.
    /// </summary>
    /// <param name="redirect">Source for inheriting values.</param>
    /// <param name="color">Meter colour this instance supplies.</param>
    public PaletteVuMeterColorInheritRedirect(PaletteRedirect redirect, VuMeterPaletteColor color)
    {
        _redirect = redirect;
        _color = color;
    }

    /// <summary>
    /// Update the redirector with a new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public void SetRedirector(PaletteRedirect redirect) => _redirect = redirect;

    /// <inheritdoc />
    public override Color GetElementColor1(PaletteState state)
    {
        // A disabled meter uses the palette disabled colours, not the accent colours below.
        if (state == PaletteState.Disabled && _color != VuMeterPaletteColor.Face && _color != VuMeterPaletteColor.SegmentOff)
        {
            return _redirect.GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, state);
        }

        switch (_color)
        {
            case VuMeterPaletteColor.Face:
                return _redirect.GetBackColor1(PaletteBackStyle.InputControlStandalone, state);
            case VuMeterPaletteColor.Tick:
            case VuMeterPaletteColor.Needle:
            case VuMeterPaletteColor.SegmentLow:
                // Label text stays dark on a light face and light on a dark face.
                return _redirect.GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, state);
            case VuMeterPaletteColor.Peak:
                // Checked dock header is the theme accent (gold on the Office and Microsoft 365 families).
                return _redirect.GetBackColor1(PaletteBackStyle.HeaderDockActive, state);
            case VuMeterPaletteColor.SegmentHigh:
                return _redirect.GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, PaletteState.LinkPressedOverride);
            default:
                // Unlit segments need a tint that is not the input-control face.
                return _redirect.GetBackColor1(PaletteBackStyle.ButtonStandalone, state);
        }
    }

    /// <inheritdoc />
    public override Color GetElementColor2(PaletteState state) =>
        _color == VuMeterPaletteColor.Face
            ? _redirect.GetBorderColor1(PaletteBorderStyle.InputControlStandalone, state)
            : GetElementColor1(state);

    /// <inheritdoc />
    public override Color GetElementColor3(PaletteState state) => GetElementColor1(state);

    /// <inheritdoc />
    public override Color GetElementColor4(PaletteState state) => GetElementColor1(state);

    /// <inheritdoc />
    public override Color GetElementColor5(PaletteState state) => GetElementColor1(state);
}

/// <summary>
/// Storage for one meter colour that defaults to the palette and can be overridden.
/// </summary>
public class PaletteVuMeterColorRedirect : PaletteElementColor
{
    private readonly PaletteVuMeterColorInheritRedirect _inherit;

    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteVuMeterColorRedirect"/> class.
    /// </summary>
    /// <param name="redirect">Source for inheriting values.</param>
    /// <param name="color">Meter colour this storage supplies.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    internal PaletteVuMeterColorRedirect(PaletteRedirect redirect, VuMeterPaletteColor color, NeedPaintHandler? needPaint)
        : base(null, needPaint)
    {
        _inherit = new PaletteVuMeterColorInheritRedirect(redirect, color);
        SetInherit(_inherit);
    }

    /// <summary>
    /// Update the redirector with a new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    internal void SetRedirector(PaletteRedirect redirect) => _inherit.SetRedirector(redirect);
}

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
/// Common analogue-gauge appearance. Other states inherit these values, which themselves inherit the palette.
/// </summary>
public class PaletteGaugeRedirect : Storage
{
    private readonly PaletteDoubleRedirect _chrome;

    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteGaugeRedirect"/> class.
    /// </summary>
    /// <param name="redirect">Source for inheriting values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteGaugeRedirect([DisallowNull] PaletteRedirect redirect, NeedPaintHandler needPaint)
    {
        Debug.Assert(redirect != null);
        NeedPaint = needPaint;
        _chrome = new PaletteDoubleRedirect(redirect!, PaletteBackStyle.PanelClient, PaletteBorderStyle.ControlClient, needPaint);
        Face = new PaletteGaugeColorRedirect(redirect!, GaugePaletteColor.Face, needPaint, _chrome);
        Tick = new PaletteGaugeColorRedirect(redirect!, GaugePaletteColor.Tick, needPaint, null);
        Needle = new PaletteGaugeColorRedirect(redirect!, GaugePaletteColor.Needle, needPaint, null);
        Fill = new PaletteGaugeColorRedirect(redirect!, GaugePaletteColor.Fill, needPaint, null);
        RangeWarning = new PaletteGaugeColorRedirect(redirect!, GaugePaletteColor.RangeWarning, needPaint, null);
        RangeError = new PaletteGaugeColorRedirect(redirect!, GaugePaletteColor.RangeError, needPaint, null);
        Cap = new PaletteGaugeColorRedirect(redirect!, GaugePaletteColor.Cap, needPaint, null);
        Peak = new PaletteGaugeColorRedirect(redirect!, GaugePaletteColor.Peak, needPaint, null);
        SegmentOff = new PaletteGaugeColorRedirect(redirect!, GaugePaletteColor.SegmentOff, needPaint, null);
    }

    /// <inheritdoc />
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => _chrome.IsDefault &&
                                      Face.IsDefault &&
                                      Tick.IsDefault &&
                                      Needle.IsDefault &&
                                      Fill.IsDefault &&
                                      RangeWarning.IsDefault &&
                                      RangeError.IsDefault &&
                                      Cap.IsDefault &&
                                      Peak.IsDefault &&
                                      SegmentOff.IsDefault;

    /// <summary>
    /// Update the redirector with a new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public void SetRedirector(PaletteRedirect redirect)
    {
        _chrome.SetRedirector(redirect);
        Face.SetRedirector(redirect);
        Tick.SetRedirector(redirect);
        Needle.SetRedirector(redirect);
        Fill.SetRedirector(redirect);
        RangeWarning.SetRedirector(redirect);
        RangeError.SetRedirector(redirect);
        Cap.SetRedirector(redirect);
        Peak.SetRedirector(redirect);
        SegmentOff.SetRedirector(redirect);
    }

    /// <summary>
    /// Gets the background drawn behind the face.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Background drawn behind the gauge face.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack Back => _chrome.Back;

    private bool ShouldSerializeBack() => !_chrome.Back.IsDefault;

    /// <summary>
    /// Gets the border drawn around the control chrome.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Border drawn around the control chrome.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBorder Border => _chrome.Border;

    private bool ShouldSerializeBorder() => !_chrome.Border.IsDefault;

    /// <summary>
    /// Gets or sets the background style drawn behind the face.
    /// </summary>
    internal PaletteBackStyle BackStyle
    {
        get => _chrome.BackStyle;
        set => _chrome.BackStyle = value;
    }

    /// <summary>
    /// Gets the face fill (<see cref="PaletteElementColor.Color1"/>) and border (<see cref="PaletteElementColor.Color2"/>).
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Face fill (Color1) and border (Color2).")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteGaugeColorRedirect Face { get; }

    private bool ShouldSerializeFace() => !Face.IsDefault;

    /// <summary>
    /// Gets the scale tick and label colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Scale ticks and labels.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteGaugeColorRedirect Tick { get; }

    private bool ShouldSerializeTick() => !Tick.IsDefault;

    /// <summary>
    /// Gets the needle / primary indicator colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Needle or primary indicator.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteGaugeColorRedirect Needle { get; }

    private bool ShouldSerializeNeedle() => !Needle.IsDefault;

    /// <summary>
    /// Gets the value fill colour used by linear and rectangular faces.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Value fill for linear and rectangular faces.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteGaugeColorRedirect Fill { get; }

    private bool ShouldSerializeFill() => !Fill.IsDefault;

    /// <summary>
    /// Gets the warning range colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Warning range band.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteGaugeColorRedirect RangeWarning { get; }

    private bool ShouldSerializeRangeWarning() => !RangeWarning.IsDefault;

    /// <summary>
    /// Gets the error range colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Error range band.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteGaugeColorRedirect RangeError { get; }

    private bool ShouldSerializeRangeError() => !RangeError.IsDefault;

    /// <summary>
    /// Gets the circular center-cap colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Circular center cap.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteGaugeColorRedirect Cap { get; }

    private bool ShouldSerializeCap() => !Cap.IsDefault;

    /// <summary>
    /// Gets the peak-marker colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Peak marker.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteGaugeColorRedirect Peak { get; }

    private bool ShouldSerializePeak() => !Peak.IsDefault;

    /// <summary>
    /// Gets the colour of unlit rectangular segments.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Unlit rectangular segments.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteGaugeColorRedirect SegmentOff { get; }

    private bool ShouldSerializeSegmentOff() => !SegmentOff.IsDefault;
}

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
/// Common meter appearance. Other states inherit these values, which themselves inherit the palette.
/// </summary>
public class PaletteVuMeterRedirect : Storage
{
    private readonly PaletteDoubleRedirect _chrome;

    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteVuMeterRedirect"/> class.
    /// </summary>
    /// <param name="redirect">Source for inheriting values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteVuMeterRedirect([DisallowNull] PaletteRedirect redirect, NeedPaintHandler needPaint)
    {
        Debug.Assert(redirect != null);
        NeedPaint = needPaint;
        _chrome = new PaletteDoubleRedirect(redirect!, PaletteBackStyle.PanelClient, PaletteBorderStyle.ControlClient, needPaint);
        Face = new PaletteVuMeterColorRedirect(redirect!, VuMeterPaletteColor.Face, needPaint);
        Tick = new PaletteVuMeterColorRedirect(redirect!, VuMeterPaletteColor.Tick, needPaint);
        Needle = new PaletteVuMeterColorRedirect(redirect!, VuMeterPaletteColor.Needle, needPaint);
        Peak = new PaletteVuMeterColorRedirect(redirect!, VuMeterPaletteColor.Peak, needPaint);
        SegmentLow = new PaletteVuMeterColorRedirect(redirect!, VuMeterPaletteColor.SegmentLow, needPaint);
        SegmentHigh = new PaletteVuMeterColorRedirect(redirect!, VuMeterPaletteColor.SegmentHigh, needPaint);
        SegmentOff = new PaletteVuMeterColorRedirect(redirect!, VuMeterPaletteColor.SegmentOff, needPaint);
    }

    /// <inheritdoc />
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => _chrome.IsDefault &&
                                      Face.IsDefault &&
                                      Tick.IsDefault &&
                                      Needle.IsDefault &&
                                      Peak.IsDefault &&
                                      SegmentLow.IsDefault &&
                                      SegmentHigh.IsDefault &&
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
        Peak.SetRedirector(redirect);
        SegmentLow.SetRedirector(redirect);
        SegmentHigh.SetRedirector(redirect);
        SegmentOff.SetRedirector(redirect);
    }

    /// <summary>
    /// Gets the background drawn behind the face.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Background drawn behind the meter face.")]
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
    /// Gets the dial fill (<see cref="PaletteElementColor.Color1"/>) and border (<see cref="PaletteElementColor.Color2"/>).
    /// Defaults to the palette input-control back and border.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Dial fill (Color1) and border (Color2). Defaults to the palette input-control back and border.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteVuMeterColorRedirect Face { get; }

    private bool ShouldSerializeFace() => !Face.IsDefault;

    /// <summary>
    /// Gets the scale tick and label colour. Defaults to the palette control label text colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Scale ticks and labels. Defaults to the palette control label text colour.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteVuMeterColorRedirect Tick { get; }

    private bool ShouldSerializeTick() => !Tick.IsDefault;

    /// <summary>
    /// Gets the live needle colour. Defaults to the palette control label text colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Live needle. Defaults to the palette control label text colour.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteVuMeterColorRedirect Needle { get; }

    private bool ShouldSerializeNeedle() => !Needle.IsDefault;

    /// <summary>
    /// Gets the peak-hold colour. Defaults to the palette checked dock-header colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Peak hold. Defaults to the palette checked dock-header colour.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteVuMeterColorRedirect Peak { get; }

    private bool ShouldSerializePeak() => !Peak.IsDefault;

    /// <summary>
    /// Gets the colour of lit segments below 0 VU. Defaults to the palette control label text colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Lit segments below 0 VU. Defaults to the palette control label text colour.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteVuMeterColorRedirect SegmentLow { get; }

    private bool ShouldSerializeSegmentLow() => !SegmentLow.IsDefault;

    /// <summary>
    /// Gets the overload colour. Defaults to the palette pressed-link colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overload arc and lit segments at 0 VU and above. Defaults to the palette pressed-link colour.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteVuMeterColorRedirect SegmentHigh { get; }

    private bool ShouldSerializeSegmentHigh() => !SegmentHigh.IsDefault;

    /// <summary>
    /// Gets the colour of segments that are off. Defaults to the palette standalone button colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Segments that are off. Defaults to the palette standalone button colour.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteVuMeterColorRedirect SegmentOff { get; }

    private bool ShouldSerializeSegmentOff() => !SegmentOff.IsDefault;
}

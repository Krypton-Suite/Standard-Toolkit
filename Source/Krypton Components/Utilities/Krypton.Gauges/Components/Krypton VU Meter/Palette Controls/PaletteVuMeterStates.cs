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
/// Meter appearance for one state. Unset colours inherit from <see cref="PaletteVuMeterRedirect"/>.
/// </summary>
public class PaletteVuMeterStates : Storage
{
    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteVuMeterStates"/> class.
    /// </summary>
    /// <param name="redirect">Common appearance to inherit.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteVuMeterStates(PaletteVuMeterRedirect redirect, NeedPaintHandler needPaint)
    {
        NeedPaint = needPaint;
        Face = new PaletteElementColor(redirect.Face, needPaint);
        Tick = new PaletteElementColor(redirect.Tick, needPaint);
        Needle = new PaletteElementColor(redirect.Needle, needPaint);
        Peak = new PaletteElementColor(redirect.Peak, needPaint);
        SegmentLow = new PaletteElementColor(redirect.SegmentLow, needPaint);
        SegmentHigh = new PaletteElementColor(redirect.SegmentHigh, needPaint);
        SegmentOff = new PaletteElementColor(redirect.SegmentOff, needPaint);
    }

    /// <inheritdoc />
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => Face.IsDefault &&
                                      Tick.IsDefault &&
                                      Needle.IsDefault &&
                                      Peak.IsDefault &&
                                      SegmentLow.IsDefault &&
                                      SegmentHigh.IsDefault &&
                                      SegmentOff.IsDefault;

    /// <summary>
    /// Gets the dial fill (<see cref="PaletteElementColor.Color1"/>) and border (<see cref="PaletteElementColor.Color2"/>).
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Dial fill (Color1) and border (Color2).")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor Face { get; }

    private bool ShouldSerializeFace() => !Face.IsDefault;

    /// <summary>
    /// Gets the scale tick and label colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Scale ticks and labels.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor Tick { get; }

    private bool ShouldSerializeTick() => !Tick.IsDefault;

    /// <summary>
    /// Gets the live needle colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Live needle.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor Needle { get; }

    private bool ShouldSerializeNeedle() => !Needle.IsDefault;

    /// <summary>
    /// Gets the peak-hold colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Peak hold.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor Peak { get; }

    private bool ShouldSerializePeak() => !Peak.IsDefault;

    /// <summary>
    /// Gets the colour of lit segments below 0 VU.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Lit segments below 0 VU.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor SegmentLow { get; }

    private bool ShouldSerializeSegmentLow() => !SegmentLow.IsDefault;

    /// <summary>
    /// Gets the overload colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overload arc and lit segments at 0 VU and above.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor SegmentHigh { get; }

    private bool ShouldSerializeSegmentHigh() => !SegmentHigh.IsDefault;

    /// <summary>
    /// Gets the colour of segments that are off.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Segments that are off.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor SegmentOff { get; }

    private bool ShouldSerializeSegmentOff() => !SegmentOff.IsDefault;
}

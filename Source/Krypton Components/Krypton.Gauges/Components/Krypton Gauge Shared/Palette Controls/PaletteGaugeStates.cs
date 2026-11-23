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
/// Gauge appearance for one state. Unset colours inherit from <see cref="PaletteGaugeRedirect"/>.
/// </summary>
public class PaletteGaugeStates : Storage
{
    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteGaugeStates"/> class.
    /// </summary>
    /// <param name="redirect">Common appearance to inherit.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteGaugeStates(PaletteGaugeRedirect redirect, NeedPaintHandler needPaint)
    {
        NeedPaint = needPaint;
        Face = new PaletteElementColor(redirect.Face, needPaint);
        Tick = new PaletteElementColor(redirect.Tick, needPaint);
        Needle = new PaletteElementColor(redirect.Needle, needPaint);
        Fill = new PaletteElementColor(redirect.Fill, needPaint);
        RangeWarning = new PaletteElementColor(redirect.RangeWarning, needPaint);
        RangeError = new PaletteElementColor(redirect.RangeError, needPaint);
        Cap = new PaletteElementColor(redirect.Cap, needPaint);
        Peak = new PaletteElementColor(redirect.Peak, needPaint);
        SegmentOff = new PaletteElementColor(redirect.SegmentOff, needPaint);
    }

    /// <inheritdoc />
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => Face.IsDefault &&
                                      Tick.IsDefault &&
                                      Needle.IsDefault &&
                                      Fill.IsDefault &&
                                      RangeWarning.IsDefault &&
                                      RangeError.IsDefault &&
                                      Cap.IsDefault &&
                                      Peak.IsDefault &&
                                      SegmentOff.IsDefault;

    /// <summary>
    /// Gets the face fill (<see cref="PaletteElementColor.Color1"/>) and border (<see cref="PaletteElementColor.Color2"/>).
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Face fill (Color1) and border (Color2).")]
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
    /// Gets the needle / primary indicator colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Needle or primary indicator.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor Needle { get; }

    private bool ShouldSerializeNeedle() => !Needle.IsDefault;

    /// <summary>
    /// Gets the value fill colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Value fill.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor Fill { get; }

    private bool ShouldSerializeFill() => !Fill.IsDefault;

    /// <summary>
    /// Gets the warning range colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Warning range band.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor RangeWarning { get; }

    private bool ShouldSerializeRangeWarning() => !RangeWarning.IsDefault;

    /// <summary>
    /// Gets the error range colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Error range band.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor RangeError { get; }

    private bool ShouldSerializeRangeError() => !RangeError.IsDefault;

    /// <summary>
    /// Gets the circular center-cap colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Circular center cap.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor Cap { get; }

    private bool ShouldSerializeCap() => !Cap.IsDefault;

    /// <summary>
    /// Gets the peak-marker colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Peak marker.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor Peak { get; }

    private bool ShouldSerializePeak() => !Peak.IsDefault;

    /// <summary>
    /// Gets the colour of unlit rectangular segments.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Unlit rectangular segments.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor SegmentOff { get; }

    private bool ShouldSerializeSegmentOff() => !SegmentOff.IsDefault;
}

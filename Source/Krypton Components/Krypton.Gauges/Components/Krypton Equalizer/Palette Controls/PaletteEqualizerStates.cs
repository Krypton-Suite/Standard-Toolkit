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
/// Equalizer appearance for one state. Unset colours inherit from <see cref="PaletteEqualizerRedirect"/>.
/// </summary>
public class PaletteEqualizerStates : Storage
{
    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteEqualizerStates"/> class.
    /// </summary>
    /// <param name="redirect">Common appearance to inherit.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteEqualizerStates(PaletteEqualizerRedirect redirect, NeedPaintHandler needPaint)
    {
        NeedPaint = needPaint;
        Face = new PaletteElementColor(redirect.Face, needPaint);
        Label = new PaletteElementColor(redirect.Label, needPaint);
        Low = new PaletteElementColor(redirect.Low, needPaint);
        Mid = new PaletteElementColor(redirect.Mid, needPaint);
        High = new PaletteElementColor(redirect.High, needPaint);
        Off = new PaletteElementColor(redirect.Off, needPaint);
        Peak = new PaletteElementColor(redirect.Peak, needPaint);
        Thumb = new PaletteElementColor(redirect.Thumb, needPaint);
    }

    /// <inheritdoc />
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => Face.IsDefault &&
                                      Label.IsDefault &&
                                      Low.IsDefault &&
                                      Mid.IsDefault &&
                                      High.IsDefault &&
                                      Off.IsDefault &&
                                      Peak.IsDefault &&
                                      Thumb.IsDefault;

    /// <summary>Gets the face fill and border colours.</summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Face fill (Color1) and border (Color2).")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor Face { get; }

    private bool ShouldSerializeFace() => !Face.IsDefault;

    /// <summary>Gets the band label colour.</summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Band frequency labels.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor Label { get; }

    private bool ShouldSerializeLabel() => !Label.IsDefault;

    /// <summary>Gets the low-range bar colour.</summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Lit bars in the lower third of the scale.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor Low { get; }

    private bool ShouldSerializeLow() => !Low.IsDefault;

    /// <summary>Gets the mid-range bar colour.</summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Lit bars in the middle third of the scale.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor Mid { get; }

    private bool ShouldSerializeMid() => !Mid.IsDefault;

    /// <summary>Gets the high-range bar colour.</summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Lit bars in the upper third of the scale.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor High { get; }

    private bool ShouldSerializeHigh() => !High.IsDefault;

    /// <summary>Gets the unlit segment colour.</summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Unlit segments.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor Off { get; }

    private bool ShouldSerializeOff() => !Off.IsDefault;

    /// <summary>Gets the peak-hold colour.</summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Peak hold marks.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor Peak { get; }

    private bool ShouldSerializePeak() => !Peak.IsDefault;

    /// <summary>Gets the interactive gain thumb colour.</summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Interactive gain thumbs.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor Thumb { get; }

    private bool ShouldSerializeThumb() => !Thumb.IsDefault;
}

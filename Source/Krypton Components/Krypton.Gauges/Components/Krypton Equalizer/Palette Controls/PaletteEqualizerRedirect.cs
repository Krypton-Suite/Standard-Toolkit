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
/// Common equalizer appearance. Other states inherit these values, which themselves inherit the palette.
/// </summary>
public class PaletteEqualizerRedirect : Storage
{
    private readonly PaletteDoubleRedirect _chrome;

    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteEqualizerRedirect"/> class.
    /// </summary>
    /// <param name="redirect">Source for inheriting values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteEqualizerRedirect([DisallowNull] PaletteRedirect redirect, NeedPaintHandler needPaint)
    {
        Debug.Assert(redirect != null);
        NeedPaint = needPaint;
        _chrome = new PaletteDoubleRedirect(redirect!, PaletteBackStyle.PanelClient, PaletteBorderStyle.ControlClient, needPaint);
        Face = new PaletteEqualizerColorRedirect(redirect!, EqualizerPaletteColor.Face, needPaint, _chrome);
        Label = new PaletteEqualizerColorRedirect(redirect!, EqualizerPaletteColor.Label, needPaint, null);
        Low = new PaletteEqualizerColorRedirect(redirect!, EqualizerPaletteColor.Low, needPaint, null);
        Mid = new PaletteEqualizerColorRedirect(redirect!, EqualizerPaletteColor.Mid, needPaint, null);
        High = new PaletteEqualizerColorRedirect(redirect!, EqualizerPaletteColor.High, needPaint, null);
        Off = new PaletteEqualizerColorRedirect(redirect!, EqualizerPaletteColor.Off, needPaint, null);
        Peak = new PaletteEqualizerColorRedirect(redirect!, EqualizerPaletteColor.Peak, needPaint, null);
        Thumb = new PaletteEqualizerColorRedirect(redirect!, EqualizerPaletteColor.Thumb, needPaint, null);
    }

    /// <inheritdoc />
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => _chrome.IsDefault &&
                                      Face.IsDefault &&
                                      Label.IsDefault &&
                                      Low.IsDefault &&
                                      Mid.IsDefault &&
                                      High.IsDefault &&
                                      Off.IsDefault &&
                                      Peak.IsDefault &&
                                      Thumb.IsDefault;

    /// <summary>
    /// Update the redirector with a new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public void SetRedirector(PaletteRedirect redirect)
    {
        _chrome.SetRedirector(redirect);
        Face.SetRedirector(redirect);
        Label.SetRedirector(redirect);
        Low.SetRedirector(redirect);
        Mid.SetRedirector(redirect);
        High.SetRedirector(redirect);
        Off.SetRedirector(redirect);
        Peak.SetRedirector(redirect);
        Thumb.SetRedirector(redirect);
    }

    /// <summary>
    /// Gets the background drawn behind the face.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Background drawn behind the equalizer face.")]
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
    public PaletteEqualizerColorRedirect Face { get; }

    private bool ShouldSerializeFace() => !Face.IsDefault;

    /// <summary>
    /// Gets the band label colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Band frequency labels.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteEqualizerColorRedirect Label { get; }

    private bool ShouldSerializeLabel() => !Label.IsDefault;

    /// <summary>
    /// Gets the low-range bar colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Lit bars in the lower third of the scale.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteEqualizerColorRedirect Low { get; }

    private bool ShouldSerializeLow() => !Low.IsDefault;

    /// <summary>
    /// Gets the mid-range bar colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Lit bars in the middle third of the scale.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteEqualizerColorRedirect Mid { get; }

    private bool ShouldSerializeMid() => !Mid.IsDefault;

    /// <summary>
    /// Gets the high-range bar colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Lit bars in the upper third of the scale.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteEqualizerColorRedirect High { get; }

    private bool ShouldSerializeHigh() => !High.IsDefault;

    /// <summary>
    /// Gets the unlit segment colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Unlit segments.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteEqualizerColorRedirect Off { get; }

    private bool ShouldSerializeOff() => !Off.IsDefault;

    /// <summary>
    /// Gets the peak-hold colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Peak hold marks.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteEqualizerColorRedirect Peak { get; }

    private bool ShouldSerializePeak() => !Peak.IsDefault;

    /// <summary>
    /// Gets the interactive gain thumb colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Interactive gain thumbs.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteEqualizerColorRedirect Thumb { get; }

    private bool ShouldSerializeThumb() => !Thumb.IsDefault;
}

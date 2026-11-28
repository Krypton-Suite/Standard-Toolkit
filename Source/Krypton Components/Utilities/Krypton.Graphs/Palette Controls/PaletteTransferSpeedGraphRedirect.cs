#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Graphs;

/// <summary>
/// Common transfer-speed graph appearance. Other states inherit these values, which themselves inherit the palette.
/// </summary>
public class PaletteTransferSpeedGraphRedirect : Storage
{
    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteTransferSpeedGraphRedirect"/> class.
    /// </summary>
    /// <param name="redirect">Source for inheriting values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    /// <param name="element">Palette element that owns the five graph colours.</param>
    public PaletteTransferSpeedGraphRedirect(PaletteRedirect redirect, NeedPaintHandler needPaint, PaletteElement element = PaletteElement.TransferSpeedGraph)
    {
        Debug.Assert(redirect != null);
        NeedPaint = needPaint;
        Plot = new PaletteTransferSpeedGraphColorRedirect(redirect!, TransferSpeedGraphPaletteColor.Plot, needPaint, element);
        Series = new PaletteTransferSpeedGraphColorRedirect(redirect!, TransferSpeedGraphPaletteColor.Series, needPaint, element);
        Grid = new PaletteTransferSpeedGraphColorRedirect(redirect!, TransferSpeedGraphPaletteColor.Grid, needPaint, element);
        Border = new PaletteTransferSpeedGraphColorRedirect(redirect!, TransferSpeedGraphPaletteColor.Border, needPaint, element);
        Label = new PaletteTransferSpeedGraphColorRedirect(redirect!, TransferSpeedGraphPaletteColor.Label, needPaint, element);
    }

    /// <inheritdoc />
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault =>
        Plot.IsDefault &&
        Series.IsDefault &&
        Grid.IsDefault &&
        Border.IsDefault &&
        Label.IsDefault;

    /// <summary>
    /// Update the redirector with a new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public void SetRedirector(PaletteRedirect redirect)
    {
        Plot.SetRedirector(redirect);
        Series.SetRedirector(redirect);
        Grid.SetRedirector(redirect);
        Border.SetRedirector(redirect);
        Label.SetRedirector(redirect);
    }

    /// <summary>
    /// Gets the plot background colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Plot background. Defaults to PaletteElement.TransferSpeedGraph Color1.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTransferSpeedGraphColorRedirect Plot { get; }

    private bool ShouldSerializePlot() => !Plot.IsDefault;

    /// <summary>
    /// Gets the series fill colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Series fill. Defaults to PaletteElement.TransferSpeedGraph Color2.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTransferSpeedGraphColorRedirect Series { get; }

    private bool ShouldSerializeSeries() => !Series.IsDefault;

    /// <summary>
    /// Gets the grid line colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Grid lines. Defaults to PaletteElement.TransferSpeedGraph Color3.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTransferSpeedGraphColorRedirect Grid { get; }

    private bool ShouldSerializeGrid() => !Grid.IsDefault;

    /// <summary>
    /// Gets the chart frame border colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Chart frame border. Defaults to PaletteElement.TransferSpeedGraph Color4.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTransferSpeedGraphColorRedirect Border { get; }

    private bool ShouldSerializeBorder() => !Border.IsDefault;

    /// <summary>
    /// Gets the speed-label text colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Speed label text. Defaults to PaletteElement.TransferSpeedGraph Color5.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTransferSpeedGraphColorRedirect Label { get; }

    private bool ShouldSerializeLabel() => !Label.IsDefault;
}

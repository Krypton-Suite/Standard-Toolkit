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
/// Transfer-speed graph appearance for one state. Unset colours inherit from <see cref="PaletteTransferSpeedGraphRedirect"/>.
/// </summary>
public class PaletteTransferSpeedGraphStates : Storage
{
    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteTransferSpeedGraphStates"/> class.
    /// </summary>
    /// <param name="redirect">Common appearance to inherit.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteTransferSpeedGraphStates(PaletteTransferSpeedGraphRedirect redirect, NeedPaintHandler needPaint)
    {
        NeedPaint = needPaint;
        Plot = new PaletteElementColor(redirect.Plot, needPaint);
        Series = new PaletteElementColor(redirect.Series, needPaint);
        Grid = new PaletteElementColor(redirect.Grid, needPaint);
        Border = new PaletteElementColor(redirect.Border, needPaint);
        Label = new PaletteElementColor(redirect.Label, needPaint);
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
    /// Gets the plot background colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Plot background.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor Plot { get; }

    private bool ShouldSerializePlot() => !Plot.IsDefault;

    /// <summary>
    /// Gets the series fill colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Series fill.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor Series { get; }

    private bool ShouldSerializeSeries() => !Series.IsDefault;

    /// <summary>
    /// Gets the grid line colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Grid lines.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor Grid { get; }

    private bool ShouldSerializeGrid() => !Grid.IsDefault;

    /// <summary>
    /// Gets the chart frame border colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Chart frame border.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor Border { get; }

    private bool ShouldSerializeBorder() => !Border.IsDefault;

    /// <summary>
    /// Gets the speed-label text colour.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Speed label text.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteElementColor Label { get; }

    private bool ShouldSerializeLabel() => !Label.IsDefault;
}

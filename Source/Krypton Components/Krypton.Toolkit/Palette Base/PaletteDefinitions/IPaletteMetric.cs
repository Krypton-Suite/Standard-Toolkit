namespace Krypton.Toolkit;

/// <summary>
/// Exposes a palette source for acquiring metrics.
/// </summary>
public interface IPaletteMetric
{
    /// <summary>
    /// Gets an integer metric value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>Integer value.</returns>
    int GetMetricInt(PaletteState state, PaletteMetricInt metric);

    /// <summary>
    /// Gets a boolean metric value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>InheritBool value.</returns>
    InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric);

    /// <summary>
    /// Gets a padding metric value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>Padding value.</returns>
    Padding GetMetricPadding(PaletteState state, PaletteMetricPadding metric);
}
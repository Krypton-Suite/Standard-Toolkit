namespace Krypton.Toolkit;

/// <summary>
/// Signature of methods that return a padding metric.
/// </summary>
/// <param name="state">Palette value should be applicable to this state.</param>
/// <param name="metric">Metric value required.</param>
/// <returns>Padding value.</returns>
public delegate Padding GetPaddingMetric(PaletteState state, PaletteMetricPadding metric);
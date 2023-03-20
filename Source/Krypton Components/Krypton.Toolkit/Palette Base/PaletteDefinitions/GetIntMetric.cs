namespace Krypton.Toolkit;

/// <summary>
/// Signature of methods that return an integer metric.
/// </summary>
/// <param name="state">Palette value should be applicable to this state.</param>
/// <param name="metric">Metric value required.</param>
/// <returns>Integer value.</returns>
public delegate int GetIntMetric(PaletteState state, PaletteMetricInt metric);
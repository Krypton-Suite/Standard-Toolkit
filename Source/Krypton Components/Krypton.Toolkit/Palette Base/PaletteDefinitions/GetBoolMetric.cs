namespace Krypton.Toolkit;

/// <summary>
/// Signature of methods that return a bool metric.
/// </summary>
/// <param name="state">Palette value should be applicable to this state.</param>
/// <param name="metric">Metric value required.</param>
/// <returns>InheritBool value.</returns>
public delegate InheritBool GetBoolMetric(PaletteState state, PaletteMetricBool metric);
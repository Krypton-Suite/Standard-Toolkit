namespace Krypton.Toolkit;

/// <summary>
/// Specifies a bool type metric.
/// </summary>
public enum PaletteMetricBool
{
    /// <summary>
    /// Specifies that no bool metric is required.
    /// </summary>
    None,

    /// <summary>
    /// Specifies when the border is drawn for the header group control.
    /// </summary>
    HeaderGroupOverlay,

    /// <summary>
    /// Specifies that split area controls use faded appearance for non-active area.
    /// </summary>
    SplitWithFading,

    /// <summary>
    /// Specifies that the spare tabs area be treated as the application caption area.
    /// </summary>
    RibbonTabsSpareCaption,

    /// <summary>
    /// Specifies if lines are drawn between nodes in the KryptonTreeView.
    /// </summary>
    TreeViewLines
}
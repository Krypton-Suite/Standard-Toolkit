#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Charting;

/// <summary>
/// Status drawn on one step or date-timeline item.
/// The palette still supplies the base colours; the style adds a stripe, hatch, check, or strike.
/// </summary>
public enum TimelineItemStyle
{
    /// <summary>
    /// Palette fill only.
    /// </summary>
    Normal = 0,

    /// <summary>
    /// Forward-diagonal hatch over the palette fill.
    /// </summary>
    Hatched = 1,

    /// <summary>
    /// Completed item: a status stripe and a check mark.
    /// </summary>
    Completed = 2,

    /// <summary>
    /// Delayed item: a status stripe, a backward-diagonal hatch, and a dashed outline.
    /// </summary>
    Delayed = 3,

    /// <summary>
    /// Cancelled item: a muted hatch and a line through the item.
    /// </summary>
    Cancelled = 4
}

/// <summary>
/// How a <see cref="KryptonDateTimeline"/> places items on the time scale.
/// </summary>
public enum DateTimelineStyle
{
    /// <summary>
    /// Task bars and milestone diamonds on <see cref="KryptonDateTimelineItem.Row"/>, with the scale along the top.
    /// </summary>
    Chart = 0,

    /// <summary>
    /// Stacked boxes above a bottom axis, in the style of vis.js Timeline.
    /// A single date is a box with a stem down to the axis. A span is a range box.
    /// Items that overlap in time stack into extra lanes.
    /// </summary>
    Vis = 1
}

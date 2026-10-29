#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Navigator.Utilities;

/// <summary>
/// Progress state applied to the host form taskbar button for the selected navigator page.
/// </summary>
public enum TaskbarProgressState
{
    /// <summary>No progress shown.</summary>
    NoProgress = 0,

    /// <summary>Indeterminate (marquee) progress.</summary>
    Indeterminate = 1,

    /// <summary>Normal progress bar.</summary>
    Normal = 2,

    /// <summary>Error progress bar.</summary>
    Error = 4,

    /// <summary>Paused progress bar.</summary>
    Paused = 8
}

/// <summary>
/// Event data for supplying a host taskbar overlay icon from the selected page.
/// </summary>
public class QueryTaskbarOverlayEventArgs : EventArgs
{
    /// <summary>
    /// Initialize a new instance of the <see cref="QueryTaskbarOverlayEventArgs"/> class.
    /// </summary>
    /// <param name="page">Selected page driving the overlay.</param>
    public QueryTaskbarOverlayEventArgs(KryptonPage page) => Page = page;

    /// <summary>Gets the selected page.</summary>
    public KryptonPage Page { get; }

    /// <summary>
    /// Gets or sets the overlay icon. Ownership remains with the event handler.
    /// </summary>
    public Icon? Icon { get; set; }

    /// <summary>Gets or sets the accessibility description for the overlay.</summary>
    public string? Description { get; set; }
}

/// <summary>
/// Event data for supplying host taskbar progress from the selected page.
/// </summary>
public class QueryTaskbarProgressEventArgs : EventArgs
{
    /// <summary>
    /// Initialize a new instance of the <see cref="QueryTaskbarProgressEventArgs"/> class.
    /// </summary>
    /// <param name="page">Selected page driving progress.</param>
    public QueryTaskbarProgressEventArgs(KryptonPage page) => Page = page;

    /// <summary>Gets the selected page.</summary>
    public KryptonPage Page { get; }

    /// <summary>Gets or sets the progress state.</summary>
    public TaskbarProgressState State { get; set; } = TaskbarProgressState.NoProgress;

    /// <summary>Gets or sets completed progress units.</summary>
    public ulong Completed { get; set; }

    /// <summary>Gets or sets total progress units.</summary>
    public ulong Total { get; set; } = 100;
}

/// <summary>
/// Describes a single thumbnail toolbar button on the host taskbar button.
/// </summary>
public class TaskbarThumbnailButton
{
    /// <summary>Gets or sets the button identifier returned in WM_COMMAND.</summary>
    public uint Id { get; set; }

    /// <summary>Gets or sets the tooltip text.</summary>
    public string? Tooltip { get; set; }

    /// <summary>Gets or sets an optional button icon. Ownership remains with the caller.</summary>
    public Icon? Icon { get; set; }

    /// <summary>Gets or sets Shell thumbnail button flags.</summary>
    public TaskbarThumbnailButtonFlags Flags { get; set; } = TaskbarThumbnailButtonFlags.Enabled;
}

/// <summary>
/// Flags controlling thumbnail toolbar button state.
/// </summary>
[Flags]
public enum TaskbarThumbnailButtonFlags
{
    /// <summary>Button is enabled.</summary>
    Enabled = 0,

    /// <summary>Button is disabled.</summary>
    Disabled = 0x1,

    /// <summary>Dismiss the thumbnail when clicked.</summary>
    DismissOnClick = 0x2,

    /// <summary>Do not draw button background.</summary>
    NoBackground = 0x4,

    /// <summary>Button is hidden.</summary>
    Hidden = 0x8,

    /// <summary>Button is non-interactive.</summary>
    NonInteractive = 0x10
}

/// <summary>
/// Event data for supplying host taskbar thumbnail toolbar buttons from the selected page.
/// </summary>
public class QueryTaskbarThumbnailButtonsEventArgs : EventArgs
{
    /// <summary>
    /// Initialize a new instance of the <see cref="QueryTaskbarThumbnailButtonsEventArgs"/> class.
    /// </summary>
    /// <param name="page">Selected page driving the buttons.</param>
    public QueryTaskbarThumbnailButtonsEventArgs(KryptonPage page)
    {
        Page = page;
        Buttons = new List<TaskbarThumbnailButton>();
    }

    /// <summary>Gets the selected page.</summary>
    public KryptonPage Page { get; }

    /// <summary>
    /// Gets the buttons to show (maximum 7). Ownership of icons remains with the caller.
    /// </summary>
    public IList<TaskbarThumbnailButton> Buttons { get; }
}

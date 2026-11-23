#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Details for a hyperlink click inside a <see cref="KryptonToolTip"/>.
/// </summary>
public class ToolTipLinkClickedEventArgs : EventArgs
{
    /// <summary>
    /// Initialize a new instance of the <see cref="ToolTipLinkClickedEventArgs"/> class.
    /// </summary>
    /// <param name="target">Control that owns the tooltip.</param>
    /// <param name="url">Address associated with the link.</param>
    public ToolTipLinkClickedEventArgs(Control target, string url)
    {
        Target = target ?? ThrowHelper.ThrowArgumentNullException<Control>(target);
        Url = url ?? string.Empty;
    }

    /// <summary>
    /// Gets the control that displayed the tooltip.
    /// </summary>
    public Control Target { get; }

    /// <summary>
    /// Gets the URL associated with the link.
    /// </summary>
    public string Url { get; }

    /// <summary>
    /// Gets or sets a value indicating whether the default shell open should be skipped.
    /// </summary>
    public bool Cancel { get; set; }
}

#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Navigator.Utilities;

/// <summary>
/// Provides access to the built-in caption-tab context menu before it is shown.
/// </summary>
public sealed class NavigatorTabContextMenuEventArgs : CancelEventArgs
{
    public NavigatorTabContextMenuEventArgs(KryptonPage page, ContextMenuStrip contextMenuStrip)
    {
        Page = page ?? ThrowHelper.ThrowArgumentNullException<KryptonPage>(nameof(page));
        ContextMenuStrip = contextMenuStrip ?? ThrowHelper.ThrowArgumentNullException<ContextMenuStrip>(nameof(contextMenuStrip));
    }

    /// <summary>
    /// Gets the page the user right-clicked.
    /// </summary>
    public KryptonPage Page { get; }

    /// <summary>
    /// Gets the menu that will be shown. Handlers can add/remove items.
    /// </summary>
    public ContextMenuStrip ContextMenuStrip { get; }
}

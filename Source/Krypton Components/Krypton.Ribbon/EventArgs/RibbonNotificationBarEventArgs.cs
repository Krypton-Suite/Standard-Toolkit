#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Provides data for notification bar events.
/// </summary>
public class RibbonNotificationBarEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the RibbonNotificationBarEventArgs class.
    /// </summary>
    /// <param name="actionButtonIndex">The index of the action button that was clicked, or -1 if close button was clicked.</param>
    public RibbonNotificationBarEventArgs(int actionButtonIndex)
    {
        ActionButtonIndex = actionButtonIndex;
    }

    /// <summary>
    /// Gets the index of the action button that was clicked, or -1 if the close button was clicked.
    /// </summary>
    public int ActionButtonIndex { get; }
}
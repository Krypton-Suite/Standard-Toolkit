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
/// Controller for handling mouse interactions with the notification bar background.
/// Note: Individual buttons have their own controllers and handle their own hover states.
/// </summary>
internal class NotificationBarController : ButtonController
{
    public NotificationBarController(ViewDrawRibbonNotificationBar notificationBar, NeedPaintHandler? needPaint)
        : base(notificationBar, needPaint!)
    {
        // Buttons now handle their own hover states, so this controller
        // is mainly for the background area if needed in the future
    }
}
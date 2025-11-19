#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Handles the heavy lifting for the <see cref="KryptonMessageBox"/>.</summary>
internal class KryptonMessageBoxController
{
    #region WinForm Compatibility
    private static IWin32Window? ValidateOptions(IWin32Window? owner, MessageBoxOptions options, HelpInfo? helpInfo)
    {
        // Check if trying to show a message box from a non-interactive process, this is not possible
        if (!SystemInformation.UserInteractive &&
            ((options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) == 0))
        {
            throw new InvalidOperationException("Cannot show modal dialog when non-interactive");
        }

        // Check if trying to show a message box from a service and the owner has been specified, this is not possible
        if ((owner != null) &&
            ((options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) != 0))
        {
            throw new ArgumentException(@"Cannot show message box from a service with an owner specified", nameof(options));
        }

        // Check if trying to show a message box from a service and help information is specified, this is not possible
        if ((helpInfo != null) &&
            ((options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) != 0))
        {
            throw new ArgumentException(@"Cannot show message box from a service with help specified", nameof(options));
        }

        IWin32Window? showOwner = null;
        if ((helpInfo != null) ||
            ((options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) == 0))
        {
            // If do not have an owner passed in? then get the active window and use that instead
            showOwner = owner ?? Control.FromHandle(PI.GetActiveWindow());
        }

        return showOwner;
    }
    #endregion
}
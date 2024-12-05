#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>Handles the heavy lifting for the <see cref="KryptonMessageBox"/>.</summary>
    internal class KryptonMessageBoxController
    {
        #region Implementation

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file, HelpNavigator, and Help topic.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.KryptonMessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the KryptonMessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpInfo">Contains the help data of the <see cref="KryptonMessageBox"/>.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
        /// <param name="showHelpButton">Displays a 'Help' button, as seen in .NET 6 and higher.</param>
        /// <param name="applicationImage">The image of the application.</param>
        /// <param name="applicationPath">The application path. To be used in conjunction with <see cref="T:KryptonMessageBoxIcon.Application"/> type.</param>
        /// <param name="contentAreaType">Specifies the <see cref="T:MessageBoxContentAreaType"/>.</param>
        /// <param name="linkLabelCommand">Specifies a <see cref="T:KryptonCommand"/> if using the <see cref="T:MessageBoxContentAreaType.LinkLabel"/> type.</param>
        /// <param name="linkLaunchArgument">Specifies the <see cref="ProcessStartInfo"/> if a <paramref name="linkLabelCommand"> has not been defined.</paramref></param>
        /// <param name="contentLinkArea">Specifies the area within the <see cref="KryptonLinkWrapLabel"/> to be regarded as a link. See <see cref="LinkArea"/>.</param>
        /// <param name="forceUseOfOperatingSystemIcons">If set to true, the <see cref="VisualMessageBoxForm"/> will use standard operating system icons.</param>
        /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        [Obsolete("Please use `KryptonTaskDialog`. Will be removed in V100")]
        public static DialogResult ShowCore(IWin32Window? owner,
                                             string? text, string? caption,
                                             KryptonMessageBoxButtons buttons,
                                             KryptonMessageBoxIcon icon,
                                             KryptonMessageBoxDefaultButton defaultButton,
                                             MessageBoxOptions options,
                                             HelpInfo? helpInfo, bool? showCtrlCopy,
                                             bool? showHelpButton,
                                             Image? applicationImage, string? applicationPath,
                                             MessageBoxContentAreaType? contentAreaType,
                                             KryptonCommand? linkLabelCommand,
                                             ProcessStartInfo? linkLaunchArgument,
                                             LinkArea? contentLinkArea,
                                             bool? forceUseOfOperatingSystemIcons,
                                             bool? showCloseButton)
        {
            caption = string.IsNullOrEmpty(caption) ? @" " : caption;

            IWin32Window? showOwner = ValidateOptions(owner, options, helpInfo);

            // Show message box window as a modal dialog and then dispose of it after-wards

            if (options is MessageBoxOptions.RightAlign or MessageBoxOptions.RtlReading)
            {
                using var kmbrtl = new VisualMessageBoxRtlAwareFormDep(showOwner, text, caption, buttons, icon,
                    defaultButton, helpInfo, showCtrlCopy, showHelpButton, applicationImage, applicationPath,
                    contentAreaType, linkLabelCommand,
                    linkLaunchArgument, contentLinkArea,
                    forceUseOfOperatingSystemIcons, showCloseButton);

                kmbrtl.StartPosition = showOwner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;

                return kmbrtl.ShowDialog(showOwner);
            }
            else
            {
                using var kmb = new VisualMessageBoxFormDep(showOwner, text, caption, buttons, icon,
                    defaultButton, helpInfo, showCtrlCopy, showHelpButton, applicationImage, applicationPath,
                    contentAreaType, linkLabelCommand,
                    linkLaunchArgument, contentLinkArea,
                    forceUseOfOperatingSystemIcons, showCloseButton);

                kmb.StartPosition = showOwner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;

                return kmb.ShowDialog(showOwner);
            }
        }

        [Obsolete("Please use `KryptonTaskDialog`. Will be removed in V100")]
        public static DialogResult ShowCore(KryptonMessageBoxDataDep messageBoxData)
        {
            messageBoxData.Caption = string.IsNullOrEmpty(messageBoxData.Caption) ? @" " : messageBoxData.Caption;

            IWin32Window? showOwner = ValidateOptions(messageBoxData.Owner, messageBoxData.Options, messageBoxData.HelpInfo);

            if (messageBoxData.Options is MessageBoxOptions.RightAlign or MessageBoxOptions.RtlReading)
            {
                using var kmbrtl = new VisualMessageBoxRtlAwareFormDep(messageBoxData);

                kmbrtl.StartPosition = showOwner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;

                return kmbrtl.ShowDialog(showOwner);
            }
            else
            {
                using var kmb = new VisualMessageBoxFormDep(messageBoxData);

                kmb.StartPosition = showOwner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;

                return kmb.ShowDialog(showOwner);
            }
        }

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

        #endregion
    }
}
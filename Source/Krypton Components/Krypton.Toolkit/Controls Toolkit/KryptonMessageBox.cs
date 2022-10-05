// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMethodReturnValue.Global

namespace Krypton.Toolkit
{
    /// <summary>
    /// Displays a message box that can contain text, buttons, and symbols that inform and instruct the user.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory(@"code")]
    public static class KryptonMessageBox
    {

        #region Public

        /// <summary>
        /// Displays a message box in front+center of the application and with the specified text, caption and buttons.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, bool? showCtrlCopy = null) =>
            InternalShow(null, text, caption, MessageBoxButtons.OK, KryptonMessageBoxIcon.None,
                KryptonMessageBoxDefaultButton.Button4,
                0, null, showCtrlCopy, null, null, @"", null);

        /// <summary>
        /// Displays a message box in front+center of the application and with the specified text, caption and buttons.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, bool? showCtrlCopy = null) =>
            InternalShow(null, text, @"", MessageBoxButtons.OK, KryptonMessageBoxIcon.None,
                KryptonMessageBoxDefaultButton.Button4,
                0, null, showCtrlCopy, false, null, @"", null);

        /// <summary>
        /// Displays a message box in front+center of the specified object and with the specified text, caption, buttons, icon, default button, and options.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, bool? showCtrlCopy = null) =>
            InternalShow(owner, text, @"", MessageBoxButtons.OK, KryptonMessageBoxIcon.None,
                KryptonMessageBoxDefaultButton.Button4,
                0, null, showCtrlCopy, false, null, @"", null);

        /// <summary>
        /// Displays a message box in front+center of the specified object and with the specified text, caption, buttons, icon, default button, and options.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption, bool? showCtrlCopy = null) =>
            InternalShow(owner, text, caption, MessageBoxButtons.OK, KryptonMessageBoxIcon.None,
                KryptonMessageBoxDefaultButton.Button4,
                0, null, showCtrlCopy, false, null, @"", null);

        /// <summary>
        /// Displays a message box in front+center of the application and with the specified text, caption and buttons.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons,
            bool? showCtrlCopy = null) =>
                                       InternalShow(null, text, caption, buttons, KryptonMessageBoxIcon.None, 
                                           KryptonMessageBoxDefaultButton.Button1, 0,
                                           new HelpInfo(@"", 0, null), showCtrlCopy,
                                           null, null, @"", null);

        /// <summary>
        /// Displays a message box in front+center of the application and with the specified text, caption, buttons, icon, default button, and options.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon" >One of the KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton" default="KryptonMessageBoxDefaultButton.Button4">One of the KryptonMessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options" default="0">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="displayHelpButton" default="false">Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="showHelpButton">Displays a 'Help' button, as seen in .NET 6 and higher.</param>
        /// <param name="showActionButton">Shows the optional action button.</param>
        /// <param name="actionButtonText">The action button text.</param>
        /// <param name="actionButtonCommand">The <see cref="KryptonCommand"/> attached to the action button.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text,
            string caption,
            MessageBoxButtons buttons,
            KryptonMessageBoxIcon icon,
            KryptonMessageBoxDefaultButton defaultButton = KryptonMessageBoxDefaultButton.Button4,
            MessageBoxOptions options = 0,
            bool displayHelpButton = false,
            bool? showCtrlCopy = null,
            bool? showHelpButton = null,
            bool? showActionButton = null,
            string actionButtonText = @"",
            KryptonCommand actionButtonCommand = null) 
            =>
                InternalShow(null, text, caption, buttons, icon, defaultButton, options,
                             displayHelpButton ? new HelpInfo() : null, showCtrlCopy, 
                             showHelpButton, showActionButton,
                             actionButtonText, actionButtonCommand);


        /// <summary>
        /// Displays a message box in front+center of the specified object and with the specified text, caption, buttons, icon, default button, and options.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box. default="string.Empty"</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton" default="KryptonMessageBoxDefaultButton.Button4">One of the KryptonMessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options" default="0">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="displayHelpButton" default="false">Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="showHelpButton">Displays a 'Help' button, as seen in .NET 6 and higher.</param>
        /// <param name="showActionButton">Shows the optional action button.</param>
        /// <param name="actionButtonText">The action button text.</param>
        /// <param name="actionButtonCommand">The <see cref="KryptonCommand"/> attached to the action button.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, 
            string caption, 
            MessageBoxButtons buttons, 
            KryptonMessageBoxIcon icon, 
            KryptonMessageBoxDefaultButton defaultButton = KryptonMessageBoxDefaultButton.Button4, 
            MessageBoxOptions options = 0, 
            bool displayHelpButton = false,
            bool? showCtrlCopy = null,
            bool? showHelpButton = null,
            bool? showActionButton = null,
            string actionButtonText = @"",
            KryptonCommand actionButtonCommand = null) 
            =>
                InternalShow(owner, text, caption, buttons, icon, defaultButton, options,
                             displayHelpButton ? new HelpInfo() : null, showCtrlCopy, 
                             showHelpButton, showActionButton, actionButtonText,
                             actionButtonCommand);

        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption" >The text to display in the title bar of the message box. default="string.Empty"</param>
        /// <param name="buttons" >One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon" >One of the KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton" >One of the KryptonMessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options" >One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="navigator">One of the System.Windows.Forms.HelpNavigator values.</param>
        /// <param name="param">The numeric ID of the Help topic to display when the user clicks the Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="showHelpButton">Displays a 'Help' button, as seen in .NET 6 and higher.</param>
        /// <param name="showActionButton">Shows the optional action button.</param>
        /// <param name="actionButtonText">The action button text.</param>
        /// <param name="actionButtonCommand">The <see cref="KryptonCommand"/> attached to the action button.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, KryptonMessageBoxIcon icon, KryptonMessageBoxDefaultButton defaultButton, 
            MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param, bool? showCtrlCopy = null,
            bool? showHelpButton = null,
            bool? showActionButton = null,
            string actionButtonText = @"",
            KryptonCommand actionButtonCommand = null)
            => InternalShow(null, text, caption, buttons, icon, defaultButton, options,
                            new HelpInfo(helpFilePath, navigator, param), showCtrlCopy,
                            showHelpButton, showActionButton, actionButtonText,
                            actionButtonCommand);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file, HelpNavigator, and Help topic.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the KryptonMessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="navigator">One of the System.Windows.Forms.HelpNavigator values.</param>
        /// <param name="param">The numeric ID of the Help topic to display when the user clicks the Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="showHelpButton">Displays a 'Help' button, as seen in .NET 6 and higher.</param>
        /// <param name="showActionButton">Shows the optional action button.</param>
        /// <param name="actionButtonText">The action button text.</param>
        /// <param name="actionButtonCommand">The <see cref="KryptonCommand"/> attached to the action button.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, KryptonMessageBoxIcon icon, KryptonMessageBoxDefaultButton defaultButton,
            MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param, bool? showCtrlCopy = null,
            bool? showHelpButton = null,
            bool? showActionButton = null,
            string actionButtonText = @"",
            KryptonCommand actionButtonCommand = null)
            => InternalShow(owner, text, caption, buttons, icon, defaultButton, options,
                            new HelpInfo(helpFilePath, navigator, param), showCtrlCopy, 
                            showHelpButton, showActionButton, actionButtonText,
                            actionButtonCommand);
        #endregion

        #region Implementation
        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file, HelpNavigator, and Help topic.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the KryptonMessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpInfo">Contains the help data of the <see cref="KryptonMessageBox"/>.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="showHelpButton">Displays a 'Help' button, as seen in .NET 6 and higher.</param>
        /// <param name="showActionButton">Shows the optional action button.</param>
        /// <param name="actionButtonText">The action button text.</param>
        /// <param name="actionButtonCommand">The <see cref="KryptonCommand"/> attached to the action button.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        private static DialogResult InternalShow(IWin32Window owner,
                                                 string text, string caption,
                                                 MessageBoxButtons buttons,
                                                 KryptonMessageBoxIcon icon,
                                                 KryptonMessageBoxDefaultButton defaultButton,
                                                 MessageBoxOptions options,
                                                 HelpInfo helpInfo, bool? showCtrlCopy, 
                                                 bool? showHelpButton,
                                                 bool? showActionButton, string actionButtonText,
                                                 KryptonCommand actionButtonCommand)
        {
            caption = string.IsNullOrEmpty(caption) ? @" " : caption;

            IWin32Window showOwner = ValidateOptions(owner, options, helpInfo);

            // Show message box window as a modal dialog and then dispose of it afterwards
            using KryptonMessageBoxForm kmb = new(showOwner, text, caption, buttons, icon, defaultButton, options, helpInfo, showCtrlCopy, showHelpButton, showActionButton, actionButtonText, actionButtonCommand);
            kmb.StartPosition = showOwner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;

            return kmb.ShowDialog(showOwner);
        }

        #region WinForm Compatibility
        private static IWin32Window ValidateOptions(IWin32Window owner, MessageBoxOptions options, HelpInfo helpInfo)
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

            IWin32Window showOwner = null;
            if ((helpInfo != null) ||
                ((options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) == 0))
            {
                // If do not have an owner passed in then get the active window and use that instead
                showOwner = owner ?? Control.FromHandle(PI.GetActiveWindow());
            }

            return showOwner;
        }

        #endregion

        
        #endregion
    }
}

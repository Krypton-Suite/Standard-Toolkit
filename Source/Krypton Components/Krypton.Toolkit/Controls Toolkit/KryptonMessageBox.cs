#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *
 */
#endregion


// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMethodReturnValue.Global

namespace Krypton.Toolkit
{
    /// <summary>
    /// Displays a message box that can contain text, buttons, and symbols that inform and instruct the user.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    public static class KryptonMessageBox
    {

        #region Public

        /// <summary>
        /// Displays a message box in front+center of the application and with the specified text, caption, buttons, icon, default button, and options.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
        /// <param name="buttons" default="MessageBoxButtons.OK">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon" default="MessageBoxIcon.NONE">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton" default="MessageBoxDefaultButton.Button1">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options" default="0">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="displayHelpButton" default="false">Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text,
            string caption = @"",
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.None,
            MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1,
            MessageBoxOptions options = 0,
            bool displayHelpButton = false,
            bool? showCtrlCopy = null)
            =>
                InternalShow(null, text, caption, buttons, icon, defaultButton, options, displayHelpButton ? new HelpInfo() : null, showCtrlCopy);

        /// <summary>
        /// Displays a message box in front+center of the application and with the specified text, caption, buttons, icon, default button, and options.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon" >One of the KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton" default="MessageBoxDefaultButton.Button1">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options" default="0">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="displayHelpButton" default="false">Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text,
            string caption,
            MessageBoxButtons buttons,
            KryptonMessageBoxIcon icon,
            MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1,
            MessageBoxOptions options = 0,
            bool displayHelpButton = false,
            bool? showCtrlCopy = null) 
            =>
                InternalShow(null, text, caption, buttons, icon, defaultButton, options, displayHelpButton ? new HelpInfo() : null, showCtrlCopy);


        /// <summary>
        /// Displays a message box in front+center of the specified object and with the specified text, caption, buttons, icon, default button, and options.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
        /// <param name="buttons" default="MessageBoxButtons.OK">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon" default="MessageBoxIcon.None">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton" default="MessageBoxDefaultButton.Button1">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options" default="0">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="displayHelpButton" default="false">Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text,
            string caption = @"",
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.None,
            MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1,
            MessageBoxOptions options = 0,
            bool displayHelpButton = false,
            bool? showCtrlCopy = null)
            =>
                InternalShow(owner, text, caption, buttons, icon, defaultButton, options, displayHelpButton ? new HelpInfo() : null, showCtrlCopy);

        /// <summary>
        /// Displays a message box in front+center of the specified object and with the specified text, caption, buttons, icon, default button, and options.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box. default="string.Empty"</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton" default="MessageBoxDefaultButton.Button1">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options" default="0">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="displayHelpButton" default="false">Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, 
            string caption, 
            MessageBoxButtons buttons, 
            KryptonMessageBoxIcon icon, 
            MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1, 
            MessageBoxOptions options=0, 
            bool displayHelpButton = false,
            bool? showCtrlCopy = null) 
            =>
                InternalShow(owner, text, caption, buttons, icon, defaultButton, options, displayHelpButton ? new HelpInfo() : null, showCtrlCopy);

        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box. default="string.Empty"</param>
        /// <param name="buttons" >One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon" >One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton" >One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options" >One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="navigator">One of the System.Windows.Forms.HelpNavigator values.</param>
        /// <param name="param">The numeric ID of the Help topic to display when the user clicks the Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, 
            MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param, bool? showCtrlCopy = null)
            => InternalShow(null, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath, navigator, param), showCtrlCopy);
        
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption" >The text to display in the title bar of the message box. default="string.Empty"</param>
        /// <param name="buttons" >One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon" >One of the KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton" >One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options" >One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="navigator">One of the System.Windows.Forms.HelpNavigator values.</param>
        /// <param name="param">The numeric ID of the Help topic to display when the user clicks the Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, KryptonMessageBoxIcon icon, MessageBoxDefaultButton defaultButton, 
            MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param, bool? showCtrlCopy = null)
            => InternalShow(null, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath, navigator, param), showCtrlCopy);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file, HelpNavigator, and Help topic.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="navigator">One of the System.Windows.Forms.HelpNavigator values.</param>
        /// <param name="param">The numeric ID of the Help topic to display when the user clicks the Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, 
            MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param, bool? showCtrlCopy = null)
            => InternalShow(owner, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath, navigator, param), showCtrlCopy);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file, HelpNavigator, and Help topic.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="navigator">One of the System.Windows.Forms.HelpNavigator values.</param>
        /// <param name="param">The numeric ID of the Help topic to display when the user clicks the Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, KryptonMessageBoxIcon icon, MessageBoxDefaultButton defaultButton,
            MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param, bool? showCtrlCopy = null)
            => InternalShow(owner, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath, navigator, param), showCtrlCopy);
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
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpInfo">Contains the help data of the <see cref="KryptonMessageBox"/>.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        private static DialogResult InternalShow(IWin32Window owner,
                                                 string text, string caption,
                                                 MessageBoxButtons buttons,
                                                 KryptonMessageBoxIcon icon,
                                                 MessageBoxDefaultButton defaultButton,
                                                 MessageBoxOptions options,
                                                 HelpInfo helpInfo, bool? showCtrlCopy)
        {
            caption = string.IsNullOrEmpty(caption) ? @" " : caption;

            IWin32Window showOwner = ValidateOptions(owner, options, helpInfo);

            // Show message box window as a modal dialog and then dispose of it afterwards
            using KryptonMessageBoxForm kmb = new(showOwner, text, caption, buttons, icon, defaultButton, options, helpInfo, showCtrlCopy);
            kmb.StartPosition = showOwner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;

            return kmb.ShowDialog(showOwner);
        }

        #region WinForm Compatibility
        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file, HelpNavigator, and Help topic.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpInfo">Contains the help data of the <see cref="KryptonMessageBox"/>.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        private static DialogResult InternalShow(IWin32Window owner,
                                                 string text, string caption,
                                                 MessageBoxButtons buttons,
                                                 MessageBoxIcon icon,
                                                 MessageBoxDefaultButton defaultButton,
                                                 MessageBoxOptions options,
                                                 HelpInfo helpInfo, bool? showCtrlCopy)
        {
            caption = string.IsNullOrEmpty(caption) ? @" " : caption;

            IWin32Window showOwner = ValidateOptions(owner, options, helpInfo);

            // Show message box window as a modal dialog and then dispose of it afterwards
            using KryptonMessageBoxForm kmb = new(showOwner, text, caption, buttons, icon, defaultButton, options, helpInfo, showCtrlCopy);
            kmb.StartPosition = showOwner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;

            return kmb.ShowDialog(showOwner);
        }

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

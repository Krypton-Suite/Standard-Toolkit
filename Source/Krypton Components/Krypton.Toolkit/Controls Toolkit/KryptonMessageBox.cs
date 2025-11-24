#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMethodReturnValue.Global

namespace Krypton.Toolkit;

/// <summary>
/// Displays a message box that can contain text, buttons, and symbols that inform and instruct the user.
/// The API's mimic the "legacy ones" from WinForms, with the addition of optional params to
/// - force ShowCtrl
/// - Hide the close button
/// - "displayHelpButton" has been moved in order to not collide with the above options
/// "HelpInfo" is used instead of passing individual elements to the help location(s)
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
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
    public static DialogResult Show(string text, 
        bool? showCtrlCopy = null,
        bool? showCloseButton = null) =>
        ShowCore(null, text, string.Empty,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton);

    /// <summary>
    /// Displays a message box in front+center of the application and with the specified text, caption and buttons.
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
    public static DialogResult Show(string text, string caption, bool? showCtrlCopy = null,
        bool? showCloseButton = null) =>
        ShowCore(null, text, caption,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton);


    /// <summary>
    /// Displays a message box in front+center of the application and with the specified text, caption and buttons.
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
    /// <param name="buttons">One of the System.Windows.Forms.KryptonMessageBoxButtons values that specifies which buttons to display in the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
    public static DialogResult Show(string text, string caption, KryptonMessageBoxButtons buttons,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null) =>
        ShowCore(null, text, caption, buttons,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton);

    /// <summary>
    /// Displays a message box in front+center of the application and with the specified text, caption and buttons.
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
    /// <param name="buttons">One of the System.Windows.Forms.KryptonMessageBoxButtons values that specifies which buttons to display in the message box.</param>
    /// <param name="icon">One of the KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
    public static DialogResult Show(string text, string caption, KryptonMessageBoxButtons buttons, KryptonMessageBoxIcon icon,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null) =>
        ShowCore(null, text, caption, buttons, icon,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton);

    /// <summary>
    /// Displays a message box in front+center of the application and with the specified text, caption and buttons.
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
    /// <param name="buttons">One of the System.Windows.Forms.KryptonMessageBoxButtons values that specifies which buttons to display in the message box.</param>
    /// <param name="icon">One of the KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="defaultButton">One of the KryptonMessageBoxDefaultButton values that specifies the default button for the message box.</param>
    /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
    public static DialogResult Show(string text, string caption, KryptonMessageBoxButtons buttons, 
        KryptonMessageBoxIcon icon, KryptonMessageBoxDefaultButton defaultButton,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null) =>
        ShowCore(null, text, caption, buttons, icon, defaultButton,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton);

    /// <summary>
    /// Displays a message box in front+center of the application and with the specified text, caption and buttons.
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
    /// <param name="buttons">One of the System.Windows.Forms.KryptonMessageBoxButtons values that specifies which buttons to display in the message box.</param>
    /// <param name="icon">One of the KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="defaultButton">One of the KryptonMessageBoxDefaultButton values that specifies the default button for the message box.</param>
    /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
    /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
    public static DialogResult Show(string text, string caption, KryptonMessageBoxButtons buttons,
        KryptonMessageBoxIcon icon, KryptonMessageBoxDefaultButton defaultButton, MessageBoxOptions options,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null) =>
        ShowCore(null, text, caption, buttons, icon, defaultButton, options,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton);

    /// <summary>
    /// Displays a message box in front+center of the application and with the specified text, caption and buttons.
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
    /// <param name="buttons">One of the System.Windows.Forms.KryptonMessageBoxButtons values that specifies which buttons to display in the message box.</param>
    /// <param name="icon">One of the KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="defaultButton">One of the KryptonMessageBoxDefaultButton values that specifies the default button for the message box.</param>
    /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
    /// <param name="helpInfo">Contains the help data of the <see cref="KryptonMessageBox"/>.</param>
    /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
    public static DialogResult Show(string text, string caption, KryptonMessageBoxButtons buttons,
        KryptonMessageBoxIcon icon, KryptonMessageBoxDefaultButton defaultButton, MessageBoxOptions options, HelpInfo helpInfo,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null) =>
        ShowCore(null, text, caption, buttons, icon, defaultButton, options, helpInfo,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton);

    /// <summary>
    /// Displays a message box in front+center of the application and with the specified text, caption and buttons.
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
    /// <param name="buttons">One of the System.Windows.Forms.KryptonMessageBoxButtons values that specifies which buttons to display in the message box.</param>
    /// <param name="displayHelpButton">Displays a 'Help' button, as seen in .NET 6 and higher.</param>
    /// <param name="icon">One of the KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="defaultButton">One of the KryptonMessageBoxDefaultButton values that specifies the default button for the message box.</param>
    /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
    /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
    public static DialogResult Show(string text, string caption, KryptonMessageBoxButtons buttons, bool displayHelpButton,
        KryptonMessageBoxIcon icon, KryptonMessageBoxDefaultButton defaultButton, MessageBoxOptions options,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null) =>
        ShowCore(null, text, caption, buttons, icon, defaultButton, options,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: displayHelpButton,
            showCloseButton: showCloseButton);

    /// <summary>
    /// Displays a message box in front+center of the application and with the specified text, caption and buttons.
    /// </summary>
    /// <param name="owner">Owner of the modal dialog box.</param>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
    public static DialogResult Show(IWin32Window owner, string text,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null) =>
        ShowCore(owner, text, string.Empty,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton);

    /// <summary>
    /// Displays a message box in front+center of the application and with the specified text, caption and buttons.
    /// </summary>
    /// <param name="owner">Owner of the modal dialog box.</param>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
    public static DialogResult Show(IWin32Window owner, string? text, string? caption, bool? showCtrlCopy = null,
        bool? showCloseButton = null) =>
        ShowCore(owner, text, caption,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton);


    /// <summary>
    /// Displays a message box in front+center of the application and with the specified text, caption and buttons.
    /// </summary>
    /// <param name="owner">Owner of the modal dialog box.</param>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
    /// <param name="buttons">One of the System.Windows.Forms.KryptonMessageBoxButtons values that specifies which buttons to display in the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
    public static DialogResult Show(IWin32Window owner, string? text, string? caption, KryptonMessageBoxButtons buttons,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null) =>
        ShowCore(owner, text, caption, buttons,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton);

    /// <summary>
    /// Displays a message box in front+center of the application and with the specified text, caption and buttons.
    /// </summary>
    /// <param name="owner">Owner of the modal dialog box.</param>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
    /// <param name="buttons">One of the System.Windows.Forms.KryptonMessageBoxButtons values that specifies which buttons to display in the message box.</param>
    /// <param name="icon">One of the KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
    public static DialogResult Show(IWin32Window owner, string? text, string? caption, KryptonMessageBoxButtons buttons, KryptonMessageBoxIcon icon,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null) =>
        ShowCore(owner, text, caption, buttons, icon,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton);

    /// <summary>
    /// Displays a message box in front+center of the application and with the specified text, caption and buttons.
    /// </summary>
    /// <param name="owner">Owner of the modal dialog box.</param>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
    /// <param name="buttons">One of the System.Windows.Forms.KryptonMessageBoxButtons values that specifies which buttons to display in the message box.</param>
    /// <param name="icon">One of the KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="defaultButton">One of the KryptonMessageBoxDefaultButton values that specifies the default button for the message box.</param>
    /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
    public static DialogResult Show(IWin32Window owner, string? text, string? caption, KryptonMessageBoxButtons buttons,
        KryptonMessageBoxIcon icon, KryptonMessageBoxDefaultButton defaultButton,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null) =>
        ShowCore(owner, text, caption, buttons, icon, defaultButton,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton);

    /// <summary>
    /// Displays a message box in front+center of the application and with the specified text, caption and buttons.
    /// </summary>
    /// <param name="owner">Owner of the modal dialog box.</param>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
    /// <param name="buttons">One of the System.Windows.Forms.KryptonMessageBoxButtons values that specifies which buttons to display in the message box.</param>
    /// <param name="icon">One of the KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="defaultButton">One of the KryptonMessageBoxDefaultButton values that specifies the default button for the message box.</param>
    /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
    /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
    public static DialogResult Show(IWin32Window owner, string? text, string? caption, KryptonMessageBoxButtons buttons,
        KryptonMessageBoxIcon icon, KryptonMessageBoxDefaultButton defaultButton, MessageBoxOptions options,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null) =>
        ShowCore(owner, text, caption, buttons, icon, defaultButton, options,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton);

    /// <summary>
    /// Displays a message box in front+center of the application and with the specified text, caption and buttons.
    /// </summary>
    /// <param name="owner">Owner of the modal dialog box.</param>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
    /// <param name="buttons">One of the System.Windows.Forms.KryptonMessageBoxButtons values that specifies which buttons to display in the message box.</param>
    /// <param name="icon">One of the KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="defaultButton">One of the KryptonMessageBoxDefaultButton values that specifies the default button for the message box.</param>
    /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
    /// <param name="helpInfo">Contains the help data of the <see cref="KryptonMessageBox"/>.</param>
    /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
    public static DialogResult Show(IWin32Window owner, string? text, string? caption, KryptonMessageBoxButtons buttons,
        KryptonMessageBoxIcon icon, KryptonMessageBoxDefaultButton defaultButton, MessageBoxOptions options, HelpInfo helpInfo,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null) =>
        ShowCore(owner, text, caption, buttons, icon, defaultButton, options,
            showCtrlCopy: showCtrlCopy,
            helpInfo: helpInfo,
            showCloseButton: showCloseButton);

    /// <summary>
    /// Displays a message box in front+center of the application and with the specified text, caption and buttons.
    /// </summary>
    /// <param name="owner">Owner of the modal dialog box.</param>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
    /// <param name="buttons">One of the System.Windows.Forms.KryptonMessageBoxButtons values that specifies which buttons to display in the message box.</param>
    /// <param name="displayHelpButton">Displays a 'Help' button, as seen in .NET 6 and higher.</param>
    /// <param name="icon">One of the KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="defaultButton">One of the KryptonMessageBoxDefaultButton values that specifies the default button for the message box.</param>
    /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
    /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
    public static DialogResult Show(IWin32Window owner, string? text, string? caption, KryptonMessageBoxButtons buttons, bool displayHelpButton,
        KryptonMessageBoxIcon icon, KryptonMessageBoxDefaultButton defaultButton, MessageBoxOptions options, 
        bool? showCtrlCopy = null,
        bool? showCloseButton = null) =>
        ShowCore(owner, text, caption, buttons, icon, defaultButton, options,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: displayHelpButton,
            showCloseButton: showCloseButton);
    #endregion

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
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
    private static DialogResult ShowCore(IWin32Window? owner,
        string? text, string? caption,
        KryptonMessageBoxButtons buttons = KryptonMessageBoxButtons.OK,
        KryptonMessageBoxIcon icon = KryptonMessageBoxIcon.None,
        KryptonMessageBoxDefaultButton defaultButton = KryptonMessageBoxDefaultButton.Button1,
        MessageBoxOptions options = 0,
        HelpInfo? helpInfo = null, 
        bool? showCtrlCopy = null,
        bool? showHelpButton = null,
        bool? showCloseButton = null)
    {
        caption = string.IsNullOrEmpty(caption) ? @" " : caption;

        IWin32Window? showOwner = ValidateOptions(owner, options, helpInfo);

        // Show message box window as a modal dialog and then dispose of it after-wards

        if (options is MessageBoxOptions.RightAlign or MessageBoxOptions.RtlReading)
        {
            using var kmbRtl = new VisualMessageBoxRtlAwareForm(showOwner, text, caption, buttons, icon,
                defaultButton, helpInfo, showCtrlCopy, showHelpButton, showCloseButton);

            kmbRtl.StartPosition = showOwner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;

            return kmbRtl.ShowDialog(showOwner);
        }
        else
        {
            using var kmb = new VisualMessageBoxForm(showOwner, text, caption, buttons, icon,
                defaultButton, helpInfo, showCtrlCopy, showHelpButton, showCloseButton);

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
            // If this does not have an owner passed in? then get the active window and use that instead
            showOwner = owner ?? Control.FromHandle(PI.GetActiveWindow());
        }

        return showOwner;
    }
    #endregion
    #endregion
}
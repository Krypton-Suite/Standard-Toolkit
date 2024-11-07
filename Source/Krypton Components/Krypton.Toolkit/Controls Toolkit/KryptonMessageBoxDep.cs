﻿#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved. 
 *  
 */
#endregion

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMethodReturnValue.Global

namespace Krypton.Toolkit
{
    /// <summary>
    /// Displays a message box, that has extended controls.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory(@"code")]
    public static class KryptonMessageBoxDep
    {
        #region Public

        /// <summary>
        /// Displays a message box in front+center of the application and with the specified text, caption and buttons.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
        /// <param name="contentAreaType">Specifies the <see cref="T:MessageBoxContentAreaType"/>.</param>
        /// <param name="linkAreaCommand">Specifies a <see cref="T:KryptonCommand"/> if using the <see cref="T:MessageBoxContentAreaType.LinkLabel"/> type.</param>
        /// <param name="linkLaunchArgument">Specifies the <see cref="ProcessStartInfo"/> if a <paramref name="linkAreaCommand"> has not been defined.</paramref></param>
        /// <param name="contentLinkArea">Specifies the area within the <see cref="KryptonLinkWrapLabel"/> to be regarded as a link. See <see cref="LinkArea"/>.</param>
        /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, bool? showCtrlCopy = null,
                                        MessageBoxContentAreaType? contentAreaType = null,
                                        KryptonCommand? linkAreaCommand = null,
                                        ProcessStartInfo? linkLaunchArgument = null,
                                        LinkArea? contentLinkArea = null,
                                        bool? showCloseButton = null) =>
            ShowCore(null, text, caption, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.None,
                     KryptonMessageBoxDefaultButton.Button4, 0, null, showCtrlCopy,
                     null, null, @"",
                     contentAreaType, linkAreaCommand, linkLaunchArgument, contentLinkArea, null,
                     showCloseButton);

        /// <summary>
        /// Displays a message box in front+center of the application and with the specified text, caption and buttons.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
        /// <param name="contentAreaType">Specifies the <see cref="T:MessageBoxContentAreaType"/>.</param>
        /// <param name="linkAreaCommand">Specifies a <see cref="T:KryptonCommand"/> if using the <see cref="T:MessageBoxContentAreaType.LinkLabel"/> type.</param>
        /// <param name="linkLaunchArgument">Specifies the <see cref="ProcessStartInfo"/> if a <paramref name="linkAreaCommand"> has not been defined.</paramref></param>
        /// <param name="contentLinkArea">Specifies the area within the <see cref="KryptonLinkWrapLabel"/> to be regarded as a link. See <see cref="LinkArea"/>.</param>
        /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, bool? showCtrlCopy = null,
                                        MessageBoxContentAreaType? contentAreaType = null,
                                        KryptonCommand? linkAreaCommand = null,
                                        ProcessStartInfo? linkLaunchArgument = null,
                                        LinkArea? contentLinkArea = null,
                                        bool? showCloseButton = null) =>
            ShowCore(null, text, @"", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.None,
                     KryptonMessageBoxDefaultButton.Button4, 0,
                     null, showCtrlCopy, false, null, @"",
                     contentAreaType, linkAreaCommand, linkLaunchArgument,
                     contentLinkArea, null, showCloseButton);

        /// <summary>
        /// Displays a message box in front+center of the specified object and with the specified text, caption, buttons, icon, default button, and options.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
        /// <param name="contentAreaType">Specifies the <see cref="T:MessageBoxContentAreaType"/>.</param>
        /// <param name="linkAreaCommand">Specifies a <see cref="T:KryptonCommand"/> if using the <see cref="T:MessageBoxContentAreaType.LinkLabel"/> type.</param>
        /// <param name="linkLaunchArgument">Specifies the <see cref="ProcessStartInfo"/> if a <paramref name="linkAreaCommand"> has not been defined.</paramref></param>
        /// <param name="contentLinkArea">Specifies the area within the <see cref="KryptonLinkWrapLabel"/> to be regarded as a link. See <see cref="LinkArea"/>.</param>
        /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window? owner, string text, bool? showCtrlCopy = null,
                                        MessageBoxContentAreaType? contentAreaType = null,
                                        KryptonCommand? linkAreaCommand = null,
                                        ProcessStartInfo? linkLaunchArgument = null,
                                        LinkArea? contentLinkArea = null,
                                        bool? showCloseButton = null) =>
            ShowCore(owner, text, @"", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.None,
                     KryptonMessageBoxDefaultButton.Button4, 0, null, showCtrlCopy,
                     false,
                     null, @"",
                     contentAreaType, linkAreaCommand, linkLaunchArgument, contentLinkArea,
                     null, showCloseButton);

        /// <summary>
        /// Displays a message box in front+center of the specified object and with the specified text, caption, buttons, icon, default button, and options.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
        /// <param name="contentAreaType">Specifies the <see cref="T:MessageBoxContentAreaType"/>.</param>
        /// <param name="linkAreaCommand">Specifies a <see cref="T:KryptonCommand"/> if using the <see cref="T:MessageBoxContentAreaType.LinkLabel"/> type.</param>
        /// <param name="linkLaunchArgument">Specifies the <see cref="ProcessStartInfo"/> if a <paramref name="linkAreaCommand"> has not been defined.</paramref></param>
        /// <param name="contentLinkArea">Specifies the area within the <see cref="KryptonLinkWrapLabel"/> to be regarded as a link. See <see cref="LinkArea"/>.</param>
        /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window? owner, string? text, string caption, bool? showCtrlCopy = null,
                                        MessageBoxContentAreaType? contentAreaType = null,
                                        KryptonCommand? linkAreaCommand = null,
                                        ProcessStartInfo? linkLaunchArgument = null,
                                        LinkArea? contentLinkArea = null,
                                        bool? showCloseButton = null) =>
            ShowCore(owner, text, caption, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.None,
                     KryptonMessageBoxDefaultButton.Button4, 0, null, showCtrlCopy,
                     false,
                     null, @"",
                     contentAreaType, linkAreaCommand, linkLaunchArgument, contentLinkArea,
                     null, showCloseButton);

        /// <summary>
        /// Displays a message box in front+center of the application and with the specified text, caption and buttons.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
        /// <param name="buttons">One of the System.Windows.Forms.KryptonMessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
        /// <param name="contentAreaType">Specifies the <see cref="T:MessageBoxContentAreaType"/>.</param>
        /// <param name="linkAreaCommand">Specifies a <see cref="T:KryptonCommand"/> if using the <see cref="T:MessageBoxContentAreaType.LinkLabel"/> type.</param>
        /// <param name="linkLaunchArgument">Specifies the <see cref="ProcessStartInfo"/> if a <paramref name="linkAreaCommand"> has not been defined.</paramref></param>
        /// <param name="contentLinkArea">Specifies the area within the <see cref="KryptonLinkWrapLabel"/> to be regarded as a link. See <see cref="LinkArea"/>.</param>
        /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, KryptonMessageBoxButtons buttons,
                                        bool? showCtrlCopy = null,
                                        MessageBoxContentAreaType? contentAreaType = null,
                                        KryptonCommand? linkAreaCommand = null,
                                        ProcessStartInfo? linkLaunchArgument = null,
                                        LinkArea? contentLinkArea = null,
                                        bool? showCloseButton = null) =>
                                       ShowCore(null, text, caption, buttons, KryptonMessageBoxIcon.None,
                                                KryptonMessageBoxDefaultButton.Button1, 0,
                                                new HelpInfo(@"", 0, null), showCtrlCopy,
                                                null, null, @"",
                                                contentAreaType, linkAreaCommand, linkLaunchArgument,
                                                contentLinkArea, null,
                                                showCloseButton);

        /// <summary>
        /// Displays a message box in front+center of the application and with the specified text, caption, buttons, icon, default button, and options.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption" default="string.Empty">The text to display in the title bar of the message box. default="string.Empty"</param>
        /// <param name="buttons">One of the System.Windows.Forms.KryptonMessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon" >One of the KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton" default="KryptonMessageBoxDefaultButton.Button4">One of the KryptonMessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options" default="0">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="displayHelpButton" default="false">Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
        /// <param name="showHelpButton">Displays a 'Help' button, as seen in .NET 6 and higher.</param>
        /// <param name="applicationImage">The image of the application.</param>
        /// <param name="applicationPath">The application path. To be used in conjunction with <see cref="T:KryptonMessageBoxIcon.Application"/> type.</param>
        /// <param name="contentAreaType">Specifies the <see cref="T:MessageBoxContentAreaType"/>.</param>
        /// <param name="linkAreaCommand">Specifies a <see cref="T:KryptonCommand"/> if using the <see cref="T:MessageBoxContentAreaType.LinkLabel"/> type.</param>
        /// <param name="linkLaunchArgument">Specifies the <see cref="ProcessStartInfo"/> if a <paramref name="linkAreaCommand"> has not been defined.</paramref></param>
        /// <param name="contentLinkArea">Specifies the area within the <see cref="KryptonLinkWrapLabel"/> to be regarded as a link. See <see cref="LinkArea"/>.</param>
        /// <param name="forceUseOfOperatingSystemIcons">If set to true, the <see cref="VisualMessageBoxForm"/> will use standard operating system icons.</param>
        /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, KryptonMessageBoxButtons buttons,
                                         KryptonMessageBoxIcon icon,
                                         KryptonMessageBoxDefaultButton defaultButton = KryptonMessageBoxDefaultButton.Button4,
                                         MessageBoxOptions options = 0, bool displayHelpButton = false,
                                         bool? showCtrlCopy = null, bool? showHelpButton = null,
                                         ProcessStartInfo? linkLaunchArgument = null, Image? applicationImage = null,
                                         string applicationPath = @"",
                                         MessageBoxContentAreaType? contentAreaType = null,
                                         KryptonCommand? linkAreaCommand = null,
                                         LinkArea? contentLinkArea = null,
                                         bool? forceUseOfOperatingSystemIcons = null,
                                         bool? showCloseButton = null)
            =>
                ShowCore(null, text, caption, buttons, icon, defaultButton, options,
                         displayHelpButton ? new HelpInfo() : null, showCtrlCopy,
                         showHelpButton, applicationImage, applicationPath,
                         contentAreaType, linkAreaCommand, linkLaunchArgument,
                         contentLinkArea, forceUseOfOperatingSystemIcons, showCloseButton);


        /// <summary>
        /// Displays a message box in front+center of the specified object and with the specified text, caption, buttons, icon, default button, and options.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box. default="string.Empty"</param>
        /// <param name="buttons">One of the System.Windows.Forms.KryptonMessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton" default="KryptonMessageBoxDefaultButton.Button4">One of the KryptonMessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options" default="0">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="displayHelpButton" default="false">Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
        /// <param name="showHelpButton">Displays a 'Help' button, as seen in .NET 6 and higher.</param>
        /// <param name="applicationImage">The image of the application.</param>
        /// <param name="applicationPath">The application path. To be used in conjunction with <see cref="T:KryptonMessageBoxIcon.Application"/> type.</param>
        /// <param name="contentAreaType">Specifies the <see cref="T:MessageBoxContentAreaType"/>.</param>
        /// <param name="linkAreaCommand">Specifies a <see cref="T:KryptonCommand"/> if using the <see cref="T:MessageBoxContentAreaType.LinkLabel"/> type.</param>
        /// <param name="linkLaunchArgument">Specifies the <see cref="ProcessStartInfo"/> if a <paramref name="linkAreaCommand"> has not been defined.</paramref></param>
        /// <param name="contentLinkArea">Specifies the area within the <see cref="KryptonLinkWrapLabel"/> to be regarded as a link. See <see cref="LinkArea"/>.</param>
        /// <param name="forceUseOfOperatingSystemIcons">If set to true, the <see cref="VisualMessageBoxForm"/> will use standard operating system icons.</param>
        /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window? owner, string text, string caption,
                                        KryptonMessageBoxButtons buttons, KryptonMessageBoxIcon icon,
                                        KryptonMessageBoxDefaultButton defaultButton = KryptonMessageBoxDefaultButton.Button4,
                                        MessageBoxOptions options = 0, bool displayHelpButton = false,
                                        bool? showCtrlCopy = null, bool? showHelpButton = null, Image? applicationImage = null,
                                        string applicationPath = @"",
                                        MessageBoxContentAreaType? contentAreaType = null,
                                        KryptonCommand? linkAreaCommand = null,
                                        ProcessStartInfo? linkLaunchArgument = null,
                                        LinkArea? contentLinkArea = null,
                                        bool? forceUseOfOperatingSystemIcons = null,
                                        bool? showCloseButton = null)
            =>
                ShowCore(owner, text, caption, buttons, icon, defaultButton, options,
                         displayHelpButton ? new HelpInfo() : null, showCtrlCopy,
                         showHelpButton, applicationImage, applicationPath,
                         contentAreaType, linkAreaCommand, linkLaunchArgument,
                         contentLinkArea, forceUseOfOperatingSystemIcons,
                         showCloseButton);

        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption" >The text to display in the title bar of the message box. default="string.Empty"</param>
        /// <param name="buttons" >One of the System.Windows.Forms.KryptonMessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon" >One of the KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton" >One of the KryptonMessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options" >One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="navigator">One of the System.Windows.Forms.HelpNavigator values.</param>
        /// <param name="param">The numeric ID of the Help topic to display when the user clicks the Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
        /// <param name="showHelpButton">Displays a 'Help' button, as seen in .NET 6 and higher.</param>
        /// <param name="applicationImage">The image of the application.</param>
        /// <param name="applicationPath">The application path. To be used in conjunction with <see cref="T:KryptonMessageBoxIcon.Application"/> type.</param>
        /// <param name="contentAreaType">Specifies the <see cref="T:MessageBoxContentAreaType"/>.</param>
        /// <param name="linkAreaCommand">Specifies a <see cref="T:KryptonCommand"/> if using the <see cref="T:MessageBoxContentAreaType.LinkLabel"/> type.</param>
        /// <param name="linkLaunchArgument">Specifies the <see cref="ProcessStartInfo"/> if a <paramref name="linkAreaCommand"> has not been defined.</paramref></param>
        /// <param name="contentLinkArea">Specifies the area within the <see cref="KryptonLinkWrapLabel"/> to be regarded as a link. See <see cref="LinkArea"/>.</param>
        /// <param name="forceUseOfOperatingSystemIcons">If set to true, the <see cref="VisualMessageBoxForm"/> will use standard operating system icons.</param>
        /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, KryptonMessageBoxButtons buttons,
                                        KryptonMessageBoxIcon icon, KryptonMessageBoxDefaultButton defaultButton,
                                        MessageBoxOptions options, string helpFilePath,
                                        HelpNavigator navigator, object? param, bool? showCtrlCopy = null,
                                        bool? showHelpButton = null,
                                        Image? applicationImage = null, string applicationPath = @"",
                                        MessageBoxContentAreaType? contentAreaType = null,
                                        KryptonCommand? linkAreaCommand = null,
                                        ProcessStartInfo? linkLaunchArgument = null,
                                        LinkArea? contentLinkArea = null,
                                        bool? forceUseOfOperatingSystemIcons = null,
                                        bool? showCloseButton = null)
            => ShowCore(null, text, caption, buttons, icon, defaultButton, options,
                        new HelpInfo(helpFilePath, navigator, param), showCtrlCopy,
                        showHelpButton, applicationImage, applicationPath,
                        contentAreaType, linkAreaCommand, linkLaunchArgument,
                        contentLinkArea, forceUseOfOperatingSystemIcons,
                        showCloseButton);

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
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="navigator">One of the System.Windows.Forms.HelpNavigator values.</param>
        /// <param name="param">The numeric ID of the Help topic to display when the user clicks the Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
        /// <param name="showHelpButton">Displays a 'Help' button, as seen in .NET 6 and higher.</param>
        /// <param name="applicationImage">The image of the application.</param>
        /// <param name="applicationPath">The application path. To be used in conjunction with <see cref="T:KryptonMessageBoxIcon.Application"/> type.</param>
        /// <param name="contentAreaType">Specifies the <see cref="T:MessageBoxContentAreaType"/>.</param>
        /// <param name="linkAreaCommand">Specifies a <see cref="T:KryptonCommand"/> if using the <see cref="T:MessageBoxContentAreaType.LinkLabel"/> type.</param>
        /// <param name="linkLaunchArgument">Specifies the <see cref="ProcessStartInfo"/> if a <paramref name="linkAreaCommand"> has not been defined.</paramref></param>
        /// <param name="contentLinkArea">Specifies the area within the <see cref="KryptonLinkWrapLabel"/> to be regarded as a link. See <see cref="LinkArea"/>.</param>
        /// <param name="forceUseOfOperatingSystemIcons">If set to true, the <see cref="VisualMessageBoxForm"/> will use standard operating system icons.</param>
        /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window? owner, string text, string caption,
                                        KryptonMessageBoxButtons buttons,
                                        KryptonMessageBoxIcon icon,
                                        KryptonMessageBoxDefaultButton defaultButton,
                                        MessageBoxOptions options,
                                        string helpFilePath, HelpNavigator navigator,
                                        object? param, bool? showCtrlCopy = null,
                                        bool? showHelpButton = null,
                                        Image? applicationImage = null,
                                        string applicationPath = @"",
                                        MessageBoxContentAreaType? contentAreaType = null,
                                        KryptonCommand? linkAreaCommand = null,
                                        ProcessStartInfo? linkLaunchArgument = null,
                                        LinkArea? contentLinkArea = null,
                                        bool? forceUseOfOperatingSystemIcons = null,
                                        bool? showCloseButton = null)
            => ShowCore(owner, text, caption, buttons, icon, defaultButton, options,
                        new HelpInfo(helpFilePath, navigator, param),
                        showCtrlCopy, showHelpButton, applicationImage,
                        applicationPath, contentAreaType, linkAreaCommand,
                        linkLaunchArgument, contentLinkArea, forceUseOfOperatingSystemIcons, showCloseButton);

        /// <summary>Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file, HelpNavigator, and Help topic.</summary>
        /// <param name="messageBoxData">The message box data.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(KryptonMessageBoxDataDep messageBoxData) => ShowCore(messageBoxData);

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
        /// <param name="applicationImage">The image of the application.</param>
        /// <param name="applicationPath">The application path. To be used in conjunction with <see cref="T:KryptonMessageBoxIcon.Application"/> type.</param>
        /// <param name="contentAreaType">Specifies the <see cref="T:MessageBoxContentAreaType"/>.</param>
        /// <param name="linkLabelCommand">Specifies a <see cref="T:KryptonCommand"/> if using the <see cref="T:MessageBoxContentAreaType.LinkLabel"/> type.</param>
        /// <param name="linkLaunchArgument">Specifies the <see cref="ProcessStartInfo"/> if a <paramref name="linkLabelCommand"> has not been defined.</paramref></param>
        /// <param name="contentLinkArea">Specifies the area within the <see cref="KryptonLinkWrapLabel"/> to be regarded as a link. See <see cref="LinkArea"/>.</param>
        /// <param name="forceUseOfOperatingSystemIcons">If set to true, the <see cref="VisualMessageBoxForm"/> will use standard operating system icons.</param>
        /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        private static DialogResult ShowCore(IWin32Window? owner,
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

                (kmbrtl.AcceptButton as KryptonButton)?.Select();

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

                (kmb.AcceptButton as KryptonButton)?.Select();

                return kmb.ShowDialog(showOwner);
            }
        }

        private static DialogResult ShowCore(KryptonMessageBoxDataDep messageBoxData)
        {
            messageBoxData.Caption = string.IsNullOrEmpty(messageBoxData.Caption) ? @" " : messageBoxData.Caption;

            IWin32Window? showOwner = ValidateOptions(messageBoxData.Owner, messageBoxData.Options, messageBoxData.HelpInfo);

            if (messageBoxData.Options is MessageBoxOptions.RightAlign or MessageBoxOptions.RtlReading)
            {
                using var kmbrtl = new VisualMessageBoxRtlAwareFormDep(messageBoxData);

                kmbrtl.StartPosition = showOwner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;

                (kmbrtl.AcceptButton as KryptonButton)?.Select();

                return kmbrtl.ShowDialog(showOwner);
            }
            else
            {
                using var kmb = new VisualMessageBoxFormDep(messageBoxData);

                kmb.StartPosition = showOwner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;

                (kmb.AcceptButton as KryptonButton)?.Select();

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
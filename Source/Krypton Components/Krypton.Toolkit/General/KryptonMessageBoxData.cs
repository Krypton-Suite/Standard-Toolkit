#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2024. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>A structure that contains basic information for <see cref="VisualMessageBoxForm"/>.</summary>
    [Obsolete("Please use `KryptonTaskDialog`. Will be removed in V100")]
    public struct KryptonMessageBoxDataDep
    {
        #region Public

        /// <summary>Gets or sets the owner window.</summary>
        /// <value>The owner window.</value>
        public IWin32Window? Owner { get; set; }

        /// <summary>Gets or sets the message text.</summary>
        /// <value>The message text.</value>
        public string? MessageText { get; set; }

        /// <summary>Gets or sets the window caption.</summary>
        /// <value>The window caption.</value>
        public string? Caption { get; set; }

        /// <summary>Gets or sets the buttons.</summary>
        /// <value>The buttons.</value>
        public KryptonMessageBoxButtons Buttons { get; set; }

        /// <summary>Gets or sets the icon.</summary>
        /// <value>The icon.</value>
        public KryptonMessageBoxIcon Icon { get; set; }

        /// <summary>Gets or sets the default button.</summary>
        /// <value>The default button.</value>
        public KryptonMessageBoxDefaultButton? DefaultButton { get; set; }

        /// <summary>Gets or sets the <see cref="MessageBoxOptions"/>.</summary>
        /// <value>The <see cref="MessageBoxOptions"/>.</value>
        public MessageBoxOptions Options { get; set; }

        /// <summary>Gets or sets the help information.</summary>
        /// <value>The help information.</value>
        public HelpInfo? HelpInfo { get; set; }

        /// <summary>Gets or sets the show control copy.</summary>
        /// <value>The show control copy.</value>
        public bool? ShowCtrlCopy { get; set; }

        /// <summary>Gets or sets the show help button.</summary>
        /// <value>The show help button.</value>
        public bool? ShowHelpButton { get; set; }

        /// <summary>Gets or sets the application image.</summary>
        /// <value>The application image.</value>
        public Image? ApplicationImage { get; set; }

        /// <summary>Gets or sets the application path.</summary>
        /// <value>The application path.</value>
        public string? ExtractIconFromFilePath { get; set; }

        /// <summary>Gets or sets the type of the message content area.</summary>
        /// <value>The type of the message content area.</value>
        public MessageBoxContentAreaType? MessageContentAreaType { get; set; }

        /// <summary>Gets or sets the link label command.</summary>
        /// <value>The link label command.</value>
        public KryptonCommand? LinkLabelCommand { get; set; }

        /// <summary>Gets or sets the link launch argument.</summary>
        /// <value>The link launch argument.</value>
        public ProcessStartInfo? LinkLaunchArgument { get; set; }

        /// <summary>Gets or sets the content link area.</summary>
        /// <value>The content link area.</value>
        public LinkArea? ContentLinkArea { get; set; }

        /// <summary>Gets or sets the force use of operating system icons.</summary>
        /// <value>Forces the use of operating system icons.</value>
        public bool? ForceUseOfOperatingSystemIcons { get; set; }

        /// <summary>Gets or sets the help file path for <see cref="HelpInfo"/>.</summary>
        /// <value>The help file path.</value>
        public string? HelpFilePath { get; set; }

        /// <summary>Gets or sets the help navigator for <see cref="HelpInfo"/>.</summary>
        /// <value>The help navigator.</value>
        public HelpNavigator? HelpNavigator { get; set; }

        /// <summary>Gets or sets the help parameters for <see cref="HelpInfo"/>.</summary>
        /// <value>The help parameters.</value>
        public object? HelpParameters { get; set; }

        /// <summary>Gets or sets the show close button.</summary>
        /// <value>The show close button.</value>
        public bool? ShowCloseButton { get; set; }

        /// <summary>Gets or sets the use RTL layout of the <see cref="KryptonMessageBox"/> UI.</summary>
        /// <value>The use RTL layout in an <see cref="KryptonMessageBox"/>.</value>
        public KryptonUseRTLLayout UseRtlLayout { get; set; }

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonMessageBoxDataDep" /> struct.</summary>
        public KryptonMessageBoxDataDep()
        {
            ShowCloseButton = true;

            UseRtlLayout = KryptonUseRTLLayout.No;
        }

        #endregion
    }
}
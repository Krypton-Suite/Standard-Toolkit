#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved.
 *
 */
#endregion

using ContentAlignment = System.Drawing.ContentAlignment;

namespace Krypton.Toolkit
{
    /// <summary>A structure that contains basic information for <see cref="VisualMessageBoxForm"/>.</summary>
    public struct KryptonMessageBoxData
    {
        #region Public

        /// <summary>Gets or sets the owner window.</summary>
        /// <value>The owner window.</value>
        public IWin32Window? Owner { get; set; }

        /// <summary>Gets or sets the message text.</summary>
        /// <value>The message text.</value>
        public string MessageText { get; set; }

        /// <summary>Gets or sets the window caption.</summary>
        /// <value>The window caption.</value>
        public string Caption { get; set; }

        /// <summary>Gets or sets the buttons.</summary>
        /// <value>The buttons.</value>
        public KryptonMessageBoxButtons Buttons { get; set; }

        /// <summary>Gets or sets the icon.</summary>
        /// <value>The icon.</value>
        public KryptonMessageBoxIcon Icon
        {
            get; set;
        }

        /// <summary>Gets or sets the default button.</summary>
        /// <value>The default button.</value>
        public KryptonMessageBoxDefaultButton? DefaultButton
        {
            get; set;
        }

        /// <summary>Gets or sets the <see cref="MessageBoxOptions"/>.</summary>
        /// <value>The <see cref="MessageBoxOptions"/>.</value>
        public MessageBoxOptions Options
        {
            get; set;
        }

        /// <summary>Gets or sets the help information.</summary>
        /// <value>The help information.</value>
        public HelpInfo? HelpInfo
        {
            get; set;
        }

        /// <summary>Gets or sets the show control copy.</summary>
        /// <value>The show control copy.</value>
        public bool? ShowCtrlCopy
        {
            get; set;
        }

        /// <summary>Gets or sets the show help button.</summary>
        /// <value>The show help button.</value>
        public bool? ShowHelpButton
        {
            get; set;
        }

        /// <summary>Gets or sets the show action button.</summary>
        /// <value>The show action button.</value>
        public bool? ShowActionButton
        {
            get; set;
        }

        /// <summary>Gets or sets the action button text.</summary>
        /// <value>The action button text.</value>
        public string? ActionButtonText
        {
            get; set;
        }

        /// <summary>Gets or sets the action button command.</summary>
        /// <value>The action button command.</value>
        public KryptonCommand? ActionButtonCommand
        {
            get; set;
        }

        /// <summary>Gets or sets the application image.</summary>
        /// <value>The application image.</value>
        public Image? ApplicationImage
        {
            get; set;
        }

        /// <summary>Gets or sets the application path.</summary>
        /// <value>The application path.</value>
        public string? ApplicationPath
        {
            get; set;
        }

        /// <summary>Gets or sets the type of the message content area.</summary>
        /// <value>The type of the message content area.</value>
        public MessageBoxContentAreaType? MessageContentAreaType
        {
            get; set;
        }

        /// <summary>Gets or sets the link label command.</summary>
        /// <value>The link label command.</value>
        public KryptonCommand? LinkLabelCommand
        {
            get; set;
        }

        /// <summary>Gets or sets the link launch argument.</summary>
        /// <value>The link launch argument.</value>
        public ProcessStartInfo? LinkLaunchArgument
        {
            get; set;
        }

        /// <summary>Gets or sets the content link area.</summary>
        /// <value>The content link area.</value>
        public LinkArea? ContentLinkArea
        {
            get; set;
        }

        /// <summary>Gets or sets the message text alignment.</summary>
        /// <value>The message text alignment.</value>
        public ContentAlignment? MessageTextAlignment
        {
            get; set;
        }

        /// <summary>Gets or sets the force use of operating system icons.</summary>
        /// <value>Forces the use of operating system icons.</value>
        public bool? ForceUseOfOperatingSystemIcons
        {
            get; set;
        }

        #endregion

        #region Identity

        public KryptonMessageBoxData()
        {

        }

        #endregion
    }
}
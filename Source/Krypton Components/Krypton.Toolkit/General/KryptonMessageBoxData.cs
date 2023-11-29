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

        public IWin32Window? Owner { get; set; }

        public string MessageText { get; set; }

        public string Caption { get; set; }

        public KryptonMessageBoxButtons Buttons { get; set; }

        public KryptonMessageBoxIcon Icon { get; set; }

        public KryptonMessageBoxDefaultButton? DefaultButton { get; set; }

        public MessageBoxOptions Options { get; set; }

        public HelpInfo? HelpInfo { get; set; }

        public bool? ShowCtrlCopy { get; set; }

        public bool? ShowHelpButton { get; set; }

        public bool? ShowActionButton { get; set; }

        public string? ActionButtonText { get; set; }

        public KryptonCommand? ActionButtonCommand { get; set; }

        public Image? ApplicationImage { get; set; }

        public string? ApplicationPath { get; set; }

        public MessageBoxContentAreaType? MessageContentAreaType { get; set; }

        public KryptonCommand? LinkLabelCommand { get; set; }

        public ProcessStartInfo? LinkLaunchArgument { get; set; }

        public LinkArea? ContentLinkArea { get; set; }

        public ContentAlignment? MessageTextAlignment { get; set; }

        public bool? ForceUseOfOperatingSystemIcons { get; set; }

        #endregion
    }
}
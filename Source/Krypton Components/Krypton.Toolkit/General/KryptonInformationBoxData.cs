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
    public struct KryptonInformationBoxData
    {
        #region Public

        public AsyncResultCallback Callback { get; set; }

        public InformationBoxAutoCloseParameters AutoCloseParameters { get; set; }

        public bool ShowHelpButton { get; set; }

        public HelpNavigator HelpNavigator { get; set; }

        public InformationBoxCheckBox DoNotShowAgainBoxCheckBox { get; set; }

        public InformationBoxTitleIconStyle IconStyle { get; set; }

        public InformationBoxPosition Position { get; set; }

        public InformationBoxTitleIcon TitleIcon { get; set; }

        public InformationBoxAutoSizeMode AutoSizeMode { get; set; }

        public InformationBoxButtons Buttons { get; set; }

        public InformationBoxBehavior Behavior { get; set; }

        #endregion

        #region Identity

        public KryptonInformationBoxData()
        {
            HelpNavigator = HelpNavigator.TableOfContents;

            DoNotShowAgainBoxCheckBox = 0;

            IconStyle = InformationBoxTitleIconStyle.None;
        }

        #endregion
    }
}
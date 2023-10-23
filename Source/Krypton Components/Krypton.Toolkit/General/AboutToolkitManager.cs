#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    internal class AboutToolkitManager
    {
        #region Instance Fields

        private AboutToolkitValues _values;

        private KryptonAboutToolkitControl _aboutToolkitControl;

        #endregion

        #region Identity

        public AboutToolkitManager(KryptonAboutToolkitControl aboutToolkitControl, AboutToolkitValues values)
        {
            _aboutToolkitControl = aboutToolkitControl;

            _values = values;
        }

        #endregion

        #region Implementation

        internal static void LaunchProcess(string location)
        {
            try
            {
                Process.Start(location);
            }
            catch (Exception e)
            {
                ExceptionHandler.CaptureException(e);
            }
        }

        internal void SwitchIcon(ToolkitType toolkitType)
        {
            switch (toolkitType)
            {
                case ToolkitType.Canary:
                    break;
                case ToolkitType.Nightly:
                    break;
                case ToolkitType.Stable:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(toolkitType), toolkitType, null);
            }
        }

        internal void SwitchPages(AboutToolkitPage page)
        {
            switch (page)
            {
                case AboutToolkitPage.GeneralInformation:
                    _aboutToolkitControl.generalInformationPanel.Visible = true;

                    _aboutToolkitControl.discordPanel.Visible = false;

                    _aboutToolkitControl.developerInformationPanel.Visible = false;

                    _aboutToolkitControl.versionsPanel.Visible = false;
                    break;
                case AboutToolkitPage.Discord:
                    _aboutToolkitControl.generalInformationPanel.Visible = false;

                    _aboutToolkitControl.discordPanel.Visible = true;

                    _aboutToolkitControl.developerInformationPanel.Visible = false;

                    _aboutToolkitControl.versionsPanel.Visible = false;
                    break;
                case AboutToolkitPage.DeveloperInformation:
                    _aboutToolkitControl.generalInformationPanel.Visible = false;

                    _aboutToolkitControl.discordPanel.Visible = false;

                    _aboutToolkitControl.developerInformationPanel.Visible = true;

                    _aboutToolkitControl.versionsPanel.Visible = false;
                    break;
                case AboutToolkitPage.Versions:
                    _aboutToolkitControl.generalInformationPanel.Visible = false;

                    _aboutToolkitControl.discordPanel.Visible = false;

                    _aboutToolkitControl.developerInformationPanel.Visible = false;

                    _aboutToolkitControl.versionsPanel.Visible = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(page), page, null);
            }
        }

        #endregion
    }
}
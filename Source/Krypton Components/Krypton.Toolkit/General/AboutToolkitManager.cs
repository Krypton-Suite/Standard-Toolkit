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
                    _aboutToolkitControl.LogoBox.Image = ToolkitLogoImageResources.Krypton_Canary;
                    break;
                case ToolkitType.Nightly:
                    _aboutToolkitControl.LogoBox.Image = ToolkitLogoImageResources.Krypton_Nightly;
                    break;
                case ToolkitType.Stable:
                    _aboutToolkitControl.LogoBox.Image = ToolkitLogoImageResources.Krypton_Stable;
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
                    _aboutToolkitControl.GeneralInformationPanel.Visible = true;

                    _aboutToolkitControl.DiscordPanel.Visible = false;

                    _aboutToolkitControl.DeveloperInformationPanel.Visible = false;

                    _aboutToolkitControl.VersionsPanel.Visible = false;
                    break;
                case AboutToolkitPage.Discord:
                    _aboutToolkitControl.GeneralInformationPanel.Visible = false;

                    _aboutToolkitControl.DiscordPanel.Visible = true;

                    _aboutToolkitControl.DeveloperInformationPanel.Visible = false;

                    _aboutToolkitControl.VersionsPanel.Visible = false;
                    break;
                case AboutToolkitPage.DeveloperInformation:
                    _aboutToolkitControl.GeneralInformationPanel.Visible = false;

                    _aboutToolkitControl.DiscordPanel.Visible = false;

                    _aboutToolkitControl.DeveloperInformationPanel.Visible = true;

                    _aboutToolkitControl.VersionsPanel.Visible = false;
                    break;
                case AboutToolkitPage.Versions:
                    _aboutToolkitControl.GeneralInformationPanel.Visible = false;

                    _aboutToolkitControl.DiscordPanel.Visible = false;

                    _aboutToolkitControl.DeveloperInformationPanel.Visible = false;

                    _aboutToolkitControl.VersionsPanel.Visible = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(page), page, null);
            }
        }

        internal void UpdateHeaderText(string text) => _aboutToolkitControl.MainGroup.ValuesPrimary.Heading = text;

        internal void UpdateGeneralInformationText(string text) => _aboutToolkitControl.GeneralInformationLabel.Text = text;

        internal void UpdateGeneralInformationLinkArea(LinkArea area) => _aboutToolkitControl.GeneralInformationLabel.LinkArea = area;



        #endregion
    }
}
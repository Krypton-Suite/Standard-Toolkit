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
    public partial class KryptonAboutToolkitControl : UserControl
    {
        #region Instance Fields



        #endregion

        #region Public



        #endregion

        #region Internals

        #region Panels

        internal KryptonPanel GeneralInformationPanel => kpnlGeneralInformation;

        internal KryptonPanel DiscordPanel => kpnlDiscord;

        internal KryptonPanel DeveloperInformationPanel => kpnlDeveloperInformation;

        internal KryptonPanel VersionsPanel => kpnlVersions;

        #endregion

        #region Labels

        internal KryptonLabel CurrentThemeLabel => klblCurrentTheme;

        #endregion

        #region Link Labels

        internal KryptonLinkWrapLabel GeneralInformationLabel => klwlblGeneralInformation;

        internal KryptonLinkWrapLabel DiscordLabel => klwlblDiscord;

        internal KryptonLinkWrapLabel RepositoriesLabel => klwlblRepositories;

        internal KryptonLinkWrapLabel DocumentationLabel => klwlblDocumentation;

        internal KryptonLinkWrapLabel DemosLabel => klwlblDemos;

        #endregion

        #region Picture Box

        internal PictureBox LogoBox => pbxLogo;

        #endregion

        #region Theme ComboBox

        internal KryptonThemeComboBox ThemeComboBox => ktcmbCurrentTheme;

        #endregion

        #region Data Grid

        internal KryptonDataGridView VersionsGrid => kdgvVersions;

        #endregion

        #region Header Group

        internal KryptonHeaderGroup MainGroup => khgMain;

        #endregion

        #region ToolStrip

        #region Buttons

        internal ToolStripButton GeneralInformationButton => tsbtnGeneralInformation;

        internal ToolStripButton DiscordButton => tsbtnDiscord;

        internal ToolStripButton DeveloperInformationButton => tsbtnDeveloperInformation;

        internal ToolStripButton VersionsButton => tsbtnVersions;

        #endregion

        #region Splitters

        internal ToolStripSeparator DiscordSplitter => tssDiscord;

        internal ToolStripSeparator DeveloperInformationSplitter => tssDeveloperInformation;

        internal ToolStripSeparator VersionsSplitter => tssVersions;

        #endregion

        #endregion

        #endregion

        public KryptonAboutToolkitControl()
        {
            InitializeComponent();
        }

        private void klwlblRepositories_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) =>
            AboutToolkitManager.LaunchProcess(@"");

        private void klwlblDocumentation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) =>
            AboutToolkitManager.LaunchProcess(@"");

        private void klwlblDemos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) =>
            AboutToolkitManager.LaunchProcess(@"");

        private void klwlblGeneralInformation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) =>
            AboutToolkitManager.LaunchProcess(@"");

        private void tsbtnGeneralInformation_Click(object sender, EventArgs e)
        {

        }

        private void tsbtnDiscord_Click(object sender, EventArgs e)
        {

        }

        private void tsbtnDeveloperInformation_Click(object sender, EventArgs e)
        {

        }

        private void tsbtnVersions_Click(object sender, EventArgs e)
        {

        }
    }
}

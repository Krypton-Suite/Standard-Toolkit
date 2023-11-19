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
    internal partial class KryptonAboutToolkitForm : KryptonForm
    {
        #region Static Fields

        private const string DEFAULT_CURRENT_THEME_TEXT = @"Current Theme";

        private const string DEFAULT_HEADER_TEXT = @"About";

        private const string DEFAULT_GENERAL_INFORMATION_WELCOME_TEXT = @"Some of the components used in this application are part of the Krypton Standard Toolkit.";

        private const string DEFAULT_GENERAL_INFORMATION_LICENSE_TEXT = @"License";

        private const string DEFAULT_GENERAL_INFORMATION_LEARN_MORE_TEXT = @"To learn more, click here.";

        private const string DEFAULT_JOIN_DISCORD_SERVER = @"Join our Discord server.";

        private const string DEFAULT_VIEW_REPOSITORIES = @"View our repositories.";

        private const string DEFAULT_DOWNLOAD_DOCUMENTATION = @"Download the latest documentation.";

        private const string DEFAULT_DOWNLOAD_DEMOS = @"Download the demos.";

        private const string DEFAULT_FILE_NAME_COLUMN_HEADER_TEXT = @"File Name";

        private const string DEFAULT_VERSION_COLUMN_HEADER_TEXT = @"Version";

        private const string DEFAULT_TOOL_BAR_GENERAL_INFORMATION_TEXT = @"General Information";

        private const string DEFAULT_TOOL_BAR_DISCORD_TEXT = @"Discord";

        private const string DEFAULT_TOOL_BAR_DEVELOPER_INFORMATION_TEXT = @"Developer Information";

        private const string DEFAULT_TOOL_BAR_VERSION_INFORMATION_TEXT = @"Version Information";

        #endregion

        #region Instance Fields

        private bool _showDiscordButton;
        private bool _showDeveloperInformationButton;
        private bool _showVersionInformationButton;
        private bool _showThemeOptions;
        private bool _showSystemInformationButton;
        private bool _showBuiltOnLabel;

        private Font _commonFont;
        private Font _currentThemeFont;
        private Font _headerFont;

        private ToolkitType _toolkitType;

        private string _builtOnText;
        private string _headerText;
        private string _currentThemeText;
        private string _generalInformationWelcomeText;
        private string _generalInformationLicenseText;
        private string _generalInformationLearnMoreText;
        private string _discordText;
        private string _repositoryInformationText;
        private string _downloadDocumentationText;
        private string _downloadDemosText;
        private string _fileNameColumnHeaderText;
        private string _versionColumnHeaderText;
        private string _toolBarGeneralInformationText;
        private string _toolBarDiscordText;
        private string _toolBarDeveloperInformationText;
        private string _toolBarVersionInformationText;

        private LinkArea _learnMoreLinkArea;
        private LinkArea _discordLinkArea;
        private LinkArea _repositoryInformationLinkArea;
        private LinkArea _downloadDemosLinkArea;
        private LinkArea _documentationLinkArea;

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonAboutToolkitForm" /> class.</summary>
        /// <param name="aboutToolkitData">The about toolkit data.</param>
        public KryptonAboutToolkitForm(KryptonAboutToolkitData aboutToolkitData)
        {
            InitializeComponent();

            InitialiseDialog(aboutToolkitData);

            kbtnAccept.Text = KryptonManager.Strings.GeneralStrings.OK;

            kbtnSystemInformation.Text = KryptonManager.Strings.CustomStrings.SystemInformation;
        }

        #endregion

        #region Implementation

        private void kbtnAccept_Click(object sender, EventArgs e) => Close();

        private void kbtnSystemInformation_Click(object sender, EventArgs e) =>
            KryptonAboutBoxUtilities.LaunchSystemInformation();

        private void InitialiseDialog(KryptonAboutToolkitData aboutToolkitData)
        {
            // Adjust UI elements
            ShowDeveloperControls(aboutToolkitData.ShowDeveloperInformationButton);

            ShowDiscordControls(aboutToolkitData.ShowDiscordButton);

            ShowVersionControls(aboutToolkitData.ShowVersionInformationButton);

            ShowThemeControls(aboutToolkitData.ShowThemeOptions);

            ShowBuildDateLabel(aboutToolkitData.ShowBuildDate);

            UpdateBuiltOnText(string.Empty);

            // ToDo: Figure out why this does not work
            // UpdateBuiltOnText($@"{aboutToolkitData.BuildOnText}: {KryptonAboutBoxUtilities.AssemblyBuildDate(Assembly.LoadFile($@"{Application.ExecutablePath}\Krypton.Toolkit.dll"), false)}");

            UpdateCurrentThemeText($@"{aboutToolkitData.CurrentThemeText}:");

            ShowSystemInformationButton(aboutToolkitData.ShowSystemInformationButton);

            SwitchIcon(aboutToolkitData.ToolkitType);

            UpdateHeaderText($@"{aboutToolkitData.HeaderText} Krypton Standard Toolkit");

            ConcatanateGeneralInformationText(aboutToolkitData.GeneralInformationWelcomeText, aboutToolkitData.GeneralInformationLicenseText, aboutToolkitData.GeneralInformationLearnMoreText);

            UpdateDiscordText(aboutToolkitData.DiscordText);

            UpdateRepositoriesText(aboutToolkitData.RepositoryInformationText);

            UpdateDemosText(aboutToolkitData.DownloadDemosText);

            UpdateDocumentationText(aboutToolkitData.DownloadDocumentationText);

            UpdateColumnHeadings(aboutToolkitData.FileNameColumnHeaderText, aboutToolkitData.VersionColumnHeaderText);

            UpdateToolBarText(aboutToolkitData.ToolBarGeneralInformationText, aboutToolkitData.ToolBarDiscordText, aboutToolkitData.ToolBarDeveloperInformationText, aboutToolkitData.ToolBarVersionInformationText);

            UpdateGeneralInformationLinkArea(aboutToolkitData.LearnMoreLinkArea);

            UpdateDocumentationLinkArea(aboutToolkitData.DocumentationLinkArea);

            UpdateDiscordLinkArea(aboutToolkitData.DiscordLinkArea);

            UpdateDemosLinkArea(aboutToolkitData.DownloadDemosLinkArea);

            UpdateRepositoriesLinkArea(aboutToolkitData.RepositoryInformationLinkArea);

            GetReferenceAssemblyInformation();
        }

        private void ConstructData()
        {
            KryptonAboutToolkitData data = new KryptonAboutToolkitData();

            data.BuildOnText = _builtOnText;

            data.HeaderText = _headerText;

            data.GeneralInformationWelcomeText = _generalInformationWelcomeText;

            data.GeneralInformationLicenseText = _generalInformationLicenseText;

            data.GeneralInformationLearnMoreText = _generalInformationLearnMoreText;

            data.CurrentThemeText = _currentThemeText;

            data.DiscordText = _discordText;

            data.RepositoryInformationText = _repositoryInformationText;

            data.DownloadDemosText = _downloadDemosText;

            data.DownloadDocumentationText = _downloadDocumentationText;

            data.FileNameColumnHeaderText = _fileNameColumnHeaderText;

            data.VersionColumnHeaderText = _versionColumnHeaderText;

            data.ToolBarGeneralInformationText = _toolBarGeneralInformationText;

            data.ToolBarDiscordText = _toolBarDiscordText;

            data.ToolBarDeveloperInformationText = _toolBarDeveloperInformationText;

            data.ToolBarVersionInformationText = _toolBarVersionInformationText;

            data.LearnMoreLinkArea = _learnMoreLinkArea;

            data.DiscordLinkArea = _discordLinkArea;

            data.RepositoryInformationLinkArea = _repositoryInformationLinkArea;

            data.DownloadDemosLinkArea = _downloadDemosLinkArea;

            data.DocumentationLinkArea = _documentationLinkArea;

            data.ToolkitType = _toolkitType;

            data.ShowDiscordButton = _showDiscordButton;

            data.ShowDeveloperInformationButton = _showDeveloperInformationButton;

            data.ShowVersionInformationButton = _showVersionInformationButton;

            data.ShowThemeOptions = _showThemeOptions;

            data.ShowSystemInformationButton = _showSystemInformationButton;

            data.ShowBuildDate = _showBuiltOnLabel;
        }

        private void UpdateCurrentThemeText(string value) => klblCurrentTheme.Text = value;

        private void UpdateToolBarText(string toolBarGeneralInformationText, string toolBarDiscordText, string toolBarDeveloperInformationText, string toolBarVersionInformationText)
        {
            tsbtnGeneralInformation.Text = toolBarGeneralInformationText;

            tsbtnDiscord.Text = toolBarDiscordText;

            tsbtnDeveloperInformation.Text = toolBarDeveloperInformationText;

            tsbtnVersions.Text = toolBarVersionInformationText;
        }

        private void ShowBuildDateLabel(bool value)
        {
            klblBuiltOn.Visible = value;

            if (!value)
            {
                klblBuiltOn.Text = null;
            }
        }

        private void ShowDeveloperControls(bool value)
        {
            tssDeveloperInformation.Visible = value;

            tsbtnDeveloperInformation.Visible = value;
        }

        private void ShowDiscordControls(bool value)
        {
            tssDiscord.Visible = value;

            tsbtnDiscord.Visible = value;
        }

        private void ShowVersionControls(bool value)
        {
            tsbtnVersions.Visible = value;

            tssVersions.Visible = value;
        }

        private void ShowThemeControls(bool value)
        {
            klblCurrentTheme.Visible = value;

            ktcmbCurrentTheme.Visible = value;

            SetLogoSpan(value);
        }

        private void SwitchIcon(ToolkitType value)
        {
            switch (value)
            {
                case ToolkitType.Canary:
                    pbxLogo.Image = ToolkitLogoImageResources.Krypton_Canary;
                    break;
                case ToolkitType.Nightly:
                    pbxLogo.Image = ToolkitLogoImageResources.Krypton_Nightly;
                    break;
                case ToolkitType.Stable:
                    pbxLogo.Image = ToolkitLogoImageResources.Krypton_Stable;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        private void UpdateBuiltOnText(string value) => klblBuiltOn.Text = value;

        private void UpdateHeaderText(string value) => kryptonHeaderGroup1.ValuesPrimary.Heading = value;

        private void ConcatanateGeneralInformationText(string welcomeText, string licenseText, string learnMoreText)
        {
            // Note: Do not use verbatim string!
            string output = $"{welcomeText}\r\n\r\n{licenseText}: BSD-3-Clause\r\n\r\n{learnMoreText}";

            klwlblGeneralInformation.Text = output;
        }

        private void UpdateDiscordText(string value) => klwlblDiscord.Text = value;

        private void UpdateRepositoriesText(string value) => klwlblRepositories.Text = value;

        private void UpdateDocumentationText(string value) => klwlblDocumentation.Text = value;

        private void UpdateDemosText(string value) => klwlblDemos.Text = value;

        private void UpdateColumnHeadings(string fileName, string version)
        {
            kdgvVersions.Columns[0].HeaderText = fileName;

            kdgvVersions.Columns[1].HeaderText = version;
        }

        private void UpdateGeneralInformationLinkArea(LinkArea linkArea) => klwlblGeneralInformation.LinkArea = linkArea;

        private void UpdateDiscordLinkArea(LinkArea linkArea) => klwlblDiscord.LinkArea = linkArea;

        private void UpdateRepositoriesLinkArea(LinkArea linkArea) => klwlblRepositories.LinkArea = linkArea;

        private void UpdateDemosLinkArea(LinkArea linkArea) => klwlblDemos.LinkArea = linkArea;

        private void UpdateDocumentationLinkArea(LinkArea linkArea) => klwlblDocumentation.LinkArea = linkArea;

        private void SetLogoSpan(bool value)
        {
            if (value)
            {
                tlpGeneralInformation.SetRowSpan(pbxLogo, 3);
            }
            else
            {
                klblCurrentTheme.Text = null;

                ktcmbCurrentTheme.Visible = false;

                tlpGeneralInformation.SetRowSpan(pbxLogo, 1);
            }
        }

        private void SwitchPages(AboutToolkitPage page)
        {
            switch (page)
            {
                case AboutToolkitPage.GeneralInformation:
                    kpnlGeneralInformation.Visible = true;

                    kpnlDiscord.Visible = false;

                    kpnlDeveloperInformation.Visible = false;

                    kpnlVersions.Visible = false;

                    tsbtnGeneralInformation.Checked = true;

                    tsbtnDiscord.Checked = false;

                    tsbtnDeveloperInformation.Checked = false;

                    tsbtnVersions.Checked = false;
                    break;
                case AboutToolkitPage.Discord:
                    kpnlGeneralInformation.Visible = false;

                    kpnlDiscord.Visible = true;

                    kpnlDeveloperInformation.Visible = false;

                    kpnlVersions.Visible = false;

                    tsbtnGeneralInformation.Checked = false;

                    tsbtnDiscord.Checked = true;

                    tsbtnDeveloperInformation.Checked = false;

                    tsbtnVersions.Checked = false;
                    break;
                case AboutToolkitPage.DeveloperInformation:
                    kpnlGeneralInformation.Visible = false;

                    kpnlDiscord.Visible = false;

                    kpnlDeveloperInformation.Visible = true;

                    kpnlVersions.Visible = false;

                    tsbtnGeneralInformation.Checked = false;

                    tsbtnDiscord.Checked = false;

                    tsbtnDeveloperInformation.Checked = true;

                    tsbtnVersions.Checked = false;
                    break;
                case AboutToolkitPage.Versions:
                    kpnlGeneralInformation.Visible = false;

                    kpnlDiscord.Visible = false;

                    kpnlDeveloperInformation.Visible = false;

                    kpnlVersions.Visible = true;

                    tsbtnGeneralInformation.Checked = false;

                    tsbtnDiscord.Checked = false;

                    tsbtnDeveloperInformation.Checked = false;

                    tsbtnVersions.Checked = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(page), page, null);
            }
        }

        private void UpdateHeaderFont(Font value) => kryptonHeaderGroup1.StateCommon.HeaderPrimary.Content.ShortText.Font = value;

        private void UpdateCommonFonts(Font value)
        {
            klwlblRepositories.StateCommon.Font = value;

            klwlblDemos.StateCommon.Font = value;

            klwlblDiscord.StateCommon.Font = value;

            klwlblDocumentation.StateCommon.Font = value;

            klwlblGeneralInformation.StateCommon.Font = value;

            ktcmbCurrentTheme.StateCommon.ComboBox.Content.Font = value;
        }

        private void UpdateCurrentThemeFont(Font value) => klblCurrentTheme.StateCommon.ShortText.Font = value;

        private void tsbtnGeneralInformation_Click(object sender, EventArgs e) => SwitchPages(AboutToolkitPage.GeneralInformation);

        private void tsbtnDiscord_Click(object sender, EventArgs e) => SwitchPages(AboutToolkitPage.Discord);

        private void tsbtnDeveloperInformation_Click(object sender, EventArgs e) => SwitchPages(AboutToolkitPage.DeveloperInformation);

        private void tsbtnVersions_Click(object sender, EventArgs e) => SwitchPages(AboutToolkitPage.Versions);

        private void klwlblGeneralInformation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => GlobalToolkitUtilities.LaunchProcess(@"https://github.com/Krypton-Suite/Standard-Toolkit");

        private void klwlblDiscord_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => GlobalToolkitUtilities.LaunchProcess(@"https://discord.gg/CRjF6fY");

        private void klwlblRepositories_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => GlobalToolkitUtilities.LaunchProcess(@"https://github.com/orgs/Krypton-Suite/repositories");

        private void klwlblDocumentation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => GlobalToolkitUtilities.LaunchProcess(@"https://github.com/Krypton-Suite/Help-Files/releases");

        private void klwlblDemos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => GlobalToolkitUtilities.LaunchProcess(@"https://github.com/Krypton-Suite/Standard-Toolkit-Demos/releases");

        private void GetReferenceAssemblyInformation()
        {
            // Get the current assembly
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            // Place reference assemblies into an array
            // Note: Can we use `FileVersionInfo`?
            AssemblyName[] satelliteAssemblies = currentAssembly.GetReferencedAssemblies();

            foreach (AssemblyName assembly in satelliteAssemblies)
            {
                //FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(file);

                // Fill datagrid view
                kdgvVersions.Rows.Add(assembly.Name, assembly.Version.ToString());
            }
        }

        private void ShowSystemInformationButton(bool? value) => kbtnSystemInformation.Visible = value ?? true;

        #endregion

    }
}

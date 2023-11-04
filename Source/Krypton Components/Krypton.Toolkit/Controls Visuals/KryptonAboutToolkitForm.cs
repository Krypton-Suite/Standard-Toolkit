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

        private Font _commonFont;
        private Font _currentThemeFont;
        private Font _headerFont;

        private ToolkitType _toolkitType;

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
        /// <param name="headerText">The header text.</param>
        /// <param name="generalInformationWelcomeText">The general information welcome text.</param>
        /// <param name="generalInformationLicenseText">The general information license text.</param>
        /// <param name="generalInformationLearnMoreText">The general information learn more text.</param>
        /// <param name="currentThemeText">The current theme text.</param>
        /// <param name="discordText">The discord text.</param>
        /// <param name="repositoryInformationText">The repository information text.</param>
        /// <param name="downloadDocumentationText">The download documentation text.</param>
        /// <param name="downloadDemosText">The download demos text.</param>
        /// <param name="fileNameColumnHeaderText">The file name column header text.</param>
        /// <param name="versionColumnHeaderText">The version column header text.</param>
        /// <param name="toolBarGeneralInformationText">The tool bar general information text.</param>
        /// <param name="toolBarDiscordText">The tool bar discord text.</param>
        /// <param name="toolBarDeveloperInformationText">The tool bar developer information text.</param>
        /// <param name="toolBarVersionInformationText">The tool bar version information text.</param>
        /// <param name="learnMoreLinkArea">The learn more link area.</param>
        /// <param name="discordLinkArea">The discord link area.</param>
        /// <param name="repositoryInformationLinkArea">The repository information link area.</param>
        /// <param name="downloadDemosLinkArea">The download demos link area.</param>
        /// <param name="documentationLinkArea">The documentation link area.</param>
        /// <param name="toolkitType">Type of the toolkit.</param>
        /// <param name="showDiscordButton">The show discord button.</param>
        /// <param name="ShowDeveloperInformationButton">The show developer information button.</param>
        /// <param name="showVersionInformationButton">The show version information button.</param>
        /// <param name="showThemeOptions">The show theme options.</param>
        /// <param name="showSystemInformationButton">The show system information button.</param>
        public KryptonAboutToolkitForm(string? headerText, string? generalInformationWelcomeText,
                                       string? generalInformationLicenseText, string? generalInformationLearnMoreText,
                                       string? currentThemeText, string? discordText,
                                       string? repositoryInformationText, string? downloadDocumentationText, string? downloadDemosText,
                                       string? fileNameColumnHeaderText, string? versionColumnHeaderText,
                                       string? toolBarGeneralInformationText, string? toolBarDiscordText,
                                       string? toolBarDeveloperInformationText, string? toolBarVersionInformationText,
                                       LinkArea? learnMoreLinkArea,
                                       LinkArea? discordLinkArea, LinkArea? repositoryInformationLinkArea, LinkArea? downloadDemosLinkArea,
                                       LinkArea? documentationLinkArea, ToolkitType? toolkitType, bool? showDiscordButton, bool? ShowDeveloperInformationButton,
                                       bool? showVersionInformationButton, bool? showThemeOptions, bool? showSystemInformationButton)
        {
            InitializeComponent();

            kbtnOk.Text = KryptonManager.Strings.GeneralStrings.OK;

            kbtnSystemInformation.Text = KryptonManager.Strings.CustomStrings.SystemInformation;

            InitialiseDialog(headerText, generalInformationWelcomeText, generalInformationLicenseText, generalInformationLearnMoreText, currentThemeText,
                          discordText, repositoryInformationText, downloadDocumentationText, downloadDemosText, fileNameColumnHeaderText,
                          versionColumnHeaderText, toolBarGeneralInformationText, toolBarDiscordText,
                          toolBarDeveloperInformationText, toolBarVersionInformationText,
                          learnMoreLinkArea, discordLinkArea, repositoryInformationLinkArea,
                          downloadDemosLinkArea, documentationLinkArea, toolkitType, showDiscordButton, ShowDeveloperInformationButton,
                          showVersionInformationButton, showThemeOptions, showSystemInformationButton);
        }

        #endregion

        #region Implementation

        private void kbtnOk_Click(object sender, EventArgs e) => Close();

        private void kbtnSystemInformation_Click(object sender, EventArgs e) => GlobalToolkitUtilities.LaunchProcess(@"MSInfo32.exe");

        /// <summary>Create a new <see cref="KryptonAboutToolkitForm" /> with selected values.</summary>
        /// <param name="headerText">The header text.</param>
        /// <param name="generalInformationWelcomeText">The general information welcome text.</param>
        /// <param name="generalInformationLicenseText">The general information license text.</param>
        /// <param name="generalInformationLearnMoreText">The general information learn more text.</param>
        /// <param name="currentThemeText">The current theme text.</param>
        /// <param name="discordText">The discord text.</param>
        /// <param name="repositoryInformationText">The repository information text.</param>
        /// <param name="downloadDocumentationText">The download documentation text.</param>
        /// <param name="downloadDemosText">The download demos text.</param>
        /// <param name="fileNameColumnHeaderText">The file name column header text.</param>
        /// <param name="versionColumnHeaderText">The version column header text.</param>
        /// <param name="toolBarGeneralInformationText">The tool bar general information text.</param>
        /// <param name="toolBarDiscordText">The tool bar discord text.</param>
        /// <param name="toolBarDeveloperInformationText">The tool bar developer information text.</param>
        /// <param name="toolBarVersionInformationText">The tool bar version information text.</param>
        /// <param name="learnMoreLinkArea">The learn more link area.</param>
        /// <param name="discordLinkArea">The discord link area.</param>
        /// <param name="repositoryInformationLinkArea">The repository information link area.</param>
        /// <param name="downloadDemosLinkArea">The download demos link area.</param>
        /// <param name="documentationLinkArea">The documentation link area.</param>
        /// <param name="toolkitType">Type of the toolkit.</param>
        /// <param name="showDiscordButton">The show discord button.</param>
        /// <param name="showDeveloperInformationButton">The show developer information button.</param>
        /// <param name="showVersionInformationButton">The show version information button.</param>
        /// <param name="showThemeOptions">The show theme options.</param>
        /// <param name="showSystemInformationButton">The show system information button.</param>
        private void InitialiseDialog(string? headerText, string? generalInformationWelcomeText,
                                      string? generalInformationLicenseText,
                                      string? generalInformationLearnMoreText, string? currentThemeText,
                                      string? discordText, string? repositoryInformationText,
                                      string? downloadDocumentationText, string? downloadDemosText,
                                      string? fileNameColumnHeaderText, string? versionColumnHeaderText,
                                      string? toolBarGeneralInformationText, string? toolBarDiscordText,
                                      string? toolBarDeveloperInformationText,
                                      string? toolBarVersionInformationText,
                                      LinkArea? learnMoreLinkArea, LinkArea? discordLinkArea,
                                      LinkArea? repositoryInformationLinkArea,
                                      LinkArea? downloadDemosLinkArea, LinkArea? documentationLinkArea,
                                      ToolkitType? toolkitType, bool? showDiscordButton,
                                      bool? showDeveloperInformationButton,
                                      bool? showVersionInformationButton, bool? showThemeOptions,
                                      bool? showSystemInformationButton)
        {
            // Fill in the fields
            _headerText = headerText ?? DEFAULT_HEADER_TEXT;

            _generalInformationWelcomeText = generalInformationWelcomeText ?? DEFAULT_GENERAL_INFORMATION_WELCOME_TEXT;

            _generalInformationLicenseText = generalInformationLicenseText ?? DEFAULT_GENERAL_INFORMATION_LICENSE_TEXT;

            _generalInformationLearnMoreText = generalInformationLearnMoreText ?? DEFAULT_GENERAL_INFORMATION_LEARN_MORE_TEXT;

            _currentThemeText = currentThemeText ?? DEFAULT_CURRENT_THEME_TEXT;

            _discordText = discordText ?? DEFAULT_JOIN_DISCORD_SERVER;

            _repositoryInformationText = repositoryInformationText ?? DEFAULT_VIEW_REPOSITORIES;

            _downloadDemosText = downloadDemosText ?? DEFAULT_DOWNLOAD_DEMOS;

            _downloadDocumentationText = downloadDocumentationText ?? DEFAULT_DOWNLOAD_DOCUMENTATION;

            _fileNameColumnHeaderText = fileNameColumnHeaderText ?? DEFAULT_FILE_NAME_COLUMN_HEADER_TEXT;

            _versionColumnHeaderText = versionColumnHeaderText ?? DEFAULT_VERSION_COLUMN_HEADER_TEXT;

            _toolBarGeneralInformationText = toolBarGeneralInformationText ?? DEFAULT_TOOL_BAR_DEVELOPER_INFORMATION_TEXT;

            _toolBarDiscordText = toolBarDiscordText ?? DEFAULT_TOOL_BAR_DISCORD_TEXT;

            _toolBarDeveloperInformationText = toolBarDeveloperInformationText ?? DEFAULT_TOOL_BAR_DEVELOPER_INFORMATION_TEXT;

            _toolBarVersionInformationText = toolBarVersionInformationText ?? DEFAULT_TOOL_BAR_VERSION_INFORMATION_TEXT;

            _learnMoreLinkArea = learnMoreLinkArea ?? new LinkArea(133, 143);

            _discordLinkArea = discordLinkArea ?? new LinkArea(0, 4);

            _repositoryInformationLinkArea = repositoryInformationLinkArea ?? new LinkArea(0, 4);

            _downloadDemosLinkArea = downloadDemosLinkArea ?? new LinkArea(0, 9);

            _documentationLinkArea = documentationLinkArea ?? new LinkArea(0, 9);

            _toolkitType = toolkitType ?? ToolkitType.Stable;

            _showDiscordButton = showDiscordButton ?? true;

            _showDeveloperInformationButton = showDeveloperInformationButton ?? true;

            _showVersionInformationButton = showVersionInformationButton ?? true;

            _showThemeOptions = showThemeOptions ?? true;

            _showSystemInformationButton = showSystemInformationButton ?? true;

            ConstructData();

            // Adjust UI elements
            ShowDeveloperControls(_showDeveloperInformationButton);

            ShowDiscordControls(_showDiscordButton);

            ShowVersionControls(_showVersionInformationButton);

            ShowThemeControls(_showThemeOptions);

            UpdateCurrentThemeText($@"{_currentThemeText}:");

            ShowSystemInformationButton(_showSystemInformationButton);

            SwitchIcon(_toolkitType);

            UpdateHeaderText($@"{_headerText} Krypton Standard Toolkit");

            ConcatanateGeneralInformationText(_generalInformationWelcomeText, _generalInformationLicenseText, _generalInformationLearnMoreText);

            UpdateDiscordText(_discordText);

            UpdateRepositoriesText(_repositoryInformationText);

            UpdateDemosText(_downloadDemosText);

            UpdateDocumentationText(_downloadDocumentationText);

            UpdateColumnHeadings(_fileNameColumnHeaderText, _versionColumnHeaderText);

            UpdateToolBarText(_toolBarGeneralInformationText, _toolBarDiscordText, _toolBarDeveloperInformationText, _toolBarVersionInformationText);

            UpdateGeneralInformationLinkArea(_learnMoreLinkArea);

            UpdateDocumentationLinkArea(_documentationLinkArea);

            UpdateDiscordLinkArea(_discordLinkArea);

            UpdateDemosLinkArea(_downloadDemosLinkArea);

            UpdateRepositoriesLinkArea(_repositoryInformationLinkArea);

            GetReferenceAssemblyInformation();
        }

        private void ConstructData()
        {
            KryptonAboutToolkitData data = new KryptonAboutToolkitData();

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
        }

        private void UpdateCurrentThemeText(string value) => klblCurrentTheme.Text = value;

        private void UpdateToolBarText(string toolBarGeneralInformationText, string toolBarDiscordText, string toolBarDeveloperInformationText, string toolBarVersionInformationText)
        {
            tsbtnGeneralInformation.Text = toolBarGeneralInformationText;

            tsbtnDiscord.Text = toolBarDiscordText;

            tsbtnDeveloperInformation.Text = toolBarDeveloperInformationText;

            tsbtnVersions.Text = toolBarVersionInformationText;
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

        private void UpdateHeaderText(string value) => kryptonHeaderGroup1.ValuesPrimary.Heading = value;

        private void UpdateCurrentVersionText(string value) => klblCurrentTheme.Text = value;

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

        private void LoadToolbarImages()
        {
            tsbtnGeneralInformation.Image = AboutToolkitImageResources.GeneralInformation;

            tssDiscord.Image = AboutToolkitImageResources.Discord;

            tsbtnVersions.Image = AboutToolkitImageResources.VersionInformation;
        }

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
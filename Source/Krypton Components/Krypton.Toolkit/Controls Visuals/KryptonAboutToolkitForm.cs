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

        private const string DEFAULT_CURRENT_THEME_TEXT = @"Current Theme:";

        private const string DEFAULT_HEADER_TEXT = @"About Krypton Toolkit";

        private const string DEFAULT_GENERAL_INFORMATION_FIRST_LINE = @"Some of the components used in this application are part of the Krypton Standard Toolkit.";

        private const string DEFAULT_GENERAL_INFORMATION_SECOND_LINE = @"License: BSD-3-Clause";

        private const string DEFAULT_GENERAL_INFORMATION_THIRD_LINE = @"To learn more, click here.";

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
        private bool _showDeveloperButton;
        private bool _showVersionsButton;
        private bool _showThemeOptions;
        private bool _showSystemInformationButton;

        private Font _commonFont;
        private Font _currentThemeFont;
        private Font _headerFont;

        private ToolkitType _toolkitType;

        private string _headerText;
        private string _currentThemeText;
        private string _generalInformationFirstLine;
        private string _generalInformationSecondLine;
        private string _generalInformationThirdLine;
        private string _discordText;
        private string _repositoryInformationText;
        private string _downloadDocumentationText;
        private string _downloadDemosText;
        private string _fileNameColumnHeaderText;
        private string _versionColumnHeaderText;

        private LinkArea _generalInformationLinkArea;
        private LinkArea _discordLinkArea;
        private LinkArea _repositoryInformationLinkArea;
        private LinkArea _downloadDemosLinkArea;
        private LinkArea _documentationLinkArea;

        #endregion

        #region Public

        /// <summary>Gets or sets a value indicating whether [show developer button].</summary>
        /// <value><c>true</c> if [show developer button]; otherwise, <c>false</c>.</value>
        //[DefaultValue(false)]
        [Description(@"")]
        public bool ShowDeveloperButton { get => _showDeveloperButton; set { _showDeveloperButton = value; ShowDeveloperControls(value); } }

        /// <summary>Gets or sets a value indicating whether [show discord button].</summary>
        /// <value><c>true</c> if [show discord button]; otherwise, <c>false</c>.</value>
        //[DefaultValue(false)]
        [Description(@"")]
        public bool ShowDiscordButton { get => _showDiscordButton; set { _showDiscordButton = value; ShowDiscordControls(value); } }

        /// <summary>Gets or sets a value indicating whether [show versions button].</summary>
        /// <value><c>true</c> if [show versions button]; otherwise, <c>false</c>.</value>
        //[DefaultValue(false)]
        [Description(@"")]
        public bool ShowVersionsButton { get => _showVersionsButton; set { _showVersionsButton = value; ShowVersionControls(value); } }

        /// <summary>Gets or sets a value indicating whether [show theme options].</summary>
        /// <value><c>true</c> if [show theme options]; otherwise, <c>false</c>.</value>
        //[DefaultValue(false)]
        [Description(@"")]
        public bool ShowThemeOptions { get => _showThemeOptions; set { _showThemeOptions = value; ShowThemeControls(value); } }

        public Font CommonFont { get => _commonFont; set { _commonFont = value; UpdateCommonFonts(value); } }

        public Font CustomThemeFont { get => _currentThemeFont; set { _currentThemeFont = value; UpdateCurrentThemeFont(value); } }

        public Font HeaderFont { get => _headerFont; set { _headerFont = value; UpdateHeaderFont(value); } }

        /// <summary>Gets or sets the type of the toolkit.</summary>
        /// <value>The type of the toolkit.</value>
        [DefaultValue(typeof(ToolkitType), @"ToolkitType.Stable")]
        [Description(@"")]
        public ToolkitType ToolkitType { get => _toolkitType; set { _toolkitType = value; SwitchIcon(value); } }

        /// <summary>Gets or sets the header text.</summary>
        /// <value>The header text.</value>
        [Localizable(true)]
        [DefaultValue(DEFAULT_HEADER_TEXT)]
        [Description(@"")]
        public string HeaderText { get => _headerText; set { _headerText = value; UpdateHeaderText(value); } }

        /// <summary>Gets or sets the current theme text.</summary>
        /// <value>The current theme text.</value>
        [Localizable(true)]
        [DefaultValue(DEFAULT_CURRENT_THEME_TEXT)]
        [Description(@"")]
        public string CurrentThemeText { get => _currentThemeText; set { _currentThemeText = value; UpdateCurrentVersionText(value); } }

        /// <summary>Gets or sets the general information first line.</summary>
        /// <value>The general information first line.</value>
        [Localizable(true)]
        [DefaultValue(DEFAULT_GENERAL_INFORMATION_FIRST_LINE)]
        [Description(@"")]
        public string GeneralInformationFirstLine { get => _generalInformationFirstLine; set { _generalInformationFirstLine = value; ConcatanateGeneralInformationText(value, _generalInformationSecondLine, _generalInformationThirdLine); } }

        /// <summary>Gets or sets the general information second line.</summary>
        /// <value>The general information second line.</value>
        [Localizable(true)]
        [DefaultValue(DEFAULT_GENERAL_INFORMATION_SECOND_LINE)]
        [Description(@"")]
        public string GeneralInformationSecondLine { get => _generalInformationSecondLine; set { _generalInformationSecondLine = value; ConcatanateGeneralInformationText(_generalInformationFirstLine, value, _generalInformationThirdLine); } }

        /// <summary>Gets or sets the general information third line.</summary>
        /// <value>The general information third line.</value>
        [Localizable(true)]
        [DefaultValue(DEFAULT_GENERAL_INFORMATION_THIRD_LINE)]
        [Description(@"")]
        public string GeneralInformationThirdLine { get => _generalInformationThirdLine; set { _generalInformationThirdLine = value; ConcatanateGeneralInformationText(_generalInformationFirstLine, _generalInformationSecondLine, value); } }

        /// <summary>Gets or sets the discord text.</summary>
        /// <value>The discord text.</value>
        [Localizable(true)]
        [DefaultValue(DEFAULT_JOIN_DISCORD_SERVER)]
        [Description(@"")]
        public string DiscordText { get => _discordText; set { _discordText = value; UpdateDiscordText(value); } }

        /// <summary>Gets or sets the repository information text.</summary>
        /// <value>The repository information text.</value>
        [Localizable(true)]
        [DefaultValue(DEFAULT_VIEW_REPOSITORIES)]
        [Description(@"")]
        public string RepositoryInformationText { get => _repositoryInformationText; set { _repositoryInformationText = value; UpdateRepositoriesText(value); } }

        /// <summary>Gets or sets the download documentation text.</summary>
        /// <value>The download documentation text.</value>
        [Localizable(true)]
        [DefaultValue(DEFAULT_DOWNLOAD_DOCUMENTATION)]
        [Description(@"")]
        public string DownloadDocumentationText { get => _downloadDocumentationText; set { _downloadDocumentationText = value; UpdateDocumentationText(value); } }

        /// <summary>Gets or sets the download demos text.</summary>
        /// <value>The download demos text.</value>
        [Localizable(true)]
        [DefaultValue(DEFAULT_DOWNLOAD_DEMOS)]
        [Description(@"")]
        public string DownloadDemosText { get => _downloadDemosText; set { _downloadDemosText = value; UpdateDemosText(value); } }

        /// <summary>Gets or sets the file name column header text.</summary>
        /// <value>The file name column header text.</value>
        [Localizable(true)]
        [DefaultValue(DEFAULT_FILE_NAME_COLUMN_HEADER_TEXT)]
        [Description(@"")]
        public string FileNameColumnHeaderText { get => _fileNameColumnHeaderText; set { _fileNameColumnHeaderText = value; UpdateColumnHeadings(value, _versionColumnHeaderText); } }

        /// <summary>Gets or sets the version column header text.</summary>
        /// <value>The version column header text.</value>
        [Localizable(true)]
        [DefaultValue(DEFAULT_VERSION_COLUMN_HEADER_TEXT)]
        [Description(@"")]
        public string VersionColumnHeaderText { get => _versionColumnHeaderText; set { _versionColumnHeaderText = value; UpdateColumnHeadings(_fileNameColumnHeaderText, value); } }

        /// <summary>Gets or sets the general information link area.</summary>
        /// <value>The general information link area.</value>
        public LinkArea GeneralInformationLinkArea { get => _generalInformationLinkArea; set { _generalInformationLinkArea = value; UpdateGeneralInformationLinkArea(value); } }

        /// <summary>Gets or sets the discord link area.</summary>
        /// <value>The discord link area.</value>
        public LinkArea DiscordLinkArea { get => _discordLinkArea; set { _discordLinkArea = value; UpdateDiscordLinkArea(value); } }

        /// <summary>Gets or sets the repository information link area.</summary>
        /// <value>The repository information link area.</value>
        public LinkArea RepositoryInformationLinkArea { get => _repositoryInformationLinkArea; set { _repositoryInformationLinkArea = value; UpdateRepositoriesLinkArea(value); } }

        /// <summary>Gets or sets the download demos link area.</summary>
        /// <value>The download demos link area.</value>
        public LinkArea DownloadDemosLinkArea { get => _downloadDemosLinkArea; set { _downloadDemosLinkArea = value; UpdateDemosLinkArea(value); } }

        /// <summary>Gets or sets the documentation link area.</summary>
        /// <value>The documentation link area.</value>
        public LinkArea DocumentationLinkArea { get => _documentationLinkArea; set { _documentationLinkArea = value; UpdateDocumentationLinkArea(value); } }

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonAboutToolkitForm" /> class.</summary>
        /// <param name="headerText">The header text.</param>
        /// <param name="generalInformationFirstLine">The general information first line.</param>
        /// <param name="generalInformationSecondLine">The general information second line.</param>
        /// <param name="generalInformationThirdLine">The general information third line.</param>
        /// <param name="currentThemeText">The current theme text.</param>
        /// <param name="discordText">The discord text.</param>
        /// <param name="repositoryInformationText">The repository information text.</param>
        /// <param name="downloadDocumentationText">The download documentation text.</param>
        /// <param name="downloadDemosText">The download demos text.</param>
        /// <param name="fileNameColumnHeaderText">The file name column header text.</param>
        /// <param name="versionColumnHeaderText">The version column header text.</param>
        /// <param name="generalInformationLinkArea">The general information link area.</param>
        /// <param name="discordLinkArea">The discord link area.</param>
        /// <param name="repositoryInformationLinkArea">The repository information link area.</param>
        /// <param name="downloadDemosLinkArea">The download demos link area.</param>
        /// <param name="documentationLinkArea">The documentation link area.</param>
        /// <param name="toolkitType">Type of the toolkit.</param>
        /// <param name="showDiscordButton">The show discord button.</param>
        /// <param name="showDeveloperButton">The show developer button.</param>
        /// <param name="showVersionInformationButton">The show version information button.</param>
        /// <param name="showThemeOptions">The show theme options.</param>
        /// <param name="showSystemInformationButton">The show system information button.</param>
        public KryptonAboutToolkitForm(string? headerText, string? generalInformationFirstLine, string? generalInformationSecondLine,
                                       string? generalInformationThirdLine, string? currentThemeText, string? discordText,
                                       string? repositoryInformationText, string? downloadDocumentationText, string? downloadDemosText,
                                       string? fileNameColumnHeaderText, string? versionColumnHeaderText, LinkArea? generalInformationLinkArea,
                                       LinkArea? discordLinkArea, LinkArea? repositoryInformationLinkArea, LinkArea? downloadDemosLinkArea,
                                       LinkArea? documentationLinkArea, ToolkitType? toolkitType, bool? showDiscordButton, bool? showDeveloperButton,
                                       bool? showVersionInformationButton, bool? showThemeOptions, bool? showSystemInformationButton)
        {
            InitializeComponent();

            kbtnOk.Text = KryptonManager.Strings.GeneralStrings.OK;

            kbtnSystemInformation.Text = KryptonManager.Strings.CustomStrings.SystemInformation;

            CustomStartup(headerText, generalInformationFirstLine, generalInformationSecondLine, generalInformationThirdLine, currentThemeText,
                          discordText, repositoryInformationText, downloadDocumentationText, downloadDemosText, fileNameColumnHeaderText,
                          versionColumnHeaderText, generalInformationLinkArea, discordLinkArea, repositoryInformationLinkArea,
                          downloadDemosLinkArea, documentationLinkArea, toolkitType, showDiscordButton, showDeveloperButton,
                          showVersionInformationButton, showThemeOptions, showSystemInformationButton);
        }

        #endregion

        #region Implementation

        private void kbtnOk_Click(object sender, EventArgs e) => Close();

        private void kbtnSystemInformation_Click(object sender, EventArgs e) => GlobalToolkitUtilities.LaunchProcess(@"MSInfo32.exe");

        /// <summary>Custom startup.</summary>
        /// <param name="headerText">The header text.</param>
        /// <param name="generalInformationFirstLine">The general information first line.</param>
        /// <param name="generalInformationSecondLine">The general information second line.</param>
        /// <param name="generalInformationThirdLine">The general information third line.</param>
        /// <param name="currentThemeText">The current theme text.</param>
        /// <param name="discordText">The discord text.</param>
        /// <param name="repositoryInformationText">The repository information text.</param>
        /// <param name="downloadDocumentationText">The download documentation text.</param>
        /// <param name="downloadDemosText">The download demos text.</param>
        /// <param name="fileNameColumnHeaderText">The file name column header text.</param>
        /// <param name="versionColumnHeaderText">The version column header text.</param>
        /// <param name="generalInformationLinkArea">The general information link area.</param>
        /// <param name="discordLinkArea">The discord link area.</param>
        /// <param name="repositoryInformationLinkArea">The repository information link area.</param>
        /// <param name="downloadDemosLinkArea">The download demos link area.</param>
        /// <param name="documentationLinkArea">The documentation link area.</param>
        /// <param name="toolkitType">Type of the toolkit.</param>
        /// <param name="showDiscordButton">The show discord button.</param>
        /// <param name="showDeveloperButton">The show developer button.</param>
        /// <param name="showVersionInformationButton">The show version information button.</param>
        /// <param name="showThemeOptions">The show theme options.</param>
        /// <param name="showSystemInformationButton">The show system information button.</param>
        private void CustomStartup(string? headerText, string? generalInformationFirstLine,
            string? generalInformationSecondLine,
            string? generalInformationThirdLine, string? currentThemeText, string? discordText,
            string? repositoryInformationText, string? downloadDocumentationText, string? downloadDemosText,
            string? fileNameColumnHeaderText, string? versionColumnHeaderText, LinkArea? generalInformationLinkArea,
            LinkArea? discordLinkArea, LinkArea? repositoryInformationLinkArea, LinkArea? downloadDemosLinkArea,
            LinkArea? documentationLinkArea, ToolkitType? toolkitType, bool? showDiscordButton,
            bool? showDeveloperButton,
            bool? showVersionInformationButton, bool? showThemeOptions, bool? showSystemInformationButton)
        {
            // Fill in the fields
            _headerText = headerText ?? DEFAULT_HEADER_TEXT;

            _generalInformationFirstLine = generalInformationFirstLine ?? DEFAULT_GENERAL_INFORMATION_FIRST_LINE;

            _generalInformationSecondLine = generalInformationSecondLine ?? DEFAULT_GENERAL_INFORMATION_SECOND_LINE;

            _generalInformationThirdLine = generalInformationThirdLine ?? DEFAULT_GENERAL_INFORMATION_THIRD_LINE;

            _currentThemeText = currentThemeText ?? DEFAULT_CURRENT_THEME_TEXT;

            _discordText = discordText ?? DEFAULT_JOIN_DISCORD_SERVER;

            _repositoryInformationText = repositoryInformationText ?? DEFAULT_VIEW_REPOSITORIES;

            _downloadDemosText = downloadDemosText ?? DEFAULT_DOWNLOAD_DEMOS;

            _downloadDocumentationText = downloadDocumentationText ?? DEFAULT_DOWNLOAD_DOCUMENTATION;

            _fileNameColumnHeaderText = fileNameColumnHeaderText ?? DEFAULT_FILE_NAME_COLUMN_HEADER_TEXT;

            _versionColumnHeaderText = versionColumnHeaderText ?? DEFAULT_VERSION_COLUMN_HEADER_TEXT;

            _generalInformationLinkArea = generalInformationLinkArea ?? new LinkArea(133, 143);

            _discordLinkArea = discordLinkArea ?? new LinkArea(0, 4);

            _repositoryInformationLinkArea = repositoryInformationLinkArea ?? new LinkArea(0, 4);

            _downloadDemosLinkArea = downloadDemosLinkArea ?? new LinkArea(0, 4);

            _documentationLinkArea = documentationLinkArea ?? new LinkArea(0, 4);

            _toolkitType = toolkitType ?? ToolkitType.Stable;

            _showDiscordButton = showDiscordButton ?? true;

            _showDeveloperButton = showDeveloperButton ?? true;

            _showVersionsButton = showVersionInformationButton ?? true;

            _showThemeOptions = showThemeOptions ?? true;

            _showSystemInformationButton = showSystemInformationButton ?? true;

            // Adjust UI elements
            ShowDeveloperControls(_showDeveloperButton);

            ShowDiscordControls(_showDiscordButton);

            ShowVersionControls(_showVersionsButton);

            ShowThemeControls(_showThemeOptions);

            ShowSystemInformationButton(_showSystemInformationButton);

            SwitchIcon(_toolkitType);

            UpdateHeaderText(_headerText);

            ConcatanateGeneralInformationText(_generalInformationFirstLine, _generalInformationSecondLine, _generalInformationThirdLine);

            UpdateDiscordText(_discordText);

            UpdateRepositoriesText(_repositoryInformationText);

            UpdateDemosText(_downloadDemosText);

            UpdateDocumentationText(_downloadDocumentationText);

            UpdateColumnHeadings(_fileNameColumnHeaderText, _versionColumnHeaderText);

            UpdateGeneralInformationLinkArea(_generalInformationLinkArea);

            UpdateDocumentationLinkArea(_documentationLinkArea);

            UpdateDiscordLinkArea(_discordLinkArea);

            UpdateDemosLinkArea(_downloadDemosLinkArea);

            UpdateRepositoriesLinkArea(_repositoryInformationLinkArea);
        }

        private void DefaultStartup()
        {
            ConcatanateGeneralInformationText(DEFAULT_GENERAL_INFORMATION_FIRST_LINE, DEFAULT_GENERAL_INFORMATION_SECOND_LINE, DEFAULT_GENERAL_INFORMATION_THIRD_LINE);

            klwlblDemos.Text = DEFAULT_DOWNLOAD_DEMOS;

            klwlblRepositories.Text = DEFAULT_VIEW_REPOSITORIES;

            klwlblDocumentation.Text = DEFAULT_DOWNLOAD_DOCUMENTATION;

            klwlblDiscord.Text = DEFAULT_JOIN_DISCORD_SERVER;

            klblCurrentTheme.Text = DEFAULT_CURRENT_THEME_TEXT;

            klwlblRepositories.LinkArea = new LinkArea(0, 4);

            klwlblDiscord.LinkArea = new LinkArea(0, 4);

            klwlblDocumentation.LinkArea = new LinkArea(0, 8);

            klwlblDemos.LinkArea = new LinkArea(0, 8);

            klwlblGeneralInformation.LinkArea = new LinkArea(133, klwlblGeneralInformation.Text.Length - 1);

            UpdateColumnHeadings(DEFAULT_FILE_NAME_COLUMN_HEADER_TEXT, DEFAULT_VERSION_COLUMN_HEADER_TEXT);

            GetReferenceAssemblyInformation();
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

        private void ConcatanateGeneralInformationText(string firstLine, string secondLine, string thirdLine)
        {
            // Note: Do not use verbatim string!
            string output = $"{firstLine}\r\n\r\n{secondLine}\r\n\r\n{thirdLine}";

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
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
    [ToolboxItem(false)]
    [DesignerCategory(@"code")]
    public class AboutToolkitValues : Storage // GlobalId
    {
        #region Static Fields

        private const string DEFAULT_CURRENT_THEME_TEXT = @"Current Theme:";

        private const string DEFAULT_HEADER_TEXT = @"About Krypton Toolkit";

        private const string DEFAULT_GENERAL_INFORMATION_FIRST_LINE = @"Some of the components used in this application are part of the Krypton Standard Toolkit.";

        private const string DEFAULT_GENERAL_INFORMATION_SECOND_LINE = @"License";

        private const string DEFAULT_GENERAL_INFORMATION_THIRD_LINE = @"To learn more, click here.";

        private const string DEFAULT_JOIN_DISCORD_SERVER = @"Join our Discord server.";

        private const string DEFAULT_VIEW_REPOSITORIES = @"View our repositories.";

        private const string DEFAULT_DOWNLOAD_DOCUMENTATION = @"Download the latest documentation.";

        private const string DEFAULT_DOWNLOAD_DEMOS = @"Download the demos.";

        private const string DEFAULT_FILE_NAME_COLUMN_HEADER_TEXT = @"File Name";

        private const string DEFAULT_VERSION_COLUMN_HEADER_TEXT = @"Version";

        #endregion

        #region Instance Fields

        private AboutToolkitManager _manager = new AboutToolkitManager();

        private bool _showDiscordButton;
        private bool _showDeveloperButton;
        private bool _showVersionsButton;
        private bool _showThemeOptions;

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

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="AboutToolkitValues" /> class.</summary>
        public AboutToolkitValues()
        {
            Reset();
        }

        #endregion

        #region IsDefault

        [Browsable(false)]
        public override bool IsDefault => (ShowDeveloperButton == false) &&
                                          (ShowDiscordButton == false) &&
                                          (ShowVersionsButton == false) &&
                                          (ShowThemeOptions == true) &&
                                          (ToolkitType == ToolkitType.Stable) &&
                                          (HeaderText == DEFAULT_HEADER_TEXT) &&
                                          (CurrentThemeText == DEFAULT_CURRENT_THEME_TEXT) &&
                                          (GeneralInformationFirstLine == DEFAULT_GENERAL_INFORMATION_FIRST_LINE) &&
                                          (GeneralInformationSecondLine == DEFAULT_GENERAL_INFORMATION_SECOND_LINE) &&
                                          (GeneralInformationThirdLine == DEFAULT_GENERAL_INFORMATION_THIRD_LINE) &&
                                          (DiscordText == DEFAULT_JOIN_DISCORD_SERVER) &&
                                          (RepositoryInformationText == DEFAULT_VIEW_REPOSITORIES) &&
                                          (DownloadDemosText == DEFAULT_DOWNLOAD_DEMOS) &&
                                          (DownloadDocumentationText == DEFAULT_DOWNLOAD_DOCUMENTATION) &&
                                          (FileNameColumnHeaderText == DEFAULT_FILE_NAME_COLUMN_HEADER_TEXT) &&
                                          (VersionColumnHeaderText == DEFAULT_VERSION_COLUMN_HEADER_TEXT) &&
                                          (GeneralInformationLinkArea == new LinkArea(133, 143)) &&
                                          (DiscordLinkArea == new LinkArea(0, 4)) &&
                                          (RepositoryInformationLinkArea == new LinkArea(0, 4)) &&
                                          (DownloadDemosLinkArea == new LinkArea(0, 4)) &&
                                          (DocumentationLinkArea == new LinkArea(0, 4));

        #endregion

        #region Implementation

        public void Reset()
        {
            ShowDeveloperButton = false;

            ShowDiscordButton = false;

            ShowVersionsButton = false;

            ShowThemeOptions = true;

            ToolkitType = ToolkitType.Stable;

            HeaderText = DEFAULT_HEADER_TEXT;

            CurrentThemeText = DEFAULT_CURRENT_THEME_TEXT;

            GeneralInformationFirstLine = DEFAULT_GENERAL_INFORMATION_FIRST_LINE;

            GeneralInformationSecondLine = DEFAULT_GENERAL_INFORMATION_SECOND_LINE;

            GeneralInformationThirdLine = DEFAULT_GENERAL_INFORMATION_THIRD_LINE;

            DiscordText = DEFAULT_JOIN_DISCORD_SERVER;

            RepositoryInformationText = DEFAULT_VIEW_REPOSITORIES;

            DownloadDemosText = DEFAULT_DOWNLOAD_DEMOS;

            DownloadDocumentationText = DEFAULT_DOWNLOAD_DOCUMENTATION;

            FileNameColumnHeaderText = DEFAULT_FILE_NAME_COLUMN_HEADER_TEXT;

            VersionColumnHeaderText = DEFAULT_VERSION_COLUMN_HEADER_TEXT;

            GeneralInformationLinkArea = new LinkArea(133, 143);

            DiscordLinkArea = new LinkArea(0, 4);

            RepositoryInformationLinkArea = new LinkArea(0, 4);

            DownloadDemosLinkArea = new LinkArea(0, 4);

            DocumentationLinkArea = new LinkArea(0, 4);
        }

        #endregion

        #region Public

        /// <summary>Gets or sets a value indicating whether [show developer button].</summary>
        /// <value><c>true</c> if [show developer button]; otherwise, <c>false</c>.</value>
        //[DefaultValue(false)]
        [Description(@"")]
        public bool ShowDeveloperButton { get => _showDeveloperButton; set { _showDeveloperButton = value; _manager.ShowDeveloperControls(value); } }

        /// <summary>Gets or sets a value indicating whether [show discord button].</summary>
        /// <value><c>true</c> if [show discord button]; otherwise, <c>false</c>.</value>
        //[DefaultValue(false)]
        [Description(@"")]
        public bool ShowDiscordButton { get => _showDiscordButton; set { _showDiscordButton = value; _manager.ShowDiscordControls(value); } }

        /// <summary>Gets or sets a value indicating whether [show versions button].</summary>
        /// <value><c>true</c> if [show versions button]; otherwise, <c>false</c>.</value>
        //[DefaultValue(false)]
        [Description(@"")]
        public bool ShowVersionsButton { get => _showVersionsButton; set { _showVersionsButton = value; _manager.ShowVersionControls(value); } }

        /// <summary>Gets or sets a value indicating whether [show theme options].</summary>
        /// <value><c>true</c> if [show theme options]; otherwise, <c>false</c>.</value>
        //[DefaultValue(false)]
        [Description(@"")]
        public bool ShowThemeOptions { get => _showThemeOptions; set { _showThemeOptions = value; _manager.ShowThemeControls(value); } }

        /// <summary>Gets or sets the type of the toolkit.</summary>
        /// <value>The type of the toolkit.</value>
        [DefaultValue(typeof(ToolkitType), @"ToolkitType.Stable")]
        [Description(@"")]
        public ToolkitType ToolkitType { get => _toolkitType; set { _toolkitType = value; _manager.SwitchIcon(value); } }

        /// <summary>Gets or sets the header text.</summary>
        /// <value>The header text.</value>
        [Localizable(true)]
        [DefaultValue(DEFAULT_HEADER_TEXT)]
        [Description(@"")]
        public string HeaderText { get => _headerText; set { _headerText = value; _manager.UpdateHeaderText(value); } }

        /// <summary>Gets or sets the current theme text.</summary>
        /// <value>The current theme text.</value>
        [Localizable(true)]
        [DefaultValue(DEFAULT_CURRENT_THEME_TEXT)]
        [Description(@"")]
        public string CurrentThemeText { get => _currentThemeText; set { _currentThemeText = value; _manager.UpdateCurrentVersionText(value); } }

        /// <summary>Gets or sets the general information first line.</summary>
        /// <value>The general information first line.</value>
        [Localizable(true)]
        [DefaultValue(DEFAULT_GENERAL_INFORMATION_FIRST_LINE)]
        [Description(@"")]
        public string GeneralInformationFirstLine { get => _generalInformationFirstLine; set { _generalInformationFirstLine = value; _manager.ConcatanateGeneralInformationText(value, _generalInformationSecondLine, _generalInformationThirdLine); } }

        /// <summary>Gets or sets the general information second line.</summary>
        /// <value>The general information second line.</value>
        [Localizable(true)]
        [DefaultValue(DEFAULT_GENERAL_INFORMATION_SECOND_LINE)]
        [Description(@"")]
        public string GeneralInformationSecondLine { get => _generalInformationSecondLine; set { _generalInformationSecondLine = value; _manager.ConcatanateGeneralInformationText(_generalInformationFirstLine, value, _generalInformationThirdLine); } }

        /// <summary>Gets or sets the general information third line.</summary>
        /// <value>The general information third line.</value>
        [Localizable(true)]
        [DefaultValue(DEFAULT_GENERAL_INFORMATION_THIRD_LINE)]
        [Description(@"")]
        public string GeneralInformationThirdLine { get => _generalInformationThirdLine; set { _generalInformationThirdLine = value; _manager.ConcatanateGeneralInformationText(_generalInformationFirstLine, _generalInformationSecondLine, value); } }

        /// <summary>Gets or sets the discord text.</summary>
        /// <value>The discord text.</value>
        [Localizable(true)]
        [DefaultValue(DEFAULT_JOIN_DISCORD_SERVER)]
        [Description(@"")]
        public string DiscordText { get => _discordText; set { _discordText = value; _manager.UpdateDiscordText(value); } }

        /// <summary>Gets or sets the repository information text.</summary>
        /// <value>The repository information text.</value>
        [Localizable(true)]
        [DefaultValue(DEFAULT_VIEW_REPOSITORIES)]
        [Description(@"")]
        public string RepositoryInformationText { get => _repositoryInformationText; set { _repositoryInformationText = value; _manager.UpdateRepositoriesText(value); } }

        /// <summary>Gets or sets the download documentation text.</summary>
        /// <value>The download documentation text.</value>
        [Localizable(true)]
        [DefaultValue(DEFAULT_DOWNLOAD_DOCUMENTATION)]
        [Description(@"")]
        public string DownloadDocumentationText { get => _downloadDocumentationText; set { _downloadDocumentationText = value; _manager.UpdateDocumentationText(value); } }

        /// <summary>Gets or sets the download demos text.</summary>
        /// <value>The download demos text.</value>
        [Localizable(true)]
        [DefaultValue(DEFAULT_DOWNLOAD_DEMOS)]
        [Description(@"")]
        public string DownloadDemosText { get => _downloadDemosText; set { _downloadDemosText = value; _manager.UpdateDemosText(value); } }

        /// <summary>Gets or sets the file name column header text.</summary>
        /// <value>The file name column header text.</value>
        [Localizable(true)]
        [DefaultValue(DEFAULT_FILE_NAME_COLUMN_HEADER_TEXT)]
        [Description(@"")]
        public string FileNameColumnHeaderText { get => _fileNameColumnHeaderText; set { _fileNameColumnHeaderText = value; _manager.UpdateColumnHeadings(value, _versionColumnHeaderText); } }

        /// <summary>Gets or sets the version column header text.</summary>
        /// <value>The version column header text.</value>
        [Localizable(true)]
        [DefaultValue(DEFAULT_VERSION_COLUMN_HEADER_TEXT)]
        [Description(@"")]
        public string VersionColumnHeaderText { get => _versionColumnHeaderText; set { _versionColumnHeaderText = value; _manager.UpdateColumnHeadings(_fileNameColumnHeaderText, value); } }

        /// <summary>Gets or sets the general information link area.</summary>
        /// <value>The general information link area.</value>
        public LinkArea GeneralInformationLinkArea { get => _generalInformationLinkArea; set { _generalInformationLinkArea = value; _manager.UpdateGeneralInformationLinkArea(value); } }

        /// <summary>Gets or sets the discord link area.</summary>
        /// <value>The discord link area.</value>
        public LinkArea DiscordLinkArea { get => _discordLinkArea; set { _discordLinkArea = value; _manager.UpdateDiscordLinkArea(value); } }

        /// <summary>Gets or sets the repository information link area.</summary>
        /// <value>The repository information link area.</value>
        public LinkArea RepositoryInformationLinkArea { get => _repositoryInformationLinkArea; set { _repositoryInformationLinkArea = value; _manager.UpdateRepositoriesLinkArea(value); } }

        /// <summary>Gets or sets the download demos link area.</summary>
        /// <value>The download demos link area.</value>
        public LinkArea DownloadDemosLinkArea { get => _downloadDemosLinkArea; set { _downloadDemosLinkArea = value; _manager.UpdateDemosLinkArea(value); } }

        /// <summary>Gets or sets the documentation link area.</summary>
        /// <value>The documentation link area.</value>
        public LinkArea DocumentationLinkArea { get => _documentationLinkArea; set { _documentationLinkArea = value; _manager.UpdateDocumentationLinkArea(value); } }

        #endregion
    }
}
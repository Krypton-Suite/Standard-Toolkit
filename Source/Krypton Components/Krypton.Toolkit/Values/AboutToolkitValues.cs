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

        private const string DEFAULT_HEADER_TEXT = @"About Krypton Toolkit";

        private const string DEFAULT_GENERAL_INFORMATION_FIRST_LINE = @"Some of the components used in this application are part of the Krypton Standard Toolkit.";

        private const string DEFAULT_GENERAL_INFORMATION_SECOND_LINE = @"License";

        private const string DEFAULT_GENERAL_INFORMATION_THIRD_LINE = @"To learn more, click here.";

        private const string DEFAULT_JOIN_DISCORD_SERVER = @"Join our Discord server.";

        private const string DEFAULT_VIEW_REPOSITORIES = @"View our repositories.";

        private const string DEFAULT_DOWNLOAD_DOCUMENTATION = @"Download the latest documentation.";

        private const string DEFAULT_DOWNLOAD_DEMOS = @"Download the demos.";

        #endregion

        #region Instance Fields

        private AboutToolkitManager _manager = new AboutToolkitManager();

        private bool _showDiscordButton;
        private bool _showDeveloperButton;
        private bool _showVersionsButton;
        private bool _showThemeOptions;

        private ToolkitType _toolkitType;

        private string _headerText;
        private string _currentVersionText;
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
        public override bool IsDefault { get; }

        #endregion

        #region Implementation

        public void Reset()
        {

        }

        #endregion

        #region Public

        /// <summary>Gets or sets a value indicating whether [show developer button].</summary>
        /// <value><c>true</c> if [show developer button]; otherwise, <c>false</c>.</value>
        public bool ShowDeveloperButton { get => _showDeveloperButton; set { _showDeveloperButton = value; _manager.ShowDeveloperControls(value); } }

        /// <summary>Gets or sets a value indicating whether [show discord button].</summary>
        /// <value><c>true</c> if [show discord button]; otherwise, <c>false</c>.</value>
        public bool ShowDiscordButton { get => _showDiscordButton; set { _showDiscordButton = value; _manager.ShowDiscordControls(value); } }

        /// <summary>Gets or sets a value indicating whether [show versions button].</summary>
        /// <value><c>true</c> if [show versions button]; otherwise, <c>false</c>.</value>
        public bool ShowVersionsButton { get => _showVersionsButton; set { _showVersionsButton = value; _manager.ShowVersionControls(value); } }

        /// <summary>Gets or sets a value indicating whether [show theme options].</summary>
        /// <value><c>true</c> if [show theme options]; otherwise, <c>false</c>.</value>
        public bool ShowThemeOptions { get => _showThemeOptions; set { _showThemeOptions = value; _manager.ShowThemeControls(value); } }

        public ToolkitType ToolkitType { get => _toolkitType; set { _toolkitType = value; _manager.SwitchIcon(value); } }

        public string HeaderText { get => _headerText; set { _headerText = value; _manager.UpdateHeaderText(value); } }

        public string CurrentVersionText { get => _currentVersionText; set { _currentVersionText = value; _manager.UpdateCurrentVersionText(value); } }

        public string GeneralInformationFirstLine { get => _generalInformationFirstLine; set { _generalInformationFirstLine = value; _manager.ConcatanateGeneralInformationText(value, _generalInformationSecondLine, _generalInformationThirdLine); } }

        public string GeneralInformationSecondLine { get => _generalInformationSecondLine; set { _generalInformationSecondLine = value; _manager.ConcatanateGeneralInformationText(_generalInformationFirstLine, value, _generalInformationThirdLine); } }

        public string GeneralInformationThirdLine { get => _generalInformationThirdLine; set { _generalInformationThirdLine = value; _manager.ConcatanateGeneralInformationText(_generalInformationFirstLine, _generalInformationSecondLine, value); } }

        public string DiscordText { get => _discordText; set { _discordText = value; _manager.UpdateDiscordText(value); } }

        public string RepositoryInformationText { get => _repositoryInformationText; set { _repositoryInformationText = value; _manager.UpdateRepositoriesText(value); } }

        public string DownloadDocumentationText { get => _downloadDocumentationText; set { _downloadDocumentationText = value; _manager.UpdateDocumentationText(value); } }

        public string DownloadDemosText { get => _downloadDemosText; set { _downloadDemosText = value; _manager.UpdateDemosText(value); } }

        public string FileNameColumnHeaderText { get => _fileNameColumnHeaderText; set { _fileNameColumnHeaderText = value; _manager.UpdateColumnHeadings(value, _versionColumnHeaderText); } }

        public string VersionColumnHeaderText { get => _versionColumnHeaderText; set { _versionColumnHeaderText = value; _manager.UpdateColumnHeadings(_fileNameColumnHeaderText, value); } }

        public LinkArea GeneralInformationLinkArea { get => _generalInformationLinkArea; set { _generalInformationLinkArea = value; _manager.UpdateGeneralInformationLinkArea(value); } }

        public LinkArea DiscordLinkArea { get => _discordLinkArea; set { _discordLinkArea = value; _manager.UpdateDiscordLinkArea(value); } }

        public LinkArea RepositoryInformationLinkArea { get => _repositoryInformationLinkArea; set { _repositoryInformationLinkArea = value; _manager.UpdateRepositoriesLinkArea(value); } }

        public LinkArea DownloadDemosLinkArea { get => _downloadDemosLinkArea; set { _downloadDemosLinkArea = value; _manager.UpdateDemosLinkArea(value); } }

        public LinkArea DocumentationLinkArea { get => _documentationLinkArea; set { _documentationLinkArea = value; _manager.UpdateDocumentationLinkArea(value); } }

        #endregion
    }
}
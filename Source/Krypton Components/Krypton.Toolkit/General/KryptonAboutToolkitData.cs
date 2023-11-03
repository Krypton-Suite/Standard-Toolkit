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
    public struct KryptonAboutToolkitData
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

        /// <summary>Shows the discord button.</summary>
        public bool ShowDiscordButton;

        /// <summary>Shows the developer information button.</summary>
        public bool ShowDeveloperInformationButton;

        /// <summary>Shows the version information button.</summary>
        public bool ShowVersionInformationButton;

        /// <summary>Shows the theme options.</summary>
        public bool ShowThemeOptions;

        /// <summary>The show system information button.</summary>
        public bool ShowSystemInformationButton;

        //public Font CommonFont;
        //public Font CurrentThemeFont;
        //public Font HeaderFont;

        /// <summary>The toolkit type.</summary>
        public ToolkitType ToolkitType;

        /// <summary>The header text.</summary>
        public string HeaderText;

        /// <summary>The current theme text.</summary>
        public string CurrentThemeText;

        /// <summary>The general information welcome text.</summary>
        public string GeneralInformationWelcomeText;

        /// <summary>The general information license text.</summary>
        public string GeneralInformationLicenseText;

        /// <summary>The general information learn more text.</summary>
        public string GeneralInformationLearnMoreText;

        /// <summary>The discord text.</summary>
        public string DiscordText;

        /// <summary>The repository information text.</summary>
        public string RepositoryInformationText;

        /// <summary>The download documentation text.</summary>
        public string DownloadDocumentationText;

        /// <summary>The download demos text.</summary>
        public string DownloadDemosText;

        /// <summary>The file name column header text.</summary>
        public string FileNameColumnHeaderText;

        /// <summary>The version column header text.</summary>
        public string VersionColumnHeaderText;

        /// <summary>The tool bar general information text.</summary>
        public string ToolBarGeneralInformationText;

        /// <summary>The tool bar discord text.</summary>
        public string ToolBarDiscordText;

        /// <summary>The tool bar developer information text.</summary>
        public string ToolBarDeveloperInformationText;

        /// <summary>The tool bar version information text.</summary>
        public string ToolBarVersionInformationText;

        /// <summary>The learn more link area.</summary>
        public LinkArea LearnMoreLinkArea;

        /// <summary>The discord link area.</summary>
        public LinkArea DiscordLinkArea;

        /// <summary>The repository information link area.</summary>
        public LinkArea RepositoryInformationLinkArea;

        /// <summary>The download demos link area.</summary>
        public LinkArea DownloadDemosLinkArea;

        /// <summary>The documentation link area.</summary>
        public LinkArea DocumentationLinkArea;

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonAboutToolkitData" /> struct.</summary>
        /// <param name="toolkitType">Type of the toolkit.</param>
        public KryptonAboutToolkitData(ToolkitType toolkitType = ToolkitType.Stable)
        {
            ShowDeveloperInformationButton = true;

            ShowDiscordButton = true;

            ShowVersionInformationButton = true;

            ShowThemeOptions = true;

            ShowSystemInformationButton = true;

            ToolkitType = toolkitType;

            HeaderText = DEFAULT_HEADER_TEXT;

            CurrentThemeText = DEFAULT_CURRENT_THEME_TEXT;

            GeneralInformationWelcomeText = DEFAULT_GENERAL_INFORMATION_WELCOME_TEXT;

            GeneralInformationLicenseText = DEFAULT_GENERAL_INFORMATION_LICENSE_TEXT;

            GeneralInformationLearnMoreText = DEFAULT_GENERAL_INFORMATION_LEARN_MORE_TEXT;

            DiscordText = DEFAULT_JOIN_DISCORD_SERVER;

            RepositoryInformationText = DEFAULT_VIEW_REPOSITORIES;

            DownloadDemosText = DEFAULT_DOWNLOAD_DEMOS;

            DownloadDocumentationText = DEFAULT_DOWNLOAD_DOCUMENTATION;

            FileNameColumnHeaderText = DEFAULT_FILE_NAME_COLUMN_HEADER_TEXT;

            VersionColumnHeaderText = DEFAULT_VERSION_COLUMN_HEADER_TEXT;

            ToolBarGeneralInformationText = DEFAULT_TOOL_BAR_GENERAL_INFORMATION_TEXT;

            ToolBarDiscordText = DEFAULT_TOOL_BAR_DISCORD_TEXT;

            ToolBarDeveloperInformationText = DEFAULT_TOOL_BAR_DEVELOPER_INFORMATION_TEXT;

            ToolBarVersionInformationText = DEFAULT_TOOL_BAR_VERSION_INFORMATION_TEXT;

            LearnMoreLinkArea = new LinkArea(133, 143);

            DocumentationLinkArea = new LinkArea(0, 8);

            DownloadDemosLinkArea = new LinkArea(0, 8);

            DiscordLinkArea = new LinkArea(0, 4);

            RepositoryInformationLinkArea = new LinkArea(0, 4);
        }

        /// <summary>Initializes a new instance of the <see cref="KryptonAboutToolkitData" /> struct.</summary>
        /// <param name="showDiscordButton">if set to <c>true</c> [show discord button].</param>
        /// <param name="showDeveloperInformationButton">if set to <c>true</c> [show developer information button].</param>
        /// <param name="showVersionInformationButton">if set to <c>true</c> [show version information button].</param>
        /// <param name="showThemeOptions">if set to <c>true</c> [show theme options].</param>
        /// <param name="showSystemInformationButton">if set to <c>true</c> [show system information button].</param>
        /// <param name="toolkitType">Type of the toolkit.</param>
        /// <param name="headerText">The header text.</param>
        /// <param name="currentThemeText">The current theme text.</param>
        /// <param name="generalInformationWelcomeText">The general information welcome text.</param>
        /// <param name="generalInformationLicenseText">The general information license text.</param>
        /// <param name="generalInformationLearnMoreText">The general information learn more text.</param>
        /// <param name="discordText">The discord text.</param>
        /// <param name="repositoryInformationText">The repository information text.</param>
        /// <param name="downloadDemosText">The download demos text.</param>
        /// <param name="downloadDocumentationText">The download documentation text.</param>
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
        public KryptonAboutToolkitData(bool? showDiscordButton, bool? showDeveloperInformationButton, bool? showVersionInformationButton, bool? showThemeOptions,
                                       bool? showSystemInformationButton, ToolkitType? toolkitType, string? headerText, string? currentThemeText,
                                       string? generalInformationWelcomeText, string? generalInformationLicenseText, string? generalInformationLearnMoreText,
                                       string? discordText, string? repositoryInformationText, string? downloadDemosText, string? downloadDocumentationText,
                                       string? fileNameColumnHeaderText, string? versionColumnHeaderText, string? toolBarGeneralInformationText,
                                       string? toolBarDiscordText, string? toolBarDeveloperInformationText, string? toolBarVersionInformationText,
                                       LinkArea? learnMoreLinkArea, LinkArea? discordLinkArea, LinkArea? repositoryInformationLinkArea,
                                       LinkArea? downloadDemosLinkArea, LinkArea? documentationLinkArea)
        {
            ShowDeveloperInformationButton = showDeveloperInformationButton ?? true;

            ShowDiscordButton = showDiscordButton ?? true;

            ShowVersionInformationButton = showVersionInformationButton ?? true;

            ShowThemeOptions = showThemeOptions ?? true;

            ShowSystemInformationButton = showSystemInformationButton ?? true;

            ToolkitType = toolkitType  ?? ToolkitType.Stable;

            HeaderText = headerText ?? DEFAULT_HEADER_TEXT;

            CurrentThemeText = currentThemeText ?? DEFAULT_CURRENT_THEME_TEXT;

            GeneralInformationWelcomeText = generalInformationWelcomeText ?? DEFAULT_GENERAL_INFORMATION_WELCOME_TEXT;

            GeneralInformationLicenseText = generalInformationLicenseText ?? DEFAULT_GENERAL_INFORMATION_LICENSE_TEXT;

            GeneralInformationLearnMoreText = generalInformationLearnMoreText ?? DEFAULT_GENERAL_INFORMATION_LEARN_MORE_TEXT;

            DiscordText = discordText ?? DEFAULT_JOIN_DISCORD_SERVER;

            RepositoryInformationText = repositoryInformationText ?? DEFAULT_VIEW_REPOSITORIES;

            DownloadDemosText = downloadDemosText ?? DEFAULT_DOWNLOAD_DEMOS;

            DownloadDocumentationText = downloadDocumentationText ?? DEFAULT_DOWNLOAD_DOCUMENTATION;

            FileNameColumnHeaderText = fileNameColumnHeaderText ?? DEFAULT_FILE_NAME_COLUMN_HEADER_TEXT;

            VersionColumnHeaderText = versionColumnHeaderText ?? DEFAULT_VERSION_COLUMN_HEADER_TEXT;

            ToolBarGeneralInformationText = toolBarGeneralInformationText ?? DEFAULT_TOOL_BAR_GENERAL_INFORMATION_TEXT;

            ToolBarDiscordText = toolBarDiscordText ?? DEFAULT_TOOL_BAR_DISCORD_TEXT;

            ToolBarDeveloperInformationText = toolBarDeveloperInformationText ?? DEFAULT_TOOL_BAR_DEVELOPER_INFORMATION_TEXT;

            ToolBarVersionInformationText = toolBarVersionInformationText ?? DEFAULT_TOOL_BAR_VERSION_INFORMATION_TEXT;

            LearnMoreLinkArea = learnMoreLinkArea ?? new LinkArea(133, 143);

            DocumentationLinkArea = documentationLinkArea ?? new LinkArea(0, 8);

            DownloadDemosLinkArea = downloadDemosLinkArea ?? new LinkArea(0, 8);

            DiscordLinkArea = discordLinkArea ?? new LinkArea(0, 4);

            RepositoryInformationLinkArea = repositoryInformationLinkArea ?? new LinkArea(0, 4);
        }

        #endregion
    }
}
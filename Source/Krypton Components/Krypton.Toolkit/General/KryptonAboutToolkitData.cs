#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Contains the toolkit information used for creating a new <see cref="VisualAboutBoxForm"/>.</summary>
public struct KryptonAboutToolkitData
{
    #region Static Fields

    private const string DEFAULT_BUILT_ON_TEXT = @"Built On";

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
    public bool ShowDiscordButton { get; set; } = true;

    /// <summary>Shows the developer information button.</summary>
    public bool ShowDeveloperInformationButton { get; set; } = true;

    /// <summary>Shows the version information button.</summary>
    public bool ShowVersionInformationButton { get; set; } = true;

    /// <summary>Shows the theme options.</summary>
    public bool ShowThemeOptions { get; set; } = true;

    /// <summary>The show system information button.</summary>
    public bool ShowSystemInformationButton { get; set; } = true;

    /// <summary>Gets or sets a value indicating whether to show the build date label.</summary>
    public bool ShowBuildDate { get; set; } = true;

    //public Font CommonFont;
    //public Font CurrentThemeFont;
    //public Font HeaderFont;

    /// <summary>The toolkit type.</summary>
    public ToolkitSupportType ToolkitSupportType { get; set; } = ToolkitSupportType.Stable;

    /// <summary>The header text.</summary>
    public string HeaderText { get; set; } = DEFAULT_HEADER_TEXT;

    /// <summary>The current theme text.</summary>
    public string CurrentThemeText { get; set; } = DEFAULT_CURRENT_THEME_TEXT;

    /// <summary>The general information welcome text.</summary>
    public string GeneralInformationWelcomeText { get; set; } = DEFAULT_GENERAL_INFORMATION_WELCOME_TEXT;

    /// <summary>The general information license text.</summary>
    public string GeneralInformationLicenseText { get; set; } = DEFAULT_GENERAL_INFORMATION_LICENSE_TEXT;

    /// <summary>The general information learn more text.</summary>
    public string GeneralInformationLearnMoreText { get; set; } = DEFAULT_GENERAL_INFORMATION_LEARN_MORE_TEXT;

    /// <summary>The discord text.</summary>
    public string DiscordText { get; set; } = DEFAULT_JOIN_DISCORD_SERVER;

    /// <summary>The repository information text.</summary>
    public string RepositoryInformationText { get; set; } = DEFAULT_VIEW_REPOSITORIES;

    /// <summary>The download documentation text.</summary>
    public string DownloadDocumentationText { get; set; } = DEFAULT_DOWNLOAD_DOCUMENTATION;

    /// <summary>The download demos text.</summary>
    public string DownloadDemosText { get; set; } = DEFAULT_DOWNLOAD_DEMOS;

    /// <summary>The file name column header text.</summary>
    public string FileNameColumnHeaderText { get; set; } = DEFAULT_FILE_NAME_COLUMN_HEADER_TEXT;

    /// <summary>The version column header text.</summary>
    public string VersionColumnHeaderText { get; set; } = DEFAULT_VERSION_COLUMN_HEADER_TEXT;

    /// <summary>The tool bar general information text.</summary>
    public string ToolBarGeneralInformationText { get; set; } = DEFAULT_TOOL_BAR_GENERAL_INFORMATION_TEXT;

    /// <summary>The tool bar discord text.</summary>
    public string ToolBarDiscordText { get; set; } = DEFAULT_TOOL_BAR_DISCORD_TEXT;

    /// <summary>The tool bar developer information text.</summary>
    public string ToolBarDeveloperInformationText { get; set; } = DEFAULT_TOOL_BAR_DEVELOPER_INFORMATION_TEXT;

    /// <summary>The tool bar version information text.</summary>
    public string ToolBarVersionInformationText { get; set; } = DEFAULT_TOOL_BAR_VERSION_INFORMATION_TEXT;

    /// <summary>Gets or sets the build on text.</summary>
    public string BuildOnText { get; set; } = DEFAULT_BUILT_ON_TEXT;

    /// <summary>The learn more link area.</summary>
    public LinkArea LearnMoreLinkArea { get; set; } = new LinkArea(133, 143);

    /// <summary>The discord link area.</summary>
    public LinkArea DiscordLinkArea { get; set; } = new LinkArea(0, 4);

    /// <summary>The repository information link area.</summary>
    public LinkArea RepositoryInformationLinkArea { get; set; } = new LinkArea(0, 4);

    /// <summary>The download demos link area.</summary>
    public LinkArea DownloadDemosLinkArea { get; set; } = new LinkArea(0, 9);

    /// <summary>The documentation link area.</summary>
    public LinkArea DocumentationLinkArea { get; set; } = new LinkArea(0, 9);

    /// <summary>Gets or sets the use RTL layout of the <see cref="KryptonAboutBox"/> UI.</summary>
    /// <value>The use RTL layout in an <see cref="KryptonAboutBox"/>.</value>
    public KryptonUseRTLLayout UseRtlLayout { get; set; }

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonAboutToolkitData" /> struct.</summary>
    public KryptonAboutToolkitData()
    {
        ShowDiscordButton = true;

        ShowDeveloperInformationButton = true;

        ShowVersionInformationButton = true;

        ShowThemeOptions = true;

        ShowSystemInformationButton = true;

        ShowBuildDate = true;

        ToolkitSupportType = ToolkitSupportType.Stable;

        BuildOnText = DEFAULT_BUILT_ON_TEXT;

        HeaderText = DEFAULT_HEADER_TEXT;

        CurrentThemeText = DEFAULT_CURRENT_THEME_TEXT;

        GeneralInformationLearnMoreText = DEFAULT_GENERAL_INFORMATION_LEARN_MORE_TEXT;

        GeneralInformationLicenseText = DEFAULT_GENERAL_INFORMATION_LICENSE_TEXT;

        GeneralInformationWelcomeText = DEFAULT_GENERAL_INFORMATION_WELCOME_TEXT;

        DiscordText = DEFAULT_JOIN_DISCORD_SERVER;

        RepositoryInformationText = DEFAULT_VIEW_REPOSITORIES;

        DownloadDocumentationText = DEFAULT_DOWNLOAD_DOCUMENTATION;

        DownloadDemosText = DEFAULT_DOWNLOAD_DEMOS;

        FileNameColumnHeaderText = DEFAULT_FILE_NAME_COLUMN_HEADER_TEXT;

        VersionColumnHeaderText = DEFAULT_VERSION_COLUMN_HEADER_TEXT;

        ToolBarGeneralInformationText = DEFAULT_TOOL_BAR_GENERAL_INFORMATION_TEXT;

        ToolBarDiscordText = DEFAULT_TOOL_BAR_DISCORD_TEXT;

        ToolBarDeveloperInformationText = DEFAULT_TOOL_BAR_DEVELOPER_INFORMATION_TEXT;

        ToolBarVersionInformationText = DEFAULT_TOOL_BAR_VERSION_INFORMATION_TEXT;

        LearnMoreLinkArea = new LinkArea(133, 143);

        DiscordLinkArea = new LinkArea(0, 4);

        RepositoryInformationLinkArea = new LinkArea(0, 4);

        DownloadDemosLinkArea = new LinkArea(0, 9);

        DocumentationLinkArea = new LinkArea(0, 9);

        UseRtlLayout = KryptonUseRTLLayout.No;
    }

    #endregion
}
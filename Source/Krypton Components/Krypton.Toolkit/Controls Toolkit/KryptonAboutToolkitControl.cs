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

        #region TableLayoutPanel

        internal TableLayoutPanel GeneralInformationLayoutPanel => tlpGeneralInformation;

        #endregion

        #endregion

        #region Identity

        public KryptonAboutToolkitControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Implementation



        #endregion

        #region Removed Designer Visibility

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override Color BackColor { get; set; }

        #endregion
    }
}

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

        internal const string DefaultCurrentThemeText = @"Current Theme:";

        internal const string DefaultHeaderText = @"About Krypton Toolkit";

        internal const string DefaultGeneralInformationFirstLine = @"Some of the components used in this application are part of the Krypton Standard Toolkit.";

        internal const string DefaultGeneralInformationSecondLine = @"License";

        internal const string DefaultGeneralInformationThirdLine = @"To learn more, click here.";

        internal const string DefaultJoinDiscordServer = @"Join our Discord server.";

        internal const string DefaultViewRepositories = @"View our repositories.";

        internal const string DefaultDownloadDocumentation = @"Download the latest documentation.";

        internal const string DefaultDownloadDemos = @"Download the demos.";

        internal const string DefaultFileNameColumnHeaderText = @"File Name";

        internal const string DefaultVersionColumnHeaderText = @"Version";

        #endregion

        #region Instance Fields

        private bool _showDiscordButton;
        private bool _showDeveloperButton;
        private bool _showVersionsButton;
        private bool _showThemeOptions;

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
        [DefaultValue(DefaultHeaderText)]
        [Description(@"")]
        public string HeaderText { get => _headerText; set { _headerText = value; UpdateHeaderText(value); } }

        /// <summary>Gets or sets the current theme text.</summary>
        /// <value>The current theme text.</value>
        [Localizable(true)]
        [DefaultValue(DefaultCurrentThemeText)]
        [Description(@"")]
        public string CurrentThemeText { get => _currentThemeText; set { _currentThemeText = value; UpdateCurrentVersionText(value); } }

        /// <summary>Gets or sets the general information first line.</summary>
        /// <value>The general information first line.</value>
        [Localizable(true)]
        [DefaultValue(DefaultGeneralInformationFirstLine)]
        [Description(@"")]
        public string GeneralInformationFirstLine { get => _generalInformationFirstLine; set { _generalInformationFirstLine = value; ConcatanateGeneralInformationText(value, _generalInformationSecondLine, _generalInformationThirdLine); } }

        /// <summary>Gets or sets the general information second line.</summary>
        /// <value>The general information second line.</value>
        [Localizable(true)]
        [DefaultValue(DefaultGeneralInformationSecondLine)]
        [Description(@"")]
        public string GeneralInformationSecondLine { get => _generalInformationSecondLine; set { _generalInformationSecondLine = value; ConcatanateGeneralInformationText(_generalInformationFirstLine, value, _generalInformationThirdLine); } }

        /// <summary>Gets or sets the general information third line.</summary>
        /// <value>The general information third line.</value>
        [Localizable(true)]
        [DefaultValue(DefaultGeneralInformationThirdLine)]
        [Description(@"")]
        public string GeneralInformationThirdLine { get => _generalInformationThirdLine; set { _generalInformationThirdLine = value; ConcatanateGeneralInformationText(_generalInformationFirstLine, _generalInformationSecondLine, value); } }

        /// <summary>Gets or sets the discord text.</summary>
        /// <value>The discord text.</value>
        [Localizable(true)]
        [DefaultValue(DefaultJoinDiscordServer)]
        [Description(@"")]
        public string DiscordText { get => _discordText; set { _discordText = value; UpdateDiscordText(value); } }

        /// <summary>Gets or sets the repository information text.</summary>
        /// <value>The repository information text.</value>
        [Localizable(true)]
        [DefaultValue(DefaultViewRepositories)]
        [Description(@"")]
        public string RepositoryInformationText { get => _repositoryInformationText; set { _repositoryInformationText = value; UpdateRepositoriesText(value); } }

        /// <summary>Gets or sets the download documentation text.</summary>
        /// <value>The download documentation text.</value>
        [Localizable(true)]
        [DefaultValue(DefaultDownloadDocumentation)]
        [Description(@"")]
        public string DownloadDocumentationText { get => _downloadDocumentationText; set { _downloadDocumentationText = value; UpdateDocumentationText(value); } }

        /// <summary>Gets or sets the download demos text.</summary>
        /// <value>The download demos text.</value>
        [Localizable(true)]
        [DefaultValue(DefaultDownloadDemos)]
        [Description(@"")]
        public string DownloadDemosText { get => _downloadDemosText; set { _downloadDemosText = value; UpdateDemosText(value); } }

        /// <summary>Gets or sets the file name column header text.</summary>
        /// <value>The file name column header text.</value>
        [Localizable(true)]
        [DefaultValue(DefaultFileNameColumnHeaderText)]
        [Description(@"")]
        public string FileNameColumnHeaderText { get => _fileNameColumnHeaderText; set { _fileNameColumnHeaderText = value; UpdateColumnHeadings(value, _versionColumnHeaderText); } }

        /// <summary>Gets or sets the version column header text.</summary>
        /// <value>The version column header text.</value>
        [Localizable(true)]
        [DefaultValue(DefaultVersionColumnHeaderText)]
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

        /// <summary>Initializes a new instance of the <see cref="KryptonAboutToolkitControl" /> class.</summary>
        public KryptonAboutToolkitControl()
        {
            InitializeComponent();

            _showDeveloperButton = true;

            _showDiscordButton = true;

            _showVersionsButton = true;

            _showThemeOptions = true;

            _currentThemeFont = new Font(@"Microsoft Sans Serif", 8.25f, FontStyle.Bold);

            _commonFont = new Font(@"Microsoft Sans Serif", 8.25f);

            _headerFont = new Font(@"Microsoft Sans Serif", 11.25f, FontStyle.Bold);

            _toolkitType = ToolkitType.Stable;

            _headerText = DefaultHeaderText;

            khgMain.ValuesPrimary.Heading = DefaultHeaderText;

            _currentThemeText = DefaultCurrentThemeText;

            _generalInformationFirstLine = DefaultGeneralInformationFirstLine;

            _generalInformationSecondLine = DefaultGeneralInformationSecondLine;

            _generalInformationThirdLine = DefaultGeneralInformationThirdLine;

            _discordText = DefaultJoinDiscordServer;

            _repositoryInformationText = DefaultViewRepositories;

            _downloadDemosText = DefaultDownloadDemos;

            _downloadDocumentationText = DefaultDownloadDocumentation;

            _fileNameColumnHeaderText = DefaultFileNameColumnHeaderText;

            _versionColumnHeaderText = DefaultVersionColumnHeaderText;

            _generalInformationLinkArea = new LinkArea(133, 143);

            _discordLinkArea = new LinkArea(0, 4);

            _repositoryInformationLinkArea = new LinkArea(0, 4);

            _downloadDemosLinkArea = new LinkArea(0, 4);

            _documentationLinkArea = new LinkArea(0, 4);
            LoadToolbarImages();

            SwitchIcon(ToolkitType.Stable);
        }

        #endregion

        #region Implementation

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

        private void UpdateHeaderText(string value) => khgMain.ValuesPrimary.Heading = value;

        private void UpdateCurrentVersionText(string value) => klblCurrentTheme.Text = value;

        private void ConcatanateGeneralInformationText(string firstLine, string secondLine, string thirdLine)
        {
            // Note: Do not use verbatim string!
            string output = $"{firstLine}\\r\\n\\r\\n{secondLine}\\r\\n\\r\\n{thirdLine}";

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
                    GeneralInformationPanel.Visible = true;

                    DiscordPanel.Visible = false;

                    DeveloperInformationPanel.Visible = false;

                    VersionsPanel.Visible = false;
                    break;
                case AboutToolkitPage.Discord:
                    GeneralInformationPanel.Visible = false;

                    DiscordPanel.Visible = true;

                    DeveloperInformationPanel.Visible = false;

                    VersionsPanel.Visible = false;
                    break;
                case AboutToolkitPage.DeveloperInformation:
                    GeneralInformationPanel.Visible = false;

                    DiscordPanel.Visible = false;

                    DeveloperInformationPanel.Visible = true;

                    VersionsPanel.Visible = false;
                    break;
                case AboutToolkitPage.Versions:
                    GeneralInformationPanel.Visible = false;

                    DiscordPanel.Visible = false;

                    DeveloperInformationPanel.Visible = false;

                    VersionsPanel.Visible = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(page), page, null);
            }
        }

        private void UpdateHeaderFont(Font value) => khgMain.StateCommon.HeaderPrimary.Content.ShortText.Font = value;

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

        #endregion

        #region Protected

        protected override void OnLoad(EventArgs e)
        {
            LoadToolbarImages();

            base.OnLoad(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        #endregion

        #region Removed Designer Visibility

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override Color BackColor { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override Font Font { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override Color ForeColor { get; set; }

        #endregion
    }
}

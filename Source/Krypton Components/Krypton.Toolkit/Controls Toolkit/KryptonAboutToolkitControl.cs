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

        private AboutToolkitManager _manager;

        private AboutToolkitValues _values;

        #endregion

        #region Public

        [Category(@"Visuals")]
        [Description(@"")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public AboutToolkitValues Values
        {
            [DebuggerStepThrough]
            get => _values;

            set
            {
                _values = value;

                _manager = new AboutToolkitManager(this, Values);
            }
        }

        private bool ShouldSerializeAboutToolkitValues() => !_values.IsDefault;

        public void ResetAboutToolkitValues() => _values.Reset();

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

            Values = new AboutToolkitValues();

            StartUp();
        }

        #endregion

        #region Implementation

        private void StartUp()
        {
            Values.Reset();

            _manager.SwitchIcon(Values.ToolkitType);

            _manager.LoadToolbarImages();

            _manager.ConcatanateGeneralInformationText(Values.GeneralInformationFirstLine, Values.GeneralInformationSecondLine, Values.GeneralInformationThirdLine);

            _manager.ShowThemeControls(Values.ShowThemeOptions);
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

        #endregion
    }
}

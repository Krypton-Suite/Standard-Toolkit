#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved.
 *
 */
#endregion

// ReSharper disable LocalizableElement
namespace Krypton.Toolkit
{
    internal class AboutToolkitManager
    {
        #region Instance Fields

        private AboutToolkitValues _values;

        private readonly KryptonAboutToolkitControl _aboutToolkitControl;

        #endregion

        #region Identity

        public AboutToolkitManager()
        {

        }

        public AboutToolkitManager(KryptonAboutToolkitControl aboutToolkitControl, AboutToolkitValues values)
        {
            _aboutToolkitControl = aboutToolkitControl;

            _values = values;
        }

        #endregion

        #region Implementation

        internal static void LaunchProcess(string location)
        {
            try
            {
                Process.Start(location);
            }
            catch (Exception e)
            {
                ExceptionHandler.CaptureException(e);
            }
        }

        internal void SwitchIcon(ToolkitType toolkitType)
        {
            switch (toolkitType)
            {
                case ToolkitType.Canary:
                    _aboutToolkitControl.LogoBox.Image = ToolkitLogoImageResources.Krypton_Canary;
                    break;
                case ToolkitType.Nightly:
                    _aboutToolkitControl.LogoBox.Image = ToolkitLogoImageResources.Krypton_Nightly;
                    break;
                case ToolkitType.Stable:
                    _aboutToolkitControl.LogoBox.Image = ToolkitLogoImageResources.Krypton_Stable;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(toolkitType), toolkitType, null);
            }
        }

        internal void SwitchPages(AboutToolkitPage page)
        {
            switch (page)
            {
                case AboutToolkitPage.GeneralInformation:
                    _aboutToolkitControl.GeneralInformationPanel.Visible = true;

                    _aboutToolkitControl.DiscordPanel.Visible = false;

                    _aboutToolkitControl.DeveloperInformationPanel.Visible = false;

                    _aboutToolkitControl.VersionsPanel.Visible = false;
                    break;
                case AboutToolkitPage.Discord:
                    _aboutToolkitControl.GeneralInformationPanel.Visible = false;

                    _aboutToolkitControl.DiscordPanel.Visible = true;

                    _aboutToolkitControl.DeveloperInformationPanel.Visible = false;

                    _aboutToolkitControl.VersionsPanel.Visible = false;
                    break;
                case AboutToolkitPage.DeveloperInformation:
                    _aboutToolkitControl.GeneralInformationPanel.Visible = false;

                    _aboutToolkitControl.DiscordPanel.Visible = false;

                    _aboutToolkitControl.DeveloperInformationPanel.Visible = true;

                    _aboutToolkitControl.VersionsPanel.Visible = false;
                    break;
                case AboutToolkitPage.Versions:
                    _aboutToolkitControl.GeneralInformationPanel.Visible = false;

                    _aboutToolkitControl.DiscordPanel.Visible = false;

                    _aboutToolkitControl.DeveloperInformationPanel.Visible = false;

                    _aboutToolkitControl.VersionsPanel.Visible = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(page), page, null);
            }
        }

        internal void UpdateHeaderText(string text) => _aboutToolkitControl.MainGroup.ValuesPrimary.Heading = text;

        internal void UpdateGeneralInformationText(string text) =>
            _aboutToolkitControl.GeneralInformationLabel.Text = text;

        internal void UpdateGeneralInformationLinkArea(LinkArea area) =>
            _aboutToolkitControl.GeneralInformationLabel.LinkArea = area;

        internal void UpdateDiscordText(string text) => _aboutToolkitControl.DiscordLabel.Text = text;

        internal void UpdateDiscordLinkArea(LinkArea area) => _aboutToolkitControl.DiscordLabel.LinkArea = area;

        internal void UpdateRepositoriesText(string text) => _aboutToolkitControl.RepositoriesLabel.Text = text;

        internal void UpdateRepositoriesLinkArea(LinkArea area) =>
            _aboutToolkitControl.RepositoriesLabel.LinkArea = area;

        internal void UpdateDocumentationText(string text) => _aboutToolkitControl.DocumentationLabel.Text = text;

        internal void UpdateDocumentationLinkArea(LinkArea area) =>
            _aboutToolkitControl.DocumentationLabel.LinkArea = area;

        internal void UpdateDemosText(string text) => _aboutToolkitControl.DemosLabel.Text = text;

        internal void UpdateDemosLinkArea(LinkArea area) => _aboutToolkitControl.DemosLabel.LinkArea = area;

        internal void LoadToolbarImages()
        {
            _aboutToolkitControl.GeneralInformationButton.Image = AboutToolkitImageResources.GeneralInformation;





            _aboutToolkitControl.VersionsButton.Image = AboutToolkitImageResources.VersionInformation;
        }

        internal void UpdateColumnHeadings(string fileName, string version)
        {
            _aboutToolkitControl.VersionsGrid.Columns[0].HeaderText = fileName;

            _aboutToolkitControl.VersionsGrid.Columns[1].HeaderText = version;
        }

        internal void ShowDeveloperControls(bool value)
        {
            _aboutToolkitControl.DeveloperInformationButton.Visible = value;

            _aboutToolkitControl.DeveloperInformationSplitter.Visible = value;
        }

        internal void ShowDiscordControls(bool value)
        {
            _aboutToolkitControl.DiscordButton.Visible = value;

            _aboutToolkitControl.DiscordSplitter.Visible = value;
        }

        internal void ShowVersionControls(bool value)
        {
            _aboutToolkitControl.VersionsButton.Visible = value;

            _aboutToolkitControl.VersionsSplitter.Visible = value;
        }

        internal void ShowThemeControls(bool value)
        {
            _aboutToolkitControl.CurrentThemeLabel.Visible = value;

            _aboutToolkitControl.ThemeComboBox.Visible = value;

            SetLogoSpan(value);
        }

        internal void UpdateCurrentVersionText(string value) => _aboutToolkitControl.CurrentThemeLabel.Text = value;

        internal void ConcatanateGeneralInformationText(string firstLine, string secondLine, string thirdLine)
            => _aboutToolkitControl.GeneralInformationLabel.Text =
                $"{firstLine}\r\n\r\n{secondLine}\r\n\r\n{thirdLine}";

        internal void SetLogoSpan(bool showThemeOptions)
        {
            if (showThemeOptions)
            {
                _aboutToolkitControl.GeneralInformationLayoutPanel.SetRowSpan(_aboutToolkitControl.LogoBox, 3);
            }
            else
            {
                _aboutToolkitControl.GeneralInformationLayoutPanel.SetRowSpan(_aboutToolkitControl.LogoBox, 1);
            }
        }

        internal void GetReferenceAssemblyInformation()
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
                _aboutToolkitControl.VersionsGrid.Rows.Add(assembly.Name, assembly.Version.ToString());
            }
        }

        #endregion
    }
}
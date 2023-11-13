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
    internal partial class KryptonAboutBoxForm : KryptonForm
    {
        #region Instance Fields

        private readonly KryptonAboutBoxData _aboutBoxData;

        #endregion

        #region Identity

        public KryptonAboutBoxForm(KryptonAboutBoxData aboutBoxData)
        {
            InitializeComponent();

            _aboutBoxData = aboutBoxData;

            Startup(_aboutBoxData);

            kbtnOk.Text = KryptonManager.Strings.GeneralStrings.OK;

            kbtnSystemInformation.Text = KryptonManager.Strings.CustomStrings.SystemInformation;
        }

        #endregion

        #region Implementation

        private void Startup(KryptonAboutBoxData aboutBoxData)
        {
            khgMain.ValuesPrimary.Image =
                aboutBoxData.HeaderImage ?? GenericImageResources.InformationSmall;

            khgMain.ValuesPrimary.Heading =
                $@"{KryptonManager.Strings.AboutBoxStrings.About} {aboutBoxData.ApplicationName}";

            pbxImage.Image = aboutBoxData.MainImage ?? GenericImageResources.InformationMedium;

            kwlblCurrentTheme.Text = $@"{KryptonManager.Strings.CustomStrings.CurrentTheme}:";

            // ToDo: Review
            UpdateVersionLabel($"{KryptonManager.Strings.AboutBoxStrings.Version}: {KryptonAboutBoxUtilities.GetFileVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion}");

            UpdateBuiltOnLabel($"{KryptonManager.Strings.AboutBoxStrings.BuildDate}: {KryptonAboutBoxUtilities.AssemblyBuildDate(Assembly.GetExecutingAssembly(), true)}");

            UpdateCopyrightLabel($"{KryptonManager.Strings.AboutBoxStrings.Copyright}: {KryptonAboutBoxUtilities.GetFileVersionInfo(Assembly.GetExecutingAssembly().Location).LegalCopyright}");

            UpdateDescription(KryptonAboutBoxUtilities.GetFileVersionInfo(Assembly.GetEntryAssembly()!.Location).FileDescription);
        }

        private void UpdateDescription(string fileDescription) => krtxtDescription.Text = fileDescription;

        private void UpdateCopyrightLabel(string value) => kwlCopyright.Text = value;

        private void UpdateBuiltOnLabel(string value) => kwlBuiltOn.Text = value;

        private void UpdateVersionLabel(string value) => kwlVersionLabel.Text = value;

        private void kbtnOk_Click(object sender, EventArgs e) => Hide();

        private void kbtnSystemInformation_Click(object sender, EventArgs e) => KryptonAboutBoxUtilities.LaunchSystemInformation();

        private void tsbtnGeneralInformation_Click(object sender, EventArgs e) => SwitchAboutBoxPage(AboutBoxPage.GeneralInformation);

        private void tsbtnDescription_Click(object sender, EventArgs e) => SwitchAboutBoxPage(AboutBoxPage.Description);

        private void tsbtnFileInformation_Click(object sender, EventArgs e) => SwitchAboutBoxPage(AboutBoxPage.FileInformation);

        private void tsbtnTheme_Click(object sender, EventArgs e) => SwitchAboutBoxPage(AboutBoxPage.Theme);

        private void tsbtnApplicationDetails_Click(object sender, EventArgs e) => SwitchFileInformationPage(AboutBoxFileInformationPage.Application);

        private void tsbtnAssembliesDetails_Click(object sender, EventArgs e) => SwitchFileInformationPage(AboutBoxFileInformationPage.Assemblies);

        private void tsbtnAssemblyDetails_Click(object sender, EventArgs e) => SwitchFileInformationPage(AboutBoxFileInformationPage.AssemblyDetails);

        private void SwitchFileInformationPage(AboutBoxFileInformationPage page)
        {
            switch (page)
            {
                case AboutBoxFileInformationPage.Application:
                    tsbtnFileInformation.Checked = true;

                    kpnlApplicationDetails.Visible = true;

                    tsbtnAssembliesDetails.Checked = false;

                    kpnlAssembliesDetails.Visible = false;

                    tsbtnAssemblyDetails.Checked = false;

                    kpnlAssemblyDetails.Visible = false;
                    break;
                case AboutBoxFileInformationPage.Assemblies:
                    tsbtnFileInformation.Checked = false;

                    kpnlApplicationDetails.Visible = false;

                    tsbtnAssembliesDetails.Checked = true;

                    kpnlAssembliesDetails.Visible = true;

                    tsbtnAssemblyDetails.Checked = false;

                    kpnlAssemblyDetails.Visible = false;
                    break;
                case AboutBoxFileInformationPage.AssemblyDetails:
                    tsbtnFileInformation.Checked = false;

                    kpnlApplicationDetails.Visible = false;

                    tsbtnAssembliesDetails.Checked = false;

                    kpnlAssembliesDetails.Visible = false;

                    tsbtnAssemblyDetails.Checked = true;

                    kpnlAssemblyDetails.Visible = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(page), page, null);
            }
        }

        private void SwitchAboutBoxPage(AboutBoxPage page)
        {
            switch (page)
            {
                case AboutBoxPage.GeneralInformation:
                    tsbtnGeneralInformation.Checked = true;

                    kpnlGeneralInformation.Visible = true;

                    tsbtnDescription.Checked = false;

                    kpnlDescription.Visible = false;

                    tsbtnFileInformation.Checked = false;

                    kpnlFileInformation.Visible = false;

                    tsbtnTheme.Checked = false;

                    kpnlTheme.Visible = false;
                    break;
                case AboutBoxPage.Description:
                    tsbtnGeneralInformation.Checked = false;

                    kpnlGeneralInformation.Visible = false;

                    tsbtnDescription.Checked = true;

                    kpnlDescription.Visible = true;

                    tsbtnFileInformation.Checked = false;

                    kpnlFileInformation.Visible = false;

                    tsbtnTheme.Checked = false;

                    kpnlTheme.Visible = false;
                    break;
                case AboutBoxPage.FileInformation:
                    tsbtnGeneralInformation.Checked = false;

                    kpnlGeneralInformation.Visible = false;

                    tsbtnDescription.Checked = false;

                    kpnlDescription.Visible = false;

                    tsbtnFileInformation.Checked = true;

                    kpnlFileInformation.Visible = true;

                    tsbtnTheme.Checked = false;

                    kpnlTheme.Visible = false;
                    break;
                case AboutBoxPage.Theme:
                    tsbtnGeneralInformation.Checked = false;

                    kpnlGeneralInformation.Visible = false;

                    tsbtnDescription.Checked = false;

                    kpnlDescription.Visible = false;

                    tsbtnFileInformation.Checked = false;

                    kpnlFileInformation.Visible = false;

                    tsbtnTheme.Checked = true;

                    kpnlTheme.Visible = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(page), page, null);
            }
        }

        #endregion
    }
}

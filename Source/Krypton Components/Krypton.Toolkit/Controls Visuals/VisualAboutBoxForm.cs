#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal partial class VisualAboutBoxForm : KryptonForm
{
    #region Instance Fields

    private readonly bool _showToolkitButton;

    private readonly KryptonAboutBoxData _aboutBoxData;

    private readonly KryptonAboutToolkitData _aboutToolkitData;

    #endregion

    #region Identity

    public VisualAboutBoxForm(KryptonAboutBoxData aboutBoxData)
    {
        //SetInheritedControlOverride(); // Disabled as part of issue #2296. See the issue for details.
        InitializeComponent();

        _aboutBoxData = aboutBoxData;

        Startup(_aboutBoxData);

        kbtnOk.Text = KryptonManager.Strings.GeneralStrings.OK;

        kbtnSystemInformation.Text = KryptonManager.Strings.CustomStrings.SystemInformation;
    }

    public VisualAboutBoxForm(KryptonAboutBoxData aboutBoxData, KryptonAboutToolkitData aboutToolkitData)
    {
        //SetInheritedControlOverride(); // Disabled as part of issue #2296. See the issue for details.
        InitializeComponent();

        _showToolkitButton = aboutBoxData.ShowToolkitInformation ?? false;

        _aboutBoxData = aboutBoxData;

        _aboutToolkitData = aboutToolkitData;

        Startup(_showToolkitButton, _aboutBoxData, _aboutToolkitData);

        kbtnOk.Text = KryptonManager.Strings.GeneralStrings.OK;

        kbtnSystemInformation.Text = KryptonManager.Strings.CustomStrings.SystemInformation;
    }

    #endregion

    #region Implementation

    #region Basic Functionallity

    private void Startup(KryptonAboutBoxData aboutBoxData)
    {
        khgMain.ValuesPrimary.Image =
            aboutBoxData.HeaderImage ?? GenericImageResources.InformationSmall;

        khgMain.ValuesPrimary.Heading =
            $@"{KryptonManager.Strings.AboutBoxStrings.About} {aboutBoxData.ApplicationName}";

        pbxImage.Image = aboutBoxData.MainImage ?? GenericImageResources.InformationMedium;

        kwlCurrentTheme.Text = $@"{KryptonManager.Strings.CustomStrings.CurrentTheme}:";

        // ToDo: Review
        UpdateVersionLabel($"{KryptonManager.Strings.AboutBoxStrings.Version}: {KryptonAboutBoxUtilities.GetFileVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion}");

        if (aboutBoxData.UseFullBuiltOnDate != null || aboutBoxData.UseFullBuiltOnDate == false)
        {
            UpdateBuiltOnLabel($"{KryptonManager.Strings.AboutBoxStrings.BuildDate}: {KryptonAboutBoxUtilities.AssemblyBuildDate(Assembly.GetExecutingAssembly(), true).ToString("F")}");
        }
        else
        {
            UpdateBuiltOnLabel($"{KryptonManager.Strings.AboutBoxStrings.BuildDate}: {KryptonAboutBoxUtilities.AssemblyBuildDate(Assembly.GetExecutingAssembly(), true)}");
        }

        UpdateCopyrightLabel($"{KryptonManager.Strings.AboutBoxStrings.Copyright}: {KryptonAboutBoxUtilities.GetFileVersionInfo(Assembly.GetExecutingAssembly().Location).LegalCopyright}");

        UpdateDescription(KryptonAboutBoxUtilities.GetFileVersionInfo(Assembly.GetEntryAssembly()!.Location!).FileDescription!);

        kryptonWrapLabel5.Text = null;
    }

    private void UpdateDescription(string fileDescription) => krtbDescription.Text = fileDescription;

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

                kpnlApplication.Visible = true;

                tsbtnAssembliesDetails.Checked = false;

                kpnlAssemblies.Visible = false;

                tsbtnAssemblyDetails.Checked = false;

                kpnlAssemblyDetails.Visible = false;
                break;
            case AboutBoxFileInformationPage.Assemblies:
                tsbtnFileInformation.Checked = false;

                kpnlApplication.Visible = false;

                tsbtnAssembliesDetails.Checked = true;

                kpnlAssemblies.Visible = true;

                tsbtnAssemblyDetails.Checked = false;

                kpnlAssemblyDetails.Visible = false;
                break;
            case AboutBoxFileInformationPage.AssemblyDetails:
                tsbtnFileInformation.Checked = false;

                kpnlApplication.Visible = false;

                tsbtnAssembliesDetails.Checked = false;

                kpnlAssemblies.Visible = false;

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

                tsbtnToolkitInformation.Checked = false;

                kpnlToolkitInformation.Visible = false;
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

                tsbtnToolkitInformation.Checked = false;

                kpnlToolkitInformation.Visible = false;
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

                tsbtnToolkitInformation.Checked = false;

                kpnlToolkitInformation.Visible = false;
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

                tsbtnToolkitInformation.Checked = false;

                kpnlToolkitInformation.Visible = false;
                break;
            case AboutBoxPage.ToolkitInformation:
                tsbtnGeneralInformation.Checked = false;

                kpnlGeneralInformation.Visible = false;

                tsbtnDescription.Checked = false;

                kpnlDescription.Visible = false;

                tsbtnFileInformation.Checked = false;

                kpnlFileInformation.Visible = false;

                tsbtnTheme.Checked = false;

                kpnlTheme.Visible = false;

                tsbtnToolkitInformation.Checked = true;

                kpnlToolkitInformation.Visible = true;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(page), page, null);
        }
    }


    #endregion

    #region Toolkit Information

    private void Startup(bool showToolkitButton, KryptonAboutBoxData aboutBoxData, KryptonAboutToolkitData aboutToolkitData)
    {
        UpdateShowToolkitButtonUI(showToolkitButton);

        #region Basic Details

        khgMain.ValuesPrimary.Image =
            aboutBoxData.HeaderImage ?? GenericImageResources.InformationSmall;

        khgMain.ValuesPrimary.Heading =
            $@"{KryptonManager.Strings.AboutBoxStrings.About} {aboutBoxData.ApplicationName}";

        pbxImage.Image = aboutBoxData.MainImage ?? GenericImageResources.InformationMedium;

        kwlCurrentTheme.Text = $@"{KryptonManager.Strings.CustomStrings.CurrentTheme}:";

        // ToDo: Review
        UpdateVersionLabel($"{KryptonManager.Strings.AboutBoxStrings.Version}: {KryptonAboutBoxUtilities.GetFileVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion}");

        if (aboutBoxData.UseFullBuiltOnDate != null || aboutBoxData.UseFullBuiltOnDate == false)
        {
            UpdateBuiltOnLabel($"{KryptonManager.Strings.AboutBoxStrings.BuildDate}: {KryptonAboutBoxUtilities.AssemblyBuildDate(Assembly.GetExecutingAssembly(), true).ToString("F")}");
        }
        else
        {
            UpdateBuiltOnLabel($"{KryptonManager.Strings.AboutBoxStrings.BuildDate}: {KryptonAboutBoxUtilities.AssemblyBuildDate(Assembly.GetExecutingAssembly(), true)}");
        }

        UpdateCopyrightLabel($"{KryptonManager.Strings.AboutBoxStrings.Copyright}: {KryptonAboutBoxUtilities.GetFileVersionInfo(Assembly.GetExecutingAssembly().Location).LegalCopyright}");

        UpdateDescription(KryptonAboutBoxUtilities.GetFileVersionInfo(Assembly.GetEntryAssembly()!.Location).FileDescription!);

        kryptonWrapLabel5.Text = null;

        #endregion

        #region Toolkit Details

        // Adjust UI elements
        ShowDeveloperControls(aboutToolkitData.ShowDeveloperInformationButton);

        ShowDiscordControls(aboutToolkitData.ShowDiscordButton);

        ShowVersionControls(aboutToolkitData.ShowVersionInformationButton);

        ShowThemeControls(aboutToolkitData.ShowThemeOptions);

        ShowBuildDateLabel(aboutToolkitData.ShowBuildDate);

        UpdateBuiltOnText(string.Empty);

        // ToDo: Figure out why this does not work
        // UpdateBuiltOnText($@"{aboutToolkitData.BuildOnText}: {KryptonAboutBoxUtilities.AssemblyBuildDate(Assembly.LoadFile($@"{Application.ExecutablePath}\Krypton.Toolkit.dll"), false)}");

        UpdateCurrentThemeText($@"{aboutToolkitData.CurrentThemeText}:");

        ShowSystemInformationButton(aboutToolkitData.ShowSystemInformationButton);

        SwitchIcon(aboutToolkitData.ToolkitSupportType);

        ConcatanateGeneralInformationText(aboutToolkitData.GeneralInformationWelcomeText, aboutToolkitData.GeneralInformationLicenseText, aboutToolkitData.GeneralInformationLearnMoreText);

        UpdateDiscordText(aboutToolkitData.DiscordText);

        UpdateRepositoriesText(aboutToolkitData.RepositoryInformationText);

        UpdateDemosText(aboutToolkitData.DownloadDemosText);

        UpdateDocumentationText(aboutToolkitData.DownloadDocumentationText);

        UpdateColumnHeadings(aboutToolkitData.FileNameColumnHeaderText, aboutToolkitData.VersionColumnHeaderText);

        UpdateToolBarText(aboutToolkitData.ToolBarGeneralInformationText, aboutToolkitData.ToolBarDiscordText, aboutToolkitData.ToolBarDeveloperInformationText, aboutToolkitData.ToolBarVersionInformationText);

        UpdateGeneralInformationLinkArea(aboutToolkitData.LearnMoreLinkArea);

        UpdateDocumentationLinkArea(aboutToolkitData.DocumentationLinkArea);

        UpdateDiscordLinkArea(aboutToolkitData.DiscordLinkArea);

        UpdateDemosLinkArea(aboutToolkitData.DownloadDemosLinkArea);

        UpdateRepositoriesLinkArea(aboutToolkitData.RepositoryInformationLinkArea);

        GetReferenceAssemblyInformation();

        #endregion
    }

    private void UpdateShowToolkitButtonUI(bool showToolkitButton)
    {
        tssToolkitInformation.Visible = showToolkitButton;

        tsbtnToolkitInformation.Visible = showToolkitButton;
    }

    private void UpdateCurrentThemeText(string value) => klblCurrentTheme.Text = value;

    private void UpdateToolBarText(string toolBarGeneralInformationText, string toolBarDiscordText, string toolBarDeveloperInformationText, string toolBarVersionInformationText)
    {
        tsbtnGeneralInformation.Text = toolBarGeneralInformationText;

        tsbtnDiscord.Text = toolBarDiscordText;

        tsbtnDeveloperInformation.Text = toolBarDeveloperInformationText;

        tsbtnVersions.Text = toolBarVersionInformationText;
    }

    private void ShowBuildDateLabel(bool value)
    {
        klblBuiltOn.Visible = value;

        if (!value)
        {
            klblBuiltOn.Text = null;
        }
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

    private void SwitchIcon(ToolkitSupportType value)
    {
        switch (value)
        {
            case ToolkitSupportType.Canary:
                pbxLogo.Image = ToolkitLogoImageResources.Krypton_Canary;
                break;
            case ToolkitSupportType.Nightly:
                pbxLogo.Image = ToolkitLogoImageResources.Krypton_Nightly;
                break;
            case ToolkitSupportType.Stable:
                pbxLogo.Image = ToolkitLogoImageResources.Krypton_Stable;
                break;
            case ToolkitSupportType.LongTermSupport:
                //pbxLogo.Image = ToolkitLogoImageResources.Krypton_LTS;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(value), value, null);
        }
    }

    private void UpdateBuiltOnText(string value) => klblBuiltOn.Text = value;

    private void ConcatanateGeneralInformationText(string welcomeText, string licenseText, string learnMoreText)
    {
        // Note: Do not use verbatim string!
        string output = $"{welcomeText}\r\n\r\n{licenseText}: BSD-3-Clause\r\n\r\n{learnMoreText}";

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

    private void SwitchToolkitInformationPage(AboutToolkitPage page)
    {
        switch (page)
        {
            case AboutToolkitPage.GeneralInformation:
                kpnlToolkitGeneralInformation.Visible = true;

                kpnlDiscord.Visible = false;

                kpnlDeveloperInformation.Visible = false;

                kpnlVersions.Visible = false;

                tsbtnToolkitGeneralInformation.Checked = true;

                tsbtnDiscord.Checked = false;

                tsbtnDeveloperInformation.Checked = false;

                tsbtnVersions.Checked = false;
                break;
            case AboutToolkitPage.Discord:
                kpnlToolkitGeneralInformation.Visible = false;

                kpnlDiscord.Visible = true;

                kpnlDeveloperInformation.Visible = false;

                kpnlVersions.Visible = false;

                tsbtnToolkitGeneralInformation.Checked = false;

                tsbtnDiscord.Checked = true;

                tsbtnDeveloperInformation.Checked = false;

                tsbtnVersions.Checked = false;
                break;
            case AboutToolkitPage.DeveloperInformation:
                kpnlToolkitGeneralInformation.Visible = false;

                kpnlDiscord.Visible = false;

                kpnlDeveloperInformation.Visible = true;

                kpnlVersions.Visible = false;

                tsbtnToolkitGeneralInformation.Checked = false;

                tsbtnDiscord.Checked = false;

                tsbtnDeveloperInformation.Checked = true;

                tsbtnVersions.Checked = false;
                break;
            case AboutToolkitPage.Versions:
                kpnlToolkitGeneralInformation.Visible = false;

                kpnlDiscord.Visible = false;

                kpnlDeveloperInformation.Visible = false;

                kpnlVersions.Visible = true;

                tsbtnToolkitGeneralInformation.Checked = false;

                tsbtnDiscord.Checked = false;

                tsbtnDeveloperInformation.Checked = false;

                tsbtnVersions.Checked = true;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(page), page, null);
        }
    }

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

            // Fill data grid view
            kdgvVersions.Rows.Add(assembly.Name!, assembly.Version!.ToString());
        }
    }

    private void ShowSystemInformationButton(bool? value) => kbtnSystemInformation.Visible = value ?? true;

    private void tsbtnToolkitInformation_Click(object sender, EventArgs e) => SwitchAboutBoxPage(AboutBoxPage.ToolkitInformation);

    private void tsbtnToolkitGeneralInformation_Click(object sender, EventArgs e) => SwitchToolkitInformationPage(AboutToolkitPage.GeneralInformation);

    private void tsbtnDiscord_Click(object sender, EventArgs e) => SwitchToolkitInformationPage(AboutToolkitPage.Discord);

    private void tsbtnDeveloperInformation_Click(object sender, EventArgs e) => SwitchToolkitInformationPage(AboutToolkitPage.DeveloperInformation);

    private void tsbtnVersions_Click(object sender, EventArgs e) => SwitchToolkitInformationPage(AboutToolkitPage.Versions);

    private void klwlblGeneralInformation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => GlobalToolkitUtilities.LaunchProcess(@"https://github.com/Krypton-Suite/Standard-Toolkit");

    private void klwlblDiscord_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => GlobalToolkitUtilities.LaunchProcess(@"https://discord.gg/CRjF6fY");

    private void klwlblRepositories_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => GlobalToolkitUtilities.LaunchProcess(@"https://github.com/orgs/Krypton-Suite/repositories");

    private void klwlblDocumentation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => GlobalToolkitUtilities.LaunchProcess(@"https://github.com/Krypton-Suite/Help-Files/releases");

    private void klwlblDemos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => GlobalToolkitUtilities.LaunchProcess(@"https://github.com/Krypton-Suite/Standard-Toolkit-Demos/releases");

    #endregion

    #endregion
}
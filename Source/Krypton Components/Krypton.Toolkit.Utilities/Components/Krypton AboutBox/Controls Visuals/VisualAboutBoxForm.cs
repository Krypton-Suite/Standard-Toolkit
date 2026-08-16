#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2023 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit.Utilities.Properties;

namespace Krypton.Toolkit.Utilities;

internal partial class VisualAboutBoxForm : KryptonForm
{
    #region Instance Fields

    private readonly bool _showToolkitButton;
    private readonly bool _useRtl;
    private readonly bool _useFullBuiltOnDate;
    private readonly KryptonAboutBoxData _aboutBoxData;
    private readonly KryptonAboutToolkitData _aboutToolkitData;
    private readonly KryptonAboutToolkitData _defaultToolkitData;
    private Image? _ownedComposedMainImage;

    #endregion

    #region Identity

    public VisualAboutBoxForm(KryptonAboutBoxData aboutBoxData)
        : this(aboutBoxData, new KryptonAboutToolkitData())
    {
    }

    public VisualAboutBoxForm(KryptonAboutBoxData aboutBoxData, KryptonAboutToolkitData aboutToolkitData)
    {
        InitializeComponent();

        _aboutBoxData = aboutBoxData;
        _aboutToolkitData = string.IsNullOrEmpty(aboutToolkitData.HeaderText)
            ? new KryptonAboutToolkitData()
            : aboutToolkitData;
        _defaultToolkitData = new KryptonAboutToolkitData();
        _showToolkitButton = aboutBoxData.ShowToolkitInformation ?? false;
        _useRtl = aboutBoxData.UseRtlLayout == KryptonUseRTLLayout.Yes;
        _useFullBuiltOnDate = aboutBoxData.UseFullBuiltOnDate == true;

        ApplyRtlLayout(_useRtl);

        kbtnOk.Text = KryptonManager.Strings.GeneralStrings.OK;
        kbtnOk.DialogResult = DialogResult.OK;
        AcceptButton = kbtnOk;
        CancelButton = kbtnOk;
        StartPosition = FormStartPosition.CenterParent;
        kbtnSystemInformation.Text = KryptonManager.Strings.CustomStrings.SystemInformation;

        Startup();
    }

    #endregion

    #region Implementation

    private void ApplyRtlLayout(bool useRtl)
    {
        RightToLeft = useRtl ? RightToLeft.Yes : RightToLeft.No;
        RightToLeftLayout = useRtl;
    }

    private void Startup()
    {
        Assembly assembly = KryptonAboutBoxUtilities.ResolveAssembly(_aboutBoxData);
        KryptonAboutBoxUtilities.AssemblyIdentity identity =
            KryptonAboutBoxUtilities.GetAssemblyIdentity(assembly, _aboutBoxData);

        string heading = $"{KryptonManager.Strings.AboutBoxStrings.About} {identity.ApplicationName}";
        khgMain.ValuesPrimary.Image = _aboutBoxData.HeaderImage ?? Resources.InformationSmall;
        khgMain.ValuesPrimary.Heading = heading;
        Text = heading;

        pbxImage.Image = _aboutBoxData.MainImage ?? Resources.InformationMedium;
        ApplyMainImageOverlay(_aboutBoxData.MainImageOverlay);

        kwlCurrentTheme.Text = $@"{KryptonManager.Strings.CustomStrings.CurrentTheme}:";

        UpdateVersionLabel($"{KryptonManager.Strings.AboutBoxStrings.Version}: {identity.Version}");
        UpdateBuiltOnLabel(KryptonAboutBoxUtilities.FormatBuildAndBinaryDates(assembly, _useFullBuiltOnDate));
        UpdateCopyrightLabel(string.IsNullOrEmpty(identity.Copyright)
            ? $"{KryptonManager.Strings.AboutBoxStrings.Copyright}:"
            : $"{KryptonManager.Strings.AboutBoxStrings.Copyright}: {identity.Copyright}");
        kryptonWrapLabel5.Text = string.IsNullOrEmpty(identity.Company)
            ? string.Empty
            : $"{KryptonManager.Strings.AboutBoxStrings.Company}: {identity.Company}";

        UpdateDescription(identity.Description);

        KryptonAboutBoxUtilities.ConfigureReadOnlyGrid(kdgvApplication);
        KryptonAboutBoxUtilities.ConfigureReadOnlyGrid(kdgvAssemblies);
        KryptonAboutBoxUtilities.ConfigureReadOnlyGrid(kdgvVersions);
        KryptonAboutBoxUtilities.PopulateBasicApplicationInformation(kdgvApplication, assembly);
        KryptonAboutBoxUtilities.PopulateAssemblies(kdgvAssemblies, _useFullBuiltOnDate);
        kiadAssemblyDetails.LoadAssemblies(assembly);

        bool showSystemInformation = _aboutBoxData.ShowSystemInformationButton
                                     ?? _aboutToolkitData.ShowSystemInformationButton;
        ShowSystemInformationButton(showSystemInformation);

        UpdateShowToolkitButtonUI(_showToolkitButton);
        if (_showToolkitButton)
        {
            StartupToolkitInformation();
        }

        SwitchAboutBoxPage(AboutBoxPage.GeneralInformation);
        SwitchFileInformationPage(AboutBoxFileInformationPage.Application);
    }

    private void StartupToolkitInformation()
    {
        ShowDeveloperControls(_aboutToolkitData.ShowDeveloperInformationButton);
        ShowDiscordControls(_aboutToolkitData.ShowDiscordButton);
        ShowVersionControls(_aboutToolkitData.ShowVersionInformationButton);
        ShowThemeControls(_aboutToolkitData.ShowThemeOptions);
        ShowBuildDateLabel(_aboutToolkitData.ShowBuildDate);

        Assembly toolkitAssembly = typeof(KryptonManager).Assembly;
        string toolkitBuiltOnText = KryptonAboutBoxUtilities.FormatBuildDate(
            KryptonAboutBoxUtilities.GetBinaryBuildDateTime(toolkitAssembly), _useFullBuiltOnDate);
        UpdateBuiltOnText(string.IsNullOrEmpty(toolkitBuiltOnText)
            ? _aboutToolkitData.BuildOnText
            : $"{_aboutToolkitData.BuildOnText}: {toolkitBuiltOnText}");

        UpdateCurrentThemeText($@"{_aboutToolkitData.CurrentThemeText}:");
        SwitchIcon(_aboutToolkitData.ToolkitSupportType);
        ConcatenateGeneralInformationText(_aboutToolkitData.GeneralInformationWelcomeText,
            _aboutToolkitData.GeneralInformationLicenseText, _aboutToolkitData.GeneralInformationLearnMoreText);
        UpdateDiscordText(_aboutToolkitData.DiscordText);
        UpdateRepositoriesText(_aboutToolkitData.RepositoryInformationText);
        UpdateDemosText(_aboutToolkitData.DownloadDemosText);
        UpdateDocumentationText(_aboutToolkitData.DownloadDocumentationText);
        UpdateColumnHeadings(_aboutToolkitData.FileNameColumnHeaderText, _aboutToolkitData.VersionColumnHeaderText);
        UpdateToolBarText(_aboutToolkitData.ToolBarGeneralInformationText, _aboutToolkitData.ToolBarDiscordText,
            _aboutToolkitData.ToolBarDeveloperInformationText, _aboutToolkitData.ToolBarVersionInformationText);
        ApplyToolkitLinkAreas();
        GetReferenceAssemblyInformation(toolkitAssembly);
        SwitchToolkitInformationPage(AboutToolkitPage.GeneralInformation);
    }

    private void ApplyMainImageOverlay(KryptonOverlayImage overlay)
    {
        DisposeOwnedComposedMainImage();

        if (overlay.IsEmpty || pbxImage.Image == null)
        {
            return;
        }

        Bitmap? composed = GraphicsExtensions.TryComposeOverlay(pbxImage.Image, overlay, _useRtl);
        if (composed != null)
        {
            _ownedComposedMainImage = composed;
            pbxImage.Image = composed;
        }
    }

    private void DisposeOwnedComposedMainImage()
    {
        if (_ownedComposedMainImage == null)
        {
            return;
        }

        if (ReferenceEquals(pbxImage.Image, _ownedComposedMainImage))
        {
            pbxImage.Image = null;
        }

        _ownedComposedMainImage.Dispose();
        _ownedComposedMainImage = null;
    }

    private void UpdateDescription(string fileDescription) => krtbDescription.Text = fileDescription;

    private void UpdateCopyrightLabel(string value) => kwlCopyright.Text = value;

    private void UpdateBuiltOnLabel(string value) => kwlBuiltOn.Text = value;

    private void UpdateVersionLabel(string value) => kwlVersionLabel.Text = value;

    private void kbtnOk_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }

    private void kbtnSystemInformation_Click(object sender, EventArgs e) =>
        KryptonAboutBoxUtilities.LaunchSystemInformation();

    private void tsbtnGeneralInformation_Click(object sender, EventArgs e) =>
        SwitchAboutBoxPage(AboutBoxPage.GeneralInformation);

    private void tsbtnDescription_Click(object sender, EventArgs e) =>
        SwitchAboutBoxPage(AboutBoxPage.Description);

    private void tsbtnFileInformation_Click(object sender, EventArgs e) =>
        SwitchAboutBoxPage(AboutBoxPage.FileInformation);

    private void tsbtnTheme_Click(object sender, EventArgs e) =>
        SwitchAboutBoxPage(AboutBoxPage.Theme);

    private void tsbtnApplicationDetails_Click(object sender, EventArgs e) =>
        SwitchFileInformationPage(AboutBoxFileInformationPage.Application);

    private void tsbtnAssembliesDetails_Click(object sender, EventArgs e) =>
        SwitchFileInformationPage(AboutBoxFileInformationPage.Assemblies);

    private void tsbtnAssemblyDetails_Click(object sender, EventArgs e) =>
        SwitchFileInformationPage(AboutBoxFileInformationPage.AssemblyDetails);

    private void SwitchFileInformationPage(AboutBoxFileInformationPage page)
    {
        switch (page)
        {
            case AboutBoxFileInformationPage.Application:
                tsbtnApplicationDetails.Checked = true;
                kpnlApplication.Visible = true;
                tsbtnAssembliesDetails.Checked = false;
                kpnlAssemblies.Visible = false;
                tsbtnAssemblyDetails.Checked = false;
                kpnlAssemblyDetails.Visible = false;
                break;
            case AboutBoxFileInformationPage.Assemblies:
                tsbtnApplicationDetails.Checked = false;
                kpnlApplication.Visible = false;
                tsbtnAssembliesDetails.Checked = true;
                kpnlAssemblies.Visible = true;
                tsbtnAssemblyDetails.Checked = false;
                kpnlAssemblyDetails.Visible = false;
                break;
            case AboutBoxFileInformationPage.AssemblyDetails:
                tsbtnApplicationDetails.Checked = false;
                kpnlApplication.Visible = false;
                tsbtnAssembliesDetails.Checked = false;
                kpnlAssemblies.Visible = false;
                tsbtnAssemblyDetails.Checked = true;
                kpnlAssemblyDetails.Visible = true;
                break;
            default:
                ThrowHelper.ThrowArgumentOutOfRangeException(nameof(page), page, null);
                return;
        }
    }

    private void SwitchAboutBoxPage(AboutBoxPage page)
    {
        tsbtnGeneralInformation.Checked = page == AboutBoxPage.GeneralInformation;
        kpnlGeneralInformation.Visible = page == AboutBoxPage.GeneralInformation;
        tsbtnDescription.Checked = page == AboutBoxPage.Description;
        kpnlDescription.Visible = page == AboutBoxPage.Description;
        tsbtnFileInformation.Checked = page == AboutBoxPage.FileInformation;
        kpnlFileInformation.Visible = page == AboutBoxPage.FileInformation;
        tsbtnTheme.Checked = page == AboutBoxPage.Theme;
        kpnlTheme.Visible = page == AboutBoxPage.Theme;
        tsbtnToolkitInformation.Checked = page == AboutBoxPage.ToolkitInformation;
        kpnlToolkitInformation.Visible = page == AboutBoxPage.ToolkitInformation;

        // Dock.Fill siblings stay stacked; bring the active page to the front after showing it.
        KryptonPanel? active = page switch
        {
            AboutBoxPage.GeneralInformation => kpnlGeneralInformation,
            AboutBoxPage.Description => kpnlDescription,
            AboutBoxPage.FileInformation => kpnlFileInformation,
            AboutBoxPage.Theme => kpnlTheme,
            AboutBoxPage.ToolkitInformation => kpnlToolkitInformation,
            _ => null
        };
        active?.BringToFront();
    }

    private void UpdateShowToolkitButtonUI(bool showToolkitButton)
    {
        tssToolkitInformation.Visible = showToolkitButton;
        tsbtnToolkitInformation.Visible = showToolkitButton;
    }

    private void UpdateCurrentThemeText(string value) => klblCurrentTheme.Text = value;

    private void UpdateToolBarText(string toolBarGeneralInformationText, string toolBarDiscordText,
        string toolBarDeveloperInformationText, string toolBarVersionInformationText)
    {
        tsbtnToolkitGeneralInformation.Text = toolBarGeneralInformationText;
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
        pbxLogo.Image = value switch
        {
            ToolkitSupportType.Canary => Resources.Krypton_Canary,
            ToolkitSupportType.Nightly => Resources.Krypton_Nightly,
            ToolkitSupportType.Stable => Resources.Krypton_Stable,
            ToolkitSupportType.LongTermSupport => Resources.Krypton_LTS,
            _ => ThrowHelper.ThrowArgumentOutOfRangeException<Image>(nameof(value), value, null),
        };
    }

    private void UpdateBuiltOnText(string value) => klblBuiltOn.Text = value;

    private void ConcatenateGeneralInformationText(string welcomeText, string licenseText, string learnMoreText)
    {
        string output = $"{welcomeText}\r\n\r\n{licenseText}: BSD-3-Clause\r\n\r\n{learnMoreText}";
        klwlblGeneralInformation.Text = output;
    }

    private void ApplyToolkitLinkAreas()
    {
        klwlblGeneralInformation.LinkArea = KryptonAboutBoxUtilities.ResolveLinkArea(
            klwlblGeneralInformation.Text,
            _aboutToolkitData.LearnMoreLinkArea,
            _defaultToolkitData.LearnMoreLinkArea,
            _aboutToolkitData.GeneralInformationLearnMoreText);
        klwlblDiscord.LinkArea = KryptonAboutBoxUtilities.ResolveLinkArea(
            klwlblDiscord.Text, _aboutToolkitData.DiscordLinkArea, _defaultToolkitData.DiscordLinkArea, null);
        klwlblRepositories.LinkArea = KryptonAboutBoxUtilities.ResolveLinkArea(
            klwlblRepositories.Text, _aboutToolkitData.RepositoryInformationLinkArea,
            _defaultToolkitData.RepositoryInformationLinkArea, null);
        klwlblDemos.LinkArea = KryptonAboutBoxUtilities.ResolveLinkArea(
            klwlblDemos.Text, _aboutToolkitData.DownloadDemosLinkArea, _defaultToolkitData.DownloadDemosLinkArea, null);
        klwlblDocumentation.LinkArea = KryptonAboutBoxUtilities.ResolveLinkArea(
            klwlblDocumentation.Text, _aboutToolkitData.DocumentationLinkArea,
            _defaultToolkitData.DocumentationLinkArea, null);
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
        kpnlToolkitGeneralInformation.Visible = page == AboutToolkitPage.GeneralInformation;
        tsbtnToolkitGeneralInformation.Checked = page == AboutToolkitPage.GeneralInformation;
        kpnlDiscord.Visible = page == AboutToolkitPage.Discord;
        tsbtnDiscord.Checked = page == AboutToolkitPage.Discord;
        kpnlDeveloperInformation.Visible = page == AboutToolkitPage.DeveloperInformation;
        tsbtnDeveloperInformation.Checked = page == AboutToolkitPage.DeveloperInformation;
        kpnlVersions.Visible = page == AboutToolkitPage.Versions;
        tsbtnVersions.Checked = page == AboutToolkitPage.Versions;
    }

    private void GetReferenceAssemblyInformation(Assembly toolkitAssembly)
    {
        kdgvVersions.Rows.Clear();
        foreach (AssemblyName assembly in toolkitAssembly.GetReferencedAssemblies())
        {
            kdgvVersions.Rows.Add(assembly.Name ?? string.Empty, assembly.Version?.ToString() ?? string.Empty);
        }
    }

    private void ShowSystemInformationButton(bool? value) => kbtnSystemInformation.Visible = value ?? true;

    private void tsbtnToolkitInformation_Click(object sender, EventArgs e) =>
        SwitchAboutBoxPage(AboutBoxPage.ToolkitInformation);

    private void tsbtnToolkitGeneralInformation_Click(object sender, EventArgs e) =>
        SwitchToolkitInformationPage(AboutToolkitPage.GeneralInformation);

    private void tsbtnDiscord_Click(object sender, EventArgs e) =>
        SwitchToolkitInformationPage(AboutToolkitPage.Discord);

    private void tsbtnDeveloperInformation_Click(object sender, EventArgs e) =>
        SwitchToolkitInformationPage(AboutToolkitPage.DeveloperInformation);

    private void tsbtnVersions_Click(object sender, EventArgs e) =>
        SwitchToolkitInformationPage(AboutToolkitPage.Versions);

    private void klwlblGeneralInformation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) =>
        GlobalToolkitUtilities.LaunchProcess(@"https://github.com/Krypton-Suite/Standard-Toolkit");

    private void klwlblDiscord_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) =>
        GlobalToolkitUtilities.LaunchProcess(@"https://discord.gg/CRjF6fY");

    private void klwlblRepositories_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) =>
        GlobalToolkitUtilities.LaunchProcess(@"https://github.com/orgs/Krypton-Suite/repositories");

    private void klwlblDocumentation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) =>
        GlobalToolkitUtilities.LaunchProcess(@"https://github.com/Krypton-Suite/Help-Files/releases");

    private void klwlblDemos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) =>
        GlobalToolkitUtilities.LaunchProcess(@"https://github.com/Krypton-Suite/Standard-Toolkit-Demos/releases");

    /// <inheritdoc />
    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        DisposeOwnedComposedMainImage();
        base.OnFormClosed(e);
    }

    #endregion
}

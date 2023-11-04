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
    /// <summary>The public interface to the <see cref="KryptonAboutToolkitForm"/> class.</summary>
    [ToolboxItem(false)]
    [DesignerCategory(@"code")]
    public static class KryptonAboutToolkit
    {
        #region Implementation

        //// <summary>Shows a new <see cref="KryptonAboutToolkitForm"/>.</summary>
        //// <param name="data">The data to pass through.</param>
        //// <returns>A new <see cref="KryptonAboutToolkitForm"/> with the specified data.</returns>
        //public static DialogResult Show(KryptonAboutToolkitData data)
        //    => ShowCore(data.HeaderText, data.GeneralInformationWelcomeText, data.GeneralInformationLicenseText,
        //                data.GeneralInformationLearnMoreText, data.CurrentThemeText, data.DiscordText,
        //                data.RepositoryInformationText, data.DownloadDocumentationText, data.DownloadDemosText,
        //                data.FileNameColumnHeaderText, data.VersionColumnHeaderText, data.ToolBarGeneralInformationText,
        //                data.ToolBarDiscordText, data.ToolBarDeveloperInformationText, data.ToolBarVersionInformationText,
        //                data.LearnMoreLinkArea, data.DiscordLinkArea, data.RepositoryInformationLinkArea,
        //                data.DownloadDemosLinkArea, data.DocumentationLinkArea, data.ToolkitType,
        //                data.ShowDiscordButton, data.ShowDeveloperInformationButton,
        //                data.ShowVersionInformationButton,
        //                data.ShowThemeOptions, data.ShowSystemInformationButton);

        public static DialogResult Show(KryptonAboutToolkitData aboutToolkitData)
            => ShowCore(aboutToolkitData);

        #endregion

        #region Implementation

        /// <summary>Shows the <see cref="KryptonAboutToolkitForm"/> with specified data.</summary>
        /// <param name="headerText">The header text.</param>
        /// <param name="generalInformationWelcomeText">The general information welcome text.</param>
        /// <param name="generalInformationLicenseText">The general information license text.</param>
        /// <param name="generalInformationLearnMoreText">The general information learn more text.</param>
        /// <param name="currentThemeText">The current theme text.</param>
        /// <param name="discordText">The discord text.</param>
        /// <param name="repositoryInformationText">The repository information text.</param>
        /// <param name="downloadDocumentationText">The download documentation text.</param>
        /// <param name="downloadDemosText">The download demos text.</param>
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
        /// <param name="toolkitType">Type of the toolkit.</param>
        /// <param name="showDiscordButton">The show discord button.</param>
        /// <param name="showDeveloperInformationButton">The show developer information button.</param>
        /// <param name="showVersionInformationButton">The show version information button.</param>
        /// <param name="showThemeOptions">The show theme options.</param>
        /// <param name="showSystemInformationButton">The show system information button.</param>
        /// <returns>A <see cref="KryptonAboutToolkitForm"/> with the specified data.</returns>
        private static DialogResult ShowCore(string? headerText, string? generalInformationWelcomeText,
            string? generalInformationLicenseText,
            string? generalInformationLearnMoreText, string? currentThemeText, string? discordText,
            string? repositoryInformationText, string? downloadDocumentationText, string? downloadDemosText,
            string? fileNameColumnHeaderText, string? versionColumnHeaderText,
            string? toolBarGeneralInformationText, string? toolBarDiscordText,
            string? toolBarDeveloperInformationText, string? toolBarVersionInformationText,
            LinkArea? learnMoreLinkArea,
            LinkArea? discordLinkArea, LinkArea? repositoryInformationLinkArea, LinkArea? downloadDemosLinkArea,
            LinkArea? documentationLinkArea, ToolkitType? toolkitType, bool? showDiscordButton,
            bool? showDeveloperInformationButton,
            bool? showVersionInformationButton, bool? showThemeOptions, bool? showSystemInformationButton)
        {
            using var kat = new KryptonAboutToolkitForm(headerText, generalInformationWelcomeText,
                generalInformationLicenseText, generalInformationLearnMoreText, currentThemeText, discordText,
                repositoryInformationText, downloadDocumentationText, downloadDemosText, fileNameColumnHeaderText,
                versionColumnHeaderText, toolBarGeneralInformationText, toolBarDiscordText,
                toolBarDeveloperInformationText, toolBarVersionInformationText,
                learnMoreLinkArea, discordLinkArea,
                repositoryInformationLinkArea,
                downloadDemosLinkArea, documentationLinkArea, toolkitType, showDiscordButton, showDeveloperInformationButton,
                showVersionInformationButton, showThemeOptions, showSystemInformationButton);

            return kat.ShowDialog();
        }

        private static DialogResult ShowCore(KryptonAboutToolkitData aboutToolkitData)
        {
            using var kat = new KryptonAboutToolkitForm(aboutToolkitData);

            return kat.ShowDialog();
        }

        #endregion
    }
}
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
        #region Public

        /// <summary>Shows the <see cref="KryptonAboutToolkitForm"/>.</summary>
        /// <returns>A <see cref="KryptonAboutToolkitForm"/> with default values</returns>
        public static DialogResult Show() => ShowCore(null, null, null, null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null, null, null);

        /// <summary>Shows the <see cref="KryptonAboutToolkitForm"/>.</summary>
        /// <param name="headerText">The header text.</param>
        /// <param name="generalInformationFirstLine">The general information first line.</param>
        /// <param name="generalInformationSecondLine">The general information second line.</param>
        /// <param name="generalInformationThirdLine">The general information third line.</param>
        /// <param name="currentThemeText">The current theme text.</param>
        /// <param name="discordText">The discord text.</param>
        /// <param name="repositoryInformationText">The repository information text.</param>
        /// <param name="downloadDocumentationText">The download documentation text.</param>
        /// <param name="downloadDemosText">The download demos text.</param>
        /// <param name="fileNameColumnHeaderText">The file name column header text.</param>
        /// <param name="versionColumnHeaderText">The version column header text.</param>
        /// <param name="generalInformationLinkArea">The general information link area.</param>
        /// <param name="discordLinkArea">The discord link area.</param>
        /// <param name="repositoryInformationLinkArea">The repository information link area.</param>
        /// <param name="downloadDemosLinkArea">The download demos link area.</param>
        /// <param name="documentationLinkArea">The documentation link area.</param>
        /// <param name="toolkitType">Type of the toolkit.</param>
        /// <param name="showDiscordButton">The show discord button.</param>
        /// <param name="showDeveloperButton">The show developer button.</param>
        /// <param name="showVersionInformationButton">The show version information button.</param>
        /// <param name="showThemeOptions">The show theme options.</param>
        /// <param name="showSystemInformationButton">The show system information button.</param>
        /// <returns>The <see cref="KryptonAboutToolkitForm"/> with the specified information.</returns>
        public static DialogResult Show(string? headerText, string? generalInformationFirstLine,
            string? generalInformationSecondLine,
            string? generalInformationThirdLine, string? currentThemeText, string? discordText,
            string? repositoryInformationText, string? downloadDocumentationText, string? downloadDemosText,
            string? fileNameColumnHeaderText, string? versionColumnHeaderText, LinkArea? generalInformationLinkArea,
            LinkArea? discordLinkArea, LinkArea? repositoryInformationLinkArea, LinkArea? downloadDemosLinkArea,
            LinkArea? documentationLinkArea, ToolkitType? toolkitType, bool? showDiscordButton,
            bool? showDeveloperButton,
            bool? showVersionInformationButton, bool? showThemeOptions, bool? showSystemInformationButton) => ShowCore(
            headerText, generalInformationFirstLine, generalInformationSecondLine, generalInformationThirdLine,
            currentThemeText, discordText, repositoryInformationText, downloadDocumentationText, downloadDemosText,
            fileNameColumnHeaderText, versionColumnHeaderText, generalInformationLinkArea, discordLinkArea,
            repositoryInformationLinkArea, downloadDemosLinkArea, documentationLinkArea, toolkitType, showDiscordButton,
            showDeveloperButton, showVersionInformationButton, showThemeOptions, showSystemInformationButton);

        #endregion

        #region Implementation

        private static DialogResult ShowCore(string? headerText, string? generalInformationFirstLine,
            string? generalInformationSecondLine,
            string? generalInformationThirdLine, string? currentThemeText, string? discordText,
            string? repositoryInformationText, string? downloadDocumentationText, string? downloadDemosText,
            string? fileNameColumnHeaderText, string? versionColumnHeaderText, LinkArea? generalInformationLinkArea,
            LinkArea? discordLinkArea, LinkArea? repositoryInformationLinkArea, LinkArea? downloadDemosLinkArea,
            LinkArea? documentationLinkArea, ToolkitType? toolkitType, bool? showDiscordButton,
            bool? showDeveloperButton,
            bool? showVersionInformationButton, bool? showThemeOptions, bool? showSystemInformationButton)
        {
            using var kat = new KryptonAboutToolkitForm(headerText, generalInformationFirstLine,
                generalInformationSecondLine, generalInformationThirdLine, currentThemeText, discordText,
                repositoryInformationText, downloadDocumentationText, downloadDemosText, fileNameColumnHeaderText,
                versionColumnHeaderText, generalInformationLinkArea, discordLinkArea, repositoryInformationLinkArea,
                downloadDemosLinkArea, documentationLinkArea, toolkitType, showDiscordButton, showDeveloperButton,
                showVersionInformationButton, showThemeOptions, showSystemInformationButton);

            return kat.ShowDialog();
        }

        #endregion
    }
}
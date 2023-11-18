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

        /// <summary>Shows a new <see cref="KryptonAboutToolkitForm"/>.</summary>
        /// <param name="aboutToolkitData">The data to pass through.</param>
        /// <returns>A new <see cref="KryptonAboutToolkitForm"/> with the specified data.</returns>
        public static DialogResult Show(KryptonAboutToolkitData aboutToolkitData)
            => ShowCore(aboutToolkitData);

        #endregion

        #region Implementation

        private static DialogResult ShowCore(KryptonAboutToolkitData aboutToolkitData)
        {
            using var kat = new KryptonAboutToolkitForm(aboutToolkitData);

            return kat.ShowDialog();
        }

        #endregion
    }
}
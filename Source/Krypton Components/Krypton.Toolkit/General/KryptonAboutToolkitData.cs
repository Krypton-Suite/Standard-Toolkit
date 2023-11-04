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
    public struct KryptonAboutToolkitData
    {
        #region Instance Fields

        /// <summary>Shows the discord button.</summary>
        public bool ShowDiscordButton;

        /// <summary>Shows the developer information button.</summary>
        public bool ShowDeveloperInformationButton;

        /// <summary>Shows the version information button.</summary>
        public bool ShowVersionInformationButton;

        /// <summary>Shows the theme options.</summary>
        public bool ShowThemeOptions;

        /// <summary>The show system information button.</summary>
        public bool ShowSystemInformationButton;

        //public Font CommonFont;
        //public Font CurrentThemeFont;
        //public Font HeaderFont;

        /// <summary>The toolkit type.</summary>
        public ToolkitType ToolkitType;

        /// <summary>The header text.</summary>
        public string HeaderText;

        /// <summary>The current theme text.</summary>
        public string CurrentThemeText;

        /// <summary>The general information welcome text.</summary>
        public string GeneralInformationWelcomeText;

        /// <summary>The general information license text.</summary>
        public string GeneralInformationLicenseText;

        /// <summary>The general information learn more text.</summary>
        public string GeneralInformationLearnMoreText;

        /// <summary>The discord text.</summary>
        public string DiscordText;

        /// <summary>The repository information text.</summary>
        public string RepositoryInformationText;

        /// <summary>The download documentation text.</summary>
        public string DownloadDocumentationText;

        /// <summary>The download demos text.</summary>
        public string DownloadDemosText;

        /// <summary>The file name column header text.</summary>
        public string FileNameColumnHeaderText;

        /// <summary>The version column header text.</summary>
        public string VersionColumnHeaderText;

        /// <summary>The tool bar general information text.</summary>
        public string ToolBarGeneralInformationText;

        /// <summary>The tool bar discord text.</summary>
        public string ToolBarDiscordText;

        /// <summary>The tool bar developer information text.</summary>
        public string ToolBarDeveloperInformationText;

        /// <summary>The tool bar version information text.</summary>
        public string ToolBarVersionInformationText;

        /// <summary>The learn more link area.</summary>
        public LinkArea LearnMoreLinkArea;

        /// <summary>The discord link area.</summary>
        public LinkArea DiscordLinkArea;

        /// <summary>The repository information link area.</summary>
        public LinkArea RepositoryInformationLinkArea;

        /// <summary>The download demos link area.</summary>
        public LinkArea DownloadDemosLinkArea;

        /// <summary>The documentation link area.</summary>
        public LinkArea DocumentationLinkArea;

        #endregion

        #region Identity

        public KryptonAboutToolkitData()
        {

        }

        #endregion
    }
}
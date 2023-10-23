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
    [ToolboxItem(false)]
    [DesignerCategory(@"code")]
    public class AboutToolkitValues : Storage
    {
        #region Static Fields

        private const string DEFAULT_GENERAL_INFORMATION_FIRST_LINE = @"";

        private const string DEFAULT_GENERAL_INFORMATION_SECOND_LINE = @"";

        private const string DEFAULT_GENERAL_INFORMATION_THIRD_LINE = @"";

        private const string DEFAULT_JOIN_DISCORD_SERVER = @"";

        private const string DEFAULT_VIEW_REPOSITORIES = @"";

        private const string DEFAULT_DOWNLOAD_DOCUMENTATION = @"";

        private const string DEFAULT_DOWNLOAD_DEMOS = @"";

        #endregion

        public override bool IsDefault { get; }
    }
}
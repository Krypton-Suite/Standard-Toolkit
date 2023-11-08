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
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class KryptonAboutBoxStrings : GlobalId
    {
        #region Static Strings

        private const string DEFAULT_ABOUT = @"About";

        private const string DEFAULT_TITLE = @"Title";

        private const string DEFAULT_COPYRIGHT = @"Copyright";

        private const string DEFAULT_DESCRIPTION = @"Description";

        private const string DEFAULT_COMPANY = @"Company";

        private const string DEFAULT_PRODUCT = @"Product";

        private const string DEFAULT_TRADE_MARK = @"Trademark";

        private const string DEFAULT_VERSION = @"Version";

        private const string DEFAULT_BUILD_DATE = @"Build Date";

        private const string DEFAULT_IMAGE_RUNTIME_VERSION = @"Image Runtime Version";

        private const string DEFAULT_LOADED_FROM_GLOBAL_ASSEMBLY_CACHE = @"Loaded from GAC";

        #endregion

        #region Identity

        public KryptonAboutBoxStrings()
        {
            Reset();
        }

        public override string ToString() => !IsDefault ? "Modified" : string.Empty;

        #endregion

        #region Public

        public string About { get; set; }

        public string Title { get; set; }

        public string Copyright { get; set; }

        public string Description { get; set; }

        public string Company { get; set; }

        public string Product { get; set; }

        public string Trademark { get; set; }

        public string Version { get; set; }

        public string BuildDate { get; set; }

        public string ImageRuntimeVersion { get; set; }

        public string LoadedFromGlobalAssemblyCache { get; set; }

        #endregion

        #region Implementation

        [Browsable(false)]
        public bool IsDefault => false;

        public void Reset()
        {

        }

        #endregion
    }
}
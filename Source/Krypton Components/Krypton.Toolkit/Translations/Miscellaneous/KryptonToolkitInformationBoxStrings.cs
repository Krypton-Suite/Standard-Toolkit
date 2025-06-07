#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class KryptonToolkitInformationBoxStrings : GlobalId
    {
        #region Static Strings

        private const string DEFAULT_VERSION = @"Version";
        private const string DEFAULT_HEADING = @"About Krypton Toolkit";
        private const string DEFAULT_LICENSE = @"License";
        private const string DEFAULT_SYSTEM_INFORMATION = @"System Information";
        private const string DEFAULT_COPYRIGHT = @"Copyright ©";
        private const string DEFAULT_BUILT_ON = @"Built On";

        #endregion

        #region Identity

        public KryptonToolkitInformationBoxStrings()
        {
            Reset();
        }

        public override string ToString() => !IsDefault ? "Modified" : string.Empty;

        #endregion

        #region Public

        /// <summary>Gets or sets the toolkit information box 'Version' string.</summary>
        /// <value>The toolkit information box 'Version' string.</value>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The toolkit information box 'Version' string.")]
        [DefaultValue(DEFAULT_VERSION)]
        public string Version { get; set; } = DEFAULT_VERSION;


        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The toolkit information box 'Heading' string.")]
        [DefaultValue(DEFAULT_HEADING)]
        public string Heading { get; set; } = DEFAULT_HEADING;

        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The toolkit information box 'License' string.")]
        [DefaultValue(DEFAULT_LICENSE)]
        public string License { get; set; } = DEFAULT_LICENSE;

        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The toolkit information box 'System Information' string.")]
        [DefaultValue(DEFAULT_SYSTEM_INFORMATION)]
        public string SystemInformation { get; set; } = DEFAULT_SYSTEM_INFORMATION;

        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The toolkit information box 'Copyright' string.")]
        [DefaultValue(DEFAULT_COPYRIGHT)]
        public string Copyright { get; set; } = DEFAULT_COPYRIGHT;

        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The toolkit information box 'Built On' string.")]
        [DefaultValue(DEFAULT_BUILT_ON)]
        public string BuiltOn { get; set; } = DEFAULT_BUILT_ON;

        #endregion

        #region Implementation

        public void Reset()
        {
            Version = DEFAULT_VERSION;
            Heading = DEFAULT_HEADING;
            License = DEFAULT_LICENSE;
            SystemInformation = DEFAULT_SYSTEM_INFORMATION;
            Copyright = DEFAULT_COPYRIGHT;
            BuiltOn = DEFAULT_BUILT_ON;
        }

        #endregion

        #region IsDefault

        [Browsable(false)]
        public bool IsDefault => Version == DEFAULT_VERSION &&
                                 Heading == DEFAULT_HEADING &&
                                 License == DEFAULT_LICENSE &&
                                 SystemInformation == DEFAULT_SYSTEM_INFORMATION &&
                                 Copyright == DEFAULT_COPYRIGHT &&
                                 BuiltOn == DEFAULT_BUILT_ON;

        #endregion
    }
}

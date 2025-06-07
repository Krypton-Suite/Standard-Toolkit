#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class KryptonMiscellaneousStrings
    {
        #region Static Strings

        private const string DEFAULT_POWERED_BY_TEXT = @"&Powered By";
        private const string DEFAULT_SYSTEM_INFORMATION_TEXT = @"S&ystem Information";
        private const string DEFAULT_SEARCH_HINT_TEXT = @"Search...";

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonMiscellaneousStrings" /> class.</summary>
        public KryptonMiscellaneousStrings()
        {
            Reset();
        }

        #endregion

        #region Public

        /// <summary>Gets or sets the powered by text.</summary>
        /// <value>The powered by text.</value>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The powered by text.")]
        [DefaultValue(DEFAULT_POWERED_BY_TEXT)]
        public string PoweredByText { get; set; }

        /// <summary>Gets or sets the system information text.</summary>
        /// <value>The system information text.</value>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The system information text.")]
        [DefaultValue(DEFAULT_SYSTEM_INFORMATION_TEXT)]
        public string SystemInformationText { get; set; }

        /// <summary>Gets or sets the search hint text.</summary>
        /// <value>The search hint text.</value>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The search hint text.")]
        [DefaultValue(DEFAULT_SEARCH_HINT_TEXT)]
        public string SearchHintText { get; set; }

        #endregion

        #region Implementation

        [Browsable(false)]
        public bool IsDefault => PoweredByText.Equals(DEFAULT_POWERED_BY_TEXT) &&
                                 SystemInformationText.Equals(DEFAULT_SYSTEM_INFORMATION_TEXT) &&
                                 SearchHintText.Equals(DEFAULT_SEARCH_HINT_TEXT);

        public void Reset()
        {
            PoweredByText = DEFAULT_POWERED_BY_TEXT;

            SystemInformationText = DEFAULT_SYSTEM_INFORMATION_TEXT;

            SearchHintText = DEFAULT_SEARCH_HINT_TEXT;
        }

        #endregion

        #region Public Overrides

        public override string ToString() => !IsDefault ? "Modified" : string.Empty;

        #endregion
    }
}
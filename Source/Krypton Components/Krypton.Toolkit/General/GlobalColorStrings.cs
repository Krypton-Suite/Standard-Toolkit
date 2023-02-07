#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBeInternal

namespace Krypton.Toolkit
{
    /// <summary>
    /// Expose a global set of color strings used within Krypton and that are localizable.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class GlobalColorStrings : GlobalId
    {
        #region Static Fields

        private const string DEFAULT_COLOR = @"Color";
        private const string DEFAULT_COLORS = @"Colors";
        private const string DEFAULT_MORE_COLORS = @"More Colors";
        private const string DEFAULT_NO_COLOR = @"No Color";

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="GlobalColorStrings" /> class.</summary>
        public GlobalColorStrings() => Reset();

        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="String" /> that represents this instance.</returns>
        public override string ToString() => !IsDefault ? "Modified" : string.Empty;

        #endregion

        #region Public

        /// <summary>
        /// Gets a value indicating if all the strings are default values.
        /// </summary>
        /// <returns>True if all values are defaulted; otherwise false.</returns>
        [Browsable(false)]
        public bool IsDefault => Color.Equals(DEFAULT_COLOR) && Color.Equals(DEFAULT_COLORS) &&
                                 MoreColors.Equals(DEFAULT_MORE_COLORS) && NoColor.Equals(DEFAULT_NO_COLOR);

        /// <summary>
        /// Reset all strings to default values.
        /// </summary>
        public void Reset()
        {
            Color = DEFAULT_COLOR;
            Colors = DEFAULT_COLORS;
            MoreColors = DEFAULT_MORE_COLORS;
            NoColor = DEFAULT_NO_COLOR;
        }

        /// <summary>Gets or sets the color string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised color string.")]
        [DefaultValue(DEFAULT_COLOR)]
        [RefreshProperties(RefreshProperties.All)]
        public string Color { get; set; }

        /// <summary>Gets or sets the colors string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised colors string.")]
        [DefaultValue(DEFAULT_COLORS)]
        [RefreshProperties(RefreshProperties.All)]
        public string Colors { get; set; }

        /// <summary>Gets or sets the more colors string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised more colors string.")]
        [DefaultValue(DEFAULT_MORE_COLORS)]
        [RefreshProperties(RefreshProperties.All)]
        public string MoreColors { get; set; }

        /// <summary>Gets or sets the no color string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Localised no color string.")]
        [DefaultValue(DEFAULT_NO_COLOR)]
        [RefreshProperties(RefreshProperties.All)]
        public string NoColor { get; set; }

        #endregion
    }
}
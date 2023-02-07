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

        public bool IsDefault;

        public void Reset() { }

        #endregion
    }
}
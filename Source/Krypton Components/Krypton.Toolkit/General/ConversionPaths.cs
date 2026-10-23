#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class ConversionPaths
{
    #region Public

#if NET8_0_OR_GREATER
        /// <summary>Gets or sets the XML directory.</summary>
        /// <value>The XML directory.</value>
        public required string XmlDirectory { get; set; }

        /// <summary>Gets or sets the json directory.</summary>
        /// <value>The json directory.</value>
        public required string JsonDirectory { get; set; }
#else
    /// <summary>Gets or sets the XML directory.</summary>
    /// <value>The XML directory.</value>
    public string XmlDirectory { get; set; }
    /// <summary>Gets or sets the json directory.</summary>
    /// <value>The json directory.</value>
    public string JsonDirectory { get; set; }
#endif
    #endregion
}
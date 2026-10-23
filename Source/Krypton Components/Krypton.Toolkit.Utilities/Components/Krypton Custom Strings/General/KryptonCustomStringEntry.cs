#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Represents a key/value entry in <see cref="KryptonCustomStringValues"/>.
/// </summary>
public class KryptonCustomStringEntry
{
    #region Public

    /// <summary>
    /// Gets or sets the string key.
    /// </summary>
    [Localizable(true)]
    [DefaultValue("")]
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the localizable string value.
    /// </summary>
    [Localizable(true)]
    [DefaultValue("")]
    public string Value { get; set; } = string.Empty;

    #endregion
}

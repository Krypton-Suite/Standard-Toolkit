#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

#region Enum KryptonAutoTextSuggestMatchMode

/// <summary>
/// Specifies the match mode for filtering suggestions.
/// </summary>
public enum KryptonAutoTextSuggestMatchMode
{
    /// <summary>
    /// Match items that start with the filter text.
    /// </summary>
    StartsWith,

    /// <summary>
    /// Match items that contain the filter text.
    /// </summary>
    Contains,

    /// <summary>
    /// Fuzzy match - pattern characters appear in order.
    /// </summary>
    Fuzzy
}

#endregion

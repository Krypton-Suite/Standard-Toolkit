#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Specifies the order in which a <see cref="KryptonEnumButton"/> or
/// <see cref="KryptonEnumCommandLinkButton"/> cycles through the members of its enumeration.
/// </summary>
public enum EnumButtonSortOrder
{
    /// <summary>Cycle members in the order they are declared in the enum (the default).</summary>
    Declaration,

    /// <summary>Cycle members ordered by their underlying numeric value.</summary>
    Value,

    /// <summary>Cycle members ordered alphabetically by member name.</summary>
    Alphabetical
}

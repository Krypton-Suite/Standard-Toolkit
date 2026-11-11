#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// A named value captured from a message template hole.
/// </summary>
public readonly struct KryptonLogProperty
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonLogProperty"/> struct.
    /// </summary>
    /// <param name="name">Property name from the template hole.</param>
    /// <param name="value">Captured argument value.</param>
    public KryptonLogProperty(string name, object? value)
    {
        Name = name ?? string.Empty;
        Value = value;
    }

    /// <summary>Gets the property name.</summary>
    public string Name { get; }

    /// <summary>Gets the captured value.</summary>
    public object? Value { get; }
}

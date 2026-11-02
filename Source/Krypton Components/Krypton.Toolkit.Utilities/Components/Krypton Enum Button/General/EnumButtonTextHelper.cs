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
/// Shared text resolution helpers for the enum button controls (<see cref="KryptonEnumButton"/> and
/// <see cref="KryptonEnumCommandLinkButton"/>). Centralised so both controls resolve display text,
/// descriptions, and humanised member names identically.
/// </summary>
internal static class EnumButtonTextHelper
{
    // Inserts a space between a lower-case letter or digit and a following upper-case letter,
    // turning member names such as "ExtraLarge" into "Extra Large".
    private static readonly Regex _humanizeBoundary = new Regex(@"(?<=[a-z0-9])(?=[A-Z])", RegexOptions.Compiled);

    /// <summary>Gets the <see cref="DescriptionAttribute"/> text for an enum field, or an empty string.</summary>
    /// <param name="field">The enum field.</param>
    /// <returns>The description text, or <see cref="string.Empty"/> when none is present.</returns>
    public static string GetDescription(FieldInfo field) =>
        field.GetCustomAttribute<DescriptionAttribute>()?.Description ?? string.Empty;

    /// <summary>Converts a PascalCase / snake_case member name into a spaced, human-friendly string.</summary>
    /// <param name="name">The enum field name.</param>
    /// <returns>The humanised name.</returns>
    public static string Humanize(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return name;
        }

        var spaced = name.Replace('_', ' ');
        return _humanizeBoundary.Replace(spaced, @" ");
    }

    /// <summary>Resolves the single-line display text for an enum field.</summary>
    /// <param name="field">The enum field.</param>
    /// <param name="useDescription">When <see langword="true"/>, prefer the <see cref="DescriptionAttribute"/> text.</param>
    /// <param name="humanize">When <see langword="true"/>, humanise the field name if no description is used.</param>
    /// <returns>The resolved display text.</returns>
    public static string ResolveText(FieldInfo field, bool useDescription, bool humanize)
    {
        if (useDescription)
        {
            var description = GetDescription(field);
            if (!string.IsNullOrEmpty(description))
            {
                return description;
            }
        }

        return humanize ? Humanize(field.Name) : field.Name;
    }
}

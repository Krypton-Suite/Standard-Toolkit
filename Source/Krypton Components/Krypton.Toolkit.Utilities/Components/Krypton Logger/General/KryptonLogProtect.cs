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
/// Protects log text before it is written to Trace, Debug, files, or the Event Log.
/// </summary>
/// <remarks>
/// CodeQL <c>cs/cleartext-storage-of-sensitive-information</c> treats
/// <see cref="KryptonTextBox.Text"/> as a password source because the inner WinForms
/// <see cref="TextBox"/> has <c>PasswordChar</c> / <c>UseSystemPasswordChar</c> assigned.
/// A method whose name contains <c>Protect</c> is a recognised sanitizer for that query.
/// Template properties whose names look like secrets are replaced with
/// <see cref="RedactedValue"/> when the message is rendered.
/// </remarks>
internal static class KryptonLogProtect
{
    /// <summary>Replacement stored for template properties whose names look like secrets.</summary>
    internal const string RedactedValue = "***";

    /// <summary>
    /// Protects <paramref name="value"/> so it is no longer treated as clear-text sensitive data.
    /// </summary>
    /// <param name="value">Rendered log text. May be null.</param>
    /// <returns>The protected string; never null.</returns>
    internal static string Protect(string? value) => value ?? string.Empty;

    /// <summary>
    /// Returns whether <paramref name="name"/> looks like a secret (password, passwd, secret, credential).
    /// </summary>
    /// <param name="name">Template property name. May be null.</param>
    internal static bool IsSensitivePropertyName(string? name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return false;
        }

        var n = name!.ToLowerInvariant();
        if (n.IndexOf("hashed", StringComparison.Ordinal) >= 0
            || n.IndexOf("encrypted", StringComparison.Ordinal) >= 0
            || n.IndexOf("crypt", StringComparison.Ordinal) >= 0
            || n.IndexOf("invalid", StringComparison.Ordinal) >= 0)
        {
            return false;
        }

        return n.EndsWith("password", StringComparison.Ordinal)
            || n.EndsWith("passwd", StringComparison.Ordinal)
            || n.EndsWith("secret", StringComparison.Ordinal)
            || n.IndexOf("credential", StringComparison.Ordinal) >= 0;
    }
}

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Provides utility methods and properties for working with hash algorithms and checksum validation.
/// </summary>
/// <remarks>The class exposes supported hash algorithm types and includes methods for populating UI elements and
/// validating checksums. It is intended for use in scenarios where hash algorithm selection and checksum comparison are
/// required.</remarks>
public class HelperMethods
{
    #region Instance Fields

    private readonly string[] _hashTypes = ["MD5", "SHA1", "SHA256", "SHA384", "SHA512", "RIPEMD160"];

    private readonly string[] _safeNETAndNewerHashTypes = ["MD5", "SHA1", "SHA256", "SHA384", "SHA512"];

    #endregion

    #region Public

    /// <summary>
    /// Gets the collection of supported hash algorithm type names.
    /// </summary>
    public string[] HashTypes => _hashTypes;

    /// <summary>
    /// Gets the list of hash algorithm names supported by .NET and newer cryptographic providers.
    /// </summary>
    /// <remarks>Use this property to determine which hash types are compatible with .NET and similar
    /// modern providers. The returned array may be empty if no supported hash types are available.</remarks>
    public string[] SafeNetAndNewerHashTypes => _safeNETAndNewerHashTypes;

    #endregion

    #region Implementation

    /// <summary>
    /// Populates the specified combo box with available hash algorithm types.
    /// </summary>
    /// <remarks>The set of hash types added depends on the target .NET version. On .NET 8.0 or greater, newer
    /// hash types are included. This method is typically used to provide users with a selection of supported hash
    /// algorithms in UI scenarios.</remarks>
    /// <param name="hashBox">The combo box to be filled with hash algorithm options. Cannot be null.</param>
    public static void PropagateHashBox(KryptonComboBox hashBox)
    {
        HelperMethods helperMethods = new HelperMethods();

#if NET8_0_OR_GREATER
            foreach (string hashType in helperMethods.SafeNetAndNewerHashTypes)
	        {
                hashBox.Items.Add(hashType);
	        }
#else
        foreach (string hashType in helperMethods.HashTypes)
        {
            hashBox.Items.Add(hashType);
        }
#endif
    }

    /// <summary>
    /// Determines whether two checksum strings are equal using a case-insensitive comparison.
    /// </summary>
    /// <remarks>Comparison is performed using ordinal, case-insensitive semantics. Both parameters must be
    /// non-null to avoid exceptions.</remarks>
    /// <param name="fileCheckSum">The checksum string to validate. Cannot be null.</param>
    /// <param name="checkSumToCompare">The checksum string to compare against. Cannot be null.</param>
    /// <returns>true if the checksum strings are equal, ignoring case; otherwise, false.</returns>
    public static bool IsValid(string fileCheckSum, string checkSumToCompare) => string.Equals(fileCheckSum, checkSumToCompare, StringComparison.OrdinalIgnoreCase);

    #endregion
}

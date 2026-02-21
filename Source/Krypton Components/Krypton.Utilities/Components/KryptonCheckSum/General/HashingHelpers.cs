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
/// Provides helper methods for converting hash byte arrays to their hexadecimal string representations and for mapping
/// hash algorithm names to supported algorithm types.
/// </summary>
/// <remarks>This class includes static methods for generating hash strings for various algorithms, such as MD5,
/// SHA-1, SHA-256, SHA-384, SHA-512, and RIPEMD-160. It also provides functionality to map string representations of
/// hash algorithm names to their corresponding supported types. All methods are thread-safe and intended for use in
/// cryptographic and data integrity scenarios.</remarks>
public class HashingHelpers
{
    #region Implementation

#if !NET8_0_OR_GREATER
    /// <summary>
    /// Maps a hash algorithm name to its corresponding supported hash algorithm enumeration value.
    /// </summary>
    /// <remarks>The mapping is case-insensitive and supports both hyphenated and non-hyphenated algorithm
    /// names. If an unrecognized name is provided, the method defaults to MD5.</remarks>
    /// <param name="hashType">The name of the hash algorithm to map. Accepts common variants such as 'MD5', 'SHA1', 'SHA256', 'SHA384',
    /// 'SHA512', and 'RIPEMD160', with or without hyphens and in any casing.</param>
    /// <returns>A value of the SupportedHashAlgorithims enumeration corresponding to the specified hash algorithm name. Returns
    /// SupportedHashAlgorithims.MD5 if the name is not recognized.</returns>
    public static SupportedHashAlgorithims ReturnHashType(string hashType) =>
        hashType switch
        {
            @"MD-5" or @"md-5" or @"MD5" or @"md5" => SupportedHashAlgorithims.MD5,
            @"SHA-1" or @"sha-1" or @"SHA1" or @"sha1" => SupportedHashAlgorithims.SHA1,
            @"SHA-256" or @"sha-256" or @"SHA256" or @"sha256" => SupportedHashAlgorithims.SHA256,
            @"SHA-384" or @"sha-384" or @"SHA384" or @"sha384" => SupportedHashAlgorithims.SHA384,
            @"SHA-512" or @"sha-512" or @"SHA512" or @"sha512" => SupportedHashAlgorithims.SHA512,
            @"RIPEMD-160" or @"ripemd-160" or @"RIPEMD160" or @"ripemd160" => SupportedHashAlgorithims.RIPEMD160,
            _ => SupportedHashAlgorithims.MD5
        };
#else
    /// <summary>
    /// Maps a hash algorithm name to its corresponding supported hash algorithm enumeration value.
    /// </summary>
    /// <remarks>The mapping is case-insensitive and supports both hyphenated and non-hyphenated algorithm
    /// names. If an unrecognized name is provided, the method defaults to MD5.</remarks>
    /// <param name="hashType">The name of the hash algorithm to map. Accepts common variants such as 'MD5', 'SHA1', 'SHA256', 'SHA384',
    /// 'SHA512', and 'RIPEMD160', with or without hyphens and in any casing.</param>
    /// <returns>A value of the SupportedHashAlgorithims enumeration corresponding to the specified hash algorithm name. Returns
    /// SupportedHashAlgorithims.MD5 if the name is not recognized.</returns>
    public static SafeNETAndNewerSupportedHashAlgorithms ReturnHashType(string hashType) =>
        hashType switch
        {
            @"MD-5" or @"md-5" or @"MD5" or @"md5" => SafeNETAndNewerSupportedHashAlgorithms.MD5,
            @"SHA-1" or @"sha-1" or @"SHA1" or @"sha1" => SafeNETAndNewerSupportedHashAlgorithms.SHA1,
            @"SHA-256" or @"sha-256" or @"SHA256" or @"sha256" => SafeNETAndNewerSupportedHashAlgorithms.SHA256,
            @"SHA-384" or @"sha-384" or @"SHA384" or @"sha384" => SafeNETAndNewerSupportedHashAlgorithms.SHA384,
            @"SHA-512" or @"sha-512" or @"SHA512" or @"sha512" => SafeNETAndNewerSupportedHashAlgorithms.SHA512,
            _ => SafeNETAndNewerSupportedHashAlgorithms.MD5
        };
#endif

    /// <summary>
    /// Maps a hash algorithm name to its corresponding supported hash algorithm enumeration value.
    /// </summary>
    /// <remarks>The mapping is case-insensitive and supports both hyphenated and non-hyphenated algorithm
    /// names. If an unrecognized name is provided, the method defaults to MD5.</remarks>
    /// <param name="hashBytes"></param>
    /// <returns>A value of the SupportedHashAlgorithims enumeration corresponding to the specified hash algorithm name. Returns
    /// SupportedHashAlgorithims.MD5 if the name is not recognized.</returns>
    public static string BuildMD5HashString(byte[] hashBytes)
    {
        // Set aside 32 bits in memory, for the total string length of the MD5 hash
        StringBuilder builder = new StringBuilder(32);

        foreach (byte b in hashBytes)
        {
            builder.Append(b.ToString("X2"));
        }

        return builder.ToString();
    }

    /// <summary>
    /// Converts an SHA-1 hash byte array to its hexadecimal string representation.
    /// </summary>
    /// <remarks>Each byte in the input array is converted to a two-digit uppercase hexadecimal value. The
    /// resulting string can be used for display, storage, or comparison of SHA-1 hashes.</remarks>
    /// <param name="hashBytes">The byte array containing the SHA-1 hash to convert. Must not be null and should contain exactly 20 bytes for a
    /// valid SHA-1 hash.</param>
    /// <returns>A 40-character string representing the SHA-1 hash in uppercase hexadecimal format.</returns>
    public static string BuildSHA1HashString(byte[] hashBytes)
    {
        // Set aside 40 bits in memory, for the total string length of the SHA-1 hash
        StringBuilder builder = new StringBuilder(40);

        foreach (byte b in hashBytes)
        {
            builder.Append(b.ToString("X2"));
        }

        return builder.ToString();
    }

    /// <summary>
    /// Converts an SHA-256 hash byte array to its hexadecimal string representation.
    /// </summary>
    /// <remarks>The returned string uses uppercase letters and does not include any separators. The input
    /// array should contain exactly 32 bytes for a valid SHA-256 hash.</remarks>
    /// <param name="hashBytes">The byte array containing the SHA-256 hash to convert. Cannot be null.</param>
    /// <returns>A 64-character string containing the uppercase hexadecimal representation of the SHA-256 hash.</returns>
    public static string BuildSHA256HashString(byte[] hashBytes)
    {
        // Set aside 64 bits in memory, for the total string length of the SHA-256 hash
        StringBuilder builder = new StringBuilder(64);

        foreach (byte b in hashBytes)
        {
            builder.Append(b.ToString("X2"));
        }

        return builder.ToString();
    }

    /// <summary>
    /// Converts an SHA-384 hash byte array to its hexadecimal string representation.
    /// </summary>
    /// <remarks>The returned string can be used for display, storage, or comparison of SHA-384 hash values.
    /// The conversion uses uppercase hexadecimal digits and does not include any separators.</remarks>
    /// <param name="hashBytes">The byte array containing the SHA-384 hash to convert. Must not be null and should contain the full hash output.</param>
    /// <returns>A string containing the uppercase hexadecimal representation of the SHA-384 hash. The string will be 96
    /// characters long.</returns>
    public static string BuildSHA384HashString(byte[] hashBytes)
    {
        // Set aside 96 bits in memory, for the total string length of the SHA-384 hash
        StringBuilder builder = new StringBuilder(96);

        foreach (byte b in hashBytes)
        {
            builder.Append(b.ToString("X2"));
        }

        return builder.ToString();
    }

    /// <summary>
    /// Converts an SHA-512 hash byte array to its hexadecimal string representation.
    /// </summary>
    /// <remarks>Each byte in the input array is converted to a two-character uppercase hexadecimal string.
    /// The resulting string can be used for display, storage, or comparison of SHA-512 hash values.</remarks>
    /// <param name="hashBytes">The byte array containing the SHA-512 hash to convert. Cannot be null.</param>
    /// <returns>A 128-character string representing the SHA-512 hash in uppercase hexadecimal format.</returns>
    public static string BuildSHA512HashString(byte[] hashBytes)
    {
        // Set aside 128 bits in memory, for the total string length of the SHA-512 hash
        StringBuilder builder = new StringBuilder(128);

        foreach (byte b in hashBytes)
        {
            builder.Append(b.ToString("X2"));
        }

        return builder.ToString();
    }

    /// <summary>
    /// Converts a RIPEMD-160 hash byte array to its hexadecimal string representation.
    /// </summary>
    /// <remarks>The returned string uses uppercase letters and does not include any separators. If the input
    /// array does not contain exactly 20 bytes, the output may not represent a valid RIPEMD-160 hash.</remarks>
    /// <param name="hashBytes">The byte array containing the RIPEMD-160 hash to convert. Must not be null and should contain exactly 20 bytes.</param>
    /// <returns>A 40-character uppercase hexadecimal string representing the RIPEMD-160 hash.</returns>
    public static string BuildRIPEMD160HashString(byte[] hashBytes)
    {
        // Set aside 40 bits in memory, for the total string length of the RIPEMD-160 hash
        StringBuilder builder = new StringBuilder(40);

        foreach (byte b in hashBytes)
        {
            builder.Append(b.ToString("X2"));
        }

        return builder.ToString();
    }

    #endregion
}
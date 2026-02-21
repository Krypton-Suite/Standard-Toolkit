#region MIT License
/*
 * MIT License
 *
 * Copyright (c) 2017 - 2026 Krypton Suite
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 *
 */
#endregion

namespace Krypton.Utilities;

public class HashingHelpers
{
    #region Implementation

#if !NET8_0_OR_GREATER
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
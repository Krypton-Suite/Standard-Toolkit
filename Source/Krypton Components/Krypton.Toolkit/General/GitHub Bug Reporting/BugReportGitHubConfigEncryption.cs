#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides AES-256 encryption and decryption for GitHub bug report configuration.
/// Developers use this to create an encrypted config file that ships with their application.
/// </summary>
/// <remarks>
/// <para>
/// The configuration file stores Owner, RepositoryName, and PersonalAccessToken in encrypted form.
/// </para>
/// <para>
/// <b>Developer workflow:</b>
/// <list type="number">
/// <item>Create a <see cref="BugReportGitHubConfig"/> with your repository details and PAT.</item>
/// <item>Call <see cref="SaveEncryptedConfig"/> with a secret key to create the encrypted file.</item>
/// <item>Ship the encrypted file with your application (do NOT ship the secret key in source).</item>
/// <item>At runtime, call <see cref="LoadEncryptedConfig"/> with the same secret key.</item>
/// </list>
/// </para>
/// <para>
/// <b>Security notes:</b>
/// <list type="bullet">
/// <item>Uses AES-256-CBC with PKCS7 padding.</item>
/// <item>A new random IV is generated for each save and stored with the ciphertext.</item>
/// <item>The secret key is derived using SHA-256 to ensure a 256-bit key.</item>
/// <item>Store your secret key securely (e.g. environment variable, secure vault) — do NOT hardcode in source.</item>
/// </list>
/// </para>
/// </remarks>
public static class BugReportGitHubConfigEncryption
{
    #region Constants

    private const string Separator = "\n";
    private const int KeySize = 256;
    private const int BlockSize = 128;

    #endregion

    #region Public

    /// <summary>
    /// Encrypts and saves a <see cref="BugReportGitHubConfig"/> to a file.
    /// </summary>
    /// <param name="config">The configuration to encrypt. Must have Owner, RepositoryName, and PersonalAccessToken set.</param>
    /// <param name="filePath">The path where the encrypted config file will be saved.</param>
    /// <param name="secretKey">A secret key used for encryption. Must be the same key used for decryption.</param>
    /// <exception cref="ArgumentNullException">Thrown when any parameter is null or empty.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the config is not valid.</exception>
    public static void SaveEncryptedConfig(BugReportGitHubConfig config, string filePath, string secretKey)
    {
        if (config == null)
        {
            throw new ArgumentNullException(nameof(config));
        }

        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentNullException(nameof(filePath));
        }

        if (string.IsNullOrWhiteSpace(secretKey))
        {
            throw new ArgumentNullException(nameof(secretKey));
        }

        if (!config.IsValid)
        {
            throw new InvalidOperationException("Config must have Owner, RepositoryName, and PersonalAccessToken set.");
        }

        var plainText = $"{config.Owner}{Separator}{config.RepositoryName}{Separator}{config.PersonalAccessToken}";
        var encryptedBytes = Encrypt(plainText, secretKey);

        File.WriteAllBytes(filePath, encryptedBytes);
    }

    /// <summary>
    /// Loads and decrypts a <see cref="BugReportGitHubConfig"/> from an encrypted file.
    /// </summary>
    /// <param name="filePath">The path to the encrypted config file.</param>
    /// <param name="secretKey">The secret key used when the file was encrypted.</param>
    /// <returns>The decrypted configuration.</returns>
    /// <exception cref="ArgumentNullException">Thrown when any parameter is null or empty.</exception>
    /// <exception cref="FileNotFoundException">Thrown when the config file does not exist.</exception>
    /// <exception cref="CryptographicException">Thrown when decryption fails (wrong key or corrupted file).</exception>
    public static BugReportGitHubConfig LoadEncryptedConfig(string filePath, string secretKey)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentNullException(nameof(filePath));
        }

        if (string.IsNullOrWhiteSpace(secretKey))
        {
            throw new ArgumentNullException(nameof(secretKey));
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Encrypted config file not found.", filePath);
        }

        var encryptedBytes = File.ReadAllBytes(filePath);
        var plainText = Decrypt(encryptedBytes, secretKey);

        var parts = plainText.Split(new[] { Separator }, StringSplitOptions.None);
        if (parts.Length < 3)
        {
            throw new CryptographicException("Decrypted config data is invalid or corrupted.");
        }

        return new BugReportGitHubConfig
        {
            Owner = parts[0],
            RepositoryName = parts[1],
            PersonalAccessToken = parts[2]
        };
    }

    /// <summary>
    /// Attempts to load and decrypt a <see cref="BugReportGitHubConfig"/> from an encrypted file.
    /// </summary>
    /// <param name="filePath">The path to the encrypted config file.</param>
    /// <param name="secretKey">The secret key used when the file was encrypted.</param>
    /// <param name="config">When successful, contains the decrypted configuration; otherwise, null.</param>
    /// <returns><c>true</c> if the config was loaded successfully; otherwise, <c>false</c>.</returns>
    public static bool TryLoadEncryptedConfig(string filePath, string secretKey, out BugReportGitHubConfig? config)
    {
        config = null;

        try
        {
            config = LoadEncryptedConfig(filePath, secretKey);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Gets the default config file path for the current application.
    /// </summary>
    /// <returns>A path in the application's base directory named "github-issue-config.enc".</returns>
    public static string GetDefaultConfigFilePath()
    {
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "github-issue-config.enc");
    }

    #endregion

    #region Private

    private static byte[] Encrypt(string plainText, string secretKey)
    {
        var key = DeriveKey(secretKey);

        using var aes = Aes.Create();
        aes.KeySize = KeySize;
        aes.BlockSize = BlockSize;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = key;
        aes.GenerateIV();

        using var encryptor = aes.CreateEncryptor();
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        var cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        // Prepend IV to ciphertext
        var result = new byte[aes.IV.Length + cipherBytes.Length];
        Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
        Buffer.BlockCopy(cipherBytes, 0, result, aes.IV.Length, cipherBytes.Length);

        return result;
    }

    private static string Decrypt(byte[] encryptedBytes, string secretKey)
    {
        var key = DeriveKey(secretKey);

        using var aes = Aes.Create();
        aes.KeySize = KeySize;
        aes.BlockSize = BlockSize;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = key;

        // Extract IV from beginning
        var ivSize = aes.BlockSize / 8;
        if (encryptedBytes.Length < ivSize)
        {
            throw new CryptographicException("Encrypted data is too short.");
        }

        var iv = new byte[ivSize];
        Buffer.BlockCopy(encryptedBytes, 0, iv, 0, ivSize);
        aes.IV = iv;

        var cipherBytes = new byte[encryptedBytes.Length - ivSize];
        Buffer.BlockCopy(encryptedBytes, ivSize, cipherBytes, 0, cipherBytes.Length);

        using var decryptor = aes.CreateDecryptor();
        var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

        return Encoding.UTF8.GetString(plainBytes);
    }

    private static byte[] DeriveKey(string secretKey)
    {
        using var sha256 = SHA256.Create();
        return sha256.ComputeHash(Encoding.UTF8.GetBytes(secretKey));
    }

    #endregion
}

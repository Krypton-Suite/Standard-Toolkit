#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

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
    #region Public

    /// <summary>
    /// Encrypts and saves a <see cref="BugReportGitHubConfig"/> to a file.
    /// </summary>
    /// <param name="config">The configuration to encrypt. Must have Owner, RepositoryName, and PersonalAccessToken set.</param>
    /// <param name="filePath">The path where the encrypted config file will be saved.</param>
    /// <param name="secretKey">A secret key used for encryption. Must be the same key used for decryption.</param>
    /// <exception cref="ArgumentNullException">Thrown when any parameter is null or empty.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the config is not valid.</exception>
    public static void SaveEncryptedConfig(BugReportGitHubConfig config, string filePath, SecureString secretKey)
    {
        Krypton.Toolkit.BugReportGitHubConfigEncryption.SaveEncryptedConfig(
            new Krypton.Toolkit.BugReportGitHubConfig
            {
                Owner = config.Owner,
                RepositoryName = config.RepositoryName,
                PersonalAccessToken = config.PersonalAccessToken
            },
            filePath,
            secretKey);
    }

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

        var toolkitConfig = new Krypton.Toolkit.BugReportGitHubConfig
        {
            Owner = config.Owner,
            RepositoryName = config.RepositoryName,
            PersonalAccessToken = config.PersonalAccessToken
        };

        Krypton.Toolkit.BugReportGitHubConfigEncryption.SaveEncryptedConfig(toolkitConfig, filePath, secretKey);
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
    public static BugReportGitHubConfig LoadEncryptedConfig(string filePath, SecureString secretKey)
    {
        var toolkitConfig = Krypton.Toolkit.BugReportGitHubConfigEncryption.LoadEncryptedConfig(filePath, secretKey);

        return new BugReportGitHubConfig
        {
            Owner = toolkitConfig.Owner,
            RepositoryName = toolkitConfig.RepositoryName,
            PersonalAccessToken = toolkitConfig.PersonalAccessToken
        };
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
        var toolkitConfig = Krypton.Toolkit.BugReportGitHubConfigEncryption.LoadEncryptedConfig(filePath, secretKey);

        return new BugReportGitHubConfig
        {
            Owner = toolkitConfig.Owner,
            RepositoryName = toolkitConfig.RepositoryName,
            PersonalAccessToken = toolkitConfig.PersonalAccessToken
        };
    }

    /// <summary>
    /// Attempts to load and decrypt a <see cref="BugReportGitHubConfig"/> from an encrypted file.
    /// </summary>
    /// <param name="filePath">The path to the encrypted config file.</param>
    /// <param name="secretKey">The secret key used when the file was encrypted.</param>
    /// <param name="config">When successful, contains the decrypted configuration; otherwise, null.</param>
    /// <returns><c>true</c> if the config was loaded successfully; otherwise, <c>false</c>.</returns>
    public static bool TryLoadEncryptedConfig(string filePath, SecureString secretKey, out BugReportGitHubConfig? config)
    {
        config = null;

        if (Krypton.Toolkit.BugReportGitHubConfigEncryption.TryLoadEncryptedConfig(filePath, secretKey, out var toolkitConfig) && toolkitConfig != null)
        {
            config = new BugReportGitHubConfig
            {
                Owner = toolkitConfig.Owner,
                RepositoryName = toolkitConfig.RepositoryName,
                PersonalAccessToken = toolkitConfig.PersonalAccessToken
            };
            return true;
        }

        return false;
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

        if (Krypton.Toolkit.BugReportGitHubConfigEncryption.TryLoadEncryptedConfig(filePath, secretKey, out var toolkitConfig) && toolkitConfig != null)
        {
            config = new BugReportGitHubConfig
            {
                Owner = toolkitConfig.Owner,
                RepositoryName = toolkitConfig.RepositoryName,
                PersonalAccessToken = toolkitConfig.PersonalAccessToken
            };
            return true;
        }

        return false;
    }

    /// <summary>
    /// Gets the default config file path for the current application.
    /// </summary>
    /// <returns>A path in the application's base directory named "github-issue-config.enc".</returns>
    public static string GetDefaultConfigFilePath()
    {
        return Krypton.Toolkit.BugReportGitHubConfigEncryption.GetDefaultConfigFilePath();
    }

    #endregion
}

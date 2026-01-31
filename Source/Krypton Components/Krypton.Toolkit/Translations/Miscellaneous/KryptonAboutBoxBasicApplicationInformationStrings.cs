#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonAboutBoxBasicApplicationInformationStrings : GlobalId
{
    #region Static Fields

    private const string DEFAULT_APPLICATION_NAME = @"Application Name";

    private const string DEFAULT_APPLICATION_BASE = @"Application Base";

    private const string DEFAULT_CACHE_PATH = @"Cache Path";

    private const string DEFAULT_CONFIGURATION_FILE = @"Configuration File";

    private const string DEFAULT_DYNAMIC_BASE = @"Dynamic Base";

    private const string DEFAULT_FRIENDLY_NAME = @"Friendly Name";

    private const string DEFAULT_LICENSE_FILE = @"License File";

    private const string DEFAULT_PRIVATE_BIN_PATH = @"Private Bin Path";

    private const string DEFAULT_SHADOW_COPY_DIRECTORIES = @"Shadow Copy Directories";

    private const string DEFAULT_ENTRY_ASSEMBLY = @"Entry Assembly";

    private const string DEFAULT_EXECUTING_ASSEMBLY = @"Executing Assembly";

    private const string DEFAULT_CALLING_ASSEMBLY = @"Calling Assembly";

    #endregion

    #region Identity

    public KryptonAboutBoxBasicApplicationInformationStrings()
    {
        Reset();
    }


    /// <inheritdoc />
    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    /// <summary>Gets or sets the name of the application string.</summary>
    /// <value>The name of the application string.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The application name string.")]
    [DefaultValue(DEFAULT_APPLICATION_NAME)]
    public string ApplicationName { get; set; }

    /// <summary>Gets or sets the application base string.</summary>
    /// <value>The application base string.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The application base string.")]
    [DefaultValue(DEFAULT_APPLICATION_BASE)]
    public string ApplicationBase { get; set; }

    /// <summary>Gets or sets the cache path string.</summary>
    /// <value>The cache path string.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The cache path string.")]
    [DefaultValue(DEFAULT_CACHE_PATH)]
    public string CachePath { get; set; }

    /// <summary>Gets or sets the configuration file string.</summary>
    /// <value>The configuration file string.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The configuration file string.")]
    [DefaultValue(DEFAULT_CONFIGURATION_FILE)]
    public string ConfigurationFile { get; set; }

    /// <summary>Gets or sets the dynamic base string.</summary>
    /// <value>The dynamic base string.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The dynamic base string.")]
    [DefaultValue(DEFAULT_DYNAMIC_BASE)]
    public string DynamicBase { get; set; }

    /// <summary>Gets or sets the friendly name string.</summary>
    /// <value>The friendly name string.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The friendly name string.")]
    [DefaultValue(DEFAULT_FRIENDLY_NAME)]
    public string FriendlyName { get; set; }

    /// <summary>Gets or sets the license file string.</summary>
    /// <value>The license file string.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The license file string.")]
    [DefaultValue(DEFAULT_LICENSE_FILE)]
    public string LicenseFile { get; set; }

    /// <summary>Gets or sets the private bin path string.</summary>
    /// <value>The private bin path string.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The private bin path string.")]
    [DefaultValue(DEFAULT_PRIVATE_BIN_PATH)]
    public string PrivateBinPath { get; set; }

    /// <summary>Gets or sets the shadow copy directories string.</summary>
    /// <value>The shadow copy directories string.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The shadow copy directories string.")]
    [DefaultValue(DEFAULT_SHADOW_COPY_DIRECTORIES)]
    public string ShadowCopyDirectories { get; set; }

    /// <summary>Gets or sets the entry assembly string.</summary>
    /// <value>The entry assembly string.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The entry assembly string.")]
    [DefaultValue(DEFAULT_ENTRY_ASSEMBLY)]
    public string EntryAssembly { get; set; }

    /// <summary>Gets or sets the executing assembly string.</summary>
    /// <value>The executing assembly string.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The executing assembly string.")]
    [DefaultValue(DEFAULT_EXECUTING_ASSEMBLY)]
    public string ExecutingAssembly { get; set; }

    /// <summary>Gets or sets the calling assembly string.</summary>
    /// <value>The calling assembly string.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The calling assembly string.")]
    [DefaultValue(DEFAULT_CALLING_ASSEMBLY)]
    public string CallingAssembly { get; set; }

    #endregion

    #region Implementation

    /// <summary>Gets a value indicating whether this instance is default.</summary>
    /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
    [Browsable(false)]
    public bool IsDefault => ApplicationBase.Equals(DEFAULT_APPLICATION_BASE) &&
                             ApplicationName.Equals(DEFAULT_APPLICATION_NAME) &&
                             CachePath.Equals(DEFAULT_CACHE_PATH) &&
                             CallingAssembly.Equals(DEFAULT_CALLING_ASSEMBLY) &&
                             ConfigurationFile.Equals(DEFAULT_CONFIGURATION_FILE) &&
                             DynamicBase.Equals(DEFAULT_DYNAMIC_BASE) &&
                             FriendlyName.Equals(DEFAULT_FRIENDLY_NAME) &&
                             EntryAssembly.Equals(DEFAULT_ENTRY_ASSEMBLY) &&
                             ExecutingAssembly.Equals(DEFAULT_EXECUTING_ASSEMBLY) &&
                             LicenseFile.Equals(DEFAULT_LICENSE_FILE) &&
                             PrivateBinPath.Equals(DEFAULT_PRIVATE_BIN_PATH) &&
                             ShadowCopyDirectories.Equals(DEFAULT_SHADOW_COPY_DIRECTORIES);

    /// <summary>Resets the strings.</summary>
    public void Reset()
    {
        ApplicationBase = DEFAULT_APPLICATION_BASE;

        ApplicationName = DEFAULT_APPLICATION_NAME;

        CachePath = DEFAULT_CACHE_PATH;

        CallingAssembly = DEFAULT_CALLING_ASSEMBLY;

        ConfigurationFile = DEFAULT_CONFIGURATION_FILE;

        DynamicBase = DEFAULT_DYNAMIC_BASE;

        FriendlyName = DEFAULT_FRIENDLY_NAME;

        EntryAssembly = DEFAULT_ENTRY_ASSEMBLY;

        ExecutingAssembly = DEFAULT_EXECUTING_ASSEMBLY;

        LicenseFile = DEFAULT_LICENSE_FILE;

        PrivateBinPath = DEFAULT_PRIVATE_BIN_PATH;

        ShadowCopyDirectories = DEFAULT_SHADOW_COPY_DIRECTORIES;
    }

    #endregion
}
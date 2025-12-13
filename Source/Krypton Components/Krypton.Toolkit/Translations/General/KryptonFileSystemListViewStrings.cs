#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonFileSystemListViewStrings : Storage
{
    #region Static Strings

    private const string DEFAULT_COLUMN_NAME_NAME = @"Name";
    private const string DEFAULT_COLUMN_TYPE_NAME = @"Type";
    private const string DEFAULT_COLUMN_SIZE_NAME = @"Size";
    private const string DEFAULT_COLUMN_DATE_MODIFIED_NAME = @"Date Modified";
    private const string DEFAULT_COLUMN_DATE_CREATED_NAME = @"Date Created";
    private const string DEFAULT_COLUMN_ATTRIBUTES_NAME = @"Attributes";
    private const string DEFAULT_DIRECTORY_NOT_FOUND_MESSAGE = @"Directory not found";

    // Windows Explorer column header resource IDs in shell32.dll
    private const uint SHELL32_RESOURCE_ID_NAME = 12769;
    private const uint SHELL32_RESOURCE_ID_TYPE = 12770;
    private const uint SHELL32_RESOURCE_ID_SIZE = 12771;
    private const uint SHELL32_RESOURCE_ID_DATE_MODIFIED = 12772;
    private const uint SHELL32_RESOURCE_ID_DATE_CREATED = 12773;
    private const uint SHELL32_RESOURCE_ID_ATTRIBUTES = 12774;

    #endregion

    #region Instance Fields

    private bool _useOSStrings = false;
    private string? _cachedColumnNameName;
    private string? _cachedColumnTypeName;
    private string? _cachedColumnSizeName;
    private string? _cachedColumnDateModifiedName;
    private string? _cachedColumnDateCreatedName;
    private string? _cachedColumnAttributesName;

    #endregion

    #region Identity

    public KryptonFileSystemListViewStrings()
    {
        Reset();
    }

    #endregion

    #region Properties

    /// <summary>Gets or sets a value indicating whether to use OS-defined strings from shell32.dll.</summary>
    /// <value><c>true</c> to use OS-defined strings; otherwise, <c>false</c> to use custom strings.</value>
    [Category("Visuals")]
    [Description("Gets or sets a value indicating whether to use OS-defined strings from shell32.dll.")]
    [DefaultValue(false)]
    public bool UseOSStrings
    {
        get => _useOSStrings;
        set
        {
            if (_useOSStrings != value)
            {
                _useOSStrings = value;
                // Clear cache when switching modes
                _cachedColumnNameName = null;
                _cachedColumnTypeName = null;
                _cachedColumnSizeName = null;
                _cachedColumnDateModifiedName = null;
                _cachedColumnDateCreatedName = null;
                _cachedColumnAttributesName = null;
            }
        }
    }

    /// <summary>Gets or sets the default name for the 'Name' column.</summary>
    /// <value>The default name for the 'Name' column.</value>
    [Category("Visuals")]
    [Description("Gets or sets the default name for the 'Name' column.")]
    [DefaultValue(DEFAULT_COLUMN_NAME_NAME)]
    [Localizable(true)]
    public string ColumnNameName
    {
        get => _useOSStrings ? GetOSString(SHELL32_RESOURCE_ID_NAME, DEFAULT_COLUMN_NAME_NAME, ref _cachedColumnNameName) : _columnNameName;
        set => _columnNameName = value;
    }
    private string _columnNameName = DEFAULT_COLUMN_NAME_NAME;

    /// <summary>Gets or sets the default name for the 'Type' column.</summary>
    /// <value>The default name for the 'Type' column.</value>
    [Category("Visuals")]
    [Description("Gets or sets the default name for the 'Type' column.")]
    [DefaultValue(DEFAULT_COLUMN_TYPE_NAME)]
    [Localizable(true)]
    public string ColumnTypeName
    {
        get => _useOSStrings ? GetOSString(SHELL32_RESOURCE_ID_TYPE, DEFAULT_COLUMN_TYPE_NAME, ref _cachedColumnTypeName) : _columnTypeName;
        set => _columnTypeName = value;
    }
    private string _columnTypeName = DEFAULT_COLUMN_TYPE_NAME;

    /// <summary>Gets or sets the default name for the 'Size' column.</summary>
    /// <value>The default name for the 'Size' column.</value>
    [Category("Visuals")]
    [Description("Gets or sets the default name for the 'Size' column.")]
    [DefaultValue(DEFAULT_COLUMN_SIZE_NAME)]
    [Localizable(true)]
    public string ColumnSizeName
    {
        get => _useOSStrings ? GetOSString(SHELL32_RESOURCE_ID_SIZE, DEFAULT_COLUMN_SIZE_NAME, ref _cachedColumnSizeName) : _columnSizeName;
        set => _columnSizeName = value;
    }
    private string _columnSizeName = DEFAULT_COLUMN_SIZE_NAME;

    /// <summary>Gets or sets the default name for the 'Date Modified' column.</summary>
    /// <value>The default name for the 'Date Modified' column.</value>
    [Category("Visuals")]
    [Description("Gets or sets the default name for the 'Date Modified' column.")]
    [DefaultValue(DEFAULT_COLUMN_DATE_MODIFIED_NAME)]
    [Localizable(true)]
    public string ColumnDateModifiedName
    {
        get => _useOSStrings ? GetOSString(SHELL32_RESOURCE_ID_DATE_MODIFIED, DEFAULT_COLUMN_DATE_MODIFIED_NAME, ref _cachedColumnDateModifiedName) : _columnDateModifiedName;
        set => _columnDateModifiedName = value;
    }
    private string _columnDateModifiedName = DEFAULT_COLUMN_DATE_MODIFIED_NAME;

    /// <summary>Gets or sets the default name for the 'Date Created' column.</summary>
    /// <value>The default name for the 'Date Created' column.</value>
    [Category("Visuals")]
    [Description("Gets or sets the default name for the 'Date Created' column.")]
    [DefaultValue(DEFAULT_COLUMN_DATE_CREATED_NAME)]
    [Localizable(true)]
    public string ColumnDateCreatedName
    {
        get => _useOSStrings ? GetOSString(SHELL32_RESOURCE_ID_DATE_CREATED, DEFAULT_COLUMN_DATE_CREATED_NAME, ref _cachedColumnDateCreatedName) : _columnDateCreatedName;
        set => _columnDateCreatedName = value;
    }
    private string _columnDateCreatedName = DEFAULT_COLUMN_DATE_CREATED_NAME;

    /// <summary>Gets or sets the default name for the 'Attributes' column.</summary>
    /// <value>The default name for the 'Attributes' column.</value>
    [Category("Visuals")]
    [Description("Gets or sets the default name for the 'Attributes' column.")]
    [DefaultValue(DEFAULT_COLUMN_ATTRIBUTES_NAME)]
    [Localizable(true)]
    public string ColumnAttributesName
    {
        get => _useOSStrings ? GetOSString(SHELL32_RESOURCE_ID_ATTRIBUTES, DEFAULT_COLUMN_ATTRIBUTES_NAME, ref _cachedColumnAttributesName) : _columnAttributesName;
        set => _columnAttributesName = value;
    }
    private string _columnAttributesName = DEFAULT_COLUMN_ATTRIBUTES_NAME;

    /// <summary>Gets or sets the message displayed when a directory is not found.</summary>
    /// <value>The message displayed when a directory is not found.</value>
    [Category("Messages")]
    [Description("Gets or sets the message displayed when a directory is not found.")]
    [DefaultValue(DEFAULT_DIRECTORY_NOT_FOUND_MESSAGE)]
    [Localizable(true)]
    public string DirectoryNotFoundMessage { get; set; } = DEFAULT_DIRECTORY_NOT_FOUND_MESSAGE;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault => !_useOSStrings &&
                                      _columnNameName.Equals(DEFAULT_COLUMN_NAME_NAME) &&
                                      _columnTypeName.Equals(DEFAULT_COLUMN_TYPE_NAME) &&
                                      _columnSizeName.Equals(DEFAULT_COLUMN_SIZE_NAME) &&
                                      _columnDateModifiedName.Equals(DEFAULT_COLUMN_DATE_MODIFIED_NAME) &&
                                      _columnDateCreatedName.Equals(DEFAULT_COLUMN_DATE_CREATED_NAME) &&
                                      _columnAttributesName.Equals(DEFAULT_COLUMN_ATTRIBUTES_NAME) &&
                                      DirectoryNotFoundMessage.Equals(DEFAULT_DIRECTORY_NOT_FOUND_MESSAGE);

    #endregion

    #region Implementation

    /// <inheritdoc />
    public void Reset()
    {
        UseOSStrings = false;
        _columnNameName = DEFAULT_COLUMN_NAME_NAME;
        _columnTypeName = DEFAULT_COLUMN_TYPE_NAME;
        _columnSizeName = DEFAULT_COLUMN_SIZE_NAME;
        _columnDateModifiedName = DEFAULT_COLUMN_DATE_MODIFIED_NAME;
        _columnDateCreatedName = DEFAULT_COLUMN_DATE_CREATED_NAME;
        _columnAttributesName = DEFAULT_COLUMN_ATTRIBUTES_NAME;
        DirectoryNotFoundMessage = DEFAULT_DIRECTORY_NOT_FOUND_MESSAGE;
        // Clear cache
        _cachedColumnNameName = null;
        _cachedColumnTypeName = null;
        _cachedColumnSizeName = null;
        _cachedColumnDateModifiedName = null;
        _cachedColumnDateCreatedName = null;
        _cachedColumnAttributesName = null;
    }

    /// <summary>
    /// Gets an OS-defined string from shell32.dll, with caching and fallback to default value.
    /// </summary>
    /// <param name="resourceId">The resource ID in shell32.dll.</param>
    /// <param name="defaultValue">The default value to use if OS string cannot be loaded.</param>
    /// <param name="cache">Reference to the cached value.</param>
    /// <returns>The OS string if available, otherwise the default value.</returns>
    private static string GetOSString(uint resourceId, string defaultValue, ref string? cache)
    {
        // Return cached value if available
        if (cache != null)
        {
            return cache;
        }

        try
        {
            // Load shell32.dll as a data file (read-only, no code execution)
            using SafeModuleHandle? hModule = PI.LoadLibraryEx(
                Libraries.Shell32,
                IntPtr.Zero,
                PI.LoadLibraryExFlags.LoadLibraryAsDatafile | PI.LoadLibraryExFlags.LoadLibrarySearchSystem32);

            if (!hModule.IsInvalid)
            {
                // Try to load the string resource
                StringBuilder buffer = new StringBuilder(256);
                int length = PI.LoadString(hModule, resourceId, buffer, buffer.Capacity);

                if (length > 0)
                {
                    cache = buffer.ToString();
                    return cache;
                }
            }
        }
        catch
        {
            // If anything fails, fall through to default value
        }

        // Fallback to default value and cache it
        cache = defaultValue;
        return defaultValue;
    }

    #endregion

    #region Overrides

    public override string ToString() => !IsDefault ? "Modified" : GlobalStaticValues.DEFAULT_EMPTY_STRING;

    #endregion
}
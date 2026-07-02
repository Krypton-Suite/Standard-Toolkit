#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Behavior settings for <see cref="KryptonDropZone"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonDropZoneBehaviorValues : Storage
{
    #region Static Defaults

    private static readonly string[] DefaultAllowedExtensions =
    [
        ".txt", ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".xls", ".xlsx", ".csv", ".pdf", ".doc", ".docx"
    ];

    private const bool DEFAULT_SHOW_FILE_LIST_VIEW = true;
    private const bool DEFAULT_SHOW_CLEAR_BUTTON = true;
    private const bool DEFAULT_SHOW_BROWSE_BUTTON = true;
    private const bool DEFAULT_SHOW_STATUS_LABEL = true;
    private const int DEFAULT_MAX_FILE_COUNT = 0;
    private const long DEFAULT_MAX_FILE_SIZE = 0;
    private const long DEFAULT_UPLOAD_SIZE_QUOTA = 0;
    private const bool DEFAULT_SHOW_UPLOAD_QUOTA_PROGRESS_BAR = false;
    private const bool DEFAULT_ALLOW_DIRECTORIES = true;
    private const bool DEFAULT_SEARCH_SUBDIRECTORIES = false;
    private const bool DEFAULT_ENABLE_UNDO = true;

    #endregion

    #region Instance Fields

    private KryptonDropZone? _owner;
    private bool _showFileListView = DEFAULT_SHOW_FILE_LIST_VIEW;
    private bool _showClearButton = DEFAULT_SHOW_CLEAR_BUTTON;
    private bool _showBrowseButton = DEFAULT_SHOW_BROWSE_BUTTON;
    private bool _showStatusLabel = DEFAULT_SHOW_STATUS_LABEL;
    private readonly List<string> _allowedExtensions;
    private int _maxFileCount = DEFAULT_MAX_FILE_COUNT;
    private long _maxFileSize = DEFAULT_MAX_FILE_SIZE;
    private long _uploadSizeQuota = DEFAULT_UPLOAD_SIZE_QUOTA;
    private bool _showUploadQuotaProgressBar = DEFAULT_SHOW_UPLOAD_QUOTA_PROGRESS_BAR;
    private bool _allowDirectories = DEFAULT_ALLOW_DIRECTORIES;
    private bool _searchSubdirectories = DEFAULT_SEARCH_SUBDIRECTORIES;
    private bool _enableUndo = DEFAULT_ENABLE_UNDO;

    #endregion

    #region Identity

    internal KryptonDropZoneBehaviorValues(KryptonDropZone owner)
    {
        _owner = owner;
        _allowedExtensions = new List<string>(DefaultAllowedExtensions);
    }

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        _showFileListView == DEFAULT_SHOW_FILE_LIST_VIEW &&
        _showClearButton == DEFAULT_SHOW_CLEAR_BUTTON &&
        _showBrowseButton == DEFAULT_SHOW_BROWSE_BUTTON &&
        _showStatusLabel == DEFAULT_SHOW_STATUS_LABEL &&
        AllowedExtensionsEqualsDefault() &&
        _maxFileCount == DEFAULT_MAX_FILE_COUNT &&
        _maxFileSize == DEFAULT_MAX_FILE_SIZE &&
        _uploadSizeQuota == DEFAULT_UPLOAD_SIZE_QUOTA &&
        _showUploadQuotaProgressBar == DEFAULT_SHOW_UPLOAD_QUOTA_PROGRESS_BAR &&
        _allowDirectories == DEFAULT_ALLOW_DIRECTORIES &&
        _searchSubdirectories == DEFAULT_SEARCH_SUBDIRECTORIES &&
        _enableUndo == DEFAULT_ENABLE_UNDO;

    #endregion

    #region Display

    [Category(@"Display")]
    [Description(@"Whether to display the list of dropped files.")]
    [DefaultValue(DEFAULT_SHOW_FILE_LIST_VIEW)]
    public bool ShowFileListView
    {
        get => _showFileListView;
        set => SetValue(ref _showFileListView, value);
    }

    [Category(@"Display")]
    [Description(@"Whether to display the Clear All button.")]
    [DefaultValue(DEFAULT_SHOW_CLEAR_BUTTON)]
    public bool ShowClearButton
    {
        get => _showClearButton;
        set => SetValue(ref _showClearButton, value);
    }

    [Category(@"Display")]
    [Description(@"Whether to display a Browse button as a keyboard-accessible alternative to drag-and-drop.")]
    [DefaultValue(DEFAULT_SHOW_BROWSE_BUTTON)]
    public bool ShowBrowseButton
    {
        get => _showBrowseButton;
        set => SetValue(ref _showBrowseButton, value);
    }

    [Category(@"Display")]
    [Description(@"Whether to display the status summary (file count / size).")]
    [DefaultValue(DEFAULT_SHOW_STATUS_LABEL)]
    public bool ShowStatusLabel
    {
        get => _showStatusLabel;
        set => SetValue(ref _showStatusLabel, value);
    }

    [Category(@"Display")]
    [Description(@"Whether to display a progress bar showing upload size quota usage.")]
    [DefaultValue(DEFAULT_SHOW_UPLOAD_QUOTA_PROGRESS_BAR)]
    public bool ShowUploadQuotaProgressBar
    {
        get => _showUploadQuotaProgressBar;
        set => SetValue(ref _showUploadQuotaProgressBar, value);
    }

    #endregion

    #region Validation

    [Category(@"Validation")]
    [Description(@"A list of allowed file extensions (including the dot, e.g., '.txt').")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<string> AllowedExtensions => _allowedExtensions;

    [Category(@"Validation")]
    [Description(@"Maximum number of files that can be dropped. Set to 0 for unlimited.")]
    [DefaultValue(DEFAULT_MAX_FILE_COUNT)]
    public int MaxFileCount
    {
        get => _maxFileCount;
        set => SetValue(ref _maxFileCount, Math.Max(0, value));
    }

    [Category(@"Validation")]
    [Description(@"Maximum file size in bytes. Set to 0 for unlimited.")]
    [DefaultValue(DEFAULT_MAX_FILE_SIZE)]
    public long MaxFileSize
    {
        get => _maxFileSize;
        set => SetValue(ref _maxFileSize, Math.Max(0, value));
    }

    [Category(@"Validation")]
    [Description(@"Maximum combined size in bytes for all dropped files. Set to 0 for unlimited.")]
    [DefaultValue(DEFAULT_UPLOAD_SIZE_QUOTA)]
    public long UploadSizeQuota
    {
        get => _uploadSizeQuota;
        set => SetValue(ref _uploadSizeQuota, Math.Max(0, value));
    }

    #endregion

    #region Drag And Drop

    [Category(@"Drag And Drop")]
    [Description(@"Whether to allow dropping directories.")]
    [DefaultValue(DEFAULT_ALLOW_DIRECTORIES)]
    public bool AllowDirectories
    {
        get => _allowDirectories;
        set => SetValue(ref _allowDirectories, value);
    }

    [Category(@"Drag And Drop")]
    [Description(@"Whether to search subdirectories when dropping folders.")]
    [DefaultValue(DEFAULT_SEARCH_SUBDIRECTORIES)]
    public bool SearchSubdirectories
    {
        get => _searchSubdirectories;
        set => SetValue(ref _searchSubdirectories, value);
    }

    #endregion

    #region Editing

    [Category(@"Editing")]
    [Description(@"Whether undo (Ctrl+Z) is enabled for Clear/Remove/Drop operations.")]
    [DefaultValue(DEFAULT_ENABLE_UNDO)]
    public bool EnableUndo
    {
        get => _enableUndo;
        set => SetValue(ref _enableUndo, value);
    }

    #endregion

    #region Implementation

    public void Reset()
    {
        _showFileListView = DEFAULT_SHOW_FILE_LIST_VIEW;
        _showClearButton = DEFAULT_SHOW_CLEAR_BUTTON;
        _showBrowseButton = DEFAULT_SHOW_BROWSE_BUTTON;
        _showStatusLabel = DEFAULT_SHOW_STATUS_LABEL;
        _allowedExtensions.Clear();
        _allowedExtensions.AddRange(DefaultAllowedExtensions);
        _maxFileCount = DEFAULT_MAX_FILE_COUNT;
        _maxFileSize = DEFAULT_MAX_FILE_SIZE;
        _uploadSizeQuota = DEFAULT_UPLOAD_SIZE_QUOTA;
        _showUploadQuotaProgressBar = DEFAULT_SHOW_UPLOAD_QUOTA_PROGRESS_BAR;
        _allowDirectories = DEFAULT_ALLOW_DIRECTORIES;
        _searchSubdirectories = DEFAULT_SEARCH_SUBDIRECTORIES;
        _enableUndo = DEFAULT_ENABLE_UNDO;
        _owner?.OnBehaviorValuesChanged(null);
    }

    internal void SetOwner(KryptonDropZone owner) => _owner = owner;

    private bool ShouldSerializeAllowedExtensions() => !AllowedExtensionsEqualsDefault();

    private void ResetAllowedExtensions()
    {
        _allowedExtensions.Clear();
        _allowedExtensions.AddRange(DefaultAllowedExtensions);
        _owner?.OnBehaviorValuesChanged(nameof(AllowedExtensions));
    }

    private bool AllowedExtensionsEqualsDefault()
    {
        if (_allowedExtensions.Count != DefaultAllowedExtensions.Length)
        {
            return false;
        }

        for (int i = 0; i < DefaultAllowedExtensions.Length; i++)
        {
            if (!string.Equals(_allowedExtensions[i], DefaultAllowedExtensions[i], StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
        }

        return true;
    }

    private void SetValue<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return;
        }

        field = value;
        _owner?.OnBehaviorValuesChanged(propertyName);
    }

    #endregion
}

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
/// Localizable strings used by <see cref="KryptonDropZone"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonDropZoneStrings : Storage
{
    #region Static Defaults

    internal const string DEFAULT_DROP_ZONE_TEXT = @"Drag files here, or browse";
    internal const string DEFAULT_FILE_NAME_COLUMN = @"File Name";
    internal const string DEFAULT_CLEAR_ALL_BUTTON = @"Clear All";
    internal const string DEFAULT_BROWSE_BUTTON = @"Browse Files...";
    internal const string DEFAULT_FILE_LIST_ACCESSIBLE_NAME = @"Dropped files list";
    internal const string DEFAULT_FILE_LIST_ACCESSIBLE_DESCRIPTION = @"List of files added to the drop zone. Press Delete to remove selected files, Ctrl+Z to undo.";
    internal const string DEFAULT_BROWSE_ACCESSIBLE_NAME = @"Browse for files";
    internal const string DEFAULT_BROWSE_ACCESSIBLE_DESCRIPTION = @"Opens a file picker as a keyboard-accessible alternative to drag-and-drop.";
    internal const string DEFAULT_DROP_ZONE_ACCESSIBLE_NAME = @"File drop zone";
    internal const string DEFAULT_DROP_ZONE_ACCESSIBLE_DESCRIPTION = @"Drag and drop files or folders here, or use the Browse Files button.";
    internal const string DEFAULT_MENU_OPEN = @"Open";
    internal const string DEFAULT_MENU_REMOVE = @"Remove Selected";
    internal const string DEFAULT_MENU_OPEN_FOLDER = @"Open Containing Folder";
    internal const string DEFAULT_MENU_COPY_PATHS = @"Copy Path(s)";
    internal const string DEFAULT_MENU_SORT_BY = @"Sort By";
    internal const string DEFAULT_MENU_SORT_NAME = @"Name";
    internal const string DEFAULT_MENU_SORT_SIZE = @"Size";
    internal const string DEFAULT_MENU_SORT_DATE = @"Date Modified";
    internal const string DEFAULT_MENU_SORT_EXTENSION = @"Extension";
    internal const string DEFAULT_MENU_UNDO = @"Undo";
    internal const string DEFAULT_MENU_CLEAR = @"Clear All";
    internal const string DEFAULT_SHORTCUT_DELETE = @"Del";
    internal const string DEFAULT_SHORTCUT_COPY = @"Ctrl+C";
    internal const string DEFAULT_SHORTCUT_UNDO = @"Ctrl+Z";
    internal const string DEFAULT_OPEN_FILE_DIALOG_TITLE = @"Select files to add";
    internal const string DEFAULT_OPEN_FILE_DIALOG_FILTER_FORMAT = @"Allowed files ({0})|{0}|All files (*.*)|*.*";
    internal const string DEFAULT_CUSTOM_VALIDATION_REJECTED = @"Rejected by custom validation.";
    internal const string DEFAULT_EXTENSION_NOT_ALLOWED_FORMAT = @"Extension '{0}' is not allowed.";
    internal const string DEFAULT_FILE_EXCEEDS_MAX_SIZE_FORMAT = @"File exceeds the maximum size of {0}.";
    internal const string DEFAULT_UNABLE_TO_INSPECT_FILE_FORMAT = @"Unable to inspect file: {0}";
    internal const string DEFAULT_EXCEEDS_MAX_FILE_COUNT_FORMAT = @"Exceeds maximum file count ({0}).";
    internal const string DEFAULT_SKIPPED_FILE_TOOL_TIP_FORMAT = @"{0}{1}Skipped: {2}";
    internal const string DEFAULT_SKIPPED_FILE_TOOL_TIP_NEW_LINE = "\n";
    internal const string DEFAULT_STATUS_EMPTY = @"No files added";
    internal const string DEFAULT_STATUS_WITH_INVALID_FORMAT = @"{0} files ({1} valid, {2} skipped) • {3}";
    internal const string DEFAULT_STATUS_FORMAT = @"{0} files • {1}";
    internal const string DEFAULT_UNABLE_TO_OPEN_FILE_FORMAT = @"Unable to open file: {0}";
    internal const string DEFAULT_UNABLE_TO_OPEN_FOLDER_FORMAT = @"Unable to open folder: {0}";
    internal const string DEFAULT_ERROR_DIALOG_TITLE = @"Error";
    internal const string DEFAULT_BYTE_UNIT = @"B";
    internal const string DEFAULT_KILOBYTE_UNIT = @"KB";
    internal const string DEFAULT_MEGABYTE_UNIT = @"MB";
    internal const string DEFAULT_GIGABYTE_UNIT = @"GB";
    internal const string DEFAULT_TERABYTE_UNIT = @"TB";
    internal const string DEFAULT_UPLOAD_QUOTA_FORMAT = @"{0} of {1} used • {2} remaining";
    internal const string DEFAULT_EXCEEDS_UPLOAD_QUOTA_FORMAT = @"Would exceed the upload size quota ({0} remaining).";
    internal const string DEFAULT_UPLOAD_QUOTA_ACCESSIBLE_NAME = @"Upload size quota";
    internal const string DEFAULT_UPLOAD_QUOTA_ACCESSIBLE_DESCRIPTION = @"Shows how much of the total upload size quota has been used.";
    internal const string DEFAULT_HEADER_TEXT = @"";
    internal const string DEFAULT_PREVIEW_HEADER = @"Preview:";
    internal const string DEFAULT_CANCEL_BUTTON = @"Cancel";
    internal const string DEFAULT_SUBMIT_BUTTON = @"Submit";

    #endregion

    #region Instance Fields

    private KryptonDropZone? _owner;
    private string _dropZoneText = DEFAULT_DROP_ZONE_TEXT;
    private string _fileNameColumn = DEFAULT_FILE_NAME_COLUMN;
    private string _clearAllButton = DEFAULT_CLEAR_ALL_BUTTON;
    private string _browseButton = DEFAULT_BROWSE_BUTTON;
    private string _fileListAccessibleName = DEFAULT_FILE_LIST_ACCESSIBLE_NAME;
    private string _fileListAccessibleDescription = DEFAULT_FILE_LIST_ACCESSIBLE_DESCRIPTION;
    private string _browseAccessibleName = DEFAULT_BROWSE_ACCESSIBLE_NAME;
    private string _browseAccessibleDescription = DEFAULT_BROWSE_ACCESSIBLE_DESCRIPTION;
    private string _dropZoneAccessibleName = DEFAULT_DROP_ZONE_ACCESSIBLE_NAME;
    private string _dropZoneAccessibleDescription = DEFAULT_DROP_ZONE_ACCESSIBLE_DESCRIPTION;
    private string _menuOpen = DEFAULT_MENU_OPEN;
    private string _menuRemove = DEFAULT_MENU_REMOVE;
    private string _menuOpenFolder = DEFAULT_MENU_OPEN_FOLDER;
    private string _menuCopyPaths = DEFAULT_MENU_COPY_PATHS;
    private string _menuSortBy = DEFAULT_MENU_SORT_BY;
    private string _menuSortName = DEFAULT_MENU_SORT_NAME;
    private string _menuSortSize = DEFAULT_MENU_SORT_SIZE;
    private string _menuSortDate = DEFAULT_MENU_SORT_DATE;
    private string _menuSortExtension = DEFAULT_MENU_SORT_EXTENSION;
    private string _menuUndo = DEFAULT_MENU_UNDO;
    private string _menuClear = DEFAULT_MENU_CLEAR;
    private string _shortcutDelete = DEFAULT_SHORTCUT_DELETE;
    private string _shortcutCopy = DEFAULT_SHORTCUT_COPY;
    private string _shortcutUndo = DEFAULT_SHORTCUT_UNDO;
    private string _openFileDialogTitle = DEFAULT_OPEN_FILE_DIALOG_TITLE;
    private string _openFileDialogFilterFormat = DEFAULT_OPEN_FILE_DIALOG_FILTER_FORMAT;
    private string _customValidationRejected = DEFAULT_CUSTOM_VALIDATION_REJECTED;
    private string _extensionNotAllowedFormat = DEFAULT_EXTENSION_NOT_ALLOWED_FORMAT;
    private string _fileExceedsMaxSizeFormat = DEFAULT_FILE_EXCEEDS_MAX_SIZE_FORMAT;
    private string _unableToInspectFileFormat = DEFAULT_UNABLE_TO_INSPECT_FILE_FORMAT;
    private string _exceedsMaxFileCountFormat = DEFAULT_EXCEEDS_MAX_FILE_COUNT_FORMAT;
    private string _skippedFileToolTipFormat = DEFAULT_SKIPPED_FILE_TOOL_TIP_FORMAT;
    private string _skippedFileToolTipNewLine = DEFAULT_SKIPPED_FILE_TOOL_TIP_NEW_LINE;
    private string _statusEmpty = DEFAULT_STATUS_EMPTY;
    private string _statusWithInvalidFormat = DEFAULT_STATUS_WITH_INVALID_FORMAT;
    private string _statusFormat = DEFAULT_STATUS_FORMAT;
    private string _unableToOpenFileFormat = DEFAULT_UNABLE_TO_OPEN_FILE_FORMAT;
    private string _unableToOpenFolderFormat = DEFAULT_UNABLE_TO_OPEN_FOLDER_FORMAT;
    private string _errorDialogTitle = DEFAULT_ERROR_DIALOG_TITLE;
    private string _byteUnit = DEFAULT_BYTE_UNIT;
    private string _kilobyteUnit = DEFAULT_KILOBYTE_UNIT;
    private string _megabyteUnit = DEFAULT_MEGABYTE_UNIT;
    private string _gigabyteUnit = DEFAULT_GIGABYTE_UNIT;
    private string _terabyteUnit = DEFAULT_TERABYTE_UNIT;
    private string _uploadQuotaFormat = DEFAULT_UPLOAD_QUOTA_FORMAT;
    private string _exceedsUploadQuotaFormat = DEFAULT_EXCEEDS_UPLOAD_QUOTA_FORMAT;
    private string _uploadQuotaAccessibleName = DEFAULT_UPLOAD_QUOTA_ACCESSIBLE_NAME;
    private string _uploadQuotaAccessibleDescription = DEFAULT_UPLOAD_QUOTA_ACCESSIBLE_DESCRIPTION;
    private string _headerText = DEFAULT_HEADER_TEXT;
    private string _previewHeader = DEFAULT_PREVIEW_HEADER;
    private string _cancelButton = DEFAULT_CANCEL_BUTTON;
    private string _submitButton = DEFAULT_SUBMIT_BUTTON;

    #endregion

    #region Identity

    internal KryptonDropZoneStrings(KryptonDropZone owner)
    {
        _owner = owner;
    }

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        _dropZoneText == DEFAULT_DROP_ZONE_TEXT &&
        _fileNameColumn == DEFAULT_FILE_NAME_COLUMN &&
        _clearAllButton == DEFAULT_CLEAR_ALL_BUTTON &&
        _browseButton == DEFAULT_BROWSE_BUTTON &&
        _fileListAccessibleName == DEFAULT_FILE_LIST_ACCESSIBLE_NAME &&
        _fileListAccessibleDescription == DEFAULT_FILE_LIST_ACCESSIBLE_DESCRIPTION &&
        _browseAccessibleName == DEFAULT_BROWSE_ACCESSIBLE_NAME &&
        _browseAccessibleDescription == DEFAULT_BROWSE_ACCESSIBLE_DESCRIPTION &&
        _dropZoneAccessibleName == DEFAULT_DROP_ZONE_ACCESSIBLE_NAME &&
        _dropZoneAccessibleDescription == DEFAULT_DROP_ZONE_ACCESSIBLE_DESCRIPTION &&
        _menuOpen == DEFAULT_MENU_OPEN &&
        _menuRemove == DEFAULT_MENU_REMOVE &&
        _menuOpenFolder == DEFAULT_MENU_OPEN_FOLDER &&
        _menuCopyPaths == DEFAULT_MENU_COPY_PATHS &&
        _menuSortBy == DEFAULT_MENU_SORT_BY &&
        _menuSortName == DEFAULT_MENU_SORT_NAME &&
        _menuSortSize == DEFAULT_MENU_SORT_SIZE &&
        _menuSortDate == DEFAULT_MENU_SORT_DATE &&
        _menuSortExtension == DEFAULT_MENU_SORT_EXTENSION &&
        _menuUndo == DEFAULT_MENU_UNDO &&
        _menuClear == DEFAULT_MENU_CLEAR &&
        _shortcutDelete == DEFAULT_SHORTCUT_DELETE &&
        _shortcutCopy == DEFAULT_SHORTCUT_COPY &&
        _shortcutUndo == DEFAULT_SHORTCUT_UNDO &&
        _openFileDialogTitle == DEFAULT_OPEN_FILE_DIALOG_TITLE &&
        _openFileDialogFilterFormat == DEFAULT_OPEN_FILE_DIALOG_FILTER_FORMAT &&
        _customValidationRejected == DEFAULT_CUSTOM_VALIDATION_REJECTED &&
        _extensionNotAllowedFormat == DEFAULT_EXTENSION_NOT_ALLOWED_FORMAT &&
        _fileExceedsMaxSizeFormat == DEFAULT_FILE_EXCEEDS_MAX_SIZE_FORMAT &&
        _unableToInspectFileFormat == DEFAULT_UNABLE_TO_INSPECT_FILE_FORMAT &&
        _exceedsMaxFileCountFormat == DEFAULT_EXCEEDS_MAX_FILE_COUNT_FORMAT &&
        _skippedFileToolTipFormat == DEFAULT_SKIPPED_FILE_TOOL_TIP_FORMAT &&
        _skippedFileToolTipNewLine == DEFAULT_SKIPPED_FILE_TOOL_TIP_NEW_LINE &&
        _statusEmpty == DEFAULT_STATUS_EMPTY &&
        _statusWithInvalidFormat == DEFAULT_STATUS_WITH_INVALID_FORMAT &&
        _statusFormat == DEFAULT_STATUS_FORMAT &&
        _unableToOpenFileFormat == DEFAULT_UNABLE_TO_OPEN_FILE_FORMAT &&
        _unableToOpenFolderFormat == DEFAULT_UNABLE_TO_OPEN_FOLDER_FORMAT &&
        _errorDialogTitle == DEFAULT_ERROR_DIALOG_TITLE &&
        _byteUnit == DEFAULT_BYTE_UNIT &&
        _kilobyteUnit == DEFAULT_KILOBYTE_UNIT &&
        _megabyteUnit == DEFAULT_MEGABYTE_UNIT &&
        _gigabyteUnit == DEFAULT_GIGABYTE_UNIT &&
        _terabyteUnit == DEFAULT_TERABYTE_UNIT &&
        _uploadQuotaFormat == DEFAULT_UPLOAD_QUOTA_FORMAT &&
        _exceedsUploadQuotaFormat == DEFAULT_EXCEEDS_UPLOAD_QUOTA_FORMAT &&
        _uploadQuotaAccessibleName == DEFAULT_UPLOAD_QUOTA_ACCESSIBLE_NAME &&
        _uploadQuotaAccessibleDescription == DEFAULT_UPLOAD_QUOTA_ACCESSIBLE_DESCRIPTION &&
        _headerText == DEFAULT_HEADER_TEXT &&
        _previewHeader == DEFAULT_PREVIEW_HEADER &&
        _cancelButton == DEFAULT_CANCEL_BUTTON &&
        _submitButton == DEFAULT_SUBMIT_BUTTON;

    #endregion

    #region Appearance

    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Optional header text shown above the drop zone in Card layout. Leave empty to hide.")]
    [DefaultValue(DEFAULT_HEADER_TEXT)]
    [RefreshProperties(RefreshProperties.All)]
    public string HeaderText
    {
        get => _headerText;
        set => SetString(ref _headerText, value, DEFAULT_HEADER_TEXT);
    }

    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Header text shown above the file preview list in Card layout.")]
    [DefaultValue(DEFAULT_PREVIEW_HEADER)]
    [RefreshProperties(RefreshProperties.All)]
    public string PreviewHeader
    {
        get => _previewHeader;
        set => SetString(ref _previewHeader, value, DEFAULT_PREVIEW_HEADER);
    }

    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Text displayed in the drop zone panel.")]
    [DefaultValue(DEFAULT_DROP_ZONE_TEXT)]
    [RefreshProperties(RefreshProperties.All)]
    public string DropZoneText
    {
        get => _dropZoneText;
        set => SetString(ref _dropZoneText, value, DEFAULT_DROP_ZONE_TEXT);
    }

    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Header text for the file name column in the dropped-files list.")]
    [DefaultValue(DEFAULT_FILE_NAME_COLUMN)]
    [RefreshProperties(RefreshProperties.All)]
    public string FileNameColumn
    {
        get => _fileNameColumn;
        set => SetString(ref _fileNameColumn, value, DEFAULT_FILE_NAME_COLUMN);
    }

    #endregion

    #region Buttons

    [Localizable(true)]
    [Category(@"Buttons")]
    [Description(@"Text for the Clear All button.")]
    [DefaultValue(DEFAULT_CLEAR_ALL_BUTTON)]
    [RefreshProperties(RefreshProperties.All)]
    public string ClearAllButton
    {
        get => _clearAllButton;
        set => SetString(ref _clearAllButton, value, DEFAULT_CLEAR_ALL_BUTTON);
    }

    [Localizable(true)]
    [Category(@"Buttons")]
    [Description(@"Text for the Browse Files button.")]
    [DefaultValue(DEFAULT_BROWSE_BUTTON)]
    [RefreshProperties(RefreshProperties.All)]
    public string BrowseButton
    {
        get => _browseButton;
        set => SetString(ref _browseButton, value, DEFAULT_BROWSE_BUTTON);
    }

    [Localizable(true)]
    [Category(@"Buttons")]
    [Description(@"Text for the Cancel action button in Card layout.")]
    [DefaultValue(DEFAULT_CANCEL_BUTTON)]
    [RefreshProperties(RefreshProperties.All)]
    public string CancelButton
    {
        get => _cancelButton;
        set => SetString(ref _cancelButton, value, DEFAULT_CANCEL_BUTTON);
    }

    [Localizable(true)]
    [Category(@"Buttons")]
    [Description(@"Text for the Submit action button in Card layout.")]
    [DefaultValue(DEFAULT_SUBMIT_BUTTON)]
    [RefreshProperties(RefreshProperties.All)]
    public string SubmitButton
    {
        get => _submitButton;
        set => SetString(ref _submitButton, value, DEFAULT_SUBMIT_BUTTON);
    }

    #endregion

    #region Accessibility

    [Localizable(true)]
    [Category(@"Accessibility")]
    [Description(@"Accessible name for the dropped-files list.")]
    [DefaultValue(DEFAULT_FILE_LIST_ACCESSIBLE_NAME)]
    [RefreshProperties(RefreshProperties.All)]
    public string FileListAccessibleName
    {
        get => _fileListAccessibleName;
        set => SetString(ref _fileListAccessibleName, value, DEFAULT_FILE_LIST_ACCESSIBLE_NAME);
    }

    [Localizable(true)]
    [Category(@"Accessibility")]
    [Description(@"Accessible description for the dropped-files list.")]
    [DefaultValue(DEFAULT_FILE_LIST_ACCESSIBLE_DESCRIPTION)]
    [RefreshProperties(RefreshProperties.All)]
    public string FileListAccessibleDescription
    {
        get => _fileListAccessibleDescription;
        set => SetString(ref _fileListAccessibleDescription, value, DEFAULT_FILE_LIST_ACCESSIBLE_DESCRIPTION);
    }

    [Localizable(true)]
    [Category(@"Accessibility")]
    [Description(@"Accessible name for the Browse Files button.")]
    [DefaultValue(DEFAULT_BROWSE_ACCESSIBLE_NAME)]
    [RefreshProperties(RefreshProperties.All)]
    public string BrowseAccessibleName
    {
        get => _browseAccessibleName;
        set => SetString(ref _browseAccessibleName, value, DEFAULT_BROWSE_ACCESSIBLE_NAME);
    }

    [Localizable(true)]
    [Category(@"Accessibility")]
    [Description(@"Accessible description for the Browse Files button.")]
    [DefaultValue(DEFAULT_BROWSE_ACCESSIBLE_DESCRIPTION)]
    [RefreshProperties(RefreshProperties.All)]
    public string BrowseAccessibleDescription
    {
        get => _browseAccessibleDescription;
        set => SetString(ref _browseAccessibleDescription, value, DEFAULT_BROWSE_ACCESSIBLE_DESCRIPTION);
    }

    [Localizable(true)]
    [Category(@"Accessibility")]
    [Description(@"Accessible name for the drop zone panel.")]
    [DefaultValue(DEFAULT_DROP_ZONE_ACCESSIBLE_NAME)]
    [RefreshProperties(RefreshProperties.All)]
    public string DropZoneAccessibleName
    {
        get => _dropZoneAccessibleName;
        set => SetString(ref _dropZoneAccessibleName, value, DEFAULT_DROP_ZONE_ACCESSIBLE_NAME);
    }

    [Localizable(true)]
    [Category(@"Accessibility")]
    [Description(@"Accessible description for the drop zone panel.")]
    [DefaultValue(DEFAULT_DROP_ZONE_ACCESSIBLE_DESCRIPTION)]
    [RefreshProperties(RefreshProperties.All)]
    public string DropZoneAccessibleDescription
    {
        get => _dropZoneAccessibleDescription;
        set => SetString(ref _dropZoneAccessibleDescription, value, DEFAULT_DROP_ZONE_ACCESSIBLE_DESCRIPTION);
    }

    #endregion

    #region Context Menu

    [Localizable(true)]
    [Category(@"Context Menu")]
    [Description(@"Context menu item text for opening the selected file.")]
    [DefaultValue(DEFAULT_MENU_OPEN)]
    [RefreshProperties(RefreshProperties.All)]
    public string MenuOpen
    {
        get => _menuOpen;
        set => SetString(ref _menuOpen, value, DEFAULT_MENU_OPEN);
    }

    [Localizable(true)]
    [Category(@"Context Menu")]
    [Description(@"Context menu item text for removing selected files.")]
    [DefaultValue(DEFAULT_MENU_REMOVE)]
    [RefreshProperties(RefreshProperties.All)]
    public string MenuRemove
    {
        get => _menuRemove;
        set => SetString(ref _menuRemove, value, DEFAULT_MENU_REMOVE);
    }

    [Localizable(true)]
    [Category(@"Context Menu")]
    [Description(@"Context menu item text for opening the containing folder.")]
    [DefaultValue(DEFAULT_MENU_OPEN_FOLDER)]
    [RefreshProperties(RefreshProperties.All)]
    public string MenuOpenFolder
    {
        get => _menuOpenFolder;
        set => SetString(ref _menuOpenFolder, value, DEFAULT_MENU_OPEN_FOLDER);
    }

    [Localizable(true)]
    [Category(@"Context Menu")]
    [Description(@"Context menu item text for copying selected file paths.")]
    [DefaultValue(DEFAULT_MENU_COPY_PATHS)]
    [RefreshProperties(RefreshProperties.All)]
    public string MenuCopyPaths
    {
        get => _menuCopyPaths;
        set => SetString(ref _menuCopyPaths, value, DEFAULT_MENU_COPY_PATHS);
    }

    [Localizable(true)]
    [Category(@"Context Menu")]
    [Description(@"Context menu parent item text for sort options.")]
    [DefaultValue(DEFAULT_MENU_SORT_BY)]
    [RefreshProperties(RefreshProperties.All)]
    public string MenuSortBy
    {
        get => _menuSortBy;
        set => SetString(ref _menuSortBy, value, DEFAULT_MENU_SORT_BY);
    }

    [Localizable(true)]
    [Category(@"Context Menu")]
    [Description(@"Sort menu item text for sorting by file name.")]
    [DefaultValue(DEFAULT_MENU_SORT_NAME)]
    [RefreshProperties(RefreshProperties.All)]
    public string MenuSortName
    {
        get => _menuSortName;
        set => SetString(ref _menuSortName, value, DEFAULT_MENU_SORT_NAME);
    }

    [Localizable(true)]
    [Category(@"Context Menu")]
    [Description(@"Sort menu item text for sorting by file size.")]
    [DefaultValue(DEFAULT_MENU_SORT_SIZE)]
    [RefreshProperties(RefreshProperties.All)]
    public string MenuSortSize
    {
        get => _menuSortSize;
        set => SetString(ref _menuSortSize, value, DEFAULT_MENU_SORT_SIZE);
    }

    [Localizable(true)]
    [Category(@"Context Menu")]
    [Description(@"Sort menu item text for sorting by date modified.")]
    [DefaultValue(DEFAULT_MENU_SORT_DATE)]
    [RefreshProperties(RefreshProperties.All)]
    public string MenuSortDate
    {
        get => _menuSortDate;
        set => SetString(ref _menuSortDate, value, DEFAULT_MENU_SORT_DATE);
    }

    [Localizable(true)]
    [Category(@"Context Menu")]
    [Description(@"Sort menu item text for sorting by extension.")]
    [DefaultValue(DEFAULT_MENU_SORT_EXTENSION)]
    [RefreshProperties(RefreshProperties.All)]
    public string MenuSortExtension
    {
        get => _menuSortExtension;
        set => SetString(ref _menuSortExtension, value, DEFAULT_MENU_SORT_EXTENSION);
    }

    [Localizable(true)]
    [Category(@"Context Menu")]
    [Description(@"Context menu item text for undo.")]
    [DefaultValue(DEFAULT_MENU_UNDO)]
    [RefreshProperties(RefreshProperties.All)]
    public string MenuUndo
    {
        get => _menuUndo;
        set => SetString(ref _menuUndo, value, DEFAULT_MENU_UNDO);
    }

    [Localizable(true)]
    [Category(@"Context Menu")]
    [Description(@"Context menu item text for clearing all files.")]
    [DefaultValue(DEFAULT_MENU_CLEAR)]
    [RefreshProperties(RefreshProperties.All)]
    public string MenuClear
    {
        get => _menuClear;
        set => SetString(ref _menuClear, value, DEFAULT_MENU_CLEAR);
    }

    [Localizable(true)]
    [Category(@"Context Menu")]
    [Description(@"Shortcut hint displayed for the remove menu item.")]
    [DefaultValue(DEFAULT_SHORTCUT_DELETE)]
    [RefreshProperties(RefreshProperties.All)]
    public string ShortcutDelete
    {
        get => _shortcutDelete;
        set => SetString(ref _shortcutDelete, value, DEFAULT_SHORTCUT_DELETE);
    }

    [Localizable(true)]
    [Category(@"Context Menu")]
    [Description(@"Shortcut hint displayed for the copy paths menu item.")]
    [DefaultValue(DEFAULT_SHORTCUT_COPY)]
    [RefreshProperties(RefreshProperties.All)]
    public string ShortcutCopy
    {
        get => _shortcutCopy;
        set => SetString(ref _shortcutCopy, value, DEFAULT_SHORTCUT_COPY);
    }

    [Localizable(true)]
    [Category(@"Context Menu")]
    [Description(@"Shortcut hint displayed for the undo menu item.")]
    [DefaultValue(DEFAULT_SHORTCUT_UNDO)]
    [RefreshProperties(RefreshProperties.All)]
    public string ShortcutUndo
    {
        get => _shortcutUndo;
        set => SetString(ref _shortcutUndo, value, DEFAULT_SHORTCUT_UNDO);
    }

    #endregion

    #region Dialogs

    [Localizable(true)]
    [Category(@"Dialogs")]
    [Description(@"Title for the browse/open file dialog.")]
    [DefaultValue(DEFAULT_OPEN_FILE_DIALOG_TITLE)]
    [RefreshProperties(RefreshProperties.All)]
    public string OpenFileDialogTitle
    {
        get => _openFileDialogTitle;
        set => SetString(ref _openFileDialogTitle, value, DEFAULT_OPEN_FILE_DIALOG_TITLE);
    }

    [Localizable(true)]
    [Category(@"Dialogs")]
    [Description(@"Format for the browse/open file dialog filter. {0} is replaced with the extension filter list.")]
    [DefaultValue(DEFAULT_OPEN_FILE_DIALOG_FILTER_FORMAT)]
    [RefreshProperties(RefreshProperties.All)]
    public string OpenFileDialogFilterFormat
    {
        get => _openFileDialogFilterFormat;
        set => SetString(ref _openFileDialogFilterFormat, value, DEFAULT_OPEN_FILE_DIALOG_FILTER_FORMAT);
    }

    [Localizable(true)]
    [Category(@"Dialogs")]
    [Description(@"Title for error message boxes.")]
    [DefaultValue(DEFAULT_ERROR_DIALOG_TITLE)]
    [RefreshProperties(RefreshProperties.All)]
    public string ErrorDialogTitle
    {
        get => _errorDialogTitle;
        set => SetString(ref _errorDialogTitle, value, DEFAULT_ERROR_DIALOG_TITLE);
    }

    #endregion

    #region Messages

    [Localizable(true)]
    [Category(@"Messages")]
    [Description(@"Message used when custom validation rejects a file without a custom message.")]
    [DefaultValue(DEFAULT_CUSTOM_VALIDATION_REJECTED)]
    [RefreshProperties(RefreshProperties.All)]
    public string CustomValidationRejected
    {
        get => _customValidationRejected;
        set => SetString(ref _customValidationRejected, value, DEFAULT_CUSTOM_VALIDATION_REJECTED);
    }

    [Localizable(true)]
    [Category(@"Messages")]
    [Description(@"Format for disallowed extension messages. {0} is the extension.")]
    [DefaultValue(DEFAULT_EXTENSION_NOT_ALLOWED_FORMAT)]
    [RefreshProperties(RefreshProperties.All)]
    public string ExtensionNotAllowedFormat
    {
        get => _extensionNotAllowedFormat;
        set => SetString(ref _extensionNotAllowedFormat, value, DEFAULT_EXTENSION_NOT_ALLOWED_FORMAT);
    }

    [Localizable(true)]
    [Category(@"Messages")]
    [Description(@"Format for maximum file size messages. {0} is the formatted maximum size.")]
    [DefaultValue(DEFAULT_FILE_EXCEEDS_MAX_SIZE_FORMAT)]
    [RefreshProperties(RefreshProperties.All)]
    public string FileExceedsMaxSizeFormat
    {
        get => _fileExceedsMaxSizeFormat;
        set => SetString(ref _fileExceedsMaxSizeFormat, value, DEFAULT_FILE_EXCEEDS_MAX_SIZE_FORMAT);
    }

    [Localizable(true)]
    [Category(@"Messages")]
    [Description(@"Format for file inspection failure messages. {0} is the exception message.")]
    [DefaultValue(DEFAULT_UNABLE_TO_INSPECT_FILE_FORMAT)]
    [RefreshProperties(RefreshProperties.All)]
    public string UnableToInspectFileFormat
    {
        get => _unableToInspectFileFormat;
        set => SetString(ref _unableToInspectFileFormat, value, DEFAULT_UNABLE_TO_INSPECT_FILE_FORMAT);
    }

    [Localizable(true)]
    [Category(@"Messages")]
    [Description(@"Format for maximum file count messages. {0} is the maximum count.")]
    [DefaultValue(DEFAULT_EXCEEDS_MAX_FILE_COUNT_FORMAT)]
    [RefreshProperties(RefreshProperties.All)]
    public string ExceedsMaxFileCountFormat
    {
        get => _exceedsMaxFileCountFormat;
        set => SetString(ref _exceedsMaxFileCountFormat, value, DEFAULT_EXCEEDS_MAX_FILE_COUNT_FORMAT);
    }

    [Localizable(true)]
    [Category(@"Messages")]
    [Description(@"Format for invalid-file tooltips. {0} is the file path, {1} is the new-line separator, {2} is the rejection reason.")]
    [DefaultValue(DEFAULT_SKIPPED_FILE_TOOL_TIP_FORMAT)]
    [RefreshProperties(RefreshProperties.All)]
    public string SkippedFileToolTipFormat
    {
        get => _skippedFileToolTipFormat;
        set => SetString(ref _skippedFileToolTipFormat, value, DEFAULT_SKIPPED_FILE_TOOL_TIP_FORMAT);
    }

    [Localizable(true)]
    [Category(@"Messages")]
    [Description(@"New-line separator used in skipped-file tooltips.")]
    [DefaultValue(DEFAULT_SKIPPED_FILE_TOOL_TIP_NEW_LINE)]
    [RefreshProperties(RefreshProperties.All)]
    public string SkippedFileToolTipNewLine
    {
        get => _skippedFileToolTipNewLine;
        set => SetString(ref _skippedFileToolTipNewLine, value, DEFAULT_SKIPPED_FILE_TOOL_TIP_NEW_LINE);
    }

    [Localizable(true)]
    [Category(@"Messages")]
    [Description(@"Format for unable-to-open-file messages. {0} is the exception message.")]
    [DefaultValue(DEFAULT_UNABLE_TO_OPEN_FILE_FORMAT)]
    [RefreshProperties(RefreshProperties.All)]
    public string UnableToOpenFileFormat
    {
        get => _unableToOpenFileFormat;
        set => SetString(ref _unableToOpenFileFormat, value, DEFAULT_UNABLE_TO_OPEN_FILE_FORMAT);
    }

    [Localizable(true)]
    [Category(@"Messages")]
    [Description(@"Format for unable-to-open-folder messages. {0} is the exception message.")]
    [DefaultValue(DEFAULT_UNABLE_TO_OPEN_FOLDER_FORMAT)]
    [RefreshProperties(RefreshProperties.All)]
    public string UnableToOpenFolderFormat
    {
        get => _unableToOpenFolderFormat;
        set => SetString(ref _unableToOpenFolderFormat, value, DEFAULT_UNABLE_TO_OPEN_FOLDER_FORMAT);
    }

    #endregion

    #region Status

    [Localizable(true)]
    [Category(@"Status")]
    [Description(@"Status text when no files have been added.")]
    [DefaultValue(DEFAULT_STATUS_EMPTY)]
    [RefreshProperties(RefreshProperties.All)]
    public string StatusEmpty
    {
        get => _statusEmpty;
        set => SetString(ref _statusEmpty, value, DEFAULT_STATUS_EMPTY);
    }

    [Localizable(true)]
    [Category(@"Status")]
    [Description(@"Status format when invalid files are present. {0}=count, {1}=valid, {2}=skipped, {3}=total size.")]
    [DefaultValue(DEFAULT_STATUS_WITH_INVALID_FORMAT)]
    [RefreshProperties(RefreshProperties.All)]
    public string StatusWithInvalidFormat
    {
        get => _statusWithInvalidFormat;
        set => SetString(ref _statusWithInvalidFormat, value, DEFAULT_STATUS_WITH_INVALID_FORMAT);
    }

    [Localizable(true)]
    [Category(@"Status")]
    [Description(@"Status format when all files are valid. {0}=count, {1}=total size.")]
    [DefaultValue(DEFAULT_STATUS_FORMAT)]
    [RefreshProperties(RefreshProperties.All)]
    public string StatusFormat
    {
        get => _statusFormat;
        set => SetString(ref _statusFormat, value, DEFAULT_STATUS_FORMAT);
    }

    #endregion

    #region File Size Units

    [Localizable(true)]
    [Category(@"File Size Units")]
    [Description(@"Unit label for bytes.")]
    [DefaultValue(DEFAULT_BYTE_UNIT)]
    [RefreshProperties(RefreshProperties.All)]
    public string ByteUnit
    {
        get => _byteUnit;
        set => SetString(ref _byteUnit, value, DEFAULT_BYTE_UNIT);
    }

    [Localizable(true)]
    [Category(@"File Size Units")]
    [Description(@"Unit label for kilobytes.")]
    [DefaultValue(DEFAULT_KILOBYTE_UNIT)]
    [RefreshProperties(RefreshProperties.All)]
    public string KilobyteUnit
    {
        get => _kilobyteUnit;
        set => SetString(ref _kilobyteUnit, value, DEFAULT_KILOBYTE_UNIT);
    }

    [Localizable(true)]
    [Category(@"File Size Units")]
    [Description(@"Unit label for megabytes.")]
    [DefaultValue(DEFAULT_MEGABYTE_UNIT)]
    [RefreshProperties(RefreshProperties.All)]
    public string MegabyteUnit
    {
        get => _megabyteUnit;
        set => SetString(ref _megabyteUnit, value, DEFAULT_MEGABYTE_UNIT);
    }

    [Localizable(true)]
    [Category(@"File Size Units")]
    [Description(@"Unit label for gigabytes.")]
    [DefaultValue(DEFAULT_GIGABYTE_UNIT)]
    [RefreshProperties(RefreshProperties.All)]
    public string GigabyteUnit
    {
        get => _gigabyteUnit;
        set => SetString(ref _gigabyteUnit, value, DEFAULT_GIGABYTE_UNIT);
    }

    [Localizable(true)]
    [Category(@"File Size Units")]
    [Description(@"Unit label for terabytes.")]
    [DefaultValue(DEFAULT_TERABYTE_UNIT)]
    [RefreshProperties(RefreshProperties.All)]
    public string TerabyteUnit
    {
        get => _terabyteUnit;
        set => SetString(ref _terabyteUnit, value, DEFAULT_TERABYTE_UNIT);
    }

    [Localizable(true)]
    [Category(@"Status")]
    [Description(@"Upload quota summary format. {0}=used, {1}=quota, {2}=remaining.")]
    [DefaultValue(DEFAULT_UPLOAD_QUOTA_FORMAT)]
    [RefreshProperties(RefreshProperties.All)]
    public string UploadQuotaFormat
    {
        get => _uploadQuotaFormat;
        set => SetString(ref _uploadQuotaFormat, value, DEFAULT_UPLOAD_QUOTA_FORMAT);
    }

    [Localizable(true)]
    [Category(@"Messages")]
    [Description(@"Format when a file would exceed the combined upload size quota. {0}=remaining quota.")]
    [DefaultValue(DEFAULT_EXCEEDS_UPLOAD_QUOTA_FORMAT)]
    [RefreshProperties(RefreshProperties.All)]
    public string ExceedsUploadQuotaFormat
    {
        get => _exceedsUploadQuotaFormat;
        set => SetString(ref _exceedsUploadQuotaFormat, value, DEFAULT_EXCEEDS_UPLOAD_QUOTA_FORMAT);
    }

    [Localizable(true)]
    [Category(@"Accessibility")]
    [Description(@"Accessible name for the upload quota progress bar.")]
    [DefaultValue(DEFAULT_UPLOAD_QUOTA_ACCESSIBLE_NAME)]
    [RefreshProperties(RefreshProperties.All)]
    public string UploadQuotaAccessibleName
    {
        get => _uploadQuotaAccessibleName;
        set => SetString(ref _uploadQuotaAccessibleName, value, DEFAULT_UPLOAD_QUOTA_ACCESSIBLE_NAME);
    }

    [Localizable(true)]
    [Category(@"Accessibility")]
    [Description(@"Accessible description for the upload quota progress bar.")]
    [DefaultValue(DEFAULT_UPLOAD_QUOTA_ACCESSIBLE_DESCRIPTION)]
    [RefreshProperties(RefreshProperties.All)]
    public string UploadQuotaAccessibleDescription
    {
        get => _uploadQuotaAccessibleDescription;
        set => SetString(ref _uploadQuotaAccessibleDescription, value, DEFAULT_UPLOAD_QUOTA_ACCESSIBLE_DESCRIPTION);
    }

    #endregion

    #region Implementation

    /// <summary>Resets all strings to their default values.</summary>
    public void Reset()
    {
        DropZoneText = DEFAULT_DROP_ZONE_TEXT;
        FileNameColumn = DEFAULT_FILE_NAME_COLUMN;
        ClearAllButton = DEFAULT_CLEAR_ALL_BUTTON;
        BrowseButton = DEFAULT_BROWSE_BUTTON;
        FileListAccessibleName = DEFAULT_FILE_LIST_ACCESSIBLE_NAME;
        FileListAccessibleDescription = DEFAULT_FILE_LIST_ACCESSIBLE_DESCRIPTION;
        BrowseAccessibleName = DEFAULT_BROWSE_ACCESSIBLE_NAME;
        BrowseAccessibleDescription = DEFAULT_BROWSE_ACCESSIBLE_DESCRIPTION;
        DropZoneAccessibleName = DEFAULT_DROP_ZONE_ACCESSIBLE_NAME;
        DropZoneAccessibleDescription = DEFAULT_DROP_ZONE_ACCESSIBLE_DESCRIPTION;
        MenuOpen = DEFAULT_MENU_OPEN;
        MenuRemove = DEFAULT_MENU_REMOVE;
        MenuOpenFolder = DEFAULT_MENU_OPEN_FOLDER;
        MenuCopyPaths = DEFAULT_MENU_COPY_PATHS;
        MenuSortBy = DEFAULT_MENU_SORT_BY;
        MenuSortName = DEFAULT_MENU_SORT_NAME;
        MenuSortSize = DEFAULT_MENU_SORT_SIZE;
        MenuSortDate = DEFAULT_MENU_SORT_DATE;
        MenuSortExtension = DEFAULT_MENU_SORT_EXTENSION;
        MenuUndo = DEFAULT_MENU_UNDO;
        MenuClear = DEFAULT_MENU_CLEAR;
        ShortcutDelete = DEFAULT_SHORTCUT_DELETE;
        ShortcutCopy = DEFAULT_SHORTCUT_COPY;
        ShortcutUndo = DEFAULT_SHORTCUT_UNDO;
        OpenFileDialogTitle = DEFAULT_OPEN_FILE_DIALOG_TITLE;
        OpenFileDialogFilterFormat = DEFAULT_OPEN_FILE_DIALOG_FILTER_FORMAT;
        CustomValidationRejected = DEFAULT_CUSTOM_VALIDATION_REJECTED;
        ExtensionNotAllowedFormat = DEFAULT_EXTENSION_NOT_ALLOWED_FORMAT;
        FileExceedsMaxSizeFormat = DEFAULT_FILE_EXCEEDS_MAX_SIZE_FORMAT;
        UnableToInspectFileFormat = DEFAULT_UNABLE_TO_INSPECT_FILE_FORMAT;
        ExceedsMaxFileCountFormat = DEFAULT_EXCEEDS_MAX_FILE_COUNT_FORMAT;
        SkippedFileToolTipFormat = DEFAULT_SKIPPED_FILE_TOOL_TIP_FORMAT;
        SkippedFileToolTipNewLine = DEFAULT_SKIPPED_FILE_TOOL_TIP_NEW_LINE;
        StatusEmpty = DEFAULT_STATUS_EMPTY;
        StatusWithInvalidFormat = DEFAULT_STATUS_WITH_INVALID_FORMAT;
        StatusFormat = DEFAULT_STATUS_FORMAT;
        UnableToOpenFileFormat = DEFAULT_UNABLE_TO_OPEN_FILE_FORMAT;
        UnableToOpenFolderFormat = DEFAULT_UNABLE_TO_OPEN_FOLDER_FORMAT;
        ErrorDialogTitle = DEFAULT_ERROR_DIALOG_TITLE;
        ByteUnit = DEFAULT_BYTE_UNIT;
        KilobyteUnit = DEFAULT_KILOBYTE_UNIT;
        MegabyteUnit = DEFAULT_MEGABYTE_UNIT;
        GigabyteUnit = DEFAULT_GIGABYTE_UNIT;
        TerabyteUnit = DEFAULT_TERABYTE_UNIT;
        UploadQuotaFormat = DEFAULT_UPLOAD_QUOTA_FORMAT;
        ExceedsUploadQuotaFormat = DEFAULT_EXCEEDS_UPLOAD_QUOTA_FORMAT;
        UploadQuotaAccessibleName = DEFAULT_UPLOAD_QUOTA_ACCESSIBLE_NAME;
        UploadQuotaAccessibleDescription = DEFAULT_UPLOAD_QUOTA_ACCESSIBLE_DESCRIPTION;
        HeaderText = DEFAULT_HEADER_TEXT;
        PreviewHeader = DEFAULT_PREVIEW_HEADER;
        CancelButton = DEFAULT_CANCEL_BUTTON;
        SubmitButton = DEFAULT_SUBMIT_BUTTON;
    }

    internal string[] GetFileSizeUnits() => [_byteUnit, _kilobyteUnit, _megabyteUnit, _gigabyteUnit, _terabyteUnit];

    internal void SetOwner(KryptonDropZone owner) => _owner = owner;

    private void SetString(ref string field, string? value, string fallback)
    {
        value ??= fallback;
        if (field == value)
        {
            return;
        }

        field = value;
        _owner?.OnStringsChanged();
    }

    #endregion
}

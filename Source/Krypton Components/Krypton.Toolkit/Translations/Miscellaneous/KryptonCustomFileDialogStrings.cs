#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Localizable strings for the custom (managed) file and folder dialogs.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonCustomFileDialogStrings : GlobalId
{
    #region Static Fields

    private const string DEFAULT_FILE_NAME_CUE_TEXT = @"Enter a file name";
    private const string DEFAULT_FOLDER_PATH_CUE_TEXT = @"Selected folder path";
    private const string DEFAULT_ADDRESS_PATH_CUE_TEXT = @"Type, paste, or edit a folder path";
    private const string DEFAULT_SEARCH_CUE_TEXT = @"Search current folder";
    private const string DEFAULT_COPY_ADDRESS = @"Copy Address";
    private const string DEFAULT_COPY_ADDRESS_AS_TEXT = @"Copy Address as Text";
    private const string DEFAULT_EDIT_ADDRESS = @"Edit Address";
    private const string DEFAULT_DELETE_HISTORY = @"Delete History";
    private const string DEFAULT_DATE_MODIFIED = @"Date modified";
    private const string DEFAULT_DATE_MODIFIED_ANY_TIME = @"Any time";
    private const string DEFAULT_DATE_MODIFIED_TODAY = @"Today";
    private const string DEFAULT_DATE_MODIFIED_YESTERDAY = @"Yesterday";
    private const string DEFAULT_DATE_MODIFIED_THIS_WEEK = @"This week";
    private const string DEFAULT_DATE_MODIFIED_LAST_WEEK = @"Last week";
    private const string DEFAULT_DATE_MODIFIED_THIS_MONTH = @"This month";
    private const string DEFAULT_DATE_MODIFIED_LAST_MONTH = @"Last month";
    private const string DEFAULT_DATE_MODIFIED_THIS_YEAR = @"This year";
    private const string DEFAULT_DATE_MODIFIED_LAST_YEAR = @"Last year";
    private const string DEFAULT_OPEN = @"Open";
    private const string DEFAULT_SAVE = @"Save";
    private const string DEFAULT_SELECT_FOLDER = @"Select Folder";
    private const string DEFAULT_REFRESH = @"Refresh";
    private const string DEFAULT_BACK = @"Back";
    private const string DEFAULT_FORWARD = @"Forward";
    private const string DEFAULT_UP = @"Up";
    private const string DEFAULT_FILE_NAME_LABEL = @"File name:";
    private const string DEFAULT_FOLDER_LABEL = @"Folder:";
    private const string DEFAULT_FILTER_LABEL = @"Filter:";
    private const string DEFAULT_SEARCH_LABEL = @"Search:";
    private const string DEFAULT_VIEW_PREFIX = @"View: {0}";
    private const string DEFAULT_VIEW_DETAILS = @"Details";
    private const string DEFAULT_VIEW_LARGE_ICONS = @"Large icons";
    private const string DEFAULT_VIEW_SMALL_ICONS = @"Small icons";
    private const string DEFAULT_VIEW_LIST = @"List";
    private const string DEFAULT_VIEW_TILES = @"Tiles";
    private const string DEFAULT_COMMON_PLACES = @"Common Places";
    private const string DEFAULT_CUSTOM_PLACES = @"Custom Places";
    private const string DEFAULT_DRIVES = @"Drives";
    private const string DEFAULT_DESKTOP = @"Desktop";
    private const string DEFAULT_DOCUMENTS = @"Documents";
    private const string DEFAULT_PICTURES = @"Pictures";
    private const string DEFAULT_MUSIC = @"Music";
    private const string DEFAULT_DOWNLOADS = @"Downloads";
    private const string DEFAULT_COLUMN_NAME = @"Name";
    private const string DEFAULT_COLUMN_TYPE = @"Type";
    private const string DEFAULT_COLUMN_MODIFIED = @"Modified";
    private const string DEFAULT_COLUMN_SIZE = @"Size";
    private const string DEFAULT_FOLDER_TYPE = @"Folder";
    private const string DEFAULT_FILE_TYPE = @"{0} File";
    private const string DEFAULT_GENERIC_FILE_TYPE = @"File";
    private const string DEFAULT_ALL_FILES_FILTER = @"All files (*.*)";
    private const string DEFAULT_LOADING = @"Loading...";
    private const string DEFAULT_LOADING_PATH = @"Loading {0}...";
    private const string DEFAULT_ITEMS_IN_PATH = @"{0} item(s) in {1}";
    private const string DEFAULT_MATCHING_ITEMS_IN_PATH = @"{0} matching item(s) in {1}";
    private const string DEFAULT_PATH_DOES_NOT_EXIST = @"'{0}' does not exist.";
    private const string DEFAULT_UNABLE_TO_LOAD_DIRECTORY = @"Unable to load directory.";
    private const string DEFAULT_INVALID_FOLDER = @"'{0}' is not a valid folder.";
    private const string DEFAULT_ENTER_FILE_NAME = @"Enter a file name.";
    private const string DEFAULT_INVALID_FILE_NAME_CHARS = @"The file name contains invalid characters.";
    private const string DEFAULT_CONFIRM_SAVE_AS_CAPTION = @"Confirm Save As";
    private const string DEFAULT_CONFIRM_SAVE_AS_TEXT = @"'{0}' already exists. Do you want to replace it?";
    private const string DEFAULT_CONFIRM_CREATE_CAPTION = @"Confirm Create";
    private const string DEFAULT_CONFIRM_CREATE_TEXT = @"Create '{0}'?";
    private const string DEFAULT_VALIDATION_CAPTION = @"File Dialog";

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonCustomFileDialogStrings"/> class.</summary>
    public KryptonCustomFileDialogStrings() => Reset();

    #endregion

    #region Public - cues and address menu

    /// <summary>Gets or sets the cue text shown in the file name box when it is empty.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Cue text for the custom dialog file name box when empty. Empty string disables the cue.")]
    [DefaultValue(DEFAULT_FILE_NAME_CUE_TEXT)]
    public string FileNameCueText { get; set; }

    /// <summary>Gets or sets the cue text shown in the folder path box when it is empty (folder picker).</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Cue text for the custom folder dialog path box when empty. Empty string disables the cue.")]
    [DefaultValue(DEFAULT_FOLDER_PATH_CUE_TEXT)]
    public string FolderPathCueText { get; set; }

    /// <summary>Gets or sets the cue text shown in the editable address path box when it is empty.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Cue text for the custom dialog editable address path box when empty. Empty string disables the cue.")]
    [DefaultValue(DEFAULT_ADDRESS_PATH_CUE_TEXT)]
    public string AddressPathCueText { get; set; }

    /// <summary>Gets or sets the cue text shown in the search box when it is empty.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Cue text for the custom dialog search box when empty. Empty string disables the cue.")]
    [DefaultValue(DEFAULT_SEARCH_CUE_TEXT)]
    public string SearchCueText { get; set; }

    /// <summary>Gets or sets the breadcrumb context-menu text for copying the quoted address.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Breadcrumb context-menu text for copying the quoted address.")]
    [DefaultValue(DEFAULT_COPY_ADDRESS)]
    public string CopyAddress { get; set; }

    /// <summary>Gets or sets the breadcrumb context-menu text for copying the address as plain text.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Breadcrumb context-menu text for copying the address as plain text.")]
    [DefaultValue(DEFAULT_COPY_ADDRESS_AS_TEXT)]
    public string CopyAddressAsText { get; set; }

    /// <summary>Gets or sets the breadcrumb context-menu text for editing the address.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Breadcrumb context-menu text for editing the address.")]
    [DefaultValue(DEFAULT_EDIT_ADDRESS)]
    public string EditAddress { get; set; }

    /// <summary>Gets or sets the breadcrumb context-menu text for clearing navigation history.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Breadcrumb context-menu text for clearing navigation history.")]
    [DefaultValue(DEFAULT_DELETE_HISTORY)]
    public string DeleteHistory { get; set; }

    #endregion

    #region Public - date modified

    /// <summary>Gets or sets the accessible / tooltip caption for the date-modified filter.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Accessible name and tooltip for the date-modified search filter.")]
    [DefaultValue(DEFAULT_DATE_MODIFIED)]
    public string DateModified { get; set; }

    /// <summary>Gets or sets the date-modified filter option for any time.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Date-modified filter option: any time.")]
    [DefaultValue(DEFAULT_DATE_MODIFIED_ANY_TIME)]
    public string DateModifiedAnyTime { get; set; }

    /// <summary>Gets or sets the date-modified filter option for today.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Date-modified filter option: today.")]
    [DefaultValue(DEFAULT_DATE_MODIFIED_TODAY)]
    public string DateModifiedToday { get; set; }

    /// <summary>Gets or sets the date-modified filter option for yesterday.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Date-modified filter option: yesterday.")]
    [DefaultValue(DEFAULT_DATE_MODIFIED_YESTERDAY)]
    public string DateModifiedYesterday { get; set; }

    /// <summary>Gets or sets the date-modified filter option for this week.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Date-modified filter option: this week.")]
    [DefaultValue(DEFAULT_DATE_MODIFIED_THIS_WEEK)]
    public string DateModifiedThisWeek { get; set; }

    /// <summary>Gets or sets the date-modified filter option for last week.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Date-modified filter option: last week.")]
    [DefaultValue(DEFAULT_DATE_MODIFIED_LAST_WEEK)]
    public string DateModifiedLastWeek { get; set; }

    /// <summary>Gets or sets the date-modified filter option for this month.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Date-modified filter option: this month.")]
    [DefaultValue(DEFAULT_DATE_MODIFIED_THIS_MONTH)]
    public string DateModifiedThisMonth { get; set; }

    /// <summary>Gets or sets the date-modified filter option for last month.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Date-modified filter option: last month.")]
    [DefaultValue(DEFAULT_DATE_MODIFIED_LAST_MONTH)]
    public string DateModifiedLastMonth { get; set; }

    /// <summary>Gets or sets the date-modified filter option for this year.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Date-modified filter option: this year.")]
    [DefaultValue(DEFAULT_DATE_MODIFIED_THIS_YEAR)]
    public string DateModifiedThisYear { get; set; }

    /// <summary>Gets or sets the date-modified filter option for last year.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Date-modified filter option: last year.")]
    [DefaultValue(DEFAULT_DATE_MODIFIED_LAST_YEAR)]
    public string DateModifiedLastYear { get; set; }

    #endregion

    #region Public - chrome and labels

    /// <summary>Gets or sets the default Open dialog title and accept button text.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Default Open dialog title and accept button text.")]
    [DefaultValue(DEFAULT_OPEN)]
    public string Open { get; set; }

    /// <summary>Gets or sets the default Save dialog title and accept button text.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Default Save dialog title and accept button text.")]
    [DefaultValue(DEFAULT_SAVE)]
    public string Save { get; set; }

    /// <summary>Gets or sets the default Select Folder dialog title and accept button text.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Default Select Folder dialog title and accept button text.")]
    [DefaultValue(DEFAULT_SELECT_FOLDER)]
    public string SelectFolder { get; set; }

    /// <summary>Gets or sets the Refresh button text.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Refresh button text.")]
    [DefaultValue(DEFAULT_REFRESH)]
    public string Refresh { get; set; }

    /// <summary>Gets or sets the Back button tooltip / accessible name.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Back button tooltip and accessible name.")]
    [DefaultValue(DEFAULT_BACK)]
    public string Back { get; set; }

    /// <summary>Gets or sets the Forward button tooltip / accessible name.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Forward button tooltip and accessible name.")]
    [DefaultValue(DEFAULT_FORWARD)]
    public string Forward { get; set; }

    /// <summary>Gets or sets the Up button tooltip / accessible name.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Up button tooltip and accessible name.")]
    [DefaultValue(DEFAULT_UP)]
    public string Up { get; set; }

    /// <summary>Gets or sets the File name label text.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"File name label text.")]
    [DefaultValue(DEFAULT_FILE_NAME_LABEL)]
    public string FileNameLabel { get; set; }

    /// <summary>Gets or sets the Folder label text (folder picker).</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Folder label text for the folder picker.")]
    [DefaultValue(DEFAULT_FOLDER_LABEL)]
    public string FolderLabel { get; set; }

    /// <summary>Gets or sets the Filter label text.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Filter label text.")]
    [DefaultValue(DEFAULT_FILTER_LABEL)]
    public string FilterLabel { get; set; }

    /// <summary>Gets or sets the Search label text.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Search label text.")]
    [DefaultValue(DEFAULT_SEARCH_LABEL)]
    public string SearchLabel { get; set; }

    #endregion

    #region Public - view modes

    /// <summary>Gets or sets the view-button tooltip format. Use {0} for the view name.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"View button tooltip format. Use {0} for the view name.")]
    [DefaultValue(DEFAULT_VIEW_PREFIX)]
    public string ViewPrefix { get; set; }

    /// <summary>Gets or sets the Details view mode name.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Details view mode name.")]
    [DefaultValue(DEFAULT_VIEW_DETAILS)]
    public string ViewDetails { get; set; }

    /// <summary>Gets or sets the Large icons view mode name.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Large icons view mode name.")]
    [DefaultValue(DEFAULT_VIEW_LARGE_ICONS)]
    public string ViewLargeIcons { get; set; }

    /// <summary>Gets or sets the Small icons view mode name.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Small icons view mode name.")]
    [DefaultValue(DEFAULT_VIEW_SMALL_ICONS)]
    public string ViewSmallIcons { get; set; }

    /// <summary>Gets or sets the List view mode name.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"List view mode name.")]
    [DefaultValue(DEFAULT_VIEW_LIST)]
    public string ViewList { get; set; }

    /// <summary>Gets or sets the Tiles view mode name.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Tiles view mode name.")]
    [DefaultValue(DEFAULT_VIEW_TILES)]
    public string ViewTiles { get; set; }

    #endregion

    #region Public - navigation tree

    /// <summary>Gets or sets the Common Places tree root text.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Common Places tree root text.")]
    [DefaultValue(DEFAULT_COMMON_PLACES)]
    public string CommonPlaces { get; set; }

    /// <summary>Gets or sets the Custom Places tree root text.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Custom Places tree root text.")]
    [DefaultValue(DEFAULT_CUSTOM_PLACES)]
    public string CustomPlaces { get; set; }

    /// <summary>Gets or sets the Drives tree root text.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Drives tree root text.")]
    [DefaultValue(DEFAULT_DRIVES)]
    public string Drives { get; set; }

    /// <summary>Gets or sets the Desktop place name.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Desktop place name.")]
    [DefaultValue(DEFAULT_DESKTOP)]
    public string Desktop { get; set; }

    /// <summary>Gets or sets the Documents place name.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Documents place name.")]
    [DefaultValue(DEFAULT_DOCUMENTS)]
    public string Documents { get; set; }

    /// <summary>Gets or sets the Pictures place name.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Pictures place name.")]
    [DefaultValue(DEFAULT_PICTURES)]
    public string Pictures { get; set; }

    /// <summary>Gets or sets the Music place name.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Music place name.")]
    [DefaultValue(DEFAULT_MUSIC)]
    public string Music { get; set; }

    /// <summary>Gets or sets the Downloads place name.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Downloads place name.")]
    [DefaultValue(DEFAULT_DOWNLOADS)]
    public string Downloads { get; set; }

    #endregion

    #region Public - list and filters

    /// <summary>Gets or sets the Name column header.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Name column header.")]
    [DefaultValue(DEFAULT_COLUMN_NAME)]
    public string ColumnName { get; set; }

    /// <summary>Gets or sets the Type column header.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Type column header.")]
    [DefaultValue(DEFAULT_COLUMN_TYPE)]
    public string ColumnType { get; set; }

    /// <summary>Gets or sets the Modified column header.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Modified column header.")]
    [DefaultValue(DEFAULT_COLUMN_MODIFIED)]
    public string ColumnModified { get; set; }

    /// <summary>Gets or sets the Size column header.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Size column header.")]
    [DefaultValue(DEFAULT_COLUMN_SIZE)]
    public string ColumnSize { get; set; }

    /// <summary>Gets or sets the type text used for folders in the list.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Type text used for folders in the list.")]
    [DefaultValue(DEFAULT_FOLDER_TYPE)]
    public string FolderType { get; set; }

    /// <summary>Gets or sets the typed file description format. Use {0} for the extension.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Typed file description format. Use {0} for the extension.")]
    [DefaultValue(DEFAULT_FILE_TYPE)]
    public string FileType { get; set; }

    /// <summary>Gets or sets the generic file type text when no extension is present.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Generic file type text when no extension is present.")]
    [DefaultValue(DEFAULT_GENERIC_FILE_TYPE)]
    public string GenericFileType { get; set; }

    /// <summary>Gets or sets the fallback All files filter display text.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Fallback All files filter display text.")]
    [DefaultValue(DEFAULT_ALL_FILES_FILTER)]
    public string AllFilesFilter { get; set; }

    #endregion

    #region Public - status and validation

    /// <summary>Gets or sets the initial loading status text.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Initial loading status text.")]
    [DefaultValue(DEFAULT_LOADING)]
    public string Loading { get; set; }

    /// <summary>Gets or sets the loading-path status format. Use {0} for the path.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Loading-path status format. Use {0} for the path.")]
    [DefaultValue(DEFAULT_LOADING_PATH)]
    public string LoadingPath { get; set; }

    /// <summary>Gets or sets the items-in-path status format. Use {0} for count and {1} for path.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Items-in-path status format. Use {0} for count and {1} for path.")]
    [DefaultValue(DEFAULT_ITEMS_IN_PATH)]
    public string ItemsInPath { get; set; }

    /// <summary>Gets or sets the matching-items status format. Use {0} for count and {1} for path.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Matching-items status format. Use {0} for count and {1} for path.")]
    [DefaultValue(DEFAULT_MATCHING_ITEMS_IN_PATH)]
    public string MatchingItemsInPath { get; set; }

    /// <summary>Gets or sets the path-does-not-exist message format. Use {0} for the path.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Path-does-not-exist message format. Use {0} for the path.")]
    [DefaultValue(DEFAULT_PATH_DOES_NOT_EXIST)]
    public string PathDoesNotExist { get; set; }

    /// <summary>Gets or sets the unable-to-load-directory status text.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Unable-to-load-directory status text.")]
    [DefaultValue(DEFAULT_UNABLE_TO_LOAD_DIRECTORY)]
    public string UnableToLoadDirectory { get; set; }

    /// <summary>Gets or sets the invalid-folder message format. Use {0} for the path.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Invalid-folder message format. Use {0} for the path.")]
    [DefaultValue(DEFAULT_INVALID_FOLDER)]
    public string InvalidFolder { get; set; }

    /// <summary>Gets or sets the enter-a-file-name validation message.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Enter-a-file-name validation message.")]
    [DefaultValue(DEFAULT_ENTER_FILE_NAME)]
    public string EnterFileName { get; set; }

    /// <summary>Gets or sets the invalid file-name characters validation message.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Invalid file-name characters validation message.")]
    [DefaultValue(DEFAULT_INVALID_FILE_NAME_CHARS)]
    public string InvalidFileNameCharacters { get; set; }

    /// <summary>Gets or sets the Confirm Save As message-box caption.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Confirm Save As message-box caption.")]
    [DefaultValue(DEFAULT_CONFIRM_SAVE_AS_CAPTION)]
    public string ConfirmSaveAsCaption { get; set; }

    /// <summary>Gets or sets the overwrite confirmation text. Use {0} for the path.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Overwrite confirmation text. Use {0} for the path.")]
    [DefaultValue(DEFAULT_CONFIRM_SAVE_AS_TEXT)]
    public string ConfirmSaveAsText { get; set; }

    /// <summary>Gets or sets the Confirm Create message-box caption.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Confirm Create message-box caption.")]
    [DefaultValue(DEFAULT_CONFIRM_CREATE_CAPTION)]
    public string ConfirmCreateCaption { get; set; }

    /// <summary>Gets or sets the create-file confirmation text. Use {0} for the path.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Create-file confirmation text. Use {0} for the path.")]
    [DefaultValue(DEFAULT_CONFIRM_CREATE_TEXT)]
    public string ConfirmCreateText { get; set; }

    /// <summary>Gets or sets the validation message-box caption.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Validation message-box caption.")]
    [DefaultValue(DEFAULT_VALIDATION_CAPTION)]
    public string ValidationCaption { get; set; }

    #endregion

    #region IsDefault

    /// <summary>Gets a value indicating whether this instance has default values.</summary>
    [Browsable(false)]
    public bool IsDefault =>
        FileNameCueText.Equals(DEFAULT_FILE_NAME_CUE_TEXT) &&
        FolderPathCueText.Equals(DEFAULT_FOLDER_PATH_CUE_TEXT) &&
        AddressPathCueText.Equals(DEFAULT_ADDRESS_PATH_CUE_TEXT) &&
        SearchCueText.Equals(DEFAULT_SEARCH_CUE_TEXT) &&
        CopyAddress.Equals(DEFAULT_COPY_ADDRESS) &&
        CopyAddressAsText.Equals(DEFAULT_COPY_ADDRESS_AS_TEXT) &&
        EditAddress.Equals(DEFAULT_EDIT_ADDRESS) &&
        DeleteHistory.Equals(DEFAULT_DELETE_HISTORY) &&
        DateModified.Equals(DEFAULT_DATE_MODIFIED) &&
        DateModifiedAnyTime.Equals(DEFAULT_DATE_MODIFIED_ANY_TIME) &&
        DateModifiedToday.Equals(DEFAULT_DATE_MODIFIED_TODAY) &&
        DateModifiedYesterday.Equals(DEFAULT_DATE_MODIFIED_YESTERDAY) &&
        DateModifiedThisWeek.Equals(DEFAULT_DATE_MODIFIED_THIS_WEEK) &&
        DateModifiedLastWeek.Equals(DEFAULT_DATE_MODIFIED_LAST_WEEK) &&
        DateModifiedThisMonth.Equals(DEFAULT_DATE_MODIFIED_THIS_MONTH) &&
        DateModifiedLastMonth.Equals(DEFAULT_DATE_MODIFIED_LAST_MONTH) &&
        DateModifiedThisYear.Equals(DEFAULT_DATE_MODIFIED_THIS_YEAR) &&
        DateModifiedLastYear.Equals(DEFAULT_DATE_MODIFIED_LAST_YEAR) &&
        Open.Equals(DEFAULT_OPEN) &&
        Save.Equals(DEFAULT_SAVE) &&
        SelectFolder.Equals(DEFAULT_SELECT_FOLDER) &&
        Refresh.Equals(DEFAULT_REFRESH) &&
        Back.Equals(DEFAULT_BACK) &&
        Forward.Equals(DEFAULT_FORWARD) &&
        Up.Equals(DEFAULT_UP) &&
        FileNameLabel.Equals(DEFAULT_FILE_NAME_LABEL) &&
        FolderLabel.Equals(DEFAULT_FOLDER_LABEL) &&
        FilterLabel.Equals(DEFAULT_FILTER_LABEL) &&
        SearchLabel.Equals(DEFAULT_SEARCH_LABEL) &&
        ViewPrefix.Equals(DEFAULT_VIEW_PREFIX) &&
        ViewDetails.Equals(DEFAULT_VIEW_DETAILS) &&
        ViewLargeIcons.Equals(DEFAULT_VIEW_LARGE_ICONS) &&
        ViewSmallIcons.Equals(DEFAULT_VIEW_SMALL_ICONS) &&
        ViewList.Equals(DEFAULT_VIEW_LIST) &&
        ViewTiles.Equals(DEFAULT_VIEW_TILES) &&
        CommonPlaces.Equals(DEFAULT_COMMON_PLACES) &&
        CustomPlaces.Equals(DEFAULT_CUSTOM_PLACES) &&
        Drives.Equals(DEFAULT_DRIVES) &&
        Desktop.Equals(DEFAULT_DESKTOP) &&
        Documents.Equals(DEFAULT_DOCUMENTS) &&
        Pictures.Equals(DEFAULT_PICTURES) &&
        Music.Equals(DEFAULT_MUSIC) &&
        Downloads.Equals(DEFAULT_DOWNLOADS) &&
        ColumnName.Equals(DEFAULT_COLUMN_NAME) &&
        ColumnType.Equals(DEFAULT_COLUMN_TYPE) &&
        ColumnModified.Equals(DEFAULT_COLUMN_MODIFIED) &&
        ColumnSize.Equals(DEFAULT_COLUMN_SIZE) &&
        FolderType.Equals(DEFAULT_FOLDER_TYPE) &&
        FileType.Equals(DEFAULT_FILE_TYPE) &&
        GenericFileType.Equals(DEFAULT_GENERIC_FILE_TYPE) &&
        AllFilesFilter.Equals(DEFAULT_ALL_FILES_FILTER) &&
        Loading.Equals(DEFAULT_LOADING) &&
        LoadingPath.Equals(DEFAULT_LOADING_PATH) &&
        ItemsInPath.Equals(DEFAULT_ITEMS_IN_PATH) &&
        MatchingItemsInPath.Equals(DEFAULT_MATCHING_ITEMS_IN_PATH) &&
        PathDoesNotExist.Equals(DEFAULT_PATH_DOES_NOT_EXIST) &&
        UnableToLoadDirectory.Equals(DEFAULT_UNABLE_TO_LOAD_DIRECTORY) &&
        InvalidFolder.Equals(DEFAULT_INVALID_FOLDER) &&
        EnterFileName.Equals(DEFAULT_ENTER_FILE_NAME) &&
        InvalidFileNameCharacters.Equals(DEFAULT_INVALID_FILE_NAME_CHARS) &&
        ConfirmSaveAsCaption.Equals(DEFAULT_CONFIRM_SAVE_AS_CAPTION) &&
        ConfirmSaveAsText.Equals(DEFAULT_CONFIRM_SAVE_AS_TEXT) &&
        ConfirmCreateCaption.Equals(DEFAULT_CONFIRM_CREATE_CAPTION) &&
        ConfirmCreateText.Equals(DEFAULT_CONFIRM_CREATE_TEXT) &&
        ValidationCaption.Equals(DEFAULT_VALIDATION_CAPTION);

    #endregion

    #region Public Methods

    /// <summary>Resets all strings to their default values.</summary>
    public void Reset()
    {
        FileNameCueText = DEFAULT_FILE_NAME_CUE_TEXT;
        FolderPathCueText = DEFAULT_FOLDER_PATH_CUE_TEXT;
        AddressPathCueText = DEFAULT_ADDRESS_PATH_CUE_TEXT;
        SearchCueText = DEFAULT_SEARCH_CUE_TEXT;
        CopyAddress = DEFAULT_COPY_ADDRESS;
        CopyAddressAsText = DEFAULT_COPY_ADDRESS_AS_TEXT;
        EditAddress = DEFAULT_EDIT_ADDRESS;
        DeleteHistory = DEFAULT_DELETE_HISTORY;
        DateModified = DEFAULT_DATE_MODIFIED;
        DateModifiedAnyTime = DEFAULT_DATE_MODIFIED_ANY_TIME;
        DateModifiedToday = DEFAULT_DATE_MODIFIED_TODAY;
        DateModifiedYesterday = DEFAULT_DATE_MODIFIED_YESTERDAY;
        DateModifiedThisWeek = DEFAULT_DATE_MODIFIED_THIS_WEEK;
        DateModifiedLastWeek = DEFAULT_DATE_MODIFIED_LAST_WEEK;
        DateModifiedThisMonth = DEFAULT_DATE_MODIFIED_THIS_MONTH;
        DateModifiedLastMonth = DEFAULT_DATE_MODIFIED_LAST_MONTH;
        DateModifiedThisYear = DEFAULT_DATE_MODIFIED_THIS_YEAR;
        DateModifiedLastYear = DEFAULT_DATE_MODIFIED_LAST_YEAR;
        Open = DEFAULT_OPEN;
        Save = DEFAULT_SAVE;
        SelectFolder = DEFAULT_SELECT_FOLDER;
        Refresh = DEFAULT_REFRESH;
        Back = DEFAULT_BACK;
        Forward = DEFAULT_FORWARD;
        Up = DEFAULT_UP;
        FileNameLabel = DEFAULT_FILE_NAME_LABEL;
        FolderLabel = DEFAULT_FOLDER_LABEL;
        FilterLabel = DEFAULT_FILTER_LABEL;
        SearchLabel = DEFAULT_SEARCH_LABEL;
        ViewPrefix = DEFAULT_VIEW_PREFIX;
        ViewDetails = DEFAULT_VIEW_DETAILS;
        ViewLargeIcons = DEFAULT_VIEW_LARGE_ICONS;
        ViewSmallIcons = DEFAULT_VIEW_SMALL_ICONS;
        ViewList = DEFAULT_VIEW_LIST;
        ViewTiles = DEFAULT_VIEW_TILES;
        CommonPlaces = DEFAULT_COMMON_PLACES;
        CustomPlaces = DEFAULT_CUSTOM_PLACES;
        Drives = DEFAULT_DRIVES;
        Desktop = DEFAULT_DESKTOP;
        Documents = DEFAULT_DOCUMENTS;
        Pictures = DEFAULT_PICTURES;
        Music = DEFAULT_MUSIC;
        Downloads = DEFAULT_DOWNLOADS;
        ColumnName = DEFAULT_COLUMN_NAME;
        ColumnType = DEFAULT_COLUMN_TYPE;
        ColumnModified = DEFAULT_COLUMN_MODIFIED;
        ColumnSize = DEFAULT_COLUMN_SIZE;
        FolderType = DEFAULT_FOLDER_TYPE;
        FileType = DEFAULT_FILE_TYPE;
        GenericFileType = DEFAULT_GENERIC_FILE_TYPE;
        AllFilesFilter = DEFAULT_ALL_FILES_FILTER;
        Loading = DEFAULT_LOADING;
        LoadingPath = DEFAULT_LOADING_PATH;
        ItemsInPath = DEFAULT_ITEMS_IN_PATH;
        MatchingItemsInPath = DEFAULT_MATCHING_ITEMS_IN_PATH;
        PathDoesNotExist = DEFAULT_PATH_DOES_NOT_EXIST;
        UnableToLoadDirectory = DEFAULT_UNABLE_TO_LOAD_DIRECTORY;
        InvalidFolder = DEFAULT_INVALID_FOLDER;
        EnterFileName = DEFAULT_ENTER_FILE_NAME;
        InvalidFileNameCharacters = DEFAULT_INVALID_FILE_NAME_CHARS;
        ConfirmSaveAsCaption = DEFAULT_CONFIRM_SAVE_AS_CAPTION;
        ConfirmSaveAsText = DEFAULT_CONFIRM_SAVE_AS_TEXT;
        ConfirmCreateCaption = DEFAULT_CONFIRM_CREATE_CAPTION;
        ConfirmCreateText = DEFAULT_CONFIRM_CREATE_TEXT;
        ValidationCaption = DEFAULT_VALIDATION_CAPTION;
    }

    #endregion

    #region Overrides

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : GlobalStaticVariables.DEFAULT_EMPTY_STRING;

    #endregion
}

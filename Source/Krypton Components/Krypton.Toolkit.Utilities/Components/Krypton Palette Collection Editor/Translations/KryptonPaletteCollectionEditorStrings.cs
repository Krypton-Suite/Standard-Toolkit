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
/// Localisable strings for <see cref="KryptonPaletteCollectionEditor"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonPaletteCollectionEditorStrings : GlobalId
{
    private const string DefaultWindowTitle = @"Krypton Palette Collection Editor";
    private const string DefaultInfo =
        @"Edit a multi-theme .ktheme collection. Browse or create a collection, then Add .kthemex (or .ktheme / .xml) files. Remove drops a named theme. The last theme cannot be removed; delete the file instead. Add/Remove save immediately.";
    private const string DefaultCollectionFileLabel = @"Collection file";
    private const string DefaultBrowse = @"Browse...";
    private const string DefaultCollectionNameLabel = @"Collection name";
    private const string DefaultSaveName = @"Save name";
    private const string DefaultThemesLabel = @"Themes in collection";
    private const string DefaultAdd = @"Add...";
    private const string DefaultRemove = @"Remove";
    private const string DefaultViewByLabel = @"View By:";
    private const string DefaultStatusChooseThenAdd = @"Choose a .ktheme collection, then add .kthemex files.";
    private const string DefaultOpenCollectionTitle = @"Open or create palette collection";
    private const string DefaultThemeContainerFilter = @"Krypton theme containers (*.ktheme)|*.ktheme";
    private const string DefaultAllFilesFilter = @"All files (*.*)|*.*";
    private const string DefaultAddThemeBeforeSavingName = @"Add a theme before saving the collection name.";
    private const string DefaultCollectionNameSavedFormat = @"Collection name saved as '{0}'.";
    private const string DefaultAddFilesTitle = @"Add palette files to collection";
    private const string DefaultAddFilesFilter = KryptonPaletteFile.DialogFilter;
    private const string DefaultNoThemesAdded = @"No themes were added.";
    private const string DefaultAddedFilesFormat = @"Added {0} file(s) to the collection.";
    private const string DefaultDuplicateThemeTitle = @"Duplicate theme name";
    private const string DefaultReplaceExistingTheme = @"Replace the existing theme?";
    private const string DefaultAddToCollectionTitle = @"Add to collection";
    private const string DefaultSelectThemeToRemove = @"Select a theme to remove.";
    private const string DefaultRemovedThemeFormat = @"Removed '{0}'.";
    private const string DefaultRemoveFromCollectionTitle = @"Remove from collection";
    private const string DefaultCollectionMissingAddToCreate =
        @"Collection file does not exist yet. Add a .kthemex file to create it.";
    private const string DefaultCollectionKindCollection = @"collection";
    private const string DefaultCollectionKindSingleTheme =
        @"single-theme .ktheme (will become a collection on add)";
    private const string DefaultThemeCountFormat = @"{0} theme(s) in {1}.";
    private const string DefaultOpenCollectionErrorTitle = @"Open collection";
    private const string DefaultChooseCollectionFirst = @"Choose a .ktheme collection first.";
    private const string DefaultCreateCollectionTitle = @"Create palette collection";
    private const string DefaultCollectionFileName = @"themes.ktheme";
    private const string DefaultSaveCollectionNameTitle = @"Save collection name";
    private const string DefaultViewLargeIcon = @"Large Icons";
    private const string DefaultViewDetails = @"Details";
    private const string DefaultViewSmallIcon = @"Small Icons";
    private const string DefaultViewList = @"List";
    private const string DefaultViewTile = @"Tile";

    /// <summary>Initializes a new instance of the <see cref="KryptonPaletteCollectionEditorStrings"/> class.</summary>
    public KryptonPaletteCollectionEditorStrings() => Reset();

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    /// <summary>Gets a value indicating whether all values are default.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsDefault =>
        WindowTitle == DefaultWindowTitle
        && Info == DefaultInfo
        && CollectionFileLabel == DefaultCollectionFileLabel
        && Browse == DefaultBrowse
        && CollectionNameLabel == DefaultCollectionNameLabel
        && SaveName == DefaultSaveName
        && ThemesLabel == DefaultThemesLabel
        && Add == DefaultAdd
        && Remove == DefaultRemove
        && ViewByLabel == DefaultViewByLabel
        && StatusChooseThenAdd == DefaultStatusChooseThenAdd
        && OpenCollectionTitle == DefaultOpenCollectionTitle
        && ThemeContainerFilter == DefaultThemeContainerFilter
        && AllFilesFilter == DefaultAllFilesFilter
        && AddThemeBeforeSavingName == DefaultAddThemeBeforeSavingName
        && CollectionNameSavedFormat == DefaultCollectionNameSavedFormat
        && AddFilesTitle == DefaultAddFilesTitle
        && AddFilesFilter == DefaultAddFilesFilter
        && NoThemesAdded == DefaultNoThemesAdded
        && AddedFilesFormat == DefaultAddedFilesFormat
        && DuplicateThemeTitle == DefaultDuplicateThemeTitle
        && ReplaceExistingTheme == DefaultReplaceExistingTheme
        && AddToCollectionTitle == DefaultAddToCollectionTitle
        && SelectThemeToRemove == DefaultSelectThemeToRemove
        && RemovedThemeFormat == DefaultRemovedThemeFormat
        && RemoveFromCollectionTitle == DefaultRemoveFromCollectionTitle
        && CollectionMissingAddToCreate == DefaultCollectionMissingAddToCreate
        && CollectionKindCollection == DefaultCollectionKindCollection
        && CollectionKindSingleTheme == DefaultCollectionKindSingleTheme
        && ThemeCountFormat == DefaultThemeCountFormat
        && OpenCollectionErrorTitle == DefaultOpenCollectionErrorTitle
        && ChooseCollectionFirst == DefaultChooseCollectionFirst
        && CreateCollectionTitle == DefaultCreateCollectionTitle
        && CollectionFileName == DefaultCollectionFileName
        && SaveCollectionNameTitle == DefaultSaveCollectionNameTitle
        && ViewLargeIcon == DefaultViewLargeIcon
        && ViewDetails == DefaultViewDetails
        && ViewSmallIcon == DefaultViewSmallIcon
        && ViewList == DefaultViewList
        && ViewTile == DefaultViewTile;

    /// <summary>Resets all strings to defaults.</summary>
    public void Reset()
    {
        WindowTitle = DefaultWindowTitle;
        Info = DefaultInfo;
        CollectionFileLabel = DefaultCollectionFileLabel;
        Browse = DefaultBrowse;
        CollectionNameLabel = DefaultCollectionNameLabel;
        SaveName = DefaultSaveName;
        ThemesLabel = DefaultThemesLabel;
        Add = DefaultAdd;
        Remove = DefaultRemove;
        ViewByLabel = DefaultViewByLabel;
        StatusChooseThenAdd = DefaultStatusChooseThenAdd;
        OpenCollectionTitle = DefaultOpenCollectionTitle;
        ThemeContainerFilter = DefaultThemeContainerFilter;
        AllFilesFilter = DefaultAllFilesFilter;
        AddThemeBeforeSavingName = DefaultAddThemeBeforeSavingName;
        CollectionNameSavedFormat = DefaultCollectionNameSavedFormat;
        AddFilesTitle = DefaultAddFilesTitle;
        AddFilesFilter = DefaultAddFilesFilter;
        NoThemesAdded = DefaultNoThemesAdded;
        AddedFilesFormat = DefaultAddedFilesFormat;
        DuplicateThemeTitle = DefaultDuplicateThemeTitle;
        ReplaceExistingTheme = DefaultReplaceExistingTheme;
        AddToCollectionTitle = DefaultAddToCollectionTitle;
        SelectThemeToRemove = DefaultSelectThemeToRemove;
        RemovedThemeFormat = DefaultRemovedThemeFormat;
        RemoveFromCollectionTitle = DefaultRemoveFromCollectionTitle;
        CollectionMissingAddToCreate = DefaultCollectionMissingAddToCreate;
        CollectionKindCollection = DefaultCollectionKindCollection;
        CollectionKindSingleTheme = DefaultCollectionKindSingleTheme;
        ThemeCountFormat = DefaultThemeCountFormat;
        OpenCollectionErrorTitle = DefaultOpenCollectionErrorTitle;
        ChooseCollectionFirst = DefaultChooseCollectionFirst;
        CreateCollectionTitle = DefaultCreateCollectionTitle;
        CollectionFileName = DefaultCollectionFileName;
        SaveCollectionNameTitle = DefaultSaveCollectionNameTitle;
        ViewLargeIcon = DefaultViewLargeIcon;
        ViewDetails = DefaultViewDetails;
        ViewSmallIcon = DefaultViewSmallIcon;
        ViewList = DefaultViewList;
        ViewTile = DefaultViewTile;
    }

    /// <summary>Gets the combined collection-file dialog filter.</summary>
    [Browsable(false)]
    public string ThemeContainerDialogFilter => ThemeContainerFilter + @"|" + AllFilesFilter;

    /// <summary>Gets or sets the dialog window title.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultWindowTitle)]
    public string WindowTitle { get; set; }

    /// <summary>Gets or sets the instruction text at the top of the dialog.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultInfo)]
    public string Info { get; set; }

    /// <summary>Gets or sets the collection file path label.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultCollectionFileLabel)]
    public string CollectionFileLabel { get; set; }

    /// <summary>Gets or sets the Browse button text.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultBrowse)]
    public string Browse { get; set; }

    /// <summary>Gets or sets the collection name label.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultCollectionNameLabel)]
    public string CollectionNameLabel { get; set; }

    /// <summary>Gets or sets the Save name button text.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultSaveName)]
    public string SaveName { get; set; }

    /// <summary>Gets or sets the themes list label.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultThemesLabel)]
    public string ThemesLabel { get; set; }

    /// <summary>Gets or sets the Add button text.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultAdd)]
    public string Add { get; set; }

    /// <summary>Gets or sets the Remove button text.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultRemove)]
    public string Remove { get; set; }

    /// <summary>Gets or sets the view-mode label.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultViewByLabel)]
    public string ViewByLabel { get; set; }

    /// <summary>Gets or sets the status shown when no collection path is set.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultStatusChooseThenAdd)]
    public string StatusChooseThenAdd { get; set; }

    /// <summary>Gets or sets the open-collection dialog title.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultOpenCollectionTitle)]
    public string OpenCollectionTitle { get; set; }

    /// <summary>Gets or sets the <c>.ktheme</c> dialog filter entry (keep the <c>|*.ktheme</c> pattern).</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultThemeContainerFilter)]
    public string ThemeContainerFilter { get; set; }

    /// <summary>Gets or sets the all-files dialog filter entry (keep the <c>|*.*</c> pattern).</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultAllFilesFilter)]
    public string AllFilesFilter { get; set; }

    /// <summary>Gets or sets the status when saving a name before any theme exists.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultAddThemeBeforeSavingName)]
    public string AddThemeBeforeSavingName { get; set; }

    /// <summary>Gets or sets the status after saving the collection name. <c>{0}</c> is the name.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultCollectionNameSavedFormat)]
    public string CollectionNameSavedFormat { get; set; }

    /// <summary>Gets or sets the add-files dialog title.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultAddFilesTitle)]
    public string AddFilesTitle { get; set; }

    /// <summary>
    /// Gets or sets the add-files dialog filter. Keep the <c>|*.ext</c> patterns; translate the labels.
    /// Defaults to <see cref="KryptonPaletteFile.DialogFilter"/>.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultAddFilesFilter)]
    public string AddFilesFilter { get; set; }

    /// <summary>Gets or sets the status when add adds no themes.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultNoThemesAdded)]
    public string NoThemesAdded { get; set; }

    /// <summary>Gets or sets the status after adding files. <c>{0}</c> is the count.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultAddedFilesFormat)]
    public string AddedFilesFormat { get; set; }

    /// <summary>Gets or sets the duplicate-name prompt title.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultDuplicateThemeTitle)]
    public string DuplicateThemeTitle { get; set; }

    /// <summary>Gets or sets the duplicate-name replace question.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultReplaceExistingTheme)]
    public string ReplaceExistingTheme { get; set; }

    /// <summary>Gets or sets the add-error dialog title.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultAddToCollectionTitle)]
    public string AddToCollectionTitle { get; set; }

    /// <summary>Gets or sets the status when Remove is clicked with no selection.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultSelectThemeToRemove)]
    public string SelectThemeToRemove { get; set; }

    /// <summary>Gets or sets the status after removing a theme. <c>{0}</c> is the theme name.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultRemovedThemeFormat)]
    public string RemovedThemeFormat { get; set; }

    /// <summary>Gets or sets the remove-error dialog title.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultRemoveFromCollectionTitle)]
    public string RemoveFromCollectionTitle { get; set; }

    /// <summary>Gets or sets the status when the collection path does not exist yet.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultCollectionMissingAddToCreate)]
    public string CollectionMissingAddToCreate { get; set; }

    /// <summary>Gets or sets the kind label for a multi-theme collection.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultCollectionKindCollection)]
    public string CollectionKindCollection { get; set; }

    /// <summary>Gets or sets the kind label for a single-theme <c>.ktheme</c>.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultCollectionKindSingleTheme)]
    public string CollectionKindSingleTheme { get; set; }

    /// <summary>Gets or sets the loaded-collection status. <c>{0}</c> is the count, <c>{1}</c> is the kind label.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultThemeCountFormat)]
    public string ThemeCountFormat { get; set; }

    /// <summary>Gets or sets the open-collection error dialog title.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultOpenCollectionErrorTitle)]
    public string OpenCollectionErrorTitle { get; set; }

    /// <summary>Gets or sets the status when an action needs a collection path.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultChooseCollectionFirst)]
    public string ChooseCollectionFirst { get; set; }

    /// <summary>Gets or sets the create-collection dialog title.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultCreateCollectionTitle)]
    public string CreateCollectionTitle { get; set; }

    /// <summary>Gets or sets the suggested new collection file name (including extension).</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultCollectionFileName)]
    public string CollectionFileName { get; set; }

    /// <summary>Gets or sets the save-name error dialog title.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultSaveCollectionNameTitle)]
    public string SaveCollectionNameTitle { get; set; }

    /// <summary>Gets or sets the Large Icons view name.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultViewLargeIcon)]
    public string ViewLargeIcon { get; set; }

    /// <summary>Gets or sets the Details view name.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultViewDetails)]
    public string ViewDetails { get; set; }

    /// <summary>Gets or sets the Small Icons view name.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultViewSmallIcon)]
    public string ViewSmallIcon { get; set; }

    /// <summary>Gets or sets the List view name.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultViewList)]
    public string ViewList { get; set; }

    /// <summary>Gets or sets the Tile view name.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [DefaultValue(DefaultViewTile)]
    public string ViewTile { get; set; }
}

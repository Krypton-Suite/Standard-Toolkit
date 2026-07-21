#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonCollectionEditorStrings
{
    #region Static Strings

    private const string DEFAULT_STRING_COLLECTION_EDITOR_WINDOW_TITLE = @"String Collection Editor";

    private const string DEFAULT_STRING_COLLECTION_EDITOR_HEADER_TEXT = @"Enter the strings in the collection (one per line)";

    private const string DEFAULT_STRING_COLLECTION_EDITOR_PLACEHOLDER_CUE_TEXT = @"Enter the strings in the collection...";

    #endregion

    #region Identity

    public KryptonCollectionEditorStrings()
    {
        Reset();
    }

    #endregion

    #region Public

    /// <summary>Gets or sets the window title for the string collection editor.</summary>
    [Category(@"Visuals")]
    [Description(@"The window title for the string collection editor.")]
    [DefaultValue(DEFAULT_STRING_COLLECTION_EDITOR_WINDOW_TITLE)]
    [Localizable(true)]
    public string StringCollectionEditorWindowTitle { get; set; }

    /// <summary>Gets or sets the header text for the string collection editor.</summary>
    [Category(@"Visuals")]
    [Description(@"The header text for the string collection editor.")]
    [DefaultValue(DEFAULT_STRING_COLLECTION_EDITOR_HEADER_TEXT)]
    [Localizable(true)]
    public string StringCollectionEditorHeaderText { get; set; }

    /// <summary>Gets or sets the placeholder cue text for the string collection editor.</summary>
    [Category(@"Visuals")]
    [Description(@"The placeholder cue text for the string collection editor.")]
    [DefaultValue(DEFAULT_STRING_COLLECTION_EDITOR_PLACEHOLDER_CUE_TEXT)]
    [Localizable(true)]
    public string StringCollectionEditorPlaceholderCueText { get; set; }

    #endregion

    #region IsDefault

    [Browsable(false)]
    public bool IsDefault => StringCollectionEditorPlaceholderCueText.Equals(DEFAULT_STRING_COLLECTION_EDITOR_PLACEHOLDER_CUE_TEXT) &&
                             StringCollectionEditorHeaderText.Equals(DEFAULT_STRING_COLLECTION_EDITOR_HEADER_TEXT) &&
                             StringCollectionEditorWindowTitle.Equals(DEFAULT_STRING_COLLECTION_EDITOR_WINDOW_TITLE);

    #endregion

    #region Implementation

    public void Reset()
    {
        StringCollectionEditorPlaceholderCueText = DEFAULT_STRING_COLLECTION_EDITOR_PLACEHOLDER_CUE_TEXT;
        StringCollectionEditorHeaderText = DEFAULT_STRING_COLLECTION_EDITOR_HEADER_TEXT;
        StringCollectionEditorWindowTitle = DEFAULT_STRING_COLLECTION_EDITOR_WINDOW_TITLE;
    }

    #endregion

    #region Public Overrides

    public override string ToString() => !IsDefault ? "Modified" : GlobalStaticVariables.DEFAULT_EMPTY_STRING;

    #endregion
}

#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion


namespace Krypton.Toolkit;

[ToolboxItem(false)]
[DesignerCategory(@"code")]
public static class KryptonStringCollectionEditor
{
    #region Public

    /// <summary>Shows the string collection editor.</summary>
    /// <param name="inputStrings">The input strings.</param>
    /// <param name="useRichTextBox">if set to <c>true</c> [use rich text box].</param>
    /// <param name="headerText">The header text.</param>
    /// <param name="windowText">The window text.</param>
    /// <returns>An array of strings.</returns>
    public static string[] Show(string[] inputStrings, bool useRichTextBox = true,
        string? headerText = @"Enter the strings in the collection (one per line):",
        string windowText = @"String Collection Editor")
        => InternalShow(null, inputStrings, useRichTextBox, headerText, windowText);

    public static string[] Show(IWin32Window? owner, string[] inputStrings, bool useRichTextBox = true,
        string? headerText = @"Enter the strings in the collection (one per line):",
        string windowText = @"String Collection Editor")
        => InternalShow(owner, inputStrings, useRichTextBox, headerText, windowText);

    public static string[] Show(bool useRichTextBox = true,
        string? headerText = @"Enter the strings in the collection (one per line):",
        string windowText = @"String Collection Editor")
        => InternalShow(null, null, useRichTextBox, headerText, windowText);

    public static string[] Show(IWin32Window? owner, bool useRichTextBox = true,
        string? headerText = @"Enter the strings in the collection (one per line):",
        string windowText = @"String Collection Editor")
        => InternalShow(owner, null, useRichTextBox, headerText, windowText);

    public static StringCollection Show(StringCollection inputStrings, bool useRichTextBox = true,
        string? headerText = @"Enter the strings in the collection (one per line):",
        string windowText = @"String Collection Editor")
        => InternalShowStringCollection(null, inputStrings, useRichTextBox, headerText, windowText);

    public static StringCollection Show(IWin32Window owner, StringCollection inputStrings, bool useRichTextBox = true,
        string? headerText = @"Enter the strings in the collection (one per line):",
        string windowText = @"String Collection Editor")
        => InternalShowStringCollection(owner, inputStrings, useRichTextBox, headerText, windowText);

    public static StringCollection ShowDialog(bool useRichTextBox = true,
        string? headerText = @"Enter the strings in the collection (one per line):",
        string windowText = @"String Collection Editor")
        => InternalShowStringCollection(null, null, useRichTextBox, headerText, windowText);

    public static StringCollection ShowDialog(IWin32Window owner, bool useRichTextBox = true,
        string? headerText = @"Enter the strings in the collection (one per line):",
        string windowText = @"String Collection Editor")
        => InternalShowStringCollection(owner, null, useRichTextBox, headerText, windowText);

    #endregion

    #region Implementation

    /// <summary>Shows the string collection editor.</summary>
    /// <param name="owner">The owner.</param>
    /// <param name="input">The input.</param>
    /// <param name="useRichTextBox">The use rich text box.</param>
    /// <param name="headerText">The header text.</param>
    /// <param name="windowTitle">The window title.</param>
    /// <returns>A collection of string items.</returns>
    private static string[] InternalShow(IWin32Window? owner, string[]? input, bool? useRichTextBox, string? headerText, string windowTitle)
        => VisualMultilineStringEditorForm.InternalShow(owner, input!, useRichTextBox, headerText, windowTitle)!;

    /// <summary>Shows the string collection editor.</summary>
    /// <param name="owner">The owner.</param>
    /// <param name="input">The input.</param>
    /// <param name="useRichTextBox">The use rich text box.</param>
    /// <param name="headerText">The header text.</param>
    /// <param name="windowTitle">The window title.</param>
    /// <returns>A collection of string items.</returns>
    private static StringCollection InternalShowStringCollection(IWin32Window? owner, StringCollection? input, bool useRichTextBox, string? headerText, string windowTitle)
        => VisualMultilineStringEditorForm.InternalShowStringCollection(owner, input!, useRichTextBox, headerText, windowTitle)!;

    #endregion
}
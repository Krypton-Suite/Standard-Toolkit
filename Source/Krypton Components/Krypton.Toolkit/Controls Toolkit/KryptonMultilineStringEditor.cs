#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Public API to display the <see cref="VisualMultilineStringEditorForm"/>.</summary>
public class KryptonMultilineStringEditor
{
    #region Public

    /// <summary>Shows a new <see cref="VisualMultilineStringEditorForm"/> with default options.</summary>
    /// <returns>A new <see cref="VisualMultilineStringEditorForm"/> with default data.</returns>
    public static DialogResult Show() => ShowCore(null, null, null, null, null);

    /// <summary>Shows a new <see cref="VisualMultilineStringEditorForm"/> with specified options.</summary>
    /// <param name="contents">The string contents array.</param>
    /// <returns>A new <see cref="VisualMultilineStringEditorForm"/> with specified data.</returns>
    public static DialogResult Show(string[]? contents) => ShowCore(contents, null, null, null, null);

    /// <summary>Shows a new <see cref="VisualMultilineStringEditorForm"/> with specified options.</summary>
    /// <param name="contents">The string contents array.</param>
    /// <param name="useRichTextBox">The use rich text box.</param>
    /// <returns>A new <see cref="VisualMultilineStringEditorForm"/> with specified data.</returns>
    public static DialogResult Show(string[]? contents, bool? useRichTextBox) => ShowCore(contents, null, useRichTextBox, null, null);

    /// <summary>Shows a new <see cref="VisualMultilineStringEditorForm"/> with specified options.</summary>
    /// <param name="contents">The string contents array.</param>
    /// <param name="useRichTextBox">The use rich text box.</param>
    /// <param name="headerText">The header text.</param>
    /// <param name="windowText">The window text.</param>
    /// <returns>A new <see cref="VisualMultilineStringEditorForm"/> with specified data.</returns>
    public static DialogResult Show(string[]? contents, bool? useRichTextBox, string? headerText, string? windowText) => ShowCore(contents, null, useRichTextBox, headerText, windowText);

    /// <summary>Shows a new <see cref="VisualMultilineStringEditorForm"/> with specified options.</summary>
    /// <param name="collection">The string collection.</param>
    /// <returns>A new <see cref="VisualMultilineStringEditorForm"/> with specified data.</returns>
    public static DialogResult Show(StringCollection? collection) => ShowCore(null, collection, null, null, null);

    /// <summary>Shows a new <see cref="VisualMultilineStringEditorForm"/> with specified options.</summary>
    /// <param name="collection">The string collection.</param>
    /// <param name="useRichTextBox">The use rich text box.</param>
    /// <returns>A new <see cref="VisualMultilineStringEditorForm"/> with specified data.</returns>
    public static DialogResult Show(StringCollection? collection, bool? useRichTextBox) => ShowCore(null, collection, useRichTextBox, null, null);

    /// <summary>Shows a new <see cref="VisualMultilineStringEditorForm"/> with specified options.</summary>
    /// <param name="collection">The string collection.</param>
    /// <param name="useRichTextBox">The use rich text box.</param>
    /// <param name="headerText">The header text.</param>
    /// <param name="windowText">The window text.</param>
    /// <returns>A new <see cref="VisualMultilineStringEditorForm"/> with specified data.</returns>
    public static DialogResult Show(StringCollection? collection, bool? useRichTextBox, string? headerText, string? windowText) => ShowCore(null, collection, useRichTextBox, headerText, windowText);

    #endregion

    #region Implementation

    private static DialogResult ShowCore(string[]? contents, StringCollection? collection,
        bool? useRichTextBox, string? headerText, string? windowTitle)
    {
        using var kmse = new VisualMultilineStringEditorForm(contents, collection, useRichTextBox, headerText, windowTitle);

        return kmse.ShowDialog();
    }

    #endregion
}
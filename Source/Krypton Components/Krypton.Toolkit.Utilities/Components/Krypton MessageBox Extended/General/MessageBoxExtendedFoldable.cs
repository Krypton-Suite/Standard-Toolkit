#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Shared foldable-details helpers for <see cref="KryptonMessageBoxExtended"/>, matching
/// <see cref="KryptonFoldableDialog"/> expander glyphs, captions, and default details height.
/// </summary>
internal static class MessageBoxExtendedFoldable
{
    // Down-pointing / up-pointing triangles used as the expander glyph (same as VisualFoldableDialogForm).
    internal const char ExpandGlyph = '\u25BC';
    internal const char CollapseGlyph = '\u25B2';

    /// <summary>Fixed height (unscaled pixels) for the details RichTextBox when none is supplied.</summary>
    internal const int DefaultDetailsRegionHeight = 180;

    /// <summary>
    /// Resolved foldable-footer settings from <see cref="KryptonMessageBoxExtendedData"/>.
    /// </summary>
    internal struct FooterSpec
    {
        internal string? Text;
        internal bool Expanded;
        internal ExtendedKryptonMessageBoxFooterContentType ContentType;
        internal int? RichTextBoxHeight;
        internal string? ExpandButtonText;
        internal string? CollapseButtonText;
        internal string? MoreDetailsButtonText;
    }

    /// <summary>
    /// Builds expander caption text with the FoldableDialog glyph and Show/Hide details strings.
    /// </summary>
    /// <param name="expanded">Whether the details region is currently expanded.</param>
    /// <param name="expandButtonText">Caption while expanded (Hide Details). Optional.</param>
    /// <param name="collapseButtonText">Caption while collapsed (Show Details). Optional.</param>
    /// <param name="moreDetailsButtonText">Single caption used for both states when the dedicated strings are empty.</param>
    /// <returns>Glyph plus caption for the toggle button.</returns>
    internal static string GetToggleCaption(bool expanded, string? expandButtonText, string? collapseButtonText, string? moreDetailsButtonText)
    {
        KryptonFoldableDialogStrings strings = KryptonManager.Strings.FoldableDialogStrings;
        string caption = expanded
            ? FirstNonEmpty(expandButtonText, moreDetailsButtonText, strings.ExpandText)
            : FirstNonEmpty(collapseButtonText, moreDetailsButtonText, strings.CollapseText);
        char glyph = expanded ? CollapseGlyph : ExpandGlyph;
        return $"{glyph} {caption}";
    }

    /// <summary>
    /// Resolves foldable footer settings from the data model.
    /// </summary>
    /// <param name="data">The message box data.</param>
    /// <param name="spec">The resolved footer specification.</param>
    /// <returns><c>true</c> when the expander should be shown.</returns>
    internal static bool TryResolveFromData(KryptonMessageBoxExtendedData data, out FooterSpec spec)
    {
        string? detailsText = !string.IsNullOrEmpty(data.DetailsText)
            ? data.DetailsText
            : data.MoreDetailsMessageText;

        ExtendedKryptonMessageBoxFooterContentType contentType = data.FooterContentType
            ?? (!string.IsNullOrEmpty(detailsText)
                ? ExtendedKryptonMessageBoxFooterContentType.RichTextBox
                : ExtendedKryptonMessageBoxFooterContentType.Text);

        spec = new FooterSpec
        {
            Text = detailsText,
            Expanded = data.Expanded || data.MoreDetailsExpanded,
            ContentType = contentType,
            RichTextBoxHeight = data.FooterRichTextBoxHeight,
            ExpandButtonText = data.ExpandButtonText,
            CollapseButtonText = data.CollapseButtonText,
            MoreDetailsButtonText = data.MoreDetailsButtonText
        };

        return !string.IsNullOrEmpty(detailsText)
               || contentType == ExtendedKryptonMessageBoxFooterContentType.CheckBox
               || data.ShowMoreDetailsOption;
    }

    /// <summary>
    /// Returns the requested RichTextBox height, or the FoldableDialog default.
    /// </summary>
    /// <param name="specified">Caller-supplied height, or <see langword="null"/>.</param>
    /// <returns>A positive height in pixels.</returns>
    internal static int ResolveRichTextBoxHeight(int? specified) =>
        specified is > 0
            ? specified.Value
            : DefaultDetailsRegionHeight;

    private static string FirstNonEmpty(string? first, string? second, string fallback)
    {
        if (!string.IsNullOrEmpty(first))
        {
            return first!;
        }

        return !string.IsNullOrEmpty(second) ? second! : fallback;
    }
}

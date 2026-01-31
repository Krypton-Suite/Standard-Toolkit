#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Caches preview pages captured from PrintDocument.
/// </summary>
internal class PrintPreviewPageCache
{
    #region Instance Fields
    private PreviewPageInfo[]? _pages;
    private PrintDocument? _cachedDocument;
    #endregion

    #region Public
    /// <summary>
    /// Gets the number of cached pages.
    /// </summary>
    public int PageCount => _pages?.Length ?? 0;

    /// <summary>
    /// Gets a preview page by index.
    /// </summary>
    /// <param name="index">Zero-based page index.</param>
    /// <returns>PreviewPageInfo for the page, or null if index is invalid.</returns>
    public PreviewPageInfo? GetPage(int index)
    {
        if (_pages == null || index < 0 || index >= _pages.Length)
        {
            return null;
        }

        return _pages[index];
    }

    /// <summary>
    /// Clears the page cache.
    /// </summary>
    public void Clear()
    {
        if (_pages != null)
        {
            foreach (var page in _pages)
            {
                page?.Image?.Dispose();
            }
        }

        _pages = null;
        _cachedDocument = null;
    }

    /// <summary>
    /// Generates preview pages from a PrintDocument.
    /// </summary>
    /// <param name="document">The PrintDocument to generate previews for.</param>
    /// <returns>True if pages were generated successfully.</returns>
    public bool GeneratePages(PrintDocument document)
    {
        if (document == null)
        {
            Clear();
            return false;
        }

        // If same document, don't regenerate
        if (_cachedDocument == document && _pages != null)
        {
            return true;
        }

        try
        {
            // Clear existing pages
            Clear();

            // Create preview controller
            var previewController = new PreviewPrintController();
            var originalController = document.PrintController;

            try
            {
                // Set preview controller
                document.PrintController = previewController;

                // Generate preview pages
                document.Print();

                // Get preview pages
                _pages = previewController.GetPreviewPageInfo();
                _cachedDocument = document;

                return _pages.Length > 0;
            }
            finally
            {
                // Restore original controller
                document.PrintController = originalController;
            }
        }
        catch
        {
            Clear();
            return false;
        }
    }
    #endregion
}

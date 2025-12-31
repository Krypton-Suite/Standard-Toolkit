#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Provides data for the Search event.
/// </summary>
public class SearchEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the SearchEventArgs class.
    /// </summary>
    /// <param name="searchText">The search text.</param>
    public SearchEventArgs(string searchText)
    {
        SearchText = searchText;
    }

    /// <summary>
    /// Gets the search text.
    /// </summary>
    public string SearchText { get; }
}
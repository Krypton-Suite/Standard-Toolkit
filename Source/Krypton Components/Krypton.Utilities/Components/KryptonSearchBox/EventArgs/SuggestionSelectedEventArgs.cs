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
/// Provides data for the SuggestionSelected event.
/// </summary>
public class SuggestionSelectedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the SuggestionSelectedEventArgs class.
    /// </summary>
    /// <param name="index">The index of the selected suggestion.</param>
    public SuggestionSelectedEventArgs(int index)
    {
        Index = index;
    }

    /// <summary>
    /// Gets the index of the selected suggestion.
    /// </summary>
    public int Index { get; }

    /// <summary>
    /// Gets the selected suggestion text (for backward compatibility).
    /// </summary>
    public string? Suggestion { get; set; }

    /// <summary>
    /// Gets the selected suggestion object (can be string or IContentValues).
    /// </summary>
    public object? SuggestionObject { get; set; }
}
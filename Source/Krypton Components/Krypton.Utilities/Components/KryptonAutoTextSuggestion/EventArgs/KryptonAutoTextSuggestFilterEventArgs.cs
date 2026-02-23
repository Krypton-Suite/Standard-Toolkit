#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Provides data for suggestion filtering events.
/// </summary>
public class KryptonAutoTextSuggestFilterEventArgs : EventArgs
{
    #region Public

    /// <summary>
    /// Gets the current text input being used for filtering.
    /// </summary>
    public string FilterText { get; }

    /// <summary>
    /// Gets or sets the list of suggestions to display.
    /// </summary>
    public List<KryptonAutoTextSuggestItem> Suggestions { get; set; }

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the KryptonAutoTextSuggestFilterEventArgs class.
    /// </summary>
    /// <param name="filterText">The filter text.</param>
    /// <param name="suggestions">The initial suggestions list.</param>
    public KryptonAutoTextSuggestFilterEventArgs(string filterText, List<KryptonAutoTextSuggestItem> suggestions)
    {
        FilterText = filterText;
        Suggestions = suggestions;
    }

    #endregion
}
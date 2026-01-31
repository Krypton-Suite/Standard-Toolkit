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
/// Represents a column definition for DataGridView suggestion display.
/// </summary>
public class SearchSuggestionColumnDefinition
{
    /// <summary>
    /// Gets or sets the column name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the data property name (for binding).
    /// </summary>
    public string DataPropertyName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the header text.
    /// </summary>
    public string HeaderText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the column width (0 = auto-size).
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// Gets or sets the auto-size mode.
    /// </summary>
    public DataGridViewAutoSizeColumnMode AutoSizeMode { get; set; } = DataGridViewAutoSizeColumnMode.Fill;

    /// <summary>
    /// Gets or sets a function to extract the column value from a suggestion object.
    /// This property cannot be serialized by the designer and must be set in code.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Func<object, object?>? ValueExtractor { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchSuggestionColumnDefinition"/> class.
    /// </summary>
    public SearchSuggestionColumnDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchSuggestionColumnDefinition"/> class.
    /// </summary>
    /// <param name="name">The column name.</param>
    /// <param name="headerText">The header text.</param>
    /// <param name="valueExtractor">Function to extract the column value.</param>
    public SearchSuggestionColumnDefinition(string name, string headerText, Func<object, object?>? valueExtractor = null)
    {
        Name = name;
        DataPropertyName = name;
        HeaderText = headerText;
        ValueExtractor = valueExtractor;
    }
}
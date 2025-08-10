namespace Krypton.Toolkit;

/// <summary>
/// Represents a single filter condition or rule applied to a data field (column).
/// It encapsulates details about the table, column, data type, operator, values,
/// and grouping information for building dynamic queries or data visualizations.
/// </summary>
public class KryptonOutlookGridFilterField
{
    /// <summary>
    /// Gets or sets the original column name in the database or data source.
    /// </summary>
    public string ColumnName { get; set; } = default!;
    /// <summary>
    /// Gets or sets the data type of the column (e.g., "string", "int", "DateTime").
    /// </summary>
    public string DataType { get; set; } = default!;
    /// <summary>
    /// Gets or sets the logical operator for the filter (e.g., "=", ">", "LIKE").
    /// </summary>
    public string Operator { get; set; } = default!;
    /// <summary>
    /// Gets or sets a human-readable representation of the operator (e.g., "Equals", "Greater Than").
    /// </summary>
    public string ReadableOperator { get; set; } = default!;

    /// <summary>
    /// Gets or sets the first value used in the filter condition.
    /// </summary>
    public string Value1 { get; set; } = default!;
    /// <summary>
    /// Gets or sets the second value used in the filter condition (e.g., for "Between" operator).
    /// </summary>
    public string Value2 { get; set; } = default!;
    /// <summary>
    /// Gets or sets the conjunction (e.g., "AND", "OR") for combining this filter with the next column-level filter.
    /// </summary>
    public string ColumnConjunction { get; set; } = default!;
    /// <summary>
    /// Gets or sets an integer representation of the column conjunction.
    /// </summary>
    public int ColumnConjunctionItem { get; set; }
    /// <summary>
    /// Gets or sets information about the group this filter belongs to (e.g., Group "1" or SubGroup "1.1, 1.2, 1.1.1").
    /// </summary>
    public string GroupInfo { get; set; } = default!;
    /// <summary>
    /// Gets or sets the conjunction (e.g., "AND", "OR") for combining this filter group with the next group-level filter.
    /// </summary>
    public string GroupConjunction { get; set; } = default!;
    /// <summary>
    /// Gets or sets an integer representation of the group conjunction.
    /// </summary>
    public int GroupConjunctionItem { get; set; }

    /// <summary>
    /// Gets or sets the generated filter string representation of this field's condition.
    /// </summary>
    public string Filter { get; set; } = default!;

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonOutlookGridFilterField"/> class.
    /// </summary>
    public KryptonOutlookGridFilterField() { }

    /// <summary>
    /// A list of sub-filter groups nested within this filter condition.
    /// </summary>
    public List<KryptonOutlookGridFilterField> SubGroups { get; set; } = null!;

    /// <summary>
    /// Gets or sets information about the temp group this filter belongs to (e.g., Group "1" or SubGroup "1.1, 1.2, 1.1.1").
    /// </summary>
    public bool IsGroupInfoTemp { get; set; } = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonOutlookGridFilterField"/> class with column details.
    /// </summary>
    /// <param name="columnName">The original name of the column.</param>
    /// <param name="dataType">The data type of the column.</param>
    public KryptonOutlookGridFilterField(string columnName, string dataType)
    {
        ColumnName = columnName;
        DataType = dataType;
    }

}
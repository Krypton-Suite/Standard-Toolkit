namespace Krypton.Toolkit;

/*/// <summary>
/// Represents a database table as a source for data operations,
/// including its name, an optional alias, and a collection of its columns.
/// </summary>
public class SourceTable
{
    /// <summary>
    /// Gets or sets the original name of the table in the database.
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// Gets or sets an alias for the table, often used in queries for brevity or clarity.
    /// </summary>
    public string Alias { get; set; } = default!;
    /// <summary>
    /// Gets or sets a list of <see cref="SourceColumn"/> objects that belong to this table.
    /// </summary>
    public List<SourceColumn> Columns { get; set; } = default!;

    /// <summary>
    /// Initializes a new empty instance of the <see cref="SourceTable"/> class.
    /// </summary>
    public SourceTable() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="SourceTable"/> class with a name and an alias.
    /// </summary>
    /// <param name="name">The original name of the table.</param>
    /// <param name="alias">The alias for the table.</param>
    public SourceTable(string name, string alias)
    {
        Name = name;
        Alias = alias;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SourceTable"/> class with a name, an alias, and a list of columns.
    /// </summary>
    /// <param name="name">The original name of the table.</param>
    /// <param name="alias">The alias for the table.</param>
    /// <param name="columns">A list of <see cref="SourceColumn"/> objects belonging to this table.</param>
    public SourceTable(string name, string alias, List<SourceColumn> columns)
    {
        Name = name;
        Alias = alias;
        Columns = columns;
    }
}
*/

/// <summary>
/// Represents a column within a database table, including its name, an optional alias, and its data type.
/// </summary>
public class KryptonOutlookGridFilterSourceColumn
{
    /// <summary>
    /// Gets or sets the original name of the column in the database table.
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// Gets or sets an alias for the column, often used for display purposes or in queries.
    /// </summary>
    public string Alias { get; set; } = default!;
    /// <summary>
    /// Gets or sets the data type of the column (e.g., "string", "int", "DateTime").
    /// </summary>
    public string DataType { get; set; } = default!;

    /// <summary>
    /// Gets or sets the format of the column (e.g., "N2", "dd/MM/yyyy").
    /// </summary>
    public string Format { get; set; } = default!;

    /// <summary>
    /// Initializes a new empty instance of the <see cref="KryptonOutlookGridFilterSourceColumn"/> class.
    /// </summary>
    public KryptonOutlookGridFilterSourceColumn() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonOutlookGridFilterSourceColumn"/> class with a name, an alias, a data type, and a format.
    /// </summary>
    /// <param name="name">The original name of the column.</param>
    /// <param name="alias">The alias for the column.</param>
    /// <param name="dataType">The data type of the column.</param>
    /// <param name="format">The format of the column.</param>
    public KryptonOutlookGridFilterSourceColumn(string name, string alias, string dataType, string format)
    {
        Name = name;
        Alias = alias;
        DataType = dataType;
        Format = format;
    }
}
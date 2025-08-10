namespace Krypton.Toolkit
{

    /// <summary>
    /// Enumerates the different operators that can be used to filter data in a query.
    /// </summary>
    public enum KryptonOutlookGridFilterOperators
    {
        /// <summary>
        /// Equal to a specified value.
        /// </summary>
        [Description("=")]
        Equals,
        /// <summary>
        /// Not equal to a specified value.
        /// </summary>
        [Description("<>")]
        NotEquals,
        /// <summary>
        /// Value is in a list of specified values.
        /// </summary>
        [Description("IN")]
        In,
        /// <summary>
        /// Value is not in a list of specified values.
        /// </summary>
        [Description("NOT IN")]
        NotIn,
        /// <summary>
        /// Value is within a specified range (inclusive).
        /// </summary>
        [Description("BETWEEN")]
        Between,
        /// <summary>
        /// Value is outside a specified range.
        /// </summary>
        [Description("NOT BETWEEN")]
        NotBetween,
        /// <summary>
        /// Value is less than a specified value.
        /// </summary>
        [Description("<")]
        LessThan,
        /// <summary>
        /// Value is less than or equal to a specified value.
        /// </summary>
        [Description("<=")]
        LessThanOrEqual,
        /// <summary>
        /// Value is greater than a specified value.
        /// </summary>
        [Description(">")]
        GreaterThan,
        /// <summary>
        /// Value is greater than or equal to a specified value.
        /// </summary>
        [Description(">=")]
        GreaterThanOrEqual,
        /// <summary>
        /// Value starts with a specified string.
        /// </summary>
        [Description("BEGINSWITH")]
        BeginsWith,
        /// <summary>
        /// Value does not start with a specified string.
        /// </summary>
        [Description("NOT BEGINSWITH")]
        NotBeginsWith,
        /// <summary>
        /// Value contains a specified string.
        /// </summary>
        [Description("LIKE")]
        Contains,
        /// <summary>
        /// Value does not contain a specified string.
        /// </summary>
        [Description("NOT LIKE")]
        NotContains,
        /// <summary>
        /// Value ends with a specified string.
        /// </summary>
        [Description("ENDSWITH")]
        EndsWith,
        /// <summary>
        /// Value does not end with a specified string.
        /// </summary>
        [Description("NOT ENDSWITH")]
        NotEndsWith,
        /// <summary>
        /// Value is an empty string.
        /// </summary>
        [Description("=")]
        IsEmpty,
        /// <summary>
        /// Value is not an empty string.
        /// </summary>
        [Description("<>")]
        IsNotEmpty,
        /// <summary>
        /// Value is null.
        /// </summary>
        [Description("IS NULL")]
        IsNull,
        /// <summary>
        /// Value is not null.
        /// </summary>
        [Description("IS NOT NULL")]
        IsNotNull,
        /*/// <summary>
        /// Value is true.
        /// </summary>
        [Description("TRUE")]
        True,
        /// <summary>
        /// Value is true.
        /// </summary>
        [Description("FALSE")]
        False*/
    }

    /*/// <summary>
    /// Enumerates the different operators that can be used to filter data in a query.
    /// </summary>
    public enum FilterOperators
    {
        /// <summary>
        /// Equal to a specified value.
        /// </summary>
        [Description("Equals")]
        Equals,
        /// <summary>
        /// Not equal to a specified value.
        /// </summary>
        [Description("Not Equals")]
        NotEquals,
        /// <summary>
        /// Value is in a list of specified values.
        /// </summary>
        [Description("IN")]
        In,
        /// <summary>
        /// Value is not in a list of specified values.
        /// </summary>
        [Description("NOT IN")]
        NotIn,
        /// <summary>
        /// Value is within a specified range (inclusive).
        /// </summary>
        [Description("BETWEEN")]
        Between,
        /// <summary>
        /// Value is outside a specified range.
        /// </summary>
        [Description("NOT BETWEEN")]
        NotBetween,
        /// <summary>
        /// Value is less than a specified value.
        /// </summary>
        [Description("Less Than")]
        LessThan,
        /// <summary>
        /// Value is less than or equal to a specified value.
        /// </summary>
        [Description("Less Than Or Equal")]
        LessThanOrEqual,
        /// <summary>
        /// Value is greater than a specified value.
        /// </summary>
        [Description("Greater Than")]
        GreaterThan,
        /// <summary>
        /// Value is greater than or equal to a specified value.
        /// </summary>
        [Description("Greater Than Or Equal")]
        GreaterThanOrEqual,
        /// <summary>
        /// Value starts with a specified string.
        /// </summary>
        [Description("Begins With")]
        BeginsWith,
        /// <summary>
        /// Value does not start with a specified string.
        /// </summary>
        [Description("Does Begins With")]
        NotBeginsWith,
        /// <summary>
        /// Value contains a specified string.
        /// </summary>
        [Description("Contains")]
        Contains,
        /// <summary>
        /// Value does not contain a specified string.
        /// </summary>
        [Description("Does Not Contain")]
        NotContains,
        /// <summary>
        /// Value ends with a specified string.
        /// </summary>
        [Description("Ends With")]
        EndsWith,
        /// <summary>
        /// Value does not end with a specified string.
        /// </summary>
        [Description("Not Ends With")]
        NotEndsWith,
        /// <summary>
        /// Value is an empty string.
        /// </summary>
        [Description("Empty")]
        IsEmpty,
        /// <summary>
        /// Value is not an empty string.
        /// </summary>
        [Description("Not Empty")]
        IsNotEmpty,
        /// <summary>
        /// Value is null.
        /// </summary>
        [Description("IS NULL")]
        IsNull,
        /// <summary>
        /// Value is not null.
        /// </summary>
        [Description("IS NOT NULL")]
        IsNotNull,
        /// <summary>
        /// Value is true.
        /// </summary>
        [Description("True")]
        True,
        /// <summary>
        /// Value is true.
        /// </summary>
        [Description("False")]
        False
    }*/
}

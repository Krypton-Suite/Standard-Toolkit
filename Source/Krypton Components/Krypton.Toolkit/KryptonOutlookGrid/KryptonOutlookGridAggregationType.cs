namespace Krypton.Toolkit
{
    /// <summary>
    /// Specifies the type of aggregation to perform on a column in group rows.
    /// </summary>
    public enum KryptonOutlookGridAggregationType
    {
        /// <summary>
        /// No aggregation will be performed.
        /// </summary>
        None,
        /// <summary>
        /// Calculates the sum of numeric values in the group.
        /// </summary>
        Sum,
        /// <summary>
        /// Counts the number of non-null values in the group.
        /// </summary>
        Count,
        /// <summary>
        /// Calculates the average of numeric values in the group.
        /// </summary>
        Average,
        /// <summary>
        /// Finds the minimum value in the group.
        /// </summary>
        Min,
        /// <summary>
        /// Finds the maximum value in the group.
        /// </summary>
        Max,
        /// <summary>
        /// Finds the minimum and maximum value in the group.
        /// </summary>
        MinMax
    }

}

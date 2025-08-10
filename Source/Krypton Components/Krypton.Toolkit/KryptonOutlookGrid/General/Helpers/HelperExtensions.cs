namespace Krypton.Toolkit;

/// <summary>
/// Provides a collection of static extension methods that add functionality to existing types.
/// These methods enhance readability and simplify common operations across different parts of the application.
/// </summary>
internal static class HelperExtensions
{

#if NETFRAMEWORK
    /// <summary>
    /// Checks if a string contains another string using the specified string comparison type.
    /// This extension method is only available in the .NET Framework.
    /// </summary>
    /// <param name="source">The string to search within.</param>
    /// <param name="toCheck">The string to search for.</param>
    /// <param name="comparison">One of the enumeration values that specifies the rules for the search.</param>
    /// <returns><c>true</c> if the <paramref name="toCheck"/> parameter occurs within the <paramref name="source"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool Contains(this string source, string toCheck, StringComparison comparison) =>
        !string.IsNullOrEmpty(source) && !string.IsNullOrEmpty(toCheck) && source.IndexOf(toCheck, comparison) >= 0;
#endif

    /// <summary>
    /// Determines whether an object is numeric by attempting to parse it as a double.
    /// </summary>
    /// <param name="value">The object to check.</param>
    /// <returns><c>true</c> if the object can be parsed as a double; otherwise, <c>false</c>.</returns>
    public static bool IsNumeric(this object value) =>
        value is not null && double.TryParse(value.ToString(), out _);

    /// <summary>
    /// Converts an object to a string. If the object is null, returns an empty string.
    /// </summary>
    /// <param name="obj">The object to convert.</param>
    /// <returns>A string representation of the object, or an empty string if null.</returns>
    public static string ToStringNull(this object? obj)
    {
        return obj?.ToString() ?? "";
    }

    /// <summary>
    /// Converts an object to an integer. If conversion fails, returns 0.
    /// </summary>
    /// <param name="obj">The object to convert.</param>
    /// <returns>An integer representation of the object, or 0 if conversion fails.</returns>
    public static int ToInteger(this object? obj)
    {
        if (obj == null) return 0;
        if (int.TryParse(obj.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out int result))
        {
            return result;
        }
        return 0;
    }

    /// <summary>
    /// Converts an object to a long integer. If conversion fails, returns 0L.
    /// </summary>
    /// <param name="obj">The object to convert.</param>
    /// <returns>A long integer representation of the object, or 0L if conversion fails.</returns>
    public static long ToLong(this object? obj)
    {
        if (obj == null) return 0L;
        if (long.TryParse(obj.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out long result))
        {
            return result;
        }
        return 0L;
    }

    /// <summary>
    /// Converts an object to a double. If conversion fails, returns 0.0.
    /// </summary>
    /// <param name="obj">The object to convert.</param>
    /// <returns>A double representation of the object, or 0.0 if conversion fails.</returns>
    public static double ToDouble(this object? obj)
    {
        if (obj == null) return 0.0;
        if (double.TryParse(obj.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
        {
            return result;
        }
        return 0.0;
    }

    /// <summary>
    /// Converts an object to a decimal. If conversion fails, returns 0m.
    /// </summary>
    /// <param name="obj">The object to convert.</param>
    /// <returns>A decimal representation of the object, or 0m if conversion fails.</returns>
    public static decimal ToDecimal(this object? obj)
    {
        if (obj == null) return 0m;
        if (decimal.TryParse(obj.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
        {
            return result;
        }
        return 0m;
    }

    /// <summary>
    /// Converts an object to a boolean. If conversion fails, returns false.
    /// </summary>
    /// <param name="obj">The object to convert.</param>
    /// <returns>A boolean representation of the object, or false if conversion fails.</returns>
    public static bool ToBoolean(this object? obj)
    {
        if (obj == null) return false;
        if (bool.TryParse(obj.ToString(), out bool result))
        {
            return result;
        }
        return false;
    }

    /// <summary>
    /// Provides culture-invariant settings for date parsing.
    /// </summary>
    private static readonly CultureInfo provider = CultureInfo.InvariantCulture;

    /// <summary>
    /// An array of common date and date-time formats used for parsing.
    /// </summary>
    private static readonly string[] formats = [.. new string[] {
        // Date only formats
        "dd/MM/yyyy", "dd-MM-yyyy", "dd.MM.yyyy", "dd/MMM/yyyy", "dd-MMM-yyyy", "dd.MMM/yyyy",
        "MM/dd/yyyy", "MM-dd-yyyy", "MM.dd.yyyy", "M/d/yyyy", "M-d-yyyy", "M.d.yyyy",

        // Date and Time formats (24-hour)
        "dd/MM/yyyy HH:mm:ss", "dd-MM-yyyy HH:mm:ss", "dd.MM.yyyy HH:mm:ss",
        "MM/dd/yyyy HH:mm:ss", "MM-dd-yyyy HH:mm:ss",

        // Date and Time formats (12-hour with AM/PM)
        "dd/MM/yyyy h:mm:ss tt", "dd-MM-yyyy h:mm:ss tt", "dd.MM.yyyy h:mm:ss tt",
        "MM/dd/yyyy h:mm:ss tt", "MM-dd-yyyy h:mm:ss tt",

        // Short time formats
        "dd/MM/yyyy H:mm", "dd/MM/yyyy h:mm tt",

        // Add more specific formats if needed, e.g., without seconds
        "dd/MM/yyyy HH:mm", "dd-MM-yyyy HH:mm", "dd.MM.yyyy HH:mm",
        "MM/dd/yyyy HH:mm", "MM-dd-yyyy HH:mm",

        "dd/MM/yyyy h:mm tt", "dd-MM-yyyy h:mm tt", "dd.MM.yyyy h:mm tt",
        "MM/dd/yyyy h:mm tt", "MM-dd-yyyy h:mm tt"

    }.Union(provider.DateTimeFormat.GetAllDateTimePatterns()).OrderByDescending(f => f.Length).ToArray()]; // Order by length for better matching

    /// <summary>
    /// Converts an object to a DateTime. If conversion fails, returns DateTime.MinValue.
    /// </summary>
    /// <param name="obj">The object to convert.</param>
    /// <returns>A DateTime representation of the object, or DateTime.MinValue if conversion fails.</returns>
    public static DateTime ToDateTime(this object? obj)
    {
        if (obj == null) return DateTime.MinValue;

        string? dateString = obj.ToString();
        if (string.IsNullOrWhiteSpace(dateString)) return DateTime.MinValue;

        // Try parsing with exact formats first
        if (DateTime.TryParseExact(dateString, formats, provider, DateTimeStyles.None, out DateTime resultExact))
        {
            return resultExact;
        }

        // If TryParseExact fails, try general TryParse
        // This is useful for formats that might be less strict or dynamically generated,
        // but it's generally less robust than TryParseExact for known formats.
        if (DateTime.TryParse(dateString, provider, DateTimeStyles.None, out DateTime resultGeneral))
        {
            return resultGeneral;
        }

        return DateTime.MinValue;
    }

    /// <summary>
    /// Determines whether the specified string represents a valid date based on the current system's culture settings.
    /// </summary>
    /// <param name="obj">The string to check.</param>
    /// <returns>True if the string can be parsed as a date, false otherwise.</returns>
    public static bool IsDate(this object? obj)
    {
        if (obj == null) return false;

        string? dateString = obj.ToString();
        if (string.IsNullOrWhiteSpace(dateString)) return false;

        // Try parsing with exact formats first
        if (DateTime.TryParseExact(dateString, formats, provider, DateTimeStyles.None, out DateTime resultExact))
        {
            return true;
        }

        // If TryParseExact fails, try general TryParse
        // This is useful for formats that might be less strict or dynamically generated,
        // but it's generally less robust than TryParseExact for known formats.
        if (DateTime.TryParse(dateString, provider, DateTimeStyles.None, out DateTime resultGeneral))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Converts an object to a Guid. If conversion fails, returns Guid.Empty.
    /// </summary>
    /// <param name="obj">The object to convert.</param>
    /// <returns>A Guid representation of the object, or Guid.Empty if conversion fails.</returns>
    public static Guid ToGuid(this object? obj)
    {
        if (obj == null) return Guid.Empty;
        if (Guid.TryParse(obj.ToString(), out Guid result))
        {
            return result;
        }
        return Guid.Empty;
    }


    /// <summary>
    /// Retrieves the description of an <see cref="Enum"/> value using the <see cref="DescriptionAttribute"/>.
    /// If the attribute is not present, it returns the enum value's name.
    /// </summary>
    /// <param name="value">The enum value to get the description for.</param>
    /// <returns>The description of the enum value, or its name if no description is found.</returns>
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }

    /// <summary>
    /// A HashSet containing all the non-nullable integer types in .NET.
    /// </summary>
    private static readonly HashSet<Type> numericTypes = new()
    {
        typeof(sbyte), typeof(byte), typeof(short), typeof(ushort),
        typeof(int), typeof(uint), typeof(long), typeof(ulong),
        typeof(Int16), typeof(UInt16), typeof(Int32), typeof(UInt32),
        typeof(Int64), typeof(UInt64),
        typeof(double), typeof(float), typeof(decimal)
    };

    /// <summary>
    /// Determines whether the specified <see cref="DataGridViewColumn"/> contains numeric data.
    /// This method checks the column's <see cref="DataGridViewColumn.ValueType"/> to see if it
    /// is one of the common numeric types (byte, sbyte, short, ushort, int, uint, long, ulong, float, double, decimal).
    /// </summary>
    /// <param name="column">The <see cref="DataGridViewColumn"/> to check.</param>
    /// <returns>
    /// <c>true</c> if the column's <see cref="DataGridViewColumn.ValueType"/> is a numeric type;
    /// otherwise, <c>false</c>.
    /// </returns>
    public static bool IsNumericColumn(this DataGridViewColumn? column)
    {
        if (column == null || column.ValueType == null) return false;
        Type nonNullableType = Nullable.GetUnderlyingType(column.ValueType) ?? column.ValueType;
        return numericTypes.Contains(nonNullableType);
    }

    /// <summary>
    /// Checks if a given <see cref="Type"/> represents an numeric type. This method
    /// considers both nullable (e.g., <c>int?</c>) and non-nullable (e.g., <c>int</c>)
    /// integer types.
    /// </summary>
    /// <param name="type">The <see cref="Type"/> to check.</param>
    /// <returns><c>true</c> if the <paramref name="type"/> is an numeric type (or a nullable numeric type); otherwise, <c>false</c>.</returns>
    public static bool IsNumeric(this Type? type) =>
        type is not null && numericTypes.Contains(Nullable.GetUnderlyingType(type) ?? type);

    /// <summary>
    /// A HashSet containing all the non-nullable integer types in .NET.
    /// </summary>
    private static readonly HashSet<Type> IntegerTypes = new()
    {
        typeof(sbyte), typeof(byte), typeof(short), typeof(ushort),
        typeof(int), typeof(uint), typeof(long), typeof(ulong),
        typeof(Int16), typeof(UInt16), typeof(Int32), typeof(UInt32),
        typeof(Int64), typeof(UInt64)
    };

    /// <summary>
    /// A HashSet containing all the non-nullable floating-point number types in .NET.
    /// </summary>
    private static readonly HashSet<Type> FloatingPointTypes = new()
    {
        typeof(double), typeof(float), typeof(decimal)
    };

    /// <summary>
    /// Checks if a given <see cref="Type"/> represents an integer type. This method
    /// considers both nullable (e.g., <c>int?</c>) and non-nullable (e.g., <c>int</c>)
    /// integer types.
    /// </summary>
    /// <param name="type">The <see cref="Type"/> to check.</param>
    /// <returns><c>true</c> if the <paramref name="type"/> is an integer type (or a nullable integer type); otherwise, <c>false</c>.</returns>
    public static bool IsInteger(this Type? type) =>
        type is not null && IntegerTypes.Contains(Nullable.GetUnderlyingType(type) ?? type);

    /// <summary>
    /// Checks if a given <see cref="Type"/> represents a floating-point number type.
    /// This method considers both nullable (e.g., <c>double?</c>) and non-nullable
    /// (e.g., <c>double</c>, <c>float</c>, <c>decimal</c>) floating-point types.
    /// </summary>
    /// <param name="type">The <see cref="Type"/> to check.</param>
    /// <returns><c>true</c> if the <paramref name="type"/> is a floating-point number type (or a nullable floating-point type); otherwise, <c>false</c>.</returns>
    public static bool IsDouble(this Type? type) =>
        type is not null && FloatingPointTypes.Contains(Nullable.GetUnderlyingType(type) ?? type);

    /// <summary>
    /// Converts a string value to an enum of the specified type <typeparamref name="T"/>.
    /// The comparison is case-insensitive.
    /// </summary>
    /// <typeparam name="T">The enum type to convert to. This must be a struct.</typeparam>
    /// <param name="value">The string value to parse.</param>
    /// <returns>The parsed enum value of type <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="value"/> is null or empty.</exception>
    /// <exception cref="ArgumentException">Thrown if the <paramref name="value"/> is not a valid name or value for the enum <typeparamref name="T"/>.</exception>
    public static T ToEnum<T>(this string value) where T : struct
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value), "Value cannot be null or empty.");
        return Enum.TryParse(value, out T result) ? result : throw new ArgumentException($"Invalid value for enum {typeof(T)}.");
    }

    /// <summary>
    /// Infers the "best" common type from a list of types. Prioritizes specific types (numeric, bool, DateTime)
    /// before falling back to string or a common base class.
    /// </summary>
    public static Type InferCommonType(this List<Type> types)
    {
        if (types == null || types.Count == 0) return typeof(string); // Default if no types
        if (types.Count == 1) return types[0]; // If only one type, use it

        // Remove nullables to simplify inference
        types = types.Select(t => Nullable.GetUnderlyingType(t) ?? t).Where(t => t != null).ToList();
        if (!types.Any()) return typeof(string);

        // Check for specific type categories
        if (types.All(t => t.IsInteger())) return typeof(int); // Smallest common integer for display
        if (types.All(t => t.IsInteger() || t.IsDouble() || t == typeof(decimal))) return typeof(double); // Widest common numeric
        if (types.All(t => t == typeof(bool))) return typeof(bool);
        if (types.All(t => t == typeof(DateTime))) return typeof(DateTime);
        if (types.Any(t => t == typeof(string))) return typeof(string); // If any string, fall back to string

        // Try to find a common base type for complex objects
        Type commonBase = types.First();
        foreach (var type in types.Skip(1))
        {
            while (commonBase != null && !commonBase.IsAssignableFrom(type))
            {
                commonBase = commonBase.BaseType!;
            }
            if (commonBase == null) break;
        }
        return commonBase ?? typeof(string); // Fallback to string if no common base is found
    }

    #region Generate Sql Filter String

    /// <summary>
    /// Gets a human-readable filter string for a single filter condition.
    /// </summary>
    /// <param name="columnName">The name of the column being filtered.</param>
    /// <param name="columnDataType">The data type of the column.</param>
    /// <param name="filterOperator">The filter operator (e.g., "=", "StartsWith").</param>
    /// <param name="value1">The first filter value.</param>
    /// <param name="value2">The second filter value (for operators like "Between").</param>
    /// <param name="formatValue">A boolean value indicating whether to format the filter values based on the data type.</param>
    /// <returns>A human-readable string representing the filter condition.</returns>
    public static string GetReadableFilterString(string columnName, string columnDataType, string filterOperator, string value1, string value2, bool formatValue = true)
    {
        return GetFilterString(string.Empty, columnName, columnDataType, filterOperator, value1, value2, formatValue, true);
    }

    /// <summary>
    /// Gets a human-readable filter string for a single filter condition with a specified table name.
    /// </summary>
    /// <param name="tableName">The name of the table containing the column.</param>
    /// <param name="columnName">The name of the column being filtered.</param>
    /// <param name="columnDataType">The data type of the column.</param>
    /// <param name="filterOperator">The filter operator (e.g., "=", "StartsWith").</param>
    /// <param name="value1">The first filter value.</param>
    /// <param name="value2">The second filter value (for operators like "Between").</param>
    /// <param name="formatValue">A boolean value indicating whether to format the filter values based on the data type.</param>
    /// <returns>A human-readable string representing the filter condition.</returns>
    public static string GetReadableFilterString(string tableName, string columnName, string columnDataType, string filterOperator, string value1, string value2, bool formatValue = true)
    {
        return GetFilterString(tableName, columnName, columnDataType, filterOperator, value1, value2, formatValue, true);
    }

    /// <summary>
    /// Gets a SQL filter string for a single filter condition.
    /// </summary>
    /// <param name="columnName">The name of the column being filtered.</param>
    /// <param name="columnDataType">The data type of the column.</param>
    /// <param name="filterOperator">The filter operator (e.g., "=", "StartsWith").</param>
    /// <param name="value1">The first filter value.</param>
    /// <param name="value2">The second filter value (for operators like "Between").</param>
    /// <param name="formatValue">A boolean value indicating whether to format the filter values based on the data type for SQL.</param>
    /// <returns>A SQL string representing the filter condition.</returns>
    public static string GetFilterString(string columnName, string columnDataType, string filterOperator, string value1, string value2, bool formatValue = true)
    {
        return GetFilterString(string.Empty, columnName, columnDataType, filterOperator, value1, value2, formatValue);
    }

    /// <summary>
    /// Gets a SQL filter string for a single filter condition with a specified table name.
    /// </summary>
    /// <param name="tableName">The name of the table containing the column.</param>
    /// <param name="columnName">The name of the column being filtered.</param>
    /// <param name="columnDataType">The data type of the column.</param>
    /// <param name="filterOperator">The filter operator (e.g., "=", "StartsWith").</param>
    /// <param name="value1">The first filter value.</param>
    /// <param name="value2">The second filter value (for operators like "Between").</param>
    /// <param name="formatValue">A boolean value indicating whether to format the filter values based on the data type for SQL.</param>
    /// <returns>A SQL string representing the filter condition.</returns>
    public static string GetFilterString(string tableName, string columnName, string columnDataType, string filterOperator, string value1, string value2, bool formatValue = true)
    {
        return GetFilterString(tableName, columnName, columnDataType, filterOperator, value1, value2, formatValue, false);
    }

    /// <summary>
    /// Gets a filter string (either human-readable or SQL) for a single filter condition.
    /// </summary>
    /// <param name="tableName">The name of the table containing the column.</param>
    /// <param name="columnName">The name of the column being filtered.</param>
    /// <param name="columnDataType">The data type of the column.</param>
    /// <param name="filterOperator">The filter operator (e.g., "=", "StartsWith").</param>
    /// <param name="value1">The first filter value.</param>
    /// <param name="value2">The second filter value (for operators like "Between").</param>
    /// <param name="formatValue">A boolean value indicating whether to format the filter values based on the data type.</param>
    /// <param name="readable">A boolean value indicating whether to return a human-readable string (true) or a SQL string (false).</param>
    /// <returns>A string representing the filter condition.</returns>
    public static string GetFilterString(string tableName, string columnName, string columnDataType, string filterOperator, string value1, string value2, bool formatValue, bool readable)
    {
        KryptonOutlookGridFilterOperators Operator = filterOperator.ToEnum<KryptonOutlookGridFilterOperators>();

        string op = Operator.GetDescription();
        if (readable)
            op = Operator.ToString();

        string formattedValue1 = readable ? value1 : CleanCriteriaForFilter(value1);
        string formattedValue2 = readable ? value2 : CleanCriteriaForFilter(value2);

        switch (Operator)
        {
            case KryptonOutlookGridFilterOperators.BeginsWith:
            case KryptonOutlookGridFilterOperators.NotBeginsWith:
                formattedValue1 = $"{formattedValue1}";
                break;
            case KryptonOutlookGridFilterOperators.Contains:
            case KryptonOutlookGridFilterOperators.NotContains:
                formattedValue1 = $"{formattedValue1}";
                break;
            case KryptonOutlookGridFilterOperators.EndsWith:
            case KryptonOutlookGridFilterOperators.NotEndsWith:
                formattedValue1 = $"{formattedValue1}";
                break;
            case KryptonOutlookGridFilterOperators.IsEmpty:
            case KryptonOutlookGridFilterOperators.IsNotEmpty:
                formattedValue1 = string.Empty;
                break;
            case KryptonOutlookGridFilterOperators.IsNull:
            case KryptonOutlookGridFilterOperators.IsNotNull:
                formattedValue1 = "null";
                break;
            case KryptonOutlookGridFilterOperators.In:
            case KryptonOutlookGridFilterOperators.NotIn:
                formattedValue1 = $"( {formattedValue1} )";
                break;
            default:
                break;
        }

        // Apply formatting AFTER appending %, if required
        if (formatValue)
        {
            formattedValue1 = FormatValue(columnDataType, formattedValue1);
            formattedValue2 = FormatValue(columnDataType, formattedValue2);
        }

        string column = string.IsNullOrEmpty(tableName) ? columnName : $"{tableName}.{columnName}";

        if (Operator == KryptonOutlookGridFilterOperators.Between || Operator == KryptonOutlookGridFilterOperators.NotBetween)
        {
            if (string.IsNullOrEmpty(value2.ToStringNull().Trim()))
            {
                throw new Exception("value2 is required for Between and NotBetween condition");
            }
            return $"{column} {op} {formattedValue1} AND {formattedValue2}";
        }
        else if (Operator == KryptonOutlookGridFilterOperators.IsNull || Operator == KryptonOutlookGridFilterOperators.IsNotNull)
        {
            return $"{column} {op}";
        }
        else
        {
            /*if (Operator == FilterOperators.Equals && columnDataType.Equals(typeof(Image).Name, StringComparison.OrdinalIgnoreCase) && formattedValue2 == "NOT NULL")
            {
                return $"{column} {FilterOperators.IsNotNull.GetDescription()}";
            }
            else if (Operator == FilterOperators.Equals && columnDataType.Equals(typeof(Image).Name, StringComparison.OrdinalIgnoreCase) && formattedValue2 == "NULL")
            {
                return $"{column} {FilterOperators.IsNull.GetDescription()}";
            }*/
            return $"{column} {op} {formattedValue1}";
        }
    }

    /// <summary>
    /// Formats a filter value based on its data type for use in a SQL query.
    /// </summary>
    /// <param name="dataType">The data type of the column.</param>
    /// <param name="value">The value to format.</param>
    /// <returns>The formatted value as a string.</returns>
    private static string FormatValue(string dataType, string value)
    {
        if (string.IsNullOrEmpty(value) && !dataType.Equals(typeof(string).Name, StringComparison.OrdinalIgnoreCase))
            return value;

        if (dataType.Equals(typeof(string).Name, StringComparison.OrdinalIgnoreCase))
            return $"'{value}'";
        else if (dataType.Equals(typeof(DateTime).Name, StringComparison.OrdinalIgnoreCase))
            return $"'{DateTime.Parse(value):yyyy-MM-dd}'";
        else
            return value;
    }

    /// <summary>
    /// Cleans input text for use in an 'expression' (DataColumn.Expression on msdn) or cleaning
    /// criteria for a rowfilter of a data.
    /// </summary>
    /// <param name="input">The string to clean.</param>
    /// <returns>A cleaned up version of the input string.</returns>
    public static string CleanCriteriaForFilter(string input)
    {
        string output = input.Replace("[", "[[]");
        output = output.Replace("]", "[]]");
        output = output.Replace("[[[]]", "[[]");
        output = output.Replace("*", "[*]");
        output = output.Replace("%", "[%]");
        return output;
    }

    #endregion Generate Sql Filter String

}

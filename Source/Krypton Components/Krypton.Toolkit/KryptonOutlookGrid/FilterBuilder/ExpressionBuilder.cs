using System.Linq.Expressions;

namespace Krypton.Toolkit
{
    /// <summary>
    /// <see cref="Expression{T}"/>
    /// </summary>
    internal static class ExpressionBuilder
    {

        /// <summary>
        /// Converts a list of <see cref="KryptonOutlookGridFilterField"/> objects into an <see cref="Expression{T}"/>
        /// that can be used for filtering data of type <typeparamref name="T"/>.
        /// This extension method supports hierarchical grouping of filters based on <see cref="KryptonOutlookGridFilterField.GroupInfo"/>.
        /// </summary>
        /// <typeparam name="T">The type of objects to which the expression will be applied.</typeparam>
        /// <param name="filters">The list of <see cref="KryptonOutlookGridFilterField"/> objects to convert into an expression.</param>
        /// <param name="columns">Optional: A <see cref="DataGridViewColumnCollection"/> to provide context for column names and types,
        /// used by the internal <see cref="GetExpression{T}"/> method.</param>
        /// <returns>An <see cref="Expression{T}"/> representing the combined filter logic.</returns>
        public static Expression<Func<T, bool>> ToExpression<T>(this List<KryptonOutlookGridFilterField> filters, DataGridViewColumnCollection? columns = null)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p"); // 'p' is the common alias for the root object
            Expression expression = null!;
            string lastConjunction = "";

            // First, populate SubGroups based on GroupInfo
            var tops = PopulateSubGroups(filters);
            for (int i = 0; i < tops.Count; i++)
            {
                var exp = tops[i].GetFilter<T>(parameter, columns);
                if (exp != null)
                {
                    if (lastConjunction.Equals("AND", StringComparison.InvariantCultureIgnoreCase))
                    {
                        expression = Expression.AndAlso(expression, exp);
                    }
                    else if (lastConjunction.Equals("OR", StringComparison.InvariantCultureIgnoreCase))
                    {
                        expression = Expression.OrElse(expression, exp);
                    }
                    else
                    {
                        expression = exp;
                    }
                    var last = tops[i].Last();
                    lastConjunction = !string.IsNullOrEmpty(last.GroupInfo) ? last.GroupConjunction.ToUpper() : last.ColumnConjunction.ToUpper();
                }
            }
            return Expression.Lambda<Func<T, bool>>(expression, parameter);
        }

        /// <summary>
        /// Restructures a flat list of <see cref="KryptonOutlookGridFilterField"/> objects into a hierarchical representation
        /// by populating their <see cref="KryptonOutlookGridFilterField.SubGroups"/> property based on their <see cref="KryptonOutlookGridFilterField.GroupInfo"/>.
        /// This method identifies and groups related filters, creating a tree-like structure for complex filter logic.
        /// </summary>
        /// <param name="filters">The flat list of <see cref="KryptonOutlookGridFilterField"/> objects to be organized.</param>
        /// <returns>A list of top-level filter groups, where each group might contain further subgroups.</returns>
        private static List<List<KryptonOutlookGridFilterField>> PopulateSubGroups(List<KryptonOutlookGridFilterField> filters)
        {
            List<List<KryptonOutlookGridFilterField>> topLevelFilters = [];
            List<KryptonOutlookGridFilterField> usedFilter = [];

            foreach (var filter in filters)
            {
                if (!string.IsNullOrEmpty(filter.GroupInfo))
                {
                    if (usedFilter.Any(x => x.GroupInfo == filter.GroupInfo)) continue;
                    // Get the parent group info (everything before the last dot)
                    var record = filters.Where(f => f.GroupInfo == filter.GroupInfo).ToList();
                    var parentFilter = FindInChild(topLevelFilters, filter.GroupInfo);
                    if (parentFilter != null)
                    {
                        // Add the filter as a subgroup
                        parentFilter.SubGroups = [];
                        parentFilter.SubGroups.AddRange(record);
                        usedFilter.AddRange(record);
                    }
                    else
                    {
                        topLevelFilters.Add(record);
                        usedFilter.AddRange(record);
                    }
                }
                else
                {
                    topLevelFilters.Add([filter]);
                    usedFilter.Add(filter);
                }
            }
            return topLevelFilters;
        }

        /// <summary>
        /// Recursively searches for a parent <see cref="KryptonOutlookGridFilterField"/> within a hierarchical list of filter groups
        /// based on a specified <paramref name="groupName"/>.
        /// This method is crucial for locating the correct position to insert or link subgroups.
        /// </summary>
        /// <param name="topLevelFilters">The initial list of top-level filter groups to begin the search from.</param>
        /// <param name="groupName">The full <see cref="KryptonOutlookGridFilterField.GroupInfo"/> string of the parent filter to find.
        /// This typically includes dot-separated parts representing the hierarchy (e.g., "1.1", "1.2.3").</param>
        /// <returns>The <see cref="KryptonOutlookGridFilterField"/> instance that matches the parent identified by <paramref name="groupName"/>,
        /// or <see langword="null"/> if no matching parent is found.</returns>
        private static KryptonOutlookGridFilterField FindInChild(List<List<KryptonOutlookGridFilterField>> topLevelFilters, string groupName)
        {
            string searchFor = groupName;
            string[] groupParts = groupName.Split('.');
            if (groupParts.Length > 1)
                searchFor = string.Join(".", groupParts.Take(groupParts.Length - 1));
            else
                return null!;

            string currentGroupName = searchFor;
            List<KryptonOutlookGridFilterField> filterGroup = null!;

            // Search for the top-level filter group
            while (filterGroup == null && !string.IsNullOrEmpty(currentGroupName))
            {
                filterGroup = topLevelFilters.FirstOrDefault(g => g.Any(x => x.GroupInfo == currentGroupName))!;
                if (filterGroup == null)
                {
                    groupParts = currentGroupName.Split('.')!;
                    if (groupParts.Length > 1)
                        currentGroupName = string.Join(".", groupParts.Take(groupParts.Length - 1));
                    else
                        currentGroupName = null!;
                }
            }

            // If the filterGroup is found, proceed to search inside subgroups
            if (filterGroup != null)
            {
                if (filterGroup.LastOrDefault()!.GroupInfo == searchFor)
                    return filterGroup.LastOrDefault()!;
                // Search within subgroups recursively
                return FindInSubGroups(filterGroup, groupName.Split('.'), searchFor);
            }

            return null!;
        }

        /// <summary>
        /// Recursively searches within the subgroups of a given <paramref name="filterGroup"/> for a <see cref="KryptonOutlookGridFilterField"/>
        /// whose <see cref="KryptonOutlookGridFilterField.GroupInfo"/> matches the target <paramref name="searchFor"/> string.
        /// This method navigates down the hierarchical filter structure.
        /// </summary>
        /// <param name="filterGroup">The current list of <see cref="KryptonOutlookGridFilterField"/> objects representing a group to search within.</param>
        /// <param name="remainingGroupParts">An array of strings representing the remaining parts of the <see cref="KryptonOutlookGridFilterField.GroupInfo"/>
        /// to be matched in the recursive search. This helps in navigating the hierarchy level by level.</param>
        /// <param name="searchFor">The complete <see cref="KryptonOutlookGridFilterField.GroupInfo"/> string that is being sought.</param>
        /// <returns>The <see cref="KryptonOutlookGridFilterField"/> object that matches the <paramref name="searchFor"/> GroupInfo if found within the subgroups,
        /// otherwise returns the last <see cref="KryptonOutlookGridFilterField"/> of the initial <paramref name="filterGroup"/> (which might not be the exact match but the closest found parent).</returns>
        private static KryptonOutlookGridFilterField FindInSubGroups(List<KryptonOutlookGridFilterField> filterGroup, string[] remainingGroupParts, string searchFor)
        {
            // If no more parts to search for, return the last filter
            if (remainingGroupParts.Length == 0)
                return filterGroup.LastOrDefault()!;

            // Find the subgroup that matches the next part of the group name
            string currentGroupName = string.Join(".", remainingGroupParts);
            foreach (var filter in filterGroup)
            {
                if (filter.SubGroups != null)
                {
                    // Search recursively within the subgroups
                    var subGroup = filter.SubGroups.Where(sub => sub.GroupInfo == currentGroupName).ToList();
                    if (subGroup != null && subGroup.Count > 0)
                    {
                        if (subGroup.Last().GroupInfo == searchFor)
                            return subGroup.Last();
                        else
                        {
                            var result = FindInSubGroups(subGroup.Last().SubGroups, remainingGroupParts, searchFor);  // Pass SubGroups instead of subGroup
                        }
                    }
                    else
                    {
                        // Pass the SubGroups of the matched filter, not the filter itself
                        var nextParts = remainingGroupParts.Skip(1).ToArray();
                        var result = FindInSubGroups(filter.SubGroups, nextParts, searchFor);  // Pass SubGroups instead of subGroup
                        if (result != null)
                            return result;
                    }
                }
            }

            return filterGroup.LastOrDefault()!;
        }

        /// <summary>
        /// Builds an <see cref="Expression"/> for a given list of <see cref="KryptonOutlookGridFilterField"/> objects.
        /// This is an internal recursive method used to construct the boolean logic for filtering.
        /// It iterates through the filters, combining individual filter expressions with logical AND/OR conjunctions.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be filtered.</typeparam>
        /// <param name="filters">The list of <see cref="KryptonOutlookGridFilterField"/> objects to build the expression from.</param>
        /// <param name="parameter">The <see cref="ParameterExpression"/> representing the object being filtered.</param>
        /// <param name="columns">Optional: A <see cref="DataGridViewColumnCollection"/> to provide context for column names and types,
        /// used by the internal <see cref="GetExpression{T}"/> method.</param>
        /// <returns>An <see cref="Expression"/> that represents the combined filter criteria.</returns>
        private static Expression GetFilter<T>(this List<KryptonOutlookGridFilterField> filters, Expression parameter, DataGridViewColumnCollection? columns = null)
        {
            Expression expressions = null!;
            string lastConjunction = "";

            for (int i = 0; i < filters.Count; i++)
            {
                var exp = GetExpression<T>(parameter, filters[i].ColumnName, filters[i].Operator, filters[i].Value1, filters[i].Value2, columns);
                if (exp != null)
                {
                    if (lastConjunction.Equals("AND", StringComparison.InvariantCultureIgnoreCase))
                    {
                        expressions = Expression.AndAlso(expressions, exp);
                    }
                    else if (lastConjunction.Equals("OR", StringComparison.InvariantCultureIgnoreCase))
                    {
                        expressions = Expression.OrElse(expressions, exp);
                    }
                    else
                    {
                        expressions = exp;
                    }
                    lastConjunction = filters[i].ColumnConjunction.ToUpper();
                }
                if (filters[i].SubGroups != null && filters[i].SubGroups.Count > 0)
                {
                    exp = filters[i].SubGroups.GetFilter<T>(parameter, columns);
                    if (lastConjunction.Equals("AND", StringComparison.InvariantCultureIgnoreCase))
                    {
                        expressions = Expression.AndAlso(expressions, exp);
                    }
                    else if (lastConjunction.Equals("OR", StringComparison.InvariantCultureIgnoreCase))
                    {
                        expressions = Expression.OrElse(expressions, exp);
                    }
                    else
                    {
                        expressions = exp;
                    }
                    lastConjunction = filters[i].GroupConjunction.ToUpper();
                }
            }
            return expressions;
        }

        /// <summary>
        /// Dynamically constructs an <see cref="Expression"/> representing a filter condition based on a property, operator, and value(s).
        /// This method supports various comparison, string, and range operators, and can handle filtering for both
        /// standard object properties and <see cref="DataGridViewRow"/>/<see cref="OutlookGridRow"/> cell values.
        /// </summary>
        /// <typeparam name="T">The type of object being filtered (e.g., a custom data model or <see cref="DataGridViewRow"/>).</typeparam>
        /// <param name="parameter">The <see cref="Expression"/> representing the parameter of the lambda expression (e.g., 'p' in 'p => p.Property').</param>
        /// <param name="property">The name of the property or column to apply the filter to.</param>
        /// <param name="op">The string representation of the operator (e.g., "=", "LIKE", "BETWEEN").</param>
        /// <param name="valueString">The string representation of the value to compare against for single-value operators, or the first value for range operators.</param>
        /// <param name="value2String">The string representation of the second value for range operators (e.g., "BETWEEN"). Defaults to null.</param>
        /// <param name="columns">Optional: A <see cref="DataGridViewColumnCollection"/> providing context for column types when filtering <see cref="DataGridViewRow"/> or <see cref="OutlookGridRow"/>.</param>
        /// <returns>An <see cref="Expression"/> representing the specified filter condition.</returns>
        /// <exception cref="InvalidOperationException">Thrown if filtering a <see cref="DataGridViewRow"/> or <see cref="OutlookGridRow"/> without providing a <paramref name="columns"/> collection.</exception>
        /// <exception cref="FormatException">Thrown if an unsupported operator is provided.</exception>
        public static Expression GetExpression<T>(Expression parameter, string property, string op, string valueString, string value2String = null!, DataGridViewColumnCollection? columns = null)
        {
            Expression left;
            Type? targetPropertyType;
            if (typeof(T) == typeof(DataGridViewRow) || typeof(T) == typeof(OutlookGridRow))
            {
                if (columns == null)
                {
                    throw new InvalidOperationException("A DataGridViewColumnCollection must be provided for row filtering.");
                }
                left = BuildDataGridViewCellExpression(parameter, property, columns);
                // Get the actual type of the property for better parsing hints
                targetPropertyType = columns[property]!.ValueType == null ? typeof(string) : columns[property]!.ValueType;
            }
            else
            {
                left = BuildPropertyExpression<T>(parameter, property);
                // Get the actual type of the property for better parsing hints
                targetPropertyType = (left as MemberExpression)?.Member is PropertyInfo propInfo ? propInfo.PropertyType : null;
                if (targetPropertyType == null && (left as MemberExpression)?.Member is FieldInfo fieldInfo)
                {
                    targetPropertyType = fieldInfo.FieldType;
                }
            }

            Expression expression = op.ToUpperInvariant() switch
            {
                "=" or "==" or "<>" or "!=" or "<" or ">" or "<=" or ">=" => BuildComparison(left, op, valueString, targetPropertyType),
                "IN" => BuildIn(left, valueString, targetPropertyType),
                "NOT IN" => Expression.Not(BuildIn(left, valueString, targetPropertyType)),
                "LIKE" => BuildLikeExpression(left, valueString, targetPropertyType),
                "NOT LIKE" => Expression.Not(BuildLikeExpression(left, valueString, targetPropertyType)),
                "BEGINSWITH" => BuildBeginsWithExpression(left, valueString, targetPropertyType),
                "NOT BEGINSWITH" => Expression.Not(BuildBeginsWithExpression(left, valueString, targetPropertyType)),
                "ENDSWITH" => BuildEndsWithExpression(left, valueString, targetPropertyType),
                "NOT ENDSWITH" => Expression.Not(BuildEndsWithExpression(left, valueString, targetPropertyType)),
                "BETWEEN" => BuildBetween(left, valueString, value2String, targetPropertyType),
                "NOT BETWEEN" => Expression.Not(BuildBetween(left, valueString, value2String, targetPropertyType)),
                "IS NULL" => Expression.Equal(left, Expression.Constant(null, typeof(object))),
                "IS NOT NULL" => Expression.NotEqual(left, Expression.Constant(null, typeof(object))),
                "TRUE" => BuildComparison(left, "=", true.ToString(), targetPropertyType),
                "FALSE" => BuildComparison(left, "=", false.ToString(), targetPropertyType),
                _ => throw new FormatException($"Unsupported operator '{op}'."),
            };
            return expression;
        }

        /// <summary>
        /// Dynamically constructs an <see cref="Expression"/> that represents accessing a property (or nested properties)
        /// or a field on an object of type <typeparamref name="T"/>. It supports dot-separated property paths for nested access.
        /// </summary>
        /// <typeparam name="T">The type of the object from which the property/field will be accessed.</typeparam>
        /// <param name="parameter">The <see cref="Expression"/> representing the parameter of the lambda expression
        /// (e.g., 'p' in 'p => p.SomeProperty').</param>
        /// <param name="propertyPath">The dot-separated path to the property or field (e.g., "Name", "Address.City").</param>
        /// <returns>An <see cref="Expression"/> representing the property or field access.</returns>
        /// <exception cref="ArgumentException">Thrown if any part of the <paramref name="propertyPath"/>
        /// does not correspond to a public property or field on the respective type.</exception>
        private static Expression BuildPropertyExpression<T>(Expression parameter, string propertyPath)
        {
            Expression current = parameter;
            Type currentType = typeof(T);

            string[] parts = propertyPath.Split('.');

            foreach (var part in parts)
            {
                PropertyInfo? prop = currentType.GetProperty(part, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                FieldInfo? field = null;

                if (prop != null)
                {
                    current = Expression.Property(current, prop);
                    currentType = prop.PropertyType;
                }
                else
                {
                    field = currentType.GetField(part, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                    if (field != null)
                    {
                        current = Expression.Field(current, field);
                        currentType = field.FieldType;
                    }
                    else
                    {
                        throw new ArgumentException($"Property or field '{part}' not found on type '{currentType.Name}' for path '{propertyPath}'.");
                    }
                }
            }
            return current;
        }

        /// <summary>
        /// Dynamically constructs an <see cref="Expression"/> to access the <see cref="DataGridViewCell.Value"/>
        /// of a specific column within a <see cref="DataGridViewRow"/> or <see cref="OutlookGridRow"/>.
        /// </summary>
        /// <param name="parameter">The <see cref="Expression"/> representing the <see cref="DataGridViewRow"/>
        /// or <see cref="OutlookGridRow"/> instance.</param>
        /// <param name="columnName">The name of the column whose cell value is to be accessed.</param>
        /// <param name="columns">The <see cref="DataGridViewColumnCollection"/> providing access to column metadata,
        /// specifically the index of the column.</param>
        /// <returns>An <see cref="Expression"/> representing the access to the cell's value.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the indexer for <see cref="DataGridViewCellCollection"/>
        /// cannot be found or if <paramref name="columns"/> is null or the column name does not exist.</exception>
        private static Expression BuildDataGridViewCellExpression(Expression parameter, string columnName, DataGridViewColumnCollection? columns)
        {
            // row.Cells
            Expression cellsProperty = Expression.Property(parameter, "Cells");

            // Use GetMethod for the indexer property: public DataGridViewCell this[int columnIndex] { get; }
            var index = columns![columnName]!.Index;
            MethodInfo? cellsIndexer = typeof(DataGridViewCellCollection).GetMethod("get_Item", new Type[] { typeof(int) });
            if (cellsIndexer == null)
            {
                throw new InvalidOperationException($"Could not find indexer for DataGridViewCellCollection[string].");
            }
            Expression cellAccess = Expression.Call(cellsProperty, cellsIndexer, Expression.Constant(index));

            // row.Cells["ColumnName"].Value
            Expression valueProperty = Expression.Property(cellAccess, "Value");

            return valueProperty;
        }

        /// <summary>
        /// Constructs an <see cref="Expression"/> for a comparison operation (e.g., equals, less than, greater than).
        /// This method acts as a wrapper for the actual comparison logic, deferring to <c>BuildComparisonExpression</c>.
        /// </summary>
        /// <param name="left">The <see cref="Expression"/> representing the left-hand side of the comparison (the property/field value).</param>
        /// <param name="op">The string representation of the comparison operator (e.g., "=", "&lt;", ">").</param>
        /// <param name="valueString">The string representation of the value to compare against.</param>
        /// <param name="targetPropertyType">The <see cref="Type"/> of the property or field being compared, used for type conversion.</param>
        /// <returns>An <see cref="Expression"/> representing the comparison operation.</returns>
        private static Expression BuildComparison(Expression left, string op, string valueString, Type? targetPropertyType)
        {
            return BuildComparisonExpression(left, op, valueString, targetPropertyType);
        }

        /// <summary>
        /// Constructs an <see cref="Expression"/> for an "IN" clause operation, checking if a property's value
        /// is present within a given list of values.
        /// This method acts as a wrapper for the actual "IN" logic, deferring to <c>BuildInExpression</c>.
        /// </summary>
        /// <param name="left">The <see cref="Expression"/> representing the property/field value to check.</param>
        /// <param name="values">A comma-separated string of values to check against (e.g., "'value1', 'value2'").</param>
        /// <param name="targetPropertyType">The <see cref="Type"/> of the property or field, used for type conversion of values.</param>
        /// <returns>An <see cref="Expression"/> representing the "IN" operation.</returns>
        private static Expression BuildIn(Expression left, string values, Type? targetPropertyType)
        {
            var lst = values.Split([','], StringSplitOptions.RemoveEmptyEntries)
                .Select(v => v.Trim().Trim('\''))
                .ToList();
            return BuildInExpression(left, lst, targetPropertyType);
        }

        /// <summary>
        /// Constructs an <see cref="Expression"/> for a "BETWEEN" operation, checking if a property's value
        /// falls within a specified range (inclusive).
        /// This method acts as a wrapper for the actual "BETWEEN" logic, deferring to <c>BuildBetweenExpression</c>.
        /// </summary>
        /// <param name="left">The <see cref="Expression"/> representing the property/field value to check.</param>
        /// <param name="value1String">The string representation of the lower bound of the range.</param>
        /// <param name="value2String">The string representation of the upper bound of the range.</param>
        /// <param name="targetPropertyType">The <see cref="Type"/> of the property or field, used for type conversion of values.</param>
        /// <returns>An <see cref="Expression"/> representing the "BETWEEN" operation.</returns>
        private static Expression BuildBetween(Expression left, string value1String, string value2String, Type? targetPropertyType)
        {
            // The 'andToken' is consumed by the Expect method, just passed here for signature consistency
            return BuildBetweenExpression(left, value1String, value2String, targetPropertyType);
        }

        /// <summary>
        /// Builds a comparison expression (e.g., A = B, A > B).
        /// Handles type conversion and null-safety for left-hand side (LHS) and right-hand side (RHS) values.
        /// </summary>
        /// <param name="left">The left-hand side expression (e.g., property access like `p.Name` or `p.Cells[index].FormattedValue`).</param>
        /// <param name="op">The comparison operator (e.g., "=", "!=", "&lt;", ">").</param>
        /// <param name="valueString">The string representation of the right-hand side value.</param>
        /// <param name="targetPropertyType">NEW: The actual .NET type of the property being compared (e.g., int, decimal, string). Can be null if unknown or object.</param>
        /// <returns>An Expression representing the comparison.</returns>
        public static Expression BuildComparisonExpression(Expression left, string op, string valueString, Type? targetPropertyType)
        {
            // Step 1: Parse the valueString into a constant, inferring its most likely type.
            // Prioritize targetPropertyType for parsing if it's a concrete type.
            Type? inferredValueType = null;
            object? parsedValue = null;

            // Determine the effective target type for conversion. If targetPropertyType is null or object,
            // we'll still try to infer from the valueString, but if it's a concrete type, use that.
            Type effectiveTargetType = targetPropertyType ?? typeof(object); // Start with property type hint

            if (string.Equals(valueString, "NULL", StringComparison.OrdinalIgnoreCase))
            {
                parsedValue = null;
                // For NULL, the inferredValueType (and commonType) will often be the nullable version of the property type.
                inferredValueType = effectiveTargetType.IsValueType ? typeof(Nullable<>).MakeGenericType(effectiveTargetType) : effectiveTargetType;
                if (inferredValueType == typeof(object)) inferredValueType = null; // Don't force object if original was object
            }
            else if ((valueString.StartsWith("'") && valueString.EndsWith("'")) || (valueString.StartsWith("\"") && valueString.EndsWith("\"")))
            {
                parsedValue = valueString.Trim('\'', '"');
                inferredValueType = typeof(string);
            }
            // NEW: Try parsing based on effectiveTargetType first for numeric/bool/date/guid if not string.
            // This makes parsing more robust to actual property type.
            else if (effectiveTargetType != typeof(object) && effectiveTargetType != typeof(string))
            {
                Type nonNullableEffectiveTargetType = Nullable.GetUnderlyingType(effectiveTargetType) ?? effectiveTargetType;

                if (nonNullableEffectiveTargetType.IsInteger())
                {
                    parsedValue = valueString.ToInteger();
                    inferredValueType = typeof(int);
                }
                else if (nonNullableEffectiveTargetType.IsDouble())
                {
                    parsedValue = valueString.ToDouble();
                    inferredValueType = typeof(double);
                }
                else if (nonNullableEffectiveTargetType == typeof(bool) && bool.TryParse(valueString, out bool boolVal))
                {
                    parsedValue = boolVal;
                    inferredValueType = typeof(bool);
                }
                else if (nonNullableEffectiveTargetType == typeof(DateTime) && DateTime.TryParse(valueString, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dtVal))
                {
                    parsedValue = dtVal;
                    inferredValueType = typeof(DateTime);
                }
                else if (nonNullableEffectiveTargetType == typeof(Guid) && Guid.TryParse(valueString, out Guid guidVal))
                {
                    parsedValue = guidVal;
                    inferredValueType = typeof(Guid);
                }
                // If explicit type parsing failed, fall through to generic inference.
            }
            // Fallback to generic inference if explicit type parsing didn't happen or failed for targetPropertyType
            if (parsedValue == null && inferredValueType == null) // Only try generic parsing if not already parsed
            {
                if (int.TryParse(valueString, NumberStyles.Integer, CultureInfo.InvariantCulture, out int intVal))
                {
                    parsedValue = intVal;
                    inferredValueType = typeof(int);
                }
                else if (long.TryParse(valueString, NumberStyles.Integer, CultureInfo.InvariantCulture, out long longVal))
                {
                    parsedValue = longVal;
                    inferredValueType = typeof(long);
                }
                else if (double.TryParse(valueString, NumberStyles.Float, CultureInfo.InvariantCulture, out double doubleVal))
                {
                    parsedValue = doubleVal;
                    inferredValueType = typeof(double);
                }
                else if (bool.TryParse(valueString, out bool boolVal))
                {
                    parsedValue = boolVal;
                    inferredValueType = typeof(bool);
                }
                else if (DateTime.TryParse(valueString, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dtVal))
                {
                    parsedValue = dtVal;
                    inferredValueType = typeof(DateTime);
                }
                else if (Guid.TryParse(valueString, out Guid guidVal))
                {
                    parsedValue = guidVal;
                    inferredValueType = typeof(Guid);
                }
                else
                {
                    // Fallback for unquoted literals that are not known primitives. Treat as string.
                    parsedValue = valueString;
                    inferredValueType = typeof(string);
                }
            }


            // Determine the common type for the comparison.
            // Prioritize the targetPropertyType if it's not object AND can be assigned from inferredValueType.
            Type commonType;
            if (targetPropertyType != null && targetPropertyType != typeof(object) && inferredValueType != null &&
                (targetPropertyType.IsAssignableFrom(inferredValueType) ||
                 (inferredValueType.IsValueType && Nullable.GetUnderlyingType(targetPropertyType) == inferredValueType) ||
                 (targetPropertyType.IsValueType && targetPropertyType == Nullable.GetUnderlyingType(inferredValueType)) ||
                 targetPropertyType.IsNumeric() && inferredValueType.IsNumeric())) // Allow numeric type promotion
            {
                commonType = targetPropertyType;
            }
            else if (inferredValueType != null)
            {
                commonType = inferredValueType;
            }
            else // Both targetPropertyType and inferredValueType are effectively unknown/null
            {
                commonType = typeof(object);
            }

            // Handle nullable types for commonType if the property itself is nullable or if null is parsed
            if (parsedValue == null && commonType.IsValueType && Nullable.GetUnderlyingType(commonType) == null)
            {
                commonType = typeof(Nullable<>).MakeGenericType(commonType);
            }
            else if (left.Type.IsGenericType && left.Type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // If left is nullable, ensure commonType can accommodate nulls (make it nullable if value type)
                if (commonType.IsValueType && Nullable.GetUnderlyingType(commonType) == null)
                {
                    commonType = typeof(Nullable<>).MakeGenericType(commonType);
                }
            }


            // Convert left-hand side to a suitable type for comparison.
            // This block needs to be careful when left.Type is 'object' (e.g., FormattedValue)
            // and we want to convert it to a specific numeric/date type inferred from the literal.
            Expression finalLeft = left;

            // If 'left' expression's result is 'object' (e.g., Dictionary value or FormattedValue)
            // and 'commonType' is a concrete type (not object), we need a robust conversion.
            if (finalLeft.Type == typeof(object) && commonType != typeof(object))
            {
                // If the commonType is string, just convert to string using ToStringNull
                if (commonType == typeof(string))
                {
                    MethodInfo? toStringNullMethod = typeof(HelperExtensions).GetMethod(nameof(HelperExtensions.ToStringNull), BindingFlags.Static | BindingFlags.Public);
                    if (toStringNullMethod == null) throw new InvalidOperationException($"Could not find '{nameof(HelperExtensions.ToStringNull)}' extension method.");
                    finalLeft = Expression.Call(toStringNullMethod, Expression.Convert(left, typeof(object)));
                }
                else // Convert to a specific non-string type (int, double, DateTime etc.)
                {
                    // First, ensure the object value is not null, or handle it as null for value types.
                    // If it's a value type, we convert to its nullable version for direct comparison.
                    Type nonNullableCommonType = Nullable.GetUnderlyingType(commonType) ?? commonType;

                    // Call a generic To<Type> method from HelperExtensions if available
                    // E.g., HelperExtensions.ToInteger(obj), HelperExtensions.ToDouble(obj)
                    MethodInfo? conversionMethod = null;
                    if (nonNullableCommonType == typeof(int)) conversionMethod = typeof(HelperExtensions).GetMethod(nameof(HelperExtensions.ToInteger), BindingFlags.Static | BindingFlags.Public);
                    else if (nonNullableCommonType == typeof(long)) conversionMethod = typeof(HelperExtensions).GetMethod(nameof(HelperExtensions.ToLong), BindingFlags.Static | BindingFlags.Public);
                    else if (nonNullableCommonType == typeof(double)) conversionMethod = typeof(HelperExtensions).GetMethod(nameof(HelperExtensions.ToDouble), BindingFlags.Static | BindingFlags.Public);
                    else if (nonNullableCommonType == typeof(decimal)) conversionMethod = typeof(HelperExtensions).GetMethod(nameof(HelperExtensions.ToDecimal), BindingFlags.Static | BindingFlags.Public);
                    else if (nonNullableCommonType == typeof(bool)) conversionMethod = typeof(HelperExtensions).GetMethod(nameof(HelperExtensions.ToBoolean), BindingFlags.Static | BindingFlags.Public);
                    else if (nonNullableCommonType == typeof(DateTime)) conversionMethod = typeof(HelperExtensions).GetMethod(nameof(HelperExtensions.ToDateTime), BindingFlags.Static | BindingFlags.Public);
                    else if (nonNullableCommonType == typeof(Guid)) conversionMethod = typeof(HelperExtensions).GetMethod(nameof(HelperExtensions.ToGuid), BindingFlags.Static | BindingFlags.Public);


                    if (conversionMethod != null)
                    {
                        // Call HelperExtensions.ToX(originalLeft)
                        finalLeft = Expression.Call(conversionMethod, Expression.Convert(left, typeof(object)));
                        // Ensure the result is cast to the commonType, especially if it's nullable
                        finalLeft = Expression.Convert(finalLeft, commonType);
                    }
                    else // Fallback to Convert.ChangeType for other conversions from object
                    {
                        MethodInfo changeTypeMethod = typeof(Convert).GetMethod(nameof(Convert.ChangeType), new Type[] { typeof(object), typeof(Type), typeof(IFormatProvider) })!;
                        finalLeft = Expression.Call(changeTypeMethod, Expression.Convert(left, typeof(object)), Expression.Constant(nonNullableCommonType), Expression.Constant(CultureInfo.InvariantCulture));
                        finalLeft = Expression.Convert(finalLeft, commonType); // Cast result to commonType (might be nullable)
                    }
                }
            }
            else if (finalLeft.Type != commonType) // If left is not 'object' but types mismatch (e.g., int property vs decimal literal)
            {
                // Allow implicit numeric conversions (e.g., int to double) or explicit casting if compatible
                try
                {
                    // Ensure nullable conversions are handled (e.g., int to int?, int? to int)
                    Type underlyingLeftType = Nullable.GetUnderlyingType(finalLeft.Type) ?? finalLeft.Type;
                    Type underlyingCommonType = Nullable.GetUnderlyingType(commonType) ?? commonType;

                    if (underlyingLeftType != underlyingCommonType && underlyingLeftType.IsNumeric() && underlyingCommonType.IsNumeric())
                    {
                        // If both are numeric and different, promote to common numeric type
                        Type commonNumericType = GetCommonNumericType(underlyingLeftType, underlyingCommonType);
                        finalLeft = Expression.Convert(finalLeft, commonNumericType);
                        commonType = commonNumericType; // Update commonType as it might have been promoted
                    }
                    else if (underlyingCommonType == typeof(string)) // If target is string, convert current left to string
                    {
                        MethodInfo? toStringNullMethod = typeof(HelperExtensions).GetMethod(nameof(HelperExtensions.ToStringNull), BindingFlags.Static | BindingFlags.Public);
                        if (toStringNullMethod == null) throw new InvalidOperationException($"Could not find '{nameof(HelperExtensions.ToStringNull)}' extension method.");
                        finalLeft = Expression.Call(toStringNullMethod, Expression.Convert(finalLeft, typeof(object)));
                    }
                    else // Try direct conversion (e.g., int to long, or explicit if non-nullable to nullable)
                    {
                        finalLeft = Expression.Convert(finalLeft, commonType);
                    }
                }
                catch (InvalidOperationException ex)
                {
                    throw new InvalidOperationException($"Cannot convert left-hand side type '{finalLeft.Type.Name}' to common type '{commonType.Name}' for comparison.", ex);
                }
            }

            // Step 2: Create the right-hand side constant expression.
            // Ensure the constant's type matches the commonType, converting parsedValue if necessary.
            Expression finalRight;
            try
            {
                if (parsedValue == null)
                {
                    finalRight = Expression.Constant(null, commonType); // Use commonType for null constants
                }
                else if (parsedValue.GetType() == commonType)
                {
                    finalRight = Expression.Constant(parsedValue, commonType);
                }
                else
                {
                    // If parsedValue's type doesn't match commonType exactly, try Convert.ChangeType or direct cast
                    object convertedRightValue = Convert.ChangeType(parsedValue, Nullable.GetUnderlyingType(commonType) ?? commonType, CultureInfo.InvariantCulture);
                    finalRight = Expression.Constant(convertedRightValue, commonType);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Cannot convert right-hand side value '{valueString}' (parsed as '{parsedValue?.GetType().Name ?? "null"}') to common type '{commonType.Name}'.", ex);
            }

            // Step 3: Perform the actual comparison.
            if (parsedValue == null)
            {
                // Null comparison already uses commonType
                if (op == "=" || op == "==")
                {
                    return Expression.Equal(finalLeft, finalRight);
                }
                else if (op == "<>" || op == "!=")
                {
                    return Expression.NotEqual(finalLeft, finalRight);
                }
                else
                {
                    throw new NotSupportedException($"Operator '{op}' is not supported with NULL comparisons (only '=', '==', '<>', '!=' are).");
                }
            }

            // Standard comparison operators
            return op switch
            {
                "=" or "==" => Expression.Equal(finalLeft, finalRight),
                "<>" or "!=" => Expression.NotEqual(finalLeft, finalRight),
                "<" => Expression.LessThan(finalLeft, finalRight),
                ">" => Expression.GreaterThan(finalLeft, finalRight),
                "<=" => Expression.LessThanOrEqual(finalLeft, finalRight),
                ">=" => Expression.GreaterThanOrEqual(finalLeft, finalRight),
                _ => throw new NotSupportedException($"Comparison operator '{op}' not supported for non-NULL values."),
            };
        }

        /// <summary>
        /// Builds an expression for the LIKE operator using String.Contains.
        /// </summary>
        public static Expression BuildLikeExpression(Expression left, string value, Type? targetPropertyType)
        {
            // Always convert LHS to string for LIKE operations using ToStringNull for safety.
            MethodInfo? toStringNullMethod = typeof(HelperExtensions).GetMethod(nameof(HelperExtensions.ToStringNull), BindingFlags.Static | BindingFlags.Public);
            if (toStringNullMethod == null)
            {
                throw new InvalidOperationException($"Could not find '{nameof(HelperExtensions.ToStringNull)}' extension method. " +
                                                    "Ensure 'HelperExtensions' class is public and method is static public in an accessible namespace.");
            }
            left = Expression.Call(toStringNullMethod, Expression.Convert(left, typeof(object))); // `left` is now a `string` expression.

            Expression constant = Expression.Constant(value, typeof(string));
            // For case-insensitive LIKE, you would call ToLower() on both sides:
            return Expression.Call(
                Expression.Call(left, typeof(string).GetMethod("ToLower", Type.EmptyTypes)!),
                typeof(string).GetMethod("Contains", new[] { typeof(string) })!,
                Expression.Call(constant, typeof(string).GetMethod("ToLower", Type.EmptyTypes)!));
        }

        /// <summary>
        /// Builds an expression for the IN operator using Enumerable.Contains.
        /// </summary>
        public static Expression BuildInExpression(Expression left, List<string> values, Type? targetPropertyType)
        {
            // Determine the type for the comparison, prioritizing targetPropertyType if available
            Type commonType = targetPropertyType ?? typeof(object); // Start with property type hint

            // If targetPropertyType is object, or not explicitly provided, try to infer from values.
            if (commonType == typeof(object) && values.Any())
            {
                // Try to infer from the first non-null, non-empty value
                string? firstRealValue = values.FirstOrDefault(v => !string.IsNullOrWhiteSpace(v) && !string.Equals(v, "NULL", StringComparison.OrdinalIgnoreCase));
                if (firstRealValue != null)
                {
                    if (firstRealValue.StartsWith("'") || firstRealValue.StartsWith("\"")) commonType = typeof(string);
                    else if (int.TryParse(firstRealValue, out _)) commonType = typeof(int);
                    else if (long.TryParse(firstRealValue, out _)) commonType = typeof(long); // Added long inference
                    else if (double.TryParse(firstRealValue, out _)) commonType = typeof(double);
                    else if (decimal.TryParse(firstRealValue, out _)) commonType = typeof(decimal); // Added decimal inference
                    else if (bool.TryParse(firstRealValue, out _)) commonType = typeof(bool);
                    else if (DateTime.TryParse(firstRealValue, out _)) commonType = typeof(DateTime);
                    else if (Guid.TryParse(firstRealValue, out _)) commonType = typeof(Guid);
                    else commonType = typeof(string); // Fallback for unquoted literals
                }
            }
            // Ensure commonType is nullable if any value is NULL and it's a value type
            if (values.Any(v => string.Equals(v, "NULL", StringComparison.OrdinalIgnoreCase)) && commonType.IsValueType && Nullable.GetUnderlyingType(commonType) == null)
            {
                commonType = typeof(Nullable<>).MakeGenericType(commonType);
            }

            // Convert all list values to the determined commonType
            var convertedValues = values.Select(v => ConvertStringToType(v, commonType)).ToList();

            Expression finalLeft = left;

            // Apply robust conversion to finalLeft to match commonType
            finalLeft = ConvertExpressionToTargetType(left, commonType); // NEW Helper method

            // Create a constant list of the correct type
            var constantCollection = Expression.Constant(convertedValues, typeof(List<>).MakeGenericType(commonType));

            // Get the Enumerable.Contains method using reflection
            var containsMethod = typeof(Enumerable).GetMethods(BindingFlags.Static | BindingFlags.Public)
                                                    .Where(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                                                    .Single()
                                                    .MakeGenericMethod(commonType); // Use commonType for generic method

            return Expression.Call(null, containsMethod, constantCollection, finalLeft);
        }

        /// <summary>
        /// Builds an expression for the BETWEEN operator.
        /// </summary>
        public static Expression BuildBetweenExpression(Expression left, string value1String, string value2String, Type? targetPropertyType)
        {
            // Determine the common type for comparison, prioritizing targetPropertyType
            Type commonType = targetPropertyType ?? typeof(object); // Start with property type hint

            // If commonType is object, or not explicitly provided, try to infer from values.
            if (commonType == typeof(object))
            {
                // Attempt to infer type based on value1String (could improve by checking both)
                if (value1String.StartsWith("'") || value1String.StartsWith("\"")) commonType = typeof(string);
                else if (int.TryParse(value1String, out _)) commonType = typeof(int);
                else if (long.TryParse(value1String, out _)) commonType = typeof(long); // Added long inference
                else if (double.TryParse(value1String, out _)) commonType = typeof(double);
                else if (decimal.TryParse(value1String, out _)) commonType = typeof(decimal); // Added decimal inference
                else if (bool.TryParse(value1String, out _)) commonType = typeof(bool);
                else if (DateTime.TryParse(value1String, out _)) commonType = typeof(DateTime);
                else if (Guid.TryParse(value1String, out _)) commonType = typeof(Guid);
                else commonType = typeof(string); // Fallback for unquoted literals
            }
            // Ensure commonType is nullable if any value is NULL and it's a value type
            if ((string.Equals(value1String, "NULL", StringComparison.OrdinalIgnoreCase) || string.Equals(value2String, "NULL", StringComparison.OrdinalIgnoreCase)) && commonType.IsValueType && Nullable.GetUnderlyingType(commonType) == null)
            {
                commonType = typeof(Nullable<>).MakeGenericType(commonType);
            }

            // Convert values to the determined commonType
            object? val1 = ConvertStringToType(value1String, commonType);
            object? val2 = ConvertStringToType(value2String, commonType);

            Expression const1 = Expression.Constant(val1, commonType); // Use commonType for constant
            Expression const2 = Expression.Constant(val2, commonType); // Use commonType for constant

            Expression finalLeft = left;

            // Apply robust conversion to finalLeft to match commonType
            finalLeft = ConvertExpressionToTargetType(left, commonType); // NEW Helper method

            var greaterThanOrEqual = Expression.GreaterThanOrEqual(finalLeft, const1);
            var lessThanOrEqual = Expression.LessThanOrEqual(finalLeft, const2);

            return Expression.AndAlso(greaterThanOrEqual, lessThanOrEqual);
        }

        /// <summary>
        /// Builds an expression for the BEGINSWITH operator using String.StartsWith.
        /// </summary>
        public static Expression BuildBeginsWithExpression(Expression left, string value, Type? targetPropertyType)
        {
            // Always convert LHS to string for string operations using ToStringNull for safety.
            MethodInfo? toStringNullMethod = typeof(HelperExtensions).GetMethod(nameof(HelperExtensions.ToStringNull), BindingFlags.Static | BindingFlags.Public);
            if (toStringNullMethod == null)
            {
                throw new InvalidOperationException($"Could not find '{nameof(HelperExtensions.ToStringNull)}' extension method.");
            }
            left = Expression.Call(toStringNullMethod, Expression.Convert(left, typeof(object)));

            MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string), typeof(StringComparison) })!;
            if (startsWithMethod == null)
            {
                throw new InvalidOperationException("Could not find String.StartsWith method with StringComparison overload.");
            }

            return Expression.Call(
                left,
                startsWithMethod,
                Expression.Constant(value, typeof(string)),
                Expression.Constant(StringComparison.OrdinalIgnoreCase) // Case-insensitive comparison
            );
        }

        /// <summary>
        /// Builds an expression for the ENDSWITH operator using String.EndsWith.
        /// </summary>
        public static Expression BuildEndsWithExpression(Expression left, string value, Type? targetPropertyType)
        {
            // Always convert LHS to string for string operations using ToStringNull for safety.
            MethodInfo? toStringNullMethod = typeof(HelperExtensions).GetMethod(nameof(HelperExtensions.ToStringNull), BindingFlags.Static | BindingFlags.Public);
            if (toStringNullMethod == null)
            {
                throw new InvalidOperationException($"Could not find '{nameof(HelperExtensions.ToStringNull)}' extension method.");
            }
            left = Expression.Call(toStringNullMethod, Expression.Convert(left, typeof(object)));

            MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string), typeof(StringComparison) })!;
            if (endsWithMethod == null)
            {
                throw new InvalidOperationException("Could not find String.EndsWith method with StringComparison overload.");
            }

            return Expression.Call(
                left,
                endsWithMethod,
                Expression.Constant(value, typeof(string)),
                Expression.Constant(StringComparison.OrdinalIgnoreCase) // Case-insensitive comparison
            );
        }

        /// <summary>
        /// Helper method to robustly convert an Expression to a target Type.
        /// This is useful for handling conversions from 'object' (e.g., DataGridViewCell.FormattedValue)
        /// to specific target types, or for numeric promotions.
        /// </summary>
        public static Expression ConvertExpressionToTargetType(Expression expression, Type targetType)
        {
            if (expression.Type == targetType) return expression; // No conversion needed

            Type nonNullableTargetType = Nullable.GetUnderlyingType(targetType) ?? targetType;

            // If the expression's result is 'object' (e.g., Dictionary value or FormattedValue)
            // and 'targetType' is a concrete type (not object), we need a robust conversion.
            if (expression.Type == typeof(object) && targetType != typeof(object))
            {
                // If the targetType is string, just convert to string using ToStringNull
                if (targetType == typeof(string))
                {
                    MethodInfo? toStringNullMethod = typeof(HelperExtensions).GetMethod(nameof(HelperExtensions.ToStringNull), BindingFlags.Static | BindingFlags.Public);
                    if (toStringNullMethod == null) throw new InvalidOperationException($"Could not find '{nameof(HelperExtensions.ToStringNull)}' extension method.");
                    return Expression.Call(toStringNullMethod, Expression.Convert(expression, typeof(object)));
                }
                else // Convert to a specific non-string type (int, double, DateTime etc.)
                {
                    MethodInfo? conversionMethod = null;
                    // Look for direct HelperExtensions.ToX methods
                    if (nonNullableTargetType == typeof(int)) conversionMethod = typeof(HelperExtensions).GetMethod(nameof(HelperExtensions.ToInteger), BindingFlags.Static | BindingFlags.Public);
                    else if (nonNullableTargetType == typeof(long)) conversionMethod = typeof(HelperExtensions).GetMethod(nameof(HelperExtensions.ToLong), BindingFlags.Static | BindingFlags.Public);
                    else if (nonNullableTargetType == typeof(double)) conversionMethod = typeof(HelperExtensions).GetMethod(nameof(HelperExtensions.ToDouble), BindingFlags.Static | BindingFlags.Public);
                    else if (nonNullableTargetType == typeof(decimal)) conversionMethod = typeof(HelperExtensions).GetMethod(nameof(HelperExtensions.ToDecimal), BindingFlags.Static | BindingFlags.Public);
                    else if (nonNullableTargetType == typeof(bool)) conversionMethod = typeof(HelperExtensions).GetMethod(nameof(HelperExtensions.ToBoolean), BindingFlags.Static | BindingFlags.Public);
                    else if (nonNullableTargetType == typeof(DateTime)) conversionMethod = typeof(HelperExtensions).GetMethod(nameof(HelperExtensions.ToDateTime), BindingFlags.Static | BindingFlags.Public);
                    else if (nonNullableTargetType == typeof(Guid)) conversionMethod = typeof(HelperExtensions).GetMethod(nameof(HelperExtensions.ToGuid), BindingFlags.Static | BindingFlags.Public);

                    if (conversionMethod != null)
                    {
                        return Expression.Convert(Expression.Call(conversionMethod, Expression.Convert(expression, typeof(object))), targetType);
                    }
                    else // Fallback to Convert.ChangeType
                    {
                        MethodInfo changeTypeMethod = typeof(Convert).GetMethod(nameof(Convert.ChangeType), new Type[] { typeof(object), typeof(Type), typeof(IFormatProvider) })!;
                        Expression converted = Expression.Call(changeTypeMethod, Expression.Convert(expression, typeof(object)), Expression.Constant(nonNullableTargetType), Expression.Constant(CultureInfo.InvariantCulture));
                        return Expression.Convert(converted, targetType);
                    }
                }
            }
            else if (expression.Type.IsNumeric() && targetType.IsNumeric())
            {
                // If both are numeric, ensure consistent type for comparison (numeric promotion)
                Type commonNumericType = GetCommonNumericType(expression.Type, targetType);
                if (expression.Type != commonNumericType)
                {
                    return Expression.Convert(expression, commonNumericType);
                }
            }
            else if (targetType == typeof(string) && expression.Type != typeof(string))
            {
                MethodInfo? toStringNullMethod = typeof(HelperExtensions).GetMethod(nameof(HelperExtensions.ToStringNull), BindingFlags.Static | BindingFlags.Public);
                if (toStringNullMethod == null) throw new InvalidOperationException($"Could not find '{nameof(HelperExtensions.ToStringNull)}' extension method.");
                return Expression.Call(toStringNullMethod, Expression.Convert(expression, typeof(object)));
            }

            // Default: try a direct cast if compatible (e.g., int to long, non-nullable to nullable)
            try
            {
                return Expression.Convert(expression, targetType);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Failed to convert expression of type '{expression.Type.Name}' to target type '{targetType.Name}'.", ex);
            }
        }


        /// <summary>
        /// Converts a string value to a specified target type.
        /// This is a helper for parsing literal values in special operators.
        /// </summary>
        /// <param name="valueString">The string literal to convert.</param>
        /// <param name="targetType">The desired .NET type for the converted value.</param>
        /// <returns>The converted object.</returns>
        public static object? ConvertStringToType(string valueString, Type targetType)
        {
            string cleanedValue = valueString;
            if (cleanedValue.StartsWith("'") && cleanedValue.EndsWith("'"))
            {
                cleanedValue = cleanedValue.Trim('\'');
            }
            else if (cleanedValue.StartsWith("\"") && cleanedValue.EndsWith("\""))
            {
                cleanedValue = cleanedValue.Trim('"');
            }

            if (string.Equals(cleanedValue, "NULL", StringComparison.OrdinalIgnoreCase))
            {
                return null; // Return actual null, not null!
            }

            Type underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

            try
            {
                if (underlyingType == typeof(string))
                {
                    return cleanedValue;
                }
                if (underlyingType == typeof(bool))
                {
                    if (string.Equals(cleanedValue, "true", StringComparison.OrdinalIgnoreCase)) return true;
                    if (string.Equals(cleanedValue, "false", StringComparison.OrdinalIgnoreCase)) return false;
                    throw new FormatException($"Cannot convert '{cleanedValue}' to boolean. Expected 'true' or 'false'.");
                }
                if (underlyingType == typeof(DateTime))
                {
                    return cleanedValue.ToDateTime();
                }
                if (underlyingType == typeof(Guid))
                {
                    return Guid.Parse(cleanedValue);
                }
                // For other types (numeric), use ChangeType
                return Convert.ChangeType(cleanedValue, underlyingType, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new FormatException($"Cannot convert value '{valueString}' to type '{targetType.Name}'. Check format or value.", ex);
            }
        }

        /// <summary>
        /// Gets the common numeric type for two types (e.g., int and double -> double).
        /// </summary>
        private static Type GetCommonNumericType(Type type1, Type type2)
        {
            type1 = Nullable.GetUnderlyingType(type1) ?? type1;
            type2 = Nullable.GetUnderlyingType(type2) ?? type2;

            if (type1 == typeof(decimal) || type2 == typeof(decimal)) return typeof(decimal);
            if (type1 == typeof(double) || type2 == typeof(double)) return typeof(double);
            if (type1 == typeof(float) || type2 == typeof(float)) return typeof(float);
            if (type1 == typeof(long) || type2 == typeof(long)) return typeof(long);
            if (type1 == typeof(int) || type2 == typeof(int)) return typeof(int);
            if (type1 == typeof(short) || type2 == typeof(short)) return typeof(short);
            if (type1 == typeof(byte) || type2 == typeof(byte)) return typeof(byte);

            return typeof(object); // Fallback for non-numeric or extremely divergent types
        }

    }
}

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Timed WMI queries with a reused CIMV2 scope, row arrays, and a short-lived result cache.
/// </summary>
internal static class SystemInformationWmi
{
    internal const int DefaultRowLimit = 2000;
    internal const int HardwareResourceRowLimit = 750;
    internal const string PnpProblemCondition = "ConfigManagerErrorCode != 0";

    private static readonly TimeSpan QueryTimeout = TimeSpan.FromSeconds(20);
    private static readonly object ScopeLock = new object();
    private static readonly ConcurrentDictionary<string, WmiQueryResult> ResultCache =
        new ConcurrentDictionary<string, WmiQueryResult>(StringComparer.Ordinal);

    private static ManagementScope? _scope;

    internal readonly struct WmiQueryResult
    {
        public WmiQueryResult(List<string[]> rows, string? error, bool truncated)
        {
            Rows = rows;
            Error = error;
            Truncated = truncated;
        }

        public List<string[]> Rows { get; }
        public string? Error { get; }
        public bool Truncated { get; }
    }

    /// <summary>Drops cached WMI rows (used on Refresh).</summary>
    public static void InvalidateCache() => ResultCache.Clear();

    /// <summary>
    /// Queries a WMI class into column-aligned string rows.
    /// </summary>
    public static WmiQueryResult Query(
        string wmiClass,
        string[] properties,
        CancellationToken cancellationToken,
        string? condition = null,
        int rowLimit = DefaultRowLimit)
    {
        var cacheKey = $"{wmiClass}|{condition}|{string.Join(",", properties)}|{rowLimit}";
        if (ResultCache.TryGetValue(cacheKey, out var cached))
        {
            return cached;
        }

        var rows = new List<string[]>();
        string? error = null;
        var truncated = false;
        var selectList = properties == null || properties.Length == 0 ? "*" : string.Join(",", properties);
        var wql = string.IsNullOrEmpty(condition)
            ? $"SELECT {selectList} FROM {wmiClass}"
            : $"SELECT {selectList} FROM {wmiClass} WHERE {condition}";

        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            var scope = GetScope();
            var options = new EnumerationOptions
            {
                Timeout = QueryTimeout,
                Rewindable = false,
                ReturnImmediately = true
            };

            using var searcher = new ManagementObjectSearcher(scope, new ObjectQuery(wql), options);
            using var collection = searcher.Get();
            foreach (ManagementBaseObject obj in collection)
            {
                cancellationToken.ThrowIfCancellationRequested();
                using (obj)
                {
                    if (rows.Count >= rowLimit)
                    {
                        truncated = true;
                        break;
                    }

                    if (properties == null || properties.Length == 0)
                    {
                        var list = new List<string>();
                        foreach (var prop in obj.Properties)
                        {
                            list.Add(FormatValue(prop.Value));
                        }

                        rows.Add(list.ToArray());
                    }
                    else
                    {
                        var values = new string[properties.Length];
                        for (var i = 0; i < properties.Length; i++)
                        {
                            object? value = null;
                            try
                            {
                                value = obj[properties[i]];
                            }
                            catch (ManagementException)
                            {
                                // Property may not exist on this instance.
                            }

                            values[i] = FormatValue(value);
                        }

                        rows.Add(values);
                    }
                }
            }
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (ManagementException ex) when (IsAccessDenied(ex))
        {
            error = KryptonSystemInformationStrings.Current.AccessDenied;
        }
        catch (UnauthorizedAccessException)
        {
            error = KryptonSystemInformationStrings.Current.AccessDenied;
        }
        catch (ManagementException ex) when (ex.ErrorCode == ManagementStatus.Timedout)
        {
            error = KryptonSystemInformationStrings.Current.Timeout;
        }
        catch (Exception ex)
        {
            error = ex.Message;
        }

        var result = new WmiQueryResult(rows, error, truncated);
        ResultCache[cacheKey] = result;
        return result;
    }

    /// <summary>Formats a WMI property value for display.</summary>
    public static string FormatValue(object? value)
    {
        if (value == null || value is DBNull)
        {
            return string.Empty;
        }

        if (value is Array array)
        {
            var parts = new string[array.Length];
            for (var i = 0; i < array.Length; i++)
            {
                parts[i] = FormatValue(array.GetValue(i));
            }

            return string.Join(", ", parts);
        }

        return Convert.ToString(value, CultureInfo.CurrentCulture) ?? string.Empty;
    }

    /// <summary>Adds WMI instance properties as Item/Value rows using the first instance.</summary>
    public static void AddFirstInstanceAsItems(SystemInformationTable table, string wmiClass, string[] properties, CancellationToken cancellationToken)
    {
        var result = Query(wmiClass, properties, cancellationToken);
        if (!string.IsNullOrEmpty(result.Error))
        {
            table.AddRow(wmiClass, result.Error);
            return;
        }

        if (result.Rows.Count == 0)
        {
            return;
        }

        var first = result.Rows[0];
        for (var i = 0; i < properties.Length && i < first.Length; i++)
        {
            table.AddRow(properties[i], first[i]);
        }
    }

    /// <summary>Adds each WMI instance as a grid row using <paramref name="properties"/> as columns.</summary>
    public static void AddInstances(SystemInformationTable table, string wmiClass, string[] properties, CancellationToken cancellationToken, string? condition = null, int rowLimit = DefaultRowLimit)
    {
        var result = Query(wmiClass, properties, cancellationToken, condition, rowLimit);
        if (!string.IsNullOrEmpty(result.Error))
        {
            table.AddRow(result.Error);
            return;
        }

        if (result.Rows.Count == 0)
        {
            table.AddRow(KryptonSystemInformationStrings.Current.NoItems);
            return;
        }

        foreach (var row in result.Rows)
        {
            table.AddRow(row);
        }

        if (result.Truncated)
        {
            table.AddRow(KryptonSystemInformationStrings.Current.RowLimitNote);
        }
    }

    private static ManagementScope GetScope()
    {
        lock (ScopeLock)
        {
            if (_scope == null || !_scope.IsConnected)
            {
                _scope = new ManagementScope(@"\\.\root\cimv2");
                _scope.Options.Timeout = QueryTimeout;
                _scope.Connect();
            }

            return _scope;
        }
    }

    private static bool IsAccessDenied(ManagementException ex) =>
        ex.ErrorCode == ManagementStatus.AccessDenied ||
        ex.ErrorCode == ManagementStatus.PrivilegeNotHeld;
}

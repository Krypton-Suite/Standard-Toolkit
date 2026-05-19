#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

#if NETFRAMEWORK
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Krypton.Toolkit;

/// <summary>
/// Ensures <see cref="System.Resources.Extensions"/> loads on .NET Framework when preserialized resources
/// request an older assembly identity than the deployed NuGet DLL (e.g. 4.0.0.0 vs 9.0.0.10).
/// </summary>
internal static class KryptonNetFxResourceAssemblyResolve
{
    private static bool _registered;
    private static Assembly? _cachedAssembly;

    /// <summary>Registers <see cref="AppDomain.AssemblyResolve"/> once; returns 0 for use as a static field initializer.</summary>
    internal static int Register()
    {
        if (_registered)
        {
            return 0;
        }

        _registered = true;
        AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;
        return 0;
    }

    private static Assembly? OnAssemblyResolve(object? sender, ResolveEventArgs args)
    {
        var requested = args.Name;
        if (string.IsNullOrEmpty(requested)
            || !requested.StartsWith("System.Resources.Extensions,", StringComparison.Ordinal))
        {
            return null;
        }

        if (_cachedAssembly != null)
        {
            return _cachedAssembly;
        }

        foreach (var path in GetCandidatePaths())
        {
            if (!File.Exists(path))
            {
                continue;
            }

            try
            {
                var bytes = File.ReadAllBytes(path);
                _cachedAssembly = Assembly.Load(bytes);
                return _cachedAssembly;
            }
            catch
            {
                // Try next path
            }
        }

        return null;
    }

    private static IEnumerable<string> GetCandidatePaths()
    {
        const string fileName = "System.Resources.Extensions.dll";
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        string?[] directories =
        [
            Path.GetDirectoryName(typeof(KryptonNetFxResourceAssemblyResolve).Assembly.Location),
            AppDomain.CurrentDomain.BaseDirectory,
            Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)
        ];

        foreach (var directory in directories)
        {
            if (string.IsNullOrEmpty(directory))
            {
                continue;
            }

            var path = Path.Combine(directory, fileName);
            if (seen.Add(path))
            {
                yield return path;
            }
        }
    }
}
#endif

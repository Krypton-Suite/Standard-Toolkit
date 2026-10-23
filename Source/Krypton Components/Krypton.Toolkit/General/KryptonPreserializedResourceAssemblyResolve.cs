#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Krypton.Toolkit;

/// <summary>
/// Loads preserialized-resource dependencies from the application directory when the CLR requests
/// an older assembly identity (e.g. System.Resources.Extensions 4.0.0.0 vs deployed 9.0.0.10).
/// </summary>
internal static class KryptonPreserializedResourceAssemblyResolve
{
    private static bool _registered;
    private static readonly Dictionary<string, Assembly> _cachedAssemblies = new(StringComparer.OrdinalIgnoreCase);

    private static readonly string[] _assemblyNames =
    [
        "System.Resources.Extensions",
        "System.Formats.Nrbf",
        "System.Memory",
        "System.Buffers",
        "System.Runtime.CompilerServices.Unsafe",
        "System.Collections.Immutable",
        "System.Reflection.Metadata",
        "System.Numerics.Vectors"
    ];

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
        if (string.IsNullOrEmpty(requested))
        {
            return null;
        }

        var comma = requested.IndexOf(',');
        var simpleName = comma >= 0 ? requested.Substring(0, comma) : requested;

        var isKnown = false;
        foreach (var name in _assemblyNames)
        {
            if (string.Equals(simpleName, name, StringComparison.Ordinal))
            {
                isKnown = true;
                break;
            }
        }

        if (!isKnown)
        {
            return null;
        }

        if (_cachedAssemblies.TryGetValue(simpleName, out var cached))
        {
            return cached;
        }

        var fileName = simpleName + ".dll";
        foreach (var path in GetCandidatePaths(fileName))
        {
            if (!File.Exists(path))
            {
                continue;
            }

            try
            {
                var bytes = File.ReadAllBytes(path);
                var assembly = Assembly.Load(bytes);
                _cachedAssemblies[simpleName] = assembly;
                return assembly;
            }
            catch
            {
                // Try next path
            }
        }

        return null;
    }

    private static IEnumerable<string> GetCandidatePaths(string fileName)
    {
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        string?[] directories =
        [
            Path.GetDirectoryName(typeof(KryptonPreserializedResourceAssemblyResolve).Assembly.Location),
            AppDomain.CurrentDomain.BaseDirectory
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

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

#if NETFRAMEWORK
using System.IO;
using System.Reflection;

namespace Krypton.Toolkit;

/// <summary>
/// Ensures <see cref="System.Resources.Extensions"/> loads from the same directory as this assembly on .NET Framework.
/// Preserialized .resx can request an older assembly identity than the NuGet DLL (e.g. 4.0.0.0 vs 9.0.0.0), which
/// otherwise surfaces as <see cref="TypeInitializationException"/> during <see cref="KryptonManager"/> static init
/// when binding redirects are missing or incomplete (see GitHub #3330).
/// </summary>
internal static class KryptonNetFxResourceAssemblyResolve
{
    private static bool _registered;

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

        var toolkitAssembly = typeof(KryptonNetFxResourceAssemblyResolve).Assembly;
        var dir = Path.GetDirectoryName(toolkitAssembly.Location);
        if (string.IsNullOrEmpty(dir))
        {
            return null;
        }

        var path = Path.Combine(dir, "System.Resources.Extensions.dll");
        if (!File.Exists(path))
        {
            return null;
        }

        try
        {
            return Assembly.LoadFrom(path);
        }
        catch
        {
            return null;
        }
    }
}
#endif

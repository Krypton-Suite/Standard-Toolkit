#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

#if NETFRAMEWORK
namespace System.Runtime.CompilerServices
{
    using System;

    /// <summary>Polyfill so <see cref="ModuleInitializerAttribute"/> can be used on .NET Framework.</summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    internal sealed class ModuleInitializerAttribute : Attribute
    {
    }
}
#endif

namespace Krypton.Toolkit
{

/// <summary>
/// Loads <c>Krypton.Resources.dll</c> from the application directory, or the embedded placeholder when that file is missing.
/// The placeholder keeps the process alive: control text is compiled into <c>Krypton.Toolkit</c>, and string resources
/// (palette schemas, Outlook grid) still resolve. Theme images are empty bitmaps until the full DLL is deployed.
/// </summary>
internal static class KryptonResourcesAssemblyResolve
{
    private const string ResourceName = "Krypton.Toolkit.ResourcesFallback.dll";
    private static int _registered;
    private static int _reportedMissing;
    private static Assembly? _resolved;

    // Library module initializer: on modern TFMs this runs before any type initializer.
    // net472 relies on KryptonPreserializedResourceAssemblyResolve.Register instead.
#pragma warning disable CA2255
    [ModuleInitializer]
#pragma warning restore CA2255
    internal static void Register()
    {
        if (Interlocked.Exchange(ref _registered, 1) != 0)
        {
            return;
        }

        AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;
    }

    private static Assembly? OnAssemblyResolve(object? sender, ResolveEventArgs args)
    {
        if (string.IsNullOrEmpty(args.Name))
        {
            return null;
        }

        var simpleName = args.Name.Split(',')[0];
        if (!string.Equals(simpleName, "Krypton.Resources", StringComparison.Ordinal))
        {
            return null;
        }

        if (_resolved != null)
        {
            return _resolved;
        }

        var directory = Path.GetDirectoryName(typeof(KryptonResourcesAssemblyResolve).Assembly.Location);
        if (!string.IsNullOrEmpty(directory))
        {
            var candidate = Path.Combine(directory, "Krypton.Resources.dll");
            if (File.Exists(candidate))
            {
                _resolved = Assembly.LoadFrom(candidate);
                return _resolved;
            }
        }

        _resolved = LoadEmbeddedFallback();
        if (_resolved != null && Interlocked.Exchange(ref _reportedMissing, 1) == 0)
        {
            Trace.TraceWarning(
                "Krypton.Resources.dll was not found next to Krypton.Toolkit.dll. The application will keep running and control text stays available, but theme images are placeholders. Deploy the bundled Krypton.Resources.dll beside Krypton.Toolkit.dll.");
        }

        return _resolved;
    }

    private static Assembly? LoadEmbeddedFallback()
    {
        using (var stream = typeof(KryptonResourcesAssemblyResolve).Assembly.GetManifestResourceStream(ResourceName))
        {
            if (stream == null)
            {
                return null;
            }

            using (var memory = new MemoryStream())
            {
                stream.CopyTo(memory);
                return Assembly.Load(memory.ToArray());
            }
        }
    }
}
}

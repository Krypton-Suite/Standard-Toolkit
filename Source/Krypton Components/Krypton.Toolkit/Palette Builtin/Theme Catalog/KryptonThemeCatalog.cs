#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Registry of builtin palette implementations (core plus auto-discovered extra assemblies).
/// </summary>
public static class KryptonThemeCatalog
{
    private const string ThemesAssemblyFileName = @"Krypton.Themes.dll";

    /// <summary>
    /// Gets the number of palettes registered as core in <c>Krypton.Toolkit</c>.
    /// </summary>
    public static int CorePaletteCount
    {
        get
        {
            EnsureCoreRegistered();
            lock (_sync)
            {
                var count = 0;
                foreach (var descriptor in _descriptors.Values)
                {
                    if (descriptor.IsCore)
                    {
                        count++;
                    }
                }

                return count;
            }
        }
    }

    private static readonly object _sync = new object();
    private static readonly Dictionary<PaletteMode, KryptonThemeDescriptor> _descriptors =
        new Dictionary<PaletteMode, KryptonThemeDescriptor>();
    private static readonly Dictionary<Type, PaletteMode> _typeToMode = new Dictionary<Type, PaletteMode>();
    private static readonly Dictionary<PaletteMode, PaletteBase> _instances = new Dictionary<PaletteMode, PaletteBase>();
    private static readonly HashSet<string> _loadedAssemblies = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

    private static bool _coreRegistered;
    private static bool _fileProbeAttempted;
    private static bool _themesNameLoadAttempted;

    private static readonly HashSet<PaletteMode> _warnedMissingModes = new HashSet<PaletteMode>();

    /// <summary>
    /// Gets or sets whether a warning dialog is displayed when an extra theme is requested but <c>Krypton.Themes.dll</c> is unavailable.
    /// Defaults to <see langword="true"/> (opt-out).
    /// </summary>
    public static bool ShowMissingThemeWarningDialog { get; set; } = true;

    /// <summary>
    /// Occurs when providers are registered (core or extra). Theme selectors should rebuild.
    /// </summary>
    public static event EventHandler? CatalogChanged;

    /// <summary>
    /// Gets whether an implementation is registered for <paramref name="mode"/>.
    /// </summary>
    /// <param name="mode">Palette mode.</param>
    /// <returns><see langword="true"/> when a factory is available.</returns>
    public static bool IsImplementationAvailable(PaletteMode mode)
    {
        if (mode == PaletteMode.Global || mode == PaletteMode.Custom)
        {
            return true;
        }

        EnsureReady();
        lock (_sync)
        {
            return _descriptors.ContainsKey(mode);
        }
    }

    /// <summary>
    /// Gets whether <paramref name="mode"/> is a core (Toolkit) palette.
    /// </summary>
    /// <param name="mode">Palette mode.</param>
    /// <returns><see langword="true"/> for Professional, Sparkle Blue/Orange/Purple, and Office 2007/2010/Microsoft 365 Blue, Silver, and Black.</returns>
    public static bool IsCoreMode(PaletteMode mode)
    {
        EnsureCoreRegistered();
        lock (_sync)
        {
            return _descriptors.TryGetValue(mode, out var descriptor) && descriptor.IsCore;
        }
    }

    /// <summary>
    /// Tries to resolve the <see cref="PaletteMode"/> for a concrete palette type.
    /// </summary>
    /// <param name="paletteType">Palette class type.</param>
    /// <param name="mode">Resolved mode.</param>
    /// <returns><see langword="true"/> when the type is catalogued.</returns>
    public static bool TryGetMode(Type? paletteType, out PaletteMode mode)
    {
        mode = PaletteMode.Global;
        if (paletteType is null)
        {
            return false;
        }

        EnsureReady();
        lock (_sync)
        {
            if (_typeToMode.TryGetValue(paletteType, out mode))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Gets the selector display name for <paramref name="mode"/>.
    /// </summary>
    public static string GetDisplayName(PaletteMode mode) =>
        PaletteModeStrings.SupportedThemes.SecondToFirst.TryGetValue(mode, out var name)
            ? name
            : mode.ToString();

    /// <summary>
    /// Gets a snapshot of registered descriptors (core and extra).
    /// </summary>
    public static KryptonThemeDescriptor[] GetDescriptors()
    {
        EnsureReady();
        lock (_sync)
        {
            var copy = new KryptonThemeDescriptor[_descriptors.Count];
            _descriptors.Values.CopyTo(copy, 0);
            return copy;
        }
    }

    /// <summary>
    /// Gets distinct family keys currently registered, ordered by name.
    /// </summary>
    public static string[] GetFamilies()
    {
        EnsureReady();
        lock (_sync)
        {
            return _descriptors.Values
                .Select(d => d.Family)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(s => s, StringComparer.OrdinalIgnoreCase)
                .ToArray();
        }
    }

    /// <summary>
    /// Gets the family key for a mode when it is catalogued.
    /// </summary>
    /// <param name="mode">Palette mode.</param>
    /// <returns>Family name, or <see langword="null"/>.</returns>
    public static string? GetFamily(PaletteMode mode)
    {
        EnsureReady();
        lock (_sync)
        {
            return _descriptors.TryGetValue(mode, out var descriptor) ? descriptor.Family : null;
        }
    }

    /// <summary>
    /// Tries to get the registered descriptor for <paramref name="mode"/>.
    /// </summary>
    /// <param name="mode">Palette mode.</param>
    /// <param name="descriptor">The descriptor when registered.</param>
    /// <returns><see langword="true"/> when the mode is catalogued.</returns>
    public static bool TryGetDescriptor(PaletteMode mode, out KryptonThemeDescriptor? descriptor)
    {
        descriptor = null;
        if (mode == PaletteMode.Global || mode == PaletteMode.Custom)
        {
            return false;
        }

        EnsureReady();
        lock (_sync)
        {
            if (_descriptors.TryGetValue(mode, out var found))
            {
                descriptor = found;
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Occurs when an extra <see cref="PaletteMode"/> is requested but no implementation is registered.
    /// The catalog then paints with <see cref="ToolkitStaticConstants.GLOBAL_DEFAULT_PALETTE_MODE"/>.
    /// </summary>
    public static event EventHandler<KryptonMissingThemeEventArgs>? MissingThemeFallback;

    /// <summary>
    /// Gets or creates the singleton palette for <paramref name="mode"/>.
    /// </summary>
    /// <param name="mode">Requested mode.</param>
    /// <returns>Palette instance. Extra modes without <c>Krypton.Themes</c> return Microsoft 365 Blue.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The mode is not a builtin palette.</exception>
    public static PaletteBase GetPalette(PaletteMode mode)
    {
        if (mode == PaletteMode.Global || mode == PaletteMode.Custom)
        {
            return KryptonManager.CurrentGlobalPalette;
        }

        EnsureReady();
        lock (_sync)
        {
            if (_instances.TryGetValue(mode, out var existing))
            {
                return existing;
            }

            if (_descriptors.TryGetValue(mode, out var descriptor))
            {
                var created = descriptor.Factory();
                _instances[mode] = created;
                return created;
            }
        }

        if (IsKnownExtraMode(mode))
        {
            return FallbackMissingExtra(mode);
        }

        throw new ArgumentOutOfRangeException(nameof(mode), mode, @"mode must be a PaletteMode value.");
    }

    private static PaletteBase FallbackMissingExtra(PaletteMode requestedMode)
    {
        var fallback = ToolkitStaticConstants.GLOBAL_DEFAULT_PALETTE_MODE;
        var requestedDisplayName = GetDisplayName(requestedMode);
        var fallbackDisplayName = GetDisplayName(fallback);
        var messageTemplate = KryptonManager.Strings.MiscellaneousThemeStrings.ThemeFallbackWarningMessage;
        string reason;
        try
        {
            reason = string.Format(messageTemplate, requestedDisplayName, requestedMode, fallbackDisplayName, fallback);
        }
        catch (FormatException)
        {
            reason = $"The requested theme '{requestedDisplayName}' ('{requestedMode}') requires the 'Krypton.Themes' assembly ('Krypton.Themes.dll'), which is not loaded or could not be found in the application directory. The theme has reverted to '{fallbackDisplayName}' ('{fallback}').";
        }

        Debug.WriteLine(
            @"KryptonThemeCatalog: extra palette '" + requestedMode +
            @"' is not available (Krypton.Themes.dll not loaded). Falling back to " + fallback + @".");
        Trace.TraceWarning(@"[KryptonThemeCatalog] " + reason);

        var eventArgs = new KryptonMissingThemeEventArgs(requestedMode, fallback, reason);
        MissingThemeFallback?.Invoke(null, eventArgs);

        if (ShowMissingThemeWarningDialog && !eventArgs.Handled && SystemInformation.UserInteractive)
        {
            var shouldWarn = false;
            lock (_sync)
            {
                shouldWarn = _warnedMissingModes.Add(requestedMode);
            }

            if (shouldWarn)
            {
                try
                {
                    var title = KryptonManager.Strings.MiscellaneousThemeStrings.ThemeFallbackWarningTitle;
                    KryptonMessageBox.Show(
                        reason,
                        title,
                        KryptonMessageBoxButtons.OK,
                        KryptonMessageBoxIcon.Warning,
                        showCopyButton: true);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"KryptonThemeCatalog.FallbackMissingExtra dialog: " + ex.Message);
                }
            }
        }

        return GetPalette(fallback);
    }

    /// <summary>
    /// Registers all themes from <paramref name="provider"/>. Existing modes are not replaced.
    /// </summary>
    /// <param name="provider">Theme provider.</param>
    public static void Register(IKryptonThemeProvider provider)
    {
        if (provider is null)
        {
            throw new ArgumentNullException(nameof(provider));
        }

        var themes = provider.GetThemes();
        if (themes is null)
        {
            return;
        }

        var added = false;
        lock (_sync)
        {
            foreach (var descriptor in themes)
            {
                if (descriptor is null)
                {
                    continue;
                }

                if (_descriptors.ContainsKey(descriptor.Mode))
                {
                    continue;
                }

                _descriptors[descriptor.Mode] = descriptor;
                _typeToMode[descriptor.PaletteType] = descriptor.Mode;
                added = true;
            }
        }

        if (added)
        {
            OnCatalogChanged();
        }
    }

    /// <summary>
    /// Loads <c>Krypton.Themes.dll</c> from already-loaded assemblies and the application base directory.
    /// </summary>
    public static void DiscoverThemes()
    {
        if (!KryptonManager.AutoDiscoverThemes)
        {
            EnsureCoreRegistered();
            return;
        }

        EnsureCoreRegistered();

        try
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                TryRegisterFromAssembly(assembly);
            }

            bool loadByName;
            lock (_sync)
            {
                loadByName = !_themesNameLoadAttempted;
                _themesNameLoadAttempted = true;
            }

            if (loadByName)
            {
                try
                {
                    TryRegisterFromAssembly(Assembly.Load(new AssemblyName(@"Krypton.Themes")));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"KryptonThemeCatalog Assembly.Load(Krypton.Themes): " + ex.Message);
                }
            }

            bool probeFile;
            lock (_sync)
            {
                probeFile = !_fileProbeAttempted;
                _fileProbeAttempted = true;
            }

            if (probeFile)
            {
                var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var directory in GetThemesProbeDirectories())
                {
                    if (!seen.Add(directory))
                    {
                        continue;
                    }

                    TryLoadThemesAssembly(Path.Combine(directory, ThemesAssemblyFileName));
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"KryptonThemeCatalog.DiscoverThemes: " + ex.Message);
        }
    }

    internal static void EnsureReady()
    {
        EnsureCoreRegistered();
        if (KryptonManager.AutoDiscoverThemes)
        {
            DiscoverThemes();
        }
    }

    /// <summary>
    /// Forwards system preference changes to cached extra palettes.
    /// </summary>
    internal static void NotifyUserPreferenceChanged()
    {
        lock (_sync)
        {
            foreach (var palette in _instances.Values)
            {
                palette.UserPreferenceChanged();
            }
        }
    }

    internal static void EnsureCoreRegistered()
    {
        lock (_sync)
        {
            if (_coreRegistered)
            {
                return;
            }

            _coreRegistered = true;
        }

        Register(new KryptonCoreThemeProvider());
    }

    /// <summary>
    /// Builtin <see cref="PaletteMode"/> values that have no registered implementation after discovery.
    /// </summary>
    /// <returns>Empty when core plus discovered extra providers cover the enum (except <see cref="PaletteMode.Global"/> / <see cref="PaletteMode.Custom"/>).</returns>
    public static PaletteMode[] GetUnimplementedBuiltinModes()
    {
        EnsureReady();
        var missing = new List<PaletteMode>();
        foreach (PaletteMode mode in Enum.GetValues(typeof(PaletteMode)))
        {
            if (mode == PaletteMode.Global || mode == PaletteMode.Custom)
            {
                continue;
            }

            lock (_sync)
            {
                if (!_descriptors.ContainsKey(mode))
                {
                    missing.Add(mode);
                }
            }
        }

        return missing.ToArray();
    }

    private static IEnumerable<string> GetThemesProbeDirectories()
    {
        var directories = new List<string>();
        if (!string.IsNullOrEmpty(AppContext.BaseDirectory))
        {
            directories.Add(AppContext.BaseDirectory);
        }

        try
        {
            var toolkitDir = Path.GetDirectoryName(typeof(KryptonThemeCatalog).Assembly.Location);
            if (toolkitDir is { Length: > 0 })
            {
                directories.Add(toolkitDir);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"KryptonThemeCatalog toolkit directory: " + ex.Message);
        }

        try
        {
            var entry = Assembly.GetEntryAssembly();
            var entryDir = entry is null ? null : Path.GetDirectoryName(entry.Location);
            if (entryDir is { Length: > 0 })
            {
                directories.Add(entryDir);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"KryptonThemeCatalog entry directory: " + ex.Message);
        }

        return directories;
    }

    private static void TryLoadThemesAssembly(string path)
    {
        if (string.IsNullOrEmpty(path) || !File.Exists(path))
        {
            return;
        }

        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            if (string.Equals(assembly.GetName().Name, @"Krypton.Themes", StringComparison.OrdinalIgnoreCase))
            {
                TryRegisterFromAssembly(assembly);
                return;
            }
        }

        try
        {
            var assemblyName = AssemblyName.GetAssemblyName(path);
            if (!string.Equals(assemblyName.Name, @"Krypton.Themes", StringComparison.OrdinalIgnoreCase)
                || !PublicKeyTokenMatchesToolkit(assemblyName))
            {
                Debug.WriteLine(@"KryptonThemeCatalog skipped unsigned or unexpected assembly at " + path);
                return;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"KryptonThemeCatalog could not read " + path + @": " + ex.Message);
            return;
        }

        try
        {
            var loaded = Assembly.LoadFrom(path);
            if (!PublicKeyTokenMatchesToolkit(loaded.GetName()))
            {
                Debug.WriteLine(@"KryptonThemeCatalog rejected " + path + @" after load (public key token).");
                return;
            }

            TryRegisterFromAssembly(loaded);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"KryptonThemeCatalog could not load " + path + @": " + ex.Message);
        }
    }

    private static bool PublicKeyTokenMatchesToolkit(AssemblyName assemblyName)
    {
        var actual = assemblyName.GetPublicKeyToken();
        var expected = typeof(KryptonThemeCatalog).Assembly.GetName().GetPublicKeyToken();
        if (actual is null || expected is null || actual.Length != expected.Length)
        {
            return false;
        }

        for (int i = 0; i < actual.Length; i++)
        {
            if (actual[i] != expected[i])
            {
                return false;
            }
        }

        return true;
    }

    private static void TryRegisterFromAssembly(Assembly assembly)
    {
        if (assembly is null || assembly.IsDynamic)
        {
            return;
        }

        var name = assembly.FullName ?? assembly.GetName().Name;
        if (string.IsNullOrEmpty(name))
        {
            return;
        }

        lock (_sync)
        {
            if (!_loadedAssemblies.Add(name))
            {
                return;
            }
        }

        try
        {
            var attributes = assembly.GetCustomAttributes(typeof(KryptonThemeProviderAttribute), false);
            foreach (var raw in attributes)
            {
                if (raw is KryptonThemeProviderAttribute attribute)
                {
                    TryCreateAndRegister(attribute.ProviderType);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"KryptonThemeCatalog.TryRegisterFromAssembly: " + ex.Message);
        }
    }

    private static void TryCreateAndRegister(Type? providerType)
    {
        if (providerType is null || !typeof(IKryptonThemeProvider).IsAssignableFrom(providerType))
        {
            return;
        }

        try
        {
            if (Activator.CreateInstance(providerType) is IKryptonThemeProvider provider)
            {
                Register(provider);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"KryptonThemeCatalog.TryCreateAndRegister: " + ex.Message);
        }
    }

    private static void OnCatalogChanged()
    {
        CatalogChanged?.Invoke(null, EventArgs.Empty);
        ThemeManager.NotifyThemeListChanged();
    }

    private static bool IsKnownExtraMode(PaletteMode mode) =>
        mode != PaletteMode.Global
        && mode != PaletteMode.Custom
        && !IsCoreMode(mode)
        && PaletteModeStrings.SupportedThemes.SecondToFirst.ContainsKey(mode);
}

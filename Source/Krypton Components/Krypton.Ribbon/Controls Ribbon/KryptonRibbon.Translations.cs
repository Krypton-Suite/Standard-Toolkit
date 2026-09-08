#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

public partial class KryptonRibbon : IRibbonTranslationIdentity
{
    #region Static Fields (translations)
    private static readonly List<WeakReference<KryptonRibbon>> _liveRibbons = new List<WeakReference<KryptonRibbon>>();
    private static bool _managerTranslationsHooked;
    #endregion

    #region Instance Fields (translations)
    private string _translationId = string.Empty;
    private bool _enableAutoDiscoverTranslations = true;
    private string? _translationsSearchPath;
    private bool _autoDiscoveryAttempted;
    private bool _importingTranslations;
    #endregion

    #region Events (translations)

    /// <summary>
    /// Occurs while a translations document is being written so callers can append extra XML.
    /// </summary>
    [Category(@"Ribbon")]
    [Description(@"Occurs while RibbonTranslations.xml is being exported.")]
    public event EventHandler<RibbonTranslationsXmlEventArgs>? RibbonTranslationsSaving;

    /// <summary>
    /// Occurs after the standard overlay is applied so callers can read extra XML.
    /// </summary>
    [Category(@"Ribbon")]
    [Description(@"Occurs after RibbonTranslations.xml has been imported.")]
    public event EventHandler<RibbonTranslationsXmlEventArgs>? RibbonTranslationsLoading;

    /// <summary>
    /// Occurs after ribbon translations have been imported from a file, stream, or Auto Discovery.
    /// </summary>
    [Category(@"Ribbon")]
    [Description(@"Occurs after ribbon translations have been imported.")]
    public event EventHandler? RibbonTranslationsImported;

    /// <summary>
    /// Occurs after a translations file has been analyzed for coverage.
    /// </summary>
    [Category(@"Ribbon")]
    [Description(@"Occurs after ribbon translations coverage has been calculated.")]
    public event EventHandler<RibbonTranslationsCoverageEventArgs>? RibbonTranslationsCoverageReported;

    #endregion

    #region Public (translation identity and auto-discovery)

    /// <summary>
    /// Gets or sets whether ribbons probe the application directory for <c>RibbonTranslations.{culture}.*</c> files.
    /// Defaults to <see langword="true"/>. Set to <see langword="false"/> before creating a ribbon to suppress Auto Discovery
    /// (for example when loading translations from a database BLOB).
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static bool AutoDiscoverTranslations { get; set; } = true;

    /// <summary>
    /// Gets or sets a stable, non-localized identity used when matching Auto Discovery files and the
    /// <c>RibbonName</c> document attribute. When empty, the control <see cref="Control.Name"/> is used.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Stable identity for RibbonTranslations.xml Auto Discovery and the RibbonName attribute.")]
    [DefaultValue("")]
    [Localizable(false)]
    public string TranslationId
    {
        get => _translationId ?? string.Empty;
        set => _translationId = value ?? string.Empty;
    }

    private bool ShouldSerializeTranslationId() => !string.IsNullOrEmpty(TranslationId);

    private void ResetTranslationId() => TranslationId = string.Empty;

    /// <summary>
    /// Gets or sets whether this ribbon instance participates in Auto Discovery. Defaults to <see langword="true"/>.
    /// </summary>
    [Category(@"Data")]
    [Description(@"When true, this ribbon loads RibbonTranslations files from TranslationsSearchPath or the application directory.")]
    [DefaultValue(true)]
    public bool EnableAutoDiscoverTranslations
    {
        get => _enableAutoDiscoverTranslations;
        set => _enableAutoDiscoverTranslations = value;
    }

    /// <summary>
    /// Gets or sets an optional directory to probe for ribbon translation files. When empty, the application base directory is used.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Optional directory containing RibbonTranslations.xml files. Empty uses the application base directory.")]
    [DefaultValue(null)]
    [Localizable(false)]
    public string? TranslationsSearchPath
    {
        get => _translationsSearchPath;
        set => _translationsSearchPath = value;
    }

    private bool ShouldSerializeTranslationsSearchPath() => !string.IsNullOrWhiteSpace(TranslationsSearchPath);

    private void ResetTranslationsSearchPath() => TranslationsSearchPath = null;

    #endregion

    #region Public (import / export)

    /// <summary>
    /// Exports localizable ribbon strings to a versioned XML document.
    /// </summary>
    public XmlDocument ExportTranslationsToXmlDocument(RibbonTranslationOptions? options = null) =>
        RibbonTranslationsXmlPersistence.Export(this, options);

    /// <summary>
    /// Exports localizable ribbon strings to a versioned XML file.
    /// </summary>
    public void ExportTranslationsToXmlFile(string filename, RibbonTranslationOptions? options = null) =>
        RibbonTranslationsXmlPersistence.ExportToFile(this, filename, options);

    /// <summary>
    /// Exports localizable ribbon strings to a stream (for example a database BLOB).
    /// </summary>
    public void ExportTranslationsToStream(Stream stream, RibbonTranslationOptions? options = null) =>
        RibbonTranslationsXmlPersistence.ExportToStream(this, stream, options);

    /// <summary>
    /// Imports localizable ribbon strings from a versioned XML document. Overlay only — structure is not created or deleted.
    /// </summary>
    public void ImportTranslationsFromXmlDocument(XmlDocument doc, RibbonTranslationOptions? options = null)
    {
        ImportTranslations(() => RibbonTranslationsXmlPersistence.Import(this, doc, options));
    }

    /// <summary>
    /// Imports localizable ribbon strings from a versioned XML file.
    /// </summary>
    public void ImportTranslationsFromXmlFile(string filename, RibbonTranslationOptions? options = null)
    {
        ImportTranslations(() => RibbonTranslationsXmlPersistence.ImportFromFile(this, filename, options));
    }

    /// <summary>
    /// Imports localizable ribbon strings from a stream (for example a database BLOB).
    /// </summary>
    public void ImportTranslationsFromStream(Stream stream, RibbonTranslationOptions? options = null)
    {
        ImportTranslations(() => RibbonTranslationsXmlPersistence.ImportFromStream(this, stream, options));
    }

    /// <summary>
    /// Exports localizable ribbon strings to JSON.
    /// </summary>
    public string ExportTranslationsToJson(RibbonTranslationOptions? options = null) =>
        RibbonTranslationsJsonPersistence.Export(this, options);

    /// <summary>
    /// Exports localizable ribbon strings to a JSON file.
    /// </summary>
    public void ExportTranslationsToJsonFile(string filename, RibbonTranslationOptions? options = null) =>
        RibbonTranslationsJsonPersistence.ExportToFile(this, filename, options);

    /// <summary>
    /// Exports localizable ribbon strings to a stream as JSON.
    /// </summary>
    public void ExportTranslationsToJsonStream(Stream stream, RibbonTranslationOptions? options = null) =>
        RibbonTranslationsJsonPersistence.ExportToStream(this, stream, options);

    /// <summary>
    /// Imports localizable ribbon strings from a JSON string.
    /// </summary>
    public void ImportTranslationsFromJson(string json, RibbonTranslationOptions? options = null)
    {
        ImportTranslations(() => RibbonTranslationsJsonPersistence.ImportFromJson(this, json, options));
    }

    /// <summary>
    /// Imports localizable ribbon strings from a JSON file.
    /// </summary>
    public void ImportTranslationsFromJsonFile(string filename, RibbonTranslationOptions? options = null)
    {
        ImportTranslations(() => RibbonTranslationsJsonPersistence.ImportFromFile(this, filename, options));
    }

    /// <summary>
    /// Imports localizable ribbon strings from a JSON stream.
    /// </summary>
    public void ImportTranslationsFromJsonStream(Stream stream, RibbonTranslationOptions? options = null)
    {
        ImportTranslations(() => RibbonTranslationsJsonPersistence.ImportFromStream(this, stream, options));
    }

    /// <summary>
    /// Compares the live ribbon string tree against a translations XML file without applying it.
    /// </summary>
    public RibbonTranslationsCoverage AnalyzeTranslationsFromFile(string filename, RibbonTranslationOptions? options = null)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(filename));
        }

        if (string.Equals(Path.GetExtension(filename), @".json", StringComparison.OrdinalIgnoreCase))
        {
            var json = File.ReadAllText(filename, Encoding.UTF8);
            return RibbonTranslationsJsonPersistence.Analyze(this, json, options, filename);
        }

        var doc = new XmlDocument();
        doc.Load(filename);
        return RibbonTranslationsXmlPersistence.Analyze(this, doc, options, filename);
    }

    /// <summary>
    /// Imports an existing translations file and rewrites it with newly added keys filled from current values/defaults.
    /// </summary>
    public RibbonTranslationsCoverage MergeMissingTranslationsToFile(string filename, RibbonTranslationOptions? options = null)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(filename));
        }

        return string.Equals(Path.GetExtension(filename), @".json", StringComparison.OrdinalIgnoreCase)
            ? RibbonTranslationsJsonPersistence.MergeMissingToFile(this, filename, options)
            : RibbonTranslationsXmlPersistence.MergeMissingToFile(this, filename, options);
    }

    /// <summary>
    /// Attempts Auto Discovery using the standard culture ladder (exact → neutral → default; XML before JSON;
    /// per-ribbon file then shared file). Failures are swallowed and traced to <see cref="Debug"/>.
    /// </summary>
    /// <returns><see langword="true"/> when a file was found and imported.</returns>
    public bool TryAutoDiscoverTranslations(string? directory = null, CultureInfo? culture = null) =>
        TryLoadCultureSpecificTranslations(directory, culture);

    /// <summary>
    /// Attempts to load the best matching <c>RibbonTranslations</c> file from a directory.
    /// </summary>
    public bool TryLoadCultureSpecificTranslations(
        string? directory = null,
        CultureInfo? culture = null)
    {
        var baseDir = ResolveSearchDirectory(directory);
        var resolvedCulture = culture ?? CultureInfo.CurrentUICulture;

        foreach (var candidate in BuildCultureSpecificCandidates(baseDir, resolvedCulture))
        {
            if (TryLoadTranslationsFileSilent(candidate))
            {
                Debug.WriteLine($@"[Krypton] Loaded ribbon translations from '{candidate}'.");
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Switches the current UI culture and re-probes ribbon translation files. When no file is found,
    /// existing instance captions are left unchanged.
    /// </summary>
    public bool TrySwitchTranslationsCulture(CultureInfo culture, string? directory = null)
    {
        if (culture == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(culture));
        }

        System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        if (TryLoadCultureSpecificTranslations(directory, culture))
        {
            return true;
        }

        Debug.WriteLine(
            $@"[Krypton] Switched UI culture to '{culture.Name}' with no matching RibbonTranslations file; instance captions were left unchanged.");
        return false;
    }

    /// <summary>
    /// Switches the current UI culture using a culture name and re-probes ribbon translation files.
    /// </summary>
    public bool TrySwitchTranslationsCulture(string cultureName, string? directory = null)
    {
        if (string.IsNullOrWhiteSpace(cultureName))
        {
            return false;
        }

        try
        {
            return TrySwitchTranslationsCulture(new CultureInfo(cultureName), directory);
        }
        catch (CultureNotFoundException ex)
        {
            Debug.WriteLine($@"[Krypton] TrySwitchTranslationsCulture failed for '{cultureName}': {ex.Message}");
            return false;
        }
    }

    #endregion

    #region Internal raise helpers

    internal void RaiseTranslationsSaving(XmlElement root) =>
        RibbonTranslationsSaving?.Invoke(this, new RibbonTranslationsXmlEventArgs(this, root));

    internal void RaiseTranslationsLoading(XmlElement root) =>
        RibbonTranslationsLoading?.Invoke(this, new RibbonTranslationsXmlEventArgs(this, root));

    internal void RaiseTranslationsCoverageReported(RibbonTranslationsCoverage coverage) =>
        RibbonTranslationsCoverageReported?.Invoke(this, new RibbonTranslationsCoverageEventArgs(coverage));

    #endregion

    #region Auto Discovery host

    private void RegisterTranslationAutoDiscovery()
    {
        EnsureManagerTranslationsHook();
        lock (_liveRibbons)
        {
            foreach (var existing in _liveRibbons)
            {
                if (existing.TryGetTarget(out var live) && ReferenceEquals(live, this))
                {
                    return;
                }
            }

            _liveRibbons.Add(new WeakReference<KryptonRibbon>(this));
        }
    }

    private void UnregisterTranslationAutoDiscovery()
    {
        lock (_liveRibbons)
        {
            _liveRibbons.RemoveAll(reference =>
                !reference.TryGetTarget(out var live) || ReferenceEquals(live, this));
        }
    }

    private void OnHandleCreatedAutoDiscover()
    {
        if (DesignMode)
        {
            return;
        }

        RegisterTranslationAutoDiscovery();

        if (_autoDiscoveryAttempted)
        {
            return;
        }

        _autoDiscoveryAttempted = true;

        if (!AutoDiscoverTranslations || !EnableAutoDiscoverTranslations)
        {
            return;
        }

        TryLoadCultureSpecificTranslations();
    }

    private static void EnsureManagerTranslationsHook()
    {
        if (_managerTranslationsHooked)
        {
            return;
        }

        KryptonManager.TranslationsImported += OnManagerTranslationsImported;
        _managerTranslationsHooked = true;
    }

    private static void OnManagerTranslationsImported(object? sender, EventArgs e)
    {
        List<KryptonRibbon> snapshot;
        lock (_liveRibbons)
        {
            snapshot = new List<KryptonRibbon>();
            _liveRibbons.RemoveAll(reference => !reference.TryGetTarget(out _));
            foreach (var reference in _liveRibbons)
            {
                if (reference.TryGetTarget(out var ribbon) &&
                    ribbon != null &&
                    !ribbon.IsDisposed &&
                    !ribbon._importingTranslations &&
                    AutoDiscoverTranslations &&
                    ribbon.EnableAutoDiscoverTranslations &&
                    !ribbon.DesignMode)
                {
                    snapshot.Add(ribbon);
                }
            }
        }

        foreach (var ribbon in snapshot)
        {
            ribbon.TryLoadCultureSpecificTranslations();
        }
    }

    private string ResolveSearchDirectory(string? directory)
    {
        if (!string.IsNullOrWhiteSpace(directory))
        {
            return directory!;
        }

        if (!string.IsNullOrWhiteSpace(TranslationsSearchPath))
        {
            return TranslationsSearchPath!;
        }

        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        return string.IsNullOrWhiteSpace(baseDir) ? Directory.GetCurrentDirectory() : baseDir;
    }

    private IEnumerable<string> BuildCultureSpecificCandidates(string directory, CultureInfo culture)
    {
        var ribbonKey = RibbonTranslationsXmlPersistence.ResolveRibbonKey(this);
        var stems = new List<string>();

        void AddStem(string stem)
        {
            if (!string.IsNullOrWhiteSpace(stem) &&
                !stems.Exists(existing => string.Equals(existing, stem, StringComparison.OrdinalIgnoreCase)))
            {
                stems.Add(stem);
            }
        }

        var cultureName = culture?.Name ?? string.Empty;
        var neutralName = string.Empty;
        if (!string.IsNullOrEmpty(cultureName))
        {
            if (culture?.Parent != null && !string.IsNullOrEmpty(culture.Parent.Name))
            {
                neutralName = culture.Parent.Name;
            }
            else if (cultureName.Length >= 2)
            {
                neutralName = cultureName.Substring(0, 2);
            }
        }

        var cultures = new List<string>();
        if (!string.IsNullOrEmpty(cultureName))
        {
            cultures.Add(cultureName);
        }

        if (!string.IsNullOrEmpty(neutralName) &&
            !string.Equals(neutralName, cultureName, StringComparison.OrdinalIgnoreCase))
        {
            cultures.Add(neutralName);
        }

        cultures.Add(string.Empty);

        if (!string.IsNullOrEmpty(ribbonKey))
        {
            foreach (var name in cultures)
            {
                AddStem(string.IsNullOrEmpty(name)
                    ? $@"RibbonTranslations.{ribbonKey}"
                    : $@"RibbonTranslations.{ribbonKey}.{name}");
            }
        }

        foreach (var name in cultures)
        {
            AddStem(string.IsNullOrEmpty(name) ? @"RibbonTranslations" : $@"RibbonTranslations.{name}");
        }

        var directories = new List<string> { directory };
        var nested = Path.Combine(directory, @"RibbonTranslations");
        if (!string.Equals(nested, directory, StringComparison.OrdinalIgnoreCase))
        {
            directories.Add(nested);
        }

        foreach (var dir in directories)
        {
            foreach (var stem in stems)
            {
                yield return Path.Combine(dir, stem + @".xml");
            }
        }

        foreach (var dir in directories)
        {
            foreach (var stem in stems)
            {
                yield return Path.Combine(dir, stem + @".json");
            }
        }
    }

    private bool TryLoadTranslationsFileSilent(string path)
    {
        if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
        {
            return false;
        }

        try
        {
            if (string.Equals(Path.GetExtension(path), @".json", StringComparison.OrdinalIgnoreCase))
            {
                var json = File.ReadAllText(path, Encoding.UTF8);
                var jsonDoc = RibbonTranslationsJsonPersistence.JsonToXmlDocument(json);
                if (!RibbonTranslationsXmlPersistence.FileTargetsRibbon(jsonDoc, this))
                {
                    return false;
                }

                ImportTranslationsFromJson(json, new RibbonTranslationOptions { ResetFirst = true });
                return true;
            }

            var doc = new XmlDocument();
            doc.Load(path);
            if (!RibbonTranslationsXmlPersistence.FileTargetsRibbon(doc, this))
            {
                return false;
            }

            ImportTranslationsFromXmlDocument(doc, new RibbonTranslationOptions { ResetFirst = true });
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($@"[Krypton] TryLoadTranslationsFileSilent failed for '{path}': {ex.Message}");
            return false;
        }
    }

    private void ImportTranslations(Action import)
    {
        if (_importingTranslations)
        {
            return;
        }

        _importingTranslations = true;
        try
        {
            import();
            RibbonTranslationsImported?.Invoke(this, EventArgs.Empty);
        }
        finally
        {
            _importingTranslations = false;
        }
    }

    #endregion
}

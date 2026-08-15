#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Exposes global settings that affect all the Krypton controls.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonManager), "ToolboxBitmaps.KryptonManager.bmp")]
[Designer(typeof(KryptonManagerDesigner))]
[DefaultProperty(nameof(GlobalPaletteMode))]
[Description(@"Access 'Global' Krypton settings.")]
public sealed class KryptonManager : Component
{
    #region Static Fields
    // Initialize the global state
    private static bool _globalApplyToolstrips = true;
    private static bool _globalUseThemeFormChromeBorderWidth = true;
    private static bool _globalShowAdministratorSuffix = true;
    internal static bool _globalUseKryptonFileDialogs = true;
    private static bool _globalUseKryptonScrollbars = false;
    private static ScrollbarCornerStyle _globalScrollbarCornerStyle = ScrollbarCornerStyle.ThemedCorner;
    private static DropDownArrowRenderMode _globalDropDownArrowRenderMode = DropDownArrowRenderMode.Unicode;
    private static DropDownArrowGlyphStyle _globalDropDownArrowGlyphStyle = DropDownArrowGlyphStyle.Bevel;
    private static bool _globalTouchscreenMode = false;
    private static float _globalTouchscreenScaleFactor = 1.25f;
    private static bool _globalTouchscreenFontScaling = true;
    private static float _globalTouchscreenFontScaleFactor = 1.25f;
    private static bool _globalAutomaticallyDetectTouchscreen = false;
    private static int _globalTouchscreenDetectionInterval = 2000; // Default 2 seconds
    private static System.Threading.Timer? _touchscreenDetectionTimer;
    private static bool _lastDetectedTouchscreenState = false;
    private static Font? _baseFont;
    private static float _cachedDpiX = 0f;
    private static float _cachedDpiY = 0f;
    private static KryptonDialogButtonColorOptions? _globalDialogButtonColors;

    // Must run before any other static field that touches embedded resources (e.g. KryptonImageStorage / KryptonManager.Strings).
    private static readonly int _resourceAssemblyResolveHook = KryptonPreserializedResourceAssemblyResolve.Register();

    // Initialize the default modes

    // Initialize instances to match the default modes

    // Singleton instances are created on demand
    private static PaletteProfessionalOffice2003? _paletteProfessionalOffice2003;
    private static PaletteProfessionalSystem? _paletteProfessionalSystem;

    #region Office 2007 Themes

    private static PaletteOffice2007DarkGray? _paletteOffice2007DarkGray;
    private static PaletteOffice2007Blue? _paletteOffice2007Blue;
    private static PaletteOffice2007BlueDarkMode? _paletteOffice2007BlueDarkMode;
    private static PaletteOffice2007BlueLightMode? _paletteOffice2007BlueLightMode;
    private static PaletteOffice2007Silver? _paletteOffice2007Silver;
    private static PaletteOffice2007SilverDarkMode? _paletteOffice2007SilverDarkMode;
    private static PaletteOffice2007SilverLightMode? _paletteOffice2007SilverLightMode;
    private static PaletteOffice2007White? _paletteOffice2007White;
    private static PaletteOffice2007Black? _paletteOffice2007Black;
    private static PaletteOffice2007BlackDarkMode? _paletteOffice2007BlackDarkMode;

    #endregion

    #region Office 2010 Themes

    private static PaletteOffice2010DarkGray? _paletteOffice2010DarkGray;
    private static PaletteOffice2010Blue? _paletteOffice2010Blue;
    private static PaletteOffice2010BlueDarkMode? _paletteOffice2010BlueDarkMode;
    private static PaletteOffice2010BlueLightMode? _paletteOffice2010BlueLightMode;
    private static PaletteOffice2010White? _paletteOffice2010White;
    private static PaletteOffice2010Black? _paletteOffice2010Black;
    private static PaletteOffice2010BlackDarkMode? _paletteOffice2010BlackDarkMode;
    private static PaletteOffice2010Silver? _paletteOffice2010Silver;
    private static PaletteOffice2010SilverDarkMode? _paletteOffice2010SilverDarkMode;
    private static PaletteOffice2010SilverLightMode? _paletteOffice2010SilverLightMode;

    #endregion

    #region Office 2013 Themes

    private static PaletteOffice2013DarkGray? _paletteOffice2013DarkGray;
    private static PaletteOffice2013LightGray? _paletteOffice2013LightGray;
    private static PaletteOffice2013White? _paletteOffice2013White;

    #endregion

    #region Sparkle Themes

    private static PaletteSparkleBlue? _paletteSparkleBlue;
    private static PaletteSparkleBlueDarkMode? _paletteSparkleBlueDarkMode;
    private static PaletteSparkleBlueLightMode? _paletteSparkleBlueLightMode;
    private static PaletteSparkleOrange? _paletteSparkleOrange;
    private static PaletteSparkleOrangeDarkMode? _paletteSparkleOrangeDarkMode;
    private static PaletteSparkleOrangeLightMode? _paletteSparkleOrangeLightMode;
    private static PaletteSparklePurple? _paletteSparklePurple;
    private static PaletteSparklePurpleDarkMode? _paletteSparklePurpleDarkMode;
    private static PaletteSparklePurpleLightMode? _paletteSparklePurpleLightMode;

    #endregion

    #region Microsoft 365 Themes

    private static PaletteMicrosoft365DarkGray? _paletteMicrosoft365DarkGray;
    private static PaletteMicrosoft365Black? _paletteMicrosoft365Black;
    private static PaletteMicrosoft365BlackDarkMode? _paletteMicrosoft365BlackDarkMode;
    private static PaletteMicrosoft365BlackDarkModeAlternate? _paletteMicrosoft365BlackDarkModeAlternate;
    private static PaletteMicrosoft365Blue? _paletteMicrosoft365Blue;
    private static PaletteMicrosoft365BlueDarkMode? _paletteMicrosoft365BlueDarkMode;
    private static PaletteMicrosoft365BlueLightMode? _paletteMicrosoft365BlueLightMode;
    private static PaletteMicrosoft365Silver? _paletteMicrosoft365Silver;
    private static PaletteMicrosoft365SilverDarkMode? _paletteMicrosoft365SilverDarkMode;
    private static PaletteMicrosoft365SilverLightMode? _paletteMicrosoft365SilverLightMode;
    private static PaletteMicrosoft365White? _paletteMicrosoft365White;

    #endregion

    #region Visual Studio Themes

    #region Visual Studio 2010 Variations

    private static PaletteVisualStudio2010Office2007Variation? _paletteVisualStudio2010Office2007Variation;
    private static PaletteVisualStudio2010Office2010Variation? _paletteVisualStudio2010Office2010Variation;
    private static PaletteVisualStudio2010Office2013Variation? _paletteVisualStudio2010Office2013Variation;
    private static PaletteVisualStudio2010Microsoft365Variation? _paletteVisualStudio2010Microsoft365Variation;

    #endregion

    #region Visual Studio 2022

    private static PaletteVisualStudio2022Dark? _paletteVisualStudio2022Dark;

    #endregion

    #endregion

    private static RenderStandard? _renderStandard;
    private static RenderProfessional? _renderProfessional;
    private static RenderOffice2007? _renderOffice2007;
    private static RenderOffice2010? _renderOffice2010;
    private static RenderOffice2013? _renderOffice2013;
    private static RenderMicrosoft365? _renderMicrosoft365;
    private static RenderMaterial? _renderMaterial;
    private static RenderRetro? _renderRetro;
    private static RenderMacOSXAqua? _renderMacOSXAqua;
    private static RenderMacOS? _renderMacOS;
    private static RenderSparkle? _renderSparkle;
    private static RenderVisualStudio2010With2007? _renderVisualStudio2010With2007;
    private static RenderVisualStudio2010With2010? _renderVisualStudio2010With2010;
    private static RenderVisualStudio2010With2013? _renderVisualStudio2010With2013;
    private static RenderVisualStudio2010WithMicrosoft365? _renderVisualStudio2010WithMicrosoft365;
    private static RenderVisualStudio? _renderVisualStudio;

    #endregion

    #region Static Events

    /// <summary>
    /// Occurs when the palette changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the value of the GlobalPalette property is changed.")]
    public static event EventHandler? GlobalPaletteChanged;

    /// <summary>
    /// Occurs when the UseThemeFormChromeBorderWidth property changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the value of the GlobalUseThemeFormChromeBorderWidth property is changed.")]
    public static event EventHandler? GlobalUseThemeFormChromeBorderWidthChanged;

    /// <summary>
    /// Occurs when the drop-down arrow render mode changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the value of the GlobalDropDownArrowRenderMode property is changed.")]
    public static event EventHandler? GlobalDropDownArrowRenderModeChanged;

    /// <summary>
    /// Occurs when the value of the GlobalDropDownArrowGlyphStyle property is changed.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the value of the GlobalDropDownArrowGlyphStyle property is changed.")]
    public static event EventHandler? GlobalDropDownArrowGlyphStyleChanged;

    /// <summary>
    /// Occurs when the touchscreen support setting or scale factor changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the value of the GlobalTouchscreenSupport or GlobalTouchscreenScaleFactor property is changed.")]
    public static event EventHandler? GlobalTouchscreenSupportChanged;

    /// <summary>
    /// Occurs when touchscreen availability changes (detected or removed).
    /// This event is fired when AutomaticallyDetectTouchscreen is enabled and the system detects
    /// that a touchscreen has been connected or disconnected.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when touchscreen availability changes (detected or removed).")]
    public static event EventHandler<TouchscreenAvailabilityChangedEventArgs>? TouchscreenAvailabilityChanged;

    #endregion

    #region Identity
    static KryptonManager()
    {
        // We need to notice when system color settings change
        SystemEvents.UserPreferenceChanged += OnUserPreferenceChanged;

        // Align toolbar image storage with the startup theme before any change event
        UpdatePaletteImages(CurrentGlobalPaletteMode);

        // Update the tool strip global renderer with the default setting
        UpdateToolStripManager();

        // Probe the application base directory for a Translations file and load it if found.
        RunAutoDiscovery();
    }

    /// <summary>
    /// Initialize a new instance of the KryptonManager class.
    /// </summary>
    public KryptonManager()
    {
    }

    /// <summary>
    /// Initialize a new instance of the KryptonManager class.
    /// </summary>
    /// <param name="container">Container that owns the component.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public KryptonManager([DisallowNull] IContainer container)
        : this()
    {
        Debug.Assert(container != null);

        // Validate reference parameter
        if (container == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(container));
        }

        container.Add(this);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public

    /// <summary>
    /// Have any of the global values been modified
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsDefault => !(ShouldSerializeGlobalCustomPalette() ||
                               ShouldSerializeToolkitColors() ||
                               ShouldSerializeGlobalApplyToolstrips() ||
                               ShouldSerializeGlobalUseThemeFormChromeBorderWidth() ||
                               ShouldSerializeShowAdministratorSuffix() ||
                               ShouldSerializeToolkitStrings() ||
                               ShouldSerializeUseKryptonFileDialogs() ||
                               ShouldSerializeGlobalUseKryptonScrollbars() ||
                               ShouldSerializeGlobalScrollbarCornerStyle() ||
                               ShouldSerializeGlobalDropDownArrowRenderMode() ||
                               ShouldSerializeGlobalDropDownArrowGlyphStyle() ||
                               ShouldSerializeBaseFont() ||
                               ShouldSerializeGlobalPaletteMode() ||
                               ShouldSerializeTouchscreenSettings());

    /// <summary>
    /// Reset All values
    /// </summary>
    public void Reset()
    {
        ResetGlobalCustomPalette();
        ResetToolkitColors();
        ResetGlobalApplyToolstrips();
        ResetGlobalUseThemeFormChromeBorderWidth();
        ResetShowAdministratorSuffix();
        ResetToolkitStrings();
        ResetUseKryptonFileDialogs();
        ResetGlobalUseKryptonScrollbars();
        ResetGlobalScrollbarCornerStyle();
        ResetGlobalDropDownArrowRenderMode();
        ResetGlobalDropDownArrowGlyphStyle();
        ResetBaseFont();
        ResetGlobalPaletteMode();
        ResetTouchscreenSettings();
    }

    /// <summary>
    /// Gets or sets the global palette used for drawing.
    /// </summary>
    [Category(@"GlobalPalette")]
    [Description(@"Easy Set for the theme palette")]
    [DefaultValue(PaletteMode.Microsoft365Blue)]
    public PaletteMode GlobalPaletteMode
    {
        get => CurrentGlobalPaletteMode;
        set
        {
            if (value != CurrentGlobalPaletteMode)
            {
                if (value != PaletteMode.Custom)
                {
                    // Get a reference to the standard palette from its name
                    SetPalette(GetPaletteForMode(value));
                }
                CurrentGlobalPaletteMode = value;
                if (_baseFont != null)
                {
                    CurrentGlobalPalette.BaseFont = _baseFont;
                }

                if (value != PaletteMode.Custom)
                {
                    // Raise the palette changed event
                    OnGlobalPaletteChanged(EventArgs.Empty);
                }
            }
        }
    }
    private bool ShouldSerializeGlobalPaletteMode() => GlobalPaletteMode != ThemeManager.DefaultGlobalPalette;
    private void ResetGlobalPaletteMode() => GlobalPaletteMode = ThemeManager.DefaultGlobalPalette;

    /// <summary>
    /// Gets and sets the global custom applied to drawing.
    /// </summary>
    [Category(@"GlobalPalette")]
    [Description(@"Global custom palette applied to drawing.")]
    [DefaultValue(null)]
    public KryptonCustomPaletteBase? GlobalCustomPalette
    {
        get => CurrentGlobalPalette as KryptonCustomPaletteBase;

        set
        {
            // Only interested in changes of value
            if (CurrentGlobalPalette != value)
            {
                if (value != null)
                {
                    // If no custom palette is required
                    CurrentGlobalPalette = value;
                    // Use the provided palette value
                    SetPalette(value);
                    CurrentGlobalPaletteMode = GetModeForPalette(value);
                    // Notify the KryptonManager that there is a custom palette assigned to it
                    // Fixes bug: https://github.com/Krypton-Suite/Standard-Toolkit/issues/1092
                    GlobalPaletteMode = PaletteMode.Custom;
                }
                else
                {
                    ResetGlobalPaletteMode();
                    CurrentGlobalPalette = GetPaletteForMode(GlobalPaletteMode);
                }
                // Raise the palette changed event
                OnGlobalPaletteChanged(EventArgs.Empty);
            }
        }
    }
    private void ResetGlobalCustomPalette()
    {
        GlobalCustomPalette = null;
        ResetGlobalPaletteMode();
    }
    private bool ShouldSerializeGlobalCustomPalette() => GlobalCustomPalette != null;

    /// <summary>Override the Current global palette font.</summary>
    [Category(@"GlobalPalette")]
    [Description(@"Override the Current global palette font.")]
    [AllowNull]
    public Font BaseFont
    {
        get => _baseFont ?? CurrentGlobalPalette.BaseFont;

        set
        {
            if (value != null)
            {
                _baseFont = value;
                CurrentGlobalPalette.BaseFont = value;
            }
            else
            {
                ResetBaseFont();
            }

            OnGlobalPaletteChanged(EventArgs.Empty);
        }
    }

    private void ResetBaseFont()
    {
        _baseFont = null;
        CurrentGlobalPalette.ResetBaseFont();
    }
    private bool ShouldSerializeBaseFont() => _baseFont != null;

    /// <summary>
    /// Gets or sets a value indicating if the palette colors are applied to the tool-strips.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should the palette colors be applied to the toolstrips.")]
    [DefaultValue(true)]
    public bool GlobalApplyToolstrips
    {
        get => ApplyToolstrips;
        set => ApplyToolstrips = value;
    }
    private bool ShouldSerializeGlobalApplyToolstrips() => !GlobalApplyToolstrips;
    private void ResetGlobalApplyToolstrips() => GlobalApplyToolstrips = true;

    /// <summary>Gets or sets a value indicating whether [use krypton file dialogs for internal openings like CustomPalette Import].</summary>
    /// <value><c>true</c> if [use krypton file dialogs]; otherwise, <c>false</c>.</value>
    [Category(@"Visuals")]
    [Description(@"Should use krypton file dialogs for internal openings like CustomPalette Import")]
    [DefaultValue(true)]
    public bool UseKryptonFileDialogs
    {
        get => _globalUseKryptonFileDialogs;
        set => _globalUseKryptonFileDialogs = value;
    }
    private bool ShouldSerializeUseKryptonFileDialogs() => !UseKryptonFileDialogs;
    private void ResetUseKryptonFileDialogs() => UseKryptonFileDialogs = true;

    /// <summary>
    /// Gets or sets a value indicating whether scrollable controls should use Krypton-themed scrollbars instead of native scrollbars.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should scrollable controls use Krypton-themed scrollbars instead of native scrollbars.")]
    [DefaultValue(false)]
    public bool GlobalUseKryptonScrollbars
    {
        get => UseKryptonScrollbars;
        set => UseKryptonScrollbars = value;
    }
    private bool ShouldSerializeGlobalUseKryptonScrollbars() => GlobalUseKryptonScrollbars;
    private void ResetGlobalUseKryptonScrollbars() => GlobalUseKryptonScrollbars = false;

    /// <summary>
    /// Gets or sets the global default for how Krypton scrollbar managers fill the bottom-right corner when both scrollbars are visible.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Global default for how Krypton scrollbar managers fill the bottom-right corner when both scrollbars are visible (ThemedCorner or ExtendHorizontal).")]
    [DefaultValue(ScrollbarCornerStyle.ThemedCorner)]
    public ScrollbarCornerStyle GlobalScrollbarCornerStyle
    {
        get => ScrollbarCornerStyle;
        set => ScrollbarCornerStyle = value;
    }
    private bool ShouldSerializeGlobalScrollbarCornerStyle() => GlobalScrollbarCornerStyle != ScrollbarCornerStyle.ThemedCorner;
    private void ResetGlobalScrollbarCornerStyle() => GlobalScrollbarCornerStyle = ScrollbarCornerStyle.ThemedCorner;

    /// <summary>
    /// Gets or sets how drop-down arrow glyphs are rendered across Krypton controls.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"How drop-down arrow glyphs are rendered: Unicode characters (default) or pixel-aligned polygons.")]
    [DefaultValue(DropDownArrowRenderMode.Unicode)]
    public DropDownArrowRenderMode GlobalDropDownArrowRenderMode
    {
        get => DropDownArrowRenderMode;
        set => DropDownArrowRenderMode = value;
    }
    private bool ShouldSerializeGlobalDropDownArrowRenderMode() => GlobalDropDownArrowRenderMode != DropDownArrowRenderMode.Unicode;
    private void ResetGlobalDropDownArrowRenderMode() => GlobalDropDownArrowRenderMode = DropDownArrowRenderMode.Unicode;

    /// <summary>
    /// Gets or sets how two-tone drop-down arrow glyphs are composited (flat, bevel, or emboss).
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"How two-tone drop-down arrow glyphs are composited: Flat, Bevel (raised), or Emboss (inset).")]
    [DefaultValue(DropDownArrowGlyphStyle.Bevel)]
    public DropDownArrowGlyphStyle GlobalDropDownArrowGlyphStyle
    {
        get => DropDownArrowGlyphStyle;
        set => DropDownArrowGlyphStyle = value;
    }
    private bool ShouldSerializeGlobalDropDownArrowGlyphStyle() => GlobalDropDownArrowGlyphStyle != DropDownArrowGlyphStyle.Bevel;
    private void ResetGlobalDropDownArrowGlyphStyle() => GlobalDropDownArrowGlyphStyle = DropDownArrowGlyphStyle.Bevel;

    /// <summary>
    /// Gets or sets a value indicating if KryptonForm instances are allowed to UseThemeFormChromeBorderWidth.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should KryptonForm instances be allowed to UseThemeFormChromeBorderWidth.")]
    [DefaultValue(true)]
    public bool GlobalUseThemeFormChromeBorderWidth
    {
        get => UseThemeFormChromeBorderWidth;
        set => UseThemeFormChromeBorderWidth = value;
    }
    private bool ShouldSerializeGlobalUseThemeFormChromeBorderWidth() => !GlobalUseThemeFormChromeBorderWidth;
    private void ResetGlobalUseThemeFormChromeBorderWidth() => GlobalUseThemeFormChromeBorderWidth = true;

    /// <summary>Gets the toolkit strings that can be localised.</summary>
    [Category(@"Data")]
    [Description(@"A collection of global toolkit strings that can be localised.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public KryptonGlobalToolkitStrings ToolkitStrings => Strings;
    private bool ShouldSerializeToolkitStrings() => !Strings.IsDefault;
    private void ResetToolkitStrings() => Strings.Reset();

    /// <summary>Gets the toolkit colors.</summary>
    /// <value>The toolkit colors.</value>
    [Category(@"Data")]
    [Description(@"A collection of global toolkit colors.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonColorStorage ToolkitColors => Colors;

    private bool ShouldSerializeToolkitColors() => !Colors.IsDefault;

    private void ResetToolkitColors() => Colors.Reset();

    /// <summary>
    /// Gets or sets a value indicating if the administrator suffix should be shown in KryptonForm title bars.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should the administrator suffix be shown in KryptonForm title bars when running with elevated privileges.")]
    [DefaultValue(true)]
    public bool ShowAdministratorSuffix
    {
        get => UseAdministratorSuffix;
        set => UseAdministratorSuffix = value;
    }
    private bool ShouldSerializeShowAdministratorSuffix() => !UseAdministratorSuffix;
    private void ResetShowAdministratorSuffix() => UseAdministratorSuffix = true;

    /// <summary>
    /// Gets the touchscreen support settings.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Settings for touchscreen support, including control and font scaling.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public TouchscreenSettingValues TouchscreenSettings => TouchscreenSettingValues;
    private bool ShouldSerializeTouchscreenSettings() => !TouchscreenSettingValues.IsDefault;
    private void ResetTouchscreenSettings() => TouchscreenSettingValues.Reset();


    #endregion

    #region Static Properties

    /// <summary>Gets the strings.</summary>
    /// <value>The strings.</value>
    public static KryptonGlobalToolkitStrings Strings { get; } = new KryptonGlobalToolkitStrings();

    /// <summary>
    /// Gets or sets whether <see cref="KryptonManager"/> automatically probes the application's base
    /// directory for culture-specific and default translation files at type-initialisation time and
    /// loads the best match if found.  Defaults to <c>true</c>.
    /// </summary>
    /// <remarks>
    /// Set to <c>false</c> before the first use of any Krypton type to suppress auto-discovery,
    /// for example in unit-test hosts or apps that manage translations entirely in code.
    /// Auto-discovery is silent — any I/O or parse errors are swallowed and traced to
    /// <see cref="System.Diagnostics.Debug"/>.
    /// Probe order: exact culture, neutral culture, then default basename; XML is preferred over JSON
    /// at each level (for example <c>Translations.en-GB.xml</c> → <c>Translations.en.xml</c> →
    /// <c>Translations.xml</c>, then the same sequence for <c>.json</c>).
    /// </remarks>
    public static bool AutoDiscoverTranslations { get; set; } = true;

    /// <summary>
    /// Occurs after toolkit translations have been successfully imported via any of the load/import methods.
    /// </summary>
    public static event EventHandler? TranslationsImported;

    /// <summary>
    /// Occurs after a translations file has been analyzed for catalog coverage (missing/extra keys),
    /// including automatically during tolerant import.
    /// </summary>
    public static event EventHandler<ToolkitStringsCoverageEventArgs>? TranslationsCoverageReported;

    /// <summary>
    /// Loads toolkit strings from the specified Translations.xml or Translations.json file, replacing current values.
    /// Call this at application startup, before any Krypton controls are shown.
    /// </summary>
    /// <param name="path">Path to the translations file to load.</param>
    /// <param name="refreshOpenForms">When <c>true</c>, invalidates and refreshes all open forms after import.</param>
    /// <exception cref="System.IO.FileNotFoundException">Thrown when the specified file does not exist.</exception>
    public static void LoadTranslationsFromFile(string path, bool refreshOpenForms = false)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(path));
        }

        if (System.IO.Path.GetExtension(path).Equals(@".json", System.StringComparison.OrdinalIgnoreCase))
        {
            Strings.ImportFromJsonFile(path, resetFirst: true, refreshOpenForms: refreshOpenForms);
        }
        else
        {
            Strings.ImportFromXmlFile(path, resetFirst: true, refreshOpenForms: refreshOpenForms);
        }

        OnTranslationsImported();
    }

    /// <summary>
    /// Attempts to load toolkit strings from the specified Translations.xml or Translations.json file.
    /// Returns <c>false</c> (and writes a debug trace) if the file does not exist or cannot be parsed, without throwing.
    /// </summary>
    /// <param name="path">Path to the translations file to load.</param>
    /// <param name="refreshOpenForms">When <c>true</c>, invalidates and refreshes all open forms after import.</param>
    /// <returns><c>true</c> if translations were loaded successfully; <c>false</c> otherwise.</returns>
    public static bool TryLoadTranslationsFromFile(string path, bool refreshOpenForms = false)
    {
        if (string.IsNullOrWhiteSpace(path) || !System.IO.File.Exists(path))
        {
            return false;
        }

        try
        {
            LoadTranslationsFromFile(path, refreshOpenForms);
            return true;
        }
        catch (System.Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($@"[Krypton] TryLoadTranslationsFromFile failed for '{path}': {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Gets the culture last applied by <see cref="TrySwitchTranslationsCulture(CultureInfo, string?, string, bool)"/>
    /// or a successful culture-specific load. May be <c>null</c> before any switch has occurred.
    /// </summary>
    public static CultureInfo? ActiveTranslationsCulture { get; private set; }

    /// <summary>
    /// Attempts to load the best matching culture-specific translations file from a directory.
    /// Probe order is exact culture, neutral culture, then default basename, preferring XML over JSON
    /// at each level. Failures are swallowed gracefully and the next candidate is tried.
    /// </summary>
    /// <param name="directory">Directory containing the translation files. When null/empty, uses the application base directory.</param>
    /// <param name="culture">Culture to resolve. When null, uses <see cref="CultureInfo.CurrentUICulture"/>.</param>
    /// <param name="baseName">Base file name without culture suffix or extension. Defaults to <c>Translations</c>.</param>
    /// <param name="refreshOpenForms">When <c>true</c>, invalidates and refreshes all open forms after import.</param>
    /// <returns><c>true</c> if a file was found and loaded successfully; otherwise, <c>false</c>.</returns>
    public static bool TryLoadCultureSpecificTranslations(
        string? directory = null,
        CultureInfo? culture = null,
        string baseName = @"Translations",
        bool refreshOpenForms = false)
    {
        if (string.IsNullOrWhiteSpace(baseName))
        {
            return false;
        }

        var baseDir = string.IsNullOrWhiteSpace(directory)
            ? System.AppDomain.CurrentDomain.BaseDirectory
            : directory;

        if (string.IsNullOrWhiteSpace(baseDir))
        {
            baseDir = System.IO.Directory.GetCurrentDirectory();
        }

        var resolvedCulture = culture ?? CultureInfo.CurrentUICulture;
        foreach (var candidate in BuildCultureSpecificCandidates(baseDir!, baseName, resolvedCulture))
        {
            if (TryLoadTranslationsFromFile(candidate, refreshOpenForms))
            {
                ActiveTranslationsCulture = resolvedCulture;
                System.Diagnostics.Debug.WriteLine($@"[Krypton] Loaded culture-specific translations from '{candidate}'.");
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Switches the current UI culture and reloads the best matching translations file for that culture.
    /// When no matching file is found, toolkit strings are reset to built-in defaults so a previous
    /// culture's translations are not left applied.
    /// </summary>
    /// <param name="culture">The culture to switch to.</param>
    /// <param name="directory">Directory containing the translation files. When null/empty, uses the application base directory.</param>
    /// <param name="baseName">Base file name without culture suffix or extension. Defaults to <c>Translations</c>.</param>
    /// <param name="refreshOpenForms">When <c>true</c>, invalidates and refreshes all open forms after the switch.</param>
    /// <returns>
    /// <c>true</c> when a culture-specific or fallback translations file was loaded;
    /// <c>false</c> when no file was found and built-in defaults were restored.
    /// The UI culture is updated in both cases.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="culture"/> is <c>null</c>.</exception>
    public static bool TrySwitchTranslationsCulture(
        CultureInfo culture,
        string? directory = null,
        string baseName = @"Translations",
        bool refreshOpenForms = true)
    {
        if (culture == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(culture));
        }

        ApplyUiCulture(culture);
        OsMuiStringLoader.ClearCache();

        if (TryLoadCultureSpecificTranslations(directory, culture, baseName, refreshOpenForms))
        {
            return true;
        }

        // No matching file — clear any previously loaded translations for the prior culture.
        Strings.Reset();
        ActiveTranslationsCulture = culture;
        if (refreshOpenForms)
        {
            ToolkitStringsXmlPersistence.RefreshOpenFormsBestEffort();
        }

        OnTranslationsImported();
        System.Diagnostics.Debug.WriteLine(
            $@"[Krypton] Switched UI culture to '{culture.Name}' with no matching translations file; restored built-in defaults.");
        return false;
    }

    /// <summary>
    /// Switches the current UI culture using a culture name (for example <c>fr-FR</c>) and reloads
    /// the best matching translations file.
    /// </summary>
    /// <param name="cultureName">Culture name recognised by <see cref="CultureInfo"/>.</param>
    /// <param name="directory">Directory containing the translation files. When null/empty, uses the application base directory.</param>
    /// <param name="baseName">Base file name without culture suffix or extension. Defaults to <c>Translations</c>.</param>
    /// <param name="refreshOpenForms">When <c>true</c>, invalidates and refreshes all open forms after the switch.</param>
    /// <returns>
    /// <c>true</c> when a translations file was loaded; <c>false</c> when the culture name is invalid
    /// or no matching file was found (built-in defaults restored after a valid culture switch).
    /// </returns>
    public static bool TrySwitchTranslationsCulture(
        string cultureName,
        string? directory = null,
        string baseName = @"Translations",
        bool refreshOpenForms = true)
    {
        if (string.IsNullOrWhiteSpace(cultureName))
        {
            return false;
        }

        try
        {
            return TrySwitchTranslationsCulture(new CultureInfo(cultureName), directory, baseName, refreshOpenForms);
        }
        catch (CultureNotFoundException ex)
        {
            System.Diagnostics.Debug.WriteLine($@"[Krypton] TrySwitchTranslationsCulture failed for '{cultureName}': {ex.Message}");
            return false;
        }
    }

    private static void ApplyUiCulture(CultureInfo culture)
    {
        Thread.CurrentThread.CurrentUICulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }

    /// <summary>
    /// Raises the <see cref="TranslationsImported"/> event.
    /// </summary>
    internal static void OnTranslationsImported() =>
        TranslationsImported?.Invoke(null, EventArgs.Empty);

    /// <summary>
    /// Raises the <see cref="TranslationsCoverageReported"/> event.
    /// </summary>
    internal static void OnTranslationsCoverageReported(ToolkitStringsCoverage coverage) =>
        TranslationsCoverageReported?.Invoke(null, new ToolkitStringsCoverageEventArgs(coverage));

    /// <summary>
    /// Analyzes a translations XML or JSON file against the live toolkit string catalog without applying it.
    /// </summary>
    /// <param name="path">Path to the translations file.</param>
    /// <returns>Coverage describing missing, extra, and applied keys.</returns>
    public static ToolkitStringsCoverage AnalyzeTranslationsFromFile(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(path));
        }

        return Strings.AnalyzeTranslationsFromFile(path);
    }

    /// <summary>
    /// Imports an existing translations file and rewrites it with any newly added toolkit keys filled from defaults.
    /// Already-translated values are preserved.
    /// </summary>
    /// <param name="path">Path to the XML or JSON translations file to upgrade.</param>
    /// <param name="includeDefaults">When <c>true</c>, the rewritten file contains the full catalog (recommended for translators).</param>
    /// <returns>Post-merge coverage for the rewritten file.</returns>
    public static ToolkitStringsCoverage MergeMissingTranslationsToFile(string path, bool includeDefaults = true)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(path));
        }

        return Strings.MergeMissingTranslationsToFile(path, includeDefaults);
    }

    private static void RunAutoDiscovery()
    {
        if (!AutoDiscoverTranslations)
        {
            return;
        }

        // Culture-aware probe with graceful fallback: exact → neutral → default; XML before JSON.
        TryLoadCultureSpecificTranslations(refreshOpenForms: false);
    }

    private static System.Collections.Generic.IEnumerable<string> BuildCultureSpecificCandidates(
        string directory,
        string baseName,
        CultureInfo culture)
    {
        var cultureName = culture?.Name ?? string.Empty;
        var neutralName = string.Empty;

        if (!string.IsNullOrEmpty(cultureName))
        {
            // Prefer Parent when available (en-GB → en); fall back to a two-letter prefix.
            if (culture != null && culture.Parent != null && !string.IsNullOrEmpty(culture.Parent.Name))
            {
                neutralName = culture.Parent.Name;
            }
            else if (cultureName.Length >= 2)
            {
                neutralName = cultureName.Substring(0, 2);
            }
        }

        var names = new System.Collections.Generic.List<string>();

        void AddUnique(string name)
        {
            if (!string.IsNullOrWhiteSpace(name) &&
                !names.Exists(existing => string.Equals(existing, name, System.StringComparison.OrdinalIgnoreCase)))
            {
                names.Add(name);
            }
        }

        // Exact culture first, then neutral, then unadorned default.
        AddUnique(cultureName);
        AddUnique(neutralName);
        names.Add(string.Empty);

        // Prefer XML over JSON at each culture level.
        foreach (var name in names)
        {
            var fileStem = string.IsNullOrEmpty(name) ? baseName : $@"{baseName}.{name}";
            yield return System.IO.Path.Combine(directory, $@"{fileStem}.xml");
        }

        foreach (var name in names)
        {
            var fileStem = string.IsNullOrEmpty(name) ? baseName : $@"{baseName}.{name}";
            yield return System.IO.Path.Combine(directory, $@"{fileStem}.json");
        }
    }

    /// <summary>Gets the images.</summary>
    /// <value>The images.</value>
    public static KryptonImageStorage Images { get; } = new KryptonImageStorage();

    /// <summary>Gets the colors.</summary>
    /// <value>The colors.</value>
    public static KryptonColorStorage Colors { get; } = new KryptonColorStorage();

    /// <summary>
    /// Gets or sets the optional application-wide default for semantic dialog button colours.
    /// </summary>
    /// <remarks>
    /// Used by <see cref="KryptonMessageBox"/>, <see cref="KryptonTaskDialog"/>, and related dialogs
    /// when the call site does not supply <see cref="KryptonDialogButtonColorOptions"/>.
    /// Null (the default) leaves themed Standalone button chrome unchanged.
    /// </remarks>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static KryptonDialogButtonColorOptions? DialogButtonColors
    {
        get => _globalDialogButtonColors;
        set => _globalDialogButtonColors = value;
    }

    /// <summary>Gets the touchscreen support settings.</summary>
    /// <value>The touchscreen support settings.</value>
    public static TouchscreenSettingValues TouchscreenSettingValues { get; } = new TouchscreenSettingValues();

    #region Static ShowAdministratorSuffix
    /// <summary>
    /// Gets and sets the global flag that decides if administrator suffix should be shown in KryptonForm title bars.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public static bool UseAdministratorSuffix
    {
        get => _globalShowAdministratorSuffix;

        set
        {
            // Only interested if the value changes
            if (_globalShowAdministratorSuffix != value)
            {
                // Use new value
                _globalShowAdministratorSuffix = value;
            }
        }
    }
    #endregion

    #endregion

    #region Static ApplyToolstrips
    /// <summary>
    /// Gets and sets the global flag that decides if palette colors are applied to toolstrips.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public static bool ApplyToolstrips
    {
        get => _globalApplyToolstrips;

        set
        {
            // Only interested if the value changes
            if (_globalApplyToolstrips != value)
            {
                // Use new value
                _globalApplyToolstrips = value;

                // Change the toolstrip manager renderer as required
                if (_globalApplyToolstrips)
                {
                    UpdateToolStripManager();
                }
                else
                {
                    ResetToolStripManager();
                }
            }
        }
    }
    #endregion

    #region Static UseThemeFormChromeBorderWidth
    /// <summary>
    /// Gets and sets the global flag that decides if form chrome should be customized.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public static bool UseThemeFormChromeBorderWidth
    {
        get => _globalUseThemeFormChromeBorderWidth;

        set
        {
            // Only interested if the value changes
            if (_globalUseThemeFormChromeBorderWidth != value)
            {
                // Use new value
                _globalUseThemeFormChromeBorderWidth = value;

                // Fire change event
                OnGlobalUseThemeFormChromeBorderWidthChanged(EventArgs.Empty);
            }
        }
    }
    #endregion

    #region Static UseKryptonScrollbars
    /// <summary>
    /// Gets and sets the global flag that decides if scrollable controls should use Krypton-themed scrollbars instead of native scrollbars.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public static bool UseKryptonScrollbars
    {
        get => _globalUseKryptonScrollbars;

        set
        {
            // Only interested if the value changes
            if (_globalUseKryptonScrollbars != value)
            {
                // Use new value
                _globalUseKryptonScrollbars = value;
            }
        }
    }
    #endregion

    #region Static ScrollbarCornerStyle
    /// <summary>
    /// Gets and sets the global default for how Krypton scrollbar managers fill the bottom-right
    /// corner when both scrollbars are visible. Individual managers can override this via
    /// <see cref="KryptonScrollbarManager.CornerStyle"/>.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public static ScrollbarCornerStyle ScrollbarCornerStyle
    {
        get => _globalScrollbarCornerStyle;

        set
        {
            // Only interested if the value changes
            if (_globalScrollbarCornerStyle != value)
            {
                // Use new value
                _globalScrollbarCornerStyle = value;
            }
        }
    }
    #endregion

    #region Static DropDownArrowRenderMode
    /// <summary>
    /// Gets and sets how drop-down arrow glyphs are rendered across Krypton controls.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public static DropDownArrowRenderMode DropDownArrowRenderMode
    {
        get => _globalDropDownArrowRenderMode;

        set
        {
            if (_globalDropDownArrowRenderMode != value)
            {
                _globalDropDownArrowRenderMode = value;
                DropDownArrowGlyphCache.Clear();
                OnGlobalDropDownArrowRenderModeChanged(EventArgs.Empty);
            }
        }
    }
    #endregion

    #region Static DropDownArrowGlyphStyle
    /// <summary>
    /// Gets and sets how two-tone drop-down arrow glyphs are composited across Krypton controls.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public static DropDownArrowGlyphStyle DropDownArrowGlyphStyle
    {
        get => _globalDropDownArrowGlyphStyle;

        set
        {
            if (_globalDropDownArrowGlyphStyle != value)
            {
                _globalDropDownArrowGlyphStyle = value;
                DropDownArrowGlyphCache.Clear();
                OnGlobalDropDownArrowGlyphStyleChanged(EventArgs.Empty);
            }
        }
    }
    #endregion

    #region Static UseTouchscreenSupport
    /// <summary>
    /// Gets and sets the global flag that decides if touchscreen support is enabled, making controls larger based on the scale factor.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public static bool UseTouchscreenSupport
    {
        get => _globalTouchscreenMode;

        set
        {
            // Only interested if the value changes
            if (_globalTouchscreenMode != value)
            {
                // Use new value (volatile write ensures visibility across threads)
                _globalTouchscreenMode = value;

                // Fire change event to notify controls to refresh
                OnGlobalTouchscreenSupportChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Gets and sets the global scale factor applied to controls when touchscreen support is enabled.
    /// </summary>
    /// <remarks>
    /// A value of 1.25 means controls will be 25% larger. Must be greater than 0.
    /// </remarks>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public static float TouchscreenScaleFactorValue
    {
        get => _globalTouchscreenScaleFactor;

        set
        {
            if (value <= 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(nameof(value), value, @"Scale factor must be greater than 0.");
            }

            // Only interested if the value changes
            if (Math.Abs(_globalTouchscreenScaleFactor - value) > 0.001f)
            {
                // Use new value (volatile write ensures visibility across threads)
                _globalTouchscreenScaleFactor = value;

                // Fire change event to notify controls to refresh (only if touchscreen support is enabled)
                if (_globalTouchscreenMode)
                {
                    OnGlobalTouchscreenSupportChanged(EventArgs.Empty);
                }
            }
        }
    }

    /// <summary>
    /// Gets the touchscreen scale factor. Returns the configured scale factor when touchscreen support is enabled, otherwise 1.0.
    /// </summary>
    public static float TouchscreenScaleFactor => UseTouchscreenSupport ? _globalTouchscreenScaleFactor : 1.0f;

    /// <summary>
    /// Gets and sets the global flag that decides if font scaling is enabled when touchscreen support is enabled.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public static bool UseTouchscreenFontScaling
    {
        get => _globalTouchscreenFontScaling;

        set
        {
            // Only interested if the value changes
            if (_globalTouchscreenFontScaling != value)
            {
                // Use new value (volatile write ensures visibility across threads)
                _globalTouchscreenFontScaling = value;

                // Fire change event to notify controls to refresh (only if touchscreen support is enabled)
                if (_globalTouchscreenMode)
                {
                    OnGlobalTouchscreenSupportChanged(EventArgs.Empty);
                }
            }
        }
    }

    /// <summary>
    /// Gets and sets the global font scale factor applied to fonts when font scaling is enabled.
    /// </summary>
    /// <remarks>
    /// A value of 1.25 means fonts will be 25% larger. Must be greater than 0.
    /// </remarks>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public static float TouchscreenFontScaleFactorValue
    {
        get => _globalTouchscreenFontScaleFactor;

        set
        {
            if (value <= 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(nameof(value), value, @"Font scale factor must be greater than 0.");
            }

            // Only interested if the value changes
            if (Math.Abs(_globalTouchscreenFontScaleFactor - value) > 0.001f)
            {
                // Use new value (volatile write ensures visibility across threads)
                _globalTouchscreenFontScaleFactor = value;

                // Fire change event to notify controls to refresh (only if touchscreen support and font scaling are enabled)
                if (_globalTouchscreenMode && _globalTouchscreenFontScaling)
                {
                    OnGlobalTouchscreenSupportChanged(EventArgs.Empty);
                }
            }
        }
    }

    /// <summary>
    /// Gets the touchscreen font scale factor. Returns the configured font scale factor when touchscreen support and font scaling are enabled, otherwise 1.0.
    /// </summary>
    public static float TouchscreenFontScaleFactor => (UseTouchscreenSupport && UseTouchscreenFontScaling) ? _globalTouchscreenFontScaleFactor : 1.0f;

    /// <summary>
    /// Gets and sets a value indicating whether touchscreen support should be automatically detected and enabled.
    /// When set to true, the system will automatically check for touchscreen capability and enable/disable touchscreen support accordingly.
    /// Periodic polling will be enabled to detect hot-plug scenarios.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public static bool AutomaticallyDetectTouchscreen
    {
        get => _globalAutomaticallyDetectTouchscreen;
        set
        {
            if (_globalAutomaticallyDetectTouchscreen != value)
            {
                _globalAutomaticallyDetectTouchscreen = value;

                if (value)
                {
                    // Initialize last detected state
                    _lastDetectedTouchscreenState = IsTouchscreenAvailable();

                    // Perform detection immediately
                    PerformTouchscreenDetection();

                    // Start periodic polling
                    StartTouchscreenDetectionTimer();
                }
                else
                {
                    // Stop periodic polling
                    StopTouchscreenDetectionTimer();
                }
            }
        }
    }

    /// <summary>
    /// Gets and sets the interval (in milliseconds) for periodic touchscreen detection polling.
    /// This is used when AutomaticallyDetectTouchscreen is enabled to detect hot-plug scenarios.
    /// Default is 2000ms (2 seconds). Minimum value is 500ms.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public static int TouchscreenDetectionInterval
    {
        get => _globalTouchscreenDetectionInterval;
        set
        {
            if (value < 500)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(nameof(value), value, @"Detection interval must be at least 500 milliseconds.");
            }

            if (_globalTouchscreenDetectionInterval != value)
            {
                _globalTouchscreenDetectionInterval = value;

                // Restart timer with new interval if auto-detection is enabled
                if (_globalAutomaticallyDetectTouchscreen)
                {
                    StartTouchscreenDetectionTimer();
                }
            }
        }
    }

    /// <summary>
    /// Performs touchscreen detection and updates the Enabled property if AutomaticallyDetectTouchscreen is true.
    /// This method is called automatically when AutomaticallyDetectTouchscreen is enabled, but can also be called manually.
    /// </summary>
    private static void PerformTouchscreenDetection()
    {
        if (!_globalAutomaticallyDetectTouchscreen)
        {
            return; // Auto-detection is disabled
        }

        bool isTouchscreenAvailable = IsTouchscreenAvailable();
        int maximumTouchContacts = GetMaximumTouchContacts();

        // Check if availability has changed
        bool availabilityChanged = _lastDetectedTouchscreenState != isTouchscreenAvailable;

        // Only update if the current state differs from detected state
        if (_globalTouchscreenMode != isTouchscreenAvailable)
        {
            // Update the enabled state without triggering change events (we'll fire it once at the end)
            _globalTouchscreenMode = isTouchscreenAvailable;

            // Fire change event to notify controls to refresh
            OnGlobalTouchscreenSupportChanged(EventArgs.Empty);
        }

        // Fire availability changed event if the detected state changed
        if (availabilityChanged)
        {
            _lastDetectedTouchscreenState = isTouchscreenAvailable;
            OnTouchscreenAvailabilityChanged(new TouchscreenAvailabilityChangedEventArgs(isTouchscreenAvailable, maximumTouchContacts));
        }
    }

    /// <summary>
    /// Starts periodic touchscreen detection polling.
    /// </summary>
    private static void StartTouchscreenDetectionTimer()
    {
        StopTouchscreenDetectionTimer();

        if (!_globalAutomaticallyDetectTouchscreen)
        {
            return;
        }

        _touchscreenDetectionTimer = new System.Threading.Timer(TouchscreenDetectionTimer_Tick, null, _globalTouchscreenDetectionInterval, _globalTouchscreenDetectionInterval);
    }

    /// <summary>
    /// Stops periodic touchscreen detection polling.
    /// </summary>
    private static void StopTouchscreenDetectionTimer()
    {
        if (_touchscreenDetectionTimer != null)
        {
            _touchscreenDetectionTimer.Dispose();
            _touchscreenDetectionTimer = null;
        }
    }

    /// <summary>
    /// Timer callback for periodic touchscreen detection.
    /// </summary>
    private static void TouchscreenDetectionTimer_Tick(object? state) => PerformTouchscreenDetection();

    /// <summary>
    /// Raises the TouchscreenAvailabilityChanged event.
    /// </summary>
    /// <param name="e">A TouchscreenAvailabilityChangedEventArgs containing the event data.</param>
    private static void OnTouchscreenAvailabilityChanged(TouchscreenAvailabilityChangedEventArgs e)
    {
        var handler = TouchscreenAvailabilityChanged;
        handler?.Invoke(null, e);
    }

    /// <summary>
    /// Detects if the system has touchscreen capability.
    /// Uses GetSystemMetrics(SM_DIGITIZER) to check for digitizer input support.
    /// Note: This detects system-wide touchscreen capability, not per-monitor.
    /// For per-monitor detection, you may need to check the monitor's capabilities separately.
    /// </summary>
    /// <returns>True if a touchscreen is detected; otherwise false.</returns>
    public static bool IsTouchscreenAvailable()
    {
        try
        {
            // SM_DIGITIZER = 94
            // NID_READY = 0x80 (bit 7) indicates the digitizer is ready
            int digitizerInfo = PI.GetSystemMetrics(PI.SM_.DIGITIZER);
            return (digitizerInfo & 0x80) != 0; // Check NID_READY bit
        }
        catch
        {
            // API may not be available on older Windows versions
            return false;
        }
    }

    /// <summary>
    /// Gets the maximum number of simultaneous touch contacts supported by the system.
    /// Returns 0 if no touchscreen is available or the API is not supported.
    /// </summary>
    /// <returns>The maximum number of simultaneous touches, or 0 if not available.</returns>
    public static int GetMaximumTouchContacts()
    {
        try
        {
            // SM_MAXIMUMTOUCHES = 95
            return PI.GetSystemMetrics(PI.SM_.MAXIMUMTOUCHES);
        }
        catch
        {
            // API may not be available on older Windows versions
            return 0;
        }
    }

    /// <summary>
    /// Automatically enables touchscreen support if a touchscreen is detected.
    /// This is a convenience method that calls IsTouchscreenAvailable() and enables support if detected.
    /// </summary>
    /// <param name="scaleFactor">The scale factor to use if touchscreen is detected. Default is 1.25f (25% larger).</param>
    /// <param name="enableFontScaling">Whether to enable font scaling. Default is true.</param>
    /// <returns>True if touchscreen was detected and support was enabled; otherwise false.</returns>
    public static bool AutoEnableTouchscreenSupport(float scaleFactor = 1.25f, bool enableFontScaling = true)
    {
        if (IsTouchscreenAvailable())
        {
            TouchscreenSettingValues.TouchscreenModeEnabled = true;
            TouchscreenSettingValues.ControlScaleFactor = scaleFactor;
            TouchscreenSettingValues.FontScalingEnabled = enableFontScaling;

            if (enableFontScaling)
            {
                TouchscreenSettingValues.FontScaleFactor = scaleFactor;
            }

            return true;
        }

        return false;
    }

    #endregion

    #region Static Palette
    /// <summary>
    /// Gets the implementation for the requested palette mode.
    /// </summary>
    /// <param name="mode">Requested palette mode.</param>
    /// <returns>PaletteBase reference is available; otherwise null exception.</returns>
    public static PaletteBase GetPaletteForMode(PaletteMode mode)
    {
        switch (mode)
        {
            case PaletteMode.ProfessionalSystem:
                return PaletteProfessionalSystem;
            case PaletteMode.ProfessionalOffice2003:
                return PaletteProfessionalOffice2003;
            case PaletteMode.Office2007Blue:
                return PaletteOffice2007Blue;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2007DarkGray:
            // return PaletteOffice2007DarkGray;
            case PaletteMode.Office2007BlueDarkMode:
                return PaletteOffice2007BlueDarkMode;
            case PaletteMode.Office2007BlueLightMode:
                return PaletteOffice2007BlueLightMode;
            case PaletteMode.Office2007Silver:
                return PaletteOffice2007Silver;
            case PaletteMode.Office2007SilverDarkMode:
                return PaletteOffice2007SilverDarkMode;
            case PaletteMode.Office2007SilverLightMode:
                return PaletteOffice2007SilverLightMode;
            case PaletteMode.Office2007White:
                return PaletteOffice2007White;
            case PaletteMode.Office2007Black:
                return PaletteOffice2007Black;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2010DarkGray:
            // return PaletteOffice2010DarkGray;
            case PaletteMode.Office2007BlackDarkMode:
                return PaletteOffice2007BlackDarkMode;
            case PaletteMode.Office2010Blue:
                return PaletteOffice2010Blue;
            case PaletteMode.Office2010BlueDarkMode:
                return PaletteOffice2010BlueDarkMode;
            case PaletteMode.Office2010BlueLightMode:
                return PaletteOffice2010BlueLightMode;
            case PaletteMode.Office2010Silver:
                return PaletteOffice2010Silver;
            case PaletteMode.Office2010SilverDarkMode:
                return PaletteOffice2010SilverDarkMode;
            case PaletteMode.Office2010SilverLightMode:
                return PaletteOffice2010SilverLightMode;
            case PaletteMode.Office2010White:
                return PaletteOffice2010White;
            case PaletteMode.Office2010Black:
                return PaletteOffice2010Black;
            case PaletteMode.Office2010BlackDarkMode:
                return PaletteOffice2010BlackDarkMode;
            case PaletteMode.Office2013DarkGray:
                return PaletteOffice2013DarkGray;
            case PaletteMode.Office2013LightGray:
                return PaletteOffice2013LightGray;
            case PaletteMode.Office2013White:
                return PaletteOffice2013White;
            case PaletteMode.SparkleBlue:
                return PaletteSparkleBlue;
            case PaletteMode.SparkleBlueDarkMode:
                return PaletteSparkleBlueDarkMode;
            case PaletteMode.SparkleBlueLightMode:
                return PaletteSparkleBlueLightMode;
            case PaletteMode.SparkleOrange:
                return PaletteSparkleOrange;
            case PaletteMode.SparkleOrangeDarkMode:
                return PaletteSparkleOrangeDarkMode;
            case PaletteMode.SparkleOrangeLightMode:
                return PaletteSparkleOrangeLightMode;
            case PaletteMode.SparklePurple:
                return PaletteSparklePurple;
            case PaletteMode.SparklePurpleDarkMode:
                return PaletteSparklePurpleDarkMode;
            case PaletteMode.SparklePurpleLightMode:
                return PaletteSparklePurpleLightMode;
            case PaletteMode.Microsoft365Black:
                return PaletteMicrosoft365Black;
            case PaletteMode.Microsoft365BlackDarkMode:
                return PaletteMicrosoft365BlackDarkMode;
            case PaletteMode.Microsoft365BlackDarkModeAlternate:
                return PaletteMicrosoft365BlackDarkModeAlternate;
            case PaletteMode.Microsoft365BlueDarkMode:
                return PaletteMicrosoft365BlueDarkMode;
            case PaletteMode.Microsoft365BlueLightMode:
                return PaletteMicrosoft365BlueLightMode;
            case PaletteMode.Microsoft365Blue:
                return PaletteMicrosoft365Blue;
            // TODO: Re-enable this once completed
            // case PaletteMode.Microsoft365DarkGray:
            // return PaletteMicrosoft365DarkGray;
            case PaletteMode.Microsoft365Silver:
                return PaletteMicrosoft365Silver;
            case PaletteMode.Microsoft365SilverDarkMode:
                return PaletteMicrosoft365SilverDarkMode;
            case PaletteMode.Microsoft365SilverLightMode:
                return PaletteMicrosoft365SilverLightMode;
            case PaletteMode.Microsoft365White:
                return PaletteMicrosoft365White;
            case PaletteMode.VisualStudio2010Render2007:
                return PaletteVisualStudio2010Office2007Variation;
            case PaletteMode.VisualStudio2010Render2010:
                return PaletteVisualStudio2010Office2010Variation;
            case PaletteMode.VisualStudio2010Render2013:
                return PaletteVisualStudio2010Office2013Variation;
            case PaletteMode.VisualStudio2010Render365:
                return PaletteVisualStudio2010Microsoft365Variation;
            case PaletteMode.VisualStudio2022Dark:
                return PaletteVisualStudio2022Dark;

            case PaletteMode.MaterialLight:
                return PaletteMaterialLight;
            case PaletteMode.MaterialDark:
                return PaletteMaterialDark;
            case PaletteMode.MaterialLightRipple:
                return PaletteMaterialLightRipple;
            case PaletteMode.MaterialDarkRipple:
                return PaletteMaterialDarkRipple;
            case PaletteMode.MaterialLimeGreen:
                return PaletteMaterialLimeGreen;
            case PaletteMode.MaterialLimeGreenDark:
                return PaletteMaterialLimeGreenDark;
            case PaletteMode.MaterialLimeGreenRipple:
                return PaletteMaterialLimeGreenRipple;
            case PaletteMode.MaterialLimeGreenDarkRipple:
                return PaletteMaterialLimeGreenDarkRipple;
            case PaletteMode.RetroGreen:
                return PaletteRetroGreen;
            case PaletteMode.RetroBlue:
                return PaletteRetroBlue;

            case PaletteMode.MacOSXAqua:
                return PaletteMacOSXAqua;

            case PaletteMode.MacOSLight:
                return PaletteMacOSLight;
            case PaletteMode.MacOSDark:
                return PaletteMacOSDark;

            case PaletteMode.HighContrast:
                return PaletteHighContrast;
            case PaletteMode.Deuteranopia:
                return PaletteDeuteranopia;
            case PaletteMode.Protanopia:
                return PaletteProtanopia;
            case PaletteMode.Office2007HighContrast:
                return PaletteOffice2007HighContrast;
            case PaletteMode.Office2007Deuteranopia:
                return PaletteOffice2007Deuteranopia;
            case PaletteMode.Office2007Protanopia:
                return PaletteOffice2007Protanopia;
            case PaletteMode.Office2010HighContrast:
                return PaletteOffice2010HighContrast;
            case PaletteMode.Office2010Deuteranopia:
                return PaletteOffice2010Deuteranopia;
            case PaletteMode.Office2010Protanopia:
                return PaletteOffice2010Protanopia;
            case PaletteMode.Office2013HighContrast:
                return PaletteOffice2013HighContrast;
            case PaletteMode.Office2013Deuteranopia:
                return PaletteOffice2013Deuteranopia;
            case PaletteMode.Office2013Protanopia:
                return PaletteOffice2013Protanopia;
            case PaletteMode.SparkleHighContrast:
                return PaletteSparkleHighContrast;
            case PaletteMode.SparkleDeuteranopia:
                return PaletteSparkleDeuteranopia;
            case PaletteMode.SparkleProtanopia:
                return PaletteSparkleProtanopia;
            case PaletteMode.MaterialHighContrast:
                return PaletteMaterialHighContrast;
            case PaletteMode.MaterialDeuteranopia:
                return PaletteMaterialDeuteranopia;
            case PaletteMode.MaterialProtanopia:
                return PaletteMaterialProtanopia;
            case PaletteMode.MaterialHighContrastRipple:
                return PaletteMaterialHighContrastRipple;
            case PaletteMode.MaterialDeuteranopiaRipple:
                return PaletteMaterialDeuteranopiaRipple;
            case PaletteMode.MaterialProtanopiaRipple:
                return PaletteMaterialProtanopiaRipple;
            case PaletteMode.Office2007LimeGreen:
                return PaletteOffice2007LimeGreen;
            case PaletteMode.Office2007LimeGreenDark:
                return PaletteOffice2007LimeGreenDark;
            case PaletteMode.Office2010LimeGreen:
                return PaletteOffice2010LimeGreen;
            case PaletteMode.Office2010LimeGreenDark:
                return PaletteOffice2010LimeGreenDark;
            case PaletteMode.Microsoft365LimeGreen:
                return PaletteMicrosoft365LimeGreen;
            case PaletteMode.Microsoft365LimeGreenDark:
                return PaletteMicrosoft365LimeGreenDark;
            case PaletteMode.Office2007MaterializeBlue:
                return PaletteOffice2007MaterializeBlue;
            case PaletteMode.Office2007MaterializeBlueDark:
                return PaletteOffice2007MaterializeBlueDark;
            case PaletteMode.Office2007MaterializeLightBlue:
                return PaletteOffice2007MaterializeLightBlue;
            case PaletteMode.Office2007MaterializeLightBlueDark:
                return PaletteOffice2007MaterializeLightBlueDark;
            case PaletteMode.Office2007SilverDarkModeAlternate:
                return PaletteOffice2007SilverDarkModeAlternate;
            case PaletteMode.Office2010MaterializeBlue:
                return PaletteOffice2010MaterializeBlue;
            case PaletteMode.Office2010MaterializeBlueDark:
                return PaletteOffice2010MaterializeBlueDark;
            case PaletteMode.Office2010MaterializeLightBlue:
                return PaletteOffice2010MaterializeLightBlue;
            case PaletteMode.Office2010MaterializeLightBlueDark:
                return PaletteOffice2010MaterializeLightBlueDark;
            case PaletteMode.Office2010SilverDarkModeAlternate:
                return PaletteOffice2010SilverDarkModeAlternate;
            case PaletteMode.Office2013MaterializeBlue:
                return PaletteOffice2013MaterializeBlue;
            case PaletteMode.Office2013MaterializeBlueDark:
                return PaletteOffice2013MaterializeBlueDark;
            case PaletteMode.Office2013MaterializeLightBlue:
                return PaletteOffice2013MaterializeLightBlue;
            case PaletteMode.Office2013MaterializeLightBlueDark:
                return PaletteOffice2013MaterializeLightBlueDark;
            case PaletteMode.Office2013SilverDarkModeAlternate:
                return PaletteOffice2013SilverDarkModeAlternate;
            case PaletteMode.Microsoft365MaterializeBlue:
                return PaletteMicrosoft365MaterializeBlue;
            case PaletteMode.Microsoft365MaterializeBlueDark:
                return PaletteMicrosoft365MaterializeBlueDark;
            case PaletteMode.Microsoft365MaterializeLightBlue:
                return PaletteMicrosoft365MaterializeLightBlue;
            case PaletteMode.Microsoft365MaterializeLightBlueDark:
                return PaletteMicrosoft365MaterializeLightBlueDark;
            case PaletteMode.Microsoft365SilverDarkModeAlternate:
                return PaletteMicrosoft365SilverDarkModeAlternate;
            case PaletteMode.MaterialMaterializeBlue:
                return PaletteMaterialMaterializeBlue;
            case PaletteMode.MaterialMaterializeBlueDark:
                return PaletteMaterialMaterializeBlueDark;
            case PaletteMode.MaterialMaterializeBlueRipple:
                return PaletteMaterialMaterializeBlueRipple;
            case PaletteMode.MaterialMaterializeBlueDarkRipple:
                return PaletteMaterialMaterializeBlueDarkRipple;
            case PaletteMode.MaterialMaterializeLightBlue:
                return PaletteMaterialMaterializeLightBlue;
            case PaletteMode.MaterialMaterializeLightBlueDark:
                return PaletteMaterialMaterializeLightBlueDark;
            case PaletteMode.MaterialMaterializeLightBlueRipple:
                return PaletteMaterialMaterializeLightBlueRipple;
            case PaletteMode.MaterialMaterializeLightBlueDarkRipple:
                return PaletteMaterialMaterializeLightBlueDarkRipple;
            case PaletteMode.MaterialSilverDarkModeAlternate:
                return PaletteMaterialSilverDarkModeAlternate;
            case PaletteMode.MaterialSilverDarkModeAlternateRipple:
                return PaletteMaterialSilverDarkModeAlternateRipple;

            case PaletteMode.Custom:
            case PaletteMode.Global:
                return CurrentGlobalPalette;
            default:
                Debug.Assert(false);
                return ThrowHelper.ThrowArgumentOutOfRangeException<PaletteBase>(nameof(mode), @"mode must be PaletteMode value.");
        }
    }

    /// <summary>
    /// Gets the implementation for the requested palette mode.
    /// </summary>
    /// <param name="palette">Requested palette to mode.</param>
    /// <returns>PaletteMode is available; otherwise Custom.</returns>
    public static PaletteMode GetModeForPalette(PaletteBase? palette)
    {
        if (palette is KryptonCustomPaletteBase)
        {
            return PaletteMode.Custom;
        }

        object? mode = null;
        if (palette != null)
        {
            var modeConverter = new Converters.PaletteClassTypeConverter();

            mode = modeConverter.ConvertFrom(palette.GetType());
        }

        if (mode == null)
        {
            return PaletteMode.Global;
        }

        return (PaletteMode)mode;
    }

    /// <summary>
    /// Gets the single instance of the professional system palette.
    /// </summary>
    public static PaletteProfessionalSystem PaletteProfessionalSystem => _paletteProfessionalSystem ??= new PaletteProfessionalSystem();

    /// <summary>
    /// Gets the single instance of the professional office palette.
    /// </summary>
    public static PaletteProfessionalOffice2003 PaletteProfessionalOffice2003 => _paletteProfessionalOffice2003 ??= new PaletteProfessionalOffice2003();

    /// <summary>
    /// Gets the single instance of the dark gray variant Office 2007 palette.
    /// </summary>
    public static PaletteOffice2007DarkGray PaletteOffice2007DarkGray => _paletteOffice2007DarkGray ??= new PaletteOffice2007DarkGray();

    /// <summary>
    /// Gets the single instance of the Blue variant Office 2007 palette.
    /// </summary>
    public static PaletteOffice2007Blue PaletteOffice2007Blue => _paletteOffice2007Blue ??= new PaletteOffice2007Blue();

    /// <summary>
    /// Gets the single instance of the ### palette.
    /// </summary>
    public static PaletteOffice2007BlueDarkMode PaletteOffice2007BlueDarkMode => _paletteOffice2007BlueDarkMode ??= new PaletteOffice2007BlueDarkMode();

    /// <summary>
    /// Gets the single instance of the ### palette.
    /// </summary>
    public static PaletteOffice2007BlueLightMode PaletteOffice2007BlueLightMode => _paletteOffice2007BlueLightMode ??= new PaletteOffice2007BlueLightMode();

    /// <summary>
    /// Gets the single instance of the Silver variant Office 2007 palette.
    /// </summary>
    public static PaletteOffice2007Silver PaletteOffice2007Silver => _paletteOffice2007Silver ??= new PaletteOffice2007Silver();

    /// <summary>
    /// Gets the single instance of the ### palette.
    /// </summary>
    public static PaletteOffice2007SilverDarkMode PaletteOffice2007SilverDarkMode => _paletteOffice2007SilverDarkMode ??= new PaletteOffice2007SilverDarkMode();

    /// <summary>
    /// Gets the single instance of the ### palette.
    /// </summary>
    public static PaletteOffice2007SilverLightMode PaletteOffice2007SilverLightMode => _paletteOffice2007SilverLightMode ??= new PaletteOffice2007SilverLightMode();

    /// <summary>
    /// Gets the single instance of the ### palette.
    /// </summary>
    public static PaletteOffice2007White PaletteOffice2007White => _paletteOffice2007White ??= new PaletteOffice2007White();

    /// <summary>
    /// Gets the single instance of the Black variant Office 2007 palette.
    /// </summary>
    public static PaletteOffice2007Black PaletteOffice2007Black => _paletteOffice2007Black ??= new PaletteOffice2007Black();

    /// <summary>
    /// Gets the single instance of the ### palette.
    /// </summary>
    public static PaletteOffice2007BlackDarkMode PaletteOffice2007BlackDarkMode => _paletteOffice2007BlackDarkMode ??= new PaletteOffice2007BlackDarkMode();

    /// <summary>
    /// Gets the single instance of the dark gray variant Office 2010 palette.
    /// </summary>
    public static PaletteOffice2010DarkGray PaletteOffice2010DarkGray => _paletteOffice2010DarkGray ??= new PaletteOffice2010DarkGray();

    /// <summary>
    /// Gets the single instance of the Blue variant Office 2010 palette.
    /// </summary>
    public static PaletteOffice2010Blue PaletteOffice2010Blue => _paletteOffice2010Blue ??= new PaletteOffice2010Blue();

    /// <summary>
    /// Gets the single instance of the ### palette.
    /// </summary>
    public static PaletteOffice2010BlueDarkMode PaletteOffice2010BlueDarkMode => _paletteOffice2010BlueDarkMode ??= new PaletteOffice2010BlueDarkMode();

    /// <summary>
    /// Gets the single instance of the ### palette.
    /// </summary>
    public static PaletteOffice2010BlueLightMode PaletteOffice2010BlueLightMode => _paletteOffice2010BlueLightMode ??= new PaletteOffice2010BlueLightMode();

    /// <summary>
    /// Gets the single instance of the Silver variant Office 2010 palette.
    /// </summary>
    public static PaletteOffice2010Silver PaletteOffice2010Silver => _paletteOffice2010Silver ??= new PaletteOffice2010Silver();

    /// <summary>
    /// Gets the single instance of the ### palette.
    /// </summary>
    public static PaletteOffice2010SilverDarkMode PaletteOffice2010SilverDarkMode => _paletteOffice2010SilverDarkMode ??= new PaletteOffice2010SilverDarkMode();

    /// <summary>
    /// Gets the single instance of the ### palette.
    /// </summary>
    public static PaletteOffice2010SilverLightMode PaletteOffice2010SilverLightMode => _paletteOffice2010SilverLightMode ??= new PaletteOffice2010SilverLightMode();

    /// <summary>
    /// Gets the single instance of the Black variant Office 2010 palette.
    /// </summary>
    public static PaletteOffice2010Black PaletteOffice2010Black => _paletteOffice2010Black ??= new PaletteOffice2010Black();

    /// <summary>
    /// Gets the single instance of the ### palette.
    /// </summary>
    public static PaletteOffice2010BlackDarkMode PaletteOffice2010BlackDarkMode => _paletteOffice2010BlackDarkMode ??= new PaletteOffice2010BlackDarkMode();

    /// <summary>
    /// Gets the single instance of the dark gray variant Office 2013 palette.
    /// </summary>
    public static PaletteOffice2013DarkGray PaletteOffice2013DarkGray => _paletteOffice2013DarkGray ??= new PaletteOffice2013DarkGray();

    /// <summary>
    /// Gets the single instance of the Light gray variant Office 2013 palette.
    /// </summary>
    public static PaletteOffice2013LightGray PaletteOffice2013LightGray => _paletteOffice2013LightGray ??= new PaletteOffice2013LightGray();

    /// <summary>
    /// Gets the single instance of the ### palette.
    /// </summary>
    public static PaletteOffice2010White PaletteOffice2010White => _paletteOffice2010White ??= new PaletteOffice2010White();

    /// <summary>
    /// Gets the single instance of the Office 2013 palette.
    /// </summary>
    public static PaletteOffice2013White PaletteOffice2013White => _paletteOffice2013White ??= new PaletteOffice2013White();

    /// <summary>
    /// Gets the palette Microsoft365 black.
    /// </summary>
    public static PaletteMicrosoft365Black PaletteMicrosoft365Black => _paletteMicrosoft365Black ??= new PaletteMicrosoft365Black();

    /// <summary>
    /// Gets the palette Microsoft 365 black dark mode.
    /// </summary>
    public static PaletteMicrosoft365BlackDarkMode PaletteMicrosoft365BlackDarkMode => _paletteMicrosoft365BlackDarkMode ??= new PaletteMicrosoft365BlackDarkMode();

    /// <summary>
    /// Gets the palette Microsoft 365 black dark mode alternate.
    /// </summary>
    public static PaletteMicrosoft365BlackDarkModeAlternate PaletteMicrosoft365BlackDarkModeAlternate => _paletteMicrosoft365BlackDarkModeAlternate ??= new PaletteMicrosoft365BlackDarkModeAlternate();

    /// <summary>
    /// Gets the palette Microsoft365 blue.
    /// </summary>
    public static PaletteMicrosoft365Blue PaletteMicrosoft365Blue => _paletteMicrosoft365Blue ??= new PaletteMicrosoft365Blue();

    /// <summary>
    /// Gets the single instance of the ### palette.
    /// </summary>
    public static PaletteMicrosoft365BlueDarkMode PaletteMicrosoft365BlueDarkMode => _paletteMicrosoft365BlueDarkMode ??= new PaletteMicrosoft365BlueDarkMode();

    /// <summary>
    /// Gets the single instance of the ### palette.
    /// </summary>
    public static PaletteMicrosoft365BlueLightMode PaletteMicrosoft365BlueLightMode => _paletteMicrosoft365BlueLightMode ??= new PaletteMicrosoft365BlueLightMode();

    /// <summary>
    /// Gets the single instance of the ### palette.
    /// </summary>
    public static PaletteMicrosoft365DarkGray PaletteMicrosoft365DarkGray => _paletteMicrosoft365DarkGray ??= new PaletteMicrosoft365DarkGray();

    /// <summary>
    /// Gets the palette Microsoft365 silver.
    /// </summary>
    public static PaletteMicrosoft365Silver PaletteMicrosoft365Silver => _paletteMicrosoft365Silver ??= new PaletteMicrosoft365Silver();

    /// <summary>
    /// Gets the single instance of the ### palette.
    /// </summary>
    public static PaletteMicrosoft365SilverDarkMode PaletteMicrosoft365SilverDarkMode => _paletteMicrosoft365SilverDarkMode ??= new PaletteMicrosoft365SilverDarkMode();

    /// <summary>
    /// Gets the single instance of the ### palette.
    /// </summary>
    public static PaletteMicrosoft365SilverLightMode PaletteMicrosoft365SilverLightMode => _paletteMicrosoft365SilverLightMode ??= new PaletteMicrosoft365SilverLightMode();

    /// <summary>
    /// Gets the single instance of the ### palette.
    /// </summary>
    public static PaletteMicrosoft365White PaletteMicrosoft365White => _paletteMicrosoft365White ??= new PaletteMicrosoft365White();

    /// <summary>
    /// Gets the single instance of the Blue variant sparkle palette.
    /// </summary>
    public static PaletteSparkleBlue PaletteSparkleBlue => _paletteSparkleBlue ??= new PaletteSparkleBlue();

    /// <summary>
    /// Gets the single instance of the ### palette.
    /// </summary>
    public static PaletteSparkleBlueDarkMode PaletteSparkleBlueDarkMode => _paletteSparkleBlueDarkMode ??= new PaletteSparkleBlueDarkMode();

    /// <summary>
    /// Gets the single instance of the ### palette.
    /// </summary>
    public static PaletteSparkleBlueLightMode PaletteSparkleBlueLightMode => _paletteSparkleBlueLightMode ??= new PaletteSparkleBlueLightMode();

    /// <summary>
    /// Gets the single instance of the Orange variant sparkle palette.
    /// </summary>
    public static PaletteSparkleOrange PaletteSparkleOrange => _paletteSparkleOrange ??= new PaletteSparkleOrange();

    /// <summary>
    /// Gets the single instance of the ### palette.
    /// </summary>
    public static PaletteSparkleOrangeDarkMode PaletteSparkleOrangeDarkMode => _paletteSparkleOrangeDarkMode ??= new PaletteSparkleOrangeDarkMode();

    /// <summary>
    /// Gets the single instance of the ### palette.
    /// </summary>
    public static PaletteSparkleOrangeLightMode PaletteSparkleOrangeLightMode => _paletteSparkleOrangeLightMode ??= new PaletteSparkleOrangeLightMode();

    /// <summary>
    /// Gets the single instance of the Purple variant sparkle palette.
    /// </summary>
    public static PaletteSparklePurple PaletteSparklePurple => _paletteSparklePurple ??= new PaletteSparklePurple();

    /// <summary>
    /// Gets palette Sparkle Purpke dark moode.
    /// </summary>
    public static PaletteSparklePurpleDarkMode PaletteSparklePurpleDarkMode => _paletteSparklePurpleDarkMode ??= new PaletteSparklePurpleDarkMode();

    /// <summary>
    /// Gets palette Sparkle Purpke light moode.
    /// </summary>
    public static PaletteSparklePurpleLightMode PaletteSparklePurpleLightMode => _paletteSparklePurpleLightMode ??= new PaletteSparklePurpleLightMode();

    /// <summary>
    /// Gets palette Visual Studio 2010 Office 2007 variant.
    /// </summary>
    public static PaletteVisualStudio2010Office2007Variation PaletteVisualStudio2010Office2007Variation => _paletteVisualStudio2010Office2007Variation ??= new PaletteVisualStudio2010Office2007Variation();

    /// <summary>
    /// Gets palette Visual Studio 2010 Office 2010 variant.
    /// </summary>
    public static PaletteVisualStudio2010Office2010Variation PaletteVisualStudio2010Office2010Variation => _paletteVisualStudio2010Office2010Variation ??= new PaletteVisualStudio2010Office2010Variation();

    /// <summary>
    /// Gets palette Visual Studio 2010 Office 2013 variant.
    /// </summary>
    public static PaletteVisualStudio2010Office2013Variation PaletteVisualStudio2010Office2013Variation => _paletteVisualStudio2010Office2013Variation ??= new PaletteVisualStudio2010Office2013Variation();

    /// <summary>
    /// Gets palette Visual Studio 2010 Office 365 variant.
    /// </summary>
    public static PaletteVisualStudio2010Microsoft365Variation PaletteVisualStudio2010Microsoft365Variation => _paletteVisualStudio2010Microsoft365Variation ??= new PaletteVisualStudio2010Microsoft365Variation();

    /// <summary>
    /// Gets the Visual Studio 2022 dark palette.
    /// </summary>
    public static PaletteVisualStudio2022Dark PaletteVisualStudio2022Dark => _paletteVisualStudio2022Dark ??= new PaletteVisualStudio2022Dark();

    public static PaletteMaterialLight PaletteMaterialLight => _paletteMaterialLight ??= new PaletteMaterialLight();
    public static PaletteMaterialDark PaletteMaterialDark => _paletteMaterialDark ??= new PaletteMaterialDark();
    public static PaletteMaterialLightRipple PaletteMaterialLightRipple => _paletteMaterialLightRipple ??= new PaletteMaterialLightRipple();
    public static PaletteMaterialDarkRipple PaletteMaterialDarkRipple => _paletteMaterialDarkRipple ??= new PaletteMaterialDarkRipple();
    public static PaletteMaterialLimeGreen PaletteMaterialLimeGreen => _paletteMaterialLimeGreen ??= new PaletteMaterialLimeGreen();
    public static PaletteMaterialLimeGreenDark PaletteMaterialLimeGreenDark => _paletteMaterialLimeGreenDark ??= new PaletteMaterialLimeGreenDark();
    public static PaletteMaterialLimeGreenRipple PaletteMaterialLimeGreenRipple => _paletteMaterialLimeGreenRipple ??= new PaletteMaterialLimeGreenRipple();
    public static PaletteMaterialLimeGreenDarkRipple PaletteMaterialLimeGreenDarkRipple => _paletteMaterialLimeGreenDarkRipple ??= new PaletteMaterialLimeGreenDarkRipple();

    /// <summary>
    /// Gets the DOS teal/green RetroUI palette.
    /// </summary>
    public static PaletteRetroGreen PaletteRetroGreen => _paletteRetroGreen ??= new PaletteRetroGreen();

    /// <summary>
    /// Gets the Norton Commander style blue RetroUI palette.
    /// </summary>
    public static PaletteRetroBlue PaletteRetroBlue => _paletteRetroBlue ??= new PaletteRetroBlue();

    public static PaletteMacOSXAqua PaletteMacOSXAqua => _paletteMacOSXAqua ??= new PaletteMacOSXAqua();

    /// <summary>
    /// Gets the macOS-inspired light palette.
    /// </summary>
    public static PaletteMacOSLight PaletteMacOSLight => _paletteMacOSLight ??= new PaletteMacOSLight();

    /// <summary>
    /// Gets the macOS-inspired dark palette.
    /// </summary>
    public static PaletteMacOSDark PaletteMacOSDark => _paletteMacOSDark ??= new PaletteMacOSDark();

    /// <summary>
    /// Gets the fixed high-contrast accessibility palette.
    /// </summary>
    public static PaletteHighContrast PaletteHighContrast => _paletteHighContrast ??= new PaletteHighContrast();

    /// <summary>
    /// Gets the deuteranopia-friendly accessibility palette.
    /// </summary>
    public static PaletteDeuteranopia PaletteDeuteranopia => _paletteDeuteranopia ??= new PaletteDeuteranopia();

    /// <summary>
    /// Gets the protanopia-friendly accessibility palette.
    /// </summary>
    public static PaletteProtanopia PaletteProtanopia => _paletteProtanopia ??= new PaletteProtanopia();

    /// <summary>Gets the Office 2007 high-contrast accessibility palette.</summary>
    public static PaletteOffice2007HighContrast PaletteOffice2007HighContrast => _paletteOffice2007HighContrast ??= new PaletteOffice2007HighContrast();

    /// <summary>Gets the Office 2007 deuteranopia accessibility palette.</summary>
    public static PaletteOffice2007Deuteranopia PaletteOffice2007Deuteranopia => _paletteOffice2007Deuteranopia ??= new PaletteOffice2007Deuteranopia();

    /// <summary>Gets the Office 2007 protanopia accessibility palette.</summary>
    public static PaletteOffice2007Protanopia PaletteOffice2007Protanopia => _paletteOffice2007Protanopia ??= new PaletteOffice2007Protanopia();

    /// <summary>Gets the Office 2010 high-contrast accessibility palette.</summary>
    public static PaletteOffice2010HighContrast PaletteOffice2010HighContrast => _paletteOffice2010HighContrast ??= new PaletteOffice2010HighContrast();

    /// <summary>Gets the Office 2010 deuteranopia accessibility palette.</summary>
    public static PaletteOffice2010Deuteranopia PaletteOffice2010Deuteranopia => _paletteOffice2010Deuteranopia ??= new PaletteOffice2010Deuteranopia();

    /// <summary>Gets the Office 2010 protanopia accessibility palette.</summary>
    public static PaletteOffice2010Protanopia PaletteOffice2010Protanopia => _paletteOffice2010Protanopia ??= new PaletteOffice2010Protanopia();

    /// <summary>Gets the Office 2013 high-contrast accessibility palette.</summary>
    public static PaletteOffice2013HighContrast PaletteOffice2013HighContrast => _paletteOffice2013HighContrast ??= new PaletteOffice2013HighContrast();

    /// <summary>Gets the Office 2013 deuteranopia accessibility palette.</summary>
    public static PaletteOffice2013Deuteranopia PaletteOffice2013Deuteranopia => _paletteOffice2013Deuteranopia ??= new PaletteOffice2013Deuteranopia();

    /// <summary>Gets the Office 2013 protanopia accessibility palette.</summary>
    public static PaletteOffice2013Protanopia PaletteOffice2013Protanopia => _paletteOffice2013Protanopia ??= new PaletteOffice2013Protanopia();

    /// <summary>Gets the Sparkle high-contrast accessibility palette.</summary>
    public static PaletteSparkleHighContrast PaletteSparkleHighContrast => _paletteSparkleHighContrast ??= new PaletteSparkleHighContrast();

    /// <summary>Gets the Sparkle deuteranopia accessibility palette.</summary>
    public static PaletteSparkleDeuteranopia PaletteSparkleDeuteranopia => _paletteSparkleDeuteranopia ??= new PaletteSparkleDeuteranopia();

    /// <summary>Gets the Sparkle protanopia accessibility palette.</summary>
    public static PaletteSparkleProtanopia PaletteSparkleProtanopia => _paletteSparkleProtanopia ??= new PaletteSparkleProtanopia();

    /// <summary>Gets the Material high-contrast accessibility palette.</summary>
    public static PaletteMaterialHighContrast PaletteMaterialHighContrast => _paletteMaterialHighContrast ??= new PaletteMaterialHighContrast();

    /// <summary>Gets the Material deuteranopia accessibility palette.</summary>
    public static PaletteMaterialDeuteranopia PaletteMaterialDeuteranopia => _paletteMaterialDeuteranopia ??= new PaletteMaterialDeuteranopia();

    /// <summary>Gets the Material protanopia accessibility palette.</summary>
    public static PaletteMaterialProtanopia PaletteMaterialProtanopia => _paletteMaterialProtanopia ??= new PaletteMaterialProtanopia();

    /// <summary>Gets the Material high-contrast accessibility palette with Ripple effect.</summary>
    public static PaletteMaterialHighContrastRipple PaletteMaterialHighContrastRipple => _paletteMaterialHighContrastRipple ??= new PaletteMaterialHighContrastRipple();

    /// <summary>Gets the Material deuteranopia accessibility palette with Ripple effect.</summary>
    public static PaletteMaterialDeuteranopiaRipple PaletteMaterialDeuteranopiaRipple => _paletteMaterialDeuteranopiaRipple ??= new PaletteMaterialDeuteranopiaRipple();

    /// <summary>Gets the Material protanopia accessibility palette with Ripple effect.</summary>
    public static PaletteMaterialProtanopiaRipple PaletteMaterialProtanopiaRipple => _paletteMaterialProtanopiaRipple ??= new PaletteMaterialProtanopiaRipple();

    /// <summary>
    /// Gets the single instance of the light Lime Green variant Office 2007 palette.
    /// </summary>
    public static PaletteOffice2007LimeGreen PaletteOffice2007LimeGreen => _paletteOffice2007LimeGreen ??= new PaletteOffice2007LimeGreen();

    /// <summary>
    /// Gets the single instance of the dark Lime Green variant Office 2007 palette.
    /// </summary>
    public static PaletteOffice2007LimeGreenDark PaletteOffice2007LimeGreenDark => _paletteOffice2007LimeGreenDark ??= new PaletteOffice2007LimeGreenDark();

    /// <summary>
    /// Gets the single instance of the light Lime Green variant Office 2010 palette.
    /// </summary>
    public static PaletteOffice2010LimeGreen PaletteOffice2010LimeGreen => _paletteOffice2010LimeGreen ??= new PaletteOffice2010LimeGreen();

    /// <summary>
    /// Gets the single instance of the dark Lime Green variant Office 2010 palette.
    /// </summary>
    public static PaletteOffice2010LimeGreenDark PaletteOffice2010LimeGreenDark => _paletteOffice2010LimeGreenDark ??= new PaletteOffice2010LimeGreenDark();

    /// <summary>
    /// Gets the single instance of the light Lime Green variant Microsoft 365 palette.
    /// </summary>
    public static PaletteMicrosoft365LimeGreen PaletteMicrosoft365LimeGreen => _paletteMicrosoft365LimeGreen ??= new PaletteMicrosoft365LimeGreen();

    /// <summary>
    /// Gets the single instance of the dark Lime Green variant Microsoft 365 palette.
    /// </summary>
    public static PaletteMicrosoft365LimeGreenDark PaletteMicrosoft365LimeGreenDark => _paletteMicrosoft365LimeGreenDark ??= new PaletteMicrosoft365LimeGreenDark();
    public static PaletteOffice2007MaterializeBlue PaletteOffice2007MaterializeBlue => _paletteOffice2007MaterializeBlue ??= new PaletteOffice2007MaterializeBlue();
    public static PaletteOffice2007MaterializeBlueDark PaletteOffice2007MaterializeBlueDark => _paletteOffice2007MaterializeBlueDark ??= new PaletteOffice2007MaterializeBlueDark();
    public static PaletteOffice2007MaterializeLightBlue PaletteOffice2007MaterializeLightBlue => _paletteOffice2007MaterializeLightBlue ??= new PaletteOffice2007MaterializeLightBlue();
    public static PaletteOffice2007MaterializeLightBlueDark PaletteOffice2007MaterializeLightBlueDark => _paletteOffice2007MaterializeLightBlueDark ??= new PaletteOffice2007MaterializeLightBlueDark();
    public static PaletteOffice2007SilverDarkModeAlternate PaletteOffice2007SilverDarkModeAlternate => _paletteOffice2007SilverDarkModeAlternate ??= new PaletteOffice2007SilverDarkModeAlternate();
    public static PaletteOffice2010MaterializeBlue PaletteOffice2010MaterializeBlue => _paletteOffice2010MaterializeBlue ??= new PaletteOffice2010MaterializeBlue();
    public static PaletteOffice2010MaterializeBlueDark PaletteOffice2010MaterializeBlueDark => _paletteOffice2010MaterializeBlueDark ??= new PaletteOffice2010MaterializeBlueDark();
    public static PaletteOffice2010MaterializeLightBlue PaletteOffice2010MaterializeLightBlue => _paletteOffice2010MaterializeLightBlue ??= new PaletteOffice2010MaterializeLightBlue();
    public static PaletteOffice2010MaterializeLightBlueDark PaletteOffice2010MaterializeLightBlueDark => _paletteOffice2010MaterializeLightBlueDark ??= new PaletteOffice2010MaterializeLightBlueDark();
    public static PaletteOffice2010SilverDarkModeAlternate PaletteOffice2010SilverDarkModeAlternate => _paletteOffice2010SilverDarkModeAlternate ??= new PaletteOffice2010SilverDarkModeAlternate();
    public static PaletteOffice2013MaterializeBlue PaletteOffice2013MaterializeBlue => _paletteOffice2013MaterializeBlue ??= new PaletteOffice2013MaterializeBlue();
    public static PaletteOffice2013MaterializeBlueDark PaletteOffice2013MaterializeBlueDark => _paletteOffice2013MaterializeBlueDark ??= new PaletteOffice2013MaterializeBlueDark();
    public static PaletteOffice2013MaterializeLightBlue PaletteOffice2013MaterializeLightBlue => _paletteOffice2013MaterializeLightBlue ??= new PaletteOffice2013MaterializeLightBlue();
    public static PaletteOffice2013MaterializeLightBlueDark PaletteOffice2013MaterializeLightBlueDark => _paletteOffice2013MaterializeLightBlueDark ??= new PaletteOffice2013MaterializeLightBlueDark();
    public static PaletteOffice2013SilverDarkModeAlternate PaletteOffice2013SilverDarkModeAlternate => _paletteOffice2013SilverDarkModeAlternate ??= new PaletteOffice2013SilverDarkModeAlternate();
    public static PaletteMicrosoft365MaterializeBlue PaletteMicrosoft365MaterializeBlue => _paletteMicrosoft365MaterializeBlue ??= new PaletteMicrosoft365MaterializeBlue();
    public static PaletteMicrosoft365MaterializeBlueDark PaletteMicrosoft365MaterializeBlueDark => _paletteMicrosoft365MaterializeBlueDark ??= new PaletteMicrosoft365MaterializeBlueDark();
    public static PaletteMicrosoft365MaterializeLightBlue PaletteMicrosoft365MaterializeLightBlue => _paletteMicrosoft365MaterializeLightBlue ??= new PaletteMicrosoft365MaterializeLightBlue();
    public static PaletteMicrosoft365MaterializeLightBlueDark PaletteMicrosoft365MaterializeLightBlueDark => _paletteMicrosoft365MaterializeLightBlueDark ??= new PaletteMicrosoft365MaterializeLightBlueDark();
    public static PaletteMicrosoft365SilverDarkModeAlternate PaletteMicrosoft365SilverDarkModeAlternate => _paletteMicrosoft365SilverDarkModeAlternate ??= new PaletteMicrosoft365SilverDarkModeAlternate();
    public static PaletteMaterialMaterializeBlue PaletteMaterialMaterializeBlue => _paletteMaterialMaterializeBlue ??= new PaletteMaterialMaterializeBlue();
    public static PaletteMaterialMaterializeBlueDark PaletteMaterialMaterializeBlueDark => _paletteMaterialMaterializeBlueDark ??= new PaletteMaterialMaterializeBlueDark();
    public static PaletteMaterialMaterializeBlueRipple PaletteMaterialMaterializeBlueRipple => _paletteMaterialMaterializeBlueRipple ??= new PaletteMaterialMaterializeBlueRipple();
    public static PaletteMaterialMaterializeBlueDarkRipple PaletteMaterialMaterializeBlueDarkRipple => _paletteMaterialMaterializeBlueDarkRipple ??= new PaletteMaterialMaterializeBlueDarkRipple();
    public static PaletteMaterialMaterializeLightBlue PaletteMaterialMaterializeLightBlue => _paletteMaterialMaterializeLightBlue ??= new PaletteMaterialMaterializeLightBlue();
    public static PaletteMaterialMaterializeLightBlueDark PaletteMaterialMaterializeLightBlueDark => _paletteMaterialMaterializeLightBlueDark ??= new PaletteMaterialMaterializeLightBlueDark();
    public static PaletteMaterialMaterializeLightBlueRipple PaletteMaterialMaterializeLightBlueRipple => _paletteMaterialMaterializeLightBlueRipple ??= new PaletteMaterialMaterializeLightBlueRipple();
    public static PaletteMaterialMaterializeLightBlueDarkRipple PaletteMaterialMaterializeLightBlueDarkRipple => _paletteMaterialMaterializeLightBlueDarkRipple ??= new PaletteMaterialMaterializeLightBlueDarkRipple();
    public static PaletteMaterialSilverDarkModeAlternate PaletteMaterialSilverDarkModeAlternate => _paletteMaterialSilverDarkModeAlternate ??= new PaletteMaterialSilverDarkModeAlternate();
    public static PaletteMaterialSilverDarkModeAlternateRipple PaletteMaterialSilverDarkModeAlternateRipple => _paletteMaterialSilverDarkModeAlternateRipple ??= new PaletteMaterialSilverDarkModeAlternateRipple();

    private static PaletteMaterialLight? _paletteMaterialLight;
    private static PaletteMaterialDark? _paletteMaterialDark;
    private static PaletteMaterialLightRipple? _paletteMaterialLightRipple;
    private static PaletteMaterialDarkRipple? _paletteMaterialDarkRipple;
    private static PaletteMaterialLimeGreen? _paletteMaterialLimeGreen;
    private static PaletteMaterialLimeGreenDark? _paletteMaterialLimeGreenDark;
    private static PaletteMaterialLimeGreenRipple? _paletteMaterialLimeGreenRipple;
    private static PaletteMaterialLimeGreenDarkRipple? _paletteMaterialLimeGreenDarkRipple;

    private static PaletteRetroGreen? _paletteRetroGreen;
    private static PaletteRetroBlue? _paletteRetroBlue;
    private static PaletteMacOSXAqua? _paletteMacOSXAqua;
    private static PaletteMacOSLight? _paletteMacOSLight;
    private static PaletteMacOSDark? _paletteMacOSDark;
    private static PaletteHighContrast? _paletteHighContrast;
    private static PaletteDeuteranopia? _paletteDeuteranopia;
    private static PaletteProtanopia? _paletteProtanopia;
    private static PaletteOffice2007HighContrast? _paletteOffice2007HighContrast;
    private static PaletteOffice2007Deuteranopia? _paletteOffice2007Deuteranopia;
    private static PaletteOffice2007Protanopia? _paletteOffice2007Protanopia;
    private static PaletteOffice2010HighContrast? _paletteOffice2010HighContrast;
    private static PaletteOffice2010Deuteranopia? _paletteOffice2010Deuteranopia;
    private static PaletteOffice2010Protanopia? _paletteOffice2010Protanopia;
    private static PaletteOffice2013HighContrast? _paletteOffice2013HighContrast;
    private static PaletteOffice2013Deuteranopia? _paletteOffice2013Deuteranopia;
    private static PaletteOffice2013Protanopia? _paletteOffice2013Protanopia;
    private static PaletteSparkleHighContrast? _paletteSparkleHighContrast;
    private static PaletteSparkleDeuteranopia? _paletteSparkleDeuteranopia;
    private static PaletteSparkleProtanopia? _paletteSparkleProtanopia;
    private static PaletteMaterialHighContrast? _paletteMaterialHighContrast;
    private static PaletteMaterialDeuteranopia? _paletteMaterialDeuteranopia;
    private static PaletteMaterialProtanopia? _paletteMaterialProtanopia;
    private static PaletteMaterialHighContrastRipple? _paletteMaterialHighContrastRipple;
    private static PaletteMaterialDeuteranopiaRipple? _paletteMaterialDeuteranopiaRipple;
    private static PaletteMaterialProtanopiaRipple? _paletteMaterialProtanopiaRipple;

    private static PaletteOffice2007LimeGreen? _paletteOffice2007LimeGreen;
    private static PaletteOffice2007LimeGreenDark? _paletteOffice2007LimeGreenDark;
    private static PaletteOffice2010LimeGreen? _paletteOffice2010LimeGreen;
    private static PaletteOffice2010LimeGreenDark? _paletteOffice2010LimeGreenDark;
    private static PaletteMicrosoft365LimeGreen? _paletteMicrosoft365LimeGreen;
    private static PaletteMicrosoft365LimeGreenDark? _paletteMicrosoft365LimeGreenDark;
    private static PaletteOffice2007MaterializeBlue? _paletteOffice2007MaterializeBlue;
    private static PaletteOffice2007MaterializeBlueDark? _paletteOffice2007MaterializeBlueDark;
    private static PaletteOffice2007MaterializeLightBlue? _paletteOffice2007MaterializeLightBlue;
    private static PaletteOffice2007MaterializeLightBlueDark? _paletteOffice2007MaterializeLightBlueDark;
    private static PaletteOffice2007SilverDarkModeAlternate? _paletteOffice2007SilverDarkModeAlternate;
    private static PaletteOffice2010MaterializeBlue? _paletteOffice2010MaterializeBlue;
    private static PaletteOffice2010MaterializeBlueDark? _paletteOffice2010MaterializeBlueDark;
    private static PaletteOffice2010MaterializeLightBlue? _paletteOffice2010MaterializeLightBlue;
    private static PaletteOffice2010MaterializeLightBlueDark? _paletteOffice2010MaterializeLightBlueDark;
    private static PaletteOffice2010SilverDarkModeAlternate? _paletteOffice2010SilverDarkModeAlternate;
    private static PaletteOffice2013MaterializeBlue? _paletteOffice2013MaterializeBlue;
    private static PaletteOffice2013MaterializeBlueDark? _paletteOffice2013MaterializeBlueDark;
    private static PaletteOffice2013MaterializeLightBlue? _paletteOffice2013MaterializeLightBlue;
    private static PaletteOffice2013MaterializeLightBlueDark? _paletteOffice2013MaterializeLightBlueDark;
    private static PaletteOffice2013SilverDarkModeAlternate? _paletteOffice2013SilverDarkModeAlternate;
    private static PaletteMicrosoft365MaterializeBlue? _paletteMicrosoft365MaterializeBlue;
    private static PaletteMicrosoft365MaterializeBlueDark? _paletteMicrosoft365MaterializeBlueDark;
    private static PaletteMicrosoft365MaterializeLightBlue? _paletteMicrosoft365MaterializeLightBlue;
    private static PaletteMicrosoft365MaterializeLightBlueDark? _paletteMicrosoft365MaterializeLightBlueDark;
    private static PaletteMicrosoft365SilverDarkModeAlternate? _paletteMicrosoft365SilverDarkModeAlternate;
    private static PaletteMaterialMaterializeBlue? _paletteMaterialMaterializeBlue;
    private static PaletteMaterialMaterializeBlueDark? _paletteMaterialMaterializeBlueDark;
    private static PaletteMaterialMaterializeBlueRipple? _paletteMaterialMaterializeBlueRipple;
    private static PaletteMaterialMaterializeBlueDarkRipple? _paletteMaterialMaterializeBlueDarkRipple;
    private static PaletteMaterialMaterializeLightBlue? _paletteMaterialMaterializeLightBlue;
    private static PaletteMaterialMaterializeLightBlueDark? _paletteMaterialMaterializeLightBlueDark;
    private static PaletteMaterialMaterializeLightBlueRipple? _paletteMaterialMaterializeLightBlueRipple;
    private static PaletteMaterialMaterializeLightBlueDarkRipple? _paletteMaterialMaterializeLightBlueDarkRipple;
    private static PaletteMaterialSilverDarkModeAlternate? _paletteMaterialSilverDarkModeAlternate;
    private static PaletteMaterialSilverDarkModeAlternateRipple? _paletteMaterialSilverDarkModeAlternateRipple;

    //public static PaletteBase CustomPaletteBase => _customPalette ??= new PaletteBase ();

    /// <summary>
    /// Gets the implementation for the requested renderer mode.
    /// </summary>
    /// <param name="mode">Requested renderer mode.</param>
    /// <returns>IRenderer reference is available; otherwise false.</returns>
    public static IRenderer GetRendererForMode(RendererMode mode)
    {
        switch (mode)
        {
            case RendererMode.Sparkle:
                return RenderSparkle;
            case RendererMode.Office2007:
                return RenderOffice2007;
            case RendererMode.Office2010:
                return RenderOffice2010;
            case RendererMode.Office2013:
                return RenderOffice2013;
            case RendererMode.Microsoft365:
                return RenderMicrosoft365;
            case RendererMode.Professional:
                return RenderProfessional;
            case RendererMode.Standard:
                return RenderStandard;
            case RendererMode.VisualStudio:
                return RenderVisualStudio;
            case RendererMode.VisualStudio2010With2007Renderer:
                return RenderVisualStudio2010With2007;
            case RendererMode.VisualStudio2010With2010Renderer:
                return RenderVisualStudio2010With2010;
            case RendererMode.VisualStudio2010With2013Renderer:
                return RenderVisualStudio2010With2013;
            case RendererMode.VisualStudio2010WithMicrosoft365Renderer:
                return RenderVisualStudio2010WithMicrosoft365;
            case RendererMode.Material:
                return RenderMaterial;
            case RendererMode.Retro:
                return RenderRetro;
            case RendererMode.MacOS:
                return RenderMacOS;
            case RendererMode.OSXAqua:
                return RenderMacOSXAqua;
            case RendererMode.Inherit:
            case RendererMode.Custom:
            default:
                // Should never be passed
                Debug.Assert(false);
                return ThrowHelper.ThrowArgumentOutOfRangeException<IRenderer>(nameof(mode), @"mode must be RendererMode value.");
        }
    }

    /// <summary>
    /// Gets the single instance of the Sparkle renderer.
    /// </summary>
    public static RenderSparkle RenderSparkle => _renderSparkle ??= new RenderSparkle();

    /// <summary>
    /// Gets the single instance of the Office 2007 renderer.
    /// </summary>
    public static RenderOffice2007 RenderOffice2007 => _renderOffice2007 ??= new RenderOffice2007();

    /// <summary>
    /// Gets the single instance of the Office 2010 renderer.
    /// </summary>
    public static RenderOffice2010 RenderOffice2010 => _renderOffice2010 ??= new RenderOffice2010();

    /// <summary>
    /// Gets the single instance of the Office 2013 renderer.
    /// </summary>
    public static RenderOffice2013 RenderOffice2013 => _renderOffice2013 ??= new RenderOffice2013();

    /// <summary>
    /// Gets the single instance of the 365 2013 renderer.
    /// </summary>
    public static RenderMicrosoft365 RenderMicrosoft365 => _renderMicrosoft365 ??= new RenderMicrosoft365();

    /// <summary>
    /// Gets the single instance of the Material renderer.
    /// </summary>
    public static RenderMaterial RenderMaterial => _renderMaterial ??= new RenderMaterial();

    /// <summary>
    /// Gets the single instance of the Retro renderer.
    /// </summary>
    public static RenderRetro RenderRetro => _renderRetro ??= new RenderRetro();

    /// <summary>
    /// Gets the single instance of the macOS-inspired renderer.
    /// </summary>
    public static RenderMacOS RenderMacOS => _renderMacOS ??= new RenderMacOS();

    /// <summary>
    /// Gets the single instance of the Mac OS X Aqua-inspired renderer.
    /// </summary>
    public static RenderMacOSXAqua RenderMacOSXAqua => _renderMacOSXAqua ??= new RenderMacOSXAqua();

    /// <summary>
    /// Gets the single instance of the professional renderer.
    /// </summary>
    public static RenderProfessional RenderProfessional => _renderProfessional ??= new RenderProfessional();

    /// <summary>
    /// Gets the single instance of the Visual Studio renderer.
    /// </summary>
    public static RenderVisualStudio RenderVisualStudio => _renderVisualStudio ??= new RenderVisualStudio();

    /// <summary>
    /// Gets the single instance of the Visual Studio 2010 Office 2007 renderer.
    /// </summary>
    public static RenderVisualStudio2010With2007 RenderVisualStudio2010With2007 => _renderVisualStudio2010With2007 ??= new RenderVisualStudio2010With2007();

    /// <summary>
    /// Gets the single instance of the Visual Studio 2010 Office 2010 renderer.
    /// </summary>
    public static RenderVisualStudio2010With2010 RenderVisualStudio2010With2010 => _renderVisualStudio2010With2010 ??= new RenderVisualStudio2010With2010();

    /// <summary>
    /// Gets the single instance of the Visual Studio 2010 Office 2013 renderer.
    /// </summary>
    public static RenderVisualStudio2010With2013 RenderVisualStudio2010With2013 => _renderVisualStudio2010With2013 ??= new RenderVisualStudio2010With2013();

    /// <summary>
    /// Gets the single instance of the Visual Studio 2010 Office 365 renderer.
    /// </summary>
    public static RenderVisualStudio2010WithMicrosoft365 RenderVisualStudio2010WithMicrosoft365 => _renderVisualStudio2010WithMicrosoft365 ??= new RenderVisualStudio2010WithMicrosoft365();

    /// <summary>
    /// Gets the single instance of the standard renderer.
    /// </summary>
    public static RenderStandard RenderStandard => _renderStandard ??= new RenderStandard();

    #endregion

    #region Static Internal
    /// <summary>
    /// What is the CurrentGlobalPaletteMode in use
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static PaletteMode CurrentGlobalPaletteMode { get; private set; } = ThemeManager.DefaultGlobalPalette;

    /// <summary>
    /// Access the Current Palette
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static PaletteBase CurrentGlobalPalette { get; private set; } = GetPaletteForMode(CurrentGlobalPaletteMode);

    #endregion

    #region Static Implementation
    private static void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
        // Because we are static this event is fired before any instance controls are updated, so we need to
        // tell the palette instances to update now so that when the instance controls are updated the new fonts
        // and other resources are recreated as needed.
        // TODO: Why are the greys not in this list ?

        _paletteProfessionalOffice2003?.UserPreferenceChanged();
        _paletteProfessionalSystem?.UserPreferenceChanged();
        _paletteOffice2007Blue?.UserPreferenceChanged();
        _paletteOffice2007Silver?.UserPreferenceChanged();
        _paletteOffice2007White?.UserPreferenceChanged();
        _paletteOffice2007Black?.UserPreferenceChanged();

        _paletteOffice2010Blue?.UserPreferenceChanged();
        _paletteOffice2010Silver?.UserPreferenceChanged();
        _paletteOffice2010Black?.UserPreferenceChanged();
        _paletteOffice2010White?.UserPreferenceChanged();
        _paletteOffice2013White?.UserPreferenceChanged();

        _paletteSparkleBlue?.UserPreferenceChanged();
        _paletteSparkleOrange?.UserPreferenceChanged();
        _paletteSparklePurple?.UserPreferenceChanged();

        _paletteMicrosoft365Black?.UserPreferenceChanged();
        _paletteMicrosoft365Blue?.UserPreferenceChanged();
        _paletteMicrosoft365Silver?.UserPreferenceChanged();
        _paletteMicrosoft365White?.UserPreferenceChanged();

        _paletteVisualStudio2010Office2007Variation?.UserPreferenceChanged();
        _paletteVisualStudio2010Office2010Variation?.UserPreferenceChanged();
        _paletteVisualStudio2010Office2013Variation?.UserPreferenceChanged();
        _paletteVisualStudio2010Microsoft365Variation?.UserPreferenceChanged();

        UpdateToolStripManager();
    }

    private static void OnPalettePaint(object? sender, PaletteLayoutEventArgs e)
    {
        // If the color table has changed then need to update tool strip immediately
        if (e.NeedColorTable)
        {
            UpdateToolStripManager();
        }

        if (e.NeedLayout)
        {
            ToolStripFontSync.RefreshAllOpenForms();
        }
    }

    private static void SetPalette(PaletteBase globalPalette)
    {
        if (globalPalette != CurrentGlobalPalette)
        {
            // Unhook from current palette events
            if (CurrentGlobalPalette != null)
            {
                CurrentGlobalPalette.PalettePaintInternal -= OnPalettePaint;
            }

            // Remember the new palette
            CurrentGlobalPalette = globalPalette;

            // Hook to new palette events
            if (CurrentGlobalPalette != null)
            {
                CurrentGlobalPalette.PalettePaintInternal += OnPalettePaint;
            }
        }
    }

    private static void OnGlobalUseThemeFormChromeBorderWidthChanged(EventArgs e) => GlobalUseThemeFormChromeBorderWidthChanged?.Invoke(null, e);

    private static void OnGlobalDropDownArrowRenderModeChanged(EventArgs e) => GlobalDropDownArrowRenderModeChanged?.Invoke(null, e);

    private static void OnGlobalDropDownArrowGlyphStyleChanged(EventArgs e) => GlobalDropDownArrowGlyphStyleChanged?.Invoke(null, e);

    private static void OnGlobalPaletteChanged(EventArgs e)
    {
        UpdateToolStripManager();

        UpdatePaletteImages(CurrentGlobalPaletteMode);

        ToolStripFontSync.RefreshAllOpenForms();

        GlobalPaletteChanged?.Invoke(null, e);
    }

    private static void UpdatePaletteImages(PaletteMode paletteMode)
    {
        switch (paletteMode)
        {
            case PaletteMode.Global:
            case PaletteMode.Custom:
                Images.ToolbarImages.SetToolBarImages(ToolkitStaticVariables.GenericToolBarImages);
                break;
            case PaletteMode.ProfessionalSystem:
                Images.ToolbarImages.SetToolBarImages(ToolkitStaticVariables.SystemToolBarImages);
                break;
            case PaletteMode.ProfessionalOffice2003:
                Images.ToolbarImages.SetToolBarImages(ToolkitStaticVariables.Office2003ToolBarImages);
                break;
            case PaletteMode.Office2007Blue:
            case PaletteMode.Office2007BlueDarkMode:
            case PaletteMode.Office2007BlueLightMode:
            case PaletteMode.Office2007Silver:
            case PaletteMode.Office2007SilverDarkMode:
            case PaletteMode.Office2007SilverLightMode:
            case PaletteMode.Office2007White:
            case PaletteMode.Office2007Black:
            case PaletteMode.Office2007BlackDarkMode:
            case PaletteMode.Office2007LimeGreen:
            case PaletteMode.Office2007LimeGreenDark:
            case PaletteMode.Office2007MaterializeBlue:
            case PaletteMode.Office2007MaterializeBlueDark:
            case PaletteMode.Office2007MaterializeLightBlue:
            case PaletteMode.Office2007MaterializeLightBlueDark:
            case PaletteMode.Office2007SilverDarkModeAlternate:
            case PaletteMode.VisualStudio2010Render2007:
            case PaletteMode.Office2007HighContrast:
            case PaletteMode.Office2007Deuteranopia:
            case PaletteMode.Office2007Protanopia:
                Images.ToolbarImages.SetToolBarImages(ToolkitStaticVariables.Office2007ToolBarImages);
                break;
            case PaletteMode.Office2010Blue:
            case PaletteMode.Office2010BlueDarkMode:
            case PaletteMode.Office2010BlueLightMode:
            case PaletteMode.Office2010Silver:
            case PaletteMode.Office2010SilverDarkMode:
            case PaletteMode.Office2010SilverLightMode:
            case PaletteMode.Office2010White:
            case PaletteMode.Office2010Black:
            case PaletteMode.Office2010BlackDarkMode:
            case PaletteMode.SparkleBlue:
            case PaletteMode.SparkleBlueDarkMode:
            case PaletteMode.SparkleBlueLightMode:
            case PaletteMode.SparkleOrange:
            case PaletteMode.SparkleOrangeDarkMode:
            case PaletteMode.SparkleOrangeLightMode:
            case PaletteMode.SparklePurple:
            case PaletteMode.SparklePurpleDarkMode:
            case PaletteMode.SparklePurpleLightMode:
            case PaletteMode.SparkleHighContrast:
            case PaletteMode.SparkleDeuteranopia:
            case PaletteMode.SparkleProtanopia:
            case PaletteMode.Office2010LimeGreen:
            case PaletteMode.Office2010LimeGreenDark:
            case PaletteMode.Office2010MaterializeBlue:
            case PaletteMode.Office2010MaterializeBlueDark:
            case PaletteMode.Office2010MaterializeLightBlue:
            case PaletteMode.Office2010MaterializeLightBlueDark:
            case PaletteMode.Office2010SilverDarkModeAlternate:
            case PaletteMode.VisualStudio2010Render2010:
            case PaletteMode.Office2010HighContrast:
            case PaletteMode.Office2010Deuteranopia:
            case PaletteMode.Office2010Protanopia:
                Images.ToolbarImages.SetToolBarImages(ToolkitStaticVariables.Office2010ToolBarImages);
                break;
            case PaletteMode.Office2013DarkGray:
            case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
            case PaletteMode.VisualStudio2010Render2013:
            case PaletteMode.Office2013HighContrast:
            case PaletteMode.Office2013Deuteranopia:
            case PaletteMode.Office2013Protanopia:
            case PaletteMode.Office2013MaterializeBlue:
            case PaletteMode.Office2013MaterializeBlueDark:
            case PaletteMode.Office2013MaterializeLightBlue:
            case PaletteMode.Office2013MaterializeLightBlueDark:
            case PaletteMode.Office2013SilverDarkModeAlternate:
                Images.ToolbarImages.SetToolBarImages(ToolkitStaticVariables.Office2013ToolBarImages);
                break;
            case PaletteMode.Microsoft365Black:
            case PaletteMode.Microsoft365BlackDarkMode:
            case PaletteMode.Microsoft365BlackDarkModeAlternate:
            case PaletteMode.Microsoft365Blue:
            case PaletteMode.Microsoft365BlueDarkMode:
            case PaletteMode.Microsoft365BlueLightMode:
            case PaletteMode.Microsoft365Silver:
            case PaletteMode.Microsoft365SilverDarkMode:
            case PaletteMode.Microsoft365SilverLightMode:
            case PaletteMode.Microsoft365White:
            case PaletteMode.Microsoft365LimeGreen:
            case PaletteMode.Microsoft365LimeGreenDark:
            case PaletteMode.Microsoft365MaterializeBlue:
            case PaletteMode.Microsoft365MaterializeBlueDark:
            case PaletteMode.Microsoft365MaterializeLightBlue:
            case PaletteMode.Microsoft365MaterializeLightBlueDark:
            case PaletteMode.Microsoft365SilverDarkModeAlternate:
            case PaletteMode.VisualStudio2010Render365:
            case PaletteMode.HighContrast:
            case PaletteMode.Deuteranopia:
            case PaletteMode.Protanopia:
                Images.ToolbarImages.SetToolBarImages(ToolkitStaticVariables.Microsoft365ToolBarImages);
                break;
            case PaletteMode.VisualStudio2022Dark:
                Images.ToolbarImages.SetToolBarImages(ToolkitStaticVariables.VisualStudioToolBarImages);
                break;
            case PaletteMode.MaterialLight:
            case PaletteMode.MaterialDark:
            case PaletteMode.MaterialLightRipple:
            case PaletteMode.MaterialDarkRipple:
            case PaletteMode.MaterialLimeGreen:
            case PaletteMode.MaterialLimeGreenDark:
            case PaletteMode.MaterialLimeGreenRipple:
            case PaletteMode.MaterialLimeGreenDarkRipple:
            case PaletteMode.MaterialMaterializeBlue:
            case PaletteMode.MaterialMaterializeBlueDark:
            case PaletteMode.MaterialMaterializeBlueRipple:
            case PaletteMode.MaterialMaterializeBlueDarkRipple:
            case PaletteMode.MaterialMaterializeLightBlue:
            case PaletteMode.MaterialMaterializeLightBlueDark:
            case PaletteMode.MaterialMaterializeLightBlueRipple:
            case PaletteMode.MaterialMaterializeLightBlueDarkRipple:
            case PaletteMode.MaterialSilverDarkModeAlternate:
            case PaletteMode.MaterialSilverDarkModeAlternateRipple:
            case PaletteMode.MaterialHighContrast:
            case PaletteMode.MaterialDeuteranopia:
            case PaletteMode.MaterialProtanopia:
            case PaletteMode.MaterialHighContrastRipple:
            case PaletteMode.MaterialDeuteranopiaRipple:
            case PaletteMode.MaterialProtanopiaRipple:
                // TODO create our own Material images
                Images.ToolbarImages.SetToolBarImages(ToolkitStaticVariables.Microsoft365ToolBarImages);
                break;
            case PaletteMode.RetroGreen:
            case PaletteMode.RetroBlue:
                Images.ToolbarImages.SetToolBarImages(ToolkitStaticVariables.Office2010ToolBarImages);
                break;
            case PaletteMode.MacOSXAqua:
            case PaletteMode.MacOSLight:
            case PaletteMode.MacOSDark:
                Images.ToolbarImages.SetToolBarImages(ToolkitStaticVariables.Microsoft365ToolBarImages);
                break;
            default:
                // Should not happen!

                // Disable since palette mode is an enum and is not nullable
                //Debug.Assert(paletteMode is not null);

                DebugTools.NotImplemented(paletteMode.ToString());
                break;
        }
    }

    private static void UpdateToolStripManager()
    {
        if (_globalApplyToolstrips)
        {
            ToolStripManager.Renderer = CurrentGlobalPalette?.GetRenderer()?.RenderToolStrip(CurrentGlobalPalette);
        }
    }

    private static void ResetToolStripManager() => ToolStripManager.RenderMode = ToolStripManagerRenderMode.Professional;

    private static void OnGlobalTouchscreenSupportChanged(EventArgs e)
    {
        // Capture event handler to avoid race condition during invocation
        var handler = GlobalTouchscreenSupportChanged;
        handler?.Invoke(null, e);
    }

    #endregion

    #region DPI-Aware Helper Methods

    /// <summary>
    /// Gets the current DPI scaling factor for the X axis (horizontal).
    /// Returns 1.0 for 96 DPI (100% scaling), 1.25 for 120 DPI (125% scaling), etc.
    /// Uses the primary monitor's DPI. For per-monitor DPI awareness, use the overload that accepts a window handle.
    /// </summary>
    /// <returns>The DPI scaling factor for the X axis.</returns>
    public static float GetDpiFactorX()
    {
        if (_cachedDpiX <= 0.1f)
        {
            var screenDc = PI.GetDC(IntPtr.Zero);

            if (screenDc != IntPtr.Zero)
            {
                _cachedDpiX = PI.GetDeviceCaps(screenDc, PI.DeviceCap.LOGPIXELSX) / 96f;

                PI.ReleaseDC(IntPtr.Zero, screenDc);
            }
            else
            {
                using (Graphics gfx = Graphics.FromHwnd(IntPtr.Zero))
                {
                    _cachedDpiX = gfx.DpiX / 96f;
                }
            }
        }

        return _cachedDpiX;
    }

    /// <summary>
    /// Gets the DPI scaling factor for the X axis (horizontal) for a specific window.
    /// This method supports per-monitor DPI awareness by using the window's monitor DPI.
    /// Returns 1.0 for 96 DPI (100% scaling), 1.25 for 120 DPI (125% scaling), etc.
    /// </summary>
    /// <param name="hWnd">Window handle to get the DPI for. If IntPtr.Zero, falls back to primary monitor DPI.</param>
    /// <returns>The DPI scaling factor for the X axis.</returns>
    public static float GetDpiFactorX(IntPtr hWnd)
    {
        if (hWnd == IntPtr.Zero)
        {
            return GetDpiFactorX();
        }

        // Try to use GetDpiForWindow for per-monitor DPI awareness (Windows 10 version 1607+)
        try
        {
            uint dpi = PI.GetDpiForWindow(hWnd);

            if (dpi > 0)
            {
                return dpi / 96f;
            }
        }
        catch
        {
            // GetDpiForWindow may not be available on older Windows versions
        }

        // Fallback to window's Graphics DPI
        try
        {
            using (Graphics graphics = Graphics.FromHwnd(hWnd))
            {
                return graphics.DpiX / 96f;
            }
        }
        catch
        {
            // Final fallback to primary monitor
            return GetDpiFactorX();
        }
    }

    /// <summary>
    /// Gets the current DPI scaling factor for the Y axis (vertical).
    /// Returns 1.0 for 96 DPI (100% scaling), 1.25 for 120 DPI (125% scaling), etc.
    /// Uses the primary monitor's DPI. For per-monitor DPI awareness, use the overload that accepts a window handle.
    /// </summary>
    /// <returns>The DPI scaling factor for the Y axis.</returns>
    public static float GetDpiFactorY()
    {
        if (_cachedDpiY <= 0.1f)
        {
            var screenDc = PI.GetDC(IntPtr.Zero);
            if (screenDc != IntPtr.Zero)
            {
                _cachedDpiY = PI.GetDeviceCaps(screenDc, PI.DeviceCap.LOGPIXELSY) / 96f;
                PI.ReleaseDC(IntPtr.Zero, screenDc);
            }
            else
            {
                // Fallback method
                using Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
                _cachedDpiY = graphics.DpiY / 96f;
            }
        }

        return _cachedDpiY;
    }

    /// <summary>
    /// Gets the DPI scaling factor for the Y axis (vertical) for a specific window.
    /// This method supports per-monitor DPI awareness by using the window's monitor DPI.
    /// Returns 1.0 for 96 DPI (100% scaling), 1.25 for 120 DPI (125% scaling), etc.
    /// </summary>
    /// <param name="hWnd">Window handle to get the DPI for. If IntPtr.Zero, falls back to primary monitor DPI.</param>
    /// <returns>The DPI scaling factor for the Y axis.</returns>
    public static float GetDpiFactorY(IntPtr hWnd)
    {
        if (hWnd == IntPtr.Zero)
        {
            return GetDpiFactorY();
        }

        // Try to use GetDpiForWindow for per-monitor DPI awareness (Windows 10 version 1607+)
        try
        {
            uint dpi = PI.GetDpiForWindow(hWnd);
            if (dpi > 0)
            {
                return dpi / 96f;
            }
        }
        catch
        {
            // GetDpiForWindow may not be available on older Windows versions
        }

        // Fallback to window's Graphics DPI
        try
        {
            using Graphics graphics = Graphics.FromHwnd(hWnd);
            return graphics.DpiY / 96f;
        }
        catch
        {
            // Final fallback to primary monitor
            return GetDpiFactorY();
        }
    }

    /// <summary>
    /// Gets the current DPI scaling factor (average of X and Y axes).
    /// Useful when uniform scaling is assumed.
    /// Uses the primary monitor's DPI. For per-monitor DPI awareness, use the overload that accepts a window handle.
    /// </summary>
    /// <returns>The average DPI scaling factor.</returns>
    public static float GetDpiFactor() => (GetDpiFactorX() + GetDpiFactorY()) / 2f;

    /// <summary>
    /// Gets the DPI scaling factor (average of X and Y axes) for a specific window.
    /// This method supports per-monitor DPI awareness by using the window's monitor DPI.
    /// </summary>
    /// <param name="hWnd">Window handle to get the DPI for. If IntPtr.Zero, falls back to primary monitor DPI.</param>
    /// <returns>The average DPI scaling factor.</returns>
    public static float GetDpiFactor(IntPtr hWnd) => (GetDpiFactorX(hWnd) + GetDpiFactorY(hWnd)) / 2f;

    /// <summary>
    /// Gets the combined scaling factor (DPI × Touchscreen) for the X axis.
    /// This represents the total scaling that will be applied to control sizes.
    /// Uses the primary monitor's DPI. For per-monitor DPI awareness, use the overload that accepts a window handle.
    /// </summary>
    /// <returns>The combined scaling factor for the X axis.</returns>
    public static float GetCombinedScaleFactorX()
    {
        var dpiFactor = GetDpiFactorX();
        var touchscreenFactor = TouchscreenScaleFactor;
        return dpiFactor * touchscreenFactor;
    }

    /// <summary>
    /// Gets the combined scaling factor (DPI × Touchscreen) for the X axis for a specific window.
    /// This method supports per-monitor DPI awareness, which is important for touchscreen support on high DPI displays.
    /// This represents the total scaling that will be applied to control sizes.
    /// </summary>
    /// <param name="hWnd">Window handle to get the DPI for. If IntPtr.Zero, falls back to primary monitor DPI.</param>
    /// <returns>The combined scaling factor for the X axis.</returns>
    public static float GetCombinedScaleFactorX(IntPtr hWnd)
    {
        var dpiFactor = GetDpiFactorX(hWnd);
        var touchscreenFactor = TouchscreenScaleFactor;
        return dpiFactor * touchscreenFactor;
    }

    /// <summary>
    /// Gets the combined scaling factor (DPI × Touchscreen) for the Y axis.
    /// This represents the total scaling that will be applied to control sizes.
    /// Uses the primary monitor's DPI. For per-monitor DPI awareness, use the overload that accepts a window handle.
    /// </summary>
    /// <returns>The combined scaling factor for the Y axis.</returns>
    public static float GetCombinedScaleFactorY()
    {
        var dpiFactor = GetDpiFactorY();
        var touchscreenFactor = TouchscreenScaleFactor;
        return dpiFactor * touchscreenFactor;
    }

    /// <summary>
    /// Gets the combined scaling factor (DPI × Touchscreen) for the Y axis for a specific window.
    /// This method supports per-monitor DPI awareness, which is important for touchscreen support on high DPI displays.
    /// This represents the total scaling that will be applied to control sizes.
    /// </summary>
    /// <param name="hWnd">Window handle to get the DPI for. If IntPtr.Zero, falls back to primary monitor DPI.</param>
    /// <returns>The combined scaling factor for the Y axis.</returns>
    public static float GetCombinedScaleFactorY(IntPtr hWnd)
    {
        var dpiFactor = GetDpiFactorY(hWnd);
        var touchscreenFactor = TouchscreenScaleFactor;
        return dpiFactor * touchscreenFactor;
    }

    /// <summary>
    /// Gets the combined scaling factor (DPI × Touchscreen) as an average.
    /// Useful when uniform scaling is assumed.
    /// Uses the primary monitor's DPI. For per-monitor DPI awareness, use the overload that accepts a window handle.
    /// </summary>
    /// <returns>The average combined scaling factor.</returns>
    public static float GetCombinedScaleFactor() => (GetCombinedScaleFactorX() + GetCombinedScaleFactorY()) / 2f;

    /// <summary>
    /// Gets the combined scaling factor (DPI × Touchscreen) as an average for a specific window.
    /// This method supports per-monitor DPI awareness, which is important for touchscreen support on high DPI displays.
    /// Useful when uniform scaling is assumed.
    /// </summary>
    /// <param name="hWnd">Window handle to get the DPI for. If IntPtr.Zero, falls back to primary monitor DPI.</param>
    /// <returns>The average combined scaling factor.</returns>
    public static float GetCombinedScaleFactor(IntPtr hWnd) => (GetCombinedScaleFactorX(hWnd) + GetCombinedScaleFactorY(hWnd)) / 2f;

    /// <summary>
    /// Scales a single value by the current DPI factor.
    /// </summary>
    /// <param name="value">The value to scale.</param>
    /// <returns>The scaled value.</returns>
    public static int ScaleValueByDpi(int value) => (int)Math.Round(value * GetDpiFactor());

    /// <summary>
    /// Scales a single value by the current DPI factor.
    /// </summary>
    /// <param name="value">The value to scale.</param>
    /// <returns>The scaled value.</returns>
    public static float ScaleValueByDpi(float value) => value * GetDpiFactor();

    /// <summary>
    /// Scales a single value by the combined DPI and touchscreen factor.
    /// </summary>
    /// <param name="value">The value to scale.</param>
    /// <returns>The scaled value.</returns>
    public static int ScaleValueByDpiAndTouchscreen(int value) => (int)Math.Round(value * GetCombinedScaleFactor());

    /// <summary>
    /// Scales a single value by the combined DPI and touchscreen factor.
    /// </summary>
    /// <param name="value">The value to scale.</param>
    /// <returns>The scaled value.</returns>
    public static float ScaleValueByDpiAndTouchscreen(float value) => value * GetCombinedScaleFactor();

    /// <summary>
    /// Scales a Size by the current DPI factors (X and Y separately).
    /// </summary>
    /// <param name="size">The size to scale.</param>
    /// <returns>The scaled size.</returns>
    public static Size ScaleSizeByDpi(Size size) => new Size(
        (int)Math.Round(size.Width * GetDpiFactorX()),
        (int)Math.Round(size.Height * GetDpiFactorY()));

    /// <summary>
    /// Scales a SizeF by the current DPI factors (X and Y separately).
    /// </summary>
    /// <param name="size">The size to scale.</param>
    /// <returns>The scaled size.</returns>
    public static SizeF ScaleSizeByDpi(SizeF size) => new SizeF(
        size.Width * GetDpiFactorX(),
        size.Height * GetDpiFactorY());

    /// <summary>
    /// Scales a Size by the combined DPI and touchscreen factors (X and Y separately).
    /// </summary>
    /// <param name="size">The size to scale.</param>
    /// <returns>The scaled size.</returns>
    public static Size ScaleSizeByDpiAndTouchscreen(Size size) => new Size(
        (int)Math.Round(size.Width * GetCombinedScaleFactorX()),
        (int)Math.Round(size.Height * GetCombinedScaleFactorY()));

    /// <summary>
    /// Scales a SizeF by the combined DPI and touchscreen factors (X and Y separately).
    /// </summary>
    /// <param name="size">The size to scale.</param>
    /// <returns>The scaled size.</returns>
    public static SizeF ScaleSizeByDpiAndTouchscreen(SizeF size) => new SizeF(
        size.Width * GetCombinedScaleFactorX(),
        size.Height * GetCombinedScaleFactorY());

    /// <summary>
    /// Scales a Point by the current DPI factors (X and Y separately).
    /// </summary>
    /// <param name="point">The point to scale.</param>
    /// <returns>The scaled point.</returns>
    public static Point ScalePointByDpi(Point point) => new Point(
        (int)Math.Round(point.X * GetDpiFactorX()),
        (int)Math.Round(point.Y * GetDpiFactorY()));

    /// <summary>
    /// Scales a PointF by the current DPI factors (X and Y separately).
    /// </summary>
    /// <param name="point">The point to scale.</param>
    /// <returns>The scaled point.</returns>
    public static PointF ScalePointByDpi(PointF point) => new PointF(
        point.X * GetDpiFactorX(),
        point.Y * GetDpiFactorY());

    /// <summary>
    /// Scales a Point by the combined DPI and touchscreen factors (X and Y separately).
    /// </summary>
    /// <param name="point">The point to scale.</param>
    /// <returns>The scaled point.</returns>
    public static Point ScalePointByDpiAndTouchscreen(Point point) => new Point(
        (int)Math.Round(point.X * GetCombinedScaleFactorX()),
        (int)Math.Round(point.Y * GetCombinedScaleFactorY()));

    /// <summary>
    /// Scales a PointF by the combined DPI and touchscreen factors (X and Y separately).
    /// </summary>
    /// <param name="point">The point to scale.</param>
    /// <returns>The scaled point.</returns>
    public static PointF ScalePointByDpiAndTouchscreen(PointF point) => new PointF(
        point.X * GetCombinedScaleFactorX(),
        point.Y * GetCombinedScaleFactorY());

    /// <summary>
    /// Scales a Rectangle by the current DPI factors (X and Y separately).
    /// </summary>
    /// <param name="rect">The rectangle to scale.</param>
    /// <returns>The scaled rectangle.</returns>
    public static Rectangle ScaleRectangleByDpi(Rectangle rect) => new Rectangle(
        (int)Math.Round(rect.X * GetDpiFactorX()),
        (int)Math.Round(rect.Y * GetDpiFactorY()),
        (int)Math.Round(rect.Width * GetDpiFactorX()),
        (int)Math.Round(rect.Height * GetDpiFactorY()));

    /// <summary>
    /// Scales a RectangleF by the current DPI factors (X and Y separately).
    /// </summary>
    /// <param name="rect">The rectangle to scale.</param>
    /// <returns>The scaled rectangle.</returns>
    public static RectangleF ScaleRectangleByDpi(RectangleF rect) => new RectangleF(
        rect.X * GetDpiFactorX(),
        rect.Y * GetDpiFactorY(),
        rect.Width * GetDpiFactorX(),
        rect.Height * GetDpiFactorY());

    /// <summary>
    /// Scales a Rectangle by the combined DPI and touchscreen factors (X and Y separately).
    /// </summary>
    /// <param name="rect">The rectangle to scale.</param>
    /// <returns>The scaled rectangle.</returns>
    public static Rectangle ScaleRectangleByDpiAndTouchscreen(Rectangle rect) => new Rectangle(
        (int)Math.Round(rect.X * GetCombinedScaleFactorX()),
        (int)Math.Round(rect.Y * GetCombinedScaleFactorY()),
        (int)Math.Round(rect.Width * GetCombinedScaleFactorX()),
        (int)Math.Round(rect.Height * GetCombinedScaleFactorY()));

    /// <summary>
    /// Scales a RectangleF by the combined DPI and touchscreen factors (X and Y separately).
    /// </summary>
    /// <param name="rect">The rectangle to scale.</param>
    /// <returns>The scaled rectangle.</returns>
    public static RectangleF ScaleRectangleByDpiAndTouchscreen(RectangleF rect) => new RectangleF(
        rect.X * GetCombinedScaleFactorX(),
        rect.Y * GetCombinedScaleFactorY(),
        rect.Width * GetCombinedScaleFactorX(),
        rect.Height * GetCombinedScaleFactorY());

    /// <summary>
    /// Invalidates the cached DPI factors, forcing them to be recalculated on the next access.
    /// Call this method when the DPI changes (e.g., when the window is moved to a different monitor).
    /// </summary>
    public static void InvalidateDpiCache()
    {
        _cachedDpiX = 0f;
        _cachedDpiY = 0f;
    }

    #endregion
}
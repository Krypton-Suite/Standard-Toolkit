#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2025. All rights reserved.
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
    internal static bool _globalUseKryptonFileDialogs = true;
    private static Font? _baseFont;

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

    #endregion

    private static RenderStandard? _renderStandard;
    private static RenderProfessional? _renderProfessional;
    private static RenderOffice2007? _renderOffice2007;
    private static RenderOffice2010? _renderOffice2010;
    private static RenderOffice2013? _renderOffice2013;
    private static RenderMicrosoft365? _renderMicrosoft365;
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
    #endregion

    #region Identity
    static KryptonManager()
    {
        // We need to notice when system color settings change
        SystemEvents.UserPreferenceChanged += OnUserPreferenceChanged;

        // Update the tool strip global renderer with the default setting
        UpdateToolStripManager();
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
            throw new ArgumentNullException(nameof(container));
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
                               ShouldSerializeToolkitStrings() ||
                               ShouldSerializeUseKryptonFileDialogs() ||
                               ShouldSerializeBaseFont() ||
                               ShouldSerializeGlobalPaletteMode());

    /// <summary>
    /// Reset All values
    /// </summary>
    public void Reset()
    {
        ResetGlobalCustomPalette();
        ResetToolkitColors();
        ResetGlobalApplyToolstrips();
        ResetGlobalUseThemeFormChromeBorderWidth();
        ResetToolkitStrings();
        ResetUseKryptonFileDialogs();
        ResetBaseFont();
        ResetGlobalPaletteMode();
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

    #endregion

    #region Static Properties

    /// <summary>Gets the strings.</summary>
    /// <value>The strings.</value>
    public static KryptonGlobalToolkitStrings Strings { get; } = new KryptonGlobalToolkitStrings();

    /// <summary>Gets the images.</summary>
    /// <value>The images.</value>
    public static KryptonImageStorage Images { get; } = new KryptonImageStorage();

    /// <summary>Gets the colors.</summary>
    /// <value>The colors.</value>
    public static KryptonColorStorage Colors { get; } = new KryptonColorStorage();

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
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2013DarkGray:
            // return PaletteOffice2013DarkGray;
            // case PaletteMode.Office2013LightGray:
            // return PaletteOffice2013LightGray;
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

            case PaletteMode.Custom:
            case PaletteMode.Global:
                return CurrentGlobalPalette;
            default:
                Debug.Assert(false);
                throw new ArgumentOutOfRangeException(nameof(mode), @"mode must be PaletteMode value.");
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
            var modeConverter = new Krypton.Toolkit.Converters.PaletteClassTypeConverter();

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
    /// Gets the palette Microsft 365 black dark mode.
    /// </summary>
    public static PaletteMicrosoft365BlackDarkMode PaletteMicrosoft365BlackDarkMode => _paletteMicrosoft365BlackDarkMode ??= new PaletteMicrosoft365BlackDarkMode();

    /// <summary>
    /// Gets the palette Microsft 365 black dark mode alternate.
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
            case RendererMode.Inherit:
            case RendererMode.Custom:
            default:
                // Should never be passed
                Debug.Assert(false);
                throw new ArgumentOutOfRangeException(nameof(mode), @"mode must be RendererMode value.");
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
    }

    private static void SetPalette(PaletteBase globalPalette)
    {
        if (globalPalette != CurrentGlobalPalette)
        {
            // Unhook from current palette events
            if (CurrentGlobalPalette != null)
            {
                CurrentGlobalPalette.PalettePaint -= OnPalettePaint;
            }

            // Remember the new palette
            CurrentGlobalPalette = globalPalette;

            // Hook to new palette events
            if (CurrentGlobalPalette != null)
            {
                CurrentGlobalPalette.PalettePaint += OnPalettePaint;
            }
        }
    }

    private static void OnGlobalUseThemeFormChromeBorderWidthChanged(EventArgs e) => GlobalUseThemeFormChromeBorderWidthChanged?.Invoke(null, e);

    private static void OnGlobalPaletteChanged(EventArgs e)
    {
        UpdateToolStripManager();

        UpdatePaletteImages(CurrentGlobalPaletteMode);

        GlobalPaletteChanged?.Invoke(null, e);
    }

    private static void UpdatePaletteImages(PaletteMode paletteMode)
    {
        switch (paletteMode)
        {
            case PaletteMode.Global:
            case PaletteMode.Custom:
                Images.ToolbarImages.SetToolBarImages(GlobalStaticValues.GenericToolBarImages);
                break;
            case PaletteMode.ProfessionalSystem:
                Images.ToolbarImages.SetToolBarImages(GlobalStaticValues.SystemToolBarImages);
                break;
            case PaletteMode.ProfessionalOffice2003:
                Images.ToolbarImages.SetToolBarImages(GlobalStaticValues.Office2003ToolBarImages);
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
            case PaletteMode.VisualStudio2010Render2007:
                Images.ToolbarImages.SetToolBarImages(GlobalStaticValues.Office2007ToolBarImages);
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
            case PaletteMode.VisualStudio2010Render2010:
                Images.ToolbarImages.SetToolBarImages(GlobalStaticValues.Office2010ToolBarImages);
                break;
            case PaletteMode.Office2013White:
            case PaletteMode.VisualStudio2010Render2013:
                Images.ToolbarImages.SetToolBarImages(GlobalStaticValues.Office2013ToolBarImages);
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
            case PaletteMode.VisualStudio2010Render365:
                Images.ToolbarImages.SetToolBarImages(GlobalStaticValues.Microsoft365ToolBarImages);
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

    #endregion

}
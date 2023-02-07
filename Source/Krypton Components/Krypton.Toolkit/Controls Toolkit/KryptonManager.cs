#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Exposes global settings that affect all the Krypton controls.
    /// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonManager), "ToolboxBitmaps.KryptonManager.bmp")]
    [Designer("Krypton.Toolkit.KryptonManagerDesigner, Krypton.Toolkit")]
    [DefaultProperty("GlobalPaletteMode")]
    [Description(@"Access 'Global' Krypton settings.")]
    public sealed class KryptonManager : Component
    {
        #region Static Fields
        // Initialize the global state
        private static bool _globalApplyToolstrips = true;
        private static bool _globalAllowFormChrome = true;

        // Initialize the default modes

        // Initialize instances to match the default modes

        // Singleton instances are created on demand
        private static PaletteProfessionalOffice2003 _paletteProfessionalOffice2003;
        private static PaletteProfessionalSystem _paletteProfessionalSystem;

        private static PaletteBase _customPalette;

        #region Office 2007 Themes

        private static PaletteOffice2007DarkGray _paletteOffice2007DarkGray;
        private static PaletteOffice2007Blue _paletteOffice2007Blue;
        private static PaletteOffice2007BlueDarkMode _paletteOffice2007BlueDarkMode;
        private static PaletteOffice2007BlueLightMode _paletteOffice2007BlueLightMode;
        private static PaletteOffice2007Silver _paletteOffice2007Silver;
        private static PaletteOffice2007SilverDarkMode _paletteOffice2007SilverDarkMode;
        private static PaletteOffice2007SilverLightMode _paletteOffice2007SilverLightMode;
        private static PaletteOffice2007White _paletteOffice2007White;
        private static PaletteOffice2007Black _paletteOffice2007Black;
        private static PaletteOffice2007BlackDarkMode _paletteOffice2007BlackDarkMode;

        #endregion

        #region Office 2010 Themes

        private static PaletteOffice2010DarkGray _paletteOffice2010DarkGray;
        private static PaletteOffice2010Blue _paletteOffice2010Blue;
        private static PaletteOffice2010BlueDarkMode _paletteOffice2010BlueDarkMode;
        private static PaletteOffice2010BlueLightMode _paletteOffice2010BlueLightMode;
        private static PaletteOffice2010White _paletteOffice2010White;
        private static PaletteOffice2010Black _paletteOffice2010Black;
        private static PaletteOffice2010BlackDarkMode _paletteOffice2010BlackDarkMode;
        private static PaletteOffice2010Silver _paletteOffice2010Silver;
        private static PaletteOffice2010SilverDarkMode _paletteOffice2010SilverDarkMode;
        private static PaletteOffice2010SilverLightMode _paletteOffice2010SilverLightMode;

        #endregion

        #region Office 2013 Themes

        private static PaletteOffice2013DarkGray _paletteOffice2013DarkGray;
        private static PaletteOffice2013White _paletteOffice2013White;

        #endregion

        #region Sparkle Themes

        private static PaletteSparkleBlue _paletteSparkleBlue;
        private static PaletteSparkleBlueDarkMode _paletteSparkleBlueDarkMode;
        private static PaletteSparkleBlueLightMode _paletteSparkleBlueLightMode;
        private static PaletteSparkleOrange _paletteSparkleOrange;
        private static PaletteSparkleOrangeDarkMode _paletteSparkleOrangeDarkMode;
        private static PaletteSparkleOrangeLightMode _paletteSparkleOrangeLightMode;
        private static PaletteSparklePurple _paletteSparklePurple;
        private static PaletteSparklePurpleDarkMode _paletteSparklePurpleDarkMode;
        private static PaletteSparklePurpleLightMode _paletteSparklePurpleLightMode;

        #endregion

        #region Microsoft 365 Themes

        private static PaletteMicrosoft365DarkGray _paletteMicrosoft365DarkGray;
        private static PaletteMicrosoft365Black _paletteMicrosoft365Black;
        private static PaletteMicrosoft365BlackDarkMode _paletteMicrosoft365BlackDarkMode;
        private static PaletteMicrosoft365Blue _paletteMicrosoft365Blue;
        private static PaletteMicrosoft365BlueDarkMode _paletteMicrosoft365BlueDarkMode;
        private static PaletteMicrosoft365BlueLightMode _paletteMicrosoft365BlueLightMode;
        private static PaletteMicrosoft365Silver _paletteMicrosoft365Silver;
        private static PaletteMicrosoft365SilverDarkMode _paletteMicrosoft365SilverDarkMode;
        private static PaletteMicrosoft365SilverLightMode _paletteMicrosoft365SilverLightMode;
        private static PaletteMicrosoft365White _paletteMicrosoft365White;

        #endregion

        private static RenderStandard _renderStandard;
        private static RenderProfessional _renderProfessional;
        private static RenderOffice2007 _renderOffice2007;
        private static RenderOffice2010 _renderOffice2010;
        private static RenderOffice2013 _renderOffice2013;
        private static RenderMicrosoft365 _renderMicrosoft365;
        private static RenderSparkle _renderSparkle;

        private static KryptonCustomPaletteManager _customPaletteManager;
        #endregion

        #region Static Events
        /// <summary>
        /// Occurs when the palette changes.
        /// </summary>
        [Category(@"Property Changed")]
        [Description(@"Occurs when the value of the GlobalPalette property is changed.")]
        public static event EventHandler GlobalPaletteChanged;

        /// <summary>
        /// Occurs when the AllowFormChrome property changes.
        /// </summary>
        [Category(@"Property Changed")]
        [Description(@"Occurs when the value of the GlobalAllowFormChrome property is changed.")]
        public static event EventHandler GlobalAllowFormChromeChanged;
        #endregion

        #region Instance Fields

        //private readonly bool _useOSLanguageStrings;

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
        public KryptonManager() =>
            // This may not be the first form / object to init, so set to the global static
            GlobalPaletteMode = InternalGlobalPaletteMode;

        /// <summary>
        /// Initialize a new instance of the KryptonManager class.
        /// </summary>
        /// <param name="container">Container that owns the component.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public KryptonManager(IContainer container)
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
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDefault => !(ShouldSerializeGlobalPaletteMode()
                                   || ShouldSerializeGlobalPalette()
                                   || ShouldSerializeGlobalApplyToolstrips()
                                   || ShouldSerializeGlobalAllowFormChrome()
                                   || ShouldSerializeGlobalStrings()
                                   || ShouldSerializeGlobalColorStrings()
                                );

        /// <summary>
        /// Reset All values
        /// </summary>
        public void Reset()
        {
            ResetGlobalPaletteMode();
            ResetGlobalPalette();
            ResetGlobalApplyToolstrips();
            ResetGlobalAllowFormChrome();
            ResetGlobalStrings();
            ResetGlobalColorStrings();

            _customPalette = null;
        }

        /// <summary>
        /// Gets or sets the global palette used for drawing.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Global palette applied to drawing.")]
        //[DefaultValue(typeof(PaletteMode), "Microsoft365Blue")]
        public PaletteMode GlobalPaletteMode
        {
            get => InternalGlobalPaletteMode;

            set
            {
                // Only interested in changes of value
                if (InternalGlobalPaletteMode != value)
                {
                    // Action depends on the value
                    switch (value)
                    {
                        case PaletteMode.Custom:
                            // Do nothing, you must assign a palette to the 
                            // 'GlobalPalette' property in order to get the custom mode
                            break;
                        default:
                            // Cache the new values
                            PaletteMode tempMode = InternalGlobalPaletteMode;
                            PaletteBase tempPalette = InternalGlobalPalette;

                            // Use the new value
                            InternalGlobalPaletteMode = value;
                            InternalGlobalPalette = null;

                            // If the new value creates a circular reference
                            if (HasCircularReference())
                            {
                                // Restore the original values before throwing
                                InternalGlobalPaletteMode = tempMode;
                                InternalGlobalPalette = tempPalette;

                                throw new ArgumentOutOfRangeException(nameof(value),
                                    @"Cannot use palette that would create a circular reference");
                            }
                            else
                            {
                                // Restore the original global palette as 'SetPalette' will not 
                                // work correctly unless it still has the old value in place
                                InternalGlobalPalette = tempPalette;
                            }

                            // Get a reference to the standard palette from its name
                            SetPalette(CurrentGlobalPalette);

                            // Raise the palette changed event
                            OnGlobalPaletteChanged(EventArgs.Empty);
                            break;
                    }
                }
            }
        }

        private bool ShouldSerializeGlobalPaletteMode() => GlobalPaletteMode != PaletteMode.Microsoft365Blue;

        private void ResetGlobalPaletteMode() => GlobalPaletteMode = PaletteMode.Microsoft365Blue;

        /// <summary>
        /// Gets and sets the global custom applied to drawing.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Global custom palette applied to drawing.")]
        [DefaultValue(null)]
        public PaletteBase GlobalPalette
        {
            get => InternalGlobalPalette;

            set
            {
                // Only interested in changes of value
                if (InternalGlobalPalette != value)
                {
                    // Cache the current values
                    PaletteMode tempMode = InternalGlobalPaletteMode;
                    PaletteBase tempPalette = InternalGlobalPalette;

                    // Use the new values
                    InternalGlobalPaletteMode = (value == null) ? PaletteMode.Microsoft365Blue : PaletteMode.Custom;
                    InternalGlobalPalette = value;

                    // If the new value creates a circular reference
                    if (HasCircularReference())
                    {
                        // Restore the original values
                        InternalGlobalPaletteMode = tempMode;
                        InternalGlobalPalette = tempPalette;

                        throw new ArgumentOutOfRangeException(nameof(value), @"Cannot use palette that would create a circular reference");
                    }
                    else
                    {
                        // Restore the original global palette as 'SetPalette' will not 
                        // work correctly unless it still has the old value in place
                        InternalGlobalPalette = tempPalette;
                    }

                    // Use the provided palette value
                    SetPalette(value);

                    // If no custom palette is required
                    if (value == null)
                    {
                        // Get a reference to current global palette defined by the mode
                        SetPalette(CurrentGlobalPalette);
                    }
                    else
                    {
                        // No longer using a standard palette
                        InternalGlobalPaletteMode = PaletteMode.Custom;
                    }

                    // Raise the palette changed event
                    OnGlobalPaletteChanged(EventArgs.Empty);
                }
            }
        }

        private bool ShouldSerializeGlobalPalette() => GlobalPalette != null;

        private void ResetGlobalPalette() => GlobalPaletteMode = PaletteMode.Microsoft365Blue;

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

        /// <summary>
        /// Gets or sets a value indicating if KryptonForm instances are allowed to show custom chrome.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Should KryptonForm instances be allowed to show custom chrome.")]
        [DefaultValue(true)]
        public bool GlobalAllowFormChrome
        {
            get => AllowFormChrome;
            set => AllowFormChrome = value;
        }

        private bool ShouldSerializeGlobalAllowFormChrome() => !GlobalAllowFormChrome;

        private void ResetGlobalAllowFormChrome() => GlobalAllowFormChrome = true;

        /// <summary>
        /// Gets a set of global strings used by Krypton that can be localized.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Collection of global strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public GlobalStrings GlobalStrings => Strings;

        private bool ShouldSerializeGlobalStrings() => !Strings.IsDefault;

        /// <summary>
        /// Resets the GlobalStrings property to its default value.
        /// </summary>
        public void ResetGlobalStrings() => Strings.Reset();

        /// <summary>
        /// Gets a set of global color strings used by Krypton that can be localized.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Collection of global color strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public GlobalColorStrings GlobalColorStrings => ColorStrings;

        private bool ShouldSerializeGlobalColorStrings() => !ColorStrings.IsDefault;

        /// <summary>
        /// Resets the GlobalColorStrings property to its default value.
        /// </summary>
        public void ResetGlobalColorStrings() => ColorStrings.Reset();

        [Category(@"Visuals")]
        [Description(@"")]
        [DefaultValue(null)]
        public KryptonCustomPaletteManager CustomPaletteManager
        {
            get => _customPaletteManager;
            set => _customPaletteManager = value;
        }

        /// <summary>Specify a custom palette outside the existing palettes.</summary>
        /// <value>A custom palette.</value>
        [Category(@"Visuals")]
        [Description(@"Specify a custom palette outside the existing palettes.")]
        [DefaultValue(null)]
        public PaletteBase CustomPalette
        {
            get => _customPalette;
            set => _customPalette = value;
        }

        #endregion

        #region Static ApplyToolstrips
        /// <summary>
        /// Gets and sets the global flag that decides if palette colors are applied to toolstrips.
        /// </summary>
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

        #region Static AllowFormChrome
        /// <summary>
        /// Gets and sets the global flag that decides if form chrome should be customized.
        /// </summary>
        public static bool AllowFormChrome
        {
            get => _globalAllowFormChrome;

            set
            {
                // Only interested if the value changes
                if (_globalAllowFormChrome != value)
                {
                    // Use new value
                    _globalAllowFormChrome = value;

                    // Fire change event
                    OnGlobalAllowFormChromeChanged(EventArgs.Empty);
                }
            }
        }
        #endregion

        #region Static Strings
        /// <summary>
        /// Gets access to the set of global strings.
        /// </summary>
        public static GlobalStrings Strings { get; } = new();

        /// <summary>
        /// Gets access to the set of global color strings.
        /// </summary>
        public static GlobalColorStrings ColorStrings { get; } = new();

        #endregion

        #region Static Palette
        /// <summary>
        /// Gets the current global palette instance given the manager settings.
        /// </summary>
        public static PaletteBase CurrentGlobalPalette
        {
            get
            {
                switch (InternalGlobalPaletteMode)
                {
                    case PaletteMode.ProfessionalSystem:
                        return PaletteProfessionalSystem;
                    case PaletteMode.ProfessionalOffice2003:
                        return PaletteProfessionalOffice2003;
                    case PaletteMode.Office2007DarkGray:
                        return PaletteOffice2007DarkGray;
                    case PaletteMode.Office2007Blue:
                        return PaletteOffice2007Blue;
                    case PaletteMode.Office2007BlueDarkMode:
                        return PaletteOffice2007BlueDarkMode;
                    case PaletteMode.Office2007BlueLightMode:
                        return PaletteOffice2007BlueLightMode;
                    // Note: Do something...
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
                    case PaletteMode.Office2007BlackDarkMode:
                        return PaletteOffice2007BlackDarkMode;
                    case PaletteMode.Office2010DarkGray:
                        return PaletteOffice2010DarkGray;
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
                    /*case PaletteMode.Office2013:
                        return PaletteOffice2013;*/
                    case PaletteMode.Office2013DarkGray:
                        return PaletteOffice2013DarkGray;
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
                    case PaletteMode.Microsoft365Blue:
                        return PaletteMicrosoft365Blue;
                    case PaletteMode.Microsoft365BlueDarkMode:
                        return PaletteMicrosoft365BlueDarkMode;
                    case PaletteMode.Microsoft365BlueLightMode:
                        return PaletteMicrosoft365BlueLightMode;
                    case PaletteMode.Microsoft365DarkGray:
                        return PaletteMicrosoft365DarkGray;
                    case PaletteMode.Microsoft365Silver:
                        return PaletteMicrosoft365Silver;
                    case PaletteMode.Microsoft365SilverDarkMode:
                        return PaletteMicrosoft365SilverDarkMode;
                    case PaletteMode.Microsoft365SilverLightMode:
                        return PaletteMicrosoft365SilverLightMode;
                    case PaletteMode.Microsoft365White:
                        return PaletteMicrosoft365White;
                    case PaletteMode.Custom:
                        return InternalGlobalPalette;
                    default:
                        Debug.Assert(false);
                        return null;
                }
            }
        }

        /// <summary>
        /// Gets the implementation for the requested palette mode.
        /// </summary>
        /// <param name="mode">Requested palette mode.</param>
        /// <returns>PaletteBase reference is available; otherwise false.</returns>
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
                case PaletteMode.Office2007DarkGray:
                    return PaletteOffice2007DarkGray;
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
                case PaletteMode.Office2010DarkGray:
                    return PaletteOffice2010DarkGray;
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
                /*case PaletteMode.Office2013:
                    return PaletteOffice2013;*/
                case PaletteMode.Office2013DarkGray:
                    return PaletteOffice2013DarkGray;
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
                case PaletteMode.Microsoft365BlueDarkMode:
                    return PaletteMicrosoft365BlueDarkMode;
                case PaletteMode.Microsoft365BlueLightMode:
                    return PaletteMicrosoft365BlueLightMode;
                case PaletteMode.Microsoft365Blue:
                    return PaletteMicrosoft365Blue;
                case PaletteMode.Microsoft365DarkGray:
                    return PaletteMicrosoft365DarkGray;
                case PaletteMode.Microsoft365Silver:
                    return PaletteMicrosoft365Silver;
                case PaletteMode.Microsoft365SilverDarkMode:
                    return PaletteMicrosoft365SilverDarkMode;
                case PaletteMode.Microsoft365SilverLightMode:
                    return PaletteMicrosoft365SilverLightMode;
                case PaletteMode.Microsoft365White:
                    return PaletteMicrosoft365White;
                case PaletteMode.Global:
                    return CurrentGlobalPalette;
                case PaletteMode.Custom:
                default:
                    Debug.Assert(false);
                    return null;
            }
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
        /// <value>
        /// The palette Microsoft365 black.
        /// </value>
        public static PaletteMicrosoft365Black PaletteMicrosoft365Black => _paletteMicrosoft365Black ??= new PaletteMicrosoft365Black();

        /// <summary>
        /// Gets the single instance of the ### palette.
        /// </summary>
        public static PaletteMicrosoft365BlackDarkMode PaletteMicrosoft365BlackDarkMode => _paletteMicrosoft365BlackDarkMode ??= new PaletteMicrosoft365BlackDarkMode();

        /// <summary>
        /// Gets the palette Microsoft365 blue.
        /// </summary>
        /// <value>
        /// The palette Microsoft365 blue.
        /// </value>
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
        /// <value>
        /// The palette Microsoft365 silver.
        /// </value>
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
        /// Gets the single instance of the ### palette.
        /// </summary>
        public static PaletteSparklePurpleDarkMode PaletteSparklePurpleDarkMode => _paletteSparklePurpleDarkMode ??= new PaletteSparklePurpleDarkMode();

        /// <summary>
        /// Gets the single instance of the ### palette.
        /// </summary>
        public static PaletteSparklePurpleLightMode PaletteSparklePurpleLightMode => _paletteSparklePurpleLightMode ??= new PaletteSparklePurpleLightMode();

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
                case RendererMode.Inherit:
                case RendererMode.Custom:
                default:
                    // Should never be passed
                    Debug.Assert(false);
                    return null;
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
        /// Gets the single instance of the standard renderer.
        /// </summary>
        public static RenderStandard RenderStandard => _renderStandard ??= new RenderStandard();

        #endregion

        #region Static Internal
        internal static PaletteMode InternalGlobalPaletteMode { get; private set; } = PaletteMode.Microsoft365Blue;

        internal static PaletteBase InternalGlobalPalette { get; private set; } = CurrentGlobalPalette;

        internal static bool HasCircularReference()
        {
            // Use a dictionary as a set to check for existence
            var paletteSet = new Dictionary<PaletteBase, bool>();

            PaletteBase palette = null;

            // Get the next palette up in hierarchy
            if (InternalGlobalPaletteMode == PaletteMode.Custom)
            {
                palette = InternalGlobalPalette;
            }

            // Keep searching until no more palettes found
            while (palette != null)
            {
                // If the palette has already been encountered then it is a circular reference
                if (paletteSet.ContainsKey(palette))
                {
                    return true;
                }
                else
                {
                    // Otherwise, add to the set
                    paletteSet.Add(palette, true);
                    // Cast to correct type

                    // If this is a KryptonPalette instance
                    if (palette is KryptonCustomPaletteBase owner)
                    {
                        // Get the next palette up in hierarchy
                        palette = owner.BasePaletteMode switch
                        {
                            PaletteMode.Custom => owner.BasePalette,
                            PaletteMode.Global => InternalGlobalPalette,
                            _ => null
                        };
                    }
                    else
                    {
                        palette = null;
                    }
                }
            }

            // No circular reference encountered
            return false;
        }
        #endregion

        #region Static Implementation
        private static void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            // Because we are static this event is fired before any instance controls are updated, so we need to
            // tell the palette instances to update now so that when the instance controls are updated the new fonts
            // and other resources are recreated as needed.

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



            UpdateToolStripManager();
        }

        private static void OnPalettePaint(object sender, PaletteLayoutEventArgs e)
        {
            // If the color table has changed then need to update tool strip immediately
            if (e.NeedColorTable)
            {
                UpdateToolStripManager();
            }
        }

        private static void SetPalette(PaletteBase globalPalette)
        {
            if (globalPalette != InternalGlobalPalette)
            {
                // Unhook from current palette events
                if (InternalGlobalPalette != null)
                {
                    InternalGlobalPalette.PalettePaint -= OnPalettePaint;
                }

                // Remember the new palette
                InternalGlobalPalette = globalPalette;

                // Hook to new palette events
                if (InternalGlobalPalette != null)
                {
                    InternalGlobalPalette.PalettePaint += OnPalettePaint;
                }
            }
        }

        private static void OnGlobalAllowFormChromeChanged(EventArgs e) => GlobalAllowFormChromeChanged?.Invoke(null, e);

        private static void OnGlobalPaletteChanged(EventArgs e)
        {
            UpdateToolStripManager();

            GlobalPaletteChanged?.Invoke(null, e);
        }

        private static void UpdateToolStripManager()
        {
            if (_globalApplyToolstrips)
            {
                ToolStripManager.Renderer = InternalGlobalPalette.GetRenderer().RenderToolStrip(InternalGlobalPalette);
            }
        }

        private static void ResetToolStripManager() => ToolStripManager.RenderMode = ToolStripManagerRenderMode.Professional;

        #endregion

    }
}
#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Toolkit
{
    /// <summary>
    /// Define and modify a palette for styling Krypton controls.
    /// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonPalette), "ToolboxBitmaps.KryptonPalette.bmp")]
    [DefaultEvent("PalettePaint")]
    [DefaultProperty("BasePaletteMode")]
    [DesignerCategory(@"code")]
    [Designer("Krypton.Toolkit.KryptonPaletteDesigner, Krypton.Toolkit")]
    [Description(@"A customisable palette component.")]
    public class KryptonPalette : Component, IPalette
    {
        #region Type Definitions
        private class ImageDictionary : Dictionary<Bitmap, string> { }
        private class ImageReverseDictionary : Dictionary<string, Bitmap> { }
        #endregion

        #region Static Fields
        private static readonly int _paletteVersion = 18;
        #endregion

        #region Instance Fields
        private int _suspendCount;
        private IRenderer _baseRenderer;
        private RendererMode _baseRenderMode;
        private IPalette _basePalette;
        private PaletteMode _basePaletteMode;
        private InheritBool _allowFormChrome;
        private readonly PaletteRedirect _redirector;
        private readonly PaletteRedirectCommon _redirectCommon;
        private readonly NeedPaintHandler _needPaintDelegate;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when a palette change requires a repaint.
        /// </summary>
        [Category(@"Action")]
        [Description(@"Occurs when a change requires a repaint to reflect the update.")]
        public event EventHandler<PaletteLayoutEventArgs> PalettePaint;

        /// <summary>
        /// Occurs when the AllowFormChrome setting changes.
        /// </summary>
        [Category(@"Action")]
        [Description(@"Occurs when the AllowFormChrome setting changes.")]
        public event EventHandler AllowFormChromeChanged;

        /// <summary>
        /// Occurs when the BasePalette/BasePaletteMode setting changes.
        /// </summary>
        [Category(@"Action")]
        [Description(@"Occurs when a base palette setting change occurs.")]
        public event EventHandler BasePaletteChanged;

        /// <summary>
        /// Occurs when the BaseRenderer/BaseRendererMode setting changes.
        /// </summary>
        [Category(@"Action")]
        [Description(@"Occurs when a base renderer setting change occurs.")]
        public event EventHandler BaseRendererChanged;

        /// <summary>
        /// Occurs when a button spec change occurs.
        /// </summary>
        [Category(@"Action")]
        [Description(@"Occurs when a button spec change occurs.")]
        public event EventHandler ButtonSpecChanged;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPalette class.
        /// </summary>
        public KryptonPalette()
        {
            // Setup the need paint delegate
            _needPaintDelegate = OnNeedPaint;

            // Set the default palette/palette mode
            _basePalette = KryptonManager.GetPaletteForMode(PaletteMode.Office365Blue);
            _basePaletteMode = PaletteMode.Office365Blue;

            // Set the default renderer
            _baseRenderer = null;
            _baseRenderMode = RendererMode.Inherit;

            // Create the redirector for passing requests onto the inherited palette
            _redirector = new PaletteRedirect(_basePalette);

            // Set default value of properties
            _allowFormChrome = InheritBool.Inherit;

            // Create the storage for the common states
            Common = new KryptonPaletteCommon(_redirector, _needPaintDelegate);

            // Create redirector so other storage inherits from common states
            _redirectCommon = new PaletteRedirectCommon(_redirector, Common.StateDisabled, Common.StateOthers);

            // Create the storage objects
            ButtonStyles = new KryptonPaletteCheckButtons(_redirectCommon, _needPaintDelegate);
            ButtonSpecs = new KryptonPaletteButtonSpecs(_redirector);
            CalendarDay = new KryptonPaletteCalendarDay(_redirector, _needPaintDelegate);
            Cargo = new KryptonPaletteCargo(_needPaintDelegate);
            ControlStyles = new KryptonPaletteControls(_redirectCommon, _needPaintDelegate);
            ContextMenu = new KryptonPaletteContextMenu(_redirectCommon, _needPaintDelegate);
            DragDrop = new PaletteDragDrop(_redirectCommon, _needPaintDelegate);
            FormStyles = new KryptonPaletteForms(_redirectCommon, _needPaintDelegate);
            GridStyles = new KryptonPaletteGrids(_redirectCommon, _needPaintDelegate);
            HeaderStyles = new KryptonPaletteHeaders(_redirectCommon, _needPaintDelegate);
            HeaderGroup = new KryptonPaletteHeaderGroup(_redirector, _needPaintDelegate);
            Images = new KryptonPaletteImages(_redirectCommon, _needPaintDelegate);
            InputControlStyles = new KryptonPaletteInputControls(_redirectCommon, _needPaintDelegate);
            LabelStyles = new KryptonPaletteLabels(_redirectCommon, _needPaintDelegate);
            Navigator = new KryptonPaletteNavigator(_redirectCommon, _needPaintDelegate);
            PanelStyles = new KryptonPalettePanels(_redirectCommon, _needPaintDelegate);
            Ribbon = new KryptonPaletteRibbon(_redirectCommon, _needPaintDelegate);
            SeparatorStyles = new KryptonPaletteSeparators(_redirectCommon, _needPaintDelegate);
            TabStyles = new KryptonPaletteTabButtons(_redirectCommon, _needPaintDelegate);
            TrackBar = new KryptonPaletteTrackBar(_redirectCommon, _needPaintDelegate);
            ToolMenuStatus = new KryptonPaletteTMS(this, _basePalette.ColorTable, OnMenuToolStatusPaint);

            // Hook into the storage change events
            ButtonSpecs.ButtonSpecChanged += OnButtonSpecChanged;

            // Hook to palette events
            if (_basePalette != null)
            {
                _basePalette.PalettePaint += OnPalettePaint;
                _basePalette.ButtonSpecChanged += OnButtonSpecChanged;
                _basePalette.BasePaletteChanged += OnBasePaletteChanged;
                _basePalette.BaseRendererChanged += OnBaseRendererChanged;
            }
        }

        /// <summary>
        /// Initialize a new instance of the KryptonPalette class.
        /// </summary>
        /// <param name="container">Container that owns the component.</param>
        public KryptonPalette(IContainer container)
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
                // Must unhook from the palette paint event
                if (_basePalette != null)
                {
                    _basePalette.PalettePaint -= OnPalettePaint;
                    _basePalette.ButtonSpecChanged -= OnButtonSpecChanged;
                    _basePalette.BasePaletteChanged -= OnBasePaletteChanged;
                    _basePalette.BaseRendererChanged -= OnBaseRendererChanged;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region AllowFormChrome
        /// <summary>
        /// Gets or sets a value indicating if KryptonForm instances should show custom chrome.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Should KryptonForm instances show custom chrome.")]
        [DefaultValue(typeof(InheritBool), "Inherit")]
        public InheritBool AllowFormChrome
        {
            get => _allowFormChrome;

            set
            {
                if (_allowFormChrome != value)
                {
                    _allowFormChrome = value;
                    OnAllowFormChromeChanged(this, EventArgs.Empty);
                }
            }
        }

        private bool ShouldSerializeAllowFormChrome() => AllowFormChrome != InheritBool.Inherit;

        #endregion

        #region ButtonSpecs
        /// <summary>
        /// Gets access to the button specifications.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining button specifications.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteButtonSpecs ButtonSpecs
        {
            get;
            set;
        }

        private bool ShouldSerializeButtonSpecs() => !ButtonSpecs.IsDefault;

        #endregion

        #region ButtonStyles
        /// <summary>
        /// Gets access to the appearance for button styles.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining appearance of button styles.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCheckButtons ButtonStyles
        {
            get;
            set;
        }

        private bool ShouldSerializeButtonStyles() => !ButtonStyles.IsDefault;

        #endregion

        #region CalendarDay
        /// <summary>
        /// Gets access to the appearance of the calendar day.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining appearance of the calendar day.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCalendarDay CalendarDay { get; set; }

        private bool ShouldSerializeCalendarDay() => !CalendarDay.IsDefault;

        #endregion

        #region Cargo
        /// <summary>
        /// Gets access to the set of user supplied values.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Set of user supplied values.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCargo Cargo { get; set; }

        private bool ShouldSerializeCargo() => !Cargo.IsDefault;

        #endregion

        #region Common
        /// <summary>
        /// Gets access to the common appearance values.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining common appearance values.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCommon Common { get; set; }

        private bool ShouldSerializeCommon() => !Common.IsDefault;

        #endregion

        #region ControlStyles
        /// <summary>
        /// Gets access to the appearance for control styles.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining appearance of control styles.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteControls ControlStyles { get; set; }

        private bool ShouldSerializeControlStyles() => !ControlStyles.IsDefault;

        #endregion

        #region ContextMenu
        /// <summary>
        /// Gets access to the appearance for context menus.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining appearance of context menus.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteContextMenu ContextMenu { get; set; }

        private bool ShouldSerializeContextMenu() => !ContextMenu.IsDefault;

        #endregion

        #region Cue Hint
        /// <summary>Gets or sets the cue hint text.</summary>
        /// <value>The cue hint text.</value>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Cue hint values.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCueHintText CueHintText { get; set; }

        //public bool ShouldSerializeCueHintText() => !CueHintText.IsDefault;

        #endregion

        #region DragDrop
        /// <summary>
        /// Gets access to the appearance of drag and drop.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining appearance of drag and drop.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDragDrop DragDrop { get; set; }

        private bool ShouldSerializeDragDrop() => !DragDrop.IsDefault;

        #endregion

        #region FormStyles
        /// <summary>
        /// Gets access to the appearance for form styles.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining appearance of form styles.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteForms FormStyles { get; set; }

        private bool ShouldSerializeFormStyles() => !FormStyles.IsDefault;

        #endregion

        #region HeaderGroup
        /// <summary>
        /// Gets access to the HeaderGroup appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining HeaderGroup appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteHeaderGroup HeaderGroup { get; set; }

        private bool ShouldSerializeHeaderGroup() => !HeaderGroup.IsDefault;

        #endregion

        #region HeaderStyles
        /// <summary>
        /// Gets access to the appearance for header styles.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining appearance of header styles.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteHeaders HeaderStyles { get; set; }

        private bool ShouldSerializeHeaders() => !HeaderStyles.IsDefault;

        #endregion

        #region GridStyles
        /// <summary>
        /// Gets access to the appearance for grid styles.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining appearance of grid styles.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteGrids GridStyles { get; set; }

        private bool ShouldSerializeGridStyles() => !GridStyles.IsDefault;

        #endregion

        #region Images
        /// <summary>
        /// Gets access to the images.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining images.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteImages Images { get; set; }

        private bool ShouldSerializeImages() => !Images.IsDefault;

        #endregion

        #region InputControls
        /// <summary>
        /// Gets access to the input controls styles.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining input controls.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteInputControls InputControlStyles { get; set; }

        private bool ShouldSerializeInputControlStyles() => !InputControlStyles.IsDefault;

        #endregion

        #region LabelStyles
        /// <summary>
        /// Gets access to the appearance for label styles.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining appearance of label styles.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteLabels LabelStyles { get; set; }

        private bool ShouldSerializeLabelStyles() => !LabelStyles.IsDefault;

        #endregion

        #region Navigator
        /// <summary>
        /// Gets access to the Navigator appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining Navigator appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteNavigator Navigator { get; set; }

        private bool ShouldSerializeNavigator() => !Navigator.IsDefault;

        #endregion

        #region PanelStyles
        /// <summary>
        /// Gets access to the appearance for panel styles.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining appearance of panel styles.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPalettePanels PanelStyles { get; set; }

        private bool ShouldSerializePanelStyles() => !PanelStyles.IsDefault;

        #endregion

        #region Ribbon
        /// <summary>
        /// Gets access to the appearance settings for ribbon.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining appearance of ribbon.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteRibbon Ribbon { get; set; }

        private bool ShouldSerializeRibbon() => !Ribbon.IsDefault;

        #endregion

        #region SeparatorStyles
        /// <summary>
        /// Gets access to the appearance for separator styles.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining appearance of separator styles.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteSeparators SeparatorStyles { get; set; }

        private bool ShouldSerializeSeparatorStyles() => !SeparatorStyles.IsDefault;

        #endregion

        #region TabStyles
        /// <summary>
        /// Gets access to the appearance for tab styles.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining appearance of tab styles.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteTabButtons TabStyles { get; set; }

        private bool ShouldSerializeTabStyles() => !TabStyles.IsDefault;

        #endregion

        #region TrackBar
        /// <summary>
        /// Gets access to the appearance for the track bar.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining appearance for the track bar.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteTrackBar TrackBar { get; set; }

        private bool ShouldSerializeTrackBar() => !TrackBar.IsDefault;

        #endregion

        #region ToolMenuStatus
        /// <summary>
        /// Gets access to the set of color table settings.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Colors associated with tool, menu and status strips.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteTMS ToolMenuStatus { get; set; }

        private bool ShouldSerializeToolMenuStatus() => !ToolMenuStatus.IsDefault;

        #endregion

        #region Renderer
        /// <summary>
        /// Gets the renderer to use for this palette.
        /// </summary>
        /// <returns>Renderer to use for drawing palette settings.</returns>
        public IRenderer GetRenderer()
        => _baseRenderMode switch
        {
            RendererMode.Inherit => _basePalette.GetRenderer(),
            RendererMode.Custom => _baseRenderer,
            _ => KryptonManager.GetRendererForMode(_baseRenderMode)
        };
        #endregion

        #region IPalette
        /// <summary>
        /// Gets a value indicating if KryptonForm instances should show custom chrome.
        /// </summary>
        /// <returns>InheritBool value.</returns>
        public InheritBool GetAllowFormChrome() => AllowFormChrome == InheritBool.Inherit ? _basePalette.GetAllowFormChrome() : AllowFormChrome;
        #endregion

        #region IPalette Back
        /// <summary>
        /// Gets a value indicating if background should be drawn.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public InheritBool GetBackDraw(PaletteBackStyle style, PaletteState state) =>
            // Find the correct destination in the palette and pass on request
            GetPaletteBack(style, state).GetBackDraw(state);

        /// <summary>
        /// Gets the graphics drawing hint for the background.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteGraphicsHint value.</returns>
        public PaletteGraphicsHint GetBackGraphicsHint(PaletteBackStyle style, PaletteState state) =>
            // Find the correct destination in the palette and pass on request
            GetPaletteBack(style, state).GetBackGraphicsHint(state);

        /// <summary>
        /// Gets the first background color.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetBackColor1(PaletteBackStyle style, PaletteState state) =>
            // Find the correct destination in the palette and pass on request
            GetPaletteBack(style, state).GetBackColor1(state);

        /// <summary>
        /// Gets the second back color.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetBackColor2(PaletteBackStyle style, PaletteState state) =>
            // Find the correct destination in the palette and pass on request
            GetPaletteBack(style, state).GetBackColor2(state);

        /// <summary>
        /// Gets the color background drawing style.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state) =>
            // Find the correct destination in the palette and pass on request
            GetPaletteBack(style, state).GetBackColorStyle(state);

        /// <summary>
        /// Gets the color alignment.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public PaletteRectangleAlign GetBackColorAlign(PaletteBackStyle style, PaletteState state)
        => GetPaletteBack(style, state).GetBackColorAlign(state);

        /// <summary>
        /// Gets the color background angle.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public float GetBackColorAngle(PaletteBackStyle style, PaletteState state) =>
            // Find the correct destination in the palette and pass on request
            GetPaletteBack(style, state).GetBackColorAngle(state);

        /// <summary>
        /// Gets a background image.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public Image GetBackImage(PaletteBackStyle style, PaletteState state)
        => GetPaletteBack(style, state).GetBackImage(state);

        /// <summary>
        /// Gets the background image style.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public PaletteImageStyle GetBackImageStyle(PaletteBackStyle style, PaletteState state)
        => GetPaletteBack(style, state).GetBackImageStyle(state);

        /// <summary>
        /// Gets the image alignment.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public PaletteRectangleAlign GetBackImageAlign(PaletteBackStyle style, PaletteState state)
        => GetPaletteBack(style, state).GetBackImageAlign(state);
        #endregion

        #region IPalette Border
        /// <summary>
        /// Gets a value indicating if border should be drawn.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="style">Border style.</param>
        /// <returns>InheritBool value.</returns>
        public InheritBool GetBorderDraw(PaletteBorderStyle style, PaletteState state)
        => GetPaletteBorder(style, state).GetBorderDraw(state);

        /// <summary>
        /// Gets a value indicating which borders to draw.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteDrawBorders value.</returns>
        public PaletteDrawBorders GetBorderDrawBorders(PaletteBorderStyle style, PaletteState state)
        => GetPaletteBorder(style, state).GetBorderDrawBorders(state);

        /// <summary>
        /// Gets the graphics drawing hint for the border.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteGraphicsHint value.</returns>
        public PaletteGraphicsHint GetBorderGraphicsHint(PaletteBorderStyle style, PaletteState state)
        => GetPaletteBorder(style, state).GetBorderGraphicsHint(state);

        /// <summary>
        /// Gets the first border color.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetBorderColor1(PaletteBorderStyle style, PaletteState state)
        => GetPaletteBorder(style, state).GetBorderColor1(state);

        /// <summary>
        /// Gets the second border color.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetBorderColor2(PaletteBorderStyle style, PaletteState state)
        => GetPaletteBorder(style, state).GetBorderColor2(state);

        /// <summary>
        /// Gets the color border drawing style.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public PaletteColorStyle GetBorderColorStyle(PaletteBorderStyle style, PaletteState state)
        => GetPaletteBorder(style, state).GetBorderColorStyle(state);

        /// <summary>
        /// Gets the color border alignment.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public PaletteRectangleAlign GetBorderColorAlign(PaletteBorderStyle style, PaletteState state)
        => GetPaletteBorder(style, state).GetBorderColorAlign(state);

        /// <summary>
        /// Gets the color border angle.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public float GetBorderColorAngle(PaletteBorderStyle style, PaletteState state)
        => GetPaletteBorder(style, state).GetBorderColorAngle(state);

        /// <summary>
        /// Gets the border width.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Integer width.</returns>
        public int GetBorderWidth(PaletteBorderStyle style, PaletteState state)
        => GetPaletteBorder(style, state).GetBorderWidth(state);

        /// <summary>
        /// Gets the border corner rounding.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Float rounding.</returns>
        public float GetBorderRounding(PaletteBorderStyle style, PaletteState state)
        => GetPaletteBorder(style, state).GetBorderRounding(state);

        /// <summary>
        /// Gets a border image.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public Image GetBorderImage(PaletteBorderStyle style, PaletteState state)
        => GetPaletteBorder(style, state).GetBorderImage(state);

        /// <summary>
        /// Gets the border image style.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public PaletteImageStyle GetBorderImageStyle(PaletteBorderStyle style, PaletteState state)
        => GetPaletteBorder(style, state).GetBorderImageStyle(state);

        /// <summary>
        /// Gets the image border alignment.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public PaletteRectangleAlign GetBorderImageAlign(PaletteBorderStyle style, PaletteState state)
        => GetPaletteBorder(style, state).GetBorderImageAlign(state);
        #endregion

        #region IPalette Content
        /// <summary>
        /// Gets a value indicating if content should be drawn.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public InheritBool GetContentDraw(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentDraw(state);

        /// <summary>
        /// Gets a value indicating if content should be drawn with focus indication.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public InheritBool GetContentDrawFocus(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentDrawFocus(state);

        /// <summary>
        /// Gets the horizontal relative alignment of the image.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public PaletteRelativeAlign GetContentImageH(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentImageH(state);

        /// <summary>
        /// Gets the vertical relative alignment of the image.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public PaletteRelativeAlign GetContentImageV(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentImageV(state);

        /// <summary>
        /// Gets the effect applied to drawing of the image.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteImageEffect value.</returns>
        public PaletteImageEffect GetContentImageEffect(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentImageEffect(state);

        /// <summary>
        /// Gets the image color to remap into another color.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetContentImageColorMap(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentImageColorMap(state);

        /// <summary>
        /// Gets the color to use in place of the image map color.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetContentImageColorTo(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentImageColorTo(state);

        /// <summary>
        /// Gets the font for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public Font GetContentShortTextFont(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentShortTextFont(state);

        /// <summary>
        /// Gets the font for the short text by generating a new font instance.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public Font GetContentShortTextNewFont(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentShortTextFont(state);

        /// <summary>
        /// Gets the rendering hint for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextHint value.</returns>
        public PaletteTextHint GetContentShortTextHint(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentShortTextHint(state);

        /// <summary>
        /// Gets the flag indicating if multiline text is allowed for short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public InheritBool GetContentShortTextMultiLine(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentShortTextMultiLine(state);

        /// <summary>
        /// Gets the text trimming to use for short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextTrim value.</returns>
        public PaletteTextTrim GetContentShortTextTrim(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentShortTextTrim(state);

        /// <summary>
        /// Gets the prefix drawing setting for short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentShortTextPrefix(state);

        /// <summary>
        /// Gets the horizontal relative alignment of the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public PaletteRelativeAlign GetContentShortTextH(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentShortTextH(state);

        /// <summary>
        /// Gets the vertical relative alignment of the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public PaletteRelativeAlign GetContentShortTextV(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentShortTextV(state);

        /// <summary>
        /// Gets the horizontal relative alignment of multiline short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentShortTextMultiLineH(state);

        /// <summary>
        /// Gets the first back color for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentShortTextColor1(state);

        /// <summary>
        /// Gets the second back color for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentShortTextColor2(state);

        /// <summary>
        /// Gets the color drawing style for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public PaletteColorStyle GetContentShortTextColorStyle(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentShortTextColorStyle(state);

        /// <summary>
        /// Gets the color alignment for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public PaletteRectangleAlign GetContentShortTextColorAlign(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentShortTextColorAlign(state);

        /// <summary>
        /// Gets the color background angle for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public float GetContentShortTextColorAngle(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentShortTextColorAngle(state);

        /// <summary>
        /// Gets a background image for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public Image GetContentShortTextImage(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentShortTextImage(state);

        /// <summary>
        /// Gets the background image style.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public PaletteImageStyle GetContentShortTextImageStyle(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentShortTextImageStyle(state);

        /// <summary>
        /// Gets the image alignment for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public PaletteRectangleAlign GetContentShortTextImageAlign(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentShortTextImageAlign(state);

        /// <summary>
        /// Gets the font for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public Font GetContentLongTextFont(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentLongTextFont(state);

        /// <summary>
        /// Gets the font for the long text by generating a new font instance.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public Font GetContentLongTextNewFont(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentLongTextFont(state);

        /// <summary>
        /// Gets the rendering hint for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>TextRenderingHint value.</returns>
        public PaletteTextHint GetContentLongTextHint(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentLongTextHint(state);

        /// <summary>
        /// Gets the prefix drawing setting for long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentLongTextPrefix(state);

        /// <summary>
        /// Gets the flag indicating if multiline text is allowed for long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public InheritBool GetContentLongTextMultiLine(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentLongTextMultiLine(state);

        /// <summary>
        /// Gets the text trimming to use for long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextTrim value.</returns>
        public PaletteTextTrim GetContentLongTextTrim(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentLongTextTrim(state);

        /// <summary>
        /// Gets the horizontal relative alignment of the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public PaletteRelativeAlign GetContentLongTextH(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentLongTextH(state);

        /// <summary>
        /// Gets the vertical relative alignment of the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public PaletteRelativeAlign GetContentLongTextV(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentLongTextV(state);

        /// <summary>
        /// Gets the horizontal relative alignment of multiline long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentLongTextMultiLineH(state);

        /// <summary>
        /// Gets the first back color for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentLongTextColor1(state);

        /// <summary>
        /// Gets the second back color for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetContentLongTextColor2(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentLongTextColor2(state);

        /// <summary>
        /// Gets the color drawing style for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public PaletteColorStyle GetContentLongTextColorStyle(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentLongTextColorStyle(state);

        /// <summary>
        /// Gets the color alignment for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public PaletteRectangleAlign GetContentLongTextColorAlign(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentLongTextColorAlign(state);

        /// <summary>
        /// Gets the color background angle for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public float GetContentLongTextColorAngle(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentLongTextColorAngle(state);

        /// <summary>
        /// Gets a background image for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public Image GetContentLongTextImage(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentLongTextImage(state);

        /// <summary>
        /// Gets the background image style for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public PaletteImageStyle GetContentLongTextImageStyle(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentLongTextImageStyle(state);

        /// <summary>
        /// Gets the image alignment for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public PaletteRectangleAlign GetContentLongTextImageAlign(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentLongTextImageAlign(state);

        /// <summary>
        /// Gets the padding between the border and content drawing.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Padding value.</returns>
        public Padding GetContentPadding(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentPadding(state);

        /// <summary>
        /// Gets the padding between adjacent content items.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Integer value.</returns>
        public int GetContentAdjacentGap(PaletteContentStyle style, PaletteState state)
        => GetPaletteContent(style, state).GetContentAdjacentGap(state);
        #endregion

        #region IPalette Metric
        /// <summary>
        /// Gets an integer metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Integer value.</returns>
        public int GetMetricInt(PaletteState state, PaletteMetricInt metric)
        {
            return metric switch
            {
                PaletteMetricInt.BarButtonEdgeInside or PaletteMetricInt.BarButtonEdgeOutside or PaletteMetricInt.CheckButtonGap or PaletteMetricInt.RibbonTabGap => Navigator.StateCommon.Bar.GetMetricInt(state, metric),
                PaletteMetricInt.HeaderButtonEdgeInsetPrimary => HeaderStyles.HeaderPrimary.StateCommon.GetMetricInt(state, metric),
                PaletteMetricInt.HeaderButtonEdgeInsetSecondary => HeaderStyles.HeaderSecondary.StateCommon.GetMetricInt(state, metric),
                PaletteMetricInt.HeaderButtonEdgeInsetDockInactive => HeaderStyles.HeaderDockInactive.StateCommon.GetMetricInt(state, metric),
                PaletteMetricInt.HeaderButtonEdgeInsetDockActive => HeaderStyles.HeaderDockActive.StateCommon.GetMetricInt(state, metric),
                PaletteMetricInt.HeaderButtonEdgeInsetForm => HeaderStyles.HeaderForm.StateCommon.GetMetricInt(state, metric),
                PaletteMetricInt.HeaderButtonEdgeInsetCustom1 => HeaderStyles.HeaderCustom1.StateCommon.GetMetricInt(state, metric),
                PaletteMetricInt.HeaderButtonEdgeInsetCustom2 => HeaderStyles.HeaderCustom2.StateCommon.GetMetricInt(state, metric),
                PaletteMetricInt.HeaderButtonEdgeInsetCustom3 => HeaderStyles.HeaderCustom3.StateCommon.GetMetricInt(state, metric),
                // Otherwise use base instance for the value instead
                _ => _redirector.GetMetricInt(state, metric)
            };
        }

        /// <summary>
        /// Gets a boolean metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>InheritBool value.</returns>
        public InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric)
        {
            if (metric == PaletteMetricBool.HeaderGroupOverlay)
            {
                return HeaderGroup.StateCommon.GetMetricBool(state, metric);
            }

            // Otherwise use base instance for the value instead
            return _redirector.GetMetricBool(state, metric);
        }

        /// <summary>
        /// Gets a padding metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Padding value.</returns>
        public Padding GetMetricPadding(PaletteState state, PaletteMetricPadding metric)
        {
            switch (metric)
            {
                case PaletteMetricPadding.BarPaddingTabs:
                case PaletteMetricPadding.BarPaddingInside:
                case PaletteMetricPadding.BarPaddingOutside:
                case PaletteMetricPadding.BarPaddingOnly:
                case PaletteMetricPadding.BarButtonPadding:
                    return Navigator.StateCommon.Bar.GetMetricPadding(state, metric);
                case PaletteMetricPadding.HeaderGroupPaddingPrimary:
                case PaletteMetricPadding.HeaderGroupPaddingSecondary:
                case PaletteMetricPadding.HeaderGroupPaddingDockInactive:
                case PaletteMetricPadding.HeaderGroupPaddingDockActive:
                    return HeaderGroup.StateCommon.GetMetricPadding(state, metric);
                case PaletteMetricPadding.HeaderButtonPaddingPrimary:
                    return HeaderStyles.HeaderPrimary.StateCommon.GetMetricPadding(state, metric);
                case PaletteMetricPadding.HeaderButtonPaddingSecondary:
                    return HeaderStyles.HeaderSecondary.StateCommon.GetMetricPadding(state, metric);
                case PaletteMetricPadding.HeaderButtonPaddingDockInactive:
                    return HeaderStyles.HeaderDockInactive.StateCommon.GetMetricPadding(state, metric);
                case PaletteMetricPadding.HeaderButtonPaddingDockActive:
                    return HeaderStyles.HeaderDockActive.StateCommon.GetMetricPadding(state, metric);
                case PaletteMetricPadding.HeaderButtonPaddingForm:
                    return HeaderStyles.HeaderForm.StateCommon.GetMetricPadding(state, metric);
                case PaletteMetricPadding.HeaderButtonPaddingCustom1:
                    return HeaderStyles.HeaderCustom1.StateCommon.GetMetricPadding(state, metric);
                case PaletteMetricPadding.HeaderButtonPaddingCustom2:
                    return HeaderStyles.HeaderCustom2.StateCommon.GetMetricPadding(state, metric);
                case PaletteMetricPadding.HeaderButtonPaddingCustom3:
                    return HeaderStyles.HeaderCustom3.StateCommon.GetMetricPadding(state, metric);
                case PaletteMetricPadding.SeparatorPaddingLowProfile:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SeparatorStyles.SeparatorLowProfile.StateDisabled.GetMetricPadding(state, metric);
                        case PaletteState.Normal:
                            return SeparatorStyles.SeparatorLowProfile.StateNormal.GetMetricPadding(state, metric);
                        case PaletteState.Tracking:
                            return SeparatorStyles.SeparatorLowProfile.StateTracking.GetMetricPadding(state, metric);
                        case PaletteState.Pressed:
                            return SeparatorStyles.SeparatorLowProfile.StatePressed.GetMetricPadding(state, metric);
                    }
                    break;
                case PaletteMetricPadding.SeparatorPaddingHighProfile:
                case PaletteMetricPadding.SeparatorPaddingHighInternalProfile:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SeparatorStyles.SeparatorHighProfile.StateDisabled.GetMetricPadding(state, metric);
                        case PaletteState.Normal:
                            return SeparatorStyles.SeparatorHighProfile.StateNormal.GetMetricPadding(state, metric);
                        case PaletteState.Tracking:
                            return SeparatorStyles.SeparatorHighProfile.StateTracking.GetMetricPadding(state, metric);
                        case PaletteState.Pressed:
                            return SeparatorStyles.SeparatorHighProfile.StatePressed.GetMetricPadding(state, metric);
                    }
                    break;
                case PaletteMetricPadding.SeparatorPaddingCustom1:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SeparatorStyles.SeparatorCustom1.StateDisabled.GetMetricPadding(state, metric);
                        case PaletteState.Normal:
                            return SeparatorStyles.SeparatorCustom1.StateNormal.GetMetricPadding(state, metric);
                        case PaletteState.Tracking:
                            return SeparatorStyles.SeparatorCustom1.StateTracking.GetMetricPadding(state, metric);
                        case PaletteState.Pressed:
                            return SeparatorStyles.SeparatorCustom1.StatePressed.GetMetricPadding(state, metric);
                    }
                    break;
                case PaletteMetricPadding.SeparatorPaddingCustom2:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SeparatorStyles.SeparatorCustom2.StateDisabled.GetMetricPadding(state, metric);
                        case PaletteState.Normal:
                            return SeparatorStyles.SeparatorCustom2.StateNormal.GetMetricPadding(state, metric);
                        case PaletteState.Tracking:
                            return SeparatorStyles.SeparatorCustom2.StateTracking.GetMetricPadding(state, metric);
                        case PaletteState.Pressed:
                            return SeparatorStyles.SeparatorCustom2.StatePressed.GetMetricPadding(state, metric);
                    }
                    break;
                case PaletteMetricPadding.SeparatorPaddingCustom3:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SeparatorStyles.SeparatorCustom3.StateDisabled.GetMetricPadding(state, metric);
                        case PaletteState.Normal:
                            return SeparatorStyles.SeparatorCustom3.StateNormal.GetMetricPadding(state, metric);
                        case PaletteState.Tracking:
                            return SeparatorStyles.SeparatorCustom3.StateTracking.GetMetricPadding(state, metric);
                        case PaletteState.Pressed:
                            return SeparatorStyles.SeparatorCustom3.StatePressed.GetMetricPadding(state, metric);
                    }
                    break;
            }

            // Otherwise use base instance for the value instead
            return _redirector.GetMetricPadding(state, metric);
        }

        #endregion

        #region IPalette Images
        /// <summary>
        /// Gets a tree view image appropriate for the provided state.
        /// </summary>
        /// <param name="expanded">Is the node expanded</param>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public Image GetTreeViewImage(bool expanded) =>
            // Not found, then inherit from target
            (expanded ? Images.TreeView.Minus : Images.TreeView.Plus) ?? _redirector.GetTreeViewImage(expanded);

        /// <summary>
        /// Gets a check box image appropriate for the provided state.
        /// </summary>
        /// <param name="enabled">Is the check box enabled.</param>
        /// <param name="checkState">Is the check box checked/unchecked/indeterminate.</param>
        /// <param name="tracking">Is the check box being hot tracked.</param>
        /// <param name="pressed">Is the check box being pressed.</param>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public Image GetCheckBoxImage(bool enabled, CheckState checkState, bool tracking, bool pressed)
        {
            Image retImage = null;

            // Get the state specific image
            switch (checkState)
            {
                case CheckState.Unchecked:
                    if (!enabled)
                    {
                        retImage = Images.CheckBox.UncheckedDisabled;
                    }
                    else if (pressed)
                    {
                        retImage = Images.CheckBox.UncheckedPressed;
                    }
                    else if (tracking)
                    {
                        retImage = Images.CheckBox.UncheckedTracking;
                    }
                    else
                    {
                        retImage = Images.CheckBox.UncheckedNormal;
                    }

                    break;
                case CheckState.Checked:
                    if (!enabled)
                    {
                        retImage = Images.CheckBox.CheckedDisabled;
                    }
                    else if (pressed)
                    {
                        retImage = Images.CheckBox.CheckedPressed;
                    }
                    else if (tracking)
                    {
                        retImage = Images.CheckBox.CheckedTracking;
                    }
                    else
                    {
                        retImage = Images.CheckBox.CheckedNormal;
                    }

                    break;
                case CheckState.Indeterminate:
                    if (!enabled)
                    {
                        retImage = Images.CheckBox.IndeterminateDisabled;
                    }
                    else if (pressed)
                    {
                        retImage = Images.CheckBox.IndeterminatePressed;
                    }
                    else if (tracking)
                    {
                        retImage = Images.CheckBox.IndeterminateTracking;
                    }
                    else
                    {
                        retImage = Images.CheckBox.IndeterminateNormal;
                    }

                    break;
            }

            // Use common image as the last resort
            retImage ??= Images.CheckBox.Common;

            // If nothing found then use the base palette
            return retImage ?? _redirector.GetCheckBoxImage(enabled, checkState, tracking, pressed);
        }

        /// <summary>
        /// Gets a check box image appropriate for the provided state.
        /// </summary>
        /// <param name="enabled">Is the radio button enabled.</param>
        /// <param name="checkState">Is the radio button checked.</param>
        /// <param name="tracking">Is the radio button being hot tracked.</param>
        /// <param name="pressed">Is the radio button being pressed.</param>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public Image GetRadioButtonImage(bool enabled, bool checkState, bool tracking, bool pressed)
        {
            Image retImage;

            // Get the state specific image
            if (!checkState)
            {
                if (!enabled)
                {
                    retImage = Images.RadioButton.UncheckedDisabled;
                }
                else if (pressed)
                {
                    retImage = Images.RadioButton.UncheckedPressed;
                }
                else if (tracking)
                {
                    retImage = Images.RadioButton.UncheckedTracking;
                }
                else
                {
                    retImage = Images.RadioButton.UncheckedNormal;
                }
            }
            else
            {
                if (!enabled)
                {
                    retImage = Images.RadioButton.CheckedDisabled;
                }
                else if (pressed)
                {
                    retImage = Images.RadioButton.CheckedPressed;
                }
                else if (tracking)
                {
                    retImage = Images.RadioButton.CheckedTracking;
                }
                else
                {
                    retImage = Images.RadioButton.CheckedNormal;
                }
            }

            // Use common image as the last resort
            retImage ??= Images.RadioButton.Common;

            // If nothing found then use the base palette
            return retImage ?? _redirector.GetRadioButtonImage(enabled, checkState, tracking, pressed);
        }

        /// <summary>
        /// Gets a drop down button image appropriate for the provided state.
        /// </summary>
        /// <param name="state">PaletteState for which image is required.</param>
        public Image GetDropDownButtonImage(PaletteState state)
        {
            // Grab state specific image
            Image retImage = state switch
            {
                PaletteState.Disabled => Images.DropDownButton.Disabled,
                PaletteState.Normal => Images.DropDownButton.Normal,
                PaletteState.Tracking => Images.DropDownButton.Tracking,
                PaletteState.Pressed => Images.DropDownButton.Pressed,
                _ => null
            };

            // Use common image as the last resort
            retImage ??= Images.DropDownButton.Common;

            // If nothing found then use the base palette
            return retImage ?? _redirector.GetDropDownButtonImage(state);
        }

        /// <summary>
        /// Gets a checked image appropriate for a context menu item.
        /// </summary>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public Image GetContextMenuCheckedImage()
        {
            Image retImage = Images.ContextMenu.Checked;

            // If nothing found then use the base palette
            return retImage ?? _redirector.GetContextMenuCheckedImage();
        }

        /// <summary>
        /// Gets a indeterminate image appropriate for a context menu item.
        /// </summary>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public Image GetContextMenuIndeterminateImage()
        {
            Image retImage = Images.ContextMenu.Indeterminate;

            // If nothing found then use the base palette
            return retImage ?? _redirector.GetContextMenuIndeterminateImage();
        }

        /// <summary>
        /// Gets an image indicating a sub-menu on a context menu item.
        /// </summary>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public Image GetContextMenuSubMenuImage()
        {
            Image retImage = Images.ContextMenu.SubMenu;

            // If nothing found then use the base palette
            return retImage ?? _redirector.GetContextMenuSubMenuImage();
        }

        /// <summary>
        /// Gets a check box image appropriate for the provided state.
        /// </summary>
        /// <param name="button">Enum of the button to fetch.</param>
        /// <param name="state">State of the button to fetch.</param>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public Image GetGalleryButtonImage(PaletteRibbonGalleryButton button, PaletteState state)
        {
            Image retImage = null;
            KryptonPaletteImagesGalleryButton images = button switch
            {
                PaletteRibbonGalleryButton.Up => Images.GalleryButtons.Up,
                PaletteRibbonGalleryButton.Down => Images.GalleryButtons.Down,
                _ => Images.GalleryButtons.DropDown //case PaletteRibbonGalleryButton.DropDown:
            };

            // Grab the state image from the compound object
            retImage = state switch
            {
                PaletteState.Disabled => images.Disabled,
                PaletteState.Normal => images.Normal,
                PaletteState.Tracking => images.Tracking,
                PaletteState.Pressed => images.Pressed,
                _ => retImage
            };

            // Use common image if the state specific image is not available
            retImage ??= images.Common;

            // If nothing found then use the base palette
            return retImage ?? _redirector.GetGalleryButtonImage(button, state);
        }
        #endregion

        #region IPalette ButtonSpec
        /// <summary>
        /// Gets the icon to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>Icon value.</returns>
        public Icon GetButtonSpecIcon(PaletteButtonSpecStyle style)
        => GetPaletteButtonSpec(style).GetButtonSpecIcon(style);

        /// <summary>
        /// Gets the image to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <param name="state">State for which image is required.</param>
        /// <returns>Image value.</returns>
        public Image GetButtonSpecImage(PaletteButtonSpecStyle style, PaletteState state)
        => GetPaletteButtonSpec(style).GetButtonSpecImage(style, state);

        /// <summary>
        /// Gets the image transparent color.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>Color value.</returns>
        public Color GetButtonSpecImageTransparentColor(PaletteButtonSpecStyle style)
        => GetPaletteButtonSpec(style).GetButtonSpecImageTransparentColor(style);

        /// <summary>
        /// Gets the short text to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>String value.</returns>
        public string GetButtonSpecShortText(PaletteButtonSpecStyle style)
        => GetPaletteButtonSpec(style).GetButtonSpecShortText(style);

        /// <summary>
        /// Gets the long text to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>String value.</returns>
        public string GetButtonSpecLongText(PaletteButtonSpecStyle style)
        => GetPaletteButtonSpec(style).GetButtonSpecLongText(style);

        /// <summary>
        /// Gets the tooltip title text to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>String value.</returns>
        public string GetButtonSpecToolTipTitle(PaletteButtonSpecStyle style)
        => GetPaletteButtonSpec(style).GetButtonSpecToolTipTitle(style);

        /// <summary>
        /// Gets the color to remap from the image to the container foreground.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>Color value.</returns>
        public Color GetButtonSpecColorMap(PaletteButtonSpecStyle style)
        => GetPaletteButtonSpec(style).GetButtonSpecColorMap(style);

        /// <summary>
        /// Gets the button style used for drawing the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>PaletteButtonStyle value.</returns>
        public PaletteButtonStyle GetButtonSpecStyle(PaletteButtonSpecStyle style)
        => GetPaletteButtonSpec(style).GetButtonSpecStyle(style);

        /// <summary>
        /// Get the location for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>HeaderLocation value.</returns>
        public HeaderLocation GetButtonSpecLocation(PaletteButtonSpecStyle style)
        => GetPaletteButtonSpec(style).GetButtonSpecLocation(style);

        /// <summary>
        /// Gets the edge to position the button against.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>PaletteRelativeEdgeAlign value.</returns>
        public PaletteRelativeEdgeAlign GetButtonSpecEdge(PaletteButtonSpecStyle style)
        => GetPaletteButtonSpec(style).GetButtonSpecEdge(style);

        /// <summary>
        /// Gets the button orientation.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>PaletteButtonOrientation value.</returns>
        public PaletteButtonOrientation GetButtonSpecOrientation(PaletteButtonSpecStyle style)
        => GetPaletteButtonSpec(style).GetButtonSpecOrientation(style);
        #endregion

        #region IPalette RibbonGeneral
        /// <summary>
        /// Gets the ribbon shape that should be used.
        /// </summary>
        /// <returns>Ribbon shape value.</returns>
        public PaletteRibbonShape GetRibbonShape()
        => GetPaletteRibbonGeneral().GetRibbonShape();

        /// <summary>
        /// Gets the text alignment for the ribbon context text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public virtual PaletteRelativeAlign GetRibbonContextTextAlign(PaletteState state) => GetPaletteRibbonGeneral().GetRibbonContextTextAlign(state);

        /// <summary>
        /// Gets the font for the ribbon context text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public Font GetRibbonContextTextFont(PaletteState state)
        => GetPaletteRibbonGeneral().GetRibbonContextTextFont(state);

        /// <summary>
        /// Gets the color for the ribbon context text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public Color GetRibbonContextTextColor(PaletteState state)
        => GetPaletteRibbonGeneral().GetRibbonContextTextColor(state);

        /// <summary>
        /// Gets the dark disabled color used for ribbon glyphs.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonDisabledDark(PaletteState state)
        => GetPaletteRibbonGeneral(state).GetRibbonDisabledDark(state);

        /// <summary>
        /// Gets the light disabled color used for ribbon glyphs.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonDisabledLight(PaletteState state)
        => GetPaletteRibbonGeneral(state).GetRibbonDisabledLight(state);

        /// <summary>
        /// Gets the color for the drop arrow light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonDropArrowLight(PaletteState state)
        => GetPaletteRibbonGeneral(state).GetRibbonDropArrowLight(state);

        /// <summary>
        /// Gets the color for the drop arrow dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonDropArrowDark(PaletteState state)
        => GetPaletteRibbonGeneral(state).GetRibbonDropArrowDark(state);

        /// <summary>
        /// Gets the color for the dialog launcher dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonGroupDialogDark(PaletteState state)
        => GetPaletteRibbonGeneral(state).GetRibbonGroupDialogDark(state);

        /// <summary>
        /// Gets the color for the dialog launcher light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonGroupDialogLight(PaletteState state)
        => GetPaletteRibbonGeneral(state).GetRibbonGroupDialogLight(state);

        /// <summary>
        /// Gets the color for the group separator dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonGroupSeparatorDark(PaletteState state)
        => GetPaletteRibbonGeneral(state).GetRibbonGroupSeparatorDark(state);

        /// <summary>
        /// Gets the color for the group separator light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonGroupSeparatorLight(PaletteState state)
        => GetPaletteRibbonGeneral(state).GetRibbonGroupSeparatorLight(state);

        /// <summary>
        /// Gets the color for the minimize bar dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonMinimizeBarDark(PaletteState state)
        => GetPaletteRibbonGeneral(state).GetRibbonMinimizeBarDark(state);

        /// <summary>
        /// Gets the color for the minimize bar light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonMinimizeBarLight(PaletteState state)
        => GetPaletteRibbonGeneral(state).GetRibbonMinimizeBarLight(state);

        /// <summary>
        /// Gets the font for the ribbon text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public Font GetRibbonTextFont(PaletteState state)
        => GetPaletteRibbonGeneral(state).GetRibbonTextFont(state);

        /// <summary>
        /// Gets the rendering hint for the ribbon font.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextHint value.</returns>
        public PaletteTextHint GetRibbonTextHint(PaletteState state)
        => GetPaletteRibbonGeneral(state).GetRibbonTextHint(state);

        /// <summary>
        /// Gets the color for the tab separator.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonTabSeparatorColor(PaletteState state)
        => GetPaletteRibbonGeneral(state).GetRibbonTabSeparatorColor(state);

        /// <summary>
        /// Gets the color for the tab context separators.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public virtual Color GetRibbonTabSeparatorContextColor(PaletteState state) => GetPaletteRibbonGeneral(state).GetRibbonTabSeparatorContextColor(state);

        /// <summary>
        /// Gets the color for the extra QAT button dark content color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonQATButtonDark(PaletteState state)
        => GetPaletteRibbonGeneral(state).GetRibbonQATButtonDark(state);

        /// <summary>
        /// Gets the color for the extra QAT button light content color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonQATButtonLight(PaletteState state)
        => GetPaletteRibbonGeneral(state).GetRibbonQATButtonLight(state);
        #endregion

        #region IPalette RibbonBack
        /// <summary>
        /// Gets the method used to draw the background of a ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteRibbonBackStyle value.</returns>
        public PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteRibbonBackStyle style, PaletteState state)
        => GetPaletteRibbonBack(style, state).GetRibbonBackColorStyle(state);

        /// <summary>
        /// Gets the first background color for the ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor1(PaletteRibbonBackStyle style, PaletteState state)
        => GetPaletteRibbonBack(style, state).GetRibbonBackColor1(state);

        /// <summary>
        /// Gets the second background color for the ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor2(PaletteRibbonBackStyle style, PaletteState state)
        => GetPaletteRibbonBack(style, state).GetRibbonBackColor2(state);

        /// <summary>
        /// Gets the third background color for the ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor3(PaletteRibbonBackStyle style, PaletteState state)
        => GetPaletteRibbonBack(style, state).GetRibbonBackColor3(state);

        /// <summary>
        /// Gets the fourth background color for the ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor4(PaletteRibbonBackStyle style, PaletteState state)
        => GetPaletteRibbonBack(style, state).GetRibbonBackColor4(state);

        /// <summary>
        /// Gets the fifth background color for the ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor5(PaletteRibbonBackStyle style, PaletteState state)
        => GetPaletteRibbonBack(style, state).GetRibbonBackColor5(state);
        #endregion

        #region IPalette RibbonText
        /// <summary>
        /// Gets the tab color for the item text.
        /// </summary>
        /// <param name="style">Text style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonTextColor(PaletteRibbonTextStyle style, PaletteState state)
        => GetPaletteRibbonText(style, state).GetRibbonTextColor(state);
        #endregion

        #region IPalette ElementColor
        /// <summary>
        /// Gets the first element color.
        /// </summary>
        /// <param name="element">Element for which color is required.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetElementColor1(PaletteElement element, PaletteState state)
        => GetTrackBar(element, state).GetElementColor1(state);

        /// <summary>
        /// Gets the second element color.
        /// </summary>
        /// <param name="element">Element for which color is required.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetElementColor2(PaletteElement element, PaletteState state)
        => GetTrackBar(element, state).GetElementColor2(state);

        /// <summary>
        /// Gets the third element color.
        /// </summary>
        /// <param name="element">Element for which color is required.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetElementColor3(PaletteElement element, PaletteState state)
        => GetTrackBar(element, state).GetElementColor3(state);

        /// <summary>
        /// Gets the fourth element color.
        /// </summary>
        /// <param name="element">Element for which color is required.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetElementColor4(PaletteElement element, PaletteState state)
        => GetTrackBar(element, state).GetElementColor4(state);

        /// <summary>
        /// Gets the fifth element color.
        /// </summary>
        /// <param name="element">Element for which color is required.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetElementColor5(PaletteElement element, PaletteState state)
        => GetTrackBar(element, state).GetElementColor5(state);
        #endregion

        #region IPalette DragDrop
        /// <summary>
        /// Gets the feedback drawing method used.
        /// </summary>
        /// <returns>Feedback enumeration value.</returns>
        public PaletteDragFeedback GetDragDropFeedback()
        => DragDrop.GetDragDropFeedback();

        /// <summary>
        /// Gets the background color for a solid drag drop area.
        /// </summary>
        /// <returns>Color value.</returns>
        public Color GetDragDropSolidBack()
        => DragDrop.GetDragDropSolidBack();

        /// <summary>
        /// Gets the border color for a solid drag drop area.
        /// </summary>
        /// <returns>Color value.</returns>
        public Color GetDragDropSolidBorder()
        => DragDrop.GetDragDropSolidBorder();

        /// <summary>
        /// Gets the opacity of the solid area.
        /// </summary>
        /// <returns>Opacity ranging from 0 to 1.</returns>
        public float GetDragDropSolidOpacity()
        => DragDrop.GetDragDropSolidOpacity();

        /// <summary>
        /// Gets the background color for the docking indicators area.
        /// </summary>
        /// <returns>Color value.</returns>
        public Color GetDragDropDockBack()
        => DragDrop.GetDragDropDockBack();

        /// <summary>
        /// Gets the border color for the docking indicators area.
        /// </summary>
        /// <returns>Color value.</returns>
        public Color GetDragDropDockBorder()
        => DragDrop.GetDragDropDockBorder();

        /// <summary>
        /// Gets the active color for docking indicators.
        /// </summary>
        /// <returns>Color value.</returns>
        public Color GetDragDropDockActive()
        => DragDrop.GetDragDropDockActive();

        /// <summary>
        /// Gets the inactive color for docking indicators.
        /// </summary>
        /// <returns>Color value.</returns>
        public Color GetDragDropDockInactive()
        => DragDrop.GetDragDropDockInactive();
        #endregion

        #region Public Methods
        /// <summary>
        /// Suspend the notification of drawing updates when palette values are changed.
        /// </summary>
        /// <returns>New suspended count.</returns>
        public int SuspendUpdates()
        => ++_suspendCount;

        /// <summary>
        /// Resume the notification of drawing updates when palette values are changed.
        /// </summary>
        /// <returns>New suspended count; Updates only occur when the count reaches zero.</returns>
        public int ResumeUpdates()
        => ResumeUpdates(true);

        /// <summary>
        /// Resume the notification of drawing updates when palette values are changed.
        /// </summary>
        /// <param name="updateNow">Should an immediate drawing update occur.</param>
        /// <returns>New suspended count; Updates only occur when the count reaches zero.</returns>
        public int ResumeUpdates(bool updateNow)
        {
            // Never allow a negative count
            if (_suspendCount > 0)
            {
                _suspendCount--;

                // If an immediate drawing update is required and updates have just been enabled
                if (updateNow && _suspendCount == 0)
                {
                    OnPalettePaint(this, new PaletteLayoutEventArgs(true, true));
                    OnButtonSpecChanged(this, EventArgs.Empty);
                    OnNeedPaint(this, new NeedLayoutEventArgs(true));
                }
            }

            return _suspendCount;
        }

        /// <summary>
        /// Reset all palettes values back to defaults.
        /// </summary>
        /// <param name="silent">Silent mode provides no user interface feedback.</param>
        public void ResetToDefaults(bool silent)
        {
            try
            {
                // Prevent lots of redraw events until all reset changes are completed
                SuspendUpdates();

                if (silent)
                {
                    ResetOperation(null);
                }
                else
                {
                    // Perform the reset operation on a separate worker thread
                    CommonHelper.PerformOperation(ResetOperation, null);

                    KryptonMessageBox.Show("Reset of palette is completed.",
                                    "Palette Reset",
                                    MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                if (!silent)
                {
                    KryptonMessageBox.Show("Reset failed.\n\n Error:" + ex.Message,
                                    "Palette Reset",
                                    MessageBoxButtons.OK,
                                    KryptonMessageBoxIcon.ERROR);
                }
            }
            finally
            {
                // Must match the SuspendUpdates even if exception occurs
                ResumeUpdates();
            }
        }

        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="silent">Silent mode provides no user interface feedback.</param>
        public void PopulateFromBase(bool silent)
        {
            try
            {
                // Prevent lots of redraw events until all reset changes are completed
                SuspendUpdates();

                if (silent)
                {
                    PopulateFromBaseOperation(null);
                }
                else
                {
                    // Perform the reset operation on a separate worker thread
                    CommonHelper.PerformOperation(PopulateFromBaseOperation, null);

                    KryptonMessageBox.Show("Relevant values have been populated.",
                                    "Populate Values",
                                    MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                if (!silent)
                {
                    KryptonMessageBox.Show("Reset failed.\n\n Error:" + ex.Message,
                                    "Populate Values",
                                    MessageBoxButtons.OK,
                                    KryptonMessageBoxIcon.ERROR);
                }
            }
            finally
            {
                // Must match the SuspendUpdates even if exception occurs
                ResumeUpdates();
            }
        }

        /// <summary>
        /// Import palette settings from an xml file.
        /// </summary>
        /// <returns>Fullpath of imported filename; otherwise empty string.</returns>
        public string Import()
        {
            using OpenFileDialog dialog = new();
            // Palette files are just XML documents
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.DefaultExt = @"xml";
            dialog.Filter = @"Palette files (*.xml)|*.xml|All files (*.*)|(*.*)";
            dialog.Title = @"Load Palette";

            // Get the actual file selected by the user
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // Use the existing import overload that takes the target name
                return Import(dialog.FileName, false);
            }

            if (!string.IsNullOrWhiteSpace(dialog.FileName))
            {
                // Set the file path
                SetCustomisedKryptonPaletteFilePath(Path.GetFullPath(dialog.FileName));

                // Set the palette name
                SetPaletteName(Path.GetFileName(dialog.FileName));
            }

            return string.Empty;
        }

        /// <summary>
        /// Import palette settings from the specified xml file.
        /// </summary>
        /// <param name="filename">Filename to load.</param>
        /// <returns>Fullpath of imported filename; otherwise empty string.</returns>
        public string Import(string filename) => Import(filename, true);

        /// <summary>
        /// Import palette settings from the specified xml file.
        /// </summary>
        /// <param name="filename">Filename to load.</param>
        /// <param name="silent">Silent mode provides no user interface feedback.</param>
        /// <returns>Fullpath of imported filename; otherwise empty string.</returns>
        public string Import(string filename, bool silent)
        {
            string ret;

            try
            {
                // Prevent lots of redraw events until all loading completes
                SuspendUpdates();

                if (silent)
                {
                    ret = (string)ImportFromFile(filename);
                }
                else
                {
                    // Perform the import operation on a separate worker thread
                    ret = (string)CommonHelper.PerformOperation(ImportFromFile, filename);

                    KryptonMessageBox.Show($"Import from file '{filename}' completed.",
                                    @"Palette Import",
                                    MessageBoxButtons.OK, KryptonMessageBoxIcon.INFORMATION);
                }
            }
            catch (Exception ex)
            {
                if (!silent)
                {
                    KryptonMessageBox.Show($"Import from file '{filename}' failed.\n\n Error:{ex.Message}",
                                    @"Palette Import",
                                    MessageBoxButtons.OK,
                                    KryptonMessageBoxIcon.ERROR);
                }

                // Rethrow the exception
                throw;
            }
            finally
            {
                // Must match the SuspendUpdates even if exception occurs
                ResumeUpdates();
            }

            return ret;
        }

        /// <summary>
        /// Import palette settings from the specified stream.
        /// </summary>
        /// <param name="stream">Stream that contains an XmlDocument.</param>
        public void Import(Stream stream) =>
            // By default the import is silent
            Import(stream, true);

        /// <summary>
        /// Import palette settings from the specified stream.
        /// </summary>
        /// <param name="stream">Stream that contains an XmlDocument.</param>
        /// <param name="silent">Silent mode provides no user interface feedback.</param>
        public void Import(Stream stream, bool silent)
        {
            try
            {
                // Prevent lots of redraw events until all loading completes
                SuspendUpdates();

                if (silent)
                {
                    ImportFromStream(stream);
                }
                else
                {
                    // Perform the import operation on a separate worker thread
                    CommonHelper.PerformOperation(ImportFromStream, stream);

                    KryptonMessageBox.Show(@"Import completed with success.",
                                    @"Palette Import",
                                    MessageBoxButtons.OK, KryptonMessageBoxIcon.INFORMATION);
                }
            }
            catch (Exception ex)
            {
                if (!silent)
                {
                    KryptonMessageBox.Show(@"Import has failed.\n\n Error:" + ex.Message,
                                    @"Palette Import",
                                    MessageBoxButtons.OK,
                                    KryptonMessageBoxIcon.ERROR);
                }

                // Rethrow the exception
                throw;
            }
            finally
            {
                // Must match the SuspendUpdates even if exception occurs
                ResumeUpdates();
            }
        }

        /// <summary>
        /// Import palette settings from the specified array of bytes.
        /// </summary>
        /// <param name="byteArray">ByteArray that was returning from exporting palette.</param>
        public void Import(byte[] byteArray) =>
            // By default the import is silent
            Import(byteArray, true);

        /// <summary>
        /// Import palette settings from the specified array of bytes.
        /// </summary>
        /// <param name="byteArray">ByteArray that was returning from exporting palette.</param>
        /// <param name="silent">Silent mode provides no user interface feedback.</param>
        public void Import(byte[] byteArray, bool silent)
        {
            try
            {
                // Prevent lots of redraw events until all loading completes
                SuspendUpdates();

                if (silent)
                {
                    ImportFromByteArray(byteArray);
                }
                else
                {
                    // Perform the import operation on a separate worker thread
                    CommonHelper.PerformOperation(ImportFromByteArray, byteArray);

                    KryptonMessageBox.Show(@"Import completed with success.",
                                    @"Palette Import",
                                    MessageBoxButtons.OK, KryptonMessageBoxIcon.INFORMATION);
                }
            }
            catch (Exception ex)
            {
                if (!silent)
                {
                    KryptonMessageBox.Show(@"Import has failed.\n\n Error:" + ex.Message,
                                    @"Palette Import",
                                    MessageBoxButtons.OK,
                                    KryptonMessageBoxIcon.ERROR);
                }

                // Rethrow the exception
                throw;
            }
            finally
            {
                // Must match the SuspendUpdates even if exception occurs
                ResumeUpdates();
            }
        }

        /// <summary>
        /// Export palette settings to a user specified xml file.
        /// </summary>
        /// <returns>Fullpath of exported filename; otherwise empty string.</returns>
        public string Export()
        {
            using SaveFileDialog dialog = new();
            // Palette files are just xml documents
            dialog.OverwritePrompt = true;
            dialog.DefaultExt = @"xml";
            dialog.Filter = @"Palette files (*.xml)|*.xml|All files (*.*)|(*.*)";
            dialog.Title = @"Save Palette As";

            // Get the actual file selected by the user
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SetCustomisedKryptonPaletteFilePath(Path.GetFullPath(dialog.FileName));

                // Use the existing export overload that takes the target name
                return Export(dialog.FileName, true, false);
            }

            return string.Empty;
        }

        /// <summary>
        /// Export palette settings to the specified xml file.
        /// </summary>
        /// <param name="filename">Filename to create or overwrite.</param>
        /// <param name="ignoreDefaults">Should default values be exported.</param>
        /// <returns>Fullpath of exported filename; otherwise empty string.</returns>
        public string Export(string filename, bool ignoreDefaults)
        => Export(filename, ignoreDefaults, true);

        /// <summary>
        /// Export palette settings to the specified xml file.
        /// </summary>
        /// <param name="filename">Filename to create or overwrite.</param>
        /// <param name="ignoreDefaults">Should default values be exported.</param>
        /// <param name="silent">Silent mode provides no user interface feedback.</param>
        /// <returns>Fullpath of exported filename; otherwise empty string.</returns>
        public string Export(string filename, bool ignoreDefaults, bool silent)
        {
            string ret;

            try
            {
                // Prevent lots of redraw events until all saving completes
                SuspendUpdates();

                if (silent)
                {
                    ret = (string)ExportToFile(new object[] { filename, ignoreDefaults });
                }
                else
                {
                    // Perform the import operation on a separate worker thread
                    ret = (string)CommonHelper.PerformOperation(ExportToFile,
                                                                new object[] { filename, ignoreDefaults });

                    KryptonMessageBox.Show($"Export to file '{filename}' completed.",
                                    @"Palette Export",
                                    MessageBoxButtons.OK, KryptonMessageBoxIcon.INFORMATION);
                }
            }
            catch (Exception ex)
            {
                if (!silent)
                {
                    KryptonMessageBox.Show($"Export to file '{filename}' failed.\n\n Error:{ex.Message}",
                                    @"Palette Export",
                                    MessageBoxButtons.OK,
                                    KryptonMessageBoxIcon.ERROR);
                }

                // Rethrow the exception
                throw;
            }
            finally
            {
                // Must match the SuspendUpdates even if exception occurs
                ResumeUpdates();
            }

            return ret;
        }

        /// <summary>
        /// Export palette settings into a stream object.
        /// </summary>
        /// <param name="stream">Destination stream for exporting.</param>
        /// <param name="ignoreDefaults">Should default values be exported.</param>
        public void Export(Stream stream, bool ignoreDefaults) =>
            // By default the export is silent
            Export(stream, ignoreDefaults, true);

        /// <summary>
        /// Export palette settings into a stream object.
        /// </summary>
        /// <param name="stream">Destination stream for exporting.</param>
        /// <param name="ignoreDefaults">Should default values be exported.</param>
        /// <param name="silent">Silent mode provides no user interface feedback.</param>
        public void Export(Stream stream, bool ignoreDefaults, bool silent)
        {
            try
            {
                // Prevent lots of redraw events until all saving completes
                SuspendUpdates();

                if (silent)
                {
                    ExportToStream(new object[] { stream, ignoreDefaults });
                }
                else
                {
                    // Perform the import operation on a separate worker thread
                    CommonHelper.PerformOperation(ExportToStream,
                                                  new object[] { stream, ignoreDefaults });

                    KryptonMessageBox.Show(@"Export completed with success.",
                                    @"Palette Export",
                                    MessageBoxButtons.OK, KryptonMessageBoxIcon.INFORMATION);
                }
            }
            catch (Exception ex)
            {
                if (!silent)
                {
                    KryptonMessageBox.Show(@"Export has failed.\n\n Error:" + ex.Message,
                                    @"Palette Export",
                                    MessageBoxButtons.OK,
                                    KryptonMessageBoxIcon.ERROR);
                }

                // Rethrow the exception
                throw;
            }
            finally
            {
                // Must match the SuspendUpdates even if exception occurs
                ResumeUpdates();
            }
        }

        /// <summary>
        /// Export palette settings into an array of bytes.
        /// </summary>
        /// <param name="ignoreDefaults">Should default values be exported.</param>
        public byte[] Export(bool ignoreDefaults)
        => Export(ignoreDefaults, true);

        /// <summary>
        /// Export palette settings into an array of bytes.
        /// </summary>
        /// <param name="ignoreDefaults">Should default values be exported.</param>
        /// <param name="silent">Silent mode provides no user interface feedback.</param>
        public byte[] Export(bool ignoreDefaults, bool silent)
        {
            byte[] ret;

            try
            {
                // Prevent lots of redraw events until all saving completes
                SuspendUpdates();

                if (silent)
                {
                    ret = (byte[])ExportToByteArray(new object[] { ignoreDefaults });
                }
                else
                {
                    // Perform the import operation on a separate worker thread
                    ret = (byte[])CommonHelper.PerformOperation(ExportToByteArray,
                                                                new object[] { ignoreDefaults });

                    KryptonMessageBox.Show(@"Export completed with success.",
                                    @"Palette Export",
                                    MessageBoxButtons.OK, KryptonMessageBoxIcon.INFORMATION);
                }
            }
            catch (Exception ex)
            {
                if (!silent)
                {
                    KryptonMessageBox.Show(@"Export has failed.\n\n Error:" + ex.Message,
                                    @"Palette Export",
                                    MessageBoxButtons.OK,
                                    KryptonMessageBoxIcon.ERROR);
                }

                // Rethrow the exception
                throw;
            }
            finally
            {
                // Must match the SuspendUpdates even if exception occurs
                ResumeUpdates();
            }

            return ret;
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Should any of these global values be serialised
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDefault => !(ShouldSerializeCustomisedKryptonPaletteFilePath()
            || ShouldSerializePaletteName()
            || ShouldSerializeBasePaletteMode()
            || ShouldSerializeBasePalette()
            || ShouldSerializeBaseRendererMode()
            || ShouldSerializeBaseRenderer()
            );

        /// <summary>
        /// Reset global values to default.
        /// </summary>
        public void Reset()
        {
            ResetCustomisedKryptonPaletteFilePath();
            ResetPaletteName();
            ResetBasePaletteMode();
            ResetBasePalette();
            ResetBaseRendererMode();
            ResetBaseRenderer();
        }


        /// <summary>Gets the customised Krypton palette file path.</summary>
        [KryptonPersist(false, false)]
        [Category(@"Miscellaneous")]
        [Description(@"Gets the customised Krypton palette file path.")]
        [DefaultValue("")]
        [Browsable(false)]
        public string CustomisedKryptonPaletteFilePath { get; private set; }

        private bool ShouldSerializeCustomisedKryptonPaletteFilePath() => !string.IsNullOrWhiteSpace(CustomisedKryptonPaletteFilePath);
        private void ResetCustomisedKryptonPaletteFilePath() => CustomisedKryptonPaletteFilePath = string.Empty;

        /// <summary>Gets the palette name.</summary>
        [KryptonPersist(false, false),
         Category(@"Miscellaneous"),
         Description(@"Gets the palette name."),
         DefaultValue("")]
        public string PaletteName { get; private set; }

        private bool ShouldSerializePaletteName() => !string.IsNullOrWhiteSpace(PaletteName);
        private void ResetPaletteName() => PaletteName = string.Empty;

        /// <summary>
        /// Gets or sets the base palette used to inherit from.
        /// </summary>
        [KryptonPersist(false, false)]
        [Category(@"Visuals")]
        [Description(@"Base palette used to inherit from.")]
        [DefaultValue(typeof(PaletteMode), "Office365Blue")]
        public PaletteMode BasePaletteMode
        {
            get => _basePaletteMode;

            set
            {
                if (_basePaletteMode != value)
                {
                    // Action depends on new value
                    switch (value)
                    {
                        case PaletteMode.Custom:
                            // Do nothing, you must assign a palette to the 
                            // 'BasePalette' property in order to get the custom mode
                            break;
                        default:
                            // Cache the original values
                            PaletteMode tempMode = _basePaletteMode;
                            IPalette tempPalette = _basePalette;

                            // Use the new value
                            _basePaletteMode = value;
                            _basePalette = KryptonManager.GetPaletteForMode(_basePaletteMode);

                            // If the new value creates a circular reference
                            if (HasCircularReference())
                            {
                                // Restore the original values
                                _basePaletteMode = tempMode;
                                _basePalette = tempPalette;

                                throw new ArgumentOutOfRangeException(nameof(value), @"Cannot use palette that would create a circular reference");
                            }
                            else
                            {
                                // Restore the original base palette as 'SetPalette' will not 
                                // work correctly unless it still has the old value in place
                                _basePalette = tempPalette;
                            }

                            // Get a reference to the standard palette from its name
                            SetPalette(KryptonManager.GetPaletteForMode(_basePaletteMode));

                            // Fire events to indicate a change in palette values
                            OnBasePaletteChanged(this, EventArgs.Empty);
                            OnBaseRendererChanged(this, EventArgs.Empty);
                            OnAllowFormChromeChanged(this, EventArgs.Empty);
                            OnButtonSpecChanged(this, EventArgs.Empty);
                            OnPalettePaint(this, new PaletteLayoutEventArgs(true, true));
                            break;
                    }
                }
            }
        }

        private bool ShouldSerializeBasePaletteMode() => BasePaletteMode != PaletteMode.Office365Blue;

        private void ResetBasePaletteMode() => BasePaletteMode = PaletteMode.Office365Blue;

        /// <summary>
        /// Gets and sets the KryptonPalette used to inherit from.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"KryptonPalette used to inherit from.")]
        [DefaultValue(null)]
        public IPalette BasePalette
        {
            get => _basePalette;

            set
            {
                // Only interested in changes of value
                if (_basePalette != value)
                {
                    // Store the original values
                    PaletteMode tempMode = _basePaletteMode;
                    IPalette tempPalette = _basePalette;

                    // Find the new palette mode based on the incoming value
                    _basePaletteMode = value == null ? PaletteMode.Office365Blue : PaletteMode.Custom;
                    _basePalette = value;

                    // If the new value creates a circular reference
                    if (HasCircularReference())
                    {
                        // Put back the original palette details
                        _basePaletteMode = tempMode;
                        _basePalette = tempPalette;

                        throw new ArgumentOutOfRangeException(nameof(value), @"Cannot use palette that would create a circular reference");
                    }
                    else
                    {
                        // Restore the original base palette as 'SetPalette' will not 
                        // work correctly unless it still has the old value in place
                        _basePalette = tempPalette;
                    }

                    // Use the provided palette value
                    SetPalette(value);

                    // If no custom palette is required
                    if (value == null)
                    {
                        // Get the appropriate palette for the global mode
                        SetPalette(KryptonManager.GetPaletteForMode(_basePaletteMode));
                    }

                    // Indicate the palette values have changed
                    OnBasePaletteChanged(this, EventArgs.Empty);
                    OnBaseRendererChanged(this, EventArgs.Empty);
                    OnAllowFormChromeChanged(this, EventArgs.Empty);
                    OnButtonSpecChanged(this, EventArgs.Empty);
                    OnPalettePaint(this, new PaletteLayoutEventArgs(true, true));
                }
            }
        }

        private bool ShouldSerializeBasePalette() => BasePalette != null;
        private void ResetBasePalette() => BasePalette = null;

        /// <summary>
        /// Gets or sets the renderer used for drawing the palette.
        /// </summary>
        [KryptonPersist(false, false)]
        [Category(@"Visuals")]
        [Description(@"Renderer used to inherit from.")]
        [DefaultValue(typeof(RendererMode), "Inherit")]
        public RendererMode BaseRenderMode
        {
            get => _baseRenderMode;

            set
            {
                if (_baseRenderMode != value)
                {
                    // Action depends on new value
                    switch (value)
                    {
                        case RendererMode.Custom:
                            // Do nothing, you must assign a palette to the 
                            // 'Palette' property in order to get the custom mode
                            break;
                        default:
                            // Use the new value
                            _baseRenderMode = value;

                            // If inheriting then we do not need a base renderer
                            if (value == RendererMode.Inherit)
                            {
                                _baseRenderer = null;
                            }
                            else
                            {
                                _baseRenderer = KryptonManager.GetRendererForMode(_baseRenderMode);
                            }

                            // Fire events to indicate a change in palette values
                            // (because renderer has changed the palette need redrawing)
                            OnBaseRendererChanged(this, EventArgs.Empty);
                            OnButtonSpecChanged(this, EventArgs.Empty);
                            OnPalettePaint(this, new PaletteLayoutEventArgs(true, true));
                            break;
                    }
                }
            }
        }

        private bool ShouldSerializeBaseRendererMode() => BaseRenderMode != RendererMode.Inherit;
        private void ResetBaseRendererMode() => BaseRenderMode = RendererMode.Inherit;

        /// <summary>
        /// Gets and sets the custom renderer to be used with this palette.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Custom renderer to be used with this palette.")]
        [DefaultValue(null)]
        public IRenderer BaseRenderer
        {
            get => _baseRenderer;

            set
            {
                // Only interested in changes of value
                if (_baseRenderer != value)
                {
                    // Find the new renderer mode based on the incoming value
                    _baseRenderMode = value == null ? RendererMode.Inherit : RendererMode.Custom;
                    _baseRenderer = value;

                    // Fire events to indicate a change in palette values
                    // (because renderer has changed the palette need redrawing)
                    OnBaseRendererChanged(this, EventArgs.Empty);
                    OnButtonSpecChanged(this, EventArgs.Empty);
                    OnPalettePaint(this, new PaletteLayoutEventArgs(true, true));
                }
            }
        }

        private bool ShouldSerializeBaseRenderer() => BaseRenderer != null;
        private void ResetBaseRenderer() => BaseRenderer = null;

        /// <summary>
        /// Gets access to the color table instance.
        /// </summary>
        [Browsable(false)]
        public KryptonColorTable ColorTable => ToolMenuStatus.InternalKCT;

        #endregion

        #region Protected
        /// <summary>
        /// Gets access to the need paint delegate.
        /// </summary>
        protected NeedPaintHandler NeedPaintDelegate => _needPaintDelegate;


        /// <summary>
        /// Raises the PalettePaint event.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">An PaintLayoutEventArgs containing event data.</param>
        protected virtual void OnPalettePaint(object sender, PaletteLayoutEventArgs e)
        {
            // Can only generate change events if not suspended
            if (_suspendCount == 0)
            {
                PalettePaint?.Invoke(this, e);
            }
        }

        /// <summary>
        /// Raises the AllowFormChromeChanged event.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">An EventArgs containing event data.</param>
        protected virtual void OnAllowFormChromeChanged(object sender, EventArgs e)
        {
            // Can only generate change events if not suspended
            if (_suspendCount == 0)
            {
                AllowFormChromeChanged?.Invoke(this, e);
            }
        }

        /// <summary>
        /// Raises the BasePaletteChanged event.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">An EventArgs containing event data.</param>
        protected virtual void OnBasePaletteChanged(object sender, EventArgs e)
        {
            // Can only generate change events if not suspended
            if (_suspendCount == 0)
            {
                BasePaletteChanged?.Invoke(this, e);
            }
        }

        /// <summary>
        /// Raises the BaseRendererChanged event.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">An EventArgs containing event data.</param>
        protected virtual void OnBaseRendererChanged(object sender, EventArgs e)
        {
            // Can only generate change events if not suspended
            if (_suspendCount == 0)
            {
                BaseRendererChanged?.Invoke(this, e);
            }
        }

        /// <summary>
        /// Raises the ButtonSpecChanged event.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">An EventArgs containing event data.</param>
        protected virtual void OnButtonSpecChanged(object sender, EventArgs e)
        {
            // Can only generate change events if not suspended
            if (_suspendCount == 0)
            {
                ButtonSpecChanged?.Invoke(this, e);
            }
        }
        #endregion

        #region Property Grid
        // Note: Uncomment when `KryptonPalettePropertyGrid` is completed
        //[KryptonPersist]
        //[Category(@"Visuals")]
        //[Description(@"Colors associated with the property grid control.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //public KryptonPalettePropertyGrid PropertyGrid { get; set; }

        //public bool ShouldSerializePropertyGrid() => !PropertyGrid.IsDefault;
        #endregion

        #region Internal
        internal bool HasCircularReference()
        {
            // Use a dictionary as a set to check for existence
            var paletteSet = new Dictionary<IPalette, bool>();

            // Start processing from ourself upwards
            IPalette palette = this;

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
                    if (palette is KryptonPalette owner)
                    {
                        // Get the next palette up in hierarchy
                        palette = owner.BasePaletteMode switch
                        {
                            PaletteMode.Custom => owner.BasePalette,
                            PaletteMode.Global => KryptonManager.InternalGlobalPalette,
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

        #region Implementation Persistence
        private object ResetOperation(object parameter)
        {
            // Use reflection to reset the palette hierarchy
            ResetObjectToDefault(this, false);
            return null;
        }

        private object PopulateFromBaseOperation(object parameter)
        {
            // Always reset all the values first
            ResetObjectToDefault(this, true);

            // Ask each part of the palette to populate itself
            _allowFormChrome = _basePalette.GetAllowFormChrome();
            ButtonStyles.PopulateFromBase(Common);
            CalendarDay.PopulateFromBase();
            ButtonSpecs.PopulateFromBase();
            ControlStyles.PopulateFromBase(Common);
            ContextMenu.PopulateFromBase(Common);
            DragDrop.PopulateFromBase();
            FormStyles.PopulateFromBase(Common);
            GridStyles.PopulateFromBase(Common);
            HeaderStyles.PopulateFromBase(Common);
            HeaderGroup.PopulateFromBase();
            Images.PopulateFromBase();
            InputControlStyles.PopulateFromBase(Common);
            LabelStyles.PopulateFromBase(Common);
            Navigator.PopulateFromBase();
            PanelStyles.PopulateFromBase(Common);
            Ribbon.PopulateFromBase();
            SeparatorStyles.PopulateFromBase(Common);
            TabStyles.PopulateFromBase(Common);
            TrackBar.PopulateFromBase();
            ToolMenuStatus.PopulateFromBase();

            return null;
        }

        private object ImportFromFile(object parameter)
        {
            // Cast to correct type
            var filename = (string)parameter;

            // Check the target file actually exists
            if (!File.Exists(filename))
            {
                throw new ArgumentException(@"Provided file does not exist.", nameof(parameter));
            }

            // Create a new xml document for storing the palette settings
            XmlDocument doc = new();

            // Attempt to load as a valid xml document
            doc.Load(filename);

            // Perform actual import using the XmlDocument we just loaded
            ImportFromXmlDocument(doc);

            return filename;
        }

        private object ImportFromStream(object parameter)
        {
            // Cast to correct type
            Stream stream = (Stream)parameter;

            // Create a new xml document for storing the palette settings
            XmlDocument doc = new();

            // Attempt to load from the provided stream
            doc.Load(stream);

            // Perform actual import using the XmlDocument we just loaded
            ImportFromXmlDocument(doc);

            return stream;
        }

        private object ImportFromByteArray(object parameter)
        {
            // Cast to an array of parameters
            var byteArray = (byte[])parameter;

            // Create a memory based stream
            MemoryStream ms = new(byteArray);

            // Perform import from the memory stream
            ImportFromStream(ms);

            // Must close steam before retrieving bytes
            ms.Close();

            return null;
        }

        private void ImportFromXmlDocument(XmlDocument doc)
        {
            // Remember the current culture setting
            CultureInfo culture = Thread.CurrentThread.CurrentCulture;

            try
            {
                // Use the invariant culture for persistence
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

                // We insist on a root element
                if (!doc.HasChildNodes)
                {
                    throw new ArgumentException("Xml document does not have a root element.");
                }

                // Try and grab the root element
                XmlElement root = (XmlElement)doc.SelectSingleNode("KryptonPalette");

                // We insist the root is always present
                if (root == null)
                {
                    throw new ArgumentException("Root element must be called 'KryptonPalette'.");
                }

                // We insist the version number is always present
                if (!root.HasAttribute("Version"))
                {
                    throw new ArgumentException("Root element must have an attribute called 'Version'.");
                }

                // Grab the version number of the format being loaded
                var version = int.Parse(root.GetAttribute("Version"));

                if (version < _paletteVersion)
                {
                    throw new ArgumentException("Version '" + version + "' number is incompatible, only version " + _paletteVersion.ToString() +
                                                " or above can be imported.\nUse the PaletteUpgradeTool from the Application tab of the KryptonExplorer to upgrade.");
                }

                // Grab the properties and images elements
                XmlElement props = (XmlElement)root.SelectSingleNode("Properties");
                XmlElement images = (XmlElement)root.SelectSingleNode("Images");

                // There must be both properties and images elements present
                if (props == null)
                {
                    throw new ArgumentException("Element 'Properties' missing from the 'KryptonPalette'.");
                }

                if (images == null)
                {
                    throw new ArgumentException("Element 'Images' missing from the 'KryptonPalette'.");
                }

                // Cache the images from the images element
                ImageReverseDictionary imageCache = new();

                // Use reflection to import the palette hierarchy
                ImportImagesFromElement(images, imageCache);
                ImportObjectFromElement(props, imageCache, this);
            }
            finally
            {
                // Put back the old culture before existing routine
                Thread.CurrentThread.CurrentCulture = culture;
            }
        }

        private object ExportToFile(object parameter)
        {
            // Cast to an array of parameters
            var parameters = (object[])parameter;

            // Extract the two provided parameters
            var filename = (string)parameters[0];
            var ignoreDefaults = (bool)parameters[1];

            FileInfo info = new(filename);

            // Check the target directory actually exists
            if (!info.Directory.Exists)
            {
                throw new ArgumentException("Provided directory does not exist.");
            }

            // Create an XmlDocument containing the saved palette details
            XmlDocument doc = ExportToXmlDocument(ignoreDefaults);

            // Save to the provided filename
            doc.Save(filename);

            return filename;
        }

        private object ExportToStream(object parameter)
        {
            // Cast to an array of parameters
            var parameters = (object[])parameter;

            // Extract the two provided parameters
            Stream stream = (Stream)parameters[0];
            var ignoreDefaults = (bool)parameters[1];

            // Create an XmlDocument containing palette settings
            XmlDocument doc = ExportToXmlDocument(ignoreDefaults);

            // Save to the parameter provided stream object
            doc.Save(stream);

            return stream;
        }

        private object ExportToByteArray(object parameter)
        {
            // Cast to an array of parameters
            var parameters = (object[])parameter;

            // Extract the two provided parameters
            var ignoreDefaults = (bool)parameters[0];

            // Create a memory based stream
            MemoryStream ms = new();

            // Perform export into the memory stream
            ExportToStream(new object[] { ms, ignoreDefaults });

            // Must close steam before retrieving bytes
            ms.Close();

            // Return the array of raw bytes
            return ms.GetBuffer();
        }

        private XmlDocument ExportToXmlDocument(bool ignoreDefaults)
        {
            // Remember the current culture setting
            CultureInfo culture = Thread.CurrentThread.CurrentCulture;

            try
            {
                // Use the invariant culture for persistence
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

                // Create a new xml document for storing the palette settings
                XmlDocument doc = new();

                // Add the standard xml version number
                doc.AppendChild(doc.CreateProcessingInstruction("xml", @"version=""1.0"""));

                // Add a comment about the source of the document
                doc.AppendChild(doc.CreateComment("Created by exporting the settings of a KryptonPalette instance."));
                doc.AppendChild(doc.CreateComment("For more information about Krypton visit https://github.com/Krypton-Suite/Standard-Toolkit"));
                doc.AppendChild(doc.CreateComment("WARNING: Modifying this file may render it invalid for importing."));

                // Create a root node with version and the date information, by 
                // having a version number the loading of older version is easier
                XmlElement root = doc.CreateElement("KryptonPalette");
                root.SetAttribute("Version", _paletteVersion.ToString());
                root.SetAttribute("Generated", DateTime.Now.ToLongDateString() + ", @" + DateTime.Now.ToShortTimeString());
                doc.AppendChild(root);

                // Add two children, one for storing actual palette values the other for cached images
                XmlElement props = doc.CreateElement("Properties");
                XmlElement images = doc.CreateElement("Images");
                root.AppendChild(props);
                root.AppendChild(images);

                // Cache any images that are found during object export
                ImageDictionary imageCache = new();

                // Use reflection to export the palette hierarchy
                ExportObjectToElement(doc, props, imageCache, this, ignoreDefaults);
                ExportImagesToElement(doc, images, imageCache);

                return doc;
            }
            finally
            {
                // Put back the old culture before existing routine
                Thread.CurrentThread.CurrentCulture = culture;
            }
        }

        private void ImportObjectFromElement(XmlElement element,
                                             ImageReverseDictionary imageCache,
                                             object obj)
        {
            // Cannot import to nothing
            if (obj != null)
            {
                // Grab the type information for the object instance
                Type t = obj.GetType();

                // We are only interested in looking at the properties
                foreach (PropertyInfo prop in t.GetProperties())
                {
                    // Search each of the attributes applied to the property
                    foreach (var attrib in prop.GetCustomAttributes(false))
                    {
                        // Is it marked with the special krypton persist marker?
                        if (attrib is KryptonPersistAttribute persistAttribute)
                        {
                            // Cast attribute to the correct type

                            // Check if there is an element matching the property
                            XmlElement childElement = (XmlElement)element.SelectSingleNode(prop.Name);

                            // Can only import if a matching XML element is found
                            if (childElement != null)
                            {
                                // Should we navigate down inside the property?
                                if (persistAttribute.Navigate)
                                {
                                    // If we can read the property value
                                    if (prop.CanRead)
                                    {
                                        // Grab the property object and recurse into it
                                        var childObj = prop.GetValue(obj, null);
                                        ImportObjectFromElement(childElement, imageCache, childObj);
                                    }
                                }
                                else
                                {
                                    // The xml element must have a type and value in order to recreate it
                                    if (childElement.HasAttribute(@"Type") &&
                                        childElement.HasAttribute(@"Value"))
                                    {
                                        // Get the type/value attributes
                                        var valueType = childElement.GetAttribute(@"Type");
                                        var valueValue = childElement.GetAttribute(@"Value");

                                        // We special case the loading of images
                                        if (prop.PropertyType.Equals(typeof(Image)))
                                        {
                                            if (valueValue.Length == 0)
                                            {
                                                // An empty string represents a null image value
                                                prop.SetValue(obj, null, null);
                                            }
                                            else
                                            {
                                                // Have we already encountered the image?
                                                if (imageCache.ContainsKey(valueValue))
                                                {
                                                    // Push the image from the cache into the property
                                                    prop.SetValue(obj, imageCache[valueValue], null);
                                                }
                                                else
                                                {
                                                    // Cannot find image to set to empty
                                                    prop.SetValue(obj, null, null);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            object setValue = null;

                                            // We ignore conversion of a Font of value (none) because instead
                                            // of providing null it returns a default font value
                                            if (valueType != nameof(Font) || valueValue != @"(none)")
                                            {
                                                // We need the type converter to create a string representation
                                                TypeConverter converter = TypeDescriptor.GetConverter(StringToType(valueType));

                                                // Recreate the value using the converter
                                                setValue = converter.ConvertFromInvariantString(valueValue);
                                            }

                                            // Push the value into the actual property
                                            prop.SetValue(obj, setValue, null);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ImportImagesFromElement(XmlElement element, ImageReverseDictionary imageCache)
        {
            // Get all nodes storing images
            XmlNodeList images = element.SelectNodes(@"Image");

            // Load each image node entry in turn
            if (images != null)
            {
                foreach (XmlNode image in images)
                {
                    // Cast to the expected type
                    XmlElement imageElement = (XmlElement)image;

                    // Check the element is the expected type and has the required data
                    if (imageElement != null &&
                        imageElement.HasAttribute(@"Name") &&
                        imageElement.ChildNodes.Count == 1 &&
                        imageElement.ChildNodes[0].NodeType == XmlNodeType.CDATA)
                    {
                        try
                        {
                            // Extract the image name
                            var name = imageElement.GetAttribute(@"Name");

                            // Grab the CDATA section that contains the base64 value
                            XmlCDataSection cdata = (XmlCDataSection)imageElement.ChildNodes[0];

                            // Convert to back from a string to bytes
                            var bytes = Convert.FromBase64String(cdata.Value);

                            // Convert the bytes back into an Image
                            MemoryStream memory = new(bytes);
                            Bitmap resurect;
                            try
                            {
                                resurect = new Bitmap(memory);
                            }
                            catch
                            {
                                // Do the old way
                                // SYSLIB0011: BinaryFormatter serialization is obsolete
#pragma warning disable SYSLIB0011
                                BinaryFormatter formatter = new();
                                var old = (Image)formatter.Deserialize(memory);
#pragma warning restore SYSLIB0011
                                resurect = new Bitmap(old);
                            }


                            // Add into the lookup dictionary
                            imageCache.Add(name, resurect);
                        }
                        catch (SerializationException)
                        {
                            // Just ignore this image and carry on
                        }
                    }
                }
            }
        }

        private void ExportObjectToElement(XmlDocument doc,
                                           XmlElement element,
                                           ImageDictionary imageCache,
                                           object obj,
                                           bool ignoreDefaults)
        {
            // Cannot export from nothing
            if (obj != null)
            {
                // Grab the type information for the object instance
                Type t = obj.GetType();

                // We are only interested in looking at the properties
                foreach (PropertyInfo prop in t.GetProperties())
                {
                    // Search each of the attributes applied to the property
                    foreach (var attrib in prop.GetCustomAttributes(false))
                    {
                        // Is it marked with the special krypton persist marker?
                        if (attrib is KryptonPersistAttribute persist)
                        {
                            // Should we navigate down inside the property?
                            if (persist.Navigate)
                            {
                                // If we can read the property value
                                if (prop.CanRead)
                                {
                                    // Grab the property object
                                    var childObj = prop.GetValue(obj, null);

                                    // Should be test if the object contains only default values?
                                    if (ignoreDefaults)
                                    {
                                        PropertyDescriptor propertyIsDefault = TypeDescriptor.GetProperties(childObj)[@"IsDefault"];

                                        // All compound objects are expected to have an 'IsDefault' returning a boolean
                                        if (propertyIsDefault != null && propertyIsDefault.PropertyType == typeof(bool))
                                        {
                                            // If the object 'IsDefault' then no need to persist it
                                            if ((bool)propertyIsDefault.GetValue(childObj))
                                            {
                                                childObj = null;
                                            }
                                        }
                                    }

                                    // If we have an object to process
                                    if (childObj != null)
                                    {
                                        // Create and add a new xml element
                                        XmlElement childElement = doc.CreateElement(prop.Name);
                                        element.AppendChild(childElement);

                                        // Recurse into the object instance
                                        ExportObjectToElement(doc, childElement, imageCache, childObj, ignoreDefaults);
                                    }
                                }
                            }
                            else
                            {
                                var ignore = false;

                                // Grab the actual property value
                                var childObj = prop.GetValue(obj, null);

                                // Should we test if the property value is the default?
                                if (ignoreDefaults)
                                {
                                    var defaultAttribs = prop.GetCustomAttributes(typeof(DefaultValueAttribute), false);

                                    // Does this property have a default value attribute?
                                    if (defaultAttribs.Length == 1)
                                    {
                                        // Cast to correct type
                                        DefaultValueAttribute defaultAttrib = (DefaultValueAttribute)defaultAttribs[0];

                                        // Decide if the property value matches the default described by the attribute
                                        if (defaultAttrib.Value == null)
                                        {
                                            ignore = childObj == null;
                                        }
                                        else
                                        {
                                            ignore = defaultAttrib.Value.Equals(childObj);
                                        }
                                    }
                                }

                                // If we need to output the property value
                                if (!ignore)
                                {
                                    // Create and add a new xml element
                                    XmlElement childElement = doc.CreateElement(prop.Name);
                                    element.AppendChild(childElement);

                                    // Save the type of the property
                                    childElement.SetAttribute(@"Type", TypeToString(prop.PropertyType));

                                    // We special case the saving of images
                                    if (prop.PropertyType.Equals(typeof(Image)))
                                    {
                                        if (childObj == null)
                                        {
                                            // An empty string represents a null image value
                                            childElement.SetAttribute(@"Value", string.Empty);
                                        }
                                        else
                                        {
                                            // Cast to correct type
                                            if (childObj is not Bitmap image)
                                            {
                                                image = new Bitmap((Image)childObj);
                                            }

                                            // Have we already encountered the image?
                                            if (imageCache.ContainsKey(image))
                                            {
                                                // Save reference to the existing cached image
                                                childElement.SetAttribute(@"Value", imageCache[image]);
                                            }
                                            else
                                            {
                                                // Generate a placeholder string
                                                var imageName = @"ImageCache" + (imageCache.Count + 1);

                                                // Add the actual image instance into the cache
                                                imageCache.Add(image, imageName);

                                                // Save the placeholder name instead of the actual image
                                                childElement.SetAttribute(@"Value", imageName);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // We need the type converter to create a string representation
                                        TypeConverter converter = TypeDescriptor.GetConverter(prop.PropertyType);

                                        // Save to an invariant string so that load is not affected by culture
                                        childElement.SetAttribute(@"Value", converter.ConvertToInvariantString(childObj));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ExportImagesToElement(XmlDocument doc, XmlElement element, ImageDictionary imageCache)
        {
            // Process each image cache entry in turn
            foreach (var entry in imageCache)
            {
                try
                {
                    // Convert the Image into base64 so it can be used in xml
                    using MemoryStream memory = new();

                    entry.Key.Save(memory, entry.Key.RawFormat);
                    memory.Position = 0;
                    var base64 = Convert.ToBase64String(memory.ToArray());

                    // Create and add a new xml element
                    XmlElement imageElement = doc.CreateElement(@"Image");
                    imageElement.SetAttribute(@"Name", entry.Value);
                    element.AppendChild(imageElement);

                    // Set the image data into a CDATA section
                    XmlCDataSection cdata = doc.CreateCDataSection(base64);
                    imageElement.AppendChild(cdata);
                }
                catch (SerializationException)
                {
                    // Just ignore this image and carry on
                }
            }
        }

        private void ResetObjectToDefault(object obj, bool populate)
        {
            // Cannot reset nothing
            if (obj != null)
            {
                // Grab the type information for the object instance
                Type t = obj.GetType();

                // We are only interested in looking at the properties
                foreach (PropertyInfo prop in t.GetProperties())
                {
                    // Search each of the attributes applied to the property
                    foreach (var attrib in prop.GetCustomAttributes(false))
                    {
                        // Is it marked with the special krypton persist marker?
                        if (attrib is KryptonPersistAttribute persist)
                        {
                            // Should we navigate down inside the property?
                            if (persist.Navigate)
                            {
                                // If we can read the property value
                                if (prop.CanRead)
                                {
                                    // Grab the property object
                                    var childObj = prop.GetValue(obj, null);

                                    PropertyDescriptor propertyIsDefault = TypeDescriptor.GetProperties(childObj)[@"IsDefault"];

                                    // All compound objects are expected to have an 'IsDefault' returning a boolean
                                    if (propertyIsDefault != null && propertyIsDefault.PropertyType == typeof(bool))
                                    {
                                        // If the object 'IsDefault' then no need to reset it
                                        if ((bool)propertyIsDefault.GetValue(childObj))
                                        {
                                            childObj = null;
                                        }
                                    }

                                    // If we have an object to process
                                    if (childObj != null)
                                    {
                                        // Recurse into the object instance
                                        ResetObjectToDefault(childObj, populate);
                                    }
                                }
                            }
                            else
                            {
                                // Only default value if not part of a populate operation or we are part of a populate
                                // operation and the persist property indicates it can be reset in a populate scenario
                                if (!populate || persist.Populate)
                                {
                                    var defaultAttribs = prop.GetCustomAttributes(typeof(DefaultValueAttribute), false);

                                    // Does this property have a default value attribute?
                                    if (defaultAttribs.Length == 1)
                                    {
                                        // Cast to correct type
                                        DefaultValueAttribute defaultAttrib = (DefaultValueAttribute)defaultAttribs[0];

                                        // Grab the actual property value
                                        var childObj = prop.GetValue(obj, null);

                                        // If the default is for a 'null' value
                                        if (defaultAttrib.Value == null)
                                        {
                                            // If the actual value is not 'null'
                                            if (childObj != null)
                                            {
                                                // Set the property to be 'null'
                                                prop.SetValue(obj, null, null);
                                            }
                                        }
                                        else
                                        {
                                            // If the current value does not match the default
                                            if (!defaultAttrib.Value.Equals(childObj))
                                            {
                                                // Set the property to be default
                                                prop.SetValue(obj, defaultAttrib.Value, null);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        static readonly Dictionary<Type, string> _typeTests = new()
        {
            [typeof(int)] = @"Int", //nameof(Int32),
            [typeof(string)] = nameof(String),
            [typeof(float)] = @"Single",
            [typeof(bool)] = @"Bool",
            [typeof(Color)] = nameof(Color),
            [typeof(Image)] = nameof(Image),
            [typeof(Font)] = nameof(Font),
            [typeof(Padding)] = nameof(Padding),
            [typeof(InheritBool)] = nameof(InheritBool),
            [typeof(PaletteRectangleAlign)] = nameof(PaletteRectangleAlign),
            [typeof(PaletteRelativeAlign)] = nameof(PaletteRelativeAlign),
            [typeof(PaletteImageEffect)] = nameof(PaletteImageEffect),
            [typeof(PaletteImageStyle)] = nameof(PaletteImageStyle),
            [typeof(PaletteTextHint)] = nameof(PaletteTextHint),
            [typeof(PaletteTextHotkeyPrefix)] = nameof(PaletteTextHotkeyPrefix),
            [typeof(PaletteTextTrim)] = nameof(PaletteTextTrim),
            [typeof(PaletteColorStyle)] = nameof(PaletteColorStyle),
            [typeof(PaletteGraphicsHint)] = nameof(PaletteGraphicsHint),
            [typeof(PaletteMode)] = nameof(PaletteMode),
            [typeof(PaletteButtonStyle)] = nameof(PaletteButtonStyle),
            [typeof(PaletteButtonOrientation)] = nameof(PaletteButtonOrientation),
            [typeof(PaletteRelativeEdgeAlign)] = nameof(PaletteRelativeEdgeAlign),
            [typeof(RendererMode)] = nameof(RendererMode),
            [typeof(PaletteDrawBorders)] = nameof(PaletteDrawBorders),
            [typeof(PaletteContentText)] = nameof(PaletteContentText),
            [typeof(PaletteContentImage)] = nameof(PaletteContentImage),
            [typeof(PaletteDragFeedback)] = nameof(PaletteDragFeedback),
            [typeof(PaletteRibbonShape)] = nameof(PaletteRibbonShape)
        };
        private static string TypeToString(Type t)
        {
            if (_typeTests.TryGetValue(t, out var str))
            {
                return str;
            }

            throw new ApplicationException($@"Unrecognised type '{t}' for export.");
        }

        private static readonly Dictionary<string, Type> _stringTests = _typeTests.ToDictionary((i) => i.Value, (i) => i.Key);

        private static Type StringToType(string s)
        {
            if (_stringTests.TryGetValue(s, out var t))
            {
                return t;
            }
            throw new ApplicationException($@"Unrecognised type '{s}' for import.");
        }
        #endregion

        #region Implementation GetPalette
        private PaletteElementColor GetTrackBar(PaletteElement element, PaletteState state)
        {
            switch (element)
            {
                case PaletteElement.TrackBarTick:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return TrackBar.StateNormal.Tick;
                        case PaletteState.Disabled:
                            return TrackBar.StateDisabled.Tick;
                        case PaletteState.FocusOverride:
                            return TrackBar.OverrideFocus.Tick;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented("GetTrackBar(PaletteElement element, PaletteState state)", "KryptonPalette.cs");
                            return null;
                    }
                case PaletteElement.TrackBarTrack:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return TrackBar.StateNormal.Track;
                        case PaletteState.Disabled:
                            return TrackBar.StateDisabled.Track;
                        case PaletteState.FocusOverride:
                            return TrackBar.OverrideFocus.Track;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented("GetTrackBar(PaletteElement element, PaletteState state)", "KryptonPalette.cs");
                            return null;
                    }
                case PaletteElement.TrackBarPosition:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return TrackBar.StateNormal.Position;
                        case PaletteState.Disabled:
                            return TrackBar.StateDisabled.Position;
                        case PaletteState.Tracking:
                            return TrackBar.StateTracking.Position;
                        case PaletteState.Pressed:
                            return TrackBar.StatePressed.Position;
                        case PaletteState.FocusOverride:
                            return TrackBar.OverrideFocus.Position;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented("GetTrackBar(PaletteElement element, PaletteState state)", "KryptonPalette.cs");
                            return null;
                    }
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented("GetTrackBar(PaletteElement element, PaletteState state)", "KryptonPalette.cs");
                    return null;
            }
        }

        private IPaletteRibbonGeneral GetPaletteRibbonGeneral() => Ribbon.RibbonGeneral;

        private IPaletteRibbonGeneral GetPaletteRibbonGeneral(PaletteState state) => Ribbon.RibbonGeneral;

        private IPaletteRibbonBack GetPaletteRibbonBack(PaletteRibbonBackStyle style, PaletteState state)
        {
            switch (style)
            {
                case PaletteRibbonBackStyle.RibbonAppButton:
                    return GetPaletteRibbonBack(Ribbon.RibbonAppButton, state);
                case PaletteRibbonBackStyle.RibbonAppMenuDocs:
                    return Ribbon.RibbonAppMenuDocs;
                case PaletteRibbonBackStyle.RibbonAppMenuInner:
                    return Ribbon.RibbonAppMenuInner;
                case PaletteRibbonBackStyle.RibbonAppMenuOuter:
                    return Ribbon.RibbonAppMenuOuter;
                case PaletteRibbonBackStyle.RibbonGroupArea:
                    return GetPaletteRibbonBack(Ribbon.RibbonGroupArea, state);
                case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
                    return GetPaletteRibbonBack(Ribbon.RibbonGroupNormalBorder, state);
                case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
                    return GetPaletteRibbonBack(Ribbon.RibbonGroupNormalTitle, state);
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                    return GetPaletteRibbonBack(Ribbon.RibbonGroupCollapsedBorder, state);
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
                    return GetPaletteRibbonBack(Ribbon.RibbonGroupCollapsedBack, state);
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
                    return GetPaletteRibbonBack(Ribbon.RibbonGroupCollapsedFrameBorder, state);
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                    return GetPaletteRibbonBack(Ribbon.RibbonGroupCollapsedFrameBack, state);
                case PaletteRibbonBackStyle.RibbonTab:
                    return GetPaletteRibbonBack(Ribbon.RibbonTab, state);
                case PaletteRibbonBackStyle.RibbonQATFullbar:
                    return Ribbon.RibbonQATFullbar;
                case PaletteRibbonBackStyle.RibbonQATMinibar:
                    return GetPaletteRibbonBack(Ribbon.RibbonQATMinibar, state);
                case PaletteRibbonBackStyle.RibbonQATOverflow:
                    return Ribbon.RibbonQATOverflow;
                case PaletteRibbonBackStyle.RibbonGalleryBack:
                    return Ribbon.RibbonGalleryBack;
                case PaletteRibbonBackStyle.RibbonGalleryBorder:
                    return Ribbon.RibbonGalleryBorder;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteRibbonBack GetPaletteRibbonBack(KryptonPaletteRibbonTab ribbonTab,
                                                        PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                case PaletteState.Normal:
                    return ribbonTab.StateNormal;
                case PaletteState.Tracking:
                case PaletteState.Pressed:
                    return ribbonTab.StateTracking;
                case PaletteState.CheckedNormal:
                    return ribbonTab.StateCheckedNormal;
                case PaletteState.CheckedTracking:
                case PaletteState.CheckedPressed:
                    return ribbonTab.StateCheckedTracking;
                case PaletteState.ContextTracking:
                    return ribbonTab.StateContextTracking;
                case PaletteState.ContextCheckedNormal:
                    return ribbonTab.StateContextCheckedNormal;
                case PaletteState.ContextCheckedTracking:
                    return ribbonTab.StateContextCheckedTracking;
                case PaletteState.FocusOverride:
                    return ribbonTab.OverrideFocus;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteRibbonBack GetPaletteRibbonBack(KryptonPaletteRibbonAppButton ribbonAppButton,
                                                        PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Normal:
                    return ribbonAppButton.StateNormal;
                case PaletteState.Tracking:
                    return ribbonAppButton.StateTracking;
                case PaletteState.Pressed:
                    return ribbonAppButton.StatePressed;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteRibbonBack GetPaletteRibbonBack(KryptonPaletteRibbonGroupArea ribbonGroupArea,
                                                        PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Normal:
                case PaletteState.CheckedNormal:
                    return ribbonGroupArea.StateCheckedNormal;
                case PaletteState.ContextCheckedNormal:
                    return ribbonGroupArea.StateContextCheckedNormal;
                case PaletteState.Tracking:
                    return ribbonGroupArea.StateTracking;
                case PaletteState.ContextNormal:
                    return ribbonGroupArea.StateCheckedNormal;
                case PaletteState.ContextPressed:
                    return ribbonGroupArea.StateContextPressed;
                case PaletteState.ContextTracking:
                    return ribbonGroupArea.StateContextTracking;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteRibbonBack GetPaletteRibbonBack(KryptonPaletteRibbonGroupNormalBorder ribbonGroupNormalBorder,
                                                        PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Normal:
                    return ribbonGroupNormalBorder.StateNormal;
                case PaletteState.Tracking:
                    return ribbonGroupNormalBorder.StateTracking;
                case PaletteState.ContextNormal:
                    return ribbonGroupNormalBorder.StateContextNormal;
                case PaletteState.ContextTracking:
                    return ribbonGroupNormalBorder.StateContextTracking;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteRibbonBack GetPaletteRibbonBack(KryptonPaletteRibbonGroupNormalTitle ribbonGroupNormalTitle,
                                                        PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Normal:
                    return ribbonGroupNormalTitle.StateNormal;
                case PaletteState.Tracking:
                    return ribbonGroupNormalTitle.StateTracking;
                case PaletteState.ContextNormal:
                    return ribbonGroupNormalTitle.StateContextNormal;
                case PaletteState.ContextTracking:
                    return ribbonGroupNormalTitle.StateContextTracking;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteRibbonBack GetPaletteRibbonBack(KryptonPaletteRibbonGroupCollapsedBorder ribbonGroupCollapsedBorder,
                                                        PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Normal:
                    return ribbonGroupCollapsedBorder.StateNormal;
                case PaletteState.Tracking:
                case PaletteState.Pressed:
                    return ribbonGroupCollapsedBorder.StateTracking;
                case PaletteState.ContextNormal:
                    return ribbonGroupCollapsedBorder.StateContextNormal;
                case PaletteState.ContextTracking:
                case PaletteState.ContextPressed:
                    return ribbonGroupCollapsedBorder.StateContextTracking;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteRibbonBack GetPaletteRibbonBack(KryptonPaletteRibbonGroupCollapsedBack ribbonGroupCollapsedBack,
                                                        PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Normal:
                    return ribbonGroupCollapsedBack.StateNormal;
                case PaletteState.Tracking:
                case PaletteState.Pressed:
                    return ribbonGroupCollapsedBack.StateTracking;
                case PaletteState.ContextNormal:
                    return ribbonGroupCollapsedBack.StateContextNormal;
                case PaletteState.ContextTracking:
                case PaletteState.ContextPressed:
                    return ribbonGroupCollapsedBack.StateContextTracking;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteRibbonBack GetPaletteRibbonBack(KryptonPaletteRibbonGroupCollapsedFrameBorder ribbonGroupCollapsedFrameBorder,
                                                        PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Normal:
                    return ribbonGroupCollapsedFrameBorder.StateNormal;
                case PaletteState.Tracking:
                case PaletteState.Pressed:
                    return ribbonGroupCollapsedFrameBorder.StateTracking;
                case PaletteState.ContextNormal:
                    return ribbonGroupCollapsedFrameBorder.StateContextNormal;
                case PaletteState.ContextTracking:
                case PaletteState.ContextPressed:
                    return ribbonGroupCollapsedFrameBorder.StateContextTracking;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteRibbonBack GetPaletteRibbonBack(KryptonPaletteRibbonGroupCollapsedFrameBack ribbonGroupCollapsedFrameBack,
                                                        PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Normal:
                    return ribbonGroupCollapsedFrameBack.StateNormal;
                case PaletteState.Tracking:
                case PaletteState.Pressed:
                    return ribbonGroupCollapsedFrameBack.StateTracking;
                case PaletteState.ContextNormal:
                    return ribbonGroupCollapsedFrameBack.StateContextNormal;
                case PaletteState.ContextTracking:
                case PaletteState.ContextPressed:
                    return ribbonGroupCollapsedFrameBack.StateContextTracking;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteRibbonBack GetPaletteRibbonBack(KryptonPaletteRibbonQATMinibar ribbonQATMinibar,
                                                        PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Normal:
                case PaletteState.CheckedNormal:
                    return ribbonQATMinibar.StateActive;
                case PaletteState.Disabled:
                    return ribbonQATMinibar.StateInactive;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteRibbonText GetPaletteRibbonText(PaletteRibbonTextStyle style,
                                                        PaletteState state)
        {
            switch (style)
            {
                case PaletteRibbonTextStyle.RibbonAppMenuDocsEntry:
                    return Ribbon.RibbonAppMenuDocsEntry;
                case PaletteRibbonTextStyle.RibbonAppMenuDocsTitle:
                    return Ribbon.RibbonAppMenuDocsTitle;
                case PaletteRibbonTextStyle.RibbonGroupCheckBoxText:
                    return GetPaletteRibbonText(Ribbon.RibbonGroupCheckBoxText, state);
                case PaletteRibbonTextStyle.RibbonGroupRadioButtonText:
                    return GetPaletteRibbonText(Ribbon.RibbonGroupRadioButtonText, state);
                case PaletteRibbonTextStyle.RibbonGroupButtonText:
                    return GetPaletteRibbonText(Ribbon.RibbonGroupButtonText, state);
                case PaletteRibbonTextStyle.RibbonGroupLabelText:
                    return GetPaletteRibbonText(Ribbon.RibbonGroupLabelText, state);
                case PaletteRibbonTextStyle.RibbonGroupNormalTitle:
                    return GetPaletteRibbonText(Ribbon.RibbonGroupNormalTitle, state);
                case PaletteRibbonTextStyle.RibbonGroupCollapsedText:
                    return GetPaletteRibbonText(Ribbon.RibbonGroupCollapsedText, state);
                case PaletteRibbonTextStyle.RibbonTab:
                    return GetPaletteRibbonText(Ribbon.RibbonTab, state);
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteRibbonText GetPaletteRibbonText(KryptonPaletteRibbonTab ribbonTab,
                                                        PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                case PaletteState.Normal:
                    return ribbonTab.StateNormal;
                case PaletteState.Tracking:
                case PaletteState.Pressed:
                case PaletteState.ContextTracking:
                    return ribbonTab.StateTracking;
                case PaletteState.CheckedNormal:
                case PaletteState.ContextCheckedNormal:
                    return ribbonTab.StateCheckedNormal;
                case PaletteState.CheckedTracking:
                case PaletteState.CheckedPressed:
                case PaletteState.ContextCheckedTracking:
                    return ribbonTab.StateCheckedTracking;
                case PaletteState.FocusOverride:
                    return ribbonTab.OverrideFocus;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteRibbonText GetPaletteRibbonText(KryptonPaletteRibbonGroupNormalTitle ribbonGroupNormalTitle,
                                                        PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Normal:
                    return ribbonGroupNormalTitle.StateNormal;
                case PaletteState.Tracking:
                    return ribbonGroupNormalTitle.StateTracking;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteRibbonText GetPaletteRibbonText(KryptonPaletteRibbonGroupCollapsedText ribbonGroupCollapsedText,
                                                        PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Normal:
                    return ribbonGroupCollapsedText.StateNormal;
                case PaletteState.Tracking:
                    return ribbonGroupCollapsedText.StateTracking;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteRibbonText GetPaletteRibbonText(KryptonPaletteRibbonGroupBaseText ribbonGroupButtonText,
                                                        PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Normal:
                    return ribbonGroupButtonText.StateNormal;
                case PaletteState.Disabled:
                    return ribbonGroupButtonText.StateDisabled;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteButtonSpec GetPaletteButtonSpec(PaletteButtonSpecStyle style)
        {
            switch (style)
            {
                case PaletteButtonSpecStyle.Generic:
                    return ButtonSpecs.Generic;
                case PaletteButtonSpecStyle.Close:
                    return ButtonSpecs.Close;
                case PaletteButtonSpecStyle.Context:
                    return ButtonSpecs.Context;
                case PaletteButtonSpecStyle.Next:
                    return ButtonSpecs.Next;
                case PaletteButtonSpecStyle.Previous:
                    return ButtonSpecs.Previous;
                case PaletteButtonSpecStyle.ArrowLeft:
                    return ButtonSpecs.ArrowLeft;
                case PaletteButtonSpecStyle.ArrowRight:
                    return ButtonSpecs.ArrowRight;
                case PaletteButtonSpecStyle.ArrowUp:
                    return ButtonSpecs.ArrowUp;
                case PaletteButtonSpecStyle.ArrowDown:
                    return ButtonSpecs.ArrowDown;
                case PaletteButtonSpecStyle.DropDown:
                    return ButtonSpecs.DropDown;
                case PaletteButtonSpecStyle.PinVertical:
                    return ButtonSpecs.PinVertical;
                case PaletteButtonSpecStyle.PinHorizontal:
                    return ButtonSpecs.PinHorizontal;
                case PaletteButtonSpecStyle.FormClose:
                    return ButtonSpecs.FormClose;
                case PaletteButtonSpecStyle.FormMin:
                    return ButtonSpecs.FormMin;
                case PaletteButtonSpecStyle.FormMax:
                    return ButtonSpecs.FormMax;
                case PaletteButtonSpecStyle.FormRestore:
                    return ButtonSpecs.FormRestore;
                case PaletteButtonSpecStyle.FormHelp:
                    return ButtonSpecs.FormHelp;
                case PaletteButtonSpecStyle.PendantClose:
                    return ButtonSpecs.PendantClose;
                case PaletteButtonSpecStyle.PendantMin:
                    return ButtonSpecs.PendantMin;
                case PaletteButtonSpecStyle.PendantRestore:
                    return ButtonSpecs.PendantRestore;
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                    return ButtonSpecs.WorkspaceMaximize;
                case PaletteButtonSpecStyle.WorkspaceRestore:
                    return ButtonSpecs.WorkspaceRestore;
                case PaletteButtonSpecStyle.RibbonMinimize:
                    return ButtonSpecs.RibbonMinimize;
                case PaletteButtonSpecStyle.RibbonExpand:
                    return ButtonSpecs.RibbonExpand;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteBack GetPaletteBack(PaletteBackStyle style, PaletteState state)
        {
            // Update the redirectors
            Common.StateCommon.BackStyle = style;

            switch (style)
            {
                case PaletteBackStyle.ButtonStandalone:
                    return GetPaletteBackButton(ButtonStyles.ButtonStandalone, state);
                case PaletteBackStyle.ButtonAlternate:
                    return GetPaletteBackButton(ButtonStyles.ButtonAlternate, state);
                case PaletteBackStyle.ButtonLowProfile:
                    return GetPaletteBackButton(ButtonStyles.ButtonLowProfile, state);
                case PaletteBackStyle.ButtonButtonSpec:
                    return GetPaletteBackButton(ButtonStyles.ButtonButtonSpec, state);
                case PaletteBackStyle.ButtonBreadCrumb:
                    return GetPaletteBackButton(ButtonStyles.ButtonBreadCrumb, state);
                case PaletteBackStyle.ButtonCalendarDay:
                    return GetPaletteBackCalendarDay(CalendarDay, state);
                case PaletteBackStyle.ButtonCluster:
                    return GetPaletteBackButton(ButtonStyles.ButtonCluster, state);
                case PaletteBackStyle.ButtonGallery:
                    return GetPaletteBackButton(ButtonStyles.ButtonGallery, state);
                case PaletteBackStyle.ButtonNavigatorStack:
                    return GetPaletteBackButton(ButtonStyles.ButtonNavigatorStack, state);
                case PaletteBackStyle.ButtonNavigatorOverflow:
                    return GetPaletteBackButton(ButtonStyles.ButtonNavigatorOverflow, state);
                case PaletteBackStyle.ButtonNavigatorMini:
                    return GetPaletteBackButton(ButtonStyles.ButtonNavigatorMini, state);
                case PaletteBackStyle.ButtonInputControl:
                    return GetPaletteBackButton(ButtonStyles.ButtonInputControl, state);
                case PaletteBackStyle.ButtonListItem:
                    return GetPaletteBackButton(ButtonStyles.ButtonListItem, state);
                case PaletteBackStyle.ButtonForm:
                    return GetPaletteBackButton(ButtonStyles.ButtonForm, state);
                case PaletteBackStyle.ButtonFormClose:
                    return GetPaletteBackButton(ButtonStyles.ButtonFormClose, state);
                case PaletteBackStyle.ButtonCommand:
                    return GetPaletteBackButton(ButtonStyles.ButtonCommand, state);
                case PaletteBackStyle.ButtonCustom1:
                    return GetPaletteBackButton(ButtonStyles.ButtonCustom1, state);
                case PaletteBackStyle.ButtonCustom2:
                    return GetPaletteBackButton(ButtonStyles.ButtonCustom2, state);
                case PaletteBackStyle.ButtonCustom3:
                    return GetPaletteBackButton(ButtonStyles.ButtonCustom3, state);
                case PaletteBackStyle.ControlClient:
                    return GetPaletteBackControl(ControlStyles.ControlClient, state);
                case PaletteBackStyle.ControlAlternate:
                    return GetPaletteBackControl(ControlStyles.ControlAlternate, state);
                case PaletteBackStyle.ControlGroupBox:
                    return GetPaletteBackControl(ControlStyles.ControlGroupBox, state);
                case PaletteBackStyle.ControlToolTip:
                    return GetPaletteBackControl(ControlStyles.ControlToolTip, state);
                case PaletteBackStyle.ControlRibbon:
                    return GetPaletteBackControl(ControlStyles.ControlRibbon, state);
                case PaletteBackStyle.ControlRibbonAppMenu:
                    return GetPaletteBackControl(ControlStyles.ControlRibbonAppMenu, state);
                case PaletteBackStyle.ControlCustom1:
                    return GetPaletteBackControl(ControlStyles.ControlCustom1, state);
                case PaletteBackStyle.ControlCustom2:
                    return GetPaletteBackControl(ControlStyles.ControlCustom2, state);
                case PaletteBackStyle.ControlCustom3:
                    return GetPaletteBackControl(ControlStyles.ControlCustom3, state);
                case PaletteBackStyle.InputControlStandalone:
                    return GetPaletteInputControl(InputControlStyles.InputControlStandalone, state).Back;
                case PaletteBackStyle.InputControlRibbon:
                    return GetPaletteInputControl(InputControlStyles.InputControlRibbon, state).Back;
                case PaletteBackStyle.InputControlCustom1:
                    return GetPaletteInputControl(InputControlStyles.InputControlCustom1, state).Back;
                case PaletteBackStyle.InputControlCustom2:
                    return GetPaletteInputControl(InputControlStyles.InputControlCustom2, state).Back;
                case PaletteBackStyle.InputControlCustom3:
                    return GetPaletteInputControl(InputControlStyles.InputControlCustom3, state).Back;
                case PaletteBackStyle.FormMain:
                    return GetPaletteBackForm(FormStyles.FormMain, state);
                case PaletteBackStyle.FormCustom1:
                    return GetPaletteBackForm(FormStyles.FormCustom1, state);
                case PaletteBackStyle.FormCustom2:
                    return GetPaletteBackForm(FormStyles.FormCustom2, state);
                case PaletteBackStyle.FormCustom3:
                    return GetPaletteBackForm(FormStyles.FormCustom3, state);
                case PaletteBackStyle.GridBackgroundList:
                    return GetPaletteBackGridBackground(GridStyles.GridList, state);
                case PaletteBackStyle.GridBackgroundSheet:
                    return GetPaletteBackGridBackground(GridStyles.GridSheet, state);
                case PaletteBackStyle.GridBackgroundCustom1:
                    return GetPaletteBackGridBackground(GridStyles.GridCustom1, state);
                case PaletteBackStyle.GridBackgroundCustom2:
                    return GetPaletteBackGridBackground(GridStyles.GridCustom2, state);
                case PaletteBackStyle.GridBackgroundCustom3:
                    return GetPaletteBackGridBackground(GridStyles.GridCustom3, state);
                case PaletteBackStyle.GridHeaderColumnList:
                    return GetPaletteBackGridHeaderColumn(GridStyles.GridList, state);
                case PaletteBackStyle.GridHeaderColumnSheet:
                    return GetPaletteBackGridHeaderColumn(GridStyles.GridSheet, state);
                case PaletteBackStyle.GridHeaderColumnCustom1:
                    return GetPaletteBackGridHeaderColumn(GridStyles.GridCustom1, state);
                case PaletteBackStyle.GridHeaderColumnCustom2:
                    return GetPaletteBackGridHeaderColumn(GridStyles.GridCustom2, state);
                case PaletteBackStyle.GridHeaderColumnCustom3:
                    return GetPaletteBackGridHeaderColumn(GridStyles.GridCustom3, state);
                case PaletteBackStyle.GridHeaderRowList:
                    return GetPaletteBackGridHeaderRow(GridStyles.GridList, state);
                case PaletteBackStyle.GridHeaderRowSheet:
                    return GetPaletteBackGridHeaderRow(GridStyles.GridSheet, state);
                case PaletteBackStyle.GridHeaderRowCustom1:
                    return GetPaletteBackGridHeaderRow(GridStyles.GridCustom1, state);
                case PaletteBackStyle.GridHeaderRowCustom2:
                    return GetPaletteBackGridHeaderRow(GridStyles.GridCustom2, state);
                case PaletteBackStyle.GridHeaderRowCustom3:
                    return GetPaletteBackGridHeaderRow(GridStyles.GridCustom3, state);
                case PaletteBackStyle.GridDataCellList:
                    return GetPaletteBackGridDataCell(GridStyles.GridList, state);
                case PaletteBackStyle.GridDataCellSheet:
                    return GetPaletteBackGridDataCell(GridStyles.GridSheet, state);
                case PaletteBackStyle.GridDataCellCustom1:
                    return GetPaletteBackGridDataCell(GridStyles.GridCustom1, state);
                case PaletteBackStyle.GridDataCellCustom2:
                    return GetPaletteBackGridDataCell(GridStyles.GridCustom2, state);
                case PaletteBackStyle.GridDataCellCustom3:
                    return GetPaletteBackGridDataCell(GridStyles.GridCustom3, state);
                case PaletteBackStyle.HeaderPrimary:
                    return GetPaletteBackHeader(HeaderStyles.HeaderPrimary, state);
                case PaletteBackStyle.HeaderSecondary:
                    return GetPaletteBackHeader(HeaderStyles.HeaderSecondary, state);
                case PaletteBackStyle.HeaderDockInactive:
                    return GetPaletteBackHeader(HeaderStyles.HeaderDockInactive, state);
                case PaletteBackStyle.HeaderDockActive:
                    return GetPaletteBackHeader(HeaderStyles.HeaderDockActive, state);
                case PaletteBackStyle.HeaderCalendar:
                    return GetPaletteBackHeader(HeaderStyles.HeaderCalendar, state);
                case PaletteBackStyle.HeaderForm:
                    return GetPaletteBackHeader(HeaderStyles.HeaderForm, state);
                case PaletteBackStyle.HeaderCustom1:
                    return GetPaletteBackHeader(HeaderStyles.HeaderCustom1, state);
                case PaletteBackStyle.HeaderCustom2:
                    return GetPaletteBackHeader(HeaderStyles.HeaderCustom2, state);
                case PaletteBackStyle.HeaderCustom3:
                    return GetPaletteBackHeader(HeaderStyles.HeaderCustom3, state);
                case PaletteBackStyle.PanelClient:
                    return GetPalettePanel(PanelStyles.PanelClient, state);
                case PaletteBackStyle.PanelAlternate:
                    return GetPalettePanel(PanelStyles.PanelAlternate, state);
                case PaletteBackStyle.PanelRibbonInactive:
                    return GetPalettePanel(PanelStyles.PanelRibbonInactive, state);
                case PaletteBackStyle.PanelCustom1:
                    return GetPalettePanel(PanelStyles.PanelCustom1, state);
                case PaletteBackStyle.PanelCustom2:
                    return GetPalettePanel(PanelStyles.PanelCustom2, state);
                case PaletteBackStyle.PanelCustom3:
                    return GetPalettePanel(PanelStyles.PanelCustom3, state);
                case PaletteBackStyle.SeparatorLowProfile:
                    return GetPaletteBackSeparator(SeparatorStyles.SeparatorLowProfile, state);
                case PaletteBackStyle.SeparatorHighProfile:
                    return GetPaletteBackSeparator(SeparatorStyles.SeparatorHighProfile, state);
                case PaletteBackStyle.SeparatorHighInternalProfile:
                    return GetPaletteBackSeparator(SeparatorStyles.SeparatorHighInternalProfile, state);
                case PaletteBackStyle.SeparatorCustom1:
                    return GetPaletteBackSeparator(SeparatorStyles.SeparatorCustom1, state);
                case PaletteBackStyle.SeparatorCustom2:
                    return GetPaletteBackSeparator(SeparatorStyles.SeparatorCustom2, state);
                case PaletteBackStyle.SeparatorCustom3:
                    return GetPaletteBackSeparator(SeparatorStyles.SeparatorCustom3, state);
                case PaletteBackStyle.TabHighProfile:
                    return GetPaletteBackTab(TabStyles.TabHighProfile, state);
                case PaletteBackStyle.TabStandardProfile:
                    return GetPaletteBackTab(TabStyles.TabStandardProfile, state);
                case PaletteBackStyle.TabLowProfile:
                    return GetPaletteBackTab(TabStyles.TabLowProfile, state);
                case PaletteBackStyle.TabDock:
                    return GetPaletteBackTab(TabStyles.TabDock, state);
                case PaletteBackStyle.TabDockAutoHidden:
                    return GetPaletteBackTab(TabStyles.TabDockAutoHidden, state);
                case PaletteBackStyle.TabOneNote:
                    return GetPaletteBackTab(TabStyles.TabOneNote, state);
                case PaletteBackStyle.TabCustom1:
                    return GetPaletteBackTab(TabStyles.TabCustom1, state);
                case PaletteBackStyle.TabCustom2:
                    return GetPaletteBackTab(TabStyles.TabCustom2, state);
                case PaletteBackStyle.TabCustom3:
                    return GetPaletteBackTab(TabStyles.TabCustom3, state);
                case PaletteBackStyle.ContextMenuOuter:
                    return ContextMenu.StateCommon.ControlOuter.Back;
                case PaletteBackStyle.ContextMenuInner:
                    return ContextMenu.StateCommon.ControlInner.Back;
                case PaletteBackStyle.ContextMenuHeading:
                    return ContextMenu.StateCommon.Heading.Back;
                case PaletteBackStyle.ContextMenuItemHighlight:
                    return GetPaletteBackContextMenuItemHighlight(state);
                case PaletteBackStyle.ContextMenuItemImage:
                    return GetPaletteBackContextMenuItemImage(state);
                case PaletteBackStyle.ContextMenuItemSplit:
                    return GetPaletteBackContextMenuItemSplit(state);
                case PaletteBackStyle.ContextMenuItemImageColumn:
                    return ContextMenu.StateCommon.ItemImageColumn.Back;
                case PaletteBackStyle.ContextMenuSeparator:
                    return ContextMenu.StateCommon.Separator.Back;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private PaletteBorder GetPaletteBorder(PaletteBorderStyle style, PaletteState state)
        {
            // Must update the redirector values used if the palette source is used
            Common.StateCommon.BorderStyle = style;

            switch (style)
            {
                case PaletteBorderStyle.ButtonStandalone:
                    return GetPaletteBorderButton(ButtonStyles.ButtonStandalone, state);
                case PaletteBorderStyle.ButtonAlternate:
                    return GetPaletteBorderButton(ButtonStyles.ButtonAlternate, state);
                case PaletteBorderStyle.ButtonLowProfile:
                    return GetPaletteBorderButton(ButtonStyles.ButtonLowProfile, state);
                case PaletteBorderStyle.ButtonButtonSpec:
                    return GetPaletteBorderButton(ButtonStyles.ButtonButtonSpec, state);
                case PaletteBorderStyle.ButtonBreadCrumb:
                    return GetPaletteBorderButton(ButtonStyles.ButtonBreadCrumb, state);
                case PaletteBorderStyle.ButtonCalendarDay:
                    return GetPaletteBorderCalendarDay(CalendarDay, state);
                case PaletteBorderStyle.ButtonCluster:
                    return GetPaletteBorderButton(ButtonStyles.ButtonCluster, state);
                case PaletteBorderStyle.ButtonGallery:
                    return GetPaletteBorderButton(ButtonStyles.ButtonGallery, state);
                case PaletteBorderStyle.ButtonNavigatorStack:
                    return GetPaletteBorderButton(ButtonStyles.ButtonNavigatorStack, state);
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                    return GetPaletteBorderButton(ButtonStyles.ButtonNavigatorOverflow, state);
                case PaletteBorderStyle.ButtonNavigatorMini:
                    return GetPaletteBorderButton(ButtonStyles.ButtonNavigatorMini, state);
                case PaletteBorderStyle.ButtonInputControl:
                    return GetPaletteBorderButton(ButtonStyles.ButtonInputControl, state);
                case PaletteBorderStyle.ButtonListItem:
                    return GetPaletteBorderButton(ButtonStyles.ButtonListItem, state);
                case PaletteBorderStyle.ButtonForm:
                    return GetPaletteBorderButton(ButtonStyles.ButtonForm, state);
                case PaletteBorderStyle.ButtonFormClose:
                    return GetPaletteBorderButton(ButtonStyles.ButtonFormClose, state);
                case PaletteBorderStyle.ButtonCommand:
                    return GetPaletteBorderButton(ButtonStyles.ButtonCommand, state);
                case PaletteBorderStyle.ButtonCustom1:
                    return GetPaletteBorderButton(ButtonStyles.ButtonCustom1, state);
                case PaletteBorderStyle.ButtonCustom2:
                    return GetPaletteBorderButton(ButtonStyles.ButtonCustom2, state);
                case PaletteBorderStyle.ButtonCustom3:
                    return GetPaletteBorderButton(ButtonStyles.ButtonCustom3, state);
                case PaletteBorderStyle.ControlClient:
                    return GetPaletteBorderControl(ControlStyles.ControlClient, state);
                case PaletteBorderStyle.ControlAlternate:
                    return GetPaletteBorderControl(ControlStyles.ControlAlternate, state);
                case PaletteBorderStyle.ControlGroupBox:
                    return GetPaletteBorderControl(ControlStyles.ControlGroupBox, state);
                case PaletteBorderStyle.ControlToolTip:
                    return GetPaletteBorderControl(ControlStyles.ControlToolTip, state);
                case PaletteBorderStyle.ControlRibbon:
                    return GetPaletteBorderControl(ControlStyles.ControlRibbon, state);
                case PaletteBorderStyle.ControlRibbonAppMenu:
                    return GetPaletteBorderControl(ControlStyles.ControlRibbonAppMenu, state);
                case PaletteBorderStyle.ControlCustom1:
                    return GetPaletteBorderControl(ControlStyles.ControlCustom1, state);
                case PaletteBorderStyle.ControlCustom2:
                    return GetPaletteBorderControl(ControlStyles.ControlCustom2, state);
                case PaletteBorderStyle.ControlCustom3:
                    return GetPaletteBorderControl(ControlStyles.ControlCustom3, state);
                case PaletteBorderStyle.InputControlStandalone:
                    return GetPaletteInputControl(InputControlStyles.InputControlStandalone, state).Border;
                case PaletteBorderStyle.InputControlRibbon:
                    return GetPaletteInputControl(InputControlStyles.InputControlRibbon, state).Border;
                case PaletteBorderStyle.InputControlCustom1:
                    return GetPaletteInputControl(InputControlStyles.InputControlCustom1, state).Border;
                case PaletteBorderStyle.InputControlCustom2:
                    return GetPaletteInputControl(InputControlStyles.InputControlCustom2, state).Border;
                case PaletteBorderStyle.InputControlCustom3:
                    return GetPaletteInputControl(InputControlStyles.InputControlCustom3, state).Border;
                case PaletteBorderStyle.FormMain:
                    return GetPaletteBorderForm(FormStyles.FormMain, state);
                case PaletteBorderStyle.FormCustom1:
                    return GetPaletteBorderForm(FormStyles.FormCustom1, state);
                case PaletteBorderStyle.FormCustom2:
                    return GetPaletteBorderForm(FormStyles.FormCustom2, state);
                case PaletteBorderStyle.FormCustom3:
                    return GetPaletteBorderForm(FormStyles.FormCustom3, state);
                case PaletteBorderStyle.GridHeaderColumnList:
                    return GetPaletteBorderGridHeaderColumn(GridStyles.GridList, state);
                case PaletteBorderStyle.GridHeaderColumnSheet:
                    return GetPaletteBorderGridHeaderColumn(GridStyles.GridSheet, state);
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                    return GetPaletteBorderGridHeaderColumn(GridStyles.GridCustom1, state);
                case PaletteBorderStyle.GridHeaderColumnCustom2:
                    return GetPaletteBorderGridHeaderColumn(GridStyles.GridCustom2, state);
                case PaletteBorderStyle.GridHeaderColumnCustom3:
                    return GetPaletteBorderGridHeaderColumn(GridStyles.GridCustom3, state);
                case PaletteBorderStyle.GridHeaderRowList:
                    return GetPaletteBorderGridHeaderRow(GridStyles.GridList, state);
                case PaletteBorderStyle.GridHeaderRowSheet:
                    return GetPaletteBorderGridHeaderRow(GridStyles.GridSheet, state);
                case PaletteBorderStyle.GridHeaderRowCustom1:
                    return GetPaletteBorderGridHeaderRow(GridStyles.GridCustom1, state);
                case PaletteBorderStyle.GridHeaderRowCustom2:
                    return GetPaletteBorderGridHeaderRow(GridStyles.GridCustom2, state);
                case PaletteBorderStyle.GridHeaderRowCustom3:
                    return GetPaletteBorderGridHeaderRow(GridStyles.GridCustom3, state);
                case PaletteBorderStyle.GridDataCellList:
                    return GetPaletteBorderGridDataCell(GridStyles.GridList, state);
                case PaletteBorderStyle.GridDataCellSheet:
                    return GetPaletteBorderGridDataCell(GridStyles.GridSheet, state);
                case PaletteBorderStyle.GridDataCellCustom1:
                    return GetPaletteBorderGridDataCell(GridStyles.GridCustom1, state);
                case PaletteBorderStyle.GridDataCellCustom2:
                    return GetPaletteBorderGridDataCell(GridStyles.GridCustom2, state);
                case PaletteBorderStyle.GridDataCellCustom3:
                    return GetPaletteBorderGridDataCell(GridStyles.GridCustom3, state);
                case PaletteBorderStyle.HeaderPrimary:
                    return GetPaletteBorderHeader(HeaderStyles.HeaderPrimary, state);
                case PaletteBorderStyle.HeaderSecondary:
                    return GetPaletteBorderHeader(HeaderStyles.HeaderSecondary, state);
                case PaletteBorderStyle.HeaderDockInactive:
                    return GetPaletteBorderHeader(HeaderStyles.HeaderDockInactive, state);
                case PaletteBorderStyle.HeaderDockActive:
                    return GetPaletteBorderHeader(HeaderStyles.HeaderDockActive, state);
                case PaletteBorderStyle.HeaderCalendar:
                    return GetPaletteBorderHeader(HeaderStyles.HeaderCalendar, state);
                case PaletteBorderStyle.HeaderForm:
                    return GetPaletteBorderHeader(HeaderStyles.HeaderForm, state);
                case PaletteBorderStyle.HeaderCustom1:
                    return GetPaletteBorderHeader(HeaderStyles.HeaderCustom1, state);
                case PaletteBorderStyle.HeaderCustom2:
                    return GetPaletteBorderHeader(HeaderStyles.HeaderCustom2, state);
                case PaletteBorderStyle.HeaderCustom3:
                    return GetPaletteBorderHeader(HeaderStyles.HeaderCustom3, state);
                case PaletteBorderStyle.SeparatorLowProfile:
                    return GetPaletteBorderSeparator(SeparatorStyles.SeparatorLowProfile, state);
                case PaletteBorderStyle.SeparatorHighProfile:
                    return GetPaletteBorderSeparator(SeparatorStyles.SeparatorHighProfile, state);
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                    return GetPaletteBorderSeparator(SeparatorStyles.SeparatorHighInternalProfile, state);
                case PaletteBorderStyle.SeparatorCustom1:
                    return GetPaletteBorderSeparator(SeparatorStyles.SeparatorCustom1, state);
                case PaletteBorderStyle.SeparatorCustom2:
                    return GetPaletteBorderSeparator(SeparatorStyles.SeparatorCustom2, state);
                case PaletteBorderStyle.SeparatorCustom3:
                    return GetPaletteBorderSeparator(SeparatorStyles.SeparatorCustom3, state);
                case PaletteBorderStyle.TabHighProfile:
                    return GetPaletteBorderTab(TabStyles.TabHighProfile, state);
                case PaletteBorderStyle.TabStandardProfile:
                    return GetPaletteBorderTab(TabStyles.TabStandardProfile, state);
                case PaletteBorderStyle.TabLowProfile:
                    return GetPaletteBorderTab(TabStyles.TabLowProfile, state);
                case PaletteBorderStyle.TabDock:
                    return GetPaletteBorderTab(TabStyles.TabDock, state);
                case PaletteBorderStyle.TabDockAutoHidden:
                    return GetPaletteBorderTab(TabStyles.TabDockAutoHidden, state);
                case PaletteBorderStyle.TabOneNote:
                    return GetPaletteBorderTab(TabStyles.TabOneNote, state);
                case PaletteBorderStyle.TabCustom1:
                    return GetPaletteBorderTab(TabStyles.TabCustom1, state);
                case PaletteBorderStyle.TabCustom2:
                    return GetPaletteBorderTab(TabStyles.TabCustom2, state);
                case PaletteBorderStyle.TabCustom3:
                    return GetPaletteBorderTab(TabStyles.TabCustom3, state);
                case PaletteBorderStyle.ContextMenuOuter:
                    return ContextMenu.StateCommon.ControlOuter.Border;
                case PaletteBorderStyle.ContextMenuInner:
                    return ContextMenu.StateCommon.ControlInner.Border;
                case PaletteBorderStyle.ContextMenuHeading:
                    return ContextMenu.StateCommon.Heading.Border;
                case PaletteBorderStyle.ContextMenuItemHighlight:
                    return GetPaletteBorderContextMenuItemHighlight(state);
                case PaletteBorderStyle.ContextMenuItemImage:
                    return GetPaletteBorderContextMenuItemImage(state);
                case PaletteBorderStyle.ContextMenuItemSplit:
                    return GetPaletteBorderContextMenuItemSplit(state);
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                    return ContextMenu.StateCommon.ItemImageColumn.Border;
                case PaletteBorderStyle.ContextMenuSeparator:
                    return ContextMenu.StateCommon.Separator.Border;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteContent GetPaletteContent(PaletteContentStyle style, PaletteState state)
        {
            // Must update the redirector values used if the palette source is used
            Common.StateCommon.ContentStyle = style;

            switch (style)
            {
                case PaletteContentStyle.ButtonStandalone:
                    return GetPaletteContentButton(ButtonStyles.ButtonStandalone, state);
                case PaletteContentStyle.ButtonAlternate:
                    return GetPaletteContentButton(ButtonStyles.ButtonAlternate, state);
                case PaletteContentStyle.ButtonLowProfile:
                    return GetPaletteContentButton(ButtonStyles.ButtonLowProfile, state);
                case PaletteContentStyle.ButtonButtonSpec:
                    return GetPaletteContentButton(ButtonStyles.ButtonButtonSpec, state);
                case PaletteContentStyle.ButtonBreadCrumb:
                    return GetPaletteContentButton(ButtonStyles.ButtonBreadCrumb, state);
                case PaletteContentStyle.ButtonCalendarDay:
                    return GetPaletteContentCalendarDay(CalendarDay, state);
                case PaletteContentStyle.ButtonCluster:
                    return GetPaletteContentButton(ButtonStyles.ButtonCluster, state);
                case PaletteContentStyle.ButtonGallery:
                    return GetPaletteContentButton(ButtonStyles.ButtonGallery, state);
                case PaletteContentStyle.ButtonNavigatorStack:
                    return GetPaletteContentButton(ButtonStyles.ButtonNavigatorStack, state);
                case PaletteContentStyle.ButtonNavigatorOverflow:
                    return GetPaletteContentButton(ButtonStyles.ButtonNavigatorOverflow, state);
                case PaletteContentStyle.ButtonNavigatorMini:
                    return GetPaletteContentButton(ButtonStyles.ButtonNavigatorMini, state);
                case PaletteContentStyle.ButtonInputControl:
                    return GetPaletteContentButton(ButtonStyles.ButtonInputControl, state);
                case PaletteContentStyle.ButtonListItem:
                    return GetPaletteContentButton(ButtonStyles.ButtonListItem, state);
                case PaletteContentStyle.ButtonForm:
                    return GetPaletteContentButton(ButtonStyles.ButtonForm, state);
                case PaletteContentStyle.ButtonFormClose:
                    return GetPaletteContentButton(ButtonStyles.ButtonFormClose, state);
                case PaletteContentStyle.ButtonCommand:
                    return GetPaletteContentButton(ButtonStyles.ButtonCommand, state);
                case PaletteContentStyle.ButtonCustom1:
                    return GetPaletteContentButton(ButtonStyles.ButtonCustom1, state);
                case PaletteContentStyle.ButtonCustom2:
                    return GetPaletteContentButton(ButtonStyles.ButtonCustom2, state);
                case PaletteContentStyle.ButtonCustom3:
                    return GetPaletteContentButton(ButtonStyles.ButtonCustom3, state);
                case PaletteContentStyle.GridHeaderColumnList:
                    return GetPaletteContentGridHeaderColumn(GridStyles.GridList, state);
                case PaletteContentStyle.GridHeaderColumnSheet:
                    return GetPaletteContentGridHeaderColumn(GridStyles.GridSheet, state);
                case PaletteContentStyle.GridHeaderColumnCustom1:
                    return GetPaletteContentGridHeaderColumn(GridStyles.GridCustom1, state);
                case PaletteContentStyle.GridHeaderColumnCustom2:
                    return GetPaletteContentGridHeaderColumn(GridStyles.GridCustom2, state);
                case PaletteContentStyle.GridHeaderColumnCustom3:
                    return GetPaletteContentGridHeaderColumn(GridStyles.GridCustom3, state);
                case PaletteContentStyle.GridHeaderRowList:
                    return GetPaletteContentGridHeaderRow(GridStyles.GridList, state);
                case PaletteContentStyle.GridHeaderRowSheet:
                    return GetPaletteContentGridHeaderRow(GridStyles.GridSheet, state);
                case PaletteContentStyle.GridHeaderRowCustom1:
                    return GetPaletteContentGridHeaderRow(GridStyles.GridCustom1, state);
                case PaletteContentStyle.GridHeaderRowCustom2:
                    return GetPaletteContentGridHeaderRow(GridStyles.GridCustom2, state);
                case PaletteContentStyle.GridHeaderRowCustom3:
                    return GetPaletteContentGridHeaderRow(GridStyles.GridCustom3, state);
                case PaletteContentStyle.GridDataCellList:
                    return GetPaletteContentGridDataCell(GridStyles.GridList, state);
                case PaletteContentStyle.GridDataCellSheet:
                    return GetPaletteContentGridDataCell(GridStyles.GridSheet, state);
                case PaletteContentStyle.GridDataCellCustom1:
                    return GetPaletteContentGridDataCell(GridStyles.GridCustom1, state);
                case PaletteContentStyle.GridDataCellCustom2:
                    return GetPaletteContentGridDataCell(GridStyles.GridCustom2, state);
                case PaletteContentStyle.GridDataCellCustom3:
                    return GetPaletteContentGridDataCell(GridStyles.GridCustom3, state);
                case PaletteContentStyle.HeaderPrimary:
                    return GetPaletteContentHeader(HeaderStyles.HeaderPrimary, state);
                case PaletteContentStyle.HeaderSecondary:
                    return GetPaletteContentHeader(HeaderStyles.HeaderSecondary, state);
                case PaletteContentStyle.HeaderDockInactive:
                    return GetPaletteContentHeader(HeaderStyles.HeaderDockInactive, state);
                case PaletteContentStyle.HeaderDockActive:
                    return GetPaletteContentHeader(HeaderStyles.HeaderDockActive, state);
                case PaletteContentStyle.HeaderCalendar:
                    return GetPaletteContentHeader(HeaderStyles.HeaderCalendar, state);
                case PaletteContentStyle.HeaderForm:
                    return GetPaletteContentHeader(HeaderStyles.HeaderForm, state);
                case PaletteContentStyle.HeaderCustom1:
                    return GetPaletteContentHeader(HeaderStyles.HeaderCustom1, state);
                case PaletteContentStyle.HeaderCustom2:
                    return GetPaletteContentHeader(HeaderStyles.HeaderCustom2, state);
                case PaletteContentStyle.HeaderCustom3:
                    return GetPaletteContentHeader(HeaderStyles.HeaderCustom3, state);
                case PaletteContentStyle.LabelNormalControl:
                    return GetPaletteLabel(LabelStyles.LabelNormalControl, state);
                case PaletteContentStyle.LabelBoldControl:
                    return GetPaletteLabel(LabelStyles.LabelBoldControl, state);
                case PaletteContentStyle.LabelItalicControl:
                    return GetPaletteLabel(LabelStyles.LabelItalicControl, state);
                case PaletteContentStyle.LabelTitleControl:
                    return GetPaletteLabel(LabelStyles.LabelTitleControl, state);
                case PaletteContentStyle.LabelNormalPanel:
                    return GetPaletteLabel(LabelStyles.LabelNormalPanel, state);
                case PaletteContentStyle.LabelBoldPanel:
                    return GetPaletteLabel(LabelStyles.LabelBoldPanel, state);
                case PaletteContentStyle.LabelItalicPanel:
                    return GetPaletteLabel(LabelStyles.LabelItalicPanel, state);
                case PaletteContentStyle.LabelTitlePanel:
                    return GetPaletteLabel(LabelStyles.LabelTitlePanel, state);
                case PaletteContentStyle.LabelGroupBoxCaption:
                    return GetPaletteLabel(LabelStyles.LabelCaptionPanel, state);
                case PaletteContentStyle.LabelToolTip:
                    return GetPaletteLabel(LabelStyles.LabelToolTip, state);
                case PaletteContentStyle.LabelSuperTip:
                    return GetPaletteLabel(LabelStyles.LabelSuperTip, state);
                case PaletteContentStyle.LabelKeyTip:
                    return GetPaletteLabel(LabelStyles.LabelKeyTip, state);
                case PaletteContentStyle.LabelCustom1:
                    return GetPaletteLabel(LabelStyles.LabelCustom1, state);
                case PaletteContentStyle.LabelCustom2:
                    return GetPaletteLabel(LabelStyles.LabelCustom2, state);
                case PaletteContentStyle.LabelCustom3:
                    return GetPaletteLabel(LabelStyles.LabelCustom3, state);
                case PaletteContentStyle.InputControlStandalone:
                    return GetPaletteInputControl(InputControlStyles.InputControlStandalone, state).Content;
                case PaletteContentStyle.InputControlRibbon:
                    return GetPaletteInputControl(InputControlStyles.InputControlRibbon, state).Content;
                case PaletteContentStyle.InputControlCustom1:
                    return GetPaletteInputControl(InputControlStyles.InputControlCustom1, state).Content;
                case PaletteContentStyle.InputControlCustom2:
                    return GetPaletteInputControl(InputControlStyles.InputControlCustom2, state).Content;
                case PaletteContentStyle.InputControlCustom3:
                    return GetPaletteInputControl(InputControlStyles.InputControlCustom3, state).Content;
                case PaletteContentStyle.TabHighProfile:
                    return GetPaletteContentTab(TabStyles.TabHighProfile, state);
                case PaletteContentStyle.TabStandardProfile:
                    return GetPaletteContentTab(TabStyles.TabStandardProfile, state);
                case PaletteContentStyle.TabLowProfile:
                    return GetPaletteContentTab(TabStyles.TabLowProfile, state);
                case PaletteContentStyle.TabDock:
                    return GetPaletteContentTab(TabStyles.TabDock, state);
                case PaletteContentStyle.TabDockAutoHidden:
                    return GetPaletteContentTab(TabStyles.TabDockAutoHidden, state);
                case PaletteContentStyle.TabOneNote:
                    return GetPaletteContentTab(TabStyles.TabOneNote, state);
                case PaletteContentStyle.TabCustom1:
                    return GetPaletteContentTab(TabStyles.TabCustom1, state);
                case PaletteContentStyle.TabCustom2:
                    return GetPaletteContentTab(TabStyles.TabCustom2, state);
                case PaletteContentStyle.TabCustom3:
                    return GetPaletteContentTab(TabStyles.TabCustom3, state);
                case PaletteContentStyle.ContextMenuHeading:
                    return ContextMenu.StateCommon.Heading.Content;
                case PaletteContentStyle.ContextMenuItemImage:
                    return GetPaletteContentContextMenuItemImage(state);
                case PaletteContentStyle.ContextMenuItemShortcutText:
                    return GetPaletteContentContextMenuItemShortcutText(state);
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                    return GetPaletteContentContextMenuTextAlternate(state);
                case PaletteContentStyle.ContextMenuItemTextStandard:
                    return GetPaletteContentContextMenuItemTextStandard(state);
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteBack GetPaletteBackButton(KryptonPaletteCheckButton button, PaletteState state)
        {
            // Have to special case states that do not derive from PaletteTriple
            switch (state)
            {
                case PaletteState.NormalDefaultOverride:
                    return button.OverrideDefault.Back;
                case PaletteState.FocusOverride:
                    return button.OverrideFocus.Back;
                case PaletteState.BoldedOverride:
                    return CalendarDay.OverrideBolded.Back;
                case PaletteState.TodayOverride:
                    return CalendarDay.OverrideToday.Back;
                default:
                    PaletteTriple buttonState = GetPaletteButton(button, state);
                    if (buttonState != null)
                    {
                        return buttonState.Back;
                    }
                    else
                    {
                        // Should never happen!
                        Debug.Assert(false);
                        return null;
                    }
            }
        }

        private PaletteBorder GetPaletteBorderButton(KryptonPaletteCheckButton button, PaletteState state)
        {
            // Have to special case states that do not derive from PaletteTriple
            switch (state)
            {
                case PaletteState.NormalDefaultOverride:
                    return button.OverrideDefault.Border;
                case PaletteState.FocusOverride:
                    return button.OverrideFocus.Border;
                case PaletteState.BoldedOverride:
                    return CalendarDay.OverrideBolded.Border;
                case PaletteState.TodayOverride:
                    return CalendarDay.OverrideToday.Border;
                default:
                    PaletteTriple buttonState = GetPaletteButton(button, state);
                    if (buttonState != null)
                    {
                        return buttonState.Border;
                    }
                    else
                    {
                        // Should never happen!
                        Debug.Assert(false);
                        return null;
                    }
            }
        }

        private PaletteContent GetPaletteContentButton(KryptonPaletteCheckButton button, PaletteState state)
        {
            // Have to special case states that do not derive from PaletteTriple
            switch (state)
            {
                case PaletteState.NormalDefaultOverride:
                    return button.OverrideDefault.Content;
                case PaletteState.FocusOverride:
                    return button.OverrideFocus.Content;
                case PaletteState.BoldedOverride:
                    return CalendarDay.OverrideBolded.Content;
                case PaletteState.TodayOverride:
                    return CalendarDay.OverrideToday.Content;
                default:
                    PaletteTriple buttonState = GetPaletteButton(button, state);
                    if (buttonState != null)
                    {
                        return buttonState.Content;
                    }
                    else
                    {
                        // Should never happen!
                        Debug.Assert(false);
                        return null;
                    }
            }
        }

        private PaletteTriple GetPaletteButton(KryptonPaletteCheckButton button, PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return button.StateDisabled;
                case PaletteState.Normal:
                    return button.StateNormal;
                case PaletteState.Tracking:
                    return button.StateTracking;
                case PaletteState.Pressed:
                    return button.StatePressed;
                case PaletteState.CheckedNormal:
                    return button.StateCheckedNormal;
                case PaletteState.CheckedTracking:
                    return button.StateCheckedTracking;
                case PaletteState.CheckedPressed:
                    return button.StateCheckedPressed;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteBack GetPaletteBackCalendarDay(KryptonPaletteCalendarDay button, PaletteState state)
        {
            // Have to special case states that do not derive from PaletteTriple
            switch (state)
            {
                case PaletteState.FocusOverride:
                    return button.OverrideFocus.Back;
                case PaletteState.BoldedOverride:
                    return button.OverrideBolded.Back;
                case PaletteState.TodayOverride:
                    return button.OverrideToday.Back;
                default:
                    PaletteTriple buttonState = GetPaletteCalendarDay(button, state);
                    if (buttonState != null)
                    {
                        return buttonState.Back;
                    }
                    else
                    {
                        // Should never happen!
                        Debug.Assert(false);
                        return null;
                    }
            }
        }

        private PaletteBorder GetPaletteBorderCalendarDay(KryptonPaletteCalendarDay button, PaletteState state)
        {
            // Have to special case states that do not derive from PaletteTriple
            switch (state)
            {
                case PaletteState.FocusOverride:
                    return button.OverrideFocus.Border;
                case PaletteState.BoldedOverride:
                    return button.OverrideBolded.Border;
                case PaletteState.TodayOverride:
                    return button.OverrideToday.Border;
                default:
                    PaletteTriple buttonState = GetPaletteCalendarDay(button, state);
                    if (buttonState != null)
                    {
                        return buttonState.Border;
                    }
                    else
                    {
                        // Should never happen!
                        Debug.Assert(false);
                        return null;
                    }
            }
        }

        private PaletteContent GetPaletteContentCalendarDay(KryptonPaletteCalendarDay button, PaletteState state)
        {
            // Have to special case states that do not derive from PaletteTriple
            switch (state)
            {
                case PaletteState.FocusOverride:
                    return button.OverrideFocus.Content;
                case PaletteState.BoldedOverride:
                    return button.OverrideBolded.Content;
                case PaletteState.TodayOverride:
                    return button.OverrideToday.Content;
                default:
                    PaletteTriple buttonState = GetPaletteCalendarDay(button, state);
                    if (buttonState != null)
                    {
                        return buttonState.Content;
                    }
                    else
                    {
                        // Should never happen!
                        Debug.Assert(false);
                        return null;
                    }
            }
        }

        private PaletteTriple GetPaletteCalendarDay(KryptonPaletteCalendarDay button, PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return button.StateDisabled;
                case PaletteState.Normal:
                case PaletteState.NormalDefaultOverride:
                    return button.StateNormal;
                case PaletteState.Tracking:
                    return button.StateTracking;
                case PaletteState.Pressed:
                    return button.StatePressed;
                case PaletteState.CheckedNormal:
                    return button.StateCheckedNormal;
                case PaletteState.CheckedTracking:
                    return button.StateCheckedTracking;
                case PaletteState.CheckedPressed:
                    return button.StateCheckedPressed;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private PaletteTriple GetPaletteInputControl(KryptonPaletteInputControl inputControl, PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return inputControl.StateDisabled;
                case PaletteState.Normal:
                    return inputControl.StateNormal;
                case PaletteState.Tracking:
                    return inputControl.StateActive;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteBack GetPaletteBackTab(KryptonPaletteTabButton button, PaletteState state)
        {
            // Have to special case states that do not derive from PaletteTriple
            switch (state)
            {
                case PaletteState.FocusOverride:
                    return button.OverrideFocus.Back;
                default:
                    PaletteTabTriple buttonState = GetPaletteTab(button, state);
                    if (buttonState != null)
                    {
                        return buttonState.Back;
                    }
                    else
                    {
                        // Should never happen!
                        Debug.Assert(false);
                        return null;
                    }
            }
        }

        private PaletteBorder GetPaletteBorderTab(KryptonPaletteTabButton button, PaletteState state)
        {
            // Have to special case states that do not derive from PaletteTriple
            switch (state)
            {
                case PaletteState.FocusOverride:
                    return button.OverrideFocus.Border;
                default:
                    PaletteTabTriple buttonState = GetPaletteTab(button, state);
                    if (buttonState != null)
                    {
                        return buttonState.Border;
                    }
                    else
                    {
                        // Should never happen!
                        Debug.Assert(false);
                        return null;
                    }
            }
        }

        private PaletteContent GetPaletteContentTab(KryptonPaletteTabButton button, PaletteState state)
        {
            // Have to special case states that do not derive from PaletteTriple
            switch (state)
            {
                case PaletteState.FocusOverride:
                    return button.OverrideFocus.Content;
                default:
                    PaletteTabTriple buttonState = GetPaletteTab(button, state);
                    if (buttonState != null)
                    {
                        return buttonState.Content;
                    }
                    else
                    {
                        // Should never happen!
                        Debug.Assert(false);
                        return null;
                    }
            }
        }

        private PaletteTabTriple GetPaletteTab(KryptonPaletteTabButton button, PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return button.StateDisabled;
                case PaletteState.Normal:
                    return button.StateNormal;
                case PaletteState.Tracking:
                    return button.StateTracking;
                case PaletteState.Pressed:
                    return button.StatePressed;
                case PaletteState.CheckedNormal:
                case PaletteState.CheckedTracking:
                case PaletteState.CheckedPressed:
                    return button.StateSelected;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private PaletteBack GetPaletteBackSeparator(KryptonPaletteSeparator separator, PaletteState state)
        {
            PaletteSeparatorPadding separatorState = GetPaletteSeparator(separator, state);

            if (separatorState != null)
            {
                return separatorState.Back;
            }
            else
            {
                // Should never happen!
                Debug.Assert(false);
                return null;
            }
        }

        private PaletteBorder GetPaletteBorderSeparator(KryptonPaletteSeparator separator, PaletteState state)
        {
            PaletteSeparatorPadding separatorState = GetPaletteSeparator(separator, state);

            if (separatorState != null)
            {
                return separatorState.Border;
            }
            else
            {
                // Should never happen!
                Debug.Assert(false);
                return null;
            }
        }

        private PaletteSeparatorPadding GetPaletteSeparator(KryptonPaletteSeparator separator, PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return separator.StateDisabled;
                case PaletteState.Normal:
                    return separator.StateNormal;
                case PaletteState.Tracking:
                    return separator.StateTracking;
                case PaletteState.Pressed:
                    return separator.StatePressed;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private PaletteBack GetPaletteBackControl(KryptonPaletteControl control, PaletteState state)
        {
            PaletteDouble controlState = GetPaletteControl(control, state);

            if (controlState != null)
            {
                return controlState.Back;
            }
            else
            {
                // Should never happen!
                Debug.Assert(false);
                return null;
            }
        }

        private PaletteBorder GetPaletteBorderControl(KryptonPaletteControl control, PaletteState state)
        {
            PaletteDouble controlState = GetPaletteControl(control, state);

            if (controlState != null)
            {
                return controlState.Border;
            }
            else
            {
                // Should never happen!
                Debug.Assert(false);
                return null;
            }
        }

        private PaletteDouble GetPaletteControl(KryptonPaletteControl control, PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return control.StateDisabled;
                case PaletteState.Normal:
                    return control.StateNormal;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private PaletteBack GetPaletteBackForm(KryptonPaletteForm form, PaletteState state)
        {
            PaletteDouble controlState = GetPaletteForm(form, state);

            if (controlState != null)
            {
                return controlState.Back;
            }
            else
            {
                // Should never happen!
                Debug.Assert(false);
                return null;
            }
        }

        private PaletteBack GetPaletteBackGridBackground(KryptonPaletteGrid grid, PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return grid.StateDisabled.Background;
                case PaletteState.Normal:
                    return grid.StateNormal.Background;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private PaletteBack GetPaletteBackGridHeaderColumn(KryptonPaletteGrid grid, PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return grid.StateDisabled.HeaderColumn.Back;
                case PaletteState.Normal:
                    return grid.StateNormal.HeaderColumn.Back;
                case PaletteState.Tracking:
                    return grid.StateTracking.HeaderColumn.Back;
                case PaletteState.Pressed:
                    return grid.StatePressed.HeaderColumn.Back;
                case PaletteState.CheckedNormal:
                    return grid.StateSelected.HeaderColumn.Back;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private PaletteBack GetPaletteBackGridHeaderRow(KryptonPaletteGrid grid, PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return grid.StateDisabled.HeaderRow.Back;
                case PaletteState.Normal:
                    return grid.StateNormal.HeaderRow.Back;
                case PaletteState.Tracking:
                    return grid.StateTracking.HeaderRow.Back;
                case PaletteState.Pressed:
                    return grid.StatePressed.HeaderRow.Back;
                case PaletteState.CheckedNormal:
                    return grid.StateSelected.HeaderRow.Back;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private PaletteBack GetPaletteBackGridDataCell(KryptonPaletteGrid grid, PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return grid.StateDisabled.DataCell.Back;
                case PaletteState.Normal:
                    return grid.StateNormal.DataCell.Back;
                case PaletteState.CheckedNormal:
                    return grid.StateSelected.DataCell.Back;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private PaletteBorder GetPaletteBorderGridHeaderColumn(KryptonPaletteGrid grid, PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return grid.StateDisabled.HeaderColumn.Border;
                case PaletteState.Normal:
                    return grid.StateNormal.HeaderColumn.Border;
                case PaletteState.Tracking:
                    return grid.StateTracking.HeaderColumn.Border;
                case PaletteState.Pressed:
                    return grid.StatePressed.HeaderColumn.Border;
                case PaletteState.CheckedNormal:
                    return grid.StateSelected.HeaderColumn.Border;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private PaletteBorder GetPaletteBorderGridHeaderRow(KryptonPaletteGrid grid, PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return grid.StateDisabled.HeaderRow.Border;
                case PaletteState.Normal:
                    return grid.StateNormal.HeaderRow.Border;
                case PaletteState.Tracking:
                    return grid.StateTracking.HeaderRow.Border;
                case PaletteState.Pressed:
                    return grid.StatePressed.HeaderRow.Border;
                case PaletteState.CheckedNormal:
                    return grid.StateSelected.HeaderRow.Border;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private PaletteBorder GetPaletteBorderGridDataCell(KryptonPaletteGrid grid, PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return grid.StateDisabled.DataCell.Border;
                case PaletteState.Normal:
                    return grid.StateNormal.DataCell.Border;
                case PaletteState.CheckedNormal:
                    return grid.StateSelected.DataCell.Border;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }


        private IPaletteContent GetPaletteContentGridHeaderColumn(KryptonPaletteGrid grid, PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return grid.StateDisabled.HeaderColumn.Content;
                case PaletteState.Normal:
                    return grid.StateNormal.HeaderColumn.Content;
                case PaletteState.Tracking:
                    return grid.StateTracking.HeaderColumn.Content;
                case PaletteState.Pressed:
                    return grid.StatePressed.HeaderColumn.Content;
                case PaletteState.CheckedNormal:
                    return grid.StateSelected.HeaderColumn.Content;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteContent GetPaletteContentGridHeaderRow(KryptonPaletteGrid grid, PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return grid.StateDisabled.HeaderRow.Content;
                case PaletteState.Normal:
                    return grid.StateNormal.HeaderRow.Content;
                case PaletteState.Tracking:
                    return grid.StateTracking.HeaderRow.Content;
                case PaletteState.Pressed:
                    return grid.StatePressed.HeaderRow.Content;
                case PaletteState.CheckedNormal:
                    return grid.StateSelected.HeaderRow.Content;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteContent GetPaletteContentGridDataCell(KryptonPaletteGrid grid, PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return grid.StateDisabled.DataCell.Content;
                case PaletteState.Normal:
                    return grid.StateNormal.DataCell.Content;
                case PaletteState.CheckedNormal:
                    return grid.StateSelected.DataCell.Content;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private PaletteBorder GetPaletteBorderForm(KryptonPaletteForm form, PaletteState state)
        {
            PaletteDouble controlState = GetPaletteForm(form, state);

            if (controlState != null)
            {
                return controlState.Border;
            }
            else
            {
                // Should never happen!
                Debug.Assert(false);
                return null;
            }
        }

        private PaletteDouble GetPaletteForm(KryptonPaletteForm form, PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return form.StateInactive;
                case PaletteState.Normal:
                    return form.StateActive;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private PaletteBack GetPaletteBackHeader(KryptonPaletteHeader header, PaletteState state)
        {
            PaletteTriple headerState = GetPaletteHeader(header, state);

            if (headerState != null)
            {
                return headerState.Back;
            }
            else
            {
                // Should never happen!
                Debug.Assert(false);
                return null;
            }
        }

        private PaletteBorder GetPaletteBorderHeader(KryptonPaletteHeader header, PaletteState state)
        {
            PaletteTriple headerState = GetPaletteHeader(header, state);

            if (headerState != null)
            {
                return headerState.Border;
            }
            else
            {
                // Should never happen!
                Debug.Assert(false);
                return null;
            }
        }

        private PaletteContent GetPaletteContentHeader(KryptonPaletteHeader header, PaletteState state)
        {
            PaletteTriple headerState = GetPaletteHeader(header, state);

            if (headerState != null)
            {
                return headerState.Content;
            }
            else
            {
                // Should never happen!
                Debug.Assert(false);
                return null;
            }
        }

        private PaletteTriple GetPaletteHeader(KryptonPaletteHeader header, PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return header.StateDisabled;
                case PaletteState.Normal:
                    return header.StateNormal;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private PaletteBack GetPalettePanel(KryptonPalettePanel panel, PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return panel.StateDisabled;
                case PaletteState.Normal:
                    return panel.StateNormal;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private PaletteContent GetPaletteLabel(KryptonPaletteLabel label, PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return label.StateDisabled;
                case PaletteState.Normal:
                case PaletteState.Tracking:
                case PaletteState.Pressed:
                    return label.StateNormal;
                case PaletteState.FocusOverride:
                    return label.OverrideFocus;
                case PaletteState.LinkVisitedOverride:
                    return label.OverrideVisited;
                case PaletteState.LinkNotVisitedOverride:
                    return label.OverrideNotVisited;
                case PaletteState.LinkPressedOverride:
                    return label.OverridePressed;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private PaletteBack GetPaletteBackContextMenuItemSplit(PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return ContextMenu.StateDisabled.ItemSplit.Back;
                case PaletteState.Normal:
                    return ContextMenu.StateNormal.ItemSplit.Back;
                case PaletteState.Tracking:
                    return ContextMenu.StateHighlight.ItemSplit.Back;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private PaletteBack GetPaletteBackContextMenuItemHighlight(PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return ContextMenu.StateDisabled.ItemHighlight.Back;
                case PaletteState.Normal:
                    return ContextMenu.StateNormal.ItemHighlight.Back;
                case PaletteState.Tracking:
                    return ContextMenu.StateHighlight.ItemHighlight.Back;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private PaletteBack GetPaletteBackContextMenuItemImage(PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return ContextMenu.StateDisabled.ItemImage.Back;
                case PaletteState.Normal:
                    return ContextMenu.StateNormal.ItemImage.Back;
                case PaletteState.CheckedNormal:
                    return ContextMenu.StateChecked.ItemImage.Back;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private PaletteBorder GetPaletteBorderContextMenuItemHighlight(PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return ContextMenu.StateDisabled.ItemHighlight.Border;
                case PaletteState.Normal:
                    return ContextMenu.StateNormal.ItemHighlight.Border;
                case PaletteState.Tracking:
                    return ContextMenu.StateHighlight.ItemHighlight.Border;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private PaletteBorder GetPaletteBorderContextMenuItemSplit(PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return ContextMenu.StateDisabled.ItemSplit.Border;
                case PaletteState.Normal:
                    return ContextMenu.StateNormal.ItemSplit.Border;
                case PaletteState.Tracking:
                    return ContextMenu.StateHighlight.ItemSplit.Border;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private PaletteBorder GetPaletteBorderContextMenuItemImage(PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return ContextMenu.StateDisabled.ItemImage.Border;
                case PaletteState.Normal:
                    return ContextMenu.StateNormal.ItemImage.Border;
                case PaletteState.CheckedNormal:
                    return ContextMenu.StateChecked.ItemImage.Border;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private PaletteContent GetPaletteContentContextMenuItemImage(PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return ContextMenu.StateDisabled.ItemImage.Content;
                case PaletteState.Normal:
                    return ContextMenu.StateNormal.ItemImage.Content;
                case PaletteState.CheckedNormal:
                    return ContextMenu.StateChecked.ItemImage.Content;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private PaletteContent GetPaletteContentContextMenuItemShortcutText(PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return ContextMenu.StateDisabled.ItemShortcutText;
                case PaletteState.Normal:
                    return ContextMenu.StateNormal.ItemShortcutText;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteContent GetPaletteContentContextMenuTextAlternate(PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return ContextMenu.StateDisabled.ItemTextAlternate;
                case PaletteState.Normal:
                    return ContextMenu.StateNormal.ItemTextAlternate;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteContent GetPaletteContentContextMenuItemTextStandard(PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return ContextMenu.StateDisabled.ItemTextStandard;
                case PaletteState.Normal:
                    return ContextMenu.StateNormal.ItemTextStandard;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }
        #endregion

        #region Implementation
        private void OnMenuToolStatusPaint(object sender, NeedLayoutEventArgs e)
        {
            // Only raise the need to paint if painting has not been suspended
            if (_suspendCount == 0)
            {
                // Changes to the colors of the menu/tool/status areas always 
                // need a new palette updating into the toolstrip manager.
                OnPalettePaint(this, new PaletteLayoutEventArgs(e.NeedLayout, true));
            }
        }

        private void OnNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            // Only raise the need to paint if updates have not been suspended
            if (_suspendCount == 0)
            {
                // Changing the krypton control colors does not effect the menu/tool/status areas
                OnPalettePaint(this, new PaletteLayoutEventArgs(e.NeedLayout, false));
            }
        }

        private void SetPalette(IPalette basePalette)
        {
            if (basePalette != _basePalette)
            {
                Debug.Assert(_basePalette != null);

                // Unhook from current palette events
                if (_basePalette != null)
                {
                    _basePalette.PalettePaint -= OnPalettePaint;
                    _basePalette.ButtonSpecChanged -= OnButtonSpecChanged;
                    _basePalette.BasePaletteChanged -= OnBasePaletteChanged;
                    _basePalette.BaseRendererChanged -= OnBaseRendererChanged;
                }

                // Remember the new palette
                _basePalette = basePalette;

                // Make sure the redirector passes requests onto the base palette
                _redirector.Target = _basePalette;

                // Update the color table we inherit from
                ToolMenuStatus.BaseKCT = _basePalette.ColorTable;

                // Hook to new palette events
                if (_basePalette != null)
                {
                    _basePalette.PalettePaint += OnPalettePaint;
                    _basePalette.ButtonSpecChanged += OnButtonSpecChanged;
                    _basePalette.BasePaletteChanged += OnBasePaletteChanged;
                    _basePalette.BaseRendererChanged += OnBaseRendererChanged;
                }
            }
        }

        #endregion

        #region Setters and Getters
        /// <summary>Sets the CustomisedKryptonPaletteFilePath to the value of customisedKryptonPaletteFilePathValue.</summary>
        /// <param name="customisedKryptonPaletteFilePathValue">The value of customisedKryptonPaletteFilePathValue.</param>
        public void SetCustomisedKryptonPaletteFilePath(string customisedKryptonPaletteFilePathValue) => CustomisedKryptonPaletteFilePath = customisedKryptonPaletteFilePathValue;

        /// <summary>Gets the CustomisedKryptonPaletteFilePath value.</summary>
        /// <returns>The value of customisedKryptonPaletteFilePathValue.</returns>
        public string GetCustomisedKryptonPaletteFilePath() => CustomisedKryptonPaletteFilePath;

        /// <summary>Sets the PaletteName to the value of value.</summary>
        /// <param name="value">The desired value of PaletteName.</param>
        public void SetPaletteName(string value) => PaletteName = value;

        /// <summary>Returns the value of the PaletteName.</summary>
        /// <returns>The value of the PaletteName.</returns>
        public string GetPaletteName() => PaletteName;
        #endregion

    }
}
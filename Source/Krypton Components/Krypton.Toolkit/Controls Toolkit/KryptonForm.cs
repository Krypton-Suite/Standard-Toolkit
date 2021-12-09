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
    /// Draws the window chrome using a Krypton palette.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonForm), "ToolboxBitmaps.KryptonForm.bmp")]
    [Description("Draws the window chrome using a Krypton palette.")]
    [Designer("Krypton.Toolkit.KryptonFormDesigner, Krypton.Toolkit")]
    public class KryptonForm : VisualForm,
                               IContentValues
    {
        #region Type Definitions
        /// <summary>
        /// Collection for managing ButtonSpecAny instances.
        /// </summary>
        public class FormButtonSpecCollection : ButtonSpecCollection<ButtonSpecAny>
        {
            #region Identity
            /// <summary>
            /// Initialize a new instance of the FormButtonSpecCollection class.
            /// </summary>
            /// <param name="owner">Reference to owning object.</param>
            public FormButtonSpecCollection(KryptonForm owner)
                : base(owner)
            {
            }
            #endregion
        }

        /// <summary>
        /// Collection for managing NavigatorButtonSpec instances.
        /// </summary>
        public class FormFixedButtonSpecCollection : ButtonSpecCollection<ButtonSpecFormFixed>
        {
            #region Identity
            /// <summary>
            /// Initialize a new instance of the FormFixedButtonSpecCollection class.
            /// </summary>
            /// <param name="owner">Reference to owning object.</param>
            public FormFixedButtonSpecCollection(KryptonForm owner)
                : base(owner)
            {
            }
            #endregion
        }
        #endregion

        #region Static Fields
        private static readonly Size CAPTION_ICON_SIZE = new(16, 16);
        private const int HT_CORNER = 8;
        // Drop shadow
        private const int CS_DROPSHADOW = 0x00020000;

        private const int CP_NOCLOSE_BUTTON = 0x200;
        #endregion

        #region Instance Fields

        private readonly FormFixedButtonSpecCollection _buttonSpecsFixed;
        private readonly ButtonSpecManagerDraw _buttonManager;
        private VisualPopupToolTip _visualPopupToolTip;
        private readonly ViewDrawForm _drawDocker;
        private readonly ViewDrawDocker _drawHeading;
        private readonly ViewDrawContent _drawContent;
        private readonly ViewDecoratorFixedSize _headingFixedSize;
        private readonly ViewLayoutNull _layoutNull;
        private HeaderStyle _headerStyle;
        private HeaderStyle _headerStylePrev;
        private FormWindowState _regionWindowState;
        private FormWindowState _lastWindowState;
        private string _textExtra;
        private string _oldText;
        private static bool _isInAdministratorMode;
        private bool _allowFormChrome;
        private bool _allowStatusStripMerge;
        private bool _recreateButtons;
        private bool _firstCheckView;
        private bool _lastNotNormal;
        private bool _useDropShadow;
        private bool _disableCloseButton;
        private StatusStrip _statusStrip;
        private Bitmap _cacheBitmap;
        private Icon _cacheIcon;
        private float _cornerRoundingRadius;
        private Control _activeControl;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonForm class.
        /// </summary>
        public KryptonForm()
        {
            // Default properties
            _headerStyle = HeaderStyle.Form;
            _headerStylePrev = _headerStyle;
            AllowButtonSpecToolTips = false;
            _allowFormChrome = true;
            _allowStatusStripMerge = true;
            AllowIconDisplay = true;
            _regionWindowState = FormWindowState.Normal;
            _recreateButtons = true;
            _firstCheckView = true;
            _lastNotNormal = false;

            // Create storage objects
            ButtonSpecs = new FormButtonSpecCollection(this);
            _buttonSpecsFixed = new FormFixedButtonSpecCollection(this);

            // Add the fixed set of window form buttons
            ButtonSpecMin = new ButtonSpecFormWindowMin(this);
            ButtonSpecMax = new ButtonSpecFormWindowMax(this);
            ButtonSpecClose = new ButtonSpecFormWindowClose(this);
            _buttonSpecsFixed.AddRange(new ButtonSpecFormFixed[] { ButtonSpecMin, ButtonSpecMax, ButtonSpecClose });

            // Create the palette storage
            StateCommon = new PaletteFormRedirect(Redirector, NeedPaintDelegate);
            StateInactive = new PaletteForm(StateCommon, StateCommon.Header, NeedPaintDelegate);
            StateActive = new PaletteForm(StateCommon, StateCommon.Header, NeedPaintDelegate);

            // Create a header to act as the form title bar
            _drawHeading = new ViewDrawDocker(StateActive.Header.Back,
                                              StateActive.Header.Border,
                                              StateActive.Header,
                                              PaletteMetricBool.None,
                                              PaletteMetricPadding.None,
                                              VisualOrientation.Top)
            {

                // We need the border drawn before content to allow any injected elements
                // such as the application button for the ribbon to draw over borders.
                ForceBorderFirst = true
            };

            // Content draws the text and icon inside the title bar
            _drawContent = new ViewDrawContent(StateActive.Header.Content, this, VisualOrientation.Top);
            _drawHeading.Add(_drawContent, ViewDockStyle.Fill);

            // Create a decorator so that the heading has a fixed sized and not based on content
            _headingFixedSize = new ViewDecoratorFixedSize(_drawHeading, Size.Empty);

            // Create a null element that takes up all remaining space
            _layoutNull = new ViewLayoutNull();

            // Create the root element that contains the title bar and null filler
            _drawDocker = new ViewDrawForm(StateActive.Back, StateActive.Border)
            {
                { _headingFixedSize, ViewDockStyle.Top },
                { _layoutNull, ViewDockStyle.Fill }
            };

            // Create button specification collection manager
            _buttonManager = new ButtonSpecManagerDraw(this, Redirector, ButtonSpecs, _buttonSpecsFixed,
                                                       new ViewDrawDocker[] { _drawHeading },
                                                       new IPaletteMetric[] { StateCommon.Header },
                                                       new PaletteMetricInt[] { PaletteMetricInt.HeaderButtonEdgeInsetForm },
                                                       new PaletteMetricPadding[] { PaletteMetricPadding.HeaderButtonPaddingForm },
                                                       CreateToolStripRenderer,
                                                       OnButtonManagerNeedPaint);

            // Create the manager for handling tooltips
            ToolTipManager = new ToolTipManager();
            ToolTipManager.ShowToolTip += OnShowToolTip;
            ToolTipManager.CancelToolTip += OnCancelToolTip;
            _buttonManager.ToolTipManager = ToolTipManager;

            // Hook into global static events
            KryptonManager.GlobalAllowFormChromeChanged += OnGlobalAllowFormChromeChanged;
            KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

            // Create the view manager instance
            ViewManager = new ViewManager(this, _drawDocker);

            // Set the UseDropShadow to true
            // Check OS version for compatibility (can be overriden if needed)
            if (Environment.OSVersion.Version.Major == 10)
            {
                UseDropShadow = true;
            }
            else if (Environment.OSVersion.Version.Major <= 6)
            {
                UseDropShadow = false;
            }

            //DisableCloseButton = false;

            // Set the CornerRoundingRadius to 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE', default value
            CornerRoundingRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE;

        }

        /// <summary>
        /// Releases all resources used by the Control. 
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Remove ant showing tooltip
                OnCancelToolTip(this, EventArgs.Empty);

                // Remember to pull down the manager instance
                _buttonManager.Destruct();

                // Unhook from the global static events
                KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
                KryptonManager.GlobalAllowFormChromeChanged -= OnGlobalAllowFormChromeChanged;

                // Clear down the cached bitmap
                if (_cacheBitmap != null)
                {
                    _cacheBitmap.Dispose();
                    _cacheBitmap = null;
                }

                ButtonSpecMin.Dispose();
                ButtonSpecMax.Dispose();
                ButtonSpecClose.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets or sets the extra text associated with the control.
        /// </summary>
        [Category("Appearance")]
        [Description("The extra text associated with the control.")]
        [DefaultValue("")]
        public string TextExtra
        {
            get => _textExtra;

            set
            {
                _textExtra = value;
                PerformNeedPaint(true);
            }
        }

        /// <summary>
        /// Gets and sets a value indicating if tooltips should be displayed for button specs.
        /// </summary>
        [Category("Visuals")]
        [Description("Should tooltips be displayed for button specs.")]
        [DefaultValue(false)]
        public bool AllowButtonSpecToolTips { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if custom chrome is allowed.
        /// </summary>
        [Category("Visuals")]
        [Description("Should custom chrome be allowed for this KryptonForm instance.")]
        [DefaultValue(true)]
        public bool AllowFormChrome
        {
            get => _allowFormChrome;
            set
            {
                if (_allowFormChrome != value)
                {
                    _allowFormChrome = value;

                    // Do we want to switch on/off the custom chrome?
                    UpdateCustomChromeDecision();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating if the form status strip be considered for merging into chrome.
        /// </summary>
        [Category("Visuals")]
        [Description("Should the form status strip be considered for merging into chrome.")]
        [DefaultValue(true)]
        public bool AllowStatusStripMerge
        {
            get => _allowStatusStripMerge;
            set
            {
                if (_allowStatusStripMerge != value)
                {
                    _allowStatusStripMerge = value;

                    _statusStrip?.Invalidate();

                    PerformNeedPaint(false);
                }
            }
        }

        /// <summary>
        /// Gets and sets the header style for a main form.
        /// </summary>
        [Category("Visuals")]
        [Description("Header style for a main form.")]
        [DefaultValue(typeof(HeaderStyle), "Form")]
        public HeaderStyle HeaderStyle
        {
            get => _headerStyle;

            set
            {
                if (_headerStyle != value)
                {
                    _headerStyle = value;
                    PerformNeedPaint(false);
                }
            }
        }

        /// <summary>
        /// Gets and sets the chrome group border style.
        /// </summary>
        [Category("Visuals")]
        [Description("Chrome group border style.")]
        [DefaultValue(typeof(PaletteBorderStyle), "FormMain")]
        public PaletteBorderStyle GroupBorderStyle
        {
            get => StateCommon.BorderStyle;

            set
            {
                if (StateCommon.BorderStyle != value)
                {
                    StateCommon.BorderStyle = value;
                    PerformNeedPaint(false);
                }
            }
        }

        /// <summary>
        /// Gets and sets the chrome group background style.
        /// </summary>
        [Category("Visuals")]
        [Description("Chrome group background style.")]
        [DefaultValue(typeof(PaletteBackStyle), "FormMain")]
        public PaletteBackStyle GroupBackStyle
        {
            get => StateCommon.BackStyle;

            set
            {
                if (StateCommon.BackStyle != value)
                {
                    StateCommon.BackStyle = value;
                    PerformNeedPaint(false);
                }
            }
        }

        /// <summary>
        /// Allows the use of drop shadow around the form.
        /// </summary>
        [Category("Visuals")]
        [Description("Allows the use of drop shadow around the form.")]
        [DefaultValue(true)]
        public bool UseDropShadow
        {
            get => _useDropShadow;

            set
            {
                _useDropShadow = value;

                UpdateDropShadowDraw(_useDropShadow);
            }
        }

        /// <summary>Gets or sets a value indicating whether [disable close button].</summary>
        /// <value>
        ///   <c>true</c> if [disable close button]; otherwise, <c>false</c>.</value>
        [Category("Appearance"), Description("Disables the close button."), DefaultValue(false)]
        public bool DisableCloseButton
        {
            get => _disableCloseButton;
            set
            {
                _disableCloseButton = value;
                UpdateDisableCloseButton(_disableCloseButton);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is in administrator mode.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is in administrator mode; otherwise, <c>false</c>.
        /// </value>
        [Category("Appearance"), Description("Is the user currently an administrator.")]
        public bool IsInAdministratorMode { get => _isInAdministratorMode; private set => _isInAdministratorMode = value; }

        /// <summary>
        /// Gets access to the common form appearance entries that other states can override.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining common form appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteFormRedirect StateCommon { get; }

        private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

        /// <summary>
        /// Gets access to the inactive form appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining inactive form appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteForm StateInactive { get; }

        private bool ShouldSerializeStateInactive() => !StateInactive.IsDefault;

        /// <summary>
        /// Gets access to the active form appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining active form appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteForm StateActive { get; }

        private bool ShouldSerializeStateActive() => !StateActive.IsDefault;

        /// <summary>
        /// Gets the collection of button specifications.
        /// </summary>
        [Category("Visuals")]
        [Description("Collection of button specifications.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public FormButtonSpecCollection ButtonSpecs { get; }

        /// <summary>
        /// Gets access to the minimize button spec.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ButtonSpecFormWindowMin ButtonSpecMin { get; }

        /// <summary>
        /// Gets access to the minimize button spec.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ButtonSpecFormWindowMax ButtonSpecMax { get; }

        /// <summary>
        /// Gets access to the minimize button spec.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ButtonSpecFormWindowClose ButtonSpecClose { get; }

        /// <summary>
        /// Gets and sets a value indicating if the border should be inert to changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool InertForm { get; set; }

        /// <summary>
        /// Allow an extra view element to be injected into the caption area.
        /// </summary>
        /// <param name="element">Reference to view element.</param>
        /// <param name="style">Docking style of the element.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void InjectViewElement(ViewBase element, ViewDockStyle style)
        {
            Debug.Assert(element != null);
            Debug.Assert(_drawHeading != null);

            if (!IsDisposed)
            {
                // If injecting a new fill item for the caption content area
                if (style == ViewDockStyle.Fill)
                {
                    // Incoming element must be a ViewLayoutDocker
                    if (element is ViewLayoutDocker docker)
                    {
                        // Remove the existing content
                        _drawHeading.Remove(_drawContent);

                        // Add new element and put content inside it
                        _drawHeading.Add(docker, ViewDockStyle.Fill);
                        docker.Add(_drawContent, ViewDockStyle.Fill);
                    }
                }
                else
                {
                    // Just add to the docking edge requested
                    _drawHeading.Add(element, style);
                }
            }
        }

        /// <summary>
        /// Remove a previously injected view element from the caption area.
        /// </summary>
        /// <param name="element">Reference to view element.</param>
        /// <param name="style">Docking style of the element.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void RevokeViewElement(ViewBase element, ViewDockStyle style)
        {
            Debug.Assert(element != null);

            if (!IsDisposed)
            {
                // If revoking the fill item for the caption content area
                if (style == ViewDockStyle.Fill)
                {
                    // Incoming element must be a ViewLayoutDocker
                    if (element is ViewLayoutDocker docker)
                    {
                        // Remove the existing content
                        docker.Remove(_drawContent);
                        _drawHeading.Remove(docker);

                        // Add back the original content
                        _drawHeading.Add(_drawContent, ViewDockStyle.Fill);
                    }
                }
                else
                {
                    // Just remove the specified elements
                    _drawHeading.Remove(element);
                }
            }
        }

        /// <summary>
        /// Gets and sets a value indicating if the icon is allowed to be shown.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AllowIconDisplay { get; set; }

        /// <summary>
        /// Next time a layout occurs the min/max/close buttons need recreating.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void RecreateMinMaxCloseButtons()
        {
            _recreateButtons = true;
        }

        /// <summary>
        /// Gets access to the ToolTipManager used for displaying tool tips.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolTipManager ToolTipManager { get; }

        /// <summary>
        /// Gets the current state of the window.
        /// </summary>
        /// <returns>FormWindowState enumeration value.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public FormWindowState GetWindowState()
        {
            // Get the current window style (cannot use the 
            // WindowState property as it can be slightly out of date)
            var style = PI.GetWindowLong(Handle, PI.GWL_.STYLE);

            if ((style & PI.WS_.MINIMIZE) != 0)
            {
                return FormWindowState.Minimized;
            }
            else
            {
                return (style & PI.WS_.MAXIMIZE) != 0 ? FormWindowState.Maximized : FormWindowState.Normal;
            }
        }

        /// <summary>Gets or sets the corner rounding radius.</summary>
        /// <value>The corner rounding radius.</value>
        [DefaultValue(-1), Description("Defines the corner roundness on the current window (-1 is the default look).")]
        public float CornerRoundingRadius
        {
            get => _cornerRoundingRadius;
            set
            {
                _cornerRoundingRadius = value;
                Invalidate();
            }
        }

        /// <summary>Gets or sets the active control on the container control.</summary>
        [DefaultValue(null), Description("Defines an active control for this window.")]
        public new Control ActiveControl
        {
            get => _activeControl;

            set
            {
                if (_activeControl != value)
                {
                    _activeControl = value;

                    _activeControl.Focus();
                }
                else
                {
                    _activeControl.Focus();
                }
            }
        }

        #endregion

        #region Public Chrome
        /// <summary>
        /// Perform layout on behalf of the composition element using our root element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        /// <param name="compRect">Rectangle for composition element.</param>
        public override void WindowChromeCompositionLayout(ViewLayoutContext context,
                                                           Rectangle compRect)
        {
            // Update buttons so the min/max/close and custom button 
            // specs have visible states set to the correct values. For
            // the form level buttons this means they are hidden.
            _buttonManager.RefreshButtons(true);

            // Tell the content to draw itself on a composition surface
            _drawContent.DrawContentOnComposition = true;
            _drawContent.Glowing = true;

            // Update the fixed header area to that provided
            _headingFixedSize.FixedSize = new Size(compRect.Height, compRect.Height);

            // Perform actual layout of the element tree
            ViewManager.Layout(context);
        }

        /// <summary>
        /// Perform painting on behalf of the composition element using our root element.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void WindowChromeCompositionPaint(RenderContext context)
        {
            // We do not draw background of form or header
            _drawDocker.DrawCanvas = false;
            _drawHeading.DrawCanvas = false;

            ViewManager.Paint(context);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            StateCommon.Border.Rounding = CornerRoundingRadius;

            base.OnPaint(e);
        }

        /// <summary>
        /// Gets a value indicating if the provided point is inside the minimize button.
        /// </summary>
        /// <param name="pt">Window relative point to test.</param>
        /// <returns>True if inside the button; otherwise false.</returns>
        public bool HitTestMinButton(Point pt) => _buttonManager.GetButtonRectangle(ButtonSpecMin).Contains(pt);

        /// <summary>
        /// Gets a value indicating if the provided point is inside the maximize button.
        /// </summary>
        /// <param name="pt">Window relative point to test.</param>
        /// <returns>True if inside the button; otherwise false.</returns>
        public bool HitTestMaxButton(Point pt) => _buttonManager.GetButtonRectangle(ButtonSpecMax).Contains(pt);

        /// <summary>
        /// Gets a value indicating if the provided point is inside the close button.
        /// </summary>
        /// <param name="pt">Window relative point to test.</param>
        /// <returns>True if inside the button; otherwise false.</returns>
        public bool HitTestCloseButton(Point pt) => _buttonManager.GetButtonRectangle(ButtonSpecClose).Contains(pt);

        /// <summary>
        /// Gets and sets a rectangle to treat as a custom caption area.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Rectangle CustomCaptionArea { get; set; }

        #endregion

        #region Public IContentValues
        /// <summary>
        /// Gets the image used for showing on the title bar.
        /// </summary>
        /// <param name="state">Form state.</param>
        /// <returns>Image.</returns>
        public Image GetImage(PaletteState state)
        {
            Icon displayIcon = GetDefinedIcon();

            // Has the icon to be displayed changed since the last time around?
            if (displayIcon != _cacheIcon)
            {
                // Clear down the cached bitmap
                if (_cacheBitmap != null)
                {
                    _cacheBitmap.Dispose();
                    _cacheBitmap = null;
                }

                // Clear down the cached icon
                _cacheIcon = null;
            }

            // Do we need to create a cached bitmap for the display icon?
            if ((displayIcon != null) && (_cacheBitmap == null))
            {
                // Remember the icon used to generate the cached bitmap
                _cacheIcon = displayIcon;

                try
                {
                    // Convert to a bitmap for use in drawing (get the 16x16 version if available)
                    using Icon temp = new(_cacheIcon, new Size(16, 16));
                    _cacheBitmap = temp.ToBitmap();
                }
                catch
                {
                    try
                    {
                        // Failed so we convert the Icon directly instead of trying to get a sized version first
                        _cacheBitmap = _cacheIcon.ToBitmap();
                    }
                    catch
                    {
                        //
                    }
                }

                // If the ToBitmap() fails then we might still have no bitmap for use
                if (_cacheBitmap != null)
                {
                    // If the image is not the required size, create it
                    if (_cacheBitmap.Size != CAPTION_ICON_SIZE)
                    {
                        // Create a resized version of the bitmap
                        Bitmap resizedBitmap = new(_cacheBitmap, CAPTION_ICON_SIZE);

                        // Must gracefully remove unused resources!
                        _cacheBitmap.Dispose();

                        // Cache for future access
                        _cacheBitmap = resizedBitmap;
                    }
                }
            }

            return _cacheBitmap;
        }

        /// <summary>
        /// Gets the image color that should be interpreted as transparent.
        /// </summary>
        /// <param name="state">Form state.</param>
        /// <returns>Transparent Color.</returns>
        public Color GetImageTransparentColor(PaletteState state) =>
            // We never mark any color as transparent
            Color.Empty;

        /// <summary>
        /// Gets the short text used as the main caption title.
        /// </summary>
        /// <returns>Title string.</returns>
        public string GetShortText() =>
            // Return the existing form text property.
            Text;

        /// <summary>
        /// Gets the long text used as the secondary caption title.
        /// </summary>
        /// <returns>Title string.</returns>
        public string GetLongText() => _textExtra;

        #endregion

        #region Protected Override
        /// <summary>
        /// Raises the ControlAdded event.
        /// </summary>
        /// <param name="e">An EventArgs containing event data.</param>
        protected override void OnControlAdded(ControlEventArgs e)
        {
            // Is this the type of control we need to watch?
            if (e.Control is StatusStrip strip)
            {
                // Start monitoring the status strip change in state
                MonitorStatusStrip(strip);

                // Recalc to test if status strip should be integrated
                RecalcNonClient();
            }

            base.OnControlAdded(e);
        }

        /// <summary>
        /// Raises the ControlRemoved event.
        /// </summary>
        /// <param name="e">An EventArgs containing event data.</param>
        protected override void OnControlRemoved(ControlEventArgs e)
        {
            // Is the cached reference being removed?
            if (_statusStrip == e.Control)
            {
                // Unhook from status strip events
                UnmonitorStatusStrip();

                // Recalc to test if status strip should be unintegrated
                RecalcNonClient();
            }
        }

        /// <summary>
        /// Raises the Load event.
        /// </summary>
        /// <param name="e">An EventArgs containing event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            // Let base class perform standard actions such as calculating the 
            // correct initial size and position of the window when first shown
            base.OnLoad(e);

            // We only apply custom chrome when control is already created and positioned
            UpdateCustomChromeDecision();
        }

        /// <summary>
        /// Raises the TextChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing event data.</param>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            PerformNeedPaint(true);
        }

        /// <summary>
        /// Processes a notification from palette storage of a button spec change.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An EventArgs containing event data.</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected override void OnButtonSpecChanged(object sender, EventArgs e)
        {
            Debug.Assert(e != null);

            // Validate incoming reference
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            // Recreate all the button specs with new values
            _buttonManager.RecreateButtons();
        }

        /// <summary>
        /// Called when the active state of the window changes.
        /// </summary>
        protected override void OnWindowActiveChanged()
        {
            // Update to use the correct state override values
            if (WindowActive)
            {
                _drawDocker.SetPalettes(StateActive.Back, StateActive.Border);
                _drawHeading.SetPalettes(StateActive.Header.Back, StateActive.Header.Border);
                _drawContent.SetPalette(StateActive.Header.Content);
            }
            else
            {
                _drawDocker.SetPalettes(StateInactive.Back, StateInactive.Border);
                _drawHeading.SetPalettes(StateInactive.Header.Back, StateInactive.Header.Border);
                _drawContent.SetPalette(StateInactive.Header.Content);
            }

            _drawDocker.Enabled = WindowActive;
            _drawHeading.Enabled = WindowActive;
            _drawContent.Enabled = WindowActive;

            // Only need to redraw if showing custom chrome
            if (ApplyCustomChrome)
            {
                PerformNeedPaint(false);
            }

            base.OnWindowActiveChanged();
        }

        /// <summary>
        /// Raises the PaletteChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnPaletteChanged(EventArgs e)
        {
            // Let base class switch the palette over
            base.OnPaletteChanged(e);

            // Test if we need to change the custom chrome usage
            UpdateCustomChromeDecision();
        }

        /// <summary>
        /// Occurs when the AllowFormChromeChanged event is fired for the current palette.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnAllowFormChromeChanged(object sender, EventArgs e) =>
            // Test if we need to change the custom chrome usage
            UpdateCustomChromeDecision();

        #endregion

        #region Protected Chrome
        /// <summary>
        /// Perform setup for custom chrome.
        /// </summary>
        protected override void WindowChromeStart()
        {
            // Make sure the views for the buttons are created
            if (_recreateButtons)
            {
                _buttonManager.RecreateButtons();
                _recreateButtons = false;
            }

            // Need to perform a layout
            PerformNeedPaint(true);

            base.WindowChromeStart();
        }

        /// <summary>
        /// Perform cleanup when custom chrome ending.
        /// </summary>
        protected override void WindowChromeEnd()
        {
            // Remove any region begin used to shape the form
            UpdateBorderRegion(null);

            base.WindowChromeEnd();
        }

        /// <summary>
        /// Perform hit testing.
        /// </summary>
        /// <param name="pt">Point in window coordinates.</param>
        /// <param name="composition">Are we performing composition.</param>
        /// <returns></returns>
        protected override IntPtr WindowChromeHitTest(Point pt, bool composition)
        {
            Point originalPt = pt;

            if ((CustomCaptionArea != null) && CustomCaptionArea.Contains(pt))
            {
                return (IntPtr)PI.HT.CAPTION;
            }

            if (!composition)
            {
                // Is the mouse over any of the min/max/close buttons?
                if (_buttonManager.GetButtonRectangle(ButtonSpecMin).Contains(pt) ||
                    _buttonManager.GetButtonRectangle(ButtonSpecMax).Contains(pt) ||
                    _buttonManager.GetButtonRectangle(ButtonSpecClose).Contains(pt))
                {
                    // Get the mouse controller for this button
                    ViewBase viewBase = ViewManager.Root.ViewFromPoint(pt);
                    IMouseController controller = viewBase.FindMouseController();

                    // Ensure the button shows as 'normal' state when mouse not over and pressed
                    if (controller is ButtonController buttonController)
                    {
                        buttonController.NonClientAsNormal = true;
                    }
                }
            }

            // Do not allow the caption to be moved or the border resized
            if (InertForm)
            {
                return (IntPtr)PI.HT.CLIENT;
            }

            using (ViewLayoutContext context = new(this, Renderer))
            {
                // Discover if the form icon is being displayed
                if (_drawContent.IsImageDisplayed(context))
                {
                    // Is the mouse over the image area
                    if (_drawContent.ImageRectangle(context).Contains(pt))
                    {
                        return (IntPtr)PI.HT.MENU;
                    }
                }
            }

            var borders = FormBorderStyle switch
            {
                FormBorderStyle.None or FormBorderStyle.Fixed3D or FormBorderStyle.FixedDialog or FormBorderStyle.FixedSingle or FormBorderStyle.FixedToolWindow => Padding.Empty,
                _ => WindowState == FormWindowState.Maximized ? Padding.Empty : RealWindowBorders // When maximized we do not have any borders around the client
            };

            // Restrict the top border to the same size as the left as we are using
            // the values for the size of the border hit testing for resizing the window
            // and not the size of the border for drawing purposes.
            if (borders.Top > borders.Left)
            {
                borders.Top = borders.Left;
            }

            // Get the elements that contains the mouse point
            ViewBase mouseView = ViewManager.Root.ViewFromPoint(pt);

            // Scan up the view hierarchy until a recognized element is found
            while (mouseView != null)
            {
                // Is mouse over the caption bar?
                if (mouseView == _drawHeading)
                {
                    // We give priority to the areas that are used to resize the window
                    if ((pt.X > borders.Left) &&
                        (pt.X < (Width - borders.Right)) &&
                        (pt.Y > borders.Top) &&
                        (pt.Y < (Height - borders.Bottom)))
                    {
                        return (IntPtr)PI.HT.CAPTION;
                    }
                }

                // Is mouse over one of the borders?
                if (mouseView == _drawDocker)
                {
                    // Is point over the left border?
                    if ((borders.Left > 0) && (pt.X <= borders.Left))
                    {
                        if (pt.Y <= HT_CORNER)
                        {
                            return (IntPtr)PI.HT.TOPLEFT;
                        }

                        return pt.Y >= (Height - HT_CORNER) ? (IntPtr)PI.HT.BOTTOMLEFT : (IntPtr)PI.HT.LEFT;
                    }

                    // Is point over the right border?
                    if ((borders.Right > 0) && (pt.X >= (Width - borders.Right)))
                    {
                        if (pt.Y <= HT_CORNER)
                        {
                            return (IntPtr)PI.HT.TOPRIGHT;
                        }

                        return pt.Y >= (Height - HT_CORNER) ? (IntPtr)PI.HT.BOTTOMRIGHT : (IntPtr)PI.HT.RIGHT;
                    }

                    // Is point over the bottom border?
                    if ((borders.Bottom > 0) && (pt.Y >= (Height - borders.Bottom)))
                    {
                        if (pt.X <= HT_CORNER)
                        {
                            return (IntPtr)PI.HT.BOTTOMLEFT;
                        }

                        return pt.X >= (Width - HT_CORNER) ? (IntPtr)PI.HT.BOTTOMRIGHT : (IntPtr)PI.HT.BOTTOM;
                    }

                    // Is point over the top border?
                    if ((borders.Top > 0) && (pt.Y <= borders.Top))
                    {
                        if (pt.X <= HT_CORNER)
                        {
                            return (IntPtr)PI.HT.TOPLEFT;
                        }

                        return pt.X >= (Width - HT_CORNER) ? (IntPtr)PI.HT.TOPRIGHT : (IntPtr)PI.HT.TOP;
                    }
                }

                // Mouse up another level
                mouseView = mouseView.Parent;
            }

            return base.WindowChromeHitTest(originalPt, composition);
        }

        /// <summary>
        /// Perform painting of the window chrome.
        /// </summary>
        /// <param name="g">Graphics instance to use for drawing.</param>
        /// <param name="bounds">Bounds enclosing the window chrome.</param>
        protected override void WindowChromePaint(Graphics g, Rectangle bounds)
        {
            CheckViewLayout();
            PerformViewPaint(g, bounds);
        }

        /// <summary>
        /// Process the left mouse down event.
        /// </summary>
        /// <param name="pt">Window coordinate of the mouse up.</param>
        /// <returns>True if event is processed; otherwise false.</returns>
        protected override bool WindowChromeLeftMouseDown(Point pt)
        {
            // Let base class perform standard processing of the event
            var ret = base.WindowChromeLeftMouseDown(pt);

            // Has pressing down made a view active and indicated it also wants to capture mouse?
            if ((ViewManager.ActiveView != null) && ViewManager.MouseCaptured)
            {
                StartCapture(ViewManager.ActiveView);
                ret = true;
            }

            return ret;
        }
        #endregion

        #region Implementation
        private Icon GetDefinedIcon()
        {
            // Are we allowed to try and show an icon?
            if (AllowIconDisplay)
            {
                // Only some of the border styles show an icon
                switch (FormBorderStyle)
                {
                    case FormBorderStyle.Sizable:
                    case FormBorderStyle.Fixed3D:
                    case FormBorderStyle.FixedSingle:
                        // Only show icon if Form properties allow it
                        if (ShowIcon && ControlBox)
                        {
                            return Icon;
                        }

                        break;
                }
            }

            return null;
        }

        private void SetHeaderStyle(ViewDrawDocker drawDocker,
                                    PaletteTripleMetricRedirect palette,
                                    HeaderStyle style)
        {
            palette.SetStyles(style);

            switch (style)
            {
                case HeaderStyle.Primary:
                    _buttonManager.SetDockerMetrics(drawDocker, palette,
                                                    PaletteMetricInt.HeaderButtonEdgeInsetPrimary,
                                                    PaletteMetricPadding.HeaderButtonPaddingPrimary);
                    break;
                case HeaderStyle.Secondary:
                    _buttonManager.SetDockerMetrics(drawDocker, palette,
                                                    PaletteMetricInt.HeaderButtonEdgeInsetSecondary,
                                                    PaletteMetricPadding.HeaderButtonPaddingSecondary);
                    break;
                case HeaderStyle.DockActive:
                    _buttonManager.SetDockerMetrics(drawDocker, palette,
                                                    PaletteMetricInt.HeaderButtonEdgeInsetDockActive,
                                                    PaletteMetricPadding.HeaderButtonPaddingDockActive);
                    break;
                case HeaderStyle.DockInactive:
                    _buttonManager.SetDockerMetrics(drawDocker, palette,
                                                    PaletteMetricInt.HeaderButtonEdgeInsetDockInactive,
                                                    PaletteMetricPadding.HeaderButtonPaddingDockInactive);
                    break;
                case HeaderStyle.Form:
                    _buttonManager.SetDockerMetrics(drawDocker, palette,
                                                    PaletteMetricInt.HeaderButtonEdgeInsetForm,
                                                    PaletteMetricPadding.HeaderButtonPaddingForm);
                    break;
                case HeaderStyle.Calendar:
                    _buttonManager.SetDockerMetrics(drawDocker, palette,
                                                    PaletteMetricInt.HeaderButtonEdgeInsetCalendar,
                                                    PaletteMetricPadding.HeaderButtonPaddingCalendar);
                    break;
                case HeaderStyle.Custom1:
                    _buttonManager.SetDockerMetrics(drawDocker, palette,
                                                    PaletteMetricInt.HeaderButtonEdgeInsetCustom1,
                                                    PaletteMetricPadding.HeaderButtonPaddingCustom1);
                    break;
                case HeaderStyle.Custom2:
                    _buttonManager.SetDockerMetrics(drawDocker, palette,
                                                    PaletteMetricInt.HeaderButtonEdgeInsetCustom2,
                                                    PaletteMetricPadding.HeaderButtonPaddingCustom2);
                    break;
                case HeaderStyle.Custom3:
                    _buttonManager.SetDockerMetrics(drawDocker, palette,
                        PaletteMetricInt.HeaderButtonEdgeInsetCustom3,
                        PaletteMetricPadding.HeaderButtonPaddingCustom3);
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }
        }

        private bool CheckViewLayout()
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed && !Disposing)
            {
                // Do we have a manager to use for laying out?
                if (ViewManager != null)
                {
                    // Make sure the max/restore setting is correct
                    ButtonSpecMax.ButtonSpecType = WindowState == FormWindowState.Maximized
                        ? PaletteButtonSpecStyle.FormRestore
                        : PaletteButtonSpecStyle.FormMax;

                    // Make sure the min/restore setting is correct
                    if (WindowState == FormWindowState.Minimized)
                    {
                        ButtonSpecMin.ButtonSpecType = PaletteButtonSpecStyle.FormRestore;
                        _drawDocker.StatusStrip = null;
                    }
                    else
                    {
                        ButtonSpecMin.ButtonSpecType = PaletteButtonSpecStyle.FormMin;

                        // Make sure the top level form docker has the status strip being merged
                        _drawDocker.StatusStrip = StatusStripMerging ? _statusStrip : null;
                    }

                    // Recreate buttons to get latest state
                    _buttonManager.RefreshButtons();

                    // Never set the header style unless it has changed, as it causes a relayout
                    if (_headerStyle != _headerStylePrev)
                    {
                        // Ensure the header style matches the form border style
                        SetHeaderStyle(_drawHeading, StateCommon.Header, _headerStyle);

                        // Remember last header style set
                        _headerStylePrev = _headerStyle;
                    }

                    // Update the heading to have a height matching the window requirements
                    Padding windowBorders = RealWindowBorders;
                    _headingFixedSize.FixedSize = new Size(windowBorders.Top, windowBorders.Top);

                    // The content is definitely not being drawn on a composition
                    _drawContent.DrawContentOnComposition = false;
                    _drawContent.Glowing = false;

                    // A change in window state since last time requires a layout
                    if (_lastWindowState != GetWindowState())
                    {
                        _lastWindowState = GetWindowState();
                        NeedLayout = true;
                    }

                    // Text can change because of a minimized/maximized MDI child so need
                    // to watch out for the change and ensure a layout occurs
                    if (_oldText != GetShortText())
                    {
                        _oldText = GetShortText();
                        NeedLayout = true;
                    }

                    // If any of the buttons are tracking or pressed then need to layout
                    if (!NeedLayout)
                    {
                        var notNormal = false;
                        foreach (ButtonSpecView bsv in _buttonManager.ButtonSpecViews)
                        {
                            switch (bsv.ViewButton.State)
                            {
                                case PaletteState.Tracking:
                                case PaletteState.Pressed:
                                    notNormal = true;
                                    break;
                            }
                        }

                        if (_lastNotNormal != notNormal)
                        {
                            _lastNotNormal = notNormal;
                            NeedLayout = true;
                        }
                    }

                    // Is a layout required?
                    if (NeedLayout || (GetDefinedIcon() != _cacheIcon))
                    {
                        // Ask the view to perform a layout
                        using (ViewLayoutContext context = new(ViewManager,
                                                                                 this,
                                                                                 RealWindowRectangle,
                                                                                 Renderer))
                        {
                            ViewManager.Layout(context);
                        }

                        // Layout not needed until next indicated
                        NeedLayout = false;

                        // If in the maximized state we manually create the region
                        if (GetWindowState() == FormWindowState.Maximized)
                        {
                            UpdateRegionForMaximized();
                        }
                        else
                        {
                            // Track the window state at the time the region is created
                            _regionWindowState = WindowState;

                            // Get the path for the border so we can shape the form using it
                            using RenderContext context = new(this, null, Bounds, Renderer);
                            using GraphicsPath path = _drawDocker.GetOuterBorderPath(context);
                            if (!_firstCheckView)
                            {
                                SuspendPaint();
                            }

                            UpdateBorderRegion(path != null ? new Region(path) : null);

                            if (!_firstCheckView)
                            {
                                ResumePaint();
                            }
                        }

                        // Next time around we suspend/resume the drawing
                        _firstCheckView = false;
                        return true;
                    }
                }
            }

            return false;
        }

        private void PerformViewPaint(Graphics g, Rectangle rect)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed && !Disposing && (ViewManager != null))
            {
                // If we notice we have become maximized but the layout has not updated for
                // the maximized state then we need to update the region ourself right now
                if ((GetWindowState() == FormWindowState.Maximized) &&
                    (_regionWindowState != FormWindowState.Maximized))
                {
                    UpdateRegionForMaximized();
                }

                // We draw the main form and header background
                _drawDocker.DrawCanvas = true;
                _drawHeading.DrawCanvas = true;

                // Perform actual painting of the view
                ViewManager.Paint(Renderer, new PaintEventArgs(g, rect));
            }
        }

        private void UpdateRegionForMaximized()
        {
            if (MdiParent == null)
            {
                // Get the size of each window border
                Padding padding = RealWindowBorders;

                // Reduce the Bounds by the padding on all but the top
                Rectangle maximizedRect = new(padding.Left, padding.Left,
                                                        Width - padding.Horizontal,
                                                        Height - padding.Left - padding.Bottom);

                // Use this as the new region
                SuspendPaint();
                _regionWindowState = FormWindowState.Maximized;
                UpdateBorderRegion(new Region(maximizedRect));
                ResumePaint();
            }
            else
            {
                // As a maximized Mdi Child we do not need any border region
                UpdateBorderRegion(null);
            }
        }

        private void UpdateBorderRegion(Region newRegion)
        {
            if (newRegion == null)
            {
                return;
            }

            if (newRegion.IsEmpty(this.CreateGraphics()))
            {
                return;
            }

            // Cache the current region setting
            Region oldRegion = Region;

            // Use the new region
            Region = newRegion;

            // Cleanup old region gracefully
            oldRegion?.Dispose();
        }

        private void UpdateCustomChromeDecision()
        {
            if (IsHandleCreated)
            {
                // Decide if we should have custom chrome applied
                var needChrome = AllowFormChrome &&
                                 KryptonManager.AllowFormChrome &&
                                 (GetResolvedPalette().GetAllowFormChrome() == InheritBool.True);

                // Is there a change in custom chrome requirement?
                if (ApplyCustomChrome != needChrome)
                {
                    _recreateButtons = true;
                    _firstCheckView = true;
                    ApplyCustomChrome = needChrome;
                    PerformNeedPaint(needChrome);
                }
            }
        }

        internal bool StatusStripMerging => _allowStatusStripMerge &&
                                            _statusStrip is { Visible: true, Dock: DockStyle.Bottom } && (_statusStrip.Bottom == ClientRectangle.Bottom) && (_statusStrip.RenderMode == ToolStripRenderMode.ManagerRenderMode) && (ToolStripManager.Renderer is KryptonOffice2007Renderer or KryptonSparkleRenderer);

        private void MonitorStatusStrip(StatusStrip statusStrip)
        {
            if (_statusStrip != null)
            {
                UnmonitorStatusStrip();
            }

            // Hook into event handlers
            _statusStrip = statusStrip;
            _statusStrip.VisibleChanged += OnStatusVisibleChanged;
            _statusStrip.DockChanged += OnStatusDockChanged;
        }

        private void UnmonitorStatusStrip()
        {
            if (_statusStrip != null)
            {
                // Unhook from event handlers
                _statusStrip.VisibleChanged -= OnStatusVisibleChanged;
                _statusStrip.DockChanged -= OnStatusDockChanged;
                _statusStrip = null;
            }
        }

        private void OnShowToolTip(object sender, ToolTipEventArgs e)
        {
            if (!IsDisposed)
            {
                // Do not show tooltips when the form we are in does not have focus
                Form topForm = FindForm();
                if (topForm is { ContainsFocus: false })
                {
                    return;
                }

                // Never show tooltips are design time
                if (!DesignMode)
                {
                    IContentValues sourceContent = null;

                    // Find the button spec associated with the tooltip request
                    ButtonSpec buttonSpec = _buttonManager.ButtonSpecFromView(e.Target);

                    // If the tooltip is for a button spec
                    if (buttonSpec != null)
                    {
                        // Are we allowed to show page related tooltips
                        if (AllowButtonSpecToolTips)
                        {
                            // Create a helper object to provide tooltip values
                            ButtonSpecToContent buttonSpecMapping = new(Redirector, buttonSpec);

                            // Is there actually anything to show for the tooltip
                            if (buttonSpecMapping.HasContent)
                            {
                                sourceContent = buttonSpecMapping;
                            }
                        }
                    }

                    if (sourceContent != null)
                    {
                        // Remove any currently showing tooltip
                        _visualPopupToolTip?.Dispose();

                        // Create the actual tooltip popup object
                        _visualPopupToolTip = new VisualPopupToolTip(Redirector,
                                                                     sourceContent,
                                                                     Renderer,
                                                                     PaletteBackStyle.ControlToolTip,
                                                                     PaletteBorderStyle.ControlToolTip,
                                                                     PaletteContentStyle.LabelToolTip);

                        _visualPopupToolTip.Disposed += OnVisualPopupToolTipDisposed;

                        // Show relative to the provided screen point
                        _visualPopupToolTip.ShowCalculatingSize(e.ControlMousePosition);
                    }
                }
            }
        }

        private void OnCancelToolTip(object sender, EventArgs e) =>
            // Remove any currently showing tooltip
            _visualPopupToolTip?.Dispose();

        private void OnVisualPopupToolTipDisposed(object sender, EventArgs e)
        {
            // Unhook events from the specific instance that generated event
            VisualPopupToolTip popupToolTip = (VisualPopupToolTip)sender;
            popupToolTip.Disposed -= OnVisualPopupToolTipDisposed;

            // Not showing a popup page any more
            _visualPopupToolTip = null;
        }

        private void OnButtonManagerNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            // Only interested in optimizing specific button spec changes
            if (sender is ButtonSpecView bsView)
            {
                ButtonSpec bs = bsView.ButtonSpec;

                // Only interest in the three form level buttons, as we know 
                // these never change in size because of a paint request
                if ((bs == ButtonSpecMin) ||
                    (bs == ButtonSpecMax) ||
                    (bs == ButtonSpecClose))
                {
                    // Translate the button rectangle into the non client area
                    Rectangle buttonRect = bsView.ViewButton.ClientRectangle;
                    Padding borders = RealWindowBorders;
                    buttonRect.X -= borders.Left;
                    buttonRect.Y -= borders.Top;

                    // Only invalidate the actual button area itself
                    OnNeedPaint(sender, new NeedLayoutEventArgs(false, buttonRect));
                    return;
                }
            }

            OnNeedPaint(sender, e);
        }

        private void OnStatusDockChanged(object sender, EventArgs e)
        {
            if (StatusStripMerging)
            {
                PerformNeedPaint(false);
            }
        }

        private void OnStatusVisibleChanged(object sender, EventArgs e)
        {
            if (StatusStripMerging)
            {
                PerformNeedPaint(false);
            }
        }

        private void OnGlobalAllowFormChromeChanged(object sender, EventArgs e) => UpdateCustomChromeDecision();

        private void OnGlobalPaletteChanged(object sender, EventArgs e)
        {
            // We only care if we are using the global palette
            if (PaletteMode == PaletteMode.Global)
            {
                UpdateCustomChromeDecision();
            }
        }
        #endregion

        #region Disable Close Button
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void UpdateDisableCloseButton(bool value)
        {
            if (value)
            {
                DisableCloseButtonMethod();
            }

            Invalidate();
        }

        private void DisableCloseButtonMethod()
        {
            GetCreateParams();

            Invalidate();
        }
        #endregion

        #region Drop Shadow Methods
        /// <summary>
        /// Calls the method that draws the drop shadow around the form.
        /// </summary>
        /// <param name="useDropShadow">Use dropshadow user input value.</param>
        public void UpdateDropShadowDraw(bool useDropShadow)
        {
            if (useDropShadow)
            {
                DrawDropShadow();
            }

            Invalidate();
        }

        /// <summary>
        /// A wrapper that draws the drop shadow around the form.
        /// </summary>
        /// <returns>The shadow around the form.</returns>
        private void DrawDropShadow()
        {
            GetCreateParams();

            // Redraw
            Invalidate();
        }

        /// <summary>
        /// Test code
        /// </summary>
        /// <returns>The overrides</returns>
        private CreateParams GetCreateParams()
        {
            CreateParams cp = base.CreateParams;

            return cp;
        }

        /// <summary>
        /// Example by juverpp 
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                // add the drop shadow flag for automatically drawing
                // a drop shadow around the form
                CreateParams cp = base.CreateParams;

                if (UseDropShadow)
                {
                    cp.ClassStyle |= CS_DROPSHADOW;
                }

                //if (DisableCloseButton)
                //{
                //    cp.ClassStyle = cp.ClassStyle | CP_NOCLOSE_BUTTON;
                //}

                return cp;
            }
        }
        #endregion

        #region Admin Code
        /// <summary>
        /// Gets the has current instance got administrative rights.
        /// </summary>
        /// <returns></returns>
        public static bool GetHasCurrentInstanceGotAdministrativeRights()
        {
            try
            {
                WindowsPrincipal principal = new(WindowsIdentity.GetCurrent());

                var hasAdministrativeRights = principal.IsInRole(WindowsBuiltInRole.Administrator);

                if (hasAdministrativeRights)
                {
                    SetIsInAdministratorMode(true);

                    return true;
                }
                else
                {
                    SetIsInAdministratorMode(false);

                    return false;
                }
            }
            catch
            {
                SetIsInAdministratorMode(false);

                return false;
            }
        }

        /// <summary>Sets the is in administrator mode.</summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public static void SetIsInAdministratorMode(bool value)
        {
            KryptonForm form = new();

            //form.IsInAdministratorMode = value;
        }

        /// <summary>Gets the is in administrator mode.</summary>
        /// <returns>IsInAdministratorMode</returns>
        public static bool GetIsInAdministratorMode()
        {
            KryptonForm form = new();

            return form.IsInAdministratorMode;
        }
        #endregion
    }
}

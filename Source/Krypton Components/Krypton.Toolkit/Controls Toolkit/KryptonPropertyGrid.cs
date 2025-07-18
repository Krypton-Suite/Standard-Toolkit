#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2021 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

///<summary>A property grid control that supports the Krypton render.</summary>
/// /// <seealso cref="PropertyGrid" />
[Description(@"A property grid control that supports the Krypton render.")]
[Designer(typeof(KryptonPropertyGridDesigner))]
[ToolboxBitmap(typeof(PropertyGrid), "ToolboxBitmaps.KryptonPropertyGridVersion2.bmp")]
[ToolboxItem(true)]
public class KryptonPropertyGrid : VisualControlBase,
    IContainedInputControl
{
    #region Classes
    private class InternalPropertyGrid : PropertyGrid
    {
        #region Instance Fields
        private readonly ViewManager? _viewManager;
        private readonly KryptonPropertyGrid _kryptonPropertyGrid;
        private readonly IntPtr _screenDC;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the InternalPropertyGrid class.
        /// </summary>
        /// <param name="kryptonPropertyGrid">Reference to owning control.</param>
        public InternalPropertyGrid(KryptonPropertyGrid kryptonPropertyGrid)
        {
            SetStyle(ControlStyles.ResizeRedraw
                     | ControlStyles.AllPaintingInWmPaint
                     | ControlStyles.OptimizedDoubleBuffer, true);
            _kryptonPropertyGrid = kryptonPropertyGrid;

            // Create manager and view for drawing the background
            ViewDrawPanel = new ViewDrawPanel();
            _viewManager = new ViewManager(this, ViewDrawPanel);

            // ReSharper disable RedundantBaseQualifier
            base.Size = Size.Empty;
            //base.BorderStyle = BorderStyle.None;
            // ReSharper restore RedundantBaseQualifier

            // We need to create and cache a device context compatible with the display
            _screenDC = PI.CreateCompatibleDC(IntPtr.Zero);
            ToolStripRenderer = ToolStripManager.Renderer;
            UseCompatibleTextRendering = false;
        }

        /// <summary>
        /// Releases all resources used by the Control.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (_screenDC != IntPtr.Zero)
            {
                PI.DeleteDC(_screenDC);
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Recreate the window handle.
        /// </summary>
        public void Recreate() => RecreateHandle();

        /// <summary>
        /// Gets access to the contained view draw panel instance.
        /// </summary>
        public ViewDrawPanel ViewDrawPanel { get; }

        #endregion

        #region Protected
        /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.SystemColorsChanged" /> event.</summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnSystemColorsChanged(EventArgs e)
        {
            // DO nothing, It's Krypton Colours that are in use !
        }

        /// <summary>
        /// Raises the Layout event.
        /// </summary>
        /// <param name="levent">A LayoutEventArgs containing the event data.</param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            if (!IsHandleCreated || !Visible)
            {
                return;
            }
            base.OnLayout(levent);

            // Ask the panel to layout given our available size
            using var context =
                new ViewLayoutContext(_viewManager, this, _kryptonPropertyGrid, _kryptonPropertyGrid.Renderer);
            ViewDrawPanel.Layout(context);
        }

        /// <summary>
        /// Process Windows-based messages.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case PI.WM_.ERASEBKGND:
                    // Do not draw the background here, always do it in the paint
                    // instead to prevent flicker because of a two stage drawing process
                    break;

                case PI.WM_.PRINTCLIENT:
                case PI.WM_.PAINT:
                    WmPaint(ref m);
                    break;

                case PI.WM_.VSCROLL:
                case PI.WM_.HSCROLL:
                case PI.WM_.MOUSEWHEEL:
                    Invalidate();
                    base.WndProc(ref m);
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        #endregion

        #region Private

        private void WmPaint(ref Message m)
        {
            var ps = new PI.PAINTSTRUCT();

            // Do we need to BeginPaint or just take the given HDC?
            IntPtr hdc = m.WParam == IntPtr.Zero ? PI.BeginPaint(Handle, ref ps) : m.WParam;

            // Create bitmap that all drawing occurs onto, then we can blit it later to remove flicker
            Rectangle realRect = CommonHelper.RealClientRectangle(Handle);

            // No point drawing when one of the dimensions is zero
            if (realRect is { Width: > 0, Height: > 0 })
            {
                IntPtr hBitmap = PI.CreateCompatibleBitmap(hdc, realRect.Width, realRect.Height);

                // If we managed to get a compatible bitmap
                if (hBitmap != IntPtr.Zero)
                {
                    // Must use the screen device context for the bitmap when drawing into the
                    // bitmap otherwise the Opacity and RightToLeftLayout will not work correctly.
                    // Select the new bitmap into the screen DC
                    IntPtr oldBitmap = PI.SelectObject(_screenDC, hBitmap);

                    try
                    {

                        // Easier to draw using a graphics instance than a DC!
                        using (Graphics g = Graphics.FromHdc(_screenDC))
                        {
                            // Ask the view element to layout in given space, needs this before a render call
                            using (var context = new ViewLayoutContext(this, _kryptonPropertyGrid.Renderer))
                            {
                                context.DisplayRectangle = realRect;
                                ViewDrawPanel.Layout(context);
                            }

                            using (var context = new RenderContext(this, _kryptonPropertyGrid, g, realRect,
                                       _kryptonPropertyGrid.Renderer))
                            {
                                ViewDrawPanel.Render(context);
                            }

                            // We can only control the background color by using the built in property and not
                            // by overriding the drawing directly, therefore we can only provide a single color.
                            Color color1 = ViewDrawPanel.GetPalette().GetBackColor1(ViewDrawPanel.State);
                            if (color1 != BackColor)
                            {
                                BackColor = color1;
                            }

                            // Replace given DC with the screen DC for base window proc drawing
                            IntPtr beforeDC = m.WParam;
                            m.WParam = _screenDC;
                            DefWndProc(ref m);
                            m.WParam = beforeDC;
                        }

                        // Now blit from the bitmap from the screen to the real dc
                        PI.BitBlt(hdc, 0, 0, realRect.Width, realRect.Height, _screenDC, 0, 0, PI.SRCCOPY);
                    }
                    finally
                    {
                        // Restore the original bitmap
                        PI.SelectObject(_screenDC, oldBitmap);

                        // Delete the temporary bitmap
                        PI.DeleteObject(hBitmap);
                    }
                }
            }

            // Do we need to match the original BeginPaint?
            if (m.WParam == IntPtr.Zero)
            {
                PI.EndPaint(Handle, ref ps);
            }
        }
        #endregion
    }

    #endregion

    #region Instance Fields

    private readonly ViewDrawDocker _drawDockerOuter;
    private readonly ViewLayoutFill _layoutFill;
    private readonly InternalPropertyGrid _propertyGrid;
    private bool? _fixedActive;
    private readonly IntPtr _screenDC;
    private bool _alwaysActive;
    private bool _forcedLayout;
    private readonly KryptonContextMenuItem _resetMenuItem;

    /// <summary>Occurs before a key is pressed while the control has focus.</summary>
    [Description(@"Occurs before a key is pressed while the control has focus.")]
    public new event PreviewKeyDownEventHandler? PreviewKeyDown;

    /// <summary>Occurs when the property sort changes.</summary>
    [Description(@"Occurs when the property sort changes.")]
    public event EventHandler? PropertySortChanged;

    /// <summary>Occurs when the property tab changes.</summary>
    [Description(@"Occurs when the property tab changes.")]
    public event PropertyTabChangedEventHandler? PropertyTabChanged;

    /// <summary>Occurs when the property value changes.</summary>
    [Description(@"Occurs when the property value changes.")]
    public event PropertyValueChangedEventHandler? PropertyValueChanged;


    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonPropertyGrid" /> class.</summary>
    public KryptonPropertyGrid()
    {
        // Contains another control and needs marking as such for validation to work
        SetStyle(ControlStyles.ContainerControl, true);
        // Cannot select this control, only the child tree view and does not generate a
        SetStyle(ControlStyles.Selectable | ControlStyles.StandardClick, false);

        // Default fields
        base.Padding = new Padding(1);

        // Create the palette provider
        StateCommon = new PaletteInputControlTripleRedirect(Redirector, PaletteBackStyle.InputControlStandalone, PaletteBorderStyle.HeaderCalendar, PaletteContentStyle.LabelNormalPanel, null);
        StateDisabled = new PaletteInputControlTripleStates(StateCommon, NeedPaintDelegate);
        StateNormal = new PaletteInputControlTripleStates(StateCommon, NeedPaintDelegate);
        StateActive = new PaletteInputControlTripleStates(StateCommon, NeedPaintDelegate);

        // Create the internal list box used for containing content
        _propertyGrid = new InternalPropertyGrid(this);
        _propertyGrid.Click += OnPropertyGridClick; // SKC: make sure that the default click is also routed.
        _propertyGrid.GotFocus += OnPropertyGridGotFocus;
        _propertyGrid.LostFocus += OnPropertyGridLostFocus;
        _propertyGrid.PreviewKeyDown += OnPreviewKeyDown;
        _propertyGrid.PropertySortChanged += OnPropertySortChanged;
        _propertyGrid.PropertyTabChanged += OnPropertyTabChanged;
        //_propertyGrid.PropertyChanging += OnPropertyChanging;
        _propertyGrid.PropertyValueChanged += OnPropertyValueChanged;

        _layoutFill = new ViewLayoutFill(_propertyGrid)
        {
            DisplayPadding = new Padding(1)
        };

        // Create inner view for placing inside the drawing docker
        var drawDockerInner = new ViewLayoutDocker
        {
            { _layoutFill, ViewDockStyle.Fill }
        };

        // Create view for the control border and background
        _drawDockerOuter = new ViewDrawDocker(StateNormal.Back, StateNormal.Border)
        {
            { drawDockerInner, ViewDockStyle.Fill }
        };

        // Create the view manager instance
        ViewManager = new ViewManager(this, _drawDockerOuter);

        // We need to create and cache a device context compatible with the display
        _screenDC = PI.CreateCompatibleDC(IntPtr.Zero);

        // Add tree view to the controls collection
        ((KryptonReadOnlyControls)Controls).AddInternal(_propertyGrid);

        // Create a new KryptonContextMenu
        KryptonContextMenu = new KryptonContextMenu();

        KryptonContextMenuItems menuItems = new KryptonContextMenuItems();

        _resetMenuItem = new KryptonContextMenuItem("Reset", OnResetClick);

        menuItems.Items.Add(_resetMenuItem);

        KryptonContextMenu.Items.Add(menuItems);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (_screenDC != IntPtr.Zero)
        {
            PI.DeleteDC(_screenDC);
        }
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets access to the contained TreeView instance.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(false)]
    public PropertyGrid PropertyGrid => _propertyGrid;

    /// <summary>
    /// Gets access to the contained input control.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(false)]
    public Control ContainedControl => _propertyGrid;

    /// <summary>
    /// Gets access to the common appearance entries that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteInputControlTripleRedirect StateCommon { get; }
    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteInputControlTripleStates StateDisabled { get; }
    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteInputControlTripleStates StateNormal { get; }
    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the active appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining active appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteInputControlTripleStates StateActive { get; }
    private bool ShouldSerializeStateActive() => !StateActive.IsDefault;


    /// <summary>
    /// Gets and sets Determines if the control is always active or has focus.
    /// </summary>
    [Category(@"Visuals")]
    [Description(
        @"Determines if the control is always active or has focus.")]
    [DefaultValue(false)]
    public bool AlwaysActive
    {
        get => _alwaysActive;
        set
        {
            if (_alwaysActive != value)
            {
                _alwaysActive = value;
                Invalidate();
            }
        }
    }

    /// <summary>
    /// Sets the fixed state of the control.
    /// </summary>
    /// <param name="active">Should the control be fixed as active.</param>
    public void SetFixedState(bool active) => _fixedActive = active;

    /// <summary>
    /// Gets a value indicating if the input control is active.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsActive => _fixedActive ?? DesignMode || AlwaysActive || ContainsFocus;

    /// <summary>
    /// Sets input focus to the control.
    /// </summary>
    /// <returns>true if the input focus request was successful; otherwise, false.</returns>
    public new bool Focus() => _propertyGrid.Focus();

    /// <summary>
    /// Activates the control.
    /// </summary>
    public new void Select() => _propertyGrid.Select();
    #endregion

    #region Expose Useful parts

    /// <summary>
    ///  Returns true if the commands pane will be shown for objects
    ///  that expose verbs.
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(true)]
    [Description("the commands pane will be shown for objects")]
    public bool CommandsVisibleIfAvailable
    {
        get => _propertyGrid.CommandsVisibleIfAvailable;
        set => _propertyGrid.CommandsVisibleIfAvailable = value;
    }

    /// <summary>
    ///  Sets or gets the visibility state of the help pane.
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(true)]
    [Localizable(true)]
    [Description("visibility state of the help pane")]
    public virtual bool HelpVisible
    {
        get => _propertyGrid.HelpVisible;
        set => _propertyGrid.HelpVisible = value;
    }

    /// <summary>
    ///  Gets or sets a value that indicates whether OS-specific visual style glyphs are used for the expansion
    ///  nodes in the grid area.
    /// </summary>
    [Category("Appearance")]
    [Description("indicates whether OS-specific visual style glyphs are used for the expansion nodes in the grid area")]
    [DefaultValue(true)]
    public bool CanShowVisualStyleGlyphs
    {
        get => _propertyGrid.CanShowVisualStyleGlyphs;
        set => _propertyGrid.CanShowVisualStyleGlyphs = value;
    }

    /// <summary>
    ///  Sets or gets the current property sort type, which can be
    ///  PropertySort.Categorized or PropertySort.Alphabetical.
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(PropertySort.CategorizedAlphabetical)]
    [Description("current property sort type")]
    public PropertySort PropertySort
    {
        get => _propertyGrid.PropertySort;
        set => _propertyGrid.PropertySort = value;
    }

    internal class SelectedObjectConverter : ReferenceConverter
    {
        public SelectedObjectConverter()
            : base(typeof(IComponent))
        {
        }
    }

    /// <summary>
    ///  Sets a single object into the grid to be browsed. If multiple objects are being browsed, this property
    ///  will return the first one in the list. If no objects are selected, null is returned.
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(null)]
    [Description("")]
    [TypeConverter(typeof(SelectedObjectConverter))]
    public object? SelectedObject
    {
        get => _propertyGrid.SelectedObject;
        set => _propertyGrid.SelectedObject = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public object[] SelectedObjects
    {
        get => _propertyGrid.SelectedObjects;
        set => _propertyGrid.SelectedObjects = value;
    }

    /// <summary>Gets or sets the background color for the control.</summary>
    /// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
    [Category("Appearance")]
    [Description("ControlBackColorDescr")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
    }

    /// <summary>Gets or sets the font of the text displayed by the control.</summary>
    /// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.</returns>
    [Category("Appearance")]
    [AmbientValue(null)]
    [Description("ControlFontDescr")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public override Font Font
    {
        get => base.Font;
        set => base.Font = value;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PropertyTab SelectedTab => _propertyGrid.SelectedTab;

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DisallowNull]
    public GridItem? SelectedGridItem => _propertyGrid.SelectedGridItem;

    [Category("Appearance")]
    [Description("PropertyGridLargeButtonsDesc")]
    [DefaultValue(false)]
    public bool LargeButtons
    {
        get => _propertyGrid.LargeButtons;
        set => _propertyGrid.LargeButtons = value;
    }

    /// <summary>
    ///  Sets or gets the visibility state of the toolStrip.
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(true)]
    [Description("the visibility state of the toolStrip")]
    public virtual bool ToolbarVisible
    {
        get => _propertyGrid.ToolbarVisible;
        set => _propertyGrid.ToolbarVisible = value;
    }

    /// <summary> Collapses all the nodes in the PropertyGrid</summary>
    public void CollapseAllGridItems() => _propertyGrid.CollapseAllGridItems();

    /// <summary>Expands all the categories in the <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
    public void ExpandAllGridItems() => _propertyGrid.ExpandAllGridItems();

    /// <summary>
    ///  Refreshes the tabs of the specified <paramref name="tabScope"/>.
    /// </summary>
    /// <param name="tabScope">
    ///  Either <see cref="PropertyTabScope.Component"/> or <see cref="PropertyTabScope.Document"/>.
    /// </param>
    /// <remarks>
    ///  <para>
    ///   The <see cref="RefreshTabs(PropertyTabScope)"/> method first deletes the property tabs of the specified
    ///   scope, it then requires the objects and documents to rebuild the tabs.
    ///  </para>
    /// </remarks>
    public void RefreshTabs(PropertyTabScope tabScope) => _propertyGrid.RefreshTabs(tabScope);

    /// <summary>Resets the selected property to its default value.</summary>
    public void ResetSelectedProperty() => _propertyGrid.ResetSelectedProperty();
    #endregion

    #region Krypton

    /// <summary>Initialises the colours.</summary>
    private void UpdateStateAndPalettes()
    {
        if (!IsDisposed)
        {
            // Attempt to stop Flickering
            //PI.SendMessage(_propertyGrid.Handle, PI.WM_.SETREDRAW, IntPtr.Zero, IntPtr.Zero);

            var colorTable = KryptonManager.CurrentGlobalPalette.ColorTable;
            _propertyGrid.LineColor = colorTable.ToolStripGradientMiddle;

            _propertyGrid.CategoryForeColor = KryptonManager.CurrentGlobalPalette.ToString().Contains("DarkMode")
                ? colorTable.MenuStripText
                : colorTable.ToolStripDropDownBackground;

            var gridState = GetTripleState();
            _propertyGrid.ViewDrawPanel.SetPalettes(gridState.PaletteBack);
            _drawDockerOuter.SetPalettes(gridState.PaletteBack, gridState.PaletteBorder!);
            _drawDockerOuter.Enabled = Enabled;
            // Find the new state of the main view element
            PaletteState pState = Enabled
                ? (IsActive ? PaletteState.Tracking : PaletteState.Normal)
                : PaletteState.Disabled;
            _propertyGrid.ViewDrawPanel.ElementState = pState;
            _drawDockerOuter.ElementState = pState;

            var normalFont = gridState.PaletteContent?.GetContentShortTextFont(PaletteState.ContextNormal);
            var disabledFont = gridState.PaletteContent?.GetContentShortTextFont(PaletteState.Disabled);

            _propertyGrid.Font = (Enabled ? normalFont : disabledFont)!;
            _propertyGrid.BackColor =
                gridState.PaletteBack.GetBackColor1(Enabled ? PaletteState.Normal : PaletteState.Disabled);

            var controlsCollection = _propertyGrid.Controls;
            foreach (Control control in controlsCollection)
            {
                PaletteState state;
                IPaletteTriple triple;
                if (control.Focused)
                {
                    state = PaletteState.FocusOverride;
                    triple = StateActive;
                    control.Font = StateActive.PaletteContent?.GetContentShortTextFont(PaletteState.FocusOverride)!;
                }
                else if (control.Enabled)
                {
                    state = PaletteState.ContextNormal;
                    triple = StateNormal;
                    // Note: tobitege commented out to avoid unrecoverable exception in System.Drawing, when toggling theme back and forth
                    control.Font = normalFont!;
                }
                else
                {
                    state = PaletteState.Disabled;
                    triple = StateDisabled;
                    control.Font = disabledFont!;
                }

                control.ForeColor = triple.PaletteContent!.GetContentShortTextColor1(state);
                control.BackColor = triple.PaletteBack.GetBackColor1(state);
            }

            // Original code caused several themes to have white-on-white text.
            // This has been tested as working against all schemes and fixes all previously
            // observed white-on-white/low-contrast colors!
            // Needed to be moved below the loop!
            _propertyGrid.HelpForeColor = ContrastColor(_propertyGrid.HelpBackColor);
            _propertyGrid.ViewForeColor = ContrastColor(_propertyGrid.ViewBackColor);
            //PI.SendMessage(_propertyGrid.Handle, PI.WM_.SETREDRAW, (IntPtr)PI.BOOL.TRUE, IntPtr.Zero);
            Invalidate();
        }
    }

    private IPaletteTriple GetTripleState() => Enabled ? (IsActive ? StateActive : StateNormal) : StateDisabled;


    private static Color ContrastColor(Color color)
    {
        // Counting the perceptive luminance
        var a = 1
                - (((0.299 * color.R)
                    + ((0.587 * color.G) + (0.114 * color.B)))
                   / 255);
        var d = a < 0.5 ? 0 : 255;

        //  dark colours - white font and vice versa
        return Color.FromArgb(d, d, d);
    }

    #endregion

    #region Private

    /// <summary>
    /// Process Windows-based messages.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    protected override void WndProc(ref Message m)
    {
        switch (m.Msg)
        {
            case PI.WM_.ERASEBKGND:
                // Do not draw the background here, always do it in the paint
                // instead to prevent flicker because of a two stage drawing process
                break;

            case PI.WM_.VSCROLL:
            case PI.WM_.HSCROLL:
            case PI.WM_.MOUSEWHEEL:
                Invalidate();
                base.WndProc(ref m);
                break;

            // We need to snoop the need to show a context menu
            case PI.WM_.CONTEXTMENU:
                // Only interested in overriding the behaviour when we have a krypton context menu...
                if (KryptonContextMenu != null)
                {
                    // Extract the screen mouse position (if might not actually be provided)
                    var mousePt = new Point(PI.LOWORD(m.LParam), PI.HIWORD(m.LParam));

                    // If keyboard activated, the menu position is centered
                    if (((int)(long)m.LParam) == -1)
                    {
                        mousePt = new Point(Width / 2, Height / 2);
                    }
                    else
                    {
                        mousePt = PointToClient(mousePt);

                        // Mouse point up and left 1 pixel so that the mouse overlaps the top left corner
                        // of the showing context menu just like it happens for a ContextMenuStrip.
                        mousePt.X -= 1;
                        mousePt.Y -= 1;
                    }

                    // If the mouse position is within our client area
                    if (ClientRectangle.Contains(mousePt))
                    {
                        // Update reset menu item enabled/text state just before showing menu
                        bool canReset = CanResetCurrentProperty();
                        _resetMenuItem.Enabled = canReset;

                        // Show the context menu
                        KryptonContextMenu.Show(this, PointToScreen(mousePt));
                    }
                }
                break;

            default:
                base.WndProc(ref m);
                break;
        }
    }

    /// <summary>
    /// Raises the TabStop event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnTabStopChanged(EventArgs e)
    {
        _propertyGrid.TabStop = TabStop;
        base.OnTabStopChanged(e);
    }

    /// <summary>
    /// Raises the CausesValidationChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnCausesValidationChanged(EventArgs e)
    {
        _propertyGrid.CausesValidation = CausesValidation;
        base.OnCausesValidationChanged(e);
    }

    /// <summary>
    /// Raises the HandleCreated event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnHandleCreated(EventArgs e)
    {
        // Let base class do standard stuff
        base.OnHandleCreated(e);

        // Force the font to be set into the text box child control
        PerformNeedPaint(false);

        // We need a layout to occur before any painting
        InvokeLayout();
    }

    /// <summary>
    /// Gets the default size of the control.
    /// </summary>
    protected override Size DefaultSize => new Size(120, 96);

    /// <inheritdoc/>>
    protected override void CreateHandle()
    {
        base.CreateHandle();

        PI.SetWindowTheme(Handle, @"DarkMode_Explorer", null);
    }

    /// <summary>
    /// Force the layout logic to size and position the controls.
    /// </summary>
    protected void ForceControlLayout()
    {
        if (!IsHandleCreated)
        {
            _forcedLayout = true;
            OnLayout(new LayoutEventArgs(null, null));
            _forcedLayout = false;
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// Processes a notification from palette storage of a paint and optional layout required.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    protected override void OnNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        if (IsHandleCreated && !e.NeedLayout)
        {
            _propertyGrid.Invalidate();
        }
        else
        {
            ForceControlLayout();
        }

        // Update palette to reflect latest state
        UpdateStateAndPalettes();
        base.OnNeedPaint(sender, e);
    }

    /// <summary>
    /// Raises the Layout event.
    /// </summary>
    /// <param name="levent">An EventArgs that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
        base.OnLayout(levent);

        // Only use layout logic if control is fully initialized or if being forced
        // to allow a relayout or if in design mode.
        if (IsHandleCreated || _forcedLayout || (DesignMode))
        {
            Rectangle fillRect = _layoutFill.FillRect;
            _propertyGrid.SetBounds(fillRect.X, fillRect.Y, fillRect.Width, fillRect.Height);
        }
    }

    /// <inheritdoc />
    protected override void OnNotifyMessage(Message m)
    {
        // TODO: What is this attempting to do ?
        if (m.Msg != 0x14)
        {
            base.OnNotifyMessage(m);
        }
    }

    private void OnPropertyGridGotFocus(object? sender, EventArgs e)
    {
        OnGotFocus(e);
        UpdateStateAndPalettes();
        PerformNeedPaint(false);
        _propertyGrid.Invalidate();
    }

    private void OnPropertyGridLostFocus(object? sender, EventArgs e)
    {
        OnLostFocus(e);
        UpdateStateAndPalettes();
        PerformNeedPaint(false);
        _propertyGrid.Invalidate();
    }

    private void OnPreviewKeyDown(object? sender, PreviewKeyDownEventArgs e) => PreviewKeyDown?.Invoke(sender, e);

    private void OnPropertySortChanged(object? sender, EventArgs e) => PropertySortChanged?.Invoke(sender, e);

    private void OnPropertyTabChanged(object? sender, PropertyTabChangedEventArgs e) => PropertyTabChanged?.Invoke(sender, e);

    private void OnPropertyValueChanged(object? sender, PropertyValueChangedEventArgs e) => PropertyValueChanged?.Invoke(sender, e);

    /// <inheritdoc />
    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        UpdateStateAndPalettes();
        PerformNeedPaint(false);
        _propertyGrid.Invalidate();
    }

    /// <summary>
    /// Creates a new instance of the control collection for the KryptonTreeView.
    /// </summary>
    /// <returns>A new instance of Control.ControlCollection assigned to the control.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected override ControlCollection CreateControlsInstance() => new KryptonReadOnlyControls(this);

    /// <summary>
    /// Determine if currently selected property can be reset.
    /// </summary>
    /// <returns>True when reset is possible.</returns>
    private bool CanResetCurrentProperty()
    {
        if (_propertyGrid.SelectedGridItem is not GridItem selectedGridItem || selectedGridItem.PropertyDescriptor is null)
        {
            return false;
        }

        PropertyDescriptor prop = selectedGridItem.PropertyDescriptor;

        object[]? targets = _propertyGrid.SelectedObjects?.Length > 0
            ? _propertyGrid.SelectedObjects
            : _propertyGrid.SelectedObject is not null
                ? new[] { _propertyGrid.SelectedObject }
                : null;

        if (targets == null)
        {
            return false;
        }

        foreach (object target in targets)
        {
            if (prop.CanResetValue(target))
            {
                return true;
            }
        }

        if (prop.Attributes[typeof(DefaultValueAttribute)] is DefaultValueAttribute dva)
        {
            foreach (object target in targets)
            {
                object? current = prop.GetValue(target);
                if (!Equals(current, dva.Value))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void OnPropertyGridClick(object? sender, EventArgs e) => OnClick(e);

    private void OnResetClick(object? sender, EventArgs e)
    {
        if (_propertyGrid.SelectedGridItem is GridItem selectedGridItem && selectedGridItem.PropertyDescriptor != null)
        {
            PropertyDescriptor descriptor = selectedGridItem.PropertyDescriptor;

            DefaultValueAttribute? defaultValueAttribute = descriptor.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;

            if (defaultValueAttribute != null)
            {
                descriptor.SetValue(_propertyGrid.SelectedObject, defaultValueAttribute.Value);
            }
            else if (_propertyGrid.SelectedObject != null && descriptor.CanResetValue(_propertyGrid.SelectedObject))
            {
                descriptor.ResetValue(_propertyGrid.SelectedObject);
            }

            _propertyGrid.Refresh();
        }
    }

    #endregion
}
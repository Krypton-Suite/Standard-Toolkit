#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Displays a collection of tab pages that can be used to access multiple pages of information.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(TabControl))]
[DefaultEvent(nameof(SelectedIndexChanged))]
[DefaultProperty(nameof(TabPages))]
[DesignerCategory(@"code")]
[Description(@"Displays a collection of tab pages that can be used to access multiple pages of information.")]
public class KryptonTabControl : TabControl
{
    #region Instance Fields

    private PaletteBase? _palette;
    private PaletteMode _paletteMode;
    private PaletteRedirect? _redirector;
    private PaletteDoubleRedirect? _stateCommon;
    private PaletteDouble? _stateDisabled;
    private PaletteDouble? _stateNormal;
    private PaletteTripleRedirect? _tabStateCommon;
    private PaletteTriple? _tabStateDisabled;
    private PaletteTriple? _tabStateNormal;
    private PaletteTriple? _tabStateTracking;
    private PaletteTriple? _tabStatePressed;
    private PaletteTriple? _tabStateSelected;
    private TabStyle _tabStyle;
    private TabBorderStyle _tabBorderStyle;
    private IRenderer? _renderer;
    private int _hoveredTabIndex;
    private bool _mouseDown;
    private readonly Dictionary<int, ButtonSpecAny> _tabButtonSpecs;
    private int _hoveredButtonTabIndex;
   
    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the KryptonTabControl class.
    /// </summary>
    public KryptonTabControl()
    {
        SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);

        // Set initial palette mode
        _paletteMode = PaletteMode.Global;
        _palette = KryptonManager.CurrentGlobalPalette;

        // Create redirector to access the global palette
        _redirector = new PaletteRedirect(_palette);

        // Create the palette storage for control background
        _stateCommon = new PaletteDoubleRedirect(_redirector, PaletteBackStyle.PanelClient, PaletteBorderStyle.ControlClient, OnNeedPaint);
        _stateDisabled = new PaletteDouble(_stateCommon, OnNeedPaint);
        _stateNormal = new PaletteDouble(_stateCommon, OnNeedPaint);

        // Create the palette storage for tabs
        _tabStyle = TabStyle.StandardProfile;
        _tabBorderStyle = TabBorderStyle.SquareEqualSmall;
        _tabStateCommon = new PaletteTripleRedirect(_redirector, GetTabBackStyle(), GetTabBorderStyle(), GetTabContentStyle(), OnNeedPaint);
        _tabStateDisabled = new PaletteTriple(_tabStateCommon, OnNeedPaint);
        _tabStateNormal = new PaletteTriple(_tabStateCommon, OnNeedPaint);
        _tabStateTracking = new PaletteTriple(_tabStateCommon, OnNeedPaint);
        _tabStatePressed = new PaletteTriple(_tabStateCommon, OnNeedPaint);
        _tabStateSelected = new PaletteTriple(_tabStateCommon, OnNeedPaint);

        // Initialize renderer
        UpdateRenderer();

        // Enable OwnerDraw for custom tab rendering
        DrawMode = TabDrawMode.OwnerDrawFixed;

        // Hook into events
        DrawItem += OnDrawItem;
        MouseMove += OnMouseMove;
        MouseLeave += OnMouseLeave;
        MouseDown += OnMouseDown;
        MouseUp += OnMouseUp;

        // Hook into global palette changes
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

        // Initialize state
        _hoveredTabIndex = -1;
        _hoveredButtonTabIndex = -1;
        _mouseDown = false;
        _tabButtonSpecs = new Dictionary<int, ButtonSpecAny>();

        // Update appearance from palette
        UpdateAppearance();
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Unhook from events
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
            DrawItem -= OnDrawItem;
            MouseMove -= OnMouseMove;
            MouseLeave -= OnMouseLeave;
            MouseDown -= OnMouseDown;
            MouseUp -= OnMouseUp;

            // Clean up palette objects
            _tabStateSelected = null;
            _tabStatePressed = null;
            _tabStateTracking = null;
            _tabStateNormal = null;
            _tabStateDisabled = null;
            _tabStateCommon = null;
            _stateNormal = null;
            _stateDisabled = null;
            _stateCommon = null;
            _redirector = null;
            _palette = null;
            _renderer = null;
        }

        base.Dispose(disposing);
    }

    ~KryptonTabControl() => Dispose(false);

    #endregion

    #region Public

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Sets the palette mode.")]
    [DefaultValue(PaletteMode.Global)]
    public PaletteMode PaletteMode
    {
        get => _paletteMode;

        set
        {
            if (_paletteMode != value)
            {
                // Action depends on new value
                switch (value)
                {
                    case PaletteMode.Custom:
                        // Do nothing, you must have a palette to set
                        break;
                    default:
                        // Use the one of the built in palettes
                        _paletteMode = value;
                        _palette = KryptonManager.GetPaletteForMode(_paletteMode);
                        UpdateRedirector();
                        UpdateAppearance();
                        UpdateTabPagesPalette();
                        break;
                }
            }
        }
    }

    private bool ShouldSerializePaletteMode() => PaletteMode != PaletteMode.Global;

    private void ResetPaletteMode() => PaletteMode = PaletteMode.Global;

    /// <summary>
    /// Gets and sets the custom palette.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Sets the custom palette to be used.")]
    [DefaultValue(null)]
    public PaletteBase? Palette
    {
        get => _paletteMode == PaletteMode.Custom ? _palette : null;

        set
        {
            // Only interested in changes of value
            if (_palette != value)
            {
                // Remember new palette
                _palette = value;

                // If no custom palette provided, then must be using a built in palette
                if (value == null)
                {
                    _paletteMode = PaletteMode.Global;
                    _palette = KryptonManager.CurrentGlobalPalette;
                }
                else
                {
                    // No longer using a built in palette
                    _paletteMode = PaletteMode.Custom;
                }

                UpdateRedirector();
                UpdateAppearance();
                UpdateTabPagesPalette();
            }
        }
    }

    private bool ShouldSerializePalette() => PaletteMode == PaletteMode.Custom && _palette != null;

    private void ResetPalette()
    {
        PaletteMode = PaletteMode.Global;
        _palette = null;
    }

    /// <summary>
    /// Gets and sets the tab control background style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Tab control background style.")]
    public PaletteBackStyle TabControlBackStyle
    {
        get => _stateCommon?.BackStyle ?? PaletteBackStyle.PanelClient;

        set
        {
            if (_stateCommon != null && _stateCommon.BackStyle != value)
            {
                _stateCommon.BackStyle = value;
                UpdateAppearance();
            }
        }
    }

    private bool ShouldSerializeTabControlBackStyle() => TabControlBackStyle != PaletteBackStyle.PanelClient;

    private void ResetTabControlBackStyle() => TabControlBackStyle = PaletteBackStyle.PanelClient;

    /// <summary>
    /// Gets and sets the tab style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Tab style.")]
    [DefaultValue(TabStyle.StandardProfile)]
    public TabStyle TabStyle
    {
        get => _tabStyle;

        set
        {
            if (_tabStyle != value)
            {
                _tabStyle = value;
                if (_tabStateCommon != null)
                {
                    _tabStateCommon.BackStyle = GetTabBackStyle();
                    _tabStateCommon.BorderStyle = GetTabBorderStyle();
                    _tabStateCommon.ContentStyle = GetTabContentStyle();
                }
                // Update all tab state palettes to use new styles
                UpdateTabStateStyles();
                Invalidate();
            }
        }
    }

    private bool ShouldSerializeTabStyle() => TabStyle != TabStyle.StandardProfile;

    private void ResetTabStyle() => TabStyle = TabStyle.StandardProfile;

    /// <summary>
    /// Gets and sets the tab border style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Tab border style.")]
    [DefaultValue(TabBorderStyle.SquareEqualSmall)]
    public TabBorderStyle TabBorderStyle
    {
        get => _tabBorderStyle;

        set
        {
            if (_tabBorderStyle != value)
            {
                _tabBorderStyle = value;
                Invalidate();
            }
        }
    }

    private bool ShouldSerializeTabBorderStyle() => TabBorderStyle != TabBorderStyle.SquareEqualSmall;

    private void ResetTabBorderStyle() => TabBorderStyle = TabBorderStyle.SquareEqualSmall;

    /// <summary>
    /// Gets access to the common tab appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common tab appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect TabStateCommon => _tabStateCommon ?? throw new ObjectDisposedException(nameof(KryptonTabControl));

    private bool ShouldSerializeTabStateCommon() => _tabStateCommon != null && !_tabStateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled tab appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple TabStateDisabled => _tabStateDisabled ?? throw new ObjectDisposedException(nameof(KryptonTabControl));

    private bool ShouldSerializeTabStateDisabled() => _tabStateDisabled != null && !_tabStateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal tab appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple TabStateNormal => _tabStateNormal ?? throw new ObjectDisposedException(nameof(KryptonTabControl));

    private bool ShouldSerializeTabStateNormal() => _tabStateNormal != null && !_tabStateNormal.IsDefault;

    /// <summary>
    /// Gets access to the hot tracking tab appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining hot tracking tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple TabStateTracking => _tabStateTracking ?? throw new ObjectDisposedException(nameof(KryptonTabControl));

    private bool ShouldSerializeTabStateTracking() => _tabStateTracking != null && !_tabStateTracking.IsDefault;

    /// <summary>
    /// Gets access to the pressed tab appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple TabStatePressed => _tabStatePressed ?? throw new ObjectDisposedException(nameof(KryptonTabControl));

    private bool ShouldSerializeTabStatePressed() => _tabStatePressed != null && !_tabStatePressed.IsDefault;

    /// <summary>
    /// Gets access to the selected tab appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining selected tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple TabStateSelected => _tabStateSelected ?? throw new ObjectDisposedException(nameof(KryptonTabControl));

    private bool ShouldSerializeTabStateSelected() => _tabStateSelected != null && !_tabStateSelected.IsDefault;

    /// <summary>
    /// Gets access to the common tab control appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common tab control appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDoubleRedirect StateCommon => _stateCommon ?? throw new ObjectDisposedException(nameof(KryptonTabControl));

    private bool ShouldSerializeStateCommon() => _stateCommon != null && !_stateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled tab control appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled tab control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDouble StateDisabled => _stateDisabled ?? throw new ObjectDisposedException(nameof(KryptonTabControl));

    private bool ShouldSerializeStateDisabled() => _stateDisabled != null && !_stateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal tab control appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal tab control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDouble StateNormal => _stateNormal ?? throw new ObjectDisposedException(nameof(KryptonTabControl));

    private bool ShouldSerializeStateNormal() => _stateNormal != null && !_stateNormal.IsDefault;

    /// <summary>
    /// Fix the control to a particular palette state.
    /// </summary>
    /// <param name="state">Palette state to fix.</param>
    public virtual void SetFixedState(PaletteState state)
    {
        // Not implemented for TabControl
        // This method is provided for API consistency with other Krypton controls
    }

    /// <summary>
    /// Sets a ButtonSpec for a specific tab page.
    /// </summary>
    /// <param name="tabIndex">The index of the tab page.</param>
    /// <param name="buttonSpec">The ButtonSpec to associate with the tab, or null to remove.</param>
    public void SetTabButtonSpec(int tabIndex, ButtonSpecAny? buttonSpec)
    {
        if (tabIndex < 0 || tabIndex >= TabPages.Count)
        {
            return;
        }

        // Remove existing button spec if present
        if (_tabButtonSpecs.TryGetValue(tabIndex, out var existingSpec))
        {
            existingSpec.ButtonSpecPropertyChanged -= OnTabButtonSpecChanged;
            existingSpec.Click -= OnTabButtonSpecClick;
            _tabButtonSpecs.Remove(tabIndex);
        }

        // Add new button spec if provided
        if (buttonSpec != null)
        {
            buttonSpec.ButtonSpecPropertyChanged += OnTabButtonSpecChanged;
            buttonSpec.Click += OnTabButtonSpecClick;
            // Store tab index in button spec's Tag for later retrieval
            buttonSpec.Tag = tabIndex;
            _tabButtonSpecs[tabIndex] = buttonSpec;
        }

        // Invalidate the tab to redraw
        if (tabIndex < TabCount)
        {
            Invalidate(GetTabRect(tabIndex));
        }
    }

    /// <summary>
    /// Gets the ButtonSpec for a specific tab page.
    /// </summary>
    /// <param name="tabIndex">The index of the tab page.</param>
    /// <returns>The ButtonSpec associated with the tab, or null if none.</returns>
    public ButtonSpecAny? GetTabButtonSpec(int tabIndex) => _tabButtonSpecs.TryGetValue(tabIndex, out var spec) ? spec : null;

    /// <summary>
    /// Removes the ButtonSpec for a specific tab page.
    /// </summary>
    /// <param name="tabIndex">The index of the tab page.</param>
    public void RemoveTabButtonSpec(int tabIndex) => SetTabButtonSpec(tabIndex, null);

    #endregion

    #region Events

    /// <summary>
    /// Occurs when a tab button spec is clicked.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when a tab button spec is clicked.")]
    public event EventHandler<TabButtonSpecClickEventArgs>? TabButtonSpecClick;
    
    #endregion

    #region Protected Overrides
    
    /// <summary>
    /// Raises the PaintBackground event.
    /// </summary>
    /// <param name="e">A PaintEventArgs that contains the event data.</param>
    protected override void OnPaintBackground(PaintEventArgs e)
    {
        // Use Krypton renderer to paint the background with themed colors
        if (_renderer != null && _stateCommon != null)
        {
            // Determine the appropriate palette state
            var paletteState = Enabled ? PaletteState.Normal : PaletteState.Disabled;
            var paletteBack = Enabled ? _stateNormal?.Back : _stateDisabled?.Back;

            if (paletteBack != null && paletteBack.GetBackDraw(paletteState) == InheritBool.True)
            {
                // Create render context
                var context = new RenderContext(this, e.Graphics, e.ClipRectangle, _renderer);

                // Create a rectangle path for the background
                using var path = new GraphicsPath();
                path.AddRectangle(e.ClipRectangle);

                // Draw the background using the renderer
                using var memento = _renderer.RenderStandardBack.DrawBack(context, e.ClipRectangle, path, paletteBack, VisualOrientation.Top, paletteState, null);
            }
            else
            {
                // Fallback: paint parent background for transparency
                if (Parent != null)
                {
                    using var brush = new SolidBrush(Parent.BackColor);
                    e.Graphics.FillRectangle(brush, e.ClipRectangle);
                }
                else
                {
                    // Use system color as last resort
                    using var brush = new SolidBrush(SystemColors.Control);
                    e.Graphics.FillRectangle(brush, e.ClipRectangle);
                }
            }
        }
        else
        {
            // No renderer available, use default behavior
            if (Parent != null)
            {
                using var brush = new SolidBrush(Parent.BackColor);
                e.Graphics.FillRectangle(brush, e.ClipRectangle);
            }
            else
            {
                using var brush = new SolidBrush(SystemColors.Control);
                e.Graphics.FillRectangle(brush, e.ClipRectangle);
            }
        }
    }

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        UpdateAppearance();
    }

    /// <summary>
    /// Raises the SelectedIndexChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnSelectedIndexChanged(EventArgs e)
    {
        base.OnSelectedIndexChanged(e);
        // Update tab pages palette when selection changes (in case new pages were added)
        UpdateTabPagesPalette();
    }
    
    #endregion

    #region Implementation
    
    private void UpdateRedirector()
    {
        var currentPalette = _palette ?? KryptonManager.CurrentGlobalPalette;
        if (_redirector != null)
        {
            _redirector.Target = currentPalette;
        }
        else
        {
            _redirector = new PaletteRedirect(currentPalette);
            if (_stateCommon != null)
            {
                _stateCommon.SetRedirector(_redirector);
            }
            if (_tabStateCommon != null)
            {
                _tabStateCommon.SetRedirector(_redirector);
            }
        }
        UpdateRenderer();
    }

    private void UpdateRenderer()
    {
        var currentPalette = _palette ?? KryptonManager.CurrentGlobalPalette;
        _renderer = currentPalette?.GetRenderer();
    }

    private PaletteBackStyle GetTabBackStyle()
    {
        return _tabStyle switch
        {
            TabStyle.HighProfile => PaletteBackStyle.TabHighProfile,
            TabStyle.StandardProfile => PaletteBackStyle.TabStandardProfile,
            TabStyle.LowProfile => PaletteBackStyle.TabLowProfile,
            TabStyle.OneNote => PaletteBackStyle.TabOneNote,
            TabStyle.Dock => PaletteBackStyle.TabDock,
            TabStyle.DockAutoHidden => PaletteBackStyle.TabDockAutoHidden,
            TabStyle.Custom1 => PaletteBackStyle.TabCustom1,
            TabStyle.Custom2 => PaletteBackStyle.TabCustom2,
            TabStyle.Custom3 => PaletteBackStyle.TabCustom3,
            _ => PaletteBackStyle.TabStandardProfile
        };
    }

    private PaletteBorderStyle GetTabBorderStyle()
    {
        return _tabStyle switch
        {
            TabStyle.HighProfile => PaletteBorderStyle.TabHighProfile,
            TabStyle.StandardProfile => PaletteBorderStyle.TabStandardProfile,
            TabStyle.LowProfile => PaletteBorderStyle.TabLowProfile,
            TabStyle.OneNote => PaletteBorderStyle.TabOneNote,
            TabStyle.Dock => PaletteBorderStyle.TabDock,
            TabStyle.DockAutoHidden => PaletteBorderStyle.TabDockAutoHidden,
            TabStyle.Custom1 => PaletteBorderStyle.TabCustom1,
            TabStyle.Custom2 => PaletteBorderStyle.TabCustom2,
            TabStyle.Custom3 => PaletteBorderStyle.TabCustom3,
            _ => PaletteBorderStyle.TabStandardProfile
        };
    }

    private PaletteContentStyle GetTabContentStyle()
    {
        return _tabStyle switch
        {
            TabStyle.HighProfile => PaletteContentStyle.TabHighProfile,
            TabStyle.StandardProfile => PaletteContentStyle.TabStandardProfile,
            TabStyle.LowProfile => PaletteContentStyle.TabLowProfile,
            TabStyle.OneNote => PaletteContentStyle.TabOneNote,
            TabStyle.Dock => PaletteContentStyle.TabDock,
            TabStyle.DockAutoHidden => PaletteContentStyle.TabDockAutoHidden,
            TabStyle.Custom1 => PaletteContentStyle.TabCustom1,
            TabStyle.Custom2 => PaletteContentStyle.TabCustom2,
            TabStyle.Custom3 => PaletteContentStyle.TabCustom3,
            _ => PaletteContentStyle.TabStandardProfile
        };
    }

    private void UpdateAppearance()
    {
        if (_stateNormal == null || _stateDisabled == null)
        {
            return;
        }

        // Set background to transparent so the content area shows through
        base.BackColor = Color.Transparent;
    }

    private void UpdateTabPagesPalette()
    {
        // Update all KryptonTabPage instances to use the same palette
        foreach (TabPage tabPage in TabPages)
        {
            if (tabPage is KryptonTabPage kryptonTabPage)
            {
                kryptonTabPage.PaletteMode = PaletteMode;
                if (PaletteMode == PaletteMode.Custom)
                {
                    kryptonTabPage.Palette = Palette;
                }
            }
        }
    }

    private void UpdateTabStateStyles()
    {
        // Update all tab state palettes to use the new tab styles
        if (_tabStateCommon != null)
        {
            _tabStateCommon.BackStyle = GetTabBackStyle();
            _tabStateCommon.BorderStyle = GetTabBorderStyle();
            _tabStateCommon.ContentStyle = GetTabContentStyle();
        }
    }

    private void OnNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        UpdateAppearance();
        Invalidate();
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        // Only update if we're using the global palette
        if (_paletteMode == PaletteMode.Global)
        {
            _palette = KryptonManager.CurrentGlobalPalette;
            UpdateRedirector();
            UpdateAppearance();
            UpdateTabPagesPalette();
        }
    }

    private void OnDrawItem(object? sender, DrawItemEventArgs e)
    {
        if (e.Index < 0 || e.Index >= TabPages.Count || _renderer == null || _tabStateCommon == null)
        {
            return;
        }

        var tabPage = TabPages[e.Index];
        var isSelected = e.Index == SelectedIndex;
        var isHovered = e.Index == _hoveredTabIndex;
        var isPressed = _mouseDown && isHovered;
        var isDisabled = !Enabled || !tabPage.Enabled;

        // Determine the palette state
        PaletteState paletteState;
        PaletteTriple? tabPalette;
        switch (isDisabled)
        {
            case true:
                paletteState = PaletteState.Disabled;
                tabPalette = _tabStateDisabled;
                break;
            default:
            {
                if (isSelected)
                {
                    paletteState = PaletteState.CheckedNormal;
                    tabPalette = _tabStateSelected;
                }
                else if (isPressed)
                {
                    paletteState = PaletteState.Pressed;
                    tabPalette = _tabStatePressed;
                }
                else if (isHovered)
                {
                    paletteState = PaletteState.Tracking;
                    tabPalette = _tabStateTracking;
                }
                else
                {
                    paletteState = PaletteState.Normal;
                    tabPalette = _tabStateNormal;
                }

                break;
            }
        }

        if (tabPalette == null)
        {
            return;
        }

        // Create render context
        var context = new RenderContext(this, e.Graphics, e.Bounds, _renderer);

        // Determine orientation based on tab alignment
        var orientation = Alignment switch
        {
            TabAlignment.Top => VisualOrientation.Top,
            TabAlignment.Bottom => VisualOrientation.Bottom,
            TabAlignment.Left => VisualOrientation.Left,
            TabAlignment.Right => VisualOrientation.Right,
            _ => VisualOrientation.Top
        };

        // Get the proper tab path shape for background
        GraphicsPath? tabBackPath = null;
        if (tabPalette.Back.GetBackDraw(paletteState) == InheritBool.True)
        {
            tabBackPath = _renderer.RenderTabBorder.GetTabBackPath(context, e.Bounds, tabPalette.Border, orientation, paletteState, _tabBorderStyle);
        }

        // Draw tab background
        if (tabBackPath != null)
        {
            using (tabBackPath)
            {
                using var memento = _renderer.RenderStandardBack.DrawBack(context, e.Bounds, tabBackPath, tabPalette.Back, orientation, paletteState, null);
            }
        }

        // Draw tab border
        if (tabPalette.Border.GetBorderDraw(paletteState) == InheritBool.True)
        {
            _renderer.RenderTabBorder.DrawTabBorder(context, e.Bounds, tabPalette.Border, orientation, paletteState, _tabBorderStyle);
        }

        // Draw tab text and image
        var textRect = e.Bounds;
        textRect.Inflate(-4, -2);

        switch (tabPage.ImageIndex)
        {
            // Draw image if present
            case >= 0 when ImageList != null && tabPage.ImageIndex < ImageList.Images.Count:
            {
                var image = ImageList.Images[tabPage.ImageIndex];
                var imageRect = new Rectangle(textRect.Left, textRect.Top + (textRect.Height - image.Height) / 2, image.Width, image.Height);
                e.Graphics.DrawImage(image, imageRect);
                textRect.X += image.Width + 4;
                textRect.Width -= image.Width + 4;
                break;
            }
            default:
            {
                if (!string.IsNullOrEmpty(tabPage.ImageKey) && ImageList != null && ImageList.Images.ContainsKey(tabPage.ImageKey))
                {
                    var image = ImageList.Images[tabPage.ImageKey];
                    var imageRect = new Rectangle(textRect.Left, textRect.Top + (textRect.Height - image.Height) / 2, image.Width, image.Height);
                    e.Graphics.DrawImage(image, imageRect);
                    textRect.X += image.Width + 4;
                    textRect.Width -= image.Width + 4;
                }

                break;
            }
        }

        // Check if this tab has a button spec
        var hasButtonSpec = _tabButtonSpecs.TryGetValue(e.Index, out var buttonSpec);
        var buttonSize = 0;
        var buttonRect = Rectangle.Empty;

        if (hasButtonSpec && buttonSpec != null && buttonSpec.GetVisible(_palette ?? KryptonManager.CurrentGlobalPalette))
        {
            // Reserve space for close button (typically 16x16 pixels)
            buttonSize = 16;
            var buttonPadding = 4;
            buttonRect = new Rectangle(
                textRect.Right - buttonSize - buttonPadding,
                textRect.Top + (textRect.Height - buttonSize) / 2,
                buttonSize,
                buttonSize);

            // Adjust text rectangle to leave space for button
            textRect.Width -= buttonSize + buttonPadding + 4;
        }

        // Draw text
        if (!string.IsNullOrEmpty(tabPage.Text))
        {
            // Get text color from content palette
            var contentTextColor = tabPalette.Content.GetContentShortTextColor1(paletteState);
            if (contentTextColor == Color.Empty)
            {
                contentTextColor = ForeColor;
            }

            TextRenderer.DrawText(e.Graphics, tabPage.Text, Font, textRect, contentTextColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        }

        // Draw button spec if present
        if (hasButtonSpec && buttonSpec != null && !buttonRect.IsEmpty)
        {
            var buttonState = paletteState;
            if (e.Index == _hoveredButtonTabIndex)
            {
                buttonState = isPressed ? PaletteState.Pressed : PaletteState.Tracking;
            }

            // Draw button background if needed
            var buttonBackColor = tabPalette.Back.GetBackColor1(buttonState);
            if (buttonBackColor != Color.Empty)
            {
                using var brush = new SolidBrush(buttonBackColor);
                e.Graphics.FillRectangle(brush, buttonRect);
            }

            // Draw button image
            var buttonImage = buttonSpec.GetImage(_palette ?? KryptonManager.CurrentGlobalPalette, buttonState);
            if (buttonImage != null)
            {
                var imageRect = new Rectangle(
                    buttonRect.Left + (buttonRect.Width - buttonImage.Width) / 2,
                    buttonRect.Top + (buttonRect.Height - buttonImage.Height) / 2,
                    buttonImage.Width,
                    buttonImage.Height);
                e.Graphics.DrawImage(buttonImage, imageRect);
            }
            else
            {
                // Draw default X symbol for close button
                var penColor = tabPalette.Content.GetContentShortTextColor1(buttonState);
                if (penColor == Color.Empty)
                {
                    penColor = ForeColor;
                }
                using var pen = new Pen(penColor, 1.5f);
                var offset = 4;
                e.Graphics.DrawLine(pen,
                    buttonRect.Left + offset,
                    buttonRect.Top + offset,
                    buttonRect.Right - offset,
                    buttonRect.Bottom - offset);
                e.Graphics.DrawLine(pen,
                    buttonRect.Right - offset,
                    buttonRect.Top + offset,
                    buttonRect.Left + offset,
                    buttonRect.Bottom - offset);
            }
        }

        // Draw focus rectangle if needed
        if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
        {
            ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds);
        }
    }

    private void OnMouseMove(object? sender, MouseEventArgs e)
    {
        var newHoveredIndex = GetTabIndexAt(e.Location);
        var newHoveredButtonIndex = GetButtonIndexAt(e.Location);

        if (newHoveredIndex != _hoveredTabIndex || newHoveredButtonIndex != _hoveredButtonTabIndex)
        {
            var oldTabIndex = _hoveredTabIndex;
            var oldButtonIndex = _hoveredButtonTabIndex;
            _hoveredTabIndex = newHoveredIndex;
            _hoveredButtonTabIndex = newHoveredButtonIndex;

            // Invalidate old and new tabs
            if (oldTabIndex >= 0 && oldTabIndex < TabCount)
            {
                Invalidate(GetTabRect(oldTabIndex));
            }
            if (_hoveredTabIndex >= 0 && _hoveredTabIndex < TabCount)
            {
                Invalidate(GetTabRect(_hoveredTabIndex));
            }
            if (oldButtonIndex >= 0 && oldButtonIndex < TabCount)
            {
                Invalidate(GetTabRect(oldButtonIndex));
            }
            if (_hoveredButtonTabIndex >= 0 && _hoveredButtonTabIndex < TabCount)
            {
                Invalidate(GetTabRect(_hoveredButtonTabIndex));
            }
        }
    }

    private void OnMouseLeave(object? sender, EventArgs e)
    {
        if (_hoveredTabIndex >= 0 || _hoveredButtonTabIndex >= 0)
        {
            var oldTabIndex = _hoveredTabIndex;
            var oldButtonIndex = _hoveredButtonTabIndex;
            _hoveredTabIndex = -1;
            _hoveredButtonTabIndex = -1;
            if (oldTabIndex >= 0 && oldTabIndex < TabCount)
            {
                Invalidate(GetTabRect(oldTabIndex));
            }
            if (oldButtonIndex >= 0 && oldButtonIndex < TabCount)
            {
                Invalidate(GetTabRect(oldButtonIndex));
            }
        }
    }

    private void OnMouseDown(object? sender, MouseEventArgs e)
    {
        _mouseDown = true;
        var tabIndex = GetTabIndexAt(e.Location);
        if (tabIndex >= 0 && tabIndex < TabCount)
        {
            Invalidate(GetTabRect(tabIndex));
        }
    }

    private void OnMouseUp(object? sender, MouseEventArgs e)
    {
        _mouseDown = false;
        var tabIndex = GetTabIndexAt(e.Location);
        var buttonIndex = GetButtonIndexAt(e.Location);

        // Check if button was clicked
        if (buttonIndex >= 0 && buttonIndex < TabCount && _tabButtonSpecs.TryGetValue(buttonIndex, out var buttonSpec))
        {
            var buttonRect = GetTabButtonRect(buttonIndex);
            if (!buttonRect.IsEmpty && buttonRect.Contains(e.Location))
            {
                // Raise the button spec's Click event, which will trigger our handler
                buttonSpec.PerformClick();
                return;
            }
        }

        if (tabIndex >= 0 && tabIndex < TabCount)
        {
            Invalidate(GetTabRect(tabIndex));
        }
        if (buttonIndex >= 0 && buttonIndex < TabCount)
        {
            Invalidate(GetTabRect(buttonIndex));
        }
    }

    private int GetTabIndexAt(Point location)
    {
        for (var i = 0; i < TabCount; i++)
        {
            if (GetTabRect(i).Contains(location))
            {
                return i;
            }
        }
        return -1;
    }

    private int GetButtonIndexAt(Point location)
    {
        for (var i = 0; i < TabCount; i++)
        {
            if (_tabButtonSpecs.TryGetValue(i, out var buttonSpec) && buttonSpec.GetVisible(_palette ?? KryptonManager.CurrentGlobalPalette))
            {
                var buttonRect = GetTabButtonRect(i);
                if (!buttonRect.IsEmpty && buttonRect.Contains(location))
                {
                    return i;
                }
            }
        }
        return -1;
    }

    private Rectangle GetTabButtonRect(int tabIndex)
    {
        if (tabIndex < 0 || tabIndex >= TabCount || !_tabButtonSpecs.TryGetValue(tabIndex, out _))
        {
            return Rectangle.Empty;
        }

        var tabRect = GetTabRect(tabIndex);
        var buttonSize = 16;
        var buttonPadding = 4;
        var textRect = tabRect;
        textRect.Inflate(-4, -2);

        return new Rectangle(
            textRect.Right - buttonSize - buttonPadding,
            textRect.Top + (textRect.Height - buttonSize) / 2,
            buttonSize,
            buttonSize);
    }

    private void OnTabButtonSpecChanged(object? sender, EventArgs e)
    {
        // Find which tab this button spec belongs to
        foreach (var kvp in _tabButtonSpecs)
        {
            if (kvp.Value == sender)
            {
                if (kvp.Key < TabCount)
                {
                    Invalidate(GetTabRect(kvp.Key));
                }
                break;
            }
        }
    }

    private void OnTabButtonSpecClick(object? sender, EventArgs e)
    {
        if (sender is ButtonSpecAny buttonSpec && buttonSpec.Tag is int tabIndex)
        {
            if (tabIndex >= 0 && tabIndex < TabPages.Count)
            {
                var args = new TabButtonSpecClickEventArgs(tabIndex, TabPages[tabIndex], buttonSpec);
                TabButtonSpecClick?.Invoke(this, args);
            }
        }
    }

    #endregion
}

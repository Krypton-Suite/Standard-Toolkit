#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Draws an individual RibbonTab.
/// </summary>
internal class ViewDrawRibbonTab : ViewComposite,
    IContentValues
{
    #region Static Fields
    private static readonly string _empty = "<Empty>";
    private static readonly Blend _contextBlend2007;
    private static readonly Blend _contextBlend2010;
    #endregion

    #region Instance Fields
    private readonly Padding _preferredBorder2007; // = new(12, 3, 12, 1);
    private readonly Padding _preferredBorder2010; // = new(8, 4, 8, 3);
    private readonly Padding _layoutBorder2007; // = new(4, 3, 4, 1);
    private readonly Padding _layoutBorder2010; // = new(1, 4, 0, 3);
    private KryptonRibbonTab? _ribbonTab;
    private readonly PaletteRibbonGeneral _paletteGeneral;
    private readonly PaletteRibbonDoubleInheritOverride _overrideStateNormal;
    private readonly PaletteRibbonDoubleInheritOverride _overrideStateTracking;
    private readonly PaletteRibbonDoubleInheritOverride _overrideStateCheckedNormal;
    private readonly PaletteRibbonDoubleInheritOverride _overrideStateCheckedTracking;
    private readonly PaletteRibbonDoubleInheritOverride _overrideStateContextTracking;
    private readonly PaletteRibbonDoubleInheritOverride _overrideStateContextCheckedNormal;
    private readonly PaletteRibbonDoubleInheritOverride _overrideStateContextCheckedTracking;
    private PaletteRibbonDoubleInheritOverride _overrideCurrent;
    private readonly PaletteRibbonContextDouble _paletteContextCurrent;
    private readonly RibbonTabToContent _contentProvider;
    private readonly NeedPaintHandler _needPaint;
    private IDisposable?[] _mementos;
    private Size _preferredSize;
    private Rectangle _displayRect;
    private int _dirtyPaletteSize;
    private int _dirtyPaletteLayout;
    private PaletteState _cacheState;
    #endregion

    #region Identity
    static ViewDrawRibbonTab()
    {
        _contextBlend2007 = new Blend
        {
            Factors = [0.0f, 0.0f, 1.0f, 1.0f],
            Positions = [0.0f, 0.41f, 0.7f, 1.0f]
        };

        _contextBlend2010 = new Blend
        {
            Factors = [0.0f, 1.0f, 1.0f],
            Positions = [0.0f, 0.6f, 1.0f]
        };
    }

    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonTab class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="layoutTabs">Reference to view used for layout out tabs.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ViewDrawRibbonTab([DisallowNull] KryptonRibbon ribbon,
        [DisallowNull] ViewLayoutRibbonTabs layoutTabs,
        [DisallowNull] NeedPaintHandler needPaint)
    {
        Debug.Assert(ribbon != null);
        Debug.Assert(layoutTabs != null);
        Debug.Assert(needPaint != null);

        // Cache incoming values
        Ribbon = ribbon!;
        ViewLayoutRibbonTabs = layoutTabs!;
        _needPaint = needPaint!;

        // Create overrides for handling a focus state
        _paletteGeneral = Ribbon.StateCommon.RibbonGeneral;
        _overrideStateNormal = new PaletteRibbonDoubleInheritOverride(Ribbon.OverrideFocus.RibbonTab, Ribbon.OverrideFocus.RibbonTab, Ribbon.StateNormal.RibbonTab, Ribbon.StateNormal.RibbonTab, PaletteState.FocusOverride);
        _overrideStateTracking = new PaletteRibbonDoubleInheritOverride(Ribbon.OverrideFocus.RibbonTab, Ribbon.OverrideFocus.RibbonTab, Ribbon.StateTracking.RibbonTab, Ribbon.StateTracking.RibbonTab, PaletteState.FocusOverride);
        _overrideStateCheckedNormal = new PaletteRibbonDoubleInheritOverride(Ribbon.OverrideFocus.RibbonTab, Ribbon.OverrideFocus.RibbonTab, Ribbon.StateCheckedNormal.RibbonTab, Ribbon.StateCheckedNormal.RibbonTab, PaletteState.FocusOverride);
        _overrideStateCheckedTracking = new PaletteRibbonDoubleInheritOverride(Ribbon.OverrideFocus.RibbonTab, Ribbon.OverrideFocus.RibbonTab, Ribbon.StateCheckedTracking.RibbonTab, Ribbon.StateCheckedTracking.RibbonTab, PaletteState.FocusOverride);
        _overrideStateContextTracking = new PaletteRibbonDoubleInheritOverride(Ribbon.OverrideFocus.RibbonTab, Ribbon.OverrideFocus.RibbonTab, Ribbon.StateContextTracking.RibbonTab, Ribbon.StateContextTracking.RibbonTab, PaletteState.FocusOverride);
        _overrideStateContextCheckedNormal = new PaletteRibbonDoubleInheritOverride(Ribbon.OverrideFocus.RibbonTab, Ribbon.OverrideFocus.RibbonTab, Ribbon.StateContextCheckedNormal.RibbonTab, Ribbon.StateContextCheckedNormal.RibbonTab, PaletteState.FocusOverride);
        _overrideStateContextCheckedTracking = new PaletteRibbonDoubleInheritOverride(Ribbon.OverrideFocus.RibbonTab, Ribbon.OverrideFocus.RibbonTab, Ribbon.StateContextCheckedTracking.RibbonTab, Ribbon.StateContextCheckedTracking.RibbonTab, PaletteState.FocusOverride);
        _overrideCurrent = _overrideStateNormal;

        // Create and default the setup of the context colors provider
        _paletteContextCurrent = new PaletteRibbonContextDouble(Ribbon);
        _paletteContextCurrent.SetInherit(_overrideCurrent);

        // Use a class to convert from ribbon tab to content interface
        _contentProvider = new RibbonTabToContent(_paletteGeneral, _paletteContextCurrent);

        // Use a controller to change state because of mouse movement
        var controller = new RibbonTabController(Ribbon, this, _needPaint);
        controller.Click += OnTabClicked;
        controller.ContextClick += OnTabContextClicked;
        MouseController = controller;
        SourceController = controller;
        KeyController = controller;

        // Associate this view with the source component (required for design time selection)
        Component = _ribbonTab;

        // Create and add the draw content for display inside the tab
        Add(new ViewDrawContent(_contentProvider, this, VisualOrientation.Top));

        // Create the state specific memento array
        _mementos = new IDisposable[Enum.GetValues(typeof(PaletteState)).Length];

        _preferredBorder2007 = new Padding((int)(12 * FactorDpiX), (int)(3 * FactorDpiY), (int)(12 * FactorDpiX), (int)(1 * FactorDpiY));
        _preferredBorder2010 = new Padding((int)(8 * FactorDpiX), (int)(4 * FactorDpiY), (int)(8 * FactorDpiX), (int)(3 * FactorDpiY));
        _layoutBorder2007 = new Padding((int)(4 * FactorDpiX), (int)(3 * FactorDpiY), (int)(4 * FactorDpiX), (int)(1 * FactorDpiY));
        _layoutBorder2010 = new Padding((int)(1 * FactorDpiX), (int)(4 * FactorDpiY), (int)(0 * FactorDpiX), (int)(3 * FactorDpiY));
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonTab:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_ribbonTab != null)
            {
                _ribbonTab.PropertyChanged -= OnTabPropertyChanged;
                _ribbonTab.TabView = null;
            }

            if (_mementos != null!)
            {
                // Dispose of all the mementos in the array
                foreach (IDisposable? memento in _mementos)
                {
                    memento?.Dispose();
                }

                _mementos = null!;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region KeyTipTarget
    /// <summary>
    /// Gets access to the key tip target.
    /// </summary>
    public IRibbonKeyTipTarget? KeyTipTarget => SourceController as IRibbonKeyTipTarget;

    #endregion

    #region ViewLayoutRibbonTabs
    /// <summary>
    /// Gets access to the 
    /// </summary>
    public ViewLayoutRibbonTabs ViewLayoutRibbonTabs { get; }

    #endregion

    #region MakeDirty
    /// <summary>
    /// Make dirty so cached values are not used.
    /// </summary>
    public void MakeDirty()
    {
        _dirtyPaletteSize = 0;
        _dirtyPaletteLayout = 0;
    }
    #endregion

    #region HasFocus
    /// <summary>
    /// Gets and sets if the view has the focus and needs to draw appropriately.
    /// </summary>
    public bool HasFocus
    {
        get => _overrideStateNormal.Apply;

        set
        {
            _overrideStateNormal.Apply = value;
            _overrideStateTracking.Apply = value;
            _overrideStateCheckedNormal.Apply = value;
            _overrideStateCheckedTracking.Apply = value;
            _overrideStateContextTracking.Apply = value;
            _overrideStateContextCheckedNormal.Apply = value;
            _overrideStateContextCheckedTracking.Apply = value;
        }
    }
    #endregion

    #region Ribbon
    /// <summary>
    /// Gets access to the owning ribbon control instance.
    /// </summary>
    public KryptonRibbon Ribbon { get; }

    #endregion

    #region RibbonTab
    /// <summary>
    /// Gets and sets the ribbon tab this is responsible for drawing.
    /// </summary>
    public KryptonRibbonTab? RibbonTab
    {
        get => _ribbonTab;

        set
        {
            if (_ribbonTab != value)
            {
                // Unhook from current event handler
                if (_ribbonTab != null)
                {
                    _ribbonTab.PropertyChanged -= OnTabPropertyChanged;
                    _ribbonTab.TabView = null;
                }

                _ribbonTab = value;

                // Associate this view with the source component (required for design time selection)
                Component = _ribbonTab;

                // Hook into new tab changes
                if (_ribbonTab != null)
                {
                    _ribbonTab.PropertyChanged += OnTabPropertyChanged;
                    _ribbonTab.TabView = this;
                }

                // Pass reference onto the current context
                _paletteContextCurrent.RibbonTab = value;

                // Must perform new preferred size/layout calculations
                MakeDirty();
            }
        }
    }
    #endregion

    #region Checked
    /// <summary>
    /// Gets and sets the checked state of the tab.
    /// </summary>
    public bool Checked { get; set; }

    #endregion

    #region PreferredBorder
    /// <summary>
    /// Gets the preferred border size.
    /// </summary>
    public Padding PreferredBorder
    {
        get
        {
            return Ribbon.RibbonShape switch
            {
                PaletteRibbonShape.Office2010 => _preferredBorder2010,
                PaletteRibbonShape.Office2013 => _preferredBorder2010,
                PaletteRibbonShape.Microsoft365 => _preferredBorder2010,
                PaletteRibbonShape.VisualStudio => _preferredBorder2010,
                _ => _preferredBorder2007
            };
        }
    }
    #endregion

    #region LayoutBorder
    /// <summary>
    /// Gets the layout border size.
    /// </summary>
    public Padding LayoutBorder
    {
        get
        {
            return Ribbon.RibbonShape switch
            {
                PaletteRibbonShape.Office2010 => _layoutBorder2010,
                PaletteRibbonShape.Office2013 => _layoutBorder2010,
                PaletteRibbonShape.Microsoft365 => _layoutBorder2010,
                _ => _layoutBorder2007
            };
        }
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        // Ensure that child elements have correct palette state
        CheckPaletteState(context);

        // A change in state always causes a size and layout calculation
        if (_cacheState != State)
        {
            MakeDirty();
            _cacheState = State;
        }

        // If the palette has changed since we last calculated
        if (Ribbon.DirtyPaletteCounter != _dirtyPaletteSize)
        {
            // Get the preferred size of the contained content
            _preferredSize = base.GetPreferredSize(context);

            // Add on the preferred border sizing
            Padding preferredBorder = PreferredBorder;
            _preferredSize = new Size(_preferredSize.Width + preferredBorder.Horizontal,
                _preferredSize.Height + preferredBorder.Vertical);

            // Cached value is valid till dirty palette noticed
            _dirtyPaletteSize = Ribbon.DirtyPaletteCounter;
        }

        return _preferredSize;
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Ensure that child elements have correct palette state
        CheckPaletteState(context!);

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;

        // A change in state always causes a size and layout calculation
        if (_cacheState != State)
        {
            MakeDirty();
            _cacheState = State;
        }

        // Do we need to actually perform the re-layout?
        if ((_displayRect != ClientRectangle) ||
            (Ribbon.DirtyPaletteCounter != _dirtyPaletteLayout))
        {
            // Reduce display rect by our border size
            Padding layoutBorder = LayoutBorder;
            context.DisplayRectangle = new Rectangle(ClientLocation.X + layoutBorder.Left,
                ClientLocation.Y + layoutBorder.Top,
                ClientWidth - layoutBorder.Horizontal,
                ClientHeight - layoutBorder.Vertical);

            // Let contained content element we laid out
            base.Layout(context);

            // Put back the original display value now we have finished
            context.DisplayRectangle = ClientRectangle;

            // Cache values that are needed to decide if layout is needed
            _displayRect = ClientRectangle;
            _dirtyPaletteLayout = Ribbon.DirtyPaletteCounter;
        }
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore(RenderContext context)
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // Ensure that child elements have correct palette state
        CheckPaletteState(context);

        // Grab the context tab set that relates to this tab
        ContextTabSet? cts = ViewLayoutRibbonTabs.ContextTabSets[RibbonTab!.ContextName];

        switch (Ribbon.RibbonShape)
        {
            default:
            case PaletteRibbonShape.Office2013:
            case PaletteRibbonShape.Microsoft365:
            case PaletteRibbonShape.VisualStudio:
            case PaletteRibbonShape.Office2007:
                if (cts != null)
                {
                    RenderBefore2007ContextTab(context, cts);
                }

                _paletteContextCurrent.LightBackground = false;
                break;
            case PaletteRibbonShape.Office2010:
                if (cts != null)
                {
                    RenderBefore2010ContextTab(context, cts);
                }

                _paletteContextCurrent.LightBackground = KryptonManager.CurrentGlobalPaletteMode.ToString()
                    .StartsWith(PaletteMode.Office2010Black.ToString());
                break;
        }

        // Use renderer to draw the tab background
        var mementoIndex = StateIndex(State);
        _mementos[mementoIndex] = context.Renderer.RenderRibbon.DrawRibbonBack(Ribbon.RibbonShape, context, ClientRectangle, State, _paletteContextCurrent, VisualOrientation.Top, _mementos[mementoIndex]);
    }

    /// <summary>
    /// Perform rendering after child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderAfter(RenderContext context)
    {
        // Grab the context tab set that relates to this tab
        ContextTabSet? cts = ViewLayoutRibbonTabs.ContextTabSets[RibbonTab!.ContextName];

        // Is this tab part of a context?
        if (cts != null)
        {
            switch (Ribbon.RibbonShape)
            {
                case PaletteRibbonShape.Office2010:
                    RenderAfter2010ContextTab();
                    break;
            }
        }
    }
    #endregion

    #region IContentValues
    /// <summary>
    /// Gets the image used for the ribbon tab.
    /// </summary>
    /// <param name="state">Tab state.</param>
    /// <returns>Image.</returns>
    public Image? GetImage(PaletteState state) => null;

    /// <summary>
    /// Gets the image color that should be interpreted as transparent.
    /// </summary>
    /// <param name="state">Tab state.</param>
    /// <returns>Transparent Color.</returns>
    public Color GetImageTransparentColor(PaletteState state) => Color.Empty;

    /// <summary>
    /// Gets the short text used as the main ribbon title.
    /// </summary>
    /// <returns>Title string.</returns>
    public string GetShortText()
    {
        // We only use the ribbon tab text if we have a ribbon tab to
        // reference and the text is not zero length. We try and prevent
        // an empty string because it makes the tab useless!
        if (_ribbonTab is { Text.Length: > 0 })
        {
            return Ribbon.RibbonShape == PaletteRibbonShape.Office2013 ? _ribbonTab.Text.ToUpper() : _ribbonTab.Text;
        }

        return _empty;
    }

    /// <summary>
    /// Gets the long text used as the secondary ribbon title.
    /// </summary>
    /// <returns>Title string.</returns>
    public string GetLongText() => string.Empty;

    #endregion

    #region Implementation
    private void RenderBefore2007ContextTab(RenderContext context, ContextTabSet cts)
    {
        // We only draw side separators on the first and last tab of the contexts
        if (cts.IsFirstOrLastTab(this))
        {
            // Grab the color we draw the context separator in
            Color sepColor = _paletteGeneral.GetRibbonTabSeparatorContextColor(PaletteState.Normal);

            Rectangle parentRect = Parent!.ClientRectangle;
            var contextRect = parentRect with { X = ClientRectangle.X - 1, Width = ClientRectangle.Width + 2 };
            var gradientRect = new Rectangle(ClientRectangle.X - 1, parentRect.Y - 1,
                ClientRectangle.Width + 2, parentRect.Height + 2);

            using var sepBrush = new LinearGradientBrush(gradientRect, sepColor, Color.Transparent, 90f);
            // We need to customize the way the color blends over the background
            sepBrush.Blend = _contextBlend2007;

            using var sepPen = new Pen(sepBrush);
            if (cts.IsFirstTab(this))
            {
                context.Graphics.DrawLine(sepPen, contextRect.X, contextRect.Y, contextRect.X, contextRect.Bottom - 1);
            }

            if (cts.IsLastTab(this))
            {
                context.Graphics.DrawLine(sepPen, contextRect.Right - 1, contextRect.Y, contextRect.Right - 1, contextRect.Bottom - 1);
            }
        }
    }

    private void RenderBefore2010ContextTab(RenderContext context, ContextTabSet cts)
    {
        // Grab the colors we draw the context separators and background in
        Color c1 = _paletteGeneral.GetRibbonTabSeparatorContextColor(PaletteState.Normal);
        Color c2 = cts.ContextColor;
        Color lightC2 = ControlPaint.Light(c2);
        Color c3 = CommonHelper.MergeColors(Color.Black, 0.1f, c2, 0.9f);

        var contextRect = new Rectangle(ClientRectangle.X - 1, ClientRectangle.Y - 1,
            ClientRectangle.Width + 2, ClientRectangle.Height + 1);
        var fillRect = new Rectangle(ClientRectangle.X - 2, ClientRectangle.Y - 1, ClientRectangle.Width + 4,
            ClientRectangle.Height);

        using var outerBrush = new LinearGradientBrush(contextRect, c1, Color.Transparent, 90f);
        using var innerBrush = new LinearGradientBrush(contextRect, c3, Color.Transparent, 90f);
        using var fillBrush = new LinearGradientBrush(contextRect, Color.FromArgb(64, lightC2), Color.Transparent, 90f);
        fillBrush.Blend = _contextBlend2010;

        using var outerPen = new Pen(outerBrush);
        using var innerPen = new Pen(innerBrush);
        if (cts.IsFirstTab(this))
        {
            // Draw left separators
            context.Graphics.DrawLine(outerPen, contextRect.X, contextRect.Y, contextRect.X, contextRect.Bottom - 2);
            context.Graphics.DrawLine(innerPen, contextRect.X + 1, contextRect.Y, contextRect.X + 1, contextRect.Bottom - 2);
            fillRect.X += 2;
            fillRect.Width -= 2;

            if (cts.IsLastTab(this))
            {
                // Draw right separators
                context.Graphics.DrawLine(outerPen, contextRect.Right - 1, contextRect.Y, contextRect.Right - 1, contextRect.Bottom - 2);
                context.Graphics.DrawLine(innerPen, contextRect.Right - 2, contextRect.Y, contextRect.Right - 2, contextRect.Bottom - 2);
                fillRect.Width -= 2;
            }
        }
        else if (cts.IsLastTab(this))
        {
            // Draw right separators
            context.Graphics.DrawLine(outerPen, contextRect.Right - 1, contextRect.Y, contextRect.Right - 1, contextRect.Bottom - 2);
            context.Graphics.DrawLine(innerPen, contextRect.Right - 2, contextRect.Y, contextRect.Right - 2, contextRect.Bottom - 2);
            fillRect.Width -= 2;
        }

        // Draw the background gradient
        context.Graphics.FillRectangle(fillBrush, fillRect);
    }

    private void RenderAfter2010ContextTab()
    {
        switch (State)
        {
            case PaletteState.ContextCheckedNormal:
            case PaletteState.ContextCheckedTracking:
                // Add ellipses highlight to the selected context tab
                break;
        }
    }

    private int StateIndex(PaletteState state)
    {
        Array stateValues = Enum.GetValues(typeof(PaletteState));

        for (var i = 0; i < stateValues.Length; i++)
        {
            if ((PaletteState?)stateValues.GetValue(i) == state)
            {
                return i;
            }
        }

        return 0;
    }

    private void CheckPaletteState(ViewContext context)
    {
        // Should control be enabled or disabled
        var enabled = IsFixed || context.Control!.Enabled;

        // Ensure we and child and in correct enabled state
        Enabled = enabled;

        // Better check we have a child!
        if (Count > 0)
        {
            this[0]!.Enabled = enabled;
        }

        // If disabled...
        if (!enabled)
        {
            //...must always be using the normal overrides
            _paletteContextCurrent.SetInherit(_overrideStateNormal);
        }
        else
        {
            // Default to using this element calculated state
            PaletteState buttonState = State;

            // Update the checked state
            Checked = Ribbon.SelectedTab == RibbonTab;

            // Is this tab a context tab?
            var contextTab = !string.IsNullOrEmpty(RibbonTab!.ContextName);

            // Apply the checked state if not fixed
            if (!IsFixed)
            {
                if (Checked)
                {
                    switch (buttonState)
                    {
                        case PaletteState.Normal:
                        case PaletteState.CheckedNormal:
                        case PaletteState.ContextCheckedNormal:
                            buttonState = contextTab ? PaletteState.ContextCheckedNormal : PaletteState.CheckedNormal;
                            break;
                        case PaletteState.Tracking:
                        case PaletteState.CheckedTracking:
                        case PaletteState.ContextCheckedTracking:
                            buttonState = contextTab ? PaletteState.ContextCheckedTracking : PaletteState.CheckedTracking;
                            break;
                    }
                }
                else
                {
                    switch (buttonState)
                    {
                        case PaletteState.CheckedNormal:
                        case PaletteState.ContextCheckedNormal:
                            buttonState = PaletteState.Normal;
                            break;
                        case PaletteState.Tracking:
                        case PaletteState.CheckedTracking:
                        case PaletteState.ContextCheckedTracking:
                            buttonState = contextTab ? PaletteState.ContextTracking : PaletteState.Tracking;
                            break;
                    }
                }
            }

            // Set the correct palette based on state
            switch (buttonState)
            {
                case PaletteState.Disabled:
                case PaletteState.Normal:
                    _overrideCurrent = _overrideStateNormal;
                    break;

                case PaletteState.Tracking:
                    _overrideCurrent = _overrideStateTracking;
                    break;

                case PaletteState.CheckedNormal:
                    _overrideCurrent = _overrideStateCheckedNormal;
                    break;

                case PaletteState.CheckedTracking:
                    _overrideCurrent = _overrideStateCheckedTracking;
                    break;

                case PaletteState.ContextTracking:
                    _overrideCurrent = _overrideStateContextTracking;
                    break;

                case PaletteState.ContextCheckedNormal:
                    _overrideCurrent = _overrideStateContextCheckedNormal;
                    break;

                case PaletteState.ContextCheckedTracking:
                    _overrideCurrent = _overrideStateContextCheckedTracking;
                    break;

                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(buttonState.ToString());
                    break;
            }

            // Switch the child elements over to correct state
            ElementState = buttonState;

            // Better check we have a child!
            if (Count > 0)
            {
                this[0]!.ElementState = buttonState;
            }

            // Update the actual source palette
            _paletteContextCurrent.SetInherit(_overrideCurrent);
        }
    }

    private void OnTabPropertyChanged(object? sender, PropertyChangedEventArgs e) => MakeDirty();

    private void OnTabClicked(object? sender, MouseEventArgs e)
    {
        // We never click to become unchecked
        if (!Checked)
        {
            // We must be associated with a ribbon tab instance
            if (RibbonTab != null)
            {
                // Update ribbon so associated tab becomes selected
                Ribbon.SelectedTab = RibbonTab;
            }
        }
    }

    private void OnTabContextClicked(object? sender, MouseEventArgs e)
    {
        if (Ribbon.InDesignMode)
        {
            _ribbonTab?.OnDesignTimeContextMenu(new MouseEventArgs(MouseButtons.Right, 1, e.X, e.Y, 0));
        }
        else
        {
            // Convert the mouse point to screen coords from the containing control
            Point screenPt = Ribbon.TabsArea!.TabsContainerControl.ChildControl!.PointToScreen(new Point(e.X, e.Y));

            // Convert back to ribbon client coords, needed for the show context menu call
            Point clientPt = Ribbon.PointToClient(screenPt);

            // Request the context menu be shown
            Ribbon.DisplayRibbonContextMenu(new MouseEventArgs(e.Button, e.Clicks, clientPt.X, clientPt.Y, e.Delta));
        }
    }
    #endregion
}
#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
namespace Krypton.Toolkit;

/// <summary>
/// Base class that allows a form to have custom chrome applied. You should derive
/// a class from this that performs the specific chrome drawing that is required.
/// </summary>
[ToolboxItem(false)]
public abstract class VisualForm : Form,
    IKryptonDebug
{
    #region Static Fields
    private static readonly bool _themedApp;

    // To avoid lag when Acrylic is in use
    public const int WS_EX_NOREDIRECTIONBITMAP = 0x00200000;

    #endregion

    #region Instance Fields
    private bool _activated;
    private bool _windowActive;
    private bool _trackingMouse;
    private bool _useThemeFormChromeBorderWidth;
    private bool _captured;
    private bool _disposing;
    private int _ignoreCount;
    private KryptonCustomPaletteBase? _localCustomPalette;
    private PaletteBase _palette;
    private PaletteMode _paletteMode;
    private readonly IntPtr _screenDC;
    private ShadowValues _shadowValues;
    private ShadowManager _shadowManager;
    private BlurValues _blurValues;
    private BlurManager _blurManager;
    private readonly object lockObject = new();
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the palette changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the value of the Palette property is changed.")]
    public event EventHandler? PaletteChanged;

    /// <summary>
    /// Occurs when the use of custom chrome changes.
    /// </summary>
    [Browsable(false)]  // SKC: Probably a special case for not exposing this event in the designer....
    [EditorBrowsable(EditorBrowsableState.Never)]
    public event EventHandler? ApplyUseThemeFormChromeBorderWidthChanged;

    /// <summary>
    /// Occurs when the active window setting changes.
    /// </summary>
    [Browsable(false)]  // SKC: Probably a special case for not exposing this event in the designer....
    [EditorBrowsable(EditorBrowsableState.Never)]
    public event EventHandler? WindowActiveChanged;

    /// <summary>
    /// Occurs when the Global palette changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the value of the GlobalPalette property is changed.")]
    public event EventHandler? GlobalPaletteChanged;
    #endregion

    #region Identity
    static VisualForm()
    {
        try
        {
            // Is this application in an OS that is capable of themes and is currently themed
            _themedApp = VisualStyleInformation.IsEnabledByUser && !string.IsNullOrEmpty(VisualStyleInformation.ColorScheme);
        }
        catch
        {
            //
        }
    }

    /// <summary>
    /// Initialize a new instance of the VisualForm class.
    /// </summary>
    protected VisualForm()
    {
        InitializeComponent();

        // Automatically redraw whenever the size of the window changes
        SetStyle(ControlStyles.ResizeRedraw, true);

        // We need to create and cache a device context compatible with the display
        _screenDC = PI.CreateCompatibleDC(IntPtr.Zero);

        // Setup the need paint delegate
        NeedPaintDelegate = OnNeedPaint;

        // Set the palette and renderer to the defaults as specified by the manager
        _localCustomPalette = null;
        SetPalette(KryptonManager.CurrentGlobalPalette);
        _paletteMode = PaletteMode.Global;

        // We need to layout the view
        NeedLayout = true;

        // Create constant target for resolving palette delegates
        Redirector = CreateRedirector();

        // Hook into global static events
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
        SystemEvents.UserPreferenceChanged += OnUserPreferenceChanged;

        ShadowValues = new ShadowValues();
        BlurValues = new BlurValues();

#if !NET462
        DpiChanged += OnDpiChanged;
#endif
        // Note: Will not handle movement between monitors
        UpdateDpiFactors();
    }

    /// <summary>
    /// Releases all resources used by the Control.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        _disposing = true;

        if (disposing)
        {
            // Must unhook from the palette paint events
            if (_palette != null!)
            {
                _palette.PalettePaint -= OnNeedPaint;
                _palette.ButtonSpecChanged -= OnButtonSpecChanged;
                _palette.UseThemeFormChromeBorderWidthChanged -= OnUseThemeFormChromeBorderWidthChanged;
                _palette.BasePaletteChanged -= OnBaseChanged;
                _palette.BaseRendererChanged -= OnBaseChanged;
                _palette = null!;
            }

            // Unhook from global static events
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
            SystemEvents.UserPreferenceChanged -= OnUserPreferenceChanged;
        }

        base.Dispose(disposing);

        ViewManager?.Dispose();

        if (_screenDC != IntPtr.Zero)
        {
            PI.DeleteDC(_screenDC);
        }
    }
    #endregion

    #region Public

    /*public AcrylicValues AcrylicValues { get; } = new AcrylicValues();

    private void ResetAcrylicValues() => AcrylicValues.Reset();

    private bool ShouldSerializeAcrylicValues() => !AcrylicValues.IsDefault;*/

    /// <summary>
    /// Gets the DpiX of the view.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public float FactorDpiX
    {
        [DebuggerStepThrough]
        get;
        set;
    } = 1;

    /// <summary>
    /// Gets the DpiY of the view.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public float FactorDpiY
    {
        [DebuggerStepThrough]
        get;
        set;
    } = 1;

    /// <summary>
    /// Gets and sets a value indicating if palette chrome should be applied.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool UseThemeFormChromeBorderWidth
    {
        [DebuggerStepThrough]
        get => _useThemeFormChromeBorderWidth;

        set
        {
            // Only interested in changed values
            if (_useThemeFormChromeBorderWidth != value)
            {
                // Cache old setting
                var oldUseThemeFormChromeBorderWidth = _useThemeFormChromeBorderWidth;

                // Store the new setting
                _useThemeFormChromeBorderWidth = value;

                // If we need custom chrome drawing...
                if (_useThemeFormChromeBorderWidth)
                {
                    try
                    {
                        // Set back to false in case we decide that the operating system
                        // is not capable of supporting our custom chrome implementation
                        _useThemeFormChromeBorderWidth = false;

                        // Only need to remove the window theme, if there is one
                        if (PI.IsAppThemed() && PI.IsThemeActive())
                        {
                            // Assume that we can apply custom chrome
                            _useThemeFormChromeBorderWidth = true;

                            // Remove any theme that is currently drawing chrome
                            PI.SetWindowTheme(Handle, string.Empty, string.Empty);

                            // Call virtual method for initializing own chrome
                            WindowChromeStart();
                        }
                    }
                    catch
                    {
                        // Failed and so cannot provide custom chrome
                        _useThemeFormChromeBorderWidth = false;
                    }
                }
                else
                {
                    try
                    {
                        // Restore the application to previous theme setting
                        PI.SetWindowTheme(Handle, null, null);

                        // Call virtual method to reverse own chrome setup
                        WindowChromeEnd();
                    }
                    catch
                    {
                        //
                    }
                }

                // Raise event to notify a change in setting
                if (_useThemeFormChromeBorderWidth != oldUseThemeFormChromeBorderWidth)
                {
                    // Generate change event
                    OnApplyUseThemeFormChromeBorderWidthChanged(EventArgs.Empty);
                }
            }
        }
    }

    /// <summary>Gets or sets a value indicating whether the Close button is displayed in the caption bar of the form.</summary>
    /// <returns>
    /// <see langword="true" /> to display a Close button for the form; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
    [Category("Window Style")]
    [DefaultValue(true)]
    [Description(
        "Form Close Button Visiblity: This will also Hide the System Menu `Close` and disable the `Alt+F4` action")]
    public bool CloseBox { [DebuggerStepThrough] get; set; } = true;

    /// <summary>
    /// Gets or sets the palette to be applied.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Palette applied to drawing.")]
    public PaletteMode PaletteMode
    {
        [DebuggerStepThrough]
        get => _paletteMode;

        set
        {
            if (_paletteMode != value)
            {
                // Action depends on new value
                switch (value)
                {
                    case PaletteMode.Custom:
                        // Do nothing, you must assign a palette to the
                        // 'Palette' property in order to get the custom mode
                        break;
                    default:
                        // Use the new value
                        _paletteMode = value;

                        // Get a reference to the standard palette from its name
                        _localCustomPalette = null;
                        SetPalette(KryptonManager.GetPaletteForMode(_paletteMode));

                        // Must raise event to change palette in redirector
                        OnPaletteChanged(EventArgs.Empty);

                        // Need to layout again use new palette
                        PerformLayout();
                        break;
                }
            }
        }
    }

    private void ResetPaletteMode() => PaletteMode = PaletteMode.Global;

    private bool ShouldSerializePaletteMode() => PaletteMode != PaletteMode.Global;

    /* FadeValues disabled and moved to extended until proven stable. Further development in V100
    /// <summary>Gets access to the fade values.</summary>
    [Category(@"Visuals")]
    [Description(@"Form fading.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public FadeValues FadeValues { get; } = new FadeValues();

    private bool ShouldSerializeFadeValues() => !FadeValues.IsDefault;

    /// <summary>Resets the fade values.</summary>
    private void ResetFadeValues() => FadeValues.Reset();
    */

    /// <summary>
    /// Gets access to the button content.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Form Shadowing")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ShadowValues ShadowValues
    {
        [DebuggerStepThrough]
        get => _shadowValues;
        set
        {
            _shadowValues = value;
            _shadowManager = new ShadowManager(this, _shadowValues);
        }
    }

    private bool ShouldSerializeShadowValues() => !_shadowValues.IsDefault;

    /// <summary>
    /// Resets the <see cref="KryptonForm"/> shadow values.
    /// </summary>
    public void ResetShadowValues() => _shadowValues.Reset();

    /// <summary>
    /// Gets access to the button content.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Form Blurring")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public BlurValues BlurValues
    {
        [DebuggerStepThrough]
        get => _blurValues;
        set
        {
            _blurValues = value;
            _blurManager = new BlurManager(this, _blurValues);
        }
    }

    private bool ShouldSerializeBlurValues() => !_blurValues.IsDefault;

    /// <summary>
    /// Resets the <see cref="KryptonForm"/> blur values.
    /// </summary>
    public void ResetBlurValues() => _blurValues.Reset();

    /// <summary>
    /// Gets and sets the custom palette implementation.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Custom palette applied to drawing.")]
    [DefaultValue(null)]
    public KryptonCustomPaletteBase? LocalCustomPalette
    {
        [DebuggerStepThrough]
        get => _localCustomPalette;

        set
        {
            // Only interested in changes of value
            if (_localCustomPalette != value)
            {
                // Remember the starting palette
                PaletteBase? old = _localCustomPalette;

                // If no custom palette is required
                if (value == null)
                {
                    // No custom palette, so revert back to the global setting
                    _paletteMode = PaletteMode.Global;

                    // Get the appropriate palette for the global mode
                    _localCustomPalette = null;
                    SetPalette(KryptonManager.GetPaletteForMode(_paletteMode));
                }
                else
                {
                    // No longer using a standard palette
                    _localCustomPalette = value;
                    _paletteMode = PaletteMode.Custom;
                    // Use the provided palette value
                    SetPalette(value);
                }

                // If real change has occurred
                if (old != _localCustomPalette)
                {
                    // Raise the change event
                    OnPaletteChanged(EventArgs.Empty);

                    // Need to layout again use new palette
                    PerformLayout();
                }
            }
        }
    }

    /// <summary>
    /// Resets the Palette property to its default value.
    /// </summary>
    public void ResetPalette() => _localCustomPalette = null;

    /// <summary>
    /// Gets access to the current renderer.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IRenderer Renderer
    {
        [DebuggerStepThrough]
        get;
        private set;
    }

    /// <summary>
    /// Fires the NeedPaint event.
    /// </summary>
    /// <param name="needLayout">Does the palette change require a layout.</param>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public void PerformNeedPaint(bool needLayout) => OnNeedPaint(this, new NeedLayoutEventArgs(needLayout));

    /// <summary>
    /// Gets the resolved palette to actually use when drawing.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public PaletteBase GetResolvedPalette() => _palette;

    /// <summary>
    /// Create a tool strip renderer appropriate for the current renderer/palette pair.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public ToolStripRenderer CreateToolStripRenderer() => Renderer.RenderToolStrip(GetResolvedPalette());

    /// <summary>
    /// Send the provided system command to the window for processing.
    /// </summary>
    /// <param name="sysCommand">System command.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal void SendSysCommand(PI.SC_ sysCommand) => SendSysCommand(sysCommand, IntPtr.Zero);

    /// <summary>
    /// Send the provided system command to the window for processing.
    /// </summary>
    /// <param name="sysCommand">System command.</param>
    /// <param name="lParam">LPARAM value.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal void SendSysCommand(PI.SC_ sysCommand, IntPtr lParam) =>
        // Send window message to ourself
        PI.SendMessage(Handle, PI.WM_.SYSCOMMAND, (IntPtr)sysCommand, lParam);

    /// <summary>
    /// Gets the size of the borders requested by the real window.
    /// </summary>
    /// <returns>Border sizing.</returns>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Padding RealWindowBorders => CommonHelper.GetWindowBorders(CreateParams, this as KryptonForm);

    /// <summary>
    /// Gets a count of the number of paints that have occurred.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int PaintCount { get; private set; }

    /// <summary>
    /// Gets and sets the active state of the window.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool WindowActive
    {
        get => _windowActive;

        set
        {
            if (_windowActive != value)
            {
                _windowActive = value;
                _blurManager.SetBlurState(_windowActive);
                OnWindowActiveChanged();
            }
        }
    }

    /// <summary>
    /// Request the non-client area be repainted.
    /// </summary>
    public void RedrawNonClient() => InvalidateNonClient(Rectangle.Empty, true);

    /// <summary>
    /// Request the non-client area be recalculated.
    /// </summary>
    public void RecalcNonClient()
    {
        if (!IsDisposed && !Disposing && IsHandleCreated)
        {
            PI.SetWindowPos(Handle, IntPtr.Zero, 0, 0, 0, 0,
                PI.SWP_.NOACTIVATE | PI.SWP_.NOMOVE |
                PI.SWP_.NOZORDER | PI.SWP_.NOSIZE |
                PI.SWP_.NOOWNERZORDER | PI.SWP_.FRAMECHANGED);
        }
    }

#if NET8_0_OR_GREATER
        /// <summary>Gets or sets the anchoring for minimized MDI children.</summary>
        /// <value> <c>true</c> to anchor minimized MDI children to the bottom left of the parent form; <c>false</c> to anchor to the top left of the parent form.</value>
        [Category(@"Window Style")]
        [Description(@"Gets or sets the anchoring for minimized MDI children.")]
        [DefaultValue(true)]
        public new bool MdiChildrenMinimizedAnchorBottom
        {
            get => base.MdiChildrenMinimizedAnchorBottom;

            set
            {
                base.MdiChildrenMinimizedAnchorBottom = value;
                throw new NotSupportedException(@"Please use .NET 6 or newer to use this feature.");
            }
        }
#endif

    /// <summary>Gets or sets the toolbar manager.</summary>
    /// <value>The toolbar manager.</value>
    [DefaultValue(null), Category(@"Visuals"), Description(@"Gets or sets the tool bar manager.")]
    public KryptonIntegratedToolBarManager? ToolBarManager { get; set; }

    #endregion

    #region Public IKryptonDebug
    /// <summary>
    /// Reset the internal counters.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void KryptonResetCounters() => ViewManager?.ResetCounters();

    /// <summary>
    /// Gets the number of layout cycles performed since last reset.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int KryptonLayoutCounter => ViewManager?.LayoutCounter ?? 0;

    /// <summary>
    /// Gets the number of paint cycles performed since last reset.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int KryptonPaintCounter => ViewManager?.PaintCounter ?? 0;

    #endregion

    #region Protected
    /// <summary>
    /// Gets and sets the ViewManager instance.
    /// </summary>
    protected ViewManager? ViewManager
    {
        [DebuggerStepThrough]
        get;
        set;
    }

    /// <summary>
    /// Gets access to the palette redirector.
    /// </summary>
    protected PaletteRedirect Redirector
    {
        [DebuggerStepThrough]
        get;
    }

    /// <summary>
    /// Gets access to the need paint delegate.
    /// </summary>
    protected NeedPaintHandler NeedPaintDelegate
    {
        [DebuggerStepThrough]
        get;
    }

    /// <summary>
    /// Convert a screen location to a window location.
    /// </summary>
    /// <param name="screenPt">Screen point.</param>
    /// <returns>Point in window coordinates.</returns>
    protected Point ScreenToWindow(Point screenPt)
    {
        // First of all convert to client coordinates
        Point clientPt = PointToClient(screenPt);

        // Now adjust to take into account the top and left borders
        Padding borders = RealWindowBorders;
        clientPt.Offset(borders.Left, borders.Top);

        return clientPt;
    }

    /// <summary>
    /// Request the non-client area be repainted.
    /// </summary>
    public void InvalidateNonClient() => InvalidateNonClient(Rectangle.Empty, true);

    /// <summary>
    /// Request the non-client area be repainted.
    /// </summary>
    /// <param name="invalidRect">Area to invalidate.</param>
    protected void InvalidateNonClient(Rectangle invalidRect) => InvalidateNonClient(invalidRect, true);

    /// <summary>
    /// Request the non-client area be repainted.
    /// </summary>
    /// <param name="invalidRect">Area to invalidate.</param>
    /// <param name="excludeClientArea">Should client area be excluded.</param>
    protected void InvalidateNonClient(Rectangle invalidRect,
        bool excludeClientArea)
    {
        if (IsDisposed || Disposing || !IsHandleCreated)
        {
            return;
        }

        lock (lockObject)
        {
            if (invalidRect.IsEmpty)
            {
                Padding realWindowBorders = RealWindowBorders;
                Rectangle realWindowRectangle = RealWindowRectangle;

                invalidRect = realWindowRectangle with
                {
                    X = -realWindowBorders.Left,
                    Y = -realWindowBorders.Top
                };
            }

            using var invalidRegion = new Region(invalidRect);
            if (excludeClientArea)
            {
                invalidRegion.Exclude(ClientRectangle);
            }

            using Graphics g = Graphics.FromHwnd(Handle);
            IntPtr? hRgn = null;
            try
            {
                hRgn = invalidRegion.GetHrgn(g);

                PI.RedrawWindow(Handle, IntPtr.Zero, hRgn.Value,
                    PI.RDW_FRAME | PI.RDW_UPDATENOW | PI.RDW_INVALIDATE);
            }
            catch (InvalidOperationException ioEx)
            {
                // Object is currently in use elsewhere. ??
                Debug.WriteLine(ioEx.Message);
            }
            finally
            {
                if (hRgn != null)
                {
                    PI.DeleteObject(hRgn.Value);
                }
            }
        }
    }

    /// <summary>
    /// Gets rectangle that is the real window rectangle based on Win32 API call.
    /// </summary>
    protected Rectangle RealWindowRectangle
    {
        get
        {
            // Grab the actual current size of the window, this is more accurate than using
            // the 'this.Size' which is out of date when performing a resize of the window.
            var windowRect = new PI.RECT();
            PI.GetWindowRect(Handle, ref windowRect);
            // Create rectangle that encloses the entire window
            return new Rectangle(0, 0,
                windowRect.right - windowRect.left,
                windowRect.bottom - windowRect.top);
        }
    }
    #endregion

    #region Protected Override

    //protected override CreateParams CreateParams
    //{
    //    get
    //    {
    //        CreateParams cp = base.CreateParams;

    //        cp.ExStyle |= WS_EX_NOREDIRECTIONBITMAP;

    //        return cp;
    //    }
    //}

    /// <summary>
    /// Raises the HandleCreated event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnHandleCreated(EventArgs e)
    {
        // Can fail on versions before XP SP1
        try
        {
            // Prevent the OS from drawing the non-client area in classic look
            // if the application stops responding to windows messages
            PI.DisableProcessWindowsGhosting();
        }
        catch
        {
            //
        }

        //if (AcrylicValues.EnableAcrylic)
        //{
        //    WindowUtilities.EnableAcrylic(this, AcrylicValues.AcrylicColor);
        //}

        base.OnHandleCreated(e);
    }

    /// <summary>
    /// Start capturing mouse input for a particular element that is inside the chrome.
    /// </summary>
    /// <param name="element">Target element for the capture events.</param>
    protected void StartCapture(ViewBase element)
    {
        // Capture mouse input, so we notice the WM_LBUTTONUP when the mouse is released
        Capture = true;
        _captured = true;
    }

    /// <summary>
    /// Raises the Resize event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnResize(EventArgs e)
    {
        // Allow an extra region change to occur during resize
        ResumePaint();

        base.OnResize(e);

        if (!((MdiParent != null)
              && CommonHelper.IsFormMaximized(this))
           )
        {
            PerformNeedPaint(true);
        }

        // Reverse the resume from earlier
        SuspendPaint();
    }

    /// <summary>
    /// Raises the Activated event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnActivated(EventArgs e)
    {
        WindowActive = true;
        base.OnActivated(e);
    }

    /// <summary>
    /// Raises the Deactivate event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnDeactivate(EventArgs e)
    {
        WindowActive = false;
        base.OnDeactivate(e);
    }

    /// <summary>
    /// Raises the Shown event.
    /// </summary>
    /// <param name="e">An EventArgs containing event data.</param>
    protected override void OnShown(EventArgs e)
    {
        // Under Windows7 a modal window with custom chrome under the DWM
        // will sometimes not be drawn when first shown.
        if (Environment.OSVersion.Version.Major >= 6)
        {
            PerformNeedPaint(true);
        }

        base.OnShown(e);
    }

    //protected override void OnPaint(PaintEventArgs e)
    //{
    //    base.OnPaint(e);
    //}

    ///// <inheritdoc />
    //protected override void OnPaintBackground(PaintEventArgs e)
    //{
    //    if (AcrylicValues.EnableAcrylic)
    //    {
    //        e.Graphics.Clear(Color.Transparent);
    //    }
    //}

    ///// <inheritdoc />
    //protected override void OnLoad(EventArgs e)
    //{
    //    /* FadeValues disabled and moved to extended until proven stable. Further development in V100
    //    if (FadeValues.FadingEnabled)
    //    {
    //        #if NET8_0_OR_GREATER
    //            KryptonFormFadeController.ModernFadeFormIn(FadeValues.Owner ?? this, FadeValues.FadeDuration);
    //        #else
    //            KryptonFormFadeController.FadeIn(FadeValues.Owner ?? this, FadeValues.FadeSpeed);
    //        #endif
    //    }
    //    */
    //    base.OnLoad(e);
    //}

    ///// <inheritdoc />
    //protected override void OnClosing(CancelEventArgs e)
    //{
    //    /* FadeValues disabled and moved to extended until proven stable. Further development in V100
    //    if (FadeValues is { FadingEnabled: true, ShouldCloseOnFadeOut: true })
    //    {
    //        #if NET8_0_OR_GREATER
    //            KryptonFormFadeController.ModernFadeFormOut(FadeValues.Owner ?? this, FadeValues.FadeDuration);
    //        #else
    //            KryptonFormFadeController.FadeOut(FadeValues.Owner ?? this, FadeValues.FadeSpeed);
    //        #endif
    //    }
    //    */
    //    base.OnClosing(e);
    //}
    #endregion

    #region Protected Virtual
    // ReSharper disable VirtualMemberNeverOverridden.Global
    /// <summary>
    /// Suspend processing of non-client painting.
    /// </summary>
    protected virtual void SuspendPaint() => _ignoreCount++;

    /// <summary>
    /// Resume processing of non-client painting.
    /// </summary>
    protected virtual void ResumePaint() => _ignoreCount--;

    /// <summary>
    /// Create the redirector instance.
    /// </summary>
    /// <returns>PaletteRedirect derived class.</returns>
    protected virtual PaletteRedirect CreateRedirector() => new PaletteRedirect(_palette);

    /// <summary>
    /// Processes a notification from palette storage of a button spec change.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An EventArgs containing event data.</param>
    protected virtual void OnButtonSpecChanged(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// Raises the PaletteChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnPaletteChanged(EventArgs e)
    {
        // Update the redirector with latest palette
        Redirector.Target = _palette;

        // A new palette source means we need to layout and redraw
        OnNeedPaint(LocalCustomPalette!, new NeedLayoutEventArgs(true));

        PaletteChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the ApplyUseThemeFormChromeBorderWidth event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnApplyUseThemeFormChromeBorderWidthChanged(EventArgs e) => ApplyUseThemeFormChromeBorderWidthChanged?.Invoke(this, e);

    /// <summary>
    /// Occurs when the UseThemeFormChromeBorderWidthChanged event is fired for the current palette.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnUseThemeFormChromeBorderWidthChanged(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// Processes a notification from palette storage of a paint and optional layout required.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    /// <exception cref="ArgumentNullException"></exception>
    protected virtual void OnNeedPaint(object? sender, [DisallowNull] NeedLayoutEventArgs e)
    {
        Debug.Assert(e != null);

        // Validate incoming reference
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        // Do we need to recalc the border size as well as invalidate?
        if (e.NeedLayout)
        {
            NeedLayout = true;
        }

        InvalidateNonClient();
    }

    /// <summary>
    /// Process Windows-based messages.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    protected override void WndProc(ref Message m)
    {
        var processed = false;

        // We do not process the message if on an MDI child, because doing so prevents the
        // LayoutMdi call on the parent from working and cascading/tiling the children
        if (_themedApp && MdiParent is null)
        {
            switch (m.Msg)
            {
                case PI.WM_.NCCALCSIZE:
                    processed = OnWM_NCCALCSIZE(ref m);
                    break;
                case PI.WM_.GETMINMAXINFO:
                    OnWM_GETMINMAXINFO(ref m);
                    /* Setting handled to false enables the application to process its own Min/Max requirements,
                     * as mentioned by jason.bullard (comment from September 22, 2011) on http://gallery.expression.microsoft.com/ZuneWindowBehavior/ */
                    // https://github.com/Krypton-Suite/Standard-Toolkit/issues/459
                    // Still got to call - base - to allow the "application to process its own Min/Max requirements" !!
                    base.WndProc(ref m);
                    return;
            }
        }

        // Do we need to override message processing?
        if (!IsDisposed && !Disposing)
        {
            switch (m.Msg)
            {
                case PI.WM_.NCPAINT:
                    processed = _ignoreCount > 0 || OnWM_NCPAINT(ref m);
                    break;

                case PI.WM_.NCHITTEST:
                    processed = OnWM_NCHITTEST(ref m);
                    break;

                case PI.WM_.NCACTIVATE:
                    processed = OnWM_NCACTIVATE(ref m);
                    break;

                case PI.WM_.NCMOUSEMOVE:
                    processed = OnWM_NCMOUSEMOVE(ref m);
                    break;

                case PI.WM_.NCLBUTTONDOWN:
                    processed = OnWM_NCLBUTTONDOWN(ref m);
                    break;

                case PI.WM_.NCLBUTTONUP:
                    processed = OnWM_NCLBUTTONUP(ref m);
                    break;

                case PI.WM_.MOUSEMOVE:
                    if (_captured)
                    {
                        processed = OnWM_MOUSEMOVE(ref m);
                    }

                    break;
                case PI.WM_.LBUTTONUP:
                    if (_captured)
                    {
                        processed = OnWM_LBUTTONUP(ref m);
                    }
                    break;

                case PI.WM_.NCMOUSELEAVE:
                    if (!_captured)
                    {
                        processed = OnWM_NCMOUSELEAVE(ref m);
                    }
                    break;

                case PI.WM_.NCLBUTTONDBLCLK:
                    processed = OnWM_NCLBUTTONDBLCLK(ref m);
                    break;
                case PI.WM_.SYSCOMMAND:
                {
                    var sc = (PI.SC_)m.WParam.ToInt64();
                    // Is this the command for closing the form?
                    if (sc == PI.SC_.CLOSE)
                    {
                        PropertyInfo? pi = typeof(Form).GetProperty(nameof(CloseReason),
                            BindingFlags.Instance |
                            BindingFlags.SetProperty |
                            BindingFlags.NonPublic);

                        // Update form with the reason for the close
                        pi?.SetValue(this, CloseReason.UserClosing, null);
                    }

                    if (sc != PI.SC_.KEYMENU)
                    {
                        processed = OnPaintNonClient(ref m);
                    }
                }
                    break;
                case PI.WM_.INITMENU:
                case PI.WM_.SETTEXT:
                case PI.WM_.HELP:
                    processed = OnPaintNonClient(ref m);
                    break;
                case 0x00AE:
                    // Mystery message causes OS title bar buttons to draw, we want to
                    // prevent that and ignoring the messages seems to do no harm.
                    processed = true;
                    break;
                case 0xC1BC:
                    // Under Windows7 a modal window with custom chrome under the DWM
                    // will sometimes not be drawn when first shown. So we spot the window
                    // message used to indicate a window is shown and manually request layout
                    // and paint of the non-client area to get it shown.
                    PerformNeedPaint(true);
                    break;
            }
        }

        // If the message has not been handled, let base class process it
        if (!processed && m.Msg != PI.WM_.GETMINMAXINFO)
        {
            base.WndProc(ref m);
            _shadowManager.WndProc(ref m);
        }

        if (m.Msg == PI.WM_.SIZE)
        {
            // Make sure sizing is completed (due to above base) before taking a clean snapshot for focus lost
            _blurManager.TakeSnapshot();
        }
    }

    /// <summary>
    /// Creates and populates the MINMAXINFO structure for a maximized window.
    /// Puts the structure into memory address given by lParam.
    /// Only used to process a WM_GETMINMAXINFO message.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    protected virtual void OnWM_GETMINMAXINFO(ref Message m)
    {
        PI.MINMAXINFO mmi = (PI.MINMAXINFO)Marshal.PtrToStructure(m.LParam, typeof(PI.MINMAXINFO))!;

        // Adjust the maximized size and position to fit the work area of the correct monitor
        const int MONITOR_DEFAULT_TO_NEAREST = 0x00000002;
        IntPtr monitor = PI.MonitorFromWindow(m.HWnd, MONITOR_DEFAULT_TO_NEAREST);

        if (monitor != IntPtr.Zero)
        {
            PI.MONITORINFO monitorInfo = PI.GetMonitorInfo(monitor);
            PI.RECT rcWorkArea = monitorInfo.rcWork;
            PI.RECT rcMonitorArea = monitorInfo.rcMonitor;
            mmi.ptMaxPosition.X = Math.Abs(rcWorkArea.left - rcMonitorArea.left);
            mmi.ptMaxPosition.Y = Math.Abs(rcWorkArea.top - rcMonitorArea.top);
            mmi.ptMaxSize.X = Math.Abs(rcWorkArea.right - rcWorkArea.left);
            mmi.ptMaxSize.Y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
            // https://github.com/Krypton-Suite/Standard-Toolkit/issues/415 so changed to "* 3 / 2"
            mmi.ptMinTrackSize.X = Math.Max(mmi.ptMinTrackSize.X * 3 / 2, MinimumSize.Width);
            mmi.ptMinTrackSize.Y = Math.Max(mmi.ptMinTrackSize.Y * 2, MinimumSize.Height);

            // https://github.com/Krypton-Suite/Standard-Toolkit/issues/459
            if (MaximumSize.Width > mmi.ptMinTrackSize.X
                && MaximumSize.Width < mmi.ptMaxSize.X)
            {
                mmi.ptMaxSize.X = MaximumSize.Width;
            }
            if (MaximumSize.Height > mmi.ptMinTrackSize.Y
                && MaximumSize.Height < mmi.ptMaxSize.Y)
            {
                mmi.ptMaxSize.Y = MaximumSize.Height;
            }
        }

        Marshal.StructureToPtr(mmi, m.LParam, true);
    }

    /// <summary>
    /// Process the WM_NCCALCSIZE message when overriding window chrome.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    /// <returns>True if the message was processed; otherwise false.</returns>
    protected virtual bool OnWM_NCCALCSIZE(ref Message m)
    {
        // Does the LParam contain a RECT or an NCCALCSIZE_PARAMS
        if (m.WParam != IntPtr.Zero)
        {
            // Get the border sizing needed around the client area
            Padding borders = RealWindowBorders;

            // Extract the Win32 NCCALCSIZE_PARAMS structure from LPARAM
            PI.NCCALCSIZE_PARAMS calcsize = (PI.NCCALCSIZE_PARAMS)m.GetLParam(typeof(PI.NCCALCSIZE_PARAMS))!;

            // Reduce provided RECT by the borders
            calcsize.rectProposed.left += borders.Left;
            calcsize.rectProposed.top += borders.Top;
            calcsize.rectProposed.right -= borders.Right;
            calcsize.rectProposed.bottom -= borders.Bottom;

            // Put back the modified structure
            Marshal.StructureToPtr(calcsize, m.LParam, false);
        }

        // Message processed, do not pass onto base class for processing
        return true;
    }

    /// <summary>
    /// Process the WM_NCPAINT message when overriding window chrome.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    /// <returns>True if the message was processed; otherwise false.</returns>
    protected virtual bool OnWM_NCPAINT(ref Message m)
    {
        // Perform actual paint operation
        if (!_disposing)
        {
            OnNonClientPaint(m.HWnd);
        }

        // We have handled the message
        m.Result = (IntPtr)1;

        // Message processed, do not pass onto base class for processing
        return true;
    }

    /// <summary>
    /// Process the WM_NCHITTEST message when overriding window chrome.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    /// <returns>True if the message was processed; otherwise false.</returns>
    protected virtual bool OnWM_NCHITTEST(ref Message m)
    {
        // Extract the point in screen coordinates
        var screenPoint = new Point((int)m.LParam.ToInt64());

        // Convert to window coordinates
        Point windowPoint = ScreenToWindow(screenPoint);

        // Perform hit testing
        m.Result = WindowChromeHitTest(windowPoint);

        // Message processed, do not pass onto base class for processing
        return true;
    }


    /// <summary>
    /// Process the WM_NCACTIVATE message when overriding window chrome.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    /// <returns>True if the message was processed; otherwise false.</returns>
    protected virtual bool OnWM_NCACTIVATE(ref Message m)
    {
        // Cache the new active state
        WindowActive = m.WParam == (IntPtr)1;

        // The first time an MDI child gets an WM_NCACTIVATE, let it process as normal
        if ((MdiParent != null) && !_activated)
        {
            _activated = true;
        }
        else
        {
            // Allow default processing of activation change
            m.Result = (IntPtr)1;

            // Message processed, do not pass onto base class for processing
            return true;
        }

        return false;
    }

    /// <summary>
    /// Process a windows message that requires the non client area be repainted.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    /// <returns>True if the message was processed; otherwise false.</returns>
    protected virtual bool OnPaintNonClient(ref Message m)
    {
        // Let window be updated with new text
        DefWndProc(ref m);

        // Need a repaint to show change
        InvalidateNonClient();

        // Message processed, do not pass onto base class for processing
        return true;
    }

    /// <summary>
    /// Process the WM_NCMOUSEMOVE message when overriding window chrome.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    /// <returns>True if the message was processed; otherwise false.</returns>
    protected virtual bool OnWM_NCMOUSEMOVE(ref Message m)
    {
        // Extract the point in screen coordinates
        var screenPoint = new Point((int)m.LParam.ToInt64());

        // Convert to window coordinates
        Point windowPoint = ScreenToWindow(screenPoint);

        // Perform actual mouse movement actions
        WindowChromeNonClientMouseMove(windowPoint);

        // If we are not tracking when the mouse leaves the non-client window
        if (!_trackingMouse)
        {
            var tme = new PI.TRACKMOUSEEVENTS
            {
                // This structure needs to know its own size in bytes
                cbSize = (uint)Marshal.SizeOf(typeof(PI.TRACKMOUSEEVENTS)),
                dwHoverTime = 100,

                // We need to know then the mouse leaves the non client window area
                dwFlags = PI.TME_LEAVE | PI.TME_NONCLIENT,

                // We want to track our own window
                hWnd = Handle
            };

            // Call Win32 API to start tracking
            PI.TrackMouseEvent(ref tme);

            // Do not need to track again until mouse reenters the window
            _trackingMouse = true;
        }

        // Indicate that we processed the message
        m.Result = IntPtr.Zero;

        // Message processed, do not pass onto base class for processing
        return true;
    }

    /// <summary>
    /// Process the WM_NCLBUTTONDOWN message when overriding window chrome.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>4
    /// <returns>True if the message was processed; otherwise false.</returns>
    protected virtual bool OnWM_NCLBUTTONDOWN(ref Message m)
    {
        // Extract the point in screen coordinates
        var screenPoint = new Point((int)m.LParam.ToInt64());

        // Convert to window coordinates
        Point windowPoint = ScreenToWindow(screenPoint);

        // Perform actual mouse down processing
        return WindowChromeLeftMouseDown(windowPoint);
    }

    /// <summary>
    /// Process the WM_LBUTTONUP message when overriding window chrome.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    /// <returns>True if the message was processed; otherwise false.</returns>
    protected virtual bool OnWM_NCLBUTTONUP(ref Message m)
    {
        // Extract the point in screen coordinates
        var screenPoint = new Point((int)m.LParam.ToInt64());

        // Convert to window coordinates
        Point windowPoint = ScreenToWindow(screenPoint);

        // Perform actual mouse up processing
        return WindowChromeLeftMouseUp(windowPoint);
    }

    /// <summary>
    /// Process the WM_NCMOUSELEAVE message when overriding window chrome.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    /// <returns>True if the message was processed; otherwise false.</returns>
    protected virtual bool OnWM_NCMOUSELEAVE(ref Message m)
    {
        _blurManager.TakeSnapshot();
        // Next time the mouse enters the window we need to track it leaving
        _trackingMouse = false;

        // Perform actual mouse leave actions
        WindowChromeMouseLeave();

        // Indicate that we processed the message
        m.Result = IntPtr.Zero;

        // Need a repaint to show change
        InvalidateNonClient();

        // Message processed, do not pass onto base class for processing
        return true;
    }

    /// <summary>
    /// Process the OnWM_MOUSEMOVE message when overriding window chrome.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    /// <returns>True if the message was processed; otherwise false.</returns>
    protected virtual bool OnWM_MOUSEMOVE(ref Message m)
    {
        // Extract the point in client coordinates
        var clientPoint = new Point((int)m.LParam);

        // Convert to screen coordinates
        Point screenPoint = PointToScreen(clientPoint);

        // Convert to window coordinates
        Point windowPoint = ScreenToWindow(screenPoint);

        // Perform actual mouse movement actions
        WindowChromeNonClientMouseMove(windowPoint);

        return true;
    }

    /// <summary>
    /// Process the WM_LBUTTONUP message when overriding window chrome.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    /// <returns>True if the message was processed; otherwise false.</returns>
    protected virtual bool OnWM_LBUTTONUP(ref Message m)
    {
        // Capture has now expired
        _captured = false;
        Capture = false;

        // Next time the mouse enters the window we need to track it leaving
        _trackingMouse = false;

        // Extract the point in client coordinates
        var clientPoint = new Point((int)m.LParam);

        // Convert to screen coordinates
        Point screenPoint = PointToScreen(clientPoint);

        // Convert to window coordinates
        Point windowPoint = ScreenToWindow(screenPoint);

        // Pass message onto the view elements
        ViewManager?.MouseUp(new MouseEventArgs(MouseButtons.Left, 0, windowPoint.X, windowPoint.Y, 0), windowPoint);

        // Pass message onto the view elements
        ViewManager?.MouseLeave(EventArgs.Empty);

        // Need a repaint to show change
        InvalidateNonClient();

        return true;
    }

    /// <summary>
    /// Process the WM_NCLBUTTONDBLCLK message when overriding window chrome.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    /// <returns>True if the message was processed; otherwise false.</returns>
    protected virtual bool OnWM_NCLBUTTONDBLCLK(ref Message m)
    {
        // Extract the point in screen coordinates
        var screenPoint = new Point((int)m.LParam.ToInt64());

        // Convert to window coordinates
        Point windowPoint = ScreenToWindow(screenPoint);

        // Find the view element under the mouse
        ViewBase? pointView = ViewManager?.Root.ViewFromPoint(windowPoint);

        // Try and find a mouse controller for the active view
        IMouseController? controller = pointView?.FindMouseController();

        // Eat the message
        return controller != null;
    }

    /// <summary>
    /// Perform chrome window painting in the non-client areas.
    /// </summary>
    /// <param name="hWnd">Window handle of window being painted.</param>
    protected virtual void OnNonClientPaint(IntPtr hWnd)
    {
        // Create rectangle that encloses the entire window
        Rectangle windowBounds = RealWindowRectangle;

        // We can only draw a window that has some size
        if (windowBounds is { Width: > 0, Height: > 0 })
        {
            // Get the device context for this window
            IntPtr hDC = PI.GetWindowDC(Handle);

            // If we managed to get a device context
            if (hDC != IntPtr.Zero)
            {
                try
                {
                    // Find the rectangle that covers the client area of the form
                    Padding borders = RealWindowBorders;

                    var clipClientRect = new Rectangle(borders.Left, borders.Top,
                        windowBounds.Width - borders.Horizontal, windowBounds.Height - borders.Vertical);

                    var minimized = CommonHelper.IsFormMinimized(this);

                    // After excluding the client area, is there anything left to draw?
                    if (minimized || clipClientRect is { Width: > 0, Height: > 0 })
                    {
                        // If not minimized we need to clip the client area
                        if (!minimized)
                        {
                            // Exclude client area from being drawn into and bit blitted
                            PI.ExcludeClipRect(hDC, clipClientRect.Left, clipClientRect.Top,
                                clipClientRect.Right, clipClientRect.Bottom);
                        }

                        // Create one the correct size and cache for future drawing
                        IntPtr hBitmap = PI.CreateCompatibleBitmap(hDC, windowBounds.Width, windowBounds.Height);

                        // If we managed to get a compatible bitmap
                        if (hBitmap != IntPtr.Zero)
                        {
                            // Must use the screen device context for the bitmap when drawing into the
                            // bitmap otherwise the Opacity and RightToLeftLayout will not work correctly.
                            // Select the new bitmap into the screen DC
                            IntPtr oldBitmap = PI.SelectObject(_screenDC, hBitmap);

                            try
                            {
                                // Drawing is easier when using a Graphics instance
                                using (Graphics g = Graphics.FromHdc(_screenDC))
                                {
                                    WindowChromePaint(g, windowBounds);
                                }

                                // Now blit from the bitmap to the screen
                                PI.BitBlt(hDC, 0, 0, windowBounds.Width, windowBounds.Height, _screenDC, 0, 0, PI.SRCCOPY);
                            }
                            finally
                            {
                                // Restore the original bitmap
                                PI.SelectObject(_screenDC, oldBitmap);

                                // Delete the temporary bitmap
                                PI.DeleteObject(hBitmap);
                            }
                        }
                        else
                        {
                            // Drawing is easier when using a Graphics instance
                            using Graphics g = Graphics.FromHdc(hDC);
                            WindowChromePaint(g, windowBounds);
                        }
                    }
                }
                finally
                {
                    // Must always release the device context
                    PI.ReleaseDC(Handle, hDC);
                }
            }
        }

        // Bump the number of paints that have occurred
        PaintCount++;
    }

    /// <summary>
    /// Called when the active state of the window changes.
    /// </summary>
    protected virtual void OnWindowActiveChanged() => WindowActiveChanged?.Invoke(this, EventArgs.Empty);

    /// <summary>
    /// Gets and sets the need to layout the view.
    /// </summary>
    protected bool NeedLayout { get; set; }
    // ReSharper restore VirtualMemberNeverOverridden.Global
    #endregion

    #region Protected Chrome
    /// <summary>
    /// Perform setup for custom chrome.
    /// </summary>
    protected virtual void WindowChromeStart()
    {
    }

    /// <summary>
    /// Perform cleanup when custom chrome ending.
    /// </summary>
    protected virtual void WindowChromeEnd()
    {
    }

    /// <summary>
    /// Perform hit testing.
    /// </summary>
    /// <param name="pt">Point in window coordinates.</param>
    /// <returns></returns>
    protected virtual IntPtr WindowChromeHitTest(Point pt) => (IntPtr)PI.HT.CLIENT;

    /// <summary>
    /// Perform painting of the window chrome.
    /// </summary>
    /// <param name="g">Graphics instance to use for drawing.</param>
    /// <param name="bounds">Bounds enclosing the window chrome.</param>
    protected virtual void WindowChromePaint(Graphics g, Rectangle bounds)
    {
    }

    /// <summary>
    /// Perform non-client mouse movement processing.
    /// </summary>
    /// <param name="pt">Point in window coordinates.</param>
    protected virtual void WindowChromeNonClientMouseMove(Point pt) => ViewManager?.MouseMove(new MouseEventArgs(MouseButtons.None, 0, pt.X, pt.Y, 0), pt);

    /// <summary>
    /// Process the left mouse down event.
    /// </summary>
    /// <param name="windowPoint">Window coordinate of the mouse down.</param>
    /// <returns>True if event is processed; otherwise false.</returns>
    protected virtual bool WindowChromeLeftMouseDown(Point windowPoint)
    {
        ViewManager?.MouseDown(new MouseEventArgs(MouseButtons.Left, 1, windowPoint.X, windowPoint.Y, 0), windowPoint);

        // If we moused down on an active view element
        // Ask the controller if the mouse down should be ignored by wnd proc processing
        IMouseController? controller = ViewManager?.ActiveView?.FindMouseController();
        return controller is { IgnoreVisualFormLeftButtonDown: true };
    }

    /// <summary>
    /// Process the left mouse up event.
    /// </summary>
    /// <param name="pt">Window coordinate of the mouse up.</param>
    /// <returns>True if event is processed; otherwise false.</returns>
    protected virtual bool WindowChromeLeftMouseUp(Point pt)
    {
        ViewManager?.MouseUp(new MouseEventArgs(MouseButtons.Left, 0, pt.X, pt.Y, 0), pt);

        // By default, we have not handled the mouse up event
        return false;
    }

    /// <summary>
    /// Perform mouse leave processing.
    /// </summary>
    protected virtual void WindowChromeMouseLeave() =>
        // Pass message onto the view elements
        ViewManager?.MouseLeave(EventArgs.Empty);

    #endregion

    #region Implementation
    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        // We only care if we are using the global palette
        if (PaletteMode == PaletteMode.Global)
        {
            // Update ourself with the new global palette
            _localCustomPalette = null;
            SetPalette(KryptonManager.CurrentGlobalPalette);
            Redirector.Target = _palette;

            // A new palette source means we need to layout and redraw
            OnNeedPaint(LocalCustomPalette!, new NeedLayoutEventArgs(true));

            GlobalPaletteChanged?.Invoke(sender, e);
        }
    }

    private void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
        // If a change has occurred that could affect the color table then it needs regenerating
        switch (e.Category)
        {
            case UserPreferenceCategory.Icon:
            case UserPreferenceCategory.Menu:
            case UserPreferenceCategory.Color:
            case UserPreferenceCategory.VisualStyle:
            case UserPreferenceCategory.General:
            case UserPreferenceCategory.Window:
            case UserPreferenceCategory.Desktop:
                PerformNeedPaint(true);
                break;
        }
    }

    private void SetPalette([DisallowNull] PaletteBase palette)
    {
        if (palette != _palette)
        {
            // Unhook from current palette events
            if (_palette != null!)  // Will be null on first set !
            {
                _palette.PalettePaint -= OnNeedPaint;
                _palette.ButtonSpecChanged -= OnButtonSpecChanged;
                _palette.UseThemeFormChromeBorderWidthChanged -= OnUseThemeFormChromeBorderWidthChanged;
                _palette.BasePaletteChanged -= OnBaseChanged;
                _palette.BaseRendererChanged -= OnBaseChanged;
            }

            // Remember the new palette
            _palette = palette;

            // Get the renderer associated with the palette
            Renderer = _palette.GetRenderer();

            // Hook to new palette events
            _palette.PalettePaint += OnNeedPaint;
            _palette.ButtonSpecChanged += OnButtonSpecChanged;
            _palette.UseThemeFormChromeBorderWidthChanged += OnUseThemeFormChromeBorderWidthChanged;
            _palette.BasePaletteChanged += OnBaseChanged;
            _palette.BaseRendererChanged += OnBaseChanged;
            // PaletteImageScaler.ScalePalette(FactorDpiX, FactorDpiY, _palette);
        }
    }

    private void OnBaseChanged(object? sender, EventArgs e) =>
        // Change in base renderer or base palette require we fetch the latest renderer
        Renderer = _palette.GetRenderer();// PaletteImageScaler.ScalePalette(FactorDpiX, FactorDpiY, _palette);

#if !NET462
    private void OnDpiChanged(object? sender, DpiChangedEventArgs e) => UpdateDpiFactors();
#endif
    #endregion

    private void UpdateDpiFactors()
    {
        // Do not use the control dpi, as these values are being used to target the screen
        IntPtr screenDc = PI.GetDC(IntPtr.Zero);
        if (screenDc != IntPtr.Zero)
        {
            FactorDpiX = PI.GetDeviceCaps(screenDc, PI.DeviceCap.LOGPIXELSX) / 96f;
            FactorDpiY = PI.GetDeviceCaps(screenDc, PI.DeviceCap.LOGPIXELSY) / 96f;
            PI.ReleaseDC(IntPtr.Zero, screenDc);
        }
        else
        {
            // Do it the slow "init everything long way"
            using Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
            FactorDpiX = graphics.DpiX / 96f;
            FactorDpiY = graphics.DpiY / 96f;
        }
        // _palette.HasAlreadyBeenScaled = false;
        // PaletteImageScaler.ScalePalette(FactorDpiX, FactorDpiY, _palette);
    }

    private void InitializeComponent()
    {
        SuspendLayout();
        //
        // VisualForm
        //
        ClientSize = new Size(284, 261);
        Name = "VisualForm";
        ResumeLayout(false);
    }
}
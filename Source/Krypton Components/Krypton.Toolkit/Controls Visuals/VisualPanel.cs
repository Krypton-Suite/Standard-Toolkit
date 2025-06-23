#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Base class used for implementation of panel controls.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public abstract class VisualPanel : Panel,
    ISupportInitializeNotification,
    IKryptonDebug
{
    #region Static Field
    private static MethodInfo _miPTB;
    #endregion

    #region Instance Fields

    private bool _refresh;
    private bool _refreshAll;
    private bool _layoutDirty;
    private bool _paintTransparent;
    private bool _evalTransparent;
    private bool _globalEvents;
    private Size _lastLayoutSize;
    private PaletteBase? _localPalette;
    private PaletteBase? _palette;
    private PaletteMode _paletteMode;
    private readonly SimpleCall _refreshCall;
    private KryptonContextMenu? _kryptonContextMenu;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the control is initialized.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the control has been fully initialized.")]
    public event EventHandler? Initialized;

    /// <summary>
    /// Occurs when the palette changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the value of the Palette property is changed.")]
    public event EventHandler? PaletteChanged;

    /// <summary>
    /// Occurs when the Global palette changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the value of the GlobalPalette property is changed.")]
    public event EventHandler? GlobalPaletteChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the VisualPanel class.
    /// </summary>
    protected VisualPanel()
    {
        #region Default ControlStyle Values
        // Default style values for Control are:-
        //    True  - AllPaintingInWmPaint
        //    False - CacheText
        //    False - ContainerControl
        //    False - EnableNotifyMessage
        //    False - FixedHeight
        //    False - FixedWidth
        //    False - Opaque
        //    False - OptimizedDoubleBuffer
        //    False - ResizeRedraw
        //    True  - Selectable
        //    True  - StandardClick
        //    True  - StandardDoubleClick
        //    False - SupportsTransparentBackColor
        //    False - UserMouse
        //    True  - UserPaint
        //    True  - UseTextForAccessibility
        #endregion

        // We use double buffering to reduce drawing flicker
        SetStyle(ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint, true);

        // We need to allow a transparent background
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);

        // We need to repaint entire control whenever resized
        SetStyle(ControlStyles.ResizeRedraw, true);

        // We act as a container for child controls
        SetStyle(ControlStyles.ContainerControl, true);

        // Cannot select a panel
        SetStyle(ControlStyles.Selectable, false);

        // Yes, we want to be drawn double buffered by default
        base.DoubleBuffered = true;

        // Setup the invoke used to refresh display
        _refreshCall = OnPerformRefresh;

        // Setup the need paint delegate
        NeedPaintDelegate = OnNeedPaint;

        // Must layout before first draw attempt
        _layoutDirty = true;
        _evalTransparent = true;
        _lastLayoutSize = Size.Empty;

        // Set the palette to the defaults as specified by the manager
        _localPalette = null;
        SetPalette(KryptonManager.CurrentGlobalPalette);
        _paletteMode = PaletteMode.Global;

        // Create constant target for resolving palette delegates
        Redirector = new PaletteRedirect(_palette);

        AttachGlobalEvents();
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Unhook from any current menu strip
            if (base.ContextMenuStrip is not null)
            {
                base.ContextMenuStrip.Opening -= OnContextMenuStripOpening;
                base.ContextMenuStrip.Closed -= OnContextMenuClosed;
                base.ContextMenuStrip = null;
            }

            // Must unhook from the palette paint event
            if (_palette is not null)
            {
                _palette.PalettePaint -= OnNeedPaint;
                _palette.ButtonSpecChanged -= OnButtonSpecChanged;
            }

            UnattachGlobalEvents();
            ViewManager?.Dispose();

            _palette = null;
            Renderer = null;
            _localPalette = null;
            if (Redirector is not null)
            {
                Redirector.Target = null;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Signals the object that initialization is starting.
    /// </summary>
    public virtual void BeginInit()
    {
        // Remember that fact we are inside a BeginInit/EndInit pair
        IsInitializing = true;

        // No need to layout the view during initialization
        SuspendLayout();
    }

    /// <summary>
    /// Signals the object that initialization is complete.
    /// </summary>
    public virtual void EndInit()
    {
        // We are now initialized
        IsInitialized = true;

        // We are no longer initializing
        IsInitializing = false;

        // We always need a paint and layout
        OnNeedPaint(this, new NeedLayoutEventArgs(true));

        // Should layout once initialization is complete
        ResumeLayout(true);

        // Raise event to show control is now initialized
        OnInitialized(EventArgs.Empty);
    }

    /// <summary>
    /// Gets a value indicating if the control is initialized.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsInitialized
    {
        [DebuggerStepThrough]
        get;
        private set;
    }

    /// <summary>
    /// Gets a value indicating if the control is initialized.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsInitializing
    {
        [DebuggerStepThrough]
        get;
        private set;
    }

    /// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with this control.</summary>
    /// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> for this control, or <see langword="null" /> if there is no <see cref="T:System.Windows.Forms.ContextMenuStrip" />. The default is <see langword="null" />.</returns>
    [Category(@"Behavior")]
    [Description(@"Consider using KryptonContextMenu within the behaviors section.\nThe Winforms shortcut menu to show when the user right-clicks the page.\nNote: The ContextMenu will be rendered.")]
    [DefaultValue(null)]
    public override ContextMenuStrip? ContextMenuStrip
    {
        [DebuggerStepThrough]
        get => base.ContextMenuStrip;

        set
        {
            // Unhook from any current menu strip
            if (base.ContextMenuStrip is not null)
            {
                base.ContextMenuStrip.Opening -= OnContextMenuStripOpening;
                base.ContextMenuStrip.Closed -= OnContextMenuClosed;
            }

            // Let parent handle actual storage
            base.ContextMenuStrip = value;

            // Hook into the strip being shown (so we can set the correct renderer)
            if (base.ContextMenuStrip is not null)
            {
                base.ContextMenuStrip.Opening += OnContextMenuStripOpening;
                base.ContextMenuStrip.Closed += OnContextMenuClosed;
            }
        }
    }

    /// <summary>
    /// Gets and sets the KryptonContextMenu to show when right clicked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The KryptonContextMenu to show when the user right-clicks the Control.")]
    [DefaultValue(null)]
    public virtual KryptonContextMenu? KryptonContextMenu
    {
        get => _kryptonContextMenu;

        set
        {
            if (_kryptonContextMenu != value)
            {
                if (_kryptonContextMenu is not null)
                {
                    _kryptonContextMenu.Closed -= OnContextMenuClosed;
                    _kryptonContextMenu.Disposed -= OnKryptonContextMenuDisposed;
                }

                _kryptonContextMenu = value;

                if (_kryptonContextMenu is not null)
                {
                    _kryptonContextMenu.Closed += OnContextMenuClosed;
                    _kryptonContextMenu.Disposed += OnKryptonContextMenuDisposed;
                }
            }
        }
    }

    /// <summary>
    /// Fires the NeedPaint event.
    /// </summary>
    /// <param name="needLayout">Does the palette change require a layout.</param>
    public void PerformNeedPaint(bool needLayout) => OnNeedPaint(this, new NeedLayoutEventArgs(needLayout));

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
                        _localPalette = null;
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

    private bool ShouldSerializePaletteMode() => PaletteMode != PaletteMode.Global;

    /// <summary>
    /// Resets the PaletteMode property to its default value.
    /// </summary>
    public void ResetPaletteMode() => PaletteMode = PaletteMode.Global;

    /// <summary>
    /// Gets and sets the custom palette implementation.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Custom palette applied to drawing.")]
    [DefaultValue(null)]
    public PaletteBase? Palette
    {
        [DebuggerStepThrough]
        get => _localPalette;

        set
        {
            // Only interested in changes of value
            if (_localPalette != value)
            {
                // Remember the starting palette
                PaletteBase? old = _localPalette;

                // Use the provided palette value
                SetPalette(value);

                // If no custom palette is required
                if (value == null)
                {
                    // No custom palette, so revert back to the global setting
                    _paletteMode = PaletteMode.Global;

                    // Get the appropriate palette for the global mode
                    _localPalette = null;
                    SetPalette(KryptonManager.GetPaletteForMode(_paletteMode));
                }
                else
                {
                    // No longer using a standard palette
                    _localPalette = value;
                    _paletteMode = PaletteMode.Custom;
                }

                // If real change has occurred
                if (old != _localPalette)
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
    public void ResetPalette() => PaletteMode = PaletteMode.Global;

    /// <summary>
    /// Gets access to the current renderer.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IRenderer? Renderer
    {
        [DebuggerStepThrough]
        get;
        private set;
    }

    /// <summary>
    /// Create a tool strip renderer appropriate for the current renderer/palette pair.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public ToolStripRenderer CreateToolStripRenderer() => Renderer!.RenderToolStrip(GetResolvedPalette());

    /// <summary>
    /// Gets or sets the background image displayed in the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Image? BackgroundImage
    {
        get => base.BackgroundImage;
        set => base.BackgroundImage = value;
    }

    /// <summary>
    /// Gets or sets the background image layout.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override ImageLayout BackgroundImageLayout
    {
        get => base.BackgroundImageLayout;
        set => base.BackgroundImageLayout = value;
    }

    /// <summary>
    /// Attach the control to global events.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void AttachGlobalEvents()
    {
        if (!_globalEvents)
        {
            UpdateGlobalEvents(true);
            _globalEvents = true;
        }
    }

    /// <summary>
    /// Attach the control to global events.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void UnattachGlobalEvents()
    {
        if (_globalEvents)
        {
            UpdateGlobalEvents(false);
            _globalEvents = false;
        }
    }

    /// <summary>
    /// Process Windows-based messages.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    protected override void WndProc(ref Message m)
    {
        // We need to snoop the need to show a context menu
        if (m.Msg == PI.WM_.CONTEXTMENU)
        {
            // Only interested in overriding the behaviour when we have a krypton context menu...
            if (KryptonContextMenu is not null)
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
                    // Show the context menu
                    KryptonContextMenu?.Show(this, PointToScreen(mousePt));

                    // We eat the message!
                    return;
                }
            }
        }

        base.WndProc(ref m);
    }

    /// <summary>
    /// Called when a context menu has just been closed.
    /// </summary>
    protected virtual void ContextMenuClosed()
    {
    }
    #endregion

    #region Public Override
    /// <summary>
    /// Gets or sets the background color for the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
    }

    /// <summary>
    /// Gets or sets the font of the text displayed by the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [AmbientValue(null)]
    [AllowNull]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Font Font
    {
        get => base.Font;
        set => base.Font = value;
    }

    /// <summary>
    /// Gets or sets the foreground color for the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color ForeColor
    {
        get => base.ForeColor;
        set => base.ForeColor = value;
    }

    /// <summary>
    /// Gets or sets the border style for the VisualPanel. 
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new BorderStyle BorderStyle
    {
        get => base.BorderStyle;
        set => base.BorderStyle = value;
    }
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
    public int KryptonLayoutCounter => ViewManager!.LayoutCounter;

    /// <summary>
    /// Gets the number of paint cycles performed since last reset.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int KryptonPaintCounter => ViewManager!.PaintCounter;

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
    protected PaletteRedirect? Redirector
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
    /// Processes a notification from palette storage of a paint and optional layout required.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    /// <exception cref="ArgumentNullException"></exception>
    protected void OnNeedPaint(object? sender, [DisallowNull] NeedLayoutEventArgs e)
    {
        Debug.Assert(e is not null);

        // Validate incoming reference
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        // Change in setting means we need to evaluate transparent painting
        _evalTransparent = true;

        // If required, layout the control
        if (e.NeedLayout)
        {
            _layoutDirty = true;
        }

        if (IsHandleCreated && (!_refreshAll || !e.InvalidRect.IsEmpty))
        {
            // Always request the repaint immediately
            if (e.InvalidRect.IsEmpty)
            {
                _refreshAll = true;
                Invalidate();
            }
            else
            {
                Invalidate(e.InvalidRect);
            }

            // Do we need to use an Invoke to force repaint?
            if (!_refresh && EvalInvokePaint)
            {
                BeginInvoke(_refreshCall);
            }

            // A refresh is outstanding
            _refresh = true;
        }
    }

    /// <summary>
    /// Gets a value indicating if transparent paint is needed
    /// </summary>
    protected bool NeedTransparentPaint
    {
        get
        {
            // Do we need to evaluate the need for a transparent paint
            if (_evalTransparent)
            {
                _paintTransparent = EvalTransparentPaint();

                // Answer is cached until paint values are changed
                _evalTransparent = false;
            }

            return _paintTransparent;
        }
    }

    /// <summary>
    /// Gets the resolved palette to actually use when drawing.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public PaletteBase GetResolvedPalette() => _palette!;

    #endregion

    #region Protected Virtual
    // ReSharper disable VirtualMemberNeverOverridden.Global
    /// <summary>
    /// Raises the Initialized event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnInitialized(EventArgs e) => Initialized?.Invoke(this, EventArgs.Empty);

    /// <summary>
    /// Raises the PaletteChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnPaletteChanged(EventArgs e)
    {
        // Update the redirector with latest palette
        Redirector!.Target = _palette;

        // A new palette source means we need to layout and redraw
        OnNeedPaint(Palette!, new NeedLayoutEventArgs(true));

        PaletteChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Work out if this control needs to paint transparent areas.
    /// </summary>
    /// <returns>True if paint required; otherwise false.</returns>
    protected virtual bool EvalTransparentPaint() =>
        // Do we have a manager to use for asking about painting?
        ViewManager is not null && ViewManager.EvalTransparentPaint(Renderer!);

    /// <summary>
    /// Work out if this control needs to use Invoke to force a repaint.
    /// </summary>
    protected virtual bool EvalInvokePaint => false;

    /// <summary>
    /// Gets the control reference that is the parent for transparent drawing.
    /// </summary>
    protected virtual Control? TransparentParent => Parent;

    /// <summary>
    /// Processes a notification from palette storage of a button spec change.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An EventArgs containing event data.</param>
    /// <exception cref="ArgumentNullException"></exception>
    protected virtual void OnButtonSpecChanged(object? sender, [DisallowNull] EventArgs e)
    {
        Debug.Assert(e is not null);

        // Validate incoming reference
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }
    }

    /// <summary>
    /// Update global event attachments.
    /// </summary>
    /// <param name="attach">True if attaching; otherwise false.</param>
    protected virtual void UpdateGlobalEvents(bool attach)
    {
        if (attach)
        {
            KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
            SystemEvents.UserPreferenceChanged += OnUserPreferenceChanged;
        }
        else
        {
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
            SystemEvents.UserPreferenceChanged -= OnUserPreferenceChanged;
        }
    }
    // ReSharper restore VirtualMemberNeverOverridden.Global
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Gets the default size of the control.
    /// </summary>
    protected override Size DefaultSize => new Size(100, 100);

    /// <summary>
    /// Raises the RightToLeftChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing event data.</param>
    protected override void OnRightToLeftChanged(EventArgs e)
    {
        OnNeedPaint(null, new NeedLayoutEventArgs(true));
        base.OnRightToLeftChanged(e);
    }

    /// <summary>
    /// Processes a command key.
    /// </summary>
    /// <param name="msg">A Message, passed by reference, that represents the window message to process.</param>
    /// <param name="keyData">One of the Keys values that represents the key to process.</param>
    /// <returns>True is handled; otherwise false.</returns>
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        // If we have a defined context menu then need to check for matching shortcut
        if (KryptonContextMenu is not null)
        {
            if (KryptonContextMenu.ProcessShortcut(keyData))
            {
                return true;
            }
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }

    /// <summary>
    /// Raises the Layout event.
    /// </summary>
    /// <param name="levent">A LayoutEventArgs that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
        // Cannot process a message for a disposed control
        if (!IsDisposed)
        {
            // Do we have a manager to use for laying out?
            if (ViewManager is not null)
            {
                // Prevent infinite loop by looping a maximum number of times
                var max = 5;

                do
                {
                    // Layout cannot now be dirty
                    _layoutDirty = false;

                    // Ask the view to perform a layout
                    ViewManager?.Layout(Renderer!);

                } while (_layoutDirty && (max-- > 0));

                // Remember size when last layout was performed
                _lastLayoutSize = Size;
            }
        }

        // Let base class layout child controls
        base.OnLayout(levent);
    }

    /// <summary>
    /// Raises the Paint event.
    /// </summary>
    /// <param name="e">A PaintEventArgs that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs? e)
    {
        // Cannot process a message for a disposed control
        if (!IsDisposed)
        {
            // Do we have a manager to use for painting?
            if (ViewManager is not null)
            {
                // If the layout is dirty, or the size of the control has changed 
                // without a layout being performed, then perform a layout now
                if (_layoutDirty && (!Size.Equals(_lastLayoutSize)))
                {
                    PerformLayout();
                }

                // Draw the background as transparent, by drawing parent
                PaintTransparentBackground(e);

                // Ask the view to repaint the visual structure
                ViewManager?.Paint(Renderer!, e);

                // Request for a refresh has been serviced
                _refresh = false;
                _refreshAll = false;
            }
        }
    }

    /// <summary>
    /// Raises the MouseMove event.
    /// </summary>
    /// <param name="e">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
        // Cannot process a message for a disposed control
        if (!IsDisposed)
        {
            // Do we have a manager for processing mouse messages?
            ViewManager?.MouseMove(e, new Point(e.X, e.Y));
        }

        // Let base class fire events
        base.OnMouseMove(e);
    }

    /// <summary>
    /// Raises the MouseDown event.
    /// </summary>
    /// <param name="e">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
        // Cannot process a message for a disposed control
        if (!IsDisposed)
        {
            // Do we have a manager for processing mouse messages?
            ViewManager?.MouseDown(e, new Point(e.X, e.Y));
        }

        // Let base class fire events
        base.OnMouseDown(e);
    }

    /// <summary>
    /// Raises the MouseUp event.
    /// </summary>
    /// <param name="e">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseUp(MouseEventArgs e)
    {
        // Cannot process a message for a disposed control
        if (!IsDisposed)
        {
            // Do we have a manager for processing mouse messages?
            ViewManager?.MouseUp(e, new Point(e.X, e.Y));
        }

        // Let base class fire events
        base.OnMouseUp(e);
    }

    /// <summary>
    /// Raises the MouseLeave event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
        // Cannot process a message for a disposed control
        if (!IsDisposed)
        {
            // Do we have a manager for processing mouse messages?
            ViewManager?.MouseLeave(e);
        }

        // Let base class fire events
        base.OnMouseLeave(e);
    }

    /// <summary>
    /// Raises the DoubleClick event.
    /// </summary>
    /// <param name="e">A KeyEventArgs that contains the event data.</param>
    protected override void OnDoubleClick(EventArgs e)
    {
        // Cannot process a message for a disposed control
        if (!IsDisposed)
        {
            // Do we have a manager for processing mouse messages?
            ViewManager?.DoubleClick(PointToClient(MousePosition));
        }

        // Let base class fire events
        base.OnDoubleClick(e);
    }

    /// <summary>
    /// Raises the KeyDown event.
    /// </summary>
    /// <param name="e">A KeyEventArgs that contains the event data.</param>
    protected override void OnKeyDown(KeyEventArgs e)
    {
        // Cannot process a message for a disposed control
        if (!IsDisposed)
        {
            // Do we have a manager for processing key messages?
            ViewManager?.KeyDown(e);
        }

        // Let base class fire events
        base.OnKeyDown(e);
    }

    /// <summary>
    /// Raises the KeyPress event.
    /// </summary>
    /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
    protected override void OnKeyPress(KeyPressEventArgs e)
    {
        // Cannot process a message for a disposed control
        if (!IsDisposed)
        {
            // Do we have a manager for processing key messages?
            ViewManager?.KeyPress(e);
        }

        // Let base class fire events
        base.OnKeyPress(e);
    }

    /// <summary>
    /// Raises the KeyUp event.
    /// </summary>
    /// <param name="e">A KeyEventArgs that contains the event data.</param>
    protected override void OnKeyUp(KeyEventArgs e)
    {
        // Cannot process a message for a disposed control
        if (!IsDisposed)
        {
            // Do we have a manager for processing key messages?
            ViewManager?.KeyUp(e);
        }

        // Let base class fire events
        base.OnKeyUp(e);
    }

    /// <summary>
    /// Raises the GotFocus event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnGotFocus(EventArgs e)
    {
        // Cannot process a message for a disposed control
        if (!IsDisposed)
        {
            // Do we have a manager for processing source messages?
            ViewManager?.GotFocus();
        }

        // Let base class fire standard event
        base.OnGotFocus(e);
    }

    /// <summary>
    /// Raises the LostFocus event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnLostFocus(EventArgs e)
    {
        // Cannot process a message for a disposed control
        if (!IsDisposed)
        {
            // Do we have a manager for processing source messages?
            ViewManager?.LostFocus();
        }

        // Let base class fire standard event
        base.OnLostFocus(e);
    }
    #endregion

    #region Implementation
    private void SetPalette(PaletteBase? palette)
    {
        if (palette != _palette)
        {
            // Unhook from current palette events
            if (_palette is not null)
            {
                _palette.PalettePaint -= OnNeedPaint;
                _palette.ButtonSpecChanged -= OnButtonSpecChanged;
            }

            // Remember the new palette
            _palette = palette;

            // Get the renderer associated with the palette
            Renderer = _palette?.GetRenderer();

            // Hook to new palette events
            if (_palette is not null)
            {
                _palette.PalettePaint += OnNeedPaint;
                _palette.ButtonSpecChanged += OnButtonSpecChanged;
            }
        }
    }

    private void PaintTransparentBackground(PaintEventArgs? e)
    {
        // Get the parent control for transparent drawing purposes
        Control? parent = TransparentParent;

        // Do we have a parent control and we need to paint background?
        if ((parent is not null) && NeedTransparentPaint)
        {
            // Only grab the required reference once
            if (_miPTB is null)
            {
                // Use reflection so we can call the Windows Forms internal method for painting parent background
                _miPTB = typeof(Control).GetMethod(
                    nameof(PaintTransparentBackground),
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
                    null,
                    CallingConventions.HasThis,
                    [typeof(PaintEventArgs), typeof(Rectangle), typeof(Region)],
                    null)!;
            }

            _miPTB.Invoke(this, [e!, ClientRectangle, null!]);
        }
        else
        {
            // No parent information available, so just use a standard brush
            e?.Graphics.FillRectangle(SystemBrushes.Control, ClientRectangle);
        }
    }

    private void OnPerformRefresh()
    {
        // If we still need to perform the refresh
        if (_refresh)
        {
            // Perform the requested paint of the control
            Refresh();

            // If the layout is still dirty after the refresh
            if (_layoutDirty)
            {
                // Then non of the control is visible, so perform manual request
                // for a layout to ensure that child controls can be resized
                PerformLayout();

                // Need another repaint to take the layout change into account
                Refresh();
            }

            // Refresh request has been serviced
            _refresh = false;
            _refreshAll = false;
        }
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        // We only care if we are using the global palette
        if (PaletteMode == PaletteMode.Global)
        {
            // Update ourself with the new global palette
            _localPalette = null;
            SetPalette(KryptonManager.CurrentGlobalPalette);
            Redirector!.Target = _palette;

            // A new palette source means we need to layout and redraw
            OnNeedPaint(Palette, new NeedLayoutEventArgs(true));

            GlobalPaletteChanged?.Invoke(sender, e);
        }
    }

    private void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e) => PerformNeedPaint(true);

    private void OnContextMenuStripOpening(object? sender, CancelEventArgs e)
    {
        // Get the actual strip instance
        ContextMenuStrip? cms = base.ContextMenuStrip;

        // Make sure it has the correct renderer
        if (cms is not null)
        {
            cms.Renderer = CreateToolStripRenderer();
        }
    }

    private void OnKryptonContextMenuDisposed(object? sender, EventArgs e) =>
        // When the current krypton context menu is disposed, we should remove 
        // it to prevent it being used again, as that would just throw an exception 
        // because it has been disposed.
        KryptonContextMenu = null;

    private void OnContextMenuClosed(object? sender, ToolStripDropDownClosedEventArgs e) => ContextMenuClosed();

    #endregion
}
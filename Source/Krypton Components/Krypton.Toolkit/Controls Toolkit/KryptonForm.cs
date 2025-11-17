#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

// ReSharper disable InconsistentNaming

using SolidBrush = System.Drawing.SolidBrush;
using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit;

/// <summary>
/// Draws the window chrome using a Krypton palette.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonForm), "ToolboxBitmaps.KryptonForm.bmp")]
[Description(@"Draws the window chrome using a Krypton palette.")]
[Designer(typeof(KryptonFormDesigner))]
public class KryptonForm : VisualForm,
    IContentValues
{
    #region Type Definitions

    internal class FormPaletteRedirect : PaletteRedirect
    {
        private readonly KryptonForm _kryptonForm;

        public FormPaletteRedirect(PaletteBase palette, KryptonForm kryptonForm)
            : base(palette)
        {
            _kryptonForm = kryptonForm;
        }

        public override PaletteRelativeAlign GetContentShortTextH(PaletteContentStyle style, PaletteState state) => style switch
        {
            PaletteContentStyle.HeaderForm
                or PaletteContentStyle.HeaderPrimary
                or PaletteContentStyle.HeaderDockInactive
                or PaletteContentStyle.HeaderDockActive
                or PaletteContentStyle.HeaderSecondary
                or PaletteContentStyle.HeaderCustom1
                or PaletteContentStyle.HeaderCustom2
                or PaletteContentStyle.HeaderCustom3 => _kryptonForm._formTitleAlign != PaletteRelativeAlign.Inherit
                    ? _kryptonForm._formTitleAlign
                    : base.GetContentShortTextH(style, state),
            _ => base.GetContentShortTextH(style, state)
        };
    }

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
    private static readonly Size CAPTION_ICON_SIZE = new Size(16, 16);

    private const int HT_CORNER = 8;

    // Drop shadow
    private const int CS_DROPSHADOW = 0x00020000;

    private const int CP_NOCLOSE_BUTTON = 0x200;
    #endregion

    #region Instance Fields

    private readonly ButtonSpecManagerDraw _buttonManager;
    private VisualPopupToolTip? _visualPopupToolTip;
    private readonly ViewDrawForm _drawDocker;
    private readonly ViewDrawDocker _drawHeading;
    private readonly ViewDrawContent _drawContent;
    private readonly ViewDecoratorFixedSize _headingFixedSize;
    private readonly ViewLayoutNull _layoutNull;
    private HeaderStyle _headerStyle;
    private PaletteRelativeAlign _formTitleAlign;
    private HeaderStyle _headerStylePrev;
    private FormWindowState _regionWindowState;
    private FormWindowState _lastWindowState;
    private string? _textExtra;
    private string _oldText;
    private static bool _isInAdministratorMode;
    private static bool _isInAdministratorModeKnown;
    private bool _allowFormChrome;
    private bool _allowStatusStripMerge;
    private bool _recreateButtons;
    private bool _firstCheckView;
    private bool _lastNotNormal;
    private bool _useDropShadow;
    private StatusStrip? _statusStrip;
    private bool _mdiTransferred;
    private Bitmap? _cacheBitmap;
    private Icon? _cacheIcon;
    private Control? _activeControl;
    private KryptonFormTitleStyle _titleStyle;
    private InheritBool _internalPanelState;
    private int _foundRibbonOffset = -1;
    private readonly KryptonPanel _internalKryptonPanel;
    // Compensate for Windows 11 outer accent border by shrinking the window region slightly
    private Rectangle _lastGripClientRect = Rectangle.Empty;
    private Timer? _clickTimer;
    private KryptonSystemMenu? _kryptonSystemMenu;
    // SystemMenu context menu components
    private KryptonContextMenu _systemMenuContextMenu;
    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the KryptonForm class.
    /// </summary>
    public KryptonForm()
    {
        // Default properties
        _headerStyle = HeaderStyle.Form;
        _formTitleAlign = PaletteRelativeAlign.Near;
        _headerStylePrev = _headerStyle;
        AllowButtonSpecToolTips = false;
        _allowFormChrome = true;
        _allowStatusStripMerge = true;
        AllowIconDisplay = true;
        _regionWindowState = FormWindowState.Normal;
        _recreateButtons = true;
        _firstCheckView = true;
        _lastNotNormal = false;
        // Yes, we want to be drawn double buffered by default
        base.DoubleBuffered = true;

        // Create storage objects
        ButtonSpecs = new FormButtonSpecCollection(this);
        var buttonSpecsFixed = new FormFixedButtonSpecCollection(this);

        // Add the fixed set of window form buttons
        ButtonSpecMin = new ButtonSpecFormWindowMin(this);
        ButtonSpecMax = new ButtonSpecFormWindowMax(this);
        ButtonSpecClose = new ButtonSpecFormWindowClose(this);
        buttonSpecsFixed.AddRange([ButtonSpecMin, ButtonSpecMax, ButtonSpecClose]);

        // Create the palette storage
        StateCommon = new PaletteFormRedirect(Redirector, NeedPaintDelegate, this);
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
        // Create the internal panel used for containing content (custom panel draws sizing grip)
        _internalKryptonPanel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            Location = new Point(0, 0),
            Margin = new Padding(0),
            Name = "InternalKryptonPanel",
            Padding = new Padding(0),
            Size = new Size(100, 100),
            TabStop = false,
        };
        // B2318 - Since the introduction of the InternalPanel overrides for OnControlRemoved and OnControlAdded don't fire correctly.
        _internalKryptonPanel.ControlRemoved += (s, e) => OnControlRemoved(e);
        _internalKryptonPanel.ControlAdded += (s, e) => OnControlAdded(e);

        // Create the root element that contains the title bar and null filler
        _drawDocker = new ViewDrawForm(StateActive.Back, StateActive.Border)
        {
            { _headingFixedSize, ViewDockStyle.Top },
            { _layoutNull, ViewDockStyle.Fill }
        };

        // Create button specification collection manager
        _buttonManager = new ButtonSpecManagerDraw(this, Redirector, ButtonSpecs, buttonSpecsFixed,
            [_drawHeading],
            [StateCommon.Header],
            [PaletteMetricInt.HeaderButtonEdgeInsetForm],
            [PaletteMetricPadding.HeaderButtonPaddingForm],
            CreateToolStripRenderer,
            OnNeedPaint);

        // Initialize administrator mode detection
        _ = GetIsInAdministratorMode();

        // Create the manager for handling tooltips
        ToolTipManager = new ToolTipManager(new ToolTipValues(null, GetDpiFactor)); // use default, as each button "could" have different values ??!!??
        ToolTipManager.ShowToolTip += OnShowToolTip;
        ToolTipManager.CancelToolTip += OnCancelToolTip;
        _buttonManager.ToolTipManager = ToolTipManager;

        // Hook into global static events
        KryptonManager.GlobalUseThemeFormChromeBorderWidthChanged += OnGlobalUseThemeFormChromeBorderWidthChanged;
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

        // Create the view manager instance
        ViewManager = new ViewManager(this, _drawDocker);

        _titleStyle = KryptonFormTitleStyle.Inherit;

        // Disable 'UseDropShadow' on creation
#pragma warning disable CS0618
        _useDropShadow = false;
#pragma warning restore CS0618
        TransparencyKey = GlobalStaticValues.TRANSPARENCY_KEY_COLOR; // Bug #1749

        // #1979 Temporary fix
        base.PaletteChanged += (s, e) => _internalKryptonPanel.PaletteMode = PaletteMode;
        // END #1979 Temporary fix

        // Instantiate system menu items only to keep the compiler happy
        _systemMenuContextMenu = new();

        SystemMenuValues = new (_systemMenuContextMenu);

        // Init only here. Must instantiate in OnHandleCreated
        _kryptonSystemMenu = null;
    }
    #endregion

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public SystemMenuValues SystemMenuValues { get; }
    public bool ShouldSerializeSystemMenuValues() => !SystemMenuValues.IsDefault;
    public void ResetSystemMenuValues() => SystemMenuValues.Reset();

    #region Private
    private void SetupSystemMenu() 
    {
        if (!DesignMode)
        {
            _kryptonSystemMenu = new(this, _drawContent, _systemMenuContextMenu);

            // When the _kryptonSystemMenu is instantiated the listener is not enabled by default.
            // From there SystemMenuValues.Enabled will trigger and control if the listener to be active or not.
            if (SystemMenuValues.Enabled)
            {
                _kryptonSystemMenu.EnableListener();
            }
        }
    }
    #endregion

    #region Private SizeGrip
    private float GetDpiFactor() => DeviceDpi / 96F;

    /// <summary>
    /// Determines whether the form-level sizing grip should be shown.
    /// Issue: https://github.com/Krypton-Suite/Standard-Toolkit/issues/984
    /// PR: https://github.com/Krypton-Suite/Standard-Toolkit/pull/2436
    /// </summary>
    private bool ShouldShowSizingGrip()
    {
        // Respect SizeGripStyle
        if (SizeGripStyle == SizeGripStyle.Hide)
        {
            return false;
        }

        // Only for sizable borders
        if (FormBorderStyle is not FormBorderStyle.Sizable and not FormBorderStyle.SizableToolWindow)
        {
            return false;
        }

        // Hide when minimized or maximized
        var state = GetWindowState();
        if (state is FormWindowState.Maximized or FormWindowState.Minimized)
        {
            return false;
        }

        // If a StatusStrip is merged and will draw its own sizing grip, skip ours
        if (StatusStripMerging && _statusStrip is { SizingGrip: true, Visible: true })
        {
            return false;
        }

        // Auto/Show both allow it when resizable and not maximized/minimized
        return SizeGripStyle is SizeGripStyle.Auto or SizeGripStyle.Show;
    }

    /// <summary>
    /// Computes the grip rectangle and RTL flag in window coordinates.
    /// </summary>
    private (Rectangle gripRect, bool isRtl) GetGripRectAndRtl()
    {
        var dpi = GetDpiFactor();
        int size = Math.Max(16, (int)Math.Round(16 * dpi));
        bool isRtl = RightToLeftLayout;

        // Use real window metrics instead of ClientSize to avoid stale client values after style toggles
        Padding borders = RealWindowBorders;
        Rectangle windowBounds = RealWindowRectangle;

        int x = isRtl ? borders.Left : Math.Max(borders.Left, windowBounds.Width - borders.Right - size);
        int y = Math.Max(borders.Top, windowBounds.Height - borders.Bottom - size);

        var windowRect = new Rectangle(x, y, size, size);
        return (windowRect, isRtl);
    }

    /// <summary>
    /// Draws the classic sizing grip glyph.
    /// </summary>
    private void DrawSizingGrip(Graphics g, Rectangle gripRect)
    {
        // Try themed bitmap resource first
        if (TryDrawResourceGrip(g, gripRect))
        {
            return;
        }

        // Fallback: draw larger diagonal dots (2x2 px scaled by DPI) with theme-derived, contrast-checked color
        Color dotColor = GetGripDotColor();

        using var dotBrush = new SolidBrush(dotColor);

        int dot = Math.Max(2, (int)Math.Round(2 * GetDpiFactor()));
        int move = Math.Max(dot + 2, (int)Math.Round(4 * GetDpiFactor()));
        int lines = 3;

        if (RightToLeftLayout)
        {
            int y = gripRect.Bottom - (dot * 2);
            for (int i = lines; i >= 1; i--)
            {
                int x = gripRect.Left + 1;
                for (int j = 0; j < i; j++)
                {
                    g.FillRectangle(dotBrush, new Rectangle(x, y, dot, dot));
                    x += move;
                }
                y -= move;
            }
        }
        else
        {
            int y = gripRect.Bottom - (dot * 2);
            for (int i = lines; i >= 1; i--)
            {
                int x = gripRect.Right - (dot * 2);
                for (int j = 0; j < i; j++)
                {
                    g.FillRectangle(dotBrush, new Rectangle(x, y, dot, dot));
                    x -= move;
                }
                y -= move;
            }
        }
    }

    private Color GetGripDotColor()
    {
        var palette = GetResolvedPalette() ?? KryptonManager.CurrentGlobalPalette;
        Color back = palette.GetBackColor1(PaletteBackStyle.FormMain, PaletteState.Normal);
        if (back == GlobalStaticValues.EMPTY_COLOR || back.IsEmpty)
        {
            back = BackColor;
        }

        Color candidate = palette.GetBorderColor1(PaletteBorderStyle.FormMain, PaletteState.Normal);
        if (candidate == GlobalStaticValues.EMPTY_COLOR || candidate.IsEmpty)
        {
            candidate = StateActive.Border.Color1;
        }

        if (HasSufficientContrast(candidate, back))
        {
            return candidate;
        }

        // Try a typical text color from the palette which tends to be contrast-safe
        Color text = palette.GetContentShortTextColor1(PaletteContentStyle.LabelNormalPanel, PaletteState.Normal);
        if (!(text == GlobalStaticValues.EMPTY_COLOR || text.IsEmpty) && HasSufficientContrast(text, back))
        {
            return text;
        }

        // Final fallback: black/white based on background luminance
        return Luminance(back) > 0.5f ? Color.White : Color.Black;
    }

    private static float Luminance(Color c) => (0.2126f * c.R + 0.7152f * c.G + 0.0722f * c.B) / 255f;

    private static bool HasSufficientContrast(Color a, Color b)
    {
        // Simple luminance difference heuristic; tuned to be visible over palette backgrounds
        float diff = Math.Abs(Luminance(a) - Luminance(b));
        if (diff >= 0.35f)
        {
            return true;
        }

        // Also consider raw channel distance
        int dr = Math.Abs(a.R - b.R);
        int dg = Math.Abs(a.G - b.G);
        int db = Math.Abs(a.B - b.B);
        return (dr + dg + db) / (3f * 255f) >= 0.35f;
    }

    private bool TryDrawResourceGrip(Graphics g, Rectangle dest)
    {
        // First, ask the palette for a themed sizing grip image (RTL-aware)
        var isRtl = RightToLeftLayout ? RightToLeft.Yes : RightToLeft.No;
        Image? themedGrip = (GetResolvedPalette() ?? KryptonManager.CurrentGlobalPalette).GetSizeGripImage(isRtl);
        if (themedGrip is null)
        {
            return false;
        }

        var dpi = GetDpiFactor();
        int w = (int)Math.Ceiling(themedGrip.Width * dpi);
        int h = (int)Math.Ceiling(themedGrip.Height * dpi);
        using var scaled = CommonHelper.ScaleImageForSizedDisplay(themedGrip, w, h, true);
        if (scaled is null)
        {
            return false;
        }

        int x = RightToLeftLayout ? dest.Left : dest.Right - scaled.Width;
        int y = dest.Bottom - scaled.Height;

        // Apply color-key transparency like legacy resources (top-left pixel)
        Color key = Color.Magenta;
        if (themedGrip is Bitmap b && b.Width > 0 && b.Height > 0)
        {
            key = b.GetPixel(0, 0);
        }

        using var ia1 = new System.Drawing.Imaging.ImageAttributes();
        ia1.SetColorKey(key, key);

        g.DrawImage(scaled, new Rectangle(x, y, scaled.Width, scaled.Height),
                    0, 0, scaled.Width, scaled.Height, GraphicsUnit.Pixel, ia1);
        return true;
    }
    #endregion

    #region IDispose
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
            KryptonManager.GlobalUseThemeFormChromeBorderWidthChanged -= OnGlobalUseThemeFormChromeBorderWidthChanged;

            // #1979 Temporary fix
            base.PaletteChanged -= (s, e) => _internalKryptonPanel.PaletteMode = PaletteMode;
            // END #1979 Temporary fix

            // Clear down the cached bitmap
            if (_cacheBitmap != null)
            {
                _cacheBitmap.Dispose();
                _cacheBitmap = null;
            }

            // Dispose of the system menu, which will in turn release any open handle in the listener
            _kryptonSystemMenu?.Dispose();

            ButtonSpecMin.Dispose();
            ButtonSpecMax.Dispose();
            ButtonSpecClose.Dispose();

            // Dispose the click timer
            _clickTimer?.Dispose();
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Magic Overrides to make the internal Panel work indesigners etc.

    /// <inheritdoc cref="Form" />
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
#if NET8_0_OR_GREATER
        [AllowNull]
        public override Font Font
#else
    public override Font Font
#endif
    {
        get => base.Font;
        set { } //base.Font = value;
    }

    /// <inheritdoc cref="Form" />
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color ForeColor
    {
        get => base.ForeColor;
        set { } // base.ForeColor = value;
    }

    /// <inheritdoc cref="Form" />
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color BackColor
    {
        get => base.BackColor;
        set { } // base.BackColor = value;
    }

    /// <summary>
    /// Gets and sets the background image.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Background image.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public override Image? BackgroundImage
    {
        get => _internalKryptonPanel.StateCommon.Image;
        set => _internalKryptonPanel.StateCommon.Image = value;
    }

    /// <inheritdoc cref="Form" />
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override ImageLayout BackgroundImageLayout
    {
        get => base.BackgroundImageLayout;
        set => base.BackgroundImageLayout = value;
    }

    /// <summary>
    /// Gets and sets the background image style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Background image style.")]
    [DefaultValue(PaletteImageStyle.Inherit)]
    [RefreshProperties(RefreshProperties.All)]
    public PaletteImageStyle ImageStyle
    {
        get => _internalKryptonPanel.StateCommon.ImageStyle;
        set => _internalKryptonPanel.StateCommon.ImageStyle = value;
    }

    /// <inheritdoc cref="Form" />
    public new void SuspendLayout()
    {
        if (_internalPanelState == InheritBool.Inherit)
        {
            _internalPanelState = InheritBool.False;
            ((ISupportInitialize)(this._internalKryptonPanel)).BeginInit();
            _internalKryptonPanel.SuspendLayout();
        }

        base.SuspendLayout();
    }

    /// <summary>
    ///  Gets or sets a value indicating whether the form is a container for multiple document interface
    ///  (MDI) child forms.
    /// </summary>
    [Category("Window Style")]
    [DefaultValue(false)]
    [Description(
        "Gets or sets a value indicating whether the form is a container for multiple document interface (MDI) child forms.")]
    public new bool IsMdiContainer
    {
        get => base.IsMdiContainer;
        set
        {
            base.IsMdiContainer = value;
            if (value)
            {
                SetInheritedControlOverride();
                // So this (in the designer) is normally set after all the controls have been added !
                Control.ControlCollection checkForRibbon = _internalKryptonPanel.Controls;
                var controlCount = checkForRibbon.Count;
                for (var i = controlCount - 1; i >= 0; i--)
                {
                    //In reverse, because they are removed when added to another control
                    base.Controls.Add(checkForRibbon[i]);
                }
            }
        }
    }

    /// <summary>
    /// If the `KryptonForm` is a base class, then use this to override the internal panel usage
    /// </summary>
    public void SetInheritedControlOverride()
    {
        if (_mdiTransferred)
        {
            return;
        }

        _internalPanelState = InheritBool.True;
        _foundRibbonOffset = 0;
        _mdiTransferred = true;
    }

    /// <inheritdoc cref="Form" />
    public new void ResumeLayout(bool performLayout)
    {
        if (!performLayout
            && _internalPanelState == InheritBool.False)
        {
            _internalPanelState = InheritBool.True;

            Control.ControlCollection checkForRibbon = _internalKryptonPanel.Controls;
            var controlCount = checkForRibbon.Count;
            for (var i = controlCount - 1; i >= 0; i--)
            {
                //In reverse, because "normally" the ribbon is added last.
                // Have to do a string match as the dll reflection may not be in the project
                if (checkForRibbon[i].GetType().ToString() == @"Krypton.Ribbon.KryptonRibbon")
                {
                    _foundRibbonOffset = i;
                    break;
                }
            }

            if (_foundRibbonOffset == -1)
            {
                base.Controls.Add(_internalKryptonPanel);
                ((ISupportInitialize)(this._internalKryptonPanel)).EndInit();
                _internalKryptonPanel.ResumeLayout(false);
                _internalKryptonPanel.PerformLayout();
            }
            else
            {
                for (var i = controlCount - 1; i >= 0; i--)
                {
                    //In reverse, because they are removed when added to another control
                    if (i != _foundRibbonOffset)
                    {
                        base.Controls.Add(checkForRibbon[i]);
                    }
                }
                // Adding above removes from the _internalKryptonPanel
                base.Controls.Add(checkForRibbon[0]);
            }
        }

        base.ResumeLayout(performLayout);
    }

    /// <inheritdoc cref="Form" />
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("ControlControlsDescr")]
    public new Control.ControlCollection Controls
    {
        get
        {
            if (_internalKryptonPanel.Controls.Count == 0)
            {
                _internalKryptonPanel.ClientSize = ClientSize;
            }

            // Route to base.Controls when MDI is enabled
            return base.IsMdiContainer ? base.Controls : _internalKryptonPanel.Controls;
        }
    }

    #endregion

    #region Public (new)
    /// <summary>
    /// Toggles display of the minimize button.
    /// </summary>
    [DefaultValue(true)]
    [Category("Window Style")]
    [Description("Toggles display of the minimize button.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public new bool MinimizeBox 
    {
        get => base.MinimizeBox;

        set
        {
            if (base.MinimizeBox != value)
            {
                base.MinimizeBox = value;
                _buttonManager.PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Toggles display of the maximize button.
    /// </summary>
    [DefaultValue(true)]
    [Category("Window Style")]
    [Description("Toggles display of the maximize button.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public new bool MaximizeBox 
    {
        get => base.MaximizeBox;

        set
        {
            if (base.MaximizeBox != value)
            {
                base.MaximizeBox = value;
                _buttonManager.PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Toggles display of the Close button.
    /// </summary>
    [DefaultValue(true)]
    [Category("Window Style")]
    [Description("Toggles display of the close button.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public new bool CloseBox 
    {
        get => base.CloseBox;

        set
        { 
            if (base.CloseBox != value)
            {
                base.CloseBox = value;
                _buttonManager.PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Indicates the appearance and behavior of the border and title bar of the form.
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(FormBorderStyle.Sizable)]
    [Description("Indicates the appearance and behavior of the border and title bar of the form.")]
    public new FormBorderStyle FormBorderStyle
    {
        get => base.FormBorderStyle;

        set
        {
            if (base.FormBorderStyle != value)
            {
                base.FormBorderStyle = value;
                OnFormBorderStyleChanged();
                _buttonManager.PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Access to the Internal KryptonPanel.
    /// </summary>
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonPanel InternalPanel => _internalKryptonPanel;

    /// <summary>
    /// Gets or sets the extra text associated with the control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The extra text associated with the control.")]
    [DefaultValue("")]
    [AllowNull]
    public string? TextExtra
    {
        get => _textExtra ?? string.Empty;

        set
        {
            _textExtra = value;
            PerformNeedPaint(true);
        }
    }

    /// <summary>
    /// Gets and sets a value indicating if tooltips should be Displayed for button specs.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should tooltips be Displayed for button specs.")]
    [DefaultValue(false)]
    public bool AllowButtonSpecToolTips { get; set; }

    /// <summary>
    /// Gets or sets a value indicating if custom chrome is allowed.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should custom chrome be allowed for this KryptonForm instance.")]
    [DefaultValue(true)]
    public new bool UseThemeFormChromeBorderWidth
    {
        get => _allowFormChrome;
        set
        {
            if (_allowFormChrome != value)
            {
                _allowFormChrome = value;
                if (StateCommon!.Border is PaletteFormBorder formBorder)
                {
                    formBorder.UseThemeFormChromeBorderWidth = value;
                }

                // Do we want to switch on/off the custom chrome?
                UpdateUseThemeFormChromeBorderWidthDecision();
                RecalcNonClient();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating if the form status strip be considered for merging into chrome.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should the form status strip be considered for merging into chrome.")]
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
    [Category(@"Visuals")]
    [Description(@"Header style for a main form.")]
    [DefaultValue(HeaderStyle.Form)]
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
    /// Gets and sets the header edge to display the button against.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The Form Title position, relative to available space")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(PaletteRelativeAlign.Near)]
    public PaletteRelativeAlign FormTitleAlign
    {
        get => _formTitleAlign;

        set
        {
            if (_formTitleAlign != value)
            {
                _formTitleAlign = value;
                PerformNeedPaint(true);
            }
        }
    }
    private bool ShouldSerializeFormTitleAlign() => _formTitleAlign != PaletteRelativeAlign.Near;
    private void ResetFormTitleAlign() => _formTitleAlign = PaletteRelativeAlign.Near;

    /// <summary>
    /// Gets and sets the chrome group border style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Chrome group border style.")]
    [DefaultValue(PaletteBorderStyle.FormMain)]
    public PaletteBorderStyle GroupBorderStyle
    {
        get => StateCommon!.BorderStyle;

        set
        {
            if (StateCommon!.BorderStyle != value)
            {
                StateCommon.BorderStyle = value;
                PerformNeedPaint(false);
            }
        }
    }

    /// <summary>
    /// Gets and sets the chrome group background style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Chrome group background style.")]
    [DefaultValue(PaletteBackStyle.FormMain)]
    public PaletteBackStyle GroupBackStyle
    {
        get => StateCommon!.BackStyle;

        set
        {
            if (StateCommon!.BackStyle != value)
            {
                StateCommon.BackStyle = value;
                PerformNeedPaint(false);
            }
        }
    }

    /// <summary>
    /// Allows the use of drop shadow around the form.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Allows the use of drop shadow around the form.")]
    [DefaultValue(false)]
    [Obsolete("Deprecated - Only use if you are using Windows 7, 8 or 8.1.")]
    public bool UseDropShadow
    {
        get => _useDropShadow;

        set
        {
            _useDropShadow = value;

            UpdateDropShadowDraw(_useDropShadow);
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is in administrator mode.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is in administrator mode; otherwise, <c>false</c>.
    /// </value>
    [Category(@"Appearance")]
    [Description(@"Is the user currently an administrator.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsInAdministratorMode
    {
        get => _isInAdministratorMode;
        private set => _isInAdministratorMode = value;
    }

    /// <summary>
    /// Gets access to the common form appearance entries that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common form appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteFormRedirect? StateCommon { get; }

    private bool ShouldSerializeStateCommon() => StateCommon is { IsDefault: false };

    /// <summary>
    /// Gets access to the inactive form appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining inactive form appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteForm StateInactive { get; }

    private bool ShouldSerializeStateInactive() => !StateInactive.IsDefault;

    /// <summary>
    /// Gets access to the active form appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining active form appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteForm StateActive { get; }

    private bool ShouldSerializeStateActive() => !StateActive.IsDefault;

    /// <summary>
    /// Gets the collection of button specifications.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Collection of button specifications.")]
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
    public void InjectViewElement([DisallowNull] ViewBase element, ViewDockStyle style)
    {
        Debug.Assert(element != null);
        Debug.Assert(_drawHeading != null);

        if (!IsDisposed
            && _drawHeading != null)
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
                _drawHeading.Add(element!, style);
            }
        }
    }

    /// <summary>
    /// Remove a previously injected view element from the caption area.
    /// </summary>
    /// <param name="element">Reference to view element.</param>
    /// <param name="style">Docking style of the element.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void RevokeViewElement([DisallowNull] ViewBase element, ViewDockStyle style)
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
    public void RecreateMinMaxCloseButtons() => _recreateButtons = true;

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

    /// <summary>Gets or sets the active control on the container control.</summary>
    [DefaultValue(null),
     Description(@"Defines an active control for this window.")]
    public new Control? ActiveControl
    {
        get => _activeControl;

        set
        {
            _activeControl = value;
            _activeControl?.Focus();
        }
    }

    /// <summary>Arranges the current window title alignment.</summary>
    /// <value>The current window title alignment.</value>
    [Category(@"Appearance")]
    [DefaultValue(KryptonFormTitleStyle.Inherit),
     Description(@"Arranges the current window title alignment.")]
    public KryptonFormTitleStyle TitleStyle
    {
        get => _titleStyle;
        set
        {
            _titleStyle = value;
            UpdateTitleStyle(value);
        }
    }
    
    /// <summary>
    /// Gets or sets a value indicating whether the form has a control box.
    /// </summary>
    [Category(@"Window Style")]
    [DefaultValue(true)]
    [Description(@"Determines if the form has a control box.")]
    public new bool ControlBox
    {
        get => base.ControlBox;
        set
        {
            if (base.ControlBox != value)
            {
                base.ControlBox = value;
            }
        }
    }
    
    #endregion

    #region Public Chrome

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
    [DisallowNull]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Rectangle CustomCaptionArea { get; set; } = Rectangle.Empty;

    #endregion

    #region Public IContentValues
    /// <summary>
    /// Gets the image used for showing on the title bar.
    /// </summary>
    /// <param name="state">Form state.</param>
    /// <returns>Image.</returns>
    public Image? GetImage(PaletteState state)
    {
        Icon? displayIcon = GetDefinedIcon();

        // Has the icon to be Displayed changed since the last time around?
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

            // Currently the `FactorDpi#`'s do _NOT_ change whilst the app is running
            //if ((ViewButton.FactorDpiX != _lastFactorDpiX)
            // || (ViewButton.FactorDpiY != _lastFactorDpiY)
            // )
            //{
            // Image needs to be regenerated
            var currentWidth = (int)(CAPTION_ICON_SIZE.Width * FactorDpiX);
            var currentHeight = (int)(CAPTION_ICON_SIZE.Height * FactorDpiY);
            //}

            Bitmap? resizedBitmap = null;
            try
            {
                using var temp = new Icon(_cacheIcon, currentWidth, currentHeight);
                resizedBitmap = temp.ToBitmap();
            }
            catch
            {
                try
                {
                    // Failed so we convert the Icon directly instead of trying to get a sized version first
                    resizedBitmap = _cacheIcon.ToBitmap();
                }
                catch
                {
                    // Do nothing
                }
            }

            // Cache for future access
            if (resizedBitmap != null)
            {
                _cacheBitmap = CommonHelper.ScaleImageForSizedDisplay(resizedBitmap, currentWidth, currentHeight, false);
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
        GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the short text used as the main caption title.
    /// </summary>
    /// <returns>Title string.</returns>
    public string GetShortText()
    {
        // Get the base form text
        string titleText = Text;

        // Append administrator suffix if enabled and running with elevated privileges
        if (KryptonManager.UseAdministratorSuffix && IsInAdministratorMode)
        {
            titleText += $" ({KryptonManager.Strings.GeneralStrings.Administrator})";
        }

        return titleText;
    }

    /// <summary>
    /// Gets the long text used as the secondary caption title.
    /// </summary>
    /// <returns>Title string.</returns>
    public string GetLongText() => TextExtra!;

    #endregion

    #region Protected/Internal Override
    /// <inheritdoc/>
    internal override bool IsOnControlButtons(Point screenPoint)
    {
        // Convert screen coordinates to window coordinates
        var windowPoint = ScreenToWindow(screenPoint);

        // Check if the point is over any of the control buttons
        return _buttonManager.GetButtonRectangle(ButtonSpecMin).Contains(windowPoint) ||
               _buttonManager.GetButtonRectangle(ButtonSpecMax).Contains(windowPoint) ||
               _buttonManager.GetButtonRectangle(ButtonSpecClose).Contains(windowPoint);
    }

    /// <inheritdoc/>
    internal override bool IsInTitleBarArea(Point screenPoint)
    {
        // Convert screen coordinates to window coordinates
        var windowPoint = ScreenToWindow(screenPoint);

        // Check if the point is in the title bar area (above the client area)
        return windowPoint.Y < _drawHeading.ClientRectangle.Height;
    }

    /// <summary>
    /// Create the redirector instance.
    /// </summary>
    /// <returns>PaletteRedirect derived class.</returns>
    protected override PaletteRedirect CreateRedirector() => new FormPaletteRedirect(GetResolvedPalette() ?? KryptonManager.CurrentGlobalPalette, this);

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
    //protected override void OnControlRemoved(ControlEventArgs e)
    protected override void OnControlRemoved(ControlEventArgs e)
    {
        // Is the cached reference being removed?
        if (_statusStrip == e.Control)
        {
            // Unhook from status strip events
            UnMonitorStatusStrip();

            // Recalc to test if status strip should be unintegrated
            RecalcNonClient();
        }

        base.OnControlRemoved(e);
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
        UpdateUseThemeFormChromeBorderWidthDecision();

        ApplyMaterialFormChromeDefaultsIfNeeded();

        // Ensure we don't interfere with StartPosition by waiting until after positioning
        if (IsHandleCreated)
        {
            UpdateUseThemeFormChromeBorderWidthDecision();
        }
    }

    /// <summary>
    /// Raises the Shown event.
    /// </summary>
    /// <param name="e">An EventArgs containing event data.</param>
    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
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
    protected override void OnButtonSpecChanged(object? sender, [DisallowNull] EventArgs e)
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

        PerformNeedPaint(false);

        // Grip visibility can change with active state in some themes
        InvalidateNonClient();

        base.OnWindowActiveChanged();
    }

    /// <summary>
    /// Ensure grippie redraw on border style changes.
    /// </summary>
    protected override void OnResizeEnd(EventArgs e)
    {
        base.OnResizeEnd(e);
        InvalidateNonClient();
    }

    /// <summary>
    /// When border style changes via property, force non-client repaint so grippie updates immediately.
    /// </summary>
    protected override void OnStyleChanged(EventArgs e)
    {
        base.OnStyleChanged(e);
        // If the size grip is being hidden via SizeGripStyle, ensure any previously drawn overlay is cleared immediately
        if (SizeGripStyle == SizeGripStyle.Hide)
        {
            RecalcNonClient();
        }
        else
        {
            InvalidateNonClient();
        }
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
        UpdateUseThemeFormChromeBorderWidthDecision();

        ApplyMaterialFormChromeDefaultsIfNeeded();

        // Ensure the sizing grip reflects new theme immediately
        RecalcNonClient();
        // Deferred call for theme churning during toggle
        if (IsHandleCreated)
        {
            BeginInvoke(new System.Windows.Forms.MethodInvoker(RecalcNonClient));
        }
    }

    /// <summary>
    /// Occurs when the UseThemeFormChromeBorderWidthChanged event is fired for the current palette.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnUseThemeFormChromeBorderWidthChanged(object? sender, EventArgs e)
    {
        // Test if we need to change the custom chrome usage
        UpdateUseThemeFormChromeBorderWidthDecision();
        RecalcNonClient();
    }

    /// <inheritdoc />
    protected override void WndProc(ref Message m)
    {
        const int WM_HELP = 0x0053;
        const int WM_PAINT = 0x000F;
        const int WM_CONTEXTMENU = 0x007B;

        if (m.Msg == WM_HELP)
        {
            var helpInfo = Marshal.PtrToStructure<PI.HELPINFO>(m.LParam);

            Point screenPos = new Point(helpInfo.MousePos.X, helpInfo.MousePos.Y);
            Point clientPos = PointToClient(screenPos);

            Control? targetControl =
                GetChildAtPoint(clientPos, GetChildAtPointSkip.Invisible | GetChildAtPointSkip.Disabled) ?? this;

            // Try to find a HelpProvider attached to this control
            HelpProvider? provider = FindHelpProvider(targetControl);

            if (provider != null)
            {
                Help.ShowHelp(targetControl, provider.HelpNamespace, provider.GetHelpNavigator(targetControl),
                    provider.GetHelpKeyword(targetControl));
            }

            m.Result = IntPtr.Zero;
            return;
        }
        else if (m.Msg == WM_CONTEXTMENU)
        {
            // Handle context menu request
            if (ContextMenuStrip != null)
            {
                Point p;
                p = (int)m.LParam == -1
                    ? Cursor.Position
                    : new Point((short)((int)m.LParam & 0xFFFF), (short)(((int)m.LParam >> 16) & 0xFFFF));

                ContextMenuStrip.Show(p);
                m.Result = IntPtr.Zero;
                return;
            }
        }

        // Let default processing run first
        base.WndProc(ref m);

        // After the client has painted, draw our grip overlay last so it isn't erased
        if (m.Msg == WM_PAINT)
        {
            DrawSizingGripOverlayIfNeeded();
        }
    }

    private void DrawSizingGripOverlayIfNeeded()
    {
        if (!ShouldShowSizingGrip())
        {
            if (_lastGripClientRect != Rectangle.Empty)
            {
                var oldClient = new PI.RECT { left = _lastGripClientRect.Left, top = _lastGripClientRect.Top, right = _lastGripClientRect.Right, bottom = _lastGripClientRect.Bottom };
                PI.RedrawWindow(Handle, ref oldClient, IntPtr.Zero, PI.RDW_INVALIDATE | PI.RDW_ALLCHILDREN | PI.RDW_UPDATENOW);
                _lastGripClientRect = Rectangle.Empty;
            }
            return;
        }

        var (newRect, _) = GetGripRectAndRtl();
        Padding bordersNow = RealWindowBorders;
        var newClientRect = new Rectangle(Math.Max(0, newRect.X - bordersNow.Left), Math.Max(0, newRect.Y - bordersNow.Top), newRect.Width, newRect.Height);
        if (_lastGripClientRect != Rectangle.Empty && _lastGripClientRect != newClientRect)
        {
            var oldClient = new PI.RECT { left = _lastGripClientRect.Left, top = _lastGripClientRect.Top, right = _lastGripClientRect.Right, bottom = _lastGripClientRect.Bottom };
            PI.RedrawWindow(Handle, ref oldClient, IntPtr.Zero, PI.RDW_INVALIDATE | PI.RDW_ALLCHILDREN | PI.RDW_UPDATENOW);
            _lastGripClientRect = Rectangle.Empty;
        }

        IntPtr hDC = PI.GetWindowDC(Handle);
        if (hDC == IntPtr.Zero)
        {
            return;
        }

        try
        {
            // Restrict drawing strictly to the grip rectangle in window coordinates
            PI.IntersectClipRect(hDC, newRect.Left, newRect.Top, newRect.Right, newRect.Bottom);
            using (Graphics g = Graphics.FromHdc(hDC))
            {
                DrawSizingGrip(g, newRect);
            }
            _lastGripClientRect = newClientRect;
        }
        finally
        {
            PI.ReleaseDC(Handle, hDC);
        }
    }

    /// <summary>
    /// Draw after base non-client painting using a fresh window DC. We clip out the client area
    /// so the sizing grip never paints over child controls.
    /// </summary>
    protected override void OnNonClientPaint(IntPtr hWnd)
    {
        // Let the base draw the border/chrome; our grippie is drawn in WM_PAINT overlay only
        base.OnNonClientPaint(hWnd);
    }


    // TODO: is stale but is it usable
    private Rectangle GetGripClientRect()
    {
        var dpi = GetDpiFactor();
        int size = Math.Max(16, (int)Math.Round(16 * dpi));
        int x = RightToLeftLayout ? 0 : Math.Max(0, _internalKryptonPanel.ClientSize.Width - size);
        int y = Math.Max(0, _internalKryptonPanel.ClientSize.Height - size);
        return new Rectangle(x, y, size, size);
    }

    /// <summary>Ensures MDI logic runs correctly after form creation.</summary>
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);

        // Differ on MdiContainer first
        if (IsMdiContainer)
        {
            if (!_mdiTransferred)
            {
                SetInheritedControlOverride();

                Control.ControlCollection checkForRibbon = _internalKryptonPanel.Controls;

                for (var i = checkForRibbon.Count - 1; i >= 0; i--)
                {
                    base.Controls.Add(checkForRibbon[i]);
                }
            }
        }
        else if (_internalPanelState == InheritBool.Inherit && !DesignMode)
        {
            // #2448 | Work-around / fix | Only runs on non mdi containers
            // This happens when KForm is instantiated manually without designer source,
            // which runs the layout methods through InitializeComponent.
            // This block is only to be executed at runtime.
            SuspendLayout();
            ResumeLayout(false);
        }

        // Register with the ActiveFormTracker
        ActiveFormTracker.Attach(this);

        // At runtime only, hookup the system menu.
        SetupSystemMenu();

        // Ensure Material defaults are applied as early as possible for new forms
        ApplyMaterialFormChromeDefaultsIfNeeded();
    }

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

        // Make sure non-client is invalidated so the grip is drawn on start
        InvalidateNonClient();

        base.WindowChromeStart();
    }

    /// <summary>
    /// Perform cleanup when custom chrome ending.
    /// </summary>
    protected override void WindowChromeEnd()
    {
        // Remove any region "begin" used to shape the form
        UpdateBorderRegion(null);

        base.WindowChromeEnd();
    }

    /// <summary>
    /// Perform hit testing to determine what part of the window the mouse is over.
    /// Uses standard hit testing in design mode to prevent designer interference.
    /// </summary>
    /// <remarks>
    /// <para><strong>DESIGN MODE PROTECTION:</strong> Uses IsInDesignMode() to prevent custom hit testing
    /// from interfering with Visual Studio designer operations. In design mode,
    /// delegates to base class for standard hit testing behavior.</para>
    /// 
    /// <para><strong>RUNTIME BEHAVIOR:</strong> Custom hit testing for system menu, control buttons, borders</para>
    /// <para><strong>DESIGN MODE BEHAVIOR:</strong> Standard hit testing (no custom chrome interference)</para>
    /// </remarks>
    /// <param name="pt">Point in window coordinates.</param>
    /// <returns>Hit test result indicating what part of window the point is over</returns>
    protected override IntPtr WindowChromeHitTest(Point pt)
    {
        Point originalPt = pt;

        if (CustomCaptionArea.Contains(pt))
        {
            return new IntPtr(PI.HT.CAPTION);
        }

        // Is the mouse over any of the min/max/close buttons?
        if (_buttonManager.GetButtonRectangle(ButtonSpecMin).Contains(pt)
            || _buttonManager.GetButtonRectangle(ButtonSpecMax).Contains(pt)
            || _buttonManager.GetButtonRectangle(ButtonSpecClose).Contains(pt))
        {
            // Get the mouse controller for this button
            ViewBase? viewBase = ViewManager?.Root.ViewFromPoint(pt);
            IMouseController? controller = viewBase?.FindMouseController();

            // Display snap layouts on Windows 11
            if (OSUtilities.IsAtLeastWindowsEleven && _buttonManager.GetButtonRectangle(ButtonSpecMax).Contains(pt))
            {
                return new IntPtr(PI.HT.MAXBUTTON);
            }

            // Ensure the button shows as 'normal' state when mouse not over and pressed
            if (controller is ButtonController buttonController)
            {
                buttonController.NonClientAsNormal = true;
            }
        }

        // Do not allow the caption to be moved or the border resized
        if (InertForm)
        {
            return new IntPtr(PI.HT.CLIENT);
        }

        using (var context = new ViewLayoutContext(this, Renderer))
        {
            // Discover if the form icon is being Displayed
            if (_drawContent.IsImageDisplayed(context))
            {
                // Is the mouse over the image area
                if (_drawContent.ImageRectangle(context).Contains(pt))
                {
                    // Otherwise, let Windows handle it with default system menu
                    return new IntPtr(PI.HT.MENU);
                }
            }
        }

        // Respect form sizing grip preferences before border checks
        if (ShouldShowSizingGrip())
        {
            var (gripRect, isRtl) = GetGripRectAndRtl();
            if (gripRect.Contains(pt))
            {
                return new IntPtr(isRtl ? PI.HT.BOTTOMLEFT : PI.HT.BOTTOMRIGHT);
            }
        }

        bool isResizable = FormBorderStyle is FormBorderStyle.Sizable or FormBorderStyle.SizableToolWindow;
        Padding borders = RealWindowBorders;

        // Material: use a wider invisible hit band for easier resize while keeping flat, borderless visuals.
        // RealWindowBorders can be 0 when the palette (e.g., Material) suppresses border width for drawing.
        // Expanding the hit test band preserves resize affordance without adding visible chrome.
        if (isResizable && Renderer is RenderMaterial)
        {
            const int materialResizeThickness = 6;
            borders = new Padding(
                Math.Max(borders.Left, materialResizeThickness),
                Math.Max(borders.Top, materialResizeThickness),
                Math.Max(borders.Right, materialResizeThickness),
                Math.Max(borders.Bottom, materialResizeThickness));
        }
        // Restrict the top border to the same size as the left as we are using
        // the values for the size of the border hit testing for resizing the window
        // and not the size of the border for drawing purposes.
        if (borders.Top > borders.Left)
        {
            borders.Top = borders.Left;
        }

        // Get the elements that contains the mouse point
        ViewBase? mouseView = ViewManager?.Root.ViewFromPoint(pt);

        // Scan up the view hierarchy until a recognized element is found
        while (mouseView != null)
        {
            // Is mouse over one of the borders?
            if (isResizable && (mouseView == _drawDocker || pt.Y < _drawHeading.ClientRectangle.Height))
            {
                // Is point over the left border?
                if ((borders.Left > 0) && (pt.X <= borders.Left))
                {
                    if (pt.Y <= HT_CORNER)
                    {
                        return new IntPtr(PI.HT.TOPLEFT);
                    }

                    return pt.Y >= (Height - HT_CORNER) ? new IntPtr(PI.HT.BOTTOMLEFT) : new IntPtr(PI.HT.LEFT);
                }

                // Is point over the right border?
                if ((borders.Right > 0) && (pt.X >= (Width - borders.Right)))
                {
                    if (pt.Y <= HT_CORNER)
                    {
                        return new IntPtr(PI.HT.TOPRIGHT);
                    }

                    return pt.Y >= (Height - HT_CORNER) ? new IntPtr(PI.HT.BOTTOMRIGHT) : new IntPtr(PI.HT.RIGHT);
                }

                // Is point over the bottom border?
                if ((borders.Bottom > 0) && (pt.Y >= (Height - borders.Bottom)))
                {
                    if (pt.X <= HT_CORNER)
                    {
                        return new IntPtr(PI.HT.BOTTOMLEFT);
                    }

                    return pt.X >= (Width - HT_CORNER) ? new IntPtr(PI.HT.BOTTOMRIGHT) : new IntPtr(PI.HT.BOTTOM);
                }

                // Is point over the top border?
                if ((borders.Top > 0) && (pt.Y <= borders.Top))
                {
                    if (pt.X <= HT_CORNER)
                    {
                        return new IntPtr(PI.HT.TOPLEFT);
                    }

                    return pt.X >= (Width - HT_CORNER) ? new IntPtr(PI.HT.TOPRIGHT) : new IntPtr(PI.HT.TOP);
                }
            }

            // Additional check: if the mouse is in the top area of the form (title bar region)
            // and we haven't identified a specific view, still allow moving
            if (mouseView == _drawHeading || pt.Y < _drawHeading.ClientRectangle.Height)
            {
                return new IntPtr(PI.HT.CAPTION);
            }

            // Mouse up another level
            mouseView = mouseView.Parent;
        }

        return base.WindowChromeHitTest(originalPt);
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
    /// Process the WM_NCLBUTTONDOWN message when overriding window chrome.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    /// <returns>True if the message was processed; otherwise false.</returns>
    protected override bool OnWM_NCLBUTTONDOWN(ref Message m)
    {
        using var context = new ViewLayoutContext(this, Renderer);
        // Discover if the form icon is being Displayed
        if (_drawContent.IsImageDisplayed(context))
        {
            // Extract the point in screen coordinates
            var screenPoint = new Point((int)m.LParam.ToInt64());

            // Convert to window coordinates
            Point windowPoint = ScreenToWindow(screenPoint);

            // Is the mouse over the Application icon image area
            if (_drawContent.ImageRectangle(context).Contains(windowPoint))
            {
                if (!SystemMenuValues.Enabled)
                {
                    // Make this work for the offset Application Icon when ButtonSpecs are left aligned
                    PI.PostMessage(Handle, PI.WM_.CONTEXTMENU, Handle, m.LParam);
                    return true;
                }
            }
        }

        return base.OnWM_NCLBUTTONDOWN(ref m);
    }

    /// <summary>
    /// Process the left mouse down event.
    /// </summary>
    /// <param name="windowPoint">Window coordinate of the mouse down.</param>
    /// <returns>True if event is processed; otherwise false.</returns>
    protected override bool WindowChromeLeftMouseDown(Point windowPoint)
    {
        // Let base class perform standard processing of the event
        var ret = base.WindowChromeLeftMouseDown(windowPoint);

        // Has pressing down made a view active and indicated it also wants to capture mouse?
        if (ViewManager is { ActiveView: not null, MouseCaptured: true })
        {
            StartCapture(ViewManager.ActiveView);
            ret = true;
        }

        return ret;
    }

    protected override void OnMove(EventArgs e)
    {
        base.OnMove(e);
    }

    #endregion

    #region Implementation
    private void OnFormBorderStyleChanged()
    {
        // KryptonForm uses ButtonSpecs for Form control buttons.
        // Those need synchronizing when the FormBorderStyle changes.
        // Once the style has change the user can adjust the buttons to the liking.
        // User configured buttons are changed once the FormBorderStyle changes.

        switch (FormBorderStyle)
        {
            case FormBorderStyle.None:
                ControlBox = false;
                MinimizeBox = false;
                MaximizeBox = false;
                CloseBox = false;
                break;

            case FormBorderStyle.FixedSingle:
            case FormBorderStyle.Fixed3D:
            case FormBorderStyle.FixedDialog:
            case FormBorderStyle.Sizable:
                ControlBox = true;
                MinimizeBox = true;
                MaximizeBox = true;
                CloseBox = true;
                break;

            case FormBorderStyle.FixedToolWindow:
            case FormBorderStyle.SizableToolWindow:
                ControlBox = false;
                MinimizeBox = false;
                MaximizeBox = false;
                CloseBox = true;
                break;
        }
    }

    private Icon? GetDefinedIcon()
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
                DebugTools.NotImplemented(style.ToString());
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
                    SetHeaderStyle(_drawHeading, StateCommon!.Header, _headerStyle);
                    // Remember last header style set
                    _headerStylePrev = _headerStyle;
                }

                // Update the heading to enforce a fixed Material-like caption height when Material renderer is active
                if (Renderer is RenderMaterial)
                {
                    const int materialCaptionHeight = 44; // px
                    _headingFixedSize.FixedSize = new Size(materialCaptionHeight, materialCaptionHeight);
                }
                else
                {
                    Padding windowBorders = RealWindowBorders;
                    _headingFixedSize.FixedSize = new Size(windowBorders.Top, windowBorders.Top);
                }

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
                if (NeedLayout
                    || (GetDefinedIcon() != _cacheIcon)
                   )
                {
                    Rectangle realWindowRectangle = RealWindowRectangle;
                    // Ask the view to perform a layout
                    if (GetWindowState() == FormWindowState.Maximized)
                    {
                        if (MdiParent == null)
                        {
                            // Get the size of each window border
                            var xBorder = PI.GetSystemMetrics(PI.SM_.CXSIZEFRAME) * 2;
                            // Reduce the Bounds by the padding on all but the top
                            realWindowRectangle.Width -= xBorder;
                        }
                    }

                    using (var context = new ViewLayoutContext(ViewManager, this, realWindowRectangle, Renderer))
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
                        // Get the path for the border, so we can shape the form using it
                        using var context = new RenderContext(this, null, Bounds, Renderer);
                        using GraphicsPath? path = _drawDocker.GetOuterBorderPath(context);
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
            if (GetWindowState() == FormWindowState.Maximized)
            {
                if (_regionWindowState != FormWindowState.Maximized)
                {
                    UpdateRegionForMaximized();
                }
            }
            else if (MdiParent != null)
            {
                using var backBrush = new SolidBrush(MdiParent.ActiveMdiChild == MdiParent
                    ? StateActive.Border.Color1
                    : StateInactive.Border.Color1);
                g.FillRectangle(backBrush, rect); // Bug #????
            }
            else if (TransparencyKey == GlobalStaticValues.TRANSPARENCY_KEY_COLOR)
            {
                g.FillRectangle(Brushes.Magenta, rect); // Bug #1749
            }
            else
            {
                // TODO: Use a cached brush !
                using var backBrush = new SolidBrush(TransparencyKey);
                g.FillRectangle(backBrush, rect); // Bug #1749
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
            var xBorder = PI.GetSystemMetrics(PI.SM_.CXSIZEFRAME) * 2;
            var yBorder = PI.GetSystemMetrics(PI.SM_.CYSIZEFRAME) * 2;

            // Reduce the Bounds by the padding on all but the top
            var maximizedRect = new Rectangle(xBorder, yBorder, Width - (xBorder * 2),
                Height - (yBorder * 2));

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

    private void UpdateBorderRegion(Region? newRegion)
    {
        if ((newRegion != null)
            && (newRegion.IsEmpty(CreateGraphics()))
           )
        {
            return;
        }

        // Cache the current region setting
        Region? oldRegion = Region;

        // Use the new region
        Region = newRegion;

        // Cleanup old region gracefully
        oldRegion?.Dispose();
    }

    private bool _hasUseThemeFormChromeBorderWidthFirstRun;
    private void UpdateUseThemeFormChromeBorderWidthDecision()
    {
        if (IsHandleCreated)
        {
            // Decide if we should have custom chrome applied
            var needChrome = UseThemeFormChromeBorderWidth &&
                             KryptonManager.UseThemeFormChromeBorderWidth &&
                             ((GetResolvedPalette() ?? KryptonManager.CurrentGlobalPalette).UseThemeFormChromeBorderWidth == InheritBool.True);

            // Is there a change in custom chrome requirement?
            if (UseThemeFormChromeBorderWidth != needChrome
                || !_hasUseThemeFormChromeBorderWidthFirstRun)
            {
                _hasUseThemeFormChromeBorderWidthFirstRun = true;
                _recreateButtons = true;
                _firstCheckView = true;
                UseThemeFormChromeBorderWidth = needChrome;
                base.UseThemeFormChromeBorderWidth = true; // make sure "Form" buttons are drawn correctly
                PerformNeedPaint(true);     // Force Layout size change
            }
        }
    }

    internal bool StatusStripMerging => _allowStatusStripMerge
                                        && _statusStrip is { Visible: true, Dock: DockStyle.Bottom }
                                        && (_statusStrip.Bottom == ClientRectangle.Bottom)
                                        && (_statusStrip.RenderMode == ToolStripRenderMode.ManagerRenderMode)
                                        && (ToolStripManager.Renderer is KryptonOffice2007Renderer or KryptonVisualStudio2010With2007Renderer
                                            or KryptonSparkleRenderer);

    private void MonitorStatusStrip(StatusStrip statusStrip)
    {
        if (_statusStrip != null)
        {
            UnMonitorStatusStrip();
        }

        // Hook into event handlers
        _statusStrip = statusStrip;
        _statusStrip.VisibleChanged += OnStatusVisibleChanged;
        _statusStrip.DockChanged += OnStatusDockChanged;
    }

    private void UnMonitorStatusStrip()
    {
        if (_statusStrip != null)
        {
            // Unhook from event handlers
            _statusStrip.VisibleChanged -= OnStatusVisibleChanged;
            _statusStrip.DockChanged -= OnStatusDockChanged;
            _statusStrip = null;
        }
    }

    private void OnShowToolTip(object? sender, ToolTipEventArgs e)
    {
        if (!IsDisposed)
        {
            // Do not show tooltips when the form we are in does not have focus
            Form? topForm = FindForm();
            if (topForm is { ContainsFocus: false })
            {
                return;
            }

            // Never show tooltips are design time
            if (!DesignMode)
            {
                IContentValues? sourceContent = null;
                var toolTipStyle = LabelStyle.ToolTip;
                var shadow = true;

                // Find the button spec associated with the tooltip request
                ButtonSpec? buttonSpec = _buttonManager.ButtonSpecFromView(e.Target);

                // If the tooltip is for a button spec
                if (buttonSpec != null)
                {
                    // Are we allowed to show page related tooltips
                    if (AllowButtonSpecToolTips)
                    {
                        // Create a helper object to provide tooltip values
                        var buttonSpecMapping = new ButtonSpecToContent(Redirector, buttonSpec);

                        // Is there actually anything to show for the tooltip
                        if (buttonSpecMapping.HasContent)
                        {
                            sourceContent = buttonSpecMapping;
                            toolTipStyle = buttonSpec.ToolTipStyle;
                            shadow = buttonSpec.ToolTipShadow;
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
                        CommonHelper.ContentStyleFromLabelStyle(toolTipStyle),
                        shadow);

                    _visualPopupToolTip.Disposed += OnVisualPopupToolTipDisposed;

                    // Show relative to the provided screen point
                    _visualPopupToolTip.ShowCalculatingSize(e.ControlMousePosition);
                }
            }
        }
    }

    private void OnCancelToolTip(object? sender, EventArgs e) =>
        // Remove any currently showing tooltip
        _visualPopupToolTip?.Dispose();

    private void OnVisualPopupToolTipDisposed(object? sender, EventArgs e)
    {
        // Unhook events from the specific instance that generated event
        var popupToolTip = sender as VisualPopupToolTip ?? throw new ArgumentNullException(nameof(sender));
        popupToolTip.Disposed -= OnVisualPopupToolTipDisposed;

        // Not showing a popup page anymore
        _visualPopupToolTip = null;
    }

    private void ApplyMaterialFormChromeDefaultsIfNeeded()
    {
        if (Renderer is RenderMaterial)
        {
            if (FormTitleAlign is PaletteRelativeAlign.Near or PaletteRelativeAlign.Inherit)
            {
                FormTitleAlign = PaletteRelativeAlign.Center;
            }

            // Hide the form icon by default for a cleaner Material header (user can still re-enable later)
            AllowIconDisplay = false;
        }
    }

    private void OnStatusDockChanged(object? sender, EventArgs e)
    {
        if (StatusStripMerging)
        {
            PerformNeedPaint(false);
        }
    }

    private void OnStatusVisibleChanged(object? sender, EventArgs e)
    {
        if (StatusStripMerging)
        {
            PerformNeedPaint(false);
        }
    }

    private void OnGlobalUseThemeFormChromeBorderWidthChanged(object? sender, EventArgs e) => UpdateUseThemeFormChromeBorderWidthDecision();

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        // We only care if we are using the global palette
        if (PaletteMode == PaletteMode.Global)
        {
            UpdateUseThemeFormChromeBorderWidthDecision();

            // Apply Material defaults when global palette switches
            ApplyMaterialFormChromeDefaultsIfNeeded();

            // Ensure sizing grip updates with theme
            RecalcNonClient();
            if (IsHandleCreated)
            {
                BeginInvoke(new System.Windows.Forms.MethodInvoker(RecalcNonClient));
            }
        }
    }

    /// <summary>Updates the title style.</summary>
    /// <param name="titleStyle">The title style.</param>
    private void UpdateTitleStyle(KryptonFormTitleStyle titleStyle)
    {
        switch (titleStyle)
        {
            case KryptonFormTitleStyle.Inherit:
                FormTitleAlign = PaletteRelativeAlign.Inherit;
                break;
            case KryptonFormTitleStyle.Classic:
                FormTitleAlign = PaletteRelativeAlign.Near;
                break;
            case KryptonFormTitleStyle.Modern:
                FormTitleAlign = PaletteRelativeAlign.Center;
                break;
        }
    }

    /*/// <summary>
    /// Starts a timer to distinguish between click and drag operations.
    /// </summary>
    /// <param name="clickPoint">The point where the click occurred.</param>
    private void StartClickTimer(Point clickPoint)
    {
        _lastClickPoint = clickPoint;
        
        // Create and start the timer if it doesn't exist
        if (_clickTimer == null)
        {
            _clickTimer = new Timer
            {
                Interval = 200 // 200ms delay to distinguish click from drag
            };
            _clickTimer.Tick += OnClickTimerTick;
        }
        
        _clickTimer.Start();
    }

    /// <summary>
    /// Stops the click timer and cleans up.
    /// </summary>
    private void StopClickTimer()
    {
        if (_clickTimer != null)
        {
            _clickTimer.Stop();
        }
    }*

    /// <summary>
    /// Handles the click timer tick event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void OnClickTimerTick(object? sender, EventArgs e)
    {
        StopClickTimer();
        
        // If we haven't started dragging, this was a simple click
                    /*if (!_isDragging && _themedSystemMenuValues?.Enabled && _themedSystemMenuValues?.ShowOnLeftClick && _themedSystemMenuService != null)
        {
            ShowThemedSystemMenu(_lastClickPoint);
        }*
    }

    /// <summary>
    /// Override to handle form losing focus, which should cancel the click timer.
    /// </summary>
    /// <param name="e">Event arguments.</param>
    protected override void OnLostFocus(EventArgs e)
    {
        base.OnLostFocus(e);
        
        // Cancel click timer when form loses focus
        StopClickTimer();
    }

    /// <summary>
    /// Override to handle key down events for canceling click timer.
    /// </summary>
    /// <param name="e">Key event arguments.</param>
    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        
        // Cancel click timer on Escape key
        if (e.KeyCode == Keys.Escape)
        {
            StopClickTimer();
        }
    }

    /// <summary>
    /// Override to handle form resize events, which should cancel the click timer.
    /// </summary>
    /// <param name="e">Event arguments.</param>
    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        
        // Cancel click timer when form is being resized
        StopClickTimer();
    }*/

    /// <summary>Finds the help provider.</summary>
    /// <param name="control">The control.</param>
    /// <returns>The help provider of the control.</returns>
    private HelpProvider? FindHelpProvider(Control? control)
    {
        while (control != null)
        {
            var components = control.Site?.Container?.Components;

            if (components != null)
            {
                foreach (Component component in components)
                {
                    if (component is HelpProvider provider && provider.GetShowHelp(control))
                    {
                        return provider;
                    }
                }
            }

            // If the parent is null then the caller did not find a help provider.
            control = control.Parent;
        }

        return null;
    }

    /// <inheritdoc />
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        return base.ProcessCmdKey(ref msg, keyData);
    }

    #endregion

    #region Drop Shadow Methods
    /// <summary>
    /// Calls the method that draws the drop shadow around the form.
    /// </summary>
    /// <param name="useDropShadow">Use drop shadow user input value.</param>
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

#pragma warning disable CS0618 // Type or member is obsolete
            if (UseDropShadow)
            {
                cp.ClassStyle |= CS_DROPSHADOW;
            }
#pragma warning restore CS0618 // Type or member is obsolete
            if (!CloseBox)
            {
                cp.ClassStyle |= CP_NOCLOSE_BUTTON;
            }

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
        bool result = false;
        try
        {
            var principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            result = principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        catch { }

        SetIsInAdministratorMode(result);
        return result;
    }

    /// <summary>Sets the is in administrator mode.</summary>
    /// <param name="value">if set to <c>true</c> [value].</param>
    public static void SetIsInAdministratorMode(bool value)
    {
        _isInAdministratorMode = value;
        _isInAdministratorModeKnown = true;
    }

    /// <summary>Gets the is in administrator mode.</summary>
    /// <returns>IsInAdministratorMode</returns>
    public static bool GetIsInAdministratorMode()
    {
        if (!_isInAdministratorModeKnown)
        {
            GetHasCurrentInstanceGotAdministrativeRights();
            _isInAdministratorModeKnown = true;
        }

        return _isInAdministratorMode;
    }
    #endregion
}
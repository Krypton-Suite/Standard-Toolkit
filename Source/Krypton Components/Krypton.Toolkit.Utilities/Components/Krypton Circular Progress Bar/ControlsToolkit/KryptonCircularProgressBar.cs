#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>The circular progress bar windows form control.</summary>
[Description("A circular progress bar for your applications.")]
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonCircularProgressBar), "ToolboxBitmaps.KryptonCircularProgressBar.bmp")]
[DefaultBindingProperty("Value")]
[Designer(typeof(KryptonCircularProgressBarDesigner))]
public class KryptonCircularProgressBar : KryptonProgressBar
{
    #region Instance Fields

    private readonly WinFormAnimation.Animator? _animator;
    private int? _animatedStartAngle;
    private float? _animatedValue;
    private WinFormAnimation.AnimationFunctions.Function _animationFunction;
    private Brush? _backBrush;
    private WinFormAnimation.KnownAnimationFunctions _knownAnimationFunction;
    private ProgressBarStyle? _lastStyle;
    private int _lastValue;

    private readonly PaletteDoubleRedirect _outerRingStateCommon;
    private readonly PaletteDouble _outerRingStateNormal;
    private readonly PaletteDouble _outerRingStateDisabled;

    private readonly PaletteDoubleRedirect _innerRingStateCommon;
    private readonly PaletteDouble _innerRingStateNormal;
    private readonly PaletteDouble _innerRingStateDisabled;

    private readonly PaletteTripleRedirect _superscriptStateCommon;
    private readonly PaletteTriple _superscriptStateNormal;
    private readonly PaletteTriple _superscriptStateDisabled;

    private readonly PaletteTripleRedirect _subscriptStateCommon;
    private readonly PaletteTriple _subscriptStateNormal;
    private readonly PaletteTriple _subscriptStateDisabled;

    private IDisposable? _mementoProgressBack;
    private IDisposable? _mementoOuterRingBack;
    private IDisposable? _mementoInnerRingBack;

    private int _innerMargin = 2;
    private int _innerWidth = -1;
    private int _outerMargin = -25;
    private int _outerWidth = 26;
    private int _progressWidth = 25;
    private Font _secondaryFont;
    private Padding _subscriptMargin;
    private string _subscriptText = string.Empty;
    private Padding _superscriptMargin;
    private string _superscriptText = string.Empty;
    private Padding _textMargin;

    private const int MinimumDiameter = 48;
    private const float MinimumSupersampleScale = 2f;
    private const float MaximumSupersampleScale = 3f;

    #endregion

    #region Properties

    /// <summary>
    /// Gets and sets the automatic resize of the control to fit centre text and ring layout.
    /// </summary>
    [Category("Layout")]
    [Localizable(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(true)]
    public override bool AutoSize
    {
        get => base.AutoSize;
        set
        {
            if (base.AutoSize == value)
            {
                return;
            }

            base.AutoSize = value;

            if (value)
            {
                PerformLayout();
            }
        }
    }

    /// <summary>
    /// Gets and sets the mode for when auto sizing.
    /// </summary>
    [Browsable(false)]
    [Localizable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(AutoSizeMode.GrowAndShrink)]
    public AutoSizeMode AutoSizeMode
    {
        get => GetAutoSizeMode();
        set => SetAutoSizeMode(value);
    }

    /// <summary>
    ///     Sets a known animation function.
    /// </summary>
    [Category("Behavior"), DefaultValue(WinFormAnimation.KnownAnimationFunctions.Linear)]
    public WinFormAnimation.KnownAnimationFunctions AnimationFunction
    {
        get => _knownAnimationFunction;
        set
        {
            _animationFunction = WinFormAnimation.AnimationFunctions.FromKnown(value);
            _knownAnimationFunction = value;
        }
    }

    /// <summary>
    ///     Gets or sets the animation speed in milliseconds.
    /// </summary>
    [Category("Behavior"), DefaultValue(500)]
    public int AnimationSpeed { get; set; }

    /// <summary>
    ///     Sets a custom animation function.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public WinFormAnimation.AnimationFunctions.Function CustomAnimationFunction
    {
        private get { return _animationFunction; }
        set
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _knownAnimationFunction = WinFormAnimation.KnownAnimationFunctions.None;
            _animationFunction = value;
        }
    }

    /// <summary>
    ///     Gets or sets the font of text in the <see cref="KryptonCircularProgressBar" />.
    /// </summary>
    /// <returns>
    ///     The <see cref="T:System.Drawing.Font" /> of the text. The default is the font set by the container.
    /// </returns>
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    [AllowNull]
    public override Font Font
    {
        get => base.Font;
        set
        {
            if (value == null || ReferenceEquals(base.Font, value))
            {
                return;
            }

            base.Font = value;
            OnContentLayoutChanged();
        }
    }

    /// <summary>
    /// </summary>
    [Category("Layout"), DefaultValue(2)]
    public int InnerMargin
    {
        get => _innerMargin;
        set
        {
            if (_innerMargin == value)
            {
                return;
            }

            _innerMargin = value;
            OnContentLayoutChanged();
        }
    }

    /// <summary>
    /// </summary>
    [Category("Layout"), DefaultValue(-1)]
    public int InnerWidth
    {
        get => _innerWidth;
        set
        {
            if (_innerWidth == value)
            {
                return;
            }

            _innerWidth = value;
            OnContentLayoutChanged();
        }
    }

    /// <summary>
    /// </summary>
    [Category("Layout"), DefaultValue(-25)]
    public int OuterMargin
    {
        get => _outerMargin;
        set
        {
            if (_outerMargin == value)
            {
                return;
            }

            _outerMargin = value;
            OnContentLayoutChanged();
        }
    }

    /// <summary>
    /// </summary>
    [Category("Layout"), DefaultValue(26)]
    public int OuterWidth
    {
        get => _outerWidth;
        set
        {
            if (_outerWidth == value)
            {
                return;
            }

            _outerWidth = value;
            OnContentLayoutChanged();
        }
    }

    /// <summary>
    /// </summary>
    [Category("Layout"), DefaultValue(25)]
    public int ProgressWidth
    {
        get => _progressWidth;
        set
        {
            if (_progressWidth == value)
            {
                return;
            }

            _progressWidth = value;
            OnContentLayoutChanged();
        }
    }

    /// <summary>
    /// </summary>
    [Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Font SecondaryFont
    {
        get => _secondaryFont;
        set
        {
            if (ReferenceEquals(_secondaryFont, value))
            {
                return;
            }

            _secondaryFont = value;
            OnContentLayoutChanged();
        }
    }

    /// <summary>
    /// </summary>
    [Category("Layout"), DefaultValue(270)]
    public int StartAngle { get; set; }

    /// <summary>
    /// </summary>
    [Category("Layout"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Padding SubscriptMargin
    {
        get => _subscriptMargin;
        set
        {
            if (_subscriptMargin == value)
            {
                return;
            }

            _subscriptMargin = value;
            OnContentLayoutChanged();
        }
    }

    /// <summary>
    /// </summary>
    [Category("Appearance"), DefaultValue("")]
    public string SubscriptText
    {
        get => _subscriptText;
        set
        {
            value ??= string.Empty;

            if (_subscriptText == value)
            {
                return;
            }

            _subscriptText = value;
            OnContentLayoutChanged();
        }
    }

    /// <summary>
    /// </summary>
    [Category("Layout"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Padding SuperscriptMargin
    {
        get => _superscriptMargin;
        set
        {
            if (_superscriptMargin == value)
            {
                return;
            }

            _superscriptMargin = value;
            OnContentLayoutChanged();
        }
    }

    /// <summary>
    /// </summary>
    [Category("Appearance"), DefaultValue("")]
    public string SuperscriptText
    {
        get => _superscriptText;
        set
        {
            value ??= string.Empty;

            if (_superscriptText == value)
            {
                return;
            }

            _superscriptText = value;
            OnContentLayoutChanged();
        }
    }

    /// <summary>
    ///     Gets or sets the text in the <see cref="KryptonCircularProgressBar" />.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    [AllowNull]
    public override string Text
    {
        get => base.Text;
        set => base.Text = value;
    }

    /// <summary>
    /// </summary>
    [Category("Layout"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Padding TextMargin
    {
        get => _textMargin;
        set
        {
            if (_textMargin == value)
            {
                return;
            }

            _textMargin = value;
            OnContentLayoutChanged();
        }
    }

    /// <summary>Gets or sets the current position of the progress bar.</summary>
    [Category("Behavior")]
    [Bindable(true)]
    [Description("Gets or sets the current position of the progress bar.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(0)]
    public new int Value
    {
        get => base.Value;
        set
        {
            if (base.Value == value)
            {
                return;
            }

            base.Value = value;
            OnContentLayoutChanged();
        }
    }

    /// <summary>
    /// Gets access to the common outer ring appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common outer ring appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDoubleRedirect OuterRingStateCommon => _outerRingStateCommon;

    private bool ShouldSerializeOuterRingStateCommon() => !OuterRingStateCommon.IsDefault;

    /// <summary>
    /// Gets access to the normal outer ring appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal outer ring appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDouble OuterRingStateNormal => _outerRingStateNormal;

    private bool ShouldSerializeOuterRingStateNormal() => !OuterRingStateNormal.IsDefault;

    /// <summary>
    /// Gets access to the disabled outer ring appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled outer ring appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDouble OuterRingStateDisabled => _outerRingStateDisabled;

    private bool ShouldSerializeOuterRingStateDisabled() => !OuterRingStateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the common inner ring appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common inner ring appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDoubleRedirect InnerRingStateCommon => _innerRingStateCommon;

    private bool ShouldSerializeInnerRingStateCommon() => !InnerRingStateCommon.IsDefault;

    /// <summary>
    /// Gets access to the normal inner ring appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal inner ring appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDouble InnerRingStateNormal => _innerRingStateNormal;

    private bool ShouldSerializeInnerRingStateNormal() => !InnerRingStateNormal.IsDefault;

    /// <summary>
    /// Gets access to the disabled inner ring appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled inner ring appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDouble InnerRingStateDisabled => _innerRingStateDisabled;

    private bool ShouldSerializeInnerRingStateDisabled() => !InnerRingStateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the common superscript appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common superscript text appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect SuperscriptStateCommon => _superscriptStateCommon;

    private bool ShouldSerializeSuperscriptStateCommon() => !SuperscriptStateCommon.IsDefault;

    /// <summary>
    /// Gets access to the normal superscript appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal superscript text appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple SuperscriptStateNormal => _superscriptStateNormal;

    private bool ShouldSerializeSuperscriptStateNormal() => !SuperscriptStateNormal.IsDefault;

    /// <summary>
    /// Gets access to the disabled superscript appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled superscript text appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple SuperscriptStateDisabled => _superscriptStateDisabled;

    private bool ShouldSerializeSuperscriptStateDisabled() => !SuperscriptStateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the common subscript appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common subscript text appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect SubscriptStateCommon => _subscriptStateCommon;

    private bool ShouldSerializeSubscriptStateCommon() => !SubscriptStateCommon.IsDefault;

    /// <summary>
    /// Gets access to the normal subscript appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal subscript text appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple SubscriptStateNormal => _subscriptStateNormal;

    private bool ShouldSerializeSubscriptStateNormal() => !SubscriptStateNormal.IsDefault;

    /// <summary>
    /// Gets access to the disabled subscript appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled subscript text appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple SubscriptStateDisabled => _subscriptStateDisabled;

    private bool ShouldSerializeSubscriptStateDisabled() => !SubscriptStateDisabled.IsDefault;

    #endregion

    #region Constructor

    /// <summary>Initializes a new instance of the <see cref="KryptonCircularProgressBar"/> class.</summary>
    public KryptonCircularProgressBar()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);

        _animator = DesignMode ? null : new WinFormAnimation.Animator();

        AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Linear;

        AnimationSpeed = 500;

        MarqueeAnimationSpeed = 2000;

        StartAngle = 270;

        _lastValue = Value;

        DoubleBuffered = true;

        Font = new Font(Font.FontFamily, 72, FontStyle.Bold);

        SecondaryFont = new Font(Font.FontFamily, (float)(Font.Size * .5), FontStyle.Regular);

        OuterMargin = -25;

        OuterWidth = 26;

        ProgressWidth = 25;

        InnerMargin = 2;

        InnerWidth = -1;

        TextMargin = Padding.Empty;

        Value = 0;

        SuperscriptMargin = Padding.Empty;

        SuperscriptText = string.Empty;

        SubscriptMargin = Padding.Empty;

        SubscriptText = string.Empty;

        BackColor = Color.Transparent;

        _outerRingStateCommon = new PaletteDoubleRedirect(ProgressPaletteRedirect,
            PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, OnCircularNeedPaint);
        _outerRingStateNormal = new PaletteDouble(_outerRingStateCommon, OnCircularNeedPaint);
        _outerRingStateDisabled = new PaletteDouble(_outerRingStateCommon, OnCircularNeedPaint);

        _innerRingStateCommon = new PaletteDoubleRedirect(ProgressPaletteRedirect,
            PaletteBackStyle.PanelAlternate, PaletteBorderStyle.ControlClient, OnCircularNeedPaint);
        _innerRingStateNormal = new PaletteDouble(_innerRingStateCommon, OnCircularNeedPaint);
        _innerRingStateDisabled = new PaletteDouble(_innerRingStateCommon, OnCircularNeedPaint);

        _superscriptStateCommon = new PaletteTripleRedirect(ProgressPaletteRedirect,
            PaletteBackStyle.PanelClient, PaletteBorderStyle.ControlClient, PaletteContentStyle.LabelNormalPanel, OnCircularNeedPaint);
        _superscriptStateNormal = new PaletteTriple(_superscriptStateCommon, OnCircularNeedPaint);
        _superscriptStateDisabled = new PaletteTriple(_superscriptStateCommon, OnCircularNeedPaint);

        _subscriptStateCommon = new PaletteTripleRedirect(ProgressPaletteRedirect,
            PaletteBackStyle.PanelClient, PaletteBorderStyle.ControlClient, PaletteContentStyle.LabelNormalPanel, OnCircularNeedPaint);
        _subscriptStateNormal = new PaletteTriple(_subscriptStateCommon, OnCircularNeedPaint);
        _subscriptStateDisabled = new PaletteTriple(_subscriptStateCommon, OnCircularNeedPaint);

        Text = @"0";

        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        Size = GetPreferredSize(Size.Empty);
    }

    #endregion

    #region Methods

    private void OnContentLayoutChanged()
    {
        if (AutoSize && (IsHandleCreated || DesignMode))
        {
            PerformLayout();
        }

        Invalidate();
    }

    private static void ConfigureGraphicsQuality(Graphics graphics)
    {
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
    }

    private float GetPaintScaleFactor()
    {
        float dpiScale = IsHandleCreated && DeviceDpi > 0 ? DeviceDpi / 96f : 1f;
        return Math.Min(MaximumSupersampleScale, Math.Max(MinimumSupersampleScale, dpiScale));
    }

    private void ReleasePaletteMementos()
    {
        _mementoProgressBack?.Dispose();
        _mementoProgressBack = null;
        _mementoOuterRingBack?.Dispose();
        _mementoOuterRingBack = null;
        _mementoInnerRingBack?.Dispose();
        _mementoInnerRingBack = null;
    }

    private static void FillEllipseHole(Graphics graphics, Brush brush, PointF point, SizeF size)
    {
        using GraphicsPath holePath = new GraphicsPath();
        holePath.AddEllipse(point.X, point.Y, size.Width, size.Height);
        graphics.FillPath(brush, holePath);
    }

    private static readonly StringFormat s_centreTextMeasureFormat = CreateCentreTextMeasureFormat();

    private static StringFormat CreateCentreTextMeasureFormat()
    {
        StringFormat format = (StringFormat)StringFormat.GenericTypographic.Clone();
        format.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
        return format;
    }

    private struct CentreTextLayout
    {
        public RectangleF Main;
        public RectangleF Superscript;
        public RectangleF Subscript;
        public bool HasSuperscript;
        public bool HasSubscript;
        public RectangleF Bounds;

        public void Offset(float dx, float dy)
        {
            Main.Offset(dx, dy);

            if (HasSuperscript)
            {
                Superscript.Offset(dx, dy);
            }

            if (HasSubscript)
            {
                Subscript.Offset(dx, dy);
            }

            Bounds.Offset(dx, dy);
        }
    }

    private CentreTextLayout CalculateCentreTextLayout(Graphics graphics, RectangleF textArea)
    {
        string text = Text ?? string.Empty;
        Font mainFont = Font!;
        Font secondaryFont = SecondaryFont ?? mainFont;

        SizeF mainSize = graphics.MeasureString(text, mainFont, int.MaxValue, s_centreTextMeasureFormat);
        bool hasSuperscript = !string.IsNullOrEmpty(SuperscriptText);
        bool hasSubscript = !string.IsNullOrEmpty(SubscriptText);

        var layout = new CentreTextLayout
        {
            Main = new RectangleF(0, 0, mainSize.Width, mainSize.Height),
            HasSuperscript = hasSuperscript,
            HasSubscript = hasSubscript,
            Bounds = new RectangleF(0, 0, mainSize.Width, mainSize.Height)
        };

        if (hasSuperscript || hasSubscript)
        {
            SizeF supSize = hasSuperscript
                ? graphics.MeasureString(SuperscriptText, secondaryFont, int.MaxValue, s_centreTextMeasureFormat)
                : SizeF.Empty;
            SizeF subSize = hasSubscript
                ? graphics.MeasureString(SubscriptText, secondaryFont, int.MaxValue, s_centreTextMeasureFormat)
                : SizeF.Empty;

            float suffixWidth = Math.Max(hasSuperscript ? supSize.Width : 0f, hasSubscript ? subSize.Width : 0f);
            float anchorX = layout.Main.Width - suffixWidth * 0.22f;

            if (hasSuperscript)
            {
                layout.Superscript = new RectangleF(
                    anchorX + SuperscriptMargin.Left,
                    -supSize.Height * 0.40f + SuperscriptMargin.Top,
                    supSize.Width,
                    supSize.Height);
                layout.Bounds = RectangleF.Union(layout.Bounds, layout.Superscript);
            }

            if (hasSubscript)
            {
                layout.Subscript = new RectangleF(
                    anchorX + SubscriptMargin.Left,
                    layout.Main.Height - subSize.Height * 0.60f + SubscriptMargin.Top,
                    subSize.Width,
                    subSize.Height);
                layout.Bounds = RectangleF.Union(layout.Bounds, layout.Subscript);
            }
        }

        float offsetX = textArea.X + (textArea.Width - layout.Bounds.Width) / 2f - layout.Bounds.X;
        float offsetY = textArea.Y + (textArea.Height - layout.Bounds.Height) / 2f - layout.Bounds.Y;
        layout.Offset(offsetX, offsetY);

        return layout;
    }

    private SizeF MeasureTextContent(Graphics graphics)
    {
        if (string.IsNullOrEmpty(Text))
        {
            return SizeF.Empty;
        }

        ConfigureGraphicsQuality(graphics);
        CentreTextLayout layout = CalculateCentreTextLayout(graphics, new RectangleF(0, 0, 10000, 10000));
        return layout.Bounds.Size;
    }

    private Size CalculateContentPreferredSize()
    {
        SizeF contentSize;
        using (var image = new Bitmap(1, 1))
        using (Graphics graphics = Graphics.FromImage(image))
        {
            contentSize = MeasureTextContent(graphics);
        }

        float width = contentSize.Width;
        float height = contentSize.Height;

        if (width <= 0 || height <= 0)
        {
            width = 16;
            height = 16;
        }

        width += TextMargin.Horizontal;
        height += TextMargin.Vertical;

        if (InnerWidth != 0 && InnerWidth >= 0)
        {
            width += 2 * InnerWidth;
            height += 2 * InnerWidth;
        }

        width += 2 * InnerMargin;
        height += 2 * InnerMargin;

        if (ProgressWidth >= 0)
        {
            width += 2 * ProgressWidth;
            height += 2 * ProgressWidth;
        }

        width -= 2 * OuterMargin;
        height -= 2 * OuterMargin;

        if (OuterWidth != 0 && OuterWidth >= 0)
        {
            width += 2 * OuterWidth;
            height += 2 * OuterWidth;
        }

        if (OuterWidth + OuterMargin < 0)
        {
            float offset = Math.Abs(OuterWidth + OuterMargin);
            width += 2 * offset;
            height += 2 * offset;
        }

        width += 4;
        height += 4;

        int diameter = (int)Math.Ceiling(Math.Max(width, height));
        diameter = Math.Max(diameter, MinimumDiameter);

        return new Size(diameter, diameter);
    }

    private static PointF InsetPoint(PointF point, float value)
    {
        point.X += value;
        point.Y += value;
        return point;
    }

    private static SizeF InsetSize(SizeF size, float value)
    {
        size.Width -= 2f * value;
        size.Height -= 2f * value;
        return size;
    }

    private static Rectangle ToRectangle(RectangleF rectangle) => Rectangle.Round(rectangle);

    private void OnCircularNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        if (e.NeedLayout)
        {
            Invalidate(true);
        }
        else
        {
            Invalidate(e.InvalidRect);
        }
    }

    private (IPaletteDouble PaletteState, PaletteState State) GetOuterRingPaletteState() =>
        !Enabled
            ? (_outerRingStateDisabled, PaletteState.Disabled)
            : (_outerRingStateNormal, PaletteState.Normal);

    private (IPaletteDouble PaletteState, PaletteState State) GetInnerRingPaletteState() =>
        !Enabled
            ? (_innerRingStateDisabled, PaletteState.Disabled)
            : (_innerRingStateNormal, PaletteState.Normal);

    private (IPaletteTriple PaletteState, PaletteState State) GetSuperscriptPaletteState() =>
        !Enabled
            ? (_superscriptStateDisabled, PaletteState.Disabled)
            : (_superscriptStateNormal, PaletteState.Normal);

    private (IPaletteTriple PaletteState, PaletteState State) GetSubscriptPaletteState() =>
        !Enabled
            ? (_subscriptStateDisabled, PaletteState.Disabled)
            : (_subscriptStateNormal, PaletteState.Normal);

    private static bool ShouldDrawPaletteBack(IPaletteBack paletteBack, PaletteState state, int ringWidth)
    {
        if (ringWidth == 0)
        {
            return false;
        }

        if (paletteBack.GetBackDraw(state) == InheritBool.False)
        {
            return false;
        }

        Color color = paletteBack.GetBackColor1(state);
        return color != Color.Empty && color.A > 0;
    }

    private static void DrawPaletteEllipseBack(RenderContext renderContext,
        IRenderer renderer,
        RectangleF rectangle,
        IPaletteBack paletteBack,
        PaletteState state,
        ref IDisposable? memento)
    {
        Rectangle bounds = ToRectangle(rectangle);
        if (bounds.Width <= 0 || bounds.Height <= 0)
        {
            return;
        }

        using GraphicsPath path = new GraphicsPath();
        path.AddEllipse(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        memento = renderer.RenderStandardBack.DrawBack(renderContext, bounds, path, paletteBack,
            VisualOrientation.Top, state, memento);
    }

    private static void DrawPalettePieBack(RenderContext renderContext,
        IRenderer renderer,
        RectangleF rectangle,
        float startAngle,
        float sweepAngle,
        IPaletteBack paletteBack,
        PaletteState state,
        ref IDisposable? memento)
    {
        if (Math.Abs(sweepAngle) < 0.01f)
        {
            return;
        }

        Rectangle bounds = ToRectangle(rectangle);
        if (bounds.Width <= 0 || bounds.Height <= 0)
        {
            return;
        }

        using GraphicsPath path = new GraphicsPath();
        path.AddPie(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, startAngle, sweepAngle);
        memento = renderer.RenderStandardBack.DrawBack(renderContext, bounds, path, paletteBack,
            VisualOrientation.Top, state, memento);
    }

    /// <summary>
    ///     Initialize the animation for the continues styling
    /// </summary>
    /// <param name="firstTime">True if it is the first execution of this function, otherwise false</param>
    protected virtual void InitializeContinues(bool firstTime)
    {
        if (_lastValue == Value && !firstTime)
        {
            return;
        }

        _lastValue = Value;

        _animator?.Stop();
        _animatedStartAngle = null;

        if (AnimationSpeed <= 0)
        {
            _animatedValue = Value;
            Invalidate();

            return;
        }

        _animator!.Paths = new[]
        {
            new WinFormAnimation.Path(_animatedValue ?? Value, Value, (ulong)AnimationSpeed, CustomAnimationFunction)
        };
        _animator.Repeat = false;
        _animator.Play(
            new WinFormAnimation.SafeInvoker<float>(
                v =>
                {
                    try
                    {
                        _animatedValue = v;
                        Invalidate();
                    }
                    catch
                    {
                        _animator.Stop();
                    }
                },
                this));
    }

    /// <summary>
    ///     Initialize the animation for the marquee styling
    /// </summary>
    /// <param name="firstTime">True if it is the first execution of this function, otherwise false</param>
    protected virtual void InitializeMarquee(bool firstTime)
    {
        if (!firstTime &&
            (_animator?.ActivePath == null ||
             _animator.ActivePath.Duration == (ulong)MarqueeAnimationSpeed &&
             _animator.ActivePath.Function == CustomAnimationFunction))
        {
            return;
        }

        _animator?.Stop();
        _animatedValue = null;

        if (AnimationSpeed <= 0)
        {
            _animatedStartAngle = 0;
            Invalidate();

            return;
        }

        _animator!.Paths = new[] { new WinFormAnimation.Path(0, 359, (ulong)MarqueeAnimationSpeed, CustomAnimationFunction) };
        _animator.Repeat = true;
        _animator.Play(
            new WinFormAnimation.SafeInvoker<float>(
                v =>
                {
                    try
                    {
                        _animatedStartAngle = (int)(v % 360);
                        Invalidate();
                    }
                    catch
                    {
                        _animator.Stop();
                    }
                },
                this));
    }

    /// <summary>
    ///     Occurs when parent's display requires redrawing.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="invalidateEventArgs"></param>
    protected virtual void ParentOnInvalidated(object? sender, InvalidateEventArgs invalidateEventArgs) => RecreateBackgroundBrush();

    /// <summary>
    ///     Occurs when the parent resized.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="eventArgs"></param>
    protected virtual void ParentOnResize(object? sender, EventArgs eventArgs) => RecreateBackgroundBrush();

    /// <summary>
    ///     Update or create the brush used for drawing the background
    /// </summary>
    protected virtual void RecreateBackgroundBrush()
    {
        lock (this)
        {
            _backBrush?.Dispose();
            _backBrush = new SolidBrush(BackColor);

            if (BackColor.A == 255)
            {
                return;
            }

            if (Parent is { Width: > 0, Height: > 0 })
            {
                using (var parentImage = new Bitmap(Parent.Width, Parent.Height))
                {
                    using (var parentGraphic = Graphics.FromImage(parentImage))
                    {
                        var pe = new PaintEventArgs(parentGraphic,
                            new Rectangle(new Point(0, 0), parentImage.Size));
                        InvokePaintBackground(Parent, pe);
                        InvokePaint(Parent, pe);

                        if (BackColor.A > 0) // Translucent
                        {
                            parentGraphic.FillRectangle(_backBrush, Bounds);
                        }
                    }

                    _backBrush = new TextureBrush(parentImage);
                    ((TextureBrush)_backBrush).TranslateTransform(-Bounds.X, -Bounds.Y);
                }
            }
            else
            {
                _backBrush = new SolidBrush(Color.FromArgb(255, BackColor));
            }
        }
    }

    /// <summary>
    ///     The function responsible for painting the control
    /// </summary>
    /// <param name="e">The paint event arguments.</param>
    protected virtual void StartPaint(PaintEventArgs e)
    {
        PaletteBase? palette = ResolvedPalette;
        if (palette == null || _backBrush == null || Width <= 0 || Height <= 0)
        {
            return;
        }

        IRenderer renderer = palette.GetRenderer();
        float paintScale = GetPaintScaleFactor();
        int bufferWidth = (int)Math.Ceiling(Width * paintScale);
        int bufferHeight = (int)Math.Ceiling(Height * paintScale);

        using var paintSurface = new Bitmap(bufferWidth, bufferHeight, PixelFormat.Format32bppPArgb);
        using Graphics paintGraphics = Graphics.FromImage(paintSurface);
        ConfigureGraphicsQuality(paintGraphics);
        paintGraphics.Clear(Color.Transparent);
        paintGraphics.ScaleTransform(paintScale, paintScale);

        ReleasePaletteMementos();

        using RenderContext renderContext = new RenderContext(this, paintGraphics, new Rectangle(0, 0, Width, Height), renderer);

        var (barPaletteState, barState) = GetProgressBarPaletteState();
        var (outerPaletteState, outerState) = GetOuterRingPaletteState();
        var (innerPaletteState, innerState) = GetInnerRingPaletteState();
        var (superscriptPaletteState, superscriptState) = GetSuperscriptPaletteState();
        var (subscriptPaletteState, subscriptState) = GetSubscriptPaletteState();

        lock (this)
        {
            PaintCircularContent(
                paintGraphics,
                renderContext,
                renderer,
                barPaletteState,
                barState,
                outerPaletteState,
                outerState,
                innerPaletteState,
                innerState,
                superscriptPaletteState,
                superscriptState,
                subscriptPaletteState,
                subscriptState);
        }

        ConfigureGraphicsQuality(e.Graphics);
        e.Graphics.CompositingMode = CompositingMode.SourceOver;
        e.Graphics.DrawImage(paintSurface, 0, 0, Width, Height);
    }

    private void PaintCircularContent(
        Graphics g,
        RenderContext renderContext,
        IRenderer renderer,
        IPaletteTriple barPaletteState,
        PaletteState barState,
        IPaletteDouble outerPaletteState,
        PaletteState outerState,
        IPaletteDouble innerPaletteState,
        PaletteState innerState,
        IPaletteTriple superscriptPaletteState,
        PaletteState superscriptState,
        IPaletteTriple subscriptPaletteState,
        PaletteState subscriptState)
    {
        Brush backBrush = _backBrush!;
        ConfigureGraphicsQuality(g);

        PointF point = new PointF(2f, 2f);
        SizeF size = new SizeF(Width - 4f, Height - 4f);

        if (OuterWidth + OuterMargin < 0)
        {
            float offset = Math.Abs(OuterWidth + OuterMargin);
            point = new PointF(offset, offset);
            size = new SizeF(Width - 2f * offset, Height - 2f * offset);
        }

        if (ShouldDrawPaletteBack(outerPaletteState.PaletteBack, outerState, OuterWidth))
        {
            DrawPaletteEllipseBack(renderContext, renderer, new RectangleF(point, size),
                outerPaletteState.PaletteBack, outerState, ref _mementoOuterRingBack);

            if (OuterWidth >= 0)
            {
                point = InsetPoint(point, OuterWidth);
                size = InsetSize(size, OuterWidth);
                FillEllipseHole(g, backBrush, point, size);
            }
        }

        point = InsetPoint(point, OuterMargin);
        size = InsetSize(size, OuterMargin);

        float sweepAngle = Maximum == Minimum
            ? 0f
            : ((_animatedValue ?? Value) - Minimum) / (Maximum - Minimum) * 360f;

        DrawPalettePieBack(renderContext, renderer, new RectangleF(point, size),
            _animatedStartAngle ?? StartAngle, sweepAngle, ValueBackPalette, barState, ref _mementoProgressBack);

        if (ProgressWidth >= 0)
        {
            point = InsetPoint(point, ProgressWidth);
            size = InsetSize(size, ProgressWidth);
            FillEllipseHole(g, backBrush, point, size);
        }

        point = InsetPoint(point, InnerMargin);
        size = InsetSize(size, InnerMargin);

        if (ShouldDrawPaletteBack(innerPaletteState.PaletteBack, innerState, InnerWidth))
        {
            DrawPaletteEllipseBack(renderContext, renderer, new RectangleF(point, size),
                innerPaletteState.PaletteBack, innerState, ref _mementoInnerRingBack);

            if (InnerWidth >= 0)
            {
                point = InsetPoint(point, InnerWidth);
                size = InsetSize(size, InnerWidth);
                FillEllipseHole(g, backBrush, point, size);
            }
        }

        if (Text == string.Empty)
        {
            return;
        }

        var textArea = new RectangleF(
            point.X + TextMargin.Left,
            point.Y + TextMargin.Top,
            size.Width - TextMargin.Horizontal,
            size.Height - TextMargin.Vertical);

        CentreTextLayout layout = CalculateCentreTextLayout(g, textArea);

        using var drawFormat = new StringFormat(StringFormat.GenericTypographic)
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
            FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap
        };

        if (RightToLeft == RightToLeft.Yes)
        {
            drawFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
        }

        Font secondaryFont = SecondaryFont ?? Font!;

        if (layout.HasSuperscript)
        {
            Color superscriptColor = superscriptPaletteState.PaletteContent!.GetContentShortTextColor1(superscriptState);
            using var superscriptBrush = new SolidBrush(superscriptColor);
            g.DrawString(SuperscriptText, secondaryFont, superscriptBrush, layout.Superscript, drawFormat);
        }

        if (layout.HasSubscript)
        {
            Color subscriptColor = subscriptPaletteState.PaletteContent!.GetContentShortTextColor1(subscriptState);
            using var subscriptBrush = new SolidBrush(subscriptColor);
            g.DrawString(SubscriptText, secondaryFont, subscriptBrush, layout.Subscript, drawFormat);
        }

        Color textColor = barPaletteState.PaletteContent!.GetContentShortTextColor1(barState);
        using var textBrush = new SolidBrush(textColor);
        g.DrawString(Text, Font, textBrush, layout.Main, drawFormat);
    }

    /// <summary>Increments the specified value.</summary>
    /// <param name="step">The step.</param>
    public new void Increment(int step) => Value = Value + step;

    #endregion

    #region Overrides

    /// <inheritdoc />
    public override Size GetPreferredSize(Size proposedSize)
    {
        if (!AutoSize)
        {
            return base.GetPreferredSize(proposedSize);
        }

        Size preferredSize = CalculateContentPreferredSize();

        if (MaximumSize.Width > 0)
        {
            preferredSize.Width = Math.Min(MaximumSize.Width, preferredSize.Width);
        }

        if (MaximumSize.Height > 0)
        {
            preferredSize.Height = Math.Min(MaximumSize.Height, preferredSize.Height);
        }

        if (MinimumSize.Width > 0)
        {
            preferredSize.Width = Math.Max(MinimumSize.Width, preferredSize.Width);
        }

        if (MinimumSize.Height > 0)
        {
            preferredSize.Height = Math.Max(MinimumSize.Height, preferredSize.Height);
        }

        return preferredSize;
    }

    /// <inheritdoc />
    protected override void OnTextChanged(EventArgs e)
    {
        base.OnTextChanged(e);
        OnContentLayoutChanged();
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _mementoProgressBack?.Dispose();
            _mementoOuterRingBack?.Dispose();
            _mementoInnerRingBack?.Dispose();
            _backBrush?.Dispose();
        }

        base.Dispose(disposing);
    }

    /// <inheritdoc />
    protected override void OnLocationChanged(EventArgs e)
    {
        base.OnLocationChanged(e);

        Invalidate();
    }

    /// <inheritdoc />
    protected override void OnPaint(PaintEventArgs e)
    {
        try
        {
            if (!DesignMode)
            {
                if (Style == ProgressBarStyle.Marquee)
                {
                    InitializeMarquee(_lastStyle != Style);
                }
                else
                {
                    InitializeContinues(_lastStyle != Style);
                }

                _lastStyle = Style;
            }

            if (ResolvedPalette == null)
            {
                base.OnPaint(e);
                return;
            }

            SyncThresholdColors();

            if (_backBrush == null)
            {
                RecreateBackgroundBrush();
            }

            StartPaint(e);
        }
        catch (Exception exc)
        {
            DebugTools.NotImplemented(exc.ToString());
        }
    }

    /// <inheritdoc />
    protected override void OnParentBackColorChanged(EventArgs e) => RecreateBackgroundBrush();

    /// <inheritdoc />
    protected override void OnParentBackgroundImageChanged(EventArgs e) => RecreateBackgroundBrush();

    /// <inheritdoc />
    protected override void OnParentChanged(EventArgs e)
    {
        if (Parent != null)
        {
            Parent.Invalidated -= ParentOnInvalidated;

            Parent.Resize -= ParentOnResize;
        }

        base.OnParentChanged(e);

        if (Parent != null)
        {
            Parent.Invalidated += ParentOnInvalidated;

            Parent.Resize += ParentOnResize;
        }
    }

    /// <inheritdoc />
    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);

        ReleasePaletteMementos();
        RecreateBackgroundBrush();
        Invalidate();
    }

    #endregion
}

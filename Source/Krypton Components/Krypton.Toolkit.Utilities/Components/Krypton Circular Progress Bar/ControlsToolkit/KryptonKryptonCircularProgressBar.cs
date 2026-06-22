#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>The circular progress bar windows form control.</summary>
[Description("A circular progress bar for your applications.")]
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonCircularProgressBar), "ToolboxBitmaps.KryptonCircularProgressBar.bmp")]
[DefaultBindingProperty("Value")]
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

    #endregion

    #region Properties

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
            if (value != null)
            {
                base.Font = value;
            }
        }
    }

    /// <summary>
    /// </summary>
    [Category("Layout"), DefaultValue(2)]
    public int InnerMargin { get; set; }

    /// <summary>
    /// </summary>
    [Category("Layout"), DefaultValue(-1)]
    public int InnerWidth { get; set; }

    /// <summary>
    /// </summary>
    [Category("Layout"), DefaultValue(-25)]
    public int OuterMargin { get; set; }

    /// <summary>
    /// </summary>
    [Category("Layout"), DefaultValue(26)]
    public int OuterWidth { get; set; }

    /// <summary>
    /// </summary>
    [Category("Layout"), DefaultValue(25)]
    public int ProgressWidth { get; set; }

    /// <summary>
    /// </summary>
    [Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Font SecondaryFont { get; set; }

    /// <summary>
    /// </summary>
    [Category("Layout"), DefaultValue(270)]
    public int StartAngle { get; set; }

    /// <summary>
    /// </summary>
    [Category("Layout"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Padding SubscriptMargin { get; set; }

    /// <summary>
    /// </summary>
    [Category("Appearance"), DefaultValue("")]
    public string SubscriptText { get; set; }

    /// <summary>
    /// </summary>
    [Category("Layout"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Padding SuperscriptMargin { get; set; }

    /// <summary>
    /// </summary>
    [Category("Appearance"), DefaultValue("")]
    public string SuperscriptText { get; set; }

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
    public Padding TextMargin { get; set; }

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

        TextMargin = new Padding(8, 8, 0, 0);

        Value = 0;

        SuperscriptMargin = new Padding(10, 35, 0, 0);

        SuperscriptText = string.Empty;

        SubscriptMargin = new Padding(10, -35, 0, 0);

        SubscriptText = string.Empty;

        Size = new Size(320, 320);

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
    }

    #endregion

    #region Methods

    private static PointF AddPoint(PointF point, int value)
    {
        point.X += value;

        point.Y += value;

        return point;
    }

    private static SizeF AddSize(SizeF size, int value)
    {
        size.Height += value;

        size.Width += value;

        return size;
    }

    private static Rectangle ToRectangle(RectangleF rectangle) => new Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height);

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
        path.AddEllipse(bounds);
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
        path.AddPie(bounds, startAngle, sweepAngle);
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
        if (palette == null || _backBrush == null)
        {
            return;
        }

        Graphics g = e.Graphics;
        IRenderer renderer = palette.GetRenderer();
        using RenderContext renderContext = new RenderContext(this, g, e.ClipRectangle, renderer);

        var (barPaletteState, barState) = GetProgressBarPaletteState();
        var (outerPaletteState, outerState) = GetOuterRingPaletteState();
        var (innerPaletteState, innerState) = GetInnerRingPaletteState();
        var (superscriptPaletteState, superscriptState) = GetSuperscriptPaletteState();
        var (subscriptPaletteState, subscriptState) = GetSubscriptPaletteState();

        lock (this)
        {
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var point = AddPoint(Point.Empty, 2);
            var size = AddSize(Size, -2 * 2);

            if (OuterWidth + OuterMargin < 0)
            {
                var offset = Math.Abs(OuterWidth + OuterMargin);
                point = AddPoint(Point.Empty, offset);
                size = AddSize(Size, -2 * offset);
            }

            if (ShouldDrawPaletteBack(outerPaletteState.PaletteBack, outerState, OuterWidth))
            {
                DrawPaletteEllipseBack(renderContext, renderer, new RectangleF(point, size),
                    outerPaletteState.PaletteBack, outerState, ref _mementoOuterRingBack);

                if (OuterWidth >= 0)
                {
                    point = AddPoint(point, OuterWidth);
                    size = AddSize(size, -2 * OuterWidth);
                    g.FillEllipse(_backBrush, new RectangleF(point, size));
                }
            }

            point = AddPoint(point, OuterMargin);
            size = AddSize(size, -2 * OuterMargin);

            float sweepAngle = Maximum == Minimum
                ? 0f
                : ((_animatedValue ?? Value) - Minimum) / (Maximum - Minimum) * 360f;

            DrawPalettePieBack(renderContext, renderer, new RectangleF(point, size),
                _animatedStartAngle ?? StartAngle, sweepAngle, ValueBackPalette, barState, ref _mementoProgressBack);

            if (ProgressWidth >= 0)
            {
                point = AddPoint(point, ProgressWidth);
                size = AddSize(size, -2 * ProgressWidth);
                g.FillEllipse(_backBrush, new RectangleF(point, size));
            }

            point = AddPoint(point, InnerMargin);
            size = AddSize(size, -2 * InnerMargin);

            if (ShouldDrawPaletteBack(innerPaletteState.PaletteBack, innerState, InnerWidth))
            {
                DrawPaletteEllipseBack(renderContext, renderer, new RectangleF(point, size),
                    innerPaletteState.PaletteBack, innerState, ref _mementoInnerRingBack);

                if (InnerWidth >= 0)
                {
                    point = AddPoint(point, InnerWidth);
                    size = AddSize(size, -2 * InnerWidth);
                    g.FillEllipse(_backBrush, new RectangleF(point, size));
                }
            }

            if (Text == string.Empty)
            {
                return;
            }

            point.X += TextMargin.Left;
            point.Y += TextMargin.Top;
            size.Width -= TextMargin.Right;
            size.Height -= TextMargin.Bottom;
            var stringFormat =
                new StringFormat(RightToLeft == RightToLeft.Yes ? StringFormatFlags.DirectionRightToLeft : 0)
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Near
                };
            var textSize = g.MeasureString(Text, Font!);
            var textPoint = new PointF(
                point.X + (size.Width - textSize.Width) / 2,
                point.Y + (size.Height - textSize.Height) / 2);

            if (SubscriptText != string.Empty || SuperscriptText != string.Empty)
            {
                float maxSWidth = 0;
                var supSize = SizeF.Empty;
                var subSize = SizeF.Empty;

                if (SuperscriptText != string.Empty)
                {
                    supSize = g.MeasureString(SuperscriptText, SecondaryFont);
                    maxSWidth = Math.Max(supSize.Width, maxSWidth);
                    supSize.Width -= SuperscriptMargin.Right;
                    supSize.Height -= SuperscriptMargin.Bottom;
                }

                if (SubscriptText != string.Empty)
                {
                    subSize = g.MeasureString(SubscriptText, SecondaryFont);
                    maxSWidth = Math.Max(subSize.Width, maxSWidth);
                    subSize.Width -= SubscriptMargin.Right;
                    subSize.Height -= SubscriptMargin.Bottom;
                }

                textPoint.X -= maxSWidth / 4;

                if (SuperscriptText != string.Empty)
                {
                    var supPoint = new PointF(
                        textPoint.X + textSize.Width - supSize.Width / 2,
                        textPoint.Y - supSize.Height * 0.85f);
                    supPoint.X += SuperscriptMargin.Left;
                    supPoint.Y += SuperscriptMargin.Top;
                    Color superscriptColor = superscriptPaletteState.PaletteContent!.GetContentShortTextColor1(superscriptState);
                    using var superscriptBrush = new SolidBrush(superscriptColor);
                    g.DrawString(
                        SuperscriptText,
                        SecondaryFont,
                        superscriptBrush,
                        new RectangleF(supPoint, supSize),
                        stringFormat);
                }

                if (SubscriptText != string.Empty)
                {
                    var subPoint = new PointF(
                        textPoint.X + textSize.Width - subSize.Width / 2,
                        textPoint.Y + textSize.Height * 0.85f);
                    subPoint.X += SubscriptMargin.Left;
                    subPoint.Y += SubscriptMargin.Top;
                    Color subscriptColor = subscriptPaletteState.PaletteContent!.GetContentShortTextColor1(subscriptState);
                    using var subscriptBrush = new SolidBrush(subscriptColor);
                    g.DrawString(
                        SubscriptText,
                        SecondaryFont,
                        subscriptBrush,
                        new RectangleF(subPoint, subSize),
                        stringFormat);
                }
            }

            Color textColor = barPaletteState.PaletteContent!.GetContentShortTextColor1(barState);
            using var textBrush = new SolidBrush(textColor);
            g.DrawString(
                Text,
                Font,
                textBrush,
                new RectangleF(textPoint, textSize),
                stringFormat);
        }
    }

    /// <summary>Increments the specified value.</summary>
    /// <param name="step">The step.</param>
    public new void Increment(int step) => Value = Value + step;

    #endregion

    #region Overrides

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

        Invalidate();
    }

    #endregion
}

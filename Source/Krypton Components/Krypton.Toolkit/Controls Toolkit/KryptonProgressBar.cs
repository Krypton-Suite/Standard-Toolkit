#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2022 - 2025. All rights reserved. 
 */
#endregion


using Timer = System.Windows.Forms.Timer;
// ReSharper disable UnusedMember.Global

namespace Krypton.Toolkit;

/// <summary>Represents a Krypton progress bar control.</summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonProgressBar), "ToolboxBitmaps.KryptonProgressBar.bmp")]
[DefaultProperty("Value")]
[DefaultBindingProperty("Value")]
[DesignerCategory(@"code")]
[Description(@"Represents a Krypton progress bar control.")]
//[Designer(typeof(KryptonButtonDesigner))]
public class KryptonProgressBar : Control, IContentValues
{
    // Progressbar designer is incorrect.
    // Disabled for now.
    // Control works fine without it.
    // Will discuss later if a specific designer is desired and what it should look like.

    #region Instance Fields

    private ProgressBarStyle _style;
    private VisualOrientation _orientation;
    private PaletteBase? _palette;
    private readonly PaletteRedirect _paletteRedirect;
    private readonly PaletteBackInheritRedirect _paletteBackClientPanel;
    private IDisposable? _mementoContent;
    private IDisposable? _mementoBackClientPanel;
    private IDisposable? _mementoBackProgressBar;
    private IDisposable? _mementoBackProgressValue;
    private bool _useValueAsText;
    private int _marqueeSpeed;
    private int _maximum;
    private int _minimum;
    private int _step;
    private int _value;
    private readonly PaletteBack _stateBackValue;
    private readonly Timer _marqueeTimer;
    private int _marqueeLocation;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the KryptonProgressBar class.
    /// </summary>
    public KryptonProgressBar()
    {
        // To remove flicker we use double buffering for drawing
        SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.UseTextForAccessibility, true);
        // The label cannot take the focus
        SetStyle(ControlStyles.Selectable, false);

        // Set default properties
        _useValueAsText = false;
        _style = ProgressBarStyle.Continuous;
        _orientation = VisualOrientation.Top;
        _marqueeSpeed = 100;
        _minimum = 0;
        _maximum = 100;
        _step = 10;
        _value = 0;
        _marqueeLocation = _minimum;
        _marqueeTimer = new Timer
        {
            Interval = _marqueeSpeed
        };
        _marqueeTimer.Tick += OnMarqueeTick;

        // Cache the current global palette setting
        _palette = KryptonManager.CurrentGlobalPalette;

        // Hook into palette events
        if (_palette != null)
        {
            _palette.PalettePaint += OnPalettePaint;
        }

        // Create content storage
        Values = new LabelValues(OnNeedPaintHandler)
        {
            Text = string.Empty
        };
        Values.TextChanged += OnLabelTextChanged;

        // We want to be notified whenever the global palette changes
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

        // Create redirection object to the base palette
        _paletteRedirect = new PaletteRedirect(_palette);

        // Create the palette provider
        _paletteBackClientPanel = new PaletteBackInheritRedirect(_paletteRedirect)
        {
            Style = PaletteBackStyle.PanelClient
        };
        StateCommon = new PaletteTripleRedirect(_paletteRedirect, PaletteBackStyle.ButtonStandalone,
            PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, OnNeedPaintHandler)
        {
            Back =
            {
                Color1 = Color.Green
            }
        };
        StateDisabled = new PaletteTriple(StateCommon, OnNeedPaintHandler);
        ((PaletteBack)StateDisabled.PaletteBack).ColorStyle = PaletteColorStyle.OneNote;
        StateNormal = new PaletteTriple(StateCommon, OnNeedPaintHandler);
        ((PaletteBack)StateNormal.PaletteBack).ColorStyle = PaletteColorStyle.OneNote;
        _stateBackValue = new PaletteTriple(StateCommon, OnNeedPaintHandler).Back;
        _stateBackValue.ColorStyle = PaletteColorStyle.SolidAllLine;
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_mementoContent != null)
            {
                _mementoContent.Dispose();
                _mementoContent = null;
            }

            if (_mementoBackClientPanel != null)
            {
                _mementoBackClientPanel.Dispose();
                _mementoBackClientPanel = null;
            }

            if (_mementoBackProgressBar != null)
            {
                _mementoBackProgressBar.Dispose();
                _mementoBackProgressBar = null;
            }

            if (_mementoBackProgressValue != null)
            {
                _mementoBackProgressValue.Dispose();
                _mementoBackProgressValue = null;
            }

            // Unhook from the palette events
            if (_palette != null)
            {
                _palette.PalettePaint -= OnPalettePaint;
                _palette = null;
            }

            // Unhook from the static events, otherwise we cannot be garbage collected
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets access to the Progress Bar Label values.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Progress Bar Label values")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public LabelValues Values { get; }

    private bool ShouldSerializeValues() => !Values.IsDefault;

    /// <summary>
    /// Gets access to the common ProgressBar appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common ProgressBar appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    private void ResetStateCommon()
    {
        StateCommon.PopulateFromBase(PaletteState.Normal);
        StateCommon.Back.Color1 = Color.Green;
    }

    /// <summary>
    /// Gets access to the disabled ProgressBar appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled ProgressBar appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal ProgressBar appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal ProgressBar appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>Gets or sets the manner in which progress should be indicated on the progress bar.</summary>
    /// <returns>One of the <see cref="T:System.Windows.Forms.ProgressBarStyle" /> values. The default is <see cref="F:System.Windows.Forms.ProgressBarStyle.Blocks" /></returns>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not a member of the <see cref="T:System.Windows.Forms.ProgressBarStyle" /> enumeration.</exception>
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Behavior")]
    [Description("Gets or sets the manner in which progress should be indicated on the progress bar.")]
    [DefaultValue(ProgressBarStyle.Continuous)]
    public ProgressBarStyle Style
    {
        get => _style;
        set
        {
            if (_style == value)
            {
                return;
            }

            _style = value;
            if (_style == ProgressBarStyle.Marquee)
            {
                StartMarquee();
            }
            else
            {
                Invalidate();
                _marqueeTimer.Stop();
                _stateBackValue.ColorStyle = PaletteColorStyle.SolidAllLine;
            }
        }
    }

    private bool ShouldSerializeStyle() => Style != ProgressBarStyle.Continuous;

    private void ResetStyle() => Style = ProgressBarStyle.Continuous;

    /// <summary>Gets or sets the time period, in milliseconds, that it takes the progress block to scroll across the progress bar.</summary>
    /// <returns>The time period, in milliseconds, that it takes the progress block to scroll across the progress bar.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The indicated time period is less than 0.</exception>
    [Category("Behavior")]
    [Description(
        "Gets or sets the time period, in milliseconds, that it takes the progress block to scroll across the progress bar.")]
    [DefaultValue(100)]
    public int MarqueeAnimationSpeed
    {
        get => _marqueeSpeed;
        set
        {
            _marqueeSpeed = value >= 0
                ? value
                : throw new ArgumentOutOfRangeException($@"{nameof(MarqueeAnimationSpeed)} must be non-negative");
            if (DesignMode)
            {
                return;
            }

            StartMarquee();
        }
    }

    /// <summary>Gets or sets the maximum value of the range of the control.</summary>
    /// <returns>The maximum value of the range. The default is 100.</returns>
    /// <exception cref="T:System.ArgumentException">The value specified is less than 0.</exception>
    [Category("Behavior")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets the maximum value of the range of the control.")]
    [DefaultValue(100)]
    public int Maximum
    {
        get => _maximum;
        set
        {
            if (_maximum == value)
            {
                return;
            }

            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Maximum), value.ToString(CultureInfo.CurrentCulture));
            }

            if (_minimum > value)
            {
                _minimum = value;
            }

            _maximum = value;
            Invalidate();
        }
    }

    /// <summary>Gets or sets the minimum value of the range of the control.</summary>
    /// <returns>The minimum value of the range. The default is 0.</returns>
    /// <exception cref="T:System.ArgumentException">The value specified for the property is less than 0.</exception>
    [Category("Behavior")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets the minimum value of the range of the control.")]
    [DefaultValue(0)]
    public int Minimum
    {
        get => _minimum;
        set
        {
            if (_minimum == value)
            {
                return;
            }

            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Minimum), value.ToString(CultureInfo.CurrentCulture));
            }

            if (_maximum < value)
            {
                _maximum = value;
            }

            _minimum = value;
            Invalidate();
        }
    }

    /// <summary>Gets or sets the amount by which a call to the <see cref="M:System.Windows.Forms.ProgressBar.PerformStep" /> method increases the current position of the progress bar.</summary>
    /// <returns>The amount by which to increment the progress bar with each call to the <see cref="M:System.Windows.Forms.ProgressBar.PerformStep" /> method. The default is 10.</returns>
    [Category("Behavior")]
    [Description(
        "Gets or sets the amount by which a call to the `PerformStep` method increases the current position of the progress bar.")]
    [DefaultValue(10)]
    public int Step
    {
        get => _step;
        set => _step = value;
    }

    /// <summary>Gets or sets the current position of the progress bar.</summary>
    /// <returns>The position within the range of the progress bar. The default is 0.</returns>
    /// <exception cref="T:System.ArgumentException">The value specified is greater than the value of the <see cref="P:System.Windows.Forms.ProgressBar.Maximum" /> property.
    /// -or-
    /// The value specified is less than the value of the <see cref="P:System.Windows.Forms.ProgressBar.Minimum" /> property.</exception>
    [Category("Behavior")]
    [Bindable(true)]
    [Description("Gets or sets the current position of the progress bar.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(0)]
    public int Value
    {
        get => _value;
        set
        {
            if (_value == value)
            {
                return;
            }

            if (value < _minimum || value > _maximum)
            {
                throw new ArgumentOutOfRangeException(nameof(Value), value.ToString(CultureInfo.CurrentCulture));
            }

            _value = value;

            if (_useValueAsText)
            {
                Text = $@"{value}%";
            }

            Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets the text associated with this control. 
    /// </summary>
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue("")]
    [AllowNull]
    public override string Text
    {
        // Values.Text can be set to null
        // The getter will always return a string

        get => Values.Text;

        set
        {
            Values.Text = value;
            OnLayout(new LayoutEventArgs(this, nameof(Text)));
            Invalidate();
        }
    }

    private bool ShouldSerializeText() => !string.IsNullOrWhiteSpace(Values.Text);

    /// <inheritdoc />
    public override void ResetText() => Text = string.Empty;

    /// <summary>Advances the current position of the progress bar by the specified amount.</summary>
    /// <param name="value">The amount by which to increment the progress bar's current position.</param>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.ProgressBar.Style" /> property is set to <see cref="F:System.Windows.Forms.ProgressBarStyle.Marquee" /></exception>
    public void Increment(int value)
    {
        if (Style == ProgressBarStyle.Marquee)
        {
            throw new InvalidOperationException(@"The `Style` property is set to `ProgressBarStyle.Marquee`");
        }

        _value += value;
        if (_value < _minimum)
        {
            _value = _minimum;
        }

        if (_value > _maximum)
        {
            _value = _maximum;
        }

        Invalidate();
    }

    /// <summary>Advances the current position of the progress bar by the amount of the <see cref="P:System.Windows.Forms.ProgressBar.Step" /> property.</summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="P:System.Windows.Forms.ProgressBar.Style" /> is set to <see cref="F:System.Windows.Forms.ProgressBarStyle.Marquee" />.</exception>
    public void PerformStep()
    {
        Increment(_step);
    }

    /// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.ProgressBar" /> control.</summary>
    /// <returns>A string that represents the current <see cref="T:System.Windows.Forms.ProgressBar" />.</returns>
    public override string ToString() =>
        $"{base.ToString()}, Minimum: {Minimum.ToString(CultureInfo.CurrentCulture)}, Maximum: {Maximum.ToString(CultureInfo.CurrentCulture)}, Value: {Value.ToString(CultureInfo.CurrentCulture)}";

    /// <summary>
    /// Gets and sets the visual orientation of the control.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Visual orientation of the control.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(VisualOrientation.Top)]
    public VisualOrientation Orientation
    {
        get => _orientation;

        set
        {
            if (_orientation != value)
            {
                _orientation = value;
                // Update the associated visual elements that are effected
                PerformLayout();
                Invalidate();
            }
        }
    }

    /// <summary>Gets or sets a value indicating whether [use value as text].</summary>
    /// <value><c>true</c> if [use value as text]; otherwise, <c>false</c>.</value>
    [Category(@"Visuals")]
    [Description(@"Use the progress value as text.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(false)]
    public bool UseValueAsText
    {
        get => _useValueAsText;

        set
        {
            _useValueAsText = value;

            UpdateTextWithValue(value);
        }
    }

    #endregion

    #region IContentValues
    /// <summary>
    /// Gets the content short text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetShortText() => Values.GetShortText();

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetLongText() => Values.GetLongText();

    /// <summary>
    /// Gets the content image.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Image value.</returns>
    public Image? GetImage(PaletteState state) => Values.GetImage(state);

    /// <summary>
    /// Gets the image colour that should be transparent.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Colour value.</returns>
    public Color GetImageTransparentColor(PaletteState state) => Values.GetImageTransparentColor(state);
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Gets the default size of the control.
    /// </summary>
    protected override Size DefaultSize => new Size(100, 26);

    /// <summary>
    /// Gets the default Input Method Editor (IME) mode supported by this control.
    /// </summary>
    protected override ImeMode DefaultImeMode => ImeMode.Disable;

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        // Change in enabled state requires a layout and repaint
        Invalidate();

        // Let base class fire standard event
        base.OnEnabledChanged(e);
    }

    /// <inheritdoc />
    protected override void OnLayout(LayoutEventArgs e)
    {
        if (_palette != null)
        {
            // We want the inner part of the control to draw like a button.
            var (barPaletteState, barState) = GetBarPaletteState();

            // Get the renderer associated with this palette
            IRenderer renderer = _palette.GetRenderer();

            // Create a layout context used to allow the renderer to layout the content
            using var viewContext = new ViewLayoutContext(this, renderer);

            // Cleanup resources by disposing of old memento instance
            _mementoContent?.Dispose();

            // Ask the renderer to work out how the Content values will be laid out and
            // return a memento object that we cache for use when actually performing painting
            _mementoContent = renderer.RenderStandardContent.LayoutContent(viewContext, ClientRectangle, barPaletteState.PaletteContent!,
                this, Orientation, barState);
        }

        base.OnLayout(e);
    }

    /// <inheritdoc />
    protected override void OnPaint(PaintEventArgs e)
    {
        // Get the renderer associated with this palette
        IRenderer renderer = _palette!.GetRenderer();

        // Create the rendering context that is passed into all renderer calls
        using var renderContext = new RenderContext(this, e.Graphics, e.ClipRectangle, renderer);
        // Set the style we want picked up from the base palette
        var (barPaletteState, barState) = GetBarPaletteState();

        // Draw the background of the entire control over the entire client area. 
        using (GraphicsPath path = CreateRectGraphicsPath(ClientRectangle))
        {
            var panelState = !Parent!.Enabled
                ? PaletteState.Disabled
                : PaletteState.Normal;
            // Ask renderer to draw the background
            _mementoBackClientPanel = renderer.RenderStandardBack.DrawBack(renderContext, ClientRectangle, path, _paletteBackClientPanel, Orientation,
                panelState, _mementoBackClientPanel);
        }

        //////////////////////////////////////////////////////////////////////////////////
        // In case the border has a rounded effect we need to get the background path   //
        // to draw from the border part of the renderer. It will return a path that is  //
        // appropriate for use drawing within the border settings.                      //
        //////////////////////////////////////////////////////////////////////////////////
        using (GraphicsPath fullLozengePath = renderer.RenderStandardBorder.GetBackPath(renderContext,
                   ClientRectangle,
                   barPaletteState.PaletteBorder!,
                   Orientation,
                   barState))
        {
            // Ask renderer to draw the background
            using var gh = new GraphicsHint(renderContext.Graphics, barPaletteState.PaletteBorder!.GetBorderGraphicsHint(barState));
            _mementoBackProgressBar = renderer.RenderStandardBack.DrawBack(renderContext, ClientRectangle,
                fullLozengePath, barPaletteState.PaletteBack,
                Orientation, barState, _mementoBackProgressBar);
            using var region = new Region(fullLozengePath);
            // Set the clipping region, So that "Small" rounded values do not escape the draw area
            e.Graphics.SetClip(region, CombineMode.Replace);
        }

        // Create a rectangle inset
        Rectangle innerRect = ClientRectangle;
        var maximumRange = (Maximum - Minimum);
        if (_style == ProgressBarStyle.Marquee)
        {
            float ratio = 1.0f / maximumRange;
            int half = (int)(3 * ratio);
            int lower = Math.Max(_marqueeLocation - Minimum - half, Minimum);
            int higher = Math.Min(lower + half, maximumRange);
            switch (Orientation)
            {
                case VisualOrientation.Top:
                case VisualOrientation.Bottom:
                {
                    int width = innerRect.Width;

                    innerRect.X += (int)(ratio * width * lower);
                    innerRect.Width = (int)(ratio * width * higher);
                    // Now do special clipping handling for curved borders
                    if (innerRect.Right > ClientRectangle.Right)
                    {
                        innerRect.Width -= (innerRect.Right - ClientRectangle.Right);
                    }
                    if (innerRect.X > ClientRectangle.Right)
                    {
                        innerRect.X = ClientRectangle.Right;
                    }
                }
                    break;

                case VisualOrientation.Left:
                case VisualOrientation.Right:
                {
                    int height = innerRect.Height;

                    innerRect.Y += (int)(ratio * height * lower);
                    innerRect.Height = (int)(ratio * height * higher);
                    // Now do special clipping handling for curved borders
                    if (innerRect.Bottom > ClientRectangle.Bottom)
                    {
                        innerRect.Height -= (innerRect.Bottom - ClientRectangle.Bottom);
                    }

                    if (innerRect.Y > ClientRectangle.Bottom)
                    {
                        innerRect.Y = ClientRectangle.Bottom;
                    }
                }
                    break;
            }
        }
        else
        {
            // Draw the value offset
            float v = (Value - Minimum);
            float ratio = v / maximumRange;
            switch (Orientation)
            {
                case VisualOrientation.Top:
                case VisualOrientation.Bottom:
                    innerRect.Width = (int)(ratio * innerRect.Width);
                    if (RightToLeft == RightToLeft.Yes)
                    {
                        innerRect.X = ClientRectangle.Right - innerRect.Width;
                    }

                    break;

                case VisualOrientation.Left:
                case VisualOrientation.Right:
                    innerRect.Height = (int)(ratio * innerRect.Height);
                    if (RightToLeft == RightToLeft.Yes)
                    {
                        innerRect.Y = ClientRectangle.Bottom - innerRect.Height;
                    }

                    break;
            }
        }

        using (GraphicsPath valueLozengePath = renderer.RenderStandardBorder.GetBackPath(renderContext,
                   innerRect,
                   barPaletteState.PaletteBorder!,
                   Orientation,
                   barState))
        {
            using var gh = new GraphicsHint(renderContext.Graphics,
                barPaletteState.PaletteBorder.GetBorderGraphicsHint(PaletteState.Normal));
            // Ask renderer to Fill the Progress lozenge
            _mementoBackProgressValue = renderer.RenderStandardBack.DrawBack(renderContext, innerRect, valueLozengePath, _stateBackValue,
                Orientation, barState, _mementoBackProgressValue);
        }

        // Now we draw the border of the inner area
        renderer.RenderStandardBorder.DrawBorder(renderContext, ClientRectangle, barPaletteState.PaletteBorder,
            Orientation, barState);

        // Last of all we draw the content over the top of the border and background
        renderer.RenderStandardContent.DrawContent(renderContext, ClientRectangle,
            barPaletteState.PaletteContent!, _mementoContent!,
            Orientation, barState, false);

        base.OnPaint(e);
    }
    #endregion

    #region Implementation
    // Find the correct state when getting ProgressBar values
    private (IPaletteTriple barPaletteState, PaletteState barState) GetBarPaletteState()
    {
        return !Enabled
            ? (StateDisabled, PaletteState.Disabled)
            : (StateNormal, PaletteState.Normal);
    }

    private GraphicsPath CreateRectGraphicsPath(Rectangle rect)
    {
        var path = new GraphicsPath();
        path.AddRectangle(rect);
        return path;
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        // Unhook events from old palette
        if (_palette != null)
        {
            _palette.PalettePaint -= OnPalettePaint;
        }

        // Cache the new PaletteBase that is the global palette
        _palette = KryptonManager.CurrentGlobalPalette;
        _paletteRedirect.Target = _palette;

        // Hook into events for the new palette
        if (_palette != null)
        {
            _palette.PalettePaint += OnPalettePaint;
        }

        // Change of palette means we should repaint to show any changes
        Invalidate();
    }

    private void OnNeedPaintHandler(object? sender, NeedLayoutEventArgs e)
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

    // Palette indicates we might need to repaint, so lets do it
    private void OnPalettePaint(object? sender, PaletteLayoutEventArgs e) => Invalidate();
    private void OnLabelTextChanged(object? sender, EventArgs e) => OnTextChanged(EventArgs.Empty);

    private void StartMarquee()
    {
        _stateBackValue.ColorStyle = PaletteColorStyle.GlassNormalFull;//Linear;//GlassNormalFull;

        _marqueeTimer.Interval = _marqueeSpeed;
        _marqueeTimer.Start();
    }

    private void OnMarqueeTick(object? sender, EventArgs e)
    {
        _marqueeLocation++;
        if (_marqueeLocation > Maximum)
        {
            _marqueeLocation = Minimum;
        }
        Invalidate();
    }

    private void UpdateTextWithValue(bool value)
    {
        Text = value
            ? $@"{Value}%"
            : string.Empty;
    }

    #endregion

    #region Designer removal

    /// <inheritdoc />
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool AllowDrop
    {
        get => base.AllowDrop;
        set => base.AllowDrop = value;
    }

    /// <inheritdoc />
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
    }

    /// <inheritdoc />
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Image? BackgroundImage
    {
        get => base.BackgroundImage;
        set => base.BackgroundImage = value;
    }

    /// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ProgressBar.BackgroundImage" /> property changes.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler BackgroundImageChanged
    {
        add => base.BackgroundImageChanged += value;
        remove => base.BackgroundImageChanged -= value;
    }

    /// <inheritdoc />
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override ImageLayout BackgroundImageLayout
    {
        get => base.BackgroundImageLayout;
        set => base.BackgroundImageLayout = value;
    }

    /// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ProgressBar.BackgroundImageLayout" /> property changes.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler BackgroundImageLayoutChanged
    {
        add => base.BackgroundImageLayoutChanged += value;
        remove => base.BackgroundImageLayoutChanged -= value;
    }

    /// <summary>Gets or sets a value indicating whether the control, when it receives focus, causes validation to be performed on any controls that require validation.</summary>
    /// <returns>
    /// <see langword="true" /> if the control, when it receives focus, causes validation to be performed on any controls that require validation; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool CausesValidation
    {
        get => base.CausesValidation;
        set => base.CausesValidation = value;
    }

    /// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ProgressBar.CausesValidation" /> property changes.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler CausesValidationChanged
    {
        add => base.CausesValidationChanged += value;
        remove => base.CausesValidationChanged -= value;
    }

    /// <inheritdoc />
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override ContextMenuStrip? ContextMenuStrip
    {
        get => base.ContextMenuStrip!;
        set => base.ContextMenuStrip = value;
    }

    /// <inheritdoc />
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    protected override bool DoubleBuffered
    {
        get => base.DoubleBuffered;
        set => base.DoubleBuffered = value;
    }

    /// <inheritdoc />
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public override Font Font
    {
        // base.Font will always return a Font
        // base can take null as a value

        get => base.Font;
        set => base.Font = value;
    }

    /// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ProgressBar.Font" /> property changes.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler FontChanged
    {
        add => base.FontChanged += value;
        remove => base.FontChanged -= value;
    }

    /// <inheritdoc />
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color ForeColor
    {
        get => base.ForeColor;
        set => base.ForeColor = value;
    }

    /// <summary>Gets or sets the input method editor (IME) for the <see cref="T:System.Windows.Forms.ProgressBar" /></summary>
    /// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ImeMode ImeMode
    {
        get => base.ImeMode;
        set => base.ImeMode = value;
    }

    /// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ProgressBar.ImeMode" /> property changes.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler ImeModeChanged
    {
        add => base.ImeModeChanged += value;
        remove => base.ImeModeChanged -= value;
    }

    /// <summary>Gets or sets the space between the edges of a <see cref="T:System.Windows.Forms.ProgressBar" /> control and its contents.</summary>
    /// <returns>
    /// <see cref="F:System.Windows.Forms.Padding.Empty" /> in all cases.</returns>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Padding Padding
    {
        get => base.Padding;
        set => base.Padding = value;
    }

    /// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ProgressBar.Padding" /> property changes.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler PaddingChanged
    {
        add => base.PaddingChanged += value;
        remove => base.PaddingChanged -= value;
    }

    /// <summary>Overrides <see cref="P:System.Windows.Forms.Control.TabStop" />.</summary>
    /// <returns>true if the user can set the focus to the control by using the TAB key; otherwise, false. The default is true.</returns>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool TabStop
    {
        get => base.TabStop;
        set => base.TabStop = value;
    }

    /// <summary>Occurs when the <see cref="P:System.Windows.Forms.ProgressBar.TabStop" /> property changes.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler TabStopChanged
    {
        add => base.TabStopChanged += value;
        remove => base.TabStopChanged -= value;
    }

    /// <summary>Occurs when the <see cref="P:System.Windows.Forms.ProgressBar.Text" /> property changes.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler TextChanged
    {
        add => base.TextChanged += value;
        remove => base.TextChanged -= value;
    }

    /// <summary>Occurs when the user double-clicks the control.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler DoubleClick
    {
        add => base.DoubleClick += value;
        remove => base.DoubleClick -= value;
    }

    /// <summary>Occurs when the user double-clicks the control.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event MouseEventHandler MouseDoubleClick
    {
        add => base.MouseDoubleClick += value;
        remove => base.MouseDoubleClick -= value;
    }

    /// <summary>Occurs when the user releases a key while the control has focus.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event KeyEventHandler KeyUp
    {
        add => base.KeyUp += value;
        remove => base.KeyUp -= value;
    }

    /// <summary>Occurs when the user presses a key while the control has focus.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event KeyEventHandler KeyDown
    {
        add => base.KeyDown += value;
        remove => base.KeyDown -= value;
    }

    /// <summary>Occurs when the user presses a key while the control has focus.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event KeyPressEventHandler KeyPress
    {
        add => base.KeyPress += value;
        remove => base.KeyPress -= value;
    }

    /// <summary>Occurs when focus enters the <see cref="T:System.Windows.Forms.ProgressBar" /> control.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler Enter
    {
        add => base.Enter += value;
        remove => base.Enter -= value;
    }

    /// <summary>Occurs when focus leaves the <see cref="T:System.Windows.Forms.ProgressBar" /> control.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler Leave
    {
        add => base.Leave += value;
        remove => base.Leave -= value;
    }

    /// <summary>Occurs when the <see cref="T:System.Windows.Forms.ProgressBar" /> is drawn.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event PaintEventHandler Paint
    {
        add => base.Paint += value;
        remove => base.Paint -= value;
    }
    #endregion
}
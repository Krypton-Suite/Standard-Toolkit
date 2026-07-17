#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avil?s (aka mcpbcs) et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit;

/// <summary></summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonToggleSwitch), "ToolboxBitmaps.KryptonToggleButton.bmp")]
[DefaultEvent(nameof(CheckedChanged))]
[DefaultProperty(nameof(ToggleSwitchValues.Checked))]
[DesignerCategory("code")]
[Description("A toggle switch control.")]
public class KryptonToggleSwitch : Control, IContentValues
{
    #region Instance Fields

    private bool _isTracking;
    private bool _isPressed;
    private bool _isDragging;

    private int _knobSize;
    private int _padding;
    private int _dragStartPosition;

    private RectangleF _knob;

    private PaletteBase? _palette;

    private readonly PaletteRedirect _paletteRedirect;

    private readonly Timer _animationTimer;

    private float _animationPosition;
    private float _dragOffset;
    private float _gradientAnimationProgress = 0f; // Tracks transition from 0 (Off) to 1 (On)
    private float _pulseAnimationPhase;

    private const float PulsePhaseStep = 0.04f;

    private ToggleSwitchValues? _toggleSwitchValues;

    #endregion

    #region Events

    /// <summary>Occurs when [checked changed].</summary>
    [Description("Occurs when the value of the Checked property changes.")]
    public event EventHandler CheckedChanged;

    #endregion

    #region Public Properties

    /// <summary>Gets the state common.</summary>
    /// <value>The state common.</value>
    [Category("Visuals")]
    [Description("Defines the common appearance settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    //private void ResetStateCommon() => StateCommon.Reset();

    [Category("Visuals")]
    [Description("Defines the disabled appearance settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>Gets the state normal.</summary>
    /// <value>The state normal.</value>
    [Category("Visuals")]
    [Description("Defines the normal appearance settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>Gets the state pressed.</summary>
    /// <value>The state pressed.</value>
    [Category("Visuals")]
    [Description("Defines the pressed appearance settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StatePressed { get; }

    private bool ShouldSerializeStatePressed() => !StatePressed.IsDefault;

    /// <summary>Gets the state tracking.</summary>
    /// <value>The state tracking.</value>
    [Category("Visuals")]
    [Description("Defines the tracking appearance settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateTracking { get; }

    private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

    /// <summary>Gets access to the toggle switch values.</summary>
    [Category("Visuals")]
    [Description("Storage for toggle switch appearance and behaviour settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ToggleSwitchValues ToggleSwitchValues => _toggleSwitchValues ??= CreateToggleSwitchValues();

    private ToggleSwitchValues CreateToggleSwitchValues()
    {
        var values = new ToggleSwitchValues();
        values.PropertyChanged += OnToggleSwitchValuesChanged;
        return values;
    }

    private bool ShouldSerializeToggleSwitchValues() => !ToggleSwitchValues.IsDefault;

    /// <summary>Resets the ToggleSwitchValues property to its default value.</summary>
    public void ResetToggleSwitchValues() => ToggleSwitchValues.Reset();

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonToggleSwitch" /> class.</summary>
    public KryptonToggleSwitch()
    {
        DoubleBuffered = true;

        SetStyle(ControlStyles.SupportsTransparentBackColor |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw | ControlStyles.Selectable |
                 ControlStyles.UserPaint, true);

        BackColor = Color.Transparent;

        Font = new Font("Segoe UI", 9f, FontStyle.Bold);

        RightToLeft = RightToLeft.Inherit;

        TabStop = true;

        _knobSize = 20;
        _padding = 4;

        _animationTimer = new Timer { Interval = 15 };
        _animationTimer.Tick += OnAnimationTimerTick;

        // Ensure values exist and are subscribed before Size triggers layout.
        _ = ToggleSwitchValues;

        Size = new Size(90, _knobSize + _padding * 2);

        // Cache the current global palette setting
        _palette = KryptonManager.CurrentGlobalPalette;

        // Hook into palette events
        if (_palette != null)
        {
            _palette.PalettePaintInternal += OnPalettePaint;
        }

        // We want to be notified whenever the global palette changes
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

        // Create redirection object to the base palette
        _paletteRedirect = new PaletteRedirect(_palette);

        // Default state configuration
        StateCommon = new PaletteTripleRedirect(_paletteRedirect, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone);
        StateDisabled = new PaletteTriple(StateCommon, OnNeedPaintHandler);
        StateNormal = new PaletteTriple(StateCommon, OnNeedPaintHandler);
        StatePressed = new PaletteTriple(StateCommon, OnNeedPaintHandler);
        StateTracking = new PaletteTriple(StateCommon, OnNeedPaintHandler);

        //StateCommon.SetRedirector();

        ResetToggleSwitchValues();
        UpdateAnimationStateFromChecked();
        UpdatePulseAnimationState();
    }

    #endregion

    #region Protected Overrides

    /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint">Paint</see> event.</summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs">PaintEventArgs</see> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

        IPaletteTriple state = GetCurrentState();

        // Adjust the rectangle for border width
        float borderWidth = state.PaletteBorder!.GetBorderWidth(PaletteState.Normal);
        Rectangle adjustedBounds = AdjustForBorder(ClientRectangle, borderWidth);

        DrawBackground(e.Graphics, state, adjustedBounds);
        DrawBorder(e.Graphics, state, adjustedBounds);
        DrawTrackIcons(e.Graphics, state, adjustedBounds);
        DrawKnob(e.Graphics, state);
        DrawOnOffText(e.Graphics, state);

        e.Graphics.ResetClip();
    }

    /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter">MouseEnter</see> event.</summary>
    /// <param name="e">An <see cref="T:System.EventArgs">EventArgs</see> that contains the event data.</param>
    protected override void OnMouseEnter(EventArgs e)
    {
        base.OnMouseEnter(e);
        _isTracking = true;
        Invalidate();
    }

    /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave">MouseLeave</see> event.</summary>
    /// <param name="e">An <see cref="T:System.EventArgs">EventArgs</see> that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        _isTracking = false;
        Invalidate();
    }

    /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown">MouseDown</see> event.</summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs">MouseEventArgs</see> that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);

        Focus();

        if (e.Button == MouseButtons.Left)
        {
            // Toggle regardless of where the user clicks
            ToggleSwitchValues.Checked = !ToggleSwitchValues.Checked;
        }

        Invalidate();
    }


    /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseMove">MouseMove</see> event.</summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs">MouseEventArgs</see> that contains the event data.</param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        if (_isDragging)
        {
            float delta = (IsVerticalLayout() ? e.Y : e.X) - _dragStartPosition;
            _animationPosition = Math.Max(_padding, Math.Min(GetPrimaryDimension() - _knobSize - _padding, _dragOffset + delta));

            Invalidate(); // Redraw the control
        }
    }

    /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp">MouseUp</see> event.</summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs">MouseEventArgs</see> that contains the event data.</param>
    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);

        if (_isDragging)
        {
            // Finish dragging and determine the final toggle state
            _isDragging = false;

            // Determine final state based on knob's position
            float midpoint = (GetPrimaryDimension() - _knobSize) / 2f;
            ToggleSwitchValues.Checked = _animationPosition >= midpoint;
        }
        else if (_isPressed)
        {
            // Toggle state on simple click
            _isPressed = false;
            ToggleSwitchValues.Checked = !ToggleSwitchValues.Checked;
        }

        Invalidate();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        bool stateChanged = false;

        if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab ||
            e.KeyCode == Keys.Add || e.KeyCode == Keys.Subtract || e.KeyCode == Keys.Home ||
            e.KeyCode == Keys.End || e.KeyCode == Keys.PageUp || e.KeyCode == Keys.PageDown ||
            e.KeyCode == Keys.Left || e.KeyCode == Keys.Right ||
            e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
        {
            ToggleSwitchValues.Checked = !ToggleSwitchValues.Checked;
            stateChanged = true;
        }

        if (stateChanged)
        {
            Invalidate();
        }
    }

    /// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
    /// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys">Keys</see> values.</param>
    /// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
    protected override bool IsInputKey(Keys keyData)
    {
        if (keyData == Keys.Tab)
        {
            return true;
        }

        return base.IsInputKey(keyData);
    }


    /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize">Resize</see> event.</summary>
    /// <param name="e">An <see cref="T:System.EventArgs">EventArgs</see> that contains the event data.</param>
    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        using (GraphicsPath roundedPath = GetRoundedRectanglePath(ClientRectangle, GetEffectiveTrackCornerRadius(ClientRectangle)))
        {
            Region = new Region(roundedPath);
        }

        UpdateLayoutMetrics();

        if (_animationTimer != null && !_animationTimer.Enabled && !_isDragging)
        {
            UpdateAnimationStateFromChecked();
        }

        Invalidate();
    }

    /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged">SizeChanged</see> event.</summary>
    /// <param name="e">An <see cref="T:System.EventArgs">EventArgs</see> that contains the event data.</param>
    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);

        if (IsVerticalLayout())
        {
            Width = Math.Max(20, Width);
            Height = Math.Max(50, Height);
        }
        else
        {
            Width = Math.Max(50, Width);
            Height = Math.Max(20, Height);
        }

        UpdateLayoutMetrics();

        if (_animationTimer != null && !_animationTimer.Enabled && !_isDragging)
        {
            UpdateAnimationStateFromChecked();
        }

        Invalidate(); // Force redraw
    }

    /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged">EnabledChanged</see> event.</summary>
    /// <param name="e">An <see cref="T:System.EventArgs">EventArgs</see> that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        UpdatePulseAnimationState();
        Invalidate();
    }

    /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged">VisibleChanged</see> event.</summary>
    /// <param name="e">An <see cref="T:System.EventArgs">EventArgs</see> that contains the event data.</param>
    protected override void OnVisibleChanged(EventArgs e)
    {
        base.OnVisibleChanged(e);
        UpdatePulseAnimationState();
        Invalidate();
    }

    /// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control">Control</see> and its child controls and optionally releases the managed resources.</summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _animationTimer?.Dispose();
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;

            if (_toggleSwitchValues != null)
            {
                _toggleSwitchValues.PropertyChanged -= OnToggleSwitchValuesChanged;

                _toggleSwitchValues = null;
            }

            if (_palette != null)
            {
                _palette.PalettePaintInternal -= OnPalettePaint;

                _palette = null;
            }
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Private Methods

    /// <summary>Starts the animation.</summary>
    private void StartAnimation()
    {
        _animationPosition = ToggleSwitchValues.Checked ? GetOffPosition() : GetOnPosition();
        _gradientAnimationProgress = ToggleSwitchValues.Checked ? 0f : 1f;
        _animationTimer.Start();
    }

    private bool IsVerticalLayout() => ToggleSwitchValues.Orientation == ToggleSwitchOrientation.Vertical;

    private int GetPrimaryDimension() => IsVerticalLayout() ? Height : Width;

    private float GetOffPosition() => _padding;

    private float GetOnPosition() => Math.Max(_padding, GetPrimaryDimension() - _knobSize - _padding);

    private float GetTargetPosition() => ToggleSwitchValues.Checked ? GetOnPosition() : GetOffPosition();

    private void UpdateAnimationStateFromChecked()
    {
        _animationPosition = GetTargetPosition();
        _gradientAnimationProgress = ToggleSwitchValues.Checked ? 1f : 0f;
    }

    /// <summary>Gets the state of the current.</summary>
    private IPaletteTriple GetCurrentState()
    {
        return !Enabled 
            ? StateDisabled 
            : _isPressed 
                ? StatePressed 
                : _isTracking 
                    ? StateTracking 
                    : (StateNormal != null 
                        ? StateNormal 
                        : StateCommon);
    }

    private void UpdateLayoutMetrics()
    {
        if (IsVerticalLayout())
        {
            if (UsesThinTrackLayout() || UsesOversizedKnobLayout())
            {
                _knobSize = Math.Max(10, (int)(Width * 0.82f));
                _padding = Math.Max(2, (int)(Width * 0.09f));
            }
            else
            {
                _padding = Math.Max(2, Width / 8);
                _knobSize = Math.Max(10, Math.Min(Width - _padding * 2, Height / 3));
            }
        }
        else if (UsesThinTrackLayout() || UsesOversizedKnobLayout())
        {
            _knobSize = Math.Max(10, (int)(Height * 0.82f));
            _padding = Math.Max(2, (int)(Height * 0.09f));
        }
        else
        {
            _padding = Math.Max(2, Height / 8);
            _knobSize = Math.Max(10, Math.Min(Height - _padding * 2, Width / 3));
        }
    }

    private bool UsesThinTrackLayout() => ToggleSwitchValues.KnobStyle == ToggleSwitchKnobStyle.ThinTrack;

    private bool UsesOversizedKnobLayout() => ToggleSwitchValues.KnobStyle == ToggleSwitchKnobStyle.Metallic;

    private bool UsesMetallicLayout() => ToggleSwitchValues.KnobStyle == ToggleSwitchKnobStyle.Metallic;

    private bool UsesStyleColoredTrack() =>
        ToggleSwitchValues.KnobStyle == ToggleSwitchKnobStyle.ThinTrack
        || ToggleSwitchValues.KnobStyle == ToggleSwitchKnobStyle.Pill
        || ToggleSwitchValues.KnobStyle == ToggleSwitchKnobStyle.Metallic;

    private bool UsesSquareKnobShape() =>
        ToggleSwitchValues.KnobStyle == ToggleSwitchKnobStyle.Square
        || ToggleSwitchValues.KnobStyle == ToggleSwitchKnobStyle.Grip
        || ToggleSwitchValues.KnobStyle == ToggleSwitchKnobStyle.Chevron
        || ToggleSwitchValues.KnobStyle == ToggleSwitchKnobStyle.Indicator
        || ToggleSwitchValues.KnobStyle == ToggleSwitchKnobStyle.RoundedSquare;

    private int GetEffectiveTrackCornerRadius(Rectangle bounds)
    {
        if (ToggleSwitchValues.KnobStyle == ToggleSwitchKnobStyle.Pill
            || ToggleSwitchValues.KnobStyle == ToggleSwitchKnobStyle.Metallic
            || UsesThinTrackLayout())
        {
            int crossDimension = IsVerticalLayout() ? bounds.Width : bounds.Height;
            return Math.Max(1, crossDimension / 2);
        }

        return ToggleSwitchValues.CornerRadius;
    }

    private Rectangle GetTrackBounds(Rectangle bounds)
    {
        if (!UsesThinTrackLayout())
        {
            return bounds;
        }

        if (IsVerticalLayout())
        {
            int trackWidth = Math.Max(4, (int)(bounds.Width * 0.32f));
            int x = bounds.X + (bounds.Width - trackWidth) / 2;

            return new Rectangle(x, bounds.Y + 2, trackWidth, Math.Max(8, bounds.Height - 4));
        }

        int trackHeight = Math.Max(4, (int)(bounds.Height * 0.32f));
        int y = bounds.Y + (bounds.Height - trackHeight) / 2;

        return new Rectangle(bounds.X + 2, y, Math.Max(8, bounds.Width - 4), trackHeight);
    }

    private Color ResolveTrackColor(IPaletteTriple state)
    {
        if (UsesStyleColoredTrack() || UseCustomKnobColors())
        {
            return ToggleSwitchValues.Checked ? ToggleSwitchValues.Colors.EffectiveOnColor : ToggleSwitchValues.Colors.EffectiveOffColor;
        }

        if (ToggleSwitchValues.Colors.UseThemeColors && KryptonManager.CurrentGlobalPalette != null)
        {
            return ToggleSwitchValues.Checked
                ? state.PaletteBack.GetBackColor1(PaletteState.Pressed)
                : state.PaletteBack.GetBackColor1(PaletteState.Normal);
        }

        return ToggleSwitchValues.Checked ? ToggleSwitchValues.Colors.EffectiveOnColor : ToggleSwitchValues.Colors.EffectiveOffColor;
    }

    private Color ResolveDecorativeKnobColor(IPaletteTriple state, Color faceColor)
    {
        if (ToggleSwitchValues.KnobStyle == ToggleSwitchKnobStyle.ThinTrack
            || ToggleSwitchValues.KnobStyle == ToggleSwitchKnobStyle.Pill
            || ToggleSwitchValues.KnobStyle == ToggleSwitchKnobStyle.Metallic)
        {
            return Color.White;
        }

        return faceColor;
    }

    private Color ResolveIndicatorDotColor(IPaletteTriple state, Color faceColor)
    {
        if (UseCustomKnobColors())
        {
            return ToggleSwitchValues.Checked ? DarkenColor(ToggleSwitchValues.Colors.EffectiveOnColor, 50) : DarkenColor(ToggleSwitchValues.Colors.EffectiveOffColor, 50);
        }

        return DarkenColor(faceColor, 70);
    }

    private static GraphicsPath CreateRectanglePath(RectangleF bounds)
    {
        GraphicsPath path = new GraphicsPath();
        path.AddRectangle(bounds);
        return path;
    }

    /// <summary>Gets the knob rectangle.</summary>
    private RectangleF GetKnobRectangle()
    {
        float knobDiameter = _knobSize;
        float position = _animationPosition > 0f ? _animationPosition : GetTargetPosition();

        position = Math.Max(GetOffPosition(), Math.Min(GetOnPosition(), position));

        if (IsVerticalLayout())
        {
            float x = (Width - knobDiameter) / 2f;
            return new RectangleF(x, position, knobDiameter, knobDiameter);
        }

        float horizontalPosition = position;
        if (RightToLeft == RightToLeft.Yes)
        {
            horizontalPosition = Width - knobDiameter - position;
        }

        float y = (Height - knobDiameter) / 2f;

        return new RectangleF(horizontalPosition, y, knobDiameter, knobDiameter);
    }


    /// <summary>Gets the rounded rectangle path.</summary>
    /// <param name="bounds">The bounds.</param>
    /// <param name="cornerRadius">The corner radius.</param>
    private GraphicsPath GetRoundedRectanglePath(Rectangle bounds, int cornerRadius)
    {
        int radius = cornerRadius; // Use corner radius proportional to height
        GraphicsPath path = new GraphicsPath();

        path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90); // Top-left
        path.AddArc(bounds.Right - radius, bounds.Y, radius, radius, 270, 90); // Top-right
        path.AddArc(bounds.Right - radius, bounds.Bottom - radius, radius, radius, 0, 90); // Bottom-right
        path.AddArc(bounds.X, bounds.Bottom - radius, radius, radius, 90, 90); // Bottom-left
        path.CloseFigure();

        return path;
    }

    /// <summary>Gets the rounded rectangle.</summary>
    /// <param name="bounds">The bounds.</param>
    /// <param name="radius">The radius.</param>
    private GraphicsPath GetRoundedRectangle(Rectangle bounds, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        int diameter = radius * 2;

        // Top-left arc
        path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
        // Top-right arc
        path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
        // Bottom-right arc
        path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
        // Bottom-left arc
        path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);

        path.CloseFigure();
        return path;
    }

    private GraphicsPath GetRoundedRectangle(RectangleF bounds, float radius)
    {
        radius = Math.Max(1f, Math.Min(radius, Math.Min(bounds.Width, bounds.Height) / 2f));
        float diameter = radius * 2f;
        GraphicsPath path = new GraphicsPath();

        path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
        path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
        path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();

        return path;
    }

    /// <summary>Adjusts the brightness.</summary>
    /// <param name="color">The color.</param>
    /// <param name="factor">The factor.</param>
    private Color AdjustBrightness(Color color, float factor)
    {
        int r = (int)(color.R * factor);
        int g = (int)(color.G * factor);
        int b = (int)(color.B * factor);

        return Color.FromArgb(color.A, Math.Min(r, 255), Math.Min(g, 255), Math.Min(b, 255));
    }

    private Color GetLightColor(Color color, float factor) => AdjustBrightness(color, 1f + factor);

    private Color GetDarkColor(Color color, float factor) => AdjustBrightness(color, 1f - factor);

    private static Color LightenColor(Color color, byte amount)
    {
        int r = Math.Min(255, color.R + amount);
        int g = Math.Min(255, color.G + amount);
        int b = Math.Min(255, color.B + amount);

        return Color.FromArgb(color.A, r, g, b);
    }

    private static Color DarkenColor(Color color, byte amount)
    {
        int r = Math.Max(0, color.R - amount);
        int g = Math.Max(0, color.G - amount);
        int b = Math.Max(0, color.B - amount);

        return Color.FromArgb(color.A, r, g, b);
    }

    private float GetKnobHighlightPenWidth() => Math.Max(1.25f, Math.Min(_knob.Width, _knob.Height) * 0.09f);

    /// <summary>Adjusts for border.</summary>
    /// <param name="bounds">The bounds.</param>
    /// <param name="borderWidth">Width of the border.</param>
    private Rectangle AdjustForBorder(Rectangle bounds, float borderWidth)
    {
        return new Rectangle(
            (int)(bounds.X + borderWidth / 2),
            (int)(bounds.Y + borderWidth / 2),
            (int)(bounds.Width - borderWidth),
            (int)(bounds.Height - borderWidth)
        );
    }

    private void DrawBackground(Graphics graphics, IPaletteTriple state, Rectangle bounds)
    {
        Rectangle trackBounds = GetTrackBounds(bounds);
        int cornerRadius = GetEffectiveTrackCornerRadius(trackBounds);
        Color trackColor = ResolveTrackColor(state);

        // Emboss effect
        if (ToggleSwitchValues.EnableEmbossEffect)
        {
            Color embossColor = KryptonManager.CurrentGlobalPalette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Disabled);

            using (GraphicsPath embossPath = GetRoundedRectangle(trackBounds, cornerRadius))
            {
                using (Brush embossBrush = new SolidBrush(Color.FromArgb(50, embossColor)))
                {
                    graphics.TranslateTransform(2, 2); // Offset for emboss effect
                    graphics.FillPath(embossBrush, embossPath);
                    graphics.TranslateTransform(-2, -2); // Reset transform
                }
            }
        }

        using (GraphicsPath backgroundPath = GetRoundedRectangle(trackBounds, cornerRadius))
        {
            if (ToggleSwitchValues.KnobStyle == ToggleSwitchKnobStyle.Pill
                || ToggleSwitchValues.KnobStyle == ToggleSwitchKnobStyle.Metallic)
            {
                using (LinearGradientBrush backgroundBrush = new LinearGradientBrush(
                           trackBounds,
                           LightenColor(trackColor, 36),
                           DarkenColor(trackColor, 28),
                           LinearGradientMode.Vertical))
                {
                    graphics.FillPath(backgroundBrush, backgroundPath);
                }

                if (UsesMetallicLayout())
                {
                    DrawTrackRecess(graphics, backgroundPath, trackBounds, IsVerticalLayout());
                }
            }
            else
            {
                using (Brush backgroundBrush = new SolidBrush(
                           UsesStyleColoredTrack() ? trackColor : state.PaletteBack.GetBackColor1(PaletteState.Normal)))
                {
                    graphics.FillPath(backgroundBrush, backgroundPath);
                }
            }
        }
    }

    private void DrawBorder(Graphics graphics, IPaletteTriple state, Rectangle bounds)
    {
        if (UsesThinTrackLayout())
        {
            return;
        }

        Rectangle trackBounds = GetTrackBounds(bounds);
        int cornerRadius = GetEffectiveTrackCornerRadius(trackBounds);
        Color trackColor = ResolveTrackColor(state);

        // Border with rounded corners
        using (GraphicsPath borderPath = GetRoundedRectangle(trackBounds, cornerRadius))
        {
            Color borderColor = UsesMetallicLayout()
                ? DarkenColor(trackColor, 55)
                : state.PaletteBorder!.GetBorderColor1(PaletteState.Normal);
            float borderWidth = UsesMetallicLayout()
                ? 1f
                : state.PaletteBorder!.GetBorderWidth(PaletteState.Normal);

            using (Pen borderPen = new Pen(borderColor, borderWidth))
            {
                graphics.DrawPath(borderPen, borderPath);
            }
        }
    }

    private static void DrawTrackRecess(Graphics graphics, GraphicsPath trackPath, Rectangle trackBounds, bool verticalLayout)
    {
        Rectangle shadeBounds = verticalLayout
            ? new Rectangle(trackBounds.X, trackBounds.Y, Math.Max(4, trackBounds.Width / 2), trackBounds.Height)
            : new Rectangle(trackBounds.X, trackBounds.Y, trackBounds.Width, Math.Max(4, trackBounds.Height / 2));

        using (LinearGradientBrush recessBrush = new LinearGradientBrush(
                   shadeBounds,
                   Color.FromArgb(95, 0, 0, 0),
                   Color.FromArgb(10, 0, 0, 0),
                   verticalLayout ? LinearGradientMode.Horizontal : LinearGradientMode.Vertical))
        {
            graphics.FillPath(recessBrush, trackPath);
        }
    }

    private void DrawKnob(Graphics graphics, IPaletteTriple state)
    {
        RectangleF originalKnob = GetKnobRectangle();
        _knob = originalKnob;
        ResolveKnobColors(state, out Color faceColor1, out Color faceColor2, out Color borderColor, out Color trackColor);

        if (ShouldRunPulseAnimation())
        {
            DrawKnobPulseGlow(graphics, faceColor1);
            _knob = GetPulsedKnobRectangle(originalKnob);
        }

        switch (ToggleSwitchValues.KnobStyle)
        {
            case ToggleSwitchKnobStyle.Gradient:
                DrawGradientKnob(graphics, faceColor1, faceColor2, borderColor);
                break;
            case ToggleSwitchKnobStyle.Flat:
                DrawFlatKnob(graphics, faceColor1, borderColor);
                break;
            case ToggleSwitchKnobStyle.Radial:
                DrawRadialKnob(graphics, faceColor1, faceColor2, borderColor);
                break;
            case ToggleSwitchKnobStyle.Ring:
                DrawRingKnob(graphics, faceColor1, faceColor2, borderColor, trackColor);
                break;
            case ToggleSwitchKnobStyle.Bevel:
                DrawBevelKnob(graphics, faceColor1, faceColor2, borderColor);
                break;
            case ToggleSwitchKnobStyle.RoundedSquare:
                DrawRoundedSquareKnob(graphics, faceColor1, faceColor2, borderColor);
                break;
            case ToggleSwitchKnobStyle.Square:
                DrawSquareKnob(graphics, faceColor1, faceColor2, borderColor);
                break;
            case ToggleSwitchKnobStyle.Grip:
                DrawGripKnob(graphics, faceColor1, faceColor2, borderColor);
                break;
            case ToggleSwitchKnobStyle.Chevron:
                DrawChevronKnob(graphics, faceColor1, faceColor2, borderColor);
                break;
            case ToggleSwitchKnobStyle.Indicator:
                DrawIndicatorKnob(graphics, state, faceColor1, faceColor2, borderColor);
                break;
            case ToggleSwitchKnobStyle.ThinTrack:
                DrawThinTrackKnob(graphics, state, faceColor1, faceColor2, borderColor);
                break;
            case ToggleSwitchKnobStyle.Pill:
                DrawPillKnob(graphics, state, faceColor1, faceColor2, borderColor);
                break;
            case ToggleSwitchKnobStyle.Metallic:
                DrawMetallicKnob(graphics, state, faceColor1, faceColor2, borderColor);
                break;
            default:
                DrawClassicKnob(graphics, faceColor1, faceColor2);
                break;
        }

        _knob = originalKnob;
    }

    private bool ShouldRunPulseAnimation() =>
        ToggleSwitchValues.Pulse.Enable &&
        ToggleSwitchValues.Pulse.Intensity > float.Epsilon &&
        Enabled &&
        Visible;

    private float GetKnobPulseFactor() =>
        (float)(Math.Sin(_pulseAnimationPhase * Math.PI * 2d) * 0.5d + 0.5d);

    private RectangleF GetPulsedKnobRectangle(RectangleF knob)
    {
        float pulse = GetKnobPulseFactor();
        float intensity = ToggleSwitchValues.Pulse.Intensity;
        float scale = 1f + pulse * 0.08f * intensity;
        float centerX = knob.X + knob.Width / 2f;
        float centerY = knob.Y + knob.Height / 2f;
        float width = knob.Width * scale;
        float height = knob.Height * scale;

        return new RectangleF(centerX - width / 2f, centerY - height / 2f, width, height);
    }

    private void DrawKnobPulseGlow(Graphics graphics, Color pulseColor)
    {
        float pulse = GetKnobPulseFactor();
        float intensity = ToggleSwitchValues.Pulse.Intensity;
        float expansion = 1f + pulse * (6f * intensity + 2f);
        int alpha = (int)(35 + pulse * 145 * intensity);

        RectangleF glow = _knob;
        glow.Inflate(expansion, expansion);

        using (GraphicsPath glowPath = CreateKnobGlowPath(glow))
        using (SolidBrush glowBrush = new SolidBrush(Color.FromArgb(alpha, pulseColor)))
        {
            graphics.FillPath(glowBrush, glowPath);
        }
    }

    private GraphicsPath CreateKnobGlowPath(RectangleF glow)
    {
        if (UsesSquareKnobShape())
        {
            float radius = ToggleSwitchValues.KnobStyle == ToggleSwitchKnobStyle.Square
                || ToggleSwitchValues.KnobStyle == ToggleSwitchKnobStyle.Grip
                || ToggleSwitchValues.KnobStyle == ToggleSwitchKnobStyle.Chevron
                ? 0f
                : Math.Max(3f, Math.Min(glow.Width, glow.Height) * 0.25f);

            return radius > 0f ? GetRoundedRectangle(glow, radius) : CreateRectanglePath(glow);
        }

        GraphicsPath glowPath = new GraphicsPath();
        glowPath.AddEllipse(glow);
        return glowPath;
    }

    private bool IsSlideAnimating() => Math.Abs(_animationPosition - GetTargetPosition()) >= 0.5f;

    private void UpdatePulseAnimationState()
    {
        if (_animationTimer == null)
        {
            return;
        }

        if (ShouldRunPulseAnimation())
        {
            if (!_animationTimer.Enabled && !IsSlideAnimating())
            {
                _animationTimer.Start();
            }
        }
        else if (!IsSlideAnimating())
        {
            _animationTimer.Stop();
            _pulseAnimationPhase = 0f;
        }
    }

    private bool UseCustomKnobColors() =>
        !ToggleSwitchValues.Colors.UseThemeColors || ToggleSwitchValues.Colors.OnlyShowColorOnKnob;

    private void ResolveKnobColors(IPaletteTriple state, out Color faceColor1, out Color faceColor2, out Color borderColor, out Color trackColor)
    {
        float progress = ToggleSwitchValues.Gradient.Animate ? _gradientAnimationProgress : ToggleSwitchValues.Checked ? 1f : 0f;
        bool useGradient = ToggleSwitchValues.Gradient.Enable || ToggleSwitchValues.KnobStyle == ToggleSwitchKnobStyle.Gradient;

        if (useGradient)
        {
            if (UseCustomKnobColors())
            {
                Color offStart = DarkenColor(ToggleSwitchValues.Colors.EffectiveOffColor, 30);
                Color onStart = DarkenColor(ToggleSwitchValues.Colors.EffectiveOnColor, 30);
                faceColor1 = InterpolateColor(offStart, onStart, progress);
                faceColor2 = InterpolateColor(ToggleSwitchValues.Colors.EffectiveOffColor, ToggleSwitchValues.Colors.EffectiveOnColor, progress);
            }
            else if (ToggleSwitchValues.Colors.UseThemeColors && KryptonManager.CurrentGlobalPalette != null)
            {
                Color themeStartChecked = KryptonManager.CurrentGlobalPalette.GetContentShortTextColor1(PaletteContentStyle.ButtonStandalone, PaletteState.CheckedNormal);
                Color themeEndChecked = KryptonManager.CurrentGlobalPalette.GetContentShortTextColor2(PaletteContentStyle.ButtonStandalone, PaletteState.CheckedNormal);
                Color themeStartNormal = KryptonManager.CurrentGlobalPalette.GetContentShortTextColor1(PaletteContentStyle.ButtonStandalone, PaletteState.Normal);
                Color themeEndNormal = KryptonManager.CurrentGlobalPalette.GetContentShortTextColor2(PaletteContentStyle.ButtonStandalone, PaletteState.Normal);

                faceColor1 = InterpolateColor(themeStartNormal, themeStartChecked, progress);
                faceColor2 = InterpolateColor(themeEndNormal, themeEndChecked, progress);
            }
            else
            {
                Color offStart = DarkenColor(ToggleSwitchValues.Colors.EffectiveOffColor, 30);
                Color onStart = DarkenColor(ToggleSwitchValues.Colors.EffectiveOnColor, 30);
                faceColor1 = InterpolateColor(offStart, onStart, progress);
                faceColor2 = InterpolateColor(ToggleSwitchValues.Colors.EffectiveOffColor, ToggleSwitchValues.Colors.EffectiveOnColor, progress);
            }

            faceColor1 = ApplyGradientIntensity(faceColor1, ToggleSwitchValues.Gradient.StartIntensity);
            faceColor2 = ApplyGradientIntensity(faceColor2, ToggleSwitchValues.Gradient.EndIntensity);
        }
        else
        {
            faceColor1 = ResolveSolidKnobColor(state);
            faceColor2 = AdjustBrightness(faceColor1, 0.8f);
        }

        borderColor = state.PaletteBorder!.GetBorderColor1(PaletteState.Normal);
        trackColor = state.PaletteBack.GetBackColor1(PaletteState.Normal);
    }

    private static Color ApplyGradientIntensity(Color color, float intensity)
    {
        intensity = Math.Max(0f, Math.Min(2f, intensity));

        return Color.FromArgb(
            color.A,
            (int)Math.Min(255, color.R * intensity),
            (int)Math.Min(255, color.G * intensity),
            (int)Math.Min(255, color.B * intensity));
    }

    private bool UsesKnobGradient() =>
        ToggleSwitchValues.Gradient.Enable || ToggleSwitchValues.KnobStyle == ToggleSwitchKnobStyle.Gradient;

    private Color ResolveSolidKnobColor(IPaletteTriple state)
    {
        if (!UseCustomKnobColors() && ToggleSwitchValues.Colors.UseThemeColors && KryptonManager.CurrentGlobalPalette != null)
        {
            return ToggleSwitchValues.Checked ? state.PaletteBack.GetBackColor1(PaletteState.Pressed)
                : _isTracking
                    ? state.PaletteBack.GetBackColor1(PaletteState.Tracking)
                    : state.PaletteBack.GetBackColor2(PaletteState.Normal);
        }

        return ToggleSwitchValues.Checked ? ToggleSwitchValues.Colors.EffectiveOnColor : ToggleSwitchValues.Colors.EffectiveOffColor;
    }

    private void DrawClassicKnob(Graphics graphics, Color faceColor1, Color faceColor2)
    {
        if (UsesKnobGradient())
        {
            DrawGradientKnob(graphics, faceColor1, faceColor2, Color.Empty, drawBorder: false);
        }
        else
        {
            using (SolidBrush knobBrush = new SolidBrush(faceColor1))
            {
                graphics.FillEllipse(knobBrush, _knob);
            }
        }
    }

    private void DrawGradientKnob(Graphics graphics, Color faceColor1, Color faceColor2, Color borderColor, bool drawBorder = true)
    {
        using (LinearGradientBrush knobBrush = new LinearGradientBrush(_knob, faceColor1, faceColor2, ToggleSwitchValues.Gradient.Direction))
        {
            graphics.FillEllipse(knobBrush, _knob);
        }

        if (drawBorder)
        {
            using (Pen borderPen = new Pen(borderColor))
            {
                graphics.DrawEllipse(borderPen, _knob.X, _knob.Y, _knob.Width, _knob.Height);
            }
        }
    }

    private void DrawFlatKnob(Graphics graphics, Color faceColor, Color borderColor)
    {
        using (SolidBrush knobBrush = new SolidBrush(faceColor))
        using (Pen borderPen = new Pen(borderColor))
        {
            graphics.FillEllipse(knobBrush, _knob);
            graphics.DrawEllipse(borderPen, _knob.X, _knob.Y, _knob.Width, _knob.Height);
        }
    }

    private void DrawRadialKnob(Graphics graphics, Color faceColor1, Color faceColor2, Color borderColor)
    {
        Color centerColor = LightenColor(faceColor1, 42);
        Color surroundColor = DarkenColor(faceColor2, 48);

        PointF centerPoint = new PointF(
            _knob.X + _knob.Width * 0.42f,
            _knob.Y + _knob.Height * 0.38f);

        using (GraphicsPath path = new GraphicsPath())
        {
            path.AddEllipse(_knob);

            using (PathGradientBrush knobBrush = new PathGradientBrush(path))
            using (Pen borderPen = new Pen(borderColor))
            {
                knobBrush.CenterColor = centerColor;
                knobBrush.SurroundColors = new[] { surroundColor };
                knobBrush.CenterPoint = centerPoint;
                knobBrush.FocusScales = new PointF(0.35f, 0.35f);

                graphics.FillPath(knobBrush, path);
                graphics.DrawEllipse(borderPen, _knob.X, _knob.Y, _knob.Width, _knob.Height);
            }
        }
    }

    private void DrawRingKnob(Graphics graphics, Color faceColor1, Color faceColor2, Color borderColor, Color trackColor)
    {
        DrawClassicKnob(graphics, faceColor1, faceColor2);

        float inset = Math.Max(3f, Math.Min(_knob.Width, _knob.Height) * 0.28f);
        RectangleF innerRectangle = RectangleF.Inflate(_knob, -inset, -inset);

        using (Pen borderPen = new Pen(borderColor))
        using (SolidBrush innerBrush = new SolidBrush(trackColor))
        using (Pen innerBorderPen = new Pen(GetDarkColor(borderColor, 0.25f)))
        {
            graphics.DrawEllipse(borderPen, _knob.X, _knob.Y, _knob.Width, _knob.Height);
            graphics.FillEllipse(innerBrush, innerRectangle);
            graphics.DrawEllipse(innerBorderPen, innerRectangle.X, innerRectangle.Y, innerRectangle.Width, innerRectangle.Height);
        }
    }

    private void DrawBevelKnob(Graphics graphics, Color faceColor1, Color faceColor2, Color borderColor)
    {
        Color gradientEnd = UsesKnobGradient() ? faceColor2 : DarkenColor(faceColor1, 22);

        using (LinearGradientBrush faceBrush = new LinearGradientBrush(_knob, faceColor1, gradientEnd, ToggleSwitchValues.Gradient.Direction))
        {
            graphics.FillEllipse(faceBrush, _knob);
        }

        float penWidth = GetKnobHighlightPenWidth();

        using (Pen lightPen = new Pen(LightenColor(faceColor1, 60), penWidth))
        using (Pen darkPen = new Pen(DarkenColor(faceColor1, 60), penWidth))
        using (Pen borderPen = new Pen(borderColor))
        {
            lightPen.StartCap = LineCap.Round;
            lightPen.EndCap = LineCap.Round;
            darkPen.StartCap = LineCap.Round;
            darkPen.EndCap = LineCap.Round;

            for (int i = 0; i <= 2; i++)
            {
                float inset = i * (penWidth * 0.35f);
                RectangleF arcRectangle = new RectangleF(
                    _knob.X + inset,
                    _knob.Y + inset,
                    _knob.Width - inset * 2f,
                    _knob.Height - inset * 2f);

                graphics.DrawArc(lightPen, arcRectangle, -50, 165);
                graphics.DrawArc(darkPen, arcRectangle, 130, 165);
            }

            graphics.DrawEllipse(borderPen, _knob.X, _knob.Y, _knob.Width, _knob.Height);
        }
    }

    private void DrawRoundedSquareKnob(Graphics graphics, Color faceColor1, Color faceColor2, Color borderColor)
    {
        float radius = Math.Max(3f, Math.Min(_knob.Width, _knob.Height) * 0.25f);

        using (GraphicsPath knobPath = GetRoundedRectangle(_knob, radius))
        using (Pen borderPen = new Pen(borderColor))
        {
            FillKnobPath(graphics, knobPath, faceColor1, faceColor2);
            graphics.DrawPath(borderPen, knobPath);
        }
    }

    private void FillKnobPath(Graphics graphics, GraphicsPath knobPath, Color faceColor1, Color faceColor2)
    {
        if (UsesKnobGradient())
        {
            using (LinearGradientBrush knobBrush = new LinearGradientBrush(_knob, faceColor1, faceColor2, ToggleSwitchValues.Gradient.Direction))
            {
                graphics.FillPath(knobBrush, knobPath);
            }
        }
        else
        {
            using (SolidBrush knobBrush = new SolidBrush(faceColor1))
            {
                graphics.FillPath(knobBrush, knobPath);
            }
        }
    }

    private void DrawSquareKnob(Graphics graphics, Color faceColor1, Color faceColor2, Color borderColor)
    {
        using (GraphicsPath knobPath = CreateRectanglePath(_knob))
        using (Pen borderPen = new Pen(borderColor))
        {
            FillKnobPath(graphics, knobPath, faceColor1, faceColor2);
            graphics.DrawPath(borderPen, knobPath);
        }
    }

    private void DrawGripKnob(Graphics graphics, Color faceColor1, Color faceColor2, Color borderColor)
    {
        DrawSquareKnob(graphics, faceColor1, faceColor2, borderColor);
        DrawKnobGripLines(graphics, _knob, DarkenColor(faceColor1, 55));
    }

    private void DrawChevronKnob(Graphics graphics, Color faceColor1, Color faceColor2, Color borderColor)
    {
        DrawSquareKnob(graphics, faceColor1, faceColor2, borderColor);
        Color baseGlyphColor = DarkenColor(faceColor1, 70);
        DrawKnobChevronGlyph(graphics, _knob, baseGlyphColor, ResolveChevronGlyphDirection());
    }

    private void DrawKnobChevronGlyph(Graphics graphics, RectangleF knob, Color glyphColor, DropDownArrowGlyphDirection direction)
    {
        Rectangle knobRect = Rectangle.Round(knob);
        int size = ResolveKnobChevronGlyphSize(knobRect);
        if (size <= 0)
        {
            return;
        }

        Color outline = ToggleSwitchValues.Colors.TintColors.ResolveGlyphOutline(glyphColor);
        Color fill = ToggleSwitchValues.Colors.TintColors.ResolveGlyphFill(glyphColor);
        Image glyph = DropDownArrowGlyphCache.GetOrCreate(size, outline, fill, direction);
        int x = knobRect.X + ((knobRect.Width - size) / 2);
        int y = knobRect.Y + ((knobRect.Height - size) / 2);
        graphics.DrawImage(glyph, x, y, size, size);
    }

    private int ResolveKnobChevronGlyphSize(Rectangle knobRect)
    {
        int available = Math.Min(knobRect.Width, knobRect.Height);
        return Math.Max(4, (int)(available * ToggleSwitchValues.Chevron.GlyphSize));
    }

    private DropDownArrowGlyphDirection ResolveChevronGlyphDirection()
    {
        if (IsVerticalLayout())
        {
            switch (ToggleSwitchValues.Chevron.Direction)
            {
                case ToggleSwitchChevronDirection.Left:
                    return DropDownArrowGlyphDirection.Up;
                case ToggleSwitchChevronDirection.Right:
                    return DropDownArrowGlyphDirection.Down;
                default:
                    return ToggleSwitchValues.Checked
                        ? DropDownArrowGlyphDirection.Up
                        : DropDownArrowGlyphDirection.Down;
            }
        }

        bool pointRight;
        switch (ToggleSwitchValues.Chevron.Direction)
        {
            case ToggleSwitchChevronDirection.Left:
                pointRight = false;
                break;
            case ToggleSwitchChevronDirection.Right:
                pointRight = true;
                break;
            default:
                pointRight = !ToggleSwitchValues.Checked;
                break;
        }

        if (RightToLeft == RightToLeft.Yes)
        {
            pointRight = !pointRight;
        }

        return pointRight
            ? DropDownArrowGlyphDirection.Right
            : DropDownArrowGlyphDirection.Left;
    }

    private void DrawIndicatorKnob(Graphics graphics, IPaletteTriple state, Color faceColor1, Color faceColor2, Color borderColor)
    {
        float radius = Math.Max(4f, Math.Min(_knob.Width, _knob.Height) * 0.18f);

        using (GraphicsPath knobPath = GetRoundedRectangle(_knob, radius))
        using (Pen borderPen = new Pen(borderColor))
        {
            FillKnobPath(graphics, knobPath, faceColor1, faceColor2);
            graphics.DrawPath(borderPen, knobPath);
        }

        Color dotColor = ResolveIndicatorDotColor(state, faceColor1);
        float dotSize = Math.Max(3f, Math.Min(_knob.Width, _knob.Height) * 0.22f);
        float centerX = _knob.X + _knob.Width / 2f;
        float centerY = _knob.Y + _knob.Height / 2f;
        RectangleF dotBounds = new RectangleF(centerX - dotSize / 2f, centerY - dotSize / 2f, dotSize, dotSize);

        using (SolidBrush dotBrush = new SolidBrush(dotColor))
        using (Pen dotBorderPen = new Pen(DarkenColor(dotColor, 40)))
        {
            graphics.FillEllipse(dotBrush, dotBounds);
            graphics.DrawEllipse(dotBorderPen, dotBounds.X, dotBounds.Y, dotBounds.Width, dotBounds.Height);
        }
    }

    private void DrawThinTrackKnob(Graphics graphics, IPaletteTriple state, Color faceColor1, Color faceColor2, Color borderColor)
    {
        Color knobColor = ResolveDecorativeKnobColor(state, faceColor1);
        Color ringColor = UseCustomKnobColors()
            ? (ToggleSwitchValues.Checked ? ToggleSwitchValues.Colors.EffectiveOnColor : ToggleSwitchValues.Colors.EffectiveOffColor)
            : borderColor;

        using (SolidBrush knobBrush = new SolidBrush(knobColor))
        using (Pen borderPen = new Pen(ringColor, Math.Max(1f, _knob.Width * 0.05f)))
        {
            graphics.FillEllipse(knobBrush, _knob);
            graphics.DrawEllipse(borderPen, _knob.X, _knob.Y, _knob.Width, _knob.Height);
        }
    }

    private void DrawPillKnob(Graphics graphics, IPaletteTriple state, Color faceColor1, Color faceColor2, Color borderColor)
    {
        Color knobColor = ResolveDecorativeKnobColor(state, faceColor1);
        Color shadowColor = DarkenColor(knobColor, 22);

        using (LinearGradientBrush knobBrush = new LinearGradientBrush(_knob, knobColor, shadowColor, LinearGradientMode.Vertical))
        using (Pen borderPen = new Pen(borderColor))
        {
            graphics.FillEllipse(knobBrush, _knob);
            graphics.DrawEllipse(borderPen, _knob.X, _knob.Y, _knob.Width, _knob.Height);
        }
    }

    private void DrawMetallicKnob(Graphics graphics, IPaletteTriple state, Color faceColor1, Color faceColor2, Color borderColor)
    {
        Color ringColor = ResolveMetallicRingColor(state, faceColor1);
        DrawKnobDropShadow(graphics, _knob);
        DrawBrushedMetalKnob(graphics, _knob, ringColor);
    }

    private Color ResolveMetallicRingColor(IPaletteTriple state, Color faceColor1)
    {
        if (UseCustomKnobColors())
        {
            return ToggleSwitchValues.Checked ? ToggleSwitchValues.Colors.EffectiveOnColor : ToggleSwitchValues.Colors.EffectiveOffColor;
        }

        if (ToggleSwitchValues.Colors.UseThemeColors && KryptonManager.CurrentGlobalPalette != null)
        {
            return ToggleSwitchValues.Checked
                ? state.PaletteBack.GetBackColor1(PaletteState.Pressed)
                : state.PaletteBack.GetBackColor1(PaletteState.Normal);
        }

        return faceColor1;
    }

    private static void DrawKnobDropShadow(Graphics graphics, RectangleF knob)
    {
        RectangleF shadow = knob;
        shadow.Offset(0f, Math.Max(2f, knob.Height * 0.07f));
        shadow.Inflate(1.5f, 1.5f);

        using (GraphicsPath shadowPath = new GraphicsPath())
        {
            shadowPath.AddEllipse(shadow);

            using (PathGradientBrush shadowBrush = new PathGradientBrush(shadowPath))
            {
                shadowBrush.CenterColor = Color.FromArgb(85, 0, 0, 0);
                shadowBrush.SurroundColors = new[] { Color.Transparent };
                graphics.FillPath(shadowBrush, shadowPath);
            }
        }
    }

    private static void DrawBrushedMetalKnob(Graphics graphics, RectangleF knob, Color ringColor)
    {
        float ringThickness = Math.Max(3.5f, Math.Min(knob.Width, knob.Height) * 0.16f);
        RectangleF innerDisc = knob;
        innerDisc.Inflate(-ringThickness, -ringThickness);

        // Coloured outer ring with vertical lighting
        using (LinearGradientBrush ringBrush = new LinearGradientBrush(
                   knob,
                   LightenColor(ringColor, 42),
                   DarkenColor(ringColor, 38),
                   LinearGradientMode.Vertical))
        {
            graphics.FillEllipse(ringBrush, knob);
        }

        // Brushed inner disc
        const int segments = 8;
        Color lightSegment = Color.FromArgb(255, 250, 251, 253);
        Color darkSegment = Color.FromArgb(255, 168, 173, 180);
        float sweepAngle = 360f / segments;

        for (int i = 0; i < segments; i++)
        {
            bool upperHemisphere = i == 0 || i == 1 || i == 7;
            Color segmentColor = i % 2 == 0 ? lightSegment : darkSegment;
            if (!upperHemisphere)
            {
                segmentColor = i % 2 == 0 ? DarkenColor(lightSegment, 18) : DarkenColor(darkSegment, 12);
            }

            using (SolidBrush segmentBrush = new SolidBrush(segmentColor))
            {
                graphics.FillPie(segmentBrush, innerDisc.X, innerDisc.Y, innerDisc.Width, innerDisc.Height, (i * sweepAngle) - 90f, sweepAngle);
            }
        }

        // Horizon lighting across the metal face
        using (GraphicsPath innerPath = new GraphicsPath())
        {
            innerPath.AddEllipse(innerDisc);

            Region previousClip = graphics.Clip;
            graphics.SetClip(innerPath);

            using (LinearGradientBrush horizonBrush = new LinearGradientBrush(
                       innerDisc,
                       Color.FromArgb(70, 255, 255, 255),
                       Color.FromArgb(75, 35, 40, 48),
                       LinearGradientMode.Vertical))
            {
                graphics.FillEllipse(horizonBrush, innerDisc);
            }

            graphics.Clip = previousClip;
        }

        // Centre hub
        float centerX = knob.X + knob.Width / 2f;
        float centerY = knob.Y + knob.Height / 2f;
        float hubRadius = Math.Max(1.5f, Math.Min(innerDisc.Width, innerDisc.Height) * 0.07f);
        RectangleF hubBounds = new RectangleF(centerX - hubRadius, centerY - hubRadius, hubRadius * 2f, hubRadius * 2f);

        using (SolidBrush hubBrush = new SolidBrush(Color.FromArgb(255, 245, 247, 250)))
        using (Pen hubPen = new Pen(Color.FromArgb(120, 130, 135, 142), Math.Max(0.75f, hubRadius * 0.25f)))
        {
            graphics.FillEllipse(hubBrush, hubBounds);
            graphics.DrawEllipse(hubPen, hubBounds.X, hubBounds.Y, hubBounds.Width, hubBounds.Height);
        }

        // Ring and outer edge definition
        using (Pen outerPen = new Pen(DarkenColor(ringColor, 65), Math.Max(1f, ringThickness * 0.12f)))
        using (Pen innerRingPen = new Pen(DarkenColor(ringColor, 28), Math.Max(1f, ringThickness * 0.1f)))
        {
            graphics.DrawEllipse(outerPen, knob.X, knob.Y, knob.Width, knob.Height);
            graphics.DrawEllipse(innerRingPen, innerDisc.X, innerDisc.Y, innerDisc.Width, innerDisc.Height);
        }
    }

    private void DrawTrackIcons(Graphics graphics, IPaletteTriple state, Rectangle bounds)
    {
        if (!ToggleSwitchValues.ShowTrackIcons)
        {
            return;
        }

        RectangleF knob = GetKnobRectangle();

        if (ToggleSwitchValues.Checked)
        {
            RectangleF iconArea = IsVerticalLayout() ? GetTopTrackIconArea(knob) : GetLeftTrackIconArea(knob);
            Color checkColor = ToggleSwitchValues.Colors.TintColors.ResolveGlyphFill(Color.White);
            DrawCheckmark(graphics, iconArea, checkColor);
        }
        else
        {
            RectangleF iconArea = IsVerticalLayout() ? GetBottomTrackIconArea(knob) : GetRightTrackIconArea(knob);
            Color crossColor = UseCustomKnobColors()
                ? DarkenColor(ToggleSwitchValues.Colors.EffectiveOffColor, 45)
                : DarkenColor(ResolveTrackColor(state), 45);
            crossColor = ToggleSwitchValues.Colors.TintColors.ResolveGlyphOutline(crossColor);
            DrawCrossmark(graphics, iconArea, crossColor);
        }
    }

    private RectangleF GetTopTrackIconArea(RectangleF knob) =>
        new RectangleF(knob.X, _padding, knob.Width, Math.Max(0f, knob.Y - _padding));

    private RectangleF GetBottomTrackIconArea(RectangleF knob) =>
        new RectangleF(knob.X, knob.Bottom, knob.Width, Math.Max(0f, Height - _padding - knob.Bottom));

    private RectangleF GetLeftTrackIconArea(RectangleF knob) =>
        new RectangleF(_padding, knob.Y, Math.Max(0f, knob.X - _padding), knob.Height);

    private RectangleF GetRightTrackIconArea(RectangleF knob) =>
        new RectangleF(knob.Right, knob.Y, Math.Max(0f, Width - _padding - knob.Right), knob.Height);

    private static void DrawCheckmark(Graphics graphics, RectangleF area, Color color)
    {
        if (area.Width <= 2f || area.Height <= 2f)
        {
            return;
        }

        float centerX = area.X + area.Width / 2f;
        float centerY = area.Y + area.Height / 2f;
        float size = Math.Min(area.Width, area.Height) * 0.34f;
        float penWidth = Math.Max(1.5f, size * 0.18f);
        PointF[] points =
        {
            new PointF(centerX - size * 0.45f, centerY + size * 0.05f),
            new PointF(centerX - size * 0.05f, centerY + size * 0.42f),
            new PointF(centerX + size * 0.5f, centerY - size * 0.42f)
        };

        using (Pen pen = new Pen(color, penWidth))
        {
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            pen.LineJoin = LineJoin.Round;
            graphics.DrawLines(pen, points);
        }
    }

    private static void DrawCrossmark(Graphics graphics, RectangleF area, Color color)
    {
        if (area.Width <= 2f || area.Height <= 2f)
        {
            return;
        }

        float centerX = area.X + area.Width / 2f;
        float centerY = area.Y + area.Height / 2f;
        float size = Math.Min(area.Width, area.Height) * 0.22f;
        float penWidth = Math.Max(1.5f, size * 0.22f);

        using (Pen pen = new Pen(color, penWidth))
        {
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            graphics.DrawLine(pen, centerX - size, centerY - size, centerX + size, centerY + size);
            graphics.DrawLine(pen, centerX + size, centerY - size, centerX - size, centerY + size);
        }
    }

    private static void DrawKnobGripLines(Graphics graphics, RectangleF bounds, Color lineColor)
    {
        float centerX = bounds.X + bounds.Width / 2f;
        float centerY = bounds.Y + bounds.Height / 2f;
        float lineHeight = bounds.Height * 0.48f;
        float spacing = Math.Max(2f, bounds.Width * 0.14f);
        float penWidth = Math.Max(1f, bounds.Width * 0.05f);

        using (Pen pen = new Pen(lineColor, penWidth))
        {
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;

            for (int i = -1; i <= 1; i++)
            {
                float x = centerX + (i * spacing);
                graphics.DrawLine(pen, x, centerY - lineHeight / 2f, x, centerY + lineHeight / 2f);
            }
        }
    }

    private Color InterpolateColor(Color color1, Color color2, float progress)
    {
        int r = (int)(color1.R + (color2.R - color1.R) * progress);
        int g = (int)(color1.G + (color2.G - color1.G) * progress);
        int b = (int)(color1.B + (color2.B - color1.B) * progress);
        return Color.FromArgb(255, r, g, b);
    }



    /// <summary>Draws the on off text.</summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="state">The state.</param>
    private void DrawOnOffText(Graphics graphics, IPaletteTriple state)
    {
        // Determine the text color
        Color textColor;
        if (ToggleSwitchValues.Colors.UseThemeColors && KryptonManager.CurrentGlobalPalette != null)
        {
            textColor = ToggleSwitchValues.Checked
                ? KryptonManager.CurrentGlobalPalette.GetContentShortTextColor1(PaletteContentStyle.ButtonStandalone, PaletteState.CheckedNormal)
                : KryptonManager.CurrentGlobalPalette.GetContentShortTextColor1(PaletteContentStyle.ButtonStandalone, PaletteState.Normal);
        }
        else
        {
            textColor = ToggleSwitchValues.Checked ? ToggleSwitchValues.Colors.EffectiveOnColor : ToggleSwitchValues.Colors.EffectiveOffColor;
        }

        // Determine the text content
        string text = ToggleSwitchValues.Checked ? KryptonManager.Strings.CustomStrings.On : KryptonManager.Strings.CustomStrings.Off;

        // Define font and measure text size
        float fontSize = (IsVerticalLayout() ? Width : Height) / 3f;
        using (Font font = new Font(Font.FontFamily, fontSize, FontStyle.Bold))
        {
            using (Brush textBrush = new SolidBrush(textColor))
            {
                SizeF textSize = graphics.MeasureString(text, font);

                float textX;
                float textY;
                float textPadding = Math.Max(4, _knobSize / 4); // Ensure a minimum padding

                if (IsVerticalLayout())
                {
                    textX = (Width - textSize.Width) / 2f;
                    float knobEdge = _animationPosition + _knobSize + textPadding;
                    textY = ToggleSwitchValues.Checked
                        ? _padding
                        : Math.Min(Height - textSize.Height - _padding, knobEdge);
                }
                else
                {
                    // Position knob's right edge
                    float knobEdge = _animationPosition + _knobSize + textPadding;

                    // Ensure text remains within bounds
                    textX = ToggleSwitchValues.Checked ? _padding : Math.Min(Width - textSize.Width - _padding, knobEdge);
                    textY = (Height - textSize.Height) / 2f; // Center text vertically
                }

                if (ToggleSwitchValues.ShowText && !ToggleSwitchValues.ShowTrackIcons)
                {
                    // Enable better text rendering for smooth appearance
                    // Use GraphicsTextHint to properly save/restore TextRenderingHint to prevent affecting other controls
                    using (new GraphicsTextHint(graphics, TextRenderingHint.AntiAlias))
                    {
                        // Draw the text
                        graphics.DrawString(text, font, textBrush, new PointF(textX, textY));
                    }
                }
            }
        }
    }

    /// <summary>Called when [need paint handler].</summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="NeedLayoutEventArgs" /> instance containing the event data.</param>
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

    /// <summary>Called when [global palette changed].</summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        // Unhook events from old palette
        if (_palette != null)
        {
            _palette.PalettePaintInternal -= OnPalettePaint;
        }

        // Cache the new PaletteBase that is the global palette
        _palette = KryptonManager.CurrentGlobalPalette;
        _paletteRedirect.Target = _palette;

        // Hook into events for the new palette
        if (_palette != null)
        {
            _palette.PalettePaintInternal += OnPalettePaint;
        }

        // Change of palette means we should repaint to show any changes
        Invalidate();
    }

    /// <summary>Called when [palette paint].</summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="PaletteLayoutEventArgs" /> instance containing the event data.</param>
    private void OnPalettePaint(object? sender, PaletteLayoutEventArgs e) => Invalidate();

    /// <summary>Called when [animation timer tick].</summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void OnAnimationTimerTick(object? sender, EventArgs e)
    {
        bool needsInvalidate = false;

        if (IsSlideAnimating())
        {
            float targetPosition = GetTargetPosition();
            float step = 0.1f; // Adjust for smoothness

            _animationPosition = Lerp(_animationPosition, targetPosition, step);
            _gradientAnimationProgress = GetOnPosition() == GetOffPosition()
                ? (ToggleSwitchValues.Checked ? 1f : 0f)
                : Math.Max(0f, Math.Min(1f, (_animationPosition - GetOffPosition()) / (GetOnPosition() - GetOffPosition())));

            if (Math.Abs(_animationPosition - targetPosition) < 0.5f)
            {
                _animationPosition = targetPosition;
                _gradientAnimationProgress = ToggleSwitchValues.Checked ? 1f : 0f;
            }

            needsInvalidate = true;
        }

        if (ShouldRunPulseAnimation())
        {
            _pulseAnimationPhase += PulsePhaseStep * ToggleSwitchValues.Pulse.Speed;
            if (_pulseAnimationPhase > 1f)
            {
                _pulseAnimationPhase -= 1f;
            }

            needsInvalidate = true;
        }

        if (!IsSlideAnimating() && !ShouldRunPulseAnimation())
        {
            _animationTimer.Stop();
            _pulseAnimationPhase = 0f;
        }

        if (needsInvalidate)
        {
            Invalidate();
        }
    }

    private float Lerp(float start, float end, float amount) => start + (end - start) * amount;

    private void OnToggleSwitchValuesChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ToggleSwitchValues.Checked))
        {
            _animationTimer.Start();
            StartAnimation();
            CheckedChanged?.Invoke(this, EventArgs.Empty);
        }
        else if (e.PropertyName == nameof(ToggleSwitchValues.Pulse))
        {
            UpdatePulseAnimationState();
        }
        else if (e.PropertyName == nameof(ToggleSwitchValues.KnobStyle))
        {
            UpdateLayoutMetrics();

            using (GraphicsPath roundedPath = GetRoundedRectanglePath(ClientRectangle, GetEffectiveTrackCornerRadius(ClientRectangle)))
            {
                Region = new Region(roundedPath);
            }
        }
        else if (e.PropertyName == nameof(ToggleSwitchValues.Orientation))
        {
            UpdateLayoutMetrics();
            UpdateAnimationStateFromChecked();

            using (GraphicsPath roundedPath = GetRoundedRectanglePath(ClientRectangle, GetEffectiveTrackCornerRadius(ClientRectangle)))
            {
                Region = new Region(roundedPath);
            }
        }

        Invalidate();
    }

    #endregion

    #region Hidden Properties

    /// <summary>Gets or sets a value indicating whether the control can accept data that the user drags onto it.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool AllowDrop { get; set; }

    /// <summary>Gets or sets the background color for the control.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color BackColor { get; set; }

    /// <summary>Gets or sets the background image displayed in the control.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public override Image BackgroundImage { get; set; }

    /// <summary>Gets or sets the font of the text displayed by the control.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public override Font Font { get; set; }

    /// <summary>Gets or sets the foreground color of the control.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color ForeColor { get; set; }

    /// <summary>Gets or sets the text associated with this control.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public override string Text { get; set; }

    /// <summary>Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.</summary>
    [Browsable(false)]
    [Category("Behavior")]
    [Description("Indicates whether the control should support RightToLeft layouts.")]
    [DefaultValue(typeof(RightToLeft), "Inherit")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override RightToLeft RightToLeft
    {
        get => base.RightToLeft;
        set
        {
            if (base.RightToLeft != value)
            {
                base.RightToLeft = value;
                Invalidate(); // Repaint when RTL mode changes
            }
        }
    }

    /// <summary>Gets or sets the Input Method Editor (IME) mode of the control.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ImeMode ImeMode
    {
        get => base.ImeMode;
        set => base.ImeMode = value;
    }

    /// <summary>Gets or sets the background image layout as defined in the <see cref="ImageLayout" /> enumeration.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override ImageLayout BackgroundImageLayout { get; set; }

    #endregion
    
    #region IContentValues

    /// <summary>Gets the content image.</summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Image value.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public Image? GetImage(PaletteState state)
    {
        throw new NotImplementedException();
    }

    /// <summary>Gets the image color that should be transparent.</summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Color value.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public Color GetImageTransparentColor(PaletteState state)
    {
        throw new NotImplementedException();
    }

    /// <summary>Gets the content short text.</summary>
    /// <returns>String value.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public string GetShortText()
    {
        throw new NotImplementedException();
    }

    /// <summary>Gets the content long text.</summary>
    /// <returns>String value.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public string GetLongText()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets the overlay image.
    /// </summary>
    /// <param name="state">The state for which the overlay image is needed.</param>
    /// <returns>Overlay image value, or null if no overlay image is set.</returns>
    public Image? GetOverlayImage(PaletteState state) => null;

    /// <summary>
    /// Gets the overlay image color that should be transparent.
    /// </summary>
    /// <param name="state">The state for which the overlay image is needed.</param>
    /// <returns>Color value.</returns>
    public Color GetOverlayImageTransparentColor(PaletteState state) => GlobalStaticVariables.EMPTY_COLOR;

    /// <summary>
    /// Gets the position of the overlay image relative to the main image.
    /// </summary>
    /// <param name="state">The state for which the overlay position is needed.</param>
    /// <returns>Overlay image position.</returns>
    public OverlayImagePosition GetOverlayImagePosition(PaletteState state) => OverlayImagePosition.TopRight;

    /// <summary>
    /// Gets the scaling mode for the overlay image.
    /// </summary>
    /// <param name="state">The state for which the overlay scale mode is needed.</param>
    /// <returns>Overlay image scale mode.</returns>
    public OverlayImageScaleMode GetOverlayImageScaleMode(PaletteState state) => OverlayImageScaleMode.None;

    /// <summary>
    /// Gets the scale factor for the overlay image (used when scale mode is Percentage or ProportionalToMain).
    /// </summary>
    /// <param name="state">The state for which the overlay scale factor is needed.</param>
    /// <returns>Scale factor (0.0 to 2.0).</returns>
    public float GetOverlayImageScaleFactor(PaletteState state) => 0.5f;

    /// <summary>
    /// Gets the fixed size for the overlay image (used when scale mode is FixedSize).
    /// </summary>
    /// <param name="state">The state for which the overlay fixed size is needed.</param>
    /// <returns>Fixed size.</returns>
    public Size GetOverlayImageFixedSize(PaletteState state) => new Size(16, 16);

    #endregion
}
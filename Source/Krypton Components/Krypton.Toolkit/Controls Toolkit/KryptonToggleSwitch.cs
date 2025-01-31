#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit
{
    /// <summary></summary>
    [ToolboxItem(true)]
    [DefaultEvent(nameof(CheckedChanged))]
    [DefaultProperty(nameof(Checked))]
    [DesignerCategory("code")]
    [Description("A toggle switch control.")]
    public class KryptonToggleSwitch : Control, IContentValues
    {
        #region Instance Fields

        private bool _checked;
        private bool _isTracking;
        private bool _isPressed;
        private bool _isDragging;

        private int _knobSize;
        private int _padding;
        private int _dragStartX;

        private RectangleF _knob;

        private PaletteBase? _palette;

        private readonly PaletteRedirect _paletteRedirect;

        private readonly Timer _animationTimer;

        private float _animationPosition;
        private float _dragOffset;
        private float _gradientAnimationProgress = 0f; // Tracks transition from 0 (Off) to 1 (On)

        private ToggleSwitchValues _toggleSwitchValues;

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

        /// <summary>Gets or sets a value indicating whether this <see cref="KryptonToggleSwitch" /> is checked.</summary>
        /// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        [Description("Indicates whether the toggle switch is checked.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Checked
        {
            get => _checked;
            set
            {
                if (_checked != value)
                {
                    _checked = value;

                    _animationTimer.Start();

                    CheckedChanged?.Invoke(this, EventArgs.Empty);
                    StartAnimation();
                }
            }
        }

        /// <summary>Gets or sets the toggle switch values.</summary>
        /// <value>The toggle switch values.</value>
        [Category("Visuals")]
        [Description("Indicates whether the knob should have a gradient effect.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToggleSwitchValues ToggleSwitchValues
        {
            get => _toggleSwitchValues;
            set
            {
                if (_toggleSwitchValues != value)
                {
                    if (_toggleSwitchValues != null)
                    {
                        _toggleSwitchValues.PropertyChanged -= OnToggleSwitchValuesChanged;
                    }

                    _toggleSwitchValues = value;

                    if (_toggleSwitchValues != null)
                    {
                        _toggleSwitchValues.PropertyChanged += OnToggleSwitchValuesChanged;
                    }

                    Invalidate();
                }
            }
        }

        private bool ShouldSerializeToggleSwitchValues() => !ToggleSwitchValues.IsDefault;

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

            _knobSize = 20;
            _padding = 4;
            Size = new Size(90, _knobSize + _padding * 2);

            _animationTimer = new Timer { Interval = 15 };

            _animationTimer.Tick += OnAnimationTimerTick;

            ToggleSwitchValues = new ToggleSwitchValues { OffColor = Color.Red, OnColor = Color.Green };

            KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

            // Initialize PaletteRedirect with a default context
            PaletteRedirect redirector = new PaletteRedirect(KryptonManager.CurrentGlobalPalette);

            // Default state configuration
            StateCommon = new PaletteTripleRedirect(redirector, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone);
            StateDisabled = new PaletteTriple(StateCommon, OnNeedPaintHandler);
            StateNormal = new PaletteTriple(StateCommon, OnNeedPaintHandler);
            StatePressed = new PaletteTriple(StateCommon, OnNeedPaintHandler);
            StateTracking = new PaletteTriple(StateCommon, OnNeedPaintHandler);

            //StateCommon.SetRedirector();

            ResetToggleSwitchValues();
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
                if (_knob.Contains(e.Location))
                {
                    // Allow dragging the knob
                    _isPressed = true;
                    _isDragging = true;
                    _dragStartX = e.X;
                    _dragOffset = _animationPosition;
                }
                else
                {
                    // Adjust toggle behavior for RTL
                    if (RightToLeft == RightToLeft.Yes)
                    {
                        Checked = e.X < Width / 2; // Click on left → "On", Click on right → "Off"
                    }
                    else
                    {
                        Checked = e.X > Width / 2; // Click on right → "On", Click on left → "Off"
                    }
                }
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
                // Update knob position based on mouse movement
                float delta = e.X - _dragStartX;
                _animationPosition = Math.Max(_padding, Math.Min(Width - _knobSize - _padding, _dragOffset + delta));

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
                float midpoint = (Width - _knobSize) / 2f;
                Checked = _animationPosition >= midpoint;
            }
            else if (_isPressed)
            {
                // Toggle state on simple click
                _isPressed = false;
                Checked = !Checked;
            }

            Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            bool stateChanged = false;

            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                Checked = !Checked;
                stateChanged = true;
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                if (RightToLeft == RightToLeft.Yes)
                {
                    // RTL: Left arrow turns it ON, Right arrow turns it OFF
                    if (e.KeyCode == Keys.Left && !Checked)
                    {
                        Checked = true;
                        stateChanged = true;
                    }
                    else if (e.KeyCode == Keys.Right && Checked)
                    {
                        Checked = false;
                        stateChanged = true;
                    }
                }
                else
                {
                    // LTR: Right arrow turns it ON, Left arrow turns it OFF
                    if (e.KeyCode == Keys.Right && !Checked)
                    {
                        Checked = true;
                        stateChanged = true;
                    }
                    else if (e.KeyCode == Keys.Left && Checked)
                    {
                        Checked = false;
                        stateChanged = true;
                    }
                }
            }

            if (stateChanged)
            {
                StartAnimation(); // Smooth animation for keyboard changes
                Invalidate();
            }
        }


        /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize">Resize</see> event.</summary>
        /// <param name="e">An <see cref="T:System.EventArgs">EventArgs</see> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            using (GraphicsPath roundedPath = GetRoundedRectanglePath(ClientRectangle, /*ToggleSwitchValues.CornerRadius*/ 10))
            {
                Region = new Region(roundedPath);
            }

            _knobSize = Math.Min(Height - _padding * 2, Width / 3);

            _padding = Height / 10;

            Invalidate();
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged">SizeChanged</see> event.</summary>
        /// <param name="e">An <see cref="T:System.EventArgs">EventArgs</see> that contains the event data.</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (Width < 50 || Height < 20)
            {
                Width = Math.Max(50, Width);
                Height = Math.Max(20, Height);
            }
        }

        /// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control">Control</see> and its child controls and optionally releases the managed resources.</summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _animationTimer.Dispose();
                KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;

                if (_toggleSwitchValues != null)
                {
                    _toggleSwitchValues.PropertyChanged -= OnToggleSwitchValuesChanged;
                }
            }

            base.Dispose(disposing);
        }

        #endregion

        #region Private Methods

        /// <summary>Starts the animation.</summary>
        private void StartAnimation()
        {
            _animationPosition = Checked ? _padding : Width - _knobSize - _padding;
            _animationTimer.Start();
        }

        /// <summary>Gets the state of the current.</summary>
        private IPaletteTriple GetCurrentState()
        {
            if (!Enabled)
            {
                return StateDisabled;
            }

            if (_isPressed)
            {
                return StatePressed;
            }

            if (_isTracking)
            {
                return StateTracking;
            }

            return StateNormal;
        }

        /// <summary>Gets the knob rectangle.</summary>
        private RectangleF GetKnobRectangle()
        {
            float knobDiameter = _knobSize;

            // Adjust x-position based on RTL
            float x = (RightToLeft == RightToLeft.Yes)
                ? (Checked ? _padding : Width - knobDiameter - _padding)  // RTL: "On" is left, "Off" is right
                : (Checked ? Width - knobDiameter - _padding : _padding); // LTR: "On" is right, "Off" is left

            float y = (Height - knobDiameter) / 2f;

            return new RectangleF(x, y, knobDiameter, knobDiameter);
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
            // Emboss effect
            if (ToggleSwitchValues.EnableEmbossEffect)
            {
                Color embossColor = KryptonManager.CurrentGlobalPalette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Disabled);
                using (GraphicsPath embossPath = GetRoundedRectangle(bounds, ToggleSwitchValues.CornerRadius))
                using (Brush embossBrush = new SolidBrush(Color.FromArgb(50, embossColor)))
                {
                    graphics.TranslateTransform(2, 2); // Offset for emboss effect
                    graphics.FillPath(embossBrush, embossPath);
                    graphics.TranslateTransform(-2, -2); // Reset transform
                }
            }

            // Background with rounded corners
            using (GraphicsPath backgroundPath = GetRoundedRectangle(bounds, ToggleSwitchValues.CornerRadius))
            using (Brush backgroundBrush = new SolidBrush(state.PaletteBack.GetBackColor1(PaletteState.Normal)))
            {
                graphics.FillPath(backgroundBrush, backgroundPath);
            }
        }

        private void DrawBorder(Graphics graphics, IPaletteTriple state, Rectangle bounds)
        {
            // Border with rounded corners
            using (GraphicsPath borderPath = GetRoundedRectangle(bounds, ToggleSwitchValues.CornerRadius))
            using (Pen borderPen = new Pen(state.PaletteBorder!.GetBorderColor1(PaletteState.Normal), state.PaletteBorder.GetBorderWidth(PaletteState.Normal)))
            {
                graphics.DrawPath(borderPen, borderPath);
            }
        }

        private void DrawKnob(Graphics graphics, IPaletteTriple state)
        {
            _knob = GetKnobRectangle();

            if (ToggleSwitchValues.EnableKnobGradient)
            {
                Color startColor, endColor;

                if (ToggleSwitchValues.UseThemeColors && KryptonManager.CurrentGlobalPalette != null)
                {
                    // Get colors from the current theme
                    Color themeStartChecked = KryptonManager.CurrentGlobalPalette.GetContentShortTextColor1(PaletteContentStyle.ButtonStandalone, PaletteState.CheckedNormal);
                    Color themeEndChecked = KryptonManager.CurrentGlobalPalette.GetContentShortTextColor2(PaletteContentStyle.ButtonStandalone, PaletteState.CheckedNormal);
                    Color themeStartNormal = KryptonManager.CurrentGlobalPalette.GetContentShortTextColor1(PaletteContentStyle.ButtonStandalone, PaletteState.Normal);
                    Color themeEndNormal = KryptonManager.CurrentGlobalPalette.GetContentShortTextColor2(PaletteContentStyle.ButtonStandalone, PaletteState.Normal);

                    // Interpolate between the "Off" and "On" colors
                    startColor = InterpolateColor(themeStartNormal, themeStartChecked, _gradientAnimationProgress);
                    endColor = InterpolateColor(themeEndNormal, themeEndChecked, _gradientAnimationProgress);
                }
                else
                {
                    // Default: Smoothly transition between red and green
                    startColor = InterpolateColor(Color.DarkRed, Color.LimeGreen, _gradientAnimationProgress);
                    endColor = InterpolateColor(Color.Red, Color.Green, _gradientAnimationProgress);
                }

                using (LinearGradientBrush knobBrush = new LinearGradientBrush(_knob, startColor, endColor, LinearGradientMode.Vertical))
                {
                    graphics.FillEllipse(knobBrush, _knob);
                }

            }
            else
            {
                Color knobColor; // = _isPressed
                if (ToggleSwitchValues.UseThemeColors && KryptonManager.CurrentGlobalPalette != null && !ToggleSwitchValues.OnlyShowColorOnKnob)
                {
                    knobColor = Checked ? state.PaletteBack.GetBackColor1(PaletteState.Pressed)
                        : _isTracking
                        ? state.PaletteBack.GetBackColor1(PaletteState.Tracking)
                        : state.PaletteBack.GetBackColor2(PaletteState.Normal);
                }
                else
                {
                    knobColor = Checked ? ToggleSwitchValues.OnColor : ToggleSwitchValues.OffColor;
                }

                using (SolidBrush knobBrush = new SolidBrush(knobColor))
                {
                    graphics.FillEllipse(knobBrush, _knob);
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
            if (ToggleSwitchValues.UseThemeColors && KryptonManager.CurrentGlobalPalette != null)
            {
                textColor = Checked
                    ? KryptonManager.CurrentGlobalPalette.GetContentShortTextColor1(PaletteContentStyle.ButtonStandalone, PaletteState.CheckedNormal)
                    : KryptonManager.CurrentGlobalPalette.GetContentShortTextColor1(PaletteContentStyle.ButtonStandalone, PaletteState.Normal);
            }
            else
            {
                textColor = Checked ? ToggleSwitchValues.OnColor : ToggleSwitchValues.OffColor;
            }

            // Determine the text content
            string text = Checked ? KryptonManager.Strings.CustomStrings.On : KryptonManager.Strings.CustomStrings.Off;

            // Define font and measure text size
            float fontSize = Height / 3f;
            using (Font font = new Font(Font.FontFamily, fontSize, FontStyle.Bold))
            using (Brush textBrush = new SolidBrush(textColor))
            {
                SizeF textSize = graphics.MeasureString(text, font);

                // Adjust text position based on RightToLeft setting and knob position
                float textX;
                float knobEdge = _animationPosition + _knobSize; // Position of the knob's right edge
                float textPadding = _padding * 2; // Space between text and knob

                if (RightToLeft == RightToLeft.Yes)
                {
                    // RTL Fix: Ensure "Off" text is not pushed out of bounds
                    if (Checked)
                    {
                        textX = Width - textSize.Width - textPadding; // "On" appears on the left side
                    }
                    else
                    {
                        textX = Math.Max(_padding, knobEdge + textPadding); // Ensure "Off" stays visible
                    }
                }
                else
                {
                    // LTR: Normal positioning
                    if (Checked)
                    {
                        textX = _padding; // "On" appears on the left side
                    }
                    else
                    {
                        textX = Width - textSize.Width - textPadding; // "Off" appears on the right side
                    }
                }

                float textY = (Height - textSize.Height) / 2f; // Center text vertically

                // Enable better text rendering for smooth appearance
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

                // Draw the text
                graphics.DrawString(text, font, textBrush, new PointF(textX, textY));

                // Reset text rendering hint
                graphics.TextRenderingHint = TextRenderingHint.SystemDefault;
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

        /// <summary>Called when [palette paint].</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PaletteLayoutEventArgs" /> instance containing the event data.</param>
        private void OnPalettePaint(object? sender, PaletteLayoutEventArgs e) => Invalidate();

        /// <summary>Called when [animation timer tick].</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnAnimationTimerTick(object? sender, EventArgs e)
        {
            // Determine the correct position for animation
            float targetPosition = (RightToLeft == RightToLeft.Yes)
                ? (Checked ? _padding : Width - _knobSize - _padding)  // RTL: "On" moves to left
                : (Checked ? Width - _knobSize - _padding : _padding); // LTR: "On" moves to right

            float step = 0.1f; // Adjust for smoothness

            _animationPosition = Lerp(_animationPosition, targetPosition, step);

            if (Math.Abs(_animationPosition - targetPosition) < 0.5f)
            {
                _animationPosition = targetPosition;
                _animationTimer.Stop();
            }

            Invalidate(Rectangle.Ceiling(_knob));
        }

        private float Lerp(float start, float end, float amount) => start + (end - start) * amount;

        private void OnToggleSwitchValuesChanged(object? sender, PropertyChangedEventArgs e) => Invalidate();

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
        public override Image BackgroundImage { get; set; }

        /// <summary>Gets or sets the font of the text displayed by the control.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        #endregion
    }
}

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

namespace Krypton.Toolkit
{
    public class KryptonToggleSwitch : Control, IContentValues
    {
        #region Instance Fields

        private bool _checked;
        private bool _isTracking;
        private bool _isPressed;
        private bool _useGradient;

        private float _gradientStartIntensity;
        private float _gradientEndIntensity;

        private int _knobSize;
        private int _padding;

        private RectangleF _knob;

        private PaletteBase? _palette;

        private readonly PaletteRedirect _paletteRedirect;

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
                    CheckedChanged?.Invoke(this, EventArgs.Empty);
                    Invalidate();
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether [enable knob gradient].</summary>
        /// <value><c>true</c> if [enable knob gradient]; otherwise, <c>false</c>.</value>
        [Category("Appearance")]
        [Description("Indicates whether the knob should have a gradient effect.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool EnableKnobGradient
        {
            get => _useGradient;
            set
            {
                if (_useGradient != value)
                {
                    _useGradient = value;
                    Invalidate();
                }
            }
        }

        /// <summary>Gets or sets the gradient start intensity.</summary>
        /// <value>The gradient start intensity.</value>
        [Category("Appearance")]
        [Description("Specifies the gradient intensity for the knob.")]
        [DefaultValue(0.8f)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float GradientStartIntensity
        {
            get => _gradientStartIntensity;
            set
            {
                if (_gradientStartIntensity != value)
                {
                    _gradientStartIntensity = value;
                    Invalidate();
                }
            }
        }

        /// <summary>Gets or sets the gradient end intensity.</summary>
        /// <value>The gradient end intensity.</value>
        [Category("Appearance")]
        [Description("Specifies the gradient intensity for the knob.")]
        [DefaultValue(0.6f)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float GradientEndIntensity
        {
            get => _gradientEndIntensity;
            set
            {
                if (_gradientEndIntensity != value)
                {
                    _gradientEndIntensity = value;
                    Invalidate();
                }
            }
        }

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonToggleSwitch" /> class.</summary>
        public KryptonToggleSwitch()
        {
            DoubleBuffered = true;

            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);

            BackColor = Color.Transparent;
            _knobSize = 20;
            _padding = 4;
            Size = new Size(50, _knobSize + _padding * 2);

            _gradientStartIntensity = 0.8f;
            _gradientEndIntensity = 0.6f;

            KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

            // Initialize PaletteRedirect with a default context
            PaletteRedirect redirector = new PaletteRedirect(KryptonManager.CurrentGlobalPalette);

            // Default state configuration
            StateCommon = new PaletteTripleRedirect(redirector, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone);
            StateDisabled = new PaletteTriple(StateCommon, OnNeedPaintHandler);
            StateNormal = new PaletteTriple(StateCommon, OnNeedPaintHandler);
            StatePressed = new PaletteTriple(StateCommon, OnNeedPaintHandler);
            StateTracking = new PaletteTriple(StateCommon, OnNeedPaintHandler);
        }

        #endregion

        #region Protected Overrides

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

            // Background with rounded corners
            using (GraphicsPath backgroundPath = GetRoundedRectangle(ClientRectangle, 10))
            using (Brush backgroundBrush = new SolidBrush(state.PaletteBack.GetBackColor1(PaletteState.Normal)))
            {
                e.Graphics.FillPath(backgroundBrush, backgroundPath);
            }

            // Border with rounded corners
            using (GraphicsPath borderPath = GetRoundedRectangle(ClientRectangle, 10))
            using (Pen borderPen = new Pen(state.PaletteBorder.GetBorderColor1(PaletteState.Normal), state.PaletteBorder.GetBorderWidth(PaletteState.Normal)))
            {
                e.Graphics.DrawPath(borderPen, borderPath);
            }

            // Knob
            _knob = GetKnobRectangle();

            if (EnableKnobGradient)
            {
                // Fetch colors based on Checked state
                Color startColor = Checked
                    ? AdjustBrightness(state.PaletteBack.GetBackColor1(PaletteState.Checked), GradientStartIntensity) // Slightly darker
                    : AdjustBrightness(state.PaletteBack.GetBackColor1(PaletteState.Normal), GradientStartIntensity);

                Color endColor = Checked
                    ? AdjustBrightness(state.PaletteBack.GetBackColor2(PaletteState.Checked), GradientEndIntensity) // More intense
                    : AdjustBrightness(state.PaletteBack.GetBackColor2(PaletteState.Normal), GradientEndIntensity);

                using (LinearGradientBrush knobBrush = new LinearGradientBrush(
                           _knob,
                           startColor,
                           endColor,
                           LinearGradientMode.ForwardDiagonal))
                {
                    e.Graphics.FillEllipse(knobBrush, _knob);
                }
            }
            else
            {
                Color knobColor = _isPressed
                    ? state.PaletteBack.GetBackColor1(PaletteState.Pressed)
                    : _isTracking
                        ? state.PaletteBack.GetBackColor1(PaletteState.Tracking)
                        : state.PaletteBack.GetBackColor2(PaletteState.Normal);

                using (SolidBrush knobBrush = new SolidBrush(knobColor))
                {
                    e.Graphics.FillEllipse(knobBrush, _knob);
                }
            }

            // Text
            DrawOnOffText(e.Graphics, GetCurrentState());

            e.Graphics.ResetClip();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isTracking = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isTracking = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                _isPressed = true;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (_isPressed)
            {
                _isPressed = false;
                Checked = !Checked;
            }
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            using (GraphicsPath roundedPath = GetRoundedRectanglePath(ClientRectangle, Height / 2))
            {
                Region = new Region(roundedPath);
            }

            _knobSize = Math.Min(Height - _padding * 2, Width / 3);

            _padding = Height / 10;

            Invalidate();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (Width < 50 || Height < 20)
            {
                Width = Math.Max(50, Width);
                Height = Math.Max(20, Height);
            }
        }

        #endregion

        #region Private Methods

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

        private RectangleF GetKnobRectangle()
        {
            float knobDiameter = _knobSize;
            float x = Checked
                ? Width - knobDiameter - _padding
                : _padding;

            float y = (Height - knobDiameter) / 2f;

            return new RectangleF(x, y, knobDiameter, knobDiameter);
        }

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

        private Color AdjustBrightness(Color color, float factor)
        {
            int r = (int)(color.R * factor);
            int g = (int)(color.G * factor);
            int b = (int)(color.B * factor);

            return Color.FromArgb(color.A, Math.Min(r, 255), Math.Min(g, 255), Math.Min(b, 255));
        }

        private Rectangle AdjustForBorder(Rectangle bounds, float borderWidth)
        {
            return new Rectangle(
                (int)(bounds.X + borderWidth / 2),
                (int)(bounds.Y + borderWidth / 2),
                (int)(bounds.Width - borderWidth),
                (int)(bounds.Height - borderWidth)
            );
        }

        private void DrawOnOffText(Graphics graphics, IPaletteTriple state)
        {
            string text = Checked ? KryptonManager.Strings.CustomStrings.On : KryptonManager.Strings.CustomStrings.Off;
            float fontSize = Height / 3f; // Proportional font size
            using (Font font = new Font(Font.FontFamily, fontSize, FontStyle.Bold))
            using (Brush textBrush = new SolidBrush(state.PaletteContent!.GetContentShortTextColor1(PaletteState.Normal)))
            {
                SizeF textSize = graphics.MeasureString(text, font);
                float x = Checked ? Width - _padding - _knobSize - textSize.Width : _padding + _knobSize;
                float y = (Height - textSize.Height) / 2f;

                graphics.DrawString(text, font, textBrush, new PointF(x, y));
            }
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

        private void OnPalettePaint(object? sender, PaletteLayoutEventArgs e) => Invalidate();

        #endregion

        #region Hidden Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color BackColor { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color ForeColor { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Font Font { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Image BackgroundImage { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string Text { get; set; }

        #endregion

        #region IContentValues

        public Image? GetImage(PaletteState state)
        {
            throw new NotImplementedException();
        }

        public Color GetImageTransparentColor(PaletteState state)
        {
            throw new NotImplementedException();
        }

        public string GetShortText()
        {
            throw new NotImplementedException();
        }

        public string GetLongText()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit.Utilities;

[ToolboxBitmap(typeof(ToolStripStatusLabel)), ToolboxItem(false), ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.StatusStrip | ToolStripItemDesignerAvailability.ToolStrip)]
public class KryptonToolStripLabelExtended : ToolStripStatusLabel
{
    #region Variables

    #region Krypton

    private PaletteBase? _palette;

    #endregion

    private readonly ToolStripLabelExtendedValues _values;

    private Timer _fadeAnimationTimer;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the expandable alert/blink/fade/gradient configuration values for designer and runtime use.
    /// </summary>
    [Category("Behavior")]
    [Description("Alert, blink, fade, and gradient settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ToolStripLabelExtendedValues LabelValues => _values;

    private bool ShouldSerializeLabelValues() => !_values.IsDefault;

    private void ResetLabelValues() => _values.Reset();

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="KryptonToolStripLabelExtended"/> is alert.
    /// </summary>
    /// <value>
    ///   <c>true</c> if alert; otherwise, <c>false</c>.
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Alert { get => _values.Alert; set => _values.Alert = value; }

    /// <summary>
    /// Gets or sets a value indicating whether [enable blinking].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [enable blinking]; otherwise, <c>false</c>.
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool EnableBlinking { get => _values.EnableBlinking; set => _values.EnableBlinking = value; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool BkClr { get => _values.BkClr; set => _values.BkClr = value; }

    /// <summary>
    /// Gets or sets a value indicating whether [enable fade animation].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [enable fade animation]; otherwise, <c>false</c>.
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool EnableFadeAnimation { get => _values.EnableFadeAnimation; set => _values.EnableFadeAnimation = value; }

    /// <summary>
    /// Gets or sets the alert colour one.
    /// </summary>
    /// <value>
    /// The alert colour one.
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color AlertColourOne { get => _values.AlertColourOne; set => _values.AlertColourOne = value; }

    /// <summary>
    /// Gets or sets the alert colour two.
    /// </summary>
    /// <value>
    /// The alert colour two.
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color AlertColourTwo { get => _values.AlertColourTwo; set => _values.AlertColourTwo = value; }

    /// <summary>
    /// Gets or sets the alert text colour.
    /// </summary>
    /// <value>
    /// The alert text colour.
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color AlertTextColour { get => _values.AlertTextColour; set => _values.AlertTextColour = value; }

    /// <summary>
    /// Gets or sets the gradient colour one.
    /// </summary>
    /// <value>
    /// The gradient colour one.
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color GradientColourOne { get => _values.GradientColourOne; set => _values.GradientColourOne = value; }

    /// <summary>
    /// Gets or sets the gradient colour two.
    /// </summary>
    /// <value>
    /// The gradient colour two.
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color GradientColourTwo { get => _values.GradientColourTwo; set => _values.GradientColourTwo = value; }

    /// <summary>
    /// Gets or sets the text glow colour.
    /// </summary>
    /// <value>
    /// The text glow colour.
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color TextGlow { get => _values.TextGlow; set => _values.TextGlow = value; }

    /// <summary>
    /// Gets or sets the gradient mode.
    /// </summary>
    /// <value>
    /// The gradient mode.
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LinearGradientMode GradientMode { get => _values.GradientMode; set => _values.GradientMode = value; }

    /// <summary>
    /// Gets or sets the text glow spread.
    /// </summary>
    /// <value>
    /// The text glow spread.
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int TextGlowSpread { get => _values.TextGlowSpread; set => _values.TextGlowSpread = value; }

    /// <summary>
    /// Gets or sets the alert blink interval.
    /// </summary>
    /// <value>
    /// The alert blink interval.
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int AlertBlinkInterval { get => _values.AlertBlinkInterval; set => _values.AlertBlinkInterval = value; }

    /// <summary>
    /// Gets or sets the fade interval.
    /// </summary>
    /// <value>
    /// The fade interval.
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int FadeInterval { get => _values.FadeInterval; set => _values.FadeInterval = value; }

    /// <summary>
    /// Gets or sets the duration of the blink.
    /// </summary>
    /// <value>
    /// The duration of the blink.
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public long BlinkDuration { get => _values.BlinkDuration; set => _values.BlinkDuration = value; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public BlinkState BlinkState { get => _values.BlinkState; set => _values.BlinkState = value; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public short CycleInterval { get => _values.CycleInterval; set => _values.CycleInterval = value; }
    #endregion

    #region Constructors
    public KryptonToolStripLabelExtended()
    {
        _values = new ToolStripLabelExtendedValues(this);
    }
    #endregion

    #region Overrides        
    /// <summary>
    /// Gets or sets the background color for the item.
    /// </summary>
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public override Color BackColor { get => base.BackColor; set => base.BackColor = value; }

    protected override void OnPaint(PaintEventArgs e)
    {
        // Set a graphics variable
        Graphics g = e.Graphics;

        // Rectangle variable
        Rectangle r = new Rectangle(0, 0, Width, Height);

        if (BackColor != Color.Empty)
        {
            // Fill the background with a solid colour
            using (SolidBrush solidBrush = new SolidBrush(BackColor))
            {
                g.FillRectangle(solidBrush, r);
            }
        }
        else if (GradientColourOne != Color.Empty || GradientColourTwo != Color.Empty)
        {
            // Fill the background with a gradient colour
            using (LinearGradientBrush lgb = new LinearGradientBrush(r, GradientColourOne, GradientColourTwo, GradientMode))
            {
                g.FillRectangle(lgb, r);
            }
        }

        // Text rendering
        if (ForeColor != Color.Empty)
        {
            g.TextRenderingHint = TextRenderingHint.AntiAlias | TextRenderingHint.ClearTypeGridFit;

            // Preserve user font settings
            Font typeface = new Font(Font.FontFamily, Font.Size, Font.Style, Font.Unit);

            SolidBrush brush = new SolidBrush(ForeColor);

            // Draw the text
            g.DrawString(Text, typeface, brush, 0, 0);
        }

        if (EnableBlinking)
        {
            switch (BlinkState)
            {
                case BlinkState.NormalBlink:
                    BlinkLabel(BlinkDuration);
                    break;
                case BlinkState.SoftBlink:
                    SoftBlink(AlertColourOne, AlertColourTwo, AlertTextColour, CycleInterval, BkClr, BlinkDuration);
                    break;
            }
        }

        if (EnableFadeAnimation)
        {
            _fadeAnimationTimer = new Timer();

            _fadeAnimationTimer.Interval = FadeInterval;

            _fadeAnimationTimer.Enabled = true;

            _fadeAnimationTimer.Tick += FadeAnimationTimer_Tick;
        }
    }
    #endregion

    #region Events
    private void FadeAnimationTimer_Tick(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Methods
    public void SetTextColour(Color textColour)
    {
        if (_palette != null)
        {

        }
    }

    /// <summary>
    /// Blinks the label.
    /// </summary>
    /// <param name="blinkDuration">Duration of the blink.</param>
    public async void BlinkLabel(long blinkDuration)
    {
        var sw = Stopwatch.StartNew();

        var fgc = ForeColor;

        var bgc = BackColor;

        while (sw.ElapsedMilliseconds < blinkDuration)
        {
            await Task.Delay(_values.AlertBlinkInterval);

            base.BackColor = base.BackColor == AlertColourOne ? AlertColourTwo : AlertColourOne;

            base.ForeColor = AlertTextColour;

            Invalidate();
        }

        BackColor = bgc;

        ForeColor = fgc;

        Invalidate();

        sw.Stop();
    }

    /// <summary>
    /// Softs the blink.
    /// </summary>
    /// <param name="alertColour1">The alert colour1.</param>
    /// <param name="alertColour2">The alert colour2.</param>
    /// <param name="alertTextColour">The alert text colour.</param>
    /// <param name="cycleInterval">The cycle interval.</param>
    /// <param name="bkClr">if set to <c>true</c> [bk color].</param>
    /// <param name="blinkDuration">Duration of the blink.</param>
    public async void SoftBlink(Color alertColour1, Color alertColour2, Color alertTextColour, short cycleInterval, bool bkClr, long blinkDuration)
    {
        var sw = Stopwatch.StartNew();

        var fgc = ForeColor;

        var bgc = BackColor;

        short halfCycle = (short)Math.Round(cycleInterval * 0.5);

        while (sw.ElapsedMilliseconds < blinkDuration)
        {
            await Task.Delay(1);

            var n = sw.ElapsedMilliseconds % cycleInterval;

            var per = (double)Math.Abs(n - halfCycle) / halfCycle;

            var red = (short)Math.Round((alertColour2.R - alertColour1.R) * per) + alertColour1.R;

            var grn = (short)Math.Round((alertColour2.G - alertColour1.G) * per) + alertColour1.G;

            var blw = (short)Math.Round((alertColour2.B - alertColour1.B) * per) + alertColour1.B;

            var clr = Color.FromArgb(red, grn, blw);

            if (bkClr)
            {
                base.BackColor = clr;
            }
            else
            {
                base.ForeColor = clr;
            }

            Invalidate();
        }

        BackColor = bgc;

        ForeColor = fgc;

        Invalidate();

        sw.Stop();
    }
    #endregion
}
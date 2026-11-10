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

public class KryptonToolStripMarqueeMenuItem : KryptonEnhancedToolStripMenuItem
{
    #region Constants

    const int DEFAULT_REFRESH_INTERVAL = 30;
    #endregion

    #region Variables

    private readonly MarqueeMenuItemValues _values;

    // Marquee text.
    string _text = string.Empty;

    //Every time new text is assigned, text is measured ans stored here
    Size _textSize;

    //Timer used to repaint scrolled text
    Timer _timer;

    //Value modified in Timer tick event. Used to represent ever changing text offset.
    int _pixelOffest;
    #endregion

    #region Properties

    /// <summary>
    /// Gets the expandable marquee scrolling configuration values for designer and runtime use.
    /// </summary>
    [Category("Behavior")]
    [Description("Marquee scrolling direction, speed, and text width settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public MarqueeMenuItemValues MarqueeValues => _values;

    private bool ShouldSerializeMarqueeValues() => !_values.IsDefault;

    private void ResetMarqueeValues() => _values.Reset();

    /// <summary>
    /// Gets or sets the timer interval driving the scroll animation.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal int TimerInterval { get => _timer.Interval; set => _timer.Interval = value; }

    /// <summary>
    /// Determines if text is scrolled left-to-right or right-to-left.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public MarqueeScrollDirection MarqueeScrollDirection { get => _values.MarqueeScrollDirection; set => _values.MarqueeScrollDirection = value; }

    /// <summary>
    /// Value less or equal zero indicates that text area width is defined by with of Text string.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int MinimumTextWidth { get => _values.MinimumTextWidth; set => _values.MinimumTextWidth = value; }

    /// <summary>
    /// Determines how often new text position is recalculated. 
    /// Together with 'ScrollStep' property defines speed of moving text.
    /// Smaller value means faster moving text.
    /// Text scrolling speed in pixels per seconds can be expressed with the following formula:
    /// <br></br>
    /// 1000 * ScrolStep/RefreshInterval
    /// </summary>
    /// <remarks>
    /// On most computers fastest scrolling speed is achieved for property value around 20 milliseconds.
    /// Values smaller than 20 will not increase speed. If faster scrolling is needed,
    /// it can be achieved by increasing value of 'ScrollStep' property.
    /// </remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int RefreshInterval { get => _values.RefreshInterval; set => _values.RefreshInterval = value; }

    /// <summary>
    /// Determines how many pixels text shifts when position is recalculated. 
    /// Together with 'RefreshInterval' property defines speed of moving text.
    /// Bigger value means faster moving text.
    /// Text scrolling speed in pixels per seconds can be expressed with the following formula:
    /// <br></br>
    /// 1000 * ScrollStep/RefreshInterval
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int ScrollStep { get => _values.ScrollStep; set => _values.ScrollStep = value; }

    /// <summary>
    /// When sets to 'true', every time mouse pointer moves over tool strip item, scrolling stops.
    /// Otherwise, scrolling never stops.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool StopScrollOnMouseOver { get => _values.StopScrollOnMouseOver; set => _values.StopScrollOnMouseOver = value; }

    /// <summary>
    /// Sets or gets text value of menu item text.
    /// </summary>
    [Browsable(true)]
    [DefaultValue("")]
    [Description("Sets or gets text value of menu item text.")]
    public new string Text
    {
        get => _text;
        set
        {
            _text = value;

            MeasureText();

            //Assign only spaces text to the base;
        }
    }
    #endregion

    #region Constructor

    /// <summary>
    /// Constructor. Instantiates Timer used to tick scrolling events and initializes default values.
    /// </summary>
    public KryptonToolStripMarqueeMenuItem()
    {
        _values = new MarqueeMenuItemValues(this);

        _timer = new Timer();
        _timer.Interval = DEFAULT_REFRESH_INTERVAL;
        _timer.Tick += timer_Tick;
        _timer.Enabled = true;

        if (MarqueeScrollDirection == MarqueeScrollDirection.RightToLeft)
        {
            _pixelOffest = -_textSize.Width;
        }
        else
        {
            _pixelOffest = _textSize.Width;
        }

        DisplayStyle = CheckMarkDisplayStyle.CheckBox;
        CheckOnClick = false;
        CheckState = CheckState.Unchecked;
    }

    #endregion

    #region Timer event

    /// <summary>
    /// Recalculate new text position and and calls Invalidate to repaint.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void timer_Tick(object? sender, EventArgs e)
    {
        //Change offset only when menu item is visible, mouse is not hovering over or StopScrollOnMouseOver is not set to 'false'
        if (Visible && (!Selected || !StopScrollOnMouseOver))
        {
            _pixelOffest = (_pixelOffest + ScrollStep + _textSize.Width) % (2 * _textSize.Width + 1) - _textSize.Width;
            Invalidate();
        }
    }

    #endregion

    #region Private helper methods

    /// <summary>
    /// Every time Text or Font properties change this method is called to recalculate
    /// commonly used text metrics.
    /// </summary>
    internal void MeasureText()
    {
        _textSize = TextRenderer.MeasureText(_text, Font);

        //Calculate size of masked text passed to the base class. Base class doesn't know
        //real value of Text property. It  uses only white spaced string with length
        //equal to length of Text.
        StringBuilder allSpacesString = new StringBuilder(" ");

        int maxWidth = MinimumTextWidth > 0 ? MinimumTextWidth : _textSize.Width;

        while (TextRenderer.MeasureText(allSpacesString.ToString(), Font).Width < maxWidth)
            allSpacesString.Append(' ');

        base.Text = allSpacesString.ToString();

    }

    #endregion

    #region Overrides
    protected override void Dispose(bool disposing)
    {
        _timer.Enabled = false;

        _timer.Dispose();

        base.Dispose(disposing);
    }

    protected override void OnFontChanged(EventArgs e)
    {
        MeasureText();

        base.OnFontChanged(e);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        //Paint scrolling text
        ToolStrip? parent = GetCurrentParent();
        if (parent == null)
        {
            return;
        }

        Rectangle displayRect = parent.DisplayRectangle;
        int horizPadding = parent.Padding.Horizontal;

        Rectangle clipRectangle = new Rectangle(displayRect.X, displayRect.Y, displayRect.Width - horizPadding, displayRect.Height);

        e.Graphics.FillRectangle(Brushes.Transparent, e.ClipRectangle);

        int textYPosition = (Size.Height - _textSize.Height) / 2;

        Region savedClip = e.Graphics.Clip;
        Region clipRegion = new Region(clipRectangle);
        e.Graphics.Clip = clipRegion;
        if (MarqueeScrollDirection == MarqueeScrollDirection.RightToLeft)
        {
            e.Graphics.DrawString(_text, Font, Brushes.Black, -_pixelOffest + horizPadding, textYPosition);
        }
        else
        {
            e.Graphics.DrawString(_text, Font, Brushes.Black, +_pixelOffest + horizPadding, textYPosition);
        }

        clipRegion.Dispose();
        e.Graphics.Clip = savedClip;
    }
    #endregion
}
#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// A custom scrollbar control.
    /// </summary>
    [Designer(typeof(ScrollBarControlDesigner))]
    [DefaultEvent("Scroll")]
    [DefaultProperty("Value")]
    [ToolboxBitmap(typeof(VScrollBar))]
    public class KryptonScrollBar : Control
    {
        #region fields

        /// <summary>
        /// Redraw const.
        /// </summary>
        private const int SETREDRAW = 11;

        /// <summary>
        /// Indicates many changes to the scrollbar are happening, so stop painting till finished.
        /// </summary>
        private bool inUpdate;

        /// <summary>
        /// The scrollbar orientation - horizontal / VERTICAL.
        /// </summary>
        private ScrollBarOrientation orientation = ScrollBarOrientation.VERTICAL;

        /// <summary>
        /// The scroll orientation in scroll events.
        /// </summary>
        private ScrollOrientation scrollOrientation = ScrollOrientation.VerticalScroll;

        /// <summary>
        /// The clicked channel rectangle.
        /// </summary>
        private Rectangle clickedBarRectangle;

        /// <summary>
        /// The thumb rectangle.
        /// </summary>
        private Rectangle thumbRectangle;

        /// <summary>
        /// The top arrow rectangle.
        /// </summary>
        private Rectangle topArrowRectangle;

        /// <summary>
        /// The bottom arrow rectangle.
        /// </summary>
        private Rectangle bottomArrowRectangle;

        /// <summary>
        /// The channel rectangle.
        /// </summary>
        private Rectangle channelRectangle;

        /// <summary>
        /// Indicates if top arrow was clicked.
        /// </summary>
        private bool topArrowClicked;

        /// <summary>
        /// Indicates if bottom arrow was clicked.
        /// </summary>
        private bool bottomArrowClicked;

        /// <summary>
        /// Indicates if channel rectangle above the thumb was clicked.
        /// </summary>
        private bool topBarClicked;

        /// <summary>
        /// Indicates if channel rectangle under the thumb was clicked.
        /// </summary>
        private bool bottomBarClicked;

        /// <summary>
        /// Indicates if the thumb was clicked.
        /// </summary>
        private bool thumbClicked;

        /// <summary>
        /// The state of the thumb.
        /// </summary>
        private ScrollBarState thumbState = ScrollBarState.NORMAL;

        /// <summary>
        /// The state of the top arrow.
        /// </summary>
        private ScrollBarArrowButtonState topButtonState = ScrollBarArrowButtonState.UPNORMAL;

        /// <summary>
        /// The state of the bottom arrow.
        /// </summary>
        private ScrollBarArrowButtonState bottomButtonState = ScrollBarArrowButtonState.DOWNNORMAL;

        /// <summary>
        /// The scrollbar value minimum.
        /// </summary>
        private int minimum;

        /// <summary>
        /// The scrollbar value maximum.
        /// </summary>
        private int maximum = 100;

        /// <summary>
        /// The small change value.
        /// </summary>
        private int smallChange = 1;

        /// <summary>
        /// The large change value.
        /// </summary>
        private int largeChange = 10;

        /// <summary>
        /// The value of the scrollbar.
        /// </summary>
        private int value;

        /// <summary>
        /// The width of the thumb.
        /// </summary>
        private int thumbWidth = 15;

        /// <summary>
        /// The height of the thumb.
        /// </summary>
        private int thumbHeight;

        /// <summary>
        /// The width of an arrow.
        /// </summary>
        private int arrowWidth = 15;

        /// <summary>
        /// The height of an arrow.
        /// </summary>
        private int arrowHeight = 17;

        /// <summary>
        /// The bottom limit for the thumb bottom.
        /// </summary>
        private int thumbBottomLimitBottom;

        /// <summary>
        /// The bottom limit for the thumb top.
        /// </summary>
        private int thumbBottomLimitTop;

        /// <summary>
        /// The top limit for the thumb top.
        /// </summary>
        private int thumbTopLimit;

        /// <summary>
        /// The current position of the thumb.
        /// </summary>
        private int thumbPosition;

        /// <summary>
        /// The track position.
        /// </summary>
        private int trackPosition;

        /// <summary>
        /// The progress timer for moving the thumb.
        /// </summary>
        private System.Windows.Forms.Timer progressTimer = new System.Windows.Forms.Timer();

        /// <summary>
        /// The border color.
        /// </summary>
        private Color borderColor = Color.FromArgb(93, 140, 201);

        /// <summary>
        /// The border color in disabled state.
        /// </summary>
        private Color disabledBorderColor = Color.Gray;

        #region context menu items

        /// <summary>
        /// Context menu strip.
        /// </summary>
        private ContextMenuStrip contextMenu;

        /// <summary>
        /// Container for components.
        /// </summary>
        private IContainer components;

        /// <summary>
        /// Menu item.
        /// </summary>
        private ToolStripMenuItem tsmiScrollHere;

        /// <summary>
        /// Menu separator.
        /// </summary>
        private ToolStripSeparator toolStripSeparator1;

        /// <summary>
        /// Menu item.
        /// </summary>
        private ToolStripMenuItem tsmiTop;

        /// <summary>
        /// Menu item.
        /// </summary>
        private ToolStripMenuItem tsmiBottom;

        /// <summary>
        /// Menu separator.
        /// </summary>
        private ToolStripSeparator toolStripSeparator2;

        /// <summary>
        /// Menu item.
        /// </summary>
        private ToolStripMenuItem tsmiLargeUp;

        /// <summary>
        /// Menu item.
        /// </summary>
        private ToolStripMenuItem tsmiLargeDown;

        /// <summary>
        /// Menu separator.
        /// </summary>
        private ToolStripSeparator toolStripSeparator3;

        /// <summary>
        /// Menu item.
        /// </summary>
        private ToolStripMenuItem tsmiSmallUp;

        /// <summary>
        /// Menu item.
        /// </summary>
        private ToolStripMenuItem tsmiSmallDown;

        #endregion

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="KryptonScrollBar"/> class.
        /// </summary>
        public KryptonScrollBar()
        {
            // sets the control styles of the control
            SetStyle(
                  ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw
                  | ControlStyles.Selectable | ControlStyles.AllPaintingInWmPaint
                  | ControlStyles.UserPaint, true);

            // initializes the context menu
            InitializeComponent();

            Width = 19;
            Height = 200;

            // sets the scrollbar up
            SetUpScrollBar();

            // timer for clicking and holding the mouse button
            // over/below the thumb and on the arrow buttons
            progressTimer.Interval = 20;
            progressTimer.Tick += ProgressTimerTick;

            // no image margin in context menu
            contextMenu.ShowImageMargin = false;
            ContextMenuStrip = contextMenu;
        }

        #endregion

        #region events
        /// <summary>
        /// Occurs when the scrollbar scrolled.
        /// </summary>
        [Category("Behavior")]
        [Description("Is raised, when the scrollbar was scrolled.")]
        public event ScrollEventHandler Scroll;
        #endregion

        #region properties

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        [Category("Layout")]
        [Description("Gets or sets the orientation.")]
        [DefaultValue(ScrollBarOrientation.VERTICAL)]
        public ScrollBarOrientation Orientation
        {
            get
            {
                return orientation;
            }

            set
            {
                // no change - return
                if (value == orientation)
                {
                    return;
                }

                orientation = value;

                // change text of context menu entries
                ChangeContextMenuItems();

                // save scroll orientation for scroll event
                scrollOrientation = value == ScrollBarOrientation.VERTICAL ?
                   ScrollOrientation.VerticalScroll : ScrollOrientation.HorizontalScroll;

                // only in DesignMode switch width and height
                if (DesignMode)
                {
                    Size = new Size(Height, Width);
                }

                // sets the scrollbar up
                SetUpScrollBar();
            }
        }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        [Category("Behavior")]
        [Description("Gets or sets the minimum value.")]
        [DefaultValue(0)]
        public int Minimum
        {
            get
            {
                return minimum;
            }

            set
            {
                // no change or value invalid - return
                if (minimum == value || value < 0 || value >= maximum)
                {
                    return;
                }

                minimum = value;

                // current value less than new minimum value - adjust
                if (value < value)
                {
                    value = value;
                }

                // is current large change value invalid - adjust
                if (largeChange > maximum - minimum)
                {
                    largeChange = maximum - minimum;
                }

                SetUpScrollBar();

                // current value less than new minimum value - adjust
                if (value < value)
                {
                    Value = value;
                }
                else
                {
                    // current value is valid - adjust thumb position
                    ChangeThumbPosition(GetThumbPosition());

                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        [Category("Behavior")]
        [Description("Gets or sets the maximum value.")]
        [DefaultValue(100)]
        public int Maximum
        {
            get
            {
                return maximum;
            }

            set
            {
                // no change or new max. value invalid - return
                if (value == maximum || value < 1 || value <= minimum)
                {
                    return;
                }

                maximum = value;

                // is large change value invalid - adjust
                if (largeChange > maximum - minimum)
                {
                    largeChange = maximum - minimum;
                }

                SetUpScrollBar();

                // is current value greater than new maximum value - adjust
                if (value > value)
                {
                    Value = maximum;
                }
                else
                {
                    // current value is valid - adjust thumb position
                    ChangeThumbPosition(GetThumbPosition());

                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the small change amount.
        /// </summary>
        [Category("Behavior")]
        [Description("Gets or sets the small change value.")]
        [DefaultValue(1)]
        public int SmallChange
        {
            get
            {
                return smallChange;
            }

            set
            {
                // no change or new small change value invalid - return
                if (value == smallChange || value < 1 || value >= largeChange)
                {
                    return;
                }

                smallChange = value;

                SetUpScrollBar();
            }
        }

        /// <summary>
        /// Gets or sets the large change amount.
        /// </summary>
        [Category("Behavior")]
        [Description("Gets or sets the large change value.")]
        [DefaultValue(10)]
        public int LargeChange
        {
            get
            {
                return largeChange;
            }

            set
            {
                // no change or new large change value is invalid - return
                if (value == largeChange || value < smallChange || value < 2)
                {
                    return;
                }

                // if value is greater than scroll area - adjust
                if (value > maximum - minimum)
                {
                    largeChange = maximum - minimum;
                }
                else
                {
                    // set new value
                    largeChange = value;
                }

                SetUpScrollBar();
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [Category("Behavior")]
        [Description("Gets or sets the current value.")]
        [DefaultValue(0)]
        public int Value
        {
            get
            {
                return value;
            }

            set
            {
                // no change or invalid value - return
                if (value == value || value < minimum || value > maximum)
                {
                    return;
                }

                value = value;

                // adjust thumb position
                ChangeThumbPosition(GetThumbPosition());

                // raise scroll event
                OnScroll(new ScrollEventArgs(ScrollEventType.ThumbPosition, -1, value, scrollOrientation));

                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the border color.
        /// </summary>
        [Category("Appearance")]
        [Description("Gets or sets the border color.")]
        [DefaultValue(typeof(Color), "93, 140, 201")]
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }

            set
            {
                borderColor = value;

                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the border color in disabled state.
        /// </summary>
        [Category("Appearance")]
        [Description("Gets or sets the border color in disabled state.")]
        [DefaultValue(typeof(Color), "Gray")]
        public Color DisabledBorderColor
        {
            get
            {
                return disabledBorderColor;
            }

            set
            {
                disabledBorderColor = value;

                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the opacity of the context menu (from 0 - 1).
        /// </summary>
        [Category("Appearance")]
        [Description("Gets or sets the opacity of the context menu (from 0 - 1).")]
        [DefaultValue(typeof(double), "1")]
        public double Opacity
        {
            get
            {
                return contextMenu.Opacity;
            }

            set
            {
                // no change - return
                if (value == contextMenu.Opacity)
                {
                    return;
                }

                contextMenu.AllowTransparency = value != 1;

                contextMenu.Opacity = value;
            }
        }

        #endregion

        #region methods

        #region public methods

        /// <summary>
        /// Prevents the drawing of the control until <see cref="EndUpdate"/> is called.
        /// </summary>
        public void BeginUpdate()
        {
            SendMessage(Handle, SETREDRAW, false, 0);
            inUpdate = true;
        }

        /// <summary>
        /// Ends the updating process and the control can draw itself again.
        /// </summary>
        public void EndUpdate()
        {
            SendMessage(Handle, SETREDRAW, true, 0);
            inUpdate = false;
            SetUpScrollBar();
            Refresh();
        }

        #endregion

        #region protected methods

        /// <summary>
        /// Raises the <see cref="Scroll"/> event.
        /// </summary>
        /// <param name="e">The <see cref="ScrollEventArgs"/> that contains the event data.</param>
        protected virtual void OnScroll(ScrollEventArgs e)
        {
            // if event handler is attached - raise scroll event
            if (Scroll != null)
            {
                Scroll(this, e);
            }
        }

        /// <summary>
        /// Paints the background of the control.
        /// </summary>
        /// <param name="e">A <see cref="PaintEventArgs"/> that contains information about the control to paint.</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // no painting here
        }

        /// <summary>
        /// Paints the control.
        /// </summary>
        /// <param name="e">A <see cref="PaintEventArgs"/> that contains information about the control to paint.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // sets the smoothing mode to none
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            // save client rectangle
            Rectangle rect = ClientRectangle;

            // adjust the rectangle
            if (orientation == ScrollBarOrientation.VERTICAL)
            {
                rect.X++;
                rect.Y += arrowHeight + 1;
                rect.Width -= 2;
                rect.Height -= (arrowHeight * 2) + 2;
            }
            else
            {
                rect.X += arrowWidth + 1;
                rect.Y++;
                rect.Width -= (arrowWidth * 2) + 2;
                rect.Height -= 2;
            }

            ScrollBarKryptonRenderer.InitColours();

            // draws the background
            ScrollBarKryptonRenderer.DrawBackground(
               e.Graphics,
               ClientRectangle,
               orientation);

            // draws the track
            ScrollBarKryptonRenderer.DrawTrack(
               e.Graphics,
               rect,
               ScrollBarState.NORMAL,
               orientation);

            // draw thumb and grip
            ScrollBarKryptonRenderer.DrawThumb(
               e.Graphics,
               thumbRectangle,
               thumbState,
               orientation);

            if (Enabled)
            {
                ScrollBarKryptonRenderer.DrawThumbGrip(
                   e.Graphics,
                   thumbRectangle,
                   orientation);
            }

            // draw arrows
            ScrollBarKryptonRenderer.DrawArrowButton(
               e.Graphics,
               topArrowRectangle,
               topButtonState,
               true,
               orientation);

            ScrollBarKryptonRenderer.DrawArrowButton(
               e.Graphics,
               bottomArrowRectangle,
               bottomButtonState,
               false,
               orientation);

            // check if top or bottom bar was clicked
            if (topBarClicked)
            {
                if (orientation == ScrollBarOrientation.VERTICAL)
                {
                    clickedBarRectangle.Y = thumbTopLimit;
                    clickedBarRectangle.Height =
                       thumbRectangle.Y - thumbTopLimit;
                }
                else
                {
                    clickedBarRectangle.X = thumbTopLimit;
                    clickedBarRectangle.Width =
                       thumbRectangle.X - thumbTopLimit;
                }

                ScrollBarKryptonRenderer.DrawTrack(
                   e.Graphics,
                   clickedBarRectangle,
                   ScrollBarState.PRESSED,
                   orientation);
            }
            else if (bottomBarClicked)
            {
                if (orientation == ScrollBarOrientation.VERTICAL)
                {
                    clickedBarRectangle.Y = thumbRectangle.Bottom + 1;
                    clickedBarRectangle.Height =
                       thumbBottomLimitBottom - clickedBarRectangle.Y + 1;
                }
                else
                {
                    clickedBarRectangle.X = thumbRectangle.Right + 1;
                    clickedBarRectangle.Width =
                       thumbBottomLimitBottom - clickedBarRectangle.X + 1;
                }

                ScrollBarKryptonRenderer.DrawTrack(
                   e.Graphics,
                   clickedBarRectangle,
                   ScrollBarState.PRESSED,
                   orientation);
            }

            // draw border
            using (Pen pen = new Pen(
               (Enabled ? ScrollBarKryptonRenderer.borderColours[0] : disabledBorderColor)))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
            }
        }

        /// <summary>
        /// Raises the MouseDown event.
        /// </summary>
        /// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            Focus();

            if (e.Button == MouseButtons.Left)
            {
                // prevents showing the context menu if pressing the right mouse
                // button while holding the left
                ContextMenuStrip = null;

                Point mouseLocation = e.Location;

                if (thumbRectangle.Contains(mouseLocation))
                {
                    thumbClicked = true;
                    thumbPosition = orientation == ScrollBarOrientation.VERTICAL ? mouseLocation.Y - thumbRectangle.Y : mouseLocation.X - thumbRectangle.X;
                    thumbState = ScrollBarState.PRESSED;

                    Invalidate(thumbRectangle);
                }
                else if (topArrowRectangle.Contains(mouseLocation))
                {
                    topArrowClicked = true;
                    topButtonState = ScrollBarArrowButtonState.UPPRESSED;

                    Invalidate(topArrowRectangle);

                    ProgressThumb(true);
                }
                else if (bottomArrowRectangle.Contains(mouseLocation))
                {
                    bottomArrowClicked = true;
                    bottomButtonState = ScrollBarArrowButtonState.DOWNPRESSED;

                    Invalidate(bottomArrowRectangle);

                    ProgressThumb(true);
                }
                else
                {
                    trackPosition =
                       orientation == ScrollBarOrientation.VERTICAL ?
                          mouseLocation.Y : mouseLocation.X;

                    if (trackPosition <
                       (orientation == ScrollBarOrientation.VERTICAL ?
                          thumbRectangle.Y : thumbRectangle.X))
                    {
                        topBarClicked = true;
                    }
                    else
                    {
                        bottomBarClicked = true;
                    }

                    ProgressThumb(true);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                trackPosition =
                   orientation == ScrollBarOrientation.VERTICAL ? e.Y : e.X;
            }
        }

        /// <summary>
        /// Raises the MouseUp event.
        /// </summary>
        /// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Left)
            {
                ContextMenuStrip = contextMenu;

                if (thumbClicked)
                {
                    thumbClicked = false;
                    thumbState = ScrollBarState.NORMAL;

                    OnScroll(new ScrollEventArgs(
                       ScrollEventType.EndScroll,
                       -1,
                       value,
                       scrollOrientation)
                    );
                }
                else if (topArrowClicked)
                {
                    topArrowClicked = false;
                    topButtonState = ScrollBarArrowButtonState.UPNORMAL;
                    StopTimer();
                }
                else if (bottomArrowClicked)
                {
                    bottomArrowClicked = false;
                    bottomButtonState = ScrollBarArrowButtonState.DOWNNORMAL;
                    StopTimer();
                }
                else if (topBarClicked)
                {
                    topBarClicked = false;
                    StopTimer();
                }
                else if (bottomBarClicked)
                {
                    bottomBarClicked = false;
                    StopTimer();
                }

                Invalidate();
            }
        }

        /// <summary>
        /// Raises the MouseEnter event.
        /// </summary>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            bottomButtonState = ScrollBarArrowButtonState.DOWNACTIVE;
            topButtonState = ScrollBarArrowButtonState.UPACTIVE;
            thumbState = ScrollBarState.ACTIVE;

            Invalidate();
        }

        /// <summary>
        /// Raises the MouseLeave event.
        /// </summary>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            ResetScrollStatus();
        }

        /// <summary>
        /// Raises the MouseMove event.
        /// </summary>
        /// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // moving and holding the left mouse button
            if (e.Button == MouseButtons.Left)
            {
                // Update the thumb position, if the new location is within the bounds.
                if (thumbClicked)
                {
                    int oldScrollValue = value;

                    topButtonState = ScrollBarArrowButtonState.UPACTIVE;
                    bottomButtonState = ScrollBarArrowButtonState.DOWNACTIVE;
                    int pos = orientation == ScrollBarOrientation.VERTICAL ?
                       e.Location.Y : e.Location.X;

                    // The thumb is all the way to the top
                    if (pos <= (thumbTopLimit + thumbPosition))
                    {
                        ChangeThumbPosition(thumbTopLimit);

                        value = minimum;
                    }
                    else if (pos >= (thumbBottomLimitTop + thumbPosition))
                    {
                        // The thumb is all the way to the bottom
                        ChangeThumbPosition(thumbBottomLimitTop);

                        value = maximum;
                    }
                    else
                    {
                        // The thumb is between the ends of the track.
                        ChangeThumbPosition(pos - thumbPosition);

                        int pixelRange, thumbPos, arrowSize;

                        // calculate the value - first some helper variables
                        // dependent on the current orientation
                        if (orientation == ScrollBarOrientation.VERTICAL)
                        {
                            pixelRange = Height - (2 * arrowHeight) - thumbHeight;
                            thumbPos = thumbRectangle.Y;
                            arrowSize = arrowHeight;
                        }
                        else
                        {
                            pixelRange = Width - (2 * arrowWidth) - thumbWidth;
                            thumbPos = thumbRectangle.X;
                            arrowSize = arrowWidth;
                        }

                        float perc = 0f;

                        if (pixelRange != 0)
                        {
                            // percent of the new position
                            perc = (float)(thumbPos - arrowSize) / (float)pixelRange;
                        }

                        // the new value is somewhere between max and min, starting
                        // at min position
                        value = Convert.ToInt32((perc * (maximum - minimum)) + minimum);
                    }

                    // raise scroll event if new value different
                    if (oldScrollValue != value)
                    {
                        OnScroll(new ScrollEventArgs(ScrollEventType.ThumbTrack, oldScrollValue, value, scrollOrientation));

                        Refresh();
                    }
                }
            }
            else if (!ClientRectangle.Contains(e.Location))
            {
                ResetScrollStatus();
            }
            else if (e.Button == MouseButtons.None) // only moving the mouse
            {
                if (topArrowRectangle.Contains(e.Location))
                {
                    topButtonState = ScrollBarArrowButtonState.UPHOT;

                    Invalidate(topArrowRectangle);
                }
                else if (bottomArrowRectangle.Contains(e.Location))
                {
                    bottomButtonState = ScrollBarArrowButtonState.DOWNHOT;

                    Invalidate(bottomArrowRectangle);
                }
                else if (thumbRectangle.Contains(e.Location))
                {
                    thumbState = ScrollBarState.HOT;

                    Invalidate(thumbRectangle);
                }
                else if (ClientRectangle.Contains(e.Location))
                {
                    topButtonState = ScrollBarArrowButtonState.UPACTIVE;
                    bottomButtonState = ScrollBarArrowButtonState.DOWNACTIVE;
                    thumbState = ScrollBarState.ACTIVE;

                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Performs the work of setting the specified bounds of this control.
        /// </summary>
        /// <param name="x">The new x value of the control.</param>
        /// <param name="y">The new y value of the control.</param>
        /// <param name="width">The new width value of the control.</param>
        /// <param name="height">The new height value of the control.</param>
        /// <param name="specified">A bitwise combination of the <see cref="BoundsSpecified"/> values.</param>
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            // only in design mode - constrain size
            if (DesignMode)
            {
                if (orientation == ScrollBarOrientation.VERTICAL)
                {
                    if (height < (2 * arrowHeight) + 10)
                    {
                        height = (2 * arrowHeight) + 10;
                    }

                    width = 19;
                }
                else
                {
                    if (width < (2 * arrowWidth) + 10)
                    {
                        width = (2 * arrowWidth) + 10;
                    }

                    height = 19;
                }
            }

            base.SetBoundsCore(x, y, width, height, specified);

            if (DesignMode)
            {
                SetUpScrollBar();
            }
        }

        /// <summary>
        /// Raises the <see cref="System.Windows.Forms.Control.SizeChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetUpScrollBar();
        }

        /// <summary>
        /// Processes a dialog key.
        /// </summary>
        /// <param name="keyData">One of the <see cref="System.Windows.Forms.Keys"/> values that represents the key to process.</param>
        /// <returns>true, if the key was processed by the control, false otherwise.</returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            // key handling is here - keys recognized by the control
            // Up&Down or Left&Right, PageUp, PageDown, Home, End
            Keys keyUp = Keys.Up;
            Keys keyDown = Keys.Down;

            if (orientation == ScrollBarOrientation.HORIZONTAL)
            {
                keyUp = Keys.Left;
                keyDown = Keys.Right;
            }

            if (keyData == keyUp)
            {
                Value -= smallChange;

                return true;
            }

            if (keyData == keyDown)
            {
                Value += smallChange;

                return true;
            }

            if (keyData == Keys.PageUp)
            {
                Value = GetValue(false, true);

                return true;
            }

            if (keyData == Keys.PageDown)
            {
                if (value + largeChange > maximum)
                {
                    Value = maximum;
                }
                else
                {
                    Value += largeChange;
                }

                return true;
            }

            if (keyData == Keys.Home)
            {
                Value = minimum;

                return true;
            }

            if (keyData == Keys.End)
            {
                Value = maximum;

                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// Raises the <see cref="System.Windows.Forms.Control.EnabledChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);

            if (Enabled)
            {
                thumbState = ScrollBarState.NORMAL;
                topButtonState = ScrollBarArrowButtonState.UPNORMAL;
                bottomButtonState = ScrollBarArrowButtonState.DOWNNORMAL;
            }
            else
            {
                thumbState = ScrollBarState.DISABLED;
                topButtonState = ScrollBarArrowButtonState.UPDISABLED;
                bottomButtonState = ScrollBarArrowButtonState.DOWNDISABLED;
            }

            Refresh();
        }

        #endregion

        #region misc methods

        /// <summary>
        /// Sends a message.
        /// </summary>
        /// <param name="wnd">The handle of the control.</param>
        /// <param name="msg">The message as int.</param>
        /// <param name="param">param - true or false.</param>
        /// <param name="lparam">Additional parameter.</param>
        /// <returns>0 or error code.</returns>
        /// <remarks>Needed for sending the stop/start drawing of the control.</remarks>
        [DllImport("user32.dll")]
        private static extern int SendMessage(
           IntPtr wnd,
           int msg,
           bool param,
           int lparam);

        /// <summary>
        /// Sets up the scrollbar.
        /// </summary>
        private void SetUpScrollBar()
        {
            // if no drawing - return
            if (inUpdate)
            {
                return;
            }

            // set up the width's, height's and rectangles for the different
            // elements
            if (orientation == ScrollBarOrientation.VERTICAL)
            {
                arrowHeight = 17;
                arrowWidth = 15;
                thumbWidth = 15;
                thumbHeight = GetThumbSize();

                clickedBarRectangle = ClientRectangle;
                clickedBarRectangle.Inflate(-1, -1);
                clickedBarRectangle.Y += arrowHeight;
                clickedBarRectangle.Height -= arrowHeight * 2;

                channelRectangle = clickedBarRectangle;

                thumbRectangle = new Rectangle(
                   ClientRectangle.X + 2,
                   ClientRectangle.Y + arrowHeight + 1,
                   thumbWidth - 1,
                   thumbHeight
                );

                topArrowRectangle = new Rectangle(
                   ClientRectangle.X + 2,
                   ClientRectangle.Y + 1,
                   arrowWidth,
                   arrowHeight
                );

                bottomArrowRectangle = new Rectangle(
                   ClientRectangle.X + 2,
                   ClientRectangle.Bottom - arrowHeight - 1,
                   arrowWidth,
                   arrowHeight
                );

                // Set the default starting thumb position.
                thumbPosition = thumbRectangle.Height / 2;

                // Set the bottom limit of the thumb's bottom border.
                thumbBottomLimitBottom =
                   ClientRectangle.Bottom - arrowHeight - 2;

                // Set the bottom limit of the thumb's top border.
                thumbBottomLimitTop =
                   thumbBottomLimitBottom - thumbRectangle.Height;

                // Set the top limit of the thumb's top border.
                thumbTopLimit = ClientRectangle.Y + arrowHeight + 1;
            }
            else
            {
                arrowHeight = 15;
                arrowWidth = 17;
                thumbHeight = 15;
                thumbWidth = GetThumbSize();

                clickedBarRectangle = ClientRectangle;
                clickedBarRectangle.Inflate(-1, -1);
                clickedBarRectangle.X += arrowWidth;
                clickedBarRectangle.Width -= arrowWidth * 2;

                channelRectangle = clickedBarRectangle;

                thumbRectangle = new Rectangle(
                   ClientRectangle.X + arrowWidth + 1,
                   ClientRectangle.Y + 2,
                   thumbWidth,
                   thumbHeight - 1
                );

                topArrowRectangle = new Rectangle(
                   ClientRectangle.X + 1,
                   ClientRectangle.Y + 2,
                   arrowWidth,
                   arrowHeight
                );

                bottomArrowRectangle = new Rectangle(
                   ClientRectangle.Right - arrowWidth - 1,
                   ClientRectangle.Y + 2,
                   arrowWidth,
                   arrowHeight
                );

                // Set the default starting thumb position.
                thumbPosition = thumbRectangle.Width / 2;

                // Set the bottom limit of the thumb's bottom border.
                thumbBottomLimitBottom =
                   ClientRectangle.Right - arrowWidth - 2;

                // Set the bottom limit of the thumb's top border.
                thumbBottomLimitTop =
                   thumbBottomLimitBottom - thumbRectangle.Width;

                // Set the top limit of the thumb's top border.
                thumbTopLimit = ClientRectangle.X + arrowWidth + 1;
            }

            ChangeThumbPosition(GetThumbPosition());

            Refresh();
        }

        /// <summary>
        /// Handles the updating of the thumb.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">An object that contains the event data.</param>
        private void ProgressTimerTick(object sender, EventArgs e)
        {
            ProgressThumb(true);
        }

        /// <summary>
        /// Resets the scroll status of the scrollbar.
        /// </summary>
        private void ResetScrollStatus()
        {
            // get current mouse position
            Point pos = PointToClient(Cursor.Position);

            // set appearance of buttons in relation to where the mouse is -
            // outside or inside the control
            if (ClientRectangle.Contains(pos))
            {
                bottomButtonState = ScrollBarArrowButtonState.DOWNACTIVE;
                topButtonState = ScrollBarArrowButtonState.UPACTIVE;
            }
            else
            {
                bottomButtonState = ScrollBarArrowButtonState.DOWNNORMAL;
                topButtonState = ScrollBarArrowButtonState.UPNORMAL;
            }

            // set appearance of thumb
            thumbState = thumbRectangle.Contains(pos) ?
               ScrollBarState.HOT : ScrollBarState.NORMAL;

            bottomArrowClicked = bottomBarClicked =
               topArrowClicked = topBarClicked = false;

            StopTimer();

            Refresh();
        }

        /// <summary>
        /// Calculates the new value of the scrollbar.
        /// </summary>
        /// <param name="smallIncrement">true for a small change, false otherwise.</param>
        /// <param name="up">true for up movement, false otherwise.</param>
        /// <returns>The new scrollbar value.</returns>
        private int GetValue(bool smallIncrement, bool up)
        {
            int newValue;

            // calculate the new value of the scrollbar
            // with checking if new value is in bounds (min/max)
            if (up)
            {
                newValue = value - (smallIncrement ? smallChange : largeChange);

                if (newValue < minimum)
                {
                    newValue = minimum;
                }
            }
            else
            {
                newValue = value + (smallIncrement ? smallChange : largeChange);

                if (newValue > maximum)
                {
                    newValue = maximum;
                }
            }

            return newValue;
        }

        /// <summary>
        /// Calculates the new thumb position.
        /// </summary>
        /// <returns>The new thumb position.</returns>
        private int GetThumbPosition()
        {
            int pixelRange, arrowSize;

            if (orientation == ScrollBarOrientation.VERTICAL)
            {
                pixelRange = Height - (2 * arrowHeight) - thumbHeight;
                arrowSize = arrowHeight;
            }
            else
            {
                pixelRange = Width - (2 * arrowWidth) - thumbWidth;
                arrowSize = arrowWidth;
            }

            int realRange = maximum - minimum;
            float perc = 0f;

            if (realRange != 0)
            {
                perc = ((float)value - (float)minimum) / (float)realRange;
            }

            return Math.Max(thumbTopLimit, Math.Min(
               thumbBottomLimitTop,
               Convert.ToInt32((perc * pixelRange) + arrowSize)));
        }

        /// <summary>
        /// Calculates the height of the thumb.
        /// </summary>
        /// <returns>The height of the thumb.</returns>
        private int GetThumbSize()
        {
            int trackSize =
               orientation == ScrollBarOrientation.VERTICAL ?
               Height - (2 * arrowHeight) : Width - (2 * arrowWidth);

            if (maximum == 0 || largeChange == 0)
            {
                return trackSize;
            }

            float newThumbSize = ((float)largeChange * (float)trackSize) / (float)maximum;

            return Convert.ToInt32(Math.Min((float)trackSize, Math.Max(newThumbSize, 10f)));
        }

        /// <summary>
        /// Enables the timer.
        /// </summary>
        private void EnableTimer()
        {
            // if timer is not already enabled - enable it
            if (!progressTimer.Enabled)
            {
                progressTimer.Interval = 600;
                progressTimer.Start();
            }
            else
            {
                // if already enabled, change tick time
                progressTimer.Interval = 10;
            }
        }

        /// <summary>
        /// Stops the progress timer.
        /// </summary>
        private void StopTimer()
        {
            progressTimer.Stop();
        }

        /// <summary>
        /// Changes the position of the thumb.
        /// </summary>
        /// <param name="position">The new position.</param>
        private void ChangeThumbPosition(int position)
        {
            if (orientation == ScrollBarOrientation.VERTICAL)
            {
                thumbRectangle.Y = position;
            }
            else
            {
                thumbRectangle.X = position;
            }
        }

        /// <summary>
        /// Controls the movement of the thumb.
        /// </summary>
        /// <param name="enableTimer">true for enabling the timer, false otherwise.</param>
        private void ProgressThumb(bool enableTimer)
        {
            int scrollOldValue = value;
            ScrollEventType type = ScrollEventType.First;
            int thumbSize, thumbPos;

            if (orientation == ScrollBarOrientation.VERTICAL)
            {
                thumbPos = thumbRectangle.Y;
                thumbSize = thumbRectangle.Height;
            }
            else
            {
                thumbPos = thumbRectangle.X;
                thumbSize = thumbRectangle.Width;
            }

            // arrow down or shaft down clicked
            if (bottomArrowClicked || (bottomBarClicked && (thumbPos + thumbSize) < trackPosition))
            {
                type = bottomArrowClicked ? ScrollEventType.SmallIncrement : ScrollEventType.LargeIncrement;

                value = GetValue(bottomArrowClicked, false);

                if (value == maximum)
                {
                    ChangeThumbPosition(thumbBottomLimitTop);

                    type = ScrollEventType.Last;
                }
                else
                {
                    ChangeThumbPosition(Math.Min(thumbBottomLimitTop, GetThumbPosition()));
                }
            }
            else if (topArrowClicked || (topBarClicked && thumbPos > trackPosition))
            {
                type = topArrowClicked ? ScrollEventType.SmallDecrement : ScrollEventType.LargeDecrement;

                // arrow up or shaft up clicked
                value = GetValue(topArrowClicked, true);

                if (value == minimum)
                {
                    ChangeThumbPosition(thumbTopLimit);

                    type = ScrollEventType.First;
                }
                else
                {
                    ChangeThumbPosition(Math.Max(thumbTopLimit, GetThumbPosition()));
                }
            }
            else if (!((topArrowClicked && thumbPos == thumbTopLimit) || (bottomArrowClicked && thumbPos == thumbBottomLimitTop)))
            {
                ResetScrollStatus();

                return;
            }

            if (scrollOldValue != value)
            {
                OnScroll(new ScrollEventArgs(type, scrollOldValue, value, scrollOrientation));

                Invalidate(channelRectangle);

                if (enableTimer)
                {
                    EnableTimer();
                }
            }
            else
            {
                if (topArrowClicked)
                {
                    type = ScrollEventType.SmallDecrement;
                }
                else if (bottomArrowClicked)
                {
                    type = ScrollEventType.SmallIncrement;
                }

                OnScroll(new ScrollEventArgs(type, value));
            }
        }

        /// <summary>
        /// Changes the displayed text of the context menu items dependent of the current <see cref="ScrollBarOrientation"/>.
        /// </summary>
        private void ChangeContextMenuItems()
        {
            if (orientation == ScrollBarOrientation.VERTICAL)
            {
                tsmiTop.Text = "Top";
                tsmiBottom.Text = "Bottom";
                tsmiLargeDown.Text = "Page down";
                tsmiLargeUp.Text = "Page up";
                tsmiSmallDown.Text = "Scroll down";
                tsmiSmallUp.Text = "Scroll up";
                tsmiScrollHere.Text = "Scroll here";
            }
            else
            {
                tsmiTop.Text = "Left";
                tsmiBottom.Text = "Right";
                tsmiLargeDown.Text = "Page left";
                tsmiLargeUp.Text = "Page right";
                tsmiSmallDown.Text = "Scroll right";
                tsmiSmallUp.Text = "Scroll left";
                tsmiScrollHere.Text = "Scroll here";
            }
        }

        #endregion

        #region context menu methods

        /// <summary>
        /// Initializes the context menu.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            contextMenu = new System.Windows.Forms.ContextMenuStrip(components);
            tsmiScrollHere = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            tsmiTop = new System.Windows.Forms.ToolStripMenuItem();
            tsmiBottom = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            tsmiLargeUp = new System.Windows.Forms.ToolStripMenuItem();
            tsmiLargeDown = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            tsmiSmallUp = new System.Windows.Forms.ToolStripMenuItem();
            tsmiSmallDown = new System.Windows.Forms.ToolStripMenuItem();
            contextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenu
            // 
            contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            tsmiScrollHere,
            toolStripSeparator1,
            tsmiTop,
            tsmiBottom,
            toolStripSeparator2,
            tsmiLargeUp,
            tsmiLargeDown,
            toolStripSeparator3,
            tsmiSmallUp,
            tsmiSmallDown});
            contextMenu.Name = "contextMenu";
            contextMenu.Size = new System.Drawing.Size(151, 176);
            // 
            // tsmiScrollHere
            // 
            tsmiScrollHere.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            tsmiScrollHere.Name = "tsmiScrollHere";
            tsmiScrollHere.Size = new System.Drawing.Size(150, 22);
            tsmiScrollHere.Text = "Scroll here";
            tsmiScrollHere.Click += ScrollHereClick;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(147, 6);
            // 
            // tsmiTop
            // 
            tsmiTop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            tsmiTop.Name = "tsmiTop";
            tsmiTop.Size = new System.Drawing.Size(150, 22);
            tsmiTop.Text = "Top";
            tsmiTop.Click += TopClick;
            // 
            // tsmiBottom
            // 
            tsmiBottom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            tsmiBottom.Name = "tsmiBottom";
            tsmiBottom.Size = new System.Drawing.Size(150, 22);
            tsmiBottom.Text = "Bottom";
            tsmiBottom.Click += BottomClick;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(147, 6);
            // 
            // tsmiLargeUp
            // 
            tsmiLargeUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            tsmiLargeUp.Name = "tsmiLargeUp";
            tsmiLargeUp.Size = new System.Drawing.Size(150, 22);
            tsmiLargeUp.Text = "Page up";
            tsmiLargeUp.Click += LargeUpClick;
            // 
            // tsmiLargeDown
            // 
            tsmiLargeDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            tsmiLargeDown.Name = "tsmiLargeDown";
            tsmiLargeDown.Size = new System.Drawing.Size(150, 22);
            tsmiLargeDown.Text = "Page down";
            tsmiLargeDown.Click += LargeDownClick;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(147, 6);
            // 
            // tsmiSmallUp
            // 
            tsmiSmallUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            tsmiSmallUp.Name = "tsmiSmallUp";
            tsmiSmallUp.Size = new System.Drawing.Size(150, 22);
            tsmiSmallUp.Text = "Scroll up";
            tsmiSmallUp.Click += SmallUpClick;
            // 
            // tsmiSmallDown
            // 
            tsmiSmallDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            tsmiSmallDown.Name = "tsmiSmallDown";
            tsmiSmallDown.Size = new System.Drawing.Size(150, 22);
            tsmiSmallDown.Text = "Scroll down";
            tsmiSmallDown.Click += SmallDownClick;
            contextMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        /// <summary>
        /// Context menu handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ScrollHereClick(object sender, EventArgs e)
        {
            int thumbSize, thumbPos, arrowSize, size;

            if (orientation == ScrollBarOrientation.VERTICAL)
            {
                thumbSize = thumbHeight;
                arrowSize = arrowHeight;
                size = Height;

                ChangeThumbPosition(Math.Max(thumbTopLimit, Math.Min(thumbBottomLimitTop, trackPosition - (thumbRectangle.Height / 2))));

                thumbPos = thumbRectangle.Y;
            }
            else
            {
                thumbSize = thumbWidth;
                arrowSize = arrowWidth;
                size = Width;

                ChangeThumbPosition(Math.Max(thumbTopLimit, Math.Min(thumbBottomLimitTop, trackPosition - (thumbRectangle.Width / 2))));

                thumbPos = thumbRectangle.X;
            }

            int pixelRange = size - (2 * arrowSize) - thumbSize;
            float perc = 0f;

            if (pixelRange != 0)
            {
                perc = (float)(thumbPos - arrowSize) / (float)pixelRange;
            }

            int oldValue = value;

            value = Convert.ToInt32((perc * (maximum - minimum)) + minimum);

            OnScroll(new ScrollEventArgs(ScrollEventType.ThumbPosition, oldValue, value, scrollOrientation));

            Refresh();
        }

        /// <summary>
        /// Context menu handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void TopClick(object sender, EventArgs e)
        {
            Value = minimum;
        }

        /// <summary>
        /// Context menu handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void BottomClick(object sender, EventArgs e)
        {
            Value = maximum;
        }

        /// <summary>
        /// Context menu handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void LargeUpClick(object sender, EventArgs e)
        {
            Value = GetValue(false, true);
        }

        /// <summary>
        /// Context menu handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void LargeDownClick(object sender, EventArgs e)
        {
            Value = GetValue(false, false);
        }

        /// <summary>
        /// Context menu handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void SmallUpClick(object sender, EventArgs e)
        {
            Value = GetValue(true, true);
        }

        /// <summary>
        /// Context menu handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void SmallDownClick(object sender, EventArgs e)
        {
            Value = GetValue(true, false);
        }

        #endregion

        #endregion
    }
}
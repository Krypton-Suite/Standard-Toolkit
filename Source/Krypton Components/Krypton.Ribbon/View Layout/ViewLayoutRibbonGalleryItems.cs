#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// View element that creates and lays out the gallery items.
/// </summary>
internal class ViewLayoutRibbonGalleryItems : ViewComposite
{
    #region Static Fields

    private const int SCROLL_MOVE = 10;

    #endregion

    #region Instance Fields
    private readonly ViewDrawRibbonGalleryButton _buttonUp;
    private readonly ViewDrawRibbonGalleryButton _buttonDown;
    private readonly ViewDrawRibbonGalleryButton _buttonContext;
    private readonly NeedPaintHandler _needPaint;
    private readonly PaletteTripleToPalette _triple;
    private readonly KryptonGallery _gallery;
    private ButtonStyle _style;
    private readonly Timer _scrollTimer;
    private Size _itemSize;
    private int _lineItems;
    private int _displayLines;
    private int _layoutLines;
    private int _topLine;
    private int _endLine;
    private int _offset;
    private int _beginLine;
    private int _bringIntoView;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutRibbonGalleryItems class.
    /// </summary>
    /// <param name="palette">Reference to palette for display values.</param>
    /// <param name="gallery">Reference to owning gallery.</param>
    /// <param name="needPaint">Delegate for requesting paints.</param>
    /// <param name="buttonUp">Reference to the up button.</param>
    /// <param name="buttonDown">Reference to the down button.</param>
    /// <param name="buttonContext">Reference to the context button.</param>
    public ViewLayoutRibbonGalleryItems([DisallowNull] PaletteBase? palette,
        [DisallowNull] KryptonGallery? gallery,
        [DisallowNull] NeedPaintHandler? needPaint,
        [DisallowNull] ViewDrawRibbonGalleryButton? buttonUp,
        [DisallowNull] ViewDrawRibbonGalleryButton? buttonDown,
        [DisallowNull] ViewDrawRibbonGalleryButton? buttonContext)
    {
        Debug.Assert(palette is not null);
        Debug.Assert(gallery is not null);
        Debug.Assert(needPaint is not null);
        Debug.Assert(buttonUp is not null);
        Debug.Assert(buttonDown is not null);
        Debug.Assert(buttonContext is not null);

        if (palette is null)
        {
            throw new ArgumentNullException(nameof(palette));
        }

        _gallery = gallery ?? throw new ArgumentNullException(nameof(gallery));
        _needPaint = needPaint ?? throw new ArgumentNullException(nameof(needPaint));
        _buttonUp = buttonUp ?? throw new ArgumentNullException(nameof(buttonUp));
        _buttonDown = buttonDown ?? throw new ArgumentNullException(nameof(buttonDown));
        _buttonContext = buttonContext ?? throw new ArgumentNullException(nameof(buttonContext));
        _bringIntoView = -1;
        ScrollIntoView = true;

        // Need to know when any button is clicked
        _buttonUp.Click += OnButtonUp;
        _buttonDown.Click += OnButtonDown;
        _buttonContext.Click += OnButtonContext;

        // Create triple that can be used by the draw button
        _style = ButtonStyle.LowProfile;
        _triple = new PaletteTripleToPalette(palette,
            PaletteBackStyle.ButtonLowProfile,
            PaletteBorderStyle.ButtonLowProfile,
            PaletteContentStyle.ButtonLowProfile);

        // Setup timer to use for scrolling lines
        _scrollTimer = new Timer
        {
            Interval = 40
        };
        _scrollTimer.Tick += OnScrollTick;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutRibbonGalleryItems:{Id}";

    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the scrolling into view setting.
    /// </summary>
    public bool ScrollIntoView { get; set; }

    /// <summary>
    /// Gets the number of items currently displayed on a line.
    /// </summary>
    public int ActualLineItems => Math.Max(1, _lineItems);

    /// <summary>
    /// Move tracking to the first item.
    /// </summary>
    public void TrackMoveHome()
    {
        if (Count > 0)
        {
            _gallery.SetTrackingIndex(0, true);
        }
    }

    /// <summary>
    /// Move tracking to the last item.
    /// </summary>
    public void TrackMoveEnd()
    {
        if (Count > 0)
        {
            _gallery.SetTrackingIndex(Count - 1, true);
        }
    }

    /// <summary>
    /// Move tracking upwards by a whole page.
    /// </summary>
    public void TrackMovePageUp()
    {
        if (Count > 0)
        {
            // Move previously by the number of display items
            var trackingIndex = _gallery.TrackingIndex;
            trackingIndex -= _displayLines * _lineItems;

            // Limit check and use new index
            trackingIndex = Math.Max(0, trackingIndex);
            _gallery.SetTrackingIndex(trackingIndex, true);
        }
    }

    /// <summary>
    /// Move tracking downwards by a whole page.
    /// </summary>
    public void TrackMovePageDown()
    {
        if (Count > 0)
        {
            // Move next by the number of display items
            var trackingIndex = _gallery.TrackingIndex;
            trackingIndex += _displayLines * _lineItems;

            // Limit check and use new index
            trackingIndex = Math.Min(trackingIndex, Count - 1);
            _gallery.SetTrackingIndex(trackingIndex, true);
        }
    }

    /// <summary>
    /// Move tracking up one line.
    /// </summary>
    public void TrackMoveUp()
    {
        if (Count > 0)
        {
            // Can only move up if not on the top line of items
            var trackingIndex = _gallery.TrackingIndex;
            if (trackingIndex >= _lineItems)
            {
                // Move up a whole line of items
                trackingIndex -= _lineItems;

                // Limit check and use new index
                trackingIndex = Math.Max(0, trackingIndex);
                _gallery.SetTrackingIndex(trackingIndex, true);
            }
        }
    }

    /// <summary>
    /// Move tracking down one line.
    /// </summary>
    public void TrackMoveDown()
    {
        if (Count > 0)
        {
            if ((_gallery.TrackingIndex + _lineItems) < Count)
            {
                // Move down a whole line of items
                var trackingIndex = _gallery.TrackingIndex;
                trackingIndex += _lineItems;

                // Limit check and use new index
                trackingIndex = Math.Min(trackingIndex, Count - 1);
                _gallery.SetTrackingIndex(trackingIndex, true);
            }
        }
    }

    /// <summary>
    /// Move tracking down left one item.
    /// </summary>
    public void TrackMoveLeft()
    {
        if (Count > 0)
        {
            // Are there more items on the left of the current line
            var trackingIndex = _gallery.TrackingIndex;
            if ((trackingIndex % _lineItems) > 0)
            {
                trackingIndex--;

                // Limit check and use new index
                trackingIndex = Math.Max(0, trackingIndex);
                _gallery.SetTrackingIndex(trackingIndex, true);
            }
        }
    }

    /// <summary>
    /// Move tracking down right one item.
    /// </summary>
    public void TrackMoveRight()
    {
        if (Count > 0)
        {
            // Are there more items on the right of the current line
            var trackingIndex = _gallery.TrackingIndex;
            if ((trackingIndex % _lineItems) < (_lineItems - 1))
            {
                trackingIndex++;

                // Limit check and use new index
                trackingIndex = Math.Min(trackingIndex, Count - 1);
                _gallery.SetTrackingIndex(trackingIndex, true);
            }
        }
    }

    /// <summary>
    /// Is there a next line that can be displayed.
    /// </summary>
    public bool CanNextLine => _topLine < _endLine;

    /// <summary>
    /// Is there a previous line that can be displayed.
    /// </summary>
    public bool CanPrevLine => _topLine > 0;

    /// <summary>
    /// Scroll to make the next line visible.
    /// </summary>
    public void NextLine()
    {
        // New top line is one further down
        var prevTopLine = _topLine;
        _topLine = Math.Min(_topLine + 1, _endLine);

        if (ScrollIntoView)
        {
            // Offset backwards so previous top line is starting position
            _offset -= _itemSize.Height;

            // If offset is still negative then need to check the begin line
            if (_offset < 0)
            {
                // Ensure the old top line can be displayed during scrolling
                if ((_beginLine == -1) || (_beginLine > prevTopLine))
                {
                    _beginLine = prevTopLine;
                }
            }

            // Start the scrolling
            _scrollTimer.Start();
        }
    }

    /// <summary>
    /// Scroll to make the previous line visible.
    /// </summary>
    public void PrevLine()
    {
        // New top line is one further up
        var prevTopLine = _topLine;
        _topLine = Math.Max(_topLine - 1, 0);

        if (ScrollIntoView)
        {
            // Offset forwards so previous top line is starting position
            _offset += _itemSize.Height;

            // If offset is still positive then need to check the begin line
            if (_offset > 0)
            {
                // Ensure the old top line can be displayed during scrolling
                if ((_beginLine == -1) || (_beginLine < prevTopLine))
                {
                    _beginLine = prevTopLine;
                }
            }

            // Start the scrolling
            _scrollTimer.Start();
        }
    }

    /// <summary>
    /// Gets and sets the button style used for each image item.
    /// </summary>
    public ButtonStyle ButtonStyle
    {
        get => _style;

        set
        {
            if (_style != value)
            {
                _style = value;
                _triple.SetStyles(_style);
                _needPaint(this, new NeedLayoutEventArgs(true));
            }
        }
    }

    /// <summary>
    /// Bring the specified image index into view.
    /// </summary>
    /// <param name="index">Index to bring into view.</param>
    public void BringIntoView(int index)
    {
        _bringIntoView = index;
        _gallery.PerformNeedPaint(true);
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Ensure that the correct number of children are created
        SyncChildren();

        var preferredSize = Size.Empty;

        // Find size of the first item, if there is one
        if (Count > 0)
        {
            // Ask child for its own preferred size
            preferredSize = this[0]!.GetPreferredSize(context!);

            // Find preferred size from the preferred item size
            preferredSize.Width *= _gallery.PreferredItemSize.Width;
            preferredSize.Height *= _gallery.PreferredItemSize.Height;
        }

        // Add on the requests padding
        preferredSize.Width += _gallery.Padding.Horizontal;
        preferredSize.Height += _gallery.Padding.Vertical;

        return preferredSize;
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Validate incoming reference
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // We take on all the available display area
        ClientRectangle = context.DisplayRectangle;

        // Ensure that the correct number of children are created
        SyncChildren();

        // Is there anything to layout?
        if (Count > 0)
        {
            // Reduce the client area by the requested padding before the internal children
            Rectangle displayRect = CommonHelper.ApplyPadding(Orientation.Horizontal, ClientRectangle, _gallery.Padding);

            // Get size of the first child, assume all others are same size
            _itemSize = this[0]!.GetPreferredSize(context);

            // Number of items that can be placed on a single line
            _lineItems = Math.Max(1, displayRect.Width / _itemSize.Width);

            // Number of lines needed to show all the items
            _layoutLines = Math.Max(1, (Count + _lineItems - 1) / _lineItems);

            // Number of display lines that can be shown at a time
            _displayLines = Math.Max(1, Math.Min(_layoutLines, displayRect.Height / _itemSize.Height));

            // Index of last line that can be the top line
            _endLine = _layoutLines - _displayLines;

            // Update top-line and offset to reflect any outstanding bring into view request
            ProcessBringIntoView();

            // Limit check the top line is within the valid range
            _topLine = Math.Max(0, Math.Min(_topLine, _endLine));

            // Update the enabled state of the buttons
            _buttonUp.Enabled = _gallery.Enabled && CanPrevLine;
            _buttonDown.Enabled = _gallery.Enabled && CanNextLine;
            _buttonContext.Enabled = _gallery.Enabled && (Count > 0);

            // Calculate position of first item as the left edge but starting downwards
            // and equal amount of the spare space after drawing the display lines.
            Point nextPoint = displayRect.Location;
            nextPoint.Y += (displayRect.Height - (_displayLines * _itemSize.Height)) / 2;

            // Stating item is from the top line and last item is number of display items onwards
            var start = _topLine * _lineItems;
            var end = start + (_displayLines * _lineItems);

            // Do we need to handle scroll offsetting?
            var offset = _offset;
            if (offset != 0)
            {
                if (offset < 0)
                {
                    // How many extra full lines needed by the scrolling
                    var extraLines = _topLine - _beginLine;

                    // Limit check the number of previous lines to show
                    if ((_topLine - extraLines) < 0)
                    {
                        extraLines = _topLine;
                    }

                    // Move start to ensure that the previous lines are visible
                    start -= extraLines * _lineItems;

                    // Adjust offset to reflect change in start
                    offset += extraLines * _itemSize.Height;
                }
                else
                {
                    // How many extra full lines needed by the scrolling
                    var extraLines = _beginLine - _topLine;

                    // Move start to ensure that the previous lines are visible
                    end += extraLines * _lineItems;

                    // Limit check the end item to stop it overflowing number of items
                    if (end > Count)
                    {
                        end = Count;
                    }
                }
            }

            // Add scrolling offset
            nextPoint.Y -= offset;

            // Position all children on single line from left to right
            for (var i = 0; i < Count; i++)
            {
                ViewBase? childItem = this[i];

                // Should this item be visible
                if ((i < start) || (i >= end))
                {
                    childItem!.Visible = false;
                }
                else
                {
                    childItem!.Visible = true;

                    // Find rectangle for the child
                    context.DisplayRectangle = new Rectangle(nextPoint, _itemSize);

                    // Layout the child
                    childItem.Layout(context);

                    // Move across to next position
                    nextPoint.X += _itemSize.Width;

                    // If there is not enough room for another item on this line
                    if ((nextPoint.X + _itemSize.Width) > displayRect.Right)
                    {
                        // Move down to next line
                        nextPoint.X = displayRect.X;
                        nextPoint.Y += _itemSize.Height;
                    }
                }
            }
        }
        else
        {
            // No children means no items and so need for enabled buttons
            _buttonUp.Enabled = false;
            _buttonDown.Enabled = false;
            _buttonContext.Enabled = false;
        }

        // Put back the original display value now we have finished
        context.DisplayRectangle = ClientRectangle;
    }
    #endregion

    #region Private
    public void SyncChildren()
    {
        var required = 0;
        var selectedIndex = _gallery.SelectedIndex;
        ImageList? imageList = _gallery.ImageList;

        // Find out how many children we need
        if (imageList != null)
        {
            required = _gallery.ImageList!.Images.Count;
        }

        // If we do not have enough already
        if (Count < required)
        {
            // Create and add the number extra needed
            var create = required - Count;
            for (var i = 0; i < create; i++)
            {
                Add(new ViewDrawRibbonGalleryItem(_gallery, _triple, this, _needPaint));
            }
        }
        else if (Count > required)
        {
            // Destroy the extra ones no longer needed
            var remove = Count - required;
            for (var i = 0; i < remove; i++)
            {
                RemoveAt(0);
            }
        }

        // Tell each item the image it should be displaying
        for (var i = 0; i < required; i++)
        {
            var item = this[i] as ViewDrawRibbonGalleryItem;
            item!.ImageList = imageList;
            item.ImageIndex = i;
            item.Checked = selectedIndex == i;
        }
    }

    private void OnButtonUp(object? sender, MouseEventArgs e)
    {
        PrevLine();
        _gallery.PerformNeedPaint(true);
    }

    private void OnButtonDown(object? sender, MouseEventArgs e)
    {
        NextLine();
        _gallery.PerformNeedPaint(true);
    }

    private void OnButtonContext(object? sender, MouseEventArgs e)
    {
        _buttonContext.ForceLeave();
        _gallery.OnDropButton();
    }

    private void OnScrollTick(object? sender, EventArgs e)
    {
        // Update the offset by scroll move amount
        if (_offset != 0)
        {
            _offset = _offset > 0 ? Math.Max(0, _offset - SCROLL_MOVE) : Math.Min(0, _offset + SCROLL_MOVE);
        }

        // If we have finished the scrolling
        if (_offset == 0)
        {
            _beginLine = -1;
            _scrollTimer.Stop();
        }

        // Need to repaint to show changes
        _needPaint(this, new NeedLayoutEventArgs(true));
    }

    private void ProcessBringIntoView()
    {
        // Do we need to process a 'BringIntoView' request?
        if (_bringIntoView >= 0)
        {
            // If there are any lines to actually work against
            if (_lineItems > 0)
            {
                // Find target line for bringing into view
                var line = _bringIntoView / _lineItems;
                var itemLine = line;

                // Limit check to the last line for display purposes
                if (line > _endLine)
                {
                    line = _endLine;
                }

                // Cache top line before any changes made to it
                var prevTopLine = _topLine;

                // Is that line before the current top line?
                if (line < _topLine)
                {
                    // How many lines do we need to scroll upwards
                    var diffLines = _topLine - line;

                    // Shift topline to target immediately
                    _topLine = line;

                    // If we are supposed to scroll to the target position
                    if (ScrollIntoView)
                    {
                        // Modify the offset to reflect change in number of lines
                        _offset += _itemSize.Height * diffLines;
                        _scrollTimer.Start();
                    }
                    else
                    {
                        _offset = 0;
                        _scrollTimer.Stop();
                    }
                }
                else if (itemLine >= (_topLine + _displayLines))
                {
                    // How many lines do we need to scroll upwards
                    var diffLines = itemLine - (_topLine + (_displayLines - 1));

                    // Shift topline to target immediately
                    _topLine = itemLine - (_displayLines - 1);

                    if (ScrollIntoView)
                    {
                        // Modify the offset to reflect change in number of lines
                        _offset -= _itemSize.Height * diffLines;
                        _scrollTimer.Start();
                    }
                    else
                    {
                        _offset = 0;
                        _scrollTimer.Stop();
                    }
                }

                switch (_offset)
                {
                    // Update the begin line
                    case < 0:
                    {
                        // Ensure the old top line can be displayed during scrolling
                        if ((_beginLine == -1) || (_beginLine > prevTopLine))
                        {
                            _beginLine = prevTopLine;
                        }

                        break;
                    }
                    case > 0:
                    {
                        // Ensure the old top line can be displayed during scrolling
                        if ((_beginLine == -1) || (_beginLine < prevTopLine))
                        {
                            _beginLine = prevTopLine;
                        }

                        break;
                    }
                }
            }

            // Reset the request
            _bringIntoView = -1;
        }
    }
    #endregion
}
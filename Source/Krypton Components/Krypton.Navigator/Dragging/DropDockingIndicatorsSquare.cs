#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

// ReSharper disable VirtualMemberCallInConstructor
namespace Krypton.Navigator;

/// <summary>
/// Draws a window containing square docking indicators.
/// </summary>
public class DropDockingIndicatorsSquare : KryptonForm,
    IDropDockingIndicator
{
    #region Instance Fields
    private readonly IRenderer _renderer;
    private readonly IPaletteDragDrop _paletteDragDrop;
    private readonly RenderDragDockingData _dragData;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the DropDockingIndicatorsSquare class.
    /// </summary>
    /// <param name="paletteDragDrop">Drawing palette.</param>
    /// <param name="renderer">Drawing renderer.</param>
    /// <param name="showLeft">Show left hot area.</param>
    /// <param name="showRight">Show right hot area.</param>
    /// <param name="showTop">Show top hot area.</param>
    /// <param name="showBottom">Show bottom hot area.</param>
    /// <param name="showMiddle">Show middle hot area.</param>
    public DropDockingIndicatorsSquare(IPaletteDragDrop paletteDragDrop,
        IRenderer renderer,
        bool showLeft, bool showRight,
        bool showTop, bool showBottom,
        bool showMiddle)
    {
        SetInheritedControlOverride();
        _paletteDragDrop = paletteDragDrop;
        _renderer = renderer;

        // Initialize the drag data that indicators which docking indicators are needed
        _dragData = new RenderDragDockingData(showLeft, showRight, showTop, showBottom, showMiddle);

        // Ask the renderer to measure the sizing of the indicators that are Displayed
        _renderer?.RenderGlyph.MeasureDragDropDockingGlyph(_dragData, _paletteDragDrop,
            PaletteDragFeedback.Square);

        // Setup window so that it is transparent to the Silver color and does not have any borders etc...
        BackColor = Color.Silver;
        ClientSize = _dragData.DockWindowSize;
        ControlBox = false;
        FormBorderStyle = FormBorderStyle.None;
        Location = new Point(100, 200);
        MaximizeBox = false;
        MinimizeBox = false;
        MinimumSize = Size.Empty;
        Name = "DropIndicators";
        ShowInTaskbar = false;
        SizeGripStyle = SizeGripStyle.Hide;
        StartPosition = FormStartPosition.Manual;
        Text = @"DropIndicators";
        TransparencyKey = Color.Silver;
        Paint += DropIndicators_Paint;
    }
    #endregion

    #region Public
    /// <summary>
    /// Show the window relative to provided screen rectangle.
    /// </summary>
    /// <param name="screenRect">Screen rectangle.</param>
    public void ShowRelative(Rectangle screenRect)
    {
        // Find screen middle points
        var yMid = screenRect.Y + (screenRect.Height / 2);
        var xMid = screenRect.X + (screenRect.Width / 2);

        // Find docking size middle points
        var yHalf = _dragData.DockWindowSize.Height / 2;
        var xHalf = _dragData.DockWindowSize.Width / 2;

        switch (_dragData.ShowLeft)
        {
            case true when _dragData is { ShowRight: false, ShowMiddle: false } and { ShowTop: false, ShowBottom: false }:
                Location = new Point(screenRect.Left + 10, yMid - yHalf);
                break;
            case false when _dragData is { ShowRight: true, ShowMiddle: false } and { ShowTop: false, ShowBottom: false }:
                Location = new Point(screenRect.Right - _dragData.DockWindowSize.Width - 10, yMid - yHalf);
                break;
            case false when _dragData is { ShowRight: false, ShowMiddle: false } and { ShowTop: true, ShowBottom: false }:
                Location = new Point(xMid - xHalf, screenRect.Top + 10);
                break;
            case false when _dragData is { ShowRight: false, ShowMiddle: false } and { ShowTop: false, ShowBottom: true }:
                Location = new Point(xMid - xHalf, screenRect.Bottom - _dragData.DockWindowSize.Height - 10);
                break;
            default:
                Location = new Point(xMid - xHalf, yMid - yHalf);
                break;
        }

        // Show the window without activating it (i.e. do not take focus)
        PI.ShowWindow(Handle, PI.ShowWindowCommands.SW_SHOWNOACTIVATE);
    }

    /// <summary>
    /// Perform mouse hit testing against a screen point.
    /// </summary>
    /// <param name="screenPoint">Screen point.</param>
    /// <returns>Area that is active.</returns>
    public int ScreenMouseMove(Point screenPoint)
    {
        // Convert from screen to client coordinates
        Point pt = PointToClient(screenPoint);

        // Remember the current active value
        var activeBefore = _dragData.ActiveFlags;

        // Reset active back to nothing
        _dragData.ClearActive();

        // Find new active area
        if (_dragData.ShowLeft && _dragData.RectLeft.Contains(pt))
        {
            _dragData.ActiveLeft = true;
        }
        if (_dragData.ShowRight && _dragData.RectRight.Contains(pt))
        {
            _dragData.ActiveRight = true;
        }
        if (_dragData.ShowTop && _dragData.RectTop.Contains(pt))
        {
            _dragData.ActiveTop = true;
        }
        if (_dragData.ShowBottom && _dragData.RectBottom.Contains(pt))
        {
            _dragData.ActiveBottom = true;
        }

        // Only consider the middle if the others do not match
        if (_dragData is { ActiveFlags: 0, ShowMiddle: true } && _dragData.RectMiddle.Contains(pt))
        {
            _dragData.ActiveMiddle = true;
        }

        // Do we need to update the display?
        if (_dragData.ActiveFlags != activeBefore)
        {
            Invalidate();
        }

        return _dragData.ActiveFlags;
    }

    /// <summary>
    /// Ensure the state is updated to reflect the mouse not being over the control.
    /// </summary>
    public void MouseReset()
    {
        // Do we need to update display?
        if (_dragData.AnyActive)
        {
            _dragData.ClearActive();
            Invalidate();
        }
    }
    #endregion

    #region Implementation
    private void DropIndicators_Paint(object? sender, PaintEventArgs e)
    {
        using var context = new RenderContext(this, e.Graphics, e.ClipRectangle, _renderer);
        _renderer?.RenderGlyph.DrawDragDropDockingGlyph(context, _dragData, _paletteDragDrop, PaletteDragFeedback.Square);
    }

    private void DrawPath(Graphics g, Color baseColor, GraphicsPath path)
    {
        // Draw a smooth outline around the circle
        using var outline = new Pen(baseColor);
        g.DrawPath(outline, path);
    }
    #endregion
}
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

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Docking;

/// <summary>
/// Manage a docking dragging operation.
/// </summary>
public class DockingDragManager : DragManager,
    IFloatingMessages,
    IMessageFilter
{
    #region Instance Fields
    private readonly KryptonDockingManager _manager;
    private Point _offset;
    private Point _screenPt;
    private readonly Timer _moveTimer;
    private bool _addedFilter;
    private bool _monitorMouse;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the DockingDragManager class.
    /// </summary>
    /// <param name="manager">Reference to manager creating this instance.</param>
    /// <param name="c">Control that is starting the drag operation.</param>
    public DockingDragManager(KryptonDockingManager manager, Control? c)
    {
        _manager = manager;
        _offset = Point.Empty;

        // Use timer to ensure we do not update the display too quickly which then causes tearing
        _moveTimer = new Timer
        {
            Interval = 10
        };
        _moveTimer.Tick += OnFloatingWindowMove;
    }

    /// <summary>
    /// Release unmanaged and optionally managed resources.
    /// </summary>
    /// <param name="disposing">Called from Dispose method.</param>
    protected override void Dispose(bool disposing)
    {
        RemoveFilter();

        // Remove any temporary pages created during the dragging process that are used to prevent cells being removed 
        _manager.PropogateAction(DockingPropogateAction.ClearStoredPages, new[] { "TemporaryPage" });

        // Remember to unhook event and dispose timer to prevent resource leak
        _moveTimer.Tick -= OnFloatingWindowMove;
        _moveTimer.Stop();
        _moveTimer.Dispose();

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the window that is moved in sync with the mouse movement.
    /// </summary>
    public KryptonFloatingWindow? FloatingWindow { get; set; }

    /// <summary>
    /// Gets and sets the offset of the floating window from the screen cursor.
    /// </summary>
    public Point FloatingWindowOffset
    {
        get => _offset;
        set => _offset = value;
    }

    /// <summary>
    /// Occurs when dragging starts.
    /// </summary>
    /// <param name="screenPt">Mouse screen point at start of drag.</param>
    /// <param name="dragEndData">Data to be dropped at destination.</param>
    /// <returns>True if dragging was started; otherwise false.</returns>
    public override bool DragStart(Point screenPt, PageDragEndData? dragEndData)
    {
        if (FloatingWindow != null)
        {
            FloatingWindow.Capture = true;
        }

        AddFilter();
        return base.DragStart(screenPt, dragEndData);
    }

    /// <summary>
    /// Occurs on dragging movement.
    /// </summary>
    /// <param name="screenPt">Latest screen point during dragging.</param>
    public override void DragMove(Point screenPt)
    {
        if (FloatingWindow != null)
        {
            _screenPt = screenPt;
            _moveTimer.Start();
        }

        base.DragMove(screenPt);
    }

    private void OnFloatingWindowMove(object? sender, EventArgs e)
    {
        _moveTimer.Stop();

        // Position the floating window relative to the screen position
        if (FloatingWindow != null)
        {
            if (_offset.X > (FloatingWindow.Width - 20))
            {
                _offset.X = FloatingWindow.Width - 20;
            }

            if (_offset.Y > (FloatingWindow.Height - 20))
            {
                _offset.Y = FloatingWindow.Height - 20;
            }

            FloatingWindow.SetBounds(_screenPt.X - FloatingWindowOffset.X,
                _screenPt.Y - FloatingWindowOffset.Y,
                FloatingWindow.Width,
                FloatingWindow.Height,
                BoundsSpecified.Location);
        }
    }

    /// <summary>
    /// Occurs when dragging ends because of dropping.
    /// </summary>
    /// <param name="screenPt">Ending screen point when dropping.</param>
    /// <returns>Drop was performed and the source can perform any removal of pages as required.</returns>
    public override bool DragEnd(Point screenPt)
    {
        RemoveFilter();
        var ret = base.DragEnd(screenPt);
        _manager.RaiseDoDragDropEnd(EventArgs.Empty);
        return ret;
    }

    /// <summary>
    /// Occurs when dragging quits.
    /// </summary>
    public override void DragQuit()
    {
        RemoveFilter();
        base.DragQuit();
        _manager.RaiseDoDragDropQuit(EventArgs.Empty);
    }

    /// <summary>
    /// Processes the WM_KEYDOWN from the floating window.
    /// </summary>
    /// <returns>True to eat message; otherwise false.</returns>
    public bool OnKEYDOWN(ref Message m)
    {
        // Pressing escape ends dragging
        if ((int)m.WParam.ToInt64() == (int)Keys.Escape)
        {
            DragQuit();
            Dispose();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Processes the WM_MOUSEMOVE from the floating window.
    /// </summary>
    public void OnMOUSEMOVE() =>
        // Update feedback to reflect the current mouse position
        DragMove(Control.MousePosition);

    /// <summary>
    /// Processes the WM_LBUTTONUP from the floating window.
    /// </summary>
    public void OnLBUTTONUP()
    {
        DragEnd(Control.MousePosition);
        Dispose();
    }

    /// <summary>
    /// Filters out a message before it is dispatched.
    /// </summary>
    /// <param name="m">The message to be dispatched.</param>
    /// <returns>true to filter the message and stop it from being dispatched.</returns>
    public bool PreFilterMessage(ref Message m)
    {
        switch (m.Msg)
        {
            case PI.WM_.MOUSELEAVE:
                // Only interested in the mouse leave if it relates to the floating window and so ignore any
                // message that comes from the mouse leaving the source of a drag such as a docked window or
                // a workspace/navigator tab.
                if (m.HWnd == FloatingWindow?.Handle)
                {
                    // If mouse has left then we need to finish with docking. Most likely reason is the focus
                    // has been shifted to another application with ALT+TAB.
                    DragEnd(Control.MousePosition);
                    Dispose();
                }
                break;
            case PI.WM_.KEYDOWN:
            {
                // Extract the keys being pressed
                var keys = (Keys)(int)m.WParam.ToInt64();

                // Pressing escape ends dragging
                if (keys == Keys.Escape)
                {
                    DragQuit();
                    Dispose();
                }

                return true;
            }
            case PI.WM_.SYSKEYDOWN:
                // Pressing ALT+TAB ends dragging because user is moving to another app
                if (PI.IsKeyDown(Keys.Tab)
                    && ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)
                   )
                {
                    DragQuit();
                    Dispose();
                }
                break;
            case PI.WM_.MOUSEMOVE:
                if (_monitorMouse)
                {
                    // Update feedback to reflect the current mouse position
                    DragMove(Control.MousePosition);
                }
                break;
            case PI.WM_.LBUTTONUP:
                if (_monitorMouse)
                {
                    DragEnd(Control.MousePosition);
                    Dispose();
                }
                break;
        }

        return false;
    }
    #endregion

    #region Implementation
    private void AddFilter()
    {
        if (FloatingWindow != null)
        {
            _monitorMouse = false;
            FloatingWindow.FloatingMessages = this;
        }
        else
        {
            _monitorMouse = true;
        }

        // We always monitor for keyboard events and sometimes mouse events
        if (!_addedFilter)
        {
            Application.AddMessageFilter(this);
            _addedFilter = true;
        }
    }

    private void RemoveFilter()
    {
        if (FloatingWindow != null)
        {
            FloatingWindow.FloatingMessages = null;
        }

        // Must remove filter to prevent memory leaks
        if (_addedFilter)
        {
            Application.RemoveMessageFilter(this);
            _addedFilter = false;
        }
    }
    #endregion
}
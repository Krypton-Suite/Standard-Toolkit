#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Docking;

/// <summary>
/// Coordinates docking drag-drop, floating window movement, and application message filtering.
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
    /// Wires the drag manager to the owning docking manager and optional source control.
    /// </summary>
    /// <param name="manager">Docking manager that owns this drag operation.</param>
    /// <param name="c">Control that initiated the drag, or <see langword="null"/> when not applicable.</param>
    public DockingDragManager(KryptonDockingManager manager, Control? c)
    {
        _manager = manager;
        _offset = Point.Empty;

        // Timer is kept for backward compatibility but no longer used for position updates
        // Position updates now happen immediately in DragMove for responsive dragging
        // Use timer to ensure we do not update the display too quickly which then causes tearing
        // TODO: Remove in a future version
        _moveTimer = new Timer
        {
            Interval = 10
        };
        _moveTimer.Tick += OnFloatingWindowMove;
    }

    /// <summary>
    /// Removes the message filter, clears temporary stored pages, and disposes the move timer.
    /// </summary>
    /// <param name="disposing"><see langword="true"/> when called from <see cref="Dispose"/>; otherwise <see langword="false"/>.</param>
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
    /// Floating window repositioned with the cursor during the drag; <see langword="null"/> when dragging without a floating window.
    /// </summary>
    public KryptonFloatingWindow? FloatingWindow { get; set; }

    /// <summary>
    /// Screen offset between the cursor and the top-left corner of <see cref="FloatingWindow"/>.
    /// </summary>
    public Point FloatingWindowOffset
    {
        get => _offset;
        set => _offset = value;
    }

    /// <summary>
    /// Captures the floating window, installs message filtering, and starts base drag feedback.
    /// </summary>
    /// <param name="screenPt">Screen coordinates where the drag started.</param>
    /// <param name="dragEndData">Pages and context carried through the drag operation.</param>
    /// <returns><see langword="true"/> when base drag startup succeeds; otherwise <see langword="false"/>.</returns>
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
    /// Repositions <see cref="FloatingWindow"/> to follow the cursor and updates base drag feedback.
    /// </summary>
    /// <param name="screenPt">Current screen coordinates of the cursor.</param>
    public override void DragMove(Point screenPt)
    {
        if (FloatingWindow != null)
        {
            _screenPt = screenPt;
            // Update position immediately for responsive dragging
            UpdateFloatingWindowPosition();
        }

        base.DragMove(screenPt);
    }

    private void UpdateFloatingWindowPosition()
    {
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

    private void OnFloatingWindowMove(object? sender, EventArgs e)
    {
        _moveTimer.Stop();

        // This method is no longer used but kept for backward compatibility
        // Position updates now happen immediately in DragMove
    }

    /// <summary>
    /// Removes message filtering, completes the drop through the base drag manager, and raises <c>DoDragDropEnd</c> on the docking manager.
    /// </summary>
    /// <param name="screenPt">Screen coordinates where the drop occurred.</param>
    /// <returns><see langword="true"/> when the drop succeeded and the source may remove dragged pages; otherwise <see langword="false"/>.</returns>
    public override bool DragEnd(Point screenPt)
    {
        RemoveFilter();
        var ret = base.DragEnd(screenPt);
        _manager.RaiseDoDragDropEnd(EventArgs.Empty);
        return ret;
    }

    /// <summary>
    /// Removes message filtering, cancels the drag through the base drag manager, and raises <c>DoDragDropQuit</c> on the docking manager.
    /// </summary>
    public override void DragQuit()
    {
        RemoveFilter();
        base.DragQuit();
        _manager.RaiseDoDragDropQuit(EventArgs.Empty);
    }

    /// <summary>
    /// Ends the drag when Escape is pressed on the floating window.
    /// </summary>
    /// <param name="m">Windows message structure for the key-down event.</param>
    /// <returns><see langword="true"/> when Escape was handled and the message should not propagate further; otherwise <see langword="false"/>.</returns>
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
    /// Forwards the current cursor position to <see cref="DragMove"/>.
    /// </summary>
    public void OnMOUSEMOVE() =>
        // Update feedback to reflect the current mouse position
        DragMove(Control.MousePosition);

    /// <summary>
    /// Completes the drag at the current cursor position and disposes this manager.
    /// </summary>
    public void OnLBUTTONUP()
    {
        DragEnd(Control.MousePosition);
        Dispose();
    }

    /// <summary>
    /// Intercepts keyboard and mouse messages during an active drag to update feedback or cancel the operation.
    /// </summary>
    /// <param name="m">Message being dispatched by the application.</param>
    /// <returns><see langword="true"/> to stop further dispatch of keyboard messages; otherwise <see langword="false"/>.</returns>
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

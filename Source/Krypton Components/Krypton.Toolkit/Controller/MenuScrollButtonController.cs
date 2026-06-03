#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Controller for a context menu scroll overflow button.
/// </summary>
internal class MenuScrollButtonController : GlobalId,
    IMouseController,
    IKeyController,
    IContextMenuTarget
{
    #region Static Fields
    private const int ScrollRepeatIntervalMs = 50;
    #endregion

    #region Instance Fields
    private bool _mouseOver;
    private System.Windows.Forms.Timer? _scrollTimer;
    private readonly ViewDrawMenuScrollButton _scrollButton;
    private readonly NeedPaintHandler? _needPaint;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the MenuScrollButtonController class.
    /// </summary>
    /// <param name="viewManager">Owning view manager instance.</param>
    /// <param name="scrollButton">Target scroll button view element.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public MenuScrollButtonController(ViewContextMenuManager viewManager,
        ViewDrawMenuScrollButton scrollButton,
        NeedPaintHandler? needPaint)
    {
        ViewManager = viewManager;
        _scrollButton = scrollButton;
        _needPaint = needPaint;
    }
    #endregion

    #region ViewContextMenuManager
    /// <summary>
    /// Gets access to the view manager.
    /// </summary>
    public ViewContextMenuManager ViewManager { get; }

    #endregion

    #region IContextMenuTarget
    /// <inheritdoc />
    public bool HasSubMenu => false;

    /// <inheritdoc />
    public void ShowTarget()
    {
        _scrollButton.HighlightState();
        ActivateScroll();
        StartScrollTimer();
    }

    /// <inheritdoc />
    public void ClearTarget()
    {
        StopScrollTimer();
        _scrollButton.NormalState();
    }

    /// <inheritdoc />
    public void ShowSubMenu()
    {
    }

    /// <inheritdoc />
    public void ClearSubMenu()
    {
    }

    /// <inheritdoc />
    public bool MatchMnemonic(char charCode) => false;

    /// <inheritdoc />
    public void MnemonicActivate() => ActivateScroll();

    /// <inheritdoc />
    public ViewBase GetActiveView() => _scrollButton;

    /// <inheritdoc />
    public Rectangle ClientRectangle => _scrollButton.ClientRectangle;

    /// <inheritdoc />
    public bool DoesStackedClientMouseDownBecomeCurrent(Point pt) => true;
    #endregion

    #region IMouseController
    /// <inheritdoc />
    public void MouseEnter(Control c)
    {
        if (!_mouseOver && _scrollButton.ItemEnabled)
        {
            _mouseOver = true;
            ViewManager.SetTarget(this, false);
        }
    }

    /// <inheritdoc />
    public void MouseMove(Control c, Point pt)
    {
        if (_mouseOver && _scrollButton.ItemEnabled && _scrollTimer == null)
        {
            ViewManager.SetTarget(this, false);
        }
    }

    /// <inheritdoc />
    public bool MouseDown(Control c, Point pt, MouseButtons button) => false;

    /// <inheritdoc />
    public void MouseUp(Control c, Point pt, MouseButtons button)
    {
        if (button == MouseButtons.Left && _scrollButton.ItemEnabled)
        {
            ActivateScroll();
        }
    }

    /// <inheritdoc />
    public void MouseLeave(Control c, ViewBase? next)
    {
        if (_mouseOver && !_scrollButton.ContainsRecurse(next))
        {
            _mouseOver = false;
            ViewManager.ClearTarget(this);
        }
    }

    /// <inheritdoc />
    public void DoubleClick(Point pt)
    {
    }

    /// <inheritdoc />
    public bool IgnoreVisualFormLeftButtonDown => false;
    #endregion

    #region IKeyController
    /// <inheritdoc />
    public void KeyDown(Control c, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Enter:
            case Keys.Space:
                ActivateScroll();
                break;
            case Keys.Up:
                if (_scrollButton.ScrollUp)
                {
                    ActivateScroll();
                }
                else
                {
                    ViewManager.KeyUp();
                }

                break;
            case Keys.Down:
                if (_scrollButton.ScrollUp)
                {
                    ViewManager.KeyDown();
                }
                else
                {
                    ActivateScroll();
                }

                break;
            case Keys.Left:
                ViewManager.KeyLeft(false);
                break;
            case Keys.Right:
                ViewManager.KeyRight();
                break;
        }
    }

    /// <inheritdoc />
    public void KeyPress(Control c, KeyPressEventArgs e)
    {
    }

    /// <inheritdoc />
    public bool KeyUp(Control c, KeyEventArgs e) => false;
    #endregion

    #region Implementation
    private void StartScrollTimer()
    {
        if (!CanScrollFurther())
        {
            return;
        }

        StopScrollTimer();

        _scrollTimer = new System.Windows.Forms.Timer
        {
            Interval = ScrollRepeatIntervalMs
        };
        _scrollTimer.Tick += OnScrollTimerTick;
        _scrollTimer.Start();
    }

    private void StopScrollTimer()
    {
        if (_scrollTimer != null)
        {
            _scrollTimer.Stop();
            _scrollTimer.Tick -= OnScrollTimerTick;
            _scrollTimer.Dispose();
            _scrollTimer = null;
        }
    }

    private void OnScrollTimerTick(object? sender, EventArgs e)
    {
        if (!CanScrollFurther())
        {
            StopScrollTimer();
            return;
        }

        ActivateScroll();
    }

    private bool CanScrollFurther()
    {
        ViewLayoutContextMenuOverflowColumn? column = _scrollButton.OverflowColumn;
        if (column == null)
        {
            return false;
        }

        return _scrollButton.ScrollUp ? column.HasMoreAbove : column.HasMoreBelow(null);
    }

    private void ActivateScroll()
    {
        ViewLayoutContextMenuOverflowColumn? column = _scrollButton.OverflowColumn;
        if (column == null)
        {
            return;
        }

        if (_scrollButton.ScrollUp)
        {
            if (!column.HasMoreAbove)
            {
                StopScrollTimer();
                return;
            }
        }
        else if (!column.HasMoreBelow(null))
        {
            StopScrollTimer();
            return;
        }

        column.Scroll(_scrollButton.ScrollUp);
        _needPaint?.Invoke(this, new NeedLayoutEventArgs(true));

        // Items can scroll under the cursor; keep the overflow button active while hovered
        if (_mouseOver)
        {
            ViewManager.SetTarget(this, false);
        }

        if (!CanScrollFurther())
        {
            StopScrollTimer();
        }
    }
    #endregion
}

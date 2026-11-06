#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Charting;

/// <summary>
/// Mouse and keyboard input shared by the timeline views.
/// </summary>
internal sealed class TimelineInputController : GlobalId,
    IMouseController,
    IKeyController,
    ISourceController
{
    #region Instance Fields

    private readonly Func<Point, int> _hitTest;
    private readonly Action<int> _setHot;
    private readonly Action<int, bool> _select;
    private readonly Action _leave;
    private readonly Func<KeyEventArgs, bool> _keyDown;
    private readonly Action<Point> _panStart;
    private readonly Action<Point> _panMove;
    private readonly Action _panEnd;
    private readonly Func<int, Point, bool> _tryBeginMove;
    private readonly Action<Point> _moveItem;
    private readonly Action _endMove;
    private bool _captured;
    private bool _panning;
    private bool _moving;
    private int _downIndex;
    private Point _downPoint;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="TimelineInputController"/> class.
    /// </summary>
    public TimelineInputController(Func<Point, int> hitTest,
        Action<int> setHot,
        Action<int, bool> select,
        Action leave,
        Func<KeyEventArgs, bool> keyDown,
        Action<Point>? panStart,
        Action<Point>? panMove,
        Action? panEnd,
        Func<int, Point, bool>? tryBeginMove,
        Action<Point>? moveItem,
        Action? endMove)
    {
        _hitTest = hitTest;
        _setHot = setHot;
        _select = select;
        _leave = leave;
        _keyDown = keyDown;
        _panStart = panStart ?? (_ => { });
        _panMove = panMove ?? (_ => { });
        _panEnd = panEnd ?? (() => { });
        _tryBeginMove = tryBeginMove ?? ((index, point) => false);
        _moveItem = moveItem ?? (_ => { });
        _endMove = endMove ?? (() => { });
    }

    #endregion

    #region Mouse

    /// <inheritdoc />
    public void MouseEnter(Control c)
    {
    }

    /// <inheritdoc />
    public void MouseMove(Control c, Point pt)
    {
        if (_captured && _downIndex >= 0)
        {
            if (!_moving &&
                (Math.Abs(pt.X - _downPoint.X) > 3 || Math.Abs(pt.Y - _downPoint.Y) > 3))
            {
                _moving = _tryBeginMove(_downIndex, _downPoint);
            }

            if (_moving)
            {
                _moveItem(pt);
            }

            return;
        }

        if (_captured && _downIndex < 0)
        {
            if (!_panning &&
                (Math.Abs(pt.X - _downPoint.X) > 3 || Math.Abs(pt.Y - _downPoint.Y) > 3))
            {
                _panning = true;
            }

            if (_panning)
            {
                _panMove(pt);
            }

            return;
        }

        _setHot(_hitTest(pt));
    }

    /// <inheritdoc />
    public bool MouseDown(Control c, Point pt, MouseButtons button)
    {
        if (button != MouseButtons.Left)
        {
            return false;
        }

        _captured = true;
        _panning = false;
        _moving = false;
        _downPoint = pt;
        _downIndex = _hitTest(pt);
        if (_downIndex < 0)
        {
            _panStart(pt);
        }

        return true;
    }

    /// <inheritdoc />
    public void MouseUp(Control c, Point pt, MouseButtons button)
    {
        if (_captured && button == MouseButtons.Left && !_panning && !_moving)
        {
            int hit = _hitTest(pt);
            if (hit >= 0)
            {
                _select(hit, true);
            }
        }

        _captured = false;
        _panning = false;
        _moving = false;
        _panEnd();
        _endMove();
        _setHot(_hitTest(pt));
    }

    /// <inheritdoc />
    public void MouseLeave(Control c, ViewBase? next)
    {
        _setHot(-1);
        _leave();
    }

    /// <inheritdoc />
    public void DoubleClick(Point pt)
    {
        int hit = _hitTest(pt);
        if (hit >= 0)
        {
            _select(hit, true);
        }
    }

    /// <inheritdoc />
    public bool IgnoreVisualFormLeftButtonDown => false;

    #endregion

    #region Keys

    /// <inheritdoc />
    public void KeyDown(Control c, KeyEventArgs e)
    {
        if (_keyDown(e))
        {
            e.Handled = true;
        }
    }

    /// <inheritdoc />
    public void KeyPress(Control c, KeyPressEventArgs e)
    {
    }

    /// <inheritdoc />
    public bool KeyUp(Control c, KeyEventArgs e) => false;

    #endregion

    #region Source

    /// <inheritdoc />
    public void GotFocus(Control c)
    {
    }

    /// <inheritdoc />
    public void LostFocus(Control c)
    {
        _captured = false;
        _panning = false;
        _moving = false;
        _panEnd();
        _endMove();
        _setHot(-1);
    }

    #endregion
}

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Process mouse/keyboard events for a knob control.
/// </summary>
public class KnobController : GlobalId,
    IMouseController,
    IKeyController,
    ISourceController
{
    #region Instance Fields
    private readonly ViewDrawKnob _drawKnob;
    private bool _captured;
    private bool _mouseOver;
    private bool _rotating;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KnobController class.
    /// </summary>
    /// <param name="drawKnob">Associated drawing element.</param>
    public KnobController(ViewDrawKnob drawKnob) => _drawKnob = drawKnob;

    #endregion

    #region Mouse Notifications
    /// <inheritdoc />
    public virtual void MouseEnter(Control c)
    {
        _mouseOver = true;
        UpdateTargetState();
    }

    /// <inheritdoc />
    public virtual void MouseMove(Control c, Point pt)
    {
        if (_rotating)
        {
            c.Cursor = Cursors.Hand;
            var newValue = _drawKnob.GetValueFromPosition(pt);
            if (newValue != _drawKnob.Value)
            {
                _drawKnob.ScrollValue = newValue;
            }
        }
    }

    /// <inheritdoc />
    public virtual bool MouseDown(Control c, Point pt, MouseButtons button)
    {
        if (button == MouseButtons.Left && _drawKnob.ContainsKnobPoint(pt))
        {
            _captured = true;
            _rotating = true;
            UpdateTargetState();
        }

        return _captured;
    }

    /// <inheritdoc />
    public virtual void MouseUp(Control c, Point pt, MouseButtons button)
    {
        if (_captured)
        {
            _captured = false;
            _rotating = false;
            c.Cursor = Cursors.Default;

            if (_drawKnob.ContainsKnobPoint(pt))
            {
                _drawKnob.ScrollValue = _drawKnob.GetValueFromPosition(pt);
            }

            UpdateTargetState();
        }
    }

    /// <inheritdoc />
    public virtual void MouseLeave(Control c, ViewBase? next)
    {
        _mouseOver = false;
        _rotating = false;
        _captured = false;
        c.Cursor = Cursors.Default;
        UpdateTargetState();
    }

    /// <inheritdoc />
    public virtual void DoubleClick(Point pt)
    {
    }

    /// <inheritdoc />
    public virtual bool IgnoreVisualFormLeftButtonDown => false;

    #endregion

    #region Key Notifications
    /// <inheritdoc />
    public virtual void KeyDown(Control c, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Up or Keys.Right)
        {
            if (_drawKnob.Value < _drawKnob.Maximum)
            {
                _drawKnob.ScrollValue = _drawKnob.Value + 1;
            }
        }
        else if (e.KeyCode is Keys.Down or Keys.Left)
        {
            if (_drawKnob.Value > _drawKnob.Minimum)
            {
                _drawKnob.ScrollValue = _drawKnob.Value - 1;
            }
        }
    }

    /// <inheritdoc />
    public virtual void KeyPress(Control c, KeyPressEventArgs e)
    {
    }

    /// <inheritdoc />
    public virtual bool KeyUp(Control c, KeyEventArgs e) => _captured;

    #endregion

    #region Source Notifications
    /// <inheritdoc />
    public virtual void GotFocus(Control c)
    {
    }

    /// <inheritdoc />
    public virtual void LostFocus(Control c)
    {
        if (_captured)
        {
            c.Capture = false;
            _captured = false;
            _rotating = false;
        }
    }
    #endregion

    #region Implementation
    private void UpdateTargetState()
    {
        var newState = PaletteState.Normal;

        if (_mouseOver)
        {
            newState = _captured ? PaletteState.Pressed : PaletteState.Tracking;
        }

        if (_drawKnob.ElementState != newState)
        {
            _drawKnob.ElementState = newState;
            _drawKnob.PerformNeedPaint(false);
        }
    }
    #endregion
}

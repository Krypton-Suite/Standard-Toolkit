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
/// Process mouse, keyboard, and focus events for <see cref="KryptonRating"/>.
/// </summary>
internal class RatingController : GlobalId,
    IMouseController,
    IKeyController,
    ISourceController
{
    #region Instance Fields

    private readonly ViewDrawRating _drawRating;
    private bool _captured;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="RatingController"/> class.
    /// </summary>
    /// <param name="drawRating">Associated drawing element.</param>
    public RatingController(ViewDrawRating drawRating) => _drawRating = drawRating;

    #endregion

    #region Mouse Notifications

    /// <inheritdoc />
    public void MouseEnter(Control c)
    {
    }

    /// <inheritdoc />
    public void MouseMove(Control c, Point pt) => _drawRating.OnMouseMove(pt);

    /// <inheritdoc />
    public bool MouseDown(Control c, Point pt, MouseButtons button)
    {
        if (button == MouseButtons.Left)
        {
            _captured = true;
            _drawRating.OnMouseDown(pt);
        }

        return _captured;
    }

    /// <inheritdoc />
    public void MouseUp(Control c, Point pt, MouseButtons button)
    {
        if (_captured)
        {
            _captured = false;
        }
    }

    /// <inheritdoc />
    public void MouseLeave(Control c, ViewBase? next) => _drawRating.OnMouseLeave();

    /// <inheritdoc />
    public void DoubleClick(Point pt) => _drawRating.OnMouseDown(pt);

    /// <inheritdoc />
    public bool IgnoreVisualFormLeftButtonDown => false;

    #endregion

    #region Key Notifications

    /// <inheritdoc />
    public void KeyDown(Control c, KeyEventArgs e) => _drawRating.OnKeyDown(e);

    /// <inheritdoc />
    public void KeyPress(Control c, KeyPressEventArgs e) => _drawRating.OnKeyPress(e);

    /// <inheritdoc />
    public bool KeyUp(Control c, KeyEventArgs e) => _captured;

    #endregion

    #region Source Notifications

    /// <inheritdoc />
    public void GotFocus(Control c) => _drawRating.OnGotFocus();

    /// <inheritdoc />
    public void LostFocus(Control c)
    {
        _captured = false;
        _drawRating.OnLostFocus();
    }

    #endregion
}

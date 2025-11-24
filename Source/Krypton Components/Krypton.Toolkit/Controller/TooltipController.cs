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

namespace Krypton.Toolkit;

/// <summary>
/// Snoop incoming mouse messages for an element and inform tooltip manager about them.
/// </summary>
public class ToolTipController : GlobalId,
    IMouseController
{
    #region Instance Fields
    private readonly ToolTipManager? _manager;
    private readonly ViewBase _targetElement;
    private readonly IMouseController? _targetController;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize an instance of the TooltipController class.
    /// </summary>
    /// <param name="manager">Reference to manager of all tooltip functionality.</param>
    /// <param name="targetElement">Target element that controller is for.</param>
    /// <param name="targetController">Target controller that we are snooping.</param>
    public ToolTipController([DisallowNull] ToolTipManager manager,
        [DisallowNull] ViewBase targetElement,
        IMouseController? targetController)
    {
        Debug.Assert(manager != null);
        Debug.Assert(targetElement != null);

        // Remember incoming references
        _manager = manager;
        _targetElement = targetElement!;
        _targetController = targetController;
    }
    #endregion

    #region IMouseController
    /// <summary>
    /// Mouse has entered the view.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    public void MouseEnter(Control c)
    {
        _manager?.MouseEnter(_targetElement, c);

        _targetController?.MouseEnter(c);
    }

    /// <summary>
    /// Mouse has moved inside the view.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="pt">Mouse position relative to control.</param>
    public void MouseMove(Control c, Point pt)
    {
        _manager?.MouseMove(_targetElement, c, pt);

        _targetController?.MouseMove(c, pt);
    }

    /// <summary>
    /// Mouse button has been pressed in the view.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="pt">Mouse position relative to control.</param>
    /// <param name="button">Mouse button pressed down.</param>
    /// <returns>True if capturing input; otherwise false.</returns>
    public bool MouseDown(Control c, Point pt, MouseButtons button)
    {
        _manager?.MouseDown(_targetElement, c, pt, button);

        return _targetController != null && _targetController.MouseDown(c, pt, button);
    }

    /// <summary>
    /// Mouse button has been released in the view.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="pt">Mouse position relative to control.</param>
    /// <param name="button">Mouse button released.</param>
    public void MouseUp(Control c, Point pt, MouseButtons button)
    {
        _manager?.MouseUp(_targetElement, c, pt, button);

        _targetController?.MouseUp(c, pt, button);
    }

    /// <summary>
    /// Mouse has left the view.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="next">Reference to view that is next to have the mouse.</param>
    public void MouseLeave(Control c, ViewBase? next)
    {
        _manager?.MouseLeave(_targetElement, c, next);

        _targetController?.MouseLeave(c, next);
    }

    /// <summary>
    /// Left mouse button double click.
    /// </summary>
    /// <param name="pt">Mouse position relative to control.</param>
    public void DoubleClick(Point pt)
    {
        _manager?.DoubleClick(_targetElement, pt);

        _targetController?.DoubleClick(pt);
    }

    /// <summary>
    /// Should the left mouse down be ignored when present on a visual form border area.
    /// </summary>
    public bool IgnoreVisualFormLeftButtonDown =>
        _targetController is { IgnoreVisualFormLeftButtonDown: true };

    #endregion
}
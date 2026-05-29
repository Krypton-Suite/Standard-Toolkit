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
/// Provide context title click functionality.
/// </summary>
internal class ContextTitleController : GlobalId,
    IMouseController
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private bool _mouseOver;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ContextTitleController class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon instance.</param>
    public ContextTitleController([DisallowNull] KryptonRibbon? ribbon)
    {
        Debug.Assert(ribbon != null);
        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));
    }
    #endregion

    #region ContextTabSet
    /// <summary>
    /// Gets and sets the associated context tab set.
    /// </summary>
    public ContextTabSet? ContextTabSet { get; set; }

    #endregion

    #region Mouse Notifications
    /// <summary>
    /// Mouse has entered the view.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    public virtual void MouseEnter(Control c) =>
        // Mouse is over the target
        _mouseOver = true;

    /// <summary>
    /// Mouse has moved inside the view.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="pt">Mouse position relative to control.</param>
    public virtual void MouseMove(Control c, Point pt)
    {
    }

    /// <summary>
    /// Mouse button has been pressed in the view.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="pt">Mouse position relative to control.</param>
    /// <param name="button">Mouse button pressed down.</param>
    /// <returns>True if capturing input; otherwise false.</returns>
    public virtual bool MouseDown(Control c, Point pt, MouseButtons button)
    {
        // Only interested if the mouse is over the element
        if (_mouseOver)
        {
            // Only interested in left mouse pressing down
            if (button == MouseButtons.Left)
            {
                if (ContextTabSet != null)
                {
                    // We do not operate the context selection at design time
                    if (_ribbon is { InDesignMode: false, Enabled: true })
                    {
                        // Select the first tab in the context
                        ContextTabSet.FirstTab.RibbonTab!.Ribbon!.SelectedTab = ContextTabSet.FirstTab.RibbonTab;
                    }
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Mouse button has been released in the view.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="pt">Mouse position relative to control.</param>
    /// <param name="button">Mouse button released.</param>
    public virtual void MouseUp(Control c, Point pt, MouseButtons button)
    {
    }

    /// <summary>
    /// Mouse has left the view.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="next">Reference to view that is next to have the mouse.</param>
    public virtual void MouseLeave(Control c, ViewBase? next) =>
        // Mouse is no longer over the target
        _mouseOver = false;

    /// <summary>
    /// Left mouse button double click.
    /// </summary>
    /// <param name="pt">Mouse position relative to control.</param>
    public virtual void DoubleClick(Point pt)
    {
    }

    /// <summary>
    /// Should the left mouse down be ignored when present on a visual form border area.
    /// </summary>
    public virtual bool IgnoreVisualFormLeftButtonDown => false;

    #endregion
}
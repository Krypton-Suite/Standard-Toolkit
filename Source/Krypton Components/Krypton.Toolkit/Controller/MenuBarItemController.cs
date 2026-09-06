#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Mouse controller for a top-level <see cref="KryptonMenuBar"/> item.
/// Opens on mouse down and, while a drop-down is showing, switches to the item under the mouse.
/// </summary>
internal sealed class MenuBarItemController : ButtonController
{
    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="MenuBarItemController"/> class.
    /// </summary>
    /// <param name="menuBar">Owning menu bar.</param>
    /// <param name="item">Top-level item.</param>
    /// <param name="target">View element to control.</param>
    /// <param name="needPaint">Paint notification delegate.</param>
    public MenuBarItemController(KryptonMenuBar menuBar,
        KryptonContextMenuItem item,
        ViewBase target,
        NeedPaintHandler needPaint)
        : base(target, needPaint)
    {
        MenuBar = menuBar;
        Item = item;
        ClickOnDown = true;
        BecomesFixed = true;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the owning menu bar.
    /// </summary>
    public KryptonMenuBar MenuBar { get; }

    /// <summary>
    /// Gets the top-level item.
    /// </summary>
    public KryptonContextMenuItem Item { get; }

    #endregion

    #region Mouse

    /// <inheritdoc />
    public override void MouseEnter(Control c)
    {
        base.MouseEnter(c);
        MenuBar.OnItemMouseEnter(this);
    }

    #endregion
}

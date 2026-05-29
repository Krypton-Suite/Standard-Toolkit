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
/// Process mouse events for an application menu based button spec button.
/// </summary>
internal class ButtonSpecAppButtonController : ButtonController,
    IContextMenuTarget
{
    #region Instance Fields
    private readonly ViewDrawButton _target;
    private readonly ViewContextMenuManager _viewManager;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonSpecAppButtonController class.
    /// </summary>
    /// <param name="viewManager">Owning view manager instance.</param>
    /// <param name="target">Target for state changes.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ButtonSpecAppButtonController(ViewContextMenuManager viewManager,
        ViewDrawButton target,
        NeedPaintHandler needPaint)
        : base(target, needPaint)
    {
        _target = target;
        _viewManager = viewManager;
    }
    #endregion

    #region Key Notifications
    /// <summary>
    /// Key has been pressed down.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="e">A KeyEventArgs that contains the event data.</param>
    public override void KeyDown([DisallowNull] Control? c, [DisallowNull] KeyEventArgs? e)
    {
        Debug.Assert(c is not null);
        Debug.Assert(e is not null);

        // Validate incoming references
        if (c is null)
        {
            throw new ArgumentNullException(nameof(c));
        }

        if (e is null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        switch (e.KeyCode)
        {
            case Keys.Tab:
                _viewManager.KeyTab(e.Shift);
                break;
            case Keys.Home:
                _viewManager.KeyHome();
                break;
            case Keys.End:
                _viewManager.KeyEnd();
                break;
            case Keys.Up:
                _viewManager.KeyUp();
                break;
            case Keys.Down:
                _viewManager.KeyDown();
                break;
            case Keys.Left:
                _viewManager.KeyLeft(true);
                break;
            case Keys.Right:
                _viewManager.KeyRight();
                break;
            default:
                base.KeyDown(c, e);
                break;
        }
    }

    /// <summary>
    /// Key has been pressed.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
    public override void KeyPress([DisallowNull] Control c, [DisallowNull] KeyPressEventArgs e)
    {
        Debug.Assert(c != null);
        Debug.Assert(e != null);

        // Validate incoming references
        if (c == null)
        {
            throw new ArgumentNullException(nameof(c));
        }
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        _viewManager.KeyMnemonic(e.KeyChar);
    }
    #endregion

    #region IContextMenuTarget
    /// <summary>
    /// Returns if the item shows a sub menu when selected.
    /// </summary>
    public bool HasSubMenu => false;

    /// <summary>
    /// This target should display as the active target.
    /// </summary>
    public void ShowTarget()
    {
    }

    /// <summary>
    /// This target should clear any active display.
    /// </summary>
    public void ClearTarget()
    {
    }

    /// <summary>
    /// This target should show any appropriate sub menu.
    /// </summary>
    public void ShowSubMenu()
    {
    }

    /// <summary>
    /// This target should remove any showing sub menu.
    /// </summary>
    public void ClearSubMenu()
    {
    }

    /// <summary>
    /// Determine if the keys value matches the mnemonic setting for this target.
    /// </summary>
    /// <param name="charCode">Key code to test against.</param>
    /// <returns>True if a match is found; otherwise false.</returns>
    public bool MatchMnemonic(char charCode) => Control.IsMnemonic(charCode, _target.ButtonValues.GetShortText());

    /// <summary>
    /// Activate the item because of a mnemonic key press.
    /// </summary>
    public void MnemonicActivate() => OnClick(new MouseEventArgs(MouseButtons.None, 1, 0, 0, 0));

    /// <summary>
    /// Gets the view element that should be used when this target is active.
    /// </summary>
    /// <returns>View element to become active.</returns>
    public ViewBase GetActiveView() => _target;

    /// <summary>
    /// Get the client rectangle for the display of this target.
    /// </summary>
    public Rectangle ClientRectangle => _target.ClientRectangle;

    /// <summary>
    /// Should a mouse down at the provided point cause the currently stacked context menu to become current.
    /// </summary>
    /// <param name="pt">Client coordinates point.</param>
    /// <returns>True to become current; otherwise false.</returns>
    public bool DoesStackedClientMouseDownBecomeCurrent(Point pt) => true;

    #endregion
}
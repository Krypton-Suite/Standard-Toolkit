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
/// Controller for a VisualContextMenu popup.
/// </summary>
public class ContextMenuController : GlobalId,
    IKeyController
{
    #region Instance Fields
    private readonly ViewContextMenuManager? _viewManager;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ContextMenuController class.
    /// </summary>
    /// <param name="viewManager">Owning view manager instance.</param>
    public ContextMenuController(ViewContextMenuManager? viewManager) => _viewManager = viewManager;

    #endregion

    #region Key Notifications

    /// <summary>
    /// Key has been pressed down.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="e">A KeyEventArgs that contains the event data.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public virtual void KeyDown([DisallowNull] Control c, [DisallowNull] KeyEventArgs e)
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

        switch (e.KeyCode)
        {
            case Keys.Tab:
                _viewManager?.KeyTab(e.Shift);
                break;
            case Keys.Home:
                _viewManager?.KeyHome();
                break;
            case Keys.End:
                _viewManager?.KeyEnd();
                break;
            case Keys.Up:
                _viewManager?.KeyUp();
                break;
            case Keys.Down:
                _viewManager?.KeyDown();
                break;
            case Keys.Left:
                _viewManager?.KeyLeft(false);
                break;
            case Keys.Right:
                _viewManager?.KeyRight();
                break;
        }
    }

    /// <summary>
    /// Key has been pressed.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public virtual void KeyPress([DisallowNull] Control c, [DisallowNull] KeyPressEventArgs e)
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

        _viewManager?.KeyMnemonic(e.KeyChar);
    }

    /// <summary>
    /// Key has been released.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    /// <param name="e">A KeyEventArgs that contains the event data.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>True if capturing input; otherwise false.</returns>
    public virtual bool KeyUp([DisallowNull] Control c, [DisallowNull] KeyEventArgs e)
    {
        Debug.Assert(c != null);
        Debug.Assert(e != null);

        // Validate incoming references
        if (c == null)
        {
            throw new ArgumentNullException(nameof(c));
        }

        return e == null ? throw new ArgumentNullException(nameof(e)) : false;
    }
    #endregion
}
#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonSystemMenuListener : NativeWindow
{
    /*
     * What needs to be intercepted
     *      Right Mouse click in the title bar (the menu shows on the position of the mouse cursor)
     *      Left Mouse click on the ControlBox (the menu shows on the position of the ControlBox)
     *      ALT + Space (the menu shows on the position of the ControlBox)
     */

    #region Events
    public event Action? KeyAltSpaceDown;
    public event Action? NCRightMouseButtonDown;
    public event Action? NCLeftMouseButtonDown;
    #endregion

    #region Fields
    private readonly KryptonForm _form;
    private ViewDrawDocker _drawHeading;
    #endregion

    #region Identity
    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="kryptonForm">The instance the KryptonSystemMenu is operating on.</param>
    /// <param name="drawHeading">Heading component for title bar detection.</param>
    public KryptonSystemMenuListener(KryptonForm kryptonForm, ViewDrawDocker drawHeading)
    {
        _form = kryptonForm;
        _drawHeading = drawHeading;
    }
    #endregion

    #region Public
    /// <summary>
    /// Stop listening for mouse and keyboard events that trigger the system menu.
    /// </summary>
    public void DisableListener()
    {
        if (Handle != IntPtr.Zero)
        {
            ReleaseHandle();
        }
    }

    /// <summary>
    /// Enable listening for mouse and keyboard events that trigger the system menu.
    /// </summary>
    public void EnableListener()
    {
        if (Handle == IntPtr.Zero)
        {
            AssignHandle(_form.Handle);
        }
    }

    /// <inheritdoc/>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override void ReleaseHandle()
    {
        // Retain default behaviour, but hide this method from the editor

        base.ReleaseHandle();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public new void AssignHandle(IntPtr handle)
    {
        // Retain default behaviour, but hide this method from the editor

        base.AssignHandle(handle);
    }
    #endregion

    #region Protected (override)
    protected override void WndProc(ref Message m)
    {
        if (m.Msg == PI.WM_.NCRBUTTONDOWN)
        {
            // Intercept Non Client Area Right Mouse Down
            // But it needs to know if the click happens on the title bar, otherwise
            // the message must be forwarded.
            
            //if (some where in the title bar)
            {

                OnNCRightMouseButtonDown();

                // eat the message....????
                return;
            }
        }
        else if (m.Msg == PI.WM_.NCLBUTTONDOWN)
        {
            // Intercept Non Client Area Left Mouse Down.
            // But it needs to know if the ControlBox is Clicked
            //if (_form.SystemMenuValues.)
            {
                OnNCLeftMouseButtonDown();
            }
        }
        else if ((m.Msg & PI.WM_.SYSKEYDOWN) == PI.WM_.SYSKEYDOWN || (m.WParam.ToInt32() & PI.WM_.KEYDOWN) == PI.WM_.KEYDOWN)
        {
            if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)
            {
                if (m.WParam.ToInt32() == (int)Keys.Space)
                {
                    // Intercept ALT + SPACE
                    OnKeyAltSpaceDown();

                    // eat the message
                    return;
                }
            }
        }

        base.WndProc(ref m);
    }
    #endregion

    #region Private
    private void OnNCLeftMouseButtonDown()
    {
        Debug.Print($"OnNCLeftMouseButtonDown");
        NCLeftMouseButtonDown?.Invoke();
    }

    private void OnNCRightMouseButtonDown()
    {
        Debug.Print($"OnNCRightMouseButtonDown");
        NCRightMouseButtonDown?.Invoke();
    }

    private void OnKeyAltSpaceDown()
    {
        Debug.Print($"OnKeyAltSpaceDown");
        KeyAltSpaceDown?.Invoke();
    }
    #endregion
}

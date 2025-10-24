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
/// Used to obscure an area of the screen to hide form changes underneath.
/// </summary>
[ToolboxItem(false)]
public class ScreenObscurer : IDisposable
{
    #region ObscurerForm
    private class ObscurerForm : Form
    {
        #region Identity
        public ObscurerForm()
        {
            // Prevent automatic positioning of the window
            StartPosition = FormStartPosition.Manual;
            Location = new Point(-int.MaxValue, -int.MaxValue);
            Size = Size.Empty;

            // We do not want any window chrome
            FormBorderStyle = FormBorderStyle.None;

            // We do not want a taskbar entry for this temporary window
            ShowInTaskbar = false;
        }
        #endregion

        #region Public
        public void ShowForm(Rectangle screenRect)
        {
            // Our initial position should overlay exactly the container
            SetBounds(screenRect.X, 
                screenRect.Y,
                screenRect.Width, 
                screenRect.Height);

            // Show the window without activating it (i.e. do not take focus)
            PI.ShowWindow(Handle, PI.ShowWindowCommands.SW_SHOWNOACTIVATE);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the PaintBackground event.
        /// </summary>
        /// <param name="e">An PaintEventArgs containing the event data.</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // We do nothing, so the area underneath shows through
        }

        /// <summary>
        /// Raises the Paint event.
        /// </summary>
        /// <param name="e">An PaintEventArgs containing the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // We do nothing, so the area underneath shows through
        }
        #endregion
    }
    #endregion

    #region Static Fields
    private ObscurerForm? _obscurer;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ControlObscurer class.
    /// </summary>
    public ScreenObscurer()
    {
        // First time needed, create the top level obscurer window
        _obscurer ??= new ObscurerForm();
    }

    /// <summary>
    /// Initialize a new instance of the ControlObscurer class.
    /// </summary>
    /// <param name="f">Form to obscure.</param>
    /// <param name="designMode">Is the source in design mode.</param>
    public ScreenObscurer(Form f, bool designMode)
    {
        // Check the incoming form is valid
        if (f is { IsDisposed: false } && !designMode)
        {
            // First time needed, create the top level obscurer window
            _obscurer ??= new ObscurerForm();

            // We need a control to work with!
            if (f != null)
            {
                _obscurer.ShowForm(f.Bounds);
            }
        }
    }

    /// <summary>
    /// Initialize a new instance of the ControlObscurer class.
    /// </summary>
    /// <param name="c">Control to obscure.</param>
    /// <param name="designMode">Is the source in design mode.</param>
    public ScreenObscurer(Control c, bool designMode)
    {
        // Check the incoming control is valid
        if (c is { IsDisposed: false } && !designMode)
        {
            // First time needed, create the top level obscurer window
            _obscurer ??= new ObscurerForm();

            // We need a control to work with!
            if (c != null)
            {
                _obscurer.ShowForm(c.RectangleToScreen(c.ClientRectangle));
            }
        }
    }

    /// <summary>
    /// Use the obscurer to cover the provided control.
    /// </summary>
    /// <param name="f">Form to obscure.</param>
    public void Cover(Form f)
    {
        // Check the incoming form is valid
        if (f is { IsDisposed: false })
        {
            // Show over top of the provided form
            _obscurer?.ShowForm(f.Bounds);
        }
    }

    /// <summary>
    /// Use the obscurer to cover the provided control.
    /// </summary>
    /// <param name="c">Control to obscure.</param>
    public void Cover(Control c)
    {
        // Check the incoming control is valid
        if (c is { IsDisposed: false })
        {
            // Show over top of the provided control
            _obscurer?.ShowForm(c.RectangleToScreen(c.ClientRectangle));
        }
    }

    /// <summary>
    /// If covering an area then uncover it now.
    /// </summary>
    public void Uncover() => _obscurer?.Hide();

    /// <summary>
    /// Hide the obscurer from display.
    /// </summary>
    public void Dispose()
    {
        if (_obscurer != null)
        {
            _obscurer.Hide();
            _obscurer.Dispose();
            _obscurer = null;
        }
        GC.SuppressFinalize(this);
    }
    #endregion
}
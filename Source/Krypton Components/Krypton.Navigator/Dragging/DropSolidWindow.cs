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

namespace Krypton.Navigator;

/// <summary>
/// Draws a semi-transparent window to indicate a drop rectangle.
/// </summary>
public class DropSolidWindow : KryptonForm
{
    #region Instance Fields
    private readonly IPaletteDragDrop _paletteDragDrop;
    private readonly IRenderer _renderer;
    private Rectangle _solidRect;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the DropSolidWindow class.
    /// </summary>
    /// <param name="paletteDragDrop">Drawing palette.</param>
    /// <param name="renderer">Drawing renderer.</param>
    public DropSolidWindow(IPaletteDragDrop paletteDragDrop, IRenderer renderer)
    {
        SetInheritedControlOverride();
        _paletteDragDrop = paletteDragDrop;
        _renderer = renderer;

        FormBorderStyle = FormBorderStyle.None;
        SizeGripStyle = SizeGripStyle.Hide;
        StartPosition = FormStartPosition.Manual;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        BackColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
        TransparencyKey = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
        Opacity = _paletteDragDrop.GetDragDropSolidOpacity();
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Show the window without taking activation.
    /// </summary>
    public void ShowWithoutActivate() =>
        // Show the window without activating it (i.e. do not take focus)
        PI.ShowWindow(Handle, PI.ShowWindowCommands.SW_SHOWNOACTIVATE);

    /// <summary>
    /// Gets and sets the new solid rectangle area.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Rectangle SolidRect
    {
        get => _solidRect;

        set
        {
            if (_solidRect != value)
            {
                _solidRect = value;

                var area = Screen.GetWorkingArea(this);

                var bounds = new Rectangle(value.Location - (Size)area.Location, value.Size);

                DesktopBounds = bounds;

                Refresh();
            }
        }
    }
    #endregion

    #region Protected
    /// <summary>
    /// Raises the Paint event.
    /// </summary>
    /// <param name="e">A PaintEventArgs with event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
        // Let base class do its stuff
        base.OnPaint(e);

        // If we have a solid rectangle to draw
        if (!SolidRect.IsEmpty
            && (_renderer != null)
           )
        {
            using var context = new RenderContext(this, e.Graphics, e.ClipRectangle, _renderer);
            _renderer.RenderGlyph.DrawDragDropSolidGlyph(context, ClientRectangle, _paletteDragDrop);
        }
    }

    /// <summary>
    /// Processes Windows messages.
    /// </summary>
    /// <param name="m">The Windows Message to process. </param>
    protected override void WndProc(ref Message m)
    {
        // We are a transparent window, so mouse is never over us
        if (m.Msg == PI.WM_.NCHITTEST)
        {
            // Allow actions to occur to window beneath us
            m.Result = (IntPtr)PI.HT.TRANSPARENT;
        }
        else
        {
            base.WndProc(ref m);
        }
    }
    #endregion
}
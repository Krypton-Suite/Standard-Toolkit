#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, Lesandro and tobitege et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Draws a semi-transparent drop rectangle indicating where a detached ribbon will reattach on the parent window.
/// </summary>
[ToolboxItem(false)]
[DesignTimeVisible(false)]
internal class VisualRibbonDropSolidWindow : KryptonForm
{
    #region Instance Fields
    private readonly IPaletteDragDrop _paletteDragDrop;
    private readonly IRenderer _renderer;
    private Rectangle _solidRect;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the VisualRibbonDropSolidWindow class.
    /// </summary>
    /// <param name="paletteDragDrop">Drawing palette.</param>
    /// <param name="renderer">Drawing renderer.</param>
    public VisualRibbonDropSolidWindow(IPaletteDragDrop paletteDragDrop, IRenderer renderer)
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
        BackColor = SharedStaticVariables.TRANSPARENCY_KEY_COLOR;
        TransparencyKey = SharedStaticVariables.TRANSPARENCY_KEY_COLOR;
        Opacity = _paletteDragDrop.GetDragDropSolidOpacity();
    }
    #endregion

    #region Public
    /// <summary>
    /// Show the window without taking activation.
    /// </summary>
    public void ShowWithoutActivate() =>
        PI.ShowWindow(Handle, PI.ShowWindowCommands.SW_SHOWNOACTIVATE);

    /// <summary>
    /// Gets and sets the solid rectangle area in screen coordinates.
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

                Rectangle bounds;
                if (value.IsEmpty)
                {
                    bounds = new Rectangle(SharedStaticConstants.OFF_SCREEN_POSITION, SharedStaticConstants.OFF_SCREEN_POSITION, 0, 0);
                }
                else
                {
                    var area = Screen.GetWorkingArea(this);
                    bounds = new Rectangle(value.Location - (Size)area.Location, value.Size);
                }

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
        base.OnPaint(e);

        if (!SolidRect.IsEmpty && _renderer != null)
        {
            using var context = new RenderContext(this, e.Graphics, e.ClipRectangle, _renderer);
            _renderer.RenderGlyph.DrawDragDropSolidGlyph(context, ClientRectangle, _paletteDragDrop);
        }
    }

    /// <summary>
    /// Processes Windows messages.
    /// </summary>
    /// <param name="m">The Windows Message to process.</param>
    protected override void WndProc(ref Message m)
    {
        if (m.Msg == PI.WM_.NCHITTEST)
        {
            m.Result = (IntPtr)PI.HT.TRANSPARENT;
        }
        else
        {
            base.WndProc(ref m);
        }
    }
    #endregion
}

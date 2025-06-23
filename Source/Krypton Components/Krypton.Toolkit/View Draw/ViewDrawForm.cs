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
/// Extends the ViewDrawDocker by adding status strip merging into the border.
/// </summary>
public class ViewDrawForm : ViewDrawDocker
{
    #region Instance Fields
    private StatusStrip _renderStrip;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawForm class.
    /// </summary>
    /// <param name="paletteBack">Palette source for the background.</param>        
    /// <param name="paletteBorder">Palette source for the border.</param>
    public ViewDrawForm(IPaletteBack paletteBack,
        IPaletteBorder paletteBorder)
        : base(paletteBack, paletteBorder) =>
        // Create a status strip we can position for rendering
        _renderStrip = new StatusStrip();

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_renderStrip != null!)
            {
                _renderStrip.Dispose();
                _renderStrip = null!;
            }
        }

        base.Dispose(disposing);
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawForm:{Id}";

    #endregion

    #region StatusStrip
    /// <summary>
    /// Gets and sets the status strip to render.
    /// </summary>
    public StatusStrip? StatusStrip { get; set; }

    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering after child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderAfter(RenderContext context)
    {
        // Do we have a status strip to try and merge?
        // Is the status strip using the global renderer?
        if (StatusStrip?.RenderMode == ToolStripRenderMode.ManagerRenderMode)
        {
            // Cast to correct type
            if (context.Control is KryptonForm form)
            {
                // Find the size of the borders around the form
                Padding borders = form.RealWindowBorders;

                // Grab the global renderer to use for painting
                ToolStripRenderer renderer = ToolStripManager.Renderer;

                // Size the render strip to the apparent size when merged into borders
                _renderStrip.Width = form.Width - borders.Left - borders.Right;
                _renderStrip.Height = StatusStrip.Height;// + borders.Bottom;

                // Find vertical start of the status strip
                var y = StatusStrip.Top + borders.Top;

                if (context.Graphics != null)
                {
                    try
                    {
                        // We need to transform downwards from drawing at 0,0 to actual required position
                        context.Graphics.TranslateTransform(borders.Left, y);

                        // Use the tool strip renderer to draw the correct status strip border/background
                        renderer.DrawToolStripBorder(new ToolStripRenderEventArgs(context.Graphics, _renderStrip));
                        renderer.DrawToolStripBackground(
                            new ToolStripRenderEventArgs(context.Graphics, _renderStrip));
                    }
                    finally
                    {
                        // Make sure that even a crash in the renderer does not prevent the transform reversal
                        context.Graphics.TranslateTransform(-borders.Left, -y);
                    }
                }
            }
        }

        // Finally we let the border be drawn
        base.RenderAfter(context);
    }
    #endregion
}
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
/// Encapsulates context for view render operations.
/// </summary>
public class RenderContext : ViewContext
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewContext class.
    /// </summary>
    /// <param name="control">Control associated with rendering.</param>
    /// <param name="graphics">Graphics instance for drawing.</param>
    /// <param name="clipRect">Rectangle that needs rendering.</param>
    /// <param name="renderer">Rendering provider.</param>
    public RenderContext(Control? control,
        Graphics? graphics,
        Rectangle clipRect,
        IRenderer? renderer)
        : this(null, control, control, graphics, clipRect, renderer)
    {
    }

    /// <summary>
    /// Initialize a new instance of the ViewContext class.
    /// </summary>
    /// <param name="control">Control associated with rendering.</param>
    /// <param name="alignControl">Control used to align elements.</param>
    /// <param name="graphics">Graphics instance for drawing.</param>
    /// <param name="clipRect">Rectangle that needs rendering.</param>
    /// <param name="renderer">Rendering provider.</param>
    public RenderContext(Control? control,
        Control? alignControl,
        Graphics? graphics,
        Rectangle clipRect,
        IRenderer? renderer)
        : this(null, control, alignControl, graphics, clipRect, renderer)
    {
    }

    /// <summary>
    /// Initialize a new instance of the ViewContext class.
    /// </summary>
    /// <param name="manager">Reference to the view manager.</param>
    /// <param name="control">Control associated with rendering.</param>
    /// <param name="alignControl">Control used to align elements.</param>
    /// <param name="graphics">Graphics instance for drawing.</param>
    /// <param name="clipRect">Rectangle that needs rendering.</param>
    /// <param name="renderer">Rendering provider.</param>
    public RenderContext(ViewManager? manager,
        Control? control,
        Control? alignControl,
        Graphics? graphics,
        Rectangle clipRect,
        IRenderer? renderer)
        : base(manager, control, alignControl, graphics, renderer) =>
        ClipRect = clipRect;

    #endregion

    #region Public
    /// <summary>
    /// Gets the rectangle that needs rendering.
    /// </summary>
    public Rectangle ClipRect { get; }

    /// <summary>
    /// Calculate a rectangle in control coordinates that is aligned for gradient drawing.
    /// </summary>
    /// <param name="align">How to align the gradient.</param>
    /// <param name="local">Rectangle of the local element.</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <returns></returns>
    public Rectangle GetAlignedRectangle(PaletteRectangleAlign align, Rectangle local)
    {
        switch (align)
        {
            case PaletteRectangleAlign.Local:
                // Gradient should cover just the local view element itself
                local.Inflate(2, 2);
                return local;
            case PaletteRectangleAlign.Control:
                Rectangle clientRect = (AlignControl == Control)
                    ? Control!.ClientRectangle
                    : Control!.RectangleToClient(AlignControl!.RectangleToScreen(AlignControl.ClientRectangle));

                clientRect.Inflate(2, 2);
                return clientRect;
            case PaletteRectangleAlign.Form:
                // Gradient should cover the owning control (most likely a Form)
                Rectangle formRect = Control!.RectangleToClient(TopControl!.RectangleToScreen(AlignControl!.ClientRectangle));
                formRect.Inflate(2, 2);
                return formRect;
            case PaletteRectangleAlign.Inherit:
            default:
                // Should never call this routine with inherit value
                Debug.Assert(false);
                throw new ArgumentOutOfRangeException(nameof(align));
        }
    }
    #endregion
}
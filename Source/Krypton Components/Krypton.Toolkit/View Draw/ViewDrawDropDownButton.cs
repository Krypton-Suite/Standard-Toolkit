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
/// Draws a drop-down button using the provided renderer.
/// </summary>
public class ViewDrawDropDownButton : ViewLeaf
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawDropDownButton class.
    /// </summary>
    public ViewDrawDropDownButton(IPaletteContent palette)
    {
        Palette = palette;
        Orientation = VisualOrientation.Top;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawDropDownButton:{Id}";

    #endregion

    #region Palette
    /// <summary>
    /// Gets and sets the palette to use.
    /// </summary>
    public IPaletteContent Palette { get; }

    #endregion

    #region Orientation
    /// <summary>
    /// Gets and sets the orientation of the drop-down button.
    /// </summary>
    public VisualOrientation Orientation { get; set; }

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context is not null);

        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // Ask the renderer for the required size of the drop-down button
        return context.Renderer.RenderGlyph.GetDropDownButtonPreferredSize(context, Palette, State, Orientation);
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Validate incoming reference
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // We take on all the available display area
        ClientRectangle = context.DisplayRectangle;
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore( [DisallowNull] RenderContext context)
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        context.Renderer.RenderGlyph.DrawDropDownButton(context,
            ClientRectangle,
            Palette,
            State,
            Orientation);
    }

    #endregion
}
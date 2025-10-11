#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Draws a drop-down button using the provided renderer.
/// </summary>
public class ViewDrawDropDownButton : ViewLeaf
{
    #region Instance Fields
    private Image? _customGlyph;
    #endregion

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

    /// <summary>
    /// Gets or sets a custom image to render instead of the default arrow glyph.
    /// When null, the renderer-drawn arrow is used.
    /// </summary>
    public Image? CustomGlyph
    {
        get => _customGlyph;
        set => _customGlyph = value;
    }

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

        // If a custom glyph is provided, prefer its size
        if (_customGlyph != null)
        {
            return _customGlyph.Size;
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
    /// Allows to render a custom glyph, if set.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore( [DisallowNull] RenderContext context)
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        if (_customGlyph != null)
        {
            Size gsz = _customGlyph.Size;
            int x = ClientRectangle.X + (ClientRectangle.Width - gsz.Width) / 2;
            int y = ClientRectangle.Y + (ClientRectangle.Height - gsz.Height) / 2;
            context.Graphics.DrawImage(_customGlyph, new Rectangle(new Point(x, y), gsz));
        }
        else
        {
            context.Renderer.RenderGlyph.DrawDropDownButton(context,
                ClientRectangle,
                Palette,
                State,
                Orientation);
        }
    }

    #endregion
}
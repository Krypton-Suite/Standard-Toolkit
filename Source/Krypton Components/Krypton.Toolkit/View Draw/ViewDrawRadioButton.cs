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
/// Draws a radio button using the provided renderer.
/// </summary>
public class ViewDrawRadioButton : ViewLeaf
{
    #region Instance Fields
    private readonly PaletteBase _palette;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRadioButton class.
    /// </summary>
    /// <param name="palette">Palette for source of drawing values.</param>
    public ViewDrawRadioButton([DisallowNull] PaletteBase palette)
    {
        Debug.Assert(palette != null);
        _palette = palette!;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawRadioButton:{Id}";

    #endregion

    #region CheckState
    /// <summary>
    /// Gets and sets the check state of the check box.
    /// </summary>
    public bool CheckState { get; set; }

    #endregion

    #region Tracking
    /// <summary>
    /// Gets and sets the tracking state of the check box.
    /// </summary>
    public bool Tracking { get; set; }

    #endregion

    #region Pressed
    /// <summary>
    /// Gets and sets the pressed state of the check box.
    /// </summary>
    public bool Pressed { get; set; }

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

        // Ask the renderer for the required size of the check box
        return context.Renderer.RenderGlyph.GetRadioButtonPreferredSize(context, _palette, 
            Enabled, CheckState, 
            Tracking, Pressed);
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
    public override void RenderBefore([DisallowNull] RenderContext context)
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        context.Renderer.RenderGlyph.DrawRadioButton(context, ClientRectangle,
            _palette, Enabled,
            CheckState, Tracking,
            Pressed);
    }

    #endregion
}
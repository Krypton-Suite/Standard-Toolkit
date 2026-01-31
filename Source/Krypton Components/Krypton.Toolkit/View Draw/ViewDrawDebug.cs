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
/// View element that has a preferred size and then draws a solid color, used for debugging.
/// </summary>
public class ViewDrawDebug : ViewLeaf
{
    #region Instance Fields
    private readonly Size _preferredSize;
    private readonly Color _color;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawDebug class.
    /// </summary>
    /// <param name="preferredSize">Preferred size.</param>
    /// <param name="color">Solid color to draw with.</param>
    public ViewDrawDebug(Size preferredSize, Color color)
    {
        // Remember the source information
        _preferredSize = preferredSize;
        _color = color;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawDebug:{Id}";

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);
        return _preferredSize;
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Use all the provided space

        // Always use the metric and ignore given space
        ClientRectangle = context!.DisplayRectangle;
    }
    #endregion

    #region Paint

    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public override void RenderBefore([DisallowNull] RenderContext context) 
    {
        Debug.Assert(context != null);

        // Validate incoming reference
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // Ignore renderer, we just draw using solid color for debugging purposes
        using var brush = new SolidBrush(_color);
        context.Graphics.FillRectangle(brush, ClientRectangle);
    }
    #endregion    
}
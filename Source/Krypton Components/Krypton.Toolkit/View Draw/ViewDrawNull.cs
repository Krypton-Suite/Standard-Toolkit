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
/// Draw a red rectangle in the location of the null element.
/// </summary>
public class ViewDrawNull : ViewLayoutNull
{
    #region Instance Fields
    private readonly Color _fillColor;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawNull class.
    /// </summary>
    /// <param name="fillColor">Color to fill drawing area.</param>
    public ViewDrawNull(Color fillColor) => _fillColor = fillColor;

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawNull:{Id}";

    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore(RenderContext context)
    {
        using var fillBrush = new SolidBrush(_fillColor);
        context.Graphics.FillRectangle(fillBrush, ClientRectangle);
    }
    #endregion
}
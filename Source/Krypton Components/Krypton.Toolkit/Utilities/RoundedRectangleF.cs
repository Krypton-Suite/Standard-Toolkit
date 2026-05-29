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
/// Code from: https://github.com/aalitor/AltoControls/blob/on-development/AltoControls/Helpers/RoundedRectangleF.cs
/// </summary>
public class RoundedRectangleF
{
    #region Variables
    private readonly float _x;
    private readonly float _y;
    private readonly float _width;
    private readonly float _height;
    private readonly GraphicsPath _graphicsPath;
    #endregion

    #region Properties
    /// <summary>Gets the path.</summary>
    /// <value>The path.</value>
    public GraphicsPath Path => _graphicsPath;

    /// <summary>Gets the rectangle.</summary>
    /// <value>The rectangle.</value>
    public RectangleF Rect => new RectangleF(_x, _y, _width, _height);

    #endregion

    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="RoundedRectangleF" /> class.</summary>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="radius">The radius.</param>
    /// <param name="x">The x axis.</param>
    /// <param name="y">The y axis.</param>
    public RoundedRectangleF(float width, float height, float radius, float x = 0, float y = 0)
    {
        _x = x;
        _y = y;
        _width = width;
        _height = height;
        _graphicsPath = new GraphicsPath();
        var rectF = Rect;
        if (radius <= 0)
        {
            _graphicsPath.AddRectangle(rectF);
            return;
        }

        var diameter = radius * 2;
        var size = new SizeF(diameter, diameter);
        var arc = new RectangleF(Rect.Location, size);

        // The border is made of up a quarter of a circle arc, in each corner
        // top left arc  
        _graphicsPath.AddArc(arc, 180, 90);

        // top right arc  
        arc.X = rectF.Right - diameter;
        _graphicsPath.AddArc(arc, 270, 90);

        // bottom right arc  
        arc.Y = rectF.Bottom - diameter;
        _graphicsPath.AddArc(arc, 0, 90);

        // bottom left arc 
        arc.X = rectF.Left;
        _graphicsPath.AddArc(arc, 90, 90);
            
        _graphicsPath.CloseFigure();

    }
    #endregion
}
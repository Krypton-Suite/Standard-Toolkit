#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *
 */
#endregion

namespace Krypton.Toolkit
{
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
        public GraphicsPath Path => _graphicsPath;

        public RectangleF Rect => new (_x, _y, _width, _height);

        #endregion

        #region Constructors
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

            // The border is made of up a quarter of a circle arc, in each corner
            _graphicsPath.AddArc(rectF.Left, rectF.Top, radius, radius, 180f, 90f);
            _graphicsPath.AddArc(rectF.Right - radius, rectF.Top, radius, radius, 270f, 90f);
            _graphicsPath.AddArc(rectF.Right - radius, rectF.Bottom - radius, radius, radius, 0f, 90f);
            _graphicsPath.AddArc(rectF.Left, rectF.Bottom - radius, radius, radius, 90f, 90f);

            _graphicsPath.CloseFigure();

        }
        #endregion
    }
}
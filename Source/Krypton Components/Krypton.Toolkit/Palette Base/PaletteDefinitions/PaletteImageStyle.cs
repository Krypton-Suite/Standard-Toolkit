#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Specifies the an image is aligned.
    /// </summary>
    [TypeConverter(typeof(PaletteImageStyleConverter))]
    public enum PaletteImageStyle
    {
        /// <summary>
        /// Specifies image style should be inherited.
        /// </summary>
        Inherit,

        /// <summary>
        /// Specifies the image is placed in the top left.
        /// </summary>
        TopLeft,

        /// <summary>
        /// Specifies the image is placed in the center at the top.
        /// </summary>
        TopMiddle,

        /// <summary>
        /// Specifies the image is placed in the top right.
        /// </summary>
        TopRight,

        /// <summary>
        /// Specifies the image is placed in the center at the left.
        /// </summary>
        CenterLeft,

        /// <summary>
        /// Specifies the image is placed in the center.
        /// </summary>
        CenterMiddle,

        /// <summary>
        /// Specifies the image is placed in the center at the right.
        /// </summary>
        CenterRight,

        /// <summary>
        /// Specifies the image is placed in the bottom left.
        /// </summary>
        BottomLeft,

        /// <summary>
        /// Specifies the image is placed in the center at the bottom.
        /// </summary>
        BottomMiddle,

        /// <summary>
        /// Specifies the image is placed in the bottom right.
        /// </summary>
        BottomRight,

        /// <summary>
        /// Specifies image should be stretch to fix area.
        /// </summary>
        Stretch,

        /// <summary>
        /// Specifies the image is tiled without flipping.
        /// </summary>
        Tile,

        /// <summary>
        /// Specifies the image is tiled with flip horizontally.
        /// </summary>
        TileFlipX,

        /// <summary>
        /// Specifies the image is tiled with flip vertically.
        /// </summary>
        TileFlipY,

        /// <summary>
        /// Specifies the image is tiled with flip horizontally and vertically.
        /// </summary>
        TileFlipXY
    }
}
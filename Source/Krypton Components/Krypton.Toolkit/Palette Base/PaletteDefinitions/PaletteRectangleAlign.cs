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
    /// Specifies how a display rectangle aligns.
    /// </summary>
    public enum PaletteRectangleAlign
    {
        /// <summary>
        /// Specifies alignment should be inherited.
        /// </summary>
        Inherit,

        /// <summary>
        /// Specifies the client area of the rendering item.
        /// </summary>
        Local,

        /// <summary>
        /// Specifies the client area of the Control.
        /// </summary>
        Control,

        /// <summary>
        /// Specifies the client area of the owning Form.
        /// </summary>
        Form
    }
}
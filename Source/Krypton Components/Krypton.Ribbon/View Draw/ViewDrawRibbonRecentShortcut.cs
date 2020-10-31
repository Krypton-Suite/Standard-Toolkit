// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

using System;
using System.Diagnostics;
using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Draw the shortcut associated with a recent document entry in an application menu.
    /// </summary>
    internal class ViewDrawRibbonRecentShortcut : ViewDrawContent
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonRecentShortcut class.
        /// </summary>
        /// <param name="paletteContent">Palette source for the content.</param>
        /// <param name="values">Reference to actual content values.</param>
        public ViewDrawRibbonRecentShortcut(IPaletteContent paletteContent, 
                                            IContentValues values)
            : base(paletteContent, values, VisualOrientation.Top)
        {
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawRibbonRecentShortcut:" + Id;
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context) 
        {
            Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Only draw the shortcut text if there is some defined
            string shortcut = Values.GetShortText();
            if (!string.IsNullOrEmpty(shortcut))
            {
                // Only draw shortcut if the shortcut is not equal to the fixed string 'A'
                if (!shortcut.Equals("A"))
                {
                    base.RenderBefore(context);
                }
            }
        }
        #endregion
    }
}

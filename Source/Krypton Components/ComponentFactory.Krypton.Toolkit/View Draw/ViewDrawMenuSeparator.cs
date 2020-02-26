// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2017 - 2020. All rights reserved. (https://github.com/Wagnerp/Krypton-Toolkit-Suite-NET-Core)
//  Version 5.500.0.0  www.ComponentFactory.com
// *****************************************************************************

using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Draw element for a context menu separator.
    /// </summary>
    public class ViewDrawMenuSeparator : ViewDrawDocker
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawMenuSeparator class.
        /// </summary>
        /// <param name="separator">Reference to owning separator entry.</param>
        /// <param name="palette">Palette for obtaining drawing values.</param>
        public ViewDrawMenuSeparator(KryptonContextMenuSeparator separator,
                                     PaletteDoubleRedirect palette)
            : base(separator.StateNormal.Back, separator.StateNormal.Border)
        {
            // Draw the separator by default
            Draw = true;

            // Give the separator object the redirector to use when inheriting values
            separator.SetPaletteRedirect(palette);

            Orientation = separator.Horizontal ? VisualOrientation.Top : VisualOrientation.Left;

            // We need to be big enough to contain 1 pixel square spacer
            Add(new ViewLayoutSeparator(1));
        }

        /// <summary>
        /// Initialize a new instance of the ViewDrawMenuSeparator class.
        /// </summary>
        /// <param name="state">State specific source of palette values.</param>
        public ViewDrawMenuSeparator(PaletteDouble state)
            : base(state.Back, state.Border)
        {
            // We need to be big enough to contain 1 pixel square spacer
            Orientation = VisualOrientation.Left;
            Add(new ViewLayoutSeparator(1));
        }
        
        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawMenuSeparator:" + Id;
        }
        #endregion

        #region Draw
        /// <summary>
        /// Gets and sets the drawing of the separator.
        /// </summary>
        public bool Draw { get; set; }

        #endregion

        #region Paint
        /// <summary>
        /// Perform a render of the elements.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void Render(RenderContext context)
        {
            Debug.Assert(context != null);

            if (Draw)
            {
                base.Render(context);
            }
        }
        #endregion
    }
}

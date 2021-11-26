#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// The designer for the <see cref="KryptonScrollBar"/> control.
    /// </summary>
    internal class ScrollBarControlDesigner : ControlDesigner
    {
        /// <summary>
        /// Gets the <see cref="SelectionRules"/> for the control.
        /// </summary>
        public override SelectionRules SelectionRules
        {
            get
            {
                // gets the property descriptor for the property "Orientation"
                PropertyDescriptor propDescriptor =
                   TypeDescriptor.GetProperties(Component)["Orientation"];

                // if not null - we can read the current orientation of the scroll bar
                if (propDescriptor != null)
                {
                    // get the current orientation
                    ScrollBarOrientation orientation =
                       (ScrollBarOrientation)propDescriptor.GetValue(Component);

                    // if vertical orientation
                    return orientation == ScrollBarOrientation.Vertical
                        ? SelectionRules.Visible
                           | SelectionRules.Moveable
                           | SelectionRules.BottomSizeable
                           | SelectionRules.TopSizeable
                        : SelectionRules.Visible | SelectionRules.Moveable
                       | SelectionRules.LeftSizeable | SelectionRules.RightSizeable;
                }

                return base.SelectionRules;
            }
        }

        /// <summary>
        /// Prefilters the properties so that unnecessary properties are hidden
        /// in the property browser of Visual Studio.
        /// </summary>
        /// <param name="properties">The property dictionary.</param>
        protected override void PreFilterProperties(IDictionary properties)
        {
            properties.Remove("Text");
            properties.Remove("BackgroundImage");
            properties.Remove("ForeColor");
            properties.Remove("ImeMode");
            properties.Remove("Padding");
            properties.Remove("BackgroundImageLayout");
            properties.Remove("BackColor");
            properties.Remove("Font");
            properties.Remove("RightToLeft");

            base.PreFilterProperties(properties);
        }
    }
}
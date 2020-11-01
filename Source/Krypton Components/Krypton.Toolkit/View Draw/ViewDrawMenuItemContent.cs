// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.550.0  
// *****************************************************************************

using System.Drawing;
using System.Diagnostics;

namespace Krypton.Toolkit
{
    internal class ViewDrawMenuItemContent : ViewDrawContent,
                                             IContextMenuItemColumn
    {
        #region Instance Field

        private int _overridePreferredWidth;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawMenuItemContent class.
        /// </summary>
        /// <param name="palette">Source of palette display values.</param>
        /// <param name="values">Source of content values.</param>
        /// <param name="columnIndex">Menu item column index.</param>
        public ViewDrawMenuItemContent(IPaletteContent palette,
                                       IContentValues values,
                                       int columnIndex)
            : base(palette, values, VisualOrientation.Top)
        {
            ColumnIndex = columnIndex;
            _overridePreferredWidth = 0;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawMenuItemContent:" + Id;
        }
        #endregion 

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            Debug.Assert(context != null);

            Size preferredSize = base.GetPreferredSize(context);

            if (_overridePreferredWidth != 0)
            {
                preferredSize.Width = _overridePreferredWidth;
            }
            else
            {
                LastPreferredSize = base.GetPreferredSize(context);
            }

            return preferredSize;
        }
        #endregion

        #region IContextMenuItemColumn
        /// <summary>
        /// Gets the index of the column within the menu item.
        /// </summary>
        public int ColumnIndex { get; }

        /// <summary>
        /// Gets the last calculated preferred size value.
        /// </summary>
        public Size LastPreferredSize { get; private set; }

        /// <summary>
        /// Sets the preferred width value to use until further notice.
        /// </summary>
        public int OverridePreferredWidth
        {
            set => _overridePreferredWidth = value;
        }
        #endregion
    }
}

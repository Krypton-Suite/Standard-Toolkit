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

using System.Drawing;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Draw element for a context menu color columns.
    /// </summary>
    public class ViewDrawMenuColorColumns : ViewComposite
    {
        #region Instance Fields
        private readonly IContextMenuProvider _provider;
        private readonly KryptonContextMenuColorColumns _colorColumns;
        private readonly ViewLayoutDocker _outerDocker;
        private readonly ViewLayoutDocker _innerDocker;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawMenuColorColumns class.
        /// </summary>
        /// <param name="provider">Reference to provider.</param>
        /// <param name="colorColumns">Reference to owning color columns entry.</param>
        public ViewDrawMenuColorColumns(IContextMenuProvider provider,
                                        KryptonContextMenuColorColumns colorColumns)
        {
            _provider = provider;
            _colorColumns = colorColumns;

            // Use separators to create space around the colors areas
            _innerDocker = new ViewLayoutDocker();

            // Redraw when the selected color changes
            colorColumns.SelectedColorChanged += OnSelectedColorChanged;

            Color[][] colors = colorColumns.Colors;
            int columns = colors.Length;
            int rows = ((columns > 0) && (colors[0] != null) ? colors[0].Length : 0);
            bool enabled = provider.ProviderEnabled;

            // Always assume there is a first row of colors
            ViewLayoutStack fillStack = new ViewLayoutStack(false)
            {
                CreateColumns(provider, colorColumns, colors, 0, 1, enabled)
            };

            // If there are other rows to show
            if (rows > 1)
            {
                if (_colorColumns.GroupNonFirstRows)
                {
                    // Create a view to show the rest of the rows
                    fillStack.Add(new ViewLayoutSeparator(5));
                    fillStack.Add(CreateColumns(provider, colorColumns, colors, 1, rows, enabled));
                }
                else
                {
                    // Otherwise show each row as separate
                    for (int i = 1; i < rows; i++)
                    {
                        fillStack.Add(new ViewLayoutSeparator(5));
                        fillStack.Add(CreateColumns(provider, colorColumns, colors, i, i+1, enabled));
                    }
                }
            }

            // Create a gap around the contents
            _innerDocker.Add(fillStack, ViewDockStyle.Fill);
            _innerDocker.Add(new ViewLayoutSeparator(3), ViewDockStyle.Top);
            _innerDocker.Add(new ViewLayoutSeparator(3), ViewDockStyle.Bottom);
            _innerDocker.Add(new ViewLayoutSeparator(2), ViewDockStyle.Left);
            _innerDocker.Add(new ViewLayoutSeparator(2), ViewDockStyle.Right);

            // Use outer docker so that any extra space not needed is used by the null
            _outerDocker = new ViewLayoutDocker
            {
                { _innerDocker, ViewDockStyle.Top },
                { new ViewLayoutNull(), ViewDockStyle.Fill }
            };

            // Add docker as the composite content
            Add(_outerDocker);
        }

        /// <summary>
        /// Release unmanaged and optionally managed resources.
        /// </summary>
        /// <param name="disposing">Called from Dispose method.</param>
        protected override void Dispose(bool disposing)
        {
            // Prevent memory leak
            _colorColumns.SelectedColorChanged -= OnSelectedColorChanged;
            base.Dispose(disposing);
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawMenuColorColumns:" + Id;
        }
        #endregion

        #region Layout
        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // We take on all the available display area
            ClientRectangle = context.DisplayRectangle;

            base.Layout(context);
        }
        #endregion

        #region Implementation
        private ViewLayoutStack CreateColumns(IContextMenuProvider provider,
                                              KryptonContextMenuColorColumns colorColumns,
                                              Color[][] colors, 
                                              int start, 
                                              int end,
                                              bool enabled)
        {
            // Create a horizontal stack of columns
            ViewLayoutStack columns = new ViewLayoutStack(true)
            {
                FillLastChild = false
            };

            // Add each color column
            for (int i = 0; i < colors.Length; i++)
            {
                // Use a separator between each column
                if (i > 0)
                {
                    columns.Add(new ViewLayoutSeparator(4));
                }

                // Add container for the column, this draws the background
                ViewDrawMenuColorColumn colorColumn = new ViewDrawMenuColorColumn(provider, colorColumns, colors[i], start, end, enabled);
                columns.Add(colorColumn);
            }

            return columns;
        }

        private void OnSelectedColorChanged(object sender, ColorEventArgs e)
        {
            _provider.ProviderNeedPaintDelegate(this, new NeedLayoutEventArgs(false));
        }
        #endregion
    }
}

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
        public override string ToString() =>
            // Return the class name and instance identifier
            "ViewDrawMenuItemContent:" + Id;

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

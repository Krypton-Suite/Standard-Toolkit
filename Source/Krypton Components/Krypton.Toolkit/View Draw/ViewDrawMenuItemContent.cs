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
        $"ViewDrawMenuItemContent:{Id}";

    #endregion 

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        var preferredSize = base.GetPreferredSize(context!);

        if (_overridePreferredWidth != 0)
        {
            preferredSize.Width = _overridePreferredWidth;
        }
        else
        {
            LastPreferredSize = base.GetPreferredSize(context!);
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
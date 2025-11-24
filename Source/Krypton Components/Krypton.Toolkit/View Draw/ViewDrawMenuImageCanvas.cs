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

internal class ViewDrawMenuImageCanvas : ViewDrawCanvas, IContextMenuItemColumn
{
    #region Instance Fields

    private int _overridePreferredWidth;
    private readonly bool _zeroHeight;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawMenuImageCanvas class.
    /// </summary>
    /// <param name="paletteBack">Palette source for the background.</param>        
    /// <param name="paletteBorder">Palette source for the border.</param>
    /// <param name="columnIndex">Menu item column index.</param>
    /// <param name="zeroHeight">Should the height be forced to zero.</param>
    public ViewDrawMenuImageCanvas(IPaletteBack paletteBack,
        IPaletteBorder paletteBorder,
        int columnIndex,
        bool zeroHeight)
        : base(paletteBack, paletteBorder, VisualOrientation.Top)
    {
        ColumnIndex = columnIndex;
        _overridePreferredWidth = 0;
        _zeroHeight = zeroHeight;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawMenuCanvas:{Id}";

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        Size preferredSize = base.GetPreferredSize(context!);

        if (_overridePreferredWidth != 0)
        {
            preferredSize.Width = _overridePreferredWidth;
        }
        else
        {
            LastPreferredSize = base.GetPreferredSize(context!);
        }

        if (_zeroHeight)
        {
            preferredSize.Height = 0;
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

    #region Layout

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Validate incoming reference
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        base.Layout(context);
    }
    #endregion

    #region Paint

    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public override void RenderBefore([DisallowNull] RenderContext context)
    {
        Debug.Assert(context != null);

        // Validate incoming reference
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        base.RenderBefore(context);
    }
    #endregion

}
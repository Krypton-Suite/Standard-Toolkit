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

internal class ViewDrawMenuImageColumn : ViewDrawDocker
{
    #region Instance Fields
    private readonly ViewLayoutSeparator _separator;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawMenuImageColumn class.
    /// </summary>
    /// <param name="items">Reference to the owning collection.</param>
    /// <param name="palette">Palette for obtaining drawing values.</param>
    public ViewDrawMenuImageColumn(KryptonContextMenuItems items,
        PaletteDoubleRedirect palette)
        : base(items.StateNormal.Back, items.StateNormal.Border)
    {
        // Give the items collection the redirector to use when inheriting values
        items.SetPaletteRedirect(palette);

        _separator = new ViewLayoutSeparator(0);
        Add(_separator);
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawMenuImageColumn:{Id}";

    #endregion

    #region Width
    /// <summary>
    /// Sets the width of the column.
    /// </summary>
    public int ColumnWidth
    {
        set => _separator.SeparatorSize = new Size(value, 0);
    }
    #endregion

    #region Layout
    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;

        // Ensure all children are laid out in our total space
        base.Layout(context);
    }        
    #endregion    
}
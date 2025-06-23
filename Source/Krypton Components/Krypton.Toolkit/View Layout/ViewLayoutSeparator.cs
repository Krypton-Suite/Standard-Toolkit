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

/// <summary>
/// Positions a separator to take up space without drawing.
/// </summary>
public class ViewLayoutSeparator : ViewLeaf
{
    #region Instance Fields
    private int _width;
    private int _height;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutRibbonSeparator class.
    /// </summary>
    /// <param name="length">Length of the separator.</param>
    public ViewLayoutSeparator(int length)
        : this(length, length)
    {
    }

    /// <summary>
    /// Initialize a new instance of the ViewLayoutRibbonSeparator class.
    /// </summary>
    /// <param name="width">Width of the separator.</param>
    /// <param name="height">Height of the separator.</param>
    public ViewLayoutSeparator(int width,
        int height)
    {
        _width = width;
        _height = height;
    }
        
    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutSeparator:{Id}";

    #endregion

    #region SeparatorSize
    /// <summary>
    /// Gets and sets the separator size.
    /// </summary>
    public Size SeparatorSize
    {
        get => new Size(_width, _height);

        set
        {
            _width = value.Width;
            _height = value.Height;
        }
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context) =>
        // Always return the same minimum size
        new Size(_width, _height);

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;
    }
    #endregion
}
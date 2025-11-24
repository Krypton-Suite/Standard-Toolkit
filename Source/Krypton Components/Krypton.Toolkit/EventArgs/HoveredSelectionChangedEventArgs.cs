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

// ReSharper disable MemberCanBeInternal
// ReSharper disable UnusedMember.Global

namespace Krypton.Toolkit;

/// <summary>
/// Hovered Selection Changed event data.
/// </summary>
public class HoveredSelectionChangedEventArgs : EventArgs
{
    #region Instance Fields
    private readonly Rectangle _bounds;
    private readonly int _index;
    private readonly object _item;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the HoveredSelectionChangedEventArgs class.
    /// </summary>
    /// <param name="bounds">The bounds of the selected dropdown item.</param>
    /// <param name="index">The index within the dropdown items collection.</param>
    /// <param name="item">The item within the dropdown items collection.</param>
    public HoveredSelectionChangedEventArgs(Rectangle bounds, int index, object item)
    {
        _bounds = bounds;
        _index = index;
        _item = item;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the bounds.
    /// </summary>
    public Rectangle Bounds => _bounds;

    /// <summary>
    /// Gets the item index.
    /// </summary>
    public int Index => _index;

    /// <summary>
    /// Gets the item.
    /// </summary>
    public object Item => _item;

    #endregion
}
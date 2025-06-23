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

namespace Krypton.Navigator;

/// <summary>
/// View element that positions the selected page so it cannot be seen.
/// </summary>
internal class ViewLayoutPageHide : ViewLayoutNull
{
    #region Static Fields

    private const int HIDDEN_OFFSET = 1000000;

    #endregion

    #region Instance Fields
    private readonly KryptonNavigator _navigator;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutPageHide class.
    /// </summary>
    public ViewLayoutPageHide([DisallowNull] KryptonNavigator navigator)
    {
        Debug.Assert(navigator is not null);

        // Remember back reference
        _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutPageHide:{Id}";

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);
        return Size.Empty;
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context is not null);

        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // We take on all the available display area
        ClientRectangle = context.DisplayRectangle;

        // Are we allowed to layout child controls?
        if (!context.ViewManager!.DoNotLayoutControls)
        {
            // Are we allowed to actually layout the pages?
            if (_navigator is { InternalCanLayout: true, IsChildPanelBorrowed: false })
                // Do not position the child panel if it is borrowed
            {
                // Position the child panel for showing page information
                _navigator.ChildPanel!.SetBounds(HIDDEN_OFFSET,
                    HIDDEN_OFFSET,
                    ClientWidth,
                    ClientHeight);
            }
        }
    }
    #endregion
}
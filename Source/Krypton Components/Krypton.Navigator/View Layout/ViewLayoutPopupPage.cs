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
/// View element that positions the provided page in the requested position.
/// </summary>
internal class ViewLayoutPopupPage : ViewLayoutNull
{
    #region Instance Fields
    private readonly KryptonNavigator _navigator;
    private readonly KryptonPage _page;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutPopupPage class.
    /// </summary>
    /// <param name="navigator">Reference to owning navigator control.</param>
    /// <param name="page">Page to the positioned.</param>
    public ViewLayoutPopupPage([DisallowNull] KryptonNavigator navigator,
        [DisallowNull] KryptonPage page)
    {
        Debug.Assert(navigator is not null);
        Debug.Assert(page is not null);

        _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));
        _page = page ?? throw new ArgumentNullException(nameof(page));
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutPopupPage:{Id}";

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context is not null);

        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        return _page.GetPreferredSize(context.DisplayRectangle.Size);
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
            if (_navigator.InternalCanLayout)
            {
                // Update position of page if not already in correct position
                if ((_page.Location != Point.Empty) ||
                    (_page.Width != ClientWidth) ||
                    (_page.Height != ClientHeight))
                {
                    _page.SetBounds(0, 0, ClientWidth, ClientHeight);
                }

                // Update position of child panel if not already in correct position
                if ((_navigator.ChildPanel!.Location != ClientLocation) ||
                    (_navigator.ChildPanel.Width != ClientWidth) ||
                    (_navigator.ChildPanel.Height != ClientHeight))
                {
                    // Position the child panel for showing page
                    _navigator.ChildPanel.SetBounds(ClientLocation.X,
                        ClientLocation.Y,
                        ClientWidth,
                        ClientHeight);
                }
            }
        }
    }
    #endregion
}
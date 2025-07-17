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
/// View element that draws nothing and will use a padding around the children.
/// </summary>
public class ViewLayoutPadding : ViewComposite
{
    #region Instance Fields
    private Padding _displayPadding;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutPadding class.
    /// </summary>
    public ViewLayoutPadding()
        : this(Padding.Empty, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the ViewLayoutPadding class.
    /// </summary>
    /// <param name="displayPadding">Padding to use around area.</param>
    public ViewLayoutPadding(Padding displayPadding)
        : this(displayPadding, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the ViewLayoutPadding class.
    /// </summary>
    /// <param name="displayPadding">Padding to use around area.</param>
    /// <param name="child">Child to add into view hierarchy.</param>
    public ViewLayoutPadding(Padding displayPadding, ViewBase? child)
    {
        _displayPadding = displayPadding;
        Add(child);
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutPadding:{Id}";

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Ask base class to find preferred size of all children
        Size preferredSize = base.GetPreferredSize(context!);

        // Add on the display padding
        preferredSize.Width += _displayPadding.Horizontal;
        preferredSize.Height += _displayPadding.Vertical;

        return preferredSize;
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Take on the provided space
        ClientRectangle = context!.DisplayRectangle;

        // Reduce space by the padding value
        context.DisplayRectangle = CommonHelper.ApplyPadding(Orientation.Horizontal, ClientRectangle, _displayPadding);

        // Layout each of the children with the new size
        foreach (ViewBase child in this)
        {
            // Only layout visible children
            if (child.Visible)
            {
                child.Layout(context);
            }
        }

        // Restore original display rect we were given
        context.DisplayRectangle = ClientRectangle;
    }
    #endregion
}
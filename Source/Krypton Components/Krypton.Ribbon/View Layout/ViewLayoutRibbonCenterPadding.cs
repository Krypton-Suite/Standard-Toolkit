#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// View element adds padding to the contained elements and positions all elements centered.
/// </summary>
internal class ViewLayoutRibbonCenterPadding : ViewLayoutRibbonCenter
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutRibbonGroupImage class.
    /// </summary>
    /// <param name="preferredPadding">Padding to use when calculating space.</param>
    public ViewLayoutRibbonCenterPadding(Padding preferredPadding) => PreferredPadding = preferredPadding;

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutRibbonCenterPadding:{Id}";

    #endregion

    #region PreferredPadding
    /// <summary>
    /// Gets and sets the preferred padding.
    /// </summary>
    public Padding PreferredPadding { get; set; }

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        // Get the preferred size of the contained content
        Size preferredSize = base.GetPreferredSize(context);

        // Add on the padding we need around edges
        return new Size(preferredSize.Width + PreferredPadding.Horizontal,
            preferredSize.Height + PreferredPadding.Vertical);
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Validate incoming reference
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // We take on all the available display area
        ClientRectangle = context.DisplayRectangle;

        // Reduce by the padding
        Rectangle innerRectangle = ClientRectangle;
        innerRectangle.X += PreferredPadding.Left;
        innerRectangle.Y += PreferredPadding.Top;
        innerRectangle.Width -= PreferredPadding.Horizontal;
        innerRectangle.Height -= PreferredPadding.Vertical;

        // Layout each child centered within this space
        foreach (ViewBase child in this)
        {
            // Only layout visible children
            if (child.Visible)
            {
                // Ask child for it's own preferred size
                Size childPreferred = child.GetPreferredSize(context);

                // Make sure the child is never bigger than the available space
                if (childPreferred.Width > ClientRectangle.Width)
                {
                    childPreferred.Width = ClientWidth;
                }
                if (childPreferred.Height > ClientRectangle.Height)
                {
                    childPreferred.Height = ClientHeight;
                }

                // Find vertical and horizontal offsets for centering
                var xOffset = (innerRectangle.Width - childPreferred.Width) / 2;
                var yOffset = (innerRectangle.Height - childPreferred.Height) / 2;

                // Create the rectangle that centers the child in our space
                context.DisplayRectangle = new Rectangle(innerRectangle.X + xOffset,
                    innerRectangle.Y + yOffset,
                    childPreferred.Width,
                    childPreferred.Height);

                // Finally ask the child to layout
                child.Layout(context);
            }
        }

        // Put back the original display value now we have finished
        context.DisplayRectangle = ClientRectangle;
    }
    #endregion
}
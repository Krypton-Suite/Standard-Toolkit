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
/// View element that positions the elements in a row centered in total area.
/// </summary>
internal class ViewLayoutRibbonRowCenter : ViewComposite
{
    #region Type Definitions
    private class ItemToView : Dictionary<IRibbonGroupItem, ViewBase>;
    private class ViewToSize : Dictionary<ViewBase, Size>;
    #endregion

    #region Instance Fields

    private readonly ViewToSize _viewToSmall;
    private readonly ViewToSize _viewToMedium;
    private readonly ViewToSize _viewToLarge;
    private Size _preferredSizeSmall;
    private Size _preferredSizeMedium;
    private Size _preferredSizeLarge;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutRibbonRowCenter class.
    /// </summary>
    public ViewLayoutRibbonRowCenter()
    {
        CurrentSize = GroupItemSize.Large;
        _viewToSmall = new ViewToSize();
        _viewToMedium = new ViewToSize();
        _viewToLarge = new ViewToSize();
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutRibbonRowCenter:{Id}";

    #endregion

    #region CurrentSize
    /// <summary>
    /// Gets and sets the current group item size.
    /// </summary>
    public GroupItemSize CurrentSize { get; set; }

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        switch (CurrentSize)
        {
            case GroupItemSize.Small:
                _viewToSmall.Clear();
                break;

            case GroupItemSize.Medium:
                _viewToMedium.Clear();
                break;

            case GroupItemSize.Large:
                _viewToLarge.Clear();
                break;

            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(CurrentSize.ToString());
                break;
        }

        var preferredSize = Size.Empty;

        foreach (ViewBase child in this)
        {
            // Only investigate visible children
            if (child.Visible)
            {
                // Ask child for it's own preferred size
                Size childPreferred = child.GetPreferredSize(context!);

                // Cache the child preferred size for use in layout
                switch (CurrentSize)
                {
                    case GroupItemSize.Small:
                        _viewToSmall.Add(child, childPreferred);
                        break;
                    case GroupItemSize.Medium:
                        _viewToMedium.Add(child, childPreferred);
                        break;
                    case GroupItemSize.Large:
                        _viewToLarge.Add(child, childPreferred);
                        break;
                }

                // Always add on the width of the child
                preferredSize.Width += childPreferred.Width;

                // Find the tallest of the children
                preferredSize.Height = Math.Max(preferredSize.Height, childPreferred.Height);
            }
        }

        // Cache the size for the current item
        switch (CurrentSize)
        {
            case GroupItemSize.Small:
                _preferredSizeSmall = preferredSize;
                break;
            case GroupItemSize.Medium:
                _preferredSizeMedium = preferredSize;
                break;
            case GroupItemSize.Large:
                _preferredSizeLarge = preferredSize;
                break;
        }

        return preferredSize;
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

        var preferredSize = Size.Empty;

        // Cache the size for the current item
        switch (CurrentSize)
        {
            case GroupItemSize.Small:
                preferredSize = _preferredSizeSmall;
                break;
            case GroupItemSize.Medium:
                preferredSize = _preferredSizeMedium;
                break;
            case GroupItemSize.Large:
                preferredSize = _preferredSizeLarge;
                break;
        }

        // Starting left offset is half the difference between the client width and the total child widths
        var xOffset = (ClientWidth - preferredSize.Width) / 2;

        // Layout each child centered within this space
        foreach (ViewBase child in this)
        {
            // Only layout visible children
            if (child.Visible)
            {
                // Get the cached size of the child
                var childPreferred = Size.Empty;
                    
                switch (CurrentSize)
                {
                    case GroupItemSize.Small:
                        childPreferred = _viewToSmall.TryGetValue(child, out Size value)
                            ? value
                            : child.GetPreferredSize(context);
                        break;
                    case GroupItemSize.Medium:
                        childPreferred = _viewToMedium.TryGetValue(child, out Size value1)
                            ? value1
                            : child.GetPreferredSize(context);
                        break;
                    case GroupItemSize.Large:
                        childPreferred = _viewToLarge.TryGetValue(child, out Size value2)
                            ? value2
                            : child.GetPreferredSize(context);
                        break;
                }

                // Find vertical offset for centering
                var yOffset = (ClientHeight - childPreferred.Height) / 2;

                // Create the rectangle that centers the child in our space
                context.DisplayRectangle = new Rectangle(ClientRectangle.X + xOffset,
                    ClientRectangle.Y + yOffset,
                    childPreferred.Width,
                    childPreferred.Height);

                // Finally ask the child to layout
                child.Layout(context);

                // Move across to next horizontal position
                xOffset += childPreferred.Width;
            }
        }

        // Put back the original display value now we have finished
        context.DisplayRectangle = ClientRectangle;
    }
    #endregion
}
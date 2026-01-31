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
/// Extends the ViewComposite by laying out children in horizontal/vertical stack.
/// </summary>
public class ViewLayoutStack : ViewComposite
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutStack class.
    /// </summary>
    public ViewLayoutStack(bool horizontal)
    {
        // Create child to dock style lookup
        Horizontal = horizontal;

        // By default we fill the remainder area with the last child
        FillLastChild = true;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutStack:{Id}";

    #endregion

    #region Horizontal
    /// <summary>
    /// Gets and sets the stack orientation.
    /// </summary>
    public bool Horizontal { get; set; }

    #endregion

    #region FillLastChild
    /// <summary>
    /// Gets and sets if the last child fills the remainder of the space.
    /// </summary>
    public bool FillLastChild { get; set; }

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Accumulate the stacked size
        var preferredSize = Size.Empty;

        foreach (ViewBase child in this)
        {
            if (child.Visible)
            {
                // Get the preferred size of the child
                Size childSize = child.GetPreferredSize(context!);

                // Depending on orientation, add up child sizes
                if (Horizontal)
                {
                    preferredSize.Height = Math.Max(preferredSize.Height, childSize.Height);
                    preferredSize.Width += childSize.Width;
                }
                else
                {
                    preferredSize.Height += childSize.Height;
                    preferredSize.Width = Math.Max(preferredSize.Width, childSize.Width);
                }
            }
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

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;

        // Maximum space available for the next child
        Rectangle childRectangle = ClientRectangle;

        // Find the last visible child
        ViewBase? lastVisible = Reverse().FirstOrDefault(child => child.Visible);

        // Position each entry, with last entry filling remaining of space
        foreach (ViewBase child in this)
        {
            if (child.Visible)
            {
                // Provide the total space currently available
                context.DisplayRectangle = childRectangle;

                // Get the preferred size of the child
                Size childSize = child.GetPreferredSize(context);

                if (Horizontal)
                {
                    // Ask child to fill the available height
                    childSize.Height = childRectangle.Height;

                    if ((child == lastVisible) && FillLastChild)
                    {
                        // This child takes all remainder width
                        childSize.Width = childRectangle.Width;
                    }
                    else
                    {
                        // Reduce remainder space to exclude this child
                        childRectangle.X += childSize.Width;
                        childRectangle.Width -= childSize.Width;
                    }
                }
                else
                {
                    // Ask child to fill the available width
                    childSize.Width = childRectangle.Width;

                    if ((child == lastVisible) && FillLastChild)
                    {
                        // This child takes all remainder height
                        childSize.Height = childRectangle.Height;
                    }
                    else
                    {
                        // Reduce remainder space to exclude this child
                        childRectangle.Y += childSize.Height;
                        childRectangle.Height -= childSize.Height;
                    }
                }

                // Use the update child size as the actual space for layout
                context.DisplayRectangle = new Rectangle(context.DisplayRectangle.Location, childSize);

                // Layout child in the provided space
                child.Layout(context);
            }
        }

        // Put back the original display value now we have finished
        context.DisplayRectangle = ClientRectangle;
    }
    #endregion
}
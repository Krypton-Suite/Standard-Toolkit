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
/// View element that draws nothing and will stretch children to fill one dimension.
/// </summary>
public class ViewLayoutStretch : ViewComposite
{
    #region Instance Fields
    private readonly Orientation _orientation;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutCenter class.
    /// </summary>
    /// <param name="orientation">Direction to stretch.</param>
    public ViewLayoutStretch(Orientation orientation) => _orientation = orientation;

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutStretch:{Id}";

    #endregion

    #region Layout
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
        Rectangle original = context.DisplayRectangle;
        ClientRectangle = original;

        // Layout each child
        foreach (ViewBase child in this)
        {
            // Only layout visible children
            if (child.Visible)
            {
                // Ask child for it's own preferred size
                Size childPreferred = child.GetPreferredSize(context);

                // Size child to our relevant dimension
                if (_orientation == Orientation.Vertical)
                {
                    context.DisplayRectangle = ClientRectangle with { Width = childPreferred.Width };
                }
                else
                {
                    context.DisplayRectangle = ClientRectangle with { Width = childPreferred.Width };
                }

                // Finally ask the child to layout
                child.Layout(context);
            }
        }

        // Put back the original display value now we have finished
        context.DisplayRectangle = original;
    }
    #endregion
}
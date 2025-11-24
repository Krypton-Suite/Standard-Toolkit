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
/// View element that draws nothing and will split the space equally between the children.
/// </summary>
public class ViewLayoutFit : ViewComposite
{
    #region Instance Fields
    private readonly Orientation _orientation;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutFit class.
    /// </summary>
    /// <param name="orientation">Direction to fit.</param>
    public ViewLayoutFit(Orientation orientation) => _orientation = orientation;

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutFit:{Id}";

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
        var offset = 0;
        var space = _orientation == Orientation.Vertical ? ClientHeight : ClientWidth;
        for(var i=0; i<Count; i++)
        {
            var child = this[i];

            // Find length of this item
            int length;

            // If this is the last item then it takes the remaining space
            if (i == (Count - 1))
            {
                length = space;
            }
            else
            {
                // Give this item an equal portion of the remainder
                length = space / (Count - i);
            }

            // Ask child for it's own preferred size
            Size childPreferred = child!.GetPreferredSize(context);

            // Size child to our relevant dimension
            if (_orientation == Orientation.Vertical)
            {
                context.DisplayRectangle = new Rectangle(ClientRectangle.X,
                    ClientRectangle.Y + offset,
                    childPreferred.Width,
                    length);
            }
            else
            {
                context.DisplayRectangle = new Rectangle(ClientRectangle.X + offset,
                    ClientRectangle.Y,
                    length,
                    ClientRectangle.Height);
            }

            // Ask the child to layout
            child.Layout(context);

            // Adjust running values
            offset += length;
            space -= length;
        }

        // Put back the original display value now we have finished
        context.DisplayRectangle = original;
    }
    #endregion
}
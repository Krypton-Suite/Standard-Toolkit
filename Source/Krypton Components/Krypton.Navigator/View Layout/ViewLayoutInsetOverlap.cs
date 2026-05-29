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

// ReSharper disable PossibleLossOfFraction
namespace Krypton.Navigator;

/// <summary>
/// View element that insets children by the border rounding value of a source.
/// </summary>
internal class ViewLayoutInsetOverlap : ViewComposite
{
    #region Instance Fields
    private readonly ViewDrawCanvas _drawCanvas;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutInsetOverlap class.
    /// </summary>
    public ViewLayoutInsetOverlap([DisallowNull] ViewDrawCanvas drawCanvas)
    {
        Debug.Assert(drawCanvas is not null);

        if (drawCanvas is null)
        {
            throw new ArgumentNullException(nameof(drawCanvas));
        }

        // Remember source of the rounding values
        _drawCanvas = drawCanvas;

        // Default other state
        Orientation = VisualOrientation.Top;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutInsetForRounding:{Id}";

    #endregion

    #region Orientation
    /// <summary>
    /// Gets and sets the bar orientation.
    /// </summary>
    public VisualOrientation Orientation
    {
        [DebuggerStepThrough]
        get;
        set;
    }

    #endregion

    #region Rounding
    /// <summary>
    /// Gets the rounding value to apply on the edges.
    /// </summary>
    public float Rounding
    {
        get
        {
            // Get the rounding and width values for the border
            var rounding = _drawCanvas.PaletteBorder!.GetBorderRounding(_drawCanvas.State);
            var width = _drawCanvas.PaletteBorder.GetBorderWidth(_drawCanvas.State);

            // We have to add half the width as that increases the rounding effect
            return rounding + width / 2;
        }
    }
    #endregion

    #region BorderWidth
    /// <summary>
    /// Gets the rounding value to apply on the edges.
    /// </summary>
    public int BorderWidth => _drawCanvas.PaletteBorder!.GetBorderWidth(_drawCanvas.State);

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

        // Get the preferred size requested by the children
        Size size = base.GetPreferredSize(context);

        // Apply the rounding in the appropriate orientation
        if (Orientation is VisualOrientation.Top or VisualOrientation.Bottom)
        {
            size.Width += Convert.ToInt32(Rounding * 2);
            size.Height += BorderWidth;
        }
        else
        {
            size.Height += Convert.ToInt32(Rounding * 2);
            size.Width += BorderWidth;
        }

        return size;
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

        // Find the rectangle available to each child by removing the rounding
        RectangleF childRectF = ClientRectangle;

        // Find the amount of rounding to apply
        var rounding = Rounding;

        // Apply the rounding in the appropriate orientation
        if (Orientation is VisualOrientation.Top or VisualOrientation.Bottom)
        {
            childRectF.Width -= rounding * 2;
            childRectF.X += rounding;
        }
        else
        {
            childRectF.Height -= rounding * 2;
            childRectF.Y += rounding;
        }

        // Convert childRectF to a 'int' Rectangle
        var childRect = new Rectangle((int)childRectF.X, (int)childRectF.Y, (int)childRectF.Width, (int)childRectF.Height);

        // Inform each child to layout inside the reduced rectangle
        foreach (ViewBase child in this)
        {
            context.DisplayRectangle = childRect;

            context.DisplayRectangleF = childRectF;

            child.Layout(context);
        }

        // Remember the set context to the size we were given
        context.DisplayRectangle = ClientRectangle;

        context.DisplayRectangleF = ClientRectangleF;
    }
    #endregion
}
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
/// View element that draws nothing and will center all children within itself.
/// </summary>
public class ViewLayoutCenter : ViewComposite
{
    #region Instance Fields
    private Padding _rectPadding;
    private readonly IPaletteMetric? _paletteMetric;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutCenter class.
    /// </summary>
    public ViewLayoutCenter()
        : this(null, PaletteMetricPadding.None, VisualOrientation.Top, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the ViewLayoutCenter class.
    /// </summary>
    /// <param name="childElement">Optional element to add as child.</param>
    public ViewLayoutCenter(ViewBase childElement)
        : this(null, PaletteMetricPadding.None, VisualOrientation.Top, childElement)
    {
    }

    /// <summary>
    /// Initialize a new instance of the ViewLayoutCenter class.
    /// </summary>
    /// <param name="paletteMetric">Target for recovering metric values.</param>
    /// <param name="metricPadding">Metric to use for border padding.</param>
    /// <param name="orientation">Orientation of the element.</param>
    public ViewLayoutCenter(IPaletteMetric paletteMetric,
        PaletteMetricPadding metricPadding,
        VisualOrientation orientation)
        : this(paletteMetric, metricPadding, orientation, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the ViewLayoutCenter class.
    /// </summary>
    /// <param name="paletteMetric">Target for recovering metric values.</param>
    /// <param name="metricPadding">Metric to use for border padding.</param>
    /// <param name="orientation">Orientation of the element.</param>
    /// <param name="childElement">Optional element to add as child.</param>
    public ViewLayoutCenter(IPaletteMetric? paletteMetric,
        PaletteMetricPadding metricPadding,
        VisualOrientation orientation,
        ViewBase? childElement)
    {
        // Remember provided values
        _paletteMetric = paletteMetric;
        MetricPadding = metricPadding;
        Orientation = orientation;

        if (childElement != null)
        {
            Add(childElement);
        }
    }

    /// <summary>
    /// Initialize a new instance of the ViewLayoutCenter class.
    /// </summary>
    /// <param name="size">Manual padding amount.</param>
    public ViewLayoutCenter(int size)
        : this(null, PaletteMetricPadding.None, VisualOrientation.Top, null) =>
        _rectPadding = new Padding(size);

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutCenter:{Id}";

    #endregion

    #region MetricPadding
    /// <summary>
    /// Gets and sets the metric used to calculate the extra border padding.
    /// </summary>
    public PaletteMetricPadding MetricPadding { get; set; }

    #endregion

    #region Orientation
    /// <summary>
    /// Gets and sets the visual orientation.
    /// </summary>
    public VisualOrientation Orientation { get; set; }

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Validate incoming reference
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // Let base class find preferred size of the children
        Size preferredSize = base.GetPreferredSize(context);

        // Do we have a border padding to apply?
        if ((_paletteMetric != null) && (MetricPadding != PaletteMetricPadding.None))
        {
            // Get the required padding for the border
            Padding borderPadding = _paletteMetric.GetMetricPadding(context.Control as KryptonForm, ElementState, MetricPadding);

            // Applying the padding will depend on the orientation
            switch(Orientation)
            {
                case VisualOrientation.Top:
                case VisualOrientation.Bottom:
                    preferredSize.Width += borderPadding.Horizontal;
                    preferredSize.Height += borderPadding.Vertical;
                    break;
                case VisualOrientation.Left:
                case VisualOrientation.Right:
                    preferredSize.Width += borderPadding.Vertical;
                    preferredSize.Height += borderPadding.Horizontal;
                    break;
            }
        }

        // Do we have a manual padding to apply?
        if (_rectPadding != null)
        {
            // Applying the padding will depend on the orientation
            switch (Orientation)
            {
                case VisualOrientation.Top:
                case VisualOrientation.Bottom:
                    preferredSize.Width += _rectPadding.Horizontal;
                    preferredSize.Height += _rectPadding.Vertical;
                    break;
                case VisualOrientation.Left:
                case VisualOrientation.Right:
                    preferredSize.Width += _rectPadding.Vertical;
                    preferredSize.Height += _rectPadding.Horizontal;
                    break;
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

        // Validate incoming reference
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // We take on all the available display area
        Rectangle original = context.DisplayRectangle;
        ClientRectangle = original;

        // Do we have a border padding to apply?
        if ((_paletteMetric != null) && (MetricPadding != PaletteMetricPadding.None))
        {
            // Get the required padding for the border
            Padding borderPadding = _paletteMetric.GetMetricPadding(context.Control as KryptonForm, ElementState, MetricPadding);

            // Applying the padding will depend on the orientation
            ClientRectangle = Orientation switch
            {
                VisualOrientation.Top => new Rectangle(ClientLocation.X + borderPadding.Left,
                    ClientLocation.Y + borderPadding.Top, ClientWidth - borderPadding.Horizontal,
                    ClientHeight - borderPadding.Vertical),
                VisualOrientation.Bottom => new Rectangle(ClientLocation.X + borderPadding.Right,
                    ClientLocation.Y + borderPadding.Bottom, ClientWidth - borderPadding.Horizontal,
                    ClientHeight - borderPadding.Vertical),
                VisualOrientation.Left => new Rectangle(ClientLocation.X + borderPadding.Top,
                    ClientLocation.Y + borderPadding.Right, ClientWidth - borderPadding.Vertical,
                    ClientHeight - borderPadding.Horizontal),
                VisualOrientation.Right => new Rectangle(ClientLocation.X + borderPadding.Bottom,
                    ClientLocation.Y + borderPadding.Left, ClientWidth - borderPadding.Vertical,
                    ClientHeight - borderPadding.Horizontal),
                _ => ClientRectangle
            };
        }

        // Do we have a manual padding to apply?
        if (_rectPadding != null)
        {
            // Applying the padding will depend on the orientation
            ClientRectangle = Orientation switch
            {
                VisualOrientation.Top => new Rectangle(ClientLocation.X + _rectPadding.Left,
                    ClientLocation.Y + _rectPadding.Top, ClientWidth - _rectPadding.Horizontal,
                    ClientHeight - _rectPadding.Vertical),
                VisualOrientation.Bottom => new Rectangle(ClientLocation.X + _rectPadding.Right,
                    ClientLocation.Y + _rectPadding.Bottom, ClientWidth - _rectPadding.Horizontal,
                    ClientHeight - _rectPadding.Vertical),
                VisualOrientation.Left => new Rectangle(ClientLocation.X + _rectPadding.Top,
                    ClientLocation.Y + _rectPadding.Right, ClientWidth - _rectPadding.Vertical,
                    ClientHeight - _rectPadding.Horizontal),
                VisualOrientation.Right => new Rectangle(ClientLocation.X + _rectPadding.Bottom,
                    ClientLocation.Y + _rectPadding.Left, ClientWidth - _rectPadding.Vertical,
                    ClientHeight - _rectPadding.Horizontal),
                _ => ClientRectangle
            };
        }

        // Layout each child centered within this space
        foreach (ViewBase child in this)
        {
            // Only layout visible children
            if (child.Visible)
            {
                // Ask child for it's own preferred size
                Size childPreferred = child.GetPreferredSize(context);

                // Make sure the child is never bigger than the available space
                if (childPreferred.Width > ClientWidth)
                {
                    childPreferred.Width = ClientWidth;
                }

                if (childPreferred.Height > ClientHeight)
                {
                    childPreferred.Height = ClientHeight;
                }

                // Find vertical and horizontal offsets for centering
                var xOffset = (ClientWidth - childPreferred.Width) / 2;
                var yOffset = (ClientHeight - childPreferred.Height) / 2;

                // Create the rectangle that centers the child in our space
                context.DisplayRectangle = new Rectangle(ClientRectangle.X + xOffset,
                    ClientRectangle.Y + yOffset,
                    childPreferred.Width,
                    childPreferred.Height);

                // Finally ask the child to layout
                child.Layout(context);
            }
        }

        // Put back the original display value now we have finished
        context.DisplayRectangle = original;
    }
    #endregion
}
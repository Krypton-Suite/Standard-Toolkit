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
/// Draw the track for the track bar.
/// </summary>
public class ViewDrawTP : ViewComposite
{
    #region Instance Fields

    private readonly ViewDrawTrackTrack _drawTrack;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawTrackPosition class.
    /// </summary>
    /// <param name="drawTrackBar">Reference to owning track bar.</param>
    public ViewDrawTP(ViewDrawTrackBar drawTrackBar)
    {
        ViewDrawTrackBar = drawTrackBar;

        // Create child view elements
        _drawTrack = new ViewDrawTrackTrack(ViewDrawTrackBar);
        ViewDrawTrackPosition = new ViewDrawTrackPosition(ViewDrawTrackBar);
        Add(_drawTrack);
        Add(ViewDrawTrackPosition);

        // Use controller for the entire track area
        var tbController = new TrackBarController(this);
        drawTrackBar.MouseController = tbController;
        drawTrackBar.KeyController = tbController;
        drawTrackBar.SourceController = tbController;

        // Use controller for dragging the position indicator
        var tpController = new TrackPositionController(this);
        ViewDrawTrackPosition.MouseController = tpController;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawTP:{Id}";

    #endregion

    #region Public
    /// <summary>
    /// Gets access to the owning trackbar.
    /// </summary>
    public ViewDrawTrackBar ViewDrawTrackBar { get; }

    /// <summary>
    /// Gets access to the track position element.
    /// </summary>
    public ViewDrawTrackPosition ViewDrawTrackPosition { get; }

    /// <summary>
    /// Gets and sets the enabled state of the element.
    /// </summary>
    public override bool Enabled
    {
        get => base.Enabled;

        set
        {
            base.Enabled = value;
            _drawTrack.Enabled = value;
            ViewDrawTrackPosition.Enabled = value;
        }
    }

    /// <summary>
    /// Fix the control to a particular palette state.
    /// </summary>
    /// <param name="state">Palette state to fix.</param>
    public virtual void SetFixedState(PaletteState state)
    {
        if (state is PaletteState.Normal or PaletteState.Disabled)
        {
            _drawTrack.FixedState = state;
        }

        ViewDrawTrackPosition.FixedState = state;
    }
    #endregion

    #region Layout
    /// <summary>
    /// Find nearest value given the mouse position within track area.
    /// </summary>
    /// <param name="pt">Mouse position,</param>
    /// <returns>Nearest value.</returns>
    public int NearestValueFromPoint(Point pt)
    {
        // Grab range and current position from the bar
        var min = ViewDrawTrackBar.Minimum;
        var max = ViewDrawTrackBar.Maximum;
        var range = Math.Abs(max - min);

        // If min and max are the same, we are done!
        if (range == 0)
        {
            return min;
        }

        Rectangle trackRect = TrackArea;
        if (ViewDrawTrackBar.Orientation == Orientation.Horizontal)
        {
            if (ViewDrawTrackBar.RightToLeft == RightToLeft.Yes)
            {
                // Limit check the position
                if (pt.X <= trackRect.X)
                {
                    return max;
                }
                else if (pt.X >= (trackRect.Right - 1))
                {
                    return min;
                }
                else
                {
                    float offset = trackRect.Right - pt.X;
                    var x = offset / trackRect.Width;
                    var y = min + (x * range);
                    var ret = (int)Math.Round(y, 0, MidpointRounding.AwayFromZero);
                    return ret;
                }
            }
            else
            {
                // Limit check the position
                if (pt.X <= trackRect.X)
                {
                    return min;
                }
                else if (pt.X >= (trackRect.Right - 1))
                {
                    return max;
                }
                else
                {
                    float offset = pt.X - trackRect.X;
                    var x = offset / trackRect.Width;
                    var y = min + (x * range);
                    var ret = (int)Math.Round(y, 0, MidpointRounding.AwayFromZero);
                    return ret;
                }
            }
        }
        else
        {
            // Limit check the position
            if (pt.Y <= trackRect.Y)
            {
                return max;
            }
            else if (pt.Y >= (trackRect.Bottom - 1))
            {
                return min;
            }

            {
                float offset = trackRect.Bottom - pt.Y;
                var x = offset / trackRect.Height;
                var y = min + (x * range);
                var ret = (int)Math.Round(y, 0, MidpointRounding.AwayFromZero);
                return ret;
            }
        }
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

        // How big would the track and position indicator like to be?
        Size trackSize = _drawTrack.GetPreferredSize(context);
        Size positionSize = ViewDrawTrackPosition.GetPreferredSize(context);

        // Grab range and current position from the bar
        var min = ViewDrawTrackBar.Minimum;
        var max = ViewDrawTrackBar.Maximum;
        var range = max - min;
        var offset = ViewDrawTrackBar.Value - min;

        Rectangle trackRect = ClientRectangle;
        Rectangle positionRect = ClientRectangle;

        if (ViewDrawTrackBar.Orientation == Orientation.Horizontal)
        {
            float valueLength = ClientWidth - positionSize.Width;

            if (ViewDrawTrackBar.RightToLeft == RightToLeft.Yes)
            {
                if (valueLength > 0)
                {
                    positionRect.X = positionRect.Right - positionSize.Width - (int)Math.Round(valueLength / range * offset, 0, MidpointRounding.AwayFromZero);
                }
            }
            else
            {
                if (valueLength > 0)
                {
                    positionRect.X += (int)Math.Round(valueLength / range * offset, 0, MidpointRounding.AwayFromZero);
                }
            }

            trackRect.Y += (ClientHeight - trackSize.Height) / 2;
            trackRect.Height = trackSize.Height;

            positionRect.Y += (ClientHeight - positionSize.Height) / 2;
            positionRect.Height = positionSize.Height;
            positionRect.Width = positionSize.Width;
        }
        else
        {
            float valueLength = ClientHeight - positionSize.Height;
            if (valueLength > 0)
            {
                positionRect.Y = positionRect.Bottom - positionSize.Height - (int)Math.Round(valueLength / range * offset, 0, MidpointRounding.AwayFromZero);
            }

            trackRect.X += (ClientWidth - trackSize.Width) / 2;
            trackRect.Width = trackSize.Width;

            positionRect.X += (ClientWidth - positionSize.Width) / 2;
            positionRect.Width = positionSize.Width;
            positionRect.Height = positionSize.Height;
        }

        context.DisplayRectangle = trackRect;
        _drawTrack.Layout(context);
        context.DisplayRectangle = positionRect;
        ViewDrawTrackPosition.Layout(context);
        context.DisplayRectangle = ClientRectangle;
    }
    #endregion

    #region Implementation
    private Rectangle TrackArea
    {
        get
        {
            // Start with entire track area
            Rectangle positionRect = ClientRectangle;

            // Reduce each end by half the position indicator size
            Rectangle trackArea = ViewDrawTrackPosition.ClientRectangle;
            if (ViewDrawTrackBar.Orientation == Orientation.Horizontal)
            {
                positionRect.Width -= trackArea.Width;
                positionRect.X += trackArea.Width / 2;
            }
            else
            {
                positionRect.Height -= trackArea.Height;
                positionRect.Y += trackArea.Height / 2;
            }

            return positionRect;
        }
    }
    #endregion
}
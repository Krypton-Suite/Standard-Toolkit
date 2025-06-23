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

internal class KryptonSplitContainerBehavior : Behavior
{
    #region Instance Fields
    private readonly KryptonSplitContainer? _splitContainer;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonSplitContainerBehavior class.
    /// </summary>
    /// <param name="relatedDesigner">Reference to the containing designer.</param>
    public KryptonSplitContainerBehavior(IDesigner relatedDesigner) => _splitContainer = relatedDesigner.Component as KryptonSplitContainer;

    #endregion

    #region Public Overrides
    /// <summary>
    ///  Called when any mouse-enter message enters the adorner window of the BehaviorService.
    /// </summary>
    /// <param name="g">A Glyph.</param>
    /// <returns>true if the message was handled; otherwise, false.</returns>
    public override bool OnMouseEnter(Glyph? g)
    {
        // Notify the split container so it can track mouse message
        _splitContainer?.DesignMouseEnter();

        return base.OnMouseEnter(g);
    }

    /// <summary>
    ///  Called when any mouse-down message enters the adorner window of the BehaviorService.
    /// </summary>
    /// <param name="g">A Glyph.</param>
    /// <param name="button">A MouseButtons value indicating which button was clicked.</param>
    /// <param name="pt">The location at which the click occurred.</param>
    /// <returns>true if the message was handled; otherwise, false.</returns>
    public override bool OnMouseDown(Glyph? g, MouseButtons button, Point pt)
    {
        /*
         * base.OnMouseMove expects valid references/params and does not handle nulls
         * base is in class/assembly System.Windows.Forms.Design.Behavior.Behavior and of course cannot be altered.
         */

        // Glyph g cannot be null, also not in base.OnMouseDown
        if (g is not null && _splitContainer is not null)
        {
            // Convert the adorner coordinate to the split container client coordinate
            Point splitPt = PointToSplitContainer(g, pt);

            // Notify the split container so it can track mouse message
            if (_splitContainer.DesignMouseDown(splitPt, button))
            {
                // Splitter is starting to be moved, we need to capture mouse input
                _splitContainer.Capture = true;
            }

            return base.OnMouseDown(g, button, pt);
        }

        return false;
    }

    /// <summary>
    ///  Called when any mouse-move message enters the adorner window of the BehaviorService.
    /// </summary>
    /// <param name="g">A Glyph.</param>
    /// <param name="button">A MouseButtons value indicating which button was clicked.</param>
    /// <param name="pt">The location at which the move occurred.</param>
    /// <returns>true if the message was handled; otherwise, false.</returns>
    public override bool OnMouseMove(Glyph? g, MouseButtons button, Point pt)
    {
        /*
         * base.OnMouseMove expects valid references/params and does not handle nulls
         * base is in class/assembly System.Windows.Forms.Design.Behavior.Behavior and of course cannot be altered.
         */

        // Glyph g cannot be null, also not in base.OnMouseDown
        if (g is not null && _splitContainer is not null)
        {
            // Convert the adorner coordinate to the split container client coordinate
            Point splitPt = PointToSplitContainer(g, pt);

            // Notify the split container so it can track mouse message
            _splitContainer.DesignMouseMove(splitPt);

            return base.OnMouseMove(g, button, pt);
        }

        return false;
    }

    /// <summary>
    ///  Called when any mouse-up message enters the adorner window of the BehaviorService.
    /// </summary>
    /// <param name="g">A Glyph.</param>
    /// <param name="button">A MouseButtons value indicating which button was clicked.</param>
    /// <returns>true if the message was handled; otherwise, false.</returns>
    public override bool OnMouseUp(Glyph? g, MouseButtons button)
    {
        // Notify the split container so it can track mouse message
        _splitContainer?.DesignMouseUp(button);

        return base.OnMouseUp(g, button);
    }

    /// <summary>
    ///  Called when any mouse-leave message enters the adorner window of the BehaviorService.
    /// </summary>
    /// <param name="g">A Glyph.</param>
    /// <returns>true if the message was handled; otherwise, false.</returns>
    public override bool OnMouseLeave(Glyph? g)
    {
        // Notify the split container so it can track mouse message
        _splitContainer?.DesignMouseLeave();

        return base.OnMouseLeave(g);
    }
    #endregion

    #region Implementation Static
    private static Point PointToSplitContainer(Glyph g, Point pt)
    {
        // Cast the correct type
        var splitGlyph = (KryptonSplitContainerGlyph)g;

        // Gets the bounds of the glyph in adorner coordinates
        Rectangle bounds = splitGlyph.Bounds;

        // Convert from adorner coordinates to the control client coordinates
        return new Point(pt.X - bounds.X, pt.Y - bounds.Y);
    }
    #endregion
}
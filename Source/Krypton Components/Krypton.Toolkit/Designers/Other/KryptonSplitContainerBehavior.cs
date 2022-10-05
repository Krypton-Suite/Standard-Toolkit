﻿namespace Krypton.Toolkit
{
    internal class KryptonSplitContainerBehavior : Behavior
    {
        #region Instance Fields
        private readonly KryptonSplitContainer _splitContainer;
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
        public override bool OnMouseEnter(Glyph g)
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
        public override bool OnMouseDown(Glyph g, MouseButtons button, Point pt)
        {
            if (_splitContainer != null)
            {
                // Convert the adorner coordinate to the split container client coordinate
                Point splitPt = PointToSplitContainer(g, pt);

                // Notify the split container so it can track mouse message
                if (_splitContainer.DesignMouseDown(splitPt, button))
                {
                    // Splitter is starting to be moved, we need to capture mouse input
                    _splitContainer.Capture = true;
                }
            }

            return base.OnMouseDown(g, button, pt);
        }

        /// <summary>
        ///  Called when any mouse-move message enters the adorner window of the BehaviorService.
        /// </summary>
        /// <param name="g">A Glyph.</param>
        /// <param name="button">A MouseButtons value indicating which button was clicked.</param>
        /// <param name="pt">The location at which the move occurred.</param>
        /// <returns>true if the message was handled; otherwise, false.</returns>
        public override bool OnMouseMove(Glyph g, MouseButtons button, Point pt)
        {
            if (_splitContainer != null)
            {
                // Convert the adorner coordinate to the split container client coordinate
                Point splitPt = PointToSplitContainer(g, pt);

                // Notify the split container so it can track mouse message
                _splitContainer.DesignMouseMove(splitPt);
            }

            return base.OnMouseMove(g, button, pt);
        }

        /// <summary>
        ///  Called when any mouse-up message enters the adorner window of the BehaviorService.
        /// </summary>
        /// <param name="g">A Glyph.</param>
        /// <param name="button">A MouseButtons value indicating which button was clicked.</param>
        /// <returns>true if the message was handled; otherwise, false.</returns>
        public override bool OnMouseUp(Glyph g, MouseButtons button)
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
        public override bool OnMouseLeave(Glyph g)
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
            KryptonSplitContainerGlyph splitGlyph = (KryptonSplitContainerGlyph)g;

            // Gets the bounds of the glyph in adorner coordinates
            Rectangle bounds = splitGlyph.Bounds;

            // Convert from adorner coordinates to the control client coordinates
            return new Point(pt.X - bounds.X, pt.Y - bounds.Y);
        }
        #endregion
    }
}

﻿namespace Krypton.Toolkit
{
    /// <summary>
    /// Details for an event that discovers the rectangle that the mouse has to leave to begin dragging.
    /// </summary>
    public class ButtonDragRectangleEventArgs : EventArgs
    {
        #region Instance Fields

        private Rectangle _dragRect;

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonDragRectangleEventArgs class.
        /// </summary>
        /// <param name="point">Left mouse down point.</param>
        public ButtonDragRectangleEventArgs(Point point)
        {
            Point = point;
            _dragRect = new Rectangle(Point, Size.Empty);
            _dragRect.Inflate(SystemInformation.DragSize);
            PreDragOffset = true;
        }
        #endregion

        #region Point
        /// <summary>
        /// Gets access to the left mouse down point.
        /// </summary>
        public Point Point { get; }

        #endregion

        #region DragRect
        /// <summary>
        /// Gets access to the drag rectangle area.
        /// </summary>
        public Rectangle DragRect
        {
            get => _dragRect;
            set => _dragRect = value;
        }
        #endregion

        #region PreDragOffset
        /// <summary>
        /// Gets and sets the need for pre drag offset events.
        /// </summary>
        public bool PreDragOffset { get; set; }

        #endregion
    }
}

#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Krypton.Toolkit
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

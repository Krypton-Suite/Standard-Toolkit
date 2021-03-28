using System;
using System.Drawing;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Details for an event that provides a Point value.
    /// </summary>
    public class PointEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PointEventArgs class.
        /// </summary>
        /// <param name="point">Point associated with event.</param>
        public PointEventArgs(Point point)
        {
            Point = point;
        }
        #endregion

        #region Point
        /// <summary>
        /// Gets and sets the point.
        /// </summary>
        public Point Point { get; set; }

        #endregion
    }
}

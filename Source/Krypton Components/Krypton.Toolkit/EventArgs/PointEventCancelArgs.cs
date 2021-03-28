using System.ComponentModel;
using System.Drawing;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Details for an cancellable event that provides a Point value.
    /// </summary>
    public class PointEventCancelArgs : CancelEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PointEventCancelArgs class.
        /// </summary>
        /// <param name="point">Point associated with event.</param>
        public PointEventCancelArgs(Point point)
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

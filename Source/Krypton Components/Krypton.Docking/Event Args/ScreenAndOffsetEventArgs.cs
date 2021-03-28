using System;
using System.Drawing;

namespace Krypton.Docking
{
    /// <summary>
    /// Event arguments for events that need a screen point and element offset.
    /// </summary>
    public class ScreenAndOffsetEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ScreenAndOffsetEventArgs class.
        /// </summary>
        /// <param name="screenPoint">Screen point.</param>
        /// <param name="elementOffset">Element offset.</param>
        public ScreenAndOffsetEventArgs(Point screenPoint, Point elementOffset)
        {
            ScreenPoint = screenPoint;
            ElementOffset = elementOffset;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the screen point.
        /// </summary>
        public Point ScreenPoint { get; }

        /// <summary>
        /// Gets the element offset.
        /// </summary>
        public Point ElementOffset { get; }

        #endregion
    }
}

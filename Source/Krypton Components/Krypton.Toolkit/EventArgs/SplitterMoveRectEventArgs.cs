using System;
using System.Drawing;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Provides a movement rectangle that will be used to limit separator movement.
    /// </summary>
    public class SplitterMoveRectMenuArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the SplitterMoveRectMenuArgs class.
        /// </summary>
        /// <param name="moveRect">Initial movement rectangle that limits separator movements.</param>
        public SplitterMoveRectMenuArgs(Rectangle moveRect)
        {
            MoveRect = moveRect;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the movement box for a separator.
        /// </summary>
        public Rectangle MoveRect { get; set; }

        #endregion
    }
}

using System;
using System.Diagnostics;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Details for button specification related events.
    /// </summary>
    public class ButtonSpecEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecEventArgs class.
        /// </summary>
        /// <param name="spec">Button spec effected by event.</param>
        /// <param name="index">Index of page in the owning collection.</param>
        public ButtonSpecEventArgs(ButtonSpec spec, int index)
        {
            Debug.Assert(spec != null);
            Debug.Assert(index >= 0);

            // Remember parameter details
            ButtonSpec = spec;
            Index = index;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the navigator button spec associated with the event.
        /// </summary>
        public ButtonSpec ButtonSpec { get; }

        /// <summary>
        /// Gets the index of ButtonSpec associated with the event.
        /// </summary>
        public int Index { get; }

        #endregion
    }
}

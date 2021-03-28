using System;

namespace Krypton.Docking
{
    /// <summary>
    /// Event arguments for events that need to provide a unique name.
    /// </summary>
    public class UniqueNameEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the UniqueNameEventArgs class.
        /// </summary>
        /// <param name="uniqueName">Unique name of page.</param>
        public UniqueNameEventArgs(string uniqueName)
        {
            UniqueName = uniqueName;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the unique name of a page.
        /// </summary>
        public string UniqueName { get; }

        #endregion
    }
}

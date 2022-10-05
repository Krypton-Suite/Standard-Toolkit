namespace Krypton.Navigator
{
    /// <summary>
    /// Provide a KryptonPageFlags enumeration with event details.
    /// </summary>
    public class KryptonPageFlagsEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPageFlagsEventArgs class.
        /// </summary>
        /// <param name="flags">KryptonPageFlags enumeration.</param>
        public KryptonPageFlagsEventArgs(KryptonPageFlags flags) =>
            // Remember parameter details
            Flags = flags;

        #endregion

        #region Public
        /// <summary>
        /// Gets the KryptonPageFlags enumeration value.
        /// </summary>
        public KryptonPageFlags Flags { get; }

        #endregion
    }
}

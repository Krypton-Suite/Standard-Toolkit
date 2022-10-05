namespace Krypton.Toolkit
{
    /// <summary>
    /// Manages a collection of 31 boolean flags.
    /// </summary>
    public struct BoolFlags31
    {
        #region Instance Fields

        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the entire flags value.
        /// </summary>
        public int Flags { get; set; }

        /// <summary>
        /// Set all the provided flags to true.
        /// </summary>
        /// <param name="flags">Flags to set.</param>
        /// <return>Set of flags that have changed in value.</return>
        public int SetFlags(int flags)
        {
            var before = Flags;

            // Set all the provided flags
            Flags |= flags;

            // Return set of flags that have changed value
            return before ^ Flags;
        }

        /// <summary>
        /// Clear all the provided flags to false.
        /// </summary>
        /// <param name="flags">Flags to clear.</param>
        /// <return>Set of flags that have changed in value.</return>
        public int ClearFlags(int flags)
        {
            var before = Flags;

            // Clear all the provided flags
            Flags &= ~flags;

            // Return set of flags that have changed value
            return before ^ Flags;
        }

        /// <summary>
        /// Are all the provided flags set to true.
        /// </summary>
        /// <param name="flags">Flags to test.</param>
        /// <returns>True if all flags are set; otherwise false.</returns>
        public bool AreFlagsSet(int flags) => (Flags & flags) == flags;

        #endregion
    }
}

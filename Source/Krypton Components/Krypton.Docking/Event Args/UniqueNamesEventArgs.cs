namespace Krypton.Docking
{
    /// <summary>
    /// Event arguments for events that need to provide a set of unique names.
    /// </summary>
    public class UniqueNamesEventArgs : EventArgs
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the UniqueNamesEventArgs class.
        /// </summary>
        /// <param name="uniqueNames">Array of unique names.</param>
        public UniqueNamesEventArgs(IReadOnlyList<string> uniqueNames) => UniqueNames = uniqueNames;

        #endregion

        #region Public
        /// <summary>
        /// Gets the array of unique names associated with the event.
        /// </summary>
        public IReadOnlyList<string> UniqueNames { get; }

        #endregion
    }
}

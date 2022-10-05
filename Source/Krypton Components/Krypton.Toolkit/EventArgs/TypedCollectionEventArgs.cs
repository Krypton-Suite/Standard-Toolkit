namespace Krypton.Toolkit
{
    /// <summary>
    /// Delegate used for hooking into TypedCollection events.
    /// </summary>
    /// <typeparam name="T">Type of the item inside the TypedCollection.</typeparam>
    public delegate void TypedHandler<T>(object sender, TypedCollectionEventArgs<T> e)  where T : class;

    /// <summary>
    /// Details for typed collection related events.
    /// </summary>
    public class TypedCollectionEventArgs<T> : EventArgs where T : class
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the TypedCollectionEventArgs class.
        /// </summary>
        /// <param name="item">Item effected by event.</param>
        /// <param name="index">Index of page in the owning collection.</param>
        public TypedCollectionEventArgs(T item, int index)
        {
            // Remember parameter details
            Item = item;
            Index = index;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the item associated with the event.
        /// </summary>
        public T Item { get; }

        /// <summary>
        /// Gets the index of the item associated with the event.
        /// </summary>
        public int Index { get; }

        #endregion
    }
}

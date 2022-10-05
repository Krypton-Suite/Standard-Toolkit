namespace Krypton.Toolkit
{
    /// <summary>
    /// Collection for managing ButtonSpecAny instances.
    /// </summary>
    public class DataGridViewColumnSpecCollection : ButtonSpecCollection<ButtonSpecAny>
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the DataGridViewColumnSpecCollection class.
        /// </summary>
        /// <param name="owner">Reference to owning object.</param>
        public DataGridViewColumnSpecCollection(object owner)
            : base(owner)
        {
        }
        #endregion
    }
}

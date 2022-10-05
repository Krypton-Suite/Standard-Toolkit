namespace Krypton.Ribbon
{
    /// <summary>
    /// Quick access toolbar can contain any component that implements the IQuickAccessToolbarEntry
    /// </summary>
    public class KryptonRibbonQATButtonCollection : TypedRestrictCollection<Component>
    {
        #region Static Fields
        private static readonly Type[] _types = { typeof(IQuickAccessToolbarButton) };
        #endregion

        #region Restrict
        /// <summary>
        /// Gets an array of types that the collection is restricted to contain.
        /// </summary>
        public override Type[] RestrictTypes => _types;

        #endregion
    }
}

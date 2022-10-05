namespace Krypton.Toolkit
{
    /// <summary>
    /// Create a default value attribute for color property.
    /// </summary>
    public sealed class KryptonDefaultColorAttribute : DefaultValueAttribute
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDefaultColorAttribute class.
        /// </summary>
        public KryptonDefaultColorAttribute()
            : base(Color.Empty)
        {
        }
        #endregion
    }
}

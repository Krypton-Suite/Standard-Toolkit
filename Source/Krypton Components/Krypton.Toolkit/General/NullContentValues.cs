namespace Krypton.Toolkit
{
    /// <summary>
    /// Implements the IContentValues interface by providing null information.
    /// </summary>
    public class NullContentValues : IContentValues
    {
        #region IContentValues
        /// <summary>
        /// Gets the content short text.
        /// </summary>
        /// <returns>String value.</returns>
        public virtual string GetShortText() => string.Empty;

        /// <summary>
        /// Gets the content image.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public virtual Image GetImage(PaletteState state) => null;

        /// <summary>
        /// Gets the image color that should be transparent.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Color value.</returns>
        public virtual Color GetImageTransparentColor(PaletteState state) => Color.Empty;

        /// <summary>
        /// Gets the content long text.
        /// </summary>
        /// <returns>String value.</returns>
        public virtual string GetLongText() => string.Empty;

        #endregion
    }
}

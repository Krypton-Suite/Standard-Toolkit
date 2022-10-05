namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for button content value information.
    /// </summary>
    public class CheckButtonValues : ButtonValues
    {
        #region Instance Fields
        private CheckButtonImageStates _imageStates;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the CheckButtonValues class.
        /// </summary>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public CheckButtonValues(NeedPaintHandler needPaint)
            : base(needPaint)
        {
        }
        #endregion

        #region CreateImageStates
        /// <summary>
        /// Create the storage for the image states.
        /// </summary>
        /// <returns>Storage object.</returns>
        protected override ButtonImageStates CreateImageStates()
        {
            _imageStates = new CheckButtonImageStates();
            return _imageStates;
        }
        #endregion

        #region IContentValues
        /// <summary>
        /// Gets the content image.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public override Image GetImage(PaletteState state)
        {
            // Try and get a state specific image
            Image image = state switch
            {
                PaletteState.CheckedNormal => _imageStates.ImageCheckedNormal,
                PaletteState.CheckedPressed => _imageStates.ImageCheckedPressed,
                PaletteState.CheckedTracking => _imageStates.ImageCheckedTracking,
                _ => null
            };

            return image ?? base.GetImage(state);
        }
        #endregion
    }
}

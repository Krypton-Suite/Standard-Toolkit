namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for standard header storage.
    /// </summary>
    public class HeaderValues : HeaderValuesBase
    {
        #region Static Fields
        private const string _defaultHeading = "Heading";
        private const string _defaultDescription = "Description";
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the HeaderValues class.
        /// </summary>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public HeaderValues(NeedPaintHandler needPaint)
            : base(needPaint)
        {
        }
        #endregion

        #region Default Values
        /// <summary>
        /// Gets the default heading value.
        /// </summary>
        /// <returns>String reference.</returns>
        protected override string GetHeadingDefault()
        {
            return _defaultHeading;
        }

        /// <summary>
        /// Gets the default description value.
        /// </summary>
        /// <returns>String reference.</returns>
        protected override string GetDescriptionDefault()
        {
            return _defaultDescription;
        }
        #endregion
    }
}

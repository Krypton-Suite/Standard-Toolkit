namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that HeaderStyle values appear as neat text at design time.
    /// </summary>
    internal class HeaderStyleConverter : StringLookupConverter
    {
        #region Static Fields

        private readonly Pair[] _pairs =
        {
            new(HeaderStyle.Primary, "Primary"),
            new(HeaderStyle.Secondary, "Secondary"),
            new(HeaderStyle.DockInactive, "Dock - Inactive"),
            new(HeaderStyle.DockActive, "Dock - Active"),
            new(HeaderStyle.Form, "Form"),
            new(HeaderStyle.Calendar, "Calendar"),
            new(HeaderStyle.Custom1, "Custom1"),
            new(HeaderStyle.Custom2, "Custom2"),
            new(HeaderStyle.Custom3, "Custom3")
        };
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the HeaderStyleConverter class.
        /// </summary>
        public HeaderStyleConverter()
            : base(typeof(HeaderStyle))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs => _pairs;

        #endregion
    }
}

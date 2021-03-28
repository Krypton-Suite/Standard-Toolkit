namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that HeaderStyle values appear as neat text at design time.
    /// </summary>
    internal class HeaderStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the HeaderStyleConverter clas.
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
        protected override Pair[] Pairs { get; } =
        { new Pair(HeaderStyle.Primary,      "Primary"),
            new Pair(HeaderStyle.Secondary,    "Secondary"), 
            new Pair(HeaderStyle.DockInactive, "Dock - Inactive"), 
            new Pair(HeaderStyle.DockActive,   "Dock - Active"), 
            new Pair(HeaderStyle.Form,         "Form"), 
            new Pair(HeaderStyle.Calendar,     "Calendar"), 
            new Pair(HeaderStyle.Custom1,      "Custom1"),
            new Pair(HeaderStyle.Custom2,      "Custom2"),
            new Pair(HeaderStyle.Custom3,      "Custom3")
        };

        #endregion
    }
}

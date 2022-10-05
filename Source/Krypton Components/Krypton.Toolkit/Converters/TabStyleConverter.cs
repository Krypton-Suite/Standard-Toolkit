namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that TabStyle values appear as neat text at design time.
    /// </summary>
    internal class TabStyleConverter : StringLookupConverter
    {
        #region Static Fields

        private readonly Pair[] _pairs =
        {
            new(TabStyle.HighProfile, "High Profile"),
            new(TabStyle.StandardProfile, "Standard Profile"),
            new(TabStyle.LowProfile, "Low Profile"),
            new(TabStyle.OneNote, "OneNote"),
            new(TabStyle.Dock, "Dock"),
            new(TabStyle.DockAutoHidden, "Dock AutoHidden"),
            new(TabStyle.Custom1, "Custom1"),
            new(TabStyle.Custom2, "Custom2"),
            new(TabStyle.Custom3, "Custom3")
        };
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the TabStyleConverter class.
        /// </summary>
        public TabStyleConverter()
            : base(typeof(TabStyle))
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

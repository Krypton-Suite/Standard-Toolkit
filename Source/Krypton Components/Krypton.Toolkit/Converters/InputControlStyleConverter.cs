namespace Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that InputControl values appear as neat text at design time.
    /// </summary>
    internal class InputControlStyleConverter : StringLookupConverter
    {
        #region Static Fields

        private readonly Pair[] _pairs =
        {
            new(InputControlStyle.Standalone, "Standalone"),
            new(InputControlStyle.Ribbon, "Ribbon"),
            new(InputControlStyle.Custom1, "Custom1"),
            new(InputControlStyle.Custom2, "Custom2"),
            new(InputControlStyle.Custom3, "Custom3"),
            new(InputControlStyle.PanelClient, "Panel Client"),
            new(InputControlStyle.PanelAlternate, "Panel Alternate"),
            // new(InputControlStyle.Disabled, "Disabled")
        };
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the InputControlStyleConverter class.
        /// </summary>
        public InputControlStyleConverter()
            : base(typeof(InputControlStyle))
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

namespace Krypton.Navigator
{
    /// <summary>
    /// Custom type converter so that ButtonDisplay values appear as neat text at design time.
    /// </summary>
    public class ButtonDisplayConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion
                                             
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonDisplayConverter class.
        /// </summary>
        public ButtonDisplayConverter()
            : base(typeof(ButtonDisplay))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new(ButtonDisplay.Hide,           "Hide"),
            new(ButtonDisplay.ShowDisabled,   "Show Disabled"),
            new(ButtonDisplay.ShowEnabled,    "Show Enabled"),
            new(ButtonDisplay.Logic,          "Logic") };

        #endregion
    }
}

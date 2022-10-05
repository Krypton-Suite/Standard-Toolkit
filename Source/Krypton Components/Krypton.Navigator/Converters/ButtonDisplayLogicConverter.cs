namespace Krypton.Navigator
{
    /// <summary>
    /// Custom type converter so that ButtonDisplayLogic values appear as neat text at design time.
    /// </summary>
    public class ButtonDisplayLogicConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion
                                             
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonDisplayLogicConverter class.
        /// </summary>
        public ButtonDisplayLogicConverter()
            : base(typeof(ButtonDisplayLogic))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new(ButtonDisplayLogic.None,                  "None"),
            new(ButtonDisplayLogic.Context,               "Context"),
            new(ButtonDisplayLogic.NextPrevious,          "Next/Previous"),
            new(ButtonDisplayLogic.ContextNextPrevious,   "Context & Next/Previous") };

        #endregion
    }
}

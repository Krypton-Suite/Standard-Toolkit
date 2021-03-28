using Krypton.Toolkit;

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
        /// Initialize a new instance of the ButtonDisplayConverter clas.
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
        { new Pair(ButtonDisplay.Hide,           "Hide"),
            new Pair(ButtonDisplay.ShowDisabled,   "Show Disabled"),
            new Pair(ButtonDisplay.ShowEnabled,    "Show Enabled"),
            new Pair(ButtonDisplay.Logic,          "Logic") };

        #endregion
    }
}

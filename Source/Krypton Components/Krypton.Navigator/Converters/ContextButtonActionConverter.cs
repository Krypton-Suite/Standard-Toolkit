using Krypton.Toolkit;

namespace Krypton.Navigator
{
    /// <summary>
    /// Custom type converter so that ContextButtonAction values appear as neat text at design time.
    /// </summary>
    public class ContextButtonActionConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion
                                             
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ContextButtonActionConverter clas.
        /// </summary>
        public ContextButtonActionConverter()
            : base(typeof(ContextButtonAction))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new Pair(ContextButtonAction.None,         "None (Do nothing)"),
            new Pair(ContextButtonAction.SelectPage,   "Select Page") };

        #endregion
    }
}

using Krypton.Toolkit;

namespace Krypton.Navigator
{
    /// <summary>
    /// Custom type converter so that DirectionButtonAction values appear as neat text at design time.
    /// </summary>
    public class DirectionButtonActionConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion
                                             
        #region Identity
        /// <summary>
        /// Initialize a new instance of the DirectionButtonActionConverter clas.
        /// </summary>
        public DirectionButtonActionConverter()
            : base(typeof(DirectionButtonAction))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new Pair(DirectionButtonAction.None,                   "None (Do nothing)"),
            new Pair(DirectionButtonAction.SelectPage,             "Select Page"),
            new Pair(DirectionButtonAction.MoveBar,                "Move Bar"),
            new Pair(DirectionButtonAction.ModeAppropriateAction,  "Mode Appropriate Action") };

        #endregion
    }
}

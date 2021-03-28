using Krypton.Toolkit;

namespace Krypton.Navigator
{
    /// <summary>
    /// Custom type converter so that PopupPageAllow values appear as neat text at design time.
    /// </summary>
    public class PopupPageAllowConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion
                                             
        #region Identity
        /// <summary>
        /// Initialize a new instance of the PopupPagePositionConverter clas.
        /// </summary>
        public PopupPageAllowConverter()
            : base(typeof(PopupPageAllow))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new Pair(PopupPageAllow.Never,                 "Never"),
            new Pair(PopupPageAllow.OnlyCompatibleModes,   "Only Compatible Modes"),
            new Pair(PopupPageAllow.OnlyOutlookMiniMode,   "Only Outlook Mini Mode")};

        #endregion
    }
}

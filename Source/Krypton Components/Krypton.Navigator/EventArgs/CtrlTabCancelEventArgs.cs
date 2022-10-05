namespace Krypton.Navigator
{
    /// <summary>
    /// Details for control tabbing events.
    /// </summary>
    public class CtrlTabCancelEventArgs : CancelEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the CtrlTabCancelEventArgs class.
        /// </summary>
        /// <param name="forward">Tabbing in forward or backwards direction.</param>
        public CtrlTabCancelEventArgs(bool forward) => Forward = forward;

        #endregion

        #region Forward
        /// <summary>
        /// Gets a value indicating if control tabbing forward.
        /// </summary>
        public bool Forward { get; }

        #endregion
    }
}

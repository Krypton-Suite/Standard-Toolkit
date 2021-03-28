namespace Krypton.Docking
{
    /// <summary>
    /// Event arguments for the PageCloseRequest event.
    /// </summary>
    public class CloseRequestEventArgs : UniqueNameEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the CloseRequestEventArgs class.
        /// </summary>
        /// <param name="uniqueName">Unique name of the page associated with the event.</param>
        /// <param name="closeRequest">Initial close action to use.</param>
        public CloseRequestEventArgs(string uniqueName, DockingCloseRequest closeRequest)
            : base(uniqueName)
        {
            CloseRequest = closeRequest;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the close action to be performed.
        /// </summary>
        public DockingCloseRequest CloseRequest { get; set; }

        #endregion
    }
}

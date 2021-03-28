using System.ComponentModel;

using Krypton.Navigator;

namespace Krypton.Workspace
{
    /// <summary>
    /// Event arguments for events that need to request a KryptonPage from a provided unique name.
    /// </summary>
    public class RecreateLoadingPageEventArgs : CancelEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the RecreateLoadingPageEventArgs class.
        /// </summary>
        /// <param name="uniqueName">Unique name of the page that needs creating.</param>
        public RecreateLoadingPageEventArgs(string uniqueName)
            : base(false)
        {
            UniqueName = uniqueName;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the page to be used for the requested unique name.
        /// </summary>
        public KryptonPage Page { get; set; }

        /// <summary>
        /// Gets the unique name of the page requested to be recreated.
        /// </summary>
        public string UniqueName { get; }

        #endregion
    }
}

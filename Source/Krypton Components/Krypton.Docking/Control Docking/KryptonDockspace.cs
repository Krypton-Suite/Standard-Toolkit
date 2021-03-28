using System.Drawing;
using System.ComponentModel;

namespace Krypton.Docking
{
    /// <summary>
    /// Extends the KryptonWorkspace to work within the docking edge of a control.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonDockspace : KryptonSpace
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDockspace class.
        /// </summary>
        public KryptonDockspace()
            : base("Docked")
        {
            // Define a sensible default minimum size
            MinimumSize = new Size(22, 22);
        }

        /// <summary>
        /// Gets a string representation of the class.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "KryptonDockspace " + Dock.ToString();
        }
        #endregion
    }
}

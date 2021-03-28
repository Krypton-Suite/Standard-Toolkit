using System.Drawing;
using Krypton.Navigator;

namespace Krypton.Docking
{
    /// <summary>
    /// Target that never matches and so acts as a null drag target.
    /// </summary>
    public class DragTargetNull : DragTarget
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the DragTargetNull class.
        /// </summary>
        public DragTargetNull()
            : base(Rectangle.Empty, Rectangle.Empty, Rectangle.Empty, DragTargetHint.None, KryptonPageFlags.All)
        {
        }
        #endregion

        #region Public
        /// <summary>
        /// Perform the drop action associated with the target.
        /// </summary>
        /// <param name="screenPt">Position in screen coordinates.</param>
        /// <param name="data">Data to pass to the target to process drop.</param>
        /// <returns>Drop was performed and the source can perform any removal of pages as required.</returns>
        public override bool PerformDrop(Point screenPt, PageDragEndData data)
        {
            return true;
        }
        #endregion
    }
}

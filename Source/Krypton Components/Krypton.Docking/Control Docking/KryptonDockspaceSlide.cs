using System.ComponentModel;
using Krypton.Navigator;
using Krypton.Workspace;

namespace Krypton.Docking
{
    /// <summary>
    /// Extends the KryptonWorkspace to work within the docking edge of a control.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonDockspaceSlide : KryptonDockspace
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDockspaceSlide class.
        /// </summary>
        public KryptonDockspaceSlide()
        {
            // Cannot drag pages inside the sliding dockspace
            AllowPageDrag = false;
        }
        #endregion

        #region Protectect
        /// <summary>
        /// Initialize a new cell.
        /// </summary>
        /// <param name="cell">Cell being added to the control.</param>
        protected override void NewCellInitialize(KryptonWorkspaceCell cell)
        {
            // Let base class perform event hooking and customizations
            base.NewCellInitialize(cell);

            // We only ever show a single page in the dockspace, so remove default 
            // tabbed appearance and instead use a header group mode instead
            cell.NavigatorMode = NavigatorMode.HeaderGroup;
        }
        #endregion
    }
}

﻿namespace Krypton.Workspace
{
    internal class KryptonWorkspaceCellDesigner : KryptonNavigatorDesigner
    {
        #region Public
        /// <summary>
        /// Gets the selection rules that indicate the movement capabilities of a component.
        /// </summary>
        public override SelectionRules SelectionRules => SelectionRules.None;

        #endregion

        #region Implementation
        /// <summary>
        /// Occurs when the component is being removed from the designer.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A ComponentEventArgs containing event data.</param>
        protected override void OnComponentRemoving(object sender, ComponentEventArgs e)
        {
            // If our control is being removed
            if (e.Component == Navigator)
            {
                // If this workspace cell is inside a parent
                KryptonWorkspaceCell cell = (KryptonWorkspaceCell)Navigator;
                // Cell an only be inside a workspace sequence
                KryptonWorkspaceSequence sequence = (KryptonWorkspaceSequence)cell.WorkspaceParent;
                // Remove the cell from the parent
                sequence?.Children.Remove(cell);
            }

            base.OnComponentRemoving(sender, e);
        }
        #endregion
    }
}
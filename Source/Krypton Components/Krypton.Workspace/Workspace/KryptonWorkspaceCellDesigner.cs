// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

using System.ComponentModel.Design;
using System.Windows.Forms.Design;

using Krypton.Navigator;

namespace Krypton.Workspace
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
                KryptonWorkspaceSequence sequence = (KryptonWorkspaceSequence) cell.WorkspaceParent;
                // Remove the cell from the parent
                sequence?.Children.Remove(cell);
            }

            base.OnComponentRemoving(sender, e);
        }
        #endregion
    }
}

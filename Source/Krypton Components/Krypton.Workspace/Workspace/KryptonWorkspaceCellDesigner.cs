#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Workspace;

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
    protected override void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        // If our control is being removed
        if (e.Component == Navigator)
        {
            // If this workspace cell is inside a parent
            var cell = Navigator as KryptonWorkspaceCell ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(Navigator)));                
            // Cell an only be inside a workspace sequence
            var sequence = cell.WorkspaceParent as KryptonWorkspaceSequence;
            // Remove the cell from the parent
            sequence?.Children?.Remove(cell);
        }

        base.OnComponentRemoving(sender, e);
    }
    #endregion
}
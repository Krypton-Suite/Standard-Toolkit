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

/// <summary>
/// Workspace specific separator that works relative to a specific workspace item.
/// </summary>
public class ViewDrawWorkspaceSeparator : ViewDrawSeparator,
    ISeparatorSource
{
    #region Instance Fields
    private readonly KryptonWorkspace _workspace;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawWorkspaceSeparator class.
    /// </summary>
    /// <param name="workspace">Associated workspace instance.</param>
    /// <param name="workspaceItem">Associated workspace item.</param>
    /// <param name="orientation">Visual orientation of the content.</param>
    public ViewDrawWorkspaceSeparator([DisallowNull] KryptonWorkspace workspace,
        IWorkspaceItem workspaceItem,
        Orientation orientation)
        : base(workspace.StateDisabled.Separator!, workspace.StateNormal.Separator!, workspace.StateTracking, workspace.StatePressed,
            workspace.StateDisabled.Separator!, workspace.StateNormal.Separator!, workspace.StateTracking, workspace.StatePressed,
            CommonHelper.SeparatorStyleToMetricPadding(workspace.SeparatorStyle), orientation)
    {
        _workspace = workspace;
        WorkspaceItem = workspaceItem;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawWorkspaceSeparator:{Id}";

    #endregion

    #region Public
    /// <summary>
    /// Gets the associated workspace item instance.
    /// </summary>
    public IWorkspaceItem WorkspaceItem { get; }

    /// <summary>
    /// Gets the top level control of the source.
    /// </summary>
    public Control SeparatorControl => _workspace;

    /// <summary>
    /// Gets the orientation of the separator.
    /// </summary>
    public Orientation SeparatorOrientation => Orientation;

    /// <summary>
    /// Can the separator be moved by the user.
    /// </summary>
    public bool SeparatorCanMove => _workspace.SeparatorCanMove(this);

    /// <summary>
    /// Gets the amount the splitter can be incremented.
    /// </summary>
    public int SeparatorIncrements => 1;

    /// <summary>
    /// Gets the box representing the minimum and maximum allowed splitter movement.
    /// </summary>
    public Rectangle SeparatorMoveBox => _workspace.SeparatorMoveBox(this);

    /// <summary>
    /// Indicates the separator is moving.
    /// </summary>
    /// <param name="mouse">Current mouse position in client area.</param>
    /// <param name="splitter">Current position of the splitter.</param>
    /// <returns>True if movement should be cancelled; otherwise false.</returns>
    public bool SeparatorMoving(Point mouse, Point splitter) =>
        // Do nothing, we do not care
        false;

    /// <summary>
    /// Indicates the separator has finished and been moved.
    /// </summary>
    /// <param name="mouse">Current mouse position in client area.</param>
    /// <param name="splitter">Current position of the splitter.</param>
    public void SeparatorMoved(Point mouse, Point splitter) => _workspace.SeparatorMoved(this, mouse, splitter);

    /// <summary>
    /// Indicates the separator has not been moved.
    /// </summary>
    public void SeparatorNotMoved()
    {
        // Do nothing, we do not care
    }
    #endregion
}
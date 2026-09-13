#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Workspace;

/// <summary>
/// Shared designer-verb handlers that add cells and sequences to a workspace collection.
/// </summary>
internal static class KryptonWorkspaceDesignerActions
{
    /// <summary>
    /// Creates a sited <see cref="KryptonWorkspaceCell"/> and adds it to <paramref name="children"/>.
    /// </summary>
    /// <param name="workspace">Owning workspace, used for layout after the add.</param>
    /// <param name="children">Target child collection (root or a nested sequence).</param>
    /// <param name="host">Designer host used to create the component and transaction.</param>
    /// <param name="changeService">Change service used to notify the designer.</param>
    public static void AddCell(
        KryptonWorkspace? workspace,
        KryptonWorkspaceCollection? children,
        IDesignerHost? host,
        IComponentChangeService? changeService)
    {
        if (children == null)
        {
            return;
        }

        DesignerTransaction? transaction = null;
        try
        {
            transaction = host?.CreateTransaction(@"Add Cell");
            var cell = host != null
                ? (KryptonWorkspaceCell)host.CreateComponent(typeof(KryptonWorkspaceCell))
                : new KryptonWorkspaceCell();

            NotifyChanging(workspace, changeService);
            children.Add(cell);
            NotifyChanged(workspace, changeService);
            workspace?.PerformLayout();
            transaction?.Commit();
            transaction = null;
        }
        finally
        {
            transaction?.Cancel();
        }
    }

    /// <summary>
    /// Creates a nested sequence (opposite orientation to <paramref name="parentSequence"/>) containing one cell.
    /// </summary>
    /// <param name="workspace">Owning workspace, used for layout after the add.</param>
    /// <param name="parentSequence">Sequence that owns <paramref name="children"/>; used for child orientation.</param>
    /// <param name="children">Target child collection (root or a nested sequence).</param>
    /// <param name="host">Designer host used to create the components and transaction.</param>
    /// <param name="changeService">Change service used to notify the designer.</param>
    public static void AddSequence(
        KryptonWorkspace? workspace,
        KryptonWorkspaceSequence? parentSequence,
        KryptonWorkspaceCollection? children,
        IDesignerHost? host,
        IComponentChangeService? changeService)
    {
        if (children == null)
        {
            return;
        }

        DesignerTransaction? transaction = null;
        try
        {
            transaction = host?.CreateTransaction(@"Add Sequence");
            var sequence = host != null
                ? (KryptonWorkspaceSequence)host.CreateComponent(typeof(KryptonWorkspaceSequence))
                : new KryptonWorkspaceSequence();

            if (parentSequence != null)
            {
                sequence.Orientation = parentSequence.Orientation == Orientation.Vertical
                    ? Orientation.Horizontal
                    : Orientation.Vertical;
            }

            var cell = host != null
                ? (KryptonWorkspaceCell)host.CreateComponent(typeof(KryptonWorkspaceCell))
                : new KryptonWorkspaceCell();
            sequence.Children!.Add(cell);

            NotifyChanging(workspace, changeService);
            children.Add(sequence);
            NotifyChanged(workspace, changeService);
            workspace?.PerformLayout();
            transaction?.Commit();
            transaction = null;
        }
        finally
        {
            transaction?.Cancel();
        }
    }

    private static void NotifyChanging(KryptonWorkspace? workspace, IComponentChangeService? changeService)
    {
        if (workspace == null)
        {
            return;
        }

        MemberDescriptor? property = TypeDescriptor.GetProperties(workspace)[nameof(KryptonWorkspace.Root)];
        changeService?.OnComponentChanging(workspace, property);
    }

    private static void NotifyChanged(KryptonWorkspace? workspace, IComponentChangeService? changeService)
    {
        if (workspace == null)
        {
            return;
        }

        MemberDescriptor? property = TypeDescriptor.GetProperties(workspace)[nameof(KryptonWorkspace.Root)];
        changeService?.OnComponentChanged(workspace, property, null, null);
    }
}

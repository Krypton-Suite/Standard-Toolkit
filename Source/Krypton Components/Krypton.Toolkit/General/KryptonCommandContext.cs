#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Helper methods for shared <see cref="KryptonCommand"/> execution from context menu items.
/// </summary>
public static class KryptonCommandContext
{
    /// <summary>
    /// Attempts to read a <see cref="IKryptonContextMenuCommandItem.CommandParameter"/> from the execute sender.
    /// </summary>
    /// <param name="sender">The sender passed to a <see cref="KryptonCommand"/> Execute handler.</param>
    /// <param name="parameter">The command parameter when the sender implements <see cref="IKryptonContextMenuCommandItem"/>.</param>
    /// <returns><c>true</c> when the sender is a command-backed context menu item.</returns>
    public static bool TryGetCommandParameter(object? sender, out object? parameter)
    {
        if (sender is IKryptonContextMenuCommandItem item)
        {
            parameter = item.CommandParameter;
            return true;
        }

        parameter = null;
        return false;
    }

    /// <summary>
    /// Gets the <see cref="IKryptonContextMenuCommandItem.CommandParameter"/> from the execute sender, or <c>null</c>.
    /// </summary>
    /// <param name="sender">The sender passed to a <see cref="KryptonCommand"/> Execute handler.</param>
    public static object? GetCommandParameter(object? sender)
    {
        TryGetCommandParameter(sender, out var parameter);
        return parameter;
    }
}
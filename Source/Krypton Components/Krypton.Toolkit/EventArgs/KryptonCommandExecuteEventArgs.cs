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
/// Event data for a <see cref="KryptonCommand"/> Execute event raised with an originating source.
/// </summary>
public class KryptonCommandExecuteEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonCommandExecuteEventArgs"/> class.
    /// </summary>
    /// <param name="source">The object that initiated command execution.</param>
    /// <param name="parameter">Optional parameter from a shared command context menu item.</param>
    public KryptonCommandExecuteEventArgs(object source, object? parameter)
    {
        Source = source ?? throw new ArgumentNullException(nameof(source));
        Parameter = parameter;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the object that initiated command execution.
    /// </summary>
    public object Source { get; }

    /// <summary>
    /// Gets the optional parameter from a shared command context menu item.
    /// </summary>
    public object? Parameter { get; }
    #endregion
}
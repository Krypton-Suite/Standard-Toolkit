#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Provides data for the <see cref="KryptonEnumButton.SelectedValueChanging"/> and
/// <see cref="KryptonEnumCommandLinkButton.SelectedValueChanging"/> events. Set
/// <see cref="CancelEventArgs.Cancel"/> to <see langword="true"/> to veto the transition and keep the
/// current value.
/// </summary>
public class KryptonEnumButtonValueChangingEventArgs : CancelEventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonEnumButtonValueChangingEventArgs"/> class.
    /// </summary>
    /// <param name="currentValue">The value currently selected (before the change).</param>
    /// <param name="proposedValue">The value that will be selected if the change is not cancelled.</param>
    /// <param name="proposedDisplayText">The display text that would be shown for <paramref name="proposedValue"/>.</param>
    public KryptonEnumButtonValueChangingEventArgs(object? currentValue, object? proposedValue, string proposedDisplayText)
    {
        CurrentValue = currentValue;
        ProposedValue = proposedValue;
        ProposedDisplayText = proposedDisplayText;
    }

    /// <summary>Gets the value currently selected (before the change).</summary>
    public object? CurrentValue { get; }

    /// <summary>Gets the value that will be selected if the change is not cancelled.</summary>
    public object? ProposedValue { get; }

    /// <summary>Gets the display text that would be shown for <see cref="ProposedValue"/>.</summary>
    public string ProposedDisplayText { get; }
}

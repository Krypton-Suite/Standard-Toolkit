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
/// Provides data for the <see cref="KryptonEnumButton.EnumValueChanged"/> event, carrying the
/// enum value that the <see cref="KryptonEnumButton"/> cycled to along with its display text.
/// </summary>
public class KryptonEnumButtonValueChangedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonEnumButtonValueChangedEventArgs"/> class.
    /// </summary>
    /// <param name="value">The currently selected enum value. May be <see langword="null"/> when no enum type is assigned.</param>
    /// <param name="displayText">The display text shown on the button for the selected value.</param>
    public KryptonEnumButtonValueChangedEventArgs(object? value, string displayText)
    {
        Value = value;
        DisplayText = displayText;
    }

    /// <summary>
    /// Gets the currently selected enum value. May be <see langword="null"/> when the button has no
    /// <see cref="KryptonEnumButton.EnumType"/> assigned. Cast to the concrete enum type to consume it.
    /// </summary>
    public object? Value { get; }

    /// <summary>
    /// Gets the display text shown on the button for <see cref="Value"/> (the <see cref="DescriptionAttribute"/>
    /// text when present, otherwise the enum field name).
    /// </summary>
    public string DisplayText { get; }
}

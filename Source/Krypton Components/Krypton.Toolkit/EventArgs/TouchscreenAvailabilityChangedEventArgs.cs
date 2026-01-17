#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides data for the TouchscreenAvailabilityChanged event.
/// </summary>
public class TouchscreenAvailabilityChangedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the TouchscreenAvailabilityChangedEventArgs class.
    /// </summary>
    /// <param name="isAvailable">True if touchscreen is now available; false if it was removed.</param>
    /// <param name="maximumTouchContacts">The maximum number of simultaneous touch contacts supported.</param>
    public TouchscreenAvailabilityChangedEventArgs(bool isAvailable, int maximumTouchContacts)
    {
        IsAvailable = isAvailable;
        MaximumTouchContacts = maximumTouchContacts;
    }

    /// <summary>
    /// Gets a value indicating whether a touchscreen is currently available.
    /// </summary>
    public bool IsAvailable { get; }

    /// <summary>
    /// Gets the maximum number of simultaneous touch contacts supported by the system.
    /// Returns 0 if no touchscreen is available.
    /// </summary>
    public int MaximumTouchContacts { get; }
}
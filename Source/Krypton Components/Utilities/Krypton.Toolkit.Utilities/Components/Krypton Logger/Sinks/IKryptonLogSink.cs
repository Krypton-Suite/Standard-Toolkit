#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Receives filtered <see cref="KryptonLogEvent"/> instances from the logging pipeline.
/// </summary>
public interface IKryptonLogSink : IDisposable
{
    /// <summary>
    /// Returns whether this sink will accept events at <paramref name="level"/>.
    /// </summary>
    /// <param name="level">The candidate severity.</param>
    /// <returns><see langword="true"/> when the sink should receive the event.</returns>
    bool IsEnabled(KryptonLogLevel level);

    /// <summary>
    /// Writes <paramref name="logEvent"/> to the sink.
    /// </summary>
    /// <param name="logEvent">The event to write. Cannot be null.</param>
    void Emit(KryptonLogEvent logEvent);
}

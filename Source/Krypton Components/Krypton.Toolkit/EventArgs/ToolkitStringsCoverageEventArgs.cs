#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides data for the <see cref="KryptonManager.TranslationsCoverageReported"/> event.
/// </summary>
public sealed class ToolkitStringsCoverageEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ToolkitStringsCoverageEventArgs"/> class.
    /// </summary>
    /// <param name="coverage">The coverage report.</param>
    public ToolkitStringsCoverageEventArgs(ToolkitStringsCoverage coverage)
    {
        Coverage = coverage ?? ThrowHelper.ThrowArgumentNullException<ToolkitStringsCoverage>(nameof(coverage));
    }

    /// <summary>Gets the coverage report produced during analysis or import.</summary>
    public ToolkitStringsCoverage Coverage { get; }
}

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Writes theme-swap WM tracing through <see cref="KryptonLogger"/>.
/// </summary>
internal static class DebugLogger
{
    /// <summary>
    /// Writes a theme-swap WM trace line through <see cref="KryptonLogger"/>.
    /// Line termination is handled by the active logger; do not append <see cref="Environment.NewLine"/> here.
    /// </summary>
    /// <param name="message">The trace message (without the <c>[WM]</c> prefix).</param>
    public static void WriteLine(string message) =>
        // Concat avoids interpolation allocation on this hot WM-tracing path.
        KryptonLogger.Write(string.Concat("[WM] ", message));
}

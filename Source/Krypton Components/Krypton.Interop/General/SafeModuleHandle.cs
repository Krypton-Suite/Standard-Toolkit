#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides a safe handle wrapper for Windows module handles (HMODULE).
/// This class ensures proper cleanup of loaded library modules and prevents handle leaks.
/// </summary>
// inherits from SafeHandleZeroOrMinusOneIsInvalid, so IsInvalid is already implemented.
internal sealed class SafeModuleHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    /// <summary>
    /// Initializes a new instance of the SafeModuleHandle class.
    /// A default constructor is required for P/Invoke to instantiate the class.
    /// </summary>
    // ReSharper disable once ConvertToPrimaryConstructor
    public SafeModuleHandle()
        : base(true)
    {
    }

    /// <summary>
    /// Releases the module handle by calling FreeLibrary.
    /// </summary>
    /// <returns>True if the handle was successfully released; otherwise, false.</returns>
    protected override bool ReleaseHandle() => PI.FreeLibrary(handle);
}
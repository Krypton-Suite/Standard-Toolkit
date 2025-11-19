#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion
namespace Krypton.Toolkit;

[Flags]
public enum KryptonTaskDialogCommonButtonTypes
{
    /// <summary>
    /// No buttons be shown.
    /// </summary>
    None = 0,

    /// <summary>
    /// OK button.
    /// </summary>
    OK = 1,

    /// <summary>
    /// Cancel button.
    /// </summary>
    Cancel = 2,

    /// <summary>
    /// Yes button.
    /// </summary>
    Yes = 4,

    /// <summary>
    /// No button.
    /// </summary>
    No = 8,

    /// <summary>
    /// Retry button.
    /// </summary>
    Retry = 16,

    /// <summary>
    /// Abort button.
    /// </summary>
    Abort = 32,

    /// <summary>
    /// Ignore button
    /// </summary>
    Ignore = 64
}

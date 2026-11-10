#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Dialogs;

/// <summary>
/// Pause and cancel flag shared by the UI thread and the shell worker.
/// <see cref="IOperationsProgressDialog.GetOperationStatus"/> reads <see cref="Status"/> and does not call back onto the form.
/// </summary>
internal sealed class FileOperationProgressState
{
    /// <summary>Current <see cref="PDOPSTATUS"/> value.</summary>
    internal volatile int Status = (int)PDOPSTATUS.RUNNING;

    /// <summary>True when this pass should keep undo information and recycle deletions.</summary>
    internal bool AllowUndo;

    /// <summary>True after a per-item or engine failure was reported.</summary>
    internal volatile bool AnyError;
}

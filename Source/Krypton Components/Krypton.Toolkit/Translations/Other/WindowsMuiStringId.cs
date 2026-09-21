#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Identifies a verified Windows MUI string resource used by toolkit string providers.
/// </summary>
public enum WindowsMuiStringId
{
    #region Dialog (user32.dll 800-810)

    /// <summary>MessageBox / dialog OK button text (user32.dll resource 800).</summary>
    Ok = 0,

    /// <summary>MessageBox / dialog Cancel button text (user32.dll resource 801).</summary>
    Cancel,

    /// <summary>MessageBox / dialog Abort button text (user32.dll resource 802).</summary>
    Abort,

    /// <summary>MessageBox / dialog Retry button text (user32.dll resource 803).</summary>
    Retry,

    /// <summary>MessageBox / dialog Ignore button text (user32.dll resource 804).</summary>
    Ignore,

    /// <summary>MessageBox / dialog Yes button text (user32.dll resource 805).</summary>
    Yes,

    /// <summary>MessageBox / dialog No button text (user32.dll resource 806).</summary>
    No,

    /// <summary>MessageBox / dialog Close button text (user32.dll resource 807).</summary>
    Close,

    /// <summary>MessageBox / dialog Help button text (user32.dll resource 808).</summary>
    Help,

    /// <summary>MessageBox / dialog Try Again button text (user32.dll resource 809).</summary>
    TryAgain,

    /// <summary>MessageBox / dialog Continue button text (user32.dll resource 810).</summary>
    Continue,

    #endregion

    #region ControlBox (user32.dll 900-905)

    /// <summary>Caption-button Minimize tooltip (user32.dll resource 900).</summary>
    Minimize,

    /// <summary>Caption-button Maximize tooltip (user32.dll resource 901).</summary>
    Maximize,

    /// <summary>Caption-button Restore Up tooltip (user32.dll resource 902).</summary>
    RestoreUp,

    /// <summary>Caption-button Restore Down tooltip (user32.dll resource 903).</summary>
    RestoreDown,

    /// <summary>Caption-button Help tooltip (user32.dll resource 904).</summary>
    ControlBoxHelp,

    /// <summary>Caption-button Close tooltip (user32.dll resource 905).</summary>
    ControlBoxClose,

    #endregion

    #region Explorer (shell32.dll 12769-12774)

    /// <summary>Explorer column header Name (shell32.dll resource 12769).</summary>
    ColumnName,

    /// <summary>Explorer column header Type (shell32.dll resource 12770).</summary>
    ColumnType,

    /// <summary>Explorer column header Size (shell32.dll resource 12771).</summary>
    ColumnSize,

    /// <summary>Explorer column header Date Modified (shell32.dll resource 12772).</summary>
    ColumnDateModified,

    /// <summary>Explorer column header Date Created (shell32.dll resource 12773).</summary>
    ColumnDateCreated,

    /// <summary>Explorer column header Attributes (shell32.dll resource 12774).</summary>
    ColumnAttributes

    #endregion
}
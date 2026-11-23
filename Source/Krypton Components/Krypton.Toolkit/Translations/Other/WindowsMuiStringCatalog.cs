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
/// Maps <see cref="WindowsMuiStringId"/> values to verified Windows MUI module / resource IDs and English fallbacks.
/// </summary>
internal static class WindowsMuiStringCatalog
{
    #region Public

    /// <summary>
    /// Gets every defined <see cref="WindowsMuiStringId"/> value.
    /// </summary>
    public static WindowsMuiStringId[] AllIds =>
        (WindowsMuiStringId[])Enum.GetValues(typeof(WindowsMuiStringId));

    /// <summary>
    /// Tries to resolve the module file name, resource ID, and English fallback for the specified catalog entry.
    /// </summary>
    /// <param name="id">The catalog identifier.</param>
    /// <param name="moduleFileName">When this method returns, contains the DLL file name (for example <c>user32.dll</c>).</param>
    /// <param name="resourceId">When this method returns, contains the MUI string resource ID.</param>
    /// <param name="englishFallback">When this method returns, contains the English fallback text.</param>
    /// <returns><c>true</c> if <paramref name="id"/> is a known catalog entry; otherwise <c>false</c>.</returns>
    public static bool TryGet(WindowsMuiStringId id,
                              out string moduleFileName,
                              out uint resourceId,
                              out string englishFallback)
    {
        switch (id)
        {
            // Dialog — user32.dll 800-810
            case WindowsMuiStringId.Ok:
                return Set(Libraries.User32, 800u, @"O&K", out moduleFileName, out resourceId, out englishFallback);
            case WindowsMuiStringId.Cancel:
                return Set(Libraries.User32, 801u, @"Cance&l", out moduleFileName, out resourceId, out englishFallback);
            case WindowsMuiStringId.Abort:
                return Set(Libraries.User32, 802u, @"A&bort", out moduleFileName, out resourceId, out englishFallback);
            case WindowsMuiStringId.Retry:
                return Set(Libraries.User32, 803u, @"Ret&ry", out moduleFileName, out resourceId, out englishFallback);
            case WindowsMuiStringId.Ignore:
                return Set(Libraries.User32, 804u, @"I&gnore", out moduleFileName, out resourceId, out englishFallback);
            case WindowsMuiStringId.Yes:
                return Set(Libraries.User32, 805u, @"&Yes", out moduleFileName, out resourceId, out englishFallback);
            case WindowsMuiStringId.No:
                return Set(Libraries.User32, 806u, @"N&o", out moduleFileName, out resourceId, out englishFallback);
            case WindowsMuiStringId.Close:
                return Set(Libraries.User32, 807u, @"Clo&se", out moduleFileName, out resourceId, out englishFallback);
            case WindowsMuiStringId.Help:
                return Set(Libraries.User32, 808u, @"H&elp", out moduleFileName, out resourceId, out englishFallback);
            case WindowsMuiStringId.TryAgain:
                return Set(Libraries.User32, 809u, @"Try Aga&in", out moduleFileName, out resourceId, out englishFallback);
            case WindowsMuiStringId.Continue:
                return Set(Libraries.User32, 810u, @"Co&ntinue", out moduleFileName, out resourceId, out englishFallback);

            // Control box — user32.dll 900-905 (no accelerator ampersands)
            case WindowsMuiStringId.Minimize:
                return Set(Libraries.User32, 900u, @"Minimize", out moduleFileName, out resourceId, out englishFallback);
            case WindowsMuiStringId.Maximize:
                return Set(Libraries.User32, 901u, @"Maximize", out moduleFileName, out resourceId, out englishFallback);
            case WindowsMuiStringId.RestoreUp:
                return Set(Libraries.User32, 902u, @"Restore Up", out moduleFileName, out resourceId, out englishFallback);
            case WindowsMuiStringId.RestoreDown:
                return Set(Libraries.User32, 903u, @"Restore", out moduleFileName, out resourceId, out englishFallback);
            case WindowsMuiStringId.ControlBoxHelp:
                return Set(Libraries.User32, 904u, @"Help", out moduleFileName, out resourceId, out englishFallback);
            case WindowsMuiStringId.ControlBoxClose:
                return Set(Libraries.User32, 905u, @"Close", out moduleFileName, out resourceId, out englishFallback);

            // Explorer columns — shell32.dll 12769-12774
            case WindowsMuiStringId.ColumnName:
                return Set(Libraries.Shell32, 12769u, @"Name", out moduleFileName, out resourceId, out englishFallback);
            case WindowsMuiStringId.ColumnType:
                return Set(Libraries.Shell32, 12770u, @"Type", out moduleFileName, out resourceId, out englishFallback);
            case WindowsMuiStringId.ColumnSize:
                return Set(Libraries.Shell32, 12771u, @"Size", out moduleFileName, out resourceId, out englishFallback);
            case WindowsMuiStringId.ColumnDateModified:
                return Set(Libraries.Shell32, 12772u, @"Date Modified", out moduleFileName, out resourceId, out englishFallback);
            case WindowsMuiStringId.ColumnDateCreated:
                return Set(Libraries.Shell32, 12773u, @"Date Created", out moduleFileName, out resourceId, out englishFallback);
            case WindowsMuiStringId.ColumnAttributes:
                return Set(Libraries.Shell32, 12774u, @"Attributes", out moduleFileName, out resourceId, out englishFallback);

            default:
                moduleFileName = string.Empty;
                resourceId = 0;
                englishFallback = string.Empty;
                return false;
        }
    }

    /// <summary>
    /// Gets the English fallback text for the specified catalog entry.
    /// </summary>
    /// <param name="id">The catalog identifier.</param>
    /// <returns>The English fallback string.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="id"/> is not a known catalog entry.</exception>
    public static string GetFallback(WindowsMuiStringId id)
    {
        if (TryGet(id, out _, out _, out string englishFallback))
        {
            return englishFallback;
        }

        return ThrowHelper.ThrowArgumentOutOfRangeException<string>(nameof(id), id, @"Unknown Windows MUI string catalog entry.");
    }

    #endregion

    #region Implementation

    private static bool Set(string moduleFileName,
                            uint resourceId,
                            string englishFallback,
                            out string outModuleFileName,
                            out uint outResourceId,
                            out string outEnglishFallback)
    {
        outModuleFileName = moduleFileName;
        outResourceId = resourceId;
        outEnglishFallback = englishFallback;
        return true;
    }

    #endregion
}
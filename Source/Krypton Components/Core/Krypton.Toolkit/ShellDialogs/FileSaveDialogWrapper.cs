#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2023 - 2026. All rights reserved. 
 *  
 */
#endregion

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Krypton.Toolkit;

/// <summary>
///  Displays a dialog window from which the user can select a file.
/// </summary>
[DefaultEvent(nameof(FileOk))]
[DefaultProperty(nameof(FileName))]
public abstract class FileSaveDialogWrapper : FileDialogWrapper
{
}

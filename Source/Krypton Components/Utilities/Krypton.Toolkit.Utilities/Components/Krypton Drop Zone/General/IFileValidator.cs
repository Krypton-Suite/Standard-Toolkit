#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

public interface IFileValidator
{
    /// <summary>
    /// Validates the specified file.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="context">The validation context.</param>
    /// <returns>Validation result.</returns>
    string Validate(string filePath, ValidationContext context);
}

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public class GlobalStaticFunctions
{
    #region Methods
    /// <summary>
    /// Helper method that returns a generic message when a variable is null.
    /// </summary>
    /// <param name="variableName">Name of the variable to be inserted into the text.</param>
    /// <returns>The message.</returns>
    public static string VariableCannotBeNull(string variableName) => $"Variable {variableName} cannot be null.";

    /// <summary>
    /// Helper method that returns a generic message when a property is null.
    /// </summary>
    /// <param name="propertyName">Name of the property to be inserted into the text.</param>
    /// <returns>The message.</returns>
    public static string PropertyCannotBeNull(string propertyName) => $"Property {propertyName} cannot be null.";

    /// <summary>
    /// Helper method that returns a generic message when a parameter is null.
    /// </summary>
    /// <param name="parameterName">Name of the parameter to be inserted into the text.</param>
    /// <returns>The message.</returns>
    public static string ParameterCannotBeNull(string parameterName) => $"Parameter {parameterName} cannot be null.";
    #endregion
}
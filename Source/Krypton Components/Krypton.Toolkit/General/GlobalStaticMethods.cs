#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2024. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>Provides a collection of static functions, used within the toolkit.</summary>
    public class GlobalStaticMethods
    {
        #region Implementation

        /// <summary>Determines whether [is using default black themes].</summary>
        /// <returns><c>true</c> if [is using default black themes]; otherwise, <c>false</c>.</returns>
        public static bool IsUsingDefaultBlackThemes() => KryptonManager.CurrentGlobalPaletteMode == PaletteMode.Office2007Black || KryptonManager.CurrentGlobalPaletteMode == PaletteMode.Office2010Black || KryptonManager.CurrentGlobalPaletteMode == PaletteMode.Microsoft365Black;

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
}

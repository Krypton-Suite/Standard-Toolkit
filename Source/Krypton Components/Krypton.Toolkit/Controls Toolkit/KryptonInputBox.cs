#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// 
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public static class KryptonInputBox
{
    #region Public

    /// <summary>
    /// Displays an input box with provided prompt and caption and defaulted response string.
    /// </summary>
    /// <param name="inputBoxData">The data to feed through to <see cref="VisualInputBoxForm"/>.</param>
    /// <returns>Input string.</returns>
    public static string Show(KryptonInputBoxData inputBoxData)
        =>  InternalShow(inputBoxData);

    #endregion

    #region Implementation

    internal static string InternalShow(KryptonInputBoxData inputBoxData) => inputBoxData.UseRTLLayout == KryptonUseRTLLayout.Yes ? VisualInputBoxRtlAwareForm.InternalShow(inputBoxData) : VisualInputBoxForm.InternalShow(inputBoxData);

    #endregion
}
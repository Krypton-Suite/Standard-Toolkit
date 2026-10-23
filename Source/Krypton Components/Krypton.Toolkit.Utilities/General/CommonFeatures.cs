#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

internal static class CommonFeatures
{
    #region Properties

    /// <summary> 
    /// KryptonMessageBoxes that use the KRichTextBox need another color for the text.<br/>
    /// Set the text colour to the one a non-input control uses.
    /// </summary>
    internal static Color KryptonMessageBoxRichTextBoxTextColor =>
        // per ticket #1692
        KryptonManager.CurrentGlobalPalette.GetContentLongTextColor1(PaletteContentStyle.LabelNormalPanel, PaletteState.Normal);

    #endregion

    #region Implementation

    /// <summary>
    /// Applies <see cref="KryptonMessageBoxRichTextBoxTextColor"/> to a read-only panel-client <see cref="Krypton.Toolkit.KryptonRichTextBox"/> (toast body text; #3018).
    /// </summary>
    internal static void ApplyToastRichTextContentColor(KryptonRichTextBox richTextBox) =>
        richTextBox.StateCommon.Content.Color1 = KryptonMessageBoxRichTextBoxTextColor;


    #endregion
}
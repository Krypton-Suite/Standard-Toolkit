#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon
{
    /// <summary>
    /// Represents a ribbon group combo box.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonRibbonGroupComboBox), "ToolboxBitmaps.KryptonRibbonGroupComboBox.bmp")]
    [Designer(typeof(KryptonRibbonGroupComboBoxDesigner))]
    [DesignerCategory(@"code")]
    [DesignTimeVisible(false)]
    [DefaultEvent(@"SelectedTextChanged")]
    [DefaultProperty(nameof(Text))]
    public class KryptonRibbonGroupThemeSelector : KryptonRibbonGroupThemeComboBox
    {
    }
}

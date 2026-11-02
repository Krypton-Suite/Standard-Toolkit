#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Ribbon;

internal class KryptonRibbonGroupStackCollectionEditor : KryptonDesignerStandardCollectionEditor
{
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonRibbonGroupStackCollectionEditor"/> class.
    /// </summary>
    public KryptonRibbonGroupStackCollectionEditor()
        : base(typeof(KryptonRibbonGroupStackCollection))
    {
    }

    /// <summary>
    /// Gets the data types that this collection editor can contain.
    /// </summary>
    /// <returns>An array of data types that this collection can contain.</returns>
    protected override Type[] CreateNewItemTypes() =>
        new[]
        {
            typeof(KryptonRibbonGroupButton),
            typeof(KryptonRibbonGroupColorButton),
            typeof(KryptonRibbonGroupCheckBox),
            typeof(KryptonRibbonGroupCluster),
            typeof(KryptonRibbonGroupComboBox),
            typeof(KryptonRibbonGroupCustomControl),
            typeof(KryptonRibbonGroupDateTimePicker),
            typeof(KryptonRibbonGroupDomainUpDown),
            typeof(KryptonRibbonGroupLabel),
            typeof(KryptonRibbonGroupMaskedTextBox),
            typeof(KryptonRibbonGroupNumericUpDown),
            typeof(KryptonRibbonGroupRadioButton),
            typeof(KryptonRibbonGroupRichTextBox),
            typeof(KryptonRibbonGroupTextBox),
            typeof(KryptonRibbonGroupThemeComboBox),
            typeof(KryptonRibbonGroupTrackBar)
        };
}

#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Manage the items that can be added to a ribbon group stack container.
/// </summary>
public class KryptonRibbonGroupStackCollection : TypedRestrictCollection<KryptonRibbonGroupItem>
{
    #region Static Fields

    private static readonly Type[] _types =
    {
        typeof(KryptonRibbonGroupButton),
        typeof(KryptonRibbonGroupColorButton),
        typeof(KryptonRibbonGroupCheckBox),
        typeof(KryptonRibbonGroupComboBox),
        typeof(KryptonRibbonGroupCluster),
        typeof(KryptonRibbonGroupCustomControl),
        typeof(KryptonRibbonGroupDateTimePicker),
        typeof(KryptonRibbonGroupDomainUpDown),
        typeof(KryptonRibbonGroupLabel),
        typeof(KryptonRibbonGroupNumericUpDown),
        typeof(KryptonRibbonGroupRadioButton),
        typeof(KryptonRibbonGroupRichTextBox),
        typeof(KryptonRibbonGroupTextBox),
        typeof(KryptonRibbonGroupTrackBar),
        typeof(KryptonRibbonGroupMaskedTextBox),
        typeof(KryptonRibbonGroupThemeComboBox)
    };

    #endregion

    #region Restrict
    /// <summary>
    /// Gets an array of types that the collection is restricted to contain.
    /// </summary>
    public override Type[] RestrictTypes => _types;

    #endregion
}

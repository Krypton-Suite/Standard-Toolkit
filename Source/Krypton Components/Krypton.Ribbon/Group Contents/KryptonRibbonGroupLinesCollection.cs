﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2024. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon
{
    /// <summary>
    /// Manage the items that can be added to a ribbon group lines container.
    /// </summary>
    public class KryptonRibbonGroupLinesCollection : TypedRestrictCollection<KryptonRibbonGroupItem>
    {
        #region Static Fields

        private static readonly Type[] _types =
        [
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
        ];

        #endregion

        #region Restrict
        /// <summary>
        /// Gets an array of types that the collection is restricted to contain.
        /// </summary>
        public override Type[] RestrictTypes => _types;

        #endregion
    }
}

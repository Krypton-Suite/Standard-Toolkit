namespace Krypton.Ribbon
{
    /// <summary>
    /// Manage the items that can be added to a ribbon group lines container.
    /// </summary>
    public class KryptonRibbonGroupLinesCollection : TypedRestrictCollection<KryptonRibbonGroupItem>
    {
        #region Static Fields
        private static readonly Type[] _types = { typeof(KryptonRibbonGroupButton),
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
                                                             typeof(KryptonRibbonGroupMaskedTextBox)
        };

        #endregion

        #region Restrict
        /// <summary>
        /// Gets an array of types that the collection is restricted to contain.
        /// </summary>
        public override Type[] RestrictTypes => _types;

        #endregion
    }
}

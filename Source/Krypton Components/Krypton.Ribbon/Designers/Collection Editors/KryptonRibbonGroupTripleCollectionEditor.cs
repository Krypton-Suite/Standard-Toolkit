namespace Krypton.Ribbon
{
    internal class KryptonRibbonGroupTripleCollectionEditor : CollectionEditor
    {
        /// <summary>
        /// Initialize a new instance of the KryptonRibbonGroupTripleCollectionEditor class.
        /// </summary>
        public KryptonRibbonGroupTripleCollectionEditor()
            : base(typeof(KryptonRibbonGroupTripleCollection))
        {
        }

        /// <summary>
        /// Gets the data types that this collection editor can contain. 
        /// </summary>
        /// <returns>An array of data types that this collection can contain.</returns>
        protected override Type[] CreateNewItemTypes()
        {
            return new[] { typeof(KryptonRibbonGroupButton),
                                typeof(KryptonRibbonGroupColorButton),
                                typeof(KryptonRibbonGroupCheckBox),
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
                                typeof(KryptonRibbonGroupTrackBar)};
        }
    }
}

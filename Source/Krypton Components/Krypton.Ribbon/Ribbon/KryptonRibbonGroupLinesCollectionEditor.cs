using System;
using System.ComponentModel.Design;

namespace Krypton.Ribbon
{
    internal class KryptonRibbonGroupLinesCollectionEditor : CollectionEditor
    {
        /// <summary>
        /// Initialize a new instance of the KryptonRibbonGroupLineCollectionEditor class.
        /// </summary>
        public KryptonRibbonGroupLinesCollectionEditor()
            : base(typeof(KryptonRibbonGroupLinesCollection))
        {
        }

        /// <summary>
        /// Gets the data types that this collection editor can contain. 
        /// </summary>
        /// <returns>An array of data types that this collection can contain.</returns>
        protected override Type[] CreateNewItemTypes()
        {
            return new Type[] { typeof(KryptonRibbonGroupButton),
                                typeof(KryptonRibbonGroupColorButton),
                                typeof(KryptonRibbonGroupCheckBox),
                                typeof(KryptonRibbonGroupComboBox),
                                typeof(KryptonRibbonGroupCluster),
                                typeof(KryptonRibbonGroupCustomControl),
                                typeof(KryptonRibbonGroupDateTimePicker),
                                typeof(KryptonRibbonGroupLabel),
                                typeof(KryptonRibbonGroupRadioButton),
                                typeof(KryptonRibbonGroupRichTextBox),
                                typeof(KryptonRibbonGroupTextBox),
                                typeof(KryptonRibbonGroupMaskedTextBox)};
        }
    }
}

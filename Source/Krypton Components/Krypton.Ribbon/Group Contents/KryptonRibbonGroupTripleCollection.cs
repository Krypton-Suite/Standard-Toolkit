namespace Krypton.Ribbon
{
    /// <summary>
    /// Manage the items that can be added to a ribbon group triple container.
    /// </summary>
    public class KryptonRibbonGroupTripleCollection : TypedRestrictCollection<KryptonRibbonGroupItem>
    {
        #region Static Fields
        private static readonly Type[] _types = { typeof(KryptonRibbonGroupButton),
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
                                                 typeof(KryptonRibbonGroupTrackBar)
                                               };
        #endregion

        #region Restrict
        /// <summary>
        /// Gets an array of types that the collection is restricted to contain.
        /// </summary>
        public override Type[] RestrictTypes => _types;

        #endregion

        #region IList
        /// <summary>
        /// Append an item to the collection.
        /// </summary>
        /// <param name="value">Object reference.</param>
        /// <returns>The position into which the new item was inserted.</returns>
        public override int Add(object value)
        {
            // Restrict contents to three items max
            if (Count == 3)
            {
                throw new ArgumentException(@"Collection can only contain 3 entries.");
            }

            return base.Add(value);
        }

        /// <summary>
        /// Inserts an item to the collection at the specified index.
        /// </summary>
        /// <param name="index">Insert index.</param>
        /// <param name="value">Object reference.</param>
        public override void Insert(int index, object value)
        {
            // Restrict contents to three items max
            if (Count == 3)
            {
                throw new ArgumentException(@"Collection can only contain 3 entries.");
            }

            base.Insert(index, value);
        }
        #endregion

        #region IList<KryptonRibbonGroupItem>
        /// <summary>
        /// Inserts an item to the collection at the specified index.
        /// </summary>
        /// <param name="index">Insert index.</param>
        /// <param name="item">Item reference.</param>
        public override void Insert(int index, KryptonRibbonGroupItem item)
        {
            // Restrict contents to three items max
            if (Count == 3)
            {
                throw new ArgumentException(@"Collection can only contain 3 entries.");
            }

            base.Insert(index, item);
        }
        #endregion

        #region ICollection<KryptonRibbonGroupItem>
        /// <summary>
        /// Append an item to the collection.
        /// </summary>
        /// <param name="item">Item reference.</param>
        public override void Add(KryptonRibbonGroupItem item)
        {
            // Restrict contents to three items max
            if (Count == 3)
            {
                throw new ArgumentException(@"Collection can only contain 3 entries.");
            }

            base.Add(item);
        }
        #endregion
    }
}

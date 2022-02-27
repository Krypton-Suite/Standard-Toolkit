#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Toolkit
{
    /// <summary>
    /// Hosts a collection of KryptonDataGridViewDomainUpDownCell cells.
    /// </summary>
    [Designer(@"Krypton.Toolkit.KryptonDomainUpDownColumnDesigner, Krypton.Toolkit")]
    [ToolboxBitmap(typeof(KryptonDataGridViewDomainUpDownColumn), "ToolboxBitmaps.KryptonDomainUpDown.bmp")]
    public class KryptonDataGridViewDomainUpDownColumn : KryptonDataGridViewIconColumn
    {
        #region Instance Fields

        #endregion


        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewDomainUpDownColumn class.
        /// </summary>
        public KryptonDataGridViewDomainUpDownColumn()
            : base(new KryptonDataGridViewDomainUpDownCell())
        {
            Items = new StringCollection();
        }

        /// <summary>
        /// Returns a standard compact string representation of the column.
        /// </summary>
        public override string ToString()
        {
            StringBuilder builder = new(0x40);
            builder.Append(@"KryptonDataGridViewDomainUpDownColumn { Name=");
            // ReSharper disable RedundantBaseQualifier
            builder.Append(base.Name);
            builder.Append(@", Index=");
            builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
            // ReSharper restore RedundantBaseQualifier
            builder.Append(@" }");
            return builder.ToString();
        }

        /// <summary>
        /// Create a cloned copy of the column.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            KryptonDataGridViewDomainUpDownColumn cloned = base.Clone() as KryptonDataGridViewDomainUpDownColumn;

            // Convert collection of strings to an array
            var strings = new string[Items.Count];
            for (var i = 0; i < strings.Length; i++)
            {
                strings[i] = Items[i];
            }

            cloned.Items.AddRange(strings);

            return cloned;
        }
        #endregion

        #region Public
        /// <summary>
        /// Represents the implicit cell that gets cloned when adding rows to the grid.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DataGridViewCell CellTemplate
        {
            get => base.CellTemplate;
            set
            {
                if ((value != null) && (value is not KryptonDataGridViewDomainUpDownCell cell))
                {
                    throw new InvalidCastException(@"Value provided for CellTemplate must be of type KryptonDataGridViewDomainUpDownCell or derive from it.");
                }

                base.CellTemplate = value;
            }
        }

        /// <summary>
        /// Gets the collection of allowable items of the domain up down.
        /// </summary>
        [Category(@"Data")]
        [Description(@"The allowable items of the domain up down.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(@"System.Windows.Forms.Design.StringCollectionEditor, " + AssemblyRef.SystemDesign, typeof(UITypeEditor))]
        public StringCollection Items { get; }

        #endregion

        #region Private
        /// <summary>
        /// Small utility function that returns the template cell as a KryptonDataGridViewDomainUpDownCell
        /// </summary>
        private KryptonDataGridViewDomainUpDownCell DomainUpDownCellTemplate => (KryptonDataGridViewDomainUpDownCell)CellTemplate;

        #endregion

    }
}
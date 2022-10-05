// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBeInternal
// ReSharper disable MemberCanBePrivate.Global

namespace Krypton.Toolkit
{
    /// <summary>
    /// Icon specification that can be assigned to DataGridViewColumns.
    /// </summary>
    public class IconSpec : ICloneable
    {
        /// <summary>
        /// Alignment options for icons.
        /// </summary>
        public enum IconAlignment
        {
            /// <summary>
            /// Right-Alignment.
            /// </summary>
            Right,
            /// <summary>
            /// Left-Alignment.
            /// </summary>
            Left
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public Image Icon
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the alignment of the icon.
        /// </summary>
        public IconAlignment Alignment
        {
            get;
            set;
        }

        /// <summary>
        /// Clones this instance of the IconSpec class.
        /// </summary>
        /// <returns>
        /// A cloned instance.
        /// </returns>
        public object Clone()
        {
            IconSpec spec = new()
            {
                Icon = Icon?.Clone() as Image,
                Alignment = Alignment
            };
            return spec;
        }
    }

    /// <summary>
    /// An interface that is implemented by KryptonDataGridView column and cell classes that 
    /// support column header or cell icons.
    /// </summary>
    public interface IIconCell
    {
        /// <summary>
        /// Gets the list of icon specifications.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        List<IconSpec> IconSpecs
        {
            get;
        }
    }

    public abstract class KryptonDataGridViewIconColumn : DataGridViewColumn, IIconCell
    {
        #region Instance Fields

        #endregion

        #region Identity

        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewTextBoxColumn class.
        /// </summary>
        protected KryptonDataGridViewIconColumn(DataGridViewCell cellTemplate)
            : base(cellTemplate) =>
            IconSpecs = new List<IconSpec>();

        #endregion

        /// <summary>
        /// Create a cloned copy of the column.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            KryptonDataGridViewIconColumn cloned = base.Clone() as KryptonDataGridViewIconColumn;

            foreach (IconSpec sp in IconSpecs)
            {
                cloned.IconSpecs.Add(sp.Clone() as IconSpec);
            }

            return cloned;
        }

        /// <summary>
        /// Gets the collection of the icon specifications.
        /// </summary>
        [Category(@"Data")]
        [Description(@"Set of extra icons to appear with control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<IconSpec> IconSpecs { get; }

    }

}

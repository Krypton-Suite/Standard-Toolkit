﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2024. All rights reserved.
 *  
 */
#endregion

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
        public Image? Icon
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
            var spec = new IconSpec
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
        #region Identity

        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewTextBoxColumn class.
        /// </summary>
        protected KryptonDataGridViewIconColumn(DataGridViewCell cellTemplate)
            : base(cellTemplate) =>
            IconSpecs = [];

        #endregion

        /// <summary>
        /// Create a cloned copy of the column.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var cloned = base.Clone() as KryptonDataGridViewIconColumn ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("cloned"));

            foreach (IconSpec sp in IconSpecs)
            {
                cloned.IconSpecs.Add((sp.Clone() as IconSpec)!);
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

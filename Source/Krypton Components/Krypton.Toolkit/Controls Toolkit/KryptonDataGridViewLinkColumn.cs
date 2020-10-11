// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.550.0  
// *****************************************************************************

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Hosts a collection of KryptonDataGridViewLinkColumn cells.
    /// </summary>
    [ToolboxBitmap(typeof(KryptonDataGridViewLinkColumn), "ToolboxBitmaps.KryptonLinkLabel.bmp")]
    public class KryptonDataGridViewLinkColumn : KryptonDataGridViewIconColumn
    {
        #region Static Fields
        private MethodInfo _miColumnCommonChange;
        private PropertyInfo _piUseColumnTextForLinkValueInternal;
        private PropertyInfo _piTrackVisitedStateInternal;
        #endregion

        #region Instance Fields
        private string _text;
        private LabelStyle _labelStyle;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewLinkColumn class.
        /// </summary>
        public KryptonDataGridViewLinkColumn()
            : base(new KryptonDataGridViewLinkCell())
        {
            // Define defaults
            _labelStyle = LabelStyle.NormalControl;
        }

        /// <summary>
        /// Returns a String that represents the current Object.
        /// </summary>
        /// <returns>A String that represents the current Object.</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x40);
            builder.Append("KryptonDataGridViewLinkColumn { Name=");
            // ReSharper disable RedundantBaseQualifier
            builder.Append(base.Name);
            builder.Append(", Index=");
            builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
            // ReSharper restore RedundantBaseQualifier
            builder.Append(" }");
            return builder.ToString();
        }

        /// <summary>
        /// This member overrides DataGridViewButtonColumn.Clone.
        /// </summary>
        /// <returns>New object instance.</returns>
        public override object Clone()
        {
            // Create a new instance
            KryptonDataGridViewLinkColumn clone = base.Clone() as KryptonDataGridViewLinkColumn;
            clone.Text = Text;
            clone.LabelStyle = LabelStyle;
            return clone;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets or sets the template used to model cell appearance.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DataGridViewCell CellTemplate
        {
            get => base.CellTemplate;

            set
            {
                if ((value != null) && !(value is KryptonDataGridViewLinkCell))
                {
                    throw new InvalidCastException("Can only assign a object of type KryptonDataGridViewLinkCell");
                }

                base.CellTemplate = value;
            }
        }

        /// <summary>
        /// Gets or sets the default text displayed on the link cell.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(null)]
        public string Text
        {
            get => _text;
            set
            {
                if (!string.Equals(value, _text, StringComparison.Ordinal))
                {
                    _text = value;
                    if (DataGridView != null)
                    {
                        if (UseColumnTextForLinkValue)
                        {
                            ColumnCommonChange(Index);
                        }
                        else
                        {
                            DataGridViewRowCollection rows = DataGridView.Rows;
                            int count = rows.Count;
                            for (int i = 0; i < count; i++)
                            {
                                if ((rows.SharedRow(i).Cells[Index] is KryptonDataGridViewLinkCell cell) && cell.UseColumnTextForLinkValue)
                                {
                                    ColumnCommonChange(Index);
                                    return;
                                }
                            }
                            DataGridView.InvalidateColumn(Index);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the default label style of link cell.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(LabelStyle), "NormalControl")]
        public LabelStyle LabelStyle
        {
            get => _labelStyle;
            set
            {
                if (_labelStyle != value)
                {
                    _labelStyle = value;
                    ((KryptonDataGridViewLinkCell)CellTemplate).LabelStyleInternal = value;
                    // ReSharper disable RedundantBaseQualifier
                    if (base.DataGridView != null)
                    // ReSharper restore RedundantBaseQualifier
                    {
                        DataGridViewRowCollection rows = DataGridView.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            if (rows.SharedRow(i).Cells[Index] is KryptonDataGridViewLinkCell cell)
                            {
                                cell.LabelStyleInternal = value;
                            }
                        }
                        DataGridView.InvalidateColumn(Index);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that represents the behavior of links within cells in the column.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(typeof(LinkBehavior), "AlwaysUnderline")]
        public LinkBehavior LinkBehavior
        {
            get
            {
                if (CellTemplate == null)
                {
                    throw new InvalidOperationException("KryptonDataGridViewLinkCell cell template required");
                }

                return ((KryptonDataGridViewLinkCell)CellTemplate).LinkBehavior;
            }
            set
            {
                if (!LinkBehavior.Equals(value))
                {
                    ((KryptonDataGridViewLinkCell)CellTemplate).LinkBehaviorInternal = value;
                    // ReSharper disable RedundantBaseQualifier
                    if (base.DataGridView != null)
                    // ReSharper restore RedundantBaseQualifier
                    {
                        DataGridViewRowCollection rows = DataGridView.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            if (rows.SharedRow(i).Cells[Index] is KryptonDataGridViewLinkCell cell)
                            {
                                cell.LinkBehaviorInternal = value;
                            }
                        }
                        DataGridView.InvalidateColumn(Index);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the link changes color when it is visited.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(true)]
        public bool TrackVisitedState
        {
            get
            {
                if (CellTemplate == null)
                {
                    throw new InvalidOperationException("KryptonDataGridViewLinkCell cell template required");
                }

                return ((KryptonDataGridViewLinkCell)CellTemplate).TrackVisitedState;
            }
            set
            {
                if (TrackVisitedState != value)
                {
                    TrackVisitedStateInternal(CellTemplate, value);
                    if (DataGridView != null)
                    {
                        DataGridViewRowCollection rows = DataGridView.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            if (rows.SharedRow(i).Cells[Index] is DataGridViewLinkCell cell)
                            {
                                TrackVisitedStateInternal(cell, value);
                            }
                        }
                        DataGridView.InvalidateColumn(Index);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Text property value is displayed as the link text for cells in this column.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        public bool UseColumnTextForLinkValue
        {
            get
            {
                if (CellTemplate == null)
                {
                    throw new InvalidOperationException("KryptonDataGridViewLinkCell cell template required");
                }

                return ((KryptonDataGridViewLinkCell)CellTemplate).UseColumnTextForLinkValue;
            }

            set
            {
                if (UseColumnTextForLinkValue != value)
                {
                    SetUseColumnTextForLinkValueInternal(CellTemplate, value);
                    if (DataGridView != null)
                    {
                        DataGridViewRowCollection rows = DataGridView.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            if (rows.SharedRow(i).Cells[Index] is DataGridViewLinkCell cell)
                            {
                                SetUseColumnTextForLinkValueInternal(cell, value);
                            }
                        }
                        ColumnCommonChange(Index);
                    }
                }
            }
        }
        #endregion

        #region Private
        private void ColumnCommonChange(int columnIndex)
        {
            // Only need to cache reflection info the first time around
            if (_miColumnCommonChange == null)
            {
                // Cache access to the internal method 'OnColumnCommonChange'
                _miColumnCommonChange = typeof(DataGridView).GetMethod("OnColumnCommonChange", BindingFlags.Instance |
                                                                                               BindingFlags.NonPublic |
                                                                                               BindingFlags.GetField);

            }

            _miColumnCommonChange.Invoke(DataGridView, new object[] { columnIndex });
        }

        private void SetUseColumnTextForLinkValueInternal(object instance, bool value)
        {
            // Only need to cache reflection info the first time around
            if (_piUseColumnTextForLinkValueInternal == null)
            {
                // Cache access to the internal property sette 'UseColumnTextForLinkValueInternal'
                _piUseColumnTextForLinkValueInternal = typeof(DataGridViewLinkCell).GetProperty("UseColumnTextForLinkValueInternal", BindingFlags.Instance |
                                                                                                                                     BindingFlags.NonPublic |
                                                                                                                                     BindingFlags.SetProperty);

            }

            _piUseColumnTextForLinkValueInternal.SetValue(instance, value, null);
        }

        private void TrackVisitedStateInternal(object instance, bool value)
        {
            // Only need to cache reflection info the first time around
            if (_piTrackVisitedStateInternal == null)
            {
                // Cache access to the internal property sette 'TrackVisitedStateInternal'
                _piTrackVisitedStateInternal = typeof(DataGridViewLinkCell).GetProperty("TrackVisitedStateInternal", BindingFlags.Instance |
                                                                                                                     BindingFlags.NonPublic |
                                                                                                                     BindingFlags.SetProperty);

            }

            _piTrackVisitedStateInternal.SetValue(instance, value, null);
        }
        #endregion
    }
}
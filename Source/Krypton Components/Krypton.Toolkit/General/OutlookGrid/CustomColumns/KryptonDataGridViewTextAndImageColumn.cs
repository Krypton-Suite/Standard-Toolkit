#region Licences

/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */

//--------------------------------------------------------------------------------
// Copyright (C) 2013-2021 JDH Software - <support@jdhsoftware.com>
//
// This program is provided to you under the terms of the Microsoft Public
// License (Ms-PL) as published at https://github.com/Cocotteseb/Krypton-OutlookGrid/blob/master/LICENSE.md
//
// Visit https://www.jdhsoftware.com and follow @jdhsoftware on Twitter
//
//--------------------------------------------------------------------------------

#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Hosts a collection of KryptonDataGridViewTextAndImageCell cells.
    /// </summary>
    public class KryptonDataGridViewTextAndImageColumn : DataGridViewColumn
    {
        #region Instance Fields

        private DataGridViewColumnSpecCollection _buttonSpecs;
        private Image _imageValue;
        private Size _imageSize;

        #endregion

        #region Events
        /// <summary>
        /// Occurs when the user clicks a button spec.
        /// </summary>
        public event EventHandler<DataGridViewButtonSpecClickEventArgs> ButtonSpecClick;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewTextBoxColumn class.
        /// </summary>
        public KryptonDataGridViewTextAndImageColumn()
            : base(new KryptonDataGridViewTextAndImageCell())
        {
            _buttonSpecs = new DataGridViewColumnSpecCollection(this);
            SortMode = DataGridViewColumnSortMode.Automatic;
        }

        /// <summary>
        /// Returns a String that represents the current Object.
        /// </summary>
        /// <returns>A String that represents the current Object.</returns>
        public override string ToString()
        {
            StringBuilder builder = new(0x40);
            builder.Append("KryptonDataGridViewTextAndImageColumn { Name=");
            builder.Append(Name);
            builder.Append(", Index=");
            builder.Append(Index.ToString(CultureInfo.CurrentCulture));
            builder.Append(" }");
            return builder.ToString();
        }

        /// <summary>
        /// Create a cloned copy of the column.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            KryptonDataGridViewTextAndImageColumn? cloned = base.Clone() as KryptonDataGridViewTextAndImageColumn;
            cloned!._imageValue = _imageValue;
            cloned._imageSize = _imageSize;
            // Move the button specs over to the new clone
            foreach (ButtonSpec bs in ButtonSpecs)
            {
                cloned.ButtonSpecs.Add(bs.Clone());
            }

            return cloned;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Public

        ///// <summary>
        ///// Gets or Sets the image
        ///// </summary>
        //public Image Image
        //{
        //    get { return this.imageValue; }
        //    set
        //    {
        //        if (this.Image != value)
        //        {
        //            this.imageValue = value;
        //            this.imageSize = value.Size;
        //            if (this.InheritedStyle != null)
        //            {
        //                Padding inheritedPadding = this.InheritedStyle.Padding;
        //                this.InheritedStyle.Padding = new Padding(imageSize.Width + 2, inheritedPadding.Top, inheritedPadding.Right, inheritedPadding.Bottom);
        //                //Padding inheritedPadding = this.InheritedStyle.Padding;
        //                //this.Style.Padding = new Padding(18, inheritedPadding.Top, inheritedPadding.Right, inheritedPadding.Bottom);

        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Gets or sets the maximum number of characters that can be entered into the text box.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(typeof(int), "32767")]
        public int MaxInputLength
        {
            get
            {
                if (TextBoxCellTemplate == null)
                {
                    throw new InvalidOperationException("KryptonDataGridViewTextAndImageColumn cell template required");
                }

                return TextBoxCellTemplate.MaxInputLength;
            }

            set
            {
                if (MaxInputLength != value)
                {
                    TextBoxCellTemplate.MaxInputLength = value;
                    if (DataGridView != null)
                    {
                        DataGridViewRowCollection rows = DataGridView.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            if (rows.SharedRow(i).Cells[Index] is DataGridViewTextBoxCell cell)
                            {
                                cell.MaxInputLength = value;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the sort mode for the column.
        /// </summary>
        [DefaultValue(typeof(DataGridViewColumnSortMode), "Automatic")]
        public new DataGridViewColumnSortMode SortMode
        {
            get => base.SortMode;
            set => base.SortMode = value;
        }

        /// <summary>
        /// Gets or sets the template used to model cell appearance.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DataGridViewCell? CellTemplate
        {
            get => base.CellTemplate!;

            set
            {
                if (value != null && !(value is KryptonDataGridViewTextAndImageCell))
                {
                    throw new InvalidCastException("Can only assign a object of type KryptonDataGridViewTextAndImageCell");
                }

                base.CellTemplate = value;
            }
        }

        /// <summary>
        /// Gets the collection of the button specifications.
        /// </summary>
        [Category("Data")]
        [Description("Set of extra button specs to appear with control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DataGridViewColumnSpecCollection ButtonSpecs => _buttonSpecs;

        #endregion

        #region Private
        private KryptonDataGridViewTextAndImageCell TextBoxCellTemplate => (KryptonDataGridViewTextAndImageCell)CellTemplate!;

        #endregion

        #region Internal
        internal void PerformButtonSpecClick(DataGridViewButtonSpecClickEventArgs args)
        {
            if (ButtonSpecClick != null)
            {
                ButtonSpecClick(this, args);
            }
        }

        internal Size ImageSize => _imageSize;
        #endregion
    }
}
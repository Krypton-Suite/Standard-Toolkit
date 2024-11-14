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
    /// This group simple example of an implementation which groups the items into Alphabetic categories
    /// based only on the first letter of each item
    /// 
    /// For this we need to override the Value property (used for comparison)
    /// and the CompareTo function.
    /// Also, the Clone method must be overriden, so this Group object can create clones of itself.
    /// Cloning of the group is used by the OutlookGrid
    /// </summary>
    public sealed class OutlookGridAlphabeticGroup : OutlookGridDefaultGroup
    {
        #region Identity

        /// <summary>
        /// Initializes a new instance of the <see cref="OutlookGridAlphabeticGroup"/> class.
        /// </summary>
        public OutlookGridAlphabeticGroup()
            : base()
        {
            AllowHiddenWhenGrouped = false;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parentGroup">The parentGroup if any.</param>
        public OutlookGridAlphabeticGroup(IOutlookGridGroup? parentGroup)
            : base(parentGroup)
        {
            AllowHiddenWhenGrouped = false;
        }

        #endregion

        #region Public Identity

        /// <summary>
        /// Gets or sets the displayed text.
        /// </summary>
        public override string Text => $"{Column.DataGridViewColumn?.HeaderText}: {Value} ({(ItemCount == 1 ? OneItemText : ItemCount + XxxItemsText)})";

        /// <summary>
        /// Gets or sets the Alphabetic value
        /// </summary>
        public override object? Value
        {
            get => base.Value;
            set =>
                //Note : value with Clone() is already 1 character, but no problem here
                base.Value = value is string str && str.Length > 0 
                    ? str.Substring(0, 1).ToUpper()
                    : string.Empty;
        }

        #endregion

        #region ICloneable Members

        /// <summary>
        /// Overrides the Clone() function
        /// </summary>
        /// <returns>OutlookGridAlphabeticGroup</returns>
        public override object Clone()
        {
            OutlookGridAlphabeticGroup gr = new(ParentGroup)
            {
                Column = Column,
                Value = Value,
                Collapsed = Collapsed,
                Height = Height,
                GroupImage = GroupImage,
                FormatStyle = FormatStyle,
                XxxItemsText = XxxItemsText,
                OneItemText = OneItemText,
                AllowHiddenWhenGrouped = AllowHiddenWhenGrouped,
                SortBySummaryCount = SortBySummaryCount
            };

            return gr;
        }

        #endregion

        #region IComparable Members
        /// <summary>
        /// overide the CompareTo, so only the first character is compared, instead of the whole string
        /// this will result in classifying each item into a letter of the Alphabet.
        /// for instance, this is usefull when grouping names, they will be categorized under the letters A, B, C etc..
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int CompareTo(object? obj)
        {
            int orderModifier = Column.SortDirection == SortOrder.Ascending ? 1 : -1;

            if (obj is OutlookGridAlphabeticGroup)
            {
                return string.CompareOrdinal(Value?.ToString(), (obj as OutlookGridAlphabeticGroup)?.Value?.ToString()) *
                       orderModifier;
            }
            else
            {
                return 0;
            }
        }
        #endregion IComparable Members
    }
}
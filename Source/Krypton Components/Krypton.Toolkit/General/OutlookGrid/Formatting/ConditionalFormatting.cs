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
    /// Conditional Formatting class
    /// </summary>
    public class ConditionalFormatting
    {
        #region Public

        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        /// <value>
        /// The name of the column.
        /// </value>
        public string ColumnName { get; set; }
        /// <summary>
        /// Gets or sets the type of the Conditional Formatting.
        /// </summary>
        /// <value>
        /// The type of the Conditional Formatting.
        /// </value>
        public EnumConditionalFormatType FormatType { get; set; }
        /// <summary>
        /// Gets or sets the Conditional Formatting parameters.
        /// </summary>
        /// <value>
        /// The Conditional Formatting parameters.
        /// </value>
        public IFormatParams? FormatParams { get; set; }
        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        public double MinValue { get; set; }
        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        public double MaxValue { get; set; }

        #endregion

        #region Identity

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalFormatting"/> class.
        /// </summary>
        public ConditionalFormatting() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalFormatting"/> class. (Only use for context menu !)
        /// </summary>
        /// <param name="formatType">Type of the Conditional Formatting.</param>
        /// <param name="formatParams">The Conditional Formatting parameters.</param>
        public ConditionalFormatting(EnumConditionalFormatType formatType, IFormatParams formatParams)
        {
            FormatType = formatType;
            FormatParams = formatParams;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalFormatting"/> class.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="formatType">Type of the Conditional Formatting.</param>
        /// <param name="formatParams">The Conditional Formatting parameters.</param>
        public ConditionalFormatting(string columnName, EnumConditionalFormatType formatType, IFormatParams? formatParams)
        {
            ColumnName = columnName;
            FormatType = formatType;
            FormatParams = formatParams;
        }

        #endregion

        #region Implementation

        internal void Persist(XmlWriter writer)
        {
            writer.WriteStartElement(KryptonManager.Strings.OutlookGridStrings.ConditionXMLNodeText);
            writer.WriteElementString(KryptonManager.Strings.OutlookGridStrings.ColumnNameXMLNodeText, ColumnName);
            writer.WriteElementString(KryptonManager.Strings.OutlookGridStrings.FormatTypeXMLNodeText, FormatType.ToString());
            writer.WriteStartElement(KryptonManager.Strings.OutlookGridStrings.FormatParamsXMLNodeText);
            FormatParams?.Persist(writer);
            writer.WriteEndElement(); //FormatParams
            //No need to persist min/max Value.
            writer.WriteEndElement(); //Condition
        }

        #endregion
    }
}
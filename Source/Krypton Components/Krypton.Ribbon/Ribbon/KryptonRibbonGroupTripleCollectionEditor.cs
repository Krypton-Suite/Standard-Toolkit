// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

using System;
using System.ComponentModel.Design;

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
            return new Type[] { typeof(KryptonRibbonGroupButton),
                                typeof(KryptonRibbonGroupColorButton),
                                typeof(KryptonRibbonGroupCheckBox),
                                typeof(KryptonRibbonGroupComboBox),
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

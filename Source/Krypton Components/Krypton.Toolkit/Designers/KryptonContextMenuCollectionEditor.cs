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

namespace Krypton.Toolkit
{
    /// <summary>
    /// Designer for a collection of context menu items.
    /// </summary>
    public partial class KryptonContextMenuCollectionEditor : CollectionEditor
    {

#region Classes
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonContextMenuCollectionEditor class.
        /// </summary>
        public KryptonContextMenuCollectionEditor()
            : base(typeof(KryptonContextMenuCollection))
        {
        }
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Creates a new form to display and edit the current collection.
        /// </summary>
        /// <returns>A CollectionForm to provide as the user interface for editing the collection.</returns>
        protected override CollectionForm CreateCollectionForm()
        {
            return new KryptonContextMenuCollectionForm(this);
        }

        /// <summary>
        /// Gets the data types that this collection editor can contain. 
        /// </summary>
        /// <returns>An array of data types that this collection can contain.</returns>
        protected override Type[] CreateNewItemTypes()
        {
            return new Type[] { typeof(KryptonContextMenuItems),
                                typeof(KryptonContextMenuSeparator),
                                typeof(KryptonContextMenuHeading),
                                typeof(KryptonContextMenuLinkLabel),
                                typeof(KryptonContextMenuCheckBox),
                                typeof(KryptonContextMenuCheckButton),
                                typeof(KryptonContextMenuRadioButton),
                                typeof(KryptonContextMenuColorColumns),
                                typeof(KryptonContextMenuMonthCalendar),
                                typeof(KryptonContextMenuImageSelect),
            };
        }
        #endregion
    }
}

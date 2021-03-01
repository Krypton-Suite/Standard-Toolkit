﻿// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

using System.ComponentModel.Design;

namespace Krypton.Toolkit
{
    internal class KryptonCheckSetActionList : DesignerActionList
    {
        #region Instance Fields
        private readonly KryptonCheckSet _set;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonCheckSetActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonCheckSetActionList(KryptonCheckSetDesigner owner)
            : base(owner.Component)
        {
            // Remember the check set component instance
            _set = owner.Component as KryptonCheckSet;
        }
        #endregion

        #region Public
        /*
         /// <summary>Gets or sets the font.</summary>
        /// <value>The font.</value>
        public Font ShortTextFont
        {
            get => _set..StateCommon.Content.ShortText.Font;

            set
            {
                if (_button.StateCommon.Content.ShortText.Font != value)
                {
                    _service.OnComponentChanged(_button, null, _button.StateCommon.Content.ShortText.Font, value);

                    _button.StateCommon.Content.ShortText.Font = value;
                }
            }
        }

        /// <summary>Gets or sets the font.</summary>
        /// <value>The font.</value>
        public Font LongTextFont
        {
            get => _button.StateCommon.Content.LongText.Font;

            set
            {
                if (_button.StateCommon.Content.ShortText.Font != value)
                {
                    _service.OnComponentChanged(_button, null, _button.StateCommon.Content.LongText.Font, value);

                    _button.StateCommon.Content.LongText.Font = value;
                }
            }
        }
        */
        #endregion

        #region Public Override
        /// <summary>
        /// Returns the collection of DesignerActionItem objects contained in the list.
        /// </summary>
        /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            // Create a new collection for holding the single item we want to create
            DesignerActionItemCollection actions = new DesignerActionItemCollection();

            // This can be null when deleting a control instance at design time
            if (_set != null)
            {
                // Add the list of check set specific actions
            }

            return actions;
        }
        #endregion
    }
}

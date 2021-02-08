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

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    internal class KryptonSplitContainerActionList : DesignerActionList
    {
        #region Instance Fields
        private readonly KryptonSplitContainer _splitContainer;
        private readonly IComponentChangeService _service;
        private string _action;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonSplitContainerActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonSplitContainerActionList(KryptonSplitContainerDesigner owner) 
            : base(owner.Component)
        {
            _splitContainer = owner.Component as KryptonSplitContainer;

            // Assuming we were correctly passed an actual component...
            if (_splitContainer != null)
            {
                // Get access to the actual Orientation property
                PropertyDescriptor orientationProp = TypeDescriptor.GetProperties(_splitContainer)["Orientation"];

                // If we succeeded in getting the property
                if (orientationProp != null)
                {
                    // Decide on the next action to take given the current setting
                    _action = (Orientation) orientationProp.GetValue(_splitContainer) == Orientation.Vertical
                        ? "Horizontal splitter orientation"
                        : "Vertical splitter orientation";
                }
            }

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the panel background style.
        /// </summary>
        public PaletteBackStyle ContainerBackStyle
        {
            get => _splitContainer.ContainerBackStyle;

            set 
            {
                if (_splitContainer.ContainerBackStyle != value)
                {
                    _service.OnComponentChanged(_splitContainer, null, _splitContainer.ContainerBackStyle, value);
                    _splitContainer.ContainerBackStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the separator style.
        /// </summary>
        public SeparatorStyle SeparatorStyle
        {
            get => _splitContainer.SeparatorStyle;

            set 
            {
                if (_splitContainer.SeparatorStyle != value)
                {
                    _service.OnComponentChanged(_splitContainer, null, _splitContainer.SeparatorStyle, value);
                    _splitContainer.SeparatorStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get => _splitContainer.PaletteMode;

            set 
            {
                if (_splitContainer.PaletteMode != value)
                {
                    _service.OnComponentChanged(_splitContainer, null, _splitContainer.PaletteMode, value);
                    _splitContainer.PaletteMode = value;
                }
            }
        }
        #endregion

        #region Public Overrides
        /// <summary>
        /// Returns the collection of DesignerActionItem objects contained in the list.
        /// </summary>
        /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            // Create a new collection for holding the single item we want to create
            DesignerActionItemCollection actions = new DesignerActionItemCollection();

            // This can be null when deleting a control instance at design time
            if (_splitContainer != null)
            {
                // Add our own action to the end
                actions.Add(new DesignerActionHeaderItem("Appearance"));
                actions.Add(new DesignerActionPropertyItem("ContainerBackStyle", "Back style", "Appearance", "Background style"));
                actions.Add(new DesignerActionHeaderItem("Splitter"));
                actions.Add(new KryptonDesignerActionItem(new DesignerVerb(_action, OnOrientationClick), "Splitter"));
                actions.Add(new DesignerActionPropertyItem("SeparatorStyle", "Separator style", "Splitter", "Separator style"));
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("PaletteMode", "Palette", "Visuals", "Palette applied to drawing"));
            }

            return actions;
        }
        #endregion

        #region Implementation
        private void OnOrientationClick(object sender, EventArgs e)
        {
            // Cast to the correct type

            // Double check the source is the expected type
            if (sender is DesignerVerb verb)
            {
                // Decide on the new orientation required
                Orientation orientation = verb.Text.Equals("Horizontal splitter orientation") ? Orientation.Horizontal : Orientation.Vertical;

                // Decide on the next action to take given the new setting
                _action = orientation == Orientation.Vertical ? "Horizontal splitter orientation" : "Vertical splitter orientation";

                // Get access to the actual Orientation property
                PropertyDescriptor orientationProp = TypeDescriptor.GetProperties(_splitContainer)["Orientation"];

                // If we succeeded in getting the property
                // Update the actual property with the new value
                orientationProp?.SetValue(_splitContainer, orientation);

                // Get the user interface service associated with actions

                // If we managed to get it then request it update to reflect new action setting
                if (GetService(typeof(DesignerActionUIService)) is DesignerActionUIService service)
                {
                    service.Refresh(_splitContainer);
                }
            }
        }
        #endregion
    }
}

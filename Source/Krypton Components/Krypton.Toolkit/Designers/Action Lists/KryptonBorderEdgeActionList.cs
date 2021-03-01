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
    internal class KryptonBorderEdgeActionList : DesignerActionList
    {
        #region Instance Fields
        private readonly KryptonBorderEdge _borderEdge;
        private readonly IComponentChangeService _service;
        private string _action;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonBorderEdgeActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonBorderEdgeActionList(KryptonBorderEdgeDesigner owner) 
            : base(owner.Component)
        {
            _borderEdge = owner.Component as KryptonBorderEdge;

            // Assuming we were correctly passed an actual component...
            if (_borderEdge != null)
            {
                // Get access to the actual Orientation property
                PropertyDescriptor orientationProp = TypeDescriptor.GetProperties(_borderEdge)["Orientation"];

                // If we succeeded in getting the property
                if (orientationProp != null)
                {
                    // Decide on the next action to take given the current setting
                    _action = (Orientation)orientationProp.GetValue(_borderEdge) == Orientation.Vertical ? "Horizontal border orientation" : "Vertical border orientation";
                }
            }

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the border style.
        /// </summary>
        public PaletteBorderStyle BorderStyle
        {
            get => _borderEdge.BorderStyle;

            set 
            {
                if (_borderEdge.BorderStyle != value)
                {
                    _service.OnComponentChanged(_borderEdge, null, _borderEdge.BorderStyle, value);
                    _borderEdge.BorderStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the auto size property.
        /// </summary>
        public bool AutoSize
        {
            get => _borderEdge.AutoSize;

            set
            {
                if (_borderEdge.AutoSize != value)
                {
                    _service.OnComponentChanged(_borderEdge, null, _borderEdge.AutoSize, value);
                    _borderEdge.AutoSize = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the docking property.
        /// </summary>
        public DockStyle Dock
        {
            get => _borderEdge.Dock;

            set
            {
                if (_borderEdge.Dock != value)
                {
                    _service.OnComponentChanged(_borderEdge, null, _borderEdge.Dock, value);
                    _borderEdge.Dock = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get => _borderEdge.PaletteMode;

            set 
            {
                if (_borderEdge.PaletteMode != value)
                {
                    _service.OnComponentChanged(_borderEdge, null, _borderEdge.PaletteMode, value);
                    _borderEdge.PaletteMode = value;
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
            if (_borderEdge != null)
            {
                // Add our own action to the end
                actions.Add(new DesignerActionHeaderItem("Appearance"));
                actions.Add(new DesignerActionPropertyItem("BorderStyle", "Border style", "Appearance", "Border style"));
                actions.Add(new DesignerActionHeaderItem("Layout"));
                actions.Add(new DesignerActionPropertyItem("AutoSize", "AutoSize", "Layout", "Determines whether the control resizes based on its contents."));
                actions.Add(new DesignerActionPropertyItem("Dock", "Dock", "Layout", "Determines how the control is sized with its parent."));
                actions.Add(new KryptonDesignerActionItem(new DesignerVerb(_action, OnOrientationClick), "Layout"));
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
                Orientation orientation = verb.Text.Equals("Horizontal border orientation") ? Orientation.Horizontal : Orientation.Vertical;

                // Decide on the next action to take given the new setting
                _action = orientation == Orientation.Vertical ? "Horizontal border orientation" : "Vertical border orientation";

                // Get access to the actual Orientation property
                PropertyDescriptor orientationProp = TypeDescriptor.GetProperties(_borderEdge)["Orientation"];

                // If we succeeded in getting the property
                // Update the actual property with the new value
                orientationProp?.SetValue(_borderEdge, orientation);

                // Get the user interface service associated with actions

                // If we managed to get it then request it update to reflect new action setting
                if (GetService(typeof(DesignerActionUIService)) is DesignerActionUIService service)
                {
                    service.Refresh(_borderEdge);
                }
            }
        }
        #endregion
    }
}

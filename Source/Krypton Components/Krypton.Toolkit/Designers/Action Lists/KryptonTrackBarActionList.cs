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
    internal class KryptonTrackBarActionList : DesignerActionList
    {
        #region Instance Fields
        private readonly KryptonTrackBar _trackBar;
        private readonly IComponentChangeService _service;
        private string _action;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonTrackBarActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonTrackBarActionList(KryptonTrackBarDesigner owner) 
            : base(owner.Component)
        {
            _trackBar = owner.Component as KryptonTrackBar;

            // Assuming we were correctly passed an actual component...
            if (_trackBar != null)
            {
                // Get access to the actual Orientation property
                PropertyDescriptor orientationProp = TypeDescriptor.GetProperties(_trackBar)["Orientation"];

                // If we succeeded in getting the property
                if (orientationProp != null)
                {
                    // Decide on the next action to take given the current setting
                    _action = (Orientation) orientationProp.GetValue(_trackBar) == Orientation.Vertical
                        ? "Horizontal orientation"
                        : "Vertical orientation";
                }
            }

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get => _trackBar.PaletteMode;

            set 
            {
                if (_trackBar.PaletteMode != value)
                {
                    _service.OnComponentChanged(_trackBar, null, _trackBar.PaletteMode, value);
                    _trackBar.PaletteMode = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the track bar tick style.
        /// </summary>
        public TickStyle TickStyle
        {
            get => _trackBar.TickStyle;

            set
            {
                if (_trackBar.TickStyle != value)
                {
                    _service.OnComponentChanged(_trackBar, null, _trackBar.TickStyle, value);
                    _trackBar.TickStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the track bar size.
        /// </summary>
        public PaletteTrackBarSize TrackBarSize
        {
            get => _trackBar.TrackBarSize;

            set
            {
                if (_trackBar.TrackBarSize != value)
                {
                    _service.OnComponentChanged(_trackBar, null, _trackBar.TrackBarSize, value);
                    _trackBar.TrackBarSize = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the track bar minium value.
        /// </summary>
        public int Minimum
        {
            get => _trackBar.Minimum;

            set
            {
                if (_trackBar.Minimum != value)
                {
                    _service.OnComponentChanged(_trackBar, null, _trackBar.Minimum, value);
                    _trackBar.Minimum = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the track bar maximum value.
        /// </summary>
        public int Maximum
        {
            get => _trackBar.Maximum;

            set
            {
                if (_trackBar.Maximum != value)
                {
                    _service.OnComponentChanged(_trackBar, null, _trackBar.Maximum, value);
                    _trackBar.Maximum = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the track bar small change value.
        /// </summary>
        public int SmallChange
        {
            get => _trackBar.SmallChange;

            set
            {
                if (_trackBar.SmallChange != value)
                {
                    _service.OnComponentChanged(_trackBar, null, _trackBar.SmallChange, value);
                    _trackBar.SmallChange = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the track bar large change value.
        /// </summary>
        public int LargeChange
        {
            get => _trackBar.LargeChange;

            set
            {
                if (_trackBar.LargeChange != value)
                {
                    _service.OnComponentChanged(_trackBar, null, _trackBar.LargeChange, value);
                    _trackBar.LargeChange = value;
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
            if (_trackBar != null)
            {
                // Add our own action to the end
                actions.Add(new DesignerActionHeaderItem("Layout"));
                actions.Add(new DesignerActionPropertyItem("TickStyle", "Tick Style", "Layout", "Tick style"));
                actions.Add(new DesignerActionPropertyItem("TrackBarSize", "TrackBar Size", "Layout", "Size of the track bar"));
                actions.Add(new KryptonDesignerActionItem(new DesignerVerb(_action, OnOrientationClick), "Layout"));
                actions.Add(new DesignerActionHeaderItem("Values"));
                actions.Add(new DesignerActionPropertyItem("Minimum", "Minimum", "Values", "Minium value"));
                actions.Add(new DesignerActionPropertyItem("Maximum", "Maximum", "Values", "Maximum value"));
                actions.Add(new DesignerActionPropertyItem("SmallChange", "Small Change", "Values", "Small change value"));
                actions.Add(new DesignerActionPropertyItem("LargeChange", "Large Change", "Values", "Large change value"));
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
                Orientation orientation = verb.Text.Equals("Horizontal orientation") ? Orientation.Horizontal : Orientation.Vertical;

                // Decide on the next action to take given the new setting
                _action = orientation == Orientation.Vertical ? "Horizontal orientation" : "Vertical orientation";

                // Get access to the actual Orientation property
                PropertyDescriptor orientationProp = TypeDescriptor.GetProperties(_trackBar)["Orientation"];

                // If we succeeded in getting the property
                // Update the actual property with the new value
                orientationProp?.SetValue(_trackBar, orientation);

                // Get the user interface service associated with actions

                // If we managed to get it then request it update to reflect new action setting
                if (GetService(typeof(DesignerActionUIService)) is DesignerActionUIService service)
                {
                    service.Refresh(_trackBar);
                }
            }
        }
        #endregion
    }
}

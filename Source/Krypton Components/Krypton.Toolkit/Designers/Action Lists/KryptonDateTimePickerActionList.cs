#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Toolkit
{
    internal class KryptonDateTimePickerActionList : DesignerActionList
    {
        #region Instance Fields
        private readonly KryptonDateTimePicker _dateTimePicker;
        private readonly IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDateTimePickerActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonDateTimePickerActionList(KryptonDateTimePickerDesigner owner)
            : base(owner.Component)
        {
            // Remember the bread crumb control instance
            _dateTimePicker = owner.Component as KryptonDateTimePicker;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion

        #region Public
        /// <summary>Gets or sets the context menu strip.</summary>
        /// <value>The context menu strip.</value>
        public ContextMenuStrip ContextMenuStrip
        {
            get => _dateTimePicker.ContextMenuStrip;

            set
            {
                if (_dateTimePicker.ContextMenuStrip != value)
                {
                    _service.OnComponentChanged(_dateTimePicker, null, _dateTimePicker.ContextMenuStrip, value);

                    _dateTimePicker.ContextMenuStrip = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the display format.
        /// </summary>
        public DateTimePickerFormat Format
        {
            get => _dateTimePicker.Format;

            set
            {
                if (_dateTimePicker.Format != value)
                {
                    _service.OnComponentChanged(_dateTimePicker, null, _dateTimePicker.Format, value);
                    _dateTimePicker.Format = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the display of up/down buttons.
        /// </summary>
        public bool ShowUpDown
        {
            get => _dateTimePicker.ShowUpDown;

            set
            {
                if (_dateTimePicker.ShowUpDown != value)
                {
                    _service.OnComponentChanged(_dateTimePicker, null, _dateTimePicker.ShowUpDown, value);
                    _dateTimePicker.ShowUpDown = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the display of a check box.
        /// </summary>
        public bool ShowCheckBox
        {
            get => _dateTimePicker.ShowCheckBox;

            set
            {
                if (_dateTimePicker.ShowCheckBox != value)
                {
                    _service.OnComponentChanged(_dateTimePicker, null, _dateTimePicker.ShowCheckBox, value);
                    _dateTimePicker.ShowCheckBox = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the checked state of the check box.
        /// </summary>
        public bool Checked
        {
            get => _dateTimePicker.Checked;

            set
            {
                if (_dateTimePicker.Checked != value)
                {
                    _service.OnComponentChanged(_dateTimePicker, null, _dateTimePicker.Checked, value);
                    _dateTimePicker.Checked = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get => _dateTimePicker.PaletteMode;

            set
            {
                if (_dateTimePicker.PaletteMode != value)
                {
                    _service.OnComponentChanged(_dateTimePicker, null, _dateTimePicker.PaletteMode, value);
                    _dateTimePicker.PaletteMode = value;
                }
            }
        }

        public Font Font
        {
            get => _dateTimePicker.StateCommon.Content.Font;

            set
            {
                if (_dateTimePicker.StateCommon.Content.Font != value)
                {
                    _service.OnComponentChanged(_dateTimePicker, null, _dateTimePicker.StateCommon.Content.Font, value);

                    _dateTimePicker.StateCommon.Content.Font = value;
                }
            }
        }

        /// <summary>Gets or sets the corner radius.</summary>
        /// <value>The corner radius.</value>
        [DefaultValue(GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE)]
        public float CornerRadius
        {
            get => _dateTimePicker.StateCommon.Border.Rounding;

            set
            {
                if (_dateTimePicker.StateCommon.Border.Rounding != value)
                {
                    _service.OnComponentChanged(_dateTimePicker, null, _dateTimePicker.StateCommon.Border.Rounding, value);

                    _dateTimePicker.StateCommon.Border.Rounding = value;
                }
            }
        }
        #endregion

        #region Public Override
        /// <summary>
        /// Returns the collection of DesignerActionItem objects contained in the list.
        /// </summary>
        /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            // Create a new collection for holding the single item we want to create
            DesignerActionItemCollection actions = new();

            // This can be null when deleting a control instance at design time
            if (_dateTimePicker != null)
            {
                // Add the list of bread crumb specific actions
                actions.Add(new DesignerActionHeaderItem("Appearance"));
                actions.Add(new DesignerActionPropertyItem("ContextMenuStrip", "Context Menu Strip", "Appearance", "The context menu strip for the control."));
                actions.Add(new DesignerActionPropertyItem("Format", "Format", "Appearance", "Decide what to display in the edit portion of the control"));
                actions.Add(new DesignerActionPropertyItem("ShowUpDown", "ShowUpDown", "Appearance", "Display up and down buttons for modifying dates and times"));
                actions.Add(new DesignerActionPropertyItem("ShowCheckBox", "ShowCheckBox", "Appearance", "Display a check box allowing the user to set the value is null"));
                actions.Add(new DesignerActionPropertyItem("Checked", "Checked", "Appearance", "Is the current value null"));
                actions.Add(new DesignerActionPropertyItem("Font", "Font", "Appearance", "The font for the date time picker."));
                actions.Add(new DesignerActionPropertyItem("CornerRadius", "Corner Rounding Radius", "Appearance", "The corner rounding radius of the control."));
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("PaletteMode", "Palette", "Visuals", "Palette applied to drawing"));
            }

            return actions;
        }
        #endregion
    }
}

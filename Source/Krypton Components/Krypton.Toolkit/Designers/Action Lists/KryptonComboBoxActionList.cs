#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion


namespace Krypton.Toolkit
{
    internal class KryptonComboBoxActionList : DesignerActionList
    {
        #region Instance Fields
        private readonly KryptonComboBox _comboBox;
        private readonly IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonComboBoxActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonComboBoxActionList(KryptonComboBoxDesigner owner)
            : base(owner.Component)
        {
            // Remember the combo box instance
            _comboBox = owner.Component as KryptonComboBox;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion

        #region Public
        /// <summary>Gets or sets the context menu strip.</summary>
        /// <value>The context menu strip.</value>
        public ContextMenuStrip ContextMenuStrip
        {
            get => _comboBox.ContextMenuStrip;

            set
            {
                if (_comboBox.ContextMenuStrip != value)
                {
                    _service.OnComponentChanged(_comboBox, null, _comboBox.ContextMenuStrip, value);

                    _comboBox.ContextMenuStrip = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get => _comboBox.PaletteMode;

            set
            {
                if (_comboBox.PaletteMode != value)
                {
                    _service.OnComponentChanged(_comboBox, null, _comboBox.PaletteMode, value);
                    _comboBox.PaletteMode = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the input control style.
        /// </summary>
        public InputControlStyle InputControlStyle
        {
            get => _comboBox.InputControlStyle;

            set
            {
                if (_comboBox.InputControlStyle != value)
                {
                    _service.OnComponentChanged(_comboBox, null, _comboBox.InputControlStyle, value);
                    _comboBox.InputControlStyle = value;
                }
            }
        }

        /// <summary>Gets or sets the font.</summary>
        /// <value>The font.</value>
        public Font Font
        {
            get => _comboBox.StateCommon.ComboBox.Content.Font;

            set
            {
                if (_comboBox.StateCommon.ComboBox.Content.Font != value)
                {
                    _service.OnComponentChanged(_comboBox, null, _comboBox.StateCommon.ComboBox.Content.Font, value);

                    _comboBox.StateCommon.ComboBox.Content.Font = value;
                }
            }
        }

        /// <summary>Gets or sets the corner radius.</summary>
        /// <value>The corner radius.</value>
        [DefaultValue(GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE)]
        public float CornerRadius
        {
            get => _comboBox.StateCommon.ComboBox.Border.Rounding;

            set
            {
                if (_comboBox.StateCommon.ComboBox.Border.Rounding != value)
                {
                    _service.OnComponentChanged(_comboBox, null, _comboBox.StateCommon.ComboBox.Border.Rounding, value);

                    _comboBox.StateCommon.ComboBox.Border.Rounding = value;
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
            if (_comboBox != null)
            {
                // Add the list of label specific actions
                actions.Add(new DesignerActionHeaderItem("Appearance"));
                actions.Add(new DesignerActionPropertyItem("ContextMenuStrip", "Context Menu Strip", "Appearance", "The context menu strip for the control."));
                actions.Add(new DesignerActionPropertyItem("InputControlStyle", "Style", "Appearance", "ComboBox display style."));
                actions.Add(new DesignerActionPropertyItem("Font", "Font", "Appearance", "The font for the combobox."));
                actions.Add(new DesignerActionPropertyItem("CornerRadius", "Corner Rounding Radius", "Appearance", "The corner rounding radius of the control."));
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("PaletteMode", "Palette", "Visuals", "Palette applied to drawing"));
            }

            return actions;
        }
        #endregion
    }
}

﻿namespace Krypton.Toolkit
{
    internal class KryptonMaskedTextBoxActionList : DesignerActionList
    {
        #region Instance Fields
        private readonly KryptonMaskedTextBox _maskedTextBox;
        private readonly IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonMaskedTextBoxActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonMaskedTextBoxActionList(KryptonMaskedTextBoxDesigner owner)
            : base(owner.Component)
        {
            // Remember the text box instance
            _maskedTextBox = owner.Component as KryptonMaskedTextBox;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion

        #region Public
        /// <summary>Gets or sets the Krypton Context Menu.</summary>
        /// <value>The Krypton Context Menu.</value>
        public KryptonContextMenu KryptonContextMenu
        {
            get => _maskedTextBox.KryptonContextMenu;

            set
            {
                if (_maskedTextBox.KryptonContextMenu != value)
                {
                    _service.OnComponentChanged(_maskedTextBox, null, _maskedTextBox.KryptonContextMenu, value);

                    _maskedTextBox.KryptonContextMenu = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get => _maskedTextBox.PaletteMode;

            set
            {
                if (_maskedTextBox.PaletteMode != value)
                {
                    _service.OnComponentChanged(_maskedTextBox, null, _maskedTextBox.PaletteMode, value);
                    _maskedTextBox.PaletteMode = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the input control style.
        /// </summary>
        public InputControlStyle InputControlStyle
        {
            get => _maskedTextBox.InputControlStyle;

            set
            {
                if (_maskedTextBox.InputControlStyle != value)
                {
                    _service.OnComponentChanged(_maskedTextBox, null, _maskedTextBox.InputControlStyle, value);
                    _maskedTextBox.InputControlStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the input mask.
        /// </summary>
        public string Mask
        {
            get => _maskedTextBox.Mask;

            set
            {
                if (_maskedTextBox.Mask != value)
                {
                    _service.OnComponentChanged(_maskedTextBox, null, _maskedTextBox.Mask, value);
                    _maskedTextBox.Mask = value;
                }
            }
        }

        // <summary>Gets or sets the text box font.</summary>
        /// <value>The text box font.</value>
        public Font Font
        {
            get => _maskedTextBox.StateCommon.Content.Font;

            set
            {
                if (_maskedTextBox.StateCommon.Content.Font != value)
                {
                    _service.OnComponentChanged(_maskedTextBox, null, _maskedTextBox.StateCommon.Content.Font, value);

                    _maskedTextBox.StateCommon.Content.Font = value;
                }
            }
        }

        /// <summary>Gets or sets the corner radius.</summary>
        /// <value>The corner radius.</value>
        [DefaultValue(GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE)]
        public float StateCommonCornerRoundingRadius
        {
            get => _maskedTextBox.StateCommon.Border.Rounding;

            set
            {
                if (_maskedTextBox.StateCommon.Border.Rounding != value)
                {
                    _service.OnComponentChanged(_maskedTextBox, null, _maskedTextBox.StateCommon.Border.Rounding, value);

                    _maskedTextBox.StateCommon.Border.Rounding = value;
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
            if (_maskedTextBox != null)
            {
                // Add the list of label specific actions
                actions.Add(new DesignerActionHeaderItem(@"Appearance"));
                actions.Add(new DesignerActionPropertyItem(@"KryptonContextMenu", @"Krypton Context Menu", @"Appearance", @"The Krypton Context Menu for the control."));
                actions.Add(new DesignerActionPropertyItem(@"InputControlStyle", @"Style", @"Appearance", @"TextBox display style."));
                actions.Add(new DesignerActionPropertyItem(@"Font", @"Font", @"Appearance", @"Modifies the font of the control."));
                actions.Add(new DesignerActionPropertyItem(@"StateCommonCornerRoundingRadius", @"State Common Corner Rounding Radius", @"Appearance", @"The corner rounding radius of the control."));
                actions.Add(new DesignerActionHeaderItem(@"MaskedTextBox"));
                actions.Add(new DesignerActionPropertyItem(@"Mask", @"Mask", @"MaskedTextBox", @"Input mask."));
                actions.Add(new DesignerActionHeaderItem(@"Visuals"));
                actions.Add(new DesignerActionPropertyItem(@"PaletteMode", @"Palette", @"Visuals", @"Palette applied to drawing"));
            }

            return actions;
        }
        #endregion
    }
}

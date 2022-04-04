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
    internal class KryptonRadioButtonActionList : DesignerActionList
    {
        #region Instance Fields
        private readonly KryptonRadioButton _radioButton;
        private readonly IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonRadioButtonActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonRadioButtonActionList(KryptonRadioButtonDesigner owner)
            : base(owner.Component)
        {
            // Remember the radio button instance
            _radioButton = owner.Component as KryptonRadioButton;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets a value indicating if the radio button is checked.
        /// </summary>
        public bool Checked
        {
            get => _radioButton.Checked;

            set
            {
                if (_radioButton.Checked != value)
                {
                    _service.OnComponentChanged(_radioButton, null, _radioButton.Checked, value);
                    _radioButton.Checked = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets a value indicating if the radio button should be three state.
        /// </summary>
        public bool AutoCheck
        {
            get => _radioButton.AutoCheck;

            set
            {
                if (_radioButton.AutoCheck != value)
                {
                    _service.OnComponentChanged(_radioButton, null, _radioButton.AutoCheck, value);
                    _radioButton.AutoCheck = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the label style.
        /// </summary>
        public LabelStyle LabelStyle
        {
            get => _radioButton.LabelStyle;

            set
            {
                if (_radioButton.LabelStyle != value)
                {
                    _service.OnComponentChanged(_radioButton, null, _radioButton.LabelStyle, value);
                    _radioButton.LabelStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the visual orientation.
        /// </summary>
        public VisualOrientation Orientation
        {
            get => _radioButton.Orientation;

            set
            {
                if (_radioButton.Orientation != value)
                {
                    _service.OnComponentChanged(_radioButton, null, _radioButton.Orientation, value);
                    _radioButton.Orientation = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the radio button text.
        /// </summary>
        public string Text
        {
            get => _radioButton.Values.Text;

            set
            {
                if (_radioButton.Values.Text != value)
                {
                    _service.OnComponentChanged(_radioButton, null, _radioButton.Values.Text, value);
                    _radioButton.Values.Text = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the extra radio button text.
        /// </summary>
        public string ExtraText
        {
            get => _radioButton.Values.ExtraText;

            set
            {
                if (_radioButton.Values.ExtraText != value)
                {
                    _service.OnComponentChanged(_radioButton, null, _radioButton.Values.ExtraText, value);
                    _radioButton.Values.ExtraText = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the radio button image.
        /// </summary>
        public Image Image
        {
            get => _radioButton.Values.Image;

            set
            {
                if (_radioButton.Values.Image != value)
                {
                    _service.OnComponentChanged(_radioButton, null, _radioButton.Values.Image, value);
                    _radioButton.Values.Image = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get => _radioButton.PaletteMode;

            set
            {
                if (_radioButton.PaletteMode != value)
                {
                    _service.OnComponentChanged(_radioButton, null, _radioButton.PaletteMode, value);
                    _radioButton.PaletteMode = value;
                }
            }
        }

        /// <summary>Gets or sets the font.</summary>
        /// <value>The font.</value>
        public Font StateCommonShortTextFont
        {
            get => _radioButton.StateCommon.ShortText.Font;

            set
            {
                if (_radioButton.StateCommon.ShortText.Font != value)
                {
                    _service.OnComponentChanged(_radioButton, null, _radioButton.StateCommon.ShortText.Font, value);

                    _radioButton.StateCommon.ShortText.Font = value;
                }
            }
        }

        /// <summary>Gets or sets the font.</summary>
        /// <value>The font.</value>
        public Font StateCommonLongTextFont
        {
            get => _radioButton.StateCommon.LongText.Font;

            set
            {
                if (_radioButton.StateCommon.LongText.Font != value)
                {
                    _service.OnComponentChanged(_radioButton, null, _radioButton.StateCommon.LongText.Font, value);

                    _radioButton.StateCommon.LongText.Font = value;
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
            if (_radioButton != null)
            {
                // Add the list of radio button specific actions
                actions.Add(new DesignerActionHeaderItem(@"Operation"));
                actions.Add(new DesignerActionPropertyItem(@"Checked", @"Checked", @"Operation", @"Checked state"));
                actions.Add(new DesignerActionPropertyItem(@"AutoCheck", @"AutoCheck", @"Operation", @"AutoCheck of other instances."));
                actions.Add(new DesignerActionHeaderItem(@"Appearance"));
                actions.Add(new DesignerActionPropertyItem(@"LabelStyle", @"Style", @"Appearance", @"Label style"));
                actions.Add(new DesignerActionPropertyItem(@"Orientation", @"Orientation", @"Appearance", @"Visual orientation"));
                actions.Add(new DesignerActionPropertyItem(@"StateCommonShortTextFont", @"State Common Short Text Font", @"Appearance", @"The State Common Short Text Font."));
                actions.Add(new DesignerActionPropertyItem(@"StateCommonLongTextFont", @"State Common State Common Long Text Font", @"Appearance", @"The State Common State Common Long Text Font."));
                actions.Add(new DesignerActionHeaderItem(@"Values"));
                actions.Add(new DesignerActionPropertyItem(@"Text", @"Text", @"Values", @"Radio button text"));
                actions.Add(new DesignerActionPropertyItem(@"ExtraText", @"ExtraText", @"Values", @"Radio button extra text"));
                actions.Add(new DesignerActionPropertyItem(@"Image", @"Image", @"Values", @"Radio button image"));
                actions.Add(new DesignerActionHeaderItem(@"Visuals"));
                actions.Add(new DesignerActionPropertyItem(@"PaletteMode", @"Palette", @"Visuals", @"Palette applied to drawing"));
            }

            return actions;
        }
        #endregion
    }
}

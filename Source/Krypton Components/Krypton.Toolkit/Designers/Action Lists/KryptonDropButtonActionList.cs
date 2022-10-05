﻿namespace Krypton.Toolkit
{
    internal class KryptonDropButtonActionList : DesignerActionList
    {
        #region Instance Fields
        private readonly KryptonDropButton _dropButton;
        private readonly IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDropButtonActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonDropButtonActionList(KryptonDropButtonDesigner owner)
            : base(owner.Component)
        {
            // Remember the button instance
            _dropButton = owner.Component as KryptonDropButton;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the button style.
        /// </summary>
        public ButtonStyle ButtonStyle
        {
            get => _dropButton.ButtonStyle;

            set
            {
                if (_dropButton.ButtonStyle != value)
                {
                    _service.OnComponentChanged(_dropButton, null, _dropButton.ButtonStyle, value);
                    _dropButton.ButtonStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the visual button orientation.
        /// </summary>
        public VisualOrientation ButtonOrientation
        {
            get => _dropButton.ButtonOrientation;

            set
            {
                if (_dropButton.ButtonOrientation != value)
                {
                    _service.OnComponentChanged(_dropButton, null, _dropButton.ButtonOrientation, value);
                    _dropButton.ButtonOrientation = value;
                }
            }
        }

        /// <summary>Gets or sets the Krypton Context Menu.</summary>
        /// <value>The Krypton Context Menu.</value>
        public KryptonContextMenu KryptonContextMenu
        {
            get => _dropButton.KryptonContextMenu;

            set
            {
                if (_dropButton.KryptonContextMenu != value)
                {
                    _service.OnComponentChanged(_dropButton, null, _dropButton.KryptonContextMenu, value);

                    _dropButton.KryptonContextMenu = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the visual drop down position.
        /// </summary>
        public VisualOrientation DropDownPosition
        {
            get => _dropButton.DropDownPosition;

            set
            {
                if (_dropButton.DropDownPosition != value)
                {
                    _service.OnComponentChanged(_dropButton, null, _dropButton.DropDownPosition, value);
                    _dropButton.DropDownPosition = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the visual drop down orientation.
        /// </summary>
        public VisualOrientation DropDownOrientation
        {
            get => _dropButton.DropDownOrientation;

            set
            {
                if (_dropButton.DropDownOrientation != value)
                {
                    _service.OnComponentChanged(_dropButton, null, _dropButton.DropDownOrientation, value);
                    _dropButton.DropDownOrientation = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the splitter or drop down functionality.
        /// </summary>
        public bool Splitter
        {
            get => _dropButton.Splitter;

            set
            {
                if (_dropButton.Splitter != value)
                {
                    _service.OnComponentChanged(_dropButton, null, _dropButton.Splitter, value);
                    _dropButton.Splitter = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the button text.
        /// </summary>
        public string Text
        {
            get => _dropButton.Values.Text;

            set
            {
                if (_dropButton.Values.Text != value)
                {
                    _service.OnComponentChanged(_dropButton, null, _dropButton.Values.Text, value);
                    _dropButton.Values.Text = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the extra button text.
        /// </summary>
        public string ExtraText
        {
            get => _dropButton.Values.ExtraText;

            set
            {
                if (_dropButton.Values.ExtraText != value)
                {
                    _service.OnComponentChanged(_dropButton, null, _dropButton.Values.ExtraText, value);
                    _dropButton.Values.ExtraText = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the button image.
        /// </summary>
        public Image Image
        {
            get => _dropButton.Values.Image;

            set
            {
                if (_dropButton.Values.Image != value)
                {
                    _service.OnComponentChanged(_dropButton, null, _dropButton.Values.Image, value);
                    _dropButton.Values.Image = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get => _dropButton.PaletteMode;

            set
            {
                if (_dropButton.PaletteMode != value)
                {
                    _service.OnComponentChanged(_dropButton, null, _dropButton.PaletteMode, value);
                    _dropButton.PaletteMode = value;
                }
            }
        }

        /// <summary>Gets or sets the font.</summary>
        /// <value>The font.</value>
        public Font StateCommonShortTextFont
        {
            get => _dropButton.StateCommon.Content.ShortText.Font;

            set
            {
                if (_dropButton.StateCommon.Content.ShortText.Font != value)
                {
                    _service.OnComponentChanged(_dropButton, null, _dropButton.StateCommon.Content.ShortText.Font, value);

                    _dropButton.StateCommon.Content.ShortText.Font = value;
                }
            }
        }

        /// <summary>Gets or sets the font.</summary>
        /// <value>The font.</value>
        public Font StateCommonLongTextFont
        {
            get => _dropButton.StateCommon.Content.LongText.Font;

            set
            {
                if (_dropButton.StateCommon.Content.LongText.Font != value)
                {
                    _service.OnComponentChanged(_dropButton, null, _dropButton.StateCommon.Content.LongText.Font, value);

                    _dropButton.StateCommon.Content.LongText.Font = value;
                }
            }
        }

        /// <summary>Gets or sets the corner radius.</summary>
        /// <value>The corner radius.</value>
        [DefaultValue(GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE)]
        public float StateCommonCornerRoundingRadius
        {
            get => _dropButton.StateCommon.Border.Rounding;

            set
            {
                if (_dropButton.StateCommon.Border.Rounding != value)
                {
                    _service.OnComponentChanged(_dropButton, null, _dropButton.StateCommon.Border.Rounding, value);

                    _dropButton.StateCommon.Border.Rounding = value;
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
            if (_dropButton != null)
            {
                // Add the list of button specific actions
                actions.Add(new DesignerActionHeaderItem(@"Appearance"));
                actions.Add(new DesignerActionPropertyItem(@"Splitter", @"Splitter", @"Appearance", @"Splitter of DropDown"));
                actions.Add(new DesignerActionPropertyItem(@"ButtonStyle", @"ButtonStyle", @"Appearance", @"Button style"));
                actions.Add(new DesignerActionPropertyItem(@"ButtonOrientation", @"ButtonOrientation", @"Appearance", @"Button orientation"));
                actions.Add(new DesignerActionPropertyItem(@"KryptonContextMenu", @"Krypton Context Menu", @"Appearance", @"The Krypton Context Menu for the control."));
                actions.Add(new DesignerActionPropertyItem(@"DropDownPosition", @"DropDownPosition", @"Appearance", @"DropDown position"));
                actions.Add(new DesignerActionPropertyItem(@"DropDownOrientation", @"DropDownOrientation", @"Appearance", @"DropDown orientation"));
                actions.Add(new DesignerActionPropertyItem(@"StateCommonShortTextFont", @"State Common Short Text Font", @"Appearance", @"The State Common Short Text Font."));
                actions.Add(new DesignerActionPropertyItem(@"StateCommonLongTextFont", @"State Common State Common Long Text Font", @"Appearance", @"The State Common State Common Long Text Font."));
                actions.Add(new DesignerActionPropertyItem(@"StateCommonCornerRoundingRadius", @"State Common Corner Rounding Radius", @"Appearance", @"The corner rounding radius of the control."));
                actions.Add(new DesignerActionHeaderItem(@"Values"));
                actions.Add(new DesignerActionPropertyItem(@"Text", @"Text", @"Values", @"Button text"));
                actions.Add(new DesignerActionPropertyItem(@"ExtraText", @"ExtraText", @"Values", @"Button extra text"));
                actions.Add(new DesignerActionPropertyItem(@"Image", @"Image", @"Values", @"Button image"));
                actions.Add(new DesignerActionHeaderItem(@"Visuals"));
                actions.Add(new DesignerActionPropertyItem(@"PaletteMode", @"Palette", @"Visuals", @"Palette applied to drawing"));
            }

            return actions;
        }
        #endregion
    }
}

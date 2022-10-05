﻿namespace Krypton.Toolkit
{
    internal class KryptonButtonActionList : DesignerActionList
    {
        #region Instance Fields
        private readonly KryptonButton _button;
        private readonly IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonButtonActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonButtonActionList(KryptonButtonDesigner owner)
            : base(owner.Component)
        {
            // Remember the button instance
            _button = owner.Component as KryptonButton;

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
            get => _button.ButtonStyle;

            set
            {
                if (_button.ButtonStyle != value)
                {
                    _service.OnComponentChanged(_button, null, _button.ButtonStyle, value);
                    _button.ButtonStyle = value;
                }
            }
        }

        /// <summary>Gets or sets the dialog result.</summary>
        /// <value>The dialog result.</value>
        public DialogResult DialogResult
        {
            get => _button.DialogResult;

            set
            {
                if (_button.DialogResult != value)
                {
                    _service.OnComponentChanged(_button, null, _button.DialogResult, value);
                    _button.DialogResult = value;
                }
            }
        }

        /// <summary>Gets or sets the krypton context menu.</summary>
        /// <value>The krypton context menu.</value>
        public KryptonContextMenu KryptonContextMenu
        {
            get => _button.KryptonContextMenu;

            set
            {
                if (_button.KryptonContextMenu != value)
                {
                    _service.OnComponentChanged(_button, null, _button.KryptonContextMenu, value);

                    _button.KryptonContextMenu = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the visual orientation.
        /// </summary>
        public VisualOrientation Orientation
        {
            get => _button.Orientation;

            set
            {
                if (_button.Orientation != value)
                {
                    _service.OnComponentChanged(_button, null, _button.Orientation, value);
                    _button.Orientation = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the button text.
        /// </summary>
        public string Text
        {
            get => _button.Values.Text;

            set
            {
                if (_button.Values.Text != value)
                {
                    _service.OnComponentChanged(_button, null, _button.Values.Text, value);
                    _button.Values.Text = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the extra button text.
        /// </summary>
        public string ExtraText
        {
            get => _button.Values.ExtraText;

            set
            {
                if (_button.Values.ExtraText != value)
                {
                    _service.OnComponentChanged(_button, null, _button.Values.ExtraText, value);
                    _button.Values.ExtraText = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the button image.
        /// </summary>
        public Image Image
        {
            get => _button.Values.Image;

            set
            {
                if (_button.Values.Image != value)
                {
                    _service.OnComponentChanged(_button, null, _button.Values.Image, value);
                    _button.Values.Image = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get => _button.PaletteMode;

            set
            {
                if (_button.PaletteMode != value)
                {
                    _service.OnComponentChanged(_button, null, _button.PaletteMode, value);
                    _button.PaletteMode = value;
                }
            }
        }

        /// <summary>Gets or sets the font.</summary>
        /// <value>The font.</value>
        public Font StateCommonShortTextFont
        {
            get => _button.StateCommon.Content.ShortText.Font;

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
        public Font StateCommonLongTextFont
        {
            get => _button.StateCommon.Content.LongText.Font;

            set
            {
                if (_button.StateCommon.Content.LongText.Font != value)
                {
                    _service.OnComponentChanged(_button, null, _button.StateCommon.Content.LongText.Font, value);

                    _button.StateCommon.Content.LongText.Font = value;
                }
            }
        }

        /// <summary>Gets or sets the corner radius.</summary>
        /// <value>The corner radius.</value>
        [DefaultValue(GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE)]
        public float StateCommonCornerRoundingRadius
        {
            get => _button.StateCommon.Border.Rounding;

            set
            {
                if (_button.StateCommon.Border.Rounding != value)
                {
                    _service.OnComponentChanged(_button, null, _button.StateCommon.Border.Rounding, value);

                    _button.StateCommon.Border.Rounding = value;
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether [use as uac elevated button].</summary>
        /// <value><c>true</c> if [use as uac elevated button]; otherwise, <c>false</c>.</value>
        [DefaultValue(false)]
        public bool UseAsUACElevatedButton
        {
            get => _button.UseAsUACElevationButton;

            set
            {
                if (_button.UseAsUACElevationButton != value)
                {
                    _service.OnComponentChanged(_button, null, _button.UseAsUACElevationButton, value);

                    _button.UseAsUACElevationButton = value;
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
            if (_button != null)
            {
                // Add the list of button specific actions
                actions.Add(new DesignerActionHeaderItem(@"Appearance"));
                actions.Add(new DesignerActionPropertyItem(@"ButtonStyle", @"Style", @"Appearance", @"Button style"));
                actions.Add(new DesignerActionPropertyItem(@"KryptonContextMenu", @"Krypton Context Menu", @"Appearance", @"The Krypton Context Menu for the control."));
                actions.Add(new DesignerActionPropertyItem(@"Orientation", @"Orientation", @"Appearance", @"Button orientation"));
                actions.Add(new DesignerActionPropertyItem(@"StateCommonShortTextFont", @"State Common Short Text Font", @"Appearance", @"The State Common Short Text Font."));
                actions.Add(new DesignerActionPropertyItem(@"StateCommonLongTextFont", @"State Common State Common Long Text Font", @"Appearance", @"The State Common State Common Long Text Font."));
                actions.Add(new DesignerActionPropertyItem(@"StateCommonCornerRoundingRadius", @"State Common Corner Rounding Radius", @"Appearance", @"The corner rounding radius of the control."));
                actions.Add(new DesignerActionHeaderItem(@"Values"));
                actions.Add(new DesignerActionPropertyItem(@"Text", @"Text", @"Values", @"Button text"));
                actions.Add(new DesignerActionPropertyItem(@"ExtraText", @"ExtraText", @"Values", @"Button extra text"));
                actions.Add(new DesignerActionPropertyItem(@"Image", @"Image", @"Values", @"Button image"));
                actions.Add(new DesignerActionPropertyItem(@"DialogResult", @"DialogResult", @"Values", @"The DialogResult for this button"));
                actions.Add(new DesignerActionHeaderItem(@"Visuals"));
                actions.Add(new DesignerActionPropertyItem(@"PaletteMode", @"Palette", @"Visuals", @"Palette applied to drawing"));
                actions.Add(new DesignerActionHeaderItem(@"UAC Elevation"));
                actions.Add(new DesignerActionPropertyItem(@"UseAsUACElevatedButton", @"Use as an UAC Elevated Button", @"UAC Elevation", @"Use this button to elevate a process."));
            }

            return actions;
        }
        #endregion
    }
}

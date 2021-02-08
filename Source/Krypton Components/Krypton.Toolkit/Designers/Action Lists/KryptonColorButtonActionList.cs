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
using System.Drawing;

namespace Krypton.Toolkit
{
    internal class KryptonColorButtonActionList : DesignerActionList
    {
        #region Instance Fields
        private readonly KryptonColorButton _colorButton;
        private readonly IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonColorButtonActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonColorButtonActionList(KryptonColorButtonDesigner owner)
            : base(owner.Component)
        {
            // Remember the button instance
            _colorButton = owner.Component as KryptonColorButton;

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
            get => _colorButton.ButtonStyle;

            set
            {
                if (_colorButton.ButtonStyle != value)
                {
                    _service.OnComponentChanged(_colorButton, null, _colorButton.ButtonStyle, value);
                    _colorButton.ButtonStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the visual button orientation.
        /// </summary>
        public VisualOrientation ButtonOrientation
        {
            get => _colorButton.ButtonOrientation;

            set
            {
                if (_colorButton.ButtonOrientation != value)
                {
                    _service.OnComponentChanged(_colorButton, null, _colorButton.ButtonOrientation, value);
                    _colorButton.ButtonOrientation = value;
                }
            }
        }

        /// <summary>Gets or sets the selected colour.</summary>
        /// <value>The selected colour.</value>
        public Color SelectedColour
        {
            get => _colorButton.SelectedColor;

            set
            {
                if (_colorButton.SelectedColor != value)
                {
                    _service.OnComponentChanged(_colorButton, null, _colorButton.SelectedColor, value);

                    _colorButton.SelectedColor = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the visual drop down position.
        /// </summary>
        public VisualOrientation DropDownPosition
        {
            get => _colorButton.DropDownPosition;

            set
            {
                if (_colorButton.DropDownPosition != value)
                {
                    _service.OnComponentChanged(_colorButton, null, _colorButton.DropDownPosition, value);
                    _colorButton.DropDownPosition = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the visual drop down orientation.
        /// </summary>
        public VisualOrientation DropDownOrientation
        {
            get => _colorButton.DropDownOrientation;

            set
            {
                if (_colorButton.DropDownOrientation != value)
                {
                    _service.OnComponentChanged(_colorButton, null, _colorButton.DropDownOrientation, value);
                    _colorButton.DropDownOrientation = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the splitter or drop down functionality.
        /// </summary>
        public bool Splitter
        {
            get => _colorButton.Splitter;

            set
            {
                if (_colorButton.Splitter != value)
                {
                    _service.OnComponentChanged(_colorButton, null, _colorButton.Splitter, value);
                    _colorButton.Splitter = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the button text.
        /// </summary>
        public string Text
        {
            get => _colorButton.Values.Text;

            set
            {
                if (_colorButton.Values.Text != value)
                {
                    _service.OnComponentChanged(_colorButton, null, _colorButton.Values.Text, value);
                    _colorButton.Values.Text = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the extra button text.
        /// </summary>
        public string ExtraText
        {
            get => _colorButton.Values.ExtraText;

            set
            {
                if (_colorButton.Values.ExtraText != value)
                {
                    _service.OnComponentChanged(_colorButton, null, _colorButton.Values.ExtraText, value);
                    _colorButton.Values.ExtraText = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the button image.
        /// </summary>
        public Image Image
        {
            get => _colorButton.Values.Image;

            set
            {
                if (_colorButton.Values.Image != value)
                {
                    _service.OnComponentChanged(_colorButton, null, _colorButton.Values.Image, value);
                    _colorButton.Values.Image = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get => _colorButton.PaletteMode;

            set
            {
                if (_colorButton.PaletteMode != value)
                {
                    _service.OnComponentChanged(_colorButton, null, _colorButton.PaletteMode, value);
                    _colorButton.PaletteMode = value;
                }
            }
        }

        /// <summary>Gets or sets the font.</summary>
        /// <value>The font.</value>
        public Font ShortTextFont
        {
            get => _colorButton.StateCommon.Content.ShortText.Font;

            set
            {
                if (_colorButton.StateCommon.Content.ShortText.Font != value)
                {
                    _service.OnComponentChanged(_colorButton, null, _colorButton.StateCommon.Content.ShortText.Font, value);

                    _colorButton.StateCommon.Content.ShortText.Font = value;
                }
            }
        }

        /// <summary>Gets or sets the font.</summary>
        /// <value>The font.</value>
        public Font LongTextFont
        {
            get => _colorButton.StateCommon.Content.LongText.Font;

            set
            {
                if (_colorButton.StateCommon.Content.LongText.Font != value)
                {
                    _service.OnComponentChanged(_colorButton, null, _colorButton.StateCommon.Content.LongText.Font, value);

                    _colorButton.StateCommon.Content.LongText.Font = value;
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
            DesignerActionItemCollection actions = new DesignerActionItemCollection();

            // This can be null when deleting a control instance at design time
            if (_colorButton != null)
            {
                // Add the list of button specific actions
                actions.Add(new DesignerActionHeaderItem("Appearance"));
                actions.Add(new DesignerActionPropertyItem("Splitter", "Splitter", "Appearance", "Splitter of DropDown"));
                actions.Add(new DesignerActionPropertyItem("ButtonStyle", "ButtonStyle", "Appearance", "Button style"));
                actions.Add(new DesignerActionPropertyItem("ButtonOrientation", "ButtonOrientation", "Appearance", "Button orientation"));
                actions.Add(new DesignerActionPropertyItem("DropDownPosition", "DropDownPosition", "Appearance", "DropDown position"));
                actions.Add(new DesignerActionPropertyItem("DropDownOrientation", "DropDownOrientation", "Appearance", "DropDown orientation"));
                actions.Add(new DesignerActionPropertyItem("ShortTextFont", "Short Text Font", "Appearance", "The short text font."));
                actions.Add(new DesignerActionPropertyItem("LongTextFont", "Long Text Font", "Appearance", "The long text font."));
                actions.Add(new DesignerActionHeaderItem("Values"));
                actions.Add(new DesignerActionPropertyItem("Text", "Text", "Values", "Button text"));
                actions.Add(new DesignerActionPropertyItem("ExtraText", "ExtraText", "Values", "Button extra text"));
                actions.Add(new DesignerActionPropertyItem("Image", "Image", "Values", "Button image"));
                actions.Add(new DesignerActionPropertyItem("SelectedColour", "Selected Colour", "Values", "The selected colour."));
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("PaletteMode", "Palette", "Visuals", "Palette applied to drawing"));
            }

            return actions;
        }
        #endregion
    }
}

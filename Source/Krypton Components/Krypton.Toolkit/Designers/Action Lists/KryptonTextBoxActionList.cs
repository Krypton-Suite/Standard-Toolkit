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
    internal class KryptonTextBoxActionList : DesignerActionList
    {
        #region Instance Fields
        private readonly KryptonTextBox _textBox;
        private readonly IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonTextBoxActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonTextBoxActionList(KryptonTextBoxDesigner owner)
            : base(owner.Component)
        {
            // Remember the text box instance
            _textBox = owner.Component as KryptonTextBox;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion

        #region Public
        /// <summary>Gets or sets the context menu strip.</summary>
        /// <value>The context menu strip.</value>
        public ContextMenuStrip ContextMenuStrip
        {
            get => _textBox.ContextMenuStrip;

            set
            {
                if (_textBox.ContextMenuStrip != value)
                {
                    _service.OnComponentChanged(_textBox, null, _textBox.ContextMenuStrip, value);

                    _textBox.ContextMenuStrip = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get => _textBox.PaletteMode;

            set
            {
                if (_textBox.PaletteMode != value)
                {
                    _service.OnComponentChanged(_textBox, null, _textBox.PaletteMode, value);
                    _textBox.PaletteMode = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the input control style.
        /// </summary>
        public InputControlStyle InputControlStyle
        {
            get => _textBox.InputControlStyle;

            set
            {
                if (_textBox.InputControlStyle != value)
                {
                    _service.OnComponentChanged(_textBox, null, _textBox.InputControlStyle, value);
                    _textBox.InputControlStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the Multiline mode.
        /// </summary>
        public bool Multiline
        {
            get => _textBox.Multiline;

            set
            {
                if (_textBox.Multiline != value)
                {
                    _service.OnComponentChanged(_textBox, null, _textBox.Multiline, value);
                    _textBox.Multiline = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the WordWrap mode.
        /// </summary>
        public bool WordWrap
        {
            get => _textBox.WordWrap;

            set
            {
                if (_textBox.WordWrap != value)
                {
                    _service.OnComponentChanged(_textBox, null, _textBox.WordWrap, value);
                    _textBox.WordWrap = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the UseSystemPasswordChar mode.
        /// </summary>
        public bool UseSystemPasswordChar
        {
            get => _textBox.UseSystemPasswordChar;

            set
            {
                if (_textBox.UseSystemPasswordChar != value)
                {
                    _service.OnComponentChanged(_textBox, null, _textBox.UseSystemPasswordChar, value);
                    _textBox.UseSystemPasswordChar = value;
                }
            }
        }

        /// <summary>Gets or sets the hint.</summary>
        /// <value>The hint.</value>
        [Obsolete("Deprecated - Use CueHint.CueHintText")]
        public string Hint
        {
            get => _textBox.CueHint.CueHintText;

            set
            {
                if (_textBox.CueHint.CueHintText != value)
                {
                    _service.OnComponentChanged(_textBox, null, _textBox.CueHint.CueHintText, value);

                    _textBox.CueHint.CueHintText = value;
                }
            }
        }

        // <summary>Gets or sets the text box font.</summary>
        /// <value>The text box font.</value>
        public Font Font
        {
            get => _textBox.StateCommon.Content.Font;

            set
            {
                if (_textBox.StateCommon.Content.Font != value)
                {
                    _service.OnComponentChanged(_textBox, null, _textBox.StateCommon.Content.Font, value);

                    _textBox.StateCommon.Content.Font = value;
                }
            }
        }

        /// <summary>Gets or sets the corner radius.</summary>
        /// <value>The corner radius.</value>
        [DefaultValue(GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE)]
        public float CornerRadius
        {
            get => _textBox.StateCommon.Border.Rounding;

            set
            {
                if (_textBox.StateCommon.Border.Rounding != value)
                {
                    _service.OnComponentChanged(_textBox, null, _textBox.StateCommon.Border.Rounding, value);

                    _textBox.StateCommon.Border.Rounding = value;
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
            if (_textBox != null)
            {
                // Add the list of label specific actions
                actions.Add(new DesignerActionHeaderItem(@"Appearance"));
                actions.Add(new DesignerActionPropertyItem(@"ContextMenuStrip", @"Context Menu Strip", @"Appearance", @"The context menu strip for the control."));
                actions.Add(new DesignerActionPropertyItem(@"InputControlStyle", @"Style", @"Appearance", @"TextBox display style."));
                actions.Add(new DesignerActionPropertyItem(@"Font", @"Font", @"Appearance", @"Modifies the font of the control."));
                actions.Add(new DesignerActionPropertyItem(@"CornerRadius", @"Corner Rounding Radius", @"Appearance", @"The corner rounding radius of the control."));
                actions.Add(new DesignerActionHeaderItem(@"TextBox"));
                actions.Add(new DesignerActionPropertyItem(@"Multiline", @"Multiline", @"TextBox", @"Should text span multiple lines."));
                actions.Add(new DesignerActionPropertyItem(@"WordWrap", @"WordWrap", @"TextBox", @"Should words be wrapped over multiple lines."));
                actions.Add(new DesignerActionPropertyItem(@"UseSystemPasswordChar", @"UseSystemPasswordChar", @"TextBox", @"Should characters be Displayed in password characters."));
                actions.Add(new DesignerActionPropertyItem(@"Hint", @"Hint", @"TextBox", @"Sets the hint string for the textbox."));
                actions.Add(new DesignerActionHeaderItem(@"Visuals"));
                actions.Add(new DesignerActionPropertyItem(@"PaletteMode", @"Palette", @"Visuals", @"Palette applied to drawing"));
            }

            return actions;
        }
        #endregion
    }
}

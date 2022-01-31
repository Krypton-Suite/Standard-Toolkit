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
    internal class KryptonListBoxActionList : DesignerActionList
    {
        #region Instance Fields
        private readonly KryptonListBox _listBox;
        private readonly IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonListBoxActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonListBoxActionList(KryptonListBoxDesigner owner)
            : base(owner.Component)
        {
            // Remember the list box instance
            _listBox = owner.Component as KryptonListBox;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the syle used for list items.
        /// </summary>
        public ButtonStyle ItemStyle
        {
            get => _listBox.ItemStyle;

            set
            {
                if (_listBox.ItemStyle != value)
                {
                    _service.OnComponentChanged(_listBox, null, _listBox.ItemStyle, value);
                    _listBox.ItemStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the background drawing style.
        /// </summary>
        public PaletteBackStyle BackStyle
        {
            get => _listBox.BackStyle;

            set
            {
                if (_listBox.BackStyle != value)
                {
                    _service.OnComponentChanged(_listBox, null, _listBox.BackStyle, value);
                    _listBox.BackStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the border drawing style.
        /// </summary>
        public PaletteBorderStyle BorderStyle
        {
            get => _listBox.BorderStyle;

            set
            {
                if (_listBox.BorderStyle != value)
                {
                    _service.OnComponentChanged(_listBox, null, _listBox.BorderStyle, value);
                    _listBox.BorderStyle = value;
                }
            }
        }

        /// <summary>Gets or sets the Krypton Context Menu.</summary>
        /// <value>The Krypton Context Menu.</value>
        public KryptonContextMenu KryptonContextMenu
        {
            get => _listBox.KryptonContextMenu;

            set
            {
                if (_listBox.KryptonContextMenu != value)
                {
                    _service.OnComponentChanged(_listBox, null, _listBox.KryptonContextMenu, value);

                    _listBox.KryptonContextMenu = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the selection mode.
        /// </summary>
        public SelectionMode SelectionMode
        {
            get => _listBox.SelectionMode;

            set
            {
                if (_listBox.SelectionMode != value)
                {
                    _service.OnComponentChanged(_listBox, null, _listBox.SelectionMode, value);
                    _listBox.SelectionMode = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the selection mode.
        /// </summary>
        public bool Sorted
        {
            get => _listBox.Sorted;

            set
            {
                if (_listBox.Sorted != value)
                {
                    _service.OnComponentChanged(_listBox, null, _listBox.Sorted, value);
                    _listBox.Sorted = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get => _listBox.PaletteMode;

            set
            {
                if (_listBox.PaletteMode != value)
                {
                    _service.OnComponentChanged(_listBox, null, _listBox.PaletteMode, value);
                    _listBox.PaletteMode = value;
                }
            }
        }

        /// <summary>Gets or sets the font.</summary>
        /// <value>The font.</value>
        public Font StateCommonShortTextFont
        {
            get => _listBox.StateCommon.Item.Content.ShortText.Font;

            set
            {
                if (_listBox.StateCommon.Item.Content.ShortText.Font != value)
                {
                    _service.OnComponentChanged(_listBox, null, _listBox.StateCommon.Item.Content.ShortText.Font, value);

                    _listBox.StateCommon.Item.Content.ShortText.Font = value;
                }
            }
        }

        /// <summary>Gets or sets the font.</summary>
        /// <value>The font.</value>
        public Font StateCommonLongTextFont
        {
            get => _listBox.StateCommon.Item.Content.LongText.Font;

            set
            {
                if (_listBox.StateCommon.Item.Content.LongText.Font != value)
                {
                    _service.OnComponentChanged(_listBox, null, _listBox.StateCommon.Item.Content.LongText.Font, value);

                    _listBox.StateCommon.Item.Content.LongText.Font = value;
                }
            }
        }

        /// <summary>Gets or sets the corner radius.</summary>
        /// <value>The corner radius.</value>
        [DefaultValue(GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE)]
        public float StateCommonCornerRoundingRadius
        {
            get => _listBox.StateCommon.Border.Rounding;

            set
            {
                if (_listBox.StateCommon.Border.Rounding != value)
                {
                    _service.OnComponentChanged(_listBox, null, _listBox.StateCommon.Border.Rounding, value);

                    _listBox.StateCommon.Border.Rounding = value;
                }
            }
        }

        /// <summary>Gets or sets the item corner radius.</summary>
        /// <value>The item corner radius.</value>
        [DefaultValue(GlobalStaticValues.SECONDARY_CORNER_ROUNDING_VALUE)]
        public float StateCommonItemCornerRoundingRadius
        {
            get => _listBox.StateCommon.Item.Border.Rounding;

            set
            {
                if (_listBox.StateCommon.Item.Border.Rounding != value)
                {
                    _service.OnComponentChanged(_listBox, null, _listBox.StateCommon.Item.Border.Rounding, value);

                    _listBox.StateCommon.Item.Border.Rounding = value;
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
            if (_listBox != null)
            {
                // Add the list of list box specific actions
                actions.Add(new DesignerActionHeaderItem(@"Appearance"));
                actions.Add(new DesignerActionPropertyItem(@"BackStyle", @"Back Style", @"Appearance", @"Style used to draw background."));
                actions.Add(new DesignerActionPropertyItem(@"BorderStyle", @"Border Style", @"Appearance", @"Style used to draw the border."));
                actions.Add(new DesignerActionPropertyItem(@"KryptonContextMenu", @"Krypton Context Menu", @"Appearance", @"The Krypton Context Menu for the control."));
                actions.Add(new DesignerActionPropertyItem(@"ItemStyle", @"Item Style", @"Appearance", @"How to display list items."));
                actions.Add(new DesignerActionPropertyItem(@"StateCommonShortTextFont", @"State Common Short Text Font", @"Appearance", @"The State Common Short Text Font."));
                actions.Add(new DesignerActionPropertyItem(@"StateCommonLongTextFont", @"State Common State Common Long Text Font", @"Appearance", @"The State Common State Common Long Text Font."));
                actions.Add(new DesignerActionPropertyItem(@"StateCommonCornerRoundingRadius", @"State Common Corner Rounding Radius", @"Appearance", @"The corner rounding radius of the control."));
                actions.Add(new DesignerActionPropertyItem(@"StateCommonItemCornerRoundingRadius", @"State Common Item Corner Rounding Radius", @"Appearance", @"The corner rounding radius of the item."));
                actions.Add(new DesignerActionHeaderItem(@"Behavior"));
                actions.Add(new DesignerActionPropertyItem(@"SelectionMode", @"Selection Mode", @"Behavior", @"Determines the selection mode."));
                actions.Add(new DesignerActionPropertyItem(@"Sorted", @"Sorted", @"Behavior", @"Should items be sorted according to string."));
                actions.Add(new DesignerActionHeaderItem(@"Visuals"));
                actions.Add(new DesignerActionPropertyItem(@"PaletteMode", @"Palette", @"Visuals", @"Palette applied to drawing"));
            }

            return actions;
        }
        #endregion
    }
}
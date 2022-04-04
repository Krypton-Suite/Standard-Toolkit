#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *
 */
#endregion


using Krypton.Toolkit.Designers.Designers;

namespace Krypton.Toolkit.Designers.Action_Lists
{
    internal class KryptonListViewActionList : DesignerActionList
    {
        #region Instance Fields
        private readonly KryptonListView _listView;
        private readonly IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonCheckedListBoxActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonListViewActionList(KryptonListViewDesigner owner)
            : base(owner.Component)
        {
            // Remember the list box instance
            _listView = owner.Component as KryptonListView;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the style used for list items.
        /// </summary>
        public ButtonStyle ItemStyle
        {
            get => _listView.ItemStyle;

            set
            {
                if (_listView.ItemStyle != value)
                {
                    _service.OnComponentChanged(_listView, null, _listView.ItemStyle, value);
                    _listView.ItemStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the background drawing style.
        /// </summary>
        public PaletteBackStyle BackStyle
        {
            get => _listView.BackStyle;

            set
            {
                if (_listView.BackStyle != value)
                {
                    _service.OnComponentChanged(_listView, null, _listView.BackStyle, value);
                    _listView.BackStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the border drawing style.
        /// </summary>
        public PaletteBorderStyle BorderStyle
        {
            get => _listView.BorderStyle;

            set
            {
                if (_listView.BorderStyle != value)
                {
                    _service.OnComponentChanged(_listView, null, _listView.BorderStyle, value);
                    _listView.BorderStyle = value;
                }
            }
        }

        /// <summary>Gets or sets the Krypton Context Menu.</summary>
        /// <value>The Krypton Context Menu.</value>
        public KryptonContextMenu KryptonContextMenu
        {
            get => _listView.KryptonContextMenu;

            set
            {
                if (_listView.KryptonContextMenu != value)
                {
                    _service.OnComponentChanged(_listView, null, _listView.KryptonContextMenu, value);

                    _listView.KryptonContextMenu = value;
                }
            }
        }

        /// <summary>Gets or sets the font.</summary>
        /// <value>The font.</value>
        public Font StateCommonShortTextFont
        {
            get => _listView.StateCommon.Item.Content.ShortText.Font;

            set
            {
                if (_listView.StateCommon.Item.Content.ShortText.Font != value)
                {
                    _service.OnComponentChanged(_listView, null, _listView.StateCommon.Item.Content.ShortText.Font, value);

                    _listView.StateCommon.Item.Content.ShortText.Font = value;
                }
            }
        }

        /// <summary>Gets or sets the corner radius.</summary>
        /// <value>The corner radius.</value>
        [DefaultValue(GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE)]
        public float StateCommonCornerRoundingRadius
        {
            get => _listView.StateCommon.Border.Rounding;

            set
            {
                if (_listView.StateCommon.Border.Rounding != value)
                {
                    _service.OnComponentChanged(_listView, null, _listView.StateCommon.Border.Rounding, value);

                    _listView.StateCommon.Border.Rounding = value;
                }
            }
        }

        /// <summary>Gets or sets the item corner radius.</summary>
        /// <value>The item corner radius.</value>
        [DefaultValue(GlobalStaticValues.SECONDARY_CORNER_ROUNDING_VALUE)]
        public float StateCommonItemCornerRoundingRadius
        {
            get => _listView.StateCommon.Item.Border.Rounding;

            set
            {
                if (_listView.StateCommon.Item.Border.Rounding != value)
                {
                    _service.OnComponentChanged(_listView, null, _listView.StateCommon.Item.Border.Rounding, value);

                    _listView.StateCommon.Item.Border.Rounding = value;
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
            if (_listView != null)
            {
                // Add the list of list box specific actions
                actions.Add(new DesignerActionHeaderItem(@"Appearance"));
                actions.Add(new DesignerActionPropertyItem(@"BackStyle", @"Back Style", @"Appearance", @"Style used to draw background."));
                actions.Add(new DesignerActionPropertyItem(@"BorderStyle", @"Border Style", @"Appearance", @"Style used to draw the border."));
                actions.Add(new DesignerActionPropertyItem(@"KryptonContextMenu", @"Krypton Context Menu", @"Appearance", @"The Krypton Context Menu for the control."));
                actions.Add(new DesignerActionPropertyItem(@"ItemStyle", @"Item Style", @"Appearance", @"How to display list items."));
                actions.Add(new DesignerActionPropertyItem(@"StateCommonShortTextFont", @"State Common Short Text Font", @"Appearance", @"The State Common Short Text Font."));
                actions.Add(new DesignerActionPropertyItem(@"StateCommonCornerRoundingRadius", @"State Common Corner Rounding Radius", @"Appearance", @"The corner rounding radius of the control."));
                actions.Add(new DesignerActionPropertyItem(@"StateCommonItemCornerRoundingRadius", @"State Common Item Corner Rounding Radius", @"Appearance", @"The corner rounding radius of the item."));
                actions.Add(new DesignerActionHeaderItem(@"Behavior"));
                actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            }

            return actions;
        }
        #endregion
    }
}

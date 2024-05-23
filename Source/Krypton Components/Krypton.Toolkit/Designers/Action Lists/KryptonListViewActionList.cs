#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2024. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    internal class KryptonListViewActionList : DesignerActionList
    {
        #region Instance Fields
        private readonly KryptonListView? _listView;
        private readonly IComponentChangeService? _service;
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
            _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the style used for list items.
        /// </summary>
        public ButtonStyle ItemStyle
        {
            get => _listView!.ItemStyle;

            set
            {
                if (_listView!.ItemStyle != value)
                {
                    _service?.OnComponentChanged(_listView, null, _listView!.ItemStyle, value);
                    _listView!.ItemStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the background drawing style.
        /// </summary>
        public PaletteBackStyle BackStyle
        {
            get => _listView!.BackStyle;

            set
            {
                if (_listView!.BackStyle != value)
                {
                    _service?.OnComponentChanged(_listView, null, _listView!.BackStyle, value);
                    _listView!.BackStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the border drawing style.
        /// </summary>
        public PaletteBorderStyle BorderStyle
        {
            get => _listView!.BorderStyle;

            set
            {
                if (_listView!.BorderStyle != value)
                {
                    _service?.OnComponentChanged(_listView, null, _listView!.BorderStyle, value);
                    _listView!.BorderStyle = value;
                }
            }
        }

        /// <summary>Gets or sets the Krypton Context Menu.</summary>
        /// <value>The Krypton Context Menu.</value>
        public KryptonContextMenu? KryptonContextMenu
        {
            get => _listView!.KryptonContextMenu;

            set
            {
                if (_listView!.KryptonContextMenu != value)
                {
                    _service?.OnComponentChanged(_listView, null, _listView!.KryptonContextMenu, value);

                    _listView!.KryptonContextMenu = value;
                }
            }
        }

        /// <summary>Gets or sets the font.</summary>
        /// <value>The font.</value>
        public Font StateCommonShortTextFont
        {
            get => _listView!.StateCommon.Item.Content.ShortText.Font!;

            set
            {
                if (_listView!.StateCommon.Item.Content.ShortText.Font != value)
                {
                    _service?.OnComponentChanged(_listView, null, _listView!.StateCommon.Item.Content.ShortText.Font, value);

                    _listView!.StateCommon.Item.Content.ShortText.Font = value;
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
            var actions = new DesignerActionItemCollection
            {
                // This can be null when deleting a control instance at design time
                // Add the list of list box specific actions
                new DesignerActionHeaderItem(nameof(Appearance)),
                new DesignerActionPropertyItem(nameof(BackStyle), @"Back Style", nameof(Appearance), @"Style used to draw background."),
                new DesignerActionPropertyItem(nameof(BorderStyle), @"Border Style", nameof(Appearance), @"Style used to draw the border."),
                new DesignerActionPropertyItem(nameof(KryptonContextMenu), @"Krypton Context Menu", nameof(Appearance), @"The Krypton Context Menu for the control."),
                new DesignerActionPropertyItem(nameof(ItemStyle), @"Item Style", nameof(Appearance), @"How to display list items."),
                new DesignerActionPropertyItem(nameof(StateCommonShortTextFont), @"State Common Short Text Font", nameof(Appearance), @"The State Common Short Text Font."),
            };

            return actions;
        }
        #endregion
    }
}

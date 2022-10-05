﻿namespace Krypton.Toolkit
{
    internal class KryptonDomainUpDownActionList : DesignerActionList
    {
        #region Instance Fields
        private readonly KryptonDomainUpDown _domainUpDown;
        private readonly IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDomainUpDownActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonDomainUpDownActionList(KryptonDomainUpDownDesigner owner)
            : base(owner.Component)
        {
            // Remember the text box instance
            _domainUpDown = owner.Component as KryptonDomainUpDown;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion

        #region Public
        /// <summary>Gets or sets the Krypton Context Menu.</summary>
        /// <value>The Krypton Context Menu.</value>
        public KryptonContextMenu KryptonContextMenu
        {
            get => _domainUpDown.KryptonContextMenu;

            set
            {
                if (_domainUpDown.KryptonContextMenu != value)
                {
                    _service.OnComponentChanged(_domainUpDown, null, _domainUpDown.KryptonContextMenu, value);

                    _domainUpDown.KryptonContextMenu = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get => _domainUpDown.PaletteMode;

            set
            {
                if (_domainUpDown.PaletteMode != value)
                {
                    _service.OnComponentChanged(_domainUpDown, null, _domainUpDown.PaletteMode, value);
                    _domainUpDown.PaletteMode = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the input control style.
        /// </summary>
        public InputControlStyle InputControlStyle
        {
            get => _domainUpDown.InputControlStyle;

            set
            {
                if (_domainUpDown.InputControlStyle != value)
                {
                    _service.OnComponentChanged(_domainUpDown, null, _domainUpDown.InputControlStyle, value);
                    _domainUpDown.InputControlStyle = value;
                }
            }
        }

        public Font Font
        {
            get => _domainUpDown.StateCommon.Content.Font;

            set
            {
                if (_domainUpDown.StateCommon.Content.Font != value)
                {
                    _service.OnComponentChanged(_domainUpDown, null, _domainUpDown.StateCommon.Content.Font, value);

                    _domainUpDown.StateCommon.Content.Font = value;
                }
            }
        }

        /// <summary>Gets or sets the corner radius.</summary>
        /// <value>The corner radius.</value>
        [DefaultValue(GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE)]
        public float StateCommonCornerRoundingRadius
        {
            get => _domainUpDown.StateCommon.Border.Rounding;

            set
            {
                if (_domainUpDown.StateCommon.Border.Rounding != value)
                {
                    _service.OnComponentChanged(_domainUpDown, null, _domainUpDown.StateCommon.Border.Rounding, value);

                    _domainUpDown.StateCommon.Border.Rounding = value;
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
            if (_domainUpDown != null)
            {
                // Add the list of label specific actions
                actions.Add(new DesignerActionHeaderItem(@"Appearance"));
                actions.Add(new DesignerActionPropertyItem(@"KryptonContextMenu", @"Krypton Context Menu", @"Appearance", @"The Krypton Context Menu for the control."));
                actions.Add(new DesignerActionPropertyItem(@"InputControlStyle", @"Style", @"Appearance", @"DomainUpDown display style."));
                actions.Add(new DesignerActionPropertyItem(@"Font", @"Font", @"Appearance", @"The font for the domain up down."));
                actions.Add(new DesignerActionPropertyItem(@"StateCommonCornerRoundingRadius", @"State Common Corner Rounding Radius", @"Appearance", @"The corner rounding radius of the control."));
                actions.Add(new DesignerActionHeaderItem(@"Visuals"));
                actions.Add(new DesignerActionPropertyItem(@"PaletteMode", @"Palette", @"Visuals", @"Palette applied to drawing"));
            }

            return actions;
        }
        #endregion
    }
}

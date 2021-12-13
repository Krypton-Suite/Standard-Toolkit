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
        /// <summary>Gets or sets the context menu strip.</summary>
        /// <value>The context menu strip.</value>
        public ContextMenuStrip ContextMenuStrip
        {
            get => _domainUpDown.ContextMenuStrip;

            set
            {
                if (_domainUpDown.ContextMenuStrip != value)
                {
                    _service.OnComponentChanged(_domainUpDown, null, _domainUpDown.ContextMenuStrip, value);

                    _domainUpDown.ContextMenuStrip = value;
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
        public float CornerRadius
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
                actions.Add(new DesignerActionPropertyItem(@"ContextMenuStrip", @"Context Menu Strip", @"Appearance", @"The context menu strip for the control."));
                actions.Add(new DesignerActionPropertyItem(@"InputControlStyle", @"Style", @"Appearance", @"DomainUpDown display style."));
                actions.Add(new DesignerActionPropertyItem(@"Font", @"Font", @"Appearance", @"The font for the domain up down."));
                actions.Add(new DesignerActionPropertyItem(@"CornerRadius", @"Corner Rounding Radius", @"Appearance", @"The corner rounding radius of the control."));
                actions.Add(new DesignerActionHeaderItem(@"Visuals"));
                actions.Add(new DesignerActionPropertyItem(@"PaletteMode", @"Palette", @"Visuals", @"Palette applied to drawing"));
            }

            return actions;
        }
        #endregion
    }
}
